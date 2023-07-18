// noinspection JSUnusedGlobalSymbols,SpellCheckingInspection

//#region AccountUserSettings

/**
 * ID: {48332157-4f6a-4cb1-ace6-4b811c0e5364}
 * Alias: AccountUserSettings
 * Caption: $CardTypes_Blocks_AccountSettings
 * Group: UserSettings
 */
class AccountUserSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "AccountUserSettings": {48332157-4f6a-4cb1-ace6-4b811c0e5364}.
   */
  readonly ID: guid = '48332157-4f6a-4cb1-ace6-4b811c0e5364';

  /**
   * Card type name for "AccountUserSettings".
   */
  readonly Alias: string = 'AccountUserSettings';

  /**
   * Card type caption for "AccountUserSettings".
   */
  readonly Caption: string = '$CardTypes_Blocks_AccountSettings';

  /**
   * Card type group for "AccountUserSettings".
   */
  readonly Group: string = 'UserSettings';

  //#endregion

  //#region Forms

  readonly FormMySettings: string = 'MySettings';

  //#endregion

  //#region Blocks

  readonly BlockAccountSettings: string = 'AccountSettings';

  //#endregion

  //#region Controls

  readonly ResetSettingsButton: string = 'ResetSettingsButton';
  readonly ChangePasswordButton: string = 'ChangePasswordButton';
  readonly ApplyUserSettings: string = 'ApplyUserSettings';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region AclGenerationRule

/**
 * ID: {76b57ab5-19d4-4cc9-9e80-2c2f05cc2425}
 * Alias: AclGenerationRule
 * Caption: $CardTypes_TypesNames_AclGenerationRule
 * Group: Settings
 */
class AclGenerationRuleTypeInfo {
  //#region Common

  /**
   * Card type identifier for "AclGenerationRule": {76b57ab5-19d4-4cc9-9e80-2c2f05cc2425}.
   */
  readonly ID: guid = '76b57ab5-19d4-4cc9-9e80-2c2f05cc2425';

  /**
   * Card type name for "AclGenerationRule".
   */
  readonly Alias: string = 'AclGenerationRule';

  /**
   * Card type caption for "AclGenerationRule".
   */
  readonly Caption: string = '$CardTypes_TypesNames_AclGenerationRule';

  /**
   * Card type group for "AclGenerationRule".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormAclGenerationRule: string = 'AclGenerationRule';
  readonly FormTab: string = 'Tab';

  //#endregion

  //#region Blocks

  /**
   * Block caption "License" for "License".
   */
  readonly BlockLicense: string = 'License';
  readonly BlockBlock1: string = 'Block1';
  readonly BlockSmartRole: string = 'SmartRole';
  readonly BlockExtensions: string = 'Extensions';
  readonly BlockTriggers: string = 'Triggers';
  readonly BlockTriggerMain: string = 'TriggerMain';
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  /**
   * Control caption "LicenseHint" for "LicenseHint".
   */
  readonly LicenseHint: string = 'LicenseHint';

  readonly RolesSqlQuery: string = 'RolesSqlQuery';
  readonly CardOwnerSelectorSql: string = 'CardOwnerSelectorSql';
  readonly CardsByOwnerSelectorSql: string = 'CardsByOwnerSelectorSql';

  /**
   * Control caption "Extensions" for "Extensions".
   */
  readonly Extensions: string = 'Extensions';
  readonly CardTypes: string = 'CardTypes';
  readonly OnlySelfUpdate: string = 'OnlySelfUpdate';
  readonly UpdateAclCardSelectorSql: string = 'UpdateAclCardSelectorSql';
  readonly Triggers: string = 'Triggers';
  readonly Validate: string = 'Validate';
  readonly ValidateAll: string = 'ValidateAll';
  readonly Errors: string = 'Errors';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region ActionHistoryRecord

/**
 * ID: {abc13918-aa63-45ca-a3f4-d1fd5673c248}
 * Alias: ActionHistoryRecord
 * Caption: $CardTypes_TypesNames_ActionHistoryRecord
 * Group: System
 */
class ActionHistoryRecordTypeInfo {
  //#region Common

  /**
   * Card type identifier for "ActionHistoryRecord": {abc13918-aa63-45ca-a3f4-d1fd5673c248}.
   */
  readonly ID: guid = 'abc13918-aa63-45ca-a3f4-d1fd5673c248';

  /**
   * Card type name for "ActionHistoryRecord".
   */
  readonly Alias: string = 'ActionHistoryRecord';

  /**
   * Card type caption for "ActionHistoryRecord".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ActionHistoryRecord';

  /**
   * Card type group for "ActionHistoryRecord".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormActionHistoryRecord: string = 'ActionHistoryRecord';
  readonly FormSystemInfo: string = 'SystemInfo';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockShowInWindow: string = 'ShowInWindow';
  readonly BlockShowErrorDetails: string = 'ShowErrorDetails';
  readonly BlockChangesInfo: string = 'ChangesInfo';
  readonly BlockAdditionalDescription: string = 'AdditionalDescription';
  readonly BlockSystemInfo: string = 'SystemInfo';

  //#endregion

  //#region Controls

  readonly Category: string = 'Category';
  readonly ShowInWindow: string = 'ShowInWindow';
  readonly ShowErrorDetails: string = 'ShowErrorDetails';
  readonly AdditionalDescription: string = 'AdditionalDescription';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region AdSync

/**
 * ID: {cdaa9e03-9e06-4b4d-9a21-cfc446d2d9d1}
 * Alias: AdSync
 * Caption: $CardTypes_TypesNames_ADSync
 * Group: Settings
 */
class AdSyncTypeInfo {
  //#region Common

  /**
   * Card type identifier for "AdSync": {cdaa9e03-9e06-4b4d-9a21-cfc446d2d9d1}.
   */
  readonly ID: guid = 'cdaa9e03-9e06-4b4d-9a21-cfc446d2d9d1';

  /**
   * Card type name for "AdSync".
   */
  readonly Alias: string = 'AdSync';

  /**
   * Card type caption for "AdSync".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ADSync';

  /**
   * Card type group for "AdSync".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormAdSync: string = 'AdSync';
  readonly FormManual_sync: string = 'Manual_sync';

  //#endregion

  //#region Blocks

  /**
   * Block caption "License" for "License".
   */
  readonly BlockLicense: string = 'License';
  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockSyncRoots: string = 'SyncRoots';
  readonly BlockBlock1: string = 'Block1';
  readonly BlockRecurrentSync: string = 'RecurrentSync';
  readonly BlockSyncActions: string = 'SyncActions';

  //#endregion

  //#region Controls

  /**
   * Control caption "LicenseHint" for "LicenseHint".
   */
  readonly LicenseHint: string = 'LicenseHint';

  readonly AdSyncStart: string = 'AdSyncStart';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Application

/**
 * ID: {029094a5-52a1-4f4f-8aa6-3297f41a3a75}
 * Alias: Application
 * Caption: $CardTypes_TypesNames_Application
 * Group: System
 */
class ApplicationTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Application": {029094a5-52a1-4f4f-8aa6-3297f41a3a75}.
   */
  readonly ID: guid = '029094a5-52a1-4f4f-8aa6-3297f41a3a75';

  /**
   * Card type name for "Application".
   */
  readonly Alias: string = 'Application';

  /**
   * Card type caption for "Application".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Application';

  /**
   * Card type group for "Application".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormApplication: string = 'Application';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockFiles: string = 'Files';

  //#endregion

  //#region Controls

  readonly Files: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region AuthorCondition

/**
 * ID: {13502054-248d-4619-81e0-5ed08b5a5db9}
 * Alias: AuthorCondition
 * Caption: $CardTypes_TypesNames_AuthorCondition
 * Group: Conditions
 */
class AuthorConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "AuthorCondition": {13502054-248d-4619-81e0-5ed08b5a5db9}.
   */
  readonly ID: guid = '13502054-248d-4619-81e0-5ed08b5a5db9';

  /**
   * Card type name for "AuthorCondition".
   */
  readonly Alias: string = 'AuthorCondition';

  /**
   * Card type caption for "AuthorCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_AuthorCondition';

  /**
   * Card type group for "AuthorCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormAuthorCondition: string = 'AuthorCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region AutoCompleteDialogs

/**
 * ID: {8067e1c6-15a4-4741-b62c-29e5257efedd}
 * Alias: AutoCompleteDialogs
 * Caption: $Cards_DefaultCaption
 * Group: System
 */
class AutoCompleteDialogsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "AutoCompleteDialogs": {8067e1c6-15a4-4741-b62c-29e5257efedd}.
   */
  readonly ID: guid = '8067e1c6-15a4-4741-b62c-29e5257efedd';

  /**
   * Card type name for "AutoCompleteDialogs".
   */
  readonly Alias: string = 'AutoCompleteDialogs';

  /**
   * Card type caption for "AutoCompleteDialogs".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "AutoCompleteDialogs".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Scheme Items

  readonly Scheme: AutoCompleteDialogsSchemeInfoVirtual = new AutoCompleteDialogsSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormSingleValueSelector: string = 'SingleValueSelector';

  //#endregion

  //#region Blocks

  /**
   * Block caption "MainBlock" for "MainBlock".
   */
  readonly BlockMainBlock: string = 'MainBlock';

  //#endregion

  //#region Controls

  /**
   * Control caption "MainViewControl" for "MainViewControl".
   */
  readonly MainViewControl: string = 'MainViewControl';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region AutoCompleteDialogs Scheme Items

//#region SchemeInfo

class AutoCompleteDialogsSchemeInfoVirtual {
}

//#endregion

//#region Tables

//#endregion

//#endregion

//#region BusinessProcessTemplate

/**
 * ID: {d05799bd-35b6-43b4-97dd-b6d0e683eff2}
 * Alias: BusinessProcessTemplate
 * Caption: $CardTypes_TypesNames_BusinessProcessTemplate
 * Group: Dictionaries
 */
class BusinessProcessTemplateTypeInfo {
  //#region Common

  /**
   * Card type identifier for "BusinessProcessTemplate": {d05799bd-35b6-43b4-97dd-b6d0e683eff2}.
   */
  readonly ID: guid = 'd05799bd-35b6-43b4-97dd-b6d0e683eff2';

  /**
   * Card type name for "BusinessProcessTemplate".
   */
  readonly Alias: string = 'BusinessProcessTemplate';

  /**
   * Card type caption for "BusinessProcessTemplate".
   */
  readonly Caption: string = '$CardTypes_TypesNames_BusinessProcessTemplate';

  /**
   * Card type group for "BusinessProcessTemplate".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormBusinessProcessTemplate: string = 'BusinessProcessTemplate';

  //#endregion

  //#region Blocks

  readonly BlockInfo: string = 'Info';
  readonly BlockAdditional: string = 'Additional';
  readonly BlockNotifications: string = 'Notifications';
  readonly BlockPermissions: string = 'Permissions';
  readonly BlockVersions: string = 'Versions';
  readonly BlockMainBlock: string = 'MainBlock';
  readonly BlockMainBlock2: string = 'MainBlock2';
  readonly BlockMainBlock3: string = 'MainBlock3';
  readonly BlockExecutionAccessDeniedBlock: string = 'ExecutionAccessDeniedBlock';
  readonly BlockProcessBlock: string = 'ProcessBlock';
  readonly BlockConditionBlock: string = 'ConditionBlock';
  readonly BlockButtons: string = 'Buttons';

  //#endregion

  //#region Controls

  readonly VersionsTable: string = 'VersionsTable';
  readonly Compile: string = 'Compile';
  readonly ProcessButtons: string = 'ProcessButtons';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Calendar

/**
 * ID: {9079d7f9-7b44-43f2-bf11-f9ed11965dd6}
 * Alias: Calendar
 * Caption: $CardTypes_TypesNames_Tabs_Calendar
 * Group: Settings
 */
class CalendarTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Calendar": {9079d7f9-7b44-43f2-bf11-f9ed11965dd6}.
   */
  readonly ID: guid = '9079d7f9-7b44-43f2-bf11-f9ed11965dd6';

  /**
   * Card type name for "Calendar".
   */
  readonly Alias: string = 'Calendar';

  /**
   * Card type caption for "Calendar".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Tabs_Calendar';

  /**
   * Card type group for "Calendar".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormCalendar: string = 'Calendar';

  //#endregion

  //#region Blocks

  readonly BlockBlock3: string = 'Block3';

  /**
   * Block caption "Block5" for "Block5".
   */
  readonly BlockBlock5: string = 'Block5';

  /**
   * Block caption "Block6" for "Block6".
   */
  readonly BlockBlock6: string = 'Block6';

  /**
   * Block caption "Block2" for "Block2".
   */
  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock4: string = 'Block4';
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly CalendarType: string = 'CalendarType';
  readonly RebuildCalendar: string = 'RebuildCalendar';
  readonly ValidateCalendar: string = 'ValidateCalendar';
  readonly NamedRanges: string = 'NamedRanges';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region CalendarCalcMethod

/**
 * ID: {2a9b3b6d-1c22-46dc-ada2-7437fd4273ce}
 * Alias: CalendarCalcMethod
 * Caption: $CardTypes_TypesNames_Tabs_Controls_CalendarCalcMethod
 * Group: Settings
 */
class CalendarCalcMethodTypeInfo {
  //#region Common

  /**
   * Card type identifier for "CalendarCalcMethod": {2a9b3b6d-1c22-46dc-ada2-7437fd4273ce}.
   */
  readonly ID: guid = '2a9b3b6d-1c22-46dc-ada2-7437fd4273ce';

  /**
   * Card type name for "CalendarCalcMethod".
   */
  readonly Alias: string = 'CalendarCalcMethod';

  /**
   * Card type caption for "CalendarCalcMethod".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Tabs_Controls_CalendarCalcMethod';

  /**
   * Card type group for "CalendarCalcMethod".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormCalendarCalcMethod: string = 'CalendarCalcMethod';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  /**
   * Block caption "Block2" for "Block2".
   */
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  readonly CompileButton: string = 'CompileButton';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Car

/**
 * ID: {d0006e40-a342-4797-8d77-6501c4b7c4ac}
 * Alias: Car
 * Caption: $CardTypes_TypesNames_Car
 * Group: (Без группы)
 */
class CarTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Car": {d0006e40-a342-4797-8d77-6501c4b7c4ac}.
   */
  readonly ID: guid = 'd0006e40-a342-4797-8d77-6501c4b7c4ac';

  /**
   * Card type name for "Car".
   */
  readonly Alias: string = 'Car';

  /**
   * Card type caption for "Car".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Car';

  /**
   * Card type group for "Car".
   */
  readonly Group: string = '(Без группы)';

  //#endregion

  //#region Forms

  readonly FormCar: string = 'Car';
  readonly FormFileCopies: string = 'FileCopies';
  readonly FormFiles: string = 'Files';
  readonly FormDialog_1C: string = 'Dialog_1C';
  readonly FormViewsDemonstrations: string = 'ViewsDemonstrations';
  readonly FormTaskHistory: string = 'TaskHistory';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockAdditionalInfo: string = 'AdditionalInfo';
  readonly BlockMainInfo2: string = 'MainInfo2';
  readonly BlockSale: string = 'Sale';
  readonly BlockCustomer: string = 'Customer';
  readonly BlockOperation: string = 'Operation';
  readonly BlockCustomers: string = 'Customers';
  readonly BlockFiles: string = 'Files';
  readonly BlockBlock1: string = 'Block1';
  readonly BlockFileTabs: string = 'FileTabs';
  readonly BlockFileCopies: string = 'FileCopies';

  /**
   * Block caption "Превью1" for "Block2".
   */
  readonly BlockBlock2: string = 'Block2';
  readonly BlockDriverInfo: string = 'DriverInfo';
  readonly BlockTableViewDemo: string = 'TableViewDemo';

  /**
   * Block caption "TaskHistory" for "TaskHistory".
   */
  readonly BlockTaskHistory: string = 'TaskHistory';

  //#endregion

  //#region Controls

  readonly CarName: string = 'CarName';
  readonly Running: string = 'Running';
  readonly ReleaseDate: string = 'ReleaseDate';
  readonly ReleaseDate2: string = 'ReleaseDate2';

  /**
   * Control caption "Html" for "HtmlControl".
   */
  readonly HtmlControl: string = 'HtmlControl';
  readonly Color: string = 'Color';
  readonly ShareName: string = 'ShareName';
  readonly ManagerName: string = 'ManagerName';
  readonly EndDate: string = 'EndDate';
  readonly Buyers: string = 'Buyers';
  readonly ShareList: string = 'ShareList';
  readonly DriverName: string = 'DriverName';
  readonly DriverName2: string = 'DriverName2';
  readonly Owners: string = 'Owners';
  readonly Owners2: string = 'Owners2';
  readonly Files1: string = 'Files1';
  readonly Files2: string = 'Files2';
  readonly Files3: string = 'Files3';
  readonly AllFilesControl: string = 'AllFilesControl';
  readonly ImageFilesControl: string = 'ImageFilesControl';
  readonly Get1CButton: string = 'Get1CButton';
  readonly ShowDialogTypeForm: string = 'ShowDialogTypeForm';
  readonly FileInTabs: string = 'FileInTabs';
  readonly FileTabs: string = 'FileTabs';
  readonly FileCopies: string = 'FileCopies';

  /**
   * Control caption "Control1" for "CompareFilesView1".
   */
  readonly CompareFilesView1: string = 'CompareFilesView1';

  /**
   * Control caption "Control2" for "CompareFilesView2".
   */
  readonly CompareFilesView2: string = 'CompareFilesView2';

  /**
   * Control caption "Preview1" for "Preview1".
   */
  readonly Preview1: string = 'Preview1';

  /**
   * Control caption "Preview2" for "Preview2".
   */
  readonly Preview2: string = 'Preview2';
  readonly ShareListView: string = 'ShareListView';
  readonly BuyersView: string = 'BuyersView';
  readonly BuyerOperationsView: string = 'BuyerOperationsView';

  /**
   * Control caption "TaskHistory" for "TaskHistoryViewControl".
   */
  readonly TaskHistoryViewControl: string = 'TaskHistoryViewControl';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Car1CDialog

/**
 * ID: {0fa7aaeb-dc36-4647-a29e-2eb87153984d}
 * Alias: Car1CDialog
 * Caption: $CardTypes_Tabs_Dialog1C
 * Group: (Без группы)
 */
class Car1CDialogTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Car1CDialog": {0fa7aaeb-dc36-4647-a29e-2eb87153984d}.
   */
  readonly ID: guid = '0fa7aaeb-dc36-4647-a29e-2eb87153984d';

  /**
   * Card type name for "Car1CDialog".
   */
  readonly Alias: string = 'Car1CDialog';

  /**
   * Card type caption for "Car1CDialog".
   */
  readonly Caption: string = '$CardTypes_Tabs_Dialog1C';

  /**
   * Card type group for "Car1CDialog".
   */
  readonly Group: string = '(Без группы)';

  //#endregion

  //#region Scheme Items

  readonly Scheme: Car1CDialogSchemeInfoVirtual = new Car1CDialogSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormDialog1C: string = 'Dialog1C';

  //#endregion

  //#region Blocks

  readonly BlockDriverInfo: string = 'DriverInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Car1CDialog Scheme Items

//#region SchemeInfo

class Car1CDialogSchemeInfoVirtual {
  readonly TEST_CarMainInfoDialog: TEST_CarMainInfoDialogCar1CDialogSchemeInfoVirtual = new TEST_CarMainInfoDialogCar1CDialogSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region TEST_CarMainInfoDialog

/**
 * ID: {c2712502-4f22-49c9-a91a-0a48ac5703e5}
 * Alias: TEST_CarMainInfoDialog
 */
// tslint:disable-next-line:class-name
class TEST_CarMainInfoDialogCar1CDialogSchemeInfoVirtual {
  private readonly name: string = "TEST_CarMainInfoDialog";

  //#region Columns

  readonly Name: string = 'Name';
  readonly DriverID: string = 'DriverID';
  readonly DriverName: string = 'DriverName';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region CardTasksEditorDialog

/**
 * ID: {db737600-1bf6-451b-80ca-01fe06161ee6}
 * Alias: CardTasksEditorDialog
 * Caption: $CardTypes_CardTasksEditor
 * Group: System
 */
class CardTasksEditorDialogTypeInfo {
  //#region Common

  /**
   * Card type identifier for "CardTasksEditorDialog": {db737600-1bf6-451b-80ca-01fe06161ee6}.
   */
  readonly ID: guid = 'db737600-1bf6-451b-80ca-01fe06161ee6';

  /**
   * Card type name for "CardTasksEditorDialog".
   */
  readonly Alias: string = 'CardTasksEditorDialog';

  /**
   * Card type caption for "CardTasksEditorDialog".
   */
  readonly Caption: string = '$CardTypes_CardTasksEditor';

  /**
   * Card type group for "CardTasksEditorDialog".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormCardTasksEditorDialog: string = 'CardTasksEditorDialog';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  /**
   * Control caption "CardTasks" for "CardTasks".
   */
  readonly CardTasks: string = 'CardTasks';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region CarViewParameters

/**
 * ID: {ef08853d-7fdf-4fec-91b2-a1b8905e29fc}
 * Alias: CarViewParameters
 * Caption: $Views_FilterDialog_Caption
 * Group: (Без группы)
 */
class CarViewParametersTypeInfo {
  //#region Common

  /**
   * Card type identifier for "CarViewParameters": {ef08853d-7fdf-4fec-91b2-a1b8905e29fc}.
   */
  readonly ID: guid = 'ef08853d-7fdf-4fec-91b2-a1b8905e29fc';

  /**
   * Card type name for "CarViewParameters".
   */
  readonly Alias: string = 'CarViewParameters';

  /**
   * Card type caption for "CarViewParameters".
   */
  readonly Caption: string = '$Views_FilterDialog_Caption';

  /**
   * Card type group for "CarViewParameters".
   */
  readonly Group: string = '(Без группы)';

  //#endregion

  //#region Scheme Items

  readonly Scheme: CarViewParametersSchemeInfoVirtual = new CarViewParametersSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormMainTab: string = 'MainTab';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Parameters" for "Parameters".
   */
  readonly BlockParameters: string = 'Parameters';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region CarViewParameters Scheme Items

//#region SchemeInfo

class CarViewParametersSchemeInfoVirtual {
  readonly Parameters: ParametersCarViewParametersSchemeInfoVirtual = new ParametersCarViewParametersSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region Parameters

/**
 * ID: {dfcd24a1-1ba0-435e-9dbd-276a218f9ed3}
 * Alias: Parameters
 * Description: Параметры представления
 */
class ParametersCarViewParametersSchemeInfoVirtual {
  private readonly name: string = "Parameters";

  //#region Columns

  readonly Name: string = 'Name';
  readonly MaxSpeed: string = 'MaxSpeed';
  readonly DriverID: string = 'DriverID';
  readonly DriverName: string = 'DriverName';
  readonly ReleaseDateFrom: string = 'ReleaseDateFrom';
  readonly ReleaseDateTo: string = 'ReleaseDateTo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region CompletionOption

/**
 * ID: {f6b95639-234e-4800-a2f1-3cb20e0bcda4}
 * Alias: CompletionOption
 * Caption: $CardTypes_TypesNames_CompletionOption
 * Group: System
 */
class CompletionOptionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "CompletionOption": {f6b95639-234e-4800-a2f1-3cb20e0bcda4}.
   */
  readonly ID: guid = 'f6b95639-234e-4800-a2f1-3cb20e0bcda4';

  /**
   * Card type name for "CompletionOption".
   */
  readonly Alias: string = 'CompletionOption';

  /**
   * Card type caption for "CompletionOption".
   */
  readonly Caption: string = '$CardTypes_TypesNames_CompletionOption';

  /**
   * Card type group for "CompletionOption".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormCompletionOption: string = 'CompletionOption';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region ConditionsBase

/**
 * ID: {0eb20600-f137-42ad-b007-440492200acf}
 * Alias: ConditionsBase
 * Caption: ConditionsBase
 * Group: Conditions
 */
class ConditionsBaseTypeInfo {
  //#region Common

  /**
   * Card type identifier for "ConditionsBase": {0eb20600-f137-42ad-b007-440492200acf}.
   */
  readonly ID: guid = '0eb20600-f137-42ad-b007-440492200acf';

  /**
   * Card type name for "ConditionsBase".
   */
  readonly Alias: string = 'ConditionsBase';

  /**
   * Card type caption for "ConditionsBase".
   */
  readonly Caption: string = 'ConditionsBase';

  /**
   * Card type group for "ConditionsBase".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormConditionsBase: string = 'ConditionsBase';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockConditions: string = 'Conditions';

  //#endregion

  //#region Controls

  readonly ConditionType: string = 'ConditionType';
  readonly ConditionsTable: string = 'ConditionsTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region ConditionType

/**
 * ID: {ca37649d-9585-4c83-8ef4-fdf91c4c42ee}
 * Alias: ConditionType
 * Caption: $CardTypes_TypesNames_ConditionType
 * Group: Settings
 */
class ConditionTypeTypeInfo {
  //#region Common

  /**
   * Card type identifier for "ConditionType": {ca37649d-9585-4c83-8ef4-fdf91c4c42ee}.
   */
  readonly ID: guid = 'ca37649d-9585-4c83-8ef4-fdf91c4c42ee';

  /**
   * Card type name for "ConditionType".
   */
  readonly Alias: string = 'ConditionType';

  /**
   * Card type caption for "ConditionType".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ConditionType';

  /**
   * Card type group for "ConditionType".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormConditionType: string = 'ConditionType';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  /**
   * Block caption "Block2" for "Block2".
   */
  readonly BlockBlock2: string = 'Block2';

  /**
   * Block caption "Block3" for "Block3".
   */
  readonly BlockBlock3: string = 'Block3';
  readonly BlockExamples: string = 'Examples';

  //#endregion

  //#region Controls

  readonly RepairConditionsButton: string = 'RepairConditionsButton';
  readonly RepairAllConditionsButton: string = 'RepairAllConditionsButton';
  readonly CompileButton: string = 'CompileButton';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region ContextRole

/**
 * ID: {b672e00c-0241-0485-9b07-4764bc96c9d3}
 * Alias: ContextRole
 * Caption: $CardTypes_TypesNames_ContextRole
 * Group: Roles
 */
class ContextRoleTypeInfo {
  //#region Common

  /**
   * Card type identifier for "ContextRole": {b672e00c-0241-0485-9b07-4764bc96c9d3}.
   */
  readonly ID: guid = 'b672e00c-0241-0485-9b07-4764bc96c9d3';

  /**
   * Card type name for "ContextRole".
   */
  readonly Alias: string = 'ContextRole';

  /**
   * Card type caption for "ContextRole".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ContextRole';

  /**
   * Card type group for "ContextRole".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region Forms

  readonly FormContextRole: string = 'ContextRole';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockGeneratedSQL: string = 'GeneratedSQL';

  //#endregion

  //#region Controls

  readonly DisableDeputies: string = 'DisableDeputies';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Contract

/**
 * ID: {335f86a1-d009-012c-8b45-1f43c2382c2d}
 * Alias: Contract
 * Caption: $CardTypes_TypesNames_Contract
 * Group: Documents
 */
class ContractTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Contract": {335f86a1-d009-012c-8b45-1f43c2382c2d}.
   */
  readonly ID: guid = '335f86a1-d009-012c-8b45-1f43c2382c2d';

  /**
   * Card type name for "Contract".
   */
  readonly Alias: string = 'Contract';

  /**
   * Card type caption for "Contract".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Contract';

  /**
   * Card type group for "Contract".
   */
  readonly Group: string = 'Documents';

  //#endregion

  //#region Document Types

  /**
   * Document type identifier for "$KrTypes_DocTypes_Contract": {93a392e7-097c-4420-85c4-db10b2df3c1d}.
   */
  readonly ContractID: guid = '93a392e7-097c-4420-85c4-db10b2df3c1d';

  /**
   * Document type caption for "$KrTypes_DocTypes_Contract".
   */
  readonly ContractCaption: string = '$KrTypes_DocTypes_Contract';

  //#endregion

  //#region Forms

  readonly FormContract: string = 'Contract';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockBlock4: string = 'Block4';
  readonly BlockDepartmentSignedByRecipientsPerformersBlock: string = 'DepartmentSignedByRecipientsPerformersBlock';
  readonly BlockRefsBlock: string = 'RefsBlock';
  readonly BlockBlock5: string = 'Block5';

  //#endregion

  //#region Controls

  readonly PartnerControl: string = 'PartnerControl';
  readonly IncomingRefsControl: string = 'IncomingRefsControl';
  readonly Files: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region CreateFileFromTemplate

/**
 * ID: {98662e67-2e75-4d33-97d0-e1cefa096336}
 * Alias: CreateFileFromTemplate
 * Caption: $Cards_FileFromTemplate_CreateDialog
 * Group: System
 */
class CreateFileFromTemplateTypeInfo {
  //#region Common

  /**
   * Card type identifier for "CreateFileFromTemplate": {98662e67-2e75-4d33-97d0-e1cefa096336}.
   */
  readonly ID: guid = '98662e67-2e75-4d33-97d0-e1cefa096336';

  /**
   * Card type name for "CreateFileFromTemplate".
   */
  readonly Alias: string = 'CreateFileFromTemplate';

  /**
   * Card type caption for "CreateFileFromTemplate".
   */
  readonly Caption: string = '$Cards_FileFromTemplate_CreateDialog';

  /**
   * Card type group for "CreateFileFromTemplate".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Scheme Items

  readonly Scheme: CreateFileFromTemplateSchemeInfoVirtual = new CreateFileFromTemplateSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormCreateFileFromTemplate: string = 'CreateFileFromTemplate';

  //#endregion

  //#region Blocks

  /**
   * Block caption "TemplatesList" for "TemplatesList".
   */
  readonly BlockTemplatesList: string = 'TemplatesList';
  readonly BlockMainSettings: string = 'MainSettings';

  /**
   * Block caption "TemplateParameters" for "TemplateParameters".
   */
  readonly BlockTemplateParameters: string = 'TemplateParameters';

  //#endregion

  //#region Controls

  /**
   * Control caption "FileTemplates" for "FileTemplates".
   */
  readonly FileTemplates: string = 'FileTemplates';

  readonly FileName: string = 'FileName';
  readonly Extension: string = 'Extension';
  readonly ConvertToPDF: string = 'ConvertToPDF';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region CreateFileFromTemplate Scheme Items

//#region SchemeInfo

class CreateFileFromTemplateSchemeInfoVirtual {
  readonly FileTemplateParametersVirtual: FileTemplateParametersVirtualCreateFileFromTemplateSchemeInfoVirtual = new FileTemplateParametersVirtualCreateFileFromTemplateSchemeInfoVirtual();
  readonly CreateFileFromTemplateVirtual: CreateFileFromTemplateVirtualCreateFileFromTemplateSchemeInfoVirtual = new CreateFileFromTemplateVirtualCreateFileFromTemplateSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region FileTemplateParametersVirtual

/**
 * ID: {7d8c306f-2c1e-4a0e-b166-9803272cd53e}
 * Alias: FileTemplateParametersVirtual
 */
class FileTemplateParametersVirtualCreateFileFromTemplateSchemeInfoVirtual {
  private readonly name: string = "FileTemplateParametersVirtual";

  //#region Columns

  readonly ConvertToPDF: string = 'ConvertToPDF';
  readonly FileTemplateFileName: string = 'FileTemplateFileName';
  readonly FileTemplateFileExtension: string = 'FileTemplateFileExtension';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CreateFileFromTemplateVirtual

/**
 * ID: {ed862670-9e2f-449c-a0d3-f2afeda6a437}
 * Alias: CreateFileFromTemplateVirtual
 */
class CreateFileFromTemplateVirtualCreateFileFromTemplateSchemeInfoVirtual {
  private readonly name: string = "CreateFileFromTemplateVirtual";

  //#region Columns

  readonly FileTemplateName: string = 'FileTemplateName';
  readonly FileTemplateFileExtension: string = 'FileTemplateFileExtension';
  readonly FileTemplateFileName: string = 'FileTemplateFileName';
  readonly FileTemplateID: string = 'FileTemplateID';
  readonly FileTemplateGroupName: string = 'FileTemplateGroupName';
  readonly ConvertToPdf: string = 'ConvertToPdf';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region Currency

/**
 * ID: {4bce97d2-6b76-468d-9b09-711ba189cb1e}
 * Alias: Currency
 * Caption: $CardTypes_TypesNames_Currency
 * Group: Dictionaries
 */
class CurrencyTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Currency": {4bce97d2-6b76-468d-9b09-711ba189cb1e}.
   */
  readonly ID: guid = '4bce97d2-6b76-468d-9b09-711ba189cb1e';

  /**
   * Card type name for "Currency".
   */
  readonly Alias: string = 'Currency';

  /**
   * Card type caption for "Currency".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Currency';

  /**
   * Card type group for "Currency".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormCurrency: string = 'Currency';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region DefaultCalendarType

/**
 * ID: {2d1d11a0-3ed0-46a6-bd5c-8fc8a952eabd}
 * Alias: DefaultCalendarType
 * Caption: $CardTypes_TypesNames_Tabs_DefaultCalendarType
 * Group: Settings
 */
class DefaultCalendarTypeTypeInfo {
  //#region Common

  /**
   * Card type identifier for "DefaultCalendarType": {2d1d11a0-3ed0-46a6-bd5c-8fc8a952eabd}.
   */
  readonly ID: guid = '2d1d11a0-3ed0-46a6-bd5c-8fc8a952eabd';

  /**
   * Card type name for "DefaultCalendarType".
   */
  readonly Alias: string = 'DefaultCalendarType';

  /**
   * Card type caption for "DefaultCalendarType".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Tabs_DefaultCalendarType';

  /**
   * Card type group for "DefaultCalendarType".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormDefaultCalendarType: string = 'DefaultCalendarType';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock4: string = 'Block4';

  /**
   * Block caption "Warning" for "Block5".
   */
  readonly BlockBlock5: string = 'Block5';
  readonly BlockWorkTimeBlock: string = 'WorkTimeBlock';
  readonly BlockLunchTimeBlock: string = 'LunchTimeBlock';
  readonly BlockBlock3: string = 'Block3';

  //#endregion

  //#region Controls

  /**
   * Control caption "Warning" for "AdvancedNotificationsTooltip1".
   */
  readonly AdvancedNotificationsTooltip1: string = 'AdvancedNotificationsTooltip1';

  readonly WeekDaysControl: string = 'WeekDaysControl';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Deleted

/**
 * ID: {f5e74fbb-5357-4a6d-adce-4c2607853fdd}
 * Alias: Deleted
 * Caption: $CardTypes_TypesNames_Deleted
 * Group: System
 */
class DeletedTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Deleted": {f5e74fbb-5357-4a6d-adce-4c2607853fdd}.
   */
  readonly ID: guid = 'f5e74fbb-5357-4a6d-adce-4c2607853fdd';

  /**
   * Card type name for "Deleted".
   */
  readonly Alias: string = 'Deleted';

  /**
   * Card type caption for "Deleted".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Deleted';

  /**
   * Card type group for "Deleted".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormDeleted: string = 'Deleted';
  readonly FormTasks: string = 'Tasks';
  readonly FormSystemInfo: string = 'SystemInfo';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockFiles: string = 'Files';
  readonly BlockTasks: string = 'Tasks';
  readonly BlockSystemInfo: string = 'SystemInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region DepartmentCondition

/**
 * ID: {c8c6d9b5-0cc0-4f09-8d67-eb3f8e2e0bd4}
 * Alias: DepartmentCondition
 * Caption: $CardTypes_TypesNames_DepartmentCondition
 * Group: Conditions
 */
class DepartmentConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "DepartmentCondition": {c8c6d9b5-0cc0-4f09-8d67-eb3f8e2e0bd4}.
   */
  readonly ID: guid = 'c8c6d9b5-0cc0-4f09-8d67-eb3f8e2e0bd4';

  /**
   * Card type name for "DepartmentCondition".
   */
  readonly Alias: string = 'DepartmentCondition';

  /**
   * Card type caption for "DepartmentCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_DepartmentCondition';

  /**
   * Card type group for "DepartmentCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormDepartmentCondition: string = 'DepartmentCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region DepartmentRole

/**
 * ID: {abe57cb7-e1cb-06f6-b7ca-ad1668bebd72}
 * Alias: DepartmentRole
 * Caption: $CardTypes_TypesNames_DepartmentRole
 * Group: Roles
 */
class DepartmentRoleTypeInfo {
  //#region Common

  /**
   * Card type identifier for "DepartmentRole": {abe57cb7-e1cb-06f6-b7ca-ad1668bebd72}.
   */
  readonly ID: guid = 'abe57cb7-e1cb-06f6-b7ca-ad1668bebd72';

  /**
   * Card type name for "DepartmentRole".
   */
  readonly Alias: string = 'DepartmentRole';

  /**
   * Card type caption for "DepartmentRole".
   */
  readonly Caption: string = '$CardTypes_TypesNames_DepartmentRole';

  /**
   * Card type group for "DepartmentRole".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region Forms

  readonly FormDepartmentRole: string = 'DepartmentRole';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockAdInfo: string = 'AdInfo';
  readonly BlockUsers: string = 'Users';
  readonly BlockFiles: string = 'Files';

  //#endregion

  //#region Controls

  readonly TimeZone: string = 'TimeZone';
  readonly InheritTimeZone: string = 'InheritTimeZone';
  readonly AdSyncDate: string = 'AdSyncDate';
  readonly AdSyncWhenChanged: string = 'AdSyncWhenChanged';
  readonly AdSyncDistinguishedName: string = 'AdSyncDistinguishedName';
  readonly AdSyncID: string = 'AdSyncID';
  readonly AdSyncDisableUpdate: string = 'AdSyncDisableUpdate';
  readonly AdSyncIndependent: string = 'AdSyncIndependent';
  readonly AdSyncManualSync: string = 'AdSyncManualSync';
  readonly Files: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Dialogs

/**
 * ID: {d3107c45-c40a-4a86-b831-fbef1b71050f}
 * Alias: Dialogs
 * Caption: $CardTypes_TypesNames_Dialogs
 * Group: System
 */
class DialogsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Dialogs": {d3107c45-c40a-4a86-b831-fbef1b71050f}.
   */
  readonly ID: guid = 'd3107c45-c40a-4a86-b831-fbef1b71050f';

  /**
   * Card type name for "Dialogs".
   */
  readonly Alias: string = 'Dialogs';

  /**
   * Card type caption for "Dialogs".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Dialogs';

  /**
   * Card type group for "Dialogs".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormCreateMultipleCards: string = 'CreateMultipleCards';
  readonly FormAcquaintance: string = 'Acquaintance';
  readonly FormAddEmployees: string = 'AddEmployees';
  readonly FormChangePassword: string = 'ChangePassword';
  readonly FormApplyUserSettings: string = 'ApplyUserSettings';
  readonly FormDownloadDeski: string = 'DownloadDeski';

  //#endregion

  //#region Blocks

  readonly BlockMainBlock: string = 'MainBlock';
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly ChangePartner: string = 'ChangePartner';
  readonly ChangeAuthor: string = 'ChangeAuthor';
  readonly OldPassword: string = 'OldPassword';

  /**
   * Control caption "Warning label" for "WarningLabel".
   */
  readonly WarningLabel: string = 'WarningLabel';
  readonly Password: string = 'Password';
  readonly PasswordRepeat: string = 'PasswordRepeat';

  /**
   * Control caption "WebApp" for "WebApp".
   */
  readonly WebApp: string = 'WebApp';

  /**
   * Control caption "DownloadButton" for "DownloadButton".
   */
  readonly DownloadButton: string = 'DownloadButton';

  /**
   * Control caption "Description" for "Description".
   */
  readonly Description: string = 'Description';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region DocLoad

/**
 * ID: {33023ffa-2fd3-4b3b-80d9-bba6ab48ea8e}
 * Alias: DocLoad
 * Caption: $CardTypes_TypesNames_DocLoad
 * Group: Settings
 */
class DocLoadTypeInfo {
  //#region Common

  /**
   * Card type identifier for "DocLoad": {33023ffa-2fd3-4b3b-80d9-bba6ab48ea8e}.
   */
  readonly ID: guid = '33023ffa-2fd3-4b3b-80d9-bba6ab48ea8e';

  /**
   * Card type name for "DocLoad".
   */
  readonly Alias: string = 'DocLoad';

  /**
   * Card type caption for "DocLoad".
   */
  readonly Caption: string = '$CardTypes_TypesNames_DocLoad';

  /**
   * Card type group for "DocLoad".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormDocLoad: string = 'DocLoad';

  //#endregion

  //#region Blocks

  /**
   * Block caption "License" for "License".
   */
  readonly BlockLicense: string = 'License';
  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockRecognition: string = 'Recognition';
  readonly BlockBarcodes: string = 'Barcodes';
  readonly BlockBarcodeSettings: string = 'BarcodeSettings';

  //#endregion

  //#region Controls

  /**
   * Control caption "LicenseHint" for "LicenseHint".
   */
  readonly LicenseHint: string = 'LicenseHint';

  readonly StreamingInputUser: string = 'StreamingInputUser';

  /**
   * Control caption "RecognitionHint" for "RecognitionHint".
   */
  readonly RecognitionHint: string = 'RecognitionHint';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region DocStateCondition

/**
 * ID: {d57e0e60-6cae-4a4b-a481-2eeb704e46ec}
 * Alias: DocStateCondition
 * Caption: $CardTypes_TypesNames_DocStateCondition
 * Group: Conditions
 */
class DocStateConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "DocStateCondition": {d57e0e60-6cae-4a4b-a481-2eeb704e46ec}.
   */
  readonly ID: guid = 'd57e0e60-6cae-4a4b-a481-2eeb704e46ec';

  /**
   * Card type name for "DocStateCondition".
   */
  readonly Alias: string = 'DocStateCondition';

  /**
   * Card type caption for "DocStateCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_DocStateCondition';

  /**
   * Card type group for "DocStateCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormDocStateCondition: string = 'DocStateCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region DocTypeCondition

/**
 * ID: {6a525f7e-deaa-48a0-b2c7-20c2c5151932}
 * Alias: DocTypeCondition
 * Caption: $CardTypes_TypesNames_DocTypeCondition
 * Group: Conditions
 */
class DocTypeConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "DocTypeCondition": {6a525f7e-deaa-48a0-b2c7-20c2c5151932}.
   */
  readonly ID: guid = '6a525f7e-deaa-48a0-b2c7-20c2c5151932';

  /**
   * Card type name for "DocTypeCondition".
   */
  readonly Alias: string = 'DocTypeCondition';

  /**
   * Card type caption for "DocTypeCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_DocTypeCondition';

  /**
   * Card type group for "DocTypeCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormDocTypeCondition: string = 'DocTypeCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Document

/**
 * ID: {6d06c5a0-9687-4f6b-9bed-d3a081d84d9a}
 * Alias: Document
 * Caption: $CardTypes_TypesNames_Document
 * Group: Documents
 */
class DocumentTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Document": {6d06c5a0-9687-4f6b-9bed-d3a081d84d9a}.
   */
  readonly ID: guid = '6d06c5a0-9687-4f6b-9bed-d3a081d84d9a';

  /**
   * Card type name for "Document".
   */
  readonly Alias: string = 'Document';

  /**
   * Card type caption for "Document".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Document';

  /**
   * Card type group for "Document".
   */
  readonly Group: string = 'Documents';

  //#endregion

  //#region Document Types

  /**
   * Document type identifier for "$KrTypes_DocTypes_Addendum": {d1906ef6-38f1-4a05-b943-52c87da7e8ba}.
   */
  readonly AddendumID: guid = 'd1906ef6-38f1-4a05-b943-52c87da7e8ba';

  /**
   * Document type caption for "$KrTypes_DocTypes_Addendum".
   */
  readonly AddendumCaption: string = '$KrTypes_DocTypes_Addendum';

  /**
   * Document type identifier for "$KrTypes_DocTypes_Instruction": {b8463b4e-2d5f-4923-983e-aca16fb01635}.
   */
  readonly InstructionID: guid = 'b8463b4e-2d5f-4923-983e-aca16fb01635';

  /**
   * Document type caption for "$KrTypes_DocTypes_Instruction".
   */
  readonly InstructionCaption: string = '$KrTypes_DocTypes_Instruction';

  /**
   * Document type identifier for "$KrTypes_DocTypes_Document": {ea798bef-1b3b-49eb-9131-87333dafec19}.
   */
  readonly DocumentID: guid = 'ea798bef-1b3b-49eb-9131-87333dafec19';

  /**
   * Document type caption for "$KrTypes_DocTypes_Document".
   */
  readonly DocumentCaption: string = '$KrTypes_DocTypes_Document';

  /**
   * Document type identifier for "$KrTypes_DocTypes_ServiceRecord": {18d01f19-9c21-49ee-a27f-04ffd6ec27eb}.
   */
  readonly ServiceRecordID: guid = '18d01f19-9c21-49ee-a27f-04ffd6ec27eb';

  /**
   * Document type caption for "$KrTypes_DocTypes_ServiceRecord".
   */
  readonly ServiceRecordCaption: string = '$KrTypes_DocTypes_ServiceRecord';

  /**
   * Document type identifier for "$KrTypes_DocTypes_Order": {77fd0adc-24d9-426f-9397-c1ee0d175b93}.
   */
  readonly OrderID: guid = '77fd0adc-24d9-426f-9397-c1ee0d175b93';

  /**
   * Document type caption for "$KrTypes_DocTypes_Order".
   */
  readonly OrderCaption: string = '$KrTypes_DocTypes_Order';

  //#endregion

  //#region Forms

  readonly FormDocument: string = 'Document';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockBlock4: string = 'Block4';
  readonly BlockDepartmentSignedByRecipientsPerformersBlock: string = 'DepartmentSignedByRecipientsPerformersBlock';
  readonly BlockRefsBlock: string = 'RefsBlock';
  readonly BlockBlock5: string = 'Block5';

  //#endregion

  //#region Controls

  readonly IncomingRefsControl: string = 'IncomingRefsControl';
  readonly Files: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region DocumentCategory

/**
 * ID: {5da46e89-f932-4a48-b5a3-ec6285bfe3ea}
 * Alias: DocumentCategory
 * Caption: $CardTypes_TypesNames_Category
 * Group: Dictionaries
 */
class DocumentCategoryTypeInfo {
  //#region Common

  /**
   * Card type identifier for "DocumentCategory": {5da46e89-f932-4a48-b5a3-ec6285bfe3ea}.
   */
  readonly ID: guid = '5da46e89-f932-4a48-b5a3-ec6285bfe3ea';

  /**
   * Card type name for "DocumentCategory".
   */
  readonly Alias: string = 'DocumentCategory';

  /**
   * Card type caption for "DocumentCategory".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Category';

  /**
   * Card type group for "DocumentCategory".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormDocumentCategory: string = 'DocumentCategory';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region DynamicRole

/**
 * ID: {97a945bc-58f5-07fa-a274-b6a7f0f1282c}
 * Alias: DynamicRole
 * Caption: $CardTypes_TypesNames_DynamicRole
 * Group: Roles
 */
class DynamicRoleTypeInfo {
  //#region Common

  /**
   * Card type identifier for "DynamicRole": {97a945bc-58f5-07fa-a274-b6a7f0f1282c}.
   */
  readonly ID: guid = '97a945bc-58f5-07fa-a274-b6a7f0f1282c';

  /**
   * Card type name for "DynamicRole".
   */
  readonly Alias: string = 'DynamicRole';

  /**
   * Card type caption for "DynamicRole".
   */
  readonly Caption: string = '$CardTypes_TypesNames_DynamicRole';

  /**
   * Card type group for "DynamicRole".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region Forms

  readonly FormDynamicRole: string = 'DynamicRole';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockUsers: string = 'Users';
  readonly BlockErrorInfo: string = 'ErrorInfo';

  //#endregion

  //#region Controls

  readonly TimeZone: string = 'TimeZone';
  readonly RecalcButton: string = 'RecalcButton';
  readonly RoleUsersLimitDisclaimer: string = 'RoleUsersLimitDisclaimer';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region EmptyCondition

/**
 * ID: {4b68414d-bf1a-4227-bb01-b1fc939d0e5a}
 * Alias: EmptyCondition
 * Caption: $CardTypes_TypesNames_EmptyCondition
 * Group: Conditions
 */
class EmptyConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "EmptyCondition": {4b68414d-bf1a-4227-bb01-b1fc939d0e5a}.
   */
  readonly ID: guid = '4b68414d-bf1a-4227-bb01-b1fc939d0e5a';

  /**
   * Card type name for "EmptyCondition".
   */
  readonly Alias: string = 'EmptyCondition';

  /**
   * Card type caption for "EmptyCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_EmptyCondition';

  /**
   * Card type group for "EmptyCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Error

/**
 * ID: {fa81208d-2d83-4cb6-a83d-cba7e3f483a7}
 * Alias: Error
 * Caption: $CardTypes_TypesNames_Error
 * Group: System
 */
class ErrorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Error": {fa81208d-2d83-4cb6-a83d-cba7e3f483a7}.
   */
  readonly ID: guid = 'fa81208d-2d83-4cb6-a83d-cba7e3f483a7';

  /**
   * Card type name for "Error".
   */
  readonly Alias: string = 'Error';

  /**
   * Card type caption for "Error".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Error';

  /**
   * Card type group for "Error".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormError: string = 'Error';
  readonly FormSystemInfo: string = 'SystemInfo';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockShowInWindow: string = 'ShowInWindow';
  readonly BlockChangesInfo: string = 'ChangesInfo';
  readonly BlockAdditionalDescription: string = 'AdditionalDescription';
  readonly BlockBlock5: string = 'Block5';
  readonly BlockSystemInfo: string = 'SystemInfo';

  //#endregion

  //#region Controls

  readonly Category: string = 'Category';
  readonly ShowInWindow: string = 'ShowInWindow';
  readonly AdditionalDescription: string = 'AdditionalDescription';
  readonly Files: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region FieldChangedCondition

/**
 * ID: {6077353b-1af6-496f-a930-86954dce122c}
 * Alias: FieldChangedCondition
 * Caption: $CardTypes_TypesNames_FieldChangedCondition
 * Group: Conditions
 */
class FieldChangedConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "FieldChangedCondition": {6077353b-1af6-496f-a930-86954dce122c}.
   */
  readonly ID: guid = '6077353b-1af6-496f-a930-86954dce122c';

  /**
   * Card type name for "FieldChangedCondition".
   */
  readonly Alias: string = 'FieldChangedCondition';

  /**
   * Card type caption for "FieldChangedCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_FieldChangedCondition';

  /**
   * Card type group for "FieldChangedCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormFieldChangedCondition: string = 'FieldChangedCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region File

/**
 * ID: {ab387c69-fd62-0655-bbc3-b879e433a143}
 * Alias: File
 * Caption: $CardTypes_TypesNames_File
 * Group: System
 */
class FileTypeInfo {
  //#region Common

  /**
   * Card type identifier for "File": {ab387c69-fd62-0655-bbc3-b879e433a143}.
   */
  readonly ID: guid = 'ab387c69-fd62-0655-bbc3-b879e433a143';

  /**
   * Card type name for "File".
   */
  readonly Alias: string = 'File';

  /**
   * Card type caption for "File".
   */
  readonly Caption: string = '$CardTypes_TypesNames_File';

  /**
   * Card type group for "File".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region FileCategory

/**
 * ID: {97182afc-43ce-4bd9-9c96-73a4d2c5d5eb}
 * Alias: FileCategory
 * Caption: $CardTypes_TypesNames_FileCategory
 * Group: Dictionaries
 */
class FileCategoryTypeInfo {
  //#region Common

  /**
   * Card type identifier for "FileCategory": {97182afc-43ce-4bd9-9c96-73a4d2c5d5eb}.
   */
  readonly ID: guid = '97182afc-43ce-4bd9-9c96-73a4d2c5d5eb';

  /**
   * Card type name for "FileCategory".
   */
  readonly Alias: string = 'FileCategory';

  /**
   * Card type caption for "FileCategory".
   */
  readonly Caption: string = '$CardTypes_TypesNames_FileCategory';

  /**
   * Card type group for "FileCategory".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormFileCategory: string = 'FileCategory';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region FileConverterCache

/**
 * ID: {7609d1d7-9a46-4617-8789-2dff55aa4072}
 * Alias: FileConverterCache
 * Caption: $CardTypes_TypesNames_FileConverterCache
 * Group: Settings
 */
class FileConverterCacheTypeInfo {
  //#region Common

  /**
   * Card type identifier for "FileConverterCache": {7609d1d7-9a46-4617-8789-2dff55aa4072}.
   */
  readonly ID: guid = '7609d1d7-9a46-4617-8789-2dff55aa4072';

  /**
   * Card type name for "FileConverterCache".
   */
  readonly Alias: string = 'FileConverterCache';

  /**
   * Card type caption for "FileConverterCache".
   */
  readonly Caption: string = '$CardTypes_TypesNames_FileConverterCache';

  /**
   * Card type group for "FileConverterCache".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormFileConverterCache: string = 'FileConverterCache';

  //#endregion

  //#region Blocks

  readonly BlockMainInformation: string = 'MainInformation';
  readonly BlockCacheMaintenance: string = 'CacheMaintenance';

  //#endregion

  //#region Controls

  readonly RemoveOldFiles: string = 'RemoveOldFiles';
  readonly RemoveAllFiles: string = 'RemoveAllFiles';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region FilePreviewDialog

/**
 * ID: {1d196c65-f645-4fea-b515-c0d615dbb867}
 * Alias: FilePreviewDialog
 * Caption: FilePreviewDialog
 * Group: Forums
 */
class FilePreviewDialogTypeInfo {
  //#region Common

  /**
   * Card type identifier for "FilePreviewDialog": {1d196c65-f645-4fea-b515-c0d615dbb867}.
   */
  readonly ID: guid = '1d196c65-f645-4fea-b515-c0d615dbb867';

  /**
   * Card type name for "FilePreviewDialog".
   */
  readonly Alias: string = 'FilePreviewDialog';

  /**
   * Card type caption for "FilePreviewDialog".
   */
  readonly Caption: string = 'FilePreviewDialog';

  /**
   * Card type group for "FilePreviewDialog".
   */
  readonly Group: string = 'Forums';

  //#endregion

  //#region Scheme Items

  readonly Scheme: FilePreviewDialogSchemeInfoVirtual = new FilePreviewDialogSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormPreviewTab: string = 'PreviewTab';

  //#endregion

  //#region Blocks

  /**
   * Block caption "PreviewFile" for "PreviewFile".
   */
  readonly BlockPreviewFile: string = 'PreviewFile';

  //#endregion

  //#region Controls

  /**
   * Control caption "PreviewFile" for "PreviewFile".
   */
  readonly PreviewFile: string = 'PreviewFile';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region FilePreviewDialog Scheme Items

//#region SchemeInfo

class FilePreviewDialogSchemeInfoVirtual {
}

//#endregion

//#region Tables

//#endregion

//#endregion

//#region FileSatellite

/**
 * ID: {580cd334-6630-420e-acd8-9524be99e43e}
 * Alias: FileSatellite
 * Caption: $CardTypes_TypesNames_FileSatellite
 * Group: System
 */
class FileSatelliteTypeInfo {
  //#region Common

  /**
   * Card type identifier for "FileSatellite": {580cd334-6630-420e-acd8-9524be99e43e}.
   */
  readonly ID: guid = '580cd334-6630-420e-acd8-9524be99e43e';

  /**
   * Card type name for "FileSatellite".
   */
  readonly Alias: string = 'FileSatellite';

  /**
   * Card type caption for "FileSatellite".
   */
  readonly Caption: string = '$CardTypes_TypesNames_FileSatellite';

  /**
   * Card type group for "FileSatellite".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region FileTemplate

/**
 * ID: {b7e1b93e-eeda-49b7-9402-2471d4d14bdf}
 * Alias: FileTemplate
 * Caption: $CardTypes_TypesNames_FileTemplate
 * Group: Dictionaries
 */
class FileTemplateTypeInfo {
  //#region Common

  /**
   * Card type identifier for "FileTemplate": {b7e1b93e-eeda-49b7-9402-2471d4d14bdf}.
   */
  readonly ID: guid = 'b7e1b93e-eeda-49b7-9402-2471d4d14bdf';

  /**
   * Card type name for "FileTemplate".
   */
  readonly Alias: string = 'FileTemplate';

  /**
   * Card type caption for "FileTemplate".
   */
  readonly Caption: string = '$CardTypes_TypesNames_FileTemplate';

  /**
   * Card type group for "FileTemplate".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormFileTemplate: string = 'FileTemplate';
  readonly FormTab: string = 'Tab';

  //#endregion

  //#region Blocks

  readonly BlockMainInfoBlock: string = 'MainInfoBlock';
  readonly BlockCardTypesBlock: string = 'CardTypesBlock';
  readonly BlockViewsBlock: string = 'ViewsBlock';
  readonly BlockBlock5: string = 'Block5';
  readonly BlockBlock2: string = 'Block2';

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly CardTypesControl: string = 'CardTypesControl';
  readonly ViewsControl: string = 'ViewsControl';
  readonly ConvertToPDF: string = 'ConvertToPDF';
  readonly CompileButton: string = 'CompileButton';
  readonly Files: string = 'Files';
  readonly CompileExtensionsButton: string = 'CompileExtensionsButton';
  readonly CompileAllButton: string = 'CompileAllButton';
  readonly Extensions: string = 'Extensions';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region FormatSettings

/**
 * ID: {568141c1-e258-487d-85c6-8aaeddf387fc}
 * Alias: FormatSettings
 * Caption: $CardTypes_TypesNames_FormatSettings
 * Group: Settings
 */
class FormatSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "FormatSettings": {568141c1-e258-487d-85c6-8aaeddf387fc}.
   */
  readonly ID: guid = '568141c1-e258-487d-85c6-8aaeddf387fc';

  /**
   * Card type name for "FormatSettings".
   */
  readonly Alias: string = 'FormatSettings';

  /**
   * Card type caption for "FormatSettings".
   */
  readonly Caption: string = '$CardTypes_TypesNames_FormatSettings';

  /**
   * Card type group for "FormatSettings".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormFormatSettings: string = 'FormatSettings';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockDateTimeInfo: string = 'DateTimeInfo';
  readonly BlockNumberInfo: string = 'NumberInfo';

  //#endregion

  //#region Controls

  readonly UpdateFromCultureInfo: string = 'UpdateFromCultureInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region ForumSatellite

/**
 * ID: {48e7c07a-295d-479a-9990-02a1f7a5f7db}
 * Alias: ForumSatellite
 * Caption: ForumSatellite
 * Group: Forums
 */
class ForumSatelliteTypeInfo {
  //#region Common

  /**
   * Card type identifier for "ForumSatellite": {48e7c07a-295d-479a-9990-02a1f7a5f7db}.
   */
  readonly ID: guid = '48e7c07a-295d-479a-9990-02a1f7a5f7db';

  /**
   * Card type name for "ForumSatellite".
   */
  readonly Alias: string = 'ForumSatellite';

  /**
   * Card type caption for "ForumSatellite".
   */
  readonly Caption: string = 'ForumSatellite';

  /**
   * Card type group for "ForumSatellite".
   */
  readonly Group: string = 'Forums';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region FunctionRole

/**
 * ID: {a830094d-6e03-4242-9c17-0d0a8f2fcb33}
 * Alias: FunctionRole
 * Caption: $CardTypes_TypesNames_FunctionRole
 * Group: System
 */
class FunctionRoleTypeInfo {
  //#region Common

  /**
   * Card type identifier for "FunctionRole": {a830094d-6e03-4242-9c17-0d0a8f2fcb33}.
   */
  readonly ID: guid = 'a830094d-6e03-4242-9c17-0d0a8f2fcb33';

  /**
   * Card type name for "FunctionRole".
   */
  readonly Alias: string = 'FunctionRole';

  /**
   * Card type caption for "FunctionRole".
   */
  readonly Caption: string = '$CardTypes_TypesNames_FunctionRole';

  /**
   * Card type group for "FunctionRole".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormFunctionRole: string = 'FunctionRole';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';

  //#endregion

  //#region Controls

  readonly CanBeDeputy: string = 'CanBeDeputy';
  readonly CanTakeInProgress: string = 'CanTakeInProgress';
  readonly HideTaskByDefault: string = 'HideTaskByDefault';
  readonly CanChangeTaskInfo: string = 'CanChangeTaskInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region GeneralUserSettings

/**
 * ID: {39dce9d4-f429-4552-a268-5a9bf9c1ba53}
 * Alias: GeneralUserSettings
 * Caption: $CardTypes_Blocks_GeneralSettings
 * Group: UserSettings
 */
class GeneralUserSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "GeneralUserSettings": {39dce9d4-f429-4552-a268-5a9bf9c1ba53}.
   */
  readonly ID: guid = '39dce9d4-f429-4552-a268-5a9bf9c1ba53';

  /**
   * Card type name for "GeneralUserSettings".
   */
  readonly Alias: string = 'GeneralUserSettings';

  /**
   * Card type caption for "GeneralUserSettings".
   */
  readonly Caption: string = '$CardTypes_Blocks_GeneralSettings';

  /**
   * Card type group for "GeneralUserSettings".
   */
  readonly Group: string = 'UserSettings';

  //#endregion

  //#region Forms

  readonly FormMySettings: string = 'MySettings';

  //#endregion

  //#region Blocks

  readonly BlockGeneralSettings: string = 'GeneralSettings';
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region HelpSection

/**
 * ID: {3442db47-ab96-4d9c-870f-535e1fc8d42f}
 * Alias: HelpSection
 * Caption: $Cards_HelpSection
 * Group: System
 */
class HelpSectionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "HelpSection": {3442db47-ab96-4d9c-870f-535e1fc8d42f}.
   */
  readonly ID: guid = '3442db47-ab96-4d9c-870f-535e1fc8d42f';

  /**
   * Card type name for "HelpSection".
   */
  readonly Alias: string = 'HelpSection';

  /**
   * Card type caption for "HelpSection".
   */
  readonly Caption: string = '$Cards_HelpSection';

  /**
   * Card type group for "HelpSection".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormHelpSection: string = 'HelpSection';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  /**
   * Block caption "Block2" for "Block2".
   */
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  readonly Code: string = 'Code';
  readonly Name: string = 'Name';
  readonly RichText: string = 'RichText';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region HelpSectionDialogs

/**
 * ID: {dc32f94b-82fb-48a1-8c44-79c5240d5f22}
 * Alias: HelpSectionDialogs
 * Caption: $Cards_DefaultCaption
 * Group: System
 */
class HelpSectionDialogsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "HelpSectionDialogs": {dc32f94b-82fb-48a1-8c44-79c5240d5f22}.
   */
  readonly ID: guid = 'dc32f94b-82fb-48a1-8c44-79c5240d5f22';

  /**
   * Card type name for "HelpSectionDialogs".
   */
  readonly Alias: string = 'HelpSectionDialogs';

  /**
   * Card type caption for "HelpSectionDialogs".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "HelpSectionDialogs".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Scheme Items

  readonly Scheme: HelpSectionDialogsSchemeInfoVirtual = new HelpSectionDialogsSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormMainTab: string = 'MainTab';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  /**
   * Block caption "Block2" for "Block2".
   */
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  readonly Code: string = 'Code';
  readonly Name: string = 'Name';
  readonly RichText: string = 'RichText';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region HelpSectionDialogs Scheme Items

//#region SchemeInfo

class HelpSectionDialogsSchemeInfoVirtual {
  readonly HelpSectionDialog: HelpSectionDialogHelpSectionDialogsSchemeInfoVirtual = new HelpSectionDialogHelpSectionDialogsSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region HelpSectionDialog

/**
 * ID: {7ec35ee9-e48b-48b3-a568-1a5eafc5a8f4}
 * Alias: HelpSectionDialog
 */
class HelpSectionDialogHelpSectionDialogsSchemeInfoVirtual {
  private readonly name: string = "HelpSectionDialog";

  //#region Columns

  readonly Name: string = 'Name';
  readonly Code: string = 'Code';
  readonly RichText: string = 'RichText';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region Incoming

/**
 * ID: {001f99fd-5bf3-0679-9b6f-455767af72b5}
 * Alias: Incoming
 * Caption: $CardTypes_TypesNames_Incoming
 * Group: Documents
 */
class IncomingTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Incoming": {001f99fd-5bf3-0679-9b6f-455767af72b5}.
   */
  readonly ID: guid = '001f99fd-5bf3-0679-9b6f-455767af72b5';

  /**
   * Card type name for "Incoming".
   */
  readonly Alias: string = 'Incoming';

  /**
   * Card type caption for "Incoming".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Incoming';

  /**
   * Card type group for "Incoming".
   */
  readonly Group: string = 'Documents';

  //#endregion

  //#region Document Types

  /**
   * Document type identifier for "$KrTypes_DocTypes_Incoming": {93806539-4931-47ad-9d68-7387156c417f}.
   */
  readonly IncomingID: guid = '93806539-4931-47ad-9d68-7387156c417f';

  /**
   * Document type caption for "$KrTypes_DocTypes_Incoming".
   */
  readonly IncomingCaption: string = '$KrTypes_DocTypes_Incoming';

  //#endregion

  //#region Forms

  readonly FormIncoming: string = 'Incoming';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockDepartmentSignedByRecipientsPerformersBlock: string = 'DepartmentSignedByRecipientsPerformersBlock';
  readonly BlockRefsBlock: string = 'RefsBlock';
  readonly BlockBlock5: string = 'Block5';

  //#endregion

  //#region Controls

  readonly PartnerControl: string = 'PartnerControl';
  readonly IncomingRefsControl: string = 'IncomingRefsControl';
  readonly Files: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region InitiatorCondition

/**
 * ID: {8ff1a133-0300-4246-b117-a5455487acc1}
 * Alias: InitiatorCondition
 * Caption: $CardTypes_TypesNames_InitiatorCondition
 * Group: Conditions
 */
class InitiatorConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "InitiatorCondition": {8ff1a133-0300-4246-b117-a5455487acc1}.
   */
  readonly ID: guid = '8ff1a133-0300-4246-b117-a5455487acc1';

  /**
   * Card type name for "InitiatorCondition".
   */
  readonly Alias: string = 'InitiatorCondition';

  /**
   * Card type caption for "InitiatorCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_InitiatorCondition';

  /**
   * Card type group for "InitiatorCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormInitiatorCondition: string = 'InitiatorCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrAcquaintanceAction

/**
 * ID: {956a34eb-8318-4d35-92a2-c0df118c01ea}
 * Alias: KrAcquaintanceAction
 * Caption: $KrActions_Acquaintance
 * Group: KrProcess
 */
class KrAcquaintanceActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrAcquaintanceAction": {956a34eb-8318-4d35-92a2-c0df118c01ea}.
   */
  readonly ID: guid = '956a34eb-8318-4d35-92a2-c0df118c01ea';

  /**
   * Card type name for "KrAcquaintanceAction".
   */
  readonly Alias: string = 'KrAcquaintanceAction';

  /**
   * Card type caption for "KrAcquaintanceAction".
   */
  readonly Caption: string = '$KrActions_Acquaintance';

  /**
   * Card type group for "KrAcquaintanceAction".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrAcquaintanceAction: string = 'KrAcquaintanceAction';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrAcquaintanceStageTypeSettings

/**
 * ID: {728382fe-12b2-444b-b62e-fe4a4d5ac65f}
 * Alias: KrAcquaintanceStageTypeSettings
 * Caption: $KrStages_Acquaintance
 * Group: KrProcess
 */
class KrAcquaintanceStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrAcquaintanceStageTypeSettings": {728382fe-12b2-444b-b62e-fe4a4d5ac65f}.
   */
  readonly ID: guid = '728382fe-12b2-444b-b62e-fe4a4d5ac65f';

  /**
   * Card type name for "KrAcquaintanceStageTypeSettings".
   */
  readonly Alias: string = 'KrAcquaintanceStageTypeSettings';

  /**
   * Card type caption for "KrAcquaintanceStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_Acquaintance';

  /**
   * Card type group for "KrAcquaintanceStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrAcquaintanceStageTypeSettings: string = 'KrAcquaintanceStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrAddFileFromTemplateStageTypeSettings

/**
 * ID: {b196cbd5-a534-4d18-91f9-561f31a2fe89}
 * Alias: KrAddFileFromTemplateStageTypeSettings
 * Caption: $KrStages_AddFromTemplate
 * Group: KrProcess
 */
class KrAddFileFromTemplateStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrAddFileFromTemplateStageTypeSettings": {b196cbd5-a534-4d18-91f9-561f31a2fe89}.
   */
  readonly ID: guid = 'b196cbd5-a534-4d18-91f9-561f31a2fe89';

  /**
   * Card type name for "KrAddFileFromTemplateStageTypeSettings".
   */
  readonly Alias: string = 'KrAddFileFromTemplateStageTypeSettings';

  /**
   * Card type caption for "KrAddFileFromTemplateStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_AddFromTemplate';

  /**
   * Card type group for "KrAddFileFromTemplateStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrAddFileFromTemplateStageTypeSettings: string = 'KrAddFileFromTemplateStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrAdditionalApproval

/**
 * ID: {b3d8eae3-c6bf-4b59-bcc7-461d526c326c}
 * Alias: KrAdditionalApproval
 * Caption: $CardTypes_TypesNames_KrAdditionalApproval
 * Group: KrProcess
 */
class KrAdditionalApprovalTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrAdditionalApproval": {b3d8eae3-c6bf-4b59-bcc7-461d526c326c}.
   */
  readonly ID: guid = 'b3d8eae3-c6bf-4b59-bcc7-461d526c326c';

  /**
   * Card type name for "KrAdditionalApproval".
   */
  readonly Alias: string = 'KrAdditionalApproval';

  /**
   * Card type caption for "KrAdditionalApproval".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrAdditionalApproval';

  /**
   * Card type group for "KrAdditionalApproval".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrAdditionalApproval: string = 'KrAdditionalApproval';
  readonly FormRequestComment: string = 'RequestComment';
  readonly FormRejectForm: string = 'RejectForm';
  readonly FormApprove: string = 'Approve';
  readonly FormDisapprove: string = 'Disapprove';
  readonly FormAdditionalApproval: string = 'AdditionalApproval';

  //#endregion

  //#region Blocks

  readonly BlockBlock2: string = 'Block2';
  readonly BlockCommentBlock: string = 'CommentBlock';
  readonly BlockCommentsBlockShort: string = 'CommentsBlockShort';
  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockBlock1: string = 'Block1';
  readonly BlockAdditionalApprovalBlockShort: string = 'AdditionalApprovalBlockShort';

  //#endregion

  //#region Controls

  readonly KrCommentsTable: string = 'KrCommentsTable';
  readonly KrAdditionalApprovalTable: string = 'KrAdditionalApprovalTable';
  readonly AdditionalApprovalsRequestedInfoTable: string = 'AdditionalApprovalsRequestedInfoTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrAmendingAction

/**
 * ID: {9c530e93-ec3a-48ba-b09c-ee9eceb2173e}
 * Alias: KrAmendingAction
 * Caption: $KrActions_Amending
 * Group: WorkflowActions
 */
class KrAmendingActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrAmendingAction": {9c530e93-ec3a-48ba-b09c-ee9eceb2173e}.
   */
  readonly ID: guid = '9c530e93-ec3a-48ba-b09c-ee9eceb2173e';

  /**
   * Card type name for "KrAmendingAction".
   */
  readonly Alias: string = 'KrAmendingAction';

  /**
   * Card type caption for "KrAmendingAction".
   */
  readonly Caption: string = '$KrActions_Amending';

  /**
   * Card type group for "KrAmendingAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormKrAmendingAction: string = 'KrAmendingAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  readonly ActionEventsTable: string = 'ActionEventsTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrApprovalAction

/**
 * ID: {70762c81-bd23-4580-a3fb-c452604f6e78}
 * Alias: KrApprovalAction
 * Caption: $KrActions_Approval
 * Group: WorkflowActions
 */
class KrApprovalActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrApprovalAction": {70762c81-bd23-4580-a3fb-c452604f6e78}.
   */
  readonly ID: guid = '70762c81-bd23-4580-a3fb-c452604f6e78';

  /**
   * Card type name for "KrApprovalAction".
   */
  readonly Alias: string = 'KrApprovalAction';

  /**
   * Card type caption for "KrApprovalAction".
   */
  readonly Caption: string = '$KrActions_Approval';

  /**
   * Card type group for "KrApprovalAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormKrApprovalAction: string = 'KrApprovalAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockAdditionalApprovalBlock: string = 'AdditionalApprovalBlock';
  readonly BlockKrApprovalAction_CommonSettings: string = 'KrApprovalAction_CommonSettings';
  readonly BlockKrApprovalAction_StageFlags: string = 'KrApprovalAction_StageFlags';
  readonly BlockKrApprovalAction_AdditionalSettings: string = 'KrApprovalAction_AdditionalSettings';
  readonly BlockDisclaimerBlock: string = 'DisclaimerBlock';
  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockBlock4: string = 'Block4';

  //#endregion

  //#region Controls

  readonly AddComputedRoleLink: string = 'AddComputedRoleLink';
  readonly AdditionalApprovers: string = 'AdditionalApprovers';
  readonly AdditionalApprovalContainer: string = 'AdditionalApprovalContainer';
  readonly FlagsTabs: string = 'FlagsTabs';
  readonly CompletionOptionsTable: string = 'CompletionOptionsTable';
  readonly ActionCompletionOptionsTable: string = 'ActionCompletionOptionsTable';
  readonly ActionEventsTable: string = 'ActionEventsTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrApprovalStageTypeSettings

/**
 * ID: {4a377758-2366-47e9-98ac-c5f553974236}
 * Alias: KrApprovalStageTypeSettings
 * Caption: $KrStages_Approval
 * Group: KrProcess
 */
class KrApprovalStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrApprovalStageTypeSettings": {4a377758-2366-47e9-98ac-c5f553974236}.
   */
  readonly ID: guid = '4a377758-2366-47e9-98ac-c5f553974236';

  /**
   * Card type name for "KrApprovalStageTypeSettings".
   */
  readonly Alias: string = 'KrApprovalStageTypeSettings';

  /**
   * Card type caption for "KrApprovalStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_Approval';

  /**
   * Card type group for "KrApprovalStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrApprovalStageTypeSettings: string = 'KrApprovalStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockAdditionalApprovalBlock: string = 'AdditionalApprovalBlock';
  readonly BlockBlock1: string = 'Block1';
  readonly BlockCommonSettings: string = 'CommonSettings';
  readonly BlockStageFlags: string = 'StageFlags';
  readonly BlockAdditionalSettings: string = 'AdditionalSettings';
  readonly BlockDisclaimerBlock: string = 'DisclaimerBlock';
  readonly BlockApprovalStageFlags: string = 'ApprovalStageFlags';

  //#endregion

  //#region Controls

  readonly AdditionalApprovers: string = 'AdditionalApprovers';
  readonly IsParallelFlag: string = 'IsParallelFlag';
  readonly ReturnIfNotApproved: string = 'ReturnIfNotApproved';
  readonly ReturnAfterApproval: string = 'ReturnAfterApproval';
  readonly DisclaimerControl: string = 'DisclaimerControl';
  readonly EditCardFlagControl: string = 'EditCardFlagControl';
  readonly EditFilesFlagControl: string = 'EditFilesFlagControl';
  readonly FlagsTabs: string = 'FlagsTabs';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrApprove

/**
 * ID: {e4d7f6bf-fea9-4a3b-8a5a-e1a0a40de74c}
 * Alias: KrApprove
 * Caption: $CardTypes_TypesNames_KrApprove
 * Group: KrProcess
 */
class KrApproveTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrApprove": {e4d7f6bf-fea9-4a3b-8a5a-e1a0a40de74c}.
   */
  readonly ID: guid = 'e4d7f6bf-fea9-4a3b-8a5a-e1a0a40de74c';

  /**
   * Card type name for "KrApprove".
   */
  readonly Alias: string = 'KrApprove';

  /**
   * Card type caption for "KrApprove".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrApprove';

  /**
   * Card type group for "KrApprove".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrApprove: string = 'KrApprove';
  readonly FormDelegate: string = 'Delegate';
  readonly FormRequestComment: string = 'RequestComment';
  readonly FormApprove: string = 'Approve';
  readonly FormDisapprove: string = 'Disapprove';
  readonly FormAdditionalApproval: string = 'AdditionalApproval';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockCommentBlock: string = 'CommentBlock';
  readonly BlockCommentsBlockShort: string = 'CommentsBlockShort';
  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockAdditionalApprovalBlockShort: string = 'AdditionalApprovalBlockShort';
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  readonly KrCommentsTable: string = 'KrCommentsTable';
  readonly KrAdditionalApprovalTable: string = 'KrAdditionalApprovalTable';
  readonly WithControl: string = 'WithControl';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrAuthorSettings

/**
 * ID: {02dbc094-7f2c-48b0-acf3-1fc6dddf015c}
 * Alias: KrAuthorSettings
 * Caption: KrAuthorSettings
 * Group: KrProcess
 */
class KrAuthorSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrAuthorSettings": {02dbc094-7f2c-48b0-acf3-1fc6dddf015c}.
   */
  readonly ID: guid = '02dbc094-7f2c-48b0-acf3-1fc6dddf015c';

  /**
   * Card type name for "KrAuthorSettings".
   */
  readonly Alias: string = 'KrAuthorSettings';

  /**
   * Card type caption for "KrAuthorSettings".
   */
  readonly Caption: string = 'KrAuthorSettings';

  /**
   * Card type group for "KrAuthorSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrAuthorSettings: string = 'KrAuthorSettings';

  //#endregion

  //#region Blocks

  /**
   * Block caption "AuthorBlock" for "AuthorBlock".
   */
  readonly BlockAuthorBlock: string = 'AuthorBlock';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrCard

/**
 * ID: {21bca3fc-f75f-413b-b5c8-49538cbfc761}
 * Alias: KrCard
 * Caption: $CardTypes_TypesNames_KrCard
 * Group: KrProcess
 */
class KrCardTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrCard": {21bca3fc-f75f-413b-b5c8-49538cbfc761}.
   */
  readonly ID: guid = '21bca3fc-f75f-413b-b5c8-49538cbfc761';

  /**
   * Card type name for "KrCard".
   */
  readonly Alias: string = 'KrCard';

  /**
   * Card type caption for "KrCard".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrCard';

  /**
   * Card type group for "KrCard".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormApprovalProcess: string = 'ApprovalProcess';

  //#endregion

  //#region Blocks

  readonly BlockSummaryBlock: string = 'SummaryBlock';
  readonly BlockStageCommonInfoBlock: string = 'StageCommonInfoBlock';
  readonly BlockApprovalStagesBlock: string = 'ApprovalStagesBlock';
  readonly BlockDisclaimerBlock: string = 'DisclaimerBlock';

  //#endregion

  //#region Controls

  readonly DocStateControl: string = 'DocStateControl';
  readonly DocStateChangedControl: string = 'DocStateChangedControl';
  readonly TimeLimitInput: string = 'TimeLimitInput';
  readonly PlannedInput: string = 'PlannedInput';
  readonly ApprovalStagesTable: string = 'ApprovalStagesTable';
  readonly DisclaimerControl: string = 'DisclaimerControl';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrChangeStateAction

/**
 * ID: {4f07209c-ab6b-44f6-9460-f594c3bdf8a3}
 * Alias: KrChangeStateAction
 * Caption: $KrActions_ChangeState
 * Group: WorkflowActions
 */
class KrChangeStateActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrChangeStateAction": {4f07209c-ab6b-44f6-9460-f594c3bdf8a3}.
   */
  readonly ID: guid = '4f07209c-ab6b-44f6-9460-f594c3bdf8a3';

  /**
   * Card type name for "KrChangeStateAction".
   */
  readonly Alias: string = 'KrChangeStateAction';

  /**
   * Card type caption for "KrChangeStateAction".
   */
  readonly Caption: string = '$KrActions_ChangeState';

  /**
   * Card type group for "KrChangeStateAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormKrChangeStateAction: string = 'KrChangeStateAction';

  //#endregion

  //#region Blocks

  readonly BlockMainBlock: string = 'MainBlock';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrChangeStateStageTypeSettings

/**
 * ID: {784388f6-dad3-4ce2-a8b9-49e73d71784c}
 * Alias: KrChangeStateStageTypeSettings
 * Caption: $KrStages_ChangeState
 * Group: KrProcess
 */
class KrChangeStateStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrChangeStateStageTypeSettings": {784388f6-dad3-4ce2-a8b9-49e73d71784c}.
   */
  readonly ID: guid = '784388f6-dad3-4ce2-a8b9-49e73d71784c';

  /**
   * Card type name for "KrChangeStateStageTypeSettings".
   */
  readonly Alias: string = 'KrChangeStateStageTypeSettings';

  /**
   * Card type caption for "KrChangeStateStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_ChangeState';

  /**
   * Card type group for "KrChangeStateStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrChangeStateStageTypeSettings: string = 'KrChangeStateStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrCheckStateWorkflowTileExtension

/**
 * ID: {9fce6311-1746-412a-9346-77394caebe90}
 * Alias: KrCheckStateWorkflowTileExtension
 * Caption: $Cards_DefaultCaption
 * Group: KrProcess
 */
class KrCheckStateWorkflowTileExtensionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrCheckStateWorkflowTileExtension": {9fce6311-1746-412a-9346-77394caebe90}.
   */
  readonly ID: guid = '9fce6311-1746-412a-9346-77394caebe90';

  /**
   * Card type name for "KrCheckStateWorkflowTileExtension".
   */
  readonly Alias: string = 'KrCheckStateWorkflowTileExtension';

  /**
   * Card type caption for "KrCheckStateWorkflowTileExtension".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "KrCheckStateWorkflowTileExtension".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrCheckStateWorkflowTileExtension: string = 'KrCheckStateWorkflowTileExtension';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrCreateCardStageTypeSettings

/**
 * ID: {d444f8d4-be81-4714-b00d-02172fad1c81}
 * Alias: KrCreateCardStageTypeSettings
 * Caption: $KrStages_CreateCard
 * Group: KrProcess
 */
class KrCreateCardStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrCreateCardStageTypeSettings": {d444f8d4-be81-4714-b00d-02172fad1c81}.
   */
  readonly ID: guid = 'd444f8d4-be81-4714-b00d-02172fad1c81';

  /**
   * Card type name for "KrCreateCardStageTypeSettings".
   */
  readonly Alias: string = 'KrCreateCardStageTypeSettings';

  /**
   * Card type caption for "KrCreateCardStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_CreateCard';

  /**
   * Card type group for "KrCreateCardStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrCreateCardStageTypeSettings: string = 'KrCreateCardStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockCreateCardStageTypeSettings: string = 'CreateCardStageTypeSettings';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrDeregistrationAction

/**
 * ID: {94e91c8c-1336-4c04-87c5-11ceb9839de3}
 * Alias: KrDeregistrationAction
 * Caption: $KrActions_Deregistration
 * Group: WorkflowActions
 */
class KrDeregistrationActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrDeregistrationAction": {94e91c8c-1336-4c04-87c5-11ceb9839de3}.
   */
  readonly ID: guid = '94e91c8c-1336-4c04-87c5-11ceb9839de3';

  /**
   * Card type name for "KrDeregistrationAction".
   */
  readonly Alias: string = 'KrDeregistrationAction';

  /**
   * Card type caption for "KrDeregistrationAction".
   */
  readonly Caption: string = '$KrActions_Deregistration';

  /**
   * Card type group for "KrDeregistrationAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrDialogStageTypeSettings

/**
 * ID: {71464f65-e572-4fba-b54f-3e9f9ef0125a}
 * Alias: KrDialogStageTypeSettings
 * Caption: KrDialogStageTypeSettings
 * Group: KrProcess
 */
class KrDialogStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrDialogStageTypeSettings": {71464f65-e572-4fba-b54f-3e9f9ef0125a}.
   */
  readonly ID: guid = '71464f65-e572-4fba-b54f-3e9f9ef0125a';

  /**
   * Card type name for "KrDialogStageTypeSettings".
   */
  readonly Alias: string = 'KrDialogStageTypeSettings';

  /**
   * Card type caption for "KrDialogStageTypeSettings".
   */
  readonly Caption: string = 'KrDialogStageTypeSettings';

  /**
   * Card type group for "KrDialogStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrDialogStageTypeSettings: string = 'KrDialogStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockBlock1: string = 'Block1';
  readonly BlockKrDialogScriptsBlock: string = 'KrDialogScriptsBlock';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrDocState

/**
 * ID: {e83a230a-f5fc-445e-9b44-7d0140ee69f6}
 * Alias: KrDocState
 * Caption: $CardTypes_TypesNames_KrDocState
 * Group: Dictionaries
 */
class KrDocStateTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrDocState": {e83a230a-f5fc-445e-9b44-7d0140ee69f6}.
   */
  readonly ID: guid = 'e83a230a-f5fc-445e-9b44-7d0140ee69f6';

  /**
   * Card type name for "KrDocState".
   */
  readonly Alias: string = 'KrDocState';

  /**
   * Card type caption for "KrDocState".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrDocState';

  /**
   * Card type group for "KrDocState".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormKrDocState: string = 'KrDocState';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrDocType

/**
 * ID: {b17f4f35-17e1-4509-994b-ebd576f2c95e}
 * Alias: KrDocType
 * Caption: $CardTypes_TypesNames_KrDocType
 * Group: Dictionaries
 */
class KrDocTypeTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrDocType": {b17f4f35-17e1-4509-994b-ebd576f2c95e}.
   */
  readonly ID: guid = 'b17f4f35-17e1-4509-994b-ebd576f2c95e';

  /**
   * Card type name for "KrDocType".
   */
  readonly Alias: string = 'KrDocType';

  /**
   * Card type caption for "KrDocType".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrDocType';

  /**
   * Card type group for "KrDocType".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormKrDocType: string = 'KrDocType';

  //#endregion

  //#region Blocks

  readonly BlockMainInfoBlock: string = 'MainInfoBlock';
  readonly BlockUseApprovingBlock: string = 'UseApprovingBlock';
  readonly BlockAutoApprovalSettingsBlock1: string = 'AutoApprovalSettingsBlock1';
  readonly BlockAutoApprovalSettingsBlock2: string = 'AutoApprovalSettingsBlock2';
  readonly BlockApprovalSettingsBlock: string = 'ApprovalSettingsBlock';
  readonly BlockUseRegistrationBlock: string = 'UseRegistrationBlock';
  readonly BlockRegistrationSettingsBlock: string = 'RegistrationSettingsBlock';
  readonly BlockUseResolutionsBlock: string = 'UseResolutionsBlock';
  readonly BlockUseForumBlock: string = 'UseForumBlock';

  //#endregion

  //#region Controls

  readonly TitleControl: string = 'TitleControl';
  readonly CardTypeControl: string = 'CardTypeControl';
  readonly DescriptionControl: string = 'DescriptionControl';
  readonly HideCreationButton: string = 'HideCreationButton';
  readonly UseApprovingControl: string = 'UseApprovingControl';
  readonly UseAutoApprovingControl: string = 'UseAutoApprovingControl';
  readonly HideRouteTab: string = 'HideRouteTab';
  readonly UseRoutesInWorkflowEngine: string = 'UseRoutesInWorkflowEngine';
  readonly UseRegistrationControl: string = 'UseRegistrationControl';
  readonly UseResolutionsControl: string = 'UseResolutionsControl';
  readonly DisableChildResolutionDateCheck_UseResolutions: string = 'DisableChildResolutionDateCheck_UseResolutions';

  /**
   * Control caption "Warning" for "ForumLicenseWarning".
   */
  readonly ForumLicenseWarning: string = 'ForumLicenseWarning';
  readonly UseDefaultDiscussionTab_UseForum: string = 'UseDefaultDiscussionTab_UseForum';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrEdit

/**
 * ID: {e19ca9b5-48be-4fdf-8dc5-78534b4767de}
 * Alias: KrEdit
 * Caption: $CardTypes_TypesNames_KrEdit
 * Group: KrProcess
 */
class KrEditTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrEdit": {e19ca9b5-48be-4fdf-8dc5-78534b4767de}.
   */
  readonly ID: guid = 'e19ca9b5-48be-4fdf-8dc5-78534b4767de';

  /**
   * Card type name for "KrEdit".
   */
  readonly Alias: string = 'KrEdit';

  /**
   * Card type caption for "KrEdit".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrEdit';

  /**
   * Card type group for "KrEdit".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrEdit: string = 'KrEdit';
  readonly FormNewCycle: string = 'NewCycle';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly Comment: string = 'Comment';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrEditInterject

/**
 * ID: {c9b93ae3-9b7b-4431-a306-aace4aea8732}
 * Alias: KrEditInterject
 * Caption: $CardTypes_TypesNames_KrEditInterject
 * Group: KrProcess
 */
class KrEditInterjectTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrEditInterject": {c9b93ae3-9b7b-4431-a306-aace4aea8732}.
   */
  readonly ID: guid = 'c9b93ae3-9b7b-4431-a306-aace4aea8732';

  /**
   * Card type name for "KrEditInterject".
   */
  readonly Alias: string = 'KrEditInterject';

  /**
   * Card type caption for "KrEditInterject".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrEditInterject';

  /**
   * Card type group for "KrEditInterject".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrEditInterject: string = 'KrEditInterject';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrEditStageTypeSettings

/**
 * ID: {995621e3-fdcf-412b-91a6-3f28fe933e70}
 * Alias: KrEditStageTypeSettings
 * Caption: $KrStages_Edit
 * Group: KrProcess
 */
class KrEditStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrEditStageTypeSettings": {995621e3-fdcf-412b-91a6-3f28fe933e70}.
   */
  readonly ID: guid = '995621e3-fdcf-412b-91a6-3f28fe933e70';

  /**
   * Card type name for "KrEditStageTypeSettings".
   */
  readonly Alias: string = 'KrEditStageTypeSettings';

  /**
   * Card type caption for "KrEditStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_Edit';

  /**
   * Card type group for "KrEditStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrEditStageTypeSettings: string = 'KrEditStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockMainBlock: string = 'MainBlock';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrExampleDialogSatellite

/**
 * ID: {7cfe67a4-0b8e-423b-8c15-8e2c584b429b}
 * Alias: KrExampleDialogSatellite
 * Caption: KrExampleDialogSatellite
 * Group: KrProcess
 */
class KrExampleDialogSatelliteTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrExampleDialogSatellite": {7cfe67a4-0b8e-423b-8c15-8e2c584b429b}.
   */
  readonly ID: guid = '7cfe67a4-0b8e-423b-8c15-8e2c584b429b';

  /**
   * Card type name for "KrExampleDialogSatellite".
   */
  readonly Alias: string = 'KrExampleDialogSatellite';

  /**
   * Card type caption for "KrExampleDialogSatellite".
   */
  readonly Caption: string = 'KrExampleDialogSatellite';

  /**
   * Card type group for "KrExampleDialogSatellite".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrExampleDialogSatellite: string = 'KrExampleDialogSatellite';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrForkManagementStageTypeSettings

/**
 * ID: {9393407b-d4ff-408b-abbc-de7ce148ea54}
 * Alias: KrForkManagementStageTypeSettings
 * Caption: $KrStages_ForkManagement
 * Group: KrProcess
 */
class KrForkManagementStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrForkManagementStageTypeSettings": {9393407b-d4ff-408b-abbc-de7ce148ea54}.
   */
  readonly ID: guid = '9393407b-d4ff-408b-abbc-de7ce148ea54';

  /**
   * Card type name for "KrForkManagementStageTypeSettings".
   */
  readonly Alias: string = 'KrForkManagementStageTypeSettings';

  /**
   * Card type caption for "KrForkManagementStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_ForkManagement';

  /**
   * Card type group for "KrForkManagementStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrForkManagementStageTypeSettings: string = 'KrForkManagementStageTypeSettings';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrForkStageTypeSettings

/**
 * ID: {2729c019-fab9-4eb4-bd98-d3628b1a19f6}
 * Alias: KrForkStageTypeSettings
 * Caption: $KrStages_Fork
 * Group: KrProcess
 */
class KrForkStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrForkStageTypeSettings": {2729c019-fab9-4eb4-bd98-d3628b1a19f6}.
   */
  readonly ID: guid = '2729c019-fab9-4eb4-bd98-d3628b1a19f6';

  /**
   * Card type name for "KrForkStageTypeSettings".
   */
  readonly Alias: string = 'KrForkStageTypeSettings';

  /**
   * Card type caption for "KrForkStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_Fork';

  /**
   * Card type group for "KrForkStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrForkStageTypeSettings: string = 'KrForkStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrHistoryManagementStageTypeSettings

/**
 * ID: {cfe5e2af-1014-4ddb-afa1-7450623b103a}
 * Alias: KrHistoryManagementStageTypeSettings
 * Caption: $KrStages_HistoryManagement
 * Group: KrProcess
 */
class KrHistoryManagementStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrHistoryManagementStageTypeSettings": {cfe5e2af-1014-4ddb-afa1-7450623b103a}.
   */
  readonly ID: guid = 'cfe5e2af-1014-4ddb-afa1-7450623b103a';

  /**
   * Card type name for "KrHistoryManagementStageTypeSettings".
   */
  readonly Alias: string = 'KrHistoryManagementStageTypeSettings';

  /**
   * Card type caption for "KrHistoryManagementStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_HistoryManagement';

  /**
   * Card type group for "KrHistoryManagementStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrHistoryManagementStageTypeSettings: string = 'KrHistoryManagementStageTypeSettings';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "KrTaskHistoryBlockAlias".
   */
  readonly BlockKrTaskHistoryBlockAlias: string = 'KrTaskHistoryBlockAlias';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrInfoForInitiator

/**
 * ID: {c6f3828f-b001-46f6-b121-3f3ed9e65cde}
 * Alias: KrInfoForInitiator
 * Caption: $CardTypes_TypesNames_KrInfoForInitiator
 * Group: KrProcess
 */
class KrInfoForInitiatorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrInfoForInitiator": {c6f3828f-b001-46f6-b121-3f3ed9e65cde}.
   */
  readonly ID: guid = 'c6f3828f-b001-46f6-b121-3f3ed9e65cde';

  /**
   * Card type name for "KrInfoForInitiator".
   */
  readonly Alias: string = 'KrInfoForInitiator';

  /**
   * Card type caption for "KrInfoForInitiator".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrInfoForInitiator';

  /**
   * Card type group for "KrInfoForInitiator".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrInfoForInitiator: string = 'KrInfoForInitiator';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrNotificationStageTypeSettings

/**
 * ID: {9e57dfaf-986e-41c1-a1c0-be007f0a36a0}
 * Alias: KrNotificationStageTypeSettings
 * Caption: $KrStages_Notification
 * Group: KrProcess
 */
class KrNotificationStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrNotificationStageTypeSettings": {9e57dfaf-986e-41c1-a1c0-be007f0a36a0}.
   */
  readonly ID: guid = '9e57dfaf-986e-41c1-a1c0-be007f0a36a0';

  /**
   * Card type name for "KrNotificationStageTypeSettings".
   */
  readonly Alias: string = 'KrNotificationStageTypeSettings';

  /**
   * Card type caption for "KrNotificationStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_Notification';

  /**
   * Card type group for "KrNotificationStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrNotificationStageTypeSettings: string = 'KrNotificationStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrPerformersSettings

/**
 * ID: {5ac67186-b62b-4dc4-b9b9-e74d18f53600}
 * Alias: KrPerformersSettings
 * Caption: KrPerformersSettings
 * Group: KrProcess
 */
class KrPerformersSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrPerformersSettings": {5ac67186-b62b-4dc4-b9b9-e74d18f53600}.
   */
  readonly ID: guid = '5ac67186-b62b-4dc4-b9b9-e74d18f53600';

  /**
   * Card type name for "KrPerformersSettings".
   */
  readonly Alias: string = 'KrPerformersSettings';

  /**
   * Card type caption for "KrPerformersSettings".
   */
  readonly Caption: string = 'KrPerformersSettings';

  /**
   * Card type group for "KrPerformersSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrPerformersSettings: string = 'KrPerformersSettings';

  //#endregion

  //#region Blocks

  /**
   * Block caption "PerformersBlock" for "PerformersBlock".
   */
  readonly BlockPerformersBlock: string = 'PerformersBlock';

  //#endregion

  //#region Controls

  readonly SinglePerformerEntryAC: string = 'SinglePerformerEntryAC';
  readonly MultiplePerformersTableAC: string = 'MultiplePerformersTableAC';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrPermissions

/**
 * ID: {fa9dbdac-8708-41df-bd72-900f69655dfa}
 * Alias: KrPermissions
 * Caption: $CardTypes_TypesNames_KrPermissions
 * Group: Settings
 */
class KrPermissionsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrPermissions": {fa9dbdac-8708-41df-bd72-900f69655dfa}.
   */
  readonly ID: guid = 'fa9dbdac-8708-41df-bd72-900f69655dfa';

  /**
   * Card type name for "KrPermissions".
   */
  readonly Alias: string = 'KrPermissions';

  /**
   * Card type caption for "KrPermissions".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrPermissions';

  /**
   * Card type group for "KrPermissions".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormKrPermissions: string = 'KrPermissions';
  readonly FormTab: string = 'Tab';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  /**
   * Block caption "Block5" for "Block5".
   */
  readonly BlockBlock5: string = 'Block5';
  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockBlock4: string = 'Block4';
  readonly BlockAclGenerationRules: string = 'AclGenerationRules';
  readonly BlockAdditionalSettings: string = 'AdditionalSettings';

  //#endregion

  //#region Controls

  readonly AclGenerationRules: string = 'AclGenerationRules';
  readonly Priority: string = 'Priority';
  readonly Fields: string = 'Fields';
  readonly Mask: string = 'Mask';
  readonly CardExtendedSettings: string = 'CardExtendedSettings';
  readonly TasksExtendedSettings: string = 'TasksExtendedSettings';
  readonly TaskTypes: string = 'TaskTypes';
  readonly CompletionOptions: string = 'CompletionOptions';
  readonly MandatoryExtendedSettings: string = 'MandatoryExtendedSettings';
  readonly VisibilityExtendedSettings: string = 'VisibilityExtendedSettings';
  readonly FileReadAccessSetting: string = 'FileReadAccessSetting';
  readonly FileAddAccessSetting: string = 'FileAddAccessSetting';
  readonly FileEditAccessSetting: string = 'FileEditAccessSetting';
  readonly FileDeleteAccessSetting: string = 'FileDeleteAccessSetting';
  readonly FileSignAccessSetting: string = 'FileSignAccessSetting';
  readonly FileExtendedPermissionsSettings: string = 'FileExtendedPermissionsSettings';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrProcessManagementStageTypeSettings

/**
 * ID: {ff753641-0691-4cfc-a8cc-baa89b25a83b}
 * Alias: KrProcessManagementStageTypeSettings
 * Caption: $KrStages_ProcessManagement
 * Group: KrProcess
 */
class KrProcessManagementStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrProcessManagementStageTypeSettings": {ff753641-0691-4cfc-a8cc-baa89b25a83b}.
   */
  readonly ID: guid = 'ff753641-0691-4cfc-a8cc-baa89b25a83b';

  /**
   * Card type name for "KrProcessManagementStageTypeSettings".
   */
  readonly Alias: string = 'KrProcessManagementStageTypeSettings';

  /**
   * Card type caption for "KrProcessManagementStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_ProcessManagement';

  /**
   * Card type group for "KrProcessManagementStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrProcessManagementStageTypeSettings: string = 'KrProcessManagementStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrRegistration

/**
 * ID: {09fdd6a3-3946-4f30-9ef9-f533fad3a4a2}
 * Alias: KrRegistration
 * Caption: $CardTypes_TypesNames_KrRegistration
 * Group: KrProcess
 */
class KrRegistrationTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrRegistration": {09fdd6a3-3946-4f30-9ef9-f533fad3a4a2}.
   */
  readonly ID: guid = '09fdd6a3-3946-4f30-9ef9-f533fad3a4a2';

  /**
   * Card type name for "KrRegistration".
   */
  readonly Alias: string = 'KrRegistration';

  /**
   * Card type caption for "KrRegistration".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrRegistration';

  /**
   * Card type group for "KrRegistration".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrRegistrationAction

/**
 * ID: {bf4641ad-f4dc-4a75-83f4-534cba8bf225}
 * Alias: KrRegistrationAction
 * Caption: $KrActions_Registration
 * Group: WorkflowActions
 */
class KrRegistrationActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrRegistrationAction": {bf4641ad-f4dc-4a75-83f4-534cba8bf225}.
   */
  readonly ID: guid = 'bf4641ad-f4dc-4a75-83f4-534cba8bf225';

  /**
   * Card type name for "KrRegistrationAction".
   */
  readonly Alias: string = 'KrRegistrationAction';

  /**
   * Card type caption for "KrRegistrationAction".
   */
  readonly Caption: string = '$KrActions_Registration';

  /**
   * Card type group for "KrRegistrationAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrRegistrationStageTypeSettings

/**
 * ID: {d92e4659-a66b-4efa-aa29-716953da636a}
 * Alias: KrRegistrationStageTypeSettings
 * Caption: $Cards_DefaultCaption
 * Group: KrProcess
 */
class KrRegistrationStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrRegistrationStageTypeSettings": {d92e4659-a66b-4efa-aa29-716953da636a}.
   */
  readonly ID: guid = 'd92e4659-a66b-4efa-aa29-716953da636a';

  /**
   * Card type name for "KrRegistrationStageTypeSettings".
   */
  readonly Alias: string = 'KrRegistrationStageTypeSettings';

  /**
   * Card type caption for "KrRegistrationStageTypeSettings".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "KrRegistrationStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrRegistrationStageTypeSettings: string = 'KrRegistrationStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockDisclaimerBlock: string = 'DisclaimerBlock';

  //#endregion

  //#region Controls

  readonly EditCardFlagControl: string = 'EditCardFlagControl';
  readonly EditFilesFlagControl: string = 'EditFilesFlagControl';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrRequestComment

/**
 * ID: {f0360d95-4f88-4809-b926-57b34a2f69f5}
 * Alias: KrRequestComment
 * Caption: $CardTypes_TypesNames_KrRequestComment
 * Group: KrProcess
 */
class KrRequestCommentTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrRequestComment": {f0360d95-4f88-4809-b926-57b34a2f69f5}.
   */
  readonly ID: guid = 'f0360d95-4f88-4809-b926-57b34a2f69f5';

  /**
   * Card type name for "KrRequestComment".
   */
  readonly Alias: string = 'KrRequestComment';

  /**
   * Card type caption for "KrRequestComment".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrRequestComment';

  /**
   * Card type group for "KrRequestComment".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrRequestComment: string = 'KrRequestComment';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly Comment: string = 'Comment';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrResolutionAction

/**
 * ID: {235e42ea-7ad8-4321-9a3a-91b752985ef0}
 * Alias: KrResolutionAction
 * Caption: $KrActions_Resolution
 * Group: WorkflowActions
 */
class KrResolutionActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrResolutionAction": {235e42ea-7ad8-4321-9a3a-91b752985ef0}.
   */
  readonly ID: guid = '235e42ea-7ad8-4321-9a3a-91b752985ef0';

  /**
   * Card type name for "KrResolutionAction".
   */
  readonly Alias: string = 'KrResolutionAction';

  /**
   * Card type caption for "KrResolutionAction".
   */
  readonly Caption: string = '$KrActions_Resolution';

  /**
   * Card type group for "KrResolutionAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormKrResolutionAction: string = 'KrResolutionAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly AddComputedRoleLink: string = 'AddComputedRoleLink';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrResolutionStageTypeSettings

/**
 * ID: {c898080f-0fa7-45d9-bbc9-f28dfd2c8f1c}
 * Alias: KrResolutionStageTypeSettings
 * Caption: $KrStages_Resolution
 * Group: KrProcess
 */
class KrResolutionStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrResolutionStageTypeSettings": {c898080f-0fa7-45d9-bbc9-f28dfd2c8f1c}.
   */
  readonly ID: guid = 'c898080f-0fa7-45d9-bbc9-f28dfd2c8f1c';

  /**
   * Card type name for "KrResolutionStageTypeSettings".
   */
  readonly Alias: string = 'KrResolutionStageTypeSettings';

  /**
   * Card type caption for "KrResolutionStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_Resolution';

  /**
   * Card type group for "KrResolutionStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrResolutionStageTypeSettings: string = 'KrResolutionStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockSender: string = 'Sender';
  readonly BlockPerformers: string = 'Performers';
  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockMoreInfo: string = 'MoreInfo';

  //#endregion

  //#region Controls

  readonly MassCreation: string = 'MassCreation';
  readonly MajorPerformer: string = 'MajorPerformer';
  readonly Controller: string = 'Controller';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrRouteInitializationAction

/**
 * ID: {25ca876a-50b2-4c27-b847-56d4fc597934}
 * Alias: KrRouteInitializationAction
 * Caption: $KrActions_RouteInitialization
 * Group: WorkflowActions
 */
class KrRouteInitializationActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrRouteInitializationAction": {25ca876a-50b2-4c27-b847-56d4fc597934}.
   */
  readonly ID: guid = '25ca876a-50b2-4c27-b847-56d4fc597934';

  /**
   * Card type name for "KrRouteInitializationAction".
   */
  readonly Alias: string = 'KrRouteInitializationAction';

  /**
   * Card type caption for "KrRouteInitializationAction".
   */
  readonly Caption: string = '$KrActions_RouteInitialization';

  /**
   * Card type group for "KrRouteInitializationAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormKrRouteInitializationAction: string = 'KrRouteInitializationAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrSamplePermissionsExtensionType

/**
 * ID: {3e00b804-7905-484e-a50b-191ad1a118a2}
 * Alias: KrSamplePermissionsExtensionType
 * Caption: An example of card type used to extend access rules in standard solution
 * Group: Permissions
 */
class KrSamplePermissionsExtensionTypeTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrSamplePermissionsExtensionType": {3e00b804-7905-484e-a50b-191ad1a118a2}.
   */
  readonly ID: guid = '3e00b804-7905-484e-a50b-191ad1a118a2';

  /**
   * Card type name for "KrSamplePermissionsExtensionType".
   */
  readonly Alias: string = 'KrSamplePermissionsExtensionType';

  /**
   * Card type caption for "KrSamplePermissionsExtensionType".
   */
  readonly Caption: string = 'An example of card type used to extend access rules in standard solution';

  /**
   * Card type group for "KrSamplePermissionsExtensionType".
   */
  readonly Group: string = 'Permissions';

  //#endregion

  //#region Forms

  readonly FormKrSamplePermissionsExtensionType: string = 'KrSamplePermissionsExtensionType';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Amount" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrSatellite

/**
 * ID: {4115f07e-0aaa-4563-a749-0450c1a850af}
 * Alias: KrSatellite
 * Caption: $CardTypes_TypesNames_KrSatellite
 * Group: KrProcess
 */
class KrSatelliteTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrSatellite": {4115f07e-0aaa-4563-a749-0450c1a850af}.
   */
  readonly ID: guid = '4115f07e-0aaa-4563-a749-0450c1a850af';

  /**
   * Card type name for "KrSatellite".
   */
  readonly Alias: string = 'KrSatellite';

  /**
   * Card type caption for "KrSatellite".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrSatellite';

  /**
   * Card type group for "KrSatellite".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrSecondaryProcess

/**
 * ID: {61420fa1-cc1f-47cb-b0bb-4ea8ee77f51a}
 * Alias: KrSecondaryProcess
 * Caption: $CardTypes_TypesNames_KrSecondaryProcess
 * Group: Routes
 */
class KrSecondaryProcessTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrSecondaryProcess": {61420fa1-cc1f-47cb-b0bb-4ea8ee77f51a}.
   */
  readonly ID: guid = '61420fa1-cc1f-47cb-b0bb-4ea8ee77f51a';

  /**
   * Card type name for "KrSecondaryProcess".
   */
  readonly Alias: string = 'KrSecondaryProcess';

  /**
   * Card type caption for "KrSecondaryProcess".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrSecondaryProcess';

  /**
   * Card type group for "KrSecondaryProcess".
   */
  readonly Group: string = 'Routes';

  //#endregion

  //#region Forms

  readonly FormKrSecondaryProcess: string = 'KrSecondaryProcess';
  readonly FormCompilerOutput: string = 'CompilerOutput';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';
  readonly BlockMainInformationBlock: string = 'MainInformationBlock';
  readonly BlockPureProcessParametersBlock: string = 'PureProcessParametersBlock';
  readonly BlockActionParametersBlock: string = 'ActionParametersBlock';

  /**
   * Block caption "Block2" for "Block2".
   */
  readonly BlockBlock2: string = 'Block2';

  /**
   * Block caption "Block3" for "Block3".
   */
  readonly BlockBlock3: string = 'Block3';
  readonly BlockTileParametersBlock: string = 'TileParametersBlock';
  readonly BlockExecutionAccessDeniedBlock: string = 'ExecutionAccessDeniedBlock';
  readonly BlockRestictionsBlock: string = 'RestictionsBlock';
  readonly BlockRouteParametersBlock: string = 'RouteParametersBlock';
  readonly BlockVisibilityScriptsBlock: string = 'VisibilityScriptsBlock';
  readonly BlockExecutionScriptsBlock: string = 'ExecutionScriptsBlock';
  readonly BlockBlock4: string = 'Block4';

  //#endregion

  //#region Controls

  readonly CheckRecalcRestrictionsCheckbox: string = 'CheckRecalcRestrictionsCheckbox';
  readonly CompileButton: string = 'CompileButton';
  readonly CompileAllButton: string = 'CompileAllButton';
  readonly CompilerOutputTable: string = 'CompilerOutputTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrSecondarySatellite

/**
 * ID: {7593c144-31f7-43c2-9c4b-e3b776562f8f}
 * Alias: KrSecondarySatellite
 * Caption: $CardTypes_TypesNames_KrSecondarySatellite
 * Group: KrProcess
 */
class KrSecondarySatelliteTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrSecondarySatellite": {7593c144-31f7-43c2-9c4b-e3b776562f8f}.
   */
  readonly ID: guid = '7593c144-31f7-43c2-9c4b-e3b776562f8f';

  /**
   * Card type name for "KrSecondarySatellite".
   */
  readonly Alias: string = 'KrSecondarySatellite';

  /**
   * Card type caption for "KrSecondarySatellite".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrSecondarySatellite';

  /**
   * Card type group for "KrSecondarySatellite".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrSettings

/**
 * ID: {35a03878-57b6-4263-ae36-92eb59032132}
 * Alias: KrSettings
 * Caption: $CardTypes_TypesNames_KrSettings
 * Group: Settings
 */
class KrSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrSettings": {35a03878-57b6-4263-ae36-92eb59032132}.
   */
  readonly ID: guid = '35a03878-57b6-4263-ae36-92eb59032132';

  /**
   * Card type name for "KrSettings".
   */
  readonly Alias: string = 'KrSettings';

  /**
   * Card type caption for "KrSettings".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrSettings';

  /**
   * Card type group for "KrSettings".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormKrSettings: string = 'KrSettings';
  readonly FormStages: string = 'Stages';
  readonly FormGenerator: string = 'Generator';

  //#endregion

  //#region Blocks

  readonly BlockOtherSettings: string = 'OtherSettings';
  readonly BlockTypeSettingsBlock: string = 'TypeSettingsBlock';
  readonly BlockHideCreationButtonBlock: string = 'HideCreationButtonBlock';
  readonly BlockUseApprovingBlock: string = 'UseApprovingBlock';
  readonly BlockAutoApprovalSettingsBlock1: string = 'AutoApprovalSettingsBlock1';
  readonly BlockAutoApprovalSettingsBlock2: string = 'AutoApprovalSettingsBlock2';
  readonly BlockApprovalSettingsBlock: string = 'ApprovalSettingsBlock';
  readonly BlockUseRegistrationBlock: string = 'UseRegistrationBlock';
  readonly BlockRegistrationSettingsBlock: string = 'RegistrationSettingsBlock';
  readonly BlockUseResolutionsBlock: string = 'UseResolutionsBlock';
  readonly BlockUseForumBlock: string = 'UseForumBlock';
  readonly BlockCardTypesBlock: string = 'CardTypesBlock';
  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock4: string = 'Block4';

  //#endregion

  //#region Controls

  readonly AclReadCardAccess: string = 'AclReadCardAccess';
  readonly UseDocTypesControl: string = 'UseDocTypesControl';
  readonly HideCreationButton: string = 'HideCreationButton';
  readonly UseApprovingControl: string = 'UseApprovingControl';
  readonly UseAutoApprovingControl: string = 'UseAutoApprovingControl';
  readonly HideRouteTab: string = 'HideRouteTab';
  readonly UseRoutesInWorkflowEngine: string = 'UseRoutesInWorkflowEngine';
  readonly UseRegistrationControl: string = 'UseRegistrationControl';
  readonly UseResolutionsControl: string = 'UseResolutionsControl';
  readonly DisableChildResolutionDateCheck_UseResolutions: string = 'DisableChildResolutionDateCheck_UseResolutions';

  /**
   * Control caption "Warning" for "ForumLicenseWarning".
   */
  readonly ForumLicenseWarning: string = 'ForumLicenseWarning';
  readonly UseDefaultDiscussionTab_UseForum: string = 'UseDefaultDiscussionTab_UseForum';
  readonly CardTypeControl: string = 'CardTypeControl';

  /**
   * Control caption "Warning" for "AdvancedNotificationsTooltip".
   */
  readonly AdvancedNotificationsTooltip: string = 'AdvancedNotificationsTooltip';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrShowDialog

/**
 * ID: {5309ce42-c4d2-4e99-a733-697c589311e7}
 * Alias: KrShowDialog
 * Caption: $CardTypes_TypesNames_ShowDialog
 * Group: KrProcess
 */
class KrShowDialogTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrShowDialog": {5309ce42-c4d2-4e99-a733-697c589311e7}.
   */
  readonly ID: guid = '5309ce42-c4d2-4e99-a733-697c589311e7';

  /**
   * Card type name for "KrShowDialog".
   */
  readonly Alias: string = 'KrShowDialog';

  /**
   * Card type caption for "KrShowDialog".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ShowDialog';

  /**
   * Card type group for "KrShowDialog".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrSigning

/**
 * ID: {968d68b3-a7c5-4b5d-bfa4-bb0f346880b6}
 * Alias: KrSigning
 * Caption: $CardTypes_TypesNames_KrSigning
 * Group: KrProcess
 */
class KrSigningTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrSigning": {968d68b3-a7c5-4b5d-bfa4-bb0f346880b6}.
   */
  readonly ID: guid = '968d68b3-a7c5-4b5d-bfa4-bb0f346880b6';

  /**
   * Card type name for "KrSigning".
   */
  readonly Alias: string = 'KrSigning';

  /**
   * Card type caption for "KrSigning".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrSigning';

  /**
   * Card type group for "KrSigning".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrSigning: string = 'KrSigning';
  readonly FormDelegate: string = 'Delegate';
  readonly FormRequestComment: string = 'RequestComment';
  readonly FormSign: string = 'Sign';
  readonly FormDecline: string = 'Decline';
  readonly FormAdditionalApproval: string = 'AdditionalApproval';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockCommentBlock: string = 'CommentBlock';
  readonly BlockCommentsBlockShort: string = 'CommentsBlockShort';
  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockAdditionalApprovalBlockShort: string = 'AdditionalApprovalBlockShort';
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  readonly KrCommentsTable: string = 'KrCommentsTable';
  readonly KrAdditionalApprovalTable: string = 'KrAdditionalApprovalTable';
  readonly WithControl: string = 'WithControl';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrSigningAction

/**
 * ID: {01762690-a192-4e8e-9b5e-0110666fd977}
 * Alias: KrSigningAction
 * Caption: $KrActions_Signing
 * Group: WorkflowActions
 */
class KrSigningActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrSigningAction": {01762690-a192-4e8e-9b5e-0110666fd977}.
   */
  readonly ID: guid = '01762690-a192-4e8e-9b5e-0110666fd977';

  /**
   * Card type name for "KrSigningAction".
   */
  readonly Alias: string = 'KrSigningAction';

  /**
   * Card type caption for "KrSigningAction".
   */
  readonly Caption: string = '$KrActions_Signing';

  /**
   * Card type group for "KrSigningAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormKrSigningAction: string = 'KrSigningAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockKrApprovalAction_CommonSettings: string = 'KrApprovalAction_CommonSettings';
  readonly BlockStageFlags: string = 'StageFlags';
  readonly BlockKrApprovalAction_AdditionalSettings: string = 'KrApprovalAction_AdditionalSettings';
  readonly BlockDisclaimerBlock: string = 'DisclaimerBlock';
  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockBlock4: string = 'Block4';

  //#endregion

  //#region Controls

  readonly AddComputedRoleLink: string = 'AddComputedRoleLink';
  readonly FlagsTabs: string = 'FlagsTabs';
  readonly CompletionOptionsTable: string = 'CompletionOptionsTable';
  readonly ActionCompletionOptionsTable: string = 'ActionCompletionOptionsTable';
  readonly ActionEventsTable: string = 'ActionEventsTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrSigningStageTypeSettings

/**
 * ID: {5c473877-1e54-495c-8eca-74885d292786}
 * Alias: KrSigningStageTypeSettings
 * Caption: $KrStages_Signing
 * Group: KrProcess
 */
class KrSigningStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrSigningStageTypeSettings": {5c473877-1e54-495c-8eca-74885d292786}.
   */
  readonly ID: guid = '5c473877-1e54-495c-8eca-74885d292786';

  /**
   * Card type name for "KrSigningStageTypeSettings".
   */
  readonly Alias: string = 'KrSigningStageTypeSettings';

  /**
   * Card type caption for "KrSigningStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_Signing';

  /**
   * Card type group for "KrSigningStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrSigningStageTypeSettings: string = 'KrSigningStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockCommonSettings: string = 'CommonSettings';
  readonly BlockStageFlags: string = 'StageFlags';
  readonly BlockAdditionalSettings: string = 'AdditionalSettings';
  readonly BlockDisclaimerBlock: string = 'DisclaimerBlock';
  readonly BlockSigningStageFlags: string = 'SigningStageFlags';

  //#endregion

  //#region Controls

  readonly IsParallelFlag: string = 'IsParallelFlag';
  readonly ReturnIfNotSigned: string = 'ReturnIfNotSigned';
  readonly AllowAdditionalApproval: string = 'AllowAdditionalApproval';
  readonly ReturnAfterSigning: string = 'ReturnAfterSigning';
  readonly DisclaimerControl: string = 'DisclaimerControl';
  readonly EditCardFlagControl: string = 'EditCardFlagControl';
  readonly EditFilesFlagControl: string = 'EditFilesFlagControl';
  readonly FlagsTabs: string = 'FlagsTabs';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrStageCommonMethod

/**
 * ID: {66cd517b-5423-43db-8374-f50ec0d967eb}
 * Alias: KrStageCommonMethod
 * Caption: $CardTypes_TypesNames_KrCommonMethod
 * Group: Routes
 */
class KrStageCommonMethodTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrStageCommonMethod": {66cd517b-5423-43db-8374-f50ec0d967eb}.
   */
  readonly ID: guid = '66cd517b-5423-43db-8374-f50ec0d967eb';

  /**
   * Card type name for "KrStageCommonMethod".
   */
  readonly Alias: string = 'KrStageCommonMethod';

  /**
   * Card type caption for "KrStageCommonMethod".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrCommonMethod';

  /**
   * Card type group for "KrStageCommonMethod".
   */
  readonly Group: string = 'Routes';

  //#endregion

  //#region Forms

  readonly FormKrStageCommonMethod: string = 'KrStageCommonMethod';
  readonly FormCompilerOutput: string = 'CompilerOutput';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock4: string = 'Block4';

  //#endregion

  //#region Controls

  readonly CompileButton: string = 'CompileButton';
  readonly CompileAllButton: string = 'CompileAllButton';
  readonly CompilerOutputTable: string = 'CompilerOutputTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrStageGroup

/**
 * ID: {9ce8e9f4-cbf0-4b5f-a569-b508b1fd4b3a}
 * Alias: KrStageGroup
 * Caption: $CardTypes_TypesNames_KrStageGroup
 * Group: Routes
 */
class KrStageGroupTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrStageGroup": {9ce8e9f4-cbf0-4b5f-a569-b508b1fd4b3a}.
   */
  readonly ID: guid = '9ce8e9f4-cbf0-4b5f-a569-b508b1fd4b3a';

  /**
   * Card type name for "KrStageGroup".
   */
  readonly Alias: string = 'KrStageGroup';

  /**
   * Card type caption for "KrStageGroup".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrStageGroup';

  /**
   * Card type group for "KrStageGroup".
   */
  readonly Group: string = 'Routes';

  //#endregion

  //#region Forms

  readonly FormKrStageGroup: string = 'KrStageGroup';
  readonly FormCompilerOutput: string = 'CompilerOutput';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';
  readonly BlockMainBlock: string = 'MainBlock';
  readonly BlockRestrictionsBlock: string = 'RestrictionsBlock';
  readonly BlockStageTemplatesBlock: string = 'StageTemplatesBlock';
  readonly BlockBlock4: string = 'Block4';

  //#endregion

  //#region Controls

  readonly CSharpSourceTableDesign: string = 'CSharpSourceTableDesign';
  readonly CSharpSourceTableRuntime: string = 'CSharpSourceTableRuntime';
  readonly CompileButton: string = 'CompileButton';
  readonly CompileAllButton: string = 'CompileAllButton';
  readonly CompilerOutputTable: string = 'CompilerOutputTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrStageTemplate

/**
 * ID: {2fa85bb3-bba4-4ab6-ba97-652106db96de}
 * Alias: KrStageTemplate
 * Caption: $CardTypes_TypesNames_KrStageTemplate
 * Group: Routes
 */
class KrStageTemplateTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrStageTemplate": {2fa85bb3-bba4-4ab6-ba97-652106db96de}.
   */
  readonly ID: guid = '2fa85bb3-bba4-4ab6-ba97-652106db96de';

  /**
   * Card type name for "KrStageTemplate".
   */
  readonly Alias: string = 'KrStageTemplate';

  /**
   * Card type caption for "KrStageTemplate".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrStageTemplate';

  /**
   * Card type group for "KrStageTemplate".
   */
  readonly Group: string = 'Routes';

  //#endregion

  //#region Forms

  readonly FormKrStageTemplate: string = 'KrStageTemplate';
  readonly FormCompilerOutput: string = 'CompilerOutput';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockRestrictionsBlock: string = 'RestrictionsBlock';
  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock4: string = 'Block4';

  //#endregion

  //#region Controls

  readonly CanMoveCheckboxAlias: string = 'CanMoveCheckboxAlias';
  readonly CSharpSourceTable: string = 'CSharpSourceTable';
  readonly CompileButton: string = 'CompileButton';
  readonly CompileAllButton: string = 'CompileAllButton';
  readonly CompilerOutputTable: string = 'CompilerOutputTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrStateExtension

/**
 * ID: {cca319ab-ed7a-4715-9b56-d0d5bd9d41ab}
 * Alias: KrStateExtension
 * Caption: KrStateExtension
 * Group: Acl
 */
class KrStateExtensionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrStateExtension": {cca319ab-ed7a-4715-9b56-d0d5bd9d41ab}.
   */
  readonly ID: guid = 'cca319ab-ed7a-4715-9b56-d0d5bd9d41ab';

  /**
   * Card type name for "KrStateExtension".
   */
  readonly Alias: string = 'KrStateExtension';

  /**
   * Card type caption for "KrStateExtension".
   */
  readonly Caption: string = 'KrStateExtension';

  /**
   * Card type group for "KrStateExtension".
   */
  readonly Group: string = 'Acl';

  //#endregion

  //#region Scheme Items

  readonly Scheme: KrStateExtensionSchemeInfoVirtual = new KrStateExtensionSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormTab: string = 'Tab';

  //#endregion

  //#region Blocks

  /**
   * Block caption "States" for "States".
   */
  readonly BlockStates: string = 'States';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrStateExtension Scheme Items

//#region SchemeInfo

class KrStateExtensionSchemeInfoVirtual {
  readonly AclExtensions_States: AclExtensions_StatesKrStateExtensionSchemeInfoVirtual = new AclExtensions_StatesKrStateExtensionSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region AclExtensions_States

/**
 * ID: {03851fcd-ec1e-4b06-9c29-50b69d027ea7}
 * Alias: AclExtensions_States
 */
// tslint:disable-next-line:class-name
class AclExtensions_StatesKrStateExtensionSchemeInfoVirtual {
  private readonly name: string = "AclExtensions_States";

  //#region Columns

  readonly StateID: string = 'StateID';
  readonly StateName: string = 'StateName';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region KrTaskKindSettings

/**
 * ID: {f1e87ca5-e1f1-4e5a-acbf-d6cfb20bcb11}
 * Alias: KrTaskKindSettings
 * Caption: KrTaskKindSettings
 * Group: KrProcess
 */
class KrTaskKindSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrTaskKindSettings": {f1e87ca5-e1f1-4e5a-acbf-d6cfb20bcb11}.
   */
  readonly ID: guid = 'f1e87ca5-e1f1-4e5a-acbf-d6cfb20bcb11';

  /**
   * Card type name for "KrTaskKindSettings".
   */
  readonly Alias: string = 'KrTaskKindSettings';

  /**
   * Card type caption for "KrTaskKindSettings".
   */
  readonly Caption: string = 'KrTaskKindSettings';

  /**
   * Card type group for "KrTaskKindSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrTaskKindSettings: string = 'KrTaskKindSettings';

  //#endregion

  //#region Blocks

  readonly BlockTaskKindBlock: string = 'TaskKindBlock';

  //#endregion

  //#region Controls

  readonly TaskKindEntryAC: string = 'TaskKindEntryAC';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrTaskRegistrationAction

/**
 * ID: {2d6cbf60-1c5a-40fd-a091-fa42bd4441bc}
 * Alias: KrTaskRegistrationAction
 * Caption: $KrActions_TaskRegistration
 * Group: WorkflowActions
 */
class KrTaskRegistrationActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrTaskRegistrationAction": {2d6cbf60-1c5a-40fd-a091-fa42bd4441bc}.
   */
  readonly ID: guid = '2d6cbf60-1c5a-40fd-a091-fa42bd4441bc';

  /**
   * Card type name for "KrTaskRegistrationAction".
   */
  readonly Alias: string = 'KrTaskRegistrationAction';

  /**
   * Card type caption for "KrTaskRegistrationAction".
   */
  readonly Caption: string = '$KrActions_TaskRegistration';

  /**
   * Card type group for "KrTaskRegistrationAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormKrTaskRegistrationAction: string = 'KrTaskRegistrationAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockDisclaimerBlock: string = 'DisclaimerBlock';
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  readonly FlagsTabs: string = 'FlagsTabs';
  readonly CompletionOptionsTable: string = 'CompletionOptionsTable';
  readonly ActionEventsTable: string = 'ActionEventsTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrTemplateCard

/**
 * ID: {d3d3d2e1-a45e-40c5-8228-cd304fdf6f4d}
 * Alias: KrTemplateCard
 * Caption: KrTemplateCard
 * Group: Routes
 */
class KrTemplateCardTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrTemplateCard": {d3d3d2e1-a45e-40c5-8228-cd304fdf6f4d}.
   */
  readonly ID: guid = 'd3d3d2e1-a45e-40c5-8228-cd304fdf6f4d';

  /**
   * Card type name for "KrTemplateCard".
   */
  readonly Alias: string = 'KrTemplateCard';

  /**
   * Card type caption for "KrTemplateCard".
   */
  readonly Caption: string = 'KrTemplateCard';

  /**
   * Card type group for "KrTemplateCard".
   */
  readonly Group: string = 'Routes';

  //#endregion

  //#region Forms

  readonly FormApprovalProcess: string = 'ApprovalProcess';

  //#endregion

  //#region Blocks

  readonly BlockStageCommonInfoBlock: string = 'StageCommonInfoBlock';

  /**
   * Block caption "KrSqlPerformersLinkBlock" for "KrSqlPerformersLinkBlock".
   */
  readonly BlockKrSqlPerformersLinkBlock: string = 'KrSqlPerformersLinkBlock';

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';
  readonly BlockSqlRoleBlock: string = 'SqlRoleBlock';
  readonly BlockSource_Block: string = 'Source_Block';
  readonly BlockApprovalStagesBlock: string = 'ApprovalStagesBlock';

  //#endregion

  //#region Controls

  readonly TimeLimitInput: string = 'TimeLimitInput';
  readonly PlannedInput: string = 'PlannedInput';
  readonly HiddenStageCheckbox: string = 'HiddenStageCheckbox';
  readonly CanBeSkippedCheckbox: string = 'CanBeSkippedCheckbox';
  readonly AddComputedRoleLink: string = 'AddComputedRoleLink';
  readonly CSharpSourceTable: string = 'CSharpSourceTable';
  readonly ApprovalStagesTable: string = 'ApprovalStagesTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrTypedTaskStageTypeSettings

/**
 * ID: {d8e1c89c-12b2-44e5-9bd0-c6a01c49b1e9}
 * Alias: KrTypedTaskStageTypeSettings
 * Caption: $KrStages_TypedTask
 * Group: KrProcess
 */
class KrTypedTaskStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrTypedTaskStageTypeSettings": {d8e1c89c-12b2-44e5-9bd0-c6a01c49b1e9}.
   */
  readonly ID: guid = 'd8e1c89c-12b2-44e5-9bd0-c6a01c49b1e9';

  /**
   * Card type name for "KrTypedTaskStageTypeSettings".
   */
  readonly Alias: string = 'KrTypedTaskStageTypeSettings';

  /**
   * Card type caption for "KrTypedTaskStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_TypedTask';

  /**
   * Card type group for "KrTypedTaskStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrTypedTaskStageTypeSettings: string = 'KrTypedTaskStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrUniversalTask

/**
 * ID: {9c6d9824-41d7-41e6-99f1-e19ea9e576c5}
 * Alias: KrUniversalTask
 * Caption: $CardTypes_TypesNames_KrUniversalTask
 * Group: KrProcess
 */
class KrUniversalTaskTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrUniversalTask": {9c6d9824-41d7-41e6-99f1-e19ea9e576c5}.
   */
  readonly ID: guid = '9c6d9824-41d7-41e6-99f1-e19ea9e576c5';

  /**
   * Card type name for "KrUniversalTask".
   */
  readonly Alias: string = 'KrUniversalTask';

  /**
   * Card type caption for "KrUniversalTask".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrUniversalTask';

  /**
   * Card type group for "KrUniversalTask".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormExtended: string = 'Extended';

  //#endregion

  //#region Blocks

  readonly BlockExtended: string = 'Extended';

  //#endregion

  //#region Controls

  /**
   * Control caption "MessageLabel" for "MessageLabel".
   */
  readonly MessageLabel: string = 'MessageLabel';

  readonly Comment: string = 'Comment';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrUniversalTaskAction

/**
 * ID: {231eea47-db41-4ad4-8846-164da4ef4048}
 * Alias: KrUniversalTaskAction
 * Caption: $KrActions_UniversalTask
 * Group: WorkflowActions
 */
class KrUniversalTaskActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrUniversalTaskAction": {231eea47-db41-4ad4-8846-164da4ef4048}.
   */
  readonly ID: guid = '231eea47-db41-4ad4-8846-164da4ef4048';

  /**
   * Card type name for "KrUniversalTaskAction".
   */
  readonly Alias: string = 'KrUniversalTaskAction';

  /**
   * Card type caption for "KrUniversalTaskAction".
   */
  readonly Caption: string = '$KrActions_UniversalTask';

  /**
   * Card type group for "KrUniversalTaskAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormKrUniversalTaskAction: string = 'KrUniversalTaskAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockDisclaimerBlock: string = 'DisclaimerBlock';
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  readonly FlagsTabs: string = 'FlagsTabs';
  readonly CompletionOptionsTable: string = 'CompletionOptionsTable';
  readonly ActionEventsTable: string = 'ActionEventsTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrUniversalTaskStageTypeSettings

/**
 * ID: {eada56ed-7d98-4e6e-9d9f-950d8aa42696}
 * Alias: KrUniversalTaskStageTypeSettings
 * Caption: $KrStages_UniversalTask
 * Group: KrProcess
 */
class KrUniversalTaskStageTypeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrUniversalTaskStageTypeSettings": {eada56ed-7d98-4e6e-9d9f-950d8aa42696}.
   */
  readonly ID: guid = 'eada56ed-7d98-4e6e-9d9f-950d8aa42696';

  /**
   * Card type name for "KrUniversalTaskStageTypeSettings".
   */
  readonly Alias: string = 'KrUniversalTaskStageTypeSettings';

  /**
   * Card type caption for "KrUniversalTaskStageTypeSettings".
   */
  readonly Caption: string = '$KrStages_UniversalTask';

  /**
   * Card type group for "KrUniversalTaskStageTypeSettings".
   */
  readonly Group: string = 'KrProcess';

  //#endregion

  //#region Forms

  readonly FormKrUniversalTaskStageTypeSettings: string = 'KrUniversalTaskStageTypeSettings';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockBlock1: string = 'Block1';
  readonly BlockDisclaimerBlock: string = 'DisclaimerBlock';

  //#endregion

  //#region Controls

  readonly EditCardFlagControl: string = 'EditCardFlagControl';
  readonly EditFilesFlagControl: string = 'EditFilesFlagControl';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrUserSettings

/**
 * ID: {793864e5-39e5-4d4f-af59-c3d7a9facca9}
 * Alias: KrUserSettings
 * Caption: $CardTypes_TypesNames_KrUserSettings
 * Group: UserSettings
 */
class KrUserSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrUserSettings": {793864e5-39e5-4d4f-af59-c3d7a9facca9}.
   */
  readonly ID: guid = '793864e5-39e5-4d4f-af59-c3d7a9facca9';

  /**
   * Card type name for "KrUserSettings".
   */
  readonly Alias: string = 'KrUserSettings';

  /**
   * Card type caption for "KrUserSettings".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrUserSettings';

  /**
   * Card type group for "KrUserSettings".
   */
  readonly Group: string = 'UserSettings';

  //#endregion

  //#region Forms

  readonly FormMySettings: string = 'MySettings';

  //#endregion

  //#region Blocks

  readonly BlockStandardSolution: string = 'StandardSolution';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region KrVirtualFile

/**
 * ID: {81250a95-5c1e-488c-a423-106e7f982c6b}
 * Alias: KrVirtualFile
 * Caption: $CardTypes_TypesNames_KrVirtualFile
 * Group: Dictionaries
 */
class KrVirtualFileTypeInfo {
  //#region Common

  /**
   * Card type identifier for "KrVirtualFile": {81250a95-5c1e-488c-a423-106e7f982c6b}.
   */
  readonly ID: guid = '81250a95-5c1e-488c-a423-106e7f982c6b';

  /**
   * Card type name for "KrVirtualFile".
   */
  readonly Alias: string = 'KrVirtualFile';

  /**
   * Card type caption for "KrVirtualFile".
   */
  readonly Caption: string = '$CardTypes_TypesNames_KrVirtualFile';

  /**
   * Card type group for "KrVirtualFile".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormKrVirtualFile: string = 'KrVirtualFile';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockVersionBlock: string = 'VersionBlock';
  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockBlock4: string = 'Block4';
  readonly BlockExamples: string = 'Examples';
  readonly BlockBlock5: string = 'Block5';

  //#endregion

  //#region Controls

  readonly CompileButton: string = 'CompileButton';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region LawCase

/**
 * ID: {e16a1f04-9ccd-4338-90b0-ff4d2f581208}
 * Alias: LawCase
 * Caption: $Cards_Case
 * Group: Law
 */
class LawCaseTypeInfo {
  //#region Common

  /**
   * Card type identifier for "LawCase": {e16a1f04-9ccd-4338-90b0-ff4d2f581208}.
   */
  readonly ID: guid = 'e16a1f04-9ccd-4338-90b0-ff4d2f581208';

  /**
   * Card type name for "LawCase".
   */
  readonly Alias: string = 'LawCase';

  /**
   * Card type caption for "LawCase".
   */
  readonly Caption: string = '$Cards_Case';

  /**
   * Card type group for "LawCase".
   */
  readonly Group: string = 'Law';

  //#endregion

  //#region Forms

  readonly FormMain: string = 'Main';
  readonly FormFiles: string = 'Files';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockFoldersBlock: string = 'FoldersBlock';
  readonly BlockFilesBlock: string = 'FilesBlock';

  //#endregion

  //#region Controls

  readonly ClassificationIndex: string = 'ClassificationIndex';
  readonly Location: string = 'Location';
  readonly AdministratorsControl: string = 'AdministratorsControl';
  readonly UsersControl: string = 'UsersControl';
  readonly ThirdPartiesControl: string = 'ThirdPartiesControl';
  readonly ThirdPartiesRepresentativeControl: string = 'ThirdPartiesRepresentativeControl';
  readonly FoldersView: string = 'FoldersView';

  /**
   * Control caption "Control1" for "AllFileList".
   */
  readonly AllFileList: string = 'AllFileList';
  readonly FileList: string = 'FileList';

  /**
   * Control caption "Control2" for "FilePreview".
   */
  readonly FilePreview: string = 'FilePreview';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region LawPartnerSelectionDialog

/**
 * ID: {03083069-cd54-4a82-a236-8c1ef9179832}
 * Alias: LawPartnerSelectionDialog
 * Caption: $Cards_DefaultCaption
 * Group: System
 */
class LawPartnerSelectionDialogTypeInfo {
  //#region Common

  /**
   * Card type identifier for "LawPartnerSelectionDialog": {03083069-cd54-4a82-a236-8c1ef9179832}.
   */
  readonly ID: guid = '03083069-cd54-4a82-a236-8c1ef9179832';

  /**
   * Card type name for "LawPartnerSelectionDialog".
   */
  readonly Alias: string = 'LawPartnerSelectionDialog';

  /**
   * Card type caption for "LawPartnerSelectionDialog".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "LawPartnerSelectionDialog".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormMain: string = 'Main';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  /**
   * Control caption "Partners" for "PartnersTable".
   */
  readonly PartnersTable: string = 'PartnersTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region LawVirtualFile

/**
 * ID: {ad5fae5a-d585-4aa2-8590-92ed11f49393}
 * Alias: LawVirtualFile
 * Caption: $Cards_DefaultCaption
 * Group: Law
 */
class LawVirtualFileTypeInfo {
  //#region Common

  /**
   * Card type identifier for "LawVirtualFile": {ad5fae5a-d585-4aa2-8590-92ed11f49393}.
   */
  readonly ID: guid = 'ad5fae5a-d585-4aa2-8590-92ed11f49393';

  /**
   * Card type name for "LawVirtualFile".
   */
  readonly Alias: string = 'LawVirtualFile';

  /**
   * Card type caption for "LawVirtualFile".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "LawVirtualFile".
   */
  readonly Group: string = 'Law';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region License

/**
 * ID: {f9c7b09c-de09-46b5-ba35-e73c83ea52a7}
 * Alias: License
 * Caption: $CardTypes_TypesNames_License
 * Group: Settings
 */
class LicenseTypeInfo {
  //#region Common

  /**
   * Card type identifier for "License": {f9c7b09c-de09-46b5-ba35-e73c83ea52a7}.
   */
  readonly ID: guid = 'f9c7b09c-de09-46b5-ba35-e73c83ea52a7';

  /**
   * Card type name for "License".
   */
  readonly Alias: string = 'License';

  /**
   * Card type caption for "License".
   */
  readonly Caption: string = '$CardTypes_TypesNames_License';

  /**
   * Card type group for "License".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormLicense: string = 'License';

  //#endregion

  //#region Blocks

  readonly BlockConcurrentLicenses: string = 'ConcurrentLicenses';
  readonly BlockPersonalLicenses: string = 'PersonalLicenses';
  readonly BlockAdvancedNotifications: string = 'AdvancedNotifications';

  //#endregion

  //#region Controls

  readonly ConcurrentText: string = 'ConcurrentText';
  readonly PersonalSelectRoles: string = 'PersonalSelectRoles';
  readonly PersonalText: string = 'PersonalText';
  readonly MobileSelectRoles: string = 'MobileSelectRoles';
  readonly MobileText: string = 'MobileText';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region LocalizationDialogs

/**
 * ID: {75c2f788-c389-46ea-8dcf-a62331752a7c}
 * Alias: LocalizationDialogs
 * Caption: $Cards_DefaultCaption
 * Group: System
 */
class LocalizationDialogsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "LocalizationDialogs": {75c2f788-c389-46ea-8dcf-a62331752a7c}.
   */
  readonly ID: guid = '75c2f788-c389-46ea-8dcf-a62331752a7c';

  /**
   * Card type name for "LocalizationDialogs".
   */
  readonly Alias: string = 'LocalizationDialogs';

  /**
   * Card type caption for "LocalizationDialogs".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "LocalizationDialogs".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Scheme Items

  readonly Scheme: LocalizationDialogsSchemeInfoVirtual = new LocalizationDialogsSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormAddString: string = 'AddString';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region LocalizationDialogs Scheme Items

//#region SchemeInfo

class LocalizationDialogsSchemeInfoVirtual {
  readonly AddStringSection: AddStringSectionLocalizationDialogsSchemeInfoVirtual = new AddStringSectionLocalizationDialogsSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region AddStringSection

/**
 * ID: {cb989448-69c4-4257-9da9-350f9fed356a}
 * Alias: AddStringSection
 */
class AddStringSectionLocalizationDialogsSchemeInfoVirtual {
  private readonly name: string = "AddStringSection";

  //#region Columns

  readonly LibraryName: string = 'LibraryName';
  readonly CultureName: string = 'CultureName';
  readonly SourceLanguageID: string = 'SourceLanguageID';
  readonly SourceLanguageCaption: string = 'SourceLanguageCaption';
  readonly SourceLanguageCode: string = 'SourceLanguageCode';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region MetadataEditor

/**
 * ID: {d4ed365b-5384-439f-ac44-9f71977e60d6}
 * Alias: MetadataEditor
 * Caption: $Cards_DefaultCaption
 * Group: System
 */
class MetadataEditorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "MetadataEditor": {d4ed365b-5384-439f-ac44-9f71977e60d6}.
   */
  readonly ID: guid = 'd4ed365b-5384-439f-ac44-9f71977e60d6';

  /**
   * Card type name for "MetadataEditor".
   */
  readonly Alias: string = 'MetadataEditor';

  /**
   * Card type caption for "MetadataEditor".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "MetadataEditor".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Scheme Items

  readonly Scheme: MetadataEditorSchemeInfoVirtual = new MetadataEditorSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormEditor: string = 'Editor';

  //#endregion

  //#region Blocks

  readonly BlockOverridesBlock: string = 'OverridesBlock';
  readonly BlockGeneralSettingsBlock: string = 'GeneralSettingsBlock';
  readonly BlockGroupingSettingsBlock: string = 'GroupingSettingsBlock';

  /**
   * Block caption "ViewsBlock" for "ViewsBlock".
   */
  readonly BlockViewsBlock: string = 'ViewsBlock';

  /**
   * Block caption "ViewsContainerBlock" for "ViewsContainerBlock".
   */
  readonly BlockViewsContainerBlock: string = 'ViewsContainerBlock';

  /**
   * Block caption "EmptyPresenterBlock" for "EmptyPresenterBlock".
   */
  readonly BlockEmptyPresenterBlock: string = 'EmptyPresenterBlock';

  /**
   * Block caption "ColumnProperties" for "ColumnProperties".
   */
  readonly BlockColumnProperties: string = 'ColumnProperties';

  /**
   * Block caption "CalendarOverdueColumnInfo" for "CalendarOverdueColumnInfo".
   */
  readonly BlockCalendarOverdueColumnInfo: string = 'CalendarOverdueColumnInfo';

  /**
   * Block caption "ParameterProperties" for "ParameterProperties".
   */
  readonly BlockParameterProperties: string = 'ParameterProperties';

  /**
   * Block caption "AutoCompleteInfo" for "AutoCompleteInfo".
   */
  readonly BlockAutoCompleteInfo: string = 'AutoCompleteInfo';

  /**
   * Block caption "DropDownInfo" for "DropDownInfo".
   */
  readonly BlockDropDownInfo: string = 'DropDownInfo';

  /**
   * Block caption "AppearanceProperties" for "AppearanceProperties".
   */
  readonly BlockAppearanceProperties: string = 'AppearanceProperties';
  readonly BlockFontProperties: string = 'FontProperties';

  /**
   * Block caption "ReferenceProperties" for "ReferenceProperties".
   */
  readonly BlockReferenceProperties: string = 'ReferenceProperties';

  /**
   * Block caption "SubsetProperties" for "SubsetProperties".
   */
  readonly BlockSubsetProperties: string = 'SubsetProperties';

  /**
   * Block caption "ExtensionProperties" for "ExtensionProperties".
   */
  readonly BlockExtensionProperties: string = 'ExtensionProperties';

  /**
   * Block caption "PropertiesContainerBlock" for "PropertiesContainerBlock".
   */
  readonly BlockPropertiesContainerBlock: string = 'PropertiesContainerBlock';
  readonly BlockOtherSettingsBlock: string = 'OtherSettingsBlock';
  readonly BlockTagsSettingsBlock: string = 'TagsSettingsBlock';

  //#endregion

  //#region Controls

  /**
   * Control caption "OverridesView" for "Overrides_OverridesView".
   */
  readonly Overrides_OverridesView: string = 'Overrides_OverridesView';

  readonly General_Condition: string = 'General_Condition';

  /**
   * Control caption "Paging" for "General_Paging".
   */
  readonly General_Paging: string = 'General_Paging';

  /**
   * Control caption "QuickSearchParam" for "General_QuickSearchParam".
   */
  readonly General_QuickSearchParam: string = 'General_QuickSearchParam';

  /**
   * Control caption "DefaultSortColumns" for "General_DefaultSortColumns".
   */
  readonly General_DefaultSortColumns: string = 'General_DefaultSortColumns';

  /**
   * Control caption "RowCountSubset" for "General_RowCountSubset".
   */
  readonly General_RowCountSubset: string = 'General_RowCountSubset';

  /**
   * Control caption "GroupingColumn" for "General_GroupingColumn".
   */
  readonly General_GroupingColumn: string = 'General_GroupingColumn';

  /**
   * Control caption "SelectionMode" for "General_SelectionMode".
   */
  readonly General_SelectionMode: string = 'General_SelectionMode';

  /**
   * Control caption "ConnectionAlias" for "General_ConnectionAlias".
   */
  readonly General_ConnectionAlias: string = 'General_ConnectionAlias';

  /**
   * Control caption "PageLimit" for "General_PageLimit".
   */
  readonly General_PageLimit: string = 'General_PageLimit';

  /**
   * Control caption "ExportDataPageLimit" for "General_ExportDataPageLimit".
   */
  readonly General_ExportDataPageLimit: string = 'General_ExportDataPageLimit';

  /**
   * Control caption "AutoWidthRowLimit" for "General_AutoWidthRowLimit".
   */
  readonly General_AutoWidthRowLimit: string = 'General_AutoWidthRowLimit';

  /**
   * Control caption "Appearance" for "General_Appearance".
   */
  readonly General_Appearance: string = 'General_Appearance';

  /**
   * Control caption "MultiSelect" for "General_MultiSelect".
   */
  readonly General_MultiSelect: string = 'General_MultiSelect';

  /**
   * Control caption "EnableAutoWidth" for "General_EnableAutoWidth".
   */
  readonly General_EnableAutoWidth: string = 'General_EnableAutoWidth';

  /**
   * Control caption "TreatAsSingleQuery" for "General_TreatAsSingleQuery".
   */
  readonly General_TreatAsSingleQuery: string = 'General_TreatAsSingleQuery';

  /**
   * Control caption "RowCounterVisible" for "General_RowCounterVisible".
   */
  readonly General_RowCounterVisible: string = 'General_RowCounterVisible';

  /**
   * Control caption "AutoSelectFirstRow" for "General_AutoSelectFirstRow".
   */
  readonly General_AutoSelectFirstRow: string = 'General_AutoSelectFirstRow';

  /**
   * Control caption "TreeGroupDisplayValue" for "Grouping_TreeGroupDisplayValue".
   */
  readonly Grouping_TreeGroupDisplayValue: string = 'Grouping_TreeGroupDisplayValue';

  /**
   * Control caption "TreeGroup" for "Grouping_TreeGroup".
   */
  readonly Grouping_TreeGroup: string = 'Grouping_TreeGroup';

  /**
   * Control caption "TreeGroupId" for "Grouping_TreeGroupId".
   */
  readonly Grouping_TreeGroupId: string = 'Grouping_TreeGroupId';

  /**
   * Control caption "TreeGroupParentId" for "Grouping_TreeGroupParentId".
   */
  readonly Grouping_TreeGroupParentId: string = 'Grouping_TreeGroupParentId';

  /**
   * Control caption "TreeId" for "Grouping_TreeId".
   */
  readonly Grouping_TreeId: string = 'Grouping_TreeId';

  /**
   * Control caption "TreeParentId" for "Grouping_TreeParentId".
   */
  readonly Grouping_TreeParentId: string = 'Grouping_TreeParentId';
  readonly ColumnsView: string = 'ColumnsView';
  readonly ParametersView: string = 'ParametersView';
  readonly ReferencesView: string = 'ReferencesView';
  readonly SubsetsView: string = 'SubsetsView';
  readonly AppearancesView: string = 'AppearancesView';
  readonly ExtensionsView: string = 'ExtensionsView';

  /**
   * Control caption "ViewsContainer" for "ViewsContainer".
   */
  readonly ViewsContainer: string = 'ViewsContainer';

  /**
   * Control caption "EmptyPresenter_Text" for "EmptyPresenter_Text".
   */
  readonly EmptyPresenter_Text: string = 'EmptyPresenter_Text';
  readonly Column_Description: string = 'Column_Description';

  /**
   * Control caption "Alias" for "Column_Alias".
   */
  readonly Column_Alias: string = 'Column_Alias';

  /**
   * Control caption "Caption" for "Column_Caption".
   */
  readonly Column_Caption: string = 'Column_Caption';
  readonly Column_Condition: string = 'Column_Condition';

  /**
   * Control caption "Type" for "Column_Type".
   */
  readonly Column_Type: string = 'Column_Type';

  /**
   * Control caption "Hidden" for "Column_Hidden".
   */
  readonly Column_Hidden: string = 'Column_Hidden';

  /**
   * Control caption "SortBy" for "Column_SortBy".
   */
  readonly Column_SortBy: string = 'Column_SortBy';

  /**
   * Control caption "TreatValueAsUtc" for "Column_TreatValueAsUtc".
   */
  readonly Column_TreatValueAsUtc: string = 'Column_TreatValueAsUtc';

  /**
   * Control caption "Localizable" for "Column_Localizable".
   */
  readonly Column_Localizable: string = 'Column_Localizable';

  /**
   * Control caption "DisableGrouping" for "Column_DisableGrouping".
   */
  readonly Column_DisableGrouping: string = 'Column_DisableGrouping';

  /**
   * Control caption "HasTag" for "Column_HasTag".
   */
  readonly Column_HasTag: string = 'Column_HasTag';

  /**
   * Control caption "Appearance" for "Column_Appearance".
   */
  readonly Column_Appearance: string = 'Column_Appearance';

  /**
   * Control caption "MaxLength" for "Column_MaxLength".
   */
  readonly Column_MaxLength: string = 'Column_MaxLength';

  /**
   * Control caption "CalendarQuantsColumn" for "Column_CalendarQuantsColumn".
   */
  readonly Column_CalendarQuantsColumn: string = 'Column_CalendarQuantsColumn';

  /**
   * Control caption "PlannedColumn" for "Column_PlannedColumn".
   */
  readonly Column_PlannedColumn: string = 'Column_PlannedColumn';

  /**
   * Control caption "CalendarIDColumn" for "Column_CalendarIDColumn".
   */
  readonly Column_CalendarIDColumn: string = 'Column_CalendarIDColumn';

  /**
   * Control caption "CalendarOverdueFormat" for "Column_CalendarOverdueFormat".
   */
  readonly Column_CalendarOverdueFormat: string = 'Column_CalendarOverdueFormat';
  readonly Parameter_Description: string = 'Parameter_Description';

  /**
   * Control caption "Alias" for "Parameter_Alias".
   */
  readonly Parameter_Alias: string = 'Parameter_Alias';

  /**
   * Control caption "Caption" for "Parameter_Caption".
   */
  readonly Parameter_Caption: string = 'Parameter_Caption';
  readonly Parameter_Condition: string = 'Parameter_Condition';

  /**
   * Control caption "Type" for "Parameter_Type".
   */
  readonly Parameter_Type: string = 'Parameter_Type';

  /**
   * Control caption "DateTimeType" for "Parameter_DateTimeType".
   */
  readonly Parameter_DateTimeType: string = 'Parameter_DateTimeType';

  /**
   * Control caption "RefSection" for "Parameter_RefSection".
   */
  readonly Parameter_RefSection: string = 'Parameter_RefSection';

  /**
   * Control caption "Hidden" for "Parameter_Hidden".
   */
  readonly Parameter_Hidden: string = 'Parameter_Hidden';

  /**
   * Control caption "TreatValueAsUtc" for "Parameter_TreatValueAsUtc".
   */
  readonly Parameter_TreatValueAsUtc: string = 'Parameter_TreatValueAsUtc';

  /**
   * Control caption "Multiple" for "Parameter_Multiple".
   */
  readonly Parameter_Multiple: string = 'Parameter_Multiple';

  /**
   * Control caption "HideAutoCompleteButton" for "Parameter_HideAutoCompleteButton".
   */
  readonly Parameter_HideAutoCompleteButton: string = 'Parameter_HideAutoCompleteButton';

  /**
   * Control caption "IgnoreCase" for "Parameter_IgnoreCase".
   */
  readonly Parameter_IgnoreCase: string = 'Parameter_IgnoreCase';

  /**
   * Control caption "AllowedOperands" for "Parameter_AllowedOperands".
   */
  readonly Parameter_AllowedOperands: string = 'Parameter_AllowedOperands';

  /**
   * Control caption "DisallowedOperands" for "Parameter_DisallowedOperands".
   */
  readonly Parameter_DisallowedOperands: string = 'Parameter_DisallowedOperands';

  /**
   * Control caption "Param" for "AutoCompleteInfo_Param".
   */
  readonly AutoCompleteInfo_Param: string = 'AutoCompleteInfo_Param';

  /**
   * Control caption "PopupColumns" for "AutoCompleteInfo_PopupColumns".
   */
  readonly AutoCompleteInfo_PopupColumns: string = 'AutoCompleteInfo_PopupColumns';

  /**
   * Control caption "RefPrefix" for "AutoCompleteInfo_RefPrefix".
   */
  readonly AutoCompleteInfo_RefPrefix: string = 'AutoCompleteInfo_RefPrefix';

  /**
   * Control caption "View" for "AutoCompleteInfo_View".
   */
  readonly AutoCompleteInfo_View: string = 'AutoCompleteInfo_View';

  /**
   * Control caption "PopupColumns" for "DropDownInfo_PopupColumns".
   */
  readonly DropDownInfo_PopupColumns: string = 'DropDownInfo_PopupColumns';

  /**
   * Control caption "View" for "DropDownInfo_View".
   */
  readonly DropDownInfo_View: string = 'DropDownInfo_View';

  /**
   * Control caption "RefPrefix" for "DropDownInfo_RefPrefix".
   */
  readonly DropDownInfo_RefPrefix: string = 'DropDownInfo_RefPrefix';
  readonly Appearance_Description: string = 'Appearance_Description';

  /**
   * Control caption "Alias" for "Appearance_Alias".
   */
  readonly Appearance_Alias: string = 'Appearance_Alias';
  readonly Appearance_Condition: string = 'Appearance_Condition';

  /**
   * Control caption "Background" for "Appearance_Background".
   */
  readonly Appearance_Background: string = 'Appearance_Background';

  /**
   * Control caption "Foreground" for "Appearance_Foreground".
   */
  readonly Appearance_Foreground: string = 'Appearance_Foreground';

  /**
   * Control caption "ToolTip" for "Appearance_ToolTip".
   */
  readonly Appearance_ToolTip: string = 'Appearance_ToolTip';

  /**
   * Control caption "HorizontalAlignment" for "Appearance_HorizontalAlignment".
   */
  readonly Appearance_HorizontalAlignment: string = 'Appearance_HorizontalAlignment';

  /**
   * Control caption "VerticalAlignment" for "Appearance_VerticalAlignment".
   */
  readonly Appearance_VerticalAlignment: string = 'Appearance_VerticalAlignment';

  /**
   * Control caption "TextAlignment" for "Appearance_TextAlignment".
   */
  readonly Appearance_TextAlignment: string = 'Appearance_TextAlignment';

  /**
   * Control caption "FontFamily" for "Font_FontFamily".
   */
  readonly Font_FontFamily: string = 'Font_FontFamily';

  /**
   * Control caption "FontFamilyUri" for "Font_FontFamilyUri".
   */
  readonly Font_FontFamilyUri: string = 'Font_FontFamilyUri';

  /**
   * Control caption "FontSize" for "Font_FontSize".
   */
  readonly Font_FontSize: string = 'Font_FontSize';

  /**
   * Control caption "FontStretch" for "Font_FontStretch".
   */
  readonly Font_FontStretch: string = 'Font_FontStretch';

  /**
   * Control caption "FontStyle" for "Font_FontStyle".
   */
  readonly Font_FontStyle: string = 'Font_FontStyle';

  /**
   * Control caption "FontWeight" for "Font_FontWeight".
   */
  readonly Font_FontWeight: string = 'Font_FontWeight';
  readonly Reference_Description: string = 'Reference_Description';

  /**
   * Control caption "ColPrefix" for "Reference_ColPrefix".
   */
  readonly Reference_ColPrefix: string = 'Reference_ColPrefix';
  readonly Reference_Condition: string = 'Reference_Condition';

  /**
   * Control caption "RefSection" for "Reference_Alias".
   */
  readonly Reference_Alias: string = 'Reference_Alias';

  /**
   * Control caption "DisplayValueColumn" for "Reference_DisplayValueColumn".
   */
  readonly Reference_DisplayValueColumn: string = 'Reference_DisplayValueColumn';
  readonly Subset_Description: string = 'Subset_Description';

  /**
   * Control caption "Alias" for "Subset_Alias".
   */
  readonly Subset_Alias: string = 'Subset_Alias';

  /**
   * Control caption "Caption" for "Subset_Caption".
   */
  readonly Subset_Caption: string = 'Subset_Caption';
  readonly Subset_Condition: string = 'Subset_Condition';

  /**
   * Control caption "CaptionColumn" for "Subset_CaptionColumn".
   */
  readonly Subset_CaptionColumn: string = 'Subset_CaptionColumn';

  /**
   * Control caption "CountColumn" for "Subset_CountColumn".
   */
  readonly Subset_CountColumn: string = 'Subset_CountColumn';

  /**
   * Control caption "HideZeroCount" for "Subset_HideZeroCount".
   */
  readonly Subset_HideZeroCount: string = 'Subset_HideZeroCount';

  /**
   * Control caption "RefColumn" for "Subset_RefColumn".
   */
  readonly Subset_RefColumn: string = 'Subset_RefColumn';

  /**
   * Control caption "RefParam" for "Subset_RefParam".
   */
  readonly Subset_RefParam: string = 'Subset_RefParam';

  /**
   * Control caption "TreeHasChildrenColumn" for "Subset_TreeHasChildrenColumn".
   */
  readonly Subset_TreeHasChildrenColumn: string = 'Subset_TreeHasChildrenColumn';

  /**
   * Control caption "TreeRefParam" for "Subset_TreeRefParam".
   */
  readonly Subset_TreeRefParam: string = 'Subset_TreeRefParam';

  /**
   * Control caption "Kind" for "Subset_Kind".
   */
  readonly Subset_Kind: string = 'Subset_Kind';
  readonly Extension_Description: string = 'Extension_Description';
  readonly Extension_Condition: string = 'Extension_Condition';
  readonly Extension_Order: string = 'Extension_Order';
  readonly Extension_TypeName: string = 'Extension_TypeName';
  readonly Extension_TypeDescription: string = 'Extension_TypeDescription';
  readonly Extension_TypeSettings: string = 'Extension_TypeSettings';
  readonly Extension_Container: string = 'Extension_Container';

  /**
   * Control caption "PropertiesContainer" for "PropertiesContainer".
   */
  readonly PropertiesContainer: string = 'PropertiesContainer';

  /**
   * Control caption "OtherSettingsContainer" for "OtherSettingsContainer".
   */
  readonly OtherSettingsContainer: string = 'OtherSettingsContainer';

  /**
   * Control caption "TagsPosition" for "Tags_TagsPosition".
   */
  readonly Tags_TagsPosition: string = 'Tags_TagsPosition';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region MetadataEditor Scheme Items

//#region SchemeInfo

class MetadataEditorSchemeInfoVirtual {
  readonly ViewColumnMetadata: ViewColumnMetadataMetadataEditorSchemeInfoVirtual = new ViewColumnMetadataMetadataEditorSchemeInfoVirtual();
  readonly GeneralProperties: GeneralPropertiesMetadataEditorSchemeInfoVirtual = new GeneralPropertiesMetadataEditorSchemeInfoVirtual();
  readonly AllowedOperands: AllowedOperandsMetadataEditorSchemeInfoVirtual = new AllowedOperandsMetadataEditorSchemeInfoVirtual();
  readonly NamedValue: NamedValueMetadataEditorSchemeInfoVirtual = new NamedValueMetadataEditorSchemeInfoVirtual();
  readonly SotingColumns: SotingColumnsMetadataEditorSchemeInfoVirtual = new SotingColumnsMetadataEditorSchemeInfoVirtual();
  readonly ViewParameterMetadata: ViewParameterMetadataMetadataEditorSchemeInfoVirtual = new ViewParameterMetadataMetadataEditorSchemeInfoVirtual();
  readonly ViewAppearanceMetadata: ViewAppearanceMetadataMetadataEditorSchemeInfoVirtual = new ViewAppearanceMetadataMetadataEditorSchemeInfoVirtual();
  readonly DisallowedOperands: DisallowedOperandsMetadataEditorSchemeInfoVirtual = new DisallowedOperandsMetadataEditorSchemeInfoVirtual();
  readonly ViewExtensionMetadata: ViewExtensionMetadataMetadataEditorSchemeInfoVirtual = new ViewExtensionMetadataMetadataEditorSchemeInfoVirtual();
  readonly ViewReferenceMetadata: ViewReferenceMetadataMetadataEditorSchemeInfoVirtual = new ViewReferenceMetadataMetadataEditorSchemeInfoVirtual();
  readonly ViewSubsetMetadata: ViewSubsetMetadataMetadataEditorSchemeInfoVirtual = new ViewSubsetMetadataMetadataEditorSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region ViewColumnMetadata

/**
 * ID: {149488ff-ecc0-485d-a6f1-f7990000900e}
 * Alias: ViewColumnMetadata
 */
class ViewColumnMetadataMetadataEditorSchemeInfoVirtual {
  private readonly name: string = "ViewColumnMetadata";

  //#region Columns

  readonly Appearance: string = 'Appearance';
  readonly TreatValueAsUtc: string = 'TreatValueAsUtc';
  readonly DisableGrouping: string = 'DisableGrouping';
  readonly HasTag: string = 'HasTag';
  readonly Hidden: string = 'Hidden';
  readonly Localizable: string = 'Localizable';
  readonly MaxLength: string = 'MaxLength';
  readonly SortBy: string = 'SortBy';
  readonly Condition: string = 'Condition';
  readonly Alias: string = 'Alias';
  readonly Caption: string = 'Caption';
  readonly TypeID: string = 'TypeID';
  readonly TypeName: string = 'TypeName';
  readonly CalendarQuantsColumn: string = 'CalendarQuantsColumn';
  readonly CalendarIDColumn: string = 'CalendarIDColumn';
  readonly CalendarOverdueFormat: string = 'CalendarOverdueFormat';
  readonly PlannedColumn: string = 'PlannedColumn';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region GeneralProperties

/**
 * ID: {1c90ec5a-27ad-4231-9f52-a2a298802c46}
 * Alias: GeneralProperties
 */
class GeneralPropertiesMetadataEditorSchemeInfoVirtual {
  private readonly name: string = "GeneralProperties";

  //#region Columns

  readonly Appearance: string = 'Appearance';
  readonly AutoWidthRowLimit: string = 'AutoWidthRowLimit';
  readonly EnableAutoWidth: string = 'EnableAutoWidth';
  readonly ExportDataPageLimit: string = 'ExportDataPageLimit';
  readonly MultiSelect: string = 'MultiSelect';
  readonly PageLimit: string = 'PageLimit';
  readonly PagingID: string = 'PagingID';
  readonly PagingName: string = 'PagingName';
  readonly RowCounterVisible: string = 'RowCounterVisible';
  readonly SelectionModeID: string = 'SelectionModeID';
  readonly SelectionModeName: string = 'SelectionModeName';
  readonly TreatAsSingleQuery: string = 'TreatAsSingleQuery';
  readonly Condition: string = 'Condition';
  readonly GroupingColumnID: string = 'GroupingColumnID';
  readonly GroupingColumnName: string = 'GroupingColumnName';
  readonly RowCountSubsetID: string = 'RowCountSubsetID';
  readonly RowCountSubsetName: string = 'RowCountSubsetName';
  readonly QuickSearchParamID: string = 'QuickSearchParamID';
  readonly QuickSearchParamName: string = 'QuickSearchParamName';
  readonly TreeGroupDisplayValueID: string = 'TreeGroupDisplayValueID';
  readonly TreeGroupDisplayValueName: string = 'TreeGroupDisplayValueName';
  readonly TreeGroupID: string = 'TreeGroupID';
  readonly TreeGroupName: string = 'TreeGroupName';
  readonly TreeGroupIdComplexColumnID: string = 'TreeGroupIdComplexColumnID';
  readonly TreeGroupIdComplexColumnName: string = 'TreeGroupIdComplexColumnName';
  readonly TreeGroupParentIdID: string = 'TreeGroupParentIdID';
  readonly TreeGroupParentIdName: string = 'TreeGroupParentIdName';
  readonly TreeIdID: string = 'TreeIdID';
  readonly TreeIdName: string = 'TreeIdName';
  readonly TreeParentIdID: string = 'TreeParentIdID';
  readonly TreeParentIdName: string = 'TreeParentIdName';
  readonly ConnectionAlias: string = 'ConnectionAlias';
  readonly TagsPositionID: string = 'TagsPositionID';
  readonly TagsPositionName: string = 'TagsPositionName';
  readonly AutoSelectFirstRow: string = 'AutoSelectFirstRow';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AllowedOperands

/**
 * ID: {2690d8de-8d86-45e2-ac3b-c41f66216a27}
 * Alias: AllowedOperands
 */
class AllowedOperandsMetadataEditorSchemeInfoVirtual {
  private readonly name: string = "AllowedOperands";

  //#region Columns

  readonly ValueID: string = 'ValueID';
  readonly ValueName: string = 'ValueName';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region NamedValue

/**
 * ID: {28d43c98-5089-4d7b-9ac0-21128d74f222}
 * Alias: NamedValue
 */
class NamedValueMetadataEditorSchemeInfoVirtual {
  private readonly name: string = "NamedValue";

  //#region Columns

  readonly Name: string = 'Name';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region SotingColumns

/**
 * ID: {2d14ade0-4017-4484-ad63-4be77e139f6f}
 * Alias: SotingColumns
 */
class SotingColumnsMetadataEditorSchemeInfoVirtual {
  private readonly name: string = "SotingColumns";

  //#region Columns

  readonly ValueID: string = 'ValueID';
  readonly ValueName: string = 'ValueName';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ViewParameterMetadata

/**
 * ID: {4431360b-345f-416d-b1ef-28dbfad5ebd2}
 * Alias: ViewParameterMetadata
 */
class ViewParameterMetadataMetadataEditorSchemeInfoVirtual {
  private readonly name: string = "ViewParameterMetadata";

  //#region Columns

  readonly HideAutoCompleteButton: string = 'HideAutoCompleteButton';
  readonly Caption: string = 'Caption';
  readonly TreatValueAsUtc: string = 'TreatValueAsUtc';
  readonly Hidden: string = 'Hidden';
  readonly Multiple: string = 'Multiple';
  readonly RefSection: string = 'RefSection';
  readonly AutoCompleteInfo_ParamAlias: string = 'AutoCompleteInfo_ParamAlias';
  readonly AutoCompleteInfo_PopupColumns: string = 'AutoCompleteInfo_PopupColumns';
  readonly AutoCompleteInfo_RefPrefix: string = 'AutoCompleteInfo_RefPrefix';
  readonly AutoCompleteInfo_ViewAlias: string = 'AutoCompleteInfo_ViewAlias';
  readonly DropDownInfo_PopupColumns: string = 'DropDownInfo_PopupColumns';
  readonly DropDownInfo_ViewAlias: string = 'DropDownInfo_ViewAlias';
  readonly DropDownInfo_RefPrefix: string = 'DropDownInfo_RefPrefix';
  readonly Alias: string = 'Alias';
  readonly TypeID: string = 'TypeID';
  readonly TypeName: string = 'TypeName';
  readonly Condition: string = 'Condition';
  readonly DateTimeTypeID: string = 'DateTimeTypeID';
  readonly DateTimeTypeName: string = 'DateTimeTypeName';
  readonly IgnoreCase: string = 'IgnoreCase';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ViewAppearanceMetadata

/**
 * ID: {59ad978c-d1f1-4bac-93e5-a17a895a9f6a}
 * Alias: ViewAppearanceMetadata
 */
class ViewAppearanceMetadataMetadataEditorSchemeInfoVirtual {
  private readonly name: string = "ViewAppearanceMetadata";

  //#region Columns

  readonly FontFamily: string = 'FontFamily';
  readonly FontFamilyUri: string = 'FontFamilyUri';
  readonly FontSize: string = 'FontSize';
  readonly FontStyleID: string = 'FontStyleID';
  readonly FontStyleName: string = 'FontStyleName';
  readonly ToolTip: string = 'ToolTip';
  readonly Alias: string = 'Alias';
  readonly Condition: string = 'Condition';
  readonly BackgroundID: string = 'BackgroundID';
  readonly BackgroundName: string = 'BackgroundName';
  readonly ForegroundID: string = 'ForegroundID';
  readonly ForegroundName: string = 'ForegroundName';
  readonly HorizontalAlignmentID: string = 'HorizontalAlignmentID';
  readonly HorizontalAlignmentName: string = 'HorizontalAlignmentName';
  readonly VerticalAlignmentID: string = 'VerticalAlignmentID';
  readonly VerticalAlignmentName: string = 'VerticalAlignmentName';
  readonly TextAlignmentID: string = 'TextAlignmentID';
  readonly TextAlignmentName: string = 'TextAlignmentName';
  readonly FontStretchID: string = 'FontStretchID';
  readonly FontStretchName: string = 'FontStretchName';
  readonly FontWeightID: string = 'FontWeightID';
  readonly FontWeightName: string = 'FontWeightName';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region DisallowedOperands

/**
 * ID: {9d108c33-59dc-4793-9e45-da238356614b}
 * Alias: DisallowedOperands
 */
class DisallowedOperandsMetadataEditorSchemeInfoVirtual {
  private readonly name: string = "DisallowedOperands";

  //#region Columns

  readonly ValueID: string = 'ValueID';
  readonly ValueName: string = 'ValueName';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ViewExtensionMetadata

/**
 * ID: {b6b33b5b-3b54-4aac-9525-45328acfd4b2}
 * Alias: ViewExtensionMetadata
 */
class ViewExtensionMetadataMetadataEditorSchemeInfoVirtual {
  private readonly name: string = "ViewExtensionMetadata";

  //#region Columns

  readonly Order: string = 'Order';
  readonly Condition: string = 'Condition';
  readonly TypeNameID: string = 'TypeNameID';
  readonly TypeNameName: string = 'TypeNameName';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ViewReferenceMetadata

/**
 * ID: {e9cab64d-d239-4bea-8d13-ddfcb6c6e486}
 * Alias: ViewReferenceMetadata
 */
class ViewReferenceMetadataMetadataEditorSchemeInfoVirtual {
  private readonly name: string = "ViewReferenceMetadata";

  //#region Columns

  readonly CardType: string = 'CardType';
  readonly CardTypeColumn: string = 'CardTypeColumn';
  readonly ColPrefix: string = 'ColPrefix';
  readonly DisplayValueColumn: string = 'DisplayValueColumn';
  readonly IsCard: string = 'IsCard';
  readonly OpenOnDoubleClick: string = 'OpenOnDoubleClick';
  readonly RefSection: string = 'RefSection';
  readonly Condition: string = 'Condition';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region ViewSubsetMetadata

/**
 * ID: {f39350a6-e9d7-4b4e-95f3-e22ae48abbc9}
 * Alias: ViewSubsetMetadata
 */
class ViewSubsetMetadataMetadataEditorSchemeInfoVirtual {
  private readonly name: string = "ViewSubsetMetadata";

  //#region Columns

  readonly Alias: string = 'Alias';
  readonly Caption: string = 'Caption';
  readonly CaptionColumn: string = 'CaptionColumn';
  readonly CountColumn: string = 'CountColumn';
  readonly HideZeroCount: string = 'HideZeroCount';
  readonly RefColumn: string = 'RefColumn';
  readonly RefParam: string = 'RefParam';
  readonly Condition: string = 'Condition';
  readonly TreeHasChildrenColumn: string = 'TreeHasChildrenColumn';
  readonly TreeRefParam: string = 'TreeRefParam';
  readonly KindID: string = 'KindID';
  readonly KindName: string = 'KindName';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region Metarole

/**
 * ID: {c6c9e585-c053-0aa0-994a-f80225f8585f}
 * Alias: Metarole
 * Caption: $CardTypes_TypesNames_Metarole
 * Group: Roles
 */
class MetaroleTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Metarole": {c6c9e585-c053-0aa0-994a-f80225f8585f}.
   */
  readonly ID: guid = 'c6c9e585-c053-0aa0-994a-f80225f8585f';

  /**
   * Card type name for "Metarole".
   */
  readonly Alias: string = 'Metarole';

  /**
   * Card type caption for "Metarole".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Metarole';

  /**
   * Card type group for "Metarole".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region Forms

  readonly FormMetarole: string = 'Metarole';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockUsers: string = 'Users';

  //#endregion

  //#region Controls

  readonly TimeZone: string = 'TimeZone';
  readonly RoleUsersLimitDisclaimer: string = 'RoleUsersLimitDisclaimer';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region NestedRole

/**
 * ID: {f33a4b0d-dbd8-4af7-a199-6802a77498bb}
 * Alias: NestedRole
 * Caption: $CardTypes_TypesNames_NestedRole
 * Group: Roles
 */
class NestedRoleTypeInfo {
  //#region Common

  /**
   * Card type identifier for "NestedRole": {f33a4b0d-dbd8-4af7-a199-6802a77498bb}.
   */
  readonly ID: guid = 'f33a4b0d-dbd8-4af7-a199-6802a77498bb';

  /**
   * Card type name for "NestedRole".
   */
  readonly Alias: string = 'NestedRole';

  /**
   * Card type caption for "NestedRole".
   */
  readonly Caption: string = '$CardTypes_TypesNames_NestedRole';

  /**
   * Card type group for "NestedRole".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region Forms

  readonly FormNestedRole: string = 'NestedRole';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockUsers: string = 'Users';

  //#endregion

  //#region Controls

  readonly TimeZone: string = 'TimeZone';
  readonly RoleUsersLimitDisclaimer: string = 'RoleUsersLimitDisclaimer';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Notification

/**
 * ID: {d3087e3c-a2da-4cc7-a92d-d5cf17e48d3f}
 * Alias: Notification
 * Caption: $CardTypes_TypesNames_Notification
 * Group: Dictionaries
 */
class NotificationTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Notification": {d3087e3c-a2da-4cc7-a92d-d5cf17e48d3f}.
   */
  readonly ID: guid = 'd3087e3c-a2da-4cc7-a92d-d5cf17e48d3f';

  /**
   * Card type name for "Notification".
   */
  readonly Alias: string = 'Notification';

  /**
   * Card type caption for "Notification".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Notification';

  /**
   * Card type group for "Notification".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormNotification: string = 'Notification';

  //#endregion

  //#region Blocks

  readonly BlockBlock: string = 'Block';
  readonly BlockAliasMetadataBlock: string = 'AliasMetadataBlock';
  readonly BlockNotificationBlock: string = 'NotificationBlock';

  //#endregion

  //#region Controls

  readonly CompileButton: string = 'CompileButton';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region NotificationSubscriptions

/**
 * ID: {4d92f912-2907-4e3c-bce5-7767a0f98bf8}
 * Alias: NotificationSubscriptions
 * Caption: $CardTypes_TypesNames_NotificationSubscriptions
 * Group: System
 */
class NotificationSubscriptionsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "NotificationSubscriptions": {4d92f912-2907-4e3c-bce5-7767a0f98bf8}.
   */
  readonly ID: guid = '4d92f912-2907-4e3c-bce5-7767a0f98bf8';

  /**
   * Card type name for "NotificationSubscriptions".
   */
  readonly Alias: string = 'NotificationSubscriptions';

  /**
   * Card type caption for "NotificationSubscriptions".
   */
  readonly Caption: string = '$CardTypes_TypesNames_NotificationSubscriptions';

  /**
   * Card type group for "NotificationSubscriptions".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormNotificationSubscriptionsFull: string = 'NotificationSubscriptionsFull';
  readonly FormNotificationSubscriptionsShort: string = 'NotificationSubscriptionsShort';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region NotificationType

/**
 * ID: {813234a7-3b29-4845-be9d-b5bece5f7c1c}
 * Alias: NotificationType
 * Caption: $CardTypes_TypesNames_NotificationType
 * Group: Dictionaries
 */
class NotificationTypeTypeInfo {
  //#region Common

  /**
   * Card type identifier for "NotificationType": {813234a7-3b29-4845-be9d-b5bece5f7c1c}.
   */
  readonly ID: guid = '813234a7-3b29-4845-be9d-b5bece5f7c1c';

  /**
   * Card type name for "NotificationType".
   */
  readonly Alias: string = 'NotificationType';

  /**
   * Card type caption for "NotificationType".
   */
  readonly Caption: string = '$CardTypes_TypesNames_NotificationType';

  /**
   * Card type group for "NotificationType".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormNotificationType: string = 'NotificationType';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly IsGlobal: string = 'IsGlobal';
  readonly CanSubscribe: string = 'CanSubscribe';
  readonly CardTypes: string = 'CardTypes';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region NotificationUserSettings

/**
 * ID: {193c278d-7178-4ee3-a73b-438357d69d2a}
 * Alias: NotificationUserSettings
 * Caption: $Cards_DefaultCaption
 * Group: UserSettings
 */
class NotificationUserSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "NotificationUserSettings": {193c278d-7178-4ee3-a73b-438357d69d2a}.
   */
  readonly ID: guid = '193c278d-7178-4ee3-a73b-438357d69d2a';

  /**
   * Card type name for "NotificationUserSettings".
   */
  readonly Alias: string = 'NotificationUserSettings';

  /**
   * Card type caption for "NotificationUserSettings".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "NotificationUserSettings".
   */
  readonly Group: string = 'UserSettings';

  //#endregion

  //#region Forms

  readonly FormNotificationUserSettings: string = 'NotificationUserSettings';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock2: string = 'Block2';

  /**
   * Block caption "Block2" for "Rules".
   */
  readonly BlockRules: string = 'Rules';

  //#endregion

  //#region Controls

  readonly NotificationTypes: string = 'NotificationTypes';

  /**
   * Control caption "Control1" for "DescriptionLabel".
   */
  readonly DescriptionLabel: string = 'DescriptionLabel';
  readonly Rules: string = 'Rules';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region OcrOperation

/**
 * ID: {8275fa2d-91d6-462f-8a1a-189c9b57720e}
 * Alias: OcrOperation
 * Caption: $CardTypes_TypesNames_OcrOperation
 * Group: System
 */
class OcrOperationTypeInfo {
  //#region Common

  /**
   * Card type identifier for "OcrOperation": {8275fa2d-91d6-462f-8a1a-189c9b57720e}.
   */
  readonly ID: guid = '8275fa2d-91d6-462f-8a1a-189c9b57720e';

  /**
   * Card type name for "OcrOperation".
   */
  readonly Alias: string = 'OcrOperation';

  /**
   * Card type caption for "OcrOperation".
   */
  readonly Caption: string = '$CardTypes_TypesNames_OcrOperation';

  /**
   * Card type group for "OcrOperation".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormVerificationTab: string = 'VerificationTab';
  readonly FormCompareFilesTab: string = 'CompareFilesTab';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Verification" for "VerificationBlock".
   */
  readonly BlockVerificationBlock: string = 'VerificationBlock';

  /**
   * Block caption "Recognition results" for "RecognitionResultsBlock".
   */
  readonly BlockRecognitionResultsBlock: string = 'RecognitionResultsBlock';

  /**
   * Block caption "Files preview" for "FilesPreviewBlock".
   */
  readonly BlockFilesPreviewBlock: string = 'FilesPreviewBlock';

  /**
   * Block caption "First files" for "FirstFilesBlock".
   */
  readonly BlockFirstFilesBlock: string = 'FirstFilesBlock';

  /**
   * Block caption "Second files" for "SecondFilesBlock".
   */
  readonly BlockSecondFilesBlock: string = 'SecondFilesBlock';

  /**
   * Block caption "First preview" for "FirstPreviewBlock".
   */
  readonly BlockFirstPreviewBlock: string = 'FirstPreviewBlock';

  /**
   * Block caption "Second preview" for "SecondPreviewBlock".
   */
  readonly BlockSecondPreviewBlock: string = 'SecondPreviewBlock';

  //#endregion

  //#region Controls

  /**
   * Control caption "Text layer exist hint" for "TextLayerHint".
   */
  readonly TextLayerHint: string = 'TextLayerHint';

  readonly Text: string = 'Text';
  readonly FirstLanguage: string = 'FirstLanguage';
  readonly FirstConfidence: string = 'FirstConfidence';
  readonly FirstText: string = 'FirstText';
  readonly FirstResults: string = 'FirstResults';

  /**
   * Control caption "Files preview" for "Preview".
   */
  readonly Preview: string = 'Preview';
  readonly FirstFiles: string = 'FirstFiles';
  readonly SecondFiles: string = 'SecondFiles';

  /**
   * Control caption "First preview" for "FirstPreview".
   */
  readonly FirstPreview: string = 'FirstPreview';

  /**
   * Control caption "Second preview" for "SecondPreview".
   */
  readonly SecondPreview: string = 'SecondPreview';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region OcrRequestDialog

/**
 * ID: {51488d15-c19c-4855-8bec-7cc26936e9d6}
 * Alias: OcrRequestDialog
 * Caption: $CardTypes_OcrRequestDialog
 * Group: System
 */
class OcrRequestDialogTypeInfo {
  //#region Common

  /**
   * Card type identifier for "OcrRequestDialog": {51488d15-c19c-4855-8bec-7cc26936e9d6}.
   */
  readonly ID: guid = '51488d15-c19c-4855-8bec-7cc26936e9d6';

  /**
   * Card type name for "OcrRequestDialog".
   */
  readonly Alias: string = 'OcrRequestDialog';

  /**
   * Card type caption for "OcrRequestDialog".
   */
  readonly Caption: string = '$CardTypes_OcrRequestDialog';

  /**
   * Card type group for "OcrRequestDialog".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Scheme Items

  readonly Scheme: OcrRequestDialogSchemeInfoVirtual = new OcrRequestDialogSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormOcrRequestDialog: string = 'OcrRequestDialog';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Recognition request" for "RecognitionRequestBlock".
   */
  readonly BlockRecognitionRequestBlock: string = 'RecognitionRequestBlock';

  //#endregion

  //#region Controls

  readonly SegmentationMode: string = 'SegmentationMode';
  readonly Confidence: string = 'Confidence';
  readonly Languages: string = 'Languages';

  /**
   * Control caption "Text layer possibility exist hint" for "TextLayerHint".
   */
  readonly TextLayerHint: string = 'TextLayerHint';
  readonly Overwrite: string = 'Overwrite';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region OcrRequestDialog Scheme Items

//#region SchemeInfo

class OcrRequestDialogSchemeInfoVirtual {
  readonly OcrRequest: OcrRequestOcrRequestDialogSchemeInfoVirtual = new OcrRequestOcrRequestDialogSchemeInfoVirtual();
  readonly OcrRequestLanguages: OcrRequestLanguagesOcrRequestDialogSchemeInfoVirtual = new OcrRequestLanguagesOcrRequestDialogSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region OcrRequest

/**
 * ID: {10d7cb53-c7de-4180-ba88-e6d5c4dba2fb}
 * Alias: OcrRequest
 * Description: Информация по запросу на распознавание текста
 */
class OcrRequestOcrRequestDialogSchemeInfoVirtual {
  private readonly name: string = "OcrRequest";

  //#region Columns

  readonly Confidence: string = 'Confidence';
  readonly Preprocess: string = 'Preprocess';
  readonly SegmentationModeID: string = 'SegmentationModeID';
  readonly SegmentationModeName: string = 'SegmentationModeName';
  readonly DetectLanguages: string = 'DetectLanguages';
  readonly Overwrite: string = 'Overwrite';
  readonly DetectRotation: string = 'DetectRotation';
  readonly DetectTables: string = 'DetectTables';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region OcrRequestLanguages

/**
 * ID: {9050da4d-e06a-4160-a876-5ef5ad502794}
 * Alias: OcrRequestLanguages
 * Description: Информация по языкам, используемым в запросах на распознавание текста
 */
class OcrRequestLanguagesOcrRequestDialogSchemeInfoVirtual {
  private readonly name: string = "OcrRequestLanguages";

  //#region Columns

  readonly LanguageID: string = 'LanguageID';
  readonly LanguageISO: string = 'LanguageISO';
  readonly LanguageCaption: string = 'LanguageCaption';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region OcrSettings

/**
 * ID: {b3c9c077-8f51-47fc-ba36-0247f71d6b0f}
 * Alias: OcrSettings
 * Caption: $CardTypes_TypesNames_OcrSettings
 * Group: Settings
 */
class OcrSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "OcrSettings": {b3c9c077-8f51-47fc-ba36-0247f71d6b0f}.
   */
  readonly ID: guid = 'b3c9c077-8f51-47fc-ba36-0247f71d6b0f';

  /**
   * Card type name for "OcrSettings".
   */
  readonly Alias: string = 'OcrSettings';

  /**
   * Card type caption for "OcrSettings".
   */
  readonly Caption: string = '$CardTypes_TypesNames_OcrSettings';

  /**
   * Card type group for "OcrSettings".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormRecognitionTab: string = 'RecognitionTab';
  readonly FormMappingTab: string = 'MappingTab';
  readonly FormPatternsTab: string = 'PatternsTab';

  //#endregion

  //#region Blocks

  /**
   * Block caption "RecognitionSettingsBlock" for "Recognition settings".
   */
  readonly BlockRecognitionSettings: string = 'Recognition settings';

  /**
   * Block caption "Mapping settings" for "MappingSettingsBlock".
   */
  readonly BlockMappingSettingsBlock: string = 'MappingSettingsBlock';
  readonly BlockMainInfoBlock: string = 'MainInfoBlock';

  /**
   * Block caption "Patterns settings" for "PatternsSettingsBlock".
   */
  readonly BlockPatternsSettingsBlock: string = 'PatternsSettingsBlock';
  readonly BlockFieldsMappingTypeSettingBlock: string = 'FieldsMappingTypeSettingBlock';
  readonly BlockFieldsMappingSectionSettingBlock: string = 'FieldsMappingSectionSettingBlock';
  readonly BlockFieldMappingSettingBlock: string = 'FieldMappingSettingBlock';
  readonly BlockFieldMappingSettingBlock1: string = 'FieldMappingSettingBlock1';

  //#endregion

  //#region Controls

  readonly FieldsMappingTypesSettings: string = 'FieldsMappingTypesSettings';
  readonly FieldsMappingSectionsSettings: string = 'FieldsMappingSectionsSettings';
  readonly FieldsMappingSettings: string = 'FieldsMappingSettings';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region OnlyOfficeSettings

/**
 * ID: {4d17a80b-c711-4dce-8552-c9ddb30dd7ce}
 * Alias: OnlyOfficeSettings
 * Caption: $CardTypes_TypesNames_OnlyOffice
 * Group: Settings
 */
class OnlyOfficeSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "OnlyOfficeSettings": {4d17a80b-c711-4dce-8552-c9ddb30dd7ce}.
   */
  readonly ID: guid = '4d17a80b-c711-4dce-8552-c9ddb30dd7ce';

  /**
   * Card type name for "OnlyOfficeSettings".
   */
  readonly Alias: string = 'OnlyOfficeSettings';

  /**
   * Card type caption for "OnlyOfficeSettings".
   */
  readonly Caption: string = '$CardTypes_TypesNames_OnlyOffice';

  /**
   * Card type group for "OnlyOfficeSettings".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormOnlyOfficeSettings: string = 'OnlyOfficeSettings';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Settings" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region OpenInModalDialogSettings

/**
 * ID: {57493ed6-316b-4fb9-b85e-2b931e022640}
 * Alias: OpenInModalDialogSettings
 * Caption: $Cards_DefaultCaption
 * Group: System
 */
class OpenInModalDialogSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "OpenInModalDialogSettings": {57493ed6-316b-4fb9-b85e-2b931e022640}.
   */
  readonly ID: guid = '57493ed6-316b-4fb9-b85e-2b931e022640';

  /**
   * Card type name for "OpenInModalDialogSettings".
   */
  readonly Alias: string = 'OpenInModalDialogSettings';

  /**
   * Card type caption for "OpenInModalDialogSettings".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "OpenInModalDialogSettings".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Scheme Items

  readonly Scheme: OpenInModalDialogSettingsSchemeInfoVirtual = new OpenInModalDialogSettingsSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormMainForm: string = 'MainForm';

  //#endregion

  //#region Blocks

  /**
   * Block caption "MainBlock" for "MainBlock".
   */
  readonly BlockMainBlock: string = 'MainBlock';

  //#endregion

  //#region Controls

  readonly OpenInFullscreen: string = 'OpenInFullscreen';
  readonly OpenOnlyFirstTab: string = 'OpenOnlyFirstTab';
  readonly RefreshViewOnClose: string = 'RefreshViewOnClose';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region OpenInModalDialogSettings Scheme Items

//#region SchemeInfo

class OpenInModalDialogSettingsSchemeInfoVirtual {
  readonly OpenInDialogSettings: OpenInDialogSettingsOpenInModalDialogSettingsSchemeInfoVirtual = new OpenInDialogSettingsOpenInModalDialogSettingsSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region OpenInDialogSettings

/**
 * ID: {ad8203db-107a-4680-8ebe-d0ed840e47aa}
 * Alias: OpenInDialogSettings
 */
class OpenInDialogSettingsOpenInModalDialogSettingsSchemeInfoVirtual {
  private readonly name: string = "OpenInDialogSettings";

  //#region Columns

  readonly OpenInFullscreen: string = 'OpenInFullscreen';
  readonly OpenOnlyFirstTab: string = 'OpenOnlyFirstTab';
  readonly RefreshViewOnClose: string = 'RefreshViewOnClose';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region Operation

/**
 * ID: {f2517b14-035b-4451-b4a1-605bf7a764a4}
 * Alias: Operation
 * Caption: $CardTypes_TypesNames_Operation
 * Group: System
 */
class OperationTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Operation": {f2517b14-035b-4451-b4a1-605bf7a764a4}.
   */
  readonly ID: guid = 'f2517b14-035b-4451-b4a1-605bf7a764a4';

  /**
   * Card type name for "Operation".
   */
  readonly Alias: string = 'Operation';

  /**
   * Card type caption for "Operation".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Operation';

  /**
   * Card type group for "Operation".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormOperation: string = 'Operation';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';

  /**
   * Block caption "Dates" for "Dates".
   */
  readonly BlockDates: string = 'Dates';
  readonly BlockChangesInfo: string = 'ChangesInfo';

  //#endregion

  //#region Controls

  readonly Category: string = 'Category';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Outgoing

/**
 * ID: {c59b76d9-c0db-01cd-a3fb-b339740f0620}
 * Alias: Outgoing
 * Caption: $CardTypes_TypesNames_Outgoing
 * Group: Documents
 */
class OutgoingTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Outgoing": {c59b76d9-c0db-01cd-a3fb-b339740f0620}.
   */
  readonly ID: guid = 'c59b76d9-c0db-01cd-a3fb-b339740f0620';

  /**
   * Card type name for "Outgoing".
   */
  readonly Alias: string = 'Outgoing';

  /**
   * Card type caption for "Outgoing".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Outgoing';

  /**
   * Card type group for "Outgoing".
   */
  readonly Group: string = 'Documents';

  //#endregion

  //#region Document Types

  /**
   * Document type identifier for "$KrTypes_DocTypes_Outgoing": {13597b5f-d25d-4385-9e36-43f152914719}.
   */
  readonly OutgoingID: guid = '13597b5f-d25d-4385-9e36-43f152914719';

  /**
   * Document type caption for "$KrTypes_DocTypes_Outgoing".
   */
  readonly OutgoingCaption: string = '$KrTypes_DocTypes_Outgoing';

  //#endregion

  //#region Forms

  readonly FormOutgoing: string = 'Outgoing';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockBlock4: string = 'Block4';
  readonly BlockDepartmentSignedByRecipientsPerformersBlock: string = 'DepartmentSignedByRecipientsPerformersBlock';
  readonly BlockRefsBlock: string = 'RefsBlock';
  readonly BlockBlock5: string = 'Block5';

  //#endregion

  //#region Controls

  readonly PartnerControl: string = 'PartnerControl';
  readonly IncomingRefsControl: string = 'IncomingRefsControl';
  readonly Files: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Partner

/**
 * ID: {b9a1f125-ab1d-4cff-929f-5ad8351bda4f}
 * Alias: Partner
 * Caption: $CardTypes_TypesNames_Partner
 * Group: Dictionaries
 */
class PartnerTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Partner": {b9a1f125-ab1d-4cff-929f-5ad8351bda4f}.
   */
  readonly ID: guid = 'b9a1f125-ab1d-4cff-929f-5ad8351bda4f';

  /**
   * Card type name for "Partner".
   */
  readonly Alias: string = 'Partner';

  /**
   * Card type caption for "Partner".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Partner';

  /**
   * Card type group for "Partner".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormPartner: string = 'Partner';
  readonly FormPartnerContacts: string = 'PartnerContacts';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock5: string = 'Block5';

  //#endregion

  //#region Controls

  readonly Files: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region PartnerCondition

/**
 * ID: {57c6e535-ad4b-4e03-8c06-2d3fbc59e1b8}
 * Alias: PartnerCondition
 * Caption: $CardTypes_TypesNames_PartnerCondition
 * Group: Conditions
 */
class PartnerConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "PartnerCondition": {57c6e535-ad4b-4e03-8c06-2d3fbc59e1b8}.
   */
  readonly ID: guid = '57c6e535-ad4b-4e03-8c06-2d3fbc59e1b8';

  /**
   * Card type name for "PartnerCondition".
   */
  readonly Alias: string = 'PartnerCondition';

  /**
   * Card type caption for "PartnerCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_PartnerCondition';

  /**
   * Card type group for "PartnerCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormPartnerCondition: string = 'PartnerCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region PersonalizationUserSettings

/**
 * ID: {3bd67bff-6990-4026-aa7f-b789ba8a8744}
 * Alias: PersonalizationUserSettings
 * Caption: $CardTypes_Tabs_Personalization
 * Group: UserSettings
 */
class PersonalizationUserSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "PersonalizationUserSettings": {3bd67bff-6990-4026-aa7f-b789ba8a8744}.
   */
  readonly ID: guid = '3bd67bff-6990-4026-aa7f-b789ba8a8744';

  /**
   * Card type name for "PersonalizationUserSettings".
   */
  readonly Alias: string = 'PersonalizationUserSettings';

  /**
   * Card type caption for "PersonalizationUserSettings".
   */
  readonly Caption: string = '$CardTypes_Tabs_Personalization';

  /**
   * Card type group for "PersonalizationUserSettings".
   */
  readonly Group: string = 'UserSettings';

  //#endregion

  //#region Forms

  readonly FormPersonalization: string = 'Personalization';

  //#endregion

  //#region Blocks

  readonly BlockPersonalization: string = 'Personalization';
  readonly BlockBlock1: string = 'Block1';
  readonly BlockFrequentlyUsedEmoji: string = 'FrequentlyUsedEmoji';

  //#endregion

  //#region Controls

  readonly Emojis: string = 'Emojis';
  readonly ResetEmojis: string = 'ResetEmojis';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region PersonalRole

/**
 * ID: {929ad23c-8a22-09aa-9000-398bf13979b2}
 * Alias: PersonalRole
 * Caption: $CardTypes_TypesNames_PersonalRole
 * Group: Roles
 */
class PersonalRoleTypeInfo {
  //#region Common

  /**
   * Card type identifier for "PersonalRole": {929ad23c-8a22-09aa-9000-398bf13979b2}.
   */
  readonly ID: guid = '929ad23c-8a22-09aa-9000-398bf13979b2';

  /**
   * Card type name for "PersonalRole".
   */
  readonly Alias: string = 'PersonalRole';

  /**
   * Card type caption for "PersonalRole".
   */
  readonly Caption: string = '$CardTypes_TypesNames_PersonalRole';

  /**
   * Card type group for "PersonalRole".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region Forms

  readonly FormPersonalRole: string = 'PersonalRole';

  //#endregion

  //#region Blocks

  readonly BlockNameInfo: string = 'NameInfo';
  readonly BlockRoles: string = 'Roles';
  readonly BlockLoginInfo: string = 'LoginInfo';
  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockUserAccessSecurity: string = 'UserAccessSecurity';
  readonly BlockAdInfo: string = 'AdInfo';
  readonly BlockUsers: string = 'Users';
  readonly BlockFiles: string = 'Files';

  //#endregion

  //#region Controls

  readonly Login: string = 'Login';

  /**
   * Control caption "Warning label" for "WarningLabel".
   */
  readonly WarningLabel: string = 'WarningLabel';
  readonly Password: string = 'Password';
  readonly PasswordRepeat: string = 'PasswordRepeat';
  readonly TimeZone: string = 'TimeZone';
  readonly InheritTimeZone: string = 'InheritTimeZone';
  readonly AdSyncDate: string = 'AdSyncDate';
  readonly AdSyncWhenChanged: string = 'AdSyncWhenChanged';
  readonly AdSyncDistinguishedName: string = 'AdSyncDistinguishedName';
  readonly AdSyncID: string = 'AdSyncID';
  readonly AdSyncDisableUpdate: string = 'AdSyncDisableUpdate';
  readonly AdSyncIndependent: string = 'AdSyncIndependent';
  readonly AdSyncManualSync: string = 'AdSyncManualSync';
  readonly Files: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region PersonalRoleSatellite

/**
 * ID: {f6c54fed-0bee-4d61-980a-8057179289ea}
 * Alias: PersonalRoleSatellite
 * Caption: $CardTypes_TypesNames_PersonalRoleSatellite
 * Group: Roles
 */
class PersonalRoleSatelliteTypeInfo {
  //#region Common

  /**
   * Card type identifier for "PersonalRoleSatellite": {f6c54fed-0bee-4d61-980a-8057179289ea}.
   */
  readonly ID: guid = 'f6c54fed-0bee-4d61-980a-8057179289ea';

  /**
   * Card type name for "PersonalRoleSatellite".
   */
  readonly Alias: string = 'PersonalRoleSatellite';

  /**
   * Card type caption for "PersonalRoleSatellite".
   */
  readonly Caption: string = '$CardTypes_TypesNames_PersonalRoleSatellite';

  /**
   * Card type group for "PersonalRoleSatellite".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Protocol

/**
 * ID: {4d9f9590-0131-4d32-9710-5e07c282b5d3}
 * Alias: Protocol
 * Caption: $CardTypes_TypesNames_Protocol
 * Group: Documents
 */
class ProtocolTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Protocol": {4d9f9590-0131-4d32-9710-5e07c282b5d3}.
   */
  readonly ID: guid = '4d9f9590-0131-4d32-9710-5e07c282b5d3';

  /**
   * Card type name for "Protocol".
   */
  readonly Alias: string = 'Protocol';

  /**
   * Card type caption for "Protocol".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Protocol';

  /**
   * Card type group for "Protocol".
   */
  readonly Group: string = 'Documents';

  //#endregion

  //#region Forms

  readonly FormProtocol: string = 'Protocol';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock6: string = 'Block6';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockBlock7: string = 'Block7';
  readonly BlockBlock4: string = 'Block4';
  readonly BlockDepartmentSignedByRecipientsPerformersBlock: string = 'DepartmentSignedByRecipientsPerformersBlock';
  readonly BlockRefsBlock: string = 'RefsBlock';
  readonly BlockBlock8: string = 'Block8';
  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock5: string = 'Block5';

  //#endregion

  //#region Controls

  readonly IncomingRefsControl: string = 'IncomingRefsControl';
  readonly Files: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region ReportPermissions

/**
 * ID: {65a88390-3b00-4b74-925d-b635027feff2}
 * Alias: ReportPermissions
 * Caption: $CardTypes_TypesNames_ReportPermissions
 * Group: Settings
 */
class ReportPermissionsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "ReportPermissions": {65a88390-3b00-4b74-925d-b635027feff2}.
   */
  readonly ID: guid = '65a88390-3b00-4b74-925d-b635027feff2';

  /**
   * Card type name for "ReportPermissions".
   */
  readonly Alias: string = 'ReportPermissions';

  /**
   * Card type caption for "ReportPermissions".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ReportPermissions';

  /**
   * Card type group for "ReportPermissions".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormReportPermissions: string = 'ReportPermissions';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockCurrentReportsPermissions: string = 'CurrentReportsPermissions';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region RoleDeputiesManagement

/**
 * ID: {cb931209-2ad9-4370-bb3c-3172e61937ba}
 * Alias: RoleDeputiesManagement
 * Caption: $CardTypes_TypesNames_RoleDeputiesManagement
 * Group: Roles
 */
class RoleDeputiesManagementTypeInfo {
  //#region Common

  /**
   * Card type identifier for "RoleDeputiesManagement": {cb931209-2ad9-4370-bb3c-3172e61937ba}.
   */
  readonly ID: guid = 'cb931209-2ad9-4370-bb3c-3172e61937ba';

  /**
   * Card type name for "RoleDeputiesManagement".
   */
  readonly Alias: string = 'RoleDeputiesManagement';

  /**
   * Card type caption for "RoleDeputiesManagement".
   */
  readonly Caption: string = '$CardTypes_TypesNames_RoleDeputiesManagement';

  /**
   * Card type group for "RoleDeputiesManagement".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region Forms

  readonly FormRoleDeputiesManagement: string = 'RoleDeputiesManagement';

  //#endregion

  //#region Blocks

  readonly BlockDeputies: string = 'Deputies';
  readonly BlockPeriod: string = 'Period';
  readonly BlockDeputized: string = 'Deputized';

  //#endregion

  //#region Controls

  readonly Roles: string = 'Roles';
  readonly DeputizingPermanent: string = 'DeputizingPermanent';
  readonly DeputizingStart: string = 'DeputizingStart';
  readonly DeputizingEnd: string = 'DeputizingEnd';
  readonly RoleDeputiesManagement: string = 'RoleDeputiesManagement';
  readonly RoleDeputiesNestedManagement: string = 'RoleDeputiesNestedManagement';
  readonly RoleDeputiesManagementDeputized: string = 'RoleDeputiesManagementDeputized';
  readonly RoleDeputiesManagementDeputizedView: string = 'RoleDeputiesManagementDeputizedView';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region RoleGenerator

/**
 * ID: {890379d8-651e-01d9-85c7-12644b5364b8}
 * Alias: RoleGenerator
 * Caption: $CardTypes_TypesNames_RoleGenerator
 * Group: Roles
 */
class RoleGeneratorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "RoleGenerator": {890379d8-651e-01d9-85c7-12644b5364b8}.
   */
  readonly ID: guid = '890379d8-651e-01d9-85c7-12644b5364b8';

  /**
   * Card type name for "RoleGenerator".
   */
  readonly Alias: string = 'RoleGenerator';

  /**
   * Card type caption for "RoleGenerator".
   */
  readonly Caption: string = '$CardTypes_TypesNames_RoleGenerator';

  /**
   * Card type group for "RoleGenerator".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region Forms

  readonly FormRoleGenerator: string = 'RoleGenerator';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockErrorInfo: string = 'ErrorInfo';

  //#endregion

  //#region Controls

  readonly RecalcButton: string = 'RecalcButton';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region RouteCondition

/**
 * ID: {94a948b6-9cea-4a4a-9005-b0316d89ed6e}
 * Alias: RouteCondition
 * Caption: $CardTypes_TypesNames_RouteCondition
 * Group: Conditions
 */
class RouteConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "RouteCondition": {94a948b6-9cea-4a4a-9005-b0316d89ed6e}.
   */
  readonly ID: guid = '94a948b6-9cea-4a4a-9005-b0316d89ed6e';

  /**
   * Card type name for "RouteCondition".
   */
  readonly Alias: string = 'RouteCondition';

  /**
   * Card type caption for "RouteCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_RouteCondition';

  /**
   * Card type group for "RouteCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormRouteCondition: string = 'RouteCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region RowAddedCondition

/**
 * ID: {ad464749-98a9-480f-a59b-41ef203d00ce}
 * Alias: RowAddedCondition
 * Caption: $CardTypes_TypesNames_RowAddedCondition
 * Group: Conditions
 */
class RowAddedConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "RowAddedCondition": {ad464749-98a9-480f-a59b-41ef203d00ce}.
   */
  readonly ID: guid = 'ad464749-98a9-480f-a59b-41ef203d00ce';

  /**
   * Card type name for "RowAddedCondition".
   */
  readonly Alias: string = 'RowAddedCondition';

  /**
   * Card type caption for "RowAddedCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_RowAddedCondition';

  /**
   * Card type group for "RowAddedCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormRowAddedCondition: string = 'RowAddedCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region RowDeletedCondition

/**
 * ID: {9d705cf7-e108-449d-a6f8-92512eb8d4ea}
 * Alias: RowDeletedCondition
 * Caption: $CardTypes_TypesNames_RowDeletedCondition
 * Group: Conditions
 */
class RowDeletedConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "RowDeletedCondition": {9d705cf7-e108-449d-a6f8-92512eb8d4ea}.
   */
  readonly ID: guid = '9d705cf7-e108-449d-a6f8-92512eb8d4ea';

  /**
   * Card type name for "RowDeletedCondition".
   */
  readonly Alias: string = 'RowDeletedCondition';

  /**
   * Card type caption for "RowDeletedCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_RowDeletedCondition';

  /**
   * Card type group for "RowDeletedCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormRowDeletedCondition: string = 'RowDeletedCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region SelectTag

/**
 * ID: {b9bdea71-5b6f-494b-a878-afec21add8d0}
 * Alias: SelectTag
 * Caption: $Cards_DefaultCaption
 * Group: Tags
 */
class SelectTagTypeInfo {
  //#region Common

  /**
   * Card type identifier for "SelectTag": {b9bdea71-5b6f-494b-a878-afec21add8d0}.
   */
  readonly ID: guid = 'b9bdea71-5b6f-494b-a878-afec21add8d0';

  /**
   * Card type name for "SelectTag".
   */
  readonly Alias: string = 'SelectTag';

  /**
   * Card type caption for "SelectTag".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "SelectTag".
   */
  readonly Group: string = 'Tags';

  //#endregion

  //#region Scheme Items

  readonly Scheme: SelectTagSchemeInfoVirtual = new SelectTagSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormTab: string = 'Tab';

  //#endregion

  //#region Blocks

  /**
   * Block caption "SelectTag" for "SelectTag".
   */
  readonly BlockSelectTag: string = 'SelectTag';

  //#endregion

  //#region Controls

  /**
   * Control caption "Tags" for "Tags".
   */
  readonly Tags: string = 'Tags';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region SelectTag Scheme Items

//#region SchemeInfo

class SelectTagSchemeInfoVirtual {
}

//#endregion

//#region Tables

//#endregion

//#endregion

//#region Sequence

/**
 * ID: {81e7e5a2-4745-4ccc-8178-547663d47737}
 * Alias: Sequence
 * Caption: $CardTypes_TypesNames_Sequence
 * Group: Settings
 */
class SequenceTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Sequence": {81e7e5a2-4745-4ccc-8178-547663d47737}.
   */
  readonly ID: guid = '81e7e5a2-4745-4ccc-8178-547663d47737';

  /**
   * Card type name for "Sequence".
   */
  readonly Alias: string = 'Sequence';

  /**
   * Card type caption for "Sequence".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Sequence';

  /**
   * Card type group for "Sequence".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormSequence: string = 'Sequence';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  /**
   * Block caption "Block2" for "Block2".
   */
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region ServerInstance

/**
 * ID: {7b891314-474f-4a60-8e0d-744dcb075209}
 * Alias: ServerInstance
 * Caption: $CardTypes_TypesNames_ServerInstance
 * Group: Settings
 */
class ServerInstanceTypeInfo {
  //#region Common

  /**
   * Card type identifier for "ServerInstance": {7b891314-474f-4a60-8e0d-744dcb075209}.
   */
  readonly ID: guid = '7b891314-474f-4a60-8e0d-744dcb075209';

  /**
   * Card type name for "ServerInstance".
   */
  readonly Alias: string = 'ServerInstance';

  /**
   * Card type caption for "ServerInstance".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ServerInstance';

  /**
   * Card type group for "ServerInstance".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormServerInstance: string = 'ServerInstance';
  readonly FormSecurity: string = 'Security';
  readonly FormPaletteColors: string = 'PaletteColors';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockForumInfo: string = 'ForumInfo';
  readonly BlockFileSource: string = 'FileSource';
  readonly BlockOptional: string = 'Optional';
  readonly BlockFileSources: string = 'FileSources';
  readonly BlockDatabases: string = 'Databases';

  /**
   * Block caption "Warning" for "Warning".
   */
  readonly BlockWarning: string = 'Warning';
  readonly BlockLoginSettings: string = 'LoginSettings';
  readonly BlockPasswordSettings: string = 'PasswordSettings';
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly DisableDesktopLinksInNotifications: string = 'DisableDesktopLinksInNotifications';
  readonly ForumMaxAttachedFileSizeKb: string = 'ForumMaxAttachedFileSizeKb';
  readonly ForumMaxAttachedFiles: string = 'ForumMaxAttachedFiles';
  readonly ForumMaxMessageInlines: string = 'ForumMaxMessageInlines';
  readonly ForumMaxMessageSize: string = 'ForumMaxMessageSize';
  readonly IsDatabase: string = 'IsDatabase';
  readonly FileSources: string = 'FileSources';

  /**
   * Control caption "LicenseHintForActionHistory" for "LicenseHintForActionHistory".
   */
  readonly LicenseHintForActionHistory: string = 'LicenseHintForActionHistory';
  readonly Databases: string = 'Databases';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region ShowDialog

/**
 * ID: {706e6173-f55d-4fe8-9cbe-aaeb2964ba80}
 * Alias: ShowDialog
 * Caption: $CardTypes_TypesNames_ShowDialog
 * Group: System
 */
class ShowDialogTypeInfo {
  //#region Common

  /**
   * Card type identifier for "ShowDialog": {706e6173-f55d-4fe8-9cbe-aaeb2964ba80}.
   */
  readonly ID: guid = '706e6173-f55d-4fe8-9cbe-aaeb2964ba80';

  /**
   * Card type name for "ShowDialog".
   */
  readonly Alias: string = 'ShowDialog';

  /**
   * Card type caption for "ShowDialog".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ShowDialog';

  /**
   * Card type group for "ShowDialog".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region SignatureSettings

/**
 * ID: {07257ec6-43cb-4425-9fcb-893a6c46f20d}
 * Alias: SignatureSettings
 * Caption: $CardTypes_TypesNames_SignatureSetting
 * Group: Settings
 */
class SignatureSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "SignatureSettings": {07257ec6-43cb-4425-9fcb-893a6c46f20d}.
   */
  readonly ID: guid = '07257ec6-43cb-4425-9fcb-893a6c46f20d';

  /**
   * Card type name for "SignatureSettings".
   */
  readonly Alias: string = 'SignatureSettings';

  /**
   * Card type caption for "SignatureSettings".
   */
  readonly Caption: string = '$CardTypes_TypesNames_SignatureSetting';

  /**
   * Card type group for "SignatureSettings".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormSignatureSettings: string = 'SignatureSettings';

  //#endregion

  //#region Blocks

  readonly BlockMainInformation: string = 'MainInformation';

  /**
   * Block caption "Filters and settings" for "FiltersAndSettings".
   */
  readonly BlockFiltersAndSettings: string = 'FiltersAndSettings';
  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly TSPDigestAlgorithm: string = 'TSPDigestAlgorithm';
  readonly FilterStartDate: string = 'FilterStartDate';
  readonly FilterEndDate: string = 'FilterEndDate';
  readonly FilterIsValidDate: string = 'FilterIsValidDate';
  readonly FilterCompany: string = 'FilterCompany';
  readonly FilterSubject: string = 'FilterSubject';
  readonly FilterIssuer: string = 'FilterIssuer';
  readonly CeritificateFilters: string = 'CeritificateFilters';
  readonly EncryptionAlgorithm: string = 'EncryptionAlgorithm';
  readonly DigestAlgorithm: string = 'DigestAlgorithm';
  readonly EdsManager: string = 'EdsManager';
  readonly EncryptionDigest: string = 'EncryptionDigest';
  readonly UseSystemRootCertificates: string = 'UseSystemRootCertificates';
  readonly Files: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region SmartRole

/**
 * ID: {ff6a7318-11d6-4b9d-8018-80498c50566c}
 * Alias: SmartRole
 * Caption: $CardTypes_TypesNames_SmartRole
 * Group: Roles
 */
class SmartRoleTypeInfo {
  //#region Common

  /**
   * Card type identifier for "SmartRole": {ff6a7318-11d6-4b9d-8018-80498c50566c}.
   */
  readonly ID: guid = 'ff6a7318-11d6-4b9d-8018-80498c50566c';

  /**
   * Card type name for "SmartRole".
   */
  readonly Alias: string = 'SmartRole';

  /**
   * Card type caption for "SmartRole".
   */
  readonly Caption: string = '$CardTypes_TypesNames_SmartRole';

  /**
   * Card type group for "SmartRole".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region Forms

  readonly FormSmartRole: string = 'SmartRole';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockUsers: string = 'Users';

  //#endregion

  //#region Controls

  readonly TimeZone: string = 'TimeZone';
  readonly RoleUsersLimitDisclaimer: string = 'RoleUsersLimitDisclaimer';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region SmartRoleGenerator

/**
 * ID: {c72e05fb-7eef-4256-9029-72f821f4f79e}
 * Alias: SmartRoleGenerator
 * Caption: $CardTypes_TypesNames_SmartRoleGenerator
 * Group: Roles
 */
class SmartRoleGeneratorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "SmartRoleGenerator": {c72e05fb-7eef-4256-9029-72f821f4f79e}.
   */
  readonly ID: guid = 'c72e05fb-7eef-4256-9029-72f821f4f79e';

  /**
   * Card type name for "SmartRoleGenerator".
   */
  readonly Alias: string = 'SmartRoleGenerator';

  /**
   * Card type caption for "SmartRoleGenerator".
   */
  readonly Caption: string = '$CardTypes_TypesNames_SmartRoleGenerator';

  /**
   * Card type group for "SmartRoleGenerator".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region Forms

  readonly FormSmartRoleGenerator: string = 'SmartRoleGenerator';
  readonly FormTab: string = 'Tab';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockSmartRole: string = 'SmartRole';
  readonly BlockTriggerMain: string = 'TriggerMain';
  readonly BlockTriggers: string = 'Triggers';
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  readonly OwnersSelectorSql: string = 'OwnersSelectorSql';
  readonly OwnerNameSelectorSql: string = 'OwnerNameSelectorSql';
  readonly OnlySelfUpdate: string = 'OnlySelfUpdate';
  readonly UpdateAclCardSelectorSql: string = 'UpdateAclCardSelectorSql';
  readonly Triggers: string = 'Triggers';
  readonly Validate: string = 'Validate';
  readonly ValidateAll: string = 'ValidateAll';
  readonly Errors: string = 'Errors';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region StaticRole

/**
 * ID: {825dbacc-ddec-00d1-a550-2a837792542e}
 * Alias: StaticRole
 * Caption: $CardTypes_TypesNames_StaticRole
 * Group: Roles
 */
class StaticRoleTypeInfo {
  //#region Common

  /**
   * Card type identifier for "StaticRole": {825dbacc-ddec-00d1-a550-2a837792542e}.
   */
  readonly ID: guid = '825dbacc-ddec-00d1-a550-2a837792542e';

  /**
   * Card type name for "StaticRole".
   */
  readonly Alias: string = 'StaticRole';

  /**
   * Card type caption for "StaticRole".
   */
  readonly Caption: string = '$CardTypes_TypesNames_StaticRole';

  /**
   * Card type group for "StaticRole".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region Forms

  readonly FormStaticRole: string = 'StaticRole';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockAdInfo: string = 'AdInfo';
  readonly BlockUsers: string = 'Users';

  //#endregion

  //#region Controls

  readonly TimeZone: string = 'TimeZone';
  readonly InheritTimeZone: string = 'InheritTimeZone';
  readonly AdSyncDate: string = 'AdSyncDate';
  readonly AdSyncWhenChanged: string = 'AdSyncWhenChanged';
  readonly AdSyncDistinguishedName: string = 'AdSyncDistinguishedName';
  readonly AdSyncID: string = 'AdSyncID';
  readonly AdSyncDisableUpdate: string = 'AdSyncDisableUpdate';
  readonly AdSyncIndependent: string = 'AdSyncIndependent';
  readonly AdSyncManualSync: string = 'AdSyncManualSync';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Tag

/**
 * ID: {0e459a72-4d99-43bf-8ea5-8cf7cc911382}
 * Alias: Tag
 * Caption: $CardTypes_TypesNames_Tag
 * Group: Settings
 */
class TagTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Tag": {0e459a72-4d99-43bf-8ea5-8cf7cc911382}.
   */
  readonly ID: guid = '0e459a72-4d99-43bf-8ea5-8cf7cc911382';

  /**
   * Card type name for "Tag".
   */
  readonly Alias: string = 'Tag';

  /**
   * Card type caption for "Tag".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Tag';

  /**
   * Card type group for "Tag".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormTag: string = 'Tag';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TagCondition

/**
 * ID: {237be0a6-2cf5-4ea6-9074-f31cce11eb58}
 * Alias: TagCondition
 * Caption: $CardTypes_TypesNames_TagCondition
 * Group: Conditions
 */
class TagConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TagCondition": {237be0a6-2cf5-4ea6-9074-f31cce11eb58}.
   */
  readonly ID: guid = '237be0a6-2cf5-4ea6-9074-f31cce11eb58';

  /**
   * Card type name for "TagCondition".
   */
  readonly Alias: string = 'TagCondition';

  /**
   * Card type caption for "TagCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TagCondition';

  /**
   * Card type group for "TagCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormTagCondition: string = 'TagCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TagsUserSettings

/**
 * ID: {7101497d-9057-43d9-99c6-1d425eadf3bd}
 * Alias: TagsUserSettings
 * Caption: $CardTypes_CardNames_TagsSettings
 * Group: UserSettings
 */
class TagsUserSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TagsUserSettings": {7101497d-9057-43d9-99c6-1d425eadf3bd}.
   */
  readonly ID: guid = '7101497d-9057-43d9-99c6-1d425eadf3bd';

  /**
   * Card type name for "TagsUserSettings".
   */
  readonly Alias: string = 'TagsUserSettings';

  /**
   * Card type caption for "TagsUserSettings".
   */
  readonly Caption: string = '$CardTypes_CardNames_TagsSettings';

  /**
   * Card type group for "TagsUserSettings".
   */
  readonly Group: string = 'UserSettings';

  //#endregion

  //#region Forms

  readonly FormMySettings: string = 'MySettings';

  //#endregion

  //#region Blocks

  readonly BlockTagsSettings: string = 'TagsSettings';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TaskAssignedRoleEditor

/**
 * ID: {5822a6de-95e6-4519-9045-173be057eb90}
 * Alias: TaskAssignedRoleEditor
 * Caption: $CardTypes_NewTaskAssignedRoleDialog
 * Group: System
 */
class TaskAssignedRoleEditorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TaskAssignedRoleEditor": {5822a6de-95e6-4519-9045-173be057eb90}.
   */
  readonly ID: guid = '5822a6de-95e6-4519-9045-173be057eb90';

  /**
   * Card type name for "TaskAssignedRoleEditor".
   */
  readonly Alias: string = 'TaskAssignedRoleEditor';

  /**
   * Card type caption for "TaskAssignedRoleEditor".
   */
  readonly Caption: string = '$CardTypes_NewTaskAssignedRoleDialog';

  /**
   * Card type group for "TaskAssignedRoleEditor".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Scheme Items

  readonly Scheme: TaskAssignedRoleEditorSchemeInfoVirtual = new TaskAssignedRoleEditorSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormTaskAssignedRoleEditor: string = 'TaskAssignedRoleEditor';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Новая роль связанная с заданием" for "Editor".
   */
  readonly BlockEditor: string = 'Editor';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TaskAssignedRoleEditor Scheme Items

//#region SchemeInfo

class TaskAssignedRoleEditorSchemeInfoVirtual {
  readonly TaskAssignedRolesVirtualRow: TaskAssignedRolesVirtualRowTaskAssignedRoleEditorSchemeInfoVirtual = new TaskAssignedRolesVirtualRowTaskAssignedRoleEditorSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region TaskAssignedRolesVirtualRow

/**
 * ID: {ab603ea9-a1dc-4919-a853-a30d611339c8}
 * Alias: TaskAssignedRolesVirtualRow
 */
class TaskAssignedRolesVirtualRowTaskAssignedRoleEditorSchemeInfoVirtual {
  private readonly name: string = "TaskAssignedRolesVirtualRow";

  //#region Columns

  readonly TaskRoleID: string = 'TaskRoleID';
  readonly TaskRoleCaption: string = 'TaskRoleCaption';
  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly Master: string = 'Master';
  readonly ShowInTaskDetails: string = 'ShowInTaskDetails';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region TaskAssignedRoles

/**
 * ID: {18f5d3ef-087a-4938-be67-4deb0b6e08b2}
 * Alias: TaskAssignedRoles
 * Caption: $CardTypes_TaskAssignedRolesDialog
 * Group: System
 */
class TaskAssignedRolesTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TaskAssignedRoles": {18f5d3ef-087a-4938-be67-4deb0b6e08b2}.
   */
  readonly ID: guid = '18f5d3ef-087a-4938-be67-4deb0b6e08b2';

  /**
   * Card type name for "TaskAssignedRoles".
   */
  readonly Alias: string = 'TaskAssignedRoles';

  /**
   * Card type caption for "TaskAssignedRoles".
   */
  readonly Caption: string = '$CardTypes_TaskAssignedRolesDialog';

  /**
   * Card type group for "TaskAssignedRoles".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Scheme Items

  readonly Scheme: TaskAssignedRolesSchemeInfoVirtual = new TaskAssignedRolesSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormTaskAssignedRoles: string = 'TaskAssignedRoles';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Представления" for "Views".
   */
  readonly BlockViews: string = 'Views';

  /**
   * Block caption "Block2" for "Block2".
   */
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  /**
   * Control caption "TaskAssignedRoles" for "TaskAssignedRoles".
   */
  readonly TaskAssignedRoles: string = 'TaskAssignedRoles';

  /**
   * Control caption "Users" for "Users".
   */
  readonly Users: string = 'Users';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TaskAssignedRoles Scheme Items

//#region SchemeInfo

class TaskAssignedRolesSchemeInfoVirtual {
  readonly TaskAssignedRolesVirtual: TaskAssignedRolesVirtualTaskAssignedRolesSchemeInfoVirtual = new TaskAssignedRolesVirtualTaskAssignedRolesSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region TaskAssignedRolesVirtual

/**
 * ID: {251dc5bb-cc2b-48df-af81-d295b849e4f2}
 * Alias: TaskAssignedRolesVirtual
 */
class TaskAssignedRolesVirtualTaskAssignedRolesSchemeInfoVirtual {
  private readonly name: string = "TaskAssignedRolesVirtual";

  //#region Columns

  readonly RoleID: string = 'RoleID';
  readonly RoleName: string = 'RoleName';
  readonly Position: string = 'Position';
  readonly Master: string = 'Master';
  readonly ShowInTaskDetails: string = 'ShowInTaskDetails';
  readonly RoleTypeID: string = 'RoleTypeID';
  readonly ParentRowID: string = 'ParentRowID';
  readonly TaskRoleID: string = 'TaskRoleID';
  readonly TaskRoleCaption: string = 'TaskRoleCaption';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region TaskChangedCondition

/**
 * ID: {5779afe8-1fd4-4585-8c63-bf6043996250}
 * Alias: TaskChangedCondition
 * Caption: $CardTypes_TypesNames_TaskChangedCondition
 * Group: Conditions
 */
class TaskChangedConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TaskChangedCondition": {5779afe8-1fd4-4585-8c63-bf6043996250}.
   */
  readonly ID: guid = '5779afe8-1fd4-4585-8c63-bf6043996250';

  /**
   * Card type name for "TaskChangedCondition".
   */
  readonly Alias: string = 'TaskChangedCondition';

  /**
   * Card type caption for "TaskChangedCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TaskChangedCondition';

  /**
   * Card type group for "TaskChangedCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormTaskChangedCondition: string = 'TaskChangedCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TaskHistoryGroupType

/**
 * ID: {5f25db65-1291-40c4-a1de-7673c2be448f}
 * Alias: TaskHistoryGroupType
 * Caption: $CardTypes_TypesNames_TaskHistoryGroupType
 * Group: Dictionaries
 */
class TaskHistoryGroupTypeTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TaskHistoryGroupType": {5f25db65-1291-40c4-a1de-7673c2be448f}.
   */
  readonly ID: guid = '5f25db65-1291-40c4-a1de-7673c2be448f';

  /**
   * Card type name for "TaskHistoryGroupType".
   */
  readonly Alias: string = 'TaskHistoryGroupType';

  /**
   * Card type caption for "TaskHistoryGroupType".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TaskHistoryGroupType';

  /**
   * Card type group for "TaskHistoryGroupType".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormTaskHistoryGroupType: string = 'TaskHistoryGroupType';

  //#endregion

  //#region Blocks

  readonly BlockMainInfoBlock: string = 'MainInfoBlock';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TaskKind

/**
 * ID: {2f41698a-3823-48c9-9476-f756f090eb11}
 * Alias: TaskKind
 * Caption: $CardTypes_TypesNames_TaskKind
 * Group: Dictionaries
 */
class TaskKindTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TaskKind": {2f41698a-3823-48c9-9476-f756f090eb11}.
   */
  readonly ID: guid = '2f41698a-3823-48c9-9476-f756f090eb11';

  /**
   * Card type name for "TaskKind".
   */
  readonly Alias: string = 'TaskKind';

  /**
   * Card type caption for "TaskKind".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TaskKind';

  /**
   * Card type group for "TaskKind".
   */
  readonly Group: string = 'Dictionaries';

  //#endregion

  //#region Forms

  readonly FormTaskKind: string = 'TaskKind';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TaskRole

/**
 * ID: {e97c253c-9102-0440-ac7e-4876e8f789da}
 * Alias: TaskRole
 * Caption: $CardTypes_TypesNames_TaskRole
 * Group: Roles
 */
class TaskRoleTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TaskRole": {e97c253c-9102-0440-ac7e-4876e8f789da}.
   */
  readonly ID: guid = 'e97c253c-9102-0440-ac7e-4876e8f789da';

  /**
   * Card type name for "TaskRole".
   */
  readonly Alias: string = 'TaskRole';

  /**
   * Card type caption for "TaskRole".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TaskRole';

  /**
   * Card type group for "TaskRole".
   */
  readonly Group: string = 'Roles';

  //#endregion

  //#region Forms

  readonly FormTaskRole: string = 'TaskRole';

  //#endregion

  //#region Blocks

  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockUsers: string = 'Users';

  //#endregion

  //#region Controls

  readonly TimeZone: string = 'TimeZone';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TaskTypeCondition

/**
 * ID: {c463100c-8a7b-4c31-a23f-743d8b3eb29f}
 * Alias: TaskTypeCondition
 * Caption: $CardTypes_TypesNames_TaskTypeCondition
 * Group: Conditions
 */
class TaskTypeConditionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TaskTypeCondition": {c463100c-8a7b-4c31-a23f-743d8b3eb29f}.
   */
  readonly ID: guid = 'c463100c-8a7b-4c31-a23f-743d8b3eb29f';

  /**
   * Card type name for "TaskTypeCondition".
   */
  readonly Alias: string = 'TaskTypeCondition';

  /**
   * Card type caption for "TaskTypeCondition".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TaskTypeCondition';

  /**
   * Card type group for "TaskTypeCondition".
   */
  readonly Group: string = 'Conditions';

  //#endregion

  //#region Forms

  readonly FormTaskTypeCondition: string = 'TaskTypeCondition';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Template

/**
 * ID: {7ed2fb6d-4ece-458f-9151-0c72995c2d19}
 * Alias: Template
 * Caption: $CardTypes_TypesNames_Blocks_Tabs_Template
 * Group: System
 */
class TemplateTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Template": {7ed2fb6d-4ece-458f-9151-0c72995c2d19}.
   */
  readonly ID: guid = '7ed2fb6d-4ece-458f-9151-0c72995c2d19';

  /**
   * Card type name for "Template".
   */
  readonly Alias: string = 'Template';

  /**
   * Card type caption for "Template".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Blocks_Tabs_Template';

  /**
   * Card type group for "Template".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormTemplate: string = 'Template';

  //#endregion

  //#region Blocks

  readonly BlockTemplateInfo: string = 'TemplateInfo';
  readonly BlockCardInfo: string = 'CardInfo';
  readonly BlockFiles: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TemplateFile

/**
 * ID: {a259101b-58f7-47b4-959e-dd5e7be1671c}
 * Alias: TemplateFile
 * Caption: $CardTypes_TypesNames_TemplateFile
 * Group: System
 */
class TemplateFileTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TemplateFile": {a259101b-58f7-47b4-959e-dd5e7be1671c}.
   */
  readonly ID: guid = 'a259101b-58f7-47b4-959e-dd5e7be1671c';

  /**
   * Card type name for "TemplateFile".
   */
  readonly Alias: string = 'TemplateFile';

  /**
   * Card type caption for "TemplateFile".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TemplateFile';

  /**
   * Card type group for "TemplateFile".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TestTask1

/**
 * ID: {929e345c-acdf-41ea-acb6-6bb308de73ae}
 * Alias: TestTask1
 * Caption: $CardTypes_TypesNames_TestTask1
 * Group: TestProcess
 */
class TestTask1TypeInfo {
  //#region Common

  /**
   * Card type identifier for "TestTask1": {929e345c-acdf-41ea-acb6-6bb308de73ae}.
   */
  readonly ID: guid = '929e345c-acdf-41ea-acb6-6bb308de73ae';

  /**
   * Card type name for "TestTask1".
   */
  readonly Alias: string = 'TestTask1';

  /**
   * Card type caption for "TestTask1".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TestTask1';

  /**
   * Card type group for "TestTask1".
   */
  readonly Group: string = 'TestProcess';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TestTask2

/**
 * ID: {5239e1b6-1ed6-4a3f-a11e-7e4c6e187af6}
 * Alias: TestTask2
 * Caption: $CardTypes_TypesNames_TestTask2
 * Group: TestProcess
 */
class TestTask2TypeInfo {
  //#region Common

  /**
   * Card type identifier for "TestTask2": {5239e1b6-1ed6-4a3f-a11e-7e4c6e187af6}.
   */
  readonly ID: guid = '5239e1b6-1ed6-4a3f-a11e-7e4c6e187af6';

  /**
   * Card type name for "TestTask2".
   */
  readonly Alias: string = 'TestTask2';

  /**
   * Card type caption for "TestTask2".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TestTask2';

  /**
   * Card type group for "TestTask2".
   */
  readonly Group: string = 'TestProcess';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TileUserSettings

/**
 * ID: {d3e5a259-e7e6-49f2-a7dd-1fc7b051781b}
 * Alias: TileUserSettings
 * Caption: $CardTypes_Blocks_TilePanels
 * Group: UserSettings
 */
class TileUserSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TileUserSettings": {d3e5a259-e7e6-49f2-a7dd-1fc7b051781b}.
   */
  readonly ID: guid = 'd3e5a259-e7e6-49f2-a7dd-1fc7b051781b';

  /**
   * Card type name for "TileUserSettings".
   */
  readonly Alias: string = 'TileUserSettings';

  /**
   * Card type caption for "TileUserSettings".
   */
  readonly Caption: string = '$CardTypes_Blocks_TilePanels';

  /**
   * Card type group for "TileUserSettings".
   */
  readonly Group: string = 'UserSettings';

  //#endregion

  //#region Forms

  readonly FormTilePanels: string = 'TilePanels';

  //#endregion

  //#region Blocks

  readonly BlockTilePanels: string = 'TilePanels';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TimeZones

/**
 * ID: {cc60e8c6-af29-4ad5-a55d-3e6ee985ceb2}
 * Alias: TimeZones
 * Caption: $CardTypes_TypesNames_Tabs_TimeZones
 * Group: Settings
 */
class TimeZonesTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TimeZones": {cc60e8c6-af29-4ad5-a55d-3e6ee985ceb2}.
   */
  readonly ID: guid = 'cc60e8c6-af29-4ad5-a55d-3e6ee985ceb2';

  /**
   * Card type name for "TimeZones".
   */
  readonly Alias: string = 'TimeZones';

  /**
   * Card type caption for "TimeZones".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Tabs_TimeZones';

  /**
   * Card type group for "TimeZones".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormTimeZones: string = 'TimeZones';

  //#endregion

  //#region Blocks

  readonly BlockSettings: string = 'Settings';
  readonly BlockBlock1: string = 'Block1';

  /**
   * Block caption "TimeZonesBlock" for "TimeZonesBlock".
   */
  readonly BlockTimeZonesBlock: string = 'TimeZonesBlock';

  //#endregion

  //#region Controls

  /**
   * Control caption "Warning" for "TimeZonesTooltip".
   */
  readonly TimeZonesTooltip: string = 'TimeZonesTooltip';

  readonly TimeZonesControl: string = 'TimeZonesControl';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TopicDialogs

/**
 * ID: {e8c0c3a0-80cb-4f92-9fee-d38f1dddacd2}
 * Alias: TopicDialogs
 * Caption: TopicDialogs
 * Group: Forums
 */
class TopicDialogsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TopicDialogs": {e8c0c3a0-80cb-4f92-9fee-d38f1dddacd2}.
   */
  readonly ID: guid = 'e8c0c3a0-80cb-4f92-9fee-d38f1dddacd2';

  /**
   * Card type name for "TopicDialogs".
   */
  readonly Alias: string = 'TopicDialogs';

  /**
   * Card type caption for "TopicDialogs".
   */
  readonly Caption: string = 'TopicDialogs';

  /**
   * Card type group for "TopicDialogs".
   */
  readonly Group: string = 'Forums';

  //#endregion

  //#region Scheme Items

  readonly Scheme: TopicDialogsSchemeInfoVirtual = new TopicDialogsSchemeInfoVirtual();

  //#endregion

  //#region Forms

  readonly FormAddTopicTab: string = 'AddTopicTab';
  readonly FormAddParticipantsTabName: string = 'AddParticipantsTabName';
  readonly FormAddRoleParticipantsTabName: string = 'AddRoleParticipantsTabName';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "AddTopicBlock".
   */
  readonly BlockAddTopicBlock: string = 'AddTopicBlock';

  /**
   * Block caption "Block2" for "Block2".
   */
  readonly BlockBlock2: string = 'Block2';

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly AddParticipants: string = 'AddParticipants';
  readonly IsReadOnly: string = 'IsReadOnly';
  readonly IsModerator: string = 'IsModerator';
  readonly IsSubscribed: string = 'IsSubscribed';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region TopicDialogs Scheme Items

//#region SchemeInfo

class TopicDialogsSchemeInfoVirtual {
  readonly TopicParticipantsInfo: TopicParticipantsInfoTopicDialogsSchemeInfoVirtual = new TopicParticipantsInfoTopicDialogsSchemeInfoVirtual();
  readonly TopicParticipants: TopicParticipantsTopicDialogsSchemeInfoVirtual = new TopicParticipantsTopicDialogsSchemeInfoVirtual();
  readonly TopicRoleParticipantsInfo: TopicRoleParticipantsInfoTopicDialogsSchemeInfoVirtual = new TopicRoleParticipantsInfoTopicDialogsSchemeInfoVirtual();
  readonly AddTopicInfo: AddTopicInfoTopicDialogsSchemeInfoVirtual = new AddTopicInfoTopicDialogsSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region TopicParticipantsInfo

/**
 * ID: {27b033d8-9631-4934-ad14-560c3bc31405}
 * Alias: TopicParticipantsInfo
 * Description: Settings for participants users to add or modify.
 */
class TopicParticipantsInfoTopicDialogsSchemeInfoVirtual {
  private readonly name: string = "TopicParticipantsInfo";

  //#region Columns

  readonly IsReadOnly: string = 'IsReadOnly';
  readonly IsModerator: string = 'IsModerator';
  readonly IsSubscribed: string = 'IsSubscribed';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TopicParticipants

/**
 * ID: {ba2dcbd3-8454-4bb4-9d04-5cc018d579c9}
 * Alias: TopicParticipants
 * Description: Participants list.
 */
class TopicParticipantsTopicDialogsSchemeInfoVirtual {
  private readonly name: string = "TopicParticipants";

  //#region Columns

  readonly ParticipantID: string = 'ParticipantID';
  readonly ParticipantName: string = 'ParticipantName';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region TopicRoleParticipantsInfo

/**
 * ID: {bf181a7b-8456-439e-bbe7-cf0e0a5ceda9}
 * Alias: TopicRoleParticipantsInfo
 * Description: Settings for participants roles to add or modify.
 */
class TopicRoleParticipantsInfoTopicDialogsSchemeInfoVirtual {
  private readonly name: string = "TopicRoleParticipantsInfo";

  //#region Columns

  readonly IsReadOnly: string = 'IsReadOnly';
  readonly IsSubscribed: string = 'IsSubscribed';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AddTopicInfo

/**
 * ID: {f9a0cca3-551e-41d1-bef1-3ec8036e7a48}
 * Alias: AddTopicInfo
 * Description: Title and description for the topic to add.
 */
class AddTopicInfoTopicDialogsSchemeInfoVirtual {
  private readonly name: string = "AddTopicInfo";

  //#region Columns

  readonly Title: string = 'Title';
  readonly Description: string = 'Description';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region TopicTabs

/**
 * ID: {aed972eb-883d-4b02-a75b-c96d4a5aef4c}
 * Alias: TopicTabs
 * Caption: TopicTabs
 * Group: Forums
 */
class TopicTabsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "TopicTabs": {aed972eb-883d-4b02-a75b-c96d4a5aef4c}.
   */
  readonly ID: guid = 'aed972eb-883d-4b02-a75b-c96d4a5aef4c';

  /**
   * Card type name for "TopicTabs".
   */
  readonly Alias: string = 'TopicTabs';

  /**
   * Card type caption for "TopicTabs".
   */
  readonly Caption: string = 'TopicTabs';

  /**
   * Card type group for "TopicTabs".
   */
  readonly Group: string = 'Forums';

  //#endregion

  //#region Forms

  readonly FormForum: string = 'Forum';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Топики" for "Topics".
   */
  readonly BlockTopics: string = 'Topics';

  //#endregion

  //#region Controls

  readonly Topics: string = 'Topics';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region UserSettingsSystemType

/**
 * ID: {7e6a9f59-0889-4b4d-8989-9f0c70c39f90}
 * Alias: UserSettingsSystemType
 * Caption: Used by the system. Do not modify it.
 * Group: System
 */
class UserSettingsSystemTypeTypeInfo {
  //#region Common

  /**
   * Card type identifier for "UserSettingsSystemType": {7e6a9f59-0889-4b4d-8989-9f0c70c39f90}.
   */
  readonly ID: guid = '7e6a9f59-0889-4b4d-8989-9f0c70c39f90';

  /**
   * Card type name for "UserSettingsSystemType".
   */
  readonly Alias: string = 'UserSettingsSystemType';

  /**
   * Card type caption for "UserSettingsSystemType".
   */
  readonly Caption: string = 'Used by the system. Do not modify it.';

  /**
   * Card type group for "UserSettingsSystemType".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region View

/**
 * ID: {635bbe7b-9c2e-4fde-87c2-9deefaad7981}
 * Alias: View
 * Caption: $CardTypes_TypesNames_View
 * Group: System
 */
class ViewTypeInfo {
  //#region Common

  /**
   * Card type identifier for "View": {635bbe7b-9c2e-4fde-87c2-9deefaad7981}.
   */
  readonly ID: guid = '635bbe7b-9c2e-4fde-87c2-9deefaad7981';

  /**
   * Card type name for "View".
   */
  readonly Alias: string = 'View';

  /**
   * Card type caption for "View".
   */
  readonly Caption: string = '$CardTypes_TypesNames_View';

  /**
   * Card type group for "View".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormView: string = 'View';

  //#endregion

  //#region Blocks

  readonly BlockMainInformation: string = 'MainInformation';
  readonly BlockRoles: string = 'Roles';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region ViewExtensions

/**
 * ID: {5d12d3d7-c5df-4983-9578-f065d2b74768}
 * Alias: ViewExtensions
 * Caption: $Cards_DefaultCaption
 * Group: System
 */
class ViewExtensionsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "ViewExtensions": {5d12d3d7-c5df-4983-9578-f065d2b74768}.
   */
  readonly ID: guid = '5d12d3d7-c5df-4983-9578-f065d2b74768';

  /**
   * Card type name for "ViewExtensions".
   */
  readonly Alias: string = 'ViewExtensions';

  /**
   * Card type caption for "ViewExtensions".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "ViewExtensions".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Scheme Items

  readonly Scheme: ViewExtensionsSchemeInfoVirtual = new ViewExtensionsSchemeInfoVirtual();

  //#endregion

  //#region Forms

  /**
   * Form caption "CreateCardExtension" for "CreateCardExtension".
   */
  readonly FormCreateCardExtension: string = 'CreateCardExtension';

  /**
   * Form caption "AutomaticNodeRefreshExtension" for "AutomaticNodeRefreshExtension".
   */
  readonly FormAutomaticNodeRefreshExtension: string = 'AutomaticNodeRefreshExtension';

  /**
   * Form caption "RefSectionExtension" for "RefSectionExtension".
   */
  readonly FormRefSectionExtension: string = 'RefSectionExtension';

  /**
   * Form caption "ManagerWorkplaceExtension" for "ManagerWorkplaceExtension".
   */
  readonly FormManagerWorkplaceExtension: string = 'ManagerWorkplaceExtension';

  //#endregion

  //#region Blocks

  /**
   * Block caption "MainBlock" for "MainBlock".
   */
  readonly BlockMainBlock: string = 'MainBlock';

  //#endregion

  //#region Controls

  readonly IDParam: string = 'IDParam';
  readonly CardOpeningKind: string = 'CardOpeningKind';
  readonly CreateCardKind: string = 'CreateCardKind';
  readonly TypeAlias: string = 'TypeAlias';
  readonly DocTypeIdentifier: string = 'DocTypeIdentifier';
  readonly RefreshInterval: string = 'RefreshInterval';
  readonly WithContentDataRefreshing: string = 'WithContentDataRefreshing';
  readonly Name: string = 'Name';
  readonly RefSections: string = 'RefSections';
  readonly Parameters: string = 'Parameters';
  readonly CardId: string = 'CardId';
  readonly CountColumnName: string = 'CountColumnName';
  readonly TileColumnName: string = 'TileColumnName';
  readonly ActiveImageColumnName: string = 'ActiveImageColumnName';
  readonly HoverImageColumnName: string = 'HoverImageColumnName';
  readonly InactiveImageColumnName: string = 'InactiveImageColumnName';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region ViewExtensions Scheme Items

//#region SchemeInfo

class ViewExtensionsSchemeInfoVirtual {
  readonly ManagerWorkplaceExtension: ManagerWorkplaceExtensionViewExtensionsSchemeInfoVirtual = new ManagerWorkplaceExtensionViewExtensionsSchemeInfoVirtual();
  readonly Parameters: ParametersViewExtensionsSchemeInfoVirtual = new ParametersViewExtensionsSchemeInfoVirtual();
  readonly RefSections: RefSectionsViewExtensionsSchemeInfoVirtual = new RefSectionsViewExtensionsSchemeInfoVirtual();
  readonly CreateCardExtension: CreateCardExtensionViewExtensionsSchemeInfoVirtual = new CreateCardExtensionViewExtensionsSchemeInfoVirtual();
  readonly AutomaticNodeRefreshExtension: AutomaticNodeRefreshExtensionViewExtensionsSchemeInfoVirtual = new AutomaticNodeRefreshExtensionViewExtensionsSchemeInfoVirtual();
  readonly NamedValue: NamedValueViewExtensionsSchemeInfoVirtual = new NamedValueViewExtensionsSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region ManagerWorkplaceExtension

/**
 * ID: {1095bd7f-6938-4778-91bf-84a1886675ad}
 * Alias: ManagerWorkplaceExtension
 */
class ManagerWorkplaceExtensionViewExtensionsSchemeInfoVirtual {
  private readonly name: string = "ManagerWorkplaceExtension";

  //#region Columns

  readonly CardId: string = 'CardId';
  readonly ActiveImageColumnID: string = 'ActiveImageColumnID';
  readonly ActiveImageColumnName: string = 'ActiveImageColumnName';
  readonly CountColumnID: string = 'CountColumnID';
  readonly CountColumnName: string = 'CountColumnName';
  readonly InactiveImageColumnID: string = 'InactiveImageColumnID';
  readonly InactiveImageColumnName: string = 'InactiveImageColumnName';
  readonly TileColumnID: string = 'TileColumnID';
  readonly TileColumnName: string = 'TileColumnName';
  readonly HoverImageColumnID: string = 'HoverImageColumnID';
  readonly HoverImageColumnName: string = 'HoverImageColumnName';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region Parameters

/**
 * ID: {8fbd1dea-c2ee-47ed-8972-7d9c63d7a6af}
 * Alias: Parameters
 */
class ParametersViewExtensionsSchemeInfoVirtual {
  private readonly name: string = "Parameters";

  //#region Columns

  readonly Value: string = 'Value';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region RefSections

/**
 * ID: {cb5f4897-b19d-4151-9a96-9024fab5822e}
 * Alias: RefSections
 */
class RefSectionsViewExtensionsSchemeInfoVirtual {
  private readonly name: string = "RefSections";

  //#region Columns

  readonly Value: string = 'Value';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region CreateCardExtension

/**
 * ID: {d77e4dd1-425c-423b-9331-0d2a9ef1701f}
 * Alias: CreateCardExtension
 */
class CreateCardExtensionViewExtensionsSchemeInfoVirtual {
  private readonly name: string = "CreateCardExtension";

  //#region Columns

  readonly CreateCardKindID: string = 'CreateCardKindID';
  readonly CreateCardKindName: string = 'CreateCardKindName';
  readonly CardOpeningKindID: string = 'CardOpeningKindID';
  readonly CardOpeningKindName: string = 'CardOpeningKindName';
  readonly TypeAlias: string = 'TypeAlias';
  readonly DocTypeIdentifier: string = 'DocTypeIdentifier';
  readonly IDParam: string = 'IDParam';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region AutomaticNodeRefreshExtension

/**
 * ID: {eea6d5c6-12a4-4a06-8c0d-8135850b3d45}
 * Alias: AutomaticNodeRefreshExtension
 */
class AutomaticNodeRefreshExtensionViewExtensionsSchemeInfoVirtual {
  private readonly name: string = "AutomaticNodeRefreshExtension";

  //#region Columns

  readonly RefreshInterval: string = 'RefreshInterval';
  readonly WithContentDataRefreshing: string = 'WithContentDataRefreshing';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region NamedValue

/**
 * ID: {f6590ca7-ed43-4958-8883-2276978eb272}
 * Alias: NamedValue
 */
class NamedValueViewExtensionsSchemeInfoVirtual {
  private readonly name: string = "NamedValue";

  //#region Columns

  readonly Name: string = 'Name';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region VirtualScheme

/**
 * ID: {b2b4d2c2-8f92-4262-9951-fe1a64bf9b30}
 * Alias: VirtualScheme
 * Caption: $Cards_DefaultCaption
 * Group: System
 */
class VirtualSchemeTypeInfo {
  //#region Common

  /**
   * Card type identifier for "VirtualScheme": {b2b4d2c2-8f92-4262-9951-fe1a64bf9b30}.
   */
  readonly ID: guid = 'b2b4d2c2-8f92-4262-9951-fe1a64bf9b30';

  /**
   * Card type name for "VirtualScheme".
   */
  readonly Alias: string = 'VirtualScheme';

  /**
   * Card type caption for "VirtualScheme".
   */
  readonly Caption: string = '$Cards_DefaultCaption';

  /**
   * Card type group for "VirtualScheme".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Scheme Items

  readonly Scheme: VirtualSchemeSchemeInfoVirtual = new VirtualSchemeSchemeInfoVirtual();

  //#endregion

  //#region Forms

  /**
   * Form caption "Editor" for "Editor".
   */
  readonly FormEditor: string = 'Editor';

  //#endregion

  //#region Blocks

  /**
   * Block caption "SchemeViewEditorBlock" for "SchemeViewEditorBlock".
   */
  readonly BlockSchemeViewEditorBlock: string = 'SchemeViewEditorBlock';

  /**
   * Block caption "PropertiesEditorBlock" for "PropertiesEditorBlock".
   */
  readonly BlockPropertiesEditorBlock: string = 'PropertiesEditorBlock';

  /**
   * Block caption "ReferenceColumnsEditorBlock" for "ReferenceColumnsEditorBlock".
   */
  readonly BlockReferenceColumnsEditorBlock: string = 'ReferenceColumnsEditorBlock';

  //#endregion

  //#region Controls

  readonly SchemeEditor: string = 'SchemeEditor';
  readonly NameEditor: string = 'NameEditor';
  readonly DescriptionEdtor: string = 'DescriptionEdtor';
  readonly TableContentTypeEditor: string = 'TableContentTypeEditor';
  readonly ColumnContentTypeEditor: string = 'ColumnContentTypeEditor';
  readonly ReferenceTableEditor: string = 'ReferenceTableEditor';
  readonly IsReferencedToOwnerEditor: string = 'IsReferencedToOwnerEditor';
  readonly DefaultStringValueEditor: string = 'DefaultStringValueEditor';
  readonly DefaultDateTimeValueEditor: string = 'DefaultDateTimeValueEditor';
  readonly DefaultDateValueEditor: string = 'DefaultDateValueEditor';
  readonly DefaultTimeValueEditor: string = 'DefaultTimeValueEditor';
  readonly DefaultDateTimeOffsetDateTimeValueEditor: string = 'DefaultDateTimeOffsetDateTimeValueEditor';
  readonly DefaultDateTimeOffsetOffSetValueEditor: string = 'DefaultDateTimeOffsetOffSetValueEditor';
  readonly ReferenceColumnsEditor: string = 'ReferenceColumnsEditor';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region VirtualScheme Scheme Items

//#region SchemeInfo

class VirtualSchemeSchemeInfoVirtual {
  readonly Properties: PropertiesVirtualSchemeSchemeInfoVirtual = new PropertiesVirtualSchemeSchemeInfoVirtual();
  readonly NamedValue: NamedValueVirtualSchemeSchemeInfoVirtual = new NamedValueVirtualSchemeSchemeInfoVirtual();
}

//#endregion

//#region Tables

//#region Properties

/**
 * ID: {14ca4189-29fe-4d4c-a4f0-cc864a6203b8}
 * Alias: Properties
 */
class PropertiesVirtualSchemeSchemeInfoVirtual {
  private readonly name: string = "Properties";

  //#region Columns

  readonly Name: string = 'Name';
  readonly Description: string = 'Description';
  readonly RefTableID: string = 'RefTableID';
  readonly RefTableName: string = 'RefTableName';
  readonly DefaultValueString: string = 'DefaultValueString';
  readonly DefaultValueDateTime: string = 'DefaultValueDateTime';
  readonly ColumnContentTypeID: string = 'ColumnContentTypeID';
  readonly ColumnContentTypeName: string = 'ColumnContentTypeName';
  readonly TableTypeID: string = 'TableTypeID';
  readonly TableTypeName: string = 'TableTypeName';
  readonly IsReferencedToOwner: string = 'IsReferencedToOwner';
  readonly OffSet: string = 'OffSet';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#region NamedValue

/**
 * ID: {bba81bba-6e98-4e77-9b1b-dc9ed7895a37}
 * Alias: NamedValue
 */
class NamedValueVirtualSchemeSchemeInfoVirtual {
  private readonly name: string = "NamedValue";

  //#region Columns

  readonly Name: string = 'Name';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.name;
  }

  //#endregion
}

//#endregion

//#endregion

//#endregion

//#region WebApplication

/**
 * ID: {104076c7-3cd3-4b4e-9344-e17f24c88ee8}
 * Alias: WebApplication
 * Caption: $CardTypes_TypesNames_WebApplication
 * Group: System
 */
class WebApplicationTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WebApplication": {104076c7-3cd3-4b4e-9344-e17f24c88ee8}.
   */
  readonly ID: guid = '104076c7-3cd3-4b4e-9344-e17f24c88ee8';

  /**
   * Card type name for "WebApplication".
   */
  readonly Alias: string = 'WebApplication';

  /**
   * Card type caption for "WebApplication".
   */
  readonly Caption: string = '$CardTypes_TypesNames_WebApplication';

  /**
   * Card type group for "WebApplication".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormWebApplication: string = 'WebApplication';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockFiles: string = 'Files';

  //#endregion

  //#region Controls

  readonly Files: string = 'Files';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WebClientUserSettings

/**
 * ID: {7104ac73-bf0a-4dba-be9e-21f7148c8c82}
 * Alias: WebClientUserSettings
 * Caption: $CardTypes_Blocks_WebClientUserSettings
 * Group: UserSettings
 */
class WebClientUserSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WebClientUserSettings": {7104ac73-bf0a-4dba-be9e-21f7148c8c82}.
   */
  readonly ID: guid = '7104ac73-bf0a-4dba-be9e-21f7148c8c82';

  /**
   * Card type name for "WebClientUserSettings".
   */
  readonly Alias: string = 'WebClientUserSettings';

  /**
   * Card type caption for "WebClientUserSettings".
   */
  readonly Caption: string = '$CardTypes_Blocks_WebClientUserSettings';

  /**
   * Card type group for "WebClientUserSettings".
   */
  readonly Group: string = 'UserSettings';

  //#endregion

  //#region Forms

  readonly FormWebPanels: string = 'WebPanels';

  //#endregion

  //#region Blocks

  readonly BlockTilePanels: string = 'TilePanels';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WfResolution

/**
 * ID: {928132fe-202d-4f9f-8ec5-5093ea2122d1}
 * Alias: WfResolution
 * Caption: $CardTypes_TypesNames_WfResolution
 * Group: Wf
 */
class WfResolutionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WfResolution": {928132fe-202d-4f9f-8ec5-5093ea2122d1}.
   */
  readonly ID: guid = '928132fe-202d-4f9f-8ec5-5093ea2122d1';

  /**
   * Card type name for "WfResolution".
   */
  readonly Alias: string = 'WfResolution';

  /**
   * Card type caption for "WfResolution".
   */
  readonly Caption: string = '$CardTypes_TypesNames_WfResolution';

  /**
   * Card type group for "WfResolution".
   */
  readonly Group: string = 'Wf';

  //#endregion

  //#region Forms

  readonly FormWfResolution: string = 'WfResolution';
  readonly FormComplete: string = 'Complete';
  readonly FormSendToPerformer: string = 'SendToPerformer';
  readonly FormCreateChildResolution: string = 'CreateChildResolution';
  readonly FormRevokeOrCancel: string = 'RevokeOrCancel';
  readonly FormModifyAsAuthor: string = 'ModifyAsAuthor';

  //#endregion

  //#region Blocks

  readonly BlockTaskInfo: string = 'TaskInfo';
  readonly BlockMainInfo: string = 'MainInfo';
  readonly BlockChildResolutions: string = 'ChildResolutions';
  readonly BlockPerformers: string = 'Performers';
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly TaskInfo: string = 'TaskInfo';
  readonly RevokeChildren_ChildResolutions: string = 'RevokeChildren_ChildResolutions';
  readonly MassCreation_MultiplePerformers: string = 'MassCreation_MultiplePerformers';
  readonly MajorPerformer_MassCreation: string = 'MajorPerformer_MassCreation';
  readonly WithControl: string = 'WithControl';
  readonly Kind_Additional: string = 'Kind_Additional';
  readonly From_Additional: string = 'From_Additional';
  readonly Controller_WithControl: string = 'Controller_WithControl';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WfResolutionChild

/**
 * ID: {539ecfe8-5fb6-4681-8aa8-1ee4d9ef1dda}
 * Alias: WfResolutionChild
 * Caption: $CardTypes_TypesNames_WfResolutionChild
 * Group: Wf
 */
class WfResolutionChildTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WfResolutionChild": {539ecfe8-5fb6-4681-8aa8-1ee4d9ef1dda}.
   */
  readonly ID: guid = '539ecfe8-5fb6-4681-8aa8-1ee4d9ef1dda';

  /**
   * Card type name for "WfResolutionChild".
   */
  readonly Alias: string = 'WfResolutionChild';

  /**
   * Card type caption for "WfResolutionChild".
   */
  readonly Caption: string = '$CardTypes_TypesNames_WfResolutionChild';

  /**
   * Card type group for "WfResolutionChild".
   */
  readonly Group: string = 'Wf';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WfResolutionControl

/**
 * ID: {85a5e8d7-a901-46df-9173-4d9a043ce6d3}
 * Alias: WfResolutionControl
 * Caption: $CardTypes_TypesNames_WfResolutionControl
 * Group: Wf
 */
class WfResolutionControlTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WfResolutionControl": {85a5e8d7-a901-46df-9173-4d9a043ce6d3}.
   */
  readonly ID: guid = '85a5e8d7-a901-46df-9173-4d9a043ce6d3';

  /**
   * Card type name for "WfResolutionControl".
   */
  readonly Alias: string = 'WfResolutionControl';

  /**
   * Card type caption for "WfResolutionControl".
   */
  readonly Caption: string = '$CardTypes_TypesNames_WfResolutionControl';

  /**
   * Card type group for "WfResolutionControl".
   */
  readonly Group: string = 'Wf';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WfResolutionProject

/**
 * ID: {c989d91f-7ddd-455c-ae16-3bb380132ba8}
 * Alias: WfResolutionProject
 * Caption: $CardTypes_TypesNames_WfResolutionProject
 * Group: Wf
 */
class WfResolutionProjectTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WfResolutionProject": {c989d91f-7ddd-455c-ae16-3bb380132ba8}.
   */
  readonly ID: guid = 'c989d91f-7ddd-455c-ae16-3bb380132ba8';

  /**
   * Card type name for "WfResolutionProject".
   */
  readonly Alias: string = 'WfResolutionProject';

  /**
   * Card type caption for "WfResolutionProject".
   */
  readonly Caption: string = '$CardTypes_TypesNames_WfResolutionProject';

  /**
   * Card type group for "WfResolutionProject".
   */
  readonly Group: string = 'Wf';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WfSatellite

/**
 * ID: {a382ec40-6321-42e5-a9f9-c7b103feb38d}
 * Alias: WfSatellite
 * Caption: $CardTypes_TypesNames_WfSatellite
 * Group: Wf
 */
class WfSatelliteTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WfSatellite": {a382ec40-6321-42e5-a9f9-c7b103feb38d}.
   */
  readonly ID: guid = 'a382ec40-6321-42e5-a9f9-c7b103feb38d';

  /**
   * Card type name for "WfSatellite".
   */
  readonly Alias: string = 'WfSatellite';

  /**
   * Card type caption for "WfSatellite".
   */
  readonly Caption: string = '$CardTypes_TypesNames_WfSatellite';

  /**
   * Card type group for "WfSatellite".
   */
  readonly Group: string = 'Wf';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WfTaskCard

/**
 * ID: {de75a343-8164-472d-a20e-4937819760ac}
 * Alias: WfTaskCard
 * Caption: $CardTypes_TypesNames_WfTaskCard
 * Group: Wf
 */
class WfTaskCardTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WfTaskCard": {de75a343-8164-472d-a20e-4937819760ac}.
   */
  readonly ID: guid = 'de75a343-8164-472d-a20e-4937819760ac';

  /**
   * Card type name for "WfTaskCard".
   */
  readonly Alias: string = 'WfTaskCard';

  /**
   * Card type caption for "WfTaskCard".
   */
  readonly Caption: string = '$CardTypes_TypesNames_WfTaskCard';

  /**
   * Card type group for "WfTaskCard".
   */
  readonly Group: string = 'Wf';

  //#endregion

  //#region Forms

  readonly FormWfTaskCard: string = 'WfTaskCard';

  //#endregion

  //#region Blocks

  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock1: string = 'Block1';

  /**
   * Block caption "KrBlockForDocStatus" for "KrBlockForDocStatus".
   */
  readonly BlockKrBlockForDocStatus: string = 'KrBlockForDocStatus';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockBlock4: string = 'Block4';
  readonly BlockBlock5: string = 'Block5';

  //#endregion

  //#region Controls

  readonly NavigateMainCard: string = 'NavigateMainCard';
  readonly DocStateControl: string = 'DocStateControl';
  readonly DocStateChangedControl: string = 'DocStateChangedControl';
  readonly TaskFiles: string = 'TaskFiles';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowActionEditor

/**
 * ID: {d0960012-d05a-4c86-9e45-ab467231d11d}
 * Alias: WorkflowActionEditor
 * Caption: $CardTypes_TypesNames_ActionTemplate
 * Group: WorkflowEngine
 */
class WorkflowActionEditorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowActionEditor": {d0960012-d05a-4c86-9e45-ab467231d11d}.
   */
  readonly ID: guid = 'd0960012-d05a-4c86-9e45-ab467231d11d';

  /**
   * Card type name for "WorkflowActionEditor".
   */
  readonly Alias: string = 'WorkflowActionEditor';

  /**
   * Card type caption for "WorkflowActionEditor".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ActionTemplate';

  /**
   * Card type group for "WorkflowActionEditor".
   */
  readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Forms

  readonly FormWorkflowActionEditor: string = 'WorkflowActionEditor';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockAdditionalBlock: string = 'AdditionalBlock';
  readonly BlockPreScript: string = 'PreScript';
  readonly BlockPostScript: string = 'PostScript';

  //#endregion

  //#region Controls

  readonly EditParametersButton: string = 'EditParametersButton';
  readonly CompileButton: string = 'CompileButton';
  readonly OpenFileButton: string = 'OpenFileButton';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowActionInstanceEditor

/**
 * ID: {f9e6b610-60cf-4e6a-bcfc-40ccff3be8db}
 * Alias: WorkflowActionInstanceEditor
 * Caption: $CardTypes_TypesNames_ActionInstance
 * Group: WorkflowEngine
 */
class WorkflowActionInstanceEditorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowActionInstanceEditor": {f9e6b610-60cf-4e6a-bcfc-40ccff3be8db}.
   */
  readonly ID: guid = 'f9e6b610-60cf-4e6a-bcfc-40ccff3be8db';

  /**
   * Card type name for "WorkflowActionInstanceEditor".
   */
  readonly Alias: string = 'WorkflowActionInstanceEditor';

  /**
   * Card type caption for "WorkflowActionInstanceEditor".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ActionInstance';

  /**
   * Card type group for "WorkflowActionInstanceEditor".
   */
  readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Forms

  readonly FormWorkflowActionInstanceEditor: string = 'WorkflowActionInstanceEditor';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly EditParametersButton: string = 'EditParametersButton';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowAddFileFromTemplateAction

/**
 * ID: {8b0a3173-b495-4978-b445-23a0e79eda25}
 * Alias: WorkflowAddFileFromTemplateAction
 * Caption: $CardTypes_TypesNames_AddFileFromTemplate
 * Group: WorkflowActions
 */
class WorkflowAddFileFromTemplateActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowAddFileFromTemplateAction": {8b0a3173-b495-4978-b445-23a0e79eda25}.
   */
  readonly ID: guid = '8b0a3173-b495-4978-b445-23a0e79eda25';

  /**
   * Card type name for "WorkflowAddFileFromTemplateAction".
   */
  readonly Alias: string = 'WorkflowAddFileFromTemplateAction';

  /**
   * Card type caption for "WorkflowAddFileFromTemplateAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_AddFileFromTemplate';

  /**
   * Card type group for "WorkflowAddFileFromTemplateAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowAddFileFromTemplateAction: string = 'WorkflowAddFileFromTemplateAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowAndAction

/**
 * ID: {575959db-4dd4-4b82-bbe9-445733b8e7dd}
 * Alias: WorkflowAndAction
 * Caption: $CardTypes_TypesNames_And
 * Group: WorkflowActions
 */
class WorkflowAndActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowAndAction": {575959db-4dd4-4b82-bbe9-445733b8e7dd}.
   */
  readonly ID: guid = '575959db-4dd4-4b82-bbe9-445733b8e7dd';

  /**
   * Card type name for "WorkflowAndAction".
   */
  readonly Alias: string = 'WorkflowAndAction';

  /**
   * Card type caption for "WorkflowAndAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_And';

  /**
   * Card type group for "WorkflowAndAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowCheckRolesForExecutionTileExtension

/**
 * ID: {0295bfb2-acf1-415b-aa6d-b272d8ebc3fe}
 * Alias: WorkflowCheckRolesForExecutionTileExtension
 * Caption: $WorkflowEngine_TilePermission_CheckRolesForExecution
 * Group: WorkflowEngine
 */
class WorkflowCheckRolesForExecutionTileExtensionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowCheckRolesForExecutionTileExtension": {0295bfb2-acf1-415b-aa6d-b272d8ebc3fe}.
   */
  readonly ID: guid = '0295bfb2-acf1-415b-aa6d-b272d8ebc3fe';

  /**
   * Card type name for "WorkflowCheckRolesForExecutionTileExtension".
   */
  readonly Alias: string = 'WorkflowCheckRolesForExecutionTileExtension';

  /**
   * Card type caption for "WorkflowCheckRolesForExecutionTileExtension".
   */
  readonly Caption: string = '$WorkflowEngine_TilePermission_CheckRolesForExecution';

  /**
   * Card type group for "WorkflowCheckRolesForExecutionTileExtension".
   */
  readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Forms

  readonly FormWorkflowCheckRolesForExecutionTileExtension: string = 'WorkflowCheckRolesForExecutionTileExtension';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowCommandAction

/**
 * ID: {91b1cd51-da7a-4721-bb80-f5908fc936a2}
 * Alias: WorkflowCommandAction
 * Caption: $CardTypes_TypesNames_WaitingForSignal
 * Group: WorkflowActions
 */
class WorkflowCommandActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowCommandAction": {91b1cd51-da7a-4721-bb80-f5908fc936a2}.
   */
  readonly ID: guid = '91b1cd51-da7a-4721-bb80-f5908fc936a2';

  /**
   * Card type name for "WorkflowCommandAction".
   */
  readonly Alias: string = 'WorkflowCommandAction';

  /**
   * Card type caption for "WorkflowCommandAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_WaitingForSignal';

  /**
   * Card type group for "WorkflowCommandAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowCommandAction: string = 'WorkflowCommandAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowConditionAction

/**
 * ID: {eb222506-6f7d-4c22-b3d2-d98a2f390ac5}
 * Alias: WorkflowConditionAction
 * Caption: $CardTypes_TypesNames_Condition
 * Group: WorkflowActions
 */
class WorkflowConditionActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowConditionAction": {eb222506-6f7d-4c22-b3d2-d98a2f390ac5}.
   */
  readonly ID: guid = 'eb222506-6f7d-4c22-b3d2-d98a2f390ac5';

  /**
   * Card type name for "WorkflowConditionAction".
   */
  readonly Alias: string = 'WorkflowConditionAction';

  /**
   * Card type caption for "WorkflowConditionAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Condition';

  /**
   * Card type group for "WorkflowConditionAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowConditionAction: string = 'WorkflowConditionAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  /**
   * Block caption "Block3" for "Block3".
   */
  readonly BlockBlock3: string = 'Block3';

  //#endregion

  //#region Controls

  readonly Condition: string = 'Condition';
  readonly Conditions: string = 'Conditions';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowCreateCardAction

/**
 * ID: {1c7a8067-cd49-45ab-a3c9-04a0cfe26c43}
 * Alias: WorkflowCreateCardAction
 * Caption: $CardTypes_TypesNames_CreateCard
 * Group: WorkflowActions
 */
class WorkflowCreateCardActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowCreateCardAction": {1c7a8067-cd49-45ab-a3c9-04a0cfe26c43}.
   */
  readonly ID: guid = '1c7a8067-cd49-45ab-a3c9-04a0cfe26c43';

  /**
   * Card type name for "WorkflowCreateCardAction".
   */
  readonly Alias: string = 'WorkflowCreateCardAction';

  /**
   * Card type caption for "WorkflowCreateCardAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_CreateCard';

  /**
   * Card type group for "WorkflowCreateCardAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowCreateCardAction: string = 'WorkflowCreateCardAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowDialogAction

/**
 * ID: {fc004cdf-e9ed-4549-9f35-781cbc779af2}
 * Alias: WorkflowDialogAction
 * Caption: $CardTypes_TypesNames_Dialog
 * Group: WorkflowActions
 */
class WorkflowDialogActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowDialogAction": {fc004cdf-e9ed-4549-9f35-781cbc779af2}.
   */
  readonly ID: guid = 'fc004cdf-e9ed-4549-9f35-781cbc779af2';

  /**
   * Card type name for "WorkflowDialogAction".
   */
  readonly Alias: string = 'WorkflowDialogAction';

  /**
   * Card type caption for "WorkflowDialogAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Dialog';

  /**
   * Card type group for "WorkflowDialogAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowDialogAction: string = 'WorkflowDialogAction';

  //#endregion

  //#region Blocks

  readonly BlockMainBlock: string = 'MainBlock';
  readonly BlockTaskBlock: string = 'TaskBlock';
  readonly BlockBlock1: string = 'Block1';
  readonly BlockScripts: string = 'Scripts';

  //#endregion

  //#region Controls

  readonly DialogType: string = 'DialogType';
  readonly DialogStoreMode: string = 'DialogStoreMode';
  readonly DialogOpenMode: string = 'DialogOpenMode';
  readonly KeepFiles: string = 'KeepFiles';
  readonly IsCloseWithoutConfirmation: string = 'IsCloseWithoutConfirmation';
  readonly ButtonCancel: string = 'ButtonCancel';
  readonly ButtonNotEnd: string = 'ButtonNotEnd';
  readonly ButtonLinks: string = 'ButtonLinks';
  readonly ButtonScenario: string = 'ButtonScenario';
  readonly ButtonSettings: string = 'ButtonSettings';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowEndAction

/**
 * ID: {86913540-9f30-4a6b-b0d5-a24a674b2ef2}
 * Alias: WorkflowEndAction
 * Caption: $CardTypes_TypesNames_EndProcess
 * Group: WorkflowActions
 */
class WorkflowEndActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowEndAction": {86913540-9f30-4a6b-b0d5-a24a674b2ef2}.
   */
  readonly ID: guid = '86913540-9f30-4a6b-b0d5-a24a674b2ef2';

  /**
   * Card type name for "WorkflowEndAction".
   */
  readonly Alias: string = 'WorkflowEndAction';

  /**
   * Card type caption for "WorkflowEndAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_EndProcess';

  /**
   * Card type group for "WorkflowEndAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowEndAction: string = 'WorkflowEndAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowEngineSettings

/**
 * ID: {a73da16b-d69b-4957-a2de-b35adebcd85e}
 * Alias: WorkflowEngineSettings
 * Caption: $CardTypes_TypesNames_WorkflowEngineSettings
 * Group: Settings
 */
class WorkflowEngineSettingsTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowEngineSettings": {a73da16b-d69b-4957-a2de-b35adebcd85e}.
   */
  readonly ID: guid = 'a73da16b-d69b-4957-a2de-b35adebcd85e';

  /**
   * Card type name for "WorkflowEngineSettings".
   */
  readonly Alias: string = 'WorkflowEngineSettings';

  /**
   * Card type caption for "WorkflowEngineSettings".
   */
  readonly Caption: string = '$CardTypes_TypesNames_WorkflowEngineSettings';

  /**
   * Card type group for "WorkflowEngineSettings".
   */
  readonly Group: string = 'Settings';

  //#endregion

  //#region Forms

  readonly FormWorkflowEngineSettings: string = 'WorkflowEngineSettings';

  //#endregion

  //#region Blocks

  /**
   * Block caption "License" for "License".
   */
  readonly BlockLicense: string = 'License';
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  /**
   * Control caption "LicenseHint" for "LicenseHint".
   */
  readonly LicenseHint: string = 'LicenseHint';

  readonly TypeSettings: string = 'TypeSettings';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowHistoryManagementAction

/**
 * ID: {4e397642-1672-4598-8851-7ed18378c915}
 * Alias: WorkflowHistoryManagementAction
 * Caption: $CardTypes_TypesNames_HistoryManagementAction
 * Group: WorkflowActions
 */
class WorkflowHistoryManagementActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowHistoryManagementAction": {4e397642-1672-4598-8851-7ed18378c915}.
   */
  readonly ID: guid = '4e397642-1672-4598-8851-7ed18378c915';

  /**
   * Card type name for "WorkflowHistoryManagementAction".
   */
  readonly Alias: string = 'WorkflowHistoryManagementAction';

  /**
   * Card type caption for "WorkflowHistoryManagementAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_HistoryManagementAction';

  /**
   * Card type group for "WorkflowHistoryManagementAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowHistoryManagementAction: string = 'WorkflowHistoryManagementAction';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "KrTaskHistoryBlockAlias".
   */
  readonly BlockKrTaskHistoryBlockAlias: string = 'KrTaskHistoryBlockAlias';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowLinkEditor

/**
 * ID: {eb4d7730-6104-4f37-9f63-0c0fa8baf3d1}
 * Alias: WorkflowLinkEditor
 * Caption: $CardTypes_TypesNames_Link
 * Group: WorkflowEngine
 */
class WorkflowLinkEditorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowLinkEditor": {eb4d7730-6104-4f37-9f63-0c0fa8baf3d1}.
   */
  readonly ID: guid = 'eb4d7730-6104-4f37-9f63-0c0fa8baf3d1';

  /**
   * Card type name for "WorkflowLinkEditor".
   */
  readonly Alias: string = 'WorkflowLinkEditor';

  /**
   * Card type caption for "WorkflowLinkEditor".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Link';

  /**
   * Card type group for "WorkflowLinkEditor".
   */
  readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Forms

  readonly FormWorkflowLinkEditor: string = 'WorkflowLinkEditor';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockAdditional: string = 'Additional';
  readonly BlockExitSettings: string = 'ExitSettings';
  readonly BlockEnterSettings: string = 'EnterSettings';

  //#endregion

  //#region Controls

  readonly CompileButton: string = 'CompileButton';
  readonly OpenFileButton: string = 'OpenFileButton';
  readonly LockProcess: string = 'LockProcess';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowNewItemEditor

/**
 * ID: {798aafa5-4bfc-4c16-9e6f-9a761e83d6cc}
 * Alias: WorkflowNewItemEditor
 * Caption: Редатор элементов на левой панели (пока не используется)
 * Group: WorkflowEngine
 */
class WorkflowNewItemEditorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowNewItemEditor": {798aafa5-4bfc-4c16-9e6f-9a761e83d6cc}.
   */
  readonly ID: guid = '798aafa5-4bfc-4c16-9e6f-9a761e83d6cc';

  /**
   * Card type name for "WorkflowNewItemEditor".
   */
  readonly Alias: string = 'WorkflowNewItemEditor';

  /**
   * Card type caption for "WorkflowNewItemEditor".
   */
  readonly Caption: string = 'Редатор элементов на левой панели (пока не используется)';

  /**
   * Card type group for "WorkflowNewItemEditor".
   */
  readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Forms

  /**
   * Form caption "Редактор шаблона элемента" for "WorkflowNewItemEditor".
   */
  readonly FormWorkflowNewItemEditor: string = 'WorkflowNewItemEditor';

  //#endregion

  //#region Blocks

  /**
   * Block caption "Block1" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  /**
   * Control caption "Редактировать шаблон" for "EditTemplateButton".
   */
  readonly EditTemplateButton: string = 'EditTemplateButton';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowNodeEditor

/**
 * ID: {f14d88f8-5e3d-4609-b377-0ad9f647d87a}
 * Alias: WorkflowNodeEditor
 * Caption: $CardTypes_TypesNames_NodeTemplate
 * Group: WorkflowEngine
 */
class WorkflowNodeEditorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowNodeEditor": {f14d88f8-5e3d-4609-b377-0ad9f647d87a}.
   */
  readonly ID: guid = 'f14d88f8-5e3d-4609-b377-0ad9f647d87a';

  /**
   * Card type name for "WorkflowNodeEditor".
   */
  readonly Alias: string = 'WorkflowNodeEditor';

  /**
   * Card type caption for "WorkflowNodeEditor".
   */
  readonly Caption: string = '$CardTypes_TypesNames_NodeTemplate';

  /**
   * Card type group for "WorkflowNodeEditor".
   */
  readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Forms

  readonly FormWorkflowNodeEditor: string = 'WorkflowNodeEditor';

  //#endregion

  //#region Blocks

  readonly BlockMain: string = 'Main';
  readonly BlockNodeInstances: string = 'NodeInstances';

  /**
   * Block caption "Настройка входящей связи" for "Block1".
   */
  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock2: string = 'Block2';

  //#endregion

  //#region Controls

  readonly Icon: string = 'Icon';
  readonly EditParametersButton: string = 'EditParametersButton';
  readonly CompileButton: string = 'CompileButton';
  readonly Actions: string = 'Actions';
  readonly NodeInstances: string = 'NodeInstances';

  /**
   * Control caption "Блокировать процесс при асинхронной обработке" for "LockProcess".
   */
  readonly LockProcess: string = 'LockProcess';
  readonly InLinks: string = 'InLinks';
  readonly OutLinks: string = 'OutLinks';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowNodeInstanceEditor

/**
 * ID: {34d4cdae-de49-471d-af77-ab9455bfb8cd}
 * Alias: WorkflowNodeInstanceEditor
 * Caption: $CardTypes_TypesNames_NodeInstance
 * Group: WorkflowEngine
 */
class WorkflowNodeInstanceEditorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowNodeInstanceEditor": {34d4cdae-de49-471d-af77-ab9455bfb8cd}.
   */
  readonly ID: guid = '34d4cdae-de49-471d-af77-ab9455bfb8cd';

  /**
   * Card type name for "WorkflowNodeInstanceEditor".
   */
  readonly Alias: string = 'WorkflowNodeInstanceEditor';

  /**
   * Card type caption for "WorkflowNodeInstanceEditor".
   */
  readonly Caption: string = '$CardTypes_TypesNames_NodeInstance';

  /**
   * Card type group for "WorkflowNodeInstanceEditor".
   */
  readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Forms

  readonly FormWorkflowNodeInstanceEditor: string = 'WorkflowNodeInstanceEditor';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockTasks: string = 'Tasks';
  readonly BlockSubprocesses: string = 'Subprocesses';

  //#endregion

  //#region Controls

  readonly EditParametersButton: string = 'EditParametersButton';
  readonly Actions: string = 'Actions';
  readonly Tasks: string = 'Tasks';
  readonly Subprocesses: string = 'Subprocesses';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowNotificationAction

/**
 * ID: {d5e2b20d-46c6-4f23-8ede-820b5a8381a3}
 * Alias: WorkflowNotificationAction
 * Caption: $CardTypes_TypesNames_NotificationAction
 * Group: WorkflowActions
 */
class WorkflowNotificationActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowNotificationAction": {d5e2b20d-46c6-4f23-8ede-820b5a8381a3}.
   */
  readonly ID: guid = 'd5e2b20d-46c6-4f23-8ede-820b5a8381a3';

  /**
   * Card type name for "WorkflowNotificationAction".
   */
  readonly Alias: string = 'WorkflowNotificationAction';

  /**
   * Card type caption for "WorkflowNotificationAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_NotificationAction';

  /**
   * Card type group for "WorkflowNotificationAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowNotificationAction: string = 'WorkflowNotificationAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockCustomEmail: string = 'CustomEmail';

  //#endregion

  //#region Controls

  readonly CustomNotificationType: string = 'CustomNotificationType';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowProcess

/**
 * ID: {bb3e1452-30da-4eb2-a4fe-10871608bed3}
 * Alias: WorkflowProcess
 * Caption: $CardTypes_TypesNames_ProcessInstance
 * Group: WorkflowEngine
 */
class WorkflowProcessTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowProcess": {bb3e1452-30da-4eb2-a4fe-10871608bed3}.
   */
  readonly ID: guid = 'bb3e1452-30da-4eb2-a4fe-10871608bed3';

  /**
   * Card type name for "WorkflowProcess".
   */
  readonly Alias: string = 'WorkflowProcess';

  /**
   * Card type caption for "WorkflowProcess".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ProcessInstance';

  /**
   * Card type group for "WorkflowProcess".
   */
  readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowProcessEditor

/**
 * ID: {d7420bf0-ce4e-4155-9b97-0117b9d1e13c}
 * Alias: WorkflowProcessEditor
 * Caption: $CardTypes_TypesNames_ProcessTemplate
 * Group: WorkflowEngine
 */
class WorkflowProcessEditorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowProcessEditor": {d7420bf0-ce4e-4155-9b97-0117b9d1e13c}.
   */
  readonly ID: guid = 'd7420bf0-ce4e-4155-9b97-0117b9d1e13c';

  /**
   * Card type name for "WorkflowProcessEditor".
   */
  readonly Alias: string = 'WorkflowProcessEditor';

  /**
   * Card type caption for "WorkflowProcessEditor".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ProcessTemplate';

  /**
   * Card type group for "WorkflowProcessEditor".
   */
  readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Forms

  readonly FormWorkflowProcessEditor: string = 'WorkflowProcessEditor';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  /**
   * Block caption "ScriptManagement" for "ScriptManagement".
   */
  readonly BlockScriptManagement: string = 'ScriptManagement';

  /**
   * Block caption "Script" for "Script".
   */
  readonly BlockScript: string = 'Script';

  //#endregion

  //#region Controls

  readonly EditParametersButton: string = 'EditParametersButton';
  readonly CompileButton: string = 'CompileButton';
  readonly OpenFileButton: string = 'OpenFileButton';
  readonly SelectProject: string = 'SelectProject';
  readonly UpdateProject: string = 'UpdateProject';
  readonly UpdateProcess: string = 'UpdateProcess';
  readonly ShowReferences: string = 'ShowReferences';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowProcessInstanceEditor

/**
 * ID: {584680fa-e289-42dc-8c88-91ece7440316}
 * Alias: WorkflowProcessInstanceEditor
 * Caption: $CardTypes_TypesNames_ProcessInstance
 * Group: WorkflowEngine
 */
class WorkflowProcessInstanceEditorTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowProcessInstanceEditor": {584680fa-e289-42dc-8c88-91ece7440316}.
   */
  readonly ID: guid = '584680fa-e289-42dc-8c88-91ece7440316';

  /**
   * Card type name for "WorkflowProcessInstanceEditor".
   */
  readonly Alias: string = 'WorkflowProcessInstanceEditor';

  /**
   * Card type caption for "WorkflowProcessInstanceEditor".
   */
  readonly Caption: string = '$CardTypes_TypesNames_ProcessInstance';

  /**
   * Card type group for "WorkflowProcessInstanceEditor".
   */
  readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Forms

  readonly FormWorkflowProcessInstanceEditor: string = 'WorkflowProcessInstanceEditor';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockErrors: string = 'Errors';

  //#endregion

  //#region Controls

  readonly EditParametersButton: string = 'EditParametersButton';
  readonly Errors: string = 'Errors';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowScenarioAction

/**
 * ID: {a7179d00-8641-4bde-b1ce-0f97053fe1e2}
 * Alias: WorkflowScenarioAction
 * Caption: $CardTypes_TypesNames_Scenario
 * Group: WorkflowActions
 */
class WorkflowScenarioActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowScenarioAction": {a7179d00-8641-4bde-b1ce-0f97053fe1e2}.
   */
  readonly ID: guid = 'a7179d00-8641-4bde-b1ce-0f97053fe1e2';

  /**
   * Card type name for "WorkflowScenarioAction".
   */
  readonly Alias: string = 'WorkflowScenarioAction';

  /**
   * Card type caption for "WorkflowScenarioAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Scenario';

  /**
   * Card type group for "WorkflowScenarioAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowScenarioAction: string = 'WorkflowScenarioAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowSendSignalAction

/**
 * ID: {3e2ea605-b60d-42e4-ace9-97d2f343a778}
 * Alias: WorkflowSendSignalAction
 * Caption: $CardTypes_TypesNames_SendSignal
 * Group: WorkflowActions
 */
class WorkflowSendSignalActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowSendSignalAction": {3e2ea605-b60d-42e4-ace9-97d2f343a778}.
   */
  readonly ID: guid = '3e2ea605-b60d-42e4-ace9-97d2f343a778';

  /**
   * Card type name for "WorkflowSendSignalAction".
   */
  readonly Alias: string = 'WorkflowSendSignalAction';

  /**
   * Card type caption for "WorkflowSendSignalAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_SendSignal';

  /**
   * Card type group for "WorkflowSendSignalAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowSendSignalAction: string = 'WorkflowSendSignalAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowStartAction

/**
 * ID: {a4f7fb59-cc66-4aba-b593-3f4d26698666}
 * Alias: WorkflowStartAction
 * Caption: $CardTypes_TypesNames_StartProcess
 * Group: WorkflowActions
 */
class WorkflowStartActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowStartAction": {a4f7fb59-cc66-4aba-b593-3f4d26698666}.
   */
  readonly ID: guid = 'a4f7fb59-cc66-4aba-b593-3f4d26698666';

  /**
   * Card type name for "WorkflowStartAction".
   */
  readonly Alias: string = 'WorkflowStartAction';

  /**
   * Card type caption for "WorkflowStartAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_StartProcess';

  /**
   * Card type group for "WorkflowStartAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowStartAction: string = 'WorkflowStartAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowSubprocessAction

/**
 * ID: {d1d3acfd-8a78-4009-ac38-4dd851c8176a}
 * Alias: WorkflowSubprocessAction
 * Caption: $CardTypes_TypesNames_Subprocess
 * Group: WorkflowActions
 */
class WorkflowSubprocessActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowSubprocessAction": {d1d3acfd-8a78-4009-ac38-4dd851c8176a}.
   */
  readonly ID: guid = 'd1d3acfd-8a78-4009-ac38-4dd851c8176a';

  /**
   * Card type name for "WorkflowSubprocessAction".
   */
  readonly Alias: string = 'WorkflowSubprocessAction';

  /**
   * Card type caption for "WorkflowSubprocessAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Subprocess';

  /**
   * Card type group for "WorkflowSubprocessAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowSubprocessAction: string = 'WorkflowSubprocessAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly OpenSubprocessEditor: string = 'OpenSubprocessEditor';
  readonly ProcessParam: string = 'ProcessParam';
  readonly StartMappingTable: string = 'StartMappingTable';
  readonly EndMappingTable: string = 'EndMappingTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowSubprocessControlAction

/**
 * ID: {3dc924c7-a7c8-407c-a7c7-5d2d77e6e1bc}
 * Alias: WorkflowSubprocessControlAction
 * Caption: $CardTypes_TypesNames_SubprocessControl
 * Group: WorkflowActions
 */
class WorkflowSubprocessControlActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowSubprocessControlAction": {3dc924c7-a7c8-407c-a7c7-5d2d77e6e1bc}.
   */
  readonly ID: guid = '3dc924c7-a7c8-407c-a7c7-5d2d77e6e1bc';

  /**
   * Card type name for "WorkflowSubprocessControlAction".
   */
  readonly Alias: string = 'WorkflowSubprocessControlAction';

  /**
   * Card type caption for "WorkflowSubprocessControlAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_SubprocessControl';

  /**
   * Card type group for "WorkflowSubprocessControlAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowSubprocessControlAction: string = 'WorkflowSubprocessControlAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowTaskAction

/**
 * ID: {dc541752-496d-45eb-ad71-92ab186d7601}
 * Alias: WorkflowTaskAction
 * Caption: $CardTypes_TypesNames_Task
 * Group: WorkflowActions
 */
class WorkflowTaskActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowTaskAction": {dc541752-496d-45eb-ad71-92ab186d7601}.
   */
  readonly ID: guid = 'dc541752-496d-45eb-ad71-92ab186d7601';

  /**
   * Card type name for "WorkflowTaskAction".
   */
  readonly Alias: string = 'WorkflowTaskAction';

  /**
   * Card type caption for "WorkflowTaskAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Task';

  /**
   * Card type group for "WorkflowTaskAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowTaskAction: string = 'WorkflowTaskAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock2: string = 'Block2';
  readonly BlockMainBlock: string = 'MainBlock';
  readonly BlockScripts: string = 'Scripts';

  //#endregion

  //#region Controls

  readonly DialogType: string = 'DialogType';
  readonly ButtonCancel: string = 'ButtonCancel';
  readonly ButtonNotEnd: string = 'ButtonNotEnd';
  readonly ButtonLinks: string = 'ButtonLinks';
  readonly ButtonScenario: string = 'ButtonScenario';
  readonly ButtonSettings: string = 'ButtonSettings';
  readonly DialogsTable: string = 'DialogsTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowTaskControlAction

/**
 * ID: {c80a6abd-361d-4290-bca4-3df7a03bbc10}
 * Alias: WorkflowTaskControlAction
 * Caption: $CardTypes_TypesNames_TaskControl
 * Group: WorkflowActions
 */
class WorkflowTaskControlActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowTaskControlAction": {c80a6abd-361d-4290-bca4-3df7a03bbc10}.
   */
  readonly ID: guid = 'c80a6abd-361d-4290-bca4-3df7a03bbc10';

  /**
   * Card type name for "WorkflowTaskControlAction".
   */
  readonly Alias: string = 'WorkflowTaskControlAction';

  /**
   * Card type caption for "WorkflowTaskControlAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TaskControl';

  /**
   * Card type group for "WorkflowTaskControlAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowTaskControlAction: string = 'WorkflowTaskControlAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowTaskGroupAction

/**
 * ID: {b0d0d053-4ef2-46bc-b3bf-5c701532760e}
 * Alias: WorkflowTaskGroupAction
 * Caption: $CardTypes_TypesNames_TaskGroup
 * Group: WorkflowActions
 */
class WorkflowTaskGroupActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowTaskGroupAction": {b0d0d053-4ef2-46bc-b3bf-5c701532760e}.
   */
  readonly ID: guid = 'b0d0d053-4ef2-46bc-b3bf-5c701532760e';

  /**
   * Card type name for "WorkflowTaskGroupAction".
   */
  readonly Alias: string = 'WorkflowTaskGroupAction';

  /**
   * Card type caption for "WorkflowTaskGroupAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TaskGroup';

  /**
   * Card type group for "WorkflowTaskGroupAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowTaskGroupAction: string = 'WorkflowTaskGroupAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';
  readonly BlockBlock2: string = 'Block2';
  readonly BlockBlock3: string = 'Block3';
  readonly BlockMainBlock: string = 'MainBlock';
  readonly BlockScripts: string = 'Scripts';

  //#endregion

  //#region Controls

  readonly DialogType: string = 'DialogType';
  readonly ButtonCancel: string = 'ButtonCancel';
  readonly ButtonNotEnd: string = 'ButtonNotEnd';
  readonly ButtonLinks: string = 'ButtonLinks';
  readonly ButtonScenario: string = 'ButtonScenario';
  readonly ButtonSettings: string = 'ButtonSettings';
  readonly DialogsTable: string = 'DialogsTable';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowTaskGroupControlAction

/**
 * ID: {ec60ba12-edd9-48d5-be49-6a2f4caf22fb}
 * Alias: WorkflowTaskGroupControlAction
 * Caption: $CardTypes_TypesNames_TaskGroupControl
 * Group: WorkflowActions
 */
class WorkflowTaskGroupControlActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowTaskGroupControlAction": {ec60ba12-edd9-48d5-be49-6a2f4caf22fb}.
   */
  readonly ID: guid = 'ec60ba12-edd9-48d5-be49-6a2f4caf22fb';

  /**
   * Card type name for "WorkflowTaskGroupControlAction".
   */
  readonly Alias: string = 'WorkflowTaskGroupControlAction';

  /**
   * Card type caption for "WorkflowTaskGroupControlAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TaskGroupControl';

  /**
   * Card type group for "WorkflowTaskGroupControlAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowTaskGroupControlAction: string = 'WorkflowTaskGroupControlAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly UseAsNextRole: string = 'UseAsNextRole';
  readonly ResumeGroup: string = 'ResumeGroup';
  readonly PauseGroup: string = 'PauseGroup';
  readonly CancelGroup: string = 'CancelGroup';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowTimerAction

/**
 * ID: {a2d50ba1-434b-406a-a31c-f86c821b6b20}
 * Alias: WorkflowTimerAction
 * Caption: $CardTypes_TypesNames_Timer
 * Group: WorkflowActions
 */
class WorkflowTimerActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowTimerAction": {a2d50ba1-434b-406a-a31c-f86c821b6b20}.
   */
  readonly ID: guid = 'a2d50ba1-434b-406a-a31c-f86c821b6b20';

  /**
   * Card type name for "WorkflowTimerAction".
   */
  readonly Alias: string = 'WorkflowTimerAction';

  /**
   * Card type caption for "WorkflowTimerAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Timer';

  /**
   * Card type group for "WorkflowTimerAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowTimerAction: string = 'WorkflowTimerAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region Controls

  readonly ExitCondition: string = 'ExitCondition';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region WorkflowTimerControlAction

/**
 * ID: {fd97cf1e-a1c6-4b5f-9b0c-1b254735e17a}
 * Alias: WorkflowTimerControlAction
 * Caption: $CardTypes_TypesNames_TimerControl
 * Group: WorkflowActions
 */
class WorkflowTimerControlActionTypeInfo {
  //#region Common

  /**
   * Card type identifier for "WorkflowTimerControlAction": {fd97cf1e-a1c6-4b5f-9b0c-1b254735e17a}.
   */
  readonly ID: guid = 'fd97cf1e-a1c6-4b5f-9b0c-1b254735e17a';

  /**
   * Card type name for "WorkflowTimerControlAction".
   */
  readonly Alias: string = 'WorkflowTimerControlAction';

  /**
   * Card type caption for "WorkflowTimerControlAction".
   */
  readonly Caption: string = '$CardTypes_TypesNames_TimerControl';

  /**
   * Card type group for "WorkflowTimerControlAction".
   */
  readonly Group: string = 'WorkflowActions';

  //#endregion

  //#region Forms

  readonly FormWorkflowTimerControlAction: string = 'WorkflowTimerControlAction';

  //#endregion

  //#region Blocks

  readonly BlockBlock1: string = 'Block1';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

//#region Workplace

/**
 * ID: {12fd0e19-d751-4d8e-959d-78c9763bbb38}
 * Alias: Workplace
 * Caption: $CardTypes_TypesNames_Workplace
 * Group: System
 */
class WorkplaceTypeInfo {
  //#region Common

  /**
   * Card type identifier for "Workplace": {12fd0e19-d751-4d8e-959d-78c9763bbb38}.
   */
  readonly ID: guid = '12fd0e19-d751-4d8e-959d-78c9763bbb38';

  /**
   * Card type name for "Workplace".
   */
  readonly Alias: string = 'Workplace';

  /**
   * Card type caption for "Workplace".
   */
  readonly Caption: string = '$CardTypes_TypesNames_Workplace';

  /**
   * Card type group for "Workplace".
   */
  readonly Group: string = 'System';

  //#endregion

  //#region Forms

  readonly FormWorkplace: string = 'Workplace';

  //#endregion

  //#region Blocks

  readonly BlockMainInformation: string = 'MainInformation';
  readonly BlockRoles: string = 'Roles';

  //#endregion

  //#region ToString

  get ToString(): string {
    return this.Alias;
  }

  //#endregion
}

//#endregion

export class TypeInfo {
  //#region Const

   readonly CurrentRow: string = 'CurrentRow';
   readonly SelectedRow: string = 'SelectedRow';
   readonly SelectedItem: string = 'SelectedItem';
   readonly SelectedItemCount: string = 'SelectedItemCount';
   readonly SelectedDate: string = 'SelectedDate';
   readonly IsActive: string = 'IsActive';
   readonly IsChecked: string = 'IsChecked';
   readonly IsExpanded: string = 'IsExpanded';
   readonly IsDataLoading: string = 'IsDataLoading';
   readonly LastUpdateTime: string = 'LastUpdateTime';
   readonly Parent: string = 'Parent';
   readonly Count: string = 'Count';

  //#endregion

  //#region Types

  /**
   * Card type identifier for "$CardTypes_Blocks_AccountSettings": {48332157-4f6a-4cb1-ace6-4b811c0e5364}.
   */
  static get AccountUserSettings(): AccountUserSettingsTypeInfo {
    return TypeInfo.accountUserSettings = TypeInfo.accountUserSettings ?? new AccountUserSettingsTypeInfo();
  }

  private static accountUserSettings: AccountUserSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_AclGenerationRule": {76b57ab5-19d4-4cc9-9e80-2c2f05cc2425}.
   */
  static get AclGenerationRule(): AclGenerationRuleTypeInfo {
    return TypeInfo.aclGenerationRule = TypeInfo.aclGenerationRule ?? new AclGenerationRuleTypeInfo();
  }

  private static aclGenerationRule: AclGenerationRuleTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ActionHistoryRecord": {abc13918-aa63-45ca-a3f4-d1fd5673c248}.
   */
  static get ActionHistoryRecord(): ActionHistoryRecordTypeInfo {
    return TypeInfo.actionHistoryRecord = TypeInfo.actionHistoryRecord ?? new ActionHistoryRecordTypeInfo();
  }

  private static actionHistoryRecord: ActionHistoryRecordTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ADSync": {cdaa9e03-9e06-4b4d-9a21-cfc446d2d9d1}.
   */
  static get AdSync(): AdSyncTypeInfo {
    return TypeInfo.adSync = TypeInfo.adSync ?? new AdSyncTypeInfo();
  }

  private static adSync: AdSyncTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Application": {029094a5-52a1-4f4f-8aa6-3297f41a3a75}.
   */
  static get Application(): ApplicationTypeInfo {
    return TypeInfo.application = TypeInfo.application ?? new ApplicationTypeInfo();
  }

  private static application: ApplicationTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_AuthorCondition": {13502054-248d-4619-81e0-5ed08b5a5db9}.
   */
  static get AuthorCondition(): AuthorConditionTypeInfo {
    return TypeInfo.authorCondition = TypeInfo.authorCondition ?? new AuthorConditionTypeInfo();
  }

  private static authorCondition: AuthorConditionTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {8067e1c6-15a4-4741-b62c-29e5257efedd}.
   */
  static get AutoCompleteDialogs(): AutoCompleteDialogsTypeInfo {
    return TypeInfo.autoCompleteDialogs = TypeInfo.autoCompleteDialogs ?? new AutoCompleteDialogsTypeInfo();
  }

  private static autoCompleteDialogs: AutoCompleteDialogsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_BusinessProcessTemplate": {d05799bd-35b6-43b4-97dd-b6d0e683eff2}.
   */
  static get BusinessProcessTemplate(): BusinessProcessTemplateTypeInfo {
    return TypeInfo.businessProcessTemplate = TypeInfo.businessProcessTemplate ?? new BusinessProcessTemplateTypeInfo();
  }

  private static businessProcessTemplate: BusinessProcessTemplateTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Tabs_Calendar": {9079d7f9-7b44-43f2-bf11-f9ed11965dd6}.
   */
  static get Calendar(): CalendarTypeInfo {
    return TypeInfo.calendar = TypeInfo.calendar ?? new CalendarTypeInfo();
  }

  private static calendar: CalendarTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Tabs_Controls_CalendarCalcMethod": {2a9b3b6d-1c22-46dc-ada2-7437fd4273ce}.
   */
  static get CalendarCalcMethod(): CalendarCalcMethodTypeInfo {
    return TypeInfo.calendarCalcMethod = TypeInfo.calendarCalcMethod ?? new CalendarCalcMethodTypeInfo();
  }

  private static calendarCalcMethod: CalendarCalcMethodTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Car": {d0006e40-a342-4797-8d77-6501c4b7c4ac}.
   */
  static get Car(): CarTypeInfo {
    return TypeInfo.car = TypeInfo.car ?? new CarTypeInfo();
  }

  private static car: CarTypeInfo;

  /**
   * Card type identifier for "$CardTypes_Tabs_Dialog1C": {0fa7aaeb-dc36-4647-a29e-2eb87153984d}.
   */
  static get Car1CDialog(): Car1CDialogTypeInfo {
    return TypeInfo.car1CDialog = TypeInfo.car1CDialog ?? new Car1CDialogTypeInfo();
  }

  private static car1CDialog: Car1CDialogTypeInfo;

  /**
   * Card type identifier for "$CardTypes_CardTasksEditor": {db737600-1bf6-451b-80ca-01fe06161ee6}.
   */
  static get CardTasksEditorDialog(): CardTasksEditorDialogTypeInfo {
    return TypeInfo.cardTasksEditorDialog = TypeInfo.cardTasksEditorDialog ?? new CardTasksEditorDialogTypeInfo();
  }

  private static cardTasksEditorDialog: CardTasksEditorDialogTypeInfo;

  /**
   * Card type identifier for "$Views_FilterDialog_Caption": {ef08853d-7fdf-4fec-91b2-a1b8905e29fc}.
   */
  static get CarViewParameters(): CarViewParametersTypeInfo {
    return TypeInfo.carViewParameters = TypeInfo.carViewParameters ?? new CarViewParametersTypeInfo();
  }

  private static carViewParameters: CarViewParametersTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_CompletionOption": {f6b95639-234e-4800-a2f1-3cb20e0bcda4}.
   */
  static get CompletionOption(): CompletionOptionTypeInfo {
    return TypeInfo.completionOption = TypeInfo.completionOption ?? new CompletionOptionTypeInfo();
  }

  private static completionOption: CompletionOptionTypeInfo;

  /**
   * Card type identifier for "ConditionsBase": {0eb20600-f137-42ad-b007-440492200acf}.
   */
  static get ConditionsBase(): ConditionsBaseTypeInfo {
    return TypeInfo.conditionsBase = TypeInfo.conditionsBase ?? new ConditionsBaseTypeInfo();
  }

  private static conditionsBase: ConditionsBaseTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ConditionType": {ca37649d-9585-4c83-8ef4-fdf91c4c42ee}.
   */
  static get ConditionType(): ConditionTypeTypeInfo {
    return TypeInfo.conditionType = TypeInfo.conditionType ?? new ConditionTypeTypeInfo();
  }

  private static conditionType: ConditionTypeTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ContextRole": {b672e00c-0241-0485-9b07-4764bc96c9d3}.
   */
  static get ContextRole(): ContextRoleTypeInfo {
    return TypeInfo.contextRole = TypeInfo.contextRole ?? new ContextRoleTypeInfo();
  }

  private static contextRole: ContextRoleTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Contract": {335f86a1-d009-012c-8b45-1f43c2382c2d}.
   */
  static get Contract(): ContractTypeInfo {
    return TypeInfo.contract = TypeInfo.contract ?? new ContractTypeInfo();
  }

  private static contract: ContractTypeInfo;

  /**
   * Card type identifier for "$Cards_FileFromTemplate_CreateDialog": {98662e67-2e75-4d33-97d0-e1cefa096336}.
   */
  static get CreateFileFromTemplate(): CreateFileFromTemplateTypeInfo {
    return TypeInfo.createFileFromTemplate = TypeInfo.createFileFromTemplate ?? new CreateFileFromTemplateTypeInfo();
  }

  private static createFileFromTemplate: CreateFileFromTemplateTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Currency": {4bce97d2-6b76-468d-9b09-711ba189cb1e}.
   */
  static get Currency(): CurrencyTypeInfo {
    return TypeInfo.currency = TypeInfo.currency ?? new CurrencyTypeInfo();
  }

  private static currency: CurrencyTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Tabs_DefaultCalendarType": {2d1d11a0-3ed0-46a6-bd5c-8fc8a952eabd}.
   */
  static get DefaultCalendarType(): DefaultCalendarTypeTypeInfo {
    return TypeInfo.defaultCalendarType = TypeInfo.defaultCalendarType ?? new DefaultCalendarTypeTypeInfo();
  }

  private static defaultCalendarType: DefaultCalendarTypeTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Deleted": {f5e74fbb-5357-4a6d-adce-4c2607853fdd}.
   */
  static get Deleted(): DeletedTypeInfo {
    return TypeInfo.deleted = TypeInfo.deleted ?? new DeletedTypeInfo();
  }

  private static deleted: DeletedTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_DepartmentCondition": {c8c6d9b5-0cc0-4f09-8d67-eb3f8e2e0bd4}.
   */
  static get DepartmentCondition(): DepartmentConditionTypeInfo {
    return TypeInfo.departmentCondition = TypeInfo.departmentCondition ?? new DepartmentConditionTypeInfo();
  }

  private static departmentCondition: DepartmentConditionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_DepartmentRole": {abe57cb7-e1cb-06f6-b7ca-ad1668bebd72}.
   */
  static get DepartmentRole(): DepartmentRoleTypeInfo {
    return TypeInfo.departmentRole = TypeInfo.departmentRole ?? new DepartmentRoleTypeInfo();
  }

  private static departmentRole: DepartmentRoleTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Dialogs": {d3107c45-c40a-4a86-b831-fbef1b71050f}.
   */
  static get Dialogs(): DialogsTypeInfo {
    return TypeInfo.dialogs = TypeInfo.dialogs ?? new DialogsTypeInfo();
  }

  private static dialogs: DialogsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_DocLoad": {33023ffa-2fd3-4b3b-80d9-bba6ab48ea8e}.
   */
  static get DocLoad(): DocLoadTypeInfo {
    return TypeInfo.docLoad = TypeInfo.docLoad ?? new DocLoadTypeInfo();
  }

  private static docLoad: DocLoadTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_DocStateCondition": {d57e0e60-6cae-4a4b-a481-2eeb704e46ec}.
   */
  static get DocStateCondition(): DocStateConditionTypeInfo {
    return TypeInfo.docStateCondition = TypeInfo.docStateCondition ?? new DocStateConditionTypeInfo();
  }

  private static docStateCondition: DocStateConditionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_DocTypeCondition": {6a525f7e-deaa-48a0-b2c7-20c2c5151932}.
   */
  static get DocTypeCondition(): DocTypeConditionTypeInfo {
    return TypeInfo.docTypeCondition = TypeInfo.docTypeCondition ?? new DocTypeConditionTypeInfo();
  }

  private static docTypeCondition: DocTypeConditionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Document": {6d06c5a0-9687-4f6b-9bed-d3a081d84d9a}.
   */
  static get Document(): DocumentTypeInfo {
    return TypeInfo.document = TypeInfo.document ?? new DocumentTypeInfo();
  }

  private static document: DocumentTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Category": {5da46e89-f932-4a48-b5a3-ec6285bfe3ea}.
   */
  static get DocumentCategory(): DocumentCategoryTypeInfo {
    return TypeInfo.documentCategory = TypeInfo.documentCategory ?? new DocumentCategoryTypeInfo();
  }

  private static documentCategory: DocumentCategoryTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_DynamicRole": {97a945bc-58f5-07fa-a274-b6a7f0f1282c}.
   */
  static get DynamicRole(): DynamicRoleTypeInfo {
    return TypeInfo.dynamicRole = TypeInfo.dynamicRole ?? new DynamicRoleTypeInfo();
  }

  private static dynamicRole: DynamicRoleTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_EmptyCondition": {4b68414d-bf1a-4227-bb01-b1fc939d0e5a}.
   */
  static get EmptyCondition(): EmptyConditionTypeInfo {
    return TypeInfo.emptyCondition = TypeInfo.emptyCondition ?? new EmptyConditionTypeInfo();
  }

  private static emptyCondition: EmptyConditionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Error": {fa81208d-2d83-4cb6-a83d-cba7e3f483a7}.
   */
  static get Error(): ErrorTypeInfo {
    return TypeInfo.error = TypeInfo.error ?? new ErrorTypeInfo();
  }

  private static error: ErrorTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_FieldChangedCondition": {6077353b-1af6-496f-a930-86954dce122c}.
   */
  static get FieldChangedCondition(): FieldChangedConditionTypeInfo {
    return TypeInfo.fieldChangedCondition = TypeInfo.fieldChangedCondition ?? new FieldChangedConditionTypeInfo();
  }

  private static fieldChangedCondition: FieldChangedConditionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_File": {ab387c69-fd62-0655-bbc3-b879e433a143}.
   */
  static get File(): FileTypeInfo {
    return TypeInfo.file = TypeInfo.file ?? new FileTypeInfo();
  }

  private static file: FileTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_FileCategory": {97182afc-43ce-4bd9-9c96-73a4d2c5d5eb}.
   */
  static get FileCategory(): FileCategoryTypeInfo {
    return TypeInfo.fileCategory = TypeInfo.fileCategory ?? new FileCategoryTypeInfo();
  }

  private static fileCategory: FileCategoryTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_FileConverterCache": {7609d1d7-9a46-4617-8789-2dff55aa4072}.
   */
  static get FileConverterCache(): FileConverterCacheTypeInfo {
    return TypeInfo.fileConverterCache = TypeInfo.fileConverterCache ?? new FileConverterCacheTypeInfo();
  }

  private static fileConverterCache: FileConverterCacheTypeInfo;

  /**
   * Card type identifier for "FilePreviewDialog": {1d196c65-f645-4fea-b515-c0d615dbb867}.
   */
  static get FilePreviewDialog(): FilePreviewDialogTypeInfo {
    return TypeInfo.filePreviewDialog = TypeInfo.filePreviewDialog ?? new FilePreviewDialogTypeInfo();
  }

  private static filePreviewDialog: FilePreviewDialogTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_FileSatellite": {580cd334-6630-420e-acd8-9524be99e43e}.
   */
  static get FileSatellite(): FileSatelliteTypeInfo {
    return TypeInfo.fileSatellite = TypeInfo.fileSatellite ?? new FileSatelliteTypeInfo();
  }

  private static fileSatellite: FileSatelliteTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_FileTemplate": {b7e1b93e-eeda-49b7-9402-2471d4d14bdf}.
   */
  static get FileTemplate(): FileTemplateTypeInfo {
    return TypeInfo.fileTemplate = TypeInfo.fileTemplate ?? new FileTemplateTypeInfo();
  }

  private static fileTemplate: FileTemplateTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_FormatSettings": {568141c1-e258-487d-85c6-8aaeddf387fc}.
   */
  static get FormatSettings(): FormatSettingsTypeInfo {
    return TypeInfo.formatSettings = TypeInfo.formatSettings ?? new FormatSettingsTypeInfo();
  }

  private static formatSettings: FormatSettingsTypeInfo;

  /**
   * Card type identifier for "ForumSatellite": {48e7c07a-295d-479a-9990-02a1f7a5f7db}.
   */
  static get ForumSatellite(): ForumSatelliteTypeInfo {
    return TypeInfo.forumSatellite = TypeInfo.forumSatellite ?? new ForumSatelliteTypeInfo();
  }

  private static forumSatellite: ForumSatelliteTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_FunctionRole": {a830094d-6e03-4242-9c17-0d0a8f2fcb33}.
   */
  static get FunctionRole(): FunctionRoleTypeInfo {
    return TypeInfo.functionRole = TypeInfo.functionRole ?? new FunctionRoleTypeInfo();
  }

  private static functionRole: FunctionRoleTypeInfo;

  /**
   * Card type identifier for "$CardTypes_Blocks_GeneralSettings": {39dce9d4-f429-4552-a268-5a9bf9c1ba53}.
   */
  static get GeneralUserSettings(): GeneralUserSettingsTypeInfo {
    return TypeInfo.generalUserSettings = TypeInfo.generalUserSettings ?? new GeneralUserSettingsTypeInfo();
  }

  private static generalUserSettings: GeneralUserSettingsTypeInfo;

  /**
   * Card type identifier for "$Cards_HelpSection": {3442db47-ab96-4d9c-870f-535e1fc8d42f}.
   */
  static get HelpSection(): HelpSectionTypeInfo {
    return TypeInfo.helpSection = TypeInfo.helpSection ?? new HelpSectionTypeInfo();
  }

  private static helpSection: HelpSectionTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {dc32f94b-82fb-48a1-8c44-79c5240d5f22}.
   */
  static get HelpSectionDialogs(): HelpSectionDialogsTypeInfo {
    return TypeInfo.helpSectionDialogs = TypeInfo.helpSectionDialogs ?? new HelpSectionDialogsTypeInfo();
  }

  private static helpSectionDialogs: HelpSectionDialogsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Incoming": {001f99fd-5bf3-0679-9b6f-455767af72b5}.
   */
  static get Incoming(): IncomingTypeInfo {
    return TypeInfo.incoming = TypeInfo.incoming ?? new IncomingTypeInfo();
  }

  private static incoming: IncomingTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_InitiatorCondition": {8ff1a133-0300-4246-b117-a5455487acc1}.
   */
  static get InitiatorCondition(): InitiatorConditionTypeInfo {
    return TypeInfo.initiatorCondition = TypeInfo.initiatorCondition ?? new InitiatorConditionTypeInfo();
  }

  private static initiatorCondition: InitiatorConditionTypeInfo;

  /**
   * Card type identifier for "$KrActions_Acquaintance": {956a34eb-8318-4d35-92a2-c0df118c01ea}.
   */
  static get KrAcquaintanceAction(): KrAcquaintanceActionTypeInfo {
    return TypeInfo.krAcquaintanceAction = TypeInfo.krAcquaintanceAction ?? new KrAcquaintanceActionTypeInfo();
  }

  private static krAcquaintanceAction: KrAcquaintanceActionTypeInfo;

  /**
   * Card type identifier for "$KrStages_Acquaintance": {728382fe-12b2-444b-b62e-fe4a4d5ac65f}.
   */
  static get KrAcquaintanceStageTypeSettings(): KrAcquaintanceStageTypeSettingsTypeInfo {
    return TypeInfo.krAcquaintanceStageTypeSettings = TypeInfo.krAcquaintanceStageTypeSettings ?? new KrAcquaintanceStageTypeSettingsTypeInfo();
  }

  private static krAcquaintanceStageTypeSettings: KrAcquaintanceStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$KrStages_AddFromTemplate": {b196cbd5-a534-4d18-91f9-561f31a2fe89}.
   */
  static get KrAddFileFromTemplateStageTypeSettings(): KrAddFileFromTemplateStageTypeSettingsTypeInfo {
    return TypeInfo.krAddFileFromTemplateStageTypeSettings = TypeInfo.krAddFileFromTemplateStageTypeSettings ?? new KrAddFileFromTemplateStageTypeSettingsTypeInfo();
  }

  private static krAddFileFromTemplateStageTypeSettings: KrAddFileFromTemplateStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrAdditionalApproval": {b3d8eae3-c6bf-4b59-bcc7-461d526c326c}.
   */
  static get KrAdditionalApproval(): KrAdditionalApprovalTypeInfo {
    return TypeInfo.krAdditionalApproval = TypeInfo.krAdditionalApproval ?? new KrAdditionalApprovalTypeInfo();
  }

  private static krAdditionalApproval: KrAdditionalApprovalTypeInfo;

  /**
   * Card type identifier for "$KrActions_Amending": {9c530e93-ec3a-48ba-b09c-ee9eceb2173e}.
   */
  static get KrAmendingAction(): KrAmendingActionTypeInfo {
    return TypeInfo.krAmendingAction = TypeInfo.krAmendingAction ?? new KrAmendingActionTypeInfo();
  }

  private static krAmendingAction: KrAmendingActionTypeInfo;

  /**
   * Card type identifier for "$KrActions_Approval": {70762c81-bd23-4580-a3fb-c452604f6e78}.
   */
  static get KrApprovalAction(): KrApprovalActionTypeInfo {
    return TypeInfo.krApprovalAction = TypeInfo.krApprovalAction ?? new KrApprovalActionTypeInfo();
  }

  private static krApprovalAction: KrApprovalActionTypeInfo;

  /**
   * Card type identifier for "$KrStages_Approval": {4a377758-2366-47e9-98ac-c5f553974236}.
   */
  static get KrApprovalStageTypeSettings(): KrApprovalStageTypeSettingsTypeInfo {
    return TypeInfo.krApprovalStageTypeSettings = TypeInfo.krApprovalStageTypeSettings ?? new KrApprovalStageTypeSettingsTypeInfo();
  }

  private static krApprovalStageTypeSettings: KrApprovalStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrApprove": {e4d7f6bf-fea9-4a3b-8a5a-e1a0a40de74c}.
   */
  static get KrApprove(): KrApproveTypeInfo {
    return TypeInfo.krApprove = TypeInfo.krApprove ?? new KrApproveTypeInfo();
  }

  private static krApprove: KrApproveTypeInfo;

  /**
   * Card type identifier for "KrAuthorSettings": {02dbc094-7f2c-48b0-acf3-1fc6dddf015c}.
   */
  static get KrAuthorSettings(): KrAuthorSettingsTypeInfo {
    return TypeInfo.krAuthorSettings = TypeInfo.krAuthorSettings ?? new KrAuthorSettingsTypeInfo();
  }

  private static krAuthorSettings: KrAuthorSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrCard": {21bca3fc-f75f-413b-b5c8-49538cbfc761}.
   */
  static get KrCard(): KrCardTypeInfo {
    return TypeInfo.krCard = TypeInfo.krCard ?? new KrCardTypeInfo();
  }

  private static krCard: KrCardTypeInfo;

  /**
   * Card type identifier for "$KrActions_ChangeState": {4f07209c-ab6b-44f6-9460-f594c3bdf8a3}.
   */
  static get KrChangeStateAction(): KrChangeStateActionTypeInfo {
    return TypeInfo.krChangeStateAction = TypeInfo.krChangeStateAction ?? new KrChangeStateActionTypeInfo();
  }

  private static krChangeStateAction: KrChangeStateActionTypeInfo;

  /**
   * Card type identifier for "$KrStages_ChangeState": {784388f6-dad3-4ce2-a8b9-49e73d71784c}.
   */
  static get KrChangeStateStageTypeSettings(): KrChangeStateStageTypeSettingsTypeInfo {
    return TypeInfo.krChangeStateStageTypeSettings = TypeInfo.krChangeStateStageTypeSettings ?? new KrChangeStateStageTypeSettingsTypeInfo();
  }

  private static krChangeStateStageTypeSettings: KrChangeStateStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {9fce6311-1746-412a-9346-77394caebe90}.
   */
  static get KrCheckStateWorkflowTileExtension(): KrCheckStateWorkflowTileExtensionTypeInfo {
    return TypeInfo.krCheckStateWorkflowTileExtension = TypeInfo.krCheckStateWorkflowTileExtension ?? new KrCheckStateWorkflowTileExtensionTypeInfo();
  }

  private static krCheckStateWorkflowTileExtension: KrCheckStateWorkflowTileExtensionTypeInfo;

  /**
   * Card type identifier for "$KrStages_CreateCard": {d444f8d4-be81-4714-b00d-02172fad1c81}.
   */
  static get KrCreateCardStageTypeSettings(): KrCreateCardStageTypeSettingsTypeInfo {
    return TypeInfo.krCreateCardStageTypeSettings = TypeInfo.krCreateCardStageTypeSettings ?? new KrCreateCardStageTypeSettingsTypeInfo();
  }

  private static krCreateCardStageTypeSettings: KrCreateCardStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$KrActions_Deregistration": {94e91c8c-1336-4c04-87c5-11ceb9839de3}.
   */
  static get KrDeregistrationAction(): KrDeregistrationActionTypeInfo {
    return TypeInfo.krDeregistrationAction = TypeInfo.krDeregistrationAction ?? new KrDeregistrationActionTypeInfo();
  }

  private static krDeregistrationAction: KrDeregistrationActionTypeInfo;

  /**
   * Card type identifier for "KrDialogStageTypeSettings": {71464f65-e572-4fba-b54f-3e9f9ef0125a}.
   */
  static get KrDialogStageTypeSettings(): KrDialogStageTypeSettingsTypeInfo {
    return TypeInfo.krDialogStageTypeSettings = TypeInfo.krDialogStageTypeSettings ?? new KrDialogStageTypeSettingsTypeInfo();
  }

  private static krDialogStageTypeSettings: KrDialogStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrDocState": {e83a230a-f5fc-445e-9b44-7d0140ee69f6}.
   */
  static get KrDocState(): KrDocStateTypeInfo {
    return TypeInfo.krDocState = TypeInfo.krDocState ?? new KrDocStateTypeInfo();
  }

  private static krDocState: KrDocStateTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrDocType": {b17f4f35-17e1-4509-994b-ebd576f2c95e}.
   */
  static get KrDocType(): KrDocTypeTypeInfo {
    return TypeInfo.krDocType = TypeInfo.krDocType ?? new KrDocTypeTypeInfo();
  }

  private static krDocType: KrDocTypeTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrEdit": {e19ca9b5-48be-4fdf-8dc5-78534b4767de}.
   */
  static get KrEdit(): KrEditTypeInfo {
    return TypeInfo.krEdit = TypeInfo.krEdit ?? new KrEditTypeInfo();
  }

  private static krEdit: KrEditTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrEditInterject": {c9b93ae3-9b7b-4431-a306-aace4aea8732}.
   */
  static get KrEditInterject(): KrEditInterjectTypeInfo {
    return TypeInfo.krEditInterject = TypeInfo.krEditInterject ?? new KrEditInterjectTypeInfo();
  }

  private static krEditInterject: KrEditInterjectTypeInfo;

  /**
   * Card type identifier for "$KrStages_Edit": {995621e3-fdcf-412b-91a6-3f28fe933e70}.
   */
  static get KrEditStageTypeSettings(): KrEditStageTypeSettingsTypeInfo {
    return TypeInfo.krEditStageTypeSettings = TypeInfo.krEditStageTypeSettings ?? new KrEditStageTypeSettingsTypeInfo();
  }

  private static krEditStageTypeSettings: KrEditStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "KrExampleDialogSatellite": {7cfe67a4-0b8e-423b-8c15-8e2c584b429b}.
   */
  static get KrExampleDialogSatellite(): KrExampleDialogSatelliteTypeInfo {
    return TypeInfo.krExampleDialogSatellite = TypeInfo.krExampleDialogSatellite ?? new KrExampleDialogSatelliteTypeInfo();
  }

  private static krExampleDialogSatellite: KrExampleDialogSatelliteTypeInfo;

  /**
   * Card type identifier for "$KrStages_ForkManagement": {9393407b-d4ff-408b-abbc-de7ce148ea54}.
   */
  static get KrForkManagementStageTypeSettings(): KrForkManagementStageTypeSettingsTypeInfo {
    return TypeInfo.krForkManagementStageTypeSettings = TypeInfo.krForkManagementStageTypeSettings ?? new KrForkManagementStageTypeSettingsTypeInfo();
  }

  private static krForkManagementStageTypeSettings: KrForkManagementStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$KrStages_Fork": {2729c019-fab9-4eb4-bd98-d3628b1a19f6}.
   */
  static get KrForkStageTypeSettings(): KrForkStageTypeSettingsTypeInfo {
    return TypeInfo.krForkStageTypeSettings = TypeInfo.krForkStageTypeSettings ?? new KrForkStageTypeSettingsTypeInfo();
  }

  private static krForkStageTypeSettings: KrForkStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$KrStages_HistoryManagement": {cfe5e2af-1014-4ddb-afa1-7450623b103a}.
   */
  static get KrHistoryManagementStageTypeSettings(): KrHistoryManagementStageTypeSettingsTypeInfo {
    return TypeInfo.krHistoryManagementStageTypeSettings = TypeInfo.krHistoryManagementStageTypeSettings ?? new KrHistoryManagementStageTypeSettingsTypeInfo();
  }

  private static krHistoryManagementStageTypeSettings: KrHistoryManagementStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrInfoForInitiator": {c6f3828f-b001-46f6-b121-3f3ed9e65cde}.
   */
  static get KrInfoForInitiator(): KrInfoForInitiatorTypeInfo {
    return TypeInfo.krInfoForInitiator = TypeInfo.krInfoForInitiator ?? new KrInfoForInitiatorTypeInfo();
  }

  private static krInfoForInitiator: KrInfoForInitiatorTypeInfo;

  /**
   * Card type identifier for "$KrStages_Notification": {9e57dfaf-986e-41c1-a1c0-be007f0a36a0}.
   */
  static get KrNotificationStageTypeSettings(): KrNotificationStageTypeSettingsTypeInfo {
    return TypeInfo.krNotificationStageTypeSettings = TypeInfo.krNotificationStageTypeSettings ?? new KrNotificationStageTypeSettingsTypeInfo();
  }

  private static krNotificationStageTypeSettings: KrNotificationStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "KrPerformersSettings": {5ac67186-b62b-4dc4-b9b9-e74d18f53600}.
   */
  static get KrPerformersSettings(): KrPerformersSettingsTypeInfo {
    return TypeInfo.krPerformersSettings = TypeInfo.krPerformersSettings ?? new KrPerformersSettingsTypeInfo();
  }

  private static krPerformersSettings: KrPerformersSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrPermissions": {fa9dbdac-8708-41df-bd72-900f69655dfa}.
   */
  static get KrPermissions(): KrPermissionsTypeInfo {
    return TypeInfo.krPermissions = TypeInfo.krPermissions ?? new KrPermissionsTypeInfo();
  }

  private static krPermissions: KrPermissionsTypeInfo;

  /**
   * Card type identifier for "$KrStages_ProcessManagement": {ff753641-0691-4cfc-a8cc-baa89b25a83b}.
   */
  static get KrProcessManagementStageTypeSettings(): KrProcessManagementStageTypeSettingsTypeInfo {
    return TypeInfo.krProcessManagementStageTypeSettings = TypeInfo.krProcessManagementStageTypeSettings ?? new KrProcessManagementStageTypeSettingsTypeInfo();
  }

  private static krProcessManagementStageTypeSettings: KrProcessManagementStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrRegistration": {09fdd6a3-3946-4f30-9ef9-f533fad3a4a2}.
   */
  static get KrRegistration(): KrRegistrationTypeInfo {
    return TypeInfo.krRegistration = TypeInfo.krRegistration ?? new KrRegistrationTypeInfo();
  }

  private static krRegistration: KrRegistrationTypeInfo;

  /**
   * Card type identifier for "$KrActions_Registration": {bf4641ad-f4dc-4a75-83f4-534cba8bf225}.
   */
  static get KrRegistrationAction(): KrRegistrationActionTypeInfo {
    return TypeInfo.krRegistrationAction = TypeInfo.krRegistrationAction ?? new KrRegistrationActionTypeInfo();
  }

  private static krRegistrationAction: KrRegistrationActionTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {d92e4659-a66b-4efa-aa29-716953da636a}.
   */
  static get KrRegistrationStageTypeSettings(): KrRegistrationStageTypeSettingsTypeInfo {
    return TypeInfo.krRegistrationStageTypeSettings = TypeInfo.krRegistrationStageTypeSettings ?? new KrRegistrationStageTypeSettingsTypeInfo();
  }

  private static krRegistrationStageTypeSettings: KrRegistrationStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrRequestComment": {f0360d95-4f88-4809-b926-57b34a2f69f5}.
   */
  static get KrRequestComment(): KrRequestCommentTypeInfo {
    return TypeInfo.krRequestComment = TypeInfo.krRequestComment ?? new KrRequestCommentTypeInfo();
  }

  private static krRequestComment: KrRequestCommentTypeInfo;

  /**
   * Card type identifier for "$KrActions_Resolution": {235e42ea-7ad8-4321-9a3a-91b752985ef0}.
   */
  static get KrResolutionAction(): KrResolutionActionTypeInfo {
    return TypeInfo.krResolutionAction = TypeInfo.krResolutionAction ?? new KrResolutionActionTypeInfo();
  }

  private static krResolutionAction: KrResolutionActionTypeInfo;

  /**
   * Card type identifier for "$KrStages_Resolution": {c898080f-0fa7-45d9-bbc9-f28dfd2c8f1c}.
   */
  static get KrResolutionStageTypeSettings(): KrResolutionStageTypeSettingsTypeInfo {
    return TypeInfo.krResolutionStageTypeSettings = TypeInfo.krResolutionStageTypeSettings ?? new KrResolutionStageTypeSettingsTypeInfo();
  }

  private static krResolutionStageTypeSettings: KrResolutionStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$KrActions_RouteInitialization": {25ca876a-50b2-4c27-b847-56d4fc597934}.
   */
  static get KrRouteInitializationAction(): KrRouteInitializationActionTypeInfo {
    return TypeInfo.krRouteInitializationAction = TypeInfo.krRouteInitializationAction ?? new KrRouteInitializationActionTypeInfo();
  }

  private static krRouteInitializationAction: KrRouteInitializationActionTypeInfo;

  /**
   * Card type identifier for "An example of card type used to extend access rules in standard solution": {3e00b804-7905-484e-a50b-191ad1a118a2}.
   */
  static get KrSamplePermissionsExtensionType(): KrSamplePermissionsExtensionTypeTypeInfo {
    return TypeInfo.krSamplePermissionsExtensionType = TypeInfo.krSamplePermissionsExtensionType ?? new KrSamplePermissionsExtensionTypeTypeInfo();
  }

  private static krSamplePermissionsExtensionType: KrSamplePermissionsExtensionTypeTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrSatellite": {4115f07e-0aaa-4563-a749-0450c1a850af}.
   */
  static get KrSatellite(): KrSatelliteTypeInfo {
    return TypeInfo.krSatellite = TypeInfo.krSatellite ?? new KrSatelliteTypeInfo();
  }

  private static krSatellite: KrSatelliteTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrSecondaryProcess": {61420fa1-cc1f-47cb-b0bb-4ea8ee77f51a}.
   */
  static get KrSecondaryProcess(): KrSecondaryProcessTypeInfo {
    return TypeInfo.krSecondaryProcess = TypeInfo.krSecondaryProcess ?? new KrSecondaryProcessTypeInfo();
  }

  private static krSecondaryProcess: KrSecondaryProcessTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrSecondarySatellite": {7593c144-31f7-43c2-9c4b-e3b776562f8f}.
   */
  static get KrSecondarySatellite(): KrSecondarySatelliteTypeInfo {
    return TypeInfo.krSecondarySatellite = TypeInfo.krSecondarySatellite ?? new KrSecondarySatelliteTypeInfo();
  }

  private static krSecondarySatellite: KrSecondarySatelliteTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrSettings": {35a03878-57b6-4263-ae36-92eb59032132}.
   */
  static get KrSettings(): KrSettingsTypeInfo {
    return TypeInfo.krSettings = TypeInfo.krSettings ?? new KrSettingsTypeInfo();
  }

  private static krSettings: KrSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ShowDialog": {5309ce42-c4d2-4e99-a733-697c589311e7}.
   */
  static get KrShowDialog(): KrShowDialogTypeInfo {
    return TypeInfo.krShowDialog = TypeInfo.krShowDialog ?? new KrShowDialogTypeInfo();
  }

  private static krShowDialog: KrShowDialogTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrSigning": {968d68b3-a7c5-4b5d-bfa4-bb0f346880b6}.
   */
  static get KrSigning(): KrSigningTypeInfo {
    return TypeInfo.krSigning = TypeInfo.krSigning ?? new KrSigningTypeInfo();
  }

  private static krSigning: KrSigningTypeInfo;

  /**
   * Card type identifier for "$KrActions_Signing": {01762690-a192-4e8e-9b5e-0110666fd977}.
   */
  static get KrSigningAction(): KrSigningActionTypeInfo {
    return TypeInfo.krSigningAction = TypeInfo.krSigningAction ?? new KrSigningActionTypeInfo();
  }

  private static krSigningAction: KrSigningActionTypeInfo;

  /**
   * Card type identifier for "$KrStages_Signing": {5c473877-1e54-495c-8eca-74885d292786}.
   */
  static get KrSigningStageTypeSettings(): KrSigningStageTypeSettingsTypeInfo {
    return TypeInfo.krSigningStageTypeSettings = TypeInfo.krSigningStageTypeSettings ?? new KrSigningStageTypeSettingsTypeInfo();
  }

  private static krSigningStageTypeSettings: KrSigningStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrCommonMethod": {66cd517b-5423-43db-8374-f50ec0d967eb}.
   */
  static get KrStageCommonMethod(): KrStageCommonMethodTypeInfo {
    return TypeInfo.krStageCommonMethod = TypeInfo.krStageCommonMethod ?? new KrStageCommonMethodTypeInfo();
  }

  private static krStageCommonMethod: KrStageCommonMethodTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrStageGroup": {9ce8e9f4-cbf0-4b5f-a569-b508b1fd4b3a}.
   */
  static get KrStageGroup(): KrStageGroupTypeInfo {
    return TypeInfo.krStageGroup = TypeInfo.krStageGroup ?? new KrStageGroupTypeInfo();
  }

  private static krStageGroup: KrStageGroupTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrStageTemplate": {2fa85bb3-bba4-4ab6-ba97-652106db96de}.
   */
  static get KrStageTemplate(): KrStageTemplateTypeInfo {
    return TypeInfo.krStageTemplate = TypeInfo.krStageTemplate ?? new KrStageTemplateTypeInfo();
  }

  private static krStageTemplate: KrStageTemplateTypeInfo;

  /**
   * Card type identifier for "KrStateExtension": {cca319ab-ed7a-4715-9b56-d0d5bd9d41ab}.
   */
  static get KrStateExtension(): KrStateExtensionTypeInfo {
    return TypeInfo.krStateExtension = TypeInfo.krStateExtension ?? new KrStateExtensionTypeInfo();
  }

  private static krStateExtension: KrStateExtensionTypeInfo;

  /**
   * Card type identifier for "KrTaskKindSettings": {f1e87ca5-e1f1-4e5a-acbf-d6cfb20bcb11}.
   */
  static get KrTaskKindSettings(): KrTaskKindSettingsTypeInfo {
    return TypeInfo.krTaskKindSettings = TypeInfo.krTaskKindSettings ?? new KrTaskKindSettingsTypeInfo();
  }

  private static krTaskKindSettings: KrTaskKindSettingsTypeInfo;

  /**
   * Card type identifier for "$KrActions_TaskRegistration": {2d6cbf60-1c5a-40fd-a091-fa42bd4441bc}.
   */
  static get KrTaskRegistrationAction(): KrTaskRegistrationActionTypeInfo {
    return TypeInfo.krTaskRegistrationAction = TypeInfo.krTaskRegistrationAction ?? new KrTaskRegistrationActionTypeInfo();
  }

  private static krTaskRegistrationAction: KrTaskRegistrationActionTypeInfo;

  /**
   * Card type identifier for "KrTemplateCard": {d3d3d2e1-a45e-40c5-8228-cd304fdf6f4d}.
   */
  static get KrTemplateCard(): KrTemplateCardTypeInfo {
    return TypeInfo.krTemplateCard = TypeInfo.krTemplateCard ?? new KrTemplateCardTypeInfo();
  }

  private static krTemplateCard: KrTemplateCardTypeInfo;

  /**
   * Card type identifier for "$KrStages_TypedTask": {d8e1c89c-12b2-44e5-9bd0-c6a01c49b1e9}.
   */
  static get KrTypedTaskStageTypeSettings(): KrTypedTaskStageTypeSettingsTypeInfo {
    return TypeInfo.krTypedTaskStageTypeSettings = TypeInfo.krTypedTaskStageTypeSettings ?? new KrTypedTaskStageTypeSettingsTypeInfo();
  }

  private static krTypedTaskStageTypeSettings: KrTypedTaskStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrUniversalTask": {9c6d9824-41d7-41e6-99f1-e19ea9e576c5}.
   */
  static get KrUniversalTask(): KrUniversalTaskTypeInfo {
    return TypeInfo.krUniversalTask = TypeInfo.krUniversalTask ?? new KrUniversalTaskTypeInfo();
  }

  private static krUniversalTask: KrUniversalTaskTypeInfo;

  /**
   * Card type identifier for "$KrActions_UniversalTask": {231eea47-db41-4ad4-8846-164da4ef4048}.
   */
  static get KrUniversalTaskAction(): KrUniversalTaskActionTypeInfo {
    return TypeInfo.krUniversalTaskAction = TypeInfo.krUniversalTaskAction ?? new KrUniversalTaskActionTypeInfo();
  }

  private static krUniversalTaskAction: KrUniversalTaskActionTypeInfo;

  /**
   * Card type identifier for "$KrStages_UniversalTask": {eada56ed-7d98-4e6e-9d9f-950d8aa42696}.
   */
  static get KrUniversalTaskStageTypeSettings(): KrUniversalTaskStageTypeSettingsTypeInfo {
    return TypeInfo.krUniversalTaskStageTypeSettings = TypeInfo.krUniversalTaskStageTypeSettings ?? new KrUniversalTaskStageTypeSettingsTypeInfo();
  }

  private static krUniversalTaskStageTypeSettings: KrUniversalTaskStageTypeSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrUserSettings": {793864e5-39e5-4d4f-af59-c3d7a9facca9}.
   */
  static get KrUserSettings(): KrUserSettingsTypeInfo {
    return TypeInfo.krUserSettings = TypeInfo.krUserSettings ?? new KrUserSettingsTypeInfo();
  }

  private static krUserSettings: KrUserSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_KrVirtualFile": {81250a95-5c1e-488c-a423-106e7f982c6b}.
   */
  static get KrVirtualFile(): KrVirtualFileTypeInfo {
    return TypeInfo.krVirtualFile = TypeInfo.krVirtualFile ?? new KrVirtualFileTypeInfo();
  }

  private static krVirtualFile: KrVirtualFileTypeInfo;

  /**
   * Card type identifier for "$Cards_Case": {e16a1f04-9ccd-4338-90b0-ff4d2f581208}.
   */
  static get LawCase(): LawCaseTypeInfo {
    return TypeInfo.lawCase = TypeInfo.lawCase ?? new LawCaseTypeInfo();
  }

  private static lawCase: LawCaseTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {03083069-cd54-4a82-a236-8c1ef9179832}.
   */
  static get LawPartnerSelectionDialog(): LawPartnerSelectionDialogTypeInfo {
    return TypeInfo.lawPartnerSelectionDialog = TypeInfo.lawPartnerSelectionDialog ?? new LawPartnerSelectionDialogTypeInfo();
  }

  private static lawPartnerSelectionDialog: LawPartnerSelectionDialogTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {ad5fae5a-d585-4aa2-8590-92ed11f49393}.
   */
  static get LawVirtualFile(): LawVirtualFileTypeInfo {
    return TypeInfo.lawVirtualFile = TypeInfo.lawVirtualFile ?? new LawVirtualFileTypeInfo();
  }

  private static lawVirtualFile: LawVirtualFileTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_License": {f9c7b09c-de09-46b5-ba35-e73c83ea52a7}.
   */
  static get License(): LicenseTypeInfo {
    return TypeInfo.license = TypeInfo.license ?? new LicenseTypeInfo();
  }

  private static license: LicenseTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {75c2f788-c389-46ea-8dcf-a62331752a7c}.
   */
  static get LocalizationDialogs(): LocalizationDialogsTypeInfo {
    return TypeInfo.localizationDialogs = TypeInfo.localizationDialogs ?? new LocalizationDialogsTypeInfo();
  }

  private static localizationDialogs: LocalizationDialogsTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {d4ed365b-5384-439f-ac44-9f71977e60d6}.
   */
  static get MetadataEditor(): MetadataEditorTypeInfo {
    return TypeInfo.metadataEditor = TypeInfo.metadataEditor ?? new MetadataEditorTypeInfo();
  }

  private static metadataEditor: MetadataEditorTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Metarole": {c6c9e585-c053-0aa0-994a-f80225f8585f}.
   */
  static get Metarole(): MetaroleTypeInfo {
    return TypeInfo.metarole = TypeInfo.metarole ?? new MetaroleTypeInfo();
  }

  private static metarole: MetaroleTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_NestedRole": {f33a4b0d-dbd8-4af7-a199-6802a77498bb}.
   */
  static get NestedRole(): NestedRoleTypeInfo {
    return TypeInfo.nestedRole = TypeInfo.nestedRole ?? new NestedRoleTypeInfo();
  }

  private static nestedRole: NestedRoleTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Notification": {d3087e3c-a2da-4cc7-a92d-d5cf17e48d3f}.
   */
  static get Notification(): NotificationTypeInfo {
    return TypeInfo.notification = TypeInfo.notification ?? new NotificationTypeInfo();
  }

  private static notification: NotificationTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_NotificationSubscriptions": {4d92f912-2907-4e3c-bce5-7767a0f98bf8}.
   */
  static get NotificationSubscriptions(): NotificationSubscriptionsTypeInfo {
    return TypeInfo.notificationSubscriptions = TypeInfo.notificationSubscriptions ?? new NotificationSubscriptionsTypeInfo();
  }

  private static notificationSubscriptions: NotificationSubscriptionsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_NotificationType": {813234a7-3b29-4845-be9d-b5bece5f7c1c}.
   */
  static get NotificationType(): NotificationTypeTypeInfo {
    return TypeInfo.notificationType = TypeInfo.notificationType ?? new NotificationTypeTypeInfo();
  }

  private static notificationType: NotificationTypeTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {193c278d-7178-4ee3-a73b-438357d69d2a}.
   */
  static get NotificationUserSettings(): NotificationUserSettingsTypeInfo {
    return TypeInfo.notificationUserSettings = TypeInfo.notificationUserSettings ?? new NotificationUserSettingsTypeInfo();
  }

  private static notificationUserSettings: NotificationUserSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_OcrOperation": {8275fa2d-91d6-462f-8a1a-189c9b57720e}.
   */
  static get OcrOperation(): OcrOperationTypeInfo {
    return TypeInfo.ocrOperation = TypeInfo.ocrOperation ?? new OcrOperationTypeInfo();
  }

  private static ocrOperation: OcrOperationTypeInfo;

  /**
   * Card type identifier for "$CardTypes_OcrRequestDialog": {51488d15-c19c-4855-8bec-7cc26936e9d6}.
   */
  static get OcrRequestDialog(): OcrRequestDialogTypeInfo {
    return TypeInfo.ocrRequestDialog = TypeInfo.ocrRequestDialog ?? new OcrRequestDialogTypeInfo();
  }

  private static ocrRequestDialog: OcrRequestDialogTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_OcrSettings": {b3c9c077-8f51-47fc-ba36-0247f71d6b0f}.
   */
  static get OcrSettings(): OcrSettingsTypeInfo {
    return TypeInfo.ocrSettings = TypeInfo.ocrSettings ?? new OcrSettingsTypeInfo();
  }

  private static ocrSettings: OcrSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_OnlyOffice": {4d17a80b-c711-4dce-8552-c9ddb30dd7ce}.
   */
  static get OnlyOfficeSettings(): OnlyOfficeSettingsTypeInfo {
    return TypeInfo.onlyOfficeSettings = TypeInfo.onlyOfficeSettings ?? new OnlyOfficeSettingsTypeInfo();
  }

  private static onlyOfficeSettings: OnlyOfficeSettingsTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {57493ed6-316b-4fb9-b85e-2b931e022640}.
   */
  static get OpenInModalDialogSettings(): OpenInModalDialogSettingsTypeInfo {
    return TypeInfo.openInModalDialogSettings = TypeInfo.openInModalDialogSettings ?? new OpenInModalDialogSettingsTypeInfo();
  }

  private static openInModalDialogSettings: OpenInModalDialogSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Operation": {f2517b14-035b-4451-b4a1-605bf7a764a4}.
   */
  static get Operation(): OperationTypeInfo {
    return TypeInfo.operation = TypeInfo.operation ?? new OperationTypeInfo();
  }

  private static operation: OperationTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Outgoing": {c59b76d9-c0db-01cd-a3fb-b339740f0620}.
   */
  static get Outgoing(): OutgoingTypeInfo {
    return TypeInfo.outgoing = TypeInfo.outgoing ?? new OutgoingTypeInfo();
  }

  private static outgoing: OutgoingTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Partner": {b9a1f125-ab1d-4cff-929f-5ad8351bda4f}.
   */
  static get Partner(): PartnerTypeInfo {
    return TypeInfo.partner = TypeInfo.partner ?? new PartnerTypeInfo();
  }

  private static partner: PartnerTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_PartnerCondition": {57c6e535-ad4b-4e03-8c06-2d3fbc59e1b8}.
   */
  static get PartnerCondition(): PartnerConditionTypeInfo {
    return TypeInfo.partnerCondition = TypeInfo.partnerCondition ?? new PartnerConditionTypeInfo();
  }

  private static partnerCondition: PartnerConditionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_Tabs_Personalization": {3bd67bff-6990-4026-aa7f-b789ba8a8744}.
   */
  static get PersonalizationUserSettings(): PersonalizationUserSettingsTypeInfo {
    return TypeInfo.personalizationUserSettings = TypeInfo.personalizationUserSettings ?? new PersonalizationUserSettingsTypeInfo();
  }

  private static personalizationUserSettings: PersonalizationUserSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_PersonalRole": {929ad23c-8a22-09aa-9000-398bf13979b2}.
   */
  static get PersonalRole(): PersonalRoleTypeInfo {
    return TypeInfo.personalRole = TypeInfo.personalRole ?? new PersonalRoleTypeInfo();
  }

  private static personalRole: PersonalRoleTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_PersonalRoleSatellite": {f6c54fed-0bee-4d61-980a-8057179289ea}.
   */
  static get PersonalRoleSatellite(): PersonalRoleSatelliteTypeInfo {
    return TypeInfo.personalRoleSatellite = TypeInfo.personalRoleSatellite ?? new PersonalRoleSatelliteTypeInfo();
  }

  private static personalRoleSatellite: PersonalRoleSatelliteTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Protocol": {4d9f9590-0131-4d32-9710-5e07c282b5d3}.
   */
  static get Protocol(): ProtocolTypeInfo {
    return TypeInfo.protocol = TypeInfo.protocol ?? new ProtocolTypeInfo();
  }

  private static protocol: ProtocolTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ReportPermissions": {65a88390-3b00-4b74-925d-b635027feff2}.
   */
  static get ReportPermissions(): ReportPermissionsTypeInfo {
    return TypeInfo.reportPermissions = TypeInfo.reportPermissions ?? new ReportPermissionsTypeInfo();
  }

  private static reportPermissions: ReportPermissionsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_RoleDeputiesManagement": {cb931209-2ad9-4370-bb3c-3172e61937ba}.
   */
  static get RoleDeputiesManagement(): RoleDeputiesManagementTypeInfo {
    return TypeInfo.roleDeputiesManagement = TypeInfo.roleDeputiesManagement ?? new RoleDeputiesManagementTypeInfo();
  }

  private static roleDeputiesManagement: RoleDeputiesManagementTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_RoleGenerator": {890379d8-651e-01d9-85c7-12644b5364b8}.
   */
  static get RoleGenerator(): RoleGeneratorTypeInfo {
    return TypeInfo.roleGenerator = TypeInfo.roleGenerator ?? new RoleGeneratorTypeInfo();
  }

  private static roleGenerator: RoleGeneratorTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_RouteCondition": {94a948b6-9cea-4a4a-9005-b0316d89ed6e}.
   */
  static get RouteCondition(): RouteConditionTypeInfo {
    return TypeInfo.routeCondition = TypeInfo.routeCondition ?? new RouteConditionTypeInfo();
  }

  private static routeCondition: RouteConditionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_RowAddedCondition": {ad464749-98a9-480f-a59b-41ef203d00ce}.
   */
  static get RowAddedCondition(): RowAddedConditionTypeInfo {
    return TypeInfo.rowAddedCondition = TypeInfo.rowAddedCondition ?? new RowAddedConditionTypeInfo();
  }

  private static rowAddedCondition: RowAddedConditionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_RowDeletedCondition": {9d705cf7-e108-449d-a6f8-92512eb8d4ea}.
   */
  static get RowDeletedCondition(): RowDeletedConditionTypeInfo {
    return TypeInfo.rowDeletedCondition = TypeInfo.rowDeletedCondition ?? new RowDeletedConditionTypeInfo();
  }

  private static rowDeletedCondition: RowDeletedConditionTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {b9bdea71-5b6f-494b-a878-afec21add8d0}.
   */
  static get SelectTag(): SelectTagTypeInfo {
    return TypeInfo.selectTag = TypeInfo.selectTag ?? new SelectTagTypeInfo();
  }

  private static selectTag: SelectTagTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Sequence": {81e7e5a2-4745-4ccc-8178-547663d47737}.
   */
  static get Sequence(): SequenceTypeInfo {
    return TypeInfo.sequence = TypeInfo.sequence ?? new SequenceTypeInfo();
  }

  private static sequence: SequenceTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ServerInstance": {7b891314-474f-4a60-8e0d-744dcb075209}.
   */
  static get ServerInstance(): ServerInstanceTypeInfo {
    return TypeInfo.serverInstance = TypeInfo.serverInstance ?? new ServerInstanceTypeInfo();
  }

  private static serverInstance: ServerInstanceTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ShowDialog": {706e6173-f55d-4fe8-9cbe-aaeb2964ba80}.
   */
  static get ShowDialog(): ShowDialogTypeInfo {
    return TypeInfo.showDialog = TypeInfo.showDialog ?? new ShowDialogTypeInfo();
  }

  private static showDialog: ShowDialogTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_SignatureSetting": {07257ec6-43cb-4425-9fcb-893a6c46f20d}.
   */
  static get SignatureSettings(): SignatureSettingsTypeInfo {
    return TypeInfo.signatureSettings = TypeInfo.signatureSettings ?? new SignatureSettingsTypeInfo();
  }

  private static signatureSettings: SignatureSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_SmartRole": {ff6a7318-11d6-4b9d-8018-80498c50566c}.
   */
  static get SmartRole(): SmartRoleTypeInfo {
    return TypeInfo.smartRole = TypeInfo.smartRole ?? new SmartRoleTypeInfo();
  }

  private static smartRole: SmartRoleTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_SmartRoleGenerator": {c72e05fb-7eef-4256-9029-72f821f4f79e}.
   */
  static get SmartRoleGenerator(): SmartRoleGeneratorTypeInfo {
    return TypeInfo.smartRoleGenerator = TypeInfo.smartRoleGenerator ?? new SmartRoleGeneratorTypeInfo();
  }

  private static smartRoleGenerator: SmartRoleGeneratorTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_StaticRole": {825dbacc-ddec-00d1-a550-2a837792542e}.
   */
  static get StaticRole(): StaticRoleTypeInfo {
    return TypeInfo.staticRole = TypeInfo.staticRole ?? new StaticRoleTypeInfo();
  }

  private static staticRole: StaticRoleTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Tag": {0e459a72-4d99-43bf-8ea5-8cf7cc911382}.
   */
  static get Tag(): TagTypeInfo {
    return TypeInfo.tag = TypeInfo.tag ?? new TagTypeInfo();
  }

  private static tag: TagTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TagCondition": {237be0a6-2cf5-4ea6-9074-f31cce11eb58}.
   */
  static get TagCondition(): TagConditionTypeInfo {
    return TypeInfo.tagCondition = TypeInfo.tagCondition ?? new TagConditionTypeInfo();
  }

  private static tagCondition: TagConditionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_CardNames_TagsSettings": {7101497d-9057-43d9-99c6-1d425eadf3bd}.
   */
  static get TagsUserSettings(): TagsUserSettingsTypeInfo {
    return TypeInfo.tagsUserSettings = TypeInfo.tagsUserSettings ?? new TagsUserSettingsTypeInfo();
  }

  private static tagsUserSettings: TagsUserSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_NewTaskAssignedRoleDialog": {5822a6de-95e6-4519-9045-173be057eb90}.
   */
  static get TaskAssignedRoleEditor(): TaskAssignedRoleEditorTypeInfo {
    return TypeInfo.taskAssignedRoleEditor = TypeInfo.taskAssignedRoleEditor ?? new TaskAssignedRoleEditorTypeInfo();
  }

  private static taskAssignedRoleEditor: TaskAssignedRoleEditorTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TaskAssignedRolesDialog": {18f5d3ef-087a-4938-be67-4deb0b6e08b2}.
   */
  static get TaskAssignedRoles(): TaskAssignedRolesTypeInfo {
    return TypeInfo.taskAssignedRoles = TypeInfo.taskAssignedRoles ?? new TaskAssignedRolesTypeInfo();
  }

  private static taskAssignedRoles: TaskAssignedRolesTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TaskChangedCondition": {5779afe8-1fd4-4585-8c63-bf6043996250}.
   */
  static get TaskChangedCondition(): TaskChangedConditionTypeInfo {
    return TypeInfo.taskChangedCondition = TypeInfo.taskChangedCondition ?? new TaskChangedConditionTypeInfo();
  }

  private static taskChangedCondition: TaskChangedConditionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TaskHistoryGroupType": {5f25db65-1291-40c4-a1de-7673c2be448f}.
   */
  static get TaskHistoryGroupType(): TaskHistoryGroupTypeTypeInfo {
    return TypeInfo.taskHistoryGroupType = TypeInfo.taskHistoryGroupType ?? new TaskHistoryGroupTypeTypeInfo();
  }

  private static taskHistoryGroupType: TaskHistoryGroupTypeTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TaskKind": {2f41698a-3823-48c9-9476-f756f090eb11}.
   */
  static get TaskKind(): TaskKindTypeInfo {
    return TypeInfo.taskKind = TypeInfo.taskKind ?? new TaskKindTypeInfo();
  }

  private static taskKind: TaskKindTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TaskRole": {e97c253c-9102-0440-ac7e-4876e8f789da}.
   */
  static get TaskRole(): TaskRoleTypeInfo {
    return TypeInfo.taskRole = TypeInfo.taskRole ?? new TaskRoleTypeInfo();
  }

  private static taskRole: TaskRoleTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TaskTypeCondition": {c463100c-8a7b-4c31-a23f-743d8b3eb29f}.
   */
  static get TaskTypeCondition(): TaskTypeConditionTypeInfo {
    return TypeInfo.taskTypeCondition = TypeInfo.taskTypeCondition ?? new TaskTypeConditionTypeInfo();
  }

  private static taskTypeCondition: TaskTypeConditionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Blocks_Tabs_Template": {7ed2fb6d-4ece-458f-9151-0c72995c2d19}.
   */
  static get Template(): TemplateTypeInfo {
    return TypeInfo.template = TypeInfo.template ?? new TemplateTypeInfo();
  }

  private static template: TemplateTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TemplateFile": {a259101b-58f7-47b4-959e-dd5e7be1671c}.
   */
  static get TemplateFile(): TemplateFileTypeInfo {
    return TypeInfo.templateFile = TypeInfo.templateFile ?? new TemplateFileTypeInfo();
  }

  private static templateFile: TemplateFileTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TestTask1": {929e345c-acdf-41ea-acb6-6bb308de73ae}.
   */
  static get TestTask1(): TestTask1TypeInfo {
    return TypeInfo.testTask1 = TypeInfo.testTask1 ?? new TestTask1TypeInfo();
  }

  private static testTask1: TestTask1TypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TestTask2": {5239e1b6-1ed6-4a3f-a11e-7e4c6e187af6}.
   */
  static get TestTask2(): TestTask2TypeInfo {
    return TypeInfo.testTask2 = TypeInfo.testTask2 ?? new TestTask2TypeInfo();
  }

  private static testTask2: TestTask2TypeInfo;

  /**
   * Card type identifier for "$CardTypes_Blocks_TilePanels": {d3e5a259-e7e6-49f2-a7dd-1fc7b051781b}.
   */
  static get TileUserSettings(): TileUserSettingsTypeInfo {
    return TypeInfo.tileUserSettings = TypeInfo.tileUserSettings ?? new TileUserSettingsTypeInfo();
  }

  private static tileUserSettings: TileUserSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Tabs_TimeZones": {cc60e8c6-af29-4ad5-a55d-3e6ee985ceb2}.
   */
  static get TimeZones(): TimeZonesTypeInfo {
    return TypeInfo.timeZones = TypeInfo.timeZones ?? new TimeZonesTypeInfo();
  }

  private static timeZones: TimeZonesTypeInfo;

  /**
   * Card type identifier for "TopicDialogs": {e8c0c3a0-80cb-4f92-9fee-d38f1dddacd2}.
   */
  static get TopicDialogs(): TopicDialogsTypeInfo {
    return TypeInfo.topicDialogs = TypeInfo.topicDialogs ?? new TopicDialogsTypeInfo();
  }

  private static topicDialogs: TopicDialogsTypeInfo;

  /**
   * Card type identifier for "TopicTabs": {aed972eb-883d-4b02-a75b-c96d4a5aef4c}.
   */
  static get TopicTabs(): TopicTabsTypeInfo {
    return TypeInfo.topicTabs = TypeInfo.topicTabs ?? new TopicTabsTypeInfo();
  }

  private static topicTabs: TopicTabsTypeInfo;

  /**
   * Card type identifier for "Used by the system. Do not modify it.": {7e6a9f59-0889-4b4d-8989-9f0c70c39f90}.
   */
  static get UserSettingsSystemType(): UserSettingsSystemTypeTypeInfo {
    return TypeInfo.userSettingsSystemType = TypeInfo.userSettingsSystemType ?? new UserSettingsSystemTypeTypeInfo();
  }

  private static userSettingsSystemType: UserSettingsSystemTypeTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_View": {635bbe7b-9c2e-4fde-87c2-9deefaad7981}.
   */
  static get View(): ViewTypeInfo {
    return TypeInfo.view = TypeInfo.view ?? new ViewTypeInfo();
  }

  private static view: ViewTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {5d12d3d7-c5df-4983-9578-f065d2b74768}.
   */
  static get ViewExtensions(): ViewExtensionsTypeInfo {
    return TypeInfo.viewExtensions = TypeInfo.viewExtensions ?? new ViewExtensionsTypeInfo();
  }

  private static viewExtensions: ViewExtensionsTypeInfo;

  /**
   * Card type identifier for "$Cards_DefaultCaption": {b2b4d2c2-8f92-4262-9951-fe1a64bf9b30}.
   */
  static get VirtualScheme(): VirtualSchemeTypeInfo {
    return TypeInfo.virtualScheme = TypeInfo.virtualScheme ?? new VirtualSchemeTypeInfo();
  }

  private static virtualScheme: VirtualSchemeTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_WebApplication": {104076c7-3cd3-4b4e-9344-e17f24c88ee8}.
   */
  static get WebApplication(): WebApplicationTypeInfo {
    return TypeInfo.webApplication = TypeInfo.webApplication ?? new WebApplicationTypeInfo();
  }

  private static webApplication: WebApplicationTypeInfo;

  /**
   * Card type identifier for "$CardTypes_Blocks_WebClientUserSettings": {7104ac73-bf0a-4dba-be9e-21f7148c8c82}.
   */
  static get WebClientUserSettings(): WebClientUserSettingsTypeInfo {
    return TypeInfo.webClientUserSettings = TypeInfo.webClientUserSettings ?? new WebClientUserSettingsTypeInfo();
  }

  private static webClientUserSettings: WebClientUserSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_WfResolution": {928132fe-202d-4f9f-8ec5-5093ea2122d1}.
   */
  static get WfResolution(): WfResolutionTypeInfo {
    return TypeInfo.wfResolution = TypeInfo.wfResolution ?? new WfResolutionTypeInfo();
  }

  private static wfResolution: WfResolutionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_WfResolutionChild": {539ecfe8-5fb6-4681-8aa8-1ee4d9ef1dda}.
   */
  static get WfResolutionChild(): WfResolutionChildTypeInfo {
    return TypeInfo.wfResolutionChild = TypeInfo.wfResolutionChild ?? new WfResolutionChildTypeInfo();
  }

  private static wfResolutionChild: WfResolutionChildTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_WfResolutionControl": {85a5e8d7-a901-46df-9173-4d9a043ce6d3}.
   */
  static get WfResolutionControl(): WfResolutionControlTypeInfo {
    return TypeInfo.wfResolutionControl = TypeInfo.wfResolutionControl ?? new WfResolutionControlTypeInfo();
  }

  private static wfResolutionControl: WfResolutionControlTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_WfResolutionProject": {c989d91f-7ddd-455c-ae16-3bb380132ba8}.
   */
  static get WfResolutionProject(): WfResolutionProjectTypeInfo {
    return TypeInfo.wfResolutionProject = TypeInfo.wfResolutionProject ?? new WfResolutionProjectTypeInfo();
  }

  private static wfResolutionProject: WfResolutionProjectTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_WfSatellite": {a382ec40-6321-42e5-a9f9-c7b103feb38d}.
   */
  static get WfSatellite(): WfSatelliteTypeInfo {
    return TypeInfo.wfSatellite = TypeInfo.wfSatellite ?? new WfSatelliteTypeInfo();
  }

  private static wfSatellite: WfSatelliteTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_WfTaskCard": {de75a343-8164-472d-a20e-4937819760ac}.
   */
  static get WfTaskCard(): WfTaskCardTypeInfo {
    return TypeInfo.wfTaskCard = TypeInfo.wfTaskCard ?? new WfTaskCardTypeInfo();
  }

  private static wfTaskCard: WfTaskCardTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ActionTemplate": {d0960012-d05a-4c86-9e45-ab467231d11d}.
   */
  static get WorkflowActionEditor(): WorkflowActionEditorTypeInfo {
    return TypeInfo.workflowActionEditor = TypeInfo.workflowActionEditor ?? new WorkflowActionEditorTypeInfo();
  }

  private static workflowActionEditor: WorkflowActionEditorTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ActionInstance": {f9e6b610-60cf-4e6a-bcfc-40ccff3be8db}.
   */
  static get WorkflowActionInstanceEditor(): WorkflowActionInstanceEditorTypeInfo {
    return TypeInfo.workflowActionInstanceEditor = TypeInfo.workflowActionInstanceEditor ?? new WorkflowActionInstanceEditorTypeInfo();
  }

  private static workflowActionInstanceEditor: WorkflowActionInstanceEditorTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_AddFileFromTemplate": {8b0a3173-b495-4978-b445-23a0e79eda25}.
   */
  static get WorkflowAddFileFromTemplateAction(): WorkflowAddFileFromTemplateActionTypeInfo {
    return TypeInfo.workflowAddFileFromTemplateAction = TypeInfo.workflowAddFileFromTemplateAction ?? new WorkflowAddFileFromTemplateActionTypeInfo();
  }

  private static workflowAddFileFromTemplateAction: WorkflowAddFileFromTemplateActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_And": {575959db-4dd4-4b82-bbe9-445733b8e7dd}.
   */
  static get WorkflowAndAction(): WorkflowAndActionTypeInfo {
    return TypeInfo.workflowAndAction = TypeInfo.workflowAndAction ?? new WorkflowAndActionTypeInfo();
  }

  private static workflowAndAction: WorkflowAndActionTypeInfo;

  /**
   * Card type identifier for "$WorkflowEngine_TilePermission_CheckRolesForExecution": {0295bfb2-acf1-415b-aa6d-b272d8ebc3fe}.
   */
  static get WorkflowCheckRolesForExecutionTileExtension(): WorkflowCheckRolesForExecutionTileExtensionTypeInfo {
    return TypeInfo.workflowCheckRolesForExecutionTileExtension = TypeInfo.workflowCheckRolesForExecutionTileExtension ?? new WorkflowCheckRolesForExecutionTileExtensionTypeInfo();
  }

  private static workflowCheckRolesForExecutionTileExtension: WorkflowCheckRolesForExecutionTileExtensionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_WaitingForSignal": {91b1cd51-da7a-4721-bb80-f5908fc936a2}.
   */
  static get WorkflowCommandAction(): WorkflowCommandActionTypeInfo {
    return TypeInfo.workflowCommandAction = TypeInfo.workflowCommandAction ?? new WorkflowCommandActionTypeInfo();
  }

  private static workflowCommandAction: WorkflowCommandActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Condition": {eb222506-6f7d-4c22-b3d2-d98a2f390ac5}.
   */
  static get WorkflowConditionAction(): WorkflowConditionActionTypeInfo {
    return TypeInfo.workflowConditionAction = TypeInfo.workflowConditionAction ?? new WorkflowConditionActionTypeInfo();
  }

  private static workflowConditionAction: WorkflowConditionActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_CreateCard": {1c7a8067-cd49-45ab-a3c9-04a0cfe26c43}.
   */
  static get WorkflowCreateCardAction(): WorkflowCreateCardActionTypeInfo {
    return TypeInfo.workflowCreateCardAction = TypeInfo.workflowCreateCardAction ?? new WorkflowCreateCardActionTypeInfo();
  }

  private static workflowCreateCardAction: WorkflowCreateCardActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Dialog": {fc004cdf-e9ed-4549-9f35-781cbc779af2}.
   */
  static get WorkflowDialogAction(): WorkflowDialogActionTypeInfo {
    return TypeInfo.workflowDialogAction = TypeInfo.workflowDialogAction ?? new WorkflowDialogActionTypeInfo();
  }

  private static workflowDialogAction: WorkflowDialogActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_EndProcess": {86913540-9f30-4a6b-b0d5-a24a674b2ef2}.
   */
  static get WorkflowEndAction(): WorkflowEndActionTypeInfo {
    return TypeInfo.workflowEndAction = TypeInfo.workflowEndAction ?? new WorkflowEndActionTypeInfo();
  }

  private static workflowEndAction: WorkflowEndActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_WorkflowEngineSettings": {a73da16b-d69b-4957-a2de-b35adebcd85e}.
   */
  static get WorkflowEngineSettings(): WorkflowEngineSettingsTypeInfo {
    return TypeInfo.workflowEngineSettings = TypeInfo.workflowEngineSettings ?? new WorkflowEngineSettingsTypeInfo();
  }

  private static workflowEngineSettings: WorkflowEngineSettingsTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_HistoryManagementAction": {4e397642-1672-4598-8851-7ed18378c915}.
   */
  static get WorkflowHistoryManagementAction(): WorkflowHistoryManagementActionTypeInfo {
    return TypeInfo.workflowHistoryManagementAction = TypeInfo.workflowHistoryManagementAction ?? new WorkflowHistoryManagementActionTypeInfo();
  }

  private static workflowHistoryManagementAction: WorkflowHistoryManagementActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Link": {eb4d7730-6104-4f37-9f63-0c0fa8baf3d1}.
   */
  static get WorkflowLinkEditor(): WorkflowLinkEditorTypeInfo {
    return TypeInfo.workflowLinkEditor = TypeInfo.workflowLinkEditor ?? new WorkflowLinkEditorTypeInfo();
  }

  private static workflowLinkEditor: WorkflowLinkEditorTypeInfo;

  /**
   * Card type identifier for "Редатор элементов на левой панели (пока не используется)": {798aafa5-4bfc-4c16-9e6f-9a761e83d6cc}.
   */
  static get WorkflowNewItemEditor(): WorkflowNewItemEditorTypeInfo {
    return TypeInfo.workflowNewItemEditor = TypeInfo.workflowNewItemEditor ?? new WorkflowNewItemEditorTypeInfo();
  }

  private static workflowNewItemEditor: WorkflowNewItemEditorTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_NodeTemplate": {f14d88f8-5e3d-4609-b377-0ad9f647d87a}.
   */
  static get WorkflowNodeEditor(): WorkflowNodeEditorTypeInfo {
    return TypeInfo.workflowNodeEditor = TypeInfo.workflowNodeEditor ?? new WorkflowNodeEditorTypeInfo();
  }

  private static workflowNodeEditor: WorkflowNodeEditorTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_NodeInstance": {34d4cdae-de49-471d-af77-ab9455bfb8cd}.
   */
  static get WorkflowNodeInstanceEditor(): WorkflowNodeInstanceEditorTypeInfo {
    return TypeInfo.workflowNodeInstanceEditor = TypeInfo.workflowNodeInstanceEditor ?? new WorkflowNodeInstanceEditorTypeInfo();
  }

  private static workflowNodeInstanceEditor: WorkflowNodeInstanceEditorTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_NotificationAction": {d5e2b20d-46c6-4f23-8ede-820b5a8381a3}.
   */
  static get WorkflowNotificationAction(): WorkflowNotificationActionTypeInfo {
    return TypeInfo.workflowNotificationAction = TypeInfo.workflowNotificationAction ?? new WorkflowNotificationActionTypeInfo();
  }

  private static workflowNotificationAction: WorkflowNotificationActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ProcessInstance": {bb3e1452-30da-4eb2-a4fe-10871608bed3}.
   */
  static get WorkflowProcess(): WorkflowProcessTypeInfo {
    return TypeInfo.workflowProcess = TypeInfo.workflowProcess ?? new WorkflowProcessTypeInfo();
  }

  private static workflowProcess: WorkflowProcessTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ProcessTemplate": {d7420bf0-ce4e-4155-9b97-0117b9d1e13c}.
   */
  static get WorkflowProcessEditor(): WorkflowProcessEditorTypeInfo {
    return TypeInfo.workflowProcessEditor = TypeInfo.workflowProcessEditor ?? new WorkflowProcessEditorTypeInfo();
  }

  private static workflowProcessEditor: WorkflowProcessEditorTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_ProcessInstance": {584680fa-e289-42dc-8c88-91ece7440316}.
   */
  static get WorkflowProcessInstanceEditor(): WorkflowProcessInstanceEditorTypeInfo {
    return TypeInfo.workflowProcessInstanceEditor = TypeInfo.workflowProcessInstanceEditor ?? new WorkflowProcessInstanceEditorTypeInfo();
  }

  private static workflowProcessInstanceEditor: WorkflowProcessInstanceEditorTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Scenario": {a7179d00-8641-4bde-b1ce-0f97053fe1e2}.
   */
  static get WorkflowScenarioAction(): WorkflowScenarioActionTypeInfo {
    return TypeInfo.workflowScenarioAction = TypeInfo.workflowScenarioAction ?? new WorkflowScenarioActionTypeInfo();
  }

  private static workflowScenarioAction: WorkflowScenarioActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_SendSignal": {3e2ea605-b60d-42e4-ace9-97d2f343a778}.
   */
  static get WorkflowSendSignalAction(): WorkflowSendSignalActionTypeInfo {
    return TypeInfo.workflowSendSignalAction = TypeInfo.workflowSendSignalAction ?? new WorkflowSendSignalActionTypeInfo();
  }

  private static workflowSendSignalAction: WorkflowSendSignalActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_StartProcess": {a4f7fb59-cc66-4aba-b593-3f4d26698666}.
   */
  static get WorkflowStartAction(): WorkflowStartActionTypeInfo {
    return TypeInfo.workflowStartAction = TypeInfo.workflowStartAction ?? new WorkflowStartActionTypeInfo();
  }

  private static workflowStartAction: WorkflowStartActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Subprocess": {d1d3acfd-8a78-4009-ac38-4dd851c8176a}.
   */
  static get WorkflowSubprocessAction(): WorkflowSubprocessActionTypeInfo {
    return TypeInfo.workflowSubprocessAction = TypeInfo.workflowSubprocessAction ?? new WorkflowSubprocessActionTypeInfo();
  }

  private static workflowSubprocessAction: WorkflowSubprocessActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_SubprocessControl": {3dc924c7-a7c8-407c-a7c7-5d2d77e6e1bc}.
   */
  static get WorkflowSubprocessControlAction(): WorkflowSubprocessControlActionTypeInfo {
    return TypeInfo.workflowSubprocessControlAction = TypeInfo.workflowSubprocessControlAction ?? new WorkflowSubprocessControlActionTypeInfo();
  }

  private static workflowSubprocessControlAction: WorkflowSubprocessControlActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Task": {dc541752-496d-45eb-ad71-92ab186d7601}.
   */
  static get WorkflowTaskAction(): WorkflowTaskActionTypeInfo {
    return TypeInfo.workflowTaskAction = TypeInfo.workflowTaskAction ?? new WorkflowTaskActionTypeInfo();
  }

  private static workflowTaskAction: WorkflowTaskActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TaskControl": {c80a6abd-361d-4290-bca4-3df7a03bbc10}.
   */
  static get WorkflowTaskControlAction(): WorkflowTaskControlActionTypeInfo {
    return TypeInfo.workflowTaskControlAction = TypeInfo.workflowTaskControlAction ?? new WorkflowTaskControlActionTypeInfo();
  }

  private static workflowTaskControlAction: WorkflowTaskControlActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TaskGroup": {b0d0d053-4ef2-46bc-b3bf-5c701532760e}.
   */
  static get WorkflowTaskGroupAction(): WorkflowTaskGroupActionTypeInfo {
    return TypeInfo.workflowTaskGroupAction = TypeInfo.workflowTaskGroupAction ?? new WorkflowTaskGroupActionTypeInfo();
  }

  private static workflowTaskGroupAction: WorkflowTaskGroupActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TaskGroupControl": {ec60ba12-edd9-48d5-be49-6a2f4caf22fb}.
   */
  static get WorkflowTaskGroupControlAction(): WorkflowTaskGroupControlActionTypeInfo {
    return TypeInfo.workflowTaskGroupControlAction = TypeInfo.workflowTaskGroupControlAction ?? new WorkflowTaskGroupControlActionTypeInfo();
  }

  private static workflowTaskGroupControlAction: WorkflowTaskGroupControlActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Timer": {a2d50ba1-434b-406a-a31c-f86c821b6b20}.
   */
  static get WorkflowTimerAction(): WorkflowTimerActionTypeInfo {
    return TypeInfo.workflowTimerAction = TypeInfo.workflowTimerAction ?? new WorkflowTimerActionTypeInfo();
  }

  private static workflowTimerAction: WorkflowTimerActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_TimerControl": {fd97cf1e-a1c6-4b5f-9b0c-1b254735e17a}.
   */
  static get WorkflowTimerControlAction(): WorkflowTimerControlActionTypeInfo {
    return TypeInfo.workflowTimerControlAction = TypeInfo.workflowTimerControlAction ?? new WorkflowTimerControlActionTypeInfo();
  }

  private static workflowTimerControlAction: WorkflowTimerControlActionTypeInfo;

  /**
   * Card type identifier for "$CardTypes_TypesNames_Workplace": {12fd0e19-d751-4d8e-959d-78c9763bbb38}.
   */
  static get Workplace(): WorkplaceTypeInfo {
    return TypeInfo.workplace = TypeInfo.workplace ?? new WorkplaceTypeInfo();
  }

  private static workplace: WorkplaceTypeInfo;

  //#endregion
}