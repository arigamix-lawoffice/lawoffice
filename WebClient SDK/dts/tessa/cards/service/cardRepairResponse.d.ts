import { CardResponseBase } from './cardResponseBase';
import { IStorage } from 'tessa/platform/storage';
import { Card } from 'tessa/cards/card';
export declare class CardRepairResponse extends CardResponseBase {
    constructor(storage?: IStorage);
    static readonly cardKey: string;
    get card(): Card;
    set card(value: Card);
    get hasCard(): boolean;
    tryGetCard(): Card | null | undefined;
}
