import { PDFRef } from 'pdf-lib';
import { PDFDocumentProxy } from 'pdfjs-dist';
export declare enum Scale {
    Auto = -1,
    _50 = 0.5,
    _100 = 1,
    _133 = 1.33,
    _150 = 1.5,
    _200 = 2
}
export declare const OverContentWrapperId = "over-content-wrapper";
export declare enum Tool {
    cursor = "cursor",
    draw = "draw",
    text = "text",
    point = "point",
    area = "area",
    highlight = "highlight",
    strikeout = "strikeout",
    image = "image"
}
export declare enum COLORS {
    Black = "#000000",
    Red = "#EF4437",
    Pink = "#E71F63",
    Purple = "#8F3E97",
    'Deep Purple' = "#65499D",
    Indigo = "#4554A4",
    Blue = "#2083C5",
    'Light Blue' = "#35A4DC",
    Cyan = "#09BCD3",
    Teal = "#009688",
    Green = "#43A047",
    'Light Green' = "#8BC34A",
    Yellow = "#FDC010",
    Orange = "#F8971C",
    'Deep Orange' = "#F0592B",
    'Light Pink' = "#F06291"
}
export declare const TextSizes: number[];
export declare const BORDER_COLOR = "#00BFFF";
export declare const OVERLAY_BORDER_SIZE = 3;
export interface AnnotationClass {
    class: AnnotationClassType;
    /**
     * annotation id
     */
    id: PDFRef;
    /**
     * parent annotation id
     */
    parentId?: PDFRef;
    state: AnnotationState;
    content?: string;
    userName?: string;
    userId?: string;
    page: number;
    date?: Date;
}
export interface AnnotationAnnotation extends AnnotationClass {
    class: AnnotationClassType.Annotation;
    type: AnnotationType;
    color?: string;
    size?: number;
    documentId?: string;
    strokeWidth?: number;
    hidden?: boolean;
    comments: CommentAnnotation[];
}
export interface TextboxAnnotation extends AnnotationAnnotation {
    type: AnnotationType.Textbox;
    x: number;
    y: number;
    width: number;
    height: number;
}
export interface ImageAnnotation extends AnnotationAnnotation {
    type: AnnotationType.Image;
    x: number;
    y: number;
    width: number;
    height: number;
    fileName: string;
    bytes: ArrayBuffer;
    opacity: number;
    dataUrl: string | ArrayBuffer | null;
    /** for simultaneous on different pages editing */
    sharingId?: string;
}
export interface HighlightAnnotation extends AnnotationAnnotation {
    type: AnnotationType.Highlight;
    rectangles: AnnotationRectangle[];
}
export interface DrawingAnnotation extends AnnotationAnnotation {
    type: AnnotationType.Drawing;
    lines: number[][];
    width: number;
}
export interface AreaAnnotation extends AnnotationAnnotation {
    type: AnnotationType.Area;
    x: number;
    y: number;
    width: number;
    height: number;
}
export interface StrikeoutAnnotation extends AnnotationAnnotation {
    type: AnnotationType.Strikeout;
    rectangles: AnnotationRectangle[];
}
export declare type Annotation = TextboxAnnotation | HighlightAnnotation | DrawingAnnotation | AreaAnnotation | StrikeoutAnnotation | ImageAnnotation;
export interface CommentAnnotation extends AnnotationClass {
    class: AnnotationClassType.Comment;
}
export declare enum AnnotationClassType {
    Annotation = "Annotation",
    Comment = "Comment"
}
export interface AnnotationRectangle {
    x: number;
    y: number;
    width: number;
    height: number;
}
export interface PdfPageAnnotationCommon {
    annotationFlags: number;
    borderStyle: {
        width: number;
        style: number;
        dashArray: number[];
        horizontalCornerRadius: number;
        verticalCornerRadius: number;
    };
    color: PdfPageAnnotationColor;
    backgroundColor: PdfPageAnnotationColor | null;
    borderColor: PdfPageAnnotationColor | null;
    rotation: number;
    contentsObj: PdfPageAnnotationContentObj;
    hasAppearance: boolean;
    id: string;
    modificationDate: string;
    rect: number[];
    subtype: string;
    hasOwnCanvas: boolean;
    titleObj: PdfPageAnnotationContentObj;
    creationDate: string | null;
    hasPopup: boolean;
    annotationType: number;
}
export interface PdfPageAnnotationInk extends PdfPageAnnotationCommon {
    subtype: 'Ink';
    inkLists: {
        x: number;
        y: number;
    }[][];
}
export interface PdfPageAnnotationFreeText extends PdfPageAnnotationCommon {
    subtype: 'FreeText';
    textContent?: string[];
}
export interface PdfPageAnnotationText extends PdfPageAnnotationCommon {
    subtype: 'Text';
}
export interface PdfPageAnnotationSquare extends PdfPageAnnotationCommon {
    subtype: 'Square';
}
export interface PdfPageAnnotationHighlight extends PdfPageAnnotationCommon {
    subtype: 'Highlight';
    quadPoints: FixedSizeArray<4, {
        x: number;
        y: number;
    }>[];
}
export interface PdfPageAnnotationStrikeout extends PdfPageAnnotationCommon {
    subtype: 'StrikeOut';
    quadPoints: FixedSizeArray<4, {
        x: number;
        y: number;
    }>[];
}
export declare type PdfPageAnnotation = PdfPageAnnotationInk | PdfPageAnnotationFreeText | PdfPageAnnotationHighlight | PdfPageAnnotationSquare | PdfPageAnnotationStrikeout | PdfPageAnnotationText;
declare type PdfPageAnnotationColor = {
    '0': number;
    '1': number;
    '2': number;
};
declare type PdfPageAnnotationContentObj = {
    str: string;
    dir: 'ltr';
};
declare type FixedSizeArray<N extends number, T> = N extends 0 ? never[] : {
    0: T;
    length: N;
} & ReadonlyArray<T>;
export declare enum AnnotationState {
    Stored = 1,
    Inserted = 2,
    Deleted = 3,
    Modified = 4
}
export declare enum AnnotationType {
    Textbox = "textbox",
    Highlight = "highlight",
    Drawing = "drawing",
    Area = "area",
    Strikeout = "strikeout",
    Image = "image"
}
export interface AnnotationEditorViewModel {
    getAnnotations: () => Promise<{
        [page: number]: Annotation[];
    }>;
    addAnnotation: (ann: Partial<Annotation>) => Annotation;
    deleteAnnotation: (ann: Annotation) => void;
    editAnnotation: (ann: Annotation) => void;
    pdfDocument: PDFDocumentProxy;
}
export declare enum EditorMode {
    ViewAnnotations = 1,
    EditAnnotations = 2,
    ImageAdding = 4,
    ViewAndImage = 5,
    EditAndImage = 6
}
export declare const EditorModeStr: {
    [x: number]: string;
};
export declare const hasAllFlags: (value: EditorMode | number | undefined, ...flags: EditorMode[]) => boolean;
export declare const hasAnyFlag: (value: EditorMode | number | undefined, ...flags: EditorMode[]) => boolean;
export declare const AppClassName = "App";
export declare const ToolbarClassName = "toolbar";
export {};
