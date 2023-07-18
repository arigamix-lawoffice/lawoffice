using System;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Shared.EDS;
using Tessa.Platform.EDS;

// ReSharper disable InconsistentNaming

namespace Tessa.Extensions.Default.Client.EDS
{
    public class CryptoProEDSManager : CAdESManager
    {
        #region algo constants

        ///<summary>
        ///  	Алгоритм ГОСТ Р 34.11-94.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_CP_GOST_3411 = 100;

        ///<summary>
        ///  	Алгоритм ГОСТ Р 34.11-2012.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_256 = 101;

        ///<summary>
        ///  	Алгоритм ГОСТ Р 34.11-2012 HMAC.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_256_HMAC = 111;

        ///<summary>
        ///  	Алгоритм ГОСТ Р 34.11-2012.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_512 = 102;

        ///<summary>
        ///  	Алгоритм ГОСТ Р 34.11-2012 HMAC.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_512_HMAC = 112;

        ///<summary>
        ///  	Алгоритм ГОСТ Р 34.11-94 HMAC.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_CP_GOST_3411_HMAC = 110;

        ///<summary>
        ///  	Алгоритм MD2.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_MD2 = 1;

        ///<summary>
        ///  	Алгоритм MD4.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_MD4 = 2;

        ///<summary>
        ///  	Алгоритм MD5.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_MD5 = 3;

        ///<summary>
        ///  	Алгоритм SHA1 с длиной ключа 256 бит.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_SHA_256 = 4;

        ///<summary>
        ///  	Алгоритм SHA1 с длиной ключа 384 бита.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_SHA_384 = 5;

        ///<summary>
        ///  	Алгоритм SHA1 с длиной ключа 512 бит.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_SHA_512 = 6;

        ///<summary>
        ///  	Алгоритм SHA1.
        ///</summary>
        private const int CADESCOM_HASH_ALGORITHM_SHA1 = 0;

        #endregion

        private const int CAPICOM_CURRENT_USER_STORE = 2;

        private const int CADESCOM_BASE64_TO_BINARY = 1;

        private const int CADESCOM_CADES_BES = 1;

        public CryptoProEDSManager(ICardRepository cardRepository, ICardCache cardCache)
            : base(cardRepository, cardCache)
        {
        }

        /// <inheritdoc />
        public override async ValueTask<byte[]> GenerateSignatureAsync(
            byte[] certificate,
            ISignatureFile file,
            string hashAlgoOid,
            CancellationToken cancellationToken = default)
        {
            var certBC = new X509Certificate2(certificate);

            var hashAlgo = GetHashAlgoByOid(hashAlgoOid);

            var oHashedData = GetHashObject(await file.GetBytesAsync(cancellationToken).ConfigureAwait(false), hashAlgo);

            var oCertificate = GetSignerCertificate(certBC.SerialNumber);

            var oRawSignature = ComHelper.GetComObj("CAdESCOM.RawSignature");

            var sRawSignature = (string)oRawSignature.Invoke("SignHash", oHashedData, oCertificate);
            var bytes = GetBytesFromHex(sRawSignature);
            Array.Reverse(bytes);

            return bytes;
        }

        /// <inheritdoc />
        public override async ValueTask<(bool Success, string ErrorText)> VerifySignatureAsync(
            byte[] encodedSignature,
            ISignatureFile file,
            CancellationToken cancellationToken = default)
        {
            var errorText = string.Empty;

            var signatureAttributes = await GetAttributesFromSignatureAsync(encodedSignature, cancellationToken).ConfigureAwait(false);
            var hashAlgo = GetHashAlgoByOid(signatureAttributes.HashOid);
            var sigbytes = signatureAttributes.Signature.ToArray();
            Array.Reverse(sigbytes);
            var sRawSignature = BitConverter.ToString(sigbytes).Replace("-", string.Empty, StringComparison.Ordinal);
            var hashBytes = signatureAttributes.Hash.ToArray();
            var sRawFileHash = BitConverter.ToString(hashBytes).Replace("-", string.Empty, StringComparison.Ordinal);
            var fileBytes = await file.GetBytesAsync(cancellationToken).ConfigureAwait(false);

            try
            {
                var hashHashData = GetHashObject(fileBytes, hashAlgo);
                if ((string)hashHashData.Get("Value") != sRawFileHash)
                {
                    throw new ArgumentException("$UI_Signature_MessageDigestInvalid");
                }

                var oCertificate = ComHelper.GetComObj("CAdESCOM.Certificate");
                oCertificate.Invoke("Import", Convert.ToBase64String(signatureAttributes.Certificate));

                var oHashedData = GetHashObject(signatureAttributes.SignedAttributes, hashAlgo);

                var oRawSignature = ComHelper.GetComObj("CAdESCOM.RawSignature");

                oRawSignature.Invoke("VerifyHash", oHashedData, oCertificate, sRawSignature);
            }
            catch (TargetInvocationException ex)
            {
                errorText = ex.Message;
                var innerMessage = ex.InnerException?.Message;
                if (!string.IsNullOrEmpty(innerMessage))
                {
                    errorText = $"{ex.Message}: {innerMessage}";
                }
                try
                {
                    // Возможно это подпись из эры до 3.4 - даем второй шанс
                    VerifySignatureAlla33(fileBytes, encodedSignature);
                    errorText = string.Empty;
                }
                catch
                {
                    return (false, errorText);
                }                
            }

            return (true, errorText);
        }

