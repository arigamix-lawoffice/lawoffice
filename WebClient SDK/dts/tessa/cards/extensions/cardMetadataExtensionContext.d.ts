import { CardMetadata } from 'tessa/cards/metadata';
export interface ICardMetadataExtensionContext {
    readonly cardMetadata: CardMetadata;
}
export declare class CardMetadataExtensionContext implements ICardMetadataExtensionContext {
    constructor(cardMetadata: CardMetadata);
    readonly cardMetadata: CardMetadata;
}
