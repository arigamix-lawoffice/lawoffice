import { ICardModel, IControlViewModel } from './interfaces';
import { CardControlType } from 'tessa/cards/cardControlType';
import { CardTypeForm, CardTypeBlock, CardTypeControl } from 'tessa/cards/types';
export interface IControlType {
    createControl(control: CardTypeControl, block: CardTypeBlock, form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IControlViewModel;
}
export interface IControlTypeRegistry {
    register(type: CardControlType, factory: () => IControlType): any;
    get(control: CardTypeControl): IControlType;
    getAll(): IControlType[];
}
export declare class ControlTypeRegistry implements IControlTypeRegistry {
    private constructor();
    private static _instance;
    static get instance(): IControlTypeRegistry;
    private registry;
    register(type: CardControlType, factory: () => IControlType): void;
    get(control: CardTypeControl): IControlType;
    getAll(): IControlType[];
}
