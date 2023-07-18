








using System;

namespace Tessa.Extensions.Shared.Info
{// ReSharper disable InconsistentNaming
    #region AccountUserSettings

    /// <summary>
    ///     ID: {48332157-4f6a-4cb1-ace6-4b811c0e5364}
    ///     Alias: AccountUserSettings
    ///     Caption: $CardTypes_Blocks_AccountSettings
    ///     Group: UserSettings
    /// </summary>
    public class AccountUserSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "AccountUserSettings": {48332157-4f6a-4cb1-ace6-4b811c0e5364}.
        /// </summary>
        public readonly Guid ID = new Guid(0x48332157,0x4f6a,0x4cb1,0xac,0xe6,0x4b,0x81,0x1c,0x0e,0x53,0x64);

        /// <summary>
        ///     Card type name for "AccountUserSettings".
        /// </summary>
        public readonly string Alias = "AccountUserSettings";

        /// <summary>
        ///     Card type caption for "AccountUserSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_Blocks_AccountSettings";

        /// <summary>
        ///     Card type group for "AccountUserSettings".
        /// </summary>
        public readonly string Group = "UserSettings";

        #endregion

        #region Forms

        public readonly string FormMySettings = "MySettings";

        #endregion

        #region Blocks

        public readonly string BlockAccountSettings = "AccountSettings";

        #endregion

        #region Controls

        public readonly string ResetSettingsButton = nameof(ResetSettingsButton);
        public readonly string ChangePasswordButton = nameof(ChangePasswordButton);
        public readonly string ApplyUserSettings = nameof(ApplyUserSettings);

        #endregion

        #region ToString

        public static implicit operator string(AccountUserSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region AclGenerationRule

    /// <summary>
    ///     ID: {76b57ab5-19d4-4cc9-9e80-2c2f05cc2425}
    ///     Alias: AclGenerationRule
    ///     Caption: $CardTypes_TypesNames_AclGenerationRule
    ///     Group: Settings
    /// </summary>
    public class AclGenerationRuleTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "AclGenerationRule": {76b57ab5-19d4-4cc9-9e80-2c2f05cc2425}.
        /// </summary>
        public readonly Guid ID = new Guid(0x76b57ab5,0x19d4,0x4cc9,0x9e,0x80,0x2c,0x2f,0x05,0xcc,0x24,0x25);

        /// <summary>
        ///     Card type name for "AclGenerationRule".
        /// </summary>
        public readonly string Alias = "AclGenerationRule";

        /// <summary>
        ///     Card type caption for "AclGenerationRule".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_AclGenerationRule";

        /// <summary>
        ///     Card type group for "AclGenerationRule".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormAclGenerationRule = "AclGenerationRule";
        public readonly string FormTab = "Tab";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "License" for "License".
        /// </summary>
        public readonly string BlockLicense = "License";

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockSmartRole = "SmartRole";
        public readonly string BlockExtensions = "Extensions";
        public readonly string BlockTriggers = "Triggers";
        public readonly string BlockTriggerMain = "TriggerMain";
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "LicenseHint" for "LicenseHint".
        /// </summary>
        public readonly string LicenseHint = nameof(LicenseHint);

        public readonly string RolesSqlQuery = nameof(RolesSqlQuery);
        public readonly string CardOwnerSelectorSql = nameof(CardOwnerSelectorSql);
        public readonly string CardsByOwnerSelectorSql = nameof(CardsByOwnerSelectorSql);

        /// <summary>
        ///     Control caption "Extensions" for "Extensions".
        /// </summary>
        public readonly string Extensions = nameof(Extensions);
        public readonly string CardTypes = nameof(CardTypes);
        public readonly string OnlySelfUpdate = nameof(OnlySelfUpdate);
        public readonly string UpdateAclCardSelectorSql = nameof(UpdateAclCardSelectorSql);
        public readonly string Triggers = nameof(Triggers);
        public readonly string Validate = nameof(Validate);
        public readonly string ValidateAll = nameof(ValidateAll);
        public readonly string Errors = nameof(Errors);

        #endregion

        #region ToString

        public static implicit operator string(AclGenerationRuleTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ActionHistoryRecord

    /// <summary>
    ///     ID: {abc13918-aa63-45ca-a3f4-d1fd5673c248}
    ///     Alias: ActionHistoryRecord
    ///     Caption: $CardTypes_TypesNames_ActionHistoryRecord
    ///     Group: System
    /// </summary>
    public class ActionHistoryRecordTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "ActionHistoryRecord": {abc13918-aa63-45ca-a3f4-d1fd5673c248}.
        /// </summary>
        public readonly Guid ID = new Guid(0xabc13918,0xaa63,0x45ca,0xa3,0xf4,0xd1,0xfd,0x56,0x73,0xc2,0x48);

        /// <summary>
        ///     Card type name for "ActionHistoryRecord".
        /// </summary>
        public readonly string Alias = "ActionHistoryRecord";

        /// <summary>
        ///     Card type caption for "ActionHistoryRecord".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ActionHistoryRecord";

        /// <summary>
        ///     Card type group for "ActionHistoryRecord".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormActionHistoryRecord = "ActionHistoryRecord";
        public readonly string FormSystemInfo = "SystemInfo";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockShowInWindow = "ShowInWindow";
        public readonly string BlockShowErrorDetails = "ShowErrorDetails";
        public readonly string BlockChangesInfo = "ChangesInfo";
        public readonly string BlockAdditionalDescription = "AdditionalDescription";
        public readonly string BlockSystemInfo = "SystemInfo";

        #endregion

        #region Controls

        public readonly string Category = nameof(Category);
        public readonly string ShowInWindow = nameof(ShowInWindow);
        public readonly string ShowErrorDetails = nameof(ShowErrorDetails);
        public readonly string AdditionalDescription = nameof(AdditionalDescription);

        #endregion

        #region ToString

        public static implicit operator string(ActionHistoryRecordTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region AdSync

    /// <summary>
    ///     ID: {cdaa9e03-9e06-4b4d-9a21-cfc446d2d9d1}
    ///     Alias: AdSync
    ///     Caption: $CardTypes_TypesNames_ADSync
    ///     Group: Settings
    /// </summary>
    public class AdSyncTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "AdSync": {cdaa9e03-9e06-4b4d-9a21-cfc446d2d9d1}.
        /// </summary>
        public readonly Guid ID = new Guid(0xcdaa9e03,0x9e06,0x4b4d,0x9a,0x21,0xcf,0xc4,0x46,0xd2,0xd9,0xd1);

        /// <summary>
        ///     Card type name for "AdSync".
        /// </summary>
        public readonly string Alias = "AdSync";

        /// <summary>
        ///     Card type caption for "AdSync".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ADSync";

        /// <summary>
        ///     Card type group for "AdSync".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormAdSync = "AdSync";
        public readonly string FormManual_sync = "Manual_sync";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "License" for "License".
        /// </summary>
        public readonly string BlockLicense = "License";

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockSyncRoots = "SyncRoots";
        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockRecurrentSync = "RecurrentSync";
        public readonly string BlockSyncActions = "SyncActions";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "LicenseHint" for "LicenseHint".
        /// </summary>
        public readonly string LicenseHint = nameof(LicenseHint);

        public readonly string AdSyncStart = nameof(AdSyncStart);

        #endregion

        #region ToString

        public static implicit operator string(AdSyncTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Application

    /// <summary>
    ///     ID: {029094a5-52a1-4f4f-8aa6-3297f41a3a75}
    ///     Alias: Application
    ///     Caption: $CardTypes_TypesNames_Application
    ///     Group: System
    /// </summary>
    public class ApplicationTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Application": {029094a5-52a1-4f4f-8aa6-3297f41a3a75}.
        /// </summary>
        public readonly Guid ID = new Guid(0x029094a5,0x52a1,0x4f4f,0x8a,0xa6,0x32,0x97,0xf4,0x1a,0x3a,0x75);

        /// <summary>
        ///     Card type name for "Application".
        /// </summary>
        public readonly string Alias = "Application";

        /// <summary>
        ///     Card type caption for "Application".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Application";

        /// <summary>
        ///     Card type group for "Application".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormApplication = "Application";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockFiles = "Files";

        #endregion

        #region Controls

        public readonly string Files = nameof(Files);

        #endregion

        #region ToString

        public static implicit operator string(ApplicationTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region AuthorCondition

    /// <summary>
    ///     ID: {13502054-248d-4619-81e0-5ed08b5a5db9}
    ///     Alias: AuthorCondition
    ///     Caption: $CardTypes_TypesNames_AuthorCondition
    ///     Group: Conditions
    /// </summary>
    public class AuthorConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "AuthorCondition": {13502054-248d-4619-81e0-5ed08b5a5db9}.
        /// </summary>
        public readonly Guid ID = new Guid(0x13502054,0x248d,0x4619,0x81,0xe0,0x5e,0xd0,0x8b,0x5a,0x5d,0xb9);

        /// <summary>
        ///     Card type name for "AuthorCondition".
        /// </summary>
        public readonly string Alias = "AuthorCondition";

        /// <summary>
        ///     Card type caption for "AuthorCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_AuthorCondition";

        /// <summary>
        ///     Card type group for "AuthorCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormAuthorCondition = "AuthorCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(AuthorConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region AutoCompleteDialogs

    /// <summary>
    ///     ID: {8067e1c6-15a4-4741-b62c-29e5257efedd}
    ///     Alias: AutoCompleteDialogs
    ///     Caption: $Cards_DefaultCaption
    ///     Group: System
    /// </summary>
    public class AutoCompleteDialogsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "AutoCompleteDialogs": {8067e1c6-15a4-4741-b62c-29e5257efedd}.
        /// </summary>
        public readonly Guid ID = new Guid(0x8067e1c6,0x15a4,0x4741,0xb6,0x2c,0x29,0xe5,0x25,0x7e,0xfe,0xdd);

        /// <summary>
        ///     Card type name for "AutoCompleteDialogs".
        /// </summary>
        public readonly string Alias = "AutoCompleteDialogs";

        /// <summary>
        ///     Card type caption for "AutoCompleteDialogs".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "AutoCompleteDialogs".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormSingleValueSelector = "SingleValueSelector";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "MainBlock" for "MainBlock".
        /// </summary>
        public readonly string BlockMainBlock = "MainBlock";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "MainViewControl" for "MainViewControl".
        /// </summary>
        public readonly string MainViewControl = nameof(MainViewControl);

        #endregion

        #region ToString

        public static implicit operator string(AutoCompleteDialogsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region BusinessProcessTemplate

    /// <summary>
    ///     ID: {d05799bd-35b6-43b4-97dd-b6d0e683eff2}
    ///     Alias: BusinessProcessTemplate
    ///     Caption: $CardTypes_TypesNames_BusinessProcessTemplate
    ///     Group: Dictionaries
    /// </summary>
    public class BusinessProcessTemplateTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "BusinessProcessTemplate": {d05799bd-35b6-43b4-97dd-b6d0e683eff2}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd05799bd,0x35b6,0x43b4,0x97,0xdd,0xb6,0xd0,0xe6,0x83,0xef,0xf2);

        /// <summary>
        ///     Card type name for "BusinessProcessTemplate".
        /// </summary>
        public readonly string Alias = "BusinessProcessTemplate";

        /// <summary>
        ///     Card type caption for "BusinessProcessTemplate".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_BusinessProcessTemplate";

        /// <summary>
        ///     Card type group for "BusinessProcessTemplate".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormBusinessProcessTemplate = "BusinessProcessTemplate";

        #endregion

        #region Blocks

        public readonly string BlockInfo = "Info";
        public readonly string BlockAdditional = "Additional";
        public readonly string BlockNotifications = "Notifications";
        public readonly string BlockPermissions = "Permissions";
        public readonly string BlockVersions = "Versions";
        public readonly string BlockMainBlock = "MainBlock";
        public readonly string BlockMainBlock2 = "MainBlock2";
        public readonly string BlockMainBlock3 = "MainBlock3";
        public readonly string BlockExecutionAccessDeniedBlock = "ExecutionAccessDeniedBlock";
        public readonly string BlockProcessBlock = "ProcessBlock";
        public readonly string BlockConditionBlock = "ConditionBlock";
        public readonly string BlockButtons = "Buttons";

        #endregion

        #region Controls

        public readonly string VersionsTable = nameof(VersionsTable);
        public readonly string Compile = nameof(Compile);
        public readonly string ProcessButtons = nameof(ProcessButtons);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessTemplateTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Calendar

    /// <summary>
    ///     ID: {9079d7f9-7b44-43f2-bf11-f9ed11965dd6}
    ///     Alias: Calendar
    ///     Caption: $CardTypes_TypesNames_Tabs_Calendar
    ///     Group: Settings
    /// </summary>
    public class CalendarTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Calendar": {9079d7f9-7b44-43f2-bf11-f9ed11965dd6}.
        /// </summary>
        public readonly Guid ID = new Guid(0x9079d7f9,0x7b44,0x43f2,0xbf,0x11,0xf9,0xed,0x11,0x96,0x5d,0xd6);

        /// <summary>
        ///     Card type name for "Calendar".
        /// </summary>
        public readonly string Alias = "Calendar";

        /// <summary>
        ///     Card type caption for "Calendar".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Tabs_Calendar";

        /// <summary>
        ///     Card type group for "Calendar".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormCalendar = "Calendar";

        #endregion

        #region Blocks

        public readonly string BlockBlock3 = "Block3";

        /// <summary>
        ///     Block caption "Block5" for "Block5".
        /// </summary>
        public readonly string BlockBlock5 = "Block5";

        /// <summary>
        ///     Block caption "Block6" for "Block6".
        /// </summary>
        public readonly string BlockBlock6 = "Block6";

        /// <summary>
        ///     Block caption "Block2" for "Block2".
        /// </summary>
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock4 = "Block4";
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string CalendarType = nameof(CalendarType);
        public readonly string RebuildCalendar = nameof(RebuildCalendar);
        public readonly string ValidateCalendar = nameof(ValidateCalendar);
        public readonly string NamedRanges = nameof(NamedRanges);

        #endregion

        #region ToString

        public static implicit operator string(CalendarTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CalendarCalcMethod

    /// <summary>
    ///     ID: {2a9b3b6d-1c22-46dc-ada2-7437fd4273ce}
    ///     Alias: CalendarCalcMethod
    ///     Caption: $CardTypes_TypesNames_Tabs_Controls_CalendarCalcMethod
    ///     Group: Settings
    /// </summary>
    public class CalendarCalcMethodTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "CalendarCalcMethod": {2a9b3b6d-1c22-46dc-ada2-7437fd4273ce}.
        /// </summary>
        public readonly Guid ID = new Guid(0x2a9b3b6d,0x1c22,0x46dc,0xad,0xa2,0x74,0x37,0xfd,0x42,0x73,0xce);

        /// <summary>
        ///     Card type name for "CalendarCalcMethod".
        /// </summary>
        public readonly string Alias = "CalendarCalcMethod";

        /// <summary>
        ///     Card type caption for "CalendarCalcMethod".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Tabs_Controls_CalendarCalcMethod";

        /// <summary>
        ///     Card type group for "CalendarCalcMethod".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormCalendarCalcMethod = "CalendarCalcMethod";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        /// <summary>
        ///     Block caption "Block2" for "Block2".
        /// </summary>
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        public readonly string CompileButton = nameof(CompileButton);

        #endregion

        #region ToString

        public static implicit operator string(CalendarCalcMethodTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Car

    /// <summary>
    ///     ID: {d0006e40-a342-4797-8d77-6501c4b7c4ac}
    ///     Alias: Car
    ///     Caption: $CardTypes_TypesNames_Car
    ///     Group: (Без группы)
    /// </summary>
    public class CarTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Car": {d0006e40-a342-4797-8d77-6501c4b7c4ac}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd0006e40,0xa342,0x4797,0x8d,0x77,0x65,0x01,0xc4,0xb7,0xc4,0xac);

        /// <summary>
        ///     Card type name for "Car".
        /// </summary>
        public readonly string Alias = "Car";

        /// <summary>
        ///     Card type caption for "Car".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Car";

        /// <summary>
        ///     Card type group for "Car".
        /// </summary>
        public readonly string Group = "(Без группы)";

        #endregion

        #region Forms

        public readonly string FormCar = "Car";
        public readonly string FormFileCopies = "FileCopies";
        public readonly string FormFiles = "Files";
        public readonly string FormDialog_1C = "Dialog_1C";
        public readonly string FormViewsDemonstrations = "ViewsDemonstrations";
        public readonly string FormTaskHistory = "TaskHistory";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockAdditionalInfo = "AdditionalInfo";
        public readonly string BlockMainInfo2 = "MainInfo2";
        public readonly string BlockSale = "Sale";
        public readonly string BlockCustomer = "Customer";
        public readonly string BlockOperation = "Operation";
        public readonly string BlockCustomers = "Customers";
        public readonly string BlockFiles = "Files";
        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockFileTabs = "FileTabs";
        public readonly string BlockFileCopies = "FileCopies";

        /// <summary>
        ///     Block caption "Превью1" for "Block2".
        /// </summary>
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockDriverInfo = "DriverInfo";
        public readonly string BlockTableViewDemo = "TableViewDemo";

        /// <summary>
        ///     Block caption "TaskHistory" for "TaskHistory".
        /// </summary>
        public readonly string BlockTaskHistory = "TaskHistory";

        #endregion

        #region Controls

        public readonly string CarName = nameof(CarName);
        public readonly string Running = nameof(Running);
        public readonly string ReleaseDate = nameof(ReleaseDate);
        public readonly string ReleaseDate2 = nameof(ReleaseDate2);

        /// <summary>
        ///     Control caption "Html" for "HtmlControl".
        /// </summary>
        public readonly string HtmlControl = nameof(HtmlControl);
        public readonly string Color = nameof(Color);
        public readonly string ShareName = nameof(ShareName);
        public readonly string ManagerName = nameof(ManagerName);
        public readonly string EndDate = nameof(EndDate);
        public readonly string Buyers = nameof(Buyers);
        public readonly string ShareList = nameof(ShareList);
        public readonly string DriverName = nameof(DriverName);
        public readonly string DriverName2 = nameof(DriverName2);
        public readonly string Owners = nameof(Owners);
        public readonly string Owners2 = nameof(Owners2);
        public readonly string Files1 = nameof(Files1);
        public readonly string Files2 = nameof(Files2);
        public readonly string Files3 = nameof(Files3);
        public readonly string AllFilesControl = nameof(AllFilesControl);
        public readonly string ImageFilesControl = nameof(ImageFilesControl);
        public readonly string Get1CButton = nameof(Get1CButton);
        public readonly string ShowDialogTypeForm = nameof(ShowDialogTypeForm);
        public readonly string FileInTabs = nameof(FileInTabs);
        public readonly string FileTabs = nameof(FileTabs);
        public readonly string FileCopies = nameof(FileCopies);

        /// <summary>
        ///     Control caption "Control1" for "CompareFilesView1".
        /// </summary>
        public readonly string CompareFilesView1 = nameof(CompareFilesView1);

        /// <summary>
        ///     Control caption "Control2" for "CompareFilesView2".
        /// </summary>
        public readonly string CompareFilesView2 = nameof(CompareFilesView2);

        /// <summary>
        ///     Control caption "Preview1" for "Preview1".
        /// </summary>
        public readonly string Preview1 = nameof(Preview1);

        /// <summary>
        ///     Control caption "Preview2" for "Preview2".
        /// </summary>
        public readonly string Preview2 = nameof(Preview2);
        public readonly string ShareListView = nameof(ShareListView);
        public readonly string BuyersView = nameof(BuyersView);
        public readonly string BuyerOperationsView = nameof(BuyerOperationsView);

        /// <summary>
        ///     Control caption "TaskHistory" for "TaskHistoryViewControl".
        /// </summary>
        public readonly string TaskHistoryViewControl = nameof(TaskHistoryViewControl);

        #endregion

        #region ToString

        public static implicit operator string(CarTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Car1CDialog

    /// <summary>
    ///     ID: {0fa7aaeb-dc36-4647-a29e-2eb87153984d}
    ///     Alias: Car1CDialog
    ///     Caption: $CardTypes_Tabs_Dialog1C
    ///     Group: (Без группы)
    /// </summary>
    public class Car1CDialogTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Car1CDialog": {0fa7aaeb-dc36-4647-a29e-2eb87153984d}.
        /// </summary>
        public readonly Guid ID = new Guid(0x0fa7aaeb,0xdc36,0x4647,0xa2,0x9e,0x2e,0xb8,0x71,0x53,0x98,0x4d);

        /// <summary>
        ///     Card type name for "Car1CDialog".
        /// </summary>
        public readonly string Alias = "Car1CDialog";

        /// <summary>
        ///     Card type caption for "Car1CDialog".
        /// </summary>
        public readonly string Caption = "$CardTypes_Tabs_Dialog1C";

        /// <summary>
        ///     Card type group for "Car1CDialog".
        /// </summary>
        public readonly string Group = "(Без группы)";

        #endregion

        #region Forms

        public readonly string FormDialog1C = "Dialog1C";

        #endregion

        #region Blocks

        public readonly string BlockDriverInfo = "DriverInfo";

        #endregion

        #region ToString

        public static implicit operator string(Car1CDialogTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CardTasksEditorDialog

    /// <summary>
    ///     ID: {db737600-1bf6-451b-80ca-01fe06161ee6}
    ///     Alias: CardTasksEditorDialog
    ///     Caption: $CardTypes_CardTasksEditor
    ///     Group: System
    /// </summary>
    public class CardTasksEditorDialogTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "CardTasksEditorDialog": {db737600-1bf6-451b-80ca-01fe06161ee6}.
        /// </summary>
        public readonly Guid ID = new Guid(0xdb737600,0x1bf6,0x451b,0x80,0xca,0x01,0xfe,0x06,0x16,0x1e,0xe6);

        /// <summary>
        ///     Card type name for "CardTasksEditorDialog".
        /// </summary>
        public readonly string Alias = "CardTasksEditorDialog";

        /// <summary>
        ///     Card type caption for "CardTasksEditorDialog".
        /// </summary>
        public readonly string Caption = "$CardTypes_CardTasksEditor";

        /// <summary>
        ///     Card type group for "CardTasksEditorDialog".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormCardTasksEditorDialog = "CardTasksEditorDialog";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "CardTasks" for "CardTasks".
        /// </summary>
        public readonly string CardTasks = nameof(CardTasks);

        #endregion

        #region ToString

        public static implicit operator string(CardTasksEditorDialogTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CarViewParameters

    /// <summary>
    ///     ID: {ef08853d-7fdf-4fec-91b2-a1b8905e29fc}
    ///     Alias: CarViewParameters
    ///     Caption: $Views_FilterDialog_Caption
    ///     Group: (Без группы)
    /// </summary>
    public class CarViewParametersTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "CarViewParameters": {ef08853d-7fdf-4fec-91b2-a1b8905e29fc}.
        /// </summary>
        public readonly Guid ID = new Guid(0xef08853d,0x7fdf,0x4fec,0x91,0xb2,0xa1,0xb8,0x90,0x5e,0x29,0xfc);

        /// <summary>
        ///     Card type name for "CarViewParameters".
        /// </summary>
        public readonly string Alias = "CarViewParameters";

        /// <summary>
        ///     Card type caption for "CarViewParameters".
        /// </summary>
        public readonly string Caption = "$Views_FilterDialog_Caption";

        /// <summary>
        ///     Card type group for "CarViewParameters".
        /// </summary>
        public readonly string Group = "(Без группы)";

        #endregion

        #region Forms

        public readonly string FormMainTab = "MainTab";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Parameters" for "Parameters".
        /// </summary>
        public readonly string BlockParameters = "Parameters";

        #endregion

        #region ToString

        public static implicit operator string(CarViewParametersTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CompletionOption

    /// <summary>
    ///     ID: {f6b95639-234e-4800-a2f1-3cb20e0bcda4}
    ///     Alias: CompletionOption
    ///     Caption: $CardTypes_TypesNames_CompletionOption
    ///     Group: System
    /// </summary>
    public class CompletionOptionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "CompletionOption": {f6b95639-234e-4800-a2f1-3cb20e0bcda4}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf6b95639,0x234e,0x4800,0xa2,0xf1,0x3c,0xb2,0x0e,0x0b,0xcd,0xa4);

        /// <summary>
        ///     Card type name for "CompletionOption".
        /// </summary>
        public readonly string Alias = "CompletionOption";

        /// <summary>
        ///     Card type caption for "CompletionOption".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_CompletionOption";

        /// <summary>
        ///     Card type group for "CompletionOption".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormCompletionOption = "CompletionOption";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";

        #endregion

        #region ToString

        public static implicit operator string(CompletionOptionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ConditionsBase

    /// <summary>
    ///     ID: {0eb20600-f137-42ad-b007-440492200acf}
    ///     Alias: ConditionsBase
    ///     Caption: ConditionsBase
    ///     Group: Conditions
    /// </summary>
    public class ConditionsBaseTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "ConditionsBase": {0eb20600-f137-42ad-b007-440492200acf}.
        /// </summary>
        public readonly Guid ID = new Guid(0x0eb20600,0xf137,0x42ad,0xb0,0x07,0x44,0x04,0x92,0x20,0x0a,0xcf);

        /// <summary>
        ///     Card type name for "ConditionsBase".
        /// </summary>
        public readonly string Alias = "ConditionsBase";

        /// <summary>
        ///     Card type caption for "ConditionsBase".
        /// </summary>
        public readonly string Caption = "ConditionsBase";

        /// <summary>
        ///     Card type group for "ConditionsBase".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormConditionsBase = "ConditionsBase";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockConditions = "Conditions";

        #endregion

        #region Controls

        public readonly string ConditionType = nameof(ConditionType);
        public readonly string ConditionsTable = nameof(ConditionsTable);

        #endregion

        #region ToString

        public static implicit operator string(ConditionsBaseTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ConditionType

    /// <summary>
    ///     ID: {ca37649d-9585-4c83-8ef4-fdf91c4c42ee}
    ///     Alias: ConditionType
    ///     Caption: $CardTypes_TypesNames_ConditionType
    ///     Group: Settings
    /// </summary>
    public class ConditionTypeTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "ConditionType": {ca37649d-9585-4c83-8ef4-fdf91c4c42ee}.
        /// </summary>
        public readonly Guid ID = new Guid(0xca37649d,0x9585,0x4c83,0x8e,0xf4,0xfd,0xf9,0x1c,0x4c,0x42,0xee);

        /// <summary>
        ///     Card type name for "ConditionType".
        /// </summary>
        public readonly string Alias = "ConditionType";

        /// <summary>
        ///     Card type caption for "ConditionType".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ConditionType";

        /// <summary>
        ///     Card type group for "ConditionType".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormConditionType = "ConditionType";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        /// <summary>
        ///     Block caption "Block2" for "Block2".
        /// </summary>
        public readonly string BlockBlock2 = "Block2";

        /// <summary>
        ///     Block caption "Block3" for "Block3".
        /// </summary>
        public readonly string BlockBlock3 = "Block3";

        public readonly string BlockExamples = "Examples";

        #endregion

        #region Controls

        public readonly string RepairConditionsButton = nameof(RepairConditionsButton);
        public readonly string RepairAllConditionsButton = nameof(RepairAllConditionsButton);
        public readonly string CompileButton = nameof(CompileButton);

        #endregion

        #region ToString

        public static implicit operator string(ConditionTypeTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ContextRole

    /// <summary>
    ///     ID: {b672e00c-0241-0485-9b07-4764bc96c9d3}
    ///     Alias: ContextRole
    ///     Caption: $CardTypes_TypesNames_ContextRole
    ///     Group: Roles
    /// </summary>
    public class ContextRoleTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "ContextRole": {b672e00c-0241-0485-9b07-4764bc96c9d3}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb672e00c,0x0241,0x0485,0x9b,0x07,0x47,0x64,0xbc,0x96,0xc9,0xd3);

        /// <summary>
        ///     Card type name for "ContextRole".
        /// </summary>
        public readonly string Alias = "ContextRole";

        /// <summary>
        ///     Card type caption for "ContextRole".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ContextRole";

        /// <summary>
        ///     Card type group for "ContextRole".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region Forms

        public readonly string FormContextRole = "ContextRole";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockGeneratedSQL = "GeneratedSQL";

        #endregion

        #region Controls

        public readonly string DisableDeputies = nameof(DisableDeputies);

        #endregion

        #region ToString

        public static implicit operator string(ContextRoleTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Contract

    /// <summary>
    ///     ID: {335f86a1-d009-012c-8b45-1f43c2382c2d}
    ///     Alias: Contract
    ///     Caption: $CardTypes_TypesNames_Contract
    ///     Group: Documents
    /// </summary>
    public class ContractTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Contract": {335f86a1-d009-012c-8b45-1f43c2382c2d}.
        /// </summary>
        public readonly Guid ID = new Guid(0x335f86a1,0xd009,0x012c,0x8b,0x45,0x1f,0x43,0xc2,0x38,0x2c,0x2d);

        /// <summary>
        ///     Card type name for "Contract".
        /// </summary>
        public readonly string Alias = "Contract";

        /// <summary>
        ///     Card type caption for "Contract".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Contract";

        /// <summary>
        ///     Card type group for "Contract".
        /// </summary>
        public readonly string Group = "Documents";

        #endregion

        #region Document Types

        /// <summary>
        ///     Document type identifier for "$KrTypes_DocTypes_Contract": {93a392e7-097c-4420-85c4-db10b2df3c1d}.
        /// </summary>
        public readonly Guid KrTypes_DocTypes_ContractID = new(0x93a392e7,0x097c,0x4420,0x85,0xc4,0xdb,0x10,0xb2,0xdf,0x3c,0x1d);

        /// <summary>
        ///     Document type caption for "$KrTypes_DocTypes_Contract".
        /// </summary>
        public readonly string KrTypes_DocTypes_ContractCaption = "$KrTypes_DocTypes_Contract";

        #endregion

        #region Forms

        public readonly string FormContract = "Contract";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockBlock4 = "Block4";
        public readonly string BlockDepartmentSignedByRecipientsPerformersBlock = "DepartmentSignedByRecipientsPerformersBlock";
        public readonly string BlockRefsBlock = "RefsBlock";
        public readonly string BlockBlock5 = "Block5";

        #endregion

        #region Controls

        public readonly string PartnerControl = nameof(PartnerControl);
        public readonly string IncomingRefsControl = nameof(IncomingRefsControl);
        public readonly string Files = nameof(Files);

        #endregion

        #region ToString

        public static implicit operator string(ContractTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CreateFileFromTemplate

    /// <summary>
    ///     ID: {98662e67-2e75-4d33-97d0-e1cefa096336}
    ///     Alias: CreateFileFromTemplate
    ///     Caption: $Cards_FileFromTemplate_CreateDialog
    ///     Group: System
    /// </summary>
    public class CreateFileFromTemplateTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "CreateFileFromTemplate": {98662e67-2e75-4d33-97d0-e1cefa096336}.
        /// </summary>
        public readonly Guid ID = new Guid(0x98662e67,0x2e75,0x4d33,0x97,0xd0,0xe1,0xce,0xfa,0x09,0x63,0x36);

        /// <summary>
        ///     Card type name for "CreateFileFromTemplate".
        /// </summary>
        public readonly string Alias = "CreateFileFromTemplate";

        /// <summary>
        ///     Card type caption for "CreateFileFromTemplate".
        /// </summary>
        public readonly string Caption = "$Cards_FileFromTemplate_CreateDialog";

        /// <summary>
        ///     Card type group for "CreateFileFromTemplate".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormCreateFileFromTemplate = "CreateFileFromTemplate";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "TemplatesList" for "TemplatesList".
        /// </summary>
        public readonly string BlockTemplatesList = "TemplatesList";

        public readonly string BlockMainSettings = "MainSettings";

        /// <summary>
        ///     Block caption "TemplateParameters" for "TemplateParameters".
        /// </summary>
        public readonly string BlockTemplateParameters = "TemplateParameters";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "FileTemplates" for "FileTemplates".
        /// </summary>
        public readonly string FileTemplates = nameof(FileTemplates);

        public readonly string FileName = nameof(FileName);
        public readonly string Extension = nameof(Extension);
        public readonly string ConvertToPDF = nameof(ConvertToPDF);

        #endregion

        #region ToString

        public static implicit operator string(CreateFileFromTemplateTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Currency

    /// <summary>
    ///     ID: {4bce97d2-6b76-468d-9b09-711ba189cb1e}
    ///     Alias: Currency
    ///     Caption: $CardTypes_TypesNames_Currency
    ///     Group: Dictionaries
    /// </summary>
    public class CurrencyTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Currency": {4bce97d2-6b76-468d-9b09-711ba189cb1e}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4bce97d2,0x6b76,0x468d,0x9b,0x09,0x71,0x1b,0xa1,0x89,0xcb,0x1e);

        /// <summary>
        ///     Card type name for "Currency".
        /// </summary>
        public readonly string Alias = "Currency";

        /// <summary>
        ///     Card type caption for "Currency".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Currency";

        /// <summary>
        ///     Card type group for "Currency".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormCurrency = "Currency";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(CurrencyTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DefaultCalendarType

    /// <summary>
    ///     ID: {2d1d11a0-3ed0-46a6-bd5c-8fc8a952eabd}
    ///     Alias: DefaultCalendarType
    ///     Caption: $CardTypes_TypesNames_Tabs_DefaultCalendarType
    ///     Group: Settings
    /// </summary>
    public class DefaultCalendarTypeTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "DefaultCalendarType": {2d1d11a0-3ed0-46a6-bd5c-8fc8a952eabd}.
        /// </summary>
        public readonly Guid ID = new Guid(0x2d1d11a0,0x3ed0,0x46a6,0xbd,0x5c,0x8f,0xc8,0xa9,0x52,0xea,0xbd);

        /// <summary>
        ///     Card type name for "DefaultCalendarType".
        /// </summary>
        public readonly string Alias = "DefaultCalendarType";

        /// <summary>
        ///     Card type caption for "DefaultCalendarType".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Tabs_DefaultCalendarType";

        /// <summary>
        ///     Card type group for "DefaultCalendarType".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormDefaultCalendarType = "DefaultCalendarType";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock4 = "Block4";

        /// <summary>
        ///     Block caption "Warning" for "Block5".
        /// </summary>
        public readonly string BlockBlock5 = "Block5";
        public readonly string BlockWorkTimeBlock = "WorkTimeBlock";
        public readonly string BlockLunchTimeBlock = "LunchTimeBlock";
        public readonly string BlockBlock3 = "Block3";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "Warning" for "AdvancedNotificationsTooltip1".
        /// </summary>
        public readonly string AdvancedNotificationsTooltip1 = nameof(AdvancedNotificationsTooltip1);

        public readonly string WeekDaysControl = nameof(WeekDaysControl);

        #endregion

        #region ToString

        public static implicit operator string(DefaultCalendarTypeTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Deleted

    /// <summary>
    ///     ID: {f5e74fbb-5357-4a6d-adce-4c2607853fdd}
    ///     Alias: Deleted
    ///     Caption: $CardTypes_TypesNames_Deleted
    ///     Group: System
    /// </summary>
    public class DeletedTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Deleted": {f5e74fbb-5357-4a6d-adce-4c2607853fdd}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf5e74fbb,0x5357,0x4a6d,0xad,0xce,0x4c,0x26,0x07,0x85,0x3f,0xdd);

        /// <summary>
        ///     Card type name for "Deleted".
        /// </summary>
        public readonly string Alias = "Deleted";

        /// <summary>
        ///     Card type caption for "Deleted".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Deleted";

        /// <summary>
        ///     Card type group for "Deleted".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormDeleted = "Deleted";
        public readonly string FormTasks = "Tasks";
        public readonly string FormSystemInfo = "SystemInfo";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockFiles = "Files";
        public readonly string BlockTasks = "Tasks";
        public readonly string BlockSystemInfo = "SystemInfo";

        #endregion

        #region ToString

        public static implicit operator string(DeletedTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DepartmentCondition

    /// <summary>
    ///     ID: {c8c6d9b5-0cc0-4f09-8d67-eb3f8e2e0bd4}
    ///     Alias: DepartmentCondition
    ///     Caption: $CardTypes_TypesNames_DepartmentCondition
    ///     Group: Conditions
    /// </summary>
    public class DepartmentConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "DepartmentCondition": {c8c6d9b5-0cc0-4f09-8d67-eb3f8e2e0bd4}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc8c6d9b5,0x0cc0,0x4f09,0x8d,0x67,0xeb,0x3f,0x8e,0x2e,0x0b,0xd4);

        /// <summary>
        ///     Card type name for "DepartmentCondition".
        /// </summary>
        public readonly string Alias = "DepartmentCondition";

        /// <summary>
        ///     Card type caption for "DepartmentCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_DepartmentCondition";

        /// <summary>
        ///     Card type group for "DepartmentCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormDepartmentCondition = "DepartmentCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(DepartmentConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DepartmentRole

    /// <summary>
    ///     ID: {abe57cb7-e1cb-06f6-b7ca-ad1668bebd72}
    ///     Alias: DepartmentRole
    ///     Caption: $CardTypes_TypesNames_DepartmentRole
    ///     Group: Roles
    /// </summary>
    public class DepartmentRoleTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "DepartmentRole": {abe57cb7-e1cb-06f6-b7ca-ad1668bebd72}.
        /// </summary>
        public readonly Guid ID = new Guid(0xabe57cb7,0xe1cb,0x06f6,0xb7,0xca,0xad,0x16,0x68,0xbe,0xbd,0x72);

        /// <summary>
        ///     Card type name for "DepartmentRole".
        /// </summary>
        public readonly string Alias = "DepartmentRole";

        /// <summary>
        ///     Card type caption for "DepartmentRole".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_DepartmentRole";

        /// <summary>
        ///     Card type group for "DepartmentRole".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region Forms

        public readonly string FormDepartmentRole = "DepartmentRole";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockAdInfo = "AdInfo";
        public readonly string BlockUsers = "Users";
        public readonly string BlockFiles = "Files";

        #endregion

        #region Controls

        public readonly string TimeZone = nameof(TimeZone);
        public readonly string InheritTimeZone = nameof(InheritTimeZone);
        public readonly string AdSyncDate = nameof(AdSyncDate);
        public readonly string AdSyncWhenChanged = nameof(AdSyncWhenChanged);
        public readonly string AdSyncDistinguishedName = nameof(AdSyncDistinguishedName);
        public readonly string AdSyncID = nameof(AdSyncID);
        public readonly string AdSyncDisableUpdate = nameof(AdSyncDisableUpdate);
        public readonly string AdSyncIndependent = nameof(AdSyncIndependent);
        public readonly string AdSyncManualSync = nameof(AdSyncManualSync);
        public readonly string Files = nameof(Files);

        #endregion

        #region ToString

        public static implicit operator string(DepartmentRoleTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Dialogs

    /// <summary>
    ///     ID: {d3107c45-c40a-4a86-b831-fbef1b71050f}
    ///     Alias: Dialogs
    ///     Caption: $CardTypes_TypesNames_Dialogs
    ///     Group: System
    /// </summary>
    public class DialogsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Dialogs": {d3107c45-c40a-4a86-b831-fbef1b71050f}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd3107c45,0xc40a,0x4a86,0xb8,0x31,0xfb,0xef,0x1b,0x71,0x05,0x0f);

        /// <summary>
        ///     Card type name for "Dialogs".
        /// </summary>
        public readonly string Alias = "Dialogs";

        /// <summary>
        ///     Card type caption for "Dialogs".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Dialogs";

        /// <summary>
        ///     Card type group for "Dialogs".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormCreateMultipleCards = "CreateMultipleCards";
        public readonly string FormAcquaintance = "Acquaintance";
        public readonly string FormAddEmployees = "AddEmployees";
        public readonly string FormChangePassword = "ChangePassword";
        public readonly string FormApplyUserSettings = "ApplyUserSettings";
        public readonly string FormDownloadDeski = "DownloadDeski";

        #endregion

        #region Blocks

        public readonly string BlockMainBlock = "MainBlock";
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string ChangePartner = nameof(ChangePartner);
        public readonly string ChangeAuthor = nameof(ChangeAuthor);
        public readonly string OldPassword = nameof(OldPassword);

        /// <summary>
        ///     Control caption "Warning label" for "WarningLabel".
        /// </summary>
        public readonly string WarningLabel = nameof(WarningLabel);
        public readonly string Password = nameof(Password);
        public readonly string PasswordRepeat = nameof(PasswordRepeat);

        /// <summary>
        ///     Control caption "WebApp" for "WebApp".
        /// </summary>
        public readonly string WebApp = nameof(WebApp);

        /// <summary>
        ///     Control caption "DownloadButton" for "DownloadButton".
        /// </summary>
        public readonly string DownloadButton = nameof(DownloadButton);

        /// <summary>
        ///     Control caption "Description" for "Description".
        /// </summary>
        public readonly string Description = nameof(Description);

        #endregion

        #region ToString

        public static implicit operator string(DialogsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DocLoad

    /// <summary>
    ///     ID: {33023ffa-2fd3-4b3b-80d9-bba6ab48ea8e}
    ///     Alias: DocLoad
    ///     Caption: $CardTypes_TypesNames_DocLoad
    ///     Group: Settings
    /// </summary>
    public class DocLoadTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "DocLoad": {33023ffa-2fd3-4b3b-80d9-bba6ab48ea8e}.
        /// </summary>
        public readonly Guid ID = new Guid(0x33023ffa,0x2fd3,0x4b3b,0x80,0xd9,0xbb,0xa6,0xab,0x48,0xea,0x8e);

        /// <summary>
        ///     Card type name for "DocLoad".
        /// </summary>
        public readonly string Alias = "DocLoad";

        /// <summary>
        ///     Card type caption for "DocLoad".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_DocLoad";

        /// <summary>
        ///     Card type group for "DocLoad".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormDocLoad = "DocLoad";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "License" for "License".
        /// </summary>
        public readonly string BlockLicense = "License";

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockRecognition = "Recognition";
        public readonly string BlockBarcodes = "Barcodes";
        public readonly string BlockBarcodeSettings = "BarcodeSettings";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "LicenseHint" for "LicenseHint".
        /// </summary>
        public readonly string LicenseHint = nameof(LicenseHint);

        public readonly string StreamingInputUser = nameof(StreamingInputUser);

        /// <summary>
        ///     Control caption "RecognitionHint" for "RecognitionHint".
        /// </summary>
        public readonly string RecognitionHint = nameof(RecognitionHint);

        #endregion

        #region ToString

        public static implicit operator string(DocLoadTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DocStateCondition

    /// <summary>
    ///     ID: {d57e0e60-6cae-4a4b-a481-2eeb704e46ec}
    ///     Alias: DocStateCondition
    ///     Caption: $CardTypes_TypesNames_DocStateCondition
    ///     Group: Conditions
    /// </summary>
    public class DocStateConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "DocStateCondition": {d57e0e60-6cae-4a4b-a481-2eeb704e46ec}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd57e0e60,0x6cae,0x4a4b,0xa4,0x81,0x2e,0xeb,0x70,0x4e,0x46,0xec);

        /// <summary>
        ///     Card type name for "DocStateCondition".
        /// </summary>
        public readonly string Alias = "DocStateCondition";

        /// <summary>
        ///     Card type caption for "DocStateCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_DocStateCondition";

        /// <summary>
        ///     Card type group for "DocStateCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormDocStateCondition = "DocStateCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(DocStateConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DocTypeCondition

    /// <summary>
    ///     ID: {6a525f7e-deaa-48a0-b2c7-20c2c5151932}
    ///     Alias: DocTypeCondition
    ///     Caption: $CardTypes_TypesNames_DocTypeCondition
    ///     Group: Conditions
    /// </summary>
    public class DocTypeConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "DocTypeCondition": {6a525f7e-deaa-48a0-b2c7-20c2c5151932}.
        /// </summary>
        public readonly Guid ID = new Guid(0x6a525f7e,0xdeaa,0x48a0,0xb2,0xc7,0x20,0xc2,0xc5,0x15,0x19,0x32);

        /// <summary>
        ///     Card type name for "DocTypeCondition".
        /// </summary>
        public readonly string Alias = "DocTypeCondition";

        /// <summary>
        ///     Card type caption for "DocTypeCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_DocTypeCondition";

        /// <summary>
        ///     Card type group for "DocTypeCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormDocTypeCondition = "DocTypeCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(DocTypeConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Document

    /// <summary>
    ///     ID: {6d06c5a0-9687-4f6b-9bed-d3a081d84d9a}
    ///     Alias: Document
    ///     Caption: $CardTypes_TypesNames_Document
    ///     Group: Documents
    /// </summary>
    public class DocumentTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Document": {6d06c5a0-9687-4f6b-9bed-d3a081d84d9a}.
        /// </summary>
        public readonly Guid ID = new Guid(0x6d06c5a0,0x9687,0x4f6b,0x9b,0xed,0xd3,0xa0,0x81,0xd8,0x4d,0x9a);

        /// <summary>
        ///     Card type name for "Document".
        /// </summary>
        public readonly string Alias = "Document";

        /// <summary>
        ///     Card type caption for "Document".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Document";

        /// <summary>
        ///     Card type group for "Document".
        /// </summary>
        public readonly string Group = "Documents";

        #endregion

        #region Document Types

        /// <summary>
        ///     Document type identifier for "$KrTypes_DocTypes_Addendum": {d1906ef6-38f1-4a05-b943-52c87da7e8ba}.
        /// </summary>
        public readonly Guid KrTypes_DocTypes_AddendumID = new(0xd1906ef6,0x38f1,0x4a05,0xb9,0x43,0x52,0xc8,0x7d,0xa7,0xe8,0xba);

        /// <summary>
        ///     Document type caption for "$KrTypes_DocTypes_Addendum".
        /// </summary>
        public readonly string KrTypes_DocTypes_AddendumCaption = "$KrTypes_DocTypes_Addendum";

        /// <summary>
        ///     Document type identifier for "$KrTypes_DocTypes_Instruction": {b8463b4e-2d5f-4923-983e-aca16fb01635}.
        /// </summary>
        public readonly Guid KrTypes_DocTypes_InstructionID = new(0xb8463b4e,0x2d5f,0x4923,0x98,0x3e,0xac,0xa1,0x6f,0xb0,0x16,0x35);

        /// <summary>
        ///     Document type caption for "$KrTypes_DocTypes_Instruction".
        /// </summary>
        public readonly string KrTypes_DocTypes_InstructionCaption = "$KrTypes_DocTypes_Instruction";

        /// <summary>
        ///     Document type identifier for "$KrTypes_DocTypes_Document": {ea798bef-1b3b-49eb-9131-87333dafec19}.
        /// </summary>
        public readonly Guid KrTypes_DocTypes_DocumentID = new(0xea798bef,0x1b3b,0x49eb,0x91,0x31,0x87,0x33,0x3d,0xaf,0xec,0x19);

        /// <summary>
        ///     Document type caption for "$KrTypes_DocTypes_Document".
        /// </summary>
        public readonly string KrTypes_DocTypes_DocumentCaption = "$KrTypes_DocTypes_Document";

        /// <summary>
        ///     Document type identifier for "$KrTypes_DocTypes_ServiceRecord": {18d01f19-9c21-49ee-a27f-04ffd6ec27eb}.
        /// </summary>
        public readonly Guid KrTypes_DocTypes_ServiceRecordID = new(0x18d01f19,0x9c21,0x49ee,0xa2,0x7f,0x04,0xff,0xd6,0xec,0x27,0xeb);

        /// <summary>
        ///     Document type caption for "$KrTypes_DocTypes_ServiceRecord".
        /// </summary>
        public readonly string KrTypes_DocTypes_ServiceRecordCaption = "$KrTypes_DocTypes_ServiceRecord";

        /// <summary>
        ///     Document type identifier for "$KrTypes_DocTypes_Order": {77fd0adc-24d9-426f-9397-c1ee0d175b93}.
        /// </summary>
        public readonly Guid KrTypes_DocTypes_OrderID = new(0x77fd0adc,0x24d9,0x426f,0x93,0x97,0xc1,0xee,0x0d,0x17,0x5b,0x93);

        /// <summary>
        ///     Document type caption for "$KrTypes_DocTypes_Order".
        /// </summary>
        public readonly string KrTypes_DocTypes_OrderCaption = "$KrTypes_DocTypes_Order";

        #endregion

        #region Forms

        public readonly string FormDocument = "Document";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockBlock4 = "Block4";
        public readonly string BlockDepartmentSignedByRecipientsPerformersBlock = "DepartmentSignedByRecipientsPerformersBlock";
        public readonly string BlockRefsBlock = "RefsBlock";
        public readonly string BlockBlock5 = "Block5";

        #endregion

        #region Controls

        public readonly string IncomingRefsControl = nameof(IncomingRefsControl);
        public readonly string Files = nameof(Files);

        #endregion

        #region ToString

        public static implicit operator string(DocumentTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DocumentCategory

    /// <summary>
    ///     ID: {5da46e89-f932-4a48-b5a3-ec6285bfe3ea}
    ///     Alias: DocumentCategory
    ///     Caption: $CardTypes_TypesNames_Category
    ///     Group: Dictionaries
    /// </summary>
    public class DocumentCategoryTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "DocumentCategory": {5da46e89-f932-4a48-b5a3-ec6285bfe3ea}.
        /// </summary>
        public readonly Guid ID = new Guid(0x5da46e89,0xf932,0x4a48,0xb5,0xa3,0xec,0x62,0x85,0xbf,0xe3,0xea);

        /// <summary>
        ///     Card type name for "DocumentCategory".
        /// </summary>
        public readonly string Alias = "DocumentCategory";

        /// <summary>
        ///     Card type caption for "DocumentCategory".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Category";

        /// <summary>
        ///     Card type group for "DocumentCategory".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormDocumentCategory = "DocumentCategory";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(DocumentCategoryTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DynamicRole

    /// <summary>
    ///     ID: {97a945bc-58f5-07fa-a274-b6a7f0f1282c}
    ///     Alias: DynamicRole
    ///     Caption: $CardTypes_TypesNames_DynamicRole
    ///     Group: Roles
    /// </summary>
    public class DynamicRoleTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "DynamicRole": {97a945bc-58f5-07fa-a274-b6a7f0f1282c}.
        /// </summary>
        public readonly Guid ID = new Guid(0x97a945bc,0x58f5,0x07fa,0xa2,0x74,0xb6,0xa7,0xf0,0xf1,0x28,0x2c);

        /// <summary>
        ///     Card type name for "DynamicRole".
        /// </summary>
        public readonly string Alias = "DynamicRole";

        /// <summary>
        ///     Card type caption for "DynamicRole".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_DynamicRole";

        /// <summary>
        ///     Card type group for "DynamicRole".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region Forms

        public readonly string FormDynamicRole = "DynamicRole";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockUsers = "Users";
        public readonly string BlockErrorInfo = "ErrorInfo";

        #endregion

        #region Controls

        public readonly string TimeZone = nameof(TimeZone);
        public readonly string RecalcButton = nameof(RecalcButton);
        public readonly string RoleUsersLimitDisclaimer = nameof(RoleUsersLimitDisclaimer);

        #endregion

        #region ToString

        public static implicit operator string(DynamicRoleTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region EmptyCondition

    /// <summary>
    ///     ID: {4b68414d-bf1a-4227-bb01-b1fc939d0e5a}
    ///     Alias: EmptyCondition
    ///     Caption: $CardTypes_TypesNames_EmptyCondition
    ///     Group: Conditions
    /// </summary>
    public class EmptyConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "EmptyCondition": {4b68414d-bf1a-4227-bb01-b1fc939d0e5a}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4b68414d,0xbf1a,0x4227,0xbb,0x01,0xb1,0xfc,0x93,0x9d,0x0e,0x5a);

        /// <summary>
        ///     Card type name for "EmptyCondition".
        /// </summary>
        public readonly string Alias = "EmptyCondition";

        /// <summary>
        ///     Card type caption for "EmptyCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_EmptyCondition";

        /// <summary>
        ///     Card type group for "EmptyCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region ToString

        public static implicit operator string(EmptyConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Error

    /// <summary>
    ///     ID: {fa81208d-2d83-4cb6-a83d-cba7e3f483a7}
    ///     Alias: Error
    ///     Caption: $CardTypes_TypesNames_Error
    ///     Group: System
    /// </summary>
    public class ErrorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Error": {fa81208d-2d83-4cb6-a83d-cba7e3f483a7}.
        /// </summary>
        public readonly Guid ID = new Guid(0xfa81208d,0x2d83,0x4cb6,0xa8,0x3d,0xcb,0xa7,0xe3,0xf4,0x83,0xa7);

        /// <summary>
        ///     Card type name for "Error".
        /// </summary>
        public readonly string Alias = "Error";

        /// <summary>
        ///     Card type caption for "Error".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Error";

        /// <summary>
        ///     Card type group for "Error".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormError = "Error";
        public readonly string FormSystemInfo = "SystemInfo";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockShowInWindow = "ShowInWindow";
        public readonly string BlockChangesInfo = "ChangesInfo";
        public readonly string BlockAdditionalDescription = "AdditionalDescription";
        public readonly string BlockBlock5 = "Block5";
        public readonly string BlockSystemInfo = "SystemInfo";

        #endregion

        #region Controls

        public readonly string Category = nameof(Category);
        public readonly string ShowInWindow = nameof(ShowInWindow);
        public readonly string AdditionalDescription = nameof(AdditionalDescription);
        public readonly string Files = nameof(Files);

        #endregion

        #region ToString

        public static implicit operator string(ErrorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FieldChangedCondition

    /// <summary>
    ///     ID: {6077353b-1af6-496f-a930-86954dce122c}
    ///     Alias: FieldChangedCondition
    ///     Caption: $CardTypes_TypesNames_FieldChangedCondition
    ///     Group: Conditions
    /// </summary>
    public class FieldChangedConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "FieldChangedCondition": {6077353b-1af6-496f-a930-86954dce122c}.
        /// </summary>
        public readonly Guid ID = new Guid(0x6077353b,0x1af6,0x496f,0xa9,0x30,0x86,0x95,0x4d,0xce,0x12,0x2c);

        /// <summary>
        ///     Card type name for "FieldChangedCondition".
        /// </summary>
        public readonly string Alias = "FieldChangedCondition";

        /// <summary>
        ///     Card type caption for "FieldChangedCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_FieldChangedCondition";

        /// <summary>
        ///     Card type group for "FieldChangedCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormFieldChangedCondition = "FieldChangedCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(FieldChangedConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region File

    /// <summary>
    ///     ID: {ab387c69-fd62-0655-bbc3-b879e433a143}
    ///     Alias: File
    ///     Caption: $CardTypes_TypesNames_File
    ///     Group: System
    /// </summary>
    public class FileTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "File": {ab387c69-fd62-0655-bbc3-b879e433a143}.
        /// </summary>
        public readonly Guid ID = new Guid(0xab387c69,0xfd62,0x0655,0xbb,0xc3,0xb8,0x79,0xe4,0x33,0xa1,0x43);

        /// <summary>
        ///     Card type name for "File".
        /// </summary>
        public readonly string Alias = "File";

        /// <summary>
        ///     Card type caption for "File".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_File";

        /// <summary>
        ///     Card type group for "File".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region ToString

        public static implicit operator string(FileTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FileCategory

    /// <summary>
    ///     ID: {97182afc-43ce-4bd9-9c96-73a4d2c5d5eb}
    ///     Alias: FileCategory
    ///     Caption: $CardTypes_TypesNames_FileCategory
    ///     Group: Dictionaries
    /// </summary>
    public class FileCategoryTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "FileCategory": {97182afc-43ce-4bd9-9c96-73a4d2c5d5eb}.
        /// </summary>
        public readonly Guid ID = new Guid(0x97182afc,0x43ce,0x4bd9,0x9c,0x96,0x73,0xa4,0xd2,0xc5,0xd5,0xeb);

        /// <summary>
        ///     Card type name for "FileCategory".
        /// </summary>
        public readonly string Alias = "FileCategory";

        /// <summary>
        ///     Card type caption for "FileCategory".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_FileCategory";

        /// <summary>
        ///     Card type group for "FileCategory".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormFileCategory = "FileCategory";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";

        #endregion

        #region ToString

        public static implicit operator string(FileCategoryTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FileConverterCache

    /// <summary>
    ///     ID: {7609d1d7-9a46-4617-8789-2dff55aa4072}
    ///     Alias: FileConverterCache
    ///     Caption: $CardTypes_TypesNames_FileConverterCache
    ///     Group: Settings
    /// </summary>
    public class FileConverterCacheTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "FileConverterCache": {7609d1d7-9a46-4617-8789-2dff55aa4072}.
        /// </summary>
        public readonly Guid ID = new Guid(0x7609d1d7,0x9a46,0x4617,0x87,0x89,0x2d,0xff,0x55,0xaa,0x40,0x72);

        /// <summary>
        ///     Card type name for "FileConverterCache".
        /// </summary>
        public readonly string Alias = "FileConverterCache";

        /// <summary>
        ///     Card type caption for "FileConverterCache".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_FileConverterCache";

        /// <summary>
        ///     Card type group for "FileConverterCache".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormFileConverterCache = "FileConverterCache";

        #endregion

        #region Blocks

        public readonly string BlockMainInformation = "MainInformation";
        public readonly string BlockCacheMaintenance = "CacheMaintenance";

        #endregion

        #region Controls

        public readonly string RemoveOldFiles = nameof(RemoveOldFiles);
        public readonly string RemoveAllFiles = nameof(RemoveAllFiles);

        #endregion

        #region ToString

        public static implicit operator string(FileConverterCacheTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FilePreviewDialog

    /// <summary>
    ///     ID: {1d196c65-f645-4fea-b515-c0d615dbb867}
    ///     Alias: FilePreviewDialog
    ///     Caption: FilePreviewDialog
    ///     Group: Forums
    /// </summary>
    public class FilePreviewDialogTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "FilePreviewDialog": {1d196c65-f645-4fea-b515-c0d615dbb867}.
        /// </summary>
        public readonly Guid ID = new Guid(0x1d196c65,0xf645,0x4fea,0xb5,0x15,0xc0,0xd6,0x15,0xdb,0xb8,0x67);

        /// <summary>
        ///     Card type name for "FilePreviewDialog".
        /// </summary>
        public readonly string Alias = "FilePreviewDialog";

        /// <summary>
        ///     Card type caption for "FilePreviewDialog".
        /// </summary>
        public readonly string Caption = "FilePreviewDialog";

        /// <summary>
        ///     Card type group for "FilePreviewDialog".
        /// </summary>
        public readonly string Group = "Forums";

        #endregion

        #region Forms

        public readonly string FormPreviewTab = "PreviewTab";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "PreviewFile" for "PreviewFile".
        /// </summary>
        public readonly string BlockPreviewFile = "PreviewFile";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "PreviewFile" for "PreviewFile".
        /// </summary>
        public readonly string PreviewFile = nameof(PreviewFile);

        #endregion

        #region ToString

        public static implicit operator string(FilePreviewDialogTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FileSatellite

    /// <summary>
    ///     ID: {580cd334-6630-420e-acd8-9524be99e43e}
    ///     Alias: FileSatellite
    ///     Caption: $CardTypes_TypesNames_FileSatellite
    ///     Group: System
    /// </summary>
    public class FileSatelliteTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "FileSatellite": {580cd334-6630-420e-acd8-9524be99e43e}.
        /// </summary>
        public readonly Guid ID = new Guid(0x580cd334,0x6630,0x420e,0xac,0xd8,0x95,0x24,0xbe,0x99,0xe4,0x3e);

        /// <summary>
        ///     Card type name for "FileSatellite".
        /// </summary>
        public readonly string Alias = "FileSatellite";

        /// <summary>
        ///     Card type caption for "FileSatellite".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_FileSatellite";

        /// <summary>
        ///     Card type group for "FileSatellite".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region ToString

        public static implicit operator string(FileSatelliteTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FileTemplate

    /// <summary>
    ///     ID: {b7e1b93e-eeda-49b7-9402-2471d4d14bdf}
    ///     Alias: FileTemplate
    ///     Caption: $CardTypes_TypesNames_FileTemplate
    ///     Group: Dictionaries
    /// </summary>
    public class FileTemplateTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "FileTemplate": {b7e1b93e-eeda-49b7-9402-2471d4d14bdf}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb7e1b93e,0xeeda,0x49b7,0x94,0x02,0x24,0x71,0xd4,0xd1,0x4b,0xdf);

        /// <summary>
        ///     Card type name for "FileTemplate".
        /// </summary>
        public readonly string Alias = "FileTemplate";

        /// <summary>
        ///     Card type caption for "FileTemplate".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_FileTemplate";

        /// <summary>
        ///     Card type group for "FileTemplate".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormFileTemplate = "FileTemplate";
        public readonly string FormTab = "Tab";

        #endregion

        #region Blocks

        public readonly string BlockMainInfoBlock = "MainInfoBlock";
        public readonly string BlockCardTypesBlock = "CardTypesBlock";
        public readonly string BlockViewsBlock = "ViewsBlock";
        public readonly string BlockBlock5 = "Block5";
        public readonly string BlockBlock2 = "Block2";

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string CardTypesControl = nameof(CardTypesControl);
        public readonly string ViewsControl = nameof(ViewsControl);
        public readonly string ConvertToPDF = nameof(ConvertToPDF);
        public readonly string CompileButton = nameof(CompileButton);
        public readonly string Files = nameof(Files);
        public readonly string CompileExtensionsButton = nameof(CompileExtensionsButton);
        public readonly string CompileAllButton = nameof(CompileAllButton);
        public readonly string Extensions = nameof(Extensions);

        #endregion

        #region ToString

        public static implicit operator string(FileTemplateTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FormatSettings

    /// <summary>
    ///     ID: {568141c1-e258-487d-85c6-8aaeddf387fc}
    ///     Alias: FormatSettings
    ///     Caption: $CardTypes_TypesNames_FormatSettings
    ///     Group: Settings
    /// </summary>
    public class FormatSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "FormatSettings": {568141c1-e258-487d-85c6-8aaeddf387fc}.
        /// </summary>
        public readonly Guid ID = new Guid(0x568141c1,0xe258,0x487d,0x85,0xc6,0x8a,0xae,0xdd,0xf3,0x87,0xfc);

        /// <summary>
        ///     Card type name for "FormatSettings".
        /// </summary>
        public readonly string Alias = "FormatSettings";

        /// <summary>
        ///     Card type caption for "FormatSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_FormatSettings";

        /// <summary>
        ///     Card type group for "FormatSettings".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormFormatSettings = "FormatSettings";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockDateTimeInfo = "DateTimeInfo";
        public readonly string BlockNumberInfo = "NumberInfo";

        #endregion

        #region Controls

        public readonly string UpdateFromCultureInfo = nameof(UpdateFromCultureInfo);

        #endregion

        #region ToString

        public static implicit operator string(FormatSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ForumSatellite

    /// <summary>
    ///     ID: {48e7c07a-295d-479a-9990-02a1f7a5f7db}
    ///     Alias: ForumSatellite
    ///     Caption: ForumSatellite
    ///     Group: Forums
    /// </summary>
    public class ForumSatelliteTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "ForumSatellite": {48e7c07a-295d-479a-9990-02a1f7a5f7db}.
        /// </summary>
        public readonly Guid ID = new Guid(0x48e7c07a,0x295d,0x479a,0x99,0x90,0x02,0xa1,0xf7,0xa5,0xf7,0xdb);

        /// <summary>
        ///     Card type name for "ForumSatellite".
        /// </summary>
        public readonly string Alias = "ForumSatellite";

        /// <summary>
        ///     Card type caption for "ForumSatellite".
        /// </summary>
        public readonly string Caption = "ForumSatellite";

        /// <summary>
        ///     Card type group for "ForumSatellite".
        /// </summary>
        public readonly string Group = "Forums";

        #endregion

        #region ToString

        public static implicit operator string(ForumSatelliteTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FunctionRole

    /// <summary>
    ///     ID: {a830094d-6e03-4242-9c17-0d0a8f2fcb33}
    ///     Alias: FunctionRole
    ///     Caption: $CardTypes_TypesNames_FunctionRole
    ///     Group: System
    /// </summary>
    public class FunctionRoleTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "FunctionRole": {a830094d-6e03-4242-9c17-0d0a8f2fcb33}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa830094d,0x6e03,0x4242,0x9c,0x17,0x0d,0x0a,0x8f,0x2f,0xcb,0x33);

        /// <summary>
        ///     Card type name for "FunctionRole".
        /// </summary>
        public readonly string Alias = "FunctionRole";

        /// <summary>
        ///     Card type caption for "FunctionRole".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_FunctionRole";

        /// <summary>
        ///     Card type group for "FunctionRole".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormFunctionRole = "FunctionRole";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";

        #endregion

        #region Controls

        public readonly string CanBeDeputy = nameof(CanBeDeputy);
        public readonly string CanTakeInProgress = nameof(CanTakeInProgress);
        public readonly string HideTaskByDefault = nameof(HideTaskByDefault);
        public readonly string CanChangeTaskInfo = nameof(CanChangeTaskInfo);

        #endregion

        #region ToString

        public static implicit operator string(FunctionRoleTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region GeneralUserSettings

    /// <summary>
    ///     ID: {39dce9d4-f429-4552-a268-5a9bf9c1ba53}
    ///     Alias: GeneralUserSettings
    ///     Caption: $CardTypes_Blocks_GeneralSettings
    ///     Group: UserSettings
    /// </summary>
    public class GeneralUserSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "GeneralUserSettings": {39dce9d4-f429-4552-a268-5a9bf9c1ba53}.
        /// </summary>
        public readonly Guid ID = new Guid(0x39dce9d4,0xf429,0x4552,0xa2,0x68,0x5a,0x9b,0xf9,0xc1,0xba,0x53);

        /// <summary>
        ///     Card type name for "GeneralUserSettings".
        /// </summary>
        public readonly string Alias = "GeneralUserSettings";

        /// <summary>
        ///     Card type caption for "GeneralUserSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_Blocks_GeneralSettings";

        /// <summary>
        ///     Card type group for "GeneralUserSettings".
        /// </summary>
        public readonly string Group = "UserSettings";

        #endregion

        #region Forms

        public readonly string FormMySettings = "MySettings";

        #endregion

        #region Blocks

        public readonly string BlockGeneralSettings = "GeneralSettings";
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(GeneralUserSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region HelpSection

    /// <summary>
    ///     ID: {3442db47-ab96-4d9c-870f-535e1fc8d42f}
    ///     Alias: HelpSection
    ///     Caption: $Cards_HelpSection
    ///     Group: System
    /// </summary>
    public class HelpSectionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "HelpSection": {3442db47-ab96-4d9c-870f-535e1fc8d42f}.
        /// </summary>
        public readonly Guid ID = new Guid(0x3442db47,0xab96,0x4d9c,0x87,0x0f,0x53,0x5e,0x1f,0xc8,0xd4,0x2f);

        /// <summary>
        ///     Card type name for "HelpSection".
        /// </summary>
        public readonly string Alias = "HelpSection";

        /// <summary>
        ///     Card type caption for "HelpSection".
        /// </summary>
        public readonly string Caption = "$Cards_HelpSection";

        /// <summary>
        ///     Card type group for "HelpSection".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormHelpSection = "HelpSection";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        /// <summary>
        ///     Block caption "Block2" for "Block2".
        /// </summary>
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        public readonly string Code = nameof(Code);
        public readonly string Name = nameof(Name);
        public readonly string RichText = nameof(RichText);

        #endregion

        #region ToString

        public static implicit operator string(HelpSectionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region HelpSectionDialogs

    /// <summary>
    ///     ID: {dc32f94b-82fb-48a1-8c44-79c5240d5f22}
    ///     Alias: HelpSectionDialogs
    ///     Caption: $Cards_DefaultCaption
    ///     Group: System
    /// </summary>
    public class HelpSectionDialogsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "HelpSectionDialogs": {dc32f94b-82fb-48a1-8c44-79c5240d5f22}.
        /// </summary>
        public readonly Guid ID = new Guid(0xdc32f94b,0x82fb,0x48a1,0x8c,0x44,0x79,0xc5,0x24,0x0d,0x5f,0x22);

        /// <summary>
        ///     Card type name for "HelpSectionDialogs".
        /// </summary>
        public readonly string Alias = "HelpSectionDialogs";

        /// <summary>
        ///     Card type caption for "HelpSectionDialogs".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "HelpSectionDialogs".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormMainTab = "MainTab";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        /// <summary>
        ///     Block caption "Block2" for "Block2".
        /// </summary>
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        public readonly string Code = nameof(Code);
        public readonly string Name = nameof(Name);
        public readonly string RichText = nameof(RichText);

        #endregion

        #region ToString

        public static implicit operator string(HelpSectionDialogsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Incoming

    /// <summary>
    ///     ID: {001f99fd-5bf3-0679-9b6f-455767af72b5}
    ///     Alias: Incoming
    ///     Caption: $CardTypes_TypesNames_Incoming
    ///     Group: Documents
    /// </summary>
    public class IncomingTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Incoming": {001f99fd-5bf3-0679-9b6f-455767af72b5}.
        /// </summary>
        public readonly Guid ID = new Guid(0x001f99fd,0x5bf3,0x0679,0x9b,0x6f,0x45,0x57,0x67,0xaf,0x72,0xb5);

        /// <summary>
        ///     Card type name for "Incoming".
        /// </summary>
        public readonly string Alias = "Incoming";

        /// <summary>
        ///     Card type caption for "Incoming".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Incoming";

        /// <summary>
        ///     Card type group for "Incoming".
        /// </summary>
        public readonly string Group = "Documents";

        #endregion

        #region Document Types

        /// <summary>
        ///     Document type identifier for "$KrTypes_DocTypes_Incoming": {93806539-4931-47ad-9d68-7387156c417f}.
        /// </summary>
        public readonly Guid KrTypes_DocTypes_IncomingID = new(0x93806539,0x4931,0x47ad,0x9d,0x68,0x73,0x87,0x15,0x6c,0x41,0x7f);

        /// <summary>
        ///     Document type caption for "$KrTypes_DocTypes_Incoming".
        /// </summary>
        public readonly string KrTypes_DocTypes_IncomingCaption = "$KrTypes_DocTypes_Incoming";

        #endregion

        #region Forms

        public readonly string FormIncoming = "Incoming";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockDepartmentSignedByRecipientsPerformersBlock = "DepartmentSignedByRecipientsPerformersBlock";
        public readonly string BlockRefsBlock = "RefsBlock";
        public readonly string BlockBlock5 = "Block5";

        #endregion

        #region Controls

        public readonly string PartnerControl = nameof(PartnerControl);
        public readonly string IncomingRefsControl = nameof(IncomingRefsControl);
        public readonly string Files = nameof(Files);

        #endregion

        #region ToString

        public static implicit operator string(IncomingTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region InitiatorCondition

    /// <summary>
    ///     ID: {8ff1a133-0300-4246-b117-a5455487acc1}
    ///     Alias: InitiatorCondition
    ///     Caption: $CardTypes_TypesNames_InitiatorCondition
    ///     Group: Conditions
    /// </summary>
    public class InitiatorConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "InitiatorCondition": {8ff1a133-0300-4246-b117-a5455487acc1}.
        /// </summary>
        public readonly Guid ID = new Guid(0x8ff1a133,0x0300,0x4246,0xb1,0x17,0xa5,0x45,0x54,0x87,0xac,0xc1);

        /// <summary>
        ///     Card type name for "InitiatorCondition".
        /// </summary>
        public readonly string Alias = "InitiatorCondition";

        /// <summary>
        ///     Card type caption for "InitiatorCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_InitiatorCondition";

        /// <summary>
        ///     Card type group for "InitiatorCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormInitiatorCondition = "InitiatorCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(InitiatorConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrAcquaintanceAction

    /// <summary>
    ///     ID: {956a34eb-8318-4d35-92a2-c0df118c01ea}
    ///     Alias: KrAcquaintanceAction
    ///     Caption: $KrActions_Acquaintance
    ///     Group: KrProcess
    /// </summary>
    public class KrAcquaintanceActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrAcquaintanceAction": {956a34eb-8318-4d35-92a2-c0df118c01ea}.
        /// </summary>
        public readonly Guid ID = new Guid(0x956a34eb,0x8318,0x4d35,0x92,0xa2,0xc0,0xdf,0x11,0x8c,0x01,0xea);

        /// <summary>
        ///     Card type name for "KrAcquaintanceAction".
        /// </summary>
        public readonly string Alias = "KrAcquaintanceAction";

        /// <summary>
        ///     Card type caption for "KrAcquaintanceAction".
        /// </summary>
        public readonly string Caption = "$KrActions_Acquaintance";

        /// <summary>
        ///     Card type group for "KrAcquaintanceAction".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrAcquaintanceAction = "KrAcquaintanceAction";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";

        #endregion

        #region ToString

        public static implicit operator string(KrAcquaintanceActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrAcquaintanceStageTypeSettings

    /// <summary>
    ///     ID: {728382fe-12b2-444b-b62e-fe4a4d5ac65f}
    ///     Alias: KrAcquaintanceStageTypeSettings
    ///     Caption: $KrStages_Acquaintance
    ///     Group: KrProcess
    /// </summary>
    public class KrAcquaintanceStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrAcquaintanceStageTypeSettings": {728382fe-12b2-444b-b62e-fe4a4d5ac65f}.
        /// </summary>
        public readonly Guid ID = new Guid(0x728382fe,0x12b2,0x444b,0xb6,0x2e,0xfe,0x4a,0x4d,0x5a,0xc6,0x5f);

        /// <summary>
        ///     Card type name for "KrAcquaintanceStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrAcquaintanceStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrAcquaintanceStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_Acquaintance";

        /// <summary>
        ///     Card type group for "KrAcquaintanceStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrAcquaintanceStageTypeSettings = "KrAcquaintanceStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";

        #endregion

        #region ToString

        public static implicit operator string(KrAcquaintanceStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrAddFileFromTemplateStageTypeSettings

    /// <summary>
    ///     ID: {b196cbd5-a534-4d18-91f9-561f31a2fe89}
    ///     Alias: KrAddFileFromTemplateStageTypeSettings
    ///     Caption: $KrStages_AddFromTemplate
    ///     Group: KrProcess
    /// </summary>
    public class KrAddFileFromTemplateStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrAddFileFromTemplateStageTypeSettings": {b196cbd5-a534-4d18-91f9-561f31a2fe89}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb196cbd5,0xa534,0x4d18,0x91,0xf9,0x56,0x1f,0x31,0xa2,0xfe,0x89);

        /// <summary>
        ///     Card type name for "KrAddFileFromTemplateStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrAddFileFromTemplateStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrAddFileFromTemplateStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_AddFromTemplate";

        /// <summary>
        ///     Card type group for "KrAddFileFromTemplateStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrAddFileFromTemplateStageTypeSettings = "KrAddFileFromTemplateStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(KrAddFileFromTemplateStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrAdditionalApproval

    /// <summary>
    ///     ID: {b3d8eae3-c6bf-4b59-bcc7-461d526c326c}
    ///     Alias: KrAdditionalApproval
    ///     Caption: $CardTypes_TypesNames_KrAdditionalApproval
    ///     Group: KrProcess
    /// </summary>
    public class KrAdditionalApprovalTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrAdditionalApproval": {b3d8eae3-c6bf-4b59-bcc7-461d526c326c}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb3d8eae3,0xc6bf,0x4b59,0xbc,0xc7,0x46,0x1d,0x52,0x6c,0x32,0x6c);

        /// <summary>
        ///     Card type name for "KrAdditionalApproval".
        /// </summary>
        public readonly string Alias = "KrAdditionalApproval";

        /// <summary>
        ///     Card type caption for "KrAdditionalApproval".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrAdditionalApproval";

        /// <summary>
        ///     Card type group for "KrAdditionalApproval".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrAdditionalApproval = "KrAdditionalApproval";
        public readonly string FormRequestComment = "RequestComment";
        public readonly string FormRejectForm = "RejectForm";
        public readonly string FormApprove = "Approve";
        public readonly string FormDisapprove = "Disapprove";
        public readonly string FormAdditionalApproval = "AdditionalApproval";

        #endregion

        #region Blocks

        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockCommentBlock = "CommentBlock";
        public readonly string BlockCommentsBlockShort = "CommentsBlockShort";
        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockAdditionalApprovalBlockShort = "AdditionalApprovalBlockShort";

        #endregion

        #region Controls

        public readonly string KrCommentsTable = nameof(KrCommentsTable);
        public readonly string KrAdditionalApprovalTable = nameof(KrAdditionalApprovalTable);
        public readonly string AdditionalApprovalsRequestedInfoTable = nameof(AdditionalApprovalsRequestedInfoTable);

        #endregion

        #region ToString

        public static implicit operator string(KrAdditionalApprovalTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrAmendingAction

    /// <summary>
    ///     ID: {9c530e93-ec3a-48ba-b09c-ee9eceb2173e}
    ///     Alias: KrAmendingAction
    ///     Caption: $KrActions_Amending
    ///     Group: WorkflowActions
    /// </summary>
    public class KrAmendingActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrAmendingAction": {9c530e93-ec3a-48ba-b09c-ee9eceb2173e}.
        /// </summary>
        public readonly Guid ID = new Guid(0x9c530e93,0xec3a,0x48ba,0xb0,0x9c,0xee,0x9e,0xce,0xb2,0x17,0x3e);

        /// <summary>
        ///     Card type name for "KrAmendingAction".
        /// </summary>
        public readonly string Alias = "KrAmendingAction";

        /// <summary>
        ///     Card type caption for "KrAmendingAction".
        /// </summary>
        public readonly string Caption = "$KrActions_Amending";

        /// <summary>
        ///     Card type group for "KrAmendingAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormKrAmendingAction = "KrAmendingAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        public readonly string ActionEventsTable = nameof(ActionEventsTable);

        #endregion

        #region ToString

        public static implicit operator string(KrAmendingActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrApprovalAction

    /// <summary>
    ///     ID: {70762c81-bd23-4580-a3fb-c452604f6e78}
    ///     Alias: KrApprovalAction
    ///     Caption: $KrActions_Approval
    ///     Group: WorkflowActions
    /// </summary>
    public class KrApprovalActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrApprovalAction": {70762c81-bd23-4580-a3fb-c452604f6e78}.
        /// </summary>
        public readonly Guid ID = new Guid(0x70762c81,0xbd23,0x4580,0xa3,0xfb,0xc4,0x52,0x60,0x4f,0x6e,0x78);

        /// <summary>
        ///     Card type name for "KrApprovalAction".
        /// </summary>
        public readonly string Alias = "KrApprovalAction";

        /// <summary>
        ///     Card type caption for "KrApprovalAction".
        /// </summary>
        public readonly string Caption = "$KrActions_Approval";

        /// <summary>
        ///     Card type group for "KrApprovalAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormKrApprovalAction = "KrApprovalAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockAdditionalApprovalBlock = "AdditionalApprovalBlock";
        public readonly string BlockKrApprovalAction_CommonSettings = "KrApprovalAction_CommonSettings";
        public readonly string BlockKrApprovalAction_StageFlags = "KrApprovalAction_StageFlags";
        public readonly string BlockKrApprovalAction_AdditionalSettings = "KrApprovalAction_AdditionalSettings";
        public readonly string BlockDisclaimerBlock = "DisclaimerBlock";
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockBlock4 = "Block4";

        #endregion

        #region Controls

        public readonly string AddComputedRoleLink = nameof(AddComputedRoleLink);
        public readonly string AdditionalApprovers = nameof(AdditionalApprovers);
        public readonly string AdditionalApprovalContainer = nameof(AdditionalApprovalContainer);
        public readonly string FlagsTabs = nameof(FlagsTabs);
        public readonly string CompletionOptionsTable = nameof(CompletionOptionsTable);
        public readonly string ActionCompletionOptionsTable = nameof(ActionCompletionOptionsTable);
        public readonly string ActionEventsTable = nameof(ActionEventsTable);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrApprovalStageTypeSettings

    /// <summary>
    ///     ID: {4a377758-2366-47e9-98ac-c5f553974236}
    ///     Alias: KrApprovalStageTypeSettings
    ///     Caption: $KrStages_Approval
    ///     Group: KrProcess
    /// </summary>
    public class KrApprovalStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrApprovalStageTypeSettings": {4a377758-2366-47e9-98ac-c5f553974236}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4a377758,0x2366,0x47e9,0x98,0xac,0xc5,0xf5,0x53,0x97,0x42,0x36);

        /// <summary>
        ///     Card type name for "KrApprovalStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrApprovalStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrApprovalStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_Approval";

        /// <summary>
        ///     Card type group for "KrApprovalStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrApprovalStageTypeSettings = "KrApprovalStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockAdditionalApprovalBlock = "AdditionalApprovalBlock";
        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockCommonSettings = "CommonSettings";
        public readonly string BlockStageFlags = "StageFlags";
        public readonly string BlockAdditionalSettings = "AdditionalSettings";
        public readonly string BlockDisclaimerBlock = "DisclaimerBlock";
        public readonly string BlockApprovalStageFlags = "ApprovalStageFlags";

        #endregion

        #region Controls

        public readonly string AdditionalApprovers = nameof(AdditionalApprovers);
        public readonly string IsParallelFlag = nameof(IsParallelFlag);
        public readonly string ReturnIfNotApproved = nameof(ReturnIfNotApproved);
        public readonly string ReturnAfterApproval = nameof(ReturnAfterApproval);
        public readonly string DisclaimerControl = nameof(DisclaimerControl);
        public readonly string EditCardFlagControl = nameof(EditCardFlagControl);
        public readonly string EditFilesFlagControl = nameof(EditFilesFlagControl);
        public readonly string FlagsTabs = nameof(FlagsTabs);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrApprove

    /// <summary>
    ///     ID: {e4d7f6bf-fea9-4a3b-8a5a-e1a0a40de74c}
    ///     Alias: KrApprove
    ///     Caption: $CardTypes_TypesNames_KrApprove
    ///     Group: KrProcess
    /// </summary>
    public class KrApproveTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrApprove": {e4d7f6bf-fea9-4a3b-8a5a-e1a0a40de74c}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe4d7f6bf,0xfea9,0x4a3b,0x8a,0x5a,0xe1,0xa0,0xa4,0x0d,0xe7,0x4c);

        /// <summary>
        ///     Card type name for "KrApprove".
        /// </summary>
        public readonly string Alias = "KrApprove";

        /// <summary>
        ///     Card type caption for "KrApprove".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrApprove";

        /// <summary>
        ///     Card type group for "KrApprove".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrApprove = "KrApprove";
        public readonly string FormDelegate = "Delegate";
        public readonly string FormRequestComment = "RequestComment";
        public readonly string FormApprove = "Approve";
        public readonly string FormDisapprove = "Disapprove";
        public readonly string FormAdditionalApproval = "AdditionalApproval";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockCommentBlock = "CommentBlock";
        public readonly string BlockCommentsBlockShort = "CommentsBlockShort";
        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockAdditionalApprovalBlockShort = "AdditionalApprovalBlockShort";
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        public readonly string KrCommentsTable = nameof(KrCommentsTable);
        public readonly string KrAdditionalApprovalTable = nameof(KrAdditionalApprovalTable);
        public readonly string WithControl = nameof(WithControl);

        #endregion

        #region ToString

        public static implicit operator string(KrApproveTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrAuthorSettings

    /// <summary>
    ///     ID: {02dbc094-7f2c-48b0-acf3-1fc6dddf015c}
    ///     Alias: KrAuthorSettings
    ///     Caption: KrAuthorSettings
    ///     Group: KrProcess
    /// </summary>
    public class KrAuthorSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrAuthorSettings": {02dbc094-7f2c-48b0-acf3-1fc6dddf015c}.
        /// </summary>
        public readonly Guid ID = new Guid(0x02dbc094,0x7f2c,0x48b0,0xac,0xf3,0x1f,0xc6,0xdd,0xdf,0x01,0x5c);

        /// <summary>
        ///     Card type name for "KrAuthorSettings".
        /// </summary>
        public readonly string Alias = "KrAuthorSettings";

        /// <summary>
        ///     Card type caption for "KrAuthorSettings".
        /// </summary>
        public readonly string Caption = "KrAuthorSettings";

        /// <summary>
        ///     Card type group for "KrAuthorSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrAuthorSettings = "KrAuthorSettings";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "AuthorBlock" for "AuthorBlock".
        /// </summary>
        public readonly string BlockAuthorBlock = "AuthorBlock";

        #endregion

        #region ToString

        public static implicit operator string(KrAuthorSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrCard

    /// <summary>
    ///     ID: {21bca3fc-f75f-413b-b5c8-49538cbfc761}
    ///     Alias: KrCard
    ///     Caption: $CardTypes_TypesNames_KrCard
    ///     Group: KrProcess
    /// </summary>
    public class KrCardTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrCard": {21bca3fc-f75f-413b-b5c8-49538cbfc761}.
        /// </summary>
        public readonly Guid ID = new Guid(0x21bca3fc,0xf75f,0x413b,0xb5,0xc8,0x49,0x53,0x8c,0xbf,0xc7,0x61);

        /// <summary>
        ///     Card type name for "KrCard".
        /// </summary>
        public readonly string Alias = "KrCard";

        /// <summary>
        ///     Card type caption for "KrCard".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrCard";

        /// <summary>
        ///     Card type group for "KrCard".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormApprovalProcess = "ApprovalProcess";

        #endregion

        #region Blocks

        public readonly string BlockSummaryBlock = "SummaryBlock";
        public readonly string BlockStageCommonInfoBlock = "StageCommonInfoBlock";
        public readonly string BlockApprovalStagesBlock = "ApprovalStagesBlock";
        public readonly string BlockDisclaimerBlock = "DisclaimerBlock";

        #endregion

        #region Controls

        public readonly string DocStateControl = nameof(DocStateControl);
        public readonly string DocStateChangedControl = nameof(DocStateChangedControl);
        public readonly string TimeLimitInput = nameof(TimeLimitInput);
        public readonly string PlannedInput = nameof(PlannedInput);
        public readonly string ApprovalStagesTable = nameof(ApprovalStagesTable);
        public readonly string DisclaimerControl = nameof(DisclaimerControl);

        #endregion

        #region ToString

        public static implicit operator string(KrCardTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrChangeStateAction

    /// <summary>
    ///     ID: {4f07209c-ab6b-44f6-9460-f594c3bdf8a3}
    ///     Alias: KrChangeStateAction
    ///     Caption: $KrActions_ChangeState
    ///     Group: WorkflowActions
    /// </summary>
    public class KrChangeStateActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrChangeStateAction": {4f07209c-ab6b-44f6-9460-f594c3bdf8a3}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4f07209c,0xab6b,0x44f6,0x94,0x60,0xf5,0x94,0xc3,0xbd,0xf8,0xa3);

        /// <summary>
        ///     Card type name for "KrChangeStateAction".
        /// </summary>
        public readonly string Alias = "KrChangeStateAction";

        /// <summary>
        ///     Card type caption for "KrChangeStateAction".
        /// </summary>
        public readonly string Caption = "$KrActions_ChangeState";

        /// <summary>
        ///     Card type group for "KrChangeStateAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormKrChangeStateAction = "KrChangeStateAction";

        #endregion

        #region Blocks

        public readonly string BlockMainBlock = "MainBlock";

        #endregion

        #region ToString

        public static implicit operator string(KrChangeStateActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrChangeStateStageTypeSettings

    /// <summary>
    ///     ID: {784388f6-dad3-4ce2-a8b9-49e73d71784c}
    ///     Alias: KrChangeStateStageTypeSettings
    ///     Caption: $KrStages_ChangeState
    ///     Group: KrProcess
    /// </summary>
    public class KrChangeStateStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrChangeStateStageTypeSettings": {784388f6-dad3-4ce2-a8b9-49e73d71784c}.
        /// </summary>
        public readonly Guid ID = new Guid(0x784388f6,0xdad3,0x4ce2,0xa8,0xb9,0x49,0xe7,0x3d,0x71,0x78,0x4c);

        /// <summary>
        ///     Card type name for "KrChangeStateStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrChangeStateStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrChangeStateStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_ChangeState";

        /// <summary>
        ///     Card type group for "KrChangeStateStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrChangeStateStageTypeSettings = "KrChangeStateStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";

        #endregion

        #region ToString

        public static implicit operator string(KrChangeStateStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrCheckStateWorkflowTileExtension

    /// <summary>
    ///     ID: {9fce6311-1746-412a-9346-77394caebe90}
    ///     Alias: KrCheckStateWorkflowTileExtension
    ///     Caption: $Cards_DefaultCaption
    ///     Group: KrProcess
    /// </summary>
    public class KrCheckStateWorkflowTileExtensionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrCheckStateWorkflowTileExtension": {9fce6311-1746-412a-9346-77394caebe90}.
        /// </summary>
        public readonly Guid ID = new Guid(0x9fce6311,0x1746,0x412a,0x93,0x46,0x77,0x39,0x4c,0xae,0xbe,0x90);

        /// <summary>
        ///     Card type name for "KrCheckStateWorkflowTileExtension".
        /// </summary>
        public readonly string Alias = "KrCheckStateWorkflowTileExtension";

        /// <summary>
        ///     Card type caption for "KrCheckStateWorkflowTileExtension".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "KrCheckStateWorkflowTileExtension".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrCheckStateWorkflowTileExtension = "KrCheckStateWorkflowTileExtension";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(KrCheckStateWorkflowTileExtensionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrCreateCardStageTypeSettings

    /// <summary>
    ///     ID: {d444f8d4-be81-4714-b00d-02172fad1c81}
    ///     Alias: KrCreateCardStageTypeSettings
    ///     Caption: $KrStages_CreateCard
    ///     Group: KrProcess
    /// </summary>
    public class KrCreateCardStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrCreateCardStageTypeSettings": {d444f8d4-be81-4714-b00d-02172fad1c81}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd444f8d4,0xbe81,0x4714,0xb0,0x0d,0x02,0x17,0x2f,0xad,0x1c,0x81);

        /// <summary>
        ///     Card type name for "KrCreateCardStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrCreateCardStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrCreateCardStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_CreateCard";

        /// <summary>
        ///     Card type group for "KrCreateCardStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrCreateCardStageTypeSettings = "KrCreateCardStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockCreateCardStageTypeSettings = "CreateCardStageTypeSettings";

        #endregion

        #region ToString

        public static implicit operator string(KrCreateCardStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrDeregistrationAction

    /// <summary>
    ///     ID: {94e91c8c-1336-4c04-87c5-11ceb9839de3}
    ///     Alias: KrDeregistrationAction
    ///     Caption: $KrActions_Deregistration
    ///     Group: WorkflowActions
    /// </summary>
    public class KrDeregistrationActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrDeregistrationAction": {94e91c8c-1336-4c04-87c5-11ceb9839de3}.
        /// </summary>
        public readonly Guid ID = new Guid(0x94e91c8c,0x1336,0x4c04,0x87,0xc5,0x11,0xce,0xb9,0x83,0x9d,0xe3);

        /// <summary>
        ///     Card type name for "KrDeregistrationAction".
        /// </summary>
        public readonly string Alias = "KrDeregistrationAction";

        /// <summary>
        ///     Card type caption for "KrDeregistrationAction".
        /// </summary>
        public readonly string Caption = "$KrActions_Deregistration";

        /// <summary>
        ///     Card type group for "KrDeregistrationAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region ToString

        public static implicit operator string(KrDeregistrationActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrDialogStageTypeSettings

    /// <summary>
    ///     ID: {71464f65-e572-4fba-b54f-3e9f9ef0125a}
    ///     Alias: KrDialogStageTypeSettings
    ///     Caption: KrDialogStageTypeSettings
    ///     Group: KrProcess
    /// </summary>
    public class KrDialogStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrDialogStageTypeSettings": {71464f65-e572-4fba-b54f-3e9f9ef0125a}.
        /// </summary>
        public readonly Guid ID = new Guid(0x71464f65,0xe572,0x4fba,0xb5,0x4f,0x3e,0x9f,0x9e,0xf0,0x12,0x5a);

        /// <summary>
        ///     Card type name for "KrDialogStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrDialogStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrDialogStageTypeSettings".
        /// </summary>
        public readonly string Caption = "KrDialogStageTypeSettings";

        /// <summary>
        ///     Card type group for "KrDialogStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrDialogStageTypeSettings = "KrDialogStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockKrDialogScriptsBlock = "KrDialogScriptsBlock";

        #endregion

        #region ToString

        public static implicit operator string(KrDialogStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrDocState

    /// <summary>
    ///     ID: {e83a230a-f5fc-445e-9b44-7d0140ee69f6}
    ///     Alias: KrDocState
    ///     Caption: $CardTypes_TypesNames_KrDocState
    ///     Group: Dictionaries
    /// </summary>
    public class KrDocStateTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrDocState": {e83a230a-f5fc-445e-9b44-7d0140ee69f6}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe83a230a,0xf5fc,0x445e,0x9b,0x44,0x7d,0x01,0x40,0xee,0x69,0xf6);

        /// <summary>
        ///     Card type name for "KrDocState".
        /// </summary>
        public readonly string Alias = "KrDocState";

        /// <summary>
        ///     Card type caption for "KrDocState".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrDocState";

        /// <summary>
        ///     Card type group for "KrDocState".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormKrDocState = "KrDocState";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";

        #endregion

        #region ToString

        public static implicit operator string(KrDocStateTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrDocType

    /// <summary>
    ///     ID: {b17f4f35-17e1-4509-994b-ebd576f2c95e}
    ///     Alias: KrDocType
    ///     Caption: $CardTypes_TypesNames_KrDocType
    ///     Group: Dictionaries
    /// </summary>
    public class KrDocTypeTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrDocType": {b17f4f35-17e1-4509-994b-ebd576f2c95e}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb17f4f35,0x17e1,0x4509,0x99,0x4b,0xeb,0xd5,0x76,0xf2,0xc9,0x5e);

        /// <summary>
        ///     Card type name for "KrDocType".
        /// </summary>
        public readonly string Alias = "KrDocType";

        /// <summary>
        ///     Card type caption for "KrDocType".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrDocType";

        /// <summary>
        ///     Card type group for "KrDocType".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormKrDocType = "KrDocType";

        #endregion

        #region Blocks

        public readonly string BlockMainInfoBlock = "MainInfoBlock";
        public readonly string BlockUseApprovingBlock = "UseApprovingBlock";
        public readonly string BlockAutoApprovalSettingsBlock1 = "AutoApprovalSettingsBlock1";
        public readonly string BlockAutoApprovalSettingsBlock2 = "AutoApprovalSettingsBlock2";
        public readonly string BlockApprovalSettingsBlock = "ApprovalSettingsBlock";
        public readonly string BlockUseRegistrationBlock = "UseRegistrationBlock";
        public readonly string BlockRegistrationSettingsBlock = "RegistrationSettingsBlock";
        public readonly string BlockUseResolutionsBlock = "UseResolutionsBlock";
        public readonly string BlockUseForumBlock = "UseForumBlock";

        #endregion

        #region Controls

        public readonly string TitleControl = nameof(TitleControl);
        public readonly string CardTypeControl = nameof(CardTypeControl);
        public readonly string DescriptionControl = nameof(DescriptionControl);
        public readonly string HideCreationButton = nameof(HideCreationButton);
        public readonly string UseApprovingControl = nameof(UseApprovingControl);
        public readonly string UseAutoApprovingControl = nameof(UseAutoApprovingControl);
        public readonly string HideRouteTab = nameof(HideRouteTab);
        public readonly string UseRoutesInWorkflowEngine = nameof(UseRoutesInWorkflowEngine);
        public readonly string UseRegistrationControl = nameof(UseRegistrationControl);
        public readonly string UseResolutionsControl = nameof(UseResolutionsControl);
        public readonly string DisableChildResolutionDateCheck_UseResolutions = nameof(DisableChildResolutionDateCheck_UseResolutions);

        /// <summary>
        ///     Control caption "Warning" for "ForumLicenseWarning".
        /// </summary>
        public readonly string ForumLicenseWarning = nameof(ForumLicenseWarning);
        public readonly string UseDefaultDiscussionTab_UseForum = nameof(UseDefaultDiscussionTab_UseForum);

        #endregion

        #region ToString

        public static implicit operator string(KrDocTypeTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrEdit

    /// <summary>
    ///     ID: {e19ca9b5-48be-4fdf-8dc5-78534b4767de}
    ///     Alias: KrEdit
    ///     Caption: $CardTypes_TypesNames_KrEdit
    ///     Group: KrProcess
    /// </summary>
    public class KrEditTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrEdit": {e19ca9b5-48be-4fdf-8dc5-78534b4767de}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe19ca9b5,0x48be,0x4fdf,0x8d,0xc5,0x78,0x53,0x4b,0x47,0x67,0xde);

        /// <summary>
        ///     Card type name for "KrEdit".
        /// </summary>
        public readonly string Alias = "KrEdit";

        /// <summary>
        ///     Card type caption for "KrEdit".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrEdit";

        /// <summary>
        ///     Card type group for "KrEdit".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrEdit = "KrEdit";
        public readonly string FormNewCycle = "NewCycle";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string Comment = nameof(Comment);

        #endregion

        #region ToString

        public static implicit operator string(KrEditTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrEditInterject

    /// <summary>
    ///     ID: {c9b93ae3-9b7b-4431-a306-aace4aea8732}
    ///     Alias: KrEditInterject
    ///     Caption: $CardTypes_TypesNames_KrEditInterject
    ///     Group: KrProcess
    /// </summary>
    public class KrEditInterjectTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrEditInterject": {c9b93ae3-9b7b-4431-a306-aace4aea8732}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc9b93ae3,0x9b7b,0x4431,0xa3,0x06,0xaa,0xce,0x4a,0xea,0x87,0x32);

        /// <summary>
        ///     Card type name for "KrEditInterject".
        /// </summary>
        public readonly string Alias = "KrEditInterject";

        /// <summary>
        ///     Card type caption for "KrEditInterject".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrEditInterject";

        /// <summary>
        ///     Card type group for "KrEditInterject".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrEditInterject = "KrEditInterject";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(KrEditInterjectTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrEditStageTypeSettings

    /// <summary>
    ///     ID: {995621e3-fdcf-412b-91a6-3f28fe933e70}
    ///     Alias: KrEditStageTypeSettings
    ///     Caption: $KrStages_Edit
    ///     Group: KrProcess
    /// </summary>
    public class KrEditStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrEditStageTypeSettings": {995621e3-fdcf-412b-91a6-3f28fe933e70}.
        /// </summary>
        public readonly Guid ID = new Guid(0x995621e3,0xfdcf,0x412b,0x91,0xa6,0x3f,0x28,0xfe,0x93,0x3e,0x70);

        /// <summary>
        ///     Card type name for "KrEditStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrEditStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrEditStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_Edit";

        /// <summary>
        ///     Card type group for "KrEditStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrEditStageTypeSettings = "KrEditStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockMainBlock = "MainBlock";

        #endregion

        #region ToString

        public static implicit operator string(KrEditStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrExampleDialogSatellite

    /// <summary>
    ///     ID: {7cfe67a4-0b8e-423b-8c15-8e2c584b429b}
    ///     Alias: KrExampleDialogSatellite
    ///     Caption: KrExampleDialogSatellite
    ///     Group: KrProcess
    /// </summary>
    public class KrExampleDialogSatelliteTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrExampleDialogSatellite": {7cfe67a4-0b8e-423b-8c15-8e2c584b429b}.
        /// </summary>
        public readonly Guid ID = new Guid(0x7cfe67a4,0x0b8e,0x423b,0x8c,0x15,0x8e,0x2c,0x58,0x4b,0x42,0x9b);

        /// <summary>
        ///     Card type name for "KrExampleDialogSatellite".
        /// </summary>
        public readonly string Alias = "KrExampleDialogSatellite";

        /// <summary>
        ///     Card type caption for "KrExampleDialogSatellite".
        /// </summary>
        public readonly string Caption = "KrExampleDialogSatellite";

        /// <summary>
        ///     Card type group for "KrExampleDialogSatellite".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrExampleDialogSatellite = "KrExampleDialogSatellite";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(KrExampleDialogSatelliteTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrForkManagementStageTypeSettings

    /// <summary>
    ///     ID: {9393407b-d4ff-408b-abbc-de7ce148ea54}
    ///     Alias: KrForkManagementStageTypeSettings
    ///     Caption: $KrStages_ForkManagement
    ///     Group: KrProcess
    /// </summary>
    public class KrForkManagementStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrForkManagementStageTypeSettings": {9393407b-d4ff-408b-abbc-de7ce148ea54}.
        /// </summary>
        public readonly Guid ID = new Guid(0x9393407b,0xd4ff,0x408b,0xab,0xbc,0xde,0x7c,0xe1,0x48,0xea,0x54);

        /// <summary>
        ///     Card type name for "KrForkManagementStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrForkManagementStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrForkManagementStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_ForkManagement";

        /// <summary>
        ///     Card type group for "KrForkManagementStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrForkManagementStageTypeSettings = "KrForkManagementStageTypeSettings";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(KrForkManagementStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrForkStageTypeSettings

    /// <summary>
    ///     ID: {2729c019-fab9-4eb4-bd98-d3628b1a19f6}
    ///     Alias: KrForkStageTypeSettings
    ///     Caption: $KrStages_Fork
    ///     Group: KrProcess
    /// </summary>
    public class KrForkStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrForkStageTypeSettings": {2729c019-fab9-4eb4-bd98-d3628b1a19f6}.
        /// </summary>
        public readonly Guid ID = new Guid(0x2729c019,0xfab9,0x4eb4,0xbd,0x98,0xd3,0x62,0x8b,0x1a,0x19,0xf6);

        /// <summary>
        ///     Card type name for "KrForkStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrForkStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrForkStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_Fork";

        /// <summary>
        ///     Card type group for "KrForkStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrForkStageTypeSettings = "KrForkStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(KrForkStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrHistoryManagementStageTypeSettings

    /// <summary>
    ///     ID: {cfe5e2af-1014-4ddb-afa1-7450623b103a}
    ///     Alias: KrHistoryManagementStageTypeSettings
    ///     Caption: $KrStages_HistoryManagement
    ///     Group: KrProcess
    /// </summary>
    public class KrHistoryManagementStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrHistoryManagementStageTypeSettings": {cfe5e2af-1014-4ddb-afa1-7450623b103a}.
        /// </summary>
        public readonly Guid ID = new Guid(0xcfe5e2af,0x1014,0x4ddb,0xaf,0xa1,0x74,0x50,0x62,0x3b,0x10,0x3a);

        /// <summary>
        ///     Card type name for "KrHistoryManagementStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrHistoryManagementStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrHistoryManagementStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_HistoryManagement";

        /// <summary>
        ///     Card type group for "KrHistoryManagementStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrHistoryManagementStageTypeSettings = "KrHistoryManagementStageTypeSettings";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "KrTaskHistoryBlockAlias".
        /// </summary>
        public readonly string BlockKrTaskHistoryBlockAlias = "KrTaskHistoryBlockAlias";

        #endregion

        #region ToString

        public static implicit operator string(KrHistoryManagementStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrInfoForInitiator

    /// <summary>
    ///     ID: {c6f3828f-b001-46f6-b121-3f3ed9e65cde}
    ///     Alias: KrInfoForInitiator
    ///     Caption: $CardTypes_TypesNames_KrInfoForInitiator
    ///     Group: KrProcess
    /// </summary>
    public class KrInfoForInitiatorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrInfoForInitiator": {c6f3828f-b001-46f6-b121-3f3ed9e65cde}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc6f3828f,0xb001,0x46f6,0xb1,0x21,0x3f,0x3e,0xd9,0xe6,0x5c,0xde);

        /// <summary>
        ///     Card type name for "KrInfoForInitiator".
        /// </summary>
        public readonly string Alias = "KrInfoForInitiator";

        /// <summary>
        ///     Card type caption for "KrInfoForInitiator".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrInfoForInitiator";

        /// <summary>
        ///     Card type group for "KrInfoForInitiator".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrInfoForInitiator = "KrInfoForInitiator";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(KrInfoForInitiatorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrNotificationStageTypeSettings

    /// <summary>
    ///     ID: {9e57dfaf-986e-41c1-a1c0-be007f0a36a0}
    ///     Alias: KrNotificationStageTypeSettings
    ///     Caption: $KrStages_Notification
    ///     Group: KrProcess
    /// </summary>
    public class KrNotificationStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrNotificationStageTypeSettings": {9e57dfaf-986e-41c1-a1c0-be007f0a36a0}.
        /// </summary>
        public readonly Guid ID = new Guid(0x9e57dfaf,0x986e,0x41c1,0xa1,0xc0,0xbe,0x00,0x7f,0x0a,0x36,0xa0);

        /// <summary>
        ///     Card type name for "KrNotificationStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrNotificationStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrNotificationStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_Notification";

        /// <summary>
        ///     Card type group for "KrNotificationStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrNotificationStageTypeSettings = "KrNotificationStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";

        #endregion

        #region ToString

        public static implicit operator string(KrNotificationStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPerformersSettings

    /// <summary>
    ///     ID: {5ac67186-b62b-4dc4-b9b9-e74d18f53600}
    ///     Alias: KrPerformersSettings
    ///     Caption: KrPerformersSettings
    ///     Group: KrProcess
    /// </summary>
    public class KrPerformersSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrPerformersSettings": {5ac67186-b62b-4dc4-b9b9-e74d18f53600}.
        /// </summary>
        public readonly Guid ID = new Guid(0x5ac67186,0xb62b,0x4dc4,0xb9,0xb9,0xe7,0x4d,0x18,0xf5,0x36,0x00);

        /// <summary>
        ///     Card type name for "KrPerformersSettings".
        /// </summary>
        public readonly string Alias = "KrPerformersSettings";

        /// <summary>
        ///     Card type caption for "KrPerformersSettings".
        /// </summary>
        public readonly string Caption = "KrPerformersSettings";

        /// <summary>
        ///     Card type group for "KrPerformersSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrPerformersSettings = "KrPerformersSettings";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "PerformersBlock" for "PerformersBlock".
        /// </summary>
        public readonly string BlockPerformersBlock = "PerformersBlock";

        #endregion

        #region Controls

        public readonly string SinglePerformerEntryAC = nameof(SinglePerformerEntryAC);
        public readonly string MultiplePerformersTableAC = nameof(MultiplePerformersTableAC);

        #endregion

        #region ToString

        public static implicit operator string(KrPerformersSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissions

    /// <summary>
    ///     ID: {fa9dbdac-8708-41df-bd72-900f69655dfa}
    ///     Alias: KrPermissions
    ///     Caption: $CardTypes_TypesNames_KrPermissions
    ///     Group: Settings
    /// </summary>
    public class KrPermissionsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrPermissions": {fa9dbdac-8708-41df-bd72-900f69655dfa}.
        /// </summary>
        public readonly Guid ID = new Guid(0xfa9dbdac,0x8708,0x41df,0xbd,0x72,0x90,0x0f,0x69,0x65,0x5d,0xfa);

        /// <summary>
        ///     Card type name for "KrPermissions".
        /// </summary>
        public readonly string Alias = "KrPermissions";

        /// <summary>
        ///     Card type caption for "KrPermissions".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrPermissions";

        /// <summary>
        ///     Card type group for "KrPermissions".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormKrPermissions = "KrPermissions";
        public readonly string FormTab = "Tab";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        /// <summary>
        ///     Block caption "Block5" for "Block5".
        /// </summary>
        public readonly string BlockBlock5 = "Block5";
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockBlock4 = "Block4";
        public readonly string BlockAclGenerationRules = "AclGenerationRules";
        public readonly string BlockAdditionalSettings = "AdditionalSettings";

        #endregion

        #region Controls

        public readonly string AclGenerationRules = nameof(AclGenerationRules);
        public readonly string Priority = nameof(Priority);
        public readonly string Fields = nameof(Fields);
        public readonly string Mask = nameof(Mask);
        public readonly string CardExtendedSettings = nameof(CardExtendedSettings);
        public readonly string TasksExtendedSettings = nameof(TasksExtendedSettings);
        public readonly string TaskTypes = nameof(TaskTypes);
        public readonly string CompletionOptions = nameof(CompletionOptions);
        public readonly string MandatoryExtendedSettings = nameof(MandatoryExtendedSettings);
        public readonly string VisibilityExtendedSettings = nameof(VisibilityExtendedSettings);
        public readonly string FileReadAccessSetting = nameof(FileReadAccessSetting);
        public readonly string FileAddAccessSetting = nameof(FileAddAccessSetting);
        public readonly string FileEditAccessSetting = nameof(FileEditAccessSetting);
        public readonly string FileDeleteAccessSetting = nameof(FileDeleteAccessSetting);
        public readonly string FileSignAccessSetting = nameof(FileSignAccessSetting);
        public readonly string FileExtendedPermissionsSettings = nameof(FileExtendedPermissionsSettings);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrProcessManagementStageTypeSettings

    /// <summary>
    ///     ID: {ff753641-0691-4cfc-a8cc-baa89b25a83b}
    ///     Alias: KrProcessManagementStageTypeSettings
    ///     Caption: $KrStages_ProcessManagement
    ///     Group: KrProcess
    /// </summary>
    public class KrProcessManagementStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrProcessManagementStageTypeSettings": {ff753641-0691-4cfc-a8cc-baa89b25a83b}.
        /// </summary>
        public readonly Guid ID = new Guid(0xff753641,0x0691,0x4cfc,0xa8,0xcc,0xba,0xa8,0x9b,0x25,0xa8,0x3b);

        /// <summary>
        ///     Card type name for "KrProcessManagementStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrProcessManagementStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrProcessManagementStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_ProcessManagement";

        /// <summary>
        ///     Card type group for "KrProcessManagementStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrProcessManagementStageTypeSettings = "KrProcessManagementStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";

        #endregion

        #region ToString

        public static implicit operator string(KrProcessManagementStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrRegistration

    /// <summary>
    ///     ID: {09fdd6a3-3946-4f30-9ef9-f533fad3a4a2}
    ///     Alias: KrRegistration
    ///     Caption: $CardTypes_TypesNames_KrRegistration
    ///     Group: KrProcess
    /// </summary>
    public class KrRegistrationTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrRegistration": {09fdd6a3-3946-4f30-9ef9-f533fad3a4a2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x09fdd6a3,0x3946,0x4f30,0x9e,0xf9,0xf5,0x33,0xfa,0xd3,0xa4,0xa2);

        /// <summary>
        ///     Card type name for "KrRegistration".
        /// </summary>
        public readonly string Alias = "KrRegistration";

        /// <summary>
        ///     Card type caption for "KrRegistration".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrRegistration";

        /// <summary>
        ///     Card type group for "KrRegistration".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region ToString

        public static implicit operator string(KrRegistrationTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrRegistrationAction

    /// <summary>
    ///     ID: {bf4641ad-f4dc-4a75-83f4-534cba8bf225}
    ///     Alias: KrRegistrationAction
    ///     Caption: $KrActions_Registration
    ///     Group: WorkflowActions
    /// </summary>
    public class KrRegistrationActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrRegistrationAction": {bf4641ad-f4dc-4a75-83f4-534cba8bf225}.
        /// </summary>
        public readonly Guid ID = new Guid(0xbf4641ad,0xf4dc,0x4a75,0x83,0xf4,0x53,0x4c,0xba,0x8b,0xf2,0x25);

        /// <summary>
        ///     Card type name for "KrRegistrationAction".
        /// </summary>
        public readonly string Alias = "KrRegistrationAction";

        /// <summary>
        ///     Card type caption for "KrRegistrationAction".
        /// </summary>
        public readonly string Caption = "$KrActions_Registration";

        /// <summary>
        ///     Card type group for "KrRegistrationAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region ToString

        public static implicit operator string(KrRegistrationActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrRegistrationStageTypeSettings

    /// <summary>
    ///     ID: {d92e4659-a66b-4efa-aa29-716953da636a}
    ///     Alias: KrRegistrationStageTypeSettings
    ///     Caption: $Cards_DefaultCaption
    ///     Group: KrProcess
    /// </summary>
    public class KrRegistrationStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrRegistrationStageTypeSettings": {d92e4659-a66b-4efa-aa29-716953da636a}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd92e4659,0xa66b,0x4efa,0xaa,0x29,0x71,0x69,0x53,0xda,0x63,0x6a);

        /// <summary>
        ///     Card type name for "KrRegistrationStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrRegistrationStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrRegistrationStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "KrRegistrationStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrRegistrationStageTypeSettings = "KrRegistrationStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockDisclaimerBlock = "DisclaimerBlock";

        #endregion

        #region Controls

        public readonly string EditCardFlagControl = nameof(EditCardFlagControl);
        public readonly string EditFilesFlagControl = nameof(EditFilesFlagControl);

        #endregion

        #region ToString

        public static implicit operator string(KrRegistrationStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrRequestComment

    /// <summary>
    ///     ID: {f0360d95-4f88-4809-b926-57b34a2f69f5}
    ///     Alias: KrRequestComment
    ///     Caption: $CardTypes_TypesNames_KrRequestComment
    ///     Group: KrProcess
    /// </summary>
    public class KrRequestCommentTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrRequestComment": {f0360d95-4f88-4809-b926-57b34a2f69f5}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf0360d95,0x4f88,0x4809,0xb9,0x26,0x57,0xb3,0x4a,0x2f,0x69,0xf5);

        /// <summary>
        ///     Card type name for "KrRequestComment".
        /// </summary>
        public readonly string Alias = "KrRequestComment";

        /// <summary>
        ///     Card type caption for "KrRequestComment".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrRequestComment";

        /// <summary>
        ///     Card type group for "KrRequestComment".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrRequestComment = "KrRequestComment";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string Comment = nameof(Comment);

        #endregion

        #region ToString

        public static implicit operator string(KrRequestCommentTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrResolutionAction

    /// <summary>
    ///     ID: {235e42ea-7ad8-4321-9a3a-91b752985ef0}
    ///     Alias: KrResolutionAction
    ///     Caption: $KrActions_Resolution
    ///     Group: WorkflowActions
    /// </summary>
    public class KrResolutionActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrResolutionAction": {235e42ea-7ad8-4321-9a3a-91b752985ef0}.
        /// </summary>
        public readonly Guid ID = new Guid(0x235e42ea,0x7ad8,0x4321,0x9a,0x3a,0x91,0xb7,0x52,0x98,0x5e,0xf0);

        /// <summary>
        ///     Card type name for "KrResolutionAction".
        /// </summary>
        public readonly string Alias = "KrResolutionAction";

        /// <summary>
        ///     Card type caption for "KrResolutionAction".
        /// </summary>
        public readonly string Caption = "$KrActions_Resolution";

        /// <summary>
        ///     Card type group for "KrResolutionAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormKrResolutionAction = "KrResolutionAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string AddComputedRoleLink = nameof(AddComputedRoleLink);

        #endregion

        #region ToString

        public static implicit operator string(KrResolutionActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrResolutionStageTypeSettings

    /// <summary>
    ///     ID: {c898080f-0fa7-45d9-bbc9-f28dfd2c8f1c}
    ///     Alias: KrResolutionStageTypeSettings
    ///     Caption: $KrStages_Resolution
    ///     Group: KrProcess
    /// </summary>
    public class KrResolutionStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrResolutionStageTypeSettings": {c898080f-0fa7-45d9-bbc9-f28dfd2c8f1c}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc898080f,0x0fa7,0x45d9,0xbb,0xc9,0xf2,0x8d,0xfd,0x2c,0x8f,0x1c);

        /// <summary>
        ///     Card type name for "KrResolutionStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrResolutionStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrResolutionStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_Resolution";

        /// <summary>
        ///     Card type group for "KrResolutionStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrResolutionStageTypeSettings = "KrResolutionStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockSender = "Sender";
        public readonly string BlockPerformers = "Performers";
        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockMoreInfo = "MoreInfo";

        #endregion

        #region Controls

        public readonly string MassCreation = nameof(MassCreation);
        public readonly string MajorPerformer = nameof(MajorPerformer);
        public readonly string Controller = nameof(Controller);

        #endregion

        #region ToString

        public static implicit operator string(KrResolutionStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrRouteInitializationAction

    /// <summary>
    ///     ID: {25ca876a-50b2-4c27-b847-56d4fc597934}
    ///     Alias: KrRouteInitializationAction
    ///     Caption: $KrActions_RouteInitialization
    ///     Group: WorkflowActions
    /// </summary>
    public class KrRouteInitializationActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrRouteInitializationAction": {25ca876a-50b2-4c27-b847-56d4fc597934}.
        /// </summary>
        public readonly Guid ID = new Guid(0x25ca876a,0x50b2,0x4c27,0xb8,0x47,0x56,0xd4,0xfc,0x59,0x79,0x34);

        /// <summary>
        ///     Card type name for "KrRouteInitializationAction".
        /// </summary>
        public readonly string Alias = "KrRouteInitializationAction";

        /// <summary>
        ///     Card type caption for "KrRouteInitializationAction".
        /// </summary>
        public readonly string Caption = "$KrActions_RouteInitialization";

        /// <summary>
        ///     Card type group for "KrRouteInitializationAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormKrRouteInitializationAction = "KrRouteInitializationAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(KrRouteInitializationActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrSamplePermissionsExtensionType

    /// <summary>
    ///     ID: {3e00b804-7905-484e-a50b-191ad1a118a2}
    ///     Alias: KrSamplePermissionsExtensionType
    ///     Caption: An example of card type used to extend access rules in standard solution
    ///     Group: Permissions
    /// </summary>
    public class KrSamplePermissionsExtensionTypeTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrSamplePermissionsExtensionType": {3e00b804-7905-484e-a50b-191ad1a118a2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x3e00b804,0x7905,0x484e,0xa5,0x0b,0x19,0x1a,0xd1,0xa1,0x18,0xa2);

        /// <summary>
        ///     Card type name for "KrSamplePermissionsExtensionType".
        /// </summary>
        public readonly string Alias = "KrSamplePermissionsExtensionType";

        /// <summary>
        ///     Card type caption for "KrSamplePermissionsExtensionType".
        /// </summary>
        public readonly string Caption = "An example of card type used to extend access rules in standard solution";

        /// <summary>
        ///     Card type group for "KrSamplePermissionsExtensionType".
        /// </summary>
        public readonly string Group = "Permissions";

        #endregion

        #region Forms

        public readonly string FormKrSamplePermissionsExtensionType = "KrSamplePermissionsExtensionType";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Amount" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(KrSamplePermissionsExtensionTypeTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrSatellite

    /// <summary>
    ///     ID: {4115f07e-0aaa-4563-a749-0450c1a850af}
    ///     Alias: KrSatellite
    ///     Caption: $CardTypes_TypesNames_KrSatellite
    ///     Group: KrProcess
    /// </summary>
    public class KrSatelliteTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrSatellite": {4115f07e-0aaa-4563-a749-0450c1a850af}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4115f07e,0x0aaa,0x4563,0xa7,0x49,0x04,0x50,0xc1,0xa8,0x50,0xaf);

        /// <summary>
        ///     Card type name for "KrSatellite".
        /// </summary>
        public readonly string Alias = "KrSatellite";

        /// <summary>
        ///     Card type caption for "KrSatellite".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrSatellite";

        /// <summary>
        ///     Card type group for "KrSatellite".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region ToString

        public static implicit operator string(KrSatelliteTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrSecondaryProcess

    /// <summary>
    ///     ID: {61420fa1-cc1f-47cb-b0bb-4ea8ee77f51a}
    ///     Alias: KrSecondaryProcess
    ///     Caption: $CardTypes_TypesNames_KrSecondaryProcess
    ///     Group: Routes
    /// </summary>
    public class KrSecondaryProcessTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrSecondaryProcess": {61420fa1-cc1f-47cb-b0bb-4ea8ee77f51a}.
        /// </summary>
        public readonly Guid ID = new Guid(0x61420fa1,0xcc1f,0x47cb,0xb0,0xbb,0x4e,0xa8,0xee,0x77,0xf5,0x1a);

        /// <summary>
        ///     Card type name for "KrSecondaryProcess".
        /// </summary>
        public readonly string Alias = "KrSecondaryProcess";

        /// <summary>
        ///     Card type caption for "KrSecondaryProcess".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrSecondaryProcess";

        /// <summary>
        ///     Card type group for "KrSecondaryProcess".
        /// </summary>
        public readonly string Group = "Routes";

        #endregion

        #region Forms

        public readonly string FormKrSecondaryProcess = "KrSecondaryProcess";
        public readonly string FormCompilerOutput = "CompilerOutput";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        public readonly string BlockMainInformationBlock = "MainInformationBlock";
        public readonly string BlockPureProcessParametersBlock = "PureProcessParametersBlock";
        public readonly string BlockActionParametersBlock = "ActionParametersBlock";

        /// <summary>
        ///     Block caption "Block2" for "Block2".
        /// </summary>
        public readonly string BlockBlock2 = "Block2";

        /// <summary>
        ///     Block caption "Block3" for "Block3".
        /// </summary>
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockTileParametersBlock = "TileParametersBlock";
        public readonly string BlockExecutionAccessDeniedBlock = "ExecutionAccessDeniedBlock";
        public readonly string BlockRestictionsBlock = "RestictionsBlock";
        public readonly string BlockRouteParametersBlock = "RouteParametersBlock";
        public readonly string BlockVisibilityScriptsBlock = "VisibilityScriptsBlock";
        public readonly string BlockExecutionScriptsBlock = "ExecutionScriptsBlock";
        public readonly string BlockBlock4 = "Block4";

        #endregion

        #region Controls

        public readonly string CheckRecalcRestrictionsCheckbox = nameof(CheckRecalcRestrictionsCheckbox);
        public readonly string CompileButton = nameof(CompileButton);
        public readonly string CompileAllButton = nameof(CompileAllButton);
        public readonly string CompilerOutputTable = nameof(CompilerOutputTable);

        #endregion

        #region ToString

        public static implicit operator string(KrSecondaryProcessTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrSecondarySatellite

    /// <summary>
    ///     ID: {7593c144-31f7-43c2-9c4b-e3b776562f8f}
    ///     Alias: KrSecondarySatellite
    ///     Caption: $CardTypes_TypesNames_KrSecondarySatellite
    ///     Group: KrProcess
    /// </summary>
    public class KrSecondarySatelliteTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrSecondarySatellite": {7593c144-31f7-43c2-9c4b-e3b776562f8f}.
        /// </summary>
        public readonly Guid ID = new Guid(0x7593c144,0x31f7,0x43c2,0x9c,0x4b,0xe3,0xb7,0x76,0x56,0x2f,0x8f);

        /// <summary>
        ///     Card type name for "KrSecondarySatellite".
        /// </summary>
        public readonly string Alias = "KrSecondarySatellite";

        /// <summary>
        ///     Card type caption for "KrSecondarySatellite".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrSecondarySatellite";

        /// <summary>
        ///     Card type group for "KrSecondarySatellite".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region ToString

        public static implicit operator string(KrSecondarySatelliteTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrSettings

    /// <summary>
    ///     ID: {35a03878-57b6-4263-ae36-92eb59032132}
    ///     Alias: KrSettings
    ///     Caption: $CardTypes_TypesNames_KrSettings
    ///     Group: Settings
    /// </summary>
    public class KrSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrSettings": {35a03878-57b6-4263-ae36-92eb59032132}.
        /// </summary>
        public readonly Guid ID = new Guid(0x35a03878,0x57b6,0x4263,0xae,0x36,0x92,0xeb,0x59,0x03,0x21,0x32);

        /// <summary>
        ///     Card type name for "KrSettings".
        /// </summary>
        public readonly string Alias = "KrSettings";

        /// <summary>
        ///     Card type caption for "KrSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrSettings";

        /// <summary>
        ///     Card type group for "KrSettings".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormKrSettings = "KrSettings";
        public readonly string FormStages = "Stages";
        public readonly string FormGenerator = "Generator";

        #endregion

        #region Blocks

        public readonly string BlockOtherSettings = "OtherSettings";
        public readonly string BlockTypeSettingsBlock = "TypeSettingsBlock";
        public readonly string BlockHideCreationButtonBlock = "HideCreationButtonBlock";
        public readonly string BlockUseApprovingBlock = "UseApprovingBlock";
        public readonly string BlockAutoApprovalSettingsBlock1 = "AutoApprovalSettingsBlock1";
        public readonly string BlockAutoApprovalSettingsBlock2 = "AutoApprovalSettingsBlock2";
        public readonly string BlockApprovalSettingsBlock = "ApprovalSettingsBlock";
        public readonly string BlockUseRegistrationBlock = "UseRegistrationBlock";
        public readonly string BlockRegistrationSettingsBlock = "RegistrationSettingsBlock";
        public readonly string BlockUseResolutionsBlock = "UseResolutionsBlock";
        public readonly string BlockUseForumBlock = "UseForumBlock";
        public readonly string BlockCardTypesBlock = "CardTypesBlock";
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock4 = "Block4";

        #endregion

        #region Controls

        public readonly string AclReadCardAccess = nameof(AclReadCardAccess);
        public readonly string UseDocTypesControl = nameof(UseDocTypesControl);
        public readonly string HideCreationButton = nameof(HideCreationButton);
        public readonly string UseApprovingControl = nameof(UseApprovingControl);
        public readonly string UseAutoApprovingControl = nameof(UseAutoApprovingControl);
        public readonly string HideRouteTab = nameof(HideRouteTab);
        public readonly string UseRoutesInWorkflowEngine = nameof(UseRoutesInWorkflowEngine);
        public readonly string UseRegistrationControl = nameof(UseRegistrationControl);
        public readonly string UseResolutionsControl = nameof(UseResolutionsControl);
        public readonly string DisableChildResolutionDateCheck_UseResolutions = nameof(DisableChildResolutionDateCheck_UseResolutions);

        /// <summary>
        ///     Control caption "Warning" for "ForumLicenseWarning".
        /// </summary>
        public readonly string ForumLicenseWarning = nameof(ForumLicenseWarning);
        public readonly string UseDefaultDiscussionTab_UseForum = nameof(UseDefaultDiscussionTab_UseForum);
        public readonly string CardTypeControl = nameof(CardTypeControl);

        /// <summary>
        ///     Control caption "Warning" for "AdvancedNotificationsTooltip".
        /// </summary>
        public readonly string AdvancedNotificationsTooltip = nameof(AdvancedNotificationsTooltip);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrShowDialog

    /// <summary>
    ///     ID: {5309ce42-c4d2-4e99-a733-697c589311e7}
    ///     Alias: KrShowDialog
    ///     Caption: $CardTypes_TypesNames_ShowDialog
    ///     Group: KrProcess
    /// </summary>
    public class KrShowDialogTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrShowDialog": {5309ce42-c4d2-4e99-a733-697c589311e7}.
        /// </summary>
        public readonly Guid ID = new Guid(0x5309ce42,0xc4d2,0x4e99,0xa7,0x33,0x69,0x7c,0x58,0x93,0x11,0xe7);

        /// <summary>
        ///     Card type name for "KrShowDialog".
        /// </summary>
        public readonly string Alias = "KrShowDialog";

        /// <summary>
        ///     Card type caption for "KrShowDialog".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ShowDialog";

        /// <summary>
        ///     Card type group for "KrShowDialog".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region ToString

        public static implicit operator string(KrShowDialogTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrSigning

    /// <summary>
    ///     ID: {968d68b3-a7c5-4b5d-bfa4-bb0f346880b6}
    ///     Alias: KrSigning
    ///     Caption: $CardTypes_TypesNames_KrSigning
    ///     Group: KrProcess
    /// </summary>
    public class KrSigningTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrSigning": {968d68b3-a7c5-4b5d-bfa4-bb0f346880b6}.
        /// </summary>
        public readonly Guid ID = new Guid(0x968d68b3,0xa7c5,0x4b5d,0xbf,0xa4,0xbb,0x0f,0x34,0x68,0x80,0xb6);

        /// <summary>
        ///     Card type name for "KrSigning".
        /// </summary>
        public readonly string Alias = "KrSigning";

        /// <summary>
        ///     Card type caption for "KrSigning".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrSigning";

        /// <summary>
        ///     Card type group for "KrSigning".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrSigning = "KrSigning";
        public readonly string FormDelegate = "Delegate";
        public readonly string FormRequestComment = "RequestComment";
        public readonly string FormSign = "Sign";
        public readonly string FormDecline = "Decline";
        public readonly string FormAdditionalApproval = "AdditionalApproval";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockCommentBlock = "CommentBlock";
        public readonly string BlockCommentsBlockShort = "CommentsBlockShort";
        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockAdditionalApprovalBlockShort = "AdditionalApprovalBlockShort";
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        public readonly string KrCommentsTable = nameof(KrCommentsTable);
        public readonly string KrAdditionalApprovalTable = nameof(KrAdditionalApprovalTable);
        public readonly string WithControl = nameof(WithControl);

        #endregion

        #region ToString

        public static implicit operator string(KrSigningTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrSigningAction

    /// <summary>
    ///     ID: {01762690-a192-4e8e-9b5e-0110666fd977}
    ///     Alias: KrSigningAction
    ///     Caption: $KrActions_Signing
    ///     Group: WorkflowActions
    /// </summary>
    public class KrSigningActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrSigningAction": {01762690-a192-4e8e-9b5e-0110666fd977}.
        /// </summary>
        public readonly Guid ID = new Guid(0x01762690,0xa192,0x4e8e,0x9b,0x5e,0x01,0x10,0x66,0x6f,0xd9,0x77);

        /// <summary>
        ///     Card type name for "KrSigningAction".
        /// </summary>
        public readonly string Alias = "KrSigningAction";

        /// <summary>
        ///     Card type caption for "KrSigningAction".
        /// </summary>
        public readonly string Caption = "$KrActions_Signing";

        /// <summary>
        ///     Card type group for "KrSigningAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormKrSigningAction = "KrSigningAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockKrApprovalAction_CommonSettings = "KrApprovalAction_CommonSettings";
        public readonly string BlockStageFlags = "StageFlags";
        public readonly string BlockKrApprovalAction_AdditionalSettings = "KrApprovalAction_AdditionalSettings";
        public readonly string BlockDisclaimerBlock = "DisclaimerBlock";
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockBlock4 = "Block4";

        #endregion

        #region Controls

        public readonly string AddComputedRoleLink = nameof(AddComputedRoleLink);
        public readonly string FlagsTabs = nameof(FlagsTabs);
        public readonly string CompletionOptionsTable = nameof(CompletionOptionsTable);
        public readonly string ActionCompletionOptionsTable = nameof(ActionCompletionOptionsTable);
        public readonly string ActionEventsTable = nameof(ActionEventsTable);

        #endregion

        #region ToString

        public static implicit operator string(KrSigningActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrSigningStageTypeSettings

    /// <summary>
    ///     ID: {5c473877-1e54-495c-8eca-74885d292786}
    ///     Alias: KrSigningStageTypeSettings
    ///     Caption: $KrStages_Signing
    ///     Group: KrProcess
    /// </summary>
    public class KrSigningStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrSigningStageTypeSettings": {5c473877-1e54-495c-8eca-74885d292786}.
        /// </summary>
        public readonly Guid ID = new Guid(0x5c473877,0x1e54,0x495c,0x8e,0xca,0x74,0x88,0x5d,0x29,0x27,0x86);

        /// <summary>
        ///     Card type name for "KrSigningStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrSigningStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrSigningStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_Signing";

        /// <summary>
        ///     Card type group for "KrSigningStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrSigningStageTypeSettings = "KrSigningStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockCommonSettings = "CommonSettings";
        public readonly string BlockStageFlags = "StageFlags";
        public readonly string BlockAdditionalSettings = "AdditionalSettings";
        public readonly string BlockDisclaimerBlock = "DisclaimerBlock";
        public readonly string BlockSigningStageFlags = "SigningStageFlags";

        #endregion

        #region Controls

        public readonly string IsParallelFlag = nameof(IsParallelFlag);
        public readonly string ReturnIfNotSigned = nameof(ReturnIfNotSigned);
        public readonly string AllowAdditionalApproval = nameof(AllowAdditionalApproval);
        public readonly string ReturnAfterSigning = nameof(ReturnAfterSigning);
        public readonly string DisclaimerControl = nameof(DisclaimerControl);
        public readonly string EditCardFlagControl = nameof(EditCardFlagControl);
        public readonly string EditFilesFlagControl = nameof(EditFilesFlagControl);
        public readonly string FlagsTabs = nameof(FlagsTabs);

        #endregion

        #region ToString

        public static implicit operator string(KrSigningStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrStageCommonMethod

    /// <summary>
    ///     ID: {66cd517b-5423-43db-8374-f50ec0d967eb}
    ///     Alias: KrStageCommonMethod
    ///     Caption: $CardTypes_TypesNames_KrCommonMethod
    ///     Group: Routes
    /// </summary>
    public class KrStageCommonMethodTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrStageCommonMethod": {66cd517b-5423-43db-8374-f50ec0d967eb}.
        /// </summary>
        public readonly Guid ID = new Guid(0x66cd517b,0x5423,0x43db,0x83,0x74,0xf5,0x0e,0xc0,0xd9,0x67,0xeb);

        /// <summary>
        ///     Card type name for "KrStageCommonMethod".
        /// </summary>
        public readonly string Alias = "KrStageCommonMethod";

        /// <summary>
        ///     Card type caption for "KrStageCommonMethod".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrCommonMethod";

        /// <summary>
        ///     Card type group for "KrStageCommonMethod".
        /// </summary>
        public readonly string Group = "Routes";

        #endregion

        #region Forms

        public readonly string FormKrStageCommonMethod = "KrStageCommonMethod";
        public readonly string FormCompilerOutput = "CompilerOutput";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock4 = "Block4";

        #endregion

        #region Controls

        public readonly string CompileButton = nameof(CompileButton);
        public readonly string CompileAllButton = nameof(CompileAllButton);
        public readonly string CompilerOutputTable = nameof(CompilerOutputTable);

        #endregion

        #region ToString

        public static implicit operator string(KrStageCommonMethodTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrStageGroup

    /// <summary>
    ///     ID: {9ce8e9f4-cbf0-4b5f-a569-b508b1fd4b3a}
    ///     Alias: KrStageGroup
    ///     Caption: $CardTypes_TypesNames_KrStageGroup
    ///     Group: Routes
    /// </summary>
    public class KrStageGroupTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrStageGroup": {9ce8e9f4-cbf0-4b5f-a569-b508b1fd4b3a}.
        /// </summary>
        public readonly Guid ID = new Guid(0x9ce8e9f4,0xcbf0,0x4b5f,0xa5,0x69,0xb5,0x08,0xb1,0xfd,0x4b,0x3a);

        /// <summary>
        ///     Card type name for "KrStageGroup".
        /// </summary>
        public readonly string Alias = "KrStageGroup";

        /// <summary>
        ///     Card type caption for "KrStageGroup".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrStageGroup";

        /// <summary>
        ///     Card type group for "KrStageGroup".
        /// </summary>
        public readonly string Group = "Routes";

        #endregion

        #region Forms

        public readonly string FormKrStageGroup = "KrStageGroup";
        public readonly string FormCompilerOutput = "CompilerOutput";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        public readonly string BlockMainBlock = "MainBlock";
        public readonly string BlockRestrictionsBlock = "RestrictionsBlock";
        public readonly string BlockStageTemplatesBlock = "StageTemplatesBlock";
        public readonly string BlockBlock4 = "Block4";

        #endregion

        #region Controls

        public readonly string CSharpSourceTableDesign = nameof(CSharpSourceTableDesign);
        public readonly string CSharpSourceTableRuntime = nameof(CSharpSourceTableRuntime);
        public readonly string CompileButton = nameof(CompileButton);
        public readonly string CompileAllButton = nameof(CompileAllButton);
        public readonly string CompilerOutputTable = nameof(CompilerOutputTable);

        #endregion

        #region ToString

        public static implicit operator string(KrStageGroupTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrStageTemplate

    /// <summary>
    ///     ID: {2fa85bb3-bba4-4ab6-ba97-652106db96de}
    ///     Alias: KrStageTemplate
    ///     Caption: $CardTypes_TypesNames_KrStageTemplate
    ///     Group: Routes
    /// </summary>
    public class KrStageTemplateTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrStageTemplate": {2fa85bb3-bba4-4ab6-ba97-652106db96de}.
        /// </summary>
        public readonly Guid ID = new Guid(0x2fa85bb3,0xbba4,0x4ab6,0xba,0x97,0x65,0x21,0x06,0xdb,0x96,0xde);

        /// <summary>
        ///     Card type name for "KrStageTemplate".
        /// </summary>
        public readonly string Alias = "KrStageTemplate";

        /// <summary>
        ///     Card type caption for "KrStageTemplate".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrStageTemplate";

        /// <summary>
        ///     Card type group for "KrStageTemplate".
        /// </summary>
        public readonly string Group = "Routes";

        #endregion

        #region Forms

        public readonly string FormKrStageTemplate = "KrStageTemplate";
        public readonly string FormCompilerOutput = "CompilerOutput";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockRestrictionsBlock = "RestrictionsBlock";
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock4 = "Block4";

        #endregion

        #region Controls

        public readonly string CanMoveCheckboxAlias = nameof(CanMoveCheckboxAlias);
        public readonly string CSharpSourceTable = nameof(CSharpSourceTable);
        public readonly string CompileButton = nameof(CompileButton);
        public readonly string CompileAllButton = nameof(CompileAllButton);
        public readonly string CompilerOutputTable = nameof(CompilerOutputTable);

        #endregion

        #region ToString

        public static implicit operator string(KrStageTemplateTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrStateExtension

    /// <summary>
    ///     ID: {cca319ab-ed7a-4715-9b56-d0d5bd9d41ab}
    ///     Alias: KrStateExtension
    ///     Caption: KrStateExtension
    ///     Group: Acl
    /// </summary>
    public class KrStateExtensionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrStateExtension": {cca319ab-ed7a-4715-9b56-d0d5bd9d41ab}.
        /// </summary>
        public readonly Guid ID = new Guid(0xcca319ab,0xed7a,0x4715,0x9b,0x56,0xd0,0xd5,0xbd,0x9d,0x41,0xab);

        /// <summary>
        ///     Card type name for "KrStateExtension".
        /// </summary>
        public readonly string Alias = "KrStateExtension";

        /// <summary>
        ///     Card type caption for "KrStateExtension".
        /// </summary>
        public readonly string Caption = "KrStateExtension";

        /// <summary>
        ///     Card type group for "KrStateExtension".
        /// </summary>
        public readonly string Group = "Acl";

        #endregion

        #region Forms

        public readonly string FormTab = "Tab";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "States" for "States".
        /// </summary>
        public readonly string BlockStates = "States";

        #endregion

        #region ToString

        public static implicit operator string(KrStateExtensionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrTaskKindSettings

    /// <summary>
    ///     ID: {f1e87ca5-e1f1-4e5a-acbf-d6cfb20bcb11}
    ///     Alias: KrTaskKindSettings
    ///     Caption: KrTaskKindSettings
    ///     Group: KrProcess
    /// </summary>
    public class KrTaskKindSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrTaskKindSettings": {f1e87ca5-e1f1-4e5a-acbf-d6cfb20bcb11}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf1e87ca5,0xe1f1,0x4e5a,0xac,0xbf,0xd6,0xcf,0xb2,0x0b,0xcb,0x11);

        /// <summary>
        ///     Card type name for "KrTaskKindSettings".
        /// </summary>
        public readonly string Alias = "KrTaskKindSettings";

        /// <summary>
        ///     Card type caption for "KrTaskKindSettings".
        /// </summary>
        public readonly string Caption = "KrTaskKindSettings";

        /// <summary>
        ///     Card type group for "KrTaskKindSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrTaskKindSettings = "KrTaskKindSettings";

        #endregion

        #region Blocks

        public readonly string BlockTaskKindBlock = "TaskKindBlock";

        #endregion

        #region Controls

        public readonly string TaskKindEntryAC = nameof(TaskKindEntryAC);

        #endregion

        #region ToString

        public static implicit operator string(KrTaskKindSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrTaskRegistrationAction

    /// <summary>
    ///     ID: {2d6cbf60-1c5a-40fd-a091-fa42bd4441bc}
    ///     Alias: KrTaskRegistrationAction
    ///     Caption: $KrActions_TaskRegistration
    ///     Group: WorkflowActions
    /// </summary>
    public class KrTaskRegistrationActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrTaskRegistrationAction": {2d6cbf60-1c5a-40fd-a091-fa42bd4441bc}.
        /// </summary>
        public readonly Guid ID = new Guid(0x2d6cbf60,0x1c5a,0x40fd,0xa0,0x91,0xfa,0x42,0xbd,0x44,0x41,0xbc);

        /// <summary>
        ///     Card type name for "KrTaskRegistrationAction".
        /// </summary>
        public readonly string Alias = "KrTaskRegistrationAction";

        /// <summary>
        ///     Card type caption for "KrTaskRegistrationAction".
        /// </summary>
        public readonly string Caption = "$KrActions_TaskRegistration";

        /// <summary>
        ///     Card type group for "KrTaskRegistrationAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormKrTaskRegistrationAction = "KrTaskRegistrationAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockDisclaimerBlock = "DisclaimerBlock";
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        public readonly string FlagsTabs = nameof(FlagsTabs);
        public readonly string CompletionOptionsTable = nameof(CompletionOptionsTable);
        public readonly string ActionEventsTable = nameof(ActionEventsTable);

        #endregion

        #region ToString

        public static implicit operator string(KrTaskRegistrationActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrTemplateCard

    /// <summary>
    ///     ID: {d3d3d2e1-a45e-40c5-8228-cd304fdf6f4d}
    ///     Alias: KrTemplateCard
    ///     Caption: KrTemplateCard
    ///     Group: Routes
    /// </summary>
    public class KrTemplateCardTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrTemplateCard": {d3d3d2e1-a45e-40c5-8228-cd304fdf6f4d}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd3d3d2e1,0xa45e,0x40c5,0x82,0x28,0xcd,0x30,0x4f,0xdf,0x6f,0x4d);

        /// <summary>
        ///     Card type name for "KrTemplateCard".
        /// </summary>
        public readonly string Alias = "KrTemplateCard";

        /// <summary>
        ///     Card type caption for "KrTemplateCard".
        /// </summary>
        public readonly string Caption = "KrTemplateCard";

        /// <summary>
        ///     Card type group for "KrTemplateCard".
        /// </summary>
        public readonly string Group = "Routes";

        #endregion

        #region Forms

        public readonly string FormApprovalProcess = "ApprovalProcess";

        #endregion

        #region Blocks

        public readonly string BlockStageCommonInfoBlock = "StageCommonInfoBlock";

        /// <summary>
        ///     Block caption "KrSqlPerformersLinkBlock" for "KrSqlPerformersLinkBlock".
        /// </summary>
        public readonly string BlockKrSqlPerformersLinkBlock = "KrSqlPerformersLinkBlock";

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockSqlRoleBlock = "SqlRoleBlock";
        public readonly string BlockSource_Block = "Source_Block";
        public readonly string BlockApprovalStagesBlock = "ApprovalStagesBlock";

        #endregion

        #region Controls

        public readonly string TimeLimitInput = nameof(TimeLimitInput);
        public readonly string PlannedInput = nameof(PlannedInput);
        public readonly string HiddenStageCheckbox = nameof(HiddenStageCheckbox);
        public readonly string CanBeSkippedCheckbox = nameof(CanBeSkippedCheckbox);
        public readonly string AddComputedRoleLink = nameof(AddComputedRoleLink);
        public readonly string CSharpSourceTable = nameof(CSharpSourceTable);
        public readonly string ApprovalStagesTable = nameof(ApprovalStagesTable);

        #endregion

        #region ToString

        public static implicit operator string(KrTemplateCardTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrTypedTaskStageTypeSettings

    /// <summary>
    ///     ID: {d8e1c89c-12b2-44e5-9bd0-c6a01c49b1e9}
    ///     Alias: KrTypedTaskStageTypeSettings
    ///     Caption: $KrStages_TypedTask
    ///     Group: KrProcess
    /// </summary>
    public class KrTypedTaskStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrTypedTaskStageTypeSettings": {d8e1c89c-12b2-44e5-9bd0-c6a01c49b1e9}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd8e1c89c,0x12b2,0x44e5,0x9b,0xd0,0xc6,0xa0,0x1c,0x49,0xb1,0xe9);

        /// <summary>
        ///     Card type name for "KrTypedTaskStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrTypedTaskStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrTypedTaskStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_TypedTask";

        /// <summary>
        ///     Card type group for "KrTypedTaskStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrTypedTaskStageTypeSettings = "KrTypedTaskStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(KrTypedTaskStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrUniversalTask

    /// <summary>
    ///     ID: {9c6d9824-41d7-41e6-99f1-e19ea9e576c5}
    ///     Alias: KrUniversalTask
    ///     Caption: $CardTypes_TypesNames_KrUniversalTask
    ///     Group: KrProcess
    /// </summary>
    public class KrUniversalTaskTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrUniversalTask": {9c6d9824-41d7-41e6-99f1-e19ea9e576c5}.
        /// </summary>
        public readonly Guid ID = new Guid(0x9c6d9824,0x41d7,0x41e6,0x99,0xf1,0xe1,0x9e,0xa9,0xe5,0x76,0xc5);

        /// <summary>
        ///     Card type name for "KrUniversalTask".
        /// </summary>
        public readonly string Alias = "KrUniversalTask";

        /// <summary>
        ///     Card type caption for "KrUniversalTask".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrUniversalTask";

        /// <summary>
        ///     Card type group for "KrUniversalTask".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormExtended = "Extended";

        #endregion

        #region Blocks

        public readonly string BlockExtended = "Extended";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "MessageLabel" for "MessageLabel".
        /// </summary>
        public readonly string MessageLabel = nameof(MessageLabel);

        public readonly string Comment = nameof(Comment);

        #endregion

        #region ToString

        public static implicit operator string(KrUniversalTaskTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrUniversalTaskAction

    /// <summary>
    ///     ID: {231eea47-db41-4ad4-8846-164da4ef4048}
    ///     Alias: KrUniversalTaskAction
    ///     Caption: $KrActions_UniversalTask
    ///     Group: WorkflowActions
    /// </summary>
    public class KrUniversalTaskActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrUniversalTaskAction": {231eea47-db41-4ad4-8846-164da4ef4048}.
        /// </summary>
        public readonly Guid ID = new Guid(0x231eea47,0xdb41,0x4ad4,0x88,0x46,0x16,0x4d,0xa4,0xef,0x40,0x48);

        /// <summary>
        ///     Card type name for "KrUniversalTaskAction".
        /// </summary>
        public readonly string Alias = "KrUniversalTaskAction";

        /// <summary>
        ///     Card type caption for "KrUniversalTaskAction".
        /// </summary>
        public readonly string Caption = "$KrActions_UniversalTask";

        /// <summary>
        ///     Card type group for "KrUniversalTaskAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormKrUniversalTaskAction = "KrUniversalTaskAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockDisclaimerBlock = "DisclaimerBlock";
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        public readonly string FlagsTabs = nameof(FlagsTabs);
        public readonly string CompletionOptionsTable = nameof(CompletionOptionsTable);
        public readonly string ActionEventsTable = nameof(ActionEventsTable);

        #endregion

        #region ToString

        public static implicit operator string(KrUniversalTaskActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrUniversalTaskStageTypeSettings

    /// <summary>
    ///     ID: {eada56ed-7d98-4e6e-9d9f-950d8aa42696}
    ///     Alias: KrUniversalTaskStageTypeSettings
    ///     Caption: $KrStages_UniversalTask
    ///     Group: KrProcess
    /// </summary>
    public class KrUniversalTaskStageTypeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrUniversalTaskStageTypeSettings": {eada56ed-7d98-4e6e-9d9f-950d8aa42696}.
        /// </summary>
        public readonly Guid ID = new Guid(0xeada56ed,0x7d98,0x4e6e,0x9d,0x9f,0x95,0x0d,0x8a,0xa4,0x26,0x96);

        /// <summary>
        ///     Card type name for "KrUniversalTaskStageTypeSettings".
        /// </summary>
        public readonly string Alias = "KrUniversalTaskStageTypeSettings";

        /// <summary>
        ///     Card type caption for "KrUniversalTaskStageTypeSettings".
        /// </summary>
        public readonly string Caption = "$KrStages_UniversalTask";

        /// <summary>
        ///     Card type group for "KrUniversalTaskStageTypeSettings".
        /// </summary>
        public readonly string Group = "KrProcess";

        #endregion

        #region Forms

        public readonly string FormKrUniversalTaskStageTypeSettings = "KrUniversalTaskStageTypeSettings";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockDisclaimerBlock = "DisclaimerBlock";

        #endregion

        #region Controls

        public readonly string EditCardFlagControl = nameof(EditCardFlagControl);
        public readonly string EditFilesFlagControl = nameof(EditFilesFlagControl);

        #endregion

        #region ToString

        public static implicit operator string(KrUniversalTaskStageTypeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrUserSettings

    /// <summary>
    ///     ID: {793864e5-39e5-4d4f-af59-c3d7a9facca9}
    ///     Alias: KrUserSettings
    ///     Caption: $CardTypes_TypesNames_KrUserSettings
    ///     Group: UserSettings
    /// </summary>
    public class KrUserSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrUserSettings": {793864e5-39e5-4d4f-af59-c3d7a9facca9}.
        /// </summary>
        public readonly Guid ID = new Guid(0x793864e5,0x39e5,0x4d4f,0xaf,0x59,0xc3,0xd7,0xa9,0xfa,0xcc,0xa9);

        /// <summary>
        ///     Card type name for "KrUserSettings".
        /// </summary>
        public readonly string Alias = "KrUserSettings";

        /// <summary>
        ///     Card type caption for "KrUserSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrUserSettings";

        /// <summary>
        ///     Card type group for "KrUserSettings".
        /// </summary>
        public readonly string Group = "UserSettings";

        #endregion

        #region Forms

        public readonly string FormMySettings = "MySettings";

        #endregion

        #region Blocks

        public readonly string BlockStandardSolution = "StandardSolution";

        #endregion

        #region ToString

        public static implicit operator string(KrUserSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrVirtualFile

    /// <summary>
    ///     ID: {81250a95-5c1e-488c-a423-106e7f982c6b}
    ///     Alias: KrVirtualFile
    ///     Caption: $CardTypes_TypesNames_KrVirtualFile
    ///     Group: Dictionaries
    /// </summary>
    public class KrVirtualFileTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "KrVirtualFile": {81250a95-5c1e-488c-a423-106e7f982c6b}.
        /// </summary>
        public readonly Guid ID = new Guid(0x81250a95,0x5c1e,0x488c,0xa4,0x23,0x10,0x6e,0x7f,0x98,0x2c,0x6b);

        /// <summary>
        ///     Card type name for "KrVirtualFile".
        /// </summary>
        public readonly string Alias = "KrVirtualFile";

        /// <summary>
        ///     Card type caption for "KrVirtualFile".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_KrVirtualFile";

        /// <summary>
        ///     Card type group for "KrVirtualFile".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormKrVirtualFile = "KrVirtualFile";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockVersionBlock = "VersionBlock";
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockBlock4 = "Block4";
        public readonly string BlockExamples = "Examples";
        public readonly string BlockBlock5 = "Block5";

        #endregion

        #region Controls

        public readonly string CompileButton = nameof(CompileButton);

        #endregion

        #region ToString

        public static implicit operator string(KrVirtualFileTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawCase

    /// <summary>
    ///     ID: {e16a1f04-9ccd-4338-90b0-ff4d2f581208}
    ///     Alias: LawCase
    ///     Caption: $Cards_Case
    ///     Group: Law
    /// </summary>
    public class LawCaseTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "LawCase": {e16a1f04-9ccd-4338-90b0-ff4d2f581208}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe16a1f04,0x9ccd,0x4338,0x90,0xb0,0xff,0x4d,0x2f,0x58,0x12,0x08);

        /// <summary>
        ///     Card type name for "LawCase".
        /// </summary>
        public readonly string Alias = "LawCase";

        /// <summary>
        ///     Card type caption for "LawCase".
        /// </summary>
        public readonly string Caption = "$Cards_Case";

        /// <summary>
        ///     Card type group for "LawCase".
        /// </summary>
        public readonly string Group = "Law";

        #endregion

        #region Forms

        public readonly string FormMain = "Main";
        public readonly string FormFiles = "Files";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockFoldersBlock = "FoldersBlock";
        public readonly string BlockFilesBlock = "FilesBlock";

        #endregion

        #region Controls

        public readonly string ClassificationIndex = nameof(ClassificationIndex);
        public readonly string Location = nameof(Location);
        public readonly string AdministratorsControl = nameof(AdministratorsControl);
        public readonly string UsersControl = nameof(UsersControl);
        public readonly string ThirdPartiesControl = nameof(ThirdPartiesControl);
        public readonly string ThirdPartiesRepresentativeControl = nameof(ThirdPartiesRepresentativeControl);
        public readonly string FoldersView = nameof(FoldersView);

        /// <summary>
        ///     Control caption "Control1" for "AllFileList".
        /// </summary>
        public readonly string AllFileList = nameof(AllFileList);
        public readonly string FileList = nameof(FileList);

        /// <summary>
        ///     Control caption "Control2" for "FilePreview".
        /// </summary>
        public readonly string FilePreview = nameof(FilePreview);

        #endregion

        #region ToString

        public static implicit operator string(LawCaseTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawFile

    /// <summary>
    ///     ID: {ad5fae5a-d585-4aa2-8590-92ed11f49393}
    ///     Alias: LawFile
    ///     Caption: $Cards_DefaultCaption
    ///     Group: Law
    /// </summary>
    public class LawFileTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "LawFile": {ad5fae5a-d585-4aa2-8590-92ed11f49393}.
        /// </summary>
        public readonly Guid ID = new Guid(0xad5fae5a,0xd585,0x4aa2,0x85,0x90,0x92,0xed,0x11,0xf4,0x93,0x93);

        /// <summary>
        ///     Card type name for "LawFile".
        /// </summary>
        public readonly string Alias = "LawFile";

        /// <summary>
        ///     Card type caption for "LawFile".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "LawFile".
        /// </summary>
        public readonly string Group = "Law";

        #endregion

        #region ToString

        public static implicit operator string(LawFileTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawPartnerSelectionDialog

    /// <summary>
    ///     ID: {03083069-cd54-4a82-a236-8c1ef9179832}
    ///     Alias: LawPartnerSelectionDialog
    ///     Caption: $Cards_DefaultCaption
    ///     Group: System
    /// </summary>
    public class LawPartnerSelectionDialogTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "LawPartnerSelectionDialog": {03083069-cd54-4a82-a236-8c1ef9179832}.
        /// </summary>
        public readonly Guid ID = new Guid(0x03083069,0xcd54,0x4a82,0xa2,0x36,0x8c,0x1e,0xf9,0x17,0x98,0x32);

        /// <summary>
        ///     Card type name for "LawPartnerSelectionDialog".
        /// </summary>
        public readonly string Alias = "LawPartnerSelectionDialog";

        /// <summary>
        ///     Card type caption for "LawPartnerSelectionDialog".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "LawPartnerSelectionDialog".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormMain = "Main";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "Partners" for "PartnersTable".
        /// </summary>
        public readonly string PartnersTable = nameof(PartnersTable);

        #endregion

        #region ToString

        public static implicit operator string(LawPartnerSelectionDialogTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region License

    /// <summary>
    ///     ID: {f9c7b09c-de09-46b5-ba35-e73c83ea52a7}
    ///     Alias: License
    ///     Caption: $CardTypes_TypesNames_License
    ///     Group: Settings
    /// </summary>
    public class LicenseTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "License": {f9c7b09c-de09-46b5-ba35-e73c83ea52a7}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf9c7b09c,0xde09,0x46b5,0xba,0x35,0xe7,0x3c,0x83,0xea,0x52,0xa7);

        /// <summary>
        ///     Card type name for "License".
        /// </summary>
        public readonly string Alias = "License";

        /// <summary>
        ///     Card type caption for "License".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_License";

        /// <summary>
        ///     Card type group for "License".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormLicense = "License";

        #endregion

        #region Blocks

        public readonly string BlockConcurrentLicenses = "ConcurrentLicenses";
        public readonly string BlockPersonalLicenses = "PersonalLicenses";
        public readonly string BlockAdvancedNotifications = "AdvancedNotifications";

        #endregion

        #region Controls

        public readonly string ConcurrentText = nameof(ConcurrentText);
        public readonly string PersonalSelectRoles = nameof(PersonalSelectRoles);
        public readonly string PersonalText = nameof(PersonalText);
        public readonly string MobileSelectRoles = nameof(MobileSelectRoles);
        public readonly string MobileText = nameof(MobileText);

        #endregion

        #region ToString

        public static implicit operator string(LicenseTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LocalizationDialogs

    /// <summary>
    ///     ID: {75c2f788-c389-46ea-8dcf-a62331752a7c}
    ///     Alias: LocalizationDialogs
    ///     Caption: $Cards_DefaultCaption
    ///     Group: System
    /// </summary>
    public class LocalizationDialogsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "LocalizationDialogs": {75c2f788-c389-46ea-8dcf-a62331752a7c}.
        /// </summary>
        public readonly Guid ID = new Guid(0x75c2f788,0xc389,0x46ea,0x8d,0xcf,0xa6,0x23,0x31,0x75,0x2a,0x7c);

        /// <summary>
        ///     Card type name for "LocalizationDialogs".
        /// </summary>
        public readonly string Alias = "LocalizationDialogs";

        /// <summary>
        ///     Card type caption for "LocalizationDialogs".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "LocalizationDialogs".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormAddString = "AddString";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(LocalizationDialogsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region MetadataEditor

    /// <summary>
    ///     ID: {d4ed365b-5384-439f-ac44-9f71977e60d6}
    ///     Alias: MetadataEditor
    ///     Caption: $Cards_DefaultCaption
    ///     Group: System
    /// </summary>
    public class MetadataEditorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "MetadataEditor": {d4ed365b-5384-439f-ac44-9f71977e60d6}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd4ed365b,0x5384,0x439f,0xac,0x44,0x9f,0x71,0x97,0x7e,0x60,0xd6);

        /// <summary>
        ///     Card type name for "MetadataEditor".
        /// </summary>
        public readonly string Alias = "MetadataEditor";

        /// <summary>
        ///     Card type caption for "MetadataEditor".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "MetadataEditor".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormEditor = "Editor";

        #endregion

        #region Blocks

        public readonly string BlockOverridesBlock = "OverridesBlock";
        public readonly string BlockGeneralSettingsBlock = "GeneralSettingsBlock";
        public readonly string BlockGroupingSettingsBlock = "GroupingSettingsBlock";

        /// <summary>
        ///     Block caption "ViewsBlock" for "ViewsBlock".
        /// </summary>
        public readonly string BlockViewsBlock = "ViewsBlock";

        /// <summary>
        ///     Block caption "ViewsContainerBlock" for "ViewsContainerBlock".
        /// </summary>
        public readonly string BlockViewsContainerBlock = "ViewsContainerBlock";

        /// <summary>
        ///     Block caption "EmptyPresenterBlock" for "EmptyPresenterBlock".
        /// </summary>
        public readonly string BlockEmptyPresenterBlock = "EmptyPresenterBlock";

        /// <summary>
        ///     Block caption "ColumnProperties" for "ColumnProperties".
        /// </summary>
        public readonly string BlockColumnProperties = "ColumnProperties";

        /// <summary>
        ///     Block caption "CalendarOverdueColumnInfo" for "CalendarOverdueColumnInfo".
        /// </summary>
        public readonly string BlockCalendarOverdueColumnInfo = "CalendarOverdueColumnInfo";

        /// <summary>
        ///     Block caption "ParameterProperties" for "ParameterProperties".
        /// </summary>
        public readonly string BlockParameterProperties = "ParameterProperties";

        /// <summary>
        ///     Block caption "AutoCompleteInfo" for "AutoCompleteInfo".
        /// </summary>
        public readonly string BlockAutoCompleteInfo = "AutoCompleteInfo";

        /// <summary>
        ///     Block caption "DropDownInfo" for "DropDownInfo".
        /// </summary>
        public readonly string BlockDropDownInfo = "DropDownInfo";

        /// <summary>
        ///     Block caption "AppearanceProperties" for "AppearanceProperties".
        /// </summary>
        public readonly string BlockAppearanceProperties = "AppearanceProperties";
        public readonly string BlockFontProperties = "FontProperties";

        /// <summary>
        ///     Block caption "ReferenceProperties" for "ReferenceProperties".
        /// </summary>
        public readonly string BlockReferenceProperties = "ReferenceProperties";

        /// <summary>
        ///     Block caption "SubsetProperties" for "SubsetProperties".
        /// </summary>
        public readonly string BlockSubsetProperties = "SubsetProperties";

        /// <summary>
        ///     Block caption "ExtensionProperties" for "ExtensionProperties".
        /// </summary>
        public readonly string BlockExtensionProperties = "ExtensionProperties";

        /// <summary>
        ///     Block caption "PropertiesContainerBlock" for "PropertiesContainerBlock".
        /// </summary>
        public readonly string BlockPropertiesContainerBlock = "PropertiesContainerBlock";
        public readonly string BlockOtherSettingsBlock = "OtherSettingsBlock";
        public readonly string BlockTagsSettingsBlock = "TagsSettingsBlock";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "OverridesView" for "Overrides_OverridesView".
        /// </summary>
        public readonly string Overrides_OverridesView = nameof(Overrides_OverridesView);

        public readonly string General_Condition = nameof(General_Condition);

        /// <summary>
        ///     Control caption "Paging" for "General_Paging".
        /// </summary>
        public readonly string General_Paging = nameof(General_Paging);

        /// <summary>
        ///     Control caption "QuickSearchParam" for "General_QuickSearchParam".
        /// </summary>
        public readonly string General_QuickSearchParam = nameof(General_QuickSearchParam);

        /// <summary>
        ///     Control caption "DefaultSortColumns" for "General_DefaultSortColumns".
        /// </summary>
        public readonly string General_DefaultSortColumns = nameof(General_DefaultSortColumns);

        /// <summary>
        ///     Control caption "RowCountSubset" for "General_RowCountSubset".
        /// </summary>
        public readonly string General_RowCountSubset = nameof(General_RowCountSubset);

        /// <summary>
        ///     Control caption "GroupingColumn" for "General_GroupingColumn".
        /// </summary>
        public readonly string General_GroupingColumn = nameof(General_GroupingColumn);

        /// <summary>
        ///     Control caption "SelectionMode" for "General_SelectionMode".
        /// </summary>
        public readonly string General_SelectionMode = nameof(General_SelectionMode);

        /// <summary>
        ///     Control caption "ConnectionAlias" for "General_ConnectionAlias".
        /// </summary>
        public readonly string General_ConnectionAlias = nameof(General_ConnectionAlias);

        /// <summary>
        ///     Control caption "PageLimit" for "General_PageLimit".
        /// </summary>
        public readonly string General_PageLimit = nameof(General_PageLimit);

        /// <summary>
        ///     Control caption "ExportDataPageLimit" for "General_ExportDataPageLimit".
        /// </summary>
        public readonly string General_ExportDataPageLimit = nameof(General_ExportDataPageLimit);

        /// <summary>
        ///     Control caption "AutoWidthRowLimit" for "General_AutoWidthRowLimit".
        /// </summary>
        public readonly string General_AutoWidthRowLimit = nameof(General_AutoWidthRowLimit);

        /// <summary>
        ///     Control caption "Appearance" for "General_Appearance".
        /// </summary>
        public readonly string General_Appearance = nameof(General_Appearance);

        /// <summary>
        ///     Control caption "MultiSelect" for "General_MultiSelect".
        /// </summary>
        public readonly string General_MultiSelect = nameof(General_MultiSelect);

        /// <summary>
        ///     Control caption "EnableAutoWidth" for "General_EnableAutoWidth".
        /// </summary>
        public readonly string General_EnableAutoWidth = nameof(General_EnableAutoWidth);

        /// <summary>
        ///     Control caption "TreatAsSingleQuery" for "General_TreatAsSingleQuery".
        /// </summary>
        public readonly string General_TreatAsSingleQuery = nameof(General_TreatAsSingleQuery);

        /// <summary>
        ///     Control caption "RowCounterVisible" for "General_RowCounterVisible".
        /// </summary>
        public readonly string General_RowCounterVisible = nameof(General_RowCounterVisible);

        /// <summary>
        ///     Control caption "AutoSelectFirstRow" for "General_AutoSelectFirstRow".
        /// </summary>
        public readonly string General_AutoSelectFirstRow = nameof(General_AutoSelectFirstRow);

        /// <summary>
        ///     Control caption "TreeGroupDisplayValue" for "Grouping_TreeGroupDisplayValue".
        /// </summary>
        public readonly string Grouping_TreeGroupDisplayValue = nameof(Grouping_TreeGroupDisplayValue);

        /// <summary>
        ///     Control caption "TreeGroup" for "Grouping_TreeGroup".
        /// </summary>
        public readonly string Grouping_TreeGroup = nameof(Grouping_TreeGroup);

        /// <summary>
        ///     Control caption "TreeGroupId" for "Grouping_TreeGroupId".
        /// </summary>
        public readonly string Grouping_TreeGroupId = nameof(Grouping_TreeGroupId);

        /// <summary>
        ///     Control caption "TreeGroupParentId" for "Grouping_TreeGroupParentId".
        /// </summary>
        public readonly string Grouping_TreeGroupParentId = nameof(Grouping_TreeGroupParentId);

        /// <summary>
        ///     Control caption "TreeId" for "Grouping_TreeId".
        /// </summary>
        public readonly string Grouping_TreeId = nameof(Grouping_TreeId);

        /// <summary>
        ///     Control caption "TreeParentId" for "Grouping_TreeParentId".
        /// </summary>
        public readonly string Grouping_TreeParentId = nameof(Grouping_TreeParentId);
        public readonly string ColumnsView = nameof(ColumnsView);
        public readonly string ParametersView = nameof(ParametersView);
        public readonly string ReferencesView = nameof(ReferencesView);
        public readonly string SubsetsView = nameof(SubsetsView);
        public readonly string AppearancesView = nameof(AppearancesView);
        public readonly string ExtensionsView = nameof(ExtensionsView);

        /// <summary>
        ///     Control caption "ViewsContainer" for "ViewsContainer".
        /// </summary>
        public readonly string ViewsContainer = nameof(ViewsContainer);

        /// <summary>
        ///     Control caption "EmptyPresenter_Text" for "EmptyPresenter_Text".
        /// </summary>
        public readonly string EmptyPresenter_Text = nameof(EmptyPresenter_Text);
        public readonly string Column_Description = nameof(Column_Description);

        /// <summary>
        ///     Control caption "Alias" for "Column_Alias".
        /// </summary>
        public readonly string Column_Alias = nameof(Column_Alias);

        /// <summary>
        ///     Control caption "Caption" for "Column_Caption".
        /// </summary>
        public readonly string Column_Caption = nameof(Column_Caption);
        public readonly string Column_Condition = nameof(Column_Condition);

        /// <summary>
        ///     Control caption "Type" for "Column_Type".
        /// </summary>
        public readonly string Column_Type = nameof(Column_Type);

        /// <summary>
        ///     Control caption "Hidden" for "Column_Hidden".
        /// </summary>
        public readonly string Column_Hidden = nameof(Column_Hidden);

        /// <summary>
        ///     Control caption "SortBy" for "Column_SortBy".
        /// </summary>
        public readonly string Column_SortBy = nameof(Column_SortBy);

        /// <summary>
        ///     Control caption "TreatValueAsUtc" for "Column_TreatValueAsUtc".
        /// </summary>
        public readonly string Column_TreatValueAsUtc = nameof(Column_TreatValueAsUtc);

        /// <summary>
        ///     Control caption "Localizable" for "Column_Localizable".
        /// </summary>
        public readonly string Column_Localizable = nameof(Column_Localizable);

        /// <summary>
        ///     Control caption "DisableGrouping" for "Column_DisableGrouping".
        /// </summary>
        public readonly string Column_DisableGrouping = nameof(Column_DisableGrouping);

        /// <summary>
        ///     Control caption "HasTag" for "Column_HasTag".
        /// </summary>
        public readonly string Column_HasTag = nameof(Column_HasTag);

        /// <summary>
        ///     Control caption "Appearance" for "Column_Appearance".
        /// </summary>
        public readonly string Column_Appearance = nameof(Column_Appearance);

        /// <summary>
        ///     Control caption "MaxLength" for "Column_MaxLength".
        /// </summary>
        public readonly string Column_MaxLength = nameof(Column_MaxLength);

        /// <summary>
        ///     Control caption "CalendarQuantsColumn" for "Column_CalendarQuantsColumn".
        /// </summary>
        public readonly string Column_CalendarQuantsColumn = nameof(Column_CalendarQuantsColumn);

        /// <summary>
        ///     Control caption "PlannedColumn" for "Column_PlannedColumn".
        /// </summary>
        public readonly string Column_PlannedColumn = nameof(Column_PlannedColumn);

        /// <summary>
        ///     Control caption "CalendarIDColumn" for "Column_CalendarIDColumn".
        /// </summary>
        public readonly string Column_CalendarIDColumn = nameof(Column_CalendarIDColumn);

        /// <summary>
        ///     Control caption "CalendarOverdueFormat" for "Column_CalendarOverdueFormat".
        /// </summary>
        public readonly string Column_CalendarOverdueFormat = nameof(Column_CalendarOverdueFormat);
        public readonly string Parameter_Description = nameof(Parameter_Description);

        /// <summary>
        ///     Control caption "Alias" for "Parameter_Alias".
        /// </summary>
        public readonly string Parameter_Alias = nameof(Parameter_Alias);

        /// <summary>
        ///     Control caption "Caption" for "Parameter_Caption".
        /// </summary>
        public readonly string Parameter_Caption = nameof(Parameter_Caption);
        public readonly string Parameter_Condition = nameof(Parameter_Condition);

        /// <summary>
        ///     Control caption "Type" for "Parameter_Type".
        /// </summary>
        public readonly string Parameter_Type = nameof(Parameter_Type);

        /// <summary>
        ///     Control caption "DateTimeType" for "Parameter_DateTimeType".
        /// </summary>
        public readonly string Parameter_DateTimeType = nameof(Parameter_DateTimeType);

        /// <summary>
        ///     Control caption "RefSection" for "Parameter_RefSection".
        /// </summary>
        public readonly string Parameter_RefSection = nameof(Parameter_RefSection);

        /// <summary>
        ///     Control caption "Hidden" for "Parameter_Hidden".
        /// </summary>
        public readonly string Parameter_Hidden = nameof(Parameter_Hidden);

        /// <summary>
        ///     Control caption "TreatValueAsUtc" for "Parameter_TreatValueAsUtc".
        /// </summary>
        public readonly string Parameter_TreatValueAsUtc = nameof(Parameter_TreatValueAsUtc);

        /// <summary>
        ///     Control caption "Multiple" for "Parameter_Multiple".
        /// </summary>
        public readonly string Parameter_Multiple = nameof(Parameter_Multiple);

        /// <summary>
        ///     Control caption "HideAutoCompleteButton" for "Parameter_HideAutoCompleteButton".
        /// </summary>
        public readonly string Parameter_HideAutoCompleteButton = nameof(Parameter_HideAutoCompleteButton);

        /// <summary>
        ///     Control caption "IgnoreCase" for "Parameter_IgnoreCase".
        /// </summary>
        public readonly string Parameter_IgnoreCase = nameof(Parameter_IgnoreCase);

        /// <summary>
        ///     Control caption "AllowedOperands" for "Parameter_AllowedOperands".
        /// </summary>
        public readonly string Parameter_AllowedOperands = nameof(Parameter_AllowedOperands);

        /// <summary>
        ///     Control caption "DisallowedOperands" for "Parameter_DisallowedOperands".
        /// </summary>
        public readonly string Parameter_DisallowedOperands = nameof(Parameter_DisallowedOperands);

        /// <summary>
        ///     Control caption "Param" for "AutoCompleteInfo_Param".
        /// </summary>
        public readonly string AutoCompleteInfo_Param = nameof(AutoCompleteInfo_Param);

        /// <summary>
        ///     Control caption "PopupColumns" for "AutoCompleteInfo_PopupColumns".
        /// </summary>
        public readonly string AutoCompleteInfo_PopupColumns = nameof(AutoCompleteInfo_PopupColumns);

        /// <summary>
        ///     Control caption "RefPrefix" for "AutoCompleteInfo_RefPrefix".
        /// </summary>
        public readonly string AutoCompleteInfo_RefPrefix = nameof(AutoCompleteInfo_RefPrefix);

        /// <summary>
        ///     Control caption "View" for "AutoCompleteInfo_View".
        /// </summary>
        public readonly string AutoCompleteInfo_View = nameof(AutoCompleteInfo_View);

        /// <summary>
        ///     Control caption "PopupColumns" for "DropDownInfo_PopupColumns".
        /// </summary>
        public readonly string DropDownInfo_PopupColumns = nameof(DropDownInfo_PopupColumns);

        /// <summary>
        ///     Control caption "View" for "DropDownInfo_View".
        /// </summary>
        public readonly string DropDownInfo_View = nameof(DropDownInfo_View);

        /// <summary>
        ///     Control caption "RefPrefix" for "DropDownInfo_RefPrefix".
        /// </summary>
        public readonly string DropDownInfo_RefPrefix = nameof(DropDownInfo_RefPrefix);
        public readonly string Appearance_Description = nameof(Appearance_Description);

        /// <summary>
        ///     Control caption "Alias" for "Appearance_Alias".
        /// </summary>
        public readonly string Appearance_Alias = nameof(Appearance_Alias);
        public readonly string Appearance_Condition = nameof(Appearance_Condition);

        /// <summary>
        ///     Control caption "Background" for "Appearance_Background".
        /// </summary>
        public readonly string Appearance_Background = nameof(Appearance_Background);

        /// <summary>
        ///     Control caption "Foreground" for "Appearance_Foreground".
        /// </summary>
        public readonly string Appearance_Foreground = nameof(Appearance_Foreground);

        /// <summary>
        ///     Control caption "ToolTip" for "Appearance_ToolTip".
        /// </summary>
        public readonly string Appearance_ToolTip = nameof(Appearance_ToolTip);

        /// <summary>
        ///     Control caption "HorizontalAlignment" for "Appearance_HorizontalAlignment".
        /// </summary>
        public readonly string Appearance_HorizontalAlignment = nameof(Appearance_HorizontalAlignment);

        /// <summary>
        ///     Control caption "VerticalAlignment" for "Appearance_VerticalAlignment".
        /// </summary>
        public readonly string Appearance_VerticalAlignment = nameof(Appearance_VerticalAlignment);

        /// <summary>
        ///     Control caption "TextAlignment" for "Appearance_TextAlignment".
        /// </summary>
        public readonly string Appearance_TextAlignment = nameof(Appearance_TextAlignment);

        /// <summary>
        ///     Control caption "FontFamily" for "Font_FontFamily".
        /// </summary>
        public readonly string Font_FontFamily = nameof(Font_FontFamily);

        /// <summary>
        ///     Control caption "FontFamilyUri" for "Font_FontFamilyUri".
        /// </summary>
        public readonly string Font_FontFamilyUri = nameof(Font_FontFamilyUri);

        /// <summary>
        ///     Control caption "FontSize" for "Font_FontSize".
        /// </summary>
        public readonly string Font_FontSize = nameof(Font_FontSize);

        /// <summary>
        ///     Control caption "FontStretch" for "Font_FontStretch".
        /// </summary>
        public readonly string Font_FontStretch = nameof(Font_FontStretch);

        /// <summary>
        ///     Control caption "FontStyle" for "Font_FontStyle".
        /// </summary>
        public readonly string Font_FontStyle = nameof(Font_FontStyle);

        /// <summary>
        ///     Control caption "FontWeight" for "Font_FontWeight".
        /// </summary>
        public readonly string Font_FontWeight = nameof(Font_FontWeight);
        public readonly string Reference_Description = nameof(Reference_Description);

        /// <summary>
        ///     Control caption "ColPrefix" for "Reference_ColPrefix".
        /// </summary>
        public readonly string Reference_ColPrefix = nameof(Reference_ColPrefix);
        public readonly string Reference_Condition = nameof(Reference_Condition);

        /// <summary>
        ///     Control caption "RefSection" for "Reference_Alias".
        /// </summary>
        public readonly string Reference_Alias = nameof(Reference_Alias);

        /// <summary>
        ///     Control caption "DisplayValueColumn" for "Reference_DisplayValueColumn".
        /// </summary>
        public readonly string Reference_DisplayValueColumn = nameof(Reference_DisplayValueColumn);
        public readonly string Subset_Description = nameof(Subset_Description);

        /// <summary>
        ///     Control caption "Alias" for "Subset_Alias".
        /// </summary>
        public readonly string Subset_Alias = nameof(Subset_Alias);

        /// <summary>
        ///     Control caption "Caption" for "Subset_Caption".
        /// </summary>
        public readonly string Subset_Caption = nameof(Subset_Caption);
        public readonly string Subset_Condition = nameof(Subset_Condition);

        /// <summary>
        ///     Control caption "CaptionColumn" for "Subset_CaptionColumn".
        /// </summary>
        public readonly string Subset_CaptionColumn = nameof(Subset_CaptionColumn);

        /// <summary>
        ///     Control caption "CountColumn" for "Subset_CountColumn".
        /// </summary>
        public readonly string Subset_CountColumn = nameof(Subset_CountColumn);

        /// <summary>
        ///     Control caption "HideZeroCount" for "Subset_HideZeroCount".
        /// </summary>
        public readonly string Subset_HideZeroCount = nameof(Subset_HideZeroCount);

        /// <summary>
        ///     Control caption "RefColumn" for "Subset_RefColumn".
        /// </summary>
        public readonly string Subset_RefColumn = nameof(Subset_RefColumn);

        /// <summary>
        ///     Control caption "RefParam" for "Subset_RefParam".
        /// </summary>
        public readonly string Subset_RefParam = nameof(Subset_RefParam);

        /// <summary>
        ///     Control caption "TreeHasChildrenColumn" for "Subset_TreeHasChildrenColumn".
        /// </summary>
        public readonly string Subset_TreeHasChildrenColumn = nameof(Subset_TreeHasChildrenColumn);

        /// <summary>
        ///     Control caption "TreeRefParam" for "Subset_TreeRefParam".
        /// </summary>
        public readonly string Subset_TreeRefParam = nameof(Subset_TreeRefParam);

        /// <summary>
        ///     Control caption "Kind" for "Subset_Kind".
        /// </summary>
        public readonly string Subset_Kind = nameof(Subset_Kind);
        public readonly string Extension_Description = nameof(Extension_Description);
        public readonly string Extension_Condition = nameof(Extension_Condition);
        public readonly string Extension_Order = nameof(Extension_Order);
        public readonly string Extension_TypeName = nameof(Extension_TypeName);
        public readonly string Extension_TypeDescription = nameof(Extension_TypeDescription);
        public readonly string Extension_TypeSettings = nameof(Extension_TypeSettings);
        public readonly string Extension_Container = nameof(Extension_Container);

        /// <summary>
        ///     Control caption "PropertiesContainer" for "PropertiesContainer".
        /// </summary>
        public readonly string PropertiesContainer = nameof(PropertiesContainer);

        /// <summary>
        ///     Control caption "OtherSettingsContainer" for "OtherSettingsContainer".
        /// </summary>
        public readonly string OtherSettingsContainer = nameof(OtherSettingsContainer);

        /// <summary>
        ///     Control caption "TagsPosition" for "Tags_TagsPosition".
        /// </summary>
        public readonly string Tags_TagsPosition = nameof(Tags_TagsPosition);

        #endregion

        #region ToString

        public static implicit operator string(MetadataEditorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Metarole

    /// <summary>
    ///     ID: {c6c9e585-c053-0aa0-994a-f80225f8585f}
    ///     Alias: Metarole
    ///     Caption: $CardTypes_TypesNames_Metarole
    ///     Group: Roles
    /// </summary>
    public class MetaroleTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Metarole": {c6c9e585-c053-0aa0-994a-f80225f8585f}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc6c9e585,0xc053,0x0aa0,0x99,0x4a,0xf8,0x02,0x25,0xf8,0x58,0x5f);

        /// <summary>
        ///     Card type name for "Metarole".
        /// </summary>
        public readonly string Alias = "Metarole";

        /// <summary>
        ///     Card type caption for "Metarole".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Metarole";

        /// <summary>
        ///     Card type group for "Metarole".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region Forms

        public readonly string FormMetarole = "Metarole";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockUsers = "Users";

        #endregion

        #region Controls

        public readonly string TimeZone = nameof(TimeZone);
        public readonly string RoleUsersLimitDisclaimer = nameof(RoleUsersLimitDisclaimer);

        #endregion

        #region ToString

        public static implicit operator string(MetaroleTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region NestedRole

    /// <summary>
    ///     ID: {f33a4b0d-dbd8-4af7-a199-6802a77498bb}
    ///     Alias: NestedRole
    ///     Caption: $CardTypes_TypesNames_NestedRole
    ///     Group: Roles
    /// </summary>
    public class NestedRoleTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "NestedRole": {f33a4b0d-dbd8-4af7-a199-6802a77498bb}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf33a4b0d,0xdbd8,0x4af7,0xa1,0x99,0x68,0x02,0xa7,0x74,0x98,0xbb);

        /// <summary>
        ///     Card type name for "NestedRole".
        /// </summary>
        public readonly string Alias = "NestedRole";

        /// <summary>
        ///     Card type caption for "NestedRole".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_NestedRole";

        /// <summary>
        ///     Card type group for "NestedRole".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region Forms

        public readonly string FormNestedRole = "NestedRole";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockUsers = "Users";

        #endregion

        #region Controls

        public readonly string TimeZone = nameof(TimeZone);
        public readonly string RoleUsersLimitDisclaimer = nameof(RoleUsersLimitDisclaimer);

        #endregion

        #region ToString

        public static implicit operator string(NestedRoleTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Notification

    /// <summary>
    ///     ID: {d3087e3c-a2da-4cc7-a92d-d5cf17e48d3f}
    ///     Alias: Notification
    ///     Caption: $CardTypes_TypesNames_Notification
    ///     Group: Dictionaries
    /// </summary>
    public class NotificationTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Notification": {d3087e3c-a2da-4cc7-a92d-d5cf17e48d3f}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd3087e3c,0xa2da,0x4cc7,0xa9,0x2d,0xd5,0xcf,0x17,0xe4,0x8d,0x3f);

        /// <summary>
        ///     Card type name for "Notification".
        /// </summary>
        public readonly string Alias = "Notification";

        /// <summary>
        ///     Card type caption for "Notification".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Notification";

        /// <summary>
        ///     Card type group for "Notification".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormNotification = "Notification";

        #endregion

        #region Blocks

        public readonly string BlockBlock = "Block";
        public readonly string BlockAliasMetadataBlock = "AliasMetadataBlock";
        public readonly string BlockNotificationBlock = "NotificationBlock";

        #endregion

        #region Controls

        public readonly string CompileButton = nameof(CompileButton);

        #endregion

        #region ToString

        public static implicit operator string(NotificationTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region NotificationSubscriptions

    /// <summary>
    ///     ID: {4d92f912-2907-4e3c-bce5-7767a0f98bf8}
    ///     Alias: NotificationSubscriptions
    ///     Caption: $CardTypes_TypesNames_NotificationSubscriptions
    ///     Group: System
    /// </summary>
    public class NotificationSubscriptionsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "NotificationSubscriptions": {4d92f912-2907-4e3c-bce5-7767a0f98bf8}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4d92f912,0x2907,0x4e3c,0xbc,0xe5,0x77,0x67,0xa0,0xf9,0x8b,0xf8);

        /// <summary>
        ///     Card type name for "NotificationSubscriptions".
        /// </summary>
        public readonly string Alias = "NotificationSubscriptions";

        /// <summary>
        ///     Card type caption for "NotificationSubscriptions".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_NotificationSubscriptions";

        /// <summary>
        ///     Card type group for "NotificationSubscriptions".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormNotificationSubscriptionsFull = "NotificationSubscriptionsFull";
        public readonly string FormNotificationSubscriptionsShort = "NotificationSubscriptionsShort";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(NotificationSubscriptionsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region NotificationType

    /// <summary>
    ///     ID: {813234a7-3b29-4845-be9d-b5bece5f7c1c}
    ///     Alias: NotificationType
    ///     Caption: $CardTypes_TypesNames_NotificationType
    ///     Group: Dictionaries
    /// </summary>
    public class NotificationTypeTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "NotificationType": {813234a7-3b29-4845-be9d-b5bece5f7c1c}.
        /// </summary>
        public readonly Guid ID = new Guid(0x813234a7,0x3b29,0x4845,0xbe,0x9d,0xb5,0xbe,0xce,0x5f,0x7c,0x1c);

        /// <summary>
        ///     Card type name for "NotificationType".
        /// </summary>
        public readonly string Alias = "NotificationType";

        /// <summary>
        ///     Card type caption for "NotificationType".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_NotificationType";

        /// <summary>
        ///     Card type group for "NotificationType".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormNotificationType = "NotificationType";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string IsGlobal = nameof(IsGlobal);
        public readonly string CanSubscribe = nameof(CanSubscribe);
        public readonly string CardTypes = nameof(CardTypes);

        #endregion

        #region ToString

        public static implicit operator string(NotificationTypeTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region NotificationUserSettings

    /// <summary>
    ///     ID: {193c278d-7178-4ee3-a73b-438357d69d2a}
    ///     Alias: NotificationUserSettings
    ///     Caption: $Cards_DefaultCaption
    ///     Group: UserSettings
    /// </summary>
    public class NotificationUserSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "NotificationUserSettings": {193c278d-7178-4ee3-a73b-438357d69d2a}.
        /// </summary>
        public readonly Guid ID = new Guid(0x193c278d,0x7178,0x4ee3,0xa7,0x3b,0x43,0x83,0x57,0xd6,0x9d,0x2a);

        /// <summary>
        ///     Card type name for "NotificationUserSettings".
        /// </summary>
        public readonly string Alias = "NotificationUserSettings";

        /// <summary>
        ///     Card type caption for "NotificationUserSettings".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "NotificationUserSettings".
        /// </summary>
        public readonly string Group = "UserSettings";

        #endregion

        #region Forms

        public readonly string FormNotificationUserSettings = "NotificationUserSettings";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        public readonly string BlockBlock2 = "Block2";

        /// <summary>
        ///     Block caption "Block2" for "Rules".
        /// </summary>
        public readonly string BlockRules = "Rules";

        #endregion

        #region Controls

        public readonly string NotificationTypes = nameof(NotificationTypes);

        /// <summary>
        ///     Control caption "Control1" for "DescriptionLabel".
        /// </summary>
        public readonly string DescriptionLabel = nameof(DescriptionLabel);
        public readonly string Rules = nameof(Rules);

        #endregion

        #region ToString

        public static implicit operator string(NotificationUserSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region OcrOperation

    /// <summary>
    ///     ID: {8275fa2d-91d6-462f-8a1a-189c9b57720e}
    ///     Alias: OcrOperation
    ///     Caption: $CardTypes_TypesNames_OcrOperation
    ///     Group: System
    /// </summary>
    public class OcrOperationTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "OcrOperation": {8275fa2d-91d6-462f-8a1a-189c9b57720e}.
        /// </summary>
        public readonly Guid ID = new Guid(0x8275fa2d,0x91d6,0x462f,0x8a,0x1a,0x18,0x9c,0x9b,0x57,0x72,0x0e);

        /// <summary>
        ///     Card type name for "OcrOperation".
        /// </summary>
        public readonly string Alias = "OcrOperation";

        /// <summary>
        ///     Card type caption for "OcrOperation".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_OcrOperation";

        /// <summary>
        ///     Card type group for "OcrOperation".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormVerificationTab = "VerificationTab";
        public readonly string FormCompareFilesTab = "CompareFilesTab";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Verification" for "VerificationBlock".
        /// </summary>
        public readonly string BlockVerificationBlock = "VerificationBlock";

        /// <summary>
        ///     Block caption "Recognition results" for "RecognitionResultsBlock".
        /// </summary>
        public readonly string BlockRecognitionResultsBlock = "RecognitionResultsBlock";

        /// <summary>
        ///     Block caption "Files preview" for "FilesPreviewBlock".
        /// </summary>
        public readonly string BlockFilesPreviewBlock = "FilesPreviewBlock";

        /// <summary>
        ///     Block caption "First files" for "FirstFilesBlock".
        /// </summary>
        public readonly string BlockFirstFilesBlock = "FirstFilesBlock";

        /// <summary>
        ///     Block caption "Second files" for "SecondFilesBlock".
        /// </summary>
        public readonly string BlockSecondFilesBlock = "SecondFilesBlock";

        /// <summary>
        ///     Block caption "First preview" for "FirstPreviewBlock".
        /// </summary>
        public readonly string BlockFirstPreviewBlock = "FirstPreviewBlock";

        /// <summary>
        ///     Block caption "Second preview" for "SecondPreviewBlock".
        /// </summary>
        public readonly string BlockSecondPreviewBlock = "SecondPreviewBlock";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "Text layer exist hint" for "TextLayerHint".
        /// </summary>
        public readonly string TextLayerHint = nameof(TextLayerHint);

        public readonly string Text = nameof(Text);
        public readonly string FirstLanguage = nameof(FirstLanguage);
        public readonly string FirstConfidence = nameof(FirstConfidence);
        public readonly string FirstText = nameof(FirstText);
        public readonly string FirstResults = nameof(FirstResults);

        /// <summary>
        ///     Control caption "Files preview" for "Preview".
        /// </summary>
        public readonly string Preview = nameof(Preview);
        public readonly string FirstFiles = nameof(FirstFiles);
        public readonly string SecondFiles = nameof(SecondFiles);

        /// <summary>
        ///     Control caption "First preview" for "FirstPreview".
        /// </summary>
        public readonly string FirstPreview = nameof(FirstPreview);

        /// <summary>
        ///     Control caption "Second preview" for "SecondPreview".
        /// </summary>
        public readonly string SecondPreview = nameof(SecondPreview);

        #endregion

        #region ToString

        public static implicit operator string(OcrOperationTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region OcrRequestDialog

    /// <summary>
    ///     ID: {51488d15-c19c-4855-8bec-7cc26936e9d6}
    ///     Alias: OcrRequestDialog
    ///     Caption: $CardTypes_OcrRequestDialog
    ///     Group: System
    /// </summary>
    public class OcrRequestDialogTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "OcrRequestDialog": {51488d15-c19c-4855-8bec-7cc26936e9d6}.
        /// </summary>
        public readonly Guid ID = new Guid(0x51488d15,0xc19c,0x4855,0x8b,0xec,0x7c,0xc2,0x69,0x36,0xe9,0xd6);

        /// <summary>
        ///     Card type name for "OcrRequestDialog".
        /// </summary>
        public readonly string Alias = "OcrRequestDialog";

        /// <summary>
        ///     Card type caption for "OcrRequestDialog".
        /// </summary>
        public readonly string Caption = "$CardTypes_OcrRequestDialog";

        /// <summary>
        ///     Card type group for "OcrRequestDialog".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormOcrRequestDialog = "OcrRequestDialog";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Recognition request" for "RecognitionRequestBlock".
        /// </summary>
        public readonly string BlockRecognitionRequestBlock = "RecognitionRequestBlock";

        #endregion

        #region Controls

        public readonly string SegmentationMode = nameof(SegmentationMode);
        public readonly string Confidence = nameof(Confidence);
        public readonly string Languages = nameof(Languages);

        /// <summary>
        ///     Control caption "Text layer possibility exist hint" for "TextLayerHint".
        /// </summary>
        public readonly string TextLayerHint = nameof(TextLayerHint);
        public readonly string Overwrite = nameof(Overwrite);

        #endregion

        #region ToString

        public static implicit operator string(OcrRequestDialogTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region OcrSettings

    /// <summary>
    ///     ID: {b3c9c077-8f51-47fc-ba36-0247f71d6b0f}
    ///     Alias: OcrSettings
    ///     Caption: $CardTypes_TypesNames_OcrSettings
    ///     Group: Settings
    /// </summary>
    public class OcrSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "OcrSettings": {b3c9c077-8f51-47fc-ba36-0247f71d6b0f}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb3c9c077,0x8f51,0x47fc,0xba,0x36,0x02,0x47,0xf7,0x1d,0x6b,0x0f);

        /// <summary>
        ///     Card type name for "OcrSettings".
        /// </summary>
        public readonly string Alias = "OcrSettings";

        /// <summary>
        ///     Card type caption for "OcrSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_OcrSettings";

        /// <summary>
        ///     Card type group for "OcrSettings".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormRecognitionTab = "RecognitionTab";
        public readonly string FormMappingTab = "MappingTab";
        public readonly string FormPatternsTab = "PatternsTab";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "RecognitionSettingsBlock" for "Recognition settings".
        /// </summary>
        public readonly string BlockRecognitionSettings = "Recognition settings";

        /// <summary>
        ///     Block caption "Mapping settings" for "MappingSettingsBlock".
        /// </summary>
        public readonly string BlockMappingSettingsBlock = "MappingSettingsBlock";

        public readonly string BlockMainInfoBlock = "MainInfoBlock";

        /// <summary>
        ///     Block caption "Patterns settings" for "PatternsSettingsBlock".
        /// </summary>
        public readonly string BlockPatternsSettingsBlock = "PatternsSettingsBlock";
        public readonly string BlockFieldsMappingTypeSettingBlock = "FieldsMappingTypeSettingBlock";
        public readonly string BlockFieldsMappingSectionSettingBlock = "FieldsMappingSectionSettingBlock";
        public readonly string BlockFieldMappingSettingBlock = "FieldMappingSettingBlock";
        public readonly string BlockFieldMappingSettingBlock1 = "FieldMappingSettingBlock1";

        #endregion

        #region Controls

        public readonly string FieldsMappingTypesSettings = nameof(FieldsMappingTypesSettings);
        public readonly string FieldsMappingSectionsSettings = nameof(FieldsMappingSectionsSettings);
        public readonly string FieldsMappingSettings = nameof(FieldsMappingSettings);

        #endregion

        #region ToString

        public static implicit operator string(OcrSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region OnlyOfficeSettings

    /// <summary>
    ///     ID: {4d17a80b-c711-4dce-8552-c9ddb30dd7ce}
    ///     Alias: OnlyOfficeSettings
    ///     Caption: $CardTypes_TypesNames_OnlyOffice
    ///     Group: Settings
    /// </summary>
    public class OnlyOfficeSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "OnlyOfficeSettings": {4d17a80b-c711-4dce-8552-c9ddb30dd7ce}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4d17a80b,0xc711,0x4dce,0x85,0x52,0xc9,0xdd,0xb3,0x0d,0xd7,0xce);

        /// <summary>
        ///     Card type name for "OnlyOfficeSettings".
        /// </summary>
        public readonly string Alias = "OnlyOfficeSettings";

        /// <summary>
        ///     Card type caption for "OnlyOfficeSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_OnlyOffice";

        /// <summary>
        ///     Card type group for "OnlyOfficeSettings".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormOnlyOfficeSettings = "OnlyOfficeSettings";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Settings" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(OnlyOfficeSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region OpenInModalDialogSettings

    /// <summary>
    ///     ID: {57493ed6-316b-4fb9-b85e-2b931e022640}
    ///     Alias: OpenInModalDialogSettings
    ///     Caption: $Cards_DefaultCaption
    ///     Group: System
    /// </summary>
    public class OpenInModalDialogSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "OpenInModalDialogSettings": {57493ed6-316b-4fb9-b85e-2b931e022640}.
        /// </summary>
        public readonly Guid ID = new Guid(0x57493ed6,0x316b,0x4fb9,0xb8,0x5e,0x2b,0x93,0x1e,0x02,0x26,0x40);

        /// <summary>
        ///     Card type name for "OpenInModalDialogSettings".
        /// </summary>
        public readonly string Alias = "OpenInModalDialogSettings";

        /// <summary>
        ///     Card type caption for "OpenInModalDialogSettings".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "OpenInModalDialogSettings".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormMainForm = "MainForm";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "MainBlock" for "MainBlock".
        /// </summary>
        public readonly string BlockMainBlock = "MainBlock";

        #endregion

        #region Controls

        public readonly string OpenInFullscreen = nameof(OpenInFullscreen);
        public readonly string OpenOnlyFirstTab = nameof(OpenOnlyFirstTab);
        public readonly string RefreshViewOnClose = nameof(RefreshViewOnClose);

        #endregion

        #region ToString

        public static implicit operator string(OpenInModalDialogSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Operation

    /// <summary>
    ///     ID: {f2517b14-035b-4451-b4a1-605bf7a764a4}
    ///     Alias: Operation
    ///     Caption: $CardTypes_TypesNames_Operation
    ///     Group: System
    /// </summary>
    public class OperationTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Operation": {f2517b14-035b-4451-b4a1-605bf7a764a4}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf2517b14,0x035b,0x4451,0xb4,0xa1,0x60,0x5b,0xf7,0xa7,0x64,0xa4);

        /// <summary>
        ///     Card type name for "Operation".
        /// </summary>
        public readonly string Alias = "Operation";

        /// <summary>
        ///     Card type caption for "Operation".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Operation";

        /// <summary>
        ///     Card type group for "Operation".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormOperation = "Operation";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";

        /// <summary>
        ///     Block caption "Dates" for "Dates".
        /// </summary>
        public readonly string BlockDates = "Dates";
        public readonly string BlockChangesInfo = "ChangesInfo";

        #endregion

        #region Controls

        public readonly string Category = nameof(Category);

        #endregion

        #region ToString

        public static implicit operator string(OperationTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Outgoing

    /// <summary>
    ///     ID: {c59b76d9-c0db-01cd-a3fb-b339740f0620}
    ///     Alias: Outgoing
    ///     Caption: $CardTypes_TypesNames_Outgoing
    ///     Group: Documents
    /// </summary>
    public class OutgoingTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Outgoing": {c59b76d9-c0db-01cd-a3fb-b339740f0620}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc59b76d9,0xc0db,0x01cd,0xa3,0xfb,0xb3,0x39,0x74,0x0f,0x06,0x20);

        /// <summary>
        ///     Card type name for "Outgoing".
        /// </summary>
        public readonly string Alias = "Outgoing";

        /// <summary>
        ///     Card type caption for "Outgoing".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Outgoing";

        /// <summary>
        ///     Card type group for "Outgoing".
        /// </summary>
        public readonly string Group = "Documents";

        #endregion

        #region Document Types

        /// <summary>
        ///     Document type identifier for "$KrTypes_DocTypes_Outgoing": {13597b5f-d25d-4385-9e36-43f152914719}.
        /// </summary>
        public readonly Guid KrTypes_DocTypes_OutgoingID = new(0x13597b5f,0xd25d,0x4385,0x9e,0x36,0x43,0xf1,0x52,0x91,0x47,0x19);

        /// <summary>
        ///     Document type caption for "$KrTypes_DocTypes_Outgoing".
        /// </summary>
        public readonly string KrTypes_DocTypes_OutgoingCaption = "$KrTypes_DocTypes_Outgoing";

        #endregion

        #region Forms

        public readonly string FormOutgoing = "Outgoing";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockBlock4 = "Block4";
        public readonly string BlockDepartmentSignedByRecipientsPerformersBlock = "DepartmentSignedByRecipientsPerformersBlock";
        public readonly string BlockRefsBlock = "RefsBlock";
        public readonly string BlockBlock5 = "Block5";

        #endregion

        #region Controls

        public readonly string PartnerControl = nameof(PartnerControl);
        public readonly string IncomingRefsControl = nameof(IncomingRefsControl);
        public readonly string Files = nameof(Files);

        #endregion

        #region ToString

        public static implicit operator string(OutgoingTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Partner

    /// <summary>
    ///     ID: {b9a1f125-ab1d-4cff-929f-5ad8351bda4f}
    ///     Alias: Partner
    ///     Caption: $CardTypes_TypesNames_Partner
    ///     Group: Dictionaries
    /// </summary>
    public class PartnerTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Partner": {b9a1f125-ab1d-4cff-929f-5ad8351bda4f}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb9a1f125,0xab1d,0x4cff,0x92,0x9f,0x5a,0xd8,0x35,0x1b,0xda,0x4f);

        /// <summary>
        ///     Card type name for "Partner".
        /// </summary>
        public readonly string Alias = "Partner";

        /// <summary>
        ///     Card type caption for "Partner".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Partner";

        /// <summary>
        ///     Card type group for "Partner".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormPartner = "Partner";
        public readonly string FormPartnerContacts = "PartnerContacts";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock5 = "Block5";

        #endregion

        #region Controls

        public readonly string Files = nameof(Files);

        #endregion

        #region ToString

        public static implicit operator string(PartnerTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region PartnerCondition

    /// <summary>
    ///     ID: {57c6e535-ad4b-4e03-8c06-2d3fbc59e1b8}
    ///     Alias: PartnerCondition
    ///     Caption: $CardTypes_TypesNames_PartnerCondition
    ///     Group: Conditions
    /// </summary>
    public class PartnerConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "PartnerCondition": {57c6e535-ad4b-4e03-8c06-2d3fbc59e1b8}.
        /// </summary>
        public readonly Guid ID = new Guid(0x57c6e535,0xad4b,0x4e03,0x8c,0x06,0x2d,0x3f,0xbc,0x59,0xe1,0xb8);

        /// <summary>
        ///     Card type name for "PartnerCondition".
        /// </summary>
        public readonly string Alias = "PartnerCondition";

        /// <summary>
        ///     Card type caption for "PartnerCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_PartnerCondition";

        /// <summary>
        ///     Card type group for "PartnerCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormPartnerCondition = "PartnerCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(PartnerConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region PersonalizationUserSettings

    /// <summary>
    ///     ID: {3bd67bff-6990-4026-aa7f-b789ba8a8744}
    ///     Alias: PersonalizationUserSettings
    ///     Caption: $CardTypes_Tabs_Personalization
    ///     Group: UserSettings
    /// </summary>
    public class PersonalizationUserSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "PersonalizationUserSettings": {3bd67bff-6990-4026-aa7f-b789ba8a8744}.
        /// </summary>
        public readonly Guid ID = new Guid(0x3bd67bff,0x6990,0x4026,0xaa,0x7f,0xb7,0x89,0xba,0x8a,0x87,0x44);

        /// <summary>
        ///     Card type name for "PersonalizationUserSettings".
        /// </summary>
        public readonly string Alias = "PersonalizationUserSettings";

        /// <summary>
        ///     Card type caption for "PersonalizationUserSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_Tabs_Personalization";

        /// <summary>
        ///     Card type group for "PersonalizationUserSettings".
        /// </summary>
        public readonly string Group = "UserSettings";

        #endregion

        #region Forms

        public readonly string FormPersonalization = "Personalization";

        #endregion

        #region Blocks

        public readonly string BlockPersonalization = "Personalization";
        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockFrequentlyUsedEmoji = "FrequentlyUsedEmoji";

        #endregion

        #region Controls

        public readonly string Emojis = nameof(Emojis);
        public readonly string ResetEmojis = nameof(ResetEmojis);

        #endregion

        #region ToString

        public static implicit operator string(PersonalizationUserSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region PersonalRole

    /// <summary>
    ///     ID: {929ad23c-8a22-09aa-9000-398bf13979b2}
    ///     Alias: PersonalRole
    ///     Caption: $CardTypes_TypesNames_PersonalRole
    ///     Group: Roles
    /// </summary>
    public class PersonalRoleTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "PersonalRole": {929ad23c-8a22-09aa-9000-398bf13979b2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x929ad23c,0x8a22,0x09aa,0x90,0x00,0x39,0x8b,0xf1,0x39,0x79,0xb2);

        /// <summary>
        ///     Card type name for "PersonalRole".
        /// </summary>
        public readonly string Alias = "PersonalRole";

        /// <summary>
        ///     Card type caption for "PersonalRole".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_PersonalRole";

        /// <summary>
        ///     Card type group for "PersonalRole".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region Forms

        public readonly string FormPersonalRole = "PersonalRole";

        #endregion

        #region Blocks

        public readonly string BlockNameInfo = "NameInfo";
        public readonly string BlockRoles = "Roles";
        public readonly string BlockLoginInfo = "LoginInfo";
        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockUserAccessSecurity = "UserAccessSecurity";
        public readonly string BlockAdInfo = "AdInfo";
        public readonly string BlockUsers = "Users";
        public readonly string BlockFiles = "Files";

        #endregion

        #region Controls

        public readonly string Login = nameof(Login);

        /// <summary>
        ///     Control caption "Warning label" for "WarningLabel".
        /// </summary>
        public readonly string WarningLabel = nameof(WarningLabel);
        public readonly string Password = nameof(Password);
        public readonly string PasswordRepeat = nameof(PasswordRepeat);
        public readonly string TimeZone = nameof(TimeZone);
        public readonly string InheritTimeZone = nameof(InheritTimeZone);
        public readonly string AdSyncDate = nameof(AdSyncDate);
        public readonly string AdSyncWhenChanged = nameof(AdSyncWhenChanged);
        public readonly string AdSyncDistinguishedName = nameof(AdSyncDistinguishedName);
        public readonly string AdSyncID = nameof(AdSyncID);
        public readonly string AdSyncDisableUpdate = nameof(AdSyncDisableUpdate);
        public readonly string AdSyncIndependent = nameof(AdSyncIndependent);
        public readonly string AdSyncManualSync = nameof(AdSyncManualSync);
        public readonly string Files = nameof(Files);

        #endregion

        #region ToString

        public static implicit operator string(PersonalRoleTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region PersonalRoleSatellite

    /// <summary>
    ///     ID: {f6c54fed-0bee-4d61-980a-8057179289ea}
    ///     Alias: PersonalRoleSatellite
    ///     Caption: $CardTypes_TypesNames_PersonalRoleSatellite
    ///     Group: Roles
    /// </summary>
    public class PersonalRoleSatelliteTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "PersonalRoleSatellite": {f6c54fed-0bee-4d61-980a-8057179289ea}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf6c54fed,0x0bee,0x4d61,0x98,0x0a,0x80,0x57,0x17,0x92,0x89,0xea);

        /// <summary>
        ///     Card type name for "PersonalRoleSatellite".
        /// </summary>
        public readonly string Alias = "PersonalRoleSatellite";

        /// <summary>
        ///     Card type caption for "PersonalRoleSatellite".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_PersonalRoleSatellite";

        /// <summary>
        ///     Card type group for "PersonalRoleSatellite".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region ToString

        public static implicit operator string(PersonalRoleSatelliteTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Protocol

    /// <summary>
    ///     ID: {4d9f9590-0131-4d32-9710-5e07c282b5d3}
    ///     Alias: Protocol
    ///     Caption: $CardTypes_TypesNames_Protocol
    ///     Group: Documents
    /// </summary>
    public class ProtocolTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Protocol": {4d9f9590-0131-4d32-9710-5e07c282b5d3}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4d9f9590,0x0131,0x4d32,0x97,0x10,0x5e,0x07,0xc2,0x82,0xb5,0xd3);

        /// <summary>
        ///     Card type name for "Protocol".
        /// </summary>
        public readonly string Alias = "Protocol";

        /// <summary>
        ///     Card type caption for "Protocol".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Protocol";

        /// <summary>
        ///     Card type group for "Protocol".
        /// </summary>
        public readonly string Group = "Documents";

        #endregion

        #region Forms

        public readonly string FormProtocol = "Protocol";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock6 = "Block6";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockBlock7 = "Block7";
        public readonly string BlockBlock4 = "Block4";
        public readonly string BlockDepartmentSignedByRecipientsPerformersBlock = "DepartmentSignedByRecipientsPerformersBlock";
        public readonly string BlockRefsBlock = "RefsBlock";
        public readonly string BlockBlock8 = "Block8";
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock5 = "Block5";

        #endregion

        #region Controls

        public readonly string IncomingRefsControl = nameof(IncomingRefsControl);
        public readonly string Files = nameof(Files);

        #endregion

        #region ToString

        public static implicit operator string(ProtocolTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ReportPermissions

    /// <summary>
    ///     ID: {65a88390-3b00-4b74-925d-b635027feff2}
    ///     Alias: ReportPermissions
    ///     Caption: $CardTypes_TypesNames_ReportPermissions
    ///     Group: Settings
    /// </summary>
    public class ReportPermissionsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "ReportPermissions": {65a88390-3b00-4b74-925d-b635027feff2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x65a88390,0x3b00,0x4b74,0x92,0x5d,0xb6,0x35,0x02,0x7f,0xef,0xf2);

        /// <summary>
        ///     Card type name for "ReportPermissions".
        /// </summary>
        public readonly string Alias = "ReportPermissions";

        /// <summary>
        ///     Card type caption for "ReportPermissions".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ReportPermissions";

        /// <summary>
        ///     Card type group for "ReportPermissions".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormReportPermissions = "ReportPermissions";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockCurrentReportsPermissions = "CurrentReportsPermissions";

        #endregion

        #region ToString

        public static implicit operator string(ReportPermissionsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region RoleDeputiesManagement

    /// <summary>
    ///     ID: {cb931209-2ad9-4370-bb3c-3172e61937ba}
    ///     Alias: RoleDeputiesManagement
    ///     Caption: $CardTypes_TypesNames_RoleDeputiesManagement
    ///     Group: Roles
    /// </summary>
    public class RoleDeputiesManagementTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "RoleDeputiesManagement": {cb931209-2ad9-4370-bb3c-3172e61937ba}.
        /// </summary>
        public readonly Guid ID = new Guid(0xcb931209,0x2ad9,0x4370,0xbb,0x3c,0x31,0x72,0xe6,0x19,0x37,0xba);

        /// <summary>
        ///     Card type name for "RoleDeputiesManagement".
        /// </summary>
        public readonly string Alias = "RoleDeputiesManagement";

        /// <summary>
        ///     Card type caption for "RoleDeputiesManagement".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_RoleDeputiesManagement";

        /// <summary>
        ///     Card type group for "RoleDeputiesManagement".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region Forms

        public readonly string FormRoleDeputiesManagement = "RoleDeputiesManagement";

        #endregion

        #region Blocks

        public readonly string BlockDeputies = "Deputies";
        public readonly string BlockPeriod = "Period";
        public readonly string BlockDeputized = "Deputized";

        #endregion

        #region Controls

        public readonly string Roles = nameof(Roles);
        public readonly string DeputizingPermanent = nameof(DeputizingPermanent);
        public readonly string DeputizingStart = nameof(DeputizingStart);
        public readonly string DeputizingEnd = nameof(DeputizingEnd);
        public readonly string RoleDeputiesManagement = nameof(RoleDeputiesManagement);
        public readonly string RoleDeputiesNestedManagement = nameof(RoleDeputiesNestedManagement);
        public readonly string RoleDeputiesManagementDeputized = nameof(RoleDeputiesManagementDeputized);
        public readonly string RoleDeputiesManagementDeputizedView = nameof(RoleDeputiesManagementDeputizedView);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesManagementTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region RoleGenerator

    /// <summary>
    ///     ID: {890379d8-651e-01d9-85c7-12644b5364b8}
    ///     Alias: RoleGenerator
    ///     Caption: $CardTypes_TypesNames_RoleGenerator
    ///     Group: Roles
    /// </summary>
    public class RoleGeneratorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "RoleGenerator": {890379d8-651e-01d9-85c7-12644b5364b8}.
        /// </summary>
        public readonly Guid ID = new Guid(0x890379d8,0x651e,0x01d9,0x85,0xc7,0x12,0x64,0x4b,0x53,0x64,0xb8);

        /// <summary>
        ///     Card type name for "RoleGenerator".
        /// </summary>
        public readonly string Alias = "RoleGenerator";

        /// <summary>
        ///     Card type caption for "RoleGenerator".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_RoleGenerator";

        /// <summary>
        ///     Card type group for "RoleGenerator".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region Forms

        public readonly string FormRoleGenerator = "RoleGenerator";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockErrorInfo = "ErrorInfo";

        #endregion

        #region Controls

        public readonly string RecalcButton = nameof(RecalcButton);

        #endregion

        #region ToString

        public static implicit operator string(RoleGeneratorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region RouteCondition

    /// <summary>
    ///     ID: {94a948b6-9cea-4a4a-9005-b0316d89ed6e}
    ///     Alias: RouteCondition
    ///     Caption: $CardTypes_TypesNames_RouteCondition
    ///     Group: Conditions
    /// </summary>
    public class RouteConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "RouteCondition": {94a948b6-9cea-4a4a-9005-b0316d89ed6e}.
        /// </summary>
        public readonly Guid ID = new Guid(0x94a948b6,0x9cea,0x4a4a,0x90,0x05,0xb0,0x31,0x6d,0x89,0xed,0x6e);

        /// <summary>
        ///     Card type name for "RouteCondition".
        /// </summary>
        public readonly string Alias = "RouteCondition";

        /// <summary>
        ///     Card type caption for "RouteCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_RouteCondition";

        /// <summary>
        ///     Card type group for "RouteCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormRouteCondition = "RouteCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(RouteConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region RowAddedCondition

    /// <summary>
    ///     ID: {ad464749-98a9-480f-a59b-41ef203d00ce}
    ///     Alias: RowAddedCondition
    ///     Caption: $CardTypes_TypesNames_RowAddedCondition
    ///     Group: Conditions
    /// </summary>
    public class RowAddedConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "RowAddedCondition": {ad464749-98a9-480f-a59b-41ef203d00ce}.
        /// </summary>
        public readonly Guid ID = new Guid(0xad464749,0x98a9,0x480f,0xa5,0x9b,0x41,0xef,0x20,0x3d,0x00,0xce);

        /// <summary>
        ///     Card type name for "RowAddedCondition".
        /// </summary>
        public readonly string Alias = "RowAddedCondition";

        /// <summary>
        ///     Card type caption for "RowAddedCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_RowAddedCondition";

        /// <summary>
        ///     Card type group for "RowAddedCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormRowAddedCondition = "RowAddedCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(RowAddedConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region RowDeletedCondition

    /// <summary>
    ///     ID: {9d705cf7-e108-449d-a6f8-92512eb8d4ea}
    ///     Alias: RowDeletedCondition
    ///     Caption: $CardTypes_TypesNames_RowDeletedCondition
    ///     Group: Conditions
    /// </summary>
    public class RowDeletedConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "RowDeletedCondition": {9d705cf7-e108-449d-a6f8-92512eb8d4ea}.
        /// </summary>
        public readonly Guid ID = new Guid(0x9d705cf7,0xe108,0x449d,0xa6,0xf8,0x92,0x51,0x2e,0xb8,0xd4,0xea);

        /// <summary>
        ///     Card type name for "RowDeletedCondition".
        /// </summary>
        public readonly string Alias = "RowDeletedCondition";

        /// <summary>
        ///     Card type caption for "RowDeletedCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_RowDeletedCondition";

        /// <summary>
        ///     Card type group for "RowDeletedCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormRowDeletedCondition = "RowDeletedCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(RowDeletedConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region SelectTag

    /// <summary>
    ///     ID: {b9bdea71-5b6f-494b-a878-afec21add8d0}
    ///     Alias: SelectTag
    ///     Caption: $Cards_DefaultCaption
    ///     Group: Tags
    /// </summary>
    public class SelectTagTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "SelectTag": {b9bdea71-5b6f-494b-a878-afec21add8d0}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb9bdea71,0x5b6f,0x494b,0xa8,0x78,0xaf,0xec,0x21,0xad,0xd8,0xd0);

        /// <summary>
        ///     Card type name for "SelectTag".
        /// </summary>
        public readonly string Alias = "SelectTag";

        /// <summary>
        ///     Card type caption for "SelectTag".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "SelectTag".
        /// </summary>
        public readonly string Group = "Tags";

        #endregion

        #region Forms

        public readonly string FormTab = "Tab";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "SelectTag" for "SelectTag".
        /// </summary>
        public readonly string BlockSelectTag = "SelectTag";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "Tags" for "Tags".
        /// </summary>
        public readonly string Tags = nameof(Tags);

        #endregion

        #region ToString

        public static implicit operator string(SelectTagTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Sequence

    /// <summary>
    ///     ID: {81e7e5a2-4745-4ccc-8178-547663d47737}
    ///     Alias: Sequence
    ///     Caption: $CardTypes_TypesNames_Sequence
    ///     Group: Settings
    /// </summary>
    public class SequenceTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Sequence": {81e7e5a2-4745-4ccc-8178-547663d47737}.
        /// </summary>
        public readonly Guid ID = new Guid(0x81e7e5a2,0x4745,0x4ccc,0x81,0x78,0x54,0x76,0x63,0xd4,0x77,0x37);

        /// <summary>
        ///     Card type name for "Sequence".
        /// </summary>
        public readonly string Alias = "Sequence";

        /// <summary>
        ///     Card type caption for "Sequence".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Sequence";

        /// <summary>
        ///     Card type group for "Sequence".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormSequence = "Sequence";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        /// <summary>
        ///     Block caption "Block2" for "Block2".
        /// </summary>
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region ToString

        public static implicit operator string(SequenceTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ServerInstance

    /// <summary>
    ///     ID: {7b891314-474f-4a60-8e0d-744dcb075209}
    ///     Alias: ServerInstance
    ///     Caption: $CardTypes_TypesNames_ServerInstance
    ///     Group: Settings
    /// </summary>
    public class ServerInstanceTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "ServerInstance": {7b891314-474f-4a60-8e0d-744dcb075209}.
        /// </summary>
        public readonly Guid ID = new Guid(0x7b891314,0x474f,0x4a60,0x8e,0x0d,0x74,0x4d,0xcb,0x07,0x52,0x09);

        /// <summary>
        ///     Card type name for "ServerInstance".
        /// </summary>
        public readonly string Alias = "ServerInstance";

        /// <summary>
        ///     Card type caption for "ServerInstance".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ServerInstance";

        /// <summary>
        ///     Card type group for "ServerInstance".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormServerInstance = "ServerInstance";
        public readonly string FormSecurity = "Security";
        public readonly string FormPaletteColors = "PaletteColors";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockForumInfo = "ForumInfo";
        public readonly string BlockFileSource = "FileSource";
        public readonly string BlockOptional = "Optional";
        public readonly string BlockFileSources = "FileSources";
        public readonly string BlockDatabases = "Databases";

        /// <summary>
        ///     Block caption "Warning" for "Warning".
        /// </summary>
        public readonly string BlockWarning = "Warning";
        public readonly string BlockLoginSettings = "LoginSettings";
        public readonly string BlockPasswordSettings = "PasswordSettings";
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string DisableDesktopLinksInNotifications = nameof(DisableDesktopLinksInNotifications);
        public readonly string ForumMaxAttachedFileSizeKb = nameof(ForumMaxAttachedFileSizeKb);
        public readonly string ForumMaxAttachedFiles = nameof(ForumMaxAttachedFiles);
        public readonly string ForumMaxMessageInlines = nameof(ForumMaxMessageInlines);
        public readonly string ForumMaxMessageSize = nameof(ForumMaxMessageSize);
        public readonly string IsDatabase = nameof(IsDatabase);
        public readonly string FileSources = nameof(FileSources);

        /// <summary>
        ///     Control caption "LicenseHintForActionHistory" for "LicenseHintForActionHistory".
        /// </summary>
        public readonly string LicenseHintForActionHistory = nameof(LicenseHintForActionHistory);
        public readonly string Databases = nameof(Databases);

        #endregion

        #region ToString

        public static implicit operator string(ServerInstanceTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ShowDialog

    /// <summary>
    ///     ID: {706e6173-f55d-4fe8-9cbe-aaeb2964ba80}
    ///     Alias: ShowDialog
    ///     Caption: $CardTypes_TypesNames_ShowDialog
    ///     Group: System
    /// </summary>
    public class ShowDialogTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "ShowDialog": {706e6173-f55d-4fe8-9cbe-aaeb2964ba80}.
        /// </summary>
        public readonly Guid ID = new Guid(0x706e6173,0xf55d,0x4fe8,0x9c,0xbe,0xaa,0xeb,0x29,0x64,0xba,0x80);

        /// <summary>
        ///     Card type name for "ShowDialog".
        /// </summary>
        public readonly string Alias = "ShowDialog";

        /// <summary>
        ///     Card type caption for "ShowDialog".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ShowDialog";

        /// <summary>
        ///     Card type group for "ShowDialog".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region ToString

        public static implicit operator string(ShowDialogTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region SignatureSettings

    /// <summary>
    ///     ID: {07257ec6-43cb-4425-9fcb-893a6c46f20d}
    ///     Alias: SignatureSettings
    ///     Caption: $CardTypes_TypesNames_SignatureSetting
    ///     Group: Settings
    /// </summary>
    public class SignatureSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "SignatureSettings": {07257ec6-43cb-4425-9fcb-893a6c46f20d}.
        /// </summary>
        public readonly Guid ID = new Guid(0x07257ec6,0x43cb,0x4425,0x9f,0xcb,0x89,0x3a,0x6c,0x46,0xf2,0x0d);

        /// <summary>
        ///     Card type name for "SignatureSettings".
        /// </summary>
        public readonly string Alias = "SignatureSettings";

        /// <summary>
        ///     Card type caption for "SignatureSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_SignatureSetting";

        /// <summary>
        ///     Card type group for "SignatureSettings".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormSignatureSettings = "SignatureSettings";

        #endregion

        #region Blocks

        public readonly string BlockMainInformation = "MainInformation";

        /// <summary>
        ///     Block caption "Filters and settings" for "FiltersAndSettings".
        /// </summary>
        public readonly string BlockFiltersAndSettings = "FiltersAndSettings";
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string TSPDigestAlgorithm = nameof(TSPDigestAlgorithm);
        public readonly string FilterStartDate = nameof(FilterStartDate);
        public readonly string FilterEndDate = nameof(FilterEndDate);
        public readonly string FilterIsValidDate = nameof(FilterIsValidDate);
        public readonly string FilterCompany = nameof(FilterCompany);
        public readonly string FilterSubject = nameof(FilterSubject);
        public readonly string FilterIssuer = nameof(FilterIssuer);
        public readonly string CeritificateFilters = nameof(CeritificateFilters);
        public readonly string EncryptionAlgorithm = nameof(EncryptionAlgorithm);
        public readonly string DigestAlgorithm = nameof(DigestAlgorithm);
        public readonly string EdsManager = nameof(EdsManager);
        public readonly string EncryptionDigest = nameof(EncryptionDigest);
        public readonly string UseSystemRootCertificates = nameof(UseSystemRootCertificates);
        public readonly string Files = nameof(Files);

        #endregion

        #region ToString

        public static implicit operator string(SignatureSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region SmartRole

    /// <summary>
    ///     ID: {ff6a7318-11d6-4b9d-8018-80498c50566c}
    ///     Alias: SmartRole
    ///     Caption: $CardTypes_TypesNames_SmartRole
    ///     Group: Roles
    /// </summary>
    public class SmartRoleTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "SmartRole": {ff6a7318-11d6-4b9d-8018-80498c50566c}.
        /// </summary>
        public readonly Guid ID = new Guid(0xff6a7318,0x11d6,0x4b9d,0x80,0x18,0x80,0x49,0x8c,0x50,0x56,0x6c);

        /// <summary>
        ///     Card type name for "SmartRole".
        /// </summary>
        public readonly string Alias = "SmartRole";

        /// <summary>
        ///     Card type caption for "SmartRole".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_SmartRole";

        /// <summary>
        ///     Card type group for "SmartRole".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region Forms

        public readonly string FormSmartRole = "SmartRole";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockUsers = "Users";

        #endregion

        #region Controls

        public readonly string TimeZone = nameof(TimeZone);
        public readonly string RoleUsersLimitDisclaimer = nameof(RoleUsersLimitDisclaimer);

        #endregion

        #region ToString

        public static implicit operator string(SmartRoleTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region SmartRoleGenerator

    /// <summary>
    ///     ID: {c72e05fb-7eef-4256-9029-72f821f4f79e}
    ///     Alias: SmartRoleGenerator
    ///     Caption: $CardTypes_TypesNames_SmartRoleGenerator
    ///     Group: Roles
    /// </summary>
    public class SmartRoleGeneratorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "SmartRoleGenerator": {c72e05fb-7eef-4256-9029-72f821f4f79e}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc72e05fb,0x7eef,0x4256,0x90,0x29,0x72,0xf8,0x21,0xf4,0xf7,0x9e);

        /// <summary>
        ///     Card type name for "SmartRoleGenerator".
        /// </summary>
        public readonly string Alias = "SmartRoleGenerator";

        /// <summary>
        ///     Card type caption for "SmartRoleGenerator".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_SmartRoleGenerator";

        /// <summary>
        ///     Card type group for "SmartRoleGenerator".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region Forms

        public readonly string FormSmartRoleGenerator = "SmartRoleGenerator";
        public readonly string FormTab = "Tab";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockSmartRole = "SmartRole";
        public readonly string BlockTriggerMain = "TriggerMain";
        public readonly string BlockTriggers = "Triggers";
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        public readonly string OwnersSelectorSql = nameof(OwnersSelectorSql);
        public readonly string OwnerNameSelectorSql = nameof(OwnerNameSelectorSql);
        public readonly string OnlySelfUpdate = nameof(OnlySelfUpdate);
        public readonly string UpdateAclCardSelectorSql = nameof(UpdateAclCardSelectorSql);
        public readonly string Triggers = nameof(Triggers);
        public readonly string Validate = nameof(Validate);
        public readonly string ValidateAll = nameof(ValidateAll);
        public readonly string Errors = nameof(Errors);

        #endregion

        #region ToString

        public static implicit operator string(SmartRoleGeneratorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region StaticRole

    /// <summary>
    ///     ID: {825dbacc-ddec-00d1-a550-2a837792542e}
    ///     Alias: StaticRole
    ///     Caption: $CardTypes_TypesNames_StaticRole
    ///     Group: Roles
    /// </summary>
    public class StaticRoleTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "StaticRole": {825dbacc-ddec-00d1-a550-2a837792542e}.
        /// </summary>
        public readonly Guid ID = new Guid(0x825dbacc,0xddec,0x00d1,0xa5,0x50,0x2a,0x83,0x77,0x92,0x54,0x2e);

        /// <summary>
        ///     Card type name for "StaticRole".
        /// </summary>
        public readonly string Alias = "StaticRole";

        /// <summary>
        ///     Card type caption for "StaticRole".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_StaticRole";

        /// <summary>
        ///     Card type group for "StaticRole".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region Forms

        public readonly string FormStaticRole = "StaticRole";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockAdInfo = "AdInfo";
        public readonly string BlockUsers = "Users";

        #endregion

        #region Controls

        public readonly string TimeZone = nameof(TimeZone);
        public readonly string InheritTimeZone = nameof(InheritTimeZone);
        public readonly string AdSyncDate = nameof(AdSyncDate);
        public readonly string AdSyncWhenChanged = nameof(AdSyncWhenChanged);
        public readonly string AdSyncDistinguishedName = nameof(AdSyncDistinguishedName);
        public readonly string AdSyncID = nameof(AdSyncID);
        public readonly string AdSyncDisableUpdate = nameof(AdSyncDisableUpdate);
        public readonly string AdSyncIndependent = nameof(AdSyncIndependent);
        public readonly string AdSyncManualSync = nameof(AdSyncManualSync);

        #endregion

        #region ToString

        public static implicit operator string(StaticRoleTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Tag

    /// <summary>
    ///     ID: {0e459a72-4d99-43bf-8ea5-8cf7cc911382}
    ///     Alias: Tag
    ///     Caption: $CardTypes_TypesNames_Tag
    ///     Group: Settings
    /// </summary>
    public class TagTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Tag": {0e459a72-4d99-43bf-8ea5-8cf7cc911382}.
        /// </summary>
        public readonly Guid ID = new Guid(0x0e459a72,0x4d99,0x43bf,0x8e,0xa5,0x8c,0xf7,0xcc,0x91,0x13,0x82);

        /// <summary>
        ///     Card type name for "Tag".
        /// </summary>
        public readonly string Alias = "Tag";

        /// <summary>
        ///     Card type caption for "Tag".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Tag";

        /// <summary>
        ///     Card type group for "Tag".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormTag = "Tag";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(TagTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TagCondition

    /// <summary>
    ///     ID: {237be0a6-2cf5-4ea6-9074-f31cce11eb58}
    ///     Alias: TagCondition
    ///     Caption: $CardTypes_TypesNames_TagCondition
    ///     Group: Conditions
    /// </summary>
    public class TagConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TagCondition": {237be0a6-2cf5-4ea6-9074-f31cce11eb58}.
        /// </summary>
        public readonly Guid ID = new Guid(0x237be0a6,0x2cf5,0x4ea6,0x90,0x74,0xf3,0x1c,0xce,0x11,0xeb,0x58);

        /// <summary>
        ///     Card type name for "TagCondition".
        /// </summary>
        public readonly string Alias = "TagCondition";

        /// <summary>
        ///     Card type caption for "TagCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TagCondition";

        /// <summary>
        ///     Card type group for "TagCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormTagCondition = "TagCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(TagConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TagsUserSettings

    /// <summary>
    ///     ID: {7101497d-9057-43d9-99c6-1d425eadf3bd}
    ///     Alias: TagsUserSettings
    ///     Caption: $CardTypes_CardNames_TagsSettings
    ///     Group: UserSettings
    /// </summary>
    public class TagsUserSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TagsUserSettings": {7101497d-9057-43d9-99c6-1d425eadf3bd}.
        /// </summary>
        public readonly Guid ID = new Guid(0x7101497d,0x9057,0x43d9,0x99,0xc6,0x1d,0x42,0x5e,0xad,0xf3,0xbd);

        /// <summary>
        ///     Card type name for "TagsUserSettings".
        /// </summary>
        public readonly string Alias = "TagsUserSettings";

        /// <summary>
        ///     Card type caption for "TagsUserSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_CardNames_TagsSettings";

        /// <summary>
        ///     Card type group for "TagsUserSettings".
        /// </summary>
        public readonly string Group = "UserSettings";

        #endregion

        #region Forms

        public readonly string FormMySettings = "MySettings";

        #endregion

        #region Blocks

        public readonly string BlockTagsSettings = "TagsSettings";

        #endregion

        #region ToString

        public static implicit operator string(TagsUserSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskAssignedRoleEditor

    /// <summary>
    ///     ID: {5822a6de-95e6-4519-9045-173be057eb90}
    ///     Alias: TaskAssignedRoleEditor
    ///     Caption: $CardTypes_NewTaskAssignedRoleDialog
    ///     Group: System
    /// </summary>
    public class TaskAssignedRoleEditorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TaskAssignedRoleEditor": {5822a6de-95e6-4519-9045-173be057eb90}.
        /// </summary>
        public readonly Guid ID = new Guid(0x5822a6de,0x95e6,0x4519,0x90,0x45,0x17,0x3b,0xe0,0x57,0xeb,0x90);

        /// <summary>
        ///     Card type name for "TaskAssignedRoleEditor".
        /// </summary>
        public readonly string Alias = "TaskAssignedRoleEditor";

        /// <summary>
        ///     Card type caption for "TaskAssignedRoleEditor".
        /// </summary>
        public readonly string Caption = "$CardTypes_NewTaskAssignedRoleDialog";

        /// <summary>
        ///     Card type group for "TaskAssignedRoleEditor".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormTaskAssignedRoleEditor = "TaskAssignedRoleEditor";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Новая роль связанная с заданием" for "Editor".
        /// </summary>
        public readonly string BlockEditor = "Editor";

        #endregion

        #region ToString

        public static implicit operator string(TaskAssignedRoleEditorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskAssignedRoles

    /// <summary>
    ///     ID: {18f5d3ef-087a-4938-be67-4deb0b6e08b2}
    ///     Alias: TaskAssignedRoles
    ///     Caption: $CardTypes_TaskAssignedRolesDialog
    ///     Group: System
    /// </summary>
    public class TaskAssignedRolesTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TaskAssignedRoles": {18f5d3ef-087a-4938-be67-4deb0b6e08b2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x18f5d3ef,0x087a,0x4938,0xbe,0x67,0x4d,0xeb,0x0b,0x6e,0x08,0xb2);

        /// <summary>
        ///     Card type name for "TaskAssignedRoles".
        /// </summary>
        public readonly string Alias = "TaskAssignedRoles";

        /// <summary>
        ///     Card type caption for "TaskAssignedRoles".
        /// </summary>
        public readonly string Caption = "$CardTypes_TaskAssignedRolesDialog";

        /// <summary>
        ///     Card type group for "TaskAssignedRoles".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormTaskAssignedRoles = "TaskAssignedRoles";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Представления" for "Views".
        /// </summary>
        public readonly string BlockViews = "Views";

        /// <summary>
        ///     Block caption "Block2" for "Block2".
        /// </summary>
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "TaskAssignedRoles" for "TaskAssignedRoles".
        /// </summary>
        public readonly string TaskAssignedRoles = nameof(TaskAssignedRoles);

        /// <summary>
        ///     Control caption "Users" for "Users".
        /// </summary>
        public readonly string Users = nameof(Users);

        #endregion

        #region ToString

        public static implicit operator string(TaskAssignedRolesTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskChangedCondition

    /// <summary>
    ///     ID: {5779afe8-1fd4-4585-8c63-bf6043996250}
    ///     Alias: TaskChangedCondition
    ///     Caption: $CardTypes_TypesNames_TaskChangedCondition
    ///     Group: Conditions
    /// </summary>
    public class TaskChangedConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TaskChangedCondition": {5779afe8-1fd4-4585-8c63-bf6043996250}.
        /// </summary>
        public readonly Guid ID = new Guid(0x5779afe8,0x1fd4,0x4585,0x8c,0x63,0xbf,0x60,0x43,0x99,0x62,0x50);

        /// <summary>
        ///     Card type name for "TaskChangedCondition".
        /// </summary>
        public readonly string Alias = "TaskChangedCondition";

        /// <summary>
        ///     Card type caption for "TaskChangedCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TaskChangedCondition";

        /// <summary>
        ///     Card type group for "TaskChangedCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormTaskChangedCondition = "TaskChangedCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(TaskChangedConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskHistoryGroupType

    /// <summary>
    ///     ID: {5f25db65-1291-40c4-a1de-7673c2be448f}
    ///     Alias: TaskHistoryGroupType
    ///     Caption: $CardTypes_TypesNames_TaskHistoryGroupType
    ///     Group: Dictionaries
    /// </summary>
    public class TaskHistoryGroupTypeTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TaskHistoryGroupType": {5f25db65-1291-40c4-a1de-7673c2be448f}.
        /// </summary>
        public readonly Guid ID = new Guid(0x5f25db65,0x1291,0x40c4,0xa1,0xde,0x76,0x73,0xc2,0xbe,0x44,0x8f);

        /// <summary>
        ///     Card type name for "TaskHistoryGroupType".
        /// </summary>
        public readonly string Alias = "TaskHistoryGroupType";

        /// <summary>
        ///     Card type caption for "TaskHistoryGroupType".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TaskHistoryGroupType";

        /// <summary>
        ///     Card type group for "TaskHistoryGroupType".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormTaskHistoryGroupType = "TaskHistoryGroupType";

        #endregion

        #region Blocks

        public readonly string BlockMainInfoBlock = "MainInfoBlock";

        #endregion

        #region ToString

        public static implicit operator string(TaskHistoryGroupTypeTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskKind

    /// <summary>
    ///     ID: {2f41698a-3823-48c9-9476-f756f090eb11}
    ///     Alias: TaskKind
    ///     Caption: $CardTypes_TypesNames_TaskKind
    ///     Group: Dictionaries
    /// </summary>
    public class TaskKindTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TaskKind": {2f41698a-3823-48c9-9476-f756f090eb11}.
        /// </summary>
        public readonly Guid ID = new Guid(0x2f41698a,0x3823,0x48c9,0x94,0x76,0xf7,0x56,0xf0,0x90,0xeb,0x11);

        /// <summary>
        ///     Card type name for "TaskKind".
        /// </summary>
        public readonly string Alias = "TaskKind";

        /// <summary>
        ///     Card type caption for "TaskKind".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TaskKind";

        /// <summary>
        ///     Card type group for "TaskKind".
        /// </summary>
        public readonly string Group = "Dictionaries";

        #endregion

        #region Forms

        public readonly string FormTaskKind = "TaskKind";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";

        #endregion

        #region ToString

        public static implicit operator string(TaskKindTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskRole

    /// <summary>
    ///     ID: {e97c253c-9102-0440-ac7e-4876e8f789da}
    ///     Alias: TaskRole
    ///     Caption: $CardTypes_TypesNames_TaskRole
    ///     Group: Roles
    /// </summary>
    public class TaskRoleTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TaskRole": {e97c253c-9102-0440-ac7e-4876e8f789da}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe97c253c,0x9102,0x0440,0xac,0x7e,0x48,0x76,0xe8,0xf7,0x89,0xda);

        /// <summary>
        ///     Card type name for "TaskRole".
        /// </summary>
        public readonly string Alias = "TaskRole";

        /// <summary>
        ///     Card type caption for "TaskRole".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TaskRole";

        /// <summary>
        ///     Card type group for "TaskRole".
        /// </summary>
        public readonly string Group = "Roles";

        #endregion

        #region Forms

        public readonly string FormTaskRole = "TaskRole";

        #endregion

        #region Blocks

        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockUsers = "Users";

        #endregion

        #region Controls

        public readonly string TimeZone = nameof(TimeZone);

        #endregion

        #region ToString

        public static implicit operator string(TaskRoleTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskTypeCondition

    /// <summary>
    ///     ID: {c463100c-8a7b-4c31-a23f-743d8b3eb29f}
    ///     Alias: TaskTypeCondition
    ///     Caption: $CardTypes_TypesNames_TaskTypeCondition
    ///     Group: Conditions
    /// </summary>
    public class TaskTypeConditionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TaskTypeCondition": {c463100c-8a7b-4c31-a23f-743d8b3eb29f}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc463100c,0x8a7b,0x4c31,0xa2,0x3f,0x74,0x3d,0x8b,0x3e,0xb2,0x9f);

        /// <summary>
        ///     Card type name for "TaskTypeCondition".
        /// </summary>
        public readonly string Alias = "TaskTypeCondition";

        /// <summary>
        ///     Card type caption for "TaskTypeCondition".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TaskTypeCondition";

        /// <summary>
        ///     Card type group for "TaskTypeCondition".
        /// </summary>
        public readonly string Group = "Conditions";

        #endregion

        #region Forms

        public readonly string FormTaskTypeCondition = "TaskTypeCondition";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(TaskTypeConditionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Template

    /// <summary>
    ///     ID: {7ed2fb6d-4ece-458f-9151-0c72995c2d19}
    ///     Alias: Template
    ///     Caption: $CardTypes_TypesNames_Blocks_Tabs_Template
    ///     Group: System
    /// </summary>
    public class TemplateTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Template": {7ed2fb6d-4ece-458f-9151-0c72995c2d19}.
        /// </summary>
        public readonly Guid ID = new Guid(0x7ed2fb6d,0x4ece,0x458f,0x91,0x51,0x0c,0x72,0x99,0x5c,0x2d,0x19);

        /// <summary>
        ///     Card type name for "Template".
        /// </summary>
        public readonly string Alias = "Template";

        /// <summary>
        ///     Card type caption for "Template".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Blocks_Tabs_Template";

        /// <summary>
        ///     Card type group for "Template".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormTemplate = "Template";

        #endregion

        #region Blocks

        public readonly string BlockTemplateInfo = "TemplateInfo";
        public readonly string BlockCardInfo = "CardInfo";
        public readonly string BlockFiles = "Files";

        #endregion

        #region ToString

        public static implicit operator string(TemplateTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TemplateFile

    /// <summary>
    ///     ID: {a259101b-58f7-47b4-959e-dd5e7be1671c}
    ///     Alias: TemplateFile
    ///     Caption: $CardTypes_TypesNames_TemplateFile
    ///     Group: System
    /// </summary>
    public class TemplateFileTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TemplateFile": {a259101b-58f7-47b4-959e-dd5e7be1671c}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa259101b,0x58f7,0x47b4,0x95,0x9e,0xdd,0x5e,0x7b,0xe1,0x67,0x1c);

        /// <summary>
        ///     Card type name for "TemplateFile".
        /// </summary>
        public readonly string Alias = "TemplateFile";

        /// <summary>
        ///     Card type caption for "TemplateFile".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TemplateFile";

        /// <summary>
        ///     Card type group for "TemplateFile".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region ToString

        public static implicit operator string(TemplateFileTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TestTask1

    /// <summary>
    ///     ID: {929e345c-acdf-41ea-acb6-6bb308de73ae}
    ///     Alias: TestTask1
    ///     Caption: $CardTypes_TypesNames_TestTask1
    ///     Group: TestProcess
    /// </summary>
    public class TestTask1TypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TestTask1": {929e345c-acdf-41ea-acb6-6bb308de73ae}.
        /// </summary>
        public readonly Guid ID = new Guid(0x929e345c,0xacdf,0x41ea,0xac,0xb6,0x6b,0xb3,0x08,0xde,0x73,0xae);

        /// <summary>
        ///     Card type name for "TestTask1".
        /// </summary>
        public readonly string Alias = "TestTask1";

        /// <summary>
        ///     Card type caption for "TestTask1".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TestTask1";

        /// <summary>
        ///     Card type group for "TestTask1".
        /// </summary>
        public readonly string Group = "TestProcess";

        #endregion

        #region ToString

        public static implicit operator string(TestTask1TypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TestTask2

    /// <summary>
    ///     ID: {5239e1b6-1ed6-4a3f-a11e-7e4c6e187af6}
    ///     Alias: TestTask2
    ///     Caption: $CardTypes_TypesNames_TestTask2
    ///     Group: TestProcess
    /// </summary>
    public class TestTask2TypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TestTask2": {5239e1b6-1ed6-4a3f-a11e-7e4c6e187af6}.
        /// </summary>
        public readonly Guid ID = new Guid(0x5239e1b6,0x1ed6,0x4a3f,0xa1,0x1e,0x7e,0x4c,0x6e,0x18,0x7a,0xf6);

        /// <summary>
        ///     Card type name for "TestTask2".
        /// </summary>
        public readonly string Alias = "TestTask2";

        /// <summary>
        ///     Card type caption for "TestTask2".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TestTask2";

        /// <summary>
        ///     Card type group for "TestTask2".
        /// </summary>
        public readonly string Group = "TestProcess";

        #endregion

        #region ToString

        public static implicit operator string(TestTask2TypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TileUserSettings

    /// <summary>
    ///     ID: {d3e5a259-e7e6-49f2-a7dd-1fc7b051781b}
    ///     Alias: TileUserSettings
    ///     Caption: $CardTypes_Blocks_TilePanels
    ///     Group: UserSettings
    /// </summary>
    public class TileUserSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TileUserSettings": {d3e5a259-e7e6-49f2-a7dd-1fc7b051781b}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd3e5a259,0xe7e6,0x49f2,0xa7,0xdd,0x1f,0xc7,0xb0,0x51,0x78,0x1b);

        /// <summary>
        ///     Card type name for "TileUserSettings".
        /// </summary>
        public readonly string Alias = "TileUserSettings";

        /// <summary>
        ///     Card type caption for "TileUserSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_Blocks_TilePanels";

        /// <summary>
        ///     Card type group for "TileUserSettings".
        /// </summary>
        public readonly string Group = "UserSettings";

        #endregion

        #region Forms

        public readonly string FormTilePanels = "TilePanels";

        #endregion

        #region Blocks

        public readonly string BlockTilePanels = "TilePanels";

        #endregion

        #region ToString

        public static implicit operator string(TileUserSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TimeZones

    /// <summary>
    ///     ID: {cc60e8c6-af29-4ad5-a55d-3e6ee985ceb2}
    ///     Alias: TimeZones
    ///     Caption: $CardTypes_TypesNames_Tabs_TimeZones
    ///     Group: Settings
    /// </summary>
    public class TimeZonesTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TimeZones": {cc60e8c6-af29-4ad5-a55d-3e6ee985ceb2}.
        /// </summary>
        public readonly Guid ID = new Guid(0xcc60e8c6,0xaf29,0x4ad5,0xa5,0x5d,0x3e,0x6e,0xe9,0x85,0xce,0xb2);

        /// <summary>
        ///     Card type name for "TimeZones".
        /// </summary>
        public readonly string Alias = "TimeZones";

        /// <summary>
        ///     Card type caption for "TimeZones".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Tabs_TimeZones";

        /// <summary>
        ///     Card type group for "TimeZones".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormTimeZones = "TimeZones";

        #endregion

        #region Blocks

        public readonly string BlockSettings = "Settings";
        public readonly string BlockBlock1 = "Block1";

        /// <summary>
        ///     Block caption "TimeZonesBlock" for "TimeZonesBlock".
        /// </summary>
        public readonly string BlockTimeZonesBlock = "TimeZonesBlock";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "Warning" for "TimeZonesTooltip".
        /// </summary>
        public readonly string TimeZonesTooltip = nameof(TimeZonesTooltip);

        public readonly string TimeZonesControl = nameof(TimeZonesControl);

        #endregion

        #region ToString

        public static implicit operator string(TimeZonesTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TopicDialogs

    /// <summary>
    ///     ID: {e8c0c3a0-80cb-4f92-9fee-d38f1dddacd2}
    ///     Alias: TopicDialogs
    ///     Caption: TopicDialogs
    ///     Group: Forums
    /// </summary>
    public class TopicDialogsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TopicDialogs": {e8c0c3a0-80cb-4f92-9fee-d38f1dddacd2}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe8c0c3a0,0x80cb,0x4f92,0x9f,0xee,0xd3,0x8f,0x1d,0xdd,0xac,0xd2);

        /// <summary>
        ///     Card type name for "TopicDialogs".
        /// </summary>
        public readonly string Alias = "TopicDialogs";

        /// <summary>
        ///     Card type caption for "TopicDialogs".
        /// </summary>
        public readonly string Caption = "TopicDialogs";

        /// <summary>
        ///     Card type group for "TopicDialogs".
        /// </summary>
        public readonly string Group = "Forums";

        #endregion

        #region Forms

        public readonly string FormAddTopicTab = "AddTopicTab";
        public readonly string FormAddParticipantsTabName = "AddParticipantsTabName";
        public readonly string FormAddRoleParticipantsTabName = "AddRoleParticipantsTabName";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "AddTopicBlock".
        /// </summary>
        public readonly string BlockAddTopicBlock = "AddTopicBlock";

        /// <summary>
        ///     Block caption "Block2" for "Block2".
        /// </summary>
        public readonly string BlockBlock2 = "Block2";

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string AddParticipants = nameof(AddParticipants);
        public readonly string IsReadOnly = nameof(IsReadOnly);
        public readonly string IsModerator = nameof(IsModerator);
        public readonly string IsSubscribed = nameof(IsSubscribed);

        #endregion

        #region ToString

        public static implicit operator string(TopicDialogsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TopicTabs

    /// <summary>
    ///     ID: {aed972eb-883d-4b02-a75b-c96d4a5aef4c}
    ///     Alias: TopicTabs
    ///     Caption: TopicTabs
    ///     Group: Forums
    /// </summary>
    public class TopicTabsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "TopicTabs": {aed972eb-883d-4b02-a75b-c96d4a5aef4c}.
        /// </summary>
        public readonly Guid ID = new Guid(0xaed972eb,0x883d,0x4b02,0xa7,0x5b,0xc9,0x6d,0x4a,0x5a,0xef,0x4c);

        /// <summary>
        ///     Card type name for "TopicTabs".
        /// </summary>
        public readonly string Alias = "TopicTabs";

        /// <summary>
        ///     Card type caption for "TopicTabs".
        /// </summary>
        public readonly string Caption = "TopicTabs";

        /// <summary>
        ///     Card type group for "TopicTabs".
        /// </summary>
        public readonly string Group = "Forums";

        #endregion

        #region Forms

        public readonly string FormForum = "Forum";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Топики" for "Topics".
        /// </summary>
        public readonly string BlockTopics = "Topics";

        #endregion

        #region Controls

        public readonly string Topics = nameof(Topics);

        #endregion

        #region ToString

        public static implicit operator string(TopicTabsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region UserSettingsSystemType

    /// <summary>
    ///     ID: {7e6a9f59-0889-4b4d-8989-9f0c70c39f90}
    ///     Alias: UserSettingsSystemType
    ///     Caption: Used by the system. Do not modify it.
    ///     Group: System
    /// </summary>
    public class UserSettingsSystemTypeTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "UserSettingsSystemType": {7e6a9f59-0889-4b4d-8989-9f0c70c39f90}.
        /// </summary>
        public readonly Guid ID = new Guid(0x7e6a9f59,0x0889,0x4b4d,0x89,0x89,0x9f,0x0c,0x70,0xc3,0x9f,0x90);

        /// <summary>
        ///     Card type name for "UserSettingsSystemType".
        /// </summary>
        public readonly string Alias = "UserSettingsSystemType";

        /// <summary>
        ///     Card type caption for "UserSettingsSystemType".
        /// </summary>
        public readonly string Caption = "Used by the system. Do not modify it.";

        /// <summary>
        ///     Card type group for "UserSettingsSystemType".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region ToString

        public static implicit operator string(UserSettingsSystemTypeTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region View

    /// <summary>
    ///     ID: {635bbe7b-9c2e-4fde-87c2-9deefaad7981}
    ///     Alias: View
    ///     Caption: $CardTypes_TypesNames_View
    ///     Group: System
    /// </summary>
    public class ViewTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "View": {635bbe7b-9c2e-4fde-87c2-9deefaad7981}.
        /// </summary>
        public readonly Guid ID = new Guid(0x635bbe7b,0x9c2e,0x4fde,0x87,0xc2,0x9d,0xee,0xfa,0xad,0x79,0x81);

        /// <summary>
        ///     Card type name for "View".
        /// </summary>
        public readonly string Alias = "View";

        /// <summary>
        ///     Card type caption for "View".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_View";

        /// <summary>
        ///     Card type group for "View".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormView = "View";

        #endregion

        #region Blocks

        public readonly string BlockMainInformation = "MainInformation";
        public readonly string BlockRoles = "Roles";

        #endregion

        #region ToString

        public static implicit operator string(ViewTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ViewExtensions

    /// <summary>
    ///     ID: {5d12d3d7-c5df-4983-9578-f065d2b74768}
    ///     Alias: ViewExtensions
    ///     Caption: $Cards_DefaultCaption
    ///     Group: System
    /// </summary>
    public class ViewExtensionsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "ViewExtensions": {5d12d3d7-c5df-4983-9578-f065d2b74768}.
        /// </summary>
        public readonly Guid ID = new Guid(0x5d12d3d7,0xc5df,0x4983,0x95,0x78,0xf0,0x65,0xd2,0xb7,0x47,0x68);

        /// <summary>
        ///     Card type name for "ViewExtensions".
        /// </summary>
        public readonly string Alias = "ViewExtensions";

        /// <summary>
        ///     Card type caption for "ViewExtensions".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "ViewExtensions".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        /// <summary>
        ///     Form caption "CreateCardExtension" for "CreateCardExtension".
        /// </summary>
        public readonly string FormCreateCardExtension = "CreateCardExtension";

        /// <summary>
        ///     Form caption "AutomaticNodeRefreshExtension" for "AutomaticNodeRefreshExtension".
        /// </summary>
        public readonly string FormAutomaticNodeRefreshExtension = "AutomaticNodeRefreshExtension";

        /// <summary>
        ///     Form caption "RefSectionExtension" for "RefSectionExtension".
        /// </summary>
        public readonly string FormRefSectionExtension = "RefSectionExtension";

        /// <summary>
        ///     Form caption "ManagerWorkplaceExtension" for "ManagerWorkplaceExtension".
        /// </summary>
        public readonly string FormManagerWorkplaceExtension = "ManagerWorkplaceExtension";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "MainBlock" for "MainBlock".
        /// </summary>
        public readonly string BlockMainBlock = "MainBlock";

        #endregion

        #region Controls

        public readonly string IDParam = nameof(IDParam);
        public readonly string CardOpeningKind = nameof(CardOpeningKind);
        public readonly string CreateCardKind = nameof(CreateCardKind);
        public readonly string TypeAlias = nameof(TypeAlias);
        public readonly string DocTypeIdentifier = nameof(DocTypeIdentifier);
        public readonly string RefreshInterval = nameof(RefreshInterval);
        public readonly string WithContentDataRefreshing = nameof(WithContentDataRefreshing);
        public readonly string Name = nameof(Name);
        public readonly string RefSections = nameof(RefSections);
        public readonly string Parameters = nameof(Parameters);
        public readonly string CardId = nameof(CardId);
        public readonly string CountColumnName = nameof(CountColumnName);
        public readonly string TileColumnName = nameof(TileColumnName);
        public readonly string ActiveImageColumnName = nameof(ActiveImageColumnName);
        public readonly string HoverImageColumnName = nameof(HoverImageColumnName);
        public readonly string InactiveImageColumnName = nameof(InactiveImageColumnName);

        #endregion

        #region ToString

        public static implicit operator string(ViewExtensionsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region VirtualScheme

    /// <summary>
    ///     ID: {b2b4d2c2-8f92-4262-9951-fe1a64bf9b30}
    ///     Alias: VirtualScheme
    ///     Caption: $Cards_DefaultCaption
    ///     Group: System
    /// </summary>
    public class VirtualSchemeTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "VirtualScheme": {b2b4d2c2-8f92-4262-9951-fe1a64bf9b30}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb2b4d2c2,0x8f92,0x4262,0x99,0x51,0xfe,0x1a,0x64,0xbf,0x9b,0x30);

        /// <summary>
        ///     Card type name for "VirtualScheme".
        /// </summary>
        public readonly string Alias = "VirtualScheme";

        /// <summary>
        ///     Card type caption for "VirtualScheme".
        /// </summary>
        public readonly string Caption = "$Cards_DefaultCaption";

        /// <summary>
        ///     Card type group for "VirtualScheme".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        /// <summary>
        ///     Form caption "Editor" for "Editor".
        /// </summary>
        public readonly string FormEditor = "Editor";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "SchemeViewEditorBlock" for "SchemeViewEditorBlock".
        /// </summary>
        public readonly string BlockSchemeViewEditorBlock = "SchemeViewEditorBlock";

        /// <summary>
        ///     Block caption "PropertiesEditorBlock" for "PropertiesEditorBlock".
        /// </summary>
        public readonly string BlockPropertiesEditorBlock = "PropertiesEditorBlock";

        /// <summary>
        ///     Block caption "ReferenceColumnsEditorBlock" for "ReferenceColumnsEditorBlock".
        /// </summary>
        public readonly string BlockReferenceColumnsEditorBlock = "ReferenceColumnsEditorBlock";

        #endregion

        #region Controls

        public readonly string SchemeEditor = nameof(SchemeEditor);
        public readonly string NameEditor = nameof(NameEditor);
        public readonly string DescriptionEdtor = nameof(DescriptionEdtor);
        public readonly string TableContentTypeEditor = nameof(TableContentTypeEditor);
        public readonly string ColumnContentTypeEditor = nameof(ColumnContentTypeEditor);
        public readonly string ReferenceTableEditor = nameof(ReferenceTableEditor);
        public readonly string IsReferencedToOwnerEditor = nameof(IsReferencedToOwnerEditor);
        public readonly string DefaultStringValueEditor = nameof(DefaultStringValueEditor);
        public readonly string DefaultDateTimeValueEditor = nameof(DefaultDateTimeValueEditor);
        public readonly string DefaultDateValueEditor = nameof(DefaultDateValueEditor);
        public readonly string DefaultTimeValueEditor = nameof(DefaultTimeValueEditor);
        public readonly string DefaultDateTimeOffsetDateTimeValueEditor = nameof(DefaultDateTimeOffsetDateTimeValueEditor);
        public readonly string DefaultDateTimeOffsetOffSetValueEditor = nameof(DefaultDateTimeOffsetOffSetValueEditor);
        public readonly string ReferenceColumnsEditor = nameof(ReferenceColumnsEditor);

        #endregion

        #region ToString

        public static implicit operator string(VirtualSchemeTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WebApplication

    /// <summary>
    ///     ID: {104076c7-3cd3-4b4e-9344-e17f24c88ee8}
    ///     Alias: WebApplication
    ///     Caption: $CardTypes_TypesNames_WebApplication
    ///     Group: System
    /// </summary>
    public class WebApplicationTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WebApplication": {104076c7-3cd3-4b4e-9344-e17f24c88ee8}.
        /// </summary>
        public readonly Guid ID = new Guid(0x104076c7,0x3cd3,0x4b4e,0x93,0x44,0xe1,0x7f,0x24,0xc8,0x8e,0xe8);

        /// <summary>
        ///     Card type name for "WebApplication".
        /// </summary>
        public readonly string Alias = "WebApplication";

        /// <summary>
        ///     Card type caption for "WebApplication".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_WebApplication";

        /// <summary>
        ///     Card type group for "WebApplication".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormWebApplication = "WebApplication";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockFiles = "Files";

        #endregion

        #region Controls

        public readonly string Files = nameof(Files);

        #endregion

        #region ToString

        public static implicit operator string(WebApplicationTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WebClientUserSettings

    /// <summary>
    ///     ID: {7104ac73-bf0a-4dba-be9e-21f7148c8c82}
    ///     Alias: WebClientUserSettings
    ///     Caption: $CardTypes_Blocks_WebClientUserSettings
    ///     Group: UserSettings
    /// </summary>
    public class WebClientUserSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WebClientUserSettings": {7104ac73-bf0a-4dba-be9e-21f7148c8c82}.
        /// </summary>
        public readonly Guid ID = new Guid(0x7104ac73,0xbf0a,0x4dba,0xbe,0x9e,0x21,0xf7,0x14,0x8c,0x8c,0x82);

        /// <summary>
        ///     Card type name for "WebClientUserSettings".
        /// </summary>
        public readonly string Alias = "WebClientUserSettings";

        /// <summary>
        ///     Card type caption for "WebClientUserSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_Blocks_WebClientUserSettings";

        /// <summary>
        ///     Card type group for "WebClientUserSettings".
        /// </summary>
        public readonly string Group = "UserSettings";

        #endregion

        #region Forms

        public readonly string FormWebPanels = "WebPanels";

        #endregion

        #region Blocks

        public readonly string BlockTilePanels = "TilePanels";

        #endregion

        #region ToString

        public static implicit operator string(WebClientUserSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WfResolution

    /// <summary>
    ///     ID: {928132fe-202d-4f9f-8ec5-5093ea2122d1}
    ///     Alias: WfResolution
    ///     Caption: $CardTypes_TypesNames_WfResolution
    ///     Group: Wf
    /// </summary>
    public class WfResolutionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WfResolution": {928132fe-202d-4f9f-8ec5-5093ea2122d1}.
        /// </summary>
        public readonly Guid ID = new Guid(0x928132fe,0x202d,0x4f9f,0x8e,0xc5,0x50,0x93,0xea,0x21,0x22,0xd1);

        /// <summary>
        ///     Card type name for "WfResolution".
        /// </summary>
        public readonly string Alias = "WfResolution";

        /// <summary>
        ///     Card type caption for "WfResolution".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_WfResolution";

        /// <summary>
        ///     Card type group for "WfResolution".
        /// </summary>
        public readonly string Group = "Wf";

        #endregion

        #region Forms

        public readonly string FormWfResolution = "WfResolution";
        public readonly string FormComplete = "Complete";
        public readonly string FormSendToPerformer = "SendToPerformer";
        public readonly string FormCreateChildResolution = "CreateChildResolution";
        public readonly string FormRevokeOrCancel = "RevokeOrCancel";
        public readonly string FormModifyAsAuthor = "ModifyAsAuthor";

        #endregion

        #region Blocks

        public readonly string BlockTaskInfo = "TaskInfo";
        public readonly string BlockMainInfo = "MainInfo";
        public readonly string BlockChildResolutions = "ChildResolutions";
        public readonly string BlockPerformers = "Performers";
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string TaskInfo = nameof(TaskInfo);
        public readonly string RevokeChildren_ChildResolutions = nameof(RevokeChildren_ChildResolutions);
        public readonly string MassCreation_MultiplePerformers = nameof(MassCreation_MultiplePerformers);
        public readonly string MajorPerformer_MassCreation = nameof(MajorPerformer_MassCreation);
        public readonly string WithControl = nameof(WithControl);
        public readonly string Kind_Additional = nameof(Kind_Additional);
        public readonly string From_Additional = nameof(From_Additional);
        public readonly string Controller_WithControl = nameof(Controller_WithControl);

        #endregion

        #region ToString

        public static implicit operator string(WfResolutionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WfResolutionChild

    /// <summary>
    ///     ID: {539ecfe8-5fb6-4681-8aa8-1ee4d9ef1dda}
    ///     Alias: WfResolutionChild
    ///     Caption: $CardTypes_TypesNames_WfResolutionChild
    ///     Group: Wf
    /// </summary>
    public class WfResolutionChildTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WfResolutionChild": {539ecfe8-5fb6-4681-8aa8-1ee4d9ef1dda}.
        /// </summary>
        public readonly Guid ID = new Guid(0x539ecfe8,0x5fb6,0x4681,0x8a,0xa8,0x1e,0xe4,0xd9,0xef,0x1d,0xda);

        /// <summary>
        ///     Card type name for "WfResolutionChild".
        /// </summary>
        public readonly string Alias = "WfResolutionChild";

        /// <summary>
        ///     Card type caption for "WfResolutionChild".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_WfResolutionChild";

        /// <summary>
        ///     Card type group for "WfResolutionChild".
        /// </summary>
        public readonly string Group = "Wf";

        #endregion

        #region ToString

        public static implicit operator string(WfResolutionChildTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WfResolutionControl

    /// <summary>
    ///     ID: {85a5e8d7-a901-46df-9173-4d9a043ce6d3}
    ///     Alias: WfResolutionControl
    ///     Caption: $CardTypes_TypesNames_WfResolutionControl
    ///     Group: Wf
    /// </summary>
    public class WfResolutionControlTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WfResolutionControl": {85a5e8d7-a901-46df-9173-4d9a043ce6d3}.
        /// </summary>
        public readonly Guid ID = new Guid(0x85a5e8d7,0xa901,0x46df,0x91,0x73,0x4d,0x9a,0x04,0x3c,0xe6,0xd3);

        /// <summary>
        ///     Card type name for "WfResolutionControl".
        /// </summary>
        public readonly string Alias = "WfResolutionControl";

        /// <summary>
        ///     Card type caption for "WfResolutionControl".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_WfResolutionControl";

        /// <summary>
        ///     Card type group for "WfResolutionControl".
        /// </summary>
        public readonly string Group = "Wf";

        #endregion

        #region ToString

        public static implicit operator string(WfResolutionControlTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WfResolutionProject

    /// <summary>
    ///     ID: {c989d91f-7ddd-455c-ae16-3bb380132ba8}
    ///     Alias: WfResolutionProject
    ///     Caption: $CardTypes_TypesNames_WfResolutionProject
    ///     Group: Wf
    /// </summary>
    public class WfResolutionProjectTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WfResolutionProject": {c989d91f-7ddd-455c-ae16-3bb380132ba8}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc989d91f,0x7ddd,0x455c,0xae,0x16,0x3b,0xb3,0x80,0x13,0x2b,0xa8);

        /// <summary>
        ///     Card type name for "WfResolutionProject".
        /// </summary>
        public readonly string Alias = "WfResolutionProject";

        /// <summary>
        ///     Card type caption for "WfResolutionProject".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_WfResolutionProject";

        /// <summary>
        ///     Card type group for "WfResolutionProject".
        /// </summary>
        public readonly string Group = "Wf";

        #endregion

        #region ToString

        public static implicit operator string(WfResolutionProjectTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WfSatellite

    /// <summary>
    ///     ID: {a382ec40-6321-42e5-a9f9-c7b103feb38d}
    ///     Alias: WfSatellite
    ///     Caption: $CardTypes_TypesNames_WfSatellite
    ///     Group: Wf
    /// </summary>
    public class WfSatelliteTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WfSatellite": {a382ec40-6321-42e5-a9f9-c7b103feb38d}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa382ec40,0x6321,0x42e5,0xa9,0xf9,0xc7,0xb1,0x03,0xfe,0xb3,0x8d);

        /// <summary>
        ///     Card type name for "WfSatellite".
        /// </summary>
        public readonly string Alias = "WfSatellite";

        /// <summary>
        ///     Card type caption for "WfSatellite".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_WfSatellite";

        /// <summary>
        ///     Card type group for "WfSatellite".
        /// </summary>
        public readonly string Group = "Wf";

        #endregion

        #region ToString

        public static implicit operator string(WfSatelliteTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WfTaskCard

    /// <summary>
    ///     ID: {de75a343-8164-472d-a20e-4937819760ac}
    ///     Alias: WfTaskCard
    ///     Caption: $CardTypes_TypesNames_WfTaskCard
    ///     Group: Wf
    /// </summary>
    public class WfTaskCardTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WfTaskCard": {de75a343-8164-472d-a20e-4937819760ac}.
        /// </summary>
        public readonly Guid ID = new Guid(0xde75a343,0x8164,0x472d,0xa2,0x0e,0x49,0x37,0x81,0x97,0x60,0xac);

        /// <summary>
        ///     Card type name for "WfTaskCard".
        /// </summary>
        public readonly string Alias = "WfTaskCard";

        /// <summary>
        ///     Card type caption for "WfTaskCard".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_WfTaskCard";

        /// <summary>
        ///     Card type group for "WfTaskCard".
        /// </summary>
        public readonly string Group = "Wf";

        #endregion

        #region Forms

        public readonly string FormWfTaskCard = "WfTaskCard";

        #endregion

        #region Blocks

        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock1 = "Block1";

        /// <summary>
        ///     Block caption "KrBlockForDocStatus" for "KrBlockForDocStatus".
        /// </summary>
        public readonly string BlockKrBlockForDocStatus = "KrBlockForDocStatus";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockBlock4 = "Block4";
        public readonly string BlockBlock5 = "Block5";

        #endregion

        #region Controls

        public readonly string NavigateMainCard = nameof(NavigateMainCard);
        public readonly string DocStateControl = nameof(DocStateControl);
        public readonly string DocStateChangedControl = nameof(DocStateChangedControl);
        public readonly string TaskFiles = nameof(TaskFiles);

        #endregion

        #region ToString

        public static implicit operator string(WfTaskCardTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowActionEditor

    /// <summary>
    ///     ID: {d0960012-d05a-4c86-9e45-ab467231d11d}
    ///     Alias: WorkflowActionEditor
    ///     Caption: $CardTypes_TypesNames_ActionTemplate
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowActionEditorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowActionEditor": {d0960012-d05a-4c86-9e45-ab467231d11d}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd0960012,0xd05a,0x4c86,0x9e,0x45,0xab,0x46,0x72,0x31,0xd1,0x1d);

        /// <summary>
        ///     Card type name for "WorkflowActionEditor".
        /// </summary>
        public readonly string Alias = "WorkflowActionEditor";

        /// <summary>
        ///     Card type caption for "WorkflowActionEditor".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ActionTemplate";

        /// <summary>
        ///     Card type group for "WorkflowActionEditor".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Forms

        public readonly string FormWorkflowActionEditor = "WorkflowActionEditor";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockAdditionalBlock = "AdditionalBlock";
        public readonly string BlockPreScript = "PreScript";
        public readonly string BlockPostScript = "PostScript";

        #endregion

        #region Controls

        public readonly string EditParametersButton = nameof(EditParametersButton);
        public readonly string CompileButton = nameof(CompileButton);
        public readonly string OpenFileButton = nameof(OpenFileButton);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowActionEditorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowActionInstanceEditor

    /// <summary>
    ///     ID: {f9e6b610-60cf-4e6a-bcfc-40ccff3be8db}
    ///     Alias: WorkflowActionInstanceEditor
    ///     Caption: $CardTypes_TypesNames_ActionInstance
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowActionInstanceEditorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowActionInstanceEditor": {f9e6b610-60cf-4e6a-bcfc-40ccff3be8db}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf9e6b610,0x60cf,0x4e6a,0xbc,0xfc,0x40,0xcc,0xff,0x3b,0xe8,0xdb);

        /// <summary>
        ///     Card type name for "WorkflowActionInstanceEditor".
        /// </summary>
        public readonly string Alias = "WorkflowActionInstanceEditor";

        /// <summary>
        ///     Card type caption for "WorkflowActionInstanceEditor".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ActionInstance";

        /// <summary>
        ///     Card type group for "WorkflowActionInstanceEditor".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Forms

        public readonly string FormWorkflowActionInstanceEditor = "WorkflowActionInstanceEditor";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string EditParametersButton = nameof(EditParametersButton);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowActionInstanceEditorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowAddFileFromTemplateAction

    /// <summary>
    ///     ID: {8b0a3173-b495-4978-b445-23a0e79eda25}
    ///     Alias: WorkflowAddFileFromTemplateAction
    ///     Caption: $CardTypes_TypesNames_AddFileFromTemplate
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowAddFileFromTemplateActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowAddFileFromTemplateAction": {8b0a3173-b495-4978-b445-23a0e79eda25}.
        /// </summary>
        public readonly Guid ID = new Guid(0x8b0a3173,0xb495,0x4978,0xb4,0x45,0x23,0xa0,0xe7,0x9e,0xda,0x25);

        /// <summary>
        ///     Card type name for "WorkflowAddFileFromTemplateAction".
        /// </summary>
        public readonly string Alias = "WorkflowAddFileFromTemplateAction";

        /// <summary>
        ///     Card type caption for "WorkflowAddFileFromTemplateAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_AddFileFromTemplate";

        /// <summary>
        ///     Card type group for "WorkflowAddFileFromTemplateAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowAddFileFromTemplateAction = "WorkflowAddFileFromTemplateAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowAddFileFromTemplateActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowAndAction

    /// <summary>
    ///     ID: {575959db-4dd4-4b82-bbe9-445733b8e7dd}
    ///     Alias: WorkflowAndAction
    ///     Caption: $CardTypes_TypesNames_And
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowAndActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowAndAction": {575959db-4dd4-4b82-bbe9-445733b8e7dd}.
        /// </summary>
        public readonly Guid ID = new Guid(0x575959db,0x4dd4,0x4b82,0xbb,0xe9,0x44,0x57,0x33,0xb8,0xe7,0xdd);

        /// <summary>
        ///     Card type name for "WorkflowAndAction".
        /// </summary>
        public readonly string Alias = "WorkflowAndAction";

        /// <summary>
        ///     Card type caption for "WorkflowAndAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_And";

        /// <summary>
        ///     Card type group for "WorkflowAndAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowAndActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowCheckRolesForExecutionTileExtension

    /// <summary>
    ///     ID: {0295bfb2-acf1-415b-aa6d-b272d8ebc3fe}
    ///     Alias: WorkflowCheckRolesForExecutionTileExtension
    ///     Caption: $WorkflowEngine_TilePermission_CheckRolesForExecution
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowCheckRolesForExecutionTileExtensionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowCheckRolesForExecutionTileExtension": {0295bfb2-acf1-415b-aa6d-b272d8ebc3fe}.
        /// </summary>
        public readonly Guid ID = new Guid(0x0295bfb2,0xacf1,0x415b,0xaa,0x6d,0xb2,0x72,0xd8,0xeb,0xc3,0xfe);

        /// <summary>
        ///     Card type name for "WorkflowCheckRolesForExecutionTileExtension".
        /// </summary>
        public readonly string Alias = "WorkflowCheckRolesForExecutionTileExtension";

        /// <summary>
        ///     Card type caption for "WorkflowCheckRolesForExecutionTileExtension".
        /// </summary>
        public readonly string Caption = "$WorkflowEngine_TilePermission_CheckRolesForExecution";

        /// <summary>
        ///     Card type group for "WorkflowCheckRolesForExecutionTileExtension".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Forms

        public readonly string FormWorkflowCheckRolesForExecutionTileExtension = "WorkflowCheckRolesForExecutionTileExtension";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowCheckRolesForExecutionTileExtensionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowCommandAction

    /// <summary>
    ///     ID: {91b1cd51-da7a-4721-bb80-f5908fc936a2}
    ///     Alias: WorkflowCommandAction
    ///     Caption: $CardTypes_TypesNames_WaitingForSignal
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowCommandActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowCommandAction": {91b1cd51-da7a-4721-bb80-f5908fc936a2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x91b1cd51,0xda7a,0x4721,0xbb,0x80,0xf5,0x90,0x8f,0xc9,0x36,0xa2);

        /// <summary>
        ///     Card type name for "WorkflowCommandAction".
        /// </summary>
        public readonly string Alias = "WorkflowCommandAction";

        /// <summary>
        ///     Card type caption for "WorkflowCommandAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_WaitingForSignal";

        /// <summary>
        ///     Card type group for "WorkflowCommandAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowCommandAction = "WorkflowCommandAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowCommandActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowConditionAction

    /// <summary>
    ///     ID: {eb222506-6f7d-4c22-b3d2-d98a2f390ac5}
    ///     Alias: WorkflowConditionAction
    ///     Caption: $CardTypes_TypesNames_Condition
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowConditionActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowConditionAction": {eb222506-6f7d-4c22-b3d2-d98a2f390ac5}.
        /// </summary>
        public readonly Guid ID = new Guid(0xeb222506,0x6f7d,0x4c22,0xb3,0xd2,0xd9,0x8a,0x2f,0x39,0x0a,0xc5);

        /// <summary>
        ///     Card type name for "WorkflowConditionAction".
        /// </summary>
        public readonly string Alias = "WorkflowConditionAction";

        /// <summary>
        ///     Card type caption for "WorkflowConditionAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Condition";

        /// <summary>
        ///     Card type group for "WorkflowConditionAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowConditionAction = "WorkflowConditionAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        /// <summary>
        ///     Block caption "Block3" for "Block3".
        /// </summary>
        public readonly string BlockBlock3 = "Block3";

        #endregion

        #region Controls

        public readonly string Condition = nameof(Condition);
        public readonly string Conditions = nameof(Conditions);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowConditionActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowCreateCardAction

    /// <summary>
    ///     ID: {1c7a8067-cd49-45ab-a3c9-04a0cfe26c43}
    ///     Alias: WorkflowCreateCardAction
    ///     Caption: $CardTypes_TypesNames_CreateCard
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowCreateCardActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowCreateCardAction": {1c7a8067-cd49-45ab-a3c9-04a0cfe26c43}.
        /// </summary>
        public readonly Guid ID = new Guid(0x1c7a8067,0xcd49,0x45ab,0xa3,0xc9,0x04,0xa0,0xcf,0xe2,0x6c,0x43);

        /// <summary>
        ///     Card type name for "WorkflowCreateCardAction".
        /// </summary>
        public readonly string Alias = "WorkflowCreateCardAction";

        /// <summary>
        ///     Card type caption for "WorkflowCreateCardAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_CreateCard";

        /// <summary>
        ///     Card type group for "WorkflowCreateCardAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowCreateCardAction = "WorkflowCreateCardAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowCreateCardActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowDialogAction

    /// <summary>
    ///     ID: {fc004cdf-e9ed-4549-9f35-781cbc779af2}
    ///     Alias: WorkflowDialogAction
    ///     Caption: $CardTypes_TypesNames_Dialog
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowDialogActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowDialogAction": {fc004cdf-e9ed-4549-9f35-781cbc779af2}.
        /// </summary>
        public readonly Guid ID = new Guid(0xfc004cdf,0xe9ed,0x4549,0x9f,0x35,0x78,0x1c,0xbc,0x77,0x9a,0xf2);

        /// <summary>
        ///     Card type name for "WorkflowDialogAction".
        /// </summary>
        public readonly string Alias = "WorkflowDialogAction";

        /// <summary>
        ///     Card type caption for "WorkflowDialogAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Dialog";

        /// <summary>
        ///     Card type group for "WorkflowDialogAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowDialogAction = "WorkflowDialogAction";

        #endregion

        #region Blocks

        public readonly string BlockMainBlock = "MainBlock";
        public readonly string BlockTaskBlock = "TaskBlock";
        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockScripts = "Scripts";

        #endregion

        #region Controls

        public readonly string DialogType = nameof(DialogType);
        public readonly string DialogStoreMode = nameof(DialogStoreMode);
        public readonly string DialogOpenMode = nameof(DialogOpenMode);
        public readonly string KeepFiles = nameof(KeepFiles);
        public readonly string IsCloseWithoutConfirmation = nameof(IsCloseWithoutConfirmation);
        public readonly string ButtonCancel = nameof(ButtonCancel);
        public readonly string ButtonNotEnd = nameof(ButtonNotEnd);
        public readonly string ButtonLinks = nameof(ButtonLinks);
        public readonly string ButtonScenario = nameof(ButtonScenario);
        public readonly string ButtonSettings = nameof(ButtonSettings);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowDialogActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowEndAction

    /// <summary>
    ///     ID: {86913540-9f30-4a6b-b0d5-a24a674b2ef2}
    ///     Alias: WorkflowEndAction
    ///     Caption: $CardTypes_TypesNames_EndProcess
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowEndActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowEndAction": {86913540-9f30-4a6b-b0d5-a24a674b2ef2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x86913540,0x9f30,0x4a6b,0xb0,0xd5,0xa2,0x4a,0x67,0x4b,0x2e,0xf2);

        /// <summary>
        ///     Card type name for "WorkflowEndAction".
        /// </summary>
        public readonly string Alias = "WorkflowEndAction";

        /// <summary>
        ///     Card type caption for "WorkflowEndAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_EndProcess";

        /// <summary>
        ///     Card type group for "WorkflowEndAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowEndAction = "WorkflowEndAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEndActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowEngineSettings

    /// <summary>
    ///     ID: {a73da16b-d69b-4957-a2de-b35adebcd85e}
    ///     Alias: WorkflowEngineSettings
    ///     Caption: $CardTypes_TypesNames_WorkflowEngineSettings
    ///     Group: Settings
    /// </summary>
    public class WorkflowEngineSettingsTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowEngineSettings": {a73da16b-d69b-4957-a2de-b35adebcd85e}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa73da16b,0xd69b,0x4957,0xa2,0xde,0xb3,0x5a,0xde,0xbc,0xd8,0x5e);

        /// <summary>
        ///     Card type name for "WorkflowEngineSettings".
        /// </summary>
        public readonly string Alias = "WorkflowEngineSettings";

        /// <summary>
        ///     Card type caption for "WorkflowEngineSettings".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_WorkflowEngineSettings";

        /// <summary>
        ///     Card type group for "WorkflowEngineSettings".
        /// </summary>
        public readonly string Group = "Settings";

        #endregion

        #region Forms

        public readonly string FormWorkflowEngineSettings = "WorkflowEngineSettings";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "License" for "License".
        /// </summary>
        public readonly string BlockLicense = "License";

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "LicenseHint" for "LicenseHint".
        /// </summary>
        public readonly string LicenseHint = nameof(LicenseHint);

        public readonly string TypeSettings = nameof(TypeSettings);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineSettingsTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowHistoryManagementAction

    /// <summary>
    ///     ID: {4e397642-1672-4598-8851-7ed18378c915}
    ///     Alias: WorkflowHistoryManagementAction
    ///     Caption: $CardTypes_TypesNames_HistoryManagementAction
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowHistoryManagementActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowHistoryManagementAction": {4e397642-1672-4598-8851-7ed18378c915}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4e397642,0x1672,0x4598,0x88,0x51,0x7e,0xd1,0x83,0x78,0xc9,0x15);

        /// <summary>
        ///     Card type name for "WorkflowHistoryManagementAction".
        /// </summary>
        public readonly string Alias = "WorkflowHistoryManagementAction";

        /// <summary>
        ///     Card type caption for "WorkflowHistoryManagementAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_HistoryManagementAction";

        /// <summary>
        ///     Card type group for "WorkflowHistoryManagementAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowHistoryManagementAction = "WorkflowHistoryManagementAction";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "KrTaskHistoryBlockAlias".
        /// </summary>
        public readonly string BlockKrTaskHistoryBlockAlias = "KrTaskHistoryBlockAlias";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowHistoryManagementActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowLinkEditor

    /// <summary>
    ///     ID: {eb4d7730-6104-4f37-9f63-0c0fa8baf3d1}
    ///     Alias: WorkflowLinkEditor
    ///     Caption: $CardTypes_TypesNames_Link
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowLinkEditorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowLinkEditor": {eb4d7730-6104-4f37-9f63-0c0fa8baf3d1}.
        /// </summary>
        public readonly Guid ID = new Guid(0xeb4d7730,0x6104,0x4f37,0x9f,0x63,0x0c,0x0f,0xa8,0xba,0xf3,0xd1);

        /// <summary>
        ///     Card type name for "WorkflowLinkEditor".
        /// </summary>
        public readonly string Alias = "WorkflowLinkEditor";

        /// <summary>
        ///     Card type caption for "WorkflowLinkEditor".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Link";

        /// <summary>
        ///     Card type group for "WorkflowLinkEditor".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Forms

        public readonly string FormWorkflowLinkEditor = "WorkflowLinkEditor";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockAdditional = "Additional";
        public readonly string BlockExitSettings = "ExitSettings";
        public readonly string BlockEnterSettings = "EnterSettings";

        #endregion

        #region Controls

        public readonly string CompileButton = nameof(CompileButton);
        public readonly string OpenFileButton = nameof(OpenFileButton);
        public readonly string LockProcess = nameof(LockProcess);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowLinkEditorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowNewItemEditor

    /// <summary>
    ///     ID: {798aafa5-4bfc-4c16-9e6f-9a761e83d6cc}
    ///     Alias: WorkflowNewItemEditor
    ///     Caption: Редатор элементов на левой панели (пока не используется)
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowNewItemEditorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowNewItemEditor": {798aafa5-4bfc-4c16-9e6f-9a761e83d6cc}.
        /// </summary>
        public readonly Guid ID = new Guid(0x798aafa5,0x4bfc,0x4c16,0x9e,0x6f,0x9a,0x76,0x1e,0x83,0xd6,0xcc);

        /// <summary>
        ///     Card type name for "WorkflowNewItemEditor".
        /// </summary>
        public readonly string Alias = "WorkflowNewItemEditor";

        /// <summary>
        ///     Card type caption for "WorkflowNewItemEditor".
        /// </summary>
        public readonly string Caption = "Редатор элементов на левой панели (пока не используется)";

        /// <summary>
        ///     Card type group for "WorkflowNewItemEditor".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Forms

        /// <summary>
        ///     Form caption "Редактор шаблона элемента" for "WorkflowNewItemEditor".
        /// </summary>
        public readonly string FormWorkflowNewItemEditor = "WorkflowNewItemEditor";

        #endregion

        #region Blocks

        /// <summary>
        ///     Block caption "Block1" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        /// <summary>
        ///     Control caption "Редактировать шаблон" for "EditTemplateButton".
        /// </summary>
        public readonly string EditTemplateButton = nameof(EditTemplateButton);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowNewItemEditorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowNodeEditor

    /// <summary>
    ///     ID: {f14d88f8-5e3d-4609-b377-0ad9f647d87a}
    ///     Alias: WorkflowNodeEditor
    ///     Caption: $CardTypes_TypesNames_NodeTemplate
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowNodeEditorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowNodeEditor": {f14d88f8-5e3d-4609-b377-0ad9f647d87a}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf14d88f8,0x5e3d,0x4609,0xb3,0x77,0x0a,0xd9,0xf6,0x47,0xd8,0x7a);

        /// <summary>
        ///     Card type name for "WorkflowNodeEditor".
        /// </summary>
        public readonly string Alias = "WorkflowNodeEditor";

        /// <summary>
        ///     Card type caption for "WorkflowNodeEditor".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_NodeTemplate";

        /// <summary>
        ///     Card type group for "WorkflowNodeEditor".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Forms

        public readonly string FormWorkflowNodeEditor = "WorkflowNodeEditor";

        #endregion

        #region Blocks

        public readonly string BlockMain = "Main";
        public readonly string BlockNodeInstances = "NodeInstances";

        /// <summary>
        ///     Block caption "Настройка входящей связи" for "Block1".
        /// </summary>
        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock2 = "Block2";

        #endregion

        #region Controls

        public readonly string Icon = nameof(Icon);
        public readonly string EditParametersButton = nameof(EditParametersButton);
        public readonly string CompileButton = nameof(CompileButton);
        public readonly string Actions = nameof(Actions);
        public readonly string NodeInstances = nameof(NodeInstances);

        /// <summary>
        ///     Control caption "Блокировать процесс при асинхронной обработке" for "LockProcess".
        /// </summary>
        public readonly string LockProcess = nameof(LockProcess);
        public readonly string InLinks = nameof(InLinks);
        public readonly string OutLinks = nameof(OutLinks);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowNodeEditorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowNodeInstanceEditor

    /// <summary>
    ///     ID: {34d4cdae-de49-471d-af77-ab9455bfb8cd}
    ///     Alias: WorkflowNodeInstanceEditor
    ///     Caption: $CardTypes_TypesNames_NodeInstance
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowNodeInstanceEditorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowNodeInstanceEditor": {34d4cdae-de49-471d-af77-ab9455bfb8cd}.
        /// </summary>
        public readonly Guid ID = new Guid(0x34d4cdae,0xde49,0x471d,0xaf,0x77,0xab,0x94,0x55,0xbf,0xb8,0xcd);

        /// <summary>
        ///     Card type name for "WorkflowNodeInstanceEditor".
        /// </summary>
        public readonly string Alias = "WorkflowNodeInstanceEditor";

        /// <summary>
        ///     Card type caption for "WorkflowNodeInstanceEditor".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_NodeInstance";

        /// <summary>
        ///     Card type group for "WorkflowNodeInstanceEditor".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Forms

        public readonly string FormWorkflowNodeInstanceEditor = "WorkflowNodeInstanceEditor";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockTasks = "Tasks";
        public readonly string BlockSubprocesses = "Subprocesses";

        #endregion

        #region Controls

        public readonly string EditParametersButton = nameof(EditParametersButton);
        public readonly string Actions = nameof(Actions);
        public readonly string Tasks = nameof(Tasks);
        public readonly string Subprocesses = nameof(Subprocesses);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowNodeInstanceEditorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowNotificationAction

    /// <summary>
    ///     ID: {d5e2b20d-46c6-4f23-8ede-820b5a8381a3}
    ///     Alias: WorkflowNotificationAction
    ///     Caption: $CardTypes_TypesNames_NotificationAction
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowNotificationActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowNotificationAction": {d5e2b20d-46c6-4f23-8ede-820b5a8381a3}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd5e2b20d,0x46c6,0x4f23,0x8e,0xde,0x82,0x0b,0x5a,0x83,0x81,0xa3);

        /// <summary>
        ///     Card type name for "WorkflowNotificationAction".
        /// </summary>
        public readonly string Alias = "WorkflowNotificationAction";

        /// <summary>
        ///     Card type caption for "WorkflowNotificationAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_NotificationAction";

        /// <summary>
        ///     Card type group for "WorkflowNotificationAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowNotificationAction = "WorkflowNotificationAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockCustomEmail = "CustomEmail";

        #endregion

        #region Controls

        public readonly string CustomNotificationType = nameof(CustomNotificationType);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowNotificationActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowProcess

    /// <summary>
    ///     ID: {bb3e1452-30da-4eb2-a4fe-10871608bed3}
    ///     Alias: WorkflowProcess
    ///     Caption: $CardTypes_TypesNames_ProcessInstance
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowProcessTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowProcess": {bb3e1452-30da-4eb2-a4fe-10871608bed3}.
        /// </summary>
        public readonly Guid ID = new Guid(0xbb3e1452,0x30da,0x4eb2,0xa4,0xfe,0x10,0x87,0x16,0x08,0xbe,0xd3);

        /// <summary>
        ///     Card type name for "WorkflowProcess".
        /// </summary>
        public readonly string Alias = "WorkflowProcess";

        /// <summary>
        ///     Card type caption for "WorkflowProcess".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ProcessInstance";

        /// <summary>
        ///     Card type group for "WorkflowProcess".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowProcessTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowProcessEditor

    /// <summary>
    ///     ID: {d7420bf0-ce4e-4155-9b97-0117b9d1e13c}
    ///     Alias: WorkflowProcessEditor
    ///     Caption: $CardTypes_TypesNames_ProcessTemplate
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowProcessEditorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowProcessEditor": {d7420bf0-ce4e-4155-9b97-0117b9d1e13c}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd7420bf0,0xce4e,0x4155,0x9b,0x97,0x01,0x17,0xb9,0xd1,0xe1,0x3c);

        /// <summary>
        ///     Card type name for "WorkflowProcessEditor".
        /// </summary>
        public readonly string Alias = "WorkflowProcessEditor";

        /// <summary>
        ///     Card type caption for "WorkflowProcessEditor".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ProcessTemplate";

        /// <summary>
        ///     Card type group for "WorkflowProcessEditor".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Forms

        public readonly string FormWorkflowProcessEditor = "WorkflowProcessEditor";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        /// <summary>
        ///     Block caption "ScriptManagement" for "ScriptManagement".
        /// </summary>
        public readonly string BlockScriptManagement = "ScriptManagement";

        /// <summary>
        ///     Block caption "Script" for "Script".
        /// </summary>
        public readonly string BlockScript = "Script";

        #endregion

        #region Controls

        public readonly string EditParametersButton = nameof(EditParametersButton);
        public readonly string CompileButton = nameof(CompileButton);
        public readonly string OpenFileButton = nameof(OpenFileButton);
        public readonly string SelectProject = nameof(SelectProject);
        public readonly string UpdateProject = nameof(UpdateProject);
        public readonly string UpdateProcess = nameof(UpdateProcess);
        public readonly string ShowReferences = nameof(ShowReferences);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowProcessEditorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowProcessInstanceEditor

    /// <summary>
    ///     ID: {584680fa-e289-42dc-8c88-91ece7440316}
    ///     Alias: WorkflowProcessInstanceEditor
    ///     Caption: $CardTypes_TypesNames_ProcessInstance
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowProcessInstanceEditorTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowProcessInstanceEditor": {584680fa-e289-42dc-8c88-91ece7440316}.
        /// </summary>
        public readonly Guid ID = new Guid(0x584680fa,0xe289,0x42dc,0x8c,0x88,0x91,0xec,0xe7,0x44,0x03,0x16);

        /// <summary>
        ///     Card type name for "WorkflowProcessInstanceEditor".
        /// </summary>
        public readonly string Alias = "WorkflowProcessInstanceEditor";

        /// <summary>
        ///     Card type caption for "WorkflowProcessInstanceEditor".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_ProcessInstance";

        /// <summary>
        ///     Card type group for "WorkflowProcessInstanceEditor".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Forms

        public readonly string FormWorkflowProcessInstanceEditor = "WorkflowProcessInstanceEditor";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockErrors = "Errors";

        #endregion

        #region Controls

        public readonly string EditParametersButton = nameof(EditParametersButton);
        public readonly string Errors = nameof(Errors);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowProcessInstanceEditorTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowScenarioAction

    /// <summary>
    ///     ID: {a7179d00-8641-4bde-b1ce-0f97053fe1e2}
    ///     Alias: WorkflowScenarioAction
    ///     Caption: $CardTypes_TypesNames_Scenario
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowScenarioActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowScenarioAction": {a7179d00-8641-4bde-b1ce-0f97053fe1e2}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa7179d00,0x8641,0x4bde,0xb1,0xce,0x0f,0x97,0x05,0x3f,0xe1,0xe2);

        /// <summary>
        ///     Card type name for "WorkflowScenarioAction".
        /// </summary>
        public readonly string Alias = "WorkflowScenarioAction";

        /// <summary>
        ///     Card type caption for "WorkflowScenarioAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Scenario";

        /// <summary>
        ///     Card type group for "WorkflowScenarioAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowScenarioAction = "WorkflowScenarioAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowScenarioActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowSendSignalAction

    /// <summary>
    ///     ID: {3e2ea605-b60d-42e4-ace9-97d2f343a778}
    ///     Alias: WorkflowSendSignalAction
    ///     Caption: $CardTypes_TypesNames_SendSignal
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowSendSignalActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowSendSignalAction": {3e2ea605-b60d-42e4-ace9-97d2f343a778}.
        /// </summary>
        public readonly Guid ID = new Guid(0x3e2ea605,0xb60d,0x42e4,0xac,0xe9,0x97,0xd2,0xf3,0x43,0xa7,0x78);

        /// <summary>
        ///     Card type name for "WorkflowSendSignalAction".
        /// </summary>
        public readonly string Alias = "WorkflowSendSignalAction";

        /// <summary>
        ///     Card type caption for "WorkflowSendSignalAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_SendSignal";

        /// <summary>
        ///     Card type group for "WorkflowSendSignalAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowSendSignalAction = "WorkflowSendSignalAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowSendSignalActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowStartAction

    /// <summary>
    ///     ID: {a4f7fb59-cc66-4aba-b593-3f4d26698666}
    ///     Alias: WorkflowStartAction
    ///     Caption: $CardTypes_TypesNames_StartProcess
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowStartActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowStartAction": {a4f7fb59-cc66-4aba-b593-3f4d26698666}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa4f7fb59,0xcc66,0x4aba,0xb5,0x93,0x3f,0x4d,0x26,0x69,0x86,0x66);

        /// <summary>
        ///     Card type name for "WorkflowStartAction".
        /// </summary>
        public readonly string Alias = "WorkflowStartAction";

        /// <summary>
        ///     Card type caption for "WorkflowStartAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_StartProcess";

        /// <summary>
        ///     Card type group for "WorkflowStartAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowStartAction = "WorkflowStartAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowStartActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowSubprocessAction

    /// <summary>
    ///     ID: {d1d3acfd-8a78-4009-ac38-4dd851c8176a}
    ///     Alias: WorkflowSubprocessAction
    ///     Caption: $CardTypes_TypesNames_Subprocess
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowSubprocessActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowSubprocessAction": {d1d3acfd-8a78-4009-ac38-4dd851c8176a}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd1d3acfd,0x8a78,0x4009,0xac,0x38,0x4d,0xd8,0x51,0xc8,0x17,0x6a);

        /// <summary>
        ///     Card type name for "WorkflowSubprocessAction".
        /// </summary>
        public readonly string Alias = "WorkflowSubprocessAction";

        /// <summary>
        ///     Card type caption for "WorkflowSubprocessAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Subprocess";

        /// <summary>
        ///     Card type group for "WorkflowSubprocessAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowSubprocessAction = "WorkflowSubprocessAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string OpenSubprocessEditor = nameof(OpenSubprocessEditor);
        public readonly string ProcessParam = nameof(ProcessParam);
        public readonly string StartMappingTable = nameof(StartMappingTable);
        public readonly string EndMappingTable = nameof(EndMappingTable);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowSubprocessActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowSubprocessControlAction

    /// <summary>
    ///     ID: {3dc924c7-a7c8-407c-a7c7-5d2d77e6e1bc}
    ///     Alias: WorkflowSubprocessControlAction
    ///     Caption: $CardTypes_TypesNames_SubprocessControl
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowSubprocessControlActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowSubprocessControlAction": {3dc924c7-a7c8-407c-a7c7-5d2d77e6e1bc}.
        /// </summary>
        public readonly Guid ID = new Guid(0x3dc924c7,0xa7c8,0x407c,0xa7,0xc7,0x5d,0x2d,0x77,0xe6,0xe1,0xbc);

        /// <summary>
        ///     Card type name for "WorkflowSubprocessControlAction".
        /// </summary>
        public readonly string Alias = "WorkflowSubprocessControlAction";

        /// <summary>
        ///     Card type caption for "WorkflowSubprocessControlAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_SubprocessControl";

        /// <summary>
        ///     Card type group for "WorkflowSubprocessControlAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowSubprocessControlAction = "WorkflowSubprocessControlAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowSubprocessControlActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowTaskAction

    /// <summary>
    ///     ID: {dc541752-496d-45eb-ad71-92ab186d7601}
    ///     Alias: WorkflowTaskAction
    ///     Caption: $CardTypes_TypesNames_Task
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowTaskActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowTaskAction": {dc541752-496d-45eb-ad71-92ab186d7601}.
        /// </summary>
        public readonly Guid ID = new Guid(0xdc541752,0x496d,0x45eb,0xad,0x71,0x92,0xab,0x18,0x6d,0x76,0x01);

        /// <summary>
        ///     Card type name for "WorkflowTaskAction".
        /// </summary>
        public readonly string Alias = "WorkflowTaskAction";

        /// <summary>
        ///     Card type caption for "WorkflowTaskAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Task";

        /// <summary>
        ///     Card type group for "WorkflowTaskAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowTaskAction = "WorkflowTaskAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockMainBlock = "MainBlock";
        public readonly string BlockScripts = "Scripts";

        #endregion

        #region Controls

        public readonly string DialogType = nameof(DialogType);
        public readonly string ButtonCancel = nameof(ButtonCancel);
        public readonly string ButtonNotEnd = nameof(ButtonNotEnd);
        public readonly string ButtonLinks = nameof(ButtonLinks);
        public readonly string ButtonScenario = nameof(ButtonScenario);
        public readonly string ButtonSettings = nameof(ButtonSettings);
        public readonly string DialogsTable = nameof(DialogsTable);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowTaskActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowTaskControlAction

    /// <summary>
    ///     ID: {c80a6abd-361d-4290-bca4-3df7a03bbc10}
    ///     Alias: WorkflowTaskControlAction
    ///     Caption: $CardTypes_TypesNames_TaskControl
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowTaskControlActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowTaskControlAction": {c80a6abd-361d-4290-bca4-3df7a03bbc10}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc80a6abd,0x361d,0x4290,0xbc,0xa4,0x3d,0xf7,0xa0,0x3b,0xbc,0x10);

        /// <summary>
        ///     Card type name for "WorkflowTaskControlAction".
        /// </summary>
        public readonly string Alias = "WorkflowTaskControlAction";

        /// <summary>
        ///     Card type caption for "WorkflowTaskControlAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TaskControl";

        /// <summary>
        ///     Card type group for "WorkflowTaskControlAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowTaskControlAction = "WorkflowTaskControlAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowTaskControlActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowTaskGroupAction

    /// <summary>
    ///     ID: {b0d0d053-4ef2-46bc-b3bf-5c701532760e}
    ///     Alias: WorkflowTaskGroupAction
    ///     Caption: $CardTypes_TypesNames_TaskGroup
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowTaskGroupActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowTaskGroupAction": {b0d0d053-4ef2-46bc-b3bf-5c701532760e}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb0d0d053,0x4ef2,0x46bc,0xb3,0xbf,0x5c,0x70,0x15,0x32,0x76,0x0e);

        /// <summary>
        ///     Card type name for "WorkflowTaskGroupAction".
        /// </summary>
        public readonly string Alias = "WorkflowTaskGroupAction";

        /// <summary>
        ///     Card type caption for "WorkflowTaskGroupAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TaskGroup";

        /// <summary>
        ///     Card type group for "WorkflowTaskGroupAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowTaskGroupAction = "WorkflowTaskGroupAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";
        public readonly string BlockBlock2 = "Block2";
        public readonly string BlockBlock3 = "Block3";
        public readonly string BlockMainBlock = "MainBlock";
        public readonly string BlockScripts = "Scripts";

        #endregion

        #region Controls

        public readonly string DialogType = nameof(DialogType);
        public readonly string ButtonCancel = nameof(ButtonCancel);
        public readonly string ButtonNotEnd = nameof(ButtonNotEnd);
        public readonly string ButtonLinks = nameof(ButtonLinks);
        public readonly string ButtonScenario = nameof(ButtonScenario);
        public readonly string ButtonSettings = nameof(ButtonSettings);
        public readonly string DialogsTable = nameof(DialogsTable);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowTaskGroupActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowTaskGroupControlAction

    /// <summary>
    ///     ID: {ec60ba12-edd9-48d5-be49-6a2f4caf22fb}
    ///     Alias: WorkflowTaskGroupControlAction
    ///     Caption: $CardTypes_TypesNames_TaskGroupControl
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowTaskGroupControlActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowTaskGroupControlAction": {ec60ba12-edd9-48d5-be49-6a2f4caf22fb}.
        /// </summary>
        public readonly Guid ID = new Guid(0xec60ba12,0xedd9,0x48d5,0xbe,0x49,0x6a,0x2f,0x4c,0xaf,0x22,0xfb);

        /// <summary>
        ///     Card type name for "WorkflowTaskGroupControlAction".
        /// </summary>
        public readonly string Alias = "WorkflowTaskGroupControlAction";

        /// <summary>
        ///     Card type caption for "WorkflowTaskGroupControlAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TaskGroupControl";

        /// <summary>
        ///     Card type group for "WorkflowTaskGroupControlAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowTaskGroupControlAction = "WorkflowTaskGroupControlAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string UseAsNextRole = nameof(UseAsNextRole);
        public readonly string ResumeGroup = nameof(ResumeGroup);
        public readonly string PauseGroup = nameof(PauseGroup);
        public readonly string CancelGroup = nameof(CancelGroup);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowTaskGroupControlActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowTimerAction

    /// <summary>
    ///     ID: {a2d50ba1-434b-406a-a31c-f86c821b6b20}
    ///     Alias: WorkflowTimerAction
    ///     Caption: $CardTypes_TypesNames_Timer
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowTimerActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowTimerAction": {a2d50ba1-434b-406a-a31c-f86c821b6b20}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa2d50ba1,0x434b,0x406a,0xa3,0x1c,0xf8,0x6c,0x82,0x1b,0x6b,0x20);

        /// <summary>
        ///     Card type name for "WorkflowTimerAction".
        /// </summary>
        public readonly string Alias = "WorkflowTimerAction";

        /// <summary>
        ///     Card type caption for "WorkflowTimerAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Timer";

        /// <summary>
        ///     Card type group for "WorkflowTimerAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowTimerAction = "WorkflowTimerAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region Controls

        public readonly string ExitCondition = nameof(ExitCondition);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowTimerActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowTimerControlAction

    /// <summary>
    ///     ID: {fd97cf1e-a1c6-4b5f-9b0c-1b254735e17a}
    ///     Alias: WorkflowTimerControlAction
    ///     Caption: $CardTypes_TypesNames_TimerControl
    ///     Group: WorkflowActions
    /// </summary>
    public class WorkflowTimerControlActionTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "WorkflowTimerControlAction": {fd97cf1e-a1c6-4b5f-9b0c-1b254735e17a}.
        /// </summary>
        public readonly Guid ID = new Guid(0xfd97cf1e,0xa1c6,0x4b5f,0x9b,0x0c,0x1b,0x25,0x47,0x35,0xe1,0x7a);

        /// <summary>
        ///     Card type name for "WorkflowTimerControlAction".
        /// </summary>
        public readonly string Alias = "WorkflowTimerControlAction";

        /// <summary>
        ///     Card type caption for "WorkflowTimerControlAction".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_TimerControl";

        /// <summary>
        ///     Card type group for "WorkflowTimerControlAction".
        /// </summary>
        public readonly string Group = "WorkflowActions";

        #endregion

        #region Forms

        public readonly string FormWorkflowTimerControlAction = "WorkflowTimerControlAction";

        #endregion

        #region Blocks

        public readonly string BlockBlock1 = "Block1";

        #endregion

        #region ToString

        public static implicit operator string(WorkflowTimerControlActionTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Workplace

    /// <summary>
    ///     ID: {12fd0e19-d751-4d8e-959d-78c9763bbb38}
    ///     Alias: Workplace
    ///     Caption: $CardTypes_TypesNames_Workplace
    ///     Group: System
    /// </summary>
    public class WorkplaceTypeInfo
    {
        #region Common

        /// <summary>
        ///     Card type identifier for "Workplace": {12fd0e19-d751-4d8e-959d-78c9763bbb38}.
        /// </summary>
        public readonly Guid ID = new Guid(0x12fd0e19,0xd751,0x4d8e,0x95,0x9d,0x78,0xc9,0x76,0x3b,0xbb,0x38);

        /// <summary>
        ///     Card type name for "Workplace".
        /// </summary>
        public readonly string Alias = "Workplace";

        /// <summary>
        ///     Card type caption for "Workplace".
        /// </summary>
        public readonly string Caption = "$CardTypes_TypesNames_Workplace";

        /// <summary>
        ///     Card type group for "Workplace".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Forms

        public readonly string FormWorkplace = "Workplace";

        #endregion

        #region Blocks

        public readonly string BlockMainInformation = "MainInformation";
        public readonly string BlockRoles = "Roles";

        #endregion

        #region ToString

        public static implicit operator string(WorkplaceTypeInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    public static class TypeInfo
    {
        #region Const

        public const string CurrentRow = nameof(CurrentRow);
        public const string SelectedRow = nameof(SelectedRow);
        public const string SelectedItem = nameof(SelectedItem);
        public const string SelectedDate = nameof(SelectedDate);
        public const string IsActive = nameof(IsActive);
        public const string IsChecked = nameof(IsChecked);
        public const string IsExpanded = nameof(IsExpanded);
        public const string LastUpdateTime = nameof(LastUpdateTime);
        public const string Parent = nameof(Parent);

        #endregion

        #region Types

        /// <summary>
        ///     Card type caption "$CardTypes_Blocks_AccountSettings" for "AccountUserSettings".
        /// </summary>
        public static readonly AccountUserSettingsTypeInfo AccountUserSettings = new AccountUserSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_AclGenerationRule" for "AclGenerationRule".
        /// </summary>
        public static readonly AclGenerationRuleTypeInfo AclGenerationRule = new AclGenerationRuleTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ActionHistoryRecord" for "ActionHistoryRecord".
        /// </summary>
        public static readonly ActionHistoryRecordTypeInfo ActionHistoryRecord = new ActionHistoryRecordTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ADSync" for "AdSync".
        /// </summary>
        public static readonly AdSyncTypeInfo AdSync = new AdSyncTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Application" for "Application".
        /// </summary>
        public static readonly ApplicationTypeInfo Application = new ApplicationTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_AuthorCondition" for "AuthorCondition".
        /// </summary>
        public static readonly AuthorConditionTypeInfo AuthorCondition = new AuthorConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "AutoCompleteDialogs".
        /// </summary>
        public static readonly AutoCompleteDialogsTypeInfo AutoCompleteDialogs = new AutoCompleteDialogsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_BusinessProcessTemplate" for "BusinessProcessTemplate".
        /// </summary>
        public static readonly BusinessProcessTemplateTypeInfo BusinessProcessTemplate = new BusinessProcessTemplateTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Tabs_Calendar" for "Calendar".
        /// </summary>
        public static readonly CalendarTypeInfo Calendar = new CalendarTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Tabs_Controls_CalendarCalcMethod" for "CalendarCalcMethod".
        /// </summary>
        public static readonly CalendarCalcMethodTypeInfo CalendarCalcMethod = new CalendarCalcMethodTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Car" for "Car".
        /// </summary>
        public static readonly CarTypeInfo Car = new CarTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_Tabs_Dialog1C" for "Car1CDialog".
        /// </summary>
        public static readonly Car1CDialogTypeInfo Car1CDialog = new Car1CDialogTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_CardTasksEditor" for "CardTasksEditorDialog".
        /// </summary>
        public static readonly CardTasksEditorDialogTypeInfo CardTasksEditorDialog = new CardTasksEditorDialogTypeInfo();

        /// <summary>
        ///     Card type caption "$Views_FilterDialog_Caption" for "CarViewParameters".
        /// </summary>
        public static readonly CarViewParametersTypeInfo CarViewParameters = new CarViewParametersTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_CompletionOption" for "CompletionOption".
        /// </summary>
        public static readonly CompletionOptionTypeInfo CompletionOption = new CompletionOptionTypeInfo();

        /// <summary>
        ///     Card type caption "ConditionsBase" for "ConditionsBase".
        /// </summary>
        public static readonly ConditionsBaseTypeInfo ConditionsBase = new ConditionsBaseTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ConditionType" for "ConditionType".
        /// </summary>
        public static readonly ConditionTypeTypeInfo ConditionType = new ConditionTypeTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ContextRole" for "ContextRole".
        /// </summary>
        public static readonly ContextRoleTypeInfo ContextRole = new ContextRoleTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Contract" for "Contract".
        /// </summary>
        public static readonly ContractTypeInfo Contract = new ContractTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_FileFromTemplate_CreateDialog" for "CreateFileFromTemplate".
        /// </summary>
        public static readonly CreateFileFromTemplateTypeInfo CreateFileFromTemplate = new CreateFileFromTemplateTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Currency" for "Currency".
        /// </summary>
        public static readonly CurrencyTypeInfo Currency = new CurrencyTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Tabs_DefaultCalendarType" for "DefaultCalendarType".
        /// </summary>
        public static readonly DefaultCalendarTypeTypeInfo DefaultCalendarType = new DefaultCalendarTypeTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Deleted" for "Deleted".
        /// </summary>
        public static readonly DeletedTypeInfo Deleted = new DeletedTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_DepartmentCondition" for "DepartmentCondition".
        /// </summary>
        public static readonly DepartmentConditionTypeInfo DepartmentCondition = new DepartmentConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_DepartmentRole" for "DepartmentRole".
        /// </summary>
        public static readonly DepartmentRoleTypeInfo DepartmentRole = new DepartmentRoleTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Dialogs" for "Dialogs".
        /// </summary>
        public static readonly DialogsTypeInfo Dialogs = new DialogsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_DocLoad" for "DocLoad".
        /// </summary>
        public static readonly DocLoadTypeInfo DocLoad = new DocLoadTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_DocStateCondition" for "DocStateCondition".
        /// </summary>
        public static readonly DocStateConditionTypeInfo DocStateCondition = new DocStateConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_DocTypeCondition" for "DocTypeCondition".
        /// </summary>
        public static readonly DocTypeConditionTypeInfo DocTypeCondition = new DocTypeConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Document" for "Document".
        /// </summary>
        public static readonly DocumentTypeInfo Document = new DocumentTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Category" for "DocumentCategory".
        /// </summary>
        public static readonly DocumentCategoryTypeInfo DocumentCategory = new DocumentCategoryTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_DynamicRole" for "DynamicRole".
        /// </summary>
        public static readonly DynamicRoleTypeInfo DynamicRole = new DynamicRoleTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_EmptyCondition" for "EmptyCondition".
        /// </summary>
        public static readonly EmptyConditionTypeInfo EmptyCondition = new EmptyConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Error" for "Error".
        /// </summary>
        public static readonly ErrorTypeInfo Error = new ErrorTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_FieldChangedCondition" for "FieldChangedCondition".
        /// </summary>
        public static readonly FieldChangedConditionTypeInfo FieldChangedCondition = new FieldChangedConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_File" for "File".
        /// </summary>
        public static readonly FileTypeInfo File = new FileTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_FileCategory" for "FileCategory".
        /// </summary>
        public static readonly FileCategoryTypeInfo FileCategory = new FileCategoryTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_FileConverterCache" for "FileConverterCache".
        /// </summary>
        public static readonly FileConverterCacheTypeInfo FileConverterCache = new FileConverterCacheTypeInfo();

        /// <summary>
        ///     Card type caption "FilePreviewDialog" for "FilePreviewDialog".
        /// </summary>
        public static readonly FilePreviewDialogTypeInfo FilePreviewDialog = new FilePreviewDialogTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_FileSatellite" for "FileSatellite".
        /// </summary>
        public static readonly FileSatelliteTypeInfo FileSatellite = new FileSatelliteTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_FileTemplate" for "FileTemplate".
        /// </summary>
        public static readonly FileTemplateTypeInfo FileTemplate = new FileTemplateTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_FormatSettings" for "FormatSettings".
        /// </summary>
        public static readonly FormatSettingsTypeInfo FormatSettings = new FormatSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "ForumSatellite" for "ForumSatellite".
        /// </summary>
        public static readonly ForumSatelliteTypeInfo ForumSatellite = new ForumSatelliteTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_FunctionRole" for "FunctionRole".
        /// </summary>
        public static readonly FunctionRoleTypeInfo FunctionRole = new FunctionRoleTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_Blocks_GeneralSettings" for "GeneralUserSettings".
        /// </summary>
        public static readonly GeneralUserSettingsTypeInfo GeneralUserSettings = new GeneralUserSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_HelpSection" for "HelpSection".
        /// </summary>
        public static readonly HelpSectionTypeInfo HelpSection = new HelpSectionTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "HelpSectionDialogs".
        /// </summary>
        public static readonly HelpSectionDialogsTypeInfo HelpSectionDialogs = new HelpSectionDialogsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Incoming" for "Incoming".
        /// </summary>
        public static readonly IncomingTypeInfo Incoming = new IncomingTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_InitiatorCondition" for "InitiatorCondition".
        /// </summary>
        public static readonly InitiatorConditionTypeInfo InitiatorCondition = new InitiatorConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$KrActions_Acquaintance" for "KrAcquaintanceAction".
        /// </summary>
        public static readonly KrAcquaintanceActionTypeInfo KrAcquaintanceAction = new KrAcquaintanceActionTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_Acquaintance" for "KrAcquaintanceStageTypeSettings".
        /// </summary>
        public static readonly KrAcquaintanceStageTypeSettingsTypeInfo KrAcquaintanceStageTypeSettings = new KrAcquaintanceStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_AddFromTemplate" for "KrAddFileFromTemplateStageTypeSettings".
        /// </summary>
        public static readonly KrAddFileFromTemplateStageTypeSettingsTypeInfo KrAddFileFromTemplateStageTypeSettings = new KrAddFileFromTemplateStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrAdditionalApproval" for "KrAdditionalApproval".
        /// </summary>
        public static readonly KrAdditionalApprovalTypeInfo KrAdditionalApproval = new KrAdditionalApprovalTypeInfo();

        /// <summary>
        ///     Card type caption "$KrActions_Amending" for "KrAmendingAction".
        /// </summary>
        public static readonly KrAmendingActionTypeInfo KrAmendingAction = new KrAmendingActionTypeInfo();

        /// <summary>
        ///     Card type caption "$KrActions_Approval" for "KrApprovalAction".
        /// </summary>
        public static readonly KrApprovalActionTypeInfo KrApprovalAction = new KrApprovalActionTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_Approval" for "KrApprovalStageTypeSettings".
        /// </summary>
        public static readonly KrApprovalStageTypeSettingsTypeInfo KrApprovalStageTypeSettings = new KrApprovalStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrApprove" for "KrApprove".
        /// </summary>
        public static readonly KrApproveTypeInfo KrApprove = new KrApproveTypeInfo();

        /// <summary>
        ///     Card type caption "KrAuthorSettings" for "KrAuthorSettings".
        /// </summary>
        public static readonly KrAuthorSettingsTypeInfo KrAuthorSettings = new KrAuthorSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrCard" for "KrCard".
        /// </summary>
        public static readonly KrCardTypeInfo KrCard = new KrCardTypeInfo();

        /// <summary>
        ///     Card type caption "$KrActions_ChangeState" for "KrChangeStateAction".
        /// </summary>
        public static readonly KrChangeStateActionTypeInfo KrChangeStateAction = new KrChangeStateActionTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_ChangeState" for "KrChangeStateStageTypeSettings".
        /// </summary>
        public static readonly KrChangeStateStageTypeSettingsTypeInfo KrChangeStateStageTypeSettings = new KrChangeStateStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "KrCheckStateWorkflowTileExtension".
        /// </summary>
        public static readonly KrCheckStateWorkflowTileExtensionTypeInfo KrCheckStateWorkflowTileExtension = new KrCheckStateWorkflowTileExtensionTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_CreateCard" for "KrCreateCardStageTypeSettings".
        /// </summary>
        public static readonly KrCreateCardStageTypeSettingsTypeInfo KrCreateCardStageTypeSettings = new KrCreateCardStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$KrActions_Deregistration" for "KrDeregistrationAction".
        /// </summary>
        public static readonly KrDeregistrationActionTypeInfo KrDeregistrationAction = new KrDeregistrationActionTypeInfo();

        /// <summary>
        ///     Card type caption "KrDialogStageTypeSettings" for "KrDialogStageTypeSettings".
        /// </summary>
        public static readonly KrDialogStageTypeSettingsTypeInfo KrDialogStageTypeSettings = new KrDialogStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrDocState" for "KrDocState".
        /// </summary>
        public static readonly KrDocStateTypeInfo KrDocState = new KrDocStateTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrDocType" for "KrDocType".
        /// </summary>
        public static readonly KrDocTypeTypeInfo KrDocType = new KrDocTypeTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrEdit" for "KrEdit".
        /// </summary>
        public static readonly KrEditTypeInfo KrEdit = new KrEditTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrEditInterject" for "KrEditInterject".
        /// </summary>
        public static readonly KrEditInterjectTypeInfo KrEditInterject = new KrEditInterjectTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_Edit" for "KrEditStageTypeSettings".
        /// </summary>
        public static readonly KrEditStageTypeSettingsTypeInfo KrEditStageTypeSettings = new KrEditStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "KrExampleDialogSatellite" for "KrExampleDialogSatellite".
        /// </summary>
        public static readonly KrExampleDialogSatelliteTypeInfo KrExampleDialogSatellite = new KrExampleDialogSatelliteTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_ForkManagement" for "KrForkManagementStageTypeSettings".
        /// </summary>
        public static readonly KrForkManagementStageTypeSettingsTypeInfo KrForkManagementStageTypeSettings = new KrForkManagementStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_Fork" for "KrForkStageTypeSettings".
        /// </summary>
        public static readonly KrForkStageTypeSettingsTypeInfo KrForkStageTypeSettings = new KrForkStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_HistoryManagement" for "KrHistoryManagementStageTypeSettings".
        /// </summary>
        public static readonly KrHistoryManagementStageTypeSettingsTypeInfo KrHistoryManagementStageTypeSettings = new KrHistoryManagementStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrInfoForInitiator" for "KrInfoForInitiator".
        /// </summary>
        public static readonly KrInfoForInitiatorTypeInfo KrInfoForInitiator = new KrInfoForInitiatorTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_Notification" for "KrNotificationStageTypeSettings".
        /// </summary>
        public static readonly KrNotificationStageTypeSettingsTypeInfo KrNotificationStageTypeSettings = new KrNotificationStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "KrPerformersSettings" for "KrPerformersSettings".
        /// </summary>
        public static readonly KrPerformersSettingsTypeInfo KrPerformersSettings = new KrPerformersSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrPermissions" for "KrPermissions".
        /// </summary>
        public static readonly KrPermissionsTypeInfo KrPermissions = new KrPermissionsTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_ProcessManagement" for "KrProcessManagementStageTypeSettings".
        /// </summary>
        public static readonly KrProcessManagementStageTypeSettingsTypeInfo KrProcessManagementStageTypeSettings = new KrProcessManagementStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrRegistration" for "KrRegistration".
        /// </summary>
        public static readonly KrRegistrationTypeInfo KrRegistration = new KrRegistrationTypeInfo();

        /// <summary>
        ///     Card type caption "$KrActions_Registration" for "KrRegistrationAction".
        /// </summary>
        public static readonly KrRegistrationActionTypeInfo KrRegistrationAction = new KrRegistrationActionTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "KrRegistrationStageTypeSettings".
        /// </summary>
        public static readonly KrRegistrationStageTypeSettingsTypeInfo KrRegistrationStageTypeSettings = new KrRegistrationStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrRequestComment" for "KrRequestComment".
        /// </summary>
        public static readonly KrRequestCommentTypeInfo KrRequestComment = new KrRequestCommentTypeInfo();

        /// <summary>
        ///     Card type caption "$KrActions_Resolution" for "KrResolutionAction".
        /// </summary>
        public static readonly KrResolutionActionTypeInfo KrResolutionAction = new KrResolutionActionTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_Resolution" for "KrResolutionStageTypeSettings".
        /// </summary>
        public static readonly KrResolutionStageTypeSettingsTypeInfo KrResolutionStageTypeSettings = new KrResolutionStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$KrActions_RouteInitialization" for "KrRouteInitializationAction".
        /// </summary>
        public static readonly KrRouteInitializationActionTypeInfo KrRouteInitializationAction = new KrRouteInitializationActionTypeInfo();

        /// <summary>
        ///     Card type caption "An example of card type used to extend access rules in standard solution" for "KrSamplePermissionsExtensionType".
        /// </summary>
        public static readonly KrSamplePermissionsExtensionTypeTypeInfo KrSamplePermissionsExtensionType = new KrSamplePermissionsExtensionTypeTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrSatellite" for "KrSatellite".
        /// </summary>
        public static readonly KrSatelliteTypeInfo KrSatellite = new KrSatelliteTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrSecondaryProcess" for "KrSecondaryProcess".
        /// </summary>
        public static readonly KrSecondaryProcessTypeInfo KrSecondaryProcess = new KrSecondaryProcessTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrSecondarySatellite" for "KrSecondarySatellite".
        /// </summary>
        public static readonly KrSecondarySatelliteTypeInfo KrSecondarySatellite = new KrSecondarySatelliteTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrSettings" for "KrSettings".
        /// </summary>
        public static readonly KrSettingsTypeInfo KrSettings = new KrSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ShowDialog" for "KrShowDialog".
        /// </summary>
        public static readonly KrShowDialogTypeInfo KrShowDialog = new KrShowDialogTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrSigning" for "KrSigning".
        /// </summary>
        public static readonly KrSigningTypeInfo KrSigning = new KrSigningTypeInfo();

        /// <summary>
        ///     Card type caption "$KrActions_Signing" for "KrSigningAction".
        /// </summary>
        public static readonly KrSigningActionTypeInfo KrSigningAction = new KrSigningActionTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_Signing" for "KrSigningStageTypeSettings".
        /// </summary>
        public static readonly KrSigningStageTypeSettingsTypeInfo KrSigningStageTypeSettings = new KrSigningStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrCommonMethod" for "KrStageCommonMethod".
        /// </summary>
        public static readonly KrStageCommonMethodTypeInfo KrStageCommonMethod = new KrStageCommonMethodTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrStageGroup" for "KrStageGroup".
        /// </summary>
        public static readonly KrStageGroupTypeInfo KrStageGroup = new KrStageGroupTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrStageTemplate" for "KrStageTemplate".
        /// </summary>
        public static readonly KrStageTemplateTypeInfo KrStageTemplate = new KrStageTemplateTypeInfo();

        /// <summary>
        ///     Card type caption "KrStateExtension" for "KrStateExtension".
        /// </summary>
        public static readonly KrStateExtensionTypeInfo KrStateExtension = new KrStateExtensionTypeInfo();

        /// <summary>
        ///     Card type caption "KrTaskKindSettings" for "KrTaskKindSettings".
        /// </summary>
        public static readonly KrTaskKindSettingsTypeInfo KrTaskKindSettings = new KrTaskKindSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$KrActions_TaskRegistration" for "KrTaskRegistrationAction".
        /// </summary>
        public static readonly KrTaskRegistrationActionTypeInfo KrTaskRegistrationAction = new KrTaskRegistrationActionTypeInfo();

        /// <summary>
        ///     Card type caption "KrTemplateCard" for "KrTemplateCard".
        /// </summary>
        public static readonly KrTemplateCardTypeInfo KrTemplateCard = new KrTemplateCardTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_TypedTask" for "KrTypedTaskStageTypeSettings".
        /// </summary>
        public static readonly KrTypedTaskStageTypeSettingsTypeInfo KrTypedTaskStageTypeSettings = new KrTypedTaskStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrUniversalTask" for "KrUniversalTask".
        /// </summary>
        public static readonly KrUniversalTaskTypeInfo KrUniversalTask = new KrUniversalTaskTypeInfo();

        /// <summary>
        ///     Card type caption "$KrActions_UniversalTask" for "KrUniversalTaskAction".
        /// </summary>
        public static readonly KrUniversalTaskActionTypeInfo KrUniversalTaskAction = new KrUniversalTaskActionTypeInfo();

        /// <summary>
        ///     Card type caption "$KrStages_UniversalTask" for "KrUniversalTaskStageTypeSettings".
        /// </summary>
        public static readonly KrUniversalTaskStageTypeSettingsTypeInfo KrUniversalTaskStageTypeSettings = new KrUniversalTaskStageTypeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrUserSettings" for "KrUserSettings".
        /// </summary>
        public static readonly KrUserSettingsTypeInfo KrUserSettings = new KrUserSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_KrVirtualFile" for "KrVirtualFile".
        /// </summary>
        public static readonly KrVirtualFileTypeInfo KrVirtualFile = new KrVirtualFileTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_Case" for "LawCase".
        /// </summary>
        public static readonly LawCaseTypeInfo LawCase = new LawCaseTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "LawFile".
        /// </summary>
        public static readonly LawFileTypeInfo LawFile = new LawFileTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "LawPartnerSelectionDialog".
        /// </summary>
        public static readonly LawPartnerSelectionDialogTypeInfo LawPartnerSelectionDialog = new LawPartnerSelectionDialogTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_License" for "License".
        /// </summary>
        public static readonly LicenseTypeInfo License = new LicenseTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "LocalizationDialogs".
        /// </summary>
        public static readonly LocalizationDialogsTypeInfo LocalizationDialogs = new LocalizationDialogsTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "MetadataEditor".
        /// </summary>
        public static readonly MetadataEditorTypeInfo MetadataEditor = new MetadataEditorTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Metarole" for "Metarole".
        /// </summary>
        public static readonly MetaroleTypeInfo Metarole = new MetaroleTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_NestedRole" for "NestedRole".
        /// </summary>
        public static readonly NestedRoleTypeInfo NestedRole = new NestedRoleTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Notification" for "Notification".
        /// </summary>
        public static readonly NotificationTypeInfo Notification = new NotificationTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_NotificationSubscriptions" for "NotificationSubscriptions".
        /// </summary>
        public static readonly NotificationSubscriptionsTypeInfo NotificationSubscriptions = new NotificationSubscriptionsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_NotificationType" for "NotificationType".
        /// </summary>
        public static readonly NotificationTypeTypeInfo NotificationType = new NotificationTypeTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "NotificationUserSettings".
        /// </summary>
        public static readonly NotificationUserSettingsTypeInfo NotificationUserSettings = new NotificationUserSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_OcrOperation" for "OcrOperation".
        /// </summary>
        public static readonly OcrOperationTypeInfo OcrOperation = new OcrOperationTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_OcrRequestDialog" for "OcrRequestDialog".
        /// </summary>
        public static readonly OcrRequestDialogTypeInfo OcrRequestDialog = new OcrRequestDialogTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_OcrSettings" for "OcrSettings".
        /// </summary>
        public static readonly OcrSettingsTypeInfo OcrSettings = new OcrSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_OnlyOffice" for "OnlyOfficeSettings".
        /// </summary>
        public static readonly OnlyOfficeSettingsTypeInfo OnlyOfficeSettings = new OnlyOfficeSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "OpenInModalDialogSettings".
        /// </summary>
        public static readonly OpenInModalDialogSettingsTypeInfo OpenInModalDialogSettings = new OpenInModalDialogSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Operation" for "Operation".
        /// </summary>
        public static readonly OperationTypeInfo Operation = new OperationTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Outgoing" for "Outgoing".
        /// </summary>
        public static readonly OutgoingTypeInfo Outgoing = new OutgoingTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Partner" for "Partner".
        /// </summary>
        public static readonly PartnerTypeInfo Partner = new PartnerTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_PartnerCondition" for "PartnerCondition".
        /// </summary>
        public static readonly PartnerConditionTypeInfo PartnerCondition = new PartnerConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_Tabs_Personalization" for "PersonalizationUserSettings".
        /// </summary>
        public static readonly PersonalizationUserSettingsTypeInfo PersonalizationUserSettings = new PersonalizationUserSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_PersonalRole" for "PersonalRole".
        /// </summary>
        public static readonly PersonalRoleTypeInfo PersonalRole = new PersonalRoleTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_PersonalRoleSatellite" for "PersonalRoleSatellite".
        /// </summary>
        public static readonly PersonalRoleSatelliteTypeInfo PersonalRoleSatellite = new PersonalRoleSatelliteTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Protocol" for "Protocol".
        /// </summary>
        public static readonly ProtocolTypeInfo Protocol = new ProtocolTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ReportPermissions" for "ReportPermissions".
        /// </summary>
        public static readonly ReportPermissionsTypeInfo ReportPermissions = new ReportPermissionsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_RoleDeputiesManagement" for "RoleDeputiesManagement".
        /// </summary>
        public static readonly RoleDeputiesManagementTypeInfo RoleDeputiesManagement = new RoleDeputiesManagementTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_RoleGenerator" for "RoleGenerator".
        /// </summary>
        public static readonly RoleGeneratorTypeInfo RoleGenerator = new RoleGeneratorTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_RouteCondition" for "RouteCondition".
        /// </summary>
        public static readonly RouteConditionTypeInfo RouteCondition = new RouteConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_RowAddedCondition" for "RowAddedCondition".
        /// </summary>
        public static readonly RowAddedConditionTypeInfo RowAddedCondition = new RowAddedConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_RowDeletedCondition" for "RowDeletedCondition".
        /// </summary>
        public static readonly RowDeletedConditionTypeInfo RowDeletedCondition = new RowDeletedConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "SelectTag".
        /// </summary>
        public static readonly SelectTagTypeInfo SelectTag = new SelectTagTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Sequence" for "Sequence".
        /// </summary>
        public static readonly SequenceTypeInfo Sequence = new SequenceTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ServerInstance" for "ServerInstance".
        /// </summary>
        public static readonly ServerInstanceTypeInfo ServerInstance = new ServerInstanceTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ShowDialog" for "ShowDialog".
        /// </summary>
        public static readonly ShowDialogTypeInfo ShowDialog = new ShowDialogTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_SignatureSetting" for "SignatureSettings".
        /// </summary>
        public static readonly SignatureSettingsTypeInfo SignatureSettings = new SignatureSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_SmartRole" for "SmartRole".
        /// </summary>
        public static readonly SmartRoleTypeInfo SmartRole = new SmartRoleTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_SmartRoleGenerator" for "SmartRoleGenerator".
        /// </summary>
        public static readonly SmartRoleGeneratorTypeInfo SmartRoleGenerator = new SmartRoleGeneratorTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_StaticRole" for "StaticRole".
        /// </summary>
        public static readonly StaticRoleTypeInfo StaticRole = new StaticRoleTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Tag" for "Tag".
        /// </summary>
        public static readonly TagTypeInfo Tag = new TagTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TagCondition" for "TagCondition".
        /// </summary>
        public static readonly TagConditionTypeInfo TagCondition = new TagConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_CardNames_TagsSettings" for "TagsUserSettings".
        /// </summary>
        public static readonly TagsUserSettingsTypeInfo TagsUserSettings = new TagsUserSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_NewTaskAssignedRoleDialog" for "TaskAssignedRoleEditor".
        /// </summary>
        public static readonly TaskAssignedRoleEditorTypeInfo TaskAssignedRoleEditor = new TaskAssignedRoleEditorTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TaskAssignedRolesDialog" for "TaskAssignedRoles".
        /// </summary>
        public static readonly TaskAssignedRolesTypeInfo TaskAssignedRoles = new TaskAssignedRolesTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TaskChangedCondition" for "TaskChangedCondition".
        /// </summary>
        public static readonly TaskChangedConditionTypeInfo TaskChangedCondition = new TaskChangedConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TaskHistoryGroupType" for "TaskHistoryGroupType".
        /// </summary>
        public static readonly TaskHistoryGroupTypeTypeInfo TaskHistoryGroupType = new TaskHistoryGroupTypeTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TaskKind" for "TaskKind".
        /// </summary>
        public static readonly TaskKindTypeInfo TaskKind = new TaskKindTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TaskRole" for "TaskRole".
        /// </summary>
        public static readonly TaskRoleTypeInfo TaskRole = new TaskRoleTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TaskTypeCondition" for "TaskTypeCondition".
        /// </summary>
        public static readonly TaskTypeConditionTypeInfo TaskTypeCondition = new TaskTypeConditionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Blocks_Tabs_Template" for "Template".
        /// </summary>
        public static readonly TemplateTypeInfo Template = new TemplateTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TemplateFile" for "TemplateFile".
        /// </summary>
        public static readonly TemplateFileTypeInfo TemplateFile = new TemplateFileTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TestTask1" for "TestTask1".
        /// </summary>
        public static readonly TestTask1TypeInfo TestTask1 = new TestTask1TypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TestTask2" for "TestTask2".
        /// </summary>
        public static readonly TestTask2TypeInfo TestTask2 = new TestTask2TypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_Blocks_TilePanels" for "TileUserSettings".
        /// </summary>
        public static readonly TileUserSettingsTypeInfo TileUserSettings = new TileUserSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Tabs_TimeZones" for "TimeZones".
        /// </summary>
        public static readonly TimeZonesTypeInfo TimeZones = new TimeZonesTypeInfo();

        /// <summary>
        ///     Card type caption "TopicDialogs" for "TopicDialogs".
        /// </summary>
        public static readonly TopicDialogsTypeInfo TopicDialogs = new TopicDialogsTypeInfo();

        /// <summary>
        ///     Card type caption "TopicTabs" for "TopicTabs".
        /// </summary>
        public static readonly TopicTabsTypeInfo TopicTabs = new TopicTabsTypeInfo();

        /// <summary>
        ///     Card type caption "Used by the system. Do not modify it." for "UserSettingsSystemType".
        /// </summary>
        public static readonly UserSettingsSystemTypeTypeInfo UserSettingsSystemType = new UserSettingsSystemTypeTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_View" for "View".
        /// </summary>
        public static readonly ViewTypeInfo View = new ViewTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "ViewExtensions".
        /// </summary>
        public static readonly ViewExtensionsTypeInfo ViewExtensions = new ViewExtensionsTypeInfo();

        /// <summary>
        ///     Card type caption "$Cards_DefaultCaption" for "VirtualScheme".
        /// </summary>
        public static readonly VirtualSchemeTypeInfo VirtualScheme = new VirtualSchemeTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_WebApplication" for "WebApplication".
        /// </summary>
        public static readonly WebApplicationTypeInfo WebApplication = new WebApplicationTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_Blocks_WebClientUserSettings" for "WebClientUserSettings".
        /// </summary>
        public static readonly WebClientUserSettingsTypeInfo WebClientUserSettings = new WebClientUserSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_WfResolution" for "WfResolution".
        /// </summary>
        public static readonly WfResolutionTypeInfo WfResolution = new WfResolutionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_WfResolutionChild" for "WfResolutionChild".
        /// </summary>
        public static readonly WfResolutionChildTypeInfo WfResolutionChild = new WfResolutionChildTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_WfResolutionControl" for "WfResolutionControl".
        /// </summary>
        public static readonly WfResolutionControlTypeInfo WfResolutionControl = new WfResolutionControlTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_WfResolutionProject" for "WfResolutionProject".
        /// </summary>
        public static readonly WfResolutionProjectTypeInfo WfResolutionProject = new WfResolutionProjectTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_WfSatellite" for "WfSatellite".
        /// </summary>
        public static readonly WfSatelliteTypeInfo WfSatellite = new WfSatelliteTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_WfTaskCard" for "WfTaskCard".
        /// </summary>
        public static readonly WfTaskCardTypeInfo WfTaskCard = new WfTaskCardTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ActionTemplate" for "WorkflowActionEditor".
        /// </summary>
        public static readonly WorkflowActionEditorTypeInfo WorkflowActionEditor = new WorkflowActionEditorTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ActionInstance" for "WorkflowActionInstanceEditor".
        /// </summary>
        public static readonly WorkflowActionInstanceEditorTypeInfo WorkflowActionInstanceEditor = new WorkflowActionInstanceEditorTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_AddFileFromTemplate" for "WorkflowAddFileFromTemplateAction".
        /// </summary>
        public static readonly WorkflowAddFileFromTemplateActionTypeInfo WorkflowAddFileFromTemplateAction = new WorkflowAddFileFromTemplateActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_And" for "WorkflowAndAction".
        /// </summary>
        public static readonly WorkflowAndActionTypeInfo WorkflowAndAction = new WorkflowAndActionTypeInfo();

        /// <summary>
        ///     Card type caption "$WorkflowEngine_TilePermission_CheckRolesForExecution" for "WorkflowCheckRolesForExecutionTileExtension".
        /// </summary>
        public static readonly WorkflowCheckRolesForExecutionTileExtensionTypeInfo WorkflowCheckRolesForExecutionTileExtension = new WorkflowCheckRolesForExecutionTileExtensionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_WaitingForSignal" for "WorkflowCommandAction".
        /// </summary>
        public static readonly WorkflowCommandActionTypeInfo WorkflowCommandAction = new WorkflowCommandActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Condition" for "WorkflowConditionAction".
        /// </summary>
        public static readonly WorkflowConditionActionTypeInfo WorkflowConditionAction = new WorkflowConditionActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_CreateCard" for "WorkflowCreateCardAction".
        /// </summary>
        public static readonly WorkflowCreateCardActionTypeInfo WorkflowCreateCardAction = new WorkflowCreateCardActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Dialog" for "WorkflowDialogAction".
        /// </summary>
        public static readonly WorkflowDialogActionTypeInfo WorkflowDialogAction = new WorkflowDialogActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_EndProcess" for "WorkflowEndAction".
        /// </summary>
        public static readonly WorkflowEndActionTypeInfo WorkflowEndAction = new WorkflowEndActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_WorkflowEngineSettings" for "WorkflowEngineSettings".
        /// </summary>
        public static readonly WorkflowEngineSettingsTypeInfo WorkflowEngineSettings = new WorkflowEngineSettingsTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_HistoryManagementAction" for "WorkflowHistoryManagementAction".
        /// </summary>
        public static readonly WorkflowHistoryManagementActionTypeInfo WorkflowHistoryManagementAction = new WorkflowHistoryManagementActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Link" for "WorkflowLinkEditor".
        /// </summary>
        public static readonly WorkflowLinkEditorTypeInfo WorkflowLinkEditor = new WorkflowLinkEditorTypeInfo();

        /// <summary>
        ///     Card type caption "Редатор элементов на левой панели (пока не используется)" for "WorkflowNewItemEditor".
        /// </summary>
        public static readonly WorkflowNewItemEditorTypeInfo WorkflowNewItemEditor = new WorkflowNewItemEditorTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_NodeTemplate" for "WorkflowNodeEditor".
        /// </summary>
        public static readonly WorkflowNodeEditorTypeInfo WorkflowNodeEditor = new WorkflowNodeEditorTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_NodeInstance" for "WorkflowNodeInstanceEditor".
        /// </summary>
        public static readonly WorkflowNodeInstanceEditorTypeInfo WorkflowNodeInstanceEditor = new WorkflowNodeInstanceEditorTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_NotificationAction" for "WorkflowNotificationAction".
        /// </summary>
        public static readonly WorkflowNotificationActionTypeInfo WorkflowNotificationAction = new WorkflowNotificationActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ProcessInstance" for "WorkflowProcess".
        /// </summary>
        public static readonly WorkflowProcessTypeInfo WorkflowProcess = new WorkflowProcessTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ProcessTemplate" for "WorkflowProcessEditor".
        /// </summary>
        public static readonly WorkflowProcessEditorTypeInfo WorkflowProcessEditor = new WorkflowProcessEditorTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_ProcessInstance" for "WorkflowProcessInstanceEditor".
        /// </summary>
        public static readonly WorkflowProcessInstanceEditorTypeInfo WorkflowProcessInstanceEditor = new WorkflowProcessInstanceEditorTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Scenario" for "WorkflowScenarioAction".
        /// </summary>
        public static readonly WorkflowScenarioActionTypeInfo WorkflowScenarioAction = new WorkflowScenarioActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_SendSignal" for "WorkflowSendSignalAction".
        /// </summary>
        public static readonly WorkflowSendSignalActionTypeInfo WorkflowSendSignalAction = new WorkflowSendSignalActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_StartProcess" for "WorkflowStartAction".
        /// </summary>
        public static readonly WorkflowStartActionTypeInfo WorkflowStartAction = new WorkflowStartActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Subprocess" for "WorkflowSubprocessAction".
        /// </summary>
        public static readonly WorkflowSubprocessActionTypeInfo WorkflowSubprocessAction = new WorkflowSubprocessActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_SubprocessControl" for "WorkflowSubprocessControlAction".
        /// </summary>
        public static readonly WorkflowSubprocessControlActionTypeInfo WorkflowSubprocessControlAction = new WorkflowSubprocessControlActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Task" for "WorkflowTaskAction".
        /// </summary>
        public static readonly WorkflowTaskActionTypeInfo WorkflowTaskAction = new WorkflowTaskActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TaskControl" for "WorkflowTaskControlAction".
        /// </summary>
        public static readonly WorkflowTaskControlActionTypeInfo WorkflowTaskControlAction = new WorkflowTaskControlActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TaskGroup" for "WorkflowTaskGroupAction".
        /// </summary>
        public static readonly WorkflowTaskGroupActionTypeInfo WorkflowTaskGroupAction = new WorkflowTaskGroupActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TaskGroupControl" for "WorkflowTaskGroupControlAction".
        /// </summary>
        public static readonly WorkflowTaskGroupControlActionTypeInfo WorkflowTaskGroupControlAction = new WorkflowTaskGroupControlActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Timer" for "WorkflowTimerAction".
        /// </summary>
        public static readonly WorkflowTimerActionTypeInfo WorkflowTimerAction = new WorkflowTimerActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_TimerControl" for "WorkflowTimerControlAction".
        /// </summary>
        public static readonly WorkflowTimerControlActionTypeInfo WorkflowTimerControlAction = new WorkflowTimerControlActionTypeInfo();

        /// <summary>
        ///     Card type caption "$CardTypes_TypesNames_Workplace" for "Workplace".
        /// </summary>
        public static readonly WorkplaceTypeInfo Workplace = new WorkplaceTypeInfo();

        #endregion
    }
}