import { ICardGetFileContentExtensionContext } from './cardGetFileContentExtensionContext';
import { IExtension } from 'tessa/extensions';
export interface ICardGetFileContentExtension extends IExtension {
    beforeRequest(context: ICardGetFileContentExtensionContext): any;
    afterRequest(context: ICardGetFileContentExtensionContext): any;
}
export declare class CardGetFileContentExtension implements ICardGetFileContentExtension {
    static readonly type = "CardGetFileContentExtension";
    shouldExecute(_context: ICardGetFileContentExtensionContext): boolean;
    beforeRequest(_context: ICardGetFileContentExtensionContext): void;
    afterRequest(_context: ICardGetFileContentExtensionContext): void;
}
