import { CardValidationContext, EntryFieldKey, TableFieldKey, TableRowKey } from './cardValidationContext';
import { ValidationResult, IValidationResultBuilder } from 'tessa/platform/validation';
export declare class CardValidationResult {
    constructor(context: CardValidationContext | null, entryFieldBuilders: Map<string, [EntryFieldKey, IValidationResultBuilder]>, tableFieldBuilders: Map<string, [TableFieldKey, IValidationResultBuilder]>, tableRowBuilders: Map<string, [TableRowKey, IValidationResultBuilder]>, sectionBuilders: Map<string, IValidationResultBuilder>, cardBuilder: IValidationResultBuilder);
    private _entryFieldResults;
    private _tableFieldResults;
    private _tableRowResults;
    private _sectionResults;
    private _sectionOwnResults;
    private _cardResults;
    readonly context: CardValidationContext | null;
    static get empty(): CardValidationResult;
    getEntryFieldResult(sectionName: string, fieldName: string): ValidationResult;
    getTableFieldResult(sectionName: string, rowIndex: number, fieldName: string): ValidationResult;
    getTableRowResult(sectionName: string, rowIndex: number): ValidationResult;
    getSectionResult(sectionName: string, ownResultsOnly?: boolean): ValidationResult;
    getCardResult(): ValidationResult;
    getLimitedCardResult(): ValidationResult;
    private static aggregate;
    private static addIfNotEmpty;
    private static getResultOrEmpty;
}
