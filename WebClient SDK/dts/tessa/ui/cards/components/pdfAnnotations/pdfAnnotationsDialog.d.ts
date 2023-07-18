import * as React from 'react';
import type { AnnotationEditorViewModel, EditorMode } from 'tessa/pdf-annotations';
import type { Annotation } from 'tessa/pdf-annotations/types';
export declare class PdfAnnotationDialogViewModel {
    fileName: string;
    onSave: (fileName: string, bytes: Uint8Array, annotations: Annotation[]) => void;
    fileBytes: ArrayBuffer;
    onInit?: ((annEditorVM: AnnotationEditorViewModel) => void) | undefined;
    private _annotationsEditorViewModel;
    editorMode: EditorMode;
    set annotationsEditorViewModel(annsVM: AnnotationEditorViewModel);
    get annotationsEditorViewModel(): AnnotationEditorViewModel;
    constructor(fileName: string, onSave: (fileName: string, bytes: Uint8Array, annotations: Annotation[]) => void, fileBytes: ArrayBuffer, editorMode: EditorMode, onInit?: ((annEditorVM: AnnotationEditorViewModel) => void) | undefined);
}
interface PdfAnnotationDialogProps {
    viewModel: PdfAnnotationDialogViewModel;
    onClose: () => void;
}
export declare class PdfAnnotationDialog extends React.Component<PdfAnnotationDialogProps> {
    private init;
    render(): JSX.Element;
    private handleCloseForm;
}
export {};
