import { RichTextBoxAttachment, RichTextBoxAttachmentExternalInnerItem, RichTextBoxAttachmentFile, RichTextBoxAttachmentInnerItem } from 'ui/richTextBox';
import { ICardModel } from 'tessa/ui/cards/interfaces';
import { CardGetFileContentRequest, CardGetFileContentResponse } from 'tessa/cards/service';
import { ValidationResult } from 'tessa/platform/validation';
export interface AttachmentItem {
    [key: string]: unknown;
}
export declare function mapItemsToAttachments(items: AttachmentItem[], cardId: guid): RichTextBoxAttachment[];
export declare function serializeAttachments(attachments: RichTextBoxAttachment[]): string;
export declare function mapAttachmentsToItems(attachments: RichTextBoxAttachment[]): AttachmentItem[];
export declare function addAttachmentToFiles(cardModel: ICardModel, attachment: RichTextBoxAttachmentFile | RichTextBoxAttachmentInnerItem): Promise<void>;
export declare function removeAttachmentFromFiles(cardModel: ICardModel, attachment: RichTextBoxAttachment): Promise<void>;
export declare function getAndSaveAttachmentContent(attachment: RichTextBoxAttachmentFile | RichTextBoxAttachmentInnerItem | RichTextBoxAttachmentExternalInnerItem, cardId: string): Promise<ValidationResult>;
export declare function getAttachmentContent(attachment: RichTextBoxAttachmentFile | RichTextBoxAttachmentInnerItem | RichTextBoxAttachmentExternalInnerItem, cardId: string): Promise<CardGetFileContentResponse>;
export declare function getFileContentRequest(attachment: RichTextBoxAttachmentFile | RichTextBoxAttachmentInnerItem | RichTextBoxAttachmentExternalInnerItem, cardId: string): CardGetFileContentRequest;
