import { BaseEditor, Range } from 'slate';
import { RichTextBoxAttachment, RichTextBoxAttachmentInnerItem } from './common';
import { CoreEditor } from './withCore';
import { ImageEditor } from './withImages';
import { LinkEditor } from './withLinks';
export interface AttachmentEditor extends BaseEditor {
    insertImageAttachment: (editor: ImageEditor, attachment: RichTextBoxAttachmentInnerItem, range: Range) => void;
    insertImageAttachments: (editor: AttachmentEditor & ImageEditor, attachments: RichTextBoxAttachment[], range: Range) => void;
    removeAttachment: (editor: CoreEditor & LinkEditor, attachment: RichTextBoxAttachment) => void;
}
export declare function withAttachments<T extends BaseEditor>(e: T): T & AttachmentEditor;
