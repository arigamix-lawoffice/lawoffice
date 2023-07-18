import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class TopicsUIExtension extends CardUIExtension {
    private _dispose;
    initialized(context: ICardUIExtensionContext): Promise<void>;
    finalized(): void;
    private getOpenParticipantsAction;
    private getCheckAddTopicPermissionsAction;
    private getCheckSuperModeratorPermissionAction;
    private addTopicShowDialog;
    private superModeratorPermissionsMessage;
    private static openMarkedCard;
}
