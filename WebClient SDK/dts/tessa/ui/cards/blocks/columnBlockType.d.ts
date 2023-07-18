import { BlockTypeBase } from './blockTypeBase';
import { ICardModel, IBlockViewModel } from '../interfaces';
import { CardTypeForm, CardTypeBlock, CardTypeControl } from 'tessa/cards/types';
export declare class ColumnBlockType extends BlockTypeBase {
    static get blockClass(): string;
    protected createBlockCore(block: CardTypeBlock, form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IBlockViewModel;
}
