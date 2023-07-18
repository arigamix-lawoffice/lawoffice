import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { Guid } from 'tessa/platform';
import React, { FC, useCallback, useMemo } from 'react';
import {
  DefaultCell,
  DefaultHeaderRow,
  defaultLayout,
  DefaultTable,
  IGridCellProps,
  IGridHeaderRowProps,
  IGridLayout,
  IGridTableProps,
  IGridViewBag,
  isGridCellViewModel,
  isGridRowViewModel
} from 'components/cardElements/grid';
import { GridViewModel } from 'tessa/ui/cards/controls';
import { getColumnCount, GridMouseEventHandler } from 'components/cardElements/grid/common';
import { observer } from 'mobx-react-lite';
import { TestCardTypeID } from './common';
import { RaisedButton } from 'ui';

interface IExampleViewBag extends IGridViewBag {
  dateColumnId: string;
  shareColumnId: string;
}

/**
 * Изменяет вывод данных, поведение, а также подменяет стили контролов таблиц через "Шаблоны"
 * с помощью React-компонентов для выбранного типа карточки.
 *
 * Описание "Шаблона" и принцип его работы:
 * Шаблон состоит из шести компонентов: Table, HeaderRow, Block, Row, HeaderCell и Cell.
 * Внутри шаблона все параметры и коллбеки передаются насквозь. Это позволяет компонентам
 * на любом уровне иметь доступ к глобальному состоянию грида и при необходимости подменить
 * отдельные свойства и обработчики для дочерних компонентов.
 *
 * Результат работы расширения:
 * Пример данного расширения изменяет вывод табличных данных, поведение, а также подменяет стили
 * контрола "Список акций" тестовой карточки "Автомобиль":
 * - При наличии overflow-строк перехватывает событие клика по выбранной строке и автоматически
 * разворачивает выделенную строку и сворачивает остальные (при условии, если они были развернуты).
 * - При наличии overflow-строк подменяет стили заголовка контрола таблицы, а также добавляем дополнительное
 * меню под стилизированным заголовком.
 * - Изменяем отображение контента без внесения изменений в исходные данные:
 *   1. Если ячейка пустая, то с помощью `contentOverride` меняет ее содержимое на "отсутствует".
 *   2. Если в ячейке находится дата в числовом формате "дд.мм.гггг", то с помощью `contentOverride`
 *      перезаписывает дату в формате, где мм - полное название месяца (Например, ноябрь).
 *   3. Если название акции равно "Windows" или "Apple", то с помощью `customContentPrefix` и
 *      `customContentSuffix` добавляет, соответсвенно, логотипы данных компаний перед и
 *      после их названия в колонке "Акция".
 */
export class GridLayoutStylingExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // пытаемся получить контрол "Список акций".
    const gridControl = context.model.controls.get('ShareList');

    if (!(gridControl instanceof GridViewModel)) {
      return;
    }

    // Единственный обязательный параметр для шаблона, это его имя.
    // Если использовать имя одного из стандартных шаблонов,
    // то ваши компоненты будут использоваться взамен компонентов этого
    // шаблона.
    const customLayout: IGridLayout = { name: defaultLayout.name };

    // Необязательный параметр, определяет ширину начала шаблона.
    // Если два шаблона имеют одинаковое значение start, то
    // использоваться будет идущий первым в массиве layouts.
    customLayout.start = 0;

    // Пример перегрузки handlers.
    customLayout.table = CustomTable;

    // Пример подмены стиля.
    // Пример добавления меню под заголовком таблицы.
    customLayout.headerRow = CustomHeaderRow;

    // Пример перегрузки свойств contentOverride, contentPrefix и contentSuffix.
    customLayout.cell = CustomCell;

    // Для передачи данных компонентам можно использовать viewBag.
    const viewBag: IExampleViewBag = {
      ...gridControl.gridViewBag,
      dateColumnId: '$CardTypes_Columns_Controls_EndDate',
      shareColumnId: '$CardTypes_Columns_Blocks_Share'
    };

    gridControl.gridViewBag = viewBag;
    gridControl.layouts = [customLayout];
  }
}

