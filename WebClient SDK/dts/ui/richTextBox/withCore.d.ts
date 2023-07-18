/// <reference types="react" />
import { BaseEditor } from 'slate';
import { ReactEditor } from 'slate-react';
import { KeyHandlingEditor } from './withKeyHandling';
export interface CoreEditor extends BaseEditor, KeyHandlingEditor {
    deleteSelection: (editor: CoreEditor) => void;
    moveFocusToStart: (editor: CoreEditor) => void;
    moveFocusToEnd: (editor: CoreEditor) => void;
    moveCursorForwardInline: (editor: CoreEditor, distance: number) => void;
    insertParagraphAtDocumentStart: (editor: CoreEditor) => void;
    insertParagraphAtDocumentEnd: (editor: CoreEditor) => void;
    forceBrowserToReverseSelection: boolean;
    fixBrowserSelection: (editor: CoreEditor & ReactEditor) => void;
    removeById: (editor: CoreEditor, id: string) => void;
    clearContent: (editor: CoreEditor) => void;
    insertSoftLineBreak: (editor: CoreEditor) => void;
    onCut(editor: CoreEditor, event: React.ClipboardEvent): void;
    onPaste(editor: CoreEditor, event: React.ClipboardEvent): void;
    onDOMInputEvent(editor: CoreEditor, event: InputEvent): boolean;
}
export declare function withCore<T extends BaseEditor>(e: T): T & CoreEditor;
