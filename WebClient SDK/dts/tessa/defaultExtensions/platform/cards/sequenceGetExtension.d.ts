import { CardGetExtension, ICardGetExtensionContext } from 'tessa/cards/extensions';
export declare class SequenceGetExtension extends CardGetExtension {
    afterRequest(context: ICardGetExtensionContext): void;
    private static fixIntervalsAfterRemovingReserved;
}
