import { IExtension } from 'tessa/extensions';
import { ICardCreateFromTemplateExtensionContext } from './cardCreateFromTemplateExtensionContext';
export interface ICardCreateFromTemplateExtension extends IExtension {
    beforeRequest(context: ICardCreateFromTemplateExtensionContext): any;
    afterRequest(context: ICardCreateFromTemplateExtensionContext): any;
}
export declare class CardCreateFromTemplateExtension implements ICardCreateFromTemplateExtension {
    static readonly type = "CardCreateFromTemplateExtension";
    shouldExecute(_context: ICardCreateFromTemplateExtensionContext): boolean;
    beforeRequest(_context: ICardCreateFromTemplateExtensionContext): void;
    afterRequest(_context: ICardCreateFromTemplateExtensionContext): void;
}
