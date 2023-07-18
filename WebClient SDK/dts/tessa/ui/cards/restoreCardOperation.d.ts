import { IViewContext } from '../views';
import { CardOperation } from './cardOperation';
import { RestoreCardOperationItem } from './restoreCardOperationItem';
import { ValidationResult } from 'tessa/platform/validation';
/**
 * Операция по восстановлению удалённых карточек.
 */
export declare class RestoreCardOperation extends CardOperation<RestoreCardOperationItem, IViewContext> {
    protected completedWithMessagesText: string;
    protected completedWithErrorsText: string;
    protected splashSingleItemInitialText: string;
    protected splashMultipleItemsInitialText: string;
    protected splashMultipleItemsProcessingItemText: string;
    protected confirmSingleText: string;
    protected confirmMultipleText: string;
    protected createOperationItem(cardID: string, displayValue: string, _row: Map<string, any>): RestoreCardOperationItem;
    protected processItem(item: RestoreCardOperationItem): Promise<import("tessa/cards/service").CardDeleteResponse>;
    protected onCompleted(context: IViewContext, items: RestoreCardOperationItem[], result: ValidationResult): Promise<void>;
    /**
     * Возвращает признак того, что операция по восстановлению удалённых карточек может быть выполнена для заданного контекста.
     *
     * @param {*} context Контекст операции. Может быть равен <c>null</c>.
     * @returns booleam
     * @memberof RestoreCardOperation
     */
    static canProcess(context: IViewContext): boolean;
}
