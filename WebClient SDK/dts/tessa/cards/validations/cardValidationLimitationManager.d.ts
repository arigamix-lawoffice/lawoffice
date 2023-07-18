import { CardValidationContext } from './cardValidationContext';
import { CardValidationResult } from './cardValidationResult';
import { ValidationResult } from 'tessa/platform/validation';
export interface ICardValidationLimitationManager {
    limitSections(sectionNames: string[]): any;
    limitRows(sectionName: string, rowIds: guid[]): any;
    clearLimitations(): any;
    sectionIsAllowed(sectionName: string): boolean;
    rowIsAllowed(sectionName: string, rowId: guid): boolean;
    columnIsAllowed(sectionName: string, columnName: string): boolean;
    getCardResult(context: CardValidationContext, result: CardValidationResult): ValidationResult;
    excludeSections(...sectionNames: string[]): any;
    excludeColumns(sectionName: string, ...columnNames: string[]): any;
}
export declare class CardValidationLimitationManager implements ICardValidationLimitationManager {
    private _limitedRowIdsBySectionName;
    private _excludedColumnsBySectionName;
    limitSections(sectionNames: string[]): void;
    limitRows(sectionName: string, rowIds: guid[]): void;
    clearLimitations(): void;
    sectionIsAllowed(sectionName: string): boolean;
    rowIsAllowed(sectionName: string, rowId: guid): boolean;
    getCardResult(context: CardValidationContext, result: CardValidationResult): ValidationResult;
    columnIsAllowed(sectionName: string, columnName: string): boolean;
    private static addRowResults;
    excludeSections(...sectionNames: string[]): void;
    excludeColumns(sectionName: string, ...columnsNames: string[]): void;
}
