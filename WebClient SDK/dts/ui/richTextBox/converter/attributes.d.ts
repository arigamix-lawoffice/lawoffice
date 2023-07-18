import { List } from '..';
export declare const listAttribute = "is-list";
export declare const listTypeAttribute = "list-type";
export declare const depthAttribute = "depth";
export declare const marginAttribute = "margin";
export declare function isInline(e: Element): boolean;
export declare function isPartOfList(e: Element): boolean;
export declare function getListType(e: Element): List | undefined;
export declare function getDepth(e: Element): number;
export declare function getMargin(e: Element): number;
