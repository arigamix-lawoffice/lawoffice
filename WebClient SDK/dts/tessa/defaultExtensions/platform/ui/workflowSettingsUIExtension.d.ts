import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class WorkflowSettingsUIExtension extends CardUIExtension {
    private _disposes;
    initialized(context: ICardUIExtensionContext): void;
    finalized(): void;
}
