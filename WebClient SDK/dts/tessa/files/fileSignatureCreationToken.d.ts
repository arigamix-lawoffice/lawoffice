import { FileSignatureEventType } from './fileSignatureEventType';
import { FileSignatureState } from './fileSignatureState';
import { SignatureType, SignatureProfile } from 'tessa/cards';
export interface IFileSignatureCreationToken {
    id: guid | null;
    userId: guid;
    userName: string;
    eventType: FileSignatureEventType;
    comment: string;
    subjectName: string;
    company: string;
    signed: string | null;
    serialNumber: string;
    issuerName: string;
    state: FileSignatureState;
    errorText: string;
    data: string;
    signatureType: SignatureType;
    signatureProfile: SignatureProfile;
}
export declare class FileSignatureCreationToken implements IFileSignatureCreationToken {
    constructor();
    id: guid | null;
    userId: guid;
    userName: string;
    eventType: FileSignatureEventType;
    comment: string;
    subjectName: string;
    company: string;
    signed: string | null;
    serialNumber: string;
    issuerName: string;
    state: FileSignatureState;
    errorText: string;
    data: string;
    signatureType: SignatureType;
    signatureProfile: SignatureProfile;
}
