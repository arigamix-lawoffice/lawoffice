import { PageViewport } from 'pdfjs-dist';
import React from 'react';
export declare const SvgEditRect: (props: {
    parentRef: React.RefObject<HTMLElement>;
    pageRef: React.RefObject<HTMLElement>;
    viewport: PageViewport;
    pageNumber: number;
    transformStr?: string;
}) => JSX.Element;
