import { CardOperationItem } from './cardOperationItem';
import { CardOperation } from './cardOperation';
import { IViewContext } from '../views';
import { ViewMetadataSealed, ViewReferenceMetadataSealed } from 'tessa/views/metadata';
import { ValidationResult } from 'tessa/platform/validation';
import { CardResponse } from 'tessa/cards/service';
/**
 * Операция по удалению операций из "Активных операций".
 */
export declare class RemoveOperationOperation extends CardOperation<CardOperationItem, IViewContext> {
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
     * Возвращает признак того, что операция по удалению операций может быть выполнена для заданного контекста.
     *
     * @param {IViewContext} context Контекст операции. Может быть равен <c>null</c>.
     * @returns  <c>true</c>, если операция по удалению операций может быть выполнена для заданного контекста;
     * <c>false</c> в противном случае.
     * @memberof RemoveOperationOperation
     */
    static canProcess(context: IViewContext): boolean;
}
