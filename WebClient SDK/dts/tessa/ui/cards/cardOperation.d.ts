import { CardOperationBase } from './cardOperationBase';
import { CardOperationItem } from './cardOperationItem';
import { IViewContext } from '../views';
export declare abstract class CardOperation<TItem extends CardOperationItem, TContext> extends CardOperationBase<guid, TItem, TContext> {
    protected fixDisplayValueAndLimit(cardId: guid, displayValue: string, maxLength?: number): string;
    protected tryGetSelectedItemsFromViewContext(context: IViewContext): TItem[] | null;
    protected fixDisplayValue(cardId: guid, displayValue: string, _row: ReadonlyMap<string, any>): string;
    protected createOperationItem(cardId: guid, displayValue: string, _row: ReadonlyMap<string, any>): TItem;
    protected tryGetItems(context: TContext): TItem[] | null;
}
