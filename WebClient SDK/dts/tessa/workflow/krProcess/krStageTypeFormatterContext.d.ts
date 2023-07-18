import { CardRow, Card } from 'tessa/cards';
import { IUserSession } from 'common/utility/userSession';
import { IStorage } from 'tessa/platform/storage';
export interface IKrStageTypeFormatterContext {
    readonly stageTypeId: guid;
    readonly session: IUserSession;
    readonly info: IStorage;
    readonly card: Card;
    readonly stageRow: CardRow;
    readonly settings: IStorage | null;
    displayTimeLimit: string | null;
    displayParticipants: string | null;
    displaySettings: string | null;
}
export declare class KrStageTypeFormatterContext implements IKrStageTypeFormatterContext {
    constructor(stageTypeId: guid, session: IUserSession, info: IStorage, card: Card, stageRow: CardRow, settings: IStorage | null);
    readonly stageTypeId: guid;
    readonly session: IUserSession;
    readonly info: IStorage;
    readonly card: Card;
    readonly stageRow: CardRow;
    readonly settings: IStorage | null;
    displayTimeLimit: string | null;
    displayParticipants: string | null;
    displaySettings: string | null;
}