        private static object GetSignerCertificate(string serialNumber)
        {
            object oStore = ComHelper.GetComObj("CAdESCOM.Store");

            oStore.Invoke("Open", CAPICOM_CURRENT_USER_STORE);

            var certificates = oStore.Get("Certificates");
            var count = (int)(certificates.Get("Count"));
            for (int i = 1; i <= count; i++)
            {
                var cert = certificates.Get("Item", i);
                var certSerial = (string)cert.Get("SerialNumber");
                if (certSerial.Equals(serialNumber, StringComparison.OrdinalIgnoreCase))
                {
                    return cert;
                }
            }
            return null;
        }

        private static int GetHashAlgoByOid(string hashOid) =>
            hashOid switch
            {
                "1.2.643.2.2.9" => CADESCOM_HASH_ALGORITHM_CP_GOST_3411,
                "1.2.643.7.1.1.2.2" => CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_256,
                "1.2.643.7.1.1.4.1" => CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_256_HMAC,
                "1.2.643.7.1.1.2.3" => CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_512,
                "1.2.643.7.1.1.4.2" => CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_512_HMAC,
                "1.2.643.2.2.10" => CADESCOM_HASH_ALGORITHM_CP_GOST_3411_HMAC,
                "1.2.840.113549.2.2" => CADESCOM_HASH_ALGORITHM_MD2,
                "1.2.840.113549.2.4" => CADESCOM_HASH_ALGORITHM_MD4,
                "1.2.840.113549.2.5" => CADESCOM_HASH_ALGORITHM_MD5,
                "2.16.840.1.101.3.4.2.1" => CADESCOM_HASH_ALGORITHM_SHA_256,
                "2.16.840.1.101.3.4.2.2" => CADESCOM_HASH_ALGORITHM_SHA_384,
                "2.16.840.1.101.3.4.2.3" => CADESCOM_HASH_ALGORITHM_SHA_512,
                "1.3.14.3.2.26" => CADESCOM_HASH_ALGORITHM_SHA1,
                _ => throw new ArgumentException("Unsupported digest algorithm")
            };

        private static object GetHashObject(byte[] file, int hashAlgo)
        {
            var dataInBase64 = Convert.ToBase64String(file);

            var oHashedData = ComHelper.GetComObj("CAdESCOM.HashedData");
            oHashedData.Set("Algorithm", hashAlgo);
            oHashedData.Set("DataEncoding", CADESCOM_BASE64_TO_BINARY);
            oHashedData.Invoke("Hash", dataInBase64);

            // Получаем хэш-значение
            //var sHashValue = (string)oHashedData.Get("Value");

            return oHashedData;
        }

        private static byte[] GetBytesFromHex(string hex)
        {
            int NumberChars = hex.Length;
            byte[] bytes = new byte[NumberChars / 2];
            for (int i = 0; i < NumberChars; i += 2)
                bytes[i / 2] = Convert.ToByte(hex.Substring(i, 2), 16);
            return bytes;
        }

        private static void VerifySignatureAlla33(byte[] file, byte[] signature)
        {
            var fileInBase64 = Convert.ToBase64String(file);
            var signatureInBase64 = Convert.ToBase64String(signature);

            var oSignedData = ComHelper.GetComObj("CAdESCOM.CadesSignedData");
            oSignedData.Set("ContentEncoding", CADESCOM_BASE64_TO_BINARY);
            oSignedData.Set("Content", fileInBase64);


            oSignedData.Invoke("VerifyCades", signatureInBase64, CADESCOM_CADES_BES, true);
        }
    }
}
