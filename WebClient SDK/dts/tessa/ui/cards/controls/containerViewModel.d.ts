import { CardTypeTabControl, CardTypeControl } from 'tessa/cards/types';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { TabSelectedContext } from '../tabSelectedEventArgs';
import { ControlViewModelBase } from './controlViewModelBase';
import { IBlockViewModel, ICardModel, IFormWithBlocksViewModel } from '../interfaces';
export declare class ContainerViewModel extends ControlViewModelBase {
    constructor(control: CardTypeTabControl, parentControl: CardTypeControl | null, model: ICardModel);
    private _form;
    get form(): IFormWithBlocksViewModel;
    set form(value: IFormWithBlocksViewModel);
    get isEmpty(): boolean;
    getNestedVisibleBlocks(): Iterable<IBlockViewModel>;
    onUnloading(validationResult: ValidationResultBuilder): void;
    notifyTabSelected(context: TabSelectedContext): Promise<void>;
    notifyTabDeselected(context: TabSelectedContext): Promise<void>;
}
