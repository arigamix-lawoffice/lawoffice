import { Card } from './card';
import { CardRow } from './cardRow';
import { CardFileVersion } from './cardFileVersion';
import { CardFileFlags } from './cardFileFlags';
import { CardFileState } from './cardFileState';
import { CardFileReplacementResult } from './cardFileReplacementResult';
import { CardInfoStorageObject } from './cardInfoStorageObject';
import { CardFileContentSource } from './cardFileContentSource';
import { CardRemoveChangesDeletedHandling } from './cardRemoveChangesDeletedHandling';
import { MapStorage, IStorage, IStorageValueFactory, ArrayStorage } from 'tessa/platform/storage';
import { EventHandler } from 'tessa/platform/eventHandler';
export declare class CardFile extends CardInfoStorageObject {
    constructor(storage?: IStorage);
    private _lastVersion;
    get lastVersion(): CardFileVersion | null;
    static readonly rowIdKey: string;
    static readonly typeIdKey: string;
    static readonly typeNameKey: string;
    static readonly typeCaptionKey: string;
    static readonly categoryIdKey: string;
    static readonly categoryCaptionKey: string;
    static readonly originalFileIdKey: string;
    static readonly originalVersionRowIdKey: string;
    static readonly taskIdKey: string;
    static readonly newVersionTagsKey: string;
    static readonly optionsKey: string;
    static readonly nameKey: string;
    static readonly sizeKey: string;
    static readonly versionRowIdKey: string;
    static readonly versionNumberKey: string;
    static readonly hashKey: string;
    static readonly requestInfoKey = "RequestInfo";
    static readonly isVirtualKey: string;
    static readonly cardKey: string;
    static readonly versionsKey: string;
    static readonly sectionRowsKey: string;
    static readonly storeSourceKey: string;
    static readonly externalSourceKey: string;
    static readonly systemStateKey: string;
    static readonly systemFlagsKey: string;
    static readonly systemVersionsLoadedKey: string;
    get rowId(): guid;
    set rowId(value: guid);
    get typeId(): guid;
    set typeId(value: guid);
    get typeName(): string;
    set typeName(value: string);
    get typeCaption(): string;
    set typeCaption(value: string);
    get categoryId(): guid | null;
    set categoryId(value: guid | null);
    get categoryCaption(): string | null;
    set categoryCaption(value: string | null);
    get originalFileId(): guid | null;
    set originalFileId(value: guid | null);
    get originalVersionRowId(): guid | null;
    set originalVersionRowId(value: guid | null);
    get taskId(): guid | null;
    set taskId(value: guid | null);
    get newVersionTags(): string | null;
    set newVersionTags(value: string | null);
    /**
     * Сериализованные в типизированный JSON настройки файла. Могут быть равны `null` или пустой строке, если настройки не заданы.
     * Для установки значения рекомендуется использовать метод {@link CardFile.setOptions}, а для получения - {@link CardFile.deserializeOptions}.
     */
    get options(): string | null;
    set options(value: string | null);
    get name(): string;
    set name(value: string);
    get versionRowId(): guid;
    set versionRowId(value: guid);
    get versionNumber(): number;
    set versionNumber(value: number);
    get hash(): string | null;
    set hash(value: string | null);
    get requestInfo(): IStorage;
    set requestInfo(value: IStorage);
    get isVirtual(): boolean;
    set isVirtual(value: boolean);
    get storeSource(): number;
    set storeSource(value: number);
    get card(): Card;
    set card(value: Card);
    get versions(): ArrayStorage<CardFileVersion>;
    set versions(value: ArrayStorage<CardFileVersion>);
    get versionsLoaded(): boolean;
    set versionsLoaded(value: boolean);
    get sectionRows(): MapStorage<CardRow>;
    set sectionRows(value: MapStorage<CardRow>);
    get state(): CardFileState;
    set state(value: CardFileState);
    get flags(): CardFileFlags;
    set flags(value: CardFileFlags);
    get size(): number;
    set size(value: number);
    get externalSource(): CardFileContentSource | null;
    set externalSource(value: CardFileContentSource | null);
    tryGetCard(): Card | null | undefined;
    tryGetSectionRows(): MapStorage<CardRow> | null | undefined;
    tryGetVersions(): ArrayStorage<CardFileVersion> | null | undefined;
    tryGetRequestInfo(): IStorage | null | undefined;
    private static readonly _rowFactory;
    private static readonly _versionFactory;
    invalidateLastVersion(): void;
    setCard(card: Card): void;
    setReplacedState(): CardFileReplacementResult;
    revertReplacedState(): CardFileReplacementResult;
    hasChanges(checkStates?: boolean): boolean;
    hasPendingStateChanges(): boolean;
    removeChanges(deletedHandling?: CardRemoveChangesDeletedHandling): boolean;
    updateState(): boolean;
    hasSize(): boolean;
    resetSize(): void;
    ensureCacheResolved(): void;
    /**
     * Десериализует настройки {@link CardFile.options}. Возвращаемый объект не равен `null`.
     * Если настройки не заданы, то возвращается пустой объект.
     * @returns Настройки {@link CardFile.options}.
     */
    deserializeOptions(): IStorage;
    /**
     * Устанавливает значение свойства {@link options} с выполнением сериализации указанного хранилища.
     * @param storage Хранилище, которое сериализуется в json для свойства {@link options}. Может быть равно `null`.
     */
    setOptions(storage: IStorage | null): void;
    hasNewVersionTag(tag: string): boolean;
    addNewVersionTag(tag: string): void;
    removeNewVersionTag(tag: string): void;
    isEmpty(): boolean;
    clean(): void;
    readonly stateChanged: EventHandler<(args: CardFileStateEventArgs) => void>;
}
export declare class CardFileFactory implements IStorageValueFactory<CardFile> {
    getValue(storage: IStorage): CardFile;
    getValueAndStorage(): {
        value: CardFile;
        storage: IStorage;
    };
}
export interface CardFileStateEventArgs {
    readonly file: CardFile;
    readonly oldState: CardFileState;
    readonly newState: CardFileState;
}
