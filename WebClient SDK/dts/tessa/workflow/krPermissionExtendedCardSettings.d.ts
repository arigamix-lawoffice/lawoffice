import { KrPermissionSectionSettings } from './krPermissionSectionSettings';
import { KrPermissionsFileSettings } from './krPermissionsFileSettings';
import { KrPermissionVisibilitySettings } from './krPermissionVisibilitySettings';
import { StorageObject, IStorage, ArrayStorage } from 'tessa/platform/storage';
import { KrPermissionsFilesSettings } from './krPermissionsFilesSettings';
export declare class KrPermissionExtendedCardSettings extends StorageObject {
    constructor(storage: IStorage);
    static readonly sectionSettingsKey: string;
    static readonly taskSettingsKey: string;
    static readonly taskSettingsTypesKey: string;
    static readonly visibilitySettingsKey: string;
    static readonly fileSettingsKey: string;
    static readonly ownFilesSettingsKey: string;
    static readonly otherfilesSettingsKey: string;
    get sectionSettings(): ArrayStorage<KrPermissionSectionSettings>;
    set sectionSettings(value: ArrayStorage<KrPermissionSectionSettings>);
    get taskSettings(): ArrayStorage<ArrayStorage<KrPermissionSectionSettings>>;
    set taskSettings(value: ArrayStorage<ArrayStorage<KrPermissionSectionSettings>>);
    get taskSettingsTypes(): any[];
    set taskSettingsTypes(value: any[]);
    get visibilitySettings(): any[];
    set visibilitySettings(value: any[]);
    get fileSettings(): ArrayStorage<KrPermissionsFileSettings>;
    set fileSettings(value: ArrayStorage<KrPermissionsFileSettings>);
    get ownFilesSettings(): KrPermissionsFilesSettings | null | undefined;
    set ownFilesSettings(value: KrPermissionsFilesSettings | null | undefined);
    get otherFilesSettings(): KrPermissionsFilesSettings | null | undefined;
    set otherFilesSettings(value: KrPermissionsFilesSettings | null | undefined);
    private static readonly _cardSettingsFactory;
    private static readonly _taskSettingsFactory;
    private static readonly _fileSettingsFactory;
    getCardSettings(): KrPermissionSectionSettings[];
    getTaskSettings(): Map<guid, KrPermissionSectionSettings[]> | null;
    getVisibilitySettings(): KrPermissionVisibilitySettings[];
    getFileSettings(): KrPermissionsFileSettings[];
    getOwnFilesSettings(): KrPermissionsFilesSettings | null | undefined;
    getOtherFilesSettings(): KrPermissionsFilesSettings | null | undefined;
    setCardAccess(isAllowed: boolean, sectionId: guid, fields: guid[]): void;
}
