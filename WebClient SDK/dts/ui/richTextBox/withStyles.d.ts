import { BaseEditor } from 'slate';
import { MarkType } from './common';
import { KeyHandlingEditor } from './withKeyHandling';
export declare type StyleValue = string | number | boolean | undefined;
export interface StyleEditor extends BaseEditor, KeyHandlingEditor {
    isMarkActive: (editor: StyleEditor, type: MarkType) => boolean;
    marksHaveSameValue: (editor: StyleEditor, type: MarkType, value: StyleValue) => boolean;
    canApplyStyle: (Editor: StyleEditor) => boolean;
    toggleStyle: (editor: StyleEditor, type: MarkType, onValue: StyleValue) => void;
    toggleParagraphStyle: (editor: StyleEditor, type: MarkType, onValue: StyleValue) => void;
    setStyle: (editor: StyleEditor, type: MarkType, value: StyleValue) => void;
    toggleBold: (editor: StyleEditor) => void;
    toggleItalic: (editor: StyleEditor) => void;
    toggleUnderline: (editor: StyleEditor) => void;
    toggleStrikethrough: (editor: StyleEditor) => void;
    toggleTextAlignment: (editor: StyleEditor, value?: string) => void;
    setColor: (editor: StyleEditor, color?: string) => void;
    setBackgroundColor: (editor: StyleEditor, color?: string) => void;
    setBlockBackgroundColor: (editor: StyleEditor, color?: string) => void;
    setFontSize: (editor: StyleEditor, size?: string) => void;
    toggleMark: (editor: StyleEditor, type: MarkType, value?: string | boolean) => void;
    clearStyle: (editor: StyleEditor) => void;
}
export declare function withStyles<T extends BaseEditor>(e: T): T & StyleEditor;
