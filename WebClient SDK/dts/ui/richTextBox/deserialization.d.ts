import { NodeAttributes } from './common';
import { Descendant } from 'slate';
export declare function fromHTML(html: string): Descendant[];
export declare function normalizeTessaHtml(html: string): string;
export declare function fromHTMLElement(element: HTMLElement): Descendant[];
export declare function deserialize(element: HTMLElement): Descendant | null;
export declare function elementToAttributes(element: HTMLElement): Partial<NodeAttributes>;
