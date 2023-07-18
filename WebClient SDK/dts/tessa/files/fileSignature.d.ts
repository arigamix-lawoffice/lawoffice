import { FileSignatureEventType } from './fileSignatureEventType';
import { FileSignatureState } from './fileSignatureState';
import { IFileVersion } from './fileVersion';
import { IFileSignatureData } from './fileSignatureData';
import { FileEntity, IFileEntity } from './fileEntity';
import { SignatureType, SignatureProfile, ISignatureValidationInfo } from 'tessa/cards';
export interface IFileSignature extends IFileEntity {
    readonly userId: guid;
    readonly userName: string;
    readonly eventType: FileSignatureEventType;
    readonly comment: string;
    readonly subjectName: string;
    readonly company: string;
    readonly signed: string;
    readonly serialNumber: string;
    readonly issuerName: string;
    readonly signatureType: SignatureType;
    readonly signatureProfile: SignatureProfile;
    state: FileSignatureState;
    errorText: string;
    extendedValidationInfos: ISignatureValidationInfo[];
    data: IFileSignatureData;
    readonly version: IFileVersion;
    updateState(state: FileSignatureState, validationInfo: ISignatureValidationInfo[], errorText: string): void;
    clone(fileVersion: IFileVersion): IFileSignature;
}
export declare class FileSignature extends FileEntity implements IFileSignature {
    constructor(id: guid, userId: guid, userName: string, eventType: FileSignatureEventType, comment: string, subjectName: string, company: string, signed: string, serialNumber: string, issuerName: string, state: FileSignatureState, errorText: string, data: IFileSignatureData, version: IFileVersion, signatureType: SignatureType, signatureProfile: SignatureProfile);
    readonly userId: guid;
    readonly userName: string;
    readonly eventType: FileSignatureEventType;
    readonly comment: string;
    readonly subjectName: string;
    readonly company: string;
    readonly signed: string;
    readonly serialNumber: string;
    readonly issuerName: string;
    readonly signatureType: SignatureType;
    readonly signatureProfile: SignatureProfile;
    state: FileSignatureState;
    errorText: string;
    extendedValidationInfos: ISignatureValidationInfo[];
    data: IFileSignatureData;
    readonly version: IFileVersion;
    updateState(state: FileSignatureState, validationInfos: ISignatureValidationInfo[], errorText: string): void;
    clone(fileVersion: IFileVersion): IFileSignature;
}
