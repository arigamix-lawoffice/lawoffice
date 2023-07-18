import { TileExtension, ITileGlobalExtensionContext } from 'tessa/ui/tiles';
/**
 * Расширение на формирование тайлов для формирования файлов по шаблонам.
 */
export declare class CreateFileTemplateTileExtension extends TileExtension {
    initializingGlobal(context: ITileGlobalExtensionContext): void;
    private static openCreateFileFromTemplateDialogAsync;
}
