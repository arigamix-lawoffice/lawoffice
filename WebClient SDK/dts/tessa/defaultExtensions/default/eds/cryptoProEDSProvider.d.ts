import { EDSProvider, EncryptAndDigest } from 'tessa/cards';
import { CertData } from 'tessa/files';
export declare class CryptoProEDSProvider extends EDSProvider {
    isAvailable(showDialogError?: boolean): Promise<boolean>;
    getPluginVersion(): Promise<string>;
    getCerts(): Promise<CertData[]>;
    signFile(certificate: CertData, fileBase64Str: string, encryptAndDigestList: EncryptAndDigest[]): Promise<string>;
    checkSign(signature: string, fileBase64Str?: string | undefined): Promise<{
        valid: boolean;
        error: string;
    }>;
}
declare global {
    interface Window {
        cadesplugin: any;
    }
}
