import { BaseEditor } from 'slate';
export interface MobileEditor extends BaseEditor {
}
export declare function withMobile<T extends BaseEditor>(e: T): T & MobileEditor;
