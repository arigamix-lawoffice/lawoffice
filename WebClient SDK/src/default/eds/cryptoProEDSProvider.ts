import { EDSProvider, EncryptAndDigest } from 'tessa/cards';
import { arrayBufferToBase64, CertData } from 'tessa/files';
import { showDialogCryptoProUnavailable } from 'ui/dialog/dialogCryptoProUnavailable';
import Platform from 'common/platform';

export class CryptoProEDSProvider extends EDSProvider {
  async isAvailable(showDialogError?: boolean): Promise<boolean> {
    if (!window.cadesplugin || Platform.isMobile()) {
      return false;
    }

    try {
      await window.cadesplugin;
      return true;
    } catch (error) {
      if (showDialogError) {
        await showDialogCryptoProUnavailable();
      }
      return false;
    }
  }

  getPluginVersion(): Promise<string> {
    const CreateObject = window.cadesplugin.CreateObjectAsync || window.cadesplugin.CreateObject;

    return new Promise((resolve, reject) => {
      window.cadesplugin.async_spawn(
        function* (args) {
          const ProviderName = 'Crypto-Pro GOST R 34.10-2012 Cryptographic Service Provider';
          const ProviderType = 80;
          try {
            const oAbout = yield CreateObject('CAdESCOM.About');
            const oVersion = yield oAbout.CSPVersion(ProviderName, ProviderType);
            const version = yield oVersion.toString();
            args[0](version);
          } catch (error) {
            const err = window.cadesplugin.getLastError(error);
            if (err.indexOf('0x80090019') + 1) {
              console.log('Указанный CSP не установлен');
            } else {
              console.log(err);
            }
            args[1](err);
          }
        },
        resolve,
        reject
      );
    });
  }
  getCerts(): Promise<CertData[]> {
    const CreateObject = window.cadesplugin.CreateObjectAsync || window.cadesplugin.CreateObject;

    return new Promise((resolve, reject) => {
      window.cadesplugin.async_spawn(
        function* (args) {
          try {
            const oStore = yield CreateObject('CAdESCOM.Store');
            yield oStore.Open(
              window.cadesplugin.CAPICOM_CURRENT_USER_STORE,
              window.cadesplugin.CAPICOM_MY_STORE,
              window.cadesplugin.CAPICOM_STORE_OPEN_MAXIMUM_ALLOWED
            );

            const CertificatesObj = yield oStore.Certificates;
            const Count = yield CertificatesObj.Count;
            const certs: CertData[] = [];
            for (let i = 1; i <= Count; i++) {
              const cert = yield CertificatesObj.Item(i);
              const subjectName = yield cert.SubjectName;
              const issuerName = yield cert.IssuerName;
              const validFrom = yield cert.ValidFromDate;
              const validTo = yield cert.ValidToDate;
              const serialNumber = yield cert.SerialNumber;
              const thumbprint = yield cert.Thumbprint;
              const certificateStr = yield cert.Export(window.cadesplugin.CADESCOM_ENCODE_BASE64);
              certs.push({
                subjectName: getName(subjectName) as string,
                issuerName: getName(issuerName) as string,
                validFrom,
                validTo,
                serialNumber,
                company: getOwner(subjectName) as string,
                certificateStr,
                thumbprint
              });
            }

            yield oStore.Close();

            args[0](certs);
          } catch (err) {
            args[1](window.cadesplugin.getLastError(err));
          }
        },
        resolve,
        reject
      );
    });
  }
  signFile(
    certificate: CertData,
    fileBase64Str: string,
    encryptAndDigestList: EncryptAndDigest[]
  ): Promise<string> {
    const CreateObject = window.cadesplugin.CreateObjectAsync || window.cadesplugin.CreateObject;

    return new Promise((resolve, reject) => {
      window.cadesplugin.async_spawn(
        function* (args) {
          try {
            // certificate
            const oStore = yield CreateObject('CAdESCOM.Store');
            yield oStore.Open(
              window.cadesplugin.CAPICOM_CURRENT_USER_STORE,
              window.cadesplugin.CAPICOM_MY_STORE,
              window.cadesplugin.CAPICOM_STORE_OPEN_MAXIMUM_ALLOWED
            );

            const CertificatesObj = yield oStore.Certificates;

            let oCertificates;

            if (certificate.thumbprint) {
              oCertificates = yield CertificatesObj.Find(
                window.cadesplugin.CAPICOM_CERTIFICATE_FIND_SHA1_HASH,
                certificate.thumbprint
              );
            }

            if ((!oCertificates || (yield oCertificates.Count) === 0) && certificate.subjectName) {
              oCertificates = yield CertificatesObj.Find(
                window.cadesplugin.CAPICOM_CERTIFICATE_FIND_SUBJECT_NAME,
                certificate.subjectName
              );
            }

            const Count = yield oCertificates.Count;
            if (Count === 0) {
              throw "Certificate isn't found: " + args[0];
            }
            const oCertificate = yield oCertificates.Item(1);

            // hashAlgo
            const publicKey = yield oCertificate.PublicKey();
            const keyAlgo = yield publicKey.Algorithm;
            const publicOid = yield keyAlgo.Value;
            const hashAlgoOid = getHashByPublicOid(publicOid, encryptAndDigestList);
            const hashAlgo = getHashAlgoByOid(hashAlgoOid);

            // hashObject
            const oHashedData = yield CreateObject('CAdESCOM.HashedData');
            yield oHashedData.propset_Algorithm(hashAlgo);
            yield oHashedData.propset_DataEncoding(window.cadesplugin.CADESCOM_BASE64_TO_BINARY);
            yield oHashedData.Hash(fileBase64Str);

            // rawSignature
            const oRawSignature = yield CreateObject('CAdESCOM.RawSignature');
            const sRawSignature = yield oRawSignature.SignHash(oHashedData, oCertificate);

            const sSignedMessage = arrayBufferToBase64(toByteArray(sRawSignature).reverse());

            yield oStore.Close();

            args[2](sSignedMessage);
          } catch (err) {
            args[3](window.cadesplugin.getLastError(err) || err);
          }
        },
        certificate.subjectName,
        fileBase64Str,
        resolve,
        reject
      );
    });
  }
  async checkSign(
    signature: string,
    fileBase64Str?: string | undefined
  ): Promise<{ valid: boolean; error: string }> {
    const CreateObject = window.cadesplugin.CreateObjectAsync || window.cadesplugin.CreateObject;

    const signatureAttributes = await this.getAttributesFromSignature(signature);
    const hashAlgo = getHashAlgoByOid(signatureAttributes.hashOid);
    const sRawSignature = toHexString(
      atob(signatureAttributes.signature)
        .split('')
        .map(x => x.charCodeAt(0))
        .reverse()
    ).toUpperCase();
    const sRawFileHash = toHexString(
      atob(signatureAttributes.hash)
        .split('')
        .map(x => x.charCodeAt(0))
      // .reverse()
    ).toUpperCase();

    return new Promise((resolve, reject) => {
      window.cadesplugin.async_spawn(
        function* (args) {
          try {
            // hash
            const oHashedData = yield CreateObject('CAdESCOM.HashedData');
            yield oHashedData.propset_Algorithm(hashAlgo);
            yield oHashedData.propset_DataEncoding(window.cadesplugin.CADESCOM_BASE64_TO_BINARY);
            yield oHashedData.Hash(fileBase64Str);
            const hashValue = yield oHashedData.Value;
            if (hashValue !== sRawFileHash) {
              args[2]({ valid: false, error: '$UI_Signature_MessageDigestInvalid' });
              return;
            }

            // certificate
            const oCertificate = yield CreateObject('CAdESCOM.Certificate');
            yield oCertificate.Import(signatureAttributes.certificate);

            // hash
            const oSignAttrsHashedData = yield CreateObject('CAdESCOM.HashedData');
            yield oSignAttrsHashedData.propset_Algorithm(hashAlgo);
            yield oSignAttrsHashedData.propset_DataEncoding(
              window.cadesplugin.CADESCOM_BASE64_TO_BINARY
            );
            yield oSignAttrsHashedData.Hash(signatureAttributes.signedAttributes);

            // verify
            const oRawSignature = yield CreateObject('CAdESCOM.RawSignature');
            yield oRawSignature.VerifyHash(oSignAttrsHashedData, oCertificate, sRawSignature);
          } catch (err) {
            try {
              // Возможно это подпись из эры до 3.4 - даем второй шанс
              let oSignedData = yield CreateObject('CAdESCOM.CadesSignedData');
              yield oSignedData.propset_ContentEncoding(
                window.cadesplugin.CADESCOM_BASE64_TO_BINARY
              );
              yield oSignedData.propset_Content(fileBase64Str);
              yield oSignedData.VerifyCades(signature, window.cadesplugin.CADESCOM_CADES_BES, true);
            } catch {
              args[2]({ valid: false, error: window.cadesplugin.getLastError(err) || err });
              return;
            }
          }

          args[2]({ valid: true });
        },
        signature,
        fileBase64Str,
        resolve,
        reject
      );
    });
  }
}

