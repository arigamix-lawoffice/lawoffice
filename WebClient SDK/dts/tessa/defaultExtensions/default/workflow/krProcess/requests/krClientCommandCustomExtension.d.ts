import { CardRequestExtension, ICardRequestExtensionContext } from 'tessa/cards/extensions';
export declare class KrClientCommandCustomExtension extends CardRequestExtension {
    afterRequest(context: ICardRequestExtensionContext): Promise<void>;
}
