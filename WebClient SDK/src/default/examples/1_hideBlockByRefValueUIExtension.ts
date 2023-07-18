import {
  CardUIExtension,
  ICardUIExtensionContext,
  IBlockViewModel,
  IControlViewModel
} from 'tessa/ui/cards';
import { Visibility, Guid } from 'tessa/platform';
import { TestCardTypeID } from './common';

/**
 * В зависимости от значения ссылочного поля выбранного типа карточки:
 * - Скрываем\показываем элементы управления (блок, контрол).
 * - Делаем рид-онли\редактируемыми элементы управления (контрол).
 *
 * Результат работы расширения:
 * Для тестовой карточки "Автомобиль":
 * - Если поле "Цвет" не пустое, то блок "Дополнительная информация" становится видимым,
 *   контрол "Марка автомобиля" скрывается, а контрол "Пробег" становится только для чтения.
 * - Если поле "Цвет" пустое, то блок "Дополнительная информация" скрывается, контрол "Марка автомобиля"
 *   становится видимым, а контрол "Пробег" становится редактируемым.
 */
export class HideBlockByRefValueUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    // если карточка не для тестов, то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // находим секцию "Дополнительная информация"
    const section = context.card.sections.tryGet('TEST_CarAdditionalInfo');
    // находим блок "Дополнительная информация"
    const block = context.model.blocks.get('AdditionalInfo');
    // находим контролы "Марка автомобиля" и "Пробег" соответсвенно
    const control1 = context.model.controls.get('CarName');
    const control2 = context.model.controls.get('Running');
    if (!section || !block || !control1 || !control2) {
      return;
    }

    // смотрим есть ли какое-нибудь значение в поле Color (Цвет)
    const itemExists = !!section.fields.tryGet('Color');
    HideBlockByRefValueUIExtension.hideBlock(block, control1, control2, itemExists);

    // подписываемся на изменения полей в секции
    section.fields.fieldChanged.add(e => {
      // если было изменено другое поле, то никак не реагируем
      if (e.fieldName !== 'Color') {
        return;
      }

      // скрываем/показываем элементы управления в зависимости от содержимого поля "Color" (Цвет)
      HideBlockByRefValueUIExtension.hideBlock(block, control1, control2, !!e.fieldValue);
    });
  }

  private static hideBlock(
    block: IBlockViewModel,
    control1: IControlViewModel,
    control2: IControlViewModel,
    itemExists: boolean
  ) {
    if (itemExists) {
      // если значение есть, то мы показываем блок "Дополнительная информация", скрываем
      // контрол "Марка автомобиля" и делаем readOnly контрол "Пробег"
      block.blockVisibility = Visibility.Visible;
      control1.controlVisibility = Visibility.Collapsed;
      control2.isReadOnly = true;
    } else {
      // если значения нет, то мы скрываем блок "Дополнительная информация", показываем контрол
      // "Марка автомобиля" и делаем редактируемым контрол "Пробег"
      block.blockVisibility = Visibility.Collapsed;
      control1.controlVisibility = Visibility.Visible;
      control2.isReadOnly = false;
    }
  }
}
