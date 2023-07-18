import { IFormattingSettings } from './formattingSettings';
export declare class FormattingSettingsCache {
    private constructor();
    private static _instance;
    static get instance(): FormattingSettingsCache;
    private _items;
    get items(): ReadonlyMap<string, IFormattingSettings>;
    init(items: IFormattingSettings[] | null | undefined): void;
}
