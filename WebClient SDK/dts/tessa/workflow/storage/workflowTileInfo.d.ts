import { StorageObject, IStorage } from 'tessa/platform/storage';
export declare class WorkflowTileInfo extends StorageObject {
    constructor(storage?: IStorage);
    static readonly workflowTypeKey = "WorkflowType";
    static readonly idKey = "ID";
    static readonly orderKey = "Order";
    static readonly nameKey = "Name";
    static readonly captionKey = "Caption";
    static readonly iconKey = "Icon";
    static readonly tooltipKey = "Tooltip";
    static readonly isGlobalKey = "IsGlobal";
    static readonly askConfirmationKey = "AskConfirmation";
    static readonly confirmationMessageKey = "ConfirmationMessage";
    static readonly actionGroupingKey = "ActionGrouping";
    static readonly openEditorKey = "OpenEditor";
    static readonly nestedTilesKey = "NestedTiles";
    get workflowType(): string;
    get id(): guid;
    get order(): number;
    get name(): string;
    get caption(): string;
    get icon(): string;
    get tooltip(): string;
    get isGlobal(): boolean;
    get askConfirmation(): boolean;
    get confirmationMessage(): string;
    get actionGrouping(): boolean;
    get openEditor(): boolean;
    private _nestedTiles;
    get nestedTiles(): ReadonlyArray<WorkflowTileInfo>;
}
