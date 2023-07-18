/**
 * Локализует строку, используя текущий объект {@link LocalizationManager.instance}.
 *
 * В функцию могут быть переданы:
 * - строка локализации {@link display}`='$Something'`;
 * - строка с плейсхолдерами локализации {@link display}`='{$Something}'`;
 * - строка с аргументами {@link values}, подставляемыми в строку {@link display} на позициях `{0}`, `{1}` и др.,
 *   где каждый из них может быть или строкой локализации `$Something`, или строкой, содержащей плейсхолдер `{$Something}`,
 *   или нестроковым значением, которое форматируется по правилам функции `formatToString`;
 * - интерполированная строка вида ```localize`{$Something} ${variableName}` ```, где значения подставляемых переменных `variableName`
 *   имеют такие же правила подстановки, как и значения массива {@link values} из соответствующей перегрузки.
 *
 * @param {?string | string[]} display строка, содержащая обычную строку, строку локализации,
 * строку с плейсхолдерами локализации, или массив строк при вызове tag function.
 * @param {?Array} values параметры, подставляемые в строку {@link display} на позициях `{0}`, `{1}` и др.,
 * или вставляемые между элементами массива {@link display} при вызове как tag function.
 * Каждый параметр может быть значением любого типа, а также строкой локализации.
 * @returns {!string} строка, в которой выполнена локализация и форматирование.
 *
 * @see {@link LocalizationManager} класс, предоставляющий средства локализации.
 * @see {@link LocalizationManager.format} функция, выполняющая
 * форматирование строки с аргументами (`{0}`, `{1}` и др.) совместно с локализацией.
 * @see {@link LocalizationManager.localize} функция, выполняющая
 * локализацию строки без аргументов, заменяет как строки локализации `$Something`,
 * так и строки с плейсхолдерами локализации `{$Something} else`.
 * @see {@link LocalizationManager.formatLocalized} функция, выполняющая
 * форматирование и локализацию при вызове как tag function.
 *
 * @example <caption>Строка локализации</caption>
 * localize('$Something')
 * localize`$Something`
 * // SOMETHING
 * @example <caption>Плейсхолдеры локализации</caption>
 * localize('{$Something} else')
 * localize`{$Something} else`
 * // SOMETHING else
 * @example <caption>Форматирование с аргументами</caption>
 * const firstName = 'John';
 * const lastName = 'Doe';
 * localize('{$Hello}, {0} {1}!', firstName, lastName)
 * localize`{$Hello}, ${firstName} ${lastName}!`
 * // Hello, John Doe!
 */
export declare function localize(display: string | null | undefined, ...values: readonly unknown[]): string;
export declare function localize(display: readonly string[], ...values: readonly unknown[]): string;
