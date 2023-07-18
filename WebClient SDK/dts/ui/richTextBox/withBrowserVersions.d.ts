import { BaseEditor } from 'slate';
export interface BrowserVersionEditor extends BaseEditor {
    isMobile: boolean;
    isAndroid: boolean;
    isMacOsSafari: boolean;
}
export declare function withBrowserVersions<T extends BaseEditor>(e: T): T & BrowserVersionEditor;
