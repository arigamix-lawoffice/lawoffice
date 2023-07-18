import { FormViewModel, FormViewModelState } from './formViewModel';
import { DefaultFormSettings } from './defaultFormSettings';
import { ICardModel, IFormState, IBlockViewModel, IControlViewModel, IControlState } from '../interfaces';
import { CardTypeForm } from 'tessa/cards/types';
/**
 * Базовый класс для модели представления для формы по умолчанию.
 */
export declare abstract class DefaultFormViewModel extends FormViewModel {
    constructor(form: CardTypeForm, model: ICardModel, blocks: IBlockViewModel[], formSettings: DefaultFormSettings);
    protected _controls: ReadonlyMap<string, IControlViewModel>;
    private _fileControls;
    /**
     * Контейнер для именованных элементов управления, содержащихся в текущей форме
     * или в её дочерних формах.
     */
    get controls(): ReadonlyMap<string, IControlViewModel>;
    get filePreviewIsDisabledInternal(): boolean;
    getState(): IFormState;
    get stretchVertically(): boolean;
}
/**
 * Состояние формы DefaultFormViewModel.
 * Наследники класса DefaultFormViewModel могут унаследовать класс DefaultFormViewModelState,
 * добавив в него сохраняемую информацию.
 */
export declare class DefaultFormViewModelState extends FormViewModelState implements IFormState {
    constructor(form: DefaultFormViewModel);
    readonly controlStatesByName: Map<string, IControlState>;
    apply(form: DefaultFormViewModel): boolean;
}
