import { CardControlType } from './cardControlType';
/**
 * TextBox для ввода строки.
 */
export declare const StringControlType: CardControlType;
/**
 * TextBlock для вывода неизменяемого текста.
 */
export declare const TextControlType: CardControlType;
/**
 * TextBlock для вывода неизменяемого текста, заданного в настройках.
 */
export declare const LabelControlType: CardControlType;
/**
 * TextBox для ввода целого числа.
 */
export declare const IntegerControlType: CardControlType;
/**
 * TextBox для ввода числа с плавающей точкой.
 */
export declare const DoubleControlType: CardControlType;
/**
 * TextBox для ввода денежной суммы.
 */
export declare const DecimalControlType: CardControlType;
/**
 * Checkbox для ввода логического значения.
 */
export declare const BooleanControlType: CardControlType;
/**
 * DataGrid для вывода коллекционных или древовидных секций.
 */
export declare const GridControlType: CardControlType;
/**
 * AutoComplete для автодополнения текста.
 */
export declare const AutoCompleteEntryControlType: CardControlType;
/**
 * AutoComplete для автодополнения списка элементов.
 */
export declare const AutoCompleteTableControlType: CardControlType;
/**
 * DateTime для ввода даты и времени.
 */
export declare const DateTimeControlType: CardControlType;
/**
 * TabControl для вывода нескольких форм в отдельных вкладках.
 */
export declare const TabControlControlType: CardControlType;
/**
 * Container для вывода единственной формы без вкладок.
 */
export declare const ContainerControlType: CardControlType;
/**
 * FileList для редактирования списка файлов.
 */
export declare const FileListControlType: CardControlType;
/**
 * FilePreview для отображения области предпросмотра файлов.
 */
export declare const FilePreviewControlType: CardControlType;
/**
 * Button для вывода кнопки.
 */
export declare const ButtonControlType: CardControlType;
/**
 * Контрол для отображения html.
 */
export declare const HtmlViewerControlType: CardControlType;
/**
 * TaskInfo для вывода информации по заданию.
 */
export declare const TaskInfoControlType: CardControlType;
/**
 * Numerator для работы с номером.
 */
export declare const NumeratorControlType: CardControlType;
/**
 * Контрол форумов
 */
export declare const ForumControlType: CardControlType;
/**
 * Выбор и отображение цвета.
 */
export declare const ColorPickerControlType: CardControlType;
/**
 * TextBox с возможностью форматирования.
 */
export declare const RichTextBoxControlType: CardControlType;
/**
 * ViewControl
 */
export declare const ViewControlControlType: CardControlType;
export declare function getDefaultControlTypes(): CardControlType[];
