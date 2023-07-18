import { FunctionComponent } from 'react';
import { LinkInfo, RichTextBoxAttachment } from './common';
import { DialogType } from 'tessa/ui';
export interface RichTextBoxAttachmentsToolbarProps {
    showFileDialog(dialogType: DialogType): Promise<readonly File[] | File | null>;
    showLinkDialog(): Promise<LinkInfo | null>;
    onAddFiles(attachments: RichTextBoxAttachment[]): void;
    onAddLink(attachments: RichTextBoxAttachment[]): void;
    title?: string;
}
export declare const RichTextBoxAttachmentsToolbar: FunctionComponent<RichTextBoxAttachmentsToolbarProps>;
