/// <reference types="react" />
import { GridColumnDisplayType } from 'components/cardElements/grid';
import { TextAlignProperty } from 'csstype';
import { Visibility } from 'tessa/platform';
import { ClassNameList } from 'tessa/ui/classNameList';
export declare class GridColumnViewModel {
    constructor(header: string, alignment?: TextAlignProperty);
    private _style?;
    private _visibility;
    private _displayType;
    private _isPermanent;
    readonly header: string;
    readonly alignment: TextAlignProperty;
    get style(): React.CSSProperties | undefined;
    set style(value: React.CSSProperties | undefined);
    get displayType(): GridColumnDisplayType;
    set displayType(type: GridColumnDisplayType);
    get visibility(): Visibility;
    set visibility(value: Visibility);
    readonly className: ClassNameList;
    get isPermanent(): boolean;
    set isPermanent(value: boolean);
}
