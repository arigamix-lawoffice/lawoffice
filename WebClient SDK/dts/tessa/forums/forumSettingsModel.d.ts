import { ValidationStorageObject } from 'tessa/platform/validation';
import { IStorage } from 'tessa/platform/storage';
export declare class ForumSettingsModel extends ValidationStorageObject {
    constructor(storage?: IStorage);
    static readonly contentRatioKey: string;
    static readonly isFullSizeKey: string;
    static readonly inputBlockHeight: string;
    get contentRatio(): number | null;
    set contentRatio(value: number | null);
    get isFullSize(): boolean;
    set isFullSize(value: boolean);
    get inputBlockHeight(): number;
    set inputBlockHeight(value: number);
}
