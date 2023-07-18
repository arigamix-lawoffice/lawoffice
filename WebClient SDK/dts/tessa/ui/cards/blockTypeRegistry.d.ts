import { ICardModel, IBlockViewModel } from './interfaces';
import { CardTypeForm, CardTypeBlock, CardTypeControl } from 'tessa/cards/types';
export interface IBlockType {
    createBlock(block: CardTypeBlock, form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IBlockViewModel;
}
export interface IBlockTypeRegistry {
    register(key: string, factory: () => IBlockType): any;
    get(block: CardTypeBlock): IBlockType;
    getAll(): IBlockType[];
}
export declare class BlockTypeRegistry implements IBlockTypeRegistry {
    private constructor();
    private static _instance;
    static get instance(): IBlockTypeRegistry;
    private registry;
    register(key: string, factory: () => IBlockType): void;
    get(block: CardTypeBlock): IBlockType;
    getAll(): IBlockType[];
}
