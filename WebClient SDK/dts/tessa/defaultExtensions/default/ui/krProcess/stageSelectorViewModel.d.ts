import { StageGroup } from './stageGroup';
import { StageType } from './stageType';
export declare class StageSelectorViewModel {
    constructor(group: StageGroup | null, cardId: guid, typeId: guid);
    private _typesCache;
    readonly group: StageGroup | null;
    readonly cardId: guid;
    readonly typeId: guid;
    readonly groups: ReadonlyArray<StageGroup>;
    _types: ReadonlyArray<StageType>;
    get types(): ReadonlyArray<StageType>;
    set types(value: ReadonlyArray<StageType>);
    selectedGroupIndex: number;
    get selectedGroup(): StageGroup | null;
    selectedTypeIndex: number;
    get selectedType(): StageType | null;
    getGroupTypesFunc: (group: StageGroup | null, cardId: guid, typeId: guid) => Promise<StageGroup[]>;
    getStageTypesFunc: (groupType: guid, cardId: guid, typeId: guid) => Promise<StageType[]>;
    refresh(): Promise<void>;
    updateType(): Promise<void>;
    setSelectedGroupIndex(index: number): void;
    setSelectedTypeIndex(index: number): void;
}
