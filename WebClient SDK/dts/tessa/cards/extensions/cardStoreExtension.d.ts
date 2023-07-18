import { ICardStoreExtensionContext } from './cardStoreExtensionContext';
import { IExtension } from 'tessa/extensions';
export interface ICardStoreExtension extends IExtension {
    beforeRequest(context: ICardStoreExtensionContext): any;
    afterRequest(context: ICardStoreExtensionContext): any;
}
export declare class CardStoreExtension implements ICardStoreExtension {
    static readonly type = "CardStoreExtension";
    shouldExecute(_context: ICardStoreExtensionContext): boolean;
    beforeRequest(_context: ICardStoreExtensionContext): void;
    afterRequest(_context: ICardStoreExtensionContext): void;
}
