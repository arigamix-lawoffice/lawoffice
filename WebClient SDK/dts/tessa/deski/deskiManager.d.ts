import { ValidationResult } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
export declare type DeskiFileInfo = {
    Name: string;
    Path: string;
    Modified: string;
    IsModified: boolean;
    IsLocked: boolean;
    Size: number;
    ID: string;
    Editable: boolean;
    App: string;
};
export declare type MasterKey = {
    Key: string;
    ExpiresAt: string;
};
export interface CipherInfo {
    Info: IStorage | null;
    LocalExpiryDates: string[];
    LocalPrivateKeys: string[];
}
export declare class DeskiManager {
    private constructor();
    private static _instance;
    static get instance(): DeskiManager;
    private _masterKeys;
    private _deskiEnabled;
    private _deskiPort;
    private _deskiInfo;
    private _deskiVersionFromServer;
    private _lockDeskiFind;
    get masterKeys(): ReadonlyArray<MasterKey>;
    get deskiEnabled(): boolean;
    get deskiPort(): number | null;
    get deskiAvailable(): boolean;
    get deskiInfo(): DeskiInfo | null;
    get deskiVersion(): string | null;
    get deskiVersionFromServer(): string | null;
    get lockDeskiFind(): boolean;
    init(enabled: boolean, versionFromServer: string | null): void;
    getDeskiUrl(deskiPort?: number): string;
    findDeski(delayTimeout?: boolean): Promise<{
        port?: number;
        info?: DeskiInfo;
    }>;
    checkDeski(silent?: boolean): Promise<boolean>;
    updateMasterKeys(cipher?: CipherInfo): Promise<ValidationResult>;
    private getRequestOptions;
    setAppInfo(appUrl: string, keys: ReadonlyArray<MasterKey>): DeskiSuccessResponse;
    getContentInfo(appUrl: string, id: string): Promise<{
        isCached: boolean;
        result?: ValidationResult;
    }>;
    cacheContent(appUrl: string, id: string, name: string, content: File): DeskiSuccessResponse;
    openFile(appUrl: string, id: string, mode: 'file' | 'folder', editable: boolean): DeskiSuccessResponse;
    getFileInfo(appUrl: string, id: string, editable: boolean): Promise<{
        info?: DeskiFileInfo;
        result?: ValidationResult;
    }>;
    getFileData(appUrl: string, id: string): Promise<{
        data?: Blob;
        result?: ValidationResult;
    }>;
    cacheModFileWithNewId(appUrl: string, id: string, newId: string): DeskiSuccessResponse;
    removeFile(appUrl: string, id: string, editable: boolean, sendSynchronously?: boolean): DeskiSuccessResponse;
    getContent(appUrl: string, id: string): Promise<{
        data?: Blob;
        result?: ValidationResult;
    }>;
    getOpenFiles(appUrl: string): Promise<{
        files?: Array<DeskiFileInfo>;
        result?: ValidationResult;
    }>;
    compareFiles(appUrl: string, source: FileProcessingInfo, otherFiles: FileProcessingInfo[]): DeskiSuccessResponse;
    mergeFiles(appUrl: string, source: FileProcessingInfo, otherFiles: FileProcessingInfo[]): DeskiSuccessResponse;
    copyToClipboard(appUrl: string, files: FileProcessingInfo[]): DeskiSuccessResponse;
    pasteFromClipboard(appUrl: string): Promise<{
        files: File[];
        result?: ValidationResult;
    }>;
}
export declare type DeskiSuccessResponse = Promise<{
    success: true;
} | {
    success: false;
    result: ValidationResult;
}>;
export interface FileProcessingInfo {
    id: string;
    author: string;
}
export interface DeskiInfo {
    FullName: string;
    ShortName: string;
    VerMajor: number;
    VerMinor: number;
    Patch?: number;
    OS: 'aix' | 'android' | 'darwin' | 'dragonfly' | 'freebsd' | 'illumos' | 'js' | 'linux' | 'netbsd' | 'openbsd' | 'plan9' | 'solaris' | 'windows';
}
