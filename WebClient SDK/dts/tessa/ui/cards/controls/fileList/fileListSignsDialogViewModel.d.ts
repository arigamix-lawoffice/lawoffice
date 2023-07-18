import { ICAdESProvider } from 'tessa/cards';
import { FileContainer, FileSignatureState, IFileSignature, IFileVersion } from 'tessa/files';
import { IGridColumnViewModel, IGridRowViewModel } from 'components/cardElements/grid';
export declare class FileListSignsDialogViewModel {
    constructor(edsProvider: ICAdESProvider, version: IFileVersion, fileContainer: FileContainer);
    private _columns;
    private _edsProvider;
    private _version;
    private _fileContainer;
    private _selectedRows;
    private _showVerifyInfo;
    private _horizontalScroll;
    private _isLoading;
    private _signatures;
    get horizontalScroll(): boolean;
    set horizontalScroll(value: boolean);
    get showVerifyInfo(): boolean;
    set showVerifyInfo(value: boolean);
    get isLoading(): boolean;
    get signatures(): IFileSignature[];
    get columns(): IGridColumnViewModel[];
    get rows(): IGridRowViewModel[];
    get signState(): FileSignatureState;
    get signStateText(): string;
    verifySignatures(onClose: () => void): Promise<void>;
    removeSignature: (signature: IFileSignature) => Promise<void>;
}
