import { CardOperationItem } from './cardOperationItem';
import { CardOperation } from './cardOperation';
import { IViewContext } from '../views';
import { CardResponse } from 'tessa/cards/service';
import { ValidationResult } from 'tessa/platform/validation';
import { ViewMetadataSealed, ViewReferenceMetadataSealed } from 'tessa/views/metadata';
/**
 * Операция по закрытию сессий.
 *
 * @export
 * @class CloseSessionOperation
 * @extends {CardOperation<CardOperationItem, IViewContext>}
 */
export declare class CloseSessionOperation extends CardOperation<CardOperationItem, IViewContext> {
    protected completedWithMessagesText: string;
    protected completedWithErrorsText: string;
    protected splashSingleItemInitialText: string;
    protected splashMultipleItemsInitialText: string;
    protected splashMultipleItemsProcessingItemText: string;
    protected confirmSingleText: string;
    protected confirmMultipleText: string;
    protected tryGetReference(viewMetadata: ViewMetadataSealed): ViewReferenceMetadataSealed | null;
    protected createOperationItem(cardID: string, displayValue: string, _row: ReadonlyMap<string, any>): CardOperationItem;
    protected processItem(item: CardOperationItem): Promise<CardResponse>;
    protected onCompleted(context: IViewContext, items: CardOperationItem[], result: ValidationResult): Promise<void>;
    /**
     * Возвращает признак того, что операция по закрытию сессий может быть выполнена для заданного контекста.
     *
     * @static
     * @param {IViewContext} context Контекст операции. Может быть равен <c>null</c>.
     * @returns <c>true</c>, если операция по закрытию сессий может быть выполнена для заданного контекста; <c>false</c> в противном случае.
     * @memberof CloseSessionOperation
     */
    static canProcess(context: IViewContext): boolean;
}
