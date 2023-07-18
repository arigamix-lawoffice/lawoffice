import { MediaStyle } from 'ui';
import { ControlButtonsContainer, ControlViewModelBase } from 'tessa/ui/cards/controls';
import { CardTypeEntryControl } from 'tessa/cards/types';
import { ControlKeyDownEventArgs, ICardModel } from 'tessa/ui/cards';
import { EventHandler } from 'tessa/platform';
import { OcrDateTimeViewModel } from './ocrDateTimeViewModel';
import { OcrReferenceViewModel } from './ocrReferenceViewModel';
import { OcrTextBoxViewModel } from './ocrTextBoxViewModel';
import { ValidationResult, ValidationResultBuilder } from 'tessa/platform/validation';
/** Модель представления элемента для редактируемого поля. */
export declare class OcrInputViewModel extends ControlViewModelBase {
    /**
     * Создает экземпляр класса {@link OcrInputViewModel}.
     * @param {CardTypeEntryControl} control
     * Объект, описывающий расположение и свойства элемента
     * управления для привязки к полям строковой секции карточки.
     * @param {ICardModel} model Модель карточки, доступная в UI.
     */
    constructor(control: CardTypeEntryControl, model: ICardModel);
    private _manualInput;
    private _text;
    private _validationResult;
    private _mappingField;
    private readonly _mappingSectionName;
    private readonly _mappingFieldName;
    private readonly _mappingFields;
    private readonly _buttonsContainer;
    private readonly _validator;
    private readonly _disposers;
    private readonly _isNullable;
    private readonly _nullableResult;
    private readonly _requiredResult;
    private static readonly _noRowsWarningResult;
    private static readonly _noRowsErrorResult;
    private static readonly _longOperationResult;
    /**
     * Модель представления с данными для контрола ввода текстовых данных.
     * По умолчанию `null`, если контрол не содержит текстовые данные.
     */
    readonly textBoxViewModel: OcrTextBoxViewModel | null;
    /**
     * Модель представления с данными для контрола выбора выбора даты из календаря.
     * По умолчанию `null`, если контрол не содержит данные даты и времени.
     */
    readonly dateTimeViewModel: OcrDateTimeViewModel | null;
    /**
     * Модель представления с данными для контрола выбора выбора даты из календаря.
     * По умолчанию `null`, если контрол не содержит ссылочные данные.
     */
    readonly referenceViewModel: OcrReferenceViewModel | null;
    /** Текст, отображаемый в контроле. */
    get text(): string;
    set text(value: string);
    /** Результат проверки введенного значения. */
    get validationResult(): ValidationResult | null;
    set validationResult(value: ValidationResult | null);
    /** Контейнер с кнопками контрола. */
    get buttonsContainer(): ControlButtonsContainer;
    get error(): string | null;
    get hasEmptyValue(): boolean;
    getControlStyle(): MediaStyle | null;
    selectAll(): void;
    private initializeButtons;
    private initialValidation;
    private emptyValidation;
    private onTextChangedReaction;
    private createMappingField;
    private setMappingField;
    readonly keyDown: EventHandler<(args: ControlKeyDownEventArgs) => void>;
    private defaultKeyDown;
    onUnloading(validationResult: ValidationResultBuilder): void;
}
