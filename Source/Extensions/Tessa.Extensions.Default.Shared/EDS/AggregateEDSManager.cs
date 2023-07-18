using System;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Platform;
using Tessa.Platform.EDS;
using Unity;

namespace Tessa.Extensions.Default.Shared.EDS
{
    public class AggregateEDSManager : CAdESManager
    {
        #region Fields

        private readonly IUnityContainer unityContainer;

        private readonly Func<ICAdESManager> resolveFallbackEdsManagerFunc;

        #endregion

        #region Constructors

        public AggregateEDSManager(
            ICardRepository cardRepository,
            ICardCache cardCache,
            IUnityContainer unityContainer,
            Func<ICAdESManager> resolveFallbackEdsManagerFunc)
            : base(cardRepository, cardCache)
        {
            this.unityContainer = unityContainer;
            this.resolveFallbackEdsManagerFunc = resolveFallbackEdsManagerFunc;
        }

        #endregion

        #region Private Methods

        private async Task<ICAdESManager> ResolveManager(byte[] certificate)
        {
            var settingsCard = (await this.CardCache.Cards.GetAsync(SignatureHelper.SignatureSettingsType).ConfigureAwait(false)).GetValue();
            (_, _, string edsManagerName) = SignatureHelper.GetDigestAndEcryptOid(settingsCard, certificate);

            return this.unityContainer.TryResolve<ICAdESManager>(edsManagerName) ?? this.resolveFallbackEdsManagerFunc()
                ?? throw new InvalidOperationException("No fallback manager is provided for " + nameof(AggregateEDSManager));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<SignatureData> SignDocumentAsync(byte[] certificate, ISignatureFile file, CancellationToken cancellationToken = default) =>
            await (await this.ResolveManager(certificate).ConfigureAwait(false))
                .SignDocumentAsync(certificate, file, cancellationToken).ConfigureAwait(false);

        /// <inheritdoc />
        public override async ValueTask<(bool Success, string ErrorText)> VerifySignatureAsync(
            byte[] encodedSignature,
            ISignatureFile file,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var signature = new SignedCms(new ContentInfo(
                    await file.GetBytesAsync(cancellationToken).ConfigureAwait(false)), true);

                signature.Decode(encodedSignature);
                X509Certificate2 certificate = signature.SignerInfos[0].Certificate;

                return await (await this.ResolveManager(certificate.RawData).ConfigureAwait(false))
                    .VerifySignatureAsync(encodedSignature, file, cancellationToken).ConfigureAwait(false);
            }
            catch (CryptographicException e)
            {
                string errorText = e.Message.Trim();
                return await new ValueTask<(bool Success, string ErrorText)>((false, errorText));
            }
        }

        /// <inheritdoc />
        public override ValueTask<byte[]> GenerateSignatureAsync(
            byte[] certificate,
            ISignatureFile file,
            string digestOid,
            CancellationToken cancellationToken = default) =>
            throw new NotSupportedException();

        #endregion
    }
}
