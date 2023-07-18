using System;

namespace Tessa.Extensions.Shared.Info
{// ReSharper disable InconsistentNaming
    #region AccessLevels

    /// <summary>
    ///     ID: {648381d6-8647-4ec6-87a4-3cbd6bae380c}
    ///     Alias: AccessLevels
    ///     Group: System
    ///     Description: Уровни доступа пользователей.
    /// </summary>
    public sealed class AccessLevelsSchemeInfo
    {
        private const string name = "AccessLevels";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Regular
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_AccessLevels_Regular";
            }
            public static class Administrator
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_AccessLevels_Administrator";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(AccessLevelsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region AccessLevels Enumeration

    public sealed class AccessLevels
    {
        public readonly int ID;
        public readonly string Name;

        public AccessLevels(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region Acl

    /// <summary>
    ///     ID: {b7538557-04c6-43d4-9d7a-4412dc1ed103}
    ///     Alias: Acl
    ///     Group: Acl
    ///     Description: Основная таблица со списком ролей доступа к карточке.
    /// </summary>
    public sealed class AclSchemeInfo
    {
        private const string name = "Acl";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RuleID = nameof(RuleID);
        public readonly string RoleID = nameof(RoleID);

        #endregion

        #region ToString

        public static implicit operator string(AclSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region AclGenerationInfo

    /// <summary>
    ///     ID: {5cca17a2-50e3-4b20-98c2-2f6ed9ce31fa}
    ///     Alias: AclGenerationInfo
    ///     Group: Acl
    ///     Description: Таблица с информацией о генерации ACL.
    /// </summary>
    public sealed class AclGenerationInfoSchemeInfo
    {
        private const string name = "AclGenerationInfo";

        #region Columns

        public readonly string RuleID = nameof(RuleID);
        public readonly string RuleVersion = nameof(RuleVersion);
        public readonly string NextRequest = nameof(NextRequest);
        public readonly string NextRequestVersion = nameof(NextRequestVersion);

        #endregion

        #region ToString

        public static implicit operator string(AclGenerationInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region AclGenerationRuleExtensions

    /// <summary>
    ///     ID: {c2c3b955-ca13-4e63-83aa-cb033ebdce57}
    ///     Alias: AclGenerationRuleExtensions
    ///     Group: Acl
    /// </summary>
    public sealed class AclGenerationRuleExtensionsSchemeInfo
    {
        private const string name = "AclGenerationRuleExtensions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ExtensionID = nameof(ExtensionID);
        public readonly string ExtensionName = nameof(ExtensionName);

        #endregion

        #region ToString

        public static implicit operator string(AclGenerationRuleExtensionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region AclGenerationRules

    /// <summary>
    ///     ID: {5518f35a-ea30-4968-983d-aec524aeb710}
    ///     Alias: AclGenerationRules
    ///     Group: Acl
    ///     Description: Основная таблица для правил расчета ACL.
    /// </summary>
    public sealed class AclGenerationRulesSchemeInfo
    {
        private const string name = "AclGenerationRules";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Version = nameof(Version);
        public readonly string RolesSelectorSql = nameof(RolesSelectorSql);
        public readonly string DefaultUpdateAclCardSelectorSql = nameof(DefaultUpdateAclCardSelectorSql);
        public readonly string UseSmartRoles = nameof(UseSmartRoles);
        public readonly string CardOwnerSelectorSql = nameof(CardOwnerSelectorSql);
        public readonly string SmartRoleGeneratorID = nameof(SmartRoleGeneratorID);
        public readonly string SmartRoleGeneratorName = nameof(SmartRoleGeneratorName);
        public readonly string Diescription = nameof(Diescription);
        public readonly string IsDisabled = nameof(IsDisabled);
        public readonly string EnableErrorLogging = nameof(EnableErrorLogging);
        public readonly string CardsByOwnerSelectorSql = nameof(CardsByOwnerSelectorSql);
        public readonly string ExtensionsData = nameof(ExtensionsData);

        #endregion

        #region ToString

        public static implicit operator string(AclGenerationRulesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region AclGenerationRuleTriggerModes

    /// <summary>
    ///     ID: {b55966a9-f474-47c9-b025-8e3408208646}
    ///     Alias: AclGenerationRuleTriggerModes
    ///     Group: Acl
    /// </summary>
    public sealed class AclGenerationRuleTriggerModesSchemeInfo
    {
        private const string name = "AclGenerationRuleTriggerModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class AnyChanges
            {
                    public const int ID = 0;
                    public const string Name = "AnyChanges";
            }
            public static class FieldChanged
            {
                    public const int ID = 1;
                    public const string Name = "FieldChanged";
            }
            public static class RowAdded
            {
                    public const int ID = 2;
                    public const string Name = "RowAdded";
            }
            public static class RowDeleted
            {
                    public const int ID = 3;
                    public const string Name = "RowDeleted";
            }
            public static class CardCreated
            {
                    public const int ID = 4;
                    public const string Name = "CardCreated";
            }
            public static class CardDeleted
            {
                    public const int ID = 5;
                    public const string Name = "CardDeleted";
            }
            public static class TaskCreated
            {
                    public const int ID = 6;
                    public const string Name = "TaskCreated";
            }
            public static class TaskCompleted
            {
                    public const int ID = 7;
                    public const string Name = "TaskCompleted";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(AclGenerationRuleTriggerModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region AclGenerationRuleTriggerModes Enumeration

    public sealed class AclGenerationRuleTriggerModes
    {
        public readonly int ID;
        public readonly string Name;

        public AclGenerationRuleTriggerModes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region AclGenerationRuleTriggers

    /// <summary>
    ///     ID: {24e6a4b4-7e51-4429-8bb7-648a840e026b}
    ///     Alias: AclGenerationRuleTriggers
    ///     Group: Acl
    ///     Description: Триггеры правила генерации Acl.
    /// </summary>
    public sealed class AclGenerationRuleTriggersSchemeInfo
    {
        private const string name = "AclGenerationRuleTriggers";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string UpdateAclCardSelectorSql = nameof(UpdateAclCardSelectorSql);
        public readonly string OnlySelfUpdate = nameof(OnlySelfUpdate);
        public readonly string Order = nameof(Order);
        public readonly string Conditions = nameof(Conditions);
        public readonly string Name = nameof(Name);
        public readonly string UpdateAsync = nameof(UpdateAsync);
        public readonly string ConditionsAndMode = nameof(ConditionsAndMode);
        public readonly string UseRuleCardTypes = nameof(UseRuleCardTypes);

        #endregion

        #region ToString

        public static implicit operator string(AclGenerationRuleTriggersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region AclGenerationRuleTriggerTypes

    /// <summary>
    ///     ID: {59827979-5949-4bb2-896d-dc8b5a238a32}
    ///     Alias: AclGenerationRuleTriggerTypes
    ///     Group: Acl
    ///     Description: Типы карточек, при изменении которых проверяется триггер.
    /// </summary>
    public sealed class AclGenerationRuleTriggerTypesSchemeInfo
    {
        private const string name = "AclGenerationRuleTriggerTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TriggerRowID = nameof(TriggerRowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(AclGenerationRuleTriggerTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region AclGenerationRuleTypes

    /// <summary>
    ///     ID: {930de8d2-2496-4523-9ea2-800d229fd808}
    ///     Alias: AclGenerationRuleTypes
    ///     Group: Acl
    ///     Description: Типы карточек для правила Acl.
    /// </summary>
    public sealed class AclGenerationRuleTypesSchemeInfo
    {
        private const string name = "AclGenerationRuleTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(AclGenerationRuleTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region AcquaintanceComments

    /// <summary>
    ///     ID: {ae4e68f0-ff8e-4055-9386-f601f1f3c664}
    ///     Alias: AcquaintanceComments
    ///     Group: Common
    ///     Description: Строки по данным для комментариев, отправленных на массовое ознакомление. По одной строке для каждой отправки на ознакомление с непустым комментарием, при этом в отправке может быть указано несколько ролей, в каждой из которых несколько сотрудников.
    /// </summary>
    public sealed class AcquaintanceCommentsSchemeInfo
    {
        private const string name = "AcquaintanceComments";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Comment = nameof(Comment);

        #endregion

        #region ToString

        public static implicit operator string(AcquaintanceCommentsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region AcquaintanceRows

    /// <summary>
    ///     ID: {8874a392-0fd9-47dd-a6b5-bc3c02ede681}
    ///     Alias: AcquaintanceRows
    ///     Group: Common
    ///     Description: Строки по данным для отправки на массовое ознакомление. По одной строке для каждого сотрудника, которому была отправлена карточка на ознакомление.
    /// </summary>
    public sealed class AcquaintanceRowsSchemeInfo
    {
        private const string name = "AcquaintanceRows";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CardID = nameof(CardID);
        public readonly string SenderID = nameof(SenderID);
        public readonly string SenderName = nameof(SenderName);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string IsReceived = nameof(IsReceived);
        public readonly string Sent = nameof(Sent);
        public readonly string Received = nameof(Received);
        public readonly string CommentID = nameof(CommentID);

        #endregion

        #region ToString

        public static implicit operator string(AcquaintanceRowsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ActionHistory

    /// <summary>
    ///     ID: {5089ca1c-27af-46e4-a2c2-af01bfd42e81}
    ///     Alias: ActionHistory
    ///     Group: System
    /// </summary>
    public sealed class ActionHistorySchemeInfo
    {
        private const string name = "ActionHistory";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ActionID = nameof(ActionID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string Digest = nameof(Digest);
        public readonly string Request = nameof(Request);
        public readonly string Modified = nameof(Modified);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);
        public readonly string SessionID = nameof(SessionID);
        public readonly string ApplicationID = nameof(ApplicationID);

        #endregion

        #region ToString

        public static implicit operator string(ActionHistorySchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ActionHistoryDatabases

    /// <summary>
    ///     ID: {db0969a9-e71d-405d-bf86-15f263cf69c8}
    ///     Alias: ActionHistoryDatabases
    ///     Group: System
    ///     Description: Базы данных для хранения истории действий.
    /// </summary>
    public sealed class ActionHistoryDatabasesSchemeInfo
    {
        private const string name = "ActionHistoryDatabases";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Description = nameof(Description);
        public readonly string ConfigurationString = nameof(ConfigurationString);

        #endregion

        #region ToString

        public static implicit operator string(ActionHistoryDatabasesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ActionHistoryDatabasesVirtual

    /// <summary>
    ///     ID: {df1d09a4-5ef2-4f2b-885e-c4ad6df06555}
    ///     Alias: ActionHistoryDatabasesVirtual
    ///     Group: System
    ///     Description: Базы данных для хранения истории действий. Виртуальная таблица, обеспечивающая редактирование таблицы ActionHistoryDatabases через карточку настроек.
    ///                  Колонка ID в этой таблице соответствует идентификатору карточки настроек, а колонка DatabaseID - идентификатору базы данных, т.е. аналог ActionHistoryDatabases.ID.
    /// </summary>
    public sealed class ActionHistoryDatabasesVirtualSchemeInfo
    {
        private const string name = "ActionHistoryDatabasesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string DatabaseID = nameof(DatabaseID);
        public readonly string DatabaseIDText = nameof(DatabaseIDText);
        public readonly string Name = nameof(Name);
        public readonly string Description = nameof(Description);
        public readonly string ConfigurationString = nameof(ConfigurationString);
        public readonly string IsDefault = nameof(IsDefault);

        #endregion

        #region ToString

        public static implicit operator string(ActionHistoryDatabasesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ActionHistoryVirtual

    /// <summary>
    ///     ID: {d1ab792c-2758-4778-a3cf-d91191b3ec52}
    ///     Alias: ActionHistoryVirtual
    ///     Group: System
    ///     Description: История действий с карточкой для отображения в UI.
    /// </summary>
    public sealed class ActionHistoryVirtualSchemeInfo
    {
        private const string name = "ActionHistoryVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ActionID = nameof(ActionID);
        public readonly string ActionName = nameof(ActionName);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string CardID = nameof(CardID);
        public readonly string CardDigest = nameof(CardDigest);
        public readonly string Request = nameof(Request);
        public readonly string RequestJson = nameof(RequestJson);
        public readonly string Description = nameof(Description);
        public readonly string Category = nameof(Category);
        public readonly string Text = nameof(Text);
        public readonly string Modified = nameof(Modified);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);
        public readonly string HasDetailsCard = nameof(HasDetailsCard);
        public readonly string SessionID = nameof(SessionID);

        #endregion

        #region ToString

        public static implicit operator string(ActionHistoryVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ActionTypes

    /// <summary>
    ///     ID: {420a67fd-2ea0-4ccd-9c3f-6378c2fda2cc}
    ///     Alias: ActionTypes
    ///     Group: System
    ///     Description: Типы действий с карточкой.
    /// </summary>
    public sealed class ActionTypesSchemeInfo
    {
        private const string name = "ActionTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Creating
            {
                    public const int ID = 1;
                    public const string Name = "$ActionHistory_Action_Creating";
            }
            public static class Opening
            {
                    public const int ID = 2;
                    public const string Name = "$ActionHistory_Action_Opening";
            }
            public static class Editing
            {
                    public const int ID = 3;
                    public const string Name = "$ActionHistory_Action_Editing";
            }
            public static class Deleting
            {
                    public const int ID = 4;
                    public const string Name = "$ActionHistory_Action_Deleting";
            }
            public static class FileOpening
            {
                    public const int ID = 5;
                    public const string Name = "$ActionHistory_Action_FileOpening";
            }
            public static class Restoring
            {
                    public const int ID = 6;
                    public const string Name = "$ActionHistory_Action_Restoring";
            }
            public static class Export
            {
                    public const int ID = 7;
                    public const string Name = "$ActionHistory_Action_Export";
            }
            public static class Import
            {
                    public const int ID = 8;
                    public const string Name = "$ActionHistory_Action_Import";
            }
            public static class FinalDeleting
            {
                    public const int ID = 9;
                    public const string Name = "$ActionHistory_Action_FinalDeleting";
            }
            public static class Login
            {
                    public const int ID = 10;
                    public const string Name = "$ActionHistory_Action_Login";
            }
            public static class Logout
            {
                    public const int ID = 11;
                    public const string Name = "$ActionHistory_Action_Logout";
            }
            public static class ReserveNumber
            {
                    public const int ID = 12;
                    public const string Name = "$ActionHistory_Action_ReserveNumber";
            }
            public static class AcquireNumber
            {
                    public const int ID = 13;
                    public const string Name = "$ActionHistory_Action_AcquireNumber";
            }
            public static class AcquireReservedNumber
            {
                    public const int ID = 14;
                    public const string Name = "$ActionHistory_Action_AcquireReservedNumber";
            }
            public static class AcquireUnreservedNumber
            {
                    public const int ID = 15;
                    public const string Name = "$ActionHistory_Action_AcquireUnreservedNumber";
            }
            public static class ReleaseNumber
            {
                    public const int ID = 16;
                    public const string Name = "$ActionHistory_Action_ReleaseNumber";
            }
            public static class DereserveNumber
            {
                    public const int ID = 17;
                    public const string Name = "$ActionHistory_Action_DereserveNumber";
            }
            public static class SessionClosedByAdmin
            {
                    public const int ID = 18;
                    public const string Name = "$ActionHistory_Action_SessionClosedByAdmin";
            }
            public static class LoginFailed
            {
                    public const int ID = 19;
                    public const string Name = "$ActionHistory_Action_LoginFailed";
            }
            public static class Error
            {
                    public const int ID = 20;
                    public const string Name = "$ActionHistory_Action_Error";
            }
            public static class AddCardType
            {
                    public const int ID = 21;
                    public const string Name = "$ActionHistory_Action_AddCardType";
            }
            public static class ModifyCardType
            {
                    public const int ID = 22;
                    public const string Name = "$ActionHistory_Action_ModifyCardType";
            }
            public static class DeleteCardType
            {
                    public const int ID = 23;
                    public const string Name = "$ActionHistory_Action_DeleteCardType";
            }
            public static class AddView
            {
                    public const int ID = 24;
                    public const string Name = "$ActionHistory_Action_AddView";
            }
            public static class ModifyView
            {
                    public const int ID = 25;
                    public const string Name = "$ActionHistory_Action_ModifyView";
            }
            public static class DeleteView
            {
                    public const int ID = 26;
                    public const string Name = "$ActionHistory_Action_DeleteView";
            }
            public static class ImportView
            {
                    public const int ID = 27;
                    public const string Name = "$ActionHistory_Action_ImportView";
            }
            public static class AddWorkplace
            {
                    public const int ID = 28;
                    public const string Name = "$ActionHistory_Action_AddWorkplace";
            }
            public static class ModifyWorkplace
            {
                    public const int ID = 29;
                    public const string Name = "$ActionHistory_Action_ModifyWorkplace";
            }
            public static class DeleteWorkplace
            {
                    public const int ID = 30;
                    public const string Name = "$ActionHistory_Action_DeleteWorkplace";
            }
            public static class ImportWorkplace
            {
                    public const int ID = 31;
                    public const string Name = "$ActionHistory_Action_ImportWorkplace";
            }
            public static class ModifyTable
            {
                    public const int ID = 32;
                    public const string Name = "$ActionHistory_Action_ModifyTable";
            }
            public static class DeleteTable
            {
                    public const int ID = 33;
                    public const string Name = "$ActionHistory_Action_DeleteTable";
            }
            public static class ModifyProcedure
            {
                    public const int ID = 34;
                    public const string Name = "$ActionHistory_Action_ModifyProcedure";
            }
            public static class DeleteProcedure
            {
                    public const int ID = 35;
                    public const string Name = "$ActionHistory_Action_DeleteProcedure";
            }
            public static class ModifyFunction
            {
                    public const int ID = 36;
                    public const string Name = "$ActionHistory_Action_ModifyFunction";
            }
            public static class DeleteFunction
            {
                    public const int ID = 37;
                    public const string Name = "$ActionHistory_Action_DeleteFunction";
            }
            public static class ModifyMigration
            {
                    public const int ID = 38;
                    public const string Name = "$ActionHistory_Action_ModifyMigration";
            }
            public static class DeleteMigration
            {
                    public const int ID = 39;
                    public const string Name = "$ActionHistory_Action_DeleteMigration";
            }
            public static class ModifyPartition
            {
                    public const int ID = 40;
                    public const string Name = "$ActionHistory_Action_ModifyPartition";
            }
            public static class DeletePartition
            {
                    public const int ID = 41;
                    public const string Name = "$ActionHistory_Action_DeletePartition";
            }
            public static class ModifyLocalizationLibrary
            {
                    public const int ID = 42;
                    public const string Name = "$ActionHistory_Action_ModifyLocalizationLibrary";
            }
            public static class DeleteLocalizationLibrary
            {
                    public const int ID = 43;
                    public const string Name = "$ActionHistory_Action_DeleteLocalizationLibrary";
            }
            public static class ReserveAcquiredNumber
            {
                    public const int ID = 44;
                    public const string Name = "$ActionHistory_Action_ReserveAcquiredNumber";
            }
            public static class StoreTag
            {
                    public const int ID = 45;
                    public const string Name = "$ActionHistory_Action_StoreTag";
            }
            public static class DeleteTag
            {
                    public const int ID = 46;
                    public const string Name = "$ActionHistory_Action_DeleteTag";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(ActionTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region ActionTypes Enumeration

    public sealed class ActionTypes
    {
        public readonly int ID;
        public readonly string Name;

        public ActionTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region AdSyncRoots

    /// <summary>
    ///     ID: {68543b32-9960-4b90-9c67-72a297f4feff}
    ///     Alias: AdSyncRoots
    ///     Group: System
    /// </summary>
    public sealed class AdSyncRootsSchemeInfo
    {
        private const string name = "AdSyncRoots";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RootName = nameof(RootName);
        public readonly string SyncStaticRoles = nameof(SyncStaticRoles);
        public readonly string SyncDepartments = nameof(SyncDepartments);

        #endregion

        #region ToString

        public static implicit operator string(AdSyncRootsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region AdSyncSettings

    /// <summary>
    ///     ID: {6b7f7b41-7ba8-4549-b965-f3a2aa9a168b}
    ///     Alias: AdSyncSettings
    ///     Group: System
    /// </summary>
    public sealed class AdSyncSettingsSchemeInfo
    {
        private const string name = "AdSyncSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string SyncUsers = nameof(SyncUsers);
        public readonly string SyncDepartments = nameof(SyncDepartments);
        public readonly string SyncUsersGroup = nameof(SyncUsersGroup);
        public readonly string SyncStaticRoles = nameof(SyncStaticRoles);
        public readonly string DisableStaticRoleRename = nameof(DisableStaticRoleRename);

        #endregion

        #region ToString

        public static implicit operator string(AdSyncSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region AdSyncSettingsVirtual

    /// <summary>
    ///     ID: {c993000f-40d8-4639-a25d-e9a25d47e19c}
    ///     Alias: AdSyncSettingsVirtual
    ///     Group: System
    /// </summary>
    public sealed class AdSyncSettingsVirtualSchemeInfo
    {
        private const string name = "AdSyncSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string SyncUsers = nameof(SyncUsers);
        public readonly string SyncDepartments = nameof(SyncDepartments);
        public readonly string SyncStaticRoles = nameof(SyncStaticRoles);

        #endregion

        #region ToString

        public static implicit operator string(AdSyncSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ApplicationArchitectures

    /// <summary>
    ///     ID: {27977834-b755-4a4a-9180-90748e71f361}
    ///     Alias: ApplicationArchitectures
    ///     Group: System
    ///     Description: Архитектура процессора (разрядность) для приложений, запускаемых пользователем. Настройка задаётся администратором в карточке сотрудника.
    /// </summary>
    public sealed class ApplicationArchitecturesSchemeInfo
    {
        private const string name = "ApplicationArchitectures";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Auto
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_ApplicationArchitectures_Auto";
            }
            public static class Enum32bit
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_ApplicationArchitectures_32bit";
            }
            public static class Enum64bit
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_ApplicationArchitectures_64bit";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(ApplicationArchitecturesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region ApplicationArchitectures Enumeration

    public sealed class ApplicationArchitectures
    {
        public readonly int ID;
        public readonly string Name;

        public ApplicationArchitectures(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region ApplicationNames

    /// <summary>
    ///     ID: {b939817b-bc1f-4a9d-87ef-694336870eed}
    ///     Alias: ApplicationNames
    ///     Group: System
    ///     Description: Имена стандартных приложений.
    /// </summary>
    public sealed class ApplicationNamesSchemeInfo
    {
        private const string name = "ApplicationNames";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string IsHidden = nameof(IsHidden);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Other
            {
                    public static Guid ID = Guid.Empty;
                    public const string Name = "$Enum_ApplicationNames_Other";
                    public const bool IsHidden = false;
            }
            public static class TessaClient
            {
                    public static Guid ID = new Guid(0x3bc38194,0xa881,0x4955,0x85,0xe7,0x0c,0x6b,0xe3,0x03,0x1f,0x45);
                    public const string Name = "$Enum_ApplicationNames_TessaClient";
                    public const bool IsHidden = false;
            }
            public static class TessaAdmin
            {
                    public static Guid ID = new Guid(0x35a85591,0xa7cf,0x4b33,0x83,0x19,0x89,0x12,0x07,0x58,0x7a,0xf9);
                    public const string Name = "$Enum_ApplicationNames_TessaAdmin";
                    public const bool IsHidden = false;
            }
            public static class WebClient
            {
                    public static Guid ID = new Guid(0x9b7d9877,0x2017,0x4a35,0xb6,0x12,0x5f,0x83,0xbe,0xc3,0x9d,0xf9);
                    public const string Name = "$Enum_ApplicationNames_WebClient";
                    public const bool IsHidden = false;
            }
            public static class TessaAppManager
            {
                    public static Guid ID = new Guid(0x0468baaf,0x3a52,0x43bb,0x8e,0xfb,0x40,0xbf,0x17,0x57,0x77,0x6d);
                    public const string Name = "$Enum_ApplicationNames_TessaAppManager";
                    public const bool IsHidden = false;
            }
            public static class Chronos
            {
                    public static Guid ID = new Guid(0xfdd842ad,0x8318,0x42b8,0xb2,0xbb,0xf8,0x23,0x3b,0x37,0x19,0x9e);
                    public const string Name = "$Enum_ApplicationNames_Chronos";
                    public const bool IsHidden = false;
            }
            public static class TessaAdminConsole
            {
                    public static Guid ID = new Guid(0x6eb1fdba,0x7eac,0x4b70,0x96,0x12,0x16,0x1d,0xd9,0xfb,0xd5,0x11);
                    public const string Name = "$Enum_ApplicationNames_TessaAdminConsole";
                    public const bool IsHidden = false;
            }
            public static class TessaClientNotifications
            {
                    public static Guid ID = new Guid(0x1e3386c4,0x4baa,0x4bb6,0xb6,0xc9,0x64,0xb6,0x99,0x41,0x03,0x72);
                    public const string Name = "$Enum_ApplicationNames_TessaClientNotifications";
                    public const bool IsHidden = true;
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(ApplicationNamesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region ApplicationNames Enumeration

    public sealed class ApplicationNames
    {
        public readonly Guid ID;
        public readonly string Name;
        public readonly bool IsHidden;

        public ApplicationNames(Guid ID, string Name, bool IsHidden)
        {
            this.ID = ID;
            this.Name = Name;
            this.IsHidden = IsHidden;
        }
    }

    #endregion

    #endregion

    #region ApplicationRoles

    /// <summary>
    ///     ID: {7d23077a-8730-4ad7-9bcd-9a3d52c7e119}
    ///     Alias: ApplicationRoles
    ///     Group: System
    ///     Description: Роли, которым доступно приложение.
    /// </summary>
    public sealed class ApplicationRolesSchemeInfo
    {
        private const string name = "ApplicationRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(ApplicationRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Applications

    /// <summary>
    ///     ID: {6134967a-914b-45eb-99bd-a0ebefdca9f4}
    ///     Alias: Applications
    ///     Group: System
    ///     Description: Приложения
    /// </summary>
    public sealed class ApplicationsSchemeInfo
    {
        private const string name = "Applications";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Alias = nameof(Alias);
        public readonly string ExecutableFileName = nameof(ExecutableFileName);
        public readonly string AppVersion = nameof(AppVersion);
        public readonly string PlatformVersion = nameof(PlatformVersion);
        public readonly string ForAdmin = nameof(ForAdmin);
        public readonly string Icon = nameof(Icon);
        public readonly string GroupName = nameof(GroupName);
        public readonly string Client64Bit = nameof(Client64Bit);
        public readonly string AppManagerApiV2 = nameof(AppManagerApiV2);
        public readonly string Hidden = nameof(Hidden);

        #endregion

        #region ToString

        public static implicit operator string(ApplicationsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BackgroundColors

    /// <summary>
    ///     ID: {9f4fc1ce-af03-4009-8106-d9b861469ef1}
    ///     Alias: BackgroundColors
    ///     Group: System
    /// </summary>
    public sealed class BackgroundColorsSchemeInfo
    {
        private const string name = "BackgroundColors";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Color1 = nameof(Color1);
        public readonly string Color2 = nameof(Color2);
        public readonly string Color3 = nameof(Color3);
        public readonly string Color4 = nameof(Color4);
        public readonly string Color5 = nameof(Color5);

        #endregion

        #region ToString

        public static implicit operator string(BackgroundColorsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BarcodeTypes

    /// <summary>
    ///     ID: {60ad88cc-f913-48ce-96e1-0abf417da790}
    ///     Alias: BarcodeTypes
    ///     Group: System
    ///     Description: Типы штрих-кодов
    /// </summary>
    public sealed class BarcodeTypesSchemeInfo
    {
        private const string name = "BarcodeTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string CanPrint = nameof(CanPrint);
        public readonly string CanScan = nameof(CanScan);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class AZTEC
            {
                    public const int ID = 1;
                    public const string Name = "AZTEC";
                    public const bool CanPrint = false;
                    public const bool CanScan = true;
            }
            public static class CODABAR
            {
                    public const int ID = 2;
                    public const string Name = "CODABAR";
                    public const bool CanPrint = true;
                    public const bool CanScan = true;
            }
            public static class CODE_93
            {
                    public const int ID = 4;
                    public const string Name = "CODE_93";
                    public const bool CanPrint = true;
                    public const bool CanScan = true;
            }
            public static class EAN_13
            {
                    public const int ID = 8;
                    public const string Name = "EAN_13";
                    public const bool CanPrint = true;
                    public const bool CanScan = true;
            }
            public static class UPC_E
            {
                    public const int ID = 16;
                    public const string Name = "UPC_E";
                    public const bool CanPrint = true;
                    public const bool CanScan = true;
            }
            public static class CODE_39
            {
                    public const int ID = 3;
                    public const string Name = "CODE_39";
                    public const bool CanPrint = true;
                    public const bool CanScan = true;
            }
            public static class CODE_128
            {
                    public const int ID = 5;
                    public const string Name = "CODE_128";
                    public const bool CanPrint = true;
                    public const bool CanScan = true;
            }
            public static class DATA_MATRIX
            {
                    public const int ID = 6;
                    public const string Name = "DATA_MATRIX";
                    public const bool CanPrint = false;
                    public const bool CanScan = true;
            }
            public static class EAN_8
            {
                    public const int ID = 7;
                    public const string Name = "EAN_8";
                    public const bool CanPrint = true;
                    public const bool CanScan = true;
            }
            public static class ITF
            {
                    public const int ID = 9;
                    public const string Name = "ITF";
                    public const bool CanPrint = true;
                    public const bool CanScan = true;
            }
            public static class MAXICODE
            {
                    public const int ID = 10;
                    public const string Name = "MAXICODE";
                    public const bool CanPrint = false;
                    public const bool CanScan = true;
            }
            public static class PDF_417
            {
                    public const int ID = 11;
                    public const string Name = "PDF_417";
                    public const bool CanPrint = false;
                    public const bool CanScan = true;
            }
            public static class QR_CODE
            {
                    public const int ID = 12;
                    public const string Name = "QR_CODE";
                    public const bool CanPrint = false;
                    public const bool CanScan = true;
            }
            public static class RSS_14
            {
                    public const int ID = 13;
                    public const string Name = "RSS_14";
                    public const bool CanPrint = false;
                    public const bool CanScan = true;
            }
            public static class RSS_EXPANDED
            {
                    public const int ID = 14;
                    public const string Name = "RSS_EXPANDED";
                    public const bool CanPrint = false;
                    public const bool CanScan = true;
            }
            public static class UPC_A
            {
                    public const int ID = 15;
                    public const string Name = "UPC_A";
                    public const bool CanPrint = true;
                    public const bool CanScan = true;
            }
            public static class All_1D
            {
                    public const int ID = 17;
                    public const string Name = "All_1D";
                    public const bool CanPrint = false;
                    public const bool CanScan = true;
            }
            public static class UPC_EAN_EXTENSION
            {
                    public const int ID = 18;
                    public const string Name = "UPC_EAN_EXTENSION";
                    public const bool CanPrint = false;
                    public const bool CanScan = true;
            }
            public static class MSI
            {
                    public const int ID = 19;
                    public const string Name = "MSI";
                    public const bool CanPrint = true;
                    public const bool CanScan = true;
            }
            public static class PLESSEY
            {
                    public const int ID = 20;
                    public const string Name = "PLESSEY";
                    public const bool CanPrint = true;
                    public const bool CanScan = true;
            }
            public static class IMB
            {
                    public const int ID = 21;
                    public const string Name = "IMB";
                    public const bool CanPrint = false;
                    public const bool CanScan = true;
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(BarcodeTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region BarcodeTypes Enumeration

    public sealed class BarcodeTypes
    {
        public readonly int ID;
        public readonly string Name;
        public readonly bool CanPrint;
        public readonly bool CanScan;

        public BarcodeTypes(int ID, string Name, bool CanPrint, bool CanScan)
        {
            this.ID = ID;
            this.Name = Name;
            this.CanPrint = CanPrint;
            this.CanScan = CanScan;
        }
    }

    #endregion

    #endregion

    #region BlockColors

    /// <summary>
    ///     ID: {c1b59501-4d7f-4884-ac20-715d5d26078b}
    ///     Alias: BlockColors
    ///     Group: System
    /// </summary>
    public sealed class BlockColorsSchemeInfo
    {
        private const string name = "BlockColors";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Color1 = nameof(Color1);
        public readonly string Color2 = nameof(Color2);
        public readonly string Color3 = nameof(Color3);
        public readonly string Color4 = nameof(Color4);
        public readonly string Color5 = nameof(Color5);

        #endregion

        #region ToString

        public static implicit operator string(BlockColorsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BusinessProcessButtonExtension

    /// <summary>
    ///     ID: {b4d0da55-0e6e-4835-bb71-1df3c5b5e695}
    ///     Alias: BusinessProcessButtonExtension
    ///     Group: WorkflowEngine
    ///     Description: Основная секция для карточки-расширения тайла
    /// </summary>
    public sealed class BusinessProcessButtonExtensionSchemeInfo
    {
        private const string name = "BusinessProcessButtonExtension";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ButtonRowID = nameof(ButtonRowID);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessButtonExtensionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BusinessProcessButtonRoles

    /// <summary>
    ///     ID: {89599d8b-fa2f-44de-94d5-9687d4a16854}
    ///     Alias: BusinessProcessButtonRoles
    ///     Group: WorkflowEngine
    ///     Description: Список ролей, которым доступная данная кнопка
    /// </summary>
    public sealed class BusinessProcessButtonRolesSchemeInfo
    {
        private const string name = "BusinessProcessButtonRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ButtonRowID = nameof(ButtonRowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessButtonRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BusinessProcessButtonRolesVirtual

    /// <summary>
    ///     ID: {803fee29-9750-46f5-950d-77a44ff8b2af}
    ///     Alias: BusinessProcessButtonRolesVirtual
    ///     Group: WorkflowEngine
    ///     Description: Список ролей, которым доступная данная кнопка
    /// </summary>
    public sealed class BusinessProcessButtonRolesVirtualSchemeInfo
    {
        private const string name = "BusinessProcessButtonRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ButtonRowID = nameof(ButtonRowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessButtonRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BusinessProcessButtons

    /// <summary>
    ///     ID: {59bf0d0b-f7fc-41d3-92da-56c673f1e0b3}
    ///     Alias: BusinessProcessButtons
    ///     Group: WorkflowEngine
    ///     Description: Секция с описанием кнопок бизнес-процесса (как запускающих сам процесс, так и отправляющих команду для процесса).
    /// </summary>
    public sealed class BusinessProcessButtonsSchemeInfo
    {
        private const string name = "BusinessProcessButtons";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Caption = nameof(Caption);
        public readonly string StartProcess = nameof(StartProcess);
        public readonly string SignalID = nameof(SignalID);
        public readonly string SignalName = nameof(SignalName);
        public readonly string Group = nameof(Group);
        public readonly string Icon = nameof(Icon);
        public readonly string Description = nameof(Description);
        public readonly string Condition = nameof(Condition);
        public readonly string TileSizeID = nameof(TileSizeID);
        public readonly string TileSizeName = nameof(TileSizeName);
        public readonly string Tooltip = nameof(Tooltip);
        public readonly string AskConfirmation = nameof(AskConfirmation);
        public readonly string ConfirmationMessage = nameof(ConfirmationMessage);
        public readonly string ActionGrouping = nameof(ActionGrouping);
        public readonly string DisplaySettings = nameof(DisplaySettings);
        public readonly string ButtonHotkey = nameof(ButtonHotkey);
        public readonly string AccessDeniedMessage = nameof(AccessDeniedMessage);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessButtonsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BusinessProcessButtonsVirtual

    /// <summary>
    ///     ID: {033a363a-e183-4084-83cb-4672841a2a90}
    ///     Alias: BusinessProcessButtonsVirtual
    ///     Group: WorkflowEngine
    ///     Description: Секция с описанием кнопок бизнес-процесса (как запускающих сам процесс, так и отправляющих команду для процесса), в которую добавляются колонки из расширений на кнопки процесса.
    /// </summary>
    public sealed class BusinessProcessButtonsVirtualSchemeInfo
    {
        private const string name = "BusinessProcessButtonsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Caption = nameof(Caption);
        public readonly string StartProcess = nameof(StartProcess);
        public readonly string SignalID = nameof(SignalID);
        public readonly string SignalName = nameof(SignalName);
        public readonly string Group = nameof(Group);
        public readonly string Icon = nameof(Icon);
        public readonly string Description = nameof(Description);
        public readonly string Condition = nameof(Condition);
        public readonly string TileSizeID = nameof(TileSizeID);
        public readonly string TileSizeName = nameof(TileSizeName);
        public readonly string Tooltip = nameof(Tooltip);
        public readonly string AskConfirmation = nameof(AskConfirmation);
        public readonly string ConfirmationMessage = nameof(ConfirmationMessage);
        public readonly string ActionGrouping = nameof(ActionGrouping);
        public readonly string DisplaySettings = nameof(DisplaySettings);
        public readonly string ButtonHotkey = nameof(ButtonHotkey);
        public readonly string AccessDeniedMessage = nameof(AccessDeniedMessage);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessButtonsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BusinessProcessCardTypes

    /// <summary>
    ///     ID: {2317e111-2d0e-42d9-94dd-973411ecadca}
    ///     Alias: BusinessProcessCardTypes
    ///     Group: WorkflowEngine
    ///     Description: Список типов карточек, для которых доступен данный процесс
    /// </summary>
    public sealed class BusinessProcessCardTypesSchemeInfo
    {
        private const string name = "BusinessProcessCardTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string CardTypeID = nameof(CardTypeID);
        public readonly string CardTypeCaption = nameof(CardTypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessCardTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BusinessProcessEditRoles

    /// <summary>
    ///     ID: {669078fd-2901-4084-ac61-13a063581197}
    ///     Alias: BusinessProcessEditRoles
    ///     Group: WorkflowEngine
    ///     Description: Список ролей, имеющих доступ на редактирование  шаблона и экземпляра процесса
    /// </summary>
    public sealed class BusinessProcessEditRolesSchemeInfo
    {
        private const string name = "BusinessProcessEditRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessEditRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BusinessProcessExtensions

    /// <summary>
    ///     ID: {07e8720b-4500-4a7f-b988-7eda3bb8dc38}
    ///     Alias: BusinessProcessExtensions
    ///     Group: WorkflowEngine
    ///     Description: Секция со списком расширений для карточки шаблона процесса
    /// </summary>
    public sealed class BusinessProcessExtensionsSchemeInfo
    {
        private const string name = "BusinessProcessExtensions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ExtensionID = nameof(ExtensionID);
        public readonly string ExtensionName = nameof(ExtensionName);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessExtensionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BusinessProcessInfo

    /// <summary>
    ///     ID: {5640ffb9-ef7c-4584-8793-57da90e82fa0}
    ///     Alias: BusinessProcessInfo
    ///     Group: WorkflowEngine
    ///     Description: Секция с основной информацией о бизнес-процессе.
    /// </summary>
    public sealed class BusinessProcessInfoSchemeInfo
    {
        private const string name = "BusinessProcessInfo";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string StartFromCard = nameof(StartFromCard);
        public readonly string Multiple = nameof(Multiple);
        public readonly string ConditionModified = nameof(ConditionModified);
        public readonly string Group = nameof(Group);
        public readonly string LockMessage = nameof(LockMessage);
        public readonly string ErrorMessage = nameof(ErrorMessage);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BusinessProcessReadRoles

    /// <summary>
    ///     ID: {8a55a034-893d-4412-9458-189ef63d7008}
    ///     Alias: BusinessProcessReadRoles
    ///     Group: WorkflowEngine
    ///     Description: Список ролей, имеющих доступ на чтение экземпляра шаблона
    /// </summary>
    public sealed class BusinessProcessReadRolesSchemeInfo
    {
        private const string name = "BusinessProcessReadRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessReadRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BusinessProcessVersions

    /// <summary>
    ///     ID: {dcd38c54-ed18-4503-b435-3dee1c6c2c62}
    ///     Alias: BusinessProcessVersions
    ///     Group: WorkflowEngine
    ///     Description: Дерево версий бизнесс процесса
    /// </summary>
    public sealed class BusinessProcessVersionsSchemeInfo
    {
        private const string name = "BusinessProcessVersions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string Version = nameof(Version);
        public readonly string Created = nameof(Created);
        public readonly string CreatedByID = nameof(CreatedByID);
        public readonly string CreatedByName = nameof(CreatedByName);
        public readonly string Modified = nameof(Modified);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);
        public readonly string LockedForEditing = nameof(LockedForEditing);
        public readonly string ScriptFileID = nameof(ScriptFileID);
        public readonly string ProcessData = nameof(ProcessData);
        public readonly string IsDefault = nameof(IsDefault);
        public readonly string LockedByID = nameof(LockedByID);
        public readonly string LockedByName = nameof(LockedByName);
        public readonly string Locked = nameof(Locked);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessVersionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region BusinessProcessVersionsVirtual

    /// <summary>
    ///     ID: {6999d0d3-a44f-43b5-8e79-a551697340e6}
    ///     Alias: BusinessProcessVersionsVirtual
    ///     Group: WorkflowEngine
    ///     Description: Список версий бизнес-процесса, который отображается в карточке шаблона бизнес-процесса
    /// </summary>
    public sealed class BusinessProcessVersionsVirtualSchemeInfo
    {
        private const string name = "BusinessProcessVersionsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Version = nameof(Version);
        public readonly string Created = nameof(Created);
        public readonly string CreatedByID = nameof(CreatedByID);
        public readonly string CreatedByName = nameof(CreatedByName);
        public readonly string Modified = nameof(Modified);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);
        public readonly string ParentVersion = nameof(ParentVersion);
        public readonly string IsDefault = nameof(IsDefault);
        public readonly string LockedForEditing = nameof(LockedForEditing);
        public readonly string LockedByID = nameof(LockedByID);
        public readonly string LockedByName = nameof(LockedByName);
        public readonly string ActiveCount = nameof(ActiveCount);
        public readonly string Locked = nameof(Locked);

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessVersionsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CalendarCalcMethods

    /// <summary>
    ///     ID: {011f3246-c0f2-4d91-aaee-5129c6b83e15}
    ///     Alias: CalendarCalcMethods
    ///     Group: System
    /// </summary>
    public sealed class CalendarCalcMethodsSchemeInfo
    {
        private const string name = "CalendarCalcMethods";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Description = nameof(Description);
        public readonly string Script = nameof(Script);

        #endregion

        #region ToString

        public static implicit operator string(CalendarCalcMethodsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CalendarExclusions

    /// <summary>
    ///     ID: {aec4456f-c927-4a49-89f5-582ab17dc997}
    ///     Alias: CalendarExclusions
    ///     Group: System
    /// </summary>
    public sealed class CalendarExclusionsSchemeInfo
    {
        private const string name = "CalendarExclusions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StartTime = nameof(StartTime);
        public readonly string EndTime = nameof(EndTime);
        public readonly string IsNotWorkingTime = nameof(IsNotWorkingTime);

        #endregion

        #region ToString

        public static implicit operator string(CalendarExclusionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CalendarNamedRanges

    /// <summary>
    ///     ID: {dc5ec614-d5df-40d1-ba43-4e5b97211711}
    ///     Alias: CalendarNamedRanges
    ///     Group: System
    /// </summary>
    public sealed class CalendarNamedRangesSchemeInfo
    {
        private const string name = "CalendarNamedRanges";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StartTime = nameof(StartTime);
        public readonly string EndTime = nameof(EndTime);
        public readonly string IsNotWorkingTime = nameof(IsNotWorkingTime);
        public readonly string Name = nameof(Name);
        public readonly string IsManual = nameof(IsManual);

        #endregion

        #region ToString

        public static implicit operator string(CalendarNamedRangesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CalendarQuants

    /// <summary>
    ///     ID: {094fac6d-4fe8-4d3e-89c2-22a0f74fd705}
    ///     Alias: CalendarQuants
    ///     Group: System
    /// </summary>
    public sealed class CalendarQuantsSchemeInfo
    {
        private const string name = "CalendarQuants";

        #region Columns

        public readonly string QuantNumber = nameof(QuantNumber);
        public readonly string StartTime = nameof(StartTime);
        public readonly string EndTime = nameof(EndTime);
        public readonly string Type = nameof(Type);
        public readonly string ID = nameof(ID);

        #endregion

        #region ToString

        public static implicit operator string(CalendarQuantsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CalendarSettings

    /// <summary>
    ///     ID: {67b1fd42-0106-4b31-a368-ea3e4d38ac5c}
    ///     Alias: CalendarSettings
    ///     Group: System
    /// </summary>
    public sealed class CalendarSettingsSchemeInfo
    {
        private const string name = "CalendarSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CalendarStart = nameof(CalendarStart);
        public readonly string CalendarEnd = nameof(CalendarEnd);
        public readonly string Description = nameof(Description);
        public readonly string CalendarID = nameof(CalendarID);
        public readonly string CalendarTypeID = nameof(CalendarTypeID);
        public readonly string CalendarTypeCaption = nameof(CalendarTypeCaption);
        public readonly string Name = nameof(Name);

        #endregion

        #region ToString

        public static implicit operator string(CalendarSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CalendarTypeExclusions

    /// <summary>
    ///     ID: {3a11e188-f82f-495b-a78f-778f2988db52}
    ///     Alias: CalendarTypeExclusions
    ///     Group: System
    /// </summary>
    public sealed class CalendarTypeExclusionsSchemeInfo
    {
        private const string name = "CalendarTypeExclusions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Caption = nameof(Caption);
        public readonly string StartTime = nameof(StartTime);
        public readonly string EndTime = nameof(EndTime);
        public readonly string IsNotWorkingTime = nameof(IsNotWorkingTime);

        #endregion

        #region ToString

        public static implicit operator string(CalendarTypeExclusionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CalendarTypes

    /// <summary>
    ///     ID: {c411ab46-1df7-4a76-97b5-d0d39fff656b}
    ///     Alias: CalendarTypes
    ///     Group: System
    /// </summary>
    public sealed class CalendarTypesSchemeInfo
    {
        private const string name = "CalendarTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Caption = nameof(Caption);
        public readonly string Description = nameof(Description);
        public readonly string CalcMethodID = nameof(CalcMethodID);
        public readonly string CalcMethodName = nameof(CalcMethodName);
        public readonly string HoursInDay = nameof(HoursInDay);
        public readonly string WorkDaysInWeek = nameof(WorkDaysInWeek);

        #endregion

        #region ToString

        public static implicit operator string(CalendarTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CalendarTypeWeekDays

    /// <summary>
    ///     ID: {67d63f49-ec4f-4e3b-9364-0b6e38d138ec}
    ///     Alias: CalendarTypeWeekDays
    ///     Group: System
    /// </summary>
    public sealed class CalendarTypeWeekDaysSchemeInfo
    {
        private const string name = "CalendarTypeWeekDays";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Number = nameof(Number);
        public readonly string Name = nameof(Name);
        public readonly string WorkingDayStart = nameof(WorkingDayStart);
        public readonly string WorkingDayEnd = nameof(WorkingDayEnd);
        public readonly string LunchStart = nameof(LunchStart);
        public readonly string LunchEnd = nameof(LunchEnd);
        public readonly string IsNotWorkingDay = nameof(IsNotWorkingDay);

        #endregion

        #region ToString

        public static implicit operator string(CalendarTypeWeekDaysSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CompilationCache

    /// <summary>
    ///     ID: {3f86165e-8a0d-41d9-a7a2-b6a511bf551b}
    ///     Alias: CompilationCache
    ///     Group: System
    ///     Description: Результаты компиляции объектов системы.
    /// </summary>
    public sealed class CompilationCacheSchemeInfo
    {
        private const string name = "CompilationCache";

        #region Columns

        public readonly string CategoryID = nameof(CategoryID);
        public readonly string ID = nameof(ID);
        public readonly string Result = nameof(Result);
        public readonly string Assembly = nameof(Assembly);

        #endregion

        #region ToString

        public static implicit operator string(CompilationCacheSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CompiledViews

    /// <summary>
    ///     ID: {0ebd80aa-360b-473b-8327-90e10035c000}
    ///     Alias: CompiledViews
    ///     Group: System
    /// </summary>
    public sealed class CompiledViewsSchemeInfo
    {
        private const string name = "CompiledViews";

        #region Columns

        public readonly string ViewID = nameof(ViewID);
        public readonly string ViewAlias = nameof(ViewAlias);
        public readonly string FunctionName = nameof(FunctionName);
        public readonly string LastUsed = nameof(LastUsed);
        public readonly string ViewModifiedDateTime = nameof(ViewModifiedDateTime);

        #endregion

        #region ToString

        public static implicit operator string(CompiledViewsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CompletionOptions

    /// <summary>
    ///     ID: {08cf782d-4130-4377-8a49-3e201a05d496}
    ///     Alias: CompletionOptions
    ///     Group: System
    ///     Description: Список возможных варианты завершения.
    /// </summary>
    public sealed class CompletionOptionsSchemeInfo
    {
        private const string name = "CompletionOptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class AddComment
            {
                    public static Guid ID = new Guid(0x78c2fd7d,0xd0fe,0x0ede,0x93,0xa6,0x9d,0xe4,0xf3,0x72,0xe8,0xe6);
                    public const string Name = "AddComment";
                    public const string Caption = "$UI_Tasks_CompletionOptions_AddComment";
            }
            public static class Approve
            {
                    public static Guid ID = new Guid(0x8cf5cf41,0x8347,0x05b4,0xb3,0xb2,0x51,0x9e,0x8e,0x62,0x12,0x25);
                    public const string Name = "Approve";
                    public const string Caption = "$UI_Tasks_CompletionOptions_Approve";
            }
            public static class Cancel
            {
                    public static Guid ID = new Guid(0x2582b66f,0x375a,0x0d59,0xae,0x86,0xa1,0x49,0x30,0x9c,0x57,0x85);
                    public const string Name = "Cancel";
                    public const string Caption = "$UI_Tasks_CompletionOptions_Cancel";
            }
            public static class CancelApprovalProcess
            {
                    public static Guid ID = new Guid(0x6e244482,0x2e2f,0x46fd,0x8e,0xc3,0x0d,0xe6,0xda,0xea,0x29,0x30);
                    public const string Name = "CancelApprovalProcess";
                    public const string Caption = "$UI_Tasks_CompletionOptions_CancelApprovalProcess";
            }
            public static class Complete
            {
                    public static Guid ID = new Guid(0x5b108223,0x92db,0x49b9,0x80,0x85,0x33,0x67,0x58,0xcc,0xab,0xaa);
                    public const string Name = "Complete";
                    public const string Caption = "$UI_Tasks_CompletionOptions_Complete";
            }
            public static class Continue
            {
                    public static Guid ID = new Guid(0x9ba9f111,0xfa2f,0x4c8e,0x82,0x36,0xc9,0x24,0x28,0x0a,0x4a,0x07);
                    public const string Name = "Continue";
                    public const string Caption = "$UI_Tasks_CompletionOptions_Continue";
            }
            public static class CreateChildResolution
            {
                    public static Guid ID = new Guid(0x793bbafa,0x7f62,0x4af8,0xa1,0x56,0x51,0x58,0x87,0xd4,0xd0,0x66);
                    public const string Name = "CreateChildResolution";
                    public const string Caption = "$UI_Tasks_CompletionOptions_CreateChildResolution";
            }
            public static class Delegate
            {
                    public static Guid ID = new Guid(0xb997a7f2,0xad57,0x036f,0x87,0x98,0x29,0x8c,0x14,0x30,0x9f,0x46);
                    public const string Name = "Delegate";
                    public const string Caption = "$UI_Tasks_CompletionOptions_Delegate";
            }
            public static class DeregisterDocument
            {
                    public static Guid ID = new Guid(0x66e0a7e1,0x484a,0x40a6,0xb1,0x23,0x06,0x11,0x8c,0xe3,0xb1,0x60);
                    public const string Name = "DeregisterDocument";
                    public const string Caption = "$UI_Tasks_CompletionOptions_DeregisterDocument";
            }
            public static class Disapprove
            {
                    public static Guid ID = new Guid(0x811d41ef,0x5610,0x421e,0xa5,0x73,0xfc,0xdf,0xd8,0x21,0x71,0x3e);
                    public const string Name = "Disapprove";
                    public const string Caption = "$UI_Tasks_CompletionOptions_Disapprove";
            }
            public static class ModifyAsAuthor
            {
                    public static Guid ID = new Guid(0x89ada741,0x6829,0x4d9f,0x89,0x2b,0x72,0xd7,0x6e,0xcf,0x4e,0xe6);
                    public const string Name = "ModifyAsAuthor";
                    public const string Caption = "$UI_Tasks_CompletionOptions_ModifyAsAuthor";
            }
            public static class NewApprovalCycle
            {
                    public static Guid ID = new Guid(0xc0b704b3,0x3ac5,0x4a0d,0xbc,0xb6,0x12,0x10,0xe9,0xcd,0xb0,0xb3);
                    public const string Name = "NewApprovalCycle";
                    public const string Caption = "$UI_Tasks_CompletionOptions_NewApprovalCycle";
            }
            public static class OptionA
            {
                    public static Guid ID = new Guid(0xd6fbbf34,0xd22d,0x4226,0x83,0x1d,0xf3,0xf1,0xf3,0x1b,0x99,0x54);
                    public const string Name = "OptionA";
                    public const string Caption = "$UI_Tasks_CompletionOptions_OptionA";
            }
            public static class OptionB
            {
                    public static Guid ID = new Guid(0x679a8309,0xf251,0x4acf,0x8b,0x2e,0x7c,0x52,0x77,0xb0,0x4d,0x63);
                    public const string Name = "OptionB";
                    public const string Caption = "$UI_Tasks_CompletionOptions_OptionB";
            }
            public static class RebuildDocument
            {
                    public static Guid ID = new Guid(0x174d3f96,0xc658,0x07b7,0xba,0x6a,0xd5,0x1a,0x89,0x33,0x90,0xd8);
                    public const string Name = "RebuildDocument";
                    public const string Caption = "$UI_Tasks_CompletionOptions_Rebuild";
            }
            public static class RegisterDocument
            {
                    public static Guid ID = new Guid(0x48ae0fd4,0x8a0d,0x494a,0xb8,0x9d,0xca,0x8f,0xc3,0x3e,0xfe,0x7c);
                    public const string Name = "RegisterDocument";
                    public const string Caption = "$UI_Tasks_CompletionOptions_RegisterDocument";
            }
            public static class RejectApproval
            {
                    public static Guid ID = new Guid(0xd97d75a9,0x96ae,0x00ca,0x83,0xad,0xba,0xa5,0xc6,0xaa,0x81,0x1b);
                    public const string Name = "RejectApproval";
                    public const string Caption = "$UI_Tasks_CompletionOptions_Reject";
            }
            public static class RequestComments
            {
                    public static Guid ID = new Guid(0xfffb3209,0x2b67,0x09f0,0xbd,0x25,0xba,0x4e,0xc9,0x4c,0xa5,0xe8);
                    public const string Name = "RequestComments";
                    public const string Caption = "$UI_Tasks_CompletionOptions_RequestComments";
            }
            public static class Revoke
            {
                    public static Guid ID = new Guid(0x6472fea9,0xf818,0x4ab5,0x9f,0x31,0x9c,0xcd,0xae,0xa9,0xb4,0x12);
                    public const string Name = "Revoke";
                    public const string Caption = "$UI_Tasks_CompletionOptions_Revoke";
            }
            public static class SendToPerformer
            {
                    public static Guid ID = new Guid(0xf4ebe563,0x14f6,0x4b20,0xa6,0x1f,0x0b,0xac,0x4c,0x11,0xc8,0xac);
                    public const string Name = "SendToPerformer";
                    public const string Caption = "$UI_Tasks_CompletionOptions_SendToPerformer";
            }
            public static class Accept
            {
                    public static Guid ID = new Guid(0x7000ea10,0xefd8,0x0479,0xa6,0xd4,0xb5,0xe3,0x7a,0x27,0xf3,0x0a);
                    public const string Name = "Accept";
                    public const string Caption = "$UI_Tasks_CompletionOptions_Accept";
            }
            public static class AdditionalApproval
            {
                    public static Guid ID = new Guid(0xc726d8ba,0x73b9,0x4867,0x87,0xfe,0x38,0x7d,0x4c,0x61,0xa7,0x5a);
                    public const string Name = "AdditionalApproval";
                    public const string Caption = "$UI_Tasks_CompletionOptions_AdditionalApproval";
            }
            public static class Sign
            {
                    public static Guid ID = new Guid(0x45d6f756,0xd30b,0x4c98,0x9d,0x72,0x6a,0xdf,0x1a,0x15,0xd0,0x75);
                    public const string Name = "Sign";
                    public const string Caption = "$UI_Tasks_CompletionOptions_Sign";
            }
            public static class Decline
            {
                    public static Guid ID = new Guid(0x4de44ffd,0xc2ca,0x4fad,0x83,0x5b,0x63,0x12,0x22,0xb0,0x76,0xe1);
                    public const string Name = "Decline";
                    public const string Caption = "$UI_Tasks_CompletionOptions_Decline";
            }
            public static class ShowDialog
            {
                    public static Guid ID = new Guid(0xa9067834,0x1a01,0x468c,0x97,0x6b,0x0e,0xc7,0xa9,0x93,0x93,0x31);
                    public const string Name = "ShowDialog";
                    public const string Caption = "$UI_Tasks_CompletionOptions_ShowDialog";
            }
            public static class TakeOver
            {
                    public static Guid ID = new Guid(0x08cf782d,0x4130,0x4377,0x8a,0x49,0x3e,0x20,0x1a,0x05,0xd4,0x96);
                    public const string Name = "TakeOver";
                    public const string Caption = "$UI_Tasks_CompletionOptions_TakeOver";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(CompletionOptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region CompletionOptions Enumeration

    public sealed class CompletionOptions
    {
        public readonly Guid ID;
        public readonly string Name;
        public readonly string Caption;

        public CompletionOptions(Guid ID, string Name, string Caption)
        {
            this.ID = ID;
            this.Name = Name;
            this.Caption = Caption;
        }
    }

    #endregion

    #endregion

    #region CompletionOptionsVirtual

    /// <summary>
    ///     ID: {cfff92c8-26e6-42e5-b45d-837bc374022d}
    ///     Alias: CompletionOptionsVirtual
    ///     Group: System
    ///     Description: Виртуальная карточка для варианта завершения.
    /// </summary>
    public sealed class CompletionOptionsVirtualSchemeInfo
    {
        private const string name = "CompletionOptionsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string OptionID = nameof(OptionID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);
        public readonly string PartitionID = nameof(PartitionID);
        public readonly string PartitionName = nameof(PartitionName);

        #endregion

        #region ToString

        public static implicit operator string(CompletionOptionsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ConditionsVirtual

    /// <summary>
    ///     ID: {6d2ec0d3-4980-45f3-aa64-ab79eb9f4da1}
    ///     Alias: ConditionsVirtual
    ///     Group: System
    /// </summary>
    public sealed class ConditionsVirtualSchemeInfo
    {
        private const string name = "ConditionsVirtual";

        #region Columns

        public readonly string TriggerRowID = nameof(TriggerRowID);
        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RuleRowID = nameof(RuleRowID);
        public readonly string ConditionTypeID = nameof(ConditionTypeID);
        public readonly string ConditionTypeName = nameof(ConditionTypeName);
        public readonly string Order = nameof(Order);
        public readonly string InvertCondition = nameof(InvertCondition);
        public readonly string Settings = nameof(Settings);
        public readonly string Description = nameof(Description);
        public readonly string InvertConditionString = nameof(InvertConditionString);
        public readonly string WorkflowConditionRowID = nameof(WorkflowConditionRowID);

        #endregion

        #region ToString

        public static implicit operator string(ConditionsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ConditionTypes

    /// <summary>
    ///     ID: {7e0c2c3b-e8f3-4f96-9aa6-eb1c2100d74f}
    ///     Alias: ConditionTypes
    ///     Group: System
    ///     Description: Тип условия
    /// </summary>
    public sealed class ConditionTypesSchemeInfo
    {
        private const string name = "ConditionTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string SettingsCardTypeID = nameof(SettingsCardTypeID);
        public readonly string SettingsCardTypeCaption = nameof(SettingsCardTypeCaption);
        public readonly string ConditionText = nameof(ConditionText);
        public readonly string Condition = nameof(Condition);
        public readonly string Description = nameof(Description);

        #endregion

        #region ToString

        public static implicit operator string(ConditionTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ConditionTypeUsePlaces

    /// <summary>
    ///     ID: {b764f842-5b97-4de7-854a-f61b6b7a71dc}
    ///     Alias: ConditionTypeUsePlaces
    ///     Group: System
    /// </summary>
    public sealed class ConditionTypeUsePlacesSchemeInfo
    {
        private const string name = "ConditionTypeUsePlaces";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string UsePlaceName = nameof(UsePlaceName);
        public readonly string UsePlaceID = nameof(UsePlaceID);

        #endregion

        #region ToString

        public static implicit operator string(ConditionTypeUsePlacesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ConditionUsePlaces

    /// <summary>
    ///     ID: {6963c76f-5e8d-49b5-80a3-f2ec342de0bf}
    ///     Alias: ConditionUsePlaces
    ///     Group: System
    /// </summary>
    public sealed class ConditionUsePlacesSchemeInfo
    {
        private const string name = "ConditionUsePlaces";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class AclGenerationRule
            {
                    public static Guid ID = new Guid(0x76b57ab5,0x19d4,0x4cc9,0x9e,0x80,0x2c,0x2f,0x05,0xcc,0x24,0x25);
                    public const string Name = "$ConditionUsePlace_AclGenerationRule";
            }
            public static class SmartRoleGenerator
            {
                    public static Guid ID = new Guid(0xc72e05fb,0x7eef,0x4256,0x90,0x29,0x72,0xf8,0x21,0xf4,0xf7,0x9e);
                    public const string Name = "$ConditionUsePlace_SmartRoleGenerator";
            }
            public static class NotificationRules
            {
                    public static Guid ID = new Guid(0x929ad23c,0x8a22,0x09aa,0x90,0x00,0x39,0x8b,0xf1,0x39,0x79,0xb2);
                    public const string Name = "$ConditionUsePlace_NotificationRules";
            }
            public static class KrVirtualFiles
            {
                    public static Guid ID = new Guid(0x81250a95,0x5c1e,0x488c,0xa4,0x23,0x10,0x6e,0x7f,0x98,0x2c,0x6b);
                    public const string Name = "$ConditionUsePlace_KrVirtualFiles";
            }
            public static class KrPermissions
            {
                    public static Guid ID = new Guid(0xfa9dbdac,0x8708,0x41df,0xbd,0x72,0x90,0x0f,0x69,0x65,0x5d,0xfa);
                    public const string Name = "$ConditionUsePlace_KrPermissions";
            }
            public static class KrSecondaryProcesses
            {
                    public static Guid ID = new Guid(0x61420fa1,0xcc1f,0x47cb,0xb0,0xbb,0x4e,0xa8,0xee,0x77,0xf5,0x1a);
                    public const string Name = "$ConditionUsePlace_KrSecondaryProcesses";
            }
            public static class WorkflowEngine
            {
                    public static Guid ID = new Guid(0xeb222506,0x6f7d,0x4c22,0xb3,0xd2,0xd9,0x8a,0x2f,0x39,0x0a,0xc5);
                    public const string Name = "$ConditionUsePlace_WorkflowEngine";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(ConditionUsePlacesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region ConditionUsePlaces Enumeration

    public sealed class ConditionUsePlaces
    {
        public readonly Guid ID;
        public readonly string Name;

        public ConditionUsePlaces(Guid ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region Configuration

    /// <summary>
    ///     ID: {57b9e507-d135-4c69-9a94-bf507d499484}
    ///     Alias: Configuration
    ///     Group: System
    ///     Description: Configuration properties
    /// </summary>
    public sealed class ConfigurationSchemeInfo
    {
        private const string name = "Configuration";

        #region Columns

        public readonly string BuildVersion = nameof(BuildVersion);
        public readonly string BuildName = nameof(BuildName);
        public readonly string BuildDate = nameof(BuildDate);
        public readonly string Description = nameof(Description);
        public readonly string Modified = nameof(Modified);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);
        public readonly string Version = nameof(Version);

        #endregion

        #region ToString

        public static implicit operator string(ConfigurationSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ContextRoles

    /// <summary>
    ///     ID: {be5a85fd-b2fb-4f60-a3b7-48e79e45249f}
    ///     Alias: ContextRoles
    ///     Group: Roles
    ///     Description: Контекстные роли.
    /// </summary>
    public sealed class ContextRolesSchemeInfo
    {
        private const string name = "ContextRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string SqlText = nameof(SqlText);
        public readonly string SqlTextForCard = nameof(SqlTextForCard);
        public readonly string SqlTextForUser = nameof(SqlTextForUser);

        #endregion

        #region ToString

        public static implicit operator string(ContextRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Currencies

    /// <summary>
    ///     ID: {3612e150-032f-4a68-bf8e-8e094e5a3a73}
    ///     Alias: Currencies
    ///     Group: Common
    ///     Description: Валюты.
    /// </summary>
    public sealed class CurrenciesSchemeInfo
    {
        private const string name = "Currencies";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);
        public readonly string Code = nameof(Code);

        #endregion

        #region ToString

        public static implicit operator string(CurrenciesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CustomBackgroundColorsVirtual

    /// <summary>
    ///     ID: {5d65177e-590c-4422-9120-1a202a534640}
    ///     Alias: CustomBackgroundColorsVirtual
    ///     Group: System
    /// </summary>
    public sealed class CustomBackgroundColorsVirtualSchemeInfo
    {
        private const string name = "CustomBackgroundColorsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Color1 = nameof(Color1);
        public readonly string Color2 = nameof(Color2);
        public readonly string Color3 = nameof(Color3);
        public readonly string Color4 = nameof(Color4);

        #endregion

        #region ToString

        public static implicit operator string(CustomBackgroundColorsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CustomBlockColorsVirtual

    /// <summary>
    ///     ID: {cafa4371-0483-4d71-80cd-75d68cd6086f}
    ///     Alias: CustomBlockColorsVirtual
    ///     Group: System
    /// </summary>
    public sealed class CustomBlockColorsVirtualSchemeInfo
    {
        private const string name = "CustomBlockColorsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Color1 = nameof(Color1);
        public readonly string Color2 = nameof(Color2);
        public readonly string Color3 = nameof(Color3);
        public readonly string Color4 = nameof(Color4);

        #endregion

        #region ToString

        public static implicit operator string(CustomBlockColorsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region CustomForegroundColorsVirtual

    /// <summary>
    ///     ID: {8f0adc86-8166-4579-9a25-7c3f2921d32d}
    ///     Alias: CustomForegroundColorsVirtual
    ///     Group: System
    /// </summary>
    public sealed class CustomForegroundColorsVirtualSchemeInfo
    {
        private const string name = "CustomForegroundColorsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Color1 = nameof(Color1);
        public readonly string Color2 = nameof(Color2);
        public readonly string Color3 = nameof(Color3);
        public readonly string Color4 = nameof(Color4);

        #endregion

        #region ToString

        public static implicit operator string(CustomForegroundColorsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DateFormats

    /// <summary>
    ///     ID: {585825ed-e297-4eb3-bea2-a732ad75c6b6}
    ///     Alias: DateFormats
    ///     Group: System
    ///     Description: Формат для отображаемых дат, определяет порядок следования дня, месяца и года.
    /// </summary>
    public sealed class DateFormatsSchemeInfo
    {
        private const string name = "DateFormats";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class MonthDayYear
            {
                    public const int ID = 0;
                    public const string Name = "MonthDayYear";
                    public const string Caption = "$Enum_DateFormats_MonthDayYear";
            }
            public static class DayMonthYear
            {
                    public const int ID = 1;
                    public const string Name = "DayMonthYear";
                    public const string Caption = "$Enum_DateFormats_DayMonthYear";
            }
            public static class YearMonthDay
            {
                    public const int ID = 2;
                    public const string Name = "YearMonthDay";
                    public const string Caption = "$Enum_DateFormats_YearMonthDay";
            }
            public static class YearDayMonth
            {
                    public const int ID = 3;
                    public const string Name = "YearDayMonth";
                    public const string Caption = "$Enum_DateFormats_YearDayMonth";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(DateFormatsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region DateFormats Enumeration

    public sealed class DateFormats
    {
        public readonly int ID;
        public readonly string Name;
        public readonly string Caption;

        public DateFormats(int ID, string Name, string Caption)
        {
            this.ID = ID;
            this.Name = Name;
            this.Caption = Caption;
        }
    }

    #endregion

    #endregion

    #region DefaultTimeZone

    /// <summary>
    ///     ID: {d894a451-c0ff-4a75-b808-05d24cf077bf}
    ///     Alias: DefaultTimeZone
    ///     Group: System
    ///     Description: Данные для временной зоны по умолчанию. 
    ///                  Хранятся отдельно, чтобы не затирались при изменении таблички с временными зонами (TimeZones)
    /// </summary>
    public sealed class DefaultTimeZoneSchemeInfo
    {
        private const string name = "DefaultTimeZone";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CodeName = nameof(CodeName);
        public readonly string UtcOffsetMinutes = nameof(UtcOffsetMinutes);
        public readonly string DisplayName = nameof(DisplayName);
        public readonly string ShortName = nameof(ShortName);
        public readonly string IsNegativeOffsetDirection = nameof(IsNegativeOffsetDirection);
        public readonly string OffsetTime = nameof(OffsetTime);
        public readonly string ZoneID = nameof(ZoneID);

        #endregion

        #region ToString

        public static implicit operator string(DefaultTimeZoneSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DefaultWorkplacesVirtual

    /// <summary>
    ///     ID: {dd42ee04-02c5-407b-b596-07aa830a9b80}
    ///     Alias: DefaultWorkplacesVirtual
    ///     Group: System
    ///     Description: Список рабочих мест открываемых по умолчанию
    /// </summary>
    public sealed class DefaultWorkplacesVirtualSchemeInfo
    {
        private const string name = "DefaultWorkplacesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string WorkplaceID = nameof(WorkplaceID);
        public readonly string WorkplaceName = nameof(WorkplaceName);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(DefaultWorkplacesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Deleted

    /// <summary>
    ///     ID: {a49102cc-6bb4-425b-95ad-75ff0b3edf0d}
    ///     Alias: Deleted
    ///     Group: System
    ///     Description: Информация об удалённой карточке.
    /// </summary>
    public sealed class DeletedSchemeInfo
    {
        private const string name = "Deleted";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Digest = nameof(Digest);
        public readonly string Card = nameof(Card);
        public readonly string CardID = nameof(CardID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string Created = nameof(Created);
        public readonly string CreatedByID = nameof(CreatedByID);
        public readonly string CreatedByName = nameof(CreatedByName);

        #endregion

        #region ToString

        public static implicit operator string(DeletedSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DeletedTaskRoles

    /// <summary>
    ///     ID: {8340a9b3-74ba-4771-af73-35bed38db55e}
    ///     Alias: DeletedTaskRoles
    ///     Group: System
    ///     Description: Роли, на которые были назначены задания в удалённой карточке.
    /// </summary>
    public sealed class DeletedTaskRolesSchemeInfo
    {
        private const string name = "DeletedTaskRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string RoleTypeID = nameof(RoleTypeID);

        #endregion

        #region ToString

        public static implicit operator string(DeletedTaskRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DeletedVirtual

    /// <summary>
    ///     ID: {300db9a6-f6a0-48a8-b6c3-5f8891817cdd}
    ///     Alias: DeletedVirtual
    ///     Group: System
    /// </summary>
    public sealed class DeletedVirtualSchemeInfo
    {
        private const string name = "DeletedVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CardStorage = nameof(CardStorage);
        public readonly string CardIDString = nameof(CardIDString);

        #endregion

        #region ToString

        public static implicit operator string(DeletedVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DepartmentRoles

    /// <summary>
    ///     ID: {d43dace1-536f-4c9f-af15-49a8892a7427}
    ///     Alias: DepartmentRoles
    ///     Group: Roles
    ///     Description: Роли департаментов.
    /// </summary>
    public sealed class DepartmentRolesSchemeInfo
    {
        private const string name = "DepartmentRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string HeadUserID = nameof(HeadUserID);
        public readonly string HeadUserName = nameof(HeadUserName);

        #endregion

        #region ToString

        public static implicit operator string(DepartmentRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DeviceTypes

    /// <summary>
    ///     ID: {8b4cd042-334b-4aee-a623-7d8942aa6897}
    ///     Alias: DeviceTypes
    ///     Group: System
    ///     Description: Типы устройств, с которых пользователь использует приложения Tessa.
    /// </summary>
    public sealed class DeviceTypesSchemeInfo
    {
        private const string name = "DeviceTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Other
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_DeviceTypes_Other";
            }
            public static class Desktop
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_DeviceTypes_Desktop";
            }
            public static class Phone
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_DeviceTypes_Phone";
            }
            public static class Tablet
            {
                    public const int ID = 3;
                    public const string Name = "$Enum_DeviceTypes_Tablet";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(DeviceTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region DeviceTypes Enumeration

    public sealed class DeviceTypes
    {
        public readonly int ID;
        public readonly string Name;

        public DeviceTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region DialogButtonTypes

    /// <summary>
    ///     ID: {e07bb4d3-1312-4638-9751-ddd8e3a127fc}
    ///     Alias: DialogButtonTypes
    ///     Group: System
    /// </summary>
    public sealed class DialogButtonTypesSchemeInfo
    {
        private const string name = "DialogButtonTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class ToolbarButton
            {
                    public const int ID = 0;
                    public const string Name = "$DialogButtonTypes_ToolbarButton";
            }
            public static class BottomToolbarButton
            {
                    public const int ID = 1;
                    public const string Name = "$DialogButtonTypes_BottomToolbarButton";
            }
            public static class BottomDialogButton
            {
                    public const int ID = 2;
                    public const string Name = "$DialogButtonTypes_BottomDialogButton";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(DialogButtonTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region DialogButtonTypes Enumeration

    public sealed class DialogButtonTypes
    {
        public readonly int ID;
        public readonly string Name;

        public DialogButtonTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region DialogCardAutoOpenModes

    /// <summary>
    ///     ID: {b1827f66-89bd-4269-b2ce-ea27337616fd}
    ///     Alias: DialogCardAutoOpenModes
    ///     Group: System
    /// </summary>
    public sealed class DialogCardAutoOpenModesSchemeInfo
    {
        private const string name = "DialogCardAutoOpenModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class AlwaysOpen
            {
                    public const int ID = 0;
                    public const string Name = "$DialogCardAutoOpenModes_AlwaysOpen";
            }
            public static class ButtonClickOpen
            {
                    public const int ID = 1;
                    public const string Name = "$DialogCardAutoOpenModes_ButtonClickOpen";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(DialogCardAutoOpenModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region DialogCardAutoOpenModes Enumeration

    public sealed class DialogCardAutoOpenModes
    {
        public readonly int ID;
        public readonly string Name;

        public DialogCardAutoOpenModes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region DialogCardStoreModes

    /// <summary>
    ///     ID: {f383bf09-2ec9-4fe5-aa50-f3b14898c976}
    ///     Alias: DialogCardStoreModes
    ///     Group: System
    /// </summary>
    public sealed class DialogCardStoreModesSchemeInfo
    {
        private const string name = "DialogCardStoreModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class StoreIntoInfo
            {
                    public const int ID = 0;
                    public const string Name = "$DialogCardStoreModes_StoreIntoInfo";
            }
            public static class StoreIntoSettings
            {
                    public const int ID = 1;
                    public const string Name = "$DialogCardStoreModes_StoreIntoSettings";
            }
            public static class StoreAsCard
            {
                    public const int ID = 2;
                    public const string Name = "$DialogCardStoreModes_StoreAsCard";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(DialogCardStoreModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region DialogCardStoreModes Enumeration

    public sealed class DialogCardStoreModes
    {
        public readonly int ID;
        public readonly string Name;

        public DialogCardStoreModes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region DialogRoles

    /// <summary>
    ///     ID: {125ad61a-3698-4d07-9fa0-139c9cc25074}
    ///     Alias: DialogRoles
    ///     Group: System
    /// </summary>
    public sealed class DialogRolesSchemeInfo
    {
        private const string name = "DialogRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(DialogRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Dialogs

    /// <summary>
    ///     ID: {53a54dce-29d9-4f2c-8522-73ca60a4dbb5}
    ///     Alias: Dialogs
    ///     Group: System
    ///     Description: Виртуальная таблица для диалогов в системных карточках.
    /// </summary>
    public sealed class DialogsSchemeInfo
    {
        private const string name = "Dialogs";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CardCount = nameof(CardCount);
        public readonly string ChangePartner = nameof(ChangePartner);
        public readonly string ChangeAuthor = nameof(ChangeAuthor);
        public readonly string Comment = nameof(Comment);
        public readonly string OldPassword = nameof(OldPassword);
        public readonly string Password = nameof(Password);
        public readonly string PasswordRepeat = nameof(PasswordRepeat);
        public readonly string AppID = nameof(AppID);
        public readonly string AppName = nameof(AppName);
        public readonly string AppVersion = nameof(AppVersion);

        #endregion

        #region ToString

        public static implicit operator string(DialogsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DocLoadBarcodeRead

    /// <summary>
    ///     ID: {3d7fe6dc-f80f-4399-83aa-261e4624aaf1}
    ///     Alias: DocLoadBarcodeRead
    ///     Group: System
    /// </summary>
    public sealed class DocLoadBarcodeReadSchemeInfo
    {
        private const string name = "DocLoadBarcodeRead";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string BarcodeID = nameof(BarcodeID);
        public readonly string BarcodeName = nameof(BarcodeName);

        #endregion

        #region ToString

        public static implicit operator string(DocLoadBarcodeReadSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DocLoadSettings

    /// <summary>
    ///     ID: {0dbef4e9-7bf7-4b8f-aab0-fa908bc30e6f}
    ///     Alias: DocLoadSettings
    ///     Group: System
    /// </summary>
    public sealed class DocLoadSettingsSchemeInfo
    {
        private const string name = "DocLoadSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string InputPath = nameof(InputPath);
        public readonly string OutputPath = nameof(OutputPath);
        public readonly string ErrorPath = nameof(ErrorPath);
        public readonly string ExcludeBarcodePage = nameof(ExcludeBarcodePage);
        public readonly string DefaultBarcodeTableID = nameof(DefaultBarcodeTableID);
        public readonly string DefaultBarcodeTableName = nameof(DefaultBarcodeTableName);
        public readonly string DefaultBarcodeFieldID = nameof(DefaultBarcodeFieldID);
        public readonly string DefaultBarcodeFieldName = nameof(DefaultBarcodeFieldName);
        public readonly string BarcodeWriteID = nameof(BarcodeWriteID);
        public readonly string BarcodeWriteName = nameof(BarcodeWriteName);
        public readonly string ForceRender = nameof(ForceRender);
        public readonly string BarcodeFormat = nameof(BarcodeFormat);
        public readonly string BarcodeSequence = nameof(BarcodeSequence);
        public readonly string DocFormatName = nameof(DocFormatName);
        public readonly string BarcodeLabel = nameof(BarcodeLabel);
        public readonly string BarcodeWidth = nameof(BarcodeWidth);
        public readonly string BarcodeHeight = nameof(BarcodeHeight);
        public readonly string IsEnabled = nameof(IsEnabled);
        public readonly string ShowHeader = nameof(ShowHeader);
        public readonly string OffsetWidth = nameof(OffsetWidth);
        public readonly string OffsetHeight = nameof(OffsetHeight);
        public readonly string StartScale = nameof(StartScale);
        public readonly string StopScale = nameof(StopScale);
        public readonly string IncrementScale = nameof(IncrementScale);
        public readonly string SessionUserID = nameof(SessionUserID);
        public readonly string SessionUserName = nameof(SessionUserName);

        #endregion

        #region ToString

        public static implicit operator string(DocLoadSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DocumentCategories

    /// <summary>
    ///     ID: {f939aa52-dc1a-40b2-af4a-cb2757e8390a}
    ///     Alias: DocumentCategories
    ///     Group: Common
    ///     Description: Категории документов для Протоколов, СЗ и Документа (бывшие типы протоколов)
    /// </summary>
    public sealed class DocumentCategoriesSchemeInfo
    {
        private const string name = "DocumentCategories";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(DocumentCategoriesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DocumentCommonInfo

    /// <summary>
    ///     ID: {a161e289-2f99-4699-9e95-6e3336be8527}
    ///     Alias: DocumentCommonInfo
    ///     Group: Common
    ///     Description: Общая секция для всех видов документов.
    ///                  Должна использоваться для всех типов карточек, использующих такие поля, как номер, контрагент, тема и т.п.
    ///                  Помимо всего прочего, активно используется в поиске и представлениях.
    /// </summary>
    public sealed class DocumentCommonInfoSchemeInfo
    {
        private const string name = "DocumentCommonInfo";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CardTypeID = nameof(CardTypeID);
        public readonly string DocTypeID = nameof(DocTypeID);
        public readonly string DocTypeTitle = nameof(DocTypeTitle);
        public readonly string Number = nameof(Number);
        public readonly string FullNumber = nameof(FullNumber);
        public readonly string Sequence = nameof(Sequence);
        public readonly string SecondaryNumber = nameof(SecondaryNumber);
        public readonly string SecondaryFullNumber = nameof(SecondaryFullNumber);
        public readonly string SecondarySequence = nameof(SecondarySequence);
        public readonly string Subject = nameof(Subject);
        public readonly string DocDate = nameof(DocDate);
        public readonly string CreationDate = nameof(CreationDate);
        public readonly string OutgoingNumber = nameof(OutgoingNumber);
        public readonly string Amount = nameof(Amount);
        public readonly string Barcode = nameof(Barcode);
        public readonly string CurrencyID = nameof(CurrencyID);
        public readonly string CurrencyName = nameof(CurrencyName);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string RegistratorID = nameof(RegistratorID);
        public readonly string RegistratorName = nameof(RegistratorName);
        public readonly string SignedByID = nameof(SignedByID);
        public readonly string SignedByName = nameof(SignedByName);
        public readonly string DepartmentID = nameof(DepartmentID);
        public readonly string DepartmentName = nameof(DepartmentName);
        public readonly string PartnerID = nameof(PartnerID);
        public readonly string PartnerName = nameof(PartnerName);
        public readonly string RefDocID = nameof(RefDocID);
        public readonly string RefDocDescription = nameof(RefDocDescription);
        public readonly string ReceiverRowID = nameof(ReceiverRowID);
        public readonly string ReceiverName = nameof(ReceiverName);
        public readonly string CategoryID = nameof(CategoryID);
        public readonly string CategoryName = nameof(CategoryName);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);

        #endregion

        #region ToString

        public static implicit operator string(DocumentCommonInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region DynamicRoles

    /// <summary>
    ///     ID: {4a282d48-6d78-4923-85e4-8d3f9be213fa}
    ///     Alias: DynamicRoles
    ///     Group: Roles
    ///     Description: Динамические роли.
    /// </summary>
    public sealed class DynamicRolesSchemeInfo
    {
        private const string name = "DynamicRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string SqlText = nameof(SqlText);
        public readonly string SchedulingTypeID = nameof(SchedulingTypeID);
        public readonly string CronScheduling = nameof(CronScheduling);
        public readonly string PeriodScheduling = nameof(PeriodScheduling);
        public readonly string LastErrorDate = nameof(LastErrorDate);
        public readonly string LastErrorText = nameof(LastErrorText);
        public readonly string LastSuccessfulRecalcDate = nameof(LastSuccessfulRecalcDate);
        public readonly string ScheduleAtLaunch = nameof(ScheduleAtLaunch);

        #endregion

        #region ToString

        public static implicit operator string(DynamicRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Errors

    /// <summary>
    ///     ID: {754008b7-831b-44f9-9c58-99fa0334e62f}
    ///     Alias: Errors
    ///     Group: System
    /// </summary>
    public sealed class ErrorsSchemeInfo
    {
        private const string name = "Errors";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ActionID = nameof(ActionID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string CardID = nameof(CardID);
        public readonly string CardDigest = nameof(CardDigest);
        public readonly string Request = nameof(Request);
        public readonly string Category = nameof(Category);
        public readonly string Text = nameof(Text);
        public readonly string Modified = nameof(Modified);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);

        #endregion

        #region ToString

        public static implicit operator string(ErrorsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FieldChangedCondition

    /// <summary>
    ///     ID: {06245b07-be2a-40de-aec8-bfd367860930}
    ///     Alias: FieldChangedCondition
    ///     Group: Acl
    ///     Description: Секция для условийпроверяющих изменение поля.
    /// </summary>
    public sealed class FieldChangedConditionSchemeInfo
    {
        private const string name = "FieldChangedCondition";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string FieldID = nameof(FieldID);
        public readonly string FieldName = nameof(FieldName);

        #endregion

        #region ToString

        public static implicit operator string(FieldChangedConditionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileCategories

    /// <summary>
    ///     ID: {e1599715-02d4-4ca9-b63e-b4b1ce642c7a}
    ///     Alias: FileCategories
    ///     Group: System
    /// </summary>
    public sealed class FileCategoriesSchemeInfo
    {
        private const string name = "FileCategories";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region ToString

        public static implicit operator string(FileCategoriesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileContent

    /// <summary>
    ///     ID: {328af88c-b21a-4c2a-b825-45a086d0b24b}
    ///     Alias: FileContent
    ///     Group: System
    ///     Description: Контент файлов.
    /// </summary>
    public sealed class FileContentSchemeInfo
    {
        private const string name = "FileContent";

        #region Columns

        public readonly string VersionRowID = nameof(VersionRowID);
        public readonly string Content = nameof(Content);
        public readonly string Ext = nameof(Ext);

        #endregion

        #region ToString

        public static implicit operator string(FileContentSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileConverterCache

    /// <summary>
    ///     ID: {b376adb4-3134-4ec5-9597-a83e8c9db0f1}
    ///     Alias: FileConverterCache
    ///     Group: System
    ///     Description: Информация по сконвертированным файлам, добавленным в кэш.
    ///                  Идентификатор RowID равен идентификатору файла в кэше Files.RowID.
    /// </summary>
    public sealed class FileConverterCacheSchemeInfo
    {
        private const string name = "FileConverterCache";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string VersionID = nameof(VersionID);
        public readonly string RequestHash = nameof(RequestHash);
        public readonly string ResponseInfo = nameof(ResponseInfo);
        public readonly string LastAccessTime = nameof(LastAccessTime);

        #endregion

        #region ToString

        public static implicit operator string(FileConverterCacheSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileConverterCacheVirtual

    /// <summary>
    ///     ID: {961887ca-e67c-4283-89fc-265dbf17e4c1}
    ///     Alias: FileConverterCacheVirtual
    ///     Group: System
    ///     Description: Информация, отображаемая в карточке файловых конвертеров.
    /// </summary>
    public sealed class FileConverterCacheVirtualSchemeInfo
    {
        private const string name = "FileConverterCacheVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string FileCount = nameof(FileCount);
        public readonly string FileCountText = nameof(FileCountText);
        public readonly string OldestFileAccessTime = nameof(OldestFileAccessTime);
        public readonly string NewestFileAccessTime = nameof(NewestFileAccessTime);

        #endregion

        #region ToString

        public static implicit operator string(FileConverterCacheVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileConverterTypes

    /// <summary>
    ///     ID: {a1dd7426-13e0-42fb-a45a-0a714108e274}
    ///     Alias: FileConverterTypes
    ///     Group: System
    ///     Description: Варианты конвертеров файлов из формата в формат
    /// </summary>
    public sealed class FileConverterTypesSchemeInfo
    {
        private const string name = "FileConverterTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class None
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_FileConverterTypes_None";
            }
            public static class OpenLibre
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_FileConverterTypes_OpenLibre";
            }
            public static class OnlyOfficeService
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_FileConverterTypes_OnlyOfficeService";
            }
            public static class OnlyOfficeDocumentBuilder
            {
                    public const int ID = 3;
                    public const string Name = "$Enum_FileConverterTypes_OnlyOfficeDocumentBuilder";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(FileConverterTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region FileConverterTypes Enumeration

    public sealed class FileConverterTypes
    {
        public readonly int ID;
        public readonly string Name;

        public FileConverterTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region Files

    /// <summary>
    ///     ID: {dd716146-b177-4920-bc90-b1196b16347c}
    ///     Alias: Files
    ///     Group: System
    ///     Description: Файлы, приложенные к карточкам.
    /// </summary>
    public sealed class FilesSchemeInfo
    {
        private const string name = "Files";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);
        public readonly string TaskID = nameof(TaskID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string VersionRowID = nameof(VersionRowID);
        public readonly string VersionNumber = nameof(VersionNumber);
        public readonly string Created = nameof(Created);
        public readonly string CreatedByID = nameof(CreatedByID);
        public readonly string CreatedByName = nameof(CreatedByName);
        public readonly string Modified = nameof(Modified);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);
        public readonly string CategoryID = nameof(CategoryID);
        public readonly string CategoryCaption = nameof(CategoryCaption);
        public readonly string OriginalFileID = nameof(OriginalFileID);
        public readonly string OriginalVersionRowID = nameof(OriginalVersionRowID);
        public readonly string Options = nameof(Options);

        #endregion

        #region ToString

        public static implicit operator string(FilesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileSignatureEventTypes

    /// <summary>
    ///     ID: {5a8e7767-cd46-4ace-9da3-e3ea6f38cff2}
    ///     Alias: FileSignatureEventTypes
    ///     Group: System
    ///     Description: События, в результате которых подпись была добавлена в систему.
    /// </summary>
    public sealed class FileSignatureEventTypesSchemeInfo
    {
        private const string name = "FileSignatureEventTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Other
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_FileSignatureEventTypes_Other";
            }
            public static class Imported
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_FileSignatureEventTypes_Imported";
            }
            public static class Signed
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_FileSignatureEventTypes_Signed";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(FileSignatureEventTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region FileSignatureEventTypes Enumeration

    public sealed class FileSignatureEventTypes
    {
        public readonly int ID;
        public readonly string Name;

        public FileSignatureEventTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region FileSignatures

    /// <summary>
    ///     ID: {5f428478-eaf5-4180-bde9-499483c3f80c}
    ///     Alias: FileSignatures
    ///     Group: System
    ///     Description: Подписи файла.
    /// </summary>
    public sealed class FileSignaturesSchemeInfo
    {
        private const string name = "FileSignatures";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string VersionRowID = nameof(VersionRowID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string EventID = nameof(EventID);
        public readonly string Comment = nameof(Comment);
        public readonly string SubjectName = nameof(SubjectName);
        public readonly string Company = nameof(Company);
        public readonly string Signed = nameof(Signed);
        public readonly string SerialNumber = nameof(SerialNumber);
        public readonly string IssuerName = nameof(IssuerName);
        public readonly string Data = nameof(Data);
        public readonly string SignatureTypeID = nameof(SignatureTypeID);
        public readonly string SignatureProfileID = nameof(SignatureProfileID);

        #endregion

        #region ToString

        public static implicit operator string(FileSignaturesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileSources

    /// <summary>
    ///     ID: {e8300fe5-3b24-4c27-a45a-6cd8575bfcd5}
    ///     Alias: FileSources
    ///     Group: System
    ///     Description: Способы хранения файлов.
    /// </summary>
    public sealed class FileSourcesSchemeInfo
    {
        private const string name = "FileSources";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Path = nameof(Path);
        public readonly string IsDatabase = nameof(IsDatabase);
        public readonly string Description = nameof(Description);
        public readonly string Size = nameof(Size);
        public readonly string MaxSize = nameof(MaxSize);
        public readonly string FileExtensions = nameof(FileExtensions);

        #endregion

        #region ToString

        public static implicit operator string(FileSourcesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileSourcesVirtual

    /// <summary>
    ///     ID: {64bbc32d-95d6-434b-9c08-0288344d53bb}
    ///     Alias: FileSourcesVirtual
    ///     Group: System
    ///     Description: Способы хранения файлов. Виртуальная таблица, обеспечивающая редактирование таблицы FileSources через карточку настроек.
    ///                  Колонка ID в этой таблице соответствует идентификатору карточки настроек, а колонка SourceID - идентификатору источника файлов, т.е. аналог FileSources.ID.
    /// </summary>
    public sealed class FileSourcesVirtualSchemeInfo
    {
        private const string name = "FileSourcesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string IsDefault = nameof(IsDefault);
        public readonly string SourceID = nameof(SourceID);
        public readonly string SourceIDText = nameof(SourceIDText);
        public readonly string Name = nameof(Name);
        public readonly string IsDatabase = nameof(IsDatabase);
        public readonly string Path = nameof(Path);
        public readonly string Description = nameof(Description);
        public readonly string Size = nameof(Size);
        public readonly string MaxSize = nameof(MaxSize);
        public readonly string FileExtensions = nameof(FileExtensions);

        #endregion

        #region ToString

        public static implicit operator string(FileSourcesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileStates

    /// <summary>
    ///     ID: {de9ba182-3fc4-4f20-9060-fa83b74fd46c}
    ///     Alias: FileStates
    ///     Group: System
    ///     Description: Состояние версии файла.
    /// </summary>
    public sealed class FileStatesSchemeInfo
    {
        private const string name = "FileStates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Uploading
            {
                    public const int ID = 0;
                    public const string Name = "Uploading";
            }
            public static class Success
            {
                    public const int ID = 1;
                    public const string Name = "Success";
            }
            public static class Error
            {
                    public const int ID = 2;
                    public const string Name = "Error";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(FileStatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region FileStates Enumeration

    public sealed class FileStates
    {
        public readonly int ID;
        public readonly string Name;

        public FileStates(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region FileTemplateRoles

    /// <summary>
    ///     ID: {0eabfebc-fa8b-41b9-9aa3-6db9626a8ac6}
    ///     Alias: FileTemplateRoles
    ///     Group: System
    /// </summary>
    public sealed class FileTemplateRolesSchemeInfo
    {
        private const string name = "FileTemplateRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(FileTemplateRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileTemplates

    /// <summary>
    ///     ID: {98e0c3a9-0b9a-4fec-9843-4a077f6ff5f0}
    ///     Alias: FileTemplates
    ///     Group: System
    /// </summary>
    public sealed class FileTemplatesSchemeInfo
    {
        private const string name = "FileTemplates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string GroupName = nameof(GroupName);
        public readonly string PlaceholdersInfo = nameof(PlaceholdersInfo);
        public readonly string AliasMetadata = nameof(AliasMetadata);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeName = nameof(TypeName);
        public readonly string BeforeDocumentReplace = nameof(BeforeDocumentReplace);
        public readonly string BeforeTableReplace = nameof(BeforeTableReplace);
        public readonly string BeforeRowReplace = nameof(BeforeRowReplace);
        public readonly string BeforePlaceholderReplace = nameof(BeforePlaceholderReplace);
        public readonly string AfterPlaceholderReplace = nameof(AfterPlaceholderReplace);
        public readonly string AfterRowReplace = nameof(AfterRowReplace);
        public readonly string AfterTableReplace = nameof(AfterTableReplace);
        public readonly string AfterDocumentReplace = nameof(AfterDocumentReplace);
        public readonly string System = nameof(System);
        public readonly string ConvertToPDF = nameof(ConvertToPDF);

        #endregion

        #region ToString

        public static implicit operator string(FileTemplatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileTemplateTemplateTypes

    /// <summary>
    ///     ID: {54994b70-b619-4280-b9ff-31c20453a462}
    ///     Alias: FileTemplateTemplateTypes
    ///     Group: System
    /// </summary>
    public sealed class FileTemplateTemplateTypesSchemeInfo
    {
        private const string name = "FileTemplateTemplateTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Card
            {
                    public const int ID = 0;
                    public const string Name = "$FileTemplateType_Card";
            }
            public static class View
            {
                    public const int ID = 1;
                    public const string Name = "$FileTemplateType_View";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(FileTemplateTemplateTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region FileTemplateTemplateTypes Enumeration

    public sealed class FileTemplateTemplateTypes
    {
        public readonly int ID;
        public readonly string Name;

        public FileTemplateTemplateTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region FileTemplateTypes

    /// <summary>
    ///     ID: {628e0e44-564c-4107-b943-0ec1e378bae7}
    ///     Alias: FileTemplateTypes
    ///     Group: System
    /// </summary>
    public sealed class FileTemplateTypesSchemeInfo
    {
        private const string name = "FileTemplateTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(FileTemplateTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileTemplateViews

    /// <summary>
    ///     ID: {ebd081b9-aaf9-4bab-be51-602803756e8d}
    ///     Alias: FileTemplateViews
    ///     Group: System
    /// </summary>
    public sealed class FileTemplateViewsSchemeInfo
    {
        private const string name = "FileTemplateViews";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ViewID = nameof(ViewID);
        public readonly string ViewAlias = nameof(ViewAlias);

        #endregion

        #region ToString

        public static implicit operator string(FileTemplateViewsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FileVersions

    /// <summary>
    ///     ID: {e17fd270-5c61-49af-955d-ed6bb983f0d8}
    ///     Alias: FileVersions
    ///     Group: System
    ///     Description: Версии файла.
    /// </summary>
    public sealed class FileVersionsSchemeInfo
    {
        private const string name = "FileVersions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Number = nameof(Number);
        public readonly string Name = nameof(Name);
        public readonly string Size = nameof(Size);
        public readonly string Created = nameof(Created);
        public readonly string CreatedByID = nameof(CreatedByID);
        public readonly string CreatedByName = nameof(CreatedByName);
        public readonly string SourceID = nameof(SourceID);
        public readonly string StateID = nameof(StateID);
        public readonly string ErrorDate = nameof(ErrorDate);
        public readonly string ErrorMessage = nameof(ErrorMessage);
        public readonly string Hash = nameof(Hash);
        public readonly string Options = nameof(Options);
        public readonly string LinkID = nameof(LinkID);
        public readonly string Tags = nameof(Tags);

        #endregion

        #region ToString

        public static implicit operator string(FileVersionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FmAttachments

    /// <summary>
    ///     ID: {3f903804-3c70-4828-9887-5c9268d20b7d}
    ///     Alias: FmAttachments
    ///     Group: Fm
    ///     Description: Таблица с прикрепленными элементами
    /// </summary>
    public sealed class FmAttachmentsSchemeInfo
    {
        private const string name = "FmAttachments";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Uri = nameof(Uri);
        public readonly string Caption = nameof(Caption);
        public readonly string TypeID = nameof(TypeID);
        public readonly string MessageRowID = nameof(MessageRowID);
        public readonly string FileSize = nameof(FileSize);
        public readonly string OriginalFileID = nameof(OriginalFileID);
        public readonly string ShowInToolbar = nameof(ShowInToolbar);

        #endregion

        #region ToString

        public static implicit operator string(FmAttachmentsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FmAttachmentTypes

    /// <summary>
    ///     ID: {74caae68-ee60-4d36-b6af-b81bdd06d4a3}
    ///     Alias: FmAttachmentTypes
    ///     Group: Fm
    ///     Description: Типы прикрепленных элементов (файлы, ссылки и пр)
    /// </summary>
    public sealed class FmAttachmentTypesSchemeInfo
    {
        private const string name = "FmAttachmentTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class File
            {
                    public const int ID = 0;
                    public const string Name = "File";
            }
            public static class Link
            {
                    public const int ID = 1;
                    public const string Name = "Link";
            }
            public static class InnerItem
            {
                    public const int ID = 2;
                    public const string Name = "InnerItem";
            }
            public static class ExternalInnerItem
            {
                    public const int ID = 3;
                    public const string Name = "ExternalInnerItem";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(FmAttachmentTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region FmAttachmentTypes Enumeration

    public sealed class FmAttachmentTypes
    {
        public readonly int ID;
        public readonly string Name;

        public FmAttachmentTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region FmMessages

    /// <summary>
    ///     ID: {a03f6c5d-e719-43d6-bcc5-d2ea321765ab}
    ///     Alias: FmMessages
    ///     Group: Fm
    ///     Description: Таблица для хранения сообщений
    /// </summary>
    public sealed class FmMessagesSchemeInfo
    {
        private const string name = "FmMessages";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Body = nameof(Body);
        public readonly string Created = nameof(Created);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string TopicRowID = nameof(TopicRowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);
        public readonly string ModifiedAt = nameof(ModifiedAt);
        public readonly string PlainText = nameof(PlainText);

        #endregion

        #region ToString

        public static implicit operator string(FmMessagesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FmMessagesPluginTable

    /// <summary>
    ///     ID: {18b094ec-a87f-4ccb-bfe8-a5936cc38992}
    ///     Alias: FmMessagesPluginTable
    ///     Group: Fm
    ///     Description: Таблица со временем последней даты запуска плагина для email рассылки сообщений форумов  
    /// </summary>
    public sealed class FmMessagesPluginTableSchemeInfo
    {
        private const string name = "FmMessagesPluginTable";

        #region Columns

        public readonly string LastPluginRunDate = nameof(LastPluginRunDate);

        #endregion

        #region ToString

        public static implicit operator string(FmMessagesPluginTableSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FmMessageTypes

    /// <summary>
    ///     ID: {43f92881-c875-437a-bf1c-b7793c099d00}
    ///     Alias: FmMessageTypes
    ///     Group: Fm
    ///     Description: Типы сообщений
    /// </summary>
    public sealed class FmMessageTypesSchemeInfo
    {
        private const string name = "FmMessageTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Default
            {
                    public const int ID = 0;
                    public const string Name = "Default";
            }
            public static class AddUser
            {
                    public const int ID = 1;
                    public const string Name = "AddUser";
            }
            public static class RemoveUser
            {
                    public const int ID = 2;
                    public const string Name = "RemoveUser";
            }
            public static class AddRoles
            {
                    public const int ID = 3;
                    public const string Name = "AddRoles";
            }
            public static class RemoveRoles
            {
                    public const int ID = 4;
                    public const string Name = "RemoveRoles";
            }
            public static class Custom
            {
                    public const int ID = 5;
                    public const string Name = "Custom";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(FmMessageTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region FmMessageTypes Enumeration

    public sealed class FmMessageTypes
    {
        public readonly int ID;
        public readonly string Name;

        public FmMessageTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region FmNotifications

    /// <summary>
    ///     ID: {fe822963-6091-4f70-9fbe-167aba72b4a2}
    ///     Alias: FmNotifications
    ///     Group: Fm
    ///     Description: Таблица для хранения уведомления
    /// </summary>
    public sealed class FmNotificationsSchemeInfo
    {
        private const string name = "FmNotifications";

        #region Columns

        public readonly string UserID = nameof(UserID);
        public readonly string Batch0 = nameof(Batch0);
        public readonly string Batch1 = nameof(Batch1);
        public readonly string Count0 = nameof(Count0);
        public readonly string Count1 = nameof(Count1);
        public readonly string ReadMessages0 = nameof(ReadMessages0);
        public readonly string ReadMessages1 = nameof(ReadMessages1);
        public readonly string ActiveBatch = nameof(ActiveBatch);

        #endregion

        #region ToString

        public static implicit operator string(FmNotificationsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FmParticipantTypes

    /// <summary>
    ///     ID: {2b2a8e44-eecd-4afe-b017-20f8a00846ff}
    ///     Alias: FmParticipantTypes
    ///     Group: Fm
    ///     Description: Типы участников форума
    /// </summary>
    public sealed class FmParticipantTypesSchemeInfo
    {
        private const string name = "FmParticipantTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Participant
            {
                    public const int ID = 0;
                    public const string Name = "$FmParticipantTypes_Participant";
            }
            public static class Moderator
            {
                    public const int ID = 1;
                    public const string Name = "$FmParticipantTypes_Moderator";
            }
            public static class SuperModerator
            {
                    public const int ID = 2;
                    public const string Name = "SuperModerator";
            }
            public static class ParticipantFromRole
            {
                    public const int ID = 3;
                    public const string Name = "$FmParticipantTypes_ParticipantFromRole";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(FmParticipantTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region FmParticipantTypes Enumeration

    public sealed class FmParticipantTypes
    {
        public readonly int ID;
        public readonly string Name;

        public FmParticipantTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region FmTopicParticipantRoles

    /// <summary>
    ///     ID: {ecd6e90e-3bbe-4c24-975b-6644b20efe7f}
    ///     Alias: FmTopicParticipantRoles
    ///     Group: Fm
    ///     Description: Таблица с ролями - учасниками 
    /// </summary>
    public sealed class FmTopicParticipantRolesSchemeInfo
    {
        private const string name = "FmTopicParticipantRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TopicRowID = nameof(TopicRowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string ReadOnly = nameof(ReadOnly);
        public readonly string Subscribed = nameof(Subscribed);
        public readonly string InvitingUserID = nameof(InvitingUserID);
        public readonly string InvitingUserName = nameof(InvitingUserName);

        #endregion

        #region ToString

        public static implicit operator string(FmTopicParticipantRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FmTopicParticipantRolesUnsubscribed

    /// <summary>
    ///     ID: {e9fd155c-b189-4a5d-b0b4-970c94a2fa0a}
    ///     Alias: FmTopicParticipantRolesUnsubscribed
    ///     Group: Fm
    ///     Description: Таблица в которой хранятся данные по одписакам пользователями в ролях
    /// </summary>
    public sealed class FmTopicParticipantRolesUnsubscribedSchemeInfo
    {
        private const string name = "FmTopicParticipantRolesUnsubscribed";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TopicRowID = nameof(TopicRowID);
        public readonly string UserID = nameof(UserID);
        public readonly string Subscribe = nameof(Subscribe);

        #endregion

        #region ToString

        public static implicit operator string(FmTopicParticipantRolesUnsubscribedSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FmTopicParticipants

    /// <summary>
    ///     ID: {b8150fdd-b439-4eaa-9665-9a8b9ee774f0}
    ///     Alias: FmTopicParticipants
    ///     Group: Fm
    ///     Description: Участники топика
    /// </summary>
    public sealed class FmTopicParticipantsSchemeInfo
    {
        private const string name = "FmTopicParticipants";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TopicRowID = nameof(TopicRowID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string ReadOnly = nameof(ReadOnly);
        public readonly string Subscribed = nameof(Subscribed);
        public readonly string TypeID = nameof(TypeID);
        public readonly string InvitingUserID = nameof(InvitingUserID);
        public readonly string InvitingUserName = nameof(InvitingUserName);

        #endregion

        #region ToString

        public static implicit operator string(FmTopicParticipantsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FmTopics

    /// <summary>
    ///     ID: {35b11a3c-f9ec-4fac-a3f1-def11bba44ae}
    ///     Alias: FmTopics
    ///     Group: Fm
    ///     Description: Таблица для хранения топиков
    /// </summary>
    public sealed class FmTopicsSchemeInfo
    {
        private const string name = "FmTopics";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Description = nameof(Description);
        public readonly string Title = nameof(Title);
        public readonly string Created = nameof(Created);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string IsArchived = nameof(IsArchived);
        public readonly string LastMessageTime = nameof(LastMessageTime);
        public readonly string LastMessageAuthorID = nameof(LastMessageAuthorID);
        public readonly string LastMessageAuthorName = nameof(LastMessageAuthorName);
        public readonly string TypeID = nameof(TypeID);

        #endregion

        #region ToString

        public static implicit operator string(FmTopicsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FmTopicTypes

    /// <summary>
    ///     ID: {c0645587-3584-4b23-867f-54071abfa5a1}
    ///     Alias: FmTopicTypes
    ///     Group: Fm
    ///     Description: Типы топиков
    /// </summary>
    public sealed class FmTopicTypesSchemeInfo
    {
        private const string name = "FmTopicTypes";

        #region Columns

        public readonly string Name = nameof(Name);
        public readonly string ID = nameof(ID);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Default
            {
                    public const string Name = "Default";
                    public static Guid ID = new Guid(0x680d0d81,0xd8f3,0x485e,0x90,0x58,0xe1,0x7a,0xb9,0xe1,0x86,0xe0);
            }
            public static class Private
            {
                    public const string Name = "Private";
                    public static Guid ID = new Guid(0xe7d45adf,0x90d0,0x4fcf,0x91,0x90,0xe8,0x6c,0x92,0xd6,0x58,0x97);
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(FmTopicTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region FmTopicTypes Enumeration

    public sealed class FmTopicTypes
    {
        public readonly string Name;
        public readonly Guid ID;

        public FmTopicTypes(string Name, Guid ID)
        {
            this.Name = Name;
            this.ID = ID;
        }
    }

    #endregion

    #endregion

    #region FmUserSettingsVirtual

    /// <summary>
    ///     ID: {e8fe8b2a-428d-44b6-8328-ee2a7bb4d323}
    ///     Alias: FmUserSettingsVirtual
    ///     Group: Fm
    ///     Description: Виртуальная таблица для формы с настройками
    /// </summary>
    public sealed class FmUserSettingsVirtualSchemeInfo
    {
        private const string name = "FmUserSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string IsNotShowMsgIndicatorOnStartup = nameof(IsNotShowMsgIndicatorOnStartup);
        public readonly string EnableMessageIndicator = nameof(EnableMessageIndicator);

        #endregion

        #region ToString

        public static implicit operator string(FmUserSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FmUserStat

    /// <summary>
    ///     ID: {d10d18eb-d803-4151-8a60-8bfd262d2800}
    ///     Alias: FmUserStat
    ///     Group: Fm
    ///     Description: Таблица, в который храним дату посещения пользователем топика
    /// </summary>
    public sealed class FmUserStatSchemeInfo
    {
        private const string name = "FmUserStat";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TopicRowID = nameof(TopicRowID);
        public readonly string UserID = nameof(UserID);
        public readonly string LastReadMessageTime = nameof(LastReadMessageTime);

        #endregion

        #region ToString

        public static implicit operator string(FmUserStatSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ForegroundColors

    /// <summary>
    ///     ID: {f22e70d5-17da-4e6a-8d41-17796e5f75d0}
    ///     Alias: ForegroundColors
    ///     Group: System
    /// </summary>
    public sealed class ForegroundColorsSchemeInfo
    {
        private const string name = "ForegroundColors";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Color1 = nameof(Color1);
        public readonly string Color2 = nameof(Color2);
        public readonly string Color3 = nameof(Color3);
        public readonly string Color4 = nameof(Color4);
        public readonly string Color5 = nameof(Color5);

        #endregion

        #region ToString

        public static implicit operator string(ForegroundColorsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FormatSettings

    /// <summary>
    ///     ID: {a96047e7-3b08-42bd-8455-1032520a608f}
    ///     Alias: FormatSettings
    ///     Group: System
    ///     Description: Секция карточки с настройками форматирования.
    /// </summary>
    public sealed class FormatSettingsSchemeInfo
    {
        private const string name = "FormatSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);
        public readonly string DateFormatID = nameof(DateFormatID);
        public readonly string DateFormatCaption = nameof(DateFormatCaption);
        public readonly string DateSeparator = nameof(DateSeparator);
        public readonly string DaysWithLeadingZero = nameof(DaysWithLeadingZero);
        public readonly string MonthsWithLeadingZero = nameof(MonthsWithLeadingZero);
        public readonly string HoursWithLeadingZero = nameof(HoursWithLeadingZero);
        public readonly string Time24Hour = nameof(Time24Hour);
        public readonly string TimeSeparator = nameof(TimeSeparator);
        public readonly string TimeAmDesignator = nameof(TimeAmDesignator);
        public readonly string TimePmDesignator = nameof(TimePmDesignator);
        public readonly string NumberGroupSeparator = nameof(NumberGroupSeparator);
        public readonly string NumberDecimalSeparator = nameof(NumberDecimalSeparator);

        #endregion

        #region ToString

        public static implicit operator string(FormatSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region FunctionRoles

    /// <summary>
    ///     ID: {a59078ce-8acf-4c45-a49a-503fa88a0580}
    ///     Alias: FunctionRoles
    ///     Group: System
    ///     Description: Функциональные роли заданий, такие как \"автор\", \"исполнитель\", \"контролёр\" и др.
    /// </summary>
    public sealed class FunctionRolesSchemeInfo
    {
        private const string name = "FunctionRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);
        public readonly string CanBeDeputy = nameof(CanBeDeputy);
        public readonly string CanTakeInProgress = nameof(CanTakeInProgress);
        public readonly string HideTaskByDefault = nameof(HideTaskByDefault);
        public readonly string CanChangeTaskInfo = nameof(CanChangeTaskInfo);
        public readonly string CanChangeTaskRoles = nameof(CanChangeTaskRoles);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Author
            {
                    public static Guid ID = new Guid(0x6bc228a0,0xe5a2,0x4f15,0xbf,0x6d,0xc8,0xe7,0x44,0x53,0x32,0x41);
                    public const string Name = "Author";
                    public const string Caption = "$Enum_FunctionRoles_Author";
                    public const bool CanBeDeputy = true;
                    public const bool CanTakeInProgress = false;
                    public const bool HideTaskByDefault = true;
                    public const bool CanChangeTaskInfo = true;
                    public const bool CanChangeTaskRoles = false;
            }
            public static class Performer
            {
                    public static Guid ID = new Guid(0xf726ab6c,0xa279,0x4d79,0x86,0x3a,0x47,0x25,0x3e,0x55,0xcc,0xc1);
                    public const string Name = "Performer";
                    public const string Caption = "$Enum_FunctionRoles_Performer";
                    public const bool CanBeDeputy = true;
                    public const bool CanTakeInProgress = true;
                    public const bool HideTaskByDefault = false;
                    public const bool CanChangeTaskInfo = false;
                    public const bool CanChangeTaskRoles = false;
            }
            public static class Sender
            {
                    public static Guid ID = new Guid(0xd75c4fb4,0x50b9,0x4f9e,0x86,0x51,0xeb,0x6c,0x9d,0xe8,0xa8,0x47);
                    public const string Name = "Sender";
                    public const string Caption = "$Enum_FunctionRoles_Sender";
                    public const bool CanBeDeputy = true;
                    public const bool CanTakeInProgress = false;
                    public const bool HideTaskByDefault = true;
                    public const bool CanChangeTaskInfo = true;
                    public const bool CanChangeTaskRoles = false;
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(FunctionRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region FunctionRoles Enumeration

    public sealed class FunctionRoles
    {
        public readonly Guid ID;
        public readonly string Name;
        public readonly string Caption;
        public readonly bool CanBeDeputy;
        public readonly bool CanTakeInProgress;
        public readonly bool HideTaskByDefault;
        public readonly bool CanChangeTaskInfo;
        public readonly bool CanChangeTaskRoles;

        public FunctionRoles(Guid ID, string Name, string Caption, bool CanBeDeputy, bool CanTakeInProgress, bool HideTaskByDefault, bool CanChangeTaskInfo, bool CanChangeTaskRoles)
        {
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

    #endregion

    #endregion

    #region FunctionRolesVirtual

    /// <summary>
    ///     ID: {ef4bbb91-4d48-4c68-9e05-34ab4d5c2b36}
    ///     Alias: FunctionRolesVirtual
    ///     Group: System
    ///     Description: Виртуальная карточка для функциональной роли.
    /// </summary>
    public sealed class FunctionRolesVirtualSchemeInfo
    {
        private const string name = "FunctionRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string FunctionRoleID = nameof(FunctionRoleID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);
        public readonly string CanBeDeputy = nameof(CanBeDeputy);
        public readonly string PartitionID = nameof(PartitionID);
        public readonly string PartitionName = nameof(PartitionName);
        public readonly string CanTakeInProgress = nameof(CanTakeInProgress);
        public readonly string HideTaskByDefault = nameof(HideTaskByDefault);
        public readonly string CanChangeTaskInfo = nameof(CanChangeTaskInfo);
        public readonly string CanChangeTaskRoles = nameof(CanChangeTaskRoles);

        #endregion

        #region ToString

        public static implicit operator string(FunctionRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Functions

    /// <summary>
    ///     ID: {57e45ca3-5036-4268-b8f9-86c4933a4d2d}
    ///     Alias: Functions
    ///     Group: System
    ///     Description: Contains metadata that describes functions which used by Tessa
    /// </summary>
    public sealed class FunctionsSchemeInfo
    {
        private const string name = "Functions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Definition = nameof(Definition);

        #endregion

        #region Functions

        public const string CalendarAddWorkingDaysToDate = nameof(CalendarAddWorkingDaysToDate);
        public const string CalendarAddWorkingDaysToDateExact = nameof(CalendarAddWorkingDaysToDateExact);
        public const string CalendarAddWorkQuants = nameof(CalendarAddWorkQuants);
        public const string CalendarGetDateDiff = nameof(CalendarGetDateDiff);
        public const string CalendarGetDayOfWeek = nameof(CalendarGetDayOfWeek);
        public const string CalendarGetFirstQuantStart = nameof(CalendarGetFirstQuantStart);
        public const string CalendarGetLastQuantEnd = nameof(CalendarGetLastQuantEnd);
        public const string CalendarGetPlannedByWorkingDays = nameof(CalendarGetPlannedByWorkingDays);
        public const string CalendarIsWorkTime = nameof(CalendarIsWorkTime);
        public const string DropFunction = nameof(DropFunction);
        public const string FormatAmount = nameof(FormatAmount);
        public const string GetAggregateRoleUsers = nameof(GetAggregateRoleUsers);
        public const string GetString = nameof(GetString);
        public const string GetTimeIntervalLiteral = nameof(GetTimeIntervalLiteral);
        public const string Localization = nameof(Localization);
        public const string Localize = nameof(Localize);

        #endregion

        #region ToString

        public static implicit operator string(FunctionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region HelpSections

    /// <summary>
    ///     ID: {741301fd-f38a-4cca-bab9-df1328d53b53}
    ///     Alias: HelpSections
    ///     Group: System
    ///     Description: Разделы справки.
    /// </summary>
    public sealed class HelpSectionsSchemeInfo
    {
        private const string name = "HelpSections";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Code = nameof(Code);
        public readonly string Name = nameof(Name);
        public readonly string RichText = nameof(RichText);
        public readonly string PlainText = nameof(PlainText);

        #endregion

        #region ToString

        public static implicit operator string(HelpSectionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region IncomingRefDocs

    /// <summary>
    ///     ID: {83785076-d844-4ea4-9e84-0a389c951ef4}
    ///     Alias: IncomingRefDocs
    ///     Group: Common
    /// </summary>
    public sealed class IncomingRefDocsSchemeInfo
    {
        private const string name = "IncomingRefDocs";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string DocID = nameof(DocID);
        public readonly string DocDescription = nameof(DocDescription);

        #endregion

        #region ToString

        public static implicit operator string(IncomingRefDocsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Instances

    /// <summary>
    ///     ID: {1074eadd-21d7-4925-98c8-40d1e5f0ca0e}
    ///     Alias: Instances
    ///     Group: System
    ///     Description: Contains system info of cards
    /// </summary>
    public sealed class InstancesSchemeInfo
    {
        private const string name = "Instances";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string Created = nameof(Created);
        public readonly string CreatedByID = nameof(CreatedByID);
        public readonly string CreatedByName = nameof(CreatedByName);
        public readonly string Modified = nameof(Modified);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);
        public readonly string Version = nameof(Version);

        #endregion

        #region ToString

        public static implicit operator string(InstancesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region InstanceTypes

    /// <summary>
    ///     ID: {2a567cee-1489-4a90-acf5-4f6d2c5bd67e}
    ///     Alias: InstanceTypes
    ///     Group: System
    ///     Description: Instance types.
    /// </summary>
    public sealed class InstanceTypesSchemeInfo
    {
        private const string name = "InstanceTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Card
            {
                    public const int ID = 0;
                    public const string Name = "Card";
            }
            public static class File
            {
                    public const int ID = 1;
                    public const string Name = "File";
            }
            public static class Task
            {
                    public const int ID = 2;
                    public const string Name = "Task";
            }
            public static class Dialog
            {
                    public const int ID = 3;
                    public const string Name = "Dialog";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(InstanceTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region InstanceTypes Enumeration

    public sealed class InstanceTypes
    {
        public readonly int ID;
        public readonly string Name;

        public InstanceTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrAcquaintanceAction

    /// <summary>
    ///     ID: {2d90b630-c611-4137-8094-18986416c7b9}
    ///     Alias: KrAcquaintanceAction
    ///     Group: KrWe
    ///     Description: Основная секция для действия \"Ознакомление\"
    /// </summary>
    public sealed class KrAcquaintanceActionSchemeInfo
    {
        private const string name = "KrAcquaintanceAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string Comment = nameof(Comment);
        public readonly string AliasMetadata = nameof(AliasMetadata);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string SenderID = nameof(SenderID);
        public readonly string SenderName = nameof(SenderName);

        #endregion

        #region ToString

        public static implicit operator string(KrAcquaintanceActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAcquaintanceActionRoles

    /// <summary>
    ///     ID: {4c90a850-8ea9-4b07-8c8e-96145f624a3a}
    ///     Alias: KrAcquaintanceActionRoles
    ///     Group: KrWe
    ///     Description: Список ролей для действия \"Ознакомление\"
    /// </summary>
    public sealed class KrAcquaintanceActionRolesSchemeInfo
    {
        private const string name = "KrAcquaintanceActionRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(KrAcquaintanceActionRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAcquaintanceSettingsVirtual

    /// <summary>
    ///     ID: {61a4ec06-f583-4eaf-8d91-c73de9f61164}
    ///     Alias: KrAcquaintanceSettingsVirtual
    ///     Group: KrStageTypes
    ///     Description: Секция настроек этапа Ознакомление
    /// </summary>
    public sealed class KrAcquaintanceSettingsVirtualSchemeInfo
    {
        private const string name = "KrAcquaintanceSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string Comment = nameof(Comment);
        public readonly string AliasMetadata = nameof(AliasMetadata);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string SenderID = nameof(SenderID);
        public readonly string SenderName = nameof(SenderName);

        #endregion

        #region ToString

        public static implicit operator string(KrAcquaintanceSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrActionTypes

    /// <summary>
    ///     ID: {b401e639-9167-4ada-9d46-4982bcd92488}
    ///     Alias: KrActionTypes
    ///     Group: Kr
    /// </summary>
    public sealed class KrActionTypesSchemeInfo
    {
        private const string name = "KrActionTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string EventType = nameof(EventType);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class NewCard
            {
                    public const int ID = 0;
                    public const string Name = "$KrAction_NewCard";
                    public const string EventType = "NewCard";
            }
            public static class BeforeStoreCard
            {
                    public const int ID = 1;
                    public const string Name = "$KrAction_BeforeStoreCard";
                    public const string EventType = "BeforeStoreCard";
            }
            public static class StoreCard
            {
                    public const int ID = 2;
                    public const string Name = "$KrAction_StoreCard";
                    public const string EventType = "StoreCard";
            }
            public static class BeforeCompleteTask
            {
                    public const int ID = 3;
                    public const string Name = "$KrAction_BeforeCompleteTask";
                    public const string EventType = "BeforeCompleteTask";
            }
            public static class CompleteTask
            {
                    public const int ID = 4;
                    public const string Name = "$KrAction_CompleteTask";
                    public const string EventType = "CompleteTask";
            }
            public static class BeforeNewTask
            {
                    public const int ID = 5;
                    public const string Name = "$KrAction_BeforeNewTask";
                    public const string EventType = "BeforeNewTask";
            }
            public static class NewTask
            {
                    public const int ID = 6;
                    public const string Name = "$KrAction_NewTask";
                    public const string EventType = "NewTask";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrActionTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrActionTypes Enumeration

    public sealed class KrActionTypes
    {
        public readonly int ID;
        public readonly string Name;
        public readonly string EventType;

        public KrActionTypes(int ID, string Name, string EventType)
        {
            this.ID = ID;
            this.Name = Name;
            this.EventType = EventType;
        }
    }

    #endregion

    #endregion

    #region KrActiveTasks

    /// <summary>
    ///     ID: {c98ce2bb-a770-4e13-a1b6-314ba68f9bfc}
    ///     Alias: KrActiveTasks
    ///     Group: Kr
    ///     Description: Активные задания процесса согласования
    /// </summary>
    public sealed class KrActiveTasksSchemeInfo
    {
        private const string name = "KrActiveTasks";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TaskID = nameof(TaskID);

        #endregion

        #region ToString

        public static implicit operator string(KrActiveTasksSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrActiveTasksVirtual

    /// <summary>
    ///     ID: {21dbb01c-1510-4318-b47d-c2be3197cdfb}
    ///     Alias: KrActiveTasksVirtual
    ///     Group: Kr
    ///     Description: Активные задания процесса согласования
    /// </summary>
    public sealed class KrActiveTasksVirtualSchemeInfo
    {
        private const string name = "KrActiveTasksVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TaskID = nameof(TaskID);

        #endregion

        #region ToString

        public static implicit operator string(KrActiveTasksVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAddFromTemplateSettingsVirtual

    /// <summary>
    ///     ID: {b31d2f6f-7980-4686-8029-3abd969ee11b}
    ///     Alias: KrAddFromTemplateSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrAddFromTemplateSettingsVirtualSchemeInfo
    {
        private const string name = "KrAddFromTemplateSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string FileTemplateID = nameof(FileTemplateID);
        public readonly string FileTemplateName = nameof(FileTemplateName);

        #endregion

        #region ToString

        public static implicit operator string(KrAddFromTemplateSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAdditionalApproval

    /// <summary>
    ///     ID: {476425ca-8284-4c41-b11b-dd215042ee6a}
    ///     Alias: KrAdditionalApproval
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrAdditionalApprovalSchemeInfo
    {
        private const string name = "KrAdditionalApproval";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string TimeLimitation = nameof(TimeLimitation);
        public readonly string FirstIsResponsible = nameof(FirstIsResponsible);
        public readonly string Comment = nameof(Comment);

        #endregion

        #region ToString

        public static implicit operator string(KrAdditionalApprovalSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAdditionalApprovalInfo

    /// <summary>
    ///     ID: {5f83de75-7485-4785-9528-06ca0e41c5ba}
    ///     Alias: KrAdditionalApprovalInfo
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrAdditionalApprovalInfoSchemeInfo
    {
        private const string name = "KrAdditionalApprovalInfo";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string PerformerID = nameof(PerformerID);
        public readonly string PerformerName = nameof(PerformerName);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);
        public readonly string Comment = nameof(Comment);
        public readonly string Answer = nameof(Answer);
        public readonly string Created = nameof(Created);
        public readonly string InProgress = nameof(InProgress);
        public readonly string Planned = nameof(Planned);
        public readonly string Completed = nameof(Completed);
        public readonly string IsResponsible = nameof(IsResponsible);

        #endregion

        #region ToString

        public static implicit operator string(KrAdditionalApprovalInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAdditionalApprovalInfoUsersCardVirtual

    /// <summary>
    ///     ID: {fed14580-062d-4f30-a344-23c8d2a427d4}
    ///     Alias: KrAdditionalApprovalInfoUsersCardVirtual
    ///     Group: KrStageTypes
    ///     Description: Табличка для контрола с доп. согласантами на вкладке настроек этапа
    /// </summary>
    public sealed class KrAdditionalApprovalInfoUsersCardVirtualSchemeInfo
    {
        private const string name = "KrAdditionalApprovalInfoUsersCardVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string Order = nameof(Order);
        public readonly string MainApproverRowID = nameof(MainApproverRowID);
        public readonly string IsResponsible = nameof(IsResponsible);

        #endregion

        #region ToString

        public static implicit operator string(KrAdditionalApprovalInfoUsersCardVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAdditionalApprovalInfoVirtual

    /// <summary>
    ///     ID: {8a5782c7-06df-4c0b-9088-2efa46642e8e}
    ///     Alias: KrAdditionalApprovalInfoVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrAdditionalApprovalInfoVirtualSchemeInfo
    {
        private const string name = "KrAdditionalApprovalInfoVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string PerformerID = nameof(PerformerID);
        public readonly string PerformerName = nameof(PerformerName);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);
        public readonly string Comment = nameof(Comment);
        public readonly string Answer = nameof(Answer);
        public readonly string Created = nameof(Created);
        public readonly string InProgress = nameof(InProgress);
        public readonly string Planned = nameof(Planned);
        public readonly string Completed = nameof(Completed);
        public readonly string ColumnComment = nameof(ColumnComment);
        public readonly string ColumnState = nameof(ColumnState);
        public readonly string IsResponsible = nameof(IsResponsible);

        #endregion

        #region ToString

        public static implicit operator string(KrAdditionalApprovalInfoVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAdditionalApprovalsRequestedInfoVirtual

    /// <summary>
    ///     ID: {c5d3a740-794c-4904-b9b1-e0a697a7dd80}
    ///     Alias: KrAdditionalApprovalsRequestedInfoVirtual
    ///     Group: KrStageTypes
    ///     Description: Запрошенные дополнительные согласования.
    /// </summary>
    public sealed class KrAdditionalApprovalsRequestedInfoVirtualSchemeInfo
    {
        private const string name = "KrAdditionalApprovalsRequestedInfoVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string PerformerID = nameof(PerformerID);
        public readonly string PerformerName = nameof(PerformerName);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);
        public readonly string Comment = nameof(Comment);
        public readonly string Answer = nameof(Answer);
        public readonly string Created = nameof(Created);
        public readonly string InProgress = nameof(InProgress);
        public readonly string Planned = nameof(Planned);
        public readonly string Completed = nameof(Completed);
        public readonly string ColumnComment = nameof(ColumnComment);
        public readonly string ColumnState = nameof(ColumnState);

        #endregion

        #region ToString

        public static implicit operator string(KrAdditionalApprovalsRequestedInfoVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAdditionalApprovalTaskInfo

    /// <summary>
    ///     ID: {e0361d36-e2fd-48f9-875a-7ba9548932e5}
    ///     Alias: KrAdditionalApprovalTaskInfo
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrAdditionalApprovalTaskInfoSchemeInfo
    {
        private const string name = "KrAdditionalApprovalTaskInfo";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Comment = nameof(Comment);
        public readonly string AuthorRoleID = nameof(AuthorRoleID);
        public readonly string AuthorRoleName = nameof(AuthorRoleName);
        public readonly string IsResponsible = nameof(IsResponsible);

        #endregion

        #region ToString

        public static implicit operator string(KrAdditionalApprovalTaskInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAdditionalApprovalUsers

    /// <summary>
    ///     ID: {72544086-2776-418a-a867-516ef7aad325}
    ///     Alias: KrAdditionalApprovalUsers
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrAdditionalApprovalUsersSchemeInfo
    {
        private const string name = "KrAdditionalApprovalUsers";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(KrAdditionalApprovalUsersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAdditionalApprovalUsersCardVirtual

    /// <summary>
    ///     ID: {a4c58948-fe22-4e9c-9cfe-5535a4c13990}
    ///     Alias: KrAdditionalApprovalUsersCardVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrAdditionalApprovalUsersCardVirtualSchemeInfo
    {
        private const string name = "KrAdditionalApprovalUsersCardVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string MainApproverRowID = nameof(MainApproverRowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string Order = nameof(Order);
        public readonly string IsResponsible = nameof(IsResponsible);
        public readonly string BasedOnTemplateAdditionalApprovalRowID = nameof(BasedOnTemplateAdditionalApprovalRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrAdditionalApprovalUsersCardVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAmendingActionVirtual

    /// <summary>
    ///     ID: {9cf234bf-ca74-46e7-a91a-564526cc1517}
    ///     Alias: KrAmendingActionVirtual
    ///     Group: KrWe
    ///     Description: Параметры действия \"Доработка\".
    /// </summary>
    public sealed class KrAmendingActionVirtualSchemeInfo
    {
        private const string name = "KrAmendingActionVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string KindID = nameof(KindID);
        public readonly string KindCaption = nameof(KindCaption);
        public readonly string Digest = nameof(Digest);
        public readonly string Period = nameof(Period);
        public readonly string Planned = nameof(Planned);
        public readonly string IsChangeState = nameof(IsChangeState);
        public readonly string IsIncrementCycle = nameof(IsIncrementCycle);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);
        public readonly string InitTaskScript = nameof(InitTaskScript);
        public readonly string Result = nameof(Result);
        public readonly string CompleteOptionTaskScript = nameof(CompleteOptionTaskScript);
        public readonly string CompleteOptionNotificationID = nameof(CompleteOptionNotificationID);
        public readonly string CompleteOptionNotificationName = nameof(CompleteOptionNotificationName);
        public readonly string CompleteOptionExcludeDeputies = nameof(CompleteOptionExcludeDeputies);
        public readonly string CompleteOptionExcludeSubscribers = nameof(CompleteOptionExcludeSubscribers);
        public readonly string CompleteOptionNotificationScript = nameof(CompleteOptionNotificationScript);
        public readonly string CompleteOptionSendToPerformer = nameof(CompleteOptionSendToPerformer);
        public readonly string CompleteOptionSendToAuthor = nameof(CompleteOptionSendToAuthor);

        #endregion

        #region ToString

        public static implicit operator string(KrAmendingActionVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalActionAdditionalPerformersDisplayInfoVirtual

    /// <summary>
    ///     ID: {f909d7b8-a840-4864-a2de-fd50c4475519}
    ///     Alias: KrApprovalActionAdditionalPerformersDisplayInfoVirtual
    ///     Group: KrWe
    ///     Description: Отображаемые параметры дополнительного согласования для действия \"Согласование\".
    /// </summary>
    public sealed class KrApprovalActionAdditionalPerformersDisplayInfoVirtualSchemeInfo
    {
        private const string name = "KrApprovalActionAdditionalPerformersDisplayInfoVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string Order = nameof(Order);
        public readonly string MainApproverRowID = nameof(MainApproverRowID);
        public readonly string IsResponsible = nameof(IsResponsible);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalActionAdditionalPerformersDisplayInfoVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalActionAdditionalPerformersSettingsVirtual

    /// <summary>
    ///     ID: {d96c77c3-dec7-4332-b427-bf77ad09c546}
    ///     Alias: KrApprovalActionAdditionalPerformersSettingsVirtual
    ///     Group: KrWe
    ///     Description: Параметры дополнительного согласования для действия \"Согласование\" являющиеся едиными для всех доп. согласующих.
    /// </summary>
    public sealed class KrApprovalActionAdditionalPerformersSettingsVirtualSchemeInfo
    {
        private const string name = "KrApprovalActionAdditionalPerformersSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string IsAdditionalApprovalFirstResponsible = nameof(IsAdditionalApprovalFirstResponsible);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalActionAdditionalPerformersSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalActionAdditionalPerformersVirtual

    /// <summary>
    ///     ID: {94a86f8e-ff0f-44fd-933b-9c7af3f35a13}
    ///     Alias: KrApprovalActionAdditionalPerformersVirtual
    ///     Group: KrWe
    ///     Description: Параметры дополнительного согласования для действия \"Согласование\".
    /// </summary>
    public sealed class KrApprovalActionAdditionalPerformersVirtualSchemeInfo
    {
        private const string name = "KrApprovalActionAdditionalPerformersVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string Order = nameof(Order);
        public readonly string MainApproverRowID = nameof(MainApproverRowID);
        public readonly string IsResponsible = nameof(IsResponsible);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalActionAdditionalPerformersVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalActionNotificationActionRolesVirtual

    /// <summary>
    ///     ID: {d299cae6-f32d-48b5-8930-031e78b3a2a1}
    ///     Alias: KrApprovalActionNotificationActionRolesVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Согласование\". Таблица с ролями на которые отправляется уведомление при завершения действия с отпределённым вариантом завершения.
    /// </summary>
    public sealed class KrApprovalActionNotificationActionRolesVirtualSchemeInfo
    {
        private const string name = "KrApprovalActionNotificationActionRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string OptionRowID = nameof(OptionRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalActionNotificationActionRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalActionNotificationRolesVirtual

    /// <summary>
    ///     ID: {ae419e33-eb19-456c-a319-54da9ace8821}
    ///     Alias: KrApprovalActionNotificationRolesVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Согласование\". Таблица с обрабатываемыми вариантами завершения задания действия.
    /// </summary>
    public sealed class KrApprovalActionNotificationRolesVirtualSchemeInfo
    {
        private const string name = "KrApprovalActionNotificationRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string OptionRowID = nameof(OptionRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalActionNotificationRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalActionOptionLinksVirtual

    /// <summary>
    ///     ID: {fc3ec595-313c-4b5f-aada-07d7d2f34ff2}
    ///     Alias: KrApprovalActionOptionLinksVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Согласование\". Коллекционная секция объединяющая связи и вырианты завершения.
    /// </summary>
    public sealed class KrApprovalActionOptionLinksVirtualSchemeInfo
    {
        private const string name = "KrApprovalActionOptionLinksVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string LinkID = nameof(LinkID);
        public readonly string LinkName = nameof(LinkName);
        public readonly string LinkCaption = nameof(LinkCaption);
        public readonly string ActionOptionRowID = nameof(ActionOptionRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalActionOptionLinksVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalActionOptionsActionVirtual

    /// <summary>
    ///     ID: {244719bf-4d4a-4df6-b2fe-a00b1bf6d173}
    ///     Alias: KrApprovalActionOptionsActionVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Согласование\". Коллекционная секция содержащая параметры завершения действия.
    /// </summary>
    public sealed class KrApprovalActionOptionsActionVirtualSchemeInfo
    {
        private const string name = "KrApprovalActionOptionsActionVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string LinkID = nameof(LinkID);
        public readonly string ActionOptionID = nameof(ActionOptionID);
        public readonly string ActionOptionCaption = nameof(ActionOptionCaption);
        public readonly string Order = nameof(Order);
        public readonly string Script = nameof(Script);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalActionOptionsActionVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalActionOptionsVirtual

    /// <summary>
    ///     ID: {cea61a5b-0420-41ba-a5f2-e21c21c30f5a}
    ///     Alias: KrApprovalActionOptionsVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Согласование\". Таблица с обрабатываемыми вариантами завершения задания действия.
    /// </summary>
    public sealed class KrApprovalActionOptionsVirtualSchemeInfo
    {
        private const string name = "KrApprovalActionOptionsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);
        public readonly string Script = nameof(Script);
        public readonly string Order = nameof(Order);
        public readonly string Result = nameof(Result);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string SendToPerformer = nameof(SendToPerformer);
        public readonly string SendToAuthor = nameof(SendToAuthor);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);
        public readonly string TaskTypeID = nameof(TaskTypeID);
        public readonly string TaskTypeCaption = nameof(TaskTypeCaption);
        public readonly string TaskTypeName = nameof(TaskTypeName);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalActionOptionsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalActionVirtual

    /// <summary>
    ///     ID: {2afee5e8-3582-4c7c-9fcf-1e4fddefe548}
    ///     Alias: KrApprovalActionVirtual
    ///     Group: KrWe
    ///     Description: Параметры действия \"Согласование\".
    /// </summary>
    public sealed class KrApprovalActionVirtualSchemeInfo
    {
        private const string name = "KrApprovalActionVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string KindID = nameof(KindID);
        public readonly string KindCaption = nameof(KindCaption);
        public readonly string Digest = nameof(Digest);
        public readonly string Period = nameof(Period);
        public readonly string Planned = nameof(Planned);
        public readonly string InitTaskScript = nameof(InitTaskScript);
        public readonly string Result = nameof(Result);
        public readonly string IsParallel = nameof(IsParallel);
        public readonly string ReturnWhenApproved = nameof(ReturnWhenApproved);
        public readonly string CanEditCard = nameof(CanEditCard);
        public readonly string CanEditAnyFiles = nameof(CanEditAnyFiles);
        public readonly string ChangeStateOnStart = nameof(ChangeStateOnStart);
        public readonly string ChangeStateOnEnd = nameof(ChangeStateOnEnd);
        public readonly string IsAdvisory = nameof(IsAdvisory);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);
        public readonly string SqlPerformersScript = nameof(SqlPerformersScript);
        public readonly string IsDisableAutoApproval = nameof(IsDisableAutoApproval);
        public readonly string ExpectAllApprovers = nameof(ExpectAllApprovers);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalActionVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalCommonInfo

    /// <summary>
    ///     ID: {410324bf-ce75-4024-a14c-5d78a8ad7588}
    ///     Alias: KrApprovalCommonInfo
    ///     Group: Kr
    ///     Description: Содержит информацию по основному процессу.
    /// </summary>
    public sealed class KrApprovalCommonInfoSchemeInfo
    {
        private const string name = "KrApprovalCommonInfo";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string MainCardID = nameof(MainCardID);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);
        public readonly string CurrentApprovalStageRowID = nameof(CurrentApprovalStageRowID);
        public readonly string ApprovedBy = nameof(ApprovedBy);
        public readonly string DisapprovedBy = nameof(DisapprovedBy);
        public readonly string AuthorComment = nameof(AuthorComment);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string StateChangedDateTimeUTC = nameof(StateChangedDateTimeUTC);
        public readonly string Info = nameof(Info);
        public readonly string CurrentHistoryGroup = nameof(CurrentHistoryGroup);
        public readonly string NestedWorkflowProcesses = nameof(NestedWorkflowProcesses);
        public readonly string ProcessOwnerID = nameof(ProcessOwnerID);
        public readonly string ProcessOwnerName = nameof(ProcessOwnerName);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalCommonInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalCommonInfoVirtual

    /// <summary>
    ///     ID: {fe5739f6-d64b-45f5-a3a3-75e999f721dd}
    ///     Alias: KrApprovalCommonInfoVirtual
    ///     Group: Kr
    /// </summary>
    public sealed class KrApprovalCommonInfoVirtualSchemeInfo
    {
        private const string name = "KrApprovalCommonInfoVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string MainCardID = nameof(MainCardID);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);
        public readonly string CurrentApprovalStageRowID = nameof(CurrentApprovalStageRowID);
        public readonly string ApprovedBy = nameof(ApprovedBy);
        public readonly string DisapprovedBy = nameof(DisapprovedBy);
        public readonly string AuthorComment = nameof(AuthorComment);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string StateChangedDateTimeUTC = nameof(StateChangedDateTimeUTC);
        public readonly string ProcessOwnerID = nameof(ProcessOwnerID);
        public readonly string ProcessOwnerName = nameof(ProcessOwnerName);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalCommonInfoVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalHistory

    /// <summary>
    ///     ID: {07d45e20-a501-4e3b-a246-e548a74d0730}
    ///     Alias: KrApprovalHistory
    ///     Group: Kr
    ///     Description: Сопоставление истории заданий с историей согласования (с учетом циклов согласования)
    /// </summary>
    public sealed class KrApprovalHistorySchemeInfo
    {
        private const string name = "KrApprovalHistory";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Cycle = nameof(Cycle);
        public readonly string HistoryRecord = nameof(HistoryRecord);
        public readonly string Advisory = nameof(Advisory);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalHistorySchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalHistoryVirtual

    /// <summary>
    ///     ID: {64a54b5b-bbd2-49ae-a378-1e8daa88c070}
    ///     Alias: KrApprovalHistoryVirtual
    ///     Group: Kr
    ///     Description: Сопоставление истории заданий с историей согласования (с учетом циклов согласования)
    /// </summary>
    public sealed class KrApprovalHistoryVirtualSchemeInfo
    {
        private const string name = "KrApprovalHistoryVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Cycle = nameof(Cycle);
        public readonly string HistoryRecord = nameof(HistoryRecord);
        public readonly string Advisory = nameof(Advisory);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalHistoryVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrApprovalSettingsVirtual

    /// <summary>
    ///     ID: {5a48521b-e00c-44b6-995e-8f238e9103ff}
    ///     Alias: KrApprovalSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrApprovalSettingsVirtualSchemeInfo
    {
        private const string name = "KrApprovalSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string IsParallel = nameof(IsParallel);
        public readonly string ReturnToAuthor = nameof(ReturnToAuthor);
        public readonly string ReturnWhenDisapproved = nameof(ReturnWhenDisapproved);
        public readonly string CanEditCard = nameof(CanEditCard);
        public readonly string CanEditFiles = nameof(CanEditFiles);
        public readonly string Comment = nameof(Comment);
        public readonly string DisableAutoApproval = nameof(DisableAutoApproval);
        public readonly string FirstIsResponsible = nameof(FirstIsResponsible);
        public readonly string ChangeStateOnStart = nameof(ChangeStateOnStart);
        public readonly string ChangeStateOnEnd = nameof(ChangeStateOnEnd);
        public readonly string Advisory = nameof(Advisory);
        public readonly string NotReturnEdit = nameof(NotReturnEdit);

        #endregion

        #region ToString

        public static implicit operator string(KrApprovalSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAuthorSettingsVirtual

    /// <summary>
    ///     ID: {17931d48-fae6-415e-bb76-3ea3a457a2e9}
    ///     Alias: KrAuthorSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrAuthorSettingsVirtualSchemeInfo
    {
        private const string name = "KrAuthorSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);

        #endregion

        #region ToString

        public static implicit operator string(KrAuthorSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrAutoApproveHistory

    /// <summary>
    ///     ID: {dee2be6f-5d24-443f-b468-f6e03a6742b5}
    ///     Alias: KrAutoApproveHistory
    ///     Group: System
    /// </summary>
    public sealed class KrAutoApproveHistorySchemeInfo
    {
        private const string name = "KrAutoApproveHistory";

        #region Columns

        public readonly string CardDigest = nameof(CardDigest);
        public readonly string Date = nameof(Date);
        public readonly string CardID = nameof(CardID);
        public readonly string CardTypeID = nameof(CardTypeID);
        public readonly string CardTypeCaption = nameof(CardTypeCaption);
        public readonly string ID = nameof(ID);
        public readonly string UserID = nameof(UserID);
        public readonly string Comment = nameof(Comment);
        public readonly string RowNumber = nameof(RowNumber);

        #endregion

        #region ToString

        public static implicit operator string(KrAutoApproveHistorySchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrBuildGlobalOutputVirtual

    /// <summary>
    ///     ID: {0d23c056-70cc-4b25-9c3b-d6e2a9e48509}
    ///     Alias: KrBuildGlobalOutputVirtual
    ///     Group: Kr
    ///     Description: Секция, используемая для вывода результатов сборки всех объектов подсистемы маршрутов.
    /// </summary>
    public sealed class KrBuildGlobalOutputVirtualSchemeInfo
    {
        private const string name = "KrBuildGlobalOutputVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ObjectID = nameof(ObjectID);
        public readonly string ObjectName = nameof(ObjectName);
        public readonly string ObjectTypeCaption = nameof(ObjectTypeCaption);
        public readonly string Output = nameof(Output);
        public readonly string CompilationDateTime = nameof(CompilationDateTime);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);

        #endregion

        #region ToString

        public static implicit operator string(KrBuildGlobalOutputVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrBuildLocalOutputVirtual

    /// <summary>
    ///     ID: {255f542f-3469-4c42-928d-7cf2cfedb644}
    ///     Alias: KrBuildLocalOutputVirtual
    ///     Group: Kr
    ///     Description: Секция, используемая для вывода результатов сборки текущего объекта подсистемы маршрутов.
    /// </summary>
    public sealed class KrBuildLocalOutputVirtualSchemeInfo
    {
        private const string name = "KrBuildLocalOutputVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Output = nameof(Output);

        #endregion

        #region ToString

        public static implicit operator string(KrBuildLocalOutputVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrBuildStates

    /// <summary>
    ///     ID: {e12af590-efd5-4890-b1c7-5a7ce83195dd}
    ///     Alias: KrBuildStates
    ///     Group: Kr
    ///     Description: Состояние компиляции объекта.
    /// </summary>
    public sealed class KrBuildStatesSchemeInfo
    {
        private const string name = "KrBuildStates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class None
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_KrBuildStates_None";
            }
            public static class Error
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_KrBuildStates_Error";
            }
            public static class Success
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_KrBuildStates_Success";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrBuildStatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrBuildStates Enumeration

    public sealed class KrBuildStates
    {
        public readonly int ID;
        public readonly string Name;

        public KrBuildStates(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrCardGeneratorVirtual

    /// <summary>
    ///     ID: {1052a0bc-1a02-4fd4-9636-5dacd0acc436}
    ///     Alias: KrCardGeneratorVirtual
    ///     Group: Kr
    ///     Description: Параметры генерации тестовых карточек.
    /// </summary>
    public sealed class KrCardGeneratorVirtualSchemeInfo
    {
        private const string name = "KrCardGeneratorVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string UserCount = nameof(UserCount);
        public readonly string PartnerCount = nameof(PartnerCount);

        #endregion

        #region ToString

        public static implicit operator string(KrCardGeneratorVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrCardTasksEditorDialogVirtual

    /// <summary>
    ///     ID: {41c02d34-dd86-4115-a485-1bf5e32d2074}
    ///     Alias: KrCardTasksEditorDialogVirtual
    ///     Group: Kr
    /// </summary>
    public sealed class KrCardTasksEditorDialogVirtualSchemeInfo
    {
        private const string name = "KrCardTasksEditorDialogVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string KrToken = nameof(KrToken);
        public readonly string MainCardID = nameof(MainCardID);

        #endregion

        #region ToString

        public static implicit operator string(KrCardTasksEditorDialogVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrCardTypesVirtual

    /// <summary>
    ///     ID: {a90baecf-c9ce-4cba-8bb0-150a13666266}
    ///     Alias: KrCardTypesVirtual
    ///     Group: Kr
    ///     Description: Виртуальная таблица для ссылки из KrPermissions
    /// </summary>
    public sealed class KrCardTypesVirtualSchemeInfo
    {
        private const string name = "KrCardTypesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Caption = nameof(Caption);

        #endregion

        #region ToString

        public static implicit operator string(KrCardTypesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrChangeStateAction

    /// <summary>
    ///     ID: {1afa15c7-ca17-4fa9-bfe5-3ca066814247}
    ///     Alias: KrChangeStateAction
    ///     Group: KrWe
    ///     Description: Секция для действия ВСмена состояния
    /// </summary>
    public sealed class KrChangeStateActionSchemeInfo
    {
        private const string name = "KrChangeStateAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);

        #endregion

        #region ToString

        public static implicit operator string(KrChangeStateActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrChangeStateSettingsVirtual

    /// <summary>
    ///     ID: {bc1450c4-0ddd-4efd-9636-f2ec5d013979}
    ///     Alias: KrChangeStateSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrChangeStateSettingsVirtualSchemeInfo
    {
        private const string name = "KrChangeStateSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);

        #endregion

        #region ToString

        public static implicit operator string(KrChangeStateSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrCheckStateTileExtension

    /// <summary>
    ///     ID: {15368402-1522-4722-91b7-d27636f3596b}
    ///     Alias: KrCheckStateTileExtension
    ///     Group: Kr
    ///     Description: Расширение функциональности проверки прав доступа на тайлы в WorkflowEngine по состоянию типового решения
    /// </summary>
    public sealed class KrCheckStateTileExtensionSchemeInfo
    {
        private const string name = "KrCheckStateTileExtension";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);

        #endregion

        #region ToString

        public static implicit operator string(KrCheckStateTileExtensionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrCommentators

    /// <summary>
    ///     ID: {42c4f5aa-d0e8-4d26-abb8-a898e736fe35}
    ///     Alias: KrCommentators
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrCommentatorsSchemeInfo
    {
        private const string name = "KrCommentators";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string CommentatorID = nameof(CommentatorID);
        public readonly string CommentatorName = nameof(CommentatorName);

        #endregion

        #region ToString

        public static implicit operator string(KrCommentatorsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrCommentsInfo

    /// <summary>
    ///     ID: {b75dc3d2-10c8-4ca9-a63c-2a8f54db5c42}
    ///     Alias: KrCommentsInfo
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrCommentsInfoSchemeInfo
    {
        private const string name = "KrCommentsInfo";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Question = nameof(Question);
        public readonly string Answer = nameof(Answer);
        public readonly string CommentatorID = nameof(CommentatorID);
        public readonly string CommentatorName = nameof(CommentatorName);

        #endregion

        #region ToString

        public static implicit operator string(KrCommentsInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrCommentsInfoVirtual

    /// <summary>
    ///     ID: {e490c196-41b3-489b-8425-ee36a0119f64}
    ///     Alias: KrCommentsInfoVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrCommentsInfoVirtualSchemeInfo
    {
        private const string name = "KrCommentsInfoVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string QuestionShort = nameof(QuestionShort);
        public readonly string AnswerShort = nameof(AnswerShort);
        public readonly string CommentatorNameShort = nameof(CommentatorNameShort);
        public readonly string QuestionFull = nameof(QuestionFull);
        public readonly string AnswerFull = nameof(AnswerFull);
        public readonly string CommentatorNameFull = nameof(CommentatorNameFull);
        public readonly string Completed = nameof(Completed);

        #endregion

        #region ToString

        public static implicit operator string(KrCommentsInfoVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrCreateCardAction

    /// <summary>
    ///     ID: {70e22440-564a-40a9-88a1-f695844a113b}
    ///     Alias: KrCreateCardAction
    ///     Group: KrWe
    ///     Description: Основная секция действия создания карточки по типу или шаблону
    /// </summary>
    public sealed class KrCreateCardActionSchemeInfo
    {
        private const string name = "KrCreateCardAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string TemplateID = nameof(TemplateID);
        public readonly string TemplateCaption = nameof(TemplateCaption);
        public readonly string OpenCard = nameof(OpenCard);
        public readonly string SetAsMainCard = nameof(SetAsMainCard);
        public readonly string Script = nameof(Script);

        #endregion

        #region ToString

        public static implicit operator string(KrCreateCardActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrCreateCardStageSettingsVirtual

    /// <summary>
    ///     ID: {644515d1-8e3f-419e-b938-f59c5ec07fae}
    ///     Alias: KrCreateCardStageSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrCreateCardStageSettingsVirtualSchemeInfo
    {
        private const string name = "KrCreateCardStageSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string TemplateID = nameof(TemplateID);
        public readonly string TemplateCaption = nameof(TemplateCaption);
        public readonly string ModeID = nameof(ModeID);
        public readonly string ModeName = nameof(ModeName);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(KrCreateCardStageSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrCreateCardStageTypeModes

    /// <summary>
    ///     ID: {ebf6257e-c0c6-4f84-b913-7a66fc196418}
    ///     Alias: KrCreateCardStageTypeModes
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrCreateCardStageTypeModesSchemeInfo
    {
        private const string name = "KrCreateCardStageTypeModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Order = nameof(Order);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class OpenMode
            {
                    public const int ID = 0;
                    public const string Name = "$KrStages_CreateCard_OpenMode";
                    public const int Order = 0;
            }
            public static class StoreAndOpenMode
            {
                    public const int ID = 1;
                    public const string Name = "$KrStages_CreateCard_StoreAndOpenMode";
                    public const int Order = 2;
            }
            public static class StartProcessMode
            {
                    public const int ID = 2;
                    public const string Name = "$KrStages_CreateCard_StartProcessMode";
                    public const int Order = 3;
            }
            public static class StartProcessAndOpenMode
            {
                    public const int ID = 3;
                    public const string Name = "$KrStages_CreateCard_StartProcessAndOpenMode";
                    public const int Order = 4;
            }
            public static class StoreMode
            {
                    public const int ID = 4;
                    public const string Name = "$KrStages_CreateCard_StoreMode";
                    public const int Order = 1;
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrCreateCardStageTypeModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrCreateCardStageTypeModes Enumeration

    public sealed class KrCreateCardStageTypeModes
    {
        public readonly int ID;
        public readonly string Name;
        public readonly int Order;

        public KrCreateCardStageTypeModes(int ID, string Name, int Order)
        {
            this.ID = ID;
            this.Name = Name;
            this.Order = Order;
        }
    }

    #endregion

    #endregion

    #region KrCycleGroupingModes

    /// <summary>
    ///     ID: {3e451f29-8808-4398-930e-d5c172c21de7}
    ///     Alias: KrCycleGroupingModes
    ///     Group: Kr
    /// </summary>
    public sealed class KrCycleGroupingModesSchemeInfo
    {
        private const string name = "KrCycleGroupingModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class ShowAllCycleFiles
            {
                    public const int ID = 0;
                    public const string Name = "$UI_Controls_FilesControl_ShowAllCycleFiles";
            }
            public static class ShowCurrentCycleFilesOnly
            {
                    public const int ID = 1;
                    public const string Name = "$UI_Controls_FilesControl_ShowCurrentCycleFilesOnly";
            }
            public static class ShowCurrentAndLastCycleFilesOnly
            {
                    public const int ID = 2;
                    public const string Name = "$UI_Controls_FilesControl_ShowCurrentAndLastCycleFilesOnly";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrCycleGroupingModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrCycleGroupingModes Enumeration

    public sealed class KrCycleGroupingModes
    {
        public readonly int ID;
        public readonly string Name;

        public KrCycleGroupingModes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrDepartmentCondition

    /// <summary>
    ///     ID: {70427d3c-3df8-4efc-8bf7-8e19efa2c20d}
    ///     Alias: KrDepartmentCondition
    ///     Group: Kr
    ///     Description: Секция для условия для правил уведомлений, проверяющая Подразделение.
    /// </summary>
    public sealed class KrDepartmentConditionSchemeInfo
    {
        private const string name = "KrDepartmentCondition";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string DepartmentID = nameof(DepartmentID);
        public readonly string DepartmentName = nameof(DepartmentName);

        #endregion

        #region ToString

        public static implicit operator string(KrDepartmentConditionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrDepartmentConditionSettings

    /// <summary>
    ///     ID: {f753b988-0c00-471b-869d-0ac361af0d83}
    ///     Alias: KrDepartmentConditionSettings
    ///     Group: Kr
    ///     Description: Секция для условия для правил уведомлений, првоеряющая дополнительные настройки подразделения
    /// </summary>
    public sealed class KrDepartmentConditionSettingsSchemeInfo
    {
        private const string name = "KrDepartmentConditionSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CheckAuthor = nameof(CheckAuthor);
        public readonly string CheckInitiator = nameof(CheckInitiator);
        public readonly string CheckCard = nameof(CheckCard);

        #endregion

        #region ToString

        public static implicit operator string(KrDepartmentConditionSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrDialogButtonSettingsVirtual

    /// <summary>
    ///     ID: {0d52e2ff-45ec-449d-bd49-e0b5f666ee65}
    ///     Alias: KrDialogButtonSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrDialogButtonSettingsVirtualSchemeInfo
    {
        private const string name = "KrDialogButtonSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeName = nameof(TypeName);
        public readonly string Caption = nameof(Caption);
        public readonly string Icon = nameof(Icon);
        public readonly string Cancel = nameof(Cancel);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(KrDialogButtonSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrDialogStageTypeSettingsVirtual

    /// <summary>
    ///     ID: {663dcabe-f9d8-4a52-a235-6c407e683810}
    ///     Alias: KrDialogStageTypeSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrDialogStageTypeSettingsVirtualSchemeInfo
    {
        private const string name = "KrDialogStageTypeSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string DialogTypeID = nameof(DialogTypeID);
        public readonly string DialogTypeName = nameof(DialogTypeName);
        public readonly string DialogTypeCaption = nameof(DialogTypeCaption);
        public readonly string CardStoreModeID = nameof(CardStoreModeID);
        public readonly string CardStoreModeName = nameof(CardStoreModeName);
        public readonly string DialogActionScript = nameof(DialogActionScript);
        public readonly string ButtonName = nameof(ButtonName);
        public readonly string DialogName = nameof(DialogName);
        public readonly string DialogAlias = nameof(DialogAlias);
        public readonly string OpenModeID = nameof(OpenModeID);
        public readonly string OpenModeName = nameof(OpenModeName);
        public readonly string TaskDigest = nameof(TaskDigest);
        public readonly string DialogCardSavingScript = nameof(DialogCardSavingScript);
        public readonly string DisplayValue = nameof(DisplayValue);
        public readonly string KeepFiles = nameof(KeepFiles);
        public readonly string TemplateID = nameof(TemplateID);
        public readonly string TemplateCaption = nameof(TemplateCaption);
        public readonly string IsCloseWithoutConfirmation = nameof(IsCloseWithoutConfirmation);

        #endregion

        #region ToString

        public static implicit operator string(KrDialogStageTypeSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrDocNumberRegistrationAutoAssignment

    /// <summary>
    ///     ID: {b965332c-296b-48e3-b16f-21a0cd8a6a25}
    ///     Alias: KrDocNumberRegistrationAutoAssignment
    ///     Group: Kr
    ///     Description: Перечисление вариантов автоматического выделения номера при регистрации документа
    /// </summary>
    public sealed class KrDocNumberRegistrationAutoAssignmentSchemeInfo
    {
        private const string name = "KrDocNumberRegistrationAutoAssignment";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Description = nameof(Description);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class NotToAssign
            {
                    public const int ID = 0;
                    public const string Description = "$Views_KrAutoAssigment_NotToAssign";
            }
            public static class Assign
            {
                    public const int ID = 1;
                    public const string Description = "$Views_KrAutoAssigment_Assign";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrDocNumberRegistrationAutoAssignmentSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrDocNumberRegistrationAutoAssignment Enumeration

    public sealed class KrDocNumberRegistrationAutoAssignment
    {
        public readonly int ID;
        public readonly string Description;

        public KrDocNumberRegistrationAutoAssignment(int ID, string Description)
        {
            this.ID = ID;
            this.Description = Description;
        }
    }

    #endregion

    #endregion

    #region KrDocNumberRegularAutoAssignment

    /// <summary>
    ///     ID: {83b4c03f-fdb8-4e11-bca4-02177dd4b3dc}
    ///     Alias: KrDocNumberRegularAutoAssignment
    ///     Group: Kr
    ///     Description: Перечисление вариантов автоматического выделения номера для документа
    /// </summary>
    public sealed class KrDocNumberRegularAutoAssignmentSchemeInfo
    {
        private const string name = "KrDocNumberRegularAutoAssignment";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Description = nameof(Description);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class NotToAssign
            {
                    public const int ID = 0;
                    public const string Description = "$Views_KrAutoAssigment_NotToAssign";
            }
            public static class WhenCreating
            {
                    public const int ID = 1;
                    public const string Description = "$Views_KrAutoAssigment_WhenCreating";
            }
            public static class WhenSaving
            {
                    public const int ID = 2;
                    public const string Description = "$Views_KrAutoAssigment_WhenSaving";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrDocNumberRegularAutoAssignmentSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrDocNumberRegularAutoAssignment Enumeration

    public sealed class KrDocNumberRegularAutoAssignment
    {
        public readonly int ID;
        public readonly string Description;

        public KrDocNumberRegularAutoAssignment(int ID, string Description)
        {
            this.ID = ID;
            this.Description = Description;
        }
    }

    #endregion

    #endregion

    #region KrDocState

    /// <summary>
    ///     ID: {47107d7a-3a8c-47f0-b800-2a45da222ff4}
    ///     Alias: KrDocState
    ///     Group: Kr
    /// </summary>
    public sealed class KrDocStateSchemeInfo
    {
        private const string name = "KrDocState";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Draft
            {
                    public const int ID = 0;
                    public const string Name = "$KrStates_Doc_Draft";
            }
            public static class Active
            {
                    public const int ID = 1;
                    public const string Name = "$KrStates_Doc_Active";
            }
            public static class Approved
            {
                    public const int ID = 2;
                    public const string Name = "$KrStates_Doc_Approved";
            }
            public static class Disapproved
            {
                    public const int ID = 3;
                    public const string Name = "$KrStates_Doc_Disapproved";
            }
            public static class Editing
            {
                    public const int ID = 4;
                    public const string Name = "$KrStates_Doc_Editing";
            }
            public static class Canceled
            {
                    public const int ID = 5;
                    public const string Name = "$KrStates_Doc_Canceled";
            }
            public static class Registered
            {
                    public const int ID = 6;
                    public const string Name = "$KrStates_Doc_Registered";
            }
            public static class Registration
            {
                    public const int ID = 7;
                    public const string Name = "$KrStates_Doc_Registration";
            }
            public static class Signed
            {
                    public const int ID = 8;
                    public const string Name = "$KrStates_Doc_Signed";
            }
            public static class Declined
            {
                    public const int ID = 9;
                    public const string Name = "$KrStates_Doc_Declined";
            }
            public static class Signing
            {
                    public const int ID = 10;
                    public const string Name = "$KrStates_Doc_Signing";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrDocStateSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrDocState Enumeration

    public sealed class KrDocState
    {
        public readonly int ID;
        public readonly string Name;

        public KrDocState(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrDocStateCondition

    /// <summary>
    ///     ID: {204bfce7-5a88-4586-90e5-36d69e5b39fa}
    ///     Alias: KrDocStateCondition
    ///     Group: Kr
    ///     Description: Секция для условия для правил уведомлений, првоеряющая состояния.
    /// </summary>
    public sealed class KrDocStateConditionSchemeInfo
    {
        private const string name = "KrDocStateCondition";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);

        #endregion

        #region ToString

        public static implicit operator string(KrDocStateConditionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrDocStateVirtual

    /// <summary>
    ///     ID: {e4345324-ad03-46ca-a157-5f71742e5816}
    ///     Alias: KrDocStateVirtual
    ///     Group: Kr
    ///     Description: Виртуальная карточка для состояния документа.
    /// </summary>
    public sealed class KrDocStateVirtualSchemeInfo
    {
        private const string name = "KrDocStateVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string StateID = nameof(StateID);
        public readonly string Name = nameof(Name);
        public readonly string PartitionID = nameof(PartitionID);
        public readonly string PartitionName = nameof(PartitionName);

        #endregion

        #region ToString

        public static implicit operator string(KrDocStateVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrDocType

    /// <summary>
    ///     ID: {78bfc212-cad5-4d1d-8b91-a9c58562b9d5}
    ///     Alias: KrDocType
    ///     Group: Kr
    ///     Description: Тип документа
    /// </summary>
    public sealed class KrDocTypeSchemeInfo
    {
        private const string name = "KrDocType";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Title = nameof(Title);
        public readonly string Description = nameof(Description);
        public readonly string CardTypeID = nameof(CardTypeID);
        public readonly string CardTypeCaption = nameof(CardTypeCaption);
        public readonly string CardTypeName = nameof(CardTypeName);
        public readonly string UseApproving = nameof(UseApproving);
        public readonly string UseRegistration = nameof(UseRegistration);
        public readonly string DocNumberRegularSequence = nameof(DocNumberRegularSequence);
        public readonly string DocNumberRegularFormat = nameof(DocNumberRegularFormat);
        public readonly string AllowManualRegularDocNumberAssignment = nameof(AllowManualRegularDocNumberAssignment);
        public readonly string DocNumberRegistrationSequence = nameof(DocNumberRegistrationSequence);
        public readonly string DocNumberRegistrationFormat = nameof(DocNumberRegistrationFormat);
        public readonly string AllowManualRegistrationDocNumberAssignment = nameof(AllowManualRegistrationDocNumberAssignment);
        public readonly string DocNumberRegistrationAutoAssignmentID = nameof(DocNumberRegistrationAutoAssignmentID);
        public readonly string DocNumberRegistrationAutoAssignmentDescription = nameof(DocNumberRegistrationAutoAssignmentDescription);
        public readonly string DocNumberRegularAutoAssignmentID = nameof(DocNumberRegularAutoAssignmentID);
        public readonly string DocNumberRegularAutoAssignmentDescription = nameof(DocNumberRegularAutoAssignmentDescription);
        public readonly string ReleaseRegularNumberOnFinalDeletion = nameof(ReleaseRegularNumberOnFinalDeletion);
        public readonly string ReleaseRegistrationNumberOnFinalDeletion = nameof(ReleaseRegistrationNumberOnFinalDeletion);
        public readonly string UseResolutions = nameof(UseResolutions);
        public readonly string DisableChildResolutionDateCheck = nameof(DisableChildResolutionDateCheck);
        public readonly string UseAutoApprove = nameof(UseAutoApprove);
        public readonly string ExceededDays = nameof(ExceededDays);
        public readonly string NotifyBefore = nameof(NotifyBefore);
        public readonly string AutoApproveComment = nameof(AutoApproveComment);
        public readonly string HideCreationButton = nameof(HideCreationButton);
        public readonly string HideRouteTab = nameof(HideRouteTab);
        public readonly string UseForum = nameof(UseForum);
        public readonly string UseDefaultDiscussionTab = nameof(UseDefaultDiscussionTab);
        public readonly string UseRoutesInWorkflowEngine = nameof(UseRoutesInWorkflowEngine);

        #endregion

        #region ToString

        public static implicit operator string(KrDocTypeSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrDocTypeCondition

    /// <summary>
    ///     ID: {7a74559a-0729-4dd8-9040-3367367ac673}
    ///     Alias: KrDocTypeCondition
    ///     Group: Kr
    ///     Description: Секция для условия для правил уведомлений, првоеряющая тип документа/карточки.
    /// </summary>
    public sealed class KrDocTypeConditionSchemeInfo
    {
        private const string name = "KrDocTypeCondition";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string DocTypeID = nameof(DocTypeID);
        public readonly string DocTypeCaption = nameof(DocTypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(KrDocTypeConditionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrEditSettingsVirtual

    /// <summary>
    ///     ID: {ef86e270-047b-4b7c-9c22-dda56e8eef2c}
    ///     Alias: KrEditSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrEditSettingsVirtualSchemeInfo
    {
        private const string name = "KrEditSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ChangeState = nameof(ChangeState);
        public readonly string Comment = nameof(Comment);
        public readonly string IncrementCycle = nameof(IncrementCycle);
        public readonly string DoNotSkipStage = nameof(DoNotSkipStage);
        public readonly string ManageStageVisibility = nameof(ManageStageVisibility);

        #endregion

        #region ToString

        public static implicit operator string(KrEditSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrForkManagementModes

    /// <summary>
    ///     ID: {75e444ae-a785-4e30-a6e0-15020a31654d}
    ///     Alias: KrForkManagementModes
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrForkManagementModesSchemeInfo
    {
        private const string name = "KrForkManagementModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class AddMode
            {
                    public const int ID = 0;
                    public const string Name = "$KrStages_ForkManagement_AddMode";
            }
            public static class RemoveMode
            {
                    public const int ID = 1;
                    public const string Name = "$KrStages_ForkManagement_RemoveMode";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrForkManagementModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrForkManagementModes Enumeration

    public sealed class KrForkManagementModes
    {
        public readonly int ID;
        public readonly string Name;

        public KrForkManagementModes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrForkManagementSettingsVirtual

    /// <summary>
    ///     ID: {c6397b27-d2a4-4b67-9450-7bb19a69fbbf}
    ///     Alias: KrForkManagementSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrForkManagementSettingsVirtualSchemeInfo
    {
        private const string name = "KrForkManagementSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ModeID = nameof(ModeID);
        public readonly string ModeName = nameof(ModeName);
        public readonly string ManagePrimaryProcess = nameof(ManagePrimaryProcess);
        public readonly string DirectionAfterInterrupt = nameof(DirectionAfterInterrupt);

        #endregion

        #region ToString

        public static implicit operator string(KrForkManagementSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrForkNestedProcessesSettingsVirtual

    /// <summary>
    ///     ID: {e8f3015f-4085-4df8-bafb-4c5b466965c0}
    ///     Alias: KrForkNestedProcessesSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrForkNestedProcessesSettingsVirtualSchemeInfo
    {
        private const string name = "KrForkNestedProcessesSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string NestedProcessID = nameof(NestedProcessID);

        #endregion

        #region ToString

        public static implicit operator string(KrForkNestedProcessesSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrForkSecondaryProcessesSettingsVirtual

    /// <summary>
    ///     ID: {08119dad-4504-49f5-8273-a1851cc4a0d0}
    ///     Alias: KrForkSecondaryProcessesSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrForkSecondaryProcessesSettingsVirtualSchemeInfo
    {
        private const string name = "KrForkSecondaryProcessesSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string SecondaryProcessID = nameof(SecondaryProcessID);
        public readonly string SecondaryProcessName = nameof(SecondaryProcessName);

        #endregion

        #region ToString

        public static implicit operator string(KrForkSecondaryProcessesSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrForkSettingsVirtual

    /// <summary>
    ///     ID: {27d6b3b7-8347-4e3c-982c-437f6c87ab13}
    ///     Alias: KrForkSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrForkSettingsVirtualSchemeInfo
    {
        private const string name = "KrForkSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AfterEachNestedProcess = nameof(AfterEachNestedProcess);

        #endregion

        #region ToString

        public static implicit operator string(KrForkSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrHistoryManagementStageSettingsVirtual

    /// <summary>
    ///     ID: {e08c3797-0f25-4841-a2a4-37bb0b938f88}
    ///     Alias: KrHistoryManagementStageSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrHistoryManagementStageSettingsVirtualSchemeInfo
    {
        private const string name = "KrHistoryManagementStageSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string TaskHistoryGroupTypeID = nameof(TaskHistoryGroupTypeID);
        public readonly string TaskHistoryGroupTypeCaption = nameof(TaskHistoryGroupTypeCaption);
        public readonly string ParentTaskHistoryGroupTypeID = nameof(ParentTaskHistoryGroupTypeID);
        public readonly string ParentTaskHistoryGroupTypeCaption = nameof(ParentTaskHistoryGroupTypeCaption);
        public readonly string NewIteration = nameof(NewIteration);

        #endregion

        #region ToString

        public static implicit operator string(KrHistoryManagementStageSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrInfoForInitiator

    /// <summary>
    ///     ID: {22a3da4b-ac30-4a40-a069-3d6ee66079a0}
    ///     Alias: KrInfoForInitiator
    ///     Group: Kr
    /// </summary>
    public sealed class KrInfoForInitiatorSchemeInfo
    {
        private const string name = "KrInfoForInitiator";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ApproverRole = nameof(ApproverRole);
        public readonly string ApproverUser = nameof(ApproverUser);
        public readonly string InProgress = nameof(InProgress);

        #endregion

        #region ToString

        public static implicit operator string(KrInfoForInitiatorSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrNotificationOptionalRecipientsVirtual

    /// <summary>
    ///     ID: {2bd36c6d-c035-4407-a270-d329fae7ec76}
    ///     Alias: KrNotificationOptionalRecipientsVirtual
    ///     Group: KrStageTypes
    ///     Description: Секция необязательных получателей этапа Уведомление
    /// </summary>
    public sealed class KrNotificationOptionalRecipientsVirtualSchemeInfo
    {
        private const string name = "KrNotificationOptionalRecipientsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(KrNotificationOptionalRecipientsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrNotificationSettingVirtual

    /// <summary>
    ///     ID: {28204069-f27e-4b4e-b309-5d2f77dbff8e}
    ///     Alias: KrNotificationSettingVirtual
    ///     Group: KrStageTypes
    ///     Description: Секция настроек этапа Уведомление
    /// </summary>
    public sealed class KrNotificationSettingVirtualSchemeInfo
    {
        private const string name = "KrNotificationSettingVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string EmailModificationScript = nameof(EmailModificationScript);

        #endregion

        #region ToString

        public static implicit operator string(KrNotificationSettingVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPartnerCondition

    /// <summary>
    ///     ID: {82f81a44-b515-4187-88c9-03a59e086031}
    ///     Alias: KrPartnerCondition
    ///     Group: Kr
    ///     Description: Секция для условия для правил уведомлений, првоеряющая контрагента.
    /// </summary>
    public sealed class KrPartnerConditionSchemeInfo
    {
        private const string name = "KrPartnerCondition";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string PartnerID = nameof(PartnerID);
        public readonly string PartnerName = nameof(PartnerName);

        #endregion

        #region ToString

        public static implicit operator string(KrPartnerConditionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPerformersVirtual

    /// <summary>
    ///     ID: {b47d668e-7bf0-4165-a10c-6fe22ee10882}
    ///     Alias: KrPerformersVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrPerformersVirtualSchemeInfo
    {
        private const string name = "KrPerformersVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string PerformerID = nameof(PerformerID);
        public readonly string PerformerName = nameof(PerformerName);
        public readonly string StageRowID = nameof(StageRowID);
        public readonly string Order = nameof(Order);
        public readonly string SQLApprover = nameof(SQLApprover);

        #endregion

        #region ToString

        public static implicit operator string(KrPerformersVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionAclGenerationRules

    /// <summary>
    ///     ID: {04cb0b04-b5c2-477c-ae4a-3d1e19f9530a}
    ///     Alias: KrPermissionAclGenerationRules
    ///     Group: Kr
    /// </summary>
    public sealed class KrPermissionAclGenerationRulesSchemeInfo
    {
        private const string name = "KrPermissionAclGenerationRules";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RuleID = nameof(RuleID);
        public readonly string RuleName = nameof(RuleName);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionAclGenerationRulesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionExtendedCardRuleFields

    /// <summary>
    ///     ID: {a40f2a59-e858-499d-a24a-0f18aab6cbd0}
    ///     Alias: KrPermissionExtendedCardRuleFields
    ///     Group: Kr
    ///     Description: Набор полей для расширенных настроек доступа
    /// </summary>
    public sealed class KrPermissionExtendedCardRuleFieldsSchemeInfo
    {
        private const string name = "KrPermissionExtendedCardRuleFields";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RuleRowID = nameof(RuleRowID);
        public readonly string FieldID = nameof(FieldID);
        public readonly string FieldName = nameof(FieldName);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionExtendedCardRuleFieldsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionExtendedCardRules

    /// <summary>
    ///     ID: {24c7c7fa-0c39-44c5-aa8d-0199ab79606e}
    ///     Alias: KrPermissionExtendedCardRules
    ///     Group: Kr
    ///     Description: Секция с расширенными настройками доступа к карточке
    /// </summary>
    public sealed class KrPermissionExtendedCardRulesSchemeInfo
    {
        private const string name = "KrPermissionExtendedCardRules";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string SectionID = nameof(SectionID);
        public readonly string SectionName = nameof(SectionName);
        public readonly string SectionTypeID = nameof(SectionTypeID);
        public readonly string AccessSettingID = nameof(AccessSettingID);
        public readonly string AccessSettingName = nameof(AccessSettingName);
        public readonly string IsHidden = nameof(IsHidden);
        public readonly string Order = nameof(Order);
        public readonly string Mask = nameof(Mask);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionExtendedCardRulesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionExtendedFileRuleCategories

    /// <summary>
    ///     ID: {2a337def-1279-456a-a61a-0232aa082123}
    ///     Alias: KrPermissionExtendedFileRuleCategories
    ///     Group: Kr
    ///     Description: Набор категорий, проверяемых в расширенных правилах доступа к файлам
    /// </summary>
    public sealed class KrPermissionExtendedFileRuleCategoriesSchemeInfo
    {
        private const string name = "KrPermissionExtendedFileRuleCategories";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RuleRowID = nameof(RuleRowID);
        public readonly string CategoryID = nameof(CategoryID);
        public readonly string CategoryName = nameof(CategoryName);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionExtendedFileRuleCategoriesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionExtendedFileRules

    /// <summary>
    ///     ID: {7ca15c10-9fd1-46e9-8769-b0acc0efe118}
    ///     Alias: KrPermissionExtendedFileRules
    ///     Group: Kr
    ///     Description: Секция с расширенными настройками доступа к файлам
    /// </summary>
    public sealed class KrPermissionExtendedFileRulesSchemeInfo
    {
        private const string name = "KrPermissionExtendedFileRules";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Extensions = nameof(Extensions);
        public readonly string ReadAccessSettingID = nameof(ReadAccessSettingID);
        public readonly string ReadAccessSettingName = nameof(ReadAccessSettingName);
        public readonly string Order = nameof(Order);
        public readonly string EditAccessSettingID = nameof(EditAccessSettingID);
        public readonly string EditAccessSettingName = nameof(EditAccessSettingName);
        public readonly string DeleteAccessSettingID = nameof(DeleteAccessSettingID);
        public readonly string DeleteAccessSettingName = nameof(DeleteAccessSettingName);
        public readonly string SignAccessSettingID = nameof(SignAccessSettingID);
        public readonly string SignAccessSettingName = nameof(SignAccessSettingName);
        public readonly string FileCheckRuleID = nameof(FileCheckRuleID);
        public readonly string FileCheckRuleName = nameof(FileCheckRuleName);
        public readonly string AddAccessSettingID = nameof(AddAccessSettingID);
        public readonly string AddAccessSettingName = nameof(AddAccessSettingName);
        public readonly string FileSizeLimit = nameof(FileSizeLimit);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionExtendedFileRulesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionExtendedMandatoryRuleFields

    /// <summary>
    ///     ID: {16588bc2-69cf-4a54-bf16-b0bf9507a315}
    ///     Alias: KrPermissionExtendedMandatoryRuleFields
    ///     Group: Kr
    /// </summary>
    public sealed class KrPermissionExtendedMandatoryRuleFieldsSchemeInfo
    {
        private const string name = "KrPermissionExtendedMandatoryRuleFields";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RuleRowID = nameof(RuleRowID);
        public readonly string FieldID = nameof(FieldID);
        public readonly string FieldName = nameof(FieldName);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionExtendedMandatoryRuleFieldsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionExtendedMandatoryRuleOptions

    /// <summary>
    ///     ID: {ae17320c-ff1b-45fb-9dd4-f9d99c24d824}
    ///     Alias: KrPermissionExtendedMandatoryRuleOptions
    ///     Group: Kr
    /// </summary>
    public sealed class KrPermissionExtendedMandatoryRuleOptionsSchemeInfo
    {
        private const string name = "KrPermissionExtendedMandatoryRuleOptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RuleRowID = nameof(RuleRowID);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionExtendedMandatoryRuleOptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionExtendedMandatoryRules

    /// <summary>
    ///     ID: {a4b6af05-9147-4335-8bf4-0e7387f77455}
    ///     Alias: KrPermissionExtendedMandatoryRules
    ///     Group: Kr
    /// </summary>
    public sealed class KrPermissionExtendedMandatoryRulesSchemeInfo
    {
        private const string name = "KrPermissionExtendedMandatoryRules";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string SectionID = nameof(SectionID);
        public readonly string SectionName = nameof(SectionName);
        public readonly string SectionTypeID = nameof(SectionTypeID);
        public readonly string ValidationTypeID = nameof(ValidationTypeID);
        public readonly string ValidationTypeName = nameof(ValidationTypeName);
        public readonly string Text = nameof(Text);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionExtendedMandatoryRulesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionExtendedMandatoryRuleTypes

    /// <summary>
    ///     ID: {a707b171-d676-45fd-8386-3bd3f20b7a1a}
    ///     Alias: KrPermissionExtendedMandatoryRuleTypes
    ///     Group: Kr
    /// </summary>
    public sealed class KrPermissionExtendedMandatoryRuleTypesSchemeInfo
    {
        private const string name = "KrPermissionExtendedMandatoryRuleTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RuleRowID = nameof(RuleRowID);
        public readonly string TaskTypeID = nameof(TaskTypeID);
        public readonly string TaskTypeCaption = nameof(TaskTypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionExtendedMandatoryRuleTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionExtendedTaskRuleFields

    /// <summary>
    ///     ID: {c30346b8-91a6-4dcd-8324-254e253f0148}
    ///     Alias: KrPermissionExtendedTaskRuleFields
    ///     Group: Kr
    /// </summary>
    public sealed class KrPermissionExtendedTaskRuleFieldsSchemeInfo
    {
        private const string name = "KrPermissionExtendedTaskRuleFields";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RuleRowID = nameof(RuleRowID);
        public readonly string FieldID = nameof(FieldID);
        public readonly string FieldName = nameof(FieldName);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionExtendedTaskRuleFieldsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionExtendedTaskRules

    /// <summary>
    ///     ID: {536f27ed-f1d2-4850-ad9e-eab93f584f1a}
    ///     Alias: KrPermissionExtendedTaskRules
    ///     Group: Kr
    ///     Description: Секция с расширенными настройками доступа к заданиям
    /// </summary>
    public sealed class KrPermissionExtendedTaskRulesSchemeInfo
    {
        private const string name = "KrPermissionExtendedTaskRules";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string SectionID = nameof(SectionID);
        public readonly string SectionName = nameof(SectionName);
        public readonly string SectionTypeID = nameof(SectionTypeID);
        public readonly string AccessSettingID = nameof(AccessSettingID);
        public readonly string AccessSettingName = nameof(AccessSettingName);
        public readonly string Order = nameof(Order);
        public readonly string IsHidden = nameof(IsHidden);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionExtendedTaskRulesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionExtendedTaskRuleTypes

    /// <summary>
    ///     ID: {c7f6a799-0dd6-4389-9122-bafc68f35c9e}
    ///     Alias: KrPermissionExtendedTaskRuleTypes
    ///     Group: Kr
    ///     Description: Секция с типами заданий для расширенных настроек доступа к заданиям
    /// </summary>
    public sealed class KrPermissionExtendedTaskRuleTypesSchemeInfo
    {
        private const string name = "KrPermissionExtendedTaskRuleTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RuleRowID = nameof(RuleRowID);
        public readonly string TaskTypeID = nameof(TaskTypeID);
        public readonly string TaskTypeCaption = nameof(TaskTypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionExtendedTaskRuleTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionExtendedVisibilityRules

    /// <summary>
    ///     ID: {aa13a164-dc2e-47e2-a415-021b8b5666e9}
    ///     Alias: KrPermissionExtendedVisibilityRules
    ///     Group: Kr
    /// </summary>
    public sealed class KrPermissionExtendedVisibilityRulesSchemeInfo
    {
        private const string name = "KrPermissionExtendedVisibilityRules";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Alias = nameof(Alias);
        public readonly string ControlTypeID = nameof(ControlTypeID);
        public readonly string ControlTypeName = nameof(ControlTypeName);
        public readonly string IsHidden = nameof(IsHidden);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionExtendedVisibilityRulesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionRoles

    /// <summary>
    ///     ID: {79a6e9e0-e52f-456f-871a-00b6895566ec}
    ///     Alias: KrPermissionRoles
    ///     Group: Kr
    ///     Description: Роли, для пользователей которых применяются разрешения из карточки с правами.
    /// </summary>
    public sealed class KrPermissionRolesSchemeInfo
    {
        private const string name = "KrPermissionRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string IsContext = nameof(IsContext);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionRuleAccessSettings

    /// <summary>
    ///     ID: {4c274eda-ab9a-403f-9e5b-0b933283b5a3}
    ///     Alias: KrPermissionRuleAccessSettings
    ///     Group: Kr
    ///     Description: Список настроек доступа для расширенных прав доступа
    /// </summary>
    public sealed class KrPermissionRuleAccessSettingsSchemeInfo
    {
        private const string name = "KrPermissionRuleAccessSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class AllowEdit
            {
                    public const int ID = 0;
                    public const string Name = "$KrPermissions_AccessSettings_AllowEdit";
            }
            public static class DisallowEdit
            {
                    public const int ID = 1;
                    public const string Name = "$KrPermissions_AccessSettings_DisallowEdit";
            }
            public static class DisallowRowAdding
            {
                    public const int ID = 2;
                    public const string Name = "$KrPermissions_AccessSettings_DisallowRowAdding";
            }
            public static class DisallowRowDeleting
            {
                    public const int ID = 3;
                    public const string Name = "$KrPermissions_AccessSettings_DisallowRowDeleting";
            }
            public static class MaskData
            {
                    public const int ID = 4;
                    public const string Name = "$KrPermissions_AccessSettings_MaskData";
            }
            public static class DisallowRowEdit
            {
                    public const int ID = 5;
                    public const string Name = "$KrPermissions_AccessSettings_DisallowRowEdit";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionRuleAccessSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrPermissionRuleAccessSettings Enumeration

    public sealed class KrPermissionRuleAccessSettings
    {
        public readonly int ID;
        public readonly string Name;

        public KrPermissionRuleAccessSettings(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrPermissions

    /// <summary>
    ///     ID: {1c7406cb-e445-4d1a-bf00-a1116db39bc6}
    ///     Alias: KrPermissions
    ///     Group: Kr
    ///     Description: Основная секция для карточки настроек разрешений для бизнес-процесса.
    /// </summary>
    public sealed class KrPermissionsSchemeInfo
    {
        private const string name = "KrPermissions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Caption = nameof(Caption);
        public readonly string Description = nameof(Description);
        public readonly string Types = nameof(Types);
        public readonly string States = nameof(States);
        public readonly string Roles = nameof(Roles);
        public readonly string Permissions = nameof(Permissions);
        public readonly string Conditions = nameof(Conditions);
        public readonly string CanCreateCard = nameof(CanCreateCard);
        public readonly string CanReadCard = nameof(CanReadCard);
        public readonly string CanEditCard = nameof(CanEditCard);
        public readonly string CanEditFiles = nameof(CanEditFiles);
        public readonly string CanAddFiles = nameof(CanAddFiles);
        public readonly string CanEditRoute = nameof(CanEditRoute);
        public readonly string CanDeleteCard = nameof(CanDeleteCard);
        public readonly string CanStartProcess = nameof(CanStartProcess);
        public readonly string CanRejectProcess = nameof(CanRejectProcess);
        public readonly string CanRebuildProcess = nameof(CanRebuildProcess);
        public readonly string CanCancelProcess = nameof(CanCancelProcess);
        public readonly string CanRegisterCard = nameof(CanRegisterCard);
        public readonly string CanSeeInCatalog = nameof(CanSeeInCatalog);
        public readonly string CanEditNumber = nameof(CanEditNumber);
        public readonly string CanCreateResolutions = nameof(CanCreateResolutions);
        public readonly string CanDeleteFiles = nameof(CanDeleteFiles);
        public readonly string CanEditOwnFiles = nameof(CanEditOwnFiles);
        public readonly string CanDeleteOwnFiles = nameof(CanDeleteOwnFiles);
        public readonly string CanSignFiles = nameof(CanSignFiles);
        public readonly string CanAddTopics = nameof(CanAddTopics);
        public readonly string CanSuperModeratorMode = nameof(CanSuperModeratorMode);
        public readonly string CanSubscribeForNotifications = nameof(CanSubscribeForNotifications);
        public readonly string IsExtended = nameof(IsExtended);
        public readonly string IsRequired = nameof(IsRequired);
        public readonly string IsDisabled = nameof(IsDisabled);
        public readonly string CanCreateTemplateAndCopy = nameof(CanCreateTemplateAndCopy);
        public readonly string CanSkipStages = nameof(CanSkipStages);
        public readonly string CanFullRecalcRoute = nameof(CanFullRecalcRoute);
        public readonly string CanEditMyMessages = nameof(CanEditMyMessages);
        public readonly string CanEditAllMessages = nameof(CanEditAllMessages);
        public readonly string CanModifyAllTaskAssignedRoles = nameof(CanModifyAllTaskAssignedRoles);
        public readonly string Priority = nameof(Priority);
        public readonly string CanReadAllTopics = nameof(CanReadAllTopics);
        public readonly string CanReadAndSendMessageInAllTopics = nameof(CanReadAndSendMessageInAllTopics);
        public readonly string CanModifyOwnTaskAssignedRoles = nameof(CanModifyOwnTaskAssignedRoles);
        public readonly string AclGenerationRules = nameof(AclGenerationRules);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionsControlTypes

    /// <summary>
    ///     ID: {18ad7847-b0f7-4d74-bc04-d96cbf18eecd}
    ///     Alias: KrPermissionsControlTypes
    ///     Group: Kr
    /// </summary>
    public sealed class KrPermissionsControlTypesSchemeInfo
    {
        private const string name = "KrPermissionsControlTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Tab
            {
                    public const int ID = 0;
                    public const string Name = "$KrPermissions_ControlType_Tab";
            }
            public static class Block
            {
                    public const int ID = 1;
                    public const string Name = "$KrPermissions_ControlType_Block";
            }
            public static class Control
            {
                    public const int ID = 2;
                    public const string Name = "$KrPermissions_ControlType_Control";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsControlTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrPermissionsControlTypes Enumeration

    public sealed class KrPermissionsControlTypes
    {
        public readonly int ID;
        public readonly string Name;

        public KrPermissionsControlTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrPermissionsFileCheckRules

    /// <summary>
    ///     ID: {2baf9cf1-a8d5-4e82-bccd-769d7c70e10a}
    ///     Alias: KrPermissionsFileCheckRules
    ///     Group: Kr
    ///     Description: Правила проверки файлов в расширенных настройках доступа к файлам.
    /// </summary>
    public sealed class KrPermissionsFileCheckRulesSchemeInfo
    {
        private const string name = "KrPermissionsFileCheckRules";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class AllFiles
            {
                    public const int ID = 0;
                    public const string Name = "$KrPermissions_FileCheckRules_AllFiles";
            }
            public static class FilesOfOtherUsers
            {
                    public const int ID = 1;
                    public const string Name = "$KrPermissions_FileCheckRules_FilesOfOtherUsers";
            }
            public static class OwnFiles
            {
                    public const int ID = 2;
                    public const string Name = "$KrPermissions_FileCheckRules_OwnFiles";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsFileCheckRulesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrPermissionsFileCheckRules Enumeration

    public sealed class KrPermissionsFileCheckRules
    {
        public readonly int ID;
        public readonly string Name;

        public KrPermissionsFileCheckRules(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrPermissionsFileEditAccessSettings

    /// <summary>
    ///     ID: {9247ed2e-109d-4543-b888-1fe9da9479aa}
    ///     Alias: KrPermissionsFileEditAccessSettings
    ///     Group: Kr
    ///     Description: Настройки доступа на изменение файлов в расширенных настройках доступа к файлам.
    /// </summary>
    public sealed class KrPermissionsFileEditAccessSettingsSchemeInfo
    {
        private const string name = "KrPermissionsFileEditAccessSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Disallowed
            {
                    public const int ID = 0;
                    public const string Name = "$KrPermissions_FileEditAccessSettings_Disallowed";
            }
            public static class Allowed
            {
                    public const int ID = 1;
                    public const string Name = "$KrPermissions_FileEditAccessSettings_Allowed";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsFileEditAccessSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrPermissionsFileEditAccessSettings Enumeration

    public sealed class KrPermissionsFileEditAccessSettings
    {
        public readonly int ID;
        public readonly string Name;

        public KrPermissionsFileEditAccessSettings(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrPermissionsFileReadAccessSettings

    /// <summary>
    ///     ID: {95a74318-2e98-46bd-bced-1890dd1cd017}
    ///     Alias: KrPermissionsFileReadAccessSettings
    ///     Group: Kr
    ///     Description: Настройки доступа на чтение файлов в расширенных настройках доступа к файлам.
    /// </summary>
    public sealed class KrPermissionsFileReadAccessSettingsSchemeInfo
    {
        private const string name = "KrPermissionsFileReadAccessSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class FileNotAvailable
            {
                    public const int ID = 0;
                    public const string Name = "$KrPermissions_FileReadAccessSettings_FileNotAvailable";
            }
            public static class ContentNotAvailable
            {
                    public const int ID = 1;
                    public const string Name = "$KrPermissions_FileReadAccessSettings_ContentNotAvailable";
            }
            public static class OnlyLastVersion
            {
                    public const int ID = 2;
                    public const string Name = "$KrPermissions_FileReadAccessSettings_OnlyLastVersion";
            }
            public static class OnlyLastAndOwnVersions
            {
                    public const int ID = 3;
                    public const string Name = "$KrPermissions_FileReadAccessSettings_OnlyLastAndOwnVersions";
            }
            public static class AllVersions
            {
                    public const int ID = 4;
                    public const string Name = "$KrPermissions_FileReadAccessSettings_AllVersions";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsFileReadAccessSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrPermissionsFileReadAccessSettings Enumeration

    public sealed class KrPermissionsFileReadAccessSettings
    {
        public readonly int ID;
        public readonly string Name;

        public KrPermissionsFileReadAccessSettings(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrPermissionsMandatoryValidationTypes

    /// <summary>
    ///     ID: {4439a1f6-c747-442b-b315-caae1c934058}
    ///     Alias: KrPermissionsMandatoryValidationTypes
    ///     Group: Kr
    ///     Description: Список типов проверки обязательности
    /// </summary>
    public sealed class KrPermissionsMandatoryValidationTypesSchemeInfo
    {
        private const string name = "KrPermissionsMandatoryValidationTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Always
            {
                    public const int ID = 0;
                    public const string Name = "$KrPermissions_MandatoryValidationType_Always";
            }
            public static class OnTaskCompletion
            {
                    public const int ID = 1;
                    public const string Name = "$KrPermissions_MandatoryValidationType_OnTaskCompletion";
            }
            public static class WhenOneFieldFilled
            {
                    public const int ID = 2;
                    public const string Name = "$KrPermissions_MandatoryValidationType_WhenOneFieldFilled";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsMandatoryValidationTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrPermissionsMandatoryValidationTypes Enumeration

    public sealed class KrPermissionsMandatoryValidationTypes
    {
        public readonly int ID;
        public readonly string Name;

        public KrPermissionsMandatoryValidationTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrPermissionsSystem

    /// <summary>
    ///     ID: {937fdcfd-c412-4b5d-a319-c11684ea009a}
    ///     Alias: KrPermissionsSystem
    ///     Group: Kr
    ///     Description: Системная таблица для правил доступа.
    /// </summary>
    public sealed class KrPermissionsSystemSchemeInfo
    {
        private const string name = "KrPermissionsSystem";

        #region Columns

        public readonly string Version = nameof(Version);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsSystemSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionStates

    /// <summary>
    ///     ID: {5024c846-07bb-4932-bb8d-6c6f9c1e27f7}
    ///     Alias: KrPermissionStates
    ///     Group: Kr
    ///     Description: Состояния согласуемой карточки, к которым применяются права из карточки с правами.
    /// </summary>
    public sealed class KrPermissionStatesSchemeInfo
    {
        private const string name = "KrPermissionStates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionStatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrPermissionTypes

    /// <summary>
    ///     ID: {51c5a6be-fe5d-411c-95ba-21d503ced67a}
    ///     Alias: KrPermissionTypes
    ///     Group: Kr
    ///     Description: Типы карточек, к которым применяются разрешения из карточки с правами.
    /// </summary>
    public sealed class KrPermissionTypesSchemeInfo
    {
        private const string name = "KrPermissionTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string TypeIsDocType = nameof(TypeIsDocType);

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrProcessManagementStageSettingsVirtual

    /// <summary>
    ///     ID: {65b430e7-42f5-44c0-9d36-d31756c9941a}
    ///     Alias: KrProcessManagementStageSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrProcessManagementStageSettingsVirtualSchemeInfo
    {
        private const string name = "KrProcessManagementStageSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string StageGroupID = nameof(StageGroupID);
        public readonly string StageGroupName = nameof(StageGroupName);
        public readonly string StageRowID = nameof(StageRowID);
        public readonly string StageName = nameof(StageName);
        public readonly string StageRowGroupName = nameof(StageRowGroupName);
        public readonly string ManagePrimaryProcess = nameof(ManagePrimaryProcess);
        public readonly string ModeID = nameof(ModeID);
        public readonly string ModeName = nameof(ModeName);
        public readonly string Signal = nameof(Signal);

        #endregion

        #region ToString

        public static implicit operator string(KrProcessManagementStageSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrProcessManagementStageTypeModes

    /// <summary>
    ///     ID: {778c5e62-6064-447e-92ac-68913d6a42cd}
    ///     Alias: KrProcessManagementStageTypeModes
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrProcessManagementStageTypeModesSchemeInfo
    {
        private const string name = "KrProcessManagementStageTypeModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class StageMode
            {
                    public const int ID = 0;
                    public const string Name = "$KrStages_ProcessManagement_StageMode";
            }
            public static class GroupMode
            {
                    public const int ID = 1;
                    public const string Name = "$KrStages_ProcessManagement_GroupMode";
            }
            public static class NextGroupMode
            {
                    public const int ID = 2;
                    public const string Name = "$KrStages_ProcessManagement_NextGroupMode";
            }
            public static class PrevGroupMode
            {
                    public const int ID = 3;
                    public const string Name = "$KrStages_ProcessManagement_PrevGroupMode";
            }
            public static class CurrentGroupMode
            {
                    public const int ID = 4;
                    public const string Name = "$KrStages_ProcessManagement_CurrentGroupMode";
            }
            public static class SignalMode
            {
                    public const int ID = 5;
                    public const string Name = "$KrStages_ProcessManagement_SignalMode";
            }
            public static class CancelProcessMode
            {
                    public const int ID = 6;
                    public const string Name = "$KrStages_ProcessManagement_CancelProcessMode";
            }
            public static class SkipProcessMode
            {
                    public const int ID = 7;
                    public const string Name = "$KrStages_ProcessManagement_SkipProcessMode";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrProcessManagementStageTypeModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrProcessManagementStageTypeModes Enumeration

    public sealed class KrProcessManagementStageTypeModes
    {
        public readonly int ID;
        public readonly string Name;

        public KrProcessManagementStageTypeModes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrProcessStageTypes

    /// <summary>
    ///     ID: {7454f645-850f-4e9b-8c80-1f129c5cb1c4}
    ///     Alias: KrProcessStageTypes
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrProcessStageTypesSchemeInfo
    {
        private const string name = "KrProcessStageTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Caption = nameof(Caption);
        public readonly string DefaultStageName = nameof(DefaultStageName);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Approval
            {
                    public static Guid ID = new Guid(0x185610e1,0x6ab0,0x064e,0x94,0x29,0x4c,0x52,0x98,0x04,0xdf,0xe4);
                    public const string Caption = "$KrStages_Approval";
                    public const string DefaultStageName = null;
            }
            public static class Edit
            {
                    public static Guid ID = new Guid(0x4bf667bf,0x1a82,0x4e3f,0x9e,0xf0,0x44,0xb3,0xb5,0x6f,0xb9,0x8d);
                    public const string Caption = "$KrStages_Edit";
                    public const string DefaultStageName = "$KrStages_Edit";
            }
            public static class ChangeState
            {
                    public static Guid ID = new Guid(0xc8a9a721,0xba8e,0x45cd,0xa0,0x49,0xc2,0x4d,0x4b,0xdf,0x76,0xcb);
                    public const string Caption = "$KrStages_ChangeState";
                    public const string DefaultStageName = "$KrStages_ChangeState";
            }
            public static class PartialRecalc
            {
                    public static Guid ID = new Guid(0x42cef425,0x1180,0x4ccc,0x88,0xd9,0x50,0xfd,0xc1,0xea,0x39,0x82);
                    public const string Caption = "$KrStages_PartialRecalc";
                    public const string DefaultStageName = "$KrStages_PartialRecalc";
            }
            public static class ProcessManagement
            {
                    public static Guid ID = new Guid(0xc7bc176c,0x8779,0x46bd,0x96,0x04,0xec,0x84,0x71,0x40,0xbd,0x52);
                    public const string Caption = "$KrStages_ProcessManagement";
                    public const string DefaultStageName = "$KrStages_ProcessManagement";
            }
            public static class Resolution
            {
                    public static Guid ID = new Guid(0x6e6f6b28,0x97af,0x4ffe,0xb6,0xf1,0xb1,0xd8,0x37,0x1c,0xb3,0xfa);
                    public const string Caption = "$KrStages_Resolution";
                    public const string DefaultStageName = "$KrStages_Resolution";
            }
            public static class CreateCard
            {
                    public static Guid ID = new Guid(0x9e85f310,0x226c,0x4273,0x80,0x4c,0x52,0xc9,0x5b,0x3b,0xac,0x8e);
                    public const string Caption = "$KrStages_CreateCard";
                    public const string DefaultStageName = "$KrStages_CreateCard";
            }
            public static class Registration
            {
                    public static Guid ID = new Guid(0xb468700e,0x6535,0x440d,0xa1,0x07,0x89,0x45,0xed,0x92,0x74,0x29);
                    public const string Caption = "$KrStages_Registration";
                    public const string DefaultStageName = "$KrStages_Registration";
            }
            public static class Deregistration
            {
                    public static Guid ID = new Guid(0x9e6eee69,0xfbee,0x4be6,0xb0,0xe2,0x9a,0x1b,0x5f,0x8f,0x63,0xeb);
                    public const string Caption = "$KrStages_Deregistration";
                    public const string DefaultStageName = "$KrStages_Deregistration";
            }
            public static class Signing
            {
                    public static Guid ID = new Guid(0xd4670257,0x6028,0x4bbc,0x9c,0xd6,0xce,0x16,0x3f,0x36,0xea,0x35);
                    public const string Caption = "$KrStages_Signing";
                    public const string DefaultStageName = null;
            }
            public static class UniversalTask
            {
                    public static Guid ID = new Guid(0xc3acbff6,0x707f,0x477c,0x99,0xc9,0xd1,0x5f,0xc2,0x41,0xfc,0x78);
                    public const string Caption = "$KrStages_UniversalTask";
                    public const string DefaultStageName = "$KrStages_UniversalTask";
            }
            public static class Script
            {
                    public static Guid ID = new Guid(0xc02d9a43,0xad2a,0x475a,0x91,0x88,0x8f,0xc6,0x00,0xb6,0x4e,0xe8);
                    public const string Caption = "$KrStages_Script";
                    public const string DefaultStageName = "$KrStages_Script";
            }
            public static class Notification
            {
                    public static Guid ID = new Guid(0x19c7a9b3,0x6ae7,0x4072,0xb9,0xac,0x17,0x53,0x24,0x5e,0xc0,0xac);
                    public const string Caption = "$KrStages_Notification";
                    public const string DefaultStageName = "$KrStages_Notification";
            }
            public static class Acquaintance
            {
                    public static Guid ID = new Guid(0xc2e0e75a,0xde77,0x42cd,0x9f,0xf8,0xe8,0x72,0xb9,0x89,0x93,0x62);
                    public const string Caption = "$KrStages_Acquaintance";
                    public const string DefaultStageName = "$KrStages_Acquaintance";
            }
            public static class HistoryManagement
            {
                    public static Guid ID = new Guid(0x371937b5,0x38c6,0x436a,0x95,0x9b,0x42,0xfd,0x0e,0xe0,0x16,0x11);
                    public const string Caption = "$KrStages_HistoryManagement";
                    public const string DefaultStageName = "$KrStages_HistoryManagement";
            }
            public static class Fork
            {
                    public static Guid ID = new Guid(0x2246da18,0xbcf9,0x4a0c,0xa2,0xb8,0xf6,0x1f,0xbe,0x9b,0xfd,0xdb);
                    public const string Caption = "$KrStages_Fork";
                    public const string DefaultStageName = "$KrStages_Fork";
            }
            public static class ForkManagement
            {
                    public static Guid ID = new Guid(0xe1f86f2d,0xc8d5,0x4482,0xad,0x9f,0xa0,0x23,0xed,0xa4,0xbc,0x48);
                    public const string Caption = "$KrStages_ForkManagement";
                    public const string DefaultStageName = "$KrStages_ForkManagement";
            }
            public static class TypedTask
            {
                    public static Guid ID = new Guid(0xac7fcf5b,0x57d9,0x4a53,0x9c,0x30,0x50,0xe7,0x4c,0xd3,0xb6,0x8d);
                    public const string Caption = "$KrStages_TypedTask";
                    public const string DefaultStageName = "$KrStages_TypedTask";
            }
            public static class AddFromTemplate
            {
                    public static Guid ID = new Guid(0xc80839e2,0x1766,0x4e02,0xb8,0x5c,0x27,0x9e,0xa6,0xfd,0x60,0x0d);
                    public const string Caption = "$KrStages_AddFromTemplate";
                    public const string DefaultStageName = "$KrStages_AddFromTemplate";
            }
            public static class Dialog
            {
                    public static Guid ID = new Guid(0xbe14045d,0xf10e,0x4fc3,0x9b,0x6e,0x89,0x61,0xcc,0xc4,0x3c,0x49);
                    public const string Caption = "$KrStages_Dialog";
                    public const string DefaultStageName = "$KrStages_Dialog";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrProcessStageTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrProcessStageTypes Enumeration

    public sealed class KrProcessStageTypes
    {
        public readonly Guid ID;
        public readonly string Caption;
        public readonly string DefaultStageName;

        public KrProcessStageTypes(Guid ID, string Caption, string DefaultStageName)
        {
            this.ID = ID;
            this.Caption = Caption;
            this.DefaultStageName = DefaultStageName;
        }
    }

    #endregion

    #endregion

    #region KrRegistrationStageSettingsVirtual

    /// <summary>
    ///     ID: {cae44467-e2c1-4638-8444-857575455f80}
    ///     Alias: KrRegistrationStageSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrRegistrationStageSettingsVirtualSchemeInfo
    {
        private const string name = "KrRegistrationStageSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Comment = nameof(Comment);
        public readonly string CanEditCard = nameof(CanEditCard);
        public readonly string CanEditFiles = nameof(CanEditFiles);
        public readonly string WithoutTask = nameof(WithoutTask);

        #endregion

        #region ToString

        public static implicit operator string(KrRegistrationStageSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrRequestComment

    /// <summary>
    ///     ID: {db361bb6-d8d1-4645-8d9c-f296ce939c4b}
    ///     Alias: KrRequestComment
    ///     Group: Kr
    /// </summary>
    public sealed class KrRequestCommentSchemeInfo
    {
        private const string name = "KrRequestComment";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Comment = nameof(Comment);
        public readonly string AuthorRoleID = nameof(AuthorRoleID);
        public readonly string AuthorRoleName = nameof(AuthorRoleName);

        #endregion

        #region ToString

        public static implicit operator string(KrRequestCommentSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrResolutionActionVirtual

    /// <summary>
    ///     ID: {aed41831-bbfd-4637-8e5b-c9b69c9ca7f1}
    ///     Alias: KrResolutionActionVirtual
    ///     Group: KrWe
    ///     Description: Параметры действия \"Выполнение задачи\".
    /// </summary>
    public sealed class KrResolutionActionVirtualSchemeInfo
    {
        private const string name = "KrResolutionActionVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string KindID = nameof(KindID);
        public readonly string KindCaption = nameof(KindCaption);
        public readonly string Digest = nameof(Digest);
        public readonly string Period = nameof(Period);
        public readonly string Planned = nameof(Planned);
        public readonly string IsMajorPerformer = nameof(IsMajorPerformer);
        public readonly string IsMassCreation = nameof(IsMassCreation);
        public readonly string WithControl = nameof(WithControl);
        public readonly string ControllerID = nameof(ControllerID);
        public readonly string ControllerName = nameof(ControllerName);
        public readonly string SqlPerformersScript = nameof(SqlPerformersScript);
        public readonly string SenderID = nameof(SenderID);
        public readonly string SenderName = nameof(SenderName);

        #endregion

        #region ToString

        public static implicit operator string(KrResolutionActionVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrResolutionSettingsVirtual

    /// <summary>
    ///     ID: {5e584567-9e11-4741-ab3a-d96af0b6e0c9}
    ///     Alias: KrResolutionSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrResolutionSettingsVirtualSchemeInfo
    {
        private const string name = "KrResolutionSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string KindID = nameof(KindID);
        public readonly string KindCaption = nameof(KindCaption);
        public readonly string ControllerID = nameof(ControllerID);
        public readonly string ControllerName = nameof(ControllerName);
        public readonly string Comment = nameof(Comment);
        public readonly string Planned = nameof(Planned);
        public readonly string DurationInDays = nameof(DurationInDays);
        public readonly string WithControl = nameof(WithControl);
        public readonly string MassCreation = nameof(MassCreation);
        public readonly string MajorPerformer = nameof(MajorPerformer);
        public readonly string SenderID = nameof(SenderID);
        public readonly string SenderName = nameof(SenderName);

        #endregion

        #region ToString

        public static implicit operator string(KrResolutionSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrRouteInitializationActionVirtual

    /// <summary>
    ///     ID: {bbdef4f8-22b0-4075-83f2-1c6e89d1ba7b}
    ///     Alias: KrRouteInitializationActionVirtual
    ///     Group: KrWe
    ///     Description: Параметры действия \"Инициализация маршрута\".
    /// </summary>
    public sealed class KrRouteInitializationActionVirtualSchemeInfo
    {
        private const string name = "KrRouteInitializationActionVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string InitiatorID = nameof(InitiatorID);
        public readonly string InitiatorName = nameof(InitiatorName);
        public readonly string InitiatorComment = nameof(InitiatorComment);

        #endregion

        #region ToString

        public static implicit operator string(KrRouteInitializationActionVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrRouteModes

    /// <summary>
    ///     ID: {01c6933a-204d-490e-a6db-fc69345c7e32}
    ///     Alias: KrRouteModes
    ///     Group: Kr
    ///     Description: Перечисление режимов работы системы маршрутов.
    /// </summary>
    public sealed class KrRouteModesSchemeInfo
    {
        private const string name = "KrRouteModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class RoutesNotUsed
            {
                    public const int ID = 0;
                    public const string Name = "$KrRoute_Mode_RoutesNotUsed";
            }
            public static class RoutesUsed
            {
                    public const int ID = 1;
                    public const string Name = "$KrRoute_Mode_RoutesUsed";
            }
            public static class RoutesUsedProcessActive
            {
                    public const int ID = 2;
                    public const string Name = "$KrRoute_Mode_RoutesUsedProcessActive";
            }
            public static class RoutesUsedProcessInactive
            {
                    public const int ID = 3;
                    public const string Name = "$KrRoute_Mode_RoutesUsedProcessInactive";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrRouteModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrRouteModes Enumeration

    public sealed class KrRouteModes
    {
        public readonly int ID;
        public readonly string Name;

        public KrRouteModes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrRouteSettings

    /// <summary>
    ///     ID: {87619627-44a5-4f67-af9a-8f5736538f51}
    ///     Alias: KrRouteSettings
    ///     Group: Kr
    /// </summary>
    public sealed class KrRouteSettingsSchemeInfo
    {
        private const string name = "KrRouteSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AllowedRegistration = nameof(AllowedRegistration);
        public readonly string RouteModeID = nameof(RouteModeID);
        public readonly string RouteModeName = nameof(RouteModeName);

        #endregion

        #region ToString

        public static implicit operator string(KrRouteSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSamplePermissionsExtension

    /// <summary>
    ///     ID: {f45e2e3f-5559-4800-a9f7-45276924234b}
    ///     Alias: KrSamplePermissionsExtension
    ///     Group: Kr
    ///     Description: Таблица для примера расширения правил доступа типового решения.
    /// </summary>
    public sealed class KrSamplePermissionsExtensionSchemeInfo
    {
        private const string name = "KrSamplePermissionsExtension";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string MinAmount = nameof(MinAmount);
        public readonly string MaxAmount = nameof(MaxAmount);

        #endregion

        #region ToString

        public static implicit operator string(KrSamplePermissionsExtensionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSecondaryProcessCommonInfo

    /// <summary>
    ///     ID: {ce71fe9f-6ae4-4f76-8311-7ae54686a474}
    ///     Alias: KrSecondaryProcessCommonInfo
    ///     Group: Kr
    ///     Description: Содержит информацию по вторичному процессу.
    /// </summary>
    public sealed class KrSecondaryProcessCommonInfoSchemeInfo
    {
        private const string name = "KrSecondaryProcessCommonInfo";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string MainCardID = nameof(MainCardID);
        public readonly string CurrentApprovalStageRowID = nameof(CurrentApprovalStageRowID);
        public readonly string Info = nameof(Info);
        public readonly string SecondaryProcessID = nameof(SecondaryProcessID);
        public readonly string NestedWorkflowProcesses = nameof(NestedWorkflowProcesses);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string ProcessOwnerID = nameof(ProcessOwnerID);
        public readonly string ProcessOwnerName = nameof(ProcessOwnerName);

        #endregion

        #region ToString

        public static implicit operator string(KrSecondaryProcessCommonInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSecondaryProcesses

    /// <summary>
    ///     ID: {caac66aa-0cbb-4e2b-83fd-7c368e814d64}
    ///     Alias: KrSecondaryProcesses
    ///     Group: Kr
    /// </summary>
    public sealed class KrSecondaryProcessesSchemeInfo
    {
        private const string name = "KrSecondaryProcesses";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Description = nameof(Description);
        public readonly string TileGroup = nameof(TileGroup);
        public readonly string IsGlobal = nameof(IsGlobal);
        public readonly string Async = nameof(Async);
        public readonly string RefreshAndNotify = nameof(RefreshAndNotify);
        public readonly string Caption = nameof(Caption);
        public readonly string Tooltip = nameof(Tooltip);
        public readonly string Icon = nameof(Icon);
        public readonly string TileSizeID = nameof(TileSizeID);
        public readonly string TileSizeName = nameof(TileSizeName);
        public readonly string AskConfirmation = nameof(AskConfirmation);
        public readonly string ConfirmationMessage = nameof(ConfirmationMessage);
        public readonly string ActionGrouping = nameof(ActionGrouping);
        public readonly string VisibilitySqlCondition = nameof(VisibilitySqlCondition);
        public readonly string ExecutionSqlCondition = nameof(ExecutionSqlCondition);
        public readonly string VisibilitySourceCondition = nameof(VisibilitySourceCondition);
        public readonly string ExecutionSourceCondition = nameof(ExecutionSourceCondition);
        public readonly string ExecutionAccessDeniedMessage = nameof(ExecutionAccessDeniedMessage);
        public readonly string ModeID = nameof(ModeID);
        public readonly string ModeName = nameof(ModeName);
        public readonly string ActionID = nameof(ActionID);
        public readonly string ActionName = nameof(ActionName);
        public readonly string ActionEventType = nameof(ActionEventType);
        public readonly string AllowClientSideLaunch = nameof(AllowClientSideLaunch);
        public readonly string CheckRecalcRestrictions = nameof(CheckRecalcRestrictions);
        public readonly string RunOnce = nameof(RunOnce);
        public readonly string ButtonHotkey = nameof(ButtonHotkey);
        public readonly string Conditions = nameof(Conditions);
        public readonly string Order = nameof(Order);
        public readonly string NotMessageHasNoActiveStages = nameof(NotMessageHasNoActiveStages);

        #endregion

        #region ToString

        public static implicit operator string(KrSecondaryProcessesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSecondaryProcessGroupsVirtual

    /// <summary>
    ///     ID: {ba745c18-badf-4d8c-a26c-46619ba56b6f}
    ///     Alias: KrSecondaryProcessGroupsVirtual
    ///     Group: Kr
    /// </summary>
    public sealed class KrSecondaryProcessGroupsVirtualSchemeInfo
    {
        private const string name = "KrSecondaryProcessGroupsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StageGroupID = nameof(StageGroupID);
        public readonly string StageGroupName = nameof(StageGroupName);

        #endregion

        #region ToString

        public static implicit operator string(KrSecondaryProcessGroupsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSecondaryProcessModes

    /// <summary>
    ///     ID: {a8a8e7df-0237-4fda-824f-030df82a1030}
    ///     Alias: KrSecondaryProcessModes
    ///     Group: Kr
    /// </summary>
    public sealed class KrSecondaryProcessModesSchemeInfo
    {
        private const string name = "KrSecondaryProcessModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class PureProcess
            {
                    public const int ID = 0;
                    public const string Name = "$KrSecondaryProcess_Mode_PureProcess";
            }
            public static class Button
            {
                    public const int ID = 1;
                    public const string Name = "$KrSecondaryProcess_Mode_Button";
            }
            public static class Action
            {
                    public const int ID = 2;
                    public const string Name = "$KrSecondaryProcess_Mode_Action";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrSecondaryProcessModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrSecondaryProcessModes Enumeration

    public sealed class KrSecondaryProcessModes
    {
        public readonly int ID;
        public readonly string Name;

        public KrSecondaryProcessModes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrSecondaryProcessRoles

    /// <summary>
    ///     ID: {f7a6f1e2-a4c2-4f26-9c50-bc7e14dfc8ce}
    ///     Alias: KrSecondaryProcessRoles
    ///     Group: Kr
    ///     Description: Содержит роли для которых доступен для выполнения вторичный процесс.
    /// </summary>
    public sealed class KrSecondaryProcessRolesSchemeInfo
    {
        private const string name = "KrSecondaryProcessRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string IsContext = nameof(IsContext);

        #endregion

        #region ToString

        public static implicit operator string(KrSecondaryProcessRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettings

    /// <summary>
    ///     ID: {4a8403cf-6979-4e21-ad09-6956d681c405}
    ///     Alias: KrSettings
    ///     Group: Kr
    /// </summary>
    public sealed class KrSettingsSchemeInfo
    {
        private const string name = "KrSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AscendingApprovalList = nameof(AscendingApprovalList);
        public readonly string NotificationsDefaultLanguageID = nameof(NotificationsDefaultLanguageID);
        public readonly string NotificationsDefaultLanguageCaption = nameof(NotificationsDefaultLanguageCaption);
        public readonly string NotificationsDefaultLanguageCode = nameof(NotificationsDefaultLanguageCode);
        public readonly string PermissionsExtensionTypeID = nameof(PermissionsExtensionTypeID);
        public readonly string PermissionsExtensionTypeName = nameof(PermissionsExtensionTypeName);
        public readonly string PermissionsExtensionTypeCaption = nameof(PermissionsExtensionTypeCaption);
        public readonly string HideCommentForApprove = nameof(HideCommentForApprove);
        public readonly string AllowManualInputAndAutoCreatePartners = nameof(AllowManualInputAndAutoCreatePartners);
        public readonly string NotificationsDefaultFormatID = nameof(NotificationsDefaultFormatID);
        public readonly string NotificationsDefaultFormatName = nameof(NotificationsDefaultFormatName);
        public readonly string NotificationsDefaultFormatCaption = nameof(NotificationsDefaultFormatCaption);
        public readonly string HideLanguageSelection = nameof(HideLanguageSelection);
        public readonly string HideFormattingSelection = nameof(HideFormattingSelection);
        public readonly string AclReadCardAccess = nameof(AclReadCardAccess);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsCardTypes

    /// <summary>
    ///     ID: {949c3849-eb4b-4d64-9676-f14f9c40dbcf}
    ///     Alias: KrSettingsCardTypes
    ///     Group: Kr
    /// </summary>
    public sealed class KrSettingsCardTypesSchemeInfo
    {
        private const string name = "KrSettingsCardTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string CardTypeID = nameof(CardTypeID);
        public readonly string CardTypeCaption = nameof(CardTypeCaption);
        public readonly string UseDocTypes = nameof(UseDocTypes);
        public readonly string UseApproving = nameof(UseApproving);
        public readonly string DocNumberRegularAutoAssignmentID = nameof(DocNumberRegularAutoAssignmentID);
        public readonly string DocNumberRegularAutoAssignmentDescription = nameof(DocNumberRegularAutoAssignmentDescription);
        public readonly string DocNumberRegularSequence = nameof(DocNumberRegularSequence);
        public readonly string DocNumberRegularFormat = nameof(DocNumberRegularFormat);
        public readonly string AllowManualRegularDocNumberAssignment = nameof(AllowManualRegularDocNumberAssignment);
        public readonly string DocNumberRegistrationAutoAssignmentID = nameof(DocNumberRegistrationAutoAssignmentID);
        public readonly string DocNumberRegistrationAutoAssignmentDescription = nameof(DocNumberRegistrationAutoAssignmentDescription);
        public readonly string DocNumberRegistrationSequence = nameof(DocNumberRegistrationSequence);
        public readonly string DocNumberRegistrationFormat = nameof(DocNumberRegistrationFormat);
        public readonly string AllowManualRegistrationDocNumberAssignment = nameof(AllowManualRegistrationDocNumberAssignment);
        public readonly string UseRegistration = nameof(UseRegistration);
        public readonly string ReleaseRegularNumberOnFinalDeletion = nameof(ReleaseRegularNumberOnFinalDeletion);
        public readonly string ReleaseRegistrationNumberOnFinalDeletion = nameof(ReleaseRegistrationNumberOnFinalDeletion);
        public readonly string UseResolutions = nameof(UseResolutions);
        public readonly string DisableChildResolutionDateCheck = nameof(DisableChildResolutionDateCheck);
        public readonly string UseAutoApprove = nameof(UseAutoApprove);
        public readonly string ExceededDays = nameof(ExceededDays);
        public readonly string NotifyBefore = nameof(NotifyBefore);
        public readonly string AutoApproveComment = nameof(AutoApproveComment);
        public readonly string HideCreationButton = nameof(HideCreationButton);
        public readonly string HideRouteTab = nameof(HideRouteTab);
        public readonly string UseForum = nameof(UseForum);
        public readonly string UseDefaultDiscussionTab = nameof(UseDefaultDiscussionTab);
        public readonly string UseRoutesInWorkflowEngine = nameof(UseRoutesInWorkflowEngine);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsCardTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsCycleGrouping

    /// <summary>
    ///     ID: {4801bc15-cfaf-455c-aba6-cf77dd72484d}
    ///     Alias: KrSettingsCycleGrouping
    ///     Group: Kr
    /// </summary>
    public sealed class KrSettingsCycleGroupingSchemeInfo
    {
        private const string name = "KrSettingsCycleGrouping";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string TypeIsDocType = nameof(TypeIsDocType);
        public readonly string TypesRowID = nameof(TypesRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsCycleGroupingSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsCycleGroupingStates

    /// <summary>
    ///     ID: {11426c91-c7c4-4455-9eda-7ce5fd497982}
    ///     Alias: KrSettingsCycleGroupingStates
    ///     Group: Kr
    /// </summary>
    public sealed class KrSettingsCycleGroupingStatesSchemeInfo
    {
        private const string name = "KrSettingsCycleGroupingStates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);
        public readonly string TypesRowID = nameof(TypesRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsCycleGroupingStatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsCycleGroupingTypes

    /// <summary>
    ///     ID: {4012de1a-efd8-442d-a25c-8fe78008e38d}
    ///     Alias: KrSettingsCycleGroupingTypes
    ///     Group: Kr
    /// </summary>
    public sealed class KrSettingsCycleGroupingTypesSchemeInfo
    {
        private const string name = "KrSettingsCycleGroupingTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Description = nameof(Description);
        public readonly string DefaultModeID = nameof(DefaultModeID);
        public readonly string DefaultModeName = nameof(DefaultModeName);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsCycleGroupingTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsRouteDocTypes

    /// <summary>
    ///     ID: {9568db07-0f34-48ad-bab8-0d5e43d1846b}
    ///     Alias: KrSettingsRouteDocTypes
    ///     Group: Kr
    ///     Description: Разрешения по типам карточек или видам документов в маршрутах.
    /// </summary>
    public sealed class KrSettingsRouteDocTypesSchemeInfo
    {
        private const string name = "KrSettingsRouteDocTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string CardTypeID = nameof(CardTypeID);
        public readonly string CardTypeCaption = nameof(CardTypeCaption);
        public readonly string ParentRowID = nameof(ParentRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsRouteDocTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsRouteExtraTaskTypes

    /// <summary>
    ///     ID: {219b245d-a909-4517-8be6-d22ef7a28dba}
    ///     Alias: KrSettingsRouteExtraTaskTypes
    ///     Group: Kr
    /// </summary>
    public sealed class KrSettingsRouteExtraTaskTypesSchemeInfo
    {
        private const string name = "KrSettingsRouteExtraTaskTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TaskTypeID = nameof(TaskTypeID);
        public readonly string TaskTypeName = nameof(TaskTypeName);
        public readonly string TaskTypeCaption = nameof(TaskTypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsRouteExtraTaskTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsRoutePermissions

    /// <summary>
    ///     ID: {39e6d38f-4e35-45e9-8c71-42a932dce18c}
    ///     Alias: KrSettingsRoutePermissions
    ///     Group: Kr
    ///     Description: Разрешения в маршрутах - родительская таблица, каждой строкой которой является пересечение остальных таблиц.
    /// </summary>
    public sealed class KrSettingsRoutePermissionsSchemeInfo
    {
        private const string name = "KrSettingsRoutePermissions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsRoutePermissionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsRouteRoles

    /// <summary>
    ///     ID: {0f717b89-050d-4a3f-97fc-4520eed77540}
    ///     Alias: KrSettingsRouteRoles
    ///     Group: Kr
    ///     Description: Разрешения по ролям пользователя в маршрутах.
    /// </summary>
    public sealed class KrSettingsRouteRolesSchemeInfo
    {
        private const string name = "KrSettingsRouteRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StageRolesID = nameof(StageRolesID);
        public readonly string StageRolesName = nameof(StageRolesName);
        public readonly string ParentRowID = nameof(ParentRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsRouteRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsRouteStageGroups

    /// <summary>
    ///     ID: {9416f8fb-95b0-4617-98a6-f576580bfd49}
    ///     Alias: KrSettingsRouteStageGroups
    ///     Group: Kr
    ///     Description: Разрешения по группам этапов в маршрутах.
    /// </summary>
    public sealed class KrSettingsRouteStageGroupsSchemeInfo
    {
        private const string name = "KrSettingsRouteStageGroups";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StageGroupID = nameof(StageGroupID);
        public readonly string StageGroupName = nameof(StageGroupName);
        public readonly string ParentRowID = nameof(ParentRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsRouteStageGroupsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsRouteStageTypes

    /// <summary>
    ///     ID: {6681dd3c-cd54-405d-83bb-93ce533198fe}
    ///     Alias: KrSettingsRouteStageTypes
    ///     Group: Kr
    ///     Description: Разрешения по типам доступных этапов в маршрутах.
    /// </summary>
    public sealed class KrSettingsRouteStageTypesSchemeInfo
    {
        private const string name = "KrSettingsRouteStageTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StageTypeID = nameof(StageTypeID);
        public readonly string StageTypeCaption = nameof(StageTypeCaption);
        public readonly string ParentRowID = nameof(ParentRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsRouteStageTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsTaskAuthor

    /// <summary>
    ///     ID: {0748a117-8a2a-4198-a994-15d91094d6b7}
    ///     Alias: KrSettingsTaskAuthor
    ///     Group: Kr
    /// </summary>
    public sealed class KrSettingsTaskAuthorSchemeInfo
    {
        private const string name = "KrSettingsTaskAuthor";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string AuthorsRowID = nameof(AuthorsRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsTaskAuthorSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsTaskAuthorReplace

    /// <summary>
    ///     ID: {cfe4678d-369f-43a4-b103-32aecd9858a6}
    ///     Alias: KrSettingsTaskAuthorReplace
    ///     Group: Kr
    /// </summary>
    public sealed class KrSettingsTaskAuthorReplaceSchemeInfo
    {
        private const string name = "KrSettingsTaskAuthorReplace";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string AuthorReplaceID = nameof(AuthorReplaceID);
        public readonly string AuthorReplaceName = nameof(AuthorReplaceName);
        public readonly string AuthorsRowID = nameof(AuthorsRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsTaskAuthorReplaceSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSettingsTaskAuthors

    /// <summary>
    ///     ID: {afafd0bc-446e-4adf-8332-16be0b3d1908}
    ///     Alias: KrSettingsTaskAuthors
    ///     Group: Kr
    /// </summary>
    public sealed class KrSettingsTaskAuthorsSchemeInfo
    {
        private const string name = "KrSettingsTaskAuthors";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Description = nameof(Description);

        #endregion

        #region ToString

        public static implicit operator string(KrSettingsTaskAuthorsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSigningActionNotificationActionRolesVirtual

    /// <summary>
    ///     ID: {a6311a94-817e-48e0-afb5-6dca269563d1}
    ///     Alias: KrSigningActionNotificationActionRolesVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Согласование\". Таблица с ролями на которые отправляется уведомление при завершения действия с отпределённым вариантом завершения.
    /// </summary>
    public sealed class KrSigningActionNotificationActionRolesVirtualSchemeInfo
    {
        private const string name = "KrSigningActionNotificationActionRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string OptionRowID = nameof(OptionRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrSigningActionNotificationActionRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSigningActionNotificationRolesVirtual

    /// <summary>
    ///     ID: {7836e13c-4ebf-47f2-8968-504ab0d2fce4}
    ///     Alias: KrSigningActionNotificationRolesVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Подписание\". Таблица с обрабатываемыми вариантами завершения задания действия.
    /// </summary>
    public sealed class KrSigningActionNotificationRolesVirtualSchemeInfo
    {
        private const string name = "KrSigningActionNotificationRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string OptionRowID = nameof(OptionRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrSigningActionNotificationRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSigningActionOptionLinksVirtual

    /// <summary>
    ///     ID: {dd36aad9-d17a-41ad-b854-22f886819d28}
    ///     Alias: KrSigningActionOptionLinksVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Подписание\". Коллекционная секция объединяющая связи и вырианты завершения.
    /// </summary>
    public sealed class KrSigningActionOptionLinksVirtualSchemeInfo
    {
        private const string name = "KrSigningActionOptionLinksVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string LinkID = nameof(LinkID);
        public readonly string LinkName = nameof(LinkName);
        public readonly string LinkCaption = nameof(LinkCaption);
        public readonly string ActionOptionRowID = nameof(ActionOptionRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrSigningActionOptionLinksVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSigningActionOptionsActionVirtual

    /// <summary>
    ///     ID: {b4c6c410-c5cb-40e3-b800-3cd854c94a2c}
    ///     Alias: KrSigningActionOptionsActionVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Подписание\". Коллекционная секция содержащая параметры завершения действия.
    /// </summary>
    public sealed class KrSigningActionOptionsActionVirtualSchemeInfo
    {
        private const string name = "KrSigningActionOptionsActionVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string LinkID = nameof(LinkID);
        public readonly string ActionOptionID = nameof(ActionOptionID);
        public readonly string ActionOptionCaption = nameof(ActionOptionCaption);
        public readonly string Order = nameof(Order);
        public readonly string Script = nameof(Script);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);

        #endregion

        #region ToString

        public static implicit operator string(KrSigningActionOptionsActionVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSigningActionOptionsVirtual

    /// <summary>
    ///     ID: {3f87675e-0a60-4ece-a5c9-3c203e2c9ffb}
    ///     Alias: KrSigningActionOptionsVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Подписание\". Таблица с обрабатываемыми вариантами завершения задания действия.
    /// </summary>
    public sealed class KrSigningActionOptionsVirtualSchemeInfo
    {
        private const string name = "KrSigningActionOptionsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);
        public readonly string Script = nameof(Script);
        public readonly string Order = nameof(Order);
        public readonly string Result = nameof(Result);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string SendToPerformer = nameof(SendToPerformer);
        public readonly string SendToAuthor = nameof(SendToAuthor);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);
        public readonly string TaskTypeID = nameof(TaskTypeID);
        public readonly string TaskTypeName = nameof(TaskTypeName);
        public readonly string TaskTypeCaption = nameof(TaskTypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(KrSigningActionOptionsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSigningActionVirtual

    /// <summary>
    ///     ID: {baaceebe-011d-4f1a-9431-00c4f8b233b9}
    ///     Alias: KrSigningActionVirtual
    ///     Group: KrWe
    ///     Description: Параметры действия \"Подписание\".
    /// </summary>
    public sealed class KrSigningActionVirtualSchemeInfo
    {
        private const string name = "KrSigningActionVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string KindID = nameof(KindID);
        public readonly string KindCaption = nameof(KindCaption);
        public readonly string Digest = nameof(Digest);
        public readonly string Period = nameof(Period);
        public readonly string Planned = nameof(Planned);
        public readonly string InitTaskScript = nameof(InitTaskScript);
        public readonly string Result = nameof(Result);
        public readonly string IsParallel = nameof(IsParallel);
        public readonly string ReturnWhenApproved = nameof(ReturnWhenApproved);
        public readonly string CanEditCard = nameof(CanEditCard);
        public readonly string CanEditAnyFiles = nameof(CanEditAnyFiles);
        public readonly string ChangeStateOnStart = nameof(ChangeStateOnStart);
        public readonly string ChangeStateOnEnd = nameof(ChangeStateOnEnd);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);
        public readonly string SqlPerformersScript = nameof(SqlPerformersScript);
        public readonly string ExpectAllSigners = nameof(ExpectAllSigners);
        public readonly string AllowAdditionalApproval = nameof(AllowAdditionalApproval);

        #endregion

        #region ToString

        public static implicit operator string(KrSigningActionVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSigningStageSettingsVirtual

    /// <summary>
    ///     ID: {a53d9011-97c3-4890-97b8-c19c91ae8948}
    ///     Alias: KrSigningStageSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrSigningStageSettingsVirtualSchemeInfo
    {
        private const string name = "KrSigningStageSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string IsParallel = nameof(IsParallel);
        public readonly string ReturnToAuthor = nameof(ReturnToAuthor);
        public readonly string ReturnWhenDeclined = nameof(ReturnWhenDeclined);
        public readonly string CanEditCard = nameof(CanEditCard);
        public readonly string CanEditFiles = nameof(CanEditFiles);
        public readonly string Comment = nameof(Comment);
        public readonly string ChangeStateOnStart = nameof(ChangeStateOnStart);
        public readonly string ChangeStateOnEnd = nameof(ChangeStateOnEnd);
        public readonly string NotReturnEdit = nameof(NotReturnEdit);
        public readonly string AllowAdditionalApproval = nameof(AllowAdditionalApproval);

        #endregion

        #region ToString

        public static implicit operator string(KrSigningStageSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSigningTaskOptions

    /// <summary>
    ///     ID: {0ad2b029-2f30-4e19-96df-bc3c2dcd9dfe}
    ///     Alias: KrSigningTaskOptions
    ///     Group: KrStageTypes
    ///     Description: Таблица с параметрами задания \"Подписание\".
    /// </summary>
    public sealed class KrSigningTaskOptionsSchemeInfo
    {
        private const string name = "KrSigningTaskOptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AllowAdditionalApproval = nameof(AllowAdditionalApproval);

        #endregion

        #region ToString

        public static implicit operator string(KrSigningTaskOptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrSinglePerformerVirtual

    /// <summary>
    ///     ID: {52b86f8c-bc19-4dee-8e53-54236bf951a6}
    ///     Alias: KrSinglePerformerVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrSinglePerformerVirtualSchemeInfo
    {
        private const string name = "KrSinglePerformerVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string PerformerID = nameof(PerformerID);
        public readonly string PerformerName = nameof(PerformerName);

        #endregion

        #region ToString

        public static implicit operator string(KrSinglePerformerVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrStageCommonMethods

    /// <summary>
    ///     ID: {42a0388c-2064-4dbb-ba35-2ca8979af629}
    ///     Alias: KrStageCommonMethods
    ///     Group: Kr
    ///     Description: Основная таблица для базовых методов, используемых в шаблонах компиляции KrStageTemplate
    /// </summary>
    public sealed class KrStageCommonMethodsSchemeInfo
    {
        private const string name = "KrStageCommonMethods";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Description = nameof(Description);
        public readonly string Source = nameof(Source);

        #endregion

        #region ToString

        public static implicit operator string(KrStageCommonMethodsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrStageDocStates

    /// <summary>
    ///     ID: {4f6c7635-031d-411a-9219-069e05a7e8b6}
    ///     Alias: KrStageDocStates
    ///     Group: Kr
    /// </summary>
    public sealed class KrStageDocStatesSchemeInfo
    {
        private const string name = "KrStageDocStates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);

        #endregion

        #region ToString

        public static implicit operator string(KrStageDocStatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrStageGroups

    /// <summary>
    ///     ID: {fde6b6e3-f7b6-467f-96e1-e2df41a22f05}
    ///     Alias: KrStageGroups
    ///     Group: Kr
    /// </summary>
    public sealed class KrStageGroupsSchemeInfo
    {
        private const string name = "KrStageGroups";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Order = nameof(Order);
        public readonly string IsGroupReadonly = nameof(IsGroupReadonly);
        public readonly string SourceCondition = nameof(SourceCondition);
        public readonly string SourceBefore = nameof(SourceBefore);
        public readonly string SourceAfter = nameof(SourceAfter);
        public readonly string RuntimeSourceCondition = nameof(RuntimeSourceCondition);
        public readonly string RuntimeSourceBefore = nameof(RuntimeSourceBefore);
        public readonly string RuntimeSourceAfter = nameof(RuntimeSourceAfter);
        public readonly string SqlCondition = nameof(SqlCondition);
        public readonly string RuntimeSqlCondition = nameof(RuntimeSqlCondition);
        public readonly string Description = nameof(Description);
        public readonly string KrSecondaryProcessID = nameof(KrSecondaryProcessID);
        public readonly string KrSecondaryProcessName = nameof(KrSecondaryProcessName);
        public readonly string Ignore = nameof(Ignore);

        #endregion

        #region ToString

        public static implicit operator string(KrStageGroupsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrStageGroupTemplatesVirtual

    /// <summary>
    ///     ID: {f9d10aed-ae25-42e8-b936-1b97014c4e13}
    ///     Alias: KrStageGroupTemplatesVirtual
    ///     Group: Kr
    /// </summary>
    public sealed class KrStageGroupTemplatesVirtualSchemeInfo
    {
        private const string name = "KrStageGroupTemplatesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TemplateID = nameof(TemplateID);
        public readonly string TemplateName = nameof(TemplateName);

        #endregion

        #region ToString

        public static implicit operator string(KrStageGroupTemplatesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrStageRoles

    /// <summary>
    ///     ID: {97805de9-ed94-41a3-bf8b-fc1fa17c7d30}
    ///     Alias: KrStageRoles
    ///     Group: Kr
    ///     Description: Список ролей для шаблона этапа и группы этапов
    /// </summary>
    public sealed class KrStageRolesSchemeInfo
    {
        private const string name = "KrStageRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(KrStageRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrStages

    /// <summary>
    ///     ID: {92caadca-2409-40ff-b7d8-1d4fd302b1e9}
    ///     Alias: KrStages
    ///     Group: Kr
    /// </summary>
    public sealed class KrStagesSchemeInfo
    {
        private const string name = "KrStages";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);
        public readonly string Order = nameof(Order);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);
        public readonly string TimeLimit = nameof(TimeLimit);
        public readonly string SqlApproverRole = nameof(SqlApproverRole);
        public readonly string RowChanged = nameof(RowChanged);
        public readonly string OrderChanged = nameof(OrderChanged);
        public readonly string BasedOnStageRowID = nameof(BasedOnStageRowID);
        public readonly string BasedOnStageTemplateID = nameof(BasedOnStageTemplateID);
        public readonly string BasedOnStageTemplateName = nameof(BasedOnStageTemplateName);
        public readonly string BasedOnStageTemplateOrder = nameof(BasedOnStageTemplateOrder);
        public readonly string BasedOnStageTemplateGroupPositionID = nameof(BasedOnStageTemplateGroupPositionID);
        public readonly string StageTypeID = nameof(StageTypeID);
        public readonly string StageTypeCaption = nameof(StageTypeCaption);
        public readonly string DisplayTimeLimit = nameof(DisplayTimeLimit);
        public readonly string DisplayParticipants = nameof(DisplayParticipants);
        public readonly string DisplaySettings = nameof(DisplaySettings);
        public readonly string Settings = nameof(Settings);
        public readonly string Info = nameof(Info);
        public readonly string RuntimeSourceCondition = nameof(RuntimeSourceCondition);
        public readonly string RuntimeSourceBefore = nameof(RuntimeSourceBefore);
        public readonly string RuntimeSourceAfter = nameof(RuntimeSourceAfter);
        public readonly string StageGroupID = nameof(StageGroupID);
        public readonly string StageGroupOrder = nameof(StageGroupOrder);
        public readonly string StageGroupName = nameof(StageGroupName);
        public readonly string RuntimeSqlCondition = nameof(RuntimeSqlCondition);
        public readonly string Hidden = nameof(Hidden);
        public readonly string NestedProcessID = nameof(NestedProcessID);
        public readonly string ParentStageRowID = nameof(ParentStageRowID);
        public readonly string NestedOrder = nameof(NestedOrder);
        public readonly string ExtraSources = nameof(ExtraSources);
        public readonly string Planned = nameof(Planned);
        public readonly string Skip = nameof(Skip);
        public readonly string CanBeSkipped = nameof(CanBeSkipped);

        #endregion

        #region ToString

        public static implicit operator string(KrStagesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrStageState

    /// <summary>
    ///     ID: {beee4f3d-a385-4fc8-884f-bc1ccf55fc5b}
    ///     Alias: KrStageState
    ///     Group: Kr
    /// </summary>
    public sealed class KrStageStateSchemeInfo
    {
        private const string name = "KrStageState";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Inactive
            {
                    public const int ID = 0;
                    public const string Name = "$KrStates_Stage_Inactive";
            }
            public static class Active
            {
                    public const int ID = 1;
                    public const string Name = "$KrStates_Stage_Active";
            }
            public static class Completed
            {
                    public const int ID = 2;
                    public const string Name = "$KrStates_Stage_Completed";
            }
            public static class Skipped
            {
                    public const int ID = 3;
                    public const string Name = "$KrStates_Stage_Skipped";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrStageStateSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrStageState Enumeration

    public sealed class KrStageState
    {
        public readonly int ID;
        public readonly string Name;

        public KrStageState(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrStagesVirtual

    /// <summary>
    ///     ID: {89d78d5c-f8dd-48e7-868c-88bbafe74257}
    ///     Alias: KrStagesVirtual
    ///     Group: Kr
    /// </summary>
    public sealed class KrStagesVirtualSchemeInfo
    {
        private const string name = "KrStagesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);
        public readonly string Order = nameof(Order);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);
        public readonly string TimeLimit = nameof(TimeLimit);
        public readonly string SqlApproverRole = nameof(SqlApproverRole);
        public readonly string RowChanged = nameof(RowChanged);
        public readonly string OrderChanged = nameof(OrderChanged);
        public readonly string BasedOnStageRowID = nameof(BasedOnStageRowID);
        public readonly string BasedOnStageTemplateID = nameof(BasedOnStageTemplateID);
        public readonly string BasedOnStageTemplateName = nameof(BasedOnStageTemplateName);
        public readonly string BasedOnStageTemplateOrder = nameof(BasedOnStageTemplateOrder);
        public readonly string BasedOnStageTemplateGroupPositionID = nameof(BasedOnStageTemplateGroupPositionID);
        public readonly string StageTypeID = nameof(StageTypeID);
        public readonly string StageTypeCaption = nameof(StageTypeCaption);
        public readonly string DisplayTimeLimit = nameof(DisplayTimeLimit);
        public readonly string DisplayParticipants = nameof(DisplayParticipants);
        public readonly string DisplaySettings = nameof(DisplaySettings);
        public readonly string RuntimeSourceCondition = nameof(RuntimeSourceCondition);
        public readonly string RuntimeSourceBefore = nameof(RuntimeSourceBefore);
        public readonly string RuntimeSourceAfter = nameof(RuntimeSourceAfter);
        public readonly string StageGroupID = nameof(StageGroupID);
        public readonly string StageGroupName = nameof(StageGroupName);
        public readonly string StageGroupOrder = nameof(StageGroupOrder);
        public readonly string RuntimeSqlCondition = nameof(RuntimeSqlCondition);
        public readonly string Hidden = nameof(Hidden);
        public readonly string Planned = nameof(Planned);
        public readonly string Skip = nameof(Skip);
        public readonly string CanBeSkipped = nameof(CanBeSkipped);

        #endregion

        #region ToString

        public static implicit operator string(KrStagesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrStageTemplateGroupPosition

    /// <summary>
    ///     ID: {496c30f2-79d0-408a-8085-95b43d67a22b}
    ///     Alias: KrStageTemplateGroupPosition
    ///     Group: Kr
    ///     Description: Позиции, куда необходимо подставлять этапы из шаблона этапа KrStageTemplate
    /// </summary>
    public sealed class KrStageTemplateGroupPositionSchemeInfo
    {
        private const string name = "KrStageTemplateGroupPosition";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class AtFirst
            {
                    public const int ID = 0;
                    public const string Name = "$Views_KrStageTemplateGroupPosition_AtFirst";
            }
            public static class AtLast
            {
                    public const int ID = 1;
                    public const string Name = "$Views_KrStageTemplateGroupPosition_AtLast";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrStageTemplateGroupPositionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrStageTemplateGroupPosition Enumeration

    public sealed class KrStageTemplateGroupPosition
    {
        public readonly int ID;
        public readonly string Name;

        public KrStageTemplateGroupPosition(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region KrStageTemplates

    /// <summary>
    ///     ID: {5a33ac72-f6f5-4e5a-8d8c-4a94ed7bf324}
    ///     Alias: KrStageTemplates
    ///     Group: Kr
    ///     Description: Таблица с информацией по шаблонам этапов. Для карточки KrStageTemplate.
    /// </summary>
    public sealed class KrStageTemplatesSchemeInfo
    {
        private const string name = "KrStageTemplates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Order = nameof(Order);
        public readonly string Description = nameof(Description);
        public readonly string CanChangeOrder = nameof(CanChangeOrder);
        public readonly string IsStagesReadonly = nameof(IsStagesReadonly);
        public readonly string GroupPositionID = nameof(GroupPositionID);
        public readonly string GroupPositionName = nameof(GroupPositionName);
        public readonly string SqlCondition = nameof(SqlCondition);
        public readonly string SourceCondition = nameof(SourceCondition);
        public readonly string SourceBefore = nameof(SourceBefore);
        public readonly string SourceAfter = nameof(SourceAfter);
        public readonly string StageGroupID = nameof(StageGroupID);
        public readonly string StageGroupName = nameof(StageGroupName);

        #endregion

        #region ToString

        public static implicit operator string(KrStageTemplatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrStageTypes

    /// <summary>
    ///     ID: {971d8661-d445-42fb-84d0-b0b71aa978a2}
    ///     Alias: KrStageTypes
    ///     Group: Kr
    ///     Description: Список типов документов для шаблона этапа и группы этапов
    /// </summary>
    public sealed class KrStageTypesSchemeInfo
    {
        private const string name = "KrStageTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string TypeIsDocType = nameof(TypeIsDocType);

        #endregion

        #region ToString

        public static implicit operator string(KrStageTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrTask

    /// <summary>
    ///     ID: {51936147-e0ff-4e19-a7d1-0ea7d462ceec}
    ///     Alias: KrTask
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrTaskSchemeInfo
    {
        private const string name = "KrTask";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Comment = nameof(Comment);
        public readonly string DelegateID = nameof(DelegateID);
        public readonly string DelegateName = nameof(DelegateName);

        #endregion

        #region ToString

        public static implicit operator string(KrTaskSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrTaskCommentVirtual

    /// <summary>
    ///     ID: {344fa4e8-cdfc-4cb0-8634-9155c49fd21a}
    ///     Alias: KrTaskCommentVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrTaskCommentVirtualSchemeInfo
    {
        private const string name = "KrTaskCommentVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Comment = nameof(Comment);

        #endregion

        #region ToString

        public static implicit operator string(KrTaskCommentVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrTaskKindSettingsVirtual

    /// <summary>
    ///     ID: {80ab607a-d43f-435d-a1be-a203bb99c2d3}
    ///     Alias: KrTaskKindSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrTaskKindSettingsVirtualSchemeInfo
    {
        private const string name = "KrTaskKindSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string KindID = nameof(KindID);
        public readonly string KindCaption = nameof(KindCaption);

        #endregion

        #region ToString

        public static implicit operator string(KrTaskKindSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrTaskRegistrationActionNotificationRolesVitrual

    /// <summary>
    ///     ID: {406b3337-8cfc-437d-a7fc-408b96a92c00}
    ///     Alias: KrTaskRegistrationActionNotificationRolesVitrual
    ///     Group: KrWe
    ///     Description: Действие \"Задание регистрации\". Коллекционная секция содержащая роли на которые отправляется уведомление при завершении задания.
    /// </summary>
    public sealed class KrTaskRegistrationActionNotificationRolesVitrualSchemeInfo
    {
        private const string name = "KrTaskRegistrationActionNotificationRolesVitrual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string OptionRowID = nameof(OptionRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrTaskRegistrationActionNotificationRolesVitrualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrTaskRegistrationActionOptionLinksVirtual

    /// <summary>
    ///     ID: {f6a8f11a-68c2-4743-a8ed-236fe459dfc9}
    ///     Alias: KrTaskRegistrationActionOptionLinksVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Задание регистрации\". Коллекционная секция объединяющая связи и вырианты завершения.
    /// </summary>
    public sealed class KrTaskRegistrationActionOptionLinksVirtualSchemeInfo
    {
        private const string name = "KrTaskRegistrationActionOptionLinksVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string LinkID = nameof(LinkID);
        public readonly string LinkName = nameof(LinkName);
        public readonly string LinkCaption = nameof(LinkCaption);
        public readonly string OptionRowID = nameof(OptionRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrTaskRegistrationActionOptionLinksVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrTaskRegistrationActionOptionsVirtual

    /// <summary>
    ///     ID: {2ba2b1a3-b8ad-4c47-a8fd-3a3fa421c7a9}
    ///     Alias: KrTaskRegistrationActionOptionsVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Задание регистрации\". Таблица с обрабатываемыми вариантами завершения задания действия.
    /// </summary>
    public sealed class KrTaskRegistrationActionOptionsVirtualSchemeInfo
    {
        private const string name = "KrTaskRegistrationActionOptionsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);
        public readonly string LinkID = nameof(LinkID);
        public readonly string Script = nameof(Script);
        public readonly string Order = nameof(Order);
        public readonly string Result = nameof(Result);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string SendToPerformer = nameof(SendToPerformer);
        public readonly string SendToAuthor = nameof(SendToAuthor);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);

        #endregion

        #region ToString

        public static implicit operator string(KrTaskRegistrationActionOptionsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrTaskRegistrationActionVirtual

    /// <summary>
    ///     ID: {12b90f64-b971-4198-ad0e-0e3d1988f946}
    ///     Alias: KrTaskRegistrationActionVirtual
    ///     Group: KrWe
    ///     Description: Праметры действия \"Задание регистрации\".
    /// </summary>
    public sealed class KrTaskRegistrationActionVirtualSchemeInfo
    {
        private const string name = "KrTaskRegistrationActionVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string PerformerID = nameof(PerformerID);
        public readonly string PerformerName = nameof(PerformerName);
        public readonly string KindID = nameof(KindID);
        public readonly string KindCaption = nameof(KindCaption);
        public readonly string Digest = nameof(Digest);
        public readonly string Period = nameof(Period);
        public readonly string Planned = nameof(Planned);
        public readonly string InitTaskScript = nameof(InitTaskScript);
        public readonly string Result = nameof(Result);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);
        public readonly string CanEditCard = nameof(CanEditCard);
        public readonly string CanEditAnyFiles = nameof(CanEditAnyFiles);

        #endregion

        #region ToString

        public static implicit operator string(KrTaskRegistrationActionVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrTaskTypeCondition

    /// <summary>
    ///     ID: {0209ddc9-6457-406e-8a09-ab8ac6916e26}
    ///     Alias: KrTaskTypeCondition
    ///     Group: Kr
    ///     Description: Основная секция с тиами заданий для настройки условия \"По типу заданий\"
    /// </summary>
    public sealed class KrTaskTypeConditionSchemeInfo
    {
        private const string name = "KrTaskTypeCondition";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TaskTypeID = nameof(TaskTypeID);
        public readonly string TaskTypeCaption = nameof(TaskTypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(KrTaskTypeConditionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrTaskTypeConditionSettings

    /// <summary>
    ///     ID: {4b9735f4-7db4-46e1-bbdd-4bd71f5234bd}
    ///     Alias: KrTaskTypeConditionSettings
    ///     Group: Kr
    /// </summary>
    public sealed class KrTaskTypeConditionSettingsSchemeInfo
    {
        private const string name = "KrTaskTypeConditionSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string InProgress = nameof(InProgress);
        public readonly string IsAuthor = nameof(IsAuthor);
        public readonly string IsPerformer = nameof(IsPerformer);

        #endregion

        #region ToString

        public static implicit operator string(KrTaskTypeConditionSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrTypedTaskSettingsVirtual

    /// <summary>
    ///     ID: {e06fa88f-35a2-48fc-8ce4-6e20521b5238}
    ///     Alias: KrTypedTaskSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrTypedTaskSettingsVirtualSchemeInfo
    {
        private const string name = "KrTypedTaskSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string TaskTypeID = nameof(TaskTypeID);
        public readonly string TaskTypeName = nameof(TaskTypeName);
        public readonly string TaskTypeCaption = nameof(TaskTypeCaption);
        public readonly string AfterTaskCompletion = nameof(AfterTaskCompletion);
        public readonly string TaskDigest = nameof(TaskDigest);

        #endregion

        #region ToString

        public static implicit operator string(KrTypedTaskSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrUniversalTaskActionButtonLinksVirtual

    /// <summary>
    ///     ID: {0938b2e9-485e-4f87-8622-706bbaf0efb7}
    ///     Alias: KrUniversalTaskActionButtonLinksVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Настраиваемое задание\". Коллекционная секция объединяющая связи и вырианты завершения.
    /// </summary>
    public sealed class KrUniversalTaskActionButtonLinksVirtualSchemeInfo
    {
        private const string name = "KrUniversalTaskActionButtonLinksVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string LinkID = nameof(LinkID);
        public readonly string LinkName = nameof(LinkName);
        public readonly string LinkCaption = nameof(LinkCaption);
        public readonly string ButtonRowID = nameof(ButtonRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrUniversalTaskActionButtonLinksVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrUniversalTaskActionButtonsVirtual

    /// <summary>
    ///     ID: {e85631c4-0014-4842-86f4-9a6ba66166f3}
    ///     Alias: KrUniversalTaskActionButtonsVirtual
    ///     Group: KrWe
    ///     Description: Действие \"Настраиваемое задание\". Параметры настраиваемых вариантов завершения.
    /// </summary>
    public sealed class KrUniversalTaskActionButtonsVirtualSchemeInfo
    {
        private const string name = "KrUniversalTaskActionButtonsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Order = nameof(Order);
        public readonly string Caption = nameof(Caption);
        public readonly string Digest = nameof(Digest);
        public readonly string IsShowComment = nameof(IsShowComment);
        public readonly string IsAdditionalOption = nameof(IsAdditionalOption);
        public readonly string LinkID = nameof(LinkID);
        public readonly string Script = nameof(Script);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string SendToPerformer = nameof(SendToPerformer);
        public readonly string SendToAuthor = nameof(SendToAuthor);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);
        public readonly string OptionID = nameof(OptionID);

        #endregion

        #region ToString

        public static implicit operator string(KrUniversalTaskActionButtonsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrUniversalTaskActionNotificationRolesVitrual

    /// <summary>
    ///     ID: {1d7d2be8-692e-478d-9ce4-fc791833ffba}
    ///     Alias: KrUniversalTaskActionNotificationRolesVitrual
    ///     Group: KrWe
    ///     Description: Действие \"Настраиваемое задание\". Коллекционная секция содержащая роли на которые отправляется уведомление при завершении задания.
    /// </summary>
    public sealed class KrUniversalTaskActionNotificationRolesVitrualSchemeInfo
    {
        private const string name = "KrUniversalTaskActionNotificationRolesVitrual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string ButtonRowID = nameof(ButtonRowID);

        #endregion

        #region ToString

        public static implicit operator string(KrUniversalTaskActionNotificationRolesVitrualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrUniversalTaskActionVirtual

    /// <summary>
    ///     ID: {b0ca69b1-7c90-4ce7-995c-2f9540ec45ef}
    ///     Alias: KrUniversalTaskActionVirtual
    ///     Group: KrWe
    ///     Description: Параметры действия \"Настраиваемое задание\".
    /// </summary>
    public sealed class KrUniversalTaskActionVirtualSchemeInfo
    {
        private const string name = "KrUniversalTaskActionVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string KindID = nameof(KindID);
        public readonly string KindCaption = nameof(KindCaption);
        public readonly string Digest = nameof(Digest);
        public readonly string Period = nameof(Period);
        public readonly string Planned = nameof(Planned);
        public readonly string InitTaskScript = nameof(InitTaskScript);
        public readonly string Result = nameof(Result);
        public readonly string CanEditCard = nameof(CanEditCard);
        public readonly string CanEditAnyFiles = nameof(CanEditAnyFiles);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(KrUniversalTaskActionVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrUniversalTaskOptions

    /// <summary>
    ///     ID: {470ddc9e-4715-4efa-bd25-cbb9f4033162}
    ///     Alias: KrUniversalTaskOptions
    ///     Group: KrStageTypes
    ///     Description: Секция с данными универсального задания
    /// </summary>
    public sealed class KrUniversalTaskOptionsSchemeInfo
    {
        private const string name = "KrUniversalTaskOptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string OptionID = nameof(OptionID);
        public readonly string Caption = nameof(Caption);
        public readonly string ShowComment = nameof(ShowComment);
        public readonly string Additional = nameof(Additional);
        public readonly string Order = nameof(Order);
        public readonly string Message = nameof(Message);

        #endregion

        #region ToString

        public static implicit operator string(KrUniversalTaskOptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrUniversalTaskOptionsSettingsVirtual

    /// <summary>
    ///     ID: {49f11daf-636a-4342-aa2b-ea798bed7263}
    ///     Alias: KrUniversalTaskOptionsSettingsVirtual
    ///     Group: KrStageTypes
    ///     Description: Секция с вариантами завершения этапа универсального задания
    /// </summary>
    public sealed class KrUniversalTaskOptionsSettingsVirtualSchemeInfo
    {
        private const string name = "KrUniversalTaskOptionsSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string OptionID = nameof(OptionID);
        public readonly string Caption = nameof(Caption);
        public readonly string ShowComment = nameof(ShowComment);
        public readonly string Additional = nameof(Additional);
        public readonly string Order = nameof(Order);
        public readonly string Message = nameof(Message);

        #endregion

        #region ToString

        public static implicit operator string(KrUniversalTaskOptionsSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrUniversalTaskSettingsVirtual

    /// <summary>
    ///     ID: {01db90f2-22ec-4233-a5fd-587a832b1b48}
    ///     Alias: KrUniversalTaskSettingsVirtual
    ///     Group: KrStageTypes
    ///     Description: Секция настроек этапа Универсальное задание
    /// </summary>
    public sealed class KrUniversalTaskSettingsVirtualSchemeInfo
    {
        private const string name = "KrUniversalTaskSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Digest = nameof(Digest);
        public readonly string CanEditCard = nameof(CanEditCard);
        public readonly string CanEditFiles = nameof(CanEditFiles);

        #endregion

        #region ToString

        public static implicit operator string(KrUniversalTaskSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrUsersCondition

    /// <summary>
    ///     ID: {c9cf90e4-72e2-4cd2-a798-62b1d856cea5}
    ///     Alias: KrUsersCondition
    ///     Group: Kr
    ///     Description: Секция для условий для правил уведомлений, проверяющих принадлежность сотрудника
    /// </summary>
    public sealed class KrUsersConditionSchemeInfo
    {
        private const string name = "KrUsersCondition";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);

        #endregion

        #region ToString

        public static implicit operator string(KrUsersConditionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrUserSettingsVirtual

    /// <summary>
    ///     ID: {84d45612-8cbb-4b7d-91eb-2796003a365d}
    ///     Alias: KrUserSettingsVirtual
    ///     Group: KrStageTypes
    /// </summary>
    public sealed class KrUserSettingsVirtualSchemeInfo
    {
        private const string name = "KrUserSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string DisableTaskPopupNotifications = nameof(DisableTaskPopupNotifications);

        #endregion

        #region ToString

        public static implicit operator string(KrUserSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrVirtualFileCardTypes

    /// <summary>
    ///     ID: {5ad723d6-21d8-48c2-8799-a1ba9fb1c758}
    ///     Alias: KrVirtualFileCardTypes
    ///     Group: Kr
    ///     Description: Типы карточек для карточки виртуального файла
    /// </summary>
    public sealed class KrVirtualFileCardTypesSchemeInfo
    {
        private const string name = "KrVirtualFileCardTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(KrVirtualFileCardTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrVirtualFileRoles

    /// <summary>
    ///     ID: {9d6186ba-f7ae-4910-8784-6c63a3b13179}
    ///     Alias: KrVirtualFileRoles
    ///     Group: Kr
    ///     Description: Роли для карточки виртуального файла
    /// </summary>
    public sealed class KrVirtualFileRolesSchemeInfo
    {
        private const string name = "KrVirtualFileRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(KrVirtualFileRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrVirtualFiles

    /// <summary>
    ///     ID: {006f8d09-5eff-46fc-8bc5-d7c5f6a81d44}
    ///     Alias: KrVirtualFiles
    ///     Group: Kr
    ///     Description: Основная секция для карточи виртуального файла
    /// </summary>
    public sealed class KrVirtualFilesSchemeInfo
    {
        private const string name = "KrVirtualFiles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string FileName = nameof(FileName);
        public readonly string FileID = nameof(FileID);
        public readonly string FileVersionID = nameof(FileVersionID);
        public readonly string InitializationScenario = nameof(InitializationScenario);
        public readonly string FileTemplateID = nameof(FileTemplateID);
        public readonly string FileTemplateName = nameof(FileTemplateName);
        public readonly string FileCategoryID = nameof(FileCategoryID);
        public readonly string FileCategoryName = nameof(FileCategoryName);
        public readonly string Conditions = nameof(Conditions);

        #endregion

        #region ToString

        public static implicit operator string(KrVirtualFilesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrVirtualFileStates

    /// <summary>
    ///     ID: {a6386cf3-fff8-401e-b8d2-222d0221951f}
    ///     Alias: KrVirtualFileStates
    ///     Group: Kr
    ///     Description: Состояния для карточки виртуального файла
    /// </summary>
    public sealed class KrVirtualFileStatesSchemeInfo
    {
        private const string name = "KrVirtualFileStates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);

        #endregion

        #region ToString

        public static implicit operator string(KrVirtualFileStatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrVirtualFileVersions

    /// <summary>
    ///     ID: {8bd27c6a-16e0-4ac4-a147-8cc2d30dc88b}
    ///     Alias: KrVirtualFileVersions
    ///     Group: Kr
    /// </summary>
    public sealed class KrVirtualFileVersionsSchemeInfo
    {
        private const string name = "KrVirtualFileVersions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string FileName = nameof(FileName);
        public readonly string FileVersionID = nameof(FileVersionID);
        public readonly string FileTemplateID = nameof(FileTemplateID);
        public readonly string FileTemplateName = nameof(FileTemplateName);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(KrVirtualFileVersionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrWeActionCompletionOptions

    /// <summary>
    ///     ID: {6a24d3cd-ec83-4e7a-8815-77b054c69371}
    ///     Alias: KrWeActionCompletionOptions
    ///     Group: KrWe
    ///     Description: Список возможных вариантов завершения действий.
    /// </summary>
    public sealed class KrWeActionCompletionOptionsSchemeInfo
    {
        private const string name = "KrWeActionCompletionOptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Approved
            {
                    public static Guid ID = new Guid(0x4339a03f,0x234d,0x4a9a,0xa6,0xe4,0x58,0xa8,0x8a,0x5a,0x03,0xce);
                    public const string Name = "Approved";
                    public const string Caption = "$KrAction_ActionCompletionOption_Approved";
            }
            public static class Disapproved
            {
                    public static Guid ID = new Guid(0x6fbdd34b,0xbe9a,0x40bf,0x90,0xcb,0x16,0x40,0xd4,0xab,0xb9,0xf5);
                    public const string Name = "Disapproved";
                    public const string Caption = "$KrAction_ActionCompletionOption_Disapproved";
            }
            public static class Signed
            {
                    public static Guid ID = new Guid(0xfa94b7bf,0x7b99,0x46d6,0x9c,0x65,0xb2,0x1a,0x48,0x3e,0xbc,0x45);
                    public const string Name = "Signed";
                    public const string Caption = "$KrAction_ActionCompletionOption_Signed";
            }
            public static class Declined
            {
                    public static Guid ID = new Guid(0x4a1936c7,0x1f94,0x4897,0x9d,0xae,0x93,0x41,0x63,0xe2,0xfe,0x1c);
                    public const string Name = "Declined";
                    public const string Caption = "$KrAction_ActionCompletionOption_Declined";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(KrWeActionCompletionOptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region KrWeActionCompletionOptions Enumeration

    public sealed class KrWeActionCompletionOptions
    {
        public readonly Guid ID;
        public readonly string Name;
        public readonly string Caption;

        public KrWeActionCompletionOptions(Guid ID, string Name, string Caption)
        {
            this.ID = ID;
            this.Name = Name;
            this.Caption = Caption;
        }
    }

    #endregion

    #endregion

    #region KrWeAdditionalApprovalOptionsVirtual

    /// <summary>
    ///     ID: {54829879-8b8e-4d47-a27b-0346e93e6e45}
    ///     Alias: KrWeAdditionalApprovalOptionsVirtual
    ///     Group: KrWe
    ///     Description: Параметры дополнительного согласования.
    /// </summary>
    public sealed class KrWeAdditionalApprovalOptionsVirtualSchemeInfo
    {
        private const string name = "KrWeAdditionalApprovalOptionsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);
        public readonly string InitTaskScript = nameof(InitTaskScript);

        #endregion

        #region ToString

        public static implicit operator string(KrWeAdditionalApprovalOptionsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrWeEditInterjectOptionsVirtual

    /// <summary>
    ///     ID: {d0771103-6c21-4602-af56-d264e82f8b57}
    ///     Alias: KrWeEditInterjectOptionsVirtual
    ///     Group: KrWe
    ///     Description: Параметры доработки автором.
    /// </summary>
    public sealed class KrWeEditInterjectOptionsVirtualSchemeInfo
    {
        private const string name = "KrWeEditInterjectOptionsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string KindID = nameof(KindID);
        public readonly string KindCaption = nameof(KindCaption);
        public readonly string Digest = nameof(Digest);
        public readonly string Period = nameof(Period);
        public readonly string Planned = nameof(Planned);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);
        public readonly string InitTaskScript = nameof(InitTaskScript);

        #endregion

        #region ToString

        public static implicit operator string(KrWeEditInterjectOptionsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrWeRequestCommentOptionsVirtual

    /// <summary>
    ///     ID: {abc045c0-75b1-4a25-8d9e-d6b323118f08}
    ///     Alias: KrWeRequestCommentOptionsVirtual
    ///     Group: KrWe
    ///     Description: Параметры запроса комментария.
    /// </summary>
    public sealed class KrWeRequestCommentOptionsVirtualSchemeInfo
    {
        private const string name = "KrWeRequestCommentOptionsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);
        public readonly string InitTaskScript = nameof(InitTaskScript);

        #endregion

        #region ToString

        public static implicit operator string(KrWeRequestCommentOptionsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region KrWeRolesVirtual

    /// <summary>
    ///     ID: {eea339fd-2e18-415b-b338-418f84ac961e}
    ///     Alias: KrWeRolesVirtual
    ///     Group: KrWe
    ///     Description: Роли.
    /// </summary>
    public sealed class KrWeRolesVirtualSchemeInfo
    {
        private const string name = "KrWeRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(KrWeRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Languages

    /// <summary>
    ///     ID: {1ed36bf1-2ebf-43da-acb2-1ddb3298dbd8}
    ///     Alias: Languages
    ///     Group: System
    /// </summary>
    public sealed class LanguagesSchemeInfo
    {
        private const string name = "Languages";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Caption = nameof(Caption);
        public readonly string Code = nameof(Code);
        public readonly string FallbackID = nameof(FallbackID);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Srpski
            {
                    public const int ID = 2074;
                    public const string Caption = "Srpski";
                    public const string Code = "sr";
                    public static int? FallbackID = 0;
            }
            public static class Slovenscina
            {
                    public const int ID = 1060;
                    public const string Caption = "Slovenscina";
                    public const string Code = "sl";
                    public static int? FallbackID = 0;
            }
            public static class Hrvatski
            {
                    public const int ID = 1050;
                    public const string Caption = "Hrvatski";
                    public const string Code = "hr";
                    public static int? FallbackID = 0;
            }
            public static class English
            {
                    public const int ID = 0;
                    public const string Caption = "English";
                    public const string Code = "en";
                    public static int? FallbackID = null;
            }
            public static class Russkiy
            {
                    public const int ID = 1;
                    public const string Caption = "Русский";
                    public const string Code = "ru";
                    public static int? FallbackID = null;
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(LanguagesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region Languages Enumeration

    public sealed class Languages
    {
        public readonly int ID;
        public readonly string Caption;
        public readonly string Code;
        public readonly int? FallbackID;

        public Languages(int ID, string Caption, string Code, int? FallbackID)
        {
            this.ID = ID;
            this.Caption = Caption;
            this.Code = Code;
            this.FallbackID = FallbackID;
        }
    }

    #endregion

    #endregion

    #region LawAdministrators

    /// <summary>
    ///     ID: {3dbb9a1f-ae27-4612-aec1-4f077494dfef}
    ///     Alias: LawAdministrators
    ///     Group: LawList
    /// </summary>
    public sealed class LawAdministratorsSchemeInfo
    {
        private const string name = "LawAdministrators";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);

        #endregion

        #region ToString

        public static implicit operator string(LawAdministratorsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region LawCase

    /// <summary>
    ///     ID: {191308f6-820e-408e-b779-cef96b1b09c0}
    ///     Alias: LawCase
    ///     Group: Law
    /// </summary>
    public sealed class LawCaseSchemeInfo
    {
        private const string name = "LawCase";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ClassificationPlanID = nameof(ClassificationPlanID);
        public readonly string ClassificationPlanPlan = nameof(ClassificationPlanPlan);
        public readonly string ClassificationPlanName = nameof(ClassificationPlanName);
        public readonly string ClassificationPlanFullName = nameof(ClassificationPlanFullName);
        public readonly string NumberByCourt = nameof(NumberByCourt);
        public readonly string LocationID = nameof(LocationID);
        public readonly string LocationName = nameof(LocationName);
        public readonly string CategoryID = nameof(CategoryID);
        public readonly string CategoryName = nameof(CategoryName);
        public readonly string Number = nameof(Number);
        public readonly string Date = nameof(Date);
        public readonly string DecisionDate = nameof(DecisionDate);
        public readonly string PCTO = nameof(PCTO);
        public readonly string IsLimitedAccess = nameof(IsLimitedAccess);
        public readonly string IsArchive = nameof(IsArchive);
        public readonly string Description = nameof(Description);

        #endregion

        #region ToString

        public static implicit operator string(LawCaseSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region LawClients

    /// <summary>
    ///     ID: {362c9171-6267-42a8-8fd8-7bf39d04533e}
    ///     Alias: LawClients
    ///     Group: LawList
    /// </summary>
    public sealed class LawClientsSchemeInfo
    {
        private const string name = "LawClients";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ClientID = nameof(ClientID);
        public readonly string ClientName = nameof(ClientName);

        #endregion

        #region ToString

        public static implicit operator string(LawClientsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region LawPartnerRepresentatives

    /// <summary>
    ///     ID: {b4cfec48-deec-40fc-94fc-eae9e645afce}
    ///     Alias: LawPartnerRepresentatives
    ///     Group: LawList
    /// </summary>
    public sealed class LawPartnerRepresentativesSchemeInfo
    {
        private const string name = "LawPartnerRepresentatives";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RepresentativeID = nameof(RepresentativeID);
        public readonly string RepresentativeName = nameof(RepresentativeName);
        public readonly string RepresentativeAddressID = nameof(RepresentativeAddressID);
        public readonly string RepresentativeTaxNumber = nameof(RepresentativeTaxNumber);
        public readonly string RepresentativeRegistrationNumber = nameof(RepresentativeRegistrationNumber);
        public readonly string RepresentativeContacts = nameof(RepresentativeContacts);
        public readonly string RepresentativeStreet = nameof(RepresentativeStreet);
        public readonly string RepresentativePostalCode = nameof(RepresentativePostalCode);
        public readonly string RepresentativeCity = nameof(RepresentativeCity);
        public readonly string RepresentativeCountry = nameof(RepresentativeCountry);
        public readonly string RepresentativePoBox = nameof(RepresentativePoBox);

        #endregion

        #region ToString

        public static implicit operator string(LawPartnerRepresentativesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region LawPartners

    /// <summary>
    ///     ID: {54244411-fea4-4bdd-b009-fad9c5915882}
    ///     Alias: LawPartners
    ///     Group: LawList
    /// </summary>
    public sealed class LawPartnersSchemeInfo
    {
        private const string name = "LawPartners";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string PartnerID = nameof(PartnerID);
        public readonly string PartnerName = nameof(PartnerName);
        public readonly string PartnerAddressID = nameof(PartnerAddressID);
        public readonly string PartnerTaxNumber = nameof(PartnerTaxNumber);
        public readonly string PartnerRegistrationNumber = nameof(PartnerRegistrationNumber);
        public readonly string PartnerContacts = nameof(PartnerContacts);
        public readonly string PartnerStreet = nameof(PartnerStreet);
        public readonly string PartnerPostalCode = nameof(PartnerPostalCode);
        public readonly string PartnerCity = nameof(PartnerCity);
        public readonly string PartnerCountry = nameof(PartnerCountry);
        public readonly string PartnerPoBox = nameof(PartnerPoBox);

        #endregion

        #region ToString

        public static implicit operator string(LawPartnersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region LawPartnersDialogVirtual

    /// <summary>
    ///     ID: {c0b9d91c-4a9e-4c7a-b7c2-13daa37a1cf9}
    ///     Alias: LawPartnersDialogVirtual
    ///     Group: LawList
    ///     Description: Виртуальная таблица для редактирования компаний
    /// </summary>
    public sealed class LawPartnersDialogVirtualSchemeInfo
    {
        private const string name = "LawPartnersDialogVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);
        public readonly string AddressID = nameof(AddressID);
        public readonly string TaxNumber = nameof(TaxNumber);
        public readonly string RegistrationNumber = nameof(RegistrationNumber);
        public readonly string Contacts = nameof(Contacts);
        public readonly string Street = nameof(Street);
        public readonly string PostalCode = nameof(PostalCode);
        public readonly string City = nameof(City);
        public readonly string Country = nameof(Country);
        public readonly string PoBox = nameof(PoBox);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(LawPartnersDialogVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region LawUsers

    /// <summary>
    ///     ID: {0b3ce213-cc34-469d-a0dd-19a4643b1a49}
    ///     Alias: LawUsers
    ///     Group: LawList
    /// </summary>
    public sealed class LawUsersSchemeInfo
    {
        private const string name = "LawUsers";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string UserWorkplace = nameof(UserWorkplace);

        #endregion

        #region ToString

        public static implicit operator string(LawUsersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region LicenseTypes

    /// <summary>
    ///     ID: {bcc286d4-9d77-4750-8084-15417b966528}
    ///     Alias: LicenseTypes
    ///     Group: System
    ///     Description: Типы лицензий для сессий.
    /// </summary>
    public sealed class LicenseTypesSchemeInfo
    {
        private const string name = "LicenseTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Unspecified
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_SessionTypes_Unspecified";
            }
            public static class Concurrent
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_SessionTypes_Concurrent";
            }
            public static class Personal
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_SessionTypes_Personal";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(LicenseTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region LicenseTypes Enumeration

    public sealed class LicenseTypes
    {
        public readonly int ID;
        public readonly string Name;

        public LicenseTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region LicenseVirtual

    /// <summary>
    ///     ID: {f81a7db5-a883-49e0-918c-59a5967828b5}
    ///     Alias: LicenseVirtual
    ///     Group: System
    ///     Description: Виртуальная секция для настроек лицензий.
    /// </summary>
    public sealed class LicenseVirtualSchemeInfo
    {
        private const string name = "LicenseVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ConcurrentCount = nameof(ConcurrentCount);
        public readonly string ConcurrentLimit = nameof(ConcurrentLimit);
        public readonly string ConcurrentText = nameof(ConcurrentText);
        public readonly string PersonalCount = nameof(PersonalCount);
        public readonly string PersonalLimit = nameof(PersonalLimit);
        public readonly string PersonalText = nameof(PersonalText);
        public readonly string MobileCount = nameof(MobileCount);
        public readonly string MobileLimit = nameof(MobileLimit);
        public readonly string MobileText = nameof(MobileText);

        #endregion

        #region ToString

        public static implicit operator string(LicenseVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region LocalizationEntries

    /// <summary>
    ///     ID: {b92e97c0-4557-4d43-874a-e9a75173cbf8}
    ///     Alias: LocalizationEntries
    ///     Group: System
    /// </summary>
    public sealed class LocalizationEntriesSchemeInfo
    {
        private const string name = "LocalizationEntries";

        #region Columns

        public readonly string LibraryID = nameof(LibraryID);
        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Comment = nameof(Comment);
        public readonly string Overridden = nameof(Overridden);

        #endregion

        #region ToString

        public static implicit operator string(LocalizationEntriesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region LocalizationLibraries

    /// <summary>
    ///     ID: {3e31b54e-1a4c-4e9c-bcf5-26e4780d6419}
    ///     Alias: LocalizationLibraries
    ///     Group: System
    /// </summary>
    public sealed class LocalizationLibrariesSchemeInfo
    {
        private const string name = "LocalizationLibraries";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Priority = nameof(Priority);
        public readonly string DetachedCultures = nameof(DetachedCultures);

        #endregion

        #region ToString

        public static implicit operator string(LocalizationLibrariesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region LocalizationStrings

    /// <summary>
    ///     ID: {3e0ef7dd-7303-41e8-9ddc-af0a30e0de84}
    ///     Alias: LocalizationStrings
    ///     Group: System
    /// </summary>
    public sealed class LocalizationStringsSchemeInfo
    {
        private const string name = "LocalizationStrings";

        #region Columns

        public readonly string EntryID = nameof(EntryID);
        public readonly string Value = nameof(Value);
        public readonly string Culture = nameof(Culture);

        #endregion

        #region ToString

        public static implicit operator string(LocalizationStringsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region LoginTypes

    /// <summary>
    ///     ID: {44a94501-a954-4ab1-a7f8-47eebb2f869b}
    ///     Alias: LoginTypes
    ///     Group: System
    ///     Description: Типы входа пользователей в систему
    /// </summary>
    public sealed class LoginTypesSchemeInfo
    {
        private const string name = "LoginTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class None
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_LoginTypes_None";
            }
            public static class Tessa
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_LoginTypes_Tessa";
            }
            public static class Windows
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_LoginTypes_Windows";
            }
            public static class Ldap
            {
                    public const int ID = 3;
                    public const string Name = "$Enum_LoginTypes_Ldap";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(LoginTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region LoginTypes Enumeration

    public sealed class LoginTypes
    {
        public readonly int ID;
        public readonly string Name;

        public LoginTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region MetaRoles

    /// <summary>
    ///     ID: {984ac49b-30d7-48da-ab6a-3fe4bcf7513d}
    ///     Alias: MetaRoles
    ///     Group: Roles
    ///     Description: Метароли.
    /// </summary>
    public sealed class MetaRolesSchemeInfo
    {
        private const string name = "MetaRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string TypeID = nameof(TypeID);
        public readonly string GeneratorID = nameof(GeneratorID);
        public readonly string GeneratorName = nameof(GeneratorName);
        public readonly string IDGuid = nameof(IDGuid);
        public readonly string IDInteger = nameof(IDInteger);
        public readonly string IDString = nameof(IDString);

        #endregion

        #region ToString

        public static implicit operator string(MetaRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region MetaRoleTypes

    /// <summary>
    ///     ID: {53a3ee37-b714-4503-9e0e-e2ed1ccd164f}
    ///     Alias: MetaRoleTypes
    ///     Group: Roles
    ///     Description: Типы метаролей.
    /// </summary>
    public sealed class MetaRoleTypesSchemeInfo
    {
        private const string name = "MetaRoleTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Guid
            {
                    public const int ID = 0;
                    public const string Name = "Guid";
            }
            public static class Integer
            {
                    public const int ID = 1;
                    public const string Name = "Integer";
            }
            public static class String
            {
                    public const int ID = 2;
                    public const string Name = "String";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(MetaRoleTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region MetaRoleTypes Enumeration

    public sealed class MetaRoleTypes
    {
        public readonly int ID;
        public readonly string Name;

        public MetaRoleTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region Migrations

    /// <summary>
    ///     ID: {fd65afe6-d4bf-4885-872a-3824e64b1c63}
    ///     Alias: Migrations
    ///     Group: System
    ///     Description: Migrations
    /// </summary>
    public sealed class MigrationsSchemeInfo
    {
        private const string name = "Migrations";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Definition = nameof(Definition);

        #endregion

        #region ToString

        public static implicit operator string(MigrationsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region MobileLicenses

    /// <summary>
    ///     ID: {457f5393-50a4-40ea-8637-37fb57330ae2}
    ///     Alias: MobileLicenses
    ///     Group: System
    ///     Description: Сотрудники, для которых указаны лицензии мобильного согласования.
    /// </summary>
    public sealed class MobileLicensesSchemeInfo
    {
        private const string name = "MobileLicenses";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);

        #endregion

        #region ToString

        public static implicit operator string(MobileLicensesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region NestedRoles

    /// <summary>
    ///     ID: {312b1519-8079-44d7-a5b4-496db41da98c}
    ///     Alias: NestedRoles
    ///     Group: Acl
    ///     Description: Основная секция для вложенных ролей.
    /// </summary>
    public sealed class NestedRolesSchemeInfo
    {
        private const string name = "NestedRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ContextID = nameof(ContextID);
        public readonly string ContextName = nameof(ContextName);
        public readonly string ParentID = nameof(ParentID);

        #endregion

        #region ToString

        public static implicit operator string(NestedRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Notifications

    /// <summary>
    ///     ID: {18145bb5-fd4e-4795-aa1f-9e1cd9b4ee5a}
    ///     Alias: Notifications
    ///     Group: System
    ///     Description: Основная секция для карточки Уведомление
    /// </summary>
    public sealed class NotificationsSchemeInfo
    {
        private const string name = "Notifications";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Description = nameof(Description);
        public readonly string NotificationTypeID = nameof(NotificationTypeID);
        public readonly string NotificationTypeName = nameof(NotificationTypeName);
        public readonly string AliasMetadata = nameof(AliasMetadata);
        public readonly string Subject = nameof(Subject);
        public readonly string Text = nameof(Text);

        #endregion

        #region ToString

        public static implicit operator string(NotificationsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region NotificationSubscribeTypes

    /// <summary>
    ///     ID: {287bfcf3-aa96-44ee-96a8-68fbc1f2d3ab}
    ///     Alias: NotificationSubscribeTypes
    ///     Group: System
    /// </summary>
    public sealed class NotificationSubscribeTypesSchemeInfo
    {
        private const string name = "NotificationSubscribeTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string NotificationTypeID = nameof(NotificationTypeID);
        public readonly string NotificationTypeName = nameof(NotificationTypeName);

        #endregion

        #region ToString

        public static implicit operator string(NotificationSubscribeTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region NotificationSubscriptions

    /// <summary>
    ///     ID: {d5b074e2-eaff-4993-b238-1d5d3d248d56}
    ///     Alias: NotificationSubscriptions
    ///     Group: System
    ///     Description: Таблица с подписками/отписками пользователей по карточкам
    /// </summary>
    public sealed class NotificationSubscriptionsSchemeInfo
    {
        private const string name = "NotificationSubscriptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string UserID = nameof(UserID);
        public readonly string CardID = nameof(CardID);
        public readonly string CardDigest = nameof(CardDigest);
        public readonly string NotificationTypeID = nameof(NotificationTypeID);
        public readonly string NotificationTypeName = nameof(NotificationTypeName);
        public readonly string IsSubscription = nameof(IsSubscription);
        public readonly string SubscriptionDate = nameof(SubscriptionDate);

        #endregion

        #region ToString

        public static implicit operator string(NotificationSubscriptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region NotificationSubscriptionSettings

    /// <summary>
    ///     ID: {9ffce865-a6cd-4883-9ed0-0cbeaa1831d1}
    ///     Alias: NotificationSubscriptionSettings
    ///     Group: System
    ///     Description: Виртуальная секция для виртуальной карточки настроек уведомлений
    /// </summary>
    public sealed class NotificationSubscriptionSettingsSchemeInfo
    {
        private const string name = "NotificationSubscriptionSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CardTypeID = nameof(CardTypeID);
        public readonly string CardTypeCaption = nameof(CardTypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(NotificationSubscriptionSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region NotificationTokenRoles

    /// <summary>
    ///     ID: {a88ed4f8-dcce-400a-931f-99defee9949c}
    ///     Alias: NotificationTokenRoles
    ///     Group: System
    ///     Description: Список ролей, получающих уведомления о необходимости обновления токена.
    /// </summary>
    public sealed class NotificationTokenRolesSchemeInfo
    {
        private const string name = "NotificationTokenRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(NotificationTokenRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region NotificationTypeCardTypes

    /// <summary>
    ///     ID: {b54be13b-72be-4b20-b090-b861efaf8585}
    ///     Alias: NotificationTypeCardTypes
    ///     Group: System
    /// </summary>
    public sealed class NotificationTypeCardTypesSchemeInfo
    {
        private const string name = "NotificationTypeCardTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(NotificationTypeCardTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region NotificationTypes

    /// <summary>
    ///     ID: {bae37ba2-7a39-49a1-8cc8-64f032ba3f79}
    ///     Alias: NotificationTypes
    ///     Group: System
    ///     Description: Основная секция для карточки Тип уведомления
    /// </summary>
    public sealed class NotificationTypesSchemeInfo
    {
        private const string name = "NotificationTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string IsGlobal = nameof(IsGlobal);
        public readonly string CanSubscribe = nameof(CanSubscribe);
        public readonly string Hidden = nameof(Hidden);

        #endregion

        #region ToString

        public static implicit operator string(NotificationTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region NotificationUnsubscribeTypes

    /// <summary>
    ///     ID: {d845a7f8-9873-47c1-a160-370f66dc852e}
    ///     Alias: NotificationUnsubscribeTypes
    ///     Group: System
    /// </summary>
    public sealed class NotificationUnsubscribeTypesSchemeInfo
    {
        private const string name = "NotificationUnsubscribeTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string NotificationTypeID = nameof(NotificationTypeID);
        public readonly string NotificationTypeName = nameof(NotificationTypeName);

        #endregion

        #region ToString

        public static implicit operator string(NotificationUnsubscribeTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OcrLanguages

    /// <summary>
    ///     ID: {a5b1b1cf-ad8c-4459-a880-a5ff9f435398}
    ///     Alias: OcrLanguages
    ///     Group: Ocr
    ///     Description: Supported languages ??for text recognition
    /// </summary>
    public sealed class OcrLanguagesSchemeInfo
    {
        private const string name = "OcrLanguages";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ISO = nameof(ISO);
        public readonly string Code = nameof(Code);
        public readonly string Caption = nameof(Caption);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class English
            {
                    public const int ID = 2;
                    public const string ISO = "eng";
                    public const string Code = "en";
                    public const string Caption = "English";
            }
            public static class Slovenian
            {
                    public const int ID = 3;
                    public const string ISO = "slv";
                    public const string Code = "sl";
                    public const string Caption = "Slovenian";
            }
            public static class Russian
            {
                    public const int ID = 1;
                    public const string ISO = "rus";
                    public const string Code = "ru";
                    public const string Caption = "Russian";
            }
            public static class Auto
            {
                    public const int ID = 0;
                    public const string ISO = null;
                    public const string Code = null;
                    public const string Caption = "Auto";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(OcrLanguagesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region OcrLanguages Enumeration

    public sealed class OcrLanguages
    {
        public readonly int ID;
        public readonly string ISO;
        public readonly string Code;
        public readonly string Caption;

        public OcrLanguages(int ID, string ISO, string Code, string Caption)
        {
            this.ID = ID;
            this.ISO = ISO;
            this.Code = Code;
            this.Caption = Caption;
        }
    }

    #endregion

    #endregion

    #region OcrMappingComplexFields

    /// <summary>
    ///     ID: {e8135496-a897-44b9-bc24-9214646453fe}
    ///     Alias: OcrMappingComplexFields
    ///     Group: Ocr
    ///     Description: Parameters for mapping verified complex fields
    /// </summary>
    public sealed class OcrMappingComplexFieldsSchemeInfo
    {
        private const string name = "OcrMappingComplexFields";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string Field = nameof(Field);
        public readonly string Value = nameof(Value);

        #endregion

        #region ToString

        public static implicit operator string(OcrMappingComplexFieldsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OcrMappingFields

    /// <summary>
    ///     ID: {f60796c1-b504-424d-93c8-2fd5b9107867}
    ///     Alias: OcrMappingFields
    ///     Group: Ocr
    ///     Description: Parameters for mapping verified fields
    /// </summary>
    public sealed class OcrMappingFieldsSchemeInfo
    {
        private const string name = "OcrMappingFields";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Section = nameof(Section);
        public readonly string Field = nameof(Field);
        public readonly string Displayed = nameof(Displayed);
        public readonly string Value = nameof(Value);

        #endregion

        #region ToString

        public static implicit operator string(OcrMappingFieldsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OcrMappingSettingsFields

    /// <summary>
    ///     ID: {46a9cc0e-9492-48ce-8ac8-bc04551a041a}
    ///     Alias: OcrMappingSettingsFields
    ///     Group: Ocr
    ///     Description: Field settings by section for mapping during verification
    /// </summary>
    public sealed class OcrMappingSettingsFieldsSchemeInfo
    {
        private const string name = "OcrMappingSettingsFields";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string FieldID = nameof(FieldID);
        public readonly string FieldName = nameof(FieldName);
        public readonly string FieldComplexColumnIndex = nameof(FieldComplexColumnIndex);
        public readonly string Caption = nameof(Caption);
        public readonly string ViewRefSection = nameof(ViewRefSection);
        public readonly string ViewParameter = nameof(ViewParameter);
        public readonly string ViewReferencePrefix = nameof(ViewReferencePrefix);
        public readonly string ViewAlias = nameof(ViewAlias);

        #endregion

        #region ToString

        public static implicit operator string(OcrMappingSettingsFieldsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OcrMappingSettingsSections

    /// <summary>
    ///     ID: {3a7dbf8d-2f25-4b98-a406-2582bfeee594}
    ///     Alias: OcrMappingSettingsSections
    ///     Group: Ocr
    ///     Description: Section settings by card type for mapping fields during verification
    /// </summary>
    public sealed class OcrMappingSettingsSectionsSchemeInfo
    {
        private const string name = "OcrMappingSettingsSections";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string SectionID = nameof(SectionID);
        public readonly string SectionName = nameof(SectionName);

        #endregion

        #region ToString

        public static implicit operator string(OcrMappingSettingsSectionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OcrMappingSettingsTypes

    /// <summary>
    ///     ID: {6f34e6c5-93e9-49ed-ad42-63d8a001ded7}
    ///     Alias: OcrMappingSettingsTypes
    ///     Group: Ocr
    ///     Description: Card type settings for mapping fields during verification
    /// </summary>
    public sealed class OcrMappingSettingsTypesSchemeInfo
    {
        private const string name = "OcrMappingSettingsTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string TypeName = nameof(TypeName);

        #endregion

        #region ToString

        public static implicit operator string(OcrMappingSettingsTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OcrMappingSettingsVirtual

    /// <summary>
    ///     ID: {0c83bfa8-8e9a-454d-b805-6de0c679f4ec}
    ///     Alias: OcrMappingSettingsVirtual
    ///     Group: Ocr
    ///     Description: Virtual table for mapping settings
    /// </summary>
    public sealed class OcrMappingSettingsVirtualSchemeInfo
    {
        private const string name = "OcrMappingSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string SectionID = nameof(SectionID);

        #endregion

        #region ToString

        public static implicit operator string(OcrMappingSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OcrOperations

    /// <summary>
    ///     ID: {2fb28ad4-1b86-4d22-bc15-2a4943a0bb7f}
    ///     Alias: OcrOperations
    ///     Group: Ocr
    ///     Description: Information on text recognition operations
    /// </summary>
    public sealed class OcrOperationsSchemeInfo
    {
        private const string name = "OcrOperations";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CardID = nameof(CardID);
        public readonly string CardTypeID = nameof(CardTypeID);
        public readonly string CardTypeName = nameof(CardTypeName);
        public readonly string FileID = nameof(FileID);
        public readonly string FileName = nameof(FileName);
        public readonly string FileHasText = nameof(FileHasText);
        public readonly string FileTypeID = nameof(FileTypeID);
        public readonly string FileTypeName = nameof(FileTypeName);
        public readonly string VersionRowID = nameof(VersionRowID);

        #endregion

        #region ToString

        public static implicit operator string(OcrOperationsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OcrPatternTypes

    /// <summary>
    ///     ID: {ff8635f6-7856-45e1-82cf-6f00315ce790}
    ///     Alias: OcrPatternTypes
    ///     Group: Ocr
    ///     Description: Template types for field verification
    /// </summary>
    public sealed class OcrPatternTypesSchemeInfo
    {
        private const string name = "OcrPatternTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Boolean
            {
                    public const int ID = 0;
                    public const string Name = "Boolean";
            }
            public static class Integer
            {
                    public const int ID = 1;
                    public const string Name = "Integer";
            }
            public static class Double
            {
                    public const int ID = 2;
                    public const string Name = "Double";
            }
            public static class DateTime
            {
                    public const int ID = 3;
                    public const string Name = "DateTime";
            }
            public static class Date
            {
                    public const int ID = 4;
                    public const string Name = "Date";
            }
            public static class Time
            {
                    public const int ID = 5;
                    public const string Name = "Time";
            }
            public static class Interval
            {
                    public const int ID = 6;
                    public const string Name = "Interval";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(OcrPatternTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region OcrPatternTypes Enumeration

    public sealed class OcrPatternTypes
    {
        public readonly int ID;
        public readonly string Name;

        public OcrPatternTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region OcrRecognitionModes

    /// <summary>
    ///     ID: {b9a84cff-8153-4a81-b9f3-832d59461596}
    ///     Alias: OcrRecognitionModes
    ///     Group: Ocr
    ///     Description: Text recognition modes
    /// </summary>
    public sealed class OcrRecognitionModesSchemeInfo
    {
        private const string name = "OcrRecognitionModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Description = nameof(Description);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class OpticalRecognition
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_OcrRecognitionModes_OpticalRecognition";
                    public const string Description = "$Enum_OcrRecognitionModes_OpticalRecognition_Description";
            }
            public static class NeuralNetwork
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_OcrRecognitionModes_NeuralNetwork";
                    public const string Description = "$Enum_OcrRecognitionModes_NeuralNetwork_Description";
            }
            public static class OpticalRecognitionAndNeuralNetwork
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_OcrRecognitionModes_OpticalRecognitionAndNeuralNetwork";
                    public const string Description = "$Enum_OcrRecognitionModes_OpticalRecognitionAndNeuralNetwork_Description";
            }
            public static class Default
            {
                    public const int ID = 3;
                    public const string Name = "$Enum_OcrRecognitionModes_Default";
                    public const string Description = "$Enum_OcrRecognitionModes_Default_Description";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(OcrRecognitionModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region OcrRecognitionModes Enumeration

    public sealed class OcrRecognitionModes
    {
        public readonly int ID;
        public readonly string Name;
        public readonly string Description;

        public OcrRecognitionModes(int ID, string Name, string Description)
        {
            this.ID = ID;
            this.Name = Name;
            this.Description = Description;
        }
    }

    #endregion

    #endregion

    #region OcrRequests

    /// <summary>
    ///     ID: {d64806e9-ef31-4133-806b-670b178cc5bc}
    ///     Alias: OcrRequests
    ///     Group: Ocr
    ///     Description: Information on text recognition requests
    /// </summary>
    public sealed class OcrRequestsSchemeInfo
    {
        private const string name = "OcrRequests";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Created = nameof(Created);
        public readonly string CreatedByID = nameof(CreatedByID);
        public readonly string CreatedByName = nameof(CreatedByName);
        public readonly string StateID = nameof(StateID);
        public readonly string Confidence = nameof(Confidence);
        public readonly string Preprocess = nameof(Preprocess);
        public readonly string SegmentationModeID = nameof(SegmentationModeID);
        public readonly string SegmentationModeName = nameof(SegmentationModeName);
        public readonly string DetectLanguages = nameof(DetectLanguages);
        public readonly string ContentFileID = nameof(ContentFileID);
        public readonly string MetadataFileID = nameof(MetadataFileID);
        public readonly string IsMain = nameof(IsMain);
        public readonly string Overwrite = nameof(Overwrite);
        public readonly string DetectRotation = nameof(DetectRotation);
        public readonly string DetectTables = nameof(DetectTables);

        #endregion

        #region ToString

        public static implicit operator string(OcrRequestsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OcrRequestsLanguages

    /// <summary>
    ///     ID: {afee6930-bb0c-48b3-b0ac-3a1447a31d12}
    ///     Alias: OcrRequestsLanguages
    ///     Group: Ocr
    ///     Description: Information by languages ??used in text recognition requests
    /// </summary>
    public sealed class OcrRequestsLanguagesSchemeInfo
    {
        private const string name = "OcrRequestsLanguages";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string LanguageID = nameof(LanguageID);
        public readonly string LanguageISO = nameof(LanguageISO);
        public readonly string LanguageCaption = nameof(LanguageCaption);
        public readonly string ParentRowID = nameof(ParentRowID);

        #endregion

        #region ToString

        public static implicit operator string(OcrRequestsLanguagesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OcrRequestsStates

    /// <summary>
    ///     ID: {aefacd5d-1de1-42ac-86db-ddb98035f498}
    ///     Alias: OcrRequestsStates
    ///     Group: Ocr
    ///     Description: Text recognition request states
    /// </summary>
    public sealed class OcrRequestsStatesSchemeInfo
    {
        private const string name = "OcrRequestsStates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Created
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_OcrRequestStates_Created";
            }
            public static class Active
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_OcrRequestStates_Active";
            }
            public static class Completed
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_OcrRequestStates_Completed";
            }
            public static class Interrupted
            {
                    public const int ID = 3;
                    public const string Name = "$Enum_OcrRequestStates_Interrupted";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(OcrRequestsStatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region OcrRequestsStates Enumeration

    public sealed class OcrRequestsStates
    {
        public readonly int ID;
        public readonly string Name;

        public OcrRequestsStates(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region OcrResultsVirtual

    /// <summary>
    ///     ID: {f704ecbb-e3b2-4a79-8ca8-222417f3dc4d}
    ///     Alias: OcrResultsVirtual
    ///     Group: Ocr
    ///     Description: Information with text recognition results
    /// </summary>
    public sealed class OcrResultsVirtualSchemeInfo
    {
        private const string name = "OcrResultsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Text = nameof(Text);
        public readonly string Confidence = nameof(Confidence);
        public readonly string Language = nameof(Language);

        #endregion

        #region ToString

        public static implicit operator string(OcrResultsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OcrSegmentationModes

    /// <summary>
    ///     ID: {5455dc3b-79f8-4c06-8cf0-64e63919ab4a}
    ///     Alias: OcrSegmentationModes
    ///     Group: Ocr
    ///     Description: Image page segmentation modes for text recognition
    /// </summary>
    public sealed class OcrSegmentationModesSchemeInfo
    {
        private const string name = "OcrSegmentationModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Hidden = nameof(Hidden);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class OsdOnly
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_OcrSegmentationModes_OsdOnly";
                    public const bool Hidden = true;
            }
            public static class AutoOsd
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_OcrSegmentationModes_AutoOsd";
                    public const bool Hidden = false;
            }
            public static class Auto
            {
                    public const int ID = 3;
                    public const string Name = "$Enum_OcrSegmentationModes_Auto";
                    public const bool Hidden = false;
            }
            public static class SingleColumn
            {
                    public const int ID = 4;
                    public const string Name = "$Enum_OcrSegmentationModes_SingleColumn";
                    public const bool Hidden = false;
            }
            public static class SingleBlockVertText
            {
                    public const int ID = 5;
                    public const string Name = "$Enum_OcrSegmentationModes_SingleBlockVertText";
                    public const bool Hidden = true;
            }
            public static class SingleBlock
            {
                    public const int ID = 6;
                    public const string Name = "$Enum_OcrSegmentationModes_SingleBlock";
                    public const bool Hidden = false;
            }
            public static class SingleLine
            {
                    public const int ID = 7;
                    public const string Name = "$Enum_OcrSegmentationModes_SingleLine";
                    public const bool Hidden = false;
            }
            public static class SingleWord
            {
                    public const int ID = 8;
                    public const string Name = "$Enum_OcrSegmentationModes_SingleWord";
                    public const bool Hidden = true;
            }
            public static class CircleWord
            {
                    public const int ID = 9;
                    public const string Name = "$Enum_OcrSegmentationModes_CircleWord";
                    public const bool Hidden = true;
            }
            public static class SingleChar
            {
                    public const int ID = 10;
                    public const string Name = "$Enum_OcrSegmentationModes_SingleChar";
                    public const bool Hidden = true;
            }
            public static class SparseText
            {
                    public const int ID = 11;
                    public const string Name = "$Enum_OcrSegmentationModes_SparseText";
                    public const bool Hidden = false;
            }
            public static class SparseTextOsd
            {
                    public const int ID = 12;
                    public const string Name = "$Enum_OcrSegmentationModes_SparseTextOsd";
                    public const bool Hidden = false;
            }
            public static class RawLine
            {
                    public const int ID = 13;
                    public const string Name = "$Enum_OcrSegmentationModes_RawLine";
                    public const bool Hidden = true;
            }
            public static class AutoOnly
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_OcrSegmentationModes_AutoOnly";
                    public const bool Hidden = true;
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(OcrSegmentationModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region OcrSegmentationModes Enumeration

    public sealed class OcrSegmentationModes
    {
        public readonly int ID;
        public readonly string Name;
        public readonly bool Hidden;

        public OcrSegmentationModes(int ID, string Name, bool Hidden)
        {
            this.ID = ID;
            this.Name = Name;
            this.Hidden = Hidden;
        }
    }

    #endregion

    #endregion

    #region OcrSettings

    /// <summary>
    ///     ID: {4463ae11-e603-4daa-8b93-2e4323abef37}
    ///     Alias: OcrSettings
    ///     Group: Ocr
    ///     Description: Text recognition settings
    /// </summary>
    public sealed class OcrSettingsSchemeInfo
    {
        private const string name = "OcrSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string IsEnabled = nameof(IsEnabled);
        public readonly string BaseAddress = nameof(BaseAddress);

        #endregion

        #region ToString

        public static implicit operator string(OcrSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OcrSettingsPatterns

    /// <summary>
    ///     ID: {e432240e-e30a-4568-aa91-bb9a9e55ea61}
    ///     Alias: OcrSettingsPatterns
    ///     Group: Ocr
    ///     Description: Templates for fields verification
    /// </summary>
    public sealed class OcrSettingsPatternsSchemeInfo
    {
        private const string name = "OcrSettingsPatterns";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeName = nameof(TypeName);
        public readonly string Value = nameof(Value);
        public readonly string Order = nameof(Order);
        public readonly string Description = nameof(Description);

        #endregion

        #region ToString

        public static implicit operator string(OcrSettingsPatternsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OnlyOfficeFileCache

    /// <summary>
    ///     ID: {8c151402-bae9-41a6-864f-f7558bd88c86}
    ///     Alias: OnlyOfficeFileCache
    ///     Group: OnlyOffice
    /// </summary>
    public sealed class OnlyOfficeFileCacheSchemeInfo
    {
        private const string name = "OnlyOfficeFileCache";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CreatedByID = nameof(CreatedByID);
        public readonly string SourceFileVersionID = nameof(SourceFileVersionID);
        public readonly string SourceFileName = nameof(SourceFileName);
        public readonly string ModifiedFileUrl = nameof(ModifiedFileUrl);
        public readonly string LastModifiedFileUrlTime = nameof(LastModifiedFileUrlTime);
        public readonly string LastAccessTime = nameof(LastAccessTime);
        public readonly string HasChangesAfterClose = nameof(HasChangesAfterClose);
        public readonly string EditorWasOpen = nameof(EditorWasOpen);
        public readonly string CoeditKey = nameof(CoeditKey);

        #endregion

        #region ToString

        public static implicit operator string(OnlyOfficeFileCacheSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OnlyOfficeSettings

    /// <summary>
    ///     ID: {703ad2d1-6246-4d47-a0ef-814b9466c027}
    ///     Alias: OnlyOfficeSettings
    ///     Group: OnlyOffice
    /// </summary>
    public sealed class OnlyOfficeSettingsSchemeInfo
    {
        private const string name = "OnlyOfficeSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ApiScriptUrl = nameof(ApiScriptUrl);
        public readonly string ConverterUrl = nameof(ConverterUrl);
        public readonly string PreviewEnabled = nameof(PreviewEnabled);
        public readonly string ExcludedPreviewFormats = nameof(ExcludedPreviewFormats);
        public readonly string DocumentBuilderPath = nameof(DocumentBuilderPath);
        public readonly string WebApiBasePath = nameof(WebApiBasePath);
        public readonly string LoadTimeoutPeriod = nameof(LoadTimeoutPeriod);
        public readonly string TokenLifetimePeriod = nameof(TokenLifetimePeriod);

        #endregion

        #region ToString

        public static implicit operator string(OnlyOfficeSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Operations

    /// <summary>
    ///     ID: {4ae0856c-dd1d-4da8-80b4-e6d232be8d94}
    ///     Alias: Operations
    ///     Group: System
    ///     Description: Статическая часть информации об операциях
    /// </summary>
    public sealed class OperationsSchemeInfo
    {
        private const string name = "Operations";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string CreatedByID = nameof(CreatedByID);
        public readonly string CreatedByName = nameof(CreatedByName);
        public readonly string Created = nameof(Created);
        public readonly string Digest = nameof(Digest);
        public readonly string Request = nameof(Request);
        public readonly string RequestHash = nameof(RequestHash);
        public readonly string Postponed = nameof(Postponed);
        public readonly string SessionID = nameof(SessionID);
        public readonly string CreationFlags = nameof(CreationFlags);

        #endregion

        #region ToString

        public static implicit operator string(OperationsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OperationStates

    /// <summary>
    ///     ID: {e726339c-e2fc-4d7c-a9b4-011577ff2106}
    ///     Alias: OperationStates
    ///     Group: System
    ///     Description: Состояние операции.
    /// </summary>
    public sealed class OperationStatesSchemeInfo
    {
        private const string name = "OperationStates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Created
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_OperationStates_Created";
            }
            public static class InProgress
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_OperationStates_InProgress";
            }
            public static class Completed
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_OperationStates_Completed";
            }
            public static class Postponed
            {
                    public const int ID = 3;
                    public const string Name = "$Enum_OperationStates_Postponed";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(OperationStatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region OperationStates Enumeration

    public sealed class OperationStates
    {
        public readonly int ID;
        public readonly string Name;

        public OperationStates(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region OperationsVirtual

    /// <summary>
    ///     ID: {a87294b1-0c66-41bc-98e8-765dfb8dcf56}
    ///     Alias: OperationsVirtual
    ///     Group: System
    ///     Description: Виртуальная карточка \"Операция\". Колонки заполняются из таблиц Operations, OperationUpdates и HSET Redis'a
    /// </summary>
    public sealed class OperationsVirtualSchemeInfo
    {
        private const string name = "OperationsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeName = nameof(TypeName);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);
        public readonly string CreatedByID = nameof(CreatedByID);
        public readonly string CreatedByName = nameof(CreatedByName);
        public readonly string Created = nameof(Created);
        public readonly string InProgress = nameof(InProgress);
        public readonly string Completed = nameof(Completed);
        public readonly string Progress = nameof(Progress);
        public readonly string Digest = nameof(Digest);
        public readonly string Request = nameof(Request);
        public readonly string RequestJson = nameof(RequestJson);
        public readonly string Response = nameof(Response);
        public readonly string ResponseJson = nameof(ResponseJson);
        public readonly string OperationID = nameof(OperationID);
        public readonly string SessionID = nameof(SessionID);

        #endregion

        #region ToString

        public static implicit operator string(OperationsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OperationTypes

    /// <summary>
    ///     ID: {b23fccd5-5ba1-45b6-a0ad-e9d0cf730da0}
    ///     Alias: OperationTypes
    ///     Group: System
    ///     Description: Типы операций
    /// </summary>
    public sealed class OperationTypesSchemeInfo
    {
        private const string name = "OperationTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class SavingCard
            {
                    public static Guid ID = new Guid(0x033e01ef,0x5913,0x4588,0x80,0xc0,0x61,0x56,0x73,0x23,0x57,0x7d);
                    public const string Name = "$Enum_OperationTypes_SavingCard";
            }
            public static class Unnamed
            {
                    public static Guid ID = Guid.Empty;
                    public const string Name = "$Enum_OperationTypes_Unnamed";
            }
            public static class CalculatingCalendar
            {
                    public static Guid ID = new Guid(0xf3bd681e,0x861d,0x4820,0x8f,0xa5,0xb2,0x44,0x3b,0x20,0xdb,0xba);
                    public const string Name = "$Enum_OperationTypes_CalculatingCalendar";
            }
            public static class ImportingCard
            {
                    public static Guid ID = new Guid(0x2a7364c5,0xd927,0x4b50,0x8c,0x67,0x02,0xd2,0x41,0x76,0x5e,0x5f);
                    public const string Name = "$Enum_OperationTypes_ImportingCard";
            }
            public static class FileConvert
            {
                    public static Guid ID = new Guid(0x730e24fd,0x52ee,0x4bfc,0x9c,0xe0,0x9d,0xaf,0x32,0x83,0xfa,0xbe);
                    public const string Name = "$Enum_OperationTypes_FileConvert";
            }
            public static class CalculatingRoles
            {
                    public static Guid ID = new Guid(0xde7d7a67,0xfa4b,0x4c80,0xa7,0xf2,0xb9,0xe4,0x50,0x95,0xf6,0xc8);
                    public const string Name = "$Enum_OperationTypes_CalculatingRoles";
            }
            public static class CalculatingAD
            {
                    public static Guid ID = new Guid(0x9d7d2fe7,0x6c05,0x42c9,0xa3,0x2d,0x63,0x07,0x6c,0x1b,0x5c,0xef);
                    public const string Name = "$Enum_OperationTypes_CalculatingAD";
            }
            public static class WorkflowEngineAsync
            {
                    public static Guid ID = new Guid(0xde6c8d23,0x53e2,0x4659,0xb4,0x3c,0x0e,0xea,0x4f,0x0f,0xec,0x19);
                    public const string Name = "$Enum_OperationTypes_WorkflowEngineAsync";
            }
            public static class SendingForumsNotifications
            {
                    public static Guid ID = new Guid(0x333ee6b8,0x6468,0x4e0e,0x9a,0xc0,0xc7,0x3d,0xb8,0x39,0x19,0xdc);
                    public const string Name = "$Enum_OperationTypes_SendingForumsNotifications";
            }
            public static class AclCalculation
            {
                    public static Guid ID = new Guid(0x6d10f621,0xd73b,0x449a,0xba,0xf2,0x68,0xbe,0x18,0xc6,0x06,0x89);
                    public const string Name = "$Enum_OperationTypes_AclCalculation";
            }
            public static class CalculatingSmartRoles
            {
                    public static Guid ID = new Guid(0x0b663cca,0xc724,0x404f,0x83,0x00,0x40,0xd2,0xb6,0x03,0x92,0xc2);
                    public const string Name = "$Enum_OperationTypes_CalculatingSmartRoles";
            }
            public static class DeferredDeletion
            {
                    public static Guid ID = new Guid(0x06e3df9e,0x0820,0x4ee4,0xb3,0x2c,0xfa,0x6c,0x32,0x18,0xe9,0x9b);
                    public const string Name = "$Enum_OperationTypes_DeferredDeletion";
            }
            public static class TextRecognition
            {
                    public static Guid ID = new Guid(0xb8f6298c,0x2d53,0x446f,0x90,0x07,0x78,0x49,0xef,0x05,0x0b,0x5e);
                    public const string Name = "$Enum_OperationTypes_TextRecognition";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(OperationTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region OperationTypes Enumeration

    public sealed class OperationTypes
    {
        public readonly Guid ID;
        public readonly string Name;

        public OperationTypes(Guid ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region OperationUpdates

    /// <summary>
    ///     ID: {a6435575-79d4-44b6-b755-b9a026431556}
    ///     Alias: OperationUpdates
    ///     Group: System
    ///     Description: Обновляемая часть информации об операциях
    /// </summary>
    public sealed class OperationUpdatesSchemeInfo
    {
        private const string name = "OperationUpdates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string StateID = nameof(StateID);
        public readonly string InProgress = nameof(InProgress);
        public readonly string Completed = nameof(Completed);
        public readonly string Response = nameof(Response);

        #endregion

        #region ToString

        public static implicit operator string(OperationUpdatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Outbox

    /// <summary>
    ///     ID: {b4412a23-3e36-4468-a9cf-6b7b553f9e64}
    ///     Alias: Outbox
    ///     Group: System
    /// </summary>
    public sealed class OutboxSchemeInfo
    {
        private const string name = "Outbox";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Created = nameof(Created);
        public readonly string Email = nameof(Email);
        public readonly string Subject = nameof(Subject);
        public readonly string Body = nameof(Body);
        public readonly string Attempts = nameof(Attempts);
        public readonly string LastErrorDate = nameof(LastErrorDate);
        public readonly string LastErrorText = nameof(LastErrorText);
        public readonly string Info = nameof(Info);

        #endregion

        #region ToString

        public static implicit operator string(OutboxSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region OutgoingRefDocs

    /// <summary>
    ///     ID: {73320234-fc44-4126-a7a6-5dd0bdaa4880}
    ///     Alias: OutgoingRefDocs
    ///     Group: Common
    /// </summary>
    public sealed class OutgoingRefDocsSchemeInfo
    {
        private const string name = "OutgoingRefDocs";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string DocID = nameof(DocID);
        public readonly string DocDescription = nameof(DocDescription);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(OutgoingRefDocsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Partitions

    /// <summary>
    ///     ID: {5ca00fac-d04e-4b82-8139-9778518e00bf}
    ///     Alias: Partitions
    ///     Group: System
    /// </summary>
    public sealed class PartitionsSchemeInfo
    {
        private const string name = "Partitions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Definition = nameof(Definition);

        #endregion

        #region ToString

        public static implicit operator string(PartitionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Partners

    /// <summary>
    ///     ID: {5d47ef13-b6f4-47ef-9815-3b3d0e6d475a}
    ///     Alias: Partners
    ///     Group: Common
    ///     Description: Контрагенты
    /// </summary>
    public sealed class PartnersSchemeInfo
    {
        private const string name = "Partners";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string FullName = nameof(FullName);
        public readonly string LegalAddress = nameof(LegalAddress);
        public readonly string Phone = nameof(Phone);
        public readonly string Head = nameof(Head);
        public readonly string ChiefAccountant = nameof(ChiefAccountant);
        public readonly string ContactPerson = nameof(ContactPerson);
        public readonly string Email = nameof(Email);
        public readonly string ContactAddress = nameof(ContactAddress);
        public readonly string INN = nameof(INN);
        public readonly string KPP = nameof(KPP);
        public readonly string OGRN = nameof(OGRN);
        public readonly string OKPO = nameof(OKPO);
        public readonly string OKVED = nameof(OKVED);
        public readonly string Comment = nameof(Comment);
        public readonly string Bank = nameof(Bank);
        public readonly string SettlementAccount = nameof(SettlementAccount);
        public readonly string BIK = nameof(BIK);
        public readonly string CorrAccount = nameof(CorrAccount);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeName = nameof(TypeName);
        public readonly string VatTypeID = nameof(VatTypeID);
        public readonly string VatTypeName = nameof(VatTypeName);

        #endregion

        #region ToString

        public static implicit operator string(PartnersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PartnersContacts

    /// <summary>
    ///     ID: {c57f5563-6673-4ca0-83a1-2896dbd090e1}
    ///     Alias: PartnersContacts
    ///     Group: Common
    ///     Description: Контактные лица
    /// </summary>
    public sealed class PartnersContactsSchemeInfo
    {
        private const string name = "PartnersContacts";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);
        public readonly string Department = nameof(Department);
        public readonly string Position = nameof(Position);
        public readonly string PhoneNumber = nameof(PhoneNumber);
        public readonly string Email = nameof(Email);
        public readonly string Comment = nameof(Comment);

        #endregion

        #region ToString

        public static implicit operator string(PartnersContactsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PartnersTypes

    /// <summary>
    ///     ID: {354e4f5a-e50c-4a11-84d0-6e0a98a81ca5}
    ///     Alias: PartnersTypes
    ///     Group: Common
    /// </summary>
    public sealed class PartnersTypesSchemeInfo
    {
        private const string name = "PartnersTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class LegalEntity
            {
                    public const int ID = 1;
                    public const string Name = "$PartnerType_LegalEntity";
            }
            public static class Individual
            {
                    public const int ID = 2;
                    public const string Name = "$PartnerType_Individual";
            }
            public static class SoleTrader
            {
                    public const int ID = 3;
                    public const string Name = "$PartnerType_SoleTrader";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(PartnersTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region PartnersTypes Enumeration

    public sealed class PartnersTypes
    {
        public readonly int ID;
        public readonly string Name;

        public PartnersTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region Performers

    /// <summary>
    ///     ID: {d0f5547b-b2f5-4a08-8cd9-b34138d35125}
    ///     Alias: Performers
    ///     Group: Common
    ///     Description: Исполнители
    /// </summary>
    public sealed class PerformersSchemeInfo
    {
        private const string name = "Performers";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);

        #endregion

        #region ToString

        public static implicit operator string(PerformersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PersonalLicenses

    /// <summary>
    ///     ID: {8a96f177-b2a7-4ffc-885b-2f36730d0d2e}
    ///     Alias: PersonalLicenses
    ///     Group: System
    ///     Description: Сотрудники, для которых указаны персональные лицензии.
    /// </summary>
    public sealed class PersonalLicensesSchemeInfo
    {
        private const string name = "PersonalLicenses";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);

        #endregion

        #region ToString

        public static implicit operator string(PersonalLicensesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PersonalRoleDepartmentsVirtual

    /// <summary>
    ///     ID: {21566803-b822-42b2-ab11-2c20e72a0de4}
    ///     Alias: PersonalRoleDepartmentsVirtual
    ///     Group: Roles
    ///     Description: Таблица для отображения и редактирования департаментов, в которые входит сотрудник.
    /// </summary>
    public sealed class PersonalRoleDepartmentsVirtualSchemeInfo
    {
        private const string name = "PersonalRoleDepartmentsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string DepartmentID = nameof(DepartmentID);
        public readonly string DepartmentName = nameof(DepartmentName);

        #endregion

        #region ToString

        public static implicit operator string(PersonalRoleDepartmentsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PersonalRoleNotificationRulesVirtual

    /// <summary>
    ///     ID: {925a75d4-639f-4467-9155-c8e21f5433a9}
    ///     Alias: PersonalRoleNotificationRulesVirtual
    ///     Group: Roles
    /// </summary>
    public sealed class PersonalRoleNotificationRulesVirtualSchemeInfo
    {
        private const string name = "PersonalRoleNotificationRulesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);
        public readonly string Disallow = nameof(Disallow);
        public readonly string AllowanceType = nameof(AllowanceType);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(PersonalRoleNotificationRulesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PersonalRoleNotificationRuleTypesVirtual

    /// <summary>
    ///     ID: {16baecaa-9088-4635-af93-4c042893bf1d}
    ///     Alias: PersonalRoleNotificationRuleTypesVirtual
    ///     Group: Roles
    /// </summary>
    public sealed class PersonalRoleNotificationRuleTypesVirtualSchemeInfo
    {
        private const string name = "PersonalRoleNotificationRuleTypesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string NotificationTypeID = nameof(NotificationTypeID);
        public readonly string NotificationTypeName = nameof(NotificationTypeName);
        public readonly string RuleRowID = nameof(RuleRowID);

        #endregion

        #region ToString

        public static implicit operator string(PersonalRoleNotificationRuleTypesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PersonalRoleRolesVirtual

    /// <summary>
    ///     ID: {e631fc2a-7628-4d7e-a118-99d310fa12b8}
    ///     Alias: PersonalRoleRolesVirtual
    ///     Group: Roles
    ///     Description: Таблица для отображения всех ролей (кроме своей персональной роли), в которые входит сотрудник.
    /// </summary>
    public sealed class PersonalRoleRolesVirtualSchemeInfo
    {
        private const string name = "PersonalRoleRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(PersonalRoleRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PersonalRoles

    /// <summary>
    ///     ID: {6c977939-bbfc-456f-a133-f1c2244e3cc3}
    ///     Alias: PersonalRoles
    ///     Group: Roles
    ///     Description: Employees.
    /// </summary>
    public sealed class PersonalRolesSchemeInfo
    {
        private const string name = "PersonalRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string FullName = nameof(FullName);
        public readonly string LastName = nameof(LastName);
        public readonly string FirstName = nameof(FirstName);
        public readonly string MiddleName = nameof(MiddleName);
        public readonly string Position = nameof(Position);
        public readonly string BirthDate = nameof(BirthDate);
        public readonly string Email = nameof(Email);
        public readonly string Fax = nameof(Fax);
        public readonly string Phone = nameof(Phone);
        public readonly string MobilePhone = nameof(MobilePhone);
        public readonly string HomePhone = nameof(HomePhone);
        public readonly string IPPhone = nameof(IPPhone);
        public readonly string Login = nameof(Login);
        public readonly string PasswordKey = nameof(PasswordKey);
        public readonly string PasswordHash = nameof(PasswordHash);
        public readonly string AccessLevelID = nameof(AccessLevelID);
        public readonly string AccessLevelName = nameof(AccessLevelName);
        public readonly string LoginTypeID = nameof(LoginTypeID);
        public readonly string LoginTypeName = nameof(LoginTypeName);
        public readonly string Security = nameof(Security);
        public readonly string Blocked = nameof(Blocked);
        public readonly string BlockedDueDate = nameof(BlockedDueDate);
        public readonly string PasswordChanged = nameof(PasswordChanged);
        public readonly string ApplicationArchitectureID = nameof(ApplicationArchitectureID);
        public readonly string ApplicationArchitectureName = nameof(ApplicationArchitectureName);
        public readonly string CipherInfo = nameof(CipherInfo);
        public readonly string ExternalUid = nameof(ExternalUid);

        #endregion

        #region ToString

        public static implicit operator string(PersonalRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PersonalRoleSatellite

    /// <summary>
    ///     ID: {62fd7bdd-0fc1-4370-afd6-54ac7e5320b4}
    ///     Alias: PersonalRoleSatellite
    ///     Group: Roles
    ///     Description: Сателлит с настройками пользователя.
    /// </summary>
    public sealed class PersonalRoleSatelliteSchemeInfo
    {
        private const string name = "PersonalRoleSatellite";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string LanguageID = nameof(LanguageID);
        public readonly string LanguageCaption = nameof(LanguageCaption);
        public readonly string LanguageCode = nameof(LanguageCode);
        public readonly string FormatName = nameof(FormatName);
        public readonly string Settings = nameof(Settings);
        public readonly string FilePreviewPosition = nameof(FilePreviewPosition);
        public readonly string FilePreviewIsHidden = nameof(FilePreviewIsHidden);
        public readonly string FilePreviewWidthRatio = nameof(FilePreviewWidthRatio);
        public readonly string TaskAreaWidth = nameof(TaskAreaWidth);
        public readonly string ContentWidthRatio = nameof(ContentWidthRatio);
        public readonly string WebTheme = nameof(WebTheme);
        public readonly string WebWallpaper = nameof(WebWallpaper);
        public readonly string WorkplaceExtensions = nameof(WorkplaceExtensions);
        public readonly string NotificationSettings = nameof(NotificationSettings);
        public readonly string UserSettingsLastUpdate = nameof(UserSettingsLastUpdate);
        public readonly string ForumSettings = nameof(ForumSettings);
        public readonly string WebDefaultWallpaper = nameof(WebDefaultWallpaper);
        public readonly string CardSettings = nameof(CardSettings);

        #endregion

        #region ToString

        public static implicit operator string(PersonalRoleSatelliteSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PersonalRoleStaticRolesVirtual

    /// <summary>
    ///     ID: {122da60d-3efc-42a2-bd84-510c0819807b}
    ///     Alias: PersonalRoleStaticRolesVirtual
    ///     Group: Roles
    ///     Description: Таблица для отображения и редактирования статических ролей, в которые входит сотрудник.
    /// </summary>
    public sealed class PersonalRoleStaticRolesVirtualSchemeInfo
    {
        private const string name = "PersonalRoleStaticRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(PersonalRoleStaticRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PersonalRoleSubscribedTypesVirtual

    /// <summary>
    ///     ID: {62859264-a143-4ec0-a86b-bba80f6f61ac}
    ///     Alias: PersonalRoleSubscribedTypesVirtual
    ///     Group: Roles
    ///     Description: Секция со всеми глобальными типами уведомлений, на которые подписался пользователь.
    /// </summary>
    public sealed class PersonalRoleSubscribedTypesVirtualSchemeInfo
    {
        private const string name = "PersonalRoleSubscribedTypesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string NotificationTypeID = nameof(NotificationTypeID);
        public readonly string NotificationTypeName = nameof(NotificationTypeName);

        #endregion

        #region ToString

        public static implicit operator string(PersonalRoleSubscribedTypesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PersonalRolesVirtual

    /// <summary>
    ///     ID: {e86b07e5-da20-487b-a55e-0ed56606bddf}
    ///     Alias: PersonalRolesVirtual
    ///     Group: Roles
    ///     Description: Виртуальные поля для карточки сотрудника.
    /// </summary>
    public sealed class PersonalRolesVirtualSchemeInfo
    {
        private const string name = "PersonalRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string LanguageID = nameof(LanguageID);
        public readonly string LanguageCaption = nameof(LanguageCaption);
        public readonly string LanguageCode = nameof(LanguageCode);
        public readonly string FormatID = nameof(FormatID);
        public readonly string FormatName = nameof(FormatName);
        public readonly string FormatCaption = nameof(FormatCaption);
        public readonly string Password = nameof(Password);
        public readonly string PasswordRepeat = nameof(PasswordRepeat);
        public readonly string Settings = nameof(Settings);

        #endregion

        #region ToString

        public static implicit operator string(PersonalRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region PersonalRoleUnsubscibedTypesVirtual

    /// <summary>
    ///     ID: {fc4566ea-029b-4d37-b3f0-4ca62a4cb500}
    ///     Alias: PersonalRoleUnsubscibedTypesVirtual
    ///     Group: Roles
    ///     Description: Секция со всеми глобальными типами уведомлений, от которых отписался пользователь.
    /// </summary>
    public sealed class PersonalRoleUnsubscibedTypesVirtualSchemeInfo
    {
        private const string name = "PersonalRoleUnsubscibedTypesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string NotificationTypeID = nameof(NotificationTypeID);
        public readonly string NotificationTypeName = nameof(NotificationTypeName);

        #endregion

        #region ToString

        public static implicit operator string(PersonalRoleUnsubscibedTypesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Procedures

    /// <summary>
    ///     ID: {1bf6a3b2-725a-487c-b4d8-6b082fb56037}
    ///     Alias: Procedures
    ///     Group: System
    ///     Description: Contains metadata that describes tables which used by Tessa
    /// </summary>
    public sealed class ProceduresSchemeInfo
    {
        private const string name = "Procedures";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Definition = nameof(Definition);

        #endregion

        #region Procedures


        #endregion

        #region ToString

        public static implicit operator string(ProceduresSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ProtocolDecisions

    /// <summary>
    ///     ID: {91c272de-462d-4076-8f64-592885a4abd4}
    ///     Alias: ProtocolDecisions
    ///     Group: Common
    ///     Description: Решения по протоколу.
    /// </summary>
    public sealed class ProtocolDecisionsSchemeInfo
    {
        private const string name = "ProtocolDecisions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Question = nameof(Question);
        public readonly string Planned = nameof(Planned);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(ProtocolDecisionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ProtocolReports

    /// <summary>
    ///     ID: {5576e1f1-316a-4256-a136-c33eb871b7d5}
    ///     Alias: ProtocolReports
    ///     Group: Common
    /// </summary>
    public sealed class ProtocolReportsSchemeInfo
    {
        private const string name = "ProtocolReports";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Subject = nameof(Subject);
        public readonly string Order = nameof(Order);
        public readonly string PersonID = nameof(PersonID);
        public readonly string PersonName = nameof(PersonName);

        #endregion

        #region ToString

        public static implicit operator string(ProtocolReportsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ProtocolResponsibles

    /// <summary>
    ///     ID: {34e972b7-fd6f-4417-99d1-f2578a82ab1d}
    ///     Alias: ProtocolResponsibles
    ///     Group: Common
    /// </summary>
    public sealed class ProtocolResponsiblesSchemeInfo
    {
        private const string name = "ProtocolResponsibles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string ParentRowID = nameof(ParentRowID);

        #endregion

        #region ToString

        public static implicit operator string(ProtocolResponsiblesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Protocols

    /// <summary>
    ///     ID: {b98383dc-ecf0-4ad0-b92d-dd599775b8f5}
    ///     Alias: Protocols
    ///     Group: Common
    ///     Description: Протоколы.
    /// </summary>
    public sealed class ProtocolsSchemeInfo
    {
        private const string name = "Protocols";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ProtocolFileID = nameof(ProtocolFileID);
        public readonly string Date = nameof(Date);
        public readonly string Agenda = nameof(Agenda);

        #endregion

        #region ToString

        public static implicit operator string(ProtocolsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Recipients

    /// <summary>
    ///     ID: {386509d9-4130-467f-9a52-0004aa15247e}
    ///     Alias: Recipients
    ///     Group: Common
    ///     Description: Получатели
    /// </summary>
    public sealed class RecipientsSchemeInfo
    {
        private const string name = "Recipients";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);

        #endregion

        #region ToString

        public static implicit operator string(RecipientsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ReportRolesActive

    /// <summary>
    ///     ID: {fd37a3c0-33e5-4256-98bf-4440402f4116}
    ///     Alias: ReportRolesActive
    ///     Group: Common
    ///     Description: Роли, которые могут смотреть отчёты по текущим заданиям
    /// </summary>
    public sealed class ReportRolesActiveSchemeInfo
    {
        private const string name = "ReportRolesActive";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(ReportRolesActiveSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ReportRolesPassive

    /// <summary>
    ///     ID: {599f50f0-95c4-48ad-a739-c54fd9b5f829}
    ///     Alias: ReportRolesPassive
    ///     Group: Common
    ///     Description: Роли, которых могут смотреть отчёты по текущим заданиям
    /// </summary>
    public sealed class ReportRolesPassiveSchemeInfo
    {
        private const string name = "ReportRolesPassive";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(ReportRolesPassiveSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ReportRolesRules

    /// <summary>
    ///     ID: {359edaf2-fdb7-4e24-afc8-f31281328a81}
    ///     Alias: ReportRolesRules
    ///     Group: Common
    /// </summary>
    public sealed class ReportRolesRulesSchemeInfo
    {
        private const string name = "ReportRolesRules";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Caption = nameof(Caption);

        #endregion

        #region ToString

        public static implicit operator string(ReportRolesRulesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputies

    /// <summary>
    ///     ID: {900bdbcd-1e87-451c-8b4b-082d8f7efd48}
    ///     Alias: RoleDeputies
    ///     Group: Roles
    ///     Description: Заместители.
    /// </summary>
    public sealed class RoleDeputiesSchemeInfo
    {
        private const string name = "RoleDeputies";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string DeputyID = nameof(DeputyID);
        public readonly string DeputyName = nameof(DeputyName);
        public readonly string DeputizedID = nameof(DeputizedID);
        public readonly string DeputizedName = nameof(DeputizedName);
        public readonly string MinDate = nameof(MinDate);
        public readonly string MaxDate = nameof(MaxDate);
        public readonly string IsActive = nameof(IsActive);
        public readonly string IsEnabled = nameof(IsEnabled);
        public readonly string ManagementRowID = nameof(ManagementRowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string Level = nameof(Level);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesManagement

    /// <summary>
    ///     ID: {0f489948-bc16-42a6-8953-b92100807296}
    ///     Alias: RoleDeputiesManagement
    ///     Group: Roles
    ///     Description: Основные записи секции \"Мои замещения\"
    /// </summary>
    public sealed class RoleDeputiesManagementSchemeInfo
    {
        private const string name = "RoleDeputiesManagement";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string MinDate = nameof(MinDate);
        public readonly string MaxDate = nameof(MaxDate);
        public readonly string IsActive = nameof(IsActive);
        public readonly string IsEnabled = nameof(IsEnabled);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesManagementSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesManagementAccess

    /// <summary>
    ///     ID: {edbc91b1-dd36-43c2-867a-67c74ed7f403}
    ///     Alias: RoleDeputiesManagementAccess
    ///     Group: Roles
    ///     Description: Сотрудники, которые могут редактировать секции \"Мои замещения\"
    /// </summary>
    public sealed class RoleDeputiesManagementAccessSchemeInfo
    {
        private const string name = "RoleDeputiesManagementAccess";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string PersonalRoleID = nameof(PersonalRoleID);
        public readonly string PersonalRoleName = nameof(PersonalRoleName);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesManagementAccessSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesManagementDeputizedRolesVirtual

    /// <summary>
    ///     ID: {997561ee-218f-4f22-946b-87a78755c3e6}
    ///     Alias: RoleDeputiesManagementDeputizedRolesVirtual
    ///     Group: Roles
    ///     Description: Список ролей, который относится к каждой строке в таблице с замещаемыми сотрудниками RoleDeputiesManagementDeputizedVirtual.
    /// </summary>
    public sealed class RoleDeputiesManagementDeputizedRolesVirtualSchemeInfo
    {
        private const string name = "RoleDeputiesManagementDeputizedRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesManagementDeputizedRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesManagementDeputizedVirtual

    /// <summary>
    ///     ID: {c55a7921-d82d-4f8b-b801-f1c693c4c2e3}
    ///     Alias: RoleDeputiesManagementDeputizedVirtual
    ///     Group: Roles
    ///     Description: Таблица, в которой перечислены замещаемые сотрудники и параметры замещения.
    /// </summary>
    public sealed class RoleDeputiesManagementDeputizedVirtualSchemeInfo
    {
        private const string name = "RoleDeputiesManagementDeputizedVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string DeputizedID = nameof(DeputizedID);
        public readonly string DeputizedName = nameof(DeputizedName);
        public readonly string MinDate = nameof(MinDate);
        public readonly string MaxDate = nameof(MaxDate);
        public readonly string IsActive = nameof(IsActive);
        public readonly string IsEnabled = nameof(IsEnabled);
        public readonly string IsPermanent = nameof(IsPermanent);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesManagementDeputizedVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesManagementHelperVirtual

    /// <summary>
    ///     ID: {81f28cf8-709b-4dde-8c9e-505d3d7870e0}
    ///     Alias: RoleDeputiesManagementHelperVirtual
    ///     Group: Roles
    /// </summary>
    public sealed class RoleDeputiesManagementHelperVirtualSchemeInfo
    {
        private const string name = "RoleDeputiesManagementHelperVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string UserID = nameof(UserID);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesManagementHelperVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesManagementRoles

    /// <summary>
    ///     ID: {91acf9b9-8476-4dc8-a239-ac6b8f250077}
    ///     Alias: RoleDeputiesManagementRoles
    ///     Group: Roles
    ///     Description: Роли секции \"Мои замещения\"
    /// </summary>
    public sealed class RoleDeputiesManagementRolesSchemeInfo
    {
        private const string name = "RoleDeputiesManagementRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesManagementRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesManagementRolesVirtual

    /// <summary>
    ///     ID: {9650456c-27ea-4f62-9073-95b9be1d49ba}
    ///     Alias: RoleDeputiesManagementRolesVirtual
    ///     Group: Roles
    /// </summary>
    public sealed class RoleDeputiesManagementRolesVirtualSchemeInfo
    {
        private const string name = "RoleDeputiesManagementRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesManagementRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesManagementUsers

    /// <summary>
    ///     ID: {b8f9c863-22fd-4d63-a7cf-b9f0de519b47}
    ///     Alias: RoleDeputiesManagementUsers
    ///     Group: Roles
    ///     Description: Сотрудники секции \"Мои замещения\"
    /// </summary>
    public sealed class RoleDeputiesManagementUsersSchemeInfo
    {
        private const string name = "RoleDeputiesManagementUsers";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string PersonalRoleID = nameof(PersonalRoleID);
        public readonly string PersonalRoleName = nameof(PersonalRoleName);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesManagementUsersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesManagementUsersVirtual

    /// <summary>
    ///     ID: {9dc135b0-21b8-4deb-ab65-bdda57a3fbb5}
    ///     Alias: RoleDeputiesManagementUsersVirtual
    ///     Group: Roles
    /// </summary>
    public sealed class RoleDeputiesManagementUsersVirtualSchemeInfo
    {
        private const string name = "RoleDeputiesManagementUsersVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string PersonalRoleID = nameof(PersonalRoleID);
        public readonly string PersonalRoleName = nameof(PersonalRoleName);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesManagementUsersVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesManagementVirtual

    /// <summary>
    ///     ID: {79dca225-d99c-4dfd-94d9-27ed3ab15046}
    ///     Alias: RoleDeputiesManagementVirtual
    ///     Group: Roles
    /// </summary>
    public sealed class RoleDeputiesManagementVirtualSchemeInfo
    {
        private const string name = "RoleDeputiesManagementVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string MinDate = nameof(MinDate);
        public readonly string MaxDate = nameof(MaxDate);
        public readonly string IsActive = nameof(IsActive);
        public readonly string IsEnabled = nameof(IsEnabled);
        public readonly string IsPermanent = nameof(IsPermanent);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesManagementVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesNestedManagement

    /// <summary>
    ///     ID: {dd329f32-adf0-4336-bd9e-fa084c0fe494}
    ///     Alias: RoleDeputiesNestedManagement
    ///     Group: Acl
    /// </summary>
    public sealed class RoleDeputiesNestedManagementSchemeInfo
    {
        private const string name = "RoleDeputiesNestedManagement";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string MinDate = nameof(MinDate);
        public readonly string MaxDate = nameof(MaxDate);
        public readonly string IsActive = nameof(IsActive);
        public readonly string IsEnabled = nameof(IsEnabled);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesNestedManagementSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesNestedManagementTypes

    /// <summary>
    ///     ID: {0958f50b-8fd2-4e65-9531-fd540f3150ab}
    ///     Alias: RoleDeputiesNestedManagementTypes
    ///     Group: Acl
    /// </summary>
    public sealed class RoleDeputiesNestedManagementTypesSchemeInfo
    {
        private const string name = "RoleDeputiesNestedManagementTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesNestedManagementTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesNestedManagementTypesVirtual

    /// <summary>
    ///     ID: {a8c71408-d1a3-4dbc-abcb-287dd7b7c648}
    ///     Alias: RoleDeputiesNestedManagementTypesVirtual
    ///     Group: Acl
    /// </summary>
    public sealed class RoleDeputiesNestedManagementTypesVirtualSchemeInfo
    {
        private const string name = "RoleDeputiesNestedManagementTypesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesNestedManagementTypesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesNestedManagementUsers

    /// <summary>
    ///     ID: {c9bf7542-de37-4fad-9cda-6b1a5a4964b7}
    ///     Alias: RoleDeputiesNestedManagementUsers
    ///     Group: Acl
    /// </summary>
    public sealed class RoleDeputiesNestedManagementUsersSchemeInfo
    {
        private const string name = "RoleDeputiesNestedManagementUsers";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string PersonalRoleID = nameof(PersonalRoleID);
        public readonly string PersonalRoleName = nameof(PersonalRoleName);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesNestedManagementUsersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesNestedManagementUsersVirtual

    /// <summary>
    ///     ID: {6d0cfd99-aa36-4992-9231-eea478138fe6}
    ///     Alias: RoleDeputiesNestedManagementUsersVirtual
    ///     Group: Acl
    /// </summary>
    public sealed class RoleDeputiesNestedManagementUsersVirtualSchemeInfo
    {
        private const string name = "RoleDeputiesNestedManagementUsersVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string PersonalRoleID = nameof(PersonalRoleID);
        public readonly string PersonalRoleName = nameof(PersonalRoleName);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesNestedManagementUsersVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleDeputiesNestedManagementVirtual

    /// <summary>
    ///     ID: {3937aa4f-0658-4e8b-a25a-911802f1fa82}
    ///     Alias: RoleDeputiesNestedManagementVirtual
    ///     Group: Acl
    /// </summary>
    public sealed class RoleDeputiesNestedManagementVirtualSchemeInfo
    {
        private const string name = "RoleDeputiesNestedManagementVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string MinDate = nameof(MinDate);
        public readonly string MaxDate = nameof(MaxDate);
        public readonly string IsEnabled = nameof(IsEnabled);
        public readonly string IsPermanent = nameof(IsPermanent);

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesNestedManagementVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleGenerators

    /// <summary>
    ///     ID: {747bb53c-9e47-418d-892d-fb52a18eb42d}
    ///     Alias: RoleGenerators
    ///     Group: Roles
    ///     Description: Генераторы метаролей.
    /// </summary>
    public sealed class RoleGeneratorsSchemeInfo
    {
        private const string name = "RoleGenerators";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string SqlText = nameof(SqlText);
        public readonly string SchedulingTypeID = nameof(SchedulingTypeID);
        public readonly string CronScheduling = nameof(CronScheduling);
        public readonly string PeriodScheduling = nameof(PeriodScheduling);
        public readonly string LastErrorDate = nameof(LastErrorDate);
        public readonly string LastErrorText = nameof(LastErrorText);
        public readonly string Description = nameof(Description);
        public readonly string LastSuccessfulRecalcDate = nameof(LastSuccessfulRecalcDate);
        public readonly string ScheduleAtLaunch = nameof(ScheduleAtLaunch);
        public readonly string DisableDeputies = nameof(DisableDeputies);

        #endregion

        #region ToString

        public static implicit operator string(RoleGeneratorsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Roles

    /// <summary>
    ///     ID: {81f6010b-9641-4aa5-8897-b8e8603fbf4b}
    ///     Alias: Roles
    ///     Group: Roles
    ///     Description: Roles.
    /// </summary>
    public sealed class RolesSchemeInfo
    {
        private const string name = "Roles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string TypeID = nameof(TypeID);
        public readonly string ParentID = nameof(ParentID);
        public readonly string ParentName = nameof(ParentName);
        public readonly string Hidden = nameof(Hidden);
        public readonly string Description = nameof(Description);
        public readonly string AdSyncID = nameof(AdSyncID);
        public readonly string AdSyncDate = nameof(AdSyncDate);
        public readonly string AdSyncDisableUpdate = nameof(AdSyncDisableUpdate);
        public readonly string AdSyncIndependent = nameof(AdSyncIndependent);
        public readonly string AdSyncWhenChanged = nameof(AdSyncWhenChanged);
        public readonly string AdSyncDistinguishedName = nameof(AdSyncDistinguishedName);
        public readonly string AdSyncHash = nameof(AdSyncHash);
        public readonly string ExternalID = nameof(ExternalID);
        public readonly string TimeZoneID = nameof(TimeZoneID);
        public readonly string TimeZoneShortName = nameof(TimeZoneShortName);
        public readonly string TimeZoneUtcOffsetMinutes = nameof(TimeZoneUtcOffsetMinutes);
        public readonly string TimeZoneCodeName = nameof(TimeZoneCodeName);
        public readonly string InheritTimeZone = nameof(InheritTimeZone);
        public readonly string CalendarID = nameof(CalendarID);
        public readonly string CalendarName = nameof(CalendarName);
        public readonly string DeputiesExpired = nameof(DeputiesExpired);
        public readonly string DisableDeputies = nameof(DisableDeputies);

        #endregion

        #region ToString

        public static implicit operator string(RolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleTypes

    /// <summary>
    ///     ID: {8d6cb6a6-c3f5-4c92-88d7-0cc6b8e8d09d}
    ///     Alias: RoleTypes
    ///     Group: Roles
    ///     Description: Типы ролей.
    /// </summary>
    public sealed class RoleTypesSchemeInfo
    {
        private const string name = "RoleTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class StaticRole
            {
                    public const int ID = 0;
                    public const string Name = "$CardTypes_TypesNames_StaticRole";
            }
            public static class PersonalRole
            {
                    public const int ID = 1;
                    public const string Name = "$CardTypes_TypesNames_PersonalRole";
            }
            public static class DepartmentRole
            {
                    public const int ID = 2;
                    public const string Name = "$CardTypes_TypesNames_DepartmentRole";
            }
            public static class DynamicRole
            {
                    public const int ID = 3;
                    public const string Name = "$CardTypes_TypesNames_DynamicRole";
            }
            public static class ContextRole
            {
                    public const int ID = 4;
                    public const string Name = "$CardTypes_TypesNames_ContextRole";
            }
            public static class Metarole
            {
                    public const int ID = 5;
                    public const string Name = "$CardTypes_TypesNames_Metarole";
            }
            public static class TaskRole
            {
                    public const int ID = 6;
                    public const string Name = "$CardTypes_TypesNames_TaskRole";
            }
            public static class SmartRole
            {
                    public const int ID = 7;
                    public const string Name = "$CardTypes_TypesNames_SmartRole";
            }
            public static class NestedRole
            {
                    public const int ID = 8;
                    public const string Name = "$CardTypes_TypesNames_NestedRole";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(RoleTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region RoleTypes Enumeration

    public sealed class RoleTypes
    {
        public readonly int ID;
        public readonly string Name;

        public RoleTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region RoleUsers

    /// <summary>
    ///     ID: {a3a271db-3ce6-47c7-b75e-87dcc9dc052a}
    ///     Alias: RoleUsers
    ///     Group: Roles
    ///     Description: Состав роли (список пользователей, включённых в роль).
    /// </summary>
    public sealed class RoleUsersSchemeInfo
    {
        private const string name = "RoleUsers";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string IsDeputy = nameof(IsDeputy);

        #endregion

        #region ToString

        public static implicit operator string(RoleUsersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region RoleUsersVirtual

    /// <summary>
    ///     ID: {47428527-dd1f-4e52-9ba5-a2a988abdf93}
    ///     Alias: RoleUsersVirtual
    ///     Group: Roles
    ///     Description: Состав роли без учёта замещений.
    /// </summary>
    public sealed class RoleUsersVirtualSchemeInfo
    {
        private const string name = "RoleUsersVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);

        #endregion

        #region ToString

        public static implicit operator string(RoleUsersVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Satellites

    /// <summary>
    ///     ID: {608289ef-42c8-4f6e-8d4f-27ef725732b5}
    ///     Alias: Satellites
    ///     Group: System
    /// </summary>
    public sealed class SatellitesSchemeInfo
    {
        private const string name = "Satellites";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string MainCardID = nameof(MainCardID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TaskID = nameof(TaskID);

        #endregion

        #region ToString

        public static implicit operator string(SatellitesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SchedulingTypes

    /// <summary>
    ///     ID: {3cf60a31-28d4-42ad-86b2-343a298ea7a8}
    ///     Alias: SchedulingTypes
    ///     Group: Roles
    ///     Description: Способы указания расписания для выполнения заданий.
    /// </summary>
    public sealed class SchedulingTypesSchemeInfo
    {
        private const string name = "SchedulingTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Period
            {
                    public const int ID = 0;
                    public const string Name = "Period";
            }
            public static class Cron
            {
                    public const int ID = 1;
                    public const string Name = "Cron";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(SchedulingTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region SchedulingTypes Enumeration

    public sealed class SchedulingTypes
    {
        public readonly int ID;
        public readonly string Name;

        public SchedulingTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region Scheme

    /// <summary>
    ///     ID: {c4fcd8d3-fcb1-451f-98f4-e352cd8a3a41}
    ///     Alias: Scheme
    ///     Group: System
    ///     Description: Scheme properties
    /// </summary>
    public sealed class SchemeSchemeInfo
    {
        private const string name = "Scheme";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Description = nameof(Description);
        public readonly string CollationSqlServer = nameof(CollationSqlServer);
        public readonly string CollationPostgreSql = nameof(CollationPostgreSql);
        public readonly string SchemeVersion = nameof(SchemeVersion);
        public readonly string Modified = nameof(Modified);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);
        public readonly string DbmsVersion = nameof(DbmsVersion);

        #endregion

        #region ToString

        public static implicit operator string(SchemeSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SearchQueries

    /// <summary>
    ///     ID: {d0dde291-0d94-4e76-9f69-902809975216}
    ///     Alias: SearchQueries
    ///     Group: System
    /// </summary>
    public sealed class SearchQueriesSchemeInfo
    {
        private const string name = "SearchQueries";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Metadata = nameof(Metadata);
        public readonly string ViewAlias = nameof(ViewAlias);
        public readonly string IsPublic = nameof(IsPublic);
        public readonly string LastModified = nameof(LastModified);
        public readonly string CreatedByUserID = nameof(CreatedByUserID);
        public readonly string TemplateCompositionID = nameof(TemplateCompositionID);

        #endregion

        #region ToString

        public static implicit operator string(SearchQueriesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SectionChangedCondition

    /// <summary>
    ///     ID: {3b9d9643-ed47-4301-94b2-8eedcabc23bc}
    ///     Alias: SectionChangedCondition
    ///     Group: Acl
    ///     Description: Секция для условий, проверяющих изменение секции.
    /// </summary>
    public sealed class SectionChangedConditionSchemeInfo
    {
        private const string name = "SectionChangedCondition";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string SectionID = nameof(SectionID);
        public readonly string SectionName = nameof(SectionName);
        public readonly string SectionTypeID = nameof(SectionTypeID);

        #endregion

        #region ToString

        public static implicit operator string(SectionChangedConditionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SequencesInfo

    /// <summary>
    ///     ID: {f113a406-970b-4c1b-820f-9d960c37692a}
    ///     Alias: SequencesInfo
    ///     Group: System
    /// </summary>
    public sealed class SequencesInfoSchemeInfo
    {
        private const string name = "SequencesInfo";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region ToString

        public static implicit operator string(SequencesInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SequencesIntervals

    /// <summary>
    ///     ID: {510bf28c-bccf-4701-b9fa-1081c22c2ef9}
    ///     Alias: SequencesIntervals
    ///     Group: System
    /// </summary>
    public sealed class SequencesIntervalsSchemeInfo
    {
        private const string name = "SequencesIntervals";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string First = nameof(First);
        public readonly string Last = nameof(Last);

        #endregion

        #region ToString

        public static implicit operator string(SequencesIntervalsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SequencesReserved

    /// <summary>
    ///     ID: {506e2fe6-397e-45c1-ae35-22cd7e85b14d}
    ///     Alias: SequencesReserved
    ///     Group: System
    /// </summary>
    public sealed class SequencesReservedSchemeInfo
    {
        private const string name = "SequencesReserved";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Number = nameof(Number);
        public readonly string Date = nameof(Date);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);

        #endregion

        #region ToString

        public static implicit operator string(SequencesReservedSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ServerInstances

    /// <summary>
    ///     ID: {c3d76e97-459f-41e0-8d45-56fb19b5e07e}
    ///     Alias: ServerInstances
    ///     Group: System
    ///     Description: Таблица с настройками базы данных.
    /// </summary>
    public sealed class ServerInstancesSchemeInfo
    {
        private const string name = "ServerInstances";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Description = nameof(Description);
        public readonly string DefaultFileSourceID = nameof(DefaultFileSourceID);
        public readonly string WebAddress = nameof(WebAddress);
        public readonly string MobileApprovalEmail = nameof(MobileApprovalEmail);
        public readonly string ViewGetDataCommandTimeout = nameof(ViewGetDataCommandTimeout);
        public readonly string FileExtensionsWithoutPreview = nameof(FileExtensionsWithoutPreview);
        public readonly string FileExtensionsConvertablePreview = nameof(FileExtensionsConvertablePreview);
        public readonly string DenyMobileFileDownload = nameof(DenyMobileFileDownload);
        public readonly string BlockWindowsAndLdapUsers = nameof(BlockWindowsAndLdapUsers);
        public readonly string CsvEncoding = nameof(CsvEncoding);
        public readonly string CsvSeparator = nameof(CsvSeparator);
        public readonly string MaxFailedLoginAttemptsBeforeBlocked = nameof(MaxFailedLoginAttemptsBeforeBlocked);
        public readonly string MaxFailedLoginAttemptsInSeries = nameof(MaxFailedLoginAttemptsInSeries);
        public readonly string BlockedSeriesDueDateHours = nameof(BlockedSeriesDueDateHours);
        public readonly string FailedLoginAttemptsSeriesTime = nameof(FailedLoginAttemptsSeriesTime);
        public readonly string SessionInactivityHours = nameof(SessionInactivityHours);
        public readonly string MinPasswordLength = nameof(MinPasswordLength);
        public readonly string EnforceStrongPasswords = nameof(EnforceStrongPasswords);
        public readonly string PasswordExpirationDays = nameof(PasswordExpirationDays);
        public readonly string PasswordExpirationNotificationDays = nameof(PasswordExpirationNotificationDays);
        public readonly string UniquePasswordCount = nameof(UniquePasswordCount);
        public readonly string MaxFileSizeMb = nameof(MaxFileSizeMb);
        public readonly string LargeFileSizeMb = nameof(LargeFileSizeMb);
        public readonly string WebDefaultWallpaper = nameof(WebDefaultWallpaper);
        public readonly string ForumRefreshInterval = nameof(ForumRefreshInterval);
        public readonly string ModifyMessageAtNoOlderThan = nameof(ModifyMessageAtNoOlderThan);
        public readonly string FullTextMessageSearch = nameof(FullTextMessageSearch);
        public readonly string HelpUrl = nameof(HelpUrl);
        public readonly string UseNewDeputies = nameof(UseNewDeputies);
        public readonly string DefaultCalendarID = nameof(DefaultCalendarID);
        public readonly string DefaultCalendarName = nameof(DefaultCalendarName);
        public readonly string UseRemainingTimeInAstronomicalDays = nameof(UseRemainingTimeInAstronomicalDays);
        public readonly string ForumMaxAttachedFileSizeKb = nameof(ForumMaxAttachedFileSizeKb);
        public readonly string ForumMaxAttachedFiles = nameof(ForumMaxAttachedFiles);
        public readonly string ForumMaxMessageInlines = nameof(ForumMaxMessageInlines);
        public readonly string ForumMaxMessageSize = nameof(ForumMaxMessageSize);
        public readonly string DisableDesktopLinksInNotifications = nameof(DisableDesktopLinksInNotifications);
        public readonly string FileConverterTypeID = nameof(FileConverterTypeID);
        public readonly string FileConverterTypeName = nameof(FileConverterTypeName);
        public readonly string DefaultActionHistoryDatabaseID = nameof(DefaultActionHistoryDatabaseID);
        public readonly string DeskiDisabled = nameof(DeskiDisabled);
        public readonly string DeskiMobileEnabled = nameof(DeskiMobileEnabled);
        public readonly string DeskiMobileJwtLifeTime = nameof(DeskiMobileJwtLifeTime);

        #endregion

        #region ToString

        public static implicit operator string(ServerInstancesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SessionActivity

    /// <summary>
    ///     ID: {a396cc70-75bb-427b-ac42-4978fb5575ac}
    ///     Alias: SessionActivity
    ///     Group: System
    ///     Description: Таблица для хранения признаков активности и последней активности сессии.
    ///                  Дополняет таблицу Sessions.
    /// </summary>
    public sealed class SessionActivitySchemeInfo
    {
        private const string name = "SessionActivity";

        #region Columns

        public readonly string SessionID = nameof(SessionID);
        public readonly string IsActive = nameof(IsActive);
        public readonly string LastActivity = nameof(LastActivity);

        #endregion

        #region ToString

        public static implicit operator string(SessionActivitySchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Sessions

    /// <summary>
    ///     ID: {bbd3d574-a33e-49fb-867d-db3c6811365e}
    ///     Alias: Sessions
    ///     Group: System
    ///     Description: Открытые сессии.
    /// </summary>
    public sealed class SessionsSchemeInfo
    {
        private const string name = "Sessions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ApplicationID = nameof(ApplicationID);
        public readonly string LicenseTypeID = nameof(LicenseTypeID);
        public readonly string LoginTypeID = nameof(LoginTypeID);
        public readonly string AccessLevelID = nameof(AccessLevelID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string UserLogin = nameof(UserLogin);
        public readonly string DeviceTypeID = nameof(DeviceTypeID);
        public readonly string ServiceTypeID = nameof(ServiceTypeID);
        public readonly string HostIP = nameof(HostIP);
        public readonly string HostName = nameof(HostName);
        public readonly string Created = nameof(Created);
        public readonly string Expires = nameof(Expires);
        public readonly string UtcOffsetMinutes = nameof(UtcOffsetMinutes);
        public readonly string OSName = nameof(OSName);
        public readonly string UserAgent = nameof(UserAgent);
        public readonly string TimeZoneUtcOffset = nameof(TimeZoneUtcOffset);
        public readonly string Client64Bit = nameof(Client64Bit);
        public readonly string Client64BitOS = nameof(Client64BitOS);
        public readonly string CalendarID = nameof(CalendarID);
        public readonly string Culture = nameof(Culture);
        public readonly string UICulture = nameof(UICulture);

        #endregion

        #region ToString

        public static implicit operator string(SessionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SessionServiceTypes

    /// <summary>
    ///     ID: {62c1a795-1688-48a1-b0af-d77032c90bab}
    ///     Alias: SessionServiceTypes
    ///     Group: System
    ///     Description: Типы сессий, которые определяются типом веб-сервиса: для desktop- или для web-клиентов, или веб-сервис отсутствует (прямое взаимодействие с БД).
    /// </summary>
    public sealed class SessionServiceTypesSchemeInfo
    {
        private const string name = "SessionServiceTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Unknown
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_SessionServiceTypes_Unknown";
            }
            public static class DesktopClient
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_SessionServiceTypes_DesktopClient";
            }
            public static class WebClient
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_SessionServiceTypes_WebClient";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(SessionServiceTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region SessionServiceTypes Enumeration

    public sealed class SessionServiceTypes
    {
        public readonly int ID;
        public readonly string Name;

        public SessionServiceTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region SignatureCertificateSettings

    /// <summary>
    ///     ID: {faf66527-24c2-4f20-afa8-46915e5fd4d6}
    ///     Alias: SignatureCertificateSettings
    ///     Group: System
    /// </summary>
    public sealed class SignatureCertificateSettingsSchemeInfo
    {
        private const string name = "SignatureCertificateSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string StartDate = nameof(StartDate);
        public readonly string EndDate = nameof(EndDate);
        public readonly string IsValidDate = nameof(IsValidDate);
        public readonly string Company = nameof(Company);
        public readonly string Subject = nameof(Subject);
        public readonly string Issuer = nameof(Issuer);

        #endregion

        #region ToString

        public static implicit operator string(SignatureCertificateSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SignatureDigestAlgorithms

    /// <summary>
    ///     ID: {9180bf30-3b8b-4adc-a285-d9ee97aea219}
    ///     Alias: SignatureDigestAlgorithms
    ///     Group: System
    ///     Description: Идентификаторы алгоритмов хеширования
    /// </summary>
    public sealed class SignatureDigestAlgorithmsSchemeInfo
    {
        private const string name = "SignatureDigestAlgorithms";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string OID = nameof(OID);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Enum256
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_Signature_DigestAlgos_GOST12_256";
                    public const string OID = "1.2.643.7.1.1.2.2";
            }
            public static class Enum512
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_Signature_DigestAlgos_GOST12_512";
                    public const string OID = "1.2.643.7.1.1.2.3";
            }
            public static class GOST94
            {
                    public const int ID = 3;
                    public const string Name = "$Enum_Signature_DigestAlgos_GOST94";
                    public const string OID = "1.2.643.2.2.9";
            }
            public static class SHA1
            {
                    public const int ID = 7;
                    public const string Name = "SHA1";
                    public const string OID = "1.3.14.3.2.26";
            }
            public static class SHA256
            {
                    public const int ID = 8;
                    public const string Name = "SHA256";
                    public const string OID = "2.16.840.1.101.3.4.2.1";
            }
            public static class SHA384
            {
                    public const int ID = 9;
                    public const string Name = "SHA384";
                    public const string OID = "2.16.840.1.101.3.4.2.2";
            }
            public static class SHA512
            {
                    public const int ID = 10;
                    public const string Name = "SHA512";
                    public const string OID = "2.16.840.1.101.3.4.2.3";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(SignatureDigestAlgorithmsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region SignatureDigestAlgorithms Enumeration

    public sealed class SignatureDigestAlgorithms
    {
        public readonly int ID;
        public readonly string Name;
        public readonly string OID;

        public SignatureDigestAlgorithms(int ID, string Name, string OID)
        {
            this.ID = ID;
            this.Name = Name;
            this.OID = OID;
        }
    }

    #endregion

    #endregion

    #region SignatureEncryptDigestSettings

    /// <summary>
    ///     ID: {7c57bbba-8acc-4abf-b3cc-372399b68dbc}
    ///     Alias: SignatureEncryptDigestSettings
    ///     Group: System
    /// </summary>
    public sealed class SignatureEncryptDigestSettingsSchemeInfo
    {
        private const string name = "SignatureEncryptDigestSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string EncryptionAlgorithmID = nameof(EncryptionAlgorithmID);
        public readonly string EncryptionAlgorithmName = nameof(EncryptionAlgorithmName);
        public readonly string EncryptionAlgorithmOID = nameof(EncryptionAlgorithmOID);
        public readonly string DigestAlgorithmID = nameof(DigestAlgorithmID);
        public readonly string DigestAlgorithmName = nameof(DigestAlgorithmName);
        public readonly string DigestAlgorithmOID = nameof(DigestAlgorithmOID);
        public readonly string EdsManagerName = nameof(EdsManagerName);

        #endregion

        #region ToString

        public static implicit operator string(SignatureEncryptDigestSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SignatureEncryptionAlgorithms

    /// <summary>
    ///     ID: {93f36ef0-b0ca-4726-9038-b10339db4b00}
    ///     Alias: SignatureEncryptionAlgorithms
    ///     Group: System
    ///     Description: Идентификаторы алгоритмов подписи
    /// </summary>
    public sealed class SignatureEncryptionAlgorithmsSchemeInfo
    {
        private const string name = "SignatureEncryptionAlgorithms";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string OID = nameof(OID);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Enum256
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_Signature_EncAlgos_GOST12_256";
                    public const string OID = "1.2.643.7.1.1.1.1";
            }
            public static class Enum512
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_Signature_EncAlgos_GOST12_512";
                    public const string OID = "1.2.643.7.1.1.1.2";
            }
            public static class GOST2001
            {
                    public const int ID = 3;
                    public const string Name = "$Enum_Signature_EncAlgos_GOST2001";
                    public const string OID = "1.2.643.2.2.19";
            }
            public static class Others
            {
                    public const int ID = 4;
                    public const string Name = "$Enum_Signature_EncAlgos_Others";
                    public const string OID = "";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(SignatureEncryptionAlgorithmsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region SignatureEncryptionAlgorithms Enumeration

    public sealed class SignatureEncryptionAlgorithms
    {
        public readonly int ID;
        public readonly string Name;
        public readonly string OID;

        public SignatureEncryptionAlgorithms(int ID, string Name, string OID)
        {
            this.ID = ID;
            this.Name = Name;
            this.OID = OID;
        }
    }

    #endregion

    #endregion

    #region SignatureManagerVirtual

    /// <summary>
    ///     ID: {72eb4e5a-f328-40e6-bb2d-18ea0a9a9d2b}
    ///     Alias: SignatureManagerVirtual
    ///     Group: System
    /// </summary>
    public sealed class SignatureManagerVirtualSchemeInfo
    {
        private const string name = "SignatureManagerVirtual";

        #region Columns

        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class DefaultEDSManager
            {
                    public const string Name = "DefaultEDSManager";
            }
            public static class CryptoProEDSManager
            {
                    public const string Name = "CryptoProEDSManager";
            }
            public static class ServiceEDSManagerForCMS
            {
                    public const string Name = "ServiceEDSManagerForCMS";
            }
            public static class ServiceEDSManagerForCAdES
            {
                    public const string Name = "ServiceEDSManagerForCAdES";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(SignatureManagerVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region SignatureManagerVirtual Enumeration

    public sealed class SignatureManagerVirtual
    {
        public readonly string Name;

        public SignatureManagerVirtual(string Name)
        {
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region SignaturePackagings

    /// <summary>
    ///     ID: {15620b78-46b8-4520-aa60-4bfefe67c731}
    ///     Alias: SignaturePackagings
    ///     Group: System
    ///     Description: Варианты упаковки подписи
    /// </summary>
    public sealed class SignaturePackagingsSchemeInfo
    {
        private const string name = "SignaturePackagings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Detached
            {
                    public const int ID = 3;
                    public const string Name = "$Enum_Signature_Packagings_Detached";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(SignaturePackagingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region SignaturePackagings Enumeration

    public sealed class SignaturePackagings
    {
        public readonly int ID;
        public readonly string Name;

        public SignaturePackagings(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region SignatureProfiles

    /// <summary>
    ///     ID: {eca29bb9-3085-4556-b19a-6015cbc8fb25}
    ///     Alias: SignatureProfiles
    ///     Group: System
    ///     Description: Профили цифровой подписи
    /// </summary>
    public sealed class SignatureProfilesSchemeInfo
    {
        private const string name = "SignatureProfiles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class None
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_Signature_Profiles_None";
            }
            public static class BES
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_Signature_Profiles_BES";
            }
            public static class T
            {
                    public const int ID = 3;
                    public const string Name = "$Enum_Signature_Profiles_T";
            }
            public static class C
            {
                    public const int ID = 4;
                    public const string Name = "$Enum_Signature_Profiles_C";
            }
            public static class XL
            {
                    public const int ID = 5;
                    public const string Name = "$Enum_Signature_Profiles_XL";
            }
            public static class Type1
            {
                    public const int ID = 6;
                    public const string Name = "$Enum_Signature_Profiles_X_Type1";
            }
            public static class Type2
            {
                    public const int ID = 7;
                    public const string Name = "$Enum_Signature_Profiles_X_Type2";
            }
            public static class Type11
            {
                    public const int ID = 8;
                    public const string Name = "$Enum_Signature_Profiles_XL_Type1";
            }
            public static class Type22
            {
                    public const int ID = 9;
                    public const string Name = "$Enum_Signature_Profiles_XL_Type2";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(SignatureProfilesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region SignatureProfiles Enumeration

    public sealed class SignatureProfiles
    {
        public readonly int ID;
        public readonly string Name;

        public SignatureProfiles(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region SignatureSettings

    /// <summary>
    ///     ID: {076b2050-e20b-412b-942b-b4cb063e6941}
    ///     Alias: SignatureSettings
    ///     Group: System
    ///     Description: Таблица настроек цифровой подписи
    /// </summary>
    public sealed class SignatureSettingsSchemeInfo
    {
        private const string name = "SignatureSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string SignatureTypeID = nameof(SignatureTypeID);
        public readonly string SignatureTypeName = nameof(SignatureTypeName);
        public readonly string SignatureProfileID = nameof(SignatureProfileID);
        public readonly string SignatureProfileName = nameof(SignatureProfileName);
        public readonly string TSPAddress = nameof(TSPAddress);
        public readonly string OCSPAddress = nameof(OCSPAddress);
        public readonly string CRLAddress = nameof(CRLAddress);
        public readonly string SignaturePackagingID = nameof(SignaturePackagingID);
        public readonly string SignaturePackagingName = nameof(SignaturePackagingName);
        public readonly string TSPUserName = nameof(TSPUserName);
        public readonly string TSPPassword = nameof(TSPPassword);
        public readonly string TSPDigestAlgorithmID = nameof(TSPDigestAlgorithmID);
        public readonly string TSPDigestAlgorithmOID = nameof(TSPDigestAlgorithmOID);
        public readonly string TSPDigestAlgorithmName = nameof(TSPDigestAlgorithmName);
        public readonly string UseSystemRootCertificates = nameof(UseSystemRootCertificates);

        #endregion

        #region ToString

        public static implicit operator string(SignatureSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SignatureTypes

    /// <summary>
    ///     ID: {577baaea-6832-4eb7-9333-60661367720e}
    ///     Alias: SignatureTypes
    ///     Group: System
    ///     Description: Таблица видов подписей
    /// </summary>
    public sealed class SignatureTypesSchemeInfo
    {
        private const string name = "SignatureTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class None
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_SignatureTypes_None";
            }
            public static class CAdES
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_SignatureTypes_CAdES";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(SignatureTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region SignatureTypes Enumeration

    public sealed class SignatureTypes
    {
        public readonly int ID;
        public readonly string Name;

        public SignatureTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region SmartRoleGeneratorInfo

    /// <summary>
    ///     ID: {c44db46a-349f-45ec-b0ab-ec212c09b276}
    ///     Alias: SmartRoleGeneratorInfo
    ///     Group: Acl
    ///     Description: Таблица с информацией о расчёте генераторов умных ролей.
    /// </summary>
    public sealed class SmartRoleGeneratorInfoSchemeInfo
    {
        private const string name = "SmartRoleGeneratorInfo";

        #region Columns

        public readonly string GeneratorID = nameof(GeneratorID);
        public readonly string GeneratorVersion = nameof(GeneratorVersion);

        #endregion

        #region ToString

        public static implicit operator string(SmartRoleGeneratorInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SmartRoleGenerators

    /// <summary>
    ///     ID: {5f3a0dbc-2fc4-4269-8a5d-eb95f39970ba}
    ///     Alias: SmartRoleGenerators
    ///     Group: Acl
    ///     Description: Основная секция для генераторов умных ролей.
    /// </summary>
    public sealed class SmartRoleGeneratorsSchemeInfo
    {
        private const string name = "SmartRoleGenerators";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string RolesSelectorSql = nameof(RolesSelectorSql);
        public readonly string OwnersSelectorSql = nameof(OwnersSelectorSql);
        public readonly string RoleNameTemplate = nameof(RoleNameTemplate);
        public readonly string HideRoles = nameof(HideRoles);
        public readonly string InitRoles = nameof(InitRoles);
        public readonly string OwnerDataSelectorSql = nameof(OwnerDataSelectorSql);
        public readonly string Description = nameof(Description);
        public readonly string IsDisabled = nameof(IsDisabled);
        public readonly string EnableErrorLogging = nameof(EnableErrorLogging);
        public readonly string Version = nameof(Version);
        public readonly string DisableDeputies = nameof(DisableDeputies);

        #endregion

        #region ToString

        public static implicit operator string(SmartRoleGeneratorsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SmartRoleMembers

    /// <summary>
    ///     ID: {73cbcd25-8709-4c3d-9091-3db6ccba5055}
    ///     Alias: SmartRoleMembers
    ///     Group: Acl
    /// </summary>
    public sealed class SmartRoleMembersSchemeInfo
    {
        private const string name = "SmartRoleMembers";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);

        #endregion

        #region ToString

        public static implicit operator string(SmartRoleMembersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region SmartRoles

    /// <summary>
    ///     ID: {844013f9-7faa-422a-b583-2b04ae46f0be}
    ///     Alias: SmartRoles
    ///     Group: Acl
    ///     Description: Основная секция для записей настроек умных ролей.
    /// </summary>
    public sealed class SmartRolesSchemeInfo
    {
        private const string name = "SmartRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RuleID = nameof(RuleID);
        public readonly string RuleName = nameof(RuleName);
        public readonly string ParentID = nameof(ParentID);
        public readonly string OwnerID = nameof(OwnerID);
        public readonly string OwnerName = nameof(OwnerName);

        #endregion

        #region ToString

        public static implicit operator string(SmartRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Tables

    /// <summary>
    ///     ID: {66b31fcc-b8fa-465a-91f2-0dd391cc76ec}
    ///     Alias: Tables
    ///     Group: System
    ///     Description: Contains metadata that describes tables which used by Tessa
    /// </summary>
    public sealed class TablesSchemeInfo
    {
        private const string name = "Tables";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Definition = nameof(Definition);

        #endregion

        #region ToString

        public static implicit operator string(TablesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TagCards

    /// <summary>
    ///     ID: {7ecf1fb4-cd7b-4fea-9a3b-6e22b2186ed6}
    ///     Alias: TagCards
    ///     Group: Tags
    ///     Description: Таблица с тегами карточек.
    /// </summary>
    public sealed class TagCardsSchemeInfo
    {
        private const string name = "TagCards";

        #region Columns

        public readonly string TagID = nameof(TagID);
        public readonly string CardID = nameof(CardID);
        public readonly string UserID = nameof(UserID);
        public readonly string SetAt = nameof(SetAt);

        #endregion

        #region ToString

        public static implicit operator string(TagCardsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TagCondition

    /// <summary>
    ///     ID: {8a9a0383-b94a-40c0-aa3e-7b461f25b598}
    ///     Alias: TagCondition
    ///     Group: Tags
    ///     Description: Секция для условия для правил уведомлений, проверяющая Тег.
    /// </summary>
    public sealed class TagConditionSchemeInfo
    {
        private const string name = "TagCondition";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TagID = nameof(TagID);
        public readonly string TagName = nameof(TagName);

        #endregion

        #region ToString

        public static implicit operator string(TagConditionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TagEditors

    /// <summary>
    ///     ID: {c4ee86f8-3022-432b-ae77-e1ca4a47c891}
    ///     Alias: TagEditors
    ///     Group: Tags
    /// </summary>
    public sealed class TagEditorsSchemeInfo
    {
        private const string name = "TagEditors";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(TagEditorsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Tags

    /// <summary>
    ///     ID: {0bf4050e-d7d4-4cda-ab55-4a4f0148dd7f}
    ///     Alias: Tags
    ///     Group: Tags
    ///     Description: Основная секция с настройками тегов.
    /// </summary>
    public sealed class TagsSchemeInfo
    {
        private const string name = "Tags";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string OwnerID = nameof(OwnerID);
        public readonly string OwnerName = nameof(OwnerName);
        public readonly string Foreground = nameof(Foreground);
        public readonly string Background = nameof(Background);
        public readonly string IsCommon = nameof(IsCommon);

        #endregion

        #region ToString

        public static implicit operator string(TagsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TagSharedWith

    /// <summary>
    ///     ID: {3b4e5980-82d8-4bce-adb3-4fbc88c3e03a}
    ///     Alias: TagSharedWith
    ///     Group: Tags
    ///     Description: Список ролей, которым доступен тег.
    /// </summary>
    public sealed class TagSharedWithSchemeInfo
    {
        private const string name = "TagSharedWith";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(TagSharedWithSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TagsUserSettingsVirtual

    /// <summary>
    ///     ID: {f4947518-a710-4693-8c8b-ab2acc42bc5a}
    ///     Alias: TagsUserSettingsVirtual
    ///     Group: Tags
    ///     Description: Виртуальная таблица для формы с пользовательскими настройками тегов
    /// </summary>
    public sealed class TagsUserSettingsVirtualSchemeInfo
    {
        private const string name = "TagsUserSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string MaxTagsDisplayed = nameof(MaxTagsDisplayed);

        #endregion

        #region ToString

        public static implicit operator string(TagsUserSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TaskAssignedRoles

    /// <summary>
    ///     ID: {2539f630-3898-457e-9e49-e1f87552caaf}
    ///     Alias: TaskAssignedRoles
    ///     Group: System
    /// </summary>
    public sealed class TaskAssignedRolesSchemeInfo
    {
        private const string name = "TaskAssignedRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TaskRoleID = nameof(TaskRoleID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string RoleTypeID = nameof(RoleTypeID);
        public readonly string Position = nameof(Position);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string Master = nameof(Master);
        public readonly string ShowInTaskDetails = nameof(ShowInTaskDetails);

        #endregion

        #region ToString

        public static implicit operator string(TaskAssignedRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TaskCommonInfo

    /// <summary>
    ///     ID: {005962e7-65f1-4763-a0ef-b76751d26de3}
    ///     Alias: TaskCommonInfo
    ///     Group: Common
    ///     Description: Общая информация для заданий.
    ///                  Во всех случаях, когда в задании надо вывести некое описание текста задания, нужно использовать эту секцию. 
    ///                  Также используется в представлении \"Мои задания\", чтобы выводить некий  текст - описание задания в табличку со списком заданий.
    /// </summary>
    public sealed class TaskCommonInfoSchemeInfo
    {
        private const string name = "TaskCommonInfo";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Info = nameof(Info);
        public readonly string KindID = nameof(KindID);
        public readonly string KindCaption = nameof(KindCaption);

        #endregion

        #region ToString

        public static implicit operator string(TaskCommonInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TaskConditionCompletionOptions

    /// <summary>
    ///     ID: {059e1354-8b89-4e86-8fa1-29395e952926}
    ///     Alias: TaskConditionCompletionOptions
    ///     Group: Acl
    ///     Description: Варианты завершения для условий проверки заданий.
    /// </summary>
    public sealed class TaskConditionCompletionOptionsSchemeInfo
    {
        private const string name = "TaskConditionCompletionOptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string CompletionOptionID = nameof(CompletionOptionID);
        public readonly string CompletionOptionCaption = nameof(CompletionOptionCaption);

        #endregion

        #region ToString

        public static implicit operator string(TaskConditionCompletionOptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TaskConditionFunctionRoles

    /// <summary>
    ///     ID: {b59a92cb-8414-4a3d-91f9-9d41de829d3f}
    ///     Alias: TaskConditionFunctionRoles
    ///     Group: Acl
    ///     Description: Список функциональных ролей для условий проверки заданий.
    /// </summary>
    public sealed class TaskConditionFunctionRolesSchemeInfo
    {
        private const string name = "TaskConditionFunctionRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string FunctionRoleID = nameof(FunctionRoleID);
        public readonly string FunctionRoleCaption = nameof(FunctionRoleCaption);

        #endregion

        #region ToString

        public static implicit operator string(TaskConditionFunctionRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TaskConditionSettings

    /// <summary>
    ///     ID: {48dcb84f-1518-4de4-8995-86c4e75a1d03}
    ///     Alias: TaskConditionSettings
    ///     Group: Acl
    ///     Description: Настройки для условий проверки задания.
    /// </summary>
    public sealed class TaskConditionSettingsSchemeInfo
    {
        private const string name = "TaskConditionSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CheckTaskCreation = nameof(CheckTaskCreation);
        public readonly string CheckTaskCompletion = nameof(CheckTaskCompletion);
        public readonly string CheckTaskFunctionRolesChanges = nameof(CheckTaskFunctionRolesChanges);

        #endregion

        #region ToString

        public static implicit operator string(TaskConditionSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TaskConditionTaskKinds

    /// <summary>
    ///     ID: {a3777728-9f01-449a-b94c-953a1e205c5b}
    ///     Alias: TaskConditionTaskKinds
    ///     Group: Acl
    ///     Description: Список видов заданий для условий проверки заданий.
    /// </summary>
    public sealed class TaskConditionTaskKindsSchemeInfo
    {
        private const string name = "TaskConditionTaskKinds";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TaskKindID = nameof(TaskKindID);
        public readonly string TaskKindCaption = nameof(TaskKindCaption);

        #endregion

        #region ToString

        public static implicit operator string(TaskConditionTaskKindsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TaskConditionTaskTypes

    /// <summary>
    ///     ID: {f7cd6753-21d7-4095-8c3a-e7175f591ad3}
    ///     Alias: TaskConditionTaskTypes
    ///     Group: Acl
    ///     Description: Типы заданий для условий проверки заданий.
    /// </summary>
    public sealed class TaskConditionTaskTypesSchemeInfo
    {
        private const string name = "TaskConditionTaskTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeName = nameof(TypeName);
        public readonly string TypeCaption = nameof(TypeCaption);

        #endregion

        #region ToString

        public static implicit operator string(TaskConditionTaskTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TaskHistory

    /// <summary>
    ///     ID: {f8deab4c-fa9d-404a-8abc-b570cd81820e}
    ///     Alias: TaskHistory
    ///     Group: System
    ///     Description: История завершённых заданий.
    /// </summary>
    public sealed class TaskHistorySchemeInfo
    {
        private const string name = "TaskHistory";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string Created = nameof(Created);
        public readonly string Planned = nameof(Planned);
        public readonly string InProgress = nameof(InProgress);
        public readonly string Completed = nameof(Completed);
        public readonly string Result = nameof(Result);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string UserDepartment = nameof(UserDepartment);
        public readonly string UserPosition = nameof(UserPosition);
        public readonly string CompletedByID = nameof(CompletedByID);
        public readonly string CompletedByName = nameof(CompletedByName);
        public readonly string CompletedByRole = nameof(CompletedByRole);
        public readonly string TimeZoneID = nameof(TimeZoneID);
        public readonly string TimeZoneUtcOffsetMinutes = nameof(TimeZoneUtcOffsetMinutes);
        public readonly string GroupRowID = nameof(GroupRowID);
        public readonly string KindID = nameof(KindID);
        public readonly string KindCaption = nameof(KindCaption);
        public readonly string CalendarID = nameof(CalendarID);
        public readonly string Settings = nameof(Settings);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string AssignedOnRole = nameof(AssignedOnRole);

        #endregion

        #region ToString

        public static implicit operator string(TaskHistorySchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TaskHistoryGroups

    /// <summary>
    ///     ID: {31644536-fba1-456c-881c-7dae73b7182c}
    ///     Alias: TaskHistoryGroups
    ///     Group: System
    /// </summary>
    public sealed class TaskHistoryGroupsSchemeInfo
    {
        private const string name = "TaskHistoryGroups";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string Caption = nameof(Caption);
        public readonly string Iteration = nameof(Iteration);

        #endregion

        #region ToString

        public static implicit operator string(TaskHistoryGroupsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TaskHistoryGroupTypes

    /// <summary>
    ///     ID: {319be329-6cd3-457a-b792-41c26a266b95}
    ///     Alias: TaskHistoryGroupTypes
    ///     Group: System
    /// </summary>
    public sealed class TaskHistoryGroupTypesSchemeInfo
    {
        private const string name = "TaskHistoryGroupTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Caption = nameof(Caption);
        public readonly string Description = nameof(Description);
        public readonly string Placeholders = nameof(Placeholders);

        #endregion

        #region ToString

        public static implicit operator string(TaskHistoryGroupTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TaskKinds

    /// <summary>
    ///     ID: {856068b1-0e78-4aa8-8e7a-4f53d91a7298}
    ///     Alias: TaskKinds
    ///     Group: System
    ///     Description: Виды заданий.
    /// </summary>
    public sealed class TaskKindsSchemeInfo
    {
        private const string name = "TaskKinds";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Caption = nameof(Caption);

        #endregion

        #region ToString

        public static implicit operator string(TaskKindsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Tasks

    /// <summary>
    ///     ID: {5bfa9936-bb5a-4e8f-89a9-180bfd8f75f8}
    ///     Alias: Tasks
    ///     Group: System
    ///     Description: Tasks of a card
    /// </summary>
    public sealed class TasksSchemeInfo
    {
        private const string name = "Tasks";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string StateID = nameof(StateID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string Planned = nameof(Planned);
        public readonly string InProgress = nameof(InProgress);
        public readonly string Created = nameof(Created);
        public readonly string CreatedByID = nameof(CreatedByID);
        public readonly string CreatedByName = nameof(CreatedByName);
        public readonly string Modified = nameof(Modified);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);
        public readonly string Digest = nameof(Digest);
        public readonly string ParentID = nameof(ParentID);
        public readonly string Postponed = nameof(Postponed);
        public readonly string PostponedTo = nameof(PostponedTo);
        public readonly string PostponeComment = nameof(PostponeComment);
        public readonly string TimeZoneID = nameof(TimeZoneID);
        public readonly string TimeZoneUtcOffsetMinutes = nameof(TimeZoneUtcOffsetMinutes);
        public readonly string Settings = nameof(Settings);
        public readonly string CalendarID = nameof(CalendarID);
        public readonly string CalendarName = nameof(CalendarName);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);

        #endregion

        #region ToString

        public static implicit operator string(TasksSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TaskStates

    /// <summary>
    ///     ID: {057a85c8-c20f-430b-bd3b-6ea9f9fb82ee}
    ///     Alias: TaskStates
    ///     Group: System
    ///     Description: Состояние задания.
    /// </summary>
    public sealed class TaskStatesSchemeInfo
    {
        private const string name = "TaskStates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class New
            {
                    public const int ID = 0;
                    public const string Name = "$Cards_TaskStates_New";
            }
            public static class InWork
            {
                    public const int ID = 1;
                    public const string Name = "$Cards_TaskStates_InWork";
            }
            public static class Postponed
            {
                    public const int ID = 2;
                    public const string Name = "$Cards_TaskStates_Postponed";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(TaskStatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region TaskStates Enumeration

    public sealed class TaskStates
    {
        public readonly int ID;
        public readonly string Name;

        public TaskStates(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region TemplateEditRoles

    /// <summary>
    ///     ID: {df057f7e-f59a-4857-b615-19abb650442a}
    ///     Alias: TemplateEditRoles
    ///     Group: System
    ///     Description: Роли, которым шаблон доступен для редактирования и удаления помимо администраторов.
    ///                  Указанным ролям автоматически доступно создание шаблона или создание карточки из шаблона.
    /// </summary>
    public sealed class TemplateEditRolesSchemeInfo
    {
        private const string name = "TemplateEditRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(TemplateEditRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TemplateFiles

    /// <summary>
    ///     ID: {fb7c3c18-d7cd-4f16-84e2-33ab0269adbd}
    ///     Alias: TemplateFiles
    ///     Group: System
    ///     Description: Файлы в шаблоне карточек.
    /// </summary>
    public sealed class TemplateFilesSchemeInfo
    {
        private const string name = "TemplateFiles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string SourceFileID = nameof(SourceFileID);
        public readonly string SourceVersionRowID = nameof(SourceVersionRowID);

        #endregion

        #region ToString

        public static implicit operator string(TemplateFilesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TemplateOpenRoles

    /// <summary>
    ///     ID: {831ff542-f2b2-4a3e-9295-0695b843567c}
    ///     Alias: TemplateOpenRoles
    ///     Group: System
    ///     Description: Роли, которым доступен просмотр шаблона и создание карточки из шаблона помимо администраторов.
    /// </summary>
    public sealed class TemplateOpenRolesSchemeInfo
    {
        private const string name = "TemplateOpenRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(TemplateOpenRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Templates

    /// <summary>
    ///     ID: {9f15aaf8-032c-4222-9c7c-2cfffeee89ed}
    ///     Alias: Templates
    ///     Group: System
    ///     Description: Шаблоны карточек.
    /// </summary>
    public sealed class TemplatesSchemeInfo
    {
        private const string name = "Templates";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Digest = nameof(Digest);
        public readonly string Description = nameof(Description);
        public readonly string Definition = nameof(Definition);
        public readonly string Card = nameof(Card);
        public readonly string CardID = nameof(CardID);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string Version = nameof(Version);
        public readonly string Caption = nameof(Caption);

        #endregion

        #region ToString

        public static implicit operator string(TemplatesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TemplatesVirtual

    /// <summary>
    ///     ID: {b6509d11-f1b3-4f54-b34f-4a996f66c71c}
    ///     Alias: TemplatesVirtual
    ///     Group: System
    ///     Description: Информация по шаблонам карточек, подготовленная для вывода в UI.
    /// </summary>
    public sealed class TemplatesVirtualSchemeInfo
    {
        private const string name = "TemplatesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CardID = nameof(CardID);
        public readonly string CardDigest = nameof(CardDigest);

        #endregion

        #region ToString

        public static implicit operator string(TemplatesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TEST_CarAdditionalInfo

    /// <summary>
    ///     ID: {9cbc0f98-571a-4822-a290-3e36b2f2f2e6}
    ///     Alias: TEST_CarAdditionalInfo
    ///     Group: Test
    /// </summary>
    public sealed class TEST_CarAdditionalInfoSchemeInfo
    {
        private const string name = "TEST_CarAdditionalInfo";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Color = nameof(Color);
        public readonly string IsBaseColor = nameof(IsBaseColor);

        #endregion

        #region ToString

        public static implicit operator string(TEST_CarAdditionalInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TEST_CarCustomers

    /// <summary>
    ///     ID: {30295c44-c633-4474-9e30-4492e75e7e75}
    ///     Alias: TEST_CarCustomers
    ///     Group: Test
    /// </summary>
    public sealed class TEST_CarCustomersSchemeInfo
    {
        private const string name = "TEST_CarCustomers";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string FullName = nameof(FullName);
        public readonly string PurchaseDate = nameof(PurchaseDate);
        public readonly string SaleRowID = nameof(SaleRowID);

        #endregion

        #region ToString

        public static implicit operator string(TEST_CarCustomersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TEST_CarMainInfo

    /// <summary>
    ///     ID: {509d961f-00cf-4403-a78f-6736841de448}
    ///     Alias: TEST_CarMainInfo
    ///     Group: Test
    /// </summary>
    public sealed class TEST_CarMainInfoSchemeInfo
    {
        private const string name = "TEST_CarMainInfo";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string MaxSpeed = nameof(MaxSpeed);
        public readonly string Running = nameof(Running);
        public readonly string Cost = nameof(Cost);
        public readonly string ReleaseDate = nameof(ReleaseDate);
        public readonly string DriverID = nameof(DriverID);
        public readonly string DriverName = nameof(DriverName);
        public readonly string Documentation = nameof(Documentation);
        public readonly string Xml = nameof(Xml);
        public readonly string Json = nameof(Json);
        public readonly string Jsonb = nameof(Jsonb);
        public readonly string Binary = nameof(Binary);
        public readonly string NullableGuid = nameof(NullableGuid);

        #endregion

        #region ToString

        public static implicit operator string(TEST_CarMainInfoSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TEST_CarOwners

    /// <summary>
    ///     ID: {2cff79c9-6e5a-4e98-8c8f-7a14eb7bec80}
    ///     Alias: TEST_CarOwners
    ///     Group: Test
    /// </summary>
    public sealed class TEST_CarOwnersSchemeInfo
    {
        private const string name = "TEST_CarOwners";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);

        #endregion

        #region ToString

        public static implicit operator string(TEST_CarOwnersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TEST_CarSales

    /// <summary>
    ///     ID: {6dc3a829-b1f4-4e67-ba99-16a30fe91209}
    ///     Alias: TEST_CarSales
    ///     Group: Test
    /// </summary>
    public sealed class TEST_CarSalesSchemeInfo
    {
        private const string name = "TEST_CarSales";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);
        public readonly string EndDate = nameof(EndDate);
        public readonly string ManagerID = nameof(ManagerID);
        public readonly string ManagerName = nameof(ManagerName);
        public readonly string Used = nameof(Used);

        #endregion

        #region ToString

        public static implicit operator string(TEST_CarSalesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TEST_CustomerOperations

    /// <summary>
    ///     ID: {7f813c98-3331-46a9-8aa0-bab55a956246}
    ///     Alias: TEST_CustomerOperations
    ///     Group: Test
    /// </summary>
    public sealed class TEST_CustomerOperationsSchemeInfo
    {
        private const string name = "TEST_CustomerOperations";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string OperationName = nameof(OperationName);
        public readonly string ManagerName = nameof(ManagerName);
        public readonly string CustomerRowID = nameof(CustomerRowID);

        #endregion

        #region ToString

        public static implicit operator string(TEST_CustomerOperationsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TileSizes

    /// <summary>
    ///     ID: {9d1fb4ee-fa51-4926-8abb-c464ca91e450}
    ///     Alias: TileSizes
    ///     Group: System
    ///     Description: Размеры плиток
    /// </summary>
    public sealed class TileSizesSchemeInfo
    {
        private const string name = "TileSizes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Half
            {
                    public const int ID = 1;
                    public const string Name = "$Enum_TileSize_Half";
            }
            public static class Quarter
            {
                    public const int ID = 2;
                    public const string Name = "$Enum_TileSize_Quarter";
            }
            public static class Full
            {
                    public const int ID = 0;
                    public const string Name = "$Enum_TileSize_Full";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(TileSizesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region TileSizes Enumeration

    public sealed class TileSizes
    {
        public readonly int ID;
        public readonly string Name;

        public TileSizes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region TimeZones

    /// <summary>
    ///     ID: {984e22bf-78fc-4c69-b1a6-ca73341c36ea}
    ///     Alias: TimeZones
    ///     Group: System
    /// </summary>
    public sealed class TimeZonesSchemeInfo
    {
        private const string name = "TimeZones";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string CodeName = nameof(CodeName);
        public readonly string UtcOffsetMinutes = nameof(UtcOffsetMinutes);
        public readonly string DisplayName = nameof(DisplayName);
        public readonly string ShortName = nameof(ShortName);
        public readonly string IsNegativeOffsetDirection = nameof(IsNegativeOffsetDirection);
        public readonly string OffsetTime = nameof(OffsetTime);

        #endregion

        #region ToString

        public static implicit operator string(TimeZonesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TimeZonesSettings

    /// <summary>
    ///     ID: {44e8b6f2-f7d1-48ff-a3f2-599bf76e5180}
    ///     Alias: TimeZonesSettings
    ///     Group: System
    /// </summary>
    public sealed class TimeZonesSettingsSchemeInfo
    {
        private const string name = "TimeZonesSettings";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string AllowToModify = nameof(AllowToModify);

        #endregion

        #region ToString

        public static implicit operator string(TimeZonesSettingsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region TimeZonesVirtual

    /// <summary>
    ///     ID: {3e09239e-ebb7-4b0a-a4e1-51eae83e3c0c}
    ///     Alias: TimeZonesVirtual
    ///     Group: System
    ///     Description: Временные зоны
    /// </summary>
    public sealed class TimeZonesVirtualSchemeInfo
    {
        private const string name = "TimeZonesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string CodeName = nameof(CodeName);
        public readonly string UtcOffsetMinutes = nameof(UtcOffsetMinutes);
        public readonly string IsNegativeOffsetDirection = nameof(IsNegativeOffsetDirection);
        public readonly string OffsetTime = nameof(OffsetTime);
        public readonly string DisplayName = nameof(DisplayName);
        public readonly string ShortName = nameof(ShortName);
        public readonly string ZoneID = nameof(ZoneID);

        #endregion

        #region ToString

        public static implicit operator string(TimeZonesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Types

    /// <summary>
    ///     ID: {b0538ece-8468-4d0b-8b4e-5a1d43e024db}
    ///     Alias: Types
    ///     Group: System
    ///     Description: Contains metadata that describes types which used by Tessa.
    /// </summary>
    public sealed class TypesSchemeInfo
    {
        private const string name = "Types";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);
        public readonly string Group = nameof(Group);
        public readonly string InstanceTypeID = nameof(InstanceTypeID);
        public readonly string Flags = nameof(Flags);
        public readonly string Metadata = nameof(Metadata);

        #endregion

        #region ToString

        public static implicit operator string(TypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region UserSettingsFunctionRolesVirtual

    /// <summary>
    ///     ID: {0e7d4c80-0a90-40a6-86a7-01ec32c80ba9}
    ///     Alias: UserSettingsFunctionRolesVirtual
    ///     Group: System
    ///     Description: Таблица с настройками сотрудника, предоставляемыми системой для функциональных ролей заданий.
    ///                  Такие настройки изменяются в метаинформации динамически, в зависимости от строк в таблице FunctionRoles.
    /// </summary>
    public sealed class UserSettingsFunctionRolesVirtualSchemeInfo
    {
        private const string name = "UserSettingsFunctionRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);

        #endregion

        #region ToString

        public static implicit operator string(UserSettingsFunctionRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region UserSettingsVirtual

    /// <summary>
    ///     ID: {3c8a5e77-c4da-45f5-b974-170af387ce26}
    ///     Alias: UserSettingsVirtual
    ///     Group: System
    ///     Description: Таблица с настройками сотрудника, предоставляемыми системой.
    /// </summary>
    public sealed class UserSettingsVirtualSchemeInfo
    {
        private const string name = "UserSettingsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string LeftPanelOpenOnClick = nameof(LeftPanelOpenOnClick);
        public readonly string LeftPanelTopAreaOpenOnClick = nameof(LeftPanelTopAreaOpenOnClick);
        public readonly string LeftPanelBottomAreaOpenOnClick = nameof(LeftPanelBottomAreaOpenOnClick);
        public readonly string RightPanelOpenOnClick = nameof(RightPanelOpenOnClick);
        public readonly string RightPanelTopAreaOpenOnClick = nameof(RightPanelTopAreaOpenOnClick);
        public readonly string RightPanelBottomAreaOpenOnClick = nameof(RightPanelBottomAreaOpenOnClick);
        public readonly string DisableWindowFading = nameof(DisableWindowFading);
        public readonly string DisablePdfEmbeddedPreview = nameof(DisablePdfEmbeddedPreview);
        public readonly string PreferPdfPagingPreview = nameof(PreferPdfPagingPreview);
        public readonly string DisablePopupNotifications = nameof(DisablePopupNotifications);
        public readonly string WebLeftPanelOpenOnClick = nameof(WebLeftPanelOpenOnClick);
        public readonly string WebRightPanelOpenOnClick = nameof(WebRightPanelOpenOnClick);
        public readonly string AllowMultipleExternalPreview = nameof(AllowMultipleExternalPreview);
        public readonly string TaskColor = nameof(TaskColor);
        public readonly string TopicItemColor = nameof(TopicItemColor);
        public readonly string FrequentlyUsedEmoji = nameof(FrequentlyUsedEmoji);

        #endregion

        #region ToString

        public static implicit operator string(UserSettingsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region VatTypes

    /// <summary>
    ///     ID: {8dd87520-9d83-4d8a-8c60-c1275328c5e8}
    ///     Alias: VatTypes
    ///     Group: Common
    /// </summary>
    public sealed class VatTypesSchemeInfo
    {
        private const string name = "VatTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class WithVAT
            {
                    public const int ID = 0;
                    public const string Name = "$VatType_WithVAT";
            }
            public static class ExemptFromVAT
            {
                    public const int ID = 1;
                    public const string Name = "$VatType_ExemptFromVAT";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(VatTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region VatTypes Enumeration

    public sealed class VatTypes
    {
        public readonly int ID;
        public readonly string Name;

        public VatTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region ViewRoles

    /// <summary>
    ///     ID: {5a5dc5fe-19e1-4c69-b084-d6db36aa5a23}
    ///     Alias: ViewRoles
    ///     Group: System
    /// </summary>
    public sealed class ViewRolesSchemeInfo
    {
        private const string name = "ViewRoles";

        #region Columns

        public readonly string ViewID = nameof(ViewID);
        public readonly string RoleID = nameof(RoleID);

        #endregion

        #region ToString

        public static implicit operator string(ViewRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ViewRolesVirtual

    /// <summary>
    ///     ID: {08fccef5-fe25-4f3b-9a8c-2291b6a60209}
    ///     Alias: ViewRolesVirtual
    ///     Group: System
    ///     Description: Список ролей для представлений, отображаемых как виртуальные карточки в клиенте.
    /// </summary>
    public sealed class ViewRolesVirtualSchemeInfo
    {
        private const string name = "ViewRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(ViewRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Views

    /// <summary>
    ///     ID: {3519b63c-eea0-48f4-b70a-544e58ece5fc}
    ///     Alias: Views
    ///     Group: System
    ///     Description: Представления.
    /// </summary>
    public sealed class ViewsSchemeInfo
    {
        private const string name = "Views";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Alias = nameof(Alias);
        public readonly string Caption = nameof(Caption);
        public readonly string ModifiedDateTime = nameof(ModifiedDateTime);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);
        public readonly string MetadataSource = nameof(MetadataSource);
        public readonly string MsQuerySource = nameof(MsQuerySource);
        public readonly string Description = nameof(Description);
        public readonly string GroupName = nameof(GroupName);
        public readonly string PgQuerySource = nameof(PgQuerySource);
        public readonly string JsonMetadataSource = nameof(JsonMetadataSource);

        #endregion

        #region ToString

        public static implicit operator string(ViewsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region ViewsVirtual

    /// <summary>
    ///     ID: {cefba5f8-8b2c-4be0-ba24-564f3a474240}
    ///     Alias: ViewsVirtual
    ///     Group: System
    ///     Description: Представления, отображаемые как виртуальные карточки в клиенте.
    /// </summary>
    public sealed class ViewsVirtualSchemeInfo
    {
        private const string name = "ViewsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Alias = nameof(Alias);
        public readonly string Caption = nameof(Caption);
        public readonly string GroupName = nameof(GroupName);
        public readonly string Description = nameof(Description);
        public readonly string Modified = nameof(Modified);
        public readonly string ModifiedByID = nameof(ModifiedByID);
        public readonly string ModifiedByName = nameof(ModifiedByName);

        #endregion

        #region ToString

        public static implicit operator string(ViewsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeAddFileFromTemplateAction

    /// <summary>
    ///     ID: {93d11813-4967-458e-b3be-f7da367a8872}
    ///     Alias: WeAddFileFromTemplateAction
    ///     Group: WorkflowEngine
    ///     Description: Основная секция для действия Добавить файл по шаблону
    /// </summary>
    public sealed class WeAddFileFromTemplateActionSchemeInfo
    {
        private const string name = "WeAddFileFromTemplateAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string FileTemplateID = nameof(FileTemplateID);
        public readonly string FileTemplateName = nameof(FileTemplateName);

        #endregion

        #region ToString

        public static implicit operator string(WeAddFileFromTemplateActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WebApplications

    /// <summary>
    ///     ID: {610d8253-e293-4676-abcb-e7a0ac1a084d}
    ///     Alias: WebApplications
    ///     Group: System
    ///     Description: Карточки приложений-ассистентов web-клиента, таких как Deski
    /// </summary>
    public sealed class WebApplicationsSchemeInfo
    {
        private const string name = "WebApplications";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string AppVersion = nameof(AppVersion);
        public readonly string PlatformVersion = nameof(PlatformVersion);
        public readonly string Description = nameof(Description);
        public readonly string ExecutableFileName = nameof(ExecutableFileName);
        public readonly string LanguageID = nameof(LanguageID);
        public readonly string LanguageCaption = nameof(LanguageCaption);
        public readonly string LanguageCode = nameof(LanguageCode);
        public readonly string OSName = nameof(OSName);
        public readonly string Client64Bit = nameof(Client64Bit);

        #endregion

        #region ToString

        public static implicit operator string(WebApplicationsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WebClientRoles

    /// <summary>
    ///     ID: {383321f7-c432-42d8-84f9-e4f58e0cb021}
    ///     Alias: WebClientRoles
    ///     Group: System
    ///     Description: Роли, в одну из которых должен входить сотрудник для авторизации в web-клиенте.
    /// </summary>
    public sealed class WebClientRolesSchemeInfo
    {
        private const string name = "WebClientRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(WebClientRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeCommandAction

    /// <summary>
    ///     ID: {2dc38a34-3451-4ea9-9885-9afa15155612}
    ///     Alias: WeCommandAction
    ///     Group: WorkflowEngine
    ///     Description: Секция для действия Подписка на команду
    /// </summary>
    public sealed class WeCommandActionSchemeInfo
    {
        private const string name = "WeCommandAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ReusableSubscription = nameof(ReusableSubscription);
        public readonly string CommandID = nameof(CommandID);
        public readonly string CommandName = nameof(CommandName);

        #endregion

        #region ToString

        public static implicit operator string(WeCommandActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeCommandActionLinks

    /// <summary>
    ///     ID: {97e973ba-8fa9-4e3d-96d3-2f077ca11531}
    ///     Alias: WeCommandActionLinks
    ///     Group: WorkflowEngine
    ///     Description: Секция определяет список переходов, которые должны быть вызваны после получения команды
    /// </summary>
    public sealed class WeCommandActionLinksSchemeInfo
    {
        private const string name = "WeCommandActionLinks";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string LinkID = nameof(LinkID);
        public readonly string LinkName = nameof(LinkName);
        public readonly string LinkCaption = nameof(LinkCaption);

        #endregion

        #region ToString

        public static implicit operator string(WeCommandActionLinksSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeConditionAction

    /// <summary>
    ///     ID: {ad4abe1f-9f6b-4842-b8d5-bb34502c7dce}
    ///     Alias: WeConditionAction
    ///     Group: WorkflowEngine
    ///     Description: Основная секция действия Условия
    /// </summary>
    public sealed class WeConditionActionSchemeInfo
    {
        private const string name = "WeConditionAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Condition = nameof(Condition);
        public readonly string IsElse = nameof(IsElse);
        public readonly string LinkID = nameof(LinkID);
        public readonly string LinkName = nameof(LinkName);
        public readonly string LinkCaption = nameof(LinkCaption);
        public readonly string Description = nameof(Description);
        public readonly string TypeOfConditionCheck = nameof(TypeOfConditionCheck);

        #endregion

        #region ToString

        public static implicit operator string(WeConditionActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeDialogAction

    /// <summary>
    ///     ID: {5aac25fd-de4f-450d-9fd5-a1a9168a795c}
    ///     Alias: WeDialogAction
    ///     Group: WorkflowEngine
    ///     Description: Основная секция действия Диалог
    /// </summary>
    public sealed class WeDialogActionSchemeInfo
    {
        private const string name = "WeDialogAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string DialogTypeID = nameof(DialogTypeID);
        public readonly string DialogTypeName = nameof(DialogTypeName);
        public readonly string DialogTypeCaption = nameof(DialogTypeCaption);
        public readonly string CardStoreModeID = nameof(CardStoreModeID);
        public readonly string CardStoreModeName = nameof(CardStoreModeName);
        public readonly string ButtonName = nameof(ButtonName);
        public readonly string DialogName = nameof(DialogName);
        public readonly string DialogAlias = nameof(DialogAlias);
        public readonly string OpenModeID = nameof(OpenModeID);
        public readonly string OpenModeName = nameof(OpenModeName);
        public readonly string TaskDigest = nameof(TaskDigest);
        public readonly string SavingScript = nameof(SavingScript);
        public readonly string ActionScript = nameof(ActionScript);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string TaskKindID = nameof(TaskKindID);
        public readonly string TaskKindCaption = nameof(TaskKindCaption);
        public readonly string Planned = nameof(Planned);
        public readonly string Period = nameof(Period);
        public readonly string InitScript = nameof(InitScript);
        public readonly string DisplayValue = nameof(DisplayValue);
        public readonly string KeepFiles = nameof(KeepFiles);
        public readonly string WithoutTask = nameof(WithoutTask);
        public readonly string TemplateID = nameof(TemplateID);
        public readonly string TemplateCaption = nameof(TemplateCaption);
        public readonly string IsCloseWithoutConfirmation = nameof(IsCloseWithoutConfirmation);

        #endregion

        #region ToString

        public static implicit operator string(WeDialogActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeDialogActionButtonLinks

    /// <summary>
    ///     ID: {57f61e17-bd87-48cb-8efc-7a7dc56f2eef}
    ///     Alias: WeDialogActionButtonLinks
    ///     Group: WorkflowEngine
    /// </summary>
    public sealed class WeDialogActionButtonLinksSchemeInfo
    {
        private const string name = "WeDialogActionButtonLinks";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ButtonRowID = nameof(ButtonRowID);
        public readonly string LinkID = nameof(LinkID);
        public readonly string LinkName = nameof(LinkName);
        public readonly string LinkCaption = nameof(LinkCaption);

        #endregion

        #region ToString

        public static implicit operator string(WeDialogActionButtonLinksSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeDialogActionButtons

    /// <summary>
    ///     ID: {a99b285f-80c3-442a-85a6-2a3bfd645d2b}
    ///     Alias: WeDialogActionButtons
    ///     Group: WorkflowEngine
    ///     Description: Секция с настройками кнопок для действия Диалог
    /// </summary>
    public sealed class WeDialogActionButtonsSchemeInfo
    {
        private const string name = "WeDialogActionButtons";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeName = nameof(TypeName);
        public readonly string Caption = nameof(Caption);
        public readonly string Icon = nameof(Icon);
        public readonly string Cancel = nameof(Cancel);
        public readonly string Order = nameof(Order);
        public readonly string Script = nameof(Script);
        public readonly string NotEnd = nameof(NotEnd);
        public readonly string TaskDialogRowID = nameof(TaskDialogRowID);

        #endregion

        #region ToString

        public static implicit operator string(WeDialogActionButtonsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeEmailAction

    /// <summary>
    ///     ID: {3482fa35-9558-4a7c-832f-3ac94c73f2f9}
    ///     Alias: WeEmailAction
    ///     Group: WorkflowEngine
    ///     Description: Секция для действия отправки уведомления
    /// </summary>
    public sealed class WeEmailActionSchemeInfo
    {
        private const string name = "WeEmailAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Body = nameof(Body);
        public readonly string Header = nameof(Header);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string Script = nameof(Script);
        public readonly string NotificationTypeID = nameof(NotificationTypeID);
        public readonly string NotificationTypeName = nameof(NotificationTypeName);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);

        #endregion

        #region ToString

        public static implicit operator string(WeEmailActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeEmailActionOptionalRecipients

    /// <summary>
    ///     ID: {94b08bf8-6cb2-4a11-a42b-4ff996ac71e5}
    ///     Alias: WeEmailActionOptionalRecipients
    ///     Group: WorkflowEngine
    ///     Description: Список опциональных получателей письма
    /// </summary>
    public sealed class WeEmailActionOptionalRecipientsSchemeInfo
    {
        private const string name = "WeEmailActionOptionalRecipients";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(WeEmailActionOptionalRecipientsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeEmailActionRecievers

    /// <summary>
    ///     ID: {48d261cd-1054-41d7-9046-485e22d15060}
    ///     Alias: WeEmailActionRecievers
    ///     Group: WorkflowEngine
    ///     Description: Список получателей письма
    /// </summary>
    public sealed class WeEmailActionRecieversSchemeInfo
    {
        private const string name = "WeEmailActionRecievers";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(WeEmailActionRecieversSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeEndAction

    /// <summary>
    ///     ID: {e36e23ae-2276-494a-a3f1-5f3cd5c56f9d}
    ///     Alias: WeEndAction
    ///     Group: WorkflowEngine
    ///     Description: Секция для действия Конец процесса
    /// </summary>
    public sealed class WeEndActionSchemeInfo
    {
        private const string name = "WeEndAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string FinishProcess = nameof(FinishProcess);
        public readonly string EndSignalID = nameof(EndSignalID);
        public readonly string EndSignalName = nameof(EndSignalName);

        #endregion

        #region ToString

        public static implicit operator string(WeEndActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeHistoryManagementAction

    /// <summary>
    ///     ID: {bb018cba-ef03-4bb4-a7e6-8fb083fc44a4}
    ///     Alias: WeHistoryManagementAction
    ///     Group: WorkflowEngine
    /// </summary>
    public sealed class WeHistoryManagementActionSchemeInfo
    {
        private const string name = "WeHistoryManagementAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string TaskHistoryGroupTypeID = nameof(TaskHistoryGroupTypeID);
        public readonly string TaskHistoryGroupTypeCaption = nameof(TaskHistoryGroupTypeCaption);
        public readonly string ParentTaskHistoryGroupTypeID = nameof(ParentTaskHistoryGroupTypeID);
        public readonly string ParentTaskHistoryGroupTypeCaption = nameof(ParentTaskHistoryGroupTypeCaption);
        public readonly string NewIteration = nameof(NewIteration);

        #endregion

        #region ToString

        public static implicit operator string(WeHistoryManagementActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeScriptAction

    /// <summary>
    ///     ID: {46f88520-33d0-45c9-bd27-20cae8fa58dc}
    ///     Alias: WeScriptAction
    ///     Group: WorkflowEngine
    ///     Description: Секция для действия Скрипт
    /// </summary>
    public sealed class WeScriptActionSchemeInfo
    {
        private const string name = "WeScriptAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Script = nameof(Script);
        public readonly string ProcessAnySignal = nameof(ProcessAnySignal);

        #endregion

        #region ToString

        public static implicit operator string(WeScriptActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeSendSignalAction

    /// <summary>
    ///     ID: {fbe60ad7-091a-4f09-a57a-f1068088fa38}
    ///     Alias: WeSendSignalAction
    ///     Group: WorkflowEngine
    ///     Description: Основная секция для действия Отправка сигнала
    /// </summary>
    public sealed class WeSendSignalActionSchemeInfo
    {
        private const string name = "WeSendSignalAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string SignalID = nameof(SignalID);
        public readonly string SignalName = nameof(SignalName);
        public readonly string PassHash = nameof(PassHash);
        public readonly string Scenario = nameof(Scenario);

        #endregion

        #region ToString

        public static implicit operator string(WeSendSignalActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeStartAction

    /// <summary>
    ///     ID: {fff6d6ad-c17e-4692-863d-07032f4b95fd}
    ///     Alias: WeStartAction
    ///     Group: WorkflowEngine
    ///     Description: Секция для действия Старта процесса
    /// </summary>
    public sealed class WeStartActionSchemeInfo
    {
        private const string name = "WeStartAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string StartSignalID = nameof(StartSignalID);
        public readonly string StartSignalName = nameof(StartSignalName);
        public readonly string IsNotPersistent = nameof(IsNotPersistent);

        #endregion

        #region ToString

        public static implicit operator string(WeStartActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeSubprocessAction

    /// <summary>
    ///     ID: {3d947708-6196-443f-a4e3-a1e1a5315d9d}
    ///     Alias: WeSubprocessAction
    ///     Group: WorkflowEngine
    ///     Description: Секция с данными действия Подпроцесс
    /// </summary>
    public sealed class WeSubprocessActionSchemeInfo
    {
        private const string name = "WeSubprocessAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string StartSignalID = nameof(StartSignalID);
        public readonly string StartSignalName = nameof(StartSignalName);
        public readonly string ProcessID = nameof(ProcessID);
        public readonly string ProcessName = nameof(ProcessName);

        #endregion

        #region ToString

        public static implicit operator string(WeSubprocessActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeSubprocessActionEndMapping

    /// <summary>
    ///     ID: {ea4cd339-7a97-4221-a223-44f9b6ce6ce1}
    ///     Alias: WeSubprocessActionEndMapping
    ///     Group: WorkflowEngine
    /// </summary>
    public sealed class WeSubprocessActionEndMappingSchemeInfo
    {
        private const string name = "WeSubprocessActionEndMapping";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string SourceParamID = nameof(SourceParamID);
        public readonly string SourceParamText = nameof(SourceParamText);
        public readonly string TargetParamID = nameof(TargetParamID);
        public readonly string TargetParamText = nameof(TargetParamText);

        #endregion

        #region ToString

        public static implicit operator string(WeSubprocessActionEndMappingSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeSubprocessActionOptions

    /// <summary>
    ///     ID: {428f3b30-561c-446e-b676-4ec84ba8e03a}
    ///     Alias: WeSubprocessActionOptions
    ///     Group: WorkflowEngine
    ///     Description: Секция с настройками переходов при получении сигналов из под-процесса
    /// </summary>
    public sealed class WeSubprocessActionOptionsSchemeInfo
    {
        private const string name = "WeSubprocessActionOptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string SignalID = nameof(SignalID);
        public readonly string SignalName = nameof(SignalName);
        public readonly string LinkID = nameof(LinkID);
        public readonly string LinkName = nameof(LinkName);
        public readonly string LinkCaption = nameof(LinkCaption);
        public readonly string Script = nameof(Script);

        #endregion

        #region ToString

        public static implicit operator string(WeSubprocessActionOptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeSubprocessActionStartMapping

    /// <summary>
    ///     ID: {a2b54bf4-20ae-4fdd-8b2e-21ef246cfb32}
    ///     Alias: WeSubprocessActionStartMapping
    ///     Group: WorkflowEngine
    ///     Description: Маппинг параметров процесса, передаваемых в параметры подпроцесса
    /// </summary>
    public sealed class WeSubprocessActionStartMappingSchemeInfo
    {
        private const string name = "WeSubprocessActionStartMapping";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string SourceParamID = nameof(SourceParamID);
        public readonly string SourceParamText = nameof(SourceParamText);
        public readonly string TargetParamID = nameof(TargetParamID);
        public readonly string TargetParamText = nameof(TargetParamText);

        #endregion

        #region ToString

        public static implicit operator string(WeSubprocessActionStartMappingSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeSubprocessControlAction

    /// <summary>
    ///     ID: {2f0a4a5d-6601-4cd9-9c2d-09d193d33352}
    ///     Alias: WeSubprocessControlAction
    ///     Group: WorkflowEngine
    ///     Description: Секция для действия Управление подпроцессом
    /// </summary>
    public sealed class WeSubprocessControlActionSchemeInfo
    {
        private const string name = "WeSubprocessControlAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string SignalID = nameof(SignalID);
        public readonly string SignalName = nameof(SignalName);

        #endregion

        #region ToString

        public static implicit operator string(WeSubprocessControlActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTaskAction

    /// <summary>
    ///     ID: {ffcaed62-a85f-43b0-b029-ed50bc562ef1}
    ///     Alias: WeTaskAction
    ///     Group: WorkflowEngine
    ///     Description: Секция для действия Задание
    /// </summary>
    public sealed class WeTaskActionSchemeInfo
    {
        private const string name = "WeTaskAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Digest = nameof(Digest);
        public readonly string TaskTypeID = nameof(TaskTypeID);
        public readonly string TaskTypeCaption = nameof(TaskTypeCaption);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string Period = nameof(Period);
        public readonly string Planned = nameof(Planned);
        public readonly string Result = nameof(Result);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string InitTaskScript = nameof(InitTaskScript);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);

        #endregion

        #region ToString

        public static implicit operator string(WeTaskActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTaskActionDialogs

    /// <summary>
    ///     ID: {7c068441-e9e1-445a-a371-bf9436156428}
    ///     Alias: WeTaskActionDialogs
    ///     Group: WorkflowEngine
    ///     Description: Секция с настройками диалогов
    /// </summary>
    public sealed class WeTaskActionDialogsSchemeInfo
    {
        private const string name = "WeTaskActionDialogs";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string DialogTypeID = nameof(DialogTypeID);
        public readonly string DialogTypeName = nameof(DialogTypeName);
        public readonly string DialogTypeCaption = nameof(DialogTypeCaption);
        public readonly string CardStoreModeID = nameof(CardStoreModeID);
        public readonly string CardStoreModeName = nameof(CardStoreModeName);
        public readonly string DialogName = nameof(DialogName);
        public readonly string DialogAlias = nameof(DialogAlias);
        public readonly string SavingScript = nameof(SavingScript);
        public readonly string ActionScript = nameof(ActionScript);
        public readonly string InitScript = nameof(InitScript);
        public readonly string CompletionOptionID = nameof(CompletionOptionID);
        public readonly string CompletionOptionCaption = nameof(CompletionOptionCaption);
        public readonly string Order = nameof(Order);
        public readonly string DisplayValue = nameof(DisplayValue);
        public readonly string KeepFiles = nameof(KeepFiles);
        public readonly string TemplateID = nameof(TemplateID);
        public readonly string TemplateCaption = nameof(TemplateCaption);

        #endregion

        #region ToString

        public static implicit operator string(WeTaskActionDialogsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTaskActionEvents

    /// <summary>
    ///     ID: {797022e2-bac4-408c-b529-110943fade63}
    ///     Alias: WeTaskActionEvents
    ///     Group: WorkflowEngine
    ///     Description: Секция с обработчиками событий заданий
    /// </summary>
    public sealed class WeTaskActionEventsSchemeInfo
    {
        private const string name = "WeTaskActionEvents";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Script = nameof(Script);
        public readonly string EventID = nameof(EventID);
        public readonly string EventName = nameof(EventName);

        #endregion

        #region ToString

        public static implicit operator string(WeTaskActionEventsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTaskActionNotificationRoles

    /// <summary>
    ///     ID: {9b7fc0b0-da06-46df-a5c9-d66ecc386d55}
    ///     Alias: WeTaskActionNotificationRoles
    ///     Group: WorkflowEngine
    /// </summary>
    public sealed class WeTaskActionNotificationRolesSchemeInfo
    {
        private const string name = "WeTaskActionNotificationRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string TaskOptionRowID = nameof(TaskOptionRowID);
        public readonly string TaskGroupOptionRowID = nameof(TaskGroupOptionRowID);

        #endregion

        #region ToString

        public static implicit operator string(WeTaskActionNotificationRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTaskActionOptionLinks

    /// <summary>
    ///     ID: {a3d3bf40-b37a-4118-af51-2b555da511b7}
    ///     Alias: WeTaskActionOptionLinks
    ///     Group: WorkflowEngine
    /// </summary>
    public sealed class WeTaskActionOptionLinksSchemeInfo
    {
        private const string name = "WeTaskActionOptionLinks";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string LinkID = nameof(LinkID);
        public readonly string LinkName = nameof(LinkName);
        public readonly string LinkCaption = nameof(LinkCaption);
        public readonly string OptionRowID = nameof(OptionRowID);

        #endregion

        #region ToString

        public static implicit operator string(WeTaskActionOptionLinksSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTaskActionOptions

    /// <summary>
    ///     ID: {e30dcb0a-2a63-4f52-82f9-a12b0038d70d}
    ///     Alias: WeTaskActionOptions
    ///     Group: WorkflowEngine
    ///     Description: Таблица с вариантами завершения в действии задания
    /// </summary>
    public sealed class WeTaskActionOptionsSchemeInfo
    {
        private const string name = "WeTaskActionOptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);
        public readonly string LinkID = nameof(LinkID);
        public readonly string LinkName = nameof(LinkName);
        public readonly string LinkCaption = nameof(LinkCaption);
        public readonly string Script = nameof(Script);
        public readonly string Order = nameof(Order);
        public readonly string Result = nameof(Result);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string SendToPerformer = nameof(SendToPerformer);
        public readonly string SendToAuthor = nameof(SendToAuthor);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);

        #endregion

        #region ToString

        public static implicit operator string(WeTaskActionOptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTaskControlAction

    /// <summary>
    ///     ID: {adcf3458-d724-4411-9059-60bdb353a9b5}
    ///     Alias: WeTaskControlAction
    ///     Group: WorkflowEngine
    ///     Description: Секция для действия Управление заданием
    /// </summary>
    public sealed class WeTaskControlActionSchemeInfo
    {
        private const string name = "WeTaskControlAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Digest = nameof(Digest);
        public readonly string Planned = nameof(Planned);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);
        public readonly string ControlTypeName = nameof(ControlTypeName);
        public readonly string ControlTypeID = nameof(ControlTypeID);

        #endregion

        #region ToString

        public static implicit operator string(WeTaskControlActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTaskControlTypes

    /// <summary>
    ///     ID: {ab612473-e0a2-4dd7-b05e-d9bbdf06b62f}
    ///     Alias: WeTaskControlTypes
    ///     Group: WorkflowEngine
    ///     Description: Список доступных манипуляций над заданием из действия Управление заданием
    /// </summary>
    public sealed class WeTaskControlTypesSchemeInfo
    {
        private const string name = "WeTaskControlTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class DeleteTask
            {
                    public const int ID = 0;
                    public const string Name = "$WorkflowEngine_TaskControlTypes_DeleteTask";
            }
            public static class UpdateTask
            {
                    public const int ID = 1;
                    public const string Name = "$WorkflowEngine_TaskControlTypes_UpdateTask";
            }
            public static class CompleteTask
            {
                    public const int ID = 2;
                    public const string Name = "$WorkflowEngine_TaskControlTypes_CompleteTask";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(WeTaskControlTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region WeTaskControlTypes Enumeration

    public sealed class WeTaskControlTypes
    {
        public readonly int ID;
        public readonly string Name;

        public WeTaskControlTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region WeTaskGroupAction

    /// <summary>
    ///     ID: {915d8549-af3d-4d44-84a1-cef16ed89941}
    ///     Alias: WeTaskGroupAction
    ///     Group: WorkflowEngine
    ///     Description: Основная секция для действия Группа заданий
    /// </summary>
    public sealed class WeTaskGroupActionSchemeInfo
    {
        private const string name = "WeTaskGroupAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Digest = nameof(Digest);
        public readonly string Period = nameof(Period);
        public readonly string Planned = nameof(Planned);
        public readonly string Result = nameof(Result);
        public readonly string Parallel = nameof(Parallel);
        public readonly string TaskTypeID = nameof(TaskTypeID);
        public readonly string TaskTypeCaption = nameof(TaskTypeCaption);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string InitTaskScript = nameof(InitTaskScript);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);

        #endregion

        #region ToString

        public static implicit operator string(WeTaskGroupActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTaskGroupActionOptionLinks

    /// <summary>
    ///     ID: {1e26efb8-a6ee-4582-9ac3-88da4ef74d24}
    ///     Alias: WeTaskGroupActionOptionLinks
    ///     Group: WorkflowEngine
    /// </summary>
    public sealed class WeTaskGroupActionOptionLinksSchemeInfo
    {
        private const string name = "WeTaskGroupActionOptionLinks";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string LinkID = nameof(LinkID);
        public readonly string LinkName = nameof(LinkName);
        public readonly string LinkCaption = nameof(LinkCaption);
        public readonly string OptionRowID = nameof(OptionRowID);

        #endregion

        #region ToString

        public static implicit operator string(WeTaskGroupActionOptionLinksSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTaskGroupActionOptions

    /// <summary>
    ///     ID: {dee05376-8267-42b9-8cc9-1ff5bb58bb06}
    ///     Alias: WeTaskGroupActionOptions
    ///     Group: WorkflowEngine
    ///     Description: Секция с настройками вариантов завершения заданий
    /// </summary>
    public sealed class WeTaskGroupActionOptionsSchemeInfo
    {
        private const string name = "WeTaskGroupActionOptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);
        public readonly string LinkID = nameof(LinkID);
        public readonly string LinkName = nameof(LinkName);
        public readonly string LinkCaption = nameof(LinkCaption);
        public readonly string Script = nameof(Script);
        public readonly string CancelGroup = nameof(CancelGroup);
        public readonly string OptionTypeName = nameof(OptionTypeName);
        public readonly string OptionTypeID = nameof(OptionTypeID);
        public readonly string Order = nameof(Order);
        public readonly string PauseGroup = nameof(PauseGroup);
        public readonly string CancelOptionID = nameof(CancelOptionID);
        public readonly string CancelOptionCaption = nameof(CancelOptionCaption);
        public readonly string NewRoleID = nameof(NewRoleID);
        public readonly string NewRoleName = nameof(NewRoleName);
        public readonly string UseAsNextRole = nameof(UseAsNextRole);
        public readonly string Result = nameof(Result);
        public readonly string NotificationID = nameof(NotificationID);
        public readonly string NotificationName = nameof(NotificationName);
        public readonly string SendToPerformer = nameof(SendToPerformer);
        public readonly string SendToAuthor = nameof(SendToAuthor);
        public readonly string ExcludeDeputies = nameof(ExcludeDeputies);
        public readonly string ExcludeSubscribers = nameof(ExcludeSubscribers);
        public readonly string NotificationScript = nameof(NotificationScript);

        #endregion

        #region ToString

        public static implicit operator string(WeTaskGroupActionOptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTaskGroupActionOptionTypes

    /// <summary>
    ///     ID: {dc9eb404-c42d-40ab-a4c0-3b8b6089b926}
    ///     Alias: WeTaskGroupActionOptionTypes
    ///     Group: WorkflowEngine
    ///     Description: Список допустимых условий выполнения перехода
    /// </summary>
    public sealed class WeTaskGroupActionOptionTypesSchemeInfo
    {
        private const string name = "WeTaskGroupActionOptionTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class OneNow
            {
                    public const int ID = 0;
                    public const string Name = "$WorkflowEngine_TaskGroupOptionTypes_OneNow";
            }
            public static class OneAfterAll
            {
                    public const int ID = 1;
                    public const string Name = "$WorkflowEngine_TaskGroupOptionTypes_OneAfterAll";
            }
            public static class All
            {
                    public const int ID = 2;
                    public const string Name = "$WorkflowEngine_TaskGroupOptionTypes_All";
            }
            public static class AfterFinish
            {
                    public const int ID = 3;
                    public const string Name = "$WorkflowEngine_TaskGroupOptionTypes_AfterFinish";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(WeTaskGroupActionOptionTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region WeTaskGroupActionOptionTypes Enumeration

    public sealed class WeTaskGroupActionOptionTypes
    {
        public readonly int ID;
        public readonly string Name;

        public WeTaskGroupActionOptionTypes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region WeTaskGroupActionRoles

    /// <summary>
    ///     ID: {0656f18d-bb1c-47c9-8d40-24300c7f4b53}
    ///     Alias: WeTaskGroupActionRoles
    ///     Group: WorkflowEngine
    ///     Description: Секция со списком ролей для действия Группа заданий
    /// </summary>
    public sealed class WeTaskGroupActionRolesSchemeInfo
    {
        private const string name = "WeTaskGroupActionRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(WeTaskGroupActionRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTaskGroupControlAction

    /// <summary>
    ///     ID: {02a2a16e-7915-4a03-86b0-08b074b78c67}
    ///     Alias: WeTaskGroupControlAction
    ///     Group: WorkflowEngine
    ///     Description: Основная секция для действия управление группой заданий
    /// </summary>
    public sealed class WeTaskGroupControlActionSchemeInfo
    {
        private const string name = "WeTaskGroupControlAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string ResumeGroup = nameof(ResumeGroup);
        public readonly string PauseGroup = nameof(PauseGroup);
        public readonly string CancelGroup = nameof(CancelGroup);
        public readonly string CancelOptionID = nameof(CancelOptionID);
        public readonly string CancelOptionCaption = nameof(CancelOptionCaption);
        public readonly string UseAsNextRole = nameof(UseAsNextRole);

        #endregion

        #region ToString

        public static implicit operator string(WeTaskGroupControlActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTimerAction

    /// <summary>
    ///     ID: {318965a6-fcec-432d-8ba3-3b972fb2b750}
    ///     Alias: WeTimerAction
    ///     Group: WorkflowEngine
    ///     Description: Секция для действия Таймер
    /// </summary>
    public sealed class WeTimerActionSchemeInfo
    {
        private const string name = "WeTimerAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RunOnce = nameof(RunOnce);
        public readonly string Period = nameof(Period);
        public readonly string Cron = nameof(Cron);
        public readonly string Date = nameof(Date);
        public readonly string StopCondition = nameof(StopCondition);

        #endregion

        #region ToString

        public static implicit operator string(WeTimerActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WeTimerControlAction

    /// <summary>
    ///     ID: {38c4dd25-e26e-469c-8072-6498f33a0d06}
    ///     Alias: WeTimerControlAction
    ///     Group: WorkflowEngine
    ///     Description: Секция для действия Управление таймером
    /// </summary>
    public sealed class WeTimerControlActionSchemeInfo
    {
        private const string name = "WeTimerControlAction";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Period = nameof(Period);
        public readonly string Cron = nameof(Cron);
        public readonly string Stop = nameof(Stop);
        public readonly string Date = nameof(Date);

        #endregion

        #region ToString

        public static implicit operator string(WeTimerControlActionSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WfResolutionChildren

    /// <summary>
    ///     ID: {d4f683a4-a1e9-4fc1-ae84-2c4ab304b7fb}
    ///     Alias: WfResolutionChildren
    ///     Group: Wf
    ///     Description: Записи для дочерних резолюций.
    ///                  Колонка RowID содержит идентификатор дочернего задания.
    ///                  Если поле IsCompleted = True, то дочерняя резолюция была завершена и задание больше не существует.
    /// </summary>
    public sealed class WfResolutionChildrenSchemeInfo
    {
        private const string name = "WfResolutionChildren";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string PerformerID = nameof(PerformerID);
        public readonly string PerformerName = nameof(PerformerName);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);
        public readonly string Comment = nameof(Comment);
        public readonly string Answer = nameof(Answer);
        public readonly string Created = nameof(Created);
        public readonly string Planned = nameof(Planned);
        public readonly string InProgress = nameof(InProgress);
        public readonly string Completed = nameof(Completed);

        #endregion

        #region ToString

        public static implicit operator string(WfResolutionChildrenSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WfResolutionChildrenVirtual

    /// <summary>
    ///     ID: {17dcbbe4-108a-4f15-8716-f7d2718f0953}
    ///     Alias: WfResolutionChildrenVirtual
    ///     Group: Wf
    ///     Description: Таблица с информацией по дочерним резолюциям. Используются в заданиях совместно с таблицей WfResolutions.
    ///                  Таблица является виртуальной и заполняется автоматически в расширениях.
    ///                  Колонка RowID содержит идентификатор дочернего задания.
    /// </summary>
    public sealed class WfResolutionChildrenVirtualSchemeInfo
    {
        private const string name = "WfResolutionChildrenVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string PerformerID = nameof(PerformerID);
        public readonly string PerformerName = nameof(PerformerName);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string OptionID = nameof(OptionID);
        public readonly string OptionCaption = nameof(OptionCaption);
        public readonly string Comment = nameof(Comment);
        public readonly string Answer = nameof(Answer);
        public readonly string Created = nameof(Created);
        public readonly string InProgress = nameof(InProgress);
        public readonly string Planned = nameof(Planned);
        public readonly string Completed = nameof(Completed);
        public readonly string ColumnComment = nameof(ColumnComment);
        public readonly string ColumnState = nameof(ColumnState);

        #endregion

        #region ToString

        public static implicit operator string(WfResolutionChildrenVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WfResolutionPerformers

    /// <summary>
    ///     ID: {0f62f90e-6b94-4301-866d-0138fb147939}
    ///     Alias: WfResolutionPerformers
    ///     Group: Wf
    ///     Description: Исполнители создаваемой резолюции. Используются в заданиях совместно с таблицей WfResolutions.
    ///                  В качестве исполнителя могут выступать несколько контекстных или обычных ролей.
    ///                  Если указано более одной роли, то резолюция назначается на роль задания \"Исполнители задания\".
    /// </summary>
    public sealed class WfResolutionPerformersSchemeInfo
    {
        private const string name = "WfResolutionPerformers";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(WfResolutionPerformersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WfResolutions

    /// <summary>
    ///     ID: {6a0f5914-6a44-4e7d-b400-6b82ec1e2209}
    ///     Alias: WfResolutions
    ///     Group: Wf
    ///     Description: Задание резолюции, построенное на Workflow.
    ///                  Содержит как информацию по заданию, так и информацию по тому, какие поля будут заполняться для действий с резолюцией.
    /// </summary>
    public sealed class WfResolutionsSchemeInfo
    {
        private const string name = "WfResolutions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string KindID = nameof(KindID);
        public readonly string KindCaption = nameof(KindCaption);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string ControllerID = nameof(ControllerID);
        public readonly string ControllerName = nameof(ControllerName);
        public readonly string Comment = nameof(Comment);
        public readonly string Planned = nameof(Planned);
        public readonly string DurationInDays = nameof(DurationInDays);
        public readonly string RevokeChildren = nameof(RevokeChildren);
        public readonly string WithControl = nameof(WithControl);
        public readonly string ShowAdditional = nameof(ShowAdditional);
        public readonly string MassCreation = nameof(MassCreation);
        public readonly string ParentComment = nameof(ParentComment);
        public readonly string MajorPerformer = nameof(MajorPerformer);
        public readonly string SenderID = nameof(SenderID);
        public readonly string SenderName = nameof(SenderName);

        #endregion

        #region ToString

        public static implicit operator string(WfResolutionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WfResolutionsVirtual

    /// <summary>
    ///     ID: {1f805af5-f412-4878-9d70-af989b905fb5}
    ///     Alias: WfResolutionsVirtual
    ///     Group: Wf
    /// </summary>
    public sealed class WfResolutionsVirtualSchemeInfo
    {
        private const string name = "WfResolutionsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Planned = nameof(Planned);
        public readonly string Digest = nameof(Digest);

        #endregion

        #region ToString

        public static implicit operator string(WfResolutionsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WfSatellite

    /// <summary>
    ///     ID: {05394727-2b6f-4d59-9900-d95bc8effdc5}
    ///     Alias: WfSatellite
    ///     Group: Wf
    ///     Description: Основная секция карточки-сателлита для бизнес-процессов Workflow.
    /// </summary>
    public sealed class WfSatelliteSchemeInfo
    {
        private const string name = "WfSatellite";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Data = nameof(Data);

        #endregion

        #region ToString

        public static implicit operator string(WfSatelliteSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WfSatelliteTaskHistory

    /// <summary>
    ///     ID: {cd241343-4eb1-425f-b534-f9ff4cfa597e}
    ///     Alias: WfSatelliteTaskHistory
    ///     Group: Wf
    ///     Description: Дополнительная информация по истории заданий в карточке-сателлите Workflow.
    ///                  ID - идентификатор карточки-сателлита WfSatellite.
    ///                  RowID - идентификатор задания (он же идентификатор записи в истории заданий TaskHistory после того, как задание завершено).
    /// </summary>
    public sealed class WfSatelliteTaskHistorySchemeInfo
    {
        private const string name = "WfSatelliteTaskHistory";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ControllerID = nameof(ControllerID);
        public readonly string ControllerName = nameof(ControllerName);
        public readonly string Controlled = nameof(Controlled);
        public readonly string AliveSubtasks = nameof(AliveSubtasks);

        #endregion

        #region ToString

        public static implicit operator string(WfSatelliteTaskHistorySchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WfTaskCardsVirtual

    /// <summary>
    ///     ID: {ef5f3db3-95d9-4654-91a4-87dcd3d2195a}
    ///     Alias: WfTaskCardsVirtual
    ///     Group: Wf
    ///     Description: Виртуальная секция для карточек-сателлитов для задач.
    /// </summary>
    public sealed class WfTaskCardsVirtualSchemeInfo
    {
        private const string name = "WfTaskCardsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string DocTypeID = nameof(DocTypeID);
        public readonly string DocTypeTitle = nameof(DocTypeTitle);
        public readonly string Number = nameof(Number);
        public readonly string FullNumber = nameof(FullNumber);
        public readonly string Sequence = nameof(Sequence);
        public readonly string Subject = nameof(Subject);
        public readonly string DocDate = nameof(DocDate);
        public readonly string CreationDate = nameof(CreationDate);
        public readonly string StateModified = nameof(StateModified);
        public readonly string MainCardDigest = nameof(MainCardDigest);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string RegistratorID = nameof(RegistratorID);
        public readonly string RegistratorName = nameof(RegistratorName);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);

        #endregion

        #region ToString

        public static implicit operator string(WfTaskCardsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowActions

    /// <summary>
    ///     ID: {df81680b-406f-4f50-9df2-c14dda232aea}
    ///     Alias: WorkflowActions
    ///     Group: WorkflowEngine
    ///     Description: Секция со списком действий
    /// </summary>
    public sealed class WorkflowActionsSchemeInfo
    {
        private const string name = "WorkflowActions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Order = nameof(Order);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);
        public readonly string ActionType = nameof(ActionType);
        public readonly string HasPreCondition = nameof(HasPreCondition);
        public readonly string HasPreScript = nameof(HasPreScript);
        public readonly string HasPostScript = nameof(HasPostScript);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowActionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowCounters

    /// <summary>
    ///     ID: {7adfd330-ab0e-458f-9ac4-f2060bde8c97}
    ///     Alias: WorkflowCounters
    ///     Group: Workflow
    ///     Description: Счётчики заданий, используемые для реализации блоков \"И\" в Workflow.
    /// </summary>
    public sealed class WorkflowCountersSchemeInfo
    {
        private const string name = "WorkflowCounters";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Number = nameof(Number);
        public readonly string CurrentValue = nameof(CurrentValue);
        public readonly string ProcessRowID = nameof(ProcessRowID);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowCountersSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowDefaultSubscriptions

    /// <summary>
    ///     ID: {d8b78ce3-bedf-4faa-9fba-75ddbecf4e04}
    ///     Alias: WorkflowDefaultSubscriptions
    ///     Group: WorkflowEngine
    ///     Description: Секция с подписками узла по умолчанию
    /// </summary>
    public sealed class WorkflowDefaultSubscriptionsSchemeInfo
    {
        private const string name = "WorkflowDefaultSubscriptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string SignalID = nameof(SignalID);
        public readonly string SignalName = nameof(SignalName);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowDefaultSubscriptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineCheckContextRole

    /// <summary>
    ///     ID: {7d0b5402-9d55-4269-964d-25b5ddcb2690}
    ///     Alias: WorkflowEngineCheckContextRole
    ///     Group: WorkflowEngine
    ///     Description: Секция со списком контекстных ролей для расширения на тайлы \"Проверка контекстных ролей\"
    /// </summary>
    public sealed class WorkflowEngineCheckContextRoleSchemeInfo
    {
        private const string name = "WorkflowEngineCheckContextRole";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineCheckContextRoleSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineCommandSubscriptions

    /// <summary>
    ///     ID: {7c45a604-9175-45bd-8525-f218a465b77b}
    ///     Alias: WorkflowEngineCommandSubscriptions
    ///     Group: WorkflowEngine
    ///     Description: Подписки узлов на внешнюю команду
    /// </summary>
    public sealed class WorkflowEngineCommandSubscriptionsSchemeInfo
    {
        private const string name = "WorkflowEngineCommandSubscriptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Command = nameof(Command);
        public readonly string NodeRowID = nameof(NodeRowID);
        public readonly string ProcessRowID = nameof(ProcessRowID);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineCommandSubscriptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineErrors

    /// <summary>
    ///     ID: {61905471-1e69-4478-946f-772f11152386}
    ///     Alias: WorkflowEngineErrors
    ///     Group: WorkflowEngine
    ///     Description: Секция с ошибками обработки
    /// </summary>
    public sealed class WorkflowEngineErrorsSchemeInfo
    {
        private const string name = "WorkflowEngineErrors";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ErrorCardID = nameof(ErrorCardID);
        public readonly string NodeRowID = nameof(NodeRowID);
        public readonly string Active = nameof(Active);
        public readonly string ErrorData = nameof(ErrorData);
        public readonly string Added = nameof(Added);
        public readonly string IsAsync = nameof(IsAsync);
        public readonly string Resumable = nameof(Resumable);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineErrorsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineLogLevels

    /// <summary>
    ///     ID: {9d29f065-3c4b-4209-af8d-10b699895231}
    ///     Alias: WorkflowEngineLogLevels
    ///     Group: WorkflowEngine
    ///     Description: Уровни логирования в WorkflowEngine
    /// </summary>
    public sealed class WorkflowEngineLogLevelsSchemeInfo
    {
        private const string name = "WorkflowEngineLogLevels";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class None
            {
                    public const int ID = 0;
                    public const string Name = "None";
            }
            public static class Error
            {
                    public const int ID = 1;
                    public const string Name = "Error";
            }
            public static class Info
            {
                    public const int ID = 2;
                    public const string Name = "Info";
            }
            public static class Debug
            {
                    public const int ID = 3;
                    public const string Name = "Debug";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineLogLevelsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region WorkflowEngineLogLevels Enumeration

    public sealed class WorkflowEngineLogLevels
    {
        public readonly int ID;
        public readonly string Name;

        public WorkflowEngineLogLevels(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region WorkflowEngineLogs

    /// <summary>
    ///     ID: {f3fa0390-6444-4df6-8be5-cbad1fdd153e}
    ///     Alias: WorkflowEngineLogs
    ///     Group: WorkflowEngine
    ///     Description: Секция с логами процесса
    /// </summary>
    public sealed class WorkflowEngineLogsSchemeInfo
    {
        private const string name = "WorkflowEngineLogs";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ProcessRowID = nameof(ProcessRowID);
        public readonly string LogLevelID = nameof(LogLevelID);
        public readonly string ObjectName = nameof(ObjectName);
        public readonly string Text = nameof(Text);
        public readonly string Added = nameof(Added);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineLogsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineNodes

    /// <summary>
    ///     ID: {69f72d3a-97c1-4d67-a348-071ab861b3c7}
    ///     Alias: WorkflowEngineNodes
    ///     Group: WorkflowEngine
    ///     Description: Содержит состояния активных узлов
    /// </summary>
    public sealed class WorkflowEngineNodesSchemeInfo
    {
        private const string name = "WorkflowEngineNodes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ProcessRowID = nameof(ProcessRowID);
        public readonly string NodeData = nameof(NodeData);
        public readonly string NodeID = nameof(NodeID);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineNodesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineProcesses

    /// <summary>
    ///     ID: {27debe30-ae5f-4f69-89c9-5706e1592540}
    ///     Alias: WorkflowEngineProcesses
    ///     Group: WorkflowEngine
    ///     Description: Содержит состояния активных процессов
    /// </summary>
    public sealed class WorkflowEngineProcessesSchemeInfo
    {
        private const string name = "WorkflowEngineProcesses";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ProcessData = nameof(ProcessData);
        public readonly string ProcessTemplateRowID = nameof(ProcessTemplateRowID);
        public readonly string ProcessTemplateID = nameof(ProcessTemplateID);
        public readonly string CardID = nameof(CardID);
        public readonly string CardDigest = nameof(CardDigest);
        public readonly string Created = nameof(Created);
        public readonly string ParentRowID = nameof(ParentRowID);
        public readonly string Name = nameof(Name);
        public readonly string LastActivity = nameof(LastActivity);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineProcessesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineSettingsAdminRoles

    /// <summary>
    ///     ID: {e493b168-0c0a-4ebc-812e-229bc43aec25}
    ///     Alias: WorkflowEngineSettingsAdminRoles
    ///     Group: WorkflowEngine
    ///     Description: Список ролей, имеющих админские права к карточке шаблона БП
    /// </summary>
    public sealed class WorkflowEngineSettingsAdminRolesSchemeInfo
    {
        private const string name = "WorkflowEngineSettingsAdminRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineSettingsAdminRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineSettingsCreateRoles

    /// <summary>
    ///     ID: {9097ff9c-04a8-4af1-8921-7df7323d46f4}
    ///     Alias: WorkflowEngineSettingsCreateRoles
    ///     Group: WorkflowEngine
    ///     Description: Список ролей, имеющих доступ на создание карточек шаблона БП
    /// </summary>
    public sealed class WorkflowEngineSettingsCreateRolesSchemeInfo
    {
        private const string name = "WorkflowEngineSettingsCreateRoles";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineSettingsCreateRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineSettingsObjectTypeFields

    /// <summary>
    ///     ID: {6efad92a-46be-4d44-a495-271b264f016b}
    ///     Alias: WorkflowEngineSettingsObjectTypeFields
    ///     Group: WorkflowEngine
    ///     Description: Поля для типа объекта
    /// </summary>
    public sealed class WorkflowEngineSettingsObjectTypeFieldsSchemeInfo
    {
        private const string name = "WorkflowEngineSettingsObjectTypeFields";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string FieldID = nameof(FieldID);
        public readonly string FieldName = nameof(FieldName);
        public readonly string ParentRowID = nameof(ParentRowID);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineSettingsObjectTypeFieldsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineSettingsObjectTypes

    /// <summary>
    ///     ID: {140a411c-3b68-44c0-8a5b-cf641d2421f2}
    ///     Alias: WorkflowEngineSettingsObjectTypes
    ///     Group: WorkflowEngine
    ///     Description: Секция с типами для редактора процессов
    /// </summary>
    public sealed class WorkflowEngineSettingsObjectTypesSchemeInfo
    {
        private const string name = "WorkflowEngineSettingsObjectTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);
        public readonly string RefSection = nameof(RefSection);
        public readonly string TableID = nameof(TableID);
        public readonly string TableName = nameof(TableName);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineSettingsObjectTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineSubprocessSubscriptions

    /// <summary>
    ///     ID: {1c83c672-7de7-47f5-8c63-ec41bb5aa7ca}
    ///     Alias: WorkflowEngineSubprocessSubscriptions
    ///     Group: WorkflowEngine
    ///     Description: Подписки узлов к подпроцессам
    /// </summary>
    public sealed class WorkflowEngineSubprocessSubscriptionsSchemeInfo
    {
        private const string name = "WorkflowEngineSubprocessSubscriptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string SubprocessRowID = nameof(SubprocessRowID);
        public readonly string NodeRowID = nameof(NodeRowID);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineSubprocessSubscriptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineTaskActions

    /// <summary>
    ///     ID: {857ef2b9-6bdb-4913-bbc2-2cf9d1ae0b55}
    ///     Alias: WorkflowEngineTaskActions
    ///     Group: WorkflowEngine
    ///     Description: Список возможных действий над заданием
    /// </summary>
    public sealed class WorkflowEngineTaskActionsSchemeInfo
    {
        private const string name = "WorkflowEngineTaskActions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class InProgress
            {
                    public const int ID = 0;
                    public const string Name = "$WorkflowEngine_TaskActions_InProgress";
            }
            public static class ReturnToRole
            {
                    public const int ID = 1;
                    public const string Name = "$WorkflowEngine_TaskActions_ReturnToRole";
            }
            public static class Postpone
            {
                    public const int ID = 2;
                    public const string Name = "$WorkflowEngine_TaskActions_Postpone";
            }
            public static class ReturnFromPostpone
            {
                    public const int ID = 3;
                    public const string Name = "$WorkflowEngine_TaskActions_ReturnFromPostpone";
            }
            public static class Complete
            {
                    public const int ID = 4;
                    public const string Name = "$WorkflowEngine_TaskActions_Complete";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineTaskActionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region WorkflowEngineTaskActions Enumeration

    public sealed class WorkflowEngineTaskActions
    {
        public readonly int ID;
        public readonly string Name;

        public WorkflowEngineTaskActions(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region WorkflowEngineTaskSubscriptions

    /// <summary>
    ///     ID: {5ee285b4-a72c-4a41-88a1-3e052fa1ee44}
    ///     Alias: WorkflowEngineTaskSubscriptions
    ///     Group: WorkflowEngine
    ///     Description: Подписки узлов на действия из заданий
    /// </summary>
    public sealed class WorkflowEngineTaskSubscriptionsSchemeInfo
    {
        private const string name = "WorkflowEngineTaskSubscriptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TaskID = nameof(TaskID);
        public readonly string NodeRowID = nameof(NodeRowID);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineTaskSubscriptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowEngineTimerSubscriptions

    /// <summary>
    ///     ID: {9c65ad25-7d88-4e7c-b398-26d45d1d7204}
    ///     Alias: WorkflowEngineTimerSubscriptions
    ///     Group: WorkflowEngine
    ///     Description: Таблица с подписками таймеров
    /// </summary>
    public sealed class WorkflowEngineTimerSubscriptionsSchemeInfo
    {
        private const string name = "WorkflowEngineTimerSubscriptions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string NodeRowID = nameof(NodeRowID);
        public readonly string Period = nameof(Period);
        public readonly string Cron = nameof(Cron);
        public readonly string Date = nameof(Date);
        public readonly string RunOnce = nameof(RunOnce);
        public readonly string Modified = nameof(Modified);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineTimerSubscriptionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowInLinks

    /// <summary>
    ///     ID: {83bf8e43-0292-4fb8-ac1d-6e36c8ba99a6}
    ///     Alias: WorkflowInLinks
    ///     Group: WorkflowEngine
    ///     Description: Список входящих в узел связей
    /// </summary>
    public sealed class WorkflowInLinksSchemeInfo
    {
        private const string name = "WorkflowInLinks";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);
        public readonly string Script = nameof(Script);
        public readonly string HasCondition = nameof(HasCondition);
        public readonly string Description = nameof(Description);
        public readonly string IsAsync = nameof(IsAsync);
        public readonly string LockProcess = nameof(LockProcess);
        public readonly string LinkModeID = nameof(LinkModeID);
        public readonly string LinkModeName = nameof(LinkModeName);
        public readonly string SignalProcessingModeID = nameof(SignalProcessingModeID);
        public readonly string SignalProcessingModeName = nameof(SignalProcessingModeName);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowInLinksSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowLinkModes

    /// <summary>
    ///     ID: {29b2fb61-6880-43de-a40f-6688e1d0e247}
    ///     Alias: WorkflowLinkModes
    ///     Group: WorkflowEngine
    ///     Description: Типы связи для переходов
    /// </summary>
    public sealed class WorkflowLinkModesSchemeInfo
    {
        private const string name = "WorkflowLinkModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Default
            {
                    public const int ID = 0;
                    public const string Name = "$WorkflowEngine_LinkModes_Default";
            }
            public static class AlwaysCreateNew
            {
                    public const int ID = 1;
                    public const string Name = "$WorkflowEngine_LinkModes_AlwaysCreateNew";
            }
            public static class NeverCreateNew
            {
                    public const int ID = 2;
                    public const string Name = "$WorkflowEngine_LinkModes_NeverCreateNew";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(WorkflowLinkModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region WorkflowLinkModes Enumeration

    public sealed class WorkflowLinkModes
    {
        public readonly int ID;
        public readonly string Name;

        public WorkflowLinkModes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region WorkflowLinks

    /// <summary>
    ///     ID: {9764baef-636c-4558-86cb-0b7e4360f771}
    ///     Alias: WorkflowLinks
    ///     Group: WorkflowEngine
    ///     Description: Секция с параметрами перехода
    /// </summary>
    public sealed class WorkflowLinksSchemeInfo
    {
        private const string name = "WorkflowLinks";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string InScript = nameof(InScript);
        public readonly string OutScript = nameof(OutScript);
        public readonly string InDescription = nameof(InDescription);
        public readonly string OutDescription = nameof(OutDescription);
        public readonly string IsAsync = nameof(IsAsync);
        public readonly string LockProcess = nameof(LockProcess);
        public readonly string LinkModeID = nameof(LinkModeID);
        public readonly string LinkModeName = nameof(LinkModeName);
        public readonly string SignalProcessingModeID = nameof(SignalProcessingModeID);
        public readonly string SignalProcessingModeName = nameof(SignalProcessingModeName);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowLinksSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowMain

    /// <summary>
    ///     ID: {87f7e0c3-2d97-4e36-bb14-1aeec6e67a94}
    ///     Alias: WorkflowMain
    ///     Group: WorkflowEngine
    ///     Description: Основноя таблица для объектов WorkflowEngine
    /// </summary>
    public sealed class WorkflowMainSchemeInfo
    {
        private const string name = "WorkflowMain";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);
        public readonly string PreScript = nameof(PreScript);
        public readonly string PostScript = nameof(PostScript);
        public readonly string Icon = nameof(Icon);
        public readonly string Group = nameof(Group);
        public readonly string GlobalScript = nameof(GlobalScript);
        public readonly string Description = nameof(Description);
        public readonly string ParentTypeID = nameof(ParentTypeID);
        public readonly string ParentTypeName = nameof(ParentTypeName);
        public readonly string LogLevelID = nameof(LogLevelID);
        public readonly string LogLevelName = nameof(LogLevelName);
        public readonly string PreScriptProcessAnySignal = nameof(PreScriptProcessAnySignal);
        public readonly string PostScriptProcessAnySignal = nameof(PostScriptProcessAnySignal);
        public readonly string ProjectPath = nameof(ProjectPath);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowMainSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowNodeInstances

    /// <summary>
    ///     ID: {e2eda913-f68f-4d42-88ba-25f80bd4c3e5}
    ///     Alias: WorkflowNodeInstances
    ///     Group: WorkflowEngine
    ///     Description: Список экземпляров узлов в экзепляре процесса
    /// </summary>
    public sealed class WorkflowNodeInstancesSchemeInfo
    {
        private const string name = "WorkflowNodeInstances";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowNodeInstancesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowNodeInstanceSubprocesses

    /// <summary>
    ///     ID: {830b89cf-862c-4f6a-b564-d538d0bbec90}
    ///     Alias: WorkflowNodeInstanceSubprocesses
    ///     Group: WorkflowEngine
    ///     Description: Секция с отображением подпроцессов, привязанных у экземпляру узла
    /// </summary>
    public sealed class WorkflowNodeInstanceSubprocessesSchemeInfo
    {
        private const string name = "WorkflowNodeInstanceSubprocesses";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Created = nameof(Created);
        public readonly string Name = nameof(Name);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowNodeInstanceSubprocessesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowNodeInstanceTasks

    /// <summary>
    ///     ID: {6ba32f52-56a3-4319-968b-90f1651cc5a7}
    ///     Alias: WorkflowNodeInstanceTasks
    ///     Group: WorkflowEngine
    ///     Description: Секция с отображением заданий, привязанных к экземпляру узла
    /// </summary>
    public sealed class WorkflowNodeInstanceTasksSchemeInfo
    {
        private const string name = "WorkflowNodeInstanceTasks";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);
        public readonly string UserID = nameof(UserID);
        public readonly string UserName = nameof(UserName);
        public readonly string Planned = nameof(Planned);
        public readonly string InProgress = nameof(InProgress);
        public readonly string TypeID = nameof(TypeID);
        public readonly string TypeCaption = nameof(TypeCaption);
        public readonly string Digest = nameof(Digest);
        public readonly string AuthorID = nameof(AuthorID);
        public readonly string AuthorName = nameof(AuthorName);
        public readonly string Postponed = nameof(Postponed);
        public readonly string PostponedTo = nameof(PostponedTo);
        public readonly string Created = nameof(Created);
        public readonly string StateID = nameof(StateID);
        public readonly string StateName = nameof(StateName);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowNodeInstanceTasksSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowOutLinks

    /// <summary>
    ///     ID: {03d962fa-c020-481a-90bd-932cbbd4368d}
    ///     Alias: WorkflowOutLinks
    ///     Group: WorkflowEngine
    ///     Description: Список исходящих из узла связей
    /// </summary>
    public sealed class WorkflowOutLinksSchemeInfo
    {
        private const string name = "WorkflowOutLinks";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Name = nameof(Name);
        public readonly string Caption = nameof(Caption);
        public readonly string Script = nameof(Script);
        public readonly string HasCondition = nameof(HasCondition);
        public readonly string Description = nameof(Description);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowOutLinksSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowPreConditions

    /// <summary>
    ///     ID: {1290310e-0b81-4560-8996-71f5bcb3a9a3}
    ///     Alias: WorkflowPreConditions
    ///     Group: WorkflowEngine
    ///     Description: Список обрабатываемых типов событий
    /// </summary>
    public sealed class WorkflowPreConditionsSchemeInfo
    {
        private const string name = "WorkflowPreConditions";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string SignalID = nameof(SignalID);
        public readonly string SignalName = nameof(SignalName);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowPreConditionsSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowProcessErrorsVirtual

    /// <summary>
    ///     ID: {f7ebd016-ef99-4dfd-ba04-11f428395fe3}
    ///     Alias: WorkflowProcessErrorsVirtual
    ///     Group: WorkflowEngine
    /// </summary>
    public sealed class WorkflowProcessErrorsVirtualSchemeInfo
    {
        private const string name = "WorkflowProcessErrorsVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string Added = nameof(Added);
        public readonly string Text = nameof(Text);
        public readonly string NodeInstanceID = nameof(NodeInstanceID);
        public readonly string IsAsync = nameof(IsAsync);
        public readonly string Resumable = nameof(Resumable);
        public readonly string NodeID = nameof(NodeID);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowProcessErrorsVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowProcesses

    /// <summary>
    ///     ID: {a2db2754-b0ca-4d38-988d-0de6d58057cb}
    ///     Alias: WorkflowProcesses
    ///     Group: Workflow
    ///     Description: Информация по подпроцессам в бизнес-процессе.
    /// </summary>
    public sealed class WorkflowProcessesSchemeInfo
    {
        private const string name = "WorkflowProcesses";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string TypeName = nameof(TypeName);
        public readonly string Params = nameof(Params);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowProcessesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkflowSignalProcessingModes

    /// <summary>
    ///     ID: {67b602c1-ea47-4716-92ba-81f625ba36f1}
    ///     Alias: WorkflowSignalProcessingModes
    ///     Group: WorkflowEngine
    /// </summary>
    public sealed class WorkflowSignalProcessingModesSchemeInfo
    {
        private const string name = "WorkflowSignalProcessingModes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Default
            {
                    public const int ID = 0;
                    public const string Name = "$WorkflowEngine_SignalProcessingMode_Default";
            }
            public static class Async
            {
                    public const int ID = 1;
                    public const string Name = "$WorkflowEngine_SignalProcessingMode_Async";
            }
            public static class AfterUploadingFiles
            {
                    public const int ID = 2;
                    public const string Name = "$WorkflowEngine_SignalProcessingMode_AfterUploadingFiles";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(WorkflowSignalProcessingModesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region WorkflowSignalProcessingModes Enumeration

    public sealed class WorkflowSignalProcessingModes
    {
        public readonly int ID;
        public readonly string Name;

        public WorkflowSignalProcessingModes(int ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region WorkflowSignalTypes

    /// <summary>
    ///     ID: {53dc8c0b-391a-4fbd-86c0-3da697abf065}
    ///     Alias: WorkflowSignalTypes
    ///     Group: WorkflowEngine
    ///     Description: Список типов переходов, доступных для выбора в редакторе бизнес-процессов
    /// </summary>
    public sealed class WorkflowSignalTypesSchemeInfo
    {
        private const string name = "WorkflowSignalTypes";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region Enumeration

        public static class Records
        {
            public static class Default
            {
                    public static Guid ID = new Guid(0x7af6f549,0x66fb,0x438f,0xa9,0x49,0xf7,0x0f,0x4b,0x8e,0x0a,0x15);
                    public const string Name = "Default";
            }
            public static class Exit
            {
                    public static Guid ID = new Guid(0xf24d3d1c,0x6e83,0x4aa6,0xb7,0x6a,0x2f,0xac,0x7d,0xf1,0xf4,0x92);
                    public const string Name = "Exit";
            }
            public static class CompleteTask
            {
                    public static Guid ID = new Guid(0x8bf6e5bb,0x274b,0x44b4,0xa1,0xbd,0xae,0x9f,0xd9,0x01,0x5b,0x17);
                    public const string Name = "CompleteTask";
            }
            public static class DeleteTask
            {
                    public static Guid ID = new Guid(0x38802cf7,0xc3df,0x415c,0xb6,0x82,0xcd,0xae,0x6f,0xef,0xf6,0xce);
                    public const string Name = "DeleteTask";
            }
            public static class ReinstateTask
            {
                    public static Guid ID = new Guid(0x48783b4c,0x4391,0x476c,0xb7,0x17,0xda,0xe1,0xa0,0xcb,0xf6,0xb2);
                    public const string Name = "ReinstateTask";
            }
            public static class ProgressTask
            {
                    public static Guid ID = new Guid(0x507870ba,0xae82,0x4be6,0x9f,0x03,0x80,0x36,0x08,0x98,0x26,0x29);
                    public const string Name = "ProgressTask";
            }
            public static class PostponeTask
            {
                    public static Guid ID = new Guid(0x27b9fd4d,0xb42c,0x4790,0x8f,0xe2,0x0d,0x5f,0x3e,0x9f,0xbd,0xfc);
                    public const string Name = "PostponeTask";
            }
            public static class ReturnFromPostponeTask
            {
                    public static Guid ID = new Guid(0x5c6dda50,0x0a03,0x4430,0xa3,0x28,0x3f,0x7e,0xdd,0xa2,0x91,0xed);
                    public const string Name = "ReturnFromPostponeTask";
            }
            public static class UpdateTask
            {
                    public static Guid ID = new Guid(0xbb667fef,0x0885,0x4dc3,0x93,0x60,0x66,0x51,0x70,0x5f,0x0b,0xac);
                    public const string Name = "UpdateTask";
            }
            public static class SubprocessControl
            {
                    public static Guid ID = new Guid(0x12f172cd,0x7e80,0x45e3,0xa9,0x08,0xa5,0x8c,0xa2,0x4c,0x10,0x1c);
                    public const string Name = "SubprocessControl";
            }
            public static class Start
            {
                    public static Guid ID = new Guid(0x893427ba,0x1d2d,0x4369,0xb7,0xfa,0xc2,0x8e,0x53,0x99,0x78,0x46);
                    public const string Name = "Start";
            }
            public static class UpdateTimer
            {
                    public static Guid ID = new Guid(0x2ee28367,0x0432,0x4c10,0x85,0x71,0xa2,0x9a,0x87,0x2e,0x1e,0xc5);
                    public const string Name = "UpdateTimer";
            }
            public static class StopTimer
            {
                    public static Guid ID = new Guid(0x1b2eeb0c,0x5bda,0x495e,0xa6,0xa0,0xc0,0x9f,0x8f,0x5b,0xae,0x49);
                    public const string Name = "StopTimer";
            }
            public static class TimerTick
            {
                    public static Guid ID = new Guid(0x75cec30e,0x7b67,0x445f,0x9b,0xa6,0x88,0x7e,0x43,0x0b,0x4c,0xc6);
                    public const string Name = "TimerTick";
            }
            public static class SubprocessCompleted
            {
                    public static Guid ID = new Guid(0x380b9b0c,0xa2c3,0x4e98,0x8d,0x5a,0xb9,0x10,0xd6,0xbf,0xcc,0xa2);
                    public const string Name = "SubprocessCompleted";
            }
            public static class TaskGroupControl
            {
                    public static Guid ID = new Guid(0x00cbaffe,0x5c4e,0x4cba,0x8f,0x74,0xcb,0xe7,0x96,0xb7,0x37,0xe9);
                    public const string Name = "TaskGroupControl";
            }
        }

        #endregion

        #region ToString

        public static implicit operator string(WorkflowSignalTypesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #region WorkflowSignalTypes Enumeration

    public sealed class WorkflowSignalTypes
    {
        public readonly Guid ID;
        public readonly string Name;

        public WorkflowSignalTypes(Guid ID, string Name)
        {
            this.ID = ID;
            this.Name = Name;
        }
    }

    #endregion

    #endregion

    #region WorkflowTasks

    /// <summary>
    ///     ID: {d2683167-0425-4093-ba65-0196ded5437a}
    ///     Alias: WorkflowTasks
    ///     Group: Workflow
    ///     Description: Список активных заданий Workflow. В качестве RowID используется идентификатор задания.
    /// </summary>
    public sealed class WorkflowTasksSchemeInfo
    {
        private const string name = "WorkflowTasks";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string ProcessRowID = nameof(ProcessRowID);
        public readonly string Params = nameof(Params);

        #endregion

        #region ToString

        public static implicit operator string(WorkflowTasksSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkplaceRoles

    /// <summary>
    ///     ID: {ad21dc6e-c694-4862-ba61-1df6b7506101}
    ///     Alias: WorkplaceRoles
    ///     Group: System
    ///     Description: Роли, которые могут использовать рабочее место.
    /// </summary>
    public sealed class WorkplaceRolesSchemeInfo
    {
        private const string name = "WorkplaceRoles";

        #region Columns

        public readonly string RoleID = nameof(RoleID);
        public readonly string WorkplaceID = nameof(WorkplaceID);

        #endregion

        #region ToString

        public static implicit operator string(WorkplaceRolesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkplaceRolesVirtual

    /// <summary>
    ///     ID: {67f548c6-9fdf-44c1-9d61-eea3098021f5}
    ///     Alias: WorkplaceRolesVirtual
    ///     Group: System
    ///     Description: Список ролей для представлений, отображаемых как виртуальные карточки в клиенте.
    /// </summary>
    public sealed class WorkplaceRolesVirtualSchemeInfo
    {
        private const string name = "WorkplaceRolesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string RowID = nameof(RowID);
        public readonly string RoleID = nameof(RoleID);
        public readonly string RoleName = nameof(RoleName);

        #endregion

        #region ToString

        public static implicit operator string(WorkplaceRolesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region Workplaces

    /// <summary>
    ///     ID: {21cd7a4f-6930-4746-9a57-72481e951b02}
    ///     Alias: Workplaces
    ///     Group: System
    ///     Description: Рабочие места.
    /// </summary>
    public sealed class WorkplacesSchemeInfo
    {
        private const string name = "Workplaces";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);
        public readonly string Metadata = nameof(Metadata);
        public readonly string Order = nameof(Order);

        #endregion

        #region ToString

        public static implicit operator string(WorkplacesSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    #region WorkplacesVirtual

    /// <summary>
    ///     ID: {a2f0c6b0-32c0-4c2e-97c4-e431ef93fc84}
    ///     Alias: WorkplacesVirtual
    ///     Group: System
    ///     Description: Рабочие места, отображаемые как виртуальные карточки в клиенте.
    /// </summary>
    public sealed class WorkplacesVirtualSchemeInfo
    {
        private const string name = "WorkplacesVirtual";

        #region Columns

        public readonly string ID = nameof(ID);
        public readonly string Name = nameof(Name);

        #endregion

        #region ToString

        public static implicit operator string(WorkplacesVirtualSchemeInfo obj) => obj.ToString();

        public override string ToString() => name;

        #endregion
    }

    #endregion

    public static class SchemeInfo
    {
        #region Tables

        public static readonly AccessLevelsSchemeInfo AccessLevels = new AccessLevelsSchemeInfo();
        public static readonly AclSchemeInfo Acl = new AclSchemeInfo();
        public static readonly AclGenerationInfoSchemeInfo AclGenerationInfo = new AclGenerationInfoSchemeInfo();
        public static readonly AclGenerationRuleExtensionsSchemeInfo AclGenerationRuleExtensions = new AclGenerationRuleExtensionsSchemeInfo();
        public static readonly AclGenerationRulesSchemeInfo AclGenerationRules = new AclGenerationRulesSchemeInfo();
        public static readonly AclGenerationRuleTriggerModesSchemeInfo AclGenerationRuleTriggerModes = new AclGenerationRuleTriggerModesSchemeInfo();
        public static readonly AclGenerationRuleTriggersSchemeInfo AclGenerationRuleTriggers = new AclGenerationRuleTriggersSchemeInfo();
        public static readonly AclGenerationRuleTriggerTypesSchemeInfo AclGenerationRuleTriggerTypes = new AclGenerationRuleTriggerTypesSchemeInfo();
        public static readonly AclGenerationRuleTypesSchemeInfo AclGenerationRuleTypes = new AclGenerationRuleTypesSchemeInfo();
        public static readonly AcquaintanceCommentsSchemeInfo AcquaintanceComments = new AcquaintanceCommentsSchemeInfo();
        public static readonly AcquaintanceRowsSchemeInfo AcquaintanceRows = new AcquaintanceRowsSchemeInfo();
        public static readonly ActionHistorySchemeInfo ActionHistory = new ActionHistorySchemeInfo();
        public static readonly ActionHistoryDatabasesSchemeInfo ActionHistoryDatabases = new ActionHistoryDatabasesSchemeInfo();
        public static readonly ActionHistoryDatabasesVirtualSchemeInfo ActionHistoryDatabasesVirtual = new ActionHistoryDatabasesVirtualSchemeInfo();
        public static readonly ActionHistoryVirtualSchemeInfo ActionHistoryVirtual = new ActionHistoryVirtualSchemeInfo();
        public static readonly ActionTypesSchemeInfo ActionTypes = new ActionTypesSchemeInfo();
        public static readonly AdSyncRootsSchemeInfo AdSyncRoots = new AdSyncRootsSchemeInfo();
        public static readonly AdSyncSettingsSchemeInfo AdSyncSettings = new AdSyncSettingsSchemeInfo();
        public static readonly AdSyncSettingsVirtualSchemeInfo AdSyncSettingsVirtual = new AdSyncSettingsVirtualSchemeInfo();
        public static readonly ApplicationArchitecturesSchemeInfo ApplicationArchitectures = new ApplicationArchitecturesSchemeInfo();
        public static readonly ApplicationNamesSchemeInfo ApplicationNames = new ApplicationNamesSchemeInfo();
        public static readonly ApplicationRolesSchemeInfo ApplicationRoles = new ApplicationRolesSchemeInfo();
        public static readonly ApplicationsSchemeInfo Applications = new ApplicationsSchemeInfo();
        public static readonly BackgroundColorsSchemeInfo BackgroundColors = new BackgroundColorsSchemeInfo();
        public static readonly BarcodeTypesSchemeInfo BarcodeTypes = new BarcodeTypesSchemeInfo();
        public static readonly BlockColorsSchemeInfo BlockColors = new BlockColorsSchemeInfo();
        public static readonly BusinessProcessButtonExtensionSchemeInfo BusinessProcessButtonExtension = new BusinessProcessButtonExtensionSchemeInfo();
        public static readonly BusinessProcessButtonRolesSchemeInfo BusinessProcessButtonRoles = new BusinessProcessButtonRolesSchemeInfo();
        public static readonly BusinessProcessButtonRolesVirtualSchemeInfo BusinessProcessButtonRolesVirtual = new BusinessProcessButtonRolesVirtualSchemeInfo();
        public static readonly BusinessProcessButtonsSchemeInfo BusinessProcessButtons = new BusinessProcessButtonsSchemeInfo();
        public static readonly BusinessProcessButtonsVirtualSchemeInfo BusinessProcessButtonsVirtual = new BusinessProcessButtonsVirtualSchemeInfo();
        public static readonly BusinessProcessCardTypesSchemeInfo BusinessProcessCardTypes = new BusinessProcessCardTypesSchemeInfo();
        public static readonly BusinessProcessEditRolesSchemeInfo BusinessProcessEditRoles = new BusinessProcessEditRolesSchemeInfo();
        public static readonly BusinessProcessExtensionsSchemeInfo BusinessProcessExtensions = new BusinessProcessExtensionsSchemeInfo();
        public static readonly BusinessProcessInfoSchemeInfo BusinessProcessInfo = new BusinessProcessInfoSchemeInfo();
        public static readonly BusinessProcessReadRolesSchemeInfo BusinessProcessReadRoles = new BusinessProcessReadRolesSchemeInfo();
        public static readonly BusinessProcessVersionsSchemeInfo BusinessProcessVersions = new BusinessProcessVersionsSchemeInfo();
        public static readonly BusinessProcessVersionsVirtualSchemeInfo BusinessProcessVersionsVirtual = new BusinessProcessVersionsVirtualSchemeInfo();
        public static readonly CalendarCalcMethodsSchemeInfo CalendarCalcMethods = new CalendarCalcMethodsSchemeInfo();
        public static readonly CalendarExclusionsSchemeInfo CalendarExclusions = new CalendarExclusionsSchemeInfo();
        public static readonly CalendarNamedRangesSchemeInfo CalendarNamedRanges = new CalendarNamedRangesSchemeInfo();
        public static readonly CalendarQuantsSchemeInfo CalendarQuants = new CalendarQuantsSchemeInfo();
        public static readonly CalendarSettingsSchemeInfo CalendarSettings = new CalendarSettingsSchemeInfo();
        public static readonly CalendarTypeExclusionsSchemeInfo CalendarTypeExclusions = new CalendarTypeExclusionsSchemeInfo();
        public static readonly CalendarTypesSchemeInfo CalendarTypes = new CalendarTypesSchemeInfo();
        public static readonly CalendarTypeWeekDaysSchemeInfo CalendarTypeWeekDays = new CalendarTypeWeekDaysSchemeInfo();
        public static readonly CompilationCacheSchemeInfo CompilationCache = new CompilationCacheSchemeInfo();
        public static readonly CompiledViewsSchemeInfo CompiledViews = new CompiledViewsSchemeInfo();
        public static readonly CompletionOptionsSchemeInfo CompletionOptions = new CompletionOptionsSchemeInfo();
        public static readonly CompletionOptionsVirtualSchemeInfo CompletionOptionsVirtual = new CompletionOptionsVirtualSchemeInfo();
        public static readonly ConditionsVirtualSchemeInfo ConditionsVirtual = new ConditionsVirtualSchemeInfo();
        public static readonly ConditionTypesSchemeInfo ConditionTypes = new ConditionTypesSchemeInfo();
        public static readonly ConditionTypeUsePlacesSchemeInfo ConditionTypeUsePlaces = new ConditionTypeUsePlacesSchemeInfo();
        public static readonly ConditionUsePlacesSchemeInfo ConditionUsePlaces = new ConditionUsePlacesSchemeInfo();
        public static readonly ConfigurationSchemeInfo Configuration = new ConfigurationSchemeInfo();
        public static readonly ContextRolesSchemeInfo ContextRoles = new ContextRolesSchemeInfo();
        public static readonly CurrenciesSchemeInfo Currencies = new CurrenciesSchemeInfo();
        public static readonly CustomBackgroundColorsVirtualSchemeInfo CustomBackgroundColorsVirtual = new CustomBackgroundColorsVirtualSchemeInfo();
        public static readonly CustomBlockColorsVirtualSchemeInfo CustomBlockColorsVirtual = new CustomBlockColorsVirtualSchemeInfo();
        public static readonly CustomForegroundColorsVirtualSchemeInfo CustomForegroundColorsVirtual = new CustomForegroundColorsVirtualSchemeInfo();
        public static readonly DateFormatsSchemeInfo DateFormats = new DateFormatsSchemeInfo();
        public static readonly DefaultTimeZoneSchemeInfo DefaultTimeZone = new DefaultTimeZoneSchemeInfo();
        public static readonly DefaultWorkplacesVirtualSchemeInfo DefaultWorkplacesVirtual = new DefaultWorkplacesVirtualSchemeInfo();
        public static readonly DeletedSchemeInfo Deleted = new DeletedSchemeInfo();
        public static readonly DeletedTaskRolesSchemeInfo DeletedTaskRoles = new DeletedTaskRolesSchemeInfo();
        public static readonly DeletedVirtualSchemeInfo DeletedVirtual = new DeletedVirtualSchemeInfo();
        public static readonly DepartmentRolesSchemeInfo DepartmentRoles = new DepartmentRolesSchemeInfo();
        public static readonly DeviceTypesSchemeInfo DeviceTypes = new DeviceTypesSchemeInfo();
        public static readonly DialogButtonTypesSchemeInfo DialogButtonTypes = new DialogButtonTypesSchemeInfo();
        public static readonly DialogCardAutoOpenModesSchemeInfo DialogCardAutoOpenModes = new DialogCardAutoOpenModesSchemeInfo();
        public static readonly DialogCardStoreModesSchemeInfo DialogCardStoreModes = new DialogCardStoreModesSchemeInfo();
        public static readonly DialogRolesSchemeInfo DialogRoles = new DialogRolesSchemeInfo();
        public static readonly DialogsSchemeInfo Dialogs = new DialogsSchemeInfo();
        public static readonly DocLoadBarcodeReadSchemeInfo DocLoadBarcodeRead = new DocLoadBarcodeReadSchemeInfo();
        public static readonly DocLoadSettingsSchemeInfo DocLoadSettings = new DocLoadSettingsSchemeInfo();
        public static readonly DocumentCategoriesSchemeInfo DocumentCategories = new DocumentCategoriesSchemeInfo();
        public static readonly DocumentCommonInfoSchemeInfo DocumentCommonInfo = new DocumentCommonInfoSchemeInfo();
        public static readonly DynamicRolesSchemeInfo DynamicRoles = new DynamicRolesSchemeInfo();
        public static readonly ErrorsSchemeInfo Errors = new ErrorsSchemeInfo();
        public static readonly FieldChangedConditionSchemeInfo FieldChangedCondition = new FieldChangedConditionSchemeInfo();
        public static readonly FileCategoriesSchemeInfo FileCategories = new FileCategoriesSchemeInfo();
        public static readonly FileContentSchemeInfo FileContent = new FileContentSchemeInfo();
        public static readonly FileConverterCacheSchemeInfo FileConverterCache = new FileConverterCacheSchemeInfo();
        public static readonly FileConverterCacheVirtualSchemeInfo FileConverterCacheVirtual = new FileConverterCacheVirtualSchemeInfo();
        public static readonly FileConverterTypesSchemeInfo FileConverterTypes = new FileConverterTypesSchemeInfo();
        public static readonly FilesSchemeInfo Files = new FilesSchemeInfo();
        public static readonly FileSignatureEventTypesSchemeInfo FileSignatureEventTypes = new FileSignatureEventTypesSchemeInfo();
        public static readonly FileSignaturesSchemeInfo FileSignatures = new FileSignaturesSchemeInfo();
        public static readonly FileSourcesSchemeInfo FileSources = new FileSourcesSchemeInfo();
        public static readonly FileSourcesVirtualSchemeInfo FileSourcesVirtual = new FileSourcesVirtualSchemeInfo();
        public static readonly FileStatesSchemeInfo FileStates = new FileStatesSchemeInfo();
        public static readonly FileTemplateRolesSchemeInfo FileTemplateRoles = new FileTemplateRolesSchemeInfo();
        public static readonly FileTemplatesSchemeInfo FileTemplates = new FileTemplatesSchemeInfo();
        public static readonly FileTemplateTemplateTypesSchemeInfo FileTemplateTemplateTypes = new FileTemplateTemplateTypesSchemeInfo();
        public static readonly FileTemplateTypesSchemeInfo FileTemplateTypes = new FileTemplateTypesSchemeInfo();
        public static readonly FileTemplateViewsSchemeInfo FileTemplateViews = new FileTemplateViewsSchemeInfo();
        public static readonly FileVersionsSchemeInfo FileVersions = new FileVersionsSchemeInfo();
        public static readonly FmAttachmentsSchemeInfo FmAttachments = new FmAttachmentsSchemeInfo();
        public static readonly FmAttachmentTypesSchemeInfo FmAttachmentTypes = new FmAttachmentTypesSchemeInfo();
        public static readonly FmMessagesSchemeInfo FmMessages = new FmMessagesSchemeInfo();
        public static readonly FmMessagesPluginTableSchemeInfo FmMessagesPluginTable = new FmMessagesPluginTableSchemeInfo();
        public static readonly FmMessageTypesSchemeInfo FmMessageTypes = new FmMessageTypesSchemeInfo();
        public static readonly FmNotificationsSchemeInfo FmNotifications = new FmNotificationsSchemeInfo();
        public static readonly FmParticipantTypesSchemeInfo FmParticipantTypes = new FmParticipantTypesSchemeInfo();
        public static readonly FmTopicParticipantRolesSchemeInfo FmTopicParticipantRoles = new FmTopicParticipantRolesSchemeInfo();
        public static readonly FmTopicParticipantRolesUnsubscribedSchemeInfo FmTopicParticipantRolesUnsubscribed = new FmTopicParticipantRolesUnsubscribedSchemeInfo();
        public static readonly FmTopicParticipantsSchemeInfo FmTopicParticipants = new FmTopicParticipantsSchemeInfo();
        public static readonly FmTopicsSchemeInfo FmTopics = new FmTopicsSchemeInfo();
        public static readonly FmTopicTypesSchemeInfo FmTopicTypes = new FmTopicTypesSchemeInfo();
        public static readonly FmUserSettingsVirtualSchemeInfo FmUserSettingsVirtual = new FmUserSettingsVirtualSchemeInfo();
        public static readonly FmUserStatSchemeInfo FmUserStat = new FmUserStatSchemeInfo();
        public static readonly ForegroundColorsSchemeInfo ForegroundColors = new ForegroundColorsSchemeInfo();
        public static readonly FormatSettingsSchemeInfo FormatSettings = new FormatSettingsSchemeInfo();
        public static readonly FunctionRolesSchemeInfo FunctionRoles = new FunctionRolesSchemeInfo();
        public static readonly FunctionRolesVirtualSchemeInfo FunctionRolesVirtual = new FunctionRolesVirtualSchemeInfo();
        public static readonly FunctionsSchemeInfo Functions = new FunctionsSchemeInfo();
        public static readonly HelpSectionsSchemeInfo HelpSections = new HelpSectionsSchemeInfo();
        public static readonly IncomingRefDocsSchemeInfo IncomingRefDocs = new IncomingRefDocsSchemeInfo();
        public static readonly InstancesSchemeInfo Instances = new InstancesSchemeInfo();
        public static readonly InstanceTypesSchemeInfo InstanceTypes = new InstanceTypesSchemeInfo();
        public static readonly KrAcquaintanceActionSchemeInfo KrAcquaintanceAction = new KrAcquaintanceActionSchemeInfo();
        public static readonly KrAcquaintanceActionRolesSchemeInfo KrAcquaintanceActionRoles = new KrAcquaintanceActionRolesSchemeInfo();
        public static readonly KrAcquaintanceSettingsVirtualSchemeInfo KrAcquaintanceSettingsVirtual = new KrAcquaintanceSettingsVirtualSchemeInfo();
        public static readonly KrActionTypesSchemeInfo KrActionTypes = new KrActionTypesSchemeInfo();
        public static readonly KrActiveTasksSchemeInfo KrActiveTasks = new KrActiveTasksSchemeInfo();
        public static readonly KrActiveTasksVirtualSchemeInfo KrActiveTasksVirtual = new KrActiveTasksVirtualSchemeInfo();
        public static readonly KrAddFromTemplateSettingsVirtualSchemeInfo KrAddFromTemplateSettingsVirtual = new KrAddFromTemplateSettingsVirtualSchemeInfo();
        public static readonly KrAdditionalApprovalSchemeInfo KrAdditionalApproval = new KrAdditionalApprovalSchemeInfo();
        public static readonly KrAdditionalApprovalInfoSchemeInfo KrAdditionalApprovalInfo = new KrAdditionalApprovalInfoSchemeInfo();
        public static readonly KrAdditionalApprovalInfoUsersCardVirtualSchemeInfo KrAdditionalApprovalInfoUsersCardVirtual = new KrAdditionalApprovalInfoUsersCardVirtualSchemeInfo();
        public static readonly KrAdditionalApprovalInfoVirtualSchemeInfo KrAdditionalApprovalInfoVirtual = new KrAdditionalApprovalInfoVirtualSchemeInfo();
        public static readonly KrAdditionalApprovalsRequestedInfoVirtualSchemeInfo KrAdditionalApprovalsRequestedInfoVirtual = new KrAdditionalApprovalsRequestedInfoVirtualSchemeInfo();
        public static readonly KrAdditionalApprovalTaskInfoSchemeInfo KrAdditionalApprovalTaskInfo = new KrAdditionalApprovalTaskInfoSchemeInfo();
        public static readonly KrAdditionalApprovalUsersSchemeInfo KrAdditionalApprovalUsers = new KrAdditionalApprovalUsersSchemeInfo();
        public static readonly KrAdditionalApprovalUsersCardVirtualSchemeInfo KrAdditionalApprovalUsersCardVirtual = new KrAdditionalApprovalUsersCardVirtualSchemeInfo();
        public static readonly KrAmendingActionVirtualSchemeInfo KrAmendingActionVirtual = new KrAmendingActionVirtualSchemeInfo();
        public static readonly KrApprovalActionAdditionalPerformersDisplayInfoVirtualSchemeInfo KrApprovalActionAdditionalPerformersDisplayInfoVirtual = new KrApprovalActionAdditionalPerformersDisplayInfoVirtualSchemeInfo();
        public static readonly KrApprovalActionAdditionalPerformersSettingsVirtualSchemeInfo KrApprovalActionAdditionalPerformersSettingsVirtual = new KrApprovalActionAdditionalPerformersSettingsVirtualSchemeInfo();
        public static readonly KrApprovalActionAdditionalPerformersVirtualSchemeInfo KrApprovalActionAdditionalPerformersVirtual = new KrApprovalActionAdditionalPerformersVirtualSchemeInfo();
        public static readonly KrApprovalActionNotificationActionRolesVirtualSchemeInfo KrApprovalActionNotificationActionRolesVirtual = new KrApprovalActionNotificationActionRolesVirtualSchemeInfo();
        public static readonly KrApprovalActionNotificationRolesVirtualSchemeInfo KrApprovalActionNotificationRolesVirtual = new KrApprovalActionNotificationRolesVirtualSchemeInfo();
        public static readonly KrApprovalActionOptionLinksVirtualSchemeInfo KrApprovalActionOptionLinksVirtual = new KrApprovalActionOptionLinksVirtualSchemeInfo();
        public static readonly KrApprovalActionOptionsActionVirtualSchemeInfo KrApprovalActionOptionsActionVirtual = new KrApprovalActionOptionsActionVirtualSchemeInfo();
        public static readonly KrApprovalActionOptionsVirtualSchemeInfo KrApprovalActionOptionsVirtual = new KrApprovalActionOptionsVirtualSchemeInfo();
        public static readonly KrApprovalActionVirtualSchemeInfo KrApprovalActionVirtual = new KrApprovalActionVirtualSchemeInfo();
        public static readonly KrApprovalCommonInfoSchemeInfo KrApprovalCommonInfo = new KrApprovalCommonInfoSchemeInfo();
        public static readonly KrApprovalCommonInfoVirtualSchemeInfo KrApprovalCommonInfoVirtual = new KrApprovalCommonInfoVirtualSchemeInfo();
        public static readonly KrApprovalHistorySchemeInfo KrApprovalHistory = new KrApprovalHistorySchemeInfo();
        public static readonly KrApprovalHistoryVirtualSchemeInfo KrApprovalHistoryVirtual = new KrApprovalHistoryVirtualSchemeInfo();
        public static readonly KrApprovalSettingsVirtualSchemeInfo KrApprovalSettingsVirtual = new KrApprovalSettingsVirtualSchemeInfo();
        public static readonly KrAuthorSettingsVirtualSchemeInfo KrAuthorSettingsVirtual = new KrAuthorSettingsVirtualSchemeInfo();
        public static readonly KrAutoApproveHistorySchemeInfo KrAutoApproveHistory = new KrAutoApproveHistorySchemeInfo();
        public static readonly KrBuildGlobalOutputVirtualSchemeInfo KrBuildGlobalOutputVirtual = new KrBuildGlobalOutputVirtualSchemeInfo();
        public static readonly KrBuildLocalOutputVirtualSchemeInfo KrBuildLocalOutputVirtual = new KrBuildLocalOutputVirtualSchemeInfo();
        public static readonly KrBuildStatesSchemeInfo KrBuildStates = new KrBuildStatesSchemeInfo();
        public static readonly KrCardGeneratorVirtualSchemeInfo KrCardGeneratorVirtual = new KrCardGeneratorVirtualSchemeInfo();
        public static readonly KrCardTasksEditorDialogVirtualSchemeInfo KrCardTasksEditorDialogVirtual = new KrCardTasksEditorDialogVirtualSchemeInfo();
        public static readonly KrCardTypesVirtualSchemeInfo KrCardTypesVirtual = new KrCardTypesVirtualSchemeInfo();
        public static readonly KrChangeStateActionSchemeInfo KrChangeStateAction = new KrChangeStateActionSchemeInfo();
        public static readonly KrChangeStateSettingsVirtualSchemeInfo KrChangeStateSettingsVirtual = new KrChangeStateSettingsVirtualSchemeInfo();
        public static readonly KrCheckStateTileExtensionSchemeInfo KrCheckStateTileExtension = new KrCheckStateTileExtensionSchemeInfo();
        public static readonly KrCommentatorsSchemeInfo KrCommentators = new KrCommentatorsSchemeInfo();
        public static readonly KrCommentsInfoSchemeInfo KrCommentsInfo = new KrCommentsInfoSchemeInfo();
        public static readonly KrCommentsInfoVirtualSchemeInfo KrCommentsInfoVirtual = new KrCommentsInfoVirtualSchemeInfo();
        public static readonly KrCreateCardActionSchemeInfo KrCreateCardAction = new KrCreateCardActionSchemeInfo();
        public static readonly KrCreateCardStageSettingsVirtualSchemeInfo KrCreateCardStageSettingsVirtual = new KrCreateCardStageSettingsVirtualSchemeInfo();
        public static readonly KrCreateCardStageTypeModesSchemeInfo KrCreateCardStageTypeModes = new KrCreateCardStageTypeModesSchemeInfo();
        public static readonly KrCycleGroupingModesSchemeInfo KrCycleGroupingModes = new KrCycleGroupingModesSchemeInfo();
        public static readonly KrDepartmentConditionSchemeInfo KrDepartmentCondition = new KrDepartmentConditionSchemeInfo();
        public static readonly KrDepartmentConditionSettingsSchemeInfo KrDepartmentConditionSettings = new KrDepartmentConditionSettingsSchemeInfo();
        public static readonly KrDialogButtonSettingsVirtualSchemeInfo KrDialogButtonSettingsVirtual = new KrDialogButtonSettingsVirtualSchemeInfo();
        public static readonly KrDialogStageTypeSettingsVirtualSchemeInfo KrDialogStageTypeSettingsVirtual = new KrDialogStageTypeSettingsVirtualSchemeInfo();
        public static readonly KrDocNumberRegistrationAutoAssignmentSchemeInfo KrDocNumberRegistrationAutoAssignment = new KrDocNumberRegistrationAutoAssignmentSchemeInfo();
        public static readonly KrDocNumberRegularAutoAssignmentSchemeInfo KrDocNumberRegularAutoAssignment = new KrDocNumberRegularAutoAssignmentSchemeInfo();
        public static readonly KrDocStateSchemeInfo KrDocState = new KrDocStateSchemeInfo();
        public static readonly KrDocStateConditionSchemeInfo KrDocStateCondition = new KrDocStateConditionSchemeInfo();
        public static readonly KrDocStateVirtualSchemeInfo KrDocStateVirtual = new KrDocStateVirtualSchemeInfo();
        public static readonly KrDocTypeSchemeInfo KrDocType = new KrDocTypeSchemeInfo();
        public static readonly KrDocTypeConditionSchemeInfo KrDocTypeCondition = new KrDocTypeConditionSchemeInfo();
        public static readonly KrEditSettingsVirtualSchemeInfo KrEditSettingsVirtual = new KrEditSettingsVirtualSchemeInfo();
        public static readonly KrForkManagementModesSchemeInfo KrForkManagementModes = new KrForkManagementModesSchemeInfo();
        public static readonly KrForkManagementSettingsVirtualSchemeInfo KrForkManagementSettingsVirtual = new KrForkManagementSettingsVirtualSchemeInfo();
        public static readonly KrForkNestedProcessesSettingsVirtualSchemeInfo KrForkNestedProcessesSettingsVirtual = new KrForkNestedProcessesSettingsVirtualSchemeInfo();
        public static readonly KrForkSecondaryProcessesSettingsVirtualSchemeInfo KrForkSecondaryProcessesSettingsVirtual = new KrForkSecondaryProcessesSettingsVirtualSchemeInfo();
        public static readonly KrForkSettingsVirtualSchemeInfo KrForkSettingsVirtual = new KrForkSettingsVirtualSchemeInfo();
        public static readonly KrHistoryManagementStageSettingsVirtualSchemeInfo KrHistoryManagementStageSettingsVirtual = new KrHistoryManagementStageSettingsVirtualSchemeInfo();
        public static readonly KrInfoForInitiatorSchemeInfo KrInfoForInitiator = new KrInfoForInitiatorSchemeInfo();
        public static readonly KrNotificationOptionalRecipientsVirtualSchemeInfo KrNotificationOptionalRecipientsVirtual = new KrNotificationOptionalRecipientsVirtualSchemeInfo();
        public static readonly KrNotificationSettingVirtualSchemeInfo KrNotificationSettingVirtual = new KrNotificationSettingVirtualSchemeInfo();
        public static readonly KrPartnerConditionSchemeInfo KrPartnerCondition = new KrPartnerConditionSchemeInfo();
        public static readonly KrPerformersVirtualSchemeInfo KrPerformersVirtual = new KrPerformersVirtualSchemeInfo();
        public static readonly KrPermissionAclGenerationRulesSchemeInfo KrPermissionAclGenerationRules = new KrPermissionAclGenerationRulesSchemeInfo();
        public static readonly KrPermissionExtendedCardRuleFieldsSchemeInfo KrPermissionExtendedCardRuleFields = new KrPermissionExtendedCardRuleFieldsSchemeInfo();
        public static readonly KrPermissionExtendedCardRulesSchemeInfo KrPermissionExtendedCardRules = new KrPermissionExtendedCardRulesSchemeInfo();
        public static readonly KrPermissionExtendedFileRuleCategoriesSchemeInfo KrPermissionExtendedFileRuleCategories = new KrPermissionExtendedFileRuleCategoriesSchemeInfo();
        public static readonly KrPermissionExtendedFileRulesSchemeInfo KrPermissionExtendedFileRules = new KrPermissionExtendedFileRulesSchemeInfo();
        public static readonly KrPermissionExtendedMandatoryRuleFieldsSchemeInfo KrPermissionExtendedMandatoryRuleFields = new KrPermissionExtendedMandatoryRuleFieldsSchemeInfo();
        public static readonly KrPermissionExtendedMandatoryRuleOptionsSchemeInfo KrPermissionExtendedMandatoryRuleOptions = new KrPermissionExtendedMandatoryRuleOptionsSchemeInfo();
        public static readonly KrPermissionExtendedMandatoryRulesSchemeInfo KrPermissionExtendedMandatoryRules = new KrPermissionExtendedMandatoryRulesSchemeInfo();
        public static readonly KrPermissionExtendedMandatoryRuleTypesSchemeInfo KrPermissionExtendedMandatoryRuleTypes = new KrPermissionExtendedMandatoryRuleTypesSchemeInfo();
        public static readonly KrPermissionExtendedTaskRuleFieldsSchemeInfo KrPermissionExtendedTaskRuleFields = new KrPermissionExtendedTaskRuleFieldsSchemeInfo();
        public static readonly KrPermissionExtendedTaskRulesSchemeInfo KrPermissionExtendedTaskRules = new KrPermissionExtendedTaskRulesSchemeInfo();
        public static readonly KrPermissionExtendedTaskRuleTypesSchemeInfo KrPermissionExtendedTaskRuleTypes = new KrPermissionExtendedTaskRuleTypesSchemeInfo();
        public static readonly KrPermissionExtendedVisibilityRulesSchemeInfo KrPermissionExtendedVisibilityRules = new KrPermissionExtendedVisibilityRulesSchemeInfo();
        public static readonly KrPermissionRolesSchemeInfo KrPermissionRoles = new KrPermissionRolesSchemeInfo();
        public static readonly KrPermissionRuleAccessSettingsSchemeInfo KrPermissionRuleAccessSettings = new KrPermissionRuleAccessSettingsSchemeInfo();
        public static readonly KrPermissionsSchemeInfo KrPermissions = new KrPermissionsSchemeInfo();
        public static readonly KrPermissionsControlTypesSchemeInfo KrPermissionsControlTypes = new KrPermissionsControlTypesSchemeInfo();
        public static readonly KrPermissionsFileCheckRulesSchemeInfo KrPermissionsFileCheckRules = new KrPermissionsFileCheckRulesSchemeInfo();
        public static readonly KrPermissionsFileEditAccessSettingsSchemeInfo KrPermissionsFileEditAccessSettings = new KrPermissionsFileEditAccessSettingsSchemeInfo();
        public static readonly KrPermissionsFileReadAccessSettingsSchemeInfo KrPermissionsFileReadAccessSettings = new KrPermissionsFileReadAccessSettingsSchemeInfo();
        public static readonly KrPermissionsMandatoryValidationTypesSchemeInfo KrPermissionsMandatoryValidationTypes = new KrPermissionsMandatoryValidationTypesSchemeInfo();
        public static readonly KrPermissionsSystemSchemeInfo KrPermissionsSystem = new KrPermissionsSystemSchemeInfo();
        public static readonly KrPermissionStatesSchemeInfo KrPermissionStates = new KrPermissionStatesSchemeInfo();
        public static readonly KrPermissionTypesSchemeInfo KrPermissionTypes = new KrPermissionTypesSchemeInfo();
        public static readonly KrProcessManagementStageSettingsVirtualSchemeInfo KrProcessManagementStageSettingsVirtual = new KrProcessManagementStageSettingsVirtualSchemeInfo();
        public static readonly KrProcessManagementStageTypeModesSchemeInfo KrProcessManagementStageTypeModes = new KrProcessManagementStageTypeModesSchemeInfo();
        public static readonly KrProcessStageTypesSchemeInfo KrProcessStageTypes = new KrProcessStageTypesSchemeInfo();
        public static readonly KrRegistrationStageSettingsVirtualSchemeInfo KrRegistrationStageSettingsVirtual = new KrRegistrationStageSettingsVirtualSchemeInfo();
        public static readonly KrRequestCommentSchemeInfo KrRequestComment = new KrRequestCommentSchemeInfo();
        public static readonly KrResolutionActionVirtualSchemeInfo KrResolutionActionVirtual = new KrResolutionActionVirtualSchemeInfo();
        public static readonly KrResolutionSettingsVirtualSchemeInfo KrResolutionSettingsVirtual = new KrResolutionSettingsVirtualSchemeInfo();
        public static readonly KrRouteInitializationActionVirtualSchemeInfo KrRouteInitializationActionVirtual = new KrRouteInitializationActionVirtualSchemeInfo();
        public static readonly KrRouteModesSchemeInfo KrRouteModes = new KrRouteModesSchemeInfo();
        public static readonly KrRouteSettingsSchemeInfo KrRouteSettings = new KrRouteSettingsSchemeInfo();
        public static readonly KrSamplePermissionsExtensionSchemeInfo KrSamplePermissionsExtension = new KrSamplePermissionsExtensionSchemeInfo();
        public static readonly KrSecondaryProcessCommonInfoSchemeInfo KrSecondaryProcessCommonInfo = new KrSecondaryProcessCommonInfoSchemeInfo();
        public static readonly KrSecondaryProcessesSchemeInfo KrSecondaryProcesses = new KrSecondaryProcessesSchemeInfo();
        public static readonly KrSecondaryProcessGroupsVirtualSchemeInfo KrSecondaryProcessGroupsVirtual = new KrSecondaryProcessGroupsVirtualSchemeInfo();
        public static readonly KrSecondaryProcessModesSchemeInfo KrSecondaryProcessModes = new KrSecondaryProcessModesSchemeInfo();
        public static readonly KrSecondaryProcessRolesSchemeInfo KrSecondaryProcessRoles = new KrSecondaryProcessRolesSchemeInfo();
        public static readonly KrSettingsSchemeInfo KrSettings = new KrSettingsSchemeInfo();
        public static readonly KrSettingsCardTypesSchemeInfo KrSettingsCardTypes = new KrSettingsCardTypesSchemeInfo();
        public static readonly KrSettingsCycleGroupingSchemeInfo KrSettingsCycleGrouping = new KrSettingsCycleGroupingSchemeInfo();
        public static readonly KrSettingsCycleGroupingStatesSchemeInfo KrSettingsCycleGroupingStates = new KrSettingsCycleGroupingStatesSchemeInfo();
        public static readonly KrSettingsCycleGroupingTypesSchemeInfo KrSettingsCycleGroupingTypes = new KrSettingsCycleGroupingTypesSchemeInfo();
        public static readonly KrSettingsRouteDocTypesSchemeInfo KrSettingsRouteDocTypes = new KrSettingsRouteDocTypesSchemeInfo();
        public static readonly KrSettingsRouteExtraTaskTypesSchemeInfo KrSettingsRouteExtraTaskTypes = new KrSettingsRouteExtraTaskTypesSchemeInfo();
        public static readonly KrSettingsRoutePermissionsSchemeInfo KrSettingsRoutePermissions = new KrSettingsRoutePermissionsSchemeInfo();
        public static readonly KrSettingsRouteRolesSchemeInfo KrSettingsRouteRoles = new KrSettingsRouteRolesSchemeInfo();
        public static readonly KrSettingsRouteStageGroupsSchemeInfo KrSettingsRouteStageGroups = new KrSettingsRouteStageGroupsSchemeInfo();
        public static readonly KrSettingsRouteStageTypesSchemeInfo KrSettingsRouteStageTypes = new KrSettingsRouteStageTypesSchemeInfo();
        public static readonly KrSettingsTaskAuthorSchemeInfo KrSettingsTaskAuthor = new KrSettingsTaskAuthorSchemeInfo();
        public static readonly KrSettingsTaskAuthorReplaceSchemeInfo KrSettingsTaskAuthorReplace = new KrSettingsTaskAuthorReplaceSchemeInfo();
        public static readonly KrSettingsTaskAuthorsSchemeInfo KrSettingsTaskAuthors = new KrSettingsTaskAuthorsSchemeInfo();
        public static readonly KrSigningActionNotificationActionRolesVirtualSchemeInfo KrSigningActionNotificationActionRolesVirtual = new KrSigningActionNotificationActionRolesVirtualSchemeInfo();
        public static readonly KrSigningActionNotificationRolesVirtualSchemeInfo KrSigningActionNotificationRolesVirtual = new KrSigningActionNotificationRolesVirtualSchemeInfo();
        public static readonly KrSigningActionOptionLinksVirtualSchemeInfo KrSigningActionOptionLinksVirtual = new KrSigningActionOptionLinksVirtualSchemeInfo();
        public static readonly KrSigningActionOptionsActionVirtualSchemeInfo KrSigningActionOptionsActionVirtual = new KrSigningActionOptionsActionVirtualSchemeInfo();
        public static readonly KrSigningActionOptionsVirtualSchemeInfo KrSigningActionOptionsVirtual = new KrSigningActionOptionsVirtualSchemeInfo();
        public static readonly KrSigningActionVirtualSchemeInfo KrSigningActionVirtual = new KrSigningActionVirtualSchemeInfo();
        public static readonly KrSigningStageSettingsVirtualSchemeInfo KrSigningStageSettingsVirtual = new KrSigningStageSettingsVirtualSchemeInfo();
        public static readonly KrSigningTaskOptionsSchemeInfo KrSigningTaskOptions = new KrSigningTaskOptionsSchemeInfo();
        public static readonly KrSinglePerformerVirtualSchemeInfo KrSinglePerformerVirtual = new KrSinglePerformerVirtualSchemeInfo();
        public static readonly KrStageCommonMethodsSchemeInfo KrStageCommonMethods = new KrStageCommonMethodsSchemeInfo();
        public static readonly KrStageDocStatesSchemeInfo KrStageDocStates = new KrStageDocStatesSchemeInfo();
        public static readonly KrStageGroupsSchemeInfo KrStageGroups = new KrStageGroupsSchemeInfo();
        public static readonly KrStageGroupTemplatesVirtualSchemeInfo KrStageGroupTemplatesVirtual = new KrStageGroupTemplatesVirtualSchemeInfo();
        public static readonly KrStageRolesSchemeInfo KrStageRoles = new KrStageRolesSchemeInfo();
        public static readonly KrStagesSchemeInfo KrStages = new KrStagesSchemeInfo();
        public static readonly KrStageStateSchemeInfo KrStageState = new KrStageStateSchemeInfo();
        public static readonly KrStagesVirtualSchemeInfo KrStagesVirtual = new KrStagesVirtualSchemeInfo();
        public static readonly KrStageTemplateGroupPositionSchemeInfo KrStageTemplateGroupPosition = new KrStageTemplateGroupPositionSchemeInfo();
        public static readonly KrStageTemplatesSchemeInfo KrStageTemplates = new KrStageTemplatesSchemeInfo();
        public static readonly KrStageTypesSchemeInfo KrStageTypes = new KrStageTypesSchemeInfo();
        public static readonly KrTaskSchemeInfo KrTask = new KrTaskSchemeInfo();
        public static readonly KrTaskCommentVirtualSchemeInfo KrTaskCommentVirtual = new KrTaskCommentVirtualSchemeInfo();
        public static readonly KrTaskKindSettingsVirtualSchemeInfo KrTaskKindSettingsVirtual = new KrTaskKindSettingsVirtualSchemeInfo();
        public static readonly KrTaskRegistrationActionNotificationRolesVitrualSchemeInfo KrTaskRegistrationActionNotificationRolesVitrual = new KrTaskRegistrationActionNotificationRolesVitrualSchemeInfo();
        public static readonly KrTaskRegistrationActionOptionLinksVirtualSchemeInfo KrTaskRegistrationActionOptionLinksVirtual = new KrTaskRegistrationActionOptionLinksVirtualSchemeInfo();
        public static readonly KrTaskRegistrationActionOptionsVirtualSchemeInfo KrTaskRegistrationActionOptionsVirtual = new KrTaskRegistrationActionOptionsVirtualSchemeInfo();
        public static readonly KrTaskRegistrationActionVirtualSchemeInfo KrTaskRegistrationActionVirtual = new KrTaskRegistrationActionVirtualSchemeInfo();
        public static readonly KrTaskTypeConditionSchemeInfo KrTaskTypeCondition = new KrTaskTypeConditionSchemeInfo();
        public static readonly KrTaskTypeConditionSettingsSchemeInfo KrTaskTypeConditionSettings = new KrTaskTypeConditionSettingsSchemeInfo();
        public static readonly KrTypedTaskSettingsVirtualSchemeInfo KrTypedTaskSettingsVirtual = new KrTypedTaskSettingsVirtualSchemeInfo();
        public static readonly KrUniversalTaskActionButtonLinksVirtualSchemeInfo KrUniversalTaskActionButtonLinksVirtual = new KrUniversalTaskActionButtonLinksVirtualSchemeInfo();
        public static readonly KrUniversalTaskActionButtonsVirtualSchemeInfo KrUniversalTaskActionButtonsVirtual = new KrUniversalTaskActionButtonsVirtualSchemeInfo();
        public static readonly KrUniversalTaskActionNotificationRolesVitrualSchemeInfo KrUniversalTaskActionNotificationRolesVitrual = new KrUniversalTaskActionNotificationRolesVitrualSchemeInfo();
        public static readonly KrUniversalTaskActionVirtualSchemeInfo KrUniversalTaskActionVirtual = new KrUniversalTaskActionVirtualSchemeInfo();
        public static readonly KrUniversalTaskOptionsSchemeInfo KrUniversalTaskOptions = new KrUniversalTaskOptionsSchemeInfo();
        public static readonly KrUniversalTaskOptionsSettingsVirtualSchemeInfo KrUniversalTaskOptionsSettingsVirtual = new KrUniversalTaskOptionsSettingsVirtualSchemeInfo();
        public static readonly KrUniversalTaskSettingsVirtualSchemeInfo KrUniversalTaskSettingsVirtual = new KrUniversalTaskSettingsVirtualSchemeInfo();
        public static readonly KrUsersConditionSchemeInfo KrUsersCondition = new KrUsersConditionSchemeInfo();
        public static readonly KrUserSettingsVirtualSchemeInfo KrUserSettingsVirtual = new KrUserSettingsVirtualSchemeInfo();
        public static readonly KrVirtualFileCardTypesSchemeInfo KrVirtualFileCardTypes = new KrVirtualFileCardTypesSchemeInfo();
        public static readonly KrVirtualFileRolesSchemeInfo KrVirtualFileRoles = new KrVirtualFileRolesSchemeInfo();
        public static readonly KrVirtualFilesSchemeInfo KrVirtualFiles = new KrVirtualFilesSchemeInfo();
        public static readonly KrVirtualFileStatesSchemeInfo KrVirtualFileStates = new KrVirtualFileStatesSchemeInfo();
        public static readonly KrVirtualFileVersionsSchemeInfo KrVirtualFileVersions = new KrVirtualFileVersionsSchemeInfo();
        public static readonly KrWeActionCompletionOptionsSchemeInfo KrWeActionCompletionOptions = new KrWeActionCompletionOptionsSchemeInfo();
        public static readonly KrWeAdditionalApprovalOptionsVirtualSchemeInfo KrWeAdditionalApprovalOptionsVirtual = new KrWeAdditionalApprovalOptionsVirtualSchemeInfo();
        public static readonly KrWeEditInterjectOptionsVirtualSchemeInfo KrWeEditInterjectOptionsVirtual = new KrWeEditInterjectOptionsVirtualSchemeInfo();
        public static readonly KrWeRequestCommentOptionsVirtualSchemeInfo KrWeRequestCommentOptionsVirtual = new KrWeRequestCommentOptionsVirtualSchemeInfo();
        public static readonly KrWeRolesVirtualSchemeInfo KrWeRolesVirtual = new KrWeRolesVirtualSchemeInfo();
        public static readonly LanguagesSchemeInfo Languages = new LanguagesSchemeInfo();
        public static readonly LawAdministratorsSchemeInfo LawAdministrators = new LawAdministratorsSchemeInfo();
        public static readonly LawCaseSchemeInfo LawCase = new LawCaseSchemeInfo();
        public static readonly LawClientsSchemeInfo LawClients = new LawClientsSchemeInfo();
        public static readonly LawPartnerRepresentativesSchemeInfo LawPartnerRepresentatives = new LawPartnerRepresentativesSchemeInfo();
        public static readonly LawPartnersSchemeInfo LawPartners = new LawPartnersSchemeInfo();
        public static readonly LawPartnersDialogVirtualSchemeInfo LawPartnersDialogVirtual = new LawPartnersDialogVirtualSchemeInfo();
        public static readonly LawUsersSchemeInfo LawUsers = new LawUsersSchemeInfo();
        public static readonly LicenseTypesSchemeInfo LicenseTypes = new LicenseTypesSchemeInfo();
        public static readonly LicenseVirtualSchemeInfo LicenseVirtual = new LicenseVirtualSchemeInfo();
        public static readonly LocalizationEntriesSchemeInfo LocalizationEntries = new LocalizationEntriesSchemeInfo();
        public static readonly LocalizationLibrariesSchemeInfo LocalizationLibraries = new LocalizationLibrariesSchemeInfo();
        public static readonly LocalizationStringsSchemeInfo LocalizationStrings = new LocalizationStringsSchemeInfo();
        public static readonly LoginTypesSchemeInfo LoginTypes = new LoginTypesSchemeInfo();
        public static readonly MetaRolesSchemeInfo MetaRoles = new MetaRolesSchemeInfo();
        public static readonly MetaRoleTypesSchemeInfo MetaRoleTypes = new MetaRoleTypesSchemeInfo();
        public static readonly MigrationsSchemeInfo Migrations = new MigrationsSchemeInfo();
        public static readonly MobileLicensesSchemeInfo MobileLicenses = new MobileLicensesSchemeInfo();
        public static readonly NestedRolesSchemeInfo NestedRoles = new NestedRolesSchemeInfo();
        public static readonly NotificationsSchemeInfo Notifications = new NotificationsSchemeInfo();
        public static readonly NotificationSubscribeTypesSchemeInfo NotificationSubscribeTypes = new NotificationSubscribeTypesSchemeInfo();
        public static readonly NotificationSubscriptionsSchemeInfo NotificationSubscriptions = new NotificationSubscriptionsSchemeInfo();
        public static readonly NotificationSubscriptionSettingsSchemeInfo NotificationSubscriptionSettings = new NotificationSubscriptionSettingsSchemeInfo();
        public static readonly NotificationTokenRolesSchemeInfo NotificationTokenRoles = new NotificationTokenRolesSchemeInfo();
        public static readonly NotificationTypeCardTypesSchemeInfo NotificationTypeCardTypes = new NotificationTypeCardTypesSchemeInfo();
        public static readonly NotificationTypesSchemeInfo NotificationTypes = new NotificationTypesSchemeInfo();
        public static readonly NotificationUnsubscribeTypesSchemeInfo NotificationUnsubscribeTypes = new NotificationUnsubscribeTypesSchemeInfo();
        public static readonly OcrLanguagesSchemeInfo OcrLanguages = new OcrLanguagesSchemeInfo();
        public static readonly OcrMappingComplexFieldsSchemeInfo OcrMappingComplexFields = new OcrMappingComplexFieldsSchemeInfo();
        public static readonly OcrMappingFieldsSchemeInfo OcrMappingFields = new OcrMappingFieldsSchemeInfo();
        public static readonly OcrMappingSettingsFieldsSchemeInfo OcrMappingSettingsFields = new OcrMappingSettingsFieldsSchemeInfo();
        public static readonly OcrMappingSettingsSectionsSchemeInfo OcrMappingSettingsSections = new OcrMappingSettingsSectionsSchemeInfo();
        public static readonly OcrMappingSettingsTypesSchemeInfo OcrMappingSettingsTypes = new OcrMappingSettingsTypesSchemeInfo();
        public static readonly OcrMappingSettingsVirtualSchemeInfo OcrMappingSettingsVirtual = new OcrMappingSettingsVirtualSchemeInfo();
        public static readonly OcrOperationsSchemeInfo OcrOperations = new OcrOperationsSchemeInfo();
        public static readonly OcrPatternTypesSchemeInfo OcrPatternTypes = new OcrPatternTypesSchemeInfo();
        public static readonly OcrRecognitionModesSchemeInfo OcrRecognitionModes = new OcrRecognitionModesSchemeInfo();
        public static readonly OcrRequestsSchemeInfo OcrRequests = new OcrRequestsSchemeInfo();
        public static readonly OcrRequestsLanguagesSchemeInfo OcrRequestsLanguages = new OcrRequestsLanguagesSchemeInfo();
        public static readonly OcrRequestsStatesSchemeInfo OcrRequestsStates = new OcrRequestsStatesSchemeInfo();
        public static readonly OcrResultsVirtualSchemeInfo OcrResultsVirtual = new OcrResultsVirtualSchemeInfo();
        public static readonly OcrSegmentationModesSchemeInfo OcrSegmentationModes = new OcrSegmentationModesSchemeInfo();
        public static readonly OcrSettingsSchemeInfo OcrSettings = new OcrSettingsSchemeInfo();
        public static readonly OcrSettingsPatternsSchemeInfo OcrSettingsPatterns = new OcrSettingsPatternsSchemeInfo();
        public static readonly OnlyOfficeFileCacheSchemeInfo OnlyOfficeFileCache = new OnlyOfficeFileCacheSchemeInfo();
        public static readonly OnlyOfficeSettingsSchemeInfo OnlyOfficeSettings = new OnlyOfficeSettingsSchemeInfo();
        public static readonly OperationsSchemeInfo Operations = new OperationsSchemeInfo();
        public static readonly OperationStatesSchemeInfo OperationStates = new OperationStatesSchemeInfo();
        public static readonly OperationsVirtualSchemeInfo OperationsVirtual = new OperationsVirtualSchemeInfo();
        public static readonly OperationTypesSchemeInfo OperationTypes = new OperationTypesSchemeInfo();
        public static readonly OperationUpdatesSchemeInfo OperationUpdates = new OperationUpdatesSchemeInfo();
        public static readonly OutboxSchemeInfo Outbox = new OutboxSchemeInfo();
        public static readonly OutgoingRefDocsSchemeInfo OutgoingRefDocs = new OutgoingRefDocsSchemeInfo();
        public static readonly PartitionsSchemeInfo Partitions = new PartitionsSchemeInfo();
        public static readonly PartnersSchemeInfo Partners = new PartnersSchemeInfo();
        public static readonly PartnersContactsSchemeInfo PartnersContacts = new PartnersContactsSchemeInfo();
        public static readonly PartnersTypesSchemeInfo PartnersTypes = new PartnersTypesSchemeInfo();
        public static readonly PerformersSchemeInfo Performers = new PerformersSchemeInfo();
        public static readonly PersonalLicensesSchemeInfo PersonalLicenses = new PersonalLicensesSchemeInfo();
        public static readonly PersonalRoleDepartmentsVirtualSchemeInfo PersonalRoleDepartmentsVirtual = new PersonalRoleDepartmentsVirtualSchemeInfo();
        public static readonly PersonalRoleNotificationRulesVirtualSchemeInfo PersonalRoleNotificationRulesVirtual = new PersonalRoleNotificationRulesVirtualSchemeInfo();
        public static readonly PersonalRoleNotificationRuleTypesVirtualSchemeInfo PersonalRoleNotificationRuleTypesVirtual = new PersonalRoleNotificationRuleTypesVirtualSchemeInfo();
        public static readonly PersonalRoleRolesVirtualSchemeInfo PersonalRoleRolesVirtual = new PersonalRoleRolesVirtualSchemeInfo();
        public static readonly PersonalRolesSchemeInfo PersonalRoles = new PersonalRolesSchemeInfo();
        public static readonly PersonalRoleSatelliteSchemeInfo PersonalRoleSatellite = new PersonalRoleSatelliteSchemeInfo();
        public static readonly PersonalRoleStaticRolesVirtualSchemeInfo PersonalRoleStaticRolesVirtual = new PersonalRoleStaticRolesVirtualSchemeInfo();
        public static readonly PersonalRoleSubscribedTypesVirtualSchemeInfo PersonalRoleSubscribedTypesVirtual = new PersonalRoleSubscribedTypesVirtualSchemeInfo();
        public static readonly PersonalRolesVirtualSchemeInfo PersonalRolesVirtual = new PersonalRolesVirtualSchemeInfo();
        public static readonly PersonalRoleUnsubscibedTypesVirtualSchemeInfo PersonalRoleUnsubscibedTypesVirtual = new PersonalRoleUnsubscibedTypesVirtualSchemeInfo();
        public static readonly ProceduresSchemeInfo Procedures = new ProceduresSchemeInfo();
        public static readonly ProtocolDecisionsSchemeInfo ProtocolDecisions = new ProtocolDecisionsSchemeInfo();
        public static readonly ProtocolReportsSchemeInfo ProtocolReports = new ProtocolReportsSchemeInfo();
        public static readonly ProtocolResponsiblesSchemeInfo ProtocolResponsibles = new ProtocolResponsiblesSchemeInfo();
        public static readonly ProtocolsSchemeInfo Protocols = new ProtocolsSchemeInfo();
        public static readonly RecipientsSchemeInfo Recipients = new RecipientsSchemeInfo();
        public static readonly ReportRolesActiveSchemeInfo ReportRolesActive = new ReportRolesActiveSchemeInfo();
        public static readonly ReportRolesPassiveSchemeInfo ReportRolesPassive = new ReportRolesPassiveSchemeInfo();
        public static readonly ReportRolesRulesSchemeInfo ReportRolesRules = new ReportRolesRulesSchemeInfo();
        public static readonly RoleDeputiesSchemeInfo RoleDeputies = new RoleDeputiesSchemeInfo();
        public static readonly RoleDeputiesManagementSchemeInfo RoleDeputiesManagement = new RoleDeputiesManagementSchemeInfo();
        public static readonly RoleDeputiesManagementAccessSchemeInfo RoleDeputiesManagementAccess = new RoleDeputiesManagementAccessSchemeInfo();
        public static readonly RoleDeputiesManagementDeputizedRolesVirtualSchemeInfo RoleDeputiesManagementDeputizedRolesVirtual = new RoleDeputiesManagementDeputizedRolesVirtualSchemeInfo();
        public static readonly RoleDeputiesManagementDeputizedVirtualSchemeInfo RoleDeputiesManagementDeputizedVirtual = new RoleDeputiesManagementDeputizedVirtualSchemeInfo();
        public static readonly RoleDeputiesManagementHelperVirtualSchemeInfo RoleDeputiesManagementHelperVirtual = new RoleDeputiesManagementHelperVirtualSchemeInfo();
        public static readonly RoleDeputiesManagementRolesSchemeInfo RoleDeputiesManagementRoles = new RoleDeputiesManagementRolesSchemeInfo();
        public static readonly RoleDeputiesManagementRolesVirtualSchemeInfo RoleDeputiesManagementRolesVirtual = new RoleDeputiesManagementRolesVirtualSchemeInfo();
        public static readonly RoleDeputiesManagementUsersSchemeInfo RoleDeputiesManagementUsers = new RoleDeputiesManagementUsersSchemeInfo();
        public static readonly RoleDeputiesManagementUsersVirtualSchemeInfo RoleDeputiesManagementUsersVirtual = new RoleDeputiesManagementUsersVirtualSchemeInfo();
        public static readonly RoleDeputiesManagementVirtualSchemeInfo RoleDeputiesManagementVirtual = new RoleDeputiesManagementVirtualSchemeInfo();
        public static readonly RoleDeputiesNestedManagementSchemeInfo RoleDeputiesNestedManagement = new RoleDeputiesNestedManagementSchemeInfo();
        public static readonly RoleDeputiesNestedManagementTypesSchemeInfo RoleDeputiesNestedManagementTypes = new RoleDeputiesNestedManagementTypesSchemeInfo();
        public static readonly RoleDeputiesNestedManagementTypesVirtualSchemeInfo RoleDeputiesNestedManagementTypesVirtual = new RoleDeputiesNestedManagementTypesVirtualSchemeInfo();
        public static readonly RoleDeputiesNestedManagementUsersSchemeInfo RoleDeputiesNestedManagementUsers = new RoleDeputiesNestedManagementUsersSchemeInfo();
        public static readonly RoleDeputiesNestedManagementUsersVirtualSchemeInfo RoleDeputiesNestedManagementUsersVirtual = new RoleDeputiesNestedManagementUsersVirtualSchemeInfo();
        public static readonly RoleDeputiesNestedManagementVirtualSchemeInfo RoleDeputiesNestedManagementVirtual = new RoleDeputiesNestedManagementVirtualSchemeInfo();
        public static readonly RoleGeneratorsSchemeInfo RoleGenerators = new RoleGeneratorsSchemeInfo();
        public static readonly RolesSchemeInfo Roles = new RolesSchemeInfo();
        public static readonly RoleTypesSchemeInfo RoleTypes = new RoleTypesSchemeInfo();
        public static readonly RoleUsersSchemeInfo RoleUsers = new RoleUsersSchemeInfo();
        public static readonly RoleUsersVirtualSchemeInfo RoleUsersVirtual = new RoleUsersVirtualSchemeInfo();
        public static readonly SatellitesSchemeInfo Satellites = new SatellitesSchemeInfo();
        public static readonly SchedulingTypesSchemeInfo SchedulingTypes = new SchedulingTypesSchemeInfo();
        public static readonly SchemeSchemeInfo Scheme = new SchemeSchemeInfo();
        public static readonly SearchQueriesSchemeInfo SearchQueries = new SearchQueriesSchemeInfo();
        public static readonly SectionChangedConditionSchemeInfo SectionChangedCondition = new SectionChangedConditionSchemeInfo();
        public static readonly SequencesInfoSchemeInfo SequencesInfo = new SequencesInfoSchemeInfo();
        public static readonly SequencesIntervalsSchemeInfo SequencesIntervals = new SequencesIntervalsSchemeInfo();
        public static readonly SequencesReservedSchemeInfo SequencesReserved = new SequencesReservedSchemeInfo();
        public static readonly ServerInstancesSchemeInfo ServerInstances = new ServerInstancesSchemeInfo();
        public static readonly SessionActivitySchemeInfo SessionActivity = new SessionActivitySchemeInfo();
        public static readonly SessionsSchemeInfo Sessions = new SessionsSchemeInfo();
        public static readonly SessionServiceTypesSchemeInfo SessionServiceTypes = new SessionServiceTypesSchemeInfo();
        public static readonly SignatureCertificateSettingsSchemeInfo SignatureCertificateSettings = new SignatureCertificateSettingsSchemeInfo();
        public static readonly SignatureDigestAlgorithmsSchemeInfo SignatureDigestAlgorithms = new SignatureDigestAlgorithmsSchemeInfo();
        public static readonly SignatureEncryptDigestSettingsSchemeInfo SignatureEncryptDigestSettings = new SignatureEncryptDigestSettingsSchemeInfo();
        public static readonly SignatureEncryptionAlgorithmsSchemeInfo SignatureEncryptionAlgorithms = new SignatureEncryptionAlgorithmsSchemeInfo();
        public static readonly SignatureManagerVirtualSchemeInfo SignatureManagerVirtual = new SignatureManagerVirtualSchemeInfo();
        public static readonly SignaturePackagingsSchemeInfo SignaturePackagings = new SignaturePackagingsSchemeInfo();
        public static readonly SignatureProfilesSchemeInfo SignatureProfiles = new SignatureProfilesSchemeInfo();
        public static readonly SignatureSettingsSchemeInfo SignatureSettings = new SignatureSettingsSchemeInfo();
        public static readonly SignatureTypesSchemeInfo SignatureTypes = new SignatureTypesSchemeInfo();
        public static readonly SmartRoleGeneratorInfoSchemeInfo SmartRoleGeneratorInfo = new SmartRoleGeneratorInfoSchemeInfo();
        public static readonly SmartRoleGeneratorsSchemeInfo SmartRoleGenerators = new SmartRoleGeneratorsSchemeInfo();
        public static readonly SmartRoleMembersSchemeInfo SmartRoleMembers = new SmartRoleMembersSchemeInfo();
        public static readonly SmartRolesSchemeInfo SmartRoles = new SmartRolesSchemeInfo();
        public static readonly TablesSchemeInfo Tables = new TablesSchemeInfo();
        public static readonly TagCardsSchemeInfo TagCards = new TagCardsSchemeInfo();
        public static readonly TagConditionSchemeInfo TagCondition = new TagConditionSchemeInfo();
        public static readonly TagEditorsSchemeInfo TagEditors = new TagEditorsSchemeInfo();
        public static readonly TagsSchemeInfo Tags = new TagsSchemeInfo();
        public static readonly TagSharedWithSchemeInfo TagSharedWith = new TagSharedWithSchemeInfo();
        public static readonly TagsUserSettingsVirtualSchemeInfo TagsUserSettingsVirtual = new TagsUserSettingsVirtualSchemeInfo();
        public static readonly TaskAssignedRolesSchemeInfo TaskAssignedRoles = new TaskAssignedRolesSchemeInfo();
        public static readonly TaskCommonInfoSchemeInfo TaskCommonInfo = new TaskCommonInfoSchemeInfo();
        public static readonly TaskConditionCompletionOptionsSchemeInfo TaskConditionCompletionOptions = new TaskConditionCompletionOptionsSchemeInfo();
        public static readonly TaskConditionFunctionRolesSchemeInfo TaskConditionFunctionRoles = new TaskConditionFunctionRolesSchemeInfo();
        public static readonly TaskConditionSettingsSchemeInfo TaskConditionSettings = new TaskConditionSettingsSchemeInfo();
        public static readonly TaskConditionTaskKindsSchemeInfo TaskConditionTaskKinds = new TaskConditionTaskKindsSchemeInfo();
        public static readonly TaskConditionTaskTypesSchemeInfo TaskConditionTaskTypes = new TaskConditionTaskTypesSchemeInfo();
        public static readonly TaskHistorySchemeInfo TaskHistory = new TaskHistorySchemeInfo();
        public static readonly TaskHistoryGroupsSchemeInfo TaskHistoryGroups = new TaskHistoryGroupsSchemeInfo();
        public static readonly TaskHistoryGroupTypesSchemeInfo TaskHistoryGroupTypes = new TaskHistoryGroupTypesSchemeInfo();
        public static readonly TaskKindsSchemeInfo TaskKinds = new TaskKindsSchemeInfo();
        public static readonly TasksSchemeInfo Tasks = new TasksSchemeInfo();
        public static readonly TaskStatesSchemeInfo TaskStates = new TaskStatesSchemeInfo();
        public static readonly TemplateEditRolesSchemeInfo TemplateEditRoles = new TemplateEditRolesSchemeInfo();
        public static readonly TemplateFilesSchemeInfo TemplateFiles = new TemplateFilesSchemeInfo();
        public static readonly TemplateOpenRolesSchemeInfo TemplateOpenRoles = new TemplateOpenRolesSchemeInfo();
        public static readonly TemplatesSchemeInfo Templates = new TemplatesSchemeInfo();
        public static readonly TemplatesVirtualSchemeInfo TemplatesVirtual = new TemplatesVirtualSchemeInfo();
        public static readonly TEST_CarAdditionalInfoSchemeInfo TEST_CarAdditionalInfo = new TEST_CarAdditionalInfoSchemeInfo();
        public static readonly TEST_CarCustomersSchemeInfo TEST_CarCustomers = new TEST_CarCustomersSchemeInfo();
        public static readonly TEST_CarMainInfoSchemeInfo TEST_CarMainInfo = new TEST_CarMainInfoSchemeInfo();
        public static readonly TEST_CarOwnersSchemeInfo TEST_CarOwners = new TEST_CarOwnersSchemeInfo();
        public static readonly TEST_CarSalesSchemeInfo TEST_CarSales = new TEST_CarSalesSchemeInfo();
        public static readonly TEST_CustomerOperationsSchemeInfo TEST_CustomerOperations = new TEST_CustomerOperationsSchemeInfo();
        public static readonly TileSizesSchemeInfo TileSizes = new TileSizesSchemeInfo();
        public static readonly TimeZonesSchemeInfo TimeZones = new TimeZonesSchemeInfo();
        public static readonly TimeZonesSettingsSchemeInfo TimeZonesSettings = new TimeZonesSettingsSchemeInfo();
        public static readonly TimeZonesVirtualSchemeInfo TimeZonesVirtual = new TimeZonesVirtualSchemeInfo();
        public static readonly TypesSchemeInfo Types = new TypesSchemeInfo();
        public static readonly UserSettingsFunctionRolesVirtualSchemeInfo UserSettingsFunctionRolesVirtual = new UserSettingsFunctionRolesVirtualSchemeInfo();
        public static readonly UserSettingsVirtualSchemeInfo UserSettingsVirtual = new UserSettingsVirtualSchemeInfo();
        public static readonly VatTypesSchemeInfo VatTypes = new VatTypesSchemeInfo();
        public static readonly ViewRolesSchemeInfo ViewRoles = new ViewRolesSchemeInfo();
        public static readonly ViewRolesVirtualSchemeInfo ViewRolesVirtual = new ViewRolesVirtualSchemeInfo();
        public static readonly ViewsSchemeInfo Views = new ViewsSchemeInfo();
        public static readonly ViewsVirtualSchemeInfo ViewsVirtual = new ViewsVirtualSchemeInfo();
        public static readonly WeAddFileFromTemplateActionSchemeInfo WeAddFileFromTemplateAction = new WeAddFileFromTemplateActionSchemeInfo();
        public static readonly WebApplicationsSchemeInfo WebApplications = new WebApplicationsSchemeInfo();
        public static readonly WebClientRolesSchemeInfo WebClientRoles = new WebClientRolesSchemeInfo();
        public static readonly WeCommandActionSchemeInfo WeCommandAction = new WeCommandActionSchemeInfo();
        public static readonly WeCommandActionLinksSchemeInfo WeCommandActionLinks = new WeCommandActionLinksSchemeInfo();
        public static readonly WeConditionActionSchemeInfo WeConditionAction = new WeConditionActionSchemeInfo();
        public static readonly WeDialogActionSchemeInfo WeDialogAction = new WeDialogActionSchemeInfo();
        public static readonly WeDialogActionButtonLinksSchemeInfo WeDialogActionButtonLinks = new WeDialogActionButtonLinksSchemeInfo();
        public static readonly WeDialogActionButtonsSchemeInfo WeDialogActionButtons = new WeDialogActionButtonsSchemeInfo();
        public static readonly WeEmailActionSchemeInfo WeEmailAction = new WeEmailActionSchemeInfo();
        public static readonly WeEmailActionOptionalRecipientsSchemeInfo WeEmailActionOptionalRecipients = new WeEmailActionOptionalRecipientsSchemeInfo();
        public static readonly WeEmailActionRecieversSchemeInfo WeEmailActionRecievers = new WeEmailActionRecieversSchemeInfo();
        public static readonly WeEndActionSchemeInfo WeEndAction = new WeEndActionSchemeInfo();
        public static readonly WeHistoryManagementActionSchemeInfo WeHistoryManagementAction = new WeHistoryManagementActionSchemeInfo();
        public static readonly WeScriptActionSchemeInfo WeScriptAction = new WeScriptActionSchemeInfo();
        public static readonly WeSendSignalActionSchemeInfo WeSendSignalAction = new WeSendSignalActionSchemeInfo();
        public static readonly WeStartActionSchemeInfo WeStartAction = new WeStartActionSchemeInfo();
        public static readonly WeSubprocessActionSchemeInfo WeSubprocessAction = new WeSubprocessActionSchemeInfo();
        public static readonly WeSubprocessActionEndMappingSchemeInfo WeSubprocessActionEndMapping = new WeSubprocessActionEndMappingSchemeInfo();
        public static readonly WeSubprocessActionOptionsSchemeInfo WeSubprocessActionOptions = new WeSubprocessActionOptionsSchemeInfo();
        public static readonly WeSubprocessActionStartMappingSchemeInfo WeSubprocessActionStartMapping = new WeSubprocessActionStartMappingSchemeInfo();
        public static readonly WeSubprocessControlActionSchemeInfo WeSubprocessControlAction = new WeSubprocessControlActionSchemeInfo();
        public static readonly WeTaskActionSchemeInfo WeTaskAction = new WeTaskActionSchemeInfo();
        public static readonly WeTaskActionDialogsSchemeInfo WeTaskActionDialogs = new WeTaskActionDialogsSchemeInfo();
        public static readonly WeTaskActionEventsSchemeInfo WeTaskActionEvents = new WeTaskActionEventsSchemeInfo();
        public static readonly WeTaskActionNotificationRolesSchemeInfo WeTaskActionNotificationRoles = new WeTaskActionNotificationRolesSchemeInfo();
        public static readonly WeTaskActionOptionLinksSchemeInfo WeTaskActionOptionLinks = new WeTaskActionOptionLinksSchemeInfo();
        public static readonly WeTaskActionOptionsSchemeInfo WeTaskActionOptions = new WeTaskActionOptionsSchemeInfo();
        public static readonly WeTaskControlActionSchemeInfo WeTaskControlAction = new WeTaskControlActionSchemeInfo();
        public static readonly WeTaskControlTypesSchemeInfo WeTaskControlTypes = new WeTaskControlTypesSchemeInfo();
        public static readonly WeTaskGroupActionSchemeInfo WeTaskGroupAction = new WeTaskGroupActionSchemeInfo();
        public static readonly WeTaskGroupActionOptionLinksSchemeInfo WeTaskGroupActionOptionLinks = new WeTaskGroupActionOptionLinksSchemeInfo();
        public static readonly WeTaskGroupActionOptionsSchemeInfo WeTaskGroupActionOptions = new WeTaskGroupActionOptionsSchemeInfo();
        public static readonly WeTaskGroupActionOptionTypesSchemeInfo WeTaskGroupActionOptionTypes = new WeTaskGroupActionOptionTypesSchemeInfo();
        public static readonly WeTaskGroupActionRolesSchemeInfo WeTaskGroupActionRoles = new WeTaskGroupActionRolesSchemeInfo();
        public static readonly WeTaskGroupControlActionSchemeInfo WeTaskGroupControlAction = new WeTaskGroupControlActionSchemeInfo();
        public static readonly WeTimerActionSchemeInfo WeTimerAction = new WeTimerActionSchemeInfo();
        public static readonly WeTimerControlActionSchemeInfo WeTimerControlAction = new WeTimerControlActionSchemeInfo();
        public static readonly WfResolutionChildrenSchemeInfo WfResolutionChildren = new WfResolutionChildrenSchemeInfo();
        public static readonly WfResolutionChildrenVirtualSchemeInfo WfResolutionChildrenVirtual = new WfResolutionChildrenVirtualSchemeInfo();
        public static readonly WfResolutionPerformersSchemeInfo WfResolutionPerformers = new WfResolutionPerformersSchemeInfo();
        public static readonly WfResolutionsSchemeInfo WfResolutions = new WfResolutionsSchemeInfo();
        public static readonly WfResolutionsVirtualSchemeInfo WfResolutionsVirtual = new WfResolutionsVirtualSchemeInfo();
        public static readonly WfSatelliteSchemeInfo WfSatellite = new WfSatelliteSchemeInfo();
        public static readonly WfSatelliteTaskHistorySchemeInfo WfSatelliteTaskHistory = new WfSatelliteTaskHistorySchemeInfo();
        public static readonly WfTaskCardsVirtualSchemeInfo WfTaskCardsVirtual = new WfTaskCardsVirtualSchemeInfo();
        public static readonly WorkflowActionsSchemeInfo WorkflowActions = new WorkflowActionsSchemeInfo();
        public static readonly WorkflowCountersSchemeInfo WorkflowCounters = new WorkflowCountersSchemeInfo();
        public static readonly WorkflowDefaultSubscriptionsSchemeInfo WorkflowDefaultSubscriptions = new WorkflowDefaultSubscriptionsSchemeInfo();
        public static readonly WorkflowEngineCheckContextRoleSchemeInfo WorkflowEngineCheckContextRole = new WorkflowEngineCheckContextRoleSchemeInfo();
        public static readonly WorkflowEngineCommandSubscriptionsSchemeInfo WorkflowEngineCommandSubscriptions = new WorkflowEngineCommandSubscriptionsSchemeInfo();
        public static readonly WorkflowEngineErrorsSchemeInfo WorkflowEngineErrors = new WorkflowEngineErrorsSchemeInfo();
        public static readonly WorkflowEngineLogLevelsSchemeInfo WorkflowEngineLogLevels = new WorkflowEngineLogLevelsSchemeInfo();
        public static readonly WorkflowEngineLogsSchemeInfo WorkflowEngineLogs = new WorkflowEngineLogsSchemeInfo();
        public static readonly WorkflowEngineNodesSchemeInfo WorkflowEngineNodes = new WorkflowEngineNodesSchemeInfo();
        public static readonly WorkflowEngineProcessesSchemeInfo WorkflowEngineProcesses = new WorkflowEngineProcessesSchemeInfo();
        public static readonly WorkflowEngineSettingsAdminRolesSchemeInfo WorkflowEngineSettingsAdminRoles = new WorkflowEngineSettingsAdminRolesSchemeInfo();
        public static readonly WorkflowEngineSettingsCreateRolesSchemeInfo WorkflowEngineSettingsCreateRoles = new WorkflowEngineSettingsCreateRolesSchemeInfo();
        public static readonly WorkflowEngineSettingsObjectTypeFieldsSchemeInfo WorkflowEngineSettingsObjectTypeFields = new WorkflowEngineSettingsObjectTypeFieldsSchemeInfo();
        public static readonly WorkflowEngineSettingsObjectTypesSchemeInfo WorkflowEngineSettingsObjectTypes = new WorkflowEngineSettingsObjectTypesSchemeInfo();
        public static readonly WorkflowEngineSubprocessSubscriptionsSchemeInfo WorkflowEngineSubprocessSubscriptions = new WorkflowEngineSubprocessSubscriptionsSchemeInfo();
        public static readonly WorkflowEngineTaskActionsSchemeInfo WorkflowEngineTaskActions = new WorkflowEngineTaskActionsSchemeInfo();
        public static readonly WorkflowEngineTaskSubscriptionsSchemeInfo WorkflowEngineTaskSubscriptions = new WorkflowEngineTaskSubscriptionsSchemeInfo();
        public static readonly WorkflowEngineTimerSubscriptionsSchemeInfo WorkflowEngineTimerSubscriptions = new WorkflowEngineTimerSubscriptionsSchemeInfo();
        public static readonly WorkflowInLinksSchemeInfo WorkflowInLinks = new WorkflowInLinksSchemeInfo();
        public static readonly WorkflowLinkModesSchemeInfo WorkflowLinkModes = new WorkflowLinkModesSchemeInfo();
        public static readonly WorkflowLinksSchemeInfo WorkflowLinks = new WorkflowLinksSchemeInfo();
        public static readonly WorkflowMainSchemeInfo WorkflowMain = new WorkflowMainSchemeInfo();
        public static readonly WorkflowNodeInstancesSchemeInfo WorkflowNodeInstances = new WorkflowNodeInstancesSchemeInfo();
        public static readonly WorkflowNodeInstanceSubprocessesSchemeInfo WorkflowNodeInstanceSubprocesses = new WorkflowNodeInstanceSubprocessesSchemeInfo();
        public static readonly WorkflowNodeInstanceTasksSchemeInfo WorkflowNodeInstanceTasks = new WorkflowNodeInstanceTasksSchemeInfo();
        public static readonly WorkflowOutLinksSchemeInfo WorkflowOutLinks = new WorkflowOutLinksSchemeInfo();
        public static readonly WorkflowPreConditionsSchemeInfo WorkflowPreConditions = new WorkflowPreConditionsSchemeInfo();
        public static readonly WorkflowProcessErrorsVirtualSchemeInfo WorkflowProcessErrorsVirtual = new WorkflowProcessErrorsVirtualSchemeInfo();
        public static readonly WorkflowProcessesSchemeInfo WorkflowProcesses = new WorkflowProcessesSchemeInfo();
        public static readonly WorkflowSignalProcessingModesSchemeInfo WorkflowSignalProcessingModes = new WorkflowSignalProcessingModesSchemeInfo();
        public static readonly WorkflowSignalTypesSchemeInfo WorkflowSignalTypes = new WorkflowSignalTypesSchemeInfo();
        public static readonly WorkflowTasksSchemeInfo WorkflowTasks = new WorkflowTasksSchemeInfo();
        public static readonly WorkplaceRolesSchemeInfo WorkplaceRoles = new WorkplaceRolesSchemeInfo();
        public static readonly WorkplaceRolesVirtualSchemeInfo WorkplaceRolesVirtual = new WorkplaceRolesVirtualSchemeInfo();
        public static readonly WorkplacesSchemeInfo Workplaces = new WorkplacesSchemeInfo();
        public static readonly WorkplacesVirtualSchemeInfo WorkplacesVirtual = new WorkplacesVirtualSchemeInfo();

        #endregion
    }
}