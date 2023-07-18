// noinspection JSUnusedGlobalSymbols,SpellCheckingInspection

//#region AccessLevels

/**
 * ID: {648381d6-8647-4ec6-87a4-3cbd6bae380c}
 * Alias: AccessLevels
 * Group: System
 * Description: Уровни доступа пользователей.
 */
class AccessLevelsSchemeInfo {
  private readonly name: string = 'AccessLevels';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Regular: AccessLevels = new AccessLevels(0, '$Enum_AccessLevels_Regular');
  readonly Administrator: AccessLevels = new AccessLevels(1, '$Enum_AccessLevels_Administrator');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region AccessLevels Enumeration

class AccessLevels {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region Acl

/**
 * ID: {b7538557-04c6-43d4-9d7a-4412dc1ed103}
 * Alias: Acl
 * Group: Acl
 * Description: Основная таблица со списком ролей доступа к карточке.
 */
class AclSchemeInfo {
  private readonly name: string = 'Acl';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RuleID: string = 'RuleID';
  readonly RoleID: string = 'RoleID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AclGenerationInfo

/**
 * ID: {5cca17a2-50e3-4b20-98c2-2f6ed9ce31fa}
 * Alias: AclGenerationInfo
 * Group: Acl
 * Description: Таблица с информацией о генерации ACL.
 */
class AclGenerationInfoSchemeInfo {
  private readonly name: string = 'AclGenerationInfo';

  //#region Columns

  readonly RuleID: string = 'RuleID';
  readonly RuleVersion: string = 'RuleVersion';
  readonly NextRequest: string = 'NextRequest';
  readonly NextRequestVersion: string = 'NextRequestVersion';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AclGenerationRuleExtensions

/**
 * ID: {c2c3b955-ca13-4e63-83aa-cb033ebdce57}
 * Alias: AclGenerationRuleExtensions
 * Group: Acl
 */
class AclGenerationRuleExtensionsSchemeInfo {
  private readonly name: string = 'AclGenerationRuleExtensions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ExtensionID: string = 'ExtensionID';
  readonly ExtensionName: string = 'ExtensionName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AclGenerationRules

/**
 * ID: {5518f35a-ea30-4968-983d-aec524aeb710}
 * Alias: AclGenerationRules
 * Group: Acl
 * Description: Основная таблица для правил расчета ACL.
 */
class AclGenerationRulesSchemeInfo {
  private readonly name: string = 'AclGenerationRules';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Version: string = 'Version';
  readonly RolesSelectorSql: string = 'RolesSelectorSql';
  readonly DefaultUpdateAclCardSelectorSql: string = 'DefaultUpdateAclCardSelectorSql';
  readonly UseSmartRoles: string = 'UseSmartRoles';
  readonly CardOwnerSelectorSql: string = 'CardOwnerSelectorSql';
  readonly SmartRoleGeneratorID: string = 'SmartRoleGeneratorID';
  readonly SmartRoleGeneratorName: string = 'SmartRoleGeneratorName';
  readonly Diescription: string = 'Diescription';
  readonly IsDisabled: string = 'IsDisabled';
  readonly EnableErrorLogging: string = 'EnableErrorLogging';
  readonly CardsByOwnerSelectorSql: string = 'CardsByOwnerSelectorSql';
  readonly ExtensionsData: string = 'ExtensionsData';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AclGenerationRuleTriggerModes

/**
 * ID: {b55966a9-f474-47c9-b025-8e3408208646}
 * Alias: AclGenerationRuleTriggerModes
 * Group: Acl
 */
class AclGenerationRuleTriggerModesSchemeInfo {
  private readonly name: string = 'AclGenerationRuleTriggerModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly AnyChanges: AclGenerationRuleTriggerModes = new AclGenerationRuleTriggerModes(0, 'AnyChanges');
  readonly FieldChanged: AclGenerationRuleTriggerModes = new AclGenerationRuleTriggerModes(1, 'FieldChanged');
  readonly RowAdded: AclGenerationRuleTriggerModes = new AclGenerationRuleTriggerModes(2, 'RowAdded');
  readonly RowDeleted: AclGenerationRuleTriggerModes = new AclGenerationRuleTriggerModes(3, 'RowDeleted');
  readonly CardCreated: AclGenerationRuleTriggerModes = new AclGenerationRuleTriggerModes(4, 'CardCreated');
  readonly CardDeleted: AclGenerationRuleTriggerModes = new AclGenerationRuleTriggerModes(5, 'CardDeleted');
  readonly TaskCreated: AclGenerationRuleTriggerModes = new AclGenerationRuleTriggerModes(6, 'TaskCreated');
  readonly TaskCompleted: AclGenerationRuleTriggerModes = new AclGenerationRuleTriggerModes(7, 'TaskCompleted');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region AclGenerationRuleTriggerModes Enumeration

class AclGenerationRuleTriggerModes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region AclGenerationRuleTriggers

/**
 * ID: {24e6a4b4-7e51-4429-8bb7-648a840e026b}
 * Alias: AclGenerationRuleTriggers
 * Group: Acl
 * Description: Триггеры правила генерации Acl.
 */
class AclGenerationRuleTriggersSchemeInfo {
  private readonly name: string = 'AclGenerationRuleTriggers';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly UpdateAclCardSelectorSql: string = 'UpdateAclCardSelectorSql';
  readonly OnlySelfUpdate: string = 'OnlySelfUpdate';
  readonly Order: string = 'Order';
  readonly Conditions: string = 'Conditions';
  readonly Name: string = 'Name';
  readonly UpdateAsync: string = 'UpdateAsync';
  readonly ConditionsAndMode: string = 'ConditionsAndMode';
  readonly UseRuleCardTypes: string = 'UseRuleCardTypes';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AclGenerationRuleTriggerTypes

/**
 * ID: {59827979-5949-4bb2-896d-dc8b5a238a32}
 * Alias: AclGenerationRuleTriggerTypes
 * Group: Acl
 * Description: Типы карточек, при изменении которых проверяется триггер.
 */
class AclGenerationRuleTriggerTypesSchemeInfo {
  private readonly name: string = 'AclGenerationRuleTriggerTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TriggerRowID: string = 'TriggerRowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AclGenerationRuleTypes

/**
 * ID: {930de8d2-2496-4523-9ea2-800d229fd808}
 * Alias: AclGenerationRuleTypes
 * Group: Acl
 * Description: Типы карточек для правила Acl.
 */
class AclGenerationRuleTypesSchemeInfo {
  private readonly name: string = 'AclGenerationRuleTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AcquaintanceComments

/**
 * ID: {ae4e68f0-ff8e-4055-9386-f601f1f3c664}
 * Alias: AcquaintanceComments
 * Group: Common
 * Description: Строки по данным для комментариев, отправленных на массовое ознакомление. По одной строке для каждой отправки на ознакомление с непустым комментарием, при этом в отправке может быть указано несколько ролей, в каждой из которых несколько сотрудников.
 */
class AcquaintanceCommentsSchemeInfo {
  private readonly name: string = 'AcquaintanceComments';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Comment: string = 'Comment';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AcquaintanceRows

/**
 * ID: {8874a392-0fd9-47dd-a6b5-bc3c02ede681}
 * Alias: AcquaintanceRows
 * Group: Common
 * Description: Строки по данным для отправки на массовое ознакомление. По одной строке для каждого сотрудника, которому была отправлена карточка на ознакомление.
 */
class AcquaintanceRowsSchemeInfo {
  private readonly name: string = 'AcquaintanceRows';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CardID: string = 'CardID';
  readonly SenderID: string = 'SenderID';
  readonly SenderName: string = 'SenderName';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly IsReceived: string = 'IsReceived';
  readonly Sent: string = 'Sent';
  readonly Received: string = 'Received';
  readonly CommentID: string = 'CommentID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ActionHistory

/**
 * ID: {5089ca1c-27af-46e4-a2c2-af01bfd42e81}
 * Alias: ActionHistory
 * Group: System
 */
class ActionHistorySchemeInfo {
  private readonly name: string = 'ActionHistory';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ActionID: string = 'ActionID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly Digest: string = 'Digest';
  readonly Request: string = 'Request';
  readonly Modified: string = 'Modified';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';
  readonly SessionID: string = 'SessionID';
  readonly ApplicationID: string = 'ApplicationID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ActionHistoryDatabases

/**
 * ID: {db0969a9-e71d-405d-bf86-15f263cf69c8}
 * Alias: ActionHistoryDatabases
 * Group: System
 * Description: Базы данных для хранения истории действий.
 */
class ActionHistoryDatabasesSchemeInfo {
  private readonly name: string = 'ActionHistoryDatabases';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Description: string = 'Description';
  readonly ConfigurationString: string = 'ConfigurationString';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ActionHistoryDatabasesVirtual

/**
 * ID: {df1d09a4-5ef2-4f2b-885e-c4ad6df06555}
 * Alias: ActionHistoryDatabasesVirtual
 * Group: System
 * Description: Базы данных для хранения истории действий. Виртуальная таблица, обеспечивающая редактирование таблицы ActionHistoryDatabases через карточку настроек.
 * Колонка ID в этой таблице соответствует идентификатору карточки настроек, а колонка DatabaseID - идентификатору базы данных, т.е. аналог ActionHistoryDatabases.ID.
 */
class ActionHistoryDatabasesVirtualSchemeInfo {
  private readonly name: string = 'ActionHistoryDatabasesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly DatabaseID: string = 'DatabaseID';
  readonly DatabaseIDText: string = 'DatabaseIDText';
  readonly Name: string = 'Name';
  readonly Description: string = 'Description';
  readonly ConfigurationString: string = 'ConfigurationString';
  readonly IsDefault: string = 'IsDefault';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ActionHistoryVirtual

/**
 * ID: {d1ab792c-2758-4778-a3cf-d91191b3ec52}
 * Alias: ActionHistoryVirtual
 * Group: System
 * Description: История действий с карточкой для отображения в UI.
 */
class ActionHistoryVirtualSchemeInfo {
  private readonly name: string = 'ActionHistoryVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ActionID: string = 'ActionID';
  readonly ActionName: string = 'ActionName';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly CardID: string = 'CardID';
  readonly CardDigest: string = 'CardDigest';
  readonly Request: string = 'Request';
  readonly RequestJson: string = 'RequestJson';
  readonly Description: string = 'Description';
  readonly Category: string = 'Category';
  readonly Text: string = 'Text';
  readonly Modified: string = 'Modified';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';
  readonly HasDetailsCard: string = 'HasDetailsCard';
  readonly SessionID: string = 'SessionID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ActionTypes

/**
 * ID: {420a67fd-2ea0-4ccd-9c3f-6378c2fda2cc}
 * Alias: ActionTypes
 * Group: System
 * Description: Типы действий с карточкой.
 */
class ActionTypesSchemeInfo {
  private readonly name: string = 'ActionTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Creating: ActionTypes = new ActionTypes(1, '$ActionHistory_Action_Creating');
  readonly Opening: ActionTypes = new ActionTypes(2, '$ActionHistory_Action_Opening');
  readonly Editing: ActionTypes = new ActionTypes(3, '$ActionHistory_Action_Editing');
  readonly Deleting: ActionTypes = new ActionTypes(4, '$ActionHistory_Action_Deleting');
  readonly FileOpening: ActionTypes = new ActionTypes(5, '$ActionHistory_Action_FileOpening');
  readonly Restoring: ActionTypes = new ActionTypes(6, '$ActionHistory_Action_Restoring');
  readonly Export: ActionTypes = new ActionTypes(7, '$ActionHistory_Action_Export');
  readonly Import: ActionTypes = new ActionTypes(8, '$ActionHistory_Action_Import');
  readonly FinalDeleting: ActionTypes = new ActionTypes(9, '$ActionHistory_Action_FinalDeleting');
  readonly Login: ActionTypes = new ActionTypes(10, '$ActionHistory_Action_Login');
  readonly Logout: ActionTypes = new ActionTypes(11, '$ActionHistory_Action_Logout');
  readonly ReserveNumber: ActionTypes = new ActionTypes(12, '$ActionHistory_Action_ReserveNumber');
  readonly AcquireNumber: ActionTypes = new ActionTypes(13, '$ActionHistory_Action_AcquireNumber');
  readonly AcquireReservedNumber: ActionTypes = new ActionTypes(14, '$ActionHistory_Action_AcquireReservedNumber');
  readonly AcquireUnreservedNumber: ActionTypes = new ActionTypes(15, '$ActionHistory_Action_AcquireUnreservedNumber');
  readonly ReleaseNumber: ActionTypes = new ActionTypes(16, '$ActionHistory_Action_ReleaseNumber');
  readonly DereserveNumber: ActionTypes = new ActionTypes(17, '$ActionHistory_Action_DereserveNumber');
  readonly SessionClosedByAdmin: ActionTypes = new ActionTypes(18, '$ActionHistory_Action_SessionClosedByAdmin');
  readonly LoginFailed: ActionTypes = new ActionTypes(19, '$ActionHistory_Action_LoginFailed');
  readonly Error: ActionTypes = new ActionTypes(20, '$ActionHistory_Action_Error');
  readonly AddCardType: ActionTypes = new ActionTypes(21, '$ActionHistory_Action_AddCardType');
  readonly ModifyCardType: ActionTypes = new ActionTypes(22, '$ActionHistory_Action_ModifyCardType');
  readonly DeleteCardType: ActionTypes = new ActionTypes(23, '$ActionHistory_Action_DeleteCardType');
  readonly AddView: ActionTypes = new ActionTypes(24, '$ActionHistory_Action_AddView');
  readonly ModifyView: ActionTypes = new ActionTypes(25, '$ActionHistory_Action_ModifyView');
  readonly DeleteView: ActionTypes = new ActionTypes(26, '$ActionHistory_Action_DeleteView');
  readonly ImportView: ActionTypes = new ActionTypes(27, '$ActionHistory_Action_ImportView');
  readonly AddWorkplace: ActionTypes = new ActionTypes(28, '$ActionHistory_Action_AddWorkplace');
  readonly ModifyWorkplace: ActionTypes = new ActionTypes(29, '$ActionHistory_Action_ModifyWorkplace');
  readonly DeleteWorkplace: ActionTypes = new ActionTypes(30, '$ActionHistory_Action_DeleteWorkplace');
  readonly ImportWorkplace: ActionTypes = new ActionTypes(31, '$ActionHistory_Action_ImportWorkplace');
  readonly ModifyTable: ActionTypes = new ActionTypes(32, '$ActionHistory_Action_ModifyTable');
  readonly DeleteTable: ActionTypes = new ActionTypes(33, '$ActionHistory_Action_DeleteTable');
  readonly ModifyProcedure: ActionTypes = new ActionTypes(34, '$ActionHistory_Action_ModifyProcedure');
  readonly DeleteProcedure: ActionTypes = new ActionTypes(35, '$ActionHistory_Action_DeleteProcedure');
  readonly ModifyFunction: ActionTypes = new ActionTypes(36, '$ActionHistory_Action_ModifyFunction');
  readonly DeleteFunction: ActionTypes = new ActionTypes(37, '$ActionHistory_Action_DeleteFunction');
  readonly ModifyMigration: ActionTypes = new ActionTypes(38, '$ActionHistory_Action_ModifyMigration');
  readonly DeleteMigration: ActionTypes = new ActionTypes(39, '$ActionHistory_Action_DeleteMigration');
  readonly ModifyPartition: ActionTypes = new ActionTypes(40, '$ActionHistory_Action_ModifyPartition');
  readonly DeletePartition: ActionTypes = new ActionTypes(41, '$ActionHistory_Action_DeletePartition');
  readonly ModifyLocalizationLibrary: ActionTypes = new ActionTypes(42, '$ActionHistory_Action_ModifyLocalizationLibrary');
  readonly DeleteLocalizationLibrary: ActionTypes = new ActionTypes(43, '$ActionHistory_Action_DeleteLocalizationLibrary');
  readonly ReserveAcquiredNumber: ActionTypes = new ActionTypes(44, '$ActionHistory_Action_ReserveAcquiredNumber');
  readonly StoreTag: ActionTypes = new ActionTypes(45, '$ActionHistory_Action_StoreTag');
  readonly DeleteTag: ActionTypes = new ActionTypes(46, '$ActionHistory_Action_DeleteTag');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region ActionTypes Enumeration

class ActionTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region AdSyncRoots

/**
 * ID: {68543b32-9960-4b90-9c67-72a297f4feff}
 * Alias: AdSyncRoots
 * Group: System
 */
class AdSyncRootsSchemeInfo {
  private readonly name: string = 'AdSyncRoots';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RootName: string = 'RootName';
  readonly SyncStaticRoles: string = 'SyncStaticRoles';
  readonly SyncDepartments: string = 'SyncDepartments';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AdSyncSettings

/**
 * ID: {6b7f7b41-7ba8-4549-b965-f3a2aa9a168b}
 * Alias: AdSyncSettings
 * Group: System
 */
class AdSyncSettingsSchemeInfo {
  private readonly name: string = 'AdSyncSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly SyncUsers: string = 'SyncUsers';
  readonly SyncDepartments: string = 'SyncDepartments';
  readonly SyncUsersGroup: string = 'SyncUsersGroup';
  readonly SyncStaticRoles: string = 'SyncStaticRoles';
  readonly DisableStaticRoleRename: string = 'DisableStaticRoleRename';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AdSyncSettingsVirtual

/**
 * ID: {c993000f-40d8-4639-a25d-e9a25d47e19c}
 * Alias: AdSyncSettingsVirtual
 * Group: System
 */
class AdSyncSettingsVirtualSchemeInfo {
  private readonly name: string = 'AdSyncSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly SyncUsers: string = 'SyncUsers';
  readonly SyncDepartments: string = 'SyncDepartments';
  readonly SyncStaticRoles: string = 'SyncStaticRoles';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ApplicationArchitectures

/**
 * ID: {27977834-b755-4a4a-9180-90748e71f361}
 * Alias: ApplicationArchitectures
 * Group: System
 * Description: Архитектура процессора (разрядность) для приложений, запускаемых пользователем. Настройка задаётся администратором в карточке сотрудника.
 */
class ApplicationArchitecturesSchemeInfo {
  private readonly name: string = 'ApplicationArchitectures';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Auto: ApplicationArchitectures = new ApplicationArchitectures(0, '$Enum_ApplicationArchitectures_Auto');
  readonly Enum32bit: ApplicationArchitectures = new ApplicationArchitectures(1, '$Enum_ApplicationArchitectures_32bit');
  readonly Enum64bit: ApplicationArchitectures = new ApplicationArchitectures(2, '$Enum_ApplicationArchitectures_64bit');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region ApplicationArchitectures Enumeration

class ApplicationArchitectures {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region ApplicationNames

/**
 * ID: {b939817b-bc1f-4a9d-87ef-694336870eed}
 * Alias: ApplicationNames
 * Group: System
 * Description: Имена стандартных приложений.
 */
class ApplicationNamesSchemeInfo {
  private readonly name: string = 'ApplicationNames';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly IsHidden: string = 'IsHidden';

  //#endregion

  //#region Enumeration

  readonly Other: ApplicationNames = new ApplicationNames('00000000-0000-0000-0000-000000000000', '$Enum_ApplicationNames_Other', false);
  readonly TessaClient: ApplicationNames = new ApplicationNames('3bc38194-a881-4955-85e7-0c6be3031f45', '$Enum_ApplicationNames_TessaClient', false);
  readonly TessaAdmin: ApplicationNames = new ApplicationNames('35a85591-a7cf-4b33-8319-891207587af9', '$Enum_ApplicationNames_TessaAdmin', false);
  readonly WebClient: ApplicationNames = new ApplicationNames('9b7d9877-2017-4a35-b612-5f83bec39df9', '$Enum_ApplicationNames_WebClient', false);
  readonly TessaAppManager: ApplicationNames = new ApplicationNames('0468baaf-3a52-43bb-8efb-40bf1757776d', '$Enum_ApplicationNames_TessaAppManager', false);
  readonly Chronos: ApplicationNames = new ApplicationNames('fdd842ad-8318-42b8-b2bb-f8233b37199e', '$Enum_ApplicationNames_Chronos', false);
  readonly TessaAdminConsole: ApplicationNames = new ApplicationNames('6eb1fdba-7eac-4b70-9612-161dd9fbd511', '$Enum_ApplicationNames_TessaAdminConsole', false);
  readonly TessaClientNotifications: ApplicationNames = new ApplicationNames('1e3386c4-4baa-4bb6-b6c9-64b699410372', '$Enum_ApplicationNames_TessaClientNotifications', true);

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region ApplicationNames Enumeration

class ApplicationNames {
  readonly ID: guid;
  readonly Name: string | null;
  readonly IsHidden: boolean;

  constructor (ID: guid, Name: string | null, IsHidden: boolean) {
    this.ID = ID;
    this.Name = Name;
    this.IsHidden = IsHidden;
  }
}

//#endregion

//#endregion

//#region ApplicationRoles

/**
 * ID: {7d23077a-8730-4ad7-9bcd-9a3d52c7e119}
 * Alias: ApplicationRoles
 * Group: System
 * Description: Роли, которым доступно приложение.
 */
class ApplicationRolesSchemeInfo {
  private readonly name: string = 'ApplicationRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Applications

/**
 * ID: {6134967a-914b-45eb-99bd-a0ebefdca9f4}
 * Alias: Applications
 * Group: System
 * Description: Приложения
 */
class ApplicationsSchemeInfo {
  private readonly name: string = 'Applications';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Alias: string = 'Alias';
  readonly ExecutableFileName: string = 'ExecutableFileName';
  readonly AppVersion: string = 'AppVersion';
  readonly PlatformVersion: string = 'PlatformVersion';
  readonly ForAdmin: string = 'ForAdmin';
  readonly Icon: string = 'Icon';
  readonly GroupName: string = 'GroupName';
  readonly Client64Bit: string = 'Client64Bit';
  readonly AppManagerApiV2: string = 'AppManagerApiV2';
  readonly Hidden: string = 'Hidden';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BackgroundColors

/**
 * ID: {9f4fc1ce-af03-4009-8106-d9b861469ef1}
 * Alias: BackgroundColors
 * Group: System
 */
class BackgroundColorsSchemeInfo {
  private readonly name: string = 'BackgroundColors';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Color1: string = 'Color1';
  readonly Color2: string = 'Color2';
  readonly Color3: string = 'Color3';
  readonly Color4: string = 'Color4';
  readonly Color5: string = 'Color5';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BarcodeTypes

/**
 * ID: {60ad88cc-f913-48ce-96e1-0abf417da790}
 * Alias: BarcodeTypes
 * Group: System
 * Description: Типы штрих-кодов
 */
class BarcodeTypesSchemeInfo {
  private readonly name: string = 'BarcodeTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly CanPrint: string = 'CanPrint';
  readonly CanScan: string = 'CanScan';

  //#endregion

  //#region Enumeration

  readonly AZTEC: BarcodeTypes = new BarcodeTypes(1, 'AZTEC', false, true);
  readonly CODABAR: BarcodeTypes = new BarcodeTypes(2, 'CODABAR', true, true);
  readonly CODE_93: BarcodeTypes = new BarcodeTypes(4, 'CODE_93', true, true);
  readonly EAN_13: BarcodeTypes = new BarcodeTypes(8, 'EAN_13', true, true);
  readonly UPC_E: BarcodeTypes = new BarcodeTypes(16, 'UPC_E', true, true);
  readonly CODE_39: BarcodeTypes = new BarcodeTypes(3, 'CODE_39', true, true);
  readonly CODE_128: BarcodeTypes = new BarcodeTypes(5, 'CODE_128', true, true);
  readonly DATA_MATRIX: BarcodeTypes = new BarcodeTypes(6, 'DATA_MATRIX', false, true);
  readonly EAN_8: BarcodeTypes = new BarcodeTypes(7, 'EAN_8', true, true);
  readonly ITF: BarcodeTypes = new BarcodeTypes(9, 'ITF', true, true);
  readonly MAXICODE: BarcodeTypes = new BarcodeTypes(10, 'MAXICODE', false, true);
  readonly PDF_417: BarcodeTypes = new BarcodeTypes(11, 'PDF_417', false, true);
  readonly QR_CODE: BarcodeTypes = new BarcodeTypes(12, 'QR_CODE', false, true);
  readonly RSS_14: BarcodeTypes = new BarcodeTypes(13, 'RSS_14', false, true);
  readonly RSS_EXPANDED: BarcodeTypes = new BarcodeTypes(14, 'RSS_EXPANDED', false, true);
  readonly UPC_A: BarcodeTypes = new BarcodeTypes(15, 'UPC_A', true, true);
  readonly All_1D: BarcodeTypes = new BarcodeTypes(17, 'All_1D', false, true);
  readonly UPC_EAN_EXTENSION: BarcodeTypes = new BarcodeTypes(18, 'UPC_EAN_EXTENSION', false, true);
  readonly MSI: BarcodeTypes = new BarcodeTypes(19, 'MSI', true, true);
  readonly PLESSEY: BarcodeTypes = new BarcodeTypes(20, 'PLESSEY', true, true);
  readonly IMB: BarcodeTypes = new BarcodeTypes(21, 'IMB', false, true);

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region BarcodeTypes Enumeration

class BarcodeTypes {
  readonly ID: number;
  readonly Name: string | null;
  readonly CanPrint: boolean;
  readonly CanScan: boolean;

  constructor (ID: number, Name: string | null, CanPrint: boolean, CanScan: boolean) {
    this.ID = ID;
    this.Name = Name;
    this.CanPrint = CanPrint;
    this.CanScan = CanScan;
  }
}

//#endregion

//#endregion

//#region BlockColors

/**
 * ID: {c1b59501-4d7f-4884-ac20-715d5d26078b}
 * Alias: BlockColors
 * Group: System
 */
class BlockColorsSchemeInfo {
  private readonly name: string = 'BlockColors';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Color1: string = 'Color1';
  readonly Color2: string = 'Color2';
  readonly Color3: string = 'Color3';
  readonly Color4: string = 'Color4';
  readonly Color5: string = 'Color5';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BusinessProcessButtonExtension

/**
 * ID: {b4d0da55-0e6e-4835-bb71-1df3c5b5e695}
 * Alias: BusinessProcessButtonExtension
 * Group: WorkflowEngine
 * Description: Основная секция для карточки-расширения тайла
 */
class BusinessProcessButtonExtensionSchemeInfo {
  private readonly name: string = 'BusinessProcessButtonExtension';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ButtonRowID: string = 'ButtonRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BusinessProcessButtonRoles

/**
 * ID: {89599d8b-fa2f-44de-94d5-9687d4a16854}
 * Alias: BusinessProcessButtonRoles
 * Group: WorkflowEngine
 * Description: Список ролей, которым доступная данная кнопка
 */
class BusinessProcessButtonRolesSchemeInfo {
  private readonly name: string = 'BusinessProcessButtonRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ButtonRowID: string = 'ButtonRowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BusinessProcessButtonRolesVirtual

/**
 * ID: {803fee29-9750-46f5-950d-77a44ff8b2af}
 * Alias: BusinessProcessButtonRolesVirtual
 * Group: WorkflowEngine
 * Description: Список ролей, которым доступная данная кнопка
 */
class BusinessProcessButtonRolesVirtualSchemeInfo {
  private readonly name: string = 'BusinessProcessButtonRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ButtonRowID: string = 'ButtonRowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BusinessProcessButtons

/**
 * ID: {59bf0d0b-f7fc-41d3-92da-56c673f1e0b3}
 * Alias: BusinessProcessButtons
 * Group: WorkflowEngine
 * Description: Секция с описанием кнопок бизнес-процесса (как запускающих сам процесс, так и отправляющих команду для процесса).
 */
class BusinessProcessButtonsSchemeInfo {
  private readonly name: string = 'BusinessProcessButtons';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Caption: string = 'Caption';
  readonly StartProcess: string = 'StartProcess';
  readonly SignalID: string = 'SignalID';
  readonly SignalName: string = 'SignalName';
  readonly Group: string = 'Group';
  readonly Icon: string = 'Icon';
  readonly Description: string = 'Description';
  readonly Condition: string = 'Condition';
  readonly TileSizeID: string = 'TileSizeID';
  readonly TileSizeName: string = 'TileSizeName';
  readonly Tooltip: string = 'Tooltip';
  readonly AskConfirmation: string = 'AskConfirmation';
  readonly ConfirmationMessage: string = 'ConfirmationMessage';
  readonly ActionGrouping: string = 'ActionGrouping';
  readonly DisplaySettings: string = 'DisplaySettings';
  readonly ButtonHotkey: string = 'ButtonHotkey';
  readonly AccessDeniedMessage: string = 'AccessDeniedMessage';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BusinessProcessButtonsVirtual

/**
 * ID: {033a363a-e183-4084-83cb-4672841a2a90}
 * Alias: BusinessProcessButtonsVirtual
 * Group: WorkflowEngine
 * Description: Секция с описанием кнопок бизнес-процесса (как запускающих сам процесс, так и отправляющих команду для процесса), в которую добавляются колонки из расширений на кнопки процесса.
 */
class BusinessProcessButtonsVirtualSchemeInfo {
  private readonly name: string = 'BusinessProcessButtonsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Caption: string = 'Caption';
  readonly StartProcess: string = 'StartProcess';
  readonly SignalID: string = 'SignalID';
  readonly SignalName: string = 'SignalName';
  readonly Group: string = 'Group';
  readonly Icon: string = 'Icon';
  readonly Description: string = 'Description';
  readonly Condition: string = 'Condition';
  readonly TileSizeID: string = 'TileSizeID';
  readonly TileSizeName: string = 'TileSizeName';
  readonly Tooltip: string = 'Tooltip';
  readonly AskConfirmation: string = 'AskConfirmation';
  readonly ConfirmationMessage: string = 'ConfirmationMessage';
  readonly ActionGrouping: string = 'ActionGrouping';
  readonly DisplaySettings: string = 'DisplaySettings';
  readonly ButtonHotkey: string = 'ButtonHotkey';
  readonly AccessDeniedMessage: string = 'AccessDeniedMessage';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BusinessProcessCardTypes

/**
 * ID: {2317e111-2d0e-42d9-94dd-973411ecadca}
 * Alias: BusinessProcessCardTypes
 * Group: WorkflowEngine
 * Description: Список типов карточек, для которых доступен данный процесс
 */
class BusinessProcessCardTypesSchemeInfo {
  private readonly name: string = 'BusinessProcessCardTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly CardTypeID: string = 'CardTypeID';
  readonly CardTypeCaption: string = 'CardTypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BusinessProcessEditRoles

/**
 * ID: {669078fd-2901-4084-ac61-13a063581197}
 * Alias: BusinessProcessEditRoles
 * Group: WorkflowEngine
 * Description: Список ролей, имеющих доступ на редактирование  шаблона и экземпляра процесса
 */
class BusinessProcessEditRolesSchemeInfo {
  private readonly name: string = 'BusinessProcessEditRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BusinessProcessExtensions

/**
 * ID: {07e8720b-4500-4a7f-b988-7eda3bb8dc38}
 * Alias: BusinessProcessExtensions
 * Group: WorkflowEngine
 * Description: Секция со списком расширений для карточки шаблона процесса
 */
class BusinessProcessExtensionsSchemeInfo {
  private readonly name: string = 'BusinessProcessExtensions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ExtensionID: string = 'ExtensionID';
  readonly ExtensionName: string = 'ExtensionName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BusinessProcessInfo

/**
 * ID: {5640ffb9-ef7c-4584-8793-57da90e82fa0}
 * Alias: BusinessProcessInfo
 * Group: WorkflowEngine
 * Description: Секция с основной информацией о бизнес-процессе.
 */
class BusinessProcessInfoSchemeInfo {
  private readonly name: string = 'BusinessProcessInfo';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly StartFromCard: string = 'StartFromCard';
  readonly Multiple: string = 'Multiple';
  readonly ConditionModified: string = 'ConditionModified';
  readonly Group: string = 'Group';
  readonly LockMessage: string = 'LockMessage';
  readonly ErrorMessage: string = 'ErrorMessage';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BusinessProcessReadRoles

/**
 * ID: {8a55a034-893d-4412-9458-189ef63d7008}
 * Alias: BusinessProcessReadRoles
 * Group: WorkflowEngine
 * Description: Список ролей, имеющих доступ на чтение экземпляра шаблона
 */
class BusinessProcessReadRolesSchemeInfo {
  private readonly name: string = 'BusinessProcessReadRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BusinessProcessVersions

/**
 * ID: {dcd38c54-ed18-4503-b435-3dee1c6c2c62}
 * Alias: BusinessProcessVersions
 * Group: WorkflowEngine
 * Description: Дерево версий бизнесс процесса
 */
class BusinessProcessVersionsSchemeInfo {
  private readonly name: string = 'BusinessProcessVersions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly Version: string = 'Version';
  readonly Created: string = 'Created';
  readonly CreatedByID: string = 'CreatedByID';
  readonly CreatedByName: string = 'CreatedByName';
  readonly Modified: string = 'Modified';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';
  readonly LockedForEditing: string = 'LockedForEditing';
  readonly ScriptFileID: string = 'ScriptFileID';
  readonly ProcessData: string = 'ProcessData';
  readonly IsDefault: string = 'IsDefault';
  readonly LockedByID: string = 'LockedByID';
  readonly LockedByName: string = 'LockedByName';
  readonly Locked: string = 'Locked';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region BusinessProcessVersionsVirtual

/**
 * ID: {6999d0d3-a44f-43b5-8e79-a551697340e6}
 * Alias: BusinessProcessVersionsVirtual
 * Group: WorkflowEngine
 * Description: Список версий бизнес-процесса, который отображается в карточке шаблона бизнес-процесса
 */
class BusinessProcessVersionsVirtualSchemeInfo {
  private readonly name: string = 'BusinessProcessVersionsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Version: string = 'Version';
  readonly Created: string = 'Created';
  readonly CreatedByID: string = 'CreatedByID';
  readonly CreatedByName: string = 'CreatedByName';
  readonly Modified: string = 'Modified';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';
  readonly ParentVersion: string = 'ParentVersion';
  readonly IsDefault: string = 'IsDefault';
  readonly LockedForEditing: string = 'LockedForEditing';
  readonly LockedByID: string = 'LockedByID';
  readonly LockedByName: string = 'LockedByName';
  readonly ActiveCount: string = 'ActiveCount';
  readonly Locked: string = 'Locked';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CalendarCalcMethods

/**
 * ID: {011f3246-c0f2-4d91-aaee-5129c6b83e15}
 * Alias: CalendarCalcMethods
 * Group: System
 */
class CalendarCalcMethodsSchemeInfo {
  private readonly name: string = 'CalendarCalcMethods';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Description: string = 'Description';
  readonly Script: string = 'Script';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CalendarExclusions

/**
 * ID: {aec4456f-c927-4a49-89f5-582ab17dc997}
 * Alias: CalendarExclusions
 * Group: System
 */
class CalendarExclusionsSchemeInfo {
  private readonly name: string = 'CalendarExclusions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StartTime: string = 'StartTime';
  readonly EndTime: string = 'EndTime';
  readonly IsNotWorkingTime: string = 'IsNotWorkingTime';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CalendarNamedRanges

/**
 * ID: {dc5ec614-d5df-40d1-ba43-4e5b97211711}
 * Alias: CalendarNamedRanges
 * Group: System
 */
class CalendarNamedRangesSchemeInfo {
  private readonly name: string = 'CalendarNamedRanges';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StartTime: string = 'StartTime';
  readonly EndTime: string = 'EndTime';
  readonly IsNotWorkingTime: string = 'IsNotWorkingTime';
  readonly Name: string = 'Name';
  readonly IsManual: string = 'IsManual';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CalendarQuants

/**
 * ID: {094fac6d-4fe8-4d3e-89c2-22a0f74fd705}
 * Alias: CalendarQuants
 * Group: System
 */
class CalendarQuantsSchemeInfo {
  private readonly name: string = 'CalendarQuants';

  //#region Columns

  readonly QuantNumber: string = 'QuantNumber';
  readonly StartTime: string = 'StartTime';
  readonly EndTime: string = 'EndTime';
  readonly Type: string = 'Type';
  readonly ID: string = 'ID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CalendarSettings

/**
 * ID: {67b1fd42-0106-4b31-a368-ea3e4d38ac5c}
 * Alias: CalendarSettings
 * Group: System
 */
class CalendarSettingsSchemeInfo {
  private readonly name: string = 'CalendarSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CalendarStart: string = 'CalendarStart';
  readonly CalendarEnd: string = 'CalendarEnd';
  readonly Description: string = 'Description';
  readonly CalendarID: string = 'CalendarID';
  readonly CalendarTypeID: string = 'CalendarTypeID';
  readonly CalendarTypeCaption: string = 'CalendarTypeCaption';
  readonly Name: string = 'Name';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CalendarTypeExclusions

/**
 * ID: {3a11e188-f82f-495b-a78f-778f2988db52}
 * Alias: CalendarTypeExclusions
 * Group: System
 */
class CalendarTypeExclusionsSchemeInfo {
  private readonly name: string = 'CalendarTypeExclusions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Caption: string = 'Caption';
  readonly StartTime: string = 'StartTime';
  readonly EndTime: string = 'EndTime';
  readonly IsNotWorkingTime: string = 'IsNotWorkingTime';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CalendarTypes

/**
 * ID: {c411ab46-1df7-4a76-97b5-d0d39fff656b}
 * Alias: CalendarTypes
 * Group: System
 */
class CalendarTypesSchemeInfo {
  private readonly name: string = 'CalendarTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Caption: string = 'Caption';
  readonly Description: string = 'Description';
  readonly CalcMethodID: string = 'CalcMethodID';
  readonly CalcMethodName: string = 'CalcMethodName';
  readonly HoursInDay: string = 'HoursInDay';
  readonly WorkDaysInWeek: string = 'WorkDaysInWeek';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CalendarTypeWeekDays

/**
 * ID: {67d63f49-ec4f-4e3b-9364-0b6e38d138ec}
 * Alias: CalendarTypeWeekDays
 * Group: System
 */
class CalendarTypeWeekDaysSchemeInfo {
  private readonly name: string = 'CalendarTypeWeekDays';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Number: string = 'Number';
  readonly Name: string = 'Name';
  readonly WorkingDayStart: string = 'WorkingDayStart';
  readonly WorkingDayEnd: string = 'WorkingDayEnd';
  readonly LunchStart: string = 'LunchStart';
  readonly LunchEnd: string = 'LunchEnd';
  readonly IsNotWorkingDay: string = 'IsNotWorkingDay';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CompilationCache

/**
 * ID: {3f86165e-8a0d-41d9-a7a2-b6a511bf551b}
 * Alias: CompilationCache
 * Group: System
 * Description: Результаты компиляции объектов системы.
 */
class CompilationCacheSchemeInfo {
  private readonly name: string = 'CompilationCache';

  //#region Columns

  readonly CategoryID: string = 'CategoryID';
  readonly ID: string = 'ID';
  readonly Result: string = 'Result';
  readonly Assembly: string = 'Assembly';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CompiledViews

/**
 * ID: {0ebd80aa-360b-473b-8327-90e10035c000}
 * Alias: CompiledViews
 * Group: System
 */
class CompiledViewsSchemeInfo {
  private readonly name: string = 'CompiledViews';

  //#region Columns

  readonly ViewID: string = 'ViewID';
  readonly ViewAlias: string = 'ViewAlias';
  readonly FunctionName: string = 'FunctionName';
  readonly LastUsed: string = 'LastUsed';
  readonly ViewModifiedDateTime: string = 'ViewModifiedDateTime';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CompletionOptions

/**
 * ID: {08cf782d-4130-4377-8a49-3e201a05d496}
 * Alias: CompletionOptions
 * Group: System
 * Description: Список возможных варианты завершения.
 */
class CompletionOptionsSchemeInfo {
  private readonly name: string = 'CompletionOptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';

  //#endregion

  //#region Enumeration

  readonly AddComment: CompletionOptions = new CompletionOptions('78c2fd7d-d0fe-0ede-93a6-9de4f372e8e6', 'AddComment', '$UI_Tasks_CompletionOptions_AddComment');
  readonly Approve: CompletionOptions = new CompletionOptions('8cf5cf41-8347-05b4-b3b2-519e8e621225', 'Approve', '$UI_Tasks_CompletionOptions_Approve');
  readonly Cancel: CompletionOptions = new CompletionOptions('2582b66f-375a-0d59-ae86-a149309c5785', 'Cancel', '$UI_Tasks_CompletionOptions_Cancel');
  readonly CancelApprovalProcess: CompletionOptions = new CompletionOptions('6e244482-2e2f-46fd-8ec3-0de6daea2930', 'CancelApprovalProcess', '$UI_Tasks_CompletionOptions_CancelApprovalProcess');
  readonly Complete: CompletionOptions = new CompletionOptions('5b108223-92db-49b9-8085-336758ccabaa', 'Complete', '$UI_Tasks_CompletionOptions_Complete');
  readonly Continue: CompletionOptions = new CompletionOptions('9ba9f111-fa2f-4c8e-8236-c924280a4a07', 'Continue', '$UI_Tasks_CompletionOptions_Continue');
  readonly CreateChildResolution: CompletionOptions = new CompletionOptions('793bbafa-7f62-4af8-a156-515887d4d066', 'CreateChildResolution', '$UI_Tasks_CompletionOptions_CreateChildResolution');
  readonly Delegate: CompletionOptions = new CompletionOptions('b997a7f2-ad57-036f-8798-298c14309f46', 'Delegate', '$UI_Tasks_CompletionOptions_Delegate');
  readonly DeregisterDocument: CompletionOptions = new CompletionOptions('66e0a7e1-484a-40a6-b123-06118ce3b160', 'DeregisterDocument', '$UI_Tasks_CompletionOptions_DeregisterDocument');
  readonly Disapprove: CompletionOptions = new CompletionOptions('811d41ef-5610-421e-a573-fcdfd821713e', 'Disapprove', '$UI_Tasks_CompletionOptions_Disapprove');
  readonly ModifyAsAuthor: CompletionOptions = new CompletionOptions('89ada741-6829-4d9f-892b-72d76ecf4ee6', 'ModifyAsAuthor', '$UI_Tasks_CompletionOptions_ModifyAsAuthor');
  readonly NewApprovalCycle: CompletionOptions = new CompletionOptions('c0b704b3-3ac5-4a0d-bcb6-1210e9cdb0b3', 'NewApprovalCycle', '$UI_Tasks_CompletionOptions_NewApprovalCycle');
  readonly OptionA: CompletionOptions = new CompletionOptions('d6fbbf34-d22d-4226-831d-f3f1f31b9954', 'OptionA', '$UI_Tasks_CompletionOptions_OptionA');
  readonly OptionB: CompletionOptions = new CompletionOptions('679a8309-f251-4acf-8b2e-7c5277b04d63', 'OptionB', '$UI_Tasks_CompletionOptions_OptionB');
  readonly RebuildDocument: CompletionOptions = new CompletionOptions('174d3f96-c658-07b7-ba6a-d51a893390d8', 'RebuildDocument', '$UI_Tasks_CompletionOptions_Rebuild');
  readonly RegisterDocument: CompletionOptions = new CompletionOptions('48ae0fd4-8a0d-494a-b89d-ca8fc33efe7c', 'RegisterDocument', '$UI_Tasks_CompletionOptions_RegisterDocument');
  readonly RejectApproval: CompletionOptions = new CompletionOptions('d97d75a9-96ae-00ca-83ad-baa5c6aa811b', 'RejectApproval', '$UI_Tasks_CompletionOptions_Reject');
  readonly RequestComments: CompletionOptions = new CompletionOptions('fffb3209-2b67-09f0-bd25-ba4ec94ca5e8', 'RequestComments', '$UI_Tasks_CompletionOptions_RequestComments');
  readonly Revoke: CompletionOptions = new CompletionOptions('6472fea9-f818-4ab5-9f31-9ccdaea9b412', 'Revoke', '$UI_Tasks_CompletionOptions_Revoke');
  readonly SendToPerformer: CompletionOptions = new CompletionOptions('f4ebe563-14f6-4b20-a61f-0bac4c11c8ac', 'SendToPerformer', '$UI_Tasks_CompletionOptions_SendToPerformer');
  readonly Accept: CompletionOptions = new CompletionOptions('7000ea10-efd8-0479-a6d4-b5e37a27f30a', 'Accept', '$UI_Tasks_CompletionOptions_Accept');
  readonly AdditionalApproval: CompletionOptions = new CompletionOptions('c726d8ba-73b9-4867-87fe-387d4c61a75a', 'AdditionalApproval', '$UI_Tasks_CompletionOptions_AdditionalApproval');
  readonly Sign: CompletionOptions = new CompletionOptions('45d6f756-d30b-4c98-9d72-6adf1a15d075', 'Sign', '$UI_Tasks_CompletionOptions_Sign');
  readonly Decline: CompletionOptions = new CompletionOptions('4de44ffd-c2ca-4fad-835b-631222b076e1', 'Decline', '$UI_Tasks_CompletionOptions_Decline');
  readonly ShowDialog: CompletionOptions = new CompletionOptions('a9067834-1a01-468c-976b-0ec7a9939331', 'ShowDialog', '$UI_Tasks_CompletionOptions_ShowDialog');
  readonly TakeOver: CompletionOptions = new CompletionOptions('08cf782d-4130-4377-8a49-3e201a05d496', 'TakeOver', '$UI_Tasks_CompletionOptions_TakeOver');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region CompletionOptions Enumeration

class CompletionOptions {
  readonly ID: guid;
  readonly Name: string | null;
  readonly Caption: string | null;

  constructor (ID: guid, Name: string | null, Caption: string | null) {
    this.ID = ID;
    this.Name = Name;
    this.Caption = Caption;
  }
}

//#endregion

//#endregion

//#region CompletionOptionsVirtual

/**
 * ID: {cfff92c8-26e6-42e5-b45d-837bc374022d}
 * Alias: CompletionOptionsVirtual
 * Group: System
 * Description: Виртуальная карточка для варианта завершения.
 */
class CompletionOptionsVirtualSchemeInfo {
  private readonly name: string = 'CompletionOptionsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly OptionID: string = 'OptionID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';
  readonly PartitionID: string = 'PartitionID';
  readonly PartitionName: string = 'PartitionName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ConditionsVirtual

/**
 * ID: {6d2ec0d3-4980-45f3-aa64-ab79eb9f4da1}
 * Alias: ConditionsVirtual
 * Group: System
 */
class ConditionsVirtualSchemeInfo {
  private readonly name: string = 'ConditionsVirtual';

  //#region Columns

  readonly TriggerRowID: string = 'TriggerRowID';
  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RuleRowID: string = 'RuleRowID';
  readonly ConditionTypeID: string = 'ConditionTypeID';
  readonly ConditionTypeName: string = 'ConditionTypeName';
  readonly Order: string = 'Order';
  readonly InvertCondition: string = 'InvertCondition';
  readonly Settings: string = 'Settings';
  readonly Description: string = 'Description';
  readonly InvertConditionString: string = 'InvertConditionString';
  readonly WorkflowConditionRowID: string = 'WorkflowConditionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ConditionTypes

/**
 * ID: {7e0c2c3b-e8f3-4f96-9aa6-eb1c2100d74f}
 * Alias: ConditionTypes
 * Group: System
 * Description: Тип условия
 */
class ConditionTypesSchemeInfo {
  private readonly name: string = 'ConditionTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly SettingsCardTypeID: string = 'SettingsCardTypeID';
  readonly SettingsCardTypeCaption: string = 'SettingsCardTypeCaption';
  readonly ConditionText: string = 'ConditionText';
  readonly Condition: string = 'Condition';
  readonly Description: string = 'Description';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ConditionTypeUsePlaces

/**
 * ID: {b764f842-5b97-4de7-854a-f61b6b7a71dc}
 * Alias: ConditionTypeUsePlaces
 * Group: System
 */
class ConditionTypeUsePlacesSchemeInfo {
  private readonly name: string = 'ConditionTypeUsePlaces';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly UsePlaceName: string = 'UsePlaceName';
  readonly UsePlaceID: string = 'UsePlaceID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ConditionUsePlaces

/**
 * ID: {6963c76f-5e8d-49b5-80a3-f2ec342de0bf}
 * Alias: ConditionUsePlaces
 * Group: System
 */
class ConditionUsePlacesSchemeInfo {
  private readonly name: string = 'ConditionUsePlaces';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly AclGenerationRule: ConditionUsePlaces = new ConditionUsePlaces('76b57ab5-19d4-4cc9-9e80-2c2f05cc2425', '$ConditionUsePlace_AclGenerationRule');
  readonly SmartRoleGenerator: ConditionUsePlaces = new ConditionUsePlaces('c72e05fb-7eef-4256-9029-72f821f4f79e', '$ConditionUsePlace_SmartRoleGenerator');
  readonly NotificationRules: ConditionUsePlaces = new ConditionUsePlaces('929ad23c-8a22-09aa-9000-398bf13979b2', '$ConditionUsePlace_NotificationRules');
  readonly KrVirtualFiles: ConditionUsePlaces = new ConditionUsePlaces('81250a95-5c1e-488c-a423-106e7f982c6b', '$ConditionUsePlace_KrVirtualFiles');
  readonly KrPermissions: ConditionUsePlaces = new ConditionUsePlaces('fa9dbdac-8708-41df-bd72-900f69655dfa', '$ConditionUsePlace_KrPermissions');
  readonly KrSecondaryProcesses: ConditionUsePlaces = new ConditionUsePlaces('61420fa1-cc1f-47cb-b0bb-4ea8ee77f51a', '$ConditionUsePlace_KrSecondaryProcesses');
  readonly WorkflowEngine: ConditionUsePlaces = new ConditionUsePlaces('eb222506-6f7d-4c22-b3d2-d98a2f390ac5', '$ConditionUsePlace_WorkflowEngine');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region ConditionUsePlaces Enumeration

class ConditionUsePlaces {
  readonly ID: guid;
  readonly Name: string | null;

  constructor (ID: guid, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region Configuration

/**
 * ID: {57b9e507-d135-4c69-9a94-bf507d499484}
 * Alias: Configuration
 * Group: System
 * Description: Configuration properties
 */
class ConfigurationSchemeInfo {
  private readonly name: string = 'Configuration';

  //#region Columns

  readonly BuildVersion: string = 'BuildVersion';
  readonly BuildName: string = 'BuildName';
  readonly BuildDate: string = 'BuildDate';
  readonly Description: string = 'Description';
  readonly Modified: string = 'Modified';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';
  readonly Version: string = 'Version';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ContextRoles

/**
 * ID: {be5a85fd-b2fb-4f60-a3b7-48e79e45249f}
 * Alias: ContextRoles
 * Group: Roles
 * Description: Контекстные роли.
 */
class ContextRolesSchemeInfo {
  private readonly name: string = 'ContextRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly SqlText: string = 'SqlText';
  readonly SqlTextForCard: string = 'SqlTextForCard';
  readonly SqlTextForUser: string = 'SqlTextForUser';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Currencies

/**
 * ID: {3612e150-032f-4a68-bf8e-8e094e5a3a73}
 * Alias: Currencies
 * Group: Common
 * Description: Валюты.
 */
class CurrenciesSchemeInfo {
  private readonly name: string = 'Currencies';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';
  readonly Code: string = 'Code';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CustomBackgroundColorsVirtual

/**
 * ID: {5d65177e-590c-4422-9120-1a202a534640}
 * Alias: CustomBackgroundColorsVirtual
 * Group: System
 */
class CustomBackgroundColorsVirtualSchemeInfo {
  private readonly name: string = 'CustomBackgroundColorsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Color1: string = 'Color1';
  readonly Color2: string = 'Color2';
  readonly Color3: string = 'Color3';
  readonly Color4: string = 'Color4';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CustomBlockColorsVirtual

/**
 * ID: {cafa4371-0483-4d71-80cd-75d68cd6086f}
 * Alias: CustomBlockColorsVirtual
 * Group: System
 */
class CustomBlockColorsVirtualSchemeInfo {
  private readonly name: string = 'CustomBlockColorsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Color1: string = 'Color1';
  readonly Color2: string = 'Color2';
  readonly Color3: string = 'Color3';
  readonly Color4: string = 'Color4';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CustomForegroundColorsVirtual

/**
 * ID: {8f0adc86-8166-4579-9a25-7c3f2921d32d}
 * Alias: CustomForegroundColorsVirtual
 * Group: System
 */
class CustomForegroundColorsVirtualSchemeInfo {
  private readonly name: string = 'CustomForegroundColorsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Color1: string = 'Color1';
  readonly Color2: string = 'Color2';
  readonly Color3: string = 'Color3';
  readonly Color4: string = 'Color4';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region DateFormats

/**
 * ID: {585825ed-e297-4eb3-bea2-a732ad75c6b6}
 * Alias: DateFormats
 * Group: System
 * Description: Формат для отображаемых дат, определяет порядок следования дня, месяца и года.
 */
class DateFormatsSchemeInfo {
  private readonly name: string = 'DateFormats';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';

  //#endregion

  //#region Enumeration

  readonly MonthDayYear: DateFormats = new DateFormats(0, 'MonthDayYear', '$Enum_DateFormats_MonthDayYear');
  readonly DayMonthYear: DateFormats = new DateFormats(1, 'DayMonthYear', '$Enum_DateFormats_DayMonthYear');
  readonly YearMonthDay: DateFormats = new DateFormats(2, 'YearMonthDay', '$Enum_DateFormats_YearMonthDay');
  readonly YearDayMonth: DateFormats = new DateFormats(3, 'YearDayMonth', '$Enum_DateFormats_YearDayMonth');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region DateFormats Enumeration

class DateFormats {
  readonly ID: number;
  readonly Name: string | null;
  readonly Caption: string | null;

  constructor (ID: number, Name: string | null, Caption: string | null) {
    this.ID = ID;
    this.Name = Name;
    this.Caption = Caption;
  }
}

//#endregion

//#endregion

//#region DefaultTimeZone

/**
 * ID: {d894a451-c0ff-4a75-b808-05d24cf077bf}
 * Alias: DefaultTimeZone
 * Group: System
 * Description: Данные для временной зоны по умолчанию.
 * Хранятся отдельно, чтобы не затирались при изменении таблички с временными зонами (TimeZones)
 */
class DefaultTimeZoneSchemeInfo {
  private readonly name: string = 'DefaultTimeZone';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CodeName: string = 'CodeName';
  readonly UtcOffsetMinutes: string = 'UtcOffsetMinutes';
  readonly DisplayName: string = 'DisplayName';
  readonly ShortName: string = 'ShortName';
  readonly IsNegativeOffsetDirection: string = 'IsNegativeOffsetDirection';
  readonly OffsetTime: string = 'OffsetTime';
  readonly ZoneID: string = 'ZoneID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region DefaultWorkplacesVirtual

/**
 * ID: {dd42ee04-02c5-407b-b596-07aa830a9b80}
 * Alias: DefaultWorkplacesVirtual
 * Group: System
 * Description: Список рабочих мест открываемых по умолчанию
 */
class DefaultWorkplacesVirtualSchemeInfo {
  private readonly name: string = 'DefaultWorkplacesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly WorkplaceID: string = 'WorkplaceID';
  readonly WorkplaceName: string = 'WorkplaceName';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Deleted

/**
 * ID: {a49102cc-6bb4-425b-95ad-75ff0b3edf0d}
 * Alias: Deleted
 * Group: System
 * Description: Информация об удалённой карточке.
 */
class DeletedSchemeInfo {
  private readonly name: string = 'Deleted';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Digest: string = 'Digest';
  readonly Card: string = 'Card';
  readonly CardID: string = 'CardID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly Created: string = 'Created';
  readonly CreatedByID: string = 'CreatedByID';
  readonly CreatedByName: string = 'CreatedByName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region DeletedTaskRoles

/**
 * ID: {8340a9b3-74ba-4771-af73-35bed38db55e}
 * Alias: DeletedTaskRoles
 * Group: System
 * Description: Роли, на которые были назначены задания в удалённой карточке.
 */
class DeletedTaskRolesSchemeInfo {
  private readonly name: string = 'DeletedTaskRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly RoleTypeID: string = 'RoleTypeID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region DeletedVirtual

/**
 * ID: {300db9a6-f6a0-48a8-b6c3-5f8891817cdd}
 * Alias: DeletedVirtual
 * Group: System
 */
class DeletedVirtualSchemeInfo {
  private readonly name: string = 'DeletedVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CardStorage: string = 'CardStorage';
  readonly CardIDString: string = 'CardIDString';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region DepartmentRoles

/**
 * ID: {d43dace1-536f-4c9f-af15-49a8892a7427}
 * Alias: DepartmentRoles
 * Group: Roles
 * Description: Роли департаментов.
 */
class DepartmentRolesSchemeInfo {
  private readonly name: string = 'DepartmentRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly HeadUserID: string = 'HeadUserID';
  readonly HeadUserName: string = 'HeadUserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region DeviceTypes

/**
 * ID: {8b4cd042-334b-4aee-a623-7d8942aa6897}
 * Alias: DeviceTypes
 * Group: System
 * Description: Типы устройств, с которых пользователь использует приложения Tessa.
 */
class DeviceTypesSchemeInfo {
  private readonly name: string = 'DeviceTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Other: DeviceTypes = new DeviceTypes(0, '$Enum_DeviceTypes_Other');
  readonly Desktop: DeviceTypes = new DeviceTypes(1, '$Enum_DeviceTypes_Desktop');
  readonly Phone: DeviceTypes = new DeviceTypes(2, '$Enum_DeviceTypes_Phone');
  readonly Tablet: DeviceTypes = new DeviceTypes(3, '$Enum_DeviceTypes_Tablet');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region DeviceTypes Enumeration

class DeviceTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region DialogButtonTypes

/**
 * ID: {e07bb4d3-1312-4638-9751-ddd8e3a127fc}
 * Alias: DialogButtonTypes
 * Group: System
 */
class DialogButtonTypesSchemeInfo {
  private readonly name: string = 'DialogButtonTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly ToolbarButton: DialogButtonTypes = new DialogButtonTypes(0, '$DialogButtonTypes_ToolbarButton');
  readonly BottomToolbarButton: DialogButtonTypes = new DialogButtonTypes(1, '$DialogButtonTypes_BottomToolbarButton');
  readonly BottomDialogButton: DialogButtonTypes = new DialogButtonTypes(2, '$DialogButtonTypes_BottomDialogButton');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region DialogButtonTypes Enumeration

class DialogButtonTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region DialogCardAutoOpenModes

/**
 * ID: {b1827f66-89bd-4269-b2ce-ea27337616fd}
 * Alias: DialogCardAutoOpenModes
 * Group: System
 */
class DialogCardAutoOpenModesSchemeInfo {
  private readonly name: string = 'DialogCardAutoOpenModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly AlwaysOpen: DialogCardAutoOpenModes = new DialogCardAutoOpenModes(0, '$DialogCardAutoOpenModes_AlwaysOpen');
  readonly ButtonClickOpen: DialogCardAutoOpenModes = new DialogCardAutoOpenModes(1, '$DialogCardAutoOpenModes_ButtonClickOpen');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region DialogCardAutoOpenModes Enumeration

class DialogCardAutoOpenModes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region DialogCardStoreModes

/**
 * ID: {f383bf09-2ec9-4fe5-aa50-f3b14898c976}
 * Alias: DialogCardStoreModes
 * Group: System
 */
class DialogCardStoreModesSchemeInfo {
  private readonly name: string = 'DialogCardStoreModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly StoreIntoInfo: DialogCardStoreModes = new DialogCardStoreModes(0, '$DialogCardStoreModes_StoreIntoInfo');
  readonly StoreIntoSettings: DialogCardStoreModes = new DialogCardStoreModes(1, '$DialogCardStoreModes_StoreIntoSettings');
  readonly StoreAsCard: DialogCardStoreModes = new DialogCardStoreModes(2, '$DialogCardStoreModes_StoreAsCard');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region DialogCardStoreModes Enumeration

class DialogCardStoreModes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region DialogRoles

/**
 * ID: {125ad61a-3698-4d07-9fa0-139c9cc25074}
 * Alias: DialogRoles
 * Group: System
 */
class DialogRolesSchemeInfo {
  private readonly name: string = 'DialogRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Dialogs

/**
 * ID: {53a54dce-29d9-4f2c-8522-73ca60a4dbb5}
 * Alias: Dialogs
 * Group: System
 * Description: Виртуальная таблица для диалогов в системных карточках.
 */
class DialogsSchemeInfo {
  private readonly name: string = 'Dialogs';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CardCount: string = 'CardCount';
  readonly ChangePartner: string = 'ChangePartner';
  readonly ChangeAuthor: string = 'ChangeAuthor';
  readonly Comment: string = 'Comment';
  readonly OldPassword: string = 'OldPassword';
  readonly Password: string = 'Password';
  readonly PasswordRepeat: string = 'PasswordRepeat';
  readonly AppID: string = 'AppID';
  readonly AppName: string = 'AppName';
  readonly AppVersion: string = 'AppVersion';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region DocLoadBarcodeRead

/**
 * ID: {3d7fe6dc-f80f-4399-83aa-261e4624aaf1}
 * Alias: DocLoadBarcodeRead
 * Group: System
 */
class DocLoadBarcodeReadSchemeInfo {
  private readonly name: string = 'DocLoadBarcodeRead';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly BarcodeID: string = 'BarcodeID';
  readonly BarcodeName: string = 'BarcodeName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region DocLoadSettings

/**
 * ID: {0dbef4e9-7bf7-4b8f-aab0-fa908bc30e6f}
 * Alias: DocLoadSettings
 * Group: System
 */
class DocLoadSettingsSchemeInfo {
  private readonly name: string = 'DocLoadSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly InputPath: string = 'InputPath';
  readonly OutputPath: string = 'OutputPath';
  readonly ErrorPath: string = 'ErrorPath';
  readonly ExcludeBarcodePage: string = 'ExcludeBarcodePage';
  readonly DefaultBarcodeTableID: string = 'DefaultBarcodeTableID';
  readonly DefaultBarcodeTableName: string = 'DefaultBarcodeTableName';
  readonly DefaultBarcodeFieldID: string = 'DefaultBarcodeFieldID';
  readonly DefaultBarcodeFieldName: string = 'DefaultBarcodeFieldName';
  readonly BarcodeWriteID: string = 'BarcodeWriteID';
  readonly BarcodeWriteName: string = 'BarcodeWriteName';
  readonly ForceRender: string = 'ForceRender';
  readonly BarcodeFormat: string = 'BarcodeFormat';
  readonly BarcodeSequence: string = 'BarcodeSequence';
  readonly DocFormatName: string = 'DocFormatName';
  readonly BarcodeLabel: string = 'BarcodeLabel';
  readonly BarcodeWidth: string = 'BarcodeWidth';
  readonly BarcodeHeight: string = 'BarcodeHeight';
  readonly IsEnabled: string = 'IsEnabled';
  readonly ShowHeader: string = 'ShowHeader';
  readonly OffsetWidth: string = 'OffsetWidth';
  readonly OffsetHeight: string = 'OffsetHeight';
  readonly StartScale: string = 'StartScale';
  readonly StopScale: string = 'StopScale';
  readonly IncrementScale: string = 'IncrementScale';
  readonly SessionUserID: string = 'SessionUserID';
  readonly SessionUserName: string = 'SessionUserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region DocumentCategories

/**
 * ID: {f939aa52-dc1a-40b2-af4a-cb2757e8390a}
 * Alias: DocumentCategories
 * Group: Common
 * Description: Категории документов для Протоколов, СЗ и Документа (бывшие типы протоколов)
 */
class DocumentCategoriesSchemeInfo {
  private readonly name: string = 'DocumentCategories';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region DocumentCommonInfo

/**
 * ID: {a161e289-2f99-4699-9e95-6e3336be8527}
 * Alias: DocumentCommonInfo
 * Group: Common
 * Description: Общая секция для всех видов документов.
 * Должна использоваться для всех типов карточек, использующих такие поля, как номер, контрагент, тема и т.п.
 * Помимо всего прочего, активно используется в поиске и представлениях.
 */
class DocumentCommonInfoSchemeInfo {
  private readonly name: string = 'DocumentCommonInfo';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CardTypeID: string = 'CardTypeID';
  readonly DocTypeID: string = 'DocTypeID';
  readonly DocTypeTitle: string = 'DocTypeTitle';
  readonly Number: string = 'Number';
  readonly FullNumber: string = 'FullNumber';
  readonly Sequence: string = 'Sequence';
  readonly SecondaryNumber: string = 'SecondaryNumber';
  readonly SecondaryFullNumber: string = 'SecondaryFullNumber';
  readonly SecondarySequence: string = 'SecondarySequence';
  readonly Subject: string = 'Subject';
  readonly DocDate: string = 'DocDate';
  readonly CreationDate: string = 'CreationDate';
  readonly OutgoingNumber: string = 'OutgoingNumber';
  readonly Amount: string = 'Amount';
  readonly Barcode: string = 'Barcode';
  readonly CurrencyID: string = 'CurrencyID';
  readonly CurrencyName: string = 'CurrencyName';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly RegistratorID: string = 'RegistratorID';
  readonly RegistratorName: string = 'RegistratorName';
  readonly SignedByID: string = 'SignedByID';
  readonly SignedByName: string = 'SignedByName';
  readonly DepartmentID: string = 'DepartmentID';
  readonly DepartmentName: string = 'DepartmentName';
  readonly PartnerID: string = 'PartnerID';
  readonly PartnerName: string = 'PartnerName';
  readonly RefDocID: string = 'RefDocID';
  readonly RefDocDescription: string = 'RefDocDescription';
  readonly ReceiverRowID: string = 'ReceiverRowID';
  readonly ReceiverName: string = 'ReceiverName';
  readonly CategoryID: string = 'CategoryID';
  readonly CategoryName: string = 'CategoryName';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region DynamicRoles

/**
 * ID: {4a282d48-6d78-4923-85e4-8d3f9be213fa}
 * Alias: DynamicRoles
 * Group: Roles
 * Description: Динамические роли.
 */
class DynamicRolesSchemeInfo {
  private readonly name: string = 'DynamicRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly SqlText: string = 'SqlText';
  readonly SchedulingTypeID: string = 'SchedulingTypeID';
  readonly CronScheduling: string = 'CronScheduling';
  readonly PeriodScheduling: string = 'PeriodScheduling';
  readonly LastErrorDate: string = 'LastErrorDate';
  readonly LastErrorText: string = 'LastErrorText';
  readonly LastSuccessfulRecalcDate: string = 'LastSuccessfulRecalcDate';
  readonly ScheduleAtLaunch: string = 'ScheduleAtLaunch';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Errors

/**
 * ID: {754008b7-831b-44f9-9c58-99fa0334e62f}
 * Alias: Errors
 * Group: System
 */
class ErrorsSchemeInfo {
  private readonly name: string = 'Errors';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ActionID: string = 'ActionID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly CardID: string = 'CardID';
  readonly CardDigest: string = 'CardDigest';
  readonly Request: string = 'Request';
  readonly Category: string = 'Category';
  readonly Text: string = 'Text';
  readonly Modified: string = 'Modified';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FieldChangedCondition

/**
 * ID: {06245b07-be2a-40de-aec8-bfd367860930}
 * Alias: FieldChangedCondition
 * Group: Acl
 * Description: Секция для условийпроверяющих изменение поля.
 */
class FieldChangedConditionSchemeInfo {
  private readonly name: string = 'FieldChangedCondition';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly FieldID: string = 'FieldID';
  readonly FieldName: string = 'FieldName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileCategories

/**
 * ID: {e1599715-02d4-4ca9-b63e-b4b1ce642c7a}
 * Alias: FileCategories
 * Group: System
 */
class FileCategoriesSchemeInfo {
  private readonly name: string = 'FileCategories';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileContent

/**
 * ID: {328af88c-b21a-4c2a-b825-45a086d0b24b}
 * Alias: FileContent
 * Group: System
 * Description: Контент файлов.
 */
class FileContentSchemeInfo {
  private readonly name: string = 'FileContent';

  //#region Columns

  readonly VersionRowID: string = 'VersionRowID';
  readonly Content: string = 'Content';
  readonly Ext: string = 'Ext';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileConverterCache

/**
 * ID: {b376adb4-3134-4ec5-9597-a83e8c9db0f1}
 * Alias: FileConverterCache
 * Group: System
 * Description: Информация по сконвертированным файлам, добавленным в кэш.
 * Идентификатор RowID равен идентификатору файла в кэше Files.RowID.
 */
class FileConverterCacheSchemeInfo {
  private readonly name: string = 'FileConverterCache';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly VersionID: string = 'VersionID';
  readonly RequestHash: string = 'RequestHash';
  readonly ResponseInfo: string = 'ResponseInfo';
  readonly LastAccessTime: string = 'LastAccessTime';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileConverterCacheVirtual

/**
 * ID: {961887ca-e67c-4283-89fc-265dbf17e4c1}
 * Alias: FileConverterCacheVirtual
 * Group: System
 * Description: Информация, отображаемая в карточке файловых конвертеров.
 */
class FileConverterCacheVirtualSchemeInfo {
  private readonly name: string = 'FileConverterCacheVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly FileCount: string = 'FileCount';
  readonly FileCountText: string = 'FileCountText';
  readonly OldestFileAccessTime: string = 'OldestFileAccessTime';
  readonly NewestFileAccessTime: string = 'NewestFileAccessTime';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileConverterTypes

/**
 * ID: {a1dd7426-13e0-42fb-a45a-0a714108e274}
 * Alias: FileConverterTypes
 * Group: System
 * Description: Варианты конвертеров файлов из формата в формат
 */
class FileConverterTypesSchemeInfo {
  private readonly name: string = 'FileConverterTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly None: FileConverterTypes = new FileConverterTypes(0, '$Enum_FileConverterTypes_None');
  readonly OpenLibre: FileConverterTypes = new FileConverterTypes(1, '$Enum_FileConverterTypes_OpenLibre');
  readonly OnlyOfficeService: FileConverterTypes = new FileConverterTypes(2, '$Enum_FileConverterTypes_OnlyOfficeService');
  readonly OnlyOfficeDocumentBuilder: FileConverterTypes = new FileConverterTypes(3, '$Enum_FileConverterTypes_OnlyOfficeDocumentBuilder');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region FileConverterTypes Enumeration

class FileConverterTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region Files

/**
 * ID: {dd716146-b177-4920-bc90-b1196b16347c}
 * Alias: Files
 * Group: System
 * Description: Файлы, приложенные к карточкам.
 */
class FilesSchemeInfo {
  private readonly name: string = 'Files';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';
  readonly TaskID: string = 'TaskID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly VersionRowID: string = 'VersionRowID';
  readonly VersionNumber: string = 'VersionNumber';
  readonly Created: string = 'Created';
  readonly CreatedByID: string = 'CreatedByID';
  readonly CreatedByName: string = 'CreatedByName';
  readonly Modified: string = 'Modified';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';
  readonly CategoryID: string = 'CategoryID';
  readonly CategoryCaption: string = 'CategoryCaption';
  readonly OriginalFileID: string = 'OriginalFileID';
  readonly OriginalVersionRowID: string = 'OriginalVersionRowID';
  readonly Options: string = 'Options';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileSignatureEventTypes

/**
 * ID: {5a8e7767-cd46-4ace-9da3-e3ea6f38cff2}
 * Alias: FileSignatureEventTypes
 * Group: System
 * Description: События, в результате которых подпись была добавлена в систему.
 */
class FileSignatureEventTypesSchemeInfo {
  private readonly name: string = 'FileSignatureEventTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Other: FileSignatureEventTypes = new FileSignatureEventTypes(0, '$Enum_FileSignatureEventTypes_Other');
  readonly Imported: FileSignatureEventTypes = new FileSignatureEventTypes(1, '$Enum_FileSignatureEventTypes_Imported');
  readonly Signed: FileSignatureEventTypes = new FileSignatureEventTypes(2, '$Enum_FileSignatureEventTypes_Signed');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region FileSignatureEventTypes Enumeration

class FileSignatureEventTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region FileSignatures

/**
 * ID: {5f428478-eaf5-4180-bde9-499483c3f80c}
 * Alias: FileSignatures
 * Group: System
 * Description: Подписи файла.
 */
class FileSignaturesSchemeInfo {
  private readonly name: string = 'FileSignatures';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly VersionRowID: string = 'VersionRowID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly EventID: string = 'EventID';
  readonly Comment: string = 'Comment';
  readonly SubjectName: string = 'SubjectName';
  readonly Company: string = 'Company';
  readonly Signed: string = 'Signed';
  readonly SerialNumber: string = 'SerialNumber';
  readonly IssuerName: string = 'IssuerName';
  readonly Data: string = 'Data';
  readonly SignatureTypeID: string = 'SignatureTypeID';
  readonly SignatureProfileID: string = 'SignatureProfileID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileSources

/**
 * ID: {e8300fe5-3b24-4c27-a45a-6cd8575bfcd5}
 * Alias: FileSources
 * Group: System
 * Description: Способы хранения файлов.
 */
class FileSourcesSchemeInfo {
  private readonly name: string = 'FileSources';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Path: string = 'Path';
  readonly IsDatabase: string = 'IsDatabase';
  readonly Description: string = 'Description';
  readonly Size: string = 'Size';
  readonly MaxSize: string = 'MaxSize';
  readonly FileExtensions: string = 'FileExtensions';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileSourcesVirtual

/**
 * ID: {64bbc32d-95d6-434b-9c08-0288344d53bb}
 * Alias: FileSourcesVirtual
 * Group: System
 * Description: Способы хранения файлов. Виртуальная таблица, обеспечивающая редактирование таблицы FileSources через карточку настроек.
 * Колонка ID в этой таблице соответствует идентификатору карточки настроек, а колонка SourceID - идентификатору источника файлов, т.е. аналог FileSources.ID.
 */
class FileSourcesVirtualSchemeInfo {
  private readonly name: string = 'FileSourcesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly IsDefault: string = 'IsDefault';
  readonly SourceID: string = 'SourceID';
  readonly SourceIDText: string = 'SourceIDText';
  readonly Name: string = 'Name';
  readonly IsDatabase: string = 'IsDatabase';
  readonly Path: string = 'Path';
  readonly Description: string = 'Description';
  readonly Size: string = 'Size';
  readonly MaxSize: string = 'MaxSize';
  readonly FileExtensions: string = 'FileExtensions';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileStates

/**
 * ID: {de9ba182-3fc4-4f20-9060-fa83b74fd46c}
 * Alias: FileStates
 * Group: System
 * Description: Состояние версии файла.
 */
class FileStatesSchemeInfo {
  private readonly name: string = 'FileStates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Uploading: FileStates = new FileStates(0, 'Uploading');
  readonly Success: FileStates = new FileStates(1, 'Success');
  readonly Error: FileStates = new FileStates(2, 'Error');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region FileStates Enumeration

class FileStates {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region FileTemplateRoles

/**
 * ID: {0eabfebc-fa8b-41b9-9aa3-6db9626a8ac6}
 * Alias: FileTemplateRoles
 * Group: System
 */
class FileTemplateRolesSchemeInfo {
  private readonly name: string = 'FileTemplateRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileTemplates

/**
 * ID: {98e0c3a9-0b9a-4fec-9843-4a077f6ff5f0}
 * Alias: FileTemplates
 * Group: System
 */
class FileTemplatesSchemeInfo {
  private readonly name: string = 'FileTemplates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly GroupName: string = 'GroupName';
  readonly PlaceholdersInfo: string = 'PlaceholdersInfo';
  readonly AliasMetadata: string = 'AliasMetadata';
  readonly TypeID: string = 'TypeID';
  readonly TypeName: string = 'TypeName';
  readonly BeforeDocumentReplace: string = 'BeforeDocumentReplace';
  readonly BeforeTableReplace: string = 'BeforeTableReplace';
  readonly BeforeRowReplace: string = 'BeforeRowReplace';
  readonly BeforePlaceholderReplace: string = 'BeforePlaceholderReplace';
  readonly AfterPlaceholderReplace: string = 'AfterPlaceholderReplace';
  readonly AfterRowReplace: string = 'AfterRowReplace';
  readonly AfterTableReplace: string = 'AfterTableReplace';
  readonly AfterDocumentReplace: string = 'AfterDocumentReplace';
  readonly System: string = 'System';
  readonly ConvertToPDF: string = 'ConvertToPDF';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileTemplateTemplateTypes

/**
 * ID: {54994b70-b619-4280-b9ff-31c20453a462}
 * Alias: FileTemplateTemplateTypes
 * Group: System
 */
class FileTemplateTemplateTypesSchemeInfo {
  private readonly name: string = 'FileTemplateTemplateTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Card: FileTemplateTemplateTypes = new FileTemplateTemplateTypes(0, '$FileTemplateType_Card');
  readonly View: FileTemplateTemplateTypes = new FileTemplateTemplateTypes(1, '$FileTemplateType_View');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region FileTemplateTemplateTypes Enumeration

class FileTemplateTemplateTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region FileTemplateTypes

/**
 * ID: {628e0e44-564c-4107-b943-0ec1e378bae7}
 * Alias: FileTemplateTypes
 * Group: System
 */
class FileTemplateTypesSchemeInfo {
  private readonly name: string = 'FileTemplateTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileTemplateViews

/**
 * ID: {ebd081b9-aaf9-4bab-be51-602803756e8d}
 * Alias: FileTemplateViews
 * Group: System
 */
class FileTemplateViewsSchemeInfo {
  private readonly name: string = 'FileTemplateViews';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ViewID: string = 'ViewID';
  readonly ViewAlias: string = 'ViewAlias';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FileVersions

/**
 * ID: {e17fd270-5c61-49af-955d-ed6bb983f0d8}
 * Alias: FileVersions
 * Group: System
 * Description: Версии файла.
 */
class FileVersionsSchemeInfo {
  private readonly name: string = 'FileVersions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Number: string = 'Number';
  readonly Name: string = 'Name';
  readonly Size: string = 'Size';
  readonly Created: string = 'Created';
  readonly CreatedByID: string = 'CreatedByID';
  readonly CreatedByName: string = 'CreatedByName';
  readonly SourceID: string = 'SourceID';
  readonly StateID: string = 'StateID';
  readonly ErrorDate: string = 'ErrorDate';
  readonly ErrorMessage: string = 'ErrorMessage';
  readonly Hash: string = 'Hash';
  readonly Options: string = 'Options';
  readonly LinkID: string = 'LinkID';
  readonly Tags: string = 'Tags';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FmAttachments

/**
 * ID: {3f903804-3c70-4828-9887-5c9268d20b7d}
 * Alias: FmAttachments
 * Group: Fm
 * Description: Таблица с прикрепленными элементами
 */
class FmAttachmentsSchemeInfo {
  private readonly name: string = 'FmAttachments';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Uri: string = 'Uri';
  readonly Caption: string = 'Caption';
  readonly TypeID: string = 'TypeID';
  readonly MessageRowID: string = 'MessageRowID';
  readonly FileSize: string = 'FileSize';
  readonly OriginalFileID: string = 'OriginalFileID';
  readonly ShowInToolbar: string = 'ShowInToolbar';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FmAttachmentTypes

/**
 * ID: {74caae68-ee60-4d36-b6af-b81bdd06d4a3}
 * Alias: FmAttachmentTypes
 * Group: Fm
 * Description: Типы прикрепленных элементов (файлы, ссылки и пр)
 */
class FmAttachmentTypesSchemeInfo {
  private readonly name: string = 'FmAttachmentTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly File: FmAttachmentTypes = new FmAttachmentTypes(0, 'File');
  readonly Link: FmAttachmentTypes = new FmAttachmentTypes(1, 'Link');
  readonly InnerItem: FmAttachmentTypes = new FmAttachmentTypes(2, 'InnerItem');
  readonly ExternalInnerItem: FmAttachmentTypes = new FmAttachmentTypes(3, 'ExternalInnerItem');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region FmAttachmentTypes Enumeration

class FmAttachmentTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region FmMessages

/**
 * ID: {a03f6c5d-e719-43d6-bcc5-d2ea321765ab}
 * Alias: FmMessages
 * Group: Fm
 * Description: Таблица для хранения сообщений
 */
class FmMessagesSchemeInfo {
  private readonly name: string = 'FmMessages';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Body: string = 'Body';
  readonly Created: string = 'Created';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly TopicRowID: string = 'TopicRowID';
  readonly TypeID: string = 'TypeID';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';
  readonly ModifiedAt: string = 'ModifiedAt';
  readonly PlainText: string = 'PlainText';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FmMessagesPluginTable

/**
 * ID: {18b094ec-a87f-4ccb-bfe8-a5936cc38992}
 * Alias: FmMessagesPluginTable
 * Group: Fm
 * Description: Таблица со временем последней даты запуска плагина для email рассылки сообщений форумов
 */
class FmMessagesPluginTableSchemeInfo {
  private readonly name: string = 'FmMessagesPluginTable';

  //#region Columns

  readonly LastPluginRunDate: string = 'LastPluginRunDate';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FmMessageTypes

/**
 * ID: {43f92881-c875-437a-bf1c-b7793c099d00}
 * Alias: FmMessageTypes
 * Group: Fm
 * Description: Типы сообщений
 */
class FmMessageTypesSchemeInfo {
  private readonly name: string = 'FmMessageTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Default: FmMessageTypes = new FmMessageTypes(0, 'Default');
  readonly AddUser: FmMessageTypes = new FmMessageTypes(1, 'AddUser');
  readonly RemoveUser: FmMessageTypes = new FmMessageTypes(2, 'RemoveUser');
  readonly AddRoles: FmMessageTypes = new FmMessageTypes(3, 'AddRoles');
  readonly RemoveRoles: FmMessageTypes = new FmMessageTypes(4, 'RemoveRoles');
  readonly Custom: FmMessageTypes = new FmMessageTypes(5, 'Custom');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region FmMessageTypes Enumeration

class FmMessageTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region FmNotifications

/**
 * ID: {fe822963-6091-4f70-9fbe-167aba72b4a2}
 * Alias: FmNotifications
 * Group: Fm
 * Description: Таблица для хранения уведомления
 */
class FmNotificationsSchemeInfo {
  private readonly name: string = 'FmNotifications';

  //#region Columns

  readonly UserID: string = 'UserID';
  readonly Batch0: string = 'Batch0';
  readonly Batch1: string = 'Batch1';
  readonly Count0: string = 'Count0';
  readonly Count1: string = 'Count1';
  readonly ReadMessages0: string = 'ReadMessages0';
  readonly ReadMessages1: string = 'ReadMessages1';
  readonly ActiveBatch: string = 'ActiveBatch';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FmParticipantTypes

/**
 * ID: {2b2a8e44-eecd-4afe-b017-20f8a00846ff}
 * Alias: FmParticipantTypes
 * Group: Fm
 * Description: Типы участников форума
 */
class FmParticipantTypesSchemeInfo {
  private readonly name: string = 'FmParticipantTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Participant: FmParticipantTypes = new FmParticipantTypes(0, '$FmParticipantTypes_Participant');
  readonly Moderator: FmParticipantTypes = new FmParticipantTypes(1, '$FmParticipantTypes_Moderator');
  readonly SuperModerator: FmParticipantTypes = new FmParticipantTypes(2, 'SuperModerator');
  readonly ParticipantFromRole: FmParticipantTypes = new FmParticipantTypes(3, '$FmParticipantTypes_ParticipantFromRole');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region FmParticipantTypes Enumeration

class FmParticipantTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region FmTopicParticipantRoles

/**
 * ID: {ecd6e90e-3bbe-4c24-975b-6644b20efe7f}
 * Alias: FmTopicParticipantRoles
 * Group: Fm
 * Description: Таблица с ролями - учасниками
 */
class FmTopicParticipantRolesSchemeInfo {
  private readonly name: string = 'FmTopicParticipantRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TopicRowID: string = 'TopicRowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly ReadOnly: string = 'ReadOnly';
  readonly Subscribed: string = 'Subscribed';
  readonly InvitingUserID: string = 'InvitingUserID';
  readonly InvitingUserName: string = 'InvitingUserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FmTopicParticipantRolesUnsubscribed

/**
 * ID: {e9fd155c-b189-4a5d-b0b4-970c94a2fa0a}
 * Alias: FmTopicParticipantRolesUnsubscribed
 * Group: Fm
 * Description: Таблица в которой хранятся данные по одписакам пользователями в ролях
 */
class FmTopicParticipantRolesUnsubscribedSchemeInfo {
  private readonly name: string = 'FmTopicParticipantRolesUnsubscribed';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TopicRowID: string = 'TopicRowID';
  readonly UserID: string = 'UserID';
  readonly Subscribe: string = 'Subscribe';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FmTopicParticipants

/**
 * ID: {b8150fdd-b439-4eaa-9665-9a8b9ee774f0}
 * Alias: FmTopicParticipants
 * Group: Fm
 * Description: Участники топика
 */
class FmTopicParticipantsSchemeInfo {
  private readonly name: string = 'FmTopicParticipants';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TopicRowID: string = 'TopicRowID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly ReadOnly: string = 'ReadOnly';
  readonly Subscribed: string = 'Subscribed';
  readonly TypeID: string = 'TypeID';
  readonly InvitingUserID: string = 'InvitingUserID';
  readonly InvitingUserName: string = 'InvitingUserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FmTopics

/**
 * ID: {35b11a3c-f9ec-4fac-a3f1-def11bba44ae}
 * Alias: FmTopics
 * Group: Fm
 * Description: Таблица для хранения топиков
 */
class FmTopicsSchemeInfo {
  private readonly name: string = 'FmTopics';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Description: string = 'Description';
  readonly Title: string = 'Title';
  readonly Created: string = 'Created';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly IsArchived: string = 'IsArchived';
  readonly LastMessageTime: string = 'LastMessageTime';
  readonly LastMessageAuthorID: string = 'LastMessageAuthorID';
  readonly LastMessageAuthorName: string = 'LastMessageAuthorName';
  readonly TypeID: string = 'TypeID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FmTopicTypes

/**
 * ID: {c0645587-3584-4b23-867f-54071abfa5a1}
 * Alias: FmTopicTypes
 * Group: Fm
 * Description: Типы топиков
 */
class FmTopicTypesSchemeInfo {
  private readonly name: string = 'FmTopicTypes';

  //#region Columns

  readonly Name: string = 'Name';
  readonly ID: string = 'ID';

  //#endregion

  //#region Enumeration

  readonly Default: FmTopicTypes = new FmTopicTypes('Default', '680d0d81-d8f3-485e-9058-e17ab9e186e0');
  readonly Private: FmTopicTypes = new FmTopicTypes('Private', 'e7d45adf-90d0-4fcf-9190-e86c92d65897');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region FmTopicTypes Enumeration

class FmTopicTypes {
  readonly Name: string | null;
  readonly ID: guid;

  constructor (Name: string | null, ID: guid) {
    this.Name = Name;
    this.ID = ID;
  }
}

//#endregion

//#endregion

//#region FmUserSettingsVirtual

/**
 * ID: {e8fe8b2a-428d-44b6-8328-ee2a7bb4d323}
 * Alias: FmUserSettingsVirtual
 * Group: Fm
 * Description: Виртуальная таблица для формы с настройками
 */
class FmUserSettingsVirtualSchemeInfo {
  private readonly name: string = 'FmUserSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly IsNotShowMsgIndicatorOnStartup: string = 'IsNotShowMsgIndicatorOnStartup';
  readonly EnableMessageIndicator: string = 'EnableMessageIndicator';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FmUserStat

/**
 * ID: {d10d18eb-d803-4151-8a60-8bfd262d2800}
 * Alias: FmUserStat
 * Group: Fm
 * Description: Таблица, в который храним дату посещения пользователем топика
 */
class FmUserStatSchemeInfo {
  private readonly name: string = 'FmUserStat';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TopicRowID: string = 'TopicRowID';
  readonly UserID: string = 'UserID';
  readonly LastReadMessageTime: string = 'LastReadMessageTime';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ForegroundColors

/**
 * ID: {f22e70d5-17da-4e6a-8d41-17796e5f75d0}
 * Alias: ForegroundColors
 * Group: System
 */
class ForegroundColorsSchemeInfo {
  private readonly name: string = 'ForegroundColors';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Color1: string = 'Color1';
  readonly Color2: string = 'Color2';
  readonly Color3: string = 'Color3';
  readonly Color4: string = 'Color4';
  readonly Color5: string = 'Color5';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FormatSettings

/**
 * ID: {a96047e7-3b08-42bd-8455-1032520a608f}
 * Alias: FormatSettings
 * Group: System
 * Description: Секция карточки с настройками форматирования.
 */
class FormatSettingsSchemeInfo {
  private readonly name: string = 'FormatSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';
  readonly DateFormatID: string = 'DateFormatID';
  readonly DateFormatCaption: string = 'DateFormatCaption';
  readonly DateSeparator: string = 'DateSeparator';
  readonly DaysWithLeadingZero: string = 'DaysWithLeadingZero';
  readonly MonthsWithLeadingZero: string = 'MonthsWithLeadingZero';
  readonly HoursWithLeadingZero: string = 'HoursWithLeadingZero';
  readonly Time24Hour: string = 'Time24Hour';
  readonly TimeSeparator: string = 'TimeSeparator';
  readonly TimeAmDesignator: string = 'TimeAmDesignator';
  readonly TimePmDesignator: string = 'TimePmDesignator';
  readonly NumberGroupSeparator: string = 'NumberGroupSeparator';
  readonly NumberDecimalSeparator: string = 'NumberDecimalSeparator';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region FunctionRoles

/**
 * ID: {a59078ce-8acf-4c45-a49a-503fa88a0580}
 * Alias: FunctionRoles
 * Group: System
 * Description: Функциональные роли заданий, такие как "автор", "исполнитель", "контролёр" и др.
 */
class FunctionRolesSchemeInfo {
  private readonly name: string = 'FunctionRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';
  readonly CanBeDeputy: string = 'CanBeDeputy';
  readonly CanTakeInProgress: string = 'CanTakeInProgress';
  readonly HideTaskByDefault: string = 'HideTaskByDefault';
  readonly CanChangeTaskInfo: string = 'CanChangeTaskInfo';
  readonly CanChangeTaskRoles: string = 'CanChangeTaskRoles';

  //#endregion

  //#region Enumeration

  readonly Author: FunctionRoles = new FunctionRoles('6bc228a0-e5a2-4f15-bf6d-c8e744533241', 'Author', '$Enum_FunctionRoles_Author', true, false, true, true, false);
  readonly Performer: FunctionRoles = new FunctionRoles('f726ab6c-a279-4d79-863a-47253e55ccc1', 'Performer', '$Enum_FunctionRoles_Performer', true, true, false, false, false);
  readonly Sender: FunctionRoles = new FunctionRoles('d75c4fb4-50b9-4f9e-8651-eb6c9de8a847', 'Sender', '$Enum_FunctionRoles_Sender', true, false, true, true, false);

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region FunctionRoles Enumeration

class FunctionRoles {
  readonly ID: guid;
  readonly Name: string | null;
  readonly Caption: string | null;
  readonly CanBeDeputy: boolean;
  readonly CanTakeInProgress: boolean;
  readonly HideTaskByDefault: boolean;
  readonly CanChangeTaskInfo: boolean;
  readonly CanChangeTaskRoles: boolean;

  constructor (ID: guid, Name: string | null, Caption: string | null, CanBeDeputy: boolean, CanTakeInProgress: boolean, HideTaskByDefault: boolean, CanChangeTaskInfo: boolean, CanChangeTaskRoles: boolean) {
    this.ID = ID;
    this.Name = Name;
    this.Caption = Caption;
    this.CanBeDeputy = CanBeDeputy;
    this.CanTakeInProgress = CanTakeInProgress;
    this.HideTaskByDefault = HideTaskByDefault;
    this.CanChangeTaskInfo = CanChangeTaskInfo;
    this.CanChangeTaskRoles = CanChangeTaskRoles;
  }
}

//#endregion

//#endregion

//#region FunctionRolesVirtual

/**
 * ID: {ef4bbb91-4d48-4c68-9e05-34ab4d5c2b36}
 * Alias: FunctionRolesVirtual
 * Group: System
 * Description: Виртуальная карточка для функциональной роли.
 */
class FunctionRolesVirtualSchemeInfo {
  private readonly name: string = 'FunctionRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly FunctionRoleID: string = 'FunctionRoleID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';
  readonly CanBeDeputy: string = 'CanBeDeputy';
  readonly PartitionID: string = 'PartitionID';
  readonly PartitionName: string = 'PartitionName';
  readonly CanTakeInProgress: string = 'CanTakeInProgress';
  readonly HideTaskByDefault: string = 'HideTaskByDefault';
  readonly CanChangeTaskInfo: string = 'CanChangeTaskInfo';
  readonly CanChangeTaskRoles: string = 'CanChangeTaskRoles';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Functions

/**
 * ID: {57e45ca3-5036-4268-b8f9-86c4933a4d2d}
 * Alias: Functions
 * Group: System
 * Description: Contains metadata that describes functions which used by Tessa
 */
class FunctionsSchemeInfo {
  private readonly name: string = 'Functions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Definition: string = 'Definition';

  //#endregion

  //#region Functions

  readonly CalendarAddWorkingDaysToDate = 'CalendarAddWorkingDaysToDate';
  readonly CalendarAddWorkingDaysToDateExact = 'CalendarAddWorkingDaysToDateExact';
  readonly CalendarAddWorkQuants = 'CalendarAddWorkQuants';
  readonly CalendarGetDateDiff = 'CalendarGetDateDiff';
  readonly CalendarGetDayOfWeek = 'CalendarGetDayOfWeek';
  readonly CalendarGetFirstQuantStart = 'CalendarGetFirstQuantStart';
  readonly CalendarGetLastQuantEnd = 'CalendarGetLastQuantEnd';
  readonly CalendarGetPlannedByWorkingDays = 'CalendarGetPlannedByWorkingDays';
  readonly CalendarIsWorkTime = 'CalendarIsWorkTime';
  readonly DropFunction = 'DropFunction';
  readonly FormatAmount = 'FormatAmount';
  readonly GetAggregateRoleUsers = 'GetAggregateRoleUsers';
  readonly GetString = 'GetString';
  readonly GetTimeIntervalLiteral = 'GetTimeIntervalLiteral';
  readonly Localization = 'Localization';
  readonly Localize = 'Localize';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region HelpSections

/**
 * ID: {741301fd-f38a-4cca-bab9-df1328d53b53}
 * Alias: HelpSections
 * Group: System
 * Description: Разделы справки.
 */
class HelpSectionsSchemeInfo {
  private readonly name: string = 'HelpSections';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Code: string = 'Code';
  readonly Name: string = 'Name';
  readonly RichText: string = 'RichText';
  readonly PlainText: string = 'PlainText';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region IncomingRefDocs

/**
 * ID: {83785076-d844-4ea4-9e84-0a389c951ef4}
 * Alias: IncomingRefDocs
 * Group: Common
 */
class IncomingRefDocsSchemeInfo {
  private readonly name: string = 'IncomingRefDocs';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly DocID: string = 'DocID';
  readonly DocDescription: string = 'DocDescription';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Instances

/**
 * ID: {1074eadd-21d7-4925-98c8-40d1e5f0ca0e}
 * Alias: Instances
 * Group: System
 * Description: Contains system info of cards
 */
class InstancesSchemeInfo {
  private readonly name: string = 'Instances';

  //#region Columns

  readonly ID: string = 'ID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly Created: string = 'Created';
  readonly CreatedByID: string = 'CreatedByID';
  readonly CreatedByName: string = 'CreatedByName';
  readonly Modified: string = 'Modified';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';
  readonly Version: string = 'Version';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region InstanceTypes

/**
 * ID: {2a567cee-1489-4a90-acf5-4f6d2c5bd67e}
 * Alias: InstanceTypes
 * Group: System
 * Description: Instance types.
 */
class InstanceTypesSchemeInfo {
  private readonly name: string = 'InstanceTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Card: InstanceTypes = new InstanceTypes(0, 'Card');
  readonly File: InstanceTypes = new InstanceTypes(1, 'File');
  readonly Task: InstanceTypes = new InstanceTypes(2, 'Task');
  readonly Dialog: InstanceTypes = new InstanceTypes(3, 'Dialog');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region InstanceTypes Enumeration

class InstanceTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrAcquaintanceAction

/**
 * ID: {2d90b630-c611-4137-8094-18986416c7b9}
 * Alias: KrAcquaintanceAction
 * Group: KrWe
 * Description: Основная секция для действия "Ознакомление"
 */
class KrAcquaintanceActionSchemeInfo {
  private readonly name: string = 'KrAcquaintanceAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly Comment: string = 'Comment';
  readonly AliasMetadata: string = 'AliasMetadata';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly SenderID: string = 'SenderID';
  readonly SenderName: string = 'SenderName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAcquaintanceActionRoles

/**
 * ID: {4c90a850-8ea9-4b07-8c8e-96145f624a3a}
 * Alias: KrAcquaintanceActionRoles
 * Group: KrWe
 * Description: Список ролей для действия "Ознакомление"
 */
class KrAcquaintanceActionRolesSchemeInfo {
  private readonly name: string = 'KrAcquaintanceActionRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAcquaintanceSettingsVirtual

/**
 * ID: {61a4ec06-f583-4eaf-8d91-c73de9f61164}
 * Alias: KrAcquaintanceSettingsVirtual
 * Group: KrStageTypes
 * Description: Секция настроек этапа Ознакомление
 */
class KrAcquaintanceSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrAcquaintanceSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly Comment: string = 'Comment';
  readonly AliasMetadata: string = 'AliasMetadata';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly SenderID: string = 'SenderID';
  readonly SenderName: string = 'SenderName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrActionTypes

/**
 * ID: {b401e639-9167-4ada-9d46-4982bcd92488}
 * Alias: KrActionTypes
 * Group: Kr
 */
class KrActionTypesSchemeInfo {
  private readonly name: string = 'KrActionTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly EventType: string = 'EventType';

  //#endregion

  //#region Enumeration

  readonly NewCard: KrActionTypes = new KrActionTypes(0, '$KrAction_NewCard', 'NewCard');
  readonly BeforeStoreCard: KrActionTypes = new KrActionTypes(1, '$KrAction_BeforeStoreCard', 'BeforeStoreCard');
  readonly StoreCard: KrActionTypes = new KrActionTypes(2, '$KrAction_StoreCard', 'StoreCard');
  readonly BeforeCompleteTask: KrActionTypes = new KrActionTypes(3, '$KrAction_BeforeCompleteTask', 'BeforeCompleteTask');
  readonly CompleteTask: KrActionTypes = new KrActionTypes(4, '$KrAction_CompleteTask', 'CompleteTask');
  readonly BeforeNewTask: KrActionTypes = new KrActionTypes(5, '$KrAction_BeforeNewTask', 'BeforeNewTask');
  readonly NewTask: KrActionTypes = new KrActionTypes(6, '$KrAction_NewTask', 'NewTask');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrActionTypes Enumeration

class KrActionTypes {
  readonly ID: number;
  readonly Name: string | null;
  readonly EventType: string | null;

  constructor (ID: number, Name: string | null, EventType: string | null) {
    this.ID = ID;
    this.Name = Name;
    this.EventType = EventType;
  }
}

//#endregion

//#endregion

//#region KrActiveTasks

/**
 * ID: {c98ce2bb-a770-4e13-a1b6-314ba68f9bfc}
 * Alias: KrActiveTasks
 * Group: Kr
 * Description: Активные задания процесса согласования
 */
class KrActiveTasksSchemeInfo {
  private readonly name: string = 'KrActiveTasks';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TaskID: string = 'TaskID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrActiveTasksVirtual

/**
 * ID: {21dbb01c-1510-4318-b47d-c2be3197cdfb}
 * Alias: KrActiveTasksVirtual
 * Group: Kr
 * Description: Активные задания процесса согласования
 */
class KrActiveTasksVirtualSchemeInfo {
  private readonly name: string = 'KrActiveTasksVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TaskID: string = 'TaskID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAddFromTemplateSettingsVirtual

/**
 * ID: {b31d2f6f-7980-4686-8029-3abd969ee11b}
 * Alias: KrAddFromTemplateSettingsVirtual
 * Group: KrStageTypes
 */
class KrAddFromTemplateSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrAddFromTemplateSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly FileTemplateID: string = 'FileTemplateID';
  readonly FileTemplateName: string = 'FileTemplateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAdditionalApproval

/**
 * ID: {476425ca-8284-4c41-b11b-dd215042ee6a}
 * Alias: KrAdditionalApproval
 * Group: KrStageTypes
 */
class KrAdditionalApprovalSchemeInfo {
  private readonly name: string = 'KrAdditionalApproval';

  //#region Columns

  readonly ID: string = 'ID';
  readonly TimeLimitation: string = 'TimeLimitation';
  readonly FirstIsResponsible: string = 'FirstIsResponsible';
  readonly Comment: string = 'Comment';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAdditionalApprovalInfo

/**
 * ID: {5f83de75-7485-4785-9528-06ca0e41c5ba}
 * Alias: KrAdditionalApprovalInfo
 * Group: KrStageTypes
 */
class KrAdditionalApprovalInfoSchemeInfo {
  private readonly name: string = 'KrAdditionalApprovalInfo';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly PerformerID: string = 'PerformerID';
  readonly PerformerName: string = 'PerformerName';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';
  readonly Comment: string = 'Comment';
  readonly Answer: string = 'Answer';
  readonly Created: string = 'Created';
  readonly InProgress: string = 'InProgress';
  readonly Planned: string = 'Planned';
  readonly Completed: string = 'Completed';
  readonly IsResponsible: string = 'IsResponsible';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAdditionalApprovalInfoUsersCardVirtual

/**
 * ID: {fed14580-062d-4f30-a344-23c8d2a427d4}
 * Alias: KrAdditionalApprovalInfoUsersCardVirtual
 * Group: KrStageTypes
 * Description: Табличка для контрола с доп. согласантами на вкладке настроек этапа
 */
class KrAdditionalApprovalInfoUsersCardVirtualSchemeInfo {
  private readonly name: string = 'KrAdditionalApprovalInfoUsersCardVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly Order: string = 'Order';
  readonly MainApproverRowID: string = 'MainApproverRowID';
  readonly IsResponsible: string = 'IsResponsible';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAdditionalApprovalInfoVirtual

/**
 * ID: {8a5782c7-06df-4c0b-9088-2efa46642e8e}
 * Alias: KrAdditionalApprovalInfoVirtual
 * Group: KrStageTypes
 */
class KrAdditionalApprovalInfoVirtualSchemeInfo {
  private readonly name: string = 'KrAdditionalApprovalInfoVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly PerformerID: string = 'PerformerID';
  readonly PerformerName: string = 'PerformerName';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';
  readonly Comment: string = 'Comment';
  readonly Answer: string = 'Answer';
  readonly Created: string = 'Created';
  readonly InProgress: string = 'InProgress';
  readonly Planned: string = 'Planned';
  readonly Completed: string = 'Completed';
  readonly ColumnComment: string = 'ColumnComment';
  readonly ColumnState: string = 'ColumnState';
  readonly IsResponsible: string = 'IsResponsible';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAdditionalApprovalsRequestedInfoVirtual

/**
 * ID: {c5d3a740-794c-4904-b9b1-e0a697a7dd80}
 * Alias: KrAdditionalApprovalsRequestedInfoVirtual
 * Group: KrStageTypes
 * Description: Запрошенные дополнительные согласования.
 */
class KrAdditionalApprovalsRequestedInfoVirtualSchemeInfo {
  private readonly name: string = 'KrAdditionalApprovalsRequestedInfoVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly PerformerID: string = 'PerformerID';
  readonly PerformerName: string = 'PerformerName';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';
  readonly Comment: string = 'Comment';
  readonly Answer: string = 'Answer';
  readonly Created: string = 'Created';
  readonly InProgress: string = 'InProgress';
  readonly Planned: string = 'Planned';
  readonly Completed: string = 'Completed';
  readonly ColumnComment: string = 'ColumnComment';
  readonly ColumnState: string = 'ColumnState';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAdditionalApprovalTaskInfo

/**
 * ID: {e0361d36-e2fd-48f9-875a-7ba9548932e5}
 * Alias: KrAdditionalApprovalTaskInfo
 * Group: KrStageTypes
 */
class KrAdditionalApprovalTaskInfoSchemeInfo {
  private readonly name: string = 'KrAdditionalApprovalTaskInfo';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Comment: string = 'Comment';
  readonly AuthorRoleID: string = 'AuthorRoleID';
  readonly AuthorRoleName: string = 'AuthorRoleName';
  readonly IsResponsible: string = 'IsResponsible';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAdditionalApprovalUsers

/**
 * ID: {72544086-2776-418a-a867-516ef7aad325}
 * Alias: KrAdditionalApprovalUsers
 * Group: KrStageTypes
 */
class KrAdditionalApprovalUsersSchemeInfo {
  private readonly name: string = 'KrAdditionalApprovalUsers';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAdditionalApprovalUsersCardVirtual

/**
 * ID: {a4c58948-fe22-4e9c-9cfe-5535a4c13990}
 * Alias: KrAdditionalApprovalUsersCardVirtual
 * Group: KrStageTypes
 */
class KrAdditionalApprovalUsersCardVirtualSchemeInfo {
  private readonly name: string = 'KrAdditionalApprovalUsersCardVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly MainApproverRowID: string = 'MainApproverRowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly Order: string = 'Order';
  readonly IsResponsible: string = 'IsResponsible';
  readonly BasedOnTemplateAdditionalApprovalRowID: string = 'BasedOnTemplateAdditionalApprovalRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAmendingActionVirtual

/**
 * ID: {9cf234bf-ca74-46e7-a91a-564526cc1517}
 * Alias: KrAmendingActionVirtual
 * Group: KrWe
 * Description: Параметры действия "Доработка".
 */
class KrAmendingActionVirtualSchemeInfo {
  private readonly name: string = 'KrAmendingActionVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly KindID: string = 'KindID';
  readonly KindCaption: string = 'KindCaption';
  readonly Digest: string = 'Digest';
  readonly Period: string = 'Period';
  readonly Planned: string = 'Planned';
  readonly IsChangeState: string = 'IsChangeState';
  readonly IsIncrementCycle: string = 'IsIncrementCycle';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';
  readonly InitTaskScript: string = 'InitTaskScript';
  readonly Result: string = 'Result';
  readonly CompleteOptionTaskScript: string = 'CompleteOptionTaskScript';
  readonly CompleteOptionNotificationID: string = 'CompleteOptionNotificationID';
  readonly CompleteOptionNotificationName: string = 'CompleteOptionNotificationName';
  readonly CompleteOptionExcludeDeputies: string = 'CompleteOptionExcludeDeputies';
  readonly CompleteOptionExcludeSubscribers: string = 'CompleteOptionExcludeSubscribers';
  readonly CompleteOptionNotificationScript: string = 'CompleteOptionNotificationScript';
  readonly CompleteOptionSendToPerformer: string = 'CompleteOptionSendToPerformer';
  readonly CompleteOptionSendToAuthor: string = 'CompleteOptionSendToAuthor';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalActionAdditionalPerformersDisplayInfoVirtual

/**
 * ID: {f909d7b8-a840-4864-a2de-fd50c4475519}
 * Alias: KrApprovalActionAdditionalPerformersDisplayInfoVirtual
 * Group: KrWe
 * Description: Отображаемые параметры дополнительного согласования для действия "Согласование".
 */
class KrApprovalActionAdditionalPerformersDisplayInfoVirtualSchemeInfo {
  private readonly name: string = 'KrApprovalActionAdditionalPerformersDisplayInfoVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly Order: string = 'Order';
  readonly MainApproverRowID: string = 'MainApproverRowID';
  readonly IsResponsible: string = 'IsResponsible';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalActionAdditionalPerformersSettingsVirtual

/**
 * ID: {d96c77c3-dec7-4332-b427-bf77ad09c546}
 * Alias: KrApprovalActionAdditionalPerformersSettingsVirtual
 * Group: KrWe
 * Description: Параметры дополнительного согласования для действия "Согласование" являющиеся едиными для всех доп. согласующих.
 */
class KrApprovalActionAdditionalPerformersSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrApprovalActionAdditionalPerformersSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly IsAdditionalApprovalFirstResponsible: string = 'IsAdditionalApprovalFirstResponsible';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalActionAdditionalPerformersVirtual

/**
 * ID: {94a86f8e-ff0f-44fd-933b-9c7af3f35a13}
 * Alias: KrApprovalActionAdditionalPerformersVirtual
 * Group: KrWe
 * Description: Параметры дополнительного согласования для действия "Согласование".
 */
class KrApprovalActionAdditionalPerformersVirtualSchemeInfo {
  private readonly name: string = 'KrApprovalActionAdditionalPerformersVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly Order: string = 'Order';
  readonly MainApproverRowID: string = 'MainApproverRowID';
  readonly IsResponsible: string = 'IsResponsible';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalActionNotificationActionRolesVirtual

/**
 * ID: {d299cae6-f32d-48b5-8930-031e78b3a2a1}
 * Alias: KrApprovalActionNotificationActionRolesVirtual
 * Group: KrWe
 * Description: Действие "Согласование". Таблица с ролями на которые отправляется уведомление при завершения действия с отпределённым вариантом завершения.
 */
class KrApprovalActionNotificationActionRolesVirtualSchemeInfo {
  private readonly name: string = 'KrApprovalActionNotificationActionRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly OptionRowID: string = 'OptionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalActionNotificationRolesVirtual

/**
 * ID: {ae419e33-eb19-456c-a319-54da9ace8821}
 * Alias: KrApprovalActionNotificationRolesVirtual
 * Group: KrWe
 * Description: Действие "Согласование". Таблица с обрабатываемыми вариантами завершения задания действия.
 */
class KrApprovalActionNotificationRolesVirtualSchemeInfo {
  private readonly name: string = 'KrApprovalActionNotificationRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly OptionRowID: string = 'OptionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalActionOptionLinksVirtual

/**
 * ID: {fc3ec595-313c-4b5f-aada-07d7d2f34ff2}
 * Alias: KrApprovalActionOptionLinksVirtual
 * Group: KrWe
 * Description: Действие "Согласование". Коллекционная секция объединяющая связи и вырианты завершения.
 */
class KrApprovalActionOptionLinksVirtualSchemeInfo {
  private readonly name: string = 'KrApprovalActionOptionLinksVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly LinkID: string = 'LinkID';
  readonly LinkName: string = 'LinkName';
  readonly LinkCaption: string = 'LinkCaption';
  readonly ActionOptionRowID: string = 'ActionOptionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalActionOptionsActionVirtual

/**
 * ID: {244719bf-4d4a-4df6-b2fe-a00b1bf6d173}
 * Alias: KrApprovalActionOptionsActionVirtual
 * Group: KrWe
 * Description: Действие "Согласование". Коллекционная секция содержащая параметры завершения действия.
 */
class KrApprovalActionOptionsActionVirtualSchemeInfo {
  private readonly name: string = 'KrApprovalActionOptionsActionVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly LinkID: string = 'LinkID';
  readonly ActionOptionID: string = 'ActionOptionID';
  readonly ActionOptionCaption: string = 'ActionOptionCaption';
  readonly Order: string = 'Order';
  readonly Script: string = 'Script';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalActionOptionsVirtual

/**
 * ID: {cea61a5b-0420-41ba-a5f2-e21c21c30f5a}
 * Alias: KrApprovalActionOptionsVirtual
 * Group: KrWe
 * Description: Действие "Согласование". Таблица с обрабатываемыми вариантами завершения задания действия.
 */
class KrApprovalActionOptionsVirtualSchemeInfo {
  private readonly name: string = 'KrApprovalActionOptionsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';
  readonly Script: string = 'Script';
  readonly Order: string = 'Order';
  readonly Result: string = 'Result';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly SendToPerformer: string = 'SendToPerformer';
  readonly SendToAuthor: string = 'SendToAuthor';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';
  readonly TaskTypeID: string = 'TaskTypeID';
  readonly TaskTypeCaption: string = 'TaskTypeCaption';
  readonly TaskTypeName: string = 'TaskTypeName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalActionVirtual

/**
 * ID: {2afee5e8-3582-4c7c-9fcf-1e4fddefe548}
 * Alias: KrApprovalActionVirtual
 * Group: KrWe
 * Description: Параметры действия "Согласование".
 */
class KrApprovalActionVirtualSchemeInfo {
  private readonly name: string = 'KrApprovalActionVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly KindID: string = 'KindID';
  readonly KindCaption: string = 'KindCaption';
  readonly Digest: string = 'Digest';
  readonly Period: string = 'Period';
  readonly Planned: string = 'Planned';
  readonly InitTaskScript: string = 'InitTaskScript';
  readonly Result: string = 'Result';
  readonly IsParallel: string = 'IsParallel';
  readonly ReturnWhenApproved: string = 'ReturnWhenApproved';
  readonly CanEditCard: string = 'CanEditCard';
  readonly CanEditAnyFiles: string = 'CanEditAnyFiles';
  readonly ChangeStateOnStart: string = 'ChangeStateOnStart';
  readonly ChangeStateOnEnd: string = 'ChangeStateOnEnd';
  readonly IsAdvisory: string = 'IsAdvisory';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';
  readonly SqlPerformersScript: string = 'SqlPerformersScript';
  readonly IsDisableAutoApproval: string = 'IsDisableAutoApproval';
  readonly ExpectAllApprovers: string = 'ExpectAllApprovers';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalCommonInfo

/**
 * ID: {410324bf-ce75-4024-a14c-5d78a8ad7588}
 * Alias: KrApprovalCommonInfo
 * Group: Kr
 * Description: Содержит информацию по основному процессу.
 */
class KrApprovalCommonInfoSchemeInfo {
  private readonly name: string = 'KrApprovalCommonInfo';

  //#region Columns

  readonly ID: string = 'ID';
  readonly MainCardID: string = 'MainCardID';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';
  readonly CurrentApprovalStageRowID: string = 'CurrentApprovalStageRowID';
  readonly ApprovedBy: string = 'ApprovedBy';
  readonly DisapprovedBy: string = 'DisapprovedBy';
  readonly AuthorComment: string = 'AuthorComment';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly StateChangedDateTimeUTC: string = 'StateChangedDateTimeUTC';
  readonly Info: string = 'Info';
  readonly CurrentHistoryGroup: string = 'CurrentHistoryGroup';
  readonly NestedWorkflowProcesses: string = 'NestedWorkflowProcesses';
  readonly ProcessOwnerID: string = 'ProcessOwnerID';
  readonly ProcessOwnerName: string = 'ProcessOwnerName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalCommonInfoVirtual

/**
 * ID: {fe5739f6-d64b-45f5-a3a3-75e999f721dd}
 * Alias: KrApprovalCommonInfoVirtual
 * Group: Kr
 */
class KrApprovalCommonInfoVirtualSchemeInfo {
  private readonly name: string = 'KrApprovalCommonInfoVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly MainCardID: string = 'MainCardID';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';
  readonly CurrentApprovalStageRowID: string = 'CurrentApprovalStageRowID';
  readonly ApprovedBy: string = 'ApprovedBy';
  readonly DisapprovedBy: string = 'DisapprovedBy';
  readonly AuthorComment: string = 'AuthorComment';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly StateChangedDateTimeUTC: string = 'StateChangedDateTimeUTC';
  readonly ProcessOwnerID: string = 'ProcessOwnerID';
  readonly ProcessOwnerName: string = 'ProcessOwnerName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalHistory

/**
 * ID: {07d45e20-a501-4e3b-a246-e548a74d0730}
 * Alias: KrApprovalHistory
 * Group: Kr
 * Description: Сопоставление истории заданий с историей согласования (с учетом циклов согласования)
 */
class KrApprovalHistorySchemeInfo {
  private readonly name: string = 'KrApprovalHistory';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Cycle: string = 'Cycle';
  readonly HistoryRecord: string = 'HistoryRecord';
  readonly Advisory: string = 'Advisory';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalHistoryVirtual

/**
 * ID: {64a54b5b-bbd2-49ae-a378-1e8daa88c070}
 * Alias: KrApprovalHistoryVirtual
 * Group: Kr
 * Description: Сопоставление истории заданий с историей согласования (с учетом циклов согласования)
 */
class KrApprovalHistoryVirtualSchemeInfo {
  private readonly name: string = 'KrApprovalHistoryVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Cycle: string = 'Cycle';
  readonly HistoryRecord: string = 'HistoryRecord';
  readonly Advisory: string = 'Advisory';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrApprovalSettingsVirtual

/**
 * ID: {5a48521b-e00c-44b6-995e-8f238e9103ff}
 * Alias: KrApprovalSettingsVirtual
 * Group: KrStageTypes
 */
class KrApprovalSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrApprovalSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly IsParallel: string = 'IsParallel';
  readonly ReturnToAuthor: string = 'ReturnToAuthor';
  readonly ReturnWhenDisapproved: string = 'ReturnWhenDisapproved';
  readonly CanEditCard: string = 'CanEditCard';
  readonly CanEditFiles: string = 'CanEditFiles';
  readonly Comment: string = 'Comment';
  readonly DisableAutoApproval: string = 'DisableAutoApproval';
  readonly FirstIsResponsible: string = 'FirstIsResponsible';
  readonly ChangeStateOnStart: string = 'ChangeStateOnStart';
  readonly ChangeStateOnEnd: string = 'ChangeStateOnEnd';
  readonly Advisory: string = 'Advisory';
  readonly NotReturnEdit: string = 'NotReturnEdit';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAuthorSettingsVirtual

/**
 * ID: {17931d48-fae6-415e-bb76-3ea3a457a2e9}
 * Alias: KrAuthorSettingsVirtual
 * Group: KrStageTypes
 */
class KrAuthorSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrAuthorSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrAutoApproveHistory

/**
 * ID: {dee2be6f-5d24-443f-b468-f6e03a6742b5}
 * Alias: KrAutoApproveHistory
 * Group: System
 */
class KrAutoApproveHistorySchemeInfo {
  private readonly name: string = 'KrAutoApproveHistory';

  //#region Columns

  readonly CardDigest: string = 'CardDigest';
  readonly Date: string = 'Date';
  readonly CardID: string = 'CardID';
  readonly CardTypeID: string = 'CardTypeID';
  readonly CardTypeCaption: string = 'CardTypeCaption';
  readonly ID: string = 'ID';
  readonly UserID: string = 'UserID';
  readonly Comment: string = 'Comment';
  readonly RowNumber: string = 'RowNumber';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrBuildGlobalOutputVirtual

/**
 * ID: {0d23c056-70cc-4b25-9c3b-d6e2a9e48509}
 * Alias: KrBuildGlobalOutputVirtual
 * Group: Kr
 * Description: Секция, используемая для вывода результатов сборки всех объектов подсистемы маршрутов.
 */
class KrBuildGlobalOutputVirtualSchemeInfo {
  private readonly name: string = 'KrBuildGlobalOutputVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ObjectID: string = 'ObjectID';
  readonly ObjectName: string = 'ObjectName';
  readonly ObjectTypeCaption: string = 'ObjectTypeCaption';
  readonly Output: string = 'Output';
  readonly CompilationDateTime: string = 'CompilationDateTime';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrBuildLocalOutputVirtual

/**
 * ID: {255f542f-3469-4c42-928d-7cf2cfedb644}
 * Alias: KrBuildLocalOutputVirtual
 * Group: Kr
 * Description: Секция, используемая для вывода результатов сборки текущего объекта подсистемы маршрутов.
 */
class KrBuildLocalOutputVirtualSchemeInfo {
  private readonly name: string = 'KrBuildLocalOutputVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Output: string = 'Output';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrBuildStates

/**
 * ID: {e12af590-efd5-4890-b1c7-5a7ce83195dd}
 * Alias: KrBuildStates
 * Group: Kr
 * Description: Состояние компиляции объекта.
 */
class KrBuildStatesSchemeInfo {
  private readonly name: string = 'KrBuildStates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly None: KrBuildStates = new KrBuildStates(0, '$Enum_KrBuildStates_None');
  readonly Error: KrBuildStates = new KrBuildStates(1, '$Enum_KrBuildStates_Error');
  readonly Success: KrBuildStates = new KrBuildStates(2, '$Enum_KrBuildStates_Success');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrBuildStates Enumeration

class KrBuildStates {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrCardGeneratorVirtual

/**
 * ID: {1052a0bc-1a02-4fd4-9636-5dacd0acc436}
 * Alias: KrCardGeneratorVirtual
 * Group: Kr
 * Description: Параметры генерации тестовых карточек.
 */
class KrCardGeneratorVirtualSchemeInfo {
  private readonly name: string = 'KrCardGeneratorVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly UserCount: string = 'UserCount';
  readonly PartnerCount: string = 'PartnerCount';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrCardTasksEditorDialogVirtual

/**
 * ID: {41c02d34-dd86-4115-a485-1bf5e32d2074}
 * Alias: KrCardTasksEditorDialogVirtual
 * Group: Kr
 */
class KrCardTasksEditorDialogVirtualSchemeInfo {
  private readonly name: string = 'KrCardTasksEditorDialogVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly KrToken: string = 'KrToken';
  readonly MainCardID: string = 'MainCardID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrCardTypesVirtual

/**
 * ID: {a90baecf-c9ce-4cba-8bb0-150a13666266}
 * Alias: KrCardTypesVirtual
 * Group: Kr
 * Description: Виртуальная таблица для ссылки из KrPermissions
 */
class KrCardTypesVirtualSchemeInfo {
  private readonly name: string = 'KrCardTypesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Caption: string = 'Caption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrChangeStateAction

/**
 * ID: {1afa15c7-ca17-4fa9-bfe5-3ca066814247}
 * Alias: KrChangeStateAction
 * Group: KrWe
 * Description: Секция для действия ВСмена состояния
 */
class KrChangeStateActionSchemeInfo {
  private readonly name: string = 'KrChangeStateAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrChangeStateSettingsVirtual

/**
 * ID: {bc1450c4-0ddd-4efd-9636-f2ec5d013979}
 * Alias: KrChangeStateSettingsVirtual
 * Group: KrStageTypes
 */
class KrChangeStateSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrChangeStateSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrCheckStateTileExtension

/**
 * ID: {15368402-1522-4722-91b7-d27636f3596b}
 * Alias: KrCheckStateTileExtension
 * Group: Kr
 * Description: Расширение функциональности проверки прав доступа на тайлы в WorkflowEngine по состоянию типового решения
 */
class KrCheckStateTileExtensionSchemeInfo {
  private readonly name: string = 'KrCheckStateTileExtension';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrCommentators

/**
 * ID: {42c4f5aa-d0e8-4d26-abb8-a898e736fe35}
 * Alias: KrCommentators
 * Group: KrStageTypes
 */
class KrCommentatorsSchemeInfo {
  private readonly name: string = 'KrCommentators';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly CommentatorID: string = 'CommentatorID';
  readonly CommentatorName: string = 'CommentatorName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrCommentsInfo

/**
 * ID: {b75dc3d2-10c8-4ca9-a63c-2a8f54db5c42}
 * Alias: KrCommentsInfo
 * Group: KrStageTypes
 */
class KrCommentsInfoSchemeInfo {
  private readonly name: string = 'KrCommentsInfo';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Question: string = 'Question';
  readonly Answer: string = 'Answer';
  readonly CommentatorID: string = 'CommentatorID';
  readonly CommentatorName: string = 'CommentatorName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrCommentsInfoVirtual

/**
 * ID: {e490c196-41b3-489b-8425-ee36a0119f64}
 * Alias: KrCommentsInfoVirtual
 * Group: KrStageTypes
 */
class KrCommentsInfoVirtualSchemeInfo {
  private readonly name: string = 'KrCommentsInfoVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly QuestionShort: string = 'QuestionShort';
  readonly AnswerShort: string = 'AnswerShort';
  readonly CommentatorNameShort: string = 'CommentatorNameShort';
  readonly QuestionFull: string = 'QuestionFull';
  readonly AnswerFull: string = 'AnswerFull';
  readonly CommentatorNameFull: string = 'CommentatorNameFull';
  readonly Completed: string = 'Completed';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrCreateCardAction

/**
 * ID: {70e22440-564a-40a9-88a1-f695844a113b}
 * Alias: KrCreateCardAction
 * Group: KrWe
 * Description: Основная секция действия создания карточки по типу или шаблону
 */
class KrCreateCardActionSchemeInfo {
  private readonly name: string = 'KrCreateCardAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly TemplateID: string = 'TemplateID';
  readonly TemplateCaption: string = 'TemplateCaption';
  readonly OpenCard: string = 'OpenCard';
  readonly SetAsMainCard: string = 'SetAsMainCard';
  readonly Script: string = 'Script';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrCreateCardStageSettingsVirtual

/**
 * ID: {644515d1-8e3f-419e-b938-f59c5ec07fae}
 * Alias: KrCreateCardStageSettingsVirtual
 * Group: KrStageTypes
 */
class KrCreateCardStageSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrCreateCardStageSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly TemplateID: string = 'TemplateID';
  readonly TemplateCaption: string = 'TemplateCaption';
  readonly ModeID: string = 'ModeID';
  readonly ModeName: string = 'ModeName';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrCreateCardStageTypeModes

/**
 * ID: {ebf6257e-c0c6-4f84-b913-7a66fc196418}
 * Alias: KrCreateCardStageTypeModes
 * Group: KrStageTypes
 */
class KrCreateCardStageTypeModesSchemeInfo {
  private readonly name: string = 'KrCreateCardStageTypeModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Order: string = 'Order';

  //#endregion

  //#region Enumeration

  readonly OpenMode: KrCreateCardStageTypeModes = new KrCreateCardStageTypeModes(0, '$KrStages_CreateCard_OpenMode', 0);
  readonly StoreAndOpenMode: KrCreateCardStageTypeModes = new KrCreateCardStageTypeModes(1, '$KrStages_CreateCard_StoreAndOpenMode', 2);
  readonly StartProcessMode: KrCreateCardStageTypeModes = new KrCreateCardStageTypeModes(2, '$KrStages_CreateCard_StartProcessMode', 3);
  readonly StartProcessAndOpenMode: KrCreateCardStageTypeModes = new KrCreateCardStageTypeModes(3, '$KrStages_CreateCard_StartProcessAndOpenMode', 4);
  readonly StoreMode: KrCreateCardStageTypeModes = new KrCreateCardStageTypeModes(4, '$KrStages_CreateCard_StoreMode', 1);

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrCreateCardStageTypeModes Enumeration

class KrCreateCardStageTypeModes {
  readonly ID: number;
  readonly Name: string | null;
  readonly Order: number;

  constructor (ID: number, Name: string | null, Order: number) {
    this.ID = ID;
    this.Name = Name;
    this.Order = Order;
  }
}

//#endregion

//#endregion

//#region KrCycleGroupingModes

/**
 * ID: {3e451f29-8808-4398-930e-d5c172c21de7}
 * Alias: KrCycleGroupingModes
 * Group: Kr
 */
class KrCycleGroupingModesSchemeInfo {
  private readonly name: string = 'KrCycleGroupingModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly ShowAllCycleFiles: KrCycleGroupingModes = new KrCycleGroupingModes(0, '$UI_Controls_FilesControl_ShowAllCycleFiles');
  readonly ShowCurrentCycleFilesOnly: KrCycleGroupingModes = new KrCycleGroupingModes(1, '$UI_Controls_FilesControl_ShowCurrentCycleFilesOnly');
  readonly ShowCurrentAndLastCycleFilesOnly: KrCycleGroupingModes = new KrCycleGroupingModes(2, '$UI_Controls_FilesControl_ShowCurrentAndLastCycleFilesOnly');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrCycleGroupingModes Enumeration

class KrCycleGroupingModes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrDepartmentCondition

/**
 * ID: {70427d3c-3df8-4efc-8bf7-8e19efa2c20d}
 * Alias: KrDepartmentCondition
 * Group: Kr
 * Description: Секция для условия для правил уведомлений, проверяющая Подразделение.
 */
class KrDepartmentConditionSchemeInfo {
  private readonly name: string = 'KrDepartmentCondition';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly DepartmentID: string = 'DepartmentID';
  readonly DepartmentName: string = 'DepartmentName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrDepartmentConditionSettings

/**
 * ID: {f753b988-0c00-471b-869d-0ac361af0d83}
 * Alias: KrDepartmentConditionSettings
 * Group: Kr
 * Description: Секция для условия для правил уведомлений, првоеряющая дополнительные настройки подразделения
 */
class KrDepartmentConditionSettingsSchemeInfo {
  private readonly name: string = 'KrDepartmentConditionSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CheckAuthor: string = 'CheckAuthor';
  readonly CheckInitiator: string = 'CheckInitiator';
  readonly CheckCard: string = 'CheckCard';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrDialogButtonSettingsVirtual

/**
 * ID: {0d52e2ff-45ec-449d-bd49-e0b5f666ee65}
 * Alias: KrDialogButtonSettingsVirtual
 * Group: KrStageTypes
 */
class KrDialogButtonSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrDialogButtonSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';
  readonly TypeID: string = 'TypeID';
  readonly TypeName: string = 'TypeName';
  readonly Caption: string = 'Caption';
  readonly Icon: string = 'Icon';
  readonly Cancel: string = 'Cancel';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrDialogStageTypeSettingsVirtual

/**
 * ID: {663dcabe-f9d8-4a52-a235-6c407e683810}
 * Alias: KrDialogStageTypeSettingsVirtual
 * Group: KrStageTypes
 */
class KrDialogStageTypeSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrDialogStageTypeSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly DialogTypeID: string = 'DialogTypeID';
  readonly DialogTypeName: string = 'DialogTypeName';
  readonly DialogTypeCaption: string = 'DialogTypeCaption';
  readonly CardStoreModeID: string = 'CardStoreModeID';
  readonly CardStoreModeName: string = 'CardStoreModeName';
  readonly DialogActionScript: string = 'DialogActionScript';
  readonly ButtonName: string = 'ButtonName';
  readonly DialogName: string = 'DialogName';
  readonly DialogAlias: string = 'DialogAlias';
  readonly OpenModeID: string = 'OpenModeID';
  readonly OpenModeName: string = 'OpenModeName';
  readonly TaskDigest: string = 'TaskDigest';
  readonly DialogCardSavingScript: string = 'DialogCardSavingScript';
  readonly DisplayValue: string = 'DisplayValue';
  readonly KeepFiles: string = 'KeepFiles';
  readonly TemplateID: string = 'TemplateID';
  readonly TemplateCaption: string = 'TemplateCaption';
  readonly IsCloseWithoutConfirmation: string = 'IsCloseWithoutConfirmation';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrDocNumberRegistrationAutoAssignment

/**
 * ID: {b965332c-296b-48e3-b16f-21a0cd8a6a25}
 * Alias: KrDocNumberRegistrationAutoAssignment
 * Group: Kr
 * Description: Перечисление вариантов автоматического выделения номера при регистрации документа
 */
class KrDocNumberRegistrationAutoAssignmentSchemeInfo {
  private readonly name: string = 'KrDocNumberRegistrationAutoAssignment';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Description: string = 'Description';

  //#endregion

  //#region Enumeration

  readonly NotToAssign: KrDocNumberRegistrationAutoAssignment = new KrDocNumberRegistrationAutoAssignment(0, '$Views_KrAutoAssigment_NotToAssign');
  readonly Assign: KrDocNumberRegistrationAutoAssignment = new KrDocNumberRegistrationAutoAssignment(1, '$Views_KrAutoAssigment_Assign');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrDocNumberRegistrationAutoAssignment Enumeration

class KrDocNumberRegistrationAutoAssignment {
  readonly ID: number;
  readonly Description: string | null;

  constructor (ID: number, Description: string | null) {
    this.ID = ID;
    this.Description = Description;
  }
}

//#endregion

//#endregion

//#region KrDocNumberRegularAutoAssignment

/**
 * ID: {83b4c03f-fdb8-4e11-bca4-02177dd4b3dc}
 * Alias: KrDocNumberRegularAutoAssignment
 * Group: Kr
 * Description: Перечисление вариантов автоматического выделения номера для документа
 */
class KrDocNumberRegularAutoAssignmentSchemeInfo {
  private readonly name: string = 'KrDocNumberRegularAutoAssignment';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Description: string = 'Description';

  //#endregion

  //#region Enumeration

  readonly NotToAssign: KrDocNumberRegularAutoAssignment = new KrDocNumberRegularAutoAssignment(0, '$Views_KrAutoAssigment_NotToAssign');
  readonly WhenCreating: KrDocNumberRegularAutoAssignment = new KrDocNumberRegularAutoAssignment(1, '$Views_KrAutoAssigment_WhenCreating');
  readonly WhenSaving: KrDocNumberRegularAutoAssignment = new KrDocNumberRegularAutoAssignment(2, '$Views_KrAutoAssigment_WhenSaving');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrDocNumberRegularAutoAssignment Enumeration

class KrDocNumberRegularAutoAssignment {
  readonly ID: number;
  readonly Description: string | null;

  constructor (ID: number, Description: string | null) {
    this.ID = ID;
    this.Description = Description;
  }
}

//#endregion

//#endregion

//#region KrDocState

/**
 * ID: {47107d7a-3a8c-47f0-b800-2a45da222ff4}
 * Alias: KrDocState
 * Group: Kr
 */
class KrDocStateSchemeInfo {
  private readonly name: string = 'KrDocState';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Draft: KrDocState = new KrDocState(0, '$KrStates_Doc_Draft');
  readonly Active: KrDocState = new KrDocState(1, '$KrStates_Doc_Active');
  readonly Approved: KrDocState = new KrDocState(2, '$KrStates_Doc_Approved');
  readonly Disapproved: KrDocState = new KrDocState(3, '$KrStates_Doc_Disapproved');
  readonly Editing: KrDocState = new KrDocState(4, '$KrStates_Doc_Editing');
  readonly Canceled: KrDocState = new KrDocState(5, '$KrStates_Doc_Canceled');
  readonly Registered: KrDocState = new KrDocState(6, '$KrStates_Doc_Registered');
  readonly Registration: KrDocState = new KrDocState(7, '$KrStates_Doc_Registration');
  readonly Signed: KrDocState = new KrDocState(8, '$KrStates_Doc_Signed');
  readonly Declined: KrDocState = new KrDocState(9, '$KrStates_Doc_Declined');
  readonly Signing: KrDocState = new KrDocState(10, '$KrStates_Doc_Signing');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrDocState Enumeration

class KrDocState {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrDocStateCondition

/**
 * ID: {204bfce7-5a88-4586-90e5-36d69e5b39fa}
 * Alias: KrDocStateCondition
 * Group: Kr
 * Description: Секция для условия для правил уведомлений, првоеряющая состояния.
 */
class KrDocStateConditionSchemeInfo {
  private readonly name: string = 'KrDocStateCondition';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrDocStateVirtual

/**
 * ID: {e4345324-ad03-46ca-a157-5f71742e5816}
 * Alias: KrDocStateVirtual
 * Group: Kr
 * Description: Виртуальная карточка для состояния документа.
 */
class KrDocStateVirtualSchemeInfo {
  private readonly name: string = 'KrDocStateVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly StateID: string = 'StateID';
  readonly Name: string = 'Name';
  readonly PartitionID: string = 'PartitionID';
  readonly PartitionName: string = 'PartitionName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrDocType

/**
 * ID: {78bfc212-cad5-4d1d-8b91-a9c58562b9d5}
 * Alias: KrDocType
 * Group: Kr
 * Description: Тип документа
 */
class KrDocTypeSchemeInfo {
  private readonly name: string = 'KrDocType';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Title: string = 'Title';
  readonly Description: string = 'Description';
  readonly CardTypeID: string = 'CardTypeID';
  readonly CardTypeCaption: string = 'CardTypeCaption';
  readonly CardTypeName: string = 'CardTypeName';
  readonly UseApproving: string = 'UseApproving';
  readonly UseRegistration: string = 'UseRegistration';
  readonly DocNumberRegularSequence: string = 'DocNumberRegularSequence';
  readonly DocNumberRegularFormat: string = 'DocNumberRegularFormat';
  readonly AllowManualRegularDocNumberAssignment: string = 'AllowManualRegularDocNumberAssignment';
  readonly DocNumberRegistrationSequence: string = 'DocNumberRegistrationSequence';
  readonly DocNumberRegistrationFormat: string = 'DocNumberRegistrationFormat';
  readonly AllowManualRegistrationDocNumberAssignment: string = 'AllowManualRegistrationDocNumberAssignment';
  readonly DocNumberRegistrationAutoAssignmentID: string = 'DocNumberRegistrationAutoAssignmentID';
  readonly DocNumberRegistrationAutoAssignmentDescription: string = 'DocNumberRegistrationAutoAssignmentDescription';
  readonly DocNumberRegularAutoAssignmentID: string = 'DocNumberRegularAutoAssignmentID';
  readonly DocNumberRegularAutoAssignmentDescription: string = 'DocNumberRegularAutoAssignmentDescription';
  readonly ReleaseRegularNumberOnFinalDeletion: string = 'ReleaseRegularNumberOnFinalDeletion';
  readonly ReleaseRegistrationNumberOnFinalDeletion: string = 'ReleaseRegistrationNumberOnFinalDeletion';
  readonly UseResolutions: string = 'UseResolutions';
  readonly DisableChildResolutionDateCheck: string = 'DisableChildResolutionDateCheck';
  readonly UseAutoApprove: string = 'UseAutoApprove';
  readonly ExceededDays: string = 'ExceededDays';
  readonly NotifyBefore: string = 'NotifyBefore';
  readonly AutoApproveComment: string = 'AutoApproveComment';
  readonly HideCreationButton: string = 'HideCreationButton';
  readonly HideRouteTab: string = 'HideRouteTab';
  readonly UseForum: string = 'UseForum';
  readonly UseDefaultDiscussionTab: string = 'UseDefaultDiscussionTab';
  readonly UseRoutesInWorkflowEngine: string = 'UseRoutesInWorkflowEngine';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrDocTypeCondition

/**
 * ID: {7a74559a-0729-4dd8-9040-3367367ac673}
 * Alias: KrDocTypeCondition
 * Group: Kr
 * Description: Секция для условия для правил уведомлений, првоеряющая тип документа/карточки.
 */
class KrDocTypeConditionSchemeInfo {
  private readonly name: string = 'KrDocTypeCondition';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly DocTypeID: string = 'DocTypeID';
  readonly DocTypeCaption: string = 'DocTypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrEditSettingsVirtual

/**
 * ID: {ef86e270-047b-4b7c-9c22-dda56e8eef2c}
 * Alias: KrEditSettingsVirtual
 * Group: KrStageTypes
 */
class KrEditSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrEditSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ChangeState: string = 'ChangeState';
  readonly Comment: string = 'Comment';
  readonly IncrementCycle: string = 'IncrementCycle';
  readonly DoNotSkipStage: string = 'DoNotSkipStage';
  readonly ManageStageVisibility: string = 'ManageStageVisibility';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrForkManagementModes

/**
 * ID: {75e444ae-a785-4e30-a6e0-15020a31654d}
 * Alias: KrForkManagementModes
 * Group: KrStageTypes
 */
class KrForkManagementModesSchemeInfo {
  private readonly name: string = 'KrForkManagementModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly AddMode: KrForkManagementModes = new KrForkManagementModes(0, '$KrStages_ForkManagement_AddMode');
  readonly RemoveMode: KrForkManagementModes = new KrForkManagementModes(1, '$KrStages_ForkManagement_RemoveMode');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrForkManagementModes Enumeration

class KrForkManagementModes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrForkManagementSettingsVirtual

/**
 * ID: {c6397b27-d2a4-4b67-9450-7bb19a69fbbf}
 * Alias: KrForkManagementSettingsVirtual
 * Group: KrStageTypes
 */
class KrForkManagementSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrForkManagementSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ModeID: string = 'ModeID';
  readonly ModeName: string = 'ModeName';
  readonly ManagePrimaryProcess: string = 'ManagePrimaryProcess';
  readonly DirectionAfterInterrupt: string = 'DirectionAfterInterrupt';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrForkNestedProcessesSettingsVirtual

/**
 * ID: {e8f3015f-4085-4df8-bafb-4c5b466965c0}
 * Alias: KrForkNestedProcessesSettingsVirtual
 * Group: KrStageTypes
 */
class KrForkNestedProcessesSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrForkNestedProcessesSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly NestedProcessID: string = 'NestedProcessID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrForkSecondaryProcessesSettingsVirtual

/**
 * ID: {08119dad-4504-49f5-8273-a1851cc4a0d0}
 * Alias: KrForkSecondaryProcessesSettingsVirtual
 * Group: KrStageTypes
 */
class KrForkSecondaryProcessesSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrForkSecondaryProcessesSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly SecondaryProcessID: string = 'SecondaryProcessID';
  readonly SecondaryProcessName: string = 'SecondaryProcessName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrForkSettingsVirtual

/**
 * ID: {27d6b3b7-8347-4e3c-982c-437f6c87ab13}
 * Alias: KrForkSettingsVirtual
 * Group: KrStageTypes
 */
class KrForkSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrForkSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AfterEachNestedProcess: string = 'AfterEachNestedProcess';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrHistoryManagementStageSettingsVirtual

/**
 * ID: {e08c3797-0f25-4841-a2a4-37bb0b938f88}
 * Alias: KrHistoryManagementStageSettingsVirtual
 * Group: KrStageTypes
 */
class KrHistoryManagementStageSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrHistoryManagementStageSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly TaskHistoryGroupTypeID: string = 'TaskHistoryGroupTypeID';
  readonly TaskHistoryGroupTypeCaption: string = 'TaskHistoryGroupTypeCaption';
  readonly ParentTaskHistoryGroupTypeID: string = 'ParentTaskHistoryGroupTypeID';
  readonly ParentTaskHistoryGroupTypeCaption: string = 'ParentTaskHistoryGroupTypeCaption';
  readonly NewIteration: string = 'NewIteration';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrInfoForInitiator

/**
 * ID: {22a3da4b-ac30-4a40-a069-3d6ee66079a0}
 * Alias: KrInfoForInitiator
 * Group: Kr
 */
class KrInfoForInitiatorSchemeInfo {
  private readonly name: string = 'KrInfoForInitiator';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ApproverRole: string = 'ApproverRole';
  readonly ApproverUser: string = 'ApproverUser';
  readonly InProgress: string = 'InProgress';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrNotificationOptionalRecipientsVirtual

/**
 * ID: {2bd36c6d-c035-4407-a270-d329fae7ec76}
 * Alias: KrNotificationOptionalRecipientsVirtual
 * Group: KrStageTypes
 * Description: Секция необязательных получателей этапа Уведомление
 */
class KrNotificationOptionalRecipientsVirtualSchemeInfo {
  private readonly name: string = 'KrNotificationOptionalRecipientsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrNotificationSettingVirtual

/**
 * ID: {28204069-f27e-4b4e-b309-5d2f77dbff8e}
 * Alias: KrNotificationSettingVirtual
 * Group: KrStageTypes
 * Description: Секция настроек этапа Уведомление
 */
class KrNotificationSettingVirtualSchemeInfo {
  private readonly name: string = 'KrNotificationSettingVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly EmailModificationScript: string = 'EmailModificationScript';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPartnerCondition

/**
 * ID: {82f81a44-b515-4187-88c9-03a59e086031}
 * Alias: KrPartnerCondition
 * Group: Kr
 * Description: Секция для условия для правил уведомлений, првоеряющая контрагента.
 */
class KrPartnerConditionSchemeInfo {
  private readonly name: string = 'KrPartnerCondition';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly PartnerID: string = 'PartnerID';
  readonly PartnerName: string = 'PartnerName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPerformersVirtual

/**
 * ID: {b47d668e-7bf0-4165-a10c-6fe22ee10882}
 * Alias: KrPerformersVirtual
 * Group: KrStageTypes
 */
class KrPerformersVirtualSchemeInfo {
  private readonly name: string = 'KrPerformersVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly PerformerID: string = 'PerformerID';
  readonly PerformerName: string = 'PerformerName';
  readonly StageRowID: string = 'StageRowID';
  readonly Order: string = 'Order';
  readonly SQLApprover: string = 'SQLApprover';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionAclGenerationRules

/**
 * ID: {04cb0b04-b5c2-477c-ae4a-3d1e19f9530a}
 * Alias: KrPermissionAclGenerationRules
 * Group: Kr
 */
class KrPermissionAclGenerationRulesSchemeInfo {
  private readonly name: string = 'KrPermissionAclGenerationRules';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RuleID: string = 'RuleID';
  readonly RuleName: string = 'RuleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionExtendedCardRuleFields

/**
 * ID: {a40f2a59-e858-499d-a24a-0f18aab6cbd0}
 * Alias: KrPermissionExtendedCardRuleFields
 * Group: Kr
 * Description: Набор полей для расширенных настроек доступа
 */
class KrPermissionExtendedCardRuleFieldsSchemeInfo {
  private readonly name: string = 'KrPermissionExtendedCardRuleFields';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RuleRowID: string = 'RuleRowID';
  readonly FieldID: string = 'FieldID';
  readonly FieldName: string = 'FieldName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionExtendedCardRules

/**
 * ID: {24c7c7fa-0c39-44c5-aa8d-0199ab79606e}
 * Alias: KrPermissionExtendedCardRules
 * Group: Kr
 * Description: Секция с расширенными настройками доступа к карточке
 */
class KrPermissionExtendedCardRulesSchemeInfo {
  private readonly name: string = 'KrPermissionExtendedCardRules';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly SectionID: string = 'SectionID';
  readonly SectionName: string = 'SectionName';
  readonly SectionTypeID: string = 'SectionTypeID';
  readonly AccessSettingID: string = 'AccessSettingID';
  readonly AccessSettingName: string = 'AccessSettingName';
  readonly IsHidden: string = 'IsHidden';
  readonly Order: string = 'Order';
  readonly Mask: string = 'Mask';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionExtendedFileRuleCategories

/**
 * ID: {2a337def-1279-456a-a61a-0232aa082123}
 * Alias: KrPermissionExtendedFileRuleCategories
 * Group: Kr
 * Description: Набор категорий, проверяемых в расширенных правилах доступа к файлам
 */
class KrPermissionExtendedFileRuleCategoriesSchemeInfo {
  private readonly name: string = 'KrPermissionExtendedFileRuleCategories';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RuleRowID: string = 'RuleRowID';
  readonly CategoryID: string = 'CategoryID';
  readonly CategoryName: string = 'CategoryName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionExtendedFileRules

/**
 * ID: {7ca15c10-9fd1-46e9-8769-b0acc0efe118}
 * Alias: KrPermissionExtendedFileRules
 * Group: Kr
 * Description: Секция с расширенными настройками доступа к файлам
 */
class KrPermissionExtendedFileRulesSchemeInfo {
  private readonly name: string = 'KrPermissionExtendedFileRules';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Extensions: string = 'Extensions';
  readonly ReadAccessSettingID: string = 'ReadAccessSettingID';
  readonly ReadAccessSettingName: string = 'ReadAccessSettingName';
  readonly Order: string = 'Order';
  readonly EditAccessSettingID: string = 'EditAccessSettingID';
  readonly EditAccessSettingName: string = 'EditAccessSettingName';
  readonly DeleteAccessSettingID: string = 'DeleteAccessSettingID';
  readonly DeleteAccessSettingName: string = 'DeleteAccessSettingName';
  readonly SignAccessSettingID: string = 'SignAccessSettingID';
  readonly SignAccessSettingName: string = 'SignAccessSettingName';
  readonly FileCheckRuleID: string = 'FileCheckRuleID';
  readonly FileCheckRuleName: string = 'FileCheckRuleName';
  readonly AddAccessSettingID: string = 'AddAccessSettingID';
  readonly AddAccessSettingName: string = 'AddAccessSettingName';
  readonly FileSizeLimit: string = 'FileSizeLimit';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionExtendedMandatoryRuleFields

/**
 * ID: {16588bc2-69cf-4a54-bf16-b0bf9507a315}
 * Alias: KrPermissionExtendedMandatoryRuleFields
 * Group: Kr
 */
class KrPermissionExtendedMandatoryRuleFieldsSchemeInfo {
  private readonly name: string = 'KrPermissionExtendedMandatoryRuleFields';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RuleRowID: string = 'RuleRowID';
  readonly FieldID: string = 'FieldID';
  readonly FieldName: string = 'FieldName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionExtendedMandatoryRuleOptions

/**
 * ID: {ae17320c-ff1b-45fb-9dd4-f9d99c24d824}
 * Alias: KrPermissionExtendedMandatoryRuleOptions
 * Group: Kr
 */
class KrPermissionExtendedMandatoryRuleOptionsSchemeInfo {
  private readonly name: string = 'KrPermissionExtendedMandatoryRuleOptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RuleRowID: string = 'RuleRowID';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionExtendedMandatoryRules

/**
 * ID: {a4b6af05-9147-4335-8bf4-0e7387f77455}
 * Alias: KrPermissionExtendedMandatoryRules
 * Group: Kr
 */
class KrPermissionExtendedMandatoryRulesSchemeInfo {
  private readonly name: string = 'KrPermissionExtendedMandatoryRules';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly SectionID: string = 'SectionID';
  readonly SectionName: string = 'SectionName';
  readonly SectionTypeID: string = 'SectionTypeID';
  readonly ValidationTypeID: string = 'ValidationTypeID';
  readonly ValidationTypeName: string = 'ValidationTypeName';
  readonly Text: string = 'Text';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionExtendedMandatoryRuleTypes

/**
 * ID: {a707b171-d676-45fd-8386-3bd3f20b7a1a}
 * Alias: KrPermissionExtendedMandatoryRuleTypes
 * Group: Kr
 */
class KrPermissionExtendedMandatoryRuleTypesSchemeInfo {
  private readonly name: string = 'KrPermissionExtendedMandatoryRuleTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RuleRowID: string = 'RuleRowID';
  readonly TaskTypeID: string = 'TaskTypeID';
  readonly TaskTypeCaption: string = 'TaskTypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionExtendedTaskRuleFields

/**
 * ID: {c30346b8-91a6-4dcd-8324-254e253f0148}
 * Alias: KrPermissionExtendedTaskRuleFields
 * Group: Kr
 */
class KrPermissionExtendedTaskRuleFieldsSchemeInfo {
  private readonly name: string = 'KrPermissionExtendedTaskRuleFields';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RuleRowID: string = 'RuleRowID';
  readonly FieldID: string = 'FieldID';
  readonly FieldName: string = 'FieldName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionExtendedTaskRules

/**
 * ID: {536f27ed-f1d2-4850-ad9e-eab93f584f1a}
 * Alias: KrPermissionExtendedTaskRules
 * Group: Kr
 * Description: Секция с расширенными настройками доступа к заданиям
 */
class KrPermissionExtendedTaskRulesSchemeInfo {
  private readonly name: string = 'KrPermissionExtendedTaskRules';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly SectionID: string = 'SectionID';
  readonly SectionName: string = 'SectionName';
  readonly SectionTypeID: string = 'SectionTypeID';
  readonly AccessSettingID: string = 'AccessSettingID';
  readonly AccessSettingName: string = 'AccessSettingName';
  readonly Order: string = 'Order';
  readonly IsHidden: string = 'IsHidden';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionExtendedTaskRuleTypes

/**
 * ID: {c7f6a799-0dd6-4389-9122-bafc68f35c9e}
 * Alias: KrPermissionExtendedTaskRuleTypes
 * Group: Kr
 * Description: Секция с типами заданий для расширенных настроек доступа к заданиям
 */
class KrPermissionExtendedTaskRuleTypesSchemeInfo {
  private readonly name: string = 'KrPermissionExtendedTaskRuleTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RuleRowID: string = 'RuleRowID';
  readonly TaskTypeID: string = 'TaskTypeID';
  readonly TaskTypeCaption: string = 'TaskTypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionExtendedVisibilityRules

/**
 * ID: {aa13a164-dc2e-47e2-a415-021b8b5666e9}
 * Alias: KrPermissionExtendedVisibilityRules
 * Group: Kr
 */
class KrPermissionExtendedVisibilityRulesSchemeInfo {
  private readonly name: string = 'KrPermissionExtendedVisibilityRules';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Alias: string = 'Alias';
  readonly ControlTypeID: string = 'ControlTypeID';
  readonly ControlTypeName: string = 'ControlTypeName';
  readonly IsHidden: string = 'IsHidden';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionRoles

/**
 * ID: {79a6e9e0-e52f-456f-871a-00b6895566ec}
 * Alias: KrPermissionRoles
 * Group: Kr
 * Description: Роли, для пользователей которых применяются разрешения из карточки с правами.
 */
class KrPermissionRolesSchemeInfo {
  private readonly name: string = 'KrPermissionRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly IsContext: string = 'IsContext';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionRuleAccessSettings

/**
 * ID: {4c274eda-ab9a-403f-9e5b-0b933283b5a3}
 * Alias: KrPermissionRuleAccessSettings
 * Group: Kr
 * Description: Список настроек доступа для расширенных прав доступа
 */
class KrPermissionRuleAccessSettingsSchemeInfo {
  private readonly name: string = 'KrPermissionRuleAccessSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly AllowEdit: KrPermissionRuleAccessSettings = new KrPermissionRuleAccessSettings(0, '$KrPermissions_AccessSettings_AllowEdit');
  readonly DisallowEdit: KrPermissionRuleAccessSettings = new KrPermissionRuleAccessSettings(1, '$KrPermissions_AccessSettings_DisallowEdit');
  readonly DisallowRowAdding: KrPermissionRuleAccessSettings = new KrPermissionRuleAccessSettings(2, '$KrPermissions_AccessSettings_DisallowRowAdding');
  readonly DisallowRowDeleting: KrPermissionRuleAccessSettings = new KrPermissionRuleAccessSettings(3, '$KrPermissions_AccessSettings_DisallowRowDeleting');
  readonly MaskData: KrPermissionRuleAccessSettings = new KrPermissionRuleAccessSettings(4, '$KrPermissions_AccessSettings_MaskData');
  readonly DisallowRowEdit: KrPermissionRuleAccessSettings = new KrPermissionRuleAccessSettings(5, '$KrPermissions_AccessSettings_DisallowRowEdit');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrPermissionRuleAccessSettings Enumeration

class KrPermissionRuleAccessSettings {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrPermissions

/**
 * ID: {1c7406cb-e445-4d1a-bf00-a1116db39bc6}
 * Alias: KrPermissions
 * Group: Kr
 * Description: Основная секция для карточки настроек разрешений для бизнес-процесса.
 */
class KrPermissionsSchemeInfo {
  private readonly name: string = 'KrPermissions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Caption: string = 'Caption';
  readonly Description: string = 'Description';
  readonly Types: string = 'Types';
  readonly States: string = 'States';
  readonly Roles: string = 'Roles';
  readonly Permissions: string = 'Permissions';
  readonly Conditions: string = 'Conditions';
  readonly CanCreateCard: string = 'CanCreateCard';
  readonly CanReadCard: string = 'CanReadCard';
  readonly CanEditCard: string = 'CanEditCard';
  readonly CanEditFiles: string = 'CanEditFiles';
  readonly CanAddFiles: string = 'CanAddFiles';
  readonly CanEditRoute: string = 'CanEditRoute';
  readonly CanDeleteCard: string = 'CanDeleteCard';
  readonly CanStartProcess: string = 'CanStartProcess';
  readonly CanRejectProcess: string = 'CanRejectProcess';
  readonly CanRebuildProcess: string = 'CanRebuildProcess';
  readonly CanCancelProcess: string = 'CanCancelProcess';
  readonly CanRegisterCard: string = 'CanRegisterCard';
  readonly CanSeeInCatalog: string = 'CanSeeInCatalog';
  readonly CanEditNumber: string = 'CanEditNumber';
  readonly CanCreateResolutions: string = 'CanCreateResolutions';
  readonly CanDeleteFiles: string = 'CanDeleteFiles';
  readonly CanEditOwnFiles: string = 'CanEditOwnFiles';
  readonly CanDeleteOwnFiles: string = 'CanDeleteOwnFiles';
  readonly CanSignFiles: string = 'CanSignFiles';
  readonly CanAddTopics: string = 'CanAddTopics';
  readonly CanSuperModeratorMode: string = 'CanSuperModeratorMode';
  readonly CanSubscribeForNotifications: string = 'CanSubscribeForNotifications';
  readonly IsExtended: string = 'IsExtended';
  readonly IsRequired: string = 'IsRequired';
  readonly IsDisabled: string = 'IsDisabled';
  readonly CanCreateTemplateAndCopy: string = 'CanCreateTemplateAndCopy';
  readonly CanSkipStages: string = 'CanSkipStages';
  readonly CanFullRecalcRoute: string = 'CanFullRecalcRoute';
  readonly CanEditMyMessages: string = 'CanEditMyMessages';
  readonly CanEditAllMessages: string = 'CanEditAllMessages';
  readonly CanModifyAllTaskAssignedRoles: string = 'CanModifyAllTaskAssignedRoles';
  readonly Priority: string = 'Priority';
  readonly CanReadAllTopics: string = 'CanReadAllTopics';
  readonly CanReadAndSendMessageInAllTopics: string = 'CanReadAndSendMessageInAllTopics';
  readonly CanModifyOwnTaskAssignedRoles: string = 'CanModifyOwnTaskAssignedRoles';
  readonly AclGenerationRules: string = 'AclGenerationRules';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionsControlTypes

/**
 * ID: {18ad7847-b0f7-4d74-bc04-d96cbf18eecd}
 * Alias: KrPermissionsControlTypes
 * Group: Kr
 */
class KrPermissionsControlTypesSchemeInfo {
  private readonly name: string = 'KrPermissionsControlTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Tab: KrPermissionsControlTypes = new KrPermissionsControlTypes(0, '$KrPermissions_ControlType_Tab');
  readonly Block: KrPermissionsControlTypes = new KrPermissionsControlTypes(1, '$KrPermissions_ControlType_Block');
  readonly Control: KrPermissionsControlTypes = new KrPermissionsControlTypes(2, '$KrPermissions_ControlType_Control');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrPermissionsControlTypes Enumeration

class KrPermissionsControlTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrPermissionsFileCheckRules

/**
 * ID: {2baf9cf1-a8d5-4e82-bccd-769d7c70e10a}
 * Alias: KrPermissionsFileCheckRules
 * Group: Kr
 * Description: Правила проверки файлов в расширенных настройках доступа к файлам.
 */
class KrPermissionsFileCheckRulesSchemeInfo {
  private readonly name: string = 'KrPermissionsFileCheckRules';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly AllFiles: KrPermissionsFileCheckRules = new KrPermissionsFileCheckRules(0, '$KrPermissions_FileCheckRules_AllFiles');
  readonly FilesOfOtherUsers: KrPermissionsFileCheckRules = new KrPermissionsFileCheckRules(1, '$KrPermissions_FileCheckRules_FilesOfOtherUsers');
  readonly OwnFiles: KrPermissionsFileCheckRules = new KrPermissionsFileCheckRules(2, '$KrPermissions_FileCheckRules_OwnFiles');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrPermissionsFileCheckRules Enumeration

class KrPermissionsFileCheckRules {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrPermissionsFileEditAccessSettings

/**
 * ID: {9247ed2e-109d-4543-b888-1fe9da9479aa}
 * Alias: KrPermissionsFileEditAccessSettings
 * Group: Kr
 * Description: Настройки доступа на изменение файлов в расширенных настройках доступа к файлам.
 */
class KrPermissionsFileEditAccessSettingsSchemeInfo {
  private readonly name: string = 'KrPermissionsFileEditAccessSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Disallowed: KrPermissionsFileEditAccessSettings = new KrPermissionsFileEditAccessSettings(0, '$KrPermissions_FileEditAccessSettings_Disallowed');
  readonly Allowed: KrPermissionsFileEditAccessSettings = new KrPermissionsFileEditAccessSettings(1, '$KrPermissions_FileEditAccessSettings_Allowed');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrPermissionsFileEditAccessSettings Enumeration

class KrPermissionsFileEditAccessSettings {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrPermissionsFileReadAccessSettings

/**
 * ID: {95a74318-2e98-46bd-bced-1890dd1cd017}
 * Alias: KrPermissionsFileReadAccessSettings
 * Group: Kr
 * Description: Настройки доступа на чтение файлов в расширенных настройках доступа к файлам.
 */
class KrPermissionsFileReadAccessSettingsSchemeInfo {
  private readonly name: string = 'KrPermissionsFileReadAccessSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly FileNotAvailable: KrPermissionsFileReadAccessSettings = new KrPermissionsFileReadAccessSettings(0, '$KrPermissions_FileReadAccessSettings_FileNotAvailable');
  readonly ContentNotAvailable: KrPermissionsFileReadAccessSettings = new KrPermissionsFileReadAccessSettings(1, '$KrPermissions_FileReadAccessSettings_ContentNotAvailable');
  readonly OnlyLastVersion: KrPermissionsFileReadAccessSettings = new KrPermissionsFileReadAccessSettings(2, '$KrPermissions_FileReadAccessSettings_OnlyLastVersion');
  readonly OnlyLastAndOwnVersions: KrPermissionsFileReadAccessSettings = new KrPermissionsFileReadAccessSettings(3, '$KrPermissions_FileReadAccessSettings_OnlyLastAndOwnVersions');
  readonly AllVersions: KrPermissionsFileReadAccessSettings = new KrPermissionsFileReadAccessSettings(4, '$KrPermissions_FileReadAccessSettings_AllVersions');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrPermissionsFileReadAccessSettings Enumeration

class KrPermissionsFileReadAccessSettings {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrPermissionsMandatoryValidationTypes

/**
 * ID: {4439a1f6-c747-442b-b315-caae1c934058}
 * Alias: KrPermissionsMandatoryValidationTypes
 * Group: Kr
 * Description: Список типов проверки обязательности
 */
class KrPermissionsMandatoryValidationTypesSchemeInfo {
  private readonly name: string = 'KrPermissionsMandatoryValidationTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Always: KrPermissionsMandatoryValidationTypes = new KrPermissionsMandatoryValidationTypes(0, '$KrPermissions_MandatoryValidationType_Always');
  readonly OnTaskCompletion: KrPermissionsMandatoryValidationTypes = new KrPermissionsMandatoryValidationTypes(1, '$KrPermissions_MandatoryValidationType_OnTaskCompletion');
  readonly WhenOneFieldFilled: KrPermissionsMandatoryValidationTypes = new KrPermissionsMandatoryValidationTypes(2, '$KrPermissions_MandatoryValidationType_WhenOneFieldFilled');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrPermissionsMandatoryValidationTypes Enumeration

class KrPermissionsMandatoryValidationTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrPermissionsSystem

/**
 * ID: {937fdcfd-c412-4b5d-a319-c11684ea009a}
 * Alias: KrPermissionsSystem
 * Group: Kr
 * Description: Системная таблица для правил доступа.
 */
class KrPermissionsSystemSchemeInfo {
  private readonly name: string = 'KrPermissionsSystem';

  //#region Columns

  readonly Version: string = 'Version';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionStates

/**
 * ID: {5024c846-07bb-4932-bb8d-6c6f9c1e27f7}
 * Alias: KrPermissionStates
 * Group: Kr
 * Description: Состояния согласуемой карточки, к которым применяются права из карточки с правами.
 */
class KrPermissionStatesSchemeInfo {
  private readonly name: string = 'KrPermissionStates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrPermissionTypes

/**
 * ID: {51c5a6be-fe5d-411c-95ba-21d503ced67a}
 * Alias: KrPermissionTypes
 * Group: Kr
 * Description: Типы карточек, к которым применяются разрешения из карточки с правами.
 */
class KrPermissionTypesSchemeInfo {
  private readonly name: string = 'KrPermissionTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly TypeIsDocType: string = 'TypeIsDocType';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrProcessManagementStageSettingsVirtual

/**
 * ID: {65b430e7-42f5-44c0-9d36-d31756c9941a}
 * Alias: KrProcessManagementStageSettingsVirtual
 * Group: KrStageTypes
 */
class KrProcessManagementStageSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrProcessManagementStageSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly StageGroupID: string = 'StageGroupID';
  readonly StageGroupName: string = 'StageGroupName';
  readonly StageRowID: string = 'StageRowID';
  readonly StageName: string = 'StageName';
  readonly StageRowGroupName: string = 'StageRowGroupName';
  readonly ManagePrimaryProcess: string = 'ManagePrimaryProcess';
  readonly ModeID: string = 'ModeID';
  readonly ModeName: string = 'ModeName';
  readonly Signal: string = 'Signal';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrProcessManagementStageTypeModes

/**
 * ID: {778c5e62-6064-447e-92ac-68913d6a42cd}
 * Alias: KrProcessManagementStageTypeModes
 * Group: KrStageTypes
 */
class KrProcessManagementStageTypeModesSchemeInfo {
  private readonly name: string = 'KrProcessManagementStageTypeModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly StageMode: KrProcessManagementStageTypeModes = new KrProcessManagementStageTypeModes(0, '$KrStages_ProcessManagement_StageMode');
  readonly GroupMode: KrProcessManagementStageTypeModes = new KrProcessManagementStageTypeModes(1, '$KrStages_ProcessManagement_GroupMode');
  readonly NextGroupMode: KrProcessManagementStageTypeModes = new KrProcessManagementStageTypeModes(2, '$KrStages_ProcessManagement_NextGroupMode');
  readonly PrevGroupMode: KrProcessManagementStageTypeModes = new KrProcessManagementStageTypeModes(3, '$KrStages_ProcessManagement_PrevGroupMode');
  readonly CurrentGroupMode: KrProcessManagementStageTypeModes = new KrProcessManagementStageTypeModes(4, '$KrStages_ProcessManagement_CurrentGroupMode');
  readonly SignalMode: KrProcessManagementStageTypeModes = new KrProcessManagementStageTypeModes(5, '$KrStages_ProcessManagement_SignalMode');
  readonly CancelProcessMode: KrProcessManagementStageTypeModes = new KrProcessManagementStageTypeModes(6, '$KrStages_ProcessManagement_CancelProcessMode');
  readonly SkipProcessMode: KrProcessManagementStageTypeModes = new KrProcessManagementStageTypeModes(7, '$KrStages_ProcessManagement_SkipProcessMode');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrProcessManagementStageTypeModes Enumeration

class KrProcessManagementStageTypeModes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrProcessStageTypes

/**
 * ID: {7454f645-850f-4e9b-8c80-1f129c5cb1c4}
 * Alias: KrProcessStageTypes
 * Group: KrStageTypes
 */
class KrProcessStageTypesSchemeInfo {
  private readonly name: string = 'KrProcessStageTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Caption: string = 'Caption';
  readonly DefaultStageName: string = 'DefaultStageName';

  //#endregion

  //#region Enumeration

  readonly Approval: KrProcessStageTypes = new KrProcessStageTypes('185610e1-6ab0-064e-9429-4c529804dfe4', '$KrStages_Approval', null);
  readonly Edit: KrProcessStageTypes = new KrProcessStageTypes('4bf667bf-1a82-4e3f-9ef0-44b3b56fb98d', '$KrStages_Edit', '$KrStages_Edit');
  readonly ChangeState: KrProcessStageTypes = new KrProcessStageTypes('c8a9a721-ba8e-45cd-a049-c24d4bdf76cb', '$KrStages_ChangeState', '$KrStages_ChangeState');
  readonly PartialRecalc: KrProcessStageTypes = new KrProcessStageTypes('42cef425-1180-4ccc-88d9-50fdc1ea3982', '$KrStages_PartialRecalc', '$KrStages_PartialRecalc');
  readonly ProcessManagement: KrProcessStageTypes = new KrProcessStageTypes('c7bc176c-8779-46bd-9604-ec847140bd52', '$KrStages_ProcessManagement', '$KrStages_ProcessManagement');
  readonly Resolution: KrProcessStageTypes = new KrProcessStageTypes('6e6f6b28-97af-4ffe-b6f1-b1d8371cb3fa', '$KrStages_Resolution', '$KrStages_Resolution');
  readonly CreateCard: KrProcessStageTypes = new KrProcessStageTypes('9e85f310-226c-4273-804c-52c95b3bac8e', '$KrStages_CreateCard', '$KrStages_CreateCard');
  readonly Registration: KrProcessStageTypes = new KrProcessStageTypes('b468700e-6535-440d-a107-8945ed927429', '$KrStages_Registration', '$KrStages_Registration');
  readonly Deregistration: KrProcessStageTypes = new KrProcessStageTypes('9e6eee69-fbee-4be6-b0e2-9a1b5f8f63eb', '$KrStages_Deregistration', '$KrStages_Deregistration');
  readonly Signing: KrProcessStageTypes = new KrProcessStageTypes('d4670257-6028-4bbc-9cd6-ce163f36ea35', '$KrStages_Signing', null);
  readonly UniversalTask: KrProcessStageTypes = new KrProcessStageTypes('c3acbff6-707f-477c-99c9-d15fc241fc78', '$KrStages_UniversalTask', '$KrStages_UniversalTask');
  readonly Script: KrProcessStageTypes = new KrProcessStageTypes('c02d9a43-ad2a-475a-9188-8fc600b64ee8', '$KrStages_Script', '$KrStages_Script');
  readonly Notification: KrProcessStageTypes = new KrProcessStageTypes('19c7a9b3-6ae7-4072-b9ac-1753245ec0ac', '$KrStages_Notification', '$KrStages_Notification');
  readonly Acquaintance: KrProcessStageTypes = new KrProcessStageTypes('c2e0e75a-de77-42cd-9ff8-e872b9899362', '$KrStages_Acquaintance', '$KrStages_Acquaintance');
  readonly HistoryManagement: KrProcessStageTypes = new KrProcessStageTypes('371937b5-38c6-436a-959b-42fd0ee01611', '$KrStages_HistoryManagement', '$KrStages_HistoryManagement');
  readonly Fork: KrProcessStageTypes = new KrProcessStageTypes('2246da18-bcf9-4a0c-a2b8-f61fbe9bfddb', '$KrStages_Fork', '$KrStages_Fork');
  readonly ForkManagement: KrProcessStageTypes = new KrProcessStageTypes('e1f86f2d-c8d5-4482-ad9f-a023eda4bc48', '$KrStages_ForkManagement', '$KrStages_ForkManagement');
  readonly TypedTask: KrProcessStageTypes = new KrProcessStageTypes('ac7fcf5b-57d9-4a53-9c30-50e74cd3b68d', '$KrStages_TypedTask', '$KrStages_TypedTask');
  readonly AddFromTemplate: KrProcessStageTypes = new KrProcessStageTypes('c80839e2-1766-4e02-b85c-279ea6fd600d', '$KrStages_AddFromTemplate', '$KrStages_AddFromTemplate');
  readonly Dialog: KrProcessStageTypes = new KrProcessStageTypes('be14045d-f10e-4fc3-9b6e-8961ccc43c49', '$KrStages_Dialog', '$KrStages_Dialog');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrProcessStageTypes Enumeration

class KrProcessStageTypes {
  readonly ID: guid;
  readonly Caption: string | null;
  readonly DefaultStageName: string | null;

  constructor (ID: guid, Caption: string | null, DefaultStageName: string | null) {
    this.ID = ID;
    this.Caption = Caption;
    this.DefaultStageName = DefaultStageName;
  }
}

//#endregion

//#endregion

//#region KrRegistrationStageSettingsVirtual

/**
 * ID: {cae44467-e2c1-4638-8444-857575455f80}
 * Alias: KrRegistrationStageSettingsVirtual
 * Group: KrStageTypes
 */
class KrRegistrationStageSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrRegistrationStageSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Comment: string = 'Comment';
  readonly CanEditCard: string = 'CanEditCard';
  readonly CanEditFiles: string = 'CanEditFiles';
  readonly WithoutTask: string = 'WithoutTask';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrRequestComment

/**
 * ID: {db361bb6-d8d1-4645-8d9c-f296ce939c4b}
 * Alias: KrRequestComment
 * Group: Kr
 */
class KrRequestCommentSchemeInfo {
  private readonly name: string = 'KrRequestComment';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Comment: string = 'Comment';
  readonly AuthorRoleID: string = 'AuthorRoleID';
  readonly AuthorRoleName: string = 'AuthorRoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrResolutionActionVirtual

/**
 * ID: {aed41831-bbfd-4637-8e5b-c9b69c9ca7f1}
 * Alias: KrResolutionActionVirtual
 * Group: KrWe
 * Description: Параметры действия "Выполнение задачи".
 */
class KrResolutionActionVirtualSchemeInfo {
  private readonly name: string = 'KrResolutionActionVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly KindID: string = 'KindID';
  readonly KindCaption: string = 'KindCaption';
  readonly Digest: string = 'Digest';
  readonly Period: string = 'Period';
  readonly Planned: string = 'Planned';
  readonly IsMajorPerformer: string = 'IsMajorPerformer';
  readonly IsMassCreation: string = 'IsMassCreation';
  readonly WithControl: string = 'WithControl';
  readonly ControllerID: string = 'ControllerID';
  readonly ControllerName: string = 'ControllerName';
  readonly SqlPerformersScript: string = 'SqlPerformersScript';
  readonly SenderID: string = 'SenderID';
  readonly SenderName: string = 'SenderName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrResolutionSettingsVirtual

/**
 * ID: {5e584567-9e11-4741-ab3a-d96af0b6e0c9}
 * Alias: KrResolutionSettingsVirtual
 * Group: KrStageTypes
 */
class KrResolutionSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrResolutionSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly KindID: string = 'KindID';
  readonly KindCaption: string = 'KindCaption';
  readonly ControllerID: string = 'ControllerID';
  readonly ControllerName: string = 'ControllerName';
  readonly Comment: string = 'Comment';
  readonly Planned: string = 'Planned';
  readonly DurationInDays: string = 'DurationInDays';
  readonly WithControl: string = 'WithControl';
  readonly MassCreation: string = 'MassCreation';
  readonly MajorPerformer: string = 'MajorPerformer';
  readonly SenderID: string = 'SenderID';
  readonly SenderName: string = 'SenderName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrRouteInitializationActionVirtual

/**
 * ID: {bbdef4f8-22b0-4075-83f2-1c6e89d1ba7b}
 * Alias: KrRouteInitializationActionVirtual
 * Group: KrWe
 * Description: Параметры действия "Инициализация маршрута".
 */
class KrRouteInitializationActionVirtualSchemeInfo {
  private readonly name: string = 'KrRouteInitializationActionVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly InitiatorID: string = 'InitiatorID';
  readonly InitiatorName: string = 'InitiatorName';
  readonly InitiatorComment: string = 'InitiatorComment';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrRouteModes

/**
 * ID: {01c6933a-204d-490e-a6db-fc69345c7e32}
 * Alias: KrRouteModes
 * Group: Kr
 * Description: Перечисление режимов работы системы маршрутов.
 */
class KrRouteModesSchemeInfo {
  private readonly name: string = 'KrRouteModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly RoutesNotUsed: KrRouteModes = new KrRouteModes(0, '$KrRoute_Mode_RoutesNotUsed');
  readonly RoutesUsed: KrRouteModes = new KrRouteModes(1, '$KrRoute_Mode_RoutesUsed');
  readonly RoutesUsedProcessActive: KrRouteModes = new KrRouteModes(2, '$KrRoute_Mode_RoutesUsedProcessActive');
  readonly RoutesUsedProcessInactive: KrRouteModes = new KrRouteModes(3, '$KrRoute_Mode_RoutesUsedProcessInactive');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrRouteModes Enumeration

class KrRouteModes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrRouteSettings

/**
 * ID: {87619627-44a5-4f67-af9a-8f5736538f51}
 * Alias: KrRouteSettings
 * Group: Kr
 */
class KrRouteSettingsSchemeInfo {
  private readonly name: string = 'KrRouteSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AllowedRegistration: string = 'AllowedRegistration';
  readonly RouteModeID: string = 'RouteModeID';
  readonly RouteModeName: string = 'RouteModeName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSamplePermissionsExtension

/**
 * ID: {f45e2e3f-5559-4800-a9f7-45276924234b}
 * Alias: KrSamplePermissionsExtension
 * Group: Kr
 * Description: Таблица для примера расширения правил доступа типового решения.
 */
class KrSamplePermissionsExtensionSchemeInfo {
  private readonly name: string = 'KrSamplePermissionsExtension';

  //#region Columns

  readonly ID: string = 'ID';
  readonly MinAmount: string = 'MinAmount';
  readonly MaxAmount: string = 'MaxAmount';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSecondaryProcessCommonInfo

/**
 * ID: {ce71fe9f-6ae4-4f76-8311-7ae54686a474}
 * Alias: KrSecondaryProcessCommonInfo
 * Group: Kr
 * Description: Содержит информацию по вторичному процессу.
 */
class KrSecondaryProcessCommonInfoSchemeInfo {
  private readonly name: string = 'KrSecondaryProcessCommonInfo';

  //#region Columns

  readonly ID: string = 'ID';
  readonly MainCardID: string = 'MainCardID';
  readonly CurrentApprovalStageRowID: string = 'CurrentApprovalStageRowID';
  readonly Info: string = 'Info';
  readonly SecondaryProcessID: string = 'SecondaryProcessID';
  readonly NestedWorkflowProcesses: string = 'NestedWorkflowProcesses';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly ProcessOwnerID: string = 'ProcessOwnerID';
  readonly ProcessOwnerName: string = 'ProcessOwnerName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSecondaryProcesses

/**
 * ID: {caac66aa-0cbb-4e2b-83fd-7c368e814d64}
 * Alias: KrSecondaryProcesses
 * Group: Kr
 */
class KrSecondaryProcessesSchemeInfo {
  private readonly name: string = 'KrSecondaryProcesses';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Description: string = 'Description';
  readonly TileGroup: string = 'TileGroup';
  readonly IsGlobal: string = 'IsGlobal';
  readonly Async: string = 'Async';
  readonly RefreshAndNotify: string = 'RefreshAndNotify';
  readonly Caption: string = 'Caption';
  readonly Tooltip: string = 'Tooltip';
  readonly Icon: string = 'Icon';
  readonly TileSizeID: string = 'TileSizeID';
  readonly TileSizeName: string = 'TileSizeName';
  readonly AskConfirmation: string = 'AskConfirmation';
  readonly ConfirmationMessage: string = 'ConfirmationMessage';
  readonly ActionGrouping: string = 'ActionGrouping';
  readonly VisibilitySqlCondition: string = 'VisibilitySqlCondition';
  readonly ExecutionSqlCondition: string = 'ExecutionSqlCondition';
  readonly VisibilitySourceCondition: string = 'VisibilitySourceCondition';
  readonly ExecutionSourceCondition: string = 'ExecutionSourceCondition';
  readonly ExecutionAccessDeniedMessage: string = 'ExecutionAccessDeniedMessage';
  readonly ModeID: string = 'ModeID';
  readonly ModeName: string = 'ModeName';
  readonly ActionID: string = 'ActionID';
  readonly ActionName: string = 'ActionName';
  readonly ActionEventType: string = 'ActionEventType';
  readonly AllowClientSideLaunch: string = 'AllowClientSideLaunch';
  readonly CheckRecalcRestrictions: string = 'CheckRecalcRestrictions';
  readonly RunOnce: string = 'RunOnce';
  readonly ButtonHotkey: string = 'ButtonHotkey';
  readonly Conditions: string = 'Conditions';
  readonly Order: string = 'Order';
  readonly NotMessageHasNoActiveStages: string = 'NotMessageHasNoActiveStages';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSecondaryProcessGroupsVirtual

/**
 * ID: {ba745c18-badf-4d8c-a26c-46619ba56b6f}
 * Alias: KrSecondaryProcessGroupsVirtual
 * Group: Kr
 */
class KrSecondaryProcessGroupsVirtualSchemeInfo {
  private readonly name: string = 'KrSecondaryProcessGroupsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StageGroupID: string = 'StageGroupID';
  readonly StageGroupName: string = 'StageGroupName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSecondaryProcessModes

/**
 * ID: {a8a8e7df-0237-4fda-824f-030df82a1030}
 * Alias: KrSecondaryProcessModes
 * Group: Kr
 */
class KrSecondaryProcessModesSchemeInfo {
  private readonly name: string = 'KrSecondaryProcessModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly PureProcess: KrSecondaryProcessModes = new KrSecondaryProcessModes(0, '$KrSecondaryProcess_Mode_PureProcess');
  readonly Button: KrSecondaryProcessModes = new KrSecondaryProcessModes(1, '$KrSecondaryProcess_Mode_Button');
  readonly Action: KrSecondaryProcessModes = new KrSecondaryProcessModes(2, '$KrSecondaryProcess_Mode_Action');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrSecondaryProcessModes Enumeration

class KrSecondaryProcessModes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrSecondaryProcessRoles

/**
 * ID: {f7a6f1e2-a4c2-4f26-9c50-bc7e14dfc8ce}
 * Alias: KrSecondaryProcessRoles
 * Group: Kr
 * Description: Содержит роли для которых доступен для выполнения вторичный процесс.
 */
class KrSecondaryProcessRolesSchemeInfo {
  private readonly name: string = 'KrSecondaryProcessRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly IsContext: string = 'IsContext';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettings

/**
 * ID: {4a8403cf-6979-4e21-ad09-6956d681c405}
 * Alias: KrSettings
 * Group: Kr
 */
class KrSettingsSchemeInfo {
  private readonly name: string = 'KrSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AscendingApprovalList: string = 'AscendingApprovalList';
  readonly NotificationsDefaultLanguageID: string = 'NotificationsDefaultLanguageID';
  readonly NotificationsDefaultLanguageCaption: string = 'NotificationsDefaultLanguageCaption';
  readonly NotificationsDefaultLanguageCode: string = 'NotificationsDefaultLanguageCode';
  readonly PermissionsExtensionTypeID: string = 'PermissionsExtensionTypeID';
  readonly PermissionsExtensionTypeName: string = 'PermissionsExtensionTypeName';
  readonly PermissionsExtensionTypeCaption: string = 'PermissionsExtensionTypeCaption';
  readonly HideCommentForApprove: string = 'HideCommentForApprove';
  readonly AllowManualInputAndAutoCreatePartners: string = 'AllowManualInputAndAutoCreatePartners';
  readonly NotificationsDefaultFormatID: string = 'NotificationsDefaultFormatID';
  readonly NotificationsDefaultFormatName: string = 'NotificationsDefaultFormatName';
  readonly NotificationsDefaultFormatCaption: string = 'NotificationsDefaultFormatCaption';
  readonly HideLanguageSelection: string = 'HideLanguageSelection';
  readonly HideFormattingSelection: string = 'HideFormattingSelection';
  readonly AclReadCardAccess: string = 'AclReadCardAccess';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsCardTypes

/**
 * ID: {949c3849-eb4b-4d64-9676-f14f9c40dbcf}
 * Alias: KrSettingsCardTypes
 * Group: Kr
 */
class KrSettingsCardTypesSchemeInfo {
  private readonly name: string = 'KrSettingsCardTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly CardTypeID: string = 'CardTypeID';
  readonly CardTypeCaption: string = 'CardTypeCaption';
  readonly UseDocTypes: string = 'UseDocTypes';
  readonly UseApproving: string = 'UseApproving';
  readonly DocNumberRegularAutoAssignmentID: string = 'DocNumberRegularAutoAssignmentID';
  readonly DocNumberRegularAutoAssignmentDescription: string = 'DocNumberRegularAutoAssignmentDescription';
  readonly DocNumberRegularSequence: string = 'DocNumberRegularSequence';
  readonly DocNumberRegularFormat: string = 'DocNumberRegularFormat';
  readonly AllowManualRegularDocNumberAssignment: string = 'AllowManualRegularDocNumberAssignment';
  readonly DocNumberRegistrationAutoAssignmentID: string = 'DocNumberRegistrationAutoAssignmentID';
  readonly DocNumberRegistrationAutoAssignmentDescription: string = 'DocNumberRegistrationAutoAssignmentDescription';
  readonly DocNumberRegistrationSequence: string = 'DocNumberRegistrationSequence';
  readonly DocNumberRegistrationFormat: string = 'DocNumberRegistrationFormat';
  readonly AllowManualRegistrationDocNumberAssignment: string = 'AllowManualRegistrationDocNumberAssignment';
  readonly UseRegistration: string = 'UseRegistration';
  readonly ReleaseRegularNumberOnFinalDeletion: string = 'ReleaseRegularNumberOnFinalDeletion';
  readonly ReleaseRegistrationNumberOnFinalDeletion: string = 'ReleaseRegistrationNumberOnFinalDeletion';
  readonly UseResolutions: string = 'UseResolutions';
  readonly DisableChildResolutionDateCheck: string = 'DisableChildResolutionDateCheck';
  readonly UseAutoApprove: string = 'UseAutoApprove';
  readonly ExceededDays: string = 'ExceededDays';
  readonly NotifyBefore: string = 'NotifyBefore';
  readonly AutoApproveComment: string = 'AutoApproveComment';
  readonly HideCreationButton: string = 'HideCreationButton';
  readonly HideRouteTab: string = 'HideRouteTab';
  readonly UseForum: string = 'UseForum';
  readonly UseDefaultDiscussionTab: string = 'UseDefaultDiscussionTab';
  readonly UseRoutesInWorkflowEngine: string = 'UseRoutesInWorkflowEngine';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsCycleGrouping

/**
 * ID: {4801bc15-cfaf-455c-aba6-cf77dd72484d}
 * Alias: KrSettingsCycleGrouping
 * Group: Kr
 */
class KrSettingsCycleGroupingSchemeInfo {
  private readonly name: string = 'KrSettingsCycleGrouping';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly TypeIsDocType: string = 'TypeIsDocType';
  readonly TypesRowID: string = 'TypesRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsCycleGroupingStates

/**
 * ID: {11426c91-c7c4-4455-9eda-7ce5fd497982}
 * Alias: KrSettingsCycleGroupingStates
 * Group: Kr
 */
class KrSettingsCycleGroupingStatesSchemeInfo {
  private readonly name: string = 'KrSettingsCycleGroupingStates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';
  readonly TypesRowID: string = 'TypesRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsCycleGroupingTypes

/**
 * ID: {4012de1a-efd8-442d-a25c-8fe78008e38d}
 * Alias: KrSettingsCycleGroupingTypes
 * Group: Kr
 */
class KrSettingsCycleGroupingTypesSchemeInfo {
  private readonly name: string = 'KrSettingsCycleGroupingTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Description: string = 'Description';
  readonly DefaultModeID: string = 'DefaultModeID';
  readonly DefaultModeName: string = 'DefaultModeName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsRouteDocTypes

/**
 * ID: {9568db07-0f34-48ad-bab8-0d5e43d1846b}
 * Alias: KrSettingsRouteDocTypes
 * Group: Kr
 * Description: Разрешения по типам карточек или видам документов в маршрутах.
 */
class KrSettingsRouteDocTypesSchemeInfo {
  private readonly name: string = 'KrSettingsRouteDocTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly CardTypeID: string = 'CardTypeID';
  readonly CardTypeCaption: string = 'CardTypeCaption';
  readonly ParentRowID: string = 'ParentRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsRouteExtraTaskTypes

/**
 * ID: {219b245d-a909-4517-8be6-d22ef7a28dba}
 * Alias: KrSettingsRouteExtraTaskTypes
 * Group: Kr
 */
class KrSettingsRouteExtraTaskTypesSchemeInfo {
  private readonly name: string = 'KrSettingsRouteExtraTaskTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TaskTypeID: string = 'TaskTypeID';
  readonly TaskTypeName: string = 'TaskTypeName';
  readonly TaskTypeCaption: string = 'TaskTypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsRoutePermissions

/**
 * ID: {39e6d38f-4e35-45e9-8c71-42a932dce18c}
 * Alias: KrSettingsRoutePermissions
 * Group: Kr
 * Description: Разрешения в маршрутах - родительская таблица, каждой строкой которой является пересечение остальных таблиц.
 */
class KrSettingsRoutePermissionsSchemeInfo {
  private readonly name: string = 'KrSettingsRoutePermissions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsRouteRoles

/**
 * ID: {0f717b89-050d-4a3f-97fc-4520eed77540}
 * Alias: KrSettingsRouteRoles
 * Group: Kr
 * Description: Разрешения по ролям пользователя в маршрутах.
 */
class KrSettingsRouteRolesSchemeInfo {
  private readonly name: string = 'KrSettingsRouteRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StageRolesID: string = 'StageRolesID';
  readonly StageRolesName: string = 'StageRolesName';
  readonly ParentRowID: string = 'ParentRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsRouteStageGroups

/**
 * ID: {9416f8fb-95b0-4617-98a6-f576580bfd49}
 * Alias: KrSettingsRouteStageGroups
 * Group: Kr
 * Description: Разрешения по группам этапов в маршрутах.
 */
class KrSettingsRouteStageGroupsSchemeInfo {
  private readonly name: string = 'KrSettingsRouteStageGroups';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StageGroupID: string = 'StageGroupID';
  readonly StageGroupName: string = 'StageGroupName';
  readonly ParentRowID: string = 'ParentRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsRouteStageTypes

/**
 * ID: {6681dd3c-cd54-405d-83bb-93ce533198fe}
 * Alias: KrSettingsRouteStageTypes
 * Group: Kr
 * Description: Разрешения по типам доступных этапов в маршрутах.
 */
class KrSettingsRouteStageTypesSchemeInfo {
  private readonly name: string = 'KrSettingsRouteStageTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StageTypeID: string = 'StageTypeID';
  readonly StageTypeCaption: string = 'StageTypeCaption';
  readonly ParentRowID: string = 'ParentRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsTaskAuthor

/**
 * ID: {0748a117-8a2a-4198-a994-15d91094d6b7}
 * Alias: KrSettingsTaskAuthor
 * Group: Kr
 */
class KrSettingsTaskAuthorSchemeInfo {
  private readonly name: string = 'KrSettingsTaskAuthor';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly AuthorsRowID: string = 'AuthorsRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsTaskAuthorReplace

/**
 * ID: {cfe4678d-369f-43a4-b103-32aecd9858a6}
 * Alias: KrSettingsTaskAuthorReplace
 * Group: Kr
 */
class KrSettingsTaskAuthorReplaceSchemeInfo {
  private readonly name: string = 'KrSettingsTaskAuthorReplace';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly AuthorReplaceID: string = 'AuthorReplaceID';
  readonly AuthorReplaceName: string = 'AuthorReplaceName';
  readonly AuthorsRowID: string = 'AuthorsRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSettingsTaskAuthors

/**
 * ID: {afafd0bc-446e-4adf-8332-16be0b3d1908}
 * Alias: KrSettingsTaskAuthors
 * Group: Kr
 */
class KrSettingsTaskAuthorsSchemeInfo {
  private readonly name: string = 'KrSettingsTaskAuthors';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Description: string = 'Description';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSigningActionNotificationActionRolesVirtual

/**
 * ID: {a6311a94-817e-48e0-afb5-6dca269563d1}
 * Alias: KrSigningActionNotificationActionRolesVirtual
 * Group: KrWe
 * Description: Действие "Согласование". Таблица с ролями на которые отправляется уведомление при завершения действия с отпределённым вариантом завершения.
 */
class KrSigningActionNotificationActionRolesVirtualSchemeInfo {
  private readonly name: string = 'KrSigningActionNotificationActionRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly OptionRowID: string = 'OptionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSigningActionNotificationRolesVirtual

/**
 * ID: {7836e13c-4ebf-47f2-8968-504ab0d2fce4}
 * Alias: KrSigningActionNotificationRolesVirtual
 * Group: KrWe
 * Description: Действие "Подписание". Таблица с обрабатываемыми вариантами завершения задания действия.
 */
class KrSigningActionNotificationRolesVirtualSchemeInfo {
  private readonly name: string = 'KrSigningActionNotificationRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly OptionRowID: string = 'OptionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSigningActionOptionLinksVirtual

/**
 * ID: {dd36aad9-d17a-41ad-b854-22f886819d28}
 * Alias: KrSigningActionOptionLinksVirtual
 * Group: KrWe
 * Description: Действие "Подписание". Коллекционная секция объединяющая связи и вырианты завершения.
 */
class KrSigningActionOptionLinksVirtualSchemeInfo {
  private readonly name: string = 'KrSigningActionOptionLinksVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly LinkID: string = 'LinkID';
  readonly LinkName: string = 'LinkName';
  readonly LinkCaption: string = 'LinkCaption';
  readonly ActionOptionRowID: string = 'ActionOptionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSigningActionOptionsActionVirtual

/**
 * ID: {b4c6c410-c5cb-40e3-b800-3cd854c94a2c}
 * Alias: KrSigningActionOptionsActionVirtual
 * Group: KrWe
 * Description: Действие "Подписание". Коллекционная секция содержащая параметры завершения действия.
 */
class KrSigningActionOptionsActionVirtualSchemeInfo {
  private readonly name: string = 'KrSigningActionOptionsActionVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly LinkID: string = 'LinkID';
  readonly ActionOptionID: string = 'ActionOptionID';
  readonly ActionOptionCaption: string = 'ActionOptionCaption';
  readonly Order: string = 'Order';
  readonly Script: string = 'Script';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSigningActionOptionsVirtual

/**
 * ID: {3f87675e-0a60-4ece-a5c9-3c203e2c9ffb}
 * Alias: KrSigningActionOptionsVirtual
 * Group: KrWe
 * Description: Действие "Подписание". Таблица с обрабатываемыми вариантами завершения задания действия.
 */
class KrSigningActionOptionsVirtualSchemeInfo {
  private readonly name: string = 'KrSigningActionOptionsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';
  readonly Script: string = 'Script';
  readonly Order: string = 'Order';
  readonly Result: string = 'Result';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly SendToPerformer: string = 'SendToPerformer';
  readonly SendToAuthor: string = 'SendToAuthor';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';
  readonly TaskTypeID: string = 'TaskTypeID';
  readonly TaskTypeName: string = 'TaskTypeName';
  readonly TaskTypeCaption: string = 'TaskTypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSigningActionVirtual

/**
 * ID: {baaceebe-011d-4f1a-9431-00c4f8b233b9}
 * Alias: KrSigningActionVirtual
 * Group: KrWe
 * Description: Параметры действия "Подписание".
 */
class KrSigningActionVirtualSchemeInfo {
  private readonly name: string = 'KrSigningActionVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly KindID: string = 'KindID';
  readonly KindCaption: string = 'KindCaption';
  readonly Digest: string = 'Digest';
  readonly Period: string = 'Period';
  readonly Planned: string = 'Planned';
  readonly InitTaskScript: string = 'InitTaskScript';
  readonly Result: string = 'Result';
  readonly IsParallel: string = 'IsParallel';
  readonly ReturnWhenApproved: string = 'ReturnWhenApproved';
  readonly CanEditCard: string = 'CanEditCard';
  readonly CanEditAnyFiles: string = 'CanEditAnyFiles';
  readonly ChangeStateOnStart: string = 'ChangeStateOnStart';
  readonly ChangeStateOnEnd: string = 'ChangeStateOnEnd';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';
  readonly SqlPerformersScript: string = 'SqlPerformersScript';
  readonly ExpectAllSigners: string = 'ExpectAllSigners';
  readonly AllowAdditionalApproval: string = 'AllowAdditionalApproval';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSigningStageSettingsVirtual

/**
 * ID: {a53d9011-97c3-4890-97b8-c19c91ae8948}
 * Alias: KrSigningStageSettingsVirtual
 * Group: KrStageTypes
 */
class KrSigningStageSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrSigningStageSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly IsParallel: string = 'IsParallel';
  readonly ReturnToAuthor: string = 'ReturnToAuthor';
  readonly ReturnWhenDeclined: string = 'ReturnWhenDeclined';
  readonly CanEditCard: string = 'CanEditCard';
  readonly CanEditFiles: string = 'CanEditFiles';
  readonly Comment: string = 'Comment';
  readonly ChangeStateOnStart: string = 'ChangeStateOnStart';
  readonly ChangeStateOnEnd: string = 'ChangeStateOnEnd';
  readonly NotReturnEdit: string = 'NotReturnEdit';
  readonly AllowAdditionalApproval: string = 'AllowAdditionalApproval';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSigningTaskOptions

/**
 * ID: {0ad2b029-2f30-4e19-96df-bc3c2dcd9dfe}
 * Alias: KrSigningTaskOptions
 * Group: KrStageTypes
 * Description: Таблица с параметрами задания "Подписание".
 */
class KrSigningTaskOptionsSchemeInfo {
  private readonly name: string = 'KrSigningTaskOptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AllowAdditionalApproval: string = 'AllowAdditionalApproval';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrSinglePerformerVirtual

/**
 * ID: {52b86f8c-bc19-4dee-8e53-54236bf951a6}
 * Alias: KrSinglePerformerVirtual
 * Group: KrStageTypes
 */
class KrSinglePerformerVirtualSchemeInfo {
  private readonly name: string = 'KrSinglePerformerVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly PerformerID: string = 'PerformerID';
  readonly PerformerName: string = 'PerformerName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrStageCommonMethods

/**
 * ID: {42a0388c-2064-4dbb-ba35-2ca8979af629}
 * Alias: KrStageCommonMethods
 * Group: Kr
 * Description: Основная таблица для базовых методов, используемых в шаблонах компиляции KrStageTemplate
 */
class KrStageCommonMethodsSchemeInfo {
  private readonly name: string = 'KrStageCommonMethods';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Description: string = 'Description';
  readonly Source: string = 'Source';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrStageDocStates

/**
 * ID: {4f6c7635-031d-411a-9219-069e05a7e8b6}
 * Alias: KrStageDocStates
 * Group: Kr
 */
class KrStageDocStatesSchemeInfo {
  private readonly name: string = 'KrStageDocStates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrStageGroups

/**
 * ID: {fde6b6e3-f7b6-467f-96e1-e2df41a22f05}
 * Alias: KrStageGroups
 * Group: Kr
 */
class KrStageGroupsSchemeInfo {
  private readonly name: string = 'KrStageGroups';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Order: string = 'Order';
  readonly IsGroupReadonly: string = 'IsGroupReadonly';
  readonly SourceCondition: string = 'SourceCondition';
  readonly SourceBefore: string = 'SourceBefore';
  readonly SourceAfter: string = 'SourceAfter';
  readonly RuntimeSourceCondition: string = 'RuntimeSourceCondition';
  readonly RuntimeSourceBefore: string = 'RuntimeSourceBefore';
  readonly RuntimeSourceAfter: string = 'RuntimeSourceAfter';
  readonly SqlCondition: string = 'SqlCondition';
  readonly RuntimeSqlCondition: string = 'RuntimeSqlCondition';
  readonly Description: string = 'Description';
  readonly KrSecondaryProcessID: string = 'KrSecondaryProcessID';
  readonly KrSecondaryProcessName: string = 'KrSecondaryProcessName';
  readonly Ignore: string = 'Ignore';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrStageGroupTemplatesVirtual

/**
 * ID: {f9d10aed-ae25-42e8-b936-1b97014c4e13}
 * Alias: KrStageGroupTemplatesVirtual
 * Group: Kr
 */
class KrStageGroupTemplatesVirtualSchemeInfo {
  private readonly name: string = 'KrStageGroupTemplatesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TemplateID: string = 'TemplateID';
  readonly TemplateName: string = 'TemplateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrStageRoles

/**
 * ID: {97805de9-ed94-41a3-bf8b-fc1fa17c7d30}
 * Alias: KrStageRoles
 * Group: Kr
 * Description: Список ролей для шаблона этапа и группы этапов
 */
class KrStageRolesSchemeInfo {
  private readonly name: string = 'KrStageRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrStages

/**
 * ID: {92caadca-2409-40ff-b7d8-1d4fd302b1e9}
 * Alias: KrStages
 * Group: Kr
 */
class KrStagesSchemeInfo {
  private readonly name: string = 'KrStages';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';
  readonly Order: string = 'Order';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';
  readonly TimeLimit: string = 'TimeLimit';
  readonly SqlApproverRole: string = 'SqlApproverRole';
  readonly RowChanged: string = 'RowChanged';
  readonly OrderChanged: string = 'OrderChanged';
  readonly BasedOnStageRowID: string = 'BasedOnStageRowID';
  readonly BasedOnStageTemplateID: string = 'BasedOnStageTemplateID';
  readonly BasedOnStageTemplateName: string = 'BasedOnStageTemplateName';
  readonly BasedOnStageTemplateOrder: string = 'BasedOnStageTemplateOrder';
  readonly BasedOnStageTemplateGroupPositionID: string = 'BasedOnStageTemplateGroupPositionID';
  readonly StageTypeID: string = 'StageTypeID';
  readonly StageTypeCaption: string = 'StageTypeCaption';
  readonly DisplayTimeLimit: string = 'DisplayTimeLimit';
  readonly DisplayParticipants: string = 'DisplayParticipants';
  readonly DisplaySettings: string = 'DisplaySettings';
  readonly Settings: string = 'Settings';
  readonly Info: string = 'Info';
  readonly RuntimeSourceCondition: string = 'RuntimeSourceCondition';
  readonly RuntimeSourceBefore: string = 'RuntimeSourceBefore';
  readonly RuntimeSourceAfter: string = 'RuntimeSourceAfter';
  readonly StageGroupID: string = 'StageGroupID';
  readonly StageGroupOrder: string = 'StageGroupOrder';
  readonly StageGroupName: string = 'StageGroupName';
  readonly RuntimeSqlCondition: string = 'RuntimeSqlCondition';
  readonly Hidden: string = 'Hidden';
  readonly NestedProcessID: string = 'NestedProcessID';
  readonly ParentStageRowID: string = 'ParentStageRowID';
  readonly NestedOrder: string = 'NestedOrder';
  readonly ExtraSources: string = 'ExtraSources';
  readonly Planned: string = 'Planned';
  readonly Skip: string = 'Skip';
  readonly CanBeSkipped: string = 'CanBeSkipped';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrStageState

/**
 * ID: {beee4f3d-a385-4fc8-884f-bc1ccf55fc5b}
 * Alias: KrStageState
 * Group: Kr
 */
class KrStageStateSchemeInfo {
  private readonly name: string = 'KrStageState';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Inactive: KrStageState = new KrStageState(0, '$KrStates_Stage_Inactive');
  readonly Active: KrStageState = new KrStageState(1, '$KrStates_Stage_Active');
  readonly Completed: KrStageState = new KrStageState(2, '$KrStates_Stage_Completed');
  readonly Skipped: KrStageState = new KrStageState(3, '$KrStates_Stage_Skipped');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrStageState Enumeration

class KrStageState {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrStagesVirtual

/**
 * ID: {89d78d5c-f8dd-48e7-868c-88bbafe74257}
 * Alias: KrStagesVirtual
 * Group: Kr
 */
class KrStagesVirtualSchemeInfo {
  private readonly name: string = 'KrStagesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';
  readonly Order: string = 'Order';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';
  readonly TimeLimit: string = 'TimeLimit';
  readonly SqlApproverRole: string = 'SqlApproverRole';
  readonly RowChanged: string = 'RowChanged';
  readonly OrderChanged: string = 'OrderChanged';
  readonly BasedOnStageRowID: string = 'BasedOnStageRowID';
  readonly BasedOnStageTemplateID: string = 'BasedOnStageTemplateID';
  readonly BasedOnStageTemplateName: string = 'BasedOnStageTemplateName';
  readonly BasedOnStageTemplateOrder: string = 'BasedOnStageTemplateOrder';
  readonly BasedOnStageTemplateGroupPositionID: string = 'BasedOnStageTemplateGroupPositionID';
  readonly StageTypeID: string = 'StageTypeID';
  readonly StageTypeCaption: string = 'StageTypeCaption';
  readonly DisplayTimeLimit: string = 'DisplayTimeLimit';
  readonly DisplayParticipants: string = 'DisplayParticipants';
  readonly DisplaySettings: string = 'DisplaySettings';
  readonly RuntimeSourceCondition: string = 'RuntimeSourceCondition';
  readonly RuntimeSourceBefore: string = 'RuntimeSourceBefore';
  readonly RuntimeSourceAfter: string = 'RuntimeSourceAfter';
  readonly StageGroupID: string = 'StageGroupID';
  readonly StageGroupName: string = 'StageGroupName';
  readonly StageGroupOrder: string = 'StageGroupOrder';
  readonly RuntimeSqlCondition: string = 'RuntimeSqlCondition';
  readonly Hidden: string = 'Hidden';
  readonly Planned: string = 'Planned';
  readonly Skip: string = 'Skip';
  readonly CanBeSkipped: string = 'CanBeSkipped';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrStageTemplateGroupPosition

/**
 * ID: {496c30f2-79d0-408a-8085-95b43d67a22b}
 * Alias: KrStageTemplateGroupPosition
 * Group: Kr
 * Description: Позиции, куда необходимо подставлять этапы из шаблона этапа KrStageTemplate
 */
class KrStageTemplateGroupPositionSchemeInfo {
  private readonly name: string = 'KrStageTemplateGroupPosition';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly AtFirst: KrStageTemplateGroupPosition = new KrStageTemplateGroupPosition(0, '$Views_KrStageTemplateGroupPosition_AtFirst');
  readonly AtLast: KrStageTemplateGroupPosition = new KrStageTemplateGroupPosition(1, '$Views_KrStageTemplateGroupPosition_AtLast');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrStageTemplateGroupPosition Enumeration

class KrStageTemplateGroupPosition {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region KrStageTemplates

/**
 * ID: {5a33ac72-f6f5-4e5a-8d8c-4a94ed7bf324}
 * Alias: KrStageTemplates
 * Group: Kr
 * Description: Таблица с информацией по шаблонам этапов. Для карточки KrStageTemplate.
 */
class KrStageTemplatesSchemeInfo {
  private readonly name: string = 'KrStageTemplates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Order: string = 'Order';
  readonly Description: string = 'Description';
  readonly CanChangeOrder: string = 'CanChangeOrder';
  readonly IsStagesReadonly: string = 'IsStagesReadonly';
  readonly GroupPositionID: string = 'GroupPositionID';
  readonly GroupPositionName: string = 'GroupPositionName';
  readonly SqlCondition: string = 'SqlCondition';
  readonly SourceCondition: string = 'SourceCondition';
  readonly SourceBefore: string = 'SourceBefore';
  readonly SourceAfter: string = 'SourceAfter';
  readonly StageGroupID: string = 'StageGroupID';
  readonly StageGroupName: string = 'StageGroupName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrStageTypes

/**
 * ID: {971d8661-d445-42fb-84d0-b0b71aa978a2}
 * Alias: KrStageTypes
 * Group: Kr
 * Description: Список типов документов для шаблона этапа и группы этапов
 */
class KrStageTypesSchemeInfo {
  private readonly name: string = 'KrStageTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly TypeIsDocType: string = 'TypeIsDocType';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrTask

/**
 * ID: {51936147-e0ff-4e19-a7d1-0ea7d462ceec}
 * Alias: KrTask
 * Group: KrStageTypes
 */
class KrTaskSchemeInfo {
  private readonly name: string = 'KrTask';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Comment: string = 'Comment';
  readonly DelegateID: string = 'DelegateID';
  readonly DelegateName: string = 'DelegateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrTaskCommentVirtual

/**
 * ID: {344fa4e8-cdfc-4cb0-8634-9155c49fd21a}
 * Alias: KrTaskCommentVirtual
 * Group: KrStageTypes
 */
class KrTaskCommentVirtualSchemeInfo {
  private readonly name: string = 'KrTaskCommentVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Comment: string = 'Comment';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrTaskKindSettingsVirtual

/**
 * ID: {80ab607a-d43f-435d-a1be-a203bb99c2d3}
 * Alias: KrTaskKindSettingsVirtual
 * Group: KrStageTypes
 */
class KrTaskKindSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrTaskKindSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly KindID: string = 'KindID';
  readonly KindCaption: string = 'KindCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrTaskRegistrationActionNotificationRolesVitrual

/**
 * ID: {406b3337-8cfc-437d-a7fc-408b96a92c00}
 * Alias: KrTaskRegistrationActionNotificationRolesVitrual
 * Group: KrWe
 * Description: Действие "Задание регистрации". Коллекционная секция содержащая роли на которые отправляется уведомление при завершении задания.
 */
class KrTaskRegistrationActionNotificationRolesVitrualSchemeInfo {
  private readonly name: string = 'KrTaskRegistrationActionNotificationRolesVitrual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly OptionRowID: string = 'OptionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrTaskRegistrationActionOptionLinksVirtual

/**
 * ID: {f6a8f11a-68c2-4743-a8ed-236fe459dfc9}
 * Alias: KrTaskRegistrationActionOptionLinksVirtual
 * Group: KrWe
 * Description: Действие "Задание регистрации". Коллекционная секция объединяющая связи и вырианты завершения.
 */
class KrTaskRegistrationActionOptionLinksVirtualSchemeInfo {
  private readonly name: string = 'KrTaskRegistrationActionOptionLinksVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly LinkID: string = 'LinkID';
  readonly LinkName: string = 'LinkName';
  readonly LinkCaption: string = 'LinkCaption';
  readonly OptionRowID: string = 'OptionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrTaskRegistrationActionOptionsVirtual

/**
 * ID: {2ba2b1a3-b8ad-4c47-a8fd-3a3fa421c7a9}
 * Alias: KrTaskRegistrationActionOptionsVirtual
 * Group: KrWe
 * Description: Действие "Задание регистрации". Таблица с обрабатываемыми вариантами завершения задания действия.
 */
class KrTaskRegistrationActionOptionsVirtualSchemeInfo {
  private readonly name: string = 'KrTaskRegistrationActionOptionsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';
  readonly LinkID: string = 'LinkID';
  readonly Script: string = 'Script';
  readonly Order: string = 'Order';
  readonly Result: string = 'Result';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly SendToPerformer: string = 'SendToPerformer';
  readonly SendToAuthor: string = 'SendToAuthor';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrTaskRegistrationActionVirtual

/**
 * ID: {12b90f64-b971-4198-ad0e-0e3d1988f946}
 * Alias: KrTaskRegistrationActionVirtual
 * Group: KrWe
 * Description: Праметры действия "Задание регистрации".
 */
class KrTaskRegistrationActionVirtualSchemeInfo {
  private readonly name: string = 'KrTaskRegistrationActionVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly PerformerID: string = 'PerformerID';
  readonly PerformerName: string = 'PerformerName';
  readonly KindID: string = 'KindID';
  readonly KindCaption: string = 'KindCaption';
  readonly Digest: string = 'Digest';
  readonly Period: string = 'Period';
  readonly Planned: string = 'Planned';
  readonly InitTaskScript: string = 'InitTaskScript';
  readonly Result: string = 'Result';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';
  readonly CanEditCard: string = 'CanEditCard';
  readonly CanEditAnyFiles: string = 'CanEditAnyFiles';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrTaskTypeCondition

/**
 * ID: {0209ddc9-6457-406e-8a09-ab8ac6916e26}
 * Alias: KrTaskTypeCondition
 * Group: Kr
 * Description: Основная секция с тиами заданий для настройки условия "По типу заданий"
 */
class KrTaskTypeConditionSchemeInfo {
  private readonly name: string = 'KrTaskTypeCondition';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TaskTypeID: string = 'TaskTypeID';
  readonly TaskTypeCaption: string = 'TaskTypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrTaskTypeConditionSettings

/**
 * ID: {4b9735f4-7db4-46e1-bbdd-4bd71f5234bd}
 * Alias: KrTaskTypeConditionSettings
 * Group: Kr
 */
class KrTaskTypeConditionSettingsSchemeInfo {
  private readonly name: string = 'KrTaskTypeConditionSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly InProgress: string = 'InProgress';
  readonly IsAuthor: string = 'IsAuthor';
  readonly IsPerformer: string = 'IsPerformer';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrTypedTaskSettingsVirtual

/**
 * ID: {e06fa88f-35a2-48fc-8ce4-6e20521b5238}
 * Alias: KrTypedTaskSettingsVirtual
 * Group: KrStageTypes
 */
class KrTypedTaskSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrTypedTaskSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly TaskTypeID: string = 'TaskTypeID';
  readonly TaskTypeName: string = 'TaskTypeName';
  readonly TaskTypeCaption: string = 'TaskTypeCaption';
  readonly AfterTaskCompletion: string = 'AfterTaskCompletion';
  readonly TaskDigest: string = 'TaskDigest';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrUniversalTaskActionButtonLinksVirtual

/**
 * ID: {0938b2e9-485e-4f87-8622-706bbaf0efb7}
 * Alias: KrUniversalTaskActionButtonLinksVirtual
 * Group: KrWe
 * Description: Действие "Настраиваемое задание". Коллекционная секция объединяющая связи и вырианты завершения.
 */
class KrUniversalTaskActionButtonLinksVirtualSchemeInfo {
  private readonly name: string = 'KrUniversalTaskActionButtonLinksVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly LinkID: string = 'LinkID';
  readonly LinkName: string = 'LinkName';
  readonly LinkCaption: string = 'LinkCaption';
  readonly ButtonRowID: string = 'ButtonRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrUniversalTaskActionButtonsVirtual

/**
 * ID: {e85631c4-0014-4842-86f4-9a6ba66166f3}
 * Alias: KrUniversalTaskActionButtonsVirtual
 * Group: KrWe
 * Description: Действие "Настраиваемое задание". Параметры настраиваемых вариантов завершения.
 */
class KrUniversalTaskActionButtonsVirtualSchemeInfo {
  private readonly name: string = 'KrUniversalTaskActionButtonsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Order: string = 'Order';
  readonly Caption: string = 'Caption';
  readonly Digest: string = 'Digest';
  readonly IsShowComment: string = 'IsShowComment';
  readonly IsAdditionalOption: string = 'IsAdditionalOption';
  readonly LinkID: string = 'LinkID';
  readonly Script: string = 'Script';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly SendToPerformer: string = 'SendToPerformer';
  readonly SendToAuthor: string = 'SendToAuthor';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';
  readonly OptionID: string = 'OptionID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrUniversalTaskActionNotificationRolesVitrual

/**
 * ID: {1d7d2be8-692e-478d-9ce4-fc791833ffba}
 * Alias: KrUniversalTaskActionNotificationRolesVitrual
 * Group: KrWe
 * Description: Действие "Настраиваемое задание". Коллекционная секция содержащая роли на которые отправляется уведомление при завершении задания.
 */
class KrUniversalTaskActionNotificationRolesVitrualSchemeInfo {
  private readonly name: string = 'KrUniversalTaskActionNotificationRolesVitrual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly ButtonRowID: string = 'ButtonRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrUniversalTaskActionVirtual

/**
 * ID: {b0ca69b1-7c90-4ce7-995c-2f9540ec45ef}
 * Alias: KrUniversalTaskActionVirtual
 * Group: KrWe
 * Description: Параметры действия "Настраиваемое задание".
 */
class KrUniversalTaskActionVirtualSchemeInfo {
  private readonly name: string = 'KrUniversalTaskActionVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly KindID: string = 'KindID';
  readonly KindCaption: string = 'KindCaption';
  readonly Digest: string = 'Digest';
  readonly Period: string = 'Period';
  readonly Planned: string = 'Planned';
  readonly InitTaskScript: string = 'InitTaskScript';
  readonly Result: string = 'Result';
  readonly CanEditCard: string = 'CanEditCard';
  readonly CanEditAnyFiles: string = 'CanEditAnyFiles';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrUniversalTaskOptions

/**
 * ID: {470ddc9e-4715-4efa-bd25-cbb9f4033162}
 * Alias: KrUniversalTaskOptions
 * Group: KrStageTypes
 * Description: Секция с данными универсального задания
 */
class KrUniversalTaskOptionsSchemeInfo {
  private readonly name: string = 'KrUniversalTaskOptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly OptionID: string = 'OptionID';
  readonly Caption: string = 'Caption';
  readonly ShowComment: string = 'ShowComment';
  readonly Additional: string = 'Additional';
  readonly Order: string = 'Order';
  readonly Message: string = 'Message';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrUniversalTaskOptionsSettingsVirtual

/**
 * ID: {49f11daf-636a-4342-aa2b-ea798bed7263}
 * Alias: KrUniversalTaskOptionsSettingsVirtual
 * Group: KrStageTypes
 * Description: Секция с вариантами завершения этапа универсального задания
 */
class KrUniversalTaskOptionsSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrUniversalTaskOptionsSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly OptionID: string = 'OptionID';
  readonly Caption: string = 'Caption';
  readonly ShowComment: string = 'ShowComment';
  readonly Additional: string = 'Additional';
  readonly Order: string = 'Order';
  readonly Message: string = 'Message';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrUniversalTaskSettingsVirtual

/**
 * ID: {01db90f2-22ec-4233-a5fd-587a832b1b48}
 * Alias: KrUniversalTaskSettingsVirtual
 * Group: KrStageTypes
 * Description: Секция настроек этапа Универсальное задание
 */
class KrUniversalTaskSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrUniversalTaskSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Digest: string = 'Digest';
  readonly CanEditCard: string = 'CanEditCard';
  readonly CanEditFiles: string = 'CanEditFiles';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrUsersCondition

/**
 * ID: {c9cf90e4-72e2-4cd2-a798-62b1d856cea5}
 * Alias: KrUsersCondition
 * Group: Kr
 * Description: Секция для условий для правил уведомлений, проверяющих принадлежность сотрудника
 */
class KrUsersConditionSchemeInfo {
  private readonly name: string = 'KrUsersCondition';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrUserSettingsVirtual

/**
 * ID: {84d45612-8cbb-4b7d-91eb-2796003a365d}
 * Alias: KrUserSettingsVirtual
 * Group: KrStageTypes
 */
class KrUserSettingsVirtualSchemeInfo {
  private readonly name: string = 'KrUserSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly DisableTaskPopupNotifications: string = 'DisableTaskPopupNotifications';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrVirtualFileCardTypes

/**
 * ID: {5ad723d6-21d8-48c2-8799-a1ba9fb1c758}
 * Alias: KrVirtualFileCardTypes
 * Group: Kr
 * Description: Типы карточек для карточки виртуального файла
 */
class KrVirtualFileCardTypesSchemeInfo {
  private readonly name: string = 'KrVirtualFileCardTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrVirtualFileRoles

/**
 * ID: {9d6186ba-f7ae-4910-8784-6c63a3b13179}
 * Alias: KrVirtualFileRoles
 * Group: Kr
 * Description: Роли для карточки виртуального файла
 */
class KrVirtualFileRolesSchemeInfo {
  private readonly name: string = 'KrVirtualFileRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrVirtualFiles

/**
 * ID: {006f8d09-5eff-46fc-8bc5-d7c5f6a81d44}
 * Alias: KrVirtualFiles
 * Group: Kr
 * Description: Основная секция для карточи виртуального файла
 */
class KrVirtualFilesSchemeInfo {
  private readonly name: string = 'KrVirtualFiles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly FileName: string = 'FileName';
  readonly FileID: string = 'FileID';
  readonly FileVersionID: string = 'FileVersionID';
  readonly InitializationScenario: string = 'InitializationScenario';
  readonly FileTemplateID: string = 'FileTemplateID';
  readonly FileTemplateName: string = 'FileTemplateName';
  readonly FileCategoryID: string = 'FileCategoryID';
  readonly FileCategoryName: string = 'FileCategoryName';
  readonly Conditions: string = 'Conditions';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrVirtualFileStates

/**
 * ID: {a6386cf3-fff8-401e-b8d2-222d0221951f}
 * Alias: KrVirtualFileStates
 * Group: Kr
 * Description: Состояния для карточки виртуального файла
 */
class KrVirtualFileStatesSchemeInfo {
  private readonly name: string = 'KrVirtualFileStates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrVirtualFileVersions

/**
 * ID: {8bd27c6a-16e0-4ac4-a147-8cc2d30dc88b}
 * Alias: KrVirtualFileVersions
 * Group: Kr
 */
class KrVirtualFileVersionsSchemeInfo {
  private readonly name: string = 'KrVirtualFileVersions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly FileName: string = 'FileName';
  readonly FileVersionID: string = 'FileVersionID';
  readonly FileTemplateID: string = 'FileTemplateID';
  readonly FileTemplateName: string = 'FileTemplateName';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrWeActionCompletionOptions

/**
 * ID: {6a24d3cd-ec83-4e7a-8815-77b054c69371}
 * Alias: KrWeActionCompletionOptions
 * Group: KrWe
 * Description: Список возможных вариантов завершения действий.
 */
class KrWeActionCompletionOptionsSchemeInfo {
  private readonly name: string = 'KrWeActionCompletionOptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';

  //#endregion

  //#region Enumeration

  readonly Approved: KrWeActionCompletionOptions = new KrWeActionCompletionOptions('4339a03f-234d-4a9a-a6e4-58a88a5a03ce', 'Approved', '$KrAction_ActionCompletionOption_Approved');
  readonly Disapproved: KrWeActionCompletionOptions = new KrWeActionCompletionOptions('6fbdd34b-be9a-40bf-90cb-1640d4abb9f5', 'Disapproved', '$KrAction_ActionCompletionOption_Disapproved');
  readonly Signed: KrWeActionCompletionOptions = new KrWeActionCompletionOptions('fa94b7bf-7b99-46d6-9c65-b21a483ebc45', 'Signed', '$KrAction_ActionCompletionOption_Signed');
  readonly Declined: KrWeActionCompletionOptions = new KrWeActionCompletionOptions('4a1936c7-1f94-4897-9dae-934163e2fe1c', 'Declined', '$KrAction_ActionCompletionOption_Declined');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region KrWeActionCompletionOptions Enumeration

class KrWeActionCompletionOptions {
  readonly ID: guid;
  readonly Name: string | null;
  readonly Caption: string | null;

  constructor (ID: guid, Name: string | null, Caption: string | null) {
    this.ID = ID;
    this.Name = Name;
    this.Caption = Caption;
  }
}

//#endregion

//#endregion

//#region KrWeAdditionalApprovalOptionsVirtual

/**
 * ID: {54829879-8b8e-4d47-a27b-0346e93e6e45}
 * Alias: KrWeAdditionalApprovalOptionsVirtual
 * Group: KrWe
 * Description: Параметры дополнительного согласования.
 */
class KrWeAdditionalApprovalOptionsVirtualSchemeInfo {
  private readonly name: string = 'KrWeAdditionalApprovalOptionsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';
  readonly InitTaskScript: string = 'InitTaskScript';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrWeEditInterjectOptionsVirtual

/**
 * ID: {d0771103-6c21-4602-af56-d264e82f8b57}
 * Alias: KrWeEditInterjectOptionsVirtual
 * Group: KrWe
 * Description: Параметры доработки автором.
 */
class KrWeEditInterjectOptionsVirtualSchemeInfo {
  private readonly name: string = 'KrWeEditInterjectOptionsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly KindID: string = 'KindID';
  readonly KindCaption: string = 'KindCaption';
  readonly Digest: string = 'Digest';
  readonly Period: string = 'Period';
  readonly Planned: string = 'Planned';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';
  readonly InitTaskScript: string = 'InitTaskScript';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrWeRequestCommentOptionsVirtual

/**
 * ID: {abc045c0-75b1-4a25-8d9e-d6b323118f08}
 * Alias: KrWeRequestCommentOptionsVirtual
 * Group: KrWe
 * Description: Параметры запроса комментария.
 */
class KrWeRequestCommentOptionsVirtualSchemeInfo {
  private readonly name: string = 'KrWeRequestCommentOptionsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';
  readonly InitTaskScript: string = 'InitTaskScript';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region KrWeRolesVirtual

/**
 * ID: {eea339fd-2e18-415b-b338-418f84ac961e}
 * Alias: KrWeRolesVirtual
 * Group: KrWe
 * Description: Роли.
 */
class KrWeRolesVirtualSchemeInfo {
  private readonly name: string = 'KrWeRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Languages

/**
 * ID: {1ed36bf1-2ebf-43da-acb2-1ddb3298dbd8}
 * Alias: Languages
 * Group: System
 */
class LanguagesSchemeInfo {
  private readonly name: string = 'Languages';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Caption: string = 'Caption';
  readonly Code: string = 'Code';
  readonly FallbackID: string = 'FallbackID';

  //#endregion

  //#region Enumeration

  readonly Srpski: Languages = new Languages(2074, 'Srpski', 'sr', 0);
  readonly Slovenščina: Languages = new Languages(1060, 'Slovenščina', 'sl', 0);
  readonly Hrvatski: Languages = new Languages(1050, 'Hrvatski', 'hr', 0);
  readonly English: Languages = new Languages(0, 'English', 'en', null);
  readonly Russkiy: Languages = new Languages(1, 'Русский', 'ru', null);

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region Languages Enumeration

class Languages {
  readonly ID: number;
  readonly Caption: string | null;
  readonly Code: string | null;
  readonly FallbackID: number | null;

  constructor (ID: number, Caption: string | null, Code: string | null, FallbackID: number | null) {
    this.ID = ID;
    this.Caption = Caption;
    this.Code = Code;
    this.FallbackID = FallbackID;
  }
}

//#endregion

//#endregion

//#region LawAdministrators

/**
 * ID: {3dbb9a1f-ae27-4612-aec1-4f077494dfef}
 * Alias: LawAdministrators
 * Group: LawList
 */
class LawAdministratorsSchemeInfo {
  private readonly name: string = 'LawAdministrators';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region LawCase

/**
 * ID: {191308f6-820e-408e-b779-cef96b1b09c0}
 * Alias: LawCase
 * Group: Law
 */
class LawCaseSchemeInfo {
  private readonly name: string = 'LawCase';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ClassificationPlanID: string = 'ClassificationPlanID';
  readonly ClassificationPlanPlan: string = 'ClassificationPlanPlan';
  readonly ClassificationPlanName: string = 'ClassificationPlanName';
  readonly ClassificationPlanFullName: string = 'ClassificationPlanFullName';
  readonly NumberByCourt: string = 'NumberByCourt';
  readonly LocationID: string = 'LocationID';
  readonly LocationName: string = 'LocationName';
  readonly CategoryID: string = 'CategoryID';
  readonly CategoryName: string = 'CategoryName';
  readonly Number: string = 'Number';
  readonly Date: string = 'Date';
  readonly DecisionDate: string = 'DecisionDate';
  readonly PCTO: string = 'PCTO';
  readonly IsLimitedAccess: string = 'IsLimitedAccess';
  readonly IsArchive: string = 'IsArchive';
  readonly Description: string = 'Description';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region LawClients

/**
 * ID: {362c9171-6267-42a8-8fd8-7bf39d04533e}
 * Alias: LawClients
 * Group: LawList
 */
class LawClientsSchemeInfo {
  private readonly name: string = 'LawClients';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ClientID: string = 'ClientID';
  readonly ClientName: string = 'ClientName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region LawFolders

/**
 * ID: {48b7ff15-cb0d-4acb-8b11-4ea833ea6f9b}
 * Alias: LawFolders
 * Group: LawList
 */
class LawFoldersSchemeInfo {
  private readonly name: string = 'LawFolders';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly Name: string = 'Name';
  readonly Date: string = 'Date';
  readonly Kind: string = 'Kind';
  readonly Number: string = 'Number';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region LawPartnerRepresentatives

/**
 * ID: {b4cfec48-deec-40fc-94fc-eae9e645afce}
 * Alias: LawPartnerRepresentatives
 * Group: LawList
 */
class LawPartnerRepresentativesSchemeInfo {
  private readonly name: string = 'LawPartnerRepresentatives';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RepresentativeID: string = 'RepresentativeID';
  readonly RepresentativeName: string = 'RepresentativeName';
  readonly RepresentativeAddressID: string = 'RepresentativeAddressID';
  readonly RepresentativeTaxNumber: string = 'RepresentativeTaxNumber';
  readonly RepresentativeRegistrationNumber: string = 'RepresentativeRegistrationNumber';
  readonly RepresentativeContacts: string = 'RepresentativeContacts';
  readonly RepresentativeStreet: string = 'RepresentativeStreet';
  readonly RepresentativePostalCode: string = 'RepresentativePostalCode';
  readonly RepresentativeCity: string = 'RepresentativeCity';
  readonly RepresentativeCountry: string = 'RepresentativeCountry';
  readonly RepresentativePoBox: string = 'RepresentativePoBox';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region LawPartners

/**
 * ID: {54244411-fea4-4bdd-b009-fad9c5915882}
 * Alias: LawPartners
 * Group: LawList
 */
class LawPartnersSchemeInfo {
  private readonly name: string = 'LawPartners';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly PartnerID: string = 'PartnerID';
  readonly PartnerName: string = 'PartnerName';
  readonly PartnerAddressID: string = 'PartnerAddressID';
  readonly PartnerTaxNumber: string = 'PartnerTaxNumber';
  readonly PartnerRegistrationNumber: string = 'PartnerRegistrationNumber';
  readonly PartnerContacts: string = 'PartnerContacts';
  readonly PartnerStreet: string = 'PartnerStreet';
  readonly PartnerPostalCode: string = 'PartnerPostalCode';
  readonly PartnerCity: string = 'PartnerCity';
  readonly PartnerCountry: string = 'PartnerCountry';
  readonly PartnerPoBox: string = 'PartnerPoBox';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region LawPartnersDialogVirtual

/**
 * ID: {c0b9d91c-4a9e-4c7a-b7c2-13daa37a1cf9}
 * Alias: LawPartnersDialogVirtual
 * Group: LawList
 * Description: Виртуальная таблица для редактирования компаний
 */
class LawPartnersDialogVirtualSchemeInfo {
  private readonly name: string = 'LawPartnersDialogVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';
  readonly AddressID: string = 'AddressID';
  readonly TaxNumber: string = 'TaxNumber';
  readonly RegistrationNumber: string = 'RegistrationNumber';
  readonly Contacts: string = 'Contacts';
  readonly Street: string = 'Street';
  readonly PostalCode: string = 'PostalCode';
  readonly City: string = 'City';
  readonly Country: string = 'Country';
  readonly PoBox: string = 'PoBox';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region LawUsers

/**
 * ID: {0b3ce213-cc34-469d-a0dd-19a4643b1a49}
 * Alias: LawUsers
 * Group: LawList
 */
class LawUsersSchemeInfo {
  private readonly name: string = 'LawUsers';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly UserWorkplace: string = 'UserWorkplace';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region LicenseTypes

/**
 * ID: {bcc286d4-9d77-4750-8084-15417b966528}
 * Alias: LicenseTypes
 * Group: System
 * Description: Типы лицензий для сессий.
 */
class LicenseTypesSchemeInfo {
  private readonly name: string = 'LicenseTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Unspecified: LicenseTypes = new LicenseTypes(0, '$Enum_SessionTypes_Unspecified');
  readonly Concurrent: LicenseTypes = new LicenseTypes(1, '$Enum_SessionTypes_Concurrent');
  readonly Personal: LicenseTypes = new LicenseTypes(2, '$Enum_SessionTypes_Personal');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region LicenseTypes Enumeration

class LicenseTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region LicenseVirtual

/**
 * ID: {f81a7db5-a883-49e0-918c-59a5967828b5}
 * Alias: LicenseVirtual
 * Group: System
 * Description: Виртуальная секция для настроек лицензий.
 */
class LicenseVirtualSchemeInfo {
  private readonly name: string = 'LicenseVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ConcurrentCount: string = 'ConcurrentCount';
  readonly ConcurrentLimit: string = 'ConcurrentLimit';
  readonly ConcurrentText: string = 'ConcurrentText';
  readonly PersonalCount: string = 'PersonalCount';
  readonly PersonalLimit: string = 'PersonalLimit';
  readonly PersonalText: string = 'PersonalText';
  readonly MobileCount: string = 'MobileCount';
  readonly MobileLimit: string = 'MobileLimit';
  readonly MobileText: string = 'MobileText';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region LocalizationEntries

/**
 * ID: {b92e97c0-4557-4d43-874a-e9a75173cbf8}
 * Alias: LocalizationEntries
 * Group: System
 */
class LocalizationEntriesSchemeInfo {
  private readonly name: string = 'LocalizationEntries';

  //#region Columns

  readonly LibraryID: string = 'LibraryID';
  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Comment: string = 'Comment';
  readonly Overridden: string = 'Overridden';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region LocalizationLibraries

/**
 * ID: {3e31b54e-1a4c-4e9c-bcf5-26e4780d6419}
 * Alias: LocalizationLibraries
 * Group: System
 */
class LocalizationLibrariesSchemeInfo {
  private readonly name: string = 'LocalizationLibraries';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Priority: string = 'Priority';
  readonly DetachedCultures: string = 'DetachedCultures';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region LocalizationStrings

/**
 * ID: {3e0ef7dd-7303-41e8-9ddc-af0a30e0de84}
 * Alias: LocalizationStrings
 * Group: System
 */
class LocalizationStringsSchemeInfo {
  private readonly name: string = 'LocalizationStrings';

  //#region Columns

  readonly EntryID: string = 'EntryID';
  readonly Value: string = 'Value';
  readonly Culture: string = 'Culture';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region LoginTypes

/**
 * ID: {44a94501-a954-4ab1-a7f8-47eebb2f869b}
 * Alias: LoginTypes
 * Group: System
 * Description: Типы входа пользователей в систему
 */
class LoginTypesSchemeInfo {
  private readonly name: string = 'LoginTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly None: LoginTypes = new LoginTypes(0, '$Enum_LoginTypes_None');
  readonly Tessa: LoginTypes = new LoginTypes(1, '$Enum_LoginTypes_Tessa');
  readonly Windows: LoginTypes = new LoginTypes(2, '$Enum_LoginTypes_Windows');
  readonly Ldap: LoginTypes = new LoginTypes(3, '$Enum_LoginTypes_Ldap');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region LoginTypes Enumeration

class LoginTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region MetaRoles

/**
 * ID: {984ac49b-30d7-48da-ab6a-3fe4bcf7513d}
 * Alias: MetaRoles
 * Group: Roles
 * Description: Метароли.
 */
class MetaRolesSchemeInfo {
  private readonly name: string = 'MetaRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly TypeID: string = 'TypeID';
  readonly GeneratorID: string = 'GeneratorID';
  readonly GeneratorName: string = 'GeneratorName';
  readonly IDGuid: string = 'IDGuid';
  readonly IDInteger: string = 'IDInteger';
  readonly IDString: string = 'IDString';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region MetaRoleTypes

/**
 * ID: {53a3ee37-b714-4503-9e0e-e2ed1ccd164f}
 * Alias: MetaRoleTypes
 * Group: Roles
 * Description: Типы метаролей.
 */
class MetaRoleTypesSchemeInfo {
  private readonly name: string = 'MetaRoleTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Guid: MetaRoleTypes = new MetaRoleTypes(0, 'Guid');
  readonly Integer: MetaRoleTypes = new MetaRoleTypes(1, 'Integer');
  readonly String: MetaRoleTypes = new MetaRoleTypes(2, 'String');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region MetaRoleTypes Enumeration

class MetaRoleTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region Migrations

/**
 * ID: {fd65afe6-d4bf-4885-872a-3824e64b1c63}
 * Alias: Migrations
 * Group: System
 * Description: Migrations
 */
class MigrationsSchemeInfo {
  private readonly name: string = 'Migrations';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Definition: string = 'Definition';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region MobileLicenses

/**
 * ID: {457f5393-50a4-40ea-8637-37fb57330ae2}
 * Alias: MobileLicenses
 * Group: System
 * Description: Сотрудники, для которых указаны лицензии мобильного согласования.
 */
class MobileLicensesSchemeInfo {
  private readonly name: string = 'MobileLicenses';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region NestedRoles

/**
 * ID: {312b1519-8079-44d7-a5b4-496db41da98c}
 * Alias: NestedRoles
 * Group: Acl
 * Description: Основная секция для вложенных ролей.
 */
class NestedRolesSchemeInfo {
  private readonly name: string = 'NestedRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ContextID: string = 'ContextID';
  readonly ContextName: string = 'ContextName';
  readonly ParentID: string = 'ParentID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Notifications

/**
 * ID: {18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a}
 * Alias: Notifications
 * Group: System
 * Description: Основная секция для карточки Уведомление
 */
class NotificationsSchemeInfo {
  private readonly name: string = 'Notifications';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Description: string = 'Description';
  readonly NotificationTypeID: string = 'NotificationTypeID';
  readonly NotificationTypeName: string = 'NotificationTypeName';
  readonly AliasMetadata: string = 'AliasMetadata';
  readonly Subject: string = 'Subject';
  readonly Text: string = 'Text';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region NotificationSubscribeTypes

/**
 * ID: {287bfcf3-aa96-44ee-96a8-68fbc1f2d3ab}
 * Alias: NotificationSubscribeTypes
 * Group: System
 */
class NotificationSubscribeTypesSchemeInfo {
  private readonly name: string = 'NotificationSubscribeTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly NotificationTypeID: string = 'NotificationTypeID';
  readonly NotificationTypeName: string = 'NotificationTypeName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region NotificationSubscriptions

/**
 * ID: {d5b074e2-eaff-4993-b238-1d5d3d248d56}
 * Alias: NotificationSubscriptions
 * Group: System
 * Description: Таблица с подписками/отписками пользователей по карточкам
 */
class NotificationSubscriptionsSchemeInfo {
  private readonly name: string = 'NotificationSubscriptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly UserID: string = 'UserID';
  readonly CardID: string = 'CardID';
  readonly CardDigest: string = 'CardDigest';
  readonly NotificationTypeID: string = 'NotificationTypeID';
  readonly NotificationTypeName: string = 'NotificationTypeName';
  readonly IsSubscription: string = 'IsSubscription';
  readonly SubscriptionDate: string = 'SubscriptionDate';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region NotificationSubscriptionSettings

/**
 * ID: {9ffce865-a6cd-4883-9ed0-0cbeaa1831d1}
 * Alias: NotificationSubscriptionSettings
 * Group: System
 * Description: Виртуальная секция для виртуальной карточки настроек уведомлений
 */
class NotificationSubscriptionSettingsSchemeInfo {
  private readonly name: string = 'NotificationSubscriptionSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CardTypeID: string = 'CardTypeID';
  readonly CardTypeCaption: string = 'CardTypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region NotificationTokenRoles

/**
 * ID: {a88ed4f8-dcce-400a-931f-99defee9949c}
 * Alias: NotificationTokenRoles
 * Group: System
 * Description: Список ролей, получающих уведомления о необходимости обновления токена.
 */
class NotificationTokenRolesSchemeInfo {
  private readonly name: string = 'NotificationTokenRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region NotificationTypeCardTypes

/**
 * ID: {b54be13b-72be-4b20-b090-b861efaf8585}
 * Alias: NotificationTypeCardTypes
 * Group: System
 */
class NotificationTypeCardTypesSchemeInfo {
  private readonly name: string = 'NotificationTypeCardTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region NotificationTypes

/**
 * ID: {bae37ba2-7a39-49a1-8cc8-64f032ba3f79}
 * Alias: NotificationTypes
 * Group: System
 * Description: Основная секция для карточки Тип уведомления
 */
class NotificationTypesSchemeInfo {
  private readonly name: string = 'NotificationTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly IsGlobal: string = 'IsGlobal';
  readonly CanSubscribe: string = 'CanSubscribe';
  readonly Hidden: string = 'Hidden';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region NotificationUnsubscribeTypes

/**
 * ID: {d845a7f8-9873-47c1-a160-370f66dc852e}
 * Alias: NotificationUnsubscribeTypes
 * Group: System
 */
class NotificationUnsubscribeTypesSchemeInfo {
  private readonly name: string = 'NotificationUnsubscribeTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly NotificationTypeID: string = 'NotificationTypeID';
  readonly NotificationTypeName: string = 'NotificationTypeName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrLanguages

/**
 * ID: {a5b1b1cf-ad8c-4459-a880-a5ff9f435398}
 * Alias: OcrLanguages
 * Group: Ocr
 * Description: Supported languages ​​for text recognition
 */
class OcrLanguagesSchemeInfo {
  private readonly name: string = 'OcrLanguages';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ISO: string = 'ISO';
  readonly Code: string = 'Code';
  readonly Caption: string = 'Caption';

  //#endregion

  //#region Enumeration

  readonly English: OcrLanguages = new OcrLanguages(2, 'eng', 'en', 'English');
  readonly Slovenian: OcrLanguages = new OcrLanguages(3, 'slv', 'sl', 'Slovenian');
  readonly Russian: OcrLanguages = new OcrLanguages(1, 'rus', 'ru', 'Russian');
  readonly Auto: OcrLanguages = new OcrLanguages(0, null, null, 'Auto');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region OcrLanguages Enumeration

class OcrLanguages {
  readonly ID: number;
  readonly ISO: string | null;
  readonly Code: string | null;
  readonly Caption: string | null;

  constructor (ID: number, ISO: string | null, Code: string | null, Caption: string | null) {
    this.ID = ID;
    this.ISO = ISO;
    this.Code = Code;
    this.Caption = Caption;
  }
}

//#endregion

//#endregion

//#region OcrMappingComplexFields

/**
 * ID: {e8135496-a897-44b9-bc24-9214646453fe}
 * Alias: OcrMappingComplexFields
 * Group: Ocr
 * Description: Parameters for mapping verified complex fields
 */
class OcrMappingComplexFieldsSchemeInfo {
  private readonly name: string = 'OcrMappingComplexFields';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly Field: string = 'Field';
  readonly Value: string = 'Value';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrMappingFields

/**
 * ID: {f60796c1-b504-424d-93c8-2fd5b9107867}
 * Alias: OcrMappingFields
 * Group: Ocr
 * Description: Parameters for mapping verified fields
 */
class OcrMappingFieldsSchemeInfo {
  private readonly name: string = 'OcrMappingFields';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Section: string = 'Section';
  readonly Field: string = 'Field';
  readonly Displayed: string = 'Displayed';
  readonly Value: string = 'Value';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrMappingSettingsFields

/**
 * ID: {46a9cc0e-9492-48ce-8ac8-bc04551a041a}
 * Alias: OcrMappingSettingsFields
 * Group: Ocr
 * Description: Field settings by section for mapping during verification
 */
class OcrMappingSettingsFieldsSchemeInfo {
  private readonly name: string = 'OcrMappingSettingsFields';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly FieldID: string = 'FieldID';
  readonly FieldName: string = 'FieldName';
  readonly FieldComplexColumnIndex: string = 'FieldComplexColumnIndex';
  readonly Caption: string = 'Caption';
  readonly ViewRefSection: string = 'ViewRefSection';
  readonly ViewParameter: string = 'ViewParameter';
  readonly ViewReferencePrefix: string = 'ViewReferencePrefix';
  readonly ViewAlias: string = 'ViewAlias';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrMappingSettingsSections

/**
 * ID: {3a7dbf8d-2f25-4b98-a406-2582bfeee594}
 * Alias: OcrMappingSettingsSections
 * Group: Ocr
 * Description: Section settings by card type for mapping fields during verification
 */
class OcrMappingSettingsSectionsSchemeInfo {
  private readonly name: string = 'OcrMappingSettingsSections';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly SectionID: string = 'SectionID';
  readonly SectionName: string = 'SectionName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrMappingSettingsTypes

/**
 * ID: {6f34e6c5-93e9-49ed-ad42-63d8a001ded7}
 * Alias: OcrMappingSettingsTypes
 * Group: Ocr
 * Description: Card type settings for mapping fields during verification
 */
class OcrMappingSettingsTypesSchemeInfo {
  private readonly name: string = 'OcrMappingSettingsTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly TypeName: string = 'TypeName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrMappingSettingsVirtual

/**
 * ID: {0c83bfa8-8e9a-454d-b805-6de0c679f4ec}
 * Alias: OcrMappingSettingsVirtual
 * Group: Ocr
 * Description: Virtual table for mapping settings
 */
class OcrMappingSettingsVirtualSchemeInfo {
  private readonly name: string = 'OcrMappingSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly TypeID: string = 'TypeID';
  readonly SectionID: string = 'SectionID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrOperations

/**
 * ID: {2fb28ad4-1b86-4d22-bc15-2a4943a0bb7f}
 * Alias: OcrOperations
 * Group: Ocr
 * Description: Information on text recognition operations
 */
class OcrOperationsSchemeInfo {
  private readonly name: string = 'OcrOperations';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CardID: string = 'CardID';
  readonly CardTypeID: string = 'CardTypeID';
  readonly CardTypeName: string = 'CardTypeName';
  readonly FileID: string = 'FileID';
  readonly FileName: string = 'FileName';
  readonly FileHasText: string = 'FileHasText';
  readonly FileTypeID: string = 'FileTypeID';
  readonly FileTypeName: string = 'FileTypeName';
  readonly VersionRowID: string = 'VersionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrPatternTypes

/**
 * ID: {ff8635f6-7856-45e1-82cf-6f00315ce790}
 * Alias: OcrPatternTypes
 * Group: Ocr
 * Description: Template types for field verification
 */
class OcrPatternTypesSchemeInfo {
  private readonly name: string = 'OcrPatternTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Boolean: OcrPatternTypes = new OcrPatternTypes(0, 'Boolean');
  readonly Integer: OcrPatternTypes = new OcrPatternTypes(1, 'Integer');
  readonly Double: OcrPatternTypes = new OcrPatternTypes(2, 'Double');
  readonly DateTime: OcrPatternTypes = new OcrPatternTypes(3, 'DateTime');
  readonly Date: OcrPatternTypes = new OcrPatternTypes(4, 'Date');
  readonly Time: OcrPatternTypes = new OcrPatternTypes(5, 'Time');
  readonly Interval: OcrPatternTypes = new OcrPatternTypes(6, 'Interval');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region OcrPatternTypes Enumeration

class OcrPatternTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region OcrRecognitionModes

/**
 * ID: {b9a84cff-8153-4a81-b9f3-832d59461596}
 * Alias: OcrRecognitionModes
 * Group: Ocr
 * Description: Text recognition modes
 */
class OcrRecognitionModesSchemeInfo {
  private readonly name: string = 'OcrRecognitionModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Description: string = 'Description';

  //#endregion

  //#region Enumeration

  readonly OpticalRecognition: OcrRecognitionModes = new OcrRecognitionModes(0, '$Enum_OcrRecognitionModes_OpticalRecognition', '$Enum_OcrRecognitionModes_OpticalRecognition_Description');
  readonly NeuralNetwork: OcrRecognitionModes = new OcrRecognitionModes(1, '$Enum_OcrRecognitionModes_NeuralNetwork', '$Enum_OcrRecognitionModes_NeuralNetwork_Description');
  readonly OpticalRecognitionAndNeuralNetwork: OcrRecognitionModes = new OcrRecognitionModes(2, '$Enum_OcrRecognitionModes_OpticalRecognitionAndNeuralNetwork', '$Enum_OcrRecognitionModes_OpticalRecognitionAndNeuralNetwork_Description');
  readonly Default: OcrRecognitionModes = new OcrRecognitionModes(3, '$Enum_OcrRecognitionModes_Default', '$Enum_OcrRecognitionModes_Default_Description');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region OcrRecognitionModes Enumeration

class OcrRecognitionModes {
  readonly ID: number;
  readonly Name: string | null;
  readonly Description: string | null;

  constructor (ID: number, Name: string | null, Description: string | null) {
    this.ID = ID;
    this.Name = Name;
    this.Description = Description;
  }
}

//#endregion

//#endregion

//#region OcrRequests

/**
 * ID: {d64806e9-ef31-4133-806b-670b178cc5bc}
 * Alias: OcrRequests
 * Group: Ocr
 * Description: Information on text recognition requests
 */
class OcrRequestsSchemeInfo {
  private readonly name: string = 'OcrRequests';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Created: string = 'Created';
  readonly CreatedByID: string = 'CreatedByID';
  readonly CreatedByName: string = 'CreatedByName';
  readonly StateID: string = 'StateID';
  readonly Confidence: string = 'Confidence';
  readonly Preprocess: string = 'Preprocess';
  readonly SegmentationModeID: string = 'SegmentationModeID';
  readonly SegmentationModeName: string = 'SegmentationModeName';
  readonly DetectLanguages: string = 'DetectLanguages';
  readonly ContentFileID: string = 'ContentFileID';
  readonly MetadataFileID: string = 'MetadataFileID';
  readonly IsMain: string = 'IsMain';
  readonly Overwrite: string = 'Overwrite';
  readonly DetectRotation: string = 'DetectRotation';
  readonly DetectTables: string = 'DetectTables';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrRequestsLanguages

/**
 * ID: {afee6930-bb0c-48b3-b0ac-3a1447a31d12}
 * Alias: OcrRequestsLanguages
 * Group: Ocr
 * Description: Information by languages ​​used in text recognition requests
 */
class OcrRequestsLanguagesSchemeInfo {
  private readonly name: string = 'OcrRequestsLanguages';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly LanguageID: string = 'LanguageID';
  readonly LanguageISO: string = 'LanguageISO';
  readonly LanguageCaption: string = 'LanguageCaption';
  readonly ParentRowID: string = 'ParentRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrRequestsStates

/**
 * ID: {aefacd5d-1de1-42ac-86db-ddb98035f498}
 * Alias: OcrRequestsStates
 * Group: Ocr
 * Description: Text recognition request states
 */
class OcrRequestsStatesSchemeInfo {
  private readonly name: string = 'OcrRequestsStates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Created: OcrRequestsStates = new OcrRequestsStates(0, '$Enum_OcrRequestStates_Created');
  readonly Active: OcrRequestsStates = new OcrRequestsStates(1, '$Enum_OcrRequestStates_Active');
  readonly Completed: OcrRequestsStates = new OcrRequestsStates(2, '$Enum_OcrRequestStates_Completed');
  readonly Interrupted: OcrRequestsStates = new OcrRequestsStates(3, '$Enum_OcrRequestStates_Interrupted');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region OcrRequestsStates Enumeration

class OcrRequestsStates {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region OcrResultsVirtual

/**
 * ID: {f704ecbb-e3b2-4a79-8ca8-222417f3dc4d}
 * Alias: OcrResultsVirtual
 * Group: Ocr
 * Description: Information with text recognition results
 */
class OcrResultsVirtualSchemeInfo {
  private readonly name: string = 'OcrResultsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Text: string = 'Text';
  readonly Confidence: string = 'Confidence';
  readonly Language: string = 'Language';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrSegmentationModes

/**
 * ID: {5455dc3b-79f8-4c06-8cf0-64e63919ab4a}
 * Alias: OcrSegmentationModes
 * Group: Ocr
 * Description: Image page segmentation modes for text recognition
 */
class OcrSegmentationModesSchemeInfo {
  private readonly name: string = 'OcrSegmentationModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Hidden: string = 'Hidden';

  //#endregion

  //#region Enumeration

  readonly OsdOnly: OcrSegmentationModes = new OcrSegmentationModes(0, '$Enum_OcrSegmentationModes_OsdOnly', true);
  readonly AutoOsd: OcrSegmentationModes = new OcrSegmentationModes(1, '$Enum_OcrSegmentationModes_AutoOsd', false);
  readonly Auto: OcrSegmentationModes = new OcrSegmentationModes(3, '$Enum_OcrSegmentationModes_Auto', false);
  readonly SingleColumn: OcrSegmentationModes = new OcrSegmentationModes(4, '$Enum_OcrSegmentationModes_SingleColumn', false);
  readonly SingleBlockVertText: OcrSegmentationModes = new OcrSegmentationModes(5, '$Enum_OcrSegmentationModes_SingleBlockVertText', true);
  readonly SingleBlock: OcrSegmentationModes = new OcrSegmentationModes(6, '$Enum_OcrSegmentationModes_SingleBlock', false);
  readonly SingleLine: OcrSegmentationModes = new OcrSegmentationModes(7, '$Enum_OcrSegmentationModes_SingleLine', false);
  readonly SingleWord: OcrSegmentationModes = new OcrSegmentationModes(8, '$Enum_OcrSegmentationModes_SingleWord', true);
  readonly CircleWord: OcrSegmentationModes = new OcrSegmentationModes(9, '$Enum_OcrSegmentationModes_CircleWord', true);
  readonly SingleChar: OcrSegmentationModes = new OcrSegmentationModes(10, '$Enum_OcrSegmentationModes_SingleChar', true);
  readonly SparseText: OcrSegmentationModes = new OcrSegmentationModes(11, '$Enum_OcrSegmentationModes_SparseText', false);
  readonly SparseTextOsd: OcrSegmentationModes = new OcrSegmentationModes(12, '$Enum_OcrSegmentationModes_SparseTextOsd', false);
  readonly RawLine: OcrSegmentationModes = new OcrSegmentationModes(13, '$Enum_OcrSegmentationModes_RawLine', true);
  readonly AutoOnly: OcrSegmentationModes = new OcrSegmentationModes(2, '$Enum_OcrSegmentationModes_AutoOnly', true);

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region OcrSegmentationModes Enumeration

class OcrSegmentationModes {
  readonly ID: number;
  readonly Name: string | null;
  readonly Hidden: boolean;

  constructor (ID: number, Name: string | null, Hidden: boolean) {
    this.ID = ID;
    this.Name = Name;
    this.Hidden = Hidden;
  }
}

//#endregion

//#endregion

//#region OcrSettings

/**
 * ID: {4463ae11-e603-4daa-8b93-2e4323abef37}
 * Alias: OcrSettings
 * Group: Ocr
 * Description: Text recognition settings
 */
class OcrSettingsSchemeInfo {
  private readonly name: string = 'OcrSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly IsEnabled: string = 'IsEnabled';
  readonly BaseAddress: string = 'BaseAddress';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrSettingsPatterns

/**
 * ID: {e432240e-e30a-4568-aa91-bb9a9e55ea61}
 * Alias: OcrSettingsPatterns
 * Group: Ocr
 * Description: Templates for fields verification
 */
class OcrSettingsPatternsSchemeInfo {
  private readonly name: string = 'OcrSettingsPatterns';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeName: string = 'TypeName';
  readonly Value: string = 'Value';
  readonly Order: string = 'Order';
  readonly Description: string = 'Description';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OnlyOfficeFileCache

/**
 * ID: {8c151402-bae9-41a6-864f-f7558bd88c86}
 * Alias: OnlyOfficeFileCache
 * Group: OnlyOffice
 */
class OnlyOfficeFileCacheSchemeInfo {
  private readonly name: string = 'OnlyOfficeFileCache';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CreatedByID: string = 'CreatedByID';
  readonly SourceFileVersionID: string = 'SourceFileVersionID';
  readonly SourceFileName: string = 'SourceFileName';
  readonly ModifiedFileUrl: string = 'ModifiedFileUrl';
  readonly LastModifiedFileUrlTime: string = 'LastModifiedFileUrlTime';
  readonly LastAccessTime: string = 'LastAccessTime';
  readonly HasChangesAfterClose: string = 'HasChangesAfterClose';
  readonly EditorWasOpen: string = 'EditorWasOpen';
  readonly CoeditKey: string = 'CoeditKey';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OnlyOfficeSettings

/**
 * ID: {703ad2d1-6246-4d47-a0ef-814b9466c027}
 * Alias: OnlyOfficeSettings
 * Group: OnlyOffice
 */
class OnlyOfficeSettingsSchemeInfo {
  private readonly name: string = 'OnlyOfficeSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ApiScriptUrl: string = 'ApiScriptUrl';
  readonly ConverterUrl: string = 'ConverterUrl';
  readonly PreviewEnabled: string = 'PreviewEnabled';
  readonly ExcludedPreviewFormats: string = 'ExcludedPreviewFormats';
  readonly DocumentBuilderPath: string = 'DocumentBuilderPath';
  readonly WebApiBasePath: string = 'WebApiBasePath';
  readonly LoadTimeoutPeriod: string = 'LoadTimeoutPeriod';
  readonly TokenLifetimePeriod: string = 'TokenLifetimePeriod';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Operations

/**
 * ID: {4ae0856c-dd1d-4da8-80b4-e6d232be8d94}
 * Alias: Operations
 * Group: System
 * Description: Статическая часть информации об операциях
 */
class OperationsSchemeInfo {
  private readonly name: string = 'Operations';

  //#region Columns

  readonly ID: string = 'ID';
  readonly TypeID: string = 'TypeID';
  readonly CreatedByID: string = 'CreatedByID';
  readonly CreatedByName: string = 'CreatedByName';
  readonly Created: string = 'Created';
  readonly Digest: string = 'Digest';
  readonly Request: string = 'Request';
  readonly RequestHash: string = 'RequestHash';
  readonly Postponed: string = 'Postponed';
  readonly SessionID: string = 'SessionID';
  readonly CreationFlags: string = 'CreationFlags';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OperationStates

/**
 * ID: {e726339c-e2fc-4d7c-a9b4-011577ff2106}
 * Alias: OperationStates
 * Group: System
 * Description: Состояние операции.
 */
class OperationStatesSchemeInfo {
  private readonly name: string = 'OperationStates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Created: OperationStates = new OperationStates(0, '$Enum_OperationStates_Created');
  readonly InProgress: OperationStates = new OperationStates(1, '$Enum_OperationStates_InProgress');
  readonly Completed: OperationStates = new OperationStates(2, '$Enum_OperationStates_Completed');
  readonly Postponed: OperationStates = new OperationStates(3, '$Enum_OperationStates_Postponed');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region OperationStates Enumeration

class OperationStates {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region OperationsVirtual

/**
 * ID: {a87294b1-0c66-41bc-98e8-765dfb8dcf56}
 * Alias: OperationsVirtual
 * Group: System
 * Description: Виртуальная карточка "Операция". Колонки заполняются из таблиц Operations, OperationUpdates и HSET Redis'a
 */
class OperationsVirtualSchemeInfo {
  private readonly name: string = 'OperationsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly TypeID: string = 'TypeID';
  readonly TypeName: string = 'TypeName';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';
  readonly CreatedByID: string = 'CreatedByID';
  readonly CreatedByName: string = 'CreatedByName';
  readonly Created: string = 'Created';
  readonly InProgress: string = 'InProgress';
  readonly Completed: string = 'Completed';
  readonly Progress: string = 'Progress';
  readonly Digest: string = 'Digest';
  readonly Request: string = 'Request';
  readonly RequestJson: string = 'RequestJson';
  readonly Response: string = 'Response';
  readonly ResponseJson: string = 'ResponseJson';
  readonly OperationID: string = 'OperationID';
  readonly SessionID: string = 'SessionID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OperationTypes

/**
 * ID: {b23fccd5-5ba1-45b6-a0ad-e9d0cf730da0}
 * Alias: OperationTypes
 * Group: System
 * Description: Типы операций
 */
class OperationTypesSchemeInfo {
  private readonly name: string = 'OperationTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly SavingCard: OperationTypes = new OperationTypes('033e01ef-5913-4588-80c0-61567323577d', '$Enum_OperationTypes_SavingCard');
  readonly Unnamed: OperationTypes = new OperationTypes('00000000-0000-0000-0000-000000000000', '$Enum_OperationTypes_Unnamed');
  readonly CalculatingCalendar: OperationTypes = new OperationTypes('f3bd681e-861d-4820-8fa5-b2443b20dbba', '$Enum_OperationTypes_CalculatingCalendar');
  readonly ImportingCard: OperationTypes = new OperationTypes('2a7364c5-d927-4b50-8c67-02d241765e5f', '$Enum_OperationTypes_ImportingCard');
  readonly FileConvert: OperationTypes = new OperationTypes('730e24fd-52ee-4bfc-9ce0-9daf3283fabe', '$Enum_OperationTypes_FileConvert');
  readonly CalculatingRoles: OperationTypes = new OperationTypes('de7d7a67-fa4b-4c80-a7f2-b9e45095f6c8', '$Enum_OperationTypes_CalculatingRoles');
  readonly CalculatingAD: OperationTypes = new OperationTypes('9d7d2fe7-6c05-42c9-a32d-63076c1b5cef', '$Enum_OperationTypes_CalculatingAD');
  readonly WorkflowEngineAsync: OperationTypes = new OperationTypes('de6c8d23-53e2-4659-b43c-0eea4f0fec19', '$Enum_OperationTypes_WorkflowEngineAsync');
  readonly SendingForumsNotifications: OperationTypes = new OperationTypes('333ee6b8-6468-4e0e-9ac0-c73db83919dc', '$Enum_OperationTypes_SendingForumsNotifications');
  readonly AclCalculation: OperationTypes = new OperationTypes('6d10f621-d73b-449a-baf2-68be18c60689', '$Enum_OperationTypes_AclCalculation');
  readonly CalculatingSmartRoles: OperationTypes = new OperationTypes('0b663cca-c724-404f-8300-40d2b60392c2', '$Enum_OperationTypes_CalculatingSmartRoles');
  readonly DeferredDeletion: OperationTypes = new OperationTypes('06e3df9e-0820-4ee4-b32c-fa6c3218e99b', '$Enum_OperationTypes_DeferredDeletion');
  readonly TextRecognition: OperationTypes = new OperationTypes('b8f6298c-2d53-446f-9007-7849ef050b5e', '$Enum_OperationTypes_TextRecognition');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region OperationTypes Enumeration

class OperationTypes {
  readonly ID: guid;
  readonly Name: string | null;

  constructor (ID: guid, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region OperationUpdates

/**
 * ID: {a6435575-79d4-44b6-b755-b9a026431556}
 * Alias: OperationUpdates
 * Group: System
 * Description: Обновляемая часть информации об операциях
 */
class OperationUpdatesSchemeInfo {
  private readonly name: string = 'OperationUpdates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly StateID: string = 'StateID';
  readonly InProgress: string = 'InProgress';
  readonly Completed: string = 'Completed';
  readonly Response: string = 'Response';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Outbox

/**
 * ID: {b4412a23-3e36-4468-a9cf-6b7b553f9e64}
 * Alias: Outbox
 * Group: System
 */
class OutboxSchemeInfo {
  private readonly name: string = 'Outbox';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Created: string = 'Created';
  readonly Email: string = 'Email';
  readonly Subject: string = 'Subject';
  readonly Body: string = 'Body';
  readonly Attempts: string = 'Attempts';
  readonly LastErrorDate: string = 'LastErrorDate';
  readonly LastErrorText: string = 'LastErrorText';
  readonly Info: string = 'Info';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OutgoingRefDocs

/**
 * ID: {73320234-fc44-4126-a7a6-5dd0bdaa4880}
 * Alias: OutgoingRefDocs
 * Group: Common
 */
class OutgoingRefDocsSchemeInfo {
  private readonly name: string = 'OutgoingRefDocs';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly DocID: string = 'DocID';
  readonly DocDescription: string = 'DocDescription';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Partitions

/**
 * ID: {5ca00fac-d04e-4b82-8139-9778518e00bf}
 * Alias: Partitions
 * Group: System
 */
class PartitionsSchemeInfo {
  private readonly name: string = 'Partitions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Definition: string = 'Definition';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Partners

/**
 * ID: {5d47ef13-b6f4-47ef-9815-3b3d0e6d475a}
 * Alias: Partners
 * Group: Common
 * Description: Контрагенты
 */
class PartnersSchemeInfo {
  private readonly name: string = 'Partners';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly FullName: string = 'FullName';
  readonly LegalAddress: string = 'LegalAddress';
  readonly Phone: string = 'Phone';
  readonly Head: string = 'Head';
  readonly ChiefAccountant: string = 'ChiefAccountant';
  readonly ContactPerson: string = 'ContactPerson';
  readonly Email: string = 'Email';
  readonly ContactAddress: string = 'ContactAddress';
  readonly INN: string = 'INN';
  readonly KPP: string = 'KPP';
  readonly OGRN: string = 'OGRN';
  readonly OKPO: string = 'OKPO';
  readonly OKVED: string = 'OKVED';
  readonly Comment: string = 'Comment';
  readonly Bank: string = 'Bank';
  readonly SettlementAccount: string = 'SettlementAccount';
  readonly BIK: string = 'BIK';
  readonly CorrAccount: string = 'CorrAccount';
  readonly TypeID: string = 'TypeID';
  readonly TypeName: string = 'TypeName';
  readonly VatTypeID: string = 'VatTypeID';
  readonly VatTypeName: string = 'VatTypeName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PartnersContacts

/**
 * ID: {c57f5563-6673-4ca0-83a1-2896dbd090e1}
 * Alias: PartnersContacts
 * Group: Common
 * Description: Контактные лица
 */
class PartnersContactsSchemeInfo {
  private readonly name: string = 'PartnersContacts';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';
  readonly Department: string = 'Department';
  readonly Position: string = 'Position';
  readonly PhoneNumber: string = 'PhoneNumber';
  readonly Email: string = 'Email';
  readonly Comment: string = 'Comment';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PartnersTypes

/**
 * ID: {354e4f5a-e50c-4a11-84d0-6e0a98a81ca5}
 * Alias: PartnersTypes
 * Group: Common
 */
class PartnersTypesSchemeInfo {
  private readonly name: string = 'PartnersTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly LegalEntity: PartnersTypes = new PartnersTypes(1, '$PartnerType_LegalEntity');
  readonly Individual: PartnersTypes = new PartnersTypes(2, '$PartnerType_Individual');
  readonly SoleTrader: PartnersTypes = new PartnersTypes(3, '$PartnerType_SoleTrader');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region PartnersTypes Enumeration

class PartnersTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region Performers

/**
 * ID: {d0f5547b-b2f5-4a08-8cd9-b34138d35125}
 * Alias: Performers
 * Group: Common
 * Description: Исполнители
 */
class PerformersSchemeInfo {
  private readonly name: string = 'Performers';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PersonalLicenses

/**
 * ID: {8a96f177-b2a7-4ffc-885b-2f36730d0d2e}
 * Alias: PersonalLicenses
 * Group: System
 * Description: Сотрудники, для которых указаны персональные лицензии.
 */
class PersonalLicensesSchemeInfo {
  private readonly name: string = 'PersonalLicenses';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PersonalRoleDepartmentsVirtual

/**
 * ID: {21566803-b822-42b2-ab11-2c20e72a0de4}
 * Alias: PersonalRoleDepartmentsVirtual
 * Group: Roles
 * Description: Таблица для отображения и редактирования департаментов, в которые входит сотрудник.
 */
class PersonalRoleDepartmentsVirtualSchemeInfo {
  private readonly name: string = 'PersonalRoleDepartmentsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly DepartmentID: string = 'DepartmentID';
  readonly DepartmentName: string = 'DepartmentName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PersonalRoleNotificationRulesVirtual

/**
 * ID: {925a75d4-639f-4467-9155-c8e21f5433a9}
 * Alias: PersonalRoleNotificationRulesVirtual
 * Group: Roles
 */
class PersonalRoleNotificationRulesVirtualSchemeInfo {
  private readonly name: string = 'PersonalRoleNotificationRulesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';
  readonly Disallow: string = 'Disallow';
  readonly AllowanceType: string = 'AllowanceType';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PersonalRoleNotificationRuleTypesVirtual

/**
 * ID: {16baecaa-9088-4635-af93-4c042893bf1d}
 * Alias: PersonalRoleNotificationRuleTypesVirtual
 * Group: Roles
 */
class PersonalRoleNotificationRuleTypesVirtualSchemeInfo {
  private readonly name: string = 'PersonalRoleNotificationRuleTypesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly NotificationTypeID: string = 'NotificationTypeID';
  readonly NotificationTypeName: string = 'NotificationTypeName';
  readonly RuleRowID: string = 'RuleRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PersonalRoleRolesVirtual

/**
 * ID: {e631fc2a-7628-4d7e-a118-99d310fa12b8}
 * Alias: PersonalRoleRolesVirtual
 * Group: Roles
 * Description: Таблица для отображения всех ролей (кроме своей персональной роли), в которые входит сотрудник.
 */
class PersonalRoleRolesVirtualSchemeInfo {
  private readonly name: string = 'PersonalRoleRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PersonalRoles

/**
 * ID: {6c977939-bbfc-456f-a133-f1c2244e3cc3}
 * Alias: PersonalRoles
 * Group: Roles
 * Description: Employees.
 */
class PersonalRolesSchemeInfo {
  private readonly name: string = 'PersonalRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly FullName: string = 'FullName';
  readonly LastName: string = 'LastName';
  readonly FirstName: string = 'FirstName';
  readonly MiddleName: string = 'MiddleName';
  readonly Position: string = 'Position';
  readonly BirthDate: string = 'BirthDate';
  readonly Email: string = 'Email';
  readonly Fax: string = 'Fax';
  readonly Phone: string = 'Phone';
  readonly MobilePhone: string = 'MobilePhone';
  readonly HomePhone: string = 'HomePhone';
  readonly IPPhone: string = 'IPPhone';
  readonly Login: string = 'Login';
  readonly PasswordKey: string = 'PasswordKey';
  readonly PasswordHash: string = 'PasswordHash';
  readonly AccessLevelID: string = 'AccessLevelID';
  readonly AccessLevelName: string = 'AccessLevelName';
  readonly LoginTypeID: string = 'LoginTypeID';
  readonly LoginTypeName: string = 'LoginTypeName';
  readonly Security: string = 'Security';
  readonly Blocked: string = 'Blocked';
  readonly BlockedDueDate: string = 'BlockedDueDate';
  readonly PasswordChanged: string = 'PasswordChanged';
  readonly ApplicationArchitectureID: string = 'ApplicationArchitectureID';
  readonly ApplicationArchitectureName: string = 'ApplicationArchitectureName';
  readonly CipherInfo: string = 'CipherInfo';
  readonly ExternalUid: string = 'ExternalUid';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PersonalRoleSatellite

/**
 * ID: {62fd7bdd-0fc1-4370-afd6-54ac7e5320b4}
 * Alias: PersonalRoleSatellite
 * Group: Roles
 * Description: Сателлит с настройками пользователя.
 */
class PersonalRoleSatelliteSchemeInfo {
  private readonly name: string = 'PersonalRoleSatellite';

  //#region Columns

  readonly ID: string = 'ID';
  readonly LanguageID: string = 'LanguageID';
  readonly LanguageCaption: string = 'LanguageCaption';
  readonly LanguageCode: string = 'LanguageCode';
  readonly FormatName: string = 'FormatName';
  readonly Settings: string = 'Settings';
  readonly FilePreviewPosition: string = 'FilePreviewPosition';
  readonly FilePreviewIsHidden: string = 'FilePreviewIsHidden';
  readonly FilePreviewWidthRatio: string = 'FilePreviewWidthRatio';
  readonly TaskAreaWidth: string = 'TaskAreaWidth';
  readonly ContentWidthRatio: string = 'ContentWidthRatio';
  readonly WebTheme: string = 'WebTheme';
  readonly WebWallpaper: string = 'WebWallpaper';
  readonly WorkplaceExtensions: string = 'WorkplaceExtensions';
  readonly NotificationSettings: string = 'NotificationSettings';
  readonly UserSettingsLastUpdate: string = 'UserSettingsLastUpdate';
  readonly ForumSettings: string = 'ForumSettings';
  readonly WebDefaultWallpaper: string = 'WebDefaultWallpaper';
  readonly CardSettings: string = 'CardSettings';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PersonalRoleStaticRolesVirtual

/**
 * ID: {122da60d-3efc-42a2-bd84-510c0819807b}
 * Alias: PersonalRoleStaticRolesVirtual
 * Group: Roles
 * Description: Таблица для отображения и редактирования статических ролей, в которые входит сотрудник.
 */
class PersonalRoleStaticRolesVirtualSchemeInfo {
  private readonly name: string = 'PersonalRoleStaticRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PersonalRoleSubscribedTypesVirtual

/**
 * ID: {62859264-a143-4ec0-a86b-bba80f6f61ac}
 * Alias: PersonalRoleSubscribedTypesVirtual
 * Group: Roles
 * Description: Секция со всеми глобальными типами уведомлений, на которые подписался пользователь.
 */
class PersonalRoleSubscribedTypesVirtualSchemeInfo {
  private readonly name: string = 'PersonalRoleSubscribedTypesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly NotificationTypeID: string = 'NotificationTypeID';
  readonly NotificationTypeName: string = 'NotificationTypeName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PersonalRolesVirtual

/**
 * ID: {e86b07e5-da20-487b-a55e-0ed56606bddf}
 * Alias: PersonalRolesVirtual
 * Group: Roles
 * Description: Виртуальные поля для карточки сотрудника.
 */
class PersonalRolesVirtualSchemeInfo {
  private readonly name: string = 'PersonalRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly LanguageID: string = 'LanguageID';
  readonly LanguageCaption: string = 'LanguageCaption';
  readonly LanguageCode: string = 'LanguageCode';
  readonly FormatID: string = 'FormatID';
  readonly FormatName: string = 'FormatName';
  readonly FormatCaption: string = 'FormatCaption';
  readonly Password: string = 'Password';
  readonly PasswordRepeat: string = 'PasswordRepeat';
  readonly Settings: string = 'Settings';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region PersonalRoleUnsubscibedTypesVirtual

/**
 * ID: {fc4566ea-029b-4d37-b3f0-4ca62a4cb500}
 * Alias: PersonalRoleUnsubscibedTypesVirtual
 * Group: Roles
 * Description: Секция со всеми глобальными типами уведомлений, от которых отписался пользователь.
 */
class PersonalRoleUnsubscibedTypesVirtualSchemeInfo {
  private readonly name: string = 'PersonalRoleUnsubscibedTypesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly NotificationTypeID: string = 'NotificationTypeID';
  readonly NotificationTypeName: string = 'NotificationTypeName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Procedures

/**
 * ID: {1bf6a3b2-725a-487c-b4d8-6b082fb56037}
 * Alias: Procedures
 * Group: System
 * Description: Contains metadata that describes tables which used by Tessa
 */
class ProceduresSchemeInfo {
  private readonly name: string = 'Procedures';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Definition: string = 'Definition';

  //#endregion

  //#region Procedures


  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ProtocolDecisions

/**
 * ID: {91c272de-462d-4076-8f64-592885a4abd4}
 * Alias: ProtocolDecisions
 * Group: Common
 * Description: Решения по протоколу.
 */
class ProtocolDecisionsSchemeInfo {
  private readonly name: string = 'ProtocolDecisions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Question: string = 'Question';
  readonly Planned: string = 'Planned';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ProtocolReports

/**
 * ID: {5576e1f1-316a-4256-a136-c33eb871b7d5}
 * Alias: ProtocolReports
 * Group: Common
 */
class ProtocolReportsSchemeInfo {
  private readonly name: string = 'ProtocolReports';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Subject: string = 'Subject';
  readonly Order: string = 'Order';
  readonly PersonID: string = 'PersonID';
  readonly PersonName: string = 'PersonName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ProtocolResponsibles

/**
 * ID: {34e972b7-fd6f-4417-99d1-f2578a82ab1d}
 * Alias: ProtocolResponsibles
 * Group: Common
 */
class ProtocolResponsiblesSchemeInfo {
  private readonly name: string = 'ProtocolResponsibles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly ParentRowID: string = 'ParentRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Protocols

/**
 * ID: {b98383dc-ecf0-4ad0-b92d-dd599775b8f5}
 * Alias: Protocols
 * Group: Common
 * Description: Протоколы.
 */
class ProtocolsSchemeInfo {
  private readonly name: string = 'Protocols';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ProtocolFileID: string = 'ProtocolFileID';
  readonly Date: string = 'Date';
  readonly Agenda: string = 'Agenda';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Recipients

/**
 * ID: {386509d9-4130-467f-9a52-0004aa15247e}
 * Alias: Recipients
 * Group: Common
 * Description: Получатели
 */
class RecipientsSchemeInfo {
  private readonly name: string = 'Recipients';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ReportRolesActive

/**
 * ID: {fd37a3c0-33e5-4256-98bf-4440402f4116}
 * Alias: ReportRolesActive
 * Group: Common
 * Description: Роли, которые могут смотреть отчёты по текущим заданиям
 */
class ReportRolesActiveSchemeInfo {
  private readonly name: string = 'ReportRolesActive';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ReportRolesPassive

/**
 * ID: {599f50f0-95c4-48ad-a739-c54fd9b5f829}
 * Alias: ReportRolesPassive
 * Group: Common
 * Description: Роли, которых могут смотреть отчёты по текущим заданиям
 */
class ReportRolesPassiveSchemeInfo {
  private readonly name: string = 'ReportRolesPassive';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ReportRolesRules

/**
 * ID: {359edaf2-fdb7-4e24-afc8-f31281328a81}
 * Alias: ReportRolesRules
 * Group: Common
 */
class ReportRolesRulesSchemeInfo {
  private readonly name: string = 'ReportRolesRules';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Caption: string = 'Caption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputies

/**
 * ID: {900bdbcd-1e87-451c-8b4b-082d8f7efd48}
 * Alias: RoleDeputies
 * Group: Roles
 * Description: Заместители.
 */
class RoleDeputiesSchemeInfo {
  private readonly name: string = 'RoleDeputies';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly DeputyID: string = 'DeputyID';
  readonly DeputyName: string = 'DeputyName';
  readonly DeputizedID: string = 'DeputizedID';
  readonly DeputizedName: string = 'DeputizedName';
  readonly MinDate: string = 'MinDate';
  readonly MaxDate: string = 'MaxDate';
  readonly IsActive: string = 'IsActive';
  readonly IsEnabled: string = 'IsEnabled';
  readonly ManagementRowID: string = 'ManagementRowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly Level: string = 'Level';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesManagement

/**
 * ID: {0f489948-bc16-42a6-8953-b92100807296}
 * Alias: RoleDeputiesManagement
 * Group: Roles
 * Description: Основные записи секции "Мои замещения"
 */
class RoleDeputiesManagementSchemeInfo {
  private readonly name: string = 'RoleDeputiesManagement';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly MinDate: string = 'MinDate';
  readonly MaxDate: string = 'MaxDate';
  readonly IsActive: string = 'IsActive';
  readonly IsEnabled: string = 'IsEnabled';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesManagementAccess

/**
 * ID: {edbc91b1-dd36-43c2-867a-67c74ed7f403}
 * Alias: RoleDeputiesManagementAccess
 * Group: Roles
 * Description: Сотрудники, которые могут редактировать секции "Мои замещения"
 */
class RoleDeputiesManagementAccessSchemeInfo {
  private readonly name: string = 'RoleDeputiesManagementAccess';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly PersonalRoleID: string = 'PersonalRoleID';
  readonly PersonalRoleName: string = 'PersonalRoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesManagementDeputizedRolesVirtual

/**
 * ID: {997561ee-218f-4f22-946b-87a78755c3e6}
 * Alias: RoleDeputiesManagementDeputizedRolesVirtual
 * Group: Roles
 * Description: Список ролей, который относится к каждой строке в таблице с замещаемыми сотрудниками RoleDeputiesManagementDeputizedVirtual.
 */
class RoleDeputiesManagementDeputizedRolesVirtualSchemeInfo {
  private readonly name: string = 'RoleDeputiesManagementDeputizedRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesManagementDeputizedVirtual

/**
 * ID: {c55a7921-d82d-4f8b-b801-f1c693c4c2e3}
 * Alias: RoleDeputiesManagementDeputizedVirtual
 * Group: Roles
 * Description: Таблица, в которой перечислены замещаемые сотрудники и параметры замещения.
 */
class RoleDeputiesManagementDeputizedVirtualSchemeInfo {
  private readonly name: string = 'RoleDeputiesManagementDeputizedVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly DeputizedID: string = 'DeputizedID';
  readonly DeputizedName: string = 'DeputizedName';
  readonly MinDate: string = 'MinDate';
  readonly MaxDate: string = 'MaxDate';
  readonly IsActive: string = 'IsActive';
  readonly IsEnabled: string = 'IsEnabled';
  readonly IsPermanent: string = 'IsPermanent';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesManagementHelperVirtual

/**
 * ID: {81f28cf8-709b-4dde-8c9e-505d3d7870e0}
 * Alias: RoleDeputiesManagementHelperVirtual
 * Group: Roles
 */
class RoleDeputiesManagementHelperVirtualSchemeInfo {
  private readonly name: string = 'RoleDeputiesManagementHelperVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly UserID: string = 'UserID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesManagementRoles

/**
 * ID: {91acf9b9-8476-4dc8-a239-ac6b8f250077}
 * Alias: RoleDeputiesManagementRoles
 * Group: Roles
 * Description: Роли секции "Мои замещения"
 */
class RoleDeputiesManagementRolesSchemeInfo {
  private readonly name: string = 'RoleDeputiesManagementRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesManagementRolesVirtual

/**
 * ID: {9650456c-27ea-4f62-9073-95b9be1d49ba}
 * Alias: RoleDeputiesManagementRolesVirtual
 * Group: Roles
 */
class RoleDeputiesManagementRolesVirtualSchemeInfo {
  private readonly name: string = 'RoleDeputiesManagementRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesManagementUsers

/**
 * ID: {b8f9c863-22fd-4d63-a7cf-b9f0de519b47}
 * Alias: RoleDeputiesManagementUsers
 * Group: Roles
 * Description: Сотрудники секции "Мои замещения"
 */
class RoleDeputiesManagementUsersSchemeInfo {
  private readonly name: string = 'RoleDeputiesManagementUsers';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly PersonalRoleID: string = 'PersonalRoleID';
  readonly PersonalRoleName: string = 'PersonalRoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesManagementUsersVirtual

/**
 * ID: {9dc135b0-21b8-4deb-ab65-bdda57a3fbb5}
 * Alias: RoleDeputiesManagementUsersVirtual
 * Group: Roles
 */
class RoleDeputiesManagementUsersVirtualSchemeInfo {
  private readonly name: string = 'RoleDeputiesManagementUsersVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly PersonalRoleID: string = 'PersonalRoleID';
  readonly PersonalRoleName: string = 'PersonalRoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesManagementVirtual

/**
 * ID: {79dca225-d99c-4dfd-94d9-27ed3ab15046}
 * Alias: RoleDeputiesManagementVirtual
 * Group: Roles
 */
class RoleDeputiesManagementVirtualSchemeInfo {
  private readonly name: string = 'RoleDeputiesManagementVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly MinDate: string = 'MinDate';
  readonly MaxDate: string = 'MaxDate';
  readonly IsActive: string = 'IsActive';
  readonly IsEnabled: string = 'IsEnabled';
  readonly IsPermanent: string = 'IsPermanent';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesNestedManagement

/**
 * ID: {dd329f32-adf0-4336-bd9e-fa084c0fe494}
 * Alias: RoleDeputiesNestedManagement
 * Group: Acl
 */
class RoleDeputiesNestedManagementSchemeInfo {
  private readonly name: string = 'RoleDeputiesNestedManagement';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly MinDate: string = 'MinDate';
  readonly MaxDate: string = 'MaxDate';
  readonly IsActive: string = 'IsActive';
  readonly IsEnabled: string = 'IsEnabled';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesNestedManagementTypes

/**
 * ID: {0958f50b-8fd2-4e65-9531-fd540f3150ab}
 * Alias: RoleDeputiesNestedManagementTypes
 * Group: Acl
 */
class RoleDeputiesNestedManagementTypesSchemeInfo {
  private readonly name: string = 'RoleDeputiesNestedManagementTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesNestedManagementTypesVirtual

/**
 * ID: {a8c71408-d1a3-4dbc-abcb-287dd7b7c648}
 * Alias: RoleDeputiesNestedManagementTypesVirtual
 * Group: Acl
 */
class RoleDeputiesNestedManagementTypesVirtualSchemeInfo {
  private readonly name: string = 'RoleDeputiesNestedManagementTypesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesNestedManagementUsers

/**
 * ID: {c9bf7542-de37-4fad-9cda-6b1a5a4964b7}
 * Alias: RoleDeputiesNestedManagementUsers
 * Group: Acl
 */
class RoleDeputiesNestedManagementUsersSchemeInfo {
  private readonly name: string = 'RoleDeputiesNestedManagementUsers';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly PersonalRoleID: string = 'PersonalRoleID';
  readonly PersonalRoleName: string = 'PersonalRoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesNestedManagementUsersVirtual

/**
 * ID: {6d0cfd99-aa36-4992-9231-eea478138fe6}
 * Alias: RoleDeputiesNestedManagementUsersVirtual
 * Group: Acl
 */
class RoleDeputiesNestedManagementUsersVirtualSchemeInfo {
  private readonly name: string = 'RoleDeputiesNestedManagementUsersVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly PersonalRoleID: string = 'PersonalRoleID';
  readonly PersonalRoleName: string = 'PersonalRoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesNestedManagementVirtual

/**
 * ID: {3937aa4f-0658-4e8b-a25a-911802f1fa82}
 * Alias: RoleDeputiesNestedManagementVirtual
 * Group: Acl
 */
class RoleDeputiesNestedManagementVirtualSchemeInfo {
  private readonly name: string = 'RoleDeputiesNestedManagementVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly MinDate: string = 'MinDate';
  readonly MaxDate: string = 'MaxDate';
  readonly IsEnabled: string = 'IsEnabled';
  readonly IsPermanent: string = 'IsPermanent';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleGenerators

/**
 * ID: {747bb53c-9e47-418d-892d-fb52a18eb42d}
 * Alias: RoleGenerators
 * Group: Roles
 * Description: Генераторы метаролей.
 */
class RoleGeneratorsSchemeInfo {
  private readonly name: string = 'RoleGenerators';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly SqlText: string = 'SqlText';
  readonly SchedulingTypeID: string = 'SchedulingTypeID';
  readonly CronScheduling: string = 'CronScheduling';
  readonly PeriodScheduling: string = 'PeriodScheduling';
  readonly LastErrorDate: string = 'LastErrorDate';
  readonly LastErrorText: string = 'LastErrorText';
  readonly Description: string = 'Description';
  readonly LastSuccessfulRecalcDate: string = 'LastSuccessfulRecalcDate';
  readonly ScheduleAtLaunch: string = 'ScheduleAtLaunch';
  readonly DisableDeputies: string = 'DisableDeputies';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Roles

/**
 * ID: {81f6010b-9641-4aa5-8897-b8e8603fbf4b}
 * Alias: Roles
 * Group: Roles
 * Description: Roles.
 */
class RolesSchemeInfo {
  private readonly name: string = 'Roles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly TypeID: string = 'TypeID';
  readonly ParentID: string = 'ParentID';
  readonly ParentName: string = 'ParentName';
  readonly Hidden: string = 'Hidden';
  readonly Description: string = 'Description';
  readonly AdSyncID: string = 'AdSyncID';
  readonly AdSyncDate: string = 'AdSyncDate';
  readonly AdSyncDisableUpdate: string = 'AdSyncDisableUpdate';
  readonly AdSyncIndependent: string = 'AdSyncIndependent';
  readonly AdSyncWhenChanged: string = 'AdSyncWhenChanged';
  readonly AdSyncDistinguishedName: string = 'AdSyncDistinguishedName';
  readonly AdSyncHash: string = 'AdSyncHash';
  readonly ExternalID: string = 'ExternalID';
  readonly TimeZoneID: string = 'TimeZoneID';
  readonly TimeZoneShortName: string = 'TimeZoneShortName';
  readonly TimeZoneUtcOffsetMinutes: string = 'TimeZoneUtcOffsetMinutes';
  readonly TimeZoneCodeName: string = 'TimeZoneCodeName';
  readonly InheritTimeZone: string = 'InheritTimeZone';
  readonly CalendarID: string = 'CalendarID';
  readonly CalendarName: string = 'CalendarName';
  readonly DeputiesExpired: string = 'DeputiesExpired';
  readonly DisableDeputies: string = 'DisableDeputies';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleTypes

/**
 * ID: {8d6cb6a6-c3f5-4c92-88d7-0cc6b8e8d09d}
 * Alias: RoleTypes
 * Group: Roles
 * Description: Типы ролей.
 */
class RoleTypesSchemeInfo {
  private readonly name: string = 'RoleTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly StaticRole: RoleTypes = new RoleTypes(0, '$CardTypes_TypesNames_StaticRole');
  readonly PersonalRole: RoleTypes = new RoleTypes(1, '$CardTypes_TypesNames_PersonalRole');
  readonly DepartmentRole: RoleTypes = new RoleTypes(2, '$CardTypes_TypesNames_DepartmentRole');
  readonly DynamicRole: RoleTypes = new RoleTypes(3, '$CardTypes_TypesNames_DynamicRole');
  readonly ContextRole: RoleTypes = new RoleTypes(4, '$CardTypes_TypesNames_ContextRole');
  readonly Metarole: RoleTypes = new RoleTypes(5, '$CardTypes_TypesNames_Metarole');
  readonly TaskRole: RoleTypes = new RoleTypes(6, '$CardTypes_TypesNames_TaskRole');
  readonly SmartRole: RoleTypes = new RoleTypes(7, '$CardTypes_TypesNames_SmartRole');
  readonly NestedRole: RoleTypes = new RoleTypes(8, '$CardTypes_TypesNames_NestedRole');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region RoleTypes Enumeration

class RoleTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region RoleUsers

/**
 * ID: {a3a271db-3ce6-47c7-b75e-87dcc9dc052a}
 * Alias: RoleUsers
 * Group: Roles
 * Description: Состав роли (список пользователей, включённых в роль).
 */
class RoleUsersSchemeInfo {
  private readonly name: string = 'RoleUsers';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly IsDeputy: string = 'IsDeputy';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RoleUsersVirtual

/**
 * ID: {47428527-dd1f-4e52-9ba5-a2a988abdf93}
 * Alias: RoleUsersVirtual
 * Group: Roles
 * Description: Состав роли без учёта замещений.
 */
class RoleUsersVirtualSchemeInfo {
  private readonly name: string = 'RoleUsersVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Satellites

/**
 * ID: {608289ef-42c8-4f6e-8d4f-27ef725732b5}
 * Alias: Satellites
 * Group: System
 */
class SatellitesSchemeInfo {
  private readonly name: string = 'Satellites';

  //#region Columns

  readonly ID: string = 'ID';
  readonly MainCardID: string = 'MainCardID';
  readonly TypeID: string = 'TypeID';
  readonly TaskID: string = 'TaskID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SchedulingTypes

/**
 * ID: {3cf60a31-28d4-42ad-86b2-343a298ea7a8}
 * Alias: SchedulingTypes
 * Group: Roles
 * Description: Способы указания расписания для выполнения заданий.
 */
class SchedulingTypesSchemeInfo {
  private readonly name: string = 'SchedulingTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Period: SchedulingTypes = new SchedulingTypes(0, 'Period');
  readonly Cron: SchedulingTypes = new SchedulingTypes(1, 'Cron');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region SchedulingTypes Enumeration

class SchedulingTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region Scheme

/**
 * ID: {c4fcd8d3-fcb1-451f-98f4-e352cd8a3a41}
 * Alias: Scheme
 * Group: System
 * Description: Scheme properties
 */
class SchemeSchemeInfo {
  private readonly name: string = 'Scheme';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Description: string = 'Description';
  readonly CollationSqlServer: string = 'CollationSqlServer';
  readonly CollationPostgreSql: string = 'CollationPostgreSql';
  readonly SchemeVersion: string = 'SchemeVersion';
  readonly Modified: string = 'Modified';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';
  readonly DbmsVersion: string = 'DbmsVersion';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SearchQueries

/**
 * ID: {d0dde291-0d94-4e76-9f69-902809975216}
 * Alias: SearchQueries
 * Group: System
 */
class SearchQueriesSchemeInfo {
  private readonly name: string = 'SearchQueries';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Metadata: string = 'Metadata';
  readonly ViewAlias: string = 'ViewAlias';
  readonly IsPublic: string = 'IsPublic';
  readonly LastModified: string = 'LastModified';
  readonly CreatedByUserID: string = 'CreatedByUserID';
  readonly TemplateCompositionID: string = 'TemplateCompositionID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SectionChangedCondition

/**
 * ID: {3b9d9643-ed47-4301-94b2-8eedcabc23bc}
 * Alias: SectionChangedCondition
 * Group: Acl
 * Description: Секция для условий, проверяющих изменение секции.
 */
class SectionChangedConditionSchemeInfo {
  private readonly name: string = 'SectionChangedCondition';

  //#region Columns

  readonly ID: string = 'ID';
  readonly SectionID: string = 'SectionID';
  readonly SectionName: string = 'SectionName';
  readonly SectionTypeID: string = 'SectionTypeID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SequencesInfo

/**
 * ID: {f113a406-970b-4c1b-820f-9d960c37692a}
 * Alias: SequencesInfo
 * Group: System
 */
class SequencesInfoSchemeInfo {
  private readonly name: string = 'SequencesInfo';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SequencesIntervals

/**
 * ID: {510bf28c-bccf-4701-b9fa-1081c22c2ef9}
 * Alias: SequencesIntervals
 * Group: System
 */
class SequencesIntervalsSchemeInfo {
  private readonly name: string = 'SequencesIntervals';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly First: string = 'First';
  readonly Last: string = 'Last';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SequencesReserved

/**
 * ID: {506e2fe6-397e-45c1-ae35-22cd7e85b14d}
 * Alias: SequencesReserved
 * Group: System
 */
class SequencesReservedSchemeInfo {
  private readonly name: string = 'SequencesReserved';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Number: string = 'Number';
  readonly Date: string = 'Date';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ServerInstances

/**
 * ID: {c3d76e97-459f-41e0-8d45-56fb19b5e07e}
 * Alias: ServerInstances
 * Group: System
 * Description: Таблица с настройками базы данных.
 */
class ServerInstancesSchemeInfo {
  private readonly name: string = 'ServerInstances';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Description: string = 'Description';
  readonly DefaultFileSourceID: string = 'DefaultFileSourceID';
  readonly WebAddress: string = 'WebAddress';
  readonly MobileApprovalEmail: string = 'MobileApprovalEmail';
  readonly ViewGetDataCommandTimeout: string = 'ViewGetDataCommandTimeout';
  readonly FileExtensionsWithoutPreview: string = 'FileExtensionsWithoutPreview';
  readonly FileExtensionsConvertablePreview: string = 'FileExtensionsConvertablePreview';
  readonly DenyMobileFileDownload: string = 'DenyMobileFileDownload';
  readonly BlockWindowsAndLdapUsers: string = 'BlockWindowsAndLdapUsers';
  readonly CsvEncoding: string = 'CsvEncoding';
  readonly CsvSeparator: string = 'CsvSeparator';
  readonly MaxFailedLoginAttemptsBeforeBlocked: string = 'MaxFailedLoginAttemptsBeforeBlocked';
  readonly MaxFailedLoginAttemptsInSeries: string = 'MaxFailedLoginAttemptsInSeries';
  readonly BlockedSeriesDueDateHours: string = 'BlockedSeriesDueDateHours';
  readonly FailedLoginAttemptsSeriesTime: string = 'FailedLoginAttemptsSeriesTime';
  readonly SessionInactivityHours: string = 'SessionInactivityHours';
  readonly MinPasswordLength: string = 'MinPasswordLength';
  readonly EnforceStrongPasswords: string = 'EnforceStrongPasswords';
  readonly PasswordExpirationDays: string = 'PasswordExpirationDays';
  readonly PasswordExpirationNotificationDays: string = 'PasswordExpirationNotificationDays';
  readonly UniquePasswordCount: string = 'UniquePasswordCount';
  readonly MaxFileSizeMb: string = 'MaxFileSizeMb';
  readonly LargeFileSizeMb: string = 'LargeFileSizeMb';
  readonly WebDefaultWallpaper: string = 'WebDefaultWallpaper';
  readonly ForumRefreshInterval: string = 'ForumRefreshInterval';
  readonly ModifyMessageAtNoOlderThan: string = 'ModifyMessageAtNoOlderThan';
  readonly FullTextMessageSearch: string = 'FullTextMessageSearch';
  readonly HelpUrl: string = 'HelpUrl';
  readonly UseNewDeputies: string = 'UseNewDeputies';
  readonly DefaultCalendarID: string = 'DefaultCalendarID';
  readonly DefaultCalendarName: string = 'DefaultCalendarName';
  readonly UseRemainingTimeInAstronomicalDays: string = 'UseRemainingTimeInAstronomicalDays';
  readonly ForumMaxAttachedFileSizeKb: string = 'ForumMaxAttachedFileSizeKb';
  readonly ForumMaxAttachedFiles: string = 'ForumMaxAttachedFiles';
  readonly ForumMaxMessageInlines: string = 'ForumMaxMessageInlines';
  readonly ForumMaxMessageSize: string = 'ForumMaxMessageSize';
  readonly DisableDesktopLinksInNotifications: string = 'DisableDesktopLinksInNotifications';
  readonly FileConverterTypeID: string = 'FileConverterTypeID';
  readonly FileConverterTypeName: string = 'FileConverterTypeName';
  readonly DefaultActionHistoryDatabaseID: string = 'DefaultActionHistoryDatabaseID';
  readonly DeskiDisabled: string = 'DeskiDisabled';
  readonly DeskiMobileEnabled: string = 'DeskiMobileEnabled';
  readonly DeskiMobileJwtLifeTime: string = 'DeskiMobileJwtLifeTime';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SessionActivity

/**
 * ID: {a396cc70-75bb-427b-ac42-4978fb5575ac}
 * Alias: SessionActivity
 * Group: System
 * Description: Таблица для хранения признаков активности и последней активности сессии.
 * Дополняет таблицу Sessions.
 */
class SessionActivitySchemeInfo {
  private readonly name: string = 'SessionActivity';

  //#region Columns

  readonly SessionID: string = 'SessionID';
  readonly IsActive: string = 'IsActive';
  readonly LastActivity: string = 'LastActivity';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Sessions

/**
 * ID: {bbd3d574-a33e-49fb-867d-db3c6811365e}
 * Alias: Sessions
 * Group: System
 * Description: Открытые сессии.
 */
class SessionsSchemeInfo {
  private readonly name: string = 'Sessions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ApplicationID: string = 'ApplicationID';
  readonly LicenseTypeID: string = 'LicenseTypeID';
  readonly LoginTypeID: string = 'LoginTypeID';
  readonly AccessLevelID: string = 'AccessLevelID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly UserLogin: string = 'UserLogin';
  readonly DeviceTypeID: string = 'DeviceTypeID';
  readonly ServiceTypeID: string = 'ServiceTypeID';
  readonly HostIP: string = 'HostIP';
  readonly HostName: string = 'HostName';
  readonly Created: string = 'Created';
  readonly Expires: string = 'Expires';
  readonly UtcOffsetMinutes: string = 'UtcOffsetMinutes';
  readonly OSName: string = 'OSName';
  readonly UserAgent: string = 'UserAgent';
  readonly TimeZoneUtcOffset: string = 'TimeZoneUtcOffset';
  readonly Client64Bit: string = 'Client64Bit';
  readonly Client64BitOS: string = 'Client64BitOS';
  readonly CalendarID: string = 'CalendarID';
  readonly Culture: string = 'Culture';
  readonly UICulture: string = 'UICulture';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SessionServiceTypes

/**
 * ID: {62c1a795-1688-48a1-b0af-d77032c90bab}
 * Alias: SessionServiceTypes
 * Group: System
 * Description: Типы сессий, которые определяются типом веб-сервиса: для desktop- или для web-клиентов, или веб-сервис отсутствует (прямое взаимодействие с БД).
 */
class SessionServiceTypesSchemeInfo {
  private readonly name: string = 'SessionServiceTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Unknown: SessionServiceTypes = new SessionServiceTypes(0, '$Enum_SessionServiceTypes_Unknown');
  readonly DesktopClient: SessionServiceTypes = new SessionServiceTypes(1, '$Enum_SessionServiceTypes_DesktopClient');
  readonly WebClient: SessionServiceTypes = new SessionServiceTypes(2, '$Enum_SessionServiceTypes_WebClient');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region SessionServiceTypes Enumeration

class SessionServiceTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region SignatureCertificateSettings

/**
 * ID: {faf66527-24c2-4f20-afa8-46915e5fd4d6}
 * Alias: SignatureCertificateSettings
 * Group: System
 */
class SignatureCertificateSettingsSchemeInfo {
  private readonly name: string = 'SignatureCertificateSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly StartDate: string = 'StartDate';
  readonly EndDate: string = 'EndDate';
  readonly IsValidDate: string = 'IsValidDate';
  readonly Company: string = 'Company';
  readonly Subject: string = 'Subject';
  readonly Issuer: string = 'Issuer';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SignatureDigestAlgorithms

/**
 * ID: {9180bf30-3b8b-4adc-a285-d9ee97aea219}
 * Alias: SignatureDigestAlgorithms
 * Group: System
 * Description: Идентификаторы алгоритмов хеширования
 */
class SignatureDigestAlgorithmsSchemeInfo {
  private readonly name: string = 'SignatureDigestAlgorithms';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly OID: string = 'OID';

  //#endregion

  //#region Enumeration

  readonly Enum256: SignatureDigestAlgorithms = new SignatureDigestAlgorithms(1, '$Enum_Signature_DigestAlgos_GOST12_256', '1.2.643.7.1.1.2.2');
  readonly Enum512: SignatureDigestAlgorithms = new SignatureDigestAlgorithms(2, '$Enum_Signature_DigestAlgos_GOST12_512', '1.2.643.7.1.1.2.3');
  readonly GOST94: SignatureDigestAlgorithms = new SignatureDigestAlgorithms(3, '$Enum_Signature_DigestAlgos_GOST94', '1.2.643.2.2.9');
  readonly SHA1: SignatureDigestAlgorithms = new SignatureDigestAlgorithms(7, 'SHA1', '1.3.14.3.2.26');
  readonly SHA256: SignatureDigestAlgorithms = new SignatureDigestAlgorithms(8, 'SHA256', '2.16.840.1.101.3.4.2.1');
  readonly SHA384: SignatureDigestAlgorithms = new SignatureDigestAlgorithms(9, 'SHA384', '2.16.840.1.101.3.4.2.2');
  readonly SHA512: SignatureDigestAlgorithms = new SignatureDigestAlgorithms(10, 'SHA512', '2.16.840.1.101.3.4.2.3');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region SignatureDigestAlgorithms Enumeration

class SignatureDigestAlgorithms {
  readonly ID: number;
  readonly Name: string | null;
  readonly OID: string | null;

  constructor (ID: number, Name: string | null, OID: string | null) {
    this.ID = ID;
    this.Name = Name;
    this.OID = OID;
  }
}

//#endregion

//#endregion

//#region SignatureEncryptDigestSettings

/**
 * ID: {7c57bbba-8acc-4abf-b3cc-372399b68dbc}
 * Alias: SignatureEncryptDigestSettings
 * Group: System
 */
class SignatureEncryptDigestSettingsSchemeInfo {
  private readonly name: string = 'SignatureEncryptDigestSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly EncryptionAlgorithmID: string = 'EncryptionAlgorithmID';
  readonly EncryptionAlgorithmName: string = 'EncryptionAlgorithmName';
  readonly EncryptionAlgorithmOID: string = 'EncryptionAlgorithmOID';
  readonly DigestAlgorithmID: string = 'DigestAlgorithmID';
  readonly DigestAlgorithmName: string = 'DigestAlgorithmName';
  readonly DigestAlgorithmOID: string = 'DigestAlgorithmOID';
  readonly EdsManagerName: string = 'EdsManagerName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SignatureEncryptionAlgorithms

/**
 * ID: {93f36ef0-b0ca-4726-9038-b10339db4b00}
 * Alias: SignatureEncryptionAlgorithms
 * Group: System
 * Description: Идентификаторы алгоритмов подписи
 */
class SignatureEncryptionAlgorithmsSchemeInfo {
  private readonly name: string = 'SignatureEncryptionAlgorithms';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly OID: string = 'OID';

  //#endregion

  //#region Enumeration

  readonly Enum256: SignatureEncryptionAlgorithms = new SignatureEncryptionAlgorithms(1, '$Enum_Signature_EncAlgos_GOST12_256', '1.2.643.7.1.1.1.1');
  readonly Enum512: SignatureEncryptionAlgorithms = new SignatureEncryptionAlgorithms(2, '$Enum_Signature_EncAlgos_GOST12_512', '1.2.643.7.1.1.1.2');
  readonly GOST2001: SignatureEncryptionAlgorithms = new SignatureEncryptionAlgorithms(3, '$Enum_Signature_EncAlgos_GOST2001', '1.2.643.2.2.19');
  readonly Others: SignatureEncryptionAlgorithms = new SignatureEncryptionAlgorithms(4, '$Enum_Signature_EncAlgos_Others', '');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region SignatureEncryptionAlgorithms Enumeration

class SignatureEncryptionAlgorithms {
  readonly ID: number;
  readonly Name: string | null;
  readonly OID: string | null;

  constructor (ID: number, Name: string | null, OID: string | null) {
    this.ID = ID;
    this.Name = Name;
    this.OID = OID;
  }
}

//#endregion

//#endregion

//#region SignatureManagerVirtual

/**
 * ID: {72eb4e5a-f328-40e6-bb2d-18ea0a9a9d2b}
 * Alias: SignatureManagerVirtual
 * Group: System
 */
class SignatureManagerVirtualSchemeInfo {
  private readonly name: string = 'SignatureManagerVirtual';

  //#region Columns

  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly DefaultEDSManager: SignatureManagerVirtual = new SignatureManagerVirtual('DefaultEDSManager');
  readonly CryptoProEDSManager: SignatureManagerVirtual = new SignatureManagerVirtual('CryptoProEDSManager');
  readonly ServiceEDSManagerForCMS: SignatureManagerVirtual = new SignatureManagerVirtual('ServiceEDSManagerForCMS');
  readonly ServiceEDSManagerForCAdES: SignatureManagerVirtual = new SignatureManagerVirtual('ServiceEDSManagerForCAdES');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region SignatureManagerVirtual Enumeration

class SignatureManagerVirtual {
  readonly Name: string | null;

  constructor (Name: string | null) {
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region SignaturePackagings

/**
 * ID: {15620b78-46b8-4520-aa60-4bfefe67c731}
 * Alias: SignaturePackagings
 * Group: System
 * Description: Варианты упаковки подписи
 */
class SignaturePackagingsSchemeInfo {
  private readonly name: string = 'SignaturePackagings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Detached: SignaturePackagings = new SignaturePackagings(3, '$Enum_Signature_Packagings_Detached');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region SignaturePackagings Enumeration

class SignaturePackagings {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region SignatureProfiles

/**
 * ID: {eca29bb9-3085-4556-b19a-6015cbc8fb25}
 * Alias: SignatureProfiles
 * Group: System
 * Description: Профили цифровой подписи
 */
class SignatureProfilesSchemeInfo {
  private readonly name: string = 'SignatureProfiles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly None: SignatureProfiles = new SignatureProfiles(0, '$Enum_Signature_Profiles_None');
  readonly BES: SignatureProfiles = new SignatureProfiles(1, '$Enum_Signature_Profiles_BES');
  readonly T: SignatureProfiles = new SignatureProfiles(3, '$Enum_Signature_Profiles_T');
  readonly C: SignatureProfiles = new SignatureProfiles(4, '$Enum_Signature_Profiles_C');
  readonly XL: SignatureProfiles = new SignatureProfiles(5, '$Enum_Signature_Profiles_XL');
  readonly Type1: SignatureProfiles = new SignatureProfiles(6, '$Enum_Signature_Profiles_X_Type1');
  readonly Type2: SignatureProfiles = new SignatureProfiles(7, '$Enum_Signature_Profiles_X_Type2');
  readonly Type12: SignatureProfiles = new SignatureProfiles(8, '$Enum_Signature_Profiles_XL_Type1');
  readonly Type22: SignatureProfiles = new SignatureProfiles(9, '$Enum_Signature_Profiles_XL_Type2');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region SignatureProfiles Enumeration

class SignatureProfiles {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region SignatureSettings

/**
 * ID: {076b2050-e20b-412b-942b-b4cb063e6941}
 * Alias: SignatureSettings
 * Group: System
 * Description: Таблица настроек цифровой подписи
 */
class SignatureSettingsSchemeInfo {
  private readonly name: string = 'SignatureSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly SignatureTypeID: string = 'SignatureTypeID';
  readonly SignatureTypeName: string = 'SignatureTypeName';
  readonly SignatureProfileID: string = 'SignatureProfileID';
  readonly SignatureProfileName: string = 'SignatureProfileName';
  readonly TSPAddress: string = 'TSPAddress';
  readonly OCSPAddress: string = 'OCSPAddress';
  readonly CRLAddress: string = 'CRLAddress';
  readonly SignaturePackagingID: string = 'SignaturePackagingID';
  readonly SignaturePackagingName: string = 'SignaturePackagingName';
  readonly TSPUserName: string = 'TSPUserName';
  readonly TSPPassword: string = 'TSPPassword';
  readonly TSPDigestAlgorithmID: string = 'TSPDigestAlgorithmID';
  readonly TSPDigestAlgorithmOID: string = 'TSPDigestAlgorithmOID';
  readonly TSPDigestAlgorithmName: string = 'TSPDigestAlgorithmName';
  readonly UseSystemRootCertificates: string = 'UseSystemRootCertificates';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SignatureTypes

/**
 * ID: {577baaea-6832-4eb7-9333-60661367720e}
 * Alias: SignatureTypes
 * Group: System
 * Description: Таблица видов подписей
 */
class SignatureTypesSchemeInfo {
  private readonly name: string = 'SignatureTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly None: SignatureTypes = new SignatureTypes(0, '$Enum_SignatureTypes_None');
  readonly CAdES: SignatureTypes = new SignatureTypes(1, '$Enum_SignatureTypes_CAdES');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region SignatureTypes Enumeration

class SignatureTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region SmartRoleGeneratorInfo

/**
 * ID: {c44db46a-349f-45ec-b0ab-ec212c09b276}
 * Alias: SmartRoleGeneratorInfo
 * Group: Acl
 * Description: Таблица с информацией о расчёте генераторов умных ролей.
 */
class SmartRoleGeneratorInfoSchemeInfo {
  private readonly name: string = 'SmartRoleGeneratorInfo';

  //#region Columns

  readonly GeneratorID: string = 'GeneratorID';
  readonly GeneratorVersion: string = 'GeneratorVersion';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SmartRoleGenerators

/**
 * ID: {5f3a0dbc-2fc4-4269-8a5d-eb95f39970ba}
 * Alias: SmartRoleGenerators
 * Group: Acl
 * Description: Основная секция для генераторов умных ролей.
 */
class SmartRoleGeneratorsSchemeInfo {
  private readonly name: string = 'SmartRoleGenerators';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly RolesSelectorSql: string = 'RolesSelectorSql';
  readonly OwnersSelectorSql: string = 'OwnersSelectorSql';
  readonly RoleNameTemplate: string = 'RoleNameTemplate';
  readonly HideRoles: string = 'HideRoles';
  readonly InitRoles: string = 'InitRoles';
  readonly OwnerDataSelectorSql: string = 'OwnerDataSelectorSql';
  readonly Description: string = 'Description';
  readonly IsDisabled: string = 'IsDisabled';
  readonly EnableErrorLogging: string = 'EnableErrorLogging';
  readonly Version: string = 'Version';
  readonly DisableDeputies: string = 'DisableDeputies';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SmartRoleMembers

/**
 * ID: {73cbcd25-8709-4c3d-9091-3db6ccba5055}
 * Alias: SmartRoleMembers
 * Group: Acl
 */
class SmartRoleMembersSchemeInfo {
  private readonly name: string = 'SmartRoleMembers';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SmartRoles

/**
 * ID: {844013f9-7faa-422a-b583-2b04ae46f0be}
 * Alias: SmartRoles
 * Group: Acl
 * Description: Основная секция для записей настроек умных ролей.
 */
class SmartRolesSchemeInfo {
  private readonly name: string = 'SmartRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RuleID: string = 'RuleID';
  readonly RuleName: string = 'RuleName';
  readonly ParentID: string = 'ParentID';
  readonly OwnerID: string = 'OwnerID';
  readonly OwnerName: string = 'OwnerName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Tables

/**
 * ID: {66b31fcc-b8fa-465a-91f2-0dd391cc76ec}
 * Alias: Tables
 * Group: System
 * Description: Contains metadata that describes tables which used by Tessa
 */
class TablesSchemeInfo {
  private readonly name: string = 'Tables';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Definition: string = 'Definition';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TagCards

/**
 * ID: {7ecf1fb4-cd7b-4fea-9a3b-6e22b2186ed6}
 * Alias: TagCards
 * Group: Tags
 * Description: Таблица с тегами карточек.
 */
class TagCardsSchemeInfo {
  private readonly name: string = 'TagCards';

  //#region Columns

  readonly TagID: string = 'TagID';
  readonly CardID: string = 'CardID';
  readonly UserID: string = 'UserID';
  readonly SetAt: string = 'SetAt';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TagCondition

/**
 * ID: {8a9a0383-b94a-40c0-aa3e-7b461f25b598}
 * Alias: TagCondition
 * Group: Tags
 * Description: Секция для условия для правил уведомлений, проверяющая Тег.
 */
class TagConditionSchemeInfo {
  private readonly name: string = 'TagCondition';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TagID: string = 'TagID';
  readonly TagName: string = 'TagName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TagEditors

/**
 * ID: {c4ee86f8-3022-432b-ae77-e1ca4a47c891}
 * Alias: TagEditors
 * Group: Tags
 */
class TagEditorsSchemeInfo {
  private readonly name: string = 'TagEditors';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Tags

/**
 * ID: {0bf4050e-d7d4-4cda-ab55-4a4f0148dd7f}
 * Alias: Tags
 * Group: Tags
 * Description: Основная секция с настройками тегов.
 */
class TagsSchemeInfo {
  private readonly name: string = 'Tags';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly OwnerID: string = 'OwnerID';
  readonly OwnerName: string = 'OwnerName';
  readonly Foreground: string = 'Foreground';
  readonly Background: string = 'Background';
  readonly IsCommon: string = 'IsCommon';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TagSharedWith

/**
 * ID: {3b4e5980-82d8-4bce-adb3-4fbc88c3e03a}
 * Alias: TagSharedWith
 * Group: Tags
 * Description: Список ролей, которым доступен тег.
 */
class TagSharedWithSchemeInfo {
  private readonly name: string = 'TagSharedWith';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TagsUserSettingsVirtual

/**
 * ID: {f4947518-a710-4693-8c8b-ab2acc42bc5a}
 * Alias: TagsUserSettingsVirtual
 * Group: Tags
 * Description: Виртуальная таблица для формы с пользовательскими настройками тегов
 */
class TagsUserSettingsVirtualSchemeInfo {
  private readonly name: string = 'TagsUserSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly MaxTagsDisplayed: string = 'MaxTagsDisplayed';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TaskAssignedRoles

/**
 * ID: {2539f630-3898-457e-9e49-e1f87552caaf}
 * Alias: TaskAssignedRoles
 * Group: System
 */
class TaskAssignedRolesSchemeInfo {
  private readonly name: string = 'TaskAssignedRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TaskRoleID: string = 'TaskRoleID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly RoleTypeID: string = 'RoleTypeID';
  readonly Position: string = 'Position';
  readonly ParentRowID: string = 'ParentRowID';
  readonly Master: string = 'Master';
  readonly ShowInTaskDetails: string = 'ShowInTaskDetails';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TaskCommonInfo

/**
 * ID: {005962e7-65f1-4763-a0ef-b76751d26de3}
 * Alias: TaskCommonInfo
 * Group: Common
 * Description: Общая информация для заданий.
 * Во всех случаях, когда в задании надо вывести некое описание текста задания, нужно использовать эту секцию.
 * Также используется в представлении "Мои задания", чтобы выводить некий  текст - описание задания в табличку со списком заданий.
 */
class TaskCommonInfoSchemeInfo {
  private readonly name: string = 'TaskCommonInfo';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Info: string = 'Info';
  readonly KindID: string = 'KindID';
  readonly KindCaption: string = 'KindCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TaskConditionCompletionOptions

/**
 * ID: {059e1354-8b89-4e86-8fa1-29395e952926}
 * Alias: TaskConditionCompletionOptions
 * Group: Acl
 * Description: Варианты завершения для условий проверки заданий.
 */
class TaskConditionCompletionOptionsSchemeInfo {
  private readonly name: string = 'TaskConditionCompletionOptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly CompletionOptionID: string = 'CompletionOptionID';
  readonly CompletionOptionCaption: string = 'CompletionOptionCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TaskConditionFunctionRoles

/**
 * ID: {b59a92cb-8414-4a3d-91f9-9d41de829d3f}
 * Alias: TaskConditionFunctionRoles
 * Group: Acl
 * Description: Список функциональных ролей для условий проверки заданий.
 */
class TaskConditionFunctionRolesSchemeInfo {
  private readonly name: string = 'TaskConditionFunctionRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly FunctionRoleID: string = 'FunctionRoleID';
  readonly FunctionRoleCaption: string = 'FunctionRoleCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TaskConditionSettings

/**
 * ID: {48dcb84f-1518-4de4-8995-86c4e75a1d03}
 * Alias: TaskConditionSettings
 * Group: Acl
 * Description: Настройки для условий проверки задания.
 */
class TaskConditionSettingsSchemeInfo {
  private readonly name: string = 'TaskConditionSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CheckTaskCreation: string = 'CheckTaskCreation';
  readonly CheckTaskCompletion: string = 'CheckTaskCompletion';
  readonly CheckTaskFunctionRolesChanges: string = 'CheckTaskFunctionRolesChanges';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TaskConditionTaskKinds

/**
 * ID: {a3777728-9f01-449a-b94c-953a1e205c5b}
 * Alias: TaskConditionTaskKinds
 * Group: Acl
 * Description: Список видов заданий для условий проверки заданий.
 */
class TaskConditionTaskKindsSchemeInfo {
  private readonly name: string = 'TaskConditionTaskKinds';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TaskKindID: string = 'TaskKindID';
  readonly TaskKindCaption: string = 'TaskKindCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TaskConditionTaskTypes

/**
 * ID: {f7cd6753-21d7-4095-8c3a-e7175f591ad3}
 * Alias: TaskConditionTaskTypes
 * Group: Acl
 * Description: Типы заданий для условий проверки заданий.
 */
class TaskConditionTaskTypesSchemeInfo {
  private readonly name: string = 'TaskConditionTaskTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeName: string = 'TypeName';
  readonly TypeCaption: string = 'TypeCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TaskHistory

/**
 * ID: {f8deab4c-fa9d-404a-8abc-b570cd81820e}
 * Alias: TaskHistory
 * Group: System
 * Description: История завершённых заданий.
 */
class TaskHistorySchemeInfo {
  private readonly name: string = 'TaskHistory';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly Created: string = 'Created';
  readonly Planned: string = 'Planned';
  readonly InProgress: string = 'InProgress';
  readonly Completed: string = 'Completed';
  readonly Result: string = 'Result';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly UserDepartment: string = 'UserDepartment';
  readonly UserPosition: string = 'UserPosition';
  readonly CompletedByID: string = 'CompletedByID';
  readonly CompletedByName: string = 'CompletedByName';
  readonly CompletedByRole: string = 'CompletedByRole';
  readonly TimeZoneID: string = 'TimeZoneID';
  readonly TimeZoneUtcOffsetMinutes: string = 'TimeZoneUtcOffsetMinutes';
  readonly GroupRowID: string = 'GroupRowID';
  readonly KindID: string = 'KindID';
  readonly KindCaption: string = 'KindCaption';
  readonly CalendarID: string = 'CalendarID';
  readonly Settings: string = 'Settings';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly AssignedOnRole: string = 'AssignedOnRole';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TaskHistoryGroups

/**
 * ID: {31644536-fba1-456c-881c-7dae73b7182c}
 * Alias: TaskHistoryGroups
 * Group: System
 */
class TaskHistoryGroupsSchemeInfo {
  private readonly name: string = 'TaskHistoryGroups';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly TypeID: string = 'TypeID';
  readonly Caption: string = 'Caption';
  readonly Iteration: string = 'Iteration';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TaskHistoryGroupTypes

/**
 * ID: {319be329-6cd3-457a-b792-41c26a266b95}
 * Alias: TaskHistoryGroupTypes
 * Group: System
 */
class TaskHistoryGroupTypesSchemeInfo {
  private readonly name: string = 'TaskHistoryGroupTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Caption: string = 'Caption';
  readonly Description: string = 'Description';
  readonly Placeholders: string = 'Placeholders';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TaskKinds

/**
 * ID: {856068b1-0e78-4aa8-8e7a-4f53d91a7298}
 * Alias: TaskKinds
 * Group: System
 * Description: Виды заданий.
 */
class TaskKindsSchemeInfo {
  private readonly name: string = 'TaskKinds';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Caption: string = 'Caption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Tasks

/**
 * ID: {5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8}
 * Alias: Tasks
 * Group: System
 * Description: Tasks of a card
 */
class TasksSchemeInfo {
  private readonly name: string = 'Tasks';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly StateID: string = 'StateID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly Planned: string = 'Planned';
  readonly InProgress: string = 'InProgress';
  readonly Created: string = 'Created';
  readonly CreatedByID: string = 'CreatedByID';
  readonly CreatedByName: string = 'CreatedByName';
  readonly Modified: string = 'Modified';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';
  readonly Digest: string = 'Digest';
  readonly ParentID: string = 'ParentID';
  readonly Postponed: string = 'Postponed';
  readonly PostponedTo: string = 'PostponedTo';
  readonly PostponeComment: string = 'PostponeComment';
  readonly TimeZoneID: string = 'TimeZoneID';
  readonly TimeZoneUtcOffsetMinutes: string = 'TimeZoneUtcOffsetMinutes';
  readonly Settings: string = 'Settings';
  readonly CalendarID: string = 'CalendarID';
  readonly CalendarName: string = 'CalendarName';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TaskStates

/**
 * ID: {057a85c8-c20f-430b-bd3b-6ea9f9fb82ee}
 * Alias: TaskStates
 * Group: System
 * Description: Состояние задания.
 */
class TaskStatesSchemeInfo {
  private readonly name: string = 'TaskStates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly New: TaskStates = new TaskStates(0, '$Cards_TaskStates_New');
  readonly InWork: TaskStates = new TaskStates(1, '$Cards_TaskStates_InWork');
  readonly Postponed: TaskStates = new TaskStates(2, '$Cards_TaskStates_Postponed');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region TaskStates Enumeration

class TaskStates {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region TemplateEditRoles

/**
 * ID: {df057f7e-f59a-4857-b615-19abb650442a}
 * Alias: TemplateEditRoles
 * Group: System
 * Description: Роли, которым шаблон доступен для редактирования и удаления помимо администраторов.
 * Указанным ролям автоматически доступно создание шаблона или создание карточки из шаблона.
 */
class TemplateEditRolesSchemeInfo {
  private readonly name: string = 'TemplateEditRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TemplateFiles

/**
 * ID: {fb7c3c18-d7cd-4f16-84e2-33ab0269adbd}
 * Alias: TemplateFiles
 * Group: System
 * Description: Файлы в шаблоне карточек.
 */
class TemplateFilesSchemeInfo {
  private readonly name: string = 'TemplateFiles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly SourceFileID: string = 'SourceFileID';
  readonly SourceVersionRowID: string = 'SourceVersionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TemplateOpenRoles

/**
 * ID: {831ff542-f2b2-4a3e-9295-0695b843567c}
 * Alias: TemplateOpenRoles
 * Group: System
 * Description: Роли, которым доступен просмотр шаблона и создание карточки из шаблона помимо администраторов.
 */
class TemplateOpenRolesSchemeInfo {
  private readonly name: string = 'TemplateOpenRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Templates

/**
 * ID: {9f15aaf8-032c-4222-9c7c-2cfffeee89ed}
 * Alias: Templates
 * Group: System
 * Description: Шаблоны карточек.
 */
class TemplatesSchemeInfo {
  private readonly name: string = 'Templates';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Digest: string = 'Digest';
  readonly Description: string = 'Description';
  readonly Definition: string = 'Definition';
  readonly Card: string = 'Card';
  readonly CardID: string = 'CardID';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly Version: string = 'Version';
  readonly Caption: string = 'Caption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TemplatesVirtual

/**
 * ID: {b6509d11-f1b3-4f54-b34f-4a996f66c71c}
 * Alias: TemplatesVirtual
 * Group: System
 * Description: Информация по шаблонам карточек, подготовленная для вывода в UI.
 */
class TemplatesVirtualSchemeInfo {
  private readonly name: string = 'TemplatesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CardID: string = 'CardID';
  readonly CardDigest: string = 'CardDigest';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TEST_CarAdditionalInfo

/**
 * ID: {9cbc0f98-571a-4822-a290-3e36b2f2f2e6}
 * Alias: TEST_CarAdditionalInfo
 * Group: Test
 */
class TEST_CarAdditionalInfoSchemeInfo {
  private readonly name: string = 'TEST_CarAdditionalInfo';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Color: string = 'Color';
  readonly IsBaseColor: string = 'IsBaseColor';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TEST_CarCustomers

/**
 * ID: {30295c44-c633-4474-9e30-4492e75e7e75}
 * Alias: TEST_CarCustomers
 * Group: Test
 */
class TEST_CarCustomersSchemeInfo {
  private readonly name: string = 'TEST_CarCustomers';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly FullName: string = 'FullName';
  readonly PurchaseDate: string = 'PurchaseDate';
  readonly SaleRowID: string = 'SaleRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TEST_CarMainInfo

/**
 * ID: {509d961f-00cf-4403-a78f-6736841de448}
 * Alias: TEST_CarMainInfo
 * Group: Test
 */
class TEST_CarMainInfoSchemeInfo {
  private readonly name: string = 'TEST_CarMainInfo';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly MaxSpeed: string = 'MaxSpeed';
  readonly Running: string = 'Running';
  readonly Cost: string = 'Cost';
  readonly ReleaseDate: string = 'ReleaseDate';
  readonly DriverID: string = 'DriverID';
  readonly DriverName: string = 'DriverName';
  readonly Documentation: string = 'Documentation';
  readonly Xml: string = 'Xml';
  readonly Json: string = 'Json';
  readonly Jsonb: string = 'Jsonb';
  readonly Binary: string = 'Binary';
  readonly NullableGuid: string = 'NullableGuid';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TEST_CarOwners

/**
 * ID: {2cff79c9-6e5a-4e98-8c8f-7a14eb7bec80}
 * Alias: TEST_CarOwners
 * Group: Test
 */
class TEST_CarOwnersSchemeInfo {
  private readonly name: string = 'TEST_CarOwners';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TEST_CarSales

/**
 * ID: {6dc3a829-b1f4-4e67-ba99-16a30fe91209}
 * Alias: TEST_CarSales
 * Group: Test
 */
class TEST_CarSalesSchemeInfo {
  private readonly name: string = 'TEST_CarSales';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';
  readonly EndDate: string = 'EndDate';
  readonly ManagerID: string = 'ManagerID';
  readonly ManagerName: string = 'ManagerName';
  readonly Used: string = 'Used';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TEST_CustomerOperations

/**
 * ID: {7f813c98-3331-46a9-8aa0-bab55a956246}
 * Alias: TEST_CustomerOperations
 * Group: Test
 */
class TEST_CustomerOperationsSchemeInfo {
  private readonly name: string = 'TEST_CustomerOperations';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly OperationName: string = 'OperationName';
  readonly ManagerName: string = 'ManagerName';
  readonly CustomerRowID: string = 'CustomerRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TileSizes

/**
 * ID: {9d1fb4ee-fa51-4926-8abb-c464ca91e450}
 * Alias: TileSizes
 * Group: System
 * Description: Размеры плиток
 */
class TileSizesSchemeInfo {
  private readonly name: string = 'TileSizes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Half: TileSizes = new TileSizes(1, '$Enum_TileSize_Half');
  readonly Quarter: TileSizes = new TileSizes(2, '$Enum_TileSize_Quarter');
  readonly Full: TileSizes = new TileSizes(0, '$Enum_TileSize_Full');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region TileSizes Enumeration

class TileSizes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region TimeZones

/**
 * ID: {984e22bf-78fc-4c69-b1a6-ca73341c36ea}
 * Alias: TimeZones
 * Group: System
 */
class TimeZonesSchemeInfo {
  private readonly name: string = 'TimeZones';

  //#region Columns

  readonly ID: string = 'ID';
  readonly CodeName: string = 'CodeName';
  readonly UtcOffsetMinutes: string = 'UtcOffsetMinutes';
  readonly DisplayName: string = 'DisplayName';
  readonly ShortName: string = 'ShortName';
  readonly IsNegativeOffsetDirection: string = 'IsNegativeOffsetDirection';
  readonly OffsetTime: string = 'OffsetTime';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TimeZonesSettings

/**
 * ID: {44e8b6f2-f7d1-48ff-a3f2-599bf76e5180}
 * Alias: TimeZonesSettings
 * Group: System
 */
class TimeZonesSettingsSchemeInfo {
  private readonly name: string = 'TimeZonesSettings';

  //#region Columns

  readonly ID: string = 'ID';
  readonly AllowToModify: string = 'AllowToModify';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TimeZonesVirtual

/**
 * ID: {3e09239e-ebb7-4b0a-a4e1-51eae83e3c0c}
 * Alias: TimeZonesVirtual
 * Group: System
 * Description: Временные зоны
 */
class TimeZonesVirtualSchemeInfo {
  private readonly name: string = 'TimeZonesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly CodeName: string = 'CodeName';
  readonly UtcOffsetMinutes: string = 'UtcOffsetMinutes';
  readonly IsNegativeOffsetDirection: string = 'IsNegativeOffsetDirection';
  readonly OffsetTime: string = 'OffsetTime';
  readonly DisplayName: string = 'DisplayName';
  readonly ShortName: string = 'ShortName';
  readonly ZoneID: string = 'ZoneID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Types

/**
 * ID: {b0538ece-8468-4d0b-8b4e-5a1d43e024db}
 * Alias: Types
 * Group: System
 * Description: Contains metadata that describes types which used by Tessa.
 */
class TypesSchemeInfo {
  private readonly name: string = 'Types';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';
  readonly Group: string = 'Group';
  readonly InstanceTypeID: string = 'InstanceTypeID';
  readonly Flags: string = 'Flags';
  readonly Metadata: string = 'Metadata';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region UserSettingsFunctionRolesVirtual

/**
 * ID: {0e7d4c80-0a90-40a6-86a7-01ec32c80ba9}
 * Alias: UserSettingsFunctionRolesVirtual
 * Group: System
 * Description: Таблица с настройками сотрудника, предоставляемыми системой для функциональных ролей заданий.
 * Такие настройки изменяются в метаинформации динамически, в зависимости от строк в таблице FunctionRoles.
 */
class UserSettingsFunctionRolesVirtualSchemeInfo {
  private readonly name: string = 'UserSettingsFunctionRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region UserSettingsVirtual

/**
 * ID: {3c8a5e77-c4da-45f5-b974-170af387ce26}
 * Alias: UserSettingsVirtual
 * Group: System
 * Description: Таблица с настройками сотрудника, предоставляемыми системой.
 */
class UserSettingsVirtualSchemeInfo {
  private readonly name: string = 'UserSettingsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly LeftPanelOpenOnClick: string = 'LeftPanelOpenOnClick';
  readonly LeftPanelTopAreaOpenOnClick: string = 'LeftPanelTopAreaOpenOnClick';
  readonly LeftPanelBottomAreaOpenOnClick: string = 'LeftPanelBottomAreaOpenOnClick';
  readonly RightPanelOpenOnClick: string = 'RightPanelOpenOnClick';
  readonly RightPanelTopAreaOpenOnClick: string = 'RightPanelTopAreaOpenOnClick';
  readonly RightPanelBottomAreaOpenOnClick: string = 'RightPanelBottomAreaOpenOnClick';
  readonly DisableWindowFading: string = 'DisableWindowFading';
  readonly DisablePdfEmbeddedPreview: string = 'DisablePdfEmbeddedPreview';
  readonly PreferPdfPagingPreview: string = 'PreferPdfPagingPreview';
  readonly DisablePopupNotifications: string = 'DisablePopupNotifications';
  readonly WebLeftPanelOpenOnClick: string = 'WebLeftPanelOpenOnClick';
  readonly WebRightPanelOpenOnClick: string = 'WebRightPanelOpenOnClick';
  readonly AllowMultipleExternalPreview: string = 'AllowMultipleExternalPreview';
  readonly TaskColor: string = 'TaskColor';
  readonly TopicItemColor: string = 'TopicItemColor';
  readonly FrequentlyUsedEmoji: string = 'FrequentlyUsedEmoji';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region VatTypes

/**
 * ID: {8dd87520-9d83-4d8a-8c60-c1275328c5e8}
 * Alias: VatTypes
 * Group: Common
 */
class VatTypesSchemeInfo {
  private readonly name: string = 'VatTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly WithVAT: VatTypes = new VatTypes(0, '$VatType_WithVAT');
  readonly ExemptFromVAT: VatTypes = new VatTypes(1, '$VatType_ExemptFromVAT');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region VatTypes Enumeration

class VatTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region ViewRoles

/**
 * ID: {5a5dc5fe-19e1-4c69-b084-d6db36aa5a23}
 * Alias: ViewRoles
 * Group: System
 */
class ViewRolesSchemeInfo {
  private readonly name: string = 'ViewRoles';

  //#region Columns

  readonly ViewID: string = 'ViewID';
  readonly RoleID: string = 'RoleID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ViewRolesVirtual

/**
 * ID: {08fccef5-fe25-4f3b-9a8c-2291b6a60209}
 * Alias: ViewRolesVirtual
 * Group: System
 * Description: Список ролей для представлений, отображаемых как виртуальные карточки в клиенте.
 */
class ViewRolesVirtualSchemeInfo {
  private readonly name: string = 'ViewRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Views

/**
 * ID: {3519b63c-eea0-48f4-b70a-544e58ece5fc}
 * Alias: Views
 * Group: System
 * Description: Представления.
 */
class ViewsSchemeInfo {
  private readonly name: string = 'Views';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Alias: string = 'Alias';
  readonly Caption: string = 'Caption';
  readonly ModifiedDateTime: string = 'ModifiedDateTime';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';
  readonly MetadataSource: string = 'MetadataSource';
  readonly MsQuerySource: string = 'MsQuerySource';
  readonly Description: string = 'Description';
  readonly GroupName: string = 'GroupName';
  readonly PgQuerySource: string = 'PgQuerySource';
  readonly JsonMetadataSource: string = 'JsonMetadataSource';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ViewsVirtual

/**
 * ID: {cefba5f8-8b2c-4be0-ba24-564f3a474240}
 * Alias: ViewsVirtual
 * Group: System
 * Description: Представления, отображаемые как виртуальные карточки в клиенте.
 */
class ViewsVirtualSchemeInfo {
  private readonly name: string = 'ViewsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Alias: string = 'Alias';
  readonly Caption: string = 'Caption';
  readonly GroupName: string = 'GroupName';
  readonly Description: string = 'Description';
  readonly Modified: string = 'Modified';
  readonly ModifiedByID: string = 'ModifiedByID';
  readonly ModifiedByName: string = 'ModifiedByName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeAddFileFromTemplateAction

/**
 * ID: {93d11813-4967-458e-b3be-f7da367a8872}
 * Alias: WeAddFileFromTemplateAction
 * Group: WorkflowEngine
 * Description: Основная секция для действия Добавить файл по шаблону
 */
class WeAddFileFromTemplateActionSchemeInfo {
  private readonly name: string = 'WeAddFileFromTemplateAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly FileTemplateID: string = 'FileTemplateID';
  readonly FileTemplateName: string = 'FileTemplateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WebApplications

/**
 * ID: {610d8253-e293-4676-abcb-e7a0ac1a084d}
 * Alias: WebApplications
 * Group: System
 * Description: Карточки приложений-ассистентов web-клиента, таких как Deski
 */
class WebApplicationsSchemeInfo {
  private readonly name: string = 'WebApplications';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly AppVersion: string = 'AppVersion';
  readonly PlatformVersion: string = 'PlatformVersion';
  readonly Description: string = 'Description';
  readonly ExecutableFileName: string = 'ExecutableFileName';
  readonly LanguageID: string = 'LanguageID';
  readonly LanguageCaption: string = 'LanguageCaption';
  readonly LanguageCode: string = 'LanguageCode';
  readonly OSName: string = 'OSName';
  readonly Client64Bit: string = 'Client64Bit';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WebClientRoles

/**
 * ID: {383321f7-c432-42d8-84f9-e4f58e0cb021}
 * Alias: WebClientRoles
 * Group: System
 * Description: Роли, в одну из которых должен входить сотрудник для авторизации в web-клиенте.
 */
class WebClientRolesSchemeInfo {
  private readonly name: string = 'WebClientRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeCommandAction

/**
 * ID: {2dc38a34-3451-4ea9-9885-9afa15155612}
 * Alias: WeCommandAction
 * Group: WorkflowEngine
 * Description: Секция для действия Подписка на команду
 */
class WeCommandActionSchemeInfo {
  private readonly name: string = 'WeCommandAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ReusableSubscription: string = 'ReusableSubscription';
  readonly CommandID: string = 'CommandID';
  readonly CommandName: string = 'CommandName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeCommandActionLinks

/**
 * ID: {97e973ba-8fa9-4e3d-96d3-2f077ca11531}
 * Alias: WeCommandActionLinks
 * Group: WorkflowEngine
 * Description: Секция определяет список переходов, которые должны быть вызваны после получения команды
 */
class WeCommandActionLinksSchemeInfo {
  private readonly name: string = 'WeCommandActionLinks';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly LinkID: string = 'LinkID';
  readonly LinkName: string = 'LinkName';
  readonly LinkCaption: string = 'LinkCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeConditionAction

/**
 * ID: {ad4abe1f-9f6b-4842-b8d5-bb34502c7dce}
 * Alias: WeConditionAction
 * Group: WorkflowEngine
 * Description: Основная секция действия Условия
 */
class WeConditionActionSchemeInfo {
  private readonly name: string = 'WeConditionAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Condition: string = 'Condition';
  readonly IsElse: string = 'IsElse';
  readonly LinkID: string = 'LinkID';
  readonly LinkName: string = 'LinkName';
  readonly LinkCaption: string = 'LinkCaption';
  readonly Description: string = 'Description';
  readonly TypeOfConditionCheck: string = 'TypeOfConditionCheck';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeDialogAction

/**
 * ID: {5aac25fd-de4f-450d-9fd5-a1a9168a795c}
 * Alias: WeDialogAction
 * Group: WorkflowEngine
 * Description: Основная секция действия Диалог
 */
class WeDialogActionSchemeInfo {
  private readonly name: string = 'WeDialogAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly DialogTypeID: string = 'DialogTypeID';
  readonly DialogTypeName: string = 'DialogTypeName';
  readonly DialogTypeCaption: string = 'DialogTypeCaption';
  readonly CardStoreModeID: string = 'CardStoreModeID';
  readonly CardStoreModeName: string = 'CardStoreModeName';
  readonly ButtonName: string = 'ButtonName';
  readonly DialogName: string = 'DialogName';
  readonly DialogAlias: string = 'DialogAlias';
  readonly OpenModeID: string = 'OpenModeID';
  readonly OpenModeName: string = 'OpenModeName';
  readonly TaskDigest: string = 'TaskDigest';
  readonly SavingScript: string = 'SavingScript';
  readonly ActionScript: string = 'ActionScript';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly TaskKindID: string = 'TaskKindID';
  readonly TaskKindCaption: string = 'TaskKindCaption';
  readonly Planned: string = 'Planned';
  readonly Period: string = 'Period';
  readonly InitScript: string = 'InitScript';
  readonly DisplayValue: string = 'DisplayValue';
  readonly KeepFiles: string = 'KeepFiles';
  readonly WithoutTask: string = 'WithoutTask';
  readonly TemplateID: string = 'TemplateID';
  readonly TemplateCaption: string = 'TemplateCaption';
  readonly IsCloseWithoutConfirmation: string = 'IsCloseWithoutConfirmation';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeDialogActionButtonLinks

/**
 * ID: {57f61e17-bd87-48cb-8efc-7a7dc56f2eef}
 * Alias: WeDialogActionButtonLinks
 * Group: WorkflowEngine
 */
class WeDialogActionButtonLinksSchemeInfo {
  private readonly name: string = 'WeDialogActionButtonLinks';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ButtonRowID: string = 'ButtonRowID';
  readonly LinkID: string = 'LinkID';
  readonly LinkName: string = 'LinkName';
  readonly LinkCaption: string = 'LinkCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeDialogActionButtons

/**
 * ID: {a99b285f-80c3-442a-85a6-2a3bfd645d2b}
 * Alias: WeDialogActionButtons
 * Group: WorkflowEngine
 * Description: Секция с настройками кнопок для действия Диалог
 */
class WeDialogActionButtonsSchemeInfo {
  private readonly name: string = 'WeDialogActionButtons';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';
  readonly TypeID: string = 'TypeID';
  readonly TypeName: string = 'TypeName';
  readonly Caption: string = 'Caption';
  readonly Icon: string = 'Icon';
  readonly Cancel: string = 'Cancel';
  readonly Order: string = 'Order';
  readonly Script: string = 'Script';
  readonly NotEnd: string = 'NotEnd';
  readonly TaskDialogRowID: string = 'TaskDialogRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeEmailAction

/**
 * ID: {3482fa35-9558-4a7c-832f-3ac94c73f2f9}
 * Alias: WeEmailAction
 * Group: WorkflowEngine
 * Description: Секция для действия отправки уведомления
 */
class WeEmailActionSchemeInfo {
  private readonly name: string = 'WeEmailAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Body: string = 'Body';
  readonly Header: string = 'Header';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly Script: string = 'Script';
  readonly NotificationTypeID: string = 'NotificationTypeID';
  readonly NotificationTypeName: string = 'NotificationTypeName';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeEmailActionOptionalRecipients

/**
 * ID: {94b08bf8-6cb2-4a11-a42b-4ff996ac71e5}
 * Alias: WeEmailActionOptionalRecipients
 * Group: WorkflowEngine
 * Description: Список опциональных получателей письма
 */
class WeEmailActionOptionalRecipientsSchemeInfo {
  private readonly name: string = 'WeEmailActionOptionalRecipients';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeEmailActionRecievers

/**
 * ID: {48d261cd-1054-41d7-9046-485e22d15060}
 * Alias: WeEmailActionRecievers
 * Group: WorkflowEngine
 * Description: Список получателей письма
 */
class WeEmailActionRecieversSchemeInfo {
  private readonly name: string = 'WeEmailActionRecievers';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeEndAction

/**
 * ID: {e36e23ae-2276-494a-a3f1-5f3cd5c56f9d}
 * Alias: WeEndAction
 * Group: WorkflowEngine
 * Description: Секция для действия Конец процесса
 */
class WeEndActionSchemeInfo {
  private readonly name: string = 'WeEndAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly FinishProcess: string = 'FinishProcess';
  readonly EndSignalID: string = 'EndSignalID';
  readonly EndSignalName: string = 'EndSignalName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeHistoryManagementAction

/**
 * ID: {bb018cba-ef03-4bb4-a7e6-8fb083fc44a4}
 * Alias: WeHistoryManagementAction
 * Group: WorkflowEngine
 */
class WeHistoryManagementActionSchemeInfo {
  private readonly name: string = 'WeHistoryManagementAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly TaskHistoryGroupTypeID: string = 'TaskHistoryGroupTypeID';
  readonly TaskHistoryGroupTypeCaption: string = 'TaskHistoryGroupTypeCaption';
  readonly ParentTaskHistoryGroupTypeID: string = 'ParentTaskHistoryGroupTypeID';
  readonly ParentTaskHistoryGroupTypeCaption: string = 'ParentTaskHistoryGroupTypeCaption';
  readonly NewIteration: string = 'NewIteration';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeScriptAction

/**
 * ID: {46f88520-33d0-45c9-bd27-20cae8fa58dc}
 * Alias: WeScriptAction
 * Group: WorkflowEngine
 * Description: Секция для действия Скрипт
 */
class WeScriptActionSchemeInfo {
  private readonly name: string = 'WeScriptAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Script: string = 'Script';
  readonly ProcessAnySignal: string = 'ProcessAnySignal';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeSendSignalAction

/**
 * ID: {fbe60ad7-091a-4f09-a57a-f1068088fa38}
 * Alias: WeSendSignalAction
 * Group: WorkflowEngine
 * Description: Основная секция для действия Отправка сигнала
 */
class WeSendSignalActionSchemeInfo {
  private readonly name: string = 'WeSendSignalAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly SignalID: string = 'SignalID';
  readonly SignalName: string = 'SignalName';
  readonly PassHash: string = 'PassHash';
  readonly Scenario: string = 'Scenario';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeStartAction

/**
 * ID: {fff6d6ad-c17e-4692-863d-07032f4b95fd}
 * Alias: WeStartAction
 * Group: WorkflowEngine
 * Description: Секция для действия Старта процесса
 */
class WeStartActionSchemeInfo {
  private readonly name: string = 'WeStartAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly StartSignalID: string = 'StartSignalID';
  readonly StartSignalName: string = 'StartSignalName';
  readonly IsNotPersistent: string = 'IsNotPersistent';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeSubprocessAction

/**
 * ID: {3d947708-6196-443f-a4e3-a1e1a5315d9d}
 * Alias: WeSubprocessAction
 * Group: WorkflowEngine
 * Description: Секция с данными действия Подпроцесс
 */
class WeSubprocessActionSchemeInfo {
  private readonly name: string = 'WeSubprocessAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly StartSignalID: string = 'StartSignalID';
  readonly StartSignalName: string = 'StartSignalName';
  readonly ProcessID: string = 'ProcessID';
  readonly ProcessName: string = 'ProcessName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeSubprocessActionEndMapping

/**
 * ID: {ea4cd339-7a97-4221-a223-44f9b6ce6ce1}
 * Alias: WeSubprocessActionEndMapping
 * Group: WorkflowEngine
 */
class WeSubprocessActionEndMappingSchemeInfo {
  private readonly name: string = 'WeSubprocessActionEndMapping';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly SourceParamID: string = 'SourceParamID';
  readonly SourceParamText: string = 'SourceParamText';
  readonly TargetParamID: string = 'TargetParamID';
  readonly TargetParamText: string = 'TargetParamText';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeSubprocessActionOptions

/**
 * ID: {428f3b30-561c-446e-b676-4ec84ba8e03a}
 * Alias: WeSubprocessActionOptions
 * Group: WorkflowEngine
 * Description: Секция с настройками переходов при получении сигналов из под-процесса
 */
class WeSubprocessActionOptionsSchemeInfo {
  private readonly name: string = 'WeSubprocessActionOptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly SignalID: string = 'SignalID';
  readonly SignalName: string = 'SignalName';
  readonly LinkID: string = 'LinkID';
  readonly LinkName: string = 'LinkName';
  readonly LinkCaption: string = 'LinkCaption';
  readonly Script: string = 'Script';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeSubprocessActionStartMapping

/**
 * ID: {a2b54bf4-20ae-4fdd-8b2e-21ef246cfb32}
 * Alias: WeSubprocessActionStartMapping
 * Group: WorkflowEngine
 * Description: Маппинг параметров процесса, передаваемых в параметры подпроцесса
 */
class WeSubprocessActionStartMappingSchemeInfo {
  private readonly name: string = 'WeSubprocessActionStartMapping';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly SourceParamID: string = 'SourceParamID';
  readonly SourceParamText: string = 'SourceParamText';
  readonly TargetParamID: string = 'TargetParamID';
  readonly TargetParamText: string = 'TargetParamText';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeSubprocessControlAction

/**
 * ID: {2f0a4a5d-6601-4cd9-9c2d-09d193d33352}
 * Alias: WeSubprocessControlAction
 * Group: WorkflowEngine
 * Description: Секция для действия Управление подпроцессом
 */
class WeSubprocessControlActionSchemeInfo {
  private readonly name: string = 'WeSubprocessControlAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly SignalID: string = 'SignalID';
  readonly SignalName: string = 'SignalName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTaskAction

/**
 * ID: {ffcaed62-a85f-43b0-b029-ed50bc562ef1}
 * Alias: WeTaskAction
 * Group: WorkflowEngine
 * Description: Секция для действия Задание
 */
class WeTaskActionSchemeInfo {
  private readonly name: string = 'WeTaskAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Digest: string = 'Digest';
  readonly TaskTypeID: string = 'TaskTypeID';
  readonly TaskTypeCaption: string = 'TaskTypeCaption';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly Period: string = 'Period';
  readonly Planned: string = 'Planned';
  readonly Result: string = 'Result';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly InitTaskScript: string = 'InitTaskScript';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTaskActionDialogs

/**
 * ID: {7c068441-e9e1-445a-a371-bf9436156428}
 * Alias: WeTaskActionDialogs
 * Group: WorkflowEngine
 * Description: Секция с настройками диалогов
 */
class WeTaskActionDialogsSchemeInfo {
  private readonly name: string = 'WeTaskActionDialogs';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly DialogTypeID: string = 'DialogTypeID';
  readonly DialogTypeName: string = 'DialogTypeName';
  readonly DialogTypeCaption: string = 'DialogTypeCaption';
  readonly CardStoreModeID: string = 'CardStoreModeID';
  readonly CardStoreModeName: string = 'CardStoreModeName';
  readonly DialogName: string = 'DialogName';
  readonly DialogAlias: string = 'DialogAlias';
  readonly SavingScript: string = 'SavingScript';
  readonly ActionScript: string = 'ActionScript';
  readonly InitScript: string = 'InitScript';
  readonly CompletionOptionID: string = 'CompletionOptionID';
  readonly CompletionOptionCaption: string = 'CompletionOptionCaption';
  readonly Order: string = 'Order';
  readonly DisplayValue: string = 'DisplayValue';
  readonly KeepFiles: string = 'KeepFiles';
  readonly TemplateID: string = 'TemplateID';
  readonly TemplateCaption: string = 'TemplateCaption';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTaskActionEvents

/**
 * ID: {797022e2-bac4-408c-b529-110943fade63}
 * Alias: WeTaskActionEvents
 * Group: WorkflowEngine
 * Description: Секция с обработчиками событий заданий
 */
class WeTaskActionEventsSchemeInfo {
  private readonly name: string = 'WeTaskActionEvents';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Script: string = 'Script';
  readonly EventID: string = 'EventID';
  readonly EventName: string = 'EventName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTaskActionNotificationRoles

/**
 * ID: {9b7fc0b0-da06-46df-a5c9-d66ecc386d55}
 * Alias: WeTaskActionNotificationRoles
 * Group: WorkflowEngine
 */
class WeTaskActionNotificationRolesSchemeInfo {
  private readonly name: string = 'WeTaskActionNotificationRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly TaskOptionRowID: string = 'TaskOptionRowID';
  readonly TaskGroupOptionRowID: string = 'TaskGroupOptionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTaskActionOptionLinks

/**
 * ID: {a3d3bf40-b37a-4118-af51-2b555da511b7}
 * Alias: WeTaskActionOptionLinks
 * Group: WorkflowEngine
 */
class WeTaskActionOptionLinksSchemeInfo {
  private readonly name: string = 'WeTaskActionOptionLinks';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly LinkID: string = 'LinkID';
  readonly LinkName: string = 'LinkName';
  readonly LinkCaption: string = 'LinkCaption';
  readonly OptionRowID: string = 'OptionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTaskActionOptions

/**
 * ID: {e30dcb0a-2a63-4f52-82f9-a12b0038d70d}
 * Alias: WeTaskActionOptions
 * Group: WorkflowEngine
 * Description: Таблица с вариантами завершения в действии задания
 */
class WeTaskActionOptionsSchemeInfo {
  private readonly name: string = 'WeTaskActionOptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';
  readonly LinkID: string = 'LinkID';
  readonly LinkName: string = 'LinkName';
  readonly LinkCaption: string = 'LinkCaption';
  readonly Script: string = 'Script';
  readonly Order: string = 'Order';
  readonly Result: string = 'Result';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly SendToPerformer: string = 'SendToPerformer';
  readonly SendToAuthor: string = 'SendToAuthor';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTaskControlAction

/**
 * ID: {adcf3458-d724-4411-9059-60bdb353a9b5}
 * Alias: WeTaskControlAction
 * Group: WorkflowEngine
 * Description: Секция для действия Управление заданием
 */
class WeTaskControlActionSchemeInfo {
  private readonly name: string = 'WeTaskControlAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Digest: string = 'Digest';
  readonly Planned: string = 'Planned';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';
  readonly ControlTypeName: string = 'ControlTypeName';
  readonly ControlTypeID: string = 'ControlTypeID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTaskControlTypes

/**
 * ID: {ab612473-e0a2-4dd7-b05e-d9bbdf06b62f}
 * Alias: WeTaskControlTypes
 * Group: WorkflowEngine
 * Description: Список доступных манипуляций над заданием из действия Управление заданием
 */
class WeTaskControlTypesSchemeInfo {
  private readonly name: string = 'WeTaskControlTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly DeleteTask: WeTaskControlTypes = new WeTaskControlTypes(0, '$WorkflowEngine_TaskControlTypes_DeleteTask');
  readonly UpdateTask: WeTaskControlTypes = new WeTaskControlTypes(1, '$WorkflowEngine_TaskControlTypes_UpdateTask');
  readonly CompleteTask: WeTaskControlTypes = new WeTaskControlTypes(2, '$WorkflowEngine_TaskControlTypes_CompleteTask');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region WeTaskControlTypes Enumeration

class WeTaskControlTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region WeTaskGroupAction

/**
 * ID: {915d8549-af3d-4d44-84a1-cef16ed89941}
 * Alias: WeTaskGroupAction
 * Group: WorkflowEngine
 * Description: Основная секция для действия Группа заданий
 */
class WeTaskGroupActionSchemeInfo {
  private readonly name: string = 'WeTaskGroupAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Digest: string = 'Digest';
  readonly Period: string = 'Period';
  readonly Planned: string = 'Planned';
  readonly Result: string = 'Result';
  readonly Parallel: string = 'Parallel';
  readonly TaskTypeID: string = 'TaskTypeID';
  readonly TaskTypeCaption: string = 'TaskTypeCaption';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly InitTaskScript: string = 'InitTaskScript';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTaskGroupActionOptionLinks

/**
 * ID: {1e26efb8-a6ee-4582-9ac3-88da4ef74d24}
 * Alias: WeTaskGroupActionOptionLinks
 * Group: WorkflowEngine
 */
class WeTaskGroupActionOptionLinksSchemeInfo {
  private readonly name: string = 'WeTaskGroupActionOptionLinks';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly LinkID: string = 'LinkID';
  readonly LinkName: string = 'LinkName';
  readonly LinkCaption: string = 'LinkCaption';
  readonly OptionRowID: string = 'OptionRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTaskGroupActionOptions

/**
 * ID: {dee05376-8267-42b9-8cc9-1ff5bb58bb06}
 * Alias: WeTaskGroupActionOptions
 * Group: WorkflowEngine
 * Description: Секция с настройками вариантов завершения заданий
 */
class WeTaskGroupActionOptionsSchemeInfo {
  private readonly name: string = 'WeTaskGroupActionOptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';
  readonly LinkID: string = 'LinkID';
  readonly LinkName: string = 'LinkName';
  readonly LinkCaption: string = 'LinkCaption';
  readonly Script: string = 'Script';
  readonly CancelGroup: string = 'CancelGroup';
  readonly OptionTypeName: string = 'OptionTypeName';
  readonly OptionTypeID: string = 'OptionTypeID';
  readonly Order: string = 'Order';
  readonly PauseGroup: string = 'PauseGroup';
  readonly CancelOptionID: string = 'CancelOptionID';
  readonly CancelOptionCaption: string = 'CancelOptionCaption';
  readonly NewRoleID: string = 'NewRoleID';
  readonly NewRoleName: string = 'NewRoleName';
  readonly UseAsNextRole: string = 'UseAsNextRole';
  readonly Result: string = 'Result';
  readonly NotificationID: string = 'NotificationID';
  readonly NotificationName: string = 'NotificationName';
  readonly SendToPerformer: string = 'SendToPerformer';
  readonly SendToAuthor: string = 'SendToAuthor';
  readonly ExcludeDeputies: string = 'ExcludeDeputies';
  readonly ExcludeSubscribers: string = 'ExcludeSubscribers';
  readonly NotificationScript: string = 'NotificationScript';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTaskGroupActionOptionTypes

/**
 * ID: {dc9eb404-c42d-40ab-a4c0-3b8b6089b926}
 * Alias: WeTaskGroupActionOptionTypes
 * Group: WorkflowEngine
 * Description: Список допустимых условий выполнения перехода
 */
class WeTaskGroupActionOptionTypesSchemeInfo {
  private readonly name: string = 'WeTaskGroupActionOptionTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly OneNow: WeTaskGroupActionOptionTypes = new WeTaskGroupActionOptionTypes(0, '$WorkflowEngine_TaskGroupOptionTypes_OneNow');
  readonly OneAfterAll: WeTaskGroupActionOptionTypes = new WeTaskGroupActionOptionTypes(1, '$WorkflowEngine_TaskGroupOptionTypes_OneAfterAll');
  readonly All: WeTaskGroupActionOptionTypes = new WeTaskGroupActionOptionTypes(2, '$WorkflowEngine_TaskGroupOptionTypes_All');
  readonly AfterFinish: WeTaskGroupActionOptionTypes = new WeTaskGroupActionOptionTypes(3, '$WorkflowEngine_TaskGroupOptionTypes_AfterFinish');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region WeTaskGroupActionOptionTypes Enumeration

class WeTaskGroupActionOptionTypes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region WeTaskGroupActionRoles

/**
 * ID: {0656f18d-bb1c-47c9-8d40-24300c7f4b53}
 * Alias: WeTaskGroupActionRoles
 * Group: WorkflowEngine
 * Description: Секция со списком ролей для действия Группа заданий
 */
class WeTaskGroupActionRolesSchemeInfo {
  private readonly name: string = 'WeTaskGroupActionRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTaskGroupControlAction

/**
 * ID: {02a2a16e-7915-4a03-86b0-08b074b78c67}
 * Alias: WeTaskGroupControlAction
 * Group: WorkflowEngine
 * Description: Основная секция для действия управление группой заданий
 */
class WeTaskGroupControlActionSchemeInfo {
  private readonly name: string = 'WeTaskGroupControlAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly ResumeGroup: string = 'ResumeGroup';
  readonly PauseGroup: string = 'PauseGroup';
  readonly CancelGroup: string = 'CancelGroup';
  readonly CancelOptionID: string = 'CancelOptionID';
  readonly CancelOptionCaption: string = 'CancelOptionCaption';
  readonly UseAsNextRole: string = 'UseAsNextRole';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTimerAction

/**
 * ID: {318965a6-fcec-432d-8ba3-3b972fb2b750}
 * Alias: WeTimerAction
 * Group: WorkflowEngine
 * Description: Секция для действия Таймер
 */
class WeTimerActionSchemeInfo {
  private readonly name: string = 'WeTimerAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RunOnce: string = 'RunOnce';
  readonly Period: string = 'Period';
  readonly Cron: string = 'Cron';
  readonly Date: string = 'Date';
  readonly StopCondition: string = 'StopCondition';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WeTimerControlAction

/**
 * ID: {38c4dd25-e26e-469c-8072-6498f33a0d06}
 * Alias: WeTimerControlAction
 * Group: WorkflowEngine
 * Description: Секция для действия Управление таймером
 */
class WeTimerControlActionSchemeInfo {
  private readonly name: string = 'WeTimerControlAction';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Period: string = 'Period';
  readonly Cron: string = 'Cron';
  readonly Stop: string = 'Stop';
  readonly Date: string = 'Date';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WfResolutionChildren

/**
 * ID: {d4f683a4-a1e9-4fc1-ae84-2c4ab304b7fb}
 * Alias: WfResolutionChildren
 * Group: Wf
 * Description: Записи для дочерних резолюций.
 * Колонка RowID содержит идентификатор дочернего задания.
 * Если поле IsCompleted = True, то дочерняя резолюция была завершена и задание больше не существует.
 */
class WfResolutionChildrenSchemeInfo {
  private readonly name: string = 'WfResolutionChildren';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly PerformerID: string = 'PerformerID';
  readonly PerformerName: string = 'PerformerName';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';
  readonly Comment: string = 'Comment';
  readonly Answer: string = 'Answer';
  readonly Created: string = 'Created';
  readonly Planned: string = 'Planned';
  readonly InProgress: string = 'InProgress';
  readonly Completed: string = 'Completed';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WfResolutionChildrenVirtual

/**
 * ID: {17dcbbe4-108a-4f15-8716-f7d2718f0953}
 * Alias: WfResolutionChildrenVirtual
 * Group: Wf
 * Description: Таблица с информацией по дочерним резолюциям. Используются в заданиях совместно с таблицей WfResolutions.
 * Таблица является виртуальной и заполняется автоматически в расширениях.
 * Колонка RowID содержит идентификатор дочернего задания.
 */
class WfResolutionChildrenVirtualSchemeInfo {
  private readonly name: string = 'WfResolutionChildrenVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly PerformerID: string = 'PerformerID';
  readonly PerformerName: string = 'PerformerName';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly OptionID: string = 'OptionID';
  readonly OptionCaption: string = 'OptionCaption';
  readonly Comment: string = 'Comment';
  readonly Answer: string = 'Answer';
  readonly Created: string = 'Created';
  readonly InProgress: string = 'InProgress';
  readonly Planned: string = 'Planned';
  readonly Completed: string = 'Completed';
  readonly ColumnComment: string = 'ColumnComment';
  readonly ColumnState: string = 'ColumnState';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WfResolutionPerformers

/**
 * ID: {0f62f90e-6b94-4301-866d-0138fb147939}
 * Alias: WfResolutionPerformers
 * Group: Wf
 * Description: Исполнители создаваемой резолюции. Используются в заданиях совместно с таблицей WfResolutions.
 * В качестве исполнителя могут выступать несколько контекстных или обычных ролей.
 * Если указано более одной роли, то резолюция назначается на роль задания "Исполнители задания".
 */
class WfResolutionPerformersSchemeInfo {
  private readonly name: string = 'WfResolutionPerformers';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WfResolutions

/**
 * ID: {6a0f5914-6a44-4e7d-b400-6b82ec1e2209}
 * Alias: WfResolutions
 * Group: Wf
 * Description: Задание резолюции, построенное на Workflow.
 * Содержит как информацию по заданию, так и информацию по тому, какие поля будут заполняться для действий с резолюцией.
 */
class WfResolutionsSchemeInfo {
  private readonly name: string = 'WfResolutions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly KindID: string = 'KindID';
  readonly KindCaption: string = 'KindCaption';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly ControllerID: string = 'ControllerID';
  readonly ControllerName: string = 'ControllerName';
  readonly Comment: string = 'Comment';
  readonly Planned: string = 'Planned';
  readonly DurationInDays: string = 'DurationInDays';
  readonly RevokeChildren: string = 'RevokeChildren';
  readonly WithControl: string = 'WithControl';
  readonly ShowAdditional: string = 'ShowAdditional';
  readonly MassCreation: string = 'MassCreation';
  readonly ParentComment: string = 'ParentComment';
  readonly MajorPerformer: string = 'MajorPerformer';
  readonly SenderID: string = 'SenderID';
  readonly SenderName: string = 'SenderName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WfResolutionsVirtual

/**
 * ID: {1f805af5-f412-4878-9d70-af989b905fb5}
 * Alias: WfResolutionsVirtual
 * Group: Wf
 */
class WfResolutionsVirtualSchemeInfo {
  private readonly name: string = 'WfResolutionsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Planned: string = 'Planned';
  readonly Digest: string = 'Digest';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WfSatellite

/**
 * ID: {05394727-2b6f-4d59-9900-d95bc8effdc5}
 * Alias: WfSatellite
 * Group: Wf
 * Description: Основная секция карточки-сателлита для бизнес-процессов Workflow.
 */
class WfSatelliteSchemeInfo {
  private readonly name: string = 'WfSatellite';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Data: string = 'Data';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WfSatelliteTaskHistory

/**
 * ID: {cd241343-4eb1-425f-b534-f9ff4cfa597e}
 * Alias: WfSatelliteTaskHistory
 * Group: Wf
 * Description: Дополнительная информация по истории заданий в карточке-сателлите Workflow.
 * ID - идентификатор карточки-сателлита WfSatellite.
 * RowID - идентификатор задания (он же идентификатор записи в истории заданий TaskHistory после того, как задание завершено).
 */
class WfSatelliteTaskHistorySchemeInfo {
  private readonly name: string = 'WfSatelliteTaskHistory';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ControllerID: string = 'ControllerID';
  readonly ControllerName: string = 'ControllerName';
  readonly Controlled: string = 'Controlled';
  readonly AliveSubtasks: string = 'AliveSubtasks';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WfTaskCardsVirtual

/**
 * ID: {ef5f3db3-95d9-4654-91a4-87dcd3d2195a}
 * Alias: WfTaskCardsVirtual
 * Group: Wf
 * Description: Виртуальная секция для карточек-сателлитов для задач.
 */
class WfTaskCardsVirtualSchemeInfo {
  private readonly name: string = 'WfTaskCardsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly DocTypeID: string = 'DocTypeID';
  readonly DocTypeTitle: string = 'DocTypeTitle';
  readonly Number: string = 'Number';
  readonly FullNumber: string = 'FullNumber';
  readonly Sequence: string = 'Sequence';
  readonly Subject: string = 'Subject';
  readonly DocDate: string = 'DocDate';
  readonly CreationDate: string = 'CreationDate';
  readonly StateModified: string = 'StateModified';
  readonly MainCardDigest: string = 'MainCardDigest';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly RegistratorID: string = 'RegistratorID';
  readonly RegistratorName: string = 'RegistratorName';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowActions

/**
 * ID: {df81680b-406f-4f50-9df2-c14dda232aea}
 * Alias: WorkflowActions
 * Group: WorkflowEngine
 * Description: Секция со списком действий
 */
class WorkflowActionsSchemeInfo {
  private readonly name: string = 'WorkflowActions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Order: string = 'Order';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';
  readonly ActionType: string = 'ActionType';
  readonly HasPreCondition: string = 'HasPreCondition';
  readonly HasPreScript: string = 'HasPreScript';
  readonly HasPostScript: string = 'HasPostScript';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowCounters

/**
 * ID: {7adfd330-ab0e-458f-9ac4-f2060bde8c97}
 * Alias: WorkflowCounters
 * Group: Workflow
 * Description: Счётчики заданий, используемые для реализации блоков "И" в Workflow.
 */
class WorkflowCountersSchemeInfo {
  private readonly name: string = 'WorkflowCounters';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Number: string = 'Number';
  readonly CurrentValue: string = 'CurrentValue';
  readonly ProcessRowID: string = 'ProcessRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowDefaultSubscriptions

/**
 * ID: {d8b78ce3-bedf-4faa-9fba-75ddbecf4e04}
 * Alias: WorkflowDefaultSubscriptions
 * Group: WorkflowEngine
 * Description: Секция с подписками узла по умолчанию
 */
class WorkflowDefaultSubscriptionsSchemeInfo {
  private readonly name: string = 'WorkflowDefaultSubscriptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly SignalID: string = 'SignalID';
  readonly SignalName: string = 'SignalName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineCheckContextRole

/**
 * ID: {7d0b5402-9d55-4269-964d-25b5ddcb2690}
 * Alias: WorkflowEngineCheckContextRole
 * Group: WorkflowEngine
 * Description: Секция со списком контекстных ролей для расширения на тайлы "Проверка контекстных ролей"
 */
class WorkflowEngineCheckContextRoleSchemeInfo {
  private readonly name: string = 'WorkflowEngineCheckContextRole';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineCommandSubscriptions

/**
 * ID: {7c45a604-9175-45bd-8525-f218a465b77b}
 * Alias: WorkflowEngineCommandSubscriptions
 * Group: WorkflowEngine
 * Description: Подписки узлов на внешнюю команду
 */
class WorkflowEngineCommandSubscriptionsSchemeInfo {
  private readonly name: string = 'WorkflowEngineCommandSubscriptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Command: string = 'Command';
  readonly NodeRowID: string = 'NodeRowID';
  readonly ProcessRowID: string = 'ProcessRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineErrors

/**
 * ID: {61905471-1e69-4478-946f-772f11152386}
 * Alias: WorkflowEngineErrors
 * Group: WorkflowEngine
 * Description: Секция с ошибками обработки
 */
class WorkflowEngineErrorsSchemeInfo {
  private readonly name: string = 'WorkflowEngineErrors';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ErrorCardID: string = 'ErrorCardID';
  readonly NodeRowID: string = 'NodeRowID';
  readonly Active: string = 'Active';
  readonly ErrorData: string = 'ErrorData';
  readonly Added: string = 'Added';
  readonly IsAsync: string = 'IsAsync';
  readonly Resumable: string = 'Resumable';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineLogLevels

/**
 * ID: {9d29f065-3c4b-4209-af8d-10b699895231}
 * Alias: WorkflowEngineLogLevels
 * Group: WorkflowEngine
 * Description: Уровни логирования в WorkflowEngine
 */
class WorkflowEngineLogLevelsSchemeInfo {
  private readonly name: string = 'WorkflowEngineLogLevels';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly None: WorkflowEngineLogLevels = new WorkflowEngineLogLevels(0, 'None');
  readonly Error: WorkflowEngineLogLevels = new WorkflowEngineLogLevels(1, 'Error');
  readonly Info: WorkflowEngineLogLevels = new WorkflowEngineLogLevels(2, 'Info');
  readonly Debug: WorkflowEngineLogLevels = new WorkflowEngineLogLevels(3, 'Debug');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region WorkflowEngineLogLevels Enumeration

class WorkflowEngineLogLevels {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region WorkflowEngineLogs

/**
 * ID: {f3fa0390-6444-4df6-8be5-cbad1fdd153e}
 * Alias: WorkflowEngineLogs
 * Group: WorkflowEngine
 * Description: Секция с логами процесса
 */
class WorkflowEngineLogsSchemeInfo {
  private readonly name: string = 'WorkflowEngineLogs';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ProcessRowID: string = 'ProcessRowID';
  readonly LogLevelID: string = 'LogLevelID';
  readonly ObjectName: string = 'ObjectName';
  readonly Text: string = 'Text';
  readonly Added: string = 'Added';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineNodes

/**
 * ID: {69f72d3a-97c1-4d67-a348-071ab861b3c7}
 * Alias: WorkflowEngineNodes
 * Group: WorkflowEngine
 * Description: Содержит состояния активных узлов
 */
class WorkflowEngineNodesSchemeInfo {
  private readonly name: string = 'WorkflowEngineNodes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ProcessRowID: string = 'ProcessRowID';
  readonly NodeData: string = 'NodeData';
  readonly NodeID: string = 'NodeID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineProcesses

/**
 * ID: {27debe30-ae5f-4f69-89c9-5706e1592540}
 * Alias: WorkflowEngineProcesses
 * Group: WorkflowEngine
 * Description: Содержит состояния активных процессов
 */
class WorkflowEngineProcessesSchemeInfo {
  private readonly name: string = 'WorkflowEngineProcesses';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ProcessData: string = 'ProcessData';
  readonly ProcessTemplateRowID: string = 'ProcessTemplateRowID';
  readonly ProcessTemplateID: string = 'ProcessTemplateID';
  readonly CardID: string = 'CardID';
  readonly CardDigest: string = 'CardDigest';
  readonly Created: string = 'Created';
  readonly ParentRowID: string = 'ParentRowID';
  readonly Name: string = 'Name';
  readonly LastActivity: string = 'LastActivity';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineSettingsAdminRoles

/**
 * ID: {e493b168-0c0a-4ebc-812e-229bc43aec25}
 * Alias: WorkflowEngineSettingsAdminRoles
 * Group: WorkflowEngine
 * Description: Список ролей, имеющих админские права к карточке шаблона БП
 */
class WorkflowEngineSettingsAdminRolesSchemeInfo {
  private readonly name: string = 'WorkflowEngineSettingsAdminRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineSettingsCreateRoles

/**
 * ID: {9097ff9c-04a8-4af1-8921-7df7323d46f4}
 * Alias: WorkflowEngineSettingsCreateRoles
 * Group: WorkflowEngine
 * Description: Список ролей, имеющих доступ на создание карточек шаблона БП
 */
class WorkflowEngineSettingsCreateRolesSchemeInfo {
  private readonly name: string = 'WorkflowEngineSettingsCreateRoles';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineSettingsObjectTypeFields

/**
 * ID: {6efad92a-46be-4d44-a495-271b264f016b}
 * Alias: WorkflowEngineSettingsObjectTypeFields
 * Group: WorkflowEngine
 * Description: Поля для типа объекта
 */
class WorkflowEngineSettingsObjectTypeFieldsSchemeInfo {
  private readonly name: string = 'WorkflowEngineSettingsObjectTypeFields';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly FieldID: string = 'FieldID';
  readonly FieldName: string = 'FieldName';
  readonly ParentRowID: string = 'ParentRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineSettingsObjectTypes

/**
 * ID: {140a411c-3b68-44c0-8a5b-cf641d2421f2}
 * Alias: WorkflowEngineSettingsObjectTypes
 * Group: WorkflowEngine
 * Description: Секция с типами для редактора процессов
 */
class WorkflowEngineSettingsObjectTypesSchemeInfo {
  private readonly name: string = 'WorkflowEngineSettingsObjectTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';
  readonly RefSection: string = 'RefSection';
  readonly TableID: string = 'TableID';
  readonly TableName: string = 'TableName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineSubprocessSubscriptions

/**
 * ID: {1c83c672-7de7-47f5-8c63-ec41bb5aa7ca}
 * Alias: WorkflowEngineSubprocessSubscriptions
 * Group: WorkflowEngine
 * Description: Подписки узлов к подпроцессам
 */
class WorkflowEngineSubprocessSubscriptionsSchemeInfo {
  private readonly name: string = 'WorkflowEngineSubprocessSubscriptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly SubprocessRowID: string = 'SubprocessRowID';
  readonly NodeRowID: string = 'NodeRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineTaskActions

/**
 * ID: {857ef2b9-6bdb-4913-bbc2-2cf9d1ae0b55}
 * Alias: WorkflowEngineTaskActions
 * Group: WorkflowEngine
 * Description: Список возможных действий над заданием
 */
class WorkflowEngineTaskActionsSchemeInfo {
  private readonly name: string = 'WorkflowEngineTaskActions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly InProgress: WorkflowEngineTaskActions = new WorkflowEngineTaskActions(0, '$WorkflowEngine_TaskActions_InProgress');
  readonly ReturnToRole: WorkflowEngineTaskActions = new WorkflowEngineTaskActions(1, '$WorkflowEngine_TaskActions_ReturnToRole');
  readonly Postpone: WorkflowEngineTaskActions = new WorkflowEngineTaskActions(2, '$WorkflowEngine_TaskActions_Postpone');
  readonly ReturnFromPostpone: WorkflowEngineTaskActions = new WorkflowEngineTaskActions(3, '$WorkflowEngine_TaskActions_ReturnFromPostpone');
  readonly Complete: WorkflowEngineTaskActions = new WorkflowEngineTaskActions(4, '$WorkflowEngine_TaskActions_Complete');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region WorkflowEngineTaskActions Enumeration

class WorkflowEngineTaskActions {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region WorkflowEngineTaskSubscriptions

/**
 * ID: {5ee285b4-a72c-4a41-88a1-3e052fa1ee44}
 * Alias: WorkflowEngineTaskSubscriptions
 * Group: WorkflowEngine
 * Description: Подписки узлов на действия из заданий
 */
class WorkflowEngineTaskSubscriptionsSchemeInfo {
  private readonly name: string = 'WorkflowEngineTaskSubscriptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TaskID: string = 'TaskID';
  readonly NodeRowID: string = 'NodeRowID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineTimerSubscriptions

/**
 * ID: {9c65ad25-7d88-4e7c-b398-26d45d1d7204}
 * Alias: WorkflowEngineTimerSubscriptions
 * Group: WorkflowEngine
 * Description: Таблица с подписками таймеров
 */
class WorkflowEngineTimerSubscriptionsSchemeInfo {
  private readonly name: string = 'WorkflowEngineTimerSubscriptions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly NodeRowID: string = 'NodeRowID';
  readonly Period: string = 'Period';
  readonly Cron: string = 'Cron';
  readonly Date: string = 'Date';
  readonly RunOnce: string = 'RunOnce';
  readonly Modified: string = 'Modified';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowInLinks

/**
 * ID: {83bf8e43-0292-4fb8-ac1d-6e36c8ba99a6}
 * Alias: WorkflowInLinks
 * Group: WorkflowEngine
 * Description: Список входящих в узел связей
 */
class WorkflowInLinksSchemeInfo {
  private readonly name: string = 'WorkflowInLinks';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';
  readonly Script: string = 'Script';
  readonly HasCondition: string = 'HasCondition';
  readonly Description: string = 'Description';
  readonly IsAsync: string = 'IsAsync';
  readonly LockProcess: string = 'LockProcess';
  readonly LinkModeID: string = 'LinkModeID';
  readonly LinkModeName: string = 'LinkModeName';
  readonly SignalProcessingModeID: string = 'SignalProcessingModeID';
  readonly SignalProcessingModeName: string = 'SignalProcessingModeName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowLinkModes

/**
 * ID: {29b2fb61-6880-43de-a40f-6688e1d0e247}
 * Alias: WorkflowLinkModes
 * Group: WorkflowEngine
 * Description: Типы связи для переходов
 */
class WorkflowLinkModesSchemeInfo {
  private readonly name: string = 'WorkflowLinkModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Default: WorkflowLinkModes = new WorkflowLinkModes(0, '$WorkflowEngine_LinkModes_Default');
  readonly AlwaysCreateNew: WorkflowLinkModes = new WorkflowLinkModes(1, '$WorkflowEngine_LinkModes_AlwaysCreateNew');
  readonly NeverCreateNew: WorkflowLinkModes = new WorkflowLinkModes(2, '$WorkflowEngine_LinkModes_NeverCreateNew');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region WorkflowLinkModes Enumeration

class WorkflowLinkModes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region WorkflowLinks

/**
 * ID: {9764baef-636c-4558-86cb-0b7e4360f771}
 * Alias: WorkflowLinks
 * Group: WorkflowEngine
 * Description: Секция с параметрами перехода
 */
class WorkflowLinksSchemeInfo {
  private readonly name: string = 'WorkflowLinks';

  //#region Columns

  readonly ID: string = 'ID';
  readonly InScript: string = 'InScript';
  readonly OutScript: string = 'OutScript';
  readonly InDescription: string = 'InDescription';
  readonly OutDescription: string = 'OutDescription';
  readonly IsAsync: string = 'IsAsync';
  readonly LockProcess: string = 'LockProcess';
  readonly LinkModeID: string = 'LinkModeID';
  readonly LinkModeName: string = 'LinkModeName';
  readonly SignalProcessingModeID: string = 'SignalProcessingModeID';
  readonly SignalProcessingModeName: string = 'SignalProcessingModeName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowMain

/**
 * ID: {87f7e0c3-2d97-4e36-bb14-1aeec6e67a94}
 * Alias: WorkflowMain
 * Group: WorkflowEngine
 * Description: Основноя таблица для объектов WorkflowEngine
 */
class WorkflowMainSchemeInfo {
  private readonly name: string = 'WorkflowMain';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';
  readonly PreScript: string = 'PreScript';
  readonly PostScript: string = 'PostScript';
  readonly Icon: string = 'Icon';
  readonly Group: string = 'Group';
  readonly GlobalScript: string = 'GlobalScript';
  readonly Description: string = 'Description';
  readonly ParentTypeID: string = 'ParentTypeID';
  readonly ParentTypeName: string = 'ParentTypeName';
  readonly LogLevelID: string = 'LogLevelID';
  readonly LogLevelName: string = 'LogLevelName';
  readonly PreScriptProcessAnySignal: string = 'PreScriptProcessAnySignal';
  readonly PostScriptProcessAnySignal: string = 'PostScriptProcessAnySignal';
  readonly ProjectPath: string = 'ProjectPath';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowNodeInstances

/**
 * ID: {e2eda913-f68f-4d42-88ba-25f80bd4c3e5}
 * Alias: WorkflowNodeInstances
 * Group: WorkflowEngine
 * Description: Список экземпляров узлов в экзепляре процесса
 */
class WorkflowNodeInstancesSchemeInfo {
  private readonly name: string = 'WorkflowNodeInstances';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowNodeInstanceSubprocesses

/**
 * ID: {830b89cf-862c-4f6a-b564-d538d0bbec90}
 * Alias: WorkflowNodeInstanceSubprocesses
 * Group: WorkflowEngine
 * Description: Секция с отображением подпроцессов, привязанных у экземпляру узла
 */
class WorkflowNodeInstanceSubprocessesSchemeInfo {
  private readonly name: string = 'WorkflowNodeInstanceSubprocesses';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Created: string = 'Created';
  readonly Name: string = 'Name';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowNodeInstanceTasks

/**
 * ID: {6ba32f52-56a3-4319-968b-90f1651cc5a7}
 * Alias: WorkflowNodeInstanceTasks
 * Group: WorkflowEngine
 * Description: Секция с отображением заданий, привязанных к экземпляру узла
 */
class WorkflowNodeInstanceTasksSchemeInfo {
  private readonly name: string = 'WorkflowNodeInstanceTasks';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly UserID: string = 'UserID';
  readonly UserName: string = 'UserName';
  readonly Planned: string = 'Planned';
  readonly InProgress: string = 'InProgress';
  readonly TypeID: string = 'TypeID';
  readonly TypeCaption: string = 'TypeCaption';
  readonly Digest: string = 'Digest';
  readonly AuthorID: string = 'AuthorID';
  readonly AuthorName: string = 'AuthorName';
  readonly Postponed: string = 'Postponed';
  readonly PostponedTo: string = 'PostponedTo';
  readonly Created: string = 'Created';
  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowOutLinks

/**
 * ID: {03d962fa-c020-481a-90bd-932cbbd4368d}
 * Alias: WorkflowOutLinks
 * Group: WorkflowEngine
 * Description: Список исходящих из узла связей
 */
class WorkflowOutLinksSchemeInfo {
  private readonly name: string = 'WorkflowOutLinks';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Name: string = 'Name';
  readonly Caption: string = 'Caption';
  readonly Script: string = 'Script';
  readonly HasCondition: string = 'HasCondition';
  readonly Description: string = 'Description';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowPreConditions

/**
 * ID: {1290310e-0b81-4560-8996-71f5bcb3a9a3}
 * Alias: WorkflowPreConditions
 * Group: WorkflowEngine
 * Description: Список обрабатываемых типов событий
 */
class WorkflowPreConditionsSchemeInfo {
  private readonly name: string = 'WorkflowPreConditions';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly SignalID: string = 'SignalID';
  readonly SignalName: string = 'SignalName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowProcessErrorsVirtual

/**
 * ID: {f7ebd016-ef99-4dfd-ba04-11f428395fe3}
 * Alias: WorkflowProcessErrorsVirtual
 * Group: WorkflowEngine
 */
class WorkflowProcessErrorsVirtualSchemeInfo {
  private readonly name: string = 'WorkflowProcessErrorsVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly Added: string = 'Added';
  readonly Text: string = 'Text';
  readonly NodeInstanceID: string = 'NodeInstanceID';
  readonly IsAsync: string = 'IsAsync';
  readonly Resumable: string = 'Resumable';
  readonly NodeID: string = 'NodeID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowProcesses

/**
 * ID: {a2db2754-b0ca-4d38-988d-0de6d58057cb}
 * Alias: WorkflowProcesses
 * Group: Workflow
 * Description: Информация по подпроцессам в бизнес-процессе.
 */
class WorkflowProcessesSchemeInfo {
  private readonly name: string = 'WorkflowProcesses';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly TypeName: string = 'TypeName';
  readonly Params: string = 'Params';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkflowSignalProcessingModes

/**
 * ID: {67b602c1-ea47-4716-92ba-81f625ba36f1}
 * Alias: WorkflowSignalProcessingModes
 * Group: WorkflowEngine
 */
class WorkflowSignalProcessingModesSchemeInfo {
  private readonly name: string = 'WorkflowSignalProcessingModes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Default: WorkflowSignalProcessingModes = new WorkflowSignalProcessingModes(0, '$WorkflowEngine_SignalProcessingMode_Default');
  readonly Async: WorkflowSignalProcessingModes = new WorkflowSignalProcessingModes(1, '$WorkflowEngine_SignalProcessingMode_Async');
  readonly AfterUploadingFiles: WorkflowSignalProcessingModes = new WorkflowSignalProcessingModes(2, '$WorkflowEngine_SignalProcessingMode_AfterUploadingFiles');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region WorkflowSignalProcessingModes Enumeration

class WorkflowSignalProcessingModes {
  readonly ID: number;
  readonly Name: string | null;

  constructor (ID: number, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region WorkflowSignalTypes

/**
 * ID: {53dc8c0b-391a-4fbd-86c0-3da697abf065}
 * Alias: WorkflowSignalTypes
 * Group: WorkflowEngine
 * Description: Список типов переходов, доступных для выбора в редакторе бизнес-процессов
 */
class WorkflowSignalTypesSchemeInfo {
  private readonly name: string = 'WorkflowSignalTypes';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region Enumeration

  readonly Default: WorkflowSignalTypes = new WorkflowSignalTypes('7af6f549-66fb-438f-a949-f70f4b8e0a15', 'Default');
  readonly Exit: WorkflowSignalTypes = new WorkflowSignalTypes('f24d3d1c-6e83-4aa6-b76a-2fac7df1f492', 'Exit');
  readonly CompleteTask: WorkflowSignalTypes = new WorkflowSignalTypes('8bf6e5bb-274b-44b4-a1bd-ae9fd9015b17', 'CompleteTask');
  readonly DeleteTask: WorkflowSignalTypes = new WorkflowSignalTypes('38802cf7-c3df-415c-b682-cdae6feff6ce', 'DeleteTask');
  readonly ReinstateTask: WorkflowSignalTypes = new WorkflowSignalTypes('48783b4c-4391-476c-b717-dae1a0cbf6b2', 'ReinstateTask');
  readonly ProgressTask: WorkflowSignalTypes = new WorkflowSignalTypes('507870ba-ae82-4be6-9f03-803608982629', 'ProgressTask');
  readonly PostponeTask: WorkflowSignalTypes = new WorkflowSignalTypes('27b9fd4d-b42c-4790-8fe2-0d5f3e9fbdfc', 'PostponeTask');
  readonly ReturnFromPostponeTask: WorkflowSignalTypes = new WorkflowSignalTypes('5c6dda50-0a03-4430-a328-3f7edda291ed', 'ReturnFromPostponeTask');
  readonly UpdateTask: WorkflowSignalTypes = new WorkflowSignalTypes('bb667fef-0885-4dc3-9360-6651705f0bac', 'UpdateTask');
  readonly SubprocessControl: WorkflowSignalTypes = new WorkflowSignalTypes('12f172cd-7e80-45e3-a908-a58ca24c101c', 'SubprocessControl');
  readonly Start: WorkflowSignalTypes = new WorkflowSignalTypes('893427ba-1d2d-4369-b7fa-c28e53997846', 'Start');
  readonly UpdateTimer: WorkflowSignalTypes = new WorkflowSignalTypes('2ee28367-0432-4c10-8571-a29a872e1ec5', 'UpdateTimer');
  readonly StopTimer: WorkflowSignalTypes = new WorkflowSignalTypes('1b2eeb0c-5bda-495e-a6a0-c09f8f5bae49', 'StopTimer');
  readonly TimerTick: WorkflowSignalTypes = new WorkflowSignalTypes('75cec30e-7b67-445f-9ba6-887e430b4cc6', 'TimerTick');
  readonly SubprocessCompleted: WorkflowSignalTypes = new WorkflowSignalTypes('380b9b0c-a2c3-4e98-8d5a-b910d6bfcca2', 'SubprocessCompleted');
  readonly TaskGroupControl: WorkflowSignalTypes = new WorkflowSignalTypes('00cbaffe-5c4e-4cba-8f74-cbe796b737e9', 'TaskGroupControl');

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#region WorkflowSignalTypes Enumeration

class WorkflowSignalTypes {
  readonly ID: guid;
  readonly Name: string | null;

  constructor (ID: guid, Name: string | null) {
    this.ID = ID;
    this.Name = Name;
  }
}

//#endregion

//#endregion

//#region WorkflowTasks

/**
 * ID: {d2683167-0425-4093-ba65-0196ded5437a}
 * Alias: WorkflowTasks
 * Group: Workflow
 * Description: Список активных заданий Workflow. В качестве RowID используется идентификатор задания.
 */
class WorkflowTasksSchemeInfo {
  private readonly name: string = 'WorkflowTasks';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly ProcessRowID: string = 'ProcessRowID';
  readonly Params: string = 'Params';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkplaceRoles

/**
 * ID: {ad21dc6e-c694-4862-ba61-1df6b7506101}
 * Alias: WorkplaceRoles
 * Group: System
 * Description: Роли, которые могут использовать рабочее место.
 */
class WorkplaceRolesSchemeInfo {
  private readonly name: string = 'WorkplaceRoles';

  //#region Columns

  readonly RoleID: string = 'RoleID';
  readonly WorkplaceID: string = 'WorkplaceID';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkplaceRolesVirtual

/**
 * ID: {67f548c6-9fdf-44c1-9d61-eea3098021f5}
 * Alias: WorkplaceRolesVirtual
 * Group: System
 * Description: Список ролей для представлений, отображаемых как виртуальные карточки в клиенте.
 */
class WorkplaceRolesVirtualSchemeInfo {
  private readonly name: string = 'WorkplaceRolesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly RowID: string = 'RowID';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Workplaces

/**
 * ID: {21cd7a4f-6930-4746-9a57-72481e951b02}
 * Alias: Workplaces
 * Group: System
 * Description: Рабочие места.
 */
class WorkplacesSchemeInfo {
  private readonly name: string = 'Workplaces';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';
  readonly Metadata: string = 'Metadata';
  readonly Order: string = 'Order';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region WorkplacesVirtual

/**
 * ID: {a2f0c6b0-32c0-4c2e-97c4-e431ef93fc84}
 * Alias: WorkplacesVirtual
 * Group: System
 * Description: Рабочие места, отображаемые как виртуальные карточки в клиенте.
 */
class WorkplacesVirtualSchemeInfo {
  private readonly name: string = 'WorkplacesVirtual';

  //#region Columns

  readonly ID: string = 'ID';
  readonly Name: string = 'Name';

  //#endregion

  //#region toString

  get getName(): string {
    return this.name;
  }

  //#endregion
}

//#endregion


//#endregion

export class SchemeInfo {
  //#region Tables

  /**
   * Table identifier for "AccessLevels": {648381d6-8647-4ec6-87a4-3cbd6bae380c}.
   */
  static get AccessLevels(): AccessLevelsSchemeInfo {
    return SchemeInfo.accessLevels = SchemeInfo.accessLevels ?? new AccessLevelsSchemeInfo();
  }

  private static accessLevels: AccessLevelsSchemeInfo;

  /**
   * Table identifier for "Acl": {b7538557-04c6-43d4-9d7a-4412dc1ed103}.
   */
  static get Acl(): AclSchemeInfo {
    return SchemeInfo.acl = SchemeInfo.acl ?? new AclSchemeInfo();
  }

  private static acl: AclSchemeInfo;

  /**
   * Table identifier for "AclGenerationInfo": {5cca17a2-50e3-4b20-98c2-2f6ed9ce31fa}.
   */
  static get AclGenerationInfo(): AclGenerationInfoSchemeInfo {
    return SchemeInfo.aclGenerationInfo = SchemeInfo.aclGenerationInfo ?? new AclGenerationInfoSchemeInfo();
  }

  private static aclGenerationInfo: AclGenerationInfoSchemeInfo;

  /**
   * Table identifier for "AclGenerationRuleExtensions": {c2c3b955-ca13-4e63-83aa-cb033ebdce57}.
   */
  static get AclGenerationRuleExtensions(): AclGenerationRuleExtensionsSchemeInfo {
    return SchemeInfo.aclGenerationRuleExtensions = SchemeInfo.aclGenerationRuleExtensions ?? new AclGenerationRuleExtensionsSchemeInfo();
  }

  private static aclGenerationRuleExtensions: AclGenerationRuleExtensionsSchemeInfo;

  /**
   * Table identifier for "AclGenerationRules": {5518f35a-ea30-4968-983d-aec524aeb710}.
   */
  static get AclGenerationRules(): AclGenerationRulesSchemeInfo {
    return SchemeInfo.aclGenerationRules = SchemeInfo.aclGenerationRules ?? new AclGenerationRulesSchemeInfo();
  }

  private static aclGenerationRules: AclGenerationRulesSchemeInfo;

  /**
   * Table identifier for "AclGenerationRuleTriggerModes": {b55966a9-f474-47c9-b025-8e3408208646}.
   */
  static get AclGenerationRuleTriggerModes(): AclGenerationRuleTriggerModesSchemeInfo {
    return SchemeInfo.aclGenerationRuleTriggerModes = SchemeInfo.aclGenerationRuleTriggerModes ?? new AclGenerationRuleTriggerModesSchemeInfo();
  }

  private static aclGenerationRuleTriggerModes: AclGenerationRuleTriggerModesSchemeInfo;

  /**
   * Table identifier for "AclGenerationRuleTriggers": {24e6a4b4-7e51-4429-8bb7-648a840e026b}.
   */
  static get AclGenerationRuleTriggers(): AclGenerationRuleTriggersSchemeInfo {
    return SchemeInfo.aclGenerationRuleTriggers = SchemeInfo.aclGenerationRuleTriggers ?? new AclGenerationRuleTriggersSchemeInfo();
  }

  private static aclGenerationRuleTriggers: AclGenerationRuleTriggersSchemeInfo;

  /**
   * Table identifier for "AclGenerationRuleTriggerTypes": {59827979-5949-4bb2-896d-dc8b5a238a32}.
   */
  static get AclGenerationRuleTriggerTypes(): AclGenerationRuleTriggerTypesSchemeInfo {
    return SchemeInfo.aclGenerationRuleTriggerTypes = SchemeInfo.aclGenerationRuleTriggerTypes ?? new AclGenerationRuleTriggerTypesSchemeInfo();
  }

  private static aclGenerationRuleTriggerTypes: AclGenerationRuleTriggerTypesSchemeInfo;

  /**
   * Table identifier for "AclGenerationRuleTypes": {930de8d2-2496-4523-9ea2-800d229fd808}.
   */
  static get AclGenerationRuleTypes(): AclGenerationRuleTypesSchemeInfo {
    return SchemeInfo.aclGenerationRuleTypes = SchemeInfo.aclGenerationRuleTypes ?? new AclGenerationRuleTypesSchemeInfo();
  }

  private static aclGenerationRuleTypes: AclGenerationRuleTypesSchemeInfo;

  /**
   * Table identifier for "AcquaintanceComments": {ae4e68f0-ff8e-4055-9386-f601f1f3c664}.
   */
  static get AcquaintanceComments(): AcquaintanceCommentsSchemeInfo {
    return SchemeInfo.acquaintanceComments = SchemeInfo.acquaintanceComments ?? new AcquaintanceCommentsSchemeInfo();
  }

  private static acquaintanceComments: AcquaintanceCommentsSchemeInfo;

  /**
   * Table identifier for "AcquaintanceRows": {8874a392-0fd9-47dd-a6b5-bc3c02ede681}.
   */
  static get AcquaintanceRows(): AcquaintanceRowsSchemeInfo {
    return SchemeInfo.acquaintanceRows = SchemeInfo.acquaintanceRows ?? new AcquaintanceRowsSchemeInfo();
  }

  private static acquaintanceRows: AcquaintanceRowsSchemeInfo;

  /**
   * Table identifier for "ActionHistory": {5089ca1c-27af-46e4-a2c2-af01bfd42e81}.
   */
  static get ActionHistory(): ActionHistorySchemeInfo {
    return SchemeInfo.actionHistory = SchemeInfo.actionHistory ?? new ActionHistorySchemeInfo();
  }

  private static actionHistory: ActionHistorySchemeInfo;

  /**
   * Table identifier for "ActionHistoryDatabases": {db0969a9-e71d-405d-bf86-15f263cf69c8}.
   */
  static get ActionHistoryDatabases(): ActionHistoryDatabasesSchemeInfo {
    return SchemeInfo.actionHistoryDatabases = SchemeInfo.actionHistoryDatabases ?? new ActionHistoryDatabasesSchemeInfo();
  }

  private static actionHistoryDatabases: ActionHistoryDatabasesSchemeInfo;

  /**
   * Table identifier for "ActionHistoryDatabasesVirtual": {df1d09a4-5ef2-4f2b-885e-c4ad6df06555}.
   */
  static get ActionHistoryDatabasesVirtual(): ActionHistoryDatabasesVirtualSchemeInfo {
    return SchemeInfo.actionHistoryDatabasesVirtual = SchemeInfo.actionHistoryDatabasesVirtual ?? new ActionHistoryDatabasesVirtualSchemeInfo();
  }

  private static actionHistoryDatabasesVirtual: ActionHistoryDatabasesVirtualSchemeInfo;

  /**
   * Table identifier for "ActionHistoryVirtual": {d1ab792c-2758-4778-a3cf-d91191b3ec52}.
   */
  static get ActionHistoryVirtual(): ActionHistoryVirtualSchemeInfo {
    return SchemeInfo.actionHistoryVirtual = SchemeInfo.actionHistoryVirtual ?? new ActionHistoryVirtualSchemeInfo();
  }

  private static actionHistoryVirtual: ActionHistoryVirtualSchemeInfo;

  /**
   * Table identifier for "ActionTypes": {420a67fd-2ea0-4ccd-9c3f-6378c2fda2cc}.
   */
  static get ActionTypes(): ActionTypesSchemeInfo {
    return SchemeInfo.actionTypes = SchemeInfo.actionTypes ?? new ActionTypesSchemeInfo();
  }

  private static actionTypes: ActionTypesSchemeInfo;

  /**
   * Table identifier for "AdSyncRoots": {68543b32-9960-4b90-9c67-72a297f4feff}.
   */
  static get AdSyncRoots(): AdSyncRootsSchemeInfo {
    return SchemeInfo.adSyncRoots = SchemeInfo.adSyncRoots ?? new AdSyncRootsSchemeInfo();
  }

  private static adSyncRoots: AdSyncRootsSchemeInfo;

  /**
   * Table identifier for "AdSyncSettings": {6b7f7b41-7ba8-4549-b965-f3a2aa9a168b}.
   */
  static get AdSyncSettings(): AdSyncSettingsSchemeInfo {
    return SchemeInfo.adSyncSettings = SchemeInfo.adSyncSettings ?? new AdSyncSettingsSchemeInfo();
  }

  private static adSyncSettings: AdSyncSettingsSchemeInfo;

  /**
   * Table identifier for "AdSyncSettingsVirtual": {c993000f-40d8-4639-a25d-e9a25d47e19c}.
   */
  static get AdSyncSettingsVirtual(): AdSyncSettingsVirtualSchemeInfo {
    return SchemeInfo.adSyncSettingsVirtual = SchemeInfo.adSyncSettingsVirtual ?? new AdSyncSettingsVirtualSchemeInfo();
  }

  private static adSyncSettingsVirtual: AdSyncSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "ApplicationArchitectures": {27977834-b755-4a4a-9180-90748e71f361}.
   */
  static get ApplicationArchitectures(): ApplicationArchitecturesSchemeInfo {
    return SchemeInfo.applicationArchitectures = SchemeInfo.applicationArchitectures ?? new ApplicationArchitecturesSchemeInfo();
  }

  private static applicationArchitectures: ApplicationArchitecturesSchemeInfo;

  /**
   * Table identifier for "ApplicationNames": {b939817b-bc1f-4a9d-87ef-694336870eed}.
   */
  static get ApplicationNames(): ApplicationNamesSchemeInfo {
    return SchemeInfo.applicationNames = SchemeInfo.applicationNames ?? new ApplicationNamesSchemeInfo();
  }

  private static applicationNames: ApplicationNamesSchemeInfo;

  /**
   * Table identifier for "ApplicationRoles": {7d23077a-8730-4ad7-9bcd-9a3d52c7e119}.
   */
  static get ApplicationRoles(): ApplicationRolesSchemeInfo {
    return SchemeInfo.applicationRoles = SchemeInfo.applicationRoles ?? new ApplicationRolesSchemeInfo();
  }

  private static applicationRoles: ApplicationRolesSchemeInfo;

  /**
   * Table identifier for "Applications": {6134967a-914b-45eb-99bd-a0ebefdca9f4}.
   */
  static get Applications(): ApplicationsSchemeInfo {
    return SchemeInfo.applications = SchemeInfo.applications ?? new ApplicationsSchemeInfo();
  }

  private static applications: ApplicationsSchemeInfo;

  /**
   * Table identifier for "BackgroundColors": {9f4fc1ce-af03-4009-8106-d9b861469ef1}.
   */
  static get BackgroundColors(): BackgroundColorsSchemeInfo {
    return SchemeInfo.backgroundColors = SchemeInfo.backgroundColors ?? new BackgroundColorsSchemeInfo();
  }

  private static backgroundColors: BackgroundColorsSchemeInfo;

  /**
   * Table identifier for "BarcodeTypes": {60ad88cc-f913-48ce-96e1-0abf417da790}.
   */
  static get BarcodeTypes(): BarcodeTypesSchemeInfo {
    return SchemeInfo.barcodeTypes = SchemeInfo.barcodeTypes ?? new BarcodeTypesSchemeInfo();
  }

  private static barcodeTypes: BarcodeTypesSchemeInfo;

  /**
   * Table identifier for "BlockColors": {c1b59501-4d7f-4884-ac20-715d5d26078b}.
   */
  static get BlockColors(): BlockColorsSchemeInfo {
    return SchemeInfo.blockColors = SchemeInfo.blockColors ?? new BlockColorsSchemeInfo();
  }

  private static blockColors: BlockColorsSchemeInfo;

  /**
   * Table identifier for "BusinessProcessButtonExtension": {b4d0da55-0e6e-4835-bb71-1df3c5b5e695}.
   */
  static get BusinessProcessButtonExtension(): BusinessProcessButtonExtensionSchemeInfo {
    return SchemeInfo.businessProcessButtonExtension = SchemeInfo.businessProcessButtonExtension ?? new BusinessProcessButtonExtensionSchemeInfo();
  }

  private static businessProcessButtonExtension: BusinessProcessButtonExtensionSchemeInfo;

  /**
   * Table identifier for "BusinessProcessButtonRoles": {89599d8b-fa2f-44de-94d5-9687d4a16854}.
   */
  static get BusinessProcessButtonRoles(): BusinessProcessButtonRolesSchemeInfo {
    return SchemeInfo.businessProcessButtonRoles = SchemeInfo.businessProcessButtonRoles ?? new BusinessProcessButtonRolesSchemeInfo();
  }

  private static businessProcessButtonRoles: BusinessProcessButtonRolesSchemeInfo;

  /**
   * Table identifier for "BusinessProcessButtonRolesVirtual": {803fee29-9750-46f5-950d-77a44ff8b2af}.
   */
  static get BusinessProcessButtonRolesVirtual(): BusinessProcessButtonRolesVirtualSchemeInfo {
    return SchemeInfo.businessProcessButtonRolesVirtual = SchemeInfo.businessProcessButtonRolesVirtual ?? new BusinessProcessButtonRolesVirtualSchemeInfo();
  }

  private static businessProcessButtonRolesVirtual: BusinessProcessButtonRolesVirtualSchemeInfo;

  /**
   * Table identifier for "BusinessProcessButtons": {59bf0d0b-f7fc-41d3-92da-56c673f1e0b3}.
   */
  static get BusinessProcessButtons(): BusinessProcessButtonsSchemeInfo {
    return SchemeInfo.businessProcessButtons = SchemeInfo.businessProcessButtons ?? new BusinessProcessButtonsSchemeInfo();
  }

  private static businessProcessButtons: BusinessProcessButtonsSchemeInfo;

  /**
   * Table identifier for "BusinessProcessButtonsVirtual": {033a363a-e183-4084-83cb-4672841a2a90}.
   */
  static get BusinessProcessButtonsVirtual(): BusinessProcessButtonsVirtualSchemeInfo {
    return SchemeInfo.businessProcessButtonsVirtual = SchemeInfo.businessProcessButtonsVirtual ?? new BusinessProcessButtonsVirtualSchemeInfo();
  }

  private static businessProcessButtonsVirtual: BusinessProcessButtonsVirtualSchemeInfo;

  /**
   * Table identifier for "BusinessProcessCardTypes": {2317e111-2d0e-42d9-94dd-973411ecadca}.
   */
  static get BusinessProcessCardTypes(): BusinessProcessCardTypesSchemeInfo {
    return SchemeInfo.businessProcessCardTypes = SchemeInfo.businessProcessCardTypes ?? new BusinessProcessCardTypesSchemeInfo();
  }

  private static businessProcessCardTypes: BusinessProcessCardTypesSchemeInfo;

  /**
   * Table identifier for "BusinessProcessEditRoles": {669078fd-2901-4084-ac61-13a063581197}.
   */
  static get BusinessProcessEditRoles(): BusinessProcessEditRolesSchemeInfo {
    return SchemeInfo.businessProcessEditRoles = SchemeInfo.businessProcessEditRoles ?? new BusinessProcessEditRolesSchemeInfo();
  }

  private static businessProcessEditRoles: BusinessProcessEditRolesSchemeInfo;

  /**
   * Table identifier for "BusinessProcessExtensions": {07e8720b-4500-4a7f-b988-7eda3bb8dc38}.
   */
  static get BusinessProcessExtensions(): BusinessProcessExtensionsSchemeInfo {
    return SchemeInfo.businessProcessExtensions = SchemeInfo.businessProcessExtensions ?? new BusinessProcessExtensionsSchemeInfo();
  }

  private static businessProcessExtensions: BusinessProcessExtensionsSchemeInfo;

  /**
   * Table identifier for "BusinessProcessInfo": {5640ffb9-ef7c-4584-8793-57da90e82fa0}.
   */
  static get BusinessProcessInfo(): BusinessProcessInfoSchemeInfo {
    return SchemeInfo.businessProcessInfo = SchemeInfo.businessProcessInfo ?? new BusinessProcessInfoSchemeInfo();
  }

  private static businessProcessInfo: BusinessProcessInfoSchemeInfo;

  /**
   * Table identifier for "BusinessProcessReadRoles": {8a55a034-893d-4412-9458-189ef63d7008}.
   */
  static get BusinessProcessReadRoles(): BusinessProcessReadRolesSchemeInfo {
    return SchemeInfo.businessProcessReadRoles = SchemeInfo.businessProcessReadRoles ?? new BusinessProcessReadRolesSchemeInfo();
  }

  private static businessProcessReadRoles: BusinessProcessReadRolesSchemeInfo;

  /**
   * Table identifier for "BusinessProcessVersions": {dcd38c54-ed18-4503-b435-3dee1c6c2c62}.
   */
  static get BusinessProcessVersions(): BusinessProcessVersionsSchemeInfo {
    return SchemeInfo.businessProcessVersions = SchemeInfo.businessProcessVersions ?? new BusinessProcessVersionsSchemeInfo();
  }

  private static businessProcessVersions: BusinessProcessVersionsSchemeInfo;

  /**
   * Table identifier for "BusinessProcessVersionsVirtual": {6999d0d3-a44f-43b5-8e79-a551697340e6}.
   */
  static get BusinessProcessVersionsVirtual(): BusinessProcessVersionsVirtualSchemeInfo {
    return SchemeInfo.businessProcessVersionsVirtual = SchemeInfo.businessProcessVersionsVirtual ?? new BusinessProcessVersionsVirtualSchemeInfo();
  }

  private static businessProcessVersionsVirtual: BusinessProcessVersionsVirtualSchemeInfo;

  /**
   * Table identifier for "CalendarCalcMethods": {011f3246-c0f2-4d91-aaee-5129c6b83e15}.
   */
  static get CalendarCalcMethods(): CalendarCalcMethodsSchemeInfo {
    return SchemeInfo.calendarCalcMethods = SchemeInfo.calendarCalcMethods ?? new CalendarCalcMethodsSchemeInfo();
  }

  private static calendarCalcMethods: CalendarCalcMethodsSchemeInfo;

  /**
   * Table identifier for "CalendarExclusions": {aec4456f-c927-4a49-89f5-582ab17dc997}.
   */
  static get CalendarExclusions(): CalendarExclusionsSchemeInfo {
    return SchemeInfo.calendarExclusions = SchemeInfo.calendarExclusions ?? new CalendarExclusionsSchemeInfo();
  }

  private static calendarExclusions: CalendarExclusionsSchemeInfo;

  /**
   * Table identifier for "CalendarNamedRanges": {dc5ec614-d5df-40d1-ba43-4e5b97211711}.
   */
  static get CalendarNamedRanges(): CalendarNamedRangesSchemeInfo {
    return SchemeInfo.calendarNamedRanges = SchemeInfo.calendarNamedRanges ?? new CalendarNamedRangesSchemeInfo();
  }

  private static calendarNamedRanges: CalendarNamedRangesSchemeInfo;

  /**
   * Table identifier for "CalendarQuants": {094fac6d-4fe8-4d3e-89c2-22a0f74fd705}.
   */
  static get CalendarQuants(): CalendarQuantsSchemeInfo {
    return SchemeInfo.calendarQuants = SchemeInfo.calendarQuants ?? new CalendarQuantsSchemeInfo();
  }

  private static calendarQuants: CalendarQuantsSchemeInfo;

  /**
   * Table identifier for "CalendarSettings": {67b1fd42-0106-4b31-a368-ea3e4d38ac5c}.
   */
  static get CalendarSettings(): CalendarSettingsSchemeInfo {
    return SchemeInfo.calendarSettings = SchemeInfo.calendarSettings ?? new CalendarSettingsSchemeInfo();
  }

  private static calendarSettings: CalendarSettingsSchemeInfo;

  /**
   * Table identifier for "CalendarTypeExclusions": {3a11e188-f82f-495b-a78f-778f2988db52}.
   */
  static get CalendarTypeExclusions(): CalendarTypeExclusionsSchemeInfo {
    return SchemeInfo.calendarTypeExclusions = SchemeInfo.calendarTypeExclusions ?? new CalendarTypeExclusionsSchemeInfo();
  }

  private static calendarTypeExclusions: CalendarTypeExclusionsSchemeInfo;

  /**
   * Table identifier for "CalendarTypes": {c411ab46-1df7-4a76-97b5-d0d39fff656b}.
   */
  static get CalendarTypes(): CalendarTypesSchemeInfo {
    return SchemeInfo.calendarTypes = SchemeInfo.calendarTypes ?? new CalendarTypesSchemeInfo();
  }

  private static calendarTypes: CalendarTypesSchemeInfo;

  /**
   * Table identifier for "CalendarTypeWeekDays": {67d63f49-ec4f-4e3b-9364-0b6e38d138ec}.
   */
  static get CalendarTypeWeekDays(): CalendarTypeWeekDaysSchemeInfo {
    return SchemeInfo.calendarTypeWeekDays = SchemeInfo.calendarTypeWeekDays ?? new CalendarTypeWeekDaysSchemeInfo();
  }

  private static calendarTypeWeekDays: CalendarTypeWeekDaysSchemeInfo;

  /**
   * Table identifier for "CompilationCache": {3f86165e-8a0d-41d9-a7a2-b6a511bf551b}.
   */
  static get CompilationCache(): CompilationCacheSchemeInfo {
    return SchemeInfo.compilationCache = SchemeInfo.compilationCache ?? new CompilationCacheSchemeInfo();
  }

  private static compilationCache: CompilationCacheSchemeInfo;

  /**
   * Table identifier for "CompiledViews": {0ebd80aa-360b-473b-8327-90e10035c000}.
   */
  static get CompiledViews(): CompiledViewsSchemeInfo {
    return SchemeInfo.compiledViews = SchemeInfo.compiledViews ?? new CompiledViewsSchemeInfo();
  }

  private static compiledViews: CompiledViewsSchemeInfo;

  /**
   * Table identifier for "CompletionOptions": {08cf782d-4130-4377-8a49-3e201a05d496}.
   */
  static get CompletionOptions(): CompletionOptionsSchemeInfo {
    return SchemeInfo.completionOptions = SchemeInfo.completionOptions ?? new CompletionOptionsSchemeInfo();
  }

  private static completionOptions: CompletionOptionsSchemeInfo;

  /**
   * Table identifier for "CompletionOptionsVirtual": {cfff92c8-26e6-42e5-b45d-837bc374022d}.
   */
  static get CompletionOptionsVirtual(): CompletionOptionsVirtualSchemeInfo {
    return SchemeInfo.completionOptionsVirtual = SchemeInfo.completionOptionsVirtual ?? new CompletionOptionsVirtualSchemeInfo();
  }

  private static completionOptionsVirtual: CompletionOptionsVirtualSchemeInfo;

  /**
   * Table identifier for "ConditionsVirtual": {6d2ec0d3-4980-45f3-aa64-ab79eb9f4da1}.
   */
  static get ConditionsVirtual(): ConditionsVirtualSchemeInfo {
    return SchemeInfo.conditionsVirtual = SchemeInfo.conditionsVirtual ?? new ConditionsVirtualSchemeInfo();
  }

  private static conditionsVirtual: ConditionsVirtualSchemeInfo;

  /**
   * Table identifier for "ConditionTypes": {7e0c2c3b-e8f3-4f96-9aa6-eb1c2100d74f}.
   */
  static get ConditionTypes(): ConditionTypesSchemeInfo {
    return SchemeInfo.conditionTypes = SchemeInfo.conditionTypes ?? new ConditionTypesSchemeInfo();
  }

  private static conditionTypes: ConditionTypesSchemeInfo;

  /**
   * Table identifier for "ConditionTypeUsePlaces": {b764f842-5b97-4de7-854a-f61b6b7a71dc}.
   */
  static get ConditionTypeUsePlaces(): ConditionTypeUsePlacesSchemeInfo {
    return SchemeInfo.conditionTypeUsePlaces = SchemeInfo.conditionTypeUsePlaces ?? new ConditionTypeUsePlacesSchemeInfo();
  }

  private static conditionTypeUsePlaces: ConditionTypeUsePlacesSchemeInfo;

  /**
   * Table identifier for "ConditionUsePlaces": {6963c76f-5e8d-49b5-80a3-f2ec342de0bf}.
   */
  static get ConditionUsePlaces(): ConditionUsePlacesSchemeInfo {
    return SchemeInfo.conditionUsePlaces = SchemeInfo.conditionUsePlaces ?? new ConditionUsePlacesSchemeInfo();
  }

  private static conditionUsePlaces: ConditionUsePlacesSchemeInfo;

  /**
   * Table identifier for "Configuration": {57b9e507-d135-4c69-9a94-bf507d499484}.
   */
  static get Configuration(): ConfigurationSchemeInfo {
    return SchemeInfo.configuration = SchemeInfo.configuration ?? new ConfigurationSchemeInfo();
  }

  private static configuration: ConfigurationSchemeInfo;

  /**
   * Table identifier for "ContextRoles": {be5a85fd-b2fb-4f60-a3b7-48e79e45249f}.
   */
  static get ContextRoles(): ContextRolesSchemeInfo {
    return SchemeInfo.contextRoles = SchemeInfo.contextRoles ?? new ContextRolesSchemeInfo();
  }

  private static contextRoles: ContextRolesSchemeInfo;

  /**
   * Table identifier for "Currencies": {3612e150-032f-4a68-bf8e-8e094e5a3a73}.
   */
  static get Currencies(): CurrenciesSchemeInfo {
    return SchemeInfo.currencies = SchemeInfo.currencies ?? new CurrenciesSchemeInfo();
  }

  private static currencies: CurrenciesSchemeInfo;

  /**
   * Table identifier for "CustomBackgroundColorsVirtual": {5d65177e-590c-4422-9120-1a202a534640}.
   */
  static get CustomBackgroundColorsVirtual(): CustomBackgroundColorsVirtualSchemeInfo {
    return SchemeInfo.customBackgroundColorsVirtual = SchemeInfo.customBackgroundColorsVirtual ?? new CustomBackgroundColorsVirtualSchemeInfo();
  }

  private static customBackgroundColorsVirtual: CustomBackgroundColorsVirtualSchemeInfo;

  /**
   * Table identifier for "CustomBlockColorsVirtual": {cafa4371-0483-4d71-80cd-75d68cd6086f}.
   */
  static get CustomBlockColorsVirtual(): CustomBlockColorsVirtualSchemeInfo {
    return SchemeInfo.customBlockColorsVirtual = SchemeInfo.customBlockColorsVirtual ?? new CustomBlockColorsVirtualSchemeInfo();
  }

  private static customBlockColorsVirtual: CustomBlockColorsVirtualSchemeInfo;

  /**
   * Table identifier for "CustomForegroundColorsVirtual": {8f0adc86-8166-4579-9a25-7c3f2921d32d}.
   */
  static get CustomForegroundColorsVirtual(): CustomForegroundColorsVirtualSchemeInfo {
    return SchemeInfo.customForegroundColorsVirtual = SchemeInfo.customForegroundColorsVirtual ?? new CustomForegroundColorsVirtualSchemeInfo();
  }

  private static customForegroundColorsVirtual: CustomForegroundColorsVirtualSchemeInfo;

  /**
   * Table identifier for "DateFormats": {585825ed-e297-4eb3-bea2-a732ad75c6b6}.
   */
  static get DateFormats(): DateFormatsSchemeInfo {
    return SchemeInfo.dateFormats = SchemeInfo.dateFormats ?? new DateFormatsSchemeInfo();
  }

  private static dateFormats: DateFormatsSchemeInfo;

  /**
   * Table identifier for "DefaultTimeZone": {d894a451-c0ff-4a75-b808-05d24cf077bf}.
   */
  static get DefaultTimeZone(): DefaultTimeZoneSchemeInfo {
    return SchemeInfo.defaultTimeZone = SchemeInfo.defaultTimeZone ?? new DefaultTimeZoneSchemeInfo();
  }

  private static defaultTimeZone: DefaultTimeZoneSchemeInfo;

  /**
   * Table identifier for "DefaultWorkplacesVirtual": {dd42ee04-02c5-407b-b596-07aa830a9b80}.
   */
  static get DefaultWorkplacesVirtual(): DefaultWorkplacesVirtualSchemeInfo {
    return SchemeInfo.defaultWorkplacesVirtual = SchemeInfo.defaultWorkplacesVirtual ?? new DefaultWorkplacesVirtualSchemeInfo();
  }

  private static defaultWorkplacesVirtual: DefaultWorkplacesVirtualSchemeInfo;

  /**
   * Table identifier for "Deleted": {a49102cc-6bb4-425b-95ad-75ff0b3edf0d}.
   */
  static get Deleted(): DeletedSchemeInfo {
    return SchemeInfo.deleted = SchemeInfo.deleted ?? new DeletedSchemeInfo();
  }

  private static deleted: DeletedSchemeInfo;

  /**
   * Table identifier for "DeletedTaskRoles": {8340a9b3-74ba-4771-af73-35bed38db55e}.
   */
  static get DeletedTaskRoles(): DeletedTaskRolesSchemeInfo {
    return SchemeInfo.deletedTaskRoles = SchemeInfo.deletedTaskRoles ?? new DeletedTaskRolesSchemeInfo();
  }

  private static deletedTaskRoles: DeletedTaskRolesSchemeInfo;

  /**
   * Table identifier for "DeletedVirtual": {300db9a6-f6a0-48a8-b6c3-5f8891817cdd}.
   */
  static get DeletedVirtual(): DeletedVirtualSchemeInfo {
    return SchemeInfo.deletedVirtual = SchemeInfo.deletedVirtual ?? new DeletedVirtualSchemeInfo();
  }

  private static deletedVirtual: DeletedVirtualSchemeInfo;

  /**
   * Table identifier for "DepartmentRoles": {d43dace1-536f-4c9f-af15-49a8892a7427}.
   */
  static get DepartmentRoles(): DepartmentRolesSchemeInfo {
    return SchemeInfo.departmentRoles = SchemeInfo.departmentRoles ?? new DepartmentRolesSchemeInfo();
  }

  private static departmentRoles: DepartmentRolesSchemeInfo;

  /**
   * Table identifier for "DeviceTypes": {8b4cd042-334b-4aee-a623-7d8942aa6897}.
   */
  static get DeviceTypes(): DeviceTypesSchemeInfo {
    return SchemeInfo.deviceTypes = SchemeInfo.deviceTypes ?? new DeviceTypesSchemeInfo();
  }

  private static deviceTypes: DeviceTypesSchemeInfo;

  /**
   * Table identifier for "DialogButtonTypes": {e07bb4d3-1312-4638-9751-ddd8e3a127fc}.
   */
  static get DialogButtonTypes(): DialogButtonTypesSchemeInfo {
    return SchemeInfo.dialogButtonTypes = SchemeInfo.dialogButtonTypes ?? new DialogButtonTypesSchemeInfo();
  }

  private static dialogButtonTypes: DialogButtonTypesSchemeInfo;

  /**
   * Table identifier for "DialogCardAutoOpenModes": {b1827f66-89bd-4269-b2ce-ea27337616fd}.
   */
  static get DialogCardAutoOpenModes(): DialogCardAutoOpenModesSchemeInfo {
    return SchemeInfo.dialogCardAutoOpenModes = SchemeInfo.dialogCardAutoOpenModes ?? new DialogCardAutoOpenModesSchemeInfo();
  }

  private static dialogCardAutoOpenModes: DialogCardAutoOpenModesSchemeInfo;

  /**
   * Table identifier for "DialogCardStoreModes": {f383bf09-2ec9-4fe5-aa50-f3b14898c976}.
   */
  static get DialogCardStoreModes(): DialogCardStoreModesSchemeInfo {
    return SchemeInfo.dialogCardStoreModes = SchemeInfo.dialogCardStoreModes ?? new DialogCardStoreModesSchemeInfo();
  }

  private static dialogCardStoreModes: DialogCardStoreModesSchemeInfo;

  /**
   * Table identifier for "DialogRoles": {125ad61a-3698-4d07-9fa0-139c9cc25074}.
   */
  static get DialogRoles(): DialogRolesSchemeInfo {
    return SchemeInfo.dialogRoles = SchemeInfo.dialogRoles ?? new DialogRolesSchemeInfo();
  }

  private static dialogRoles: DialogRolesSchemeInfo;

  /**
   * Table identifier for "Dialogs": {53a54dce-29d9-4f2c-8522-73ca60a4dbb5}.
   */
  static get Dialogs(): DialogsSchemeInfo {
    return SchemeInfo.dialogs = SchemeInfo.dialogs ?? new DialogsSchemeInfo();
  }

  private static dialogs: DialogsSchemeInfo;

  /**
   * Table identifier for "DocLoadBarcodeRead": {3d7fe6dc-f80f-4399-83aa-261e4624aaf1}.
   */
  static get DocLoadBarcodeRead(): DocLoadBarcodeReadSchemeInfo {
    return SchemeInfo.docLoadBarcodeRead = SchemeInfo.docLoadBarcodeRead ?? new DocLoadBarcodeReadSchemeInfo();
  }

  private static docLoadBarcodeRead: DocLoadBarcodeReadSchemeInfo;

  /**
   * Table identifier for "DocLoadSettings": {0dbef4e9-7bf7-4b8f-aab0-fa908bc30e6f}.
   */
  static get DocLoadSettings(): DocLoadSettingsSchemeInfo {
    return SchemeInfo.docLoadSettings = SchemeInfo.docLoadSettings ?? new DocLoadSettingsSchemeInfo();
  }

  private static docLoadSettings: DocLoadSettingsSchemeInfo;

  /**
   * Table identifier for "DocumentCategories": {f939aa52-dc1a-40b2-af4a-cb2757e8390a}.
   */
  static get DocumentCategories(): DocumentCategoriesSchemeInfo {
    return SchemeInfo.documentCategories = SchemeInfo.documentCategories ?? new DocumentCategoriesSchemeInfo();
  }

  private static documentCategories: DocumentCategoriesSchemeInfo;

  /**
   * Table identifier for "DocumentCommonInfo": {a161e289-2f99-4699-9e95-6e3336be8527}.
   */
  static get DocumentCommonInfo(): DocumentCommonInfoSchemeInfo {
    return SchemeInfo.documentCommonInfo = SchemeInfo.documentCommonInfo ?? new DocumentCommonInfoSchemeInfo();
  }

  private static documentCommonInfo: DocumentCommonInfoSchemeInfo;

  /**
   * Table identifier for "DynamicRoles": {4a282d48-6d78-4923-85e4-8d3f9be213fa}.
   */
  static get DynamicRoles(): DynamicRolesSchemeInfo {
    return SchemeInfo.dynamicRoles = SchemeInfo.dynamicRoles ?? new DynamicRolesSchemeInfo();
  }

  private static dynamicRoles: DynamicRolesSchemeInfo;

  /**
   * Table identifier for "Errors": {754008b7-831b-44f9-9c58-99fa0334e62f}.
   */
  static get Errors(): ErrorsSchemeInfo {
    return SchemeInfo.errors = SchemeInfo.errors ?? new ErrorsSchemeInfo();
  }

  private static errors: ErrorsSchemeInfo;

  /**
   * Table identifier for "FieldChangedCondition": {06245b07-be2a-40de-aec8-bfd367860930}.
   */
  static get FieldChangedCondition(): FieldChangedConditionSchemeInfo {
    return SchemeInfo.fieldChangedCondition = SchemeInfo.fieldChangedCondition ?? new FieldChangedConditionSchemeInfo();
  }

  private static fieldChangedCondition: FieldChangedConditionSchemeInfo;

  /**
   * Table identifier for "FileCategories": {e1599715-02d4-4ca9-b63e-b4b1ce642c7a}.
   */
  static get FileCategories(): FileCategoriesSchemeInfo {
    return SchemeInfo.fileCategories = SchemeInfo.fileCategories ?? new FileCategoriesSchemeInfo();
  }

  private static fileCategories: FileCategoriesSchemeInfo;

  /**
   * Table identifier for "FileContent": {328af88c-b21a-4c2a-b825-45a086d0b24b}.
   */
  static get FileContent(): FileContentSchemeInfo {
    return SchemeInfo.fileContent = SchemeInfo.fileContent ?? new FileContentSchemeInfo();
  }

  private static fileContent: FileContentSchemeInfo;

  /**
   * Table identifier for "FileConverterCache": {b376adb4-3134-4ec5-9597-a83e8c9db0f1}.
   */
  static get FileConverterCache(): FileConverterCacheSchemeInfo {
    return SchemeInfo.fileConverterCache = SchemeInfo.fileConverterCache ?? new FileConverterCacheSchemeInfo();
  }

  private static fileConverterCache: FileConverterCacheSchemeInfo;

  /**
   * Table identifier for "FileConverterCacheVirtual": {961887ca-e67c-4283-89fc-265dbf17e4c1}.
   */
  static get FileConverterCacheVirtual(): FileConverterCacheVirtualSchemeInfo {
    return SchemeInfo.fileConverterCacheVirtual = SchemeInfo.fileConverterCacheVirtual ?? new FileConverterCacheVirtualSchemeInfo();
  }

  private static fileConverterCacheVirtual: FileConverterCacheVirtualSchemeInfo;

  /**
   * Table identifier for "FileConverterTypes": {a1dd7426-13e0-42fb-a45a-0a714108e274}.
   */
  static get FileConverterTypes(): FileConverterTypesSchemeInfo {
    return SchemeInfo.fileConverterTypes = SchemeInfo.fileConverterTypes ?? new FileConverterTypesSchemeInfo();
  }

  private static fileConverterTypes: FileConverterTypesSchemeInfo;

  /**
   * Table identifier for "Files": {dd716146-b177-4920-bc90-b1196b16347c}.
   */
  static get Files(): FilesSchemeInfo {
    return SchemeInfo.files = SchemeInfo.files ?? new FilesSchemeInfo();
  }

  private static files: FilesSchemeInfo;

  /**
   * Table identifier for "FileSignatureEventTypes": {5a8e7767-cd46-4ace-9da3-e3ea6f38cff2}.
   */
  static get FileSignatureEventTypes(): FileSignatureEventTypesSchemeInfo {
    return SchemeInfo.fileSignatureEventTypes = SchemeInfo.fileSignatureEventTypes ?? new FileSignatureEventTypesSchemeInfo();
  }

  private static fileSignatureEventTypes: FileSignatureEventTypesSchemeInfo;

  /**
   * Table identifier for "FileSignatures": {5f428478-eaf5-4180-bde9-499483c3f80c}.
   */
  static get FileSignatures(): FileSignaturesSchemeInfo {
    return SchemeInfo.fileSignatures = SchemeInfo.fileSignatures ?? new FileSignaturesSchemeInfo();
  }

  private static fileSignatures: FileSignaturesSchemeInfo;

  /**
   * Table identifier for "FileSources": {e8300fe5-3b24-4c27-a45a-6cd8575bfcd5}.
   */
  static get FileSources(): FileSourcesSchemeInfo {
    return SchemeInfo.fileSources = SchemeInfo.fileSources ?? new FileSourcesSchemeInfo();
  }

  private static fileSources: FileSourcesSchemeInfo;

  /**
   * Table identifier for "FileSourcesVirtual": {64bbc32d-95d6-434b-9c08-0288344d53bb}.
   */
  static get FileSourcesVirtual(): FileSourcesVirtualSchemeInfo {
    return SchemeInfo.fileSourcesVirtual = SchemeInfo.fileSourcesVirtual ?? new FileSourcesVirtualSchemeInfo();
  }

  private static fileSourcesVirtual: FileSourcesVirtualSchemeInfo;

  /**
   * Table identifier for "FileStates": {de9ba182-3fc4-4f20-9060-fa83b74fd46c}.
   */
  static get FileStates(): FileStatesSchemeInfo {
    return SchemeInfo.fileStates = SchemeInfo.fileStates ?? new FileStatesSchemeInfo();
  }

  private static fileStates: FileStatesSchemeInfo;

  /**
   * Table identifier for "FileTemplateRoles": {0eabfebc-fa8b-41b9-9aa3-6db9626a8ac6}.
   */
  static get FileTemplateRoles(): FileTemplateRolesSchemeInfo {
    return SchemeInfo.fileTemplateRoles = SchemeInfo.fileTemplateRoles ?? new FileTemplateRolesSchemeInfo();
  }

  private static fileTemplateRoles: FileTemplateRolesSchemeInfo;

  /**
   * Table identifier for "FileTemplates": {98e0c3a9-0b9a-4fec-9843-4a077f6ff5f0}.
   */
  static get FileTemplates(): FileTemplatesSchemeInfo {
    return SchemeInfo.fileTemplates = SchemeInfo.fileTemplates ?? new FileTemplatesSchemeInfo();
  }

  private static fileTemplates: FileTemplatesSchemeInfo;

  /**
   * Table identifier for "FileTemplateTemplateTypes": {54994b70-b619-4280-b9ff-31c20453a462}.
   */
  static get FileTemplateTemplateTypes(): FileTemplateTemplateTypesSchemeInfo {
    return SchemeInfo.fileTemplateTemplateTypes = SchemeInfo.fileTemplateTemplateTypes ?? new FileTemplateTemplateTypesSchemeInfo();
  }

  private static fileTemplateTemplateTypes: FileTemplateTemplateTypesSchemeInfo;

  /**
   * Table identifier for "FileTemplateTypes": {628e0e44-564c-4107-b943-0ec1e378bae7}.
   */
  static get FileTemplateTypes(): FileTemplateTypesSchemeInfo {
    return SchemeInfo.fileTemplateTypes = SchemeInfo.fileTemplateTypes ?? new FileTemplateTypesSchemeInfo();
  }

  private static fileTemplateTypes: FileTemplateTypesSchemeInfo;

  /**
   * Table identifier for "FileTemplateViews": {ebd081b9-aaf9-4bab-be51-602803756e8d}.
   */
  static get FileTemplateViews(): FileTemplateViewsSchemeInfo {
    return SchemeInfo.fileTemplateViews = SchemeInfo.fileTemplateViews ?? new FileTemplateViewsSchemeInfo();
  }

  private static fileTemplateViews: FileTemplateViewsSchemeInfo;

  /**
   * Table identifier for "FileVersions": {e17fd270-5c61-49af-955d-ed6bb983f0d8}.
   */
  static get FileVersions(): FileVersionsSchemeInfo {
    return SchemeInfo.fileVersions = SchemeInfo.fileVersions ?? new FileVersionsSchemeInfo();
  }

  private static fileVersions: FileVersionsSchemeInfo;

  /**
   * Table identifier for "FmAttachments": {3f903804-3c70-4828-9887-5c9268d20b7d}.
   */
  static get FmAttachments(): FmAttachmentsSchemeInfo {
    return SchemeInfo.fmAttachments = SchemeInfo.fmAttachments ?? new FmAttachmentsSchemeInfo();
  }

  private static fmAttachments: FmAttachmentsSchemeInfo;

  /**
   * Table identifier for "FmAttachmentTypes": {74caae68-ee60-4d36-b6af-b81bdd06d4a3}.
   */
  static get FmAttachmentTypes(): FmAttachmentTypesSchemeInfo {
    return SchemeInfo.fmAttachmentTypes = SchemeInfo.fmAttachmentTypes ?? new FmAttachmentTypesSchemeInfo();
  }

  private static fmAttachmentTypes: FmAttachmentTypesSchemeInfo;

  /**
   * Table identifier for "FmMessages": {a03f6c5d-e719-43d6-bcc5-d2ea321765ab}.
   */
  static get FmMessages(): FmMessagesSchemeInfo {
    return SchemeInfo.fmMessages = SchemeInfo.fmMessages ?? new FmMessagesSchemeInfo();
  }

  private static fmMessages: FmMessagesSchemeInfo;

  /**
   * Table identifier for "FmMessagesPluginTable": {18b094ec-a87f-4ccb-bfe8-a5936cc38992}.
   */
  static get FmMessagesPluginTable(): FmMessagesPluginTableSchemeInfo {
    return SchemeInfo.fmMessagesPluginTable = SchemeInfo.fmMessagesPluginTable ?? new FmMessagesPluginTableSchemeInfo();
  }

  private static fmMessagesPluginTable: FmMessagesPluginTableSchemeInfo;

  /**
   * Table identifier for "FmMessageTypes": {43f92881-c875-437a-bf1c-b7793c099d00}.
   */
  static get FmMessageTypes(): FmMessageTypesSchemeInfo {
    return SchemeInfo.fmMessageTypes = SchemeInfo.fmMessageTypes ?? new FmMessageTypesSchemeInfo();
  }

  private static fmMessageTypes: FmMessageTypesSchemeInfo;

  /**
   * Table identifier for "FmNotifications": {fe822963-6091-4f70-9fbe-167aba72b4a2}.
   */
  static get FmNotifications(): FmNotificationsSchemeInfo {
    return SchemeInfo.fmNotifications = SchemeInfo.fmNotifications ?? new FmNotificationsSchemeInfo();
  }

  private static fmNotifications: FmNotificationsSchemeInfo;

  /**
   * Table identifier for "FmParticipantTypes": {2b2a8e44-eecd-4afe-b017-20f8a00846ff}.
   */
  static get FmParticipantTypes(): FmParticipantTypesSchemeInfo {
    return SchemeInfo.fmParticipantTypes = SchemeInfo.fmParticipantTypes ?? new FmParticipantTypesSchemeInfo();
  }

  private static fmParticipantTypes: FmParticipantTypesSchemeInfo;

  /**
   * Table identifier for "FmTopicParticipantRoles": {ecd6e90e-3bbe-4c24-975b-6644b20efe7f}.
   */
  static get FmTopicParticipantRoles(): FmTopicParticipantRolesSchemeInfo {
    return SchemeInfo.fmTopicParticipantRoles = SchemeInfo.fmTopicParticipantRoles ?? new FmTopicParticipantRolesSchemeInfo();
  }

  private static fmTopicParticipantRoles: FmTopicParticipantRolesSchemeInfo;

  /**
   * Table identifier for "FmTopicParticipantRolesUnsubscribed": {e9fd155c-b189-4a5d-b0b4-970c94a2fa0a}.
   */
  static get FmTopicParticipantRolesUnsubscribed(): FmTopicParticipantRolesUnsubscribedSchemeInfo {
    return SchemeInfo.fmTopicParticipantRolesUnsubscribed = SchemeInfo.fmTopicParticipantRolesUnsubscribed ?? new FmTopicParticipantRolesUnsubscribedSchemeInfo();
  }

  private static fmTopicParticipantRolesUnsubscribed: FmTopicParticipantRolesUnsubscribedSchemeInfo;

  /**
   * Table identifier for "FmTopicParticipants": {b8150fdd-b439-4eaa-9665-9a8b9ee774f0}.
   */
  static get FmTopicParticipants(): FmTopicParticipantsSchemeInfo {
    return SchemeInfo.fmTopicParticipants = SchemeInfo.fmTopicParticipants ?? new FmTopicParticipantsSchemeInfo();
  }

  private static fmTopicParticipants: FmTopicParticipantsSchemeInfo;

  /**
   * Table identifier for "FmTopics": {35b11a3c-f9ec-4fac-a3f1-def11bba44ae}.
   */
  static get FmTopics(): FmTopicsSchemeInfo {
    return SchemeInfo.fmTopics = SchemeInfo.fmTopics ?? new FmTopicsSchemeInfo();
  }

  private static fmTopics: FmTopicsSchemeInfo;

  /**
   * Table identifier for "FmTopicTypes": {c0645587-3584-4b23-867f-54071abfa5a1}.
   */
  static get FmTopicTypes(): FmTopicTypesSchemeInfo {
    return SchemeInfo.fmTopicTypes = SchemeInfo.fmTopicTypes ?? new FmTopicTypesSchemeInfo();
  }

  private static fmTopicTypes: FmTopicTypesSchemeInfo;

  /**
   * Table identifier for "FmUserSettingsVirtual": {e8fe8b2a-428d-44b6-8328-ee2a7bb4d323}.
   */
  static get FmUserSettingsVirtual(): FmUserSettingsVirtualSchemeInfo {
    return SchemeInfo.fmUserSettingsVirtual = SchemeInfo.fmUserSettingsVirtual ?? new FmUserSettingsVirtualSchemeInfo();
  }

  private static fmUserSettingsVirtual: FmUserSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "FmUserStat": {d10d18eb-d803-4151-8a60-8bfd262d2800}.
   */
  static get FmUserStat(): FmUserStatSchemeInfo {
    return SchemeInfo.fmUserStat = SchemeInfo.fmUserStat ?? new FmUserStatSchemeInfo();
  }

  private static fmUserStat: FmUserStatSchemeInfo;

  /**
   * Table identifier for "ForegroundColors": {f22e70d5-17da-4e6a-8d41-17796e5f75d0}.
   */
  static get ForegroundColors(): ForegroundColorsSchemeInfo {
    return SchemeInfo.foregroundColors = SchemeInfo.foregroundColors ?? new ForegroundColorsSchemeInfo();
  }

  private static foregroundColors: ForegroundColorsSchemeInfo;

  /**
   * Table identifier for "FormatSettings": {a96047e7-3b08-42bd-8455-1032520a608f}.
   */
  static get FormatSettings(): FormatSettingsSchemeInfo {
    return SchemeInfo.formatSettings = SchemeInfo.formatSettings ?? new FormatSettingsSchemeInfo();
  }

  private static formatSettings: FormatSettingsSchemeInfo;

  /**
   * Table identifier for "FunctionRoles": {a59078ce-8acf-4c45-a49a-503fa88a0580}.
   */
  static get FunctionRoles(): FunctionRolesSchemeInfo {
    return SchemeInfo.functionRoles = SchemeInfo.functionRoles ?? new FunctionRolesSchemeInfo();
  }

  private static functionRoles: FunctionRolesSchemeInfo;

  /**
   * Table identifier for "FunctionRolesVirtual": {ef4bbb91-4d48-4c68-9e05-34ab4d5c2b36}.
   */
  static get FunctionRolesVirtual(): FunctionRolesVirtualSchemeInfo {
    return SchemeInfo.functionRolesVirtual = SchemeInfo.functionRolesVirtual ?? new FunctionRolesVirtualSchemeInfo();
  }

  private static functionRolesVirtual: FunctionRolesVirtualSchemeInfo;

  /**
   * Table identifier for "Functions": {57e45ca3-5036-4268-b8f9-86c4933a4d2d}.
   */
  static get Functions(): FunctionsSchemeInfo {
    return SchemeInfo.functions = SchemeInfo.functions ?? new FunctionsSchemeInfo();
  }

  private static functions: FunctionsSchemeInfo;

  /**
   * Table identifier for "HelpSections": {741301fd-f38a-4cca-bab9-df1328d53b53}.
   */
  static get HelpSections(): HelpSectionsSchemeInfo {
    return SchemeInfo.helpSections = SchemeInfo.helpSections ?? new HelpSectionsSchemeInfo();
  }

  private static helpSections: HelpSectionsSchemeInfo;

  /**
   * Table identifier for "IncomingRefDocs": {83785076-d844-4ea4-9e84-0a389c951ef4}.
   */
  static get IncomingRefDocs(): IncomingRefDocsSchemeInfo {
    return SchemeInfo.incomingRefDocs = SchemeInfo.incomingRefDocs ?? new IncomingRefDocsSchemeInfo();
  }

  private static incomingRefDocs: IncomingRefDocsSchemeInfo;

  /**
   * Table identifier for "Instances": {1074eadd-21d7-4925-98c8-40d1e5f0ca0e}.
   */
  static get Instances(): InstancesSchemeInfo {
    return SchemeInfo.instances = SchemeInfo.instances ?? new InstancesSchemeInfo();
  }

  private static instances: InstancesSchemeInfo;

  /**
   * Table identifier for "InstanceTypes": {2a567cee-1489-4a90-acf5-4f6d2c5bd67e}.
   */
  static get InstanceTypes(): InstanceTypesSchemeInfo {
    return SchemeInfo.instanceTypes = SchemeInfo.instanceTypes ?? new InstanceTypesSchemeInfo();
  }

  private static instanceTypes: InstanceTypesSchemeInfo;

  /**
   * Table identifier for "KrAcquaintanceAction": {2d90b630-c611-4137-8094-18986416c7b9}.
   */
  static get KrAcquaintanceAction(): KrAcquaintanceActionSchemeInfo {
    return SchemeInfo.krAcquaintanceAction = SchemeInfo.krAcquaintanceAction ?? new KrAcquaintanceActionSchemeInfo();
  }

  private static krAcquaintanceAction: KrAcquaintanceActionSchemeInfo;

  /**
   * Table identifier for "KrAcquaintanceActionRoles": {4c90a850-8ea9-4b07-8c8e-96145f624a3a}.
   */
  static get KrAcquaintanceActionRoles(): KrAcquaintanceActionRolesSchemeInfo {
    return SchemeInfo.krAcquaintanceActionRoles = SchemeInfo.krAcquaintanceActionRoles ?? new KrAcquaintanceActionRolesSchemeInfo();
  }

  private static krAcquaintanceActionRoles: KrAcquaintanceActionRolesSchemeInfo;

  /**
   * Table identifier for "KrAcquaintanceSettingsVirtual": {61a4ec06-f583-4eaf-8d91-c73de9f61164}.
   */
  static get KrAcquaintanceSettingsVirtual(): KrAcquaintanceSettingsVirtualSchemeInfo {
    return SchemeInfo.krAcquaintanceSettingsVirtual = SchemeInfo.krAcquaintanceSettingsVirtual ?? new KrAcquaintanceSettingsVirtualSchemeInfo();
  }

  private static krAcquaintanceSettingsVirtual: KrAcquaintanceSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrActionTypes": {b401e639-9167-4ada-9d46-4982bcd92488}.
   */
  static get KrActionTypes(): KrActionTypesSchemeInfo {
    return SchemeInfo.krActionTypes = SchemeInfo.krActionTypes ?? new KrActionTypesSchemeInfo();
  }

  private static krActionTypes: KrActionTypesSchemeInfo;

  /**
   * Table identifier for "KrActiveTasks": {c98ce2bb-a770-4e13-a1b6-314ba68f9bfc}.
   */
  static get KrActiveTasks(): KrActiveTasksSchemeInfo {
    return SchemeInfo.krActiveTasks = SchemeInfo.krActiveTasks ?? new KrActiveTasksSchemeInfo();
  }

  private static krActiveTasks: KrActiveTasksSchemeInfo;

  /**
   * Table identifier for "KrActiveTasksVirtual": {21dbb01c-1510-4318-b47d-c2be3197cdfb}.
   */
  static get KrActiveTasksVirtual(): KrActiveTasksVirtualSchemeInfo {
    return SchemeInfo.krActiveTasksVirtual = SchemeInfo.krActiveTasksVirtual ?? new KrActiveTasksVirtualSchemeInfo();
  }

  private static krActiveTasksVirtual: KrActiveTasksVirtualSchemeInfo;

  /**
   * Table identifier for "KrAddFromTemplateSettingsVirtual": {b31d2f6f-7980-4686-8029-3abd969ee11b}.
   */
  static get KrAddFromTemplateSettingsVirtual(): KrAddFromTemplateSettingsVirtualSchemeInfo {
    return SchemeInfo.krAddFromTemplateSettingsVirtual = SchemeInfo.krAddFromTemplateSettingsVirtual ?? new KrAddFromTemplateSettingsVirtualSchemeInfo();
  }

  private static krAddFromTemplateSettingsVirtual: KrAddFromTemplateSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrAdditionalApproval": {476425ca-8284-4c41-b11b-dd215042ee6a}.
   */
  static get KrAdditionalApproval(): KrAdditionalApprovalSchemeInfo {
    return SchemeInfo.krAdditionalApproval = SchemeInfo.krAdditionalApproval ?? new KrAdditionalApprovalSchemeInfo();
  }

  private static krAdditionalApproval: KrAdditionalApprovalSchemeInfo;

  /**
   * Table identifier for "KrAdditionalApprovalInfo": {5f83de75-7485-4785-9528-06ca0e41c5ba}.
   */
  static get KrAdditionalApprovalInfo(): KrAdditionalApprovalInfoSchemeInfo {
    return SchemeInfo.krAdditionalApprovalInfo = SchemeInfo.krAdditionalApprovalInfo ?? new KrAdditionalApprovalInfoSchemeInfo();
  }

  private static krAdditionalApprovalInfo: KrAdditionalApprovalInfoSchemeInfo;

  /**
   * Table identifier for "KrAdditionalApprovalInfoUsersCardVirtual": {fed14580-062d-4f30-a344-23c8d2a427d4}.
   */
  static get KrAdditionalApprovalInfoUsersCardVirtual(): KrAdditionalApprovalInfoUsersCardVirtualSchemeInfo {
    return SchemeInfo.krAdditionalApprovalInfoUsersCardVirtual = SchemeInfo.krAdditionalApprovalInfoUsersCardVirtual ?? new KrAdditionalApprovalInfoUsersCardVirtualSchemeInfo();
  }

  private static krAdditionalApprovalInfoUsersCardVirtual: KrAdditionalApprovalInfoUsersCardVirtualSchemeInfo;

  /**
   * Table identifier for "KrAdditionalApprovalInfoVirtual": {8a5782c7-06df-4c0b-9088-2efa46642e8e}.
   */
  static get KrAdditionalApprovalInfoVirtual(): KrAdditionalApprovalInfoVirtualSchemeInfo {
    return SchemeInfo.krAdditionalApprovalInfoVirtual = SchemeInfo.krAdditionalApprovalInfoVirtual ?? new KrAdditionalApprovalInfoVirtualSchemeInfo();
  }

  private static krAdditionalApprovalInfoVirtual: KrAdditionalApprovalInfoVirtualSchemeInfo;

  /**
   * Table identifier for "KrAdditionalApprovalsRequestedInfoVirtual": {c5d3a740-794c-4904-b9b1-e0a697a7dd80}.
   */
  static get KrAdditionalApprovalsRequestedInfoVirtual(): KrAdditionalApprovalsRequestedInfoVirtualSchemeInfo {
    return SchemeInfo.krAdditionalApprovalsRequestedInfoVirtual = SchemeInfo.krAdditionalApprovalsRequestedInfoVirtual ?? new KrAdditionalApprovalsRequestedInfoVirtualSchemeInfo();
  }

  private static krAdditionalApprovalsRequestedInfoVirtual: KrAdditionalApprovalsRequestedInfoVirtualSchemeInfo;

  /**
   * Table identifier for "KrAdditionalApprovalTaskInfo": {e0361d36-e2fd-48f9-875a-7ba9548932e5}.
   */
  static get KrAdditionalApprovalTaskInfo(): KrAdditionalApprovalTaskInfoSchemeInfo {
    return SchemeInfo.krAdditionalApprovalTaskInfo = SchemeInfo.krAdditionalApprovalTaskInfo ?? new KrAdditionalApprovalTaskInfoSchemeInfo();
  }

  private static krAdditionalApprovalTaskInfo: KrAdditionalApprovalTaskInfoSchemeInfo;

  /**
   * Table identifier for "KrAdditionalApprovalUsers": {72544086-2776-418a-a867-516ef7aad325}.
   */
  static get KrAdditionalApprovalUsers(): KrAdditionalApprovalUsersSchemeInfo {
    return SchemeInfo.krAdditionalApprovalUsers = SchemeInfo.krAdditionalApprovalUsers ?? new KrAdditionalApprovalUsersSchemeInfo();
  }

  private static krAdditionalApprovalUsers: KrAdditionalApprovalUsersSchemeInfo;

  /**
   * Table identifier for "KrAdditionalApprovalUsersCardVirtual": {a4c58948-fe22-4e9c-9cfe-5535a4c13990}.
   */
  static get KrAdditionalApprovalUsersCardVirtual(): KrAdditionalApprovalUsersCardVirtualSchemeInfo {
    return SchemeInfo.krAdditionalApprovalUsersCardVirtual = SchemeInfo.krAdditionalApprovalUsersCardVirtual ?? new KrAdditionalApprovalUsersCardVirtualSchemeInfo();
  }

  private static krAdditionalApprovalUsersCardVirtual: KrAdditionalApprovalUsersCardVirtualSchemeInfo;

  /**
   * Table identifier for "KrAmendingActionVirtual": {9cf234bf-ca74-46e7-a91a-564526cc1517}.
   */
  static get KrAmendingActionVirtual(): KrAmendingActionVirtualSchemeInfo {
    return SchemeInfo.krAmendingActionVirtual = SchemeInfo.krAmendingActionVirtual ?? new KrAmendingActionVirtualSchemeInfo();
  }

  private static krAmendingActionVirtual: KrAmendingActionVirtualSchemeInfo;

  /**
   * Table identifier for "KrApprovalActionAdditionalPerformersDisplayInfoVirtual": {f909d7b8-a840-4864-a2de-fd50c4475519}.
   */
  static get KrApprovalActionAdditionalPerformersDisplayInfoVirtual(): KrApprovalActionAdditionalPerformersDisplayInfoVirtualSchemeInfo {
    return SchemeInfo.krApprovalActionAdditionalPerformersDisplayInfoVirtual = SchemeInfo.krApprovalActionAdditionalPerformersDisplayInfoVirtual ?? new KrApprovalActionAdditionalPerformersDisplayInfoVirtualSchemeInfo();
  }

  private static krApprovalActionAdditionalPerformersDisplayInfoVirtual: KrApprovalActionAdditionalPerformersDisplayInfoVirtualSchemeInfo;

  /**
   * Table identifier for "KrApprovalActionAdditionalPerformersSettingsVirtual": {d96c77c3-dec7-4332-b427-bf77ad09c546}.
   */
  static get KrApprovalActionAdditionalPerformersSettingsVirtual(): KrApprovalActionAdditionalPerformersSettingsVirtualSchemeInfo {
    return SchemeInfo.krApprovalActionAdditionalPerformersSettingsVirtual = SchemeInfo.krApprovalActionAdditionalPerformersSettingsVirtual ?? new KrApprovalActionAdditionalPerformersSettingsVirtualSchemeInfo();
  }

  private static krApprovalActionAdditionalPerformersSettingsVirtual: KrApprovalActionAdditionalPerformersSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrApprovalActionAdditionalPerformersVirtual": {94a86f8e-ff0f-44fd-933b-9c7af3f35a13}.
   */
  static get KrApprovalActionAdditionalPerformersVirtual(): KrApprovalActionAdditionalPerformersVirtualSchemeInfo {
    return SchemeInfo.krApprovalActionAdditionalPerformersVirtual = SchemeInfo.krApprovalActionAdditionalPerformersVirtual ?? new KrApprovalActionAdditionalPerformersVirtualSchemeInfo();
  }

  private static krApprovalActionAdditionalPerformersVirtual: KrApprovalActionAdditionalPerformersVirtualSchemeInfo;

  /**
   * Table identifier for "KrApprovalActionNotificationActionRolesVirtual": {d299cae6-f32d-48b5-8930-031e78b3a2a1}.
   */
  static get KrApprovalActionNotificationActionRolesVirtual(): KrApprovalActionNotificationActionRolesVirtualSchemeInfo {
    return SchemeInfo.krApprovalActionNotificationActionRolesVirtual = SchemeInfo.krApprovalActionNotificationActionRolesVirtual ?? new KrApprovalActionNotificationActionRolesVirtualSchemeInfo();
  }

  private static krApprovalActionNotificationActionRolesVirtual: KrApprovalActionNotificationActionRolesVirtualSchemeInfo;

  /**
   * Table identifier for "KrApprovalActionNotificationRolesVirtual": {ae419e33-eb19-456c-a319-54da9ace8821}.
   */
  static get KrApprovalActionNotificationRolesVirtual(): KrApprovalActionNotificationRolesVirtualSchemeInfo {
    return SchemeInfo.krApprovalActionNotificationRolesVirtual = SchemeInfo.krApprovalActionNotificationRolesVirtual ?? new KrApprovalActionNotificationRolesVirtualSchemeInfo();
  }

  private static krApprovalActionNotificationRolesVirtual: KrApprovalActionNotificationRolesVirtualSchemeInfo;

  /**
   * Table identifier for "KrApprovalActionOptionLinksVirtual": {fc3ec595-313c-4b5f-aada-07d7d2f34ff2}.
   */
  static get KrApprovalActionOptionLinksVirtual(): KrApprovalActionOptionLinksVirtualSchemeInfo {
    return SchemeInfo.krApprovalActionOptionLinksVirtual = SchemeInfo.krApprovalActionOptionLinksVirtual ?? new KrApprovalActionOptionLinksVirtualSchemeInfo();
  }

  private static krApprovalActionOptionLinksVirtual: KrApprovalActionOptionLinksVirtualSchemeInfo;

  /**
   * Table identifier for "KrApprovalActionOptionsActionVirtual": {244719bf-4d4a-4df6-b2fe-a00b1bf6d173}.
   */
  static get KrApprovalActionOptionsActionVirtual(): KrApprovalActionOptionsActionVirtualSchemeInfo {
    return SchemeInfo.krApprovalActionOptionsActionVirtual = SchemeInfo.krApprovalActionOptionsActionVirtual ?? new KrApprovalActionOptionsActionVirtualSchemeInfo();
  }

  private static krApprovalActionOptionsActionVirtual: KrApprovalActionOptionsActionVirtualSchemeInfo;

  /**
   * Table identifier for "KrApprovalActionOptionsVirtual": {cea61a5b-0420-41ba-a5f2-e21c21c30f5a}.
   */
  static get KrApprovalActionOptionsVirtual(): KrApprovalActionOptionsVirtualSchemeInfo {
    return SchemeInfo.krApprovalActionOptionsVirtual = SchemeInfo.krApprovalActionOptionsVirtual ?? new KrApprovalActionOptionsVirtualSchemeInfo();
  }

  private static krApprovalActionOptionsVirtual: KrApprovalActionOptionsVirtualSchemeInfo;

  /**
   * Table identifier for "KrApprovalActionVirtual": {2afee5e8-3582-4c7c-9fcf-1e4fddefe548}.
   */
  static get KrApprovalActionVirtual(): KrApprovalActionVirtualSchemeInfo {
    return SchemeInfo.krApprovalActionVirtual = SchemeInfo.krApprovalActionVirtual ?? new KrApprovalActionVirtualSchemeInfo();
  }

  private static krApprovalActionVirtual: KrApprovalActionVirtualSchemeInfo;

  /**
   * Table identifier for "KrApprovalCommonInfo": {410324bf-ce75-4024-a14c-5d78a8ad7588}.
   */
  static get KrApprovalCommonInfo(): KrApprovalCommonInfoSchemeInfo {
    return SchemeInfo.krApprovalCommonInfo = SchemeInfo.krApprovalCommonInfo ?? new KrApprovalCommonInfoSchemeInfo();
  }

  private static krApprovalCommonInfo: KrApprovalCommonInfoSchemeInfo;

  /**
   * Table identifier for "KrApprovalCommonInfoVirtual": {fe5739f6-d64b-45f5-a3a3-75e999f721dd}.
   */
  static get KrApprovalCommonInfoVirtual(): KrApprovalCommonInfoVirtualSchemeInfo {
    return SchemeInfo.krApprovalCommonInfoVirtual = SchemeInfo.krApprovalCommonInfoVirtual ?? new KrApprovalCommonInfoVirtualSchemeInfo();
  }

  private static krApprovalCommonInfoVirtual: KrApprovalCommonInfoVirtualSchemeInfo;

  /**
   * Table identifier for "KrApprovalHistory": {07d45e20-a501-4e3b-a246-e548a74d0730}.
   */
  static get KrApprovalHistory(): KrApprovalHistorySchemeInfo {
    return SchemeInfo.krApprovalHistory = SchemeInfo.krApprovalHistory ?? new KrApprovalHistorySchemeInfo();
  }

  private static krApprovalHistory: KrApprovalHistorySchemeInfo;

  /**
   * Table identifier for "KrApprovalHistoryVirtual": {64a54b5b-bbd2-49ae-a378-1e8daa88c070}.
   */
  static get KrApprovalHistoryVirtual(): KrApprovalHistoryVirtualSchemeInfo {
    return SchemeInfo.krApprovalHistoryVirtual = SchemeInfo.krApprovalHistoryVirtual ?? new KrApprovalHistoryVirtualSchemeInfo();
  }

  private static krApprovalHistoryVirtual: KrApprovalHistoryVirtualSchemeInfo;

  /**
   * Table identifier for "KrApprovalSettingsVirtual": {5a48521b-e00c-44b6-995e-8f238e9103ff}.
   */
  static get KrApprovalSettingsVirtual(): KrApprovalSettingsVirtualSchemeInfo {
    return SchemeInfo.krApprovalSettingsVirtual = SchemeInfo.krApprovalSettingsVirtual ?? new KrApprovalSettingsVirtualSchemeInfo();
  }

  private static krApprovalSettingsVirtual: KrApprovalSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrAuthorSettingsVirtual": {17931d48-fae6-415e-bb76-3ea3a457a2e9}.
   */
  static get KrAuthorSettingsVirtual(): KrAuthorSettingsVirtualSchemeInfo {
    return SchemeInfo.krAuthorSettingsVirtual = SchemeInfo.krAuthorSettingsVirtual ?? new KrAuthorSettingsVirtualSchemeInfo();
  }

  private static krAuthorSettingsVirtual: KrAuthorSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrAutoApproveHistory": {dee2be6f-5d24-443f-b468-f6e03a6742b5}.
   */
  static get KrAutoApproveHistory(): KrAutoApproveHistorySchemeInfo {
    return SchemeInfo.krAutoApproveHistory = SchemeInfo.krAutoApproveHistory ?? new KrAutoApproveHistorySchemeInfo();
  }

  private static krAutoApproveHistory: KrAutoApproveHistorySchemeInfo;

  /**
   * Table identifier for "KrBuildGlobalOutputVirtual": {0d23c056-70cc-4b25-9c3b-d6e2a9e48509}.
   */
  static get KrBuildGlobalOutputVirtual(): KrBuildGlobalOutputVirtualSchemeInfo {
    return SchemeInfo.krBuildGlobalOutputVirtual = SchemeInfo.krBuildGlobalOutputVirtual ?? new KrBuildGlobalOutputVirtualSchemeInfo();
  }

  private static krBuildGlobalOutputVirtual: KrBuildGlobalOutputVirtualSchemeInfo;

  /**
   * Table identifier for "KrBuildLocalOutputVirtual": {255f542f-3469-4c42-928d-7cf2cfedb644}.
   */
  static get KrBuildLocalOutputVirtual(): KrBuildLocalOutputVirtualSchemeInfo {
    return SchemeInfo.krBuildLocalOutputVirtual = SchemeInfo.krBuildLocalOutputVirtual ?? new KrBuildLocalOutputVirtualSchemeInfo();
  }

  private static krBuildLocalOutputVirtual: KrBuildLocalOutputVirtualSchemeInfo;

  /**
   * Table identifier for "KrBuildStates": {e12af590-efd5-4890-b1c7-5a7ce83195dd}.
   */
  static get KrBuildStates(): KrBuildStatesSchemeInfo {
    return SchemeInfo.krBuildStates = SchemeInfo.krBuildStates ?? new KrBuildStatesSchemeInfo();
  }

  private static krBuildStates: KrBuildStatesSchemeInfo;

  /**
   * Table identifier for "KrCardGeneratorVirtual": {1052a0bc-1a02-4fd4-9636-5dacd0acc436}.
   */
  static get KrCardGeneratorVirtual(): KrCardGeneratorVirtualSchemeInfo {
    return SchemeInfo.krCardGeneratorVirtual = SchemeInfo.krCardGeneratorVirtual ?? new KrCardGeneratorVirtualSchemeInfo();
  }

  private static krCardGeneratorVirtual: KrCardGeneratorVirtualSchemeInfo;

  /**
   * Table identifier for "KrCardTasksEditorDialogVirtual": {41c02d34-dd86-4115-a485-1bf5e32d2074}.
   */
  static get KrCardTasksEditorDialogVirtual(): KrCardTasksEditorDialogVirtualSchemeInfo {
    return SchemeInfo.krCardTasksEditorDialogVirtual = SchemeInfo.krCardTasksEditorDialogVirtual ?? new KrCardTasksEditorDialogVirtualSchemeInfo();
  }

  private static krCardTasksEditorDialogVirtual: KrCardTasksEditorDialogVirtualSchemeInfo;

  /**
   * Table identifier for "KrCardTypesVirtual": {a90baecf-c9ce-4cba-8bb0-150a13666266}.
   */
  static get KrCardTypesVirtual(): KrCardTypesVirtualSchemeInfo {
    return SchemeInfo.krCardTypesVirtual = SchemeInfo.krCardTypesVirtual ?? new KrCardTypesVirtualSchemeInfo();
  }

  private static krCardTypesVirtual: KrCardTypesVirtualSchemeInfo;

  /**
   * Table identifier for "KrChangeStateAction": {1afa15c7-ca17-4fa9-bfe5-3ca066814247}.
   */
  static get KrChangeStateAction(): KrChangeStateActionSchemeInfo {
    return SchemeInfo.krChangeStateAction = SchemeInfo.krChangeStateAction ?? new KrChangeStateActionSchemeInfo();
  }

  private static krChangeStateAction: KrChangeStateActionSchemeInfo;

  /**
   * Table identifier for "KrChangeStateSettingsVirtual": {bc1450c4-0ddd-4efd-9636-f2ec5d013979}.
   */
  static get KrChangeStateSettingsVirtual(): KrChangeStateSettingsVirtualSchemeInfo {
    return SchemeInfo.krChangeStateSettingsVirtual = SchemeInfo.krChangeStateSettingsVirtual ?? new KrChangeStateSettingsVirtualSchemeInfo();
  }

  private static krChangeStateSettingsVirtual: KrChangeStateSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrCheckStateTileExtension": {15368402-1522-4722-91b7-d27636f3596b}.
   */
  static get KrCheckStateTileExtension(): KrCheckStateTileExtensionSchemeInfo {
    return SchemeInfo.krCheckStateTileExtension = SchemeInfo.krCheckStateTileExtension ?? new KrCheckStateTileExtensionSchemeInfo();
  }

  private static krCheckStateTileExtension: KrCheckStateTileExtensionSchemeInfo;

  /**
   * Table identifier for "KrCommentators": {42c4f5aa-d0e8-4d26-abb8-a898e736fe35}.
   */
  static get KrCommentators(): KrCommentatorsSchemeInfo {
    return SchemeInfo.krCommentators = SchemeInfo.krCommentators ?? new KrCommentatorsSchemeInfo();
  }

  private static krCommentators: KrCommentatorsSchemeInfo;

  /**
   * Table identifier for "KrCommentsInfo": {b75dc3d2-10c8-4ca9-a63c-2a8f54db5c42}.
   */
  static get KrCommentsInfo(): KrCommentsInfoSchemeInfo {
    return SchemeInfo.krCommentsInfo = SchemeInfo.krCommentsInfo ?? new KrCommentsInfoSchemeInfo();
  }

  private static krCommentsInfo: KrCommentsInfoSchemeInfo;

  /**
   * Table identifier for "KrCommentsInfoVirtual": {e490c196-41b3-489b-8425-ee36a0119f64}.
   */
  static get KrCommentsInfoVirtual(): KrCommentsInfoVirtualSchemeInfo {
    return SchemeInfo.krCommentsInfoVirtual = SchemeInfo.krCommentsInfoVirtual ?? new KrCommentsInfoVirtualSchemeInfo();
  }

  private static krCommentsInfoVirtual: KrCommentsInfoVirtualSchemeInfo;

  /**
   * Table identifier for "KrCreateCardAction": {70e22440-564a-40a9-88a1-f695844a113b}.
   */
  static get KrCreateCardAction(): KrCreateCardActionSchemeInfo {
    return SchemeInfo.krCreateCardAction = SchemeInfo.krCreateCardAction ?? new KrCreateCardActionSchemeInfo();
  }

  private static krCreateCardAction: KrCreateCardActionSchemeInfo;

  /**
   * Table identifier for "KrCreateCardStageSettingsVirtual": {644515d1-8e3f-419e-b938-f59c5ec07fae}.
   */
  static get KrCreateCardStageSettingsVirtual(): KrCreateCardStageSettingsVirtualSchemeInfo {
    return SchemeInfo.krCreateCardStageSettingsVirtual = SchemeInfo.krCreateCardStageSettingsVirtual ?? new KrCreateCardStageSettingsVirtualSchemeInfo();
  }

  private static krCreateCardStageSettingsVirtual: KrCreateCardStageSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrCreateCardStageTypeModes": {ebf6257e-c0c6-4f84-b913-7a66fc196418}.
   */
  static get KrCreateCardStageTypeModes(): KrCreateCardStageTypeModesSchemeInfo {
    return SchemeInfo.krCreateCardStageTypeModes = SchemeInfo.krCreateCardStageTypeModes ?? new KrCreateCardStageTypeModesSchemeInfo();
  }

  private static krCreateCardStageTypeModes: KrCreateCardStageTypeModesSchemeInfo;

  /**
   * Table identifier for "KrCycleGroupingModes": {3e451f29-8808-4398-930e-d5c172c21de7}.
   */
  static get KrCycleGroupingModes(): KrCycleGroupingModesSchemeInfo {
    return SchemeInfo.krCycleGroupingModes = SchemeInfo.krCycleGroupingModes ?? new KrCycleGroupingModesSchemeInfo();
  }

  private static krCycleGroupingModes: KrCycleGroupingModesSchemeInfo;

  /**
   * Table identifier for "KrDepartmentCondition": {70427d3c-3df8-4efc-8bf7-8e19efa2c20d}.
   */
  static get KrDepartmentCondition(): KrDepartmentConditionSchemeInfo {
    return SchemeInfo.krDepartmentCondition = SchemeInfo.krDepartmentCondition ?? new KrDepartmentConditionSchemeInfo();
  }

  private static krDepartmentCondition: KrDepartmentConditionSchemeInfo;

  /**
   * Table identifier for "KrDepartmentConditionSettings": {f753b988-0c00-471b-869d-0ac361af0d83}.
   */
  static get KrDepartmentConditionSettings(): KrDepartmentConditionSettingsSchemeInfo {
    return SchemeInfo.krDepartmentConditionSettings = SchemeInfo.krDepartmentConditionSettings ?? new KrDepartmentConditionSettingsSchemeInfo();
  }

  private static krDepartmentConditionSettings: KrDepartmentConditionSettingsSchemeInfo;

  /**
   * Table identifier for "KrDialogButtonSettingsVirtual": {0d52e2ff-45ec-449d-bd49-e0b5f666ee65}.
   */
  static get KrDialogButtonSettingsVirtual(): KrDialogButtonSettingsVirtualSchemeInfo {
    return SchemeInfo.krDialogButtonSettingsVirtual = SchemeInfo.krDialogButtonSettingsVirtual ?? new KrDialogButtonSettingsVirtualSchemeInfo();
  }

  private static krDialogButtonSettingsVirtual: KrDialogButtonSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrDialogStageTypeSettingsVirtual": {663dcabe-f9d8-4a52-a235-6c407e683810}.
   */
  static get KrDialogStageTypeSettingsVirtual(): KrDialogStageTypeSettingsVirtualSchemeInfo {
    return SchemeInfo.krDialogStageTypeSettingsVirtual = SchemeInfo.krDialogStageTypeSettingsVirtual ?? new KrDialogStageTypeSettingsVirtualSchemeInfo();
  }

  private static krDialogStageTypeSettingsVirtual: KrDialogStageTypeSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrDocNumberRegistrationAutoAssignment": {b965332c-296b-48e3-b16f-21a0cd8a6a25}.
   */
  static get KrDocNumberRegistrationAutoAssignment(): KrDocNumberRegistrationAutoAssignmentSchemeInfo {
    return SchemeInfo.krDocNumberRegistrationAutoAssignment = SchemeInfo.krDocNumberRegistrationAutoAssignment ?? new KrDocNumberRegistrationAutoAssignmentSchemeInfo();
  }

  private static krDocNumberRegistrationAutoAssignment: KrDocNumberRegistrationAutoAssignmentSchemeInfo;

  /**
   * Table identifier for "KrDocNumberRegularAutoAssignment": {83b4c03f-fdb8-4e11-bca4-02177dd4b3dc}.
   */
  static get KrDocNumberRegularAutoAssignment(): KrDocNumberRegularAutoAssignmentSchemeInfo {
    return SchemeInfo.krDocNumberRegularAutoAssignment = SchemeInfo.krDocNumberRegularAutoAssignment ?? new KrDocNumberRegularAutoAssignmentSchemeInfo();
  }

  private static krDocNumberRegularAutoAssignment: KrDocNumberRegularAutoAssignmentSchemeInfo;

  /**
   * Table identifier for "KrDocState": {47107d7a-3a8c-47f0-b800-2a45da222ff4}.
   */
  static get KrDocState(): KrDocStateSchemeInfo {
    return SchemeInfo.krDocState = SchemeInfo.krDocState ?? new KrDocStateSchemeInfo();
  }

  private static krDocState: KrDocStateSchemeInfo;

  /**
   * Table identifier for "KrDocStateCondition": {204bfce7-5a88-4586-90e5-36d69e5b39fa}.
   */
  static get KrDocStateCondition(): KrDocStateConditionSchemeInfo {
    return SchemeInfo.krDocStateCondition = SchemeInfo.krDocStateCondition ?? new KrDocStateConditionSchemeInfo();
  }

  private static krDocStateCondition: KrDocStateConditionSchemeInfo;

  /**
   * Table identifier for "KrDocStateVirtual": {e4345324-ad03-46ca-a157-5f71742e5816}.
   */
  static get KrDocStateVirtual(): KrDocStateVirtualSchemeInfo {
    return SchemeInfo.krDocStateVirtual = SchemeInfo.krDocStateVirtual ?? new KrDocStateVirtualSchemeInfo();
  }

  private static krDocStateVirtual: KrDocStateVirtualSchemeInfo;

  /**
   * Table identifier for "KrDocType": {78bfc212-cad5-4d1d-8b91-a9c58562b9d5}.
   */
  static get KrDocType(): KrDocTypeSchemeInfo {
    return SchemeInfo.krDocType = SchemeInfo.krDocType ?? new KrDocTypeSchemeInfo();
  }

  private static krDocType: KrDocTypeSchemeInfo;

  /**
   * Table identifier for "KrDocTypeCondition": {7a74559a-0729-4dd8-9040-3367367ac673}.
   */
  static get KrDocTypeCondition(): KrDocTypeConditionSchemeInfo {
    return SchemeInfo.krDocTypeCondition = SchemeInfo.krDocTypeCondition ?? new KrDocTypeConditionSchemeInfo();
  }

  private static krDocTypeCondition: KrDocTypeConditionSchemeInfo;

  /**
   * Table identifier for "KrEditSettingsVirtual": {ef86e270-047b-4b7c-9c22-dda56e8eef2c}.
   */
  static get KrEditSettingsVirtual(): KrEditSettingsVirtualSchemeInfo {
    return SchemeInfo.krEditSettingsVirtual = SchemeInfo.krEditSettingsVirtual ?? new KrEditSettingsVirtualSchemeInfo();
  }

  private static krEditSettingsVirtual: KrEditSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrForkManagementModes": {75e444ae-a785-4e30-a6e0-15020a31654d}.
   */
  static get KrForkManagementModes(): KrForkManagementModesSchemeInfo {
    return SchemeInfo.krForkManagementModes = SchemeInfo.krForkManagementModes ?? new KrForkManagementModesSchemeInfo();
  }

  private static krForkManagementModes: KrForkManagementModesSchemeInfo;

  /**
   * Table identifier for "KrForkManagementSettingsVirtual": {c6397b27-d2a4-4b67-9450-7bb19a69fbbf}.
   */
  static get KrForkManagementSettingsVirtual(): KrForkManagementSettingsVirtualSchemeInfo {
    return SchemeInfo.krForkManagementSettingsVirtual = SchemeInfo.krForkManagementSettingsVirtual ?? new KrForkManagementSettingsVirtualSchemeInfo();
  }

  private static krForkManagementSettingsVirtual: KrForkManagementSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrForkNestedProcessesSettingsVirtual": {e8f3015f-4085-4df8-bafb-4c5b466965c0}.
   */
  static get KrForkNestedProcessesSettingsVirtual(): KrForkNestedProcessesSettingsVirtualSchemeInfo {
    return SchemeInfo.krForkNestedProcessesSettingsVirtual = SchemeInfo.krForkNestedProcessesSettingsVirtual ?? new KrForkNestedProcessesSettingsVirtualSchemeInfo();
  }

  private static krForkNestedProcessesSettingsVirtual: KrForkNestedProcessesSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrForkSecondaryProcessesSettingsVirtual": {08119dad-4504-49f5-8273-a1851cc4a0d0}.
   */
  static get KrForkSecondaryProcessesSettingsVirtual(): KrForkSecondaryProcessesSettingsVirtualSchemeInfo {
    return SchemeInfo.krForkSecondaryProcessesSettingsVirtual = SchemeInfo.krForkSecondaryProcessesSettingsVirtual ?? new KrForkSecondaryProcessesSettingsVirtualSchemeInfo();
  }

  private static krForkSecondaryProcessesSettingsVirtual: KrForkSecondaryProcessesSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrForkSettingsVirtual": {27d6b3b7-8347-4e3c-982c-437f6c87ab13}.
   */
  static get KrForkSettingsVirtual(): KrForkSettingsVirtualSchemeInfo {
    return SchemeInfo.krForkSettingsVirtual = SchemeInfo.krForkSettingsVirtual ?? new KrForkSettingsVirtualSchemeInfo();
  }

  private static krForkSettingsVirtual: KrForkSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrHistoryManagementStageSettingsVirtual": {e08c3797-0f25-4841-a2a4-37bb0b938f88}.
   */
  static get KrHistoryManagementStageSettingsVirtual(): KrHistoryManagementStageSettingsVirtualSchemeInfo {
    return SchemeInfo.krHistoryManagementStageSettingsVirtual = SchemeInfo.krHistoryManagementStageSettingsVirtual ?? new KrHistoryManagementStageSettingsVirtualSchemeInfo();
  }

  private static krHistoryManagementStageSettingsVirtual: KrHistoryManagementStageSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrInfoForInitiator": {22a3da4b-ac30-4a40-a069-3d6ee66079a0}.
   */
  static get KrInfoForInitiator(): KrInfoForInitiatorSchemeInfo {
    return SchemeInfo.krInfoForInitiator = SchemeInfo.krInfoForInitiator ?? new KrInfoForInitiatorSchemeInfo();
  }

  private static krInfoForInitiator: KrInfoForInitiatorSchemeInfo;

  /**
   * Table identifier for "KrNotificationOptionalRecipientsVirtual": {2bd36c6d-c035-4407-a270-d329fae7ec76}.
   */
  static get KrNotificationOptionalRecipientsVirtual(): KrNotificationOptionalRecipientsVirtualSchemeInfo {
    return SchemeInfo.krNotificationOptionalRecipientsVirtual = SchemeInfo.krNotificationOptionalRecipientsVirtual ?? new KrNotificationOptionalRecipientsVirtualSchemeInfo();
  }

  private static krNotificationOptionalRecipientsVirtual: KrNotificationOptionalRecipientsVirtualSchemeInfo;

  /**
   * Table identifier for "KrNotificationSettingVirtual": {28204069-f27e-4b4e-b309-5d2f77dbff8e}.
   */
  static get KrNotificationSettingVirtual(): KrNotificationSettingVirtualSchemeInfo {
    return SchemeInfo.krNotificationSettingVirtual = SchemeInfo.krNotificationSettingVirtual ?? new KrNotificationSettingVirtualSchemeInfo();
  }

  private static krNotificationSettingVirtual: KrNotificationSettingVirtualSchemeInfo;

  /**
   * Table identifier for "KrPartnerCondition": {82f81a44-b515-4187-88c9-03a59e086031}.
   */
  static get KrPartnerCondition(): KrPartnerConditionSchemeInfo {
    return SchemeInfo.krPartnerCondition = SchemeInfo.krPartnerCondition ?? new KrPartnerConditionSchemeInfo();
  }

  private static krPartnerCondition: KrPartnerConditionSchemeInfo;

  /**
   * Table identifier for "KrPerformersVirtual": {b47d668e-7bf0-4165-a10c-6fe22ee10882}.
   */
  static get KrPerformersVirtual(): KrPerformersVirtualSchemeInfo {
    return SchemeInfo.krPerformersVirtual = SchemeInfo.krPerformersVirtual ?? new KrPerformersVirtualSchemeInfo();
  }

  private static krPerformersVirtual: KrPerformersVirtualSchemeInfo;

  /**
   * Table identifier for "KrPermissionAclGenerationRules": {04cb0b04-b5c2-477c-ae4a-3d1e19f9530a}.
   */
  static get KrPermissionAclGenerationRules(): KrPermissionAclGenerationRulesSchemeInfo {
    return SchemeInfo.krPermissionAclGenerationRules = SchemeInfo.krPermissionAclGenerationRules ?? new KrPermissionAclGenerationRulesSchemeInfo();
  }

  private static krPermissionAclGenerationRules: KrPermissionAclGenerationRulesSchemeInfo;

  /**
   * Table identifier for "KrPermissionExtendedCardRuleFields": {a40f2a59-e858-499d-a24a-0f18aab6cbd0}.
   */
  static get KrPermissionExtendedCardRuleFields(): KrPermissionExtendedCardRuleFieldsSchemeInfo {
    return SchemeInfo.krPermissionExtendedCardRuleFields = SchemeInfo.krPermissionExtendedCardRuleFields ?? new KrPermissionExtendedCardRuleFieldsSchemeInfo();
  }

  private static krPermissionExtendedCardRuleFields: KrPermissionExtendedCardRuleFieldsSchemeInfo;

  /**
   * Table identifier for "KrPermissionExtendedCardRules": {24c7c7fa-0c39-44c5-aa8d-0199ab79606e}.
   */
  static get KrPermissionExtendedCardRules(): KrPermissionExtendedCardRulesSchemeInfo {
    return SchemeInfo.krPermissionExtendedCardRules = SchemeInfo.krPermissionExtendedCardRules ?? new KrPermissionExtendedCardRulesSchemeInfo();
  }

  private static krPermissionExtendedCardRules: KrPermissionExtendedCardRulesSchemeInfo;

  /**
   * Table identifier for "KrPermissionExtendedFileRuleCategories": {2a337def-1279-456a-a61a-0232aa082123}.
   */
  static get KrPermissionExtendedFileRuleCategories(): KrPermissionExtendedFileRuleCategoriesSchemeInfo {
    return SchemeInfo.krPermissionExtendedFileRuleCategories = SchemeInfo.krPermissionExtendedFileRuleCategories ?? new KrPermissionExtendedFileRuleCategoriesSchemeInfo();
  }

  private static krPermissionExtendedFileRuleCategories: KrPermissionExtendedFileRuleCategoriesSchemeInfo;

  /**
   * Table identifier for "KrPermissionExtendedFileRules": {7ca15c10-9fd1-46e9-8769-b0acc0efe118}.
   */
  static get KrPermissionExtendedFileRules(): KrPermissionExtendedFileRulesSchemeInfo {
    return SchemeInfo.krPermissionExtendedFileRules = SchemeInfo.krPermissionExtendedFileRules ?? new KrPermissionExtendedFileRulesSchemeInfo();
  }

  private static krPermissionExtendedFileRules: KrPermissionExtendedFileRulesSchemeInfo;

  /**
   * Table identifier for "KrPermissionExtendedMandatoryRuleFields": {16588bc2-69cf-4a54-bf16-b0bf9507a315}.
   */
  static get KrPermissionExtendedMandatoryRuleFields(): KrPermissionExtendedMandatoryRuleFieldsSchemeInfo {
    return SchemeInfo.krPermissionExtendedMandatoryRuleFields = SchemeInfo.krPermissionExtendedMandatoryRuleFields ?? new KrPermissionExtendedMandatoryRuleFieldsSchemeInfo();
  }

  private static krPermissionExtendedMandatoryRuleFields: KrPermissionExtendedMandatoryRuleFieldsSchemeInfo;

  /**
   * Table identifier for "KrPermissionExtendedMandatoryRuleOptions": {ae17320c-ff1b-45fb-9dd4-f9d99c24d824}.
   */
  static get KrPermissionExtendedMandatoryRuleOptions(): KrPermissionExtendedMandatoryRuleOptionsSchemeInfo {
    return SchemeInfo.krPermissionExtendedMandatoryRuleOptions = SchemeInfo.krPermissionExtendedMandatoryRuleOptions ?? new KrPermissionExtendedMandatoryRuleOptionsSchemeInfo();
  }

  private static krPermissionExtendedMandatoryRuleOptions: KrPermissionExtendedMandatoryRuleOptionsSchemeInfo;

  /**
   * Table identifier for "KrPermissionExtendedMandatoryRules": {a4b6af05-9147-4335-8bf4-0e7387f77455}.
   */
  static get KrPermissionExtendedMandatoryRules(): KrPermissionExtendedMandatoryRulesSchemeInfo {
    return SchemeInfo.krPermissionExtendedMandatoryRules = SchemeInfo.krPermissionExtendedMandatoryRules ?? new KrPermissionExtendedMandatoryRulesSchemeInfo();
  }

  private static krPermissionExtendedMandatoryRules: KrPermissionExtendedMandatoryRulesSchemeInfo;

  /**
   * Table identifier for "KrPermissionExtendedMandatoryRuleTypes": {a707b171-d676-45fd-8386-3bd3f20b7a1a}.
   */
  static get KrPermissionExtendedMandatoryRuleTypes(): KrPermissionExtendedMandatoryRuleTypesSchemeInfo {
    return SchemeInfo.krPermissionExtendedMandatoryRuleTypes = SchemeInfo.krPermissionExtendedMandatoryRuleTypes ?? new KrPermissionExtendedMandatoryRuleTypesSchemeInfo();
  }

  private static krPermissionExtendedMandatoryRuleTypes: KrPermissionExtendedMandatoryRuleTypesSchemeInfo;

  /**
   * Table identifier for "KrPermissionExtendedTaskRuleFields": {c30346b8-91a6-4dcd-8324-254e253f0148}.
   */
  static get KrPermissionExtendedTaskRuleFields(): KrPermissionExtendedTaskRuleFieldsSchemeInfo {
    return SchemeInfo.krPermissionExtendedTaskRuleFields = SchemeInfo.krPermissionExtendedTaskRuleFields ?? new KrPermissionExtendedTaskRuleFieldsSchemeInfo();
  }

  private static krPermissionExtendedTaskRuleFields: KrPermissionExtendedTaskRuleFieldsSchemeInfo;

  /**
   * Table identifier for "KrPermissionExtendedTaskRules": {536f27ed-f1d2-4850-ad9e-eab93f584f1a}.
   */
  static get KrPermissionExtendedTaskRules(): KrPermissionExtendedTaskRulesSchemeInfo {
    return SchemeInfo.krPermissionExtendedTaskRules = SchemeInfo.krPermissionExtendedTaskRules ?? new KrPermissionExtendedTaskRulesSchemeInfo();
  }

  private static krPermissionExtendedTaskRules: KrPermissionExtendedTaskRulesSchemeInfo;

  /**
   * Table identifier for "KrPermissionExtendedTaskRuleTypes": {c7f6a799-0dd6-4389-9122-bafc68f35c9e}.
   */
  static get KrPermissionExtendedTaskRuleTypes(): KrPermissionExtendedTaskRuleTypesSchemeInfo {
    return SchemeInfo.krPermissionExtendedTaskRuleTypes = SchemeInfo.krPermissionExtendedTaskRuleTypes ?? new KrPermissionExtendedTaskRuleTypesSchemeInfo();
  }

  private static krPermissionExtendedTaskRuleTypes: KrPermissionExtendedTaskRuleTypesSchemeInfo;

  /**
   * Table identifier for "KrPermissionExtendedVisibilityRules": {aa13a164-dc2e-47e2-a415-021b8b5666e9}.
   */
  static get KrPermissionExtendedVisibilityRules(): KrPermissionExtendedVisibilityRulesSchemeInfo {
    return SchemeInfo.krPermissionExtendedVisibilityRules = SchemeInfo.krPermissionExtendedVisibilityRules ?? new KrPermissionExtendedVisibilityRulesSchemeInfo();
  }

  private static krPermissionExtendedVisibilityRules: KrPermissionExtendedVisibilityRulesSchemeInfo;

  /**
   * Table identifier for "KrPermissionRoles": {79a6e9e0-e52f-456f-871a-00b6895566ec}.
   */
  static get KrPermissionRoles(): KrPermissionRolesSchemeInfo {
    return SchemeInfo.krPermissionRoles = SchemeInfo.krPermissionRoles ?? new KrPermissionRolesSchemeInfo();
  }

  private static krPermissionRoles: KrPermissionRolesSchemeInfo;

  /**
   * Table identifier for "KrPermissionRuleAccessSettings": {4c274eda-ab9a-403f-9e5b-0b933283b5a3}.
   */
  static get KrPermissionRuleAccessSettings(): KrPermissionRuleAccessSettingsSchemeInfo {
    return SchemeInfo.krPermissionRuleAccessSettings = SchemeInfo.krPermissionRuleAccessSettings ?? new KrPermissionRuleAccessSettingsSchemeInfo();
  }

  private static krPermissionRuleAccessSettings: KrPermissionRuleAccessSettingsSchemeInfo;

  /**
   * Table identifier for "KrPermissions": {1c7406cb-e445-4d1a-bf00-a1116db39bc6}.
   */
  static get KrPermissions(): KrPermissionsSchemeInfo {
    return SchemeInfo.krPermissions = SchemeInfo.krPermissions ?? new KrPermissionsSchemeInfo();
  }

  private static krPermissions: KrPermissionsSchemeInfo;

  /**
   * Table identifier for "KrPermissionsControlTypes": {18ad7847-b0f7-4d74-bc04-d96cbf18eecd}.
   */
  static get KrPermissionsControlTypes(): KrPermissionsControlTypesSchemeInfo {
    return SchemeInfo.krPermissionsControlTypes = SchemeInfo.krPermissionsControlTypes ?? new KrPermissionsControlTypesSchemeInfo();
  }

  private static krPermissionsControlTypes: KrPermissionsControlTypesSchemeInfo;

  /**
   * Table identifier for "KrPermissionsFileCheckRules": {2baf9cf1-a8d5-4e82-bccd-769d7c70e10a}.
   */
  static get KrPermissionsFileCheckRules(): KrPermissionsFileCheckRulesSchemeInfo {
    return SchemeInfo.krPermissionsFileCheckRules = SchemeInfo.krPermissionsFileCheckRules ?? new KrPermissionsFileCheckRulesSchemeInfo();
  }

  private static krPermissionsFileCheckRules: KrPermissionsFileCheckRulesSchemeInfo;

  /**
   * Table identifier for "KrPermissionsFileEditAccessSettings": {9247ed2e-109d-4543-b888-1fe9da9479aa}.
   */
  static get KrPermissionsFileEditAccessSettings(): KrPermissionsFileEditAccessSettingsSchemeInfo {
    return SchemeInfo.krPermissionsFileEditAccessSettings = SchemeInfo.krPermissionsFileEditAccessSettings ?? new KrPermissionsFileEditAccessSettingsSchemeInfo();
  }

  private static krPermissionsFileEditAccessSettings: KrPermissionsFileEditAccessSettingsSchemeInfo;

  /**
   * Table identifier for "KrPermissionsFileReadAccessSettings": {95a74318-2e98-46bd-bced-1890dd1cd017}.
   */
  static get KrPermissionsFileReadAccessSettings(): KrPermissionsFileReadAccessSettingsSchemeInfo {
    return SchemeInfo.krPermissionsFileReadAccessSettings = SchemeInfo.krPermissionsFileReadAccessSettings ?? new KrPermissionsFileReadAccessSettingsSchemeInfo();
  }

  private static krPermissionsFileReadAccessSettings: KrPermissionsFileReadAccessSettingsSchemeInfo;

  /**
   * Table identifier for "KrPermissionsMandatoryValidationTypes": {4439a1f6-c747-442b-b315-caae1c934058}.
   */
  static get KrPermissionsMandatoryValidationTypes(): KrPermissionsMandatoryValidationTypesSchemeInfo {
    return SchemeInfo.krPermissionsMandatoryValidationTypes = SchemeInfo.krPermissionsMandatoryValidationTypes ?? new KrPermissionsMandatoryValidationTypesSchemeInfo();
  }

  private static krPermissionsMandatoryValidationTypes: KrPermissionsMandatoryValidationTypesSchemeInfo;

  /**
   * Table identifier for "KrPermissionsSystem": {937fdcfd-c412-4b5d-a319-c11684ea009a}.
   */
  static get KrPermissionsSystem(): KrPermissionsSystemSchemeInfo {
    return SchemeInfo.krPermissionsSystem = SchemeInfo.krPermissionsSystem ?? new KrPermissionsSystemSchemeInfo();
  }

  private static krPermissionsSystem: KrPermissionsSystemSchemeInfo;

  /**
   * Table identifier for "KrPermissionStates": {5024c846-07bb-4932-bb8d-6c6f9c1e27f7}.
   */
  static get KrPermissionStates(): KrPermissionStatesSchemeInfo {
    return SchemeInfo.krPermissionStates = SchemeInfo.krPermissionStates ?? new KrPermissionStatesSchemeInfo();
  }

  private static krPermissionStates: KrPermissionStatesSchemeInfo;

  /**
   * Table identifier for "KrPermissionTypes": {51c5a6be-fe5d-411c-95ba-21d503ced67a}.
   */
  static get KrPermissionTypes(): KrPermissionTypesSchemeInfo {
    return SchemeInfo.krPermissionTypes = SchemeInfo.krPermissionTypes ?? new KrPermissionTypesSchemeInfo();
  }

  private static krPermissionTypes: KrPermissionTypesSchemeInfo;

  /**
   * Table identifier for "KrProcessManagementStageSettingsVirtual": {65b430e7-42f5-44c0-9d36-d31756c9941a}.
   */
  static get KrProcessManagementStageSettingsVirtual(): KrProcessManagementStageSettingsVirtualSchemeInfo {
    return SchemeInfo.krProcessManagementStageSettingsVirtual = SchemeInfo.krProcessManagementStageSettingsVirtual ?? new KrProcessManagementStageSettingsVirtualSchemeInfo();
  }

  private static krProcessManagementStageSettingsVirtual: KrProcessManagementStageSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrProcessManagementStageTypeModes": {778c5e62-6064-447e-92ac-68913d6a42cd}.
   */
  static get KrProcessManagementStageTypeModes(): KrProcessManagementStageTypeModesSchemeInfo {
    return SchemeInfo.krProcessManagementStageTypeModes = SchemeInfo.krProcessManagementStageTypeModes ?? new KrProcessManagementStageTypeModesSchemeInfo();
  }

  private static krProcessManagementStageTypeModes: KrProcessManagementStageTypeModesSchemeInfo;

  /**
   * Table identifier for "KrProcessStageTypes": {7454f645-850f-4e9b-8c80-1f129c5cb1c4}.
   */
  static get KrProcessStageTypes(): KrProcessStageTypesSchemeInfo {
    return SchemeInfo.krProcessStageTypes = SchemeInfo.krProcessStageTypes ?? new KrProcessStageTypesSchemeInfo();
  }

  private static krProcessStageTypes: KrProcessStageTypesSchemeInfo;

  /**
   * Table identifier for "KrRegistrationStageSettingsVirtual": {cae44467-e2c1-4638-8444-857575455f80}.
   */
  static get KrRegistrationStageSettingsVirtual(): KrRegistrationStageSettingsVirtualSchemeInfo {
    return SchemeInfo.krRegistrationStageSettingsVirtual = SchemeInfo.krRegistrationStageSettingsVirtual ?? new KrRegistrationStageSettingsVirtualSchemeInfo();
  }

  private static krRegistrationStageSettingsVirtual: KrRegistrationStageSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrRequestComment": {db361bb6-d8d1-4645-8d9c-f296ce939c4b}.
   */
  static get KrRequestComment(): KrRequestCommentSchemeInfo {
    return SchemeInfo.krRequestComment = SchemeInfo.krRequestComment ?? new KrRequestCommentSchemeInfo();
  }

  private static krRequestComment: KrRequestCommentSchemeInfo;

  /**
   * Table identifier for "KrResolutionActionVirtual": {aed41831-bbfd-4637-8e5b-c9b69c9ca7f1}.
   */
  static get KrResolutionActionVirtual(): KrResolutionActionVirtualSchemeInfo {
    return SchemeInfo.krResolutionActionVirtual = SchemeInfo.krResolutionActionVirtual ?? new KrResolutionActionVirtualSchemeInfo();
  }

  private static krResolutionActionVirtual: KrResolutionActionVirtualSchemeInfo;

  /**
   * Table identifier for "KrResolutionSettingsVirtual": {5e584567-9e11-4741-ab3a-d96af0b6e0c9}.
   */
  static get KrResolutionSettingsVirtual(): KrResolutionSettingsVirtualSchemeInfo {
    return SchemeInfo.krResolutionSettingsVirtual = SchemeInfo.krResolutionSettingsVirtual ?? new KrResolutionSettingsVirtualSchemeInfo();
  }

  private static krResolutionSettingsVirtual: KrResolutionSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrRouteInitializationActionVirtual": {bbdef4f8-22b0-4075-83f2-1c6e89d1ba7b}.
   */
  static get KrRouteInitializationActionVirtual(): KrRouteInitializationActionVirtualSchemeInfo {
    return SchemeInfo.krRouteInitializationActionVirtual = SchemeInfo.krRouteInitializationActionVirtual ?? new KrRouteInitializationActionVirtualSchemeInfo();
  }

  private static krRouteInitializationActionVirtual: KrRouteInitializationActionVirtualSchemeInfo;

  /**
   * Table identifier for "KrRouteModes": {01c6933a-204d-490e-a6db-fc69345c7e32}.
   */
  static get KrRouteModes(): KrRouteModesSchemeInfo {
    return SchemeInfo.krRouteModes = SchemeInfo.krRouteModes ?? new KrRouteModesSchemeInfo();
  }

  private static krRouteModes: KrRouteModesSchemeInfo;

  /**
   * Table identifier for "KrRouteSettings": {87619627-44a5-4f67-af9a-8f5736538f51}.
   */
  static get KrRouteSettings(): KrRouteSettingsSchemeInfo {
    return SchemeInfo.krRouteSettings = SchemeInfo.krRouteSettings ?? new KrRouteSettingsSchemeInfo();
  }

  private static krRouteSettings: KrRouteSettingsSchemeInfo;

  /**
   * Table identifier for "KrSamplePermissionsExtension": {f45e2e3f-5559-4800-a9f7-45276924234b}.
   */
  static get KrSamplePermissionsExtension(): KrSamplePermissionsExtensionSchemeInfo {
    return SchemeInfo.krSamplePermissionsExtension = SchemeInfo.krSamplePermissionsExtension ?? new KrSamplePermissionsExtensionSchemeInfo();
  }

  private static krSamplePermissionsExtension: KrSamplePermissionsExtensionSchemeInfo;

  /**
   * Table identifier for "KrSecondaryProcessCommonInfo": {ce71fe9f-6ae4-4f76-8311-7ae54686a474}.
   */
  static get KrSecondaryProcessCommonInfo(): KrSecondaryProcessCommonInfoSchemeInfo {
    return SchemeInfo.krSecondaryProcessCommonInfo = SchemeInfo.krSecondaryProcessCommonInfo ?? new KrSecondaryProcessCommonInfoSchemeInfo();
  }

  private static krSecondaryProcessCommonInfo: KrSecondaryProcessCommonInfoSchemeInfo;

  /**
   * Table identifier for "KrSecondaryProcesses": {caac66aa-0cbb-4e2b-83fd-7c368e814d64}.
   */
  static get KrSecondaryProcesses(): KrSecondaryProcessesSchemeInfo {
    return SchemeInfo.krSecondaryProcesses = SchemeInfo.krSecondaryProcesses ?? new KrSecondaryProcessesSchemeInfo();
  }

  private static krSecondaryProcesses: KrSecondaryProcessesSchemeInfo;

  /**
   * Table identifier for "KrSecondaryProcessGroupsVirtual": {ba745c18-badf-4d8c-a26c-46619ba56b6f}.
   */
  static get KrSecondaryProcessGroupsVirtual(): KrSecondaryProcessGroupsVirtualSchemeInfo {
    return SchemeInfo.krSecondaryProcessGroupsVirtual = SchemeInfo.krSecondaryProcessGroupsVirtual ?? new KrSecondaryProcessGroupsVirtualSchemeInfo();
  }

  private static krSecondaryProcessGroupsVirtual: KrSecondaryProcessGroupsVirtualSchemeInfo;

  /**
   * Table identifier for "KrSecondaryProcessModes": {a8a8e7df-0237-4fda-824f-030df82a1030}.
   */
  static get KrSecondaryProcessModes(): KrSecondaryProcessModesSchemeInfo {
    return SchemeInfo.krSecondaryProcessModes = SchemeInfo.krSecondaryProcessModes ?? new KrSecondaryProcessModesSchemeInfo();
  }

  private static krSecondaryProcessModes: KrSecondaryProcessModesSchemeInfo;

  /**
   * Table identifier for "KrSecondaryProcessRoles": {f7a6f1e2-a4c2-4f26-9c50-bc7e14dfc8ce}.
   */
  static get KrSecondaryProcessRoles(): KrSecondaryProcessRolesSchemeInfo {
    return SchemeInfo.krSecondaryProcessRoles = SchemeInfo.krSecondaryProcessRoles ?? new KrSecondaryProcessRolesSchemeInfo();
  }

  private static krSecondaryProcessRoles: KrSecondaryProcessRolesSchemeInfo;

  /**
   * Table identifier for "KrSettings": {4a8403cf-6979-4e21-ad09-6956d681c405}.
   */
  static get KrSettings(): KrSettingsSchemeInfo {
    return SchemeInfo.krSettings = SchemeInfo.krSettings ?? new KrSettingsSchemeInfo();
  }

  private static krSettings: KrSettingsSchemeInfo;

  /**
   * Table identifier for "KrSettingsCardTypes": {949c3849-eb4b-4d64-9676-f14f9c40dbcf}.
   */
  static get KrSettingsCardTypes(): KrSettingsCardTypesSchemeInfo {
    return SchemeInfo.krSettingsCardTypes = SchemeInfo.krSettingsCardTypes ?? new KrSettingsCardTypesSchemeInfo();
  }

  private static krSettingsCardTypes: KrSettingsCardTypesSchemeInfo;

  /**
   * Table identifier for "KrSettingsCycleGrouping": {4801bc15-cfaf-455c-aba6-cf77dd72484d}.
   */
  static get KrSettingsCycleGrouping(): KrSettingsCycleGroupingSchemeInfo {
    return SchemeInfo.krSettingsCycleGrouping = SchemeInfo.krSettingsCycleGrouping ?? new KrSettingsCycleGroupingSchemeInfo();
  }

  private static krSettingsCycleGrouping: KrSettingsCycleGroupingSchemeInfo;

  /**
   * Table identifier for "KrSettingsCycleGroupingStates": {11426c91-c7c4-4455-9eda-7ce5fd497982}.
   */
  static get KrSettingsCycleGroupingStates(): KrSettingsCycleGroupingStatesSchemeInfo {
    return SchemeInfo.krSettingsCycleGroupingStates = SchemeInfo.krSettingsCycleGroupingStates ?? new KrSettingsCycleGroupingStatesSchemeInfo();
  }

  private static krSettingsCycleGroupingStates: KrSettingsCycleGroupingStatesSchemeInfo;

  /**
   * Table identifier for "KrSettingsCycleGroupingTypes": {4012de1a-efd8-442d-a25c-8fe78008e38d}.
   */
  static get KrSettingsCycleGroupingTypes(): KrSettingsCycleGroupingTypesSchemeInfo {
    return SchemeInfo.krSettingsCycleGroupingTypes = SchemeInfo.krSettingsCycleGroupingTypes ?? new KrSettingsCycleGroupingTypesSchemeInfo();
  }

  private static krSettingsCycleGroupingTypes: KrSettingsCycleGroupingTypesSchemeInfo;

  /**
   * Table identifier for "KrSettingsRouteDocTypes": {9568db07-0f34-48ad-bab8-0d5e43d1846b}.
   */
  static get KrSettingsRouteDocTypes(): KrSettingsRouteDocTypesSchemeInfo {
    return SchemeInfo.krSettingsRouteDocTypes = SchemeInfo.krSettingsRouteDocTypes ?? new KrSettingsRouteDocTypesSchemeInfo();
  }

  private static krSettingsRouteDocTypes: KrSettingsRouteDocTypesSchemeInfo;

  /**
   * Table identifier for "KrSettingsRouteExtraTaskTypes": {219b245d-a909-4517-8be6-d22ef7a28dba}.
   */
  static get KrSettingsRouteExtraTaskTypes(): KrSettingsRouteExtraTaskTypesSchemeInfo {
    return SchemeInfo.krSettingsRouteExtraTaskTypes = SchemeInfo.krSettingsRouteExtraTaskTypes ?? new KrSettingsRouteExtraTaskTypesSchemeInfo();
  }

  private static krSettingsRouteExtraTaskTypes: KrSettingsRouteExtraTaskTypesSchemeInfo;

  /**
   * Table identifier for "KrSettingsRoutePermissions": {39e6d38f-4e35-45e9-8c71-42a932dce18c}.
   */
  static get KrSettingsRoutePermissions(): KrSettingsRoutePermissionsSchemeInfo {
    return SchemeInfo.krSettingsRoutePermissions = SchemeInfo.krSettingsRoutePermissions ?? new KrSettingsRoutePermissionsSchemeInfo();
  }

  private static krSettingsRoutePermissions: KrSettingsRoutePermissionsSchemeInfo;

  /**
   * Table identifier for "KrSettingsRouteRoles": {0f717b89-050d-4a3f-97fc-4520eed77540}.
   */
  static get KrSettingsRouteRoles(): KrSettingsRouteRolesSchemeInfo {
    return SchemeInfo.krSettingsRouteRoles = SchemeInfo.krSettingsRouteRoles ?? new KrSettingsRouteRolesSchemeInfo();
  }

  private static krSettingsRouteRoles: KrSettingsRouteRolesSchemeInfo;

  /**
   * Table identifier for "KrSettingsRouteStageGroups": {9416f8fb-95b0-4617-98a6-f576580bfd49}.
   */
  static get KrSettingsRouteStageGroups(): KrSettingsRouteStageGroupsSchemeInfo {
    return SchemeInfo.krSettingsRouteStageGroups = SchemeInfo.krSettingsRouteStageGroups ?? new KrSettingsRouteStageGroupsSchemeInfo();
  }

  private static krSettingsRouteStageGroups: KrSettingsRouteStageGroupsSchemeInfo;

  /**
   * Table identifier for "KrSettingsRouteStageTypes": {6681dd3c-cd54-405d-83bb-93ce533198fe}.
   */
  static get KrSettingsRouteStageTypes(): KrSettingsRouteStageTypesSchemeInfo {
    return SchemeInfo.krSettingsRouteStageTypes = SchemeInfo.krSettingsRouteStageTypes ?? new KrSettingsRouteStageTypesSchemeInfo();
  }

  private static krSettingsRouteStageTypes: KrSettingsRouteStageTypesSchemeInfo;

  /**
   * Table identifier for "KrSettingsTaskAuthor": {0748a117-8a2a-4198-a994-15d91094d6b7}.
   */
  static get KrSettingsTaskAuthor(): KrSettingsTaskAuthorSchemeInfo {
    return SchemeInfo.krSettingsTaskAuthor = SchemeInfo.krSettingsTaskAuthor ?? new KrSettingsTaskAuthorSchemeInfo();
  }

  private static krSettingsTaskAuthor: KrSettingsTaskAuthorSchemeInfo;

  /**
   * Table identifier for "KrSettingsTaskAuthorReplace": {cfe4678d-369f-43a4-b103-32aecd9858a6}.
   */
  static get KrSettingsTaskAuthorReplace(): KrSettingsTaskAuthorReplaceSchemeInfo {
    return SchemeInfo.krSettingsTaskAuthorReplace = SchemeInfo.krSettingsTaskAuthorReplace ?? new KrSettingsTaskAuthorReplaceSchemeInfo();
  }

  private static krSettingsTaskAuthorReplace: KrSettingsTaskAuthorReplaceSchemeInfo;

  /**
   * Table identifier for "KrSettingsTaskAuthors": {afafd0bc-446e-4adf-8332-16be0b3d1908}.
   */
  static get KrSettingsTaskAuthors(): KrSettingsTaskAuthorsSchemeInfo {
    return SchemeInfo.krSettingsTaskAuthors = SchemeInfo.krSettingsTaskAuthors ?? new KrSettingsTaskAuthorsSchemeInfo();
  }

  private static krSettingsTaskAuthors: KrSettingsTaskAuthorsSchemeInfo;

  /**
   * Table identifier for "KrSigningActionNotificationActionRolesVirtual": {a6311a94-817e-48e0-afb5-6dca269563d1}.
   */
  static get KrSigningActionNotificationActionRolesVirtual(): KrSigningActionNotificationActionRolesVirtualSchemeInfo {
    return SchemeInfo.krSigningActionNotificationActionRolesVirtual = SchemeInfo.krSigningActionNotificationActionRolesVirtual ?? new KrSigningActionNotificationActionRolesVirtualSchemeInfo();
  }

  private static krSigningActionNotificationActionRolesVirtual: KrSigningActionNotificationActionRolesVirtualSchemeInfo;

  /**
   * Table identifier for "KrSigningActionNotificationRolesVirtual": {7836e13c-4ebf-47f2-8968-504ab0d2fce4}.
   */
  static get KrSigningActionNotificationRolesVirtual(): KrSigningActionNotificationRolesVirtualSchemeInfo {
    return SchemeInfo.krSigningActionNotificationRolesVirtual = SchemeInfo.krSigningActionNotificationRolesVirtual ?? new KrSigningActionNotificationRolesVirtualSchemeInfo();
  }

  private static krSigningActionNotificationRolesVirtual: KrSigningActionNotificationRolesVirtualSchemeInfo;

  /**
   * Table identifier for "KrSigningActionOptionLinksVirtual": {dd36aad9-d17a-41ad-b854-22f886819d28}.
   */
  static get KrSigningActionOptionLinksVirtual(): KrSigningActionOptionLinksVirtualSchemeInfo {
    return SchemeInfo.krSigningActionOptionLinksVirtual = SchemeInfo.krSigningActionOptionLinksVirtual ?? new KrSigningActionOptionLinksVirtualSchemeInfo();
  }

  private static krSigningActionOptionLinksVirtual: KrSigningActionOptionLinksVirtualSchemeInfo;

  /**
   * Table identifier for "KrSigningActionOptionsActionVirtual": {b4c6c410-c5cb-40e3-b800-3cd854c94a2c}.
   */
  static get KrSigningActionOptionsActionVirtual(): KrSigningActionOptionsActionVirtualSchemeInfo {
    return SchemeInfo.krSigningActionOptionsActionVirtual = SchemeInfo.krSigningActionOptionsActionVirtual ?? new KrSigningActionOptionsActionVirtualSchemeInfo();
  }

  private static krSigningActionOptionsActionVirtual: KrSigningActionOptionsActionVirtualSchemeInfo;

  /**
   * Table identifier for "KrSigningActionOptionsVirtual": {3f87675e-0a60-4ece-a5c9-3c203e2c9ffb}.
   */
  static get KrSigningActionOptionsVirtual(): KrSigningActionOptionsVirtualSchemeInfo {
    return SchemeInfo.krSigningActionOptionsVirtual = SchemeInfo.krSigningActionOptionsVirtual ?? new KrSigningActionOptionsVirtualSchemeInfo();
  }

  private static krSigningActionOptionsVirtual: KrSigningActionOptionsVirtualSchemeInfo;

  /**
   * Table identifier for "KrSigningActionVirtual": {baaceebe-011d-4f1a-9431-00c4f8b233b9}.
   */
  static get KrSigningActionVirtual(): KrSigningActionVirtualSchemeInfo {
    return SchemeInfo.krSigningActionVirtual = SchemeInfo.krSigningActionVirtual ?? new KrSigningActionVirtualSchemeInfo();
  }

  private static krSigningActionVirtual: KrSigningActionVirtualSchemeInfo;

  /**
   * Table identifier for "KrSigningStageSettingsVirtual": {a53d9011-97c3-4890-97b8-c19c91ae8948}.
   */
  static get KrSigningStageSettingsVirtual(): KrSigningStageSettingsVirtualSchemeInfo {
    return SchemeInfo.krSigningStageSettingsVirtual = SchemeInfo.krSigningStageSettingsVirtual ?? new KrSigningStageSettingsVirtualSchemeInfo();
  }

  private static krSigningStageSettingsVirtual: KrSigningStageSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrSigningTaskOptions": {0ad2b029-2f30-4e19-96df-bc3c2dcd9dfe}.
   */
  static get KrSigningTaskOptions(): KrSigningTaskOptionsSchemeInfo {
    return SchemeInfo.krSigningTaskOptions = SchemeInfo.krSigningTaskOptions ?? new KrSigningTaskOptionsSchemeInfo();
  }

  private static krSigningTaskOptions: KrSigningTaskOptionsSchemeInfo;

  /**
   * Table identifier for "KrSinglePerformerVirtual": {52b86f8c-bc19-4dee-8e53-54236bf951a6}.
   */
  static get KrSinglePerformerVirtual(): KrSinglePerformerVirtualSchemeInfo {
    return SchemeInfo.krSinglePerformerVirtual = SchemeInfo.krSinglePerformerVirtual ?? new KrSinglePerformerVirtualSchemeInfo();
  }

  private static krSinglePerformerVirtual: KrSinglePerformerVirtualSchemeInfo;

  /**
   * Table identifier for "KrStageCommonMethods": {42a0388c-2064-4dbb-ba35-2ca8979af629}.
   */
  static get KrStageCommonMethods(): KrStageCommonMethodsSchemeInfo {
    return SchemeInfo.krStageCommonMethods = SchemeInfo.krStageCommonMethods ?? new KrStageCommonMethodsSchemeInfo();
  }

  private static krStageCommonMethods: KrStageCommonMethodsSchemeInfo;

  /**
   * Table identifier for "KrStageDocStates": {4f6c7635-031d-411a-9219-069e05a7e8b6}.
   */
  static get KrStageDocStates(): KrStageDocStatesSchemeInfo {
    return SchemeInfo.krStageDocStates = SchemeInfo.krStageDocStates ?? new KrStageDocStatesSchemeInfo();
  }

  private static krStageDocStates: KrStageDocStatesSchemeInfo;

  /**
   * Table identifier for "KrStageGroups": {fde6b6e3-f7b6-467f-96e1-e2df41a22f05}.
   */
  static get KrStageGroups(): KrStageGroupsSchemeInfo {
    return SchemeInfo.krStageGroups = SchemeInfo.krStageGroups ?? new KrStageGroupsSchemeInfo();
  }

  private static krStageGroups: KrStageGroupsSchemeInfo;

  /**
   * Table identifier for "KrStageGroupTemplatesVirtual": {f9d10aed-ae25-42e8-b936-1b97014c4e13}.
   */
  static get KrStageGroupTemplatesVirtual(): KrStageGroupTemplatesVirtualSchemeInfo {
    return SchemeInfo.krStageGroupTemplatesVirtual = SchemeInfo.krStageGroupTemplatesVirtual ?? new KrStageGroupTemplatesVirtualSchemeInfo();
  }

  private static krStageGroupTemplatesVirtual: KrStageGroupTemplatesVirtualSchemeInfo;

  /**
   * Table identifier for "KrStageRoles": {97805de9-ed94-41a3-bf8b-fc1fa17c7d30}.
   */
  static get KrStageRoles(): KrStageRolesSchemeInfo {
    return SchemeInfo.krStageRoles = SchemeInfo.krStageRoles ?? new KrStageRolesSchemeInfo();
  }

  private static krStageRoles: KrStageRolesSchemeInfo;

  /**
   * Table identifier for "KrStages": {92caadca-2409-40ff-b7d8-1d4fd302b1e9}.
   */
  static get KrStages(): KrStagesSchemeInfo {
    return SchemeInfo.krStages = SchemeInfo.krStages ?? new KrStagesSchemeInfo();
  }

  private static krStages: KrStagesSchemeInfo;

  /**
   * Table identifier for "KrStageState": {beee4f3d-a385-4fc8-884f-bc1ccf55fc5b}.
   */
  static get KrStageState(): KrStageStateSchemeInfo {
    return SchemeInfo.krStageState = SchemeInfo.krStageState ?? new KrStageStateSchemeInfo();
  }

  private static krStageState: KrStageStateSchemeInfo;

  /**
   * Table identifier for "KrStagesVirtual": {89d78d5c-f8dd-48e7-868c-88bbafe74257}.
   */
  static get KrStagesVirtual(): KrStagesVirtualSchemeInfo {
    return SchemeInfo.krStagesVirtual = SchemeInfo.krStagesVirtual ?? new KrStagesVirtualSchemeInfo();
  }

  private static krStagesVirtual: KrStagesVirtualSchemeInfo;

  /**
   * Table identifier for "KrStageTemplateGroupPosition": {496c30f2-79d0-408a-8085-95b43d67a22b}.
   */
  static get KrStageTemplateGroupPosition(): KrStageTemplateGroupPositionSchemeInfo {
    return SchemeInfo.krStageTemplateGroupPosition = SchemeInfo.krStageTemplateGroupPosition ?? new KrStageTemplateGroupPositionSchemeInfo();
  }

  private static krStageTemplateGroupPosition: KrStageTemplateGroupPositionSchemeInfo;

  /**
   * Table identifier for "KrStageTemplates": {5a33ac72-f6f5-4e5a-8d8c-4a94ed7bf324}.
   */
  static get KrStageTemplates(): KrStageTemplatesSchemeInfo {
    return SchemeInfo.krStageTemplates = SchemeInfo.krStageTemplates ?? new KrStageTemplatesSchemeInfo();
  }

  private static krStageTemplates: KrStageTemplatesSchemeInfo;

  /**
   * Table identifier for "KrStageTypes": {971d8661-d445-42fb-84d0-b0b71aa978a2}.
   */
  static get KrStageTypes(): KrStageTypesSchemeInfo {
    return SchemeInfo.krStageTypes = SchemeInfo.krStageTypes ?? new KrStageTypesSchemeInfo();
  }

  private static krStageTypes: KrStageTypesSchemeInfo;

  /**
   * Table identifier for "KrTask": {51936147-e0ff-4e19-a7d1-0ea7d462ceec}.
   */
  static get KrTask(): KrTaskSchemeInfo {
    return SchemeInfo.krTask = SchemeInfo.krTask ?? new KrTaskSchemeInfo();
  }

  private static krTask: KrTaskSchemeInfo;

  /**
   * Table identifier for "KrTaskCommentVirtual": {344fa4e8-cdfc-4cb0-8634-9155c49fd21a}.
   */
  static get KrTaskCommentVirtual(): KrTaskCommentVirtualSchemeInfo {
    return SchemeInfo.krTaskCommentVirtual = SchemeInfo.krTaskCommentVirtual ?? new KrTaskCommentVirtualSchemeInfo();
  }

  private static krTaskCommentVirtual: KrTaskCommentVirtualSchemeInfo;

  /**
   * Table identifier for "KrTaskKindSettingsVirtual": {80ab607a-d43f-435d-a1be-a203bb99c2d3}.
   */
  static get KrTaskKindSettingsVirtual(): KrTaskKindSettingsVirtualSchemeInfo {
    return SchemeInfo.krTaskKindSettingsVirtual = SchemeInfo.krTaskKindSettingsVirtual ?? new KrTaskKindSettingsVirtualSchemeInfo();
  }

  private static krTaskKindSettingsVirtual: KrTaskKindSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrTaskRegistrationActionNotificationRolesVitrual": {406b3337-8cfc-437d-a7fc-408b96a92c00}.
   */
  static get KrTaskRegistrationActionNotificationRolesVitrual(): KrTaskRegistrationActionNotificationRolesVitrualSchemeInfo {
    return SchemeInfo.krTaskRegistrationActionNotificationRolesVitrual = SchemeInfo.krTaskRegistrationActionNotificationRolesVitrual ?? new KrTaskRegistrationActionNotificationRolesVitrualSchemeInfo();
  }

  private static krTaskRegistrationActionNotificationRolesVitrual: KrTaskRegistrationActionNotificationRolesVitrualSchemeInfo;

  /**
   * Table identifier for "KrTaskRegistrationActionOptionLinksVirtual": {f6a8f11a-68c2-4743-a8ed-236fe459dfc9}.
   */
  static get KrTaskRegistrationActionOptionLinksVirtual(): KrTaskRegistrationActionOptionLinksVirtualSchemeInfo {
    return SchemeInfo.krTaskRegistrationActionOptionLinksVirtual = SchemeInfo.krTaskRegistrationActionOptionLinksVirtual ?? new KrTaskRegistrationActionOptionLinksVirtualSchemeInfo();
  }

  private static krTaskRegistrationActionOptionLinksVirtual: KrTaskRegistrationActionOptionLinksVirtualSchemeInfo;

  /**
   * Table identifier for "KrTaskRegistrationActionOptionsVirtual": {2ba2b1a3-b8ad-4c47-a8fd-3a3fa421c7a9}.
   */
  static get KrTaskRegistrationActionOptionsVirtual(): KrTaskRegistrationActionOptionsVirtualSchemeInfo {
    return SchemeInfo.krTaskRegistrationActionOptionsVirtual = SchemeInfo.krTaskRegistrationActionOptionsVirtual ?? new KrTaskRegistrationActionOptionsVirtualSchemeInfo();
  }

  private static krTaskRegistrationActionOptionsVirtual: KrTaskRegistrationActionOptionsVirtualSchemeInfo;

  /**
   * Table identifier for "KrTaskRegistrationActionVirtual": {12b90f64-b971-4198-ad0e-0e3d1988f946}.
   */
  static get KrTaskRegistrationActionVirtual(): KrTaskRegistrationActionVirtualSchemeInfo {
    return SchemeInfo.krTaskRegistrationActionVirtual = SchemeInfo.krTaskRegistrationActionVirtual ?? new KrTaskRegistrationActionVirtualSchemeInfo();
  }

  private static krTaskRegistrationActionVirtual: KrTaskRegistrationActionVirtualSchemeInfo;

  /**
   * Table identifier for "KrTaskTypeCondition": {0209ddc9-6457-406e-8a09-ab8ac6916e26}.
   */
  static get KrTaskTypeCondition(): KrTaskTypeConditionSchemeInfo {
    return SchemeInfo.krTaskTypeCondition = SchemeInfo.krTaskTypeCondition ?? new KrTaskTypeConditionSchemeInfo();
  }

  private static krTaskTypeCondition: KrTaskTypeConditionSchemeInfo;

  /**
   * Table identifier for "KrTaskTypeConditionSettings": {4b9735f4-7db4-46e1-bbdd-4bd71f5234bd}.
   */
  static get KrTaskTypeConditionSettings(): KrTaskTypeConditionSettingsSchemeInfo {
    return SchemeInfo.krTaskTypeConditionSettings = SchemeInfo.krTaskTypeConditionSettings ?? new KrTaskTypeConditionSettingsSchemeInfo();
  }

  private static krTaskTypeConditionSettings: KrTaskTypeConditionSettingsSchemeInfo;

  /**
   * Table identifier for "KrTypedTaskSettingsVirtual": {e06fa88f-35a2-48fc-8ce4-6e20521b5238}.
   */
  static get KrTypedTaskSettingsVirtual(): KrTypedTaskSettingsVirtualSchemeInfo {
    return SchemeInfo.krTypedTaskSettingsVirtual = SchemeInfo.krTypedTaskSettingsVirtual ?? new KrTypedTaskSettingsVirtualSchemeInfo();
  }

  private static krTypedTaskSettingsVirtual: KrTypedTaskSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrUniversalTaskActionButtonLinksVirtual": {0938b2e9-485e-4f87-8622-706bbaf0efb7}.
   */
  static get KrUniversalTaskActionButtonLinksVirtual(): KrUniversalTaskActionButtonLinksVirtualSchemeInfo {
    return SchemeInfo.krUniversalTaskActionButtonLinksVirtual = SchemeInfo.krUniversalTaskActionButtonLinksVirtual ?? new KrUniversalTaskActionButtonLinksVirtualSchemeInfo();
  }

  private static krUniversalTaskActionButtonLinksVirtual: KrUniversalTaskActionButtonLinksVirtualSchemeInfo;

  /**
   * Table identifier for "KrUniversalTaskActionButtonsVirtual": {e85631c4-0014-4842-86f4-9a6ba66166f3}.
   */
  static get KrUniversalTaskActionButtonsVirtual(): KrUniversalTaskActionButtonsVirtualSchemeInfo {
    return SchemeInfo.krUniversalTaskActionButtonsVirtual = SchemeInfo.krUniversalTaskActionButtonsVirtual ?? new KrUniversalTaskActionButtonsVirtualSchemeInfo();
  }

  private static krUniversalTaskActionButtonsVirtual: KrUniversalTaskActionButtonsVirtualSchemeInfo;

  /**
   * Table identifier for "KrUniversalTaskActionNotificationRolesVitrual": {1d7d2be8-692e-478d-9ce4-fc791833ffba}.
   */
  static get KrUniversalTaskActionNotificationRolesVitrual(): KrUniversalTaskActionNotificationRolesVitrualSchemeInfo {
    return SchemeInfo.krUniversalTaskActionNotificationRolesVitrual = SchemeInfo.krUniversalTaskActionNotificationRolesVitrual ?? new KrUniversalTaskActionNotificationRolesVitrualSchemeInfo();
  }

  private static krUniversalTaskActionNotificationRolesVitrual: KrUniversalTaskActionNotificationRolesVitrualSchemeInfo;

  /**
   * Table identifier for "KrUniversalTaskActionVirtual": {b0ca69b1-7c90-4ce7-995c-2f9540ec45ef}.
   */
  static get KrUniversalTaskActionVirtual(): KrUniversalTaskActionVirtualSchemeInfo {
    return SchemeInfo.krUniversalTaskActionVirtual = SchemeInfo.krUniversalTaskActionVirtual ?? new KrUniversalTaskActionVirtualSchemeInfo();
  }

  private static krUniversalTaskActionVirtual: KrUniversalTaskActionVirtualSchemeInfo;

  /**
   * Table identifier for "KrUniversalTaskOptions": {470ddc9e-4715-4efa-bd25-cbb9f4033162}.
   */
  static get KrUniversalTaskOptions(): KrUniversalTaskOptionsSchemeInfo {
    return SchemeInfo.krUniversalTaskOptions = SchemeInfo.krUniversalTaskOptions ?? new KrUniversalTaskOptionsSchemeInfo();
  }

  private static krUniversalTaskOptions: KrUniversalTaskOptionsSchemeInfo;

  /**
   * Table identifier for "KrUniversalTaskOptionsSettingsVirtual": {49f11daf-636a-4342-aa2b-ea798bed7263}.
   */
  static get KrUniversalTaskOptionsSettingsVirtual(): KrUniversalTaskOptionsSettingsVirtualSchemeInfo {
    return SchemeInfo.krUniversalTaskOptionsSettingsVirtual = SchemeInfo.krUniversalTaskOptionsSettingsVirtual ?? new KrUniversalTaskOptionsSettingsVirtualSchemeInfo();
  }

  private static krUniversalTaskOptionsSettingsVirtual: KrUniversalTaskOptionsSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrUniversalTaskSettingsVirtual": {01db90f2-22ec-4233-a5fd-587a832b1b48}.
   */
  static get KrUniversalTaskSettingsVirtual(): KrUniversalTaskSettingsVirtualSchemeInfo {
    return SchemeInfo.krUniversalTaskSettingsVirtual = SchemeInfo.krUniversalTaskSettingsVirtual ?? new KrUniversalTaskSettingsVirtualSchemeInfo();
  }

  private static krUniversalTaskSettingsVirtual: KrUniversalTaskSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrUsersCondition": {c9cf90e4-72e2-4cd2-a798-62b1d856cea5}.
   */
  static get KrUsersCondition(): KrUsersConditionSchemeInfo {
    return SchemeInfo.krUsersCondition = SchemeInfo.krUsersCondition ?? new KrUsersConditionSchemeInfo();
  }

  private static krUsersCondition: KrUsersConditionSchemeInfo;

  /**
   * Table identifier for "KrUserSettingsVirtual": {84d45612-8cbb-4b7d-91eb-2796003a365d}.
   */
  static get KrUserSettingsVirtual(): KrUserSettingsVirtualSchemeInfo {
    return SchemeInfo.krUserSettingsVirtual = SchemeInfo.krUserSettingsVirtual ?? new KrUserSettingsVirtualSchemeInfo();
  }

  private static krUserSettingsVirtual: KrUserSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "KrVirtualFileCardTypes": {5ad723d6-21d8-48c2-8799-a1ba9fb1c758}.
   */
  static get KrVirtualFileCardTypes(): KrVirtualFileCardTypesSchemeInfo {
    return SchemeInfo.krVirtualFileCardTypes = SchemeInfo.krVirtualFileCardTypes ?? new KrVirtualFileCardTypesSchemeInfo();
  }

  private static krVirtualFileCardTypes: KrVirtualFileCardTypesSchemeInfo;

  /**
   * Table identifier for "KrVirtualFileRoles": {9d6186ba-f7ae-4910-8784-6c63a3b13179}.
   */
  static get KrVirtualFileRoles(): KrVirtualFileRolesSchemeInfo {
    return SchemeInfo.krVirtualFileRoles = SchemeInfo.krVirtualFileRoles ?? new KrVirtualFileRolesSchemeInfo();
  }

  private static krVirtualFileRoles: KrVirtualFileRolesSchemeInfo;

  /**
   * Table identifier for "KrVirtualFiles": {006f8d09-5eff-46fc-8bc5-d7c5f6a81d44}.
   */
  static get KrVirtualFiles(): KrVirtualFilesSchemeInfo {
    return SchemeInfo.krVirtualFiles = SchemeInfo.krVirtualFiles ?? new KrVirtualFilesSchemeInfo();
  }

  private static krVirtualFiles: KrVirtualFilesSchemeInfo;

  /**
   * Table identifier for "KrVirtualFileStates": {a6386cf3-fff8-401e-b8d2-222d0221951f}.
   */
  static get KrVirtualFileStates(): KrVirtualFileStatesSchemeInfo {
    return SchemeInfo.krVirtualFileStates = SchemeInfo.krVirtualFileStates ?? new KrVirtualFileStatesSchemeInfo();
  }

  private static krVirtualFileStates: KrVirtualFileStatesSchemeInfo;

  /**
   * Table identifier for "KrVirtualFileVersions": {8bd27c6a-16e0-4ac4-a147-8cc2d30dc88b}.
   */
  static get KrVirtualFileVersions(): KrVirtualFileVersionsSchemeInfo {
    return SchemeInfo.krVirtualFileVersions = SchemeInfo.krVirtualFileVersions ?? new KrVirtualFileVersionsSchemeInfo();
  }

  private static krVirtualFileVersions: KrVirtualFileVersionsSchemeInfo;

  /**
   * Table identifier for "KrWeActionCompletionOptions": {6a24d3cd-ec83-4e7a-8815-77b054c69371}.
   */
  static get KrWeActionCompletionOptions(): KrWeActionCompletionOptionsSchemeInfo {
    return SchemeInfo.krWeActionCompletionOptions = SchemeInfo.krWeActionCompletionOptions ?? new KrWeActionCompletionOptionsSchemeInfo();
  }

  private static krWeActionCompletionOptions: KrWeActionCompletionOptionsSchemeInfo;

  /**
   * Table identifier for "KrWeAdditionalApprovalOptionsVirtual": {54829879-8b8e-4d47-a27b-0346e93e6e45}.
   */
  static get KrWeAdditionalApprovalOptionsVirtual(): KrWeAdditionalApprovalOptionsVirtualSchemeInfo {
    return SchemeInfo.krWeAdditionalApprovalOptionsVirtual = SchemeInfo.krWeAdditionalApprovalOptionsVirtual ?? new KrWeAdditionalApprovalOptionsVirtualSchemeInfo();
  }

  private static krWeAdditionalApprovalOptionsVirtual: KrWeAdditionalApprovalOptionsVirtualSchemeInfo;

  /**
   * Table identifier for "KrWeEditInterjectOptionsVirtual": {d0771103-6c21-4602-af56-d264e82f8b57}.
   */
  static get KrWeEditInterjectOptionsVirtual(): KrWeEditInterjectOptionsVirtualSchemeInfo {
    return SchemeInfo.krWeEditInterjectOptionsVirtual = SchemeInfo.krWeEditInterjectOptionsVirtual ?? new KrWeEditInterjectOptionsVirtualSchemeInfo();
  }

  private static krWeEditInterjectOptionsVirtual: KrWeEditInterjectOptionsVirtualSchemeInfo;

  /**
   * Table identifier for "KrWeRequestCommentOptionsVirtual": {abc045c0-75b1-4a25-8d9e-d6b323118f08}.
   */
  static get KrWeRequestCommentOptionsVirtual(): KrWeRequestCommentOptionsVirtualSchemeInfo {
    return SchemeInfo.krWeRequestCommentOptionsVirtual = SchemeInfo.krWeRequestCommentOptionsVirtual ?? new KrWeRequestCommentOptionsVirtualSchemeInfo();
  }

  private static krWeRequestCommentOptionsVirtual: KrWeRequestCommentOptionsVirtualSchemeInfo;

  /**
   * Table identifier for "KrWeRolesVirtual": {eea339fd-2e18-415b-b338-418f84ac961e}.
   */
  static get KrWeRolesVirtual(): KrWeRolesVirtualSchemeInfo {
    return SchemeInfo.krWeRolesVirtual = SchemeInfo.krWeRolesVirtual ?? new KrWeRolesVirtualSchemeInfo();
  }

  private static krWeRolesVirtual: KrWeRolesVirtualSchemeInfo;

  /**
   * Table identifier for "Languages": {1ed36bf1-2ebf-43da-acb2-1ddb3298dbd8}.
   */
  static get Languages(): LanguagesSchemeInfo {
    return SchemeInfo.languages = SchemeInfo.languages ?? new LanguagesSchemeInfo();
  }

  private static languages: LanguagesSchemeInfo;

  /**
   * Table identifier for "LawAdministrators": {3dbb9a1f-ae27-4612-aec1-4f077494dfef}.
   */
  static get LawAdministrators(): LawAdministratorsSchemeInfo {
    return SchemeInfo.lawAdministrators = SchemeInfo.lawAdministrators ?? new LawAdministratorsSchemeInfo();
  }

  private static lawAdministrators: LawAdministratorsSchemeInfo;

  /**
   * Table identifier for "LawCase": {191308f6-820e-408e-b779-cef96b1b09c0}.
   */
  static get LawCase(): LawCaseSchemeInfo {
    return SchemeInfo.lawCase = SchemeInfo.lawCase ?? new LawCaseSchemeInfo();
  }

  private static lawCase: LawCaseSchemeInfo;

  /**
   * Table identifier for "LawClients": {362c9171-6267-42a8-8fd8-7bf39d04533e}.
   */
  static get LawClients(): LawClientsSchemeInfo {
    return SchemeInfo.lawClients = SchemeInfo.lawClients ?? new LawClientsSchemeInfo();
  }

  private static lawClients: LawClientsSchemeInfo;

  /**
   * Table identifier for "LawFolders": {48b7ff15-cb0d-4acb-8b11-4ea833ea6f9b}.
   */
  static get LawFolders(): LawFoldersSchemeInfo {
    return SchemeInfo.lawFolders = SchemeInfo.lawFolders ?? new LawFoldersSchemeInfo();
  }

  private static lawFolders: LawFoldersSchemeInfo;

  /**
   * Table identifier for "LawPartnerRepresentatives": {b4cfec48-deec-40fc-94fc-eae9e645afce}.
   */
  static get LawPartnerRepresentatives(): LawPartnerRepresentativesSchemeInfo {
    return SchemeInfo.lawPartnerRepresentatives = SchemeInfo.lawPartnerRepresentatives ?? new LawPartnerRepresentativesSchemeInfo();
  }

  private static lawPartnerRepresentatives: LawPartnerRepresentativesSchemeInfo;

  /**
   * Table identifier for "LawPartners": {54244411-fea4-4bdd-b009-fad9c5915882}.
   */
  static get LawPartners(): LawPartnersSchemeInfo {
    return SchemeInfo.lawPartners = SchemeInfo.lawPartners ?? new LawPartnersSchemeInfo();
  }

  private static lawPartners: LawPartnersSchemeInfo;

  /**
   * Table identifier for "LawPartnersDialogVirtual": {c0b9d91c-4a9e-4c7a-b7c2-13daa37a1cf9}.
   */
  static get LawPartnersDialogVirtual(): LawPartnersDialogVirtualSchemeInfo {
    return SchemeInfo.lawPartnersDialogVirtual = SchemeInfo.lawPartnersDialogVirtual ?? new LawPartnersDialogVirtualSchemeInfo();
  }

  private static lawPartnersDialogVirtual: LawPartnersDialogVirtualSchemeInfo;

  /**
   * Table identifier for "LawUsers": {0b3ce213-cc34-469d-a0dd-19a4643b1a49}.
   */
  static get LawUsers(): LawUsersSchemeInfo {
    return SchemeInfo.lawUsers = SchemeInfo.lawUsers ?? new LawUsersSchemeInfo();
  }

  private static lawUsers: LawUsersSchemeInfo;

  /**
   * Table identifier for "LicenseTypes": {bcc286d4-9d77-4750-8084-15417b966528}.
   */
  static get LicenseTypes(): LicenseTypesSchemeInfo {
    return SchemeInfo.licenseTypes = SchemeInfo.licenseTypes ?? new LicenseTypesSchemeInfo();
  }

  private static licenseTypes: LicenseTypesSchemeInfo;

  /**
   * Table identifier for "LicenseVirtual": {f81a7db5-a883-49e0-918c-59a5967828b5}.
   */
  static get LicenseVirtual(): LicenseVirtualSchemeInfo {
    return SchemeInfo.licenseVirtual = SchemeInfo.licenseVirtual ?? new LicenseVirtualSchemeInfo();
  }

  private static licenseVirtual: LicenseVirtualSchemeInfo;

  /**
   * Table identifier for "LocalizationEntries": {b92e97c0-4557-4d43-874a-e9a75173cbf8}.
   */
  static get LocalizationEntries(): LocalizationEntriesSchemeInfo {
    return SchemeInfo.localizationEntries = SchemeInfo.localizationEntries ?? new LocalizationEntriesSchemeInfo();
  }

  private static localizationEntries: LocalizationEntriesSchemeInfo;

  /**
   * Table identifier for "LocalizationLibraries": {3e31b54e-1a4c-4e9c-bcf5-26e4780d6419}.
   */
  static get LocalizationLibraries(): LocalizationLibrariesSchemeInfo {
    return SchemeInfo.localizationLibraries = SchemeInfo.localizationLibraries ?? new LocalizationLibrariesSchemeInfo();
  }

  private static localizationLibraries: LocalizationLibrariesSchemeInfo;

  /**
   * Table identifier for "LocalizationStrings": {3e0ef7dd-7303-41e8-9ddc-af0a30e0de84}.
   */
  static get LocalizationStrings(): LocalizationStringsSchemeInfo {
    return SchemeInfo.localizationStrings = SchemeInfo.localizationStrings ?? new LocalizationStringsSchemeInfo();
  }

  private static localizationStrings: LocalizationStringsSchemeInfo;

  /**
   * Table identifier for "LoginTypes": {44a94501-a954-4ab1-a7f8-47eebb2f869b}.
   */
  static get LoginTypes(): LoginTypesSchemeInfo {
    return SchemeInfo.loginTypes = SchemeInfo.loginTypes ?? new LoginTypesSchemeInfo();
  }

  private static loginTypes: LoginTypesSchemeInfo;

  /**
   * Table identifier for "MetaRoles": {984ac49b-30d7-48da-ab6a-3fe4bcf7513d}.
   */
  static get MetaRoles(): MetaRolesSchemeInfo {
    return SchemeInfo.metaRoles = SchemeInfo.metaRoles ?? new MetaRolesSchemeInfo();
  }

  private static metaRoles: MetaRolesSchemeInfo;

  /**
   * Table identifier for "MetaRoleTypes": {53a3ee37-b714-4503-9e0e-e2ed1ccd164f}.
   */
  static get MetaRoleTypes(): MetaRoleTypesSchemeInfo {
    return SchemeInfo.metaRoleTypes = SchemeInfo.metaRoleTypes ?? new MetaRoleTypesSchemeInfo();
  }

  private static metaRoleTypes: MetaRoleTypesSchemeInfo;

  /**
   * Table identifier for "Migrations": {fd65afe6-d4bf-4885-872a-3824e64b1c63}.
   */
  static get Migrations(): MigrationsSchemeInfo {
    return SchemeInfo.migrations = SchemeInfo.migrations ?? new MigrationsSchemeInfo();
  }

  private static migrations: MigrationsSchemeInfo;

  /**
   * Table identifier for "MobileLicenses": {457f5393-50a4-40ea-8637-37fb57330ae2}.
   */
  static get MobileLicenses(): MobileLicensesSchemeInfo {
    return SchemeInfo.mobileLicenses = SchemeInfo.mobileLicenses ?? new MobileLicensesSchemeInfo();
  }

  private static mobileLicenses: MobileLicensesSchemeInfo;

  /**
   * Table identifier for "NestedRoles": {312b1519-8079-44d7-a5b4-496db41da98c}.
   */
  static get NestedRoles(): NestedRolesSchemeInfo {
    return SchemeInfo.nestedRoles = SchemeInfo.nestedRoles ?? new NestedRolesSchemeInfo();
  }

  private static nestedRoles: NestedRolesSchemeInfo;

  /**
   * Table identifier for "Notifications": {18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a}.
   */
  static get Notifications(): NotificationsSchemeInfo {
    return SchemeInfo.notifications = SchemeInfo.notifications ?? new NotificationsSchemeInfo();
  }

  private static notifications: NotificationsSchemeInfo;

  /**
   * Table identifier for "NotificationSubscribeTypes": {287bfcf3-aa96-44ee-96a8-68fbc1f2d3ab}.
   */
  static get NotificationSubscribeTypes(): NotificationSubscribeTypesSchemeInfo {
    return SchemeInfo.notificationSubscribeTypes = SchemeInfo.notificationSubscribeTypes ?? new NotificationSubscribeTypesSchemeInfo();
  }

  private static notificationSubscribeTypes: NotificationSubscribeTypesSchemeInfo;

  /**
   * Table identifier for "NotificationSubscriptions": {d5b074e2-eaff-4993-b238-1d5d3d248d56}.
   */
  static get NotificationSubscriptions(): NotificationSubscriptionsSchemeInfo {
    return SchemeInfo.notificationSubscriptions = SchemeInfo.notificationSubscriptions ?? new NotificationSubscriptionsSchemeInfo();
  }

  private static notificationSubscriptions: NotificationSubscriptionsSchemeInfo;

  /**
   * Table identifier for "NotificationSubscriptionSettings": {9ffce865-a6cd-4883-9ed0-0cbeaa1831d1}.
   */
  static get NotificationSubscriptionSettings(): NotificationSubscriptionSettingsSchemeInfo {
    return SchemeInfo.notificationSubscriptionSettings = SchemeInfo.notificationSubscriptionSettings ?? new NotificationSubscriptionSettingsSchemeInfo();
  }

  private static notificationSubscriptionSettings: NotificationSubscriptionSettingsSchemeInfo;

  /**
   * Table identifier for "NotificationTokenRoles": {a88ed4f8-dcce-400a-931f-99defee9949c}.
   */
  static get NotificationTokenRoles(): NotificationTokenRolesSchemeInfo {
    return SchemeInfo.notificationTokenRoles = SchemeInfo.notificationTokenRoles ?? new NotificationTokenRolesSchemeInfo();
  }

  private static notificationTokenRoles: NotificationTokenRolesSchemeInfo;

  /**
   * Table identifier for "NotificationTypeCardTypes": {b54be13b-72be-4b20-b090-b861efaf8585}.
   */
  static get NotificationTypeCardTypes(): NotificationTypeCardTypesSchemeInfo {
    return SchemeInfo.notificationTypeCardTypes = SchemeInfo.notificationTypeCardTypes ?? new NotificationTypeCardTypesSchemeInfo();
  }

  private static notificationTypeCardTypes: NotificationTypeCardTypesSchemeInfo;

  /**
   * Table identifier for "NotificationTypes": {bae37ba2-7a39-49a1-8cc8-64f032ba3f79}.
   */
  static get NotificationTypes(): NotificationTypesSchemeInfo {
    return SchemeInfo.notificationTypes = SchemeInfo.notificationTypes ?? new NotificationTypesSchemeInfo();
  }

  private static notificationTypes: NotificationTypesSchemeInfo;

  /**
   * Table identifier for "NotificationUnsubscribeTypes": {d845a7f8-9873-47c1-a160-370f66dc852e}.
   */
  static get NotificationUnsubscribeTypes(): NotificationUnsubscribeTypesSchemeInfo {
    return SchemeInfo.notificationUnsubscribeTypes = SchemeInfo.notificationUnsubscribeTypes ?? new NotificationUnsubscribeTypesSchemeInfo();
  }

  private static notificationUnsubscribeTypes: NotificationUnsubscribeTypesSchemeInfo;

  /**
   * Table identifier for "OcrLanguages": {a5b1b1cf-ad8c-4459-a880-a5ff9f435398}.
   */
  static get OcrLanguages(): OcrLanguagesSchemeInfo {
    return SchemeInfo.ocrLanguages = SchemeInfo.ocrLanguages ?? new OcrLanguagesSchemeInfo();
  }

  private static ocrLanguages: OcrLanguagesSchemeInfo;

  /**
   * Table identifier for "OcrMappingComplexFields": {e8135496-a897-44b9-bc24-9214646453fe}.
   */
  static get OcrMappingComplexFields(): OcrMappingComplexFieldsSchemeInfo {
    return SchemeInfo.ocrMappingComplexFields = SchemeInfo.ocrMappingComplexFields ?? new OcrMappingComplexFieldsSchemeInfo();
  }

  private static ocrMappingComplexFields: OcrMappingComplexFieldsSchemeInfo;

  /**
   * Table identifier for "OcrMappingFields": {f60796c1-b504-424d-93c8-2fd5b9107867}.
   */
  static get OcrMappingFields(): OcrMappingFieldsSchemeInfo {
    return SchemeInfo.ocrMappingFields = SchemeInfo.ocrMappingFields ?? new OcrMappingFieldsSchemeInfo();
  }

  private static ocrMappingFields: OcrMappingFieldsSchemeInfo;

  /**
   * Table identifier for "OcrMappingSettingsFields": {46a9cc0e-9492-48ce-8ac8-bc04551a041a}.
   */
  static get OcrMappingSettingsFields(): OcrMappingSettingsFieldsSchemeInfo {
    return SchemeInfo.ocrMappingSettingsFields = SchemeInfo.ocrMappingSettingsFields ?? new OcrMappingSettingsFieldsSchemeInfo();
  }

  private static ocrMappingSettingsFields: OcrMappingSettingsFieldsSchemeInfo;

  /**
   * Table identifier for "OcrMappingSettingsSections": {3a7dbf8d-2f25-4b98-a406-2582bfeee594}.
   */
  static get OcrMappingSettingsSections(): OcrMappingSettingsSectionsSchemeInfo {
    return SchemeInfo.ocrMappingSettingsSections = SchemeInfo.ocrMappingSettingsSections ?? new OcrMappingSettingsSectionsSchemeInfo();
  }

  private static ocrMappingSettingsSections: OcrMappingSettingsSectionsSchemeInfo;

  /**
   * Table identifier for "OcrMappingSettingsTypes": {6f34e6c5-93e9-49ed-ad42-63d8a001ded7}.
   */
  static get OcrMappingSettingsTypes(): OcrMappingSettingsTypesSchemeInfo {
    return SchemeInfo.ocrMappingSettingsTypes = SchemeInfo.ocrMappingSettingsTypes ?? new OcrMappingSettingsTypesSchemeInfo();
  }

  private static ocrMappingSettingsTypes: OcrMappingSettingsTypesSchemeInfo;

  /**
   * Table identifier for "OcrMappingSettingsVirtual": {0c83bfa8-8e9a-454d-b805-6de0c679f4ec}.
   */
  static get OcrMappingSettingsVirtual(): OcrMappingSettingsVirtualSchemeInfo {
    return SchemeInfo.ocrMappingSettingsVirtual = SchemeInfo.ocrMappingSettingsVirtual ?? new OcrMappingSettingsVirtualSchemeInfo();
  }

  private static ocrMappingSettingsVirtual: OcrMappingSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "OcrOperations": {2fb28ad4-1b86-4d22-bc15-2a4943a0bb7f}.
   */
  static get OcrOperations(): OcrOperationsSchemeInfo {
    return SchemeInfo.ocrOperations = SchemeInfo.ocrOperations ?? new OcrOperationsSchemeInfo();
  }

  private static ocrOperations: OcrOperationsSchemeInfo;

  /**
   * Table identifier for "OcrPatternTypes": {ff8635f6-7856-45e1-82cf-6f00315ce790}.
   */
  static get OcrPatternTypes(): OcrPatternTypesSchemeInfo {
    return SchemeInfo.ocrPatternTypes = SchemeInfo.ocrPatternTypes ?? new OcrPatternTypesSchemeInfo();
  }

  private static ocrPatternTypes: OcrPatternTypesSchemeInfo;

  /**
   * Table identifier for "OcrRecognitionModes": {b9a84cff-8153-4a81-b9f3-832d59461596}.
   */
  static get OcrRecognitionModes(): OcrRecognitionModesSchemeInfo {
    return SchemeInfo.ocrRecognitionModes = SchemeInfo.ocrRecognitionModes ?? new OcrRecognitionModesSchemeInfo();
  }

  private static ocrRecognitionModes: OcrRecognitionModesSchemeInfo;

  /**
   * Table identifier for "OcrRequests": {d64806e9-ef31-4133-806b-670b178cc5bc}.
   */
  static get OcrRequests(): OcrRequestsSchemeInfo {
    return SchemeInfo.ocrRequests = SchemeInfo.ocrRequests ?? new OcrRequestsSchemeInfo();
  }

  private static ocrRequests: OcrRequestsSchemeInfo;

  /**
   * Table identifier for "OcrRequestsLanguages": {afee6930-bb0c-48b3-b0ac-3a1447a31d12}.
   */
  static get OcrRequestsLanguages(): OcrRequestsLanguagesSchemeInfo {
    return SchemeInfo.ocrRequestsLanguages = SchemeInfo.ocrRequestsLanguages ?? new OcrRequestsLanguagesSchemeInfo();
  }

  private static ocrRequestsLanguages: OcrRequestsLanguagesSchemeInfo;

  /**
   * Table identifier for "OcrRequestsStates": {aefacd5d-1de1-42ac-86db-ddb98035f498}.
   */
  static get OcrRequestsStates(): OcrRequestsStatesSchemeInfo {
    return SchemeInfo.ocrRequestsStates = SchemeInfo.ocrRequestsStates ?? new OcrRequestsStatesSchemeInfo();
  }

  private static ocrRequestsStates: OcrRequestsStatesSchemeInfo;

  /**
   * Table identifier for "OcrResultsVirtual": {f704ecbb-e3b2-4a79-8ca8-222417f3dc4d}.
   */
  static get OcrResultsVirtual(): OcrResultsVirtualSchemeInfo {
    return SchemeInfo.ocrResultsVirtual = SchemeInfo.ocrResultsVirtual ?? new OcrResultsVirtualSchemeInfo();
  }

  private static ocrResultsVirtual: OcrResultsVirtualSchemeInfo;

  /**
   * Table identifier for "OcrSegmentationModes": {5455dc3b-79f8-4c06-8cf0-64e63919ab4a}.
   */
  static get OcrSegmentationModes(): OcrSegmentationModesSchemeInfo {
    return SchemeInfo.ocrSegmentationModes = SchemeInfo.ocrSegmentationModes ?? new OcrSegmentationModesSchemeInfo();
  }

  private static ocrSegmentationModes: OcrSegmentationModesSchemeInfo;

  /**
   * Table identifier for "OcrSettings": {4463ae11-e603-4daa-8b93-2e4323abef37}.
   */
  static get OcrSettings(): OcrSettingsSchemeInfo {
    return SchemeInfo.ocrSettings = SchemeInfo.ocrSettings ?? new OcrSettingsSchemeInfo();
  }

  private static ocrSettings: OcrSettingsSchemeInfo;

  /**
   * Table identifier for "OcrSettingsPatterns": {e432240e-e30a-4568-aa91-bb9a9e55ea61}.
   */
  static get OcrSettingsPatterns(): OcrSettingsPatternsSchemeInfo {
    return SchemeInfo.ocrSettingsPatterns = SchemeInfo.ocrSettingsPatterns ?? new OcrSettingsPatternsSchemeInfo();
  }

  private static ocrSettingsPatterns: OcrSettingsPatternsSchemeInfo;

  /**
   * Table identifier for "OnlyOfficeFileCache": {8c151402-bae9-41a6-864f-f7558bd88c86}.
   */
  static get OnlyOfficeFileCache(): OnlyOfficeFileCacheSchemeInfo {
    return SchemeInfo.onlyOfficeFileCache = SchemeInfo.onlyOfficeFileCache ?? new OnlyOfficeFileCacheSchemeInfo();
  }

  private static onlyOfficeFileCache: OnlyOfficeFileCacheSchemeInfo;

  /**
   * Table identifier for "OnlyOfficeSettings": {703ad2d1-6246-4d47-a0ef-814b9466c027}.
   */
  static get OnlyOfficeSettings(): OnlyOfficeSettingsSchemeInfo {
    return SchemeInfo.onlyOfficeSettings = SchemeInfo.onlyOfficeSettings ?? new OnlyOfficeSettingsSchemeInfo();
  }

  private static onlyOfficeSettings: OnlyOfficeSettingsSchemeInfo;

  /**
   * Table identifier for "Operations": {4ae0856c-dd1d-4da8-80b4-e6d232be8d94}.
   */
  static get Operations(): OperationsSchemeInfo {
    return SchemeInfo.operations = SchemeInfo.operations ?? new OperationsSchemeInfo();
  }

  private static operations: OperationsSchemeInfo;

  /**
   * Table identifier for "OperationStates": {e726339c-e2fc-4d7c-a9b4-011577ff2106}.
   */
  static get OperationStates(): OperationStatesSchemeInfo {
    return SchemeInfo.operationStates = SchemeInfo.operationStates ?? new OperationStatesSchemeInfo();
  }

  private static operationStates: OperationStatesSchemeInfo;

  /**
   * Table identifier for "OperationsVirtual": {a87294b1-0c66-41bc-98e8-765dfb8dcf56}.
   */
  static get OperationsVirtual(): OperationsVirtualSchemeInfo {
    return SchemeInfo.operationsVirtual = SchemeInfo.operationsVirtual ?? new OperationsVirtualSchemeInfo();
  }

  private static operationsVirtual: OperationsVirtualSchemeInfo;

  /**
   * Table identifier for "OperationTypes": {b23fccd5-5ba1-45b6-a0ad-e9d0cf730da0}.
   */
  static get OperationTypes(): OperationTypesSchemeInfo {
    return SchemeInfo.operationTypes = SchemeInfo.operationTypes ?? new OperationTypesSchemeInfo();
  }

  private static operationTypes: OperationTypesSchemeInfo;

  /**
   * Table identifier for "OperationUpdates": {a6435575-79d4-44b6-b755-b9a026431556}.
   */
  static get OperationUpdates(): OperationUpdatesSchemeInfo {
    return SchemeInfo.operationUpdates = SchemeInfo.operationUpdates ?? new OperationUpdatesSchemeInfo();
  }

  private static operationUpdates: OperationUpdatesSchemeInfo;

  /**
   * Table identifier for "Outbox": {b4412a23-3e36-4468-a9cf-6b7b553f9e64}.
   */
  static get Outbox(): OutboxSchemeInfo {
    return SchemeInfo.outbox = SchemeInfo.outbox ?? new OutboxSchemeInfo();
  }

  private static outbox: OutboxSchemeInfo;

  /**
   * Table identifier for "OutgoingRefDocs": {73320234-fc44-4126-a7a6-5dd0bdaa4880}.
   */
  static get OutgoingRefDocs(): OutgoingRefDocsSchemeInfo {
    return SchemeInfo.outgoingRefDocs = SchemeInfo.outgoingRefDocs ?? new OutgoingRefDocsSchemeInfo();
  }

  private static outgoingRefDocs: OutgoingRefDocsSchemeInfo;

  /**
   * Table identifier for "Partitions": {5ca00fac-d04e-4b82-8139-9778518e00bf}.
   */
  static get Partitions(): PartitionsSchemeInfo {
    return SchemeInfo.partitions = SchemeInfo.partitions ?? new PartitionsSchemeInfo();
  }

  private static partitions: PartitionsSchemeInfo;

  /**
   * Table identifier for "Partners": {5d47ef13-b6f4-47ef-9815-3b3d0e6d475a}.
   */
  static get Partners(): PartnersSchemeInfo {
    return SchemeInfo.partners = SchemeInfo.partners ?? new PartnersSchemeInfo();
  }

  private static partners: PartnersSchemeInfo;

  /**
   * Table identifier for "PartnersContacts": {c57f5563-6673-4ca0-83a1-2896dbd090e1}.
   */
  static get PartnersContacts(): PartnersContactsSchemeInfo {
    return SchemeInfo.partnersContacts = SchemeInfo.partnersContacts ?? new PartnersContactsSchemeInfo();
  }

  private static partnersContacts: PartnersContactsSchemeInfo;

  /**
   * Table identifier for "PartnersTypes": {354e4f5a-e50c-4a11-84d0-6e0a98a81ca5}.
   */
  static get PartnersTypes(): PartnersTypesSchemeInfo {
    return SchemeInfo.partnersTypes = SchemeInfo.partnersTypes ?? new PartnersTypesSchemeInfo();
  }

  private static partnersTypes: PartnersTypesSchemeInfo;

  /**
   * Table identifier for "Performers": {d0f5547b-b2f5-4a08-8cd9-b34138d35125}.
   */
  static get Performers(): PerformersSchemeInfo {
    return SchemeInfo.performers = SchemeInfo.performers ?? new PerformersSchemeInfo();
  }

  private static performers: PerformersSchemeInfo;

  /**
   * Table identifier for "PersonalLicenses": {8a96f177-b2a7-4ffc-885b-2f36730d0d2e}.
   */
  static get PersonalLicenses(): PersonalLicensesSchemeInfo {
    return SchemeInfo.personalLicenses = SchemeInfo.personalLicenses ?? new PersonalLicensesSchemeInfo();
  }

  private static personalLicenses: PersonalLicensesSchemeInfo;

  /**
   * Table identifier for "PersonalRoleDepartmentsVirtual": {21566803-b822-42b2-ab11-2c20e72a0de4}.
   */
  static get PersonalRoleDepartmentsVirtual(): PersonalRoleDepartmentsVirtualSchemeInfo {
    return SchemeInfo.personalRoleDepartmentsVirtual = SchemeInfo.personalRoleDepartmentsVirtual ?? new PersonalRoleDepartmentsVirtualSchemeInfo();
  }

  private static personalRoleDepartmentsVirtual: PersonalRoleDepartmentsVirtualSchemeInfo;

  /**
   * Table identifier for "PersonalRoleNotificationRulesVirtual": {925a75d4-639f-4467-9155-c8e21f5433a9}.
   */
  static get PersonalRoleNotificationRulesVirtual(): PersonalRoleNotificationRulesVirtualSchemeInfo {
    return SchemeInfo.personalRoleNotificationRulesVirtual = SchemeInfo.personalRoleNotificationRulesVirtual ?? new PersonalRoleNotificationRulesVirtualSchemeInfo();
  }

  private static personalRoleNotificationRulesVirtual: PersonalRoleNotificationRulesVirtualSchemeInfo;

  /**
   * Table identifier for "PersonalRoleNotificationRuleTypesVirtual": {16baecaa-9088-4635-af93-4c042893bf1d}.
   */
  static get PersonalRoleNotificationRuleTypesVirtual(): PersonalRoleNotificationRuleTypesVirtualSchemeInfo {
    return SchemeInfo.personalRoleNotificationRuleTypesVirtual = SchemeInfo.personalRoleNotificationRuleTypesVirtual ?? new PersonalRoleNotificationRuleTypesVirtualSchemeInfo();
  }

  private static personalRoleNotificationRuleTypesVirtual: PersonalRoleNotificationRuleTypesVirtualSchemeInfo;

  /**
   * Table identifier for "PersonalRoleRolesVirtual": {e631fc2a-7628-4d7e-a118-99d310fa12b8}.
   */
  static get PersonalRoleRolesVirtual(): PersonalRoleRolesVirtualSchemeInfo {
    return SchemeInfo.personalRoleRolesVirtual = SchemeInfo.personalRoleRolesVirtual ?? new PersonalRoleRolesVirtualSchemeInfo();
  }

  private static personalRoleRolesVirtual: PersonalRoleRolesVirtualSchemeInfo;

  /**
   * Table identifier for "PersonalRoles": {6c977939-bbfc-456f-a133-f1c2244e3cc3}.
   */
  static get PersonalRoles(): PersonalRolesSchemeInfo {
    return SchemeInfo.personalRoles = SchemeInfo.personalRoles ?? new PersonalRolesSchemeInfo();
  }

  private static personalRoles: PersonalRolesSchemeInfo;

  /**
   * Table identifier for "PersonalRoleSatellite": {62fd7bdd-0fc1-4370-afd6-54ac7e5320b4}.
   */
  static get PersonalRoleSatellite(): PersonalRoleSatelliteSchemeInfo {
    return SchemeInfo.personalRoleSatellite = SchemeInfo.personalRoleSatellite ?? new PersonalRoleSatelliteSchemeInfo();
  }

  private static personalRoleSatellite: PersonalRoleSatelliteSchemeInfo;

  /**
   * Table identifier for "PersonalRoleStaticRolesVirtual": {122da60d-3efc-42a2-bd84-510c0819807b}.
   */
  static get PersonalRoleStaticRolesVirtual(): PersonalRoleStaticRolesVirtualSchemeInfo {
    return SchemeInfo.personalRoleStaticRolesVirtual = SchemeInfo.personalRoleStaticRolesVirtual ?? new PersonalRoleStaticRolesVirtualSchemeInfo();
  }

  private static personalRoleStaticRolesVirtual: PersonalRoleStaticRolesVirtualSchemeInfo;

  /**
   * Table identifier for "PersonalRoleSubscribedTypesVirtual": {62859264-a143-4ec0-a86b-bba80f6f61ac}.
   */
  static get PersonalRoleSubscribedTypesVirtual(): PersonalRoleSubscribedTypesVirtualSchemeInfo {
    return SchemeInfo.personalRoleSubscribedTypesVirtual = SchemeInfo.personalRoleSubscribedTypesVirtual ?? new PersonalRoleSubscribedTypesVirtualSchemeInfo();
  }

  private static personalRoleSubscribedTypesVirtual: PersonalRoleSubscribedTypesVirtualSchemeInfo;

  /**
   * Table identifier for "PersonalRolesVirtual": {e86b07e5-da20-487b-a55e-0ed56606bddf}.
   */
  static get PersonalRolesVirtual(): PersonalRolesVirtualSchemeInfo {
    return SchemeInfo.personalRolesVirtual = SchemeInfo.personalRolesVirtual ?? new PersonalRolesVirtualSchemeInfo();
  }

  private static personalRolesVirtual: PersonalRolesVirtualSchemeInfo;

  /**
   * Table identifier for "PersonalRoleUnsubscibedTypesVirtual": {fc4566ea-029b-4d37-b3f0-4ca62a4cb500}.
   */
  static get PersonalRoleUnsubscibedTypesVirtual(): PersonalRoleUnsubscibedTypesVirtualSchemeInfo {
    return SchemeInfo.personalRoleUnsubscibedTypesVirtual = SchemeInfo.personalRoleUnsubscibedTypesVirtual ?? new PersonalRoleUnsubscibedTypesVirtualSchemeInfo();
  }

  private static personalRoleUnsubscibedTypesVirtual: PersonalRoleUnsubscibedTypesVirtualSchemeInfo;

  /**
   * Table identifier for "Procedures": {1bf6a3b2-725a-487c-b4d8-6b082fb56037}.
   */
  static get Procedures(): ProceduresSchemeInfo {
    return SchemeInfo.procedures = SchemeInfo.procedures ?? new ProceduresSchemeInfo();
  }

  private static procedures: ProceduresSchemeInfo;

  /**
   * Table identifier for "ProtocolDecisions": {91c272de-462d-4076-8f64-592885a4abd4}.
   */
  static get ProtocolDecisions(): ProtocolDecisionsSchemeInfo {
    return SchemeInfo.protocolDecisions = SchemeInfo.protocolDecisions ?? new ProtocolDecisionsSchemeInfo();
  }

  private static protocolDecisions: ProtocolDecisionsSchemeInfo;

  /**
   * Table identifier for "ProtocolReports": {5576e1f1-316a-4256-a136-c33eb871b7d5}.
   */
  static get ProtocolReports(): ProtocolReportsSchemeInfo {
    return SchemeInfo.protocolReports = SchemeInfo.protocolReports ?? new ProtocolReportsSchemeInfo();
  }

  private static protocolReports: ProtocolReportsSchemeInfo;

  /**
   * Table identifier for "ProtocolResponsibles": {34e972b7-fd6f-4417-99d1-f2578a82ab1d}.
   */
  static get ProtocolResponsibles(): ProtocolResponsiblesSchemeInfo {
    return SchemeInfo.protocolResponsibles = SchemeInfo.protocolResponsibles ?? new ProtocolResponsiblesSchemeInfo();
  }

  private static protocolResponsibles: ProtocolResponsiblesSchemeInfo;

  /**
   * Table identifier for "Protocols": {b98383dc-ecf0-4ad0-b92d-dd599775b8f5}.
   */
  static get Protocols(): ProtocolsSchemeInfo {
    return SchemeInfo.protocols = SchemeInfo.protocols ?? new ProtocolsSchemeInfo();
  }

  private static protocols: ProtocolsSchemeInfo;

  /**
   * Table identifier for "Recipients": {386509d9-4130-467f-9a52-0004aa15247e}.
   */
  static get Recipients(): RecipientsSchemeInfo {
    return SchemeInfo.recipients = SchemeInfo.recipients ?? new RecipientsSchemeInfo();
  }

  private static recipients: RecipientsSchemeInfo;

  /**
   * Table identifier for "ReportRolesActive": {fd37a3c0-33e5-4256-98bf-4440402f4116}.
   */
  static get ReportRolesActive(): ReportRolesActiveSchemeInfo {
    return SchemeInfo.reportRolesActive = SchemeInfo.reportRolesActive ?? new ReportRolesActiveSchemeInfo();
  }

  private static reportRolesActive: ReportRolesActiveSchemeInfo;

  /**
   * Table identifier for "ReportRolesPassive": {599f50f0-95c4-48ad-a739-c54fd9b5f829}.
   */
  static get ReportRolesPassive(): ReportRolesPassiveSchemeInfo {
    return SchemeInfo.reportRolesPassive = SchemeInfo.reportRolesPassive ?? new ReportRolesPassiveSchemeInfo();
  }

  private static reportRolesPassive: ReportRolesPassiveSchemeInfo;

  /**
   * Table identifier for "ReportRolesRules": {359edaf2-fdb7-4e24-afc8-f31281328a81}.
   */
  static get ReportRolesRules(): ReportRolesRulesSchemeInfo {
    return SchemeInfo.reportRolesRules = SchemeInfo.reportRolesRules ?? new ReportRolesRulesSchemeInfo();
  }

  private static reportRolesRules: ReportRolesRulesSchemeInfo;

  /**
   * Table identifier for "RoleDeputies": {900bdbcd-1e87-451c-8b4b-082d8f7efd48}.
   */
  static get RoleDeputies(): RoleDeputiesSchemeInfo {
    return SchemeInfo.roleDeputies = SchemeInfo.roleDeputies ?? new RoleDeputiesSchemeInfo();
  }

  private static roleDeputies: RoleDeputiesSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesManagement": {0f489948-bc16-42a6-8953-b92100807296}.
   */
  static get RoleDeputiesManagement(): RoleDeputiesManagementSchemeInfo {
    return SchemeInfo.roleDeputiesManagement = SchemeInfo.roleDeputiesManagement ?? new RoleDeputiesManagementSchemeInfo();
  }

  private static roleDeputiesManagement: RoleDeputiesManagementSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesManagementAccess": {edbc91b1-dd36-43c2-867a-67c74ed7f403}.
   */
  static get RoleDeputiesManagementAccess(): RoleDeputiesManagementAccessSchemeInfo {
    return SchemeInfo.roleDeputiesManagementAccess = SchemeInfo.roleDeputiesManagementAccess ?? new RoleDeputiesManagementAccessSchemeInfo();
  }

  private static roleDeputiesManagementAccess: RoleDeputiesManagementAccessSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesManagementDeputizedRolesVirtual": {997561ee-218f-4f22-946b-87a78755c3e6}.
   */
  static get RoleDeputiesManagementDeputizedRolesVirtual(): RoleDeputiesManagementDeputizedRolesVirtualSchemeInfo {
    return SchemeInfo.roleDeputiesManagementDeputizedRolesVirtual = SchemeInfo.roleDeputiesManagementDeputizedRolesVirtual ?? new RoleDeputiesManagementDeputizedRolesVirtualSchemeInfo();
  }

  private static roleDeputiesManagementDeputizedRolesVirtual: RoleDeputiesManagementDeputizedRolesVirtualSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesManagementDeputizedVirtual": {c55a7921-d82d-4f8b-b801-f1c693c4c2e3}.
   */
  static get RoleDeputiesManagementDeputizedVirtual(): RoleDeputiesManagementDeputizedVirtualSchemeInfo {
    return SchemeInfo.roleDeputiesManagementDeputizedVirtual = SchemeInfo.roleDeputiesManagementDeputizedVirtual ?? new RoleDeputiesManagementDeputizedVirtualSchemeInfo();
  }

  private static roleDeputiesManagementDeputizedVirtual: RoleDeputiesManagementDeputizedVirtualSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesManagementHelperVirtual": {81f28cf8-709b-4dde-8c9e-505d3d7870e0}.
   */
  static get RoleDeputiesManagementHelperVirtual(): RoleDeputiesManagementHelperVirtualSchemeInfo {
    return SchemeInfo.roleDeputiesManagementHelperVirtual = SchemeInfo.roleDeputiesManagementHelperVirtual ?? new RoleDeputiesManagementHelperVirtualSchemeInfo();
  }

  private static roleDeputiesManagementHelperVirtual: RoleDeputiesManagementHelperVirtualSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesManagementRoles": {91acf9b9-8476-4dc8-a239-ac6b8f250077}.
   */
  static get RoleDeputiesManagementRoles(): RoleDeputiesManagementRolesSchemeInfo {
    return SchemeInfo.roleDeputiesManagementRoles = SchemeInfo.roleDeputiesManagementRoles ?? new RoleDeputiesManagementRolesSchemeInfo();
  }

  private static roleDeputiesManagementRoles: RoleDeputiesManagementRolesSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesManagementRolesVirtual": {9650456c-27ea-4f62-9073-95b9be1d49ba}.
   */
  static get RoleDeputiesManagementRolesVirtual(): RoleDeputiesManagementRolesVirtualSchemeInfo {
    return SchemeInfo.roleDeputiesManagementRolesVirtual = SchemeInfo.roleDeputiesManagementRolesVirtual ?? new RoleDeputiesManagementRolesVirtualSchemeInfo();
  }

  private static roleDeputiesManagementRolesVirtual: RoleDeputiesManagementRolesVirtualSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesManagementUsers": {b8f9c863-22fd-4d63-a7cf-b9f0de519b47}.
   */
  static get RoleDeputiesManagementUsers(): RoleDeputiesManagementUsersSchemeInfo {
    return SchemeInfo.roleDeputiesManagementUsers = SchemeInfo.roleDeputiesManagementUsers ?? new RoleDeputiesManagementUsersSchemeInfo();
  }

  private static roleDeputiesManagementUsers: RoleDeputiesManagementUsersSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesManagementUsersVirtual": {9dc135b0-21b8-4deb-ab65-bdda57a3fbb5}.
   */
  static get RoleDeputiesManagementUsersVirtual(): RoleDeputiesManagementUsersVirtualSchemeInfo {
    return SchemeInfo.roleDeputiesManagementUsersVirtual = SchemeInfo.roleDeputiesManagementUsersVirtual ?? new RoleDeputiesManagementUsersVirtualSchemeInfo();
  }

  private static roleDeputiesManagementUsersVirtual: RoleDeputiesManagementUsersVirtualSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesManagementVirtual": {79dca225-d99c-4dfd-94d9-27ed3ab15046}.
   */
  static get RoleDeputiesManagementVirtual(): RoleDeputiesManagementVirtualSchemeInfo {
    return SchemeInfo.roleDeputiesManagementVirtual = SchemeInfo.roleDeputiesManagementVirtual ?? new RoleDeputiesManagementVirtualSchemeInfo();
  }

  private static roleDeputiesManagementVirtual: RoleDeputiesManagementVirtualSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesNestedManagement": {dd329f32-adf0-4336-bd9e-fa084c0fe494}.
   */
  static get RoleDeputiesNestedManagement(): RoleDeputiesNestedManagementSchemeInfo {
    return SchemeInfo.roleDeputiesNestedManagement = SchemeInfo.roleDeputiesNestedManagement ?? new RoleDeputiesNestedManagementSchemeInfo();
  }

  private static roleDeputiesNestedManagement: RoleDeputiesNestedManagementSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesNestedManagementTypes": {0958f50b-8fd2-4e65-9531-fd540f3150ab}.
   */
  static get RoleDeputiesNestedManagementTypes(): RoleDeputiesNestedManagementTypesSchemeInfo {
    return SchemeInfo.roleDeputiesNestedManagementTypes = SchemeInfo.roleDeputiesNestedManagementTypes ?? new RoleDeputiesNestedManagementTypesSchemeInfo();
  }

  private static roleDeputiesNestedManagementTypes: RoleDeputiesNestedManagementTypesSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesNestedManagementTypesVirtual": {a8c71408-d1a3-4dbc-abcb-287dd7b7c648}.
   */
  static get RoleDeputiesNestedManagementTypesVirtual(): RoleDeputiesNestedManagementTypesVirtualSchemeInfo {
    return SchemeInfo.roleDeputiesNestedManagementTypesVirtual = SchemeInfo.roleDeputiesNestedManagementTypesVirtual ?? new RoleDeputiesNestedManagementTypesVirtualSchemeInfo();
  }

  private static roleDeputiesNestedManagementTypesVirtual: RoleDeputiesNestedManagementTypesVirtualSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesNestedManagementUsers": {c9bf7542-de37-4fad-9cda-6b1a5a4964b7}.
   */
  static get RoleDeputiesNestedManagementUsers(): RoleDeputiesNestedManagementUsersSchemeInfo {
    return SchemeInfo.roleDeputiesNestedManagementUsers = SchemeInfo.roleDeputiesNestedManagementUsers ?? new RoleDeputiesNestedManagementUsersSchemeInfo();
  }

  private static roleDeputiesNestedManagementUsers: RoleDeputiesNestedManagementUsersSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesNestedManagementUsersVirtual": {6d0cfd99-aa36-4992-9231-eea478138fe6}.
   */
  static get RoleDeputiesNestedManagementUsersVirtual(): RoleDeputiesNestedManagementUsersVirtualSchemeInfo {
    return SchemeInfo.roleDeputiesNestedManagementUsersVirtual = SchemeInfo.roleDeputiesNestedManagementUsersVirtual ?? new RoleDeputiesNestedManagementUsersVirtualSchemeInfo();
  }

  private static roleDeputiesNestedManagementUsersVirtual: RoleDeputiesNestedManagementUsersVirtualSchemeInfo;

  /**
   * Table identifier for "RoleDeputiesNestedManagementVirtual": {3937aa4f-0658-4e8b-a25a-911802f1fa82}.
   */
  static get RoleDeputiesNestedManagementVirtual(): RoleDeputiesNestedManagementVirtualSchemeInfo {
    return SchemeInfo.roleDeputiesNestedManagementVirtual = SchemeInfo.roleDeputiesNestedManagementVirtual ?? new RoleDeputiesNestedManagementVirtualSchemeInfo();
  }

  private static roleDeputiesNestedManagementVirtual: RoleDeputiesNestedManagementVirtualSchemeInfo;

  /**
   * Table identifier for "RoleGenerators": {747bb53c-9e47-418d-892d-fb52a18eb42d}.
   */
  static get RoleGenerators(): RoleGeneratorsSchemeInfo {
    return SchemeInfo.roleGenerators = SchemeInfo.roleGenerators ?? new RoleGeneratorsSchemeInfo();
  }

  private static roleGenerators: RoleGeneratorsSchemeInfo;

  /**
   * Table identifier for "Roles": {81f6010b-9641-4aa5-8897-b8e8603fbf4b}.
   */
  static get Roles(): RolesSchemeInfo {
    return SchemeInfo.roles = SchemeInfo.roles ?? new RolesSchemeInfo();
  }

  private static roles: RolesSchemeInfo;

  /**
   * Table identifier for "RoleTypes": {8d6cb6a6-c3f5-4c92-88d7-0cc6b8e8d09d}.
   */
  static get RoleTypes(): RoleTypesSchemeInfo {
    return SchemeInfo.roleTypes = SchemeInfo.roleTypes ?? new RoleTypesSchemeInfo();
  }

  private static roleTypes: RoleTypesSchemeInfo;

  /**
   * Table identifier for "RoleUsers": {a3a271db-3ce6-47c7-b75e-87dcc9dc052a}.
   */
  static get RoleUsers(): RoleUsersSchemeInfo {
    return SchemeInfo.roleUsers = SchemeInfo.roleUsers ?? new RoleUsersSchemeInfo();
  }

  private static roleUsers: RoleUsersSchemeInfo;

  /**
   * Table identifier for "RoleUsersVirtual": {47428527-dd1f-4e52-9ba5-a2a988abdf93}.
   */
  static get RoleUsersVirtual(): RoleUsersVirtualSchemeInfo {
    return SchemeInfo.roleUsersVirtual = SchemeInfo.roleUsersVirtual ?? new RoleUsersVirtualSchemeInfo();
  }

  private static roleUsersVirtual: RoleUsersVirtualSchemeInfo;

  /**
   * Table identifier for "Satellites": {608289ef-42c8-4f6e-8d4f-27ef725732b5}.
   */
  static get Satellites(): SatellitesSchemeInfo {
    return SchemeInfo.satellites = SchemeInfo.satellites ?? new SatellitesSchemeInfo();
  }

  private static satellites: SatellitesSchemeInfo;

  /**
   * Table identifier for "SchedulingTypes": {3cf60a31-28d4-42ad-86b2-343a298ea7a8}.
   */
  static get SchedulingTypes(): SchedulingTypesSchemeInfo {
    return SchemeInfo.schedulingTypes = SchemeInfo.schedulingTypes ?? new SchedulingTypesSchemeInfo();
  }

  private static schedulingTypes: SchedulingTypesSchemeInfo;

  /**
   * Table identifier for "Scheme": {c4fcd8d3-fcb1-451f-98f4-e352cd8a3a41}.
   */
  static get Scheme(): SchemeSchemeInfo {
    return SchemeInfo.scheme = SchemeInfo.scheme ?? new SchemeSchemeInfo();
  }

  private static scheme: SchemeSchemeInfo;

  /**
   * Table identifier for "SearchQueries": {d0dde291-0d94-4e76-9f69-902809975216}.
   */
  static get SearchQueries(): SearchQueriesSchemeInfo {
    return SchemeInfo.searchQueries = SchemeInfo.searchQueries ?? new SearchQueriesSchemeInfo();
  }

  private static searchQueries: SearchQueriesSchemeInfo;

  /**
   * Table identifier for "SectionChangedCondition": {3b9d9643-ed47-4301-94b2-8eedcabc23bc}.
   */
  static get SectionChangedCondition(): SectionChangedConditionSchemeInfo {
    return SchemeInfo.sectionChangedCondition = SchemeInfo.sectionChangedCondition ?? new SectionChangedConditionSchemeInfo();
  }

  private static sectionChangedCondition: SectionChangedConditionSchemeInfo;

  /**
   * Table identifier for "SequencesInfo": {f113a406-970b-4c1b-820f-9d960c37692a}.
   */
  static get SequencesInfo(): SequencesInfoSchemeInfo {
    return SchemeInfo.sequencesInfo = SchemeInfo.sequencesInfo ?? new SequencesInfoSchemeInfo();
  }

  private static sequencesInfo: SequencesInfoSchemeInfo;

  /**
   * Table identifier for "SequencesIntervals": {510bf28c-bccf-4701-b9fa-1081c22c2ef9}.
   */
  static get SequencesIntervals(): SequencesIntervalsSchemeInfo {
    return SchemeInfo.sequencesIntervals = SchemeInfo.sequencesIntervals ?? new SequencesIntervalsSchemeInfo();
  }

  private static sequencesIntervals: SequencesIntervalsSchemeInfo;

  /**
   * Table identifier for "SequencesReserved": {506e2fe6-397e-45c1-ae35-22cd7e85b14d}.
   */
  static get SequencesReserved(): SequencesReservedSchemeInfo {
    return SchemeInfo.sequencesReserved = SchemeInfo.sequencesReserved ?? new SequencesReservedSchemeInfo();
  }

  private static sequencesReserved: SequencesReservedSchemeInfo;

  /**
   * Table identifier for "ServerInstances": {c3d76e97-459f-41e0-8d45-56fb19b5e07e}.
   */
  static get ServerInstances(): ServerInstancesSchemeInfo {
    return SchemeInfo.serverInstances = SchemeInfo.serverInstances ?? new ServerInstancesSchemeInfo();
  }

  private static serverInstances: ServerInstancesSchemeInfo;

  /**
   * Table identifier for "SessionActivity": {a396cc70-75bb-427b-ac42-4978fb5575ac}.
   */
  static get SessionActivity(): SessionActivitySchemeInfo {
    return SchemeInfo.sessionActivity = SchemeInfo.sessionActivity ?? new SessionActivitySchemeInfo();
  }

  private static sessionActivity: SessionActivitySchemeInfo;

  /**
   * Table identifier for "Sessions": {bbd3d574-a33e-49fb-867d-db3c6811365e}.
   */
  static get Sessions(): SessionsSchemeInfo {
    return SchemeInfo.sessions = SchemeInfo.sessions ?? new SessionsSchemeInfo();
  }

  private static sessions: SessionsSchemeInfo;

  /**
   * Table identifier for "SessionServiceTypes": {62c1a795-1688-48a1-b0af-d77032c90bab}.
   */
  static get SessionServiceTypes(): SessionServiceTypesSchemeInfo {
    return SchemeInfo.sessionServiceTypes = SchemeInfo.sessionServiceTypes ?? new SessionServiceTypesSchemeInfo();
  }

  private static sessionServiceTypes: SessionServiceTypesSchemeInfo;

  /**
   * Table identifier for "SignatureCertificateSettings": {faf66527-24c2-4f20-afa8-46915e5fd4d6}.
   */
  static get SignatureCertificateSettings(): SignatureCertificateSettingsSchemeInfo {
    return SchemeInfo.signatureCertificateSettings = SchemeInfo.signatureCertificateSettings ?? new SignatureCertificateSettingsSchemeInfo();
  }

  private static signatureCertificateSettings: SignatureCertificateSettingsSchemeInfo;

  /**
   * Table identifier for "SignatureDigestAlgorithms": {9180bf30-3b8b-4adc-a285-d9ee97aea219}.
   */
  static get SignatureDigestAlgorithms(): SignatureDigestAlgorithmsSchemeInfo {
    return SchemeInfo.signatureDigestAlgorithms = SchemeInfo.signatureDigestAlgorithms ?? new SignatureDigestAlgorithmsSchemeInfo();
  }

  private static signatureDigestAlgorithms: SignatureDigestAlgorithmsSchemeInfo;

  /**
   * Table identifier for "SignatureEncryptDigestSettings": {7c57bbba-8acc-4abf-b3cc-372399b68dbc}.
   */
  static get SignatureEncryptDigestSettings(): SignatureEncryptDigestSettingsSchemeInfo {
    return SchemeInfo.signatureEncryptDigestSettings = SchemeInfo.signatureEncryptDigestSettings ?? new SignatureEncryptDigestSettingsSchemeInfo();
  }

  private static signatureEncryptDigestSettings: SignatureEncryptDigestSettingsSchemeInfo;

  /**
   * Table identifier for "SignatureEncryptionAlgorithms": {93f36ef0-b0ca-4726-9038-b10339db4b00}.
   */
  static get SignatureEncryptionAlgorithms(): SignatureEncryptionAlgorithmsSchemeInfo {
    return SchemeInfo.signatureEncryptionAlgorithms = SchemeInfo.signatureEncryptionAlgorithms ?? new SignatureEncryptionAlgorithmsSchemeInfo();
  }

  private static signatureEncryptionAlgorithms: SignatureEncryptionAlgorithmsSchemeInfo;

  /**
   * Table identifier for "SignatureManagerVirtual": {72eb4e5a-f328-40e6-bb2d-18ea0a9a9d2b}.
   */
  static get SignatureManagerVirtual(): SignatureManagerVirtualSchemeInfo {
    return SchemeInfo.signatureManagerVirtual = SchemeInfo.signatureManagerVirtual ?? new SignatureManagerVirtualSchemeInfo();
  }

  private static signatureManagerVirtual: SignatureManagerVirtualSchemeInfo;

  /**
   * Table identifier for "SignaturePackagings": {15620b78-46b8-4520-aa60-4bfefe67c731}.
   */
  static get SignaturePackagings(): SignaturePackagingsSchemeInfo {
    return SchemeInfo.signaturePackagings = SchemeInfo.signaturePackagings ?? new SignaturePackagingsSchemeInfo();
  }

  private static signaturePackagings: SignaturePackagingsSchemeInfo;

  /**
   * Table identifier for "SignatureProfiles": {eca29bb9-3085-4556-b19a-6015cbc8fb25}.
   */
  static get SignatureProfiles(): SignatureProfilesSchemeInfo {
    return SchemeInfo.signatureProfiles = SchemeInfo.signatureProfiles ?? new SignatureProfilesSchemeInfo();
  }

  private static signatureProfiles: SignatureProfilesSchemeInfo;

  /**
   * Table identifier for "SignatureSettings": {076b2050-e20b-412b-942b-b4cb063e6941}.
   */
  static get SignatureSettings(): SignatureSettingsSchemeInfo {
    return SchemeInfo.signatureSettings = SchemeInfo.signatureSettings ?? new SignatureSettingsSchemeInfo();
  }

  private static signatureSettings: SignatureSettingsSchemeInfo;

  /**
   * Table identifier for "SignatureTypes": {577baaea-6832-4eb7-9333-60661367720e}.
   */
  static get SignatureTypes(): SignatureTypesSchemeInfo {
    return SchemeInfo.signatureTypes = SchemeInfo.signatureTypes ?? new SignatureTypesSchemeInfo();
  }

  private static signatureTypes: SignatureTypesSchemeInfo;

  /**
   * Table identifier for "SmartRoleGeneratorInfo": {c44db46a-349f-45ec-b0ab-ec212c09b276}.
   */
  static get SmartRoleGeneratorInfo(): SmartRoleGeneratorInfoSchemeInfo {
    return SchemeInfo.smartRoleGeneratorInfo = SchemeInfo.smartRoleGeneratorInfo ?? new SmartRoleGeneratorInfoSchemeInfo();
  }

  private static smartRoleGeneratorInfo: SmartRoleGeneratorInfoSchemeInfo;

  /**
   * Table identifier for "SmartRoleGenerators": {5f3a0dbc-2fc4-4269-8a5d-eb95f39970ba}.
   */
  static get SmartRoleGenerators(): SmartRoleGeneratorsSchemeInfo {
    return SchemeInfo.smartRoleGenerators = SchemeInfo.smartRoleGenerators ?? new SmartRoleGeneratorsSchemeInfo();
  }

  private static smartRoleGenerators: SmartRoleGeneratorsSchemeInfo;

  /**
   * Table identifier for "SmartRoleMembers": {73cbcd25-8709-4c3d-9091-3db6ccba5055}.
   */
  static get SmartRoleMembers(): SmartRoleMembersSchemeInfo {
    return SchemeInfo.smartRoleMembers = SchemeInfo.smartRoleMembers ?? new SmartRoleMembersSchemeInfo();
  }

  private static smartRoleMembers: SmartRoleMembersSchemeInfo;

  /**
   * Table identifier for "SmartRoles": {844013f9-7faa-422a-b583-2b04ae46f0be}.
   */
  static get SmartRoles(): SmartRolesSchemeInfo {
    return SchemeInfo.smartRoles = SchemeInfo.smartRoles ?? new SmartRolesSchemeInfo();
  }

  private static smartRoles: SmartRolesSchemeInfo;

  /**
   * Table identifier for "Tables": {66b31fcc-b8fa-465a-91f2-0dd391cc76ec}.
   */
  static get Tables(): TablesSchemeInfo {
    return SchemeInfo.tables = SchemeInfo.tables ?? new TablesSchemeInfo();
  }

  private static tables: TablesSchemeInfo;

  /**
   * Table identifier for "TagCards": {7ecf1fb4-cd7b-4fea-9a3b-6e22b2186ed6}.
   */
  static get TagCards(): TagCardsSchemeInfo {
    return SchemeInfo.tagCards = SchemeInfo.tagCards ?? new TagCardsSchemeInfo();
  }

  private static tagCards: TagCardsSchemeInfo;

  /**
   * Table identifier for "TagCondition": {8a9a0383-b94a-40c0-aa3e-7b461f25b598}.
   */
  static get TagCondition(): TagConditionSchemeInfo {
    return SchemeInfo.tagCondition = SchemeInfo.tagCondition ?? new TagConditionSchemeInfo();
  }

  private static tagCondition: TagConditionSchemeInfo;

  /**
   * Table identifier for "TagEditors": {c4ee86f8-3022-432b-ae77-e1ca4a47c891}.
   */
  static get TagEditors(): TagEditorsSchemeInfo {
    return SchemeInfo.tagEditors = SchemeInfo.tagEditors ?? new TagEditorsSchemeInfo();
  }

  private static tagEditors: TagEditorsSchemeInfo;

  /**
   * Table identifier for "Tags": {0bf4050e-d7d4-4cda-ab55-4a4f0148dd7f}.
   */
  static get Tags(): TagsSchemeInfo {
    return SchemeInfo.tags = SchemeInfo.tags ?? new TagsSchemeInfo();
  }

  private static tags: TagsSchemeInfo;

  /**
   * Table identifier for "TagSharedWith": {3b4e5980-82d8-4bce-adb3-4fbc88c3e03a}.
   */
  static get TagSharedWith(): TagSharedWithSchemeInfo {
    return SchemeInfo.tagSharedWith = SchemeInfo.tagSharedWith ?? new TagSharedWithSchemeInfo();
  }

  private static tagSharedWith: TagSharedWithSchemeInfo;

  /**
   * Table identifier for "TagsUserSettingsVirtual": {f4947518-a710-4693-8c8b-ab2acc42bc5a}.
   */
  static get TagsUserSettingsVirtual(): TagsUserSettingsVirtualSchemeInfo {
    return SchemeInfo.tagsUserSettingsVirtual = SchemeInfo.tagsUserSettingsVirtual ?? new TagsUserSettingsVirtualSchemeInfo();
  }

  private static tagsUserSettingsVirtual: TagsUserSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "TaskAssignedRoles": {2539f630-3898-457e-9e49-e1f87552caaf}.
   */
  static get TaskAssignedRoles(): TaskAssignedRolesSchemeInfo {
    return SchemeInfo.taskAssignedRoles = SchemeInfo.taskAssignedRoles ?? new TaskAssignedRolesSchemeInfo();
  }

  private static taskAssignedRoles: TaskAssignedRolesSchemeInfo;

  /**
   * Table identifier for "TaskCommonInfo": {005962e7-65f1-4763-a0ef-b76751d26de3}.
   */
  static get TaskCommonInfo(): TaskCommonInfoSchemeInfo {
    return SchemeInfo.taskCommonInfo = SchemeInfo.taskCommonInfo ?? new TaskCommonInfoSchemeInfo();
  }

  private static taskCommonInfo: TaskCommonInfoSchemeInfo;

  /**
   * Table identifier for "TaskConditionCompletionOptions": {059e1354-8b89-4e86-8fa1-29395e952926}.
   */
  static get TaskConditionCompletionOptions(): TaskConditionCompletionOptionsSchemeInfo {
    return SchemeInfo.taskConditionCompletionOptions = SchemeInfo.taskConditionCompletionOptions ?? new TaskConditionCompletionOptionsSchemeInfo();
  }

  private static taskConditionCompletionOptions: TaskConditionCompletionOptionsSchemeInfo;

  /**
   * Table identifier for "TaskConditionFunctionRoles": {b59a92cb-8414-4a3d-91f9-9d41de829d3f}.
   */
  static get TaskConditionFunctionRoles(): TaskConditionFunctionRolesSchemeInfo {
    return SchemeInfo.taskConditionFunctionRoles = SchemeInfo.taskConditionFunctionRoles ?? new TaskConditionFunctionRolesSchemeInfo();
  }

  private static taskConditionFunctionRoles: TaskConditionFunctionRolesSchemeInfo;

  /**
   * Table identifier for "TaskConditionSettings": {48dcb84f-1518-4de4-8995-86c4e75a1d03}.
   */
  static get TaskConditionSettings(): TaskConditionSettingsSchemeInfo {
    return SchemeInfo.taskConditionSettings = SchemeInfo.taskConditionSettings ?? new TaskConditionSettingsSchemeInfo();
  }

  private static taskConditionSettings: TaskConditionSettingsSchemeInfo;

  /**
   * Table identifier for "TaskConditionTaskKinds": {a3777728-9f01-449a-b94c-953a1e205c5b}.
   */
  static get TaskConditionTaskKinds(): TaskConditionTaskKindsSchemeInfo {
    return SchemeInfo.taskConditionTaskKinds = SchemeInfo.taskConditionTaskKinds ?? new TaskConditionTaskKindsSchemeInfo();
  }

  private static taskConditionTaskKinds: TaskConditionTaskKindsSchemeInfo;

  /**
   * Table identifier for "TaskConditionTaskTypes": {f7cd6753-21d7-4095-8c3a-e7175f591ad3}.
   */
  static get TaskConditionTaskTypes(): TaskConditionTaskTypesSchemeInfo {
    return SchemeInfo.taskConditionTaskTypes = SchemeInfo.taskConditionTaskTypes ?? new TaskConditionTaskTypesSchemeInfo();
  }

  private static taskConditionTaskTypes: TaskConditionTaskTypesSchemeInfo;

  /**
   * Table identifier for "TaskHistory": {f8deab4c-fa9d-404a-8abc-b570cd81820e}.
   */
  static get TaskHistory(): TaskHistorySchemeInfo {
    return SchemeInfo.taskHistory = SchemeInfo.taskHistory ?? new TaskHistorySchemeInfo();
  }

  private static taskHistory: TaskHistorySchemeInfo;

  /**
   * Table identifier for "TaskHistoryGroups": {31644536-fba1-456c-881c-7dae73b7182c}.
   */
  static get TaskHistoryGroups(): TaskHistoryGroupsSchemeInfo {
    return SchemeInfo.taskHistoryGroups = SchemeInfo.taskHistoryGroups ?? new TaskHistoryGroupsSchemeInfo();
  }

  private static taskHistoryGroups: TaskHistoryGroupsSchemeInfo;

  /**
   * Table identifier for "TaskHistoryGroupTypes": {319be329-6cd3-457a-b792-41c26a266b95}.
   */
  static get TaskHistoryGroupTypes(): TaskHistoryGroupTypesSchemeInfo {
    return SchemeInfo.taskHistoryGroupTypes = SchemeInfo.taskHistoryGroupTypes ?? new TaskHistoryGroupTypesSchemeInfo();
  }

  private static taskHistoryGroupTypes: TaskHistoryGroupTypesSchemeInfo;

  /**
   * Table identifier for "TaskKinds": {856068b1-0e78-4aa8-8e7a-4f53d91a7298}.
   */
  static get TaskKinds(): TaskKindsSchemeInfo {
    return SchemeInfo.taskKinds = SchemeInfo.taskKinds ?? new TaskKindsSchemeInfo();
  }

  private static taskKinds: TaskKindsSchemeInfo;

  /**
   * Table identifier for "Tasks": {5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8}.
   */
  static get Tasks(): TasksSchemeInfo {
    return SchemeInfo.tasks = SchemeInfo.tasks ?? new TasksSchemeInfo();
  }

  private static tasks: TasksSchemeInfo;

  /**
   * Table identifier for "TaskStates": {057a85c8-c20f-430b-bd3b-6ea9f9fb82ee}.
   */
  static get TaskStates(): TaskStatesSchemeInfo {
    return SchemeInfo.taskStates = SchemeInfo.taskStates ?? new TaskStatesSchemeInfo();
  }

  private static taskStates: TaskStatesSchemeInfo;

  /**
   * Table identifier for "TemplateEditRoles": {df057f7e-f59a-4857-b615-19abb650442a}.
   */
  static get TemplateEditRoles(): TemplateEditRolesSchemeInfo {
    return SchemeInfo.templateEditRoles = SchemeInfo.templateEditRoles ?? new TemplateEditRolesSchemeInfo();
  }

  private static templateEditRoles: TemplateEditRolesSchemeInfo;

  /**
   * Table identifier for "TemplateFiles": {fb7c3c18-d7cd-4f16-84e2-33ab0269adbd}.
   */
  static get TemplateFiles(): TemplateFilesSchemeInfo {
    return SchemeInfo.templateFiles = SchemeInfo.templateFiles ?? new TemplateFilesSchemeInfo();
  }

  private static templateFiles: TemplateFilesSchemeInfo;

  /**
   * Table identifier for "TemplateOpenRoles": {831ff542-f2b2-4a3e-9295-0695b843567c}.
   */
  static get TemplateOpenRoles(): TemplateOpenRolesSchemeInfo {
    return SchemeInfo.templateOpenRoles = SchemeInfo.templateOpenRoles ?? new TemplateOpenRolesSchemeInfo();
  }

  private static templateOpenRoles: TemplateOpenRolesSchemeInfo;

  /**
   * Table identifier for "Templates": {9f15aaf8-032c-4222-9c7c-2cfffeee89ed}.
   */
  static get Templates(): TemplatesSchemeInfo {
    return SchemeInfo.templates = SchemeInfo.templates ?? new TemplatesSchemeInfo();
  }

  private static templates: TemplatesSchemeInfo;

  /**
   * Table identifier for "TemplatesVirtual": {b6509d11-f1b3-4f54-b34f-4a996f66c71c}.
   */
  static get TemplatesVirtual(): TemplatesVirtualSchemeInfo {
    return SchemeInfo.templatesVirtual = SchemeInfo.templatesVirtual ?? new TemplatesVirtualSchemeInfo();
  }

  private static templatesVirtual: TemplatesVirtualSchemeInfo;

  /**
   * Table identifier for "TEST_CarAdditionalInfo": {9cbc0f98-571a-4822-a290-3e36b2f2f2e6}.
   */
  static get TEST_CarAdditionalInfo(): TEST_CarAdditionalInfoSchemeInfo {
    return SchemeInfo.tEST_CarAdditionalInfo = SchemeInfo.tEST_CarAdditionalInfo ?? new TEST_CarAdditionalInfoSchemeInfo();
  }

  private static tEST_CarAdditionalInfo: TEST_CarAdditionalInfoSchemeInfo;

  /**
   * Table identifier for "TEST_CarCustomers": {30295c44-c633-4474-9e30-4492e75e7e75}.
   */
  static get TEST_CarCustomers(): TEST_CarCustomersSchemeInfo {
    return SchemeInfo.tEST_CarCustomers = SchemeInfo.tEST_CarCustomers ?? new TEST_CarCustomersSchemeInfo();
  }

  private static tEST_CarCustomers: TEST_CarCustomersSchemeInfo;

  /**
   * Table identifier for "TEST_CarMainInfo": {509d961f-00cf-4403-a78f-6736841de448}.
   */
  static get TEST_CarMainInfo(): TEST_CarMainInfoSchemeInfo {
    return SchemeInfo.tEST_CarMainInfo = SchemeInfo.tEST_CarMainInfo ?? new TEST_CarMainInfoSchemeInfo();
  }

  private static tEST_CarMainInfo: TEST_CarMainInfoSchemeInfo;

  /**
   * Table identifier for "TEST_CarOwners": {2cff79c9-6e5a-4e98-8c8f-7a14eb7bec80}.
   */
  static get TEST_CarOwners(): TEST_CarOwnersSchemeInfo {
    return SchemeInfo.tEST_CarOwners = SchemeInfo.tEST_CarOwners ?? new TEST_CarOwnersSchemeInfo();
  }

  private static tEST_CarOwners: TEST_CarOwnersSchemeInfo;

  /**
   * Table identifier for "TEST_CarSales": {6dc3a829-b1f4-4e67-ba99-16a30fe91209}.
   */
  static get TEST_CarSales(): TEST_CarSalesSchemeInfo {
    return SchemeInfo.tEST_CarSales = SchemeInfo.tEST_CarSales ?? new TEST_CarSalesSchemeInfo();
  }

  private static tEST_CarSales: TEST_CarSalesSchemeInfo;

  /**
   * Table identifier for "TEST_CustomerOperations": {7f813c98-3331-46a9-8aa0-bab55a956246}.
   */
  static get TEST_CustomerOperations(): TEST_CustomerOperationsSchemeInfo {
    return SchemeInfo.tEST_CustomerOperations = SchemeInfo.tEST_CustomerOperations ?? new TEST_CustomerOperationsSchemeInfo();
  }

  private static tEST_CustomerOperations: TEST_CustomerOperationsSchemeInfo;

  /**
   * Table identifier for "TileSizes": {9d1fb4ee-fa51-4926-8abb-c464ca91e450}.
   */
  static get TileSizes(): TileSizesSchemeInfo {
    return SchemeInfo.tileSizes = SchemeInfo.tileSizes ?? new TileSizesSchemeInfo();
  }

  private static tileSizes: TileSizesSchemeInfo;

  /**
   * Table identifier for "TimeZones": {984e22bf-78fc-4c69-b1a6-ca73341c36ea}.
   */
  static get TimeZones(): TimeZonesSchemeInfo {
    return SchemeInfo.timeZones = SchemeInfo.timeZones ?? new TimeZonesSchemeInfo();
  }

  private static timeZones: TimeZonesSchemeInfo;

  /**
   * Table identifier for "TimeZonesSettings": {44e8b6f2-f7d1-48ff-a3f2-599bf76e5180}.
   */
  static get TimeZonesSettings(): TimeZonesSettingsSchemeInfo {
    return SchemeInfo.timeZonesSettings = SchemeInfo.timeZonesSettings ?? new TimeZonesSettingsSchemeInfo();
  }

  private static timeZonesSettings: TimeZonesSettingsSchemeInfo;

  /**
   * Table identifier for "TimeZonesVirtual": {3e09239e-ebb7-4b0a-a4e1-51eae83e3c0c}.
   */
  static get TimeZonesVirtual(): TimeZonesVirtualSchemeInfo {
    return SchemeInfo.timeZonesVirtual = SchemeInfo.timeZonesVirtual ?? new TimeZonesVirtualSchemeInfo();
  }

  private static timeZonesVirtual: TimeZonesVirtualSchemeInfo;

  /**
   * Table identifier for "Types": {b0538ece-8468-4d0b-8b4e-5a1d43e024db}.
   */
  static get Types(): TypesSchemeInfo {
    return SchemeInfo.types = SchemeInfo.types ?? new TypesSchemeInfo();
  }

  private static types: TypesSchemeInfo;

  /**
   * Table identifier for "UserSettingsFunctionRolesVirtual": {0e7d4c80-0a90-40a6-86a7-01ec32c80ba9}.
   */
  static get UserSettingsFunctionRolesVirtual(): UserSettingsFunctionRolesVirtualSchemeInfo {
    return SchemeInfo.userSettingsFunctionRolesVirtual = SchemeInfo.userSettingsFunctionRolesVirtual ?? new UserSettingsFunctionRolesVirtualSchemeInfo();
  }

  private static userSettingsFunctionRolesVirtual: UserSettingsFunctionRolesVirtualSchemeInfo;

  /**
   * Table identifier for "UserSettingsVirtual": {3c8a5e77-c4da-45f5-b974-170af387ce26}.
   */
  static get UserSettingsVirtual(): UserSettingsVirtualSchemeInfo {
    return SchemeInfo.userSettingsVirtual = SchemeInfo.userSettingsVirtual ?? new UserSettingsVirtualSchemeInfo();
  }

  private static userSettingsVirtual: UserSettingsVirtualSchemeInfo;

  /**
   * Table identifier for "VatTypes": {8dd87520-9d83-4d8a-8c60-c1275328c5e8}.
   */
  static get VatTypes(): VatTypesSchemeInfo {
    return SchemeInfo.vatTypes = SchemeInfo.vatTypes ?? new VatTypesSchemeInfo();
  }

  private static vatTypes: VatTypesSchemeInfo;

  /**
   * Table identifier for "ViewRoles": {5a5dc5fe-19e1-4c69-b084-d6db36aa5a23}.
   */
  static get ViewRoles(): ViewRolesSchemeInfo {
    return SchemeInfo.viewRoles = SchemeInfo.viewRoles ?? new ViewRolesSchemeInfo();
  }

  private static viewRoles: ViewRolesSchemeInfo;

  /**
   * Table identifier for "ViewRolesVirtual": {08fccef5-fe25-4f3b-9a8c-2291b6a60209}.
   */
  static get ViewRolesVirtual(): ViewRolesVirtualSchemeInfo {
    return SchemeInfo.viewRolesVirtual = SchemeInfo.viewRolesVirtual ?? new ViewRolesVirtualSchemeInfo();
  }

  private static viewRolesVirtual: ViewRolesVirtualSchemeInfo;

  /**
   * Table identifier for "Views": {3519b63c-eea0-48f4-b70a-544e58ece5fc}.
   */
  static get Views(): ViewsSchemeInfo {
    return SchemeInfo.views = SchemeInfo.views ?? new ViewsSchemeInfo();
  }

  private static views: ViewsSchemeInfo;

  /**
   * Table identifier for "ViewsVirtual": {cefba5f8-8b2c-4be0-ba24-564f3a474240}.
   */
  static get ViewsVirtual(): ViewsVirtualSchemeInfo {
    return SchemeInfo.viewsVirtual = SchemeInfo.viewsVirtual ?? new ViewsVirtualSchemeInfo();
  }

  private static viewsVirtual: ViewsVirtualSchemeInfo;

  /**
   * Table identifier for "WeAddFileFromTemplateAction": {93d11813-4967-458e-b3be-f7da367a8872}.
   */
  static get WeAddFileFromTemplateAction(): WeAddFileFromTemplateActionSchemeInfo {
    return SchemeInfo.weAddFileFromTemplateAction = SchemeInfo.weAddFileFromTemplateAction ?? new WeAddFileFromTemplateActionSchemeInfo();
  }

  private static weAddFileFromTemplateAction: WeAddFileFromTemplateActionSchemeInfo;

  /**
   * Table identifier for "WebApplications": {610d8253-e293-4676-abcb-e7a0ac1a084d}.
   */
  static get WebApplications(): WebApplicationsSchemeInfo {
    return SchemeInfo.webApplications = SchemeInfo.webApplications ?? new WebApplicationsSchemeInfo();
  }

  private static webApplications: WebApplicationsSchemeInfo;

  /**
   * Table identifier for "WebClientRoles": {383321f7-c432-42d8-84f9-e4f58e0cb021}.
   */
  static get WebClientRoles(): WebClientRolesSchemeInfo {
    return SchemeInfo.webClientRoles = SchemeInfo.webClientRoles ?? new WebClientRolesSchemeInfo();
  }

  private static webClientRoles: WebClientRolesSchemeInfo;

  /**
   * Table identifier for "WeCommandAction": {2dc38a34-3451-4ea9-9885-9afa15155612}.
   */
  static get WeCommandAction(): WeCommandActionSchemeInfo {
    return SchemeInfo.weCommandAction = SchemeInfo.weCommandAction ?? new WeCommandActionSchemeInfo();
  }

  private static weCommandAction: WeCommandActionSchemeInfo;

  /**
   * Table identifier for "WeCommandActionLinks": {97e973ba-8fa9-4e3d-96d3-2f077ca11531}.
   */
  static get WeCommandActionLinks(): WeCommandActionLinksSchemeInfo {
    return SchemeInfo.weCommandActionLinks = SchemeInfo.weCommandActionLinks ?? new WeCommandActionLinksSchemeInfo();
  }

  private static weCommandActionLinks: WeCommandActionLinksSchemeInfo;

  /**
   * Table identifier for "WeConditionAction": {ad4abe1f-9f6b-4842-b8d5-bb34502c7dce}.
   */
  static get WeConditionAction(): WeConditionActionSchemeInfo {
    return SchemeInfo.weConditionAction = SchemeInfo.weConditionAction ?? new WeConditionActionSchemeInfo();
  }

  private static weConditionAction: WeConditionActionSchemeInfo;

  /**
   * Table identifier for "WeDialogAction": {5aac25fd-de4f-450d-9fd5-a1a9168a795c}.
   */
  static get WeDialogAction(): WeDialogActionSchemeInfo {
    return SchemeInfo.weDialogAction = SchemeInfo.weDialogAction ?? new WeDialogActionSchemeInfo();
  }

  private static weDialogAction: WeDialogActionSchemeInfo;

  /**
   * Table identifier for "WeDialogActionButtonLinks": {57f61e17-bd87-48cb-8efc-7a7dc56f2eef}.
   */
  static get WeDialogActionButtonLinks(): WeDialogActionButtonLinksSchemeInfo {
    return SchemeInfo.weDialogActionButtonLinks = SchemeInfo.weDialogActionButtonLinks ?? new WeDialogActionButtonLinksSchemeInfo();
  }

  private static weDialogActionButtonLinks: WeDialogActionButtonLinksSchemeInfo;

  /**
   * Table identifier for "WeDialogActionButtons": {a99b285f-80c3-442a-85a6-2a3bfd645d2b}.
   */
  static get WeDialogActionButtons(): WeDialogActionButtonsSchemeInfo {
    return SchemeInfo.weDialogActionButtons = SchemeInfo.weDialogActionButtons ?? new WeDialogActionButtonsSchemeInfo();
  }

  private static weDialogActionButtons: WeDialogActionButtonsSchemeInfo;

  /**
   * Table identifier for "WeEmailAction": {3482fa35-9558-4a7c-832f-3ac94c73f2f9}.
   */
  static get WeEmailAction(): WeEmailActionSchemeInfo {
    return SchemeInfo.weEmailAction = SchemeInfo.weEmailAction ?? new WeEmailActionSchemeInfo();
  }

  private static weEmailAction: WeEmailActionSchemeInfo;

  /**
   * Table identifier for "WeEmailActionOptionalRecipients": {94b08bf8-6cb2-4a11-a42b-4ff996ac71e5}.
   */
  static get WeEmailActionOptionalRecipients(): WeEmailActionOptionalRecipientsSchemeInfo {
    return SchemeInfo.weEmailActionOptionalRecipients = SchemeInfo.weEmailActionOptionalRecipients ?? new WeEmailActionOptionalRecipientsSchemeInfo();
  }

  private static weEmailActionOptionalRecipients: WeEmailActionOptionalRecipientsSchemeInfo;

  /**
   * Table identifier for "WeEmailActionRecievers": {48d261cd-1054-41d7-9046-485e22d15060}.
   */
  static get WeEmailActionRecievers(): WeEmailActionRecieversSchemeInfo {
    return SchemeInfo.weEmailActionRecievers = SchemeInfo.weEmailActionRecievers ?? new WeEmailActionRecieversSchemeInfo();
  }

  private static weEmailActionRecievers: WeEmailActionRecieversSchemeInfo;

  /**
   * Table identifier for "WeEndAction": {e36e23ae-2276-494a-a3f1-5f3cd5c56f9d}.
   */
  static get WeEndAction(): WeEndActionSchemeInfo {
    return SchemeInfo.weEndAction = SchemeInfo.weEndAction ?? new WeEndActionSchemeInfo();
  }

  private static weEndAction: WeEndActionSchemeInfo;

  /**
   * Table identifier for "WeHistoryManagementAction": {bb018cba-ef03-4bb4-a7e6-8fb083fc44a4}.
   */
  static get WeHistoryManagementAction(): WeHistoryManagementActionSchemeInfo {
    return SchemeInfo.weHistoryManagementAction = SchemeInfo.weHistoryManagementAction ?? new WeHistoryManagementActionSchemeInfo();
  }

  private static weHistoryManagementAction: WeHistoryManagementActionSchemeInfo;

  /**
   * Table identifier for "WeScriptAction": {46f88520-33d0-45c9-bd27-20cae8fa58dc}.
   */
  static get WeScriptAction(): WeScriptActionSchemeInfo {
    return SchemeInfo.weScriptAction = SchemeInfo.weScriptAction ?? new WeScriptActionSchemeInfo();
  }

  private static weScriptAction: WeScriptActionSchemeInfo;

  /**
   * Table identifier for "WeSendSignalAction": {fbe60ad7-091a-4f09-a57a-f1068088fa38}.
   */
  static get WeSendSignalAction(): WeSendSignalActionSchemeInfo {
    return SchemeInfo.weSendSignalAction = SchemeInfo.weSendSignalAction ?? new WeSendSignalActionSchemeInfo();
  }

  private static weSendSignalAction: WeSendSignalActionSchemeInfo;

  /**
   * Table identifier for "WeStartAction": {fff6d6ad-c17e-4692-863d-07032f4b95fd}.
   */
  static get WeStartAction(): WeStartActionSchemeInfo {
    return SchemeInfo.weStartAction = SchemeInfo.weStartAction ?? new WeStartActionSchemeInfo();
  }

  private static weStartAction: WeStartActionSchemeInfo;

  /**
   * Table identifier for "WeSubprocessAction": {3d947708-6196-443f-a4e3-a1e1a5315d9d}.
   */
  static get WeSubprocessAction(): WeSubprocessActionSchemeInfo {
    return SchemeInfo.weSubprocessAction = SchemeInfo.weSubprocessAction ?? new WeSubprocessActionSchemeInfo();
  }

  private static weSubprocessAction: WeSubprocessActionSchemeInfo;

  /**
   * Table identifier for "WeSubprocessActionEndMapping": {ea4cd339-7a97-4221-a223-44f9b6ce6ce1}.
   */
  static get WeSubprocessActionEndMapping(): WeSubprocessActionEndMappingSchemeInfo {
    return SchemeInfo.weSubprocessActionEndMapping = SchemeInfo.weSubprocessActionEndMapping ?? new WeSubprocessActionEndMappingSchemeInfo();
  }

  private static weSubprocessActionEndMapping: WeSubprocessActionEndMappingSchemeInfo;

  /**
   * Table identifier for "WeSubprocessActionOptions": {428f3b30-561c-446e-b676-4ec84ba8e03a}.
   */
  static get WeSubprocessActionOptions(): WeSubprocessActionOptionsSchemeInfo {
    return SchemeInfo.weSubprocessActionOptions = SchemeInfo.weSubprocessActionOptions ?? new WeSubprocessActionOptionsSchemeInfo();
  }

  private static weSubprocessActionOptions: WeSubprocessActionOptionsSchemeInfo;

  /**
   * Table identifier for "WeSubprocessActionStartMapping": {a2b54bf4-20ae-4fdd-8b2e-21ef246cfb32}.
   */
  static get WeSubprocessActionStartMapping(): WeSubprocessActionStartMappingSchemeInfo {
    return SchemeInfo.weSubprocessActionStartMapping = SchemeInfo.weSubprocessActionStartMapping ?? new WeSubprocessActionStartMappingSchemeInfo();
  }

  private static weSubprocessActionStartMapping: WeSubprocessActionStartMappingSchemeInfo;

  /**
   * Table identifier for "WeSubprocessControlAction": {2f0a4a5d-6601-4cd9-9c2d-09d193d33352}.
   */
  static get WeSubprocessControlAction(): WeSubprocessControlActionSchemeInfo {
    return SchemeInfo.weSubprocessControlAction = SchemeInfo.weSubprocessControlAction ?? new WeSubprocessControlActionSchemeInfo();
  }

  private static weSubprocessControlAction: WeSubprocessControlActionSchemeInfo;

  /**
   * Table identifier for "WeTaskAction": {ffcaed62-a85f-43b0-b029-ed50bc562ef1}.
   */
  static get WeTaskAction(): WeTaskActionSchemeInfo {
    return SchemeInfo.weTaskAction = SchemeInfo.weTaskAction ?? new WeTaskActionSchemeInfo();
  }

  private static weTaskAction: WeTaskActionSchemeInfo;

  /**
   * Table identifier for "WeTaskActionDialogs": {7c068441-e9e1-445a-a371-bf9436156428}.
   */
  static get WeTaskActionDialogs(): WeTaskActionDialogsSchemeInfo {
    return SchemeInfo.weTaskActionDialogs = SchemeInfo.weTaskActionDialogs ?? new WeTaskActionDialogsSchemeInfo();
  }

  private static weTaskActionDialogs: WeTaskActionDialogsSchemeInfo;

  /**
   * Table identifier for "WeTaskActionEvents": {797022e2-bac4-408c-b529-110943fade63}.
   */
  static get WeTaskActionEvents(): WeTaskActionEventsSchemeInfo {
    return SchemeInfo.weTaskActionEvents = SchemeInfo.weTaskActionEvents ?? new WeTaskActionEventsSchemeInfo();
  }

  private static weTaskActionEvents: WeTaskActionEventsSchemeInfo;

  /**
   * Table identifier for "WeTaskActionNotificationRoles": {9b7fc0b0-da06-46df-a5c9-d66ecc386d55}.
   */
  static get WeTaskActionNotificationRoles(): WeTaskActionNotificationRolesSchemeInfo {
    return SchemeInfo.weTaskActionNotificationRoles = SchemeInfo.weTaskActionNotificationRoles ?? new WeTaskActionNotificationRolesSchemeInfo();
  }

  private static weTaskActionNotificationRoles: WeTaskActionNotificationRolesSchemeInfo;

  /**
   * Table identifier for "WeTaskActionOptionLinks": {a3d3bf40-b37a-4118-af51-2b555da511b7}.
   */
  static get WeTaskActionOptionLinks(): WeTaskActionOptionLinksSchemeInfo {
    return SchemeInfo.weTaskActionOptionLinks = SchemeInfo.weTaskActionOptionLinks ?? new WeTaskActionOptionLinksSchemeInfo();
  }

  private static weTaskActionOptionLinks: WeTaskActionOptionLinksSchemeInfo;

  /**
   * Table identifier for "WeTaskActionOptions": {e30dcb0a-2a63-4f52-82f9-a12b0038d70d}.
   */
  static get WeTaskActionOptions(): WeTaskActionOptionsSchemeInfo {
    return SchemeInfo.weTaskActionOptions = SchemeInfo.weTaskActionOptions ?? new WeTaskActionOptionsSchemeInfo();
  }

  private static weTaskActionOptions: WeTaskActionOptionsSchemeInfo;

  /**
   * Table identifier for "WeTaskControlAction": {adcf3458-d724-4411-9059-60bdb353a9b5}.
   */
  static get WeTaskControlAction(): WeTaskControlActionSchemeInfo {
    return SchemeInfo.weTaskControlAction = SchemeInfo.weTaskControlAction ?? new WeTaskControlActionSchemeInfo();
  }

  private static weTaskControlAction: WeTaskControlActionSchemeInfo;

  /**
   * Table identifier for "WeTaskControlTypes": {ab612473-e0a2-4dd7-b05e-d9bbdf06b62f}.
   */
  static get WeTaskControlTypes(): WeTaskControlTypesSchemeInfo {
    return SchemeInfo.weTaskControlTypes = SchemeInfo.weTaskControlTypes ?? new WeTaskControlTypesSchemeInfo();
  }

  private static weTaskControlTypes: WeTaskControlTypesSchemeInfo;

  /**
   * Table identifier for "WeTaskGroupAction": {915d8549-af3d-4d44-84a1-cef16ed89941}.
   */
  static get WeTaskGroupAction(): WeTaskGroupActionSchemeInfo {
    return SchemeInfo.weTaskGroupAction = SchemeInfo.weTaskGroupAction ?? new WeTaskGroupActionSchemeInfo();
  }

  private static weTaskGroupAction: WeTaskGroupActionSchemeInfo;

  /**
   * Table identifier for "WeTaskGroupActionOptionLinks": {1e26efb8-a6ee-4582-9ac3-88da4ef74d24}.
   */
  static get WeTaskGroupActionOptionLinks(): WeTaskGroupActionOptionLinksSchemeInfo {
    return SchemeInfo.weTaskGroupActionOptionLinks = SchemeInfo.weTaskGroupActionOptionLinks ?? new WeTaskGroupActionOptionLinksSchemeInfo();
  }

  private static weTaskGroupActionOptionLinks: WeTaskGroupActionOptionLinksSchemeInfo;

  /**
   * Table identifier for "WeTaskGroupActionOptions": {dee05376-8267-42b9-8cc9-1ff5bb58bb06}.
   */
  static get WeTaskGroupActionOptions(): WeTaskGroupActionOptionsSchemeInfo {
    return SchemeInfo.weTaskGroupActionOptions = SchemeInfo.weTaskGroupActionOptions ?? new WeTaskGroupActionOptionsSchemeInfo();
  }

  private static weTaskGroupActionOptions: WeTaskGroupActionOptionsSchemeInfo;

  /**
   * Table identifier for "WeTaskGroupActionOptionTypes": {dc9eb404-c42d-40ab-a4c0-3b8b6089b926}.
   */
  static get WeTaskGroupActionOptionTypes(): WeTaskGroupActionOptionTypesSchemeInfo {
    return SchemeInfo.weTaskGroupActionOptionTypes = SchemeInfo.weTaskGroupActionOptionTypes ?? new WeTaskGroupActionOptionTypesSchemeInfo();
  }

  private static weTaskGroupActionOptionTypes: WeTaskGroupActionOptionTypesSchemeInfo;

  /**
   * Table identifier for "WeTaskGroupActionRoles": {0656f18d-bb1c-47c9-8d40-24300c7f4b53}.
   */
  static get WeTaskGroupActionRoles(): WeTaskGroupActionRolesSchemeInfo {
    return SchemeInfo.weTaskGroupActionRoles = SchemeInfo.weTaskGroupActionRoles ?? new WeTaskGroupActionRolesSchemeInfo();
  }

  private static weTaskGroupActionRoles: WeTaskGroupActionRolesSchemeInfo;

  /**
   * Table identifier for "WeTaskGroupControlAction": {02a2a16e-7915-4a03-86b0-08b074b78c67}.
   */
  static get WeTaskGroupControlAction(): WeTaskGroupControlActionSchemeInfo {
    return SchemeInfo.weTaskGroupControlAction = SchemeInfo.weTaskGroupControlAction ?? new WeTaskGroupControlActionSchemeInfo();
  }

  private static weTaskGroupControlAction: WeTaskGroupControlActionSchemeInfo;

  /**
   * Table identifier for "WeTimerAction": {318965a6-fcec-432d-8ba3-3b972fb2b750}.
   */
  static get WeTimerAction(): WeTimerActionSchemeInfo {
    return SchemeInfo.weTimerAction = SchemeInfo.weTimerAction ?? new WeTimerActionSchemeInfo();
  }

  private static weTimerAction: WeTimerActionSchemeInfo;

  /**
   * Table identifier for "WeTimerControlAction": {38c4dd25-e26e-469c-8072-6498f33a0d06}.
   */
  static get WeTimerControlAction(): WeTimerControlActionSchemeInfo {
    return SchemeInfo.weTimerControlAction = SchemeInfo.weTimerControlAction ?? new WeTimerControlActionSchemeInfo();
  }

  private static weTimerControlAction: WeTimerControlActionSchemeInfo;

  /**
   * Table identifier for "WfResolutionChildren": {d4f683a4-a1e9-4fc1-ae84-2c4ab304b7fb}.
   */
  static get WfResolutionChildren(): WfResolutionChildrenSchemeInfo {
    return SchemeInfo.wfResolutionChildren = SchemeInfo.wfResolutionChildren ?? new WfResolutionChildrenSchemeInfo();
  }

  private static wfResolutionChildren: WfResolutionChildrenSchemeInfo;

  /**
   * Table identifier for "WfResolutionChildrenVirtual": {17dcbbe4-108a-4f15-8716-f7d2718f0953}.
   */
  static get WfResolutionChildrenVirtual(): WfResolutionChildrenVirtualSchemeInfo {
    return SchemeInfo.wfResolutionChildrenVirtual = SchemeInfo.wfResolutionChildrenVirtual ?? new WfResolutionChildrenVirtualSchemeInfo();
  }

  private static wfResolutionChildrenVirtual: WfResolutionChildrenVirtualSchemeInfo;

  /**
   * Table identifier for "WfResolutionPerformers": {0f62f90e-6b94-4301-866d-0138fb147939}.
   */
  static get WfResolutionPerformers(): WfResolutionPerformersSchemeInfo {
    return SchemeInfo.wfResolutionPerformers = SchemeInfo.wfResolutionPerformers ?? new WfResolutionPerformersSchemeInfo();
  }

  private static wfResolutionPerformers: WfResolutionPerformersSchemeInfo;

  /**
   * Table identifier for "WfResolutions": {6a0f5914-6a44-4e7d-b400-6b82ec1e2209}.
   */
  static get WfResolutions(): WfResolutionsSchemeInfo {
    return SchemeInfo.wfResolutions = SchemeInfo.wfResolutions ?? new WfResolutionsSchemeInfo();
  }

  private static wfResolutions: WfResolutionsSchemeInfo;

  /**
   * Table identifier for "WfResolutionsVirtual": {1f805af5-f412-4878-9d70-af989b905fb5}.
   */
  static get WfResolutionsVirtual(): WfResolutionsVirtualSchemeInfo {
    return SchemeInfo.wfResolutionsVirtual = SchemeInfo.wfResolutionsVirtual ?? new WfResolutionsVirtualSchemeInfo();
  }

  private static wfResolutionsVirtual: WfResolutionsVirtualSchemeInfo;

  /**
   * Table identifier for "WfSatellite": {05394727-2b6f-4d59-9900-d95bc8effdc5}.
   */
  static get WfSatellite(): WfSatelliteSchemeInfo {
    return SchemeInfo.wfSatellite = SchemeInfo.wfSatellite ?? new WfSatelliteSchemeInfo();
  }

  private static wfSatellite: WfSatelliteSchemeInfo;

  /**
   * Table identifier for "WfSatelliteTaskHistory": {cd241343-4eb1-425f-b534-f9ff4cfa597e}.
   */
  static get WfSatelliteTaskHistory(): WfSatelliteTaskHistorySchemeInfo {
    return SchemeInfo.wfSatelliteTaskHistory = SchemeInfo.wfSatelliteTaskHistory ?? new WfSatelliteTaskHistorySchemeInfo();
  }

  private static wfSatelliteTaskHistory: WfSatelliteTaskHistorySchemeInfo;

  /**
   * Table identifier for "WfTaskCardsVirtual": {ef5f3db3-95d9-4654-91a4-87dcd3d2195a}.
   */
  static get WfTaskCardsVirtual(): WfTaskCardsVirtualSchemeInfo {
    return SchemeInfo.wfTaskCardsVirtual = SchemeInfo.wfTaskCardsVirtual ?? new WfTaskCardsVirtualSchemeInfo();
  }

  private static wfTaskCardsVirtual: WfTaskCardsVirtualSchemeInfo;

  /**
   * Table identifier for "WorkflowActions": {df81680b-406f-4f50-9df2-c14dda232aea}.
   */
  static get WorkflowActions(): WorkflowActionsSchemeInfo {
    return SchemeInfo.workflowActions = SchemeInfo.workflowActions ?? new WorkflowActionsSchemeInfo();
  }

  private static workflowActions: WorkflowActionsSchemeInfo;

  /**
   * Table identifier for "WorkflowCounters": {7adfd330-ab0e-458f-9ac4-f2060bde8c97}.
   */
  static get WorkflowCounters(): WorkflowCountersSchemeInfo {
    return SchemeInfo.workflowCounters = SchemeInfo.workflowCounters ?? new WorkflowCountersSchemeInfo();
  }

  private static workflowCounters: WorkflowCountersSchemeInfo;

  /**
   * Table identifier for "WorkflowDefaultSubscriptions": {d8b78ce3-bedf-4faa-9fba-75ddbecf4e04}.
   */
  static get WorkflowDefaultSubscriptions(): WorkflowDefaultSubscriptionsSchemeInfo {
    return SchemeInfo.workflowDefaultSubscriptions = SchemeInfo.workflowDefaultSubscriptions ?? new WorkflowDefaultSubscriptionsSchemeInfo();
  }

  private static workflowDefaultSubscriptions: WorkflowDefaultSubscriptionsSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineCheckContextRole": {7d0b5402-9d55-4269-964d-25b5ddcb2690}.
   */
  static get WorkflowEngineCheckContextRole(): WorkflowEngineCheckContextRoleSchemeInfo {
    return SchemeInfo.workflowEngineCheckContextRole = SchemeInfo.workflowEngineCheckContextRole ?? new WorkflowEngineCheckContextRoleSchemeInfo();
  }

  private static workflowEngineCheckContextRole: WorkflowEngineCheckContextRoleSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineCommandSubscriptions": {7c45a604-9175-45bd-8525-f218a465b77b}.
   */
  static get WorkflowEngineCommandSubscriptions(): WorkflowEngineCommandSubscriptionsSchemeInfo {
    return SchemeInfo.workflowEngineCommandSubscriptions = SchemeInfo.workflowEngineCommandSubscriptions ?? new WorkflowEngineCommandSubscriptionsSchemeInfo();
  }

  private static workflowEngineCommandSubscriptions: WorkflowEngineCommandSubscriptionsSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineErrors": {61905471-1e69-4478-946f-772f11152386}.
   */
  static get WorkflowEngineErrors(): WorkflowEngineErrorsSchemeInfo {
    return SchemeInfo.workflowEngineErrors = SchemeInfo.workflowEngineErrors ?? new WorkflowEngineErrorsSchemeInfo();
  }

  private static workflowEngineErrors: WorkflowEngineErrorsSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineLogLevels": {9d29f065-3c4b-4209-af8d-10b699895231}.
   */
  static get WorkflowEngineLogLevels(): WorkflowEngineLogLevelsSchemeInfo {
    return SchemeInfo.workflowEngineLogLevels = SchemeInfo.workflowEngineLogLevels ?? new WorkflowEngineLogLevelsSchemeInfo();
  }

  private static workflowEngineLogLevels: WorkflowEngineLogLevelsSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineLogs": {f3fa0390-6444-4df6-8be5-cbad1fdd153e}.
   */
  static get WorkflowEngineLogs(): WorkflowEngineLogsSchemeInfo {
    return SchemeInfo.workflowEngineLogs = SchemeInfo.workflowEngineLogs ?? new WorkflowEngineLogsSchemeInfo();
  }

  private static workflowEngineLogs: WorkflowEngineLogsSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineNodes": {69f72d3a-97c1-4d67-a348-071ab861b3c7}.
   */
  static get WorkflowEngineNodes(): WorkflowEngineNodesSchemeInfo {
    return SchemeInfo.workflowEngineNodes = SchemeInfo.workflowEngineNodes ?? new WorkflowEngineNodesSchemeInfo();
  }

  private static workflowEngineNodes: WorkflowEngineNodesSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineProcesses": {27debe30-ae5f-4f69-89c9-5706e1592540}.
   */
  static get WorkflowEngineProcesses(): WorkflowEngineProcessesSchemeInfo {
    return SchemeInfo.workflowEngineProcesses = SchemeInfo.workflowEngineProcesses ?? new WorkflowEngineProcessesSchemeInfo();
  }

  private static workflowEngineProcesses: WorkflowEngineProcessesSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineSettingsAdminRoles": {e493b168-0c0a-4ebc-812e-229bc43aec25}.
   */
  static get WorkflowEngineSettingsAdminRoles(): WorkflowEngineSettingsAdminRolesSchemeInfo {
    return SchemeInfo.workflowEngineSettingsAdminRoles = SchemeInfo.workflowEngineSettingsAdminRoles ?? new WorkflowEngineSettingsAdminRolesSchemeInfo();
  }

  private static workflowEngineSettingsAdminRoles: WorkflowEngineSettingsAdminRolesSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineSettingsCreateRoles": {9097ff9c-04a8-4af1-8921-7df7323d46f4}.
   */
  static get WorkflowEngineSettingsCreateRoles(): WorkflowEngineSettingsCreateRolesSchemeInfo {
    return SchemeInfo.workflowEngineSettingsCreateRoles = SchemeInfo.workflowEngineSettingsCreateRoles ?? new WorkflowEngineSettingsCreateRolesSchemeInfo();
  }

  private static workflowEngineSettingsCreateRoles: WorkflowEngineSettingsCreateRolesSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineSettingsObjectTypeFields": {6efad92a-46be-4d44-a495-271b264f016b}.
   */
  static get WorkflowEngineSettingsObjectTypeFields(): WorkflowEngineSettingsObjectTypeFieldsSchemeInfo {
    return SchemeInfo.workflowEngineSettingsObjectTypeFields = SchemeInfo.workflowEngineSettingsObjectTypeFields ?? new WorkflowEngineSettingsObjectTypeFieldsSchemeInfo();
  }

  private static workflowEngineSettingsObjectTypeFields: WorkflowEngineSettingsObjectTypeFieldsSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineSettingsObjectTypes": {140a411c-3b68-44c0-8a5b-cf641d2421f2}.
   */
  static get WorkflowEngineSettingsObjectTypes(): WorkflowEngineSettingsObjectTypesSchemeInfo {
    return SchemeInfo.workflowEngineSettingsObjectTypes = SchemeInfo.workflowEngineSettingsObjectTypes ?? new WorkflowEngineSettingsObjectTypesSchemeInfo();
  }

  private static workflowEngineSettingsObjectTypes: WorkflowEngineSettingsObjectTypesSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineSubprocessSubscriptions": {1c83c672-7de7-47f5-8c63-ec41bb5aa7ca}.
   */
  static get WorkflowEngineSubprocessSubscriptions(): WorkflowEngineSubprocessSubscriptionsSchemeInfo {
    return SchemeInfo.workflowEngineSubprocessSubscriptions = SchemeInfo.workflowEngineSubprocessSubscriptions ?? new WorkflowEngineSubprocessSubscriptionsSchemeInfo();
  }

  private static workflowEngineSubprocessSubscriptions: WorkflowEngineSubprocessSubscriptionsSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineTaskActions": {857ef2b9-6bdb-4913-bbc2-2cf9d1ae0b55}.
   */
  static get WorkflowEngineTaskActions(): WorkflowEngineTaskActionsSchemeInfo {
    return SchemeInfo.workflowEngineTaskActions = SchemeInfo.workflowEngineTaskActions ?? new WorkflowEngineTaskActionsSchemeInfo();
  }

  private static workflowEngineTaskActions: WorkflowEngineTaskActionsSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineTaskSubscriptions": {5ee285b4-a72c-4a41-88a1-3e052fa1ee44}.
   */
  static get WorkflowEngineTaskSubscriptions(): WorkflowEngineTaskSubscriptionsSchemeInfo {
    return SchemeInfo.workflowEngineTaskSubscriptions = SchemeInfo.workflowEngineTaskSubscriptions ?? new WorkflowEngineTaskSubscriptionsSchemeInfo();
  }

  private static workflowEngineTaskSubscriptions: WorkflowEngineTaskSubscriptionsSchemeInfo;

  /**
   * Table identifier for "WorkflowEngineTimerSubscriptions": {9c65ad25-7d88-4e7c-b398-26d45d1d7204}.
   */
  static get WorkflowEngineTimerSubscriptions(): WorkflowEngineTimerSubscriptionsSchemeInfo {
    return SchemeInfo.workflowEngineTimerSubscriptions = SchemeInfo.workflowEngineTimerSubscriptions ?? new WorkflowEngineTimerSubscriptionsSchemeInfo();
  }

  private static workflowEngineTimerSubscriptions: WorkflowEngineTimerSubscriptionsSchemeInfo;

  /**
   * Table identifier for "WorkflowInLinks": {83bf8e43-0292-4fb8-ac1d-6e36c8ba99a6}.
   */
  static get WorkflowInLinks(): WorkflowInLinksSchemeInfo {
    return SchemeInfo.workflowInLinks = SchemeInfo.workflowInLinks ?? new WorkflowInLinksSchemeInfo();
  }

  private static workflowInLinks: WorkflowInLinksSchemeInfo;

  /**
   * Table identifier for "WorkflowLinkModes": {29b2fb61-6880-43de-a40f-6688e1d0e247}.
   */
  static get WorkflowLinkModes(): WorkflowLinkModesSchemeInfo {
    return SchemeInfo.workflowLinkModes = SchemeInfo.workflowLinkModes ?? new WorkflowLinkModesSchemeInfo();
  }

  private static workflowLinkModes: WorkflowLinkModesSchemeInfo;

  /**
   * Table identifier for "WorkflowLinks": {9764baef-636c-4558-86cb-0b7e4360f771}.
   */
  static get WorkflowLinks(): WorkflowLinksSchemeInfo {
    return SchemeInfo.workflowLinks = SchemeInfo.workflowLinks ?? new WorkflowLinksSchemeInfo();
  }

  private static workflowLinks: WorkflowLinksSchemeInfo;

  /**
   * Table identifier for "WorkflowMain": {87f7e0c3-2d97-4e36-bb14-1aeec6e67a94}.
   */
  static get WorkflowMain(): WorkflowMainSchemeInfo {
    return SchemeInfo.workflowMain = SchemeInfo.workflowMain ?? new WorkflowMainSchemeInfo();
  }

  private static workflowMain: WorkflowMainSchemeInfo;

  /**
   * Table identifier for "WorkflowNodeInstances": {e2eda913-f68f-4d42-88ba-25f80bd4c3e5}.
   */
  static get WorkflowNodeInstances(): WorkflowNodeInstancesSchemeInfo {
    return SchemeInfo.workflowNodeInstances = SchemeInfo.workflowNodeInstances ?? new WorkflowNodeInstancesSchemeInfo();
  }

  private static workflowNodeInstances: WorkflowNodeInstancesSchemeInfo;

  /**
   * Table identifier for "WorkflowNodeInstanceSubprocesses": {830b89cf-862c-4f6a-b564-d538d0bbec90}.
   */
  static get WorkflowNodeInstanceSubprocesses(): WorkflowNodeInstanceSubprocessesSchemeInfo {
    return SchemeInfo.workflowNodeInstanceSubprocesses = SchemeInfo.workflowNodeInstanceSubprocesses ?? new WorkflowNodeInstanceSubprocessesSchemeInfo();
  }

  private static workflowNodeInstanceSubprocesses: WorkflowNodeInstanceSubprocessesSchemeInfo;

  /**
   * Table identifier for "WorkflowNodeInstanceTasks": {6ba32f52-56a3-4319-968b-90f1651cc5a7}.
   */
  static get WorkflowNodeInstanceTasks(): WorkflowNodeInstanceTasksSchemeInfo {
    return SchemeInfo.workflowNodeInstanceTasks = SchemeInfo.workflowNodeInstanceTasks ?? new WorkflowNodeInstanceTasksSchemeInfo();
  }

  private static workflowNodeInstanceTasks: WorkflowNodeInstanceTasksSchemeInfo;

  /**
   * Table identifier for "WorkflowOutLinks": {03d962fa-c020-481a-90bd-932cbbd4368d}.
   */
  static get WorkflowOutLinks(): WorkflowOutLinksSchemeInfo {
    return SchemeInfo.workflowOutLinks = SchemeInfo.workflowOutLinks ?? new WorkflowOutLinksSchemeInfo();
  }

  private static workflowOutLinks: WorkflowOutLinksSchemeInfo;

  /**
   * Table identifier for "WorkflowPreConditions": {1290310e-0b81-4560-8996-71f5bcb3a9a3}.
   */
  static get WorkflowPreConditions(): WorkflowPreConditionsSchemeInfo {
    return SchemeInfo.workflowPreConditions = SchemeInfo.workflowPreConditions ?? new WorkflowPreConditionsSchemeInfo();
  }

  private static workflowPreConditions: WorkflowPreConditionsSchemeInfo;

  /**
   * Table identifier for "WorkflowProcessErrorsVirtual": {f7ebd016-ef99-4dfd-ba04-11f428395fe3}.
   */
  static get WorkflowProcessErrorsVirtual(): WorkflowProcessErrorsVirtualSchemeInfo {
    return SchemeInfo.workflowProcessErrorsVirtual = SchemeInfo.workflowProcessErrorsVirtual ?? new WorkflowProcessErrorsVirtualSchemeInfo();
  }

  private static workflowProcessErrorsVirtual: WorkflowProcessErrorsVirtualSchemeInfo;

  /**
   * Table identifier for "WorkflowProcesses": {a2db2754-b0ca-4d38-988d-0de6d58057cb}.
   */
  static get WorkflowProcesses(): WorkflowProcessesSchemeInfo {
    return SchemeInfo.workflowProcesses = SchemeInfo.workflowProcesses ?? new WorkflowProcessesSchemeInfo();
  }

  private static workflowProcesses: WorkflowProcessesSchemeInfo;

  /**
   * Table identifier for "WorkflowSignalProcessingModes": {67b602c1-ea47-4716-92ba-81f625ba36f1}.
   */
  static get WorkflowSignalProcessingModes(): WorkflowSignalProcessingModesSchemeInfo {
    return SchemeInfo.workflowSignalProcessingModes = SchemeInfo.workflowSignalProcessingModes ?? new WorkflowSignalProcessingModesSchemeInfo();
  }

  private static workflowSignalProcessingModes: WorkflowSignalProcessingModesSchemeInfo;

  /**
   * Table identifier for "WorkflowSignalTypes": {53dc8c0b-391a-4fbd-86c0-3da697abf065}.
   */
  static get WorkflowSignalTypes(): WorkflowSignalTypesSchemeInfo {
    return SchemeInfo.workflowSignalTypes = SchemeInfo.workflowSignalTypes ?? new WorkflowSignalTypesSchemeInfo();
  }

  private static workflowSignalTypes: WorkflowSignalTypesSchemeInfo;

  /**
   * Table identifier for "WorkflowTasks": {d2683167-0425-4093-ba65-0196ded5437a}.
   */
  static get WorkflowTasks(): WorkflowTasksSchemeInfo {
    return SchemeInfo.workflowTasks = SchemeInfo.workflowTasks ?? new WorkflowTasksSchemeInfo();
  }

  private static workflowTasks: WorkflowTasksSchemeInfo;

  /**
   * Table identifier for "WorkplaceRoles": {ad21dc6e-c694-4862-ba61-1df6b7506101}.
   */
  static get WorkplaceRoles(): WorkplaceRolesSchemeInfo {
    return SchemeInfo.workplaceRoles = SchemeInfo.workplaceRoles ?? new WorkplaceRolesSchemeInfo();
  }

  private static workplaceRoles: WorkplaceRolesSchemeInfo;

  /**
   * Table identifier for "WorkplaceRolesVirtual": {67f548c6-9fdf-44c1-9d61-eea3098021f5}.
   */
  static get WorkplaceRolesVirtual(): WorkplaceRolesVirtualSchemeInfo {
    return SchemeInfo.workplaceRolesVirtual = SchemeInfo.workplaceRolesVirtual ?? new WorkplaceRolesVirtualSchemeInfo();
  }

  private static workplaceRolesVirtual: WorkplaceRolesVirtualSchemeInfo;

  /**
   * Table identifier for "Workplaces": {21cd7a4f-6930-4746-9a57-72481e951b02}.
   */
  static get Workplaces(): WorkplacesSchemeInfo {
    return SchemeInfo.workplaces = SchemeInfo.workplaces ?? new WorkplacesSchemeInfo();
  }

  private static workplaces: WorkplacesSchemeInfo;

  /**
   * Table identifier for "WorkplacesVirtual": {a2f0c6b0-32c0-4c2e-97c4-e431ef93fc84}.
   */
  static get WorkplacesVirtual(): WorkplacesVirtualSchemeInfo {
    return SchemeInfo.workplacesVirtual = SchemeInfo.workplacesVirtual ?? new WorkplacesVirtualSchemeInfo();
  }

  private static workplacesVirtual: WorkplacesVirtualSchemeInfo;

  //#endregion
}