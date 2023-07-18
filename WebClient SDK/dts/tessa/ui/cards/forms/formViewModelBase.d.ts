import { ICardModel, IFormState, IFormViewModelBase } from '../interfaces';
import { SupportUnloadingViewModel } from '../supportUnloadingViewModel';
import { IValidationResultBuilder } from 'tessa/platform/validation';
import { EventHandler } from 'tessa/platform';
import { ClassNameList } from 'tessa/ui/classNameList';
import type { CustomFormStyle } from 'tessa/ui/cards/customElementStyle';
/**
 * Базовый класс для моделей представления формы в автоматическом UI карточки.
 */
export declare abstract class FormViewModelBase extends SupportUnloadingViewModel implements IFormViewModelBase {
    constructor(model: ICardModel);
    protected _initialized: boolean;
    protected _headerClass: string;
    protected _contentClass: string;
    protected _hasFileControl: boolean;
    protected _isCollapsed: boolean;
    protected _customStyle: CustomFormStyle | null;
    /**
     * Признак того, что форма не содержит отображаемых данных.
     */
    get isEmpty(): boolean;
    readonly cardModel: ICardModel;
    readonly componentId: guid;
    get headerClass(): string;
    get contentClass(): string;
    readonly className: ClassNameList;
    get customStyle(): CustomFormStyle | null;
    set customStyle(val: CustomFormStyle | null);
    initialize(): void;
    protected initializeCore(): void;
    getState(): IFormState;
    setState(state: IFormState): boolean;
    close(): boolean;
    onUnloading(validationResult: IValidationResultBuilder): void;
    readonly closed: EventHandler<() => void>;
}
export declare class FormViewModelBaseState implements IFormState {
    constructor(form: FormViewModelBase);
    apply(form: FormViewModelBase): boolean;
}
