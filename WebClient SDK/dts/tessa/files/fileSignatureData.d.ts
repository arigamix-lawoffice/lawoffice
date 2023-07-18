import { FileSignatureDataState } from './fileSignatureDataState';
export interface IFileSignatureData {
    readonly isEmpty: boolean;
    state: FileSignatureDataState;
    getBytes(): string;
}
export declare class FileSignatureData implements IFileSignatureData {
    constructor(data?: string | null, state?: FileSignatureDataState);
    private data;
    get isEmpty(): boolean;
    state: FileSignatureDataState;
    getBytes(): string;
}
