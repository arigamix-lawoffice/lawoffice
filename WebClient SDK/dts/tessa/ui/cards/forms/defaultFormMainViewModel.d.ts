import { IValidationResultBuilder } from 'tessa/platform/validation';
import { DefaultPreviewAreaViewModel } from './defaultPreviewAreaViewModel';
import { FormViewModelBase, FormViewModelBaseState } from './formViewModelBase';
import { ICardModel, IFormWithBlocksViewModel, IFormState, IFormWithTabsViewModel } from '../interfaces';
/**
 * Модель представления для формы по умолчанию основной части карточки.
 */
export declare abstract class DefaultFormMainViewModel extends FormViewModelBase implements IFormWithTabsViewModel {
    protected constructor(model: ICardModel);
    protected _tabs: IFormWithBlocksViewModel[];
    protected _selectedTab: IFormWithBlocksViewModel | null;
    protected _openedTabs: Set<string>;
    protected _modelInitializing: ICardModel | null;
    get openedTabs(): ReadonlySet<string>;
    /**
     * Вкладки карточки.
     */
    get tabs(): ReadonlyArray<IFormWithBlocksViewModel>;
    /**
     * Видимые вкладки карточки.
     */
    get visibleTabs(): ReadonlyArray<IFormWithBlocksViewModel>;
    get isEmpty(): boolean;
    /**
     * Текущая выбранная вкладка.
     */
    get selectedTab(): IFormWithBlocksViewModel;
    set selectedTab(value: IFormWithBlocksViewModel);
    readonly previewArea: DefaultPreviewAreaViewModel;
    get previewAreaHidden(): boolean;
    protected initializeCore(): void;
    restoreSelectedTab(): void;
    findAvailableSelectedTab(): IFormWithBlocksViewModel | null;
    getState(): IFormState;
    tabsAreCollapsed: boolean;
    onUnloading(validationResult: IValidationResultBuilder): void;
}
/**
 * Состояние формы DefaultFormMainViewModel.
 * Наследники класса DefaultFormMainViewModel могут унаследовать
 * класс DefaultFormMainViewModelState, добавив в него сохраняемую информацию.
 */
export declare class DefaultFormMainViewModelState extends FormViewModelBaseState {
    constructor(form: DefaultFormMainViewModel);
    readonly tabsAreCollapsed: boolean;
    readonly selectedTabIndex: number;
    readonly formStatesByName: Map<string, IFormState>;
    apply(form: DefaultFormMainViewModel): boolean;
}
