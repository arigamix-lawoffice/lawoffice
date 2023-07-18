import { TileExtension, ITileLocalExtensionContext } from 'tessa/ui/tiles';
import { UIContext } from 'tessa/ui';
import { ICardModel } from 'tessa/ui/cards';
import { TestCardTypeID } from './common';

/**
 * Скрытие дефолтного тайла для выбранного типа карточки.
 *
 * Результат работы расширения:
 * Пример данного расширения скрывает тайл "Сохранить" для тестовой карточки.
 */
export class HideTileExtension extends TileExtension {
  public initializingLocal(context: ITileLocalExtensionContext): void {
    // получаем доступ к левой боковой панели
    const panel = context.workspace.leftPanel;

    // пытаемся получить тайл "Сохранить"
    const saveTile = panel.tryGetTile('SaveCard');
    if (!saveTile) {
      return;
    }

    // скрываем тайл для тестовой карточки
    saveTile.evaluating.add(e => {
      const editor = UIContext.current.cardEditor;
      let cardModel: ICardModel;
      if (!editor || !(cardModel = editor.cardModel!) || cardModel.card.typeId !== TestCardTypeID) {
        return;
      }

      e.setIsEnabledWithCollapsing(e.currentTile, false);
    });
  }
}
