import { Point, Range, BaseEditor } from 'slate';
import { StyleEditor } from '.';
import { NodeType } from './common';
import { AnchorEditor } from './withAnchors';
import { KeyHandlingEditor } from './withKeyHandling';
import { CoreEditor } from './withCore';
export interface BlockEditor extends BaseEditor, AnchorEditor, KeyHandlingEditor, CoreEditor, StyleEditor {
    isBlockActive: (editor: BlockEditor, type: NodeType) => boolean;
    getBlockNesting: (editor: BlockEditor, point: Point, type: NodeType) => number;
    unwrapParentBlock: (editor: BlockEditor, at: Point | Range) => boolean;
    removeBlockWithContent: (editor: BlockEditor, point: Point, type: NodeType) => void;
    wrapBlock: (editor: BlockEditor, type: NodeType, className?: string) => void;
    wrapInForumBlock: (editor: BlockEditor) => void;
    wrapInMonospaceBlock: (editor: BlockEditor) => void;
    toggleBlock: (editor: BlockEditor, type: NodeType) => void;
}
export declare function withBlocks<T extends BaseEditor>(e: T): T & BlockEditor;
