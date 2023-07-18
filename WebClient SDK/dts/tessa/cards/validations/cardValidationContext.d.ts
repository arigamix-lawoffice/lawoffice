import { CardValidationMode } from './cardValidationMode';
import { CardValidationResult } from './cardValidationResult';
import { ICardValidationLimitationManager } from './cardValidationLimitationManager';
import { Card } from '../card';
import { CardTypeSealed } from '../types';
import { CardStoreMode } from '../cardStoreMode';
import { CardMetadataSealed } from '../metadata';
import { IUserSession } from 'common/utility/userSession';
import { IStorage } from 'tessa/platform/storage';
import { IValidationResultBuilder } from 'tessa/platform/validation';
export declare class CardValidationContext {
    constructor(mainCard: Card, mainCardType: CardTypeSealed, storeMode: CardStoreMode, taskCard: Card | null, taskCardType: CardTypeSealed | null, cardMetadata: CardMetadataSealed, session: IUserSession, externalContextInfo?: IStorage | null, limitations?: ICardValidationLimitationManager | null, validationMode?: CardValidationMode | null);
    private _entryFieldBuilders;
    private _tableFieldBuilders;
    private _tableRowBuilders;
    private _sectionBuilders;
    private _cardBuilder;
    readonly validationMode: CardValidationMode;
    readonly mainCard: Card;
    readonly mainCardType: CardTypeSealed;
    readonly storeMode: CardStoreMode;
    readonly taskCard: Card | null;
    readonly taskCardType: CardTypeSealed | null;
    readonly cardMetadata: CardMetadataSealed;
    readonly session: IUserSession;
    readonly limitations: ICardValidationLimitationManager;
    forceWarnings: boolean;
    readonly externalContextInfo: IStorage | null;
    private static getOrAdd;
    getEntryFieldValidator(sectionName: string, fieldName: string): IValidationResultBuilder;
    getTableFieldValidator(sectionName: string, rowIndex: number, fieldName: string): IValidationResultBuilder;
    getTableRowValidator(sectionName: string, rowIndex: number): IValidationResultBuilder;
    getSectionValidator(sectionName: string): IValidationResultBuilder;
    getCardValidator(): IValidationResultBuilder;
    buildResult(): CardValidationResult;
}
export declare class EntryFieldKey {
    constructor(sectionName: string, fieldName: string);
    readonly key: string;
    readonly sectionName: string;
    readonly fieldName: string;
    isPartOfSection(sectionName: string): boolean;
}
export declare class TableFieldKey {
    constructor(sectionName: string, rowIndex: number, fieldName: string);
    readonly key: string;
    readonly sectionName: string;
    readonly rowIndex: number;
    readonly fieldName: string;
    readonly row: TableRowKey;
    isPartOfRow(row: TableRowKey): boolean;
}
export declare class TableRowKey {
    constructor(sectionName: string, rowIndex: number);
    readonly key: string;
    readonly sectionName: string;
    readonly rowIndex: number;
    isPartOfSection(sectionName: string): boolean;
}
