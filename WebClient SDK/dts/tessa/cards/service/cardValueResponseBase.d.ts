import { CardResponseBase } from './cardResponseBase';
import { Card, CardRow } from 'tessa/cards';
import { IStorage, MapStorage } from 'tessa/platform/storage';
export declare abstract class CardValueResponseBase extends CardResponseBase {
    constructor(storage?: IStorage);
    static readonly cardKey: string;
    static readonly sectionRowsKey: string;
    get card(): Card;
    set card(value: Card);
    get sectionRows(): MapStorage<CardRow>;
    set sectionRows(value: MapStorage<CardRow>);
    private static readonly _cardRowFactory;
    tryGetCard(): Card | null | undefined;
    tryGetSectionRows(): MapStorage<CardRow> | null | undefined;
    protected createDefaultCard(storage: IStorage): Card;
    clean(): void;
    ensureCacheResolved(): void;
}
