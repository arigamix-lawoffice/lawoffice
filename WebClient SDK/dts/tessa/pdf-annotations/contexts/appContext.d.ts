/// <reference types="react" />
import { AnnotationEditorViewModel, EditorMode } from '../types';
export declare function StateProvider(props: {
    children: JSX.Element;
    onChange?: (vm: AnnotationEditorViewModel) => void;
    editorMode?: EditorMode;
    images?: File[];
}): JSX.Element;
