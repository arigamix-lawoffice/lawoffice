import { Node, BaseEditor } from 'slate';
import { ImageEditor, FileDataHandler, ImageDataHandler, RichTextBoxAttachment } from './index';
import { CoreEditor } from './withCore';
import { NormalizingEditor } from './withImportNormalization';
export interface ClipboardEditor extends BaseEditor, ImageEditor, NormalizingEditor, CoreEditor {
    pasteAsText: boolean;
    attachFile: FileDataHandler;
    attachImage: ImageDataHandler;
    insertHtml: (editor: ClipboardEditor, html: string) => void;
    pasteFragment: (editor: CoreEditor, fragment?: Node[]) => void;
    onAddAttachments?: (attachments: RichTextBoxAttachment[]) => boolean;
}
export declare function withClipboard<T extends BaseEditor>(e: T): T & ClipboardEditor;