declare global {
  interface Window {
    cadesplugin: any;
  }
}

function getName(str: string | undefined) {
  return getParameter(str, 'CN');
}

function getOwner(str: string | undefined) {
  return getParameter(str, 'O');
}

function getParameter(str: string | undefined, marker: string) {
  if (!str) {
    return undefined;
  }

  const strArr = str.split(/=|,|\+/).map(x => x.trim());
  if (strArr.length === 1) {
    return undefined;
  }

  const snIndex = strArr.indexOf(marker);
  if (snIndex === -1 || snIndex === strArr.length) {
    return undefined;
  }

  return strArr[snIndex + 1];
}

const getHashByPublicOid = (keyAlgo: string, encryptAndDigestList: EncryptAndDigest[]) => {
  const pair = encryptAndDigestList.find(x => x.encryptOid === keyAlgo);
  if (pair) {
    return pair.digestOid;
  } else {
    return encryptAndDigestList.find(x => !x.encryptOid)?.digestOid || '';
  }
};

const getHashAlgoByOid = (hashOid: string) => {
  switch (hashOid) {
    case '1.2.643.2.2.9':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_CP_GOST_3411;
    case '1.2.643.7.1.1.2.2':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_256;
    case '1.2.643.7.1.1.4.1':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_256_HMAC;
    case '1.2.643.7.1.1.2.3':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_512;
    case '1.2.643.7.1.1.4.2':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_CP_GOST_3411_2012_512_HMAC;
    case '1.2.643.2.2.10':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_CP_GOST_3411_HMAC;
    case '1.2.840.113549.2.2':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_MD2;
    case '1.2.840.113549.2.4':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_MD4;
    case '1.2.840.113549.2.5':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_MD5;
    case '2.16.840.1.101.3.4.2.1':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_SHA_256;
    case '2.16.840.1.101.3.4.2.2':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_SHA_384;
    case '2.16.840.1.101.3.4.2.3':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_SHA_512;
    case '1.3.14.3.2.26':
      return window.cadesplugin.CADESCOM_HASH_ALGORITHM_SHA1;
    default:
      throw new Error(`Unsupported digest algorithm: (${hashOid})`);
  }
};

const toByteArray = (hexString: string) => {
  const result: number[] = [];
  for (let i = 0; i < hexString.length; i += 2) {
    result.push(parseInt(hexString.substr(i, 2), 16));
  }
  return result;
};
const toHexString = (byteArray: number[]) => {
  return Array.prototype.map
    .call(byteArray, function (byte) {
      return ('0' + (byte & 0xff).toString(16)).slice(-2);
    })
    .join('');
};
