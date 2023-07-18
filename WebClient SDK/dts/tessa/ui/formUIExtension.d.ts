import { IExtension } from 'tessa/extensions';
import { IFormUIExtensionContext } from './formUIExtensionContext';
export interface IFormUIExtension extends IExtension<IFormUIExtensionContext> {
    initialized(context: IFormUIExtensionContext): any;
}
export declare abstract class FormUIExtension implements IFormUIExtension {
    static readonly type = "FormUIExtension";
    initialized(_context: IFormUIExtensionContext): void;
    shouldExecute(_context: IFormUIExtensionContext): boolean;
}
