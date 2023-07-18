import { IToolbarGroup } from './interfaces';
export declare type GroupConfig = Partial<IToolbarGroup>;
export declare function filler(id: string): IToolbarGroup;
export declare function group(id: string, items: string[], config?: GroupConfig): IToolbarGroup;
export declare function apply(group: IToolbarGroup, config: GroupConfig): IToolbarGroup;
export declare function center(group: IToolbarGroup): IToolbarGroup;
export declare function end(group: IToolbarGroup): IToolbarGroup;
export declare function stretch(group: IToolbarGroup): IToolbarGroup;
export declare function vertical(group: IToolbarGroup): IToolbarGroup;
export declare function permanent(group: IToolbarGroup): IToolbarGroup;
export declare function split(group: IToolbarGroup, match?: (item: string) => boolean): IToolbarGroup;
export declare function isInternalItem(n: Node): boolean;
export declare function getGroupId(n: Node): string | undefined;
export declare function deriveSplitterId(group: IToolbarGroup, index: number): string;
