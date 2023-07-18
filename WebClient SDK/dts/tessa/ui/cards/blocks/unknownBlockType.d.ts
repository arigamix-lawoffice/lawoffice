import { BlockTypeBase } from './blockTypeBase';
import { ICardModel, IBlockViewModel } from '../interfaces';
import { CardTypeForm, CardTypeBlock, CardTypeControl } from 'tessa/cards/types';
export declare class UnknownBlockType extends BlockTypeBase {
    caption: string;
    constructor(key: string);
    static get blockClass(): string;
    protected createBlockCore(block: CardTypeBlock, _form: CardTypeForm, _parentControl: CardTypeControl | null, _model: ICardModel): IBlockViewModel;
}
