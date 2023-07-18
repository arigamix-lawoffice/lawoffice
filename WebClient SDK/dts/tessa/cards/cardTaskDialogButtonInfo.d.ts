import { CardButtonType } from './cardButtonType';
import { IStorage, StorageObject, IStorageValueFactory } from 'tessa/platform/storage';
/**
 * Информация о кнопке диалога.
 */
export declare class CardTaskDialogButtonInfo extends StorageObject {
    /**
     * Инициализирует новый экземпляр класса {@link CardTaskDialogButtonInfo}.
     * @param {IStorage} storage Хранилище, декоратором для которого является создаваемый объект.
     */
    constructor(storage: IStorage);
    static readonly nameKey: string;
    static readonly cardButtonTypeKey: string;
    static readonly captionKey: string;
    static readonly iconKey: string;
    static readonly cancelKey: string;
    static readonly orderKey: string;
    static readonly completeDialogKey: string;
    /**
     * Имя кнопки (алиас).
     */
    get name(): string;
    set name(value: string);
    /**
     * Тип кнопки.
     */
    get cardButtonType(): CardButtonType;
    set cardButtonType(value: CardButtonType);
    /**
     * Текст кнопки.
     */
    get caption(): string;
    set caption(value: string);
    /**
     * Название иконки кнопки.
     */
    get icon(): string;
    set icon(value: string);
    /**
     * Значение, показывающее, что диалог должен быть закрыт.
     */
    get cancel(): boolean;
    set cancel(value: boolean);
    /**
     * Порядковый номер кнопки.
     */
    get order(): number;
    set order(value: number);
    /**
     * Значение, показывающее, что диалог должен быть закрыт.
     */
    get completeDialog(): boolean;
    set completeDialog(value: boolean);
}
export declare class CardTaskDialogButtonInfoFactory implements IStorageValueFactory<CardTaskDialogButtonInfo> {
    getValue(storage: IStorage): CardTaskDialogButtonInfo;
    getValueAndStorage(): {
        value: CardTaskDialogButtonInfo;
        storage: IStorage;
    };
}
