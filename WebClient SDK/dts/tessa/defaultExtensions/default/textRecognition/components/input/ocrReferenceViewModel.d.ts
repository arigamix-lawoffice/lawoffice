import { CardMetadataSectionSealed } from 'tessa/cards/metadata';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { ICardModel } from 'tessa/ui/cards';
/** Модель представления с данными для контрола выбора выбора даты из календаря. */
export declare class OcrReferenceViewModel {
    /**
     * Создает экземпляр класса {@link OcrReferenceViewModel}.
     * @param {CardTypeEntryControl} control
     * Объект, описывающий расположение и свойства элемента
     * управления для привязки к полям строковой секции карточки.
     * @param {ICardModel} model Модель карточки, доступная в UI.
     */
    constructor(control: CardTypeEntryControl, model: ICardModel, prefix: string, metadataSectionSealed: CardMetadataSectionSealed);
    private readonly _mappingFieldName;
    private readonly _mappingComplexFields;
    private readonly _view;
    private readonly _viewReferencePrefix;
    private readonly _viewRefSection;
    private readonly _viewParameter;
    private readonly _criteriaOperator;
    private readonly _manualInput;
    /** Признак выполнения загрузки данных. */
    private _isDataLoading;
    /** Скрывать кнопку очистки значения. */
    readonly hideClearButton: boolean;
    /** Скрывать кнопку выбора. */
    readonly hideSelectorButton: boolean;
    /** Признак выполнения загрузки данных. */
    get isDataLoading(): boolean;
    set isDataLoading(value: boolean);
    /** Разрешен ручной ввод. */
    get manualInput(): boolean;
    selectValue(parentRowId: guid): Promise<string | null>;
    initializeMappingComplexFields(text: string, parentRowId: guid): Promise<[displayed: string | null, value: string | null]>;
    clearComplexFieldsValues(parentRowId: guid): void;
    private getViewFromSettings;
    private getViewParameterFromSettings;
    private getViewReferencePrefixFromSettings;
    private getCriteriaOperator;
    private addMappingComplexFieldRow;
}
