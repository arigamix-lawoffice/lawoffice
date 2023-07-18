import { TileExtension, ITileGlobalExtensionContext } from 'tessa/ui/tiles';
export declare class NotificationSubscriptionsTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    private static enableOnCardUpdateAndCanHaveSubscriptions;
    private static showNotificationSubscriptionsWindowAsync;
    private static checkAccess;
}
