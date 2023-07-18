import { Node, BaseEditor } from 'slate';
import { ReactEditor } from 'slate-react';
import { BlockEditor } from '.';
export interface NormalizingEditor extends BaseEditor, ReactEditor, BlockEditor {
    normalizeFragment: (editor: NormalizingEditor, fragment: Node[]) => void;
}
export declare function withImportNormalization<T extends NormalizingEditor>(e: T): T & NormalizingEditor & ReactEditor;
