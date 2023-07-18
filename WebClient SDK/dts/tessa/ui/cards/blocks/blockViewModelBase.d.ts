import { CardTypeBlock, CardTypeBlockSealed } from 'tessa/cards/types';
import { Visibility } from 'tessa/platform/visibility';
import { IValidationResultBuilder } from 'tessa/platform/validation';
import { EventHandler } from 'tessa/platform';
import { TabSelectedContext, TabSelectedEventArgs } from '../tabSelectedEventArgs';
import { IFormWithBlocksViewModel, IBlockViewModel, IControlViewModel } from '../interfaces';
import { SupportUnloadingViewModel } from '../supportUnloadingViewModel';
import type { CustomBlockStyle } from 'tessa/ui/cards/customElementStyle';
export declare abstract class BlockViewModelBase extends SupportUnloadingViewModel implements IBlockViewModel {
    constructor(block: CardTypeBlock, caption: string, captionVisibility: Visibility, leftCaptions: boolean, collapsed: boolean, doNotCollapseWithTopBlock: boolean, columnIndex?: number, rowIndex?: number, columnSpan?: number, rowSpan?: number, stretchVertically?: boolean);
    protected _initialized: boolean;
    protected _form: IFormWithBlocksViewModel;
    protected _caption: string;
    protected _captionVisibility: Visibility;
    protected _blockVisibility: Visibility;
    protected _blockMargin: string | null;
    protected _controls: ReadonlyArray<IControlViewModel>;
    protected _controlMargin: string | null;
    protected _leftCaptions: boolean;
    protected _verticalInterval: number;
    protected _horizontalInterval: number;
    protected _columnIndex?: number;
    protected _rowIndex?: number;
    protected _columnSpan?: number;
    protected _rowSpan?: number;
    protected _collapsed: boolean;
    protected _stretchVertically: boolean;
    protected _customStyle: CustomBlockStyle | null;
    get customStyle(): CustomBlockStyle | null;
    set customStyle(val: CustomBlockStyle | null);
    /**
     * Нужно ли схлопывать вместе с верхним блоком
     */
    readonly doNotCollapseWithTopBlock: boolean;
    readonly componentId: guid;
    /**
     * Информация о типе отображаемого блока.
     */
    readonly cardTypeBlock: CardTypeBlockSealed;
    /**
     * Имя блока, по которому он доступен в коллекции.
     */
    get name(): string | null;
    /**
     * Форма, в которой размещён текущий блок.
     */
    get form(): IFormWithBlocksViewModel;
    /**
     * Заголовок блока.
     */
    get caption(): string;
    set caption(value: string);
    /**
     * Видимость заголовка блока.
     */
    get captionVisibility(): Visibility;
    set captionVisibility(value: Visibility);
    /**
     * Видимость блока.
     */
    get blockVisibility(): Visibility;
    set blockVisibility(value: Visibility);
    /**
     * Предпочитаемый отступ блока относительно других блоков.
     */
    get blockMargin(): string | null;
    set blockMargin(value: string | null);
    /**
     * Упорядоченная коллекция элементов управления в блоке, доступная только для чтения.
     */
    get controls(): ReadonlyArray<IControlViewModel>;
    /**
     * Предпочитаемый отступ элемента управления относительно других элементов управления.
     */
    get controlMargin(): string | null;
    set controlMargin(value: string | null);
    /**
     * Заголовок блока.
     */
    get leftCaptions(): boolean;
    set leftCaptions(value: boolean);
    /**
     * Вертикальный интервал
     */
    get verticalInterval(): number;
    set verticalInterval(value: number);
    /**
     * Горизонтальный интервал
     */
    get horizontalInterval(): number;
    set horizontalInterval(value: number);
    /**
     * Индекс колонки
     *
     * @readonly
     * @type {(number | undefined)}
     * @memberof BlockViewModelBase
     */
    get columnIndex(): number | undefined;
    set columnIndex(value: number | undefined);
    /**
     * Индекс строки
     */
    get rowIndex(): number | undefined;
    set rowIndex(value: number | undefined);
    /**
     * Кол-во занимаемых колонок
     */
    get columnSpan(): number | undefined;
    set columnSpan(value: number | undefined);
    /**
     * Кол-во занимаемых строк
     */
    get rowSpan(): number | undefined;
    set rowSpan(value: number | undefined);
    /**
     * Свернут ли блок.
     */
    get collapsed(): boolean;
    set collapsed(value: boolean);
    /**
     * Признак того, что блок не содержит отображаемых данных.
     */
    get isEmpty(): boolean;
    /**
     * Признак того, что блок должен быть растянут по вертикали.
     */
    get stretchVertically(): boolean;
    set stretchVertically(value: boolean);
    initialize(): void;
    protected initializeCore(): void;
    setForm(form: IFormWithBlocksViewModel): void;
    getConnectedBlocks(): IBlockViewModel[];
    private setCollapsedForBottomBlocks;
    onUnloading(validationResult: IValidationResultBuilder): void;
    tabSelected: EventHandler<(args: TabSelectedEventArgs) => void>;
    tabDeselected: EventHandler<(args: TabSelectedEventArgs) => void>;
    notifyTabSelected(context: TabSelectedContext): Promise<void>;
    notifyTabDeselected(context: TabSelectedContext): Promise<void>;
}
