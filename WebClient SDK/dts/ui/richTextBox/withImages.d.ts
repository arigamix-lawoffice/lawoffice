import { Range, BaseEditor } from 'slate';
import { ImageData, RichTextBoxAttachmentInnerItemDuplicate, RichTextBoxAttachment } from './common';
import { KeyHandlingEditor } from './withKeyHandling';
interface DuplicateEntry {
    id: string;
    entries: number;
}
export interface ImageEditor extends BaseEditor, KeyHandlingEditor {
    clearImages: (editor: ImageEditor) => void;
    insertImage: (editor: ImageEditor, image: ImageData, range: Range) => void;
    deleteImage: (editor: ImageEditor, id: string) => void;
    getDuplicateImageEntries(editor: ImageEditor): DuplicateEntry[];
    resolveDuplicateImageEntries(editor: ImageEditor, duplicates: DuplicateEntry[]): RichTextBoxAttachmentInnerItemDuplicate[];
    allowInnerAttachmentDuplicates?: boolean;
    onAddInnerItemDuplicate?: (attachment: RichTextBoxAttachmentInnerItemDuplicate[]) => void;
    onGetAttachments?: () => readonly RichTextBoxAttachment[];
}
export declare function withImages<T extends BaseEditor>(e: T): T & ImageEditor;
export {};
