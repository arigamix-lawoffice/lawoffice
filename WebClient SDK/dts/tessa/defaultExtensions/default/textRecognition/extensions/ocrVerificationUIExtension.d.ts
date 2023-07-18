import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
/** Расширение для реализации общей UI логики взаимодействия с карточкой OCR. */
export declare class OcrVerificationUIExtension extends CardUIExtension {
    private _recognizedBoxSelectedDisposer;
    private readonly _disposers;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): Promise<void>;
    finalized(): void;
    private modifyPreviewer;
    private static modifyPreviewerForRecognitionMode;
    private static onRecognizedBoxSelect;
    private static getRecognizedFileId;
}
