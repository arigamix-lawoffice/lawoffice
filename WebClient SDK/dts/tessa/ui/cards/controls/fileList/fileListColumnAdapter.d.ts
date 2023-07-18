import { GridColumnDisplayType, IGridColumnViewModel } from 'components/cardElements/grid';
import { Visibility } from 'tessa/platform';
export declare class FileListColumnAdapter implements IGridColumnViewModel {
    constructor(id: string, caption: string);
    private _id;
    private _caption;
    get id(): string;
    get visibility(): Visibility;
    get displayType(): GridColumnDisplayType;
    get caption(): string;
}
