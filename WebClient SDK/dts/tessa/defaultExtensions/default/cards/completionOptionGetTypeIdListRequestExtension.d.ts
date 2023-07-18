import { CardRequestExtension, ICardRequestExtensionContext } from 'tessa/cards/extensions';
export declare class CompletionOptionGetTypeIdListRequestExtension extends CardRequestExtension {
    shouldExecute(context: ICardRequestExtensionContext): boolean;
    beforeRequest(context: ICardRequestExtensionContext): void;
}
