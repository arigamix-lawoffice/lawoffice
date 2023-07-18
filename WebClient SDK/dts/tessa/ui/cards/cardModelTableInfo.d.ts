import { ICardModel } from './interfaces';
import { CardSection, CardRow } from 'tessa/cards';
export declare class CardModelTableInfo {
    constructor(row: CardRow, section: CardSection, nestingLevel: number, getModel: () => ICardModel);
    readonly row: CardRow;
    readonly section: CardSection;
    readonly nestingLevel: number;
    private _getModel;
    get model(): ICardModel;
}
