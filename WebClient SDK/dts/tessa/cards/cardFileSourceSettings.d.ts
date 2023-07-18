import { CardFileSource } from './cardFileSource';
export declare class CardFileSourceSettings {
    private constructor();
    private static _instance;
    static get instance(): CardFileSourceSettings;
    private _card;
    private _sources;
    private _default;
    rebuildCacheIfRequired(): boolean;
    rebuildCache(): boolean;
    tryGet(id: number): CardFileSource | null;
    tryGetDefault(): CardFileSource | null;
}
