/// <reference types="react" />
import { Annotation } from '../types';
export declare const HighlightStrikeoutElments: () => JSX.Element;
export declare function toHighlight(addAnnotation: (pageNumber: number, annotation: Annotation) => void): void;
export declare function toStrikeout(addAnnotation: (pageNumber: number, annotation: Annotation) => void): void;
