import { PageViewport } from 'pdfjs-dist';
import React from 'react';
declare const SvgLayer: React.ForwardRefExoticComponent<{
    pageRef: React.RefObject<HTMLDivElement>;
    upperLayerRef: React.RefObject<HTMLDivElement>;
    viewport?: import("pdfjs-dist/types/src/display/display_utils").PageViewport | undefined;
    pageNumber: number;
} & React.RefAttributes<SVGSVGElement>>;
export { SvgLayer };
export declare function transform(viewport?: PageViewport): string;
