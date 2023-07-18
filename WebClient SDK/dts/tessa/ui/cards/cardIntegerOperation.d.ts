import { CardOperationBase } from './cardOperationBase';
import { CardOperationItem } from './cardOperationItem';
import { IViewContext } from '../views';
export declare class CardIntegerOperation<TItem extends CardOperationItem<number>, TContext> extends CardOperationBase<number, TItem, TContext> {
    protected fixDisplayValueAndLimit(cardId: number, displayValue: string, maxLength?: number): string;
    protected tryGetSelectedItemsFromViewContext(context: IViewContext): TItem[] | null;
    protected fixDisplayValue(cardId: number, displayValue: string, _row: ReadonlyMap<string, any>): string;
    protected createOperationItem(cardId: number, displayValue: string, _row: ReadonlyMap<string, any>): TItem;
    protected tryGetItems(context: TContext): TItem[] | null;
}
