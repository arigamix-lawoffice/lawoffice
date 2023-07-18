








using System;

namespace Tessa.Extensions.Shared.Info
{// ReSharper disable InconsistentNaming
    #region ViewObject

    public struct ViewObject
    {
        public ViewObject(int id, string alias, string caption = null)
        {
            this.Id = id;
            this.Alias = alias;
            this.Caption = caption ?? string.Empty;
        }

        public int Id { get; }
        public string Alias { get; }
        public string Caption { get; }

        public static implicit operator string(ViewObject obj) => obj.ToString();

        public override string ToString() => this.Alias;
    }

    #endregion

    #region AccessLevels

    /// <summary>
    ///     ID: {55fe2b54-d61b-4b02-9737-3619cfbfd962}
    ///     Alias: AccessLevels
    ///     Caption: $Views_Names_AccessLevels
    ///     Group: System
    /// </summary>
    public class AccessLevelsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "AccessLevels": {55fe2b54-d61b-4b02-9737-3619cfbfd962}.
        /// </summary>
        public readonly Guid ID = new Guid(0x55fe2b54,0xd61b,0x4b02,0x97,0x37,0x36,0x19,0xcf,0xbf,0xd9,0x62);

        /// <summary>
        ///     View name for "AccessLevels".
        /// </summary>
        public readonly string Alias = "AccessLevels";

        /// <summary>
        ///     View caption for "AccessLevels".
        /// </summary>
        public readonly string Caption = "$Views_Names_AccessLevels";

        /// <summary>
        ///     View group for "AccessLevels".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: $Views_AccessLevels_Name.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "$Views_AccessLevels_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_AccessLevels_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_AccessLevels_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(AccessLevelsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region AclForCard

    /// <summary>
    ///     ID: {4e53c550-1954-457a-95a6-4b23c3452fb4}
    ///     Alias: AclForCard
    ///     Caption: $Views_Names_AclForCard
    ///     Group: Acl
    /// </summary>
    public class AclForCardViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "AclForCard": {4e53c550-1954-457a-95a6-4b23c3452fb4}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4e53c550,0x1954,0x457a,0x95,0xa6,0x4b,0x23,0xc3,0x45,0x2f,0xb4);

        /// <summary>
        ///     View name for "AclForCard".
        /// </summary>
        public readonly string Alias = "AclForCard";

        /// <summary>
        ///     View caption for "AclForCard".
        /// </summary>
        public readonly string Caption = "$Views_Names_AclForCard";

        /// <summary>
        ///     View group for "AclForCard".
        /// </summary>
        public readonly string Group = "Acl";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RoleID.
        /// </summary>
        public readonly ViewObject ColumnRoleID = new ViewObject(0, "RoleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleName
        ///     Caption: $Views_AclForCard_RoleName.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(1, "RoleName", "$Views_AclForCard_RoleName");

        /// <summary>
        ///     ID:2 
        ///     Alias: RoleType
        ///     Caption: $Views_AclForCard_RoleType.
        /// </summary>
        public readonly ViewObject ColumnRoleType = new ViewObject(2, "RoleType", "$Views_AclForCard_RoleType");

        /// <summary>
        ///     ID:3 
        ///     Alias: RuleID.
        /// </summary>
        public readonly ViewObject ColumnRuleID = new ViewObject(3, "RuleID");

        /// <summary>
        ///     ID:4 
        ///     Alias: RuleName
        ///     Caption: $Views_AclForCard_RuleName.
        /// </summary>
        public readonly ViewObject ColumnRuleName = new ViewObject(4, "RuleName", "$Views_AclForCard_RuleName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CardID
        ///     Caption: $Views_AclForCard_CardID.
        /// </summary>
        public readonly ViewObject ParamCardID = new ViewObject(0, "CardID", "$Views_AclForCard_CardID");

        #endregion

        #region ToString

        public static implicit operator string(AclForCardViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region AclGenerationRuleExtensions

    /// <summary>
    ///     ID: {480df66f-bb9f-4a16-b26c-d1f358c80a4a}
    ///     Alias: AclGenerationRuleExtensions
    ///     Caption: $Views_Names_AclGenerationRuleExtensions
    ///     Group: Acl
    /// </summary>
    public class AclGenerationRuleExtensionsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "AclGenerationRuleExtensions": {480df66f-bb9f-4a16-b26c-d1f358c80a4a}.
        /// </summary>
        public readonly Guid ID = new Guid(0x480df66f,0xbb9f,0x4a16,0xb2,0x6c,0xd1,0xf3,0x58,0xc8,0x0a,0x4a);

        /// <summary>
        ///     View name for "AclGenerationRuleExtensions".
        /// </summary>
        public readonly string Alias = "AclGenerationRuleExtensions";

        /// <summary>
        ///     View caption for "AclGenerationRuleExtensions".
        /// </summary>
        public readonly string Caption = "$Views_Names_AclGenerationRuleExtensions";

        /// <summary>
        ///     View group for "AclGenerationRuleExtensions".
        /// </summary>
        public readonly string Group = "Acl";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ExtensionID.
        /// </summary>
        public readonly ViewObject ColumnExtensionID = new ViewObject(0, "ExtensionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ExtensionName
        ///     Caption: $Views_AclGenerationRuleExtensions_Name.
        /// </summary>
        public readonly ViewObject ColumnExtensionName = new ViewObject(1, "ExtensionName", "$Views_AclGenerationRuleExtensions_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_AclGenerationRuleExtensions_Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_AclGenerationRuleExtensions_Name");

        #endregion

        #region ToString

        public static implicit operator string(AclGenerationRuleExtensionsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region AclGenerationRules

    /// <summary>
    ///     ID: {edf05d46-215d-4c33-826a-568e626f60c6}
    ///     Alias: AclGenerationRules
    ///     Caption: $Views_Names_AclGenerationRules
    ///     Group: Acl
    /// </summary>
    public class AclGenerationRulesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "AclGenerationRules": {edf05d46-215d-4c33-826a-568e626f60c6}.
        /// </summary>
        public readonly Guid ID = new Guid(0xedf05d46,0x215d,0x4c33,0x82,0x6a,0x56,0x8e,0x62,0x6f,0x60,0xc6);

        /// <summary>
        ///     View name for "AclGenerationRules".
        /// </summary>
        public readonly string Alias = "AclGenerationRules";

        /// <summary>
        ///     View caption for "AclGenerationRules".
        /// </summary>
        public readonly string Caption = "$Views_Names_AclGenerationRules";

        /// <summary>
        ///     View group for "AclGenerationRules".
        /// </summary>
        public readonly string Group = "Acl";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: AclGenerationRuleID.
        /// </summary>
        public readonly ViewObject ColumnAclGenerationRuleID = new ViewObject(0, "AclGenerationRuleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: AclGenerationRuleName
        ///     Caption: $Views_AclGenerationRules_Name.
        /// </summary>
        public readonly ViewObject ColumnAclGenerationRuleName = new ViewObject(1, "AclGenerationRuleName", "$Views_AclGenerationRules_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: AclGenerationRuleTypes
        ///     Caption: $Views_AclGenerationRules_Types.
        /// </summary>
        public readonly ViewObject ColumnAclGenerationRuleTypes = new ViewObject(2, "AclGenerationRuleTypes", "$Views_AclGenerationRules_Types");

        /// <summary>
        ///     ID:3 
        ///     Alias: AclGenerationRuleUseSmartRoles
        ///     Caption: $Views_AclGenerationRules_UseSmartRoles.
        /// </summary>
        public readonly ViewObject ColumnAclGenerationRuleUseSmartRoles = new ViewObject(3, "AclGenerationRuleUseSmartRoles", "$Views_AclGenerationRules_UseSmartRoles");

        /// <summary>
        ///     ID:4 
        ///     Alias: AclGenerationRuleSmartRoleGenerator
        ///     Caption: $Views_AclGenerationRules_SmartRoleGenerator.
        /// </summary>
        public readonly ViewObject ColumnAclGenerationRuleSmartRoleGenerator = new ViewObject(4, "AclGenerationRuleSmartRoleGenerator", "$Views_AclGenerationRules_SmartRoleGenerator");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_AclGenerationRules_Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_AclGenerationRules_Name");

        /// <summary>
        ///     ID:1 
        ///     Alias: CardType
        ///     Caption: $Views_AclGenerationRules_CardType.
        /// </summary>
        public readonly ViewObject ParamCardType = new ViewObject(1, "CardType", "$Views_AclGenerationRules_CardType");

        /// <summary>
        ///     ID:2 
        ///     Alias: UseSmartRoles
        ///     Caption: $Views_AclGenerationRules_UseSmartRoles.
        /// </summary>
        public readonly ViewObject ParamUseSmartRoles = new ViewObject(2, "UseSmartRoles", "$Views_AclGenerationRules_UseSmartRoles");

        /// <summary>
        ///     ID:3 
        ///     Alias: SmartRoleGenerator
        ///     Caption: $Views_AclGenerationRules_SmartRoleGenerator.
        /// </summary>
        public readonly ViewObject ParamSmartRoleGenerator = new ViewObject(3, "SmartRoleGenerator", "$Views_AclGenerationRules_SmartRoleGenerator");

        #endregion

        #region ToString

        public static implicit operator string(AclGenerationRulesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region AcquaintanceHistory

    /// <summary>
    ///     ID: {6fe5e33f-4ba9-4378-bd7f-8a2a15f0d838}
    ///     Alias: AcquaintanceHistory
    ///     Caption: $Views_Names_AcquaintanceHistory
    ///     Group: Acquaintance
    /// </summary>
    public class AcquaintanceHistoryViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "AcquaintanceHistory": {6fe5e33f-4ba9-4378-bd7f-8a2a15f0d838}.
        /// </summary>
        public readonly Guid ID = new Guid(0x6fe5e33f,0x4ba9,0x4378,0xbd,0x7f,0x8a,0x2a,0x15,0xf0,0xd8,0x38);

        /// <summary>
        ///     View name for "AcquaintanceHistory".
        /// </summary>
        public readonly string Alias = "AcquaintanceHistory";

        /// <summary>
        ///     View caption for "AcquaintanceHistory".
        /// </summary>
        public readonly string Caption = "$Views_Names_AcquaintanceHistory";

        /// <summary>
        ///     View group for "AcquaintanceHistory".
        /// </summary>
        public readonly string Group = "Acquaintance";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ID.
        /// </summary>
        public readonly ViewObject ColumnID = new ViewObject(0, "ID");

        /// <summary>
        ///     ID:1 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(1, "UserID");

        /// <summary>
        ///     ID:2 
        ///     Alias: UserName
        ///     Caption: $Views_Acquaintance_Employee.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(2, "UserName", "$Views_Acquaintance_Employee");

        /// <summary>
        ///     ID:3 
        ///     Alias: IsReceived.
        /// </summary>
        public readonly ViewObject ColumnIsReceived = new ViewObject(3, "IsReceived");

        /// <summary>
        ///     ID:4 
        ///     Alias: IsReceivedString
        ///     Caption: $Views_Acquaintance_State.
        /// </summary>
        public readonly ViewObject ColumnIsReceivedString = new ViewObject(4, "IsReceivedString", "$Views_Acquaintance_State");

        /// <summary>
        ///     ID:5 
        ///     Alias: Received
        ///     Caption: $Views_Acquaintance_ReceivedDate.
        /// </summary>
        public readonly ViewObject ColumnReceived = new ViewObject(5, "Received", "$Views_Acquaintance_ReceivedDate");

        /// <summary>
        ///     ID:6 
        ///     Alias: SenderID.
        /// </summary>
        public readonly ViewObject ColumnSenderID = new ViewObject(6, "SenderID");

        /// <summary>
        ///     ID:7 
        ///     Alias: SenderName
        ///     Caption: $Views_Acquaintance_Sender.
        /// </summary>
        public readonly ViewObject ColumnSenderName = new ViewObject(7, "SenderName", "$Views_Acquaintance_Sender");

        /// <summary>
        ///     ID:8 
        ///     Alias: Sent
        ///     Caption: $Views_Acquaintance_SentDate.
        /// </summary>
        public readonly ViewObject ColumnSent = new ViewObject(8, "Sent", "$Views_Acquaintance_SentDate");

        /// <summary>
        ///     ID:9 
        ///     Alias: Comment
        ///     Caption: $Views_Acquaintance_CommentColumn.
        /// </summary>
        public readonly ViewObject ColumnComment = new ViewObject(9, "Comment", "$Views_Acquaintance_CommentColumn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CardIDParam
        ///     Caption: $Views_Acquaintance_CardID.
        /// </summary>
        public readonly ViewObject ParamCardIDParam = new ViewObject(0, "CardIDParam", "$Views_Acquaintance_CardID");

        /// <summary>
        ///     ID:1 
        ///     Alias: UserParam
        ///     Caption: $Views_Acquaintance_Employee.
        /// </summary>
        public readonly ViewObject ParamUserParam = new ViewObject(1, "UserParam", "$Views_Acquaintance_Employee");

        /// <summary>
        ///     ID:2 
        ///     Alias: IsReceivedParam
        ///     Caption: $Views_Acquaintance_State.
        /// </summary>
        public readonly ViewObject ParamIsReceivedParam = new ViewObject(2, "IsReceivedParam", "$Views_Acquaintance_State");

        /// <summary>
        ///     ID:3 
        ///     Alias: SenderParam
        ///     Caption: $Views_Acquaintance_Sender.
        /// </summary>
        public readonly ViewObject ParamSenderParam = new ViewObject(3, "SenderParam", "$Views_Acquaintance_Sender");

        /// <summary>
        ///     ID:4 
        ///     Alias: CommentParam
        ///     Caption: $Views_Acquaintance_CommentParam.
        /// </summary>
        public readonly ViewObject ParamCommentParam = new ViewObject(4, "CommentParam", "$Views_Acquaintance_CommentParam");

        #endregion

        #region ToString

        public static implicit operator string(AcquaintanceHistoryViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region AcquaintanceStates

    /// <summary>
    ///     ID: {02f5ab66-8e1f-4c0b-a257-5b53428273e2}
    ///     Alias: AcquaintanceStates
    ///     Caption: $Views_Names_AcquaintanceStates
    ///     Group: Acquaintance
    /// </summary>
    public class AcquaintanceStatesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "AcquaintanceStates": {02f5ab66-8e1f-4c0b-a257-5b53428273e2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x02f5ab66,0x8e1f,0x4c0b,0xa2,0x57,0x5b,0x53,0x42,0x82,0x73,0xe2);

        /// <summary>
        ///     View name for "AcquaintanceStates".
        /// </summary>
        public readonly string Alias = "AcquaintanceStates";

        /// <summary>
        ///     View caption for "AcquaintanceStates".
        /// </summary>
        public readonly string Caption = "$Views_Names_AcquaintanceStates";

        /// <summary>
        ///     View group for "AcquaintanceStates".
        /// </summary>
        public readonly string Group = "Acquaintance";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: StateID.
        /// </summary>
        public readonly ViewObject ColumnStateID = new ViewObject(0, "StateID");

        /// <summary>
        ///     ID:1 
        ///     Alias: StateName
        ///     Caption: $Views_Acquaintance_State.
        /// </summary>
        public readonly ViewObject ColumnStateName = new ViewObject(1, "StateName", "$Views_Acquaintance_State");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Acquaintance_State.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Acquaintance_State");

        #endregion

        #region ToString

        public static implicit operator string(AcquaintanceStatesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ActionHistory

    /// <summary>
    ///     ID: {7b10287f-31bb-4d4c-a515-ae754e452ed3}
    ///     Alias: ActionHistory
    ///     Caption: $Views_Names_ActionHistory
    ///     Group: System
    /// </summary>
    public class ActionHistoryViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ActionHistory": {7b10287f-31bb-4d4c-a515-ae754e452ed3}.
        /// </summary>
        public readonly Guid ID = new Guid(0x7b10287f,0x31bb,0x4d4c,0xa5,0x15,0xae,0x75,0x4e,0x45,0x2e,0xd3);

        /// <summary>
        ///     View name for "ActionHistory".
        /// </summary>
        public readonly string Alias = "ActionHistory";

        /// <summary>
        ///     View caption for "ActionHistory".
        /// </summary>
        public readonly string Caption = "$Views_Names_ActionHistory";

        /// <summary>
        ///     View group for "ActionHistory".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RecordID.
        /// </summary>
        public readonly ViewObject ColumnRecordID = new ViewObject(0, "RecordID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RecordCaption.
        /// </summary>
        public readonly ViewObject ColumnRecordCaption = new ViewObject(1, "RecordCaption");

        /// <summary>
        ///     ID:2 
        ///     Alias: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(2, "CardID");

        /// <summary>
        ///     ID:3 
        ///     Alias: CardCaption
        ///     Caption: $Views_ActionHistory_Caption.
        /// </summary>
        public readonly ViewObject ColumnCardCaption = new ViewObject(3, "CardCaption", "$Views_ActionHistory_Caption");

        /// <summary>
        ///     ID:4 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(4, "TypeID");

        /// <summary>
        ///     ID:5 
        ///     Alias: TypeCaption
        ///     Caption: $Views_ActionHistory_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(5, "TypeCaption", "$Views_ActionHistory_Type");

        /// <summary>
        ///     ID:6 
        ///     Alias: ActionID.
        /// </summary>
        public readonly ViewObject ColumnActionID = new ViewObject(6, "ActionID");

        /// <summary>
        ///     ID:7 
        ///     Alias: ActionName
        ///     Caption: $Views_ActionHistory_Action.
        /// </summary>
        public readonly ViewObject ColumnActionName = new ViewObject(7, "ActionName", "$Views_ActionHistory_Action");

        /// <summary>
        ///     ID:8 
        ///     Alias: Modified
        ///     Caption: $Views_ActionHistory_DateTime.
        /// </summary>
        public readonly ViewObject ColumnModified = new ViewObject(8, "Modified", "$Views_ActionHistory_DateTime");

        /// <summary>
        ///     ID:9 
        ///     Alias: ModifiedByID.
        /// </summary>
        public readonly ViewObject ColumnModifiedByID = new ViewObject(9, "ModifiedByID");

        /// <summary>
        ///     ID:10 
        ///     Alias: ModifiedByName
        ///     Caption: $Views_ActionHistory_User.
        /// </summary>
        public readonly ViewObject ColumnModifiedByName = new ViewObject(10, "ModifiedByName", "$Views_ActionHistory_User");

        /// <summary>
        ///     ID:11 
        ///     Alias: ApplicationID.
        /// </summary>
        public readonly ViewObject ColumnApplicationID = new ViewObject(11, "ApplicationID");

        /// <summary>
        ///     ID:12 
        ///     Alias: ApplicationName
        ///     Caption: $Views_ActionHistory_Application.
        /// </summary>
        public readonly ViewObject ColumnApplicationName = new ViewObject(12, "ApplicationName", "$Views_ActionHistory_Application");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CardCaption
        ///     Caption: $Views_ActionHistory_Card_Param.
        /// </summary>
        public readonly ViewObject ParamCardCaption = new ViewObject(0, "CardCaption", "$Views_ActionHistory_Card_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeID
        ///     Caption: $Views_ActionHistory_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeID = new ViewObject(1, "TypeID", "$Views_ActionHistory_Type_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: ActionID
        ///     Caption: $Views_ActionHistory_Action_Param.
        /// </summary>
        public readonly ViewObject ParamActionID = new ViewObject(2, "ActionID", "$Views_ActionHistory_Action_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: ApplicationID
        ///     Caption: $Views_ActionHistory_Application_Param.
        /// </summary>
        public readonly ViewObject ParamApplicationID = new ViewObject(3, "ApplicationID", "$Views_ActionHistory_Application_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Modified
        ///     Caption: $Views_ActionHistory_DateTime_Param.
        /// </summary>
        public readonly ViewObject ParamModified = new ViewObject(4, "Modified", "$Views_ActionHistory_DateTime_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: ModifiedByID
        ///     Caption: $Views_ActionHistory_User_Param.
        /// </summary>
        public readonly ViewObject ParamModifiedByID = new ViewObject(5, "ModifiedByID", "$Views_ActionHistory_User_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: ModifiedByName
        ///     Caption: $Views_ActionHistory_UserName_Param.
        /// </summary>
        public readonly ViewObject ParamModifiedByName = new ViewObject(6, "ModifiedByName", "$Views_ActionHistory_UserName_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: CardID
        ///     Caption: $Views_ActionHistory_CardId_Param.
        /// </summary>
        public readonly ViewObject ParamCardID = new ViewObject(7, "CardID", "$Views_ActionHistory_CardId_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: CardIDVisible
        ///     Caption: $Views_ActionHistory_CardId_Param.
        /// </summary>
        public readonly ViewObject ParamCardIDVisible = new ViewObject(8, "CardIDVisible", "$Views_ActionHistory_CardId_Param");

        /// <summary>
        ///     ID:9 
        ///     Alias: SessionID
        ///     Caption: $Views_ActionHistory_SessionID_Param.
        /// </summary>
        public readonly ViewObject ParamSessionID = new ViewObject(9, "SessionID", "$Views_ActionHistory_SessionID_Param");

        /// <summary>
        ///     ID:10 
        ///     Alias: SessionIDHidden
        ///     Caption: Session ID.
        /// </summary>
        public readonly ViewObject ParamSessionIDHidden = new ViewObject(10, "SessionIDHidden", "Session ID");

        /// <summary>
        ///     ID:11 
        ///     Alias: DepartmentID
        ///     Caption: $Views_ActionHistory_UserDepartment_Param.
        /// </summary>
        public readonly ViewObject ParamDepartmentID = new ViewObject(11, "DepartmentID", "$Views_ActionHistory_UserDepartment_Param");

        /// <summary>
        ///     ID:12 
        ///     Alias: DatabaseID.
        /// </summary>
        public readonly ViewObject ParamDatabaseID = new ViewObject(12, "DatabaseID");

        #endregion

        #region ToString

        public static implicit operator string(ActionHistoryViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ActionHistoryTypes

    /// <summary>
    ///     ID: {07775e91-96d5-4b0c-b978-abc26c55d899}
    ///     Alias: ActionHistoryTypes
    ///     Caption: $Views_Names_ActionHistoryTypes
    ///     Group: System
    /// </summary>
    public class ActionHistoryTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ActionHistoryTypes": {07775e91-96d5-4b0c-b978-abc26c55d899}.
        /// </summary>
        public readonly Guid ID = new Guid(0x07775e91,0x96d5,0x4b0c,0xb9,0x78,0xab,0xc2,0x6c,0x55,0xd8,0x99);

        /// <summary>
        ///     View name for "ActionHistoryTypes".
        /// </summary>
        public readonly string Alias = "ActionHistoryTypes";

        /// <summary>
        ///     View caption for "ActionHistoryTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_ActionHistoryTypes";

        /// <summary>
        ///     View group for "ActionHistoryTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeCaption
        ///     Caption: $Views_Types_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(1, "TypeCaption", "$Views_Types_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeName
        ///     Caption: $Views_Types_Alias.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(2, "TypeName", "$Views_Types_Alias");

        /// <summary>
        ///     ID:3 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(3, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_Types_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_Types_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_Types_Alias_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_Types_Alias_Param");

        #endregion

        #region ToString

        public static implicit operator string(ActionHistoryTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ActionTypes

    /// <summary>
    ///     ID: {12532568-f56f-4399-9a86-5e76871c33aa}
    ///     Alias: ActionTypes
    ///     Caption: $Views_Names_ActionTypes
    ///     Group: System
    /// </summary>
    public class ActionTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ActionTypes": {12532568-f56f-4399-9a86-5e76871c33aa}.
        /// </summary>
        public readonly Guid ID = new Guid(0x12532568,0xf56f,0x4399,0x9a,0x86,0x5e,0x76,0x87,0x1c,0x33,0xaa);

        /// <summary>
        ///     View name for "ActionTypes".
        /// </summary>
        public readonly string Alias = "ActionTypes";

        /// <summary>
        ///     View caption for "ActionTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_ActionTypes";

        /// <summary>
        ///     View group for "ActionTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ActionID.
        /// </summary>
        public readonly ViewObject ColumnActionID = new ViewObject(0, "ActionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ActionName
        ///     Caption: $Views_ActionTypes_Caption.
        /// </summary>
        public readonly ViewObject ColumnActionName = new ViewObject(1, "ActionName", "$Views_ActionTypes_Caption");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_ActionTypes_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_ActionTypes_Caption_Param");

        #endregion

        #region ToString

        public static implicit operator string(ActionTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ActiveWorkflows

    /// <summary>
    ///     ID: {68def22d-ade6-439f-bbc4-21ea18a3c409}
    ///     Alias: ActiveWorkflows
    ///     Caption: $Views_Names_ActiveWorkflows
    ///     Group: WorkflowEngine
    /// </summary>
    public class ActiveWorkflowsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ActiveWorkflows": {68def22d-ade6-439f-bbc4-21ea18a3c409}.
        /// </summary>
        public readonly Guid ID = new Guid(0x68def22d,0xade6,0x439f,0xbb,0xc4,0x21,0xea,0x18,0xa3,0xc4,0x09);

        /// <summary>
        ///     View name for "ActiveWorkflows".
        /// </summary>
        public readonly string Alias = "ActiveWorkflows";

        /// <summary>
        ///     View caption for "ActiveWorkflows".
        /// </summary>
        public readonly string Caption = "$Views_Names_ActiveWorkflows";

        /// <summary>
        ///     View group for "ActiveWorkflows".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ProcessID.
        /// </summary>
        public readonly ViewObject ColumnProcessID = new ViewObject(0, "ProcessID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ProcessRowID.
        /// </summary>
        public readonly ViewObject ColumnProcessRowID = new ViewObject(1, "ProcessRowID");

        /// <summary>
        ///     ID:2 
        ///     Alias: ProcessName
        ///     Caption: $Views_ActiveWorkflows_ProcessName.
        /// </summary>
        public readonly ViewObject ColumnProcessName = new ViewObject(2, "ProcessName", "$Views_ActiveWorkflows_ProcessName");

        /// <summary>
        ///     ID:3 
        ///     Alias: ProcessCreated
        ///     Caption: $Views_ActiveWorkflows_Created.
        /// </summary>
        public readonly ViewObject ColumnProcessCreated = new ViewObject(3, "ProcessCreated", "$Views_ActiveWorkflows_Created");

        /// <summary>
        ///     ID:4 
        ///     Alias: ProcessLastActivity
        ///     Caption: $Views_ActiveWorkflows_LastActivity.
        /// </summary>
        public readonly ViewObject ColumnProcessLastActivity = new ViewObject(4, "ProcessLastActivity", "$Views_ActiveWorkflows_LastActivity");

        /// <summary>
        ///     ID:5 
        ///     Alias: ProcessCardID.
        /// </summary>
        public readonly ViewObject ColumnProcessCardID = new ViewObject(5, "ProcessCardID");

        /// <summary>
        ///     ID:6 
        ///     Alias: ProcessCardDigest
        ///     Caption: $Views_ActiveWorkflows_CardDigest.
        /// </summary>
        public readonly ViewObject ColumnProcessCardDigest = new ViewObject(6, "ProcessCardDigest", "$Views_ActiveWorkflows_CardDigest");

        /// <summary>
        ///     ID:7 
        ///     Alias: ProcessCardType
        ///     Caption: $Views_ActiveWorkflows_CardType.
        /// </summary>
        public readonly ViewObject ColumnProcessCardType = new ViewObject(7, "ProcessCardType", "$Views_ActiveWorkflows_CardType");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: ProcessTemplate
        ///     Caption: $Views_ActiveWorkflows_ProcessTemplate_Param.
        /// </summary>
        public readonly ViewObject ParamProcessTemplate = new ViewObject(0, "ProcessTemplate", "$Views_ActiveWorkflows_ProcessTemplate_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: CardType
        ///     Caption: $Views_ActiveWorkflows_CardType_Param.
        /// </summary>
        public readonly ViewObject ParamCardType = new ViewObject(1, "CardType", "$Views_ActiveWorkflows_CardType_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: CardDigest
        ///     Caption: $Views_ActiveWorkflows_CardDigest_Param.
        /// </summary>
        public readonly ViewObject ParamCardDigest = new ViewObject(2, "CardDigest", "$Views_ActiveWorkflows_CardDigest_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Created
        ///     Caption: $Views_ActiveWorkflows_Created_Param.
        /// </summary>
        public readonly ViewObject ParamCreated = new ViewObject(3, "Created", "$Views_ActiveWorkflows_Created_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: LastActivity
        ///     Caption: $Views_ActiveWorkflows_LastActivity_Param.
        /// </summary>
        public readonly ViewObject ParamLastActivity = new ViewObject(4, "LastActivity", "$Views_ActiveWorkflows_LastActivity_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: CardID
        ///     Caption: $Views_ActiveWorkflows_CardID_Param.
        /// </summary>
        public readonly ViewObject ParamCardID = new ViewObject(5, "CardID", "$Views_ActiveWorkflows_CardID_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: HasActiveErrors
        ///     Caption: $Views_ActiveWorkflows_HasActiveErrors_Param.
        /// </summary>
        public readonly ViewObject ParamHasActiveErrors = new ViewObject(6, "HasActiveErrors", "$Views_ActiveWorkflows_HasActiveErrors_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: ProcessInstance
        ///     Caption: Process instance.
        /// </summary>
        public readonly ViewObject ParamProcessInstance = new ViewObject(7, "ProcessInstance", "Process instance");

        #endregion

        #region ToString

        public static implicit operator string(ActiveWorkflowsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ApplicationArchitectures

    /// <summary>
    ///     ID: {1ff3c65c-4926-4eac-ab0a-0c28f213d482}
    ///     Alias: ApplicationArchitectures
    ///     Caption: $Views_Names_ApplicationArchitectures
    ///     Group: System
    /// </summary>
    public class ApplicationArchitecturesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ApplicationArchitectures": {1ff3c65c-4926-4eac-ab0a-0c28f213d482}.
        /// </summary>
        public readonly Guid ID = new Guid(0x1ff3c65c,0x4926,0x4eac,0xab,0x0a,0x0c,0x28,0xf2,0x13,0xd4,0x82);

        /// <summary>
        ///     View name for "ApplicationArchitectures".
        /// </summary>
        public readonly string Alias = "ApplicationArchitectures";

        /// <summary>
        ///     View caption for "ApplicationArchitectures".
        /// </summary>
        public readonly string Caption = "$Views_Names_ApplicationArchitectures";

        /// <summary>
        ///     View group for "ApplicationArchitectures".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ArchitectureID.
        /// </summary>
        public readonly ViewObject ColumnArchitectureID = new ViewObject(0, "ArchitectureID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ArchitectureName
        ///     Caption: $Views_ApplicationArchitectures_Name.
        /// </summary>
        public readonly ViewObject ColumnArchitectureName = new ViewObject(1, "ArchitectureName", "$Views_ApplicationArchitectures_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_ApplicationArchitectures_Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_ApplicationArchitectures_Name");

        #endregion

        #region ToString

        public static implicit operator string(ApplicationArchitecturesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ApplicationNames

    /// <summary>
    ///     ID: {1e314e13-904d-491d-93fb-d9f2f912498e}
    ///     Alias: ApplicationNames
    ///     Caption: $Views_Names_ApplicationNames
    ///     Group: System
    /// </summary>
    public class ApplicationNamesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ApplicationNames": {1e314e13-904d-491d-93fb-d9f2f912498e}.
        /// </summary>
        public readonly Guid ID = new Guid(0x1e314e13,0x904d,0x491d,0x93,0xfb,0xd9,0xf2,0xf9,0x12,0x49,0x8e);

        /// <summary>
        ///     View name for "ApplicationNames".
        /// </summary>
        public readonly string Alias = "ApplicationNames";

        /// <summary>
        ///     View caption for "ApplicationNames".
        /// </summary>
        public readonly string Caption = "$Views_Names_ApplicationNames";

        /// <summary>
        ///     View group for "ApplicationNames".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: $Views_ApplicationNames_Name.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "$Views_ApplicationNames_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_ApplicationNames_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_ApplicationNames_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(ApplicationNamesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Applications

    /// <summary>
    ///     ID: {87345860-1b95-4fdd-887e-bf632fb3752d}
    ///     Alias: Applications
    ///     Caption: $Views_Names_Applications
    ///     Group: System
    /// </summary>
    public class ApplicationsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Applications": {87345860-1b95-4fdd-887e-bf632fb3752d}.
        /// </summary>
        public readonly Guid ID = new Guid(0x87345860,0x1b95,0x4fdd,0x88,0x7e,0xbf,0x63,0x2f,0xb3,0x75,0x2d);

        /// <summary>
        ///     View name for "Applications".
        /// </summary>
        public readonly string Alias = "Applications";

        /// <summary>
        ///     View caption for "Applications".
        /// </summary>
        public readonly string Caption = "$Views_Names_Applications";

        /// <summary>
        ///     View group for "Applications".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: AppID
        ///     Caption: ID.
        /// </summary>
        public readonly ViewObject ColumnAppID = new ViewObject(0, "AppID", "ID");

        /// <summary>
        ///     ID:1 
        ///     Alias: AppName
        ///     Caption: $Views_Applications_Name.
        /// </summary>
        public readonly ViewObject ColumnAppName = new ViewObject(1, "AppName", "$Views_Applications_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: GroupName
        ///     Caption: $Views_Applications_GroupName.
        /// </summary>
        public readonly ViewObject ColumnGroupName = new ViewObject(2, "GroupName", "$Views_Applications_GroupName");

        /// <summary>
        ///     ID:3 
        ///     Alias: LocalizedGroupName
        ///     Caption: $Views_Applications_GroupName.
        /// </summary>
        public readonly ViewObject ColumnLocalizedGroupName = new ViewObject(3, "LocalizedGroupName", "$Views_Applications_GroupName");

        /// <summary>
        ///     ID:4 
        ///     Alias: Alias
        ///     Caption: $Views_Applications_Alias.
        /// </summary>
        public readonly ViewObject ColumnAlias = new ViewObject(4, "Alias", "$Views_Applications_Alias");

        /// <summary>
        ///     ID:5 
        ///     Alias: Icon
        ///     Caption: Icon.
        /// </summary>
        public readonly ViewObject ColumnIcon = new ViewObject(5, "Icon", "Icon");

        /// <summary>
        ///     ID:6 
        ///     Alias: Client64Bit
        ///     Caption: $Views_Applications_Client64Bit.
        /// </summary>
        public readonly ViewObject ColumnClient64Bit = new ViewObject(6, "Client64Bit", "$Views_Applications_Client64Bit");

        /// <summary>
        ///     ID:7 
        ///     Alias: ExecutableFileName
        ///     Caption: $Views_Applications_ExecutableFileName.
        /// </summary>
        public readonly ViewObject ColumnExecutableFileName = new ViewObject(7, "ExecutableFileName", "$Views_Applications_ExecutableFileName");

        /// <summary>
        ///     ID:8 
        ///     Alias: ForAdmin
        ///     Caption: $Views_Applications_ForAdmin.
        /// </summary>
        public readonly ViewObject ColumnForAdmin = new ViewObject(8, "ForAdmin", "$Views_Applications_ForAdmin");

        /// <summary>
        ///     ID:9 
        ///     Alias: AppManagerApiV2.
        /// </summary>
        public readonly ViewObject ColumnAppManagerApiV2 = new ViewObject(9, "AppManagerApiV2");

        /// <summary>
        ///     ID:10 
        ///     Alias: AppVersion
        ///     Caption: $Views_Applications_AppVersion.
        /// </summary>
        public readonly ViewObject ColumnAppVersion = new ViewObject(10, "AppVersion", "$Views_Applications_AppVersion");

        /// <summary>
        ///     ID:11 
        ///     Alias: ExtensionVersion
        ///     Caption: $Views_Applications_ExtensionVersion.
        /// </summary>
        public readonly ViewObject ColumnExtensionVersion = new ViewObject(11, "ExtensionVersion", "$Views_Applications_ExtensionVersion");

        /// <summary>
        ///     ID:12 
        ///     Alias: PlatformVersion
        ///     Caption: $Views_Applications_PlatformVersion.
        /// </summary>
        public readonly ViewObject ColumnPlatformVersion = new ViewObject(12, "PlatformVersion", "$Views_Applications_PlatformVersion");

        /// <summary>
        ///     ID:13 
        ///     Alias: AvailableRoles
        ///     Caption: $Views_Applications_AvailableRoles.
        /// </summary>
        public readonly ViewObject ColumnAvailableRoles = new ViewObject(13, "AvailableRoles", "$Views_Applications_AvailableRoles");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Applications_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Applications_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(ApplicationsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region AvailableApplications

    /// <summary>
    ///     ID: {1a272344-a020-452a-b6e9-3720064ed760}
    ///     Alias: AvailableApplications
    ///     Caption: $Views_Names_AvailableApplications
    ///     Group: System
    /// </summary>
    public class AvailableApplicationsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "AvailableApplications": {1a272344-a020-452a-b6e9-3720064ed760}.
        /// </summary>
        public readonly Guid ID = new Guid(0x1a272344,0xa020,0x452a,0xb6,0xe9,0x37,0x20,0x06,0x4e,0xd7,0x60);

        /// <summary>
        ///     View name for "AvailableApplications".
        /// </summary>
        public readonly string Alias = "AvailableApplications";

        /// <summary>
        ///     View caption for "AvailableApplications".
        /// </summary>
        public readonly string Caption = "$Views_Names_AvailableApplications";

        /// <summary>
        ///     View group for "AvailableApplications".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ID.
        /// </summary>
        public readonly ViewObject ColumnID = new ViewObject(0, "ID");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_Applications_Name.
        /// </summary>
        public readonly ViewObject ColumnName = new ViewObject(1, "Name", "$Views_Applications_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: Alias
        ///     Caption: $Views_Applications_Alias.
        /// </summary>
        public readonly ViewObject ColumnAlias = new ViewObject(2, "Alias", "$Views_Applications_Alias");

        /// <summary>
        ///     ID:3 
        ///     Alias: Icon.
        /// </summary>
        public readonly ViewObject ColumnIcon = new ViewObject(3, "Icon");

        /// <summary>
        ///     ID:4 
        ///     Alias: Client64Bit
        ///     Caption: $Views_Applications_Client64Bit.
        /// </summary>
        public readonly ViewObject ColumnClient64Bit = new ViewObject(4, "Client64Bit", "$Views_Applications_Client64Bit");

        /// <summary>
        ///     ID:5 
        ///     Alias: ExecutableFileName
        ///     Caption: $Views_Applications_ExecutableFileName.
        /// </summary>
        public readonly ViewObject ColumnExecutableFileName = new ViewObject(5, "ExecutableFileName", "$Views_Applications_ExecutableFileName");

        /// <summary>
        ///     ID:6 
        ///     Alias: ForAdmin
        ///     Caption: $Views_Applications_ForAdmin.
        /// </summary>
        public readonly ViewObject ColumnForAdmin = new ViewObject(6, "ForAdmin", "$Views_Applications_ForAdmin");

        /// <summary>
        ///     ID:7 
        ///     Alias: AppManagerApiV2.
        /// </summary>
        public readonly ViewObject ColumnAppManagerApiV2 = new ViewObject(7, "AppManagerApiV2");

        /// <summary>
        ///     ID:8 
        ///     Alias: Hidden
        ///     Caption: Скрывать приложение в AppManager-e.
        /// </summary>
        public readonly ViewObject ColumnHidden = new ViewObject(8, "Hidden", "Скрывать приложение в AppManager-e");

        /// <summary>
        ///     ID:9 
        ///     Alias: AppVersion
        ///     Caption: $Views_Applications_AppVersion.
        /// </summary>
        public readonly ViewObject ColumnAppVersion = new ViewObject(9, "AppVersion", "$Views_Applications_AppVersion");

        /// <summary>
        ///     ID:10 
        ///     Alias: ExtensionVersion
        ///     Caption: $Views_Applications_ExtensionVersion.
        /// </summary>
        public readonly ViewObject ColumnExtensionVersion = new ViewObject(10, "ExtensionVersion", "$Views_Applications_ExtensionVersion");

        /// <summary>
        ///     ID:11 
        ///     Alias: PlatformVersion
        ///     Caption: $Views_Applications_PlatformVersion.
        /// </summary>
        public readonly ViewObject ColumnPlatformVersion = new ViewObject(11, "PlatformVersion", "$Views_Applications_PlatformVersion");

        /// <summary>
        ///     ID:12 
        ///     Alias: Modified.
        /// </summary>
        public readonly ViewObject ColumnModified = new ViewObject(12, "Modified");

        /// <summary>
        ///     ID:13 
        ///     Alias: GroupName
        ///     Caption: $Views_Applications_GroupName.
        /// </summary>
        public readonly ViewObject ColumnGroupName = new ViewObject(13, "GroupName", "$Views_Applications_GroupName");

        /// <summary>
        ///     ID:14 
        ///     Alias: LocalizedGroupName
        ///     Caption: $Views_Applications_GroupName.
        /// </summary>
        public readonly ViewObject ColumnLocalizedGroupName = new ViewObject(14, "LocalizedGroupName", "$Views_Applications_GroupName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Alias
        ///     Caption: Alias.
        /// </summary>
        public readonly ViewObject ParamAlias = new ViewObject(0, "Alias", "Alias");

        /// <summary>
        ///     ID:1 
        ///     Alias: Client64Bit
        ///     Caption: Client64Bit.
        /// </summary>
        public readonly ViewObject ParamClient64Bit = new ViewObject(1, "Client64Bit", "Client64Bit");

        /// <summary>
        ///     ID:2 
        ///     Alias: GetIcon
        ///     Caption: $Views_AvailableApplications_GetIcon.
        /// </summary>
        public readonly ViewObject ParamGetIcon = new ViewObject(2, "GetIcon", "$Views_AvailableApplications_GetIcon");

        /// <summary>
        ///     ID:3 
        ///     Alias: PublishMode
        ///     Caption: Publish mode.
        /// </summary>
        public readonly ViewObject ParamPublishMode = new ViewObject(3, "PublishMode", "Publish mode");

        #endregion

        #region ToString

        public static implicit operator string(AvailableApplicationsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region AvailableDeputyRoles

    /// <summary>
    ///     ID: {530f0463-70bd-4d23-9acc-7e79e1de11af}
    ///     Alias: AvailableDeputyRoles
    ///     Caption: $Views_Names_AvailableDeputyRoles
    ///     Group: System
    /// </summary>
    public class AvailableDeputyRolesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "AvailableDeputyRoles": {530f0463-70bd-4d23-9acc-7e79e1de11af}.
        /// </summary>
        public readonly Guid ID = new Guid(0x530f0463,0x70bd,0x4d23,0x9a,0xcc,0x7e,0x79,0xe1,0xde,0x11,0xaf);

        /// <summary>
        ///     View name for "AvailableDeputyRoles".
        /// </summary>
        public readonly string Alias = "AvailableDeputyRoles";

        /// <summary>
        ///     View caption for "AvailableDeputyRoles".
        /// </summary>
        public readonly string Caption = "$Views_Names_AvailableDeputyRoles";

        /// <summary>
        ///     View group for "AvailableDeputyRoles".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RoleID.
        /// </summary>
        public readonly ViewObject ColumnRoleID = new ViewObject(0, "RoleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleName
        ///     Caption: $Views_Roles_Role.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(1, "RoleName", "$Views_Roles_Role");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeName
        ///     Caption: $Views_Roles_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(2, "TypeName", "$Views_Roles_Type");

        /// <summary>
        ///     ID:3 
        ///     Alias: Info
        ///     Caption: $Views_Roles_Info.
        /// </summary>
        public readonly ViewObject ColumnInfo = new ViewObject(3, "Info", "$Views_Roles_Info");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Roles_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Roles_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeID
        ///     Caption: $Views_Roles_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeID = new ViewObject(1, "TypeID", "$Views_Roles_Type_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: ShowHidden
        ///     Caption: $Views_Roles_ShowHidden_Param.
        /// </summary>
        public readonly ViewObject ParamShowHidden = new ViewObject(2, "ShowHidden", "$Views_Roles_ShowHidden_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: User
        ///     Caption: User.
        /// </summary>
        public readonly ViewObject ParamUser = new ViewObject(3, "User", "User");

        #endregion

        #region ToString

        public static implicit operator string(AvailableDeputyRolesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region AvailableDeputyUsers

    /// <summary>
    ///     ID: {c7c46016-75e6-46e5-9627-74a4e5e66e29}
    ///     Alias: AvailableDeputyUsers
    ///     Caption: $Views_Names_AvailableDeputyUsers
    ///     Group: System
    /// </summary>
    public class AvailableDeputyUsersViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "AvailableDeputyUsers": {c7c46016-75e6-46e5-9627-74a4e5e66e29}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc7c46016,0x75e6,0x46e5,0x96,0x27,0x74,0xa4,0xe5,0xe6,0x6e,0x29);

        /// <summary>
        ///     View name for "AvailableDeputyUsers".
        /// </summary>
        public readonly string Alias = "AvailableDeputyUsers";

        /// <summary>
        ///     View caption for "AvailableDeputyUsers".
        /// </summary>
        public readonly string Caption = "$Views_Names_AvailableDeputyUsers";

        /// <summary>
        ///     View group for "AvailableDeputyUsers".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(0, "UserID");

        /// <summary>
        ///     ID:1 
        ///     Alias: UserName
        ///     Caption: $Views_Users_Name.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(1, "UserName", "$Views_Users_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: Email
        ///     Caption: $Views_Users_Email.
        /// </summary>
        public readonly ViewObject ColumnEmail = new ViewObject(2, "Email", "$Views_Users_Email");

        /// <summary>
        ///     ID:3 
        ///     Alias: Position
        ///     Caption: $Views_Users_Position.
        /// </summary>
        public readonly ViewObject ColumnPosition = new ViewObject(3, "Position", "$Views_Users_Position");

        /// <summary>
        ///     ID:4 
        ///     Alias: Departments
        ///     Caption: $Views_Users_Departments.
        /// </summary>
        public readonly ViewObject ColumnDepartments = new ViewObject(4, "Departments", "$Views_Users_Departments");

        /// <summary>
        ///     ID:5 
        ///     Alias: StaticRoles
        ///     Caption: $Views_Users_StaticRoles.
        /// </summary>
        public readonly ViewObject ColumnStaticRoles = new ViewObject(5, "StaticRoles", "$Views_Users_StaticRoles");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Users_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Users_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleID
        ///     Caption: $Views_Users_Role_Param.
        /// </summary>
        public readonly ViewObject ParamRoleID = new ViewObject(1, "RoleID", "$Views_Users_Role_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: ParentRoleID
        ///     Caption: $Views_Users_ParentRole_Param.
        /// </summary>
        public readonly ViewObject ParamParentRoleID = new ViewObject(2, "ParentRoleID", "$Views_Users_ParentRole_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: ShowHidden
        ///     Caption: $Views_Users_ShowHidden_Param.
        /// </summary>
        public readonly ViewObject ParamShowHidden = new ViewObject(3, "ShowHidden", "$Views_Users_ShowHidden_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: User
        ///     Caption: User.
        /// </summary>
        public readonly ViewObject ParamUser = new ViewObject(4, "User", "User");

        #endregion

        #region ToString

        public static implicit operator string(AvailableDeputyUsersViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region BarcodeTypes

    /// <summary>
    ///     ID: {f92af4c2-e862-4469-9e44-5c96e650e349}
    ///     Alias: BarcodeTypes
    ///     Caption: $Views_Names_BarcodeTypes
    ///     Group: System
    /// </summary>
    public class BarcodeTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "BarcodeTypes": {f92af4c2-e862-4469-9e44-5c96e650e349}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf92af4c2,0xe862,0x4469,0x9e,0x44,0x5c,0x96,0xe6,0x50,0xe3,0x49);

        /// <summary>
        ///     View name for "BarcodeTypes".
        /// </summary>
        public readonly string Alias = "BarcodeTypes";

        /// <summary>
        ///     View caption for "BarcodeTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_BarcodeTypes";

        /// <summary>
        ///     View group for "BarcodeTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: BarcodeTypeID.
        /// </summary>
        public readonly ViewObject ColumnBarcodeTypeID = new ViewObject(0, "BarcodeTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: BarcodeTypeName
        ///     Caption: $Views_BarcodeTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnBarcodeTypeName = new ViewObject(1, "BarcodeTypeName", "$Views_BarcodeTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: NameParam
        ///     Caption: $Views_BarcodeTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamNameParam = new ViewObject(0, "NameParam", "$Views_BarcodeTypes_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: CanScanParam
        ///     Caption: CanScan.
        /// </summary>
        public readonly ViewObject ParamCanScanParam = new ViewObject(1, "CanScanParam", "CanScan");

        /// <summary>
        ///     ID:2 
        ///     Alias: CanPrintParam
        ///     Caption: CanPrint.
        /// </summary>
        public readonly ViewObject ParamCanPrintParam = new ViewObject(2, "CanPrintParam", "CanPrint");

        #endregion

        #region ToString

        public static implicit operator string(BarcodeTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region BusinessProcessTemplates

    /// <summary>
    ///     ID: {b9174abb-c460-4a68-b56e-187d4d8f4896}
    ///     Alias: BusinessProcessTemplates
    ///     Caption: $Views_Names_BusinessProcessTemplates
    ///     Group: WorkflowEngine
    /// </summary>
    public class BusinessProcessTemplatesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "BusinessProcessTemplates": {b9174abb-c460-4a68-b56e-187d4d8f4896}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb9174abb,0xc460,0x4a68,0xb5,0x6e,0x18,0x7d,0x4d,0x8f,0x48,0x96);

        /// <summary>
        ///     View name for "BusinessProcessTemplates".
        /// </summary>
        public readonly string Alias = "BusinessProcessTemplates";

        /// <summary>
        ///     View caption for "BusinessProcessTemplates".
        /// </summary>
        public readonly string Caption = "$Views_Names_BusinessProcessTemplates";

        /// <summary>
        ///     View group for "BusinessProcessTemplates".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: BusinessProcessID.
        /// </summary>
        public readonly ViewObject ColumnBusinessProcessID = new ViewObject(0, "BusinessProcessID");

        /// <summary>
        ///     ID:1 
        ///     Alias: BusinessProcessName
        ///     Caption: $Views_BusinessProcessTemplates_ProcessTemplateName.
        /// </summary>
        public readonly ViewObject ColumnBusinessProcessName = new ViewObject(1, "BusinessProcessName", "$Views_BusinessProcessTemplates_ProcessTemplateName");

        /// <summary>
        ///     ID:2 
        ///     Alias: BusinessProcessGroup
        ///     Caption: $Views_BusinessProcessTemplates_Group.
        /// </summary>
        public readonly ViewObject ColumnBusinessProcessGroup = new ViewObject(2, "BusinessProcessGroup", "$Views_BusinessProcessTemplates_Group");

        /// <summary>
        ///     ID:3 
        ///     Alias: BusinessProcessStartFromCard
        ///     Caption: $Views_BusinessProcessTemplates_RunFromCard.
        /// </summary>
        public readonly ViewObject ColumnBusinessProcessStartFromCard = new ViewObject(3, "BusinessProcessStartFromCard", "$Views_BusinessProcessTemplates_RunFromCard");

        /// <summary>
        ///     ID:4 
        ///     Alias: BusinessProcessMultiple
        ///     Caption: $Views_BusinessProcessTemplates_MultipleInstances.
        /// </summary>
        public readonly ViewObject ColumnBusinessProcessMultiple = new ViewObject(4, "BusinessProcessMultiple", "$Views_BusinessProcessTemplates_MultipleInstances");

        /// <summary>
        ///     ID:5 
        ///     Alias: BusinessProcessCardTypes
        ///     Caption: $Views_BusinessProcessTemplates_CardTypes.
        /// </summary>
        public readonly ViewObject ColumnBusinessProcessCardTypes = new ViewObject(5, "BusinessProcessCardTypes", "$Views_BusinessProcessTemplates_CardTypes");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_BusinessProcessTemplates_ProcessTemplateName_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_BusinessProcessTemplates_ProcessTemplateName_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Group
        ///     Caption: $Views_BusinessProcessTemplates_Group_Param.
        /// </summary>
        public readonly ViewObject ParamGroup = new ViewObject(1, "Group", "$Views_BusinessProcessTemplates_Group_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: StartFromCard
        ///     Caption: $Views_BusinessProcessTemplates_RunFromCard_Param.
        /// </summary>
        public readonly ViewObject ParamStartFromCard = new ViewObject(2, "StartFromCard", "$Views_BusinessProcessTemplates_RunFromCard_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Multiple
        ///     Caption: $Views_BusinessProcessTemplates_MultipleInstances_Param.
        /// </summary>
        public readonly ViewObject ParamMultiple = new ViewObject(3, "Multiple", "$Views_BusinessProcessTemplates_MultipleInstances_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: CardType
        ///     Caption: $Views_BusinessProcessTemplates_CardType_Param.
        /// </summary>
        public readonly ViewObject ParamCardType = new ViewObject(4, "CardType", "$Views_BusinessProcessTemplates_CardType_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: GroupHidden
        ///     Caption: $Views_BusinessProcessTemplates_Group_Param.
        /// </summary>
        public readonly ViewObject ParamGroupHidden = new ViewObject(5, "GroupHidden", "$Views_BusinessProcessTemplates_Group_Param");

        #endregion

        #region ToString

        public static implicit operator string(BusinessProcessTemplatesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CalendarCalcMethods

    /// <summary>
    ///     ID: {61a516b2-bb7d-41b7-b05c-57a5aeb564ac}
    ///     Alias: CalendarCalcMethods
    ///     Caption: $Views_Names_CalendarCalcMethods
    ///     Group: Calendar
    /// </summary>
    public class CalendarCalcMethodsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "CalendarCalcMethods": {61a516b2-bb7d-41b7-b05c-57a5aeb564ac}.
        /// </summary>
        public readonly Guid ID = new Guid(0x61a516b2,0xbb7d,0x41b7,0xb0,0x5c,0x57,0xa5,0xae,0xb5,0x64,0xac);

        /// <summary>
        ///     View name for "CalendarCalcMethods".
        /// </summary>
        public readonly string Alias = "CalendarCalcMethods";

        /// <summary>
        ///     View caption for "CalendarCalcMethods".
        /// </summary>
        public readonly string Caption = "$Views_Names_CalendarCalcMethods";

        /// <summary>
        ///     View group for "CalendarCalcMethods".
        /// </summary>
        public readonly string Group = "Calendar";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: MethodID.
        /// </summary>
        public readonly ViewObject ColumnMethodID = new ViewObject(0, "MethodID");

        /// <summary>
        ///     ID:1 
        ///     Alias: MethodName
        ///     Caption: $Views_CalendarCalcMethods_Name.
        /// </summary>
        public readonly ViewObject ColumnMethodName = new ViewObject(1, "MethodName", "$Views_CalendarCalcMethods_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_CalendarCalcMethods_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_CalendarCalcMethods_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(CalendarCalcMethodsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Calendars

    /// <summary>
    ///     ID: {d352f577-8724-4677-a61b-d3e66effd5e1}
    ///     Alias: Calendars
    ///     Caption: $Views_Names_Calendars
    ///     Group: Calendar
    /// </summary>
    public class CalendarsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Calendars": {d352f577-8724-4677-a61b-d3e66effd5e1}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd352f577,0x8724,0x4677,0xa6,0x1b,0xd3,0xe6,0x6e,0xff,0xd5,0xe1);

        /// <summary>
        ///     View name for "Calendars".
        /// </summary>
        public readonly string Alias = "Calendars";

        /// <summary>
        ///     View caption for "Calendars".
        /// </summary>
        public readonly string Caption = "$Views_Names_Calendars";

        /// <summary>
        ///     View group for "Calendars".
        /// </summary>
        public readonly string Group = "Calendar";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CalendarID.
        /// </summary>
        public readonly ViewObject ColumnCalendarID = new ViewObject(0, "CalendarID");

        /// <summary>
        ///     ID:1 
        ///     Alias: CalendarCalendarID
        ///     Caption: $Views_Calendars_CalendarID.
        /// </summary>
        public readonly ViewObject ColumnCalendarCalendarID = new ViewObject(1, "CalendarCalendarID", "$Views_Calendars_CalendarID");

        /// <summary>
        ///     ID:2 
        ///     Alias: CalendarName
        ///     Caption: $Views_Calendars_Name.
        /// </summary>
        public readonly ViewObject ColumnCalendarName = new ViewObject(2, "CalendarName", "$Views_Calendars_Name");

        /// <summary>
        ///     ID:3 
        ///     Alias: CalendarTypeID.
        /// </summary>
        public readonly ViewObject ColumnCalendarTypeID = new ViewObject(3, "CalendarTypeID");

        /// <summary>
        ///     ID:4 
        ///     Alias: CalendarTypeCaption
        ///     Caption: $Views_Calendars_TypeCaption.
        /// </summary>
        public readonly ViewObject ColumnCalendarTypeCaption = new ViewObject(4, "CalendarTypeCaption", "$Views_Calendars_TypeCaption");

        /// <summary>
        ///     ID:5 
        ///     Alias: Description
        ///     Caption: $Views_Calendars_Description.
        /// </summary>
        public readonly ViewObject ColumnDescription = new ViewObject(5, "Description", "$Views_Calendars_Description");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Calendars_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Calendars_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(CalendarsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CalendarTypes

    /// <summary>
    ///     ID: {422a7b6e-9d7f-4d76-aba1-d3487cae216d}
    ///     Alias: CalendarTypes
    ///     Caption: $Views_Names_CalendarTypes
    ///     Group: Calendar
    /// </summary>
    public class CalendarTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "CalendarTypes": {422a7b6e-9d7f-4d76-aba1-d3487cae216d}.
        /// </summary>
        public readonly Guid ID = new Guid(0x422a7b6e,0x9d7f,0x4d76,0xab,0xa1,0xd3,0x48,0x7c,0xae,0x21,0x6d);

        /// <summary>
        ///     View name for "CalendarTypes".
        /// </summary>
        public readonly string Alias = "CalendarTypes";

        /// <summary>
        ///     View caption for "CalendarTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_CalendarTypes";

        /// <summary>
        ///     View group for "CalendarTypes".
        /// </summary>
        public readonly string Group = "Calendar";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeCaption
        ///     Caption: $Views_CalendarCalcMethods_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(1, "TypeCaption", "$Views_CalendarCalcMethods_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeWorkDaysInWeek
        ///     Caption: $CardTypes_Controls_WorkingDaysInWeek.
        /// </summary>
        public readonly ViewObject ColumnTypeWorkDaysInWeek = new ViewObject(2, "TypeWorkDaysInWeek", "$CardTypes_Controls_WorkingDaysInWeek");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeHoursInDay
        ///     Caption: $CardTypes_Controls_HoursInDay.
        /// </summary>
        public readonly ViewObject ColumnTypeHoursInDay = new ViewObject(3, "TypeHoursInDay", "$CardTypes_Controls_HoursInDay");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_CalendarCalcMethods_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_CalendarCalcMethods_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(CalendarTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CardTasks

    /// <summary>
    ///     ID: {eff8e7b5-0874-4e7d-ab09-2537e821b43d}
    ///     Alias: CardTasks
    ///     Caption: $Views_Names_CardTasks
    ///     Group: TaskAssignedRoles
    /// </summary>
    public class CardTasksViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "CardTasks": {eff8e7b5-0874-4e7d-ab09-2537e821b43d}.
        /// </summary>
        public readonly Guid ID = new Guid(0xeff8e7b5,0x0874,0x4e7d,0xab,0x09,0x25,0x37,0xe8,0x21,0xb4,0x3d);

        /// <summary>
        ///     View name for "CardTasks".
        /// </summary>
        public readonly string Alias = "CardTasks";

        /// <summary>
        ///     View caption for "CardTasks".
        /// </summary>
        public readonly string Caption = "$Views_Names_CardTasks";

        /// <summary>
        ///     View group for "CardTasks".
        /// </summary>
        public readonly string Group = "TaskAssignedRoles";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ID.
        /// </summary>
        public readonly ViewObject ColumnID = new ViewObject(0, "ID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TaskID.
        /// </summary>
        public readonly ViewObject ColumnTaskID = new ViewObject(1, "TaskID");

        /// <summary>
        ///     ID:2 
        ///     Alias: StateID.
        /// </summary>
        public readonly ViewObject ColumnStateID = new ViewObject(2, "StateID");

        /// <summary>
        ///     ID:3 
        ///     Alias: StateName
        ///     Caption: $Views_MyTasks_State.
        /// </summary>
        public readonly ViewObject ColumnStateName = new ViewObject(3, "StateName", "$Views_MyTasks_State");

        /// <summary>
        ///     ID:4 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(4, "TypeID");

        /// <summary>
        ///     ID:5 
        ///     Alias: PlannedDate
        ///     Caption: $Views_MyTasks_Planned.
        /// </summary>
        public readonly ViewObject ColumnPlannedDate = new ViewObject(5, "PlannedDate", "$Views_MyTasks_Planned");

        /// <summary>
        ///     ID:6 
        ///     Alias: TaskDigest
        ///     Caption: $Views_MyTasks_Info.
        /// </summary>
        public readonly ViewObject ColumnTaskDigest = new ViewObject(6, "TaskDigest", "$Views_MyTasks_Info");

        /// <summary>
        ///     ID:7 
        ///     Alias: RoleID.
        /// </summary>
        public readonly ViewObject ColumnRoleID = new ViewObject(7, "RoleID");

        /// <summary>
        ///     ID:8 
        ///     Alias: RoleName
        ///     Caption: $Views_MyTasks_Performer.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(8, "RoleName", "$Views_MyTasks_Performer");

        /// <summary>
        ///     ID:9 
        ///     Alias: AuthorID.
        /// </summary>
        public readonly ViewObject ColumnAuthorID = new ViewObject(9, "AuthorID");

        /// <summary>
        ///     ID:10 
        ///     Alias: AuthorName
        ///     Caption: $Views_MyTasks_Author.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(10, "AuthorName", "$Views_MyTasks_Author");

        /// <summary>
        ///     ID:11 
        ///     Alias: Modified
        ///     Caption: $Views_MyTasks_Modified.
        /// </summary>
        public readonly ViewObject ColumnModified = new ViewObject(11, "Modified", "$Views_MyTasks_Modified");

        /// <summary>
        ///     ID:12 
        ///     Alias: Created.
        /// </summary>
        public readonly ViewObject ColumnCreated = new ViewObject(12, "Created");

        /// <summary>
        ///     ID:13 
        ///     Alias: CreatedByID.
        /// </summary>
        public readonly ViewObject ColumnCreatedByID = new ViewObject(13, "CreatedByID");

        /// <summary>
        ///     ID:14 
        ///     Alias: CreatedByName.
        /// </summary>
        public readonly ViewObject ColumnCreatedByName = new ViewObject(14, "CreatedByName");

        /// <summary>
        ///     ID:15 
        ///     Alias: TimeZoneUtcOffsetMinutes.
        /// </summary>
        public readonly ViewObject ColumnTimeZoneUtcOffsetMinutes = new ViewObject(15, "TimeZoneUtcOffsetMinutes");

        /// <summary>
        ///     ID:16 
        ///     Alias: TypeCaption
        ///     Caption: $Views_MyTasks_TaskType.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(16, "TypeCaption", "$Views_MyTasks_TaskType");

        /// <summary>
        ///     ID:17 
        ///     Alias: TimeToCompletion
        ///     Caption: $Views_MyTasks_TimeToCompletion.
        /// </summary>
        public readonly ViewObject ColumnTimeToCompletion = new ViewObject(17, "TimeToCompletion", "$Views_MyTasks_TimeToCompletion");

        /// <summary>
        ///     ID:18 
        ///     Alias: CalendarID.
        /// </summary>
        public readonly ViewObject ColumnCalendarID = new ViewObject(18, "CalendarID");

        /// <summary>
        ///     ID:19 
        ///     Alias: QuantsToFinish.
        /// </summary>
        public readonly ViewObject ColumnQuantsToFinish = new ViewObject(19, "QuantsToFinish");

        /// <summary>
        ///     ID:20 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(20, "rn");

        /// <summary>
        ///     ID:21 
        ///     Alias: AppearanceColumn.
        /// </summary>
        public readonly ViewObject ColumnAppearanceColumn = new ViewObject(21, "AppearanceColumn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CardIDParam
        ///     Caption: $Views_CardTasks_CardID_Param.
        /// </summary>
        public readonly ViewObject ParamCardIDParam = new ViewObject(0, "CardIDParam", "$Views_CardTasks_CardID_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Token
        ///     Caption: Token.
        /// </summary>
        public readonly ViewObject ParamToken = new ViewObject(1, "Token", "Token");

        /// <summary>
        ///     ID:2 
        ///     Alias: FunctionRoleAuthorParam
        ///     Caption: $Views_MyTasks_FunctionRole_Author_Param.
        /// </summary>
        public readonly ViewObject ParamFunctionRoleAuthorParam = new ViewObject(2, "FunctionRoleAuthorParam", "$Views_MyTasks_FunctionRole_Author_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: FunctionRolePerformerParam
        ///     Caption: $Views_MyTasks_FunctionRole_Performer_Param.
        /// </summary>
        public readonly ViewObject ParamFunctionRolePerformerParam = new ViewObject(3, "FunctionRolePerformerParam", "$Views_MyTasks_FunctionRole_Performer_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Performer
        ///     Caption: $Views_CardTasks_Performer_Param.
        /// </summary>
        public readonly ViewObject ParamPerformer = new ViewObject(4, "Performer", "$Views_CardTasks_Performer_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Author
        ///     Caption: $Views_CardTasks_Author_Param.
        /// </summary>
        public readonly ViewObject ParamAuthor = new ViewObject(5, "Author", "$Views_CardTasks_Author_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: State
        ///     Caption: $Views_CardTasks_State_Param.
        /// </summary>
        public readonly ViewObject ParamState = new ViewObject(6, "State", "$Views_CardTasks_State_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: Planned
        ///     Caption: $Views_CardTasks_Planned_Param.
        /// </summary>
        public readonly ViewObject ParamPlanned = new ViewObject(7, "Planned", "$Views_CardTasks_Planned_Param");

        #endregion

        #region ToString

        public static implicit operator string(CardTasksViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CardTaskSessionRoles

    /// <summary>
    ///     ID: {088b9367-ca87-46b4-a9e2-336b0a183a8d}
    ///     Alias: CardTaskSessionRoles
    ///     Caption: $Views_Names_CardTaskSessionRoles
    ///     Group: TaskAssignedRoles
    /// </summary>
    public class CardTaskSessionRolesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "CardTaskSessionRoles": {088b9367-ca87-46b4-a9e2-336b0a183a8d}.
        /// </summary>
        public readonly Guid ID = new Guid(0x088b9367,0xca87,0x46b4,0xa9,0xe2,0x33,0x6b,0x0a,0x18,0x3a,0x8d);

        /// <summary>
        ///     View name for "CardTaskSessionRoles".
        /// </summary>
        public readonly string Alias = "CardTaskSessionRoles";

        /// <summary>
        ///     View caption for "CardTaskSessionRoles".
        /// </summary>
        public readonly string Caption = "$Views_Names_CardTaskSessionRoles";

        /// <summary>
        ///     View group for "CardTaskSessionRoles".
        /// </summary>
        public readonly string Group = "TaskAssignedRoles";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TaskRoleRowID.
        /// </summary>
        public readonly ViewObject ColumnTaskRoleRowID = new ViewObject(0, "TaskRoleRowID");

        /// <summary>
        ///     ID:1 
        ///     Alias: IsDeputy.
        /// </summary>
        public readonly ViewObject ColumnIsDeputy = new ViewObject(1, "IsDeputy");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: UserID
        ///     Caption: UserID.
        /// </summary>
        public readonly ViewObject ParamUserID = new ViewObject(0, "UserID", "UserID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TaskRowID
        ///     Caption: TaskRowID.
        /// </summary>
        public readonly ViewObject ParamTaskRowID = new ViewObject(1, "TaskRowID", "TaskRowID");

        #endregion

        #region ToString

        public static implicit operator string(CardTaskSessionRolesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Cars

    /// <summary>
    ///     ID: {257b72ba-9bba-457a-8456-d90d55d440e2}
    ///     Alias: Cars
    ///     Caption: $Views_Names_Cars
    ///     Group: Testing
    /// </summary>
    public class CarsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Cars": {257b72ba-9bba-457a-8456-d90d55d440e2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x257b72ba,0x9bba,0x457a,0x84,0x56,0xd9,0x0d,0x55,0xd4,0x40,0xe2);

        /// <summary>
        ///     View name for "Cars".
        /// </summary>
        public readonly string Alias = "Cars";

        /// <summary>
        ///     View caption for "Cars".
        /// </summary>
        public readonly string Caption = "$Views_Names_Cars";

        /// <summary>
        ///     View group for "Cars".
        /// </summary>
        public readonly string Group = "Testing";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CarID.
        /// </summary>
        public readonly ViewObject ColumnCarID = new ViewObject(0, "CarID");

        /// <summary>
        ///     ID:1 
        ///     Alias: CarName
        ///     Caption: $Views_Cars_CardName.
        /// </summary>
        public readonly ViewObject ColumnCarName = new ViewObject(1, "CarName", "$Views_Cars_CardName");

        /// <summary>
        ///     ID:2 
        ///     Alias: CarMaxSpeed
        ///     Caption: $Views_Cars_MaxSpeed.
        /// </summary>
        public readonly ViewObject ColumnCarMaxSpeed = new ViewObject(2, "CarMaxSpeed", "$Views_Cars_MaxSpeed");

        /// <summary>
        ///     ID:3 
        ///     Alias: DriverID.
        /// </summary>
        public readonly ViewObject ColumnDriverID = new ViewObject(3, "DriverID");

        /// <summary>
        ///     ID:4 
        ///     Alias: DriverName
        ///     Caption: $Views_Cars_DriverName.
        /// </summary>
        public readonly ViewObject ColumnDriverName = new ViewObject(4, "DriverName", "$Views_Cars_DriverName");

        /// <summary>
        ///     ID:5 
        ///     Alias: CarReleaseDate
        ///     Caption: $Views_Cars_ReleaseDate.
        /// </summary>
        public readonly ViewObject ColumnCarReleaseDate = new ViewObject(5, "CarReleaseDate", "$Views_Cars_ReleaseDate");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CarName
        ///     Caption: $Views_Cars_CardName_Param.
        /// </summary>
        public readonly ViewObject ParamCarName = new ViewObject(0, "CarName", "$Views_Cars_CardName_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: CarMaxSpeed
        ///     Caption: $Views_Cars_MaxSpeed_Param.
        /// </summary>
        public readonly ViewObject ParamCarMaxSpeed = new ViewObject(1, "CarMaxSpeed", "$Views_Cars_MaxSpeed_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Driver
        ///     Caption: $Views_Cars_DriverName_Param.
        /// </summary>
        public readonly ViewObject ParamDriver = new ViewObject(2, "Driver", "$Views_Cars_DriverName_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: CarReleaseDateFrom
        ///     Caption: $Views_Cars_ReleaseDateFrom_Param.
        /// </summary>
        public readonly ViewObject ParamCarReleaseDateFrom = new ViewObject(3, "CarReleaseDateFrom", "$Views_Cars_ReleaseDateFrom_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: CarReleaseDateTo
        ///     Caption: $Views_Cars_ReleaseDateTo_Param.
        /// </summary>
        public readonly ViewObject ParamCarReleaseDateTo = new ViewObject(4, "CarReleaseDateTo", "$Views_Cars_ReleaseDateTo_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Mileage
        ///     Caption: $Views_Cars_Mileage_Param.
        /// </summary>
        public readonly ViewObject ParamMileage = new ViewObject(5, "Mileage", "$Views_Cars_Mileage_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Price
        ///     Caption: $CardTypes_Controls_Price.
        /// </summary>
        public readonly ViewObject ParamPrice = new ViewObject(6, "Price", "$CardTypes_Controls_Price");

        /// <summary>
        ///     ID:7 
        ///     Alias: CarID
        ///     Caption: $Views_Cars_CarID_Param.
        /// </summary>
        public readonly ViewObject ParamCarID = new ViewObject(7, "CarID", "$Views_Cars_CarID_Param");

        #endregion

        #region ToString

        public static implicit operator string(CarsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CompletedTasks

    /// <summary>
    ///     ID: {c480683b-b3b4-4f8a-8786-3899b5bf7f00}
    ///     Alias: CompletedTasks
    ///     Caption: $Views_Names_CompletedTasks
    ///     Group: System
    /// </summary>
    public class CompletedTasksViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "CompletedTasks": {c480683b-b3b4-4f8a-8786-3899b5bf7f00}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc480683b,0xb3b4,0x4f8a,0x87,0x86,0x38,0x99,0xb5,0xbf,0x7f,0x00);

        /// <summary>
        ///     View name for "CompletedTasks".
        /// </summary>
        public readonly string Alias = "CompletedTasks";

        /// <summary>
        ///     View caption for "CompletedTasks".
        /// </summary>
        public readonly string Caption = "$Views_Names_CompletedTasks";

        /// <summary>
        ///     View group for "CompletedTasks".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(0, "CardID");

        /// <summary>
        ///     ID:1 
        ///     Alias: CardName
        ///     Caption: $Views_CompletedTasks_Card.
        /// </summary>
        public readonly ViewObject ColumnCardName = new ViewObject(1, "CardName", "$Views_CompletedTasks_Card");

        /// <summary>
        ///     ID:2 
        ///     Alias: TaskID.
        /// </summary>
        public readonly ViewObject ColumnTaskID = new ViewObject(2, "TaskID");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(3, "TypeID");

        /// <summary>
        ///     ID:4 
        ///     Alias: TypeCaption
        ///     Caption: $Views_CompletedTasks_TaskType.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(4, "TypeCaption", "$Views_CompletedTasks_TaskType");

        /// <summary>
        ///     ID:5 
        ///     Alias: CardTypeCaption
        ///     Caption: $Views_CompletedTasks_CardType.
        /// </summary>
        public readonly ViewObject ColumnCardTypeCaption = new ViewObject(5, "CardTypeCaption", "$Views_CompletedTasks_CardType");

        /// <summary>
        ///     ID:6 
        ///     Alias: OptionID.
        /// </summary>
        public readonly ViewObject ColumnOptionID = new ViewObject(6, "OptionID");

        /// <summary>
        ///     ID:7 
        ///     Alias: OptionCaption
        ///     Caption: $Views_CompletedTasks_CompletionOption.
        /// </summary>
        public readonly ViewObject ColumnOptionCaption = new ViewObject(7, "OptionCaption", "$Views_CompletedTasks_CompletionOption");

        /// <summary>
        ///     ID:8 
        ///     Alias: RoleID.
        /// </summary>
        public readonly ViewObject ColumnRoleID = new ViewObject(8, "RoleID");

        /// <summary>
        ///     ID:9 
        ///     Alias: RoleName
        ///     Caption: $Views_CompletedTasks_Role.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(9, "RoleName", "$Views_CompletedTasks_Role");

        /// <summary>
        ///     ID:10 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(10, "UserID");

        /// <summary>
        ///     ID:11 
        ///     Alias: UserName
        ///     Caption: $Views_CompletedTasks_User.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(11, "UserName", "$Views_CompletedTasks_User");

        /// <summary>
        ///     ID:12 
        ///     Alias: CompletedByID.
        /// </summary>
        public readonly ViewObject ColumnCompletedByID = new ViewObject(12, "CompletedByID");

        /// <summary>
        ///     ID:13 
        ///     Alias: CompletedByName
        ///     Caption: $Views_CompletedTasks_CompletedBy.
        /// </summary>
        public readonly ViewObject ColumnCompletedByName = new ViewObject(13, "CompletedByName", "$Views_CompletedTasks_CompletedBy");

        /// <summary>
        ///     ID:14 
        ///     Alias: AuthorID.
        /// </summary>
        public readonly ViewObject ColumnAuthorID = new ViewObject(14, "AuthorID");

        /// <summary>
        ///     ID:15 
        ///     Alias: AuthorName
        ///     Caption: $Views_CompletedTasks_Author.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(15, "AuthorName", "$Views_CompletedTasks_Author");

        /// <summary>
        ///     ID:16 
        ///     Alias: Result
        ///     Caption: $Views_CompletedTasks_Result.
        /// </summary>
        public readonly ViewObject ColumnResult = new ViewObject(16, "Result", "$Views_CompletedTasks_Result");

        /// <summary>
        ///     ID:17 
        ///     Alias: Created
        ///     Caption: $Views_CompletedTasks_Created.
        /// </summary>
        public readonly ViewObject ColumnCreated = new ViewObject(17, "Created", "$Views_CompletedTasks_Created");

        /// <summary>
        ///     ID:18 
        ///     Alias: Planned
        ///     Caption: $Views_CompletedTasks_Planned.
        /// </summary>
        public readonly ViewObject ColumnPlanned = new ViewObject(18, "Planned", "$Views_CompletedTasks_Planned");

        /// <summary>
        ///     ID:19 
        ///     Alias: Completed
        ///     Caption: $Views_CompletedTasks_Completed.
        /// </summary>
        public readonly ViewObject ColumnCompleted = new ViewObject(19, "Completed", "$Views_CompletedTasks_Completed");

        /// <summary>
        ///     ID:20 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(20, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CompletionDate
        ///     Caption: $Views_CompletedTasks_CompletionDate_Param.
        /// </summary>
        public readonly ViewObject ParamCompletionDate = new ViewObject(0, "CompletionDate", "$Views_CompletedTasks_CompletionDate_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeParam
        ///     Caption: $Views_CompletedTasks_CardType_Param.
        /// </summary>
        public readonly ViewObject ParamTypeParam = new ViewObject(1, "TypeParam", "$Views_CompletedTasks_CardType_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: TaskType
        ///     Caption: $Views_CompletedTasks_TaskType_Param.
        /// </summary>
        public readonly ViewObject ParamTaskType = new ViewObject(2, "TaskType", "$Views_CompletedTasks_TaskType_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: SelUser
        ///     Caption: $Views_CompletedTasks_User_Param.
        /// </summary>
        public readonly ViewObject ParamSelUser = new ViewObject(3, "SelUser", "$Views_CompletedTasks_User_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: CompletedBy
        ///     Caption: $Views_CompletedTasks_CompletedBy_Param.
        /// </summary>
        public readonly ViewObject ParamCompletedBy = new ViewObject(4, "CompletedBy", "$Views_CompletedTasks_CompletedBy_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Role
        ///     Caption: $Views_CompletedTasks_RoleGroup_Param.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(5, "Role", "$Views_CompletedTasks_RoleGroup_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Option
        ///     Caption: $Views_CompletedTasks_CompletionOption_Param.
        /// </summary>
        public readonly ViewObject ParamOption = new ViewObject(6, "Option", "$Views_CompletedTasks_CompletionOption_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: IsDelayed
        ///     Caption: $Views_CompletedTasks_IsDelayed_Param.
        /// </summary>
        public readonly ViewObject ParamIsDelayed = new ViewObject(7, "IsDelayed", "$Views_CompletedTasks_IsDelayed_Param");

        #endregion

        #region ToString

        public static implicit operator string(CompletedTasksViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CompletionOptionCards

    /// <summary>
    ///     ID: {f74f5397-74b2-4b55-8d4e-2cc3031f35af}
    ///     Alias: CompletionOptionCards
    ///     Caption: $Views_Names_CompletionOptionCards
    ///     Group: System
    /// </summary>
    public class CompletionOptionCardsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "CompletionOptionCards": {f74f5397-74b2-4b55-8d4e-2cc3031f35af}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf74f5397,0x74b2,0x4b55,0x8d,0x4e,0x2c,0xc3,0x03,0x1f,0x35,0xaf);

        /// <summary>
        ///     View name for "CompletionOptionCards".
        /// </summary>
        public readonly string Alias = "CompletionOptionCards";

        /// <summary>
        ///     View caption for "CompletionOptionCards".
        /// </summary>
        public readonly string Caption = "$Views_Names_CompletionOptionCards";

        /// <summary>
        ///     View group for "CompletionOptionCards".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: OptionID.
        /// </summary>
        public readonly ViewObject ColumnOptionID = new ViewObject(0, "OptionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: OptionCaption
        ///     Caption: $Views_CompletionOptions_Caption.
        /// </summary>
        public readonly ViewObject ColumnOptionCaption = new ViewObject(1, "OptionCaption", "$Views_CompletionOptions_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: OptionName
        ///     Caption: $Views_CompletionOptions_Alias.
        /// </summary>
        public readonly ViewObject ColumnOptionName = new ViewObject(2, "OptionName", "$Views_CompletionOptions_Alias");

        /// <summary>
        ///     ID:3 
        ///     Alias: PartitionID.
        /// </summary>
        public readonly ViewObject ColumnPartitionID = new ViewObject(3, "PartitionID");

        /// <summary>
        ///     ID:4 
        ///     Alias: PartitionName
        ///     Caption: $Views_CompletionOptions_Partition.
        /// </summary>
        public readonly ViewObject ColumnPartitionName = new ViewObject(4, "PartitionName", "$Views_CompletionOptions_Partition");

        /// <summary>
        ///     ID:5 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(5, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: OptionID
        ///     Caption: OptionID.
        /// </summary>
        public readonly ViewObject ParamOptionID = new ViewObject(0, "OptionID", "OptionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: Caption
        ///     Caption: $Views_CompletionOptions_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(1, "Caption", "$Views_CompletionOptions_Caption_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Name
        ///     Caption: $Views_CompletionOptions_Alias_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(2, "Name", "$Views_CompletionOptions_Alias_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Partition
        ///     Caption: $Views_CompletionOptions_Partition.
        /// </summary>
        public readonly ViewObject ParamPartition = new ViewObject(3, "Partition", "$Views_CompletionOptions_Partition");

        #endregion

        #region ToString

        public static implicit operator string(CompletionOptionCardsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CompletionOptions

    /// <summary>
    ///     ID: {7aa4bb6b-2bd0-469b-aac4-90c46c2d3502}
    ///     Alias: CompletionOptions
    ///     Caption: $Views_Names_CompletionOptions
    ///     Group: System
    /// </summary>
    public class CompletionOptionsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "CompletionOptions": {7aa4bb6b-2bd0-469b-aac4-90c46c2d3502}.
        /// </summary>
        public readonly Guid ID = new Guid(0x7aa4bb6b,0x2bd0,0x469b,0xaa,0xc4,0x90,0xc4,0x6c,0x2d,0x35,0x02);

        /// <summary>
        ///     View name for "CompletionOptions".
        /// </summary>
        public readonly string Alias = "CompletionOptions";

        /// <summary>
        ///     View caption for "CompletionOptions".
        /// </summary>
        public readonly string Caption = "$Views_Names_CompletionOptions";

        /// <summary>
        ///     View group for "CompletionOptions".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: OptionID.
        /// </summary>
        public readonly ViewObject ColumnOptionID = new ViewObject(0, "OptionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: OptionCaption
        ///     Caption: $Views_CompletionOptions_Caption.
        /// </summary>
        public readonly ViewObject ColumnOptionCaption = new ViewObject(1, "OptionCaption", "$Views_CompletionOptions_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: OptionName
        ///     Caption: $Views_CompletionOptions_Alias.
        /// </summary>
        public readonly ViewObject ColumnOptionName = new ViewObject(2, "OptionName", "$Views_CompletionOptions_Alias");

        /// <summary>
        ///     ID:3 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(3, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: OptionID
        ///     Caption: OptionID.
        /// </summary>
        public readonly ViewObject ParamOptionID = new ViewObject(0, "OptionID", "OptionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: Caption
        ///     Caption: $Views_CompletionOptions_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(1, "Caption", "$Views_CompletionOptions_Caption_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Name
        ///     Caption: $Views_CompletionOptions_Alias_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(2, "Name", "$Views_CompletionOptions_Alias_Param");

        #endregion

        #region ToString

        public static implicit operator string(CompletionOptionsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ConditionTypes

    /// <summary>
    ///     ID: {ecb69da2-2b28-41dd-b56d-941dd12df77b}
    ///     Alias: ConditionTypes
    ///     Caption: $Views_Names_ConditionTypes
    ///     Group: System
    /// </summary>
    public class ConditionTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ConditionTypes": {ecb69da2-2b28-41dd-b56d-941dd12df77b}.
        /// </summary>
        public readonly Guid ID = new Guid(0xecb69da2,0x2b28,0x41dd,0xb5,0x6d,0x94,0x1d,0xd1,0x2d,0xf7,0x7b);

        /// <summary>
        ///     View name for "ConditionTypes".
        /// </summary>
        public readonly string Alias = "ConditionTypes";

        /// <summary>
        ///     View caption for "ConditionTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_ConditionTypes";

        /// <summary>
        ///     View group for "ConditionTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ConditionTypeID.
        /// </summary>
        public readonly ViewObject ColumnConditionTypeID = new ViewObject(0, "ConditionTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ConditionTypeName
        ///     Caption: $Views_ConditionTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnConditionTypeName = new ViewObject(1, "ConditionTypeName", "$Views_ConditionTypes_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(2, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_ConditionTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_ConditionTypes_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: UsePlace
        ///     Caption: $Views_ConditionTypes_UsePlace_Param.
        /// </summary>
        public readonly ViewObject ParamUsePlace = new ViewObject(1, "UsePlace", "$Views_ConditionTypes_UsePlace_Param");

        #endregion

        #region ToString

        public static implicit operator string(ConditionTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ConditionUsePlaces

    /// <summary>
    ///     ID: {c0b966a6-aa3a-4ea6-b5ab-a6084099cc1f}
    ///     Alias: ConditionUsePlaces
    ///     Caption: $Views_Names_ConditionUsePlaces
    ///     Group: System
    /// </summary>
    public class ConditionUsePlacesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ConditionUsePlaces": {c0b966a6-aa3a-4ea6-b5ab-a6084099cc1f}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc0b966a6,0xaa3a,0x4ea6,0xb5,0xab,0xa6,0x08,0x40,0x99,0xcc,0x1f);

        /// <summary>
        ///     View name for "ConditionUsePlaces".
        /// </summary>
        public readonly string Alias = "ConditionUsePlaces";

        /// <summary>
        ///     View caption for "ConditionUsePlaces".
        /// </summary>
        public readonly string Caption = "$Views_Names_ConditionUsePlaces";

        /// <summary>
        ///     View group for "ConditionUsePlaces".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ConditionUsePlaceID.
        /// </summary>
        public readonly ViewObject ColumnConditionUsePlaceID = new ViewObject(0, "ConditionUsePlaceID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ConditionUsePlaceName
        ///     Caption: $Views_ConditionUsePlaces_Name.
        /// </summary>
        public readonly ViewObject ColumnConditionUsePlaceName = new ViewObject(1, "ConditionUsePlaceName", "$Views_ConditionUsePlaces_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(2, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_ConditionUsePlaces_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_ConditionUsePlaces_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(ConditionUsePlacesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ContractsDocuments

    /// <summary>
    ///     ID: {24f43f33-9b1b-476d-aa33-3deb11b9fe3b}
    ///     Alias: ContractsDocuments
    ///     Caption: $Views_Names_ContractsDocuments
    ///     Group: KrDocuments
    /// </summary>
    public class ContractsDocumentsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ContractsDocuments": {24f43f33-9b1b-476d-aa33-3deb11b9fe3b}.
        /// </summary>
        public readonly Guid ID = new Guid(0x24f43f33,0x9b1b,0x476d,0xaa,0x33,0x3d,0xeb,0x11,0xb9,0xfe,0x3b);

        /// <summary>
        ///     View name for "ContractsDocuments".
        /// </summary>
        public readonly string Alias = "ContractsDocuments";

        /// <summary>
        ///     View caption for "ContractsDocuments".
        /// </summary>
        public readonly string Caption = "$Views_Names_ContractsDocuments";

        /// <summary>
        ///     View group for "ContractsDocuments".
        /// </summary>
        public readonly string Group = "KrDocuments";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DocID.
        /// </summary>
        public readonly ViewObject ColumnDocID = new ViewObject(0, "DocID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DocNumber
        ///     Caption: $Views_Registers_Number.
        /// </summary>
        public readonly ViewObject ColumnDocNumber = new ViewObject(1, "DocNumber", "$Views_Registers_Number");

        /// <summary>
        ///     ID:2 
        ///     Alias: SubTypeTitle
        ///     Caption: $Views_Registers_DocType.
        /// </summary>
        public readonly ViewObject ColumnSubTypeTitle = new ViewObject(2, "SubTypeTitle", "$Views_Registers_DocType");

        /// <summary>
        ///     ID:3 
        ///     Alias: DocSubject
        ///     Caption: $Views_Registers_Subject.
        /// </summary>
        public readonly ViewObject ColumnDocSubject = new ViewObject(3, "DocSubject", "$Views_Registers_Subject");

        /// <summary>
        ///     ID:4 
        ///     Alias: DocDescription
        ///     Caption: $Views_Registers_DocDescription.
        /// </summary>
        public readonly ViewObject ColumnDocDescription = new ViewObject(4, "DocDescription", "$Views_Registers_DocDescription");

        /// <summary>
        ///     ID:5 
        ///     Alias: DocAmount
        ///     Caption: $Views_Registers_Sum.
        /// </summary>
        public readonly ViewObject ColumnDocAmount = new ViewObject(5, "DocAmount", "$Views_Registers_Sum");

        /// <summary>
        ///     ID:6 
        ///     Alias: PartnerID.
        /// </summary>
        public readonly ViewObject ColumnPartnerID = new ViewObject(6, "PartnerID");

        /// <summary>
        ///     ID:7 
        ///     Alias: PartnerName
        ///     Caption: $Views_Registers_Partner.
        /// </summary>
        public readonly ViewObject ColumnPartnerName = new ViewObject(7, "PartnerName", "$Views_Registers_Partner");

        /// <summary>
        ///     ID:8 
        ///     Alias: AuthorID.
        /// </summary>
        public readonly ViewObject ColumnAuthorID = new ViewObject(8, "AuthorID");

        /// <summary>
        ///     ID:9 
        ///     Alias: AuthorName
        ///     Caption: $Views_Registers_Author.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(9, "AuthorName", "$Views_Registers_Author");

        /// <summary>
        ///     ID:10 
        ///     Alias: RegistratorID.
        /// </summary>
        public readonly ViewObject ColumnRegistratorID = new ViewObject(10, "RegistratorID");

        /// <summary>
        ///     ID:11 
        ///     Alias: RegistratorName
        ///     Caption: $Views_Registers_Registrator.
        /// </summary>
        public readonly ViewObject ColumnRegistratorName = new ViewObject(11, "RegistratorName", "$Views_Registers_Registrator");

        /// <summary>
        ///     ID:12 
        ///     Alias: KrState
        ///     Caption: $Views_Registers_State.
        /// </summary>
        public readonly ViewObject ColumnKrState = new ViewObject(12, "KrState", "$Views_Registers_State");

        /// <summary>
        ///     ID:13 
        ///     Alias: DocDate
        ///     Caption: $Views_Registers_DocDate.
        /// </summary>
        public readonly ViewObject ColumnDocDate = new ViewObject(13, "DocDate", "$Views_Registers_DocDate");

        /// <summary>
        ///     ID:14 
        ///     Alias: CreationDate
        ///     Caption: $Views_Registers_CreationDate.
        /// </summary>
        public readonly ViewObject ColumnCreationDate = new ViewObject(14, "CreationDate", "$Views_Registers_CreationDate");

        /// <summary>
        ///     ID:15 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department.
        /// </summary>
        public readonly ViewObject ColumnDepartment = new ViewObject(15, "Department", "$Views_Registers_Department");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: IsAuthor
        ///     Caption: $Views_Registers_IsAuthor_Param.
        /// </summary>
        public readonly ViewObject ParamIsAuthor = new ViewObject(0, "IsAuthor", "$Views_Registers_IsAuthor_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: IsInitiator
        ///     Caption: $Views_Registers_IsInitiator_Param.
        /// </summary>
        public readonly ViewObject ParamIsInitiator = new ViewObject(1, "IsInitiator", "$Views_Registers_IsInitiator_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: IsRegistrator
        ///     Caption: $Views_Registers_IsRegistrator_Param.
        /// </summary>
        public readonly ViewObject ParamIsRegistrator = new ViewObject(2, "IsRegistrator", "$Views_Registers_IsRegistrator_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Partner
        ///     Caption: $Views_Registers_Partner_Param.
        /// </summary>
        public readonly ViewObject ParamPartner = new ViewObject(3, "Partner", "$Views_Registers_Partner_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Number
        ///     Caption: $Views_Registers_Number_Param.
        /// </summary>
        public readonly ViewObject ParamNumber = new ViewObject(4, "Number", "$Views_Registers_Number_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Subject
        ///     Caption: $Views_Registers_Subject_Param.
        /// </summary>
        public readonly ViewObject ParamSubject = new ViewObject(5, "Subject", "$Views_Registers_Subject_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: DocDate
        ///     Caption: $Views_Registers_DocDate_Param.
        /// </summary>
        public readonly ViewObject ParamDocDate = new ViewObject(6, "DocDate", "$Views_Registers_DocDate_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: Amount
        ///     Caption: $Views_Registers_Sum_Param.
        /// </summary>
        public readonly ViewObject ParamAmount = new ViewObject(7, "Amount", "$Views_Registers_Sum_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: Currency
        ///     Caption: $Views_Registers_Currency_Param.
        /// </summary>
        public readonly ViewObject ParamCurrency = new ViewObject(8, "Currency", "$Views_Registers_Currency_Param");

        /// <summary>
        ///     ID:9 
        ///     Alias: Author
        ///     Caption: $Views_Registers_Author_Param.
        /// </summary>
        public readonly ViewObject ParamAuthor = new ViewObject(9, "Author", "$Views_Registers_Author_Param");

        /// <summary>
        ///     ID:10 
        ///     Alias: Registrator
        ///     Caption: $Views_Registers_Registrator_Param.
        /// </summary>
        public readonly ViewObject ParamRegistrator = new ViewObject(10, "Registrator", "$Views_Registers_Registrator_Param");

        /// <summary>
        ///     ID:11 
        ///     Alias: State
        ///     Caption: $Views_Registers_State_Param.
        /// </summary>
        public readonly ViewObject ParamState = new ViewObject(11, "State", "$Views_Registers_State_Param");

        /// <summary>
        ///     ID:12 
        ///     Alias: DocType
        ///     Caption: $Views_Registers_DocType_Param.
        /// </summary>
        public readonly ViewObject ParamDocType = new ViewObject(12, "DocType", "$Views_Registers_DocType_Param");

        /// <summary>
        ///     ID:13 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(13, "Department", "$Views_Registers_Department_Param");

        #endregion

        #region ToString

        public static implicit operator string(ContractsDocumentsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region CreateFileFromTemplate

    /// <summary>
    ///     ID: {9334eab6-f2b7-4c35-b0ff-bf764cd0092c}
    ///     Alias: CreateFileFromTemplate
    ///     Caption: $Views_Names_CreateFileFromTemplate
    ///     Group: System
    /// </summary>
    public class CreateFileFromTemplateViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "CreateFileFromTemplate": {9334eab6-f2b7-4c35-b0ff-bf764cd0092c}.
        /// </summary>
        public readonly Guid ID = new Guid(0x9334eab6,0xf2b7,0x4c35,0xb0,0xff,0xbf,0x76,0x4c,0xd0,0x09,0x2c);

        /// <summary>
        ///     View name for "CreateFileFromTemplate".
        /// </summary>
        public readonly string Alias = "CreateFileFromTemplate";

        /// <summary>
        ///     View caption for "CreateFileFromTemplate".
        /// </summary>
        public readonly string Caption = "$Views_Names_CreateFileFromTemplate";

        /// <summary>
        ///     View group for "CreateFileFromTemplate".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: FileTemplateID.
        /// </summary>
        public readonly ViewObject ColumnFileTemplateID = new ViewObject(0, "FileTemplateID");

        /// <summary>
        ///     ID:1 
        ///     Alias: FileTemplateName
        ///     Caption: $Cards_FileFromTemplate_TemplateNameColumn.
        /// </summary>
        public readonly ViewObject ColumnFileTemplateName = new ViewObject(1, "FileTemplateName", "$Cards_FileFromTemplate_TemplateNameColumn");

        /// <summary>
        ///     ID:2 
        ///     Alias: FileTemplateFileExtension.
        /// </summary>
        public readonly ViewObject ColumnFileTemplateFileExtension = new ViewObject(2, "FileTemplateFileExtension");

        /// <summary>
        ///     ID:3 
        ///     Alias: FileTemplateFileExtensionText
        ///     Caption: $Cards_FileFromTemplate_TemplateExtensionColumn.
        /// </summary>
        public readonly ViewObject ColumnFileTemplateFileExtensionText = new ViewObject(3, "FileTemplateFileExtensionText", "$Cards_FileFromTemplate_TemplateExtensionColumn");

        /// <summary>
        ///     ID:4 
        ///     Alias: FileTemplateFileName.
        /// </summary>
        public readonly ViewObject ColumnFileTemplateFileName = new ViewObject(4, "FileTemplateFileName");

        /// <summary>
        ///     ID:5 
        ///     Alias: FileTemplateGroupName
        ///     Caption: $Cards_FileFromTemplate_TemplateGroupNameColumn.
        /// </summary>
        public readonly ViewObject ColumnFileTemplateGroupName = new ViewObject(5, "FileTemplateGroupName", "$Cards_FileFromTemplate_TemplateGroupNameColumn");

        /// <summary>
        ///     ID:6 
        ///     Alias: ConvertToPdf.
        /// </summary>
        public readonly ViewObject ColumnConvertToPdf = new ViewObject(6, "ConvertToPdf");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name");

        #endregion

        #region ToString

        public static implicit operator string(CreateFileFromTemplateViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Currencies

    /// <summary>
    ///     ID: {67e0e026-8dbd-462a-93fa-9ec03636564f}
    ///     Alias: Currencies
    ///     Caption: $Views_Names_Currencies
    ///     Group: System
    /// </summary>
    public class CurrenciesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Currencies": {67e0e026-8dbd-462a-93fa-9ec03636564f}.
        /// </summary>
        public readonly Guid ID = new Guid(0x67e0e026,0x8dbd,0x462a,0x93,0xfa,0x9e,0xc0,0x36,0x36,0x56,0x4f);

        /// <summary>
        ///     View name for "Currencies".
        /// </summary>
        public readonly string Alias = "Currencies";

        /// <summary>
        ///     View caption for "Currencies".
        /// </summary>
        public readonly string Caption = "$Views_Names_Currencies";

        /// <summary>
        ///     View group for "Currencies".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CurrencyID.
        /// </summary>
        public readonly ViewObject ColumnCurrencyID = new ViewObject(0, "CurrencyID");

        /// <summary>
        ///     ID:1 
        ///     Alias: CurrencyName
        ///     Caption: $Views_Currencies_Name.
        /// </summary>
        public readonly ViewObject ColumnCurrencyName = new ViewObject(1, "CurrencyName", "$Views_Currencies_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: CurrencyCaption
        ///     Caption: $Views_Currencies_Caption.
        /// </summary>
        public readonly ViewObject ColumnCurrencyCaption = new ViewObject(2, "CurrencyCaption", "$Views_Currencies_Caption");

        /// <summary>
        ///     ID:3 
        ///     Alias: CurrencyCode
        ///     Caption: $Views_Currencies_Code.
        /// </summary>
        public readonly ViewObject ColumnCurrencyCode = new ViewObject(3, "CurrencyCode", "$Views_Currencies_Code");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Currencies_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Currencies_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(CurrenciesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DateFormats

    /// <summary>
    ///     ID: {10ad5b14-16cd-4c8c-ad1f-63c24daeb00c}
    ///     Alias: DateFormats
    ///     Caption: $Views_Names_DateFormats
    ///     Group: System
    /// </summary>
    public class DateFormatsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "DateFormats": {10ad5b14-16cd-4c8c-ad1f-63c24daeb00c}.
        /// </summary>
        public readonly Guid ID = new Guid(0x10ad5b14,0x16cd,0x4c8c,0xad,0x1f,0x63,0xc2,0x4d,0xae,0xb0,0x0c);

        /// <summary>
        ///     View name for "DateFormats".
        /// </summary>
        public readonly string Alias = "DateFormats";

        /// <summary>
        ///     View caption for "DateFormats".
        /// </summary>
        public readonly string Caption = "$Views_Names_DateFormats";

        /// <summary>
        ///     View group for "DateFormats".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DateFormatID.
        /// </summary>
        public readonly ViewObject ColumnDateFormatID = new ViewObject(0, "DateFormatID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DateFormatName.
        /// </summary>
        public readonly ViewObject ColumnDateFormatName = new ViewObject(1, "DateFormatName");

        /// <summary>
        ///     ID:2 
        ///     Alias: DateFormatCaption
        ///     Caption: $Views_DateFormats_Caption.
        /// </summary>
        public readonly ViewObject ColumnDateFormatCaption = new ViewObject(2, "DateFormatCaption", "$Views_DateFormats_Caption");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_DateFormats_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_DateFormats_Caption_Param");

        #endregion

        #region ToString

        public static implicit operator string(DateFormatsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Deleted

    /// <summary>
    ///     ID: {52c2fe9f-b0a8-455a-b426-ab5bc2285a05}
    ///     Alias: Deleted
    ///     Caption: $Views_Names_Deleted
    ///     Group: System
    /// </summary>
    public class DeletedViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Deleted": {52c2fe9f-b0a8-455a-b426-ab5bc2285a05}.
        /// </summary>
        public readonly Guid ID = new Guid(0x52c2fe9f,0xb0a8,0x455a,0xb4,0x26,0xab,0x5b,0xc2,0x28,0x5a,0x05);

        /// <summary>
        ///     View name for "Deleted".
        /// </summary>
        public readonly string Alias = "Deleted";

        /// <summary>
        ///     View caption for "Deleted".
        /// </summary>
        public readonly string Caption = "$Views_Names_Deleted";

        /// <summary>
        ///     View group for "Deleted".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DeletedID.
        /// </summary>
        public readonly ViewObject ColumnDeletedID = new ViewObject(0, "DeletedID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DeletedCaption
        ///     Caption: $Views_Deleted_Caption.
        /// </summary>
        public readonly ViewObject ColumnDeletedCaption = new ViewObject(1, "DeletedCaption", "$Views_Deleted_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(2, "TypeID");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeCaption
        ///     Caption: $Views_Deleted_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(3, "TypeCaption", "$Views_Deleted_Type");

        /// <summary>
        ///     ID:4 
        ///     Alias: Date
        ///     Caption: $Views_Deleted_Date.
        /// </summary>
        public readonly ViewObject ColumnDate = new ViewObject(4, "Date", "$Views_Deleted_Date");

        /// <summary>
        ///     ID:5 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(5, "UserID");

        /// <summary>
        ///     ID:6 
        ///     Alias: UserName
        ///     Caption: $Views_Deleted_User.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(6, "UserName", "$Views_Deleted_User");

        /// <summary>
        ///     ID:7 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(7, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: DeletedCaption
        ///     Caption: $Views_Deleted_Card_Param.
        /// </summary>
        public readonly ViewObject ParamDeletedCaption = new ViewObject(0, "DeletedCaption", "$Views_Deleted_Card_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeID
        ///     Caption: $Views_Deleted_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeID = new ViewObject(1, "TypeID", "$Views_Deleted_Type_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeCaption
        ///     Caption: $Views_Deleted_TypeName_Param.
        /// </summary>
        public readonly ViewObject ParamTypeCaption = new ViewObject(2, "TypeCaption", "$Views_Deleted_TypeName_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Date
        ///     Caption: $Views_Deleted_Date_Param.
        /// </summary>
        public readonly ViewObject ParamDate = new ViewObject(3, "Date", "$Views_Deleted_Date_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: UserID
        ///     Caption: $Views_Deleted_User_Param.
        /// </summary>
        public readonly ViewObject ParamUserID = new ViewObject(4, "UserID", "$Views_Deleted_User_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: UserName
        ///     Caption: $Views_Deleted_UserName_Param.
        /// </summary>
        public readonly ViewObject ParamUserName = new ViewObject(5, "UserName", "$Views_Deleted_UserName_Param");

        #endregion

        #region ToString

        public static implicit operator string(DeletedViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Departments

    /// <summary>
    ///     ID: {ab58bf23-b9d7-4b51-97c1-c9517daa7993}
    ///     Alias: Departments
    ///     Caption: $Views_Names_Departments
    ///     Group: System
    /// </summary>
    public class DepartmentsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Departments": {ab58bf23-b9d7-4b51-97c1-c9517daa7993}.
        /// </summary>
        public readonly Guid ID = new Guid(0xab58bf23,0xb9d7,0x4b51,0x97,0xc1,0xc9,0x51,0x7d,0xaa,0x79,0x93);

        /// <summary>
        ///     View name for "Departments".
        /// </summary>
        public readonly string Alias = "Departments";

        /// <summary>
        ///     View caption for "Departments".
        /// </summary>
        public readonly string Caption = "$Views_Names_Departments";

        /// <summary>
        ///     View group for "Departments".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RoleID.
        /// </summary>
        public readonly ViewObject ColumnRoleID = new ViewObject(0, "RoleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleName
        ///     Caption: $Views_Departments_Department.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(1, "RoleName", "$Views_Departments_Department");

        /// <summary>
        ///     ID:2 
        ///     Alias: HeadUserID.
        /// </summary>
        public readonly ViewObject ColumnHeadUserID = new ViewObject(2, "HeadUserID");

        /// <summary>
        ///     ID:3 
        ///     Alias: HeadUserName
        ///     Caption: $Views_Departments_HeadUser.
        /// </summary>
        public readonly ViewObject ColumnHeadUserName = new ViewObject(3, "HeadUserName", "$Views_Departments_HeadUser");

        /// <summary>
        ///     ID:4 
        ///     Alias: ParentRoleID.
        /// </summary>
        public readonly ViewObject ColumnParentRoleID = new ViewObject(4, "ParentRoleID");

        /// <summary>
        ///     ID:5 
        ///     Alias: ParentRoleName
        ///     Caption: $Views_Departments_ParentDepartment.
        /// </summary>
        public readonly ViewObject ColumnParentRoleName = new ViewObject(5, "ParentRoleName", "$Views_Departments_ParentDepartment");

        /// <summary>
        ///     ID:6 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(6, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Departments_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Departments_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: UserName
        ///     Caption: $Views_Departments_User_Param.
        /// </summary>
        public readonly ViewObject ParamUserName = new ViewObject(1, "UserName", "$Views_Departments_User_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: DepID
        ///     Caption: $Views_Departments_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepID = new ViewObject(2, "DepID", "$Views_Departments_Department_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: ParentDepID
        ///     Caption: $Views_Departments_ParentDep_Param.
        /// </summary>
        public readonly ViewObject ParamParentDepID = new ViewObject(3, "ParentDepID", "$Views_Departments_ParentDep_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: ShowHidden
        ///     Caption: $Views_Departments_ShowHidden_Param.
        /// </summary>
        public readonly ViewObject ParamShowHidden = new ViewObject(4, "ShowHidden", "$Views_Departments_ShowHidden_Param");

        #endregion

        #region ToString

        public static implicit operator string(DepartmentsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DeputiesManagement

    /// <summary>
    ///     ID: {4c2769bc-89ca-4b08-bc53-d6ee93a45b95}
    ///     Alias: DeputiesManagement
    ///     Caption: $Views_Names_DeputiesManagement
    ///     Group: System
    /// </summary>
    public class DeputiesManagementViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "DeputiesManagement": {4c2769bc-89ca-4b08-bc53-d6ee93a45b95}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4c2769bc,0x89ca,0x4b08,0xbc,0x53,0xd6,0xee,0x93,0xa4,0x5b,0x95);

        /// <summary>
        ///     View name for "DeputiesManagement".
        /// </summary>
        public readonly string Alias = "DeputiesManagement";

        /// <summary>
        ///     View caption for "DeputiesManagement".
        /// </summary>
        public readonly string Caption = "$Views_Names_DeputiesManagement";

        /// <summary>
        ///     View group for "DeputiesManagement".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(0, "UserID");

        /// <summary>
        ///     ID:1 
        ///     Alias: UserName
        ///     Caption: $Views_Users_Name.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(1, "UserName", "$Views_Users_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(2, "rn");

        /// <summary>
        ///     ID:3 
        ///     Alias: RecordID.
        /// </summary>
        public readonly ViewObject ColumnRecordID = new ViewObject(3, "RecordID");

        /// <summary>
        ///     ID:4 
        ///     Alias: RecordCaption.
        /// </summary>
        public readonly ViewObject ColumnRecordCaption = new ViewObject(4, "RecordCaption");

        /// <summary>
        ///     ID:5 
        ///     Alias: Departments
        ///     Caption: $Views_Users_Departments.
        /// </summary>
        public readonly ViewObject ColumnDepartments = new ViewObject(5, "Departments", "$Views_Users_Departments");

        /// <summary>
        ///     ID:6 
        ///     Alias: StaticRoles
        ///     Caption: $Views_Users_StaticRoles.
        /// </summary>
        public readonly ViewObject ColumnStaticRoles = new ViewObject(6, "StaticRoles", "$Views_Users_StaticRoles");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: UserID
        ///     Caption: $Views_DeputiesManagement_User_Param.
        /// </summary>
        public readonly ViewObject ParamUserID = new ViewObject(0, "UserID", "$Views_DeputiesManagement_User_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: UserName
        ///     Caption: $Views_Users_Name_Param.
        /// </summary>
        public readonly ViewObject ParamUserName = new ViewObject(1, "UserName", "$Views_Users_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(DeputiesManagementViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DeviceTypes

    /// <summary>
    ///     ID: {4a9aaa12-6830-4dc5-bd0d-c31415f7a306}
    ///     Alias: DeviceTypes
    ///     Caption: $Views_Names_DeviceTypes
    ///     Group: System
    /// </summary>
    public class DeviceTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "DeviceTypes": {4a9aaa12-6830-4dc5-bd0d-c31415f7a306}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4a9aaa12,0x6830,0x4dc5,0xbd,0x0d,0xc3,0x14,0x15,0xf7,0xa3,0x06);

        /// <summary>
        ///     View name for "DeviceTypes".
        /// </summary>
        public readonly string Alias = "DeviceTypes";

        /// <summary>
        ///     View caption for "DeviceTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_DeviceTypes";

        /// <summary>
        ///     View group for "DeviceTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: $Views_DeviceTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "$Views_DeviceTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_DeviceTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_DeviceTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(DeviceTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DialogButtonTypes

    /// <summary>
    ///     ID: {bf4ac076-a8b3-4271-867e-f8f7ae9287a6}
    ///     Alias: DialogButtonTypes
    ///     Caption: $Views_Names_DialogButtonTypes
    ///     Group: System
    /// </summary>
    public class DialogButtonTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "DialogButtonTypes": {bf4ac076-a8b3-4271-867e-f8f7ae9287a6}.
        /// </summary>
        public readonly Guid ID = new Guid(0xbf4ac076,0xa8b3,0x4271,0x86,0x7e,0xf8,0xf7,0xae,0x92,0x87,0xa6);

        /// <summary>
        ///     View name for "DialogButtonTypes".
        /// </summary>
        public readonly string Alias = "DialogButtonTypes";

        /// <summary>
        ///     View caption for "DialogButtonTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_DialogButtonTypes";

        /// <summary>
        ///     View group for "DialogButtonTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: RefName.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "RefName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "Name");

        #endregion

        #region ToString

        public static implicit operator string(DialogButtonTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DialogCardAutoOpenModes

    /// <summary>
    ///     ID: {115854cc-71a8-45ae-ab4f-940962b332c6}
    ///     Alias: DialogCardAutoOpenModes
    ///     Caption: $Views_Names_DialogCardAutoOpenModes
    ///     Group: System
    /// </summary>
    public class DialogCardAutoOpenModesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "DialogCardAutoOpenModes": {115854cc-71a8-45ae-ab4f-940962b332c6}.
        /// </summary>
        public readonly Guid ID = new Guid(0x115854cc,0x71a8,0x45ae,0xab,0x4f,0x94,0x09,0x62,0xb3,0x32,0xc6);

        /// <summary>
        ///     View name for "DialogCardAutoOpenModes".
        /// </summary>
        public readonly string Alias = "DialogCardAutoOpenModes";

        /// <summary>
        ///     View caption for "DialogCardAutoOpenModes".
        /// </summary>
        public readonly string Caption = "$Views_Names_DialogCardAutoOpenModes";

        /// <summary>
        ///     View group for "DialogCardAutoOpenModes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: RefName.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "RefName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "Name");

        #endregion

        #region ToString

        public static implicit operator string(DialogCardAutoOpenModesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DialogCardStoreModes

    /// <summary>
    ///     ID: {ad759faa-bfc1-4cd1-a322-f1eb1a42b3bc}
    ///     Alias: DialogCardStoreModes
    ///     Caption: $Views_Names_DialogCardStoreModes
    ///     Group: System
    /// </summary>
    public class DialogCardStoreModesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "DialogCardStoreModes": {ad759faa-bfc1-4cd1-a322-f1eb1a42b3bc}.
        /// </summary>
        public readonly Guid ID = new Guid(0xad759faa,0xbfc1,0x4cd1,0xa3,0x22,0xf1,0xeb,0x1a,0x42,0xb3,0xbc);

        /// <summary>
        ///     View name for "DialogCardStoreModes".
        /// </summary>
        public readonly string Alias = "DialogCardStoreModes";

        /// <summary>
        ///     View caption for "DialogCardStoreModes".
        /// </summary>
        public readonly string Caption = "$Views_Names_DialogCardStoreModes";

        /// <summary>
        ///     View group for "DialogCardStoreModes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: RefName.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "RefName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "Name");

        #endregion

        #region ToString

        public static implicit operator string(DialogCardStoreModesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DocumentCategories

    /// <summary>
    ///     ID: {15fc8ec2-f206-4de1-b942-1c29c931213f}
    ///     Alias: DocumentCategories
    ///     Caption: $Views_Names_DocumentCategories
    ///     Group: System
    /// </summary>
    public class DocumentCategoriesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "DocumentCategories": {15fc8ec2-f206-4de1-b942-1c29c931213f}.
        /// </summary>
        public readonly Guid ID = new Guid(0x15fc8ec2,0xf206,0x4de1,0xb9,0x42,0x1c,0x29,0xc9,0x31,0x21,0x3f);

        /// <summary>
        ///     View name for "DocumentCategories".
        /// </summary>
        public readonly string Alias = "DocumentCategories";

        /// <summary>
        ///     View caption for "DocumentCategories".
        /// </summary>
        public readonly string Caption = "$Views_Names_DocumentCategories";

        /// <summary>
        ///     View group for "DocumentCategories".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CategoryID.
        /// </summary>
        public readonly ViewObject ColumnCategoryID = new ViewObject(0, "CategoryID");

        /// <summary>
        ///     ID:1 
        ///     Alias: CategoryName
        ///     Caption: $Views_DocumentCategories_Name.
        /// </summary>
        public readonly ViewObject ColumnCategoryName = new ViewObject(1, "CategoryName", "$Views_DocumentCategories_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(2, "TypeID");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeCaption
        ///     Caption: $Views_DocumentCategories_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(3, "TypeCaption", "$Views_DocumentCategories_Type");

        /// <summary>
        ///     ID:4 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(4, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_DocumentCategories_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_DocumentCategories_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Type
        ///     Caption: $Views_DocumentCategories_Type_Param.
        /// </summary>
        public readonly ViewObject ParamType = new ViewObject(1, "Type", "$Views_DocumentCategories_Type_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeID
        ///     Caption: $Views_DocumentCategories_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeID = new ViewObject(2, "TypeID", "$Views_DocumentCategories_Type_Param");

        #endregion

        #region ToString

        public static implicit operator string(DocumentCategoriesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Documents

    /// <summary>
    ///     ID: {8354ee75-639a-4084-808d-cf97a2b86be9}
    ///     Alias: Documents
    ///     Caption: $Views_Names_Documents
    ///     Group: KrDocuments
    /// </summary>
    public class DocumentsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Documents": {8354ee75-639a-4084-808d-cf97a2b86be9}.
        /// </summary>
        public readonly Guid ID = new Guid(0x8354ee75,0x639a,0x4084,0x80,0x8d,0xcf,0x97,0xa2,0xb8,0x6b,0xe9);

        /// <summary>
        ///     View name for "Documents".
        /// </summary>
        public readonly string Alias = "Documents";

        /// <summary>
        ///     View caption for "Documents".
        /// </summary>
        public readonly string Caption = "$Views_Names_Documents";

        /// <summary>
        ///     View group for "Documents".
        /// </summary>
        public readonly string Group = "KrDocuments";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DocID.
        /// </summary>
        public readonly ViewObject ColumnDocID = new ViewObject(0, "DocID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DocNumber
        ///     Caption: $Views_Registers_Number.
        /// </summary>
        public readonly ViewObject ColumnDocNumber = new ViewObject(1, "DocNumber", "$Views_Registers_Number");

        /// <summary>
        ///     ID:2 
        ///     Alias: SubTypeTitle
        ///     Caption: $Views_Registers_DocType.
        /// </summary>
        public readonly ViewObject ColumnSubTypeTitle = new ViewObject(2, "SubTypeTitle", "$Views_Registers_DocType");

        /// <summary>
        ///     ID:3 
        ///     Alias: DocSubject
        ///     Caption: $Views_Registers_Subject.
        /// </summary>
        public readonly ViewObject ColumnDocSubject = new ViewObject(3, "DocSubject", "$Views_Registers_Subject");

        /// <summary>
        ///     ID:4 
        ///     Alias: DocDescription
        ///     Caption: $Views_Registers_DocDescription.
        /// </summary>
        public readonly ViewObject ColumnDocDescription = new ViewObject(4, "DocDescription", "$Views_Registers_DocDescription");

        /// <summary>
        ///     ID:5 
        ///     Alias: AuthorID.
        /// </summary>
        public readonly ViewObject ColumnAuthorID = new ViewObject(5, "AuthorID");

        /// <summary>
        ///     ID:6 
        ///     Alias: AuthorName
        ///     Caption: $Views_Registers_Author.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(6, "AuthorName", "$Views_Registers_Author");

        /// <summary>
        ///     ID:7 
        ///     Alias: RegistratorID.
        /// </summary>
        public readonly ViewObject ColumnRegistratorID = new ViewObject(7, "RegistratorID");

        /// <summary>
        ///     ID:8 
        ///     Alias: RegistratorName
        ///     Caption: $Views_Registers_Registrator.
        /// </summary>
        public readonly ViewObject ColumnRegistratorName = new ViewObject(8, "RegistratorName", "$Views_Registers_Registrator");

        /// <summary>
        ///     ID:9 
        ///     Alias: KrState
        ///     Caption: $Views_Registers_State.
        /// </summary>
        public readonly ViewObject ColumnKrState = new ViewObject(9, "KrState", "$Views_Registers_State");

        /// <summary>
        ///     ID:10 
        ///     Alias: DocDate
        ///     Caption: $Views_Registers_DocDate.
        /// </summary>
        public readonly ViewObject ColumnDocDate = new ViewObject(10, "DocDate", "$Views_Registers_DocDate");

        /// <summary>
        ///     ID:11 
        ///     Alias: CreationDate
        ///     Caption: $Views_Registers_CreationDate.
        /// </summary>
        public readonly ViewObject ColumnCreationDate = new ViewObject(11, "CreationDate", "$Views_Registers_CreationDate");

        /// <summary>
        ///     ID:12 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department.
        /// </summary>
        public readonly ViewObject ColumnDepartment = new ViewObject(12, "Department", "$Views_Registers_Department");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: IsAuthor
        ///     Caption: $Views_Registers_IsAuthor_Param.
        /// </summary>
        public readonly ViewObject ParamIsAuthor = new ViewObject(0, "IsAuthor", "$Views_Registers_IsAuthor_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: IsInitiator
        ///     Caption: $Views_Registers_IsInitiator_Param.
        /// </summary>
        public readonly ViewObject ParamIsInitiator = new ViewObject(1, "IsInitiator", "$Views_Registers_IsInitiator_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: IsRegistrator
        ///     Caption: $Views_Registers_IsRegistrator_Param.
        /// </summary>
        public readonly ViewObject ParamIsRegistrator = new ViewObject(2, "IsRegistrator", "$Views_Registers_IsRegistrator_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Number
        ///     Caption: $Views_Registers_Number_Param.
        /// </summary>
        public readonly ViewObject ParamNumber = new ViewObject(3, "Number", "$Views_Registers_Number_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Subject
        ///     Caption: $Views_Registers_Subject_Param.
        /// </summary>
        public readonly ViewObject ParamSubject = new ViewObject(4, "Subject", "$Views_Registers_Subject_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: DocDate
        ///     Caption: $Views_Registers_DocDate_Param.
        /// </summary>
        public readonly ViewObject ParamDocDate = new ViewObject(5, "DocDate", "$Views_Registers_DocDate_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Author
        ///     Caption: $Views_Registers_Author_Param.
        /// </summary>
        public readonly ViewObject ParamAuthor = new ViewObject(6, "Author", "$Views_Registers_Author_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: Registrator
        ///     Caption: $Views_Registers_Registrator_Param.
        /// </summary>
        public readonly ViewObject ParamRegistrator = new ViewObject(7, "Registrator", "$Views_Registers_Registrator_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: State
        ///     Caption: $Views_Registers_State_Param.
        /// </summary>
        public readonly ViewObject ParamState = new ViewObject(8, "State", "$Views_Registers_State_Param");

        /// <summary>
        ///     ID:9 
        ///     Alias: DocType
        ///     Caption: $Views_Registers_DocType_Param.
        /// </summary>
        public readonly ViewObject ParamDocType = new ViewObject(9, "DocType", "$Views_Registers_DocType_Param");

        /// <summary>
        ///     ID:10 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(10, "Department", "$Views_Registers_Department_Param");

        #endregion

        #region ToString

        public static implicit operator string(DocumentsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DocumentTypes

    /// <summary>
    ///     ID: {b05eebcc-eb4b-4f5c-b4b8-d8bd134e27c6}
    ///     Alias: DocumentTypes
    ///     Caption: $Views_Names_DocumentTypes
    ///     Group: System
    /// </summary>
    public class DocumentTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "DocumentTypes": {b05eebcc-eb4b-4f5c-b4b8-d8bd134e27c6}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb05eebcc,0xeb4b,0x4f5c,0xb4,0xb8,0xd8,0xbd,0x13,0x4e,0x27,0xc6);

        /// <summary>
        ///     View name for "DocumentTypes".
        /// </summary>
        public readonly string Alias = "DocumentTypes";

        /// <summary>
        ///     View caption for "DocumentTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_DocumentTypes";

        /// <summary>
        ///     View group for "DocumentTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeCaption
        ///     Caption: $View_DocumentTypes_Caption.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(1, "TypeCaption", "$View_DocumentTypes_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeName
        ///     Caption: $View_DocumentTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(2, "TypeName", "$View_DocumentTypes_Name");

        /// <summary>
        ///     ID:3 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(3, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $View_DocumentTypes_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$View_DocumentTypes_Caption_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $View_DocumentTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$View_DocumentTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(DocumentTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region DurableRoles

    /// <summary>
    ///     ID: {8144d12b-ac9b-4da7-a21c-4ad1ca355dbe}
    ///     Alias: DurableRoles
    ///     Caption: $Views_Names_DurableRoles
    ///     Group: System
    /// </summary>
    public class DurableRolesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "DurableRoles": {8144d12b-ac9b-4da7-a21c-4ad1ca355dbe}.
        /// </summary>
        public readonly Guid ID = new Guid(0x8144d12b,0xac9b,0x4da7,0xa2,0x1c,0x4a,0xd1,0xca,0x35,0x5d,0xbe);

        /// <summary>
        ///     View name for "DurableRoles".
        /// </summary>
        public readonly string Alias = "DurableRoles";

        /// <summary>
        ///     View caption for "DurableRoles".
        /// </summary>
        public readonly string Caption = "$Views_Names_DurableRoles";

        /// <summary>
        ///     View group for "DurableRoles".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RoleID.
        /// </summary>
        public readonly ViewObject ColumnRoleID = new ViewObject(0, "RoleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleName
        ///     Caption: $Views_Roles_Role.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(1, "RoleName", "$Views_Roles_Role");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeName
        ///     Caption: $Views_Roles_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(2, "TypeName", "$Views_Roles_Type");

        /// <summary>
        ///     ID:3 
        ///     Alias: Info
        ///     Caption: $Views_Roles_Info.
        /// </summary>
        public readonly ViewObject ColumnInfo = new ViewObject(3, "Info", "$Views_Roles_Info");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Roles_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Roles_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeID
        ///     Caption: $Views_Roles_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeID = new ViewObject(1, "TypeID", "$Views_Roles_Type_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: ShowHidden
        ///     Caption: $Views_Roles_ShowHidden_Param.
        /// </summary>
        public readonly ViewObject ParamShowHidden = new ViewObject(2, "ShowHidden", "$Views_Roles_ShowHidden_Param");

        #endregion

        #region ToString

        public static implicit operator string(DurableRolesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region EdsManagers

    /// <summary>
    ///     ID: {18d94578-ee8e-49f7-9fbb-50b3ad3bf76b}
    ///     Alias: EdsManagers
    ///     Caption: $Views_Names_EdsManagers
    ///     Group: System
    /// </summary>
    public class EdsManagersViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "EdsManagers": {18d94578-ee8e-49f7-9fbb-50b3ad3bf76b}.
        /// </summary>
        public readonly Guid ID = new Guid(0x18d94578,0xee8e,0x49f7,0x9f,0xbb,0x50,0xb3,0xad,0x3b,0xf7,0x6b);

        /// <summary>
        ///     View name for "EdsManagers".
        /// </summary>
        public readonly string Alias = "EdsManagers";

        /// <summary>
        ///     View caption for "EdsManagers".
        /// </summary>
        public readonly string Caption = "$Views_Names_EdsManagers";

        /// <summary>
        ///     View group for "EdsManagers".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: EdsName.
        /// </summary>
        public readonly ViewObject ColumnEdsName = new ViewObject(0, "EdsName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "Name");

        #endregion

        #region ToString

        public static implicit operator string(EdsManagersViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region EmittedTasks

    /// <summary>
    ///     ID: {b6e14161-038f-4060-bd35-66ba13da2cb8}
    ///     Alias: EmittedTasks
    ///     Caption: $Views_Names_EmittedTasks
    ///     Group: System
    /// </summary>
    public class EmittedTasksViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "EmittedTasks": {b6e14161-038f-4060-bd35-66ba13da2cb8}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb6e14161,0x038f,0x4060,0xbd,0x35,0x66,0xba,0x13,0xda,0x2c,0xb8);

        /// <summary>
        ///     View name for "EmittedTasks".
        /// </summary>
        public readonly string Alias = "EmittedTasks";

        /// <summary>
        ///     View caption for "EmittedTasks".
        /// </summary>
        public readonly string Caption = "$Views_Names_EmittedTasks";

        /// <summary>
        ///     View group for "EmittedTasks".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(0, "CardID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(1, "TypeID");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeCaption
        ///     Caption: $Views_EmittedTasks_TaskType.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(2, "TypeCaption", "$Views_EmittedTasks_TaskType");

        /// <summary>
        ///     ID:3 
        ///     Alias: StateID.
        /// </summary>
        public readonly ViewObject ColumnStateID = new ViewObject(3, "StateID");

        /// <summary>
        ///     ID:4 
        ///     Alias: StateName
        ///     Caption: $Views_EmittedTasks_State.
        /// </summary>
        public readonly ViewObject ColumnStateName = new ViewObject(4, "StateName", "$Views_EmittedTasks_State");

        /// <summary>
        ///     ID:5 
        ///     Alias: RoleID.
        /// </summary>
        public readonly ViewObject ColumnRoleID = new ViewObject(5, "RoleID");

        /// <summary>
        ///     ID:6 
        ///     Alias: RoleName
        ///     Caption: $Views_EmittedTasks_Performer.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(6, "RoleName", "$Views_EmittedTasks_Performer");

        /// <summary>
        ///     ID:7 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(7, "UserID");

        /// <summary>
        ///     ID:8 
        ///     Alias: UserName
        ///     Caption: $Views_EmittedTasks_User.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(8, "UserName", "$Views_EmittedTasks_User");

        /// <summary>
        ///     ID:9 
        ///     Alias: CardName
        ///     Caption: $Views_EmittedTasks_Card.
        /// </summary>
        public readonly ViewObject ColumnCardName = new ViewObject(9, "CardName", "$Views_EmittedTasks_Card");

        /// <summary>
        ///     ID:10 
        ///     Alias: CardTypeID.
        /// </summary>
        public readonly ViewObject ColumnCardTypeID = new ViewObject(10, "CardTypeID");

        /// <summary>
        ///     ID:11 
        ///     Alias: CardTypeName
        ///     Caption: $Views_EmittedTasks_CardType.
        /// </summary>
        public readonly ViewObject ColumnCardTypeName = new ViewObject(11, "CardTypeName", "$Views_EmittedTasks_CardType");

        /// <summary>
        ///     ID:12 
        ///     Alias: PlannedDate
        ///     Caption: $Views_EmittedTasks_Planned.
        /// </summary>
        public readonly ViewObject ColumnPlannedDate = new ViewObject(12, "PlannedDate", "$Views_EmittedTasks_Planned");

        /// <summary>
        ///     ID:13 
        ///     Alias: ModificationTime
        ///     Caption: $Views_EmittedTasks_Modification.
        /// </summary>
        public readonly ViewObject ColumnModificationTime = new ViewObject(13, "ModificationTime", "$Views_EmittedTasks_Modification");

        /// <summary>
        ///     ID:14 
        ///     Alias: AuthorID.
        /// </summary>
        public readonly ViewObject ColumnAuthorID = new ViewObject(14, "AuthorID");

        /// <summary>
        ///     ID:15 
        ///     Alias: AuthorName
        ///     Caption: $Views_EmittedTasks_Author.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(15, "AuthorName", "$Views_EmittedTasks_Author");

        /// <summary>
        ///     ID:16 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(16, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Status
        ///     Caption: $Views_EmittedTasks_State_Param.
        /// </summary>
        public readonly ViewObject ParamStatus = new ViewObject(0, "Status", "$Views_EmittedTasks_State_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: TaskType
        ///     Caption: $Views_EmittedTasks_TaskType_Param.
        /// </summary>
        public readonly ViewObject ParamTaskType = new ViewObject(1, "TaskType", "$Views_EmittedTasks_TaskType_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: TaskTypeGrouped
        ///     Caption: $Views_EmittedTasks_TaskTypeGrouped_Param.
        /// </summary>
        public readonly ViewObject ParamTaskTypeGrouped = new ViewObject(2, "TaskTypeGrouped", "$Views_EmittedTasks_TaskTypeGrouped_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: CardType
        ///     Caption: $Views_EmittedTasks_CardType_Param.
        /// </summary>
        public readonly ViewObject ParamCardType = new ViewObject(3, "CardType", "$Views_EmittedTasks_CardType_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: TaskDateDueInterval
        ///     Caption: $Views_EmittedTasks_TaskDateDue_Param.
        /// </summary>
        public readonly ViewObject ParamTaskDateDueInterval = new ViewObject(4, "TaskDateDueInterval", "$Views_EmittedTasks_TaskDateDue_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Role
        ///     Caption: $Views_EmittedTasks_Performer_Param.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(5, "Role", "$Views_EmittedTasks_Performer_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: FunctionRoleAuthorParam
        ///     Caption: $Views_MyTasks_FunctionRole_Author_Param.
        /// </summary>
        public readonly ViewObject ParamFunctionRoleAuthorParam = new ViewObject(6, "FunctionRoleAuthorParam", "$Views_MyTasks_FunctionRole_Author_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: FunctionRolePerformerParam
        ///     Caption: $Views_MyTasks_FunctionRole_Performer_Param.
        /// </summary>
        public readonly ViewObject ParamFunctionRolePerformerParam = new ViewObject(7, "FunctionRolePerformerParam", "$Views_MyTasks_FunctionRole_Performer_Param");

        #endregion

        #region ToString

        public static implicit operator string(EmittedTasksViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Errors

    /// <summary>
    ///     ID: {e1307d4f-a74d-460b-bdd9-e5d8644f98da}
    ///     Alias: Errors
    ///     Caption: $Views_Names_Errors
    ///     Group: System
    /// </summary>
    public class ErrorsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Errors": {e1307d4f-a74d-460b-bdd9-e5d8644f98da}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe1307d4f,0xa74d,0x460b,0xbd,0xd9,0xe5,0xd8,0x64,0x4f,0x98,0xda);

        /// <summary>
        ///     View name for "Errors".
        /// </summary>
        public readonly string Alias = "Errors";

        /// <summary>
        ///     View caption for "Errors".
        /// </summary>
        public readonly string Caption = "$Views_Names_Errors";

        /// <summary>
        ///     View group for "Errors".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RecordID.
        /// </summary>
        public readonly ViewObject ColumnRecordID = new ViewObject(0, "RecordID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RecordCaption.
        /// </summary>
        public readonly ViewObject ColumnRecordCaption = new ViewObject(1, "RecordCaption");

        /// <summary>
        ///     ID:2 
        ///     Alias: Category
        ///     Caption: $Views_Errors_Category.
        /// </summary>
        public readonly ViewObject ColumnCategory = new ViewObject(2, "Category", "$Views_Errors_Category");

        /// <summary>
        ///     ID:3 
        ///     Alias: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(3, "CardID");

        /// <summary>
        ///     ID:4 
        ///     Alias: CardCaption
        ///     Caption: $Views_ActionHistory_Caption.
        /// </summary>
        public readonly ViewObject ColumnCardCaption = new ViewObject(4, "CardCaption", "$Views_ActionHistory_Caption");

        /// <summary>
        ///     ID:5 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(5, "TypeID");

        /// <summary>
        ///     ID:6 
        ///     Alias: TypeCaption
        ///     Caption: $Views_ActionHistory_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(6, "TypeCaption", "$Views_ActionHistory_Type");

        /// <summary>
        ///     ID:7 
        ///     Alias: ActionID.
        /// </summary>
        public readonly ViewObject ColumnActionID = new ViewObject(7, "ActionID");

        /// <summary>
        ///     ID:8 
        ///     Alias: ActionName
        ///     Caption: $Views_ActionHistory_Action.
        /// </summary>
        public readonly ViewObject ColumnActionName = new ViewObject(8, "ActionName", "$Views_ActionHistory_Action");

        /// <summary>
        ///     ID:9 
        ///     Alias: Modified
        ///     Caption: $Views_ActionHistory_DateTime.
        /// </summary>
        public readonly ViewObject ColumnModified = new ViewObject(9, "Modified", "$Views_ActionHistory_DateTime");

        /// <summary>
        ///     ID:10 
        ///     Alias: ModifiedByID.
        /// </summary>
        public readonly ViewObject ColumnModifiedByID = new ViewObject(10, "ModifiedByID");

        /// <summary>
        ///     ID:11 
        ///     Alias: ModifiedByName
        ///     Caption: $Views_ActionHistory_User.
        /// </summary>
        public readonly ViewObject ColumnModifiedByName = new ViewObject(11, "ModifiedByName", "$Views_ActionHistory_User");

        /// <summary>
        ///     ID:12 
        ///     Alias: Text
        ///     Caption: $Views_Errors_Text.
        /// </summary>
        public readonly ViewObject ColumnText = new ViewObject(12, "Text", "$Views_Errors_Text");

        /// <summary>
        ///     ID:13 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(13, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Category
        ///     Caption: $Views_Errors_Category_Param.
        /// </summary>
        public readonly ViewObject ParamCategory = new ViewObject(0, "Category", "$Views_Errors_Category_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: CardID
        ///     Caption: $Views_ActionHistory_CardId_Param.
        /// </summary>
        public readonly ViewObject ParamCardID = new ViewObject(1, "CardID", "$Views_ActionHistory_CardId_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: CardCaption
        ///     Caption: $Views_ActionHistory_Card_Param.
        /// </summary>
        public readonly ViewObject ParamCardCaption = new ViewObject(2, "CardCaption", "$Views_ActionHistory_Card_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeID
        ///     Caption: $Views_ActionHistory_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeID = new ViewObject(3, "TypeID", "$Views_ActionHistory_Type_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: TypeCaption
        ///     Caption: $Views_Errors_TypeCaption_Param.
        /// </summary>
        public readonly ViewObject ParamTypeCaption = new ViewObject(4, "TypeCaption", "$Views_Errors_TypeCaption_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: ActionID
        ///     Caption: $Views_ActionHistory_Action_Param.
        /// </summary>
        public readonly ViewObject ParamActionID = new ViewObject(5, "ActionID", "$Views_ActionHistory_Action_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Modified
        ///     Caption: $Views_ActionHistory_DateTime_Param.
        /// </summary>
        public readonly ViewObject ParamModified = new ViewObject(6, "Modified", "$Views_ActionHistory_DateTime_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: ModifiedByID
        ///     Caption: $Views_ActionHistory_User_Param.
        /// </summary>
        public readonly ViewObject ParamModifiedByID = new ViewObject(7, "ModifiedByID", "$Views_ActionHistory_User_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: ModifiedByName
        ///     Caption: $Views_ActionHistory_UserName_Param.
        /// </summary>
        public readonly ViewObject ParamModifiedByName = new ViewObject(8, "ModifiedByName", "$Views_ActionHistory_UserName_Param");

        /// <summary>
        ///     ID:9 
        ///     Alias: Text
        ///     Caption: $Views_Errors_Text_Param.
        /// </summary>
        public readonly ViewObject ParamText = new ViewObject(9, "Text", "$Views_Errors_Text_Param");

        /// <summary>
        ///     ID:10 
        ///     Alias: DepartmentID
        ///     Caption: $Views_ActionHistory_UserDepartment_Param.
        /// </summary>
        public readonly ViewObject ParamDepartmentID = new ViewObject(10, "DepartmentID", "$Views_ActionHistory_UserDepartment_Param");

        #endregion

        #region ToString

        public static implicit operator string(ErrorsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ErrorWorkflows

    /// <summary>
    ///     ID: {91bae5ac-e846-4b71-a87b-3cee38381c66}
    ///     Alias: ErrorWorkflows
    ///     Caption: $Views_Names_ErrorWorkflows
    ///     Group: WorkflowEngine
    /// </summary>
    public class ErrorWorkflowsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ErrorWorkflows": {91bae5ac-e846-4b71-a87b-3cee38381c66}.
        /// </summary>
        public readonly Guid ID = new Guid(0x91bae5ac,0xe846,0x4b71,0xa8,0x7b,0x3c,0xee,0x38,0x38,0x1c,0x66);

        /// <summary>
        ///     View name for "ErrorWorkflows".
        /// </summary>
        public readonly string Alias = "ErrorWorkflows";

        /// <summary>
        ///     View caption for "ErrorWorkflows".
        /// </summary>
        public readonly string Caption = "$Views_Names_ErrorWorkflows";

        /// <summary>
        ///     View group for "ErrorWorkflows".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ProcessID.
        /// </summary>
        public readonly ViewObject ColumnProcessID = new ViewObject(0, "ProcessID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ProcessRowID.
        /// </summary>
        public readonly ViewObject ColumnProcessRowID = new ViewObject(1, "ProcessRowID");

        /// <summary>
        ///     ID:2 
        ///     Alias: ProcessName
        ///     Caption: $Views_ErrorWorkflows_ProcessName.
        /// </summary>
        public readonly ViewObject ColumnProcessName = new ViewObject(2, "ProcessName", "$Views_ErrorWorkflows_ProcessName");

        /// <summary>
        ///     ID:3 
        ///     Alias: ProcessCreated
        ///     Caption: $Views_ErrorWorkflows_Created.
        /// </summary>
        public readonly ViewObject ColumnProcessCreated = new ViewObject(3, "ProcessCreated", "$Views_ErrorWorkflows_Created");

        /// <summary>
        ///     ID:4 
        ///     Alias: ProcessErrorAdded
        ///     Caption: $Views_ErrorWorkflows_LastErrorAdded.
        /// </summary>
        public readonly ViewObject ColumnProcessErrorAdded = new ViewObject(4, "ProcessErrorAdded", "$Views_ErrorWorkflows_LastErrorAdded");

        /// <summary>
        ///     ID:5 
        ///     Alias: ProcessCardID.
        /// </summary>
        public readonly ViewObject ColumnProcessCardID = new ViewObject(5, "ProcessCardID");

        /// <summary>
        ///     ID:6 
        ///     Alias: ProcessCardDigest
        ///     Caption: $Views_ErrorWorkflows_CardDigest.
        /// </summary>
        public readonly ViewObject ColumnProcessCardDigest = new ViewObject(6, "ProcessCardDigest", "$Views_ErrorWorkflows_CardDigest");

        /// <summary>
        ///     ID:7 
        ///     Alias: ProcessCardType
        ///     Caption: $Views_ErrorWorkflows_CardType.
        /// </summary>
        public readonly ViewObject ColumnProcessCardType = new ViewObject(7, "ProcessCardType", "$Views_ErrorWorkflows_CardType");

        /// <summary>
        ///     ID:8 
        ///     Alias: ProcessResumable
        ///     Caption: $Views_ErrorWorkflows_Resumable.
        /// </summary>
        public readonly ViewObject ColumnProcessResumable = new ViewObject(8, "ProcessResumable", "$Views_ErrorWorkflows_Resumable");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: ProcessTemplate
        ///     Caption: $Views_ErrorWorkflows_ProcessTemplate_Param.
        /// </summary>
        public readonly ViewObject ParamProcessTemplate = new ViewObject(0, "ProcessTemplate", "$Views_ErrorWorkflows_ProcessTemplate_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: CardType
        ///     Caption: $Views_ErrorWorkflows_CardType_Param.
        /// </summary>
        public readonly ViewObject ParamCardType = new ViewObject(1, "CardType", "$Views_ErrorWorkflows_CardType_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: CardDigest
        ///     Caption: $Views_ErrorWorkflows_CardDigest.
        /// </summary>
        public readonly ViewObject ParamCardDigest = new ViewObject(2, "CardDigest", "$Views_ErrorWorkflows_CardDigest");

        /// <summary>
        ///     ID:3 
        ///     Alias: CardID
        ///     Caption: CardID.
        /// </summary>
        public readonly ViewObject ParamCardID = new ViewObject(3, "CardID", "CardID");

        /// <summary>
        ///     ID:4 
        ///     Alias: ErrorText
        ///     Caption: $Views_ErrorWorkflows_ErrorText_Param.
        /// </summary>
        public readonly ViewObject ParamErrorText = new ViewObject(4, "ErrorText", "$Views_ErrorWorkflows_ErrorText_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: ResumableOnly
        ///     Caption: $Views_ErrorWorkflows_ResumableOnly_Param.
        /// </summary>
        public readonly ViewObject ParamResumableOnly = new ViewObject(5, "ResumableOnly", "$Views_ErrorWorkflows_ResumableOnly_Param");

        #endregion

        #region ToString

        public static implicit operator string(ErrorWorkflowsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FileCategoriesAll

    /// <summary>
    ///     ID: {f44a1e46-8b4b-43c7-bb9b-2f88507400db}
    ///     Alias: FileCategoriesAll
    ///     Caption: $Views_Names_FileCategoriesAll
    ///     Group: System
    /// </summary>
    public class FileCategoriesAllViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "FileCategoriesAll": {f44a1e46-8b4b-43c7-bb9b-2f88507400db}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf44a1e46,0x8b4b,0x43c7,0xbb,0x9b,0x2f,0x88,0x50,0x74,0x00,0xdb);

        /// <summary>
        ///     View name for "FileCategoriesAll".
        /// </summary>
        public readonly string Alias = "FileCategoriesAll";

        /// <summary>
        ///     View caption for "FileCategoriesAll".
        /// </summary>
        public readonly string Caption = "$Views_Names_FileCategoriesAll";

        /// <summary>
        ///     View group for "FileCategoriesAll".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CategoryID.
        /// </summary>
        public readonly ViewObject ColumnCategoryID = new ViewObject(0, "CategoryID");

        /// <summary>
        ///     ID:1 
        ///     Alias: CategoryName
        ///     Caption: $Views_FileCategoriesAll_Name.
        /// </summary>
        public readonly ViewObject ColumnCategoryName = new ViewObject(1, "CategoryName", "$Views_FileCategoriesAll_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(2, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_FileCategoriesAll_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_FileCategoriesAll_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: IncludeWithoutCategory
        ///     Caption: $Views_FileCategoriesAll_IncludeWithoutCategory_Param.
        /// </summary>
        public readonly ViewObject ParamIncludeWithoutCategory = new ViewObject(1, "IncludeWithoutCategory", "$Views_FileCategoriesAll_IncludeWithoutCategory_Param");

        #endregion

        #region ToString

        public static implicit operator string(FileCategoriesAllViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FileCategoriesFiltered

    /// <summary>
    ///     ID: {c54a9c60-2010-4806-9c52-a117baef7643}
    ///     Alias: FileCategoriesFiltered
    ///     Caption: $Views_Names_FileCategoriesFiltered
    ///     Group: System
    /// </summary>
    public class FileCategoriesFilteredViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "FileCategoriesFiltered": {c54a9c60-2010-4806-9c52-a117baef7643}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc54a9c60,0x2010,0x4806,0x9c,0x52,0xa1,0x17,0xba,0xef,0x76,0x43);

        /// <summary>
        ///     View name for "FileCategoriesFiltered".
        /// </summary>
        public readonly string Alias = "FileCategoriesFiltered";

        /// <summary>
        ///     View caption for "FileCategoriesFiltered".
        /// </summary>
        public readonly string Caption = "$Views_Names_FileCategoriesFiltered";

        /// <summary>
        ///     View group for "FileCategoriesFiltered".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CategoryID.
        /// </summary>
        public readonly ViewObject ColumnCategoryID = new ViewObject(0, "CategoryID");

        /// <summary>
        ///     ID:1 
        ///     Alias: CategoryName
        ///     Caption: $Views_FileCategoriesFiltered_Name.
        /// </summary>
        public readonly ViewObject ColumnCategoryName = new ViewObject(1, "CategoryName", "$Views_FileCategoriesFiltered_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(2, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_FileCategoriesFiltered_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_FileCategoriesFiltered_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: IncludeWithoutCategory
        ///     Caption: $Views_FileCategoriesFiltered_IncludeWithoutCategory_Param.
        /// </summary>
        public readonly ViewObject ParamIncludeWithoutCategory = new ViewObject(1, "IncludeWithoutCategory", "$Views_FileCategoriesFiltered_IncludeWithoutCategory_Param");

        #endregion

        #region ToString

        public static implicit operator string(FileCategoriesFilteredViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FileConverterTypes

    /// <summary>
    ///     ID: {cab50857-4492-4521-8779-9ef0f5055b44}
    ///     Alias: FileConverterTypes
    ///     Caption: $Views_Names_FileConverterTypes
    ///     Group: System
    /// </summary>
    public class FileConverterTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "FileConverterTypes": {cab50857-4492-4521-8779-9ef0f5055b44}.
        /// </summary>
        public readonly Guid ID = new Guid(0xcab50857,0x4492,0x4521,0x87,0x79,0x9e,0xf0,0xf5,0x05,0x5b,0x44);

        /// <summary>
        ///     View name for "FileConverterTypes".
        /// </summary>
        public readonly string Alias = "FileConverterTypes";

        /// <summary>
        ///     View caption for "FileConverterTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_FileConverterTypes";

        /// <summary>
        ///     View group for "FileConverterTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: $Views_FileConverterTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "$Views_FileConverterTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_FileConverterTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_FileConverterTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(FileConverterTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FileTemplates

    /// <summary>
    ///     ID: {86b47fab-4522-4d84-9cf1-d0db3fd06c75}
    ///     Alias: FileTemplates
    ///     Caption: $Views_Names_FileTemplates
    ///     Group: System
    /// </summary>
    public class FileTemplatesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "FileTemplates": {86b47fab-4522-4d84-9cf1-d0db3fd06c75}.
        /// </summary>
        public readonly Guid ID = new Guid(0x86b47fab,0x4522,0x4d84,0x9c,0xf1,0xd0,0xdb,0x3f,0xd0,0x6c,0x75);

        /// <summary>
        ///     View name for "FileTemplates".
        /// </summary>
        public readonly string Alias = "FileTemplates";

        /// <summary>
        ///     View caption for "FileTemplates".
        /// </summary>
        public readonly string Caption = "$Views_Names_FileTemplates";

        /// <summary>
        ///     View group for "FileTemplates".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: FileTemplateID.
        /// </summary>
        public readonly ViewObject ColumnFileTemplateID = new ViewObject(0, "FileTemplateID");

        /// <summary>
        ///     ID:1 
        ///     Alias: FileTemplateName
        ///     Caption: $Views_FileTemplate_Name.
        /// </summary>
        public readonly ViewObject ColumnFileTemplateName = new ViewObject(1, "FileTemplateName", "$Views_FileTemplate_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: FileTemplateGroupName
        ///     Caption: $Views_FileTemplate_Group.
        /// </summary>
        public readonly ViewObject ColumnFileTemplateGroupName = new ViewObject(2, "FileTemplateGroupName", "$Views_FileTemplate_Group");

        /// <summary>
        ///     ID:3 
        ///     Alias: FileTemplateType
        ///     Caption: $Views_FileTemplate_Type.
        /// </summary>
        public readonly ViewObject ColumnFileTemplateType = new ViewObject(3, "FileTemplateType", "$Views_FileTemplate_Type");

        /// <summary>
        ///     ID:4 
        ///     Alias: FileTemplateSystem
        ///     Caption: $Views_FileTemplate_System.
        /// </summary>
        public readonly ViewObject ColumnFileTemplateSystem = new ViewObject(4, "FileTemplateSystem", "$Views_FileTemplate_System");

        /// <summary>
        ///     ID:5 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(5, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_FileTemplate_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_FileTemplate_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: GroupName
        ///     Caption: $Views_FileTemplate_Group_Param.
        /// </summary>
        public readonly ViewObject ParamGroupName = new ViewObject(1, "GroupName", "$Views_FileTemplate_Group_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Type
        ///     Caption: $Views_FileTemplate_Type_Param.
        /// </summary>
        public readonly ViewObject ParamType = new ViewObject(2, "Type", "$Views_FileTemplate_Type_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: System
        ///     Caption: $Views_FileTemplate_System_Param.
        /// </summary>
        public readonly ViewObject ParamSystem = new ViewObject(3, "System", "$Views_FileTemplate_System_Param");

        #endregion

        #region ToString

        public static implicit operator string(FileTemplatesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FileTemplateTemplateTypes

    /// <summary>
    ///     ID: {91a427d5-4dd9-4a7f-b35f-cb48de3254d0}
    ///     Alias: FileTemplateTemplateTypes
    ///     Caption: $Views_Names_FileTemplateTemplateTypes
    ///     Group: System
    /// </summary>
    public class FileTemplateTemplateTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "FileTemplateTemplateTypes": {91a427d5-4dd9-4a7f-b35f-cb48de3254d0}.
        /// </summary>
        public readonly Guid ID = new Guid(0x91a427d5,0x4dd9,0x4a7f,0xb3,0x5f,0xcb,0x48,0xde,0x32,0x54,0xd0);

        /// <summary>
        ///     View name for "FileTemplateTemplateTypes".
        /// </summary>
        public readonly string Alias = "FileTemplateTemplateTypes";

        /// <summary>
        ///     View caption for "FileTemplateTemplateTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_FileTemplateTemplateTypes";

        /// <summary>
        ///     View group for "FileTemplateTemplateTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeName
        ///     Caption: $Views_KrTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(1, "TypeName", "$Views_KrTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_KrTypes_Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_KrTypes_Name");

        #endregion

        #region ToString

        public static implicit operator string(FileTemplateTemplateTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FileTemplateTypes

    /// <summary>
    ///     ID: {eb59292c-378c-412f-b780-88469049a349}
    ///     Alias: FileTemplateTypes
    ///     Caption: $Views_Names_FileTemplateTypes
    ///     Group: System
    /// </summary>
    public class FileTemplateTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "FileTemplateTypes": {eb59292c-378c-412f-b780-88469049a349}.
        /// </summary>
        public readonly Guid ID = new Guid(0xeb59292c,0x378c,0x412f,0xb7,0x80,0x88,0x46,0x90,0x49,0xa3,0x49);

        /// <summary>
        ///     View name for "FileTemplateTypes".
        /// </summary>
        public readonly string Alias = "FileTemplateTypes";

        /// <summary>
        ///     View caption for "FileTemplateTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_FileTemplateTypes";

        /// <summary>
        ///     View group for "FileTemplateTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeCaption
        ///     Caption: $Views_FileTemplateTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(1, "TypeCaption", "$Views_FileTemplateTypes_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeName
        ///     Caption: $Views_FileTemplateTypes_Alias.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(2, "TypeName", "$Views_FileTemplateTypes_Alias");

        /// <summary>
        ///     ID:3 
        ///     Alias: IsDocTypeCaption
        ///     Caption: $Views_FileTemplateTypes_Type.
        /// </summary>
        public readonly ViewObject ColumnIsDocTypeCaption = new ViewObject(3, "IsDocTypeCaption", "$Views_FileTemplateTypes_Type");

        /// <summary>
        ///     ID:4 
        ///     Alias: LocalizedCaption.
        /// </summary>
        public readonly ViewObject ColumnLocalizedCaption = new ViewObject(4, "LocalizedCaption");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_FileTemplateTypes_Alias_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_FileTemplateTypes_Alias_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Caption
        ///     Caption: $Views_FileTemplateTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(1, "Caption", "$Views_FileTemplateTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(FileTemplateTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FormatSettings

    /// <summary>
    ///     ID: {038628a6-a2c0-4276-a986-2ab73428ca42}
    ///     Alias: FormatSettings
    ///     Caption: $Views_Names_FormatSettings
    ///     Group: System
    /// </summary>
    public class FormatSettingsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "FormatSettings": {038628a6-a2c0-4276-a986-2ab73428ca42}.
        /// </summary>
        public readonly Guid ID = new Guid(0x038628a6,0xa2c0,0x4276,0xa9,0x86,0x2a,0xb7,0x34,0x28,0xca,0x42);

        /// <summary>
        ///     View name for "FormatSettings".
        /// </summary>
        public readonly string Alias = "FormatSettings";

        /// <summary>
        ///     View caption for "FormatSettings".
        /// </summary>
        public readonly string Caption = "$Views_Names_FormatSettings";

        /// <summary>
        ///     View group for "FormatSettings".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SettingsID.
        /// </summary>
        public readonly ViewObject ColumnSettingsID = new ViewObject(0, "SettingsID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SettingsCaption
        ///     Caption: $Views_FormatSettings_Caption.
        /// </summary>
        public readonly ViewObject ColumnSettingsCaption = new ViewObject(1, "SettingsCaption", "$Views_FormatSettings_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: SettingsName
        ///     Caption: $Views_FormatSettings_Name.
        /// </summary>
        public readonly ViewObject ColumnSettingsName = new ViewObject(2, "SettingsName", "$Views_FormatSettings_Name");

        /// <summary>
        ///     ID:3 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(3, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_FormatSettings_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_FormatSettings_Caption_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_FormatSettings_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_FormatSettings_Name_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: ID
        ///     Caption: ID.
        /// </summary>
        public readonly ViewObject ParamID = new ViewObject(2, "ID", "ID");

        #endregion

        #region ToString

        public static implicit operator string(FormatSettingsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region FunctionRoleCards

    /// <summary>
    ///     ID: {6693a0e4-421c-484f-a4ce-21be436a4be2}
    ///     Alias: FunctionRoleCards
    ///     Caption: $Views_Names_FunctionRoleCards
    ///     Group: System
    /// </summary>
    public class FunctionRoleCardsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "FunctionRoleCards": {6693a0e4-421c-484f-a4ce-21be436a4be2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x6693a0e4,0x421c,0x484f,0xa4,0xce,0x21,0xbe,0x43,0x6a,0x4b,0xe2);

        /// <summary>
        ///     View name for "FunctionRoleCards".
        /// </summary>
        public readonly string Alias = "FunctionRoleCards";

        /// <summary>
        ///     View caption for "FunctionRoleCards".
        /// </summary>
        public readonly string Caption = "$Views_Names_FunctionRoleCards";

        /// <summary>
        ///     View group for "FunctionRoleCards".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: FunctionRoleID.
        /// </summary>
        public readonly ViewObject ColumnFunctionRoleID = new ViewObject(0, "FunctionRoleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: FunctionRoleCaption
        ///     Caption: $Views_FunctionRoles_Caption.
        /// </summary>
        public readonly ViewObject ColumnFunctionRoleCaption = new ViewObject(1, "FunctionRoleCaption", "$Views_FunctionRoles_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: FunctionRoleName
        ///     Caption: $Views_FunctionRoles_Alias.
        /// </summary>
        public readonly ViewObject ColumnFunctionRoleName = new ViewObject(2, "FunctionRoleName", "$Views_FunctionRoles_Alias");

        /// <summary>
        ///     ID:3 
        ///     Alias: FunctionRoleCanBeDeputy
        ///     Caption: $Views_FunctionRoles_CanBeDeputy.
        /// </summary>
        public readonly ViewObject ColumnFunctionRoleCanBeDeputy = new ViewObject(3, "FunctionRoleCanBeDeputy", "$Views_FunctionRoles_CanBeDeputy");

        /// <summary>
        ///     ID:4 
        ///     Alias: FunctionRoleCanTakeInProgress
        ///     Caption: $Views_FunctionRoles_CanTakeInProgress.
        /// </summary>
        public readonly ViewObject ColumnFunctionRoleCanTakeInProgress = new ViewObject(4, "FunctionRoleCanTakeInProgress", "$Views_FunctionRoles_CanTakeInProgress");

        /// <summary>
        ///     ID:5 
        ///     Alias: FunctionRoleHideTaskByDefault
        ///     Caption: $Views_FunctionRoles_HideTaskByDefault.
        /// </summary>
        public readonly ViewObject ColumnFunctionRoleHideTaskByDefault = new ViewObject(5, "FunctionRoleHideTaskByDefault", "$Views_FunctionRoles_HideTaskByDefault");

        /// <summary>
        ///     ID:6 
        ///     Alias: FunctionRoleCanChangeTaskInfo
        ///     Caption: $Views_FunctionRoles_CanChangeTaskInfo.
        /// </summary>
        public readonly ViewObject ColumnFunctionRoleCanChangeTaskInfo = new ViewObject(6, "FunctionRoleCanChangeTaskInfo", "$Views_FunctionRoles_CanChangeTaskInfo");

        /// <summary>
        ///     ID:7 
        ///     Alias: FunctionRoleCanChangeTaskRoles
        ///     Caption: $Views_FunctionRoles_CanChangeTaskRoles.
        /// </summary>
        public readonly ViewObject ColumnFunctionRoleCanChangeTaskRoles = new ViewObject(7, "FunctionRoleCanChangeTaskRoles", "$Views_FunctionRoles_CanChangeTaskRoles");

        /// <summary>
        ///     ID:8 
        ///     Alias: PartitionID.
        /// </summary>
        public readonly ViewObject ColumnPartitionID = new ViewObject(8, "PartitionID");

        /// <summary>
        ///     ID:9 
        ///     Alias: PartitionName
        ///     Caption: $Views_FunctionRoles_Partition.
        /// </summary>
        public readonly ViewObject ColumnPartitionName = new ViewObject(9, "PartitionName", "$Views_FunctionRoles_Partition");

        /// <summary>
        ///     ID:10 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(10, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: FunctionRoleID
        ///     Caption: FunctionRoleID.
        /// </summary>
        public readonly ViewObject ParamFunctionRoleID = new ViewObject(0, "FunctionRoleID", "FunctionRoleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: Caption
        ///     Caption: $Views_FunctionRoles_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(1, "Caption", "$Views_FunctionRoles_Caption_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Name
        ///     Caption: $Views_FunctionRoles_Alias_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(2, "Name", "$Views_FunctionRoles_Alias_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: CanBeDeputy
        ///     Caption: $Views_FunctionRoles_CanBeDeputy_Param.
        /// </summary>
        public readonly ViewObject ParamCanBeDeputy = new ViewObject(3, "CanBeDeputy", "$Views_FunctionRoles_CanBeDeputy_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: CanTakeInProgress
        ///     Caption: $Views_FunctionRoles_CanTakeInProgress_Param.
        /// </summary>
        public readonly ViewObject ParamCanTakeInProgress = new ViewObject(4, "CanTakeInProgress", "$Views_FunctionRoles_CanTakeInProgress_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: HideTaskByDefault
        ///     Caption: $Views_FunctionRoles_HideTaskByDefault_Param.
        /// </summary>
        public readonly ViewObject ParamHideTaskByDefault = new ViewObject(5, "HideTaskByDefault", "$Views_FunctionRoles_HideTaskByDefault_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: CanChangeTaskInfo
        ///     Caption: $Views_FunctionRoles_CanChangeTaskInfo_Param.
        /// </summary>
        public readonly ViewObject ParamCanChangeTaskInfo = new ViewObject(6, "CanChangeTaskInfo", "$Views_FunctionRoles_CanChangeTaskInfo_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: CanChangeTaskRoles
        ///     Caption: $Views_FunctionRoles_CanChangeTaskRoles_Param.
        /// </summary>
        public readonly ViewObject ParamCanChangeTaskRoles = new ViewObject(7, "CanChangeTaskRoles", "$Views_FunctionRoles_CanChangeTaskRoles_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: Partition
        ///     Caption: $Views_FunctionRoles_Partition.
        /// </summary>
        public readonly ViewObject ParamPartition = new ViewObject(8, "Partition", "$Views_FunctionRoles_Partition");

        #endregion

        #region ToString

        public static implicit operator string(FunctionRoleCardsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region GetCardIDView

    /// <summary>
    ///     ID: {07100666-36ac-49e3-ae68-e53caafb45a2}
    ///     Alias: GetCardIDView
    ///     Caption: $Views_Names_GetCardIDView
    ///     Group: Testing
    /// </summary>
    public class GetCardIDViewViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "GetCardIDView": {07100666-36ac-49e3-ae68-e53caafb45a2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x07100666,0x36ac,0x49e3,0xae,0x68,0xe5,0x3c,0xaa,0xfb,0x45,0xa2);

        /// <summary>
        ///     View name for "GetCardIDView".
        /// </summary>
        public readonly string Alias = "GetCardIDView";

        /// <summary>
        ///     View caption for "GetCardIDView".
        /// </summary>
        public readonly string Caption = "$Views_Names_GetCardIDView";

        /// <summary>
        ///     View group for "GetCardIDView".
        /// </summary>
        public readonly string Group = "Testing";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CardID
        ///     Caption: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(0, "CardID", "CardID");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: ID
        ///     Caption: ID.
        /// </summary>
        public readonly ViewObject ParamID = new ViewObject(0, "ID", "ID");

        #endregion

        #region ToString

        public static implicit operator string(GetCardIDViewViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region GetFileNameView

    /// <summary>
    ///     ID: {1eb7431c-32f1-4ed6-bf71-57a842d61949}
    ///     Alias: GetFileNameView
    ///     Caption: $Views_Names_GetFileNameView
    ///     Group: Testing
    /// </summary>
    public class GetFileNameViewViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "GetFileNameView": {1eb7431c-32f1-4ed6-bf71-57a842d61949}.
        /// </summary>
        public readonly Guid ID = new Guid(0x1eb7431c,0x32f1,0x4ed6,0xbf,0x71,0x57,0xa8,0x42,0xd6,0x19,0x49);

        /// <summary>
        ///     View name for "GetFileNameView".
        /// </summary>
        public readonly string Alias = "GetFileNameView";

        /// <summary>
        ///     View caption for "GetFileNameView".
        /// </summary>
        public readonly string Caption = "$Views_Names_GetFileNameView";

        /// <summary>
        ///     View group for "GetFileNameView".
        /// </summary>
        public readonly string Group = "Testing";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: PreviewPath
        ///     Caption: PreviewPath.
        /// </summary>
        public readonly ViewObject ColumnPreviewPath = new ViewObject(0, "PreviewPath", "PreviewPath");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: FileName
        ///     Caption: FileName.
        /// </summary>
        public readonly ViewObject ParamFileName = new ViewObject(0, "FileName", "FileName");

        #endregion

        #region ToString

        public static implicit operator string(GetFileNameViewViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Groups

    /// <summary>
    ///     ID: {4f179970-84b9-4b6a-b921-72cc79ca2cb3}
    ///     Alias: Groups
    ///     Caption: $Views_Names_Groups
    ///     Group: Testing
    /// </summary>
    public class GroupsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Groups": {4f179970-84b9-4b6a-b921-72cc79ca2cb3}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4f179970,0x84b9,0x4b6a,0xb9,0x21,0x72,0xcc,0x79,0xca,0x2c,0xb3);

        /// <summary>
        ///     View name for "Groups".
        /// </summary>
        public readonly string Alias = "Groups";

        /// <summary>
        ///     View caption for "Groups".
        /// </summary>
        public readonly string Caption = "$Views_Names_Groups";

        /// <summary>
        ///     View group for "Groups".
        /// </summary>
        public readonly string Group = "Testing";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: GroupID.
        /// </summary>
        public readonly ViewObject ColumnGroupID = new ViewObject(0, "GroupID");

        /// <summary>
        ///     ID:1 
        ///     Alias: GroupParentID.
        /// </summary>
        public readonly ViewObject ColumnGroupParentID = new ViewObject(1, "GroupParentID");

        /// <summary>
        ///     ID:2 
        ///     Alias: GroupName.
        /// </summary>
        public readonly ViewObject ColumnGroupName = new ViewObject(2, "GroupName");

        /// <summary>
        ///     ID:3 
        ///     Alias: Content
        ///     Caption: Содержимое.
        /// </summary>
        public readonly ViewObject ColumnContent = new ViewObject(3, "Content", "Содержимое");

        #endregion

        #region ToString

        public static implicit operator string(GroupsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region GroupsWithHierarchy

    /// <summary>
    ///     ID: {5de1d29e-41ce-4279-90d5-573d2a81f009}
    ///     Alias: GroupsWithHierarchy
    ///     Caption: $Views_Names_GroupsWithHierarchy
    ///     Group: Testing
    /// </summary>
    public class GroupsWithHierarchyViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "GroupsWithHierarchy": {5de1d29e-41ce-4279-90d5-573d2a81f009}.
        /// </summary>
        public readonly Guid ID = new Guid(0x5de1d29e,0x41ce,0x4279,0x90,0xd5,0x57,0x3d,0x2a,0x81,0xf0,0x09);

        /// <summary>
        ///     View name for "GroupsWithHierarchy".
        /// </summary>
        public readonly string Alias = "GroupsWithHierarchy";

        /// <summary>
        ///     View caption for "GroupsWithHierarchy".
        /// </summary>
        public readonly string Caption = "$Views_Names_GroupsWithHierarchy";

        /// <summary>
        ///     View group for "GroupsWithHierarchy".
        /// </summary>
        public readonly string Group = "Testing";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ID.
        /// </summary>
        public readonly ViewObject ColumnID = new ViewObject(0, "ID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ParentID.
        /// </summary>
        public readonly ViewObject ColumnParentID = new ViewObject(1, "ParentID");

        /// <summary>
        ///     ID:2 
        ///     Alias: GroupID.
        /// </summary>
        public readonly ViewObject ColumnGroupID = new ViewObject(2, "GroupID");

        /// <summary>
        ///     ID:3 
        ///     Alias: GroupParentID.
        /// </summary>
        public readonly ViewObject ColumnGroupParentID = new ViewObject(3, "GroupParentID");

        /// <summary>
        ///     ID:4 
        ///     Alias: GroupName.
        /// </summary>
        public readonly ViewObject ColumnGroupName = new ViewObject(4, "GroupName");

        /// <summary>
        ///     ID:5 
        ///     Alias: Content
        ///     Caption: Содержимое.
        /// </summary>
        public readonly ViewObject ColumnContent = new ViewObject(5, "Content", "Содержимое");

        #endregion

        #region ToString

        public static implicit operator string(GroupsWithHierarchyViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region HelpSections

    /// <summary>
    ///     ID: {c35e6ac1-9cec-482d-a20f-b3c330f2dc53}
    ///     Alias: HelpSections
    ///     Caption: $Views_Names_HelpSections
    ///     Group: System
    /// </summary>
    public class HelpSectionsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "HelpSections": {c35e6ac1-9cec-482d-a20f-b3c330f2dc53}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc35e6ac1,0x9cec,0x482d,0xa2,0x0f,0xb3,0xc3,0x30,0xf2,0xdc,0x53);

        /// <summary>
        ///     View name for "HelpSections".
        /// </summary>
        public readonly string Alias = "HelpSections";

        /// <summary>
        ///     View caption for "HelpSections".
        /// </summary>
        public readonly string Caption = "$Views_Names_HelpSections";

        /// <summary>
        ///     View group for "HelpSections".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: HelpSectionID.
        /// </summary>
        public readonly ViewObject ColumnHelpSectionID = new ViewObject(0, "HelpSectionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: HelpSectionCode
        ///     Caption: $Views_HelpSections_Code.
        /// </summary>
        public readonly ViewObject ColumnHelpSectionCode = new ViewObject(1, "HelpSectionCode", "$Views_HelpSections_Code");

        /// <summary>
        ///     ID:2 
        ///     Alias: HelpSectionName
        ///     Caption: $Views_HelpSections_Name.
        /// </summary>
        public readonly ViewObject ColumnHelpSectionName = new ViewObject(2, "HelpSectionName", "$Views_HelpSections_Name");

        /// <summary>
        ///     ID:3 
        ///     Alias: HelpSectionRichText.
        /// </summary>
        public readonly ViewObject ColumnHelpSectionRichText = new ViewObject(3, "HelpSectionRichText");

        /// <summary>
        ///     ID:4 
        ///     Alias: HelpSectionPlainText
        ///     Caption: $Views_HelpSections_PlainText.
        /// </summary>
        public readonly ViewObject ColumnHelpSectionPlainText = new ViewObject(4, "HelpSectionPlainText", "$Views_HelpSections_PlainText");

        /// <summary>
        ///     ID:5 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(5, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: PlainText
        ///     Caption: $Views_HelpSections_PlainText.
        /// </summary>
        public readonly ViewObject ParamPlainText = new ViewObject(0, "PlainText", "$Views_HelpSections_PlainText");

        #endregion

        #region ToString

        public static implicit operator string(HelpSectionsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Hierarchy

    /// <summary>
    ///     ID: {29929a97-79f8-4eda-a6ee-b9621aa9ae49}
    ///     Alias: Hierarchy
    ///     Caption: $Views_Names_Hierarchy
    ///     Group: Testing
    /// </summary>
    public class HierarchyViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Hierarchy": {29929a97-79f8-4eda-a6ee-b9621aa9ae49}.
        /// </summary>
        public readonly Guid ID = new Guid(0x29929a97,0x79f8,0x4eda,0xa6,0xee,0xb9,0x62,0x1a,0xa9,0xae,0x49);

        /// <summary>
        ///     View name for "Hierarchy".
        /// </summary>
        public readonly string Alias = "Hierarchy";

        /// <summary>
        ///     View caption for "Hierarchy".
        /// </summary>
        public readonly string Caption = "$Views_Names_Hierarchy";

        /// <summary>
        ///     View group for "Hierarchy".
        /// </summary>
        public readonly string Group = "Testing";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ID.
        /// </summary>
        public readonly ViewObject ColumnID = new ViewObject(0, "ID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ParentID.
        /// </summary>
        public readonly ViewObject ColumnParentID = new ViewObject(1, "ParentID");

        /// <summary>
        ///     ID:2 
        ///     Alias: Content
        ///     Caption: Содержимое.
        /// </summary>
        public readonly ViewObject ColumnContent = new ViewObject(2, "Content", "Содержимое");

        #endregion

        #region ToString

        public static implicit operator string(HierarchyViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region IncomingDocuments

    /// <summary>
    ///     ID: {a1889a97-2c55-489a-a942-fa36b61dff04}
    ///     Alias: IncomingDocuments
    ///     Caption: $Views_Names_IncomingDocuments
    ///     Group: KrDocuments
    /// </summary>
    public class IncomingDocumentsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "IncomingDocuments": {a1889a97-2c55-489a-a942-fa36b61dff04}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa1889a97,0x2c55,0x489a,0xa9,0x42,0xfa,0x36,0xb6,0x1d,0xff,0x04);

        /// <summary>
        ///     View name for "IncomingDocuments".
        /// </summary>
        public readonly string Alias = "IncomingDocuments";

        /// <summary>
        ///     View caption for "IncomingDocuments".
        /// </summary>
        public readonly string Caption = "$Views_Names_IncomingDocuments";

        /// <summary>
        ///     View group for "IncomingDocuments".
        /// </summary>
        public readonly string Group = "KrDocuments";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DocID.
        /// </summary>
        public readonly ViewObject ColumnDocID = new ViewObject(0, "DocID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DocNumber
        ///     Caption: $Views_Registers_Number.
        /// </summary>
        public readonly ViewObject ColumnDocNumber = new ViewObject(1, "DocNumber", "$Views_Registers_Number");

        /// <summary>
        ///     ID:2 
        ///     Alias: SubTypeTitle
        ///     Caption: $Views_Registers_DocType.
        /// </summary>
        public readonly ViewObject ColumnSubTypeTitle = new ViewObject(2, "SubTypeTitle", "$Views_Registers_DocType");

        /// <summary>
        ///     ID:3 
        ///     Alias: DocSubject
        ///     Caption: $Views_Registers_Subject.
        /// </summary>
        public readonly ViewObject ColumnDocSubject = new ViewObject(3, "DocSubject", "$Views_Registers_Subject");

        /// <summary>
        ///     ID:4 
        ///     Alias: DocDescription
        ///     Caption: $Views_Registers_DocDescription.
        /// </summary>
        public readonly ViewObject ColumnDocDescription = new ViewObject(4, "DocDescription", "$Views_Registers_DocDescription");

        /// <summary>
        ///     ID:5 
        ///     Alias: PartnerID.
        /// </summary>
        public readonly ViewObject ColumnPartnerID = new ViewObject(5, "PartnerID");

        /// <summary>
        ///     ID:6 
        ///     Alias: PartnerName
        ///     Caption: $Views_Registers_Partner.
        /// </summary>
        public readonly ViewObject ColumnPartnerName = new ViewObject(6, "PartnerName", "$Views_Registers_Partner");

        /// <summary>
        ///     ID:7 
        ///     Alias: OutgoingNumber
        ///     Caption: $Views_Registers_OutgoingNumber.
        /// </summary>
        public readonly ViewObject ColumnOutgoingNumber = new ViewObject(7, "OutgoingNumber", "$Views_Registers_OutgoingNumber");

        /// <summary>
        ///     ID:8 
        ///     Alias: RegistratorID.
        /// </summary>
        public readonly ViewObject ColumnRegistratorID = new ViewObject(8, "RegistratorID");

        /// <summary>
        ///     ID:9 
        ///     Alias: RegistratorName
        ///     Caption: $Views_Registers_Registrator.
        /// </summary>
        public readonly ViewObject ColumnRegistratorName = new ViewObject(9, "RegistratorName", "$Views_Registers_Registrator");

        /// <summary>
        ///     ID:10 
        ///     Alias: DocDate
        ///     Caption: $Views_Registers_DocDate.
        /// </summary>
        public readonly ViewObject ColumnDocDate = new ViewObject(10, "DocDate", "$Views_Registers_DocDate");

        /// <summary>
        ///     ID:11 
        ///     Alias: CreationDate
        ///     Caption: $Views_Registers_CreationDate.
        /// </summary>
        public readonly ViewObject ColumnCreationDate = new ViewObject(11, "CreationDate", "$Views_Registers_CreationDate");

        /// <summary>
        ///     ID:12 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department.
        /// </summary>
        public readonly ViewObject ColumnDepartment = new ViewObject(12, "Department", "$Views_Registers_Department");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: IsInitiator
        ///     Caption: $Views_Registers_IsInitiator_Param.
        /// </summary>
        public readonly ViewObject ParamIsInitiator = new ViewObject(0, "IsInitiator", "$Views_Registers_IsInitiator_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: IsRegistrator
        ///     Caption: $Views_Registers_IsRegistrator_Param.
        /// </summary>
        public readonly ViewObject ParamIsRegistrator = new ViewObject(1, "IsRegistrator", "$Views_Registers_IsRegistrator_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Partner
        ///     Caption: $Views_Registers_Partner_Param.
        /// </summary>
        public readonly ViewObject ParamPartner = new ViewObject(2, "Partner", "$Views_Registers_Partner_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: OutgoingNumber
        ///     Caption: $Views_Registers_OutgoingNumber_Param.
        /// </summary>
        public readonly ViewObject ParamOutgoingNumber = new ViewObject(3, "OutgoingNumber", "$Views_Registers_OutgoingNumber_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Number
        ///     Caption: $Views_Registers_Number_Param.
        /// </summary>
        public readonly ViewObject ParamNumber = new ViewObject(4, "Number", "$Views_Registers_Number_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Subject
        ///     Caption: $Views_Registers_Subject_Param.
        /// </summary>
        public readonly ViewObject ParamSubject = new ViewObject(5, "Subject", "$Views_Registers_Subject_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: DocDate
        ///     Caption: $Views_Registers_DocDate_Param.
        /// </summary>
        public readonly ViewObject ParamDocDate = new ViewObject(6, "DocDate", "$Views_Registers_DocDate_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: Registrator
        ///     Caption: $Views_Registers_Registrator_Param.
        /// </summary>
        public readonly ViewObject ParamRegistrator = new ViewObject(7, "Registrator", "$Views_Registers_Registrator_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: State
        ///     Caption: $Views_Registers_State_Param.
        /// </summary>
        public readonly ViewObject ParamState = new ViewObject(8, "State", "$Views_Registers_State_Param");

        /// <summary>
        ///     ID:9 
        ///     Alias: DocType
        ///     Caption: $Views_Registers_DocType_Param.
        /// </summary>
        public readonly ViewObject ParamDocType = new ViewObject(9, "DocType", "$Views_Registers_DocType_Param");

        /// <summary>
        ///     ID:10 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(10, "Department", "$Views_Registers_Department_Param");

        /// <summary>
        ///     ID:11 
        ///     Alias: CreationDate
        ///     Caption: $Views_Registers_CreationDate_Param.
        /// </summary>
        public readonly ViewObject ParamCreationDate = new ViewObject(11, "CreationDate", "$Views_Registers_CreationDate_Param");

        #endregion

        #region ToString

        public static implicit operator string(IncomingDocumentsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrActionTypes

    /// <summary>
    ///     ID: {73ad84ae-84b6-4292-b496-5bf63cf9033e}
    ///     Alias: KrActionTypes
    ///     Caption: $Views_Names_KrActionTypes
    ///     Group: Kr Wf
    /// </summary>
    public class KrActionTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrActionTypes": {73ad84ae-84b6-4292-b496-5bf63cf9033e}.
        /// </summary>
        public readonly Guid ID = new Guid(0x73ad84ae,0x84b6,0x4292,0xb4,0x96,0x5b,0xf6,0x3c,0xf9,0x03,0x3e);

        /// <summary>
        ///     View name for "KrActionTypes".
        /// </summary>
        public readonly string Alias = "KrActionTypes";

        /// <summary>
        ///     View caption for "KrActionTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrActionTypes";

        /// <summary>
        ///     View group for "KrActionTypes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: RefName.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "RefName");

        /// <summary>
        ///     ID:2 
        ///     Alias: RefEventType
        ///     Caption: RefEventType.
        /// </summary>
        public readonly ViewObject ColumnRefEventType = new ViewObject(2, "RefEventType", "RefEventType");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "Name");

        #endregion

        #region ToString

        public static implicit operator string(KrActionTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrCreateCardStageTypeModes

    /// <summary>
    ///     ID: {1e8ca3c1-72a3-4c2c-8740-01c4dd96de39}
    ///     Alias: KrCreateCardStageTypeModes
    ///     Caption: $Views_Names_KrCreateCardStageTypeModes
    ///     Group: Kr Wf
    /// </summary>
    public class KrCreateCardStageTypeModesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrCreateCardStageTypeModes": {1e8ca3c1-72a3-4c2c-8740-01c4dd96de39}.
        /// </summary>
        public readonly Guid ID = new Guid(0x1e8ca3c1,0x72a3,0x4c2c,0x87,0x40,0x01,0xc4,0xdd,0x96,0xde,0x39);

        /// <summary>
        ///     View name for "KrCreateCardStageTypeModes".
        /// </summary>
        public readonly string Alias = "KrCreateCardStageTypeModes";

        /// <summary>
        ///     View caption for "KrCreateCardStageTypeModes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrCreateCardStageTypeModes";

        /// <summary>
        ///     View group for "KrCreateCardStageTypeModes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: RefName.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "RefName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "Name");

        #endregion

        #region ToString

        public static implicit operator string(KrCreateCardStageTypeModesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrCycleGroupingModes

    /// <summary>
    ///     ID: {ac33bea6-af04-4e73-b9ca-5fedb8fcf64f}
    ///     Alias: KrCycleGroupingModes
    ///     Caption: $Views_Names_KrCycleGroupingModes
    ///     Group: Kr Wf
    /// </summary>
    public class KrCycleGroupingModesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrCycleGroupingModes": {ac33bea6-af04-4e73-b9ca-5fedb8fcf64f}.
        /// </summary>
        public readonly Guid ID = new Guid(0xac33bea6,0xaf04,0x4e73,0xb9,0xca,0x5f,0xed,0xb8,0xfc,0xf6,0x4f);

        /// <summary>
        ///     View name for "KrCycleGroupingModes".
        /// </summary>
        public readonly string Alias = "KrCycleGroupingModes";

        /// <summary>
        ///     View caption for "KrCycleGroupingModes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrCycleGroupingModes";

        /// <summary>
        ///     View group for "KrCycleGroupingModes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ModeID.
        /// </summary>
        public readonly ViewObject ColumnModeID = new ViewObject(0, "ModeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ModeName
        ///     Caption: $Views_KrCycleGroupingModes_Name.
        /// </summary>
        public readonly ViewObject ColumnModeName = new ViewObject(1, "ModeName", "$Views_KrCycleGroupingModes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_KrCycleGroupingModes_Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_KrCycleGroupingModes_Name");

        #endregion

        #region ToString

        public static implicit operator string(KrCycleGroupingModesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrDocNumberRegistrationAutoAssigment

    /// <summary>
    ///     ID: {65867469-2aec-4c13-a807-a2784a023d6b}
    ///     Alias: KrDocNumberRegistrationAutoAssigment
    ///     Caption: $Views_Names_KrDocNumberRegistrationAutoAssigment
    ///     Group: Kr Wf
    /// </summary>
    public class KrDocNumberRegistrationAutoAssigmentViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrDocNumberRegistrationAutoAssigment": {65867469-2aec-4c13-a807-a2784a023d6b}.
        /// </summary>
        public readonly Guid ID = new Guid(0x65867469,0x2aec,0x4c13,0xa8,0x07,0xa2,0x78,0x4a,0x02,0x3d,0x6b);

        /// <summary>
        ///     View name for "KrDocNumberRegistrationAutoAssigment".
        /// </summary>
        public readonly string Alias = "KrDocNumberRegistrationAutoAssigment";

        /// <summary>
        ///     View caption for "KrDocNumberRegistrationAutoAssigment".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrDocNumberRegistrationAutoAssigment";

        /// <summary>
        ///     View group for "KrDocNumberRegistrationAutoAssigment".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DocNumberRegistrationAutoAssignmentID.
        /// </summary>
        public readonly ViewObject ColumnDocNumberRegistrationAutoAssignmentID = new ViewObject(0, "DocNumberRegistrationAutoAssignmentID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DocNumberRegistrationAutoAssignmentDescription
        ///     Caption: $Views_KrDocNumberRegistrationAutoAssigment_Option.
        /// </summary>
        public readonly ViewObject ColumnDocNumberRegistrationAutoAssignmentDescription = new ViewObject(1, "DocNumberRegistrationAutoAssignmentDescription", "$Views_KrDocNumberRegistrationAutoAssigment_Option");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Description
        ///     Caption: $Views_KrDocNumberRegistrationAutoAssigment_Option_Param.
        /// </summary>
        public readonly ViewObject ParamDescription = new ViewObject(0, "Description", "$Views_KrDocNumberRegistrationAutoAssigment_Option_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrDocNumberRegistrationAutoAssigmentViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrDocNumberRegularAutoAssigment

    /// <summary>
    ///     ID: {021327d4-1e7a-4834-bbc8-6cafd415f098}
    ///     Alias: KrDocNumberRegularAutoAssigment
    ///     Caption: $Views_Names_KrDocNumberRegularAutoAssigment
    ///     Group: Kr Wf
    /// </summary>
    public class KrDocNumberRegularAutoAssigmentViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrDocNumberRegularAutoAssigment": {021327d4-1e7a-4834-bbc8-6cafd415f098}.
        /// </summary>
        public readonly Guid ID = new Guid(0x021327d4,0x1e7a,0x4834,0xbb,0xc8,0x6c,0xaf,0xd4,0x15,0xf0,0x98);

        /// <summary>
        ///     View name for "KrDocNumberRegularAutoAssigment".
        /// </summary>
        public readonly string Alias = "KrDocNumberRegularAutoAssigment";

        /// <summary>
        ///     View caption for "KrDocNumberRegularAutoAssigment".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrDocNumberRegularAutoAssigment";

        /// <summary>
        ///     View group for "KrDocNumberRegularAutoAssigment".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DocNumberRegularAutoAssignmentID.
        /// </summary>
        public readonly ViewObject ColumnDocNumberRegularAutoAssignmentID = new ViewObject(0, "DocNumberRegularAutoAssignmentID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DocNumberRegularAutoAssignmentDescription
        ///     Caption: $Views_KrDocNumberRegularAutoAssigment_Option_Param.
        /// </summary>
        public readonly ViewObject ColumnDocNumberRegularAutoAssignmentDescription = new ViewObject(1, "DocNumberRegularAutoAssignmentDescription", "$Views_KrDocNumberRegularAutoAssigment_Option_Param");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Description
        ///     Caption: $Views_KrDocNumberRegularAutoAssigment_Option.
        /// </summary>
        public readonly ViewObject ParamDescription = new ViewObject(0, "Description", "$Views_KrDocNumberRegularAutoAssigment_Option");

        #endregion

        #region ToString

        public static implicit operator string(KrDocNumberRegularAutoAssigmentViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrDocStateCards

    /// <summary>
    ///     ID: {d9534c6c-ec26-4de9-be78-5df833b70f43}
    ///     Alias: KrDocStateCards
    ///     Caption: $Views_Names_KrDocStateCards
    ///     Group: Kr Wf
    /// </summary>
    public class KrDocStateCardsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrDocStateCards": {d9534c6c-ec26-4de9-be78-5df833b70f43}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd9534c6c,0xec26,0x4de9,0xbe,0x78,0x5d,0xf8,0x33,0xb7,0x0f,0x43);

        /// <summary>
        ///     View name for "KrDocStateCards".
        /// </summary>
        public readonly string Alias = "KrDocStateCards";

        /// <summary>
        ///     View caption for "KrDocStateCards".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrDocStateCards";

        /// <summary>
        ///     View group for "KrDocStateCards".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: StateID
        ///     Caption: $Views_KrDocStates_ID.
        /// </summary>
        public readonly ViewObject ColumnStateID = new ViewObject(0, "StateID", "$Views_KrDocStates_ID");

        /// <summary>
        ///     ID:1 
        ///     Alias: StateName
        ///     Caption: $Views_KrDocStates_Name.
        /// </summary>
        public readonly ViewObject ColumnStateName = new ViewObject(1, "StateName", "$Views_KrDocStates_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: PartitionID.
        /// </summary>
        public readonly ViewObject ColumnPartitionID = new ViewObject(2, "PartitionID");

        /// <summary>
        ///     ID:3 
        ///     Alias: PartitionName
        ///     Caption: $Views_KrDocStates_Partition.
        /// </summary>
        public readonly ViewObject ColumnPartitionName = new ViewObject(3, "PartitionName", "$Views_KrDocStates_Partition");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: ID
        ///     Caption: $Views_KrDocStates_ID.
        /// </summary>
        public readonly ViewObject ParamID = new ViewObject(0, "ID", "$Views_KrDocStates_ID");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_KrDocStates_Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_KrDocStates_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: Partition
        ///     Caption: $Views_KrDocStates_Partition.
        /// </summary>
        public readonly ViewObject ParamPartition = new ViewObject(2, "Partition", "$Views_KrDocStates_Partition");

        #endregion

        #region ToString

        public static implicit operator string(KrDocStateCardsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrDocStates

    /// <summary>
    ///     ID: {51e63141-3564-4819-8bb8-c324cf772aae}
    ///     Alias: KrDocStates
    ///     Caption: $Views_Names_KrDocStates
    ///     Group: Kr Wf
    /// </summary>
    public class KrDocStatesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrDocStates": {51e63141-3564-4819-8bb8-c324cf772aae}.
        /// </summary>
        public readonly Guid ID = new Guid(0x51e63141,0x3564,0x4819,0x8b,0xb8,0xc3,0x24,0xcf,0x77,0x2a,0xae);

        /// <summary>
        ///     View name for "KrDocStates".
        /// </summary>
        public readonly string Alias = "KrDocStates";

        /// <summary>
        ///     View caption for "KrDocStates".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrDocStates";

        /// <summary>
        ///     View group for "KrDocStates".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: StateID.
        /// </summary>
        public readonly ViewObject ColumnStateID = new ViewObject(0, "StateID");

        /// <summary>
        ///     ID:1 
        ///     Alias: StateName
        ///     Caption: $Views_KrDocStates_Name.
        /// </summary>
        public readonly ViewObject ColumnStateName = new ViewObject(1, "StateName", "$Views_KrDocStates_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_KrDocStates_Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_KrDocStates_Name");

        #endregion

        #region ToString

        public static implicit operator string(KrDocStatesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrDocTypes

    /// <summary>
    ///     ID: {f85d195b-7e93-4c09-830c-c9564c450f23}
    ///     Alias: KrDocTypes
    ///     Caption: $Views_Names_KrDocTypes
    ///     Group: Kr Wf
    /// </summary>
    public class KrDocTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrDocTypes": {f85d195b-7e93-4c09-830c-c9564c450f23}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf85d195b,0x7e93,0x4c09,0x83,0x0c,0xc9,0x56,0x4c,0x45,0x0f,0x23);

        /// <summary>
        ///     View name for "KrDocTypes".
        /// </summary>
        public readonly string Alias = "KrDocTypes";

        /// <summary>
        ///     View caption for "KrDocTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrDocTypes";

        /// <summary>
        ///     View group for "KrDocTypes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrDocTypeID.
        /// </summary>
        public readonly ViewObject ColumnKrDocTypeID = new ViewObject(0, "KrDocTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrDocTypeTitle
        ///     Caption: $Views_KrDocTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnKrDocTypeTitle = new ViewObject(1, "KrDocTypeTitle", "$Views_KrDocTypes_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: KrDocCardTypeCaption
        ///     Caption: $Views_KrDocTypes_CardType.
        /// </summary>
        public readonly ViewObject ColumnKrDocCardTypeCaption = new ViewObject(2, "KrDocCardTypeCaption", "$Views_KrDocTypes_CardType");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_KrDocTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_KrDocTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrDocTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrFilteredStageGroups

    /// <summary>
    ///     ID: {888b6543-2e85-45d3-8325-7a80f230560f}
    ///     Alias: KrFilteredStageGroups
    ///     Caption: $Views_Names_KrFilteredStageGroups
    ///     Group: Kr Wf
    /// </summary>
    public class KrFilteredStageGroupsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrFilteredStageGroups": {888b6543-2e85-45d3-8325-7a80f230560f}.
        /// </summary>
        public readonly Guid ID = new Guid(0x888b6543,0x2e85,0x45d3,0x83,0x25,0x7a,0x80,0xf2,0x30,0x56,0x0f);

        /// <summary>
        ///     View name for "KrFilteredStageGroups".
        /// </summary>
        public readonly string Alias = "KrFilteredStageGroups";

        /// <summary>
        ///     View caption for "KrFilteredStageGroups".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrFilteredStageGroups";

        /// <summary>
        ///     View group for "KrFilteredStageGroups".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: StageGroupID.
        /// </summary>
        public readonly ViewObject ColumnStageGroupID = new ViewObject(0, "StageGroupID");

        /// <summary>
        ///     ID:1 
        ///     Alias: StageGroupName
        ///     Caption: $Views_KrStageGroups_Name.
        /// </summary>
        public readonly ViewObject ColumnStageGroupName = new ViewObject(1, "StageGroupName", "$Views_KrStageGroups_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: Description
        ///     Caption: $Views_KrStageGroups_Description.
        /// </summary>
        public readonly ViewObject ColumnDescription = new ViewObject(2, "Description", "$Views_KrStageGroups_Description");

        /// <summary>
        ///     ID:3 
        ///     Alias: IsGroupReadonly
        ///     Caption: $Views_KrStageGroups_IsGroupReadonly.
        /// </summary>
        public readonly ViewObject ColumnIsGroupReadonly = new ViewObject(3, "IsGroupReadonly", "$Views_KrStageGroups_IsGroupReadonly");

        /// <summary>
        ///     ID:4 
        ///     Alias: Order
        ///     Caption: $Views_KrStageGroups_Order.
        /// </summary>
        public readonly ViewObject ColumnOrder = new ViewObject(4, "Order", "$Views_KrStageGroups_Order");

        /// <summary>
        ///     ID:5 
        ///     Alias: Types
        ///     Caption: $Views_KrStageGroups_Types.
        /// </summary>
        public readonly ViewObject ColumnTypes = new ViewObject(5, "Types", "$Views_KrStageGroups_Types");

        /// <summary>
        ///     ID:6 
        ///     Alias: Roles
        ///     Caption: $Views_KrStageGroups_Roles.
        /// </summary>
        public readonly ViewObject ColumnRoles = new ViewObject(6, "Roles", "$Views_KrStageGroups_Roles");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: StageGroupIDParam
        ///     Caption: StageGroupIDParam.
        /// </summary>
        public readonly ViewObject ParamStageGroupIDParam = new ViewObject(0, "StageGroupIDParam", "StageGroupIDParam");

        /// <summary>
        ///     ID:1 
        ///     Alias: StageGroupNameParam
        ///     Caption: $Views_KrStageGroups_Name.
        /// </summary>
        public readonly ViewObject ParamStageGroupNameParam = new ViewObject(1, "StageGroupNameParam", "$Views_KrStageGroups_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: StageGroupDescriptionParam
        ///     Caption: $Views_KrStageGroups_Description.
        /// </summary>
        public readonly ViewObject ParamStageGroupDescriptionParam = new ViewObject(2, "StageGroupDescriptionParam", "$Views_KrStageGroups_Description");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeId
        ///     Caption: TypeId.
        /// </summary>
        public readonly ViewObject ParamTypeId = new ViewObject(3, "TypeId", "TypeId");

        /// <summary>
        ///     ID:4 
        ///     Alias: CardId
        ///     Caption: CardId.
        /// </summary>
        public readonly ViewObject ParamCardId = new ViewObject(4, "CardId", "CardId");

        /// <summary>
        ///     ID:5 
        ///     Alias: IsGroupReadonlyParam
        ///     Caption: $Views_KrStageGroups_IsGroupReadonly.
        /// </summary>
        public readonly ViewObject ParamIsGroupReadonlyParam = new ViewObject(5, "IsGroupReadonlyParam", "$Views_KrStageGroups_IsGroupReadonly");

        /// <summary>
        ///     ID:6 
        ///     Alias: TypeParam
        ///     Caption: $Views_KrStageGroups_Types.
        /// </summary>
        public readonly ViewObject ParamTypeParam = new ViewObject(6, "TypeParam", "$Views_KrStageGroups_Types");

        /// <summary>
        ///     ID:7 
        ///     Alias: RoleParam
        ///     Caption: $Views_KrStageGroups_Roles.
        /// </summary>
        public readonly ViewObject ParamRoleParam = new ViewObject(7, "RoleParam", "$Views_KrStageGroups_Roles");

        #endregion

        #region ToString

        public static implicit operator string(KrFilteredStageGroupsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrFilteredStageTypes

    /// <summary>
    ///     ID: {e046d0cf-be7c-4965-b2d4-47cb943a8a7d}
    ///     Alias: KrFilteredStageTypes
    ///     Caption: $Views_Names_KrFilteredStageTypes
    ///     Group: Kr Wf
    /// </summary>
    public class KrFilteredStageTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrFilteredStageTypes": {e046d0cf-be7c-4965-b2d4-47cb943a8a7d}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe046d0cf,0xbe7c,0x4965,0xb2,0xd4,0x47,0xcb,0x94,0x3a,0x8a,0x7d);

        /// <summary>
        ///     View name for "KrFilteredStageTypes".
        /// </summary>
        public readonly string Alias = "KrFilteredStageTypes";

        /// <summary>
        ///     View caption for "KrFilteredStageTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrFilteredStageTypes";

        /// <summary>
        ///     View group for "KrFilteredStageTypes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: StageTypeID.
        /// </summary>
        public readonly ViewObject ColumnStageTypeID = new ViewObject(0, "StageTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: StageTypeCaption
        ///     Caption: $Views_KrProcessStageTypes_Caption.
        /// </summary>
        public readonly ViewObject ColumnStageTypeCaption = new ViewObject(1, "StageTypeCaption", "$Views_KrProcessStageTypes_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: StageTypeDefaultStageName
        ///     Caption: $Views_KrProcessStageTypes_DefaultStageName.
        /// </summary>
        public readonly ViewObject ColumnStageTypeDefaultStageName = new ViewObject(2, "StageTypeDefaultStageName", "$Views_KrProcessStageTypes_DefaultStageName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: IsTemplate
        ///     Caption: IsTemplate.
        /// </summary>
        public readonly ViewObject ParamIsTemplate = new ViewObject(0, "IsTemplate", "IsTemplate");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeId
        ///     Caption: TypeId.
        /// </summary>
        public readonly ViewObject ParamTypeId = new ViewObject(1, "TypeId", "TypeId");

        /// <summary>
        ///     ID:2 
        ///     Alias: CardId
        ///     Caption: CardId.
        /// </summary>
        public readonly ViewObject ParamCardId = new ViewObject(2, "CardId", "CardId");

        /// <summary>
        ///     ID:3 
        ///     Alias: StageGroupIDParam
        ///     Caption: StageGroupIDParam.
        /// </summary>
        public readonly ViewObject ParamStageGroupIDParam = new ViewObject(3, "StageGroupIDParam", "StageGroupIDParam");

        #endregion

        #region ToString

        public static implicit operator string(KrFilteredStageTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrForkManagementStageTypeModes

    /// <summary>
    ///     ID: {d5a3ccbd-975c-42de-993c-e4bccfd2ac0d}
    ///     Alias: KrForkManagementStageTypeModes
    ///     Caption: $Views_Names_KrForkManagementStageTypeModes
    ///     Group: Kr Wf
    /// </summary>
    public class KrForkManagementStageTypeModesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrForkManagementStageTypeModes": {d5a3ccbd-975c-42de-993c-e4bccfd2ac0d}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd5a3ccbd,0x975c,0x42de,0x99,0x3c,0xe4,0xbc,0xcf,0xd2,0xac,0x0d);

        /// <summary>
        ///     View name for "KrForkManagementStageTypeModes".
        /// </summary>
        public readonly string Alias = "KrForkManagementStageTypeModes";

        /// <summary>
        ///     View caption for "KrForkManagementStageTypeModes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrForkManagementStageTypeModes";

        /// <summary>
        ///     View group for "KrForkManagementStageTypeModes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: RefName.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "RefName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "Name");

        #endregion

        #region ToString

        public static implicit operator string(KrForkManagementStageTypeModesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrManagerTasks

    /// <summary>
    ///     ID: {98e09dab-c265-46e0-96ae-0a81cef3fa20}
    ///     Alias: KrManagerTasks
    ///     Caption: $Views_Names_KrManagerTasks
    ///     Group: Kr Wf
    /// </summary>
    public class KrManagerTasksViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrManagerTasks": {98e09dab-c265-46e0-96ae-0a81cef3fa20}.
        /// </summary>
        public readonly Guid ID = new Guid(0x98e09dab,0xc265,0x46e0,0x96,0xae,0x0a,0x81,0xce,0xf3,0xfa,0x20);

        /// <summary>
        ///     View name for "KrManagerTasks".
        /// </summary>
        public readonly string Alias = "KrManagerTasks";

        /// <summary>
        ///     View caption for "KrManagerTasks".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrManagerTasks";

        /// <summary>
        ///     View group for "KrManagerTasks".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: Caption
        ///     Caption: Caption.
        /// </summary>
        public readonly ViewObject ColumnCaption = new ViewObject(1, "Caption", "Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: ActiveImage
        ///     Caption: ActiveImage.
        /// </summary>
        public readonly ViewObject ColumnActiveImage = new ViewObject(2, "ActiveImage", "ActiveImage");

        /// <summary>
        ///     ID:3 
        ///     Alias: InactiveImage
        ///     Caption: InactiveImage.
        /// </summary>
        public readonly ViewObject ColumnInactiveImage = new ViewObject(3, "InactiveImage", "InactiveImage");

        /// <summary>
        ///     ID:4 
        ///     Alias: Count.
        /// </summary>
        public readonly ViewObject ColumnCount = new ViewObject(4, "Count");

        #endregion

        #region ToString

        public static implicit operator string(KrManagerTasksViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionAclGenerationRules

    /// <summary>
    ///     ID: {8adc6a95-fd78-4efa-922d-43c4c4838e39}
    ///     Alias: KrPermissionAclGenerationRules
    ///     Caption: $Views_Names_KrPermissionAclGenerationRules
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionAclGenerationRulesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionAclGenerationRules": {8adc6a95-fd78-4efa-922d-43c4c4838e39}.
        /// </summary>
        public readonly Guid ID = new Guid(0x8adc6a95,0xfd78,0x4efa,0x92,0x2d,0x43,0xc4,0xc4,0x83,0x8e,0x39);

        /// <summary>
        ///     View name for "KrPermissionAclGenerationRules".
        /// </summary>
        public readonly string Alias = "KrPermissionAclGenerationRules";

        /// <summary>
        ///     View caption for "KrPermissionAclGenerationRules".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionAclGenerationRules";

        /// <summary>
        ///     View group for "KrPermissionAclGenerationRules".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrPermissionAclGenerationRuleID.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionAclGenerationRuleID = new ViewObject(0, "KrPermissionAclGenerationRuleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrPermissionAclGenerationRuleName
        ///     Caption: $Views_KrPermissions_AclGenerationRule.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionAclGenerationRuleName = new ViewObject(1, "KrPermissionAclGenerationRuleName", "$Views_KrPermissions_AclGenerationRule");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: AccessRule
        ///     Caption: Access rule.
        /// </summary>
        public readonly ViewObject ParamAccessRule = new ViewObject(0, "AccessRule", "Access rule");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionAclGenerationRulesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionFlags

    /// <summary>
    ///     ID: {eb357452-657c-40cd-a2f4-f0214d0ac957}
    ///     Alias: KrPermissionFlags
    ///     Caption: $Views_Names_KrPermissionFlags
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionFlagsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionFlags": {eb357452-657c-40cd-a2f4-f0214d0ac957}.
        /// </summary>
        public readonly Guid ID = new Guid(0xeb357452,0x657c,0x40cd,0xa2,0xf4,0xf0,0x21,0x4d,0x0a,0xc9,0x57);

        /// <summary>
        ///     View name for "KrPermissionFlags".
        /// </summary>
        public readonly string Alias = "KrPermissionFlags";

        /// <summary>
        ///     View caption for "KrPermissionFlags".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionFlags";

        /// <summary>
        ///     View group for "KrPermissionFlags".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: FlagID.
        /// </summary>
        public readonly ViewObject ColumnFlagID = new ViewObject(0, "FlagID");

        /// <summary>
        ///     ID:1 
        ///     Alias: FlagCaption
        ///     Caption: $Views_KrPermissionFlags_Name.
        /// </summary>
        public readonly ViewObject ColumnFlagCaption = new ViewObject(1, "FlagCaption", "$Views_KrPermissionFlags_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CaptionParam
        ///     Caption: $Views_KrPermissionFlags_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaptionParam = new ViewObject(0, "CaptionParam", "$Views_KrPermissionFlags_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionFlagsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionRoles

    /// <summary>
    ///     ID: {34026b2a-6699-425c-8669-5ad5c75945f9}
    ///     Alias: KrPermissionRoles
    ///     Caption: $Views_Names_KrPermissionRoles
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionRolesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionRoles": {34026b2a-6699-425c-8669-5ad5c75945f9}.
        /// </summary>
        public readonly Guid ID = new Guid(0x34026b2a,0x6699,0x425c,0x86,0x69,0x5a,0xd5,0xc7,0x59,0x45,0xf9);

        /// <summary>
        ///     View name for "KrPermissionRoles".
        /// </summary>
        public readonly string Alias = "KrPermissionRoles";

        /// <summary>
        ///     View caption for "KrPermissionRoles".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionRoles";

        /// <summary>
        ///     View group for "KrPermissionRoles".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrPermissionRoleID.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionRoleID = new ViewObject(0, "KrPermissionRoleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrPermissionRoleName
        ///     Caption: $Views_KrPermissions_Roles.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionRoleName = new ViewObject(1, "KrPermissionRoleName", "$Views_KrPermissions_Roles");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: AccessRule
        ///     Caption: Access rule.
        /// </summary>
        public readonly ViewObject ParamAccessRule = new ViewObject(0, "AccessRule", "Access rule");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionRolesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionRuleAccessSettings

    /// <summary>
    ///     ID: {e9005e6d-e9d0-4643-86aa-8f0c72826e28}
    ///     Alias: KrPermissionRuleAccessSettings
    ///     Caption: $Views_Names_KrPermissionRuleAccessSettings
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionRuleAccessSettingsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionRuleAccessSettings": {e9005e6d-e9d0-4643-86aa-8f0c72826e28}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe9005e6d,0xe9d0,0x4643,0x86,0xaa,0x8f,0x0c,0x72,0x82,0x6e,0x28);

        /// <summary>
        ///     View name for "KrPermissionRuleAccessSettings".
        /// </summary>
        public readonly string Alias = "KrPermissionRuleAccessSettings";

        /// <summary>
        ///     View caption for "KrPermissionRuleAccessSettings".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionRuleAccessSettings";

        /// <summary>
        ///     View group for "KrPermissionRuleAccessSettings".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: AccessSettingID.
        /// </summary>
        public readonly ViewObject ColumnAccessSettingID = new ViewObject(0, "AccessSettingID");

        /// <summary>
        ///     ID:1 
        ///     Alias: AccessSettingName
        ///     Caption: $Views_KrPermissionRuleAccessSettings_Name.
        /// </summary>
        public readonly ViewObject ColumnAccessSettingName = new ViewObject(1, "AccessSettingName", "$Views_KrPermissionRuleAccessSettings_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_KrPermissionRuleAccessSettings_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_KrPermissionRuleAccessSettings_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: SectionType
        ///     Caption: Section type.
        /// </summary>
        public readonly ViewObject ParamSectionType = new ViewObject(1, "SectionType", "Section type");

        /// <summary>
        ///     ID:2 
        ///     Alias: WithMaskLevel
        ///     Caption: With mask level.
        /// </summary>
        public readonly ViewObject ParamWithMaskLevel = new ViewObject(2, "WithMaskLevel", "With mask level");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionRuleAccessSettingsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissions

    /// <summary>
    ///     ID: {42facec2-7986-4456-b089-972413cf8e89}
    ///     Alias: KrPermissions
    ///     Caption: $Views_Names_KrPermissions
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissions": {42facec2-7986-4456-b089-972413cf8e89}.
        /// </summary>
        public readonly Guid ID = new Guid(0x42facec2,0x7986,0x4456,0xb0,0x89,0x97,0x24,0x13,0xcf,0x8e,0x89);

        /// <summary>
        ///     View name for "KrPermissions".
        /// </summary>
        public readonly string Alias = "KrPermissions";

        /// <summary>
        ///     View caption for "KrPermissions".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissions";

        /// <summary>
        ///     View group for "KrPermissions".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrPermissionsID.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsID = new ViewObject(0, "KrPermissionsID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrPermissionsCaption
        ///     Caption: $Views_KrPermissions_Name.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsCaption = new ViewObject(1, "KrPermissionsCaption", "$Views_KrPermissions_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: KrPermissionsDescription
        ///     Caption: $Views_KrPermissions_Description.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsDescription = new ViewObject(2, "KrPermissionsDescription", "$Views_KrPermissions_Description");

        /// <summary>
        ///     ID:3 
        ///     Alias: KrPermissionsPriority
        ///     Caption: $Views_KrPermissions_Priority.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsPriority = new ViewObject(3, "KrPermissionsPriority", "$Views_KrPermissions_Priority");

        /// <summary>
        ///     ID:4 
        ///     Alias: KrPermissionsIsDisabled
        ///     Caption: $Views_KrPermissions_IsDisabled.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsIsDisabled = new ViewObject(4, "KrPermissionsIsDisabled", "$Views_KrPermissions_IsDisabled");

        /// <summary>
        ///     ID:5 
        ///     Alias: KrPermissionsAlwaysCheck
        ///     Caption: $Views_KrPermissions_AlwaysCheck.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsAlwaysCheck = new ViewObject(5, "KrPermissionsAlwaysCheck", "$Views_KrPermissions_AlwaysCheck");

        /// <summary>
        ///     ID:6 
        ///     Alias: KrPermissionsUseExtendedSettings
        ///     Caption: $Views_KrPermissions_UseExtendedSettings.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsUseExtendedSettings = new ViewObject(6, "KrPermissionsUseExtendedSettings", "$Views_KrPermissions_UseExtendedSettings");

        /// <summary>
        ///     ID:7 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(7, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_KrPermissions_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_KrPermissions_Caption_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Description
        ///     Caption: $Views_KrPermissions_Description_Param.
        /// </summary>
        public readonly ViewObject ParamDescription = new ViewObject(1, "Description", "$Views_KrPermissions_Description_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Type
        ///     Caption: $Views_KrPermissions_Type_Param.
        /// </summary>
        public readonly ViewObject ParamType = new ViewObject(2, "Type", "$Views_KrPermissions_Type_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: State
        ///     Caption: $Views_KrPermissions_State_Param.
        /// </summary>
        public readonly ViewObject ParamState = new ViewObject(3, "State", "$Views_KrPermissions_State_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Role
        ///     Caption: $Views_KrPermissions_Role_Param.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(4, "Role", "$Views_KrPermissions_Role_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: AclGenerationRule
        ///     Caption: $Views_KrPermissions_AclGenerationRule_Param.
        /// </summary>
        public readonly ViewObject ParamAclGenerationRule = new ViewObject(5, "AclGenerationRule", "$Views_KrPermissions_AclGenerationRule_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Permission
        ///     Caption: $Views_KrPermissions_Permission_Param.
        /// </summary>
        public readonly ViewObject ParamPermission = new ViewObject(6, "Permission", "$Views_KrPermissions_Permission_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: User
        ///     Caption: $Views_KrPermissions_User_Param.
        /// </summary>
        public readonly ViewObject ParamUser = new ViewObject(7, "User", "$Views_KrPermissions_User_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: Priority
        ///     Caption: $Views_KrPermissions_Priority_Param.
        /// </summary>
        public readonly ViewObject ParamPriority = new ViewObject(8, "Priority", "$Views_KrPermissions_Priority_Param");

        /// <summary>
        ///     ID:9 
        ///     Alias: IsDisabled
        ///     Caption: $Views_KrPermissions_IsDisabled_Param.
        /// </summary>
        public readonly ViewObject ParamIsDisabled = new ViewObject(9, "IsDisabled", "$Views_KrPermissions_IsDisabled_Param");

        /// <summary>
        ///     ID:10 
        ///     Alias: IsRequired
        ///     Caption: $Views_KrPermissions_AlwaysCheck_Param.
        /// </summary>
        public readonly ViewObject ParamIsRequired = new ViewObject(10, "IsRequired", "$Views_KrPermissions_AlwaysCheck_Param");

        /// <summary>
        ///     ID:11 
        ///     Alias: IsExtended
        ///     Caption: $Views_KrPermissions_UseExtendedSettings_Param.
        /// </summary>
        public readonly ViewObject ParamIsExtended = new ViewObject(11, "IsExtended", "$Views_KrPermissions_UseExtendedSettings_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionsControlTypes

    /// <summary>
    ///     ID: {8053d28d-666a-4997-b0ef-aff1298c4aaf}
    ///     Alias: KrPermissionsControlTypes
    ///     Caption: $Views_Names_KrPermissionsControlTypes
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionsControlTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionsControlTypes": {8053d28d-666a-4997-b0ef-aff1298c4aaf}.
        /// </summary>
        public readonly Guid ID = new Guid(0x8053d28d,0x666a,0x4997,0xb0,0xef,0xaf,0xf1,0x29,0x8c,0x4a,0xaf);

        /// <summary>
        ///     View name for "KrPermissionsControlTypes".
        /// </summary>
        public readonly string Alias = "KrPermissionsControlTypes";

        /// <summary>
        ///     View caption for "KrPermissionsControlTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionsControlTypes";

        /// <summary>
        ///     View group for "KrPermissionsControlTypes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ControlTypeID.
        /// </summary>
        public readonly ViewObject ColumnControlTypeID = new ViewObject(0, "ControlTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ControlTypeName
        ///     Caption: $Views_KrPermissionsControlTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnControlTypeName = new ViewObject(1, "ControlTypeName", "$Views_KrPermissionsControlTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_KrPermissionsControlTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_KrPermissionsControlTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsControlTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionsExtendedCards

    /// <summary>
    ///     ID: {1175f833-a028-481a-be77-69bab81a01a2}
    ///     Alias: KrPermissionsExtendedCards
    ///     Caption: $Views_Names_KrPermissionsExtendedCards
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionsExtendedCardsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionsExtendedCards": {1175f833-a028-481a-be77-69bab81a01a2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x1175f833,0xa028,0x481a,0xbe,0x77,0x69,0xba,0xb8,0x1a,0x01,0xa2);

        /// <summary>
        ///     View name for "KrPermissionsExtendedCards".
        /// </summary>
        public readonly string Alias = "KrPermissionsExtendedCards";

        /// <summary>
        ///     View caption for "KrPermissionsExtendedCards".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionsExtendedCards";

        /// <summary>
        ///     View group for "KrPermissionsExtendedCards".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrPermissionsID.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsID = new ViewObject(0, "KrPermissionsID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrPermissionsCaption
        ///     Caption: $Views_KrPermissionsExtendedCards_AccessRule.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsCaption = new ViewObject(1, "KrPermissionsCaption", "$Views_KrPermissionsExtendedCards_AccessRule");

        /// <summary>
        ///     ID:2 
        ///     Alias: KrPermissionsPriority
        ///     Caption: $Views_KrPermissionsExtendedCards_Priority.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsPriority = new ViewObject(2, "KrPermissionsPriority", "$Views_KrPermissionsExtendedCards_Priority");

        /// <summary>
        ///     ID:3 
        ///     Alias: KrPermissionsSection
        ///     Caption: $Views_KrPermissionsExtendedCards_Section.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsSection = new ViewObject(3, "KrPermissionsSection", "$Views_KrPermissionsExtendedCards_Section");

        /// <summary>
        ///     ID:4 
        ///     Alias: KrPermissionsFields
        ///     Caption: $Views_KrPermissionsExtendedCards_Fields.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsFields = new ViewObject(4, "KrPermissionsFields", "$Views_KrPermissionsExtendedCards_Fields");

        /// <summary>
        ///     ID:5 
        ///     Alias: KrPermissionsAccessSetting
        ///     Caption: $Views_KrPermissionsExtendedCards_AccessSetting.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsAccessSetting = new ViewObject(5, "KrPermissionsAccessSetting", "$Views_KrPermissionsExtendedCards_AccessSetting");

        /// <summary>
        ///     ID:6 
        ///     Alias: KrPermissionsIsHidden
        ///     Caption: $Views_KrPermissionsExtendedCards_Hide.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsIsHidden = new ViewObject(6, "KrPermissionsIsHidden", "$Views_KrPermissionsExtendedCards_Hide");

        /// <summary>
        ///     ID:7 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(7, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_KrPermissionsExtendedCards_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_KrPermissionsExtendedCards_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Priority
        ///     Caption: $Views_KrPermissionsExtendedCards_Priority_Param.
        /// </summary>
        public readonly ViewObject ParamPriority = new ViewObject(1, "Priority", "$Views_KrPermissionsExtendedCards_Priority_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: AccessRule
        ///     Caption: $Views_KrPermissionsExtendedCards_AccessRule_Param.
        /// </summary>
        public readonly ViewObject ParamAccessRule = new ViewObject(2, "AccessRule", "$Views_KrPermissionsExtendedCards_AccessRule_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Section
        ///     Caption: $Views_KrPermissionsExtendedCards_Section_Param.
        /// </summary>
        public readonly ViewObject ParamSection = new ViewObject(3, "Section", "$Views_KrPermissionsExtendedCards_Section_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Field
        ///     Caption: $Views_KrPermissionsExtendedCards_Field_Param.
        /// </summary>
        public readonly ViewObject ParamField = new ViewObject(4, "Field", "$Views_KrPermissionsExtendedCards_Field_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: AccessSetting
        ///     Caption: $Views_KrPermissionsExtendedCards_AccessSetting_Param.
        /// </summary>
        public readonly ViewObject ParamAccessSetting = new ViewObject(5, "AccessSetting", "$Views_KrPermissionsExtendedCards_AccessSetting_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: IsHidden
        ///     Caption: $Views_KrPermissionsExtendedCards_Hide_Param.
        /// </summary>
        public readonly ViewObject ParamIsHidden = new ViewObject(6, "IsHidden", "$Views_KrPermissionsExtendedCards_Hide_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsExtendedCardsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionsExtendedFiles

    /// <summary>
    ///     ID: {76a033f1-404b-4dee-9305-9cc8bf8c22f0}
    ///     Alias: KrPermissionsExtendedFiles
    ///     Caption: $Views_Names_KrPermissionsExtendedFiles
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionsExtendedFilesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionsExtendedFiles": {76a033f1-404b-4dee-9305-9cc8bf8c22f0}.
        /// </summary>
        public readonly Guid ID = new Guid(0x76a033f1,0x404b,0x4dee,0x93,0x05,0x9c,0xc8,0xbf,0x8c,0x22,0xf0);

        /// <summary>
        ///     View name for "KrPermissionsExtendedFiles".
        /// </summary>
        public readonly string Alias = "KrPermissionsExtendedFiles";

        /// <summary>
        ///     View caption for "KrPermissionsExtendedFiles".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionsExtendedFiles";

        /// <summary>
        ///     View group for "KrPermissionsExtendedFiles".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrPermissionsID.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsID = new ViewObject(0, "KrPermissionsID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrPermissionsCaption
        ///     Caption: $Views_KrPermissionsExtendedFiles_AccessRule.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsCaption = new ViewObject(1, "KrPermissionsCaption", "$Views_KrPermissionsExtendedFiles_AccessRule");

        /// <summary>
        ///     ID:2 
        ///     Alias: KrPermissionsPriority
        ///     Caption: $Views_KrPermissionsExtendedFiles_Priority.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsPriority = new ViewObject(2, "KrPermissionsPriority", "$Views_KrPermissionsExtendedFiles_Priority");

        /// <summary>
        ///     ID:3 
        ///     Alias: KrPermissionsExtensions
        ///     Caption: $Views_KrPermissionsExtendedFiles_Extensions.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsExtensions = new ViewObject(3, "KrPermissionsExtensions", "$Views_KrPermissionsExtendedFiles_Extensions");

        /// <summary>
        ///     ID:4 
        ///     Alias: KrPermissionsCategories
        ///     Caption: $Views_KrPermissionsExtendedFiles_FileCategories.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsCategories = new ViewObject(4, "KrPermissionsCategories", "$Views_KrPermissionsExtendedFiles_FileCategories");

        /// <summary>
        ///     ID:5 
        ///     Alias: KrPermissionsFileCheckRule
        ///     Caption: $Views_KrPermissionsExtendedFiles_FileCheckRule.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsFileCheckRule = new ViewObject(5, "KrPermissionsFileCheckRule", "$Views_KrPermissionsExtendedFiles_FileCheckRule");

        /// <summary>
        ///     ID:6 
        ///     Alias: KrPermissionsReadAccessSetting
        ///     Caption: $Views_KrPermissionsExtendedFiles_ReadAccessSetting.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsReadAccessSetting = new ViewObject(6, "KrPermissionsReadAccessSetting", "$Views_KrPermissionsExtendedFiles_ReadAccessSetting");

        /// <summary>
        ///     ID:7 
        ///     Alias: KrPermissionsEditAccessSetting
        ///     Caption: $Views_KrPermissionsExtendedFiles_EditAccessSetting.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsEditAccessSetting = new ViewObject(7, "KrPermissionsEditAccessSetting", "$Views_KrPermissionsExtendedFiles_EditAccessSetting");

        /// <summary>
        ///     ID:8 
        ///     Alias: KrPermissionsDeleteAccessSetting
        ///     Caption: $Views_KrPermissionsExtendedFiles_DeleteAccessSetting.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsDeleteAccessSetting = new ViewObject(8, "KrPermissionsDeleteAccessSetting", "$Views_KrPermissionsExtendedFiles_DeleteAccessSetting");

        /// <summary>
        ///     ID:9 
        ///     Alias: KrPermissionsSignAccessSetting
        ///     Caption: $Views_KrPermissionsExtendedFiles_SignAccessSetting.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsSignAccessSetting = new ViewObject(9, "KrPermissionsSignAccessSetting", "$Views_KrPermissionsExtendedFiles_SignAccessSetting");

        /// <summary>
        ///     ID:10 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(10, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_KrPermissionsExtendedFiles_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_KrPermissionsExtendedFiles_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: AccessRule
        ///     Caption: $Views_KrPermissionsExtendedFiles_AccessRule_Param.
        /// </summary>
        public readonly ViewObject ParamAccessRule = new ViewObject(1, "AccessRule", "$Views_KrPermissionsExtendedFiles_AccessRule_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Extensions
        ///     Caption: $Views_KrPermissionsExtendedFiles_Extensions_Param.
        /// </summary>
        public readonly ViewObject ParamExtensions = new ViewObject(2, "Extensions", "$Views_KrPermissionsExtendedFiles_Extensions_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Category
        ///     Caption: $Views_KrPermissionsExtendedFiles_FileCategory_Param.
        /// </summary>
        public readonly ViewObject ParamCategory = new ViewObject(3, "Category", "$Views_KrPermissionsExtendedFiles_FileCategory_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: FileCheckRule
        ///     Caption: $Views_KrPermissionsExtendedFiles_FileCheckRule_Param.
        /// </summary>
        public readonly ViewObject ParamFileCheckRule = new ViewObject(4, "FileCheckRule", "$Views_KrPermissionsExtendedFiles_FileCheckRule_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: ReadAccessSetting
        ///     Caption: $Views_KrPermissionsExtendedFiles_ReadAccessSetting_Param.
        /// </summary>
        public readonly ViewObject ParamReadAccessSetting = new ViewObject(5, "ReadAccessSetting", "$Views_KrPermissionsExtendedFiles_ReadAccessSetting_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: EditAccessSetting
        ///     Caption: $Views_KrPermissionsExtendedFiles_EditAccessSetting_Param.
        /// </summary>
        public readonly ViewObject ParamEditAccessSetting = new ViewObject(6, "EditAccessSetting", "$Views_KrPermissionsExtendedFiles_EditAccessSetting_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: DeleteAccessSetting
        ///     Caption: $Views_KrPermissionsExtendedFiles_DeleteAccessSetting_Param.
        /// </summary>
        public readonly ViewObject ParamDeleteAccessSetting = new ViewObject(7, "DeleteAccessSetting", "$Views_KrPermissionsExtendedFiles_DeleteAccessSetting_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: SignAccessSetting
        ///     Caption: $Views_KrPermissionsExtendedFiles_SignAccessSetting_Param.
        /// </summary>
        public readonly ViewObject ParamSignAccessSetting = new ViewObject(8, "SignAccessSetting", "$Views_KrPermissionsExtendedFiles_SignAccessSetting_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsExtendedFilesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionsExtendedMandatory

    /// <summary>
    ///     ID: {73970cb5-843d-49ec-821f-cb069463c1aa}
    ///     Alias: KrPermissionsExtendedMandatory
    ///     Caption: $Views_Names_KrPermissionsExtendedMandatory
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionsExtendedMandatoryViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionsExtendedMandatory": {73970cb5-843d-49ec-821f-cb069463c1aa}.
        /// </summary>
        public readonly Guid ID = new Guid(0x73970cb5,0x843d,0x49ec,0x82,0x1f,0xcb,0x06,0x94,0x63,0xc1,0xaa);

        /// <summary>
        ///     View name for "KrPermissionsExtendedMandatory".
        /// </summary>
        public readonly string Alias = "KrPermissionsExtendedMandatory";

        /// <summary>
        ///     View caption for "KrPermissionsExtendedMandatory".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionsExtendedMandatory";

        /// <summary>
        ///     View group for "KrPermissionsExtendedMandatory".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrPermissionsID.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsID = new ViewObject(0, "KrPermissionsID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrPermissionsCaption
        ///     Caption: $Views_KrPermissionsExtendedMandatory_AccessRule.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsCaption = new ViewObject(1, "KrPermissionsCaption", "$Views_KrPermissionsExtendedMandatory_AccessRule");

        /// <summary>
        ///     ID:2 
        ///     Alias: KrPermissionsSection
        ///     Caption: $Views_KrPermissionsExtendedMandatory_Section.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsSection = new ViewObject(2, "KrPermissionsSection", "$Views_KrPermissionsExtendedMandatory_Section");

        /// <summary>
        ///     ID:3 
        ///     Alias: KrPermissionsFields
        ///     Caption: $Views_KrPermissionsExtendedMandatory_Fields.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsFields = new ViewObject(3, "KrPermissionsFields", "$Views_KrPermissionsExtendedMandatory_Fields");

        /// <summary>
        ///     ID:4 
        ///     Alias: KrPermissionsValidationType
        ///     Caption: $Views_KrPermissionsExtendedMandatory_ValidationType.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsValidationType = new ViewObject(4, "KrPermissionsValidationType", "$Views_KrPermissionsExtendedMandatory_ValidationType");

        /// <summary>
        ///     ID:5 
        ///     Alias: KrPermissionsTaskTypes
        ///     Caption: $Views_KrPermissionsExtendedMandatory_TaskTypes.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsTaskTypes = new ViewObject(5, "KrPermissionsTaskTypes", "$Views_KrPermissionsExtendedMandatory_TaskTypes");

        /// <summary>
        ///     ID:6 
        ///     Alias: KrPermissionsCompletionOptions
        ///     Caption: $Views_KrPermissionsExtendedMandatory_CompletionOptions.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsCompletionOptions = new ViewObject(6, "KrPermissionsCompletionOptions", "$Views_KrPermissionsExtendedMandatory_CompletionOptions");

        /// <summary>
        ///     ID:7 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(7, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_KrPermissionsExtendedMandatory_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_KrPermissionsExtendedMandatory_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: AccessRule
        ///     Caption: $Views_KrPermissionsExtendedMandatory_AccessRule_Param.
        /// </summary>
        public readonly ViewObject ParamAccessRule = new ViewObject(1, "AccessRule", "$Views_KrPermissionsExtendedMandatory_AccessRule_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Section
        ///     Caption: $Views_KrPermissionsExtendedMandatory_Section_Param.
        /// </summary>
        public readonly ViewObject ParamSection = new ViewObject(2, "Section", "$Views_KrPermissionsExtendedMandatory_Section_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Field
        ///     Caption: $Views_KrPermissionsExtendedMandatory_Field_Param.
        /// </summary>
        public readonly ViewObject ParamField = new ViewObject(3, "Field", "$Views_KrPermissionsExtendedMandatory_Field_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: ValidationType
        ///     Caption: $Views_KrPermissionsExtendedMandatory_ValidationType_Param.
        /// </summary>
        public readonly ViewObject ParamValidationType = new ViewObject(4, "ValidationType", "$Views_KrPermissionsExtendedMandatory_ValidationType_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: TaskType
        ///     Caption: $Views_KrPermissionsExtendedMandatory_TaskType_Param.
        /// </summary>
        public readonly ViewObject ParamTaskType = new ViewObject(5, "TaskType", "$Views_KrPermissionsExtendedMandatory_TaskType_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: CompletionOption
        ///     Caption: $Views_KrPermissionsExtendedMandatory_CompletionOption_Param.
        /// </summary>
        public readonly ViewObject ParamCompletionOption = new ViewObject(6, "CompletionOption", "$Views_KrPermissionsExtendedMandatory_CompletionOption_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsExtendedMandatoryViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionsExtendedTasks

    /// <summary>
    ///     ID: {3d026d41-fe09-4b2d-bd5f-bb482c6c7726}
    ///     Alias: KrPermissionsExtendedTasks
    ///     Caption: $Views_Names_KrPermissionsExtendedTasks
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionsExtendedTasksViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionsExtendedTasks": {3d026d41-fe09-4b2d-bd5f-bb482c6c7726}.
        /// </summary>
        public readonly Guid ID = new Guid(0x3d026d41,0xfe09,0x4b2d,0xbd,0x5f,0xbb,0x48,0x2c,0x6c,0x77,0x26);

        /// <summary>
        ///     View name for "KrPermissionsExtendedTasks".
        /// </summary>
        public readonly string Alias = "KrPermissionsExtendedTasks";

        /// <summary>
        ///     View caption for "KrPermissionsExtendedTasks".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionsExtendedTasks";

        /// <summary>
        ///     View group for "KrPermissionsExtendedTasks".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrPermissionsID.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsID = new ViewObject(0, "KrPermissionsID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrPermissionsCaption
        ///     Caption: $Views_KrPermissionsExtendedTasks_AccessRule.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsCaption = new ViewObject(1, "KrPermissionsCaption", "$Views_KrPermissionsExtendedTasks_AccessRule");

        /// <summary>
        ///     ID:2 
        ///     Alias: KrPermissionsPriority
        ///     Caption: $Views_KrPermissionsExtendedTasks_Priority.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsPriority = new ViewObject(2, "KrPermissionsPriority", "$Views_KrPermissionsExtendedTasks_Priority");

        /// <summary>
        ///     ID:3 
        ///     Alias: KrPermissionsTaskTypes
        ///     Caption: $Views_KrPermissionsExtendedTasks_TaskTypes.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsTaskTypes = new ViewObject(3, "KrPermissionsTaskTypes", "$Views_KrPermissionsExtendedTasks_TaskTypes");

        /// <summary>
        ///     ID:4 
        ///     Alias: KrPermissionsSection
        ///     Caption: $Views_KrPermissionsExtendedTasks_Section.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsSection = new ViewObject(4, "KrPermissionsSection", "$Views_KrPermissionsExtendedTasks_Section");

        /// <summary>
        ///     ID:5 
        ///     Alias: KrPermissionsFields
        ///     Caption: $Views_KrPermissionsExtendedTasks_Fields.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsFields = new ViewObject(5, "KrPermissionsFields", "$Views_KrPermissionsExtendedTasks_Fields");

        /// <summary>
        ///     ID:6 
        ///     Alias: KrPermissionsAccessSetting
        ///     Caption: $Views_KrPermissionsExtendedTasks_AccessSetting.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsAccessSetting = new ViewObject(6, "KrPermissionsAccessSetting", "$Views_KrPermissionsExtendedTasks_AccessSetting");

        /// <summary>
        ///     ID:7 
        ///     Alias: KrPermissionsIsHidden
        ///     Caption: $Views_KrPermissionsExtendedTasks_Hide.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsIsHidden = new ViewObject(7, "KrPermissionsIsHidden", "$Views_KrPermissionsExtendedTasks_Hide");

        /// <summary>
        ///     ID:8 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(8, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_KrPermissionsExtendedTasks_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_KrPermissionsExtendedTasks_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: AccessRule
        ///     Caption: $Views_KrPermissionsExtendedTasks_AccessRule_Param.
        /// </summary>
        public readonly ViewObject ParamAccessRule = new ViewObject(1, "AccessRule", "$Views_KrPermissionsExtendedTasks_AccessRule_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Priority
        ///     Caption: $Views_KrPermissionsExtendedTasks_Priority_Param.
        /// </summary>
        public readonly ViewObject ParamPriority = new ViewObject(2, "Priority", "$Views_KrPermissionsExtendedTasks_Priority_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: TaskType
        ///     Caption: $Views_KrPermissionsExtendedTasks_TaskType_Param.
        /// </summary>
        public readonly ViewObject ParamTaskType = new ViewObject(3, "TaskType", "$Views_KrPermissionsExtendedTasks_TaskType_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Section
        ///     Caption: $Views_KrPermissionsExtendedTasks_Section_Param.
        /// </summary>
        public readonly ViewObject ParamSection = new ViewObject(4, "Section", "$Views_KrPermissionsExtendedTasks_Section_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Field
        ///     Caption: $Views_KrPermissionsExtendedTasks_Field_Param.
        /// </summary>
        public readonly ViewObject ParamField = new ViewObject(5, "Field", "$Views_KrPermissionsExtendedTasks_Field_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: AccessSetting
        ///     Caption: $Views_KrPermissionsExtendedTasks_AccessSetting_Param.
        /// </summary>
        public readonly ViewObject ParamAccessSetting = new ViewObject(6, "AccessSetting", "$Views_KrPermissionsExtendedTasks_AccessSetting_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: IsHidden
        ///     Caption: $Views_KrPermissionsExtendedTasks_Hide_Param.
        /// </summary>
        public readonly ViewObject ParamIsHidden = new ViewObject(7, "IsHidden", "$Views_KrPermissionsExtendedTasks_Hide_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsExtendedTasksViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionsExtendedVisibility

    /// <summary>
    ///     ID: {acbfb44c-d180-4b5b-9719-5868631b998a}
    ///     Alias: KrPermissionsExtendedVisibility
    ///     Caption: $Views_Names_KrPermissionsExtendedVisibility
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionsExtendedVisibilityViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionsExtendedVisibility": {acbfb44c-d180-4b5b-9719-5868631b998a}.
        /// </summary>
        public readonly Guid ID = new Guid(0xacbfb44c,0xd180,0x4b5b,0x97,0x19,0x58,0x68,0x63,0x1b,0x99,0x8a);

        /// <summary>
        ///     View name for "KrPermissionsExtendedVisibility".
        /// </summary>
        public readonly string Alias = "KrPermissionsExtendedVisibility";

        /// <summary>
        ///     View caption for "KrPermissionsExtendedVisibility".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionsExtendedVisibility";

        /// <summary>
        ///     View group for "KrPermissionsExtendedVisibility".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrPermissionsID.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsID = new ViewObject(0, "KrPermissionsID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrPermissionsCaption
        ///     Caption: $Views_KrPermissionsExtendedVisibility_AccessRule.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsCaption = new ViewObject(1, "KrPermissionsCaption", "$Views_KrPermissionsExtendedVisibility_AccessRule");

        /// <summary>
        ///     ID:2 
        ///     Alias: KrPermissionsControlAlias
        ///     Caption: $Views_KrPermissionsExtendedVisibility_ControlAlias.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsControlAlias = new ViewObject(2, "KrPermissionsControlAlias", "$Views_KrPermissionsExtendedVisibility_ControlAlias");

        /// <summary>
        ///     ID:3 
        ///     Alias: KrPermissionsControlType
        ///     Caption: $Views_KrPermissionsExtendedVisibility_ControlType.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsControlType = new ViewObject(3, "KrPermissionsControlType", "$Views_KrPermissionsExtendedVisibility_ControlType");

        /// <summary>
        ///     ID:4 
        ///     Alias: KrPermissionsIsHidden
        ///     Caption: $Views_KrPermissionsExtendedVisibility_HideControl.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsIsHidden = new ViewObject(4, "KrPermissionsIsHidden", "$Views_KrPermissionsExtendedVisibility_HideControl");

        /// <summary>
        ///     ID:5 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(5, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_KrPermissionsExtendedVisibility_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_KrPermissionsExtendedVisibility_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: AccessRule
        ///     Caption: $Views_KrPermissionsExtendedVisibility_AccessRule_Param.
        /// </summary>
        public readonly ViewObject ParamAccessRule = new ViewObject(1, "AccessRule", "$Views_KrPermissionsExtendedVisibility_AccessRule_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Alias
        ///     Caption: $Views_KrPermissionsExtendedVisibility_ControlAlias_Param.
        /// </summary>
        public readonly ViewObject ParamAlias = new ViewObject(2, "Alias", "$Views_KrPermissionsExtendedVisibility_ControlAlias_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: ControlType
        ///     Caption: $Views_KrPermissionsExtendedVisibility_ControlType_Param.
        /// </summary>
        public readonly ViewObject ParamControlType = new ViewObject(3, "ControlType", "$Views_KrPermissionsExtendedVisibility_ControlType_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: IsHidden
        ///     Caption: $Views_KrPermissionsExtendedVisibility_HideControl_Param.
        /// </summary>
        public readonly ViewObject ParamIsHidden = new ViewObject(4, "IsHidden", "$Views_KrPermissionsExtendedVisibility_HideControl_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsExtendedVisibilityViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionsFileCheckRules

    /// <summary>
    ///     ID: {2215eeaa-790a-4389-b800-7790487318aa}
    ///     Alias: KrPermissionsFileCheckRules
    ///     Caption: $Views_KrPermissionsFileCheckRules
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionsFileCheckRulesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionsFileCheckRules": {2215eeaa-790a-4389-b800-7790487318aa}.
        /// </summary>
        public readonly Guid ID = new Guid(0x2215eeaa,0x790a,0x4389,0xb8,0x00,0x77,0x90,0x48,0x73,0x18,0xaa);

        /// <summary>
        ///     View name for "KrPermissionsFileCheckRules".
        /// </summary>
        public readonly string Alias = "KrPermissionsFileCheckRules";

        /// <summary>
        ///     View caption for "KrPermissionsFileCheckRules".
        /// </summary>
        public readonly string Caption = "$Views_KrPermissionsFileCheckRules";

        /// <summary>
        ///     View group for "KrPermissionsFileCheckRules".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: FileCheckRuleID.
        /// </summary>
        public readonly ViewObject ColumnFileCheckRuleID = new ViewObject(0, "FileCheckRuleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: FileCheckRuleName
        ///     Caption: $Views_KrPermissionsFileCheckRules_Name.
        /// </summary>
        public readonly ViewObject ColumnFileCheckRuleName = new ViewObject(1, "FileCheckRuleName", "$Views_KrPermissionsFileCheckRules_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_KrPermissionsFileCheckRules_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_KrPermissionsFileCheckRules_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsFileCheckRulesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionsFileEditAccessSettings

    /// <summary>
    ///     ID: {e0588f6d-d7e0-4c3b-bf90-82b898bd512b}
    ///     Alias: KrPermissionsFileEditAccessSettings
    ///     Caption: $Views_Names_KrPermissionsFileEditAccessSettings
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionsFileEditAccessSettingsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionsFileEditAccessSettings": {e0588f6d-d7e0-4c3b-bf90-82b898bd512b}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe0588f6d,0xd7e0,0x4c3b,0xbf,0x90,0x82,0xb8,0x98,0xbd,0x51,0x2b);

        /// <summary>
        ///     View name for "KrPermissionsFileEditAccessSettings".
        /// </summary>
        public readonly string Alias = "KrPermissionsFileEditAccessSettings";

        /// <summary>
        ///     View caption for "KrPermissionsFileEditAccessSettings".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionsFileEditAccessSettings";

        /// <summary>
        ///     View group for "KrPermissionsFileEditAccessSettings".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: AccessSettingID.
        /// </summary>
        public readonly ViewObject ColumnAccessSettingID = new ViewObject(0, "AccessSettingID");

        /// <summary>
        ///     ID:1 
        ///     Alias: AccessSettingName
        ///     Caption: $Views_KrPermissionsFileEditAccessSettings_Name.
        /// </summary>
        public readonly ViewObject ColumnAccessSettingName = new ViewObject(1, "AccessSettingName", "$Views_KrPermissionsFileEditAccessSettings_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_KrPermissionsFileEditAccessSettings_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_KrPermissionsFileEditAccessSettings_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsFileEditAccessSettingsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionsFileReadAccessSettings

    /// <summary>
    ///     ID: {e8b1f86f-b19e-426f-8703-d87359d75c32}
    ///     Alias: KrPermissionsFileReadAccessSettings
    ///     Caption: $Views_Names_KrPermissionsFileReadAccessSettings
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionsFileReadAccessSettingsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionsFileReadAccessSettings": {e8b1f86f-b19e-426f-8703-d87359d75c32}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe8b1f86f,0xb19e,0x426f,0x87,0x03,0xd8,0x73,0x59,0xd7,0x5c,0x32);

        /// <summary>
        ///     View name for "KrPermissionsFileReadAccessSettings".
        /// </summary>
        public readonly string Alias = "KrPermissionsFileReadAccessSettings";

        /// <summary>
        ///     View caption for "KrPermissionsFileReadAccessSettings".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionsFileReadAccessSettings";

        /// <summary>
        ///     View group for "KrPermissionsFileReadAccessSettings".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: AccessSettingID.
        /// </summary>
        public readonly ViewObject ColumnAccessSettingID = new ViewObject(0, "AccessSettingID");

        /// <summary>
        ///     ID:1 
        ///     Alias: AccessSettingName
        ///     Caption: $Views_KrPermissionsFileReadAccessSettings_Name.
        /// </summary>
        public readonly ViewObject ColumnAccessSettingName = new ViewObject(1, "AccessSettingName", "$Views_KrPermissionsFileReadAccessSettings_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_KrPermissionsFileReadAccessSettings_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_KrPermissionsFileReadAccessSettings_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsFileReadAccessSettingsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionsMandatoryValidationTypes

    /// <summary>
    ///     ID: {ea16e82d-f10a-4897-90f6-a9caf61ce9cc}
    ///     Alias: KrPermissionsMandatoryValidationTypes
    ///     Caption: $Views_Names_KrPermissionsMandatoryValidationTypes
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionsMandatoryValidationTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionsMandatoryValidationTypes": {ea16e82d-f10a-4897-90f6-a9caf61ce9cc}.
        /// </summary>
        public readonly Guid ID = new Guid(0xea16e82d,0xf10a,0x4897,0x90,0xf6,0xa9,0xca,0xf6,0x1c,0xe9,0xcc);

        /// <summary>
        ///     View name for "KrPermissionsMandatoryValidationTypes".
        /// </summary>
        public readonly string Alias = "KrPermissionsMandatoryValidationTypes";

        /// <summary>
        ///     View caption for "KrPermissionsMandatoryValidationTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionsMandatoryValidationTypes";

        /// <summary>
        ///     View group for "KrPermissionsMandatoryValidationTypes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: MandatoryValidationTypeID.
        /// </summary>
        public readonly ViewObject ColumnMandatoryValidationTypeID = new ViewObject(0, "MandatoryValidationTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: MandatoryValidationTypeName
        ///     Caption: $Views_KrPermissionsMandatoryValidationTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnMandatoryValidationTypeName = new ViewObject(1, "MandatoryValidationTypeName", "$Views_KrPermissionsMandatoryValidationTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_KrPermissionsMandatoryValidationTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_KrPermissionsMandatoryValidationTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsMandatoryValidationTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionsReport

    /// <summary>
    ///     ID: {cb1362ac-3a78-4afc-baec-c585e570955a}
    ///     Alias: KrPermissionsReport
    ///     Caption: $Views_Names_KrPermissionsReport
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionsReportViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionsReport": {cb1362ac-3a78-4afc-baec-c585e570955a}.
        /// </summary>
        public readonly Guid ID = new Guid(0xcb1362ac,0x3a78,0x4afc,0xba,0xec,0xc5,0x85,0xe5,0x70,0x95,0x5a);

        /// <summary>
        ///     View name for "KrPermissionsReport".
        /// </summary>
        public readonly string Alias = "KrPermissionsReport";

        /// <summary>
        ///     View caption for "KrPermissionsReport".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionsReport";

        /// <summary>
        ///     View group for "KrPermissionsReport".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrPermissionsID.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsID = new ViewObject(0, "KrPermissionsID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrPermissionsCaption
        ///     Caption: $Views_KrPermissions_Name.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsCaption = new ViewObject(1, "KrPermissionsCaption", "$Views_KrPermissions_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: KrPermissionsPriority
        ///     Caption: $Views_KrPermissions_Priority.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsPriority = new ViewObject(2, "KrPermissionsPriority", "$Views_KrPermissions_Priority");

        /// <summary>
        ///     ID:3 
        ///     Alias: KrPermissionsTypes
        ///     Caption: $Views_KrPermissions_Types.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsTypes = new ViewObject(3, "KrPermissionsTypes", "$Views_KrPermissions_Types");

        /// <summary>
        ///     ID:4 
        ///     Alias: KrPermissionsStates
        ///     Caption: $Views_KrPermissions_States.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsStates = new ViewObject(4, "KrPermissionsStates", "$Views_KrPermissions_States");

        /// <summary>
        ///     ID:5 
        ///     Alias: KrPermissionsRoles
        ///     Caption: $Views_KrPermissions_Roles.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsRoles = new ViewObject(5, "KrPermissionsRoles", "$Views_KrPermissions_Roles");

        /// <summary>
        ///     ID:6 
        ///     Alias: KrPermissionsAclGenerationRules
        ///     Caption: $Views_KrPermissions_AclGenerationRules.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsAclGenerationRules = new ViewObject(6, "KrPermissionsAclGenerationRules", "$Views_KrPermissions_AclGenerationRules");

        /// <summary>
        ///     ID:7 
        ///     Alias: KrPermissionsPermissions
        ///     Caption: $Views_KrPermissions_Permissions.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionsPermissions = new ViewObject(7, "KrPermissionsPermissions", "$Views_KrPermissions_Permissions");

        /// <summary>
        ///     ID:8 
        ///     Alias: Background.
        /// </summary>
        public readonly ViewObject ColumnBackground = new ViewObject(8, "Background");

        /// <summary>
        ///     ID:9 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(9, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_KrPermissions_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_KrPermissions_Caption_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Description
        ///     Caption: $Views_KrPermissions_Description_Param.
        /// </summary>
        public readonly ViewObject ParamDescription = new ViewObject(1, "Description", "$Views_KrPermissions_Description_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Type
        ///     Caption: $Views_KrPermissions_Type_Param.
        /// </summary>
        public readonly ViewObject ParamType = new ViewObject(2, "Type", "$Views_KrPermissions_Type_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: State
        ///     Caption: $Views_KrPermissions_State_Param.
        /// </summary>
        public readonly ViewObject ParamState = new ViewObject(3, "State", "$Views_KrPermissions_State_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Role
        ///     Caption: $Views_KrPermissions_Role_Param.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(4, "Role", "$Views_KrPermissions_Role_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: AclGenerationRule
        ///     Caption: $Views_KrPermissions_AclGenerationRule_Param.
        /// </summary>
        public readonly ViewObject ParamAclGenerationRule = new ViewObject(5, "AclGenerationRule", "$Views_KrPermissions_AclGenerationRule_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Permission
        ///     Caption: $Views_KrPermissions_Permission_Param.
        /// </summary>
        public readonly ViewObject ParamPermission = new ViewObject(6, "Permission", "$Views_KrPermissions_Permission_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: User
        ///     Caption: $Views_KrPermissions_User_Param.
        /// </summary>
        public readonly ViewObject ParamUser = new ViewObject(7, "User", "$Views_KrPermissions_User_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: Priority
        ///     Caption: $Views_KrPermissions_Priority_Param.
        /// </summary>
        public readonly ViewObject ParamPriority = new ViewObject(8, "Priority", "$Views_KrPermissions_Priority_Param");

        /// <summary>
        ///     ID:9 
        ///     Alias: IsDisabled
        ///     Caption: $Views_KrPermissions_IsDisabled_Param.
        /// </summary>
        public readonly ViewObject ParamIsDisabled = new ViewObject(9, "IsDisabled", "$Views_KrPermissions_IsDisabled_Param");

        /// <summary>
        ///     ID:10 
        ///     Alias: IsRequired
        ///     Caption: $Views_KrPermissions_AlwaysCheck_Param.
        /// </summary>
        public readonly ViewObject ParamIsRequired = new ViewObject(10, "IsRequired", "$Views_KrPermissions_AlwaysCheck_Param");

        /// <summary>
        ///     ID:11 
        ///     Alias: IsExtended
        ///     Caption: $Views_KrPermissions_UseExtendedSettings_Param.
        /// </summary>
        public readonly ViewObject ParamIsExtended = new ViewObject(11, "IsExtended", "$Views_KrPermissions_UseExtendedSettings_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionsReportViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionStates

    /// <summary>
    ///     ID: {44026b2a-6699-425c-8669-5ad5c75945f9}
    ///     Alias: KrPermissionStates
    ///     Caption: $Views_Names_KrPermissionStates
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionStatesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionStates": {44026b2a-6699-425c-8669-5ad5c75945f9}.
        /// </summary>
        public readonly Guid ID = new Guid(0x44026b2a,0x6699,0x425c,0x86,0x69,0x5a,0xd5,0xc7,0x59,0x45,0xf9);

        /// <summary>
        ///     View name for "KrPermissionStates".
        /// </summary>
        public readonly string Alias = "KrPermissionStates";

        /// <summary>
        ///     View caption for "KrPermissionStates".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionStates";

        /// <summary>
        ///     View group for "KrPermissionStates".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrPermissionStateID.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionStateID = new ViewObject(0, "KrPermissionStateID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrPermissionStateName
        ///     Caption: $Views_KrPermissions_States.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionStateName = new ViewObject(1, "KrPermissionStateName", "$Views_KrPermissions_States");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: AccessRule
        ///     Caption: Access rule.
        /// </summary>
        public readonly ViewObject ParamAccessRule = new ViewObject(0, "AccessRule", "Access rule");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionStatesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrPermissionTypes

    /// <summary>
    ///     ID: {54026b2a-6699-425c-8669-5ad5c75945f9}
    ///     Alias: KrPermissionTypes
    ///     Caption: $Views_Names_KrPermissionTypes
    ///     Group: Kr Wf
    /// </summary>
    public class KrPermissionTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrPermissionTypes": {54026b2a-6699-425c-8669-5ad5c75945f9}.
        /// </summary>
        public readonly Guid ID = new Guid(0x54026b2a,0x6699,0x425c,0x86,0x69,0x5a,0xd5,0xc7,0x59,0x45,0xf9);

        /// <summary>
        ///     View name for "KrPermissionTypes".
        /// </summary>
        public readonly string Alias = "KrPermissionTypes";

        /// <summary>
        ///     View caption for "KrPermissionTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrPermissionTypes";

        /// <summary>
        ///     View group for "KrPermissionTypes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrPermissionTypeID.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionTypeID = new ViewObject(0, "KrPermissionTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrPermissionTypeCaption
        ///     Caption: $Views_KrPermissions_Types.
        /// </summary>
        public readonly ViewObject ColumnKrPermissionTypeCaption = new ViewObject(1, "KrPermissionTypeCaption", "$Views_KrPermissions_Types");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: AccessRule
        ///     Caption: Access rule.
        /// </summary>
        public readonly ViewObject ParamAccessRule = new ViewObject(0, "AccessRule", "Access rule");

        #endregion

        #region ToString

        public static implicit operator string(KrPermissionTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrProcessManagementStageTypeModes

    /// <summary>
    ///     ID: {20036792-579c-4228-b2f9-79495f326f06}
    ///     Alias: KrProcessManagementStageTypeModes
    ///     Caption: $Views_Names_KrProcessManagementStageTypeModes
    ///     Group: Kr Wf
    /// </summary>
    public class KrProcessManagementStageTypeModesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrProcessManagementStageTypeModes": {20036792-579c-4228-b2f9-79495f326f06}.
        /// </summary>
        public readonly Guid ID = new Guid(0x20036792,0x579c,0x4228,0xb2,0xf9,0x79,0x49,0x5f,0x32,0x6f,0x06);

        /// <summary>
        ///     View name for "KrProcessManagementStageTypeModes".
        /// </summary>
        public readonly string Alias = "KrProcessManagementStageTypeModes";

        /// <summary>
        ///     View caption for "KrProcessManagementStageTypeModes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrProcessManagementStageTypeModes";

        /// <summary>
        ///     View group for "KrProcessManagementStageTypeModes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: RefName.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "RefName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "Name");

        #endregion

        #region ToString

        public static implicit operator string(KrProcessManagementStageTypeModesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrRouteModes

    /// <summary>
    ///     ID: {3179625f-bf5e-478a-ba44-07fd2babede7}
    ///     Alias: KrRouteModes
    ///     Caption: $Views_Names_KrRouteModes
    ///     Group: Kr Wf
    /// </summary>
    public class KrRouteModesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrRouteModes": {3179625f-bf5e-478a-ba44-07fd2babede7}.
        /// </summary>
        public readonly Guid ID = new Guid(0x3179625f,0xbf5e,0x478a,0xba,0x44,0x07,0xfd,0x2b,0xab,0xed,0xe7);

        /// <summary>
        ///     View name for "KrRouteModes".
        /// </summary>
        public readonly string Alias = "KrRouteModes";

        /// <summary>
        ///     View caption for "KrRouteModes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrRouteModes";

        /// <summary>
        ///     View group for "KrRouteModes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ModeID.
        /// </summary>
        public readonly ViewObject ColumnModeID = new ViewObject(0, "ModeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ModeName
        ///     Caption: $Views_KrRouteModes_Name.
        /// </summary>
        public readonly ViewObject ColumnModeName = new ViewObject(1, "ModeName", "$Views_KrRouteModes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: ModeNameParam
        ///     Caption: $Views_KrRouteModes_Name.
        /// </summary>
        public readonly ViewObject ParamModeNameParam = new ViewObject(0, "ModeNameParam", "$Views_KrRouteModes_Name");

        #endregion

        #region ToString

        public static implicit operator string(KrRouteModesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrSecondaryProcesses

    /// <summary>
    ///     ID: {e824a33b-2194-4713-a42f-89ac225e0141}
    ///     Alias: KrSecondaryProcesses
    ///     Caption: $Views_Names_KrSecondaryProcesses
    ///     Group: Kr Wf
    /// </summary>
    public class KrSecondaryProcessesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrSecondaryProcesses": {e824a33b-2194-4713-a42f-89ac225e0141}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe824a33b,0x2194,0x4713,0xa4,0x2f,0x89,0xac,0x22,0x5e,0x01,0x41);

        /// <summary>
        ///     View name for "KrSecondaryProcesses".
        /// </summary>
        public readonly string Alias = "KrSecondaryProcesses";

        /// <summary>
        ///     View caption for "KrSecondaryProcesses".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrSecondaryProcesses";

        /// <summary>
        ///     View group for "KrSecondaryProcesses".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SecondaryProcessID.
        /// </summary>
        public readonly ViewObject ColumnSecondaryProcessID = new ViewObject(0, "SecondaryProcessID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SecondaryProcessName
        ///     Caption: $Views_KrSecondaryProcesses_Name.
        /// </summary>
        public readonly ViewObject ColumnSecondaryProcessName = new ViewObject(1, "SecondaryProcessName", "$Views_KrSecondaryProcesses_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: Description
        ///     Caption: $Views_KrSecondaryProcesses_Description.
        /// </summary>
        public readonly ViewObject ColumnDescription = new ViewObject(2, "Description", "$Views_KrSecondaryProcesses_Description");

        /// <summary>
        ///     ID:3 
        ///     Alias: IsGlobal
        ///     Caption: $Views_KrSecondaryProcesses_IsGlobal.
        /// </summary>
        public readonly ViewObject ColumnIsGlobal = new ViewObject(3, "IsGlobal", "$Views_KrSecondaryProcesses_IsGlobal");

        /// <summary>
        ///     ID:4 
        ///     Alias: Async
        ///     Caption: $Views_KrSecondaryProcesses_Async.
        /// </summary>
        public readonly ViewObject ColumnAsync = new ViewObject(4, "Async", "$Views_KrSecondaryProcesses_Async");

        /// <summary>
        ///     ID:5 
        ///     Alias: Types
        ///     Caption: $Views_KrSecondaryProcesses_Types.
        /// </summary>
        public readonly ViewObject ColumnTypes = new ViewObject(5, "Types", "$Views_KrSecondaryProcesses_Types");

        /// <summary>
        ///     ID:6 
        ///     Alias: Roles
        ///     Caption: $Views_KrSecondaryProcesses_Roles.
        /// </summary>
        public readonly ViewObject ColumnRoles = new ViewObject(6, "Roles", "$Views_KrSecondaryProcesses_Roles");

        /// <summary>
        ///     ID:7 
        ///     Alias: CardStates
        ///     Caption: $Views_KrSecondaryProcesses_States.
        /// </summary>
        public readonly ViewObject ColumnCardStates = new ViewObject(7, "CardStates", "$Views_KrSecondaryProcesses_States");

        /// <summary>
        ///     ID:8 
        ///     Alias: AvailableRoles
        ///     Caption: $Views_KrSecondaryProcesses_AvailableRoles.
        /// </summary>
        public readonly ViewObject ColumnAvailableRoles = new ViewObject(8, "AvailableRoles", "$Views_KrSecondaryProcesses_AvailableRoles");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: SecondaryProcessIDParam
        ///     Caption: SecondaryProcessIDParam.
        /// </summary>
        public readonly ViewObject ParamSecondaryProcessIDParam = new ViewObject(0, "SecondaryProcessIDParam", "SecondaryProcessIDParam");

        /// <summary>
        ///     ID:1 
        ///     Alias: SecondaryProcessNameParam
        ///     Caption: $Views_KrSecondaryProcesses_Name.
        /// </summary>
        public readonly ViewObject ParamSecondaryProcessNameParam = new ViewObject(1, "SecondaryProcessNameParam", "$Views_KrSecondaryProcesses_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: IsGlobalParam
        ///     Caption: $Views_KrSecondaryProcesses_IsGlobal.
        /// </summary>
        public readonly ViewObject ParamIsGlobalParam = new ViewObject(2, "IsGlobalParam", "$Views_KrSecondaryProcesses_IsGlobal");

        /// <summary>
        ///     ID:3 
        ///     Alias: AsyncParam
        ///     Caption: $Views_KrSecondaryProcesses_Async.
        /// </summary>
        public readonly ViewObject ParamAsyncParam = new ViewObject(3, "AsyncParam", "$Views_KrSecondaryProcesses_Async");

        /// <summary>
        ///     ID:4 
        ///     Alias: TypeParam
        ///     Caption: $Views_KrSecondaryProcesses_Types.
        /// </summary>
        public readonly ViewObject ParamTypeParam = new ViewObject(4, "TypeParam", "$Views_KrSecondaryProcesses_Types");

        /// <summary>
        ///     ID:5 
        ///     Alias: RoleParam
        ///     Caption: $Views_KrSecondaryProcesses_Roles.
        /// </summary>
        public readonly ViewObject ParamRoleParam = new ViewObject(5, "RoleParam", "$Views_KrSecondaryProcesses_Roles");

        /// <summary>
        ///     ID:6 
        ///     Alias: StateParam
        ///     Caption: $Views_KrSecondaryProcesses_States.
        /// </summary>
        public readonly ViewObject ParamStateParam = new ViewObject(6, "StateParam", "$Views_KrSecondaryProcesses_States");

        #endregion

        #region ToString

        public static implicit operator string(KrSecondaryProcessesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrSecondaryProcessModes

    /// <summary>
    ///     ID: {4f6f0744-5a4e-4285-b39e-064c56737715}
    ///     Alias: KrSecondaryProcessModes
    ///     Caption: $Views_Names_KrSecondaryProcessModes
    ///     Group: Kr Wf
    /// </summary>
    public class KrSecondaryProcessModesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrSecondaryProcessModes": {4f6f0744-5a4e-4285-b39e-064c56737715}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4f6f0744,0x5a4e,0x4285,0xb3,0x9e,0x06,0x4c,0x56,0x73,0x77,0x15);

        /// <summary>
        ///     View name for "KrSecondaryProcessModes".
        /// </summary>
        public readonly string Alias = "KrSecondaryProcessModes";

        /// <summary>
        ///     View caption for "KrSecondaryProcessModes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrSecondaryProcessModes";

        /// <summary>
        ///     View group for "KrSecondaryProcessModes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: RefName.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "RefName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "Name");

        #endregion

        #region ToString

        public static implicit operator string(KrSecondaryProcessModesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrStageCommonMethods

    /// <summary>
    ///     ID: {3b31f77a-1667-443c-b4f0-9bdb04798c72}
    ///     Alias: KrStageCommonMethods
    ///     Caption: $Views_Names_KrStageCommonMethods
    ///     Group: Kr Wf
    /// </summary>
    public class KrStageCommonMethodsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrStageCommonMethods": {3b31f77a-1667-443c-b4f0-9bdb04798c72}.
        /// </summary>
        public readonly Guid ID = new Guid(0x3b31f77a,0x1667,0x443c,0xb4,0xf0,0x9b,0xdb,0x04,0x79,0x8c,0x72);

        /// <summary>
        ///     View name for "KrStageCommonMethods".
        /// </summary>
        public readonly string Alias = "KrStageCommonMethods";

        /// <summary>
        ///     View caption for "KrStageCommonMethods".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrStageCommonMethods";

        /// <summary>
        ///     View group for "KrStageCommonMethods".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: MethodID.
        /// </summary>
        public readonly ViewObject ColumnMethodID = new ViewObject(0, "MethodID");

        /// <summary>
        ///     ID:1 
        ///     Alias: MethodName
        ///     Caption: $Views_KrStageCommonMethod_MethodName.
        /// </summary>
        public readonly ViewObject ColumnMethodName = new ViewObject(1, "MethodName", "$Views_KrStageCommonMethod_MethodName");

        /// <summary>
        ///     ID:2 
        ///     Alias: Source
        ///     Caption: $Views_KrStageCommonMethod_Source.
        /// </summary>
        public readonly ViewObject ColumnSource = new ViewObject(2, "Source", "$Views_KrStageCommonMethod_Source");

        /// <summary>
        ///     ID:3 
        ///     Alias: Description
        ///     Caption: $Views_KrStageCommonMethod_MethodDescription.
        /// </summary>
        public readonly ViewObject ColumnDescription = new ViewObject(3, "Description", "$Views_KrStageCommonMethod_MethodDescription");

        /// <summary>
        ///     ID:4 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(4, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: MethodIDParam
        ///     Caption: MethodIDParam.
        /// </summary>
        public readonly ViewObject ParamMethodIDParam = new ViewObject(0, "MethodIDParam", "MethodIDParam");

        /// <summary>
        ///     ID:1 
        ///     Alias: MethodNameParam
        ///     Caption: $Views_KrStageCommonMethod_MethodName.
        /// </summary>
        public readonly ViewObject ParamMethodNameParam = new ViewObject(1, "MethodNameParam", "$Views_KrStageCommonMethod_MethodName");

        /// <summary>
        ///     ID:2 
        ///     Alias: MethodDescriptionParam
        ///     Caption: $Views_KrStageCommonMethod_MethodDescription.
        /// </summary>
        public readonly ViewObject ParamMethodDescriptionParam = new ViewObject(2, "MethodDescriptionParam", "$Views_KrStageCommonMethod_MethodDescription");

        #endregion

        #region ToString

        public static implicit operator string(KrStageCommonMethodsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrStageGroups

    /// <summary>
    ///     ID: {6492b1f7-0fa4-4910-9911-67b2eb1614d7}
    ///     Alias: KrStageGroups
    ///     Caption: $Views_Names_KrStageGroups
    ///     Group: Kr Wf
    /// </summary>
    public class KrStageGroupsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrStageGroups": {6492b1f7-0fa4-4910-9911-67b2eb1614d7}.
        /// </summary>
        public readonly Guid ID = new Guid(0x6492b1f7,0x0fa4,0x4910,0x99,0x11,0x67,0xb2,0xeb,0x16,0x14,0xd7);

        /// <summary>
        ///     View name for "KrStageGroups".
        /// </summary>
        public readonly string Alias = "KrStageGroups";

        /// <summary>
        ///     View caption for "KrStageGroups".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrStageGroups";

        /// <summary>
        ///     View group for "KrStageGroups".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: StageGroupID.
        /// </summary>
        public readonly ViewObject ColumnStageGroupID = new ViewObject(0, "StageGroupID");

        /// <summary>
        ///     ID:1 
        ///     Alias: StageGroupName
        ///     Caption: $Views_KrStageGroups_Name.
        /// </summary>
        public readonly ViewObject ColumnStageGroupName = new ViewObject(1, "StageGroupName", "$Views_KrStageGroups_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: Description
        ///     Caption: $Views_KrStageGroups_Description.
        /// </summary>
        public readonly ViewObject ColumnDescription = new ViewObject(2, "Description", "$Views_KrStageGroups_Description");

        /// <summary>
        ///     ID:3 
        ///     Alias: IsGroupReadonly
        ///     Caption: $Views_KrStageGroups_IsGroupReadonly.
        /// </summary>
        public readonly ViewObject ColumnIsGroupReadonly = new ViewObject(3, "IsGroupReadonly", "$Views_KrStageGroups_IsGroupReadonly");

        /// <summary>
        ///     ID:4 
        ///     Alias: Order
        ///     Caption: $Views_KrStageGroups_Order.
        /// </summary>
        public readonly ViewObject ColumnOrder = new ViewObject(4, "Order", "$Views_KrStageGroups_Order");

        /// <summary>
        ///     ID:5 
        ///     Alias: Types
        ///     Caption: $Views_KrStageGroups_Types.
        /// </summary>
        public readonly ViewObject ColumnTypes = new ViewObject(5, "Types", "$Views_KrStageGroups_Types");

        /// <summary>
        ///     ID:6 
        ///     Alias: Roles
        ///     Caption: $Views_KrStageGroups_Roles.
        /// </summary>
        public readonly ViewObject ColumnRoles = new ViewObject(6, "Roles", "$Views_KrStageGroups_Roles");

        /// <summary>
        ///     ID:7 
        ///     Alias: SecondaryProcessName
        ///     Caption: $Views_KrStageGroups_SecondaryProcessCaption.
        /// </summary>
        public readonly ViewObject ColumnSecondaryProcessName = new ViewObject(7, "SecondaryProcessName", "$Views_KrStageGroups_SecondaryProcessCaption");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: StageGroupIDParam
        ///     Caption: StageGroupIDParam.
        /// </summary>
        public readonly ViewObject ParamStageGroupIDParam = new ViewObject(0, "StageGroupIDParam", "StageGroupIDParam");

        /// <summary>
        ///     ID:1 
        ///     Alias: StageGroupNameParam
        ///     Caption: $Views_KrStageGroups_Name.
        /// </summary>
        public readonly ViewObject ParamStageGroupNameParam = new ViewObject(1, "StageGroupNameParam", "$Views_KrStageGroups_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: StageGroupDescriptionParam
        ///     Caption: $Views_KrStageGroups_Description.
        /// </summary>
        public readonly ViewObject ParamStageGroupDescriptionParam = new ViewObject(2, "StageGroupDescriptionParam", "$Views_KrStageGroups_Description");

        /// <summary>
        ///     ID:3 
        ///     Alias: IsGroupReadonlyParam
        ///     Caption: $Views_KrStageGroups_IsGroupReadonly.
        /// </summary>
        public readonly ViewObject ParamIsGroupReadonlyParam = new ViewObject(3, "IsGroupReadonlyParam", "$Views_KrStageGroups_IsGroupReadonly");

        /// <summary>
        ///     ID:4 
        ///     Alias: TypeParam
        ///     Caption: $Views_KrStageGroups_Types.
        /// </summary>
        public readonly ViewObject ParamTypeParam = new ViewObject(4, "TypeParam", "$Views_KrStageGroups_Types");

        /// <summary>
        ///     ID:5 
        ///     Alias: RoleParam
        ///     Caption: $Views_KrStageGroups_Roles.
        /// </summary>
        public readonly ViewObject ParamRoleParam = new ViewObject(5, "RoleParam", "$Views_KrStageGroups_Roles");

        /// <summary>
        ///     ID:6 
        ///     Alias: StartupParam
        ///     Caption: $Views_KrStageTemplates_ByStartupTypeSubset.
        /// </summary>
        public readonly ViewObject ParamStartupParam = new ViewObject(6, "StartupParam", "$Views_KrStageTemplates_ByStartupTypeSubset");

        #endregion

        #region ToString

        public static implicit operator string(KrStageGroupsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrStageRows

    /// <summary>
    ///     ID: {74f0ec7a-2cb4-4bb9-9eca-942e28374ea9}
    ///     Alias: KrStageRows
    ///     Caption: $Views_Names_KrStageRows
    ///     Group: Kr Wf
    /// </summary>
    public class KrStageRowsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrStageRows": {74f0ec7a-2cb4-4bb9-9eca-942e28374ea9}.
        /// </summary>
        public readonly Guid ID = new Guid(0x74f0ec7a,0x2cb4,0x4bb9,0x9e,0xca,0x94,0x2e,0x28,0x37,0x4e,0xa9);

        /// <summary>
        ///     View name for "KrStageRows".
        /// </summary>
        public readonly string Alias = "KrStageRows";

        /// <summary>
        ///     View caption for "KrStageRows".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrStageRows";

        /// <summary>
        ///     View group for "KrStageRows".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: StageRowID.
        /// </summary>
        public readonly ViewObject ColumnStageRowID = new ViewObject(0, "StageRowID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeCaption
        ///     Caption: $Views_KrStageRows_TypeCaption.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(1, "TypeCaption", "$Views_KrStageRows_TypeCaption");

        /// <summary>
        ///     ID:2 
        ///     Alias: StageName
        ///     Caption: $Views_KrStageRows_Name.
        /// </summary>
        public readonly ViewObject ColumnStageName = new ViewObject(2, "StageName", "$Views_KrStageRows_Name");

        /// <summary>
        ///     ID:3 
        ///     Alias: TimeLimit
        ///     Caption: $Views_KrStageRows_TimeLimit.
        /// </summary>
        public readonly ViewObject ColumnTimeLimit = new ViewObject(3, "TimeLimit", "$Views_KrStageRows_TimeLimit");

        /// <summary>
        ///     ID:4 
        ///     Alias: Participants
        ///     Caption: $Views_KrStageRows_Participants.
        /// </summary>
        public readonly ViewObject ColumnParticipants = new ViewObject(4, "Participants", "$Views_KrStageRows_Participants");

        /// <summary>
        ///     ID:5 
        ///     Alias: Settings
        ///     Caption: $Views_KrStageRows_Settings.
        /// </summary>
        public readonly ViewObject ColumnSettings = new ViewObject(5, "Settings", "$Views_KrStageRows_Settings");

        /// <summary>
        ///     ID:6 
        ///     Alias: Order.
        /// </summary>
        public readonly ViewObject ColumnOrder = new ViewObject(6, "Order");

        /// <summary>
        ///     ID:7 
        ///     Alias: StageRowGroupID
        ///     Caption: StageGroupID.
        /// </summary>
        public readonly ViewObject ColumnStageRowGroupID = new ViewObject(7, "StageRowGroupID", "StageGroupID");

        /// <summary>
        ///     ID:8 
        ///     Alias: StageRowGroupName
        ///     Caption: StageGroupName.
        /// </summary>
        public readonly ViewObject ColumnStageRowGroupName = new ViewObject(8, "StageRowGroupName", "StageGroupName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: StageTemplateIDParam
        ///     Caption: StageTemplateIDParam.
        /// </summary>
        public readonly ViewObject ParamStageTemplateIDParam = new ViewObject(0, "StageTemplateIDParam", "StageTemplateIDParam");

        /// <summary>
        ///     ID:1 
        ///     Alias: StageNameParam
        ///     Caption: $Views_KrStageRows_Name.
        /// </summary>
        public readonly ViewObject ParamStageNameParam = new ViewObject(1, "StageNameParam", "$Views_KrStageRows_Name");

        #endregion

        #region ToString

        public static implicit operator string(KrStageRowsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrStageTemplateGroupPosition

    /// <summary>
    ///     ID: {c4092348-06c2-452d-984e-18638961365b}
    ///     Alias: KrStageTemplateGroupPosition
    ///     Caption: $Views_Names_KrStageTemplateGroupPosition
    ///     Group: Kr Wf
    /// </summary>
    public class KrStageTemplateGroupPositionViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrStageTemplateGroupPosition": {c4092348-06c2-452d-984e-18638961365b}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc4092348,0x06c2,0x452d,0x98,0x4e,0x18,0x63,0x89,0x61,0x36,0x5b);

        /// <summary>
        ///     View name for "KrStageTemplateGroupPosition".
        /// </summary>
        public readonly string Alias = "KrStageTemplateGroupPosition";

        /// <summary>
        ///     View caption for "KrStageTemplateGroupPosition".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrStageTemplateGroupPosition";

        /// <summary>
        ///     View group for "KrStageTemplateGroupPosition".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: GroupID.
        /// </summary>
        public readonly ViewObject ColumnGroupID = new ViewObject(0, "GroupID");

        /// <summary>
        ///     ID:1 
        ///     Alias: GroupName
        ///     Caption: $Views_KrStageTemplateGroupPosition_GroupPositionName.
        /// </summary>
        public readonly ViewObject ColumnGroupName = new ViewObject(1, "GroupName", "$Views_KrStageTemplateGroupPosition_GroupPositionName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: GroupNameParam
        ///     Caption: $Views_KrStageTemplateGroupPosition_GroupPositionName.
        /// </summary>
        public readonly ViewObject ParamGroupNameParam = new ViewObject(0, "GroupNameParam", "$Views_KrStageTemplateGroupPosition_GroupPositionName");

        #endregion

        #region ToString

        public static implicit operator string(KrStageTemplateGroupPositionViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrStageTemplates

    /// <summary>
    ///     ID: {2163b711-2672-443f-9af1-ede4af0e9e89}
    ///     Alias: KrStageTemplates
    ///     Caption: $Views_Names_KrStageTemplates
    ///     Group: Kr Wf
    /// </summary>
    public class KrStageTemplatesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrStageTemplates": {2163b711-2672-443f-9af1-ede4af0e9e89}.
        /// </summary>
        public readonly Guid ID = new Guid(0x2163b711,0x2672,0x443f,0x9a,0xf1,0xed,0xe4,0xaf,0x0e,0x9e,0x89);

        /// <summary>
        ///     View name for "KrStageTemplates".
        /// </summary>
        public readonly string Alias = "KrStageTemplates";

        /// <summary>
        ///     View caption for "KrStageTemplates".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrStageTemplates";

        /// <summary>
        ///     View group for "KrStageTemplates".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: StageTemplateID.
        /// </summary>
        public readonly ViewObject ColumnStageTemplateID = new ViewObject(0, "StageTemplateID");

        /// <summary>
        ///     ID:1 
        ///     Alias: StageTemplateName
        ///     Caption: $Views_KrStageTemplates_StageTemplateName.
        /// </summary>
        public readonly ViewObject ColumnStageTemplateName = new ViewObject(1, "StageTemplateName", "$Views_KrStageTemplates_StageTemplateName");

        /// <summary>
        ///     ID:2 
        ///     Alias: Description
        ///     Caption: $Views_KrStageTemplates_StageTemplateDescription.
        /// </summary>
        public readonly ViewObject ColumnDescription = new ViewObject(2, "Description", "$Views_KrStageTemplates_StageTemplateDescription");

        /// <summary>
        ///     ID:3 
        ///     Alias: CanChangeOrder
        ///     Caption: $Views_KrStageTemplates_CanChangeOrder.
        /// </summary>
        public readonly ViewObject ColumnCanChangeOrder = new ViewObject(3, "CanChangeOrder", "$Views_KrStageTemplates_CanChangeOrder");

        /// <summary>
        ///     ID:4 
        ///     Alias: Order
        ///     Caption: $Views_KrStageTemplates_Order.
        /// </summary>
        public readonly ViewObject ColumnOrder = new ViewObject(4, "Order", "$Views_KrStageTemplates_Order");

        /// <summary>
        ///     ID:5 
        ///     Alias: Types
        ///     Caption: $Views_KrStageTemplates_Types.
        /// </summary>
        public readonly ViewObject ColumnTypes = new ViewObject(5, "Types", "$Views_KrStageTemplates_Types");

        /// <summary>
        ///     ID:6 
        ///     Alias: Roles
        ///     Caption: $Views_KrStageTemplates_Roles.
        /// </summary>
        public readonly ViewObject ColumnRoles = new ViewObject(6, "Roles", "$Views_KrStageTemplates_Roles");

        /// <summary>
        ///     ID:7 
        ///     Alias: StageGroupName
        ///     Caption: $Views_KrStageTemplates_StageGroup.
        /// </summary>
        public readonly ViewObject ColumnStageGroupName = new ViewObject(7, "StageGroupName", "$Views_KrStageTemplates_StageGroup");

        /// <summary>
        ///     ID:8 
        ///     Alias: SecondaryProcessName
        ///     Caption: $Views_KrStageTemplates_SecondaryProcessCaption.
        /// </summary>
        public readonly ViewObject ColumnSecondaryProcessName = new ViewObject(8, "SecondaryProcessName", "$Views_KrStageTemplates_SecondaryProcessCaption");

        /// <summary>
        ///     ID:9 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(9, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: StageTemplateIDParam
        ///     Caption: StageTemplateID.
        /// </summary>
        public readonly ViewObject ParamStageTemplateIDParam = new ViewObject(0, "StageTemplateIDParam", "StageTemplateID");

        /// <summary>
        ///     ID:1 
        ///     Alias: StageTemplateNameParam
        ///     Caption: $Views_KrStageTemplates_StageTemplateName.
        /// </summary>
        public readonly ViewObject ParamStageTemplateNameParam = new ViewObject(1, "StageTemplateNameParam", "$Views_KrStageTemplates_StageTemplateName");

        /// <summary>
        ///     ID:2 
        ///     Alias: StageTemplateDescriptionParam
        ///     Caption: $Views_KrStageTemplates_StageTemplateDescription.
        /// </summary>
        public readonly ViewObject ParamStageTemplateDescriptionParam = new ViewObject(2, "StageTemplateDescriptionParam", "$Views_KrStageTemplates_StageTemplateDescription");

        /// <summary>
        ///     ID:3 
        ///     Alias: GroupPositionIDParam
        ///     Caption: $Views_KrStageTemplates_GroupPositionName.
        /// </summary>
        public readonly ViewObject ParamGroupPositionIDParam = new ViewObject(3, "GroupPositionIDParam", "$Views_KrStageTemplates_GroupPositionName");

        /// <summary>
        ///     ID:4 
        ///     Alias: CanChangeOrderParam
        ///     Caption: $Views_KrStageTemplates_CanChangeOrder.
        /// </summary>
        public readonly ViewObject ParamCanChangeOrderParam = new ViewObject(4, "CanChangeOrderParam", "$Views_KrStageTemplates_CanChangeOrder");

        /// <summary>
        ///     ID:5 
        ///     Alias: TypeParam
        ///     Caption: $Views_KrStageTemplates_TypeParam.
        /// </summary>
        public readonly ViewObject ParamTypeParam = new ViewObject(5, "TypeParam", "$Views_KrStageTemplates_TypeParam");

        /// <summary>
        ///     ID:6 
        ///     Alias: RoleParam
        ///     Caption: $Views_KrStageTemplates_Roles.
        /// </summary>
        public readonly ViewObject ParamRoleParam = new ViewObject(6, "RoleParam", "$Views_KrStageTemplates_Roles");

        /// <summary>
        ///     ID:7 
        ///     Alias: StageGroupParam
        ///     Caption: $Views_KrStageTemplates_StageGroups.
        /// </summary>
        public readonly ViewObject ParamStageGroupParam = new ViewObject(7, "StageGroupParam", "$Views_KrStageTemplates_StageGroups");

        /// <summary>
        ///     ID:8 
        ///     Alias: StartupParam
        ///     Caption: $Views_KrStageTemplates_ByStartupTypeSubset.
        /// </summary>
        public readonly ViewObject ParamStartupParam = new ViewObject(8, "StartupParam", "$Views_KrStageTemplates_ByStartupTypeSubset");

        /// <summary>
        ///     ID:9 
        ///     Alias: AdvisoryParam
        ///     Caption: $Views_KrStageTemplates_AdvisoryParam.
        /// </summary>
        public readonly ViewObject ParamAdvisoryParam = new ViewObject(9, "AdvisoryParam", "$Views_KrStageTemplates_AdvisoryParam");

        #endregion

        #region ToString

        public static implicit operator string(KrStageTemplatesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrStageTypes

    /// <summary>
    ///     ID: {a7ea0334-626e-41d8-9ae3-80cf3c710daa}
    ///     Alias: KrStageTypes
    ///     Caption: $Views_Names_KrStageTypes
    ///     Group: Kr Wf
    /// </summary>
    public class KrStageTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrStageTypes": {a7ea0334-626e-41d8-9ae3-80cf3c710daa}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa7ea0334,0x626e,0x41d8,0x9a,0xe3,0x80,0xcf,0x3c,0x71,0x0d,0xaa);

        /// <summary>
        ///     View name for "KrStageTypes".
        /// </summary>
        public readonly string Alias = "KrStageTypes";

        /// <summary>
        ///     View caption for "KrStageTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrStageTypes";

        /// <summary>
        ///     View group for "KrStageTypes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: StageTypeID.
        /// </summary>
        public readonly ViewObject ColumnStageTypeID = new ViewObject(0, "StageTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: StageTypeCaption
        ///     Caption: $Views_KrProcessStageTypes_Caption.
        /// </summary>
        public readonly ViewObject ColumnStageTypeCaption = new ViewObject(1, "StageTypeCaption", "$Views_KrProcessStageTypes_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: StageTypeDefaultStageName
        ///     Caption: $Views_KrProcessStageTypes_DefaultStageName.
        /// </summary>
        public readonly ViewObject ColumnStageTypeDefaultStageName = new ViewObject(2, "StageTypeDefaultStageName", "$Views_KrProcessStageTypes_DefaultStageName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_KrProcessStageTypes_Name.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_KrProcessStageTypes_Name");

        #endregion

        #region ToString

        public static implicit operator string(KrStageTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrTypes

    /// <summary>
    ///     ID: {399837f5-1c6e-470e-ae4a-6d85362650c7}
    ///     Alias: KrTypes
    ///     Caption: $Views_Names_KrTypes
    ///     Group: Kr Wf
    /// </summary>
    public class KrTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrTypes": {399837f5-1c6e-470e-ae4a-6d85362650c7}.
        /// </summary>
        public readonly Guid ID = new Guid(0x399837f5,0x1c6e,0x470e,0xae,0x4a,0x6d,0x85,0x36,0x26,0x50,0xc7);

        /// <summary>
        ///     View name for "KrTypes".
        /// </summary>
        public readonly string Alias = "KrTypes";

        /// <summary>
        ///     View caption for "KrTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrTypes";

        /// <summary>
        ///     View group for "KrTypes".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DocTypeID.
        /// </summary>
        public readonly ViewObject ColumnDocTypeID = new ViewObject(1, "DocTypeID");

        /// <summary>
        ///     ID:2 
        ///     Alias: DocTypeCaption.
        /// </summary>
        public readonly ViewObject ColumnDocTypeCaption = new ViewObject(2, "DocTypeCaption");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeCaption
        ///     Caption: $Views_KrTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(3, "TypeCaption", "$Views_KrTypes_Name");

        /// <summary>
        ///     ID:4 
        ///     Alias: TypeIsDocType
        ///     Caption: $Views_KrTypes_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeIsDocType = new ViewObject(4, "TypeIsDocType", "$Views_KrTypes_Type");

        /// <summary>
        ///     ID:5 
        ///     Alias: Name
        ///     Caption: $Views_KrTypes_Alias.
        /// </summary>
        public readonly ViewObject ColumnName = new ViewObject(5, "Name", "$Views_KrTypes_Alias");

        /// <summary>
        ///     ID:6 
        ///     Alias: IsDocTypeCaption
        ///     Caption: $Views_KrTypes_Type.
        /// </summary>
        public readonly ViewObject ColumnIsDocTypeCaption = new ViewObject(6, "IsDocTypeCaption", "$Views_KrTypes_Type");

        /// <summary>
        ///     ID:7 
        ///     Alias: State
        ///     Caption: $Views_KrTypes_State.
        /// </summary>
        public readonly ViewObject ColumnState = new ViewObject(7, "State", "$Views_KrTypes_State");

        /// <summary>
        ///     ID:8 
        ///     Alias: ParentType
        ///     Caption: $Views_KrTypes_ParentType.
        /// </summary>
        public readonly ViewObject ColumnParentType = new ViewObject(8, "ParentType", "$Views_KrTypes_ParentType");

        /// <summary>
        ///     ID:9 
        ///     Alias: LocalizedTypeCaption.
        /// </summary>
        public readonly ViewObject ColumnLocalizedTypeCaption = new ViewObject(9, "LocalizedTypeCaption");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_KrTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_KrTypes_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_KrTypes_AliasOrCaption_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_KrTypes_AliasOrCaption_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeIsDocType
        ///     Caption: $Views_KrTypes_DocumentType_Param.
        /// </summary>
        public readonly ViewObject ParamTypeIsDocType = new ViewObject(2, "TypeIsDocType", "$Views_KrTypes_DocumentType_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrTypesEffective

    /// <summary>
    ///     ID: {fd177ebc-050e-4f1a-8a28-deba816a727d}
    ///     Alias: KrTypesEffective
    ///     Caption: $Views_Names_KrTypesEffective
    ///     Group: Kr Wf
    /// </summary>
    public class KrTypesEffectiveViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrTypesEffective": {fd177ebc-050e-4f1a-8a28-deba816a727d}.
        /// </summary>
        public readonly Guid ID = new Guid(0xfd177ebc,0x050e,0x4f1a,0x8a,0x28,0xde,0xba,0x81,0x6a,0x72,0x7d);

        /// <summary>
        ///     View name for "KrTypesEffective".
        /// </summary>
        public readonly string Alias = "KrTypesEffective";

        /// <summary>
        ///     View caption for "KrTypesEffective".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrTypesEffective";

        /// <summary>
        ///     View group for "KrTypesEffective".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeCaption
        ///     Caption: $Views_KrTypesEffective_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(1, "TypeCaption", "$Views_KrTypesEffective_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: LocalizedTypeCaption.
        /// </summary>
        public readonly ViewObject ColumnLocalizedTypeCaption = new ViewObject(2, "LocalizedTypeCaption");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeIsDocType.
        /// </summary>
        public readonly ViewObject ColumnTypeIsDocType = new ViewObject(3, "TypeIsDocType");

        /// <summary>
        ///     ID:4 
        ///     Alias: TypeName
        ///     Caption: $Views_KrTypesEffective_Alias.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(4, "TypeName", "$Views_KrTypesEffective_Alias");

        /// <summary>
        ///     ID:5 
        ///     Alias: IsDocTypeCaption
        ///     Caption: $Views_KrTypesEffective_Type.
        /// </summary>
        public readonly ViewObject ColumnIsDocTypeCaption = new ViewObject(5, "IsDocTypeCaption", "$Views_KrTypesEffective_Type");

        /// <summary>
        ///     ID:6 
        ///     Alias: ParentType
        ///     Caption: $Views_KrTypesEffective_ParentType.
        /// </summary>
        public readonly ViewObject ColumnParentType = new ViewObject(6, "ParentType", "$Views_KrTypesEffective_ParentType");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_KrTypesEffective_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_KrTypesEffective_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_KrTypesEffective_Alias_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_KrTypesEffective_Alias_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeIsDocType
        ///     Caption: $Views_KrTypesEffective_DocType_Param.
        /// </summary>
        public readonly ViewObject ParamTypeIsDocType = new ViewObject(2, "TypeIsDocType", "$Views_KrTypesEffective_DocType_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrTypesEffectiveViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrTypesForDialogs

    /// <summary>
    ///     ID: {2c0b6a4a-8759-43d1-b23c-0c64f365d343}
    ///     Alias: KrTypesForDialogs
    ///     Caption: $Views_Names_KrTypesForDialogs
    ///     Group: Kr Wf
    /// </summary>
    public class KrTypesForDialogsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrTypesForDialogs": {2c0b6a4a-8759-43d1-b23c-0c64f365d343}.
        /// </summary>
        public readonly Guid ID = new Guid(0x2c0b6a4a,0x8759,0x43d1,0xb2,0x3c,0x0c,0x64,0xf3,0x65,0xd3,0x43);

        /// <summary>
        ///     View name for "KrTypesForDialogs".
        /// </summary>
        public readonly string Alias = "KrTypesForDialogs";

        /// <summary>
        ///     View caption for "KrTypesForDialogs".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrTypesForDialogs";

        /// <summary>
        ///     View group for "KrTypesForDialogs".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeCaption
        ///     Caption: $Views_Types_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(1, "TypeCaption", "$Views_Types_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeName
        ///     Caption: $Views_Types_Alias.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(2, "TypeName", "$Views_Types_Alias");

        /// <summary>
        ///     ID:3 
        ///     Alias: IsDocTypeCaption
        ///     Caption: $Views_KrTypes_Type.
        /// </summary>
        public readonly ViewObject ColumnIsDocTypeCaption = new ViewObject(3, "IsDocTypeCaption", "$Views_KrTypes_Type");

        /// <summary>
        ///     ID:4 
        ///     Alias: State
        ///     Caption: $Views_KrTypes_State.
        /// </summary>
        public readonly ViewObject ColumnState = new ViewObject(4, "State", "$Views_KrTypes_State");

        /// <summary>
        ///     ID:5 
        ///     Alias: ParentType
        ///     Caption: $Views_KrTypes_ParentType.
        /// </summary>
        public readonly ViewObject ColumnParentType = new ViewObject(5, "ParentType", "$Views_KrTypes_ParentType");

        /// <summary>
        ///     ID:6 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(6, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_Types_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_Types_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_Types_Alias_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_Types_Alias_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: NameOrCaption
        ///     Caption: NameOrCaption.
        /// </summary>
        public readonly ViewObject ParamNameOrCaption = new ViewObject(2, "NameOrCaption", "NameOrCaption");

        #endregion

        #region ToString

        public static implicit operator string(KrTypesForDialogsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrTypesForPermissionsExtension

    /// <summary>
    ///     ID: {d2c9ecb8-0e7f-4f79-a76c-c2cc71b0d959}
    ///     Alias: KrTypesForPermissionsExtension
    ///     Caption: $Views_Names_KrTypesForPermissionsExtension
    ///     Group: Kr Wf
    /// </summary>
    public class KrTypesForPermissionsExtensionViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrTypesForPermissionsExtension": {d2c9ecb8-0e7f-4f79-a76c-c2cc71b0d959}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd2c9ecb8,0x0e7f,0x4f79,0xa7,0x6c,0xc2,0xcc,0x71,0xb0,0xd9,0x59);

        /// <summary>
        ///     View name for "KrTypesForPermissionsExtension".
        /// </summary>
        public readonly string Alias = "KrTypesForPermissionsExtension";

        /// <summary>
        ///     View caption for "KrTypesForPermissionsExtension".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrTypesForPermissionsExtension";

        /// <summary>
        ///     View group for "KrTypesForPermissionsExtension".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeCaption
        ///     Caption: $Views_KrTypes_Caption.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(1, "TypeCaption", "$Views_KrTypes_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeName
        ///     Caption: $Views_KrTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(2, "TypeName", "$Views_KrTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: NameOrCaption
        ///     Caption: $Views_KrTypes_NameOrCaption_Param.
        /// </summary>
        public readonly ViewObject ParamNameOrCaption = new ViewObject(0, "NameOrCaption", "$Views_KrTypes_NameOrCaption_Param");

        #endregion

        #region ToString

        public static implicit operator string(KrTypesForPermissionsExtensionViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region KrVirtualFiles

    /// <summary>
    ///     ID: {e2ca613f-9ad1-4dba-bdaa-feb0d96b9700}
    ///     Alias: KrVirtualFiles
    ///     Caption: $Views_Names_KrVirtualFiles
    ///     Group: Kr Wf
    /// </summary>
    public class KrVirtualFilesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "KrVirtualFiles": {e2ca613f-9ad1-4dba-bdaa-feb0d96b9700}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe2ca613f,0x9ad1,0x4dba,0xbd,0xaa,0xfe,0xb0,0xd9,0x6b,0x97,0x00);

        /// <summary>
        ///     View name for "KrVirtualFiles".
        /// </summary>
        public readonly string Alias = "KrVirtualFiles";

        /// <summary>
        ///     View caption for "KrVirtualFiles".
        /// </summary>
        public readonly string Caption = "$Views_Names_KrVirtualFiles";

        /// <summary>
        ///     View group for "KrVirtualFiles".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KrVirtualFileID.
        /// </summary>
        public readonly ViewObject ColumnKrVirtualFileID = new ViewObject(0, "KrVirtualFileID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KrVirtualFileName
        ///     Caption: $Views_KrVirtualFiles_Name.
        /// </summary>
        public readonly ViewObject ColumnKrVirtualFileName = new ViewObject(1, "KrVirtualFileName", "$Views_KrVirtualFiles_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: KrVirtualFileFileTemplate
        ///     Caption: $Views_KrVirtualFiles_FileTemplate.
        /// </summary>
        public readonly ViewObject ColumnKrVirtualFileFileTemplate = new ViewObject(2, "KrVirtualFileFileTemplate", "$Views_KrVirtualFiles_FileTemplate");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_KrVirtualFiles_Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_KrVirtualFiles_Name");

        #endregion

        #region ToString

        public static implicit operator string(KrVirtualFilesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Languages

    /// <summary>
    ///     ID: {7ed54a59-1c9e-469b-83eb-ed1c6ec70753}
    ///     Alias: Languages
    ///     Caption: $Views_Names_Languages
    ///     Group: System
    /// </summary>
    public class LanguagesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Languages": {7ed54a59-1c9e-469b-83eb-ed1c6ec70753}.
        /// </summary>
        public readonly Guid ID = new Guid(0x7ed54a59,0x1c9e,0x469b,0x83,0xeb,0xed,0x1c,0x6e,0xc7,0x07,0x53);

        /// <summary>
        ///     View name for "Languages".
        /// </summary>
        public readonly string Alias = "Languages";

        /// <summary>
        ///     View caption for "Languages".
        /// </summary>
        public readonly string Caption = "$Views_Names_Languages";

        /// <summary>
        ///     View group for "Languages".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: LanguageID.
        /// </summary>
        public readonly ViewObject ColumnLanguageID = new ViewObject(0, "LanguageID");

        /// <summary>
        ///     ID:1 
        ///     Alias: LanguageCaption
        ///     Caption: $Views_KrLanguages_Caption.
        /// </summary>
        public readonly ViewObject ColumnLanguageCaption = new ViewObject(1, "LanguageCaption", "$Views_KrLanguages_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: LanguageCode.
        /// </summary>
        public readonly ViewObject ColumnLanguageCode = new ViewObject(2, "LanguageCode");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CaptionParam
        ///     Caption: $Views_KrLanguages_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamCaptionParam = new ViewObject(0, "CaptionParam", "$Views_KrLanguages_Caption_Param");

        #endregion

        #region ToString

        public static implicit operator string(LanguagesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LastTopics

    /// <summary>
    ///     ID: {ba6ff2de-b4d3-47cc-9f98-29baabdd6bce}
    ///     Alias: LastTopics
    ///     Caption: $Views_Names_LastTopics
    ///     Group: Fm
    /// </summary>
    public class LastTopicsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LastTopics": {ba6ff2de-b4d3-47cc-9f98-29baabdd6bce}.
        /// </summary>
        public readonly Guid ID = new Guid(0xba6ff2de,0xb4d3,0x47cc,0x9f,0x98,0x29,0xba,0xab,0xdd,0x6b,0xce);

        /// <summary>
        ///     View name for "LastTopics".
        /// </summary>
        public readonly string Alias = "LastTopics";

        /// <summary>
        ///     View caption for "LastTopics".
        /// </summary>
        public readonly string Caption = "$Views_Names_LastTopics";

        /// <summary>
        ///     View group for "LastTopics".
        /// </summary>
        public readonly string Group = "Fm";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(0, "CardID");

        /// <summary>
        ///     ID:1 
        ///     Alias: Created
        ///     Caption: $Views_MyTopics_Created.
        /// </summary>
        public readonly ViewObject ColumnCreated = new ViewObject(1, "Created", "$Views_MyTopics_Created");

        /// <summary>
        ///     ID:2 
        ///     Alias: TopicID.
        /// </summary>
        public readonly ViewObject ColumnTopicID = new ViewObject(2, "TopicID");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(3, "TypeID");

        /// <summary>
        ///     ID:4 
        ///     Alias: TopicName
        ///     Caption: $Views_MyTopics_TopicName.
        /// </summary>
        public readonly ViewObject ColumnTopicName = new ViewObject(4, "TopicName", "$Views_MyTopics_TopicName");

        /// <summary>
        ///     ID:5 
        ///     Alias: Description
        ///     Caption: $Views_MyTopics_Description.
        /// </summary>
        public readonly ViewObject ColumnDescription = new ViewObject(5, "Description", "$Views_MyTopics_Description");

        /// <summary>
        ///     ID:6 
        ///     Alias: AuthorName
        ///     Caption: $Views_MyTopics_AuthorName.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(6, "AuthorName", "$Views_MyTopics_AuthorName");

        /// <summary>
        ///     ID:7 
        ///     Alias: LastRead
        ///     Caption: $Views_MyTopics_LastRead.
        /// </summary>
        public readonly ViewObject ColumnLastRead = new ViewObject(7, "LastRead", "$Views_MyTopics_LastRead");

        /// <summary>
        ///     ID:8 
        ///     Alias: LastMessage
        ///     Caption: $Views_MyTopics_LastMessage.
        /// </summary>
        public readonly ViewObject ColumnLastMessage = new ViewObject(8, "LastMessage", "$Views_MyTopics_LastMessage");

        /// <summary>
        ///     ID:9 
        ///     Alias: LastMessageAuthorName
        ///     Caption: $Views_MyTopics_LastMessageAuthorName.
        /// </summary>
        public readonly ViewObject ColumnLastMessageAuthorName = new ViewObject(9, "LastMessageAuthorName", "$Views_MyTopics_LastMessageAuthorName");

        /// <summary>
        ///     ID:10 
        ///     Alias: IsArchived.
        /// </summary>
        public readonly ViewObject ColumnIsArchived = new ViewObject(10, "IsArchived");

        /// <summary>
        ///     ID:11 
        ///     Alias: AppearanceColumn.
        /// </summary>
        public readonly ViewObject ColumnAppearanceColumn = new ViewObject(11, "AppearanceColumn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Created
        ///     Caption: $Views_MyTopics_Created.
        /// </summary>
        public readonly ViewObject ParamCreated = new ViewObject(0, "Created", "$Views_MyTopics_Created");

        /// <summary>
        ///     ID:1 
        ///     Alias: IsArchived
        ///     Caption: $Views_MyTopics_ShowArchived.
        /// </summary>
        public readonly ViewObject ParamIsArchived = new ViewObject(1, "IsArchived", "$Views_MyTopics_ShowArchived");

        #endregion

        #region ToString

        public static implicit operator string(LastTopicsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawCases

    /// <summary>
    ///     ID: {fc9df545-e9f7-4562-a37d-f8da4a10d248}
    ///     Alias: LawCases
    ///     Caption: $Views_Names_LawCases
    ///     Group: Law
    /// </summary>
    public class LawCasesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawCases": {fc9df545-e9f7-4562-a37d-f8da4a10d248}.
        /// </summary>
        public readonly Guid ID = new Guid(0xfc9df545,0xe9f7,0x4562,0xa3,0x7d,0xf8,0xda,0x4a,0x10,0xd2,0x48);

        /// <summary>
        ///     View name for "LawCases".
        /// </summary>
        public readonly string Alias = "LawCases";

        /// <summary>
        ///     View caption for "LawCases".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawCases";

        /// <summary>
        ///     View group for "LawCases".
        /// </summary>
        public readonly string Group = "Law";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(0, "rn");

        /// <summary>
        ///     ID:1 
        ///     Alias: ID.
        /// </summary>
        public readonly ViewObject ColumnID = new ViewObject(1, "ID");

        /// <summary>
        ///     ID:2 
        ///     Alias: Number
        ///     Caption: $Views_LawCases_Number.
        /// </summary>
        public readonly ViewObject ColumnNumber = new ViewObject(2, "Number", "$Views_LawCases_Number");

        /// <summary>
        ///     ID:3 
        ///     Alias: StartDate
        ///     Caption: $Views_LawCases_StartDate.
        /// </summary>
        public readonly ViewObject ColumnStartDate = new ViewObject(3, "StartDate", "$Views_LawCases_StartDate");

        /// <summary>
        ///     ID:4 
        ///     Alias: DecisionDate
        ///     Caption: $Views_LawCases_DecisionDate.
        /// </summary>
        public readonly ViewObject ColumnDecisionDate = new ViewObject(4, "DecisionDate", "$Views_LawCases_DecisionDate");

        /// <summary>
        ///     ID:5 
        ///     Alias: NumberByCourt
        ///     Caption: $Views_LawCases_NumberByCourt.
        /// </summary>
        public readonly ViewObject ColumnNumberByCourt = new ViewObject(5, "NumberByCourt", "$Views_LawCases_NumberByCourt");

        /// <summary>
        ///     ID:6 
        ///     Alias: Description
        ///     Caption: $Views_LawCases_Description.
        /// </summary>
        public readonly ViewObject ColumnDescription = new ViewObject(6, "Description", "$Views_LawCases_Description");

        /// <summary>
        ///     ID:7 
        ///     Alias: Clients
        ///     Caption: $Views_LawCases_Clients.
        /// </summary>
        public readonly ViewObject ColumnClients = new ViewObject(7, "Clients", "$Views_LawCases_Clients");

        /// <summary>
        ///     ID:8 
        ///     Alias: Partners
        ///     Caption: $Views_LawCases_Partners.
        /// </summary>
        public readonly ViewObject ColumnPartners = new ViewObject(8, "Partners", "$Views_LawCases_Partners");

        /// <summary>
        ///     ID:9 
        ///     Alias: Pcto
        ///     Caption: PCTO.
        /// </summary>
        public readonly ViewObject ColumnPcto = new ViewObject(9, "Pcto", "PCTO");

        /// <summary>
        ///     ID:10 
        ///     Alias: ClassificationPlan
        ///     Caption: $Views_LawCases_ClassificationPlan.
        /// </summary>
        public readonly ViewObject ColumnClassificationPlan = new ViewObject(10, "ClassificationPlan", "$Views_LawCases_ClassificationPlan");

        /// <summary>
        ///     ID:11 
        ///     Alias: StoreLocation
        ///     Caption: $Views_LawCases_StoreLocation.
        /// </summary>
        public readonly ViewObject ColumnStoreLocation = new ViewObject(11, "StoreLocation", "$Views_LawCases_StoreLocation");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Number
        ///     Caption: $Views_LawCases_Number.
        /// </summary>
        public readonly ViewObject ParamNumber = new ViewObject(0, "Number", "$Views_LawCases_Number");

        /// <summary>
        ///     ID:1 
        ///     Alias: NumberByCourt
        ///     Caption: $Views_LawCases_NumberByCourt.
        /// </summary>
        public readonly ViewObject ParamNumberByCourt = new ViewObject(1, "NumberByCourt", "$Views_LawCases_NumberByCourt");

        /// <summary>
        ///     ID:2 
        ///     Alias: Description
        ///     Caption: $Views_LawCases_Description.
        /// </summary>
        public readonly ViewObject ParamDescription = new ViewObject(2, "Description", "$Views_LawCases_Description");

        /// <summary>
        ///     ID:3 
        ///     Alias: ClassificationPlan
        ///     Caption: $Views_LawCases_ClassificationPlan.
        /// </summary>
        public readonly ViewObject ParamClassificationPlan = new ViewObject(3, "ClassificationPlan", "$Views_LawCases_ClassificationPlan");

        /// <summary>
        ///     ID:4 
        ///     Alias: StartDate
        ///     Caption: $Views_LawCases_StartDate.
        /// </summary>
        public readonly ViewObject ParamStartDate = new ViewObject(4, "StartDate", "$Views_LawCases_StartDate");

        /// <summary>
        ///     ID:5 
        ///     Alias: Administrators
        ///     Caption: $Views_LawCases_Administrators.
        /// </summary>
        public readonly ViewObject ParamAdministrators = new ViewObject(5, "Administrators", "$Views_LawCases_Administrators");

        #endregion

        #region ToString

        public static implicit operator string(LawCasesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawCategories

    /// <summary>
    ///     ID: {abe65a63-77cd-4f40-a6ce-d5c51ac1d022}
    ///     Alias: LawCategories
    ///     Caption: $Views_Names_LawCategories
    ///     Group: LawDictionary
    /// </summary>
    public class LawCategoriesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawCategories": {abe65a63-77cd-4f40-a6ce-d5c51ac1d022}.
        /// </summary>
        public readonly Guid ID = new Guid(0xabe65a63,0x77cd,0x4f40,0xa6,0xce,0xd5,0xc5,0x1a,0xc1,0xd0,0x22);

        /// <summary>
        ///     View name for "LawCategories".
        /// </summary>
        public readonly string Alias = "LawCategories";

        /// <summary>
        ///     View caption for "LawCategories".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawCategories";

        /// <summary>
        ///     View group for "LawCategories".
        /// </summary>
        public readonly string Group = "LawDictionary";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CategoryID.
        /// </summary>
        public readonly ViewObject ColumnCategoryID = new ViewObject(0, "CategoryID");

        /// <summary>
        ///     ID:1 
        ///     Alias: CategoryNumber
        ///     Caption: $Views_LawCategories_CategoryNumber.
        /// </summary>
        public readonly ViewObject ColumnCategoryNumber = new ViewObject(1, "CategoryNumber", "$Views_LawCategories_CategoryNumber");

        /// <summary>
        ///     ID:2 
        ///     Alias: CategoryIcon
        ///     Caption: $Views_LawCategories_CategoryIcon.
        /// </summary>
        public readonly ViewObject ColumnCategoryIcon = new ViewObject(2, "CategoryIcon", "$Views_LawCategories_CategoryIcon");

        /// <summary>
        ///     ID:3 
        ///     Alias: CategoryName
        ///     Caption: $Views_LawCategories_CategoryName.
        /// </summary>
        public readonly ViewObject ColumnCategoryName = new ViewObject(3, "CategoryName", "$Views_LawCategories_CategoryName");

        /// <summary>
        ///     ID:4 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(4, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Number
        ///     Caption: $Views_LawCategories_CategoryNumber.
        /// </summary>
        public readonly ViewObject ParamNumber = new ViewObject(0, "Number", "$Views_LawCategories_CategoryNumber");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_LawCategories_CategoryName.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_LawCategories_CategoryName");

        #endregion

        #region ToString

        public static implicit operator string(LawCategoriesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawClassificationPlans

    /// <summary>
    ///     ID: {838245d0-f5b9-4ed6-9343-ee74d383f689}
    ///     Alias: LawClassificationPlans
    ///     Caption: $Views_Names_LawClassificationPlans
    ///     Group: LawDictionary
    /// </summary>
    public class LawClassificationPlansViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawClassificationPlans": {838245d0-f5b9-4ed6-9343-ee74d383f689}.
        /// </summary>
        public readonly Guid ID = new Guid(0x838245d0,0xf5b9,0x4ed6,0x93,0x43,0xee,0x74,0xd3,0x83,0xf6,0x89);

        /// <summary>
        ///     View name for "LawClassificationPlans".
        /// </summary>
        public readonly string Alias = "LawClassificationPlans";

        /// <summary>
        ///     View caption for "LawClassificationPlans".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawClassificationPlans";

        /// <summary>
        ///     View group for "LawClassificationPlans".
        /// </summary>
        public readonly string Group = "LawDictionary";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: PlanID.
        /// </summary>
        public readonly ViewObject ColumnPlanID = new ViewObject(0, "PlanID");

        /// <summary>
        ///     ID:1 
        ///     Alias: PlanFullName.
        /// </summary>
        public readonly ViewObject ColumnPlanFullName = new ViewObject(1, "PlanFullName");

        /// <summary>
        ///     ID:2 
        ///     Alias: PlanPlan
        ///     Caption: $Views_LawClassificationPlans_PlanPlan.
        /// </summary>
        public readonly ViewObject ColumnPlanPlan = new ViewObject(2, "PlanPlan", "$Views_LawClassificationPlans_PlanPlan");

        /// <summary>
        ///     ID:3 
        ///     Alias: PlanName
        ///     Caption: $Views_LawClassificationPlans_PlanName.
        /// </summary>
        public readonly ViewObject ColumnPlanName = new ViewObject(3, "PlanName", "$Views_LawClassificationPlans_PlanName");

        /// <summary>
        ///     ID:4 
        ///     Alias: PlanDescription
        ///     Caption: $Views_LawClassificationPlans_Description.
        /// </summary>
        public readonly ViewObject ColumnPlanDescription = new ViewObject(4, "PlanDescription", "$Views_LawClassificationPlans_Description");

        /// <summary>
        ///     ID:5 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(5, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Plan
        ///     Caption: $Views_LawClassificationPlans_PlanPlan.
        /// </summary>
        public readonly ViewObject ParamPlan = new ViewObject(0, "Plan", "$Views_LawClassificationPlans_PlanPlan");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_LawClassificationPlans_PlanName.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_LawClassificationPlans_PlanName");

        /// <summary>
        ///     ID:2 
        ///     Alias: Description
        ///     Caption: $Views_LawClassificationPlans_Description.
        /// </summary>
        public readonly ViewObject ParamDescription = new ViewObject(2, "Description", "$Views_LawClassificationPlans_Description");

        #endregion

        #region ToString

        public static implicit operator string(LawClassificationPlansViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawClients

    /// <summary>
    ///     ID: {d87978ea-f6d4-4cec-9a74-3354a910c5f1}
    ///     Alias: LawClients
    ///     Caption: $Views_Names_LawClients
    ///     Group: LawDictionary
    /// </summary>
    public class LawClientsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawClients": {d87978ea-f6d4-4cec-9a74-3354a910c5f1}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd87978ea,0xf6d4,0x4cec,0x9a,0x74,0x33,0x54,0xa9,0x10,0xc5,0xf1);

        /// <summary>
        ///     View name for "LawClients".
        /// </summary>
        public readonly string Alias = "LawClients";

        /// <summary>
        ///     View caption for "LawClients".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawClients";

        /// <summary>
        ///     View group for "LawClients".
        /// </summary>
        public readonly string Group = "LawDictionary";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ClientID.
        /// </summary>
        public readonly ViewObject ColumnClientID = new ViewObject(0, "ClientID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ClientName
        ///     Caption: $Views_LawClients_ClientName.
        /// </summary>
        public readonly ViewObject ColumnClientName = new ViewObject(1, "ClientName", "$Views_LawClients_ClientName");

        /// <summary>
        ///     ID:2 
        ///     Alias: ClientType
        ///     Caption: $Views_LawClients_ClientType.
        /// </summary>
        public readonly ViewObject ColumnClientType = new ViewObject(2, "ClientType", "$Views_LawClients_ClientType");

        /// <summary>
        ///     ID:3 
        ///     Alias: ClientKindID.
        /// </summary>
        public readonly ViewObject ColumnClientKindID = new ViewObject(3, "ClientKindID");

        /// <summary>
        ///     ID:4 
        ///     Alias: ClientKindName
        ///     Caption: $Views_LawClients_ClientKindName.
        /// </summary>
        public readonly ViewObject ColumnClientKindName = new ViewObject(4, "ClientKindName", "$Views_LawClients_ClientKindName");

        /// <summary>
        ///     ID:5 
        ///     Alias: ClientTaxNumber
        ///     Caption: $Views_LawClients_ClientTaxNumber.
        /// </summary>
        public readonly ViewObject ColumnClientTaxNumber = new ViewObject(5, "ClientTaxNumber", "$Views_LawClients_ClientTaxNumber");

        /// <summary>
        ///     ID:6 
        ///     Alias: ClientRegistrationNumber
        ///     Caption: $Views_LawClients_ClientRegistrationNumber.
        /// </summary>
        public readonly ViewObject ColumnClientRegistrationNumber = new ViewObject(6, "ClientRegistrationNumber", "$Views_LawClients_ClientRegistrationNumber");

        /// <summary>
        ///     ID:7 
        ///     Alias: ClientAddress
        ///     Caption: $Views_LawClients_ClientAddress.
        /// </summary>
        public readonly ViewObject ColumnClientAddress = new ViewObject(7, "ClientAddress", "$Views_LawClients_ClientAddress");

        /// <summary>
        ///     ID:8 
        ///     Alias: ClientContacts
        ///     Caption: $Views_LawClients_ClientContacts.
        /// </summary>
        public readonly ViewObject ColumnClientContacts = new ViewObject(8, "ClientContacts", "$Views_LawClients_ClientContacts");

        /// <summary>
        ///     ID:9 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(9, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_LawClients_ClientName.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_LawClients_ClientName");

        /// <summary>
        ///     ID:1 
        ///     Alias: TaxNumber
        ///     Caption: $Views_LawClients_ClientTaxNumber.
        /// </summary>
        public readonly ViewObject ParamTaxNumber = new ViewObject(1, "TaxNumber", "$Views_LawClients_ClientTaxNumber");

        /// <summary>
        ///     ID:2 
        ///     Alias: RegistrationNumber
        ///     Caption: $Views_LawClients_ClientRegistrationNumber.
        /// </summary>
        public readonly ViewObject ParamRegistrationNumber = new ViewObject(2, "RegistrationNumber", "$Views_LawClients_ClientRegistrationNumber");

        /// <summary>
        ///     ID:3 
        ///     Alias: Contacts
        ///     Caption: $Views_LawClients_ClientContacts.
        /// </summary>
        public readonly ViewObject ParamContacts = new ViewObject(3, "Contacts", "$Views_LawClients_ClientContacts");

        /// <summary>
        ///     ID:4 
        ///     Alias: EntityKind
        ///     Caption: $Views_LawClients_ClientKindName.
        /// </summary>
        public readonly ViewObject ParamEntityKind = new ViewObject(4, "EntityKind", "$Views_LawClients_ClientKindName");

        /// <summary>
        ///     ID:5 
        ///     Alias: Street
        ///     Caption: $Views_LawClients_Street.
        /// </summary>
        public readonly ViewObject ParamStreet = new ViewObject(5, "Street", "$Views_LawClients_Street");

        /// <summary>
        ///     ID:6 
        ///     Alias: PostOffice
        ///     Caption: $Views_LawClients_PostOffice.
        /// </summary>
        public readonly ViewObject ParamPostOffice = new ViewObject(6, "PostOffice", "$Views_LawClients_PostOffice");

        /// <summary>
        ///     ID:7 
        ///     Alias: Region
        ///     Caption: $Views_LawClients_Region.
        /// </summary>
        public readonly ViewObject ParamRegion = new ViewObject(7, "Region", "$Views_LawClients_Region");

        /// <summary>
        ///     ID:8 
        ///     Alias: Country
        ///     Caption: $Views_LawClients_Country.
        /// </summary>
        public readonly ViewObject ParamCountry = new ViewObject(8, "Country", "$Views_LawClients_Country");

        #endregion

        #region ToString

        public static implicit operator string(LawClientsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawDocKinds

    /// <summary>
    ///     ID: {121cee33-1f41-491b-8ac0-1d3d199be43a}
    ///     Alias: LawDocKinds
    ///     Caption: $Views_Names_LawDocKinds
    ///     Group: LawDictionary
    /// </summary>
    public class LawDocKindsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawDocKinds": {121cee33-1f41-491b-8ac0-1d3d199be43a}.
        /// </summary>
        public readonly Guid ID = new Guid(0x121cee33,0x1f41,0x491b,0x8a,0xc0,0x1d,0x3d,0x19,0x9b,0xe4,0x3a);

        /// <summary>
        ///     View name for "LawDocKinds".
        /// </summary>
        public readonly string Alias = "LawDocKinds";

        /// <summary>
        ///     View caption for "LawDocKinds".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawDocKinds";

        /// <summary>
        ///     View group for "LawDocKinds".
        /// </summary>
        public readonly string Group = "LawDictionary";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KindID.
        /// </summary>
        public readonly ViewObject ColumnKindID = new ViewObject(0, "KindID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KindDirection
        ///     Caption: $Views_LawDocKinds_KindDirection.
        /// </summary>
        public readonly ViewObject ColumnKindDirection = new ViewObject(1, "KindDirection", "$Views_LawDocKinds_KindDirection");

        /// <summary>
        ///     ID:2 
        ///     Alias: KindName
        ///     Caption: $Views_LawDocKinds_KindName.
        /// </summary>
        public readonly ViewObject ColumnKindName = new ViewObject(2, "KindName", "$Views_LawDocKinds_KindName");

        /// <summary>
        ///     ID:3 
        ///     Alias: KindByDefault
        ///     Caption: $Views_LawDocKinds_KindByDefault.
        /// </summary>
        public readonly ViewObject ColumnKindByDefault = new ViewObject(3, "KindByDefault", "$Views_LawDocKinds_KindByDefault");

        /// <summary>
        ///     ID:4 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(4, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Direction
        ///     Caption: $Views_LawDocKinds_KindDirection.
        /// </summary>
        public readonly ViewObject ParamDirection = new ViewObject(0, "Direction", "$Views_LawDocKinds_KindDirection");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_LawDocKinds_KindName.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_LawDocKinds_KindName");

        /// <summary>
        ///     ID:2 
        ///     Alias: ByDefault
        ///     Caption: $Views_LawDocKinds_KindByDefault.
        /// </summary>
        public readonly ViewObject ParamByDefault = new ViewObject(2, "ByDefault", "$Views_LawDocKinds_KindByDefault");

        #endregion

        #region ToString

        public static implicit operator string(LawDocKindsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawDocTypes

    /// <summary>
    ///     ID: {79b46d45-d6ac-4b46-8051-7cfabc879bfe}
    ///     Alias: LawDocTypes
    ///     Caption: $Views_Names_LawDocTypes
    ///     Group: LawDictionary
    /// </summary>
    public class LawDocTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawDocTypes": {79b46d45-d6ac-4b46-8051-7cfabc879bfe}.
        /// </summary>
        public readonly Guid ID = new Guid(0x79b46d45,0xd6ac,0x4b46,0x80,0x51,0x7c,0xfa,0xbc,0x87,0x9b,0xfe);

        /// <summary>
        ///     View name for "LawDocTypes".
        /// </summary>
        public readonly string Alias = "LawDocTypes";

        /// <summary>
        ///     View caption for "LawDocTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawDocTypes";

        /// <summary>
        ///     View group for "LawDocTypes".
        /// </summary>
        public readonly string Group = "LawDictionary";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeName
        ///     Caption: $Views_LawDocTypes_TypeName.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(1, "TypeName", "$Views_LawDocTypes_TypeName");

        /// <summary>
        ///     ID:2 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(2, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_LawDocTypes_TypeName.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_LawDocTypes_TypeName");

        #endregion

        #region ToString

        public static implicit operator string(LawDocTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawEntityKinds

    /// <summary>
    ///     ID: {05d82dad-cf51-4917-bb9e-6d76ede89d0b}
    ///     Alias: LawEntityKinds
    ///     Caption: $Views_Names_LawEntityKinds
    ///     Group: LawDictionary
    /// </summary>
    public class LawEntityKindsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawEntityKinds": {05d82dad-cf51-4917-bb9e-6d76ede89d0b}.
        /// </summary>
        public readonly Guid ID = new Guid(0x05d82dad,0xcf51,0x4917,0xbb,0x9e,0x6d,0x76,0xed,0xe8,0x9d,0x0b);

        /// <summary>
        ///     View name for "LawEntityKinds".
        /// </summary>
        public readonly string Alias = "LawEntityKinds";

        /// <summary>
        ///     View caption for "LawEntityKinds".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawEntityKinds";

        /// <summary>
        ///     View group for "LawEntityKinds".
        /// </summary>
        public readonly string Group = "LawDictionary";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KindID.
        /// </summary>
        public readonly ViewObject ColumnKindID = new ViewObject(0, "KindID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KindName
        ///     Caption: $Views_LawEntityKinds_KindName.
        /// </summary>
        public readonly ViewObject ColumnKindName = new ViewObject(1, "KindName", "$Views_LawEntityKinds_KindName");

        /// <summary>
        ///     ID:2 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(2, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_LawDocTypes_TypeName.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_LawDocTypes_TypeName");

        #endregion

        #region ToString

        public static implicit operator string(LawEntityKindsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawFileStorages

    /// <summary>
    ///     ID: {0160728f-e47c-426e-a569-09491c421091}
    ///     Alias: LawFileStorages
    ///     Caption: $Views_Names_LawFileStorages
    ///     Group: LawDictionary
    /// </summary>
    public class LawFileStoragesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawFileStorages": {0160728f-e47c-426e-a569-09491c421091}.
        /// </summary>
        public readonly Guid ID = new Guid(0x0160728f,0xe47c,0x426e,0xa5,0x69,0x09,0x49,0x1c,0x42,0x10,0x91);

        /// <summary>
        ///     View name for "LawFileStorages".
        /// </summary>
        public readonly string Alias = "LawFileStorages";

        /// <summary>
        ///     View caption for "LawFileStorages".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawFileStorages";

        /// <summary>
        ///     View group for "LawFileStorages".
        /// </summary>
        public readonly string Group = "LawDictionary";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: StorageID.
        /// </summary>
        public readonly ViewObject ColumnStorageID = new ViewObject(0, "StorageID");

        /// <summary>
        ///     ID:1 
        ///     Alias: StorageName
        ///     Caption: $Views_LawFileStorages_StorageName.
        /// </summary>
        public readonly ViewObject ColumnStorageName = new ViewObject(1, "StorageName", "$Views_LawFileStorages_StorageName");

        /// <summary>
        ///     ID:2 
        ///     Alias: StorageType
        ///     Caption: $Views_LawFileStorages_StorageType.
        /// </summary>
        public readonly ViewObject ColumnStorageType = new ViewObject(2, "StorageType", "$Views_LawFileStorages_StorageType");

        /// <summary>
        ///     ID:3 
        ///     Alias: StorageNotes
        ///     Caption: $Views_LawFileStorages_StorageNotes.
        /// </summary>
        public readonly ViewObject ColumnStorageNotes = new ViewObject(3, "StorageNotes", "$Views_LawFileStorages_StorageNotes");

        /// <summary>
        ///     ID:4 
        ///     Alias: StorageComputerName
        ///     Caption: $Views_LawFileStorages_StorageComputerName.
        /// </summary>
        public readonly ViewObject ColumnStorageComputerName = new ViewObject(4, "StorageComputerName", "$Views_LawFileStorages_StorageComputerName");

        /// <summary>
        ///     ID:5 
        ///     Alias: StorageByDefault
        ///     Caption: $Views_LawFileStorages_StorageByDefault.
        /// </summary>
        public readonly ViewObject ColumnStorageByDefault = new ViewObject(5, "StorageByDefault", "$Views_LawFileStorages_StorageByDefault");

        /// <summary>
        ///     ID:6 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(6, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_LawFileStorages_StorageName.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_LawFileStorages_StorageName");

        /// <summary>
        ///     ID:1 
        ///     Alias: Type
        ///     Caption: $Views_LawFileStorages_StorageType.
        /// </summary>
        public readonly ViewObject ParamType = new ViewObject(1, "Type", "$Views_LawFileStorages_StorageType");

        /// <summary>
        ///     ID:2 
        ///     Alias: Notes
        ///     Caption: $Views_LawFileStorages_StorageNotes.
        /// </summary>
        public readonly ViewObject ParamNotes = new ViewObject(2, "Notes", "$Views_LawFileStorages_StorageNotes");

        /// <summary>
        ///     ID:3 
        ///     Alias: ComputerName
        ///     Caption: $Views_LawFileStorages_StorageComputerName.
        /// </summary>
        public readonly ViewObject ParamComputerName = new ViewObject(3, "ComputerName", "$Views_LawFileStorages_StorageComputerName");

        /// <summary>
        ///     ID:4 
        ///     Alias: ByDefault
        ///     Caption: $Views_LawFileStorages_StorageByDefault.
        /// </summary>
        public readonly ViewObject ParamByDefault = new ViewObject(4, "ByDefault", "$Views_LawFileStorages_StorageByDefault");

        #endregion

        #region ToString

        public static implicit operator string(LawFileStoragesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawFolders

    /// <summary>
    ///     ID: {79dba5d9-833e-49ee-a6be-9530abe314f1}
    ///     Alias: LawFolders
    ///     Caption: $Views_Names_LawFolders
    ///     Group: Law
    /// </summary>
    public class LawFoldersViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawFolders": {79dba5d9-833e-49ee-a6be-9530abe314f1}.
        /// </summary>
        public readonly Guid ID = new Guid(0x79dba5d9,0x833e,0x49ee,0xa6,0xbe,0x95,0x30,0xab,0xe3,0x14,0xf1);

        /// <summary>
        ///     View name for "LawFolders".
        /// </summary>
        public readonly string Alias = "LawFolders";

        /// <summary>
        ///     View caption for "LawFolders".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawFolders";

        /// <summary>
        ///     View group for "LawFolders".
        /// </summary>
        public readonly string Group = "Law";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RowID.
        /// </summary>
        public readonly ViewObject ColumnRowID = new ViewObject(0, "RowID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ParentRowID.
        /// </summary>
        public readonly ViewObject ColumnParentRowID = new ViewObject(1, "ParentRowID");

        /// <summary>
        ///     ID:2 
        ///     Alias: Name
        ///     Caption: $Views_LawFolders_Name.
        /// </summary>
        public readonly ViewObject ColumnName = new ViewObject(2, "Name", "$Views_LawFolders_Name");

        /// <summary>
        ///     ID:3 
        ///     Alias: Date
        ///     Caption: $Views_LawFolders_Date.
        /// </summary>
        public readonly ViewObject ColumnDate = new ViewObject(3, "Date", "$Views_LawFolders_Date");

        /// <summary>
        ///     ID:4 
        ///     Alias: Kind
        ///     Caption: $Views_LawFolders_Kind.
        /// </summary>
        public readonly ViewObject ColumnKind = new ViewObject(4, "Kind", "$Views_LawFolders_Kind");

        /// <summary>
        ///     ID:5 
        ///     Alias: Number
        ///     Caption: $Views_LawFolders_Number.
        /// </summary>
        public readonly ViewObject ColumnNumber = new ViewObject(5, "Number", "$Views_LawFolders_Number");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CaseID.
        /// </summary>
        public readonly ViewObject ParamCaseID = new ViewObject(0, "CaseID");

        #endregion

        #region ToString

        public static implicit operator string(LawFoldersViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawPartnerRepresentatives

    /// <summary>
    ///     ID: {c8f9dad9-f2e0-40f8-9e6f-9fabb2e8fe83}
    ///     Alias: LawPartnerRepresentatives
    ///     Caption: $Views_Names_LawPartnerRepresentatives
    ///     Group: LawDictionary
    /// </summary>
    public class LawPartnerRepresentativesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawPartnerRepresentatives": {c8f9dad9-f2e0-40f8-9e6f-9fabb2e8fe83}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc8f9dad9,0xf2e0,0x40f8,0x9e,0x6f,0x9f,0xab,0xb2,0xe8,0xfe,0x83);

        /// <summary>
        ///     View name for "LawPartnerRepresentatives".
        /// </summary>
        public readonly string Alias = "LawPartnerRepresentatives";

        /// <summary>
        ///     View caption for "LawPartnerRepresentatives".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawPartnerRepresentatives";

        /// <summary>
        ///     View group for "LawPartnerRepresentatives".
        /// </summary>
        public readonly string Group = "LawDictionary";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RepresentativeID.
        /// </summary>
        public readonly ViewObject ColumnRepresentativeID = new ViewObject(0, "RepresentativeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RepresentativeName
        ///     Caption: $Views_LawPartnerRepresentatives_RepresentativeName.
        /// </summary>
        public readonly ViewObject ColumnRepresentativeName = new ViewObject(1, "RepresentativeName", "$Views_LawPartnerRepresentatives_RepresentativeName");

        /// <summary>
        ///     ID:2 
        ///     Alias: RepresentativeType
        ///     Caption: $Views_LawPartnerRepresentatives_RepresentativeType.
        /// </summary>
        public readonly ViewObject ColumnRepresentativeType = new ViewObject(2, "RepresentativeType", "$Views_LawPartnerRepresentatives_RepresentativeType");

        /// <summary>
        ///     ID:3 
        ///     Alias: RepresentativeKindID.
        /// </summary>
        public readonly ViewObject ColumnRepresentativeKindID = new ViewObject(3, "RepresentativeKindID");

        /// <summary>
        ///     ID:4 
        ///     Alias: RepresentativeKindName
        ///     Caption: $Views_LawPartnerRepresentatives_RepresentativeKindName.
        /// </summary>
        public readonly ViewObject ColumnRepresentativeKindName = new ViewObject(4, "RepresentativeKindName", "$Views_LawPartnerRepresentatives_RepresentativeKindName");

        /// <summary>
        ///     ID:5 
        ///     Alias: RepresentativeTaxNumber
        ///     Caption: $Views_LawPartnerRepresentatives_RepresentativeTaxNumber.
        /// </summary>
        public readonly ViewObject ColumnRepresentativeTaxNumber = new ViewObject(5, "RepresentativeTaxNumber", "$Views_LawPartnerRepresentatives_RepresentativeTaxNumber");

        /// <summary>
        ///     ID:6 
        ///     Alias: RepresentativeRegistrationNumber
        ///     Caption: $Views_LawPartnerRepresentatives_RepresentativeRegistrationNumber.
        /// </summary>
        public readonly ViewObject ColumnRepresentativeRegistrationNumber = new ViewObject(6, "RepresentativeRegistrationNumber", "$Views_LawPartnerRepresentatives_RepresentativeRegistrationNumber");

        /// <summary>
        ///     ID:7 
        ///     Alias: RepresentativeAddress
        ///     Caption: $Views_LawPartnerRepresentatives_RepresentativeAddress.
        /// </summary>
        public readonly ViewObject ColumnRepresentativeAddress = new ViewObject(7, "RepresentativeAddress", "$Views_LawPartnerRepresentatives_RepresentativeAddress");

        /// <summary>
        ///     ID:8 
        ///     Alias: RepresentativeContacts
        ///     Caption: $Views_LawPartnerRepresentatives_RepresentativeContacts.
        /// </summary>
        public readonly ViewObject ColumnRepresentativeContacts = new ViewObject(8, "RepresentativeContacts", "$Views_LawPartnerRepresentatives_RepresentativeContacts");

        /// <summary>
        ///     ID:9 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(9, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_LawPartners_PartnerName.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_LawPartners_PartnerName");

        /// <summary>
        ///     ID:1 
        ///     Alias: TaxNumber
        ///     Caption: $Views_LawPartnerRepresentatives_RepresentativeTaxNumber.
        /// </summary>
        public readonly ViewObject ParamTaxNumber = new ViewObject(1, "TaxNumber", "$Views_LawPartnerRepresentatives_RepresentativeTaxNumber");

        /// <summary>
        ///     ID:2 
        ///     Alias: RegistrationNumber
        ///     Caption: $Views_LawPartnerRepresentatives_RepresentativeRegistrationNumber.
        /// </summary>
        public readonly ViewObject ParamRegistrationNumber = new ViewObject(2, "RegistrationNumber", "$Views_LawPartnerRepresentatives_RepresentativeRegistrationNumber");

        /// <summary>
        ///     ID:3 
        ///     Alias: Contacts
        ///     Caption: $Views_LawPartnerRepresentatives_RepresentativeContacts.
        /// </summary>
        public readonly ViewObject ParamContacts = new ViewObject(3, "Contacts", "$Views_LawPartnerRepresentatives_RepresentativeContacts");

        /// <summary>
        ///     ID:4 
        ///     Alias: EntityKind
        ///     Caption: $Views_LawPartnerRepresentatives_RepresentativeKindName.
        /// </summary>
        public readonly ViewObject ParamEntityKind = new ViewObject(4, "EntityKind", "$Views_LawPartnerRepresentatives_RepresentativeKindName");

        /// <summary>
        ///     ID:5 
        ///     Alias: Street
        ///     Caption: $Views_LawPartnerRepresentatives_Street.
        /// </summary>
        public readonly ViewObject ParamStreet = new ViewObject(5, "Street", "$Views_LawPartnerRepresentatives_Street");

        /// <summary>
        ///     ID:6 
        ///     Alias: PostOffice
        ///     Caption: $Views_LawPartnerRepresentatives_PostOffice.
        /// </summary>
        public readonly ViewObject ParamPostOffice = new ViewObject(6, "PostOffice", "$Views_LawPartnerRepresentatives_PostOffice");

        /// <summary>
        ///     ID:7 
        ///     Alias: Region
        ///     Caption: $Views_LawPartnerRepresentatives_Region.
        /// </summary>
        public readonly ViewObject ParamRegion = new ViewObject(7, "Region", "$Views_LawPartnerRepresentatives_Region");

        /// <summary>
        ///     ID:8 
        ///     Alias: Country
        ///     Caption: $Views_LawPartnerRepresentatives_Country.
        /// </summary>
        public readonly ViewObject ParamCountry = new ViewObject(8, "Country", "$Views_LawPartnerRepresentatives_Country");

        #endregion

        #region ToString

        public static implicit operator string(LawPartnerRepresentativesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawPartners

    /// <summary>
    ///     ID: {0f2cecd3-2051-4c26-8c5a-312dbecfb2fc}
    ///     Alias: LawPartners
    ///     Caption: $Views_Names_LawPartners
    ///     Group: LawDictionary
    /// </summary>
    public class LawPartnersViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawPartners": {0f2cecd3-2051-4c26-8c5a-312dbecfb2fc}.
        /// </summary>
        public readonly Guid ID = new Guid(0x0f2cecd3,0x2051,0x4c26,0x8c,0x5a,0x31,0x2d,0xbe,0xcf,0xb2,0xfc);

        /// <summary>
        ///     View name for "LawPartners".
        /// </summary>
        public readonly string Alias = "LawPartners";

        /// <summary>
        ///     View caption for "LawPartners".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawPartners";

        /// <summary>
        ///     View group for "LawPartners".
        /// </summary>
        public readonly string Group = "LawDictionary";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: PartnerID.
        /// </summary>
        public readonly ViewObject ColumnPartnerID = new ViewObject(0, "PartnerID");

        /// <summary>
        ///     ID:1 
        ///     Alias: PartnerName
        ///     Caption: $Views_LawPartners_PartnerName.
        /// </summary>
        public readonly ViewObject ColumnPartnerName = new ViewObject(1, "PartnerName", "$Views_LawPartners_PartnerName");

        /// <summary>
        ///     ID:2 
        ///     Alias: PartnerType
        ///     Caption: $Views_LawPartners_PartnerType.
        /// </summary>
        public readonly ViewObject ColumnPartnerType = new ViewObject(2, "PartnerType", "$Views_LawPartners_PartnerType");

        /// <summary>
        ///     ID:3 
        ///     Alias: PartnerKindID.
        /// </summary>
        public readonly ViewObject ColumnPartnerKindID = new ViewObject(3, "PartnerKindID");

        /// <summary>
        ///     ID:4 
        ///     Alias: PartnerKindName
        ///     Caption: $Views_LawPartners_PartnerKindName.
        /// </summary>
        public readonly ViewObject ColumnPartnerKindName = new ViewObject(4, "PartnerKindName", "$Views_LawPartners_PartnerKindName");

        /// <summary>
        ///     ID:5 
        ///     Alias: PartnerTaxNumber
        ///     Caption: $Views_LawPartners_PartnerTaxNumber.
        /// </summary>
        public readonly ViewObject ColumnPartnerTaxNumber = new ViewObject(5, "PartnerTaxNumber", "$Views_LawPartners_PartnerTaxNumber");

        /// <summary>
        ///     ID:6 
        ///     Alias: PartnerRegistrationNumber
        ///     Caption: $Views_LawPartners_PartnerRegistrationNumber.
        /// </summary>
        public readonly ViewObject ColumnPartnerRegistrationNumber = new ViewObject(6, "PartnerRegistrationNumber", "$Views_LawPartners_PartnerRegistrationNumber");

        /// <summary>
        ///     ID:7 
        ///     Alias: PartnerAddress
        ///     Caption: $Views_LawPartners_PartnerAddress.
        /// </summary>
        public readonly ViewObject ColumnPartnerAddress = new ViewObject(7, "PartnerAddress", "$Views_LawPartners_PartnerAddress");

        /// <summary>
        ///     ID:8 
        ///     Alias: PartnerContacts
        ///     Caption: $Views_LawPartners_PartnerContacts.
        /// </summary>
        public readonly ViewObject ColumnPartnerContacts = new ViewObject(8, "PartnerContacts", "$Views_LawPartners_PartnerContacts");

        /// <summary>
        ///     ID:9 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(9, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_LawPartners_PartnerName.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_LawPartners_PartnerName");

        /// <summary>
        ///     ID:1 
        ///     Alias: TaxNumber
        ///     Caption: $Views_LawPartners_PartnerTaxNumber.
        /// </summary>
        public readonly ViewObject ParamTaxNumber = new ViewObject(1, "TaxNumber", "$Views_LawPartners_PartnerTaxNumber");

        /// <summary>
        ///     ID:2 
        ///     Alias: RegistrationNumber
        ///     Caption: $Views_LawPartners_PartnerRegistrationNumber.
        /// </summary>
        public readonly ViewObject ParamRegistrationNumber = new ViewObject(2, "RegistrationNumber", "$Views_LawPartners_PartnerRegistrationNumber");

        /// <summary>
        ///     ID:3 
        ///     Alias: Contacts
        ///     Caption: $Views_LawPartners_PartnerContacts.
        /// </summary>
        public readonly ViewObject ParamContacts = new ViewObject(3, "Contacts", "$Views_LawPartners_PartnerContacts");

        /// <summary>
        ///     ID:4 
        ///     Alias: EntityKind
        ///     Caption: $Views_LawPartners_PartnerKindName.
        /// </summary>
        public readonly ViewObject ParamEntityKind = new ViewObject(4, "EntityKind", "$Views_LawPartners_PartnerKindName");

        /// <summary>
        ///     ID:5 
        ///     Alias: Street
        ///     Caption: $Views_LawPartners_Street.
        /// </summary>
        public readonly ViewObject ParamStreet = new ViewObject(5, "Street", "$Views_LawPartners_Street");

        /// <summary>
        ///     ID:6 
        ///     Alias: PostOffice
        ///     Caption: $Views_LawPartners_PostOffice.
        /// </summary>
        public readonly ViewObject ParamPostOffice = new ViewObject(6, "PostOffice", "$Views_LawPartners_PostOffice");

        /// <summary>
        ///     ID:7 
        ///     Alias: Region
        ///     Caption: $Views_LawPartners_Region.
        /// </summary>
        public readonly ViewObject ParamRegion = new ViewObject(7, "Region", "$Views_LawPartners_Region");

        /// <summary>
        ///     ID:8 
        ///     Alias: Country
        ///     Caption: $Views_LawPartners_Country.
        /// </summary>
        public readonly ViewObject ParamCountry = new ViewObject(8, "Country", "$Views_LawPartners_Country");

        #endregion

        #region ToString

        public static implicit operator string(LawPartnersViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawStoreLocations

    /// <summary>
    ///     ID: {0604e158-4700-40e5-9a1a-b3e766262f6a}
    ///     Alias: LawStoreLocations
    ///     Caption: $Views_Names_LawStoreLocations
    ///     Group: LawDictionary
    /// </summary>
    public class LawStoreLocationsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawStoreLocations": {0604e158-4700-40e5-9a1a-b3e766262f6a}.
        /// </summary>
        public readonly Guid ID = new Guid(0x0604e158,0x4700,0x40e5,0x9a,0x1a,0xb3,0xe7,0x66,0x26,0x2f,0x6a);

        /// <summary>
        ///     View name for "LawStoreLocations".
        /// </summary>
        public readonly string Alias = "LawStoreLocations";

        /// <summary>
        ///     View caption for "LawStoreLocations".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawStoreLocations";

        /// <summary>
        ///     View group for "LawStoreLocations".
        /// </summary>
        public readonly string Group = "LawDictionary";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: LocationID.
        /// </summary>
        public readonly ViewObject ColumnLocationID = new ViewObject(0, "LocationID");

        /// <summary>
        ///     ID:1 
        ///     Alias: LocationName
        ///     Caption: $Views_LawStoreLocations_LocationName.
        /// </summary>
        public readonly ViewObject ColumnLocationName = new ViewObject(1, "LocationName", "$Views_LawStoreLocations_LocationName");

        /// <summary>
        ///     ID:2 
        ///     Alias: LocationByDefault
        ///     Caption: $Views_LawStoreLocations_LocationByDefault.
        /// </summary>
        public readonly ViewObject ColumnLocationByDefault = new ViewObject(2, "LocationByDefault", "$Views_LawStoreLocations_LocationByDefault");

        /// <summary>
        ///     ID:3 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(3, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_LawStoreLocations_LocationName.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_LawStoreLocations_LocationName");

        /// <summary>
        ///     ID:1 
        ///     Alias: ByDefault
        ///     Caption: $Views_LawStoreLocations_LocationByDefault.
        /// </summary>
        public readonly ViewObject ParamByDefault = new ViewObject(1, "ByDefault", "$Views_LawStoreLocations_LocationByDefault");

        #endregion

        #region ToString

        public static implicit operator string(LawStoreLocationsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LawUsers

    /// <summary>
    ///     ID: {04b79978-1ad2-4ecd-9326-34d259d6ea34}
    ///     Alias: LawUsers
    ///     Caption: $Views_Names_LawUsers
    ///     Group: LawDictionary
    /// </summary>
    public class LawUsersViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LawUsers": {04b79978-1ad2-4ecd-9326-34d259d6ea34}.
        /// </summary>
        public readonly Guid ID = new Guid(0x04b79978,0x1ad2,0x4ecd,0x93,0x26,0x34,0xd2,0x59,0xd6,0xea,0x34);

        /// <summary>
        ///     View name for "LawUsers".
        /// </summary>
        public readonly string Alias = "LawUsers";

        /// <summary>
        ///     View caption for "LawUsers".
        /// </summary>
        public readonly string Caption = "$Views_Names_LawUsers";

        /// <summary>
        ///     View group for "LawUsers".
        /// </summary>
        public readonly string Group = "LawDictionary";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(0, "UserID");

        /// <summary>
        ///     ID:1 
        ///     Alias: UserSurname
        ///     Caption: $Views_LawUsers_UserSurname.
        /// </summary>
        public readonly ViewObject ColumnUserSurname = new ViewObject(1, "UserSurname", "$Views_LawUsers_UserSurname");

        /// <summary>
        ///     ID:2 
        ///     Alias: UserName
        ///     Caption: $Views_LawUsers_UserName.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(2, "UserName", "$Views_LawUsers_UserName");

        /// <summary>
        ///     ID:3 
        ///     Alias: UserWorkplace
        ///     Caption: $Views_LawUsers_UserWorkplace.
        /// </summary>
        public readonly ViewObject ColumnUserWorkplace = new ViewObject(3, "UserWorkplace", "$Views_LawUsers_UserWorkplace");

        /// <summary>
        ///     ID:4 
        ///     Alias: UserUserName
        ///     Caption: $Views_LawUsers_UserUserName.
        /// </summary>
        public readonly ViewObject ColumnUserUserName = new ViewObject(4, "UserUserName", "$Views_LawUsers_UserUserName");

        /// <summary>
        ///     ID:5 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(5, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Surname
        ///     Caption: $Views_LawUsers_UserSurname.
        /// </summary>
        public readonly ViewObject ParamSurname = new ViewObject(0, "Surname", "$Views_LawUsers_UserSurname");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_LawUsers_UserName.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_LawUsers_UserName");

        #endregion

        #region ToString

        public static implicit operator string(LawUsersViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LicenseTypes

    /// <summary>
    ///     ID: {613dd133-9ac2-4cae-851f-c417fe657ec4}
    ///     Alias: LicenseTypes
    ///     Caption: $Views_Names_LicenseTypes
    ///     Group: System
    /// </summary>
    public class LicenseTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LicenseTypes": {613dd133-9ac2-4cae-851f-c417fe657ec4}.
        /// </summary>
        public readonly Guid ID = new Guid(0x613dd133,0x9ac2,0x4cae,0x85,0x1f,0xc4,0x17,0xfe,0x65,0x7e,0xc4);

        /// <summary>
        ///     View name for "LicenseTypes".
        /// </summary>
        public readonly string Alias = "LicenseTypes";

        /// <summary>
        ///     View caption for "LicenseTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_LicenseTypes";

        /// <summary>
        ///     View group for "LicenseTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: $Views_LicenseTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "$Views_LicenseTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_LicenseTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_LicenseTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(LicenseTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LinkedDocuments

    /// <summary>
    ///     ID: {88069520-441a-4f38-a103-c8f2ac8a2101}
    ///     Alias: LinkedDocuments
    ///     Caption: $Views_Names_LinkedDocuments
    ///     Group: KrDocuments
    /// </summary>
    public class LinkedDocumentsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LinkedDocuments": {88069520-441a-4f38-a103-c8f2ac8a2101}.
        /// </summary>
        public readonly Guid ID = new Guid(0x88069520,0x441a,0x4f38,0xa1,0x03,0xc8,0xf2,0xac,0x8a,0x21,0x01);

        /// <summary>
        ///     View name for "LinkedDocuments".
        /// </summary>
        public readonly string Alias = "LinkedDocuments";

        /// <summary>
        ///     View caption for "LinkedDocuments".
        /// </summary>
        public readonly string Caption = "$Views_Names_LinkedDocuments";

        /// <summary>
        ///     View group for "LinkedDocuments".
        /// </summary>
        public readonly string Group = "KrDocuments";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DocID.
        /// </summary>
        public readonly ViewObject ColumnDocID = new ViewObject(0, "DocID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DocNumber
        ///     Caption: $Views_Registers_Number.
        /// </summary>
        public readonly ViewObject ColumnDocNumber = new ViewObject(1, "DocNumber", "$Views_Registers_Number");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeName
        ///     Caption: $Views_Registers_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(2, "TypeName", "$Views_Registers_Type");

        /// <summary>
        ///     ID:3 
        ///     Alias: DocSubject
        ///     Caption: $Views_Registers_Subject.
        /// </summary>
        public readonly ViewObject ColumnDocSubject = new ViewObject(3, "DocSubject", "$Views_Registers_Subject");

        /// <summary>
        ///     ID:4 
        ///     Alias: DocDescription
        ///     Caption: $Views_Registers_DocDescription.
        /// </summary>
        public readonly ViewObject ColumnDocDescription = new ViewObject(4, "DocDescription", "$Views_Registers_DocDescription");

        /// <summary>
        ///     ID:5 
        ///     Alias: PartnerID.
        /// </summary>
        public readonly ViewObject ColumnPartnerID = new ViewObject(5, "PartnerID");

        /// <summary>
        ///     ID:6 
        ///     Alias: PartnerName
        ///     Caption: $Views_Registers_Partner.
        /// </summary>
        public readonly ViewObject ColumnPartnerName = new ViewObject(6, "PartnerName", "$Views_Registers_Partner");

        /// <summary>
        ///     ID:7 
        ///     Alias: AuthorID.
        /// </summary>
        public readonly ViewObject ColumnAuthorID = new ViewObject(7, "AuthorID");

        /// <summary>
        ///     ID:8 
        ///     Alias: AuthorName
        ///     Caption: $Views_Registers_Author.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(8, "AuthorName", "$Views_Registers_Author");

        /// <summary>
        ///     ID:9 
        ///     Alias: RegistratorID.
        /// </summary>
        public readonly ViewObject ColumnRegistratorID = new ViewObject(9, "RegistratorID");

        /// <summary>
        ///     ID:10 
        ///     Alias: RegistratorName
        ///     Caption: $Views_Registers_Registrator.
        /// </summary>
        public readonly ViewObject ColumnRegistratorName = new ViewObject(10, "RegistratorName", "$Views_Registers_Registrator");

        /// <summary>
        ///     ID:11 
        ///     Alias: DocDate
        ///     Caption: $Views_Registers_DocDate.
        /// </summary>
        public readonly ViewObject ColumnDocDate = new ViewObject(11, "DocDate", "$Views_Registers_DocDate");

        /// <summary>
        ///     ID:12 
        ///     Alias: CreationDate
        ///     Caption: $Views_Registers_CreationDate.
        /// </summary>
        public readonly ViewObject ColumnCreationDate = new ViewObject(12, "CreationDate", "$Views_Registers_CreationDate");

        /// <summary>
        ///     ID:13 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department.
        /// </summary>
        public readonly ViewObject ColumnDepartment = new ViewObject(13, "Department", "$Views_Registers_Department");

        /// <summary>
        ///     ID:14 
        ///     Alias: KrState
        ///     Caption: $Views_Registers_State.
        /// </summary>
        public readonly ViewObject ColumnKrState = new ViewObject(14, "KrState", "$Views_Registers_State");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Partner
        ///     Caption: $Views_Registers_Partner_Param.
        /// </summary>
        public readonly ViewObject ParamPartner = new ViewObject(0, "Partner", "$Views_Registers_Partner_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Number
        ///     Caption: $Views_Registers_Number_Param.
        /// </summary>
        public readonly ViewObject ParamNumber = new ViewObject(1, "Number", "$Views_Registers_Number_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Subject
        ///     Caption: $Views_Registers_Subject_Param.
        /// </summary>
        public readonly ViewObject ParamSubject = new ViewObject(2, "Subject", "$Views_Registers_Subject_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: DocDate
        ///     Caption: $Views_Registers_DocDate_Param.
        /// </summary>
        public readonly ViewObject ParamDocDate = new ViewObject(3, "DocDate", "$Views_Registers_DocDate_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Author
        ///     Caption: $Views_Registers_Author_Param.
        /// </summary>
        public readonly ViewObject ParamAuthor = new ViewObject(4, "Author", "$Views_Registers_Author_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Registrator
        ///     Caption: $Views_Registers_Registrator_Param.
        /// </summary>
        public readonly ViewObject ParamRegistrator = new ViewObject(5, "Registrator", "$Views_Registers_Registrator_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: State
        ///     Caption: $Views_Registers_State_Param.
        /// </summary>
        public readonly ViewObject ParamState = new ViewObject(6, "State", "$Views_Registers_State_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: Type
        ///     Caption: $Views_Registers_Type_Param.
        /// </summary>
        public readonly ViewObject ParamType = new ViewObject(7, "Type", "$Views_Registers_Type_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(8, "Department", "$Views_Registers_Department_Param");

        /// <summary>
        ///     ID:9 
        ///     Alias: CreationDate
        ///     Caption: $Views_Registers_CreationDate_Param.
        /// </summary>
        public readonly ViewObject ParamCreationDate = new ViewObject(9, "CreationDate", "$Views_Registers_CreationDate_Param");

        /// <summary>
        ///     ID:10 
        ///     Alias: LinkedDocID
        ///     Caption: LinkedDocID.
        /// </summary>
        public readonly ViewObject ParamLinkedDocID = new ViewObject(10, "LinkedDocID", "LinkedDocID");

        #endregion

        #region ToString

        public static implicit operator string(LinkedDocumentsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region LoginTypes

    /// <summary>
    ///     ID: {b0afaa90-23f1-4c2d-a85d-54506e027745}
    ///     Alias: LoginTypes
    ///     Caption: $Views_Names_LoginTypes
    ///     Group: System
    /// </summary>
    public class LoginTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "LoginTypes": {b0afaa90-23f1-4c2d-a85d-54506e027745}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb0afaa90,0x23f1,0x4c2d,0xa8,0x5d,0x54,0x50,0x6e,0x02,0x77,0x45);

        /// <summary>
        ///     View name for "LoginTypes".
        /// </summary>
        public readonly string Alias = "LoginTypes";

        /// <summary>
        ///     View caption for "LoginTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_LoginTypes";

        /// <summary>
        ///     View group for "LoginTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: $Views_LoginTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "$Views_LoginTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_LoginTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_LoginTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(LoginTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region MyAcquaintanceHistory

    /// <summary>
    ///     ID: {ef66bde2-1126-4c09-9f4e-56d710ccda40}
    ///     Alias: MyAcquaintanceHistory
    ///     Caption: $Views_Names_MyAcquaintanceHistory
    ///     Group: Acquaintance
    /// </summary>
    public class MyAcquaintanceHistoryViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "MyAcquaintanceHistory": {ef66bde2-1126-4c09-9f4e-56d710ccda40}.
        /// </summary>
        public readonly Guid ID = new Guid(0xef66bde2,0x1126,0x4c09,0x9f,0x4e,0x56,0xd7,0x10,0xcc,0xda,0x40);

        /// <summary>
        ///     View name for "MyAcquaintanceHistory".
        /// </summary>
        public readonly string Alias = "MyAcquaintanceHistory";

        /// <summary>
        ///     View caption for "MyAcquaintanceHistory".
        /// </summary>
        public readonly string Caption = "$Views_Names_MyAcquaintanceHistory";

        /// <summary>
        ///     View group for "MyAcquaintanceHistory".
        /// </summary>
        public readonly string Group = "Acquaintance";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ID.
        /// </summary>
        public readonly ViewObject ColumnID = new ViewObject(0, "ID");

        /// <summary>
        ///     ID:1 
        ///     Alias: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(1, "CardID");

        /// <summary>
        ///     ID:2 
        ///     Alias: CardNumber
        ///     Caption: $Views_Registers_Number.
        /// </summary>
        public readonly ViewObject ColumnCardNumber = new ViewObject(2, "CardNumber", "$Views_Registers_Number");

        /// <summary>
        ///     ID:3 
        ///     Alias: SenderID.
        /// </summary>
        public readonly ViewObject ColumnSenderID = new ViewObject(3, "SenderID");

        /// <summary>
        ///     ID:4 
        ///     Alias: SenderName
        ///     Caption: $Views_Acquaintance_Sender.
        /// </summary>
        public readonly ViewObject ColumnSenderName = new ViewObject(4, "SenderName", "$Views_Acquaintance_Sender");

        /// <summary>
        ///     ID:5 
        ///     Alias: Sent
        ///     Caption: $Views_Acquaintance_SentDate.
        /// </summary>
        public readonly ViewObject ColumnSent = new ViewObject(5, "Sent", "$Views_Acquaintance_SentDate");

        /// <summary>
        ///     ID:6 
        ///     Alias: IsReceived.
        /// </summary>
        public readonly ViewObject ColumnIsReceived = new ViewObject(6, "IsReceived");

        /// <summary>
        ///     ID:7 
        ///     Alias: IsReceivedString
        ///     Caption: $Views_Acquaintance_State.
        /// </summary>
        public readonly ViewObject ColumnIsReceivedString = new ViewObject(7, "IsReceivedString", "$Views_Acquaintance_State");

        /// <summary>
        ///     ID:8 
        ///     Alias: Received
        ///     Caption: $Views_Acquaintance_ReceivedDate.
        /// </summary>
        public readonly ViewObject ColumnReceived = new ViewObject(8, "Received", "$Views_Acquaintance_ReceivedDate");

        /// <summary>
        ///     ID:9 
        ///     Alias: Comment
        ///     Caption: $Views_Acquaintance_CommentColumn.
        /// </summary>
        public readonly ViewObject ColumnComment = new ViewObject(9, "Comment", "$Views_Acquaintance_CommentColumn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: SenderParam
        ///     Caption: $Views_Acquaintance_Sender.
        /// </summary>
        public readonly ViewObject ParamSenderParam = new ViewObject(0, "SenderParam", "$Views_Acquaintance_Sender");

        /// <summary>
        ///     ID:1 
        ///     Alias: IsReceivedParam
        ///     Caption: $Views_Acquaintance_State.
        /// </summary>
        public readonly ViewObject ParamIsReceivedParam = new ViewObject(1, "IsReceivedParam", "$Views_Acquaintance_State");

        /// <summary>
        ///     ID:2 
        ///     Alias: SentParam
        ///     Caption: $Views_Acquaintance_SentDate.
        /// </summary>
        public readonly ViewObject ParamSentParam = new ViewObject(2, "SentParam", "$Views_Acquaintance_SentDate");

        /// <summary>
        ///     ID:3 
        ///     Alias: ReceivedParam
        ///     Caption: $Views_Acquaintance_ReceivedDate.
        /// </summary>
        public readonly ViewObject ParamReceivedParam = new ViewObject(3, "ReceivedParam", "$Views_Acquaintance_ReceivedDate");

        /// <summary>
        ///     ID:4 
        ///     Alias: CommentParam
        ///     Caption: $Views_Acquaintance_CommentParam.
        /// </summary>
        public readonly ViewObject ParamCommentParam = new ViewObject(4, "CommentParam", "$Views_Acquaintance_CommentParam");

        #endregion

        #region ToString

        public static implicit operator string(MyAcquaintanceHistoryViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region MyCompletedTasks

    /// <summary>
    ///     ID: {89cf35b0-69bc-406a-9b95-77be879a94fa}
    ///     Alias: MyCompletedTasks
    ///     Caption: $Views_Names_MyCompletedTasks
    ///     Group: System
    /// </summary>
    public class MyCompletedTasksViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "MyCompletedTasks": {89cf35b0-69bc-406a-9b95-77be879a94fa}.
        /// </summary>
        public readonly Guid ID = new Guid(0x89cf35b0,0x69bc,0x406a,0x9b,0x95,0x77,0xbe,0x87,0x9a,0x94,0xfa);

        /// <summary>
        ///     View name for "MyCompletedTasks".
        /// </summary>
        public readonly string Alias = "MyCompletedTasks";

        /// <summary>
        ///     View caption for "MyCompletedTasks".
        /// </summary>
        public readonly string Caption = "$Views_Names_MyCompletedTasks";

        /// <summary>
        ///     View group for "MyCompletedTasks".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(0, "CardID");

        /// <summary>
        ///     ID:1 
        ///     Alias: CardName
        ///     Caption: $Views_CompletedTasks_Card.
        /// </summary>
        public readonly ViewObject ColumnCardName = new ViewObject(1, "CardName", "$Views_CompletedTasks_Card");

        /// <summary>
        ///     ID:2 
        ///     Alias: CardTypeCaption
        ///     Caption: $Views_CompletedTasks_CardType.
        /// </summary>
        public readonly ViewObject ColumnCardTypeCaption = new ViewObject(2, "CardTypeCaption", "$Views_CompletedTasks_CardType");

        /// <summary>
        ///     ID:3 
        ///     Alias: Subject
        ///     Caption: $Views_Registers_Subject.
        /// </summary>
        public readonly ViewObject ColumnSubject = new ViewObject(3, "Subject", "$Views_Registers_Subject");

        /// <summary>
        ///     ID:4 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(4, "TypeID");

        /// <summary>
        ///     ID:5 
        ///     Alias: TypeCaption
        ///     Caption: $Views_CompletedTasks_TaskType.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(5, "TypeCaption", "$Views_CompletedTasks_TaskType");

        /// <summary>
        ///     ID:6 
        ///     Alias: OptionID.
        /// </summary>
        public readonly ViewObject ColumnOptionID = new ViewObject(6, "OptionID");

        /// <summary>
        ///     ID:7 
        ///     Alias: OptionCaption
        ///     Caption: $Views_CompletedTasks_CompletionOption.
        /// </summary>
        public readonly ViewObject ColumnOptionCaption = new ViewObject(7, "OptionCaption", "$Views_CompletedTasks_CompletionOption");

        /// <summary>
        ///     ID:8 
        ///     Alias: Result
        ///     Caption: $Views_CompletedTasks_Result.
        /// </summary>
        public readonly ViewObject ColumnResult = new ViewObject(8, "Result", "$Views_CompletedTasks_Result");

        /// <summary>
        ///     ID:9 
        ///     Alias: RoleID.
        /// </summary>
        public readonly ViewObject ColumnRoleID = new ViewObject(9, "RoleID");

        /// <summary>
        ///     ID:10 
        ///     Alias: RoleName
        ///     Caption: $Views_CompletedTasks_Role.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(10, "RoleName", "$Views_CompletedTasks_Role");

        /// <summary>
        ///     ID:11 
        ///     Alias: AuthorID.
        /// </summary>
        public readonly ViewObject ColumnAuthorID = new ViewObject(11, "AuthorID");

        /// <summary>
        ///     ID:12 
        ///     Alias: AuthorName
        ///     Caption: $Views_CompletedTasks_Author.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(12, "AuthorName", "$Views_CompletedTasks_Author");

        /// <summary>
        ///     ID:13 
        ///     Alias: Completed
        ///     Caption: $Views_CompletedTasks_Completed.
        /// </summary>
        public readonly ViewObject ColumnCompleted = new ViewObject(13, "Completed", "$Views_CompletedTasks_Completed");

        /// <summary>
        ///     ID:14 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(14, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: FromMeMode
        ///     Caption: $Views_CompletedTasks_FromMeMode_Param.
        /// </summary>
        public readonly ViewObject ParamFromMeMode = new ViewObject(0, "FromMeMode", "$Views_CompletedTasks_FromMeMode_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: CompletionDate
        ///     Caption: $Views_CompletedTasks_CompletionDate_Param.
        /// </summary>
        public readonly ViewObject ParamCompletionDate = new ViewObject(1, "CompletionDate", "$Views_CompletedTasks_CompletionDate_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeParam
        ///     Caption: $Views_CompletedTasks_CardType_Param.
        /// </summary>
        public readonly ViewObject ParamTypeParam = new ViewObject(2, "TypeParam", "$Views_CompletedTasks_CardType_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: TaskType
        ///     Caption: $Views_CompletedTasks_TaskType_Param.
        /// </summary>
        public readonly ViewObject ParamTaskType = new ViewObject(3, "TaskType", "$Views_CompletedTasks_TaskType_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Role
        ///     Caption: $Views_CompletedTasks_RoleGroup_Param.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(4, "Role", "$Views_CompletedTasks_RoleGroup_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Option
        ///     Caption: $Views_CompletedTasks_CompletionOption_Param.
        /// </summary>
        public readonly ViewObject ParamOption = new ViewObject(5, "Option", "$Views_CompletedTasks_CompletionOption_Param");

        #endregion

        #region ToString

        public static implicit operator string(MyCompletedTasksViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region MyDocuments

    /// <summary>
    ///     ID: {198282c6-a340-472f-97fb-e8895c3cc3ca}
    ///     Alias: MyDocuments
    ///     Caption: $Views_Names_MyDocuments
    ///     Group: KrDocuments
    /// </summary>
    public class MyDocumentsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "MyDocuments": {198282c6-a340-472f-97fb-e8895c3cc3ca}.
        /// </summary>
        public readonly Guid ID = new Guid(0x198282c6,0xa340,0x472f,0x97,0xfb,0xe8,0x89,0x5c,0x3c,0xc3,0xca);

        /// <summary>
        ///     View name for "MyDocuments".
        /// </summary>
        public readonly string Alias = "MyDocuments";

        /// <summary>
        ///     View caption for "MyDocuments".
        /// </summary>
        public readonly string Caption = "$Views_Names_MyDocuments";

        /// <summary>
        ///     View group for "MyDocuments".
        /// </summary>
        public readonly string Group = "KrDocuments";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DocID.
        /// </summary>
        public readonly ViewObject ColumnDocID = new ViewObject(0, "DocID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DocNumber
        ///     Caption: $Views_Registers_Number.
        /// </summary>
        public readonly ViewObject ColumnDocNumber = new ViewObject(1, "DocNumber", "$Views_Registers_Number");

        /// <summary>
        ///     ID:2 
        ///     Alias: DocSubject
        ///     Caption: $Views_Registers_Subject.
        /// </summary>
        public readonly ViewObject ColumnDocSubject = new ViewObject(2, "DocSubject", "$Views_Registers_Subject");

        /// <summary>
        ///     ID:3 
        ///     Alias: DocDescription
        ///     Caption: $Views_Registers_DocDescription.
        /// </summary>
        public readonly ViewObject ColumnDocDescription = new ViewObject(3, "DocDescription", "$Views_Registers_DocDescription");

        /// <summary>
        ///     ID:4 
        ///     Alias: AuthorID.
        /// </summary>
        public readonly ViewObject ColumnAuthorID = new ViewObject(4, "AuthorID");

        /// <summary>
        ///     ID:5 
        ///     Alias: AuthorName
        ///     Caption: $Views_Registers_Author.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(5, "AuthorName", "$Views_Registers_Author");

        /// <summary>
        ///     ID:6 
        ///     Alias: RegistratorID.
        /// </summary>
        public readonly ViewObject ColumnRegistratorID = new ViewObject(6, "RegistratorID");

        /// <summary>
        ///     ID:7 
        ///     Alias: RegistratorName
        ///     Caption: $Views_Registers_Registrator.
        /// </summary>
        public readonly ViewObject ColumnRegistratorName = new ViewObject(7, "RegistratorName", "$Views_Registers_Registrator");

        /// <summary>
        ///     ID:8 
        ///     Alias: KrState
        ///     Caption: $Views_Registers_State.
        /// </summary>
        public readonly ViewObject ColumnKrState = new ViewObject(8, "KrState", "$Views_Registers_State");

        /// <summary>
        ///     ID:9 
        ///     Alias: KrStateModified
        ///     Caption: $Views_Registers_StateModified.
        /// </summary>
        public readonly ViewObject ColumnKrStateModified = new ViewObject(9, "KrStateModified", "$Views_Registers_StateModified");

        /// <summary>
        ///     ID:10 
        ///     Alias: DocDate
        ///     Caption: $Views_Registers_DocDate.
        /// </summary>
        public readonly ViewObject ColumnDocDate = new ViewObject(10, "DocDate", "$Views_Registers_DocDate");

        /// <summary>
        ///     ID:11 
        ///     Alias: CreationDate
        ///     Caption: $Views_Registers_CreationDate.
        /// </summary>
        public readonly ViewObject ColumnCreationDate = new ViewObject(11, "CreationDate", "$Views_Registers_CreationDate");

        /// <summary>
        ///     ID:12 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department.
        /// </summary>
        public readonly ViewObject ColumnDepartment = new ViewObject(12, "Department", "$Views_Registers_Department");

        /// <summary>
        ///     ID:13 
        ///     Alias: ApprovedBy
        ///     Caption: $Views_Registers_ApprovedBy.
        /// </summary>
        public readonly ViewObject ColumnApprovedBy = new ViewObject(13, "ApprovedBy", "$Views_Registers_ApprovedBy");

        /// <summary>
        ///     ID:14 
        ///     Alias: DisapprovedBy
        ///     Caption: $Views_Registers_DisapprovedBy.
        /// </summary>
        public readonly ViewObject ColumnDisapprovedBy = new ViewObject(14, "DisapprovedBy", "$Views_Registers_DisapprovedBy");

        /// <summary>
        ///     ID:15 
        ///     Alias: Background.
        /// </summary>
        public readonly ViewObject ColumnBackground = new ViewObject(15, "Background");

        /// <summary>
        ///     ID:16 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(16, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: IsAuthor
        ///     Caption: $Views_Registers_IsAuthor_Param.
        /// </summary>
        public readonly ViewObject ParamIsAuthor = new ViewObject(0, "IsAuthor", "$Views_Registers_IsAuthor_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: IsInitiator
        ///     Caption: $Views_Registers_IsInitiator_Param.
        /// </summary>
        public readonly ViewObject ParamIsInitiator = new ViewObject(1, "IsInitiator", "$Views_Registers_IsInitiator_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: IsRegistrator
        ///     Caption: $Views_Registers_IsRegistrator_Param.
        /// </summary>
        public readonly ViewObject ParamIsRegistrator = new ViewObject(2, "IsRegistrator", "$Views_Registers_IsRegistrator_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Number
        ///     Caption: $Views_Registers_Number_Param.
        /// </summary>
        public readonly ViewObject ParamNumber = new ViewObject(3, "Number", "$Views_Registers_Number_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Subject
        ///     Caption: $Views_Registers_Subject_Param.
        /// </summary>
        public readonly ViewObject ParamSubject = new ViewObject(4, "Subject", "$Views_Registers_Subject_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: DocDate
        ///     Caption: $Views_Registers_DocDate_Param.
        /// </summary>
        public readonly ViewObject ParamDocDate = new ViewObject(5, "DocDate", "$Views_Registers_DocDate_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Author
        ///     Caption: $Views_Registers_Author_Param.
        /// </summary>
        public readonly ViewObject ParamAuthor = new ViewObject(6, "Author", "$Views_Registers_Author_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: Registrator
        ///     Caption: $Views_Registers_Registrator_Param.
        /// </summary>
        public readonly ViewObject ParamRegistrator = new ViewObject(7, "Registrator", "$Views_Registers_Registrator_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: State
        ///     Caption: $Views_Registers_State_Param.
        /// </summary>
        public readonly ViewObject ParamState = new ViewObject(8, "State", "$Views_Registers_State_Param");

        /// <summary>
        ///     ID:9 
        ///     Alias: Type
        ///     Caption: $Views_Registers_Type_Param.
        /// </summary>
        public readonly ViewObject ParamType = new ViewObject(9, "Type", "$Views_Registers_Type_Param");

        /// <summary>
        ///     ID:10 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(10, "Department", "$Views_Registers_Department_Param");

        #endregion

        #region ToString

        public static implicit operator string(MyDocumentsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region MyTags

    /// <summary>
    ///     ID: {b206c96c-de91-4bc5-aa55-d00bf7eb9604}
    ///     Alias: MyTags
    ///     Caption: $Views_Names_MyTags
    ///     Group: Tags
    /// </summary>
    public class MyTagsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "MyTags": {b206c96c-de91-4bc5-aa55-d00bf7eb9604}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb206c96c,0xde91,0x4bc5,0xaa,0x55,0xd0,0x0b,0xf7,0xeb,0x96,0x04);

        /// <summary>
        ///     View name for "MyTags".
        /// </summary>
        public readonly string Alias = "MyTags";

        /// <summary>
        ///     View caption for "MyTags".
        /// </summary>
        public readonly string Caption = "$Views_Names_MyTags";

        /// <summary>
        ///     View group for "MyTags".
        /// </summary>
        public readonly string Group = "Tags";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TagID.
        /// </summary>
        public readonly ViewObject ColumnTagID = new ViewObject(0, "TagID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TagName
        ///     Caption: $Views_Tags_Name.
        /// </summary>
        public readonly ViewObject ColumnTagName = new ViewObject(1, "TagName", "$Views_Tags_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: TagBackground
        ///     Caption: $CardTypes_Controls_Background.
        /// </summary>
        public readonly ViewObject ColumnTagBackground = new ViewObject(2, "TagBackground", "$CardTypes_Controls_Background");

        /// <summary>
        ///     ID:3 
        ///     Alias: TagForeground
        ///     Caption: $CardTypes_Controls_Foreground.
        /// </summary>
        public readonly ViewObject ColumnTagForeground = new ViewObject(3, "TagForeground", "$CardTypes_Controls_Foreground");

        /// <summary>
        ///     ID:4 
        ///     Alias: TagIsCommon
        ///     Caption: $Tags_IsCommon.
        /// </summary>
        public readonly ViewObject ColumnTagIsCommon = new ViewObject(4, "TagIsCommon", "$Tags_IsCommon");

        /// <summary>
        ///     ID:5 
        ///     Alias: TagCanEdit.
        /// </summary>
        public readonly ViewObject ColumnTagCanEdit = new ViewObject(5, "TagCanEdit");

        /// <summary>
        ///     ID:6 
        ///     Alias: TagCanUse.
        /// </summary>
        public readonly ViewObject ColumnTagCanUse = new ViewObject(6, "TagCanUse");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Tags_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Tags_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(MyTagsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region MyTasks

    /// <summary>
    ///     ID: {d249d321-c5a3-4847-951a-f47ffcf5509d}
    ///     Alias: MyTasks
    ///     Caption: $Views_Names_MyTasks
    ///     Group: System
    /// </summary>
    public class MyTasksViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "MyTasks": {d249d321-c5a3-4847-951a-f47ffcf5509d}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd249d321,0xc5a3,0x4847,0x95,0x1a,0xf4,0x7f,0xfc,0xf5,0x50,0x9d);

        /// <summary>
        ///     View name for "MyTasks".
        /// </summary>
        public readonly string Alias = "MyTasks";

        /// <summary>
        ///     View caption for "MyTasks".
        /// </summary>
        public readonly string Caption = "$Views_Names_MyTasks";

        /// <summary>
        ///     View group for "MyTasks".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(0, "CardID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TaskRowID.
        /// </summary>
        public readonly ViewObject ColumnTaskRowID = new ViewObject(1, "TaskRowID");

        /// <summary>
        ///     ID:2 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(2, "rn");

        /// <summary>
        ///     ID:3 
        ///     Alias: StateID.
        /// </summary>
        public readonly ViewObject ColumnStateID = new ViewObject(3, "StateID");

        /// <summary>
        ///     ID:4 
        ///     Alias: StateName
        ///     Caption: $Views_MyTasks_State.
        /// </summary>
        public readonly ViewObject ColumnStateName = new ViewObject(4, "StateName", "$Views_MyTasks_State");

        /// <summary>
        ///     ID:5 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(5, "TypeID");

        /// <summary>
        ///     ID:6 
        ///     Alias: PlannedDate
        ///     Caption: $Views_MyTasks_Planned.
        /// </summary>
        public readonly ViewObject ColumnPlannedDate = new ViewObject(6, "PlannedDate", "$Views_MyTasks_Planned");

        /// <summary>
        ///     ID:7 
        ///     Alias: TaskInfo
        ///     Caption: $Views_MyTasks_Info.
        /// </summary>
        public readonly ViewObject ColumnTaskInfo = new ViewObject(7, "TaskInfo", "$Views_MyTasks_Info");

        /// <summary>
        ///     ID:8 
        ///     Alias: RoleID.
        /// </summary>
        public readonly ViewObject ColumnRoleID = new ViewObject(8, "RoleID");

        /// <summary>
        ///     ID:9 
        ///     Alias: RoleName
        ///     Caption: $Views_MyTasks_Performer.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(9, "RoleName", "$Views_MyTasks_Performer");

        /// <summary>
        ///     ID:10 
        ///     Alias: AuthorID.
        /// </summary>
        public readonly ViewObject ColumnAuthorID = new ViewObject(10, "AuthorID");

        /// <summary>
        ///     ID:11 
        ///     Alias: AuthorName
        ///     Caption: $Views_MyTasks_Author.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(11, "AuthorName", "$Views_MyTasks_Author");

        /// <summary>
        ///     ID:12 
        ///     Alias: AuthorDeptID.
        /// </summary>
        public readonly ViewObject ColumnAuthorDeptID = new ViewObject(12, "AuthorDeptID");

        /// <summary>
        ///     ID:13 
        ///     Alias: AuthorDeptName
        ///     Caption: $Views_MyTasks_AuthorDepartment.
        /// </summary>
        public readonly ViewObject ColumnAuthorDeptName = new ViewObject(13, "AuthorDeptName", "$Views_MyTasks_AuthorDepartment");

        /// <summary>
        ///     ID:14 
        ///     Alias: ModificationTime
        ///     Caption: $Views_MyTasks_Modified.
        /// </summary>
        public readonly ViewObject ColumnModificationTime = new ViewObject(14, "ModificationTime", "$Views_MyTasks_Modified");

        /// <summary>
        ///     ID:15 
        ///     Alias: Created.
        /// </summary>
        public readonly ViewObject ColumnCreated = new ViewObject(15, "Created");

        /// <summary>
        ///     ID:16 
        ///     Alias: CreatedByID.
        /// </summary>
        public readonly ViewObject ColumnCreatedByID = new ViewObject(16, "CreatedByID");

        /// <summary>
        ///     ID:17 
        ///     Alias: CreatedByName.
        /// </summary>
        public readonly ViewObject ColumnCreatedByName = new ViewObject(17, "CreatedByName");

        /// <summary>
        ///     ID:18 
        ///     Alias: TimeZoneUtcOffsetMinutes.
        /// </summary>
        public readonly ViewObject ColumnTimeZoneUtcOffsetMinutes = new ViewObject(18, "TimeZoneUtcOffsetMinutes");

        /// <summary>
        ///     ID:19 
        ///     Alias: RoleTypeID.
        /// </summary>
        public readonly ViewObject ColumnRoleTypeID = new ViewObject(19, "RoleTypeID");

        /// <summary>
        ///     ID:20 
        ///     Alias: CardName
        ///     Caption: $Views_MyTasks_Card.
        /// </summary>
        public readonly ViewObject ColumnCardName = new ViewObject(20, "CardName", "$Views_MyTasks_Card");

        /// <summary>
        ///     ID:21 
        ///     Alias: CardTypeID.
        /// </summary>
        public readonly ViewObject ColumnCardTypeID = new ViewObject(21, "CardTypeID");

        /// <summary>
        ///     ID:22 
        ///     Alias: CardTypeName
        ///     Caption: $Views_MyTasks_CardType.
        /// </summary>
        public readonly ViewObject ColumnCardTypeName = new ViewObject(22, "CardTypeName", "$Views_MyTasks_CardType");

        /// <summary>
        ///     ID:23 
        ///     Alias: TypeCaption
        ///     Caption: $Views_MyTasks_TaskType.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(23, "TypeCaption", "$Views_MyTasks_TaskType");

        /// <summary>
        ///     ID:24 
        ///     Alias: TimeToCompletion
        ///     Caption: $Views_MyTasks_TimeToCompletion.
        /// </summary>
        public readonly ViewObject ColumnTimeToCompletion = new ViewObject(24, "TimeToCompletion", "$Views_MyTasks_TimeToCompletion");

        /// <summary>
        ///     ID:25 
        ///     Alias: CalendarID.
        /// </summary>
        public readonly ViewObject ColumnCalendarID = new ViewObject(25, "CalendarID");

        /// <summary>
        ///     ID:26 
        ///     Alias: QuantsToFinish.
        /// </summary>
        public readonly ViewObject ColumnQuantsToFinish = new ViewObject(26, "QuantsToFinish");

        /// <summary>
        ///     ID:27 
        ///     Alias: AppearanceColumn.
        /// </summary>
        public readonly ViewObject ColumnAppearanceColumn = new ViewObject(27, "AppearanceColumn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Status
        ///     Caption: $Views_MyTasks_State_Param.
        /// </summary>
        public readonly ViewObject ParamStatus = new ViewObject(0, "Status", "$Views_MyTasks_State_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: TaskType
        ///     Caption: $Views_MyTasks_TaskType_Param.
        /// </summary>
        public readonly ViewObject ParamTaskType = new ViewObject(1, "TaskType", "$Views_MyTasks_TaskType_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: TaskTypeGrouped
        ///     Caption: $Views_MyTasks_TaskTypeGrouped_Param.
        /// </summary>
        public readonly ViewObject ParamTaskTypeGrouped = new ViewObject(2, "TaskTypeGrouped", "$Views_MyTasks_TaskTypeGrouped_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: AuthorDepartment
        ///     Caption: $Views_MyTasks_AuthorDepartment_Param.
        /// </summary>
        public readonly ViewObject ParamAuthorDepartment = new ViewObject(3, "AuthorDepartment", "$Views_MyTasks_AuthorDepartment_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: TaskDateDueInterval
        ///     Caption: $Views_MyTasks_TaskDateDueInterval_Param.
        /// </summary>
        public readonly ViewObject ParamTaskDateDueInterval = new ViewObject(4, "TaskDateDueInterval", "$Views_MyTasks_TaskDateDueInterval_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: User
        ///     Caption: $Views_MyTasks_User_Param.
        /// </summary>
        public readonly ViewObject ParamUser = new ViewObject(5, "User", "$Views_MyTasks_User_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Role
        ///     Caption: $Views_MyTasks_Role_Param.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(6, "Role", "$Views_MyTasks_Role_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: DeputyMode
        ///     Caption: $Views_MyTasks_Deputy_Param.
        /// </summary>
        public readonly ViewObject ParamDeputyMode = new ViewObject(7, "DeputyMode", "$Views_MyTasks_Deputy_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: InWorkNotByMe
        ///     Caption: $Views_MyTasks_InWorkNotMe_Param.
        /// </summary>
        public readonly ViewObject ParamInWorkNotByMe = new ViewObject(8, "InWorkNotByMe", "$Views_MyTasks_InWorkNotMe_Param");

        /// <summary>
        ///     ID:9 
        ///     Alias: CreationDate
        ///     Caption: $Views_ReportCurrentTasksByUser_CreationDate_Param.
        /// </summary>
        public readonly ViewObject ParamCreationDate = new ViewObject(9, "CreationDate", "$Views_ReportCurrentTasksByUser_CreationDate_Param");

        /// <summary>
        ///     ID:10 
        ///     Alias: EndDate
        ///     Caption: $Views_CurrentTasks_EndDate_Param.
        /// </summary>
        public readonly ViewObject ParamEndDate = new ViewObject(10, "EndDate", "$Views_CurrentTasks_EndDate_Param");

        /// <summary>
        ///     ID:11 
        ///     Alias: IsDelayed
        ///     Caption: $Views_MyTasks_IsDelayed_Param.
        /// </summary>
        public readonly ViewObject ParamIsDelayed = new ViewObject(11, "IsDelayed", "$Views_MyTasks_IsDelayed_Param");

        /// <summary>
        ///     ID:12 
        ///     Alias: TypeParam
        ///     Caption: $Views_CurrentTasks_DocType_Param.
        /// </summary>
        public readonly ViewObject ParamTypeParam = new ViewObject(12, "TypeParam", "$Views_CurrentTasks_DocType_Param");

        /// <summary>
        ///     ID:13 
        ///     Alias: FunctionRoleAuthorParam
        ///     Caption: $Views_MyTasks_FunctionRole_Author_Param.
        /// </summary>
        public readonly ViewObject ParamFunctionRoleAuthorParam = new ViewObject(13, "FunctionRoleAuthorParam", "$Views_MyTasks_FunctionRole_Author_Param");

        /// <summary>
        ///     ID:14 
        ///     Alias: FunctionRolePerformerParam
        ///     Caption: $Views_MyTasks_FunctionRole_Performer_Param.
        /// </summary>
        public readonly ViewObject ParamFunctionRolePerformerParam = new ViewObject(14, "FunctionRolePerformerParam", "$Views_MyTasks_FunctionRole_Performer_Param");

        #endregion

        #region ToString

        public static implicit operator string(MyTasksViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region MyTopics

    /// <summary>
    ///     ID: {01629718-ee20-45d9-820f-188b350bcf88}
    ///     Alias: MyTopics
    ///     Caption: $Views_Names_MyTopics
    ///     Group: Fm
    /// </summary>
    public class MyTopicsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "MyTopics": {01629718-ee20-45d9-820f-188b350bcf88}.
        /// </summary>
        public readonly Guid ID = new Guid(0x01629718,0xee20,0x45d9,0x82,0x0f,0x18,0x8b,0x35,0x0b,0xcf,0x88);

        /// <summary>
        ///     View name for "MyTopics".
        /// </summary>
        public readonly string Alias = "MyTopics";

        /// <summary>
        ///     View caption for "MyTopics".
        /// </summary>
        public readonly string Caption = "$Views_Names_MyTopics";

        /// <summary>
        ///     View group for "MyTopics".
        /// </summary>
        public readonly string Group = "Fm";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(0, "CardID");

        /// <summary>
        ///     ID:1 
        ///     Alias: Created
        ///     Caption: $Views_MyTopics_Created.
        /// </summary>
        public readonly ViewObject ColumnCreated = new ViewObject(1, "Created", "$Views_MyTopics_Created");

        /// <summary>
        ///     ID:2 
        ///     Alias: TopicID.
        /// </summary>
        public readonly ViewObject ColumnTopicID = new ViewObject(2, "TopicID");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(3, "TypeID");

        /// <summary>
        ///     ID:4 
        ///     Alias: TopicName
        ///     Caption: $Views_MyTopics_TopicName.
        /// </summary>
        public readonly ViewObject ColumnTopicName = new ViewObject(4, "TopicName", "$Views_MyTopics_TopicName");

        /// <summary>
        ///     ID:5 
        ///     Alias: Description
        ///     Caption: $Views_MyTopics_Description.
        /// </summary>
        public readonly ViewObject ColumnDescription = new ViewObject(5, "Description", "$Views_MyTopics_Description");

        /// <summary>
        ///     ID:6 
        ///     Alias: AuthorName
        ///     Caption: $Views_MyTopics_AuthorName.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(6, "AuthorName", "$Views_MyTopics_AuthorName");

        /// <summary>
        ///     ID:7 
        ///     Alias: LastMessage
        ///     Caption: $Views_MyTopics_LastMessage.
        /// </summary>
        public readonly ViewObject ColumnLastMessage = new ViewObject(7, "LastMessage", "$Views_MyTopics_LastMessage");

        /// <summary>
        ///     ID:8 
        ///     Alias: LastMessageAuthorName
        ///     Caption: $Views_MyTopics_LastMessageAuthorName.
        /// </summary>
        public readonly ViewObject ColumnLastMessageAuthorName = new ViewObject(8, "LastMessageAuthorName", "$Views_MyTopics_LastMessageAuthorName");

        /// <summary>
        ///     ID:9 
        ///     Alias: IsArchived.
        /// </summary>
        public readonly ViewObject ColumnIsArchived = new ViewObject(9, "IsArchived");

        /// <summary>
        ///     ID:10 
        ///     Alias: AppearanceColumn.
        /// </summary>
        public readonly ViewObject ColumnAppearanceColumn = new ViewObject(10, "AppearanceColumn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Created
        ///     Caption: $Views_MyTopics_Created.
        /// </summary>
        public readonly ViewObject ParamCreated = new ViewObject(0, "Created", "$Views_MyTopics_Created");

        /// <summary>
        ///     ID:1 
        ///     Alias: IsArchived
        ///     Caption: $Views_MyTopics_ShowArchived.
        /// </summary>
        public readonly ViewObject ParamIsArchived = new ViewObject(1, "IsArchived", "$Views_MyTopics_ShowArchived");

        #endregion

        #region ToString

        public static implicit operator string(MyTopicsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Notifications

    /// <summary>
    ///     ID: {ecc76994-461e-428d-abd0-c2499f6711fe}
    ///     Alias: Notifications
    ///     Caption: $Views_Names_Notifications
    ///     Group: System
    /// </summary>
    public class NotificationsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Notifications": {ecc76994-461e-428d-abd0-c2499f6711fe}.
        /// </summary>
        public readonly Guid ID = new Guid(0xecc76994,0x461e,0x428d,0xab,0xd0,0xc2,0x49,0x9f,0x67,0x11,0xfe);

        /// <summary>
        ///     View name for "Notifications".
        /// </summary>
        public readonly string Alias = "Notifications";

        /// <summary>
        ///     View caption for "Notifications".
        /// </summary>
        public readonly string Caption = "$Views_Names_Notifications";

        /// <summary>
        ///     View group for "Notifications".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: NotificationID.
        /// </summary>
        public readonly ViewObject ColumnNotificationID = new ViewObject(0, "NotificationID");

        /// <summary>
        ///     ID:1 
        ///     Alias: NotificationName
        ///     Caption: $Views_Notifications_Name.
        /// </summary>
        public readonly ViewObject ColumnNotificationName = new ViewObject(1, "NotificationName", "$Views_Notifications_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: NotificationType
        ///     Caption: $Views_Notifications_NotificationType.
        /// </summary>
        public readonly ViewObject ColumnNotificationType = new ViewObject(2, "NotificationType", "$Views_Notifications_NotificationType");

        /// <summary>
        ///     ID:3 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(3, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Notifications_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Notifications_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: NotificationType
        ///     Caption: $Views_Notifications_NotificationType_Param.
        /// </summary>
        public readonly ViewObject ParamNotificationType = new ViewObject(1, "NotificationType", "$Views_Notifications_NotificationType_Param");

        #endregion

        #region ToString

        public static implicit operator string(NotificationsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region NotificationSubscriptions

    /// <summary>
    ///     ID: {41fef937-d98e-48f8-9d47-eed0c1d32adf}
    ///     Alias: NotificationSubscriptions
    ///     Caption: $Views_Names_NotificationSubscriptions
    ///     Group: System
    /// </summary>
    public class NotificationSubscriptionsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "NotificationSubscriptions": {41fef937-d98e-48f8-9d47-eed0c1d32adf}.
        /// </summary>
        public readonly Guid ID = new Guid(0x41fef937,0xd98e,0x48f8,0x9d,0x47,0xee,0xd0,0xc1,0xd3,0x2a,0xdf);

        /// <summary>
        ///     View name for "NotificationSubscriptions".
        /// </summary>
        public readonly string Alias = "NotificationSubscriptions";

        /// <summary>
        ///     View caption for "NotificationSubscriptions".
        /// </summary>
        public readonly string Caption = "$Views_Names_NotificationSubscriptions";

        /// <summary>
        ///     View group for "NotificationSubscriptions".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: NotificationSubscriptionID.
        /// </summary>
        public readonly ViewObject ColumnNotificationSubscriptionID = new ViewObject(0, "NotificationSubscriptionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: NotificationSubscriptionCardID.
        /// </summary>
        public readonly ViewObject ColumnNotificationSubscriptionCardID = new ViewObject(1, "NotificationSubscriptionCardID");

        /// <summary>
        ///     ID:2 
        ///     Alias: NotificationSubscriptionCardDigest
        ///     Caption: $Views_NotificationSubscriptions_CardDigest.
        /// </summary>
        public readonly ViewObject ColumnNotificationSubscriptionCardDigest = new ViewObject(2, "NotificationSubscriptionCardDigest", "$Views_NotificationSubscriptions_CardDigest");

        /// <summary>
        ///     ID:3 
        ///     Alias: NotificationSubscriptionDate
        ///     Caption: $Views_NotificationSubscriptions_SubscriptionDate.
        /// </summary>
        public readonly ViewObject ColumnNotificationSubscriptionDate = new ViewObject(3, "NotificationSubscriptionDate", "$Views_NotificationSubscriptions_SubscriptionDate");

        /// <summary>
        ///     ID:4 
        ///     Alias: NotificationSubscriptionNotificationType
        ///     Caption: $Views_NotificationSubscriptions_NotificationType.
        /// </summary>
        public readonly ViewObject ColumnNotificationSubscriptionNotificationType = new ViewObject(4, "NotificationSubscriptionNotificationType", "$Views_NotificationSubscriptions_NotificationType");

        /// <summary>
        ///     ID:5 
        ///     Alias: NotificationSubscriptionType
        ///     Caption: $Views_NotificationSubscriptions_SubscriptionType.
        /// </summary>
        public readonly ViewObject ColumnNotificationSubscriptionType = new ViewObject(5, "NotificationSubscriptionType", "$Views_NotificationSubscriptions_SubscriptionType");

        /// <summary>
        ///     ID:6 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(6, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CardDigest
        ///     Caption: $Views_NotificationSubscriptions_CardDigest_Param.
        /// </summary>
        public readonly ViewObject ParamCardDigest = new ViewObject(0, "CardDigest", "$Views_NotificationSubscriptions_CardDigest_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: NotificationType
        ///     Caption: $Views_NotificationSubscriptions_NotificationType_Param.
        /// </summary>
        public readonly ViewObject ParamNotificationType = new ViewObject(1, "NotificationType", "$Views_NotificationSubscriptions_NotificationType_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: SubscriptionDate
        ///     Caption: $Views_NotificationSubscriptions_SubscriptionDate_Param.
        /// </summary>
        public readonly ViewObject ParamSubscriptionDate = new ViewObject(2, "SubscriptionDate", "$Views_NotificationSubscriptions_SubscriptionDate_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: IsSubscription
        ///     Caption: $Views_NotificationSubscriptions_IsSubscription_Param.
        /// </summary>
        public readonly ViewObject ParamIsSubscription = new ViewObject(3, "IsSubscription", "$Views_NotificationSubscriptions_IsSubscription_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: User
        ///     Caption: $Views_NotificationSubscriptions_User_Param.
        /// </summary>
        public readonly ViewObject ParamUser = new ViewObject(4, "User", "$Views_NotificationSubscriptions_User_Param");

        #endregion

        #region ToString

        public static implicit operator string(NotificationSubscriptionsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region NotificationTypes

    /// <summary>
    ///     ID: {72cd38b6-102a-4368-97a1-08b216865c96}
    ///     Alias: NotificationTypes
    ///     Caption: $Views_Names_NotificationTypes
    ///     Group: System
    /// </summary>
    public class NotificationTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "NotificationTypes": {72cd38b6-102a-4368-97a1-08b216865c96}.
        /// </summary>
        public readonly Guid ID = new Guid(0x72cd38b6,0x102a,0x4368,0x97,0xa1,0x08,0xb2,0x16,0x86,0x5c,0x96);

        /// <summary>
        ///     View name for "NotificationTypes".
        /// </summary>
        public readonly string Alias = "NotificationTypes";

        /// <summary>
        ///     View caption for "NotificationTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_NotificationTypes";

        /// <summary>
        ///     View group for "NotificationTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: NotificationTypeID.
        /// </summary>
        public readonly ViewObject ColumnNotificationTypeID = new ViewObject(0, "NotificationTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: NotificationTypeName
        ///     Caption: $Views_NotificationTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnNotificationTypeName = new ViewObject(1, "NotificationTypeName", "$Views_NotificationTypes_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: NotificationTypeIsGlobal
        ///     Caption: $Views_NotificationTypes_IsGlobal.
        /// </summary>
        public readonly ViewObject ColumnNotificationTypeIsGlobal = new ViewObject(2, "NotificationTypeIsGlobal", "$Views_NotificationTypes_IsGlobal");

        /// <summary>
        ///     ID:3 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(3, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_NotificationTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_NotificationTypes_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: IsGlobal
        ///     Caption: $Views_NotificationTypes_IsGlobal_Param.
        /// </summary>
        public readonly ViewObject ParamIsGlobal = new ViewObject(1, "IsGlobal", "$Views_NotificationTypes_IsGlobal_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: CanSubscribe
        ///     Caption: $Views_NotificationTypes_CanSubscribe_Param.
        /// </summary>
        public readonly ViewObject ParamCanSubscribe = new ViewObject(2, "CanSubscribe", "$Views_NotificationTypes_CanSubscribe_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: CardType
        ///     Caption: $Views_NotificationTypes_CardType_Param.
        /// </summary>
        public readonly ViewObject ParamCardType = new ViewObject(3, "CardType", "$Views_NotificationTypes_CardType_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: ShowHidden
        ///     Caption: $Views_NotificationTypes_ShowHidden_Param.
        /// </summary>
        public readonly ViewObject ParamShowHidden = new ViewObject(4, "ShowHidden", "$Views_NotificationTypes_ShowHidden_Param");

        #endregion

        #region ToString

        public static implicit operator string(NotificationTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region OcrLanguages

    /// <summary>
    ///     ID: {a7496820-b1b7-4443-b889-990996deeff1}
    ///     Alias: OcrLanguages
    ///     Caption: $Views_Names_OcrLanguages
    ///     Group: Ocr
    /// </summary>
    public class OcrLanguagesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "OcrLanguages": {a7496820-b1b7-4443-b889-990996deeff1}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa7496820,0xb1b7,0x4443,0xb8,0x89,0x99,0x09,0x96,0xde,0xef,0xf1);

        /// <summary>
        ///     View name for "OcrLanguages".
        /// </summary>
        public readonly string Alias = "OcrLanguages";

        /// <summary>
        ///     View caption for "OcrLanguages".
        /// </summary>
        public readonly string Caption = "$Views_Names_OcrLanguages";

        /// <summary>
        ///     View group for "OcrLanguages".
        /// </summary>
        public readonly string Group = "Ocr";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: LanguageID.
        /// </summary>
        public readonly ViewObject ColumnLanguageID = new ViewObject(0, "LanguageID");

        /// <summary>
        ///     ID:1 
        ///     Alias: LanguageCaption
        ///     Caption: $Views_OcrLanguages_Caption.
        /// </summary>
        public readonly ViewObject ColumnLanguageCaption = new ViewObject(1, "LanguageCaption", "$Views_OcrLanguages_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: LanguageISO
        ///     Caption: $Views_OcrLanguages_ISO.
        /// </summary>
        public readonly ViewObject ColumnLanguageISO = new ViewObject(2, "LanguageISO", "$Views_OcrLanguages_ISO");

        /// <summary>
        ///     ID:3 
        ///     Alias: LanguageCode
        ///     Caption: $Views_OcrLanguages_Code.
        /// </summary>
        public readonly ViewObject ColumnLanguageCode = new ViewObject(3, "LanguageCode", "$Views_OcrLanguages_Code");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: ISO
        ///     Caption: $Views_OcrLanguages_ISO_Param.
        /// </summary>
        public readonly ViewObject ParamISO = new ViewObject(0, "ISO", "$Views_OcrLanguages_ISO_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Code
        ///     Caption: $Views_OcrLanguages_Code_Param.
        /// </summary>
        public readonly ViewObject ParamCode = new ViewObject(1, "Code", "$Views_OcrLanguages_Code_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Caption
        ///     Caption: $Views_OcrLanguages_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(2, "Caption", "$Views_OcrLanguages_Caption_Param");

        #endregion

        #region ToString

        public static implicit operator string(OcrLanguagesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region OcrOperations

    /// <summary>
    ///     ID: {c6047012-cb5c-4fd3-a3d1-3b2b39be7a1f}
    ///     Alias: OcrOperations
    ///     Caption: $Views_Names_OcrOperations
    ///     Group: Ocr
    /// </summary>
    public class OcrOperationsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "OcrOperations": {c6047012-cb5c-4fd3-a3d1-3b2b39be7a1f}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc6047012,0xcb5c,0x4fd3,0xa3,0xd1,0x3b,0x2b,0x39,0xbe,0x7a,0x1f);

        /// <summary>
        ///     View name for "OcrOperations".
        /// </summary>
        public readonly string Alias = "OcrOperations";

        /// <summary>
        ///     View caption for "OcrOperations".
        /// </summary>
        public readonly string Caption = "$Views_Names_OcrOperations";

        /// <summary>
        ///     View group for "OcrOperations".
        /// </summary>
        public readonly string Group = "Ocr";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: OperationID.
        /// </summary>
        public readonly ViewObject ColumnOperationID = new ViewObject(0, "OperationID");

        /// <summary>
        ///     ID:1 
        ///     Alias: OperationCreated
        ///     Caption: $Views_OcrOperations_Created.
        /// </summary>
        public readonly ViewObject ColumnOperationCreated = new ViewObject(1, "OperationCreated", "$Views_OcrOperations_Created");

        /// <summary>
        ///     ID:2 
        ///     Alias: OperationCreatedByName
        ///     Caption: $Views_OcrOperations_CreatedBy.
        /// </summary>
        public readonly ViewObject ColumnOperationCreatedByName = new ViewObject(2, "OperationCreatedByName", "$Views_OcrOperations_CreatedBy");

        /// <summary>
        ///     ID:3 
        ///     Alias: OperationFileName
        ///     Caption: $Views_OcrOperations_FileName.
        /// </summary>
        public readonly ViewObject ColumnOperationFileName = new ViewObject(3, "OperationFileName", "$Views_OcrOperations_FileName");

        /// <summary>
        ///     ID:4 
        ///     Alias: OperationVersionRowID
        ///     Caption: $Views_OcrOperations_VersionRowID.
        /// </summary>
        public readonly ViewObject ColumnOperationVersionRowID = new ViewObject(4, "OperationVersionRowID", "$Views_OcrOperations_VersionRowID");

        /// <summary>
        ///     ID:5 
        ///     Alias: OperationCardID
        ///     Caption: $Views_OcrOperations_CardID.
        /// </summary>
        public readonly ViewObject ColumnOperationCardID = new ViewObject(5, "OperationCardID", "$Views_OcrOperations_CardID");

        #endregion

        #region ToString

        public static implicit operator string(OcrOperationsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region OcrPatternTypes

    /// <summary>
    ///     ID: {a1f31945-fbbb-4ae2-aa5a-db3354d75693}
    ///     Alias: OcrPatternTypes
    ///     Caption: $Views_Names_OcrPatternTypes
    ///     Group: Ocr
    /// </summary>
    public class OcrPatternTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "OcrPatternTypes": {a1f31945-fbbb-4ae2-aa5a-db3354d75693}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa1f31945,0xfbbb,0x4ae2,0xaa,0x5a,0xdb,0x33,0x54,0xd7,0x56,0x93);

        /// <summary>
        ///     View name for "OcrPatternTypes".
        /// </summary>
        public readonly string Alias = "OcrPatternTypes";

        /// <summary>
        ///     View caption for "OcrPatternTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_OcrPatternTypes";

        /// <summary>
        ///     View group for "OcrPatternTypes".
        /// </summary>
        public readonly string Group = "Ocr";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeName
        ///     Caption: $Views_OcrPatternTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(1, "TypeName", "$Views_OcrPatternTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_OcrPatternTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_OcrPatternTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(OcrPatternTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region OcrRecognitionModes

    /// <summary>
    ///     ID: {ab45451f-974d-4231-a182-16c6d04bdfba}
    ///     Alias: OcrRecognitionModes
    ///     Caption: $Views_Names_OcrRecognitionModes
    ///     Group: Ocr
    /// </summary>
    public class OcrRecognitionModesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "OcrRecognitionModes": {ab45451f-974d-4231-a182-16c6d04bdfba}.
        /// </summary>
        public readonly Guid ID = new Guid(0xab45451f,0x974d,0x4231,0xa1,0x82,0x16,0xc6,0xd0,0x4b,0xdf,0xba);

        /// <summary>
        ///     View name for "OcrRecognitionModes".
        /// </summary>
        public readonly string Alias = "OcrRecognitionModes";

        /// <summary>
        ///     View caption for "OcrRecognitionModes".
        /// </summary>
        public readonly string Caption = "$Views_Names_OcrRecognitionModes";

        /// <summary>
        ///     View group for "OcrRecognitionModes".
        /// </summary>
        public readonly string Group = "Ocr";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ModeID.
        /// </summary>
        public readonly ViewObject ColumnModeID = new ViewObject(0, "ModeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ModeName
        ///     Caption: $Views_OcrRecognitionModes_Name.
        /// </summary>
        public readonly ViewObject ColumnModeName = new ViewObject(1, "ModeName", "$Views_OcrRecognitionModes_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: ModeDescription
        ///     Caption: $Views_OcrRecognitionModes_Description.
        /// </summary>
        public readonly ViewObject ColumnModeDescription = new ViewObject(2, "ModeDescription", "$Views_OcrRecognitionModes_Description");

        #endregion

        #region ToString

        public static implicit operator string(OcrRecognitionModesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region OcrRequests

    /// <summary>
    ///     ID: {9eed84c8-53f0-4d9a-8619-bdf97c0fc615}
    ///     Alias: OcrRequests
    ///     Caption: $Views_Names_OcrRequests
    ///     Group: Ocr
    /// </summary>
    public class OcrRequestsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "OcrRequests": {9eed84c8-53f0-4d9a-8619-bdf97c0fc615}.
        /// </summary>
        public readonly Guid ID = new Guid(0x9eed84c8,0x53f0,0x4d9a,0x86,0x19,0xbd,0xf9,0x7c,0x0f,0xc6,0x15);

        /// <summary>
        ///     View name for "OcrRequests".
        /// </summary>
        public readonly string Alias = "OcrRequests";

        /// <summary>
        ///     View caption for "OcrRequests".
        /// </summary>
        public readonly string Caption = "$Views_Names_OcrRequests";

        /// <summary>
        ///     View group for "OcrRequests".
        /// </summary>
        public readonly string Group = "Ocr";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RequestID.
        /// </summary>
        public readonly ViewObject ColumnRequestID = new ViewObject(0, "RequestID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RequestIsMain
        ///     Caption: $Views_OcrRequests_IsMain.
        /// </summary>
        public readonly ViewObject ColumnRequestIsMain = new ViewObject(1, "RequestIsMain", "$Views_OcrRequests_IsMain");

        /// <summary>
        ///     ID:2 
        ///     Alias: RequestCreated
        ///     Caption: $Views_OcrRequests_Created.
        /// </summary>
        public readonly ViewObject ColumnRequestCreated = new ViewObject(2, "RequestCreated", "$Views_OcrRequests_Created");

        /// <summary>
        ///     ID:3 
        ///     Alias: RequestCreatedByName
        ///     Caption: $Views_OcrRequests_CreatedBy.
        /// </summary>
        public readonly ViewObject ColumnRequestCreatedByName = new ViewObject(3, "RequestCreatedByName", "$Views_OcrRequests_CreatedBy");

        /// <summary>
        ///     ID:4 
        ///     Alias: RequestLanguages
        ///     Caption: $Views_OcrRequests_Languages.
        /// </summary>
        public readonly ViewObject ColumnRequestLanguages = new ViewObject(4, "RequestLanguages", "$Views_OcrRequests_Languages");

        /// <summary>
        ///     ID:5 
        ///     Alias: RequestConfidence
        ///     Caption: $Views_OcrRequests_Confidence.
        /// </summary>
        public readonly ViewObject ColumnRequestConfidence = new ViewObject(5, "RequestConfidence", "$Views_OcrRequests_Confidence");

        /// <summary>
        ///     ID:6 
        ///     Alias: RequestPreprocess
        ///     Caption: $Views_OcrRequests_Preprocess.
        /// </summary>
        public readonly ViewObject ColumnRequestPreprocess = new ViewObject(6, "RequestPreprocess", "$Views_OcrRequests_Preprocess");

        /// <summary>
        ///     ID:7 
        ///     Alias: RequestDetectRotation
        ///     Caption: $Views_OcrRequests_Autorotation.
        /// </summary>
        public readonly ViewObject ColumnRequestDetectRotation = new ViewObject(7, "RequestDetectRotation", "$Views_OcrRequests_Autorotation");

        /// <summary>
        ///     ID:8 
        ///     Alias: RequestOverwrite
        ///     Caption: $Views_OcrRequests_Overwrite.
        /// </summary>
        public readonly ViewObject ColumnRequestOverwrite = new ViewObject(8, "RequestOverwrite", "$Views_OcrRequests_Overwrite");

        /// <summary>
        ///     ID:9 
        ///     Alias: RequestSegmentationMode
        ///     Caption: $Views_OcrRequests_SegmentationMode.
        /// </summary>
        public readonly ViewObject ColumnRequestSegmentationMode = new ViewObject(9, "RequestSegmentationMode", "$Views_OcrRequests_SegmentationMode");

        /// <summary>
        ///     ID:10 
        ///     Alias: RequestStateAppearance.
        /// </summary>
        public readonly ViewObject ColumnRequestStateAppearance = new ViewObject(10, "RequestStateAppearance");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Operation
        ///     Caption: $Views_OcrRequests_Operation_Param.
        /// </summary>
        public readonly ViewObject ParamOperation = new ViewObject(0, "Operation", "$Views_OcrRequests_Operation_Param");

        #endregion

        #region ToString

        public static implicit operator string(OcrRequestsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region OcrSegmentationModes

    /// <summary>
    ///     ID: {740088cd-bd83-4d81-89bb-7ff9e12da0d0}
    ///     Alias: OcrSegmentationModes
    ///     Caption: $Views_Names_OcrSegmentationModes
    ///     Group: Ocr
    /// </summary>
    public class OcrSegmentationModesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "OcrSegmentationModes": {740088cd-bd83-4d81-89bb-7ff9e12da0d0}.
        /// </summary>
        public readonly Guid ID = new Guid(0x740088cd,0xbd83,0x4d81,0x89,0xbb,0x7f,0xf9,0xe1,0x2d,0xa0,0xd0);

        /// <summary>
        ///     View name for "OcrSegmentationModes".
        /// </summary>
        public readonly string Alias = "OcrSegmentationModes";

        /// <summary>
        ///     View caption for "OcrSegmentationModes".
        /// </summary>
        public readonly string Caption = "$Views_Names_OcrSegmentationModes";

        /// <summary>
        ///     View group for "OcrSegmentationModes".
        /// </summary>
        public readonly string Group = "Ocr";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ModeID.
        /// </summary>
        public readonly ViewObject ColumnModeID = new ViewObject(0, "ModeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ModeName
        ///     Caption: $Views_OcrSegmentationModes_Name.
        /// </summary>
        public readonly ViewObject ColumnModeName = new ViewObject(1, "ModeName", "$Views_OcrSegmentationModes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_OcrSegmentationModes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_OcrSegmentationModes_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Hidden
        ///     Caption: $Views_OcrSegmentationModes_Hidden_Param.
        /// </summary>
        public readonly ViewObject ParamHidden = new ViewObject(1, "Hidden", "$Views_OcrSegmentationModes_Hidden_Param");

        #endregion

        #region ToString

        public static implicit operator string(OcrSegmentationModesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Operations

    /// <summary>
    ///     ID: {c0bc56cb-0868-4108-996f-a87fe290d90d}
    ///     Alias: Operations
    ///     Caption: $Views_Names_Operations
    ///     Group: System
    /// </summary>
    public class OperationsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Operations": {c0bc56cb-0868-4108-996f-a87fe290d90d}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc0bc56cb,0x0868,0x4108,0x99,0x6f,0xa8,0x7f,0xe2,0x90,0xd9,0x0d);

        /// <summary>
        ///     View name for "Operations".
        /// </summary>
        public readonly string Alias = "Operations";

        /// <summary>
        ///     View caption for "Operations".
        /// </summary>
        public readonly string Caption = "$Views_Names_Operations";

        /// <summary>
        ///     View group for "Operations".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeName
        ///     Caption: $Views_Operations_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(1, "TypeName", "$Views_Operations_Type");

        /// <summary>
        ///     ID:2 
        ///     Alias: OperationID.
        /// </summary>
        public readonly ViewObject ColumnOperationID = new ViewObject(2, "OperationID");

        /// <summary>
        ///     ID:3 
        ///     Alias: OperationName
        ///     Caption: $Views_Operations_Name.
        /// </summary>
        public readonly ViewObject ColumnOperationName = new ViewObject(3, "OperationName", "$Views_Operations_Name");

        /// <summary>
        ///     ID:4 
        ///     Alias: StateID.
        /// </summary>
        public readonly ViewObject ColumnStateID = new ViewObject(4, "StateID");

        /// <summary>
        ///     ID:5 
        ///     Alias: StateName
        ///     Caption: $Views_Operations_State.
        /// </summary>
        public readonly ViewObject ColumnStateName = new ViewObject(5, "StateName", "$Views_Operations_State");

        /// <summary>
        ///     ID:6 
        ///     Alias: Progress
        ///     Caption: %.
        /// </summary>
        public readonly ViewObject ColumnProgress = new ViewObject(6, "Progress", "%");

        /// <summary>
        ///     ID:7 
        ///     Alias: CreatedByID.
        /// </summary>
        public readonly ViewObject ColumnCreatedByID = new ViewObject(7, "CreatedByID");

        /// <summary>
        ///     ID:8 
        ///     Alias: CreatedByName
        ///     Caption: $Views_Operations_User.
        /// </summary>
        public readonly ViewObject ColumnCreatedByName = new ViewObject(8, "CreatedByName", "$Views_Operations_User");

        /// <summary>
        ///     ID:9 
        ///     Alias: Created
        ///     Caption: $Views_Operations_Created.
        /// </summary>
        public readonly ViewObject ColumnCreated = new ViewObject(9, "Created", "$Views_Operations_Created");

        /// <summary>
        ///     ID:10 
        ///     Alias: InProgress
        ///     Caption: $Views_Operations_InProgress.
        /// </summary>
        public readonly ViewObject ColumnInProgress = new ViewObject(10, "InProgress", "$Views_Operations_InProgress");

        /// <summary>
        ///     ID:11 
        ///     Alias: Completed
        ///     Caption: $Views_Operations_Completed.
        /// </summary>
        public readonly ViewObject ColumnCompleted = new ViewObject(11, "Completed", "$Views_Operations_Completed");

        /// <summary>
        ///     ID:12 
        ///     Alias: Postponed
        ///     Caption: $Views_Operations_Postponed.
        /// </summary>
        public readonly ViewObject ColumnPostponed = new ViewObject(12, "Postponed", "$Views_Operations_Postponed");

        /// <summary>
        ///     ID:13 
        ///     Alias: CreationFlags.
        /// </summary>
        public readonly ViewObject ColumnCreationFlags = new ViewObject(13, "CreationFlags");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeName
        ///     Caption: $Views_Operations_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeName = new ViewObject(0, "TypeName", "$Views_Operations_Type_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Digest
        ///     Caption: $Views_Operations_Name_Param.
        /// </summary>
        public readonly ViewObject ParamDigest = new ViewObject(1, "Digest", "$Views_Operations_Name_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: StateName
        ///     Caption: $Views_Operations_State_Param.
        /// </summary>
        public readonly ViewObject ParamStateName = new ViewObject(2, "StateName", "$Views_Operations_State_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: CreatedByID
        ///     Caption: $Views_Operations_User_Param.
        /// </summary>
        public readonly ViewObject ParamCreatedByID = new ViewObject(3, "CreatedByID", "$Views_Operations_User_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: CreatedByName
        ///     Caption: $Views_Operations_UserName_Param.
        /// </summary>
        public readonly ViewObject ParamCreatedByName = new ViewObject(4, "CreatedByName", "$Views_Operations_UserName_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Created
        ///     Caption: $Views_Operations_Created_Param.
        /// </summary>
        public readonly ViewObject ParamCreated = new ViewObject(5, "Created", "$Views_Operations_Created_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: InProgress
        ///     Caption: $Views_Operations_InProgress_Param.
        /// </summary>
        public readonly ViewObject ParamInProgress = new ViewObject(6, "InProgress", "$Views_Operations_InProgress_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: Completed
        ///     Caption: $Views_Operations_Completed_Param.
        /// </summary>
        public readonly ViewObject ParamCompleted = new ViewObject(7, "Completed", "$Views_Operations_Completed_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: Postponed
        ///     Caption: $Views_Operations_Postponed_Param.
        /// </summary>
        public readonly ViewObject ParamPostponed = new ViewObject(8, "Postponed", "$Views_Operations_Postponed_Param");

        #endregion

        #region ToString

        public static implicit operator string(OperationsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region OutgoingDocuments

    /// <summary>
    ///     ID: {a4d2d4e1-a59c-4265-a2ee-f58ca0cbc3fc}
    ///     Alias: OutgoingDocuments
    ///     Caption: $Views_Names_OutgoingDocuments
    ///     Group: KrDocuments
    /// </summary>
    public class OutgoingDocumentsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "OutgoingDocuments": {a4d2d4e1-a59c-4265-a2ee-f58ca0cbc3fc}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa4d2d4e1,0xa59c,0x4265,0xa2,0xee,0xf5,0x8c,0xa0,0xcb,0xc3,0xfc);

        /// <summary>
        ///     View name for "OutgoingDocuments".
        /// </summary>
        public readonly string Alias = "OutgoingDocuments";

        /// <summary>
        ///     View caption for "OutgoingDocuments".
        /// </summary>
        public readonly string Caption = "$Views_Names_OutgoingDocuments";

        /// <summary>
        ///     View group for "OutgoingDocuments".
        /// </summary>
        public readonly string Group = "KrDocuments";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DocID.
        /// </summary>
        public readonly ViewObject ColumnDocID = new ViewObject(0, "DocID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DocNumber
        ///     Caption: $Views_Registers_Number.
        /// </summary>
        public readonly ViewObject ColumnDocNumber = new ViewObject(1, "DocNumber", "$Views_Registers_Number");

        /// <summary>
        ///     ID:2 
        ///     Alias: SubTypeTitle
        ///     Caption: $Views_Registers_DocType.
        /// </summary>
        public readonly ViewObject ColumnSubTypeTitle = new ViewObject(2, "SubTypeTitle", "$Views_Registers_DocType");

        /// <summary>
        ///     ID:3 
        ///     Alias: DocSubject
        ///     Caption: $Views_Registers_Subject.
        /// </summary>
        public readonly ViewObject ColumnDocSubject = new ViewObject(3, "DocSubject", "$Views_Registers_Subject");

        /// <summary>
        ///     ID:4 
        ///     Alias: DocDescription
        ///     Caption: $Views_Registers_DocDescription.
        /// </summary>
        public readonly ViewObject ColumnDocDescription = new ViewObject(4, "DocDescription", "$Views_Registers_DocDescription");

        /// <summary>
        ///     ID:5 
        ///     Alias: PartnerID.
        /// </summary>
        public readonly ViewObject ColumnPartnerID = new ViewObject(5, "PartnerID");

        /// <summary>
        ///     ID:6 
        ///     Alias: PartnerName
        ///     Caption: $Views_Registers_Partner.
        /// </summary>
        public readonly ViewObject ColumnPartnerName = new ViewObject(6, "PartnerName", "$Views_Registers_Partner");

        /// <summary>
        ///     ID:7 
        ///     Alias: AuthorID.
        /// </summary>
        public readonly ViewObject ColumnAuthorID = new ViewObject(7, "AuthorID");

        /// <summary>
        ///     ID:8 
        ///     Alias: AuthorName
        ///     Caption: $Views_Registers_Author.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(8, "AuthorName", "$Views_Registers_Author");

        /// <summary>
        ///     ID:9 
        ///     Alias: RegistratorID.
        /// </summary>
        public readonly ViewObject ColumnRegistratorID = new ViewObject(9, "RegistratorID");

        /// <summary>
        ///     ID:10 
        ///     Alias: RegistratorName
        ///     Caption: $Views_Registers_Registrator.
        /// </summary>
        public readonly ViewObject ColumnRegistratorName = new ViewObject(10, "RegistratorName", "$Views_Registers_Registrator");

        /// <summary>
        ///     ID:11 
        ///     Alias: KrState
        ///     Caption: $Views_Registers_State.
        /// </summary>
        public readonly ViewObject ColumnKrState = new ViewObject(11, "KrState", "$Views_Registers_State");

        /// <summary>
        ///     ID:12 
        ///     Alias: DocDate
        ///     Caption: $Views_Registers_DocDate.
        /// </summary>
        public readonly ViewObject ColumnDocDate = new ViewObject(12, "DocDate", "$Views_Registers_DocDate");

        /// <summary>
        ///     ID:13 
        ///     Alias: CreationDate
        ///     Caption: $Views_Registers_CreationDate.
        /// </summary>
        public readonly ViewObject ColumnCreationDate = new ViewObject(13, "CreationDate", "$Views_Registers_CreationDate");

        /// <summary>
        ///     ID:14 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department.
        /// </summary>
        public readonly ViewObject ColumnDepartment = new ViewObject(14, "Department", "$Views_Registers_Department");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: IsAuthor
        ///     Caption: $Views_Registers_IsAuthor_Param.
        /// </summary>
        public readonly ViewObject ParamIsAuthor = new ViewObject(0, "IsAuthor", "$Views_Registers_IsAuthor_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: IsInitiator
        ///     Caption: $Views_Registers_IsInitiator_Param.
        /// </summary>
        public readonly ViewObject ParamIsInitiator = new ViewObject(1, "IsInitiator", "$Views_Registers_IsInitiator_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: IsRegistrator
        ///     Caption: $Views_Registers_IsRegistrator_Param.
        /// </summary>
        public readonly ViewObject ParamIsRegistrator = new ViewObject(2, "IsRegistrator", "$Views_Registers_IsRegistrator_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Partner
        ///     Caption: $Views_Registers_Partner_Param.
        /// </summary>
        public readonly ViewObject ParamPartner = new ViewObject(3, "Partner", "$Views_Registers_Partner_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Number
        ///     Caption: $Views_Registers_Number_Param.
        /// </summary>
        public readonly ViewObject ParamNumber = new ViewObject(4, "Number", "$Views_Registers_Number_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Subject
        ///     Caption: $Views_Registers_Subject_Param.
        /// </summary>
        public readonly ViewObject ParamSubject = new ViewObject(5, "Subject", "$Views_Registers_Subject_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: DocDate
        ///     Caption: $Views_Registers_DocDate_Param.
        /// </summary>
        public readonly ViewObject ParamDocDate = new ViewObject(6, "DocDate", "$Views_Registers_DocDate_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: Author
        ///     Caption: $Views_Registers_Author_Param.
        /// </summary>
        public readonly ViewObject ParamAuthor = new ViewObject(7, "Author", "$Views_Registers_Author_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: Registrator
        ///     Caption: $Views_Registers_Registrator_Param.
        /// </summary>
        public readonly ViewObject ParamRegistrator = new ViewObject(8, "Registrator", "$Views_Registers_Registrator_Param");

        /// <summary>
        ///     ID:9 
        ///     Alias: State
        ///     Caption: $Views_Registers_State_Param.
        /// </summary>
        public readonly ViewObject ParamState = new ViewObject(9, "State", "$Views_Registers_State_Param");

        /// <summary>
        ///     ID:10 
        ///     Alias: DocType
        ///     Caption: $Views_Registers_DocType_Param.
        /// </summary>
        public readonly ViewObject ParamDocType = new ViewObject(10, "DocType", "$Views_Registers_DocType_Param");

        /// <summary>
        ///     ID:11 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(11, "Department", "$Views_Registers_Department_Param");

        /// <summary>
        ///     ID:12 
        ///     Alias: CreationDate
        ///     Caption: $Views_Registers_CreationDate_Param.
        /// </summary>
        public readonly ViewObject ParamCreationDate = new ViewObject(12, "CreationDate", "$Views_Registers_CreationDate_Param");

        #endregion

        #region ToString

        public static implicit operator string(OutgoingDocumentsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Partitions

    /// <summary>
    ///     ID: {9500e883-9c8e-427e-930b-e93adfd0f56a}
    ///     Alias: Partitions
    ///     Caption: $Views_Names_Partitions
    ///     Group: System
    /// </summary>
    public class PartitionsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Partitions": {9500e883-9c8e-427e-930b-e93adfd0f56a}.
        /// </summary>
        public readonly Guid ID = new Guid(0x9500e883,0x9c8e,0x427e,0x93,0x0b,0xe9,0x3a,0xdf,0xd0,0xf5,0x6a);

        /// <summary>
        ///     View name for "Partitions".
        /// </summary>
        public readonly string Alias = "Partitions";

        /// <summary>
        ///     View caption for "Partitions".
        /// </summary>
        public readonly string Caption = "$Views_Names_Partitions";

        /// <summary>
        ///     View group for "Partitions".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: PartitionID.
        /// </summary>
        public readonly ViewObject ColumnPartitionID = new ViewObject(0, "PartitionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: PartitionName
        ///     Caption: $Views_Partitions_Name.
        /// </summary>
        public readonly ViewObject ColumnPartitionName = new ViewObject(1, "PartitionName", "$Views_Partitions_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: PartitionID
        ///     Caption: PartitionID.
        /// </summary>
        public readonly ViewObject ParamPartitionID = new ViewObject(0, "PartitionID", "PartitionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_Partitions_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_Partitions_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(PartitionsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Partners

    /// <summary>
    ///     ID: {f9e6f291-2de8-459c-8d05-420fb8cce90f}
    ///     Alias: Partners
    ///     Caption: $Views_Names_Partners
    ///     Group: System
    /// </summary>
    public class PartnersViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Partners": {f9e6f291-2de8-459c-8d05-420fb8cce90f}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf9e6f291,0x2de8,0x459c,0x8d,0x05,0x42,0x0f,0xb8,0xcc,0xe9,0x0f);

        /// <summary>
        ///     View name for "Partners".
        /// </summary>
        public readonly string Alias = "Partners";

        /// <summary>
        ///     View caption for "Partners".
        /// </summary>
        public readonly string Caption = "$Views_Names_Partners";

        /// <summary>
        ///     View group for "Partners".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: PartnerID.
        /// </summary>
        public readonly ViewObject ColumnPartnerID = new ViewObject(0, "PartnerID");

        /// <summary>
        ///     ID:1 
        ///     Alias: PartnerName
        ///     Caption: $Views_Partners_Name.
        /// </summary>
        public readonly ViewObject ColumnPartnerName = new ViewObject(1, "PartnerName", "$Views_Partners_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: FullName
        ///     Caption: $Views_Partners_FullName.
        /// </summary>
        public readonly ViewObject ColumnFullName = new ViewObject(2, "FullName", "$Views_Partners_FullName");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(3, "TypeID");

        /// <summary>
        ///     ID:4 
        ///     Alias: TypeName
        ///     Caption: $Views_Partners_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(4, "TypeName", "$Views_Partners_Type");

        /// <summary>
        ///     ID:5 
        ///     Alias: INN
        ///     Caption: $Views_Partners_INN.
        /// </summary>
        public readonly ViewObject ColumnINN = new ViewObject(5, "INN", "$Views_Partners_INN");

        /// <summary>
        ///     ID:6 
        ///     Alias: KPP
        ///     Caption: $Views_Partners_KPP.
        /// </summary>
        public readonly ViewObject ColumnKPP = new ViewObject(6, "KPP", "$Views_Partners_KPP");

        /// <summary>
        ///     ID:7 
        ///     Alias: OGRN.
        /// </summary>
        public readonly ViewObject ColumnOGRN = new ViewObject(7, "OGRN");

        /// <summary>
        ///     ID:8 
        ///     Alias: Comment.
        /// </summary>
        public readonly ViewObject ColumnComment = new ViewObject(8, "Comment");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: PartnerID
        ///     Caption: PartnerID.
        /// </summary>
        public readonly ViewObject ParamPartnerID = new ViewObject(0, "PartnerID", "PartnerID");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_Partners_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_Partners_Name_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: FullName
        ///     Caption: $Views_Partners_FullName_Param.
        /// </summary>
        public readonly ViewObject ParamFullName = new ViewObject(2, "FullName", "$Views_Partners_FullName_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Type
        ///     Caption: $Views_Partners_Type_Param.
        /// </summary>
        public readonly ViewObject ParamType = new ViewObject(3, "Type", "$Views_Partners_Type_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: INN
        ///     Caption: $Views_Partners_INN_Param.
        /// </summary>
        public readonly ViewObject ParamINN = new ViewObject(4, "INN", "$Views_Partners_INN_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: KPP
        ///     Caption: $Views_Partners_KPP_Param.
        /// </summary>
        public readonly ViewObject ParamKPP = new ViewObject(5, "KPP", "$Views_Partners_KPP_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: OGRN
        ///     Caption: $Views_Partners_OGRN_Param.
        /// </summary>
        public readonly ViewObject ParamOGRN = new ViewObject(6, "OGRN", "$Views_Partners_OGRN_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: Comment
        ///     Caption: $Views_Partners_Comment_Param.
        /// </summary>
        public readonly ViewObject ParamComment = new ViewObject(7, "Comment", "$Views_Partners_Comment_Param");

        #endregion

        #region ToString

        public static implicit operator string(PartnersViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region PartnersContacts

    /// <summary>
    ///     ID: {d0971b0f-42a0-433c-b3f4-a1d1b279156c}
    ///     Alias: PartnersContacts
    ///     Caption: $Views_Names_PartnersContacts
    ///     Group: System
    /// </summary>
    public class PartnersContactsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "PartnersContacts": {d0971b0f-42a0-433c-b3f4-a1d1b279156c}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd0971b0f,0x42a0,0x433c,0xb3,0xf4,0xa1,0xd1,0xb2,0x79,0x15,0x6c);

        /// <summary>
        ///     View name for "PartnersContacts".
        /// </summary>
        public readonly string Alias = "PartnersContacts";

        /// <summary>
        ///     View caption for "PartnersContacts".
        /// </summary>
        public readonly string Caption = "$Views_Names_PartnersContacts";

        /// <summary>
        ///     View group for "PartnersContacts".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: PartnerContactPartnerID.
        /// </summary>
        public readonly ViewObject ColumnPartnerContactPartnerID = new ViewObject(0, "PartnerContactPartnerID");

        /// <summary>
        ///     ID:1 
        ///     Alias: PartnerContactPartnerName
        ///     Caption: $CardTypes_Controls_Partner.
        /// </summary>
        public readonly ViewObject ColumnPartnerContactPartnerName = new ViewObject(1, "PartnerContactPartnerName", "$CardTypes_Controls_Partner");

        /// <summary>
        ///     ID:2 
        ///     Alias: PartnerContactRowID.
        /// </summary>
        public readonly ViewObject ColumnPartnerContactRowID = new ViewObject(2, "PartnerContactRowID");

        /// <summary>
        ///     ID:3 
        ///     Alias: PartnerContactFullName
        ///     Caption: $CardTypes_Controls_Columns_FullName.
        /// </summary>
        public readonly ViewObject ColumnPartnerContactFullName = new ViewObject(3, "PartnerContactFullName", "$CardTypes_Controls_Columns_FullName");

        /// <summary>
        ///     ID:4 
        ///     Alias: Department
        ///     Caption: $CardTypes_Controls_Columns_Department.
        /// </summary>
        public readonly ViewObject ColumnDepartment = new ViewObject(4, "Department", "$CardTypes_Controls_Columns_Department");

        /// <summary>
        ///     ID:5 
        ///     Alias: PartnerContactName.
        /// </summary>
        public readonly ViewObject ColumnPartnerContactName = new ViewObject(5, "PartnerContactName");

        /// <summary>
        ///     ID:6 
        ///     Alias: PhoneNumber
        ///     Caption: $CardTypes_Controls_Columns_PhoneNumber.
        /// </summary>
        public readonly ViewObject ColumnPhoneNumber = new ViewObject(6, "PhoneNumber", "$CardTypes_Controls_Columns_PhoneNumber");

        /// <summary>
        ///     ID:7 
        ///     Alias: Email
        ///     Caption: $CardTypes_Controls_Columns_Email.
        /// </summary>
        public readonly ViewObject ColumnEmail = new ViewObject(7, "Email", "$CardTypes_Controls_Columns_Email");

        /// <summary>
        ///     ID:8 
        ///     Alias: ContactAddress
        ///     Caption: $CardTypes_Controls_ContactAddress.
        /// </summary>
        public readonly ViewObject ColumnContactAddress = new ViewObject(8, "ContactAddress", "$CardTypes_Controls_ContactAddress");

        /// <summary>
        ///     ID:9 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(9, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: PartnerID
        ///     Caption: $CardTypes_Controls_Partner.
        /// </summary>
        public readonly ViewObject ParamPartnerID = new ViewObject(0, "PartnerID", "$CardTypes_Controls_Partner");

        /// <summary>
        ///     ID:1 
        ///     Alias: PartnerIDHidden
        ///     Caption: $CardTypes_Controls_Partner.
        /// </summary>
        public readonly ViewObject ParamPartnerIDHidden = new ViewObject(1, "PartnerIDHidden", "$CardTypes_Controls_Partner");

        /// <summary>
        ///     ID:2 
        ///     Alias: Name
        ///     Caption: $CardTypes_Controls_Columns_FullName.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(2, "Name", "$CardTypes_Controls_Columns_FullName");

        #endregion

        #region ToString

        public static implicit operator string(PartnersContactsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region PartnersTypes

    /// <summary>
    ///     ID: {59d56dd0-700f-46de-afd5-9aa3a1f9f69e}
    ///     Alias: PartnersTypes
    ///     Caption: $Views_Names_PartnersTypes
    ///     Group: System
    /// </summary>
    public class PartnersTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "PartnersTypes": {59d56dd0-700f-46de-afd5-9aa3a1f9f69e}.
        /// </summary>
        public readonly Guid ID = new Guid(0x59d56dd0,0x700f,0x46de,0xaf,0xd5,0x9a,0xa3,0xa1,0xf9,0xf6,0x9e);

        /// <summary>
        ///     View name for "PartnersTypes".
        /// </summary>
        public readonly string Alias = "PartnersTypes";

        /// <summary>
        ///     View caption for "PartnersTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_PartnersTypes";

        /// <summary>
        ///     View group for "PartnersTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeName
        ///     Caption: $Views_ParnerTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(1, "TypeName", "$Views_ParnerTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_ParnerTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_ParnerTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(PartnersTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ProtocolCompletedTasks

    /// <summary>
    ///     ID: {65dde283-7e4c-4898-b91f-2921e8fc8ac4}
    ///     Alias: ProtocolCompletedTasks
    ///     Caption: $Views_Names_ProtocolCompletedTasks
    ///     Group: KrDocuments
    /// </summary>
    public class ProtocolCompletedTasksViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ProtocolCompletedTasks": {65dde283-7e4c-4898-b91f-2921e8fc8ac4}.
        /// </summary>
        public readonly Guid ID = new Guid(0x65dde283,0x7e4c,0x4898,0xb9,0x1f,0x29,0x21,0xe8,0xfc,0x8a,0xc4);

        /// <summary>
        ///     View name for "ProtocolCompletedTasks".
        /// </summary>
        public readonly string Alias = "ProtocolCompletedTasks";

        /// <summary>
        ///     View caption for "ProtocolCompletedTasks".
        /// </summary>
        public readonly string Caption = "$Views_Names_ProtocolCompletedTasks";

        /// <summary>
        ///     View group for "ProtocolCompletedTasks".
        /// </summary>
        public readonly string Group = "KrDocuments";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(0, "CardID");

        /// <summary>
        ///     ID:1 
        ///     Alias: CardName
        ///     Caption: $Views_CompletedTasks_Card.
        /// </summary>
        public readonly ViewObject ColumnCardName = new ViewObject(1, "CardName", "$Views_CompletedTasks_Card");

        /// <summary>
        ///     ID:2 
        ///     Alias: TaskID.
        /// </summary>
        public readonly ViewObject ColumnTaskID = new ViewObject(2, "TaskID");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(3, "TypeID");

        /// <summary>
        ///     ID:4 
        ///     Alias: TypeCaption
        ///     Caption: $Views_CompletedTasks_TaskType.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(4, "TypeCaption", "$Views_CompletedTasks_TaskType");

        /// <summary>
        ///     ID:5 
        ///     Alias: CardTypeCaption
        ///     Caption: $Views_CompletedTasks_CardType.
        /// </summary>
        public readonly ViewObject ColumnCardTypeCaption = new ViewObject(5, "CardTypeCaption", "$Views_CompletedTasks_CardType");

        /// <summary>
        ///     ID:6 
        ///     Alias: RoleID.
        /// </summary>
        public readonly ViewObject ColumnRoleID = new ViewObject(6, "RoleID");

        /// <summary>
        ///     ID:7 
        ///     Alias: RoleName
        ///     Caption: $Views_CompletedTasks_Role.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(7, "RoleName", "$Views_CompletedTasks_Role");

        /// <summary>
        ///     ID:8 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(8, "UserID");

        /// <summary>
        ///     ID:9 
        ///     Alias: UserName
        ///     Caption: $Views_CompletedTasks_User.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(9, "UserName", "$Views_CompletedTasks_User");

        /// <summary>
        ///     ID:10 
        ///     Alias: AuthorID.
        /// </summary>
        public readonly ViewObject ColumnAuthorID = new ViewObject(10, "AuthorID");

        /// <summary>
        ///     ID:11 
        ///     Alias: AuthorName
        ///     Caption: $Views_CompletedTasks_Author.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(11, "AuthorName", "$Views_CompletedTasks_Author");

        /// <summary>
        ///     ID:12 
        ///     Alias: Created
        ///     Caption: $Views_CompletedTasks_Created.
        /// </summary>
        public readonly ViewObject ColumnCreated = new ViewObject(12, "Created", "$Views_CompletedTasks_Created");

        /// <summary>
        ///     ID:13 
        ///     Alias: Planned
        ///     Caption: $Views_CompletedTasks_Planned.
        /// </summary>
        public readonly ViewObject ColumnPlanned = new ViewObject(13, "Planned", "$Views_CompletedTasks_Planned");

        /// <summary>
        ///     ID:14 
        ///     Alias: Completed
        ///     Caption: $Views_CompletedTasks_Completed.
        /// </summary>
        public readonly ViewObject ColumnCompleted = new ViewObject(14, "Completed", "$Views_CompletedTasks_Completed");

        /// <summary>
        ///     ID:15 
        ///     Alias: OptionID.
        /// </summary>
        public readonly ViewObject ColumnOptionID = new ViewObject(15, "OptionID");

        /// <summary>
        ///     ID:16 
        ///     Alias: OptionCaption
        ///     Caption: $Views_CompletedTasks_CompletionOption.
        /// </summary>
        public readonly ViewObject ColumnOptionCaption = new ViewObject(16, "OptionCaption", "$Views_CompletedTasks_CompletionOption");

        /// <summary>
        ///     ID:17 
        ///     Alias: Result
        ///     Caption: $Views_CompletedTasks_Result.
        /// </summary>
        public readonly ViewObject ColumnResult = new ViewObject(17, "Result", "$Views_CompletedTasks_Result");

        /// <summary>
        ///     ID:18 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(18, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CardID
        ///     Caption: $Views_CompletedTasks_Card.
        /// </summary>
        public readonly ViewObject ParamCardID = new ViewObject(0, "CardID", "$Views_CompletedTasks_Card");

        #endregion

        #region ToString

        public static implicit operator string(ProtocolCompletedTasksViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ProtocolReportsWithPhoto

    /// <summary>
    ///     ID: {a5bb501c-3169-4d72-8098-588f932815b1}
    ///     Alias: ProtocolReportsWithPhoto
    ///     Caption: $Views_Names_ProtocolReportsWithPhoto
    ///     Group: KrDocuments
    /// </summary>
    public class ProtocolReportsWithPhotoViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ProtocolReportsWithPhoto": {a5bb501c-3169-4d72-8098-588f932815b1}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa5bb501c,0x3169,0x4d72,0x80,0x98,0x58,0x8f,0x93,0x28,0x15,0xb1);

        /// <summary>
        ///     View name for "ProtocolReportsWithPhoto".
        /// </summary>
        public readonly string Alias = "ProtocolReportsWithPhoto";

        /// <summary>
        ///     View caption for "ProtocolReportsWithPhoto".
        /// </summary>
        public readonly string Caption = "$Views_Names_ProtocolReportsWithPhoto";

        /// <summary>
        ///     View group for "ProtocolReportsWithPhoto".
        /// </summary>
        public readonly string Group = "KrDocuments";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: PersonName
        ///     Caption: PersonName.
        /// </summary>
        public readonly ViewObject ColumnPersonName = new ViewObject(0, "PersonName", "PersonName");

        /// <summary>
        ///     ID:1 
        ///     Alias: Subject
        ///     Caption: Subject.
        /// </summary>
        public readonly ViewObject ColumnSubject = new ViewObject(1, "Subject", "Subject");

        /// <summary>
        ///     ID:2 
        ///     Alias: PhotoFileID
        ///     Caption: PhotoFileID.
        /// </summary>
        public readonly ViewObject ColumnPhotoFileID = new ViewObject(2, "PhotoFileID", "PhotoFileID");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CardId
        ///     Caption: CardId.
        /// </summary>
        public readonly ViewObject ParamCardId = new ViewObject(0, "CardId", "CardId");

        #endregion

        #region ToString

        public static implicit operator string(ProtocolReportsWithPhotoViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Protocols

    /// <summary>
    ///     ID: {775558fa-2ec3-4c38-819e-9395e94c28c7}
    ///     Alias: Protocols
    ///     Caption: $Views_Names_Protocols
    ///     Group: KrDocuments
    /// </summary>
    public class ProtocolsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Protocols": {775558fa-2ec3-4c38-819e-9395e94c28c7}.
        /// </summary>
        public readonly Guid ID = new Guid(0x775558fa,0x2ec3,0x4c38,0x81,0x9e,0x93,0x95,0xe9,0x4c,0x28,0xc7);

        /// <summary>
        ///     View name for "Protocols".
        /// </summary>
        public readonly string Alias = "Protocols";

        /// <summary>
        ///     View caption for "Protocols".
        /// </summary>
        public readonly string Caption = "$Views_Names_Protocols";

        /// <summary>
        ///     View group for "Protocols".
        /// </summary>
        public readonly string Group = "KrDocuments";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DocID.
        /// </summary>
        public readonly ViewObject ColumnDocID = new ViewObject(0, "DocID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DocNumber
        ///     Caption: $Views_Registers_Number.
        /// </summary>
        public readonly ViewObject ColumnDocNumber = new ViewObject(1, "DocNumber", "$Views_Registers_Number");

        /// <summary>
        ///     ID:2 
        ///     Alias: DocSubject
        ///     Caption: $Views_Registers_ProtocolSubject.
        /// </summary>
        public readonly ViewObject ColumnDocSubject = new ViewObject(2, "DocSubject", "$Views_Registers_ProtocolSubject");

        /// <summary>
        ///     ID:3 
        ///     Alias: DocDescription
        ///     Caption: $Views_Registers_DocDescription.
        /// </summary>
        public readonly ViewObject ColumnDocDescription = new ViewObject(3, "DocDescription", "$Views_Registers_DocDescription");

        /// <summary>
        ///     ID:4 
        ///     Alias: AuthorID.
        /// </summary>
        public readonly ViewObject ColumnAuthorID = new ViewObject(4, "AuthorID");

        /// <summary>
        ///     ID:5 
        ///     Alias: AuthorName
        ///     Caption: $Views_Registers_Secretary.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(5, "AuthorName", "$Views_Registers_Secretary");

        /// <summary>
        ///     ID:6 
        ///     Alias: RegistratorID.
        /// </summary>
        public readonly ViewObject ColumnRegistratorID = new ViewObject(6, "RegistratorID");

        /// <summary>
        ///     ID:7 
        ///     Alias: RegistratorName
        ///     Caption: $Views_Registers_Registrator.
        /// </summary>
        public readonly ViewObject ColumnRegistratorName = new ViewObject(7, "RegistratorName", "$Views_Registers_Registrator");

        /// <summary>
        ///     ID:8 
        ///     Alias: ProtocolDate
        ///     Caption: $Views_Registers_ProtocolDate.
        /// </summary>
        public readonly ViewObject ColumnProtocolDate = new ViewObject(8, "ProtocolDate", "$Views_Registers_ProtocolDate");

        /// <summary>
        ///     ID:9 
        ///     Alias: CreationDate
        ///     Caption: $Views_Registers_CreationDate.
        /// </summary>
        public readonly ViewObject ColumnCreationDate = new ViewObject(9, "CreationDate", "$Views_Registers_CreationDate");

        /// <summary>
        ///     ID:10 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department.
        /// </summary>
        public readonly ViewObject ColumnDepartment = new ViewObject(10, "Department", "$Views_Registers_Department");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: IsAuthor
        ///     Caption: $Views_Registers_IsSecretary_Param.
        /// </summary>
        public readonly ViewObject ParamIsAuthor = new ViewObject(0, "IsAuthor", "$Views_Registers_IsSecretary_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: IsRegistrator
        ///     Caption: $Views_Registers_IsRegistrator_Param.
        /// </summary>
        public readonly ViewObject ParamIsRegistrator = new ViewObject(1, "IsRegistrator", "$Views_Registers_IsRegistrator_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Number
        ///     Caption: $Views_Registers_Number_Param.
        /// </summary>
        public readonly ViewObject ParamNumber = new ViewObject(2, "Number", "$Views_Registers_Number_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Subject
        ///     Caption: $Views_Registers_ProtocolSubject_Param.
        /// </summary>
        public readonly ViewObject ParamSubject = new ViewObject(3, "Subject", "$Views_Registers_ProtocolSubject_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: ProtocolDate
        ///     Caption: $Views_Registers_ProtocolDate_Param.
        /// </summary>
        public readonly ViewObject ParamProtocolDate = new ViewObject(4, "ProtocolDate", "$Views_Registers_ProtocolDate_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Author
        ///     Caption: $Views_Registers_Secretary_Param.
        /// </summary>
        public readonly ViewObject ParamAuthor = new ViewObject(5, "Author", "$Views_Registers_Secretary_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Registrator
        ///     Caption: $Views_Registers_Registrator_Param.
        /// </summary>
        public readonly ViewObject ParamRegistrator = new ViewObject(6, "Registrator", "$Views_Registers_Registrator_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: Department
        ///     Caption: $Views_Registers_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(7, "Department", "$Views_Registers_Department_Param");

        #endregion

        #region ToString

        public static implicit operator string(ProtocolsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region RefDocumentsLookup

    /// <summary>
    ///     ID: {57fb8582-bfe3-4ae9-8ee3-1feb96b18803}
    ///     Alias: RefDocumentsLookup
    ///     Caption: $Views_Names_RefDocumentsLookup
    ///     Group: KrDocuments
    /// </summary>
    public class RefDocumentsLookupViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "RefDocumentsLookup": {57fb8582-bfe3-4ae9-8ee3-1feb96b18803}.
        /// </summary>
        public readonly Guid ID = new Guid(0x57fb8582,0xbfe3,0x4ae9,0x8e,0xe3,0x1f,0xeb,0x96,0xb1,0x88,0x03);

        /// <summary>
        ///     View name for "RefDocumentsLookup".
        /// </summary>
        public readonly string Alias = "RefDocumentsLookup";

        /// <summary>
        ///     View caption for "RefDocumentsLookup".
        /// </summary>
        public readonly string Caption = "$Views_Names_RefDocumentsLookup";

        /// <summary>
        ///     View group for "RefDocumentsLookup".
        /// </summary>
        public readonly string Group = "KrDocuments";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DocID.
        /// </summary>
        public readonly ViewObject ColumnDocID = new ViewObject(0, "DocID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DocDescription
        ///     Caption: $Views_RefDocumentsLookup_Description.
        /// </summary>
        public readonly ViewObject ColumnDocDescription = new ViewObject(1, "DocDescription", "$Views_RefDocumentsLookup_Description");

        /// <summary>
        ///     ID:2 
        ///     Alias: DocTypeName
        ///     Caption: $Views_RefDocumentsLookup_Type.
        /// </summary>
        public readonly ViewObject ColumnDocTypeName = new ViewObject(2, "DocTypeName", "$Views_RefDocumentsLookup_Type");

        /// <summary>
        ///     ID:3 
        ///     Alias: Date
        ///     Caption: $Views_RefDocumentsLookup_Date.
        /// </summary>
        public readonly ViewObject ColumnDate = new ViewObject(3, "Date", "$Views_RefDocumentsLookup_Date");

        /// <summary>
        ///     ID:4 
        ///     Alias: PartnerName
        ///     Caption: $Views_RefDocumentsLookup_Partner.
        /// </summary>
        public readonly ViewObject ColumnPartnerName = new ViewObject(4, "PartnerName", "$Views_RefDocumentsLookup_Partner");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Description
        ///     Caption: $Views_RefDocumentsLookup_Description_Param.
        /// </summary>
        public readonly ViewObject ParamDescription = new ViewObject(0, "Description", "$Views_RefDocumentsLookup_Description_Param");

        #endregion

        #region ToString

        public static implicit operator string(RefDocumentsLookupViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ReportCurrentTasksByDepartment

    /// <summary>
    ///     ID: {48224a01-8731-4ac1-a4f0-749ee5f375d2}
    ///     Alias: ReportCurrentTasksByDepartment
    ///     Caption: $Views_Names_ReportCurrentTasksByDepartment
    ///     Group: System
    /// </summary>
    public class ReportCurrentTasksByDepartmentViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ReportCurrentTasksByDepartment": {48224a01-8731-4ac1-a4f0-749ee5f375d2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x48224a01,0x8731,0x4ac1,0xa4,0xf0,0x74,0x9e,0xe5,0xf3,0x75,0xd2);

        /// <summary>
        ///     View name for "ReportCurrentTasksByDepartment".
        /// </summary>
        public readonly string Alias = "ReportCurrentTasksByDepartment";

        /// <summary>
        ///     View caption for "ReportCurrentTasksByDepartment".
        /// </summary>
        public readonly string Caption = "$Views_Names_ReportCurrentTasksByDepartment";

        /// <summary>
        ///     View group for "ReportCurrentTasksByDepartment".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DeptID.
        /// </summary>
        public readonly ViewObject ColumnDeptID = new ViewObject(0, "DeptID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DeptName
        ///     Caption: $Views_ReportCurrentTasksByDepartment_Department.
        /// </summary>
        public readonly ViewObject ColumnDeptName = new ViewObject(1, "DeptName", "$Views_ReportCurrentTasksByDepartment_Department");

        /// <summary>
        ///     ID:2 
        ///     Alias: New
        ///     Caption: $Views_ReportCurrentTasksByDepartment_New.
        /// </summary>
        public readonly ViewObject ColumnNew = new ViewObject(2, "New", "$Views_ReportCurrentTasksByDepartment_New");

        /// <summary>
        ///     ID:3 
        ///     Alias: NewDelayed
        ///     Caption: $Views_ReportCurrentTasksByDepartment_NewDelayed.
        /// </summary>
        public readonly ViewObject ColumnNewDelayed = new ViewObject(3, "NewDelayed", "$Views_ReportCurrentTasksByDepartment_NewDelayed");

        /// <summary>
        ///     ID:4 
        ///     Alias: NewAvgDelayPeriod
        ///     Caption: $Views_ReportCurrentTasksByDepartment_NewAvgDelayPeriod.
        /// </summary>
        public readonly ViewObject ColumnNewAvgDelayPeriod = new ViewObject(4, "NewAvgDelayPeriod", "$Views_ReportCurrentTasksByDepartment_NewAvgDelayPeriod");

        /// <summary>
        ///     ID:5 
        ///     Alias: InWork
        ///     Caption: $Views_ReportCurrentTasksByDepartment_InWork.
        /// </summary>
        public readonly ViewObject ColumnInWork = new ViewObject(5, "InWork", "$Views_ReportCurrentTasksByDepartment_InWork");

        /// <summary>
        ///     ID:6 
        ///     Alias: InWorkDelayed
        ///     Caption: $Views_ReportCurrentTasksByDepartment_InWorkDelayed.
        /// </summary>
        public readonly ViewObject ColumnInWorkDelayed = new ViewObject(6, "InWorkDelayed", "$Views_ReportCurrentTasksByDepartment_InWorkDelayed");

        /// <summary>
        ///     ID:7 
        ///     Alias: InWorkAvgDelayPeriod
        ///     Caption: $Views_ReportCurrentTasksByDepartment_InWorkAvgDelayPeriod.
        /// </summary>
        public readonly ViewObject ColumnInWorkAvgDelayPeriod = new ViewObject(7, "InWorkAvgDelayPeriod", "$Views_ReportCurrentTasksByDepartment_InWorkAvgDelayPeriod");

        /// <summary>
        ///     ID:8 
        ///     Alias: Postponed
        ///     Caption: $Views_ReportCurrentTasksByDepartment_Postponed.
        /// </summary>
        public readonly ViewObject ColumnPostponed = new ViewObject(8, "Postponed", "$Views_ReportCurrentTasksByDepartment_Postponed");

        /// <summary>
        ///     ID:9 
        ///     Alias: PostponedDelayed
        ///     Caption: $Views_ReportCurrentTasksByDepartment_PostponedDelayed.
        /// </summary>
        public readonly ViewObject ColumnPostponedDelayed = new ViewObject(9, "PostponedDelayed", "$Views_ReportCurrentTasksByDepartment_PostponedDelayed");

        /// <summary>
        ///     ID:10 
        ///     Alias: PostponedAvgDelayPeriod
        ///     Caption: $Views_ReportCurrentTasksByDepartment_PostponedAvgDelayPeriod.
        /// </summary>
        public readonly ViewObject ColumnPostponedAvgDelayPeriod = new ViewObject(10, "PostponedAvgDelayPeriod", "$Views_ReportCurrentTasksByDepartment_PostponedAvgDelayPeriod");

        /// <summary>
        ///     ID:11 
        ///     Alias: Total
        ///     Caption: $Views_ReportCurrentTasksByDepartment_Total.
        /// </summary>
        public readonly ViewObject ColumnTotal = new ViewObject(11, "Total", "$Views_ReportCurrentTasksByDepartment_Total");

        /// <summary>
        ///     ID:12 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(12, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: EndDate
        ///     Caption: $Views_ReportCurrentTasksByDepartment_EndDate_Param.
        /// </summary>
        public readonly ViewObject ParamEndDate = new ViewObject(0, "EndDate", "$Views_ReportCurrentTasksByDepartment_EndDate_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: CreationDate
        ///     Caption: $Views_ReportCurrentTasksByDepartment_CreationDate.
        /// </summary>
        public readonly ViewObject ParamCreationDate = new ViewObject(1, "CreationDate", "$Views_ReportCurrentTasksByDepartment_CreationDate");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeParam
        ///     Caption: $Views_ReportCurrentTasksByDepartment_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeParam = new ViewObject(2, "TypeParam", "$Views_ReportCurrentTasksByDepartment_Type_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: TaskType
        ///     Caption: $Views_ReportCurrentTasksByDepartment_TaskType_Param.
        /// </summary>
        public readonly ViewObject ParamTaskType = new ViewObject(3, "TaskType", "$Views_ReportCurrentTasksByDepartment_TaskType_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Department
        ///     Caption: $Views_ReportCurrentTasksByDepartment_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(4, "Department", "$Views_ReportCurrentTasksByDepartment_Department_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: SelUser
        ///     Caption: $Views_ReportCurrentTasksByDepartment_User_Param.
        /// </summary>
        public readonly ViewObject ParamSelUser = new ViewObject(5, "SelUser", "$Views_ReportCurrentTasksByDepartment_User_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Role
        ///     Caption: $Views_CompletedTasks_RoleGroup_Param.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(6, "Role", "$Views_CompletedTasks_RoleGroup_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: HideTotal
        ///     Caption: HideTotal.
        /// </summary>
        public readonly ViewObject ParamHideTotal = new ViewObject(7, "HideTotal", "HideTotal");

        /// <summary>
        ///     ID:8 
        ///     Alias: FunctionRolePerformerParam
        ///     Caption: $Views_MyTasks_FunctionRole_Performer_Param.
        /// </summary>
        public readonly ViewObject ParamFunctionRolePerformerParam = new ViewObject(8, "FunctionRolePerformerParam", "$Views_MyTasks_FunctionRole_Performer_Param");

        #endregion

        #region ToString

        public static implicit operator string(ReportCurrentTasksByDepartmentViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ReportCurrentTasksByDepUnpivoted

    /// <summary>
    ///     ID: {d36bf57a-20f1-4d77-8884-0d1465333380}
    ///     Alias: ReportCurrentTasksByDepUnpivoted
    ///     Caption: $Views_Names_ReportCurrentTasksByDepUnpivoted
    ///     Group: System
    /// </summary>
    public class ReportCurrentTasksByDepUnpivotedViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ReportCurrentTasksByDepUnpivoted": {d36bf57a-20f1-4d77-8884-0d1465333380}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd36bf57a,0x20f1,0x4d77,0x88,0x84,0x0d,0x14,0x65,0x33,0x33,0x80);

        /// <summary>
        ///     View name for "ReportCurrentTasksByDepUnpivoted".
        /// </summary>
        public readonly string Alias = "ReportCurrentTasksByDepUnpivoted";

        /// <summary>
        ///     View caption for "ReportCurrentTasksByDepUnpivoted".
        /// </summary>
        public readonly string Caption = "$Views_Names_ReportCurrentTasksByDepUnpivoted";

        /// <summary>
        ///     View group for "ReportCurrentTasksByDepUnpivoted".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DeptID.
        /// </summary>
        public readonly ViewObject ColumnDeptID = new ViewObject(0, "DeptID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DeptName
        ///     Caption: $Views_ReportCurrentTasksByDepartment_Department.
        /// </summary>
        public readonly ViewObject ColumnDeptName = new ViewObject(1, "DeptName", "$Views_ReportCurrentTasksByDepartment_Department");

        /// <summary>
        ///     ID:2 
        ///     Alias: Column
        ///     Caption: $Views_ReportCurrentTasksByDepartment_Column.
        /// </summary>
        public readonly ViewObject ColumnColumn = new ViewObject(2, "Column", "$Views_ReportCurrentTasksByDepartment_Column");

        /// <summary>
        ///     ID:3 
        ///     Alias: Value
        ///     Caption: $Views_ReportCurrentTasksByDepartment_Value.
        /// </summary>
        public readonly ViewObject ColumnValue = new ViewObject(3, "Value", "$Views_ReportCurrentTasksByDepartment_Value");

        /// <summary>
        ///     ID:4 
        ///     Alias: StateID.
        /// </summary>
        public readonly ViewObject ColumnStateID = new ViewObject(4, "StateID");

        /// <summary>
        ///     ID:5 
        ///     Alias: DelayIndex.
        /// </summary>
        public readonly ViewObject ColumnDelayIndex = new ViewObject(5, "DelayIndex");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: EndDate
        ///     Caption: $Views_ReportCurrentTasksByDepartment_EndDate_Param.
        /// </summary>
        public readonly ViewObject ParamEndDate = new ViewObject(0, "EndDate", "$Views_ReportCurrentTasksByDepartment_EndDate_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: CreationDate
        ///     Caption: $Views_ReportCurrentTasksByDepartment_CreationDate.
        /// </summary>
        public readonly ViewObject ParamCreationDate = new ViewObject(1, "CreationDate", "$Views_ReportCurrentTasksByDepartment_CreationDate");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeParam
        ///     Caption: $Views_ReportCurrentTasksByDepartment_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeParam = new ViewObject(2, "TypeParam", "$Views_ReportCurrentTasksByDepartment_Type_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: TaskType
        ///     Caption: $Views_ReportCurrentTasksByDepartment_TaskType_Param.
        /// </summary>
        public readonly ViewObject ParamTaskType = new ViewObject(3, "TaskType", "$Views_ReportCurrentTasksByDepartment_TaskType_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Department
        ///     Caption: $Views_ReportCurrentTasksByDepartment_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(4, "Department", "$Views_ReportCurrentTasksByDepartment_Department_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: SelUser
        ///     Caption: $Views_ReportCurrentTasksByDepartment_User_Param.
        /// </summary>
        public readonly ViewObject ParamSelUser = new ViewObject(5, "SelUser", "$Views_ReportCurrentTasksByDepartment_User_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Role
        ///     Caption: $Views_CompletedTasks_RoleGroup_Param.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(6, "Role", "$Views_CompletedTasks_RoleGroup_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: NoAvg
        ///     Caption: NoAvg.
        /// </summary>
        public readonly ViewObject ParamNoAvg = new ViewObject(7, "NoAvg", "NoAvg");

        /// <summary>
        ///     ID:8 
        ///     Alias: FunctionRolePerformerParam
        ///     Caption: $Views_MyTasks_FunctionRole_Performer_Param.
        /// </summary>
        public readonly ViewObject ParamFunctionRolePerformerParam = new ViewObject(8, "FunctionRolePerformerParam", "$Views_MyTasks_FunctionRole_Performer_Param");

        #endregion

        #region ToString

        public static implicit operator string(ReportCurrentTasksByDepUnpivotedViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ReportCurrentTasksByUser

    /// <summary>
    ///     ID: {871ad32d-0846-448c-beb2-85c1d357177b}
    ///     Alias: ReportCurrentTasksByUser
    ///     Caption: $Views_Names_ReportCurrentTasksByUser
    ///     Group: System
    /// </summary>
    public class ReportCurrentTasksByUserViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ReportCurrentTasksByUser": {871ad32d-0846-448c-beb2-85c1d357177b}.
        /// </summary>
        public readonly Guid ID = new Guid(0x871ad32d,0x0846,0x448c,0xbe,0xb2,0x85,0xc1,0xd3,0x57,0x17,0x7b);

        /// <summary>
        ///     View name for "ReportCurrentTasksByUser".
        /// </summary>
        public readonly string Alias = "ReportCurrentTasksByUser";

        /// <summary>
        ///     View caption for "ReportCurrentTasksByUser".
        /// </summary>
        public readonly string Caption = "$Views_Names_ReportCurrentTasksByUser";

        /// <summary>
        ///     View group for "ReportCurrentTasksByUser".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(0, "UserID");

        /// <summary>
        ///     ID:1 
        ///     Alias: UserName
        ///     Caption: $Views_ReportCurrentTasksByUser_User.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(1, "UserName", "$Views_ReportCurrentTasksByUser_User");

        /// <summary>
        ///     ID:2 
        ///     Alias: New
        ///     Caption: $Views_ReportCurrentTasksByUser_New.
        /// </summary>
        public readonly ViewObject ColumnNew = new ViewObject(2, "New", "$Views_ReportCurrentTasksByUser_New");

        /// <summary>
        ///     ID:3 
        ///     Alias: NewDelayed
        ///     Caption: $Views_ReportCurrentTasksByUser_NewDelayed.
        /// </summary>
        public readonly ViewObject ColumnNewDelayed = new ViewObject(3, "NewDelayed", "$Views_ReportCurrentTasksByUser_NewDelayed");

        /// <summary>
        ///     ID:4 
        ///     Alias: NewAvgDelayPeriod
        ///     Caption: $Views_ReportCurrentTasksByUser_NewAvgDelayPeriod.
        /// </summary>
        public readonly ViewObject ColumnNewAvgDelayPeriod = new ViewObject(4, "NewAvgDelayPeriod", "$Views_ReportCurrentTasksByUser_NewAvgDelayPeriod");

        /// <summary>
        ///     ID:5 
        ///     Alias: InWork
        ///     Caption: $Views_ReportCurrentTasksByUser_InWork.
        /// </summary>
        public readonly ViewObject ColumnInWork = new ViewObject(5, "InWork", "$Views_ReportCurrentTasksByUser_InWork");

        /// <summary>
        ///     ID:6 
        ///     Alias: InWorkDelayed
        ///     Caption: $Views_ReportCurrentTasksByUser_InWorkDelayed.
        /// </summary>
        public readonly ViewObject ColumnInWorkDelayed = new ViewObject(6, "InWorkDelayed", "$Views_ReportCurrentTasksByUser_InWorkDelayed");

        /// <summary>
        ///     ID:7 
        ///     Alias: InWorkAvgDelayPeriod
        ///     Caption: $Views_ReportCurrentTasksByUser_InWorkAvgDelayPeriod.
        /// </summary>
        public readonly ViewObject ColumnInWorkAvgDelayPeriod = new ViewObject(7, "InWorkAvgDelayPeriod", "$Views_ReportCurrentTasksByUser_InWorkAvgDelayPeriod");

        /// <summary>
        ///     ID:8 
        ///     Alias: Postponed
        ///     Caption: $Views_ReportCurrentTasksByUser_Postponed.
        /// </summary>
        public readonly ViewObject ColumnPostponed = new ViewObject(8, "Postponed", "$Views_ReportCurrentTasksByUser_Postponed");

        /// <summary>
        ///     ID:9 
        ///     Alias: PostponedDelayed
        ///     Caption: $Views_ReportCurrentTasksByUser_PostponedDelayed.
        /// </summary>
        public readonly ViewObject ColumnPostponedDelayed = new ViewObject(9, "PostponedDelayed", "$Views_ReportCurrentTasksByUser_PostponedDelayed");

        /// <summary>
        ///     ID:10 
        ///     Alias: PostponedAvgDelayPeriod
        ///     Caption: $Views_ReportCurrentTasksByUser_PostponedAvgDelayPeriod.
        /// </summary>
        public readonly ViewObject ColumnPostponedAvgDelayPeriod = new ViewObject(10, "PostponedAvgDelayPeriod", "$Views_ReportCurrentTasksByUser_PostponedAvgDelayPeriod");

        /// <summary>
        ///     ID:11 
        ///     Alias: Total
        ///     Caption: $Views_ReportCurrentTasksByUser_Total.
        /// </summary>
        public readonly ViewObject ColumnTotal = new ViewObject(11, "Total", "$Views_ReportCurrentTasksByUser_Total");

        /// <summary>
        ///     ID:12 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(12, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: EndDate
        ///     Caption: $Views_ReportCurrentTasksByUser_EndDate_Param.
        /// </summary>
        public readonly ViewObject ParamEndDate = new ViewObject(0, "EndDate", "$Views_ReportCurrentTasksByUser_EndDate_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: CreationDate
        ///     Caption: $Views_ReportCurrentTasksByUser_CreationDate_Param.
        /// </summary>
        public readonly ViewObject ParamCreationDate = new ViewObject(1, "CreationDate", "$Views_ReportCurrentTasksByUser_CreationDate_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeParam
        ///     Caption: $Views_ReportCurrentTasksByUser_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeParam = new ViewObject(2, "TypeParam", "$Views_ReportCurrentTasksByUser_Type_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: TaskType
        ///     Caption: $Views_ReportCurrentTasksByUser_TaskType_Param.
        /// </summary>
        public readonly ViewObject ParamTaskType = new ViewObject(3, "TaskType", "$Views_ReportCurrentTasksByUser_TaskType_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Department
        ///     Caption: $Views_ReportCurrentTasksByUser_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(4, "Department", "$Views_ReportCurrentTasksByUser_Department_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: SelUser
        ///     Caption: $Views_ReportCurrentTasksByUser_User_Param.
        /// </summary>
        public readonly ViewObject ParamSelUser = new ViewObject(5, "SelUser", "$Views_ReportCurrentTasksByUser_User_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Role
        ///     Caption: $Views_CompletedTasks_RoleGroup_Param.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(6, "Role", "$Views_CompletedTasks_RoleGroup_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: FunctionRolePerformerParam
        ///     Caption: $Views_MyTasks_FunctionRole_Performer_Param.
        /// </summary>
        public readonly ViewObject ParamFunctionRolePerformerParam = new ViewObject(7, "FunctionRolePerformerParam", "$Views_MyTasks_FunctionRole_Performer_Param");

        #endregion

        #region ToString

        public static implicit operator string(ReportCurrentTasksByUserViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ReportCurrentTasksRules

    /// <summary>
    ///     ID: {146b25b3-e61e-4259-bcc5-959040755de6}
    ///     Alias: ReportCurrentTasksRules
    ///     Caption: $Views_Names_ReportCurrentTasksRules
    ///     Group: System
    /// </summary>
    public class ReportCurrentTasksRulesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ReportCurrentTasksRules": {146b25b3-e61e-4259-bcc5-959040755de6}.
        /// </summary>
        public readonly Guid ID = new Guid(0x146b25b3,0xe61e,0x4259,0xbc,0xc5,0x95,0x90,0x40,0x75,0x5d,0xe6);

        /// <summary>
        ///     View name for "ReportCurrentTasksRules".
        /// </summary>
        public readonly string Alias = "ReportCurrentTasksRules";

        /// <summary>
        ///     View caption for "ReportCurrentTasksRules".
        /// </summary>
        public readonly string Caption = "$Views_Names_ReportCurrentTasksRules";

        /// <summary>
        ///     View group for "ReportCurrentTasksRules".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RuleID.
        /// </summary>
        public readonly ViewObject ColumnRuleID = new ViewObject(0, "RuleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RuleCaption
        ///     Caption: $Views_ReportCurrentTasksRules_Caption.
        /// </summary>
        public readonly ViewObject ColumnRuleCaption = new ViewObject(1, "RuleCaption", "$Views_ReportCurrentTasksRules_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: ActiveRoles
        ///     Caption: $Views_ReportCurrentTasksRules_ActiveRoles.
        /// </summary>
        public readonly ViewObject ColumnActiveRoles = new ViewObject(2, "ActiveRoles", "$Views_ReportCurrentTasksRules_ActiveRoles");

        /// <summary>
        ///     ID:3 
        ///     Alias: PassiveRoles
        ///     Caption: $Views_ReportCurrentTasksRules_PassiveRoles.
        /// </summary>
        public readonly ViewObject ColumnPassiveRoles = new ViewObject(3, "PassiveRoles", "$Views_ReportCurrentTasksRules_PassiveRoles");

        /// <summary>
        ///     ID:4 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(4, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_ReportCurrentTasksRules_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_ReportCurrentTasksRules_Caption_Param");

        #endregion

        #region ToString

        public static implicit operator string(ReportCurrentTasksRulesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ReportDocumentsByType

    /// <summary>
    ///     ID: {35ebb779-0abd-411b-bc6b-358d2e2cabca}
    ///     Alias: ReportDocumentsByType
    ///     Caption: $Views_Names_ReportDocumentsByType
    ///     Group: System
    /// </summary>
    public class ReportDocumentsByTypeViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ReportDocumentsByType": {35ebb779-0abd-411b-bc6b-358d2e2cabca}.
        /// </summary>
        public readonly Guid ID = new Guid(0x35ebb779,0x0abd,0x411b,0xbc,0x6b,0x35,0x8d,0x2e,0x2c,0xab,0xca);

        /// <summary>
        ///     View name for "ReportDocumentsByType".
        /// </summary>
        public readonly string Alias = "ReportDocumentsByType";

        /// <summary>
        ///     View caption for "ReportDocumentsByType".
        /// </summary>
        public readonly string Caption = "$Views_Names_ReportDocumentsByType";

        /// <summary>
        ///     View group for "ReportDocumentsByType".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeName
        ///     Caption: $Views_ReportDocumentsByType_TypeName.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(1, "TypeName", "$Views_ReportDocumentsByType_TypeName");

        /// <summary>
        ///     ID:2 
        ///     Alias: Total
        ///     Caption: $Views_ReportDocumentsByType_Total.
        /// </summary>
        public readonly ViewObject ColumnTotal = new ViewObject(2, "Total", "$Views_ReportDocumentsByType_Total");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: DocDate
        ///     Caption: $Views_ReportDocumentsByType_DocDate_Param.
        /// </summary>
        public readonly ViewObject ParamDocDate = new ViewObject(0, "DocDate", "$Views_ReportDocumentsByType_DocDate_Param");

        #endregion

        #region ToString

        public static implicit operator string(ReportDocumentsByTypeViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ReportPastTasksByDepartment

    /// <summary>
    ///     ID: {49562bb2-b059-4f06-9771-c5e38892ff6f}
    ///     Alias: ReportPastTasksByDepartment
    ///     Caption: $Views_Names_ReportPastTasksByDepartment
    ///     Group: System
    /// </summary>
    public class ReportPastTasksByDepartmentViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ReportPastTasksByDepartment": {49562bb2-b059-4f06-9771-c5e38892ff6f}.
        /// </summary>
        public readonly Guid ID = new Guid(0x49562bb2,0xb059,0x4f06,0x97,0x71,0xc5,0xe3,0x88,0x92,0xff,0x6f);

        /// <summary>
        ///     View name for "ReportPastTasksByDepartment".
        /// </summary>
        public readonly string Alias = "ReportPastTasksByDepartment";

        /// <summary>
        ///     View caption for "ReportPastTasksByDepartment".
        /// </summary>
        public readonly string Caption = "$Views_Names_ReportPastTasksByDepartment";

        /// <summary>
        ///     View group for "ReportPastTasksByDepartment".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: DeptID.
        /// </summary>
        public readonly ViewObject ColumnDeptID = new ViewObject(0, "DeptID");

        /// <summary>
        ///     ID:1 
        ///     Alias: DeptName
        ///     Caption: $Views_ReportPastTasksByDepartment_Department.
        /// </summary>
        public readonly ViewObject ColumnDeptName = new ViewObject(1, "DeptName", "$Views_ReportPastTasksByDepartment_Department");

        /// <summary>
        ///     ID:2 
        ///     Alias: OnTime
        ///     Caption: $Views_ReportPastTasksByDepartment_OnTime.
        /// </summary>
        public readonly ViewObject ColumnOnTime = new ViewObject(2, "OnTime", "$Views_ReportPastTasksByDepartment_OnTime");

        /// <summary>
        ///     ID:3 
        ///     Alias: Overdue
        ///     Caption: $Views_ReportPastTasksByDepartment_Overdue.
        /// </summary>
        public readonly ViewObject ColumnOverdue = new ViewObject(3, "Overdue", "$Views_ReportPastTasksByDepartment_Overdue");

        /// <summary>
        ///     ID:4 
        ///     Alias: OverdueAvgDelayPeriod
        ///     Caption: $Views_ReportPastTasksByDepartment_OverdueAvgDelayPeriod.
        /// </summary>
        public readonly ViewObject ColumnOverdueAvgDelayPeriod = new ViewObject(4, "OverdueAvgDelayPeriod", "$Views_ReportPastTasksByDepartment_OverdueAvgDelayPeriod");

        /// <summary>
        ///     ID:5 
        ///     Alias: Total
        ///     Caption: $Views_ReportPastTasksByDepartment_Total.
        /// </summary>
        public readonly ViewObject ColumnTotal = new ViewObject(5, "Total", "$Views_ReportPastTasksByDepartment_Total");

        /// <summary>
        ///     ID:6 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(6, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CompletionDate
        ///     Caption: $Views_ReportPastTasksByDepartment_CompletionDate.
        /// </summary>
        public readonly ViewObject ParamCompletionDate = new ViewObject(0, "CompletionDate", "$Views_ReportPastTasksByDepartment_CompletionDate");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeParam
        ///     Caption: $Views_ReportPastTasksByDepartment_DocType_Param.
        /// </summary>
        public readonly ViewObject ParamTypeParam = new ViewObject(1, "TypeParam", "$Views_ReportPastTasksByDepartment_DocType_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: TaskType
        ///     Caption: $Views_ReportPastTasksByDepartment_TaskType_Param.
        /// </summary>
        public readonly ViewObject ParamTaskType = new ViewObject(2, "TaskType", "$Views_ReportPastTasksByDepartment_TaskType_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Department
        ///     Caption: $Views_ReportPastTasksByDepartment_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(3, "Department", "$Views_ReportPastTasksByDepartment_Department_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: SelUser
        ///     Caption: $Views_ReportPastTasksByDepartment_User_Param.
        /// </summary>
        public readonly ViewObject ParamSelUser = new ViewObject(4, "SelUser", "$Views_ReportPastTasksByDepartment_User_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Role
        ///     Caption: $Views_CompletedTasks_RoleGroup_Param.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(5, "Role", "$Views_CompletedTasks_RoleGroup_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Option
        ///     Caption: $Views_ReportPastTasksByDepartment_Option_Param.
        /// </summary>
        public readonly ViewObject ParamOption = new ViewObject(6, "Option", "$Views_ReportPastTasksByDepartment_Option_Param");

        #endregion

        #region ToString

        public static implicit operator string(ReportPastTasksByDepartmentViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ReportPastTasksByUser

    /// <summary>
    ///     ID: {44b3c3ba-6dc4-4b3b-bd1e-48f939c3a8a1}
    ///     Alias: ReportPastTasksByUser
    ///     Caption: $Views_Names_ReportPastTasksByUser
    ///     Group: System
    /// </summary>
    public class ReportPastTasksByUserViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ReportPastTasksByUser": {44b3c3ba-6dc4-4b3b-bd1e-48f939c3a8a1}.
        /// </summary>
        public readonly Guid ID = new Guid(0x44b3c3ba,0x6dc4,0x4b3b,0xbd,0x1e,0x48,0xf9,0x39,0xc3,0xa8,0xa1);

        /// <summary>
        ///     View name for "ReportPastTasksByUser".
        /// </summary>
        public readonly string Alias = "ReportPastTasksByUser";

        /// <summary>
        ///     View caption for "ReportPastTasksByUser".
        /// </summary>
        public readonly string Caption = "$Views_Names_ReportPastTasksByUser";

        /// <summary>
        ///     View group for "ReportPastTasksByUser".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(0, "UserID");

        /// <summary>
        ///     ID:1 
        ///     Alias: UserName
        ///     Caption: $Views_ReportPastTasksByUser_User.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(1, "UserName", "$Views_ReportPastTasksByUser_User");

        /// <summary>
        ///     ID:2 
        ///     Alias: OnTime
        ///     Caption: $Views_ReportPastTasksByUser_OnTime.
        /// </summary>
        public readonly ViewObject ColumnOnTime = new ViewObject(2, "OnTime", "$Views_ReportPastTasksByUser_OnTime");

        /// <summary>
        ///     ID:3 
        ///     Alias: Overdue
        ///     Caption: $Views_ReportPastTasksByUser_Overdue.
        /// </summary>
        public readonly ViewObject ColumnOverdue = new ViewObject(3, "Overdue", "$Views_ReportPastTasksByUser_Overdue");

        /// <summary>
        ///     ID:4 
        ///     Alias: OverdueAvgDelayPeriod
        ///     Caption: $Views_ReportPastTasksByUser_OverdueAvgDelayPeriod.
        /// </summary>
        public readonly ViewObject ColumnOverdueAvgDelayPeriod = new ViewObject(4, "OverdueAvgDelayPeriod", "$Views_ReportPastTasksByUser_OverdueAvgDelayPeriod");

        /// <summary>
        ///     ID:5 
        ///     Alias: Total
        ///     Caption: $Views_ReportPastTasksByUser_Total.
        /// </summary>
        public readonly ViewObject ColumnTotal = new ViewObject(5, "Total", "$Views_ReportPastTasksByUser_Total");

        /// <summary>
        ///     ID:6 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(6, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CompletionDate
        ///     Caption: $Views_ReportPastTasksByUser_CompletionDate_Param.
        /// </summary>
        public readonly ViewObject ParamCompletionDate = new ViewObject(0, "CompletionDate", "$Views_ReportPastTasksByUser_CompletionDate_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeParam
        ///     Caption: $Views_ReportPastTasksByUser_TypeParam_Param.
        /// </summary>
        public readonly ViewObject ParamTypeParam = new ViewObject(1, "TypeParam", "$Views_ReportPastTasksByUser_TypeParam_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: TaskType
        ///     Caption: $Views_ReportPastTasksByUser_TaskType_Param.
        /// </summary>
        public readonly ViewObject ParamTaskType = new ViewObject(2, "TaskType", "$Views_ReportPastTasksByUser_TaskType_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: Department
        ///     Caption: $Views_ReportPastTasksByUser_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(3, "Department", "$Views_ReportPastTasksByUser_Department_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: SelUser
        ///     Caption: $Views_ReportPastTasksByUser_User_Param.
        /// </summary>
        public readonly ViewObject ParamSelUser = new ViewObject(4, "SelUser", "$Views_ReportPastTasksByUser_User_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: Role
        ///     Caption: $Views_CompletedTasks_RoleGroup_Param.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(5, "Role", "$Views_CompletedTasks_RoleGroup_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: Option
        ///     Caption: $Views_ReportPastTasksByUser_Option_Param.
        /// </summary>
        public readonly ViewObject ParamOption = new ViewObject(6, "Option", "$Views_ReportPastTasksByUser_Option_Param");

        #endregion

        #region ToString

        public static implicit operator string(ReportPastTasksByUserViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region RoleDeputies

    /// <summary>
    ///     ID: {181eafda-939b-4b38-9f71-21c699133396}
    ///     Alias: RoleDeputies
    ///     Caption: $Views_Names_RoleDeputies
    ///     Group: System
    /// </summary>
    public class RoleDeputiesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "RoleDeputies": {181eafda-939b-4b38-9f71-21c699133396}.
        /// </summary>
        public readonly Guid ID = new Guid(0x181eafda,0x939b,0x4b38,0x9f,0x71,0x21,0xc6,0x99,0x13,0x33,0x96);

        /// <summary>
        ///     View name for "RoleDeputies".
        /// </summary>
        public readonly string Alias = "RoleDeputies";

        /// <summary>
        ///     View caption for "RoleDeputies".
        /// </summary>
        public readonly string Caption = "$Views_Names_RoleDeputies";

        /// <summary>
        ///     View group for "RoleDeputies".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RoleDeputyID.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyID = new ViewObject(0, "RoleDeputyID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleDeputyName
        ///     Caption: $Views_RoleDeputies_RoleDeputy.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyName = new ViewObject(1, "RoleDeputyName", "$Views_RoleDeputies_RoleDeputy");

        /// <summary>
        ///     ID:2 
        ///     Alias: RoleDeputizedID.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputizedID = new ViewObject(2, "RoleDeputizedID");

        /// <summary>
        ///     ID:3 
        ///     Alias: RoleDeputizedName
        ///     Caption: $Views_RoleDeputies_RoleDeputized.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputizedName = new ViewObject(3, "RoleDeputizedName", "$Views_RoleDeputies_RoleDeputized");

        /// <summary>
        ///     ID:4 
        ///     Alias: RoleDeputyFrom
        ///     Caption: $Views_RoleDeputies_RoleDeputyFrom.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyFrom = new ViewObject(4, "RoleDeputyFrom", "$Views_RoleDeputies_RoleDeputyFrom");

        /// <summary>
        ///     ID:5 
        ///     Alias: RoleDeputyTo
        ///     Caption: $Views_RoleDeputies_RoleDeputyTo.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyTo = new ViewObject(5, "RoleDeputyTo", "$Views_RoleDeputies_RoleDeputyTo");

        /// <summary>
        ///     ID:6 
        ///     Alias: RoleDeputyIsPermanent
        ///     Caption: $Views_RoleDeputies_RoleDeputyIsPermanent.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyIsPermanent = new ViewObject(6, "RoleDeputyIsPermanent", "$Views_RoleDeputies_RoleDeputyIsPermanent");

        /// <summary>
        ///     ID:7 
        ///     Alias: RoleDeputyIsEnabled
        ///     Caption: $Views_RoleDeputies_RoleDeputyAvailable.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyIsEnabled = new ViewObject(7, "RoleDeputyIsEnabled", "$Views_RoleDeputies_RoleDeputyAvailable");

        /// <summary>
        ///     ID:8 
        ///     Alias: RoleDeputyIsActive
        ///     Caption: $Views_RoleDeputies_RoleDeputyIsActive.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyIsActive = new ViewObject(8, "RoleDeputyIsActive", "$Views_RoleDeputies_RoleDeputyIsActive");

        /// <summary>
        ///     ID:9 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(9, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Role
        ///     Caption: $Views_RoleDeputies_Role.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(0, "Role", "$Views_RoleDeputies_Role");

        /// <summary>
        ///     ID:1 
        ///     Alias: Deputized
        ///     Caption: $Views_RoleDeputies_RoleDeputized.
        /// </summary>
        public readonly ViewObject ParamDeputized = new ViewObject(1, "Deputized", "$Views_RoleDeputies_RoleDeputized");

        /// <summary>
        ///     ID:2 
        ///     Alias: Deputy
        ///     Caption: $Views_RoleDeputies_RoleDeputy.
        /// </summary>
        public readonly ViewObject ParamDeputy = new ViewObject(2, "Deputy", "$Views_RoleDeputies_RoleDeputy");

        /// <summary>
        ///     ID:3 
        ///     Alias: AvailableOnDate
        ///     Caption: $Views_RoleDeputies_AvailableOnDate.
        /// </summary>
        public readonly ViewObject ParamAvailableOnDate = new ViewObject(3, "AvailableOnDate", "$Views_RoleDeputies_AvailableOnDate");

        /// <summary>
        ///     ID:4 
        ///     Alias: IsEnabled
        ///     Caption: $Views_RoleDeputies_RoleDeputyAvailable.
        /// </summary>
        public readonly ViewObject ParamIsEnabled = new ViewObject(4, "IsEnabled", "$Views_RoleDeputies_RoleDeputyAvailable");

        /// <summary>
        ///     ID:5 
        ///     Alias: IsActive
        ///     Caption: $Views_RoleDeputies_RoleDeputyIsActive.
        /// </summary>
        public readonly ViewObject ParamIsActive = new ViewObject(5, "IsActive", "$Views_RoleDeputies_RoleDeputyIsActive");

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region RoleDeputiesManagementDeputized

    /// <summary>
    ///     ID: {2877fb32-b5fa-40ed-94d2-32dcd675afc1}
    ///     Alias: RoleDeputiesManagementDeputized
    ///     Caption: $Views_Names_RoleDeputiesManagementDeputized
    ///     Group: System
    /// </summary>
    public class RoleDeputiesManagementDeputizedViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "RoleDeputiesManagementDeputized": {2877fb32-b5fa-40ed-94d2-32dcd675afc1}.
        /// </summary>
        public readonly Guid ID = new Guid(0x2877fb32,0xb5fa,0x40ed,0x94,0xd2,0x32,0xdc,0xd6,0x75,0xaf,0xc1);

        /// <summary>
        ///     View name for "RoleDeputiesManagementDeputized".
        /// </summary>
        public readonly string Alias = "RoleDeputiesManagementDeputized";

        /// <summary>
        ///     View caption for "RoleDeputiesManagementDeputized".
        /// </summary>
        public readonly string Caption = "$Views_Names_RoleDeputiesManagementDeputized";

        /// <summary>
        ///     View group for "RoleDeputiesManagementDeputized".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RoleDeputizedRoles
        ///     Caption: $Views_RoleDeputiesManagementDeputized_Roles.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputizedRoles = new ViewObject(0, "RoleDeputizedRoles", "$Views_RoleDeputiesManagementDeputized_Roles");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleDeputizedID.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputizedID = new ViewObject(1, "RoleDeputizedID");

        /// <summary>
        ///     ID:2 
        ///     Alias: RoleDeputizedName
        ///     Caption: $Views_RoleDeputies_RoleDeputized.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputizedName = new ViewObject(2, "RoleDeputizedName", "$Views_RoleDeputies_RoleDeputized");

        /// <summary>
        ///     ID:3 
        ///     Alias: RoleDeputyFrom
        ///     Caption: $Views_RoleDeputies_RoleDeputyFrom.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyFrom = new ViewObject(3, "RoleDeputyFrom", "$Views_RoleDeputies_RoleDeputyFrom");

        /// <summary>
        ///     ID:4 
        ///     Alias: RoleDeputyTo
        ///     Caption: $Views_RoleDeputies_RoleDeputyTo.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyTo = new ViewObject(4, "RoleDeputyTo", "$Views_RoleDeputies_RoleDeputyTo");

        /// <summary>
        ///     ID:5 
        ///     Alias: RoleDeputyIsPermanent
        ///     Caption: $Views_RoleDeputies_RoleDeputyIsPermanent.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyIsPermanent = new ViewObject(5, "RoleDeputyIsPermanent", "$Views_RoleDeputies_RoleDeputyIsPermanent");

        /// <summary>
        ///     ID:6 
        ///     Alias: RoleDeputyIsEnabled
        ///     Caption: $Views_RoleDeputies_RoleDeputyAvailable.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyIsEnabled = new ViewObject(6, "RoleDeputyIsEnabled", "$Views_RoleDeputies_RoleDeputyAvailable");

        /// <summary>
        ///     ID:7 
        ///     Alias: RoleDeputyIsActive
        ///     Caption: $Views_RoleDeputies_RoleDeputyIsActive.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyIsActive = new ViewObject(7, "RoleDeputyIsActive", "$Views_RoleDeputies_RoleDeputyIsActive");

        /// <summary>
        ///     ID:8 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(8, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Deputized
        ///     Caption: $Views_RoleDeputies_RoleDeputized.
        /// </summary>
        public readonly ViewObject ParamDeputized = new ViewObject(0, "Deputized", "$Views_RoleDeputies_RoleDeputized");

        /// <summary>
        ///     ID:1 
        ///     Alias: Deputy
        ///     Caption: $Views_RoleDeputies_RoleDeputy.
        /// </summary>
        public readonly ViewObject ParamDeputy = new ViewObject(1, "Deputy", "$Views_RoleDeputies_RoleDeputy");

        /// <summary>
        ///     ID:2 
        ///     Alias: AvailableOnDate
        ///     Caption: $Views_RoleDeputies_AvailableOnDate.
        /// </summary>
        public readonly ViewObject ParamAvailableOnDate = new ViewObject(2, "AvailableOnDate", "$Views_RoleDeputies_AvailableOnDate");

        /// <summary>
        ///     ID:3 
        ///     Alias: IsEnabled
        ///     Caption: $Views_RoleDeputies_RoleDeputyAvailable.
        /// </summary>
        public readonly ViewObject ParamIsEnabled = new ViewObject(3, "IsEnabled", "$Views_RoleDeputies_RoleDeputyAvailable");

        /// <summary>
        ///     ID:4 
        ///     Alias: IsActive
        ///     Caption: $Views_RoleDeputies_RoleDeputyIsActive.
        /// </summary>
        public readonly ViewObject ParamIsActive = new ViewObject(4, "IsActive", "$Views_RoleDeputies_RoleDeputyIsActive");

        /// <summary>
        ///     ID:5 
        ///     Alias: CardType
        ///     Caption: $Views_RoleDeputies_CardType.
        /// </summary>
        public readonly ViewObject ParamCardType = new ViewObject(5, "CardType", "$Views_RoleDeputies_CardType");

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesManagementDeputizedViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region RoleDeputiesNew

    /// <summary>
    ///     ID: {87c8a806-31fc-4d45-b6a0-cb1028d9fc24}
    ///     Alias: RoleDeputiesNew
    ///     Caption: $Views_Names_RoleDeputiesNew
    ///     Group: System
    /// </summary>
    public class RoleDeputiesNewViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "RoleDeputiesNew": {87c8a806-31fc-4d45-b6a0-cb1028d9fc24}.
        /// </summary>
        public readonly Guid ID = new Guid(0x87c8a806,0x31fc,0x4d45,0xb6,0xa0,0xcb,0x10,0x28,0xd9,0xfc,0x24);

        /// <summary>
        ///     View name for "RoleDeputiesNew".
        /// </summary>
        public readonly string Alias = "RoleDeputiesNew";

        /// <summary>
        ///     View caption for "RoleDeputiesNew".
        /// </summary>
        public readonly string Caption = "$Views_Names_RoleDeputiesNew";

        /// <summary>
        ///     View group for "RoleDeputiesNew".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RoleDeputyID.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyID = new ViewObject(0, "RoleDeputyID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleDeputyName
        ///     Caption: $Views_RoleDeputies_RoleDeputy.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyName = new ViewObject(1, "RoleDeputyName", "$Views_RoleDeputies_RoleDeputy");

        /// <summary>
        ///     ID:2 
        ///     Alias: RoleDeputizedID.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputizedID = new ViewObject(2, "RoleDeputizedID");

        /// <summary>
        ///     ID:3 
        ///     Alias: RoleDeputizedName
        ///     Caption: $Views_RoleDeputies_RoleDeputized.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputizedName = new ViewObject(3, "RoleDeputizedName", "$Views_RoleDeputies_RoleDeputized");

        /// <summary>
        ///     ID:4 
        ///     Alias: RoleDeputyFrom
        ///     Caption: $Views_RoleDeputies_RoleDeputyFrom.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyFrom = new ViewObject(4, "RoleDeputyFrom", "$Views_RoleDeputies_RoleDeputyFrom");

        /// <summary>
        ///     ID:5 
        ///     Alias: RoleDeputyTo
        ///     Caption: $Views_RoleDeputies_RoleDeputyTo.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyTo = new ViewObject(5, "RoleDeputyTo", "$Views_RoleDeputies_RoleDeputyTo");

        /// <summary>
        ///     ID:6 
        ///     Alias: RoleDeputyIsPermanent
        ///     Caption: $Views_RoleDeputies_RoleDeputyIsPermanent.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyIsPermanent = new ViewObject(6, "RoleDeputyIsPermanent", "$Views_RoleDeputies_RoleDeputyIsPermanent");

        /// <summary>
        ///     ID:7 
        ///     Alias: RoleDeputyIsEnabled
        ///     Caption: $Views_RoleDeputies_RoleDeputyAvailable.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyIsEnabled = new ViewObject(7, "RoleDeputyIsEnabled", "$Views_RoleDeputies_RoleDeputyAvailable");

        /// <summary>
        ///     ID:8 
        ///     Alias: RoleDeputyIsActive
        ///     Caption: $Views_RoleDeputies_RoleDeputyIsActive.
        /// </summary>
        public readonly ViewObject ColumnRoleDeputyIsActive = new ViewObject(8, "RoleDeputyIsActive", "$Views_RoleDeputies_RoleDeputyIsActive");

        /// <summary>
        ///     ID:9 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(9, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Role
        ///     Caption: $Views_RoleDeputies_Role.
        /// </summary>
        public readonly ViewObject ParamRole = new ViewObject(0, "Role", "$Views_RoleDeputies_Role");

        /// <summary>
        ///     ID:1 
        ///     Alias: Deputized
        ///     Caption: $Views_RoleDeputies_RoleDeputized.
        /// </summary>
        public readonly ViewObject ParamDeputized = new ViewObject(1, "Deputized", "$Views_RoleDeputies_RoleDeputized");

        /// <summary>
        ///     ID:2 
        ///     Alias: Deputy
        ///     Caption: $Views_RoleDeputies_RoleDeputy.
        /// </summary>
        public readonly ViewObject ParamDeputy = new ViewObject(2, "Deputy", "$Views_RoleDeputies_RoleDeputy");

        /// <summary>
        ///     ID:3 
        ///     Alias: AvailableOnDate
        ///     Caption: $Views_RoleDeputies_AvailableOnDate.
        /// </summary>
        public readonly ViewObject ParamAvailableOnDate = new ViewObject(3, "AvailableOnDate", "$Views_RoleDeputies_AvailableOnDate");

        /// <summary>
        ///     ID:4 
        ///     Alias: IsEnabled
        ///     Caption: $Views_RoleDeputies_RoleDeputyAvailable.
        /// </summary>
        public readonly ViewObject ParamIsEnabled = new ViewObject(4, "IsEnabled", "$Views_RoleDeputies_RoleDeputyAvailable");

        /// <summary>
        ///     ID:5 
        ///     Alias: IsActive
        ///     Caption: $Views_RoleDeputies_RoleDeputyIsActive.
        /// </summary>
        public readonly ViewObject ParamIsActive = new ViewObject(5, "IsActive", "$Views_RoleDeputies_RoleDeputyIsActive");

        #endregion

        #region ToString

        public static implicit operator string(RoleDeputiesNewViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region RoleGenerators

    /// <summary>
    ///     ID: {e8163ca6-19e3-09ce-bb88-efcac757a8f4}
    ///     Alias: RoleGenerators
    ///     Caption: $Views_Names_RoleGenerators
    ///     Group: System
    /// </summary>
    public class RoleGeneratorsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "RoleGenerators": {e8163ca6-19e3-09ce-bb88-efcac757a8f4}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe8163ca6,0x19e3,0x09ce,0xbb,0x88,0xef,0xca,0xc7,0x57,0xa8,0xf4);

        /// <summary>
        ///     View name for "RoleGenerators".
        /// </summary>
        public readonly string Alias = "RoleGenerators";

        /// <summary>
        ///     View caption for "RoleGenerators".
        /// </summary>
        public readonly string Caption = "$Views_Names_RoleGenerators";

        /// <summary>
        ///     View group for "RoleGenerators".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: GeneratorID.
        /// </summary>
        public readonly ViewObject ColumnGeneratorID = new ViewObject(0, "GeneratorID");

        /// <summary>
        ///     ID:1 
        ///     Alias: GeneratorName
        ///     Caption: $Views_RoleGenerators_Name.
        /// </summary>
        public readonly ViewObject ColumnGeneratorName = new ViewObject(1, "GeneratorName", "$Views_RoleGenerators_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(2, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_RoleGenerators_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_RoleGenerators_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(RoleGeneratorsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Roles

    /// <summary>
    ///     ID: {e168749e-d123-4833-a820-77c9dae80f05}
    ///     Alias: Roles
    ///     Caption: $Views_Names_Roles
    ///     Group: System
    /// </summary>
    public class RolesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Roles": {e168749e-d123-4833-a820-77c9dae80f05}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe168749e,0xd123,0x4833,0xa8,0x20,0x77,0xc9,0xda,0xe8,0x0f,0x05);

        /// <summary>
        ///     View name for "Roles".
        /// </summary>
        public readonly string Alias = "Roles";

        /// <summary>
        ///     View caption for "Roles".
        /// </summary>
        public readonly string Caption = "$Views_Names_Roles";

        /// <summary>
        ///     View group for "Roles".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RoleID.
        /// </summary>
        public readonly ViewObject ColumnRoleID = new ViewObject(0, "RoleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleName
        ///     Caption: $Views_Roles_Role.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(1, "RoleName", "$Views_Roles_Role");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeName
        ///     Caption: $Views_Roles_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(2, "TypeName", "$Views_Roles_Type");

        /// <summary>
        ///     ID:3 
        ///     Alias: Info
        ///     Caption: $Views_Roles_Info.
        /// </summary>
        public readonly ViewObject ColumnInfo = new ViewObject(3, "Info", "$Views_Roles_Info");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Roles_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Roles_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeID
        ///     Caption: $Views_Roles_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeID = new ViewObject(1, "TypeID", "$Views_Roles_Type_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: GeneratorID
        ///     Caption: $Views_Roles_Generator_Param.
        /// </summary>
        public readonly ViewObject ParamGeneratorID = new ViewObject(2, "GeneratorID", "$Views_Roles_Generator_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: IsStaticRole
        ///     Caption: Is static role.
        /// </summary>
        public readonly ViewObject ParamIsStaticRole = new ViewObject(3, "IsStaticRole", "Is static role");

        /// <summary>
        ///     ID:4 
        ///     Alias: ShowHidden
        ///     Caption: $Views_Roles_ShowHidden_Param.
        /// </summary>
        public readonly ViewObject ParamShowHidden = new ViewObject(4, "ShowHidden", "$Views_Roles_ShowHidden_Param");

        #endregion

        #region ToString

        public static implicit operator string(RolesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region RoleTypes

    /// <summary>
    ///     ID: {df92983b-2dd3-4092-8603-48d0245cd049}
    ///     Alias: RoleTypes
    ///     Caption: $Views_Names_RoleTypes
    ///     Group: System
    /// </summary>
    public class RoleTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "RoleTypes": {df92983b-2dd3-4092-8603-48d0245cd049}.
        /// </summary>
        public readonly Guid ID = new Guid(0xdf92983b,0x2dd3,0x4092,0x86,0x03,0x48,0xd0,0x24,0x5c,0xd0,0x49);

        /// <summary>
        ///     View name for "RoleTypes".
        /// </summary>
        public readonly string Alias = "RoleTypes";

        /// <summary>
        ///     View caption for "RoleTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_RoleTypes";

        /// <summary>
        ///     View group for "RoleTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RoleTypeID
        ///     Caption: Id типа роли.
        /// </summary>
        public readonly ViewObject ColumnRoleTypeID = new ViewObject(0, "RoleTypeID", "Id типа роли");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleTypeName
        ///     Caption: $Views_RoleTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnRoleTypeName = new ViewObject(1, "RoleTypeName", "$Views_RoleTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: RoleTypeNameParam
        ///     Caption: $Views_RoleTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamRoleTypeNameParam = new ViewObject(0, "RoleTypeNameParam", "$Views_RoleTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(RoleTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Sequences

    /// <summary>
    ///     ID: {b712a7be-2796-4986-b226-8bbcb26d1648}
    ///     Alias: Sequences
    ///     Caption: $Views_Names_Sequences
    ///     Group: System
    /// </summary>
    public class SequencesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Sequences": {b712a7be-2796-4986-b226-8bbcb26d1648}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb712a7be,0x2796,0x4986,0xb2,0x26,0x8b,0xbc,0xb2,0x6d,0x16,0x48);

        /// <summary>
        ///     View name for "Sequences".
        /// </summary>
        public readonly string Alias = "Sequences";

        /// <summary>
        ///     View caption for "Sequences".
        /// </summary>
        public readonly string Caption = "$Views_Names_Sequences";

        /// <summary>
        ///     View group for "Sequences".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SequenceID.
        /// </summary>
        public readonly ViewObject ColumnSequenceID = new ViewObject(0, "SequenceID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SequenceName
        ///     Caption: $Views_Sequences_Name.
        /// </summary>
        public readonly ViewObject ColumnSequenceName = new ViewObject(1, "SequenceName", "$Views_Sequences_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: Created
        ///     Caption: $Views_Sequences_Created.
        /// </summary>
        public readonly ViewObject ColumnCreated = new ViewObject(2, "Created", "$Views_Sequences_Created");

        /// <summary>
        ///     ID:3 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(3, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Sequences_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Sequences_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Created
        ///     Caption: $Views_Sequences_Created_Param.
        /// </summary>
        public readonly ViewObject ParamCreated = new ViewObject(1, "Created", "$Views_Sequences_Created_Param");

        #endregion

        #region ToString

        public static implicit operator string(SequencesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Sessions

    /// <summary>
    ///     ID: {567e7c41-536e-47af-8c2d-b6d8cb6b64fb}
    ///     Alias: Sessions
    ///     Caption: $Views_Names_Sessions
    ///     Group: System
    /// </summary>
    public class SessionsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Sessions": {567e7c41-536e-47af-8c2d-b6d8cb6b64fb}.
        /// </summary>
        public readonly Guid ID = new Guid(0x567e7c41,0x536e,0x47af,0x8c,0x2d,0xb6,0xd8,0xcb,0x6b,0x64,0xfb);

        /// <summary>
        ///     View name for "Sessions".
        /// </summary>
        public readonly string Alias = "Sessions";

        /// <summary>
        ///     View caption for "Sessions".
        /// </summary>
        public readonly string Caption = "$Views_Names_Sessions";

        /// <summary>
        ///     View group for "Sessions".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SessionID.
        /// </summary>
        public readonly ViewObject ColumnSessionID = new ViewObject(0, "SessionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SessionName.
        /// </summary>
        public readonly ViewObject ColumnSessionName = new ViewObject(1, "SessionName");

        /// <summary>
        ///     ID:2 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(2, "UserID");

        /// <summary>
        ///     ID:3 
        ///     Alias: UserName
        ///     Caption: $Views_Sessions_User.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(3, "UserName", "$Views_Sessions_User");

        /// <summary>
        ///     ID:4 
        ///     Alias: UserLogin
        ///     Caption: $Views_Sessions_Login.
        /// </summary>
        public readonly ViewObject ColumnUserLogin = new ViewObject(4, "UserLogin", "$Views_Sessions_Login");

        /// <summary>
        ///     ID:5 
        ///     Alias: HostName
        ///     Caption: $Views_Sessions_Host.
        /// </summary>
        public readonly ViewObject ColumnHostName = new ViewObject(5, "HostName", "$Views_Sessions_Host");

        /// <summary>
        ///     ID:6 
        ///     Alias: HostIP
        ///     Caption: $Views_Sessions_HostIP.
        /// </summary>
        public readonly ViewObject ColumnHostIP = new ViewObject(6, "HostIP", "$Views_Sessions_HostIP");

        /// <summary>
        ///     ID:7 
        ///     Alias: Created
        ///     Caption: $Views_Sessions_Created.
        /// </summary>
        public readonly ViewObject ColumnCreated = new ViewObject(7, "Created", "$Views_Sessions_Created");

        /// <summary>
        ///     ID:8 
        ///     Alias: LastActivity
        ///     Caption: $Views_Sessions_LastActivity.
        /// </summary>
        public readonly ViewObject ColumnLastActivity = new ViewObject(8, "LastActivity", "$Views_Sessions_LastActivity");

        /// <summary>
        ///     ID:9 
        ///     Alias: IsActive
        ///     Caption: $Views_Sessions_IsActive.
        /// </summary>
        public readonly ViewObject ColumnIsActive = new ViewObject(9, "IsActive", "$Views_Sessions_IsActive");

        /// <summary>
        ///     ID:10 
        ///     Alias: ApplicationID.
        /// </summary>
        public readonly ViewObject ColumnApplicationID = new ViewObject(10, "ApplicationID");

        /// <summary>
        ///     ID:11 
        ///     Alias: ApplicationName
        ///     Caption: $Views_Sessions_Application.
        /// </summary>
        public readonly ViewObject ColumnApplicationName = new ViewObject(11, "ApplicationName", "$Views_Sessions_Application");

        /// <summary>
        ///     ID:12 
        ///     Alias: AccessLevelID.
        /// </summary>
        public readonly ViewObject ColumnAccessLevelID = new ViewObject(12, "AccessLevelID");

        /// <summary>
        ///     ID:13 
        ///     Alias: AccessLevelName
        ///     Caption: $Views_Sessions_AccessLevel.
        /// </summary>
        public readonly ViewObject ColumnAccessLevelName = new ViewObject(13, "AccessLevelName", "$Views_Sessions_AccessLevel");

        /// <summary>
        ///     ID:14 
        ///     Alias: LoginTypeID.
        /// </summary>
        public readonly ViewObject ColumnLoginTypeID = new ViewObject(14, "LoginTypeID");

        /// <summary>
        ///     ID:15 
        ///     Alias: LoginTypeName
        ///     Caption: $Views_Sessions_LoginType.
        /// </summary>
        public readonly ViewObject ColumnLoginTypeName = new ViewObject(15, "LoginTypeName", "$Views_Sessions_LoginType");

        /// <summary>
        ///     ID:16 
        ///     Alias: LicenseTypeID.
        /// </summary>
        public readonly ViewObject ColumnLicenseTypeID = new ViewObject(16, "LicenseTypeID");

        /// <summary>
        ///     ID:17 
        ///     Alias: LicenseTypeName
        ///     Caption: $Views_Sessions_LicenseType.
        /// </summary>
        public readonly ViewObject ColumnLicenseTypeName = new ViewObject(17, "LicenseTypeName", "$Views_Sessions_LicenseType");

        /// <summary>
        ///     ID:18 
        ///     Alias: ServiceTypeID.
        /// </summary>
        public readonly ViewObject ColumnServiceTypeID = new ViewObject(18, "ServiceTypeID");

        /// <summary>
        ///     ID:19 
        ///     Alias: ServiceTypeName
        ///     Caption: $Views_Sessions_ServiceType.
        /// </summary>
        public readonly ViewObject ColumnServiceTypeName = new ViewObject(19, "ServiceTypeName", "$Views_Sessions_ServiceType");

        /// <summary>
        ///     ID:20 
        ///     Alias: DeviceTypeID.
        /// </summary>
        public readonly ViewObject ColumnDeviceTypeID = new ViewObject(20, "DeviceTypeID");

        /// <summary>
        ///     ID:21 
        ///     Alias: DeviceTypeName
        ///     Caption: $Views_Sessions_DeviceType.
        /// </summary>
        public readonly ViewObject ColumnDeviceTypeName = new ViewObject(21, "DeviceTypeName", "$Views_Sessions_DeviceType");

        /// <summary>
        ///     ID:22 
        ///     Alias: OSName
        ///     Caption: $Views_Sessions_OSName.
        /// </summary>
        public readonly ViewObject ColumnOSName = new ViewObject(22, "OSName", "$Views_Sessions_OSName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: UserID
        ///     Caption: $Views_Sessions_User_Param.
        /// </summary>
        public readonly ViewObject ParamUserID = new ViewObject(0, "UserID", "$Views_Sessions_User_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Login
        ///     Caption: $Views_Sessions_Login_Param.
        /// </summary>
        public readonly ViewObject ParamLogin = new ViewObject(1, "Login", "$Views_Sessions_Login_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: HostName
        ///     Caption: $Views_Sessions_Host_Param.
        /// </summary>
        public readonly ViewObject ParamHostName = new ViewObject(2, "HostName", "$Views_Sessions_Host_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: HostIP
        ///     Caption: $Views_Sessions_HostIP_Param.
        /// </summary>
        public readonly ViewObject ParamHostIP = new ViewObject(3, "HostIP", "$Views_Sessions_HostIP_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: Created
        ///     Caption: $Views_Sessions_Created_Param.
        /// </summary>
        public readonly ViewObject ParamCreated = new ViewObject(4, "Created", "$Views_Sessions_Created_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: LastActivity
        ///     Caption: $Views_Sessions_LastActivity_Param.
        /// </summary>
        public readonly ViewObject ParamLastActivity = new ViewObject(5, "LastActivity", "$Views_Sessions_LastActivity_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: IsActive
        ///     Caption: $Views_Sessions_IsActive.
        /// </summary>
        public readonly ViewObject ParamIsActive = new ViewObject(6, "IsActive", "$Views_Sessions_IsActive");

        /// <summary>
        ///     ID:7 
        ///     Alias: Application
        ///     Caption: $Views_Sessions_Application_Param.
        /// </summary>
        public readonly ViewObject ParamApplication = new ViewObject(7, "Application", "$Views_Sessions_Application_Param");

        /// <summary>
        ///     ID:8 
        ///     Alias: AccessLevel
        ///     Caption: $Views_Sessions_AccessLevel_Param.
        /// </summary>
        public readonly ViewObject ParamAccessLevel = new ViewObject(8, "AccessLevel", "$Views_Sessions_AccessLevel_Param");

        /// <summary>
        ///     ID:9 
        ///     Alias: LoginType
        ///     Caption: $Views_Sessions_LoginType_Param.
        /// </summary>
        public readonly ViewObject ParamLoginType = new ViewObject(9, "LoginType", "$Views_Sessions_LoginType_Param");

        /// <summary>
        ///     ID:10 
        ///     Alias: LicenseType
        ///     Caption: $Views_Sessions_LicenseType_Param.
        /// </summary>
        public readonly ViewObject ParamLicenseType = new ViewObject(10, "LicenseType", "$Views_Sessions_LicenseType_Param");

        /// <summary>
        ///     ID:11 
        ///     Alias: ServiceType
        ///     Caption: $Views_Sessions_ServiceType_Param.
        /// </summary>
        public readonly ViewObject ParamServiceType = new ViewObject(11, "ServiceType", "$Views_Sessions_ServiceType_Param");

        /// <summary>
        ///     ID:12 
        ///     Alias: DeviceType
        ///     Caption: $Views_Sessions_DeviceType_Param.
        /// </summary>
        public readonly ViewObject ParamDeviceType = new ViewObject(12, "DeviceType", "$Views_Sessions_DeviceType_Param");

        /// <summary>
        ///     ID:13 
        ///     Alias: OSName
        ///     Caption: $Views_Sessions_OSName_Param.
        /// </summary>
        public readonly ViewObject ParamOSName = new ViewObject(13, "OSName", "$Views_Sessions_OSName_Param");

        /// <summary>
        ///     ID:14 
        ///     Alias: Department
        ///     Caption: $Views_Sessions_Department_Param.
        /// </summary>
        public readonly ViewObject ParamDepartment = new ViewObject(14, "Department", "$Views_Sessions_Department_Param");

        #endregion

        #region ToString

        public static implicit operator string(SessionsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region SessionServiceTypes

    /// <summary>
    ///     ID: {24f1eafb-5b69-4329-bf8b-10de805770df}
    ///     Alias: SessionServiceTypes
    ///     Caption: $Views_Names_SessionServiceTypes
    ///     Group: System
    /// </summary>
    public class SessionServiceTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "SessionServiceTypes": {24f1eafb-5b69-4329-bf8b-10de805770df}.
        /// </summary>
        public readonly Guid ID = new Guid(0x24f1eafb,0x5b69,0x4329,0xbf,0x8b,0x10,0xde,0x80,0x57,0x70,0xdf);

        /// <summary>
        ///     View name for "SessionServiceTypes".
        /// </summary>
        public readonly string Alias = "SessionServiceTypes";

        /// <summary>
        ///     View caption for "SessionServiceTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_SessionServiceTypes";

        /// <summary>
        ///     View group for "SessionServiceTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: $Views_SessionServiceTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "$Views_SessionServiceTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_SessionServiceTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_SessionServiceTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(SessionServiceTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region SignatureDigestAlgos

    /// <summary>
    ///     ID: {c6f08c11-6dae-4e7f-a1d7-260134e9b3ff}
    ///     Alias: SignatureDigestAlgos
    ///     Caption: $Views_Names_SignatureDigestAlgos
    ///     Group: System
    /// </summary>
    public class SignatureDigestAlgosViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "SignatureDigestAlgos": {c6f08c11-6dae-4e7f-a1d7-260134e9b3ff}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc6f08c11,0x6dae,0x4e7f,0xa1,0xd7,0x26,0x01,0x34,0xe9,0xb3,0xff);

        /// <summary>
        ///     View name for "SignatureDigestAlgos".
        /// </summary>
        public readonly string Alias = "SignatureDigestAlgos";

        /// <summary>
        ///     View caption for "SignatureDigestAlgos".
        /// </summary>
        public readonly string Caption = "$Views_Names_SignatureDigestAlgos";

        /// <summary>
        ///     View group for "SignatureDigestAlgos".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SignatureDigestAlgorithmID.
        /// </summary>
        public readonly ViewObject ColumnSignatureDigestAlgorithmID = new ViewObject(0, "SignatureDigestAlgorithmID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SignatureDigestAlgorithmName
        ///     Caption: $Views_SignatureDigestAlgorithms_Name.
        /// </summary>
        public readonly ViewObject ColumnSignatureDigestAlgorithmName = new ViewObject(1, "SignatureDigestAlgorithmName", "$Views_SignatureDigestAlgorithms_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: SignatureDigestAlgorithmOID
        ///     Caption: $Views_SignatureDigestAlgorithms_OID.
        /// </summary>
        public readonly ViewObject ColumnSignatureDigestAlgorithmOID = new ViewObject(2, "SignatureDigestAlgorithmOID", "$Views_SignatureDigestAlgorithms_OID");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_SignatureDigestAlgorithms_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_SignatureDigestAlgorithms_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: EncAlgoOid
        ///     Caption: EncAlgoOid.
        /// </summary>
        public readonly ViewObject ParamEncAlgoOid = new ViewObject(1, "EncAlgoOid", "EncAlgoOid");

        #endregion

        #region ToString

        public static implicit operator string(SignatureDigestAlgosViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region SignatureEncryptionAlgos

    /// <summary>
    ///     ID: {4cb7fef2-7b0d-4300-a9ac-7b2b082bcb75}
    ///     Alias: SignatureEncryptionAlgos
    ///     Caption: $Views_Names_SignatureEncryptionAlgos
    ///     Group: System
    /// </summary>
    public class SignatureEncryptionAlgosViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "SignatureEncryptionAlgos": {4cb7fef2-7b0d-4300-a9ac-7b2b082bcb75}.
        /// </summary>
        public readonly Guid ID = new Guid(0x4cb7fef2,0x7b0d,0x4300,0xa9,0xac,0x7b,0x2b,0x08,0x2b,0xcb,0x75);

        /// <summary>
        ///     View name for "SignatureEncryptionAlgos".
        /// </summary>
        public readonly string Alias = "SignatureEncryptionAlgos";

        /// <summary>
        ///     View caption for "SignatureEncryptionAlgos".
        /// </summary>
        public readonly string Caption = "$Views_Names_SignatureEncryptionAlgos";

        /// <summary>
        ///     View group for "SignatureEncryptionAlgos".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SignatureEncryptionAlgorithmID.
        /// </summary>
        public readonly ViewObject ColumnSignatureEncryptionAlgorithmID = new ViewObject(0, "SignatureEncryptionAlgorithmID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SignatureEncryptionAlgorithmName
        ///     Caption: $Views_SignatureEncryptionAlgorithms_Name.
        /// </summary>
        public readonly ViewObject ColumnSignatureEncryptionAlgorithmName = new ViewObject(1, "SignatureEncryptionAlgorithmName", "$Views_SignatureEncryptionAlgorithms_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: SignatureEncryptionAlgorithmOID
        ///     Caption: $Views_SignatureEncryptionAlgorithms_OID.
        /// </summary>
        public readonly ViewObject ColumnSignatureEncryptionAlgorithmOID = new ViewObject(2, "SignatureEncryptionAlgorithmOID", "$Views_SignatureEncryptionAlgorithms_OID");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_SignatureEncryptionAlgorithms_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_SignatureEncryptionAlgorithms_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(SignatureEncryptionAlgosViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region SignaturePackagings

    /// <summary>
    ///     ID: {b49ba9e8-4b5c-40af-8fa2-08daf310aca6}
    ///     Alias: SignaturePackagings
    ///     Caption: $Views_Names_SignaturePackagings
    ///     Group: System
    /// </summary>
    public class SignaturePackagingsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "SignaturePackagings": {b49ba9e8-4b5c-40af-8fa2-08daf310aca6}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb49ba9e8,0x4b5c,0x40af,0x8f,0xa2,0x08,0xda,0xf3,0x10,0xac,0xa6);

        /// <summary>
        ///     View name for "SignaturePackagings".
        /// </summary>
        public readonly string Alias = "SignaturePackagings";

        /// <summary>
        ///     View caption for "SignaturePackagings".
        /// </summary>
        public readonly string Caption = "$Views_Names_SignaturePackagings";

        /// <summary>
        ///     View group for "SignaturePackagings".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SignaturePackagingID.
        /// </summary>
        public readonly ViewObject ColumnSignaturePackagingID = new ViewObject(0, "SignaturePackagingID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SignaturePackagingName
        ///     Caption: $Views_SignaturePackagings_Name.
        /// </summary>
        public readonly ViewObject ColumnSignaturePackagingName = new ViewObject(1, "SignaturePackagingName", "$Views_SignaturePackagings_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_SignaturePackagings_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_SignaturePackagings_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(SignaturePackagingsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region SignatureProfiles

    /// <summary>
    ///     ID: {8cffc16e-6086-488b-99ce-bba53520e37c}
    ///     Alias: SignatureProfiles
    ///     Caption: $Views_Names_SignatureProfiles
    ///     Group: System
    /// </summary>
    public class SignatureProfilesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "SignatureProfiles": {8cffc16e-6086-488b-99ce-bba53520e37c}.
        /// </summary>
        public readonly Guid ID = new Guid(0x8cffc16e,0x6086,0x488b,0x99,0xce,0xbb,0xa5,0x35,0x20,0xe3,0x7c);

        /// <summary>
        ///     View name for "SignatureProfiles".
        /// </summary>
        public readonly string Alias = "SignatureProfiles";

        /// <summary>
        ///     View caption for "SignatureProfiles".
        /// </summary>
        public readonly string Caption = "$Views_Names_SignatureProfiles";

        /// <summary>
        ///     View group for "SignatureProfiles".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SignatureProfileID.
        /// </summary>
        public readonly ViewObject ColumnSignatureProfileID = new ViewObject(0, "SignatureProfileID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SignatureProfileName
        ///     Caption: $Views_SignatureProfiles_Name.
        /// </summary>
        public readonly ViewObject ColumnSignatureProfileName = new ViewObject(1, "SignatureProfileName", "$Views_SignatureProfiles_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_SignatureProfiles_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_SignatureProfiles_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(SignatureProfilesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region SignatureTypes

    /// <summary>
    ///     ID: {30a4931f-c0cf-41fd-b962-053488596ca1}
    ///     Alias: SignatureTypes
    ///     Caption: $Views_Names_SignatureTypes
    ///     Group: System
    /// </summary>
    public class SignatureTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "SignatureTypes": {30a4931f-c0cf-41fd-b962-053488596ca1}.
        /// </summary>
        public readonly Guid ID = new Guid(0x30a4931f,0xc0cf,0x41fd,0xb9,0x62,0x05,0x34,0x88,0x59,0x6c,0xa1);

        /// <summary>
        ///     View name for "SignatureTypes".
        /// </summary>
        public readonly string Alias = "SignatureTypes";

        /// <summary>
        ///     View caption for "SignatureTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_SignatureTypes";

        /// <summary>
        ///     View group for "SignatureTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SignatureTypeID.
        /// </summary>
        public readonly ViewObject ColumnSignatureTypeID = new ViewObject(0, "SignatureTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SignatureTypeName
        ///     Caption: $Views_SignatureTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnSignatureTypeName = new ViewObject(1, "SignatureTypeName", "$Views_SignatureTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_SignatureTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_SignatureTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(SignatureTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region SmartRoleGenerators

    /// <summary>
    ///     ID: {26ce38e4-cf2d-48f0-a790-c4bd631e3eea}
    ///     Alias: SmartRoleGenerators
    ///     Caption: $Views_Names_SmartRoleGenerators
    ///     Group: Acl
    /// </summary>
    public class SmartRoleGeneratorsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "SmartRoleGenerators": {26ce38e4-cf2d-48f0-a790-c4bd631e3eea}.
        /// </summary>
        public readonly Guid ID = new Guid(0x26ce38e4,0xcf2d,0x48f0,0xa7,0x90,0xc4,0xbd,0x63,0x1e,0x3e,0xea);

        /// <summary>
        ///     View name for "SmartRoleGenerators".
        /// </summary>
        public readonly string Alias = "SmartRoleGenerators";

        /// <summary>
        ///     View caption for "SmartRoleGenerators".
        /// </summary>
        public readonly string Caption = "$Views_Names_SmartRoleGenerators";

        /// <summary>
        ///     View group for "SmartRoleGenerators".
        /// </summary>
        public readonly string Group = "Acl";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SmartRoleGeneratorID.
        /// </summary>
        public readonly ViewObject ColumnSmartRoleGeneratorID = new ViewObject(0, "SmartRoleGeneratorID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SmartRoleGeneratorName
        ///     Caption: $Views_SmartRoleGenerators_Name.
        /// </summary>
        public readonly ViewObject ColumnSmartRoleGeneratorName = new ViewObject(1, "SmartRoleGeneratorName", "$Views_SmartRoleGenerators_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_SmartRoleGenerators_Name.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_SmartRoleGenerators_Name");

        #endregion

        #region ToString

        public static implicit operator string(SmartRoleGeneratorsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TagCards

    /// <summary>
    ///     ID: {c2b5ea9e-2bfa-43f6-a566-7e1df45f0f51}
    ///     Alias: TagCards
    ///     Caption: $Views_Names_TagCards
    ///     Group: Tags
    /// </summary>
    public class TagCardsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "TagCards": {c2b5ea9e-2bfa-43f6-a566-7e1df45f0f51}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc2b5ea9e,0x2bfa,0x43f6,0xa5,0x66,0x7e,0x1d,0xf4,0x5f,0x0f,0x51);

        /// <summary>
        ///     View name for "TagCards".
        /// </summary>
        public readonly string Alias = "TagCards";

        /// <summary>
        ///     View caption for "TagCards".
        /// </summary>
        public readonly string Caption = "$Views_Names_TagCards";

        /// <summary>
        ///     View group for "TagCards".
        /// </summary>
        public readonly string Group = "Tags";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(0, "CardID");

        /// <summary>
        ///     ID:1 
        ///     Alias: CardType
        ///     Caption: $Views_TagCards_CardType.
        /// </summary>
        public readonly ViewObject ColumnCardType = new ViewObject(1, "CardType", "$Views_TagCards_CardType");

        /// <summary>
        ///     ID:2 
        ///     Alias: CardNumber
        ///     Caption: $Views_TagCards_CardNumber.
        /// </summary>
        public readonly ViewObject ColumnCardNumber = new ViewObject(2, "CardNumber", "$Views_TagCards_CardNumber");

        /// <summary>
        ///     ID:3 
        ///     Alias: CardSubject
        ///     Caption: $Views_TagCards_CardSubject.
        /// </summary>
        public readonly ViewObject ColumnCardSubject = new ViewObject(3, "CardSubject", "$Views_TagCards_CardSubject");

        /// <summary>
        ///     ID:4 
        ///     Alias: CardDate
        ///     Caption: $Views_TagCards_CardDate.
        /// </summary>
        public readonly ViewObject ColumnCardDate = new ViewObject(4, "CardDate", "$Views_TagCards_CardDate");

        /// <summary>
        ///     ID:5 
        ///     Alias: CardState
        ///     Caption: $Views_TagCards_CardState.
        /// </summary>
        public readonly ViewObject ColumnCardState = new ViewObject(5, "CardState", "$Views_TagCards_CardState");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Tag
        ///     Caption: $Views_TagCards_Tag_Param.
        /// </summary>
        public readonly ViewObject ParamTag = new ViewObject(0, "Tag", "$Views_TagCards_Tag_Param");

        #endregion

        #region ToString

        public static implicit operator string(TagCardsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Tags

    /// <summary>
    ///     ID: {99751083-96ea-435b-8331-4e0de99d69d6}
    ///     Alias: Tags
    ///     Caption: $Views_Names_Tags
    ///     Group: Tags
    /// </summary>
    public class TagsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Tags": {99751083-96ea-435b-8331-4e0de99d69d6}.
        /// </summary>
        public readonly Guid ID = new Guid(0x99751083,0x96ea,0x435b,0x83,0x31,0x4e,0x0d,0xe9,0x9d,0x69,0xd6);

        /// <summary>
        ///     View name for "Tags".
        /// </summary>
        public readonly string Alias = "Tags";

        /// <summary>
        ///     View caption for "Tags".
        /// </summary>
        public readonly string Caption = "$Views_Names_Tags";

        /// <summary>
        ///     View group for "Tags".
        /// </summary>
        public readonly string Group = "Tags";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TagID.
        /// </summary>
        public readonly ViewObject ColumnTagID = new ViewObject(0, "TagID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TagName
        ///     Caption: $Views_Tags_Name.
        /// </summary>
        public readonly ViewObject ColumnTagName = new ViewObject(1, "TagName", "$Views_Tags_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: TagOwnerID.
        /// </summary>
        public readonly ViewObject ColumnTagOwnerID = new ViewObject(2, "TagOwnerID");

        /// <summary>
        ///     ID:3 
        ///     Alias: TagOwnerName
        ///     Caption: $Views_Tags_Owner.
        /// </summary>
        public readonly ViewObject ColumnTagOwnerName = new ViewObject(3, "TagOwnerName", "$Views_Tags_Owner");

        /// <summary>
        ///     ID:4 
        ///     Alias: TagBackground
        ///     Caption: $CardTypes_Controls_Background.
        /// </summary>
        public readonly ViewObject ColumnTagBackground = new ViewObject(4, "TagBackground", "$CardTypes_Controls_Background");

        /// <summary>
        ///     ID:5 
        ///     Alias: TagForeground
        ///     Caption: $CardTypes_Controls_Foreground.
        /// </summary>
        public readonly ViewObject ColumnTagForeground = new ViewObject(5, "TagForeground", "$CardTypes_Controls_Foreground");

        /// <summary>
        ///     ID:6 
        ///     Alias: TagIsCommon
        ///     Caption: $Tags_IsCommon.
        /// </summary>
        public readonly ViewObject ColumnTagIsCommon = new ViewObject(6, "TagIsCommon", "$Tags_IsCommon");

        /// <summary>
        ///     ID:7 
        ///     Alias: TagCanEdit.
        /// </summary>
        public readonly ViewObject ColumnTagCanEdit = new ViewObject(7, "TagCanEdit");

        /// <summary>
        ///     ID:8 
        ///     Alias: TagCanUse.
        /// </summary>
        public readonly ViewObject ColumnTagCanUse = new ViewObject(8, "TagCanUse");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Tags_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Tags_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Owner
        ///     Caption: $Views_Tags_Owner_Param.
        /// </summary>
        public readonly ViewObject ParamOwner = new ViewObject(1, "Owner", "$Views_Tags_Owner_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: MyTagsOnly
        ///     Caption: $Views_Tags_MyTagsOnly_Param.
        /// </summary>
        public readonly ViewObject ParamMyTagsOnly = new ViewObject(2, "MyTagsOnly", "$Views_Tags_MyTagsOnly_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: IgnoreCardID
        ///     Caption: $Tags_ExcludeCardTags.
        /// </summary>
        public readonly ViewObject ParamIgnoreCardID = new ViewObject(3, "IgnoreCardID", "$Tags_ExcludeCardTags");

        /// <summary>
        ///     ID:4 
        ///     Alias: AddToCardDialogMode
        ///     Caption: $Tags_AddToCardMode.
        /// </summary>
        public readonly ViewObject ParamAddToCardDialogMode = new ViewObject(4, "AddToCardDialogMode", "$Tags_AddToCardMode");

        #endregion

        #region ToString

        public static implicit operator string(TagsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskAssignedRoles

    /// <summary>
    ///     ID: {b956e262-9b54-4aaf-a7b8-1649ea9462e7}
    ///     Alias: TaskAssignedRoles
    ///     Caption: $Views_Names_TaskAssignedRoles
    ///     Group: TaskAssignedRoles
    /// </summary>
    public class TaskAssignedRolesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "TaskAssignedRoles": {b956e262-9b54-4aaf-a7b8-1649ea9462e7}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb956e262,0x9b54,0x4aaf,0xa7,0xb8,0x16,0x49,0xea,0x94,0x62,0xe7);

        /// <summary>
        ///     View name for "TaskAssignedRoles".
        /// </summary>
        public readonly string Alias = "TaskAssignedRoles";

        /// <summary>
        ///     View caption for "TaskAssignedRoles".
        /// </summary>
        public readonly string Caption = "$Views_Names_TaskAssignedRoles";

        /// <summary>
        ///     View group for "TaskAssignedRoles".
        /// </summary>
        public readonly string Group = "TaskAssignedRoles";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TaskRowID.
        /// </summary>
        public readonly ViewObject ColumnTaskRowID = new ViewObject(0, "TaskRowID");

        /// <summary>
        ///     ID:1 
        ///     Alias: AssignedRoleRowID.
        /// </summary>
        public readonly ViewObject ColumnAssignedRoleRowID = new ViewObject(1, "AssignedRoleRowID");

        /// <summary>
        ///     ID:2 
        ///     Alias: AssignedRoleTaskRoleID.
        /// </summary>
        public readonly ViewObject ColumnAssignedRoleTaskRoleID = new ViewObject(2, "AssignedRoleTaskRoleID");

        /// <summary>
        ///     ID:3 
        ///     Alias: TaskRoleCaption
        ///     Caption: $Views_TaskAssignedRoles_TaskRoleName.
        /// </summary>
        public readonly ViewObject ColumnTaskRoleCaption = new ViewObject(3, "TaskRoleCaption", "$Views_TaskAssignedRoles_TaskRoleName");

        /// <summary>
        ///     ID:4 
        ///     Alias: AssignedRoleRoleID.
        /// </summary>
        public readonly ViewObject ColumnAssignedRoleRoleID = new ViewObject(4, "AssignedRoleRoleID");

        /// <summary>
        ///     ID:5 
        ///     Alias: RoleName
        ///     Caption: $Views_TaskAssignedRoles_RoleName.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(5, "RoleName", "$Views_TaskAssignedRoles_RoleName");

        /// <summary>
        ///     ID:6 
        ///     Alias: RoleTypeID.
        /// </summary>
        public readonly ViewObject ColumnRoleTypeID = new ViewObject(6, "RoleTypeID");

        /// <summary>
        ///     ID:7 
        ///     Alias: RoleTypeCaption
        ///     Caption: $Views_TaskAssignedRoles_RoleTypeCaption.
        /// </summary>
        public readonly ViewObject ColumnRoleTypeCaption = new ViewObject(7, "RoleTypeCaption", "$Views_TaskAssignedRoles_RoleTypeCaption");

        /// <summary>
        ///     ID:8 
        ///     Alias: Position
        ///     Caption: $Views_TaskAssignedRoles_Position.
        /// </summary>
        public readonly ViewObject ColumnPosition = new ViewObject(8, "Position", "$Views_TaskAssignedRoles_Position");

        /// <summary>
        ///     ID:9 
        ///     Alias: ParentRowID.
        /// </summary>
        public readonly ViewObject ColumnParentRowID = new ViewObject(9, "ParentRowID");

        /// <summary>
        ///     ID:10 
        ///     Alias: Master
        ///     Caption: $Views_TaskAssignedRoles_Master.
        /// </summary>
        public readonly ViewObject ColumnMaster = new ViewObject(10, "Master", "$Views_TaskAssignedRoles_Master");

        /// <summary>
        ///     ID:11 
        ///     Alias: ShowInTaskDetails
        ///     Caption: $Views_TaskAssignedRoles_ShowInTaskDetails.
        /// </summary>
        public readonly ViewObject ColumnShowInTaskDetails = new ViewObject(11, "ShowInTaskDetails", "$Views_TaskAssignedRoles_ShowInTaskDetails");

        /// <summary>
        ///     ID:12 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(12, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: TaskRowID
        ///     Caption: $Views_TaskAssignedRoles_TaskRowID.
        /// </summary>
        public readonly ViewObject ParamTaskRowID = new ViewObject(0, "TaskRowID", "$Views_TaskAssignedRoles_TaskRowID");

        /// <summary>
        ///     ID:1 
        ///     Alias: FunctionRole
        ///     Caption: $Views_TaskAssignedRoles_FunctionRole.
        /// </summary>
        public readonly ViewObject ParamFunctionRole = new ViewObject(1, "FunctionRole", "$Views_TaskAssignedRoles_FunctionRole");

        /// <summary>
        ///     ID:2 
        ///     Alias: TaskRoleCaptionOrAlias
        ///     Caption: $Views_TaskAssignedRoles_TaskRoleCaptionAlias.
        /// </summary>
        public readonly ViewObject ParamTaskRoleCaptionOrAlias = new ViewObject(2, "TaskRoleCaptionOrAlias", "$Views_TaskAssignedRoles_TaskRoleCaptionAlias");

        #endregion

        #region ToString

        public static implicit operator string(TaskAssignedRolesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskAssignedRoleUsers

    /// <summary>
    ///     ID: {966b7c87-1443-4ab9-b23a-6864234ed30e}
    ///     Alias: TaskAssignedRoleUsers
    ///     Caption: TaskAssignedRoleUsers
    ///     Group: TaskAssignedRoles
    /// </summary>
    public class TaskAssignedRoleUsersViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "TaskAssignedRoleUsers": {966b7c87-1443-4ab9-b23a-6864234ed30e}.
        /// </summary>
        public readonly Guid ID = new Guid(0x966b7c87,0x1443,0x4ab9,0xb2,0x3a,0x68,0x64,0x23,0x4e,0xd3,0x0e);

        /// <summary>
        ///     View name for "TaskAssignedRoleUsers".
        /// </summary>
        public readonly string Alias = "TaskAssignedRoleUsers";

        /// <summary>
        ///     View caption for "TaskAssignedRoleUsers".
        /// </summary>
        public readonly string Caption = "TaskAssignedRoleUsers";

        /// <summary>
        ///     View group for "TaskAssignedRoleUsers".
        /// </summary>
        public readonly string Group = "TaskAssignedRoles";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(0, "UserID");

        /// <summary>
        ///     ID:1 
        ///     Alias: UserName
        ///     Caption: $Views_Users_Name.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(1, "UserName", "$Views_Users_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: Email
        ///     Caption: $Views_Users_Email.
        /// </summary>
        public readonly ViewObject ColumnEmail = new ViewObject(2, "Email", "$Views_Users_Email");

        /// <summary>
        ///     ID:3 
        ///     Alias: Position
        ///     Caption: $Views_Users_Position.
        /// </summary>
        public readonly ViewObject ColumnPosition = new ViewObject(3, "Position", "$Views_Users_Position");

        /// <summary>
        ///     ID:4 
        ///     Alias: Departments
        ///     Caption: $Views_Users_Departments.
        /// </summary>
        public readonly ViewObject ColumnDepartments = new ViewObject(4, "Departments", "$Views_Users_Departments");

        /// <summary>
        ///     ID:5 
        ///     Alias: StaticRoles
        ///     Caption: $Views_Users_StaticRoles.
        /// </summary>
        public readonly ViewObject ColumnStaticRoles = new ViewObject(5, "StaticRoles", "$Views_Users_StaticRoles");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Users_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Users_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: TaskAssignedRoleRowID
        ///     Caption: $Views_Users_Role_Param.
        /// </summary>
        public readonly ViewObject ParamTaskAssignedRoleRowID = new ViewObject(1, "TaskAssignedRoleRowID", "$Views_Users_Role_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: RoleExclusionID
        ///     Caption: $Views_Users_RoleExclusion_Param.
        /// </summary>
        public readonly ViewObject ParamRoleExclusionID = new ViewObject(2, "RoleExclusionID", "$Views_Users_RoleExclusion_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: ShowHidden
        ///     Caption: $Views_Users_ShowHidden_Param.
        /// </summary>
        public readonly ViewObject ParamShowHidden = new ViewObject(3, "ShowHidden", "$Views_Users_ShowHidden_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: RoleID
        ///     Caption: $Views_Users_Role_Param.
        /// </summary>
        public readonly ViewObject ParamRoleID = new ViewObject(4, "RoleID", "$Views_Users_Role_Param");

        #endregion

        #region ToString

        public static implicit operator string(TaskAssignedRoleUsersViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskFunctionRoles

    /// <summary>
    ///     ID: {20a41d67-4807-496f-b5f8-1ae3f036eb2f}
    ///     Alias: TaskFunctionRoles
    ///     Caption: $Views_Names_TaskFunctionRoles
    ///     Group: TaskAssignedRoles
    /// </summary>
    public class TaskFunctionRolesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "TaskFunctionRoles": {20a41d67-4807-496f-b5f8-1ae3f036eb2f}.
        /// </summary>
        public readonly Guid ID = new Guid(0x20a41d67,0x4807,0x496f,0xb5,0xf8,0x1a,0xe3,0xf0,0x36,0xeb,0x2f);

        /// <summary>
        ///     View name for "TaskFunctionRoles".
        /// </summary>
        public readonly string Alias = "TaskFunctionRoles";

        /// <summary>
        ///     View caption for "TaskFunctionRoles".
        /// </summary>
        public readonly string Caption = "$Views_Names_TaskFunctionRoles";

        /// <summary>
        ///     View group for "TaskFunctionRoles".
        /// </summary>
        public readonly string Group = "TaskAssignedRoles";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: FunctionRoleID.
        /// </summary>
        public readonly ViewObject ColumnFunctionRoleID = new ViewObject(0, "FunctionRoleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleName
        ///     Caption: $Views_TaskAssignedRoles_TaskRoleName.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(1, "RoleName", "$Views_TaskAssignedRoles_TaskRoleName");

        /// <summary>
        ///     ID:2 
        ///     Alias: RoleCaption
        ///     Caption: $Views_TaskAssignedRoles_TaskRoleCaption.
        /// </summary>
        public readonly ViewObject ColumnRoleCaption = new ViewObject(2, "RoleCaption", "$Views_TaskAssignedRoles_TaskRoleCaption");

        /// <summary>
        ///     ID:3 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(3, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: TaskRowID
        ///     Caption: $Views_TaskAssignedRoles_TaskRowID.
        /// </summary>
        public readonly ViewObject ParamTaskRowID = new ViewObject(0, "TaskRowID", "$Views_TaskAssignedRoles_TaskRowID");

        /// <summary>
        ///     ID:1 
        ///     Alias: AssignedRoleTaskRoleCaptionOrAlias
        ///     Caption: $Views_TaskAssignedRoles_TaskRoleCaptionAlias.
        /// </summary>
        public readonly ViewObject ParamAssignedRoleTaskRoleCaptionOrAlias = new ViewObject(1, "AssignedRoleTaskRoleCaptionOrAlias", "$Views_TaskAssignedRoles_TaskRoleCaptionAlias");

        #endregion

        #region ToString

        public static implicit operator string(TaskFunctionRolesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskHistory

    /// <summary>
    ///     ID: {00da4f8f-bbf5-4b18-8b43-c528e5359d28}
    ///     Alias: TaskHistory
    ///     Caption: $Views_Names_TaskHistory
    ///     Group: System
    /// </summary>
    public class TaskHistoryViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "TaskHistory": {00da4f8f-bbf5-4b18-8b43-c528e5359d28}.
        /// </summary>
        public readonly Guid ID = new Guid(0x00da4f8f,0xbbf5,0x4b18,0x8b,0x43,0xc5,0x28,0xe5,0x35,0x9d,0x28);

        /// <summary>
        ///     View name for "TaskHistory".
        /// </summary>
        public readonly string Alias = "TaskHistory";

        /// <summary>
        ///     View caption for "TaskHistory".
        /// </summary>
        public readonly string Caption = "$Views_Names_TaskHistory";

        /// <summary>
        ///     View group for "TaskHistory".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RowID.
        /// </summary>
        public readonly ViewObject ColumnRowID = new ViewObject(0, "RowID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ParentRowID.
        /// </summary>
        public readonly ViewObject ColumnParentRowID = new ViewObject(1, "ParentRowID");

        /// <summary>
        ///     ID:2 
        ///     Alias: GroupRowID.
        /// </summary>
        public readonly ViewObject ColumnGroupRowID = new ViewObject(2, "GroupRowID");

        /// <summary>
        ///     ID:3 
        ///     Alias: GroupParentRowID.
        /// </summary>
        public readonly ViewObject ColumnGroupParentRowID = new ViewObject(3, "GroupParentRowID");

        /// <summary>
        ///     ID:4 
        ///     Alias: GroupCaption.
        /// </summary>
        public readonly ViewObject ColumnGroupCaption = new ViewObject(4, "GroupCaption");

        /// <summary>
        ///     ID:5 
        ///     Alias: Group.
        /// </summary>
        public readonly ViewObject ColumnGroup = new ViewObject(5, "Group");

        /// <summary>
        ///     ID:6 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(6, "TypeID");

        /// <summary>
        ///     ID:7 
        ///     Alias: TypeCaption
        ///     Caption: $UI_Cards_TaskHistory_Task.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(7, "TypeCaption", "$UI_Cards_TaskHistory_Task");

        /// <summary>
        ///     ID:8 
        ///     Alias: StateCaption
        ///     Caption: $Views_TaskHistory_StateCaption.
        /// </summary>
        public readonly ViewObject ColumnStateCaption = new ViewObject(8, "StateCaption", "$Views_TaskHistory_StateCaption");

        /// <summary>
        ///     ID:9 
        ///     Alias: Date
        ///     Caption: $Views_TaskHistory_Date.
        /// </summary>
        public readonly ViewObject ColumnDate = new ViewObject(9, "Date", "$Views_TaskHistory_Date");

        /// <summary>
        ///     ID:10 
        ///     Alias: OptionCaption
        ///     Caption: $Views_TaskHistory_OptionCaption.
        /// </summary>
        public readonly ViewObject ColumnOptionCaption = new ViewObject(10, "OptionCaption", "$Views_TaskHistory_OptionCaption");

        /// <summary>
        ///     ID:11 
        ///     Alias: CompletedByName
        ///     Caption: $Views_TaskHistory_CompletedByName.
        /// </summary>
        public readonly ViewObject ColumnCompletedByName = new ViewObject(11, "CompletedByName", "$Views_TaskHistory_CompletedByName");

        /// <summary>
        ///     ID:12 
        ///     Alias: Result
        ///     Caption: $Views_TaskHistory_Result.
        /// </summary>
        public readonly ViewObject ColumnResult = new ViewObject(12, "Result", "$Views_TaskHistory_Result");

        /// <summary>
        ///     ID:13 
        ///     Alias: AuthorName
        ///     Caption: $Views_TaskHistory_Author.
        /// </summary>
        public readonly ViewObject ColumnAuthorName = new ViewObject(13, "AuthorName", "$Views_TaskHistory_Author");

        /// <summary>
        ///     ID:14 
        ///     Alias: RoleName
        ///     Caption: $Views_TaskHistory_RoleName.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(14, "RoleName", "$Views_TaskHistory_RoleName");

        /// <summary>
        ///     ID:15 
        ///     Alias: UserName
        ///     Caption: $Views_TaskHistory_UserName.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(15, "UserName", "$Views_TaskHistory_UserName");

        /// <summary>
        ///     ID:16 
        ///     Alias: Created
        ///     Caption: $Views_TaskHistory_Created.
        /// </summary>
        public readonly ViewObject ColumnCreated = new ViewObject(16, "Created", "$Views_TaskHistory_Created");

        /// <summary>
        ///     ID:17 
        ///     Alias: Planned
        ///     Caption: $Views_TaskHistory_Planned.
        /// </summary>
        public readonly ViewObject ColumnPlanned = new ViewObject(17, "Planned", "$Views_TaskHistory_Planned");

        /// <summary>
        ///     ID:18 
        ///     Alias: InProgress
        ///     Caption: $Views_TaskHistory_InProgress.
        /// </summary>
        public readonly ViewObject ColumnInProgress = new ViewObject(18, "InProgress", "$Views_TaskHistory_InProgress");

        /// <summary>
        ///     ID:19 
        ///     Alias: Completed
        ///     Caption: $Views_TaskHistory_Completed.
        /// </summary>
        public readonly ViewObject ColumnCompleted = new ViewObject(19, "Completed", "$Views_TaskHistory_Completed");

        /// <summary>
        ///     ID:20 
        ///     Alias: FilesCount
        ///     Caption: $Views_TaskHistory_FilesCount.
        /// </summary>
        public readonly ViewObject ColumnFilesCount = new ViewObject(20, "FilesCount", "$Views_TaskHistory_FilesCount");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: CardID
        ///     Caption: $Views_Templates_Digest.
        /// </summary>
        public readonly ViewObject ParamCardID = new ViewObject(0, "CardID", "$Views_Templates_Digest");

        /// <summary>
        ///     ID:1 
        ///     Alias: Token
        ///     Caption: Token.
        /// </summary>
        public readonly ViewObject ParamToken = new ViewObject(1, "Token", "Token");

        #endregion

        #region ToString

        public static implicit operator string(TaskHistoryViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskHistoryGroupTypes

    /// <summary>
    ///     ID: {25d1c651-1008-496c-8252-778a4b5d9064}
    ///     Alias: TaskHistoryGroupTypes
    ///     Caption: $Views_Names_TaskHistoryGroupTypes
    ///     Group: System
    /// </summary>
    public class TaskHistoryGroupTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "TaskHistoryGroupTypes": {25d1c651-1008-496c-8252-778a4b5d9064}.
        /// </summary>
        public readonly Guid ID = new Guid(0x25d1c651,0x1008,0x496c,0x82,0x52,0x77,0x8a,0x4b,0x5d,0x90,0x64);

        /// <summary>
        ///     View name for "TaskHistoryGroupTypes".
        /// </summary>
        public readonly string Alias = "TaskHistoryGroupTypes";

        /// <summary>
        ///     View caption for "TaskHistoryGroupTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_TaskHistoryGroupTypes";

        /// <summary>
        ///     View group for "TaskHistoryGroupTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: GroupTypeID.
        /// </summary>
        public readonly ViewObject ColumnGroupTypeID = new ViewObject(0, "GroupTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: GroupTypeCaption
        ///     Caption: $Views_TaskHistoryGroupTypes_GroupTypeCaption.
        /// </summary>
        public readonly ViewObject ColumnGroupTypeCaption = new ViewObject(1, "GroupTypeCaption", "$Views_TaskHistoryGroupTypes_GroupTypeCaption");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: GroupTypeCaptionParam
        ///     Caption: $Views_TaskHistoryGroupTypes_GroupTypeCaption.
        /// </summary>
        public readonly ViewObject ParamGroupTypeCaptionParam = new ViewObject(0, "GroupTypeCaptionParam", "$Views_TaskHistoryGroupTypes_GroupTypeCaption");

        #endregion

        #region ToString

        public static implicit operator string(TaskHistoryGroupTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskKinds

    /// <summary>
    ///     ID: {57dc8c0a-080f-486d-ba97-ffc52869754e}
    ///     Alias: TaskKinds
    ///     Caption: $Views_Names_TaskKinds
    ///     Group: System
    /// </summary>
    public class TaskKindsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "TaskKinds": {57dc8c0a-080f-486d-ba97-ffc52869754e}.
        /// </summary>
        public readonly Guid ID = new Guid(0x57dc8c0a,0x080f,0x486d,0xba,0x97,0xff,0xc5,0x28,0x69,0x75,0x4e);

        /// <summary>
        ///     View name for "TaskKinds".
        /// </summary>
        public readonly string Alias = "TaskKinds";

        /// <summary>
        ///     View caption for "TaskKinds".
        /// </summary>
        public readonly string Caption = "$Views_Names_TaskKinds";

        /// <summary>
        ///     View group for "TaskKinds".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: KindID.
        /// </summary>
        public readonly ViewObject ColumnKindID = new ViewObject(0, "KindID");

        /// <summary>
        ///     ID:1 
        ///     Alias: KindCaption
        ///     Caption: $Views_TaskKinds_Caption.
        /// </summary>
        public readonly ViewObject ColumnKindCaption = new ViewObject(1, "KindCaption", "$Views_TaskKinds_Caption");

        /// <summary>
        ///     ID:2 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(2, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: ID
        ///     Caption: ID.
        /// </summary>
        public readonly ViewObject ParamID = new ViewObject(0, "ID", "ID");

        /// <summary>
        ///     ID:1 
        ///     Alias: Caption
        ///     Caption: $Views_TaskKinds_Caption_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(1, "Caption", "$Views_TaskKinds_Caption_Param");

        #endregion

        #region ToString

        public static implicit operator string(TaskKindsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskStates

    /// <summary>
    ///     ID: {b75a29da-0672-45ff-8f58-39abcb129506}
    ///     Alias: TaskStates
    ///     Caption: $Views_Names_TaskStates
    ///     Group: System
    /// </summary>
    public class TaskStatesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "TaskStates": {b75a29da-0672-45ff-8f58-39abcb129506}.
        /// </summary>
        public readonly Guid ID = new Guid(0xb75a29da,0x0672,0x45ff,0x8f,0x58,0x39,0xab,0xcb,0x12,0x95,0x06);

        /// <summary>
        ///     View name for "TaskStates".
        /// </summary>
        public readonly string Alias = "TaskStates";

        /// <summary>
        ///     View caption for "TaskStates".
        /// </summary>
        public readonly string Caption = "$Views_Names_TaskStates";

        /// <summary>
        ///     View group for "TaskStates".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TaskStateID.
        /// </summary>
        public readonly ViewObject ColumnTaskStateID = new ViewObject(0, "TaskStateID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TaskStateName
        ///     Caption: $Views_TaskStates_Name.
        /// </summary>
        public readonly ViewObject ColumnTaskStateName = new ViewObject(1, "TaskStateName", "$Views_TaskStates_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_TaskStates_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_TaskStates_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(TaskStatesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TaskTypes

    /// <summary>
    ///     ID: {fcd3f5ad-545f-41d1-ad85-345157020e33}
    ///     Alias: TaskTypes
    ///     Caption: $Views_Names_TaskTypes
    ///     Group: System
    /// </summary>
    public class TaskTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "TaskTypes": {fcd3f5ad-545f-41d1-ad85-345157020e33}.
        /// </summary>
        public readonly Guid ID = new Guid(0xfcd3f5ad,0x545f,0x41d1,0xad,0x85,0x34,0x51,0x57,0x02,0x0e,0x33);

        /// <summary>
        ///     View name for "TaskTypes".
        /// </summary>
        public readonly string Alias = "TaskTypes";

        /// <summary>
        ///     View caption for "TaskTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_TaskTypes";

        /// <summary>
        ///     View group for "TaskTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeCaption
        ///     Caption: $Views_TaskTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(1, "TypeCaption", "$Views_TaskTypes_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeName
        ///     Caption: $Views_TaskTypes_Alias.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(2, "TypeName", "$Views_TaskTypes_Alias");

        /// <summary>
        ///     ID:3 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(3, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_TaskTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_TaskTypes_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_TaskTypes_Alias_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_TaskTypes_Alias_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: NameOrCaption
        ///     Caption: NameOrCaption.
        /// </summary>
        public readonly ViewObject ParamNameOrCaption = new ViewObject(2, "NameOrCaption", "NameOrCaption");

        /// <summary>
        ///     ID:3 
        ///     Alias: OnlyEnabledForRoutes
        ///     Caption: OnlyEnabledForRoutes.
        /// </summary>
        public readonly ViewObject ParamOnlyEnabledForRoutes = new ViewObject(3, "OnlyEnabledForRoutes", "OnlyEnabledForRoutes");

        #endregion

        #region ToString

        public static implicit operator string(TaskTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Templates

    /// <summary>
    ///     ID: {edab4e60-c19e-49a2-979f-7634133c377e}
    ///     Alias: Templates
    ///     Caption: $Views_Names_Templates
    ///     Group: System
    /// </summary>
    public class TemplatesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Templates": {edab4e60-c19e-49a2-979f-7634133c377e}.
        /// </summary>
        public readonly Guid ID = new Guid(0xedab4e60,0xc19e,0x49a2,0x97,0x9f,0x76,0x34,0x13,0x3c,0x37,0x7e);

        /// <summary>
        ///     View name for "Templates".
        /// </summary>
        public readonly string Alias = "Templates";

        /// <summary>
        ///     View caption for "Templates".
        /// </summary>
        public readonly string Caption = "$Views_Names_Templates";

        /// <summary>
        ///     View group for "Templates".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TemplateID.
        /// </summary>
        public readonly ViewObject ColumnTemplateID = new ViewObject(0, "TemplateID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TemplateCaption
        ///     Caption: $Views_Templates_Name.
        /// </summary>
        public readonly ViewObject ColumnTemplateCaption = new ViewObject(1, "TemplateCaption", "$Views_Templates_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: TemplateDescription
        ///     Caption: $Views_Templates_Description.
        /// </summary>
        public readonly ViewObject ColumnTemplateDescription = new ViewObject(2, "TemplateDescription", "$Views_Templates_Description");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(3, "TypeID");

        /// <summary>
        ///     ID:4 
        ///     Alias: TypeCaption
        ///     Caption: $Views_Templates_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(4, "TypeCaption", "$Views_Templates_Type");

        /// <summary>
        ///     ID:5 
        ///     Alias: TemplateDigest
        ///     Caption: $Views_Templates_Digest.
        /// </summary>
        public readonly ViewObject ColumnTemplateDigest = new ViewObject(5, "TemplateDigest", "$Views_Templates_Digest");

        /// <summary>
        ///     ID:6 
        ///     Alias: TemplateVersion
        ///     Caption: $Views_Templates_Version.
        /// </summary>
        public readonly ViewObject ColumnTemplateVersion = new ViewObject(6, "TemplateVersion", "$Views_Templates_Version");

        /// <summary>
        ///     ID:7 
        ///     Alias: TemplateDate
        ///     Caption: $Views_Templates_Date.
        /// </summary>
        public readonly ViewObject ColumnTemplateDate = new ViewObject(7, "TemplateDate", "$Views_Templates_Date");

        /// <summary>
        ///     ID:8 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(8, "UserID");

        /// <summary>
        ///     ID:9 
        ///     Alias: UserName
        ///     Caption: $Views_Templates_User.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(9, "UserName", "$Views_Templates_User");

        /// <summary>
        ///     ID:10 
        ///     Alias: CardID.
        /// </summary>
        public readonly ViewObject ColumnCardID = new ViewObject(10, "CardID");

        /// <summary>
        ///     ID:11 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(11, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: TemplateCaption
        ///     Caption: $Views_Templates_Name_Param.
        /// </summary>
        public readonly ViewObject ParamTemplateCaption = new ViewObject(0, "TemplateCaption", "$Views_Templates_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: TemplateDescription
        ///     Caption: $Views_Templates_Description_Param.
        /// </summary>
        public readonly ViewObject ParamTemplateDescription = new ViewObject(1, "TemplateDescription", "$Views_Templates_Description_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeID
        ///     Caption: $Views_Templates_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeID = new ViewObject(2, "TypeID", "$Views_Templates_Type_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: TemplateDigest
        ///     Caption: $Views_Templates_Digest_Param.
        /// </summary>
        public readonly ViewObject ParamTemplateDigest = new ViewObject(3, "TemplateDigest", "$Views_Templates_Digest_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: TemplateVersion
        ///     Caption: $Views_Templates_Version_Param.
        /// </summary>
        public readonly ViewObject ParamTemplateVersion = new ViewObject(4, "TemplateVersion", "$Views_Templates_Version_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: TemplateDate
        ///     Caption: $Views_Templates_Date_Param.
        /// </summary>
        public readonly ViewObject ParamTemplateDate = new ViewObject(5, "TemplateDate", "$Views_Templates_Date_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: UserID
        ///     Caption: $Views_Templates_User_Param.
        /// </summary>
        public readonly ViewObject ParamUserID = new ViewObject(6, "UserID", "$Views_Templates_User_Param");

        /// <summary>
        ///     ID:7 
        ///     Alias: CardID
        ///     Caption: $Views_Templates_SourceCard.
        /// </summary>
        public readonly ViewObject ParamCardID = new ViewObject(7, "CardID", "$Views_Templates_SourceCard");

        #endregion

        #region ToString

        public static implicit operator string(TemplatesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TileSizes

    /// <summary>
    ///     ID: {942b9908-f1c0-442d-b8b1-ba269431742d}
    ///     Alias: TileSizes
    ///     Caption: $Views_Names_TileSizes
    ///     Group: System
    /// </summary>
    public class TileSizesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "TileSizes": {942b9908-f1c0-442d-b8b1-ba269431742d}.
        /// </summary>
        public readonly Guid ID = new Guid(0x942b9908,0xf1c0,0x442d,0xb8,0xb1,0xba,0x26,0x94,0x31,0x74,0x2d);

        /// <summary>
        ///     View name for "TileSizes".
        /// </summary>
        public readonly string Alias = "TileSizes";

        /// <summary>
        ///     View caption for "TileSizes".
        /// </summary>
        public readonly string Caption = "$Views_Names_TileSizes";

        /// <summary>
        ///     View group for "TileSizes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RefID.
        /// </summary>
        public readonly ViewObject ColumnRefID = new ViewObject(0, "RefID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RefName
        ///     Caption: $Views_TileSizes_Name.
        /// </summary>
        public readonly ViewObject ColumnRefName = new ViewObject(1, "RefName", "$Views_TileSizes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_TileSizes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_TileSizes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(TileSizesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TimeZones

    /// <summary>
    ///     ID: {24ed3370-f3c0-4074-a3f2-4614a7baaebb}
    ///     Alias: TimeZones
    ///     Caption: $Views_Names_TimeZones
    ///     Group: System
    /// </summary>
    public class TimeZonesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "TimeZones": {24ed3370-f3c0-4074-a3f2-4614a7baaebb}.
        /// </summary>
        public readonly Guid ID = new Guid(0x24ed3370,0xf3c0,0x4074,0xa3,0xf2,0x46,0x14,0xa7,0xba,0xae,0xbb);

        /// <summary>
        ///     View name for "TimeZones".
        /// </summary>
        public readonly string Alias = "TimeZones";

        /// <summary>
        ///     View caption for "TimeZones".
        /// </summary>
        public readonly string Caption = "$Views_Names_TimeZones";

        /// <summary>
        ///     View group for "TimeZones".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ZoneID
        ///     Caption: $Views_TimeZones_ZoneID.
        /// </summary>
        public readonly ViewObject ColumnZoneID = new ViewObject(0, "ZoneID", "$Views_TimeZones_ZoneID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ZoneShortName
        ///     Caption: $Views_TimeZones_ShortName.
        /// </summary>
        public readonly ViewObject ColumnZoneShortName = new ViewObject(1, "ZoneShortName", "$Views_TimeZones_ShortName");

        /// <summary>
        ///     ID:2 
        ///     Alias: ZoneCodeName
        ///     Caption: $Views_TimeZones_CodeName.
        /// </summary>
        public readonly ViewObject ColumnZoneCodeName = new ViewObject(2, "ZoneCodeName", "$Views_TimeZones_CodeName");

        /// <summary>
        ///     ID:3 
        ///     Alias: ZoneUtcOffsetMinutes
        ///     Caption: $Views_TimeZones_UtcOffsetMinutes.
        /// </summary>
        public readonly ViewObject ColumnZoneUtcOffsetMinutes = new ViewObject(3, "ZoneUtcOffsetMinutes", "$Views_TimeZones_UtcOffsetMinutes");

        /// <summary>
        ///     ID:4 
        ///     Alias: ZoneDisplayName
        ///     Caption: $Views_TimeZones_DisplayName.
        /// </summary>
        public readonly ViewObject ColumnZoneDisplayName = new ViewObject(4, "ZoneDisplayName", "$Views_TimeZones_DisplayName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_TimeZones_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_TimeZones_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(TimeZonesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region TopicParticipants

    /// <summary>
    ///     ID: {982d76b5-8997-4265-a8a3-c5e427746834}
    ///     Alias: TopicParticipants
    ///     Caption: $Views_Names_TopicParticipants
    ///     Group: Fm
    /// </summary>
    public class TopicParticipantsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "TopicParticipants": {982d76b5-8997-4265-a8a3-c5e427746834}.
        /// </summary>
        public readonly Guid ID = new Guid(0x982d76b5,0x8997,0x4265,0xa8,0xa3,0xc5,0xe4,0x27,0x74,0x68,0x34);

        /// <summary>
        ///     View name for "TopicParticipants".
        /// </summary>
        public readonly string Alias = "TopicParticipants";

        /// <summary>
        ///     View caption for "TopicParticipants".
        /// </summary>
        public readonly string Caption = "$Views_Names_TopicParticipants";

        /// <summary>
        ///     View group for "TopicParticipants".
        /// </summary>
        public readonly string Group = "Fm";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: RoleID.
        /// </summary>
        public readonly ViewObject ColumnRoleID = new ViewObject(0, "RoleID");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleName
        ///     Caption: $Views_Roles_Role.
        /// </summary>
        public readonly ViewObject ColumnRoleName = new ViewObject(1, "RoleName", "$Views_Roles_Role");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(2, "TypeID");

        /// <summary>
        ///     ID:3 
        ///     Alias: TypeName
        ///     Caption: $Views_Roles_Type.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(3, "TypeName", "$Views_Roles_Type");

        /// <summary>
        ///     ID:4 
        ///     Alias: TopicID.
        /// </summary>
        public readonly ViewObject ColumnTopicID = new ViewObject(4, "TopicID");

        /// <summary>
        ///     ID:5 
        ///     Alias: ReadOnly
        ///     Caption: $Views_TopicParticipants_ReadOnly.
        /// </summary>
        public readonly ViewObject ColumnReadOnly = new ViewObject(5, "ReadOnly", "$Views_TopicParticipants_ReadOnly");

        /// <summary>
        ///     ID:6 
        ///     Alias: Subscribed
        ///     Caption: $Views_TopicParticipants_Subscribed.
        /// </summary>
        public readonly ViewObject ColumnSubscribed = new ViewObject(6, "Subscribed", "$Views_TopicParticipants_Subscribed");

        /// <summary>
        ///     ID:7 
        ///     Alias: TypeParticipant
        ///     Caption: ParticipantTypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeParticipant = new ViewObject(7, "TypeParticipant", "ParticipantTypeID");

        /// <summary>
        ///     ID:8 
        ///     Alias: TypeParticipantName
        ///     Caption: $Views_TopicParticipants_TypeParticipant.
        /// </summary>
        public readonly ViewObject ColumnTypeParticipantName = new ViewObject(8, "TypeParticipantName", "$Views_TopicParticipants_TypeParticipant");

        /// <summary>
        ///     ID:9 
        ///     Alias: InvitingUserName
        ///     Caption: $Views_TopicParticipants_InvitingUserName.
        /// </summary>
        public readonly ViewObject ColumnInvitingUserName = new ViewObject(9, "InvitingUserName", "$Views_TopicParticipants_InvitingUserName");

        /// <summary>
        ///     ID:10 
        ///     Alias: Info
        ///     Caption: $Views_Roles_Info.
        /// </summary>
        public readonly ViewObject ColumnInfo = new ViewObject(10, "Info", "$Views_Roles_Info");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Roles_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Roles_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeID
        ///     Caption: $Views_Roles_Type_Param.
        /// </summary>
        public readonly ViewObject ParamTypeID = new ViewObject(1, "TypeID", "$Views_Roles_Type_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: GeneratorID
        ///     Caption: $Views_Roles_Generator_Param.
        /// </summary>
        public readonly ViewObject ParamGeneratorID = new ViewObject(2, "GeneratorID", "$Views_Roles_Generator_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: TopicID
        ///     Caption: $Views_TopicParticipants_Topic.
        /// </summary>
        public readonly ViewObject ParamTopicID = new ViewObject(3, "TopicID", "$Views_TopicParticipants_Topic");

        /// <summary>
        ///     ID:4 
        ///     Alias: ParticipantTypeID
        ///     Caption: ParticipantTypeID.
        /// </summary>
        public readonly ViewObject ParamParticipantTypeID = new ViewObject(4, "ParticipantTypeID", "ParticipantTypeID");

        /// <summary>
        ///     ID:5 
        ///     Alias: CardID
        ///     Caption: CardID.
        /// </summary>
        public readonly ViewObject ParamCardID = new ViewObject(5, "CardID", "CardID");

        /// <summary>
        ///     ID:6 
        ///     Alias: IsStaticRole
        ///     Caption: $Views_TopicParticipants_IsStaticRole.
        /// </summary>
        public readonly ViewObject ParamIsStaticRole = new ViewObject(6, "IsStaticRole", "$Views_TopicParticipants_IsStaticRole");

        /// <summary>
        ///     ID:7 
        ///     Alias: ShowHidden
        ///     Caption: $Views_Roles_ShowHidden_Param.
        /// </summary>
        public readonly ViewObject ParamShowHidden = new ViewObject(7, "ShowHidden", "$Views_Roles_ShowHidden_Param");

        #endregion

        #region ToString

        public static implicit operator string(TopicParticipantsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Types

    /// <summary>
    ///     ID: {77b991d4-3f6d-4827-ae02-354e514f6c60}
    ///     Alias: Types
    ///     Caption: $Views_Names_Types
    ///     Group: System
    /// </summary>
    public class TypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Types": {77b991d4-3f6d-4827-ae02-354e514f6c60}.
        /// </summary>
        public readonly Guid ID = new Guid(0x77b991d4,0x3f6d,0x4827,0xae,0x02,0x35,0x4e,0x51,0x4f,0x6c,0x60);

        /// <summary>
        ///     View name for "Types".
        /// </summary>
        public readonly string Alias = "Types";

        /// <summary>
        ///     View caption for "Types".
        /// </summary>
        public readonly string Caption = "$Views_Names_Types";

        /// <summary>
        ///     View group for "Types".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeCaption
        ///     Caption: $Views_Types_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeCaption = new ViewObject(1, "TypeCaption", "$Views_Types_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: TypeName
        ///     Caption: $Views_Types_Alias.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(2, "TypeName", "$Views_Types_Alias");

        /// <summary>
        ///     ID:3 
        ///     Alias: rn.
        /// </summary>
        public readonly ViewObject Columnrn = new ViewObject(3, "rn");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Caption
        ///     Caption: $Views_Types_Name_Param.
        /// </summary>
        public readonly ViewObject ParamCaption = new ViewObject(0, "Caption", "$Views_Types_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Name
        ///     Caption: $Views_Types_Alias_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(1, "Name", "$Views_Types_Alias_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: IsTypeForSettings
        ///     Caption: $Views_Types_IsTypeForSettings_Param.
        /// </summary>
        public readonly ViewObject ParamIsTypeForSettings = new ViewObject(2, "IsTypeForSettings", "$Views_Types_IsTypeForSettings_Param");

        #endregion

        #region ToString

        public static implicit operator string(TypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Users

    /// <summary>
    ///     ID: {8b68754e-19c8-0984-aac8-51d8908acecf}
    ///     Alias: Users
    ///     Caption: $Views_Names_Users
    ///     Group: System
    /// </summary>
    public class UsersViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Users": {8b68754e-19c8-0984-aac8-51d8908acecf}.
        /// </summary>
        public readonly Guid ID = new Guid(0x8b68754e,0x19c8,0x0984,0xaa,0xc8,0x51,0xd8,0x90,0x8a,0xce,0xcf);

        /// <summary>
        ///     View name for "Users".
        /// </summary>
        public readonly string Alias = "Users";

        /// <summary>
        ///     View caption for "Users".
        /// </summary>
        public readonly string Caption = "$Views_Names_Users";

        /// <summary>
        ///     View group for "Users".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(0, "UserID");

        /// <summary>
        ///     ID:1 
        ///     Alias: UserName
        ///     Caption: $Views_Users_Name.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(1, "UserName", "$Views_Users_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: Email
        ///     Caption: $Views_Users_Email.
        /// </summary>
        public readonly ViewObject ColumnEmail = new ViewObject(2, "Email", "$Views_Users_Email");

        /// <summary>
        ///     ID:3 
        ///     Alias: Position
        ///     Caption: $Views_Users_Position.
        /// </summary>
        public readonly ViewObject ColumnPosition = new ViewObject(3, "Position", "$Views_Users_Position");

        /// <summary>
        ///     ID:4 
        ///     Alias: Departments
        ///     Caption: $Views_Users_Departments.
        /// </summary>
        public readonly ViewObject ColumnDepartments = new ViewObject(4, "Departments", "$Views_Users_Departments");

        /// <summary>
        ///     ID:5 
        ///     Alias: StaticRoles
        ///     Caption: $Views_Users_StaticRoles.
        /// </summary>
        public readonly ViewObject ColumnStaticRoles = new ViewObject(5, "StaticRoles", "$Views_Users_StaticRoles");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Users_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Users_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: RoleID
        ///     Caption: $Views_Users_Role_Param.
        /// </summary>
        public readonly ViewObject ParamRoleID = new ViewObject(1, "RoleID", "$Views_Users_Role_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: RoleExclusionID
        ///     Caption: $Views_Users_RoleExclusion_Param.
        /// </summary>
        public readonly ViewObject ParamRoleExclusionID = new ViewObject(2, "RoleExclusionID", "$Views_Users_RoleExclusion_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: DepartmentRoleID
        ///     Caption: $Views_Users_Role_Param.
        /// </summary>
        public readonly ViewObject ParamDepartmentRoleID = new ViewObject(3, "DepartmentRoleID", "$Views_Users_Role_Param");

        /// <summary>
        ///     ID:4 
        ///     Alias: StaticRoleID
        ///     Caption: $Views_Users_Role_Param.
        /// </summary>
        public readonly ViewObject ParamStaticRoleID = new ViewObject(4, "StaticRoleID", "$Views_Users_Role_Param");

        /// <summary>
        ///     ID:5 
        ///     Alias: ParentRoleID
        ///     Caption: $Views_Users_ParentRole_Param.
        /// </summary>
        public readonly ViewObject ParamParentRoleID = new ViewObject(5, "ParentRoleID", "$Views_Users_ParentRole_Param");

        /// <summary>
        ///     ID:6 
        ///     Alias: ShowHidden
        ///     Caption: $Views_Users_ShowHidden_Param.
        /// </summary>
        public readonly ViewObject ParamShowHidden = new ViewObject(6, "ShowHidden", "$Views_Users_ShowHidden_Param");

        #endregion

        #region ToString

        public static implicit operator string(UsersViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region VatTypes

    /// <summary>
    ///     ID: {c14070dd-219a-4340-91f3-7c2ffd891382}
    ///     Alias: VatTypes
    ///     Caption: $Views_Names_VatTypes
    ///     Group: System
    /// </summary>
    public class VatTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "VatTypes": {c14070dd-219a-4340-91f3-7c2ffd891382}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc14070dd,0x219a,0x4340,0x91,0xf3,0x7c,0x2f,0xfd,0x89,0x13,0x82);

        /// <summary>
        ///     View name for "VatTypes".
        /// </summary>
        public readonly string Alias = "VatTypes";

        /// <summary>
        ///     View caption for "VatTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_VatTypes";

        /// <summary>
        ///     View group for "VatTypes".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: VatTypeID.
        /// </summary>
        public readonly ViewObject ColumnVatTypeID = new ViewObject(0, "VatTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: VatTypeName
        ///     Caption: $Views_VatTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnVatTypeName = new ViewObject(1, "VatTypeName", "$Views_VatTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_VatTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_VatTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(VatTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region ViewFiles

    /// <summary>
    ///     ID: {af26a537-a1c9-4e09-9048-b892cc0c687e}
    ///     Alias: ViewFiles
    ///     Caption: $Views_Names_ViewFiles
    ///     Group: Testing
    /// </summary>
    public class ViewFilesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "ViewFiles": {af26a537-a1c9-4e09-9048-b892cc0c687e}.
        /// </summary>
        public readonly Guid ID = new Guid(0xaf26a537,0xa1c9,0x4e09,0x90,0x48,0xb8,0x92,0xcc,0x0c,0x68,0x7e);

        /// <summary>
        ///     View name for "ViewFiles".
        /// </summary>
        public readonly string Alias = "ViewFiles";

        /// <summary>
        ///     View caption for "ViewFiles".
        /// </summary>
        public readonly string Caption = "$Views_Names_ViewFiles";

        /// <summary>
        ///     View group for "ViewFiles".
        /// </summary>
        public readonly string Group = "Testing";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: FullFileName
        ///     Caption: FullFileName.
        /// </summary>
        public readonly ViewObject ColumnFullFileName = new ViewObject(0, "FullFileName", "FullFileName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_ViewFiles_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_ViewFiles_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: Folder
        ///     Caption: $Views_ViewFiles_Folder_Param.
        /// </summary>
        public readonly ViewObject ParamFolder = new ViewObject(1, "Folder", "$Views_ViewFiles_Folder_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: ParentFolder
        ///     Caption: $Views_ViewFiles_ParentFolder_Param.
        /// </summary>
        public readonly ViewObject ParamParentFolder = new ViewObject(2, "ParentFolder", "$Views_ViewFiles_ParentFolder_Param");

        #endregion

        #region ToString

        public static implicit operator string(ViewFilesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Views

    /// <summary>
    ///     ID: {54614ef7-6a51-46c4-aa03-93814eb79126}
    ///     Alias: Views
    ///     Caption: $Views_Names_Views
    ///     Group: System
    /// </summary>
    public class ViewsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Views": {54614ef7-6a51-46c4-aa03-93814eb79126}.
        /// </summary>
        public readonly Guid ID = new Guid(0x54614ef7,0x6a51,0x46c4,0xaa,0x03,0x93,0x81,0x4e,0xb7,0x91,0x26);

        /// <summary>
        ///     View name for "Views".
        /// </summary>
        public readonly string Alias = "Views";

        /// <summary>
        ///     View caption for "Views".
        /// </summary>
        public readonly string Caption = "$Views_Names_Views";

        /// <summary>
        ///     View group for "Views".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ViewID.
        /// </summary>
        public readonly ViewObject ColumnViewID = new ViewObject(0, "ViewID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ViewAlias
        ///     Caption: $Views_KrTypes_Alias.
        /// </summary>
        public readonly ViewObject ColumnViewAlias = new ViewObject(1, "ViewAlias", "$Views_KrTypes_Alias");

        /// <summary>
        ///     ID:2 
        ///     Alias: ViewCaption
        ///     Caption: $Views_KrTypes_Caption.
        /// </summary>
        public readonly ViewObject ColumnViewCaption = new ViewObject(2, "ViewCaption", "$Views_KrTypes_Caption");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: AliasOrCaption
        ///     Caption: $Views_KrTypes_AliasOrCaption_Param.
        /// </summary>
        public readonly ViewObject ParamAliasOrCaption = new ViewObject(0, "AliasOrCaption", "$Views_KrTypes_AliasOrCaption_Param");

        #endregion

        #region ToString

        public static implicit operator string(ViewsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WebApplications

    /// <summary>
    ///     ID: {7849787c-5422-4a08-9652-dd77d8557f4a}
    ///     Alias: WebApplications
    ///     Caption: $Views_Names_WebApplications
    ///     Group: System
    /// </summary>
    public class WebApplicationsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WebApplications": {7849787c-5422-4a08-9652-dd77d8557f4a}.
        /// </summary>
        public readonly Guid ID = new Guid(0x7849787c,0x5422,0x4a08,0x96,0x52,0xdd,0x77,0xd8,0x55,0x7f,0x4a);

        /// <summary>
        ///     View name for "WebApplications".
        /// </summary>
        public readonly string Alias = "WebApplications";

        /// <summary>
        ///     View caption for "WebApplications".
        /// </summary>
        public readonly string Caption = "$Views_Names_WebApplications";

        /// <summary>
        ///     View group for "WebApplications".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: AppID.
        /// </summary>
        public readonly ViewObject ColumnAppID = new ViewObject(0, "AppID");

        /// <summary>
        ///     ID:1 
        ///     Alias: AppName
        ///     Caption: $Views_WebApplications_Name.
        /// </summary>
        public readonly ViewObject ColumnAppName = new ViewObject(1, "AppName", "$Views_WebApplications_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: LanguageID.
        /// </summary>
        public readonly ViewObject ColumnLanguageID = new ViewObject(2, "LanguageID");

        /// <summary>
        ///     ID:3 
        ///     Alias: LanguageCode.
        /// </summary>
        public readonly ViewObject ColumnLanguageCode = new ViewObject(3, "LanguageCode");

        /// <summary>
        ///     ID:4 
        ///     Alias: LanguageCaption
        ///     Caption: $Views_WebApplications_LanguageCaption.
        /// </summary>
        public readonly ViewObject ColumnLanguageCaption = new ViewObject(4, "LanguageCaption", "$Views_WebApplications_LanguageCaption");

        /// <summary>
        ///     ID:5 
        ///     Alias: OSName
        ///     Caption: $Views_WebApplications_OSName.
        /// </summary>
        public readonly ViewObject ColumnOSName = new ViewObject(5, "OSName", "$Views_WebApplications_OSName");

        /// <summary>
        ///     ID:6 
        ///     Alias: Client64Bit
        ///     Caption: $Views_WebApplications_Client64Bit.
        /// </summary>
        public readonly ViewObject ColumnClient64Bit = new ViewObject(6, "Client64Bit", "$Views_WebApplications_Client64Bit");

        /// <summary>
        ///     ID:7 
        ///     Alias: ExecutableFileName.
        /// </summary>
        public readonly ViewObject ColumnExecutableFileName = new ViewObject(7, "ExecutableFileName");

        /// <summary>
        ///     ID:8 
        ///     Alias: AppVersion
        ///     Caption: $Views_WebApplications_AppVersion.
        /// </summary>
        public readonly ViewObject ColumnAppVersion = new ViewObject(8, "AppVersion", "$Views_WebApplications_AppVersion");

        /// <summary>
        ///     ID:9 
        ///     Alias: PlatformVersion
        ///     Caption: $Views_WebApplications_PlatformVersion.
        /// </summary>
        public readonly ViewObject ColumnPlatformVersion = new ViewObject(9, "PlatformVersion", "$Views_WebApplications_PlatformVersion");

        /// <summary>
        ///     ID:10 
        ///     Alias: Description
        ///     Caption: $Views_WebApplications_Description.
        /// </summary>
        public readonly ViewObject ColumnDescription = new ViewObject(10, "Description", "$Views_WebApplications_Description");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_WebApplications_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_WebApplications_Name_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: CheckAvailable
        ///     Caption: $Views_WebApplications_CheckAvailable_Param.
        /// </summary>
        public readonly ViewObject ParamCheckAvailable = new ViewObject(1, "CheckAvailable", "$Views_WebApplications_CheckAvailable_Param");

        #endregion

        #region ToString

        public static implicit operator string(WebApplicationsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WeTaskControlTypes

    /// <summary>
    ///     ID: {81a77ff3-5f38-42a4-bdaf-61ec8c508c39}
    ///     Alias: WeTaskControlTypes
    ///     Caption: $Views_Names_WeTaskControlTypes
    ///     Group: WorkflowEngine
    /// </summary>
    public class WeTaskControlTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WeTaskControlTypes": {81a77ff3-5f38-42a4-bdaf-61ec8c508c39}.
        /// </summary>
        public readonly Guid ID = new Guid(0x81a77ff3,0x5f38,0x42a4,0xbd,0xaf,0x61,0xec,0x8c,0x50,0x8c,0x39);

        /// <summary>
        ///     View name for "WeTaskControlTypes".
        /// </summary>
        public readonly string Alias = "WeTaskControlTypes";

        /// <summary>
        ///     View caption for "WeTaskControlTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_WeTaskControlTypes";

        /// <summary>
        ///     View group for "WeTaskControlTypes".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ControlTypeID.
        /// </summary>
        public readonly ViewObject ColumnControlTypeID = new ViewObject(0, "ControlTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ControlTypeName
        ///     Caption: $Views_WeTaskControlTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnControlTypeName = new ViewObject(1, "ControlTypeName", "$Views_WeTaskControlTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_WeTaskControlTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_WeTaskControlTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(WeTaskControlTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WeTaskGroupActionOptionTypes

    /// <summary>
    ///     ID: {6a66914d-790f-480a-9976-cb85cc67e028}
    ///     Alias: WeTaskGroupActionOptionTypes
    ///     Caption: $Views_Names_WeTaskGroupActionOptionTypes
    ///     Group: WorkflowEngine
    /// </summary>
    public class WeTaskGroupActionOptionTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WeTaskGroupActionOptionTypes": {6a66914d-790f-480a-9976-cb85cc67e028}.
        /// </summary>
        public readonly Guid ID = new Guid(0x6a66914d,0x790f,0x480a,0x99,0x76,0xcb,0x85,0xcc,0x67,0xe0,0x28);

        /// <summary>
        ///     View name for "WeTaskGroupActionOptionTypes".
        /// </summary>
        public readonly string Alias = "WeTaskGroupActionOptionTypes";

        /// <summary>
        ///     View caption for "WeTaskGroupActionOptionTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_WeTaskGroupActionOptionTypes";

        /// <summary>
        ///     View group for "WeTaskGroupActionOptionTypes".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: OptionTypeID.
        /// </summary>
        public readonly ViewObject ColumnOptionTypeID = new ViewObject(0, "OptionTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: OptionTypeName
        ///     Caption: $Views_WeTaskGroupActionOptionTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnOptionTypeName = new ViewObject(1, "OptionTypeName", "$Views_WeTaskGroupActionOptionTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_WeTaskGroupActionOptionTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_WeTaskGroupActionOptionTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(WeTaskGroupActionOptionTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WfResolutionAuthors

    /// <summary>
    ///     ID: {f3cc3c6c-67c6-477e-a2ab-99fb0bd6e95f}
    ///     Alias: WfResolutionAuthors
    ///     Caption: $Views_Names_WfResolutionAuthors
    ///     Group: Kr Wf
    /// </summary>
    public class WfResolutionAuthorsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WfResolutionAuthors": {f3cc3c6c-67c6-477e-a2ab-99fb0bd6e95f}.
        /// </summary>
        public readonly Guid ID = new Guid(0xf3cc3c6c,0x67c6,0x477e,0xa2,0xab,0x99,0xfb,0x0b,0xd6,0xe9,0x5f);

        /// <summary>
        ///     View name for "WfResolutionAuthors".
        /// </summary>
        public readonly string Alias = "WfResolutionAuthors";

        /// <summary>
        ///     View caption for "WfResolutionAuthors".
        /// </summary>
        public readonly string Caption = "$Views_Names_WfResolutionAuthors";

        /// <summary>
        ///     View group for "WfResolutionAuthors".
        /// </summary>
        public readonly string Group = "Kr Wf";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: UserID.
        /// </summary>
        public readonly ViewObject ColumnUserID = new ViewObject(0, "UserID");

        /// <summary>
        ///     ID:1 
        ///     Alias: UserName
        ///     Caption: $Views_WfResolutionAuthors_Name.
        /// </summary>
        public readonly ViewObject ColumnUserName = new ViewObject(1, "UserName", "$Views_WfResolutionAuthors_Name");

        /// <summary>
        ///     ID:2 
        ///     Alias: Departments
        ///     Caption: $Views_WfResolutionAuthors_Departments.
        /// </summary>
        public readonly ViewObject ColumnDepartments = new ViewObject(2, "Departments", "$Views_WfResolutionAuthors_Departments");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_WfResolutionAuthors_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_WfResolutionAuthors_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(WfResolutionAuthorsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowEngineCompiledBaseTypes

    /// <summary>
    ///     ID: {d3ba4765-6a54-465e-8803-323050fd951e}
    ///     Alias: WorkflowEngineCompiledBaseTypes
    ///     Caption: $Views_Names_WorkflowEngineCompiledBaseTypes
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowEngineCompiledBaseTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WorkflowEngineCompiledBaseTypes": {d3ba4765-6a54-465e-8803-323050fd951e}.
        /// </summary>
        public readonly Guid ID = new Guid(0xd3ba4765,0x6a54,0x465e,0x88,0x03,0x32,0x30,0x50,0xfd,0x95,0x1e);

        /// <summary>
        ///     View name for "WorkflowEngineCompiledBaseTypes".
        /// </summary>
        public readonly string Alias = "WorkflowEngineCompiledBaseTypes";

        /// <summary>
        ///     View caption for "WorkflowEngineCompiledBaseTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_WorkflowEngineCompiledBaseTypes";

        /// <summary>
        ///     View group for "WorkflowEngineCompiledBaseTypes".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TypeID.
        /// </summary>
        public readonly ViewObject ColumnTypeID = new ViewObject(0, "TypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TypeName
        ///     Caption: $Views_WorkflowEngineCompiledBaseTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnTypeName = new ViewObject(1, "TypeName", "$Views_WorkflowEngineCompiledBaseTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_WorkflowEngineCompiledBaseTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_WorkflowEngineCompiledBaseTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineCompiledBaseTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowEngineErrors

    /// <summary>
    ///     ID: {6138ccf4-b5a2-4789-a3c2-82382ae666a2}
    ///     Alias: WorkflowEngineErrors
    ///     Caption: $Views_Names_WorkflowEngineErrors
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowEngineErrorsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WorkflowEngineErrors": {6138ccf4-b5a2-4789-a3c2-82382ae666a2}.
        /// </summary>
        public readonly Guid ID = new Guid(0x6138ccf4,0xb5a2,0x4789,0xa3,0xc2,0x82,0x38,0x2a,0xe6,0x66,0xa2);

        /// <summary>
        ///     View name for "WorkflowEngineErrors".
        /// </summary>
        public readonly string Alias = "WorkflowEngineErrors";

        /// <summary>
        ///     View caption for "WorkflowEngineErrors".
        /// </summary>
        public readonly string Caption = "$Views_Names_WorkflowEngineErrors";

        /// <summary>
        ///     View group for "WorkflowEngineErrors".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ProcessErrorID.
        /// </summary>
        public readonly ViewObject ColumnProcessErrorID = new ViewObject(0, "ProcessErrorID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ProcessErrorAdded
        ///     Caption: $Views_WorkflowEngineErrors_Added.
        /// </summary>
        public readonly ViewObject ColumnProcessErrorAdded = new ViewObject(1, "ProcessErrorAdded", "$Views_WorkflowEngineErrors_Added");

        /// <summary>
        ///     ID:2 
        ///     Alias: ProcessErrorText
        ///     Caption: $Views_WorkflowEngineErrors_ErrorText.
        /// </summary>
        public readonly ViewObject ColumnProcessErrorText = new ViewObject(2, "ProcessErrorText", "$Views_WorkflowEngineErrors_ErrorText");

        /// <summary>
        ///     ID:3 
        ///     Alias: ProcessNodeInstanceID.
        /// </summary>
        public readonly ViewObject ColumnProcessNodeInstanceID = new ViewObject(3, "ProcessNodeInstanceID");

        /// <summary>
        ///     ID:4 
        ///     Alias: ProcessNodeID.
        /// </summary>
        public readonly ViewObject ColumnProcessNodeID = new ViewObject(4, "ProcessNodeID");

        /// <summary>
        ///     ID:5 
        ///     Alias: ProcessIsAsync.
        /// </summary>
        public readonly ViewObject ColumnProcessIsAsync = new ViewObject(5, "ProcessIsAsync");

        /// <summary>
        ///     ID:6 
        ///     Alias: ProcessResumable.
        /// </summary>
        public readonly ViewObject ColumnProcessResumable = new ViewObject(6, "ProcessResumable");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Added
        ///     Caption: $Views_WorkflowEngineErrors_Added_Param.
        /// </summary>
        public readonly ViewObject ParamAdded = new ViewObject(0, "Added", "$Views_WorkflowEngineErrors_Added_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: ProcessInstance
        ///     Caption: $Views_WorkflowEngineErrors_Process_Param.
        /// </summary>
        public readonly ViewObject ParamProcessInstance = new ViewObject(1, "ProcessInstance", "$Views_WorkflowEngineErrors_Process_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: Active
        ///     Caption: $Views_WorkflowEngineErrors_ShowActiveOnly_Param.
        /// </summary>
        public readonly ViewObject ParamActive = new ViewObject(2, "Active", "$Views_WorkflowEngineErrors_ShowActiveOnly_Param");

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineErrorsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowEngineLogLevels

    /// <summary>
    ///     ID: {e91f6c3a-c8a0-46d3-bc07-5277f0e7d3f7}
    ///     Alias: WorkflowEngineLogLevels
    ///     Caption: $Views_Names_WorkflowEngineLogLevels
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowEngineLogLevelsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WorkflowEngineLogLevels": {e91f6c3a-c8a0-46d3-bc07-5277f0e7d3f7}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe91f6c3a,0xc8a0,0x46d3,0xbc,0x07,0x52,0x77,0xf0,0xe7,0xd3,0xf7);

        /// <summary>
        ///     View name for "WorkflowEngineLogLevels".
        /// </summary>
        public readonly string Alias = "WorkflowEngineLogLevels";

        /// <summary>
        ///     View caption for "WorkflowEngineLogLevels".
        /// </summary>
        public readonly string Caption = "$Views_Names_WorkflowEngineLogLevels";

        /// <summary>
        ///     View group for "WorkflowEngineLogLevels".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: LogLevelID.
        /// </summary>
        public readonly ViewObject ColumnLogLevelID = new ViewObject(0, "LogLevelID");

        /// <summary>
        ///     ID:1 
        ///     Alias: LogLevelName
        ///     Caption: $Views_WorkflowEngineLogLevels_Name.
        /// </summary>
        public readonly ViewObject ColumnLogLevelName = new ViewObject(1, "LogLevelName", "$Views_WorkflowEngineLogLevels_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_WorkflowEngineLogLevels_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_WorkflowEngineLogLevels_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineLogLevelsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowEngineLogs

    /// <summary>
    ///     ID: {db1faa0a-fdfd-4a97-80e4-c1573d47b6c3}
    ///     Alias: WorkflowEngineLogs
    ///     Caption: $Views_Names_WorkflowEngineLogs
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowEngineLogsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WorkflowEngineLogs": {db1faa0a-fdfd-4a97-80e4-c1573d47b6c3}.
        /// </summary>
        public readonly Guid ID = new Guid(0xdb1faa0a,0xfdfd,0x4a97,0x80,0xe4,0xc1,0x57,0x3d,0x47,0xb6,0xc3);

        /// <summary>
        ///     View name for "WorkflowEngineLogs".
        /// </summary>
        public readonly string Alias = "WorkflowEngineLogs";

        /// <summary>
        ///     View caption for "WorkflowEngineLogs".
        /// </summary>
        public readonly string Caption = "$Views_Names_WorkflowEngineLogs";

        /// <summary>
        ///     View group for "WorkflowEngineLogs".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ProcessLogID.
        /// </summary>
        public readonly ViewObject ColumnProcessLogID = new ViewObject(0, "ProcessLogID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ProcessLogAdded
        ///     Caption: $Views_WorkflowEngineLogs_Added.
        /// </summary>
        public readonly ViewObject ColumnProcessLogAdded = new ViewObject(1, "ProcessLogAdded", "$Views_WorkflowEngineLogs_Added");

        /// <summary>
        ///     ID:2 
        ///     Alias: ProcessLogLevel
        ///     Caption: $Views_WorkflowEngineLogs_LogLevel.
        /// </summary>
        public readonly ViewObject ColumnProcessLogLevel = new ViewObject(2, "ProcessLogLevel", "$Views_WorkflowEngineLogs_LogLevel");

        /// <summary>
        ///     ID:3 
        ///     Alias: ProcessLogObject
        ///     Caption: $Views_WorkflowEngineLogs_LogObject.
        /// </summary>
        public readonly ViewObject ColumnProcessLogObject = new ViewObject(3, "ProcessLogObject", "$Views_WorkflowEngineLogs_LogObject");

        /// <summary>
        ///     ID:4 
        ///     Alias: ProcessLogText
        ///     Caption: $Views_WorkflowEngineLogs_Text.
        /// </summary>
        public readonly ViewObject ColumnProcessLogText = new ViewObject(4, "ProcessLogText", "$Views_WorkflowEngineLogs_Text");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Added
        ///     Caption: $Views_WorkflowEngineLogs_Added_Param.
        /// </summary>
        public readonly ViewObject ParamAdded = new ViewObject(0, "Added", "$Views_WorkflowEngineLogs_Added_Param");

        /// <summary>
        ///     ID:1 
        ///     Alias: ProcessInstance
        ///     Caption: $Views_WorkflowEngineLogs_Process_Param.
        /// </summary>
        public readonly ViewObject ParamProcessInstance = new ViewObject(1, "ProcessInstance", "$Views_WorkflowEngineLogs_Process_Param");

        /// <summary>
        ///     ID:2 
        ///     Alias: LogLevel
        ///     Caption: $Views_WorkflowEngineLogs_LogLevel_Param.
        /// </summary>
        public readonly ViewObject ParamLogLevel = new ViewObject(2, "LogLevel", "$Views_WorkflowEngineLogs_LogLevel_Param");

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineLogsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowEngineTaskActions

    /// <summary>
    ///     ID: {c39c3de3-6448-4c35-8978-4b385ca6a647}
    ///     Alias: WorkflowEngineTaskActions
    ///     Caption: $Views_Names_WorkflowEngineTaskActions
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowEngineTaskActionsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WorkflowEngineTaskActions": {c39c3de3-6448-4c35-8978-4b385ca6a647}.
        /// </summary>
        public readonly Guid ID = new Guid(0xc39c3de3,0x6448,0x4c35,0x89,0x78,0x4b,0x38,0x5c,0xa6,0xa6,0x47);

        /// <summary>
        ///     View name for "WorkflowEngineTaskActions".
        /// </summary>
        public readonly string Alias = "WorkflowEngineTaskActions";

        /// <summary>
        ///     View caption for "WorkflowEngineTaskActions".
        /// </summary>
        public readonly string Caption = "$Views_Names_WorkflowEngineTaskActions";

        /// <summary>
        ///     View group for "WorkflowEngineTaskActions".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TaskActionID.
        /// </summary>
        public readonly ViewObject ColumnTaskActionID = new ViewObject(0, "TaskActionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TaskActionName
        ///     Caption: $Views_WorkflowEngineTaskActions_Name.
        /// </summary>
        public readonly ViewObject ColumnTaskActionName = new ViewObject(1, "TaskActionName", "$Views_WorkflowEngineTaskActions_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_WorkflowEngineTaskActions_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_WorkflowEngineTaskActions_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineTaskActionsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowEngineTileManagerExtensions

    /// <summary>
    ///     ID: {e102a6c1-807c-4976-b2e7-180404090e75}
    ///     Alias: WorkflowEngineTileManagerExtensions
    ///     Caption: $Views_Names_WorkflowEngineTileManagerExtensions
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowEngineTileManagerExtensionsViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WorkflowEngineTileManagerExtensions": {e102a6c1-807c-4976-b2e7-180404090e75}.
        /// </summary>
        public readonly Guid ID = new Guid(0xe102a6c1,0x807c,0x4976,0xb2,0xe7,0x18,0x04,0x04,0x09,0x0e,0x75);

        /// <summary>
        ///     View name for "WorkflowEngineTileManagerExtensions".
        /// </summary>
        public readonly string Alias = "WorkflowEngineTileManagerExtensions";

        /// <summary>
        ///     View caption for "WorkflowEngineTileManagerExtensions".
        /// </summary>
        public readonly string Caption = "$Views_Names_WorkflowEngineTileManagerExtensions";

        /// <summary>
        ///     View group for "WorkflowEngineTileManagerExtensions".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: ExtensionID.
        /// </summary>
        public readonly ViewObject ColumnExtensionID = new ViewObject(0, "ExtensionID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ExtensionName
        ///     Caption: $Views_WorkflowEngineTileManagerExtensions_Name.
        /// </summary>
        public readonly ViewObject ColumnExtensionName = new ViewObject(1, "ExtensionName", "$Views_WorkflowEngineTileManagerExtensions_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_WorkflowEngineTileManagerExtensions_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_WorkflowEngineTileManagerExtensions_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(WorkflowEngineTileManagerExtensionsViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowLinkModes

    /// <summary>
    ///     ID: {cc6b5f26-00f7-4e57-9260-49ed427fd243}
    ///     Alias: WorkflowLinkModes
    ///     Caption: $Views_Names_WorkflowLinkModes
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowLinkModesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WorkflowLinkModes": {cc6b5f26-00f7-4e57-9260-49ed427fd243}.
        /// </summary>
        public readonly Guid ID = new Guid(0xcc6b5f26,0x00f7,0x4e57,0x92,0x60,0x49,0xed,0x42,0x7f,0xd2,0x43);

        /// <summary>
        ///     View name for "WorkflowLinkModes".
        /// </summary>
        public readonly string Alias = "WorkflowLinkModes";

        /// <summary>
        ///     View caption for "WorkflowLinkModes".
        /// </summary>
        public readonly string Caption = "$Views_Names_WorkflowLinkModes";

        /// <summary>
        ///     View group for "WorkflowLinkModes".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: LinkModeID.
        /// </summary>
        public readonly ViewObject ColumnLinkModeID = new ViewObject(0, "LinkModeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: LinkModeName
        ///     Caption: $Views_WorkflowLinkModes_Name.
        /// </summary>
        public readonly ViewObject ColumnLinkModeName = new ViewObject(1, "LinkModeName", "$Views_WorkflowLinkModes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_WorkflowLinkModes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_WorkflowLinkModes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(WorkflowLinkModesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowNodeInstanceSubprocesses

    /// <summary>
    ///     ID: {a08fa0ff-ec43-4848-9130-b7b5728fc686}
    ///     Alias: WorkflowNodeInstanceSubprocesses
    ///     Caption: $Views_Names_WorkflowNodeInstanceSubprocesses
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowNodeInstanceSubprocessesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WorkflowNodeInstanceSubprocesses": {a08fa0ff-ec43-4848-9130-b7b5728fc686}.
        /// </summary>
        public readonly Guid ID = new Guid(0xa08fa0ff,0xec43,0x4848,0x91,0x30,0xb7,0xb5,0x72,0x8f,0xc6,0x86);

        /// <summary>
        ///     View name for "WorkflowNodeInstanceSubprocesses".
        /// </summary>
        public readonly string Alias = "WorkflowNodeInstanceSubprocesses";

        /// <summary>
        ///     View caption for "WorkflowNodeInstanceSubprocesses".
        /// </summary>
        public readonly string Caption = "$Views_Names_WorkflowNodeInstanceSubprocesses";

        /// <summary>
        ///     View group for "WorkflowNodeInstanceSubprocesses".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SubprocessID.
        /// </summary>
        public readonly ViewObject ColumnSubprocessID = new ViewObject(0, "SubprocessID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SubprocessName.
        /// </summary>
        public readonly ViewObject ColumnSubprocessName = new ViewObject(1, "SubprocessName");

        /// <summary>
        ///     ID:2 
        ///     Alias: SubprocessCreated.
        /// </summary>
        public readonly ViewObject ColumnSubprocessCreated = new ViewObject(2, "SubprocessCreated");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: NodeInstanceID
        ///     Caption: Node instance ID.
        /// </summary>
        public readonly ViewObject ParamNodeInstanceID = new ViewObject(0, "NodeInstanceID", "Node instance ID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ProcessInstance
        ///     Caption: Process instance.
        /// </summary>
        public readonly ViewObject ParamProcessInstance = new ViewObject(1, "ProcessInstance", "Process instance");

        #endregion

        #region ToString

        public static implicit operator string(WorkflowNodeInstanceSubprocessesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowNodeInstanceTasks

    /// <summary>
    ///     ID: {31853f54-cb3d-4004-a79c-a020cc0014c3}
    ///     Alias: WorkflowNodeInstanceTasks
    ///     Caption: $Views_Names_WorkflowNodeInstanceTasks
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowNodeInstanceTasksViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WorkflowNodeInstanceTasks": {31853f54-cb3d-4004-a79c-a020cc0014c3}.
        /// </summary>
        public readonly Guid ID = new Guid(0x31853f54,0xcb3d,0x4004,0xa7,0x9c,0xa0,0x20,0xcc,0x00,0x14,0xc3);

        /// <summary>
        ///     View name for "WorkflowNodeInstanceTasks".
        /// </summary>
        public readonly string Alias = "WorkflowNodeInstanceTasks";

        /// <summary>
        ///     View caption for "WorkflowNodeInstanceTasks".
        /// </summary>
        public readonly string Caption = "$Views_Names_WorkflowNodeInstanceTasks";

        /// <summary>
        ///     View group for "WorkflowNodeInstanceTasks".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: TaskID.
        /// </summary>
        public readonly ViewObject ColumnTaskID = new ViewObject(0, "TaskID");

        /// <summary>
        ///     ID:1 
        ///     Alias: TaskTypeID.
        /// </summary>
        public readonly ViewObject ColumnTaskTypeID = new ViewObject(1, "TaskTypeID");

        /// <summary>
        ///     ID:2 
        ///     Alias: TaskTypeCaption.
        /// </summary>
        public readonly ViewObject ColumnTaskTypeCaption = new ViewObject(2, "TaskTypeCaption");

        /// <summary>
        ///     ID:3 
        ///     Alias: TaskRoleID.
        /// </summary>
        public readonly ViewObject ColumnTaskRoleID = new ViewObject(3, "TaskRoleID");

        /// <summary>
        ///     ID:4 
        ///     Alias: TaskRoleName.
        /// </summary>
        public readonly ViewObject ColumnTaskRoleName = new ViewObject(4, "TaskRoleName");

        /// <summary>
        ///     ID:5 
        ///     Alias: TaskUserID.
        /// </summary>
        public readonly ViewObject ColumnTaskUserID = new ViewObject(5, "TaskUserID");

        /// <summary>
        ///     ID:6 
        ///     Alias: TaskUserName.
        /// </summary>
        public readonly ViewObject ColumnTaskUserName = new ViewObject(6, "TaskUserName");

        /// <summary>
        ///     ID:7 
        ///     Alias: TaskAuthorID.
        /// </summary>
        public readonly ViewObject ColumnTaskAuthorID = new ViewObject(7, "TaskAuthorID");

        /// <summary>
        ///     ID:8 
        ///     Alias: TaskAuthorName.
        /// </summary>
        public readonly ViewObject ColumnTaskAuthorName = new ViewObject(8, "TaskAuthorName");

        /// <summary>
        ///     ID:9 
        ///     Alias: TaskStateID.
        /// </summary>
        public readonly ViewObject ColumnTaskStateID = new ViewObject(9, "TaskStateID");

        /// <summary>
        ///     ID:10 
        ///     Alias: TaskStateName.
        /// </summary>
        public readonly ViewObject ColumnTaskStateName = new ViewObject(10, "TaskStateName");

        /// <summary>
        ///     ID:11 
        ///     Alias: TaskDigest.
        /// </summary>
        public readonly ViewObject ColumnTaskDigest = new ViewObject(11, "TaskDigest");

        /// <summary>
        ///     ID:12 
        ///     Alias: TaskCreated.
        /// </summary>
        public readonly ViewObject ColumnTaskCreated = new ViewObject(12, "TaskCreated");

        /// <summary>
        ///     ID:13 
        ///     Alias: TaskPlanned.
        /// </summary>
        public readonly ViewObject ColumnTaskPlanned = new ViewObject(13, "TaskPlanned");

        /// <summary>
        ///     ID:14 
        ///     Alias: TaskInProgress.
        /// </summary>
        public readonly ViewObject ColumnTaskInProgress = new ViewObject(14, "TaskInProgress");

        /// <summary>
        ///     ID:15 
        ///     Alias: TaskPostponed.
        /// </summary>
        public readonly ViewObject ColumnTaskPostponed = new ViewObject(15, "TaskPostponed");

        /// <summary>
        ///     ID:16 
        ///     Alias: TaskPostponedTo.
        /// </summary>
        public readonly ViewObject ColumnTaskPostponedTo = new ViewObject(16, "TaskPostponedTo");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: NodeInstanceID
        ///     Caption: Node instance ID.
        /// </summary>
        public readonly ViewObject ParamNodeInstanceID = new ViewObject(0, "NodeInstanceID", "Node instance ID");

        /// <summary>
        ///     ID:1 
        ///     Alias: ProcessInstance
        ///     Caption: Process instance.
        /// </summary>
        public readonly ViewObject ParamProcessInstance = new ViewObject(1, "ProcessInstance", "Process instance");

        /// <summary>
        ///     ID:2 
        ///     Alias: FunctionRoleAuthorParam
        ///     Caption: $Views_MyTasks_FunctionRole_Author_Param.
        /// </summary>
        public readonly ViewObject ParamFunctionRoleAuthorParam = new ViewObject(2, "FunctionRoleAuthorParam", "$Views_MyTasks_FunctionRole_Author_Param");

        /// <summary>
        ///     ID:3 
        ///     Alias: FunctionRolePerformerParam
        ///     Caption: $Views_MyTasks_FunctionRole_Performer_Param.
        /// </summary>
        public readonly ViewObject ParamFunctionRolePerformerParam = new ViewObject(3, "FunctionRolePerformerParam", "$Views_MyTasks_FunctionRole_Performer_Param");

        #endregion

        #region ToString

        public static implicit operator string(WorkflowNodeInstanceTasksViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowSignalProcessingModes

    /// <summary>
    ///     ID: {718a1f3a-0a06-490d-8a55-654114c93d54}
    ///     Alias: WorkflowSignalProcessingModes
    ///     Caption: $Views_Names_WorkflowSignalProcessingModes
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowSignalProcessingModesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WorkflowSignalProcessingModes": {718a1f3a-0a06-490d-8a55-654114c93d54}.
        /// </summary>
        public readonly Guid ID = new Guid(0x718a1f3a,0x0a06,0x490d,0x8a,0x55,0x65,0x41,0x14,0xc9,0x3d,0x54);

        /// <summary>
        ///     View name for "WorkflowSignalProcessingModes".
        /// </summary>
        public readonly string Alias = "WorkflowSignalProcessingModes";

        /// <summary>
        ///     View caption for "WorkflowSignalProcessingModes".
        /// </summary>
        public readonly string Caption = "$Views_Names_WorkflowSignalProcessingModes";

        /// <summary>
        ///     View group for "WorkflowSignalProcessingModes".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SignalProcessingModeID.
        /// </summary>
        public readonly ViewObject ColumnSignalProcessingModeID = new ViewObject(0, "SignalProcessingModeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SignalProcessingModeName
        ///     Caption: $Views_WorkflowSignalProcessingModes_Name.
        /// </summary>
        public readonly ViewObject ColumnSignalProcessingModeName = new ViewObject(1, "SignalProcessingModeName", "$Views_WorkflowSignalProcessingModes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_WorkflowSignalProcessingModes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_WorkflowSignalProcessingModes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(WorkflowSignalProcessingModesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region WorkflowSignalTypes

    /// <summary>
    ///     ID: {3bc28139-54ad-45fe-9aa7-83119dc47b62}
    ///     Alias: WorkflowSignalTypes
    ///     Caption: $Views_Names_WorkflowSignalTypes
    ///     Group: WorkflowEngine
    /// </summary>
    public class WorkflowSignalTypesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "WorkflowSignalTypes": {3bc28139-54ad-45fe-9aa7-83119dc47b62}.
        /// </summary>
        public readonly Guid ID = new Guid(0x3bc28139,0x54ad,0x45fe,0x9a,0xa7,0x83,0x11,0x9d,0xc4,0x7b,0x62);

        /// <summary>
        ///     View name for "WorkflowSignalTypes".
        /// </summary>
        public readonly string Alias = "WorkflowSignalTypes";

        /// <summary>
        ///     View caption for "WorkflowSignalTypes".
        /// </summary>
        public readonly string Caption = "$Views_Names_WorkflowSignalTypes";

        /// <summary>
        ///     View group for "WorkflowSignalTypes".
        /// </summary>
        public readonly string Group = "WorkflowEngine";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: SignalTypeID.
        /// </summary>
        public readonly ViewObject ColumnSignalTypeID = new ViewObject(0, "SignalTypeID");

        /// <summary>
        ///     ID:1 
        ///     Alias: SignalTypeName
        ///     Caption: $Views_WorkflowSignalTypes_Name.
        /// </summary>
        public readonly ViewObject ColumnSignalTypeName = new ViewObject(1, "SignalTypeName", "$Views_WorkflowSignalTypes_Name");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_WorkflowSignalTypes_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_WorkflowSignalTypes_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(WorkflowSignalTypesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region Workplaces

    /// <summary>
    ///     ID: {36b9cf55-a385-4b3d-84d8-7d251702cc88}
    ///     Alias: Workplaces
    ///     Caption: $Views_Names_Workplaces
    ///     Group: System
    /// </summary>
    public class WorkplacesViewInfo
    {
        #region Common

        /// <summary>
        ///     View identifier for "Workplaces": {36b9cf55-a385-4b3d-84d8-7d251702cc88}.
        /// </summary>
        public readonly Guid ID = new Guid(0x36b9cf55,0xa385,0x4b3d,0x84,0xd8,0x7d,0x25,0x17,0x02,0xcc,0x88);

        /// <summary>
        ///     View name for "Workplaces".
        /// </summary>
        public readonly string Alias = "Workplaces";

        /// <summary>
        ///     View caption for "Workplaces".
        /// </summary>
        public readonly string Caption = "$Views_Names_Workplaces";

        /// <summary>
        ///     View group for "Workplaces".
        /// </summary>
        public readonly string Group = "System";

        #endregion

        #region Columns

        /// <summary>
        ///     ID:0 
        ///     Alias: WorkplaceID.
        /// </summary>
        public readonly ViewObject ColumnWorkplaceID = new ViewObject(0, "WorkplaceID");

        /// <summary>
        ///     ID:1 
        ///     Alias: WorkplaceName.
        /// </summary>
        public readonly ViewObject ColumnWorkplaceName = new ViewObject(1, "WorkplaceName");

        /// <summary>
        ///     ID:2 
        ///     Alias: WorkplaceLocalizedName
        ///     Caption: $Views_Workplaces_WorkplaceLocalizedName.
        /// </summary>
        public readonly ViewObject ColumnWorkplaceLocalizedName = new ViewObject(2, "WorkplaceLocalizedName", "$Views_Workplaces_WorkplaceLocalizedName");

        #endregion

        #region Parameters

        /// <summary>
        ///     ID:0 
        ///     Alias: Name
        ///     Caption: $Views_Workplaces_Name_Param.
        /// </summary>
        public readonly ViewObject ParamName = new ViewObject(0, "Name", "$Views_Workplaces_Name_Param");

        #endregion

        #region ToString

        public static implicit operator string(WorkplacesViewInfo obj) => obj.ToString();

        public override string ToString() => this.Alias;

        #endregion
    }

    #endregion

    #region RefSections

    public sealed class RefSectionsInfo
    {
        public readonly string AccessLevels = nameof(AccessLevels);
        public readonly string AclGenerationRules = nameof(AclGenerationRules);
        public readonly string Roles = nameof(Roles);
        public readonly string AclGenerationRuleExtensions = nameof(AclGenerationRuleExtensions);
        public readonly string DurableRoles = nameof(DurableRoles);
        public readonly string PersonalRoles = nameof(PersonalRoles);
        public readonly string AcquaintanceStates = nameof(AcquaintanceStates);
        public readonly string ActionHistoryVirtual = nameof(ActionHistoryVirtual);
        public readonly string Instances = nameof(Instances);
        public readonly string ActionHistoryTypes = nameof(ActionHistoryTypes);
        public readonly string ActionTypes = nameof(ActionTypes);
        public readonly string BusinessProcessInfo = nameof(BusinessProcessInfo);
        public readonly string ApplicationArchitectures = nameof(ApplicationArchitectures);
        public readonly string ApplicationNames = nameof(ApplicationNames);
        public readonly string Applications = nameof(Applications);
        public readonly string AvailableDeputyRoles = nameof(AvailableDeputyRoles);
        public readonly string AvailableDeputyUsers = nameof(AvailableDeputyUsers);
        public readonly string BarcodeTypes = nameof(BarcodeTypes);
        public readonly string CalendarCalcMethods = nameof(CalendarCalcMethods);
        public readonly string CalendarSettings = nameof(CalendarSettings);
        public readonly string CalendarTypes = nameof(CalendarTypes);
        public readonly string Tasks = nameof(Tasks);
        public readonly string Test_CarMainInfo = nameof(Test_CarMainInfo);
        public readonly string CompletionOptions = nameof(CompletionOptions);
        public readonly string Partitions = nameof(Partitions);
        public readonly string ConditionTypes = nameof(ConditionTypes);
        public readonly string ConditionUsePlaces = nameof(ConditionUsePlaces);
        public readonly string DocumentCommonInfo = nameof(DocumentCommonInfo);
        public readonly string DocRefsSection = nameof(DocRefsSection);
        public readonly string Partners = nameof(Partners);
        public readonly string Currencies = nameof(Currencies);
        public readonly string DateFormats = nameof(DateFormats);
        public readonly string Deleted = nameof(Deleted);
        public readonly string DepartmentRoles = nameof(DepartmentRoles);
        public readonly string RoleDeputiesManagementVirtual = nameof(RoleDeputiesManagementVirtual);
        public readonly string DeviceTypes = nameof(DeviceTypes);
        public readonly string DialogButtonTypes = nameof(DialogButtonTypes);
        public readonly string DialogCardAutoOpenModes = nameof(DialogCardAutoOpenModes);
        public readonly string DialogCardStoreModes = nameof(DialogCardStoreModes);
        public readonly string DocumentCategories = nameof(DocumentCategories);
        public readonly string DocumentTypes = nameof(DocumentTypes);
        public readonly string SignatureManagerVirtual = nameof(SignatureManagerVirtual);
        public readonly string tessa_Instances = nameof(tessa_Instances);
        public readonly string FileCategory = nameof(FileCategory);
        public readonly string FileConverterTypes = nameof(FileConverterTypes);
        public readonly string FileTemplates = nameof(FileTemplates);
        public readonly string FileTemplateTemplateTypes = nameof(FileTemplateTemplateTypes);
        public readonly string FileTemplateTypes = nameof(FileTemplateTypes);
        public readonly string FormatSettings = nameof(FormatSettings);
        public readonly string FunctionRoles = nameof(FunctionRoles);
        public readonly string HelpSections = nameof(HelpSections);
        public readonly string KrActionTypes = nameof(KrActionTypes);
        public readonly string KrCreateCardStageTypeModes = nameof(KrCreateCardStageTypeModes);
        public readonly string KrCycleGroupingModes = nameof(KrCycleGroupingModes);
        public readonly string DocNumberRegistrationAutoAssignment = nameof(DocNumberRegistrationAutoAssignment);
        public readonly string DocNumberRegularAutoAssignment = nameof(DocNumberRegularAutoAssignment);
        public readonly string KrDocState = nameof(KrDocState);
        public readonly string KrDocType = nameof(KrDocType);
        public readonly string KrForkManagementStageTypeModes = nameof(KrForkManagementStageTypeModes);
        public readonly string KrPermissionAclGenerationRules = nameof(KrPermissionAclGenerationRules);
        public readonly string KrPermissionFlags = nameof(KrPermissionFlags);
        public readonly string KrPermissionRoles = nameof(KrPermissionRoles);
        public readonly string KrPermissionRuleAccessSettings = nameof(KrPermissionRuleAccessSettings);
        public readonly string KrPermissions = nameof(KrPermissions);
        public readonly string KrPermissionsControlTypes = nameof(KrPermissionsControlTypes);
        public readonly string KrPermissionsFileCheckRules = nameof(KrPermissionsFileCheckRules);
        public readonly string KrPermissionsFileEditAccessSettings = nameof(KrPermissionsFileEditAccessSettings);
        public readonly string KrPermissionsFileReadAccessSettings = nameof(KrPermissionsFileReadAccessSettings);
        public readonly string KrPermissionsMandatoryValidationTypes = nameof(KrPermissionsMandatoryValidationTypes);
        public readonly string KrPermissionStates = nameof(KrPermissionStates);
        public readonly string KrPermissionTypes = nameof(KrPermissionTypes);
        public readonly string KrProcessManagementStageTypeModes = nameof(KrProcessManagementStageTypeModes);
        public readonly string KrRouteModes = nameof(KrRouteModes);
        public readonly string KrSecondaryProcess = nameof(KrSecondaryProcess);
        public readonly string KrSecondaryProcessModes = nameof(KrSecondaryProcessModes);
        public readonly string KrStageCommonMethods = nameof(KrStageCommonMethods);
        public readonly string KrStageGroups = nameof(KrStageGroups);
        public readonly string KrStages = nameof(KrStages);
        public readonly string KrStageTemplateGroupPosition = nameof(KrStageTemplateGroupPosition);
        public readonly string KrStageTemplates = nameof(KrStageTemplates);
        public readonly string KrProcessStageTypes = nameof(KrProcessStageTypes);
        public readonly string KrCardTypesVirtual = nameof(KrCardTypesVirtual);
        public readonly string TypeForView = nameof(TypeForView);
        public readonly string KrTypesForDialogs = nameof(KrTypesForDialogs);
        public readonly string KrTypesForPermissionsExtension = nameof(KrTypesForPermissionsExtension);
        public readonly string KrVirtualFiles = nameof(KrVirtualFiles);
        public readonly string Languages = nameof(Languages);
        public readonly string LawCategories = nameof(LawCategories);
        public readonly string LawClassificationPlans = nameof(LawClassificationPlans);
        public readonly string LawClients = nameof(LawClients);
        public readonly string LawDocKinds = nameof(LawDocKinds);
        public readonly string LawDocTypes = nameof(LawDocTypes);
        public readonly string LawEntityKinds = nameof(LawEntityKinds);
        public readonly string LawFileStorages = nameof(LawFileStorages);
        public readonly string LawPartnerRepresentatives = nameof(LawPartnerRepresentatives);
        public readonly string LawPartners = nameof(LawPartners);
        public readonly string LawStoreLocations = nameof(LawStoreLocations);
        public readonly string LawUsers = nameof(LawUsers);
        public readonly string LicenseTypes = nameof(LicenseTypes);
        public readonly string LoginTypes = nameof(LoginTypes);
        public readonly string Tags = nameof(Tags);
        public readonly string Notifications = nameof(Notifications);
        public readonly string NotificationSubscriptions = nameof(NotificationSubscriptions);
        public readonly string NotificationTypes = nameof(NotificationTypes);
        public readonly string OcrLanguages = nameof(OcrLanguages);
        public readonly string OcrOperations = nameof(OcrOperations);
        public readonly string OcrPatternTypes = nameof(OcrPatternTypes);
        public readonly string OcrRecognitionModes = nameof(OcrRecognitionModes);
        public readonly string OcrRequests = nameof(OcrRequests);
        public readonly string OcrSegmentationModes = nameof(OcrSegmentationModes);
        public readonly string Operations = nameof(Operations);
        public readonly string PartnersContacts = nameof(PartnersContacts);
        public readonly string PaCo = nameof(PaCo);
        public readonly string PartnersTypes = nameof(PartnersTypes);
        public readonly string ReportRolesRules = nameof(ReportRolesRules);
        public readonly string Users = nameof(Users);
        public readonly string RoleGenerators = nameof(RoleGenerators);
        public readonly string RoleTypes = nameof(RoleTypes);
        public readonly string SequencesInfo = nameof(SequencesInfo);
        public readonly string Sessions = nameof(Sessions);
        public readonly string SessionServiceTypes = nameof(SessionServiceTypes);
        public readonly string SignatureDigestAlgorithms = nameof(SignatureDigestAlgorithms);
        public readonly string SignatureEncryptionAlgorithms = nameof(SignatureEncryptionAlgorithms);
        public readonly string SignaturePackagings = nameof(SignaturePackagings);
        public readonly string SignatureProfiles = nameof(SignatureProfiles);
        public readonly string SignatureTypes = nameof(SignatureTypes);
        public readonly string SmartRoleGenerators = nameof(SmartRoleGenerators);
        public readonly string TaskAssignedRoles = nameof(TaskAssignedRoles);
        public readonly string TaskHistoryGroupTypes = nameof(TaskHistoryGroupTypes);
        public readonly string TaskKinds = nameof(TaskKinds);
        public readonly string TaskStates = nameof(TaskStates);
        public readonly string TaskTypes = nameof(TaskTypes);
        public readonly string Templates = nameof(Templates);
        public readonly string TileSizes = nameof(TileSizes);
        public readonly string TimeZones = nameof(TimeZones);
        public readonly string Types = nameof(Types);
        public readonly string VatTypes = nameof(VatTypes);
        public readonly string Views = nameof(Views);
        public readonly string WebApplications = nameof(WebApplications);
        public readonly string WeTaskControlTypes = nameof(WeTaskControlTypes);
        public readonly string WeTaskGroupActionOptionTypes = nameof(WeTaskGroupActionOptionTypes);
        public readonly string WfResolutionAuthors = nameof(WfResolutionAuthors);
        public readonly string WorkflowEngineCompiledBaseTypes = nameof(WorkflowEngineCompiledBaseTypes);
        public readonly string WorkflowEngineLogLevels = nameof(WorkflowEngineLogLevels);
        public readonly string WorkflowEngineTaskActions = nameof(WorkflowEngineTaskActions);
        public readonly string WorkflowEngineTileManagerExtensions = nameof(WorkflowEngineTileManagerExtensions);
        public readonly string WorkflowLinkModes = nameof(WorkflowLinkModes);
        public readonly string WorkflowSignalProcessingModes = nameof(WorkflowSignalProcessingModes);
        public readonly string WorkflowSignalTypes = nameof(WorkflowSignalTypes);
        public readonly string Workplaces = nameof(Workplaces);
    }

    #endregion

    public static class ViewInfo
    {
        #region Workplaces

        public static readonly AccessLevelsViewInfo AccessLevels = new AccessLevelsViewInfo();
        public static readonly AclForCardViewInfo AclForCard = new AclForCardViewInfo();
        public static readonly AclGenerationRuleExtensionsViewInfo AclGenerationRuleExtensions = new AclGenerationRuleExtensionsViewInfo();
        public static readonly AclGenerationRulesViewInfo AclGenerationRules = new AclGenerationRulesViewInfo();
        public static readonly AcquaintanceHistoryViewInfo AcquaintanceHistory = new AcquaintanceHistoryViewInfo();
        public static readonly AcquaintanceStatesViewInfo AcquaintanceStates = new AcquaintanceStatesViewInfo();
        public static readonly ActionHistoryViewInfo ActionHistory = new ActionHistoryViewInfo();
        public static readonly ActionHistoryTypesViewInfo ActionHistoryTypes = new ActionHistoryTypesViewInfo();
        public static readonly ActionTypesViewInfo ActionTypes = new ActionTypesViewInfo();
        public static readonly ActiveWorkflowsViewInfo ActiveWorkflows = new ActiveWorkflowsViewInfo();
        public static readonly ApplicationArchitecturesViewInfo ApplicationArchitectures = new ApplicationArchitecturesViewInfo();
        public static readonly ApplicationNamesViewInfo ApplicationNames = new ApplicationNamesViewInfo();
        public static readonly ApplicationsViewInfo Applications = new ApplicationsViewInfo();
        public static readonly AvailableApplicationsViewInfo AvailableApplications = new AvailableApplicationsViewInfo();
        public static readonly AvailableDeputyRolesViewInfo AvailableDeputyRoles = new AvailableDeputyRolesViewInfo();
        public static readonly AvailableDeputyUsersViewInfo AvailableDeputyUsers = new AvailableDeputyUsersViewInfo();
        public static readonly BarcodeTypesViewInfo BarcodeTypes = new BarcodeTypesViewInfo();
        public static readonly BusinessProcessTemplatesViewInfo BusinessProcessTemplates = new BusinessProcessTemplatesViewInfo();
        public static readonly CalendarCalcMethodsViewInfo CalendarCalcMethods = new CalendarCalcMethodsViewInfo();
        public static readonly CalendarsViewInfo Calendars = new CalendarsViewInfo();
        public static readonly CalendarTypesViewInfo CalendarTypes = new CalendarTypesViewInfo();
        public static readonly CardTasksViewInfo CardTasks = new CardTasksViewInfo();
        public static readonly CardTaskSessionRolesViewInfo CardTaskSessionRoles = new CardTaskSessionRolesViewInfo();
        public static readonly CarsViewInfo Cars = new CarsViewInfo();
        public static readonly CompletedTasksViewInfo CompletedTasks = new CompletedTasksViewInfo();
        public static readonly CompletionOptionCardsViewInfo CompletionOptionCards = new CompletionOptionCardsViewInfo();
        public static readonly CompletionOptionsViewInfo CompletionOptions = new CompletionOptionsViewInfo();
        public static readonly ConditionTypesViewInfo ConditionTypes = new ConditionTypesViewInfo();
        public static readonly ConditionUsePlacesViewInfo ConditionUsePlaces = new ConditionUsePlacesViewInfo();
        public static readonly ContractsDocumentsViewInfo ContractsDocuments = new ContractsDocumentsViewInfo();
        public static readonly CreateFileFromTemplateViewInfo CreateFileFromTemplate = new CreateFileFromTemplateViewInfo();
        public static readonly CurrenciesViewInfo Currencies = new CurrenciesViewInfo();
        public static readonly DateFormatsViewInfo DateFormats = new DateFormatsViewInfo();
        public static readonly DeletedViewInfo Deleted = new DeletedViewInfo();
        public static readonly DepartmentsViewInfo Departments = new DepartmentsViewInfo();
        public static readonly DeputiesManagementViewInfo DeputiesManagement = new DeputiesManagementViewInfo();
        public static readonly DeviceTypesViewInfo DeviceTypes = new DeviceTypesViewInfo();
        public static readonly DialogButtonTypesViewInfo DialogButtonTypes = new DialogButtonTypesViewInfo();
        public static readonly DialogCardAutoOpenModesViewInfo DialogCardAutoOpenModes = new DialogCardAutoOpenModesViewInfo();
        public static readonly DialogCardStoreModesViewInfo DialogCardStoreModes = new DialogCardStoreModesViewInfo();
        public static readonly DocumentCategoriesViewInfo DocumentCategories = new DocumentCategoriesViewInfo();
        public static readonly DocumentsViewInfo Documents = new DocumentsViewInfo();
        public static readonly DocumentTypesViewInfo DocumentTypes = new DocumentTypesViewInfo();
        public static readonly DurableRolesViewInfo DurableRoles = new DurableRolesViewInfo();
        public static readonly EdsManagersViewInfo EdsManagers = new EdsManagersViewInfo();
        public static readonly EmittedTasksViewInfo EmittedTasks = new EmittedTasksViewInfo();
        public static readonly ErrorsViewInfo Errors = new ErrorsViewInfo();
        public static readonly ErrorWorkflowsViewInfo ErrorWorkflows = new ErrorWorkflowsViewInfo();
        public static readonly FileCategoriesAllViewInfo FileCategoriesAll = new FileCategoriesAllViewInfo();
        public static readonly FileCategoriesFilteredViewInfo FileCategoriesFiltered = new FileCategoriesFilteredViewInfo();
        public static readonly FileConverterTypesViewInfo FileConverterTypes = new FileConverterTypesViewInfo();
        public static readonly FileTemplatesViewInfo FileTemplates = new FileTemplatesViewInfo();
        public static readonly FileTemplateTemplateTypesViewInfo FileTemplateTemplateTypes = new FileTemplateTemplateTypesViewInfo();
        public static readonly FileTemplateTypesViewInfo FileTemplateTypes = new FileTemplateTypesViewInfo();
        public static readonly FormatSettingsViewInfo FormatSettings = new FormatSettingsViewInfo();
        public static readonly FunctionRoleCardsViewInfo FunctionRoleCards = new FunctionRoleCardsViewInfo();
        public static readonly GetCardIDViewViewInfo GetCardIDView = new GetCardIDViewViewInfo();
        public static readonly GetFileNameViewViewInfo GetFileNameView = new GetFileNameViewViewInfo();
        public static readonly GroupsViewInfo Groups = new GroupsViewInfo();
        public static readonly GroupsWithHierarchyViewInfo GroupsWithHierarchy = new GroupsWithHierarchyViewInfo();
        public static readonly HelpSectionsViewInfo HelpSections = new HelpSectionsViewInfo();
        public static readonly HierarchyViewInfo Hierarchy = new HierarchyViewInfo();
        public static readonly IncomingDocumentsViewInfo IncomingDocuments = new IncomingDocumentsViewInfo();
        public static readonly KrActionTypesViewInfo KrActionTypes = new KrActionTypesViewInfo();
        public static readonly KrCreateCardStageTypeModesViewInfo KrCreateCardStageTypeModes = new KrCreateCardStageTypeModesViewInfo();
        public static readonly KrCycleGroupingModesViewInfo KrCycleGroupingModes = new KrCycleGroupingModesViewInfo();
        public static readonly KrDocNumberRegistrationAutoAssigmentViewInfo KrDocNumberRegistrationAutoAssigment = new KrDocNumberRegistrationAutoAssigmentViewInfo();
        public static readonly KrDocNumberRegularAutoAssigmentViewInfo KrDocNumberRegularAutoAssigment = new KrDocNumberRegularAutoAssigmentViewInfo();
        public static readonly KrDocStateCardsViewInfo KrDocStateCards = new KrDocStateCardsViewInfo();
        public static readonly KrDocStatesViewInfo KrDocStates = new KrDocStatesViewInfo();
        public static readonly KrDocTypesViewInfo KrDocTypes = new KrDocTypesViewInfo();
        public static readonly KrFilteredStageGroupsViewInfo KrFilteredStageGroups = new KrFilteredStageGroupsViewInfo();
        public static readonly KrFilteredStageTypesViewInfo KrFilteredStageTypes = new KrFilteredStageTypesViewInfo();
        public static readonly KrForkManagementStageTypeModesViewInfo KrForkManagementStageTypeModes = new KrForkManagementStageTypeModesViewInfo();
        public static readonly KrManagerTasksViewInfo KrManagerTasks = new KrManagerTasksViewInfo();
        public static readonly KrPermissionAclGenerationRulesViewInfo KrPermissionAclGenerationRules = new KrPermissionAclGenerationRulesViewInfo();
        public static readonly KrPermissionFlagsViewInfo KrPermissionFlags = new KrPermissionFlagsViewInfo();
        public static readonly KrPermissionRolesViewInfo KrPermissionRoles = new KrPermissionRolesViewInfo();
        public static readonly KrPermissionRuleAccessSettingsViewInfo KrPermissionRuleAccessSettings = new KrPermissionRuleAccessSettingsViewInfo();
        public static readonly KrPermissionsViewInfo KrPermissions = new KrPermissionsViewInfo();
        public static readonly KrPermissionsControlTypesViewInfo KrPermissionsControlTypes = new KrPermissionsControlTypesViewInfo();
        public static readonly KrPermissionsExtendedCardsViewInfo KrPermissionsExtendedCards = new KrPermissionsExtendedCardsViewInfo();
        public static readonly KrPermissionsExtendedFilesViewInfo KrPermissionsExtendedFiles = new KrPermissionsExtendedFilesViewInfo();
        public static readonly KrPermissionsExtendedMandatoryViewInfo KrPermissionsExtendedMandatory = new KrPermissionsExtendedMandatoryViewInfo();
        public static readonly KrPermissionsExtendedTasksViewInfo KrPermissionsExtendedTasks = new KrPermissionsExtendedTasksViewInfo();
        public static readonly KrPermissionsExtendedVisibilityViewInfo KrPermissionsExtendedVisibility = new KrPermissionsExtendedVisibilityViewInfo();
        public static readonly KrPermissionsFileCheckRulesViewInfo KrPermissionsFileCheckRules = new KrPermissionsFileCheckRulesViewInfo();
        public static readonly KrPermissionsFileEditAccessSettingsViewInfo KrPermissionsFileEditAccessSettings = new KrPermissionsFileEditAccessSettingsViewInfo();
        public static readonly KrPermissionsFileReadAccessSettingsViewInfo KrPermissionsFileReadAccessSettings = new KrPermissionsFileReadAccessSettingsViewInfo();
        public static readonly KrPermissionsMandatoryValidationTypesViewInfo KrPermissionsMandatoryValidationTypes = new KrPermissionsMandatoryValidationTypesViewInfo();
        public static readonly KrPermissionsReportViewInfo KrPermissionsReport = new KrPermissionsReportViewInfo();
        public static readonly KrPermissionStatesViewInfo KrPermissionStates = new KrPermissionStatesViewInfo();
        public static readonly KrPermissionTypesViewInfo KrPermissionTypes = new KrPermissionTypesViewInfo();
        public static readonly KrProcessManagementStageTypeModesViewInfo KrProcessManagementStageTypeModes = new KrProcessManagementStageTypeModesViewInfo();
        public static readonly KrRouteModesViewInfo KrRouteModes = new KrRouteModesViewInfo();
        public static readonly KrSecondaryProcessesViewInfo KrSecondaryProcesses = new KrSecondaryProcessesViewInfo();
        public static readonly KrSecondaryProcessModesViewInfo KrSecondaryProcessModes = new KrSecondaryProcessModesViewInfo();
        public static readonly KrStageCommonMethodsViewInfo KrStageCommonMethods = new KrStageCommonMethodsViewInfo();
        public static readonly KrStageGroupsViewInfo KrStageGroups = new KrStageGroupsViewInfo();
        public static readonly KrStageRowsViewInfo KrStageRows = new KrStageRowsViewInfo();
        public static readonly KrStageTemplateGroupPositionViewInfo KrStageTemplateGroupPosition = new KrStageTemplateGroupPositionViewInfo();
        public static readonly KrStageTemplatesViewInfo KrStageTemplates = new KrStageTemplatesViewInfo();
        public static readonly KrStageTypesViewInfo KrStageTypes = new KrStageTypesViewInfo();
        public static readonly KrTypesViewInfo KrTypes = new KrTypesViewInfo();
        public static readonly KrTypesEffectiveViewInfo KrTypesEffective = new KrTypesEffectiveViewInfo();
        public static readonly KrTypesForDialogsViewInfo KrTypesForDialogs = new KrTypesForDialogsViewInfo();
        public static readonly KrTypesForPermissionsExtensionViewInfo KrTypesForPermissionsExtension = new KrTypesForPermissionsExtensionViewInfo();
        public static readonly KrVirtualFilesViewInfo KrVirtualFiles = new KrVirtualFilesViewInfo();
        public static readonly LanguagesViewInfo Languages = new LanguagesViewInfo();
        public static readonly LastTopicsViewInfo LastTopics = new LastTopicsViewInfo();
        public static readonly LawCasesViewInfo LawCases = new LawCasesViewInfo();
        public static readonly LawCategoriesViewInfo LawCategories = new LawCategoriesViewInfo();
        public static readonly LawClassificationPlansViewInfo LawClassificationPlans = new LawClassificationPlansViewInfo();
        public static readonly LawClientsViewInfo LawClients = new LawClientsViewInfo();
        public static readonly LawDocKindsViewInfo LawDocKinds = new LawDocKindsViewInfo();
        public static readonly LawDocTypesViewInfo LawDocTypes = new LawDocTypesViewInfo();
        public static readonly LawEntityKindsViewInfo LawEntityKinds = new LawEntityKindsViewInfo();
        public static readonly LawFileStoragesViewInfo LawFileStorages = new LawFileStoragesViewInfo();
        public static readonly LawFoldersViewInfo LawFolders = new LawFoldersViewInfo();
        public static readonly LawPartnerRepresentativesViewInfo LawPartnerRepresentatives = new LawPartnerRepresentativesViewInfo();
        public static readonly LawPartnersViewInfo LawPartners = new LawPartnersViewInfo();
        public static readonly LawStoreLocationsViewInfo LawStoreLocations = new LawStoreLocationsViewInfo();
        public static readonly LawUsersViewInfo LawUsers = new LawUsersViewInfo();
        public static readonly LicenseTypesViewInfo LicenseTypes = new LicenseTypesViewInfo();
        public static readonly LinkedDocumentsViewInfo LinkedDocuments = new LinkedDocumentsViewInfo();
        public static readonly LoginTypesViewInfo LoginTypes = new LoginTypesViewInfo();
        public static readonly MyAcquaintanceHistoryViewInfo MyAcquaintanceHistory = new MyAcquaintanceHistoryViewInfo();
        public static readonly MyCompletedTasksViewInfo MyCompletedTasks = new MyCompletedTasksViewInfo();
        public static readonly MyDocumentsViewInfo MyDocuments = new MyDocumentsViewInfo();
        public static readonly MyTagsViewInfo MyTags = new MyTagsViewInfo();
        public static readonly MyTasksViewInfo MyTasks = new MyTasksViewInfo();
        public static readonly MyTopicsViewInfo MyTopics = new MyTopicsViewInfo();
        public static readonly NotificationsViewInfo Notifications = new NotificationsViewInfo();
        public static readonly NotificationSubscriptionsViewInfo NotificationSubscriptions = new NotificationSubscriptionsViewInfo();
        public static readonly NotificationTypesViewInfo NotificationTypes = new NotificationTypesViewInfo();
        public static readonly OcrLanguagesViewInfo OcrLanguages = new OcrLanguagesViewInfo();
        public static readonly OcrOperationsViewInfo OcrOperations = new OcrOperationsViewInfo();
        public static readonly OcrPatternTypesViewInfo OcrPatternTypes = new OcrPatternTypesViewInfo();
        public static readonly OcrRecognitionModesViewInfo OcrRecognitionModes = new OcrRecognitionModesViewInfo();
        public static readonly OcrRequestsViewInfo OcrRequests = new OcrRequestsViewInfo();
        public static readonly OcrSegmentationModesViewInfo OcrSegmentationModes = new OcrSegmentationModesViewInfo();
        public static readonly OperationsViewInfo Operations = new OperationsViewInfo();
        public static readonly OutgoingDocumentsViewInfo OutgoingDocuments = new OutgoingDocumentsViewInfo();
        public static readonly PartitionsViewInfo Partitions = new PartitionsViewInfo();
        public static readonly PartnersViewInfo Partners = new PartnersViewInfo();
        public static readonly PartnersContactsViewInfo PartnersContacts = new PartnersContactsViewInfo();
        public static readonly PartnersTypesViewInfo PartnersTypes = new PartnersTypesViewInfo();
        public static readonly ProtocolCompletedTasksViewInfo ProtocolCompletedTasks = new ProtocolCompletedTasksViewInfo();
        public static readonly ProtocolReportsWithPhotoViewInfo ProtocolReportsWithPhoto = new ProtocolReportsWithPhotoViewInfo();
        public static readonly ProtocolsViewInfo Protocols = new ProtocolsViewInfo();
        public static readonly RefDocumentsLookupViewInfo RefDocumentsLookup = new RefDocumentsLookupViewInfo();
        public static readonly ReportCurrentTasksByDepartmentViewInfo ReportCurrentTasksByDepartment = new ReportCurrentTasksByDepartmentViewInfo();
        public static readonly ReportCurrentTasksByDepUnpivotedViewInfo ReportCurrentTasksByDepUnpivoted = new ReportCurrentTasksByDepUnpivotedViewInfo();
        public static readonly ReportCurrentTasksByUserViewInfo ReportCurrentTasksByUser = new ReportCurrentTasksByUserViewInfo();
        public static readonly ReportCurrentTasksRulesViewInfo ReportCurrentTasksRules = new ReportCurrentTasksRulesViewInfo();
        public static readonly ReportDocumentsByTypeViewInfo ReportDocumentsByType = new ReportDocumentsByTypeViewInfo();
        public static readonly ReportPastTasksByDepartmentViewInfo ReportPastTasksByDepartment = new ReportPastTasksByDepartmentViewInfo();
        public static readonly ReportPastTasksByUserViewInfo ReportPastTasksByUser = new ReportPastTasksByUserViewInfo();
        public static readonly RoleDeputiesViewInfo RoleDeputies = new RoleDeputiesViewInfo();
        public static readonly RoleDeputiesManagementDeputizedViewInfo RoleDeputiesManagementDeputized = new RoleDeputiesManagementDeputizedViewInfo();
        public static readonly RoleDeputiesNewViewInfo RoleDeputiesNew = new RoleDeputiesNewViewInfo();
        public static readonly RoleGeneratorsViewInfo RoleGenerators = new RoleGeneratorsViewInfo();
        public static readonly RolesViewInfo Roles = new RolesViewInfo();
        public static readonly RoleTypesViewInfo RoleTypes = new RoleTypesViewInfo();
        public static readonly SequencesViewInfo Sequences = new SequencesViewInfo();
        public static readonly SessionsViewInfo Sessions = new SessionsViewInfo();
        public static readonly SessionServiceTypesViewInfo SessionServiceTypes = new SessionServiceTypesViewInfo();
        public static readonly SignatureDigestAlgosViewInfo SignatureDigestAlgos = new SignatureDigestAlgosViewInfo();
        public static readonly SignatureEncryptionAlgosViewInfo SignatureEncryptionAlgos = new SignatureEncryptionAlgosViewInfo();
        public static readonly SignaturePackagingsViewInfo SignaturePackagings = new SignaturePackagingsViewInfo();
        public static readonly SignatureProfilesViewInfo SignatureProfiles = new SignatureProfilesViewInfo();
        public static readonly SignatureTypesViewInfo SignatureTypes = new SignatureTypesViewInfo();
        public static readonly SmartRoleGeneratorsViewInfo SmartRoleGenerators = new SmartRoleGeneratorsViewInfo();
        public static readonly TagCardsViewInfo TagCards = new TagCardsViewInfo();
        public static readonly TagsViewInfo Tags = new TagsViewInfo();
        public static readonly TaskAssignedRolesViewInfo TaskAssignedRoles = new TaskAssignedRolesViewInfo();
        public static readonly TaskAssignedRoleUsersViewInfo TaskAssignedRoleUsers = new TaskAssignedRoleUsersViewInfo();
        public static readonly TaskFunctionRolesViewInfo TaskFunctionRoles = new TaskFunctionRolesViewInfo();
        public static readonly TaskHistoryViewInfo TaskHistory = new TaskHistoryViewInfo();
        public static readonly TaskHistoryGroupTypesViewInfo TaskHistoryGroupTypes = new TaskHistoryGroupTypesViewInfo();
        public static readonly TaskKindsViewInfo TaskKinds = new TaskKindsViewInfo();
        public static readonly TaskStatesViewInfo TaskStates = new TaskStatesViewInfo();
        public static readonly TaskTypesViewInfo TaskTypes = new TaskTypesViewInfo();
        public static readonly TemplatesViewInfo Templates = new TemplatesViewInfo();
        public static readonly TileSizesViewInfo TileSizes = new TileSizesViewInfo();
        public static readonly TimeZonesViewInfo TimeZones = new TimeZonesViewInfo();
        public static readonly TopicParticipantsViewInfo TopicParticipants = new TopicParticipantsViewInfo();
        public static readonly TypesViewInfo Types = new TypesViewInfo();
        public static readonly UsersViewInfo Users = new UsersViewInfo();
        public static readonly VatTypesViewInfo VatTypes = new VatTypesViewInfo();
        public static readonly ViewFilesViewInfo ViewFiles = new ViewFilesViewInfo();
        public static readonly ViewsViewInfo Views = new ViewsViewInfo();
        public static readonly WebApplicationsViewInfo WebApplications = new WebApplicationsViewInfo();
        public static readonly WeTaskControlTypesViewInfo WeTaskControlTypes = new WeTaskControlTypesViewInfo();
        public static readonly WeTaskGroupActionOptionTypesViewInfo WeTaskGroupActionOptionTypes = new WeTaskGroupActionOptionTypesViewInfo();
        public static readonly WfResolutionAuthorsViewInfo WfResolutionAuthors = new WfResolutionAuthorsViewInfo();
        public static readonly WorkflowEngineCompiledBaseTypesViewInfo WorkflowEngineCompiledBaseTypes = new WorkflowEngineCompiledBaseTypesViewInfo();
        public static readonly WorkflowEngineErrorsViewInfo WorkflowEngineErrors = new WorkflowEngineErrorsViewInfo();
        public static readonly WorkflowEngineLogLevelsViewInfo WorkflowEngineLogLevels = new WorkflowEngineLogLevelsViewInfo();
        public static readonly WorkflowEngineLogsViewInfo WorkflowEngineLogs = new WorkflowEngineLogsViewInfo();
        public static readonly WorkflowEngineTaskActionsViewInfo WorkflowEngineTaskActions = new WorkflowEngineTaskActionsViewInfo();
        public static readonly WorkflowEngineTileManagerExtensionsViewInfo WorkflowEngineTileManagerExtensions = new WorkflowEngineTileManagerExtensionsViewInfo();
        public static readonly WorkflowLinkModesViewInfo WorkflowLinkModes = new WorkflowLinkModesViewInfo();
        public static readonly WorkflowNodeInstanceSubprocessesViewInfo WorkflowNodeInstanceSubprocesses = new WorkflowNodeInstanceSubprocessesViewInfo();
        public static readonly WorkflowNodeInstanceTasksViewInfo WorkflowNodeInstanceTasks = new WorkflowNodeInstanceTasksViewInfo();
        public static readonly WorkflowSignalProcessingModesViewInfo WorkflowSignalProcessingModes = new WorkflowSignalProcessingModesViewInfo();
        public static readonly WorkflowSignalTypesViewInfo WorkflowSignalTypes = new WorkflowSignalTypesViewInfo();
        public static readonly WorkplacesViewInfo Workplaces = new WorkplacesViewInfo();

        #endregion

        #region RefSections

        public static readonly RefSectionsInfo RefSections = new RefSectionsInfo();

        #endregion
    }
}