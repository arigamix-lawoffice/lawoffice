import React from 'react';
import { BaseEditor } from 'slate';
import { CursorPositionType, InlineSelectionType, List, SelectionType, TessaEditor } from '.';
export declare type SelectionInfo = {
    cursorPosition: CursorPositionType;
    inlineSelectionType?: InlineSelectionType;
    isFocusInBlock: boolean;
    isReadOnly: boolean;
    listType?: List;
    selectionType: SelectionType;
};
export declare type KeyInfo = {
    code: string;
    shift: boolean;
    ctrl: boolean;
    alt: boolean;
};
export interface KeyHandlingEditor extends BaseEditor {
    selectionInfo?: SelectionInfo;
    keyInfo?: KeyInfo;
    onArrowLeft(editor: TessaEditor): boolean;
    onArrowRight(editor: TessaEditor): boolean;
    onArrowDown(editor: TessaEditor): boolean;
    onArrowUp(editor: TessaEditor): boolean;
    onBackspace(editor: TessaEditor): boolean;
    onEnter(editor: TessaEditor): boolean;
    onDelete(editor: TessaEditor): boolean;
    onHome(editor: TessaEditor): boolean;
    onEnd(editor: TessaEditor): boolean;
    onTab(editor: TessaEditor): boolean;
    onKey(editor: TessaEditor): boolean;
    handleKeyDown(editor: TessaEditor, event: React.KeyboardEvent): void;
    resetSelectionInfo(editor: TessaEditor): void;
    resetKeyInfo(editor: TessaEditor): void;
    updateSelectionInfo(editor: TessaEditor): void;
    updateKeyInfo(editor: TessaEditor, event: React.KeyboardEvent): void;
}
export declare function withKeyHandling<T extends BaseEditor>(e: T): T & KeyHandlingEditor;
