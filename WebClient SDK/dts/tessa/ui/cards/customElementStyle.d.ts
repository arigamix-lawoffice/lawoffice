declare type CustomCaptionStyle = {
    /** Свойство отвечает за стилизацию размера шрифта в заголовке настраимоего элемента (см. CSS `font-size`) */
    readonly captionFontSize?: string;
    /** Свойство отвечает за стилизацию цвета шрифта в заголовке настраимоего элемента (см. CSS `color`) */
    readonly captionFontColor?: string;
    /** Свойство отвечает за наличие иконки в заголовке настраимоего элемента (указывается одна из стандартных иконок Тесса) */
    readonly captionIcon?: string;
    /** Свойство отвечает за стилизацию фона заголовка настраимоего элемента (см. CSS `background-color`) */
    readonly captionBackgroundColor?: string;
    /** Свойство отвечает за стилизацию фона заголовка элемента при наведении (см. CSS `background-color`) */
    readonly captionHoverBackgroundColor?: string;
    /** Свойство отвечает за стилизацию обводки заголовка при наведении (см. CSS `border-color`) */
    readonly captionHoverBorderColor?: string;
    /** Свойство отвечает за внутренние отсуты настаримого заголовка (см. CSS `padding`). */
    readonly captionPadding?: string;
};
declare type CustomMainStyle = {
    /** Свойство отвечает за стилизацию обводки настраимоего элемента (см. CSS `border-color`). */
    readonly mainBorderColor?: string;
    /** Свойство отвечает за скругление углов настраимоего элемента (см. CSS `border-radius`). */
    readonly mainBorderRadius?: boolean;
    /** Свойство отвечает за стилизацию фона настраимоего элемента (см. CSS `background-color`). */
    readonly mainBackgroundColor?: string;
    /** Свойство отвечает за внутренние отступы настраимоего элемента (см. CSS `padding`). */
    readonly mainPadding?: string;
};
/**
 * Объект параметров настроек стилизации блока.
 */
interface CustomBlockStyleParams extends CustomCaptionStyle, CustomMainStyle {
    /**
     * Свойство отвечает за внутренние отступы обертки контролов настраимоевого элемента
     * (см. CSS `padding`)
     */
    readonly controlsPadding?: string;
}
/** Объект настроек стилизации визуального представления блоков. */
export interface CustomBlockStyle extends CustomBlockStyleParams {
}
/**
 * Объект параметров настроек стилизации контрола.
 */
interface CustomControlStyleParams extends CustomCaptionStyle, CustomMainStyle {
}
/** Объект настроек стилизации визуального представления контролов. */
export interface CustomControlStyle extends CustomControlStyleParams {
}
/**
 * Объект параметров настроек стилизации формы.
 */
interface CustomFormStyleParams extends CustomMainStyle {
}
/** Объект настроек стилизации визуального представления форм. */
export interface CustomFormStyle extends CustomFormStyleParams {
}
/** Обобщенный объект, позволяющий настраивать стилизацию
 * визуального представления элементов интерфейса. */
export declare class CustomStyle implements CustomBlockStyle, CustomFormStyle, CustomControlStyle {
    private constructor();
    readonly captionFontSize?: string;
    readonly captionFontColor?: string;
    readonly captionIcon?: string;
    readonly captionBackgroundColor?: string;
    readonly captionHoverBackgroundColor?: string;
    readonly captionHoverBorderColor?: string;
    readonly captionPadding?: string;
    readonly mainBorderColor?: string;
    readonly mainBorderRadius?: boolean;
    readonly mainBackgroundColor?: string;
    readonly mainPadding?: string;
    readonly controlsPadding?: string;
    /** Метод, позволяющий создать объект стилизации, назначаемый в моделях представления блоков.
     * С помощью данного объекта можно настроить визуальное представление блоков.
     */
    static createCustomBlockStyle(arg: CustomBlockStyleParams, arg1?: CustomBlockStyleParams): CustomBlockStyle;
    /** Метод, позволяющий создать объект стилизации, назначаемый в моделях представления форм.
     * С помощью данного объекта можно настроить визуальное представление форм.
     */
    static createCustomFormStyle(arg: CustomFormStyleParams, arg1?: CustomFormStyleParams): CustomFormStyle;
    /** Метод, позволяющий создать объект стилизации, назначаемый в моделях представления контролов.
     * С помощью данного объекта можно настроить визуальное представление контролов.
     */
    static createCustomControlStyle(arg: CustomControlStyleParams, arg1?: CustomControlStyleParams): CustomControlStyle;
}
export {};
