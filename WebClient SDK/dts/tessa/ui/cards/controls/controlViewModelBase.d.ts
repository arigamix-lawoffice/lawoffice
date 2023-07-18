/// <reference types="react" />
import { IBlockViewModel, IControlViewModel, IControlState, ICardCommitChangesContext } from '../interfaces';
import { CardHelpMode } from '../cardHelpMode';
import { SupportUnloadingViewModel } from '../supportUnloadingViewModel';
import { CardTypeControl, CardTypeControlSealed } from 'tessa/cards/types';
import { Visibility } from 'tessa/platform/visibility';
import { IValidationResultBuilder } from 'tessa/platform/validation';
import { HorizontalAlignment, VerticalAlignment, Margin, EventHandler } from 'tessa/platform';
import { MediaStyle } from 'ui/mediaStyle';
import { ClassNameList } from 'tessa/ui';
import { Theme } from 'tessa/ui/themes';
import { TabSelectedContext, TabSelectedEventArgs } from '../tabSelectedEventArgs';
import { CustomControlStyle } from 'tessa/ui/cards';
export declare abstract class ControlViewModelBase extends SupportUnloadingViewModel implements IControlViewModel {
    constructor(control: CardTypeControl);
    protected _initialized: boolean;
    protected _block: IBlockViewModel;
    protected _caption: string;
    protected _tooltip: string;
    protected _helpMode: CardHelpMode;
    protected _helpValue: string;
    protected _captionVisibility: Visibility;
    protected _captionStyle: MediaStyle | null;
    protected _controlVisibility: Visibility;
    protected _controlStyle: MediaStyle | null;
    protected _margin: Margin | null;
    protected _minWidth: string | null;
    protected _maxWidth: string | null;
    protected _columnSpan: number;
    protected _emptyColumnsToTheLeft: number;
    protected _horizontalAlignment: HorizontalAlignment;
    protected _verticalAlignment: VerticalAlignment;
    protected _startAtNewLine: boolean;
    protected _isRequired: boolean;
    protected _isReadonly: boolean;
    protected _isSpanned: boolean;
    protected _isFocused: boolean;
    protected _hasActiveValidation: boolean;
    protected _customStyle: CustomControlStyle | null;
    protected _reactComponentRef: React.RefObject<any> | null;
    private _errorAtom;
    readonly componentId: guid;
    /**
     * Информация о типе отображаемого элемента управления.
     */
    readonly cardTypeControl: CardTypeControlSealed;
    readonly className: ClassNameList;
    /**
     * Имя элемента управления, по которому он доступен в коллекции, или null,
     * если у элемента управления не задано имя.
     */
    get name(): string | null;
    /**
     * Блок, в котором размещён текущий элемент управления.
     */
    get block(): IBlockViewModel;
    /**
     * Заголовок элемента управления.
     */
    get caption(): string;
    set caption(value: string);
    /**
     * Всплывающая подсказка для элемента управления или null, если подсказка отсутствует.
     */
    get tooltip(): string;
    set tooltip(value: string);
    /**
     * Способ отображения справочной информации для контрола.
     */
    get helpMode(): CardHelpMode;
    set helpMode(value: CardHelpMode);
    /**
     * Строка для отображения справочной информации контрола.
     */
    get helpValue(): string;
    set helpValue(value: string);
    /**
     * Видимость заголовка элемента управления.
     */
    get captionVisibility(): Visibility;
    set captionVisibility(value: Visibility);
    /**
     * Стиль заголовка элемента управления.
     */
    get captionStyle(): MediaStyle | null;
    set captionStyle(value: MediaStyle | null);
    /**
     * Видимость элемента управления.
     */
    get controlVisibility(): Visibility;
    set controlVisibility(value: Visibility);
    /**
     * Стиль элемента управления.
     */
    get controlStyle(): MediaStyle | null;
    set controlStyle(value: MediaStyle | null);
    /**
     * Заданный в настройках отступ элемента управления относительно других элементов управления.
     * По умолчанию отступ отсутствует.
     */
    get margin(): Margin | null;
    set margin(value: Margin | null);
    /**
     * Минимальная ширина контрола. По умолчанию значение равно 0 и не может быть меньше.
     * Пример: 10px
     */
    get minWidth(): string | null;
    set minWidth(value: string | null);
    /**
     * Максимальная ширина контрола.
     * Пример: 10px
     */
    get maxWidth(): string | null;
    set maxWidth(value: string | null);
    /**
     * Количество колонок, которые занимает контрол по горизонтали. Неактуально для контролов,
     * растягиваемых по ширине всей строки. По умолчанию значение равно 1 и не может быть меньше.
     * Если заданное количество колонок больше, чем общее количество колонок в блоке,
     * то контрол растягивается на ширину строки.
     */
    get columnSpan(): number;
    set columnSpan(value: number);
    /**
     * Количество пустых колонок слева.
     */
    get emptyColumnsToTheLeft(): number;
    set emptyColumnsToTheLeft(value: number);
    /**
     * Выравнивание по горизонтали элемента управления.
     */
    get horizontalAlignment(): HorizontalAlignment;
    set horizontalAlignment(value: HorizontalAlignment);
    /**
     * Выравнивание по вертикали элемента управления.
     */
    get verticalAlignment(): VerticalAlignment;
    set verticalAlignment(value: VerticalAlignment);
    /**
     * Признак того, что текущий контрол в блоке всегда начинается с новой строки.
     */
    get startAtNewLine(): boolean;
    set startAtNewLine(value: boolean);
    /**
     * Признак того, что элемент управления не содержит отображаемых данных.
     */
    get isEmpty(): boolean;
    /**
     * Признак того, что элемент управления отмечен, как обязательный для заполнения.
     */
    get isRequired(): boolean;
    set isRequired(value: boolean);
    /**
     * Текст валидации обязательного для заполнения элемента.
     */
    requiredText: string;
    /**
     * Признак того, что элемент управления доступен только для чтения
     * или не содержит редактируемых данных.
     */
    get isReadOnly(): boolean;
    set isReadOnly(value: boolean);
    /**
     * Признак того, что элемент управления должен быть растянут
     * на ширину колонки при выводе в несколько колонок.
     */
    get isSpanned(): boolean;
    set isSpanned(value: boolean);
    /**
     * Признак того, что элемент управления имеет логический фокус.
     */
    get isFocused(): boolean;
    set isFocused(value: boolean);
    /**
     * Признак того, что в элементе управления следует включить активную валидацию.
     * При этом если для элемента управления введено некорректное значение, то он будет
     * уведомлять об этом рамкой валидации. Значение устанавливливается равным true обычно
     * после неудачного сохранения карточки. По умолчанию значение равно false.
     */
    get hasActiveValidation(): boolean;
    set hasActiveValidation(value: boolean);
    /**
     * Функция валидации, проверяющая элемент управления на корректность его значения, или null,
     * если дополнительные проверки значения отсутствуют. Проверка на незаполненное значение всё
     * равно выполняется, если элемент управления был отмечен как обязательный
     * для заполнения (в т.ч. посредством валидатора).
     */
    validationFunc: ((control: IControlViewModel) => string | null) | null;
    /**
     * Сообщение об ошибке, связанное с текущим объектом, или null, если ошибки нет.
     */
    get error(): string | null;
    get hasEmptyValue(): boolean;
    get customStyle(): CustomControlStyle | null;
    set customStyle(val: CustomControlStyle | null);
    initialize(): void;
    protected initializeCore(): void;
    tryGetReactComponentRef(): any | null;
    bindReactComponentRef(ref: React.RefObject<any>): void;
    unbindReactComponentRef(): void;
    /**
     * Метод focus() вызывает нативный метод элемента. Поэтому для правильной работы метода,
     * необходимо чтобы контрол уже был отрендерен.
     * Если необходимо вызвать focus() при открытии карточки, то нужно вызывать его в CardUIExtension.contextInitialized.
     * В этом методе карточка уже будет отрисована и все контролы будут иметь ссылки на свои html элементы.
     * Если необходимы вызвать при открытии формы контрола таблицы, то нужно вызывать его в GridViewModel.rowInitialized.
     */
    focus(opt?: FocusOptions): void;
    setBlock(block: IBlockViewModel): void;
    getState(): IControlState | null;
    setState(_state: IControlState): boolean;
    commitChanges(_context: ICardCommitChangesContext): void;
    protected onUnloading(_validationResult: IValidationResultBuilder): void;
    getCaptionStyle(): MediaStyle | null;
    getControlStyle(): MediaStyle | null;
    notifyUpdateValidation(): void;
    getNestedVisibleBlocks(): Iterable<IBlockViewModel>;
    tabSelected: EventHandler<(args: TabSelectedEventArgs) => void>;
    tabDeselected: EventHandler<(args: TabSelectedEventArgs) => void>;
    notifyTabSelected(context: TabSelectedContext): Promise<void>;
    notifyTabDeselected(context: TabSelectedContext): Promise<void>;
}
export interface ThemeChangedEventArgs {
    theme: Theme;
}
