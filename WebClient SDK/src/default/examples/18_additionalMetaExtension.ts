import { ApplicationExtension, IApplicationExtensionMetadataContext, MetadataStorage } from 'tessa';
import { IStorage } from 'tessa/platform/storage';
import { tryGetFromInfo } from 'tessa/ui';
import { TileExtension, ITileGlobalExtensionContext, Tile, TileGroups } from 'tessa/ui/tiles';

/**
 * Позволяет получать и использовать данные из MetadataStorage.info,
 * добавленные в мету через ServerInitializationExtension.
 *
 * Результат работы расширения:
 * - на клиенте достает данные, содержащие информацию о тайлах, добавленную
 *   через ServerInitializationExtension (по ключу из меты и добавляет в MetadataStorage.info).
 * - после инициализации приложения обращается к MetadataStorage.info, получает информацию о тайлах
 *   и добавляет их в левую панель.
 */

const AdditionalTilesKey = 'AdditionalTiles';

export class AdditionalMetaInitializationExtension extends ApplicationExtension {
  public async afterMetadataReceived(context: IApplicationExtensionMetadataContext): Promise<void> {
    if (context.response) {
      // проверяем наличие info в контексте
      const info = context.response.tryGetInfo();
      if (!info) {
        return;
      }

      // получаем информацию о тайлах, добавленную в мету через ServerInitializationExtension
      const additionalTiles = tryGetFromInfo<IStorage[] | null>(info, AdditionalTilesKey, null);
      if (!additionalTiles) {
        return;
      }

      // если информация о тайлах отсутствует в MetadataStorage, то добавляем ее
      if (!MetadataStorage.instance.info.has(AdditionalTilesKey)) {
        MetadataStorage.instance.info.set(AdditionalTilesKey, additionalTiles);
      }
    }
  }
}

export class AdditionalMetaTileExtension extends TileExtension {
  public initializingGlobal(context: ITileGlobalExtensionContext): void {
    // достаем информацию о тайлах из MetadataStorage
    const additionalTiles: IStorage[] | null =
      MetadataStorage.instance.info.get(AdditionalTilesKey);
    if (!additionalTiles) {
      return;
    }

    const leftPanel = context.workspace.leftPanel;

    for (const tileInfo of additionalTiles) {
      // создаем тайлы из полученной иноформации и добавляем их в левую панель
      leftPanel.tiles.push(
        new Tile({
          name: tileInfo.name,
          caption: tileInfo.caption,
          icon: tileInfo.info,
          contextSource: leftPanel.contextSource,
          group: TileGroups.Cards,
          order: 10
        })
      );
    }
  }
}
