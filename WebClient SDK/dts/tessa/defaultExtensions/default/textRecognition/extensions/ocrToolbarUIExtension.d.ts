import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/** Расширение, добавляющее дополнительные кнопки тулбара в карточке операции OCR. */
export declare class OcrToolbarUIExtension extends CardUIExtension {
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): Promise<void>;
    contextInitialized(context: ICardUIExtensionContext): void;
    private ocrProcessRunCommand;
    private ocrResultSaveCommand;
    private static removeInsertedRows;
}
