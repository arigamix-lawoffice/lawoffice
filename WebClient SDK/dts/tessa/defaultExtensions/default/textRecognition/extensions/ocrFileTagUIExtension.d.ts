import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/** Расширение, выполняющее установку тэга OCR на файле в каждом файловом контроле карточки. */
export declare class OcrFileTagUIExtension extends CardUIExtension {
    private static readonly fileTag;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    contextInitialized(context: ICardUIExtensionContext): void;
}
