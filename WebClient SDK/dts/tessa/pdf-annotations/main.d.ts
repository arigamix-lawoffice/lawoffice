/// <reference types="react" />
import './main.css';
import pdfjsStatic from 'pdfjs-dist';
import 'pdfjs-dist/web/pdf_viewer.css';
import { Annotation, AnnotationEditorViewModel, EditorMode } from './types';
declare global {
    var pdfjsLib: typeof pdfjsStatic;
}
declare function App(props: IAppProps): JSX.Element;
export default App;
export interface IAppProps {
    documentId: string;
    fileBytes: ArrayBuffer;
    userName: string;
    userId: string;
    onClose: () => void;
    onSave: (fileName: string, bytes: Uint8Array, annotations: Annotation[]) => void;
    editorMode: EditorMode;
    onChange?: (vm: AnnotationEditorViewModel) => void;
    images?: File[];
}
