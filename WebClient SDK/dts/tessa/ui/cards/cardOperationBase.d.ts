import { CardOperationItem } from './cardOperationItem';
import { IUIContext } from '../uiContext';
import { IViewContext } from '../views';
import { ValidationResult, IValidationResultBuilder } from 'tessa/platform/validation';
import { CardResponseBase } from 'tessa/cards/service';
import { ITessaView } from 'tessa/views';
import { ViewReferenceMetadataSealed, ViewMetadataSealed } from 'tessa/views/metadata';
export declare abstract class CardOperationBase<TIdentifier, TItem extends CardOperationItem<TIdentifier>, TContext> {
    protected completedWithMessagesText: string;
    protected completedWithErrorsText: string;
    protected confirmSingleText: string;
    protected confirmMultipleText: string;
    protected unknownCardTypeText: string | null;
    protected abstract fixDisplayValue(cardId: TIdentifier, displayValue: string, _row: ReadonlyMap<string, any>): string;
    protected abstract createOperationItem(cardId: TIdentifier, displayValue: string, _row: ReadonlyMap<string, any>): TItem;
    protected abstract tryGetItems(context: TContext): TItem[] | null;
    protected confirm(items: TItem[]): Promise<boolean>;
    protected tryGetReference(viewMetadata: ViewMetadataSealed): ViewReferenceMetadataSealed | null;
    protected performOperation(context: TContext, items: TItem[], currentContext: IUIContext): Promise<ValidationResult>;
    protected processItemAndAddResult(item: TItem, totalResult: IValidationResultBuilder): Promise<void>;
    protected processItem(_item: TItem): Promise<CardResponseBase | null>;
    protected processItems(items: TItem[]): Promise<ValidationResult>;
    protected onStarted(_context: TContext, _items: TItem[]): Promise<void>;
    protected onCompleted(_context: TContext, _items: TItem[], _result: ValidationResult): Promise<void>;
    protected startAsyncCore(context: TContext): Promise<void>;
    protected processCardResponse<TResponse extends CardResponseBase>(item: TItem, validationResult: IValidationResultBuilder, processFunc: (item: TItem) => Promise<TResponse | null>): Promise<TResponse | null>;
    protected addResultToTotalResult(result: ValidationResult, totalResult: IValidationResultBuilder, item: TItem): void;
    protected static canProcessCore(context: IViewContext, canProcessViewFunc?: (context: ITessaView) => boolean): boolean;
    startAsync(context: TContext): Promise<void>;
}
