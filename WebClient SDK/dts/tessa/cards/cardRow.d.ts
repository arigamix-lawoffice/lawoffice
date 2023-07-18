import { CardRowState } from './cardRowState';
import { CardTableType } from './cardTableType';
import { FieldMapStorage } from './fieldMapStorage';
import { CardRemoveChangesDeletedHandling } from './cardRemoveChangesDeletedHandling';
import { ICloneable } from 'tessa/platform';
import { IStorage, IStorageCleanable, IStorageNotificationReciever, IStorageObjectStateProvider, IStorageValueFactory, IKeyedStorageValueFactory } from 'tessa/platform/storage';
import { EventHandler } from 'tessa/platform/eventHandler';
/**
 * Строка коллекционной или древовидной секции.
 */
export declare class CardRow extends FieldMapStorage implements ICloneable<CardRow>, IStorageNotificationReciever, IStorageObjectStateProvider, IStorageCleanable {
    /**
     * Создаёт экземпляр класса с указанием хранилища, декоратором для которого является создаваемый объект.
     * @param storage Хранилище, декоратором для которого является создаваемый объект.
     */
    constructor(storage?: IStorage);
    /**
     * Ключ в хранилище объекта, который соответствует свойству {@link Card.rowId}.
     */
    static readonly rowIdKey = "RowID";
    /**
     * Ключ в хранилище объекта, который соответствует свойству {@link Card.parentRowId}.
     */
    static readonly parentRowIdKey = "ParentRowID";
    /**
     * Ключ в хранилище объекта, который соответствует свойству {@link Card.state}.
     */
    static readonly systemStateKey: string;
    /**
     * Ключ в хранилище объекта, который соответствует списку изменённых полей.
     */
    static readonly systemChangedKey: string;
    /**
     * Ключ в хранилище объекта, который соответствует специальному полю
     * для сортировки строк при вставке/удалении {@link Card.sortingOrder}.
     */
    static readonly systemSortingOrderKey: string;
    /**
     * Идентификатор строки.
     */
    get rowId(): guid;
    set rowId(value: guid);
    /**
     * Идентификатор родительской строки в древовидной секции.
     */
    get parentRowId(): guid | null;
    set parentRowId(value: guid | null);
    /**
     * Состояние строки.
     * @remarks
     * Значение по умолчанию {@link CardRowState.None} возвращается даже в том случае,
     * если объект с соответствующим ключом отсутствует в хранилище.
     */
    get state(): CardRowState;
    set state(value: CardRowState);
    /**
     * Порядок строки при сортировке строк для вставки, задаваемый вручную при указании типа сортировки
     * {@link CardRowSortingType.Manual} для секции. Порядок строк при удалении будет обратным.
     * @remarks
     * Значение по умолчанию `0` возвращается даже в том случае,
     * если объект с соответствующим ключом отсутствует в хранилище.
     */
    get sortingOrder(): number;
    set sortingOrder(value: number);
    /**
     * Возвращает идентификатор строки {@link CardRow.rowId}, если он присутствует в хранилище,
     * или `null` в противном случае.
     * @returns Идентификатор строки {@link CardRow.rowId}, если он присутствует в хранилище,
     * или `null` в противном случае.
     */
    tryGetRowId(): guid | null | undefined;
    /**
     * Возвращает идентификатор родительской строки {@link CardRow.parentRowId}, если он присутствует в хранилище,
     * или `null` в противном случае.
     * @returns Идентификатор родительской строки {@link CardRow.parentRowId}, если он присутствует в хранилище,
     * или `null` в противном случае.
     */
    tryGetParentRowId(): guid | null | undefined;
    /**
     * Возвращает состояние строки {@link CardRow.state}, если оно присутствует в хранилище,
     * или `null` в противном случае.
     * @returns Состояние строки {@link CardRow.state}, если оно присутствует в хранилище,
     * или `null` в противном случае.
     */
    tryGetState(): CardRowState | null | undefined;
    /**
     * Возвращает порядок строки при сортировке строк для вставки
     * или `null`, если порядок ещё не был указан.
     * @returns Порядок строки при сортировке строк для вставки
     * или `null`, если порядок ещё не был указан.
     */
    tryGetSortingOrder(): number | null | undefined;
    private rowChanged;
    /**
     * Устанавливает хранилище объекта в соответствии с переданной коллекцией ключ / значение.
     * @param row Строка, используемая в качестве хранилища данных.
     */
    setStorage(row: CardRow): void;
    /**
     * Создаёт полную копию хранилища заданной строки в текущей строке.
     * При этом удаляются все поля и служебная информация из текущей строки, после чего она копируется из заданной.
     *
     * Подписчики на события и другая информация, не являющаяся частью хранилища текущего объекта, остаётся неизменной.
     * @param row Строка, из которой производится копирование полей и служебной информации.
     */
    setFrom(row: CardRow): void;
    private static readonly platformFieldKeys;
    private static readonly platformHierarchyFieldKeys;
    /**
     * Возвращает список системных ключей, используемых в объекте {@link CardRow}, в зависимости от типа коллекционной секции.
     * @param tableType Тип коллекционной секции.
     * @returns Список системных ключей.
     */
    static getPlatformKeys(tableType: CardTableType): string[];
    /**
     * Удаляет информацию о всех полях строки, которые не были изменены
     * посредством {@link IStorageObjectStateProvider} и не являются служебными.
     * @param tableType Тип коллекционной или древовидной секции карточки, в которую включена строка.
     * @remarks
     * Метод удаляет информацию об изменённых полях, поэтому повторный его вызов приведёт
     * к удалению всех полей. Метод не удаляет поля {@link CardRow.rowId} и {@link CardRow.state} для любой секции,
     * а также {@link CardRow.parentRowId} для древовидной секции.
     * Метод удаляет всю информацию, кроме служебной, о строках, у которых {@link CardRow.state} равен
     * {@link CardRowState.Deleted}, и не удаляет информацию у строк {@link CardRowState.Inserted}.
     * Рекомендуется вызывать этот метод перед вызовом {@link CardRow.clean}.
     */
    removeAllButChanged(tableType: CardTableType): void;
    /**
     * Событие, возникающее при изменении состояния строки {@link CardRow.state}.
     */
    stateChanged: EventHandler<(e: CardRowStateChangedEventArgs) => void>;
    /**
     * @inheritDoc
     */
    clone(): CardRow;
    /**
     * @inheritDoc
     */
    notifyStorageUpdated(): void;
    /**
     * @inheritDoc
     */
    isChanged(key: string): boolean;
    /**
     * @inheritDoc
     */
    setChanged(key: string, isChanged: boolean): IStorageObjectStateProvider;
    /**
     * @inheritDoc
     */
    getAllChanges(): string[];
    /**
     * Возвращает признак того, что объект содержит изменённые поля.
     * @param tableType Тип секции, в которую включена строка.
     * @returns `true`, если объект содержит изменённые поля; `false` в противном случае.
     * @remarks
     * Метод вернёт `false` в случае, если среди изменённых полей присутствуют только служебные поля.
     * Метод не учитывает состояние строки {@link CardRow.state}.
     */
    hasChanges(tableType?: CardTableType): boolean;
    /**
     * @inheritDoc
     */
    clearChanges(): IStorageObjectStateProvider;
    /**
     * Выполняет удаление информации по состояниям, из которой можно было бы определить,
     * что строка изменена. Возвращает признак того, что при этом были внесены изменения.
     * @param tableType Тип секции, в которую включена строка.
     * @param deletedHandling Способ обработки удалённых строк, файлов и заданий.
     * @returns `true`, если в процессе удаления были внесены изменения; `false` в противном случае.
     */
    removeChanges(tableType: CardTableType, deletedHandling?: CardRemoveChangesDeletedHandling): boolean;
    /**
     * @inheritDoc
     */
    clean(): void;
}
/**
 * Фабрика, используемая для создания объекта {@link CardRow},
 * который содержит строки коллекционной и древовидной секции.
 */
export declare class CardRowFactory implements IStorageValueFactory<CardRow> {
    getValue(storage: IStorage): CardRow;
    getValueAndStorage(): {
        value: CardRow;
        storage: IStorage;
    };
}
/**
 * Фабрика, используемая для создания объекта {@link CardRow},
 * который содержит строки коллекционной и древовидной секции.
 */
export declare class KeyedCardRowFactory implements IKeyedStorageValueFactory<string, CardRow> {
    getValue(_key: string, storage: IStorage): CardRow;
    getValueAndStorage(_key: string): {
        value: CardRow;
        storage: IStorage;
    };
}
/**
 * Аргументы события, происходящего при изменении свойства {@link CardRow.State} в объекте
 * с информацией о строке коллекционной или древовидной секции в пакете карточки {@link CardRow}.
 */
export interface CardRowStateChangedEventArgs {
    /**
     * Cтрока коллекционной или древовидной секции.
     */
    row: CardRow;
    /**
     * Предыдущее состояние.
     */
    oldState: CardRowState;
    /**
     * Текущее состояние.
     */
    newState: CardRowState;
}
