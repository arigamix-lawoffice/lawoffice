import { IRegistryItem } from 'tessa/platform';
import { CertData, FileSignatureState } from 'tessa/files';
export interface IEDSProvider {
    isAvailable(showDialogError?: boolean): Promise<boolean>;
    getCerts(): Promise<CertData[]>;
    signFile(certificate: CertData, fileBase64Str: string, encryptAndDigestList: EncryptAndDigest[]): Promise<string>;
    checkSign(signature: string, fileBase64Str?: string): Promise<{
        valid: boolean;
        error: string;
    }>;
}
export interface EncryptAndDigest {
    encryptOid: string;
    digestOid: string;
}
export interface ICAdESProvider extends IEDSProvider {
    extendSignature(signature: string, certificate: string): Promise<ISignedData>;
    checkExtendedSignature(signature: string, signatureType: SignatureType, signatureProfile: SignatureProfile): Promise<ISignatureValidationInfo[]>;
    getBesSignatureFromSignature(signature: string): Promise<string>;
    getToBeSignedDocument(certificate: string, file: string, signingTime: string): Promise<string>;
    makeBesSignaturePkcs7Async(certificate: string, file: string, signingTime: string, signature: string): Promise<string>;
    getAttributesFromSignature(signature: string): Promise<ISignatureAttributes>;
}
export declare abstract class EDSProvider implements ICAdESProvider, IRegistryItem {
    id: string;
    isAvailable(_showDialogError?: boolean): Promise<boolean>;
    getCerts(): Promise<CertData[]>;
    signFile(_certificate: CertData, _fileBase64Str: string, _encryptAndDigestList: EncryptAndDigest[]): Promise<string>;
    checkSign(_signature: string, _fileBase64Str?: string | undefined): Promise<{
        valid: boolean;
        error: string;
    }>;
    extendSignature(signature: string, certificate: string): Promise<ISignedData>;
    checkExtendedSignature(signature: string, signatureType: SignatureType, signatureProfile: SignatureProfile): Promise<ISignatureValidationInfo[]>;
    getBesSignatureFromSignature(signature: string): Promise<string>;
    getToBeSignedDocument(certificate: string, file: string, signingTime: string): Promise<string>;
    makeBesSignaturePkcs7Async(certificate: string, file: string, signingTime: string, signature: string): Promise<string>;
    getAttributesFromSignature(signature: string): Promise<ISignatureAttributes>;
}
export declare const EDSProviderID = "EDSProvider";
export declare enum EDSAction {
    None = 0,
    Sign = 1,
    Verify = 2,
    GetBESFromExtended = 3,
    GetBesSignature = 4,
    GetToBeSigned = 5,
    GetSignatureAttributesFromSignature = 6
}
export declare enum SignatureProfile {
    None = 0,
    BES = 1,
    EPES = 2,
    T = 3,
    C = 4,
    XL = 5,
    XType1 = 6,
    XType2 = 7,
    XLType1 = 8,
    XLType2 = 9,
    A = 10
}
export declare enum SignatureType {
    None = 0,
    CAdES = 1
}
export interface ISignedData {
    signature: string;
    type: SignatureType;
    profile: SignatureProfile;
}
export interface ISignatureAttributes {
    certificate: string;
    hashOid: string;
    hash: string;
    signature: string;
    signedAttributes: string;
}
export interface ISignatureValidationInfo {
    integrity: FileSignatureState;
    state: FileSignatureState;
    signingCertificateValidityDesc: string;
    signingCertificate: Partial<CertData>;
    signingDate: string;
    verifiedSigningDate: string;
    reachedLevelErrorDesc: string[][];
    reachedSignatureType: SignatureType;
    reachedSignatureProfile: SignatureProfile;
    targetSignatureProfile: SignatureProfile;
    targetSignatureType: SignatureType;
    log: ICAdESLoggerEntry[];
    certsData: {
        [key: string]: ICertDataAndVerification;
    };
    timestampsT: ITimestamp[];
    timestampsXRefs: ITimestamp[];
    timestampsXSigAndRefs: ITimestamp[];
    timestampsA: ITimestamp[];
}
export interface ICertDataAndVerification extends CertData {
    data: string;
    status: string;
    statusDescription: string;
    certificateSource: CertificateSourceType;
    ocsps: OcspInfo[];
    crls: CrlInfo[];
    issuerSerialNumber: string;
}
export interface ITimestamp {
    serialNumber: string;
    creationTime: string;
    issuerName: string;
    status: string;
    statusDescription: string;
    certStatus: string;
    certStatusDescription: string;
    state: FileSignatureState;
    issuerSerialNumber: string;
}
export interface ICAdESLoggerEntry {
    logLevel: LogLevel;
    message: string;
}
export declare enum LogLevel {
    Info = 1,
    Error = 2,
    Warn = 3
}
export declare const logLevelText: (level: LogLevel) => "" | "$UI_Common_Dialog_Information" | "$UI_Common_Dialog_Error" | "$ValidationResultDialog_Header_Warning";
export declare class SignatureValidationInfo implements ISignatureValidationInfo {
    integrity: FileSignatureState;
    state: FileSignatureState;
    signingCertificateValidityDesc: string;
    signingCertificate: Partial<CertData>;
    signingDate: string;
    verifiedSigningDate: string;
    reachedLevelErrorDesc: never[];
    reachedSignatureType: SignatureType;
    reachedSignatureProfile: SignatureProfile;
    targetSignatureProfile: SignatureProfile;
    targetSignatureType: SignatureType;
    log: ICAdESLoggerEntry[];
    certsData: {};
    timestampsT: ITimestamp[];
    timestampsXRefs: ITimestamp[];
    timestampsXSigAndRefs: ITimestamp[];
    timestampsA: ITimestamp[];
}
export declare enum CertificateSourceType {
    TRUST_STORE = 0,
    TRUSTED_LIST = 1,
    SIGNATURE = 2
}
export declare const getCertificateSourceText: (value: CertificateSourceType | undefined) => "" | "$UI_Signatures_Trust_List" | "$UI_Signatures_Signature";
export interface OcspInfo {
    subjectDN: string;
    subjectSerialNumber: string;
    status: string;
    producedAt?: string;
    sigAlgName: string;
    sigAlgOid: string;
}
export interface CrlInfo {
    issuerDN: string;
    issuerSerialNumber: string;
    nextUpdate?: string;
    thisUpdate?: string;
    sigAlgName: string;
    sigAlgOid: string;
    status: string;
    data: string;
}
export declare const getPOJO: (obj: object) => any;
