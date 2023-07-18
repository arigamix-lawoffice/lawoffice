/// <reference types="react" />
import { PDFRef } from 'pdf-lib';
import { Annotation, CommentAnnotation, AnnotationEditorViewModel } from '../types';
declare type AnnsState = {
    [key: number]: Annotation[];
};
interface AnnContext {
    annotations: AnnsState;
    getAnnotations: (pageNumber: number) => Promise<Annotation[]>;
    addAnnotation: (pageNumber: number, annotation: Partial<Annotation>) => Annotation;
    getAnnotation: (annotationId: PDFRef | undefined | null) => Annotation | undefined;
    editAnnotation: (annotationId: PDFRef, annotation: Annotation) => Annotation | undefined;
    deleteAnnotation: (annotationId: PDFRef) => boolean;
    deletePageAnnotations: (pageNumber: number) => void;
    getComments: (annotationId: PDFRef) => CommentAnnotation[];
    addComment: (annotationId: PDFRef, content: string) => CommentAnnotation | undefined;
    deleteComment: (annotationId: PDFRef, commentId: PDFRef) => boolean;
    hide: (ids: PDFRef[]) => void;
    unhide: (ids: PDFRef[]) => void;
}
export declare function useAnnotations(): AnnContext;
export declare function AnnotationsProvider(props: {
    children: JSX.Element;
    onChange?: (vm: AnnotationEditorViewModel) => void;
}): JSX.Element;
export {};
