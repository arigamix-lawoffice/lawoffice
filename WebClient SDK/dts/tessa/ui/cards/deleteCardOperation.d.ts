import { CardOperation } from './cardOperation';
import { DeleteCardOperationItem } from './deleteCardOperationItem';
import { IViewContext } from '../views';
import { CardResponseBase } from 'tessa/cards/service';
import { ValidationResult } from 'tessa/platform/validation';
export declare class DeleteCardOperation extends CardOperation<DeleteCardOperationItem, IViewContext> {
    constructor(withoutBackupOnly: boolean);
    readonly withoutBackupOnly: boolean;
    protected completedWithMessagesText: string;
    protected completedWithErrorsText: string;
    protected unknownCardTypeText: string;
    protected createOperationItem(cardId: guid, displayValue: string, _row: ReadonlyMap<string, any>): DeleteCardOperationItem;
    protected processItem(item: DeleteCardOperationItem): Promise<CardResponseBase | null>;
    protected onStarted(_context: IViewContext, items: DeleteCardOperationItem[]): Promise<void>;
    protected onCompleted(context: IViewContext, items: DeleteCardOperationItem[], result: ValidationResult): Promise<void>;
    static canProcess(context: IViewContext): boolean;
}
