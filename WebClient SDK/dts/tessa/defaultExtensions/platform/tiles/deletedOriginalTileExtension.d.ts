import { TileExtension, ITileLocalExtensionContext } from 'tessa/ui/tiles';
/**
 * Просмотр удалённой карточки. Скрываются все плитки, кроме некоторых платформенных.
 */
export declare class DeletedOriginalTileExtension extends TileExtension {
    initializingLocal(context: ITileLocalExtensionContext): void;
}
