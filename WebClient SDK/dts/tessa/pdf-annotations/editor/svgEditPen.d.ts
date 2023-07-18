import { PageViewport } from 'pdfjs-dist';
import React from 'react';
export declare const SvgEditPen: (props: {
    parentRef: React.RefObject<HTMLDivElement>;
    pageRef: React.RefObject<HTMLElement>;
    viewport: PageViewport;
    pageNumber: number;
    transformStr?: string;
}) => JSX.Element | null;
