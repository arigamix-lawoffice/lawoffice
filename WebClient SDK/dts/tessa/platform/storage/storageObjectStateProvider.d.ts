import { IStorage } from './storage';
import { IStorageCleanable } from './storageCleanable';
import { IStorageNotificationReciever } from './storageNotificationReciever';
import { TypedField } from 'tessa/platform/typedField';
/**
 * Предоставляет информацию о наличии изменений в значениях объектов хранилища.
 */
export interface IStorageObjectStateProvider {
    /**
     * Возвращает признак того, что значение объекта с ключом `key` было изменено.
     * @param key Ключ, по которому необходимо определить признак того, что значение соответствующего объекта было изменено.
     * @returns `true`, если значение объекта было изменено; `false`, если значение объекта осталось неизменным.
     */
    isChanged(key: string): boolean;
    /**
     * Устанавливает признак `isChanged`, определяющий, было ли изменено значение объекта с ключом `key`.
     * @param key Ключ, по которому необходимо установить признак наличия изменений в значении объекта.
     * @param isChanged Устанавливаемый признак наличия изменений в значении объекта с заданным ключом.
     * Равен `true`, если значение объекта считается изменённым; `false`, если значение объекта считается неизменным.
     * @returns Текущий объект для цепочки вызовов.
     */
    setChanged(key: string, isChanged: boolean): IStorageObjectStateProvider;
    /**
     * Возвращает коллекцию ключей всех объектов, значения которых были изменены.
     * @returns Коллекция ключей всех объектов, значения которых были изменены.
     */
    getAllChanges(): string[];
    /**
     * Возвращает признак того, что объект содержит изменённые поля.
     * @returns `true`, если объект содержит изменённые поля; `false` в противном случае.
     */
    hasChanges(): boolean;
    /**
     * Удаляет всю информацию об изменённых объектах.
     * @returns Текущий объект для цепочки вызовов.
     */
    clearChanges(): IStorageObjectStateProvider;
}
/**
 * Функция проверяет, что объект поддерживает интерфейс {@link IStorageObjectStateProvider}.
 * @param obj Объект, который требуется проверить.
 * @returns `true`, если объект поддерживает интерфейс; `false`, если интерфейс не поддерживается.
 */
export declare function isIStorageObjectStateProvider(obj: object): obj is IStorageObjectStateProvider;
/**
 * Предоставляет информацию о наличии изменений в значениях объектов хранилища {@link IStorage}.
 * Объект сохраняет служебную информацию в объект хранилища, располагающийся по заданному в конструкторе ключу.
 * @remarks
 * При отсутствии служебной информации в хранилище на момент создания объекта
 * эта информация не будет добавлена в хранилища до тех пор,
 * пока не произойдёт первый вызов метода {@link IStorageObjectStateProvider.setChanged}.
 */
export declare class StorageObjectStateProvider implements IStorageObjectStateProvider, IStorageNotificationReciever, IStorageCleanable {
    /**
     * Создаёт экземпляр класса с указанием хранилища `storage`, для которого объект
     * предоставляет информацию, и ключа `changedListKey`, по которому будет размещаться
     * служебная информация, необходимая объекту для отслеживания состояний.
     * @param storage Хранилище, для которого объект предоставляет информацию.
     * @param changedListKey Ключ, по которому будет размещаться информация, необходимая объекту для отслеживания состояний.
     */
    constructor(storage: IStorage, changedListKey: string);
    protected _storage: IStorage;
    protected _changedListKey: string;
    protected _changedList: TypedField[] | null;
    protected _changedHash: Set<string> | null;
    /**
     * @inheritDoc
     */
    isChanged(key: string): boolean;
    /**
     * @inheritDoc
     */
    setChanged(key: string, isChanged?: boolean): IStorageObjectStateProvider;
    /**
     * @inheritDoc
     */
    getAllChanges(): string[];
    /**
     * @inheritDoc
     */
    hasChanges(): boolean;
    /**
     * @inheritDoc
     */
    clearChanges(): IStorageObjectStateProvider;
    /**
     * @inheritDoc
     */
    notifyStorageUpdated(): void;
    /**
     * @inheritDoc
     */
    clean(): void;
}
