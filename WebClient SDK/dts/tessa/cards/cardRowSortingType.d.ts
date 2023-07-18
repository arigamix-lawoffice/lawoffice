/**
 * Тип сортировки строк для коллекционной или древовидной секции,
 * которая используется при вставке и удалении строк в процессе сохранения карточки.
 */
export declare enum CardRowSortingType {
    /**
     * Сортировка производится автоматически при необходимости.
     *
     * При этом сортируются только строки древовидной секции с учётом порядка следования
     * родительских строк по отношению к дочерним.
     */
    Auto = 0,
    /**
     * Порядок сортировки устанавливается вручную для каждой строки через свойство {@link CardRow.sortingOrder}.
     *
     * Для древовидных секций строки при этом будут отсортированы без учёта порядка следования
     * родительских строк по отношению к дочерним.
     */
    Manual = 1
}