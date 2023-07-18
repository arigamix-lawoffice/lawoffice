import { Descendant } from 'slate';
export declare type TagGenerator = (node: Descendant, attributes: string, content: string) => string;
export declare type AttributeGenerator = (node: Descendant) => string;
export declare function adaptRawHtmlToTessa(html: string): string;
export declare function toHTML(nodes: Descendant[]): string;
export declare function serialize(node: Descendant, nodeToTag: TagGenerator, nodeToAttributes: AttributeGenerator): string;
export declare function escapeAngledBrackets(html: string): string;
