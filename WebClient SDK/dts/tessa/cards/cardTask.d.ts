import { Card } from './card';
import { CardInfoStorageObject } from './cardInfoStorageObject';
import { CardRow } from './cardRow';
import { CardRowState } from './cardRowState';
import { CardTaskState } from './cardTaskState';
import { CardStoreMethod } from './cardStoreMethod';
import { CardRemoveChangesDeletedHandling } from './cardRemoveChangesDeletedHandling';
import { ICloneable } from 'tessa/platform';
import { IStorage, MapStorage, IStorageValueFactory, ArrayStorage } from 'tessa/platform/storage';
import { EventHandler } from 'tessa/platform/eventHandler';
import { CardTaskAssignedRole } from './cardTaskAssignedRole';
import { CardTaskSessionRole } from './cardTaskSessionRole';
export declare class CardTask extends CardInfoStorageObject implements ICloneable<CardTask> {
    constructor(storage?: IStorage);
    static readonly rowIdKey: string;
    static readonly actionKey: string;
    static readonly typeIdKey: string;
    static readonly typeNameKey: string;
    static readonly typeCaptionKey: string;
    static readonly userIdKey: string;
    static readonly userNameKey: string;
    static readonly authorIdKey: string;
    static readonly authorNameKey: string;
    static readonly optionIdKey: string;
    static readonly resultKey: string;
    static readonly plannedKey: string;
    static readonly plannedQuantsKey: string;
    static readonly inProgressKey: string;
    static readonly digestKey: string;
    static readonly parentRowIdKey: string;
    static readonly historyItemParentRowIdKey: string;
    static readonly processIdKey: string;
    static readonly processNameKey: string;
    static readonly processKindKey: string;
    static readonly postponedKey: string;
    static readonly postponedToKey: string;
    static readonly postponeCommentKey: string;
    static readonly cardKey: string;
    static readonly sectionRowsKey: string;
    static readonly systemStateKey: string;
    static readonly systemStoredStateKey: string;
    static readonly systemFlagsKey: string;
    static readonly timeZoneIdKey: string;
    static readonly timeZoneUtcOffsetMinutesKey: string;
    static readonly settingsKey: string;
    static readonly historySettingsKey: string;
    static readonly taskAssignedRolesKey: string;
    static readonly taskSessionRolesKey: string;
    static readonly calendarIDKey: string;
    static readonly calendarNameKey: string;
    static readonly formattedPlannedKey: string;
    private static readonly _cardRowFactory;
    get rowId(): guid;
    set rowId(value: guid);
    get action(): number;
    set action(value: number);
    get typeId(): guid;
    set typeId(value: guid);
    get typeName(): string;
    set typeName(value: string);
    get typeCaption(): string;
    set typeCaption(value: string);
    get userId(): guid | null;
    set userId(value: guid | null);
    get userName(): string | null;
    set userName(value: string | null);
    get authorId(): guid | null;
    set authorId(value: guid | null);
    get authorName(): string | null;
    set authorName(value: string | null);
    get optionId(): guid | null;
    set optionId(value: guid | null);
    get result(): string | null;
    set result(value: string | null);
    get planned(): string | null;
    set planned(value: string | null);
    get plannedQuants(): number | null;
    set plannedQuants(value: number | null);
    get inProgress(): string | null;
    set inProgress(value: string | null);
    get digest(): string | null;
    set digest(value: string | null);
    get parentRowId(): guid | null;
    set parentRowId(value: guid | null);
    get historyItemParentRowId(): guid | null;
    set historyItemParentRowId(value: guid | null);
    get processId(): guid | null;
    set processId(value: guid | null);
    get processName(): string | null;
    set processName(value: string | null);
    get processKind(): string | null;
    set processKind(value: string | null);
    get postponed(): string | null;
    set postponed(value: string | null);
    get postponedTo(): string | null;
    set postponedTo(value: string | null);
    get postponeComment(): string | null;
    set postponeComment(value: string | null);
    get card(): Card;
    set card(value: Card);
    get sectionRows(): MapStorage<CardRow>;
    set sectionRows(value: MapStorage<CardRow>);
    get state(): CardRowState;
    set state(value: CardRowState);
    get flags(): number;
    set flags(value: number);
    get storedState(): CardTaskState;
    set storedState(value: CardTaskState);
    get timeZoneId(): number | null;
    set timeZoneId(value: number | null);
    get timeZoneUtcOffsetMinutes(): number | null;
    set timeZoneUtcOffsetMinutes(value: number | null);
    get settings(): IStorage | null;
    set settings(value: IStorage | null);
    get historySettings(): IStorage | null;
    set historySettings(value: IStorage | null);
    /**
     * Записи функциональных ролей связанных с заданием
     */
    get taskAssignedRoles(): ArrayStorage<CardTaskAssignedRole>;
    set taskAssignedRoles(value: ArrayStorage<CardTaskAssignedRole>);
    /**
     * Список RowID ролей задания, к которым относится текущая сессия.
     */
    get taskSessionRoles(): ArrayStorage<CardTaskSessionRole>;
    set taskSessionRoles(value: ArrayStorage<CardTaskSessionRole>);
    get calendarID(): guid | null;
    set calendarID(value: guid | null);
    get calendarName(): string | null;
    set calendarName(value: string | null);
    get formattedPlanned(): string | null;
    set formattedPlanned(value: string | null);
    get isLocked(): boolean;
    isLockedExplicit: boolean | null;
    get isLockedEffective(): boolean;
    get isSystem(): boolean;
    get isCanPerform(): boolean;
    get isCanPerformAsDeputy(): boolean;
    get canPostpone(): boolean;
    set canPostpone(value: boolean);
    canPostponeExplicit: boolean | null;
    get IsPostpone(): boolean;
    get canPostponeEffective(): boolean;
    isHidden: boolean;
    readonly stateChanged: EventHandler<(args: CardTaskStateChangedEventArgs) => void>;
    clean(): void;
    tryGetCard(): Card | null | undefined;
    tryGetOrCreateCardForStore(storeMethod?: CardStoreMethod): Card | null | undefined;
    tryGetSectionRows(): MapStorage<CardRow> | null | undefined;
    private static readonly _cardTaskAssignedRolesFactory;
    private static readonly _cardTaskSessionFunctionRoleFactory;
    ensureCacheResolved(): void;
    setCard(card: Card): void;
    hasChanges(checkStates?: boolean): boolean;
    removeChanges(deletedHandling?: CardRemoveChangesDeletedHandling): boolean;
    hasPendingStateChanges(): boolean;
    updateState(): boolean;
    clearPendingChangesAndCard(): void;
    isEmpty(): boolean;
    tryGetTaskAssignedRoles(): ArrayStorage<CardTaskAssignedRole> | null | undefined;
    tryGetTaskSessionRoles(): ArrayStorage<CardTaskSessionRole> | null | undefined;
    clone(): CardTask;
}
export interface CardTaskStateChangedEventArgs {
    readonly task: CardTask;
    readonly oldState: CardRowState;
    readonly newState: CardRowState;
}
export declare class CardTaskFactory implements IStorageValueFactory<CardTask> {
    getValue(storage: IStorage): CardTask;
    getValueAndStorage(): {
        value: CardTask;
        storage: IStorage;
    };
}
