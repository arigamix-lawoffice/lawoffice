import { CardMetadataExtension, ICardMetadataExtensionContext } from 'tessa/cards/extensions';
/**
 * Расширение на метаданные приложения, в котором происходит добавление
 * контрола ползунка в диалог создания запроса на распознавание файла.
 */
export declare class OcrRequestMetadataExtension extends CardMetadataExtension {
    initializing(context: ICardMetadataExtensionContext): void;
}
