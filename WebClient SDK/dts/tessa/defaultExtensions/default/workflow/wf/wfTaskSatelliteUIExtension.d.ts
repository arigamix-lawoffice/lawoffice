import { CardUIExtension, ICardUIExtensionContext, CardModelInitializingEventArgs } from 'tessa/ui/cards';
export declare class WfTaskSatelliteUIExtension extends CardUIExtension {
    initialized(context: ICardUIExtensionContext): void;
    static onCardModelInitialized(e: CardModelInitializingEventArgs): Promise<void>;
}