// Таблица, кроме непосредственно вывода самой таблицы, собирает
// информацию о ширине колонок и контейнера, отслеживает
// изменения в контенте и определяет момент когда нужно
// производить расчет видимых и перекрытых (overflown) колонок
// (defaultLayout.table поддерживает отслеживание ширины колонок,
// но compactLayout.table нет, так как подразумевается что в ней
// только одна колонка).

// Перегружать табличный контрол имеет смысл только если
// необходимо повлиять на что-то на глобальном уровне.

// Если по каким-то причинам необходимо создать полностью свою
// версию таблицы, логику сбора ширины колонок нужно будет описать
// самостоятельно.

// В данном примере перехватываем событие клика и автоматически
// разворачиваем выделенную строку и сворачиваем остальные.
const CustomTable: FC<IGridTableProps> = observer(({ handlers, ...otherProps }) => {
  const { rows } = otherProps.gridProps;
  const { onClick } = handlers;

  const handleClick: GridMouseEventHandler = useCallback(
    (e, target) => {
      onClick(e, target);

      // В зависимости положения курсора событие может быть
      // вызвано из ячейки или из строки.
      const id = isGridRowViewModel(target)
        ? target.id
        : isGridCellViewModel(target)
        ? target.parent.id
        : null;

      if (id) {
        rows.forEach(r => {
          if (r.id === id) {
            r.showOverflow = true;
          } else if (r.showOverflow) {
            r.showOverflow = false;
          }
        });
      }
    },
    [rows, onClick]
  );

  const customHandlers = useMemo(
    () => ({ ...handlers, onClick: handleClick }),
    [handlers, handleClick]
  );

  return <DefaultTable handlers={customHandlers} {...otherProps} />;
});

// В данном примере подменяем стили заголовка контрола таблицы,
// а также добавляем дополнительное меню под стилизированным заголовком.
const CustomHeaderRow: FC<IGridHeaderRowProps> = observer(({ style, ...otherProps }) => {
  const { columnInfo, gridProps } = otherProps;

  // Подменяем стили заголовка.
  const customHeaderRowStyle = useMemo(() => {
    const background = columnInfo.overflownColumns.length ? '#bfbfbf' : '';
    const color = columnInfo.overflownColumns.length ? 'white' : '';
    return { ...style, background, color };
  }, [style, columnInfo.overflownColumns.length]);

  // Создаем дополнительное меню.
  const expandMenu = useMemo(() => {
    const colSpan = getColumnCount(gridProps, columnInfo);

    const tdStyle = { padding: '3px' };
    const buttonStyle = { padding: '2px 5px' };

    const handleExpandButtonClick = () => {
      gridProps.rows.forEach(r => (r.showOverflow = true));
    };

    const handleCollapseButtonClick = () => {
      gridProps.rows.forEach(r => (r.showOverflow = false));
    };

    const expandAllButton = (
      <RaisedButton style={buttonStyle} onClick={handleExpandButtonClick}>
        Развернуть все
      </RaisedButton>
    );

    const collapseAllButton = (
      <RaisedButton style={buttonStyle} onClick={handleCollapseButtonClick}>
        Свернуть все
      </RaisedButton>
    );

    return (
      <tr>
        <td colSpan={colSpan} style={tdStyle}>
          {collapseAllButton} {expandAllButton}
        </td>
      </tr>
    );
  }, [gridProps, columnInfo]);

  return (
    <>
      <DefaultHeaderRow style={customHeaderRowStyle} {...otherProps} />
      {columnInfo.overflownColumns.length > 0 && expandMenu}
    </>
  );
});

