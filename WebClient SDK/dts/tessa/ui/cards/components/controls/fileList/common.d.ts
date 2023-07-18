import { SignatureType, SignatureProfile } from 'tessa/cards';
import { FileSignatureState } from 'tessa/files';
export declare const getSignatureTypeText: (type: SignatureType) => string;
export declare const getSignatureProfileText: (profile: SignatureProfile) => string;
export declare const getSignatureIntegrityText: (state: FileSignatureState) => string;
