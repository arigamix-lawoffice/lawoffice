import { IViewGetExtensionContext } from './viewGetExtensionContext';
import { IExtension } from 'tessa/extensions';
export interface IViewGetExtension extends IExtension {
    beforeRequest(context: IViewGetExtensionContext): any;
    afterRequest(context: IViewGetExtensionContext): any;
}
export declare class ViewGetExtension implements IViewGetExtension {
    static readonly type = "ViewGetExtension";
    shouldExecute(_context: IViewGetExtensionContext): boolean;
    beforeRequest(_context: IViewGetExtensionContext): void;
    afterRequest(_context: IViewGetExtensionContext): void;
}
