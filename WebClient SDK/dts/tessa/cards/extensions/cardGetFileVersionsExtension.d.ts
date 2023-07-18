import { ICardGetFileVersionsExtensionContext } from './cardGetFileVersionsExtensionContext';
import { IExtension } from 'tessa/extensions';
export interface ICardGetFileVersionsExtension extends IExtension {
    beforeRequest(context: ICardGetFileVersionsExtensionContext): any;
    afterRequest(context: ICardGetFileVersionsExtensionContext): any;
}
export declare class CardGetFileVersionsExtension implements ICardGetFileVersionsExtension {
    static readonly type = "CardGetFileVersionsExtension";
    shouldExecute(_context: ICardGetFileVersionsExtensionContext): boolean;
    beforeRequest(_context: ICardGetFileVersionsExtensionContext): void;
    afterRequest(_context: ICardGetFileVersionsExtensionContext): void;
}
