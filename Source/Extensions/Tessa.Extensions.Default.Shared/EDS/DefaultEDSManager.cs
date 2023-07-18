using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Localization;
using Tessa.Platform.EDS;

namespace Tessa.Extensions.Default.Shared.EDS
{
    public class DefaultEDSManager : CAdESManager
    {
        public DefaultEDSManager(
            ICardRepository cardRepository,
            ICardCache cardCache)
            : base(cardRepository, cardCache)
        {
        }


        /// <inheritdoc />
        public override async ValueTask<byte[]> GenerateSignatureAsync(
            byte[] certificate,
            ISignatureFile file,
            string hashAlgo,
            CancellationToken cancellationToken = default)
        {
            if (certificate != null)
            {
                // Создаем объект ContentInfo по сообщению.
                // Это необходимо для создания объекта SignedCms.
                ContentInfo contentInfo = new ContentInfo(await file.GetBytesAsync(cancellationToken).ConfigureAwait(false));

                // Создаем объект SignedCms по только что созданному объекту ContentInfo.
                // SubjectIdentifierType установлен по умолчанию в IssuerAndSerialNumber.
                // Свойство Detached устанавливаем явно в true, т.о. сообщение будет отделено от подписи.
                SignedCms signedCms = new SignedCms(contentInfo, true);

                // Определяем подписывающего, объектом CmsSigner.
                var certObj = new X509Certificate2(certificate);

                X509Certificate2 cert = null;
                using (X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser))
                {
                    store.Open(OpenFlags.ReadOnly);

                    foreach (var certItem in store.Certificates)
                    {
                        if (string.Equals(certItem.SerialNumber, certObj.SerialNumber, StringComparison.OrdinalIgnoreCase))
                        {
                            cert = certItem;
                            break;
                        }
                    }
                }

                if (cert == null)
                {
                    throw new InvalidOperationException("Certificate isn't found");
                }

                var cmsSigner = new CmsSigner(cert) { DigestAlgorithm = new Oid(hashAlgo) };

                // Подписываем CMS/PKCS #7 сообщение.
                // false - выбор сертификата для подписи, если выбранный недействителен
                signedCms.ComputeSignature(cmsSigner, false);

                return signedCms.SignerInfos[0].GetSignature();
            }

            return null;
        }


        /// <inheritdoc />
        public override async ValueTask<(bool Success, string ErrorText)> VerifySignatureAsync(
            byte[] encodedSignature,
            ISignatureFile file,
            CancellationToken cancellationToken = default)
        {
            bool success = true;
            //string trustError = null;
            string hashError = null;
            string revocationError = null;
            string errorText;

            // Создаем объект ContentInfo по сообщению.
            // Это необходимо для создания объекта SignedCms.
            var contentInfo = new ContentInfo(await file.GetBytesAsync(cancellationToken).ConfigureAwait(false));

            // Создаем SignedCms для декодирования и проверки.
            var signature = new SignedCms(contentInfo, true);

            // Перехватываем криптографические исключения, для
            // возврата о false значения при некорректности подписи.
            try
            {
                // Декодируем подпись
                signature.Decode(encodedSignature);
            }
            catch (CryptographicException e)
            {
                errorText = e.Message.Trim();
                return (false, errorText);
            }

            // Проверяем подпись
            // Проверяем целостность хэша
            try
            {
                signature.CheckHash();
            }
            catch (CryptographicException e)
            {
                hashError = e.Message.Trim();
                success = false;
            }

            // Проверяем саму подпись, проверка сертификатов происходит на сервере
            try
            {
                signature.CheckSignature(true);
            }
            catch (CryptographicException e)
            {
                revocationError = e.Message.Trim();
                success = false;
            }

            if (!success)
            {
                errorText = LocalizationManager.GetString("Platform_EDSCheckErrors");

                // trustError не используется
                // if (!string.IsNullOrWhiteSpace(trustError))
                // {
                //     errorText += Environment.NewLine + trustError;
                // }

                if (!string.IsNullOrWhiteSpace(hashError))
                {
                    errorText += Environment.NewLine + hashError;
                }

                if (!string.IsNullOrWhiteSpace(revocationError)
                    // trustError не используется
                    // && !string.Equals(trustError, revocationError, StringComparison.CurrentCulture)
                )
                {
                    errorText += Environment.NewLine + revocationError;
                }
            }
            else
            {
                errorText = null;
            }

            return (success, errorText);
        }


        /// <inheritdoc />
        public override (IEDSCertificate Certificate, string ErrorText) DecodeCertificateFromSignature(
            byte[] encodedSignature,
            CancellationToken cancellationToken = default)
        {
            // Создаем SignedCms для декодирования.
            var signature = new SignedCms();

            // Перехватываем криптографические исключения.
            try
            {
                // Декодируем подпись
                signature.Decode(encodedSignature);
                return (new EDSCertificate(signature.SignerInfos[0].Certificate), null);
            }
            catch (CryptographicException e)
            {
                return (null, e.Message.Trim());
            }
        }
    }
}