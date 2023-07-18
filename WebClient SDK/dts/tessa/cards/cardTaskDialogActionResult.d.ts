import { IStorage, StorageObject } from 'tessa/platform/storage';
import { ICloneable } from 'tessa/platform';
import { Card } from './card';
import { CardTaskDialogStoreMode } from './cardTaskDialogStoreMode';
/**
 * Предоставляет информацию о завершении диалога.
 */
export declare class CardTaskDialogActionResult extends StorageObject implements ICloneable<CardTaskDialogActionResult> {
    /**
     * Инициализирует новый экземпляр класса {@link CardTaskDialogActionResult}.
     * @param {IStorage} storage Хранилище, декоратором для которого является создаваемый объект.
     */
    constructor(storage?: IStorage);
    static readonly mainCardIdKey: string;
    static readonly taskIdKey: string;
    static readonly pressedButtonNameKey: string;
    static readonly dialogCardIdKey: string;
    static readonly dialogCardKey: string;
    static readonly storeModeKey: string;
    static readonly completeDialogKey: string;
    static readonly keepFilesKey: string;
    /**
     * Возвращает или задаёт идентификатор основной карточки в которой открыт диалог.
     */
    get mainCardId(): guid;
    set mainCardId(value: guid);
    /**
     * Возвращает или задаёт идентификатор задания диалога.
     */
    get taskId(): guid;
    set taskId(value: guid);
    /**
     * Возвращает или задаёт имя (алиас) кнопки, нажатие на которую привело к закрытию диалога.
     */
    get pressedButtonName(): string | null;
    set pressedButtonName(value: string | null);
    /**
     * Возвращает или задаёт идентификатор карточки диалога.
     */
    get dialogCardId(): guid;
    set dialogCardId(value: guid);
    /**
     * Возвращает или задаёт карточку диалога.
     */
    get dialogCard(): Card | null;
    set dialogCard(value: Card | null);
    /**
     * Возвращает или задаёт режим сохранения диалога.
     */
    get storeMode(): CardTaskDialogStoreMode;
    set storeMode(value: CardTaskDialogStoreMode);
    /**
     * Возвращает или задаёт флаг завершения диалога.
     */
    get completeDialog(): boolean;
    set completeDialog(value: boolean);
    /**
     * Возвращает или задаёт признак, нужно ли сохранять файлы после завершения диалога или нет.
     * Актуален только для диалога с {@link storeMode} равным {@link CardTaskDialogStoreMode.Settings}.
     */
    get keepFiles(): boolean;
    set keepFiles(value: boolean);
    /**
     * Устанавливает идентификатор карточки диалога. Если в объекте уже указана карточка, то она устанавливается в null.
     *
     * @param {guid} cardId Идентификатор карточки диалога.
     */
    setDialogCardId(cardId: guid): void;
    /**
     * Устанавливает карточку диалога. Вместе с этим будет обновлен идентификатор карточки {@link dialogCardId}.
     *
     * @param {Card | null} card Карточка диалога.
     */
    setDialogCard(card: Card | null): void;
    clone(): CardTaskDialogActionResult;
}
