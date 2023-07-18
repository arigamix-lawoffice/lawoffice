import { BaseEditor, Node } from 'slate';
export interface SmartSerializationEditor extends BaseEditor {
    serializationCache: Map<Node, string>;
    serializationDirtyNodes: Map<Node, Node>;
    toHtml: () => string;
}
export declare function withSmartSerialization<T extends BaseEditor>(e: T): T & SmartSerializationEditor;
