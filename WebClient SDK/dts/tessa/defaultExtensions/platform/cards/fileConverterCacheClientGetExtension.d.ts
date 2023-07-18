import { CardGetExtension, ICardGetExtensionContext } from 'tessa/cards/extensions';
/**
 * Расширение устанавливает флаг, по которому будут загружены только данные виртуальных секций.
 * В противном случае на клиент может начать загружаться информация по десяткам тысяч файлов в кэше.
 */
export declare class FileConverterCacheClientGetExtension extends CardGetExtension {
    beforeRequest(context: ICardGetExtensionContext): void;
}
