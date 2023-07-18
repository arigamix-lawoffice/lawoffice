import { CleanupFieldsTypeExtensionSettings } from './cleanupFieldsTypeExtensionSettings';
import { TypeExtensionContext } from 'tessa/cards';
export declare class CleanupFieldsSettingsCache {
    private constructor();
    private static _instance;
    static get instance(): CleanupFieldsSettingsCache;
    private _cache;
    getExtesionsSettings(context: TypeExtensionContext): CleanupFieldsTypeExtensionSettings | null;
    private ititializeSettings;
}
