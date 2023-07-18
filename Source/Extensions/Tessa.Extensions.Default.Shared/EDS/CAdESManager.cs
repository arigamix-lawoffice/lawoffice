using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.Pkcs;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Platform.EDS;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Shared.EDS
{
    public abstract class CAdESManager : ICAdESManager
    {
        #region Constructors

        protected CAdESManager(ICardRepository cardRepository, ICardCache cardCache)
        {
            this.CardRepository = cardRepository;
            this.CardCache = cardCache;
        }

        #endregion

        #region Protected Properties

        protected ICardRepository CardRepository { get; }

        protected ICardCache CardCache { get; }

        #endregion

        public virtual async Task<IReadOnlyCollection<SignatureValidationInfo>> CheckExtendedSignatureAsync(
            SignedData signedData,
            CancellationToken cancellationToken = default)
        {
            var request = new CardRequest
            {
                RequestType = DefaultRequestTypes.CAdESSignature,
            };

            CAdESSignatureHelper.SetValidationInfo(
                request,
                signedData
            );

            var response = await this.CardRepository.RequestAsync(request, cancellationToken).ConfigureAwait(false);
            var result = response.ValidationResult.Build();
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            return CAdESSignatureHelper.GetValidationInfo(response);
        }

        public virtual (IEDSCertificate Certificate, string ErrorText) DecodeCertificateFromSignature(
            byte[] encodedSignature,
            CancellationToken cancellationToken = default)
        {
            try
            {
                var cms = new SignedCms();
                try
                {
                    // Декодируем подпись
                    cms.Decode(encodedSignature);
                }
                catch (CryptographicException e)
                {
                    var errorText = e.Message.Trim();
                    return (null, errorText);
                }
                return (new EDSCertificate(cms.SignerInfos[0].Certificate), null);
            }
            catch (Exception e)
            {
                return (null, e.Message.Trim());
            }
        }

        public virtual async ValueTask<byte[]> GetSignatureBytesFromFileAsync(string filePath, CancellationToken cancellationToken = default)
        {
            var bytes = await File.ReadAllBytesAsync(filePath, cancellationToken).ConfigureAwait(false);
            try
            {
                if (bytes.Length < 2)
                {
                    return bytes;
                }

                // Check for PEM and remove headers and footers
                string signStr = System.Text.Encoding.UTF8.GetString(bytes);
                var matches = Regex.Matches(signStr, "-{5}.+-{5}");
                if (matches.Count == 2)
                {
                    var start = signStr.IndexOf(matches[0].Value, StringComparison.Ordinal);
                    signStr = signStr.Substring(start + matches[0].Value.Length);
                    var end = signStr.IndexOf(matches[1].Value, StringComparison.Ordinal);
                    signStr = signStr.Substring(0, end);
                    signStr = signStr.Replace("\r", string.Empty, StringComparison.Ordinal).Replace("\n", string.Empty, StringComparison.Ordinal);
                }
                return Convert.FromBase64String(signStr);
            }
            catch
            {
                return bytes;
            }
        }

        public virtual async Task<SignedData> ExtendSignatureAsync(
            byte[] certificate,
            SignatureData signature,
            CancellationToken cancellationToken = default)
        {
            if (signature is null)
            {
                throw new ArgumentNullException(nameof(signature));
            }

            var request = new CardRequest
            {
                RequestType = DefaultRequestTypes.CAdESSignature,
            };

            CAdESSignatureHelper.SetSigningInfo(
                request,
                signature.Data,
                certificate
            );

            var response = await this.CardRepository.RequestAsync(request, cancellationToken).ConfigureAwait(false);
            var result = response.ValidationResult.Build();
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            return CAdESSignatureHelper.GetSignedData(response);
        }

        public virtual async Task<SignatureData> SignDocumentAsync(
            byte[] certificate,
            ISignatureFile file,
            CancellationToken cancellationToken = default)
        {
            var signingTime = DateTime.UtcNow;
            var settingsCard = (await this.CardCache.Cards.GetAsync(SignatureHelper.SignatureSettingsType, cancellationToken).ConfigureAwait(false)).GetValue();
            var (digestOid, encryptOid, _) = SignatureHelper.GetDigestAndEcryptOid(settingsCard, certificate);
            var toBeSigned = await GetToBeSignedDocumentAsync(certificate, file, signingTime, digestOid, encryptOid, cancellationToken).ConfigureAwait(false);
            byte[] signatureValue = await GenerateSignatureAsync(certificate, new MemorySignatureFile(toBeSigned), digestOid, cancellationToken).ConfigureAwait(false);

            byte[] signature = await MakeBesSignaturePkcs7Async(certificate, file, signingTime, signatureValue, digestOid, encryptOid, cancellationToken).ConfigureAwait(false);
            return new SignatureData(signature);
        }

        public async Task<byte[]> GetBesSignatureFromExtendedAsync(byte[] signature, CancellationToken cancellationToken = default)
        {
            var request = new CardRequest
            {
                RequestType = DefaultRequestTypes.CAdESSignature,
            };

            CAdESSignatureHelper.SetGetBESInfo(
                request,
                signature
            );

            var response = await this.CardRepository.RequestAsync(request, cancellationToken).ConfigureAwait(false);
            var result = response.ValidationResult.Build();
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            return CAdESSignatureHelper.GetBESInfo(response);
        }

        public async Task<SignatureAttributes> GetAttributesFromSignatureAsync(byte[] signature, CancellationToken cancellationToken = default)
        {
            var request = new CardRequest
            {
                RequestType = DefaultRequestTypes.CAdESSignature,
            };

            CAdESSignatureHelper.SetGetSignedAttributesInfo(
                request,
                signature
            );

            var response = await this.CardRepository.RequestAsync(request, cancellationToken).ConfigureAwait(false);
            var result = response.ValidationResult.Build();
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            return CAdESSignatureHelper.GetSignatureAttributes(response);
        }

        public async Task<byte[]> GetToBeSignedDocumentAsync(
            byte[] certificate,
            ISignatureFile file,
            DateTime signingTime,
            string digestAlgorithmOid = null,
            string encryptionAlgorithmOid = null,
            CancellationToken cancellationToken = default)
        {
            var request = new CardRequest
            {
                RequestType = DefaultRequestTypes.CAdESSignature,
            };

            CAdESSignatureHelper.SetToBeSignedInfo(
                request,
                certificate,
                await file.GetBytesAsync(cancellationToken).ConfigureAwait(false),
                signingTime,
                digestAlgorithmOid,
                encryptionAlgorithmOid
            );

            var response = await this.CardRepository.RequestAsync(request, cancellationToken).ConfigureAwait(false);
            var result = response.ValidationResult.Build();
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            return CAdESSignatureHelper.GetToBeSignedInfo(response);
        }

        public async Task<byte[]> MakeBesSignaturePkcs7Async(
            byte[] certificate,
            ISignatureFile file,
            DateTime signingTime,
            byte[] signature,
            string digestAlgorithmOid = null,
            string encryptionAlgorithmOid = null,
            CancellationToken cancellationToken = default)
        {
            var request = new CardRequest
            {
                RequestType = DefaultRequestTypes.CAdESSignature,
            };

            CAdESSignatureHelper.SetBesSignaturePkcs7Info(
                request,
                certificate,
                await file.GetBytesAsync(cancellationToken).ConfigureAwait(false),
                signingTime,
                signature,
                digestAlgorithmOid,
                encryptionAlgorithmOid
            );

            var response = await this.CardRepository.RequestAsync(request, cancellationToken).ConfigureAwait(false);
            var result = response.ValidationResult.Build();
            if (!result.IsSuccessful)
            {
                throw new ValidationException(result);
            }

            return CAdESSignatureHelper.GetBesSignaturePkcs7Info(response);
        }

        public abstract ValueTask<byte[]> GenerateSignatureAsync(
            byte[] certificate,
            ISignatureFile file,
            string digestOid,
            CancellationToken cancellationToken = default);

        public abstract ValueTask<(bool Success, string ErrorText)> VerifySignatureAsync(
            byte[] encodedSignature,
            ISignatureFile file,
            CancellationToken cancellationToken = default);
    }
}