// Свойства contentOverride, contentPrefix и contentSuffix позволяют
// манипулировать отображением контента без внесения изменений в
// исходные данные.
const CustomCell: FC<IGridCellProps> = observer(
  ({ contentOverride, contentPrefix, contentSuffix, ...otherProps }) => {
    const { cell } = otherProps;
    const viewBag = otherProps.gridProps.viewBag as IExampleViewBag;

    // Основная цель contentOverride это дать возможность подменить
    // значение поля IGridCellViewModel.content.
    // При наличии contentOverride стандартная генерация содержимого
    // в компоненте ячейки игнорируется.
    // HeaderCell поддерживает аналогичное свойство.
    const customContentOverride = useMemo(() => {
      if (cell.columnId !== viewBag.dateColumnId) {
        return contentOverride;
      }

      if (!cell.content) {
        return <span>отсутствует</span>;
      }

      if (typeof cell.content === 'string') {
        const parts = cell.content.split('.');
        const date = new Date(parseInt(parts[2]), parseInt(parts[1]) - 1, parseInt(parts[0]));
        return (
          <>
            {date.toLocaleDateString('ru-RU', { day: 'numeric', month: 'long', year: 'numeric' })}
          </>
        );
      }
    }, [cell.content, viewBag.dateColumnId, cell.columnId, contentOverride]);

    // customContentPrefix и customContentSuffix позволяют добавить
    // React-элемент до или после основного содержимого.
    // HeaderCell поддерживает аналогичные свойства.
    const customContentPrefix = useMemo(() => {
      if (cell.columnId !== viewBag.shareColumnId || cell.content !== 'Windows') {
        return contentPrefix;
      }
      // Показываем иконку Windows в префиксе, если содержимое ячейки 'Windows'.
      return (
        <span style={{ display: 'flex', alignItems: 'center', fontFamily: 'Marlett' }}>
          {String.fromCharCode(0x57)}
        </span>
      );
    }, [cell.columnId, cell.content, viewBag.shareColumnId, contentPrefix]);

    const customContentSuffix = useMemo(() => {
      if (cell.columnId !== viewBag.shareColumnId || cell.content !== 'Apple') {
        return contentSuffix;
      }

      // Показываем иконку Apple в суффиксе, если содержимое ячейки 'Apple'.
      return (
        <span style={{ display: 'flex', alignItems: 'center' }}>
          <svg
            xmlns="http://www.w3.org/2000/svg"
            width="16"
            height="16"
            fill="currentColor"
            className="bi bi-apple"
            viewBox="0 0 16 16"
          >
            <path d="M11.182.008C11.148-.03 9.923.023 8.857 1.18c-1.066 1.156-.902 2.482-.878 2.516.024.034 1.52.087 2.475-1.258.955-1.345.762-2.391.728-2.43Zm3.314 11.733c-.048-.096-2.325-1.234-2.113-3.422.212-2.189 1.675-2.789 1.698-2.854.023-.065-.597-.79-1.254-1.157a3.692 3.692 0 0 0-1.563-.434c-.108-.003-.483-.095-1.254.116-.508.139-1.653.589-1.968.607-.316.018-1.256-.522-2.267-.665-.647-.125-1.333.131-1.824.328-.49.196-1.422.754-2.074 2.237-.652 1.482-.311 3.83-.067 4.56.244.729.625 1.924 1.273 2.796.576.984 1.34 1.667 1.659 1.899.319.232 1.219.386 1.843.067.502-.308 1.408-.485 1.766-.472.357.013 1.061.154 1.782.539.571.197 1.111.115 1.652-.105.541-.221 1.324-1.059 2.238-2.758.347-.79.505-1.217.473-1.282Z" />
            <path d="M11.182.008C11.148-.03 9.923.023 8.857 1.18c-1.066 1.156-.902 2.482-.878 2.516.024.034 1.52.087 2.475-1.258.955-1.345.762-2.391.728-2.43Zm3.314 11.733c-.048-.096-2.325-1.234-2.113-3.422.212-2.189 1.675-2.789 1.698-2.854.023-.065-.597-.79-1.254-1.157a3.692 3.692 0 0 0-1.563-.434c-.108-.003-.483-.095-1.254.116-.508.139-1.653.589-1.968.607-.316.018-1.256-.522-2.267-.665-.647-.125-1.333.131-1.824.328-.49.196-1.422.754-2.074 2.237-.652 1.482-.311 3.83-.067 4.56.244.729.625 1.924 1.273 2.796.576.984 1.34 1.667 1.659 1.899.319.232 1.219.386 1.843.067.502-.308 1.408-.485 1.766-.472.357.013 1.061.154 1.782.539.571.197 1.111.115 1.652-.105.541-.221 1.324-1.059 2.238-2.758.347-.79.505-1.217.473-1.282Z" />
          </svg>
        </span>
      );
    }, [cell.columnId, cell.content, viewBag.shareColumnId, contentSuffix]);

    return (
      <DefaultCell
        contentOverride={customContentOverride}
        contentPrefix={customContentPrefix}
        contentSuffix={customContentSuffix}
        {...otherProps}
      />
    );
  }
);
