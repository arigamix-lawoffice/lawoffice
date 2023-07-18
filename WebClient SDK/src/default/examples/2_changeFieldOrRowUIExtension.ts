import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { Guid, DotNetType } from 'tessa/platform';
import { CardRowState, CardRow } from 'tessa/cards';
import { TestCardTypeID } from './common';

/**
 * В зависимости от значения флага на форме выбранного типа карточки менять другие поля этой же карточки:
 * - текстовое поле.
 * - добавлять строчку в коллекционную секцию.
 *
 * Когда нажимают галку "Базовый цвет", то:
 * - если галка установлена, то значение поля "Цвет" меняется на "Это базовый цвет".
 * - если галка установлена, то в секцию "Список Акций" добавляется новая строка.
 * - если галка не установлена, то значение поля "Цвет" меняется на "Это другой, не базовый цвет".
 */
export class ChangeFieldOrRowUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    // если карточка не для тестов, то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // пытаемся найти секции "Дополнительная информация" и "Список Акций"
    const additionalInfo = context.card.sections.tryGet('TEST_CarAdditionalInfo');
    const carSales = context.card.sections.tryGet('TEST_CarSales');
    if (!additionalInfo || !carSales) {
      return;
    }

    // подписываемся на изменения полей в секции
    additionalInfo.fields.fieldChanged.add(e => {
      // если было изменено другое поле, то никак не реагируем
      if (e.fieldName !== 'IsBaseColor') {
        return;
      }

      const text = e.fieldValue ? 'Это базовый цвет' : 'Это другой, не базовый цвет';
      // ставим значения поля в секции
      additionalInfo.fields.set('Color', text, DotNetType.String);

      if (e.fieldValue) {
        // добавляем новую строчку в табличную секцию
        const newRow = new CardRow();
        newRow.rowId = Guid.newGuid();
        newRow.set('ID', '3db19fa0-228a-497f-873a-0250bf0a4ccb', DotNetType.Guid);
        newRow.set('Name', 'Test Row', DotNetType.String);
        newRow.set('ManagerID', '3db19fa0-228a-497f-873a-0250bf0a4ccb', DotNetType.Guid);
        newRow.set('ManagerName', 'Admin', DotNetType.String);
        newRow.state = CardRowState.Inserted;
        carSales.rows.push(newRow);
      }
    });
  }
}
