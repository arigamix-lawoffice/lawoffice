import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class KrExtendedPermissionsUIExtension extends CardUIExtension {
    private _disposes;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
    private modifyTask;
}
