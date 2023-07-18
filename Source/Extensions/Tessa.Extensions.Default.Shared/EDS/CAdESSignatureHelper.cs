using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Tessa.Cards;
using Tessa.Files;
using Tessa.Platform.EDS;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.EDS
{
    public static class CAdESSignatureHelper
    {
        #region Static methods

        public static void SetSigningInfo(
            CardRequest request,
            byte[] signature,
            byte[] certificate)
        {
            request.Info[CAdESSignatureKeys.SignatureKey] = Convert.ToBase64String(signature);
            request.Info[CAdESSignatureKeys.EDSActionKey] = (int)EDSAction.Sign;
            request.Info[CAdESSignatureKeys.CertificateKey] = Convert.ToBase64String(certificate);
        }

        public static void SetGetBESInfo(
            CardRequest request,
            byte[] signature)
        {
            request.Info[CAdESSignatureKeys.SignatureKey] = Convert.ToBase64String(signature);
            request.Info[CAdESSignatureKeys.EDSActionKey] = (int)EDSAction.GetBesFromExtended;
        }

        public static void SetGetSignedAttributesInfo(
            CardRequest request,
            byte[] signature)
        {
            request.Info[CAdESSignatureKeys.SignatureKey] = Convert.ToBase64String(signature);
            request.Info[CAdESSignatureKeys.EDSActionKey] = (int)EDSAction.GetSignatureAttributesFromSignature;
        }

        public static byte[] GetBESInfo(CardResponse response)
        {
            return Convert.FromBase64String((string)response.Info[CAdESSignatureKeys.SignatureKey]);
        }

        public static SignedData GetSignedData(CardResponse response)
        {
            return new SignedData
            {
                Signature = Convert.FromBase64String((string)response.Info[CAdESSignatureKeys.SignatureKey]),
                SignatureType = (SignatureType)(int)response.Info[CAdESSignatureKeys.SignatureTypeKey],
                SignatureProfile = (SignatureProfile)(int)response.Info[CAdESSignatureKeys.SignatureProfileKey],
            };
        }

        public static SignatureAttributes GetSignatureAttributes(CardResponse response)
        {
            return new SignatureAttributes
            {
                Hash = Convert.FromBase64String((string)response.Info[CAdESSignatureKeys.HashKey]),
                HashOid = (string)response.Info[CAdESSignatureKeys.HashOidKey],
                Certificate = Convert.FromBase64String((string)response.Info[CAdESSignatureKeys.CertificateKey]),
                Signature = Convert.FromBase64String((string)response.Info[CAdESSignatureKeys.SignatureKey]),
                SignedAttributes = Convert.FromBase64String((string)response.Info[CAdESSignatureKeys.SignedAttributesKey]),
            };
        }

        public static void SetValidationInfo(CardRequest request, SignedData signedData)
        {
            request.Info[CAdESSignatureKeys.SignatureKey] = Convert.ToBase64String(signedData.Signature);
            request.Info[CAdESSignatureKeys.EDSActionKey] = (int)EDSAction.Verify;
            request.Info[CAdESSignatureKeys.TargetSignatureTypeKey] = (int)signedData.SignatureType;
            request.Info[CAdESSignatureKeys.TargetSignatureProfileKey] = (int)signedData.SignatureProfile;
        }

        public static void SetToBeSignedInfo(CardRequest request, byte[] certificate, byte[] file, DateTime signingTime, string digestAlgorithmOid, string encryptionAlgorithmOid)
        {
            request.Info[CAdESSignatureKeys.SignatureKey] = Convert.ToBase64String(file);
            request.Info[CAdESSignatureKeys.EDSActionKey] = (int)EDSAction.GetToBeSigned;
            request.Info[CAdESSignatureKeys.CertificateKey] = Convert.ToBase64String(certificate);
            request.Info[CAdESSignatureKeys.SigningTimeKey] = signingTime;
            request.Info[CAdESSignatureKeys.DigestAlgorithmKey] = digestAlgorithmOid;
            request.Info[CAdESSignatureKeys.EncryptionAlgorithmKey] = encryptionAlgorithmOid;
        }

        public static byte[] GetToBeSignedInfo(CardResponse response)
        {
            return Convert.FromBase64String((string)response.Info[CAdESSignatureKeys.SignatureKey]);
        }

        public static IReadOnlyCollection<SignatureValidationInfo> GetValidationInfo(CardResponse response)
        {
            var result = new List<SignatureValidationInfo>();

            var rows = response.Info.TryGet<IList>(CAdESSignatureKeys.SignatureValidationKey);
            if (rows is null)
            {
                return result;
            }

            foreach (var row in rows.Cast<Dictionary<string, object>>())
            {
                var item = new SignatureValidationInfo();
                item.State = (FileSignatureState)row.TryGet<int>(CAdESSignatureKeys.ValidationInfoStateKey);
                item.SigningCertificateValitityDesc = row.TryGet<string>(CAdESSignatureKeys.SigningCertificateValitityDescKey);
                item.SigningCertificate = GetCertificateObject(row.TryGet<Dictionary<string, object>>(CAdESSignatureKeys.SigningCertificateKey));
                item.SigningDate = row.TryGet<DateTime?>(CAdESSignatureKeys.SigningDateKey);
                item.VerifiedSigningDate = row.TryGet<DateTime?>(CAdESSignatureKeys.VerifiedSigningDateKey);

                item.ReachedLevelErrorDesc = row.TryGet<IList>(CAdESSignatureKeys.ReachedLevelErrorDescKey)?.Cast<object>()
                    .Select(x => (x as IList)?.Cast<string>().ToArray() ?? Array.Empty<string>()).ToArray() ?? Array.Empty<string[]>();

                item.TimestampsT = row.TryGet<IList>(CAdESSignatureKeys.TimestampsTKey)?.Cast<Dictionary<string, object>>()
                    .Select(GetTimestampObject).ToArray() ?? Array.Empty<TimestampInfo>();

                item.TimestampsXRefs = row.TryGet<IList>(CAdESSignatureKeys.TimestampsXRefsKey)?.Cast<Dictionary<string, object>>()
                    .Select(GetTimestampObject).ToArray() ?? Array.Empty<TimestampInfo>();

                item.TimestampsXSigAndRefs = row.TryGet<IList>(CAdESSignatureKeys.TimestampsXSigAndRefsKey)?.Cast<Dictionary<string, object>>()
                    .Select(GetTimestampObject).ToArray() ?? Array.Empty<TimestampInfo>();

                item.TimestampsA = row.TryGet<IList>(CAdESSignatureKeys.TimestampsAKey)?.Cast<Dictionary<string, object>>()
                    .Select(GetTimestampObject).ToArray() ?? Array.Empty<TimestampInfo>();

                item.Log = row.TryGet<IList>(CAdESSignatureKeys.LogKey)?.Cast<Dictionary<string, object>>()
                    .Select(GetLogObject).ToArray() ?? Array.Empty<ICAdESLoggerEntry>();

                item.ReachedSignatureType = (SignatureType)row.TryGet<int>(CAdESSignatureKeys.ReachedSignatureTypeKey);
                item.ReachedSignatureProfile = (SignatureProfile)row.TryGet<int>(CAdESSignatureKeys.ReachedSignatureProfileKey);
                item.TargetSignatureType = (SignatureType)row.TryGet<int>(CAdESSignatureKeys.ValidationTargetSignatureTypeKey);
                item.TargetSignatureProfile = (SignatureProfile)row.TryGet<int>(CAdESSignatureKeys.ValidationTargetSignatureProfileKey);
                item.CertsData = GetCertsData(row.TryGet<Dictionary<string, object>>(CAdESSignatureKeys.CertsDataKey));

                result.Add(item);
            }

            return result;
        }

        public static byte[] GetBesSignaturePkcs7Info(CardResponse response)
        {
            return Convert.FromBase64String((string)response.Info[CAdESSignatureKeys.SignatureKey]);
        }

        public static void SetBesSignaturePkcs7Info(CardRequest request, byte[] certificate, byte[] file, DateTime signingTime, byte[] signature, string digestAlgorithmOid, string encryptionAlgorithmOid)
        {
            request.Info[CAdESSignatureKeys.FileKey] = Convert.ToBase64String(file);
            request.Info[CAdESSignatureKeys.EDSActionKey] = (int)EDSAction.GetBesSignature;
            request.Info[CAdESSignatureKeys.CertificateKey] = Convert.ToBase64String(certificate);
            request.Info[CAdESSignatureKeys.SigningTimeKey] = signingTime;
            request.Info[CAdESSignatureKeys.SignatureKey] = Convert.ToBase64String(signature);
            request.Info[CAdESSignatureKeys.DigestAlgorithmKey] = digestAlgorithmOid;
            request.Info[CAdESSignatureKeys.EncryptionAlgorithmKey] = encryptionAlgorithmOid;
        }

        #endregion

        #region Private static

        private static IEDSCertificate GetCertificateObject(Dictionary<string, object> obj)
        {
            return new EDSCertificate(
                null,
                obj.TryGet<string>(CAdESSignatureKeys.CertificateCompanyKey),
                obj.TryGet<string>(CAdESSignatureKeys.CertificateSubjectNameKey),
                obj.TryGet<string>(CAdESSignatureKeys.CertificateIssuerNameKey),
                obj.TryGet<string>(CAdESSignatureKeys.CertificateSerialNumberKey),
                obj.TryGet<DateTime?>(CAdESSignatureKeys.CertificateValidFromKey),
                obj.TryGet<DateTime?>(CAdESSignatureKeys.CertificateValidToKey)
            );
        }

        private static TimestampInfo GetTimestampObject(Dictionary<string, object> obj)
        {
            return new TimestampInfo
            {
                SerialNumber = obj.TryGet<string>(CAdESSignatureKeys.TimestampSerialNumberKey),
                CreationTime = obj.TryGet<DateTime>(CAdESSignatureKeys.TimestampCreationTimeKey),
                IssuerName = obj.TryGet<string>(CAdESSignatureKeys.TimestampIssuerNameKey),
                IssuerSerialNumber = obj.TryGet<string>(CAdESSignatureKeys.TimestampIssueSerialNumberKey),
                Status = obj.TryGet<string>(CAdESSignatureKeys.TimestampStatusKey),
                StatusDescription = obj.TryGet<string>(CAdESSignatureKeys.TimestampStatusDescriptionKey),
                CertStatus = obj.TryGet<string>(CAdESSignatureKeys.TimestampCertStatusKey),
                CertStatusDescription = obj.TryGet<string>(CAdESSignatureKeys.TimestampCertStatusDescriptionKey),
                State = (FileSignatureState)obj.TryGet<int>(CAdESSignatureKeys.ValidationInfoStateKey)
            };
        }

        private static ICAdESLoggerEntry GetLogObject(Dictionary<string, object> obj)
        {
            return new CAdESLoggerEntry((CAdESLogLevel)obj.TryGet<int>(CAdESSignatureKeys.LogLogLevelKey), obj.TryGet<string>(CAdESSignatureKeys.LogMessageKey));
        }

        private static Dictionary<string, CertDataAndVerification> GetCertsData(Dictionary<string, object> obj)
        {
            var result = new Dictionary<string, CertDataAndVerification>();

            foreach (var pair in obj)
            {
                result.Add(pair.Key, GetCertData(pair.Value as Dictionary<string, object>));
            }

            return result;
        }

        private static CertDataAndVerification GetCertData(Dictionary<string, object> obj)
        {
            return new CertDataAndVerification
            {
                Company = obj.TryGet<string>(CAdESSignatureKeys.CertificateCompanyKey),
                SubjectName = obj.TryGet<string>(CAdESSignatureKeys.CertificateSubjectNameKey),
                IssuerName = obj.TryGet<string>(CAdESSignatureKeys.CertificateIssuerNameKey),
                SerialNumber = obj.TryGet<string>(CAdESSignatureKeys.CertificateSerialNumberKey),
                IssuerSerialNumber = obj.TryGet<string>(CAdESSignatureKeys.CertificateIssuerSerialNumberKey),
                ValidFrom = obj.TryGet<DateTime?>(CAdESSignatureKeys.CertificateValidFromKey),
                ValidTo = obj.TryGet<DateTime?>(CAdESSignatureKeys.CertificateValidToKey),
                Data = obj.TryGet<byte[]>(CAdESSignatureKeys.CertDataKey),
                Status = obj.TryGet<string>(CAdESSignatureKeys.CertStatusKey),
                StatusDescription = obj.TryGet<string>(CAdESSignatureKeys.CertStatusDescriptionKey),
                CertificateSourceType = (CertificateSourceType)obj.TryGet<int>(CAdESSignatureKeys.CertificateSourceTypeKey),

                OcspInfos = obj.TryGet<IList>(CAdESSignatureKeys.OcspsKey)?.Cast<Dictionary<string, object>>()
                    .Select(GetOcspObject).ToArray() ?? Array.Empty<OcspInfo>(),

                CrlInfos = obj.TryGet<IList>(CAdESSignatureKeys.CrlsKey)?.Cast<Dictionary<string, object>>()
                    .Select(GetCrlObject).ToArray() ?? Array.Empty<CrlInfo>()
            };
        }

        private static CrlInfo GetCrlObject(Dictionary<string, object> dictionary)
        {
            return new CrlInfo
            {
                NextUpdate = dictionary.TryGet<DateTime?>(CAdESSignatureKeys.CrlNextUpdateKey),
                ThisUpdate = dictionary.TryGet<DateTime?>(CAdESSignatureKeys.CrlThisUpdateKey),
                SigAlgName = dictionary.TryGet<string>(CAdESSignatureKeys.CrlSigAlgNameKey),
                SigAlgOid = dictionary.TryGet<string>(CAdESSignatureKeys.CrlSigAlgOidKey),
                IssuerDn = dictionary.TryGet<string>(CAdESSignatureKeys.CrlIssuerDnKey),
                IssuerSerialNumber = dictionary.TryGet<string>(CAdESSignatureKeys.CrlIssuerSerialNumberKey),
                Data = dictionary.TryGet<byte[]>(CAdESSignatureKeys.CrlDataKey),
                Status = dictionary.TryGet<string>(CAdESSignatureKeys.CrlStatusKey)
            };
        }

        private static OcspInfo GetOcspObject(Dictionary<string, object> dictionary)
        {
            return new OcspInfo
            {
                ProducedAt = dictionary.TryGet<DateTime?>(CAdESSignatureKeys.OcspProducedAtKey),
                SigAlgName = dictionary.TryGet<string>(CAdESSignatureKeys.OcspSigAlgNameKey),
                SigAlgOid = dictionary.TryGet<string>(CAdESSignatureKeys.OcspSigAlgOidKey),
                SubjectDn = dictionary.TryGet<string>(CAdESSignatureKeys.OcspSubjectDnKey),
                SignerSerialNumber = dictionary.TryGet<string>(CAdESSignatureKeys.OcspSubjectSerialNumberKey),
                Status = dictionary.TryGet<string>(CAdESSignatureKeys.OcspStatusKey)
            };
        }

        #endregion
    }
}
