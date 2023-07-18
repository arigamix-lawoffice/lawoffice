import { BaseEditor } from 'slate';
import { BlockEditor } from '.';
import { NodeType } from './common';
import { AnchorEditor } from './withAnchors';
import { KeyHandlingEditor } from './withKeyHandling';
import { CoreEditor } from './withCore';
import { BrowserVersionEditor } from './withBrowserVersions';
export interface InlineBlockEditor extends BaseEditor, AnchorEditor, KeyHandlingEditor, CoreEditor, BlockEditor, BrowserVersionEditor {
    toggleInlineBlock: (editor: InlineBlockEditor) => void;
    preventEmptyInlineBlockNormalization: boolean;
    toggleBlock: (editor: BlockEditor, type: NodeType) => void;
}
export declare function withInlineBlock<T extends BaseEditor>(e: T): T & InlineBlockEditor;
