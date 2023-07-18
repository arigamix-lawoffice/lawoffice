/// <reference types="react" />
import { PDFFont, PDFDict, PDFContext } from 'pdf-lib';
import { Annotation } from '../types';
export declare const SaveButton: (props: {
    onSave: (fileName: string, byte: Uint8Array, annotations: Annotation[]) => void;
    documentId: string;
}) => JSX.Element;
export interface CreateFreeTextAnnotationParams {
    rotate: number;
    fontSize: number;
    pdfFont: PDFFont;
    width: number;
    height: number;
    x: number;
    y: number;
    date?: Date;
    userName?: string;
    userId?: string;
    clr: number[];
}
export declare const createFreeTextAnnotation: (context: PDFContext, text: string, params: CreateFreeTextAnnotationParams) => Promise<{
    annotation: PDFDict;
    Rect: number[];
}>;
