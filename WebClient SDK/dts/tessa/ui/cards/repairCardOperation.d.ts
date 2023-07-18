import { CardOperation } from './cardOperation';
import { RepairCardOperationItem } from './repairCardOperationItem';
import { IViewContext } from 'tessa/ui/views';
import { Card } from 'tessa/cards';
import { IValidationResultBuilder, ValidationResult } from 'tessa/platform/validation';
import { IUIContext } from 'tessa/ui/uiContext';
export declare abstract class RepairCardOperation<TItem extends RepairCardOperationItem, TContext extends IViewContext> extends CardOperation<TItem, TContext> {
    protected abstract getCardToRepairData(mainCard: Card): string;
    protected abstract setCardToRepairData(mainCard: Card, repairedCardData: string): any;
    protected typeId: guid | null;
    protected completedWithMessagesText: string;
    protected completedWithErrorsText: string;
    protected confirmSingleText: string;
    protected confirmMultipleText: string;
    protected createOperationItem(cardId: guid, displayValue: string, _row: ReadonlyMap<string, any>): TItem;
    protected processItemAndAddResult(item: TItem, totalResult: IValidationResultBuilder): Promise<void>;
    protected onCompleted(context: TContext, items: TItem[], result: ValidationResult): Promise<void>;
    startFromCardContext(context: IUIContext): Promise<void>;
}
