import { ICardDeleteExtensionContext } from './cardDeleteExtensionContext';
import { IExtension } from 'tessa/extensions';
export interface ICardDeleteExtension extends IExtension {
    beforeRequest(context: ICardDeleteExtensionContext): any;
    afterRequest(context: ICardDeleteExtensionContext): any;
}
export declare class CardDeleteExtension implements ICardDeleteExtension {
    static readonly type = "CardDeleteExtension";
    shouldExecute(_context: ICardDeleteExtensionContext): boolean;
    beforeRequest(_context: ICardDeleteExtensionContext): void;
    afterRequest(_context: ICardDeleteExtensionContext): void;
}
