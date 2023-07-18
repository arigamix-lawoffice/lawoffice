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
    public class ServiceEDSManagerForCMS : DefaultEDSManager
    {
        #region Constructors

        public ServiceEDSManagerForCMS(
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

                var signedCms = new SignedCms();
                signedCms.Decode(cmsData);

                return signedCms.SignerInfos[0].GetSignature();
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