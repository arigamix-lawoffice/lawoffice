import { ICardNewExtensionContext } from './cardNewExtensionContext';
import { IExtension } from 'tessa/extensions';
export interface ICardNewExtension extends IExtension {
    beforeRequest(context: ICardNewExtensionContext): any;
    afterRequest(context: ICardNewExtensionContext): any;
}
export declare class CardNewExtension implements ICardNewExtension {
    static readonly type = "CardNewExtension";
    shouldExecute(_context: ICardNewExtensionContext): boolean;
    beforeRequest(_context: ICardNewExtensionContext): void;
    afterRequest(_context: ICardNewExtensionContext): void;
}
