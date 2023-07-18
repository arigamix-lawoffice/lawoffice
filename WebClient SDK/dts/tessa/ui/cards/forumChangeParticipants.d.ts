import { CardResponseBase } from 'tessa/cards/service';
import { ValidationResult } from 'tessa/platform/validation';
import { IViewContext } from '../views';
import { ForumOperation } from './forumOperation';
import { IForumOperationContext } from './forumOperationContext';
import { ForumOperationItem } from './forumOperationItem';
export declare class ForumChangeParticipants extends ForumOperation {
    constructor(context: IForumOperationContext);
    protected readonly completedWithMessagesText = "$Forum_UI_Cards_ChangeParticipants_CompletedWithMessages";
    protected readonly completedWithErrorsText = "$Forum_UI_Cards_ChangeParticipants_CompletedWithErrors";
    protected readonly splashSingleItemInitialText = "$Forum_UI_Cards_ChangeParticipants_SplashSingleItemInitial";
    protected readonly splashMultipleItemsInitialText = "$Forum_UI_Cards_ChangeParticipants_SplashMultipleItemsInitial";
    protected readonly splashMultipleItemsProcessingItemText = "$Forum_UI_Cards_ChangeParticipants_SplashMultipleItemsProcessing";
    protected readonly confirmSingleText = "$Forum_UI_Cards_ChangeParticipants_ConfirmSingle";
    protected readonly confirmMultipleText = "$Forum_UI_Cards_ChangeParticipants_ConfirmMultiple";
    protected startAsyncCore(context: IViewContext): Promise<void>;
    protected onCompleted(context: IViewContext, items: ForumOperationItem[], result: ValidationResult): Promise<void>;
    protected processItem(item: ForumOperationItem): Promise<CardResponseBase | null>;
    private processingChangeRole;
}
