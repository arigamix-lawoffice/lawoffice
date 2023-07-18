import { ICardGetExtensionContext } from './cardGetExtensionContext';
import { IExtension } from 'tessa/extensions';
export interface ICardGetExtension extends IExtension {
    beforeRequest(context: ICardGetExtensionContext): any;
    afterRequest(context: ICardGetExtensionContext): any;
}
export declare class CardGetExtension implements ICardGetExtension {
    static readonly type = "CardGetExtension";
    shouldExecute(_context: ICardGetExtensionContext): boolean;
    beforeRequest(_context: ICardGetExtensionContext): void;
    afterRequest(_context: ICardGetExtensionContext): void;
}
