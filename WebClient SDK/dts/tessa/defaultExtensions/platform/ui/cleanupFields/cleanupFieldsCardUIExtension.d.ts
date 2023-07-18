import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Очищаем поля секции вместе с заданным полем при выполнении расширения CardTypeExtensionTypes.CleanupFields
 */
export declare class CleanupFieldsCardUIExtension extends CardUIExtension {
    private _disposes;
    private _metadataBinder;
    initialized(context: ICardUIExtensionContext): Promise<void>;
    finalized(): void;
    private executeAction;
}
