import { Range, BaseEditor } from 'slate';
import { ReactEditor } from 'slate-react';
import { KeyHandlingEditor } from './withKeyHandling';
export interface LinkEditor extends BaseEditor, ReactEditor, KeyHandlingEditor {
    insertLink: (editor: LinkEditor, range: Range, id: string, caption: string, href: string) => void;
    deleteLinksById: (editor: LinkEditor, id: string) => void;
    convertTextToLinks: (editor: LinkEditor, range: Range) => void;
    unwrapLinksById: (editor: LinkEditor, id: string) => void;
    unwrapLinksByHref: (editor: LinkEditor, href: string) => void;
}
export declare function withLinks<T extends BaseEditor>(e: T): T & LinkEditor;
