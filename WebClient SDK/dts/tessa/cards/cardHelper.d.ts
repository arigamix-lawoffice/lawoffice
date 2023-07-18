import { CardStoreMode } from './cardStoreMode';
import { Card } from './card';
import { CardPermissionFlags } from './cardPermissionFlags';
import { CardTypeExtensionType } from './cardTypeExtensionType';
import { TypeExtensionContext } from './typeExtensionContext';
import { IStorage } from 'tessa/platform/storage';
import { ValidationResult } from 'tessa/platform/validation';
import { CardMetadataSealed } from 'tessa/cards/metadata';
export declare const systemKeyPrefix = ".";
export declare const userKeyPrefix = "__";
export declare const userSettingsOnlyKey: string;
export declare function isSystemKey(key: string): boolean;
export declare function isUserKey(key: string): boolean;
export declare function getStoreMode(version: number): CardStoreMode;
/**
 * Card type identifier for "CalendarCalcMethod": {2A9B3B6D-1C22-46DC-ADA2-7437FD4273CE}.
 */
export declare const CalendarCalcMethodTypeID = "2a9b3b6d-1c22-46dc-ada2-7437fd4273ce";
/**
 * Card type name for "CalendarCalcMethod".
 */
export declare const CalendarCalcMethodTypeName = "CalendarCalcMethod";
/**
 * Card type identifier for "DefaultCalendarTypeTypeID": {2D1D11A0-3ED0-46A6-BD5C-8FC8A952EABD}.
 */
export declare const DefaultCalendarTypeTypeID = "2d1d11a0-3ed0-46a6-bd5c-8fc8a952eabd";
/**
 * Card type name for "CalendarType".
 */
export declare const CalendarTypeTypeName = "DefaultCalendarType";
/**
 * Идентификатор типа карточки для настроек бизнес-календаря.
 */
export declare const CalendarTypeID = "9079d7f9-7b44-43f2-bf11-f9ed11965dd6";
/**
 * Имя типа карточки для настроек бизнес-календаря.
 */
export declare const CalendarTypeName = "Calendar";
/**
 * Отображаемое имя типа карточки для настроек бизнес-календаря.
 */
export declare const CalendarTypeCaption = "$CardTypes_TypesNames_Tabs_Calendar";
/**
 * Идентификатор типа карточки для настроек лицензии.
 */
export declare const LicenseTypeID = "f9c7b09c-de09-46b5-ba35-e73c83ea52a7";
/**
 * Имя типа карточки для настроек лицензии.
 */
export declare const LicenseTypeName = "License";
/**
 * Отображаемое имя типа карточки для настроек лицензии.
 */
export declare const LicenseTypeCaption = "$CardTypes_TypesNames_License";
/**
 * Идентификатор типа карточки для настроек сервера.
 */
export declare const ServerInstanceTypeID = "7b891314-474f-4a60-8e0d-744dcb075209";
/**
 * Имя типа карточки для настроек сервера.
 */
export declare const ServerInstanceTypeName = "ServerInstance";
/**
 * Отображаемое имя типа карточки для настроек сервера.
 */
export declare const ServerInstanceTypeCaption = "$CardTypes_TypesNames_ServerInstance";
/**
 * Идентификатор типа карточки для шаблона файла.
 */
export declare const FileTemplateTypeID = "b7e1b93e-eeda-49b7-9402-2471d4d14bdf";
/**
 * Имя типа карточки для шаблона файла.
 */
export declare const FileTemplateTypeName = "FileTemplate";
/**
 * Отображаемое имя типа карточки для шаблона файла.
 */
export declare const FileTemplateTypeCaption = "$CardTypes_TypesNames_FileTemplate";
/**
 * Идентификатор типа карточки для удалённой карточки.
 */
export declare const DeletedTypeID = "f5e74fbb-5357-4a6d-adce-4c2607853fdd";
/**
 * Имя типа карточки для удалённой карточки.
 */
export declare const DeletedTypeName = "Deleted";
/**
 * Отображаемое имя типа карточки для удалённой карточки.
 */
export declare const DeletedTypeCaption = "$CardTypes_TypesNames_Deleted";
/**
 * Card type identifier for "ActionHistoryRecord": {ABC13918-AA63-45CA-A3F4-D1FD5673C248}.
 */
export declare const ActionHistoryRecordTypeID = "abc13918-aa63-45ca-a3f4-d1fd5673c248";
/**
 * Card type name for "ActionHistoryRecord".
 */
export declare const ActionHistoryRecordTypeName = "ActionHistoryRecord";
/**
 * Card type identifier for "AclGenerationRule": {76B57AB5-19D4-4CC9-9E80-2C2F05CC2425}.
 */
export declare const AclGenerationRuleTypeID = "76b57ab5-19d4-4cc9-9e80-2c2f05cc2425";
/**
 * Card type name for "AclGenerationRule".
 */
