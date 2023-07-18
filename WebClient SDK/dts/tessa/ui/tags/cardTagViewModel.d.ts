import { MenuAction } from 'tessa/ui/menuAction';
import { TagInfoModel as TagInfoModel } from './tagInfoModel';
export declare class CardTagViewModel {
    readonly model: TagInfoModel;
    constructor(model: TagInfoModel);
    private _isDeleted;
    static maxTagLength: number;
    get id(): string;
    get caption(): string;
    get title(): string;
    get background(): string;
    get foreground(): string;
    get isCommon(): boolean;
    get isDeleted(): boolean;
    set isDeleted(value: boolean);
    getContextMenu(): MenuAction[];
}
