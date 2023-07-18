import { ITableColumnViewModel } from 'tessa/ui/views/content/tableColumnViewModel';
import { SortDirection, SortingColumn } from 'tessa/views/sortingColumn';
import { ViewMetadataSealed } from 'tessa/views/metadata/viewMetadata';
import { ViewColumnMetadataSealed } from 'tessa/views/metadata/viewColumnMetadata';
/**
 * Имя настройки пользователя для списка скрытых колонок.
 * Если колонка отсутствует в списке или список отсутствует в настройках, то колонка отображается, иначе скрывается.
 * Алиас колонки проверяется без учёта регистра символов.
 */
export declare const HiddenColumnsSetting = "HiddenColumns";
/**
 * Имя настройки пользователя для имени колонки, по которой выполняется группировка.
 * Если значение отсутствует в настройках, то используется колонка в метаинформации по умолчанию {@link ViewControlSettingsManager.getDefaultGroupingFromMetadata};
 * если `null`, то группировка сброшена; иначе алиас колонки.
 * Алиас колонки проверяется без учёта регистра символов.
 */
export declare const GroupingColumnSetting = "GroupingColumn";
/**
 * Имя настройки пользователя для списка имён колонок, по которым выполняется сортировка по умолчанию.
 * Если значение отсутствует в настройках, то используются колонки в метаинформации по умолчанию {@link ViewControlSettingsManager.getDefaultSortingFromMetadata};
 * иначе это массив алиасов колонок, где направление сортировки по убыванию определяется наличием символа "-" в начале строки.
 */
export declare const SortingColumnsSetting = "SortingColumns";
/**
 * Имя настройки пользователя для списка имён колонок в порядке их отображения, если порядок был изменён пользователем.
 * Если значение отсутствует в настройках, то используются колонки в соответствии с порядком в элементе управления по умолчанию {@link ViewControlSettingsManager.getOrderedColumnsFromTable};
 * иначе это массив алиасов тех колонок, которые являются нескрытыми в метаинформации (но могут быть скрытыми в настройках пользователя).
 * Массив будет игнорирован, если среди отображаемых колонок будут колонки, отсутствующие в этом массиве.
 */
export declare const OrderedColumnsSetting = "OrderedColumns";
/**
 * Объект, управляющий настройками пользователя для элемента управления "Представление".
 */
