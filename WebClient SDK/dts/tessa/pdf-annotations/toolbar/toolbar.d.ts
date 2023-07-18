/// <reference types="react" />
import { Annotation, EditorMode } from '../types';
export declare const Toolbar: (props: {
    documentId: string;
    onClose: () => void;
    onSave: (fileName: string, byte: Uint8Array, annotations: Annotation[]) => void;
    editorMode: EditorMode;
}) => JSX.Element;
