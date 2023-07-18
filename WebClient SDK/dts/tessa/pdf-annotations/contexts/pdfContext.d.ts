import { PDFDocument } from 'pdf-lib';
import { PDFDocumentProxy } from 'pdfjs-dist';
import React from 'react';
declare type State = {
    pdfDocument?: PDFDocumentProxy;
    pdfLibDoc?: PDFDocument;
};
declare type ContextType = [State, React.Dispatch<React.SetStateAction<State>>];
declare function usePdf(): ContextType;
declare function PdfProvider(props: {
    children: JSX.Element;
}): JSX.Element;
export { PdfProvider, usePdf };
