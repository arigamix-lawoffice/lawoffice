import { IBlockType } from '../blockTypeRegistry';
import { ICardModel, IBlockViewModel } from '../interfaces';
import { CardTypeForm, CardTypeBlock, CardTypeControl } from 'tessa/cards/types';
export declare abstract class BlockTypeBase implements IBlockType {
    protected abstract createBlockCore(block: CardTypeBlock, form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel): IBlockViewModel;
    createBlock(block: CardTypeBlock, form: CardTypeForm, parentControl: CardTypeControl | null, model: ICardModel, skipInitialization?: boolean): IBlockViewModel;
}
