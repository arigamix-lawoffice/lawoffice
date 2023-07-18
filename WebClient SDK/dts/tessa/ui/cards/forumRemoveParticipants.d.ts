import { CardResponse } from 'tessa/cards/service';
import { ValidationResult } from 'tessa/platform/validation';
import { ForumOperation, IForumOperationContext } from '.';
import { IViewContext } from '../views';
import { ForumOperationItem } from './forumOperationItem';
export declare class ForumRemoveParticipants extends ForumOperation {
    constructor(context: IForumOperationContext);
    protected readonly completedWithMessagesText = "$Forum_UI_Cards_RemoveParticipants_CompletedWithMessages";
    protected readonly completedWithErrorsText = "$Forum_UI_Cards_RemoveParticipants_CompletedWithErrors";
    protected readonly splashSingleItemInitialText = "$Forum_UI_Cards_RemoveParticipants_SplashSingleItemInitial";
    protected readonly splashMultipleItemsInitialText = "$Forum_UI_Cards_RemoveParticipants_SplashMultipleItemsInitial";
    protected readonly splashMultipleItemsProcessingItemText = "$Forum_UI_Cards_RemoveParticipants_SplashMultipleItemsProcessing";
    protected readonly confirmSingleText = "$Forum_UI_Cards_RemoveParticipant_ConfirmSingle";
    protected readonly confirmMultipleText = "$Forum_UI_Cards_RemoveParticipants_ConfirmMultiple";
    protected processItem(item: ForumOperationItem): Promise<CardResponse | null>;
    protected onCompleted(context: IViewContext, items: ForumOperationItem[], result: ValidationResult): Promise<void>;
    private processingRemoveRole;
}
