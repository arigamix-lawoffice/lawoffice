import { PDFRef } from 'pdf-lib';
import { PageViewport } from 'pdfjs-dist';
export declare const formatPDFDate: (date: Date | undefined) => string | undefined;
export declare function scaleDown<T extends Partial<DOMRect>>(viewport: PageViewport, rect: T): T;
export declare function pointIntersectsRect(x: number, y: number, rect: {
    top: number;
    bottom: number;
    left: number;
    right: number;
}): boolean;
/**
 * Normalize a color value
 *
 * @param {String} color The color to normalize
 * @return {String}
 */
export declare function normalizeColor(color: string): string;
export declare const getRandomPdfRef: () => PDFRef;
export declare const parsePDFRef: <T = string | null | undefined>(ref: T) => PDFRef | undefined;
export declare function getMetadata(svg: SVGElement | HTMLDivElement): {
    pageNumber: number;
    viewport: PageViewport;
};
