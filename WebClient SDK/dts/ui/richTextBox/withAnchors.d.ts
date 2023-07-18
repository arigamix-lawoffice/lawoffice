import { Node, BaseEditor } from 'slate';
import { KeyHandlingEditor } from './withKeyHandling';
export interface AnchorEditor extends BaseEditor, KeyHandlingEditor {
    hasAnchors: (element: Node) => boolean;
}
export declare function withAnchors<T extends BaseEditor>(e: T): T & AnchorEditor;
