import { ICardMetadata } from './cardMetadata';
import { Card, CardRow } from 'tessa/cards';
export declare class CardMetadataBinder {
    constructor(card: Card, metadata: ICardMetadata);
    private _card;
    private _metadata;
    removeRow(sectionName: string, row: CardRow): void;
    private removeInsertedRow;
}
