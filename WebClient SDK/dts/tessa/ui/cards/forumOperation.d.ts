import { IViewContext } from '../views';
import { CardOperation } from './cardOperation';
import { IForumOperationContext } from './forumOperationContext';
import { ForumOperationItem } from './forumOperationItem';
export declare class ForumOperation extends CardOperation<ForumOperationItem, IViewContext> {
    constructor(context: IForumOperationContext);
    protected operationContext: IForumOperationContext;
    protected createOperationItem(cardId: guid, displayValue: string, row: ReadonlyMap<string, any>): ForumOperationItem;
    static canProcess(context: IViewContext): boolean;
}
