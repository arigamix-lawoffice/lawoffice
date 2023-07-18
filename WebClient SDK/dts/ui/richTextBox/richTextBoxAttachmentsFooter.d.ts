import React from 'react';
import { RichTextBoxAttachment } from './common';
export interface RichTextBoxAttachmentsFooterProps {
    maxLengthFileCaption?: number;
    attachments: ReadonlyArray<RichTextBoxAttachment>;
    onRemoveAttachments?(attachment: RichTextBoxAttachment): void;
    onChangeAttachment?(attachment: RichTextBoxAttachment): void;
    onInsertLink?(attachment: RichTextBoxAttachment): void;
    onInsertFile?(attachment: RichTextBoxAttachment): void;
    onPreviewFileAttachment?(attachment: RichTextBoxAttachment): void;
    onOpenFileAttachment?(id: string): void;
    onOpenLinkAttachment?(id: string): void;
    readOnlyMode: boolean;
    maxHeight: string;
}
export declare const RichTextBoxAttachmentsFooter: React.FC<RichTextBoxAttachmentsFooterProps>;
