import { Card } from './card';
import { CardTaskDialogOpenMode } from './cardTaskDialogOpenMode';
import { CardTaskDialogStoreMode } from './cardTaskDialogStoreMode';
import { CardTaskDialogButtonInfo } from './cardTaskDialogButtonInfo';
import { CardTaskDialogNewMethod } from './cardTaskDialogNewMethod';
import { IStorage, StorageObject, ArrayStorage } from 'tessa/platform/storage';
/**
 * Параметры этапа/действия "Диалог".
 */
export declare class CardTaskCompletionOptionSettings extends StorageObject {
    /**
     * Инициализирует новый экземпляр класса {@link CardTaskCompletionOptionSettings}.
     * @param {IStorage} storage Хранилище, декоратором для которого является создаваемый объект.
     */
    constructor(storage: IStorage);
    static readonly completionOptionIdKey: string;
    static readonly dialogTypeIdKey: string;
    static readonly taskButtonCaptionKey: string;
    static readonly dialogNameKey: string;
    static readonly dialogAliasKey: string;
    static readonly dialogCardKey: string;
    static readonly persistentDialogCardIdKey: string;
    static readonly storeModeKey: string;
    static readonly openModeKey: string;
    static readonly buttonsKey: string;
    static readonly preparedNewCardKey: string;
    static readonly preparedNewCardSignatureKey: string;
    static readonly infoKey: string;
    static readonly displayValueKey: string;
    static readonly keepFilesKey: string;
    static readonly cardNewMethodKey: string;
    static readonly isCloseWithoutConfirmationKey: string;
    /**
     * Идентификатор варианта завершения задания этапа "Диалог" для которого указаны параметры описываемые этим экземпляром.
     */
    get completionOptionId(): guid;
    set completionOptionId(value: guid);
    /**
     * Идентификатор типа карточки или шаблона.
     */
    get dialogTypeId(): guid;
    set dialogTypeId(value: guid);
    /**
     * Текст кнопки в задании.
     */
    get taskButtonCaption(): string;
    set taskButtonCaption(value: string);
    /**
     * Имя диалога (для расширений).
     */
    get dialogName(): string;
    set dialogName(value: string);
    /**
     * Алиас диалога.
     */
    get dialogAlias(): string;
    set dialogAlias(value: string);
    /**
     * Карточка диалога.
     */
    get dialogCard(): Card | null;
    set dialogCard(value: Card | null);
    /**
     * Идентификатор карточки сохранённой в режиме {@link CardTaskDialogStoreMode.Card}.
     */
    get persistentDialogCardId(): guid;
    set persistentDialogCardId(value: guid);
    /**
     * Режим сохранения карточки диалога.
     */
    get storeMode(): CardTaskDialogStoreMode;
    set storeMode(value: CardTaskDialogStoreMode);
    /**
     * Режим открытия диалогового окна.
     */
    get openMode(): CardTaskDialogOpenMode;
    set openMode(value: CardTaskDialogOpenMode);
    /**
     * Коллекция, содержащая информацию о кнопках отображаемых в диалоге.
     */
    get buttons(): ArrayStorage<CardTaskDialogButtonInfo>;
    set buttons(value: ArrayStorage<CardTaskDialogButtonInfo>);
    /**
     * Массив байт, представляющий сериализованную карточку-заготовку.
     */
    get preparedNewCard(): string;
    set preparedNewCard(value: string);
    /**
     * Массив байт, представляющий цифровую подпись для сериализованной карточки-заготовка.
     */
    get preparedNewCardSignature(): string;
    set preparedNewCardSignature(value: string);
    /**
     * Дополнительная пользовательская информация.
     */
    get info(): IStorage;
    set info(value: IStorage);
    /**
     * Отображаемое имя карточки, используемое при отсутствии Digest, или undefined, если отображаемое имя вычисляется автоматически.
     */
    get displayValue(): string | undefined;
    set displayValue(value: string | undefined);
    /**
     * Признак, определяющий, нужно ли сохранять файлы после завершения диалога или нет.
     */
    get keepFiles(): boolean;
    set keepFiles(value: boolean);
    /**
     * Способ создания карточки диалога.
     */
    get cardNewMethod(): CardTaskDialogNewMethod;
    set cardNewMethod(value: CardTaskDialogNewMethod);
    /**
     * Признак, определяющий, нужно ли не предупреждать при закрытии диалога без изменений.
     */
    get isCloseWithoutConfirmation(): boolean;
    set isCloseWithoutConfirmation(value: boolean);
    private static readonly _cardTaskDialogButtonInfoFactory;
}
