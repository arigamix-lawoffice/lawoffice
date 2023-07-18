import { IStorage } from 'tessa/platform/storage';
export interface IUserSettings {
    data: IStorage;
    taskColor?: string;
    topicItemColor?: string;
    foregroundColors?: string[];
    backgroundColors?: string[];
    blockColors?: string[];
    customForegroundColors?: string[];
    customBackgroundColors?: string[];
    customBlockColors?: string[];
    frequentlyUsedEmojis?: string;
    maxTagsDisplayed: number;
    getTaskColors(functionRoleId: guid): ITaskColorInfo;
}
export interface ITaskColorInfo {
    normalColor: string;
    deputyColor: string;
}
export declare class UserSettings implements IUserSettings {
    _data: IStorage<any>;
    _taskColor?: string;
    _maxTagsDisplayed?: number | undefined;
    _topicItemColor?: string | undefined;
    _customForegroundColors?: string[];
    _customBackgroundColors?: string[];
    _customBlockColors?: string[];
    _frequentlyUsedEmojis?: string;
    get data(): IStorage;
    set data(value: IStorage);
    get maxTagsDisplayed(): number;
    set maxTagsDisplayed(value: number | undefined);
    get taskColor(): string | undefined;
    set taskColor(value: string | undefined);
    get topicItemColor(): string | undefined;
    set topicItemColor(value: string | undefined);
    get customForegroundColors(): string[] | undefined;
    set customForegroundColors(value: string[] | undefined);
    get frequentlyUsedEmojis(): string | undefined;
    set frequentlyUsedEmojis(value: string | undefined);
    get customBackgroundColors(): string[] | undefined;
    set customBackgroundColors(value: string[] | undefined);
    get customBlockColors(): string[] | undefined;
    set customBlockColors(value: string[] | undefined);
    getTaskColors(functionRoleId: guid): ITaskColorInfo;
    private taskColorsByFunctionRoleID;
    private ensureTaskColorsInitialized;
    private clearFields;
}
