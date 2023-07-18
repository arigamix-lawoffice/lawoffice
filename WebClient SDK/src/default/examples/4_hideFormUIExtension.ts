import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { Guid } from 'tessa/platform';
import { tryGetFromInfo } from 'tessa/ui';
import { CardSingletonCache } from 'tessa/cards';
import { TestCardTypeID } from './common';

/**
 * Скрывать\показывать вкладку определенной карточки в зависимости:
 * - от наличия какого-то признака в info, пришедшем с сервера.
 * - от значения какого-то справочника, загруженного в init-стриме.
 * - от данных карточки.
 *
 * Результат работы расширения:
 * Для тестовой карточки "Автомобиль" скрываем вкладку "Сравнение файлов" в зависимости от:
 * - от наличия флажка "__HideForm" в info, пришедшем с сервера.
 * - от значения справочника "HideCommentForApprove".
 * - от наличия контрола "Базовый цвет" в тестовой карточке.
 */
export class HideFormUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    // если карточка не для тестов, то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // пытаемся найти вкладку "Сравнение файлов"
    const testForm = context.model.forms.find(x => x.name === 'Files');
    if (!testForm) {
      return;
    }

    // пытаемся найти флажок в info карточки
    const hideFromInfo = tryGetFromInfo(context.card.info, '__HideForm', false);

    // пытаемся найти флажок в справочнике
    let hideFromSettings = false;
    const settings = CardSingletonCache.instance.cards.get('KrSettings');
    if (settings) {
      const section = settings.sections.tryGet('KrSettings');
      if (section) {
        hideFromSettings = section.fields.get('HideCommentForApprove');
      }
    }

    // смотрим на флажок в данных карточки
    let hideFromCard = false;
    const additionalInfo = context.card.sections.tryGet('TEST_CarAdditionalInfo');
    if (additionalInfo) {
      hideFromCard = additionalInfo.fields.get('IsBaseColor');
    }

    // скрываем или показываем вкладку
    testForm.isCollapsed = hideFromInfo || hideFromSettings || hideFromCard;
  }
}
