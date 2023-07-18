import { CardTypeExtensionType, CardTypeExtensionTypeRegistry } from 'tessa/cards';

/**
 * Перечень типов расширений для типов карточек.
 */
export class DefaultCardTypeExtensionTypes {
  /**
   * Расширение, позволяющее открывать карточки из представления.
   */
  public static openCardInView = new CardTypeExtensionType(
    '9df93a75-f788-4b06-bd17-881a0458d046',
    'OpenCardInView'
  );

  /**
   * Регистрирует типы расширений типов в общем реестре.
   */
  public static register(): void {
    CardTypeExtensionTypeRegistry.instance.register(DefaultCardTypeExtensionTypes.openCardInView);
  }
}
