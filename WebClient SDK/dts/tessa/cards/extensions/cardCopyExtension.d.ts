import { IExtension } from 'tessa/extensions';
import { ICardCopyExtensionContext } from './cardCopyExtensionContext';
export interface ICardCopyExtension extends IExtension {
    beforeRequest(context: ICardCopyExtensionContext): any;
    afterRequest(context: ICardCopyExtensionContext): any;
}
export declare class CardCopyExtension implements ICardCopyExtension {
    static readonly type = "CardCopyExtension";
    shouldExecute(_context: ICardCopyExtensionContext): boolean;
    beforeRequest(_context: ICardCopyExtensionContext): void;
    afterRequest(_context: ICardCopyExtensionContext): void;
}
