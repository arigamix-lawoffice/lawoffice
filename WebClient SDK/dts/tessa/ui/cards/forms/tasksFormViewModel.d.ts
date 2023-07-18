import { IFormWithBlocksViewModel, IBlockViewModel, ICardModel, IFormState, FormMenuContext } from '../interfaces';
import { CardHelpMode } from '../cardHelpMode';
import { CardTypeFormSealed } from 'tessa/cards/types';
import { SupportUnloadingViewModel } from 'tessa/ui/cards/supportUnloadingViewModel';
import { IValidationResultBuilder } from 'tessa/platform/validation';
import { EventHandler } from 'tessa/platform';
import { TabSelectedContext, TabSelectedEventArgs } from '../tabSelectedEventArgs';
import { ClassNameList } from 'tessa/ui/classNameList';
import { MenuAction } from 'tessa/ui/menuAction';
import type { CustomFormStyle } from 'tessa/ui/cards/customElementStyle';
export declare class TasksFormViewModel extends SupportUnloadingViewModel implements IFormWithBlocksViewModel {
    constructor(model: ICardModel);
    protected _initialized: boolean;
    protected _blocks: ReadonlyArray<IBlockViewModel>;
    protected _tabCaption: string | null;
    protected _helpMode: CardHelpMode;
    protected _helpValue: string;
    protected _blockMargin: string | null;
    protected _headerClass: string;
    protected _contentClass: string;
    protected _isCollapsed: boolean;
    protected _customStyle: CustomFormStyle | null;
    readonly cardModel: ICardModel;
    readonly componentId: guid;
    readonly cardTypeForm: CardTypeFormSealed;
    get customStyle(): CustomFormStyle | null;
    set customStyle(val: CustomFormStyle | null);
    get blocks(): ReadonlyArray<IBlockViewModel>;
    get name(): string | null;
    get isEmpty(): boolean;
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
    get blockMargin(): string | null;
    set blockMargin(value: string | null);
    get headerClass(): string;
    get contentClass(): string;
    readonly className: ClassNameList;
    get hasFileControl(): boolean;
    get filePreviewIsDisabled(): boolean;
    get isCollapsed(): boolean;
    set isCollapsed(value: boolean);
    readonly contextMenuGenerators: ((ctx: FormMenuContext) => void)[];
    initialize(): void;
    protected initializeCore(): void;
    getIsTabMode(): boolean;
    getState(): IFormState;
    setState(state: IFormState): boolean;
    close(): boolean;
    getContextMenu(): ReadonlyArray<MenuAction>;
    onUnloading(validationResult: IValidationResultBuilder): void;
    readonly closed: EventHandler<() => void>;
    tabSelected: EventHandler<(args: TabSelectedEventArgs) => void>;
    tabDeselected: EventHandler<(args: TabSelectedEventArgs) => void>;
    notifyTabSelected(context: TabSelectedContext): Promise<void>;
    notifyTabDeselected(context: TabSelectedContext): Promise<void>;
}
export declare class TasksFormViewModelState implements IFormState {
    constructor(form: TasksFormViewModel);
    readonly isForceTabMode: boolean;
    readonly isCollapsed: boolean;
    apply(form: TasksFormViewModel): boolean;
}
