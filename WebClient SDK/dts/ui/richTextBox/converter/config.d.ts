import React from 'react';
import { HtmlFormat, List } from '..';
export interface IHtmlConversionConfig {
    supportedTags: string[];
    maxRetries?: number;
    rebuildLists?: boolean;
    debug?: boolean;
    preprocess?: (root: Element) => boolean;
    elementToStyle?: (e: Element) => React.CSSProperties | undefined;
    elementToMargin?: (e: Element) => number;
    isList?: (e: Element) => List | undefined;
    isListInline?: (e: Element) => boolean;
    normalizeStyleValues?: (e: HTMLElement) => boolean;
    convertElement?: (e: Element) => boolean;
    normalizeNode?: (e: ChildNode) => boolean;
    normalizeStyle?: (e: HTMLElement) => boolean;
    onUnwrap?: (e: ChildNode) => void;
    postprocess?: (root: Element) => boolean;
}
export declare const defaultConfig: IHtmlConversionConfig;
export declare function getConfig(format: HtmlFormat): IHtmlConversionConfig;
