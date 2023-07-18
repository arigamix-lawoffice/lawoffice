import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { Guid } from 'tessa/platform';
import { GridViewModel } from 'tessa/ui/cards/controls';
import { TestCardTypeID } from './common';

/**
 * Подписываемся на двойной клик в контроле таблицы выбранной карточки.
 *
 * Результат работы расширения:
 * В карточке "Автомобиль" подписываемся на двойной клик в контроле таблицы "Список Акций".
 */
export class TableControlDoubleClickUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    // если не карточка "автомобиль", то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // пытаемся получить контрол "Список Акций"
    const tableControl = context.model.controls.get('ShareList') as GridViewModel;
    if (!tableControl) {
      return;
    }

    // подписываемся на открытие строки
    tableControl.rowInitializing.add(e => console.log(e));
  }
}
