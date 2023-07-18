import { IStorage } from 'tessa/platform/storage';
import { TagClickMode } from 'tessa/ui/tags/tagClickMode';
export declare class TagInfo {
    constructor(storage?: IStorage);
    id: guid;
    name: string;
    background: number;
    foreground: number | null;
    isCommon: boolean;
    clickMode: TagClickMode;
    getStorage(): IStorage;
}
