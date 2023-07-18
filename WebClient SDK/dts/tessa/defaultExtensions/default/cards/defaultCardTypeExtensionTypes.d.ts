import { CardTypeExtensionType } from 'tessa/cards';
/**
 * Перечень типов расширений для типов карточек.
 */
export declare class DefaultCardTypeExtensionTypes {
    /**
     * Расширение, позволяющее открывать карточки из представления.
     */
    static openCardInView: CardTypeExtensionType;
    /**
     * Регистрирует типы расширений типов в общем реестре.
     */
    static register(): void;
}
