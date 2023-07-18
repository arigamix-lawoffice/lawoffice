import { CardTypeForm, CardTypeFormSealed } from 'tessa/cards/types';
import { ICardModel } from 'tessa/ui/cards';
import { EventHandler } from 'tessa/platform';
import { IValidationResultBuilder } from 'tessa/platform/validation';
import { MenuAction } from 'tessa/ui/menuAction';
import { FormViewModelBase, FormViewModelBaseState } from './formViewModelBase';
import { IFormWithBlocksViewModel, IBlockViewModel, IFormState, FormMenuContext } from '../interfaces';
import { CardHelpMode } from '../cardHelpMode';
import { TabSelectedContext, TabSelectedEventArgs } from '../tabSelectedEventArgs';
/**
 * Базовый класс для моделей представления формы в автоматическом UI карточки.
 */
export declare abstract class FormViewModel extends FormViewModelBase implements IFormWithBlocksViewModel {
    constructor(form: CardTypeForm, model: ICardModel);
    protected _blocks: ReadonlyArray<IBlockViewModel>;
    protected _tabCaption: string | null;
    protected _helpMode: CardHelpMode;
    protected _helpValue: string;
    protected _blockMargin: string | null;
    protected _blockInterval: number | null;
    protected _horizontalInterval: number | undefined;
    protected _filePreviewIsDisabled: boolean;
    protected _gridColumns: (number | null)[] | undefined;
    protected _gridRows: (number | null)[] | undefined;
    protected _hasFileControl: boolean;
    protected _isCollapsed: boolean;
    /**
     * Информация о типе отображаемой формы.
     */
    readonly cardTypeForm: CardTypeFormSealed;
    /**
     * Упорядоченная коллекция блоков на форме, доступная только для чтения.
     */
    get blocks(): ReadonlyArray<IBlockViewModel>;
    /**
     * Имя формы, по которому она доступна в коллекции, или null, если это основная форма
     * типа карточки или другая форма, не имеющая имени.
     */
    get name(): string | null;
    get isEmpty(): boolean;
    /**
     * Заголовок вкладки или null, если форма не является вкладкой или заголовок не задан.
     */
    get tabCaption(): string | null;
    set tabCaption(value: string | null);
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
     * Отступ между блоками внутри формы в виде значения для css-параметра margin или padding.
     */
    get blockMargin(): string | null;
    set blockMargin(value: string | null);
    /**
     * Отступ между блоками внутри формы.
     */
    get blockInterval(): number | null;
    set blockInterval(value: number | null);
    /**
     * Отступ горизонтальный между блоками внутри формы.
     */
    get horizontalInterval(): number | undefined;
    set horizontalInterval(value: number | undefined);
    /**
     * Не показывать предпросмотр
     */
    get filePreviewIsDisabled(): boolean;
    set filePreviewIsDisabled(value: boolean);
    protected get filePreviewIsDisabledInternal(): boolean;
    /**
     * Описание колонок блоков
     */
    get gridColumns(): (number | null)[] | undefined;
    set gridColumns(value: (number | null)[] | undefined);
    /**
     * Описание строк блоков
     */
    get gridRows(): (number | null)[] | undefined;
    set gridRows(value: (number | null)[] | undefined);
    get hasFileControl(): boolean;
    get isCollapsed(): boolean;
    set isCollapsed(value: boolean);
    readonly contextMenuGenerators: ((ctx: FormMenuContext) => void)[];
    protected initializeCore(): void;
    getState(): IFormState;
    notifyTabSelected(context: TabSelectedContext): Promise<void>;
    notifyTabDeselected(context: TabSelectedContext): Promise<void>;
    getContextMenu(): ReadonlyArray<MenuAction>;
    onUnloading(validationResult: IValidationResultBuilder): void;
    readonly tabSelected: EventHandler<(args: TabSelectedEventArgs) => void>;
    readonly tabDeselected: EventHandler<(args: TabSelectedEventArgs) => void>;
}
export declare class FormViewModelState extends FormViewModelBaseState implements IFormState {
    constructor(form: FormViewModel);
    readonly isCollapsed: boolean;
    apply(form: FormViewModel): boolean;
}
