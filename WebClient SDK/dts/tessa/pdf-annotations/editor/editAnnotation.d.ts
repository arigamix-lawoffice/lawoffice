import { PageViewport } from 'pdfjs-dist';
import React from 'react';
export declare const EditAnnotation: (props: {
    parentRef: React.RefObject<HTMLElement>;
    svgLayer: React.RefObject<SVGSVGElement>;
    viewport: PageViewport;
    pageNumber: number;
}) => JSX.Element;
export declare function getAnnotationRect(el: Element): DOMRect;
