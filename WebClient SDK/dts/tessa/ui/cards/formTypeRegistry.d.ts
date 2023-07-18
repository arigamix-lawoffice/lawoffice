import { ICardModel, IFormWithBlocksViewModel, IFormViewModelBase } from './interfaces';
import { CardTypeControl, CardTypeForm } from 'tessa/cards/types';
export interface IFormType {
    createForm(form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IFormWithBlocksViewModel;
    createMainForm(model: ICardModel): IFormViewModelBase;
}
export interface IFormTypeRegistry {
    register(key: string, factory: () => IFormType): void;
    get(form: CardTypeForm): IFormType;
    getAll(): IFormType[];
    createCardForm(orm: CardTypeForm, model: ICardModel): IFormWithBlocksViewModel;
    createMainForm(model: ICardModel): IFormViewModelBase;
}
export declare class FormTypeRegistry implements IFormTypeRegistry {
    private constructor();
    private static _instance;
    static get instance(): IFormTypeRegistry;
    private registry;
    register(key: string, factory: () => IFormType): void;
    get(form: CardTypeForm): IFormType;
    getAll(): IFormType[];
    createCardForm(form: CardTypeForm, model: ICardModel): IFormWithBlocksViewModel;
    createMainForm(model: ICardModel): IFormViewModelBase;
}
