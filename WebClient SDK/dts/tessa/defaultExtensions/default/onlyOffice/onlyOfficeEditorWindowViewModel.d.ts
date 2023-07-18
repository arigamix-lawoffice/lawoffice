import { IFileVersion } from 'tessa/files';
import { OnlyOfficeApi } from './onlyOfficeApi';
/**
 * Представляет собой модель редактора, используемого для чтения и редактирования файлов в отдельной вкладке браузера.
 */
export declare class OnlyOfficeEditorWindowViewModel {
    readonly placeholder: string;
    readonly version: IFileVersion;
    readonly otherVersion: IFileVersion | undefined;
    readonly forEdit: boolean;
    private readonly _cardId;
    private readonly _api;
    private _window;
    /**
     *
     * @param api
     * @param version main file
     * @param forEdit
     * @param cardId
     * @param otherVersion file for a merge/compare operation
     */
    constructor(api: OnlyOfficeApi, version: IFileVersion, forEdit: boolean, cardId: guid, otherVersion?: IFileVersion | undefined);
    load: (coedit?: boolean, onCoeditChanged?: (() => Promise<unknown>) | undefined) => Promise<void>;
    mergeOrCompare: () => Promise<void>;
    unload: () => Promise<void>;
    /**
     * Open editor window
     * @param id main file id
     * @param otherId for merge/conmpare operations
     */
    private openEditorWindow;
}
