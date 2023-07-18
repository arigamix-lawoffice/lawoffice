import { IControlType } from '../controlTypeRegistry';
import { ICardModel, IControlViewModel } from '../interfaces';
import { CardTypeForm, CardTypeBlock, CardTypeControl } from 'tessa/cards/types';
export interface CreateControlDelegate {
    (control: CardTypeControl, block: CardTypeBlock, form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IControlViewModel;
}
export declare abstract class ControlTypeBase implements IControlType {
    private _createControlDelegate;
    constructor(_createControlDelegate?: CreateControlDelegate | null);
    protected abstract createControlCore(control: CardTypeControl, block: CardTypeBlock, form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IControlViewModel;
    createControl(control: CardTypeControl, block: CardTypeBlock, form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel, skipInitialization?: boolean): IControlViewModel;
    protected controlInitialization(viewModel: IControlViewModel, model: ICardModel, skipInitialization: boolean): void;
}
