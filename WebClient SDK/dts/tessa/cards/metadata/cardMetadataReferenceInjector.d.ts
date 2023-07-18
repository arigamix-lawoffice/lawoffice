import { IStorage, IStorageArray } from 'tessa/platform/storage/storage';
/**
 * Перечень допустимых типов.
 */
export declare enum CardMetadataReferenceTypes {
    CustomControl = "Tessa.Cards.CardTypeCustomControl",
    EntryControl = "Tessa.Cards.CardTypeEntryControl",
    ParamControl = "Tessa.Cards.CardTypeParamControl",
    TabControl = "Tessa.Cards.CardTypeTabControl",
    TableControl = "Tessa.Cards.CardTypeTableControl",
    Block = "Tessa.Cards.CardTypeBlock",
    NamedForm = "Tessa.Cards.CardTypeNamedForm",
    TabControlForm = "Tessa.Cards.CardTypeTabControlForm",
    TableForm = "Tessa.Cards.CardTypeTableForm",
    CompletionOption = "Tessa.Cards.CardTypeCompletionOption",
    Extension = "Tessa.Cards.CardTypeExtension",
    Validator = "Tessa.Cards.CardTypeValidator"
}
/**
 * Класс-помощник для вставки глобальных ссылок в метаинформацию о типах карточек.
 */
export declare class CardMetadataReferenceInjector {
    constructor(globalReferences?: IStorage | null);
    private _globalReferences;
    private static _expectedControlType;
    private static getQualifiedTypeName;
    private static expectedTypeIs;
    private getGlobalObject;
    private injectIntoArray;
    private injectIntoObject;
    private injectAll;
    private injectCompletionOptions;
    private injectReferencesIntoForm;
    injectReferences(cardTypes: IStorageArray<IStorage>): void;
}
