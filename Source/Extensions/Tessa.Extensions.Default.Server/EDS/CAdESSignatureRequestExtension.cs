using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.EDS;
using Tessa.Platform.EDS;
using Tessa.Platform.Storage;
using Tessa.Properties.Resharper;
using Unity;

namespace Tessa.Extensions.Default.Server.EDS
{
    public sealed class CAdESSignatureRequestExtension :
        CardRequestExtension
    {
        #region Constructors

        public CAdESSignatureRequestExtension([OptionalDependency] IEDSProvider edsProvider = null) =>
            this.edsProvider = edsProvider;

        #endregion

        #region Fields

        [CanBeNull]
        private readonly IEDSProvider edsProvider;

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardRequestExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            Dictionary<string, object> info = context.Request.Info;

            var signature = info.Get<string>(CAdESSignatureKeys.SignatureKey);
            var edsAction = (EDSAction) info.Get<int>(CAdESSignatureKeys.EDSActionKey);

            switch (edsAction)
            {
                case EDSAction.Sign:
                {
                    var certificateString = info.TryGet<string>(CAdESSignatureKeys.CertificateKey);

                    (string result, SignatureType signatureType, SignatureProfile signatureProfile) =
                        this.edsProvider is not null
                            ? await this.edsProvider.ExtendDocumentAsync(
                                signature,
                                certificateString,
                                info,
                                context.CancellationToken)
                            : (string.Empty, SignatureType.None, SignatureProfile.None);

                    context.Response.Info[CAdESSignatureKeys.SignatureKey] = result;
                    context.Response.Info[CAdESSignatureKeys.SignatureTypeKey] = (int) signatureType;
                    context.Response.Info[CAdESSignatureKeys.SignatureProfileKey] = (int) signatureProfile;
                    break;
                }

                case EDSAction.Verify:
                {
                    var targetSignatureType = (SignatureType) info.Get<int>(CAdESSignatureKeys.TargetSignatureTypeKey);
                    var targetSignatureProfile = (SignatureProfile) info.Get<int>(CAdESSignatureKeys.TargetSignatureProfileKey);

                    List<Dictionary<string, object>> signaturesValidations =
                        (this.edsProvider is not null
                            ? await this.edsProvider.ValidateDocumentAsync(
                                signature,
                                targetSignatureType,
                                targetSignatureProfile,
                                info,
                                context.CancellationToken)
                            : null)
                        ?? new List<Dictionary<string, object>>();

                    context.Response.Info[".signatureValidation"] = signaturesValidations;
                    break;
                }

                case EDSAction.GetBesFromExtended:
                {
                    context.Response.Info[CAdESSignatureKeys.SignatureKey] =
                        (this.edsProvider is not null
                            ? await this.edsProvider.GetBesSignatureAsync(
                                signature,
                                info,
                                context.CancellationToken)
                            : null)
                        ?? string.Empty;
                    break;
                }

                case EDSAction.GetToBeSigned:
                {
                    var certificate = info.Get<string>(CAdESSignatureKeys.CertificateKey);
                    var signingTime = info.Get<DateTime>(CAdESSignatureKeys.SigningTimeKey);
                    var digestAlgorithm = info.TryGet<string>(CAdESSignatureKeys.DigestAlgorithmKey);
                    var encryptionAlgorithm = info.TryGet<string>(CAdESSignatureKeys.EncryptionAlgorithmKey);

                    context.Response.Info[CAdESSignatureKeys.SignatureKey] =
                        (this.edsProvider is not null
                            ? await this.edsProvider.GetToBeSignedAsync(
                                signature,
                                certificate,
                                signingTime,
                                digestAlgorithm,
                                encryptionAlgorithm,
                                info,
                                context.CancellationToken)
                            : null)
                        ?? string.Empty;
                    break;
                }

                case EDSAction.GetBesSignature:
                {
                    var file = info.Get<string>(CAdESSignatureKeys.FileKey);
                    var certificate = info.Get<string>(CAdESSignatureKeys.CertificateKey);
                    var signingTime = info.Get<DateTime>(CAdESSignatureKeys.SigningTimeKey);
                    var digestAlgorithm = info.TryGet<string>(CAdESSignatureKeys.DigestAlgorithmKey);
                    var encryptionAlgorithm = info.TryGet<string>(CAdESSignatureKeys.EncryptionAlgorithmKey);

                    context.Response.Info[CAdESSignatureKeys.SignatureKey] =
                        (this.edsProvider is not null
                            ? await this.edsProvider.GetSignedDocumentAsync(
                                signature,
                                file,
                                certificate,
                                signingTime,
                                digestAlgorithm,
                                encryptionAlgorithm,
                                info,
                                context.CancellationToken)
                            : null)
                        ?? string.Empty;
                    break;
                }

                case EDSAction.GetSignatureAttributesFromSignature:
                {
                    SignatureAttributes signatureAttributes =
                        this.edsProvider is not null
                            ? await this.edsProvider.GetSignatureAttributesFromSignatureAsync(
                                signature,
                                info,
                                context.CancellationToken)
                            : null;

                    context.Response.Info[CAdESSignatureKeys.HashKey] =
                        signatureAttributes?.Hash is not null
                            ? Convert.ToBase64String(signatureAttributes.Hash)
                            : string.Empty;

                    context.Response.Info[CAdESSignatureKeys.HashOidKey] =
                        signatureAttributes?.HashOid ?? string.Empty;

                    context.Response.Info[CAdESSignatureKeys.CertificateKey] =
                        signatureAttributes?.Certificate is not null
                            ? Convert.ToBase64String(signatureAttributes.Certificate)
                            : string.Empty;

                    context.Response.Info[CAdESSignatureKeys.SignatureKey] =
                        signatureAttributes?.Signature is not null
                            ? Convert.ToBase64String(signatureAttributes.Signature)
                            : string.Empty;

                    context.Response.Info[CAdESSignatureKeys.SignedAttributesKey] =
                        signatureAttributes?.SignedAttributes is not null
                            ? Convert.ToBase64String(signatureAttributes.SignedAttributes)
                            : string.Empty;
                    break;
                }
            }
        }

        #endregion
    }
}