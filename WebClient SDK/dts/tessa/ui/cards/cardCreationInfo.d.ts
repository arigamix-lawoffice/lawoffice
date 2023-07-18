import { CardCreationMode } from './cardCreationMode';
import { ICardEditorModel } from './interfaces';
export declare class CardCreationInfo {
    constructor(createCardFunc: () => Promise<ICardEditorModel | null>, creationMode?: CardCreationMode, creationModeDisplayText?: string | null);
    private readonly _createCardFunc;
    private static _last;
    readonly creationMode: CardCreationMode;
    readonly creationModeDisplayText: string | null;
    workspaceName: string;
    workspaceInfo: string;
    static get last(): CardCreationInfo | null;
    createCard(updateLast?: boolean, ignoreSingleton?: boolean, saveCreationRequest?: boolean): Promise<ICardEditorModel | null>;
    static resetLast(): void;
}
