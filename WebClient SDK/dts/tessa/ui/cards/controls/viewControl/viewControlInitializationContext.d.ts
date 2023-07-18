import type { ViewControlViewModel } from './viewControlViewModel';
import { ICardModel } from '../../interfaces';
import { IStorage } from 'tessa/platform/storage';
export declare class ViewControlInitializationContext {
    readonly controlViewModel: ViewControlViewModel;
    readonly model: ICardModel;
    readonly controlSettings: IStorage;
    constructor(controlViewModel: ViewControlViewModel, model: ICardModel, controlSettings: IStorage);
}
