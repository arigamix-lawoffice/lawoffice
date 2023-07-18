import { Path, BaseEditor } from 'slate';
import { CursorPositionType } from '.';
import { NodeType, List } from './common';
import { KeyHandlingEditor } from './withKeyHandling';
import { StyleEditor } from './withStyles';
export interface WordlikeListEditor extends BaseEditor, StyleEditor, KeyHandlingEditor {
    splitListItem: (editor: WordlikeListEditor) => void;
    isInList: (editor: WordlikeListEditor) => List | undefined;
    getListNesting: (editor: WordlikeListEditor, at: Path) => number;
    toggleListBlock: (editor: WordlikeListEditor, type: NodeType) => void;
    handleBackspaceInList: (editor: WordlikeListEditor, cursorPosition: CursorPositionType) => void;
    increaseIndent: (editor: WordlikeListEditor, listType: List) => void;
    toggleBlock: (editor: BaseEditor, type: NodeType) => void;
    maxListNestingDepth: number;
}
export declare function withWordlikeLists<T extends BaseEditor>(e: T): T & WordlikeListEditor;
