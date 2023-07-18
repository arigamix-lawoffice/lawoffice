import { CardGetFileContentExtension, ICardGetFileContentExtensionContext } from 'tessa/cards/extensions';
/**
 * Расширение для предпросмотра вложенных файлов типа ForumItemViewModel | RichTextBoxAttachmentFile
 * | RichTextBoxAttachmentInnerItem.
 *
 * Изменяет CardGetFileContentRequest в соответствии с данными, полученными
 * по ключу PreviewAttachmentGetContentExtensionKey для корректного предпросмотра вложений.
 */
export declare class PreviewAttachmentGetFileContentExtension extends CardGetFileContentExtension {
    beforeRequest(context: ICardGetFileContentExtensionContext): void;
}
