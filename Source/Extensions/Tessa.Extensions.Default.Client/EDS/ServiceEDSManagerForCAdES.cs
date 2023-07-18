using System;
using System.IO;
using System.Security.Cryptography.Pkcs;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Shared.EDS;
using Tessa.Host.EDS;
using Tessa.Platform.EDS;
using Tessa.Platform.IO;

namespace Tessa.Extensions.Default.Client.EDS
{
    /// <summary>
    /// <para>Выполняет подписание в отдельном хост-процессе с использованием Windows Crypto API.
    /// Реализация <see cref="IEDSManager.DecodeCertificateFromSignature"/> наследуется от <see cref="DefaultEDSManager"/>.</para>
    /// <para>Для регистрации создайте <c>[Registrator(Order = 2)]</c>, в методе <c>RegisterUnity</c> укажите
    /// <c>UnityContainer.RegisterType&lt;ICAdESManager, ServiceEDSManager&gt;(new ContainerControlledLifetimeManager())</c></para>
    /// </summary>
    public class ServiceEDSManagerForCAdES : DefaultEDSManager
    {
        #region Constructors

        public ServiceEDSManagerForCAdES(
            IEdsService service,
            ICardRepository cardRepository,
            ICardCache cardCache)
            : base(cardRepository, cardCache)
        {
            this.service = service ?? throw new ArgumentNullException(nameof(service));
        }

        #endregion

        #region Fields

        private readonly IEdsService service;

        #endregion

        #region Base Overrides

        public override async Task<SignatureData> SignDocumentAsync(
            byte[] certificate,
            ISignatureFile file,
            CancellationToken cancellationToken = default)
        {
            var settingsCard = (await this.CardCache.Cards.GetAsync(SignatureHelper.SignatureSettingsType, cancellationToken).ConfigureAwait(false)).GetValue();
            var (digestOid, encryptOid, _) = SignatureHelper.GetDigestAndEcryptOid(settingsCard, certificate);

            var bes = await GenerateSignatureAsync(certificate, file, digestOid, cancellationToken).ConfigureAwait(false);

            var signedCms = new SignedCms();
            signedCms.Decode(bes);

            Pkcs9SigningTime pkcs9SigningTime = null;

            foreach (var attr in signedCms.SignerInfos[0].SignedAttributes)
            {
                foreach (var val in attr.Values)
                {
                    if ((pkcs9SigningTime = val as Pkcs9SigningTime) != null)
                    {
                        break;
                    }
                }

                if (pkcs9SigningTime != null)
                {
                    break;
                }
            }

            DateTime signingTime;
            byte[] signatureValue;

            if (pkcs9SigningTime != null)
            {
                signingTime = pkcs9SigningTime.SigningTime;
                signatureValue = signedCms.SignerInfos[0].GetSignature();
            }
            else
            {
                throw new ArgumentNullException(nameof(pkcs9SigningTime));
            }

            byte[] signature = await MakeBesSignaturePkcs7Async(certificate, file, signingTime, signatureValue, digestOid, encryptOid, cancellationToken).ConfigureAwait(false);
            return new SignatureData(signature);
        }


        /// <inheritdoc />
        public override async ValueTask<byte[]> GenerateSignatureAsync(
            byte[] certificate,
            ISignatureFile file,
            string hashAlgo,
            CancellationToken cancellationToken = default)
        {
            ITempFile tempFile = null;

            try
            {
                string filePath = file.TryGetLocalPath();
                if (string.IsNullOrEmpty(filePath))
                {
                    byte[] bytes = await file.GetBytesAsync(cancellationToken).ConfigureAwait(false);

                    tempFile = TempFile.Acquire("eds-sign.bin");
                    await File.WriteAllBytesAsync(tempFile.Path, bytes, cancellationToken).ConfigureAwait(false);

                    filePath = tempFile.Path;
                }

                byte[] cmsData = await this.service.GenerateSignatureAsync(filePath, hashAlgo, certificate, cancellationToken).ConfigureAwait(false);
                return cmsData;
            }
            finally
            {
                tempFile?.Dispose();
            }
        }


        /// <inheritdoc />
        public override async ValueTask<(bool Success, string ErrorText)> VerifySignatureAsync(
            byte[] encodedSignature,
            ISignatureFile file,
            CancellationToken cancellationToken = default)
        {
            ITempFile tempFile = null;

            try
            {
                string filePath = file.TryGetLocalPath();
                if (string.IsNullOrEmpty(filePath))
                {
                    byte[] bytes = await file.GetBytesAsync(cancellationToken).ConfigureAwait(false);

                    tempFile = TempFile.Acquire("eds-verify.bin");
                    await File.WriteAllBytesAsync(tempFile.Path, bytes, cancellationToken).ConfigureAwait(false);

                    filePath = tempFile.Path;
                }

                return await this.service.VerifySignatureAsync(filePath, encodedSignature, cancellationToken).ConfigureAwait(false);
            }
            finally
            {
                tempFile?.Dispose();
            }
        }

        #endregion
    }
}