export declare class ViewControlSettingsManager {
    /**
     * Создаёт экземпляр класса с указанием его зависимостей.
     * @param data Данные по настройкам пользователя, с которыми работает объект.
     * @param getMetadata Функция, возвращающая метаинформацию представления, с которой связан элемент управления,
     * или `null`, если элемент управления не связан с представлением.
     */
    constructor(data: Map<string, unknown>, getMetadata: () => ViewMetadataSealed | null);
    private readonly _data;
    private readonly _getMetadata;
    private tryGetStringFromData;
    private setStringToData;
    private tryGetArrayFromData;
    private tryGetStringArrayFromData;
    private setStringArrayToData;
    private static arraysAreEqualWithIgnoreCase;
    /**
     * Возвращает признак того, что в настройках пользователя указанная колонка отмечена как скрытая.
     * @param columnAlias Алиас колонки. Проверяется без учёта регистра символов.
     * @returns `true`, если в настройках пользователя указанная колонка отмечена как скрытая; `false` в противном случае.
     */
    isColumnHiddenSetting(columnAlias: string): boolean;
    /**
     * Возвращает алиасы скрытых колонок в настройках пользователя.
     * @returns Алиасы скрытых колонок.
     */
    getHiddenColumnsSetting(): string[];
    /**
     * Определяет в настройках пользователя, что указанная колонка является или не является скрытой.
     * Возвращает признак того, что настройки были изменены.
     * @param columnAlias Алиас колонки.
     * @param value `true`, если колонка должна быть скрытой; `false` в противном случае.
     * @returns `true`, если настройки были изменены; `false` в противном случае.
     */
    setColumnIsHiddenSetting(columnAlias: string, value: boolean): boolean;
    /**
     * Устанавливает видимость колонок по свойству {@link ITableColumnViewModel#visibility} в соответствии с текущими настройками пользователя.
     * @param columns Список колонок, видимость которых изменяется.
     * @param groupingColumn Колонка, по которой выполняется группировка, или `null`/`undefined`, если группировка не выполняется.
     * У такой колонки свойство {@link ITableColumnViewModel#visibility} не изменяется.
     */
    setColumnVisibilityFromSettings(columns: readonly ITableColumnViewModel[], groupingColumn?: ViewColumnMetadataSealed | null): void;
    /**
     * Возвращает алиас колонки группировки по умолчанию в соответствии с метаинформацией
     * или `null`, если по умолчанию группировка не выполняется.
     * @returns Алиас колонки группировки по умолчанию в соответствии с метаинформацией
     * или `null`, если по умолчанию группировка не выполняется.
     */
    getDefaultGroupingFromMetadata(): string | null;
    /**
     * Возвращает алиас колонки, по которой должна выполняться группировка в соответствии с настройками пользователя.
     * Также возвращает признак того, что возвращённый алиас является алиасом, указанным по умолчанию в соответствии с метаинформацией
     * (значение метода {@link getDefaultGroupingFromMetadata}).
     * @returns Массив `[GroupingColumn, IsDefault]`, где:
     * - `GroupingColumn` - алиас колонки, по которой должна выполняться группировка в соответствии с настройками пользователя;
     * - `IsDefault` - признак того, что возвращённый алиас является алиасом, указанным по умолчанию в соответствии с метаинформацией.
     */
    getGroupingColumnSetting(): [string | null, boolean];
    /**
     * Определяет в настройках пользователя, что по указанной колонке выполняется группировка.
     * Возвращает признак того, что настройки были изменены.
     * @param columnAlias Алиас колонки.
     * @returns `true`, если настройки были изменены; `false` в противном случае.
     */
    setGroupingColumnSetting(columnAlias: string): boolean;
    /**
     * Сбрасывает группировку в настройках пользователя так, что отсутствуют группирующие колонки.
     * При этом группировка по умолчанию {@link getDefaultGroupingFromMetadata} не учитывается.
     * Возвращает признак того, что настройки были изменены.
     * @returns `true`, если настройки были изменены; `false` в противном случае.
     */
    resetGroupingColumnSetting(): boolean;
    /**
     * Возвращает метаинформацию по группирующей колонке {@link ViewColumnMetadataSealed} в соответствии с текущими настройками
     * или `null`, если группировка не выполняется.
     * @param columns Колонки таблицы, содержащие метаинформацию.
     * @returns Метаинформация по группирующей колонке или `null`, если группировка не выполняется.
     */
    getGroupingColumnFromSettings(columns: readonly ITableColumnViewModel[]): ViewColumnMetadataSealed | null;
    /**
     * Возвращает список колонок для сортировки по умолчанию в соответствии с тем, как они указаны в метаинформации представления.
     * @returns Список колонок для сортировки по умолчанию в соответствии с тем, как они указаны в метаинформации представления.
     */
    getDefaultSortingFromMetadata(): string[];
    /**
     * Возвращает список текущих колонок для сортировки.
     * @param columns Список объектов колонок, на основании которых определяются настройки сортировки.
     * @returns Список текущих колонок для сортировки.
     */
    getActualSortingSettings(columns: readonly SortingColumn[]): string[];
    /**
     * Возвращает информацию по направлению сортировки для указанной колонки в соответствии с настройками пользователя
     * или `null`, если сортировка не выполняется.
     * @param columnAlias Алиас колонки.
     * @returns Направление сортировки для указанной колонки в соответствии с настройками пользователя
     * или `null`, если сортировка не выполняется.
     */
    getSortDirectionSetting(columnAlias: string): SortDirection | null;
    /**
     * Устанавливает направление сортировки для указанного списка колонок в соответствии с текущим состоянием объекта настроек.
     * @param columns Список объектов колонок, для которых определяются направления сортировки в соответствии с настройками.
     * @returns Список колонок {@link SortingColumn}, сортировка по которым выполняется.
     */
    setSortingFromSettings(columns: readonly ITableColumnViewModel[]): readonly SortingColumn[];
    /**
     * Обновляет настройки пользователя по сортировке в соответствии с текущим состоянием объектов колонок.
     * Возвращает признак того, что настройки были изменены.
     * @param columns Список объектов колонок, на основании которых определяются настройки сортировки.
     * @returns `true`, если настройки были изменены; `false` в противном случае.
     */
    updateSortingSettings(columns: readonly SortingColumn[]): boolean;
    /**
     * Возвращает признак того, что настройки сортировки соответствуют значениям по умолчанию.
     * @returns `true`, если настройки сортировки соответствуют значениям по умолчанию; `false` в противном случае.
     */
    sortingSettingsAreDefault(): boolean;
    /**
     * Сбрасывает настройки пользователя по сортировке в соответствии с текущим состоянием элемента управления.
     * Возвращает признак того, что настройки были изменены.
     * @returns `true`, если настройки были изменены; `false` в противном случае.
     */
    resetSortingSettings(): boolean;
    /**
     * Возвращает упорядоченный список алиасов колонок для отображения в соответствии с указанным списком.
     * @param columns Список объектов колонок, на основании которых определяются настройки порядка колонок.
     * @returns Упорядоченный список алиасов колонок.
     */
    getOrderedColumnsFromTable(columns: readonly ITableColumnViewModel[]): string[];
    /**
     * Возвращает упорядоченный список алиасов колонок для отображения в соответствии с настройками.
     * @returns Упорядоченный список алиасов колонок.
     */
    getOrderedColumnsFromSettings(): string[];
    /**
     * Обновляет настройки пользователя по указанному порядку следования алиасов колонок.
     * Возвращает признак того, что настройки пользователя были изменены.
     * @param orderedColumns Упорядоченный список алиасов колонок. Рекомендуется указать либо текущий список,
     * либо список из таблицы {@link getOrderedColumnsFromTable}.
     * @param defaultOrderedColumns Список алиасов колонок по умолчанию, который был получен вызовом метода {@link getOrderedColumnsFromTable}.
     * @returns `true`, если настройки были изменены; `false` в противном случае.
     */
    updateOrderedColumnsSettings(orderedColumns: readonly string[], defaultOrderedColumns: readonly string[] | null): boolean;
    /**
     * Возвращает признак того, что настройки порядка колонок соответствуют значениям по умолчанию.
     * @returns `true`, если настройки порядка колонок соответствуют значениям по умолчанию; `false` в противном случае.
     */
    orderedColumnsSettingsAreDefault(): boolean;
    /**
     * Упорядочивает переданный набор колонок {@link columns} в соответствии с настройками порядка колонок.
     * @param columns Список колонок, который должен быть упорядочен.
     * @param reorderFunc Функция, изменяющая порядок колонок. Если указано `null` или `undefined`,
     * то порядок колонок будет изменён непосредственно в массиве {@link columns}.
     * @param defaultOrderedColumns Упорядоченный список алиасов колонок по умолчанию. Используется в случае, если в настройках отсутствует
     * информация по порядку колонок (поскольку установлен порядок по умолчанию).
     * Если указано `null` или `undefined`, и в настройках порядок колонок не определён,
     * то метод не изменяет порядок в списке {@link columns} и возвращает `false`.
     * @returns `true`, если порядок {@link columns} был изменён; `false` в противном случае.
     */
    reorderColumnsFromSettings(columns: readonly ITableColumnViewModel[], defaultOrderedColumns?: readonly string[] | null, reorderFunc?: ((sourceIndex: number, targetIndex: number) => void) | null): boolean;
    reorderColumnsFromSettings(columns: ITableColumnViewModel[], defaultOrderedColumns?: readonly string[] | null): boolean;
    /**
     * Возвращает признак того, что в элементе управления отсутствуют применённые настройки пользователя.
     * @returns Признак того, что в элементе управления отсутствуют применённые настройки пользователя.
     */
    userSettingsAreDefault(): boolean;
}
