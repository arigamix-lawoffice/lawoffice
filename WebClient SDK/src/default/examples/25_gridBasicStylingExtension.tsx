import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { Guid } from 'tessa/platform';
import { GridViewModel } from 'tessa/ui/cards/controls';
import { TestCardTypeID } from './common';
import { compactLayout } from 'components/cardElements/grid';

import './25_styles.css';
import { reaction } from 'mobx';

// Свойства class-name и style предназначены для простых кейсов, когда
// требуется повлиять на какой-либо аспект внешнего вида таблицы или
// дочерних элементов.

// Для более сложных сценариев нужно использовать шаблоны описанные в
// 26_gridTemplateStylingExtension.

// Свойства className и style поддерживают модели представлений таблиц,
// блоков, строк, ячеек и заголовков.

// Свойства *width и border для таблиц следует использовать с
// осторожностью, так они могут повлиять на корректность расчета
// ширины таблицы.

/**
 * Изменяем стили контролов таблиц для определенного типа карточек
 * посредством CSS-классов и свойства style.
 *
 * Результат работы расширения:
 * Пример данного расширения изменяет стили контрола "Список акций" посредством добавления
 * СSS-классов для тестовой карточки "Автомобиль".
 */
export class GridBasicStylingExtension extends CardUIExtension {
  private _disposer?: () => void;

  public initialized(context: ICardUIExtensionContext): void {
    // если карточка не для тестов, то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // пытаемся найти контрол "Список акций"
    const gridControl = context.model.controls.get('ShareList');

    if (!(gridControl instanceof GridViewModel)) {
      return;
    }

    // Внутри компонентов стили объединяются в следующем порядке:
    // { ...internalStyle, ...props.style, ...viewModel.style }
    gridControl.style = { ...gridControl?.style, color: 'white !important' };

    // GridViewModel.onLayoutChange позволяет отреагировать на смену шаблона
    // перед обновлением контрола. Добавляем соответсвующие классы для мобильной и десктопной
    // версий полученного контрола.
    gridControl.onLayoutChange = (layout, _oldLayout, _metrics) => {
      if (layout.name === compactLayout.name) {
        gridControl.className.add('share-list-highlight-even-rows');
      } else {
        gridControl.className.add(
          'share-list-bigger-text share-list-dark-header share-list-highlight-even-rows'
        );
      }
    };

    // добавляем соответсвующие классы для четных и нечетных строк
    const updateRowsClassName = () =>
      gridControl.rows.forEach((r, i) => {
        if (i % 2 === 0) {
          r.className.has('grid-row-dark') && r.className.remove('grid-row-dark');
          r.className.add('grid-row-light');
        } else {
          r.className.has('grid-row-light') && r.className.remove('grid-row-light');
          r.className.add('grid-row-dark');
        }
      });

    updateRowsClassName();

    this._disposer = reaction(
      () => gridControl.rows.map(r => r.rowId),
      () => updateRowsClassName()
    );

    // Свойство autohideOrder позволяет задать порядок скрытия колонок
    // когда все колонки не умещаются в заданную ширину котнрола.
    // Принимает массив с идентификаторами колонок. Если указана только часть
    // из имеющихся колонок, то они будут скрываться первыми в порядке от первой
    // к последней. Оставшиеся колонки скрываются в порядке от последней к первой.
    gridControl.autohideOrder = ['$CardTypes_Columns_Controls_EndDate'];
  }

  public finalized(_context: ICardUIExtensionContext): void {
    this._disposer && this._disposer();
  }
}