export declare const AclGenerationRuleTypeName = "AclGenerationRule";
/**
 * Card type identifier for "Error": {FA81208D-2D83-4CB6-A83D-CBA7E3F483A7}.
 */
export declare const ErrorTypeID = "fa81208d-2d83-4cb6-a83d-cba7e3f483a7";
/**
 * Card type name for "Error".
 */
export declare const ErrorTypeName = "Error";
/**
 * Идентификатор типа карточки для настроек синхронизации с Active Directory.
 */
export declare const AdSyncTypeID = "cdaa9e03-9e06-4b4d-9a21-cfc446d2d9d1";
/**
 * Имя типа карточки для настроек синхронизации с Active Directory.
 */
export declare const AdSyncTypeName = "AdSync";
/**
 * Отображаемое имя типа карточки для настроек синхронизации с Active Directory.
 */
export declare const AdSyncTypeCaption = "$CardTypes_TypesNames_ADSync";
/**
 * Идентификатор типа карточки для кэша конвертации файлов.
 */
export declare const FileConverterCacheTypeID = "7609d1d7-9a46-4617-8789-2dff55aa4072";
/**
 * Имя типа карточки для кэша конвертации файлов.
 */
export declare const FileConverterCacheTypeName = "FileConverterCache";
/**
 * Отображаемое имя типа карточки для кэша конвертации файлов.
 */
export declare const FileConverterCacheTypeCaption = "$CardTypes_TypesNames_FileConverterCache";
/**
 * Идентификатор типа карточки шаблона.
 */
export declare const TemplateTypeID = "7ed2fb6d-4ece-458f-9151-0c72995c2d19";
/**
 * Имя типа карточки шаблона.
 */
export declare const TemplateTypeName = "Template";
/**
 * Отображаемое имя типа карточки шаблона.
 */
export declare const TemplateTypeCaption = "$CardTypes_TypesNames_Blocks_Tabs_Template";
/**
 * Идентификатор типа виртуальной карточки функциональной роли: {A830094D-6E03-4242-9C17-0D0A8F2FCB33}.
 */
export declare const FunctionRoleTypeID = "a830094d-6e03-4242-9c17-0d0a8f2fcb33";
/**
 * Имя типа виртуальной карточки функциональной роли "FunctionRole".
 */
export declare const FunctionRoleTypeName = "FunctionRole";
/**
 * Отображаемое имя типа виртуальной карточки функциональной роли.
 */
export declare const FunctionRoleTypeCaption = "$CardTypes_TypesNames_FunctionRole";
/**
 * Идентификатор типа карточки для файла шаблона.
 */
export declare const TemplateFileTypeID = "a259101b-58f7-47b4-959e-dd5e7be1671c";
/**
 * Имя типа карточки для файла шаблона.
 */
export declare const TemplateFileTypeName = "TemplateFile";
/**
 * Отображаемое имя типа карточки для файла шаблона.
 */
export declare const TemplateFileTypeCaption = "$CardTypes_TypesNames_TemplateFile";
export declare const CardTasksEditorDialogTypeID = "db737600-1bf6-451b-80ca-01fe06161ee6";
/**
 * Все разрешения, доступные для карточки.
 */
export declare const AllowAllCardPermissionFlags: CardPermissionFlags;
/**
 * Запрет всех разрешений, доступных для карточки.
 */
export declare const ProhibitAllCardPermissionFlags: CardPermissionFlags;
/**
 * Все разрешения, доступные для файла.
 */
export declare const AllowAllFilePermissionFlags: CardPermissionFlags;
/**
 * Запрет всех разрешений, доступных для файла.
 */
export declare const ProhibitAllFilePermissionFlags: CardPermissionFlags;
export declare function grantAllPermissions(card: Card, removeOtherPermissions?: boolean, excludeCards?: boolean, excludeFiles?: boolean, excludeTasks?: boolean): void;
export declare function prohibitAllPermissions(card: Card, removeOtherPermissions?: boolean, excludeCards?: boolean, excludeFiles?: boolean, excludeTasks?: boolean): void;
export declare function setAllCardPermissions(card: Card, cardPermissions: CardPermissionFlags, filePermissions: CardPermissionFlags, removeOtherPermissions?: boolean, excludeCards?: boolean, excludeFiles?: boolean, excludeTasks?: boolean): void;
export declare function setAllSingleCardWithFilesPermissions(card: Card, removeOtherPermissions: boolean | undefined, cardPermissions: CardPermissionFlags, filePermissions: CardPermissionFlags, excludeCards?: boolean, excludeFiles?: boolean): void;
export declare function executeExtensions(type: CardTypeExtensionType, card: Card, cardMetadata: CardMetadataSealed, executeAction: (ctx: TypeExtensionContext) => Promise<void> | void, externalContext?: any | null, info?: IStorage | null): Promise<ValidationResult>;
export declare const hasFilesToSave: (card: Card) => boolean;
