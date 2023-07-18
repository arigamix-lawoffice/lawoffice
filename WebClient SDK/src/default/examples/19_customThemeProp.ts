import { ApplicationExtension, IApplicationExtensionMetadataContext } from 'tessa';
import { ThemeManager } from 'tessa/ui/themes';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { Guid } from 'tessa/platform';
import { TextBoxViewModel } from 'tessa/ui/cards/controls';
import { addToMediaStyle } from 'ui';
import { TestCardTypeID } from './common';

/**
 * Добавляем дополнительное свойство в темы
 * в расширении ApplicationExtension с последующим использованием для выбранного типа карточки.
 *
 * Результат работы расширения:
 * Данное расширение добавляет дополнительное свойство "MyCustomBackgroundProp" в настройки текущей темы,
 * которое, в свою очередь, устанавливается в качестве фона для заголовка контрола "Марка автомобиля"
 * тестовой карточки "Автомобиль".
 */
export class CustomThemePropApplicationExtension extends ApplicationExtension {
  public async afterMetadataReceived(
    _context: IApplicationExtensionMetadataContext
  ): Promise<void> {
    // проверяем наличие инициализированной темы
    if (!ThemeManager.instance.isInitialized) {
      return;
    }

    // в зависимости от названия темы добавляем соответствующий цвет в ее настройки
    for (const pair of ThemeManager.instance.themes) {
      const name = pair[0];
      const theme = pair[1];
      const backgroundColor =
        name === 'Cold' ? 'rgba(150, 150, 150, 0.5)' : 'rgba(66, 88, 111, 0.75)';
      theme.settings.common['MyCustomBackgroundProp'] = backgroundColor;
    }
  }
}

export class CustomThemePropUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext): void {
    // если не карточка "автомобиль", то ничего не делаем
    if (!Guid.equals(context.card.typeId, TestCardTypeID)) {
      return;
    }

    // пытаемся найти контрол "Марка автомобиля"
    const carNameControl = context.model.controls.get('CarName') as TextBoxViewModel;
    if (!carNameControl) {
      return;
    }

    // для заголовка полученного контрола устанавливаем цвет фона, добавленный ранее в настроки темы
    carNameControl.captionStyle = addToMediaStyle(carNameControl.captionStyle, 'default', {
      background: ThemeManager.instance.currentTheme.settings.common['MyCustomBackgroundProp']
    });
  }
}
