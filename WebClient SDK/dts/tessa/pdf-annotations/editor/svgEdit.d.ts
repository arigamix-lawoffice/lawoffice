import React from 'react';
import { PageViewport } from 'pdfjs-dist';
export declare const SvgEdit: (props: {
    parentRef: React.RefObject<HTMLDivElement>;
    pageRef: React.RefObject<HTMLElement>;
    viewport: PageViewport;
    pageNumber: number;
    transformStr?: string;
}) => JSX.Element | null;
