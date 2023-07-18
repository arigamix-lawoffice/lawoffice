import { CertData } from 'tessa/files';
export declare class FileListSelectCertDialogViewModel {
    constructor(certs: CertData[], noCommentDialog: boolean);
    readonly certs: ReadonlyArray<CertData>;
    readonly noCommentDialog: boolean;
    selectedCertIndex: number;
    get selectedCert(): CertData | null;
    setSelectedCert(index: number): void;
}
