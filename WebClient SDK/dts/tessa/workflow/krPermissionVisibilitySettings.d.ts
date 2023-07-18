import { IStorage } from "tessa/platform/storage";
export declare class KrPermissionVisibilitySettings {
    readonly alias: string;
    readonly controlType: number;
    readonly isHidden: boolean;
    constructor(alias: string, controlType: number, isHidden: boolean);
    toStorage(): IStorage;
    static fromStorage(storage: IStorage): KrPermissionVisibilitySettings;
}
