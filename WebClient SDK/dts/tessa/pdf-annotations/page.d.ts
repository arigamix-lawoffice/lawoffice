import React from 'react';
import { PageViewport, PDFDocumentProxy } from 'pdfjs-dist';
import { Scale } from './types';
declare const Page: React.MemoExoticComponent<(props: {
    pageNumber: number;
    pdfDocument: PDFDocumentProxy;
    setAnnotationsAndViewport: (viewport: PageViewport) => Promise<void>;
    scale: Scale;
}) => JSX.Element>;
export { Page };
