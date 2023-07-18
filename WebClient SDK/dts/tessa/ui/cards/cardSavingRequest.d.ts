import { Card } from 'tessa/cards';
export declare enum CardSavingMode {
    RefreshOnSuccess = 0,
    KeepPreviousCard = 1
}
export declare class CardSavingRequest {
    constructor(savingMode?: CardSavingMode, cardModifierAction?: ((card: Card) => void) | null, resetFilePreview?: boolean, isClosingRequest?: boolean, cardModifierIsSeparateFromFilesContents?: boolean);
    private static _default;
    savingMode: CardSavingMode;
    cardModifierAction: ((card: Card) => void) | null;
    resetFilePreview: boolean;
    isClosingRequest: boolean;
    cardModifierIsSeparateFromFilesContents: boolean;
    static get default(): CardSavingRequest;
}
