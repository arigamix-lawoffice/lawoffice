import { WorkplaceCompositeMetadataSealed } from './workplaceCompositeMetadata';
import { ItemPropertiesSealed } from './properties';
import { ViewColumnMetadataSealed } from '../metadata';
import { TagsPosition } from 'tessa/ui/tags/tagsPosition';
export declare class ColumnSetting {
    constructor(alias: string, propertyContainer: ItemPropertiesSealed);
    readonly alias: string;
    grouping: boolean;
    orderPosition: number;
    visibility: boolean;
    private static boolConvert;
    private static intConvert;
}
export declare class ColumnSettings {
    private _owner;
    private _scopeName;
    constructor(_owner: WorkplaceCompositeMetadataSealed, _scopeName: string);
    private _columnContainer;
    private _columns;
    get columnContainer(): ItemPropertiesSealed | null;
    get columns(): Map<string, ColumnSetting>;
    private initialize;
    private rebuildColumns;
    private getOrCreateColumnContainer;
    changeGrouping(oldColumn: ViewColumnMetadataSealed | null, newColumn: ViewColumnMetadataSealed | null, rebuild?: boolean): void;
    private changeGroupingInternal;
    changeVisibility(columnName: string, visible: boolean, rebuild?: boolean): void;
    changeOrder(columnName: string, newOrder: number, rebuild?: boolean): void;
    resetAll(): void;
    persistAll(): void;
    hasChanges(): boolean;
    hasPositionSettings(): boolean;
    setTagsPosition(tagsPosition: TagsPosition): void;
    getTagsPosition(): TagsPosition | null;
}
