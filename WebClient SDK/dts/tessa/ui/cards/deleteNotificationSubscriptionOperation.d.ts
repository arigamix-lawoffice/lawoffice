import { CardOperation } from './cardOperation';
import { CardOperationItem } from './cardOperationItem';
import { IViewContext } from '../views';
import { ValidationResult } from 'tessa/platform/validation';
import { ViewMetadataSealed, ViewReferenceMetadataSealed } from 'tessa/views/metadata';
export declare class DeleteNotificationSubscriptionOperation extends CardOperation<CardOperationItem, IViewContext> {
    protected confirmSingleText: string;
    protected confirmMultipleText: string;
    protected tryGetReference(viewMetadata: ViewMetadataSealed): ViewReferenceMetadataSealed | null;
    protected processItems(items: CardOperationItem[]): Promise<ValidationResult>;
    protected onCompleted(context: IViewContext, items: CardOperationItem[], result: ValidationResult): Promise<void>;
    static canProcess(context: IViewContext): boolean;
}
