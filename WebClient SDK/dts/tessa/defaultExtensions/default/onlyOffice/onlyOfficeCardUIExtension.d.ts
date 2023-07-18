import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/**
 * Представляет собой расширение, которое устанавливает в качестве средства предпросмотра OnlyOffice
 * и предупреждает пользователя об открытых файлах на редактирование при операциях с карточкой.
 */
export declare class OnlyOfficeCardUIExtension extends CardUIExtension {
    initializing(context: ICardUIExtensionContext): void;
    reopening(context: ICardUIExtensionContext): void;
    saving(context: ICardUIExtensionContext): void;
    finalizing(context: ICardUIExtensionContext): Promise<void>;
    private static checkOpenFiles;
    private static setUpOnlyOfficePreviewFactory;
    shouldExecute(): boolean;
}
