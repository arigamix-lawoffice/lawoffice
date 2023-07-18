import { Theme } from './theme';
import { IStorage } from 'tessa/platform/storage';
export declare class ThemeManager {
    private constructor();
    private static _instance;
    static get instance(): ThemeManager;
    private _themes;
    private _isInitialized;
    get themes(): Map<string, Theme>;
    get isInitialized(): boolean;
    get currentTheme(): Theme;
    readonly wallpapersNameMap: Map<string, string>;
    initialize(themes: IStorage): void;
    tryGetTheme(name: string): Theme | null;
}
