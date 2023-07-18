import { ICardUIExtensionContext } from './cardUIExtensionContext';
import { IExtension } from 'tessa/extensions';
export interface ICardUIExtension extends IExtension {
    initializing(context: ICardUIExtensionContext): any;
    initialized(context: ICardUIExtensionContext): any;
    contextInitialized(context: ICardUIExtensionContext): any;
    reopening(context: ICardUIExtensionContext): any;
    reopened(context: ICardUIExtensionContext): any;
    saving(context: ICardUIExtensionContext): any;
    finalizing(context: ICardUIExtensionContext): any;
    finalized(context: ICardUIExtensionContext): any;
}
export declare abstract class CardUIExtension implements ICardUIExtension {
    static readonly type = "CardUIExtension";
    shouldExecute(_context: ICardUIExtensionContext): boolean;
    initializing(_context: ICardUIExtensionContext): void;
    initialized(_context: ICardUIExtensionContext): void;
    contextInitialized(_context: ICardUIExtensionContext): void;
    reopening(_context: ICardUIExtensionContext): void;
    reopened(_context: ICardUIExtensionContext): void;
    saving(_context: ICardUIExtensionContext): void;
    finalizing(_context: ICardUIExtensionContext): void;
    finalized(_context: ICardUIExtensionContext): void;
}
