import { CardGetExtension, ICardGetExtensionContext } from 'tessa/cards/extensions';
export declare class CardTemplateGetExtension extends CardGetExtension {
    afterRequest(context: ICardGetExtensionContext): Promise<void>;
}
