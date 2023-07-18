import { ICardRequestExtensionContext } from './cardRequestExtensionContext';
import { IExtension } from 'tessa/extensions';
export interface ICardRequestExtension extends IExtension {
    beforeRequest(context: ICardRequestExtensionContext): any;
    afterRequest(context: ICardRequestExtensionContext): any;
}
export declare class CardRequestExtension implements ICardRequestExtension {
    static readonly type = "CardRequestExtension";
    shouldExecute(_context: ICardRequestExtensionContext): boolean;
    beforeRequest(_context: ICardRequestExtensionContext): void;
    afterRequest(_context: ICardRequestExtensionContext): void;
}
