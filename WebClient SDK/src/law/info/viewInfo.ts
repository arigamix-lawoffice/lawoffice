// noinspection JSUnusedGlobalSymbols,SpellCheckingInspection

//#region ViewObject

class ViewObject {
  constructor (id: number, alias: string, caption: string | null = null) {
    this.id = id;
    this.alias = alias;
    this.caption = caption ?? null;
  }

  private id: number;
  private alias: string;
  private caption: string | null;

  public get Id(): number { return this.id; }
  public get Alias(): string { return this.alias; }
  public get Caption(): string | null { return this.caption; }
}

//#endregion

//#region AccessLevels

/**
 * ID: {55fe2b54-d61b-4b02-9737-3619cfbfd962}
 * Alias: AccessLevels
 * Caption: $Views_Names_AccessLevels
 * Group: System
 */
class AccessLevelsViewInfo {
  //#region Common

  /**
   * View identifier for "AccessLevels": {55fe2b54-d61b-4b02-9737-3619cfbfd962}.
   */
   readonly ID: guid = '55fe2b54-d61b-4b02-9737-3619cfbfd962';

  /**
   * View name for "AccessLevels".
   */
   readonly Alias: string = 'AccessLevels';

  /**
   * View caption for "AccessLevels".
   */
   readonly Caption: string = '$Views_Names_AccessLevels';

  /**
   * View group for "AccessLevels".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: $Views_AccessLevels_Name.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', '$Views_AccessLevels_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_AccessLevels_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_AccessLevels_Name_Param');

  //#endregion
}

//#endregion

//#region AclForCard

/**
 * ID: {4e53c550-1954-457a-95a6-4b23c3452fb4}
 * Alias: AclForCard
 * Caption: $Views_Names_AclForCard
 * Group: Acl
 */
class AclForCardViewInfo {
  //#region Common

  /**
   * View identifier for "AclForCard": {4e53c550-1954-457a-95a6-4b23c3452fb4}.
   */
   readonly ID: guid = '4e53c550-1954-457a-95a6-4b23c3452fb4';

  /**
   * View name for "AclForCard".
   */
   readonly Alias: string = 'AclForCard';

  /**
   * View caption for "AclForCard".
   */
   readonly Caption: string = '$Views_Names_AclForCard';

  /**
   * View group for "AclForCard".
   */
   readonly Group: string = 'Acl';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RoleID.
   */
   readonly ColumnRoleID: ViewObject = new ViewObject(0, 'RoleID');

  /**
   * ID:1
   * Alias: RoleName
   * Caption: $Views_AclForCard_RoleName.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(1, 'RoleName', '$Views_AclForCard_RoleName');

  /**
   * ID:2
   * Alias: RoleType
   * Caption: $Views_AclForCard_RoleType.
   */
   readonly ColumnRoleType: ViewObject = new ViewObject(2, 'RoleType', '$Views_AclForCard_RoleType');

  /**
   * ID:3
   * Alias: RuleID.
   */
   readonly ColumnRuleID: ViewObject = new ViewObject(3, 'RuleID');

  /**
   * ID:4
   * Alias: RuleName
   * Caption: $Views_AclForCard_RuleName.
   */
   readonly ColumnRuleName: ViewObject = new ViewObject(4, 'RuleName', '$Views_AclForCard_RuleName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CardID
   * Caption: $Views_AclForCard_CardID.
   */
   readonly ParamCardID: ViewObject = new ViewObject(0, 'CardID', '$Views_AclForCard_CardID');

  //#endregion
}

//#endregion

//#region AclGenerationRuleExtensions

/**
 * ID: {480df66f-bb9f-4a16-b26c-d1f358c80a4a}
 * Alias: AclGenerationRuleExtensions
 * Caption: $Views_Names_AclGenerationRuleExtensions
 * Group: Acl
 */
class AclGenerationRuleExtensionsViewInfo {
  //#region Common

  /**
   * View identifier for "AclGenerationRuleExtensions": {480df66f-bb9f-4a16-b26c-d1f358c80a4a}.
   */
   readonly ID: guid = '480df66f-bb9f-4a16-b26c-d1f358c80a4a';

  /**
   * View name for "AclGenerationRuleExtensions".
   */
   readonly Alias: string = 'AclGenerationRuleExtensions';

  /**
   * View caption for "AclGenerationRuleExtensions".
   */
   readonly Caption: string = '$Views_Names_AclGenerationRuleExtensions';

  /**
   * View group for "AclGenerationRuleExtensions".
   */
   readonly Group: string = 'Acl';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ExtensionID.
   */
   readonly ColumnExtensionID: ViewObject = new ViewObject(0, 'ExtensionID');

  /**
   * ID:1
   * Alias: ExtensionName
   * Caption: $Views_AclGenerationRuleExtensions_Name.
   */
   readonly ColumnExtensionName: ViewObject = new ViewObject(1, 'ExtensionName', '$Views_AclGenerationRuleExtensions_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_AclGenerationRuleExtensions_Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_AclGenerationRuleExtensions_Name');

  //#endregion
}

//#endregion

//#region AclGenerationRules

/**
 * ID: {edf05d46-215d-4c33-826a-568e626f60c6}
 * Alias: AclGenerationRules
 * Caption: $Views_Names_AclGenerationRules
 * Group: Acl
 */
class AclGenerationRulesViewInfo {
  //#region Common

  /**
   * View identifier for "AclGenerationRules": {edf05d46-215d-4c33-826a-568e626f60c6}.
   */
   readonly ID: guid = 'edf05d46-215d-4c33-826a-568e626f60c6';

  /**
   * View name for "AclGenerationRules".
   */
   readonly Alias: string = 'AclGenerationRules';

  /**
   * View caption for "AclGenerationRules".
   */
   readonly Caption: string = '$Views_Names_AclGenerationRules';

  /**
   * View group for "AclGenerationRules".
   */
   readonly Group: string = 'Acl';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: AclGenerationRuleID.
   */
   readonly ColumnAclGenerationRuleID: ViewObject = new ViewObject(0, 'AclGenerationRuleID');

  /**
   * ID:1
   * Alias: AclGenerationRuleName
   * Caption: $Views_AclGenerationRules_Name.
   */
   readonly ColumnAclGenerationRuleName: ViewObject = new ViewObject(1, 'AclGenerationRuleName', '$Views_AclGenerationRules_Name');

  /**
   * ID:2
   * Alias: AclGenerationRuleTypes
   * Caption: $Views_AclGenerationRules_Types.
   */
   readonly ColumnAclGenerationRuleTypes: ViewObject = new ViewObject(2, 'AclGenerationRuleTypes', '$Views_AclGenerationRules_Types');

  /**
   * ID:3
   * Alias: AclGenerationRuleUseSmartRoles
   * Caption: $Views_AclGenerationRules_UseSmartRoles.
   */
   readonly ColumnAclGenerationRuleUseSmartRoles: ViewObject = new ViewObject(3, 'AclGenerationRuleUseSmartRoles', '$Views_AclGenerationRules_UseSmartRoles');

  /**
   * ID:4
   * Alias: AclGenerationRuleSmartRoleGenerator
   * Caption: $Views_AclGenerationRules_SmartRoleGenerator.
   */
   readonly ColumnAclGenerationRuleSmartRoleGenerator: ViewObject = new ViewObject(4, 'AclGenerationRuleSmartRoleGenerator', '$Views_AclGenerationRules_SmartRoleGenerator');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_AclGenerationRules_Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_AclGenerationRules_Name');

  /**
   * ID:1
   * Alias: CardType
   * Caption: $Views_AclGenerationRules_CardType.
   */
   readonly ParamCardType: ViewObject = new ViewObject(1, 'CardType', '$Views_AclGenerationRules_CardType');

  /**
   * ID:2
   * Alias: UseSmartRoles
   * Caption: $Views_AclGenerationRules_UseSmartRoles.
   */
   readonly ParamUseSmartRoles: ViewObject = new ViewObject(2, 'UseSmartRoles', '$Views_AclGenerationRules_UseSmartRoles');

  /**
   * ID:3
   * Alias: SmartRoleGenerator
   * Caption: $Views_AclGenerationRules_SmartRoleGenerator.
   */
   readonly ParamSmartRoleGenerator: ViewObject = new ViewObject(3, 'SmartRoleGenerator', '$Views_AclGenerationRules_SmartRoleGenerator');

  //#endregion
}

//#endregion

//#region AcquaintanceHistory

/**
 * ID: {6fe5e33f-4ba9-4378-bd7f-8a2a15f0d838}
 * Alias: AcquaintanceHistory
 * Caption: $Views_Names_AcquaintanceHistory
 * Group: Acquaintance
 */
class AcquaintanceHistoryViewInfo {
  //#region Common

  /**
   * View identifier for "AcquaintanceHistory": {6fe5e33f-4ba9-4378-bd7f-8a2a15f0d838}.
   */
   readonly ID: guid = '6fe5e33f-4ba9-4378-bd7f-8a2a15f0d838';

  /**
   * View name for "AcquaintanceHistory".
   */
   readonly Alias: string = 'AcquaintanceHistory';

  /**
   * View caption for "AcquaintanceHistory".
   */
   readonly Caption: string = '$Views_Names_AcquaintanceHistory';

  /**
   * View group for "AcquaintanceHistory".
   */
   readonly Group: string = 'Acquaintance';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ID.
   */
   readonly ColumnID: ViewObject = new ViewObject(0, 'ID');

  /**
   * ID:1
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(1, 'UserID');

  /**
   * ID:2
   * Alias: UserName
   * Caption: $Views_Acquaintance_Employee.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(2, 'UserName', '$Views_Acquaintance_Employee');

  /**
   * ID:3
   * Alias: IsReceived.
   */
   readonly ColumnIsReceived: ViewObject = new ViewObject(3, 'IsReceived');

  /**
   * ID:4
   * Alias: IsReceivedString
   * Caption: $Views_Acquaintance_State.
   */
   readonly ColumnIsReceivedString: ViewObject = new ViewObject(4, 'IsReceivedString', '$Views_Acquaintance_State');

  /**
   * ID:5
   * Alias: Received
   * Caption: $Views_Acquaintance_ReceivedDate.
   */
   readonly ColumnReceived: ViewObject = new ViewObject(5, 'Received', '$Views_Acquaintance_ReceivedDate');

  /**
   * ID:6
   * Alias: SenderID.
   */
   readonly ColumnSenderID: ViewObject = new ViewObject(6, 'SenderID');

  /**
   * ID:7
   * Alias: SenderName
   * Caption: $Views_Acquaintance_Sender.
   */
   readonly ColumnSenderName: ViewObject = new ViewObject(7, 'SenderName', '$Views_Acquaintance_Sender');

  /**
   * ID:8
   * Alias: Sent
   * Caption: $Views_Acquaintance_SentDate.
   */
   readonly ColumnSent: ViewObject = new ViewObject(8, 'Sent', '$Views_Acquaintance_SentDate');

  /**
   * ID:9
   * Alias: Comment
   * Caption: $Views_Acquaintance_CommentColumn.
   */
   readonly ColumnComment: ViewObject = new ViewObject(9, 'Comment', '$Views_Acquaintance_CommentColumn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CardIDParam
   * Caption: $Views_Acquaintance_CardID.
   */
   readonly ParamCardIDParam: ViewObject = new ViewObject(0, 'CardIDParam', '$Views_Acquaintance_CardID');

  /**
   * ID:1
   * Alias: UserParam
   * Caption: $Views_Acquaintance_Employee.
   */
   readonly ParamUserParam: ViewObject = new ViewObject(1, 'UserParam', '$Views_Acquaintance_Employee');

  /**
   * ID:2
   * Alias: IsReceivedParam
   * Caption: $Views_Acquaintance_State.
   */
   readonly ParamIsReceivedParam: ViewObject = new ViewObject(2, 'IsReceivedParam', '$Views_Acquaintance_State');

  /**
   * ID:3
   * Alias: SenderParam
   * Caption: $Views_Acquaintance_Sender.
   */
   readonly ParamSenderParam: ViewObject = new ViewObject(3, 'SenderParam', '$Views_Acquaintance_Sender');

  /**
   * ID:4
   * Alias: CommentParam
   * Caption: $Views_Acquaintance_CommentParam.
   */
   readonly ParamCommentParam: ViewObject = new ViewObject(4, 'CommentParam', '$Views_Acquaintance_CommentParam');

  //#endregion
}

//#endregion

//#region AcquaintanceStates

/**
 * ID: {02f5ab66-8e1f-4c0b-a257-5b53428273e2}
 * Alias: AcquaintanceStates
 * Caption: $Views_Names_AcquaintanceStates
 * Group: Acquaintance
 */
class AcquaintanceStatesViewInfo {
  //#region Common

  /**
   * View identifier for "AcquaintanceStates": {02f5ab66-8e1f-4c0b-a257-5b53428273e2}.
   */
   readonly ID: guid = '02f5ab66-8e1f-4c0b-a257-5b53428273e2';

  /**
   * View name for "AcquaintanceStates".
   */
   readonly Alias: string = 'AcquaintanceStates';

  /**
   * View caption for "AcquaintanceStates".
   */
   readonly Caption: string = '$Views_Names_AcquaintanceStates';

  /**
   * View group for "AcquaintanceStates".
   */
   readonly Group: string = 'Acquaintance';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: StateID.
   */
   readonly ColumnStateID: ViewObject = new ViewObject(0, 'StateID');

  /**
   * ID:1
   * Alias: StateName
   * Caption: $Views_Acquaintance_State.
   */
   readonly ColumnStateName: ViewObject = new ViewObject(1, 'StateName', '$Views_Acquaintance_State');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Acquaintance_State.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Acquaintance_State');

  //#endregion
}

//#endregion

//#region ActionHistory

/**
 * ID: {7b10287f-31bb-4d4c-a515-ae754e452ed3}
 * Alias: ActionHistory
 * Caption: $Views_Names_ActionHistory
 * Group: System
 */
class ActionHistoryViewInfo {
  //#region Common

  /**
   * View identifier for "ActionHistory": {7b10287f-31bb-4d4c-a515-ae754e452ed3}.
   */
   readonly ID: guid = '7b10287f-31bb-4d4c-a515-ae754e452ed3';

  /**
   * View name for "ActionHistory".
   */
   readonly Alias: string = 'ActionHistory';

  /**
   * View caption for "ActionHistory".
   */
   readonly Caption: string = '$Views_Names_ActionHistory';

  /**
   * View group for "ActionHistory".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RecordID.
   */
   readonly ColumnRecordID: ViewObject = new ViewObject(0, 'RecordID');

  /**
   * ID:1
   * Alias: RecordCaption.
   */
   readonly ColumnRecordCaption: ViewObject = new ViewObject(1, 'RecordCaption');

  /**
   * ID:2
   * Alias: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(2, 'CardID');

  /**
   * ID:3
   * Alias: CardCaption
   * Caption: $Views_ActionHistory_Caption.
   */
   readonly ColumnCardCaption: ViewObject = new ViewObject(3, 'CardCaption', '$Views_ActionHistory_Caption');

  /**
   * ID:4
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(4, 'TypeID');

  /**
   * ID:5
   * Alias: TypeCaption
   * Caption: $Views_ActionHistory_Type.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(5, 'TypeCaption', '$Views_ActionHistory_Type');

  /**
   * ID:6
   * Alias: ActionID.
   */
   readonly ColumnActionID: ViewObject = new ViewObject(6, 'ActionID');

  /**
   * ID:7
   * Alias: ActionName
   * Caption: $Views_ActionHistory_Action.
   */
   readonly ColumnActionName: ViewObject = new ViewObject(7, 'ActionName', '$Views_ActionHistory_Action');

  /**
   * ID:8
   * Alias: Modified
   * Caption: $Views_ActionHistory_DateTime.
   */
   readonly ColumnModified: ViewObject = new ViewObject(8, 'Modified', '$Views_ActionHistory_DateTime');

  /**
   * ID:9
   * Alias: ModifiedByID.
   */
   readonly ColumnModifiedByID: ViewObject = new ViewObject(9, 'ModifiedByID');

  /**
   * ID:10
   * Alias: ModifiedByName
   * Caption: $Views_ActionHistory_User.
   */
   readonly ColumnModifiedByName: ViewObject = new ViewObject(10, 'ModifiedByName', '$Views_ActionHistory_User');

  /**
   * ID:11
   * Alias: ApplicationID.
   */
   readonly ColumnApplicationID: ViewObject = new ViewObject(11, 'ApplicationID');

  /**
   * ID:12
   * Alias: ApplicationName
   * Caption: $Views_ActionHistory_Application.
   */
   readonly ColumnApplicationName: ViewObject = new ViewObject(12, 'ApplicationName', '$Views_ActionHistory_Application');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CardCaption
   * Caption: $Views_ActionHistory_Card_Param.
   */
   readonly ParamCardCaption: ViewObject = new ViewObject(0, 'CardCaption', '$Views_ActionHistory_Card_Param');

  /**
   * ID:1
   * Alias: TypeID
   * Caption: $Views_ActionHistory_Type_Param.
   */
   readonly ParamTypeID: ViewObject = new ViewObject(1, 'TypeID', '$Views_ActionHistory_Type_Param');

  /**
   * ID:2
   * Alias: ActionID
   * Caption: $Views_ActionHistory_Action_Param.
   */
   readonly ParamActionID: ViewObject = new ViewObject(2, 'ActionID', '$Views_ActionHistory_Action_Param');

  /**
   * ID:3
   * Alias: ApplicationID
   * Caption: $Views_ActionHistory_Application_Param.
   */
   readonly ParamApplicationID: ViewObject = new ViewObject(3, 'ApplicationID', '$Views_ActionHistory_Application_Param');

  /**
   * ID:4
   * Alias: Modified
   * Caption: $Views_ActionHistory_DateTime_Param.
   */
   readonly ParamModified: ViewObject = new ViewObject(4, 'Modified', '$Views_ActionHistory_DateTime_Param');

  /**
   * ID:5
   * Alias: ModifiedByID
   * Caption: $Views_ActionHistory_User_Param.
   */
   readonly ParamModifiedByID: ViewObject = new ViewObject(5, 'ModifiedByID', '$Views_ActionHistory_User_Param');

  /**
   * ID:6
   * Alias: ModifiedByName
   * Caption: $Views_ActionHistory_UserName_Param.
   */
   readonly ParamModifiedByName: ViewObject = new ViewObject(6, 'ModifiedByName', '$Views_ActionHistory_UserName_Param');

  /**
   * ID:7
   * Alias: CardID
   * Caption: $Views_ActionHistory_CardId_Param.
   */
   readonly ParamCardID: ViewObject = new ViewObject(7, 'CardID', '$Views_ActionHistory_CardId_Param');

  /**
   * ID:8
   * Alias: CardIDVisible
   * Caption: $Views_ActionHistory_CardId_Param.
   */
   readonly ParamCardIDVisible: ViewObject = new ViewObject(8, 'CardIDVisible', '$Views_ActionHistory_CardId_Param');

  /**
   * ID:9
   * Alias: SessionID
   * Caption: $Views_ActionHistory_SessionID_Param.
   */
   readonly ParamSessionID: ViewObject = new ViewObject(9, 'SessionID', '$Views_ActionHistory_SessionID_Param');

  /**
   * ID:10
   * Alias: SessionIDHidden
   * Caption: Session ID.
   */
   readonly ParamSessionIDHidden: ViewObject = new ViewObject(10, 'SessionIDHidden', 'Session ID');

  /**
   * ID:11
   * Alias: DepartmentID
   * Caption: $Views_ActionHistory_UserDepartment_Param.
   */
   readonly ParamDepartmentID: ViewObject = new ViewObject(11, 'DepartmentID', '$Views_ActionHistory_UserDepartment_Param');

  /**
   * ID:12
   * Alias: DatabaseID.
   */
   readonly ParamDatabaseID: ViewObject = new ViewObject(12, 'DatabaseID');

  //#endregion
}

//#endregion

//#region ActionHistoryTypes

/**
 * ID: {07775e91-96d5-4b0c-b978-abc26c55d899}
 * Alias: ActionHistoryTypes
 * Caption: $Views_Names_ActionHistoryTypes
 * Group: System
 */
class ActionHistoryTypesViewInfo {
  //#region Common

  /**
   * View identifier for "ActionHistoryTypes": {07775e91-96d5-4b0c-b978-abc26c55d899}.
   */
   readonly ID: guid = '07775e91-96d5-4b0c-b978-abc26c55d899';

  /**
   * View name for "ActionHistoryTypes".
   */
   readonly Alias: string = 'ActionHistoryTypes';

  /**
   * View caption for "ActionHistoryTypes".
   */
   readonly Caption: string = '$Views_Names_ActionHistoryTypes';

  /**
   * View group for "ActionHistoryTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeCaption
   * Caption: $Views_Types_Name.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(1, 'TypeCaption', '$Views_Types_Name');

  /**
   * ID:2
   * Alias: TypeName
   * Caption: $Views_Types_Alias.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(2, 'TypeName', '$Views_Types_Alias');

  /**
   * ID:3
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(3, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_Types_Name_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_Types_Name_Param');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_Types_Alias_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_Types_Alias_Param');

  //#endregion
}

//#endregion

//#region ActionTypes

/**
 * ID: {12532568-f56f-4399-9a86-5e76871c33aa}
 * Alias: ActionTypes
 * Caption: $Views_Names_ActionTypes
 * Group: System
 */
class ActionTypesViewInfo {
  //#region Common

  /**
   * View identifier for "ActionTypes": {12532568-f56f-4399-9a86-5e76871c33aa}.
   */
   readonly ID: guid = '12532568-f56f-4399-9a86-5e76871c33aa';

  /**
   * View name for "ActionTypes".
   */
   readonly Alias: string = 'ActionTypes';

  /**
   * View caption for "ActionTypes".
   */
   readonly Caption: string = '$Views_Names_ActionTypes';

  /**
   * View group for "ActionTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ActionID.
   */
   readonly ColumnActionID: ViewObject = new ViewObject(0, 'ActionID');

  /**
   * ID:1
   * Alias: ActionName
   * Caption: $Views_ActionTypes_Caption.
   */
   readonly ColumnActionName: ViewObject = new ViewObject(1, 'ActionName', '$Views_ActionTypes_Caption');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_ActionTypes_Caption_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_ActionTypes_Caption_Param');

  //#endregion
}

//#endregion

//#region ActiveWorkflows

/**
 * ID: {68def22d-ade6-439f-bbc4-21ea18a3c409}
 * Alias: ActiveWorkflows
 * Caption: $Views_Names_ActiveWorkflows
 * Group: WorkflowEngine
 */
class ActiveWorkflowsViewInfo {
  //#region Common

  /**
   * View identifier for "ActiveWorkflows": {68def22d-ade6-439f-bbc4-21ea18a3c409}.
   */
   readonly ID: guid = '68def22d-ade6-439f-bbc4-21ea18a3c409';

  /**
   * View name for "ActiveWorkflows".
   */
   readonly Alias: string = 'ActiveWorkflows';

  /**
   * View caption for "ActiveWorkflows".
   */
   readonly Caption: string = '$Views_Names_ActiveWorkflows';

  /**
   * View group for "ActiveWorkflows".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ProcessID.
   */
   readonly ColumnProcessID: ViewObject = new ViewObject(0, 'ProcessID');

  /**
   * ID:1
   * Alias: ProcessRowID.
   */
   readonly ColumnProcessRowID: ViewObject = new ViewObject(1, 'ProcessRowID');

  /**
   * ID:2
   * Alias: ProcessName
   * Caption: $Views_ActiveWorkflows_ProcessName.
   */
   readonly ColumnProcessName: ViewObject = new ViewObject(2, 'ProcessName', '$Views_ActiveWorkflows_ProcessName');

  /**
   * ID:3
   * Alias: ProcessCreated
   * Caption: $Views_ActiveWorkflows_Created.
   */
   readonly ColumnProcessCreated: ViewObject = new ViewObject(3, 'ProcessCreated', '$Views_ActiveWorkflows_Created');

  /**
   * ID:4
   * Alias: ProcessLastActivity
   * Caption: $Views_ActiveWorkflows_LastActivity.
   */
   readonly ColumnProcessLastActivity: ViewObject = new ViewObject(4, 'ProcessLastActivity', '$Views_ActiveWorkflows_LastActivity');

  /**
   * ID:5
   * Alias: ProcessCardID.
   */
   readonly ColumnProcessCardID: ViewObject = new ViewObject(5, 'ProcessCardID');

  /**
   * ID:6
   * Alias: ProcessCardDigest
   * Caption: $Views_ActiveWorkflows_CardDigest.
   */
   readonly ColumnProcessCardDigest: ViewObject = new ViewObject(6, 'ProcessCardDigest', '$Views_ActiveWorkflows_CardDigest');

  /**
   * ID:7
   * Alias: ProcessCardType
   * Caption: $Views_ActiveWorkflows_CardType.
   */
   readonly ColumnProcessCardType: ViewObject = new ViewObject(7, 'ProcessCardType', '$Views_ActiveWorkflows_CardType');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: ProcessTemplate
   * Caption: $Views_ActiveWorkflows_ProcessTemplate_Param.
   */
   readonly ParamProcessTemplate: ViewObject = new ViewObject(0, 'ProcessTemplate', '$Views_ActiveWorkflows_ProcessTemplate_Param');

  /**
   * ID:1
   * Alias: CardType
   * Caption: $Views_ActiveWorkflows_CardType_Param.
   */
   readonly ParamCardType: ViewObject = new ViewObject(1, 'CardType', '$Views_ActiveWorkflows_CardType_Param');

  /**
   * ID:2
   * Alias: CardDigest
   * Caption: $Views_ActiveWorkflows_CardDigest_Param.
   */
   readonly ParamCardDigest: ViewObject = new ViewObject(2, 'CardDigest', '$Views_ActiveWorkflows_CardDigest_Param');

  /**
   * ID:3
   * Alias: Created
   * Caption: $Views_ActiveWorkflows_Created_Param.
   */
   readonly ParamCreated: ViewObject = new ViewObject(3, 'Created', '$Views_ActiveWorkflows_Created_Param');

  /**
   * ID:4
   * Alias: LastActivity
   * Caption: $Views_ActiveWorkflows_LastActivity_Param.
   */
   readonly ParamLastActivity: ViewObject = new ViewObject(4, 'LastActivity', '$Views_ActiveWorkflows_LastActivity_Param');

  /**
   * ID:5
   * Alias: CardID
   * Caption: $Views_ActiveWorkflows_CardID_Param.
   */
   readonly ParamCardID: ViewObject = new ViewObject(5, 'CardID', '$Views_ActiveWorkflows_CardID_Param');

  /**
   * ID:6
   * Alias: HasActiveErrors
   * Caption: $Views_ActiveWorkflows_HasActiveErrors_Param.
   */
   readonly ParamHasActiveErrors: ViewObject = new ViewObject(6, 'HasActiveErrors', '$Views_ActiveWorkflows_HasActiveErrors_Param');

  /**
   * ID:7
   * Alias: ProcessInstance
   * Caption: Process instance.
   */
   readonly ParamProcessInstance: ViewObject = new ViewObject(7, 'ProcessInstance', 'Process instance');

  //#endregion
}

//#endregion

//#region ApplicationArchitectures

/**
 * ID: {1ff3c65c-4926-4eac-ab0a-0c28f213d482}
 * Alias: ApplicationArchitectures
 * Caption: $Views_Names_ApplicationArchitectures
 * Group: System
 */
class ApplicationArchitecturesViewInfo {
  //#region Common

  /**
   * View identifier for "ApplicationArchitectures": {1ff3c65c-4926-4eac-ab0a-0c28f213d482}.
   */
   readonly ID: guid = '1ff3c65c-4926-4eac-ab0a-0c28f213d482';

  /**
   * View name for "ApplicationArchitectures".
   */
   readonly Alias: string = 'ApplicationArchitectures';

  /**
   * View caption for "ApplicationArchitectures".
   */
   readonly Caption: string = '$Views_Names_ApplicationArchitectures';

  /**
   * View group for "ApplicationArchitectures".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ArchitectureID.
   */
   readonly ColumnArchitectureID: ViewObject = new ViewObject(0, 'ArchitectureID');

  /**
   * ID:1
   * Alias: ArchitectureName
   * Caption: $Views_ApplicationArchitectures_Name.
   */
   readonly ColumnArchitectureName: ViewObject = new ViewObject(1, 'ArchitectureName', '$Views_ApplicationArchitectures_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_ApplicationArchitectures_Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_ApplicationArchitectures_Name');

  //#endregion
}

//#endregion

//#region ApplicationNames

/**
 * ID: {1e314e13-904d-491d-93fb-d9f2f912498e}
 * Alias: ApplicationNames
 * Caption: $Views_Names_ApplicationNames
 * Group: System
 */
class ApplicationNamesViewInfo {
  //#region Common

  /**
   * View identifier for "ApplicationNames": {1e314e13-904d-491d-93fb-d9f2f912498e}.
   */
   readonly ID: guid = '1e314e13-904d-491d-93fb-d9f2f912498e';

  /**
   * View name for "ApplicationNames".
   */
   readonly Alias: string = 'ApplicationNames';

  /**
   * View caption for "ApplicationNames".
   */
   readonly Caption: string = '$Views_Names_ApplicationNames';

  /**
   * View group for "ApplicationNames".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: $Views_ApplicationNames_Name.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', '$Views_ApplicationNames_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_ApplicationNames_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_ApplicationNames_Name_Param');

  //#endregion
}

//#endregion

//#region Applications

/**
 * ID: {87345860-1b95-4fdd-887e-bf632fb3752d}
 * Alias: Applications
 * Caption: $Views_Names_Applications
 * Group: System
 */
class ApplicationsViewInfo {
  //#region Common

  /**
   * View identifier for "Applications": {87345860-1b95-4fdd-887e-bf632fb3752d}.
   */
   readonly ID: guid = '87345860-1b95-4fdd-887e-bf632fb3752d';

  /**
   * View name for "Applications".
   */
   readonly Alias: string = 'Applications';

  /**
   * View caption for "Applications".
   */
   readonly Caption: string = '$Views_Names_Applications';

  /**
   * View group for "Applications".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: AppID
   * Caption: ID.
   */
   readonly ColumnAppID: ViewObject = new ViewObject(0, 'AppID', 'ID');

  /**
   * ID:1
   * Alias: AppName
   * Caption: $Views_Applications_Name.
   */
   readonly ColumnAppName: ViewObject = new ViewObject(1, 'AppName', '$Views_Applications_Name');

  /**
   * ID:2
   * Alias: GroupName
   * Caption: $Views_Applications_GroupName.
   */
   readonly ColumnGroupName: ViewObject = new ViewObject(2, 'GroupName', '$Views_Applications_GroupName');

  /**
   * ID:3
   * Alias: LocalizedGroupName
   * Caption: $Views_Applications_GroupName.
   */
   readonly ColumnLocalizedGroupName: ViewObject = new ViewObject(3, 'LocalizedGroupName', '$Views_Applications_GroupName');

  /**
   * ID:4
   * Alias: Alias
   * Caption: $Views_Applications_Alias.
   */
   readonly ColumnAlias: ViewObject = new ViewObject(4, 'Alias', '$Views_Applications_Alias');

  /**
   * ID:5
   * Alias: Icon
   * Caption: Icon.
   */
   readonly ColumnIcon: ViewObject = new ViewObject(5, 'Icon', 'Icon');

  /**
   * ID:6
   * Alias: Client64Bit
   * Caption: $Views_Applications_Client64Bit.
   */
   readonly ColumnClient64Bit: ViewObject = new ViewObject(6, 'Client64Bit', '$Views_Applications_Client64Bit');

  /**
   * ID:7
   * Alias: ExecutableFileName
   * Caption: $Views_Applications_ExecutableFileName.
   */
   readonly ColumnExecutableFileName: ViewObject = new ViewObject(7, 'ExecutableFileName', '$Views_Applications_ExecutableFileName');

  /**
   * ID:8
   * Alias: ForAdmin
   * Caption: $Views_Applications_ForAdmin.
   */
   readonly ColumnForAdmin: ViewObject = new ViewObject(8, 'ForAdmin', '$Views_Applications_ForAdmin');

  /**
   * ID:9
   * Alias: AppManagerApiV2.
   */
   readonly ColumnAppManagerApiV2: ViewObject = new ViewObject(9, 'AppManagerApiV2');

  /**
   * ID:10
   * Alias: AppVersion
   * Caption: $Views_Applications_AppVersion.
   */
   readonly ColumnAppVersion: ViewObject = new ViewObject(10, 'AppVersion', '$Views_Applications_AppVersion');

  /**
   * ID:11
   * Alias: ExtensionVersion
   * Caption: $Views_Applications_ExtensionVersion.
   */
   readonly ColumnExtensionVersion: ViewObject = new ViewObject(11, 'ExtensionVersion', '$Views_Applications_ExtensionVersion');

  /**
   * ID:12
   * Alias: PlatformVersion
   * Caption: $Views_Applications_PlatformVersion.
   */
   readonly ColumnPlatformVersion: ViewObject = new ViewObject(12, 'PlatformVersion', '$Views_Applications_PlatformVersion');

  /**
   * ID:13
   * Alias: AvailableRoles
   * Caption: $Views_Applications_AvailableRoles.
   */
   readonly ColumnAvailableRoles: ViewObject = new ViewObject(13, 'AvailableRoles', '$Views_Applications_AvailableRoles');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Applications_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Applications_Name_Param');

  //#endregion
}

//#endregion

//#region AvailableApplications

/**
 * ID: {1a272344-a020-452a-b6e9-3720064ed760}
 * Alias: AvailableApplications
 * Caption: $Views_Names_AvailableApplications
 * Group: System
 */
class AvailableApplicationsViewInfo {
  //#region Common

  /**
   * View identifier for "AvailableApplications": {1a272344-a020-452a-b6e9-3720064ed760}.
   */
   readonly ID: guid = '1a272344-a020-452a-b6e9-3720064ed760';

  /**
   * View name for "AvailableApplications".
   */
   readonly Alias: string = 'AvailableApplications';

  /**
   * View caption for "AvailableApplications".
   */
   readonly Caption: string = '$Views_Names_AvailableApplications';

  /**
   * View group for "AvailableApplications".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ID.
   */
   readonly ColumnID: ViewObject = new ViewObject(0, 'ID');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_Applications_Name.
   */
   readonly ColumnName: ViewObject = new ViewObject(1, 'Name', '$Views_Applications_Name');

  /**
   * ID:2
   * Alias: Alias
   * Caption: $Views_Applications_Alias.
   */
   readonly ColumnAlias: ViewObject = new ViewObject(2, 'Alias', '$Views_Applications_Alias');

  /**
   * ID:3
   * Alias: Icon.
   */
   readonly ColumnIcon: ViewObject = new ViewObject(3, 'Icon');

  /**
   * ID:4
   * Alias: Client64Bit
   * Caption: $Views_Applications_Client64Bit.
   */
   readonly ColumnClient64Bit: ViewObject = new ViewObject(4, 'Client64Bit', '$Views_Applications_Client64Bit');

  /**
   * ID:5
   * Alias: ExecutableFileName
   * Caption: $Views_Applications_ExecutableFileName.
   */
   readonly ColumnExecutableFileName: ViewObject = new ViewObject(5, 'ExecutableFileName', '$Views_Applications_ExecutableFileName');

  /**
   * ID:6
   * Alias: ForAdmin
   * Caption: $Views_Applications_ForAdmin.
   */
   readonly ColumnForAdmin: ViewObject = new ViewObject(6, 'ForAdmin', '$Views_Applications_ForAdmin');

  /**
   * ID:7
   * Alias: AppManagerApiV2.
   */
   readonly ColumnAppManagerApiV2: ViewObject = new ViewObject(7, 'AppManagerApiV2');

  /**
   * ID:8
   * Alias: Hidden
   * Caption: Скрывать приложение в AppManager-e.
   */
   readonly ColumnHidden: ViewObject = new ViewObject(8, 'Hidden', 'Скрывать приложение в AppManager-e');

  /**
   * ID:9
   * Alias: AppVersion
   * Caption: $Views_Applications_AppVersion.
   */
   readonly ColumnAppVersion: ViewObject = new ViewObject(9, 'AppVersion', '$Views_Applications_AppVersion');

  /**
   * ID:10
   * Alias: ExtensionVersion
   * Caption: $Views_Applications_ExtensionVersion.
   */
   readonly ColumnExtensionVersion: ViewObject = new ViewObject(10, 'ExtensionVersion', '$Views_Applications_ExtensionVersion');

  /**
   * ID:11
   * Alias: PlatformVersion
   * Caption: $Views_Applications_PlatformVersion.
   */
   readonly ColumnPlatformVersion: ViewObject = new ViewObject(11, 'PlatformVersion', '$Views_Applications_PlatformVersion');

  /**
   * ID:12
   * Alias: Modified.
   */
   readonly ColumnModified: ViewObject = new ViewObject(12, 'Modified');

  /**
   * ID:13
   * Alias: GroupName
   * Caption: $Views_Applications_GroupName.
   */
   readonly ColumnGroupName: ViewObject = new ViewObject(13, 'GroupName', '$Views_Applications_GroupName');

  /**
   * ID:14
   * Alias: LocalizedGroupName
   * Caption: $Views_Applications_GroupName.
   */
   readonly ColumnLocalizedGroupName: ViewObject = new ViewObject(14, 'LocalizedGroupName', '$Views_Applications_GroupName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Alias
   * Caption: Alias.
   */
   readonly ParamAlias: ViewObject = new ViewObject(0, 'Alias', 'Alias');

  /**
   * ID:1
   * Alias: Client64Bit
   * Caption: Client64Bit.
   */
   readonly ParamClient64Bit: ViewObject = new ViewObject(1, 'Client64Bit', 'Client64Bit');

  /**
   * ID:2
   * Alias: GetIcon
   * Caption: $Views_AvailableApplications_GetIcon.
   */
   readonly ParamGetIcon: ViewObject = new ViewObject(2, 'GetIcon', '$Views_AvailableApplications_GetIcon');

  /**
   * ID:3
   * Alias: PublishMode
   * Caption: Publish mode.
   */
   readonly ParamPublishMode: ViewObject = new ViewObject(3, 'PublishMode', 'Publish mode');

  //#endregion
}

//#endregion

//#region AvailableDeputyRoles

/**
 * ID: {530f0463-70bd-4d23-9acc-7e79e1de11af}
 * Alias: AvailableDeputyRoles
 * Caption: $Views_Names_AvailableDeputyRoles
 * Group: System
 */
class AvailableDeputyRolesViewInfo {
  //#region Common

  /**
   * View identifier for "AvailableDeputyRoles": {530f0463-70bd-4d23-9acc-7e79e1de11af}.
   */
   readonly ID: guid = '530f0463-70bd-4d23-9acc-7e79e1de11af';

  /**
   * View name for "AvailableDeputyRoles".
   */
   readonly Alias: string = 'AvailableDeputyRoles';

  /**
   * View caption for "AvailableDeputyRoles".
   */
   readonly Caption: string = '$Views_Names_AvailableDeputyRoles';

  /**
   * View group for "AvailableDeputyRoles".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RoleID.
   */
   readonly ColumnRoleID: ViewObject = new ViewObject(0, 'RoleID');

  /**
   * ID:1
   * Alias: RoleName
   * Caption: $Views_Roles_Role.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(1, 'RoleName', '$Views_Roles_Role');

  /**
   * ID:2
   * Alias: TypeName
   * Caption: $Views_Roles_Type.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(2, 'TypeName', '$Views_Roles_Type');

  /**
   * ID:3
   * Alias: Info
   * Caption: $Views_Roles_Info.
   */
   readonly ColumnInfo: ViewObject = new ViewObject(3, 'Info', '$Views_Roles_Info');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Roles_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Roles_Name_Param');

  /**
   * ID:1
   * Alias: TypeID
   * Caption: $Views_Roles_Type_Param.
   */
   readonly ParamTypeID: ViewObject = new ViewObject(1, 'TypeID', '$Views_Roles_Type_Param');

  /**
   * ID:2
   * Alias: ShowHidden
   * Caption: $Views_Roles_ShowHidden_Param.
   */
   readonly ParamShowHidden: ViewObject = new ViewObject(2, 'ShowHidden', '$Views_Roles_ShowHidden_Param');

  /**
   * ID:3
   * Alias: User
   * Caption: User.
   */
   readonly ParamUser: ViewObject = new ViewObject(3, 'User', 'User');

  //#endregion
}

//#endregion

//#region AvailableDeputyUsers

/**
 * ID: {c7c46016-75e6-46e5-9627-74a4e5e66e29}
 * Alias: AvailableDeputyUsers
 * Caption: $Views_Names_AvailableDeputyUsers
 * Group: System
 */
class AvailableDeputyUsersViewInfo {
  //#region Common

  /**
   * View identifier for "AvailableDeputyUsers": {c7c46016-75e6-46e5-9627-74a4e5e66e29}.
   */
   readonly ID: guid = 'c7c46016-75e6-46e5-9627-74a4e5e66e29';

  /**
   * View name for "AvailableDeputyUsers".
   */
   readonly Alias: string = 'AvailableDeputyUsers';

  /**
   * View caption for "AvailableDeputyUsers".
   */
   readonly Caption: string = '$Views_Names_AvailableDeputyUsers';

  /**
   * View group for "AvailableDeputyUsers".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(0, 'UserID');

  /**
   * ID:1
   * Alias: UserName
   * Caption: $Views_Users_Name.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(1, 'UserName', '$Views_Users_Name');

  /**
   * ID:2
   * Alias: Email
   * Caption: $Views_Users_Email.
   */
   readonly ColumnEmail: ViewObject = new ViewObject(2, 'Email', '$Views_Users_Email');

  /**
   * ID:3
   * Alias: Position
   * Caption: $Views_Users_Position.
   */
   readonly ColumnPosition: ViewObject = new ViewObject(3, 'Position', '$Views_Users_Position');

  /**
   * ID:4
   * Alias: Departments
   * Caption: $Views_Users_Departments.
   */
   readonly ColumnDepartments: ViewObject = new ViewObject(4, 'Departments', '$Views_Users_Departments');

  /**
   * ID:5
   * Alias: StaticRoles
   * Caption: $Views_Users_StaticRoles.
   */
   readonly ColumnStaticRoles: ViewObject = new ViewObject(5, 'StaticRoles', '$Views_Users_StaticRoles');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Users_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Users_Name_Param');

  /**
   * ID:1
   * Alias: RoleID
   * Caption: $Views_Users_Role_Param.
   */
   readonly ParamRoleID: ViewObject = new ViewObject(1, 'RoleID', '$Views_Users_Role_Param');

  /**
   * ID:2
   * Alias: ParentRoleID
   * Caption: $Views_Users_ParentRole_Param.
   */
   readonly ParamParentRoleID: ViewObject = new ViewObject(2, 'ParentRoleID', '$Views_Users_ParentRole_Param');

  /**
   * ID:3
   * Alias: ShowHidden
   * Caption: $Views_Users_ShowHidden_Param.
   */
   readonly ParamShowHidden: ViewObject = new ViewObject(3, 'ShowHidden', '$Views_Users_ShowHidden_Param');

  /**
   * ID:4
   * Alias: User
   * Caption: User.
   */
   readonly ParamUser: ViewObject = new ViewObject(4, 'User', 'User');

  //#endregion
}

//#endregion

//#region BarcodeTypes

/**
 * ID: {f92af4c2-e862-4469-9e44-5c96e650e349}
 * Alias: BarcodeTypes
 * Caption: $Views_Names_BarcodeTypes
 * Group: System
 */
class BarcodeTypesViewInfo {
  //#region Common

  /**
   * View identifier for "BarcodeTypes": {f92af4c2-e862-4469-9e44-5c96e650e349}.
   */
   readonly ID: guid = 'f92af4c2-e862-4469-9e44-5c96e650e349';

  /**
   * View name for "BarcodeTypes".
   */
   readonly Alias: string = 'BarcodeTypes';

  /**
   * View caption for "BarcodeTypes".
   */
   readonly Caption: string = '$Views_Names_BarcodeTypes';

  /**
   * View group for "BarcodeTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: BarcodeTypeID.
   */
   readonly ColumnBarcodeTypeID: ViewObject = new ViewObject(0, 'BarcodeTypeID');

  /**
   * ID:1
   * Alias: BarcodeTypeName
   * Caption: $Views_BarcodeTypes_Name.
   */
   readonly ColumnBarcodeTypeName: ViewObject = new ViewObject(1, 'BarcodeTypeName', '$Views_BarcodeTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: NameParam
   * Caption: $Views_BarcodeTypes_Name_Param.
   */
   readonly ParamNameParam: ViewObject = new ViewObject(0, 'NameParam', '$Views_BarcodeTypes_Name_Param');

  /**
   * ID:1
   * Alias: CanScanParam
   * Caption: CanScan.
   */
   readonly ParamCanScanParam: ViewObject = new ViewObject(1, 'CanScanParam', 'CanScan');

  /**
   * ID:2
   * Alias: CanPrintParam
   * Caption: CanPrint.
   */
   readonly ParamCanPrintParam: ViewObject = new ViewObject(2, 'CanPrintParam', 'CanPrint');

  //#endregion
}

//#endregion

//#region BusinessProcessTemplates

/**
 * ID: {b9174abb-c460-4a68-b56e-187d4d8f4896}
 * Alias: BusinessProcessTemplates
 * Caption: $Views_Names_BusinessProcessTemplates
 * Group: WorkflowEngine
 */
class BusinessProcessTemplatesViewInfo {
  //#region Common

  /**
   * View identifier for "BusinessProcessTemplates": {b9174abb-c460-4a68-b56e-187d4d8f4896}.
   */
   readonly ID: guid = 'b9174abb-c460-4a68-b56e-187d4d8f4896';

  /**
   * View name for "BusinessProcessTemplates".
   */
   readonly Alias: string = 'BusinessProcessTemplates';

  /**
   * View caption for "BusinessProcessTemplates".
   */
   readonly Caption: string = '$Views_Names_BusinessProcessTemplates';

  /**
   * View group for "BusinessProcessTemplates".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: BusinessProcessID.
   */
   readonly ColumnBusinessProcessID: ViewObject = new ViewObject(0, 'BusinessProcessID');

  /**
   * ID:1
   * Alias: BusinessProcessName
   * Caption: $Views_BusinessProcessTemplates_ProcessTemplateName.
   */
   readonly ColumnBusinessProcessName: ViewObject = new ViewObject(1, 'BusinessProcessName', '$Views_BusinessProcessTemplates_ProcessTemplateName');

  /**
   * ID:2
   * Alias: BusinessProcessGroup
   * Caption: $Views_BusinessProcessTemplates_Group.
   */
   readonly ColumnBusinessProcessGroup: ViewObject = new ViewObject(2, 'BusinessProcessGroup', '$Views_BusinessProcessTemplates_Group');

  /**
   * ID:3
   * Alias: BusinessProcessStartFromCard
   * Caption: $Views_BusinessProcessTemplates_RunFromCard.
   */
   readonly ColumnBusinessProcessStartFromCard: ViewObject = new ViewObject(3, 'BusinessProcessStartFromCard', '$Views_BusinessProcessTemplates_RunFromCard');

  /**
   * ID:4
   * Alias: BusinessProcessMultiple
   * Caption: $Views_BusinessProcessTemplates_MultipleInstances.
   */
   readonly ColumnBusinessProcessMultiple: ViewObject = new ViewObject(4, 'BusinessProcessMultiple', '$Views_BusinessProcessTemplates_MultipleInstances');

  /**
   * ID:5
   * Alias: BusinessProcessCardTypes
   * Caption: $Views_BusinessProcessTemplates_CardTypes.
   */
   readonly ColumnBusinessProcessCardTypes: ViewObject = new ViewObject(5, 'BusinessProcessCardTypes', '$Views_BusinessProcessTemplates_CardTypes');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_BusinessProcessTemplates_ProcessTemplateName_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_BusinessProcessTemplates_ProcessTemplateName_Param');

  /**
   * ID:1
   * Alias: Group
   * Caption: $Views_BusinessProcessTemplates_Group_Param.
   */
   readonly ParamGroup: ViewObject = new ViewObject(1, 'Group', '$Views_BusinessProcessTemplates_Group_Param');

  /**
   * ID:2
   * Alias: StartFromCard
   * Caption: $Views_BusinessProcessTemplates_RunFromCard_Param.
   */
   readonly ParamStartFromCard: ViewObject = new ViewObject(2, 'StartFromCard', '$Views_BusinessProcessTemplates_RunFromCard_Param');

  /**
   * ID:3
   * Alias: Multiple
   * Caption: $Views_BusinessProcessTemplates_MultipleInstances_Param.
   */
   readonly ParamMultiple: ViewObject = new ViewObject(3, 'Multiple', '$Views_BusinessProcessTemplates_MultipleInstances_Param');

  /**
   * ID:4
   * Alias: CardType
   * Caption: $Views_BusinessProcessTemplates_CardType_Param.
   */
   readonly ParamCardType: ViewObject = new ViewObject(4, 'CardType', '$Views_BusinessProcessTemplates_CardType_Param');

  /**
   * ID:5
   * Alias: GroupHidden
   * Caption: $Views_BusinessProcessTemplates_Group_Param.
   */
   readonly ParamGroupHidden: ViewObject = new ViewObject(5, 'GroupHidden', '$Views_BusinessProcessTemplates_Group_Param');

  //#endregion
}

//#endregion

//#region CalendarCalcMethods

/**
 * ID: {61a516b2-bb7d-41b7-b05c-57a5aeb564ac}
 * Alias: CalendarCalcMethods
 * Caption: $Views_Names_CalendarCalcMethods
 * Group: Calendar
 */
class CalendarCalcMethodsViewInfo {
  //#region Common

  /**
   * View identifier for "CalendarCalcMethods": {61a516b2-bb7d-41b7-b05c-57a5aeb564ac}.
   */
   readonly ID: guid = '61a516b2-bb7d-41b7-b05c-57a5aeb564ac';

  /**
   * View name for "CalendarCalcMethods".
   */
   readonly Alias: string = 'CalendarCalcMethods';

  /**
   * View caption for "CalendarCalcMethods".
   */
   readonly Caption: string = '$Views_Names_CalendarCalcMethods';

  /**
   * View group for "CalendarCalcMethods".
   */
   readonly Group: string = 'Calendar';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: MethodID.
   */
   readonly ColumnMethodID: ViewObject = new ViewObject(0, 'MethodID');

  /**
   * ID:1
   * Alias: MethodName
   * Caption: $Views_CalendarCalcMethods_Name.
   */
   readonly ColumnMethodName: ViewObject = new ViewObject(1, 'MethodName', '$Views_CalendarCalcMethods_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_CalendarCalcMethods_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_CalendarCalcMethods_Name_Param');

  //#endregion
}

//#endregion

//#region Calendars

/**
 * ID: {d352f577-8724-4677-a61b-d3e66effd5e1}
 * Alias: Calendars
 * Caption: $Views_Names_Calendars
 * Group: Calendar
 */
class CalendarsViewInfo {
  //#region Common

  /**
   * View identifier for "Calendars": {d352f577-8724-4677-a61b-d3e66effd5e1}.
   */
   readonly ID: guid = 'd352f577-8724-4677-a61b-d3e66effd5e1';

  /**
   * View name for "Calendars".
   */
   readonly Alias: string = 'Calendars';

  /**
   * View caption for "Calendars".
   */
   readonly Caption: string = '$Views_Names_Calendars';

  /**
   * View group for "Calendars".
   */
   readonly Group: string = 'Calendar';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CalendarID.
   */
   readonly ColumnCalendarID: ViewObject = new ViewObject(0, 'CalendarID');

  /**
   * ID:1
   * Alias: CalendarCalendarID
   * Caption: $Views_Calendars_CalendarID.
   */
   readonly ColumnCalendarCalendarID: ViewObject = new ViewObject(1, 'CalendarCalendarID', '$Views_Calendars_CalendarID');

  /**
   * ID:2
   * Alias: CalendarName
   * Caption: $Views_Calendars_Name.
   */
   readonly ColumnCalendarName: ViewObject = new ViewObject(2, 'CalendarName', '$Views_Calendars_Name');

  /**
   * ID:3
   * Alias: CalendarTypeID.
   */
   readonly ColumnCalendarTypeID: ViewObject = new ViewObject(3, 'CalendarTypeID');

  /**
   * ID:4
   * Alias: CalendarTypeCaption
   * Caption: $Views_Calendars_TypeCaption.
   */
   readonly ColumnCalendarTypeCaption: ViewObject = new ViewObject(4, 'CalendarTypeCaption', '$Views_Calendars_TypeCaption');

  /**
   * ID:5
   * Alias: Description
   * Caption: $Views_Calendars_Description.
   */
   readonly ColumnDescription: ViewObject = new ViewObject(5, 'Description', '$Views_Calendars_Description');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Calendars_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Calendars_Name_Param');

  //#endregion
}

//#endregion

//#region CalendarTypes

/**
 * ID: {422a7b6e-9d7f-4d76-aba1-d3487cae216d}
 * Alias: CalendarTypes
 * Caption: $Views_Names_CalendarTypes
 * Group: Calendar
 */
class CalendarTypesViewInfo {
  //#region Common

  /**
   * View identifier for "CalendarTypes": {422a7b6e-9d7f-4d76-aba1-d3487cae216d}.
   */
   readonly ID: guid = '422a7b6e-9d7f-4d76-aba1-d3487cae216d';

  /**
   * View name for "CalendarTypes".
   */
   readonly Alias: string = 'CalendarTypes';

  /**
   * View caption for "CalendarTypes".
   */
   readonly Caption: string = '$Views_Names_CalendarTypes';

  /**
   * View group for "CalendarTypes".
   */
   readonly Group: string = 'Calendar';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeCaption
   * Caption: $Views_CalendarCalcMethods_Name.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(1, 'TypeCaption', '$Views_CalendarCalcMethods_Name');

  /**
   * ID:2
   * Alias: TypeWorkDaysInWeek
   * Caption: $CardTypes_Controls_WorkingDaysInWeek.
   */
   readonly ColumnTypeWorkDaysInWeek: ViewObject = new ViewObject(2, 'TypeWorkDaysInWeek', '$CardTypes_Controls_WorkingDaysInWeek');

  /**
   * ID:3
   * Alias: TypeHoursInDay
   * Caption: $CardTypes_Controls_HoursInDay.
   */
   readonly ColumnTypeHoursInDay: ViewObject = new ViewObject(3, 'TypeHoursInDay', '$CardTypes_Controls_HoursInDay');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_CalendarCalcMethods_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_CalendarCalcMethods_Name_Param');

  //#endregion
}

//#endregion

//#region CardTasks

/**
 * ID: {eff8e7b5-0874-4e7d-ab09-2537e821b43d}
 * Alias: CardTasks
 * Caption: $Views_Names_CardTasks
 * Group: TaskAssignedRoles
 */
class CardTasksViewInfo {
  //#region Common

  /**
   * View identifier for "CardTasks": {eff8e7b5-0874-4e7d-ab09-2537e821b43d}.
   */
   readonly ID: guid = 'eff8e7b5-0874-4e7d-ab09-2537e821b43d';

  /**
   * View name for "CardTasks".
   */
   readonly Alias: string = 'CardTasks';

  /**
   * View caption for "CardTasks".
   */
   readonly Caption: string = '$Views_Names_CardTasks';

  /**
   * View group for "CardTasks".
   */
   readonly Group: string = 'TaskAssignedRoles';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ID.
   */
   readonly ColumnID: ViewObject = new ViewObject(0, 'ID');

  /**
   * ID:1
   * Alias: TaskID.
   */
   readonly ColumnTaskID: ViewObject = new ViewObject(1, 'TaskID');

  /**
   * ID:2
   * Alias: StateID.
   */
   readonly ColumnStateID: ViewObject = new ViewObject(2, 'StateID');

  /**
   * ID:3
   * Alias: StateName
   * Caption: $Views_MyTasks_State.
   */
   readonly ColumnStateName: ViewObject = new ViewObject(3, 'StateName', '$Views_MyTasks_State');

  /**
   * ID:4
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(4, 'TypeID');

  /**
   * ID:5
   * Alias: PlannedDate
   * Caption: $Views_MyTasks_Planned.
   */
   readonly ColumnPlannedDate: ViewObject = new ViewObject(5, 'PlannedDate', '$Views_MyTasks_Planned');

  /**
   * ID:6
   * Alias: TaskDigest
   * Caption: $Views_MyTasks_Info.
   */
   readonly ColumnTaskDigest: ViewObject = new ViewObject(6, 'TaskDigest', '$Views_MyTasks_Info');

  /**
   * ID:7
   * Alias: RoleID.
   */
   readonly ColumnRoleID: ViewObject = new ViewObject(7, 'RoleID');

  /**
   * ID:8
   * Alias: RoleName
   * Caption: $Views_MyTasks_Performer.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(8, 'RoleName', '$Views_MyTasks_Performer');

  /**
   * ID:9
   * Alias: AuthorID.
   */
   readonly ColumnAuthorID: ViewObject = new ViewObject(9, 'AuthorID');

  /**
   * ID:10
   * Alias: AuthorName
   * Caption: $Views_MyTasks_Author.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(10, 'AuthorName', '$Views_MyTasks_Author');

  /**
   * ID:11
   * Alias: Modified
   * Caption: $Views_MyTasks_Modified.
   */
   readonly ColumnModified: ViewObject = new ViewObject(11, 'Modified', '$Views_MyTasks_Modified');

  /**
   * ID:12
   * Alias: Created.
   */
   readonly ColumnCreated: ViewObject = new ViewObject(12, 'Created');

  /**
   * ID:13
   * Alias: CreatedByID.
   */
   readonly ColumnCreatedByID: ViewObject = new ViewObject(13, 'CreatedByID');

  /**
   * ID:14
   * Alias: CreatedByName.
   */
   readonly ColumnCreatedByName: ViewObject = new ViewObject(14, 'CreatedByName');

  /**
   * ID:15
   * Alias: TimeZoneUtcOffsetMinutes.
   */
   readonly ColumnTimeZoneUtcOffsetMinutes: ViewObject = new ViewObject(15, 'TimeZoneUtcOffsetMinutes');

  /**
   * ID:16
   * Alias: TypeCaption
   * Caption: $Views_MyTasks_TaskType.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(16, 'TypeCaption', '$Views_MyTasks_TaskType');

  /**
   * ID:17
   * Alias: TimeToCompletion
   * Caption: $Views_MyTasks_TimeToCompletion.
   */
   readonly ColumnTimeToCompletion: ViewObject = new ViewObject(17, 'TimeToCompletion', '$Views_MyTasks_TimeToCompletion');

  /**
   * ID:18
   * Alias: CalendarID.
   */
   readonly ColumnCalendarID: ViewObject = new ViewObject(18, 'CalendarID');

  /**
   * ID:19
   * Alias: QuantsToFinish.
   */
   readonly ColumnQuantsToFinish: ViewObject = new ViewObject(19, 'QuantsToFinish');

  /**
   * ID:20
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(20, 'rn');

  /**
   * ID:21
   * Alias: AppearanceColumn.
   */
   readonly ColumnAppearanceColumn: ViewObject = new ViewObject(21, 'AppearanceColumn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CardIDParam
   * Caption: $Views_CardTasks_CardID_Param.
   */
   readonly ParamCardIDParam: ViewObject = new ViewObject(0, 'CardIDParam', '$Views_CardTasks_CardID_Param');

  /**
   * ID:1
   * Alias: Token
   * Caption: Token.
   */
   readonly ParamToken: ViewObject = new ViewObject(1, 'Token', 'Token');

  /**
   * ID:2
   * Alias: FunctionRoleAuthorParam
   * Caption: $Views_MyTasks_FunctionRole_Author_Param.
   */
   readonly ParamFunctionRoleAuthorParam: ViewObject = new ViewObject(2, 'FunctionRoleAuthorParam', '$Views_MyTasks_FunctionRole_Author_Param');

  /**
   * ID:3
   * Alias: FunctionRolePerformerParam
   * Caption: $Views_MyTasks_FunctionRole_Performer_Param.
   */
   readonly ParamFunctionRolePerformerParam: ViewObject = new ViewObject(3, 'FunctionRolePerformerParam', '$Views_MyTasks_FunctionRole_Performer_Param');

  /**
   * ID:4
   * Alias: Performer
   * Caption: $Views_CardTasks_Performer_Param.
   */
   readonly ParamPerformer: ViewObject = new ViewObject(4, 'Performer', '$Views_CardTasks_Performer_Param');

  /**
   * ID:5
   * Alias: Author
   * Caption: $Views_CardTasks_Author_Param.
   */
   readonly ParamAuthor: ViewObject = new ViewObject(5, 'Author', '$Views_CardTasks_Author_Param');

  /**
   * ID:6
   * Alias: State
   * Caption: $Views_CardTasks_State_Param.
   */
   readonly ParamState: ViewObject = new ViewObject(6, 'State', '$Views_CardTasks_State_Param');

  /**
   * ID:7
   * Alias: Planned
   * Caption: $Views_CardTasks_Planned_Param.
   */
   readonly ParamPlanned: ViewObject = new ViewObject(7, 'Planned', '$Views_CardTasks_Planned_Param');

  //#endregion
}

//#endregion

//#region CardTaskSessionRoles

/**
 * ID: {088b9367-ca87-46b4-a9e2-336b0a183a8d}
 * Alias: CardTaskSessionRoles
 * Caption: $Views_Names_CardTaskSessionRoles
 * Group: TaskAssignedRoles
 */
class CardTaskSessionRolesViewInfo {
  //#region Common

  /**
   * View identifier for "CardTaskSessionRoles": {088b9367-ca87-46b4-a9e2-336b0a183a8d}.
   */
   readonly ID: guid = '088b9367-ca87-46b4-a9e2-336b0a183a8d';

  /**
   * View name for "CardTaskSessionRoles".
   */
   readonly Alias: string = 'CardTaskSessionRoles';

  /**
   * View caption for "CardTaskSessionRoles".
   */
   readonly Caption: string = '$Views_Names_CardTaskSessionRoles';

  /**
   * View group for "CardTaskSessionRoles".
   */
   readonly Group: string = 'TaskAssignedRoles';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TaskRoleRowID.
   */
   readonly ColumnTaskRoleRowID: ViewObject = new ViewObject(0, 'TaskRoleRowID');

  /**
   * ID:1
   * Alias: IsDeputy.
   */
   readonly ColumnIsDeputy: ViewObject = new ViewObject(1, 'IsDeputy');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: UserID
   * Caption: UserID.
   */
   readonly ParamUserID: ViewObject = new ViewObject(0, 'UserID', 'UserID');

  /**
   * ID:1
   * Alias: TaskRowID
   * Caption: TaskRowID.
   */
   readonly ParamTaskRowID: ViewObject = new ViewObject(1, 'TaskRowID', 'TaskRowID');

  //#endregion
}

//#endregion

//#region Cars

/**
 * ID: {257b72ba-9bba-457a-8456-d90d55d440e2}
 * Alias: Cars
 * Caption: $Views_Names_Cars
 * Group: Testing
 */
class CarsViewInfo {
  //#region Common

  /**
   * View identifier for "Cars": {257b72ba-9bba-457a-8456-d90d55d440e2}.
   */
   readonly ID: guid = '257b72ba-9bba-457a-8456-d90d55d440e2';

  /**
   * View name for "Cars".
   */
   readonly Alias: string = 'Cars';

  /**
   * View caption for "Cars".
   */
   readonly Caption: string = '$Views_Names_Cars';

  /**
   * View group for "Cars".
   */
   readonly Group: string = 'Testing';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CarID.
   */
   readonly ColumnCarID: ViewObject = new ViewObject(0, 'CarID');

  /**
   * ID:1
   * Alias: CarName
   * Caption: $Views_Cars_CardName.
   */
   readonly ColumnCarName: ViewObject = new ViewObject(1, 'CarName', '$Views_Cars_CardName');

  /**
   * ID:2
   * Alias: CarMaxSpeed
   * Caption: $Views_Cars_MaxSpeed.
   */
   readonly ColumnCarMaxSpeed: ViewObject = new ViewObject(2, 'CarMaxSpeed', '$Views_Cars_MaxSpeed');

  /**
   * ID:3
   * Alias: DriverID.
   */
   readonly ColumnDriverID: ViewObject = new ViewObject(3, 'DriverID');

  /**
   * ID:4
   * Alias: DriverName
   * Caption: $Views_Cars_DriverName.
   */
   readonly ColumnDriverName: ViewObject = new ViewObject(4, 'DriverName', '$Views_Cars_DriverName');

  /**
   * ID:5
   * Alias: CarReleaseDate
   * Caption: $Views_Cars_ReleaseDate.
   */
   readonly ColumnCarReleaseDate: ViewObject = new ViewObject(5, 'CarReleaseDate', '$Views_Cars_ReleaseDate');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CarName
   * Caption: $Views_Cars_CardName_Param.
   */
   readonly ParamCarName: ViewObject = new ViewObject(0, 'CarName', '$Views_Cars_CardName_Param');

  /**
   * ID:1
   * Alias: CarMaxSpeed
   * Caption: $Views_Cars_MaxSpeed_Param.
   */
   readonly ParamCarMaxSpeed: ViewObject = new ViewObject(1, 'CarMaxSpeed', '$Views_Cars_MaxSpeed_Param');

  /**
   * ID:2
   * Alias: Driver
   * Caption: $Views_Cars_DriverName_Param.
   */
   readonly ParamDriver: ViewObject = new ViewObject(2, 'Driver', '$Views_Cars_DriverName_Param');

  /**
   * ID:3
   * Alias: CarReleaseDateFrom
   * Caption: $Views_Cars_ReleaseDateFrom_Param.
   */
   readonly ParamCarReleaseDateFrom: ViewObject = new ViewObject(3, 'CarReleaseDateFrom', '$Views_Cars_ReleaseDateFrom_Param');

  /**
   * ID:4
   * Alias: CarReleaseDateTo
   * Caption: $Views_Cars_ReleaseDateTo_Param.
   */
   readonly ParamCarReleaseDateTo: ViewObject = new ViewObject(4, 'CarReleaseDateTo', '$Views_Cars_ReleaseDateTo_Param');

  /**
   * ID:5
   * Alias: Mileage
   * Caption: $Views_Cars_Mileage_Param.
   */
   readonly ParamMileage: ViewObject = new ViewObject(5, 'Mileage', '$Views_Cars_Mileage_Param');

  /**
   * ID:6
   * Alias: Price
   * Caption: $CardTypes_Controls_Price.
   */
   readonly ParamPrice: ViewObject = new ViewObject(6, 'Price', '$CardTypes_Controls_Price');

  /**
   * ID:7
   * Alias: CarID
   * Caption: $Views_Cars_CarID_Param.
   */
   readonly ParamCarID: ViewObject = new ViewObject(7, 'CarID', '$Views_Cars_CarID_Param');

  //#endregion
}

//#endregion

//#region CompletedTasks

/**
 * ID: {c480683b-b3b4-4f8a-8786-3899b5bf7f00}
 * Alias: CompletedTasks
 * Caption: $Views_Names_CompletedTasks
 * Group: System
 */
class CompletedTasksViewInfo {
  //#region Common

  /**
   * View identifier for "CompletedTasks": {c480683b-b3b4-4f8a-8786-3899b5bf7f00}.
   */
   readonly ID: guid = 'c480683b-b3b4-4f8a-8786-3899b5bf7f00';

  /**
   * View name for "CompletedTasks".
   */
   readonly Alias: string = 'CompletedTasks';

  /**
   * View caption for "CompletedTasks".
   */
   readonly Caption: string = '$Views_Names_CompletedTasks';

  /**
   * View group for "CompletedTasks".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(0, 'CardID');

  /**
   * ID:1
   * Alias: CardName
   * Caption: $Views_CompletedTasks_Card.
   */
   readonly ColumnCardName: ViewObject = new ViewObject(1, 'CardName', '$Views_CompletedTasks_Card');

  /**
   * ID:2
   * Alias: TaskID.
   */
   readonly ColumnTaskID: ViewObject = new ViewObject(2, 'TaskID');

  /**
   * ID:3
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(3, 'TypeID');

  /**
   * ID:4
   * Alias: TypeCaption
   * Caption: $Views_CompletedTasks_TaskType.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(4, 'TypeCaption', '$Views_CompletedTasks_TaskType');

  /**
   * ID:5
   * Alias: CardTypeCaption
   * Caption: $Views_CompletedTasks_CardType.
   */
   readonly ColumnCardTypeCaption: ViewObject = new ViewObject(5, 'CardTypeCaption', '$Views_CompletedTasks_CardType');

  /**
   * ID:6
   * Alias: OptionID.
   */
   readonly ColumnOptionID: ViewObject = new ViewObject(6, 'OptionID');

  /**
   * ID:7
   * Alias: OptionCaption
   * Caption: $Views_CompletedTasks_CompletionOption.
   */
   readonly ColumnOptionCaption: ViewObject = new ViewObject(7, 'OptionCaption', '$Views_CompletedTasks_CompletionOption');

  /**
   * ID:8
   * Alias: RoleID.
   */
   readonly ColumnRoleID: ViewObject = new ViewObject(8, 'RoleID');

  /**
   * ID:9
   * Alias: RoleName
   * Caption: $Views_CompletedTasks_Role.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(9, 'RoleName', '$Views_CompletedTasks_Role');

  /**
   * ID:10
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(10, 'UserID');

  /**
   * ID:11
   * Alias: UserName
   * Caption: $Views_CompletedTasks_User.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(11, 'UserName', '$Views_CompletedTasks_User');

  /**
   * ID:12
   * Alias: CompletedByID.
   */
   readonly ColumnCompletedByID: ViewObject = new ViewObject(12, 'CompletedByID');

  /**
   * ID:13
   * Alias: CompletedByName
   * Caption: $Views_CompletedTasks_CompletedBy.
   */
   readonly ColumnCompletedByName: ViewObject = new ViewObject(13, 'CompletedByName', '$Views_CompletedTasks_CompletedBy');

  /**
   * ID:14
   * Alias: AuthorID.
   */
   readonly ColumnAuthorID: ViewObject = new ViewObject(14, 'AuthorID');

  /**
   * ID:15
   * Alias: AuthorName
   * Caption: $Views_CompletedTasks_Author.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(15, 'AuthorName', '$Views_CompletedTasks_Author');

  /**
   * ID:16
   * Alias: Result
   * Caption: $Views_CompletedTasks_Result.
   */
   readonly ColumnResult: ViewObject = new ViewObject(16, 'Result', '$Views_CompletedTasks_Result');

  /**
   * ID:17
   * Alias: Created
   * Caption: $Views_CompletedTasks_Created.
   */
   readonly ColumnCreated: ViewObject = new ViewObject(17, 'Created', '$Views_CompletedTasks_Created');

  /**
   * ID:18
   * Alias: Planned
   * Caption: $Views_CompletedTasks_Planned.
   */
   readonly ColumnPlanned: ViewObject = new ViewObject(18, 'Planned', '$Views_CompletedTasks_Planned');

  /**
   * ID:19
   * Alias: Completed
   * Caption: $Views_CompletedTasks_Completed.
   */
   readonly ColumnCompleted: ViewObject = new ViewObject(19, 'Completed', '$Views_CompletedTasks_Completed');

  /**
   * ID:20
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(20, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CompletionDate
   * Caption: $Views_CompletedTasks_CompletionDate_Param.
   */
   readonly ParamCompletionDate: ViewObject = new ViewObject(0, 'CompletionDate', '$Views_CompletedTasks_CompletionDate_Param');

  /**
   * ID:1
   * Alias: TypeParam
   * Caption: $Views_CompletedTasks_CardType_Param.
   */
   readonly ParamTypeParam: ViewObject = new ViewObject(1, 'TypeParam', '$Views_CompletedTasks_CardType_Param');

  /**
   * ID:2
   * Alias: TaskType
   * Caption: $Views_CompletedTasks_TaskType_Param.
   */
   readonly ParamTaskType: ViewObject = new ViewObject(2, 'TaskType', '$Views_CompletedTasks_TaskType_Param');

  /**
   * ID:3
   * Alias: SelUser
   * Caption: $Views_CompletedTasks_User_Param.
   */
   readonly ParamSelUser: ViewObject = new ViewObject(3, 'SelUser', '$Views_CompletedTasks_User_Param');

  /**
   * ID:4
   * Alias: CompletedBy
   * Caption: $Views_CompletedTasks_CompletedBy_Param.
   */
   readonly ParamCompletedBy: ViewObject = new ViewObject(4, 'CompletedBy', '$Views_CompletedTasks_CompletedBy_Param');

  /**
   * ID:5
   * Alias: Role
   * Caption: $Views_CompletedTasks_RoleGroup_Param.
   */
   readonly ParamRole: ViewObject = new ViewObject(5, 'Role', '$Views_CompletedTasks_RoleGroup_Param');

  /**
   * ID:6
   * Alias: Option
   * Caption: $Views_CompletedTasks_CompletionOption_Param.
   */
   readonly ParamOption: ViewObject = new ViewObject(6, 'Option', '$Views_CompletedTasks_CompletionOption_Param');

  /**
   * ID:7
   * Alias: IsDelayed
   * Caption: $Views_CompletedTasks_IsDelayed_Param.
   */
   readonly ParamIsDelayed: ViewObject = new ViewObject(7, 'IsDelayed', '$Views_CompletedTasks_IsDelayed_Param');

  //#endregion
}

//#endregion

//#region CompletionOptionCards

/**
 * ID: {f74f5397-74b2-4b55-8d4e-2cc3031f35af}
 * Alias: CompletionOptionCards
 * Caption: $Views_Names_CompletionOptionCards
 * Group: System
 */
class CompletionOptionCardsViewInfo {
  //#region Common

  /**
   * View identifier for "CompletionOptionCards": {f74f5397-74b2-4b55-8d4e-2cc3031f35af}.
   */
   readonly ID: guid = 'f74f5397-74b2-4b55-8d4e-2cc3031f35af';

  /**
   * View name for "CompletionOptionCards".
   */
   readonly Alias: string = 'CompletionOptionCards';

  /**
   * View caption for "CompletionOptionCards".
   */
   readonly Caption: string = '$Views_Names_CompletionOptionCards';

  /**
   * View group for "CompletionOptionCards".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: OptionID.
   */
   readonly ColumnOptionID: ViewObject = new ViewObject(0, 'OptionID');

  /**
   * ID:1
   * Alias: OptionCaption
   * Caption: $Views_CompletionOptions_Caption.
   */
   readonly ColumnOptionCaption: ViewObject = new ViewObject(1, 'OptionCaption', '$Views_CompletionOptions_Caption');

  /**
   * ID:2
   * Alias: OptionName
   * Caption: $Views_CompletionOptions_Alias.
   */
   readonly ColumnOptionName: ViewObject = new ViewObject(2, 'OptionName', '$Views_CompletionOptions_Alias');

  /**
   * ID:3
   * Alias: PartitionID.
   */
   readonly ColumnPartitionID: ViewObject = new ViewObject(3, 'PartitionID');

  /**
   * ID:4
   * Alias: PartitionName
   * Caption: $Views_CompletionOptions_Partition.
   */
   readonly ColumnPartitionName: ViewObject = new ViewObject(4, 'PartitionName', '$Views_CompletionOptions_Partition');

  /**
   * ID:5
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(5, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: OptionID
   * Caption: OptionID.
   */
   readonly ParamOptionID: ViewObject = new ViewObject(0, 'OptionID', 'OptionID');

  /**
   * ID:1
   * Alias: Caption
   * Caption: $Views_CompletionOptions_Caption_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(1, 'Caption', '$Views_CompletionOptions_Caption_Param');

  /**
   * ID:2
   * Alias: Name
   * Caption: $Views_CompletionOptions_Alias_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(2, 'Name', '$Views_CompletionOptions_Alias_Param');

  /**
   * ID:3
   * Alias: Partition
   * Caption: $Views_CompletionOptions_Partition.
   */
   readonly ParamPartition: ViewObject = new ViewObject(3, 'Partition', '$Views_CompletionOptions_Partition');

  //#endregion
}

//#endregion

//#region CompletionOptions

/**
 * ID: {7aa4bb6b-2bd0-469b-aac4-90c46c2d3502}
 * Alias: CompletionOptions
 * Caption: $Views_Names_CompletionOptions
 * Group: System
 */
class CompletionOptionsViewInfo {
  //#region Common

  /**
   * View identifier for "CompletionOptions": {7aa4bb6b-2bd0-469b-aac4-90c46c2d3502}.
   */
   readonly ID: guid = '7aa4bb6b-2bd0-469b-aac4-90c46c2d3502';

  /**
   * View name for "CompletionOptions".
   */
   readonly Alias: string = 'CompletionOptions';

  /**
   * View caption for "CompletionOptions".
   */
   readonly Caption: string = '$Views_Names_CompletionOptions';

  /**
   * View group for "CompletionOptions".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: OptionID.
   */
   readonly ColumnOptionID: ViewObject = new ViewObject(0, 'OptionID');

  /**
   * ID:1
   * Alias: OptionCaption
   * Caption: $Views_CompletionOptions_Caption.
   */
   readonly ColumnOptionCaption: ViewObject = new ViewObject(1, 'OptionCaption', '$Views_CompletionOptions_Caption');

  /**
   * ID:2
   * Alias: OptionName
   * Caption: $Views_CompletionOptions_Alias.
   */
   readonly ColumnOptionName: ViewObject = new ViewObject(2, 'OptionName', '$Views_CompletionOptions_Alias');

  /**
   * ID:3
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(3, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: OptionID
   * Caption: OptionID.
   */
   readonly ParamOptionID: ViewObject = new ViewObject(0, 'OptionID', 'OptionID');

  /**
   * ID:1
   * Alias: Caption
   * Caption: $Views_CompletionOptions_Caption_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(1, 'Caption', '$Views_CompletionOptions_Caption_Param');

  /**
   * ID:2
   * Alias: Name
   * Caption: $Views_CompletionOptions_Alias_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(2, 'Name', '$Views_CompletionOptions_Alias_Param');

  //#endregion
}

//#endregion

//#region ConditionTypes

/**
 * ID: {ecb69da2-2b28-41dd-b56d-941dd12df77b}
 * Alias: ConditionTypes
 * Caption: $Views_Names_ConditionTypes
 * Group: System
 */
class ConditionTypesViewInfo {
  //#region Common

  /**
   * View identifier for "ConditionTypes": {ecb69da2-2b28-41dd-b56d-941dd12df77b}.
   */
   readonly ID: guid = 'ecb69da2-2b28-41dd-b56d-941dd12df77b';

  /**
   * View name for "ConditionTypes".
   */
   readonly Alias: string = 'ConditionTypes';

  /**
   * View caption for "ConditionTypes".
   */
   readonly Caption: string = '$Views_Names_ConditionTypes';

  /**
   * View group for "ConditionTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ConditionTypeID.
   */
   readonly ColumnConditionTypeID: ViewObject = new ViewObject(0, 'ConditionTypeID');

  /**
   * ID:1
   * Alias: ConditionTypeName
   * Caption: $Views_ConditionTypes_Name.
   */
   readonly ColumnConditionTypeName: ViewObject = new ViewObject(1, 'ConditionTypeName', '$Views_ConditionTypes_Name');

  /**
   * ID:2
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(2, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_ConditionTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_ConditionTypes_Name_Param');

  /**
   * ID:1
   * Alias: UsePlace
   * Caption: $Views_ConditionTypes_UsePlace_Param.
   */
   readonly ParamUsePlace: ViewObject = new ViewObject(1, 'UsePlace', '$Views_ConditionTypes_UsePlace_Param');

  //#endregion
}

//#endregion

//#region ConditionUsePlaces

/**
 * ID: {c0b966a6-aa3a-4ea6-b5ab-a6084099cc1f}
 * Alias: ConditionUsePlaces
 * Caption: $Views_Names_ConditionUsePlaces
 * Group: System
 */
class ConditionUsePlacesViewInfo {
  //#region Common

  /**
   * View identifier for "ConditionUsePlaces": {c0b966a6-aa3a-4ea6-b5ab-a6084099cc1f}.
   */
   readonly ID: guid = 'c0b966a6-aa3a-4ea6-b5ab-a6084099cc1f';

  /**
   * View name for "ConditionUsePlaces".
   */
   readonly Alias: string = 'ConditionUsePlaces';

  /**
   * View caption for "ConditionUsePlaces".
   */
   readonly Caption: string = '$Views_Names_ConditionUsePlaces';

  /**
   * View group for "ConditionUsePlaces".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ConditionUsePlaceID.
   */
   readonly ColumnConditionUsePlaceID: ViewObject = new ViewObject(0, 'ConditionUsePlaceID');

  /**
   * ID:1
   * Alias: ConditionUsePlaceName
   * Caption: $Views_ConditionUsePlaces_Name.
   */
   readonly ColumnConditionUsePlaceName: ViewObject = new ViewObject(1, 'ConditionUsePlaceName', '$Views_ConditionUsePlaces_Name');

  /**
   * ID:2
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(2, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_ConditionUsePlaces_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_ConditionUsePlaces_Name_Param');

  //#endregion
}

//#endregion

//#region ContractsDocuments

/**
 * ID: {24f43f33-9b1b-476d-aa33-3deb11b9fe3b}
 * Alias: ContractsDocuments
 * Caption: $Views_Names_ContractsDocuments
 * Group: KrDocuments
 */
class ContractsDocumentsViewInfo {
  //#region Common

  /**
   * View identifier for "ContractsDocuments": {24f43f33-9b1b-476d-aa33-3deb11b9fe3b}.
   */
   readonly ID: guid = '24f43f33-9b1b-476d-aa33-3deb11b9fe3b';

  /**
   * View name for "ContractsDocuments".
   */
   readonly Alias: string = 'ContractsDocuments';

  /**
   * View caption for "ContractsDocuments".
   */
   readonly Caption: string = '$Views_Names_ContractsDocuments';

  /**
   * View group for "ContractsDocuments".
   */
   readonly Group: string = 'KrDocuments';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DocID.
   */
   readonly ColumnDocID: ViewObject = new ViewObject(0, 'DocID');

  /**
   * ID:1
   * Alias: DocNumber
   * Caption: $Views_Registers_Number.
   */
   readonly ColumnDocNumber: ViewObject = new ViewObject(1, 'DocNumber', '$Views_Registers_Number');

  /**
   * ID:2
   * Alias: SubTypeTitle
   * Caption: $Views_Registers_DocType.
   */
   readonly ColumnSubTypeTitle: ViewObject = new ViewObject(2, 'SubTypeTitle', '$Views_Registers_DocType');

  /**
   * ID:3
   * Alias: DocSubject
   * Caption: $Views_Registers_Subject.
   */
   readonly ColumnDocSubject: ViewObject = new ViewObject(3, 'DocSubject', '$Views_Registers_Subject');

  /**
   * ID:4
   * Alias: DocDescription
   * Caption: $Views_Registers_DocDescription.
   */
   readonly ColumnDocDescription: ViewObject = new ViewObject(4, 'DocDescription', '$Views_Registers_DocDescription');

  /**
   * ID:5
   * Alias: DocAmount
   * Caption: $Views_Registers_Sum.
   */
   readonly ColumnDocAmount: ViewObject = new ViewObject(5, 'DocAmount', '$Views_Registers_Sum');

  /**
   * ID:6
   * Alias: PartnerID.
   */
   readonly ColumnPartnerID: ViewObject = new ViewObject(6, 'PartnerID');

  /**
   * ID:7
   * Alias: PartnerName
   * Caption: $Views_Registers_Partner.
   */
   readonly ColumnPartnerName: ViewObject = new ViewObject(7, 'PartnerName', '$Views_Registers_Partner');

  /**
   * ID:8
   * Alias: AuthorID.
   */
   readonly ColumnAuthorID: ViewObject = new ViewObject(8, 'AuthorID');

  /**
   * ID:9
   * Alias: AuthorName
   * Caption: $Views_Registers_Author.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(9, 'AuthorName', '$Views_Registers_Author');

  /**
   * ID:10
   * Alias: RegistratorID.
   */
   readonly ColumnRegistratorID: ViewObject = new ViewObject(10, 'RegistratorID');

  /**
   * ID:11
   * Alias: RegistratorName
   * Caption: $Views_Registers_Registrator.
   */
   readonly ColumnRegistratorName: ViewObject = new ViewObject(11, 'RegistratorName', '$Views_Registers_Registrator');

  /**
   * ID:12
   * Alias: KrState
   * Caption: $Views_Registers_State.
   */
   readonly ColumnKrState: ViewObject = new ViewObject(12, 'KrState', '$Views_Registers_State');

  /**
   * ID:13
   * Alias: DocDate
   * Caption: $Views_Registers_DocDate.
   */
   readonly ColumnDocDate: ViewObject = new ViewObject(13, 'DocDate', '$Views_Registers_DocDate');

  /**
   * ID:14
   * Alias: CreationDate
   * Caption: $Views_Registers_CreationDate.
   */
   readonly ColumnCreationDate: ViewObject = new ViewObject(14, 'CreationDate', '$Views_Registers_CreationDate');

  /**
   * ID:15
   * Alias: Department
   * Caption: $Views_Registers_Department.
   */
   readonly ColumnDepartment: ViewObject = new ViewObject(15, 'Department', '$Views_Registers_Department');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: IsAuthor
   * Caption: $Views_Registers_IsAuthor_Param.
   */
   readonly ParamIsAuthor: ViewObject = new ViewObject(0, 'IsAuthor', '$Views_Registers_IsAuthor_Param');

  /**
   * ID:1
   * Alias: IsInitiator
   * Caption: $Views_Registers_IsInitiator_Param.
   */
   readonly ParamIsInitiator: ViewObject = new ViewObject(1, 'IsInitiator', '$Views_Registers_IsInitiator_Param');

  /**
   * ID:2
   * Alias: IsRegistrator
   * Caption: $Views_Registers_IsRegistrator_Param.
   */
   readonly ParamIsRegistrator: ViewObject = new ViewObject(2, 'IsRegistrator', '$Views_Registers_IsRegistrator_Param');

  /**
   * ID:3
   * Alias: Partner
   * Caption: $Views_Registers_Partner_Param.
   */
   readonly ParamPartner: ViewObject = new ViewObject(3, 'Partner', '$Views_Registers_Partner_Param');

  /**
   * ID:4
   * Alias: Number
   * Caption: $Views_Registers_Number_Param.
   */
   readonly ParamNumber: ViewObject = new ViewObject(4, 'Number', '$Views_Registers_Number_Param');

  /**
   * ID:5
   * Alias: Subject
   * Caption: $Views_Registers_Subject_Param.
   */
   readonly ParamSubject: ViewObject = new ViewObject(5, 'Subject', '$Views_Registers_Subject_Param');

  /**
   * ID:6
   * Alias: DocDate
   * Caption: $Views_Registers_DocDate_Param.
   */
   readonly ParamDocDate: ViewObject = new ViewObject(6, 'DocDate', '$Views_Registers_DocDate_Param');

  /**
   * ID:7
   * Alias: Amount
   * Caption: $Views_Registers_Sum_Param.
   */
   readonly ParamAmount: ViewObject = new ViewObject(7, 'Amount', '$Views_Registers_Sum_Param');

  /**
   * ID:8
   * Alias: Currency
   * Caption: $Views_Registers_Currency_Param.
   */
   readonly ParamCurrency: ViewObject = new ViewObject(8, 'Currency', '$Views_Registers_Currency_Param');

  /**
   * ID:9
   * Alias: Author
   * Caption: $Views_Registers_Author_Param.
   */
   readonly ParamAuthor: ViewObject = new ViewObject(9, 'Author', '$Views_Registers_Author_Param');

  /**
   * ID:10
   * Alias: Registrator
   * Caption: $Views_Registers_Registrator_Param.
   */
   readonly ParamRegistrator: ViewObject = new ViewObject(10, 'Registrator', '$Views_Registers_Registrator_Param');

  /**
   * ID:11
   * Alias: State
   * Caption: $Views_Registers_State_Param.
   */
   readonly ParamState: ViewObject = new ViewObject(11, 'State', '$Views_Registers_State_Param');

  /**
   * ID:12
   * Alias: DocType
   * Caption: $Views_Registers_DocType_Param.
   */
   readonly ParamDocType: ViewObject = new ViewObject(12, 'DocType', '$Views_Registers_DocType_Param');

  /**
   * ID:13
   * Alias: Department
   * Caption: $Views_Registers_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(13, 'Department', '$Views_Registers_Department_Param');

  //#endregion
}

//#endregion

//#region CreateFileFromTemplate

/**
 * ID: {9334eab6-f2b7-4c35-b0ff-bf764cd0092c}
 * Alias: CreateFileFromTemplate
 * Caption: $Views_Names_CreateFileFromTemplate
 * Group: System
 */
class CreateFileFromTemplateViewInfo {
  //#region Common

  /**
   * View identifier for "CreateFileFromTemplate": {9334eab6-f2b7-4c35-b0ff-bf764cd0092c}.
   */
   readonly ID: guid = '9334eab6-f2b7-4c35-b0ff-bf764cd0092c';

  /**
   * View name for "CreateFileFromTemplate".
   */
   readonly Alias: string = 'CreateFileFromTemplate';

  /**
   * View caption for "CreateFileFromTemplate".
   */
   readonly Caption: string = '$Views_Names_CreateFileFromTemplate';

  /**
   * View group for "CreateFileFromTemplate".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: FileTemplateID.
   */
   readonly ColumnFileTemplateID: ViewObject = new ViewObject(0, 'FileTemplateID');

  /**
   * ID:1
   * Alias: FileTemplateName
   * Caption: $Cards_FileFromTemplate_TemplateNameColumn.
   */
   readonly ColumnFileTemplateName: ViewObject = new ViewObject(1, 'FileTemplateName', '$Cards_FileFromTemplate_TemplateNameColumn');

  /**
   * ID:2
   * Alias: FileTemplateFileExtension.
   */
   readonly ColumnFileTemplateFileExtension: ViewObject = new ViewObject(2, 'FileTemplateFileExtension');

  /**
   * ID:3
   * Alias: FileTemplateFileExtensionText
   * Caption: $Cards_FileFromTemplate_TemplateExtensionColumn.
   */
   readonly ColumnFileTemplateFileExtensionText: ViewObject = new ViewObject(3, 'FileTemplateFileExtensionText', '$Cards_FileFromTemplate_TemplateExtensionColumn');

  /**
   * ID:4
   * Alias: FileTemplateFileName.
   */
   readonly ColumnFileTemplateFileName: ViewObject = new ViewObject(4, 'FileTemplateFileName');

  /**
   * ID:5
   * Alias: FileTemplateGroupName
   * Caption: $Cards_FileFromTemplate_TemplateGroupNameColumn.
   */
   readonly ColumnFileTemplateGroupName: ViewObject = new ViewObject(5, 'FileTemplateGroupName', '$Cards_FileFromTemplate_TemplateGroupNameColumn');

  /**
   * ID:6
   * Alias: ConvertToPdf.
   */
   readonly ColumnConvertToPdf: ViewObject = new ViewObject(6, 'ConvertToPdf');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name');

  //#endregion
}

//#endregion

//#region Currencies

/**
 * ID: {67e0e026-8dbd-462a-93fa-9ec03636564f}
 * Alias: Currencies
 * Caption: $Views_Names_Currencies
 * Group: System
 */
class CurrenciesViewInfo {
  //#region Common

  /**
   * View identifier for "Currencies": {67e0e026-8dbd-462a-93fa-9ec03636564f}.
   */
   readonly ID: guid = '67e0e026-8dbd-462a-93fa-9ec03636564f';

  /**
   * View name for "Currencies".
   */
   readonly Alias: string = 'Currencies';

  /**
   * View caption for "Currencies".
   */
   readonly Caption: string = '$Views_Names_Currencies';

  /**
   * View group for "Currencies".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CurrencyID.
   */
   readonly ColumnCurrencyID: ViewObject = new ViewObject(0, 'CurrencyID');

  /**
   * ID:1
   * Alias: CurrencyName
   * Caption: $Views_Currencies_Name.
   */
   readonly ColumnCurrencyName: ViewObject = new ViewObject(1, 'CurrencyName', '$Views_Currencies_Name');

  /**
   * ID:2
   * Alias: CurrencyCaption
   * Caption: $Views_Currencies_Caption.
   */
   readonly ColumnCurrencyCaption: ViewObject = new ViewObject(2, 'CurrencyCaption', '$Views_Currencies_Caption');

  /**
   * ID:3
   * Alias: CurrencyCode
   * Caption: $Views_Currencies_Code.
   */
   readonly ColumnCurrencyCode: ViewObject = new ViewObject(3, 'CurrencyCode', '$Views_Currencies_Code');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Currencies_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Currencies_Name_Param');

  //#endregion
}

//#endregion

//#region DateFormats

/**
 * ID: {10ad5b14-16cd-4c8c-ad1f-63c24daeb00c}
 * Alias: DateFormats
 * Caption: $Views_Names_DateFormats
 * Group: System
 */
class DateFormatsViewInfo {
  //#region Common

  /**
   * View identifier for "DateFormats": {10ad5b14-16cd-4c8c-ad1f-63c24daeb00c}.
   */
   readonly ID: guid = '10ad5b14-16cd-4c8c-ad1f-63c24daeb00c';

  /**
   * View name for "DateFormats".
   */
   readonly Alias: string = 'DateFormats';

  /**
   * View caption for "DateFormats".
   */
   readonly Caption: string = '$Views_Names_DateFormats';

  /**
   * View group for "DateFormats".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DateFormatID.
   */
   readonly ColumnDateFormatID: ViewObject = new ViewObject(0, 'DateFormatID');

  /**
   * ID:1
   * Alias: DateFormatName.
   */
   readonly ColumnDateFormatName: ViewObject = new ViewObject(1, 'DateFormatName');

  /**
   * ID:2
   * Alias: DateFormatCaption
   * Caption: $Views_DateFormats_Caption.
   */
   readonly ColumnDateFormatCaption: ViewObject = new ViewObject(2, 'DateFormatCaption', '$Views_DateFormats_Caption');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_DateFormats_Caption_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_DateFormats_Caption_Param');

  //#endregion
}

//#endregion

//#region Deleted

/**
 * ID: {52c2fe9f-b0a8-455a-b426-ab5bc2285a05}
 * Alias: Deleted
 * Caption: $Views_Names_Deleted
 * Group: System
 */
class DeletedViewInfo {
  //#region Common

  /**
   * View identifier for "Deleted": {52c2fe9f-b0a8-455a-b426-ab5bc2285a05}.
   */
   readonly ID: guid = '52c2fe9f-b0a8-455a-b426-ab5bc2285a05';

  /**
   * View name for "Deleted".
   */
   readonly Alias: string = 'Deleted';

  /**
   * View caption for "Deleted".
   */
   readonly Caption: string = '$Views_Names_Deleted';

  /**
   * View group for "Deleted".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DeletedID.
   */
   readonly ColumnDeletedID: ViewObject = new ViewObject(0, 'DeletedID');

  /**
   * ID:1
   * Alias: DeletedCaption
   * Caption: $Views_Deleted_Caption.
   */
   readonly ColumnDeletedCaption: ViewObject = new ViewObject(1, 'DeletedCaption', '$Views_Deleted_Caption');

  /**
   * ID:2
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(2, 'TypeID');

  /**
   * ID:3
   * Alias: TypeCaption
   * Caption: $Views_Deleted_Type.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(3, 'TypeCaption', '$Views_Deleted_Type');

  /**
   * ID:4
   * Alias: Date
   * Caption: $Views_Deleted_Date.
   */
   readonly ColumnDate: ViewObject = new ViewObject(4, 'Date', '$Views_Deleted_Date');

  /**
   * ID:5
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(5, 'UserID');

  /**
   * ID:6
   * Alias: UserName
   * Caption: $Views_Deleted_User.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(6, 'UserName', '$Views_Deleted_User');

  /**
   * ID:7
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(7, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: DeletedCaption
   * Caption: $Views_Deleted_Card_Param.
   */
   readonly ParamDeletedCaption: ViewObject = new ViewObject(0, 'DeletedCaption', '$Views_Deleted_Card_Param');

  /**
   * ID:1
   * Alias: TypeID
   * Caption: $Views_Deleted_Type_Param.
   */
   readonly ParamTypeID: ViewObject = new ViewObject(1, 'TypeID', '$Views_Deleted_Type_Param');

  /**
   * ID:2
   * Alias: TypeCaption
   * Caption: $Views_Deleted_TypeName_Param.
   */
   readonly ParamTypeCaption: ViewObject = new ViewObject(2, 'TypeCaption', '$Views_Deleted_TypeName_Param');

  /**
   * ID:3
   * Alias: Date
   * Caption: $Views_Deleted_Date_Param.
   */
   readonly ParamDate: ViewObject = new ViewObject(3, 'Date', '$Views_Deleted_Date_Param');

  /**
   * ID:4
   * Alias: UserID
   * Caption: $Views_Deleted_User_Param.
   */
   readonly ParamUserID: ViewObject = new ViewObject(4, 'UserID', '$Views_Deleted_User_Param');

  /**
   * ID:5
   * Alias: UserName
   * Caption: $Views_Deleted_UserName_Param.
   */
   readonly ParamUserName: ViewObject = new ViewObject(5, 'UserName', '$Views_Deleted_UserName_Param');

  //#endregion
}

//#endregion

//#region Departments

/**
 * ID: {ab58bf23-b9d7-4b51-97c1-c9517daa7993}
 * Alias: Departments
 * Caption: $Views_Names_Departments
 * Group: System
 */
class DepartmentsViewInfo {
  //#region Common

  /**
   * View identifier for "Departments": {ab58bf23-b9d7-4b51-97c1-c9517daa7993}.
   */
   readonly ID: guid = 'ab58bf23-b9d7-4b51-97c1-c9517daa7993';

  /**
   * View name for "Departments".
   */
   readonly Alias: string = 'Departments';

  /**
   * View caption for "Departments".
   */
   readonly Caption: string = '$Views_Names_Departments';

  /**
   * View group for "Departments".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RoleID.
   */
   readonly ColumnRoleID: ViewObject = new ViewObject(0, 'RoleID');

  /**
   * ID:1
   * Alias: RoleName
   * Caption: $Views_Departments_Department.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(1, 'RoleName', '$Views_Departments_Department');

  /**
   * ID:2
   * Alias: HeadUserID.
   */
   readonly ColumnHeadUserID: ViewObject = new ViewObject(2, 'HeadUserID');

  /**
   * ID:3
   * Alias: HeadUserName
   * Caption: $Views_Departments_HeadUser.
   */
   readonly ColumnHeadUserName: ViewObject = new ViewObject(3, 'HeadUserName', '$Views_Departments_HeadUser');

  /**
   * ID:4
   * Alias: ParentRoleID.
   */
   readonly ColumnParentRoleID: ViewObject = new ViewObject(4, 'ParentRoleID');

  /**
   * ID:5
   * Alias: ParentRoleName
   * Caption: $Views_Departments_ParentDepartment.
   */
   readonly ColumnParentRoleName: ViewObject = new ViewObject(5, 'ParentRoleName', '$Views_Departments_ParentDepartment');

  /**
   * ID:6
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(6, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Departments_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Departments_Name_Param');

  /**
   * ID:1
   * Alias: UserName
   * Caption: $Views_Departments_User_Param.
   */
   readonly ParamUserName: ViewObject = new ViewObject(1, 'UserName', '$Views_Departments_User_Param');

  /**
   * ID:2
   * Alias: DepID
   * Caption: $Views_Departments_Department_Param.
   */
   readonly ParamDepID: ViewObject = new ViewObject(2, 'DepID', '$Views_Departments_Department_Param');

  /**
   * ID:3
   * Alias: ParentDepID
   * Caption: $Views_Departments_ParentDep_Param.
   */
   readonly ParamParentDepID: ViewObject = new ViewObject(3, 'ParentDepID', '$Views_Departments_ParentDep_Param');

  /**
   * ID:4
   * Alias: ShowHidden
   * Caption: $Views_Departments_ShowHidden_Param.
   */
   readonly ParamShowHidden: ViewObject = new ViewObject(4, 'ShowHidden', '$Views_Departments_ShowHidden_Param');

  //#endregion
}

//#endregion

//#region DeputiesManagement

/**
 * ID: {4c2769bc-89ca-4b08-bc53-d6ee93a45b95}
 * Alias: DeputiesManagement
 * Caption: $Views_Names_DeputiesManagement
 * Group: System
 */
class DeputiesManagementViewInfo {
  //#region Common

  /**
   * View identifier for "DeputiesManagement": {4c2769bc-89ca-4b08-bc53-d6ee93a45b95}.
   */
   readonly ID: guid = '4c2769bc-89ca-4b08-bc53-d6ee93a45b95';

  /**
   * View name for "DeputiesManagement".
   */
   readonly Alias: string = 'DeputiesManagement';

  /**
   * View caption for "DeputiesManagement".
   */
   readonly Caption: string = '$Views_Names_DeputiesManagement';

  /**
   * View group for "DeputiesManagement".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(0, 'UserID');

  /**
   * ID:1
   * Alias: UserName
   * Caption: $Views_Users_Name.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(1, 'UserName', '$Views_Users_Name');

  /**
   * ID:2
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(2, 'rn');

  /**
   * ID:3
   * Alias: RecordID.
   */
   readonly ColumnRecordID: ViewObject = new ViewObject(3, 'RecordID');

  /**
   * ID:4
   * Alias: RecordCaption.
   */
   readonly ColumnRecordCaption: ViewObject = new ViewObject(4, 'RecordCaption');

  /**
   * ID:5
   * Alias: Departments
   * Caption: $Views_Users_Departments.
   */
   readonly ColumnDepartments: ViewObject = new ViewObject(5, 'Departments', '$Views_Users_Departments');

  /**
   * ID:6
   * Alias: StaticRoles
   * Caption: $Views_Users_StaticRoles.
   */
   readonly ColumnStaticRoles: ViewObject = new ViewObject(6, 'StaticRoles', '$Views_Users_StaticRoles');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: UserID
   * Caption: $Views_DeputiesManagement_User_Param.
   */
   readonly ParamUserID: ViewObject = new ViewObject(0, 'UserID', '$Views_DeputiesManagement_User_Param');

  /**
   * ID:1
   * Alias: UserName
   * Caption: $Views_Users_Name_Param.
   */
   readonly ParamUserName: ViewObject = new ViewObject(1, 'UserName', '$Views_Users_Name_Param');

  //#endregion
}

//#endregion

//#region DeviceTypes

/**
 * ID: {4a9aaa12-6830-4dc5-bd0d-c31415f7a306}
 * Alias: DeviceTypes
 * Caption: $Views_Names_DeviceTypes
 * Group: System
 */
class DeviceTypesViewInfo {
  //#region Common

  /**
   * View identifier for "DeviceTypes": {4a9aaa12-6830-4dc5-bd0d-c31415f7a306}.
   */
   readonly ID: guid = '4a9aaa12-6830-4dc5-bd0d-c31415f7a306';

  /**
   * View name for "DeviceTypes".
   */
   readonly Alias: string = 'DeviceTypes';

  /**
   * View caption for "DeviceTypes".
   */
   readonly Caption: string = '$Views_Names_DeviceTypes';

  /**
   * View group for "DeviceTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: $Views_DeviceTypes_Name.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', '$Views_DeviceTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_DeviceTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_DeviceTypes_Name_Param');

  //#endregion
}

//#endregion

//#region DialogButtonTypes

/**
 * ID: {bf4ac076-a8b3-4271-867e-f8f7ae9287a6}
 * Alias: DialogButtonTypes
 * Caption: $Views_Names_DialogButtonTypes
 * Group: System
 */
class DialogButtonTypesViewInfo {
  //#region Common

  /**
   * View identifier for "DialogButtonTypes": {bf4ac076-a8b3-4271-867e-f8f7ae9287a6}.
   */
   readonly ID: guid = 'bf4ac076-a8b3-4271-867e-f8f7ae9287a6';

  /**
   * View name for "DialogButtonTypes".
   */
   readonly Alias: string = 'DialogButtonTypes';

  /**
   * View caption for "DialogButtonTypes".
   */
   readonly Caption: string = '$Views_Names_DialogButtonTypes';

  /**
   * View group for "DialogButtonTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: RefName.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', 'RefName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', 'Name');

  //#endregion
}

//#endregion

//#region DialogCardAutoOpenModes

/**
 * ID: {115854cc-71a8-45ae-ab4f-940962b332c6}
 * Alias: DialogCardAutoOpenModes
 * Caption: $Views_Names_DialogCardAutoOpenModes
 * Group: System
 */
class DialogCardAutoOpenModesViewInfo {
  //#region Common

  /**
   * View identifier for "DialogCardAutoOpenModes": {115854cc-71a8-45ae-ab4f-940962b332c6}.
   */
   readonly ID: guid = '115854cc-71a8-45ae-ab4f-940962b332c6';

  /**
   * View name for "DialogCardAutoOpenModes".
   */
   readonly Alias: string = 'DialogCardAutoOpenModes';

  /**
   * View caption for "DialogCardAutoOpenModes".
   */
   readonly Caption: string = '$Views_Names_DialogCardAutoOpenModes';

  /**
   * View group for "DialogCardAutoOpenModes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: RefName.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', 'RefName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', 'Name');

  //#endregion
}

//#endregion

//#region DialogCardStoreModes

/**
 * ID: {ad759faa-bfc1-4cd1-a322-f1eb1a42b3bc}
 * Alias: DialogCardStoreModes
 * Caption: $Views_Names_DialogCardStoreModes
 * Group: System
 */
class DialogCardStoreModesViewInfo {
  //#region Common

  /**
   * View identifier for "DialogCardStoreModes": {ad759faa-bfc1-4cd1-a322-f1eb1a42b3bc}.
   */
   readonly ID: guid = 'ad759faa-bfc1-4cd1-a322-f1eb1a42b3bc';

  /**
   * View name for "DialogCardStoreModes".
   */
   readonly Alias: string = 'DialogCardStoreModes';

  /**
   * View caption for "DialogCardStoreModes".
   */
   readonly Caption: string = '$Views_Names_DialogCardStoreModes';

  /**
   * View group for "DialogCardStoreModes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: RefName.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', 'RefName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', 'Name');

  //#endregion
}

//#endregion

//#region DocumentCategories

/**
 * ID: {15fc8ec2-f206-4de1-b942-1c29c931213f}
 * Alias: DocumentCategories
 * Caption: $Views_Names_DocumentCategories
 * Group: System
 */
class DocumentCategoriesViewInfo {
  //#region Common

  /**
   * View identifier for "DocumentCategories": {15fc8ec2-f206-4de1-b942-1c29c931213f}.
   */
   readonly ID: guid = '15fc8ec2-f206-4de1-b942-1c29c931213f';

  /**
   * View name for "DocumentCategories".
   */
   readonly Alias: string = 'DocumentCategories';

  /**
   * View caption for "DocumentCategories".
   */
   readonly Caption: string = '$Views_Names_DocumentCategories';

  /**
   * View group for "DocumentCategories".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CategoryID.
   */
   readonly ColumnCategoryID: ViewObject = new ViewObject(0, 'CategoryID');

  /**
   * ID:1
   * Alias: CategoryName
   * Caption: $Views_DocumentCategories_Name.
   */
   readonly ColumnCategoryName: ViewObject = new ViewObject(1, 'CategoryName', '$Views_DocumentCategories_Name');

  /**
   * ID:2
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(2, 'TypeID');

  /**
   * ID:3
   * Alias: TypeCaption
   * Caption: $Views_DocumentCategories_Type.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(3, 'TypeCaption', '$Views_DocumentCategories_Type');

  /**
   * ID:4
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(4, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_DocumentCategories_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_DocumentCategories_Name_Param');

  /**
   * ID:1
   * Alias: Type
   * Caption: $Views_DocumentCategories_Type_Param.
   */
   readonly ParamType: ViewObject = new ViewObject(1, 'Type', '$Views_DocumentCategories_Type_Param');

  /**
   * ID:2
   * Alias: TypeID
   * Caption: $Views_DocumentCategories_Type_Param.
   */
   readonly ParamTypeID: ViewObject = new ViewObject(2, 'TypeID', '$Views_DocumentCategories_Type_Param');

  //#endregion
}

//#endregion

//#region Documents

/**
 * ID: {8354ee75-639a-4084-808d-cf97a2b86be9}
 * Alias: Documents
 * Caption: $Views_Names_Documents
 * Group: KrDocuments
 */
class DocumentsViewInfo {
  //#region Common

  /**
   * View identifier for "Documents": {8354ee75-639a-4084-808d-cf97a2b86be9}.
   */
   readonly ID: guid = '8354ee75-639a-4084-808d-cf97a2b86be9';

  /**
   * View name for "Documents".
   */
   readonly Alias: string = 'Documents';

  /**
   * View caption for "Documents".
   */
   readonly Caption: string = '$Views_Names_Documents';

  /**
   * View group for "Documents".
   */
   readonly Group: string = 'KrDocuments';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DocID.
   */
   readonly ColumnDocID: ViewObject = new ViewObject(0, 'DocID');

  /**
   * ID:1
   * Alias: DocNumber
   * Caption: $Views_Registers_Number.
   */
   readonly ColumnDocNumber: ViewObject = new ViewObject(1, 'DocNumber', '$Views_Registers_Number');

  /**
   * ID:2
   * Alias: SubTypeTitle
   * Caption: $Views_Registers_DocType.
   */
   readonly ColumnSubTypeTitle: ViewObject = new ViewObject(2, 'SubTypeTitle', '$Views_Registers_DocType');

  /**
   * ID:3
   * Alias: DocSubject
   * Caption: $Views_Registers_Subject.
   */
   readonly ColumnDocSubject: ViewObject = new ViewObject(3, 'DocSubject', '$Views_Registers_Subject');

  /**
   * ID:4
   * Alias: DocDescription
   * Caption: $Views_Registers_DocDescription.
   */
   readonly ColumnDocDescription: ViewObject = new ViewObject(4, 'DocDescription', '$Views_Registers_DocDescription');

  /**
   * ID:5
   * Alias: AuthorID.
   */
   readonly ColumnAuthorID: ViewObject = new ViewObject(5, 'AuthorID');

  /**
   * ID:6
   * Alias: AuthorName
   * Caption: $Views_Registers_Author.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(6, 'AuthorName', '$Views_Registers_Author');

  /**
   * ID:7
   * Alias: RegistratorID.
   */
   readonly ColumnRegistratorID: ViewObject = new ViewObject(7, 'RegistratorID');

  /**
   * ID:8
   * Alias: RegistratorName
   * Caption: $Views_Registers_Registrator.
   */
   readonly ColumnRegistratorName: ViewObject = new ViewObject(8, 'RegistratorName', '$Views_Registers_Registrator');

  /**
   * ID:9
   * Alias: KrState
   * Caption: $Views_Registers_State.
   */
   readonly ColumnKrState: ViewObject = new ViewObject(9, 'KrState', '$Views_Registers_State');

  /**
   * ID:10
   * Alias: DocDate
   * Caption: $Views_Registers_DocDate.
   */
   readonly ColumnDocDate: ViewObject = new ViewObject(10, 'DocDate', '$Views_Registers_DocDate');

  /**
   * ID:11
   * Alias: CreationDate
   * Caption: $Views_Registers_CreationDate.
   */
   readonly ColumnCreationDate: ViewObject = new ViewObject(11, 'CreationDate', '$Views_Registers_CreationDate');

  /**
   * ID:12
   * Alias: Department
   * Caption: $Views_Registers_Department.
   */
   readonly ColumnDepartment: ViewObject = new ViewObject(12, 'Department', '$Views_Registers_Department');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: IsAuthor
   * Caption: $Views_Registers_IsAuthor_Param.
   */
   readonly ParamIsAuthor: ViewObject = new ViewObject(0, 'IsAuthor', '$Views_Registers_IsAuthor_Param');

  /**
   * ID:1
   * Alias: IsInitiator
   * Caption: $Views_Registers_IsInitiator_Param.
   */
   readonly ParamIsInitiator: ViewObject = new ViewObject(1, 'IsInitiator', '$Views_Registers_IsInitiator_Param');

  /**
   * ID:2
   * Alias: IsRegistrator
   * Caption: $Views_Registers_IsRegistrator_Param.
   */
   readonly ParamIsRegistrator: ViewObject = new ViewObject(2, 'IsRegistrator', '$Views_Registers_IsRegistrator_Param');

  /**
   * ID:3
   * Alias: Number
   * Caption: $Views_Registers_Number_Param.
   */
   readonly ParamNumber: ViewObject = new ViewObject(3, 'Number', '$Views_Registers_Number_Param');

  /**
   * ID:4
   * Alias: Subject
   * Caption: $Views_Registers_Subject_Param.
   */
   readonly ParamSubject: ViewObject = new ViewObject(4, 'Subject', '$Views_Registers_Subject_Param');

  /**
   * ID:5
   * Alias: DocDate
   * Caption: $Views_Registers_DocDate_Param.
   */
   readonly ParamDocDate: ViewObject = new ViewObject(5, 'DocDate', '$Views_Registers_DocDate_Param');

  /**
   * ID:6
   * Alias: Author
   * Caption: $Views_Registers_Author_Param.
   */
   readonly ParamAuthor: ViewObject = new ViewObject(6, 'Author', '$Views_Registers_Author_Param');

  /**
   * ID:7
   * Alias: Registrator
   * Caption: $Views_Registers_Registrator_Param.
   */
   readonly ParamRegistrator: ViewObject = new ViewObject(7, 'Registrator', '$Views_Registers_Registrator_Param');

  /**
   * ID:8
   * Alias: State
   * Caption: $Views_Registers_State_Param.
   */
   readonly ParamState: ViewObject = new ViewObject(8, 'State', '$Views_Registers_State_Param');

  /**
   * ID:9
   * Alias: DocType
   * Caption: $Views_Registers_DocType_Param.
   */
   readonly ParamDocType: ViewObject = new ViewObject(9, 'DocType', '$Views_Registers_DocType_Param');

  /**
   * ID:10
   * Alias: Department
   * Caption: $Views_Registers_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(10, 'Department', '$Views_Registers_Department_Param');

  //#endregion
}

//#endregion

//#region DocumentTypes

/**
 * ID: {b05eebcc-eb4b-4f5c-b4b8-d8bd134e27c6}
 * Alias: DocumentTypes
 * Caption: $Views_Names_DocumentTypes
 * Group: System
 */
class DocumentTypesViewInfo {
  //#region Common

  /**
   * View identifier for "DocumentTypes": {b05eebcc-eb4b-4f5c-b4b8-d8bd134e27c6}.
   */
   readonly ID: guid = 'b05eebcc-eb4b-4f5c-b4b8-d8bd134e27c6';

  /**
   * View name for "DocumentTypes".
   */
   readonly Alias: string = 'DocumentTypes';

  /**
   * View caption for "DocumentTypes".
   */
   readonly Caption: string = '$Views_Names_DocumentTypes';

  /**
   * View group for "DocumentTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeCaption
   * Caption: $View_DocumentTypes_Caption.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(1, 'TypeCaption', '$View_DocumentTypes_Caption');

  /**
   * ID:2
   * Alias: TypeName
   * Caption: $View_DocumentTypes_Name.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(2, 'TypeName', '$View_DocumentTypes_Name');

  /**
   * ID:3
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(3, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $View_DocumentTypes_Caption_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$View_DocumentTypes_Caption_Param');

  /**
   * ID:1
   * Alias: Name
   * Caption: $View_DocumentTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$View_DocumentTypes_Name_Param');

  //#endregion
}

//#endregion

//#region DurableRoles

/**
 * ID: {8144d12b-ac9b-4da7-a21c-4ad1ca355dbe}
 * Alias: DurableRoles
 * Caption: $Views_Names_DurableRoles
 * Group: System
 */
class DurableRolesViewInfo {
  //#region Common

  /**
   * View identifier for "DurableRoles": {8144d12b-ac9b-4da7-a21c-4ad1ca355dbe}.
   */
   readonly ID: guid = '8144d12b-ac9b-4da7-a21c-4ad1ca355dbe';

  /**
   * View name for "DurableRoles".
   */
   readonly Alias: string = 'DurableRoles';

  /**
   * View caption for "DurableRoles".
   */
   readonly Caption: string = '$Views_Names_DurableRoles';

  /**
   * View group for "DurableRoles".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RoleID.
   */
   readonly ColumnRoleID: ViewObject = new ViewObject(0, 'RoleID');

  /**
   * ID:1
   * Alias: RoleName
   * Caption: $Views_Roles_Role.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(1, 'RoleName', '$Views_Roles_Role');

  /**
   * ID:2
   * Alias: TypeName
   * Caption: $Views_Roles_Type.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(2, 'TypeName', '$Views_Roles_Type');

  /**
   * ID:3
   * Alias: Info
   * Caption: $Views_Roles_Info.
   */
   readonly ColumnInfo: ViewObject = new ViewObject(3, 'Info', '$Views_Roles_Info');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Roles_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Roles_Name_Param');

  /**
   * ID:1
   * Alias: TypeID
   * Caption: $Views_Roles_Type_Param.
   */
   readonly ParamTypeID: ViewObject = new ViewObject(1, 'TypeID', '$Views_Roles_Type_Param');

  /**
   * ID:2
   * Alias: ShowHidden
   * Caption: $Views_Roles_ShowHidden_Param.
   */
   readonly ParamShowHidden: ViewObject = new ViewObject(2, 'ShowHidden', '$Views_Roles_ShowHidden_Param');

  //#endregion
}

//#endregion

//#region EdsManagers

/**
 * ID: {18d94578-ee8e-49f7-9fbb-50b3ad3bf76b}
 * Alias: EdsManagers
 * Caption: $Views_Names_EdsManagers
 * Group: System
 */
class EdsManagersViewInfo {
  //#region Common

  /**
   * View identifier for "EdsManagers": {18d94578-ee8e-49f7-9fbb-50b3ad3bf76b}.
   */
   readonly ID: guid = '18d94578-ee8e-49f7-9fbb-50b3ad3bf76b';

  /**
   * View name for "EdsManagers".
   */
   readonly Alias: string = 'EdsManagers';

  /**
   * View caption for "EdsManagers".
   */
   readonly Caption: string = '$Views_Names_EdsManagers';

  /**
   * View group for "EdsManagers".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: EdsName.
   */
   readonly ColumnEdsName: ViewObject = new ViewObject(0, 'EdsName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', 'Name');

  //#endregion
}

//#endregion

//#region EmittedTasks

/**
 * ID: {b6e14161-038f-4060-bd35-66ba13da2cb8}
 * Alias: EmittedTasks
 * Caption: $Views_Names_EmittedTasks
 * Group: System
 */
class EmittedTasksViewInfo {
  //#region Common

  /**
   * View identifier for "EmittedTasks": {b6e14161-038f-4060-bd35-66ba13da2cb8}.
   */
   readonly ID: guid = 'b6e14161-038f-4060-bd35-66ba13da2cb8';

  /**
   * View name for "EmittedTasks".
   */
   readonly Alias: string = 'EmittedTasks';

  /**
   * View caption for "EmittedTasks".
   */
   readonly Caption: string = '$Views_Names_EmittedTasks';

  /**
   * View group for "EmittedTasks".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(0, 'CardID');

  /**
   * ID:1
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(1, 'TypeID');

  /**
   * ID:2
   * Alias: TypeCaption
   * Caption: $Views_EmittedTasks_TaskType.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(2, 'TypeCaption', '$Views_EmittedTasks_TaskType');

  /**
   * ID:3
   * Alias: StateID.
   */
   readonly ColumnStateID: ViewObject = new ViewObject(3, 'StateID');

  /**
   * ID:4
   * Alias: StateName
   * Caption: $Views_EmittedTasks_State.
   */
   readonly ColumnStateName: ViewObject = new ViewObject(4, 'StateName', '$Views_EmittedTasks_State');

  /**
   * ID:5
   * Alias: RoleID.
   */
   readonly ColumnRoleID: ViewObject = new ViewObject(5, 'RoleID');

  /**
   * ID:6
   * Alias: RoleName
   * Caption: $Views_EmittedTasks_Performer.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(6, 'RoleName', '$Views_EmittedTasks_Performer');

  /**
   * ID:7
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(7, 'UserID');

  /**
   * ID:8
   * Alias: UserName
   * Caption: $Views_EmittedTasks_User.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(8, 'UserName', '$Views_EmittedTasks_User');

  /**
   * ID:9
   * Alias: CardName
   * Caption: $Views_EmittedTasks_Card.
   */
   readonly ColumnCardName: ViewObject = new ViewObject(9, 'CardName', '$Views_EmittedTasks_Card');

  /**
   * ID:10
   * Alias: CardTypeID.
   */
   readonly ColumnCardTypeID: ViewObject = new ViewObject(10, 'CardTypeID');

  /**
   * ID:11
   * Alias: CardTypeName
   * Caption: $Views_EmittedTasks_CardType.
   */
   readonly ColumnCardTypeName: ViewObject = new ViewObject(11, 'CardTypeName', '$Views_EmittedTasks_CardType');

  /**
   * ID:12
   * Alias: PlannedDate
   * Caption: $Views_EmittedTasks_Planned.
   */
   readonly ColumnPlannedDate: ViewObject = new ViewObject(12, 'PlannedDate', '$Views_EmittedTasks_Planned');

  /**
   * ID:13
   * Alias: ModificationTime
   * Caption: $Views_EmittedTasks_Modification.
   */
   readonly ColumnModificationTime: ViewObject = new ViewObject(13, 'ModificationTime', '$Views_EmittedTasks_Modification');

  /**
   * ID:14
   * Alias: AuthorID.
   */
   readonly ColumnAuthorID: ViewObject = new ViewObject(14, 'AuthorID');

  /**
   * ID:15
   * Alias: AuthorName
   * Caption: $Views_EmittedTasks_Author.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(15, 'AuthorName', '$Views_EmittedTasks_Author');

  /**
   * ID:16
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(16, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Status
   * Caption: $Views_EmittedTasks_State_Param.
   */
   readonly ParamStatus: ViewObject = new ViewObject(0, 'Status', '$Views_EmittedTasks_State_Param');

  /**
   * ID:1
   * Alias: TaskType
   * Caption: $Views_EmittedTasks_TaskType_Param.
   */
   readonly ParamTaskType: ViewObject = new ViewObject(1, 'TaskType', '$Views_EmittedTasks_TaskType_Param');

  /**
   * ID:2
   * Alias: TaskTypeGrouped
   * Caption: $Views_EmittedTasks_TaskTypeGrouped_Param.
   */
   readonly ParamTaskTypeGrouped: ViewObject = new ViewObject(2, 'TaskTypeGrouped', '$Views_EmittedTasks_TaskTypeGrouped_Param');

  /**
   * ID:3
   * Alias: CardType
   * Caption: $Views_EmittedTasks_CardType_Param.
   */
   readonly ParamCardType: ViewObject = new ViewObject(3, 'CardType', '$Views_EmittedTasks_CardType_Param');

  /**
   * ID:4
   * Alias: TaskDateDueInterval
   * Caption: $Views_EmittedTasks_TaskDateDue_Param.
   */
   readonly ParamTaskDateDueInterval: ViewObject = new ViewObject(4, 'TaskDateDueInterval', '$Views_EmittedTasks_TaskDateDue_Param');

  /**
   * ID:5
   * Alias: Role
   * Caption: $Views_EmittedTasks_Performer_Param.
   */
   readonly ParamRole: ViewObject = new ViewObject(5, 'Role', '$Views_EmittedTasks_Performer_Param');

  /**
   * ID:6
   * Alias: FunctionRoleAuthorParam
   * Caption: $Views_MyTasks_FunctionRole_Author_Param.
   */
   readonly ParamFunctionRoleAuthorParam: ViewObject = new ViewObject(6, 'FunctionRoleAuthorParam', '$Views_MyTasks_FunctionRole_Author_Param');

  /**
   * ID:7
   * Alias: FunctionRolePerformerParam
   * Caption: $Views_MyTasks_FunctionRole_Performer_Param.
   */
   readonly ParamFunctionRolePerformerParam: ViewObject = new ViewObject(7, 'FunctionRolePerformerParam', '$Views_MyTasks_FunctionRole_Performer_Param');

  //#endregion
}

//#endregion

//#region Errors

/**
 * ID: {e1307d4f-a74d-460b-bdd9-e5d8644f98da}
 * Alias: Errors
 * Caption: $Views_Names_Errors
 * Group: System
 */
class ErrorsViewInfo {
  //#region Common

  /**
   * View identifier for "Errors": {e1307d4f-a74d-460b-bdd9-e5d8644f98da}.
   */
   readonly ID: guid = 'e1307d4f-a74d-460b-bdd9-e5d8644f98da';

  /**
   * View name for "Errors".
   */
   readonly Alias: string = 'Errors';

  /**
   * View caption for "Errors".
   */
   readonly Caption: string = '$Views_Names_Errors';

  /**
   * View group for "Errors".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RecordID.
   */
   readonly ColumnRecordID: ViewObject = new ViewObject(0, 'RecordID');

  /**
   * ID:1
   * Alias: RecordCaption.
   */
   readonly ColumnRecordCaption: ViewObject = new ViewObject(1, 'RecordCaption');

  /**
   * ID:2
   * Alias: Category
   * Caption: $Views_Errors_Category.
   */
   readonly ColumnCategory: ViewObject = new ViewObject(2, 'Category', '$Views_Errors_Category');

  /**
   * ID:3
   * Alias: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(3, 'CardID');

  /**
   * ID:4
   * Alias: CardCaption
   * Caption: $Views_ActionHistory_Caption.
   */
   readonly ColumnCardCaption: ViewObject = new ViewObject(4, 'CardCaption', '$Views_ActionHistory_Caption');

  /**
   * ID:5
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(5, 'TypeID');

  /**
   * ID:6
   * Alias: TypeCaption
   * Caption: $Views_ActionHistory_Type.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(6, 'TypeCaption', '$Views_ActionHistory_Type');

  /**
   * ID:7
   * Alias: ActionID.
   */
   readonly ColumnActionID: ViewObject = new ViewObject(7, 'ActionID');

  /**
   * ID:8
   * Alias: ActionName
   * Caption: $Views_ActionHistory_Action.
   */
   readonly ColumnActionName: ViewObject = new ViewObject(8, 'ActionName', '$Views_ActionHistory_Action');

  /**
   * ID:9
   * Alias: Modified
   * Caption: $Views_ActionHistory_DateTime.
   */
   readonly ColumnModified: ViewObject = new ViewObject(9, 'Modified', '$Views_ActionHistory_DateTime');

  /**
   * ID:10
   * Alias: ModifiedByID.
   */
   readonly ColumnModifiedByID: ViewObject = new ViewObject(10, 'ModifiedByID');

  /**
   * ID:11
   * Alias: ModifiedByName
   * Caption: $Views_ActionHistory_User.
   */
   readonly ColumnModifiedByName: ViewObject = new ViewObject(11, 'ModifiedByName', '$Views_ActionHistory_User');

  /**
   * ID:12
   * Alias: Text
   * Caption: $Views_Errors_Text.
   */
   readonly ColumnText: ViewObject = new ViewObject(12, 'Text', '$Views_Errors_Text');

  /**
   * ID:13
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(13, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Category
   * Caption: $Views_Errors_Category_Param.
   */
   readonly ParamCategory: ViewObject = new ViewObject(0, 'Category', '$Views_Errors_Category_Param');

  /**
   * ID:1
   * Alias: CardID
   * Caption: $Views_ActionHistory_CardId_Param.
   */
   readonly ParamCardID: ViewObject = new ViewObject(1, 'CardID', '$Views_ActionHistory_CardId_Param');

  /**
   * ID:2
   * Alias: CardCaption
   * Caption: $Views_ActionHistory_Card_Param.
   */
   readonly ParamCardCaption: ViewObject = new ViewObject(2, 'CardCaption', '$Views_ActionHistory_Card_Param');

  /**
   * ID:3
   * Alias: TypeID
   * Caption: $Views_ActionHistory_Type_Param.
   */
   readonly ParamTypeID: ViewObject = new ViewObject(3, 'TypeID', '$Views_ActionHistory_Type_Param');

  /**
   * ID:4
   * Alias: TypeCaption
   * Caption: $Views_Errors_TypeCaption_Param.
   */
   readonly ParamTypeCaption: ViewObject = new ViewObject(4, 'TypeCaption', '$Views_Errors_TypeCaption_Param');

  /**
   * ID:5
   * Alias: ActionID
   * Caption: $Views_ActionHistory_Action_Param.
   */
   readonly ParamActionID: ViewObject = new ViewObject(5, 'ActionID', '$Views_ActionHistory_Action_Param');

  /**
   * ID:6
   * Alias: Modified
   * Caption: $Views_ActionHistory_DateTime_Param.
   */
   readonly ParamModified: ViewObject = new ViewObject(6, 'Modified', '$Views_ActionHistory_DateTime_Param');

  /**
   * ID:7
   * Alias: ModifiedByID
   * Caption: $Views_ActionHistory_User_Param.
   */
   readonly ParamModifiedByID: ViewObject = new ViewObject(7, 'ModifiedByID', '$Views_ActionHistory_User_Param');

  /**
   * ID:8
   * Alias: ModifiedByName
   * Caption: $Views_ActionHistory_UserName_Param.
   */
   readonly ParamModifiedByName: ViewObject = new ViewObject(8, 'ModifiedByName', '$Views_ActionHistory_UserName_Param');

  /**
   * ID:9
   * Alias: Text
   * Caption: $Views_Errors_Text_Param.
   */
   readonly ParamText: ViewObject = new ViewObject(9, 'Text', '$Views_Errors_Text_Param');

  /**
   * ID:10
   * Alias: DepartmentID
   * Caption: $Views_ActionHistory_UserDepartment_Param.
   */
   readonly ParamDepartmentID: ViewObject = new ViewObject(10, 'DepartmentID', '$Views_ActionHistory_UserDepartment_Param');

  //#endregion
}

//#endregion

//#region ErrorWorkflows

/**
 * ID: {91bae5ac-e846-4b71-a87b-3cee38381c66}
 * Alias: ErrorWorkflows
 * Caption: $Views_Names_ErrorWorkflows
 * Group: WorkflowEngine
 */
class ErrorWorkflowsViewInfo {
  //#region Common

  /**
   * View identifier for "ErrorWorkflows": {91bae5ac-e846-4b71-a87b-3cee38381c66}.
   */
   readonly ID: guid = '91bae5ac-e846-4b71-a87b-3cee38381c66';

  /**
   * View name for "ErrorWorkflows".
   */
   readonly Alias: string = 'ErrorWorkflows';

  /**
   * View caption for "ErrorWorkflows".
   */
   readonly Caption: string = '$Views_Names_ErrorWorkflows';

  /**
   * View group for "ErrorWorkflows".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ProcessID.
   */
   readonly ColumnProcessID: ViewObject = new ViewObject(0, 'ProcessID');

  /**
   * ID:1
   * Alias: ProcessRowID.
   */
   readonly ColumnProcessRowID: ViewObject = new ViewObject(1, 'ProcessRowID');

  /**
   * ID:2
   * Alias: ProcessName
   * Caption: $Views_ErrorWorkflows_ProcessName.
   */
   readonly ColumnProcessName: ViewObject = new ViewObject(2, 'ProcessName', '$Views_ErrorWorkflows_ProcessName');

  /**
   * ID:3
   * Alias: ProcessCreated
   * Caption: $Views_ErrorWorkflows_Created.
   */
   readonly ColumnProcessCreated: ViewObject = new ViewObject(3, 'ProcessCreated', '$Views_ErrorWorkflows_Created');

  /**
   * ID:4
   * Alias: ProcessErrorAdded
   * Caption: $Views_ErrorWorkflows_LastErrorAdded.
   */
   readonly ColumnProcessErrorAdded: ViewObject = new ViewObject(4, 'ProcessErrorAdded', '$Views_ErrorWorkflows_LastErrorAdded');

  /**
   * ID:5
   * Alias: ProcessCardID.
   */
   readonly ColumnProcessCardID: ViewObject = new ViewObject(5, 'ProcessCardID');

  /**
   * ID:6
   * Alias: ProcessCardDigest
   * Caption: $Views_ErrorWorkflows_CardDigest.
   */
   readonly ColumnProcessCardDigest: ViewObject = new ViewObject(6, 'ProcessCardDigest', '$Views_ErrorWorkflows_CardDigest');

  /**
   * ID:7
   * Alias: ProcessCardType
   * Caption: $Views_ErrorWorkflows_CardType.
   */
   readonly ColumnProcessCardType: ViewObject = new ViewObject(7, 'ProcessCardType', '$Views_ErrorWorkflows_CardType');

  /**
   * ID:8
   * Alias: ProcessResumable
   * Caption: $Views_ErrorWorkflows_Resumable.
   */
   readonly ColumnProcessResumable: ViewObject = new ViewObject(8, 'ProcessResumable', '$Views_ErrorWorkflows_Resumable');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: ProcessTemplate
   * Caption: $Views_ErrorWorkflows_ProcessTemplate_Param.
   */
   readonly ParamProcessTemplate: ViewObject = new ViewObject(0, 'ProcessTemplate', '$Views_ErrorWorkflows_ProcessTemplate_Param');

  /**
   * ID:1
   * Alias: CardType
   * Caption: $Views_ErrorWorkflows_CardType_Param.
   */
   readonly ParamCardType: ViewObject = new ViewObject(1, 'CardType', '$Views_ErrorWorkflows_CardType_Param');

  /**
   * ID:2
   * Alias: CardDigest
   * Caption: $Views_ErrorWorkflows_CardDigest.
   */
   readonly ParamCardDigest: ViewObject = new ViewObject(2, 'CardDigest', '$Views_ErrorWorkflows_CardDigest');

  /**
   * ID:3
   * Alias: CardID
   * Caption: CardID.
   */
   readonly ParamCardID: ViewObject = new ViewObject(3, 'CardID', 'CardID');

  /**
   * ID:4
   * Alias: ErrorText
   * Caption: $Views_ErrorWorkflows_ErrorText_Param.
   */
   readonly ParamErrorText: ViewObject = new ViewObject(4, 'ErrorText', '$Views_ErrorWorkflows_ErrorText_Param');

  /**
   * ID:5
   * Alias: ResumableOnly
   * Caption: $Views_ErrorWorkflows_ResumableOnly_Param.
   */
   readonly ParamResumableOnly: ViewObject = new ViewObject(5, 'ResumableOnly', '$Views_ErrorWorkflows_ResumableOnly_Param');

  //#endregion
}

//#endregion

//#region FileCategoriesAll

/**
 * ID: {f44a1e46-8b4b-43c7-bb9b-2f88507400db}
 * Alias: FileCategoriesAll
 * Caption: $Views_Names_FileCategoriesAll
 * Group: System
 */
class FileCategoriesAllViewInfo {
  //#region Common

  /**
   * View identifier for "FileCategoriesAll": {f44a1e46-8b4b-43c7-bb9b-2f88507400db}.
   */
   readonly ID: guid = 'f44a1e46-8b4b-43c7-bb9b-2f88507400db';

  /**
   * View name for "FileCategoriesAll".
   */
   readonly Alias: string = 'FileCategoriesAll';

  /**
   * View caption for "FileCategoriesAll".
   */
   readonly Caption: string = '$Views_Names_FileCategoriesAll';

  /**
   * View group for "FileCategoriesAll".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CategoryID.
   */
   readonly ColumnCategoryID: ViewObject = new ViewObject(0, 'CategoryID');

  /**
   * ID:1
   * Alias: CategoryName
   * Caption: $Views_FileCategoriesAll_Name.
   */
   readonly ColumnCategoryName: ViewObject = new ViewObject(1, 'CategoryName', '$Views_FileCategoriesAll_Name');

  /**
   * ID:2
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(2, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_FileCategoriesAll_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_FileCategoriesAll_Name_Param');

  /**
   * ID:1
   * Alias: IncludeWithoutCategory
   * Caption: $Views_FileCategoriesAll_IncludeWithoutCategory_Param.
   */
   readonly ParamIncludeWithoutCategory: ViewObject = new ViewObject(1, 'IncludeWithoutCategory', '$Views_FileCategoriesAll_IncludeWithoutCategory_Param');

  //#endregion
}

//#endregion

//#region FileCategoriesFiltered

/**
 * ID: {c54a9c60-2010-4806-9c52-a117baef7643}
 * Alias: FileCategoriesFiltered
 * Caption: $Views_Names_FileCategoriesFiltered
 * Group: System
 */
class FileCategoriesFilteredViewInfo {
  //#region Common

  /**
   * View identifier for "FileCategoriesFiltered": {c54a9c60-2010-4806-9c52-a117baef7643}.
   */
   readonly ID: guid = 'c54a9c60-2010-4806-9c52-a117baef7643';

  /**
   * View name for "FileCategoriesFiltered".
   */
   readonly Alias: string = 'FileCategoriesFiltered';

  /**
   * View caption for "FileCategoriesFiltered".
   */
   readonly Caption: string = '$Views_Names_FileCategoriesFiltered';

  /**
   * View group for "FileCategoriesFiltered".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CategoryID.
   */
   readonly ColumnCategoryID: ViewObject = new ViewObject(0, 'CategoryID');

  /**
   * ID:1
   * Alias: CategoryName
   * Caption: $Views_FileCategoriesFiltered_Name.
   */
   readonly ColumnCategoryName: ViewObject = new ViewObject(1, 'CategoryName', '$Views_FileCategoriesFiltered_Name');

  /**
   * ID:2
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(2, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_FileCategoriesFiltered_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_FileCategoriesFiltered_Name_Param');

  /**
   * ID:1
   * Alias: IncludeWithoutCategory
   * Caption: $Views_FileCategoriesFiltered_IncludeWithoutCategory_Param.
   */
   readonly ParamIncludeWithoutCategory: ViewObject = new ViewObject(1, 'IncludeWithoutCategory', '$Views_FileCategoriesFiltered_IncludeWithoutCategory_Param');

  //#endregion
}

//#endregion

//#region FileConverterTypes

/**
 * ID: {cab50857-4492-4521-8779-9ef0f5055b44}
 * Alias: FileConverterTypes
 * Caption: $Views_Names_FileConverterTypes
 * Group: System
 */
class FileConverterTypesViewInfo {
  //#region Common

  /**
   * View identifier for "FileConverterTypes": {cab50857-4492-4521-8779-9ef0f5055b44}.
   */
   readonly ID: guid = 'cab50857-4492-4521-8779-9ef0f5055b44';

  /**
   * View name for "FileConverterTypes".
   */
   readonly Alias: string = 'FileConverterTypes';

  /**
   * View caption for "FileConverterTypes".
   */
   readonly Caption: string = '$Views_Names_FileConverterTypes';

  /**
   * View group for "FileConverterTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: $Views_FileConverterTypes_Name.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', '$Views_FileConverterTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_FileConverterTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_FileConverterTypes_Name_Param');

  //#endregion
}

//#endregion

//#region FileTemplates

/**
 * ID: {86b47fab-4522-4d84-9cf1-d0db3fd06c75}
 * Alias: FileTemplates
 * Caption: $Views_Names_FileTemplates
 * Group: System
 */
class FileTemplatesViewInfo {
  //#region Common

  /**
   * View identifier for "FileTemplates": {86b47fab-4522-4d84-9cf1-d0db3fd06c75}.
   */
   readonly ID: guid = '86b47fab-4522-4d84-9cf1-d0db3fd06c75';

  /**
   * View name for "FileTemplates".
   */
   readonly Alias: string = 'FileTemplates';

  /**
   * View caption for "FileTemplates".
   */
   readonly Caption: string = '$Views_Names_FileTemplates';

  /**
   * View group for "FileTemplates".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: FileTemplateID.
   */
   readonly ColumnFileTemplateID: ViewObject = new ViewObject(0, 'FileTemplateID');

  /**
   * ID:1
   * Alias: FileTemplateName
   * Caption: $Views_FileTemplate_Name.
   */
   readonly ColumnFileTemplateName: ViewObject = new ViewObject(1, 'FileTemplateName', '$Views_FileTemplate_Name');

  /**
   * ID:2
   * Alias: FileTemplateGroupName
   * Caption: $Views_FileTemplate_Group.
   */
   readonly ColumnFileTemplateGroupName: ViewObject = new ViewObject(2, 'FileTemplateGroupName', '$Views_FileTemplate_Group');

  /**
   * ID:3
   * Alias: FileTemplateType
   * Caption: $Views_FileTemplate_Type.
   */
   readonly ColumnFileTemplateType: ViewObject = new ViewObject(3, 'FileTemplateType', '$Views_FileTemplate_Type');

  /**
   * ID:4
   * Alias: FileTemplateSystem
   * Caption: $Views_FileTemplate_System.
   */
   readonly ColumnFileTemplateSystem: ViewObject = new ViewObject(4, 'FileTemplateSystem', '$Views_FileTemplate_System');

  /**
   * ID:5
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(5, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_FileTemplate_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_FileTemplate_Name_Param');

  /**
   * ID:1
   * Alias: GroupName
   * Caption: $Views_FileTemplate_Group_Param.
   */
   readonly ParamGroupName: ViewObject = new ViewObject(1, 'GroupName', '$Views_FileTemplate_Group_Param');

  /**
   * ID:2
   * Alias: Type
   * Caption: $Views_FileTemplate_Type_Param.
   */
   readonly ParamType: ViewObject = new ViewObject(2, 'Type', '$Views_FileTemplate_Type_Param');

  /**
   * ID:3
   * Alias: System
   * Caption: $Views_FileTemplate_System_Param.
   */
   readonly ParamSystem: ViewObject = new ViewObject(3, 'System', '$Views_FileTemplate_System_Param');

  //#endregion
}

//#endregion

//#region FileTemplateTemplateTypes

/**
 * ID: {91a427d5-4dd9-4a7f-b35f-cb48de3254d0}
 * Alias: FileTemplateTemplateTypes
 * Caption: $Views_Names_FileTemplateTemplateTypes
 * Group: System
 */
class FileTemplateTemplateTypesViewInfo {
  //#region Common

  /**
   * View identifier for "FileTemplateTemplateTypes": {91a427d5-4dd9-4a7f-b35f-cb48de3254d0}.
   */
   readonly ID: guid = '91a427d5-4dd9-4a7f-b35f-cb48de3254d0';

  /**
   * View name for "FileTemplateTemplateTypes".
   */
   readonly Alias: string = 'FileTemplateTemplateTypes';

  /**
   * View caption for "FileTemplateTemplateTypes".
   */
   readonly Caption: string = '$Views_Names_FileTemplateTemplateTypes';

  /**
   * View group for "FileTemplateTemplateTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeName
   * Caption: $Views_KrTypes_Name.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(1, 'TypeName', '$Views_KrTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_KrTypes_Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_KrTypes_Name');

  //#endregion
}

//#endregion

//#region FileTemplateTypes

/**
 * ID: {eb59292c-378c-412f-b780-88469049a349}
 * Alias: FileTemplateTypes
 * Caption: $Views_Names_FileTemplateTypes
 * Group: System
 */
class FileTemplateTypesViewInfo {
  //#region Common

  /**
   * View identifier for "FileTemplateTypes": {eb59292c-378c-412f-b780-88469049a349}.
   */
   readonly ID: guid = 'eb59292c-378c-412f-b780-88469049a349';

  /**
   * View name for "FileTemplateTypes".
   */
   readonly Alias: string = 'FileTemplateTypes';

  /**
   * View caption for "FileTemplateTypes".
   */
   readonly Caption: string = '$Views_Names_FileTemplateTypes';

  /**
   * View group for "FileTemplateTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeCaption
   * Caption: $Views_FileTemplateTypes_Name.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(1, 'TypeCaption', '$Views_FileTemplateTypes_Name');

  /**
   * ID:2
   * Alias: TypeName
   * Caption: $Views_FileTemplateTypes_Alias.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(2, 'TypeName', '$Views_FileTemplateTypes_Alias');

  /**
   * ID:3
   * Alias: IsDocTypeCaption
   * Caption: $Views_FileTemplateTypes_Type.
   */
   readonly ColumnIsDocTypeCaption: ViewObject = new ViewObject(3, 'IsDocTypeCaption', '$Views_FileTemplateTypes_Type');

  /**
   * ID:4
   * Alias: LocalizedCaption.
   */
   readonly ColumnLocalizedCaption: ViewObject = new ViewObject(4, 'LocalizedCaption');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_FileTemplateTypes_Alias_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_FileTemplateTypes_Alias_Param');

  /**
   * ID:1
   * Alias: Caption
   * Caption: $Views_FileTemplateTypes_Name_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(1, 'Caption', '$Views_FileTemplateTypes_Name_Param');

  //#endregion
}

//#endregion

//#region FormatSettings

/**
 * ID: {038628a6-a2c0-4276-a986-2ab73428ca42}
 * Alias: FormatSettings
 * Caption: $Views_Names_FormatSettings
 * Group: System
 */
class FormatSettingsViewInfo {
  //#region Common

  /**
   * View identifier for "FormatSettings": {038628a6-a2c0-4276-a986-2ab73428ca42}.
   */
   readonly ID: guid = '038628a6-a2c0-4276-a986-2ab73428ca42';

  /**
   * View name for "FormatSettings".
   */
   readonly Alias: string = 'FormatSettings';

  /**
   * View caption for "FormatSettings".
   */
   readonly Caption: string = '$Views_Names_FormatSettings';

  /**
   * View group for "FormatSettings".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SettingsID.
   */
   readonly ColumnSettingsID: ViewObject = new ViewObject(0, 'SettingsID');

  /**
   * ID:1
   * Alias: SettingsCaption
   * Caption: $Views_FormatSettings_Caption.
   */
   readonly ColumnSettingsCaption: ViewObject = new ViewObject(1, 'SettingsCaption', '$Views_FormatSettings_Caption');

  /**
   * ID:2
   * Alias: SettingsName
   * Caption: $Views_FormatSettings_Name.
   */
   readonly ColumnSettingsName: ViewObject = new ViewObject(2, 'SettingsName', '$Views_FormatSettings_Name');

  /**
   * ID:3
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(3, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_FormatSettings_Caption_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_FormatSettings_Caption_Param');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_FormatSettings_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_FormatSettings_Name_Param');

  /**
   * ID:2
   * Alias: ID
   * Caption: ID.
   */
   readonly ParamID: ViewObject = new ViewObject(2, 'ID', 'ID');

  //#endregion
}

//#endregion

//#region FunctionRoleCards

/**
 * ID: {6693a0e4-421c-484f-a4ce-21be436a4be2}
 * Alias: FunctionRoleCards
 * Caption: $Views_Names_FunctionRoleCards
 * Group: System
 */
class FunctionRoleCardsViewInfo {
  //#region Common

  /**
   * View identifier for "FunctionRoleCards": {6693a0e4-421c-484f-a4ce-21be436a4be2}.
   */
   readonly ID: guid = '6693a0e4-421c-484f-a4ce-21be436a4be2';

  /**
   * View name for "FunctionRoleCards".
   */
   readonly Alias: string = 'FunctionRoleCards';

  /**
   * View caption for "FunctionRoleCards".
   */
   readonly Caption: string = '$Views_Names_FunctionRoleCards';

  /**
   * View group for "FunctionRoleCards".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: FunctionRoleID.
   */
   readonly ColumnFunctionRoleID: ViewObject = new ViewObject(0, 'FunctionRoleID');

  /**
   * ID:1
   * Alias: FunctionRoleCaption
   * Caption: $Views_FunctionRoles_Caption.
   */
   readonly ColumnFunctionRoleCaption: ViewObject = new ViewObject(1, 'FunctionRoleCaption', '$Views_FunctionRoles_Caption');

  /**
   * ID:2
   * Alias: FunctionRoleName
   * Caption: $Views_FunctionRoles_Alias.
   */
   readonly ColumnFunctionRoleName: ViewObject = new ViewObject(2, 'FunctionRoleName', '$Views_FunctionRoles_Alias');

  /**
   * ID:3
   * Alias: FunctionRoleCanBeDeputy
   * Caption: $Views_FunctionRoles_CanBeDeputy.
   */
   readonly ColumnFunctionRoleCanBeDeputy: ViewObject = new ViewObject(3, 'FunctionRoleCanBeDeputy', '$Views_FunctionRoles_CanBeDeputy');

  /**
   * ID:4
   * Alias: FunctionRoleCanTakeInProgress
   * Caption: $Views_FunctionRoles_CanTakeInProgress.
   */
   readonly ColumnFunctionRoleCanTakeInProgress: ViewObject = new ViewObject(4, 'FunctionRoleCanTakeInProgress', '$Views_FunctionRoles_CanTakeInProgress');

  /**
   * ID:5
   * Alias: FunctionRoleHideTaskByDefault
   * Caption: $Views_FunctionRoles_HideTaskByDefault.
   */
   readonly ColumnFunctionRoleHideTaskByDefault: ViewObject = new ViewObject(5, 'FunctionRoleHideTaskByDefault', '$Views_FunctionRoles_HideTaskByDefault');

  /**
   * ID:6
   * Alias: FunctionRoleCanChangeTaskInfo
   * Caption: $Views_FunctionRoles_CanChangeTaskInfo.
   */
   readonly ColumnFunctionRoleCanChangeTaskInfo: ViewObject = new ViewObject(6, 'FunctionRoleCanChangeTaskInfo', '$Views_FunctionRoles_CanChangeTaskInfo');

  /**
   * ID:7
   * Alias: FunctionRoleCanChangeTaskRoles
   * Caption: $Views_FunctionRoles_CanChangeTaskRoles.
   */
   readonly ColumnFunctionRoleCanChangeTaskRoles: ViewObject = new ViewObject(7, 'FunctionRoleCanChangeTaskRoles', '$Views_FunctionRoles_CanChangeTaskRoles');

  /**
   * ID:8
   * Alias: PartitionID.
   */
   readonly ColumnPartitionID: ViewObject = new ViewObject(8, 'PartitionID');

  /**
   * ID:9
   * Alias: PartitionName
   * Caption: $Views_FunctionRoles_Partition.
   */
   readonly ColumnPartitionName: ViewObject = new ViewObject(9, 'PartitionName', '$Views_FunctionRoles_Partition');

  /**
   * ID:10
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(10, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: FunctionRoleID
   * Caption: FunctionRoleID.
   */
   readonly ParamFunctionRoleID: ViewObject = new ViewObject(0, 'FunctionRoleID', 'FunctionRoleID');

  /**
   * ID:1
   * Alias: Caption
   * Caption: $Views_FunctionRoles_Caption_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(1, 'Caption', '$Views_FunctionRoles_Caption_Param');

  /**
   * ID:2
   * Alias: Name
   * Caption: $Views_FunctionRoles_Alias_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(2, 'Name', '$Views_FunctionRoles_Alias_Param');

  /**
   * ID:3
   * Alias: CanBeDeputy
   * Caption: $Views_FunctionRoles_CanBeDeputy_Param.
   */
   readonly ParamCanBeDeputy: ViewObject = new ViewObject(3, 'CanBeDeputy', '$Views_FunctionRoles_CanBeDeputy_Param');

  /**
   * ID:4
   * Alias: CanTakeInProgress
   * Caption: $Views_FunctionRoles_CanTakeInProgress_Param.
   */
   readonly ParamCanTakeInProgress: ViewObject = new ViewObject(4, 'CanTakeInProgress', '$Views_FunctionRoles_CanTakeInProgress_Param');

  /**
   * ID:5
   * Alias: HideTaskByDefault
   * Caption: $Views_FunctionRoles_HideTaskByDefault_Param.
   */
   readonly ParamHideTaskByDefault: ViewObject = new ViewObject(5, 'HideTaskByDefault', '$Views_FunctionRoles_HideTaskByDefault_Param');

  /**
   * ID:6
   * Alias: CanChangeTaskInfo
   * Caption: $Views_FunctionRoles_CanChangeTaskInfo_Param.
   */
   readonly ParamCanChangeTaskInfo: ViewObject = new ViewObject(6, 'CanChangeTaskInfo', '$Views_FunctionRoles_CanChangeTaskInfo_Param');

  /**
   * ID:7
   * Alias: CanChangeTaskRoles
   * Caption: $Views_FunctionRoles_CanChangeTaskRoles_Param.
   */
   readonly ParamCanChangeTaskRoles: ViewObject = new ViewObject(7, 'CanChangeTaskRoles', '$Views_FunctionRoles_CanChangeTaskRoles_Param');

  /**
   * ID:8
   * Alias: Partition
   * Caption: $Views_FunctionRoles_Partition.
   */
   readonly ParamPartition: ViewObject = new ViewObject(8, 'Partition', '$Views_FunctionRoles_Partition');

  //#endregion
}

//#endregion

//#region GetCardIDView

/**
 * ID: {07100666-36ac-49e3-ae68-e53caafb45a2}
 * Alias: GetCardIDView
 * Caption: $Views_Names_GetCardIDView
 * Group: Testing
 */
class GetCardIDViewViewInfo {
  //#region Common

  /**
   * View identifier for "GetCardIDView": {07100666-36ac-49e3-ae68-e53caafb45a2}.
   */
   readonly ID: guid = '07100666-36ac-49e3-ae68-e53caafb45a2';

  /**
   * View name for "GetCardIDView".
   */
   readonly Alias: string = 'GetCardIDView';

  /**
   * View caption for "GetCardIDView".
   */
   readonly Caption: string = '$Views_Names_GetCardIDView';

  /**
   * View group for "GetCardIDView".
   */
   readonly Group: string = 'Testing';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CardID
   * Caption: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(0, 'CardID', 'CardID');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: ID
   * Caption: ID.
   */
   readonly ParamID: ViewObject = new ViewObject(0, 'ID', 'ID');

  //#endregion
}

//#endregion

//#region GetFileNameView

/**
 * ID: {1eb7431c-32f1-4ed6-bf71-57a842d61949}
 * Alias: GetFileNameView
 * Caption: $Views_Names_GetFileNameView
 * Group: Testing
 */
class GetFileNameViewViewInfo {
  //#region Common

  /**
   * View identifier for "GetFileNameView": {1eb7431c-32f1-4ed6-bf71-57a842d61949}.
   */
   readonly ID: guid = '1eb7431c-32f1-4ed6-bf71-57a842d61949';

  /**
   * View name for "GetFileNameView".
   */
   readonly Alias: string = 'GetFileNameView';

  /**
   * View caption for "GetFileNameView".
   */
   readonly Caption: string = '$Views_Names_GetFileNameView';

  /**
   * View group for "GetFileNameView".
   */
   readonly Group: string = 'Testing';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: PreviewPath
   * Caption: PreviewPath.
   */
   readonly ColumnPreviewPath: ViewObject = new ViewObject(0, 'PreviewPath', 'PreviewPath');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: FileName
   * Caption: FileName.
   */
   readonly ParamFileName: ViewObject = new ViewObject(0, 'FileName', 'FileName');

  //#endregion
}

//#endregion

//#region Groups

/**
 * ID: {4f179970-84b9-4b6a-b921-72cc79ca2cb3}
 * Alias: Groups
 * Caption: $Views_Names_Groups
 * Group: Testing
 */
class GroupsViewInfo {
  //#region Common

  /**
   * View identifier for "Groups": {4f179970-84b9-4b6a-b921-72cc79ca2cb3}.
   */
   readonly ID: guid = '4f179970-84b9-4b6a-b921-72cc79ca2cb3';

  /**
   * View name for "Groups".
   */
   readonly Alias: string = 'Groups';

  /**
   * View caption for "Groups".
   */
   readonly Caption: string = '$Views_Names_Groups';

  /**
   * View group for "Groups".
   */
   readonly Group: string = 'Testing';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: GroupID.
   */
   readonly ColumnGroupID: ViewObject = new ViewObject(0, 'GroupID');

  /**
   * ID:1
   * Alias: GroupParentID.
   */
   readonly ColumnGroupParentID: ViewObject = new ViewObject(1, 'GroupParentID');

  /**
   * ID:2
   * Alias: GroupName.
   */
   readonly ColumnGroupName: ViewObject = new ViewObject(2, 'GroupName');

  /**
   * ID:3
   * Alias: Content
   * Caption: Содержимое.
   */
   readonly ColumnContent: ViewObject = new ViewObject(3, 'Content', 'Содержимое');

  //#endregion
}

//#endregion

//#region GroupsWithHierarchy

/**
 * ID: {5de1d29e-41ce-4279-90d5-573d2a81f009}
 * Alias: GroupsWithHierarchy
 * Caption: $Views_Names_GroupsWithHierarchy
 * Group: Testing
 */
class GroupsWithHierarchyViewInfo {
  //#region Common

  /**
   * View identifier for "GroupsWithHierarchy": {5de1d29e-41ce-4279-90d5-573d2a81f009}.
   */
   readonly ID: guid = '5de1d29e-41ce-4279-90d5-573d2a81f009';

  /**
   * View name for "GroupsWithHierarchy".
   */
   readonly Alias: string = 'GroupsWithHierarchy';

  /**
   * View caption for "GroupsWithHierarchy".
   */
   readonly Caption: string = '$Views_Names_GroupsWithHierarchy';

  /**
   * View group for "GroupsWithHierarchy".
   */
   readonly Group: string = 'Testing';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ID.
   */
   readonly ColumnID: ViewObject = new ViewObject(0, 'ID');

  /**
   * ID:1
   * Alias: ParentID.
   */
   readonly ColumnParentID: ViewObject = new ViewObject(1, 'ParentID');

  /**
   * ID:2
   * Alias: GroupID.
   */
   readonly ColumnGroupID: ViewObject = new ViewObject(2, 'GroupID');

  /**
   * ID:3
   * Alias: GroupParentID.
   */
   readonly ColumnGroupParentID: ViewObject = new ViewObject(3, 'GroupParentID');

  /**
   * ID:4
   * Alias: GroupName.
   */
   readonly ColumnGroupName: ViewObject = new ViewObject(4, 'GroupName');

  /**
   * ID:5
   * Alias: Content
   * Caption: Содержимое.
   */
   readonly ColumnContent: ViewObject = new ViewObject(5, 'Content', 'Содержимое');

  //#endregion
}

//#endregion

//#region HelpSections

/**
 * ID: {c35e6ac1-9cec-482d-a20f-b3c330f2dc53}
 * Alias: HelpSections
 * Caption: $Views_Names_HelpSections
 * Group: System
 */
class HelpSectionsViewInfo {
  //#region Common

  /**
   * View identifier for "HelpSections": {c35e6ac1-9cec-482d-a20f-b3c330f2dc53}.
   */
   readonly ID: guid = 'c35e6ac1-9cec-482d-a20f-b3c330f2dc53';

  /**
   * View name for "HelpSections".
   */
   readonly Alias: string = 'HelpSections';

  /**
   * View caption for "HelpSections".
   */
   readonly Caption: string = '$Views_Names_HelpSections';

  /**
   * View group for "HelpSections".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: HelpSectionID.
   */
   readonly ColumnHelpSectionID: ViewObject = new ViewObject(0, 'HelpSectionID');

  /**
   * ID:1
   * Alias: HelpSectionCode
   * Caption: $Views_HelpSections_Code.
   */
   readonly ColumnHelpSectionCode: ViewObject = new ViewObject(1, 'HelpSectionCode', '$Views_HelpSections_Code');

  /**
   * ID:2
   * Alias: HelpSectionName
   * Caption: $Views_HelpSections_Name.
   */
   readonly ColumnHelpSectionName: ViewObject = new ViewObject(2, 'HelpSectionName', '$Views_HelpSections_Name');

  /**
   * ID:3
   * Alias: HelpSectionRichText.
   */
   readonly ColumnHelpSectionRichText: ViewObject = new ViewObject(3, 'HelpSectionRichText');

  /**
   * ID:4
   * Alias: HelpSectionPlainText
   * Caption: $Views_HelpSections_PlainText.
   */
   readonly ColumnHelpSectionPlainText: ViewObject = new ViewObject(4, 'HelpSectionPlainText', '$Views_HelpSections_PlainText');

  /**
   * ID:5
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(5, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: PlainText
   * Caption: $Views_HelpSections_PlainText.
   */
   readonly ParamPlainText: ViewObject = new ViewObject(0, 'PlainText', '$Views_HelpSections_PlainText');

  //#endregion
}

//#endregion

//#region Hierarchy

/**
 * ID: {29929a97-79f8-4eda-a6ee-b9621aa9ae49}
 * Alias: Hierarchy
 * Caption: $Views_Names_Hierarchy
 * Group: Testing
 */
class HierarchyViewInfo {
  //#region Common

  /**
   * View identifier for "Hierarchy": {29929a97-79f8-4eda-a6ee-b9621aa9ae49}.
   */
   readonly ID: guid = '29929a97-79f8-4eda-a6ee-b9621aa9ae49';

  /**
   * View name for "Hierarchy".
   */
   readonly Alias: string = 'Hierarchy';

  /**
   * View caption for "Hierarchy".
   */
   readonly Caption: string = '$Views_Names_Hierarchy';

  /**
   * View group for "Hierarchy".
   */
   readonly Group: string = 'Testing';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ID.
   */
   readonly ColumnID: ViewObject = new ViewObject(0, 'ID');

  /**
   * ID:1
   * Alias: ParentID.
   */
   readonly ColumnParentID: ViewObject = new ViewObject(1, 'ParentID');

  /**
   * ID:2
   * Alias: Content
   * Caption: Содержимое.
   */
   readonly ColumnContent: ViewObject = new ViewObject(2, 'Content', 'Содержимое');

  //#endregion
}

//#endregion

//#region IncomingDocuments

/**
 * ID: {a1889a97-2c55-489a-a942-fa36b61dff04}
 * Alias: IncomingDocuments
 * Caption: $Views_Names_IncomingDocuments
 * Group: KrDocuments
 */
class IncomingDocumentsViewInfo {
  //#region Common

  /**
   * View identifier for "IncomingDocuments": {a1889a97-2c55-489a-a942-fa36b61dff04}.
   */
   readonly ID: guid = 'a1889a97-2c55-489a-a942-fa36b61dff04';

  /**
   * View name for "IncomingDocuments".
   */
   readonly Alias: string = 'IncomingDocuments';

  /**
   * View caption for "IncomingDocuments".
   */
   readonly Caption: string = '$Views_Names_IncomingDocuments';

  /**
   * View group for "IncomingDocuments".
   */
   readonly Group: string = 'KrDocuments';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DocID.
   */
   readonly ColumnDocID: ViewObject = new ViewObject(0, 'DocID');

  /**
   * ID:1
   * Alias: DocNumber
   * Caption: $Views_Registers_Number.
   */
   readonly ColumnDocNumber: ViewObject = new ViewObject(1, 'DocNumber', '$Views_Registers_Number');

  /**
   * ID:2
   * Alias: SubTypeTitle
   * Caption: $Views_Registers_DocType.
   */
   readonly ColumnSubTypeTitle: ViewObject = new ViewObject(2, 'SubTypeTitle', '$Views_Registers_DocType');

  /**
   * ID:3
   * Alias: DocSubject
   * Caption: $Views_Registers_Subject.
   */
   readonly ColumnDocSubject: ViewObject = new ViewObject(3, 'DocSubject', '$Views_Registers_Subject');

  /**
   * ID:4
   * Alias: DocDescription
   * Caption: $Views_Registers_DocDescription.
   */
   readonly ColumnDocDescription: ViewObject = new ViewObject(4, 'DocDescription', '$Views_Registers_DocDescription');

  /**
   * ID:5
   * Alias: PartnerID.
   */
   readonly ColumnPartnerID: ViewObject = new ViewObject(5, 'PartnerID');

  /**
   * ID:6
   * Alias: PartnerName
   * Caption: $Views_Registers_Partner.
   */
   readonly ColumnPartnerName: ViewObject = new ViewObject(6, 'PartnerName', '$Views_Registers_Partner');

  /**
   * ID:7
   * Alias: OutgoingNumber
   * Caption: $Views_Registers_OutgoingNumber.
   */
   readonly ColumnOutgoingNumber: ViewObject = new ViewObject(7, 'OutgoingNumber', '$Views_Registers_OutgoingNumber');

  /**
   * ID:8
   * Alias: RegistratorID.
   */
   readonly ColumnRegistratorID: ViewObject = new ViewObject(8, 'RegistratorID');

  /**
   * ID:9
   * Alias: RegistratorName
   * Caption: $Views_Registers_Registrator.
   */
   readonly ColumnRegistratorName: ViewObject = new ViewObject(9, 'RegistratorName', '$Views_Registers_Registrator');

  /**
   * ID:10
   * Alias: DocDate
   * Caption: $Views_Registers_DocDate.
   */
   readonly ColumnDocDate: ViewObject = new ViewObject(10, 'DocDate', '$Views_Registers_DocDate');

  /**
   * ID:11
   * Alias: CreationDate
   * Caption: $Views_Registers_CreationDate.
   */
   readonly ColumnCreationDate: ViewObject = new ViewObject(11, 'CreationDate', '$Views_Registers_CreationDate');

  /**
   * ID:12
   * Alias: Department
   * Caption: $Views_Registers_Department.
   */
   readonly ColumnDepartment: ViewObject = new ViewObject(12, 'Department', '$Views_Registers_Department');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: IsInitiator
   * Caption: $Views_Registers_IsInitiator_Param.
   */
   readonly ParamIsInitiator: ViewObject = new ViewObject(0, 'IsInitiator', '$Views_Registers_IsInitiator_Param');

  /**
   * ID:1
   * Alias: IsRegistrator
   * Caption: $Views_Registers_IsRegistrator_Param.
   */
   readonly ParamIsRegistrator: ViewObject = new ViewObject(1, 'IsRegistrator', '$Views_Registers_IsRegistrator_Param');

  /**
   * ID:2
   * Alias: Partner
   * Caption: $Views_Registers_Partner_Param.
   */
   readonly ParamPartner: ViewObject = new ViewObject(2, 'Partner', '$Views_Registers_Partner_Param');

  /**
   * ID:3
   * Alias: OutgoingNumber
   * Caption: $Views_Registers_OutgoingNumber_Param.
   */
   readonly ParamOutgoingNumber: ViewObject = new ViewObject(3, 'OutgoingNumber', '$Views_Registers_OutgoingNumber_Param');

  /**
   * ID:4
   * Alias: Number
   * Caption: $Views_Registers_Number_Param.
   */
   readonly ParamNumber: ViewObject = new ViewObject(4, 'Number', '$Views_Registers_Number_Param');

  /**
   * ID:5
   * Alias: Subject
   * Caption: $Views_Registers_Subject_Param.
   */
   readonly ParamSubject: ViewObject = new ViewObject(5, 'Subject', '$Views_Registers_Subject_Param');

  /**
   * ID:6
   * Alias: DocDate
   * Caption: $Views_Registers_DocDate_Param.
   */
   readonly ParamDocDate: ViewObject = new ViewObject(6, 'DocDate', '$Views_Registers_DocDate_Param');

  /**
   * ID:7
   * Alias: Registrator
   * Caption: $Views_Registers_Registrator_Param.
   */
   readonly ParamRegistrator: ViewObject = new ViewObject(7, 'Registrator', '$Views_Registers_Registrator_Param');

  /**
   * ID:8
   * Alias: State
   * Caption: $Views_Registers_State_Param.
   */
   readonly ParamState: ViewObject = new ViewObject(8, 'State', '$Views_Registers_State_Param');

  /**
   * ID:9
   * Alias: DocType
   * Caption: $Views_Registers_DocType_Param.
   */
   readonly ParamDocType: ViewObject = new ViewObject(9, 'DocType', '$Views_Registers_DocType_Param');

  /**
   * ID:10
   * Alias: Department
   * Caption: $Views_Registers_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(10, 'Department', '$Views_Registers_Department_Param');

  /**
   * ID:11
   * Alias: CreationDate
   * Caption: $Views_Registers_CreationDate_Param.
   */
   readonly ParamCreationDate: ViewObject = new ViewObject(11, 'CreationDate', '$Views_Registers_CreationDate_Param');

  //#endregion
}

//#endregion

//#region KrActionTypes

/**
 * ID: {73ad84ae-84b6-4292-b496-5bf63cf9033e}
 * Alias: KrActionTypes
 * Caption: $Views_Names_KrActionTypes
 * Group: Kr Wf
 */
class KrActionTypesViewInfo {
  //#region Common

  /**
   * View identifier for "KrActionTypes": {73ad84ae-84b6-4292-b496-5bf63cf9033e}.
   */
   readonly ID: guid = '73ad84ae-84b6-4292-b496-5bf63cf9033e';

  /**
   * View name for "KrActionTypes".
   */
   readonly Alias: string = 'KrActionTypes';

  /**
   * View caption for "KrActionTypes".
   */
   readonly Caption: string = '$Views_Names_KrActionTypes';

  /**
   * View group for "KrActionTypes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: RefName.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', 'RefName');

  /**
   * ID:2
   * Alias: RefEventType
   * Caption: RefEventType.
   */
   readonly ColumnRefEventType: ViewObject = new ViewObject(2, 'RefEventType', 'RefEventType');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', 'Name');

  //#endregion
}

//#endregion

//#region KrCreateCardStageTypeModes

/**
 * ID: {1e8ca3c1-72a3-4c2c-8740-01c4dd96de39}
 * Alias: KrCreateCardStageTypeModes
 * Caption: $Views_Names_KrCreateCardStageTypeModes
 * Group: Kr Wf
 */
class KrCreateCardStageTypeModesViewInfo {
  //#region Common

  /**
   * View identifier for "KrCreateCardStageTypeModes": {1e8ca3c1-72a3-4c2c-8740-01c4dd96de39}.
   */
   readonly ID: guid = '1e8ca3c1-72a3-4c2c-8740-01c4dd96de39';

  /**
   * View name for "KrCreateCardStageTypeModes".
   */
   readonly Alias: string = 'KrCreateCardStageTypeModes';

  /**
   * View caption for "KrCreateCardStageTypeModes".
   */
   readonly Caption: string = '$Views_Names_KrCreateCardStageTypeModes';

  /**
   * View group for "KrCreateCardStageTypeModes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: RefName.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', 'RefName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', 'Name');

  //#endregion
}

//#endregion

//#region KrCycleGroupingModes

/**
 * ID: {ac33bea6-af04-4e73-b9ca-5fedb8fcf64f}
 * Alias: KrCycleGroupingModes
 * Caption: $Views_Names_KrCycleGroupingModes
 * Group: Kr Wf
 */
class KrCycleGroupingModesViewInfo {
  //#region Common

  /**
   * View identifier for "KrCycleGroupingModes": {ac33bea6-af04-4e73-b9ca-5fedb8fcf64f}.
   */
   readonly ID: guid = 'ac33bea6-af04-4e73-b9ca-5fedb8fcf64f';

  /**
   * View name for "KrCycleGroupingModes".
   */
   readonly Alias: string = 'KrCycleGroupingModes';

  /**
   * View caption for "KrCycleGroupingModes".
   */
   readonly Caption: string = '$Views_Names_KrCycleGroupingModes';

  /**
   * View group for "KrCycleGroupingModes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ModeID.
   */
   readonly ColumnModeID: ViewObject = new ViewObject(0, 'ModeID');

  /**
   * ID:1
   * Alias: ModeName
   * Caption: $Views_KrCycleGroupingModes_Name.
   */
   readonly ColumnModeName: ViewObject = new ViewObject(1, 'ModeName', '$Views_KrCycleGroupingModes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_KrCycleGroupingModes_Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_KrCycleGroupingModes_Name');

  //#endregion
}

//#endregion

//#region KrDocNumberRegistrationAutoAssigment

/**
 * ID: {65867469-2aec-4c13-a807-a2784a023d6b}
 * Alias: KrDocNumberRegistrationAutoAssigment
 * Caption: $Views_Names_KrDocNumberRegistrationAutoAssigment
 * Group: Kr Wf
 */
class KrDocNumberRegistrationAutoAssigmentViewInfo {
  //#region Common

  /**
   * View identifier for "KrDocNumberRegistrationAutoAssigment": {65867469-2aec-4c13-a807-a2784a023d6b}.
   */
   readonly ID: guid = '65867469-2aec-4c13-a807-a2784a023d6b';

  /**
   * View name for "KrDocNumberRegistrationAutoAssigment".
   */
   readonly Alias: string = 'KrDocNumberRegistrationAutoAssigment';

  /**
   * View caption for "KrDocNumberRegistrationAutoAssigment".
   */
   readonly Caption: string = '$Views_Names_KrDocNumberRegistrationAutoAssigment';

  /**
   * View group for "KrDocNumberRegistrationAutoAssigment".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DocNumberRegistrationAutoAssignmentID.
   */
   readonly ColumnDocNumberRegistrationAutoAssignmentID: ViewObject = new ViewObject(0, 'DocNumberRegistrationAutoAssignmentID');

  /**
   * ID:1
   * Alias: DocNumberRegistrationAutoAssignmentDescription
   * Caption: $Views_KrDocNumberRegistrationAutoAssigment_Option.
   */
   readonly ColumnDocNumberRegistrationAutoAssignmentDescription: ViewObject = new ViewObject(1, 'DocNumberRegistrationAutoAssignmentDescription', '$Views_KrDocNumberRegistrationAutoAssigment_Option');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Description
   * Caption: $Views_KrDocNumberRegistrationAutoAssigment_Option_Param.
   */
   readonly ParamDescription: ViewObject = new ViewObject(0, 'Description', '$Views_KrDocNumberRegistrationAutoAssigment_Option_Param');

  //#endregion
}

//#endregion

//#region KrDocNumberRegularAutoAssigment

/**
 * ID: {021327d4-1e7a-4834-bbc8-6cafd415f098}
 * Alias: KrDocNumberRegularAutoAssigment
 * Caption: $Views_Names_KrDocNumberRegularAutoAssigment
 * Group: Kr Wf
 */
class KrDocNumberRegularAutoAssigmentViewInfo {
  //#region Common

  /**
   * View identifier for "KrDocNumberRegularAutoAssigment": {021327d4-1e7a-4834-bbc8-6cafd415f098}.
   */
   readonly ID: guid = '021327d4-1e7a-4834-bbc8-6cafd415f098';

  /**
   * View name for "KrDocNumberRegularAutoAssigment".
   */
   readonly Alias: string = 'KrDocNumberRegularAutoAssigment';

  /**
   * View caption for "KrDocNumberRegularAutoAssigment".
   */
   readonly Caption: string = '$Views_Names_KrDocNumberRegularAutoAssigment';

  /**
   * View group for "KrDocNumberRegularAutoAssigment".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DocNumberRegularAutoAssignmentID.
   */
   readonly ColumnDocNumberRegularAutoAssignmentID: ViewObject = new ViewObject(0, 'DocNumberRegularAutoAssignmentID');

  /**
   * ID:1
   * Alias: DocNumberRegularAutoAssignmentDescription
   * Caption: $Views_KrDocNumberRegularAutoAssigment_Option_Param.
   */
   readonly ColumnDocNumberRegularAutoAssignmentDescription: ViewObject = new ViewObject(1, 'DocNumberRegularAutoAssignmentDescription', '$Views_KrDocNumberRegularAutoAssigment_Option_Param');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Description
   * Caption: $Views_KrDocNumberRegularAutoAssigment_Option.
   */
   readonly ParamDescription: ViewObject = new ViewObject(0, 'Description', '$Views_KrDocNumberRegularAutoAssigment_Option');

  //#endregion
}

//#endregion

//#region KrDocStateCards

/**
 * ID: {d9534c6c-ec26-4de9-be78-5df833b70f43}
 * Alias: KrDocStateCards
 * Caption: $Views_Names_KrDocStateCards
 * Group: Kr Wf
 */
class KrDocStateCardsViewInfo {
  //#region Common

  /**
   * View identifier for "KrDocStateCards": {d9534c6c-ec26-4de9-be78-5df833b70f43}.
   */
   readonly ID: guid = 'd9534c6c-ec26-4de9-be78-5df833b70f43';

  /**
   * View name for "KrDocStateCards".
   */
   readonly Alias: string = 'KrDocStateCards';

  /**
   * View caption for "KrDocStateCards".
   */
   readonly Caption: string = '$Views_Names_KrDocStateCards';

  /**
   * View group for "KrDocStateCards".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: StateID
   * Caption: $Views_KrDocStates_ID.
   */
   readonly ColumnStateID: ViewObject = new ViewObject(0, 'StateID', '$Views_KrDocStates_ID');

  /**
   * ID:1
   * Alias: StateName
   * Caption: $Views_KrDocStates_Name.
   */
   readonly ColumnStateName: ViewObject = new ViewObject(1, 'StateName', '$Views_KrDocStates_Name');

  /**
   * ID:2
   * Alias: PartitionID.
   */
   readonly ColumnPartitionID: ViewObject = new ViewObject(2, 'PartitionID');

  /**
   * ID:3
   * Alias: PartitionName
   * Caption: $Views_KrDocStates_Partition.
   */
   readonly ColumnPartitionName: ViewObject = new ViewObject(3, 'PartitionName', '$Views_KrDocStates_Partition');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: ID
   * Caption: $Views_KrDocStates_ID.
   */
   readonly ParamID: ViewObject = new ViewObject(0, 'ID', '$Views_KrDocStates_ID');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_KrDocStates_Name.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_KrDocStates_Name');

  /**
   * ID:2
   * Alias: Partition
   * Caption: $Views_KrDocStates_Partition.
   */
   readonly ParamPartition: ViewObject = new ViewObject(2, 'Partition', '$Views_KrDocStates_Partition');

  //#endregion
}

//#endregion

//#region KrDocStates

/**
 * ID: {51e63141-3564-4819-8bb8-c324cf772aae}
 * Alias: KrDocStates
 * Caption: $Views_Names_KrDocStates
 * Group: Kr Wf
 */
class KrDocStatesViewInfo {
  //#region Common

  /**
   * View identifier for "KrDocStates": {51e63141-3564-4819-8bb8-c324cf772aae}.
   */
   readonly ID: guid = '51e63141-3564-4819-8bb8-c324cf772aae';

  /**
   * View name for "KrDocStates".
   */
   readonly Alias: string = 'KrDocStates';

  /**
   * View caption for "KrDocStates".
   */
   readonly Caption: string = '$Views_Names_KrDocStates';

  /**
   * View group for "KrDocStates".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: StateID.
   */
   readonly ColumnStateID: ViewObject = new ViewObject(0, 'StateID');

  /**
   * ID:1
   * Alias: StateName
   * Caption: $Views_KrDocStates_Name.
   */
   readonly ColumnStateName: ViewObject = new ViewObject(1, 'StateName', '$Views_KrDocStates_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_KrDocStates_Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_KrDocStates_Name');

  //#endregion
}

//#endregion

//#region KrDocTypes

/**
 * ID: {f85d195b-7e93-4c09-830c-c9564c450f23}
 * Alias: KrDocTypes
 * Caption: $Views_Names_KrDocTypes
 * Group: Kr Wf
 */
class KrDocTypesViewInfo {
  //#region Common

  /**
   * View identifier for "KrDocTypes": {f85d195b-7e93-4c09-830c-c9564c450f23}.
   */
   readonly ID: guid = 'f85d195b-7e93-4c09-830c-c9564c450f23';

  /**
   * View name for "KrDocTypes".
   */
   readonly Alias: string = 'KrDocTypes';

  /**
   * View caption for "KrDocTypes".
   */
   readonly Caption: string = '$Views_Names_KrDocTypes';

  /**
   * View group for "KrDocTypes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrDocTypeID.
   */
   readonly ColumnKrDocTypeID: ViewObject = new ViewObject(0, 'KrDocTypeID');

  /**
   * ID:1
   * Alias: KrDocTypeTitle
   * Caption: $Views_KrDocTypes_Name.
   */
   readonly ColumnKrDocTypeTitle: ViewObject = new ViewObject(1, 'KrDocTypeTitle', '$Views_KrDocTypes_Name');

  /**
   * ID:2
   * Alias: KrDocCardTypeCaption
   * Caption: $Views_KrDocTypes_CardType.
   */
   readonly ColumnKrDocCardTypeCaption: ViewObject = new ViewObject(2, 'KrDocCardTypeCaption', '$Views_KrDocTypes_CardType');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_KrDocTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_KrDocTypes_Name_Param');

  //#endregion
}

//#endregion

//#region KrFilteredStageGroups

/**
 * ID: {888b6543-2e85-45d3-8325-7a80f230560f}
 * Alias: KrFilteredStageGroups
 * Caption: $Views_Names_KrFilteredStageGroups
 * Group: Kr Wf
 */
class KrFilteredStageGroupsViewInfo {
  //#region Common

  /**
   * View identifier for "KrFilteredStageGroups": {888b6543-2e85-45d3-8325-7a80f230560f}.
   */
   readonly ID: guid = '888b6543-2e85-45d3-8325-7a80f230560f';

  /**
   * View name for "KrFilteredStageGroups".
   */
   readonly Alias: string = 'KrFilteredStageGroups';

  /**
   * View caption for "KrFilteredStageGroups".
   */
   readonly Caption: string = '$Views_Names_KrFilteredStageGroups';

  /**
   * View group for "KrFilteredStageGroups".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: StageGroupID.
   */
   readonly ColumnStageGroupID: ViewObject = new ViewObject(0, 'StageGroupID');

  /**
   * ID:1
   * Alias: StageGroupName
   * Caption: $Views_KrStageGroups_Name.
   */
   readonly ColumnStageGroupName: ViewObject = new ViewObject(1, 'StageGroupName', '$Views_KrStageGroups_Name');

  /**
   * ID:2
   * Alias: Description
   * Caption: $Views_KrStageGroups_Description.
   */
   readonly ColumnDescription: ViewObject = new ViewObject(2, 'Description', '$Views_KrStageGroups_Description');

  /**
   * ID:3
   * Alias: IsGroupReadonly
   * Caption: $Views_KrStageGroups_IsGroupReadonly.
   */
   readonly ColumnIsGroupReadonly: ViewObject = new ViewObject(3, 'IsGroupReadonly', '$Views_KrStageGroups_IsGroupReadonly');

  /**
   * ID:4
   * Alias: Order
   * Caption: $Views_KrStageGroups_Order.
   */
   readonly ColumnOrder: ViewObject = new ViewObject(4, 'Order', '$Views_KrStageGroups_Order');

  /**
   * ID:5
   * Alias: Types
   * Caption: $Views_KrStageGroups_Types.
   */
   readonly ColumnTypes: ViewObject = new ViewObject(5, 'Types', '$Views_KrStageGroups_Types');

  /**
   * ID:6
   * Alias: Roles
   * Caption: $Views_KrStageGroups_Roles.
   */
   readonly ColumnRoles: ViewObject = new ViewObject(6, 'Roles', '$Views_KrStageGroups_Roles');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: StageGroupIDParam
   * Caption: StageGroupIDParam.
   */
   readonly ParamStageGroupIDParam: ViewObject = new ViewObject(0, 'StageGroupIDParam', 'StageGroupIDParam');

  /**
   * ID:1
   * Alias: StageGroupNameParam
   * Caption: $Views_KrStageGroups_Name.
   */
   readonly ParamStageGroupNameParam: ViewObject = new ViewObject(1, 'StageGroupNameParam', '$Views_KrStageGroups_Name');

  /**
   * ID:2
   * Alias: StageGroupDescriptionParam
   * Caption: $Views_KrStageGroups_Description.
   */
   readonly ParamStageGroupDescriptionParam: ViewObject = new ViewObject(2, 'StageGroupDescriptionParam', '$Views_KrStageGroups_Description');

  /**
   * ID:3
   * Alias: TypeId
   * Caption: TypeId.
   */
   readonly ParamTypeId: ViewObject = new ViewObject(3, 'TypeId', 'TypeId');

  /**
   * ID:4
   * Alias: CardId
   * Caption: CardId.
   */
   readonly ParamCardId: ViewObject = new ViewObject(4, 'CardId', 'CardId');

  /**
   * ID:5
   * Alias: IsGroupReadonlyParam
   * Caption: $Views_KrStageGroups_IsGroupReadonly.
   */
   readonly ParamIsGroupReadonlyParam: ViewObject = new ViewObject(5, 'IsGroupReadonlyParam', '$Views_KrStageGroups_IsGroupReadonly');

  /**
   * ID:6
   * Alias: TypeParam
   * Caption: $Views_KrStageGroups_Types.
   */
   readonly ParamTypeParam: ViewObject = new ViewObject(6, 'TypeParam', '$Views_KrStageGroups_Types');

  /**
   * ID:7
   * Alias: RoleParam
   * Caption: $Views_KrStageGroups_Roles.
   */
   readonly ParamRoleParam: ViewObject = new ViewObject(7, 'RoleParam', '$Views_KrStageGroups_Roles');

  //#endregion
}

//#endregion

//#region KrFilteredStageTypes

/**
 * ID: {e046d0cf-be7c-4965-b2d4-47cb943a8a7d}
 * Alias: KrFilteredStageTypes
 * Caption: $Views_Names_KrFilteredStageTypes
 * Group: Kr Wf
 */
class KrFilteredStageTypesViewInfo {
  //#region Common

  /**
   * View identifier for "KrFilteredStageTypes": {e046d0cf-be7c-4965-b2d4-47cb943a8a7d}.
   */
   readonly ID: guid = 'e046d0cf-be7c-4965-b2d4-47cb943a8a7d';

  /**
   * View name for "KrFilteredStageTypes".
   */
   readonly Alias: string = 'KrFilteredStageTypes';

  /**
   * View caption for "KrFilteredStageTypes".
   */
   readonly Caption: string = '$Views_Names_KrFilteredStageTypes';

  /**
   * View group for "KrFilteredStageTypes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: StageTypeID.
   */
   readonly ColumnStageTypeID: ViewObject = new ViewObject(0, 'StageTypeID');

  /**
   * ID:1
   * Alias: StageTypeCaption
   * Caption: $Views_KrProcessStageTypes_Caption.
   */
   readonly ColumnStageTypeCaption: ViewObject = new ViewObject(1, 'StageTypeCaption', '$Views_KrProcessStageTypes_Caption');

  /**
   * ID:2
   * Alias: StageTypeDefaultStageName
   * Caption: $Views_KrProcessStageTypes_DefaultStageName.
   */
   readonly ColumnStageTypeDefaultStageName: ViewObject = new ViewObject(2, 'StageTypeDefaultStageName', '$Views_KrProcessStageTypes_DefaultStageName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: IsTemplate
   * Caption: IsTemplate.
   */
   readonly ParamIsTemplate: ViewObject = new ViewObject(0, 'IsTemplate', 'IsTemplate');

  /**
   * ID:1
   * Alias: TypeId
   * Caption: TypeId.
   */
   readonly ParamTypeId: ViewObject = new ViewObject(1, 'TypeId', 'TypeId');

  /**
   * ID:2
   * Alias: CardId
   * Caption: CardId.
   */
   readonly ParamCardId: ViewObject = new ViewObject(2, 'CardId', 'CardId');

  /**
   * ID:3
   * Alias: StageGroupIDParam
   * Caption: StageGroupIDParam.
   */
   readonly ParamStageGroupIDParam: ViewObject = new ViewObject(3, 'StageGroupIDParam', 'StageGroupIDParam');

  //#endregion
}

//#endregion

//#region KrForkManagementStageTypeModes

/**
 * ID: {d5a3ccbd-975c-42de-993c-e4bccfd2ac0d}
 * Alias: KrForkManagementStageTypeModes
 * Caption: $Views_Names_KrForkManagementStageTypeModes
 * Group: Kr Wf
 */
class KrForkManagementStageTypeModesViewInfo {
  //#region Common

  /**
   * View identifier for "KrForkManagementStageTypeModes": {d5a3ccbd-975c-42de-993c-e4bccfd2ac0d}.
   */
   readonly ID: guid = 'd5a3ccbd-975c-42de-993c-e4bccfd2ac0d';

  /**
   * View name for "KrForkManagementStageTypeModes".
   */
   readonly Alias: string = 'KrForkManagementStageTypeModes';

  /**
   * View caption for "KrForkManagementStageTypeModes".
   */
   readonly Caption: string = '$Views_Names_KrForkManagementStageTypeModes';

  /**
   * View group for "KrForkManagementStageTypeModes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: RefName.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', 'RefName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', 'Name');

  //#endregion
}

//#endregion

//#region KrManagerTasks

/**
 * ID: {98e09dab-c265-46e0-96ae-0a81cef3fa20}
 * Alias: KrManagerTasks
 * Caption: $Views_Names_KrManagerTasks
 * Group: Kr Wf
 */
class KrManagerTasksViewInfo {
  //#region Common

  /**
   * View identifier for "KrManagerTasks": {98e09dab-c265-46e0-96ae-0a81cef3fa20}.
   */
   readonly ID: guid = '98e09dab-c265-46e0-96ae-0a81cef3fa20';

  /**
   * View name for "KrManagerTasks".
   */
   readonly Alias: string = 'KrManagerTasks';

  /**
   * View caption for "KrManagerTasks".
   */
   readonly Caption: string = '$Views_Names_KrManagerTasks';

  /**
   * View group for "KrManagerTasks".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: Caption
   * Caption: Caption.
   */
   readonly ColumnCaption: ViewObject = new ViewObject(1, 'Caption', 'Caption');

  /**
   * ID:2
   * Alias: ActiveImage
   * Caption: ActiveImage.
   */
   readonly ColumnActiveImage: ViewObject = new ViewObject(2, 'ActiveImage', 'ActiveImage');

  /**
   * ID:3
   * Alias: InactiveImage
   * Caption: InactiveImage.
   */
   readonly ColumnInactiveImage: ViewObject = new ViewObject(3, 'InactiveImage', 'InactiveImage');

  /**
   * ID:4
   * Alias: Count.
   */
   readonly ColumnCount: ViewObject = new ViewObject(4, 'Count');

  //#endregion
}

//#endregion

//#region KrPermissionAclGenerationRules

/**
 * ID: {8adc6a95-fd78-4efa-922d-43c4c4838e39}
 * Alias: KrPermissionAclGenerationRules
 * Caption: $Views_Names_KrPermissionAclGenerationRules
 * Group: Kr Wf
 */
class KrPermissionAclGenerationRulesViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionAclGenerationRules": {8adc6a95-fd78-4efa-922d-43c4c4838e39}.
   */
   readonly ID: guid = '8adc6a95-fd78-4efa-922d-43c4c4838e39';

  /**
   * View name for "KrPermissionAclGenerationRules".
   */
   readonly Alias: string = 'KrPermissionAclGenerationRules';

  /**
   * View caption for "KrPermissionAclGenerationRules".
   */
   readonly Caption: string = '$Views_Names_KrPermissionAclGenerationRules';

  /**
   * View group for "KrPermissionAclGenerationRules".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrPermissionAclGenerationRuleID.
   */
   readonly ColumnKrPermissionAclGenerationRuleID: ViewObject = new ViewObject(0, 'KrPermissionAclGenerationRuleID');

  /**
   * ID:1
   * Alias: KrPermissionAclGenerationRuleName
   * Caption: $Views_KrPermissions_AclGenerationRule.
   */
   readonly ColumnKrPermissionAclGenerationRuleName: ViewObject = new ViewObject(1, 'KrPermissionAclGenerationRuleName', '$Views_KrPermissions_AclGenerationRule');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: AccessRule
   * Caption: Access rule.
   */
   readonly ParamAccessRule: ViewObject = new ViewObject(0, 'AccessRule', 'Access rule');

  //#endregion
}

//#endregion

//#region KrPermissionFlags

/**
 * ID: {eb357452-657c-40cd-a2f4-f0214d0ac957}
 * Alias: KrPermissionFlags
 * Caption: $Views_Names_KrPermissionFlags
 * Group: Kr Wf
 */
class KrPermissionFlagsViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionFlags": {eb357452-657c-40cd-a2f4-f0214d0ac957}.
   */
   readonly ID: guid = 'eb357452-657c-40cd-a2f4-f0214d0ac957';

  /**
   * View name for "KrPermissionFlags".
   */
   readonly Alias: string = 'KrPermissionFlags';

  /**
   * View caption for "KrPermissionFlags".
   */
   readonly Caption: string = '$Views_Names_KrPermissionFlags';

  /**
   * View group for "KrPermissionFlags".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: FlagID.
   */
   readonly ColumnFlagID: ViewObject = new ViewObject(0, 'FlagID');

  /**
   * ID:1
   * Alias: FlagCaption
   * Caption: $Views_KrPermissionFlags_Name.
   */
   readonly ColumnFlagCaption: ViewObject = new ViewObject(1, 'FlagCaption', '$Views_KrPermissionFlags_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CaptionParam
   * Caption: $Views_KrPermissionFlags_Name_Param.
   */
   readonly ParamCaptionParam: ViewObject = new ViewObject(0, 'CaptionParam', '$Views_KrPermissionFlags_Name_Param');

  //#endregion
}

//#endregion

//#region KrPermissionRoles

/**
 * ID: {34026b2a-6699-425c-8669-5ad5c75945f9}
 * Alias: KrPermissionRoles
 * Caption: $Views_Names_KrPermissionRoles
 * Group: Kr Wf
 */
class KrPermissionRolesViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionRoles": {34026b2a-6699-425c-8669-5ad5c75945f9}.
   */
   readonly ID: guid = '34026b2a-6699-425c-8669-5ad5c75945f9';

  /**
   * View name for "KrPermissionRoles".
   */
   readonly Alias: string = 'KrPermissionRoles';

  /**
   * View caption for "KrPermissionRoles".
   */
   readonly Caption: string = '$Views_Names_KrPermissionRoles';

  /**
   * View group for "KrPermissionRoles".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrPermissionRoleID.
   */
   readonly ColumnKrPermissionRoleID: ViewObject = new ViewObject(0, 'KrPermissionRoleID');

  /**
   * ID:1
   * Alias: KrPermissionRoleName
   * Caption: $Views_KrPermissions_Roles.
   */
   readonly ColumnKrPermissionRoleName: ViewObject = new ViewObject(1, 'KrPermissionRoleName', '$Views_KrPermissions_Roles');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: AccessRule
   * Caption: Access rule.
   */
   readonly ParamAccessRule: ViewObject = new ViewObject(0, 'AccessRule', 'Access rule');

  //#endregion
}

//#endregion

//#region KrPermissionRuleAccessSettings

/**
 * ID: {e9005e6d-e9d0-4643-86aa-8f0c72826e28}
 * Alias: KrPermissionRuleAccessSettings
 * Caption: $Views_Names_KrPermissionRuleAccessSettings
 * Group: Kr Wf
 */
class KrPermissionRuleAccessSettingsViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionRuleAccessSettings": {e9005e6d-e9d0-4643-86aa-8f0c72826e28}.
   */
   readonly ID: guid = 'e9005e6d-e9d0-4643-86aa-8f0c72826e28';

  /**
   * View name for "KrPermissionRuleAccessSettings".
   */
   readonly Alias: string = 'KrPermissionRuleAccessSettings';

  /**
   * View caption for "KrPermissionRuleAccessSettings".
   */
   readonly Caption: string = '$Views_Names_KrPermissionRuleAccessSettings';

  /**
   * View group for "KrPermissionRuleAccessSettings".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: AccessSettingID.
   */
   readonly ColumnAccessSettingID: ViewObject = new ViewObject(0, 'AccessSettingID');

  /**
   * ID:1
   * Alias: AccessSettingName
   * Caption: $Views_KrPermissionRuleAccessSettings_Name.
   */
   readonly ColumnAccessSettingName: ViewObject = new ViewObject(1, 'AccessSettingName', '$Views_KrPermissionRuleAccessSettings_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_KrPermissionRuleAccessSettings_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_KrPermissionRuleAccessSettings_Name_Param');

  /**
   * ID:1
   * Alias: SectionType
   * Caption: Section type.
   */
   readonly ParamSectionType: ViewObject = new ViewObject(1, 'SectionType', 'Section type');

  /**
   * ID:2
   * Alias: WithMaskLevel
   * Caption: With mask level.
   */
   readonly ParamWithMaskLevel: ViewObject = new ViewObject(2, 'WithMaskLevel', 'With mask level');

  //#endregion
}

//#endregion

//#region KrPermissions

/**
 * ID: {42facec2-7986-4456-b089-972413cf8e89}
 * Alias: KrPermissions
 * Caption: $Views_Names_KrPermissions
 * Group: Kr Wf
 */
class KrPermissionsViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissions": {42facec2-7986-4456-b089-972413cf8e89}.
   */
   readonly ID: guid = '42facec2-7986-4456-b089-972413cf8e89';

  /**
   * View name for "KrPermissions".
   */
   readonly Alias: string = 'KrPermissions';

  /**
   * View caption for "KrPermissions".
   */
   readonly Caption: string = '$Views_Names_KrPermissions';

  /**
   * View group for "KrPermissions".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrPermissionsID.
   */
   readonly ColumnKrPermissionsID: ViewObject = new ViewObject(0, 'KrPermissionsID');

  /**
   * ID:1
   * Alias: KrPermissionsCaption
   * Caption: $Views_KrPermissions_Name.
   */
   readonly ColumnKrPermissionsCaption: ViewObject = new ViewObject(1, 'KrPermissionsCaption', '$Views_KrPermissions_Name');

  /**
   * ID:2
   * Alias: KrPermissionsDescription
   * Caption: $Views_KrPermissions_Description.
   */
   readonly ColumnKrPermissionsDescription: ViewObject = new ViewObject(2, 'KrPermissionsDescription', '$Views_KrPermissions_Description');

  /**
   * ID:3
   * Alias: KrPermissionsPriority
   * Caption: $Views_KrPermissions_Priority.
   */
   readonly ColumnKrPermissionsPriority: ViewObject = new ViewObject(3, 'KrPermissionsPriority', '$Views_KrPermissions_Priority');

  /**
   * ID:4
   * Alias: KrPermissionsIsDisabled
   * Caption: $Views_KrPermissions_IsDisabled.
   */
   readonly ColumnKrPermissionsIsDisabled: ViewObject = new ViewObject(4, 'KrPermissionsIsDisabled', '$Views_KrPermissions_IsDisabled');

  /**
   * ID:5
   * Alias: KrPermissionsAlwaysCheck
   * Caption: $Views_KrPermissions_AlwaysCheck.
   */
   readonly ColumnKrPermissionsAlwaysCheck: ViewObject = new ViewObject(5, 'KrPermissionsAlwaysCheck', '$Views_KrPermissions_AlwaysCheck');

  /**
   * ID:6
   * Alias: KrPermissionsUseExtendedSettings
   * Caption: $Views_KrPermissions_UseExtendedSettings.
   */
   readonly ColumnKrPermissionsUseExtendedSettings: ViewObject = new ViewObject(6, 'KrPermissionsUseExtendedSettings', '$Views_KrPermissions_UseExtendedSettings');

  /**
   * ID:7
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(7, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_KrPermissions_Caption_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_KrPermissions_Caption_Param');

  /**
   * ID:1
   * Alias: Description
   * Caption: $Views_KrPermissions_Description_Param.
   */
   readonly ParamDescription: ViewObject = new ViewObject(1, 'Description', '$Views_KrPermissions_Description_Param');

  /**
   * ID:2
   * Alias: Type
   * Caption: $Views_KrPermissions_Type_Param.
   */
   readonly ParamType: ViewObject = new ViewObject(2, 'Type', '$Views_KrPermissions_Type_Param');

  /**
   * ID:3
   * Alias: State
   * Caption: $Views_KrPermissions_State_Param.
   */
   readonly ParamState: ViewObject = new ViewObject(3, 'State', '$Views_KrPermissions_State_Param');

  /**
   * ID:4
   * Alias: Role
   * Caption: $Views_KrPermissions_Role_Param.
   */
   readonly ParamRole: ViewObject = new ViewObject(4, 'Role', '$Views_KrPermissions_Role_Param');

  /**
   * ID:5
   * Alias: AclGenerationRule
   * Caption: $Views_KrPermissions_AclGenerationRule_Param.
   */
   readonly ParamAclGenerationRule: ViewObject = new ViewObject(5, 'AclGenerationRule', '$Views_KrPermissions_AclGenerationRule_Param');

  /**
   * ID:6
   * Alias: Permission
   * Caption: $Views_KrPermissions_Permission_Param.
   */
   readonly ParamPermission: ViewObject = new ViewObject(6, 'Permission', '$Views_KrPermissions_Permission_Param');

  /**
   * ID:7
   * Alias: User
   * Caption: $Views_KrPermissions_User_Param.
   */
   readonly ParamUser: ViewObject = new ViewObject(7, 'User', '$Views_KrPermissions_User_Param');

  /**
   * ID:8
   * Alias: Priority
   * Caption: $Views_KrPermissions_Priority_Param.
   */
   readonly ParamPriority: ViewObject = new ViewObject(8, 'Priority', '$Views_KrPermissions_Priority_Param');

  /**
   * ID:9
   * Alias: IsDisabled
   * Caption: $Views_KrPermissions_IsDisabled_Param.
   */
   readonly ParamIsDisabled: ViewObject = new ViewObject(9, 'IsDisabled', '$Views_KrPermissions_IsDisabled_Param');

  /**
   * ID:10
   * Alias: IsRequired
   * Caption: $Views_KrPermissions_AlwaysCheck_Param.
   */
   readonly ParamIsRequired: ViewObject = new ViewObject(10, 'IsRequired', '$Views_KrPermissions_AlwaysCheck_Param');

  /**
   * ID:11
   * Alias: IsExtended
   * Caption: $Views_KrPermissions_UseExtendedSettings_Param.
   */
   readonly ParamIsExtended: ViewObject = new ViewObject(11, 'IsExtended', '$Views_KrPermissions_UseExtendedSettings_Param');

  //#endregion
}

//#endregion

//#region KrPermissionsControlTypes

/**
 * ID: {8053d28d-666a-4997-b0ef-aff1298c4aaf}
 * Alias: KrPermissionsControlTypes
 * Caption: $Views_Names_KrPermissionsControlTypes
 * Group: Kr Wf
 */
class KrPermissionsControlTypesViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionsControlTypes": {8053d28d-666a-4997-b0ef-aff1298c4aaf}.
   */
   readonly ID: guid = '8053d28d-666a-4997-b0ef-aff1298c4aaf';

  /**
   * View name for "KrPermissionsControlTypes".
   */
   readonly Alias: string = 'KrPermissionsControlTypes';

  /**
   * View caption for "KrPermissionsControlTypes".
   */
   readonly Caption: string = '$Views_Names_KrPermissionsControlTypes';

  /**
   * View group for "KrPermissionsControlTypes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ControlTypeID.
   */
   readonly ColumnControlTypeID: ViewObject = new ViewObject(0, 'ControlTypeID');

  /**
   * ID:1
   * Alias: ControlTypeName
   * Caption: $Views_KrPermissionsControlTypes_Name.
   */
   readonly ColumnControlTypeName: ViewObject = new ViewObject(1, 'ControlTypeName', '$Views_KrPermissionsControlTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_KrPermissionsControlTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_KrPermissionsControlTypes_Name_Param');

  //#endregion
}

//#endregion

//#region KrPermissionsExtendedCards

/**
 * ID: {1175f833-a028-481a-be77-69bab81a01a2}
 * Alias: KrPermissionsExtendedCards
 * Caption: $Views_Names_KrPermissionsExtendedCards
 * Group: Kr Wf
 */
class KrPermissionsExtendedCardsViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionsExtendedCards": {1175f833-a028-481a-be77-69bab81a01a2}.
   */
   readonly ID: guid = '1175f833-a028-481a-be77-69bab81a01a2';

  /**
   * View name for "KrPermissionsExtendedCards".
   */
   readonly Alias: string = 'KrPermissionsExtendedCards';

  /**
   * View caption for "KrPermissionsExtendedCards".
   */
   readonly Caption: string = '$Views_Names_KrPermissionsExtendedCards';

  /**
   * View group for "KrPermissionsExtendedCards".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrPermissionsID.
   */
   readonly ColumnKrPermissionsID: ViewObject = new ViewObject(0, 'KrPermissionsID');

  /**
   * ID:1
   * Alias: KrPermissionsCaption
   * Caption: $Views_KrPermissionsExtendedCards_AccessRule.
   */
   readonly ColumnKrPermissionsCaption: ViewObject = new ViewObject(1, 'KrPermissionsCaption', '$Views_KrPermissionsExtendedCards_AccessRule');

  /**
   * ID:2
   * Alias: KrPermissionsPriority
   * Caption: $Views_KrPermissionsExtendedCards_Priority.
   */
   readonly ColumnKrPermissionsPriority: ViewObject = new ViewObject(2, 'KrPermissionsPriority', '$Views_KrPermissionsExtendedCards_Priority');

  /**
   * ID:3
   * Alias: KrPermissionsSection
   * Caption: $Views_KrPermissionsExtendedCards_Section.
   */
   readonly ColumnKrPermissionsSection: ViewObject = new ViewObject(3, 'KrPermissionsSection', '$Views_KrPermissionsExtendedCards_Section');

  /**
   * ID:4
   * Alias: KrPermissionsFields
   * Caption: $Views_KrPermissionsExtendedCards_Fields.
   */
   readonly ColumnKrPermissionsFields: ViewObject = new ViewObject(4, 'KrPermissionsFields', '$Views_KrPermissionsExtendedCards_Fields');

  /**
   * ID:5
   * Alias: KrPermissionsAccessSetting
   * Caption: $Views_KrPermissionsExtendedCards_AccessSetting.
   */
   readonly ColumnKrPermissionsAccessSetting: ViewObject = new ViewObject(5, 'KrPermissionsAccessSetting', '$Views_KrPermissionsExtendedCards_AccessSetting');

  /**
   * ID:6
   * Alias: KrPermissionsIsHidden
   * Caption: $Views_KrPermissionsExtendedCards_Hide.
   */
   readonly ColumnKrPermissionsIsHidden: ViewObject = new ViewObject(6, 'KrPermissionsIsHidden', '$Views_KrPermissionsExtendedCards_Hide');

  /**
   * ID:7
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(7, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_KrPermissionsExtendedCards_Name_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_KrPermissionsExtendedCards_Name_Param');

  /**
   * ID:1
   * Alias: Priority
   * Caption: $Views_KrPermissionsExtendedCards_Priority_Param.
   */
   readonly ParamPriority: ViewObject = new ViewObject(1, 'Priority', '$Views_KrPermissionsExtendedCards_Priority_Param');

  /**
   * ID:2
   * Alias: AccessRule
   * Caption: $Views_KrPermissionsExtendedCards_AccessRule_Param.
   */
   readonly ParamAccessRule: ViewObject = new ViewObject(2, 'AccessRule', '$Views_KrPermissionsExtendedCards_AccessRule_Param');

  /**
   * ID:3
   * Alias: Section
   * Caption: $Views_KrPermissionsExtendedCards_Section_Param.
   */
   readonly ParamSection: ViewObject = new ViewObject(3, 'Section', '$Views_KrPermissionsExtendedCards_Section_Param');

  /**
   * ID:4
   * Alias: Field
   * Caption: $Views_KrPermissionsExtendedCards_Field_Param.
   */
   readonly ParamField: ViewObject = new ViewObject(4, 'Field', '$Views_KrPermissionsExtendedCards_Field_Param');

  /**
   * ID:5
   * Alias: AccessSetting
   * Caption: $Views_KrPermissionsExtendedCards_AccessSetting_Param.
   */
   readonly ParamAccessSetting: ViewObject = new ViewObject(5, 'AccessSetting', '$Views_KrPermissionsExtendedCards_AccessSetting_Param');

  /**
   * ID:6
   * Alias: IsHidden
   * Caption: $Views_KrPermissionsExtendedCards_Hide_Param.
   */
   readonly ParamIsHidden: ViewObject = new ViewObject(6, 'IsHidden', '$Views_KrPermissionsExtendedCards_Hide_Param');

  //#endregion
}

//#endregion

//#region KrPermissionsExtendedFiles

/**
 * ID: {76a033f1-404b-4dee-9305-9cc8bf8c22f0}
 * Alias: KrPermissionsExtendedFiles
 * Caption: $Views_Names_KrPermissionsExtendedFiles
 * Group: Kr Wf
 */
class KrPermissionsExtendedFilesViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionsExtendedFiles": {76a033f1-404b-4dee-9305-9cc8bf8c22f0}.
   */
   readonly ID: guid = '76a033f1-404b-4dee-9305-9cc8bf8c22f0';

  /**
   * View name for "KrPermissionsExtendedFiles".
   */
   readonly Alias: string = 'KrPermissionsExtendedFiles';

  /**
   * View caption for "KrPermissionsExtendedFiles".
   */
   readonly Caption: string = '$Views_Names_KrPermissionsExtendedFiles';

  /**
   * View group for "KrPermissionsExtendedFiles".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrPermissionsID.
   */
   readonly ColumnKrPermissionsID: ViewObject = new ViewObject(0, 'KrPermissionsID');

  /**
   * ID:1
   * Alias: KrPermissionsCaption
   * Caption: $Views_KrPermissionsExtendedFiles_AccessRule.
   */
   readonly ColumnKrPermissionsCaption: ViewObject = new ViewObject(1, 'KrPermissionsCaption', '$Views_KrPermissionsExtendedFiles_AccessRule');

  /**
   * ID:2
   * Alias: KrPermissionsPriority
   * Caption: $Views_KrPermissionsExtendedFiles_Priority.
   */
   readonly ColumnKrPermissionsPriority: ViewObject = new ViewObject(2, 'KrPermissionsPriority', '$Views_KrPermissionsExtendedFiles_Priority');

  /**
   * ID:3
   * Alias: KrPermissionsExtensions
   * Caption: $Views_KrPermissionsExtendedFiles_Extensions.
   */
   readonly ColumnKrPermissionsExtensions: ViewObject = new ViewObject(3, 'KrPermissionsExtensions', '$Views_KrPermissionsExtendedFiles_Extensions');

  /**
   * ID:4
   * Alias: KrPermissionsCategories
   * Caption: $Views_KrPermissionsExtendedFiles_FileCategories.
   */
   readonly ColumnKrPermissionsCategories: ViewObject = new ViewObject(4, 'KrPermissionsCategories', '$Views_KrPermissionsExtendedFiles_FileCategories');

  /**
   * ID:5
   * Alias: KrPermissionsFileCheckRule
   * Caption: $Views_KrPermissionsExtendedFiles_FileCheckRule.
   */
   readonly ColumnKrPermissionsFileCheckRule: ViewObject = new ViewObject(5, 'KrPermissionsFileCheckRule', '$Views_KrPermissionsExtendedFiles_FileCheckRule');

  /**
   * ID:6
   * Alias: KrPermissionsReadAccessSetting
   * Caption: $Views_KrPermissionsExtendedFiles_ReadAccessSetting.
   */
   readonly ColumnKrPermissionsReadAccessSetting: ViewObject = new ViewObject(6, 'KrPermissionsReadAccessSetting', '$Views_KrPermissionsExtendedFiles_ReadAccessSetting');

  /**
   * ID:7
   * Alias: KrPermissionsEditAccessSetting
   * Caption: $Views_KrPermissionsExtendedFiles_EditAccessSetting.
   */
   readonly ColumnKrPermissionsEditAccessSetting: ViewObject = new ViewObject(7, 'KrPermissionsEditAccessSetting', '$Views_KrPermissionsExtendedFiles_EditAccessSetting');

  /**
   * ID:8
   * Alias: KrPermissionsDeleteAccessSetting
   * Caption: $Views_KrPermissionsExtendedFiles_DeleteAccessSetting.
   */
   readonly ColumnKrPermissionsDeleteAccessSetting: ViewObject = new ViewObject(8, 'KrPermissionsDeleteAccessSetting', '$Views_KrPermissionsExtendedFiles_DeleteAccessSetting');

  /**
   * ID:9
   * Alias: KrPermissionsSignAccessSetting
   * Caption: $Views_KrPermissionsExtendedFiles_SignAccessSetting.
   */
   readonly ColumnKrPermissionsSignAccessSetting: ViewObject = new ViewObject(9, 'KrPermissionsSignAccessSetting', '$Views_KrPermissionsExtendedFiles_SignAccessSetting');

  /**
   * ID:10
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(10, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_KrPermissionsExtendedFiles_Name_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_KrPermissionsExtendedFiles_Name_Param');

  /**
   * ID:1
   * Alias: AccessRule
   * Caption: $Views_KrPermissionsExtendedFiles_AccessRule_Param.
   */
   readonly ParamAccessRule: ViewObject = new ViewObject(1, 'AccessRule', '$Views_KrPermissionsExtendedFiles_AccessRule_Param');

  /**
   * ID:2
   * Alias: Extensions
   * Caption: $Views_KrPermissionsExtendedFiles_Extensions_Param.
   */
   readonly ParamExtensions: ViewObject = new ViewObject(2, 'Extensions', '$Views_KrPermissionsExtendedFiles_Extensions_Param');

  /**
   * ID:3
   * Alias: Category
   * Caption: $Views_KrPermissionsExtendedFiles_FileCategory_Param.
   */
   readonly ParamCategory: ViewObject = new ViewObject(3, 'Category', '$Views_KrPermissionsExtendedFiles_FileCategory_Param');

  /**
   * ID:4
   * Alias: FileCheckRule
   * Caption: $Views_KrPermissionsExtendedFiles_FileCheckRule_Param.
   */
   readonly ParamFileCheckRule: ViewObject = new ViewObject(4, 'FileCheckRule', '$Views_KrPermissionsExtendedFiles_FileCheckRule_Param');

  /**
   * ID:5
   * Alias: ReadAccessSetting
   * Caption: $Views_KrPermissionsExtendedFiles_ReadAccessSetting_Param.
   */
   readonly ParamReadAccessSetting: ViewObject = new ViewObject(5, 'ReadAccessSetting', '$Views_KrPermissionsExtendedFiles_ReadAccessSetting_Param');

  /**
   * ID:6
   * Alias: EditAccessSetting
   * Caption: $Views_KrPermissionsExtendedFiles_EditAccessSetting_Param.
   */
   readonly ParamEditAccessSetting: ViewObject = new ViewObject(6, 'EditAccessSetting', '$Views_KrPermissionsExtendedFiles_EditAccessSetting_Param');

  /**
   * ID:7
   * Alias: DeleteAccessSetting
   * Caption: $Views_KrPermissionsExtendedFiles_DeleteAccessSetting_Param.
   */
   readonly ParamDeleteAccessSetting: ViewObject = new ViewObject(7, 'DeleteAccessSetting', '$Views_KrPermissionsExtendedFiles_DeleteAccessSetting_Param');

  /**
   * ID:8
   * Alias: SignAccessSetting
   * Caption: $Views_KrPermissionsExtendedFiles_SignAccessSetting_Param.
   */
   readonly ParamSignAccessSetting: ViewObject = new ViewObject(8, 'SignAccessSetting', '$Views_KrPermissionsExtendedFiles_SignAccessSetting_Param');

  //#endregion
}

//#endregion

//#region KrPermissionsExtendedMandatory

/**
 * ID: {73970cb5-843d-49ec-821f-cb069463c1aa}
 * Alias: KrPermissionsExtendedMandatory
 * Caption: $Views_Names_KrPermissionsExtendedMandatory
 * Group: Kr Wf
 */
class KrPermissionsExtendedMandatoryViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionsExtendedMandatory": {73970cb5-843d-49ec-821f-cb069463c1aa}.
   */
   readonly ID: guid = '73970cb5-843d-49ec-821f-cb069463c1aa';

  /**
   * View name for "KrPermissionsExtendedMandatory".
   */
   readonly Alias: string = 'KrPermissionsExtendedMandatory';

  /**
   * View caption for "KrPermissionsExtendedMandatory".
   */
   readonly Caption: string = '$Views_Names_KrPermissionsExtendedMandatory';

  /**
   * View group for "KrPermissionsExtendedMandatory".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrPermissionsID.
   */
   readonly ColumnKrPermissionsID: ViewObject = new ViewObject(0, 'KrPermissionsID');

  /**
   * ID:1
   * Alias: KrPermissionsCaption
   * Caption: $Views_KrPermissionsExtendedMandatory_AccessRule.
   */
   readonly ColumnKrPermissionsCaption: ViewObject = new ViewObject(1, 'KrPermissionsCaption', '$Views_KrPermissionsExtendedMandatory_AccessRule');

  /**
   * ID:2
   * Alias: KrPermissionsSection
   * Caption: $Views_KrPermissionsExtendedMandatory_Section.
   */
   readonly ColumnKrPermissionsSection: ViewObject = new ViewObject(2, 'KrPermissionsSection', '$Views_KrPermissionsExtendedMandatory_Section');

  /**
   * ID:3
   * Alias: KrPermissionsFields
   * Caption: $Views_KrPermissionsExtendedMandatory_Fields.
   */
   readonly ColumnKrPermissionsFields: ViewObject = new ViewObject(3, 'KrPermissionsFields', '$Views_KrPermissionsExtendedMandatory_Fields');

  /**
   * ID:4
   * Alias: KrPermissionsValidationType
   * Caption: $Views_KrPermissionsExtendedMandatory_ValidationType.
   */
   readonly ColumnKrPermissionsValidationType: ViewObject = new ViewObject(4, 'KrPermissionsValidationType', '$Views_KrPermissionsExtendedMandatory_ValidationType');

  /**
   * ID:5
   * Alias: KrPermissionsTaskTypes
   * Caption: $Views_KrPermissionsExtendedMandatory_TaskTypes.
   */
   readonly ColumnKrPermissionsTaskTypes: ViewObject = new ViewObject(5, 'KrPermissionsTaskTypes', '$Views_KrPermissionsExtendedMandatory_TaskTypes');

  /**
   * ID:6
   * Alias: KrPermissionsCompletionOptions
   * Caption: $Views_KrPermissionsExtendedMandatory_CompletionOptions.
   */
   readonly ColumnKrPermissionsCompletionOptions: ViewObject = new ViewObject(6, 'KrPermissionsCompletionOptions', '$Views_KrPermissionsExtendedMandatory_CompletionOptions');

  /**
   * ID:7
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(7, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_KrPermissionsExtendedMandatory_Name_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_KrPermissionsExtendedMandatory_Name_Param');

  /**
   * ID:1
   * Alias: AccessRule
   * Caption: $Views_KrPermissionsExtendedMandatory_AccessRule_Param.
   */
   readonly ParamAccessRule: ViewObject = new ViewObject(1, 'AccessRule', '$Views_KrPermissionsExtendedMandatory_AccessRule_Param');

  /**
   * ID:2
   * Alias: Section
   * Caption: $Views_KrPermissionsExtendedMandatory_Section_Param.
   */
   readonly ParamSection: ViewObject = new ViewObject(2, 'Section', '$Views_KrPermissionsExtendedMandatory_Section_Param');

  /**
   * ID:3
   * Alias: Field
   * Caption: $Views_KrPermissionsExtendedMandatory_Field_Param.
   */
   readonly ParamField: ViewObject = new ViewObject(3, 'Field', '$Views_KrPermissionsExtendedMandatory_Field_Param');

  /**
   * ID:4
   * Alias: ValidationType
   * Caption: $Views_KrPermissionsExtendedMandatory_ValidationType_Param.
   */
   readonly ParamValidationType: ViewObject = new ViewObject(4, 'ValidationType', '$Views_KrPermissionsExtendedMandatory_ValidationType_Param');

  /**
   * ID:5
   * Alias: TaskType
   * Caption: $Views_KrPermissionsExtendedMandatory_TaskType_Param.
   */
   readonly ParamTaskType: ViewObject = new ViewObject(5, 'TaskType', '$Views_KrPermissionsExtendedMandatory_TaskType_Param');

  /**
   * ID:6
   * Alias: CompletionOption
   * Caption: $Views_KrPermissionsExtendedMandatory_CompletionOption_Param.
   */
   readonly ParamCompletionOption: ViewObject = new ViewObject(6, 'CompletionOption', '$Views_KrPermissionsExtendedMandatory_CompletionOption_Param');

  //#endregion
}

//#endregion

//#region KrPermissionsExtendedTasks

/**
 * ID: {3d026d41-fe09-4b2d-bd5f-bb482c6c7726}
 * Alias: KrPermissionsExtendedTasks
 * Caption: $Views_Names_KrPermissionsExtendedTasks
 * Group: Kr Wf
 */
class KrPermissionsExtendedTasksViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionsExtendedTasks": {3d026d41-fe09-4b2d-bd5f-bb482c6c7726}.
   */
   readonly ID: guid = '3d026d41-fe09-4b2d-bd5f-bb482c6c7726';

  /**
   * View name for "KrPermissionsExtendedTasks".
   */
   readonly Alias: string = 'KrPermissionsExtendedTasks';

  /**
   * View caption for "KrPermissionsExtendedTasks".
   */
   readonly Caption: string = '$Views_Names_KrPermissionsExtendedTasks';

  /**
   * View group for "KrPermissionsExtendedTasks".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrPermissionsID.
   */
   readonly ColumnKrPermissionsID: ViewObject = new ViewObject(0, 'KrPermissionsID');

  /**
   * ID:1
   * Alias: KrPermissionsCaption
   * Caption: $Views_KrPermissionsExtendedTasks_AccessRule.
   */
   readonly ColumnKrPermissionsCaption: ViewObject = new ViewObject(1, 'KrPermissionsCaption', '$Views_KrPermissionsExtendedTasks_AccessRule');

  /**
   * ID:2
   * Alias: KrPermissionsPriority
   * Caption: $Views_KrPermissionsExtendedTasks_Priority.
   */
   readonly ColumnKrPermissionsPriority: ViewObject = new ViewObject(2, 'KrPermissionsPriority', '$Views_KrPermissionsExtendedTasks_Priority');

  /**
   * ID:3
   * Alias: KrPermissionsTaskTypes
   * Caption: $Views_KrPermissionsExtendedTasks_TaskTypes.
   */
   readonly ColumnKrPermissionsTaskTypes: ViewObject = new ViewObject(3, 'KrPermissionsTaskTypes', '$Views_KrPermissionsExtendedTasks_TaskTypes');

  /**
   * ID:4
   * Alias: KrPermissionsSection
   * Caption: $Views_KrPermissionsExtendedTasks_Section.
   */
   readonly ColumnKrPermissionsSection: ViewObject = new ViewObject(4, 'KrPermissionsSection', '$Views_KrPermissionsExtendedTasks_Section');

  /**
   * ID:5
   * Alias: KrPermissionsFields
   * Caption: $Views_KrPermissionsExtendedTasks_Fields.
   */
   readonly ColumnKrPermissionsFields: ViewObject = new ViewObject(5, 'KrPermissionsFields', '$Views_KrPermissionsExtendedTasks_Fields');

  /**
   * ID:6
   * Alias: KrPermissionsAccessSetting
   * Caption: $Views_KrPermissionsExtendedTasks_AccessSetting.
   */
   readonly ColumnKrPermissionsAccessSetting: ViewObject = new ViewObject(6, 'KrPermissionsAccessSetting', '$Views_KrPermissionsExtendedTasks_AccessSetting');

  /**
   * ID:7
   * Alias: KrPermissionsIsHidden
   * Caption: $Views_KrPermissionsExtendedTasks_Hide.
   */
   readonly ColumnKrPermissionsIsHidden: ViewObject = new ViewObject(7, 'KrPermissionsIsHidden', '$Views_KrPermissionsExtendedTasks_Hide');

  /**
   * ID:8
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(8, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_KrPermissionsExtendedTasks_Name_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_KrPermissionsExtendedTasks_Name_Param');

  /**
   * ID:1
   * Alias: AccessRule
   * Caption: $Views_KrPermissionsExtendedTasks_AccessRule_Param.
   */
   readonly ParamAccessRule: ViewObject = new ViewObject(1, 'AccessRule', '$Views_KrPermissionsExtendedTasks_AccessRule_Param');

  /**
   * ID:2
   * Alias: Priority
   * Caption: $Views_KrPermissionsExtendedTasks_Priority_Param.
   */
   readonly ParamPriority: ViewObject = new ViewObject(2, 'Priority', '$Views_KrPermissionsExtendedTasks_Priority_Param');

  /**
   * ID:3
   * Alias: TaskType
   * Caption: $Views_KrPermissionsExtendedTasks_TaskType_Param.
   */
   readonly ParamTaskType: ViewObject = new ViewObject(3, 'TaskType', '$Views_KrPermissionsExtendedTasks_TaskType_Param');

  /**
   * ID:4
   * Alias: Section
   * Caption: $Views_KrPermissionsExtendedTasks_Section_Param.
   */
   readonly ParamSection: ViewObject = new ViewObject(4, 'Section', '$Views_KrPermissionsExtendedTasks_Section_Param');

  /**
   * ID:5
   * Alias: Field
   * Caption: $Views_KrPermissionsExtendedTasks_Field_Param.
   */
   readonly ParamField: ViewObject = new ViewObject(5, 'Field', '$Views_KrPermissionsExtendedTasks_Field_Param');

  /**
   * ID:6
   * Alias: AccessSetting
   * Caption: $Views_KrPermissionsExtendedTasks_AccessSetting_Param.
   */
   readonly ParamAccessSetting: ViewObject = new ViewObject(6, 'AccessSetting', '$Views_KrPermissionsExtendedTasks_AccessSetting_Param');

  /**
   * ID:7
   * Alias: IsHidden
   * Caption: $Views_KrPermissionsExtendedTasks_Hide_Param.
   */
   readonly ParamIsHidden: ViewObject = new ViewObject(7, 'IsHidden', '$Views_KrPermissionsExtendedTasks_Hide_Param');

  //#endregion
}

//#endregion

//#region KrPermissionsExtendedVisibility

/**
 * ID: {acbfb44c-d180-4b5b-9719-5868631b998a}
 * Alias: KrPermissionsExtendedVisibility
 * Caption: $Views_Names_KrPermissionsExtendedVisibility
 * Group: Kr Wf
 */
class KrPermissionsExtendedVisibilityViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionsExtendedVisibility": {acbfb44c-d180-4b5b-9719-5868631b998a}.
   */
   readonly ID: guid = 'acbfb44c-d180-4b5b-9719-5868631b998a';

  /**
   * View name for "KrPermissionsExtendedVisibility".
   */
   readonly Alias: string = 'KrPermissionsExtendedVisibility';

  /**
   * View caption for "KrPermissionsExtendedVisibility".
   */
   readonly Caption: string = '$Views_Names_KrPermissionsExtendedVisibility';

  /**
   * View group for "KrPermissionsExtendedVisibility".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrPermissionsID.
   */
   readonly ColumnKrPermissionsID: ViewObject = new ViewObject(0, 'KrPermissionsID');

  /**
   * ID:1
   * Alias: KrPermissionsCaption
   * Caption: $Views_KrPermissionsExtendedVisibility_AccessRule.
   */
   readonly ColumnKrPermissionsCaption: ViewObject = new ViewObject(1, 'KrPermissionsCaption', '$Views_KrPermissionsExtendedVisibility_AccessRule');

  /**
   * ID:2
   * Alias: KrPermissionsControlAlias
   * Caption: $Views_KrPermissionsExtendedVisibility_ControlAlias.
   */
   readonly ColumnKrPermissionsControlAlias: ViewObject = new ViewObject(2, 'KrPermissionsControlAlias', '$Views_KrPermissionsExtendedVisibility_ControlAlias');

  /**
   * ID:3
   * Alias: KrPermissionsControlType
   * Caption: $Views_KrPermissionsExtendedVisibility_ControlType.
   */
   readonly ColumnKrPermissionsControlType: ViewObject = new ViewObject(3, 'KrPermissionsControlType', '$Views_KrPermissionsExtendedVisibility_ControlType');

  /**
   * ID:4
   * Alias: KrPermissionsIsHidden
   * Caption: $Views_KrPermissionsExtendedVisibility_HideControl.
   */
   readonly ColumnKrPermissionsIsHidden: ViewObject = new ViewObject(4, 'KrPermissionsIsHidden', '$Views_KrPermissionsExtendedVisibility_HideControl');

  /**
   * ID:5
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(5, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_KrPermissionsExtendedVisibility_Name_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_KrPermissionsExtendedVisibility_Name_Param');

  /**
   * ID:1
   * Alias: AccessRule
   * Caption: $Views_KrPermissionsExtendedVisibility_AccessRule_Param.
   */
   readonly ParamAccessRule: ViewObject = new ViewObject(1, 'AccessRule', '$Views_KrPermissionsExtendedVisibility_AccessRule_Param');

  /**
   * ID:2
   * Alias: Alias
   * Caption: $Views_KrPermissionsExtendedVisibility_ControlAlias_Param.
   */
   readonly ParamAlias: ViewObject = new ViewObject(2, 'Alias', '$Views_KrPermissionsExtendedVisibility_ControlAlias_Param');

  /**
   * ID:3
   * Alias: ControlType
   * Caption: $Views_KrPermissionsExtendedVisibility_ControlType_Param.
   */
   readonly ParamControlType: ViewObject = new ViewObject(3, 'ControlType', '$Views_KrPermissionsExtendedVisibility_ControlType_Param');

  /**
   * ID:4
   * Alias: IsHidden
   * Caption: $Views_KrPermissionsExtendedVisibility_HideControl_Param.
   */
   readonly ParamIsHidden: ViewObject = new ViewObject(4, 'IsHidden', '$Views_KrPermissionsExtendedVisibility_HideControl_Param');

  //#endregion
}

//#endregion

//#region KrPermissionsFileCheckRules

/**
 * ID: {2215eeaa-790a-4389-b800-7790487318aa}
 * Alias: KrPermissionsFileCheckRules
 * Caption: $Views_KrPermissionsFileCheckRules
 * Group: Kr Wf
 */
class KrPermissionsFileCheckRulesViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionsFileCheckRules": {2215eeaa-790a-4389-b800-7790487318aa}.
   */
   readonly ID: guid = '2215eeaa-790a-4389-b800-7790487318aa';

  /**
   * View name for "KrPermissionsFileCheckRules".
   */
   readonly Alias: string = 'KrPermissionsFileCheckRules';

  /**
   * View caption for "KrPermissionsFileCheckRules".
   */
   readonly Caption: string = '$Views_KrPermissionsFileCheckRules';

  /**
   * View group for "KrPermissionsFileCheckRules".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: FileCheckRuleID.
   */
   readonly ColumnFileCheckRuleID: ViewObject = new ViewObject(0, 'FileCheckRuleID');

  /**
   * ID:1
   * Alias: FileCheckRuleName
   * Caption: $Views_KrPermissionsFileCheckRules_Name.
   */
   readonly ColumnFileCheckRuleName: ViewObject = new ViewObject(1, 'FileCheckRuleName', '$Views_KrPermissionsFileCheckRules_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_KrPermissionsFileCheckRules_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_KrPermissionsFileCheckRules_Name_Param');

  //#endregion
}

//#endregion

//#region KrPermissionsFileEditAccessSettings

/**
 * ID: {e0588f6d-d7e0-4c3b-bf90-82b898bd512b}
 * Alias: KrPermissionsFileEditAccessSettings
 * Caption: $Views_Names_KrPermissionsFileEditAccessSettings
 * Group: Kr Wf
 */
class KrPermissionsFileEditAccessSettingsViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionsFileEditAccessSettings": {e0588f6d-d7e0-4c3b-bf90-82b898bd512b}.
   */
   readonly ID: guid = 'e0588f6d-d7e0-4c3b-bf90-82b898bd512b';

  /**
   * View name for "KrPermissionsFileEditAccessSettings".
   */
   readonly Alias: string = 'KrPermissionsFileEditAccessSettings';

  /**
   * View caption for "KrPermissionsFileEditAccessSettings".
   */
   readonly Caption: string = '$Views_Names_KrPermissionsFileEditAccessSettings';

  /**
   * View group for "KrPermissionsFileEditAccessSettings".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: AccessSettingID.
   */
   readonly ColumnAccessSettingID: ViewObject = new ViewObject(0, 'AccessSettingID');

  /**
   * ID:1
   * Alias: AccessSettingName
   * Caption: $Views_KrPermissionsFileEditAccessSettings_Name.
   */
   readonly ColumnAccessSettingName: ViewObject = new ViewObject(1, 'AccessSettingName', '$Views_KrPermissionsFileEditAccessSettings_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_KrPermissionsFileEditAccessSettings_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_KrPermissionsFileEditAccessSettings_Name_Param');

  //#endregion
}

//#endregion

//#region KrPermissionsFileReadAccessSettings

/**
 * ID: {e8b1f86f-b19e-426f-8703-d87359d75c32}
 * Alias: KrPermissionsFileReadAccessSettings
 * Caption: $Views_Names_KrPermissionsFileReadAccessSettings
 * Group: Kr Wf
 */
class KrPermissionsFileReadAccessSettingsViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionsFileReadAccessSettings": {e8b1f86f-b19e-426f-8703-d87359d75c32}.
   */
   readonly ID: guid = 'e8b1f86f-b19e-426f-8703-d87359d75c32';

  /**
   * View name for "KrPermissionsFileReadAccessSettings".
   */
   readonly Alias: string = 'KrPermissionsFileReadAccessSettings';

  /**
   * View caption for "KrPermissionsFileReadAccessSettings".
   */
   readonly Caption: string = '$Views_Names_KrPermissionsFileReadAccessSettings';

  /**
   * View group for "KrPermissionsFileReadAccessSettings".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: AccessSettingID.
   */
   readonly ColumnAccessSettingID: ViewObject = new ViewObject(0, 'AccessSettingID');

  /**
   * ID:1
   * Alias: AccessSettingName
   * Caption: $Views_KrPermissionsFileReadAccessSettings_Name.
   */
   readonly ColumnAccessSettingName: ViewObject = new ViewObject(1, 'AccessSettingName', '$Views_KrPermissionsFileReadAccessSettings_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_KrPermissionsFileReadAccessSettings_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_KrPermissionsFileReadAccessSettings_Name_Param');

  //#endregion
}

//#endregion

//#region KrPermissionsMandatoryValidationTypes

/**
 * ID: {ea16e82d-f10a-4897-90f6-a9caf61ce9cc}
 * Alias: KrPermissionsMandatoryValidationTypes
 * Caption: $Views_Names_KrPermissionsMandatoryValidationTypes
 * Group: Kr Wf
 */
class KrPermissionsMandatoryValidationTypesViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionsMandatoryValidationTypes": {ea16e82d-f10a-4897-90f6-a9caf61ce9cc}.
   */
   readonly ID: guid = 'ea16e82d-f10a-4897-90f6-a9caf61ce9cc';

  /**
   * View name for "KrPermissionsMandatoryValidationTypes".
   */
   readonly Alias: string = 'KrPermissionsMandatoryValidationTypes';

  /**
   * View caption for "KrPermissionsMandatoryValidationTypes".
   */
   readonly Caption: string = '$Views_Names_KrPermissionsMandatoryValidationTypes';

  /**
   * View group for "KrPermissionsMandatoryValidationTypes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: MandatoryValidationTypeID.
   */
   readonly ColumnMandatoryValidationTypeID: ViewObject = new ViewObject(0, 'MandatoryValidationTypeID');

  /**
   * ID:1
   * Alias: MandatoryValidationTypeName
   * Caption: $Views_KrPermissionsMandatoryValidationTypes_Name.
   */
   readonly ColumnMandatoryValidationTypeName: ViewObject = new ViewObject(1, 'MandatoryValidationTypeName', '$Views_KrPermissionsMandatoryValidationTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_KrPermissionsMandatoryValidationTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_KrPermissionsMandatoryValidationTypes_Name_Param');

  //#endregion
}

//#endregion

//#region KrPermissionsReport

/**
 * ID: {cb1362ac-3a78-4afc-baec-c585e570955a}
 * Alias: KrPermissionsReport
 * Caption: $Views_Names_KrPermissionsReport
 * Group: Kr Wf
 */
class KrPermissionsReportViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionsReport": {cb1362ac-3a78-4afc-baec-c585e570955a}.
   */
   readonly ID: guid = 'cb1362ac-3a78-4afc-baec-c585e570955a';

  /**
   * View name for "KrPermissionsReport".
   */
   readonly Alias: string = 'KrPermissionsReport';

  /**
   * View caption for "KrPermissionsReport".
   */
   readonly Caption: string = '$Views_Names_KrPermissionsReport';

  /**
   * View group for "KrPermissionsReport".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrPermissionsID.
   */
   readonly ColumnKrPermissionsID: ViewObject = new ViewObject(0, 'KrPermissionsID');

  /**
   * ID:1
   * Alias: KrPermissionsCaption
   * Caption: $Views_KrPermissions_Name.
   */
   readonly ColumnKrPermissionsCaption: ViewObject = new ViewObject(1, 'KrPermissionsCaption', '$Views_KrPermissions_Name');

  /**
   * ID:2
   * Alias: KrPermissionsPriority
   * Caption: $Views_KrPermissions_Priority.
   */
   readonly ColumnKrPermissionsPriority: ViewObject = new ViewObject(2, 'KrPermissionsPriority', '$Views_KrPermissions_Priority');

  /**
   * ID:3
   * Alias: KrPermissionsTypes
   * Caption: $Views_KrPermissions_Types.
   */
   readonly ColumnKrPermissionsTypes: ViewObject = new ViewObject(3, 'KrPermissionsTypes', '$Views_KrPermissions_Types');

  /**
   * ID:4
   * Alias: KrPermissionsStates
   * Caption: $Views_KrPermissions_States.
   */
   readonly ColumnKrPermissionsStates: ViewObject = new ViewObject(4, 'KrPermissionsStates', '$Views_KrPermissions_States');

  /**
   * ID:5
   * Alias: KrPermissionsRoles
   * Caption: $Views_KrPermissions_Roles.
   */
   readonly ColumnKrPermissionsRoles: ViewObject = new ViewObject(5, 'KrPermissionsRoles', '$Views_KrPermissions_Roles');

  /**
   * ID:6
   * Alias: KrPermissionsAclGenerationRules
   * Caption: $Views_KrPermissions_AclGenerationRules.
   */
   readonly ColumnKrPermissionsAclGenerationRules: ViewObject = new ViewObject(6, 'KrPermissionsAclGenerationRules', '$Views_KrPermissions_AclGenerationRules');

  /**
   * ID:7
   * Alias: KrPermissionsPermissions
   * Caption: $Views_KrPermissions_Permissions.
   */
   readonly ColumnKrPermissionsPermissions: ViewObject = new ViewObject(7, 'KrPermissionsPermissions', '$Views_KrPermissions_Permissions');

  /**
   * ID:8
   * Alias: Background.
   */
   readonly ColumnBackground: ViewObject = new ViewObject(8, 'Background');

  /**
   * ID:9
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(9, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_KrPermissions_Caption_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_KrPermissions_Caption_Param');

  /**
   * ID:1
   * Alias: Description
   * Caption: $Views_KrPermissions_Description_Param.
   */
   readonly ParamDescription: ViewObject = new ViewObject(1, 'Description', '$Views_KrPermissions_Description_Param');

  /**
   * ID:2
   * Alias: Type
   * Caption: $Views_KrPermissions_Type_Param.
   */
   readonly ParamType: ViewObject = new ViewObject(2, 'Type', '$Views_KrPermissions_Type_Param');

  /**
   * ID:3
   * Alias: State
   * Caption: $Views_KrPermissions_State_Param.
   */
   readonly ParamState: ViewObject = new ViewObject(3, 'State', '$Views_KrPermissions_State_Param');

  /**
   * ID:4
   * Alias: Role
   * Caption: $Views_KrPermissions_Role_Param.
   */
   readonly ParamRole: ViewObject = new ViewObject(4, 'Role', '$Views_KrPermissions_Role_Param');

  /**
   * ID:5
   * Alias: AclGenerationRule
   * Caption: $Views_KrPermissions_AclGenerationRule_Param.
   */
   readonly ParamAclGenerationRule: ViewObject = new ViewObject(5, 'AclGenerationRule', '$Views_KrPermissions_AclGenerationRule_Param');

  /**
   * ID:6
   * Alias: Permission
   * Caption: $Views_KrPermissions_Permission_Param.
   */
   readonly ParamPermission: ViewObject = new ViewObject(6, 'Permission', '$Views_KrPermissions_Permission_Param');

  /**
   * ID:7
   * Alias: User
   * Caption: $Views_KrPermissions_User_Param.
   */
   readonly ParamUser: ViewObject = new ViewObject(7, 'User', '$Views_KrPermissions_User_Param');

  /**
   * ID:8
   * Alias: Priority
   * Caption: $Views_KrPermissions_Priority_Param.
   */
   readonly ParamPriority: ViewObject = new ViewObject(8, 'Priority', '$Views_KrPermissions_Priority_Param');

  /**
   * ID:9
   * Alias: IsDisabled
   * Caption: $Views_KrPermissions_IsDisabled_Param.
   */
   readonly ParamIsDisabled: ViewObject = new ViewObject(9, 'IsDisabled', '$Views_KrPermissions_IsDisabled_Param');

  /**
   * ID:10
   * Alias: IsRequired
   * Caption: $Views_KrPermissions_AlwaysCheck_Param.
   */
   readonly ParamIsRequired: ViewObject = new ViewObject(10, 'IsRequired', '$Views_KrPermissions_AlwaysCheck_Param');

  /**
   * ID:11
   * Alias: IsExtended
   * Caption: $Views_KrPermissions_UseExtendedSettings_Param.
   */
   readonly ParamIsExtended: ViewObject = new ViewObject(11, 'IsExtended', '$Views_KrPermissions_UseExtendedSettings_Param');

  //#endregion
}

//#endregion

//#region KrPermissionStates

/**
 * ID: {44026b2a-6699-425c-8669-5ad5c75945f9}
 * Alias: KrPermissionStates
 * Caption: $Views_Names_KrPermissionStates
 * Group: Kr Wf
 */
class KrPermissionStatesViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionStates": {44026b2a-6699-425c-8669-5ad5c75945f9}.
   */
   readonly ID: guid = '44026b2a-6699-425c-8669-5ad5c75945f9';

  /**
   * View name for "KrPermissionStates".
   */
   readonly Alias: string = 'KrPermissionStates';

  /**
   * View caption for "KrPermissionStates".
   */
   readonly Caption: string = '$Views_Names_KrPermissionStates';

  /**
   * View group for "KrPermissionStates".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrPermissionStateID.
   */
   readonly ColumnKrPermissionStateID: ViewObject = new ViewObject(0, 'KrPermissionStateID');

  /**
   * ID:1
   * Alias: KrPermissionStateName
   * Caption: $Views_KrPermissions_States.
   */
   readonly ColumnKrPermissionStateName: ViewObject = new ViewObject(1, 'KrPermissionStateName', '$Views_KrPermissions_States');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: AccessRule
   * Caption: Access rule.
   */
   readonly ParamAccessRule: ViewObject = new ViewObject(0, 'AccessRule', 'Access rule');

  //#endregion
}

//#endregion

//#region KrPermissionTypes

/**
 * ID: {54026b2a-6699-425c-8669-5ad5c75945f9}
 * Alias: KrPermissionTypes
 * Caption: $Views_Names_KrPermissionTypes
 * Group: Kr Wf
 */
class KrPermissionTypesViewInfo {
  //#region Common

  /**
   * View identifier for "KrPermissionTypes": {54026b2a-6699-425c-8669-5ad5c75945f9}.
   */
   readonly ID: guid = '54026b2a-6699-425c-8669-5ad5c75945f9';

  /**
   * View name for "KrPermissionTypes".
   */
   readonly Alias: string = 'KrPermissionTypes';

  /**
   * View caption for "KrPermissionTypes".
   */
   readonly Caption: string = '$Views_Names_KrPermissionTypes';

  /**
   * View group for "KrPermissionTypes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrPermissionTypeID.
   */
   readonly ColumnKrPermissionTypeID: ViewObject = new ViewObject(0, 'KrPermissionTypeID');

  /**
   * ID:1
   * Alias: KrPermissionTypeCaption
   * Caption: $Views_KrPermissions_Types.
   */
   readonly ColumnKrPermissionTypeCaption: ViewObject = new ViewObject(1, 'KrPermissionTypeCaption', '$Views_KrPermissions_Types');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: AccessRule
   * Caption: Access rule.
   */
   readonly ParamAccessRule: ViewObject = new ViewObject(0, 'AccessRule', 'Access rule');

  //#endregion
}

//#endregion

//#region KrProcessManagementStageTypeModes

/**
 * ID: {20036792-579c-4228-b2f9-79495f326f06}
 * Alias: KrProcessManagementStageTypeModes
 * Caption: $Views_Names_KrProcessManagementStageTypeModes
 * Group: Kr Wf
 */
class KrProcessManagementStageTypeModesViewInfo {
  //#region Common

  /**
   * View identifier for "KrProcessManagementStageTypeModes": {20036792-579c-4228-b2f9-79495f326f06}.
   */
   readonly ID: guid = '20036792-579c-4228-b2f9-79495f326f06';

  /**
   * View name for "KrProcessManagementStageTypeModes".
   */
   readonly Alias: string = 'KrProcessManagementStageTypeModes';

  /**
   * View caption for "KrProcessManagementStageTypeModes".
   */
   readonly Caption: string = '$Views_Names_KrProcessManagementStageTypeModes';

  /**
   * View group for "KrProcessManagementStageTypeModes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: RefName.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', 'RefName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', 'Name');

  //#endregion
}

//#endregion

//#region KrRouteModes

/**
 * ID: {3179625f-bf5e-478a-ba44-07fd2babede7}
 * Alias: KrRouteModes
 * Caption: $Views_Names_KrRouteModes
 * Group: Kr Wf
 */
class KrRouteModesViewInfo {
  //#region Common

  /**
   * View identifier for "KrRouteModes": {3179625f-bf5e-478a-ba44-07fd2babede7}.
   */
   readonly ID: guid = '3179625f-bf5e-478a-ba44-07fd2babede7';

  /**
   * View name for "KrRouteModes".
   */
   readonly Alias: string = 'KrRouteModes';

  /**
   * View caption for "KrRouteModes".
   */
   readonly Caption: string = '$Views_Names_KrRouteModes';

  /**
   * View group for "KrRouteModes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ModeID.
   */
   readonly ColumnModeID: ViewObject = new ViewObject(0, 'ModeID');

  /**
   * ID:1
   * Alias: ModeName
   * Caption: $Views_KrRouteModes_Name.
   */
   readonly ColumnModeName: ViewObject = new ViewObject(1, 'ModeName', '$Views_KrRouteModes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: ModeNameParam
   * Caption: $Views_KrRouteModes_Name.
   */
   readonly ParamModeNameParam: ViewObject = new ViewObject(0, 'ModeNameParam', '$Views_KrRouteModes_Name');

  //#endregion
}

//#endregion

//#region KrSecondaryProcesses

/**
 * ID: {e824a33b-2194-4713-a42f-89ac225e0141}
 * Alias: KrSecondaryProcesses
 * Caption: $Views_Names_KrSecondaryProcesses
 * Group: Kr Wf
 */
class KrSecondaryProcessesViewInfo {
  //#region Common

  /**
   * View identifier for "KrSecondaryProcesses": {e824a33b-2194-4713-a42f-89ac225e0141}.
   */
   readonly ID: guid = 'e824a33b-2194-4713-a42f-89ac225e0141';

  /**
   * View name for "KrSecondaryProcesses".
   */
   readonly Alias: string = 'KrSecondaryProcesses';

  /**
   * View caption for "KrSecondaryProcesses".
   */
   readonly Caption: string = '$Views_Names_KrSecondaryProcesses';

  /**
   * View group for "KrSecondaryProcesses".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SecondaryProcessID.
   */
   readonly ColumnSecondaryProcessID: ViewObject = new ViewObject(0, 'SecondaryProcessID');

  /**
   * ID:1
   * Alias: SecondaryProcessName
   * Caption: $Views_KrSecondaryProcesses_Name.
   */
   readonly ColumnSecondaryProcessName: ViewObject = new ViewObject(1, 'SecondaryProcessName', '$Views_KrSecondaryProcesses_Name');

  /**
   * ID:2
   * Alias: Description
   * Caption: $Views_KrSecondaryProcesses_Description.
   */
   readonly ColumnDescription: ViewObject = new ViewObject(2, 'Description', '$Views_KrSecondaryProcesses_Description');

  /**
   * ID:3
   * Alias: IsGlobal
   * Caption: $Views_KrSecondaryProcesses_IsGlobal.
   */
   readonly ColumnIsGlobal: ViewObject = new ViewObject(3, 'IsGlobal', '$Views_KrSecondaryProcesses_IsGlobal');

  /**
   * ID:4
   * Alias: Async
   * Caption: $Views_KrSecondaryProcesses_Async.
   */
   readonly ColumnAsync: ViewObject = new ViewObject(4, 'Async', '$Views_KrSecondaryProcesses_Async');

  /**
   * ID:5
   * Alias: Types
   * Caption: $Views_KrSecondaryProcesses_Types.
   */
   readonly ColumnTypes: ViewObject = new ViewObject(5, 'Types', '$Views_KrSecondaryProcesses_Types');

  /**
   * ID:6
   * Alias: Roles
   * Caption: $Views_KrSecondaryProcesses_Roles.
   */
   readonly ColumnRoles: ViewObject = new ViewObject(6, 'Roles', '$Views_KrSecondaryProcesses_Roles');

  /**
   * ID:7
   * Alias: CardStates
   * Caption: $Views_KrSecondaryProcesses_States.
   */
   readonly ColumnCardStates: ViewObject = new ViewObject(7, 'CardStates', '$Views_KrSecondaryProcesses_States');

  /**
   * ID:8
   * Alias: AvailableRoles
   * Caption: $Views_KrSecondaryProcesses_AvailableRoles.
   */
   readonly ColumnAvailableRoles: ViewObject = new ViewObject(8, 'AvailableRoles', '$Views_KrSecondaryProcesses_AvailableRoles');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: SecondaryProcessIDParam
   * Caption: SecondaryProcessIDParam.
   */
   readonly ParamSecondaryProcessIDParam: ViewObject = new ViewObject(0, 'SecondaryProcessIDParam', 'SecondaryProcessIDParam');

  /**
   * ID:1
   * Alias: SecondaryProcessNameParam
   * Caption: $Views_KrSecondaryProcesses_Name.
   */
   readonly ParamSecondaryProcessNameParam: ViewObject = new ViewObject(1, 'SecondaryProcessNameParam', '$Views_KrSecondaryProcesses_Name');

  /**
   * ID:2
   * Alias: IsGlobalParam
   * Caption: $Views_KrSecondaryProcesses_IsGlobal.
   */
   readonly ParamIsGlobalParam: ViewObject = new ViewObject(2, 'IsGlobalParam', '$Views_KrSecondaryProcesses_IsGlobal');

  /**
   * ID:3
   * Alias: AsyncParam
   * Caption: $Views_KrSecondaryProcesses_Async.
   */
   readonly ParamAsyncParam: ViewObject = new ViewObject(3, 'AsyncParam', '$Views_KrSecondaryProcesses_Async');

  /**
   * ID:4
   * Alias: TypeParam
   * Caption: $Views_KrSecondaryProcesses_Types.
   */
   readonly ParamTypeParam: ViewObject = new ViewObject(4, 'TypeParam', '$Views_KrSecondaryProcesses_Types');

  /**
   * ID:5
   * Alias: RoleParam
   * Caption: $Views_KrSecondaryProcesses_Roles.
   */
   readonly ParamRoleParam: ViewObject = new ViewObject(5, 'RoleParam', '$Views_KrSecondaryProcesses_Roles');

  /**
   * ID:6
   * Alias: StateParam
   * Caption: $Views_KrSecondaryProcesses_States.
   */
   readonly ParamStateParam: ViewObject = new ViewObject(6, 'StateParam', '$Views_KrSecondaryProcesses_States');

  //#endregion
}

//#endregion

//#region KrSecondaryProcessModes

/**
 * ID: {4f6f0744-5a4e-4285-b39e-064c56737715}
 * Alias: KrSecondaryProcessModes
 * Caption: $Views_Names_KrSecondaryProcessModes
 * Group: Kr Wf
 */
class KrSecondaryProcessModesViewInfo {
  //#region Common

  /**
   * View identifier for "KrSecondaryProcessModes": {4f6f0744-5a4e-4285-b39e-064c56737715}.
   */
   readonly ID: guid = '4f6f0744-5a4e-4285-b39e-064c56737715';

  /**
   * View name for "KrSecondaryProcessModes".
   */
   readonly Alias: string = 'KrSecondaryProcessModes';

  /**
   * View caption for "KrSecondaryProcessModes".
   */
   readonly Caption: string = '$Views_Names_KrSecondaryProcessModes';

  /**
   * View group for "KrSecondaryProcessModes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: RefName.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', 'RefName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', 'Name');

  //#endregion
}

//#endregion

//#region KrStageCommonMethods

/**
 * ID: {3b31f77a-1667-443c-b4f0-9bdb04798c72}
 * Alias: KrStageCommonMethods
 * Caption: $Views_Names_KrStageCommonMethods
 * Group: Kr Wf
 */
class KrStageCommonMethodsViewInfo {
  //#region Common

  /**
   * View identifier for "KrStageCommonMethods": {3b31f77a-1667-443c-b4f0-9bdb04798c72}.
   */
   readonly ID: guid = '3b31f77a-1667-443c-b4f0-9bdb04798c72';

  /**
   * View name for "KrStageCommonMethods".
   */
   readonly Alias: string = 'KrStageCommonMethods';

  /**
   * View caption for "KrStageCommonMethods".
   */
   readonly Caption: string = '$Views_Names_KrStageCommonMethods';

  /**
   * View group for "KrStageCommonMethods".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: MethodID.
   */
   readonly ColumnMethodID: ViewObject = new ViewObject(0, 'MethodID');

  /**
   * ID:1
   * Alias: MethodName
   * Caption: $Views_KrStageCommonMethod_MethodName.
   */
   readonly ColumnMethodName: ViewObject = new ViewObject(1, 'MethodName', '$Views_KrStageCommonMethod_MethodName');

  /**
   * ID:2
   * Alias: Source
   * Caption: $Views_KrStageCommonMethod_Source.
   */
   readonly ColumnSource: ViewObject = new ViewObject(2, 'Source', '$Views_KrStageCommonMethod_Source');

  /**
   * ID:3
   * Alias: Description
   * Caption: $Views_KrStageCommonMethod_MethodDescription.
   */
   readonly ColumnDescription: ViewObject = new ViewObject(3, 'Description', '$Views_KrStageCommonMethod_MethodDescription');

  /**
   * ID:4
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(4, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: MethodIDParam
   * Caption: MethodIDParam.
   */
   readonly ParamMethodIDParam: ViewObject = new ViewObject(0, 'MethodIDParam', 'MethodIDParam');

  /**
   * ID:1
   * Alias: MethodNameParam
   * Caption: $Views_KrStageCommonMethod_MethodName.
   */
   readonly ParamMethodNameParam: ViewObject = new ViewObject(1, 'MethodNameParam', '$Views_KrStageCommonMethod_MethodName');

  /**
   * ID:2
   * Alias: MethodDescriptionParam
   * Caption: $Views_KrStageCommonMethod_MethodDescription.
   */
   readonly ParamMethodDescriptionParam: ViewObject = new ViewObject(2, 'MethodDescriptionParam', '$Views_KrStageCommonMethod_MethodDescription');

  //#endregion
}

//#endregion

//#region KrStageGroups

/**
 * ID: {6492b1f7-0fa4-4910-9911-67b2eb1614d7}
 * Alias: KrStageGroups
 * Caption: $Views_Names_KrStageGroups
 * Group: Kr Wf
 */
class KrStageGroupsViewInfo {
  //#region Common

  /**
   * View identifier for "KrStageGroups": {6492b1f7-0fa4-4910-9911-67b2eb1614d7}.
   */
   readonly ID: guid = '6492b1f7-0fa4-4910-9911-67b2eb1614d7';

  /**
   * View name for "KrStageGroups".
   */
   readonly Alias: string = 'KrStageGroups';

  /**
   * View caption for "KrStageGroups".
   */
   readonly Caption: string = '$Views_Names_KrStageGroups';

  /**
   * View group for "KrStageGroups".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: StageGroupID.
   */
   readonly ColumnStageGroupID: ViewObject = new ViewObject(0, 'StageGroupID');

  /**
   * ID:1
   * Alias: StageGroupName
   * Caption: $Views_KrStageGroups_Name.
   */
   readonly ColumnStageGroupName: ViewObject = new ViewObject(1, 'StageGroupName', '$Views_KrStageGroups_Name');

  /**
   * ID:2
   * Alias: Description
   * Caption: $Views_KrStageGroups_Description.
   */
   readonly ColumnDescription: ViewObject = new ViewObject(2, 'Description', '$Views_KrStageGroups_Description');

  /**
   * ID:3
   * Alias: IsGroupReadonly
   * Caption: $Views_KrStageGroups_IsGroupReadonly.
   */
   readonly ColumnIsGroupReadonly: ViewObject = new ViewObject(3, 'IsGroupReadonly', '$Views_KrStageGroups_IsGroupReadonly');

  /**
   * ID:4
   * Alias: Order
   * Caption: $Views_KrStageGroups_Order.
   */
   readonly ColumnOrder: ViewObject = new ViewObject(4, 'Order', '$Views_KrStageGroups_Order');

  /**
   * ID:5
   * Alias: Types
   * Caption: $Views_KrStageGroups_Types.
   */
   readonly ColumnTypes: ViewObject = new ViewObject(5, 'Types', '$Views_KrStageGroups_Types');

  /**
   * ID:6
   * Alias: Roles
   * Caption: $Views_KrStageGroups_Roles.
   */
   readonly ColumnRoles: ViewObject = new ViewObject(6, 'Roles', '$Views_KrStageGroups_Roles');

  /**
   * ID:7
   * Alias: SecondaryProcessName
   * Caption: $Views_KrStageGroups_SecondaryProcessCaption.
   */
   readonly ColumnSecondaryProcessName: ViewObject = new ViewObject(7, 'SecondaryProcessName', '$Views_KrStageGroups_SecondaryProcessCaption');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: StageGroupIDParam
   * Caption: StageGroupIDParam.
   */
   readonly ParamStageGroupIDParam: ViewObject = new ViewObject(0, 'StageGroupIDParam', 'StageGroupIDParam');

  /**
   * ID:1
   * Alias: StageGroupNameParam
   * Caption: $Views_KrStageGroups_Name.
   */
   readonly ParamStageGroupNameParam: ViewObject = new ViewObject(1, 'StageGroupNameParam', '$Views_KrStageGroups_Name');

  /**
   * ID:2
   * Alias: StageGroupDescriptionParam
   * Caption: $Views_KrStageGroups_Description.
   */
   readonly ParamStageGroupDescriptionParam: ViewObject = new ViewObject(2, 'StageGroupDescriptionParam', '$Views_KrStageGroups_Description');

  /**
   * ID:3
   * Alias: IsGroupReadonlyParam
   * Caption: $Views_KrStageGroups_IsGroupReadonly.
   */
   readonly ParamIsGroupReadonlyParam: ViewObject = new ViewObject(3, 'IsGroupReadonlyParam', '$Views_KrStageGroups_IsGroupReadonly');

  /**
   * ID:4
   * Alias: TypeParam
   * Caption: $Views_KrStageGroups_Types.
   */
   readonly ParamTypeParam: ViewObject = new ViewObject(4, 'TypeParam', '$Views_KrStageGroups_Types');

  /**
   * ID:5
   * Alias: RoleParam
   * Caption: $Views_KrStageGroups_Roles.
   */
   readonly ParamRoleParam: ViewObject = new ViewObject(5, 'RoleParam', '$Views_KrStageGroups_Roles');

  /**
   * ID:6
   * Alias: StartupParam
   * Caption: $Views_KrStageTemplates_ByStartupTypeSubset.
   */
   readonly ParamStartupParam: ViewObject = new ViewObject(6, 'StartupParam', '$Views_KrStageTemplates_ByStartupTypeSubset');

  //#endregion
}

//#endregion

//#region KrStageRows

/**
 * ID: {74f0ec7a-2cb4-4bb9-9eca-942e28374ea9}
 * Alias: KrStageRows
 * Caption: $Views_Names_KrStageRows
 * Group: Kr Wf
 */
class KrStageRowsViewInfo {
  //#region Common

  /**
   * View identifier for "KrStageRows": {74f0ec7a-2cb4-4bb9-9eca-942e28374ea9}.
   */
   readonly ID: guid = '74f0ec7a-2cb4-4bb9-9eca-942e28374ea9';

  /**
   * View name for "KrStageRows".
   */
   readonly Alias: string = 'KrStageRows';

  /**
   * View caption for "KrStageRows".
   */
   readonly Caption: string = '$Views_Names_KrStageRows';

  /**
   * View group for "KrStageRows".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: StageRowID.
   */
   readonly ColumnStageRowID: ViewObject = new ViewObject(0, 'StageRowID');

  /**
   * ID:1
   * Alias: TypeCaption
   * Caption: $Views_KrStageRows_TypeCaption.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(1, 'TypeCaption', '$Views_KrStageRows_TypeCaption');

  /**
   * ID:2
   * Alias: StageName
   * Caption: $Views_KrStageRows_Name.
   */
   readonly ColumnStageName: ViewObject = new ViewObject(2, 'StageName', '$Views_KrStageRows_Name');

  /**
   * ID:3
   * Alias: TimeLimit
   * Caption: $Views_KrStageRows_TimeLimit.
   */
   readonly ColumnTimeLimit: ViewObject = new ViewObject(3, 'TimeLimit', '$Views_KrStageRows_TimeLimit');

  /**
   * ID:4
   * Alias: Participants
   * Caption: $Views_KrStageRows_Participants.
   */
   readonly ColumnParticipants: ViewObject = new ViewObject(4, 'Participants', '$Views_KrStageRows_Participants');

  /**
   * ID:5
   * Alias: Settings
   * Caption: $Views_KrStageRows_Settings.
   */
   readonly ColumnSettings: ViewObject = new ViewObject(5, 'Settings', '$Views_KrStageRows_Settings');

  /**
   * ID:6
   * Alias: Order.
   */
   readonly ColumnOrder: ViewObject = new ViewObject(6, 'Order');

  /**
   * ID:7
   * Alias: StageRowGroupID
   * Caption: StageGroupID.
   */
   readonly ColumnStageRowGroupID: ViewObject = new ViewObject(7, 'StageRowGroupID', 'StageGroupID');

  /**
   * ID:8
   * Alias: StageRowGroupName
   * Caption: StageGroupName.
   */
   readonly ColumnStageRowGroupName: ViewObject = new ViewObject(8, 'StageRowGroupName', 'StageGroupName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: StageTemplateIDParam
   * Caption: StageTemplateIDParam.
   */
   readonly ParamStageTemplateIDParam: ViewObject = new ViewObject(0, 'StageTemplateIDParam', 'StageTemplateIDParam');

  /**
   * ID:1
   * Alias: StageNameParam
   * Caption: $Views_KrStageRows_Name.
   */
   readonly ParamStageNameParam: ViewObject = new ViewObject(1, 'StageNameParam', '$Views_KrStageRows_Name');

  //#endregion
}

//#endregion

//#region KrStageTemplateGroupPosition

/**
 * ID: {c4092348-06c2-452d-984e-18638961365b}
 * Alias: KrStageTemplateGroupPosition
 * Caption: $Views_Names_KrStageTemplateGroupPosition
 * Group: Kr Wf
 */
class KrStageTemplateGroupPositionViewInfo {
  //#region Common

  /**
   * View identifier for "KrStageTemplateGroupPosition": {c4092348-06c2-452d-984e-18638961365b}.
   */
   readonly ID: guid = 'c4092348-06c2-452d-984e-18638961365b';

  /**
   * View name for "KrStageTemplateGroupPosition".
   */
   readonly Alias: string = 'KrStageTemplateGroupPosition';

  /**
   * View caption for "KrStageTemplateGroupPosition".
   */
   readonly Caption: string = '$Views_Names_KrStageTemplateGroupPosition';

  /**
   * View group for "KrStageTemplateGroupPosition".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: GroupID.
   */
   readonly ColumnGroupID: ViewObject = new ViewObject(0, 'GroupID');

  /**
   * ID:1
   * Alias: GroupName
   * Caption: $Views_KrStageTemplateGroupPosition_GroupPositionName.
   */
   readonly ColumnGroupName: ViewObject = new ViewObject(1, 'GroupName', '$Views_KrStageTemplateGroupPosition_GroupPositionName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: GroupNameParam
   * Caption: $Views_KrStageTemplateGroupPosition_GroupPositionName.
   */
   readonly ParamGroupNameParam: ViewObject = new ViewObject(0, 'GroupNameParam', '$Views_KrStageTemplateGroupPosition_GroupPositionName');

  //#endregion
}

//#endregion

//#region KrStageTemplates

/**
 * ID: {2163b711-2672-443f-9af1-ede4af0e9e89}
 * Alias: KrStageTemplates
 * Caption: $Views_Names_KrStageTemplates
 * Group: Kr Wf
 */
class KrStageTemplatesViewInfo {
  //#region Common

  /**
   * View identifier for "KrStageTemplates": {2163b711-2672-443f-9af1-ede4af0e9e89}.
   */
   readonly ID: guid = '2163b711-2672-443f-9af1-ede4af0e9e89';

  /**
   * View name for "KrStageTemplates".
   */
   readonly Alias: string = 'KrStageTemplates';

  /**
   * View caption for "KrStageTemplates".
   */
   readonly Caption: string = '$Views_Names_KrStageTemplates';

  /**
   * View group for "KrStageTemplates".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: StageTemplateID.
   */
   readonly ColumnStageTemplateID: ViewObject = new ViewObject(0, 'StageTemplateID');

  /**
   * ID:1
   * Alias: StageTemplateName
   * Caption: $Views_KrStageTemplates_StageTemplateName.
   */
   readonly ColumnStageTemplateName: ViewObject = new ViewObject(1, 'StageTemplateName', '$Views_KrStageTemplates_StageTemplateName');

  /**
   * ID:2
   * Alias: Description
   * Caption: $Views_KrStageTemplates_StageTemplateDescription.
   */
   readonly ColumnDescription: ViewObject = new ViewObject(2, 'Description', '$Views_KrStageTemplates_StageTemplateDescription');

  /**
   * ID:3
   * Alias: CanChangeOrder
   * Caption: $Views_KrStageTemplates_CanChangeOrder.
   */
   readonly ColumnCanChangeOrder: ViewObject = new ViewObject(3, 'CanChangeOrder', '$Views_KrStageTemplates_CanChangeOrder');

  /**
   * ID:4
   * Alias: Order
   * Caption: $Views_KrStageTemplates_Order.
   */
   readonly ColumnOrder: ViewObject = new ViewObject(4, 'Order', '$Views_KrStageTemplates_Order');

  /**
   * ID:5
   * Alias: Types
   * Caption: $Views_KrStageTemplates_Types.
   */
   readonly ColumnTypes: ViewObject = new ViewObject(5, 'Types', '$Views_KrStageTemplates_Types');

  /**
   * ID:6
   * Alias: Roles
   * Caption: $Views_KrStageTemplates_Roles.
   */
   readonly ColumnRoles: ViewObject = new ViewObject(6, 'Roles', '$Views_KrStageTemplates_Roles');

  /**
   * ID:7
   * Alias: StageGroupName
   * Caption: $Views_KrStageTemplates_StageGroup.
   */
   readonly ColumnStageGroupName: ViewObject = new ViewObject(7, 'StageGroupName', '$Views_KrStageTemplates_StageGroup');

  /**
   * ID:8
   * Alias: SecondaryProcessName
   * Caption: $Views_KrStageTemplates_SecondaryProcessCaption.
   */
   readonly ColumnSecondaryProcessName: ViewObject = new ViewObject(8, 'SecondaryProcessName', '$Views_KrStageTemplates_SecondaryProcessCaption');

  /**
   * ID:9
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(9, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: StageTemplateIDParam
   * Caption: StageTemplateID.
   */
   readonly ParamStageTemplateIDParam: ViewObject = new ViewObject(0, 'StageTemplateIDParam', 'StageTemplateID');

  /**
   * ID:1
   * Alias: StageTemplateNameParam
   * Caption: $Views_KrStageTemplates_StageTemplateName.
   */
   readonly ParamStageTemplateNameParam: ViewObject = new ViewObject(1, 'StageTemplateNameParam', '$Views_KrStageTemplates_StageTemplateName');

  /**
   * ID:2
   * Alias: StageTemplateDescriptionParam
   * Caption: $Views_KrStageTemplates_StageTemplateDescription.
   */
   readonly ParamStageTemplateDescriptionParam: ViewObject = new ViewObject(2, 'StageTemplateDescriptionParam', '$Views_KrStageTemplates_StageTemplateDescription');

  /**
   * ID:3
   * Alias: GroupPositionIDParam
   * Caption: $Views_KrStageTemplates_GroupPositionName.
   */
   readonly ParamGroupPositionIDParam: ViewObject = new ViewObject(3, 'GroupPositionIDParam', '$Views_KrStageTemplates_GroupPositionName');

  /**
   * ID:4
   * Alias: CanChangeOrderParam
   * Caption: $Views_KrStageTemplates_CanChangeOrder.
   */
   readonly ParamCanChangeOrderParam: ViewObject = new ViewObject(4, 'CanChangeOrderParam', '$Views_KrStageTemplates_CanChangeOrder');

  /**
   * ID:5
   * Alias: TypeParam
   * Caption: $Views_KrStageTemplates_TypeParam.
   */
   readonly ParamTypeParam: ViewObject = new ViewObject(5, 'TypeParam', '$Views_KrStageTemplates_TypeParam');

  /**
   * ID:6
   * Alias: RoleParam
   * Caption: $Views_KrStageTemplates_Roles.
   */
   readonly ParamRoleParam: ViewObject = new ViewObject(6, 'RoleParam', '$Views_KrStageTemplates_Roles');

  /**
   * ID:7
   * Alias: StageGroupParam
   * Caption: $Views_KrStageTemplates_StageGroups.
   */
   readonly ParamStageGroupParam: ViewObject = new ViewObject(7, 'StageGroupParam', '$Views_KrStageTemplates_StageGroups');

  /**
   * ID:8
   * Alias: StartupParam
   * Caption: $Views_KrStageTemplates_ByStartupTypeSubset.
   */
   readonly ParamStartupParam: ViewObject = new ViewObject(8, 'StartupParam', '$Views_KrStageTemplates_ByStartupTypeSubset');

  /**
   * ID:9
   * Alias: AdvisoryParam
   * Caption: $Views_KrStageTemplates_AdvisoryParam.
   */
   readonly ParamAdvisoryParam: ViewObject = new ViewObject(9, 'AdvisoryParam', '$Views_KrStageTemplates_AdvisoryParam');

  //#endregion
}

//#endregion

//#region KrStageTypes

/**
 * ID: {a7ea0334-626e-41d8-9ae3-80cf3c710daa}
 * Alias: KrStageTypes
 * Caption: $Views_Names_KrStageTypes
 * Group: Kr Wf
 */
class KrStageTypesViewInfo {
  //#region Common

  /**
   * View identifier for "KrStageTypes": {a7ea0334-626e-41d8-9ae3-80cf3c710daa}.
   */
   readonly ID: guid = 'a7ea0334-626e-41d8-9ae3-80cf3c710daa';

  /**
   * View name for "KrStageTypes".
   */
   readonly Alias: string = 'KrStageTypes';

  /**
   * View caption for "KrStageTypes".
   */
   readonly Caption: string = '$Views_Names_KrStageTypes';

  /**
   * View group for "KrStageTypes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: StageTypeID.
   */
   readonly ColumnStageTypeID: ViewObject = new ViewObject(0, 'StageTypeID');

  /**
   * ID:1
   * Alias: StageTypeCaption
   * Caption: $Views_KrProcessStageTypes_Caption.
   */
   readonly ColumnStageTypeCaption: ViewObject = new ViewObject(1, 'StageTypeCaption', '$Views_KrProcessStageTypes_Caption');

  /**
   * ID:2
   * Alias: StageTypeDefaultStageName
   * Caption: $Views_KrProcessStageTypes_DefaultStageName.
   */
   readonly ColumnStageTypeDefaultStageName: ViewObject = new ViewObject(2, 'StageTypeDefaultStageName', '$Views_KrProcessStageTypes_DefaultStageName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_KrProcessStageTypes_Name.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_KrProcessStageTypes_Name');

  //#endregion
}

//#endregion

//#region KrTypes

/**
 * ID: {399837f5-1c6e-470e-ae4a-6d85362650c7}
 * Alias: KrTypes
 * Caption: $Views_Names_KrTypes
 * Group: Kr Wf
 */
class KrTypesViewInfo {
  //#region Common

  /**
   * View identifier for "KrTypes": {399837f5-1c6e-470e-ae4a-6d85362650c7}.
   */
   readonly ID: guid = '399837f5-1c6e-470e-ae4a-6d85362650c7';

  /**
   * View name for "KrTypes".
   */
   readonly Alias: string = 'KrTypes';

  /**
   * View caption for "KrTypes".
   */
   readonly Caption: string = '$Views_Names_KrTypes';

  /**
   * View group for "KrTypes".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: DocTypeID.
   */
   readonly ColumnDocTypeID: ViewObject = new ViewObject(1, 'DocTypeID');

  /**
   * ID:2
   * Alias: DocTypeCaption.
   */
   readonly ColumnDocTypeCaption: ViewObject = new ViewObject(2, 'DocTypeCaption');

  /**
   * ID:3
   * Alias: TypeCaption
   * Caption: $Views_KrTypes_Name.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(3, 'TypeCaption', '$Views_KrTypes_Name');

  /**
   * ID:4
   * Alias: TypeIsDocType
   * Caption: $Views_KrTypes_Type.
   */
   readonly ColumnTypeIsDocType: ViewObject = new ViewObject(4, 'TypeIsDocType', '$Views_KrTypes_Type');

  /**
   * ID:5
   * Alias: Name
   * Caption: $Views_KrTypes_Alias.
   */
   readonly ColumnName: ViewObject = new ViewObject(5, 'Name', '$Views_KrTypes_Alias');

  /**
   * ID:6
   * Alias: IsDocTypeCaption
   * Caption: $Views_KrTypes_Type.
   */
   readonly ColumnIsDocTypeCaption: ViewObject = new ViewObject(6, 'IsDocTypeCaption', '$Views_KrTypes_Type');

  /**
   * ID:7
   * Alias: State
   * Caption: $Views_KrTypes_State.
   */
   readonly ColumnState: ViewObject = new ViewObject(7, 'State', '$Views_KrTypes_State');

  /**
   * ID:8
   * Alias: ParentType
   * Caption: $Views_KrTypes_ParentType.
   */
   readonly ColumnParentType: ViewObject = new ViewObject(8, 'ParentType', '$Views_KrTypes_ParentType');

  /**
   * ID:9
   * Alias: LocalizedTypeCaption.
   */
   readonly ColumnLocalizedTypeCaption: ViewObject = new ViewObject(9, 'LocalizedTypeCaption');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_KrTypes_Name_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_KrTypes_Name_Param');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_KrTypes_AliasOrCaption_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_KrTypes_AliasOrCaption_Param');

  /**
   * ID:2
   * Alias: TypeIsDocType
   * Caption: $Views_KrTypes_DocumentType_Param.
   */
   readonly ParamTypeIsDocType: ViewObject = new ViewObject(2, 'TypeIsDocType', '$Views_KrTypes_DocumentType_Param');

  //#endregion
}

//#endregion

//#region KrTypesEffective

/**
 * ID: {fd177ebc-050e-4f1a-8a28-deba816a727d}
 * Alias: KrTypesEffective
 * Caption: $Views_Names_KrTypesEffective
 * Group: Kr Wf
 */
class KrTypesEffectiveViewInfo {
  //#region Common

  /**
   * View identifier for "KrTypesEffective": {fd177ebc-050e-4f1a-8a28-deba816a727d}.
   */
   readonly ID: guid = 'fd177ebc-050e-4f1a-8a28-deba816a727d';

  /**
   * View name for "KrTypesEffective".
   */
   readonly Alias: string = 'KrTypesEffective';

  /**
   * View caption for "KrTypesEffective".
   */
   readonly Caption: string = '$Views_Names_KrTypesEffective';

  /**
   * View group for "KrTypesEffective".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeCaption
   * Caption: $Views_KrTypesEffective_Name.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(1, 'TypeCaption', '$Views_KrTypesEffective_Name');

  /**
   * ID:2
   * Alias: LocalizedTypeCaption.
   */
   readonly ColumnLocalizedTypeCaption: ViewObject = new ViewObject(2, 'LocalizedTypeCaption');

  /**
   * ID:3
   * Alias: TypeIsDocType.
   */
   readonly ColumnTypeIsDocType: ViewObject = new ViewObject(3, 'TypeIsDocType');

  /**
   * ID:4
   * Alias: TypeName
   * Caption: $Views_KrTypesEffective_Alias.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(4, 'TypeName', '$Views_KrTypesEffective_Alias');

  /**
   * ID:5
   * Alias: IsDocTypeCaption
   * Caption: $Views_KrTypesEffective_Type.
   */
   readonly ColumnIsDocTypeCaption: ViewObject = new ViewObject(5, 'IsDocTypeCaption', '$Views_KrTypesEffective_Type');

  /**
   * ID:6
   * Alias: ParentType
   * Caption: $Views_KrTypesEffective_ParentType.
   */
   readonly ColumnParentType: ViewObject = new ViewObject(6, 'ParentType', '$Views_KrTypesEffective_ParentType');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_KrTypesEffective_Name_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_KrTypesEffective_Name_Param');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_KrTypesEffective_Alias_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_KrTypesEffective_Alias_Param');

  /**
   * ID:2
   * Alias: TypeIsDocType
   * Caption: $Views_KrTypesEffective_DocType_Param.
   */
   readonly ParamTypeIsDocType: ViewObject = new ViewObject(2, 'TypeIsDocType', '$Views_KrTypesEffective_DocType_Param');

  //#endregion
}

//#endregion

//#region KrTypesForDialogs

/**
 * ID: {2c0b6a4a-8759-43d1-b23c-0c64f365d343}
 * Alias: KrTypesForDialogs
 * Caption: $Views_Names_KrTypesForDialogs
 * Group: Kr Wf
 */
class KrTypesForDialogsViewInfo {
  //#region Common

  /**
   * View identifier for "KrTypesForDialogs": {2c0b6a4a-8759-43d1-b23c-0c64f365d343}.
   */
   readonly ID: guid = '2c0b6a4a-8759-43d1-b23c-0c64f365d343';

  /**
   * View name for "KrTypesForDialogs".
   */
   readonly Alias: string = 'KrTypesForDialogs';

  /**
   * View caption for "KrTypesForDialogs".
   */
   readonly Caption: string = '$Views_Names_KrTypesForDialogs';

  /**
   * View group for "KrTypesForDialogs".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeCaption
   * Caption: $Views_Types_Name.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(1, 'TypeCaption', '$Views_Types_Name');

  /**
   * ID:2
   * Alias: TypeName
   * Caption: $Views_Types_Alias.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(2, 'TypeName', '$Views_Types_Alias');

  /**
   * ID:3
   * Alias: IsDocTypeCaption
   * Caption: $Views_KrTypes_Type.
   */
   readonly ColumnIsDocTypeCaption: ViewObject = new ViewObject(3, 'IsDocTypeCaption', '$Views_KrTypes_Type');

  /**
   * ID:4
   * Alias: State
   * Caption: $Views_KrTypes_State.
   */
   readonly ColumnState: ViewObject = new ViewObject(4, 'State', '$Views_KrTypes_State');

  /**
   * ID:5
   * Alias: ParentType
   * Caption: $Views_KrTypes_ParentType.
   */
   readonly ColumnParentType: ViewObject = new ViewObject(5, 'ParentType', '$Views_KrTypes_ParentType');

  /**
   * ID:6
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(6, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_Types_Name_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_Types_Name_Param');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_Types_Alias_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_Types_Alias_Param');

  /**
   * ID:2
   * Alias: NameOrCaption
   * Caption: NameOrCaption.
   */
   readonly ParamNameOrCaption: ViewObject = new ViewObject(2, 'NameOrCaption', 'NameOrCaption');

  //#endregion
}

//#endregion

//#region KrTypesForPermissionsExtension

/**
 * ID: {d2c9ecb8-0e7f-4f79-a76c-c2cc71b0d959}
 * Alias: KrTypesForPermissionsExtension
 * Caption: $Views_Names_KrTypesForPermissionsExtension
 * Group: Kr Wf
 */
class KrTypesForPermissionsExtensionViewInfo {
  //#region Common

  /**
   * View identifier for "KrTypesForPermissionsExtension": {d2c9ecb8-0e7f-4f79-a76c-c2cc71b0d959}.
   */
   readonly ID: guid = 'd2c9ecb8-0e7f-4f79-a76c-c2cc71b0d959';

  /**
   * View name for "KrTypesForPermissionsExtension".
   */
   readonly Alias: string = 'KrTypesForPermissionsExtension';

  /**
   * View caption for "KrTypesForPermissionsExtension".
   */
   readonly Caption: string = '$Views_Names_KrTypesForPermissionsExtension';

  /**
   * View group for "KrTypesForPermissionsExtension".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeCaption
   * Caption: $Views_KrTypes_Caption.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(1, 'TypeCaption', '$Views_KrTypes_Caption');

  /**
   * ID:2
   * Alias: TypeName
   * Caption: $Views_KrTypes_Name.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(2, 'TypeName', '$Views_KrTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: NameOrCaption
   * Caption: $Views_KrTypes_NameOrCaption_Param.
   */
   readonly ParamNameOrCaption: ViewObject = new ViewObject(0, 'NameOrCaption', '$Views_KrTypes_NameOrCaption_Param');

  //#endregion
}

//#endregion

//#region KrVirtualFiles

/**
 * ID: {e2ca613f-9ad1-4dba-bdaa-feb0d96b9700}
 * Alias: KrVirtualFiles
 * Caption: $Views_Names_KrVirtualFiles
 * Group: Kr Wf
 */
class KrVirtualFilesViewInfo {
  //#region Common

  /**
   * View identifier for "KrVirtualFiles": {e2ca613f-9ad1-4dba-bdaa-feb0d96b9700}.
   */
   readonly ID: guid = 'e2ca613f-9ad1-4dba-bdaa-feb0d96b9700';

  /**
   * View name for "KrVirtualFiles".
   */
   readonly Alias: string = 'KrVirtualFiles';

  /**
   * View caption for "KrVirtualFiles".
   */
   readonly Caption: string = '$Views_Names_KrVirtualFiles';

  /**
   * View group for "KrVirtualFiles".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KrVirtualFileID.
   */
   readonly ColumnKrVirtualFileID: ViewObject = new ViewObject(0, 'KrVirtualFileID');

  /**
   * ID:1
   * Alias: KrVirtualFileName
   * Caption: $Views_KrVirtualFiles_Name.
   */
   readonly ColumnKrVirtualFileName: ViewObject = new ViewObject(1, 'KrVirtualFileName', '$Views_KrVirtualFiles_Name');

  /**
   * ID:2
   * Alias: KrVirtualFileFileTemplate
   * Caption: $Views_KrVirtualFiles_FileTemplate.
   */
   readonly ColumnKrVirtualFileFileTemplate: ViewObject = new ViewObject(2, 'KrVirtualFileFileTemplate', '$Views_KrVirtualFiles_FileTemplate');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_KrVirtualFiles_Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_KrVirtualFiles_Name');

  //#endregion
}

//#endregion

//#region Languages

/**
 * ID: {7ed54a59-1c9e-469b-83eb-ed1c6ec70753}
 * Alias: Languages
 * Caption: $Views_Names_Languages
 * Group: System
 */
class LanguagesViewInfo {
  //#region Common

  /**
   * View identifier for "Languages": {7ed54a59-1c9e-469b-83eb-ed1c6ec70753}.
   */
   readonly ID: guid = '7ed54a59-1c9e-469b-83eb-ed1c6ec70753';

  /**
   * View name for "Languages".
   */
   readonly Alias: string = 'Languages';

  /**
   * View caption for "Languages".
   */
   readonly Caption: string = '$Views_Names_Languages';

  /**
   * View group for "Languages".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: LanguageID.
   */
   readonly ColumnLanguageID: ViewObject = new ViewObject(0, 'LanguageID');

  /**
   * ID:1
   * Alias: LanguageCaption
   * Caption: $Views_KrLanguages_Caption.
   */
   readonly ColumnLanguageCaption: ViewObject = new ViewObject(1, 'LanguageCaption', '$Views_KrLanguages_Caption');

  /**
   * ID:2
   * Alias: LanguageCode.
   */
   readonly ColumnLanguageCode: ViewObject = new ViewObject(2, 'LanguageCode');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CaptionParam
   * Caption: $Views_KrLanguages_Caption_Param.
   */
   readonly ParamCaptionParam: ViewObject = new ViewObject(0, 'CaptionParam', '$Views_KrLanguages_Caption_Param');

  //#endregion
}

//#endregion

//#region LastTopics

/**
 * ID: {ba6ff2de-b4d3-47cc-9f98-29baabdd6bce}
 * Alias: LastTopics
 * Caption: $Views_Names_LastTopics
 * Group: Fm
 */
class LastTopicsViewInfo {
  //#region Common

  /**
   * View identifier for "LastTopics": {ba6ff2de-b4d3-47cc-9f98-29baabdd6bce}.
   */
   readonly ID: guid = 'ba6ff2de-b4d3-47cc-9f98-29baabdd6bce';

  /**
   * View name for "LastTopics".
   */
   readonly Alias: string = 'LastTopics';

  /**
   * View caption for "LastTopics".
   */
   readonly Caption: string = '$Views_Names_LastTopics';

  /**
   * View group for "LastTopics".
   */
   readonly Group: string = 'Fm';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(0, 'CardID');

  /**
   * ID:1
   * Alias: Created
   * Caption: $Views_MyTopics_Created.
   */
   readonly ColumnCreated: ViewObject = new ViewObject(1, 'Created', '$Views_MyTopics_Created');

  /**
   * ID:2
   * Alias: TopicID.
   */
   readonly ColumnTopicID: ViewObject = new ViewObject(2, 'TopicID');

  /**
   * ID:3
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(3, 'TypeID');

  /**
   * ID:4
   * Alias: TopicName
   * Caption: $Views_MyTopics_TopicName.
   */
   readonly ColumnTopicName: ViewObject = new ViewObject(4, 'TopicName', '$Views_MyTopics_TopicName');

  /**
   * ID:5
   * Alias: Description
   * Caption: $Views_MyTopics_Description.
   */
   readonly ColumnDescription: ViewObject = new ViewObject(5, 'Description', '$Views_MyTopics_Description');

  /**
   * ID:6
   * Alias: AuthorName
   * Caption: $Views_MyTopics_AuthorName.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(6, 'AuthorName', '$Views_MyTopics_AuthorName');

  /**
   * ID:7
   * Alias: LastRead
   * Caption: $Views_MyTopics_LastRead.
   */
   readonly ColumnLastRead: ViewObject = new ViewObject(7, 'LastRead', '$Views_MyTopics_LastRead');

  /**
   * ID:8
   * Alias: LastMessage
   * Caption: $Views_MyTopics_LastMessage.
   */
   readonly ColumnLastMessage: ViewObject = new ViewObject(8, 'LastMessage', '$Views_MyTopics_LastMessage');

  /**
   * ID:9
   * Alias: LastMessageAuthorName
   * Caption: $Views_MyTopics_LastMessageAuthorName.
   */
   readonly ColumnLastMessageAuthorName: ViewObject = new ViewObject(9, 'LastMessageAuthorName', '$Views_MyTopics_LastMessageAuthorName');

  /**
   * ID:10
   * Alias: IsArchived.
   */
   readonly ColumnIsArchived: ViewObject = new ViewObject(10, 'IsArchived');

  /**
   * ID:11
   * Alias: AppearanceColumn.
   */
   readonly ColumnAppearanceColumn: ViewObject = new ViewObject(11, 'AppearanceColumn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Created
   * Caption: $Views_MyTopics_Created.
   */
   readonly ParamCreated: ViewObject = new ViewObject(0, 'Created', '$Views_MyTopics_Created');

  /**
   * ID:1
   * Alias: IsArchived
   * Caption: $Views_MyTopics_ShowArchived.
   */
   readonly ParamIsArchived: ViewObject = new ViewObject(1, 'IsArchived', '$Views_MyTopics_ShowArchived');

  //#endregion
}

//#endregion

//#region LawCases

/**
 * ID: {fc9df545-e9f7-4562-a37d-f8da4a10d248}
 * Alias: LawCases
 * Caption: $Views_Names_LawCases
 * Group: Law
 */
class LawCasesViewInfo {
  //#region Common

  /**
   * View identifier for "LawCases": {fc9df545-e9f7-4562-a37d-f8da4a10d248}.
   */
   readonly ID: guid = 'fc9df545-e9f7-4562-a37d-f8da4a10d248';

  /**
   * View name for "LawCases".
   */
   readonly Alias: string = 'LawCases';

  /**
   * View caption for "LawCases".
   */
   readonly Caption: string = '$Views_Names_LawCases';

  /**
   * View group for "LawCases".
   */
   readonly Group: string = 'Law';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(0, 'rn');

  /**
   * ID:1
   * Alias: ID.
   */
   readonly ColumnID: ViewObject = new ViewObject(1, 'ID');

  /**
   * ID:2
   * Alias: Number
   * Caption: $Views_LawCases_Number.
   */
   readonly ColumnNumber: ViewObject = new ViewObject(2, 'Number', '$Views_LawCases_Number');

  /**
   * ID:3
   * Alias: StartDate
   * Caption: $Views_LawCases_StartDate.
   */
   readonly ColumnStartDate: ViewObject = new ViewObject(3, 'StartDate', '$Views_LawCases_StartDate');

  /**
   * ID:4
   * Alias: DecisionDate
   * Caption: $Views_LawCases_DecisionDate.
   */
   readonly ColumnDecisionDate: ViewObject = new ViewObject(4, 'DecisionDate', '$Views_LawCases_DecisionDate');

  /**
   * ID:5
   * Alias: NumberByCourt
   * Caption: $Views_LawCases_NumberByCourt.
   */
   readonly ColumnNumberByCourt: ViewObject = new ViewObject(5, 'NumberByCourt', '$Views_LawCases_NumberByCourt');

  /**
   * ID:6
   * Alias: Description
   * Caption: $Views_LawCases_Description.
   */
   readonly ColumnDescription: ViewObject = new ViewObject(6, 'Description', '$Views_LawCases_Description');

  /**
   * ID:7
   * Alias: Clients
   * Caption: $Views_LawCases_Clients.
   */
   readonly ColumnClients: ViewObject = new ViewObject(7, 'Clients', '$Views_LawCases_Clients');

  /**
   * ID:8
   * Alias: Partners
   * Caption: $Views_LawCases_Partners.
   */
   readonly ColumnPartners: ViewObject = new ViewObject(8, 'Partners', '$Views_LawCases_Partners');

  /**
   * ID:9
   * Alias: Pcto
   * Caption: PCTO.
   */
   readonly ColumnPcto: ViewObject = new ViewObject(9, 'Pcto', 'PCTO');

  /**
   * ID:10
   * Alias: ClassificationPlan
   * Caption: $Views_LawCases_ClassificationPlan.
   */
   readonly ColumnClassificationPlan: ViewObject = new ViewObject(10, 'ClassificationPlan', '$Views_LawCases_ClassificationPlan');

  /**
   * ID:11
   * Alias: StoreLocation
   * Caption: $Views_LawCases_StoreLocation.
   */
   readonly ColumnStoreLocation: ViewObject = new ViewObject(11, 'StoreLocation', '$Views_LawCases_StoreLocation');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Number
   * Caption: $Views_LawCases_Number.
   */
   readonly ParamNumber: ViewObject = new ViewObject(0, 'Number', '$Views_LawCases_Number');

  /**
   * ID:1
   * Alias: NumberByCourt
   * Caption: $Views_LawCases_NumberByCourt.
   */
   readonly ParamNumberByCourt: ViewObject = new ViewObject(1, 'NumberByCourt', '$Views_LawCases_NumberByCourt');

  /**
   * ID:2
   * Alias: Description
   * Caption: $Views_LawCases_Description.
   */
   readonly ParamDescription: ViewObject = new ViewObject(2, 'Description', '$Views_LawCases_Description');

  /**
   * ID:3
   * Alias: ClassificationPlan
   * Caption: $Views_LawCases_ClassificationPlan.
   */
   readonly ParamClassificationPlan: ViewObject = new ViewObject(3, 'ClassificationPlan', '$Views_LawCases_ClassificationPlan');

  /**
   * ID:4
   * Alias: StartDate
   * Caption: $Views_LawCases_StartDate.
   */
   readonly ParamStartDate: ViewObject = new ViewObject(4, 'StartDate', '$Views_LawCases_StartDate');

  /**
   * ID:5
   * Alias: Administrators
   * Caption: $Views_LawCases_Administrators.
   */
   readonly ParamAdministrators: ViewObject = new ViewObject(5, 'Administrators', '$Views_LawCases_Administrators');

  //#endregion
}

//#endregion

//#region LawCategories

/**
 * ID: {abe65a63-77cd-4f40-a6ce-d5c51ac1d022}
 * Alias: LawCategories
 * Caption: $Views_Names_LawCategories
 * Group: LawDictionary
 */
class LawCategoriesViewInfo {
  //#region Common

  /**
   * View identifier for "LawCategories": {abe65a63-77cd-4f40-a6ce-d5c51ac1d022}.
   */
   readonly ID: guid = 'abe65a63-77cd-4f40-a6ce-d5c51ac1d022';

  /**
   * View name for "LawCategories".
   */
   readonly Alias: string = 'LawCategories';

  /**
   * View caption for "LawCategories".
   */
   readonly Caption: string = '$Views_Names_LawCategories';

  /**
   * View group for "LawCategories".
   */
   readonly Group: string = 'LawDictionary';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CategoryID.
   */
   readonly ColumnCategoryID: ViewObject = new ViewObject(0, 'CategoryID');

  /**
   * ID:1
   * Alias: CategoryNumber
   * Caption: $Views_LawCategories_CategoryNumber.
   */
   readonly ColumnCategoryNumber: ViewObject = new ViewObject(1, 'CategoryNumber', '$Views_LawCategories_CategoryNumber');

  /**
   * ID:2
   * Alias: CategoryIcon
   * Caption: $Views_LawCategories_CategoryIcon.
   */
   readonly ColumnCategoryIcon: ViewObject = new ViewObject(2, 'CategoryIcon', '$Views_LawCategories_CategoryIcon');

  /**
   * ID:3
   * Alias: CategoryName
   * Caption: $Views_LawCategories_CategoryName.
   */
   readonly ColumnCategoryName: ViewObject = new ViewObject(3, 'CategoryName', '$Views_LawCategories_CategoryName');

  /**
   * ID:4
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(4, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Number
   * Caption: $Views_LawCategories_CategoryNumber.
   */
   readonly ParamNumber: ViewObject = new ViewObject(0, 'Number', '$Views_LawCategories_CategoryNumber');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_LawCategories_CategoryName.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_LawCategories_CategoryName');

  //#endregion
}

//#endregion

//#region LawClassificationPlans

/**
 * ID: {838245d0-f5b9-4ed6-9343-ee74d383f689}
 * Alias: LawClassificationPlans
 * Caption: $Views_Names_LawClassificationPlans
 * Group: LawDictionary
 */
class LawClassificationPlansViewInfo {
  //#region Common

  /**
   * View identifier for "LawClassificationPlans": {838245d0-f5b9-4ed6-9343-ee74d383f689}.
   */
   readonly ID: guid = '838245d0-f5b9-4ed6-9343-ee74d383f689';

  /**
   * View name for "LawClassificationPlans".
   */
   readonly Alias: string = 'LawClassificationPlans';

  /**
   * View caption for "LawClassificationPlans".
   */
   readonly Caption: string = '$Views_Names_LawClassificationPlans';

  /**
   * View group for "LawClassificationPlans".
   */
   readonly Group: string = 'LawDictionary';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: PlanID.
   */
   readonly ColumnPlanID: ViewObject = new ViewObject(0, 'PlanID');

  /**
   * ID:1
   * Alias: PlanFullName.
   */
   readonly ColumnPlanFullName: ViewObject = new ViewObject(1, 'PlanFullName');

  /**
   * ID:2
   * Alias: PlanPlan
   * Caption: $Views_LawClassificationPlans_PlanPlan.
   */
   readonly ColumnPlanPlan: ViewObject = new ViewObject(2, 'PlanPlan', '$Views_LawClassificationPlans_PlanPlan');

  /**
   * ID:3
   * Alias: PlanName
   * Caption: $Views_LawClassificationPlans_PlanName.
   */
   readonly ColumnPlanName: ViewObject = new ViewObject(3, 'PlanName', '$Views_LawClassificationPlans_PlanName');

  /**
   * ID:4
   * Alias: PlanDescription
   * Caption: $Views_LawClassificationPlans_Description.
   */
   readonly ColumnPlanDescription: ViewObject = new ViewObject(4, 'PlanDescription', '$Views_LawClassificationPlans_Description');

  /**
   * ID:5
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(5, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Plan
   * Caption: $Views_LawClassificationPlans_PlanPlan.
   */
   readonly ParamPlan: ViewObject = new ViewObject(0, 'Plan', '$Views_LawClassificationPlans_PlanPlan');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_LawClassificationPlans_PlanName.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_LawClassificationPlans_PlanName');

  /**
   * ID:2
   * Alias: Description
   * Caption: $Views_LawClassificationPlans_Description.
   */
   readonly ParamDescription: ViewObject = new ViewObject(2, 'Description', '$Views_LawClassificationPlans_Description');

  //#endregion
}

//#endregion

//#region LawClients

/**
 * ID: {d87978ea-f6d4-4cec-9a74-3354a910c5f1}
 * Alias: LawClients
 * Caption: $Views_Names_LawClients
 * Group: LawDictionary
 */
class LawClientsViewInfo {
  //#region Common

  /**
   * View identifier for "LawClients": {d87978ea-f6d4-4cec-9a74-3354a910c5f1}.
   */
   readonly ID: guid = 'd87978ea-f6d4-4cec-9a74-3354a910c5f1';

  /**
   * View name for "LawClients".
   */
   readonly Alias: string = 'LawClients';

  /**
   * View caption for "LawClients".
   */
   readonly Caption: string = '$Views_Names_LawClients';

  /**
   * View group for "LawClients".
   */
   readonly Group: string = 'LawDictionary';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ClientID.
   */
   readonly ColumnClientID: ViewObject = new ViewObject(0, 'ClientID');

  /**
   * ID:1
   * Alias: ClientName
   * Caption: $Views_LawClients_ClientName.
   */
   readonly ColumnClientName: ViewObject = new ViewObject(1, 'ClientName', '$Views_LawClients_ClientName');

  /**
   * ID:2
   * Alias: ClientType
   * Caption: $Views_LawClients_ClientType.
   */
   readonly ColumnClientType: ViewObject = new ViewObject(2, 'ClientType', '$Views_LawClients_ClientType');

  /**
   * ID:3
   * Alias: ClientKindID.
   */
   readonly ColumnClientKindID: ViewObject = new ViewObject(3, 'ClientKindID');

  /**
   * ID:4
   * Alias: ClientKindName
   * Caption: $Views_LawClients_ClientKindName.
   */
   readonly ColumnClientKindName: ViewObject = new ViewObject(4, 'ClientKindName', '$Views_LawClients_ClientKindName');

  /**
   * ID:5
   * Alias: ClientTaxNumber
   * Caption: $Views_LawClients_ClientTaxNumber.
   */
   readonly ColumnClientTaxNumber: ViewObject = new ViewObject(5, 'ClientTaxNumber', '$Views_LawClients_ClientTaxNumber');

  /**
   * ID:6
   * Alias: ClientRegistrationNumber
   * Caption: $Views_LawClients_ClientRegistrationNumber.
   */
   readonly ColumnClientRegistrationNumber: ViewObject = new ViewObject(6, 'ClientRegistrationNumber', '$Views_LawClients_ClientRegistrationNumber');

  /**
   * ID:7
   * Alias: ClientAddress
   * Caption: $Views_LawClients_ClientAddress.
   */
   readonly ColumnClientAddress: ViewObject = new ViewObject(7, 'ClientAddress', '$Views_LawClients_ClientAddress');

  /**
   * ID:8
   * Alias: ClientContacts
   * Caption: $Views_LawClients_ClientContacts.
   */
   readonly ColumnClientContacts: ViewObject = new ViewObject(8, 'ClientContacts', '$Views_LawClients_ClientContacts');

  /**
   * ID:9
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(9, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_LawClients_ClientName.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_LawClients_ClientName');

  /**
   * ID:1
   * Alias: TaxNumber
   * Caption: $Views_LawClients_ClientTaxNumber.
   */
   readonly ParamTaxNumber: ViewObject = new ViewObject(1, 'TaxNumber', '$Views_LawClients_ClientTaxNumber');

  /**
   * ID:2
   * Alias: RegistrationNumber
   * Caption: $Views_LawClients_ClientRegistrationNumber.
   */
   readonly ParamRegistrationNumber: ViewObject = new ViewObject(2, 'RegistrationNumber', '$Views_LawClients_ClientRegistrationNumber');

  /**
   * ID:3
   * Alias: Contacts
   * Caption: $Views_LawClients_ClientContacts.
   */
   readonly ParamContacts: ViewObject = new ViewObject(3, 'Contacts', '$Views_LawClients_ClientContacts');

  /**
   * ID:4
   * Alias: EntityKind
   * Caption: $Views_LawClients_ClientKindName.
   */
   readonly ParamEntityKind: ViewObject = new ViewObject(4, 'EntityKind', '$Views_LawClients_ClientKindName');

  /**
   * ID:5
   * Alias: Street
   * Caption: $Views_LawClients_Street.
   */
   readonly ParamStreet: ViewObject = new ViewObject(5, 'Street', '$Views_LawClients_Street');

  /**
   * ID:6
   * Alias: PostOffice
   * Caption: $Views_LawClients_PostOffice.
   */
   readonly ParamPostOffice: ViewObject = new ViewObject(6, 'PostOffice', '$Views_LawClients_PostOffice');

  /**
   * ID:7
   * Alias: Region
   * Caption: $Views_LawClients_Region.
   */
   readonly ParamRegion: ViewObject = new ViewObject(7, 'Region', '$Views_LawClients_Region');

  /**
   * ID:8
   * Alias: Country
   * Caption: $Views_LawClients_Country.
   */
   readonly ParamCountry: ViewObject = new ViewObject(8, 'Country', '$Views_LawClients_Country');

  //#endregion
}

//#endregion

//#region LawDocKinds

/**
 * ID: {121cee33-1f41-491b-8ac0-1d3d199be43a}
 * Alias: LawDocKinds
 * Caption: $Views_Names_LawDocKinds
 * Group: LawDictionary
 */
class LawDocKindsViewInfo {
  //#region Common

  /**
   * View identifier for "LawDocKinds": {121cee33-1f41-491b-8ac0-1d3d199be43a}.
   */
   readonly ID: guid = '121cee33-1f41-491b-8ac0-1d3d199be43a';

  /**
   * View name for "LawDocKinds".
   */
   readonly Alias: string = 'LawDocKinds';

  /**
   * View caption for "LawDocKinds".
   */
   readonly Caption: string = '$Views_Names_LawDocKinds';

  /**
   * View group for "LawDocKinds".
   */
   readonly Group: string = 'LawDictionary';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KindID.
   */
   readonly ColumnKindID: ViewObject = new ViewObject(0, 'KindID');

  /**
   * ID:1
   * Alias: KindDirection
   * Caption: $Views_LawDocKinds_KindDirection.
   */
   readonly ColumnKindDirection: ViewObject = new ViewObject(1, 'KindDirection', '$Views_LawDocKinds_KindDirection');

  /**
   * ID:2
   * Alias: KindName
   * Caption: $Views_LawDocKinds_KindName.
   */
   readonly ColumnKindName: ViewObject = new ViewObject(2, 'KindName', '$Views_LawDocKinds_KindName');

  /**
   * ID:3
   * Alias: KindByDefault
   * Caption: $Views_LawDocKinds_KindByDefault.
   */
   readonly ColumnKindByDefault: ViewObject = new ViewObject(3, 'KindByDefault', '$Views_LawDocKinds_KindByDefault');

  /**
   * ID:4
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(4, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Direction
   * Caption: $Views_LawDocKinds_KindDirection.
   */
   readonly ParamDirection: ViewObject = new ViewObject(0, 'Direction', '$Views_LawDocKinds_KindDirection');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_LawDocKinds_KindName.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_LawDocKinds_KindName');

  /**
   * ID:2
   * Alias: ByDefault
   * Caption: $Views_LawDocKinds_KindByDefault.
   */
   readonly ParamByDefault: ViewObject = new ViewObject(2, 'ByDefault', '$Views_LawDocKinds_KindByDefault');

  //#endregion
}

//#endregion

//#region LawDocTypes

/**
 * ID: {79b46d45-d6ac-4b46-8051-7cfabc879bfe}
 * Alias: LawDocTypes
 * Caption: $Views_Names_LawDocTypes
 * Group: LawDictionary
 */
class LawDocTypesViewInfo {
  //#region Common

  /**
   * View identifier for "LawDocTypes": {79b46d45-d6ac-4b46-8051-7cfabc879bfe}.
   */
   readonly ID: guid = '79b46d45-d6ac-4b46-8051-7cfabc879bfe';

  /**
   * View name for "LawDocTypes".
   */
   readonly Alias: string = 'LawDocTypes';

  /**
   * View caption for "LawDocTypes".
   */
   readonly Caption: string = '$Views_Names_LawDocTypes';

  /**
   * View group for "LawDocTypes".
   */
   readonly Group: string = 'LawDictionary';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeName
   * Caption: $Views_LawDocTypes_TypeName.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(1, 'TypeName', '$Views_LawDocTypes_TypeName');

  /**
   * ID:2
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(2, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_LawDocTypes_TypeName.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_LawDocTypes_TypeName');

  //#endregion
}

//#endregion

//#region LawEntityKinds

/**
 * ID: {05d82dad-cf51-4917-bb9e-6d76ede89d0b}
 * Alias: LawEntityKinds
 * Caption: $Views_Names_LawEntityKinds
 * Group: LawDictionary
 */
class LawEntityKindsViewInfo {
  //#region Common

  /**
   * View identifier for "LawEntityKinds": {05d82dad-cf51-4917-bb9e-6d76ede89d0b}.
   */
   readonly ID: guid = '05d82dad-cf51-4917-bb9e-6d76ede89d0b';

  /**
   * View name for "LawEntityKinds".
   */
   readonly Alias: string = 'LawEntityKinds';

  /**
   * View caption for "LawEntityKinds".
   */
   readonly Caption: string = '$Views_Names_LawEntityKinds';

  /**
   * View group for "LawEntityKinds".
   */
   readonly Group: string = 'LawDictionary';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KindID.
   */
   readonly ColumnKindID: ViewObject = new ViewObject(0, 'KindID');

  /**
   * ID:1
   * Alias: KindName
   * Caption: $Views_LawEntityKinds_KindName.
   */
   readonly ColumnKindName: ViewObject = new ViewObject(1, 'KindName', '$Views_LawEntityKinds_KindName');

  /**
   * ID:2
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(2, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_LawDocTypes_TypeName.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_LawDocTypes_TypeName');

  //#endregion
}

//#endregion

//#region LawFileStorages

/**
 * ID: {0160728f-e47c-426e-a569-09491c421091}
 * Alias: LawFileStorages
 * Caption: $Views_Names_LawFileStorages
 * Group: LawDictionary
 */
class LawFileStoragesViewInfo {
  //#region Common

  /**
   * View identifier for "LawFileStorages": {0160728f-e47c-426e-a569-09491c421091}.
   */
   readonly ID: guid = '0160728f-e47c-426e-a569-09491c421091';

  /**
   * View name for "LawFileStorages".
   */
   readonly Alias: string = 'LawFileStorages';

  /**
   * View caption for "LawFileStorages".
   */
   readonly Caption: string = '$Views_Names_LawFileStorages';

  /**
   * View group for "LawFileStorages".
   */
   readonly Group: string = 'LawDictionary';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: StorageID.
   */
   readonly ColumnStorageID: ViewObject = new ViewObject(0, 'StorageID');

  /**
   * ID:1
   * Alias: StorageName
   * Caption: $Views_LawFileStorages_StorageName.
   */
   readonly ColumnStorageName: ViewObject = new ViewObject(1, 'StorageName', '$Views_LawFileStorages_StorageName');

  /**
   * ID:2
   * Alias: StorageType
   * Caption: $Views_LawFileStorages_StorageType.
   */
   readonly ColumnStorageType: ViewObject = new ViewObject(2, 'StorageType', '$Views_LawFileStorages_StorageType');

  /**
   * ID:3
   * Alias: StorageNotes
   * Caption: $Views_LawFileStorages_StorageNotes.
   */
   readonly ColumnStorageNotes: ViewObject = new ViewObject(3, 'StorageNotes', '$Views_LawFileStorages_StorageNotes');

  /**
   * ID:4
   * Alias: StorageComputerName
   * Caption: $Views_LawFileStorages_StorageComputerName.
   */
   readonly ColumnStorageComputerName: ViewObject = new ViewObject(4, 'StorageComputerName', '$Views_LawFileStorages_StorageComputerName');

  /**
   * ID:5
   * Alias: StorageByDefault
   * Caption: $Views_LawFileStorages_StorageByDefault.
   */
   readonly ColumnStorageByDefault: ViewObject = new ViewObject(5, 'StorageByDefault', '$Views_LawFileStorages_StorageByDefault');

  /**
   * ID:6
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(6, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_LawFileStorages_StorageName.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_LawFileStorages_StorageName');

  /**
   * ID:1
   * Alias: Type
   * Caption: $Views_LawFileStorages_StorageType.
   */
   readonly ParamType: ViewObject = new ViewObject(1, 'Type', '$Views_LawFileStorages_StorageType');

  /**
   * ID:2
   * Alias: Notes
   * Caption: $Views_LawFileStorages_StorageNotes.
   */
   readonly ParamNotes: ViewObject = new ViewObject(2, 'Notes', '$Views_LawFileStorages_StorageNotes');

  /**
   * ID:3
   * Alias: ComputerName
   * Caption: $Views_LawFileStorages_StorageComputerName.
   */
   readonly ParamComputerName: ViewObject = new ViewObject(3, 'ComputerName', '$Views_LawFileStorages_StorageComputerName');

  /**
   * ID:4
   * Alias: ByDefault
   * Caption: $Views_LawFileStorages_StorageByDefault.
   */
   readonly ParamByDefault: ViewObject = new ViewObject(4, 'ByDefault', '$Views_LawFileStorages_StorageByDefault');

  //#endregion
}

//#endregion

//#region LawFolders

/**
 * ID: {79dba5d9-833e-49ee-a6be-9530abe314f1}
 * Alias: LawFolders
 * Caption: $Views_Names_LawFolders
 * Group: Law
 */
class LawFoldersViewInfo {
  //#region Common

  /**
   * View identifier for "LawFolders": {79dba5d9-833e-49ee-a6be-9530abe314f1}.
   */
   readonly ID: guid = '79dba5d9-833e-49ee-a6be-9530abe314f1';

  /**
   * View name for "LawFolders".
   */
   readonly Alias: string = 'LawFolders';

  /**
   * View caption for "LawFolders".
   */
   readonly Caption: string = '$Views_Names_LawFolders';

  /**
   * View group for "LawFolders".
   */
   readonly Group: string = 'Law';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RowID.
   */
   readonly ColumnRowID: ViewObject = new ViewObject(0, 'RowID');

  /**
   * ID:1
   * Alias: ParentRowID.
   */
   readonly ColumnParentRowID: ViewObject = new ViewObject(1, 'ParentRowID');

  /**
   * ID:2
   * Alias: Name
   * Caption: $Views_LawFolders_Name.
   */
   readonly ColumnName: ViewObject = new ViewObject(2, 'Name', '$Views_LawFolders_Name');

  /**
   * ID:3
   * Alias: Date
   * Caption: $Views_LawFolders_Date.
   */
   readonly ColumnDate: ViewObject = new ViewObject(3, 'Date', '$Views_LawFolders_Date');

  /**
   * ID:4
   * Alias: Kind
   * Caption: $Views_LawFolders_Kind.
   */
   readonly ColumnKind: ViewObject = new ViewObject(4, 'Kind', '$Views_LawFolders_Kind');

  /**
   * ID:5
   * Alias: Type.
   */
   readonly ColumnType: ViewObject = new ViewObject(5, 'Type');

  /**
   * ID:6
   * Alias: Number
   * Caption: $Views_LawFolders_Number.
   */
   readonly ColumnNumber: ViewObject = new ViewObject(6, 'Number', '$Views_LawFolders_Number');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CaseID.
   */
   readonly ParamCaseID: ViewObject = new ViewObject(0, 'CaseID');

  //#endregion
}

//#endregion

//#region LawPartnerRepresentatives

/**
 * ID: {c8f9dad9-f2e0-40f8-9e6f-9fabb2e8fe83}
 * Alias: LawPartnerRepresentatives
 * Caption: $Views_Names_LawPartnerRepresentatives
 * Group: LawDictionary
 */
class LawPartnerRepresentativesViewInfo {
  //#region Common

  /**
   * View identifier for "LawPartnerRepresentatives": {c8f9dad9-f2e0-40f8-9e6f-9fabb2e8fe83}.
   */
   readonly ID: guid = 'c8f9dad9-f2e0-40f8-9e6f-9fabb2e8fe83';

  /**
   * View name for "LawPartnerRepresentatives".
   */
   readonly Alias: string = 'LawPartnerRepresentatives';

  /**
   * View caption for "LawPartnerRepresentatives".
   */
   readonly Caption: string = '$Views_Names_LawPartnerRepresentatives';

  /**
   * View group for "LawPartnerRepresentatives".
   */
   readonly Group: string = 'LawDictionary';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RepresentativeID.
   */
   readonly ColumnRepresentativeID: ViewObject = new ViewObject(0, 'RepresentativeID');

  /**
   * ID:1
   * Alias: RepresentativeName
   * Caption: $Views_LawPartnerRepresentatives_RepresentativeName.
   */
   readonly ColumnRepresentativeName: ViewObject = new ViewObject(1, 'RepresentativeName', '$Views_LawPartnerRepresentatives_RepresentativeName');

  /**
   * ID:2
   * Alias: RepresentativeType
   * Caption: $Views_LawPartnerRepresentatives_RepresentativeType.
   */
   readonly ColumnRepresentativeType: ViewObject = new ViewObject(2, 'RepresentativeType', '$Views_LawPartnerRepresentatives_RepresentativeType');

  /**
   * ID:3
   * Alias: RepresentativeKindID.
   */
   readonly ColumnRepresentativeKindID: ViewObject = new ViewObject(3, 'RepresentativeKindID');

  /**
   * ID:4
   * Alias: RepresentativeKindName
   * Caption: $Views_LawPartnerRepresentatives_RepresentativeKindName.
   */
   readonly ColumnRepresentativeKindName: ViewObject = new ViewObject(4, 'RepresentativeKindName', '$Views_LawPartnerRepresentatives_RepresentativeKindName');

  /**
   * ID:5
   * Alias: RepresentativeTaxNumber
   * Caption: $Views_LawPartnerRepresentatives_RepresentativeTaxNumber.
   */
   readonly ColumnRepresentativeTaxNumber: ViewObject = new ViewObject(5, 'RepresentativeTaxNumber', '$Views_LawPartnerRepresentatives_RepresentativeTaxNumber');

  /**
   * ID:6
   * Alias: RepresentativeRegistrationNumber
   * Caption: $Views_LawPartnerRepresentatives_RepresentativeRegistrationNumber.
   */
   readonly ColumnRepresentativeRegistrationNumber: ViewObject = new ViewObject(6, 'RepresentativeRegistrationNumber', '$Views_LawPartnerRepresentatives_RepresentativeRegistrationNumber');

  /**
   * ID:7
   * Alias: RepresentativeAddress
   * Caption: $Views_LawPartnerRepresentatives_RepresentativeAddress.
   */
   readonly ColumnRepresentativeAddress: ViewObject = new ViewObject(7, 'RepresentativeAddress', '$Views_LawPartnerRepresentatives_RepresentativeAddress');

  /**
   * ID:8
   * Alias: RepresentativeAddressID.
   */
   readonly ColumnRepresentativeAddressID: ViewObject = new ViewObject(8, 'RepresentativeAddressID');

  /**
   * ID:9
   * Alias: RepresentativeContacts
   * Caption: $Views_LawPartnerRepresentatives_RepresentativeContacts.
   */
   readonly ColumnRepresentativeContacts: ViewObject = new ViewObject(9, 'RepresentativeContacts', '$Views_LawPartnerRepresentatives_RepresentativeContacts');

  /**
   * ID:10
   * Alias: RepresentativeStreet.
   */
   readonly ColumnRepresentativeStreet: ViewObject = new ViewObject(10, 'RepresentativeStreet');

  /**
   * ID:11
   * Alias: RepresentativePostalCode.
   */
   readonly ColumnRepresentativePostalCode: ViewObject = new ViewObject(11, 'RepresentativePostalCode');

  /**
   * ID:12
   * Alias: RepresentativeCity.
   */
   readonly ColumnRepresentativeCity: ViewObject = new ViewObject(12, 'RepresentativeCity');

  /**
   * ID:13
   * Alias: RepresentativeCountry.
   */
   readonly ColumnRepresentativeCountry: ViewObject = new ViewObject(13, 'RepresentativeCountry');

  /**
   * ID:14
   * Alias: RepresentativePoBox.
   */
   readonly ColumnRepresentativePoBox: ViewObject = new ViewObject(14, 'RepresentativePoBox');

  /**
   * ID:15
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(15, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_LawPartners_PartnerName.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_LawPartners_PartnerName');

  /**
   * ID:1
   * Alias: TaxNumber
   * Caption: $Views_LawPartnerRepresentatives_RepresentativeTaxNumber.
   */
   readonly ParamTaxNumber: ViewObject = new ViewObject(1, 'TaxNumber', '$Views_LawPartnerRepresentatives_RepresentativeTaxNumber');

  /**
   * ID:2
   * Alias: RegistrationNumber
   * Caption: $Views_LawPartnerRepresentatives_RepresentativeRegistrationNumber.
   */
   readonly ParamRegistrationNumber: ViewObject = new ViewObject(2, 'RegistrationNumber', '$Views_LawPartnerRepresentatives_RepresentativeRegistrationNumber');

  /**
   * ID:3
   * Alias: Contacts
   * Caption: $Views_LawPartnerRepresentatives_RepresentativeContacts.
   */
   readonly ParamContacts: ViewObject = new ViewObject(3, 'Contacts', '$Views_LawPartnerRepresentatives_RepresentativeContacts');

  /**
   * ID:4
   * Alias: EntityKind
   * Caption: $Views_LawPartnerRepresentatives_RepresentativeKindName.
   */
   readonly ParamEntityKind: ViewObject = new ViewObject(4, 'EntityKind', '$Views_LawPartnerRepresentatives_RepresentativeKindName');

  /**
   * ID:5
   * Alias: Street
   * Caption: $Views_LawPartnerRepresentatives_Street.
   */
   readonly ParamStreet: ViewObject = new ViewObject(5, 'Street', '$Views_LawPartnerRepresentatives_Street');

  /**
   * ID:6
   * Alias: PostOffice
   * Caption: $Views_LawPartnerRepresentatives_PostOffice.
   */
   readonly ParamPostOffice: ViewObject = new ViewObject(6, 'PostOffice', '$Views_LawPartnerRepresentatives_PostOffice');

  /**
   * ID:7
   * Alias: Region
   * Caption: $Views_LawPartnerRepresentatives_Region.
   */
   readonly ParamRegion: ViewObject = new ViewObject(7, 'Region', '$Views_LawPartnerRepresentatives_Region');

  /**
   * ID:8
   * Alias: Country
   * Caption: $Views_LawPartnerRepresentatives_Country.
   */
   readonly ParamCountry: ViewObject = new ViewObject(8, 'Country', '$Views_LawPartnerRepresentatives_Country');

  //#endregion
}

//#endregion

//#region LawPartners

/**
 * ID: {0f2cecd3-2051-4c26-8c5a-312dbecfb2fc}
 * Alias: LawPartners
 * Caption: $Views_Names_LawPartners
 * Group: LawDictionary
 */
class LawPartnersViewInfo {
  //#region Common

  /**
   * View identifier for "LawPartners": {0f2cecd3-2051-4c26-8c5a-312dbecfb2fc}.
   */
   readonly ID: guid = '0f2cecd3-2051-4c26-8c5a-312dbecfb2fc';

  /**
   * View name for "LawPartners".
   */
   readonly Alias: string = 'LawPartners';

  /**
   * View caption for "LawPartners".
   */
   readonly Caption: string = '$Views_Names_LawPartners';

  /**
   * View group for "LawPartners".
   */
   readonly Group: string = 'LawDictionary';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: PartnerID.
   */
   readonly ColumnPartnerID: ViewObject = new ViewObject(0, 'PartnerID');

  /**
   * ID:1
   * Alias: PartnerName
   * Caption: $Views_LawPartners_PartnerName.
   */
   readonly ColumnPartnerName: ViewObject = new ViewObject(1, 'PartnerName', '$Views_LawPartners_PartnerName');

  /**
   * ID:2
   * Alias: PartnerType
   * Caption: $Views_LawPartners_PartnerType.
   */
   readonly ColumnPartnerType: ViewObject = new ViewObject(2, 'PartnerType', '$Views_LawPartners_PartnerType');

  /**
   * ID:3
   * Alias: PartnerKindID.
   */
   readonly ColumnPartnerKindID: ViewObject = new ViewObject(3, 'PartnerKindID');

  /**
   * ID:4
   * Alias: PartnerKindName
   * Caption: $Views_LawPartners_PartnerKindName.
   */
   readonly ColumnPartnerKindName: ViewObject = new ViewObject(4, 'PartnerKindName', '$Views_LawPartners_PartnerKindName');

  /**
   * ID:5
   * Alias: PartnerTaxNumber
   * Caption: $Views_LawPartners_PartnerTaxNumber.
   */
   readonly ColumnPartnerTaxNumber: ViewObject = new ViewObject(5, 'PartnerTaxNumber', '$Views_LawPartners_PartnerTaxNumber');

  /**
   * ID:6
   * Alias: PartnerRegistrationNumber
   * Caption: $Views_LawPartners_PartnerRegistrationNumber.
   */
   readonly ColumnPartnerRegistrationNumber: ViewObject = new ViewObject(6, 'PartnerRegistrationNumber', '$Views_LawPartners_PartnerRegistrationNumber');

  /**
   * ID:7
   * Alias: PartnerAddress
   * Caption: $Views_LawPartners_PartnerAddress.
   */
   readonly ColumnPartnerAddress: ViewObject = new ViewObject(7, 'PartnerAddress', '$Views_LawPartners_PartnerAddress');

  /**
   * ID:8
   * Alias: PartnerAddressID.
   */
   readonly ColumnPartnerAddressID: ViewObject = new ViewObject(8, 'PartnerAddressID');

  /**
   * ID:9
   * Alias: PartnerContacts
   * Caption: $Views_LawPartners_PartnerContacts.
   */
   readonly ColumnPartnerContacts: ViewObject = new ViewObject(9, 'PartnerContacts', '$Views_LawPartners_PartnerContacts');

  /**
   * ID:10
   * Alias: PartnerStreet.
   */
   readonly ColumnPartnerStreet: ViewObject = new ViewObject(10, 'PartnerStreet');

  /**
   * ID:11
   * Alias: PartnerPostalCode.
   */
   readonly ColumnPartnerPostalCode: ViewObject = new ViewObject(11, 'PartnerPostalCode');

  /**
   * ID:12
   * Alias: PartnerCity.
   */
   readonly ColumnPartnerCity: ViewObject = new ViewObject(12, 'PartnerCity');

  /**
   * ID:13
   * Alias: PartnerCountry.
   */
   readonly ColumnPartnerCountry: ViewObject = new ViewObject(13, 'PartnerCountry');

  /**
   * ID:14
   * Alias: PartnerPoBox.
   */
   readonly ColumnPartnerPoBox: ViewObject = new ViewObject(14, 'PartnerPoBox');

  /**
   * ID:15
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(15, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_LawPartners_PartnerName.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_LawPartners_PartnerName');

  /**
   * ID:1
   * Alias: TaxNumber
   * Caption: $Views_LawPartners_PartnerTaxNumber.
   */
   readonly ParamTaxNumber: ViewObject = new ViewObject(1, 'TaxNumber', '$Views_LawPartners_PartnerTaxNumber');

  /**
   * ID:2
   * Alias: RegistrationNumber
   * Caption: $Views_LawPartners_PartnerRegistrationNumber.
   */
   readonly ParamRegistrationNumber: ViewObject = new ViewObject(2, 'RegistrationNumber', '$Views_LawPartners_PartnerRegistrationNumber');

  /**
   * ID:3
   * Alias: Contacts
   * Caption: $Views_LawPartners_PartnerContacts.
   */
   readonly ParamContacts: ViewObject = new ViewObject(3, 'Contacts', '$Views_LawPartners_PartnerContacts');

  /**
   * ID:4
   * Alias: EntityKind
   * Caption: $Views_LawPartners_PartnerKindName.
   */
   readonly ParamEntityKind: ViewObject = new ViewObject(4, 'EntityKind', '$Views_LawPartners_PartnerKindName');

  /**
   * ID:5
   * Alias: Street
   * Caption: $Views_LawPartners_Street.
   */
   readonly ParamStreet: ViewObject = new ViewObject(5, 'Street', '$Views_LawPartners_Street');

  /**
   * ID:6
   * Alias: PostOffice
   * Caption: $Views_LawPartners_PostOffice.
   */
   readonly ParamPostOffice: ViewObject = new ViewObject(6, 'PostOffice', '$Views_LawPartners_PostOffice');

  /**
   * ID:7
   * Alias: Region
   * Caption: $Views_LawPartners_Region.
   */
   readonly ParamRegion: ViewObject = new ViewObject(7, 'Region', '$Views_LawPartners_Region');

  /**
   * ID:8
   * Alias: Country
   * Caption: $Views_LawPartners_Country.
   */
   readonly ParamCountry: ViewObject = new ViewObject(8, 'Country', '$Views_LawPartners_Country');

  //#endregion
}

//#endregion

//#region LawStoreLocations

/**
 * ID: {0604e158-4700-40e5-9a1a-b3e766262f6a}
 * Alias: LawStoreLocations
 * Caption: $Views_Names_LawStoreLocations
 * Group: LawDictionary
 */
class LawStoreLocationsViewInfo {
  //#region Common

  /**
   * View identifier for "LawStoreLocations": {0604e158-4700-40e5-9a1a-b3e766262f6a}.
   */
   readonly ID: guid = '0604e158-4700-40e5-9a1a-b3e766262f6a';

  /**
   * View name for "LawStoreLocations".
   */
   readonly Alias: string = 'LawStoreLocations';

  /**
   * View caption for "LawStoreLocations".
   */
   readonly Caption: string = '$Views_Names_LawStoreLocations';

  /**
   * View group for "LawStoreLocations".
   */
   readonly Group: string = 'LawDictionary';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: LocationID.
   */
   readonly ColumnLocationID: ViewObject = new ViewObject(0, 'LocationID');

  /**
   * ID:1
   * Alias: LocationName
   * Caption: $Views_LawStoreLocations_LocationName.
   */
   readonly ColumnLocationName: ViewObject = new ViewObject(1, 'LocationName', '$Views_LawStoreLocations_LocationName');

  /**
   * ID:2
   * Alias: LocationByDefault
   * Caption: $Views_LawStoreLocations_LocationByDefault.
   */
   readonly ColumnLocationByDefault: ViewObject = new ViewObject(2, 'LocationByDefault', '$Views_LawStoreLocations_LocationByDefault');

  /**
   * ID:3
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(3, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_LawStoreLocations_LocationName.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_LawStoreLocations_LocationName');

  /**
   * ID:1
   * Alias: ByDefault
   * Caption: $Views_LawStoreLocations_LocationByDefault.
   */
   readonly ParamByDefault: ViewObject = new ViewObject(1, 'ByDefault', '$Views_LawStoreLocations_LocationByDefault');

  //#endregion
}

//#endregion

//#region LawUsers

/**
 * ID: {04b79978-1ad2-4ecd-9326-34d259d6ea34}
 * Alias: LawUsers
 * Caption: $Views_Names_LawUsers
 * Group: LawDictionary
 */
class LawUsersViewInfo {
  //#region Common

  /**
   * View identifier for "LawUsers": {04b79978-1ad2-4ecd-9326-34d259d6ea34}.
   */
   readonly ID: guid = '04b79978-1ad2-4ecd-9326-34d259d6ea34';

  /**
   * View name for "LawUsers".
   */
   readonly Alias: string = 'LawUsers';

  /**
   * View caption for "LawUsers".
   */
   readonly Caption: string = '$Views_Names_LawUsers';

  /**
   * View group for "LawUsers".
   */
   readonly Group: string = 'LawDictionary';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(0, 'UserID');

  /**
   * ID:1
   * Alias: UserSurname
   * Caption: $Views_LawUsers_UserSurname.
   */
   readonly ColumnUserSurname: ViewObject = new ViewObject(1, 'UserSurname', '$Views_LawUsers_UserSurname');

  /**
   * ID:2
   * Alias: UserName
   * Caption: $Views_LawUsers_UserName.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(2, 'UserName', '$Views_LawUsers_UserName');

  /**
   * ID:3
   * Alias: UserWorkplace
   * Caption: $Views_LawUsers_UserWorkplace.
   */
   readonly ColumnUserWorkplace: ViewObject = new ViewObject(3, 'UserWorkplace', '$Views_LawUsers_UserWorkplace');

  /**
   * ID:4
   * Alias: UserUserName
   * Caption: $Views_LawUsers_UserUserName.
   */
   readonly ColumnUserUserName: ViewObject = new ViewObject(4, 'UserUserName', '$Views_LawUsers_UserUserName');

  /**
   * ID:5
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(5, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Surname
   * Caption: $Views_LawUsers_UserSurname.
   */
   readonly ParamSurname: ViewObject = new ViewObject(0, 'Surname', '$Views_LawUsers_UserSurname');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_LawUsers_UserName.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_LawUsers_UserName');

  //#endregion
}

//#endregion

//#region LicenseTypes

/**
 * ID: {613dd133-9ac2-4cae-851f-c417fe657ec4}
 * Alias: LicenseTypes
 * Caption: $Views_Names_LicenseTypes
 * Group: System
 */
class LicenseTypesViewInfo {
  //#region Common

  /**
   * View identifier for "LicenseTypes": {613dd133-9ac2-4cae-851f-c417fe657ec4}.
   */
   readonly ID: guid = '613dd133-9ac2-4cae-851f-c417fe657ec4';

  /**
   * View name for "LicenseTypes".
   */
   readonly Alias: string = 'LicenseTypes';

  /**
   * View caption for "LicenseTypes".
   */
   readonly Caption: string = '$Views_Names_LicenseTypes';

  /**
   * View group for "LicenseTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: $Views_LicenseTypes_Name.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', '$Views_LicenseTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_LicenseTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_LicenseTypes_Name_Param');

  //#endregion
}

//#endregion

//#region LinkedDocuments

/**
 * ID: {88069520-441a-4f38-a103-c8f2ac8a2101}
 * Alias: LinkedDocuments
 * Caption: $Views_Names_LinkedDocuments
 * Group: KrDocuments
 */
class LinkedDocumentsViewInfo {
  //#region Common

  /**
   * View identifier for "LinkedDocuments": {88069520-441a-4f38-a103-c8f2ac8a2101}.
   */
   readonly ID: guid = '88069520-441a-4f38-a103-c8f2ac8a2101';

  /**
   * View name for "LinkedDocuments".
   */
   readonly Alias: string = 'LinkedDocuments';

  /**
   * View caption for "LinkedDocuments".
   */
   readonly Caption: string = '$Views_Names_LinkedDocuments';

  /**
   * View group for "LinkedDocuments".
   */
   readonly Group: string = 'KrDocuments';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DocID.
   */
   readonly ColumnDocID: ViewObject = new ViewObject(0, 'DocID');

  /**
   * ID:1
   * Alias: DocNumber
   * Caption: $Views_Registers_Number.
   */
   readonly ColumnDocNumber: ViewObject = new ViewObject(1, 'DocNumber', '$Views_Registers_Number');

  /**
   * ID:2
   * Alias: TypeName
   * Caption: $Views_Registers_Type.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(2, 'TypeName', '$Views_Registers_Type');

  /**
   * ID:3
   * Alias: DocSubject
   * Caption: $Views_Registers_Subject.
   */
   readonly ColumnDocSubject: ViewObject = new ViewObject(3, 'DocSubject', '$Views_Registers_Subject');

  /**
   * ID:4
   * Alias: DocDescription
   * Caption: $Views_Registers_DocDescription.
   */
   readonly ColumnDocDescription: ViewObject = new ViewObject(4, 'DocDescription', '$Views_Registers_DocDescription');

  /**
   * ID:5
   * Alias: PartnerID.
   */
   readonly ColumnPartnerID: ViewObject = new ViewObject(5, 'PartnerID');

  /**
   * ID:6
   * Alias: PartnerName
   * Caption: $Views_Registers_Partner.
   */
   readonly ColumnPartnerName: ViewObject = new ViewObject(6, 'PartnerName', '$Views_Registers_Partner');

  /**
   * ID:7
   * Alias: AuthorID.
   */
   readonly ColumnAuthorID: ViewObject = new ViewObject(7, 'AuthorID');

  /**
   * ID:8
   * Alias: AuthorName
   * Caption: $Views_Registers_Author.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(8, 'AuthorName', '$Views_Registers_Author');

  /**
   * ID:9
   * Alias: RegistratorID.
   */
   readonly ColumnRegistratorID: ViewObject = new ViewObject(9, 'RegistratorID');

  /**
   * ID:10
   * Alias: RegistratorName
   * Caption: $Views_Registers_Registrator.
   */
   readonly ColumnRegistratorName: ViewObject = new ViewObject(10, 'RegistratorName', '$Views_Registers_Registrator');

  /**
   * ID:11
   * Alias: DocDate
   * Caption: $Views_Registers_DocDate.
   */
   readonly ColumnDocDate: ViewObject = new ViewObject(11, 'DocDate', '$Views_Registers_DocDate');

  /**
   * ID:12
   * Alias: CreationDate
   * Caption: $Views_Registers_CreationDate.
   */
   readonly ColumnCreationDate: ViewObject = new ViewObject(12, 'CreationDate', '$Views_Registers_CreationDate');

  /**
   * ID:13
   * Alias: Department
   * Caption: $Views_Registers_Department.
   */
   readonly ColumnDepartment: ViewObject = new ViewObject(13, 'Department', '$Views_Registers_Department');

  /**
   * ID:14
   * Alias: KrState
   * Caption: $Views_Registers_State.
   */
   readonly ColumnKrState: ViewObject = new ViewObject(14, 'KrState', '$Views_Registers_State');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Partner
   * Caption: $Views_Registers_Partner_Param.
   */
   readonly ParamPartner: ViewObject = new ViewObject(0, 'Partner', '$Views_Registers_Partner_Param');

  /**
   * ID:1
   * Alias: Number
   * Caption: $Views_Registers_Number_Param.
   */
   readonly ParamNumber: ViewObject = new ViewObject(1, 'Number', '$Views_Registers_Number_Param');

  /**
   * ID:2
   * Alias: Subject
   * Caption: $Views_Registers_Subject_Param.
   */
   readonly ParamSubject: ViewObject = new ViewObject(2, 'Subject', '$Views_Registers_Subject_Param');

  /**
   * ID:3
   * Alias: DocDate
   * Caption: $Views_Registers_DocDate_Param.
   */
   readonly ParamDocDate: ViewObject = new ViewObject(3, 'DocDate', '$Views_Registers_DocDate_Param');

  /**
   * ID:4
   * Alias: Author
   * Caption: $Views_Registers_Author_Param.
   */
   readonly ParamAuthor: ViewObject = new ViewObject(4, 'Author', '$Views_Registers_Author_Param');

  /**
   * ID:5
   * Alias: Registrator
   * Caption: $Views_Registers_Registrator_Param.
   */
   readonly ParamRegistrator: ViewObject = new ViewObject(5, 'Registrator', '$Views_Registers_Registrator_Param');

  /**
   * ID:6
   * Alias: State
   * Caption: $Views_Registers_State_Param.
   */
   readonly ParamState: ViewObject = new ViewObject(6, 'State', '$Views_Registers_State_Param');

  /**
   * ID:7
   * Alias: Type
   * Caption: $Views_Registers_Type_Param.
   */
   readonly ParamType: ViewObject = new ViewObject(7, 'Type', '$Views_Registers_Type_Param');

  /**
   * ID:8
   * Alias: Department
   * Caption: $Views_Registers_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(8, 'Department', '$Views_Registers_Department_Param');

  /**
   * ID:9
   * Alias: CreationDate
   * Caption: $Views_Registers_CreationDate_Param.
   */
   readonly ParamCreationDate: ViewObject = new ViewObject(9, 'CreationDate', '$Views_Registers_CreationDate_Param');

  /**
   * ID:10
   * Alias: LinkedDocID
   * Caption: LinkedDocID.
   */
   readonly ParamLinkedDocID: ViewObject = new ViewObject(10, 'LinkedDocID', 'LinkedDocID');

  //#endregion
}

//#endregion

//#region LoginTypes

/**
 * ID: {b0afaa90-23f1-4c2d-a85d-54506e027745}
 * Alias: LoginTypes
 * Caption: $Views_Names_LoginTypes
 * Group: System
 */
class LoginTypesViewInfo {
  //#region Common

  /**
   * View identifier for "LoginTypes": {b0afaa90-23f1-4c2d-a85d-54506e027745}.
   */
   readonly ID: guid = 'b0afaa90-23f1-4c2d-a85d-54506e027745';

  /**
   * View name for "LoginTypes".
   */
   readonly Alias: string = 'LoginTypes';

  /**
   * View caption for "LoginTypes".
   */
   readonly Caption: string = '$Views_Names_LoginTypes';

  /**
   * View group for "LoginTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: $Views_LoginTypes_Name.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', '$Views_LoginTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_LoginTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_LoginTypes_Name_Param');

  //#endregion
}

//#endregion

//#region MyAcquaintanceHistory

/**
 * ID: {ef66bde2-1126-4c09-9f4e-56d710ccda40}
 * Alias: MyAcquaintanceHistory
 * Caption: $Views_Names_MyAcquaintanceHistory
 * Group: Acquaintance
 */
class MyAcquaintanceHistoryViewInfo {
  //#region Common

  /**
   * View identifier for "MyAcquaintanceHistory": {ef66bde2-1126-4c09-9f4e-56d710ccda40}.
   */
   readonly ID: guid = 'ef66bde2-1126-4c09-9f4e-56d710ccda40';

  /**
   * View name for "MyAcquaintanceHistory".
   */
   readonly Alias: string = 'MyAcquaintanceHistory';

  /**
   * View caption for "MyAcquaintanceHistory".
   */
   readonly Caption: string = '$Views_Names_MyAcquaintanceHistory';

  /**
   * View group for "MyAcquaintanceHistory".
   */
   readonly Group: string = 'Acquaintance';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ID.
   */
   readonly ColumnID: ViewObject = new ViewObject(0, 'ID');

  /**
   * ID:1
   * Alias: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(1, 'CardID');

  /**
   * ID:2
   * Alias: CardNumber
   * Caption: $Views_Registers_Number.
   */
   readonly ColumnCardNumber: ViewObject = new ViewObject(2, 'CardNumber', '$Views_Registers_Number');

  /**
   * ID:3
   * Alias: SenderID.
   */
   readonly ColumnSenderID: ViewObject = new ViewObject(3, 'SenderID');

  /**
   * ID:4
   * Alias: SenderName
   * Caption: $Views_Acquaintance_Sender.
   */
   readonly ColumnSenderName: ViewObject = new ViewObject(4, 'SenderName', '$Views_Acquaintance_Sender');

  /**
   * ID:5
   * Alias: Sent
   * Caption: $Views_Acquaintance_SentDate.
   */
   readonly ColumnSent: ViewObject = new ViewObject(5, 'Sent', '$Views_Acquaintance_SentDate');

  /**
   * ID:6
   * Alias: IsReceived.
   */
   readonly ColumnIsReceived: ViewObject = new ViewObject(6, 'IsReceived');

  /**
   * ID:7
   * Alias: IsReceivedString
   * Caption: $Views_Acquaintance_State.
   */
   readonly ColumnIsReceivedString: ViewObject = new ViewObject(7, 'IsReceivedString', '$Views_Acquaintance_State');

  /**
   * ID:8
   * Alias: Received
   * Caption: $Views_Acquaintance_ReceivedDate.
   */
   readonly ColumnReceived: ViewObject = new ViewObject(8, 'Received', '$Views_Acquaintance_ReceivedDate');

  /**
   * ID:9
   * Alias: Comment
   * Caption: $Views_Acquaintance_CommentColumn.
   */
   readonly ColumnComment: ViewObject = new ViewObject(9, 'Comment', '$Views_Acquaintance_CommentColumn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: SenderParam
   * Caption: $Views_Acquaintance_Sender.
   */
   readonly ParamSenderParam: ViewObject = new ViewObject(0, 'SenderParam', '$Views_Acquaintance_Sender');

  /**
   * ID:1
   * Alias: IsReceivedParam
   * Caption: $Views_Acquaintance_State.
   */
   readonly ParamIsReceivedParam: ViewObject = new ViewObject(1, 'IsReceivedParam', '$Views_Acquaintance_State');

  /**
   * ID:2
   * Alias: SentParam
   * Caption: $Views_Acquaintance_SentDate.
   */
   readonly ParamSentParam: ViewObject = new ViewObject(2, 'SentParam', '$Views_Acquaintance_SentDate');

  /**
   * ID:3
   * Alias: ReceivedParam
   * Caption: $Views_Acquaintance_ReceivedDate.
   */
   readonly ParamReceivedParam: ViewObject = new ViewObject(3, 'ReceivedParam', '$Views_Acquaintance_ReceivedDate');

  /**
   * ID:4
   * Alias: CommentParam
   * Caption: $Views_Acquaintance_CommentParam.
   */
   readonly ParamCommentParam: ViewObject = new ViewObject(4, 'CommentParam', '$Views_Acquaintance_CommentParam');

  //#endregion
}

//#endregion

//#region MyCompletedTasks

/**
 * ID: {89cf35b0-69bc-406a-9b95-77be879a94fa}
 * Alias: MyCompletedTasks
 * Caption: $Views_Names_MyCompletedTasks
 * Group: System
 */
class MyCompletedTasksViewInfo {
  //#region Common

  /**
   * View identifier for "MyCompletedTasks": {89cf35b0-69bc-406a-9b95-77be879a94fa}.
   */
   readonly ID: guid = '89cf35b0-69bc-406a-9b95-77be879a94fa';

  /**
   * View name for "MyCompletedTasks".
   */
   readonly Alias: string = 'MyCompletedTasks';

  /**
   * View caption for "MyCompletedTasks".
   */
   readonly Caption: string = '$Views_Names_MyCompletedTasks';

  /**
   * View group for "MyCompletedTasks".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(0, 'CardID');

  /**
   * ID:1
   * Alias: CardName
   * Caption: $Views_CompletedTasks_Card.
   */
   readonly ColumnCardName: ViewObject = new ViewObject(1, 'CardName', '$Views_CompletedTasks_Card');

  /**
   * ID:2
   * Alias: CardTypeCaption
   * Caption: $Views_CompletedTasks_CardType.
   */
   readonly ColumnCardTypeCaption: ViewObject = new ViewObject(2, 'CardTypeCaption', '$Views_CompletedTasks_CardType');

  /**
   * ID:3
   * Alias: Subject
   * Caption: $Views_Registers_Subject.
   */
   readonly ColumnSubject: ViewObject = new ViewObject(3, 'Subject', '$Views_Registers_Subject');

  /**
   * ID:4
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(4, 'TypeID');

  /**
   * ID:5
   * Alias: TypeCaption
   * Caption: $Views_CompletedTasks_TaskType.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(5, 'TypeCaption', '$Views_CompletedTasks_TaskType');

  /**
   * ID:6
   * Alias: OptionID.
   */
   readonly ColumnOptionID: ViewObject = new ViewObject(6, 'OptionID');

  /**
   * ID:7
   * Alias: OptionCaption
   * Caption: $Views_CompletedTasks_CompletionOption.
   */
   readonly ColumnOptionCaption: ViewObject = new ViewObject(7, 'OptionCaption', '$Views_CompletedTasks_CompletionOption');

  /**
   * ID:8
   * Alias: Result
   * Caption: $Views_CompletedTasks_Result.
   */
   readonly ColumnResult: ViewObject = new ViewObject(8, 'Result', '$Views_CompletedTasks_Result');

  /**
   * ID:9
   * Alias: RoleID.
   */
   readonly ColumnRoleID: ViewObject = new ViewObject(9, 'RoleID');

  /**
   * ID:10
   * Alias: RoleName
   * Caption: $Views_CompletedTasks_Role.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(10, 'RoleName', '$Views_CompletedTasks_Role');

  /**
   * ID:11
   * Alias: AuthorID.
   */
   readonly ColumnAuthorID: ViewObject = new ViewObject(11, 'AuthorID');

  /**
   * ID:12
   * Alias: AuthorName
   * Caption: $Views_CompletedTasks_Author.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(12, 'AuthorName', '$Views_CompletedTasks_Author');

  /**
   * ID:13
   * Alias: Completed
   * Caption: $Views_CompletedTasks_Completed.
   */
   readonly ColumnCompleted: ViewObject = new ViewObject(13, 'Completed', '$Views_CompletedTasks_Completed');

  /**
   * ID:14
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(14, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: FromMeMode
   * Caption: $Views_CompletedTasks_FromMeMode_Param.
   */
   readonly ParamFromMeMode: ViewObject = new ViewObject(0, 'FromMeMode', '$Views_CompletedTasks_FromMeMode_Param');

  /**
   * ID:1
   * Alias: CompletionDate
   * Caption: $Views_CompletedTasks_CompletionDate_Param.
   */
   readonly ParamCompletionDate: ViewObject = new ViewObject(1, 'CompletionDate', '$Views_CompletedTasks_CompletionDate_Param');

  /**
   * ID:2
   * Alias: TypeParam
   * Caption: $Views_CompletedTasks_CardType_Param.
   */
   readonly ParamTypeParam: ViewObject = new ViewObject(2, 'TypeParam', '$Views_CompletedTasks_CardType_Param');

  /**
   * ID:3
   * Alias: TaskType
   * Caption: $Views_CompletedTasks_TaskType_Param.
   */
   readonly ParamTaskType: ViewObject = new ViewObject(3, 'TaskType', '$Views_CompletedTasks_TaskType_Param');

  /**
   * ID:4
   * Alias: Role
   * Caption: $Views_CompletedTasks_RoleGroup_Param.
   */
   readonly ParamRole: ViewObject = new ViewObject(4, 'Role', '$Views_CompletedTasks_RoleGroup_Param');

  /**
   * ID:5
   * Alias: Option
   * Caption: $Views_CompletedTasks_CompletionOption_Param.
   */
   readonly ParamOption: ViewObject = new ViewObject(5, 'Option', '$Views_CompletedTasks_CompletionOption_Param');

  //#endregion
}

//#endregion

//#region MyDocuments

/**
 * ID: {198282c6-a340-472f-97fb-e8895c3cc3ca}
 * Alias: MyDocuments
 * Caption: $Views_Names_MyDocuments
 * Group: KrDocuments
 */
class MyDocumentsViewInfo {
  //#region Common

  /**
   * View identifier for "MyDocuments": {198282c6-a340-472f-97fb-e8895c3cc3ca}.
   */
   readonly ID: guid = '198282c6-a340-472f-97fb-e8895c3cc3ca';

  /**
   * View name for "MyDocuments".
   */
   readonly Alias: string = 'MyDocuments';

  /**
   * View caption for "MyDocuments".
   */
   readonly Caption: string = '$Views_Names_MyDocuments';

  /**
   * View group for "MyDocuments".
   */
   readonly Group: string = 'KrDocuments';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DocID.
   */
   readonly ColumnDocID: ViewObject = new ViewObject(0, 'DocID');

  /**
   * ID:1
   * Alias: DocNumber
   * Caption: $Views_Registers_Number.
   */
   readonly ColumnDocNumber: ViewObject = new ViewObject(1, 'DocNumber', '$Views_Registers_Number');

  /**
   * ID:2
   * Alias: DocSubject
   * Caption: $Views_Registers_Subject.
   */
   readonly ColumnDocSubject: ViewObject = new ViewObject(2, 'DocSubject', '$Views_Registers_Subject');

  /**
   * ID:3
   * Alias: DocDescription
   * Caption: $Views_Registers_DocDescription.
   */
   readonly ColumnDocDescription: ViewObject = new ViewObject(3, 'DocDescription', '$Views_Registers_DocDescription');

  /**
   * ID:4
   * Alias: AuthorID.
   */
   readonly ColumnAuthorID: ViewObject = new ViewObject(4, 'AuthorID');

  /**
   * ID:5
   * Alias: AuthorName
   * Caption: $Views_Registers_Author.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(5, 'AuthorName', '$Views_Registers_Author');

  /**
   * ID:6
   * Alias: RegistratorID.
   */
   readonly ColumnRegistratorID: ViewObject = new ViewObject(6, 'RegistratorID');

  /**
   * ID:7
   * Alias: RegistratorName
   * Caption: $Views_Registers_Registrator.
   */
   readonly ColumnRegistratorName: ViewObject = new ViewObject(7, 'RegistratorName', '$Views_Registers_Registrator');

  /**
   * ID:8
   * Alias: KrState
   * Caption: $Views_Registers_State.
   */
   readonly ColumnKrState: ViewObject = new ViewObject(8, 'KrState', '$Views_Registers_State');

  /**
   * ID:9
   * Alias: KrStateModified
   * Caption: $Views_Registers_StateModified.
   */
   readonly ColumnKrStateModified: ViewObject = new ViewObject(9, 'KrStateModified', '$Views_Registers_StateModified');

  /**
   * ID:10
   * Alias: DocDate
   * Caption: $Views_Registers_DocDate.
   */
   readonly ColumnDocDate: ViewObject = new ViewObject(10, 'DocDate', '$Views_Registers_DocDate');

  /**
   * ID:11
   * Alias: CreationDate
   * Caption: $Views_Registers_CreationDate.
   */
   readonly ColumnCreationDate: ViewObject = new ViewObject(11, 'CreationDate', '$Views_Registers_CreationDate');

  /**
   * ID:12
   * Alias: Department
   * Caption: $Views_Registers_Department.
   */
   readonly ColumnDepartment: ViewObject = new ViewObject(12, 'Department', '$Views_Registers_Department');

  /**
   * ID:13
   * Alias: ApprovedBy
   * Caption: $Views_Registers_ApprovedBy.
   */
   readonly ColumnApprovedBy: ViewObject = new ViewObject(13, 'ApprovedBy', '$Views_Registers_ApprovedBy');

  /**
   * ID:14
   * Alias: DisapprovedBy
   * Caption: $Views_Registers_DisapprovedBy.
   */
   readonly ColumnDisapprovedBy: ViewObject = new ViewObject(14, 'DisapprovedBy', '$Views_Registers_DisapprovedBy');

  /**
   * ID:15
   * Alias: Background.
   */
   readonly ColumnBackground: ViewObject = new ViewObject(15, 'Background');

  /**
   * ID:16
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(16, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: IsAuthor
   * Caption: $Views_Registers_IsAuthor_Param.
   */
   readonly ParamIsAuthor: ViewObject = new ViewObject(0, 'IsAuthor', '$Views_Registers_IsAuthor_Param');

  /**
   * ID:1
   * Alias: IsInitiator
   * Caption: $Views_Registers_IsInitiator_Param.
   */
   readonly ParamIsInitiator: ViewObject = new ViewObject(1, 'IsInitiator', '$Views_Registers_IsInitiator_Param');

  /**
   * ID:2
   * Alias: IsRegistrator
   * Caption: $Views_Registers_IsRegistrator_Param.
   */
   readonly ParamIsRegistrator: ViewObject = new ViewObject(2, 'IsRegistrator', '$Views_Registers_IsRegistrator_Param');

  /**
   * ID:3
   * Alias: Number
   * Caption: $Views_Registers_Number_Param.
   */
   readonly ParamNumber: ViewObject = new ViewObject(3, 'Number', '$Views_Registers_Number_Param');

  /**
   * ID:4
   * Alias: Subject
   * Caption: $Views_Registers_Subject_Param.
   */
   readonly ParamSubject: ViewObject = new ViewObject(4, 'Subject', '$Views_Registers_Subject_Param');

  /**
   * ID:5
   * Alias: DocDate
   * Caption: $Views_Registers_DocDate_Param.
   */
   readonly ParamDocDate: ViewObject = new ViewObject(5, 'DocDate', '$Views_Registers_DocDate_Param');

  /**
   * ID:6
   * Alias: Author
   * Caption: $Views_Registers_Author_Param.
   */
   readonly ParamAuthor: ViewObject = new ViewObject(6, 'Author', '$Views_Registers_Author_Param');

  /**
   * ID:7
   * Alias: Registrator
   * Caption: $Views_Registers_Registrator_Param.
   */
   readonly ParamRegistrator: ViewObject = new ViewObject(7, 'Registrator', '$Views_Registers_Registrator_Param');

  /**
   * ID:8
   * Alias: State
   * Caption: $Views_Registers_State_Param.
   */
   readonly ParamState: ViewObject = new ViewObject(8, 'State', '$Views_Registers_State_Param');

  /**
   * ID:9
   * Alias: Type
   * Caption: $Views_Registers_Type_Param.
   */
   readonly ParamType: ViewObject = new ViewObject(9, 'Type', '$Views_Registers_Type_Param');

  /**
   * ID:10
   * Alias: Department
   * Caption: $Views_Registers_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(10, 'Department', '$Views_Registers_Department_Param');

  //#endregion
}

//#endregion

//#region MyTags

/**
 * ID: {b206c96c-de91-4bc5-aa55-d00bf7eb9604}
 * Alias: MyTags
 * Caption: $Views_Names_MyTags
 * Group: Tags
 */
class MyTagsViewInfo {
  //#region Common

  /**
   * View identifier for "MyTags": {b206c96c-de91-4bc5-aa55-d00bf7eb9604}.
   */
   readonly ID: guid = 'b206c96c-de91-4bc5-aa55-d00bf7eb9604';

  /**
   * View name for "MyTags".
   */
   readonly Alias: string = 'MyTags';

  /**
   * View caption for "MyTags".
   */
   readonly Caption: string = '$Views_Names_MyTags';

  /**
   * View group for "MyTags".
   */
   readonly Group: string = 'Tags';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TagID.
   */
   readonly ColumnTagID: ViewObject = new ViewObject(0, 'TagID');

  /**
   * ID:1
   * Alias: TagName
   * Caption: $Views_Tags_Name.
   */
   readonly ColumnTagName: ViewObject = new ViewObject(1, 'TagName', '$Views_Tags_Name');

  /**
   * ID:2
   * Alias: TagBackground
   * Caption: $CardTypes_Controls_Background.
   */
   readonly ColumnTagBackground: ViewObject = new ViewObject(2, 'TagBackground', '$CardTypes_Controls_Background');

  /**
   * ID:3
   * Alias: TagForeground
   * Caption: $CardTypes_Controls_Foreground.
   */
   readonly ColumnTagForeground: ViewObject = new ViewObject(3, 'TagForeground', '$CardTypes_Controls_Foreground');

  /**
   * ID:4
   * Alias: TagIsCommon
   * Caption: $Tags_IsCommon.
   */
   readonly ColumnTagIsCommon: ViewObject = new ViewObject(4, 'TagIsCommon', '$Tags_IsCommon');

  /**
   * ID:5
   * Alias: TagCanEdit.
   */
   readonly ColumnTagCanEdit: ViewObject = new ViewObject(5, 'TagCanEdit');

  /**
   * ID:6
   * Alias: TagCanUse.
   */
   readonly ColumnTagCanUse: ViewObject = new ViewObject(6, 'TagCanUse');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Tags_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Tags_Name_Param');

  //#endregion
}

//#endregion

//#region MyTasks

/**
 * ID: {d249d321-c5a3-4847-951a-f47ffcf5509d}
 * Alias: MyTasks
 * Caption: $Views_Names_MyTasks
 * Group: System
 */
class MyTasksViewInfo {
  //#region Common

  /**
   * View identifier for "MyTasks": {d249d321-c5a3-4847-951a-f47ffcf5509d}.
   */
   readonly ID: guid = 'd249d321-c5a3-4847-951a-f47ffcf5509d';

  /**
   * View name for "MyTasks".
   */
   readonly Alias: string = 'MyTasks';

  /**
   * View caption for "MyTasks".
   */
   readonly Caption: string = '$Views_Names_MyTasks';

  /**
   * View group for "MyTasks".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(0, 'CardID');

  /**
   * ID:1
   * Alias: TaskRowID.
   */
   readonly ColumnTaskRowID: ViewObject = new ViewObject(1, 'TaskRowID');

  /**
   * ID:2
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(2, 'rn');

  /**
   * ID:3
   * Alias: StateID.
   */
   readonly ColumnStateID: ViewObject = new ViewObject(3, 'StateID');

  /**
   * ID:4
   * Alias: StateName
   * Caption: $Views_MyTasks_State.
   */
   readonly ColumnStateName: ViewObject = new ViewObject(4, 'StateName', '$Views_MyTasks_State');

  /**
   * ID:5
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(5, 'TypeID');

  /**
   * ID:6
   * Alias: PlannedDate
   * Caption: $Views_MyTasks_Planned.
   */
   readonly ColumnPlannedDate: ViewObject = new ViewObject(6, 'PlannedDate', '$Views_MyTasks_Planned');

  /**
   * ID:7
   * Alias: TaskInfo
   * Caption: $Views_MyTasks_Info.
   */
   readonly ColumnTaskInfo: ViewObject = new ViewObject(7, 'TaskInfo', '$Views_MyTasks_Info');

  /**
   * ID:8
   * Alias: RoleID.
   */
   readonly ColumnRoleID: ViewObject = new ViewObject(8, 'RoleID');

  /**
   * ID:9
   * Alias: RoleName
   * Caption: $Views_MyTasks_Performer.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(9, 'RoleName', '$Views_MyTasks_Performer');

  /**
   * ID:10
   * Alias: AuthorID.
   */
   readonly ColumnAuthorID: ViewObject = new ViewObject(10, 'AuthorID');

  /**
   * ID:11
   * Alias: AuthorName
   * Caption: $Views_MyTasks_Author.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(11, 'AuthorName', '$Views_MyTasks_Author');

  /**
   * ID:12
   * Alias: AuthorDeptID.
   */
   readonly ColumnAuthorDeptID: ViewObject = new ViewObject(12, 'AuthorDeptID');

  /**
   * ID:13
   * Alias: AuthorDeptName
   * Caption: $Views_MyTasks_AuthorDepartment.
   */
   readonly ColumnAuthorDeptName: ViewObject = new ViewObject(13, 'AuthorDeptName', '$Views_MyTasks_AuthorDepartment');

  /**
   * ID:14
   * Alias: ModificationTime
   * Caption: $Views_MyTasks_Modified.
   */
   readonly ColumnModificationTime: ViewObject = new ViewObject(14, 'ModificationTime', '$Views_MyTasks_Modified');

  /**
   * ID:15
   * Alias: Created.
   */
   readonly ColumnCreated: ViewObject = new ViewObject(15, 'Created');

  /**
   * ID:16
   * Alias: CreatedByID.
   */
   readonly ColumnCreatedByID: ViewObject = new ViewObject(16, 'CreatedByID');

  /**
   * ID:17
   * Alias: CreatedByName.
   */
   readonly ColumnCreatedByName: ViewObject = new ViewObject(17, 'CreatedByName');

  /**
   * ID:18
   * Alias: TimeZoneUtcOffsetMinutes.
   */
   readonly ColumnTimeZoneUtcOffsetMinutes: ViewObject = new ViewObject(18, 'TimeZoneUtcOffsetMinutes');

  /**
   * ID:19
   * Alias: RoleTypeID.
   */
   readonly ColumnRoleTypeID: ViewObject = new ViewObject(19, 'RoleTypeID');

  /**
   * ID:20
   * Alias: CardName
   * Caption: $Views_MyTasks_Card.
   */
   readonly ColumnCardName: ViewObject = new ViewObject(20, 'CardName', '$Views_MyTasks_Card');

  /**
   * ID:21
   * Alias: CardTypeID.
   */
   readonly ColumnCardTypeID: ViewObject = new ViewObject(21, 'CardTypeID');

  /**
   * ID:22
   * Alias: CardTypeName
   * Caption: $Views_MyTasks_CardType.
   */
   readonly ColumnCardTypeName: ViewObject = new ViewObject(22, 'CardTypeName', '$Views_MyTasks_CardType');

  /**
   * ID:23
   * Alias: TypeCaption
   * Caption: $Views_MyTasks_TaskType.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(23, 'TypeCaption', '$Views_MyTasks_TaskType');

  /**
   * ID:24
   * Alias: TimeToCompletion
   * Caption: $Views_MyTasks_TimeToCompletion.
   */
   readonly ColumnTimeToCompletion: ViewObject = new ViewObject(24, 'TimeToCompletion', '$Views_MyTasks_TimeToCompletion');

  /**
   * ID:25
   * Alias: CalendarID.
   */
   readonly ColumnCalendarID: ViewObject = new ViewObject(25, 'CalendarID');

  /**
   * ID:26
   * Alias: QuantsToFinish.
   */
   readonly ColumnQuantsToFinish: ViewObject = new ViewObject(26, 'QuantsToFinish');

  /**
   * ID:27
   * Alias: AppearanceColumn.
   */
   readonly ColumnAppearanceColumn: ViewObject = new ViewObject(27, 'AppearanceColumn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Status
   * Caption: $Views_MyTasks_State_Param.
   */
   readonly ParamStatus: ViewObject = new ViewObject(0, 'Status', '$Views_MyTasks_State_Param');

  /**
   * ID:1
   * Alias: TaskType
   * Caption: $Views_MyTasks_TaskType_Param.
   */
   readonly ParamTaskType: ViewObject = new ViewObject(1, 'TaskType', '$Views_MyTasks_TaskType_Param');

  /**
   * ID:2
   * Alias: TaskTypeGrouped
   * Caption: $Views_MyTasks_TaskTypeGrouped_Param.
   */
   readonly ParamTaskTypeGrouped: ViewObject = new ViewObject(2, 'TaskTypeGrouped', '$Views_MyTasks_TaskTypeGrouped_Param');

  /**
   * ID:3
   * Alias: AuthorDepartment
   * Caption: $Views_MyTasks_AuthorDepartment_Param.
   */
   readonly ParamAuthorDepartment: ViewObject = new ViewObject(3, 'AuthorDepartment', '$Views_MyTasks_AuthorDepartment_Param');

  /**
   * ID:4
   * Alias: TaskDateDueInterval
   * Caption: $Views_MyTasks_TaskDateDueInterval_Param.
   */
   readonly ParamTaskDateDueInterval: ViewObject = new ViewObject(4, 'TaskDateDueInterval', '$Views_MyTasks_TaskDateDueInterval_Param');

  /**
   * ID:5
   * Alias: User
   * Caption: $Views_MyTasks_User_Param.
   */
   readonly ParamUser: ViewObject = new ViewObject(5, 'User', '$Views_MyTasks_User_Param');

  /**
   * ID:6
   * Alias: Role
   * Caption: $Views_MyTasks_Role_Param.
   */
   readonly ParamRole: ViewObject = new ViewObject(6, 'Role', '$Views_MyTasks_Role_Param');

  /**
   * ID:7
   * Alias: DeputyMode
   * Caption: $Views_MyTasks_Deputy_Param.
   */
   readonly ParamDeputyMode: ViewObject = new ViewObject(7, 'DeputyMode', '$Views_MyTasks_Deputy_Param');

  /**
   * ID:8
   * Alias: InWorkNotByMe
   * Caption: $Views_MyTasks_InWorkNotMe_Param.
   */
   readonly ParamInWorkNotByMe: ViewObject = new ViewObject(8, 'InWorkNotByMe', '$Views_MyTasks_InWorkNotMe_Param');

  /**
   * ID:9
   * Alias: CreationDate
   * Caption: $Views_ReportCurrentTasksByUser_CreationDate_Param.
   */
   readonly ParamCreationDate: ViewObject = new ViewObject(9, 'CreationDate', '$Views_ReportCurrentTasksByUser_CreationDate_Param');

  /**
   * ID:10
   * Alias: EndDate
   * Caption: $Views_CurrentTasks_EndDate_Param.
   */
   readonly ParamEndDate: ViewObject = new ViewObject(10, 'EndDate', '$Views_CurrentTasks_EndDate_Param');

  /**
   * ID:11
   * Alias: IsDelayed
   * Caption: $Views_MyTasks_IsDelayed_Param.
   */
   readonly ParamIsDelayed: ViewObject = new ViewObject(11, 'IsDelayed', '$Views_MyTasks_IsDelayed_Param');

  /**
   * ID:12
   * Alias: TypeParam
   * Caption: $Views_CurrentTasks_DocType_Param.
   */
   readonly ParamTypeParam: ViewObject = new ViewObject(12, 'TypeParam', '$Views_CurrentTasks_DocType_Param');

  /**
   * ID:13
   * Alias: FunctionRoleAuthorParam
   * Caption: $Views_MyTasks_FunctionRole_Author_Param.
   */
   readonly ParamFunctionRoleAuthorParam: ViewObject = new ViewObject(13, 'FunctionRoleAuthorParam', '$Views_MyTasks_FunctionRole_Author_Param');

  /**
   * ID:14
   * Alias: FunctionRolePerformerParam
   * Caption: $Views_MyTasks_FunctionRole_Performer_Param.
   */
   readonly ParamFunctionRolePerformerParam: ViewObject = new ViewObject(14, 'FunctionRolePerformerParam', '$Views_MyTasks_FunctionRole_Performer_Param');

  //#endregion
}

//#endregion

//#region MyTopics

/**
 * ID: {01629718-ee20-45d9-820f-188b350bcf88}
 * Alias: MyTopics
 * Caption: $Views_Names_MyTopics
 * Group: Fm
 */
class MyTopicsViewInfo {
  //#region Common

  /**
   * View identifier for "MyTopics": {01629718-ee20-45d9-820f-188b350bcf88}.
   */
   readonly ID: guid = '01629718-ee20-45d9-820f-188b350bcf88';

  /**
   * View name for "MyTopics".
   */
   readonly Alias: string = 'MyTopics';

  /**
   * View caption for "MyTopics".
   */
   readonly Caption: string = '$Views_Names_MyTopics';

  /**
   * View group for "MyTopics".
   */
   readonly Group: string = 'Fm';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(0, 'CardID');

  /**
   * ID:1
   * Alias: Created
   * Caption: $Views_MyTopics_Created.
   */
   readonly ColumnCreated: ViewObject = new ViewObject(1, 'Created', '$Views_MyTopics_Created');

  /**
   * ID:2
   * Alias: TopicID.
   */
   readonly ColumnTopicID: ViewObject = new ViewObject(2, 'TopicID');

  /**
   * ID:3
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(3, 'TypeID');

  /**
   * ID:4
   * Alias: TopicName
   * Caption: $Views_MyTopics_TopicName.
   */
   readonly ColumnTopicName: ViewObject = new ViewObject(4, 'TopicName', '$Views_MyTopics_TopicName');

  /**
   * ID:5
   * Alias: Description
   * Caption: $Views_MyTopics_Description.
   */
   readonly ColumnDescription: ViewObject = new ViewObject(5, 'Description', '$Views_MyTopics_Description');

  /**
   * ID:6
   * Alias: AuthorName
   * Caption: $Views_MyTopics_AuthorName.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(6, 'AuthorName', '$Views_MyTopics_AuthorName');

  /**
   * ID:7
   * Alias: LastMessage
   * Caption: $Views_MyTopics_LastMessage.
   */
   readonly ColumnLastMessage: ViewObject = new ViewObject(7, 'LastMessage', '$Views_MyTopics_LastMessage');

  /**
   * ID:8
   * Alias: LastMessageAuthorName
   * Caption: $Views_MyTopics_LastMessageAuthorName.
   */
   readonly ColumnLastMessageAuthorName: ViewObject = new ViewObject(8, 'LastMessageAuthorName', '$Views_MyTopics_LastMessageAuthorName');

  /**
   * ID:9
   * Alias: IsArchived.
   */
   readonly ColumnIsArchived: ViewObject = new ViewObject(9, 'IsArchived');

  /**
   * ID:10
   * Alias: AppearanceColumn.
   */
   readonly ColumnAppearanceColumn: ViewObject = new ViewObject(10, 'AppearanceColumn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Created
   * Caption: $Views_MyTopics_Created.
   */
   readonly ParamCreated: ViewObject = new ViewObject(0, 'Created', '$Views_MyTopics_Created');

  /**
   * ID:1
   * Alias: IsArchived
   * Caption: $Views_MyTopics_ShowArchived.
   */
   readonly ParamIsArchived: ViewObject = new ViewObject(1, 'IsArchived', '$Views_MyTopics_ShowArchived');

  //#endregion
}

//#endregion

//#region Notifications

/**
 * ID: {ecc76994-461e-428d-abd0-c2499f6711fe}
 * Alias: Notifications
 * Caption: $Views_Names_Notifications
 * Group: System
 */
class NotificationsViewInfo {
  //#region Common

  /**
   * View identifier for "Notifications": {ecc76994-461e-428d-abd0-c2499f6711fe}.
   */
   readonly ID: guid = 'ecc76994-461e-428d-abd0-c2499f6711fe';

  /**
   * View name for "Notifications".
   */
   readonly Alias: string = 'Notifications';

  /**
   * View caption for "Notifications".
   */
   readonly Caption: string = '$Views_Names_Notifications';

  /**
   * View group for "Notifications".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: NotificationID.
   */
   readonly ColumnNotificationID: ViewObject = new ViewObject(0, 'NotificationID');

  /**
   * ID:1
   * Alias: NotificationName
   * Caption: $Views_Notifications_Name.
   */
   readonly ColumnNotificationName: ViewObject = new ViewObject(1, 'NotificationName', '$Views_Notifications_Name');

  /**
   * ID:2
   * Alias: NotificationType
   * Caption: $Views_Notifications_NotificationType.
   */
   readonly ColumnNotificationType: ViewObject = new ViewObject(2, 'NotificationType', '$Views_Notifications_NotificationType');

  /**
   * ID:3
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(3, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Notifications_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Notifications_Name_Param');

  /**
   * ID:1
   * Alias: NotificationType
   * Caption: $Views_Notifications_NotificationType_Param.
   */
   readonly ParamNotificationType: ViewObject = new ViewObject(1, 'NotificationType', '$Views_Notifications_NotificationType_Param');

  //#endregion
}

//#endregion

//#region NotificationSubscriptions

/**
 * ID: {41fef937-d98e-48f8-9d47-eed0c1d32adf}
 * Alias: NotificationSubscriptions
 * Caption: $Views_Names_NotificationSubscriptions
 * Group: System
 */
class NotificationSubscriptionsViewInfo {
  //#region Common

  /**
   * View identifier for "NotificationSubscriptions": {41fef937-d98e-48f8-9d47-eed0c1d32adf}.
   */
   readonly ID: guid = '41fef937-d98e-48f8-9d47-eed0c1d32adf';

  /**
   * View name for "NotificationSubscriptions".
   */
   readonly Alias: string = 'NotificationSubscriptions';

  /**
   * View caption for "NotificationSubscriptions".
   */
   readonly Caption: string = '$Views_Names_NotificationSubscriptions';

  /**
   * View group for "NotificationSubscriptions".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: NotificationSubscriptionID.
   */
   readonly ColumnNotificationSubscriptionID: ViewObject = new ViewObject(0, 'NotificationSubscriptionID');

  /**
   * ID:1
   * Alias: NotificationSubscriptionCardID.
   */
   readonly ColumnNotificationSubscriptionCardID: ViewObject = new ViewObject(1, 'NotificationSubscriptionCardID');

  /**
   * ID:2
   * Alias: NotificationSubscriptionCardDigest
   * Caption: $Views_NotificationSubscriptions_CardDigest.
   */
   readonly ColumnNotificationSubscriptionCardDigest: ViewObject = new ViewObject(2, 'NotificationSubscriptionCardDigest', '$Views_NotificationSubscriptions_CardDigest');

  /**
   * ID:3
   * Alias: NotificationSubscriptionDate
   * Caption: $Views_NotificationSubscriptions_SubscriptionDate.
   */
   readonly ColumnNotificationSubscriptionDate: ViewObject = new ViewObject(3, 'NotificationSubscriptionDate', '$Views_NotificationSubscriptions_SubscriptionDate');

  /**
   * ID:4
   * Alias: NotificationSubscriptionNotificationType
   * Caption: $Views_NotificationSubscriptions_NotificationType.
   */
   readonly ColumnNotificationSubscriptionNotificationType: ViewObject = new ViewObject(4, 'NotificationSubscriptionNotificationType', '$Views_NotificationSubscriptions_NotificationType');

  /**
   * ID:5
   * Alias: NotificationSubscriptionType
   * Caption: $Views_NotificationSubscriptions_SubscriptionType.
   */
   readonly ColumnNotificationSubscriptionType: ViewObject = new ViewObject(5, 'NotificationSubscriptionType', '$Views_NotificationSubscriptions_SubscriptionType');

  /**
   * ID:6
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(6, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CardDigest
   * Caption: $Views_NotificationSubscriptions_CardDigest_Param.
   */
   readonly ParamCardDigest: ViewObject = new ViewObject(0, 'CardDigest', '$Views_NotificationSubscriptions_CardDigest_Param');

  /**
   * ID:1
   * Alias: NotificationType
   * Caption: $Views_NotificationSubscriptions_NotificationType_Param.
   */
   readonly ParamNotificationType: ViewObject = new ViewObject(1, 'NotificationType', '$Views_NotificationSubscriptions_NotificationType_Param');

  /**
   * ID:2
   * Alias: SubscriptionDate
   * Caption: $Views_NotificationSubscriptions_SubscriptionDate_Param.
   */
   readonly ParamSubscriptionDate: ViewObject = new ViewObject(2, 'SubscriptionDate', '$Views_NotificationSubscriptions_SubscriptionDate_Param');

  /**
   * ID:3
   * Alias: IsSubscription
   * Caption: $Views_NotificationSubscriptions_IsSubscription_Param.
   */
   readonly ParamIsSubscription: ViewObject = new ViewObject(3, 'IsSubscription', '$Views_NotificationSubscriptions_IsSubscription_Param');

  /**
   * ID:4
   * Alias: User
   * Caption: $Views_NotificationSubscriptions_User_Param.
   */
   readonly ParamUser: ViewObject = new ViewObject(4, 'User', '$Views_NotificationSubscriptions_User_Param');

  //#endregion
}

//#endregion

//#region NotificationTypes

/**
 * ID: {72cd38b6-102a-4368-97a1-08b216865c96}
 * Alias: NotificationTypes
 * Caption: $Views_Names_NotificationTypes
 * Group: System
 */
class NotificationTypesViewInfo {
  //#region Common

  /**
   * View identifier for "NotificationTypes": {72cd38b6-102a-4368-97a1-08b216865c96}.
   */
   readonly ID: guid = '72cd38b6-102a-4368-97a1-08b216865c96';

  /**
   * View name for "NotificationTypes".
   */
   readonly Alias: string = 'NotificationTypes';

  /**
   * View caption for "NotificationTypes".
   */
   readonly Caption: string = '$Views_Names_NotificationTypes';

  /**
   * View group for "NotificationTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: NotificationTypeID.
   */
   readonly ColumnNotificationTypeID: ViewObject = new ViewObject(0, 'NotificationTypeID');

  /**
   * ID:1
   * Alias: NotificationTypeName
   * Caption: $Views_NotificationTypes_Name.
   */
   readonly ColumnNotificationTypeName: ViewObject = new ViewObject(1, 'NotificationTypeName', '$Views_NotificationTypes_Name');

  /**
   * ID:2
   * Alias: NotificationTypeIsGlobal
   * Caption: $Views_NotificationTypes_IsGlobal.
   */
   readonly ColumnNotificationTypeIsGlobal: ViewObject = new ViewObject(2, 'NotificationTypeIsGlobal', '$Views_NotificationTypes_IsGlobal');

  /**
   * ID:3
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(3, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_NotificationTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_NotificationTypes_Name_Param');

  /**
   * ID:1
   * Alias: IsGlobal
   * Caption: $Views_NotificationTypes_IsGlobal_Param.
   */
   readonly ParamIsGlobal: ViewObject = new ViewObject(1, 'IsGlobal', '$Views_NotificationTypes_IsGlobal_Param');

  /**
   * ID:2
   * Alias: CanSubscribe
   * Caption: $Views_NotificationTypes_CanSubscribe_Param.
   */
   readonly ParamCanSubscribe: ViewObject = new ViewObject(2, 'CanSubscribe', '$Views_NotificationTypes_CanSubscribe_Param');

  /**
   * ID:3
   * Alias: CardType
   * Caption: $Views_NotificationTypes_CardType_Param.
   */
   readonly ParamCardType: ViewObject = new ViewObject(3, 'CardType', '$Views_NotificationTypes_CardType_Param');

  /**
   * ID:4
   * Alias: ShowHidden
   * Caption: $Views_NotificationTypes_ShowHidden_Param.
   */
   readonly ParamShowHidden: ViewObject = new ViewObject(4, 'ShowHidden', '$Views_NotificationTypes_ShowHidden_Param');

  //#endregion
}

//#endregion

//#region OcrLanguages

/**
 * ID: {a7496820-b1b7-4443-b889-990996deeff1}
 * Alias: OcrLanguages
 * Caption: $Views_Names_OcrLanguages
 * Group: Ocr
 */
class OcrLanguagesViewInfo {
  //#region Common

  /**
   * View identifier for "OcrLanguages": {a7496820-b1b7-4443-b889-990996deeff1}.
   */
   readonly ID: guid = 'a7496820-b1b7-4443-b889-990996deeff1';

  /**
   * View name for "OcrLanguages".
   */
   readonly Alias: string = 'OcrLanguages';

  /**
   * View caption for "OcrLanguages".
   */
   readonly Caption: string = '$Views_Names_OcrLanguages';

  /**
   * View group for "OcrLanguages".
   */
   readonly Group: string = 'Ocr';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: LanguageID.
   */
   readonly ColumnLanguageID: ViewObject = new ViewObject(0, 'LanguageID');

  /**
   * ID:1
   * Alias: LanguageCaption
   * Caption: $Views_OcrLanguages_Caption.
   */
   readonly ColumnLanguageCaption: ViewObject = new ViewObject(1, 'LanguageCaption', '$Views_OcrLanguages_Caption');

  /**
   * ID:2
   * Alias: LanguageISO
   * Caption: $Views_OcrLanguages_ISO.
   */
   readonly ColumnLanguageISO: ViewObject = new ViewObject(2, 'LanguageISO', '$Views_OcrLanguages_ISO');

  /**
   * ID:3
   * Alias: LanguageCode
   * Caption: $Views_OcrLanguages_Code.
   */
   readonly ColumnLanguageCode: ViewObject = new ViewObject(3, 'LanguageCode', '$Views_OcrLanguages_Code');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: ISO
   * Caption: $Views_OcrLanguages_ISO_Param.
   */
   readonly ParamISO: ViewObject = new ViewObject(0, 'ISO', '$Views_OcrLanguages_ISO_Param');

  /**
   * ID:1
   * Alias: Code
   * Caption: $Views_OcrLanguages_Code_Param.
   */
   readonly ParamCode: ViewObject = new ViewObject(1, 'Code', '$Views_OcrLanguages_Code_Param');

  /**
   * ID:2
   * Alias: Caption
   * Caption: $Views_OcrLanguages_Caption_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(2, 'Caption', '$Views_OcrLanguages_Caption_Param');

  //#endregion
}

//#endregion

//#region OcrOperations

/**
 * ID: {c6047012-cb5c-4fd3-a3d1-3b2b39be7a1f}
 * Alias: OcrOperations
 * Caption: $Views_Names_OcrOperations
 * Group: Ocr
 */
class OcrOperationsViewInfo {
  //#region Common

  /**
   * View identifier for "OcrOperations": {c6047012-cb5c-4fd3-a3d1-3b2b39be7a1f}.
   */
   readonly ID: guid = 'c6047012-cb5c-4fd3-a3d1-3b2b39be7a1f';

  /**
   * View name for "OcrOperations".
   */
   readonly Alias: string = 'OcrOperations';

  /**
   * View caption for "OcrOperations".
   */
   readonly Caption: string = '$Views_Names_OcrOperations';

  /**
   * View group for "OcrOperations".
   */
   readonly Group: string = 'Ocr';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: OperationID.
   */
   readonly ColumnOperationID: ViewObject = new ViewObject(0, 'OperationID');

  /**
   * ID:1
   * Alias: OperationCreated
   * Caption: $Views_OcrOperations_Created.
   */
   readonly ColumnOperationCreated: ViewObject = new ViewObject(1, 'OperationCreated', '$Views_OcrOperations_Created');

  /**
   * ID:2
   * Alias: OperationCreatedByName
   * Caption: $Views_OcrOperations_CreatedBy.
   */
   readonly ColumnOperationCreatedByName: ViewObject = new ViewObject(2, 'OperationCreatedByName', '$Views_OcrOperations_CreatedBy');

  /**
   * ID:3
   * Alias: OperationFileName
   * Caption: $Views_OcrOperations_FileName.
   */
   readonly ColumnOperationFileName: ViewObject = new ViewObject(3, 'OperationFileName', '$Views_OcrOperations_FileName');

  /**
   * ID:4
   * Alias: OperationVersionRowID
   * Caption: $Views_OcrOperations_VersionRowID.
   */
   readonly ColumnOperationVersionRowID: ViewObject = new ViewObject(4, 'OperationVersionRowID', '$Views_OcrOperations_VersionRowID');

  /**
   * ID:5
   * Alias: OperationCardID
   * Caption: $Views_OcrOperations_CardID.
   */
   readonly ColumnOperationCardID: ViewObject = new ViewObject(5, 'OperationCardID', '$Views_OcrOperations_CardID');

  //#endregion
}

//#endregion

//#region OcrPatternTypes

/**
 * ID: {a1f31945-fbbb-4ae2-aa5a-db3354d75693}
 * Alias: OcrPatternTypes
 * Caption: $Views_Names_OcrPatternTypes
 * Group: Ocr
 */
class OcrPatternTypesViewInfo {
  //#region Common

  /**
   * View identifier for "OcrPatternTypes": {a1f31945-fbbb-4ae2-aa5a-db3354d75693}.
   */
   readonly ID: guid = 'a1f31945-fbbb-4ae2-aa5a-db3354d75693';

  /**
   * View name for "OcrPatternTypes".
   */
   readonly Alias: string = 'OcrPatternTypes';

  /**
   * View caption for "OcrPatternTypes".
   */
   readonly Caption: string = '$Views_Names_OcrPatternTypes';

  /**
   * View group for "OcrPatternTypes".
   */
   readonly Group: string = 'Ocr';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeName
   * Caption: $Views_OcrPatternTypes_Name.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(1, 'TypeName', '$Views_OcrPatternTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_OcrPatternTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_OcrPatternTypes_Name_Param');

  //#endregion
}

//#endregion

//#region OcrRecognitionModes

/**
 * ID: {ab45451f-974d-4231-a182-16c6d04bdfba}
 * Alias: OcrRecognitionModes
 * Caption: $Views_Names_OcrRecognitionModes
 * Group: Ocr
 */
class OcrRecognitionModesViewInfo {
  //#region Common

  /**
   * View identifier for "OcrRecognitionModes": {ab45451f-974d-4231-a182-16c6d04bdfba}.
   */
   readonly ID: guid = 'ab45451f-974d-4231-a182-16c6d04bdfba';

  /**
   * View name for "OcrRecognitionModes".
   */
   readonly Alias: string = 'OcrRecognitionModes';

  /**
   * View caption for "OcrRecognitionModes".
   */
   readonly Caption: string = '$Views_Names_OcrRecognitionModes';

  /**
   * View group for "OcrRecognitionModes".
   */
   readonly Group: string = 'Ocr';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ModeID.
   */
   readonly ColumnModeID: ViewObject = new ViewObject(0, 'ModeID');

  /**
   * ID:1
   * Alias: ModeName
   * Caption: $Views_OcrRecognitionModes_Name.
   */
   readonly ColumnModeName: ViewObject = new ViewObject(1, 'ModeName', '$Views_OcrRecognitionModes_Name');

  /**
   * ID:2
   * Alias: ModeDescription
   * Caption: $Views_OcrRecognitionModes_Description.
   */
   readonly ColumnModeDescription: ViewObject = new ViewObject(2, 'ModeDescription', '$Views_OcrRecognitionModes_Description');

  //#endregion
}

//#endregion

//#region OcrRequests

/**
 * ID: {9eed84c8-53f0-4d9a-8619-bdf97c0fc615}
 * Alias: OcrRequests
 * Caption: $Views_Names_OcrRequests
 * Group: Ocr
 */
class OcrRequestsViewInfo {
  //#region Common

  /**
   * View identifier for "OcrRequests": {9eed84c8-53f0-4d9a-8619-bdf97c0fc615}.
   */
   readonly ID: guid = '9eed84c8-53f0-4d9a-8619-bdf97c0fc615';

  /**
   * View name for "OcrRequests".
   */
   readonly Alias: string = 'OcrRequests';

  /**
   * View caption for "OcrRequests".
   */
   readonly Caption: string = '$Views_Names_OcrRequests';

  /**
   * View group for "OcrRequests".
   */
   readonly Group: string = 'Ocr';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RequestID.
   */
   readonly ColumnRequestID: ViewObject = new ViewObject(0, 'RequestID');

  /**
   * ID:1
   * Alias: RequestIsMain
   * Caption: $Views_OcrRequests_IsMain.
   */
   readonly ColumnRequestIsMain: ViewObject = new ViewObject(1, 'RequestIsMain', '$Views_OcrRequests_IsMain');

  /**
   * ID:2
   * Alias: RequestCreated
   * Caption: $Views_OcrRequests_Created.
   */
   readonly ColumnRequestCreated: ViewObject = new ViewObject(2, 'RequestCreated', '$Views_OcrRequests_Created');

  /**
   * ID:3
   * Alias: RequestCreatedByName
   * Caption: $Views_OcrRequests_CreatedBy.
   */
   readonly ColumnRequestCreatedByName: ViewObject = new ViewObject(3, 'RequestCreatedByName', '$Views_OcrRequests_CreatedBy');

  /**
   * ID:4
   * Alias: RequestLanguages
   * Caption: $Views_OcrRequests_Languages.
   */
   readonly ColumnRequestLanguages: ViewObject = new ViewObject(4, 'RequestLanguages', '$Views_OcrRequests_Languages');

  /**
   * ID:5
   * Alias: RequestConfidence
   * Caption: $Views_OcrRequests_Confidence.
   */
   readonly ColumnRequestConfidence: ViewObject = new ViewObject(5, 'RequestConfidence', '$Views_OcrRequests_Confidence');

  /**
   * ID:6
   * Alias: RequestPreprocess
   * Caption: $Views_OcrRequests_Preprocess.
   */
   readonly ColumnRequestPreprocess: ViewObject = new ViewObject(6, 'RequestPreprocess', '$Views_OcrRequests_Preprocess');

  /**
   * ID:7
   * Alias: RequestDetectRotation
   * Caption: $Views_OcrRequests_Autorotation.
   */
   readonly ColumnRequestDetectRotation: ViewObject = new ViewObject(7, 'RequestDetectRotation', '$Views_OcrRequests_Autorotation');

  /**
   * ID:8
   * Alias: RequestOverwrite
   * Caption: $Views_OcrRequests_Overwrite.
   */
   readonly ColumnRequestOverwrite: ViewObject = new ViewObject(8, 'RequestOverwrite', '$Views_OcrRequests_Overwrite');

  /**
   * ID:9
   * Alias: RequestSegmentationMode
   * Caption: $Views_OcrRequests_SegmentationMode.
   */
   readonly ColumnRequestSegmentationMode: ViewObject = new ViewObject(9, 'RequestSegmentationMode', '$Views_OcrRequests_SegmentationMode');

  /**
   * ID:10
   * Alias: RequestStateAppearance.
   */
   readonly ColumnRequestStateAppearance: ViewObject = new ViewObject(10, 'RequestStateAppearance');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Operation
   * Caption: $Views_OcrRequests_Operation_Param.
   */
   readonly ParamOperation: ViewObject = new ViewObject(0, 'Operation', '$Views_OcrRequests_Operation_Param');

  //#endregion
}

//#endregion

//#region OcrSegmentationModes

/**
 * ID: {740088cd-bd83-4d81-89bb-7ff9e12da0d0}
 * Alias: OcrSegmentationModes
 * Caption: $Views_Names_OcrSegmentationModes
 * Group: Ocr
 */
class OcrSegmentationModesViewInfo {
  //#region Common

  /**
   * View identifier for "OcrSegmentationModes": {740088cd-bd83-4d81-89bb-7ff9e12da0d0}.
   */
   readonly ID: guid = '740088cd-bd83-4d81-89bb-7ff9e12da0d0';

  /**
   * View name for "OcrSegmentationModes".
   */
   readonly Alias: string = 'OcrSegmentationModes';

  /**
   * View caption for "OcrSegmentationModes".
   */
   readonly Caption: string = '$Views_Names_OcrSegmentationModes';

  /**
   * View group for "OcrSegmentationModes".
   */
   readonly Group: string = 'Ocr';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ModeID.
   */
   readonly ColumnModeID: ViewObject = new ViewObject(0, 'ModeID');

  /**
   * ID:1
   * Alias: ModeName
   * Caption: $Views_OcrSegmentationModes_Name.
   */
   readonly ColumnModeName: ViewObject = new ViewObject(1, 'ModeName', '$Views_OcrSegmentationModes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_OcrSegmentationModes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_OcrSegmentationModes_Name_Param');

  /**
   * ID:1
   * Alias: Hidden
   * Caption: $Views_OcrSegmentationModes_Hidden_Param.
   */
   readonly ParamHidden: ViewObject = new ViewObject(1, 'Hidden', '$Views_OcrSegmentationModes_Hidden_Param');

  //#endregion
}

//#endregion

//#region Operations

/**
 * ID: {c0bc56cb-0868-4108-996f-a87fe290d90d}
 * Alias: Operations
 * Caption: $Views_Names_Operations
 * Group: System
 */
class OperationsViewInfo {
  //#region Common

  /**
   * View identifier for "Operations": {c0bc56cb-0868-4108-996f-a87fe290d90d}.
   */
   readonly ID: guid = 'c0bc56cb-0868-4108-996f-a87fe290d90d';

  /**
   * View name for "Operations".
   */
   readonly Alias: string = 'Operations';

  /**
   * View caption for "Operations".
   */
   readonly Caption: string = '$Views_Names_Operations';

  /**
   * View group for "Operations".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeName
   * Caption: $Views_Operations_Type.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(1, 'TypeName', '$Views_Operations_Type');

  /**
   * ID:2
   * Alias: OperationID.
   */
   readonly ColumnOperationID: ViewObject = new ViewObject(2, 'OperationID');

  /**
   * ID:3
   * Alias: OperationName
   * Caption: $Views_Operations_Name.
   */
   readonly ColumnOperationName: ViewObject = new ViewObject(3, 'OperationName', '$Views_Operations_Name');

  /**
   * ID:4
   * Alias: StateID.
   */
   readonly ColumnStateID: ViewObject = new ViewObject(4, 'StateID');

  /**
   * ID:5
   * Alias: StateName
   * Caption: $Views_Operations_State.
   */
   readonly ColumnStateName: ViewObject = new ViewObject(5, 'StateName', '$Views_Operations_State');

  /**
   * ID:6
   * Alias: Progress
   * Caption: %.
   */
   readonly ColumnProgress: ViewObject = new ViewObject(6, 'Progress', '%');

  /**
   * ID:7
   * Alias: CreatedByID.
   */
   readonly ColumnCreatedByID: ViewObject = new ViewObject(7, 'CreatedByID');

  /**
   * ID:8
   * Alias: CreatedByName
   * Caption: $Views_Operations_User.
   */
   readonly ColumnCreatedByName: ViewObject = new ViewObject(8, 'CreatedByName', '$Views_Operations_User');

  /**
   * ID:9
   * Alias: Created
   * Caption: $Views_Operations_Created.
   */
   readonly ColumnCreated: ViewObject = new ViewObject(9, 'Created', '$Views_Operations_Created');

  /**
   * ID:10
   * Alias: InProgress
   * Caption: $Views_Operations_InProgress.
   */
   readonly ColumnInProgress: ViewObject = new ViewObject(10, 'InProgress', '$Views_Operations_InProgress');

  /**
   * ID:11
   * Alias: Completed
   * Caption: $Views_Operations_Completed.
   */
   readonly ColumnCompleted: ViewObject = new ViewObject(11, 'Completed', '$Views_Operations_Completed');

  /**
   * ID:12
   * Alias: Postponed
   * Caption: $Views_Operations_Postponed.
   */
   readonly ColumnPostponed: ViewObject = new ViewObject(12, 'Postponed', '$Views_Operations_Postponed');

  /**
   * ID:13
   * Alias: CreationFlags.
   */
   readonly ColumnCreationFlags: ViewObject = new ViewObject(13, 'CreationFlags');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: TypeName
   * Caption: $Views_Operations_Type_Param.
   */
   readonly ParamTypeName: ViewObject = new ViewObject(0, 'TypeName', '$Views_Operations_Type_Param');

  /**
   * ID:1
   * Alias: Digest
   * Caption: $Views_Operations_Name_Param.
   */
   readonly ParamDigest: ViewObject = new ViewObject(1, 'Digest', '$Views_Operations_Name_Param');

  /**
   * ID:2
   * Alias: StateName
   * Caption: $Views_Operations_State_Param.
   */
   readonly ParamStateName: ViewObject = new ViewObject(2, 'StateName', '$Views_Operations_State_Param');

  /**
   * ID:3
   * Alias: CreatedByID
   * Caption: $Views_Operations_User_Param.
   */
   readonly ParamCreatedByID: ViewObject = new ViewObject(3, 'CreatedByID', '$Views_Operations_User_Param');

  /**
   * ID:4
   * Alias: CreatedByName
   * Caption: $Views_Operations_UserName_Param.
   */
   readonly ParamCreatedByName: ViewObject = new ViewObject(4, 'CreatedByName', '$Views_Operations_UserName_Param');

  /**
   * ID:5
   * Alias: Created
   * Caption: $Views_Operations_Created_Param.
   */
   readonly ParamCreated: ViewObject = new ViewObject(5, 'Created', '$Views_Operations_Created_Param');

  /**
   * ID:6
   * Alias: InProgress
   * Caption: $Views_Operations_InProgress_Param.
   */
   readonly ParamInProgress: ViewObject = new ViewObject(6, 'InProgress', '$Views_Operations_InProgress_Param');

  /**
   * ID:7
   * Alias: Completed
   * Caption: $Views_Operations_Completed_Param.
   */
   readonly ParamCompleted: ViewObject = new ViewObject(7, 'Completed', '$Views_Operations_Completed_Param');

  /**
   * ID:8
   * Alias: Postponed
   * Caption: $Views_Operations_Postponed_Param.
   */
   readonly ParamPostponed: ViewObject = new ViewObject(8, 'Postponed', '$Views_Operations_Postponed_Param');

  //#endregion
}

//#endregion

//#region OutgoingDocuments

/**
 * ID: {a4d2d4e1-a59c-4265-a2ee-f58ca0cbc3fc}
 * Alias: OutgoingDocuments
 * Caption: $Views_Names_OutgoingDocuments
 * Group: KrDocuments
 */
class OutgoingDocumentsViewInfo {
  //#region Common

  /**
   * View identifier for "OutgoingDocuments": {a4d2d4e1-a59c-4265-a2ee-f58ca0cbc3fc}.
   */
   readonly ID: guid = 'a4d2d4e1-a59c-4265-a2ee-f58ca0cbc3fc';

  /**
   * View name for "OutgoingDocuments".
   */
   readonly Alias: string = 'OutgoingDocuments';

  /**
   * View caption for "OutgoingDocuments".
   */
   readonly Caption: string = '$Views_Names_OutgoingDocuments';

  /**
   * View group for "OutgoingDocuments".
   */
   readonly Group: string = 'KrDocuments';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DocID.
   */
   readonly ColumnDocID: ViewObject = new ViewObject(0, 'DocID');

  /**
   * ID:1
   * Alias: DocNumber
   * Caption: $Views_Registers_Number.
   */
   readonly ColumnDocNumber: ViewObject = new ViewObject(1, 'DocNumber', '$Views_Registers_Number');

  /**
   * ID:2
   * Alias: SubTypeTitle
   * Caption: $Views_Registers_DocType.
   */
   readonly ColumnSubTypeTitle: ViewObject = new ViewObject(2, 'SubTypeTitle', '$Views_Registers_DocType');

  /**
   * ID:3
   * Alias: DocSubject
   * Caption: $Views_Registers_Subject.
   */
   readonly ColumnDocSubject: ViewObject = new ViewObject(3, 'DocSubject', '$Views_Registers_Subject');

  /**
   * ID:4
   * Alias: DocDescription
   * Caption: $Views_Registers_DocDescription.
   */
   readonly ColumnDocDescription: ViewObject = new ViewObject(4, 'DocDescription', '$Views_Registers_DocDescription');

  /**
   * ID:5
   * Alias: PartnerID.
   */
   readonly ColumnPartnerID: ViewObject = new ViewObject(5, 'PartnerID');

  /**
   * ID:6
   * Alias: PartnerName
   * Caption: $Views_Registers_Partner.
   */
   readonly ColumnPartnerName: ViewObject = new ViewObject(6, 'PartnerName', '$Views_Registers_Partner');

  /**
   * ID:7
   * Alias: AuthorID.
   */
   readonly ColumnAuthorID: ViewObject = new ViewObject(7, 'AuthorID');

  /**
   * ID:8
   * Alias: AuthorName
   * Caption: $Views_Registers_Author.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(8, 'AuthorName', '$Views_Registers_Author');

  /**
   * ID:9
   * Alias: RegistratorID.
   */
   readonly ColumnRegistratorID: ViewObject = new ViewObject(9, 'RegistratorID');

  /**
   * ID:10
   * Alias: RegistratorName
   * Caption: $Views_Registers_Registrator.
   */
   readonly ColumnRegistratorName: ViewObject = new ViewObject(10, 'RegistratorName', '$Views_Registers_Registrator');

  /**
   * ID:11
   * Alias: KrState
   * Caption: $Views_Registers_State.
   */
   readonly ColumnKrState: ViewObject = new ViewObject(11, 'KrState', '$Views_Registers_State');

  /**
   * ID:12
   * Alias: DocDate
   * Caption: $Views_Registers_DocDate.
   */
   readonly ColumnDocDate: ViewObject = new ViewObject(12, 'DocDate', '$Views_Registers_DocDate');

  /**
   * ID:13
   * Alias: CreationDate
   * Caption: $Views_Registers_CreationDate.
   */
   readonly ColumnCreationDate: ViewObject = new ViewObject(13, 'CreationDate', '$Views_Registers_CreationDate');

  /**
   * ID:14
   * Alias: Department
   * Caption: $Views_Registers_Department.
   */
   readonly ColumnDepartment: ViewObject = new ViewObject(14, 'Department', '$Views_Registers_Department');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: IsAuthor
   * Caption: $Views_Registers_IsAuthor_Param.
   */
   readonly ParamIsAuthor: ViewObject = new ViewObject(0, 'IsAuthor', '$Views_Registers_IsAuthor_Param');

  /**
   * ID:1
   * Alias: IsInitiator
   * Caption: $Views_Registers_IsInitiator_Param.
   */
   readonly ParamIsInitiator: ViewObject = new ViewObject(1, 'IsInitiator', '$Views_Registers_IsInitiator_Param');

  /**
   * ID:2
   * Alias: IsRegistrator
   * Caption: $Views_Registers_IsRegistrator_Param.
   */
   readonly ParamIsRegistrator: ViewObject = new ViewObject(2, 'IsRegistrator', '$Views_Registers_IsRegistrator_Param');

  /**
   * ID:3
   * Alias: Partner
   * Caption: $Views_Registers_Partner_Param.
   */
   readonly ParamPartner: ViewObject = new ViewObject(3, 'Partner', '$Views_Registers_Partner_Param');

  /**
   * ID:4
   * Alias: Number
   * Caption: $Views_Registers_Number_Param.
   */
   readonly ParamNumber: ViewObject = new ViewObject(4, 'Number', '$Views_Registers_Number_Param');

  /**
   * ID:5
   * Alias: Subject
   * Caption: $Views_Registers_Subject_Param.
   */
   readonly ParamSubject: ViewObject = new ViewObject(5, 'Subject', '$Views_Registers_Subject_Param');

  /**
   * ID:6
   * Alias: DocDate
   * Caption: $Views_Registers_DocDate_Param.
   */
   readonly ParamDocDate: ViewObject = new ViewObject(6, 'DocDate', '$Views_Registers_DocDate_Param');

  /**
   * ID:7
   * Alias: Author
   * Caption: $Views_Registers_Author_Param.
   */
   readonly ParamAuthor: ViewObject = new ViewObject(7, 'Author', '$Views_Registers_Author_Param');

  /**
   * ID:8
   * Alias: Registrator
   * Caption: $Views_Registers_Registrator_Param.
   */
   readonly ParamRegistrator: ViewObject = new ViewObject(8, 'Registrator', '$Views_Registers_Registrator_Param');

  /**
   * ID:9
   * Alias: State
   * Caption: $Views_Registers_State_Param.
   */
   readonly ParamState: ViewObject = new ViewObject(9, 'State', '$Views_Registers_State_Param');

  /**
   * ID:10
   * Alias: DocType
   * Caption: $Views_Registers_DocType_Param.
   */
   readonly ParamDocType: ViewObject = new ViewObject(10, 'DocType', '$Views_Registers_DocType_Param');

  /**
   * ID:11
   * Alias: Department
   * Caption: $Views_Registers_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(11, 'Department', '$Views_Registers_Department_Param');

  /**
   * ID:12
   * Alias: CreationDate
   * Caption: $Views_Registers_CreationDate_Param.
   */
   readonly ParamCreationDate: ViewObject = new ViewObject(12, 'CreationDate', '$Views_Registers_CreationDate_Param');

  //#endregion
}

//#endregion

//#region Partitions

/**
 * ID: {9500e883-9c8e-427e-930b-e93adfd0f56a}
 * Alias: Partitions
 * Caption: $Views_Names_Partitions
 * Group: System
 */
class PartitionsViewInfo {
  //#region Common

  /**
   * View identifier for "Partitions": {9500e883-9c8e-427e-930b-e93adfd0f56a}.
   */
   readonly ID: guid = '9500e883-9c8e-427e-930b-e93adfd0f56a';

  /**
   * View name for "Partitions".
   */
   readonly Alias: string = 'Partitions';

  /**
   * View caption for "Partitions".
   */
   readonly Caption: string = '$Views_Names_Partitions';

  /**
   * View group for "Partitions".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: PartitionID.
   */
   readonly ColumnPartitionID: ViewObject = new ViewObject(0, 'PartitionID');

  /**
   * ID:1
   * Alias: PartitionName
   * Caption: $Views_Partitions_Name.
   */
   readonly ColumnPartitionName: ViewObject = new ViewObject(1, 'PartitionName', '$Views_Partitions_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: PartitionID
   * Caption: PartitionID.
   */
   readonly ParamPartitionID: ViewObject = new ViewObject(0, 'PartitionID', 'PartitionID');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_Partitions_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_Partitions_Name_Param');

  //#endregion
}

//#endregion

//#region Partners

/**
 * ID: {f9e6f291-2de8-459c-8d05-420fb8cce90f}
 * Alias: Partners
 * Caption: $Views_Names_Partners
 * Group: System
 */
class PartnersViewInfo {
  //#region Common

  /**
   * View identifier for "Partners": {f9e6f291-2de8-459c-8d05-420fb8cce90f}.
   */
   readonly ID: guid = 'f9e6f291-2de8-459c-8d05-420fb8cce90f';

  /**
   * View name for "Partners".
   */
   readonly Alias: string = 'Partners';

  /**
   * View caption for "Partners".
   */
   readonly Caption: string = '$Views_Names_Partners';

  /**
   * View group for "Partners".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: PartnerID.
   */
   readonly ColumnPartnerID: ViewObject = new ViewObject(0, 'PartnerID');

  /**
   * ID:1
   * Alias: PartnerName
   * Caption: $Views_Partners_Name.
   */
   readonly ColumnPartnerName: ViewObject = new ViewObject(1, 'PartnerName', '$Views_Partners_Name');

  /**
   * ID:2
   * Alias: FullName
   * Caption: $Views_Partners_FullName.
   */
   readonly ColumnFullName: ViewObject = new ViewObject(2, 'FullName', '$Views_Partners_FullName');

  /**
   * ID:3
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(3, 'TypeID');

  /**
   * ID:4
   * Alias: TypeName
   * Caption: $Views_Partners_Type.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(4, 'TypeName', '$Views_Partners_Type');

  /**
   * ID:5
   * Alias: INN
   * Caption: $Views_Partners_INN.
   */
   readonly ColumnINN: ViewObject = new ViewObject(5, 'INN', '$Views_Partners_INN');

  /**
   * ID:6
   * Alias: KPP
   * Caption: $Views_Partners_KPP.
   */
   readonly ColumnKPP: ViewObject = new ViewObject(6, 'KPP', '$Views_Partners_KPP');

  /**
   * ID:7
   * Alias: OGRN.
   */
   readonly ColumnOGRN: ViewObject = new ViewObject(7, 'OGRN');

  /**
   * ID:8
   * Alias: Comment.
   */
   readonly ColumnComment: ViewObject = new ViewObject(8, 'Comment');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: PartnerID
   * Caption: PartnerID.
   */
   readonly ParamPartnerID: ViewObject = new ViewObject(0, 'PartnerID', 'PartnerID');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_Partners_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_Partners_Name_Param');

  /**
   * ID:2
   * Alias: FullName
   * Caption: $Views_Partners_FullName_Param.
   */
   readonly ParamFullName: ViewObject = new ViewObject(2, 'FullName', '$Views_Partners_FullName_Param');

  /**
   * ID:3
   * Alias: Type
   * Caption: $Views_Partners_Type_Param.
   */
   readonly ParamType: ViewObject = new ViewObject(3, 'Type', '$Views_Partners_Type_Param');

  /**
   * ID:4
   * Alias: INN
   * Caption: $Views_Partners_INN_Param.
   */
   readonly ParamINN: ViewObject = new ViewObject(4, 'INN', '$Views_Partners_INN_Param');

  /**
   * ID:5
   * Alias: KPP
   * Caption: $Views_Partners_KPP_Param.
   */
   readonly ParamKPP: ViewObject = new ViewObject(5, 'KPP', '$Views_Partners_KPP_Param');

  /**
   * ID:6
   * Alias: OGRN
   * Caption: $Views_Partners_OGRN_Param.
   */
   readonly ParamOGRN: ViewObject = new ViewObject(6, 'OGRN', '$Views_Partners_OGRN_Param');

  /**
   * ID:7
   * Alias: Comment
   * Caption: $Views_Partners_Comment_Param.
   */
   readonly ParamComment: ViewObject = new ViewObject(7, 'Comment', '$Views_Partners_Comment_Param');

  //#endregion
}

//#endregion

//#region PartnersContacts

/**
 * ID: {d0971b0f-42a0-433c-b3f4-a1d1b279156c}
 * Alias: PartnersContacts
 * Caption: $Views_Names_PartnersContacts
 * Group: System
 */
class PartnersContactsViewInfo {
  //#region Common

  /**
   * View identifier for "PartnersContacts": {d0971b0f-42a0-433c-b3f4-a1d1b279156c}.
   */
   readonly ID: guid = 'd0971b0f-42a0-433c-b3f4-a1d1b279156c';

  /**
   * View name for "PartnersContacts".
   */
   readonly Alias: string = 'PartnersContacts';

  /**
   * View caption for "PartnersContacts".
   */
   readonly Caption: string = '$Views_Names_PartnersContacts';

  /**
   * View group for "PartnersContacts".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: PartnerContactPartnerID.
   */
   readonly ColumnPartnerContactPartnerID: ViewObject = new ViewObject(0, 'PartnerContactPartnerID');

  /**
   * ID:1
   * Alias: PartnerContactPartnerName
   * Caption: $CardTypes_Controls_Partner.
   */
   readonly ColumnPartnerContactPartnerName: ViewObject = new ViewObject(1, 'PartnerContactPartnerName', '$CardTypes_Controls_Partner');

  /**
   * ID:2
   * Alias: PartnerContactRowID.
   */
   readonly ColumnPartnerContactRowID: ViewObject = new ViewObject(2, 'PartnerContactRowID');

  /**
   * ID:3
   * Alias: PartnerContactFullName
   * Caption: $CardTypes_Controls_Columns_FullName.
   */
   readonly ColumnPartnerContactFullName: ViewObject = new ViewObject(3, 'PartnerContactFullName', '$CardTypes_Controls_Columns_FullName');

  /**
   * ID:4
   * Alias: Department
   * Caption: $CardTypes_Controls_Columns_Department.
   */
   readonly ColumnDepartment: ViewObject = new ViewObject(4, 'Department', '$CardTypes_Controls_Columns_Department');

  /**
   * ID:5
   * Alias: PartnerContactName.
   */
   readonly ColumnPartnerContactName: ViewObject = new ViewObject(5, 'PartnerContactName');

  /**
   * ID:6
   * Alias: PhoneNumber
   * Caption: $CardTypes_Controls_Columns_PhoneNumber.
   */
   readonly ColumnPhoneNumber: ViewObject = new ViewObject(6, 'PhoneNumber', '$CardTypes_Controls_Columns_PhoneNumber');

  /**
   * ID:7
   * Alias: Email
   * Caption: $CardTypes_Controls_Columns_Email.
   */
   readonly ColumnEmail: ViewObject = new ViewObject(7, 'Email', '$CardTypes_Controls_Columns_Email');

  /**
   * ID:8
   * Alias: ContactAddress
   * Caption: $CardTypes_Controls_ContactAddress.
   */
   readonly ColumnContactAddress: ViewObject = new ViewObject(8, 'ContactAddress', '$CardTypes_Controls_ContactAddress');

  /**
   * ID:9
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(9, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: PartnerID
   * Caption: $CardTypes_Controls_Partner.
   */
   readonly ParamPartnerID: ViewObject = new ViewObject(0, 'PartnerID', '$CardTypes_Controls_Partner');

  /**
   * ID:1
   * Alias: PartnerIDHidden
   * Caption: $CardTypes_Controls_Partner.
   */
   readonly ParamPartnerIDHidden: ViewObject = new ViewObject(1, 'PartnerIDHidden', '$CardTypes_Controls_Partner');

  /**
   * ID:2
   * Alias: Name
   * Caption: $CardTypes_Controls_Columns_FullName.
   */
   readonly ParamName: ViewObject = new ViewObject(2, 'Name', '$CardTypes_Controls_Columns_FullName');

  //#endregion
}

//#endregion

//#region PartnersTypes

/**
 * ID: {59d56dd0-700f-46de-afd5-9aa3a1f9f69e}
 * Alias: PartnersTypes
 * Caption: $Views_Names_PartnersTypes
 * Group: System
 */
class PartnersTypesViewInfo {
  //#region Common

  /**
   * View identifier for "PartnersTypes": {59d56dd0-700f-46de-afd5-9aa3a1f9f69e}.
   */
   readonly ID: guid = '59d56dd0-700f-46de-afd5-9aa3a1f9f69e';

  /**
   * View name for "PartnersTypes".
   */
   readonly Alias: string = 'PartnersTypes';

  /**
   * View caption for "PartnersTypes".
   */
   readonly Caption: string = '$Views_Names_PartnersTypes';

  /**
   * View group for "PartnersTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeName
   * Caption: $Views_ParnerTypes_Name.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(1, 'TypeName', '$Views_ParnerTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_ParnerTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_ParnerTypes_Name_Param');

  //#endregion
}

//#endregion

//#region ProtocolCompletedTasks

/**
 * ID: {65dde283-7e4c-4898-b91f-2921e8fc8ac4}
 * Alias: ProtocolCompletedTasks
 * Caption: $Views_Names_ProtocolCompletedTasks
 * Group: KrDocuments
 */
class ProtocolCompletedTasksViewInfo {
  //#region Common

  /**
   * View identifier for "ProtocolCompletedTasks": {65dde283-7e4c-4898-b91f-2921e8fc8ac4}.
   */
   readonly ID: guid = '65dde283-7e4c-4898-b91f-2921e8fc8ac4';

  /**
   * View name for "ProtocolCompletedTasks".
   */
   readonly Alias: string = 'ProtocolCompletedTasks';

  /**
   * View caption for "ProtocolCompletedTasks".
   */
   readonly Caption: string = '$Views_Names_ProtocolCompletedTasks';

  /**
   * View group for "ProtocolCompletedTasks".
   */
   readonly Group: string = 'KrDocuments';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(0, 'CardID');

  /**
   * ID:1
   * Alias: CardName
   * Caption: $Views_CompletedTasks_Card.
   */
   readonly ColumnCardName: ViewObject = new ViewObject(1, 'CardName', '$Views_CompletedTasks_Card');

  /**
   * ID:2
   * Alias: TaskID.
   */
   readonly ColumnTaskID: ViewObject = new ViewObject(2, 'TaskID');

  /**
   * ID:3
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(3, 'TypeID');

  /**
   * ID:4
   * Alias: TypeCaption
   * Caption: $Views_CompletedTasks_TaskType.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(4, 'TypeCaption', '$Views_CompletedTasks_TaskType');

  /**
   * ID:5
   * Alias: CardTypeCaption
   * Caption: $Views_CompletedTasks_CardType.
   */
   readonly ColumnCardTypeCaption: ViewObject = new ViewObject(5, 'CardTypeCaption', '$Views_CompletedTasks_CardType');

  /**
   * ID:6
   * Alias: RoleID.
   */
   readonly ColumnRoleID: ViewObject = new ViewObject(6, 'RoleID');

  /**
   * ID:7
   * Alias: RoleName
   * Caption: $Views_CompletedTasks_Role.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(7, 'RoleName', '$Views_CompletedTasks_Role');

  /**
   * ID:8
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(8, 'UserID');

  /**
   * ID:9
   * Alias: UserName
   * Caption: $Views_CompletedTasks_User.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(9, 'UserName', '$Views_CompletedTasks_User');

  /**
   * ID:10
   * Alias: AuthorID.
   */
   readonly ColumnAuthorID: ViewObject = new ViewObject(10, 'AuthorID');

  /**
   * ID:11
   * Alias: AuthorName
   * Caption: $Views_CompletedTasks_Author.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(11, 'AuthorName', '$Views_CompletedTasks_Author');

  /**
   * ID:12
   * Alias: Created
   * Caption: $Views_CompletedTasks_Created.
   */
   readonly ColumnCreated: ViewObject = new ViewObject(12, 'Created', '$Views_CompletedTasks_Created');

  /**
   * ID:13
   * Alias: Planned
   * Caption: $Views_CompletedTasks_Planned.
   */
   readonly ColumnPlanned: ViewObject = new ViewObject(13, 'Planned', '$Views_CompletedTasks_Planned');

  /**
   * ID:14
   * Alias: Completed
   * Caption: $Views_CompletedTasks_Completed.
   */
   readonly ColumnCompleted: ViewObject = new ViewObject(14, 'Completed', '$Views_CompletedTasks_Completed');

  /**
   * ID:15
   * Alias: OptionID.
   */
   readonly ColumnOptionID: ViewObject = new ViewObject(15, 'OptionID');

  /**
   * ID:16
   * Alias: OptionCaption
   * Caption: $Views_CompletedTasks_CompletionOption.
   */
   readonly ColumnOptionCaption: ViewObject = new ViewObject(16, 'OptionCaption', '$Views_CompletedTasks_CompletionOption');

  /**
   * ID:17
   * Alias: Result
   * Caption: $Views_CompletedTasks_Result.
   */
   readonly ColumnResult: ViewObject = new ViewObject(17, 'Result', '$Views_CompletedTasks_Result');

  /**
   * ID:18
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(18, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CardID
   * Caption: $Views_CompletedTasks_Card.
   */
   readonly ParamCardID: ViewObject = new ViewObject(0, 'CardID', '$Views_CompletedTasks_Card');

  //#endregion
}

//#endregion

//#region ProtocolReportsWithPhoto

/**
 * ID: {a5bb501c-3169-4d72-8098-588f932815b1}
 * Alias: ProtocolReportsWithPhoto
 * Caption: $Views_Names_ProtocolReportsWithPhoto
 * Group: KrDocuments
 */
class ProtocolReportsWithPhotoViewInfo {
  //#region Common

  /**
   * View identifier for "ProtocolReportsWithPhoto": {a5bb501c-3169-4d72-8098-588f932815b1}.
   */
   readonly ID: guid = 'a5bb501c-3169-4d72-8098-588f932815b1';

  /**
   * View name for "ProtocolReportsWithPhoto".
   */
   readonly Alias: string = 'ProtocolReportsWithPhoto';

  /**
   * View caption for "ProtocolReportsWithPhoto".
   */
   readonly Caption: string = '$Views_Names_ProtocolReportsWithPhoto';

  /**
   * View group for "ProtocolReportsWithPhoto".
   */
   readonly Group: string = 'KrDocuments';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: PersonName
   * Caption: PersonName.
   */
   readonly ColumnPersonName: ViewObject = new ViewObject(0, 'PersonName', 'PersonName');

  /**
   * ID:1
   * Alias: Subject
   * Caption: Subject.
   */
   readonly ColumnSubject: ViewObject = new ViewObject(1, 'Subject', 'Subject');

  /**
   * ID:2
   * Alias: PhotoFileID
   * Caption: PhotoFileID.
   */
   readonly ColumnPhotoFileID: ViewObject = new ViewObject(2, 'PhotoFileID', 'PhotoFileID');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CardId
   * Caption: CardId.
   */
   readonly ParamCardId: ViewObject = new ViewObject(0, 'CardId', 'CardId');

  //#endregion
}

//#endregion

//#region Protocols

/**
 * ID: {775558fa-2ec3-4c38-819e-9395e94c28c7}
 * Alias: Protocols
 * Caption: $Views_Names_Protocols
 * Group: KrDocuments
 */
class ProtocolsViewInfo {
  //#region Common

  /**
   * View identifier for "Protocols": {775558fa-2ec3-4c38-819e-9395e94c28c7}.
   */
   readonly ID: guid = '775558fa-2ec3-4c38-819e-9395e94c28c7';

  /**
   * View name for "Protocols".
   */
   readonly Alias: string = 'Protocols';

  /**
   * View caption for "Protocols".
   */
   readonly Caption: string = '$Views_Names_Protocols';

  /**
   * View group for "Protocols".
   */
   readonly Group: string = 'KrDocuments';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DocID.
   */
   readonly ColumnDocID: ViewObject = new ViewObject(0, 'DocID');

  /**
   * ID:1
   * Alias: DocNumber
   * Caption: $Views_Registers_Number.
   */
   readonly ColumnDocNumber: ViewObject = new ViewObject(1, 'DocNumber', '$Views_Registers_Number');

  /**
   * ID:2
   * Alias: DocSubject
   * Caption: $Views_Registers_ProtocolSubject.
   */
   readonly ColumnDocSubject: ViewObject = new ViewObject(2, 'DocSubject', '$Views_Registers_ProtocolSubject');

  /**
   * ID:3
   * Alias: DocDescription
   * Caption: $Views_Registers_DocDescription.
   */
   readonly ColumnDocDescription: ViewObject = new ViewObject(3, 'DocDescription', '$Views_Registers_DocDescription');

  /**
   * ID:4
   * Alias: AuthorID.
   */
   readonly ColumnAuthorID: ViewObject = new ViewObject(4, 'AuthorID');

  /**
   * ID:5
   * Alias: AuthorName
   * Caption: $Views_Registers_Secretary.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(5, 'AuthorName', '$Views_Registers_Secretary');

  /**
   * ID:6
   * Alias: RegistratorID.
   */
   readonly ColumnRegistratorID: ViewObject = new ViewObject(6, 'RegistratorID');

  /**
   * ID:7
   * Alias: RegistratorName
   * Caption: $Views_Registers_Registrator.
   */
   readonly ColumnRegistratorName: ViewObject = new ViewObject(7, 'RegistratorName', '$Views_Registers_Registrator');

  /**
   * ID:8
   * Alias: ProtocolDate
   * Caption: $Views_Registers_ProtocolDate.
   */
   readonly ColumnProtocolDate: ViewObject = new ViewObject(8, 'ProtocolDate', '$Views_Registers_ProtocolDate');

  /**
   * ID:9
   * Alias: CreationDate
   * Caption: $Views_Registers_CreationDate.
   */
   readonly ColumnCreationDate: ViewObject = new ViewObject(9, 'CreationDate', '$Views_Registers_CreationDate');

  /**
   * ID:10
   * Alias: Department
   * Caption: $Views_Registers_Department.
   */
   readonly ColumnDepartment: ViewObject = new ViewObject(10, 'Department', '$Views_Registers_Department');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: IsAuthor
   * Caption: $Views_Registers_IsSecretary_Param.
   */
   readonly ParamIsAuthor: ViewObject = new ViewObject(0, 'IsAuthor', '$Views_Registers_IsSecretary_Param');

  /**
   * ID:1
   * Alias: IsRegistrator
   * Caption: $Views_Registers_IsRegistrator_Param.
   */
   readonly ParamIsRegistrator: ViewObject = new ViewObject(1, 'IsRegistrator', '$Views_Registers_IsRegistrator_Param');

  /**
   * ID:2
   * Alias: Number
   * Caption: $Views_Registers_Number_Param.
   */
   readonly ParamNumber: ViewObject = new ViewObject(2, 'Number', '$Views_Registers_Number_Param');

  /**
   * ID:3
   * Alias: Subject
   * Caption: $Views_Registers_ProtocolSubject_Param.
   */
   readonly ParamSubject: ViewObject = new ViewObject(3, 'Subject', '$Views_Registers_ProtocolSubject_Param');

  /**
   * ID:4
   * Alias: ProtocolDate
   * Caption: $Views_Registers_ProtocolDate_Param.
   */
   readonly ParamProtocolDate: ViewObject = new ViewObject(4, 'ProtocolDate', '$Views_Registers_ProtocolDate_Param');

  /**
   * ID:5
   * Alias: Author
   * Caption: $Views_Registers_Secretary_Param.
   */
   readonly ParamAuthor: ViewObject = new ViewObject(5, 'Author', '$Views_Registers_Secretary_Param');

  /**
   * ID:6
   * Alias: Registrator
   * Caption: $Views_Registers_Registrator_Param.
   */
   readonly ParamRegistrator: ViewObject = new ViewObject(6, 'Registrator', '$Views_Registers_Registrator_Param');

  /**
   * ID:7
   * Alias: Department
   * Caption: $Views_Registers_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(7, 'Department', '$Views_Registers_Department_Param');

  //#endregion
}

//#endregion

//#region RefDocumentsLookup

/**
 * ID: {57fb8582-bfe3-4ae9-8ee3-1feb96b18803}
 * Alias: RefDocumentsLookup
 * Caption: $Views_Names_RefDocumentsLookup
 * Group: KrDocuments
 */
class RefDocumentsLookupViewInfo {
  //#region Common

  /**
   * View identifier for "RefDocumentsLookup": {57fb8582-bfe3-4ae9-8ee3-1feb96b18803}.
   */
   readonly ID: guid = '57fb8582-bfe3-4ae9-8ee3-1feb96b18803';

  /**
   * View name for "RefDocumentsLookup".
   */
   readonly Alias: string = 'RefDocumentsLookup';

  /**
   * View caption for "RefDocumentsLookup".
   */
   readonly Caption: string = '$Views_Names_RefDocumentsLookup';

  /**
   * View group for "RefDocumentsLookup".
   */
   readonly Group: string = 'KrDocuments';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DocID.
   */
   readonly ColumnDocID: ViewObject = new ViewObject(0, 'DocID');

  /**
   * ID:1
   * Alias: DocDescription
   * Caption: $Views_RefDocumentsLookup_Description.
   */
   readonly ColumnDocDescription: ViewObject = new ViewObject(1, 'DocDescription', '$Views_RefDocumentsLookup_Description');

  /**
   * ID:2
   * Alias: DocTypeName
   * Caption: $Views_RefDocumentsLookup_Type.
   */
   readonly ColumnDocTypeName: ViewObject = new ViewObject(2, 'DocTypeName', '$Views_RefDocumentsLookup_Type');

  /**
   * ID:3
   * Alias: Date
   * Caption: $Views_RefDocumentsLookup_Date.
   */
   readonly ColumnDate: ViewObject = new ViewObject(3, 'Date', '$Views_RefDocumentsLookup_Date');

  /**
   * ID:4
   * Alias: PartnerName
   * Caption: $Views_RefDocumentsLookup_Partner.
   */
   readonly ColumnPartnerName: ViewObject = new ViewObject(4, 'PartnerName', '$Views_RefDocumentsLookup_Partner');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Description
   * Caption: $Views_RefDocumentsLookup_Description_Param.
   */
   readonly ParamDescription: ViewObject = new ViewObject(0, 'Description', '$Views_RefDocumentsLookup_Description_Param');

  //#endregion
}

//#endregion

//#region ReportCurrentTasksByDepartment

/**
 * ID: {48224a01-8731-4ac1-a4f0-749ee5f375d2}
 * Alias: ReportCurrentTasksByDepartment
 * Caption: $Views_Names_ReportCurrentTasksByDepartment
 * Group: System
 */
class ReportCurrentTasksByDepartmentViewInfo {
  //#region Common

  /**
   * View identifier for "ReportCurrentTasksByDepartment": {48224a01-8731-4ac1-a4f0-749ee5f375d2}.
   */
   readonly ID: guid = '48224a01-8731-4ac1-a4f0-749ee5f375d2';

  /**
   * View name for "ReportCurrentTasksByDepartment".
   */
   readonly Alias: string = 'ReportCurrentTasksByDepartment';

  /**
   * View caption for "ReportCurrentTasksByDepartment".
   */
   readonly Caption: string = '$Views_Names_ReportCurrentTasksByDepartment';

  /**
   * View group for "ReportCurrentTasksByDepartment".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DeptID.
   */
   readonly ColumnDeptID: ViewObject = new ViewObject(0, 'DeptID');

  /**
   * ID:1
   * Alias: DeptName
   * Caption: $Views_ReportCurrentTasksByDepartment_Department.
   */
   readonly ColumnDeptName: ViewObject = new ViewObject(1, 'DeptName', '$Views_ReportCurrentTasksByDepartment_Department');

  /**
   * ID:2
   * Alias: New
   * Caption: $Views_ReportCurrentTasksByDepartment_New.
   */
   readonly ColumnNew: ViewObject = new ViewObject(2, 'New', '$Views_ReportCurrentTasksByDepartment_New');

  /**
   * ID:3
   * Alias: NewDelayed
   * Caption: $Views_ReportCurrentTasksByDepartment_NewDelayed.
   */
   readonly ColumnNewDelayed: ViewObject = new ViewObject(3, 'NewDelayed', '$Views_ReportCurrentTasksByDepartment_NewDelayed');

  /**
   * ID:4
   * Alias: NewAvgDelayPeriod
   * Caption: $Views_ReportCurrentTasksByDepartment_NewAvgDelayPeriod.
   */
   readonly ColumnNewAvgDelayPeriod: ViewObject = new ViewObject(4, 'NewAvgDelayPeriod', '$Views_ReportCurrentTasksByDepartment_NewAvgDelayPeriod');

  /**
   * ID:5
   * Alias: InWork
   * Caption: $Views_ReportCurrentTasksByDepartment_InWork.
   */
   readonly ColumnInWork: ViewObject = new ViewObject(5, 'InWork', '$Views_ReportCurrentTasksByDepartment_InWork');

  /**
   * ID:6
   * Alias: InWorkDelayed
   * Caption: $Views_ReportCurrentTasksByDepartment_InWorkDelayed.
   */
   readonly ColumnInWorkDelayed: ViewObject = new ViewObject(6, 'InWorkDelayed', '$Views_ReportCurrentTasksByDepartment_InWorkDelayed');

  /**
   * ID:7
   * Alias: InWorkAvgDelayPeriod
   * Caption: $Views_ReportCurrentTasksByDepartment_InWorkAvgDelayPeriod.
   */
   readonly ColumnInWorkAvgDelayPeriod: ViewObject = new ViewObject(7, 'InWorkAvgDelayPeriod', '$Views_ReportCurrentTasksByDepartment_InWorkAvgDelayPeriod');

  /**
   * ID:8
   * Alias: Postponed
   * Caption: $Views_ReportCurrentTasksByDepartment_Postponed.
   */
   readonly ColumnPostponed: ViewObject = new ViewObject(8, 'Postponed', '$Views_ReportCurrentTasksByDepartment_Postponed');

  /**
   * ID:9
   * Alias: PostponedDelayed
   * Caption: $Views_ReportCurrentTasksByDepartment_PostponedDelayed.
   */
   readonly ColumnPostponedDelayed: ViewObject = new ViewObject(9, 'PostponedDelayed', '$Views_ReportCurrentTasksByDepartment_PostponedDelayed');

  /**
   * ID:10
   * Alias: PostponedAvgDelayPeriod
   * Caption: $Views_ReportCurrentTasksByDepartment_PostponedAvgDelayPeriod.
   */
   readonly ColumnPostponedAvgDelayPeriod: ViewObject = new ViewObject(10, 'PostponedAvgDelayPeriod', '$Views_ReportCurrentTasksByDepartment_PostponedAvgDelayPeriod');

  /**
   * ID:11
   * Alias: Total
   * Caption: $Views_ReportCurrentTasksByDepartment_Total.
   */
   readonly ColumnTotal: ViewObject = new ViewObject(11, 'Total', '$Views_ReportCurrentTasksByDepartment_Total');

  /**
   * ID:12
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(12, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: EndDate
   * Caption: $Views_ReportCurrentTasksByDepartment_EndDate_Param.
   */
   readonly ParamEndDate: ViewObject = new ViewObject(0, 'EndDate', '$Views_ReportCurrentTasksByDepartment_EndDate_Param');

  /**
   * ID:1
   * Alias: CreationDate
   * Caption: $Views_ReportCurrentTasksByDepartment_CreationDate.
   */
   readonly ParamCreationDate: ViewObject = new ViewObject(1, 'CreationDate', '$Views_ReportCurrentTasksByDepartment_CreationDate');

  /**
   * ID:2
   * Alias: TypeParam
   * Caption: $Views_ReportCurrentTasksByDepartment_Type_Param.
   */
   readonly ParamTypeParam: ViewObject = new ViewObject(2, 'TypeParam', '$Views_ReportCurrentTasksByDepartment_Type_Param');

  /**
   * ID:3
   * Alias: TaskType
   * Caption: $Views_ReportCurrentTasksByDepartment_TaskType_Param.
   */
   readonly ParamTaskType: ViewObject = new ViewObject(3, 'TaskType', '$Views_ReportCurrentTasksByDepartment_TaskType_Param');

  /**
   * ID:4
   * Alias: Department
   * Caption: $Views_ReportCurrentTasksByDepartment_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(4, 'Department', '$Views_ReportCurrentTasksByDepartment_Department_Param');

  /**
   * ID:5
   * Alias: SelUser
   * Caption: $Views_ReportCurrentTasksByDepartment_User_Param.
   */
   readonly ParamSelUser: ViewObject = new ViewObject(5, 'SelUser', '$Views_ReportCurrentTasksByDepartment_User_Param');

  /**
   * ID:6
   * Alias: Role
   * Caption: $Views_CompletedTasks_RoleGroup_Param.
   */
   readonly ParamRole: ViewObject = new ViewObject(6, 'Role', '$Views_CompletedTasks_RoleGroup_Param');

  /**
   * ID:7
   * Alias: HideTotal
   * Caption: HideTotal.
   */
   readonly ParamHideTotal: ViewObject = new ViewObject(7, 'HideTotal', 'HideTotal');

  /**
   * ID:8
   * Alias: FunctionRolePerformerParam
   * Caption: $Views_MyTasks_FunctionRole_Performer_Param.
   */
   readonly ParamFunctionRolePerformerParam: ViewObject = new ViewObject(8, 'FunctionRolePerformerParam', '$Views_MyTasks_FunctionRole_Performer_Param');

  //#endregion
}

//#endregion

//#region ReportCurrentTasksByDepUnpivoted

/**
 * ID: {d36bf57a-20f1-4d77-8884-0d1465333380}
 * Alias: ReportCurrentTasksByDepUnpivoted
 * Caption: $Views_Names_ReportCurrentTasksByDepUnpivoted
 * Group: System
 */
class ReportCurrentTasksByDepUnpivotedViewInfo {
  //#region Common

  /**
   * View identifier for "ReportCurrentTasksByDepUnpivoted": {d36bf57a-20f1-4d77-8884-0d1465333380}.
   */
   readonly ID: guid = 'd36bf57a-20f1-4d77-8884-0d1465333380';

  /**
   * View name for "ReportCurrentTasksByDepUnpivoted".
   */
   readonly Alias: string = 'ReportCurrentTasksByDepUnpivoted';

  /**
   * View caption for "ReportCurrentTasksByDepUnpivoted".
   */
   readonly Caption: string = '$Views_Names_ReportCurrentTasksByDepUnpivoted';

  /**
   * View group for "ReportCurrentTasksByDepUnpivoted".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DeptID.
   */
   readonly ColumnDeptID: ViewObject = new ViewObject(0, 'DeptID');

  /**
   * ID:1
   * Alias: DeptName
   * Caption: $Views_ReportCurrentTasksByDepartment_Department.
   */
   readonly ColumnDeptName: ViewObject = new ViewObject(1, 'DeptName', '$Views_ReportCurrentTasksByDepartment_Department');

  /**
   * ID:2
   * Alias: Column
   * Caption: $Views_ReportCurrentTasksByDepartment_Column.
   */
   readonly ColumnColumn: ViewObject = new ViewObject(2, 'Column', '$Views_ReportCurrentTasksByDepartment_Column');

  /**
   * ID:3
   * Alias: Value
   * Caption: $Views_ReportCurrentTasksByDepartment_Value.
   */
   readonly ColumnValue: ViewObject = new ViewObject(3, 'Value', '$Views_ReportCurrentTasksByDepartment_Value');

  /**
   * ID:4
   * Alias: StateID.
   */
   readonly ColumnStateID: ViewObject = new ViewObject(4, 'StateID');

  /**
   * ID:5
   * Alias: DelayIndex.
   */
   readonly ColumnDelayIndex: ViewObject = new ViewObject(5, 'DelayIndex');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: EndDate
   * Caption: $Views_ReportCurrentTasksByDepartment_EndDate_Param.
   */
   readonly ParamEndDate: ViewObject = new ViewObject(0, 'EndDate', '$Views_ReportCurrentTasksByDepartment_EndDate_Param');

  /**
   * ID:1
   * Alias: CreationDate
   * Caption: $Views_ReportCurrentTasksByDepartment_CreationDate.
   */
   readonly ParamCreationDate: ViewObject = new ViewObject(1, 'CreationDate', '$Views_ReportCurrentTasksByDepartment_CreationDate');

  /**
   * ID:2
   * Alias: TypeParam
   * Caption: $Views_ReportCurrentTasksByDepartment_Type_Param.
   */
   readonly ParamTypeParam: ViewObject = new ViewObject(2, 'TypeParam', '$Views_ReportCurrentTasksByDepartment_Type_Param');

  /**
   * ID:3
   * Alias: TaskType
   * Caption: $Views_ReportCurrentTasksByDepartment_TaskType_Param.
   */
   readonly ParamTaskType: ViewObject = new ViewObject(3, 'TaskType', '$Views_ReportCurrentTasksByDepartment_TaskType_Param');

  /**
   * ID:4
   * Alias: Department
   * Caption: $Views_ReportCurrentTasksByDepartment_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(4, 'Department', '$Views_ReportCurrentTasksByDepartment_Department_Param');

  /**
   * ID:5
   * Alias: SelUser
   * Caption: $Views_ReportCurrentTasksByDepartment_User_Param.
   */
   readonly ParamSelUser: ViewObject = new ViewObject(5, 'SelUser', '$Views_ReportCurrentTasksByDepartment_User_Param');

  /**
   * ID:6
   * Alias: Role
   * Caption: $Views_CompletedTasks_RoleGroup_Param.
   */
   readonly ParamRole: ViewObject = new ViewObject(6, 'Role', '$Views_CompletedTasks_RoleGroup_Param');

  /**
   * ID:7
   * Alias: NoAvg
   * Caption: NoAvg.
   */
   readonly ParamNoAvg: ViewObject = new ViewObject(7, 'NoAvg', 'NoAvg');

  /**
   * ID:8
   * Alias: FunctionRolePerformerParam
   * Caption: $Views_MyTasks_FunctionRole_Performer_Param.
   */
   readonly ParamFunctionRolePerformerParam: ViewObject = new ViewObject(8, 'FunctionRolePerformerParam', '$Views_MyTasks_FunctionRole_Performer_Param');

  //#endregion
}

//#endregion

//#region ReportCurrentTasksByUser

/**
 * ID: {871ad32d-0846-448c-beb2-85c1d357177b}
 * Alias: ReportCurrentTasksByUser
 * Caption: $Views_Names_ReportCurrentTasksByUser
 * Group: System
 */
class ReportCurrentTasksByUserViewInfo {
  //#region Common

  /**
   * View identifier for "ReportCurrentTasksByUser": {871ad32d-0846-448c-beb2-85c1d357177b}.
   */
   readonly ID: guid = '871ad32d-0846-448c-beb2-85c1d357177b';

  /**
   * View name for "ReportCurrentTasksByUser".
   */
   readonly Alias: string = 'ReportCurrentTasksByUser';

  /**
   * View caption for "ReportCurrentTasksByUser".
   */
   readonly Caption: string = '$Views_Names_ReportCurrentTasksByUser';

  /**
   * View group for "ReportCurrentTasksByUser".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(0, 'UserID');

  /**
   * ID:1
   * Alias: UserName
   * Caption: $Views_ReportCurrentTasksByUser_User.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(1, 'UserName', '$Views_ReportCurrentTasksByUser_User');

  /**
   * ID:2
   * Alias: New
   * Caption: $Views_ReportCurrentTasksByUser_New.
   */
   readonly ColumnNew: ViewObject = new ViewObject(2, 'New', '$Views_ReportCurrentTasksByUser_New');

  /**
   * ID:3
   * Alias: NewDelayed
   * Caption: $Views_ReportCurrentTasksByUser_NewDelayed.
   */
   readonly ColumnNewDelayed: ViewObject = new ViewObject(3, 'NewDelayed', '$Views_ReportCurrentTasksByUser_NewDelayed');

  /**
   * ID:4
   * Alias: NewAvgDelayPeriod
   * Caption: $Views_ReportCurrentTasksByUser_NewAvgDelayPeriod.
   */
   readonly ColumnNewAvgDelayPeriod: ViewObject = new ViewObject(4, 'NewAvgDelayPeriod', '$Views_ReportCurrentTasksByUser_NewAvgDelayPeriod');

  /**
   * ID:5
   * Alias: InWork
   * Caption: $Views_ReportCurrentTasksByUser_InWork.
   */
   readonly ColumnInWork: ViewObject = new ViewObject(5, 'InWork', '$Views_ReportCurrentTasksByUser_InWork');

  /**
   * ID:6
   * Alias: InWorkDelayed
   * Caption: $Views_ReportCurrentTasksByUser_InWorkDelayed.
   */
   readonly ColumnInWorkDelayed: ViewObject = new ViewObject(6, 'InWorkDelayed', '$Views_ReportCurrentTasksByUser_InWorkDelayed');

  /**
   * ID:7
   * Alias: InWorkAvgDelayPeriod
   * Caption: $Views_ReportCurrentTasksByUser_InWorkAvgDelayPeriod.
   */
   readonly ColumnInWorkAvgDelayPeriod: ViewObject = new ViewObject(7, 'InWorkAvgDelayPeriod', '$Views_ReportCurrentTasksByUser_InWorkAvgDelayPeriod');

  /**
   * ID:8
   * Alias: Postponed
   * Caption: $Views_ReportCurrentTasksByUser_Postponed.
   */
   readonly ColumnPostponed: ViewObject = new ViewObject(8, 'Postponed', '$Views_ReportCurrentTasksByUser_Postponed');

  /**
   * ID:9
   * Alias: PostponedDelayed
   * Caption: $Views_ReportCurrentTasksByUser_PostponedDelayed.
   */
   readonly ColumnPostponedDelayed: ViewObject = new ViewObject(9, 'PostponedDelayed', '$Views_ReportCurrentTasksByUser_PostponedDelayed');

  /**
   * ID:10
   * Alias: PostponedAvgDelayPeriod
   * Caption: $Views_ReportCurrentTasksByUser_PostponedAvgDelayPeriod.
   */
   readonly ColumnPostponedAvgDelayPeriod: ViewObject = new ViewObject(10, 'PostponedAvgDelayPeriod', '$Views_ReportCurrentTasksByUser_PostponedAvgDelayPeriod');

  /**
   * ID:11
   * Alias: Total
   * Caption: $Views_ReportCurrentTasksByUser_Total.
   */
   readonly ColumnTotal: ViewObject = new ViewObject(11, 'Total', '$Views_ReportCurrentTasksByUser_Total');

  /**
   * ID:12
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(12, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: EndDate
   * Caption: $Views_ReportCurrentTasksByUser_EndDate_Param.
   */
   readonly ParamEndDate: ViewObject = new ViewObject(0, 'EndDate', '$Views_ReportCurrentTasksByUser_EndDate_Param');

  /**
   * ID:1
   * Alias: CreationDate
   * Caption: $Views_ReportCurrentTasksByUser_CreationDate_Param.
   */
   readonly ParamCreationDate: ViewObject = new ViewObject(1, 'CreationDate', '$Views_ReportCurrentTasksByUser_CreationDate_Param');

  /**
   * ID:2
   * Alias: TypeParam
   * Caption: $Views_ReportCurrentTasksByUser_Type_Param.
   */
   readonly ParamTypeParam: ViewObject = new ViewObject(2, 'TypeParam', '$Views_ReportCurrentTasksByUser_Type_Param');

  /**
   * ID:3
   * Alias: TaskType
   * Caption: $Views_ReportCurrentTasksByUser_TaskType_Param.
   */
   readonly ParamTaskType: ViewObject = new ViewObject(3, 'TaskType', '$Views_ReportCurrentTasksByUser_TaskType_Param');

  /**
   * ID:4
   * Alias: Department
   * Caption: $Views_ReportCurrentTasksByUser_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(4, 'Department', '$Views_ReportCurrentTasksByUser_Department_Param');

  /**
   * ID:5
   * Alias: SelUser
   * Caption: $Views_ReportCurrentTasksByUser_User_Param.
   */
   readonly ParamSelUser: ViewObject = new ViewObject(5, 'SelUser', '$Views_ReportCurrentTasksByUser_User_Param');

  /**
   * ID:6
   * Alias: Role
   * Caption: $Views_CompletedTasks_RoleGroup_Param.
   */
   readonly ParamRole: ViewObject = new ViewObject(6, 'Role', '$Views_CompletedTasks_RoleGroup_Param');

  /**
   * ID:7
   * Alias: FunctionRolePerformerParam
   * Caption: $Views_MyTasks_FunctionRole_Performer_Param.
   */
   readonly ParamFunctionRolePerformerParam: ViewObject = new ViewObject(7, 'FunctionRolePerformerParam', '$Views_MyTasks_FunctionRole_Performer_Param');

  //#endregion
}

//#endregion

//#region ReportCurrentTasksRules

/**
 * ID: {146b25b3-e61e-4259-bcc5-959040755de6}
 * Alias: ReportCurrentTasksRules
 * Caption: $Views_Names_ReportCurrentTasksRules
 * Group: System
 */
class ReportCurrentTasksRulesViewInfo {
  //#region Common

  /**
   * View identifier for "ReportCurrentTasksRules": {146b25b3-e61e-4259-bcc5-959040755de6}.
   */
   readonly ID: guid = '146b25b3-e61e-4259-bcc5-959040755de6';

  /**
   * View name for "ReportCurrentTasksRules".
   */
   readonly Alias: string = 'ReportCurrentTasksRules';

  /**
   * View caption for "ReportCurrentTasksRules".
   */
   readonly Caption: string = '$Views_Names_ReportCurrentTasksRules';

  /**
   * View group for "ReportCurrentTasksRules".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RuleID.
   */
   readonly ColumnRuleID: ViewObject = new ViewObject(0, 'RuleID');

  /**
   * ID:1
   * Alias: RuleCaption
   * Caption: $Views_ReportCurrentTasksRules_Caption.
   */
   readonly ColumnRuleCaption: ViewObject = new ViewObject(1, 'RuleCaption', '$Views_ReportCurrentTasksRules_Caption');

  /**
   * ID:2
   * Alias: ActiveRoles
   * Caption: $Views_ReportCurrentTasksRules_ActiveRoles.
   */
   readonly ColumnActiveRoles: ViewObject = new ViewObject(2, 'ActiveRoles', '$Views_ReportCurrentTasksRules_ActiveRoles');

  /**
   * ID:3
   * Alias: PassiveRoles
   * Caption: $Views_ReportCurrentTasksRules_PassiveRoles.
   */
   readonly ColumnPassiveRoles: ViewObject = new ViewObject(3, 'PassiveRoles', '$Views_ReportCurrentTasksRules_PassiveRoles');

  /**
   * ID:4
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(4, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_ReportCurrentTasksRules_Caption_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_ReportCurrentTasksRules_Caption_Param');

  //#endregion
}

//#endregion

//#region ReportDocumentsByType

/**
 * ID: {35ebb779-0abd-411b-bc6b-358d2e2cabca}
 * Alias: ReportDocumentsByType
 * Caption: $Views_Names_ReportDocumentsByType
 * Group: System
 */
class ReportDocumentsByTypeViewInfo {
  //#region Common

  /**
   * View identifier for "ReportDocumentsByType": {35ebb779-0abd-411b-bc6b-358d2e2cabca}.
   */
   readonly ID: guid = '35ebb779-0abd-411b-bc6b-358d2e2cabca';

  /**
   * View name for "ReportDocumentsByType".
   */
   readonly Alias: string = 'ReportDocumentsByType';

  /**
   * View caption for "ReportDocumentsByType".
   */
   readonly Caption: string = '$Views_Names_ReportDocumentsByType';

  /**
   * View group for "ReportDocumentsByType".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeName
   * Caption: $Views_ReportDocumentsByType_TypeName.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(1, 'TypeName', '$Views_ReportDocumentsByType_TypeName');

  /**
   * ID:2
   * Alias: Total
   * Caption: $Views_ReportDocumentsByType_Total.
   */
   readonly ColumnTotal: ViewObject = new ViewObject(2, 'Total', '$Views_ReportDocumentsByType_Total');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: DocDate
   * Caption: $Views_ReportDocumentsByType_DocDate_Param.
   */
   readonly ParamDocDate: ViewObject = new ViewObject(0, 'DocDate', '$Views_ReportDocumentsByType_DocDate_Param');

  //#endregion
}

//#endregion

//#region ReportPastTasksByDepartment

/**
 * ID: {49562bb2-b059-4f06-9771-c5e38892ff6f}
 * Alias: ReportPastTasksByDepartment
 * Caption: $Views_Names_ReportPastTasksByDepartment
 * Group: System
 */
class ReportPastTasksByDepartmentViewInfo {
  //#region Common

  /**
   * View identifier for "ReportPastTasksByDepartment": {49562bb2-b059-4f06-9771-c5e38892ff6f}.
   */
   readonly ID: guid = '49562bb2-b059-4f06-9771-c5e38892ff6f';

  /**
   * View name for "ReportPastTasksByDepartment".
   */
   readonly Alias: string = 'ReportPastTasksByDepartment';

  /**
   * View caption for "ReportPastTasksByDepartment".
   */
   readonly Caption: string = '$Views_Names_ReportPastTasksByDepartment';

  /**
   * View group for "ReportPastTasksByDepartment".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: DeptID.
   */
   readonly ColumnDeptID: ViewObject = new ViewObject(0, 'DeptID');

  /**
   * ID:1
   * Alias: DeptName
   * Caption: $Views_ReportPastTasksByDepartment_Department.
   */
   readonly ColumnDeptName: ViewObject = new ViewObject(1, 'DeptName', '$Views_ReportPastTasksByDepartment_Department');

  /**
   * ID:2
   * Alias: OnTime
   * Caption: $Views_ReportPastTasksByDepartment_OnTime.
   */
   readonly ColumnOnTime: ViewObject = new ViewObject(2, 'OnTime', '$Views_ReportPastTasksByDepartment_OnTime');

  /**
   * ID:3
   * Alias: Overdue
   * Caption: $Views_ReportPastTasksByDepartment_Overdue.
   */
   readonly ColumnOverdue: ViewObject = new ViewObject(3, 'Overdue', '$Views_ReportPastTasksByDepartment_Overdue');

  /**
   * ID:4
   * Alias: OverdueAvgDelayPeriod
   * Caption: $Views_ReportPastTasksByDepartment_OverdueAvgDelayPeriod.
   */
   readonly ColumnOverdueAvgDelayPeriod: ViewObject = new ViewObject(4, 'OverdueAvgDelayPeriod', '$Views_ReportPastTasksByDepartment_OverdueAvgDelayPeriod');

  /**
   * ID:5
   * Alias: Total
   * Caption: $Views_ReportPastTasksByDepartment_Total.
   */
   readonly ColumnTotal: ViewObject = new ViewObject(5, 'Total', '$Views_ReportPastTasksByDepartment_Total');

  /**
   * ID:6
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(6, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CompletionDate
   * Caption: $Views_ReportPastTasksByDepartment_CompletionDate.
   */
   readonly ParamCompletionDate: ViewObject = new ViewObject(0, 'CompletionDate', '$Views_ReportPastTasksByDepartment_CompletionDate');

  /**
   * ID:1
   * Alias: TypeParam
   * Caption: $Views_ReportPastTasksByDepartment_DocType_Param.
   */
   readonly ParamTypeParam: ViewObject = new ViewObject(1, 'TypeParam', '$Views_ReportPastTasksByDepartment_DocType_Param');

  /**
   * ID:2
   * Alias: TaskType
   * Caption: $Views_ReportPastTasksByDepartment_TaskType_Param.
   */
   readonly ParamTaskType: ViewObject = new ViewObject(2, 'TaskType', '$Views_ReportPastTasksByDepartment_TaskType_Param');

  /**
   * ID:3
   * Alias: Department
   * Caption: $Views_ReportPastTasksByDepartment_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(3, 'Department', '$Views_ReportPastTasksByDepartment_Department_Param');

  /**
   * ID:4
   * Alias: SelUser
   * Caption: $Views_ReportPastTasksByDepartment_User_Param.
   */
   readonly ParamSelUser: ViewObject = new ViewObject(4, 'SelUser', '$Views_ReportPastTasksByDepartment_User_Param');

  /**
   * ID:5
   * Alias: Role
   * Caption: $Views_CompletedTasks_RoleGroup_Param.
   */
   readonly ParamRole: ViewObject = new ViewObject(5, 'Role', '$Views_CompletedTasks_RoleGroup_Param');

  /**
   * ID:6
   * Alias: Option
   * Caption: $Views_ReportPastTasksByDepartment_Option_Param.
   */
   readonly ParamOption: ViewObject = new ViewObject(6, 'Option', '$Views_ReportPastTasksByDepartment_Option_Param');

  //#endregion
}

//#endregion

//#region ReportPastTasksByUser

/**
 * ID: {44b3c3ba-6dc4-4b3b-bd1e-48f939c3a8a1}
 * Alias: ReportPastTasksByUser
 * Caption: $Views_Names_ReportPastTasksByUser
 * Group: System
 */
class ReportPastTasksByUserViewInfo {
  //#region Common

  /**
   * View identifier for "ReportPastTasksByUser": {44b3c3ba-6dc4-4b3b-bd1e-48f939c3a8a1}.
   */
   readonly ID: guid = '44b3c3ba-6dc4-4b3b-bd1e-48f939c3a8a1';

  /**
   * View name for "ReportPastTasksByUser".
   */
   readonly Alias: string = 'ReportPastTasksByUser';

  /**
   * View caption for "ReportPastTasksByUser".
   */
   readonly Caption: string = '$Views_Names_ReportPastTasksByUser';

  /**
   * View group for "ReportPastTasksByUser".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(0, 'UserID');

  /**
   * ID:1
   * Alias: UserName
   * Caption: $Views_ReportPastTasksByUser_User.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(1, 'UserName', '$Views_ReportPastTasksByUser_User');

  /**
   * ID:2
   * Alias: OnTime
   * Caption: $Views_ReportPastTasksByUser_OnTime.
   */
   readonly ColumnOnTime: ViewObject = new ViewObject(2, 'OnTime', '$Views_ReportPastTasksByUser_OnTime');

  /**
   * ID:3
   * Alias: Overdue
   * Caption: $Views_ReportPastTasksByUser_Overdue.
   */
   readonly ColumnOverdue: ViewObject = new ViewObject(3, 'Overdue', '$Views_ReportPastTasksByUser_Overdue');

  /**
   * ID:4
   * Alias: OverdueAvgDelayPeriod
   * Caption: $Views_ReportPastTasksByUser_OverdueAvgDelayPeriod.
   */
   readonly ColumnOverdueAvgDelayPeriod: ViewObject = new ViewObject(4, 'OverdueAvgDelayPeriod', '$Views_ReportPastTasksByUser_OverdueAvgDelayPeriod');

  /**
   * ID:5
   * Alias: Total
   * Caption: $Views_ReportPastTasksByUser_Total.
   */
   readonly ColumnTotal: ViewObject = new ViewObject(5, 'Total', '$Views_ReportPastTasksByUser_Total');

  /**
   * ID:6
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(6, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CompletionDate
   * Caption: $Views_ReportPastTasksByUser_CompletionDate_Param.
   */
   readonly ParamCompletionDate: ViewObject = new ViewObject(0, 'CompletionDate', '$Views_ReportPastTasksByUser_CompletionDate_Param');

  /**
   * ID:1
   * Alias: TypeParam
   * Caption: $Views_ReportPastTasksByUser_TypeParam_Param.
   */
   readonly ParamTypeParam: ViewObject = new ViewObject(1, 'TypeParam', '$Views_ReportPastTasksByUser_TypeParam_Param');

  /**
   * ID:2
   * Alias: TaskType
   * Caption: $Views_ReportPastTasksByUser_TaskType_Param.
   */
   readonly ParamTaskType: ViewObject = new ViewObject(2, 'TaskType', '$Views_ReportPastTasksByUser_TaskType_Param');

  /**
   * ID:3
   * Alias: Department
   * Caption: $Views_ReportPastTasksByUser_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(3, 'Department', '$Views_ReportPastTasksByUser_Department_Param');

  /**
   * ID:4
   * Alias: SelUser
   * Caption: $Views_ReportPastTasksByUser_User_Param.
   */
   readonly ParamSelUser: ViewObject = new ViewObject(4, 'SelUser', '$Views_ReportPastTasksByUser_User_Param');

  /**
   * ID:5
   * Alias: Role
   * Caption: $Views_CompletedTasks_RoleGroup_Param.
   */
   readonly ParamRole: ViewObject = new ViewObject(5, 'Role', '$Views_CompletedTasks_RoleGroup_Param');

  /**
   * ID:6
   * Alias: Option
   * Caption: $Views_ReportPastTasksByUser_Option_Param.
   */
   readonly ParamOption: ViewObject = new ViewObject(6, 'Option', '$Views_ReportPastTasksByUser_Option_Param');

  //#endregion
}

//#endregion

//#region RoleDeputies

/**
 * ID: {181eafda-939b-4b38-9f71-21c699133396}
 * Alias: RoleDeputies
 * Caption: $Views_Names_RoleDeputies
 * Group: System
 */
class RoleDeputiesViewInfo {
  //#region Common

  /**
   * View identifier for "RoleDeputies": {181eafda-939b-4b38-9f71-21c699133396}.
   */
   readonly ID: guid = '181eafda-939b-4b38-9f71-21c699133396';

  /**
   * View name for "RoleDeputies".
   */
   readonly Alias: string = 'RoleDeputies';

  /**
   * View caption for "RoleDeputies".
   */
   readonly Caption: string = '$Views_Names_RoleDeputies';

  /**
   * View group for "RoleDeputies".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RoleDeputyID.
   */
   readonly ColumnRoleDeputyID: ViewObject = new ViewObject(0, 'RoleDeputyID');

  /**
   * ID:1
   * Alias: RoleDeputyName
   * Caption: $Views_RoleDeputies_RoleDeputy.
   */
   readonly ColumnRoleDeputyName: ViewObject = new ViewObject(1, 'RoleDeputyName', '$Views_RoleDeputies_RoleDeputy');

  /**
   * ID:2
   * Alias: RoleDeputizedID.
   */
   readonly ColumnRoleDeputizedID: ViewObject = new ViewObject(2, 'RoleDeputizedID');

  /**
   * ID:3
   * Alias: RoleDeputizedName
   * Caption: $Views_RoleDeputies_RoleDeputized.
   */
   readonly ColumnRoleDeputizedName: ViewObject = new ViewObject(3, 'RoleDeputizedName', '$Views_RoleDeputies_RoleDeputized');

  /**
   * ID:4
   * Alias: RoleDeputyFrom
   * Caption: $Views_RoleDeputies_RoleDeputyFrom.
   */
   readonly ColumnRoleDeputyFrom: ViewObject = new ViewObject(4, 'RoleDeputyFrom', '$Views_RoleDeputies_RoleDeputyFrom');

  /**
   * ID:5
   * Alias: RoleDeputyTo
   * Caption: $Views_RoleDeputies_RoleDeputyTo.
   */
   readonly ColumnRoleDeputyTo: ViewObject = new ViewObject(5, 'RoleDeputyTo', '$Views_RoleDeputies_RoleDeputyTo');

  /**
   * ID:6
   * Alias: RoleDeputyIsPermanent
   * Caption: $Views_RoleDeputies_RoleDeputyIsPermanent.
   */
   readonly ColumnRoleDeputyIsPermanent: ViewObject = new ViewObject(6, 'RoleDeputyIsPermanent', '$Views_RoleDeputies_RoleDeputyIsPermanent');

  /**
   * ID:7
   * Alias: RoleDeputyIsEnabled
   * Caption: $Views_RoleDeputies_RoleDeputyAvailable.
   */
   readonly ColumnRoleDeputyIsEnabled: ViewObject = new ViewObject(7, 'RoleDeputyIsEnabled', '$Views_RoleDeputies_RoleDeputyAvailable');

  /**
   * ID:8
   * Alias: RoleDeputyIsActive
   * Caption: $Views_RoleDeputies_RoleDeputyIsActive.
   */
   readonly ColumnRoleDeputyIsActive: ViewObject = new ViewObject(8, 'RoleDeputyIsActive', '$Views_RoleDeputies_RoleDeputyIsActive');

  /**
   * ID:9
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(9, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Role
   * Caption: $Views_RoleDeputies_Role.
   */
   readonly ParamRole: ViewObject = new ViewObject(0, 'Role', '$Views_RoleDeputies_Role');

  /**
   * ID:1
   * Alias: Deputized
   * Caption: $Views_RoleDeputies_RoleDeputized.
   */
   readonly ParamDeputized: ViewObject = new ViewObject(1, 'Deputized', '$Views_RoleDeputies_RoleDeputized');

  /**
   * ID:2
   * Alias: Deputy
   * Caption: $Views_RoleDeputies_RoleDeputy.
   */
   readonly ParamDeputy: ViewObject = new ViewObject(2, 'Deputy', '$Views_RoleDeputies_RoleDeputy');

  /**
   * ID:3
   * Alias: AvailableOnDate
   * Caption: $Views_RoleDeputies_AvailableOnDate.
   */
   readonly ParamAvailableOnDate: ViewObject = new ViewObject(3, 'AvailableOnDate', '$Views_RoleDeputies_AvailableOnDate');

  /**
   * ID:4
   * Alias: IsEnabled
   * Caption: $Views_RoleDeputies_RoleDeputyAvailable.
   */
   readonly ParamIsEnabled: ViewObject = new ViewObject(4, 'IsEnabled', '$Views_RoleDeputies_RoleDeputyAvailable');

  /**
   * ID:5
   * Alias: IsActive
   * Caption: $Views_RoleDeputies_RoleDeputyIsActive.
   */
   readonly ParamIsActive: ViewObject = new ViewObject(5, 'IsActive', '$Views_RoleDeputies_RoleDeputyIsActive');

  //#endregion
}

//#endregion

//#region RoleDeputiesManagementDeputized

/**
 * ID: {2877fb32-b5fa-40ed-94d2-32dcd675afc1}
 * Alias: RoleDeputiesManagementDeputized
 * Caption: $Views_Names_RoleDeputiesManagementDeputized
 * Group: System
 */
class RoleDeputiesManagementDeputizedViewInfo {
  //#region Common

  /**
   * View identifier for "RoleDeputiesManagementDeputized": {2877fb32-b5fa-40ed-94d2-32dcd675afc1}.
   */
   readonly ID: guid = '2877fb32-b5fa-40ed-94d2-32dcd675afc1';

  /**
   * View name for "RoleDeputiesManagementDeputized".
   */
   readonly Alias: string = 'RoleDeputiesManagementDeputized';

  /**
   * View caption for "RoleDeputiesManagementDeputized".
   */
   readonly Caption: string = '$Views_Names_RoleDeputiesManagementDeputized';

  /**
   * View group for "RoleDeputiesManagementDeputized".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RoleDeputizedRoles
   * Caption: $Views_RoleDeputiesManagementDeputized_Roles.
   */
   readonly ColumnRoleDeputizedRoles: ViewObject = new ViewObject(0, 'RoleDeputizedRoles', '$Views_RoleDeputiesManagementDeputized_Roles');

  /**
   * ID:1
   * Alias: RoleDeputizedID.
   */
   readonly ColumnRoleDeputizedID: ViewObject = new ViewObject(1, 'RoleDeputizedID');

  /**
   * ID:2
   * Alias: RoleDeputizedName
   * Caption: $Views_RoleDeputies_RoleDeputized.
   */
   readonly ColumnRoleDeputizedName: ViewObject = new ViewObject(2, 'RoleDeputizedName', '$Views_RoleDeputies_RoleDeputized');

  /**
   * ID:3
   * Alias: RoleDeputyFrom
   * Caption: $Views_RoleDeputies_RoleDeputyFrom.
   */
   readonly ColumnRoleDeputyFrom: ViewObject = new ViewObject(3, 'RoleDeputyFrom', '$Views_RoleDeputies_RoleDeputyFrom');

  /**
   * ID:4
   * Alias: RoleDeputyTo
   * Caption: $Views_RoleDeputies_RoleDeputyTo.
   */
   readonly ColumnRoleDeputyTo: ViewObject = new ViewObject(4, 'RoleDeputyTo', '$Views_RoleDeputies_RoleDeputyTo');

  /**
   * ID:5
   * Alias: RoleDeputyIsPermanent
   * Caption: $Views_RoleDeputies_RoleDeputyIsPermanent.
   */
   readonly ColumnRoleDeputyIsPermanent: ViewObject = new ViewObject(5, 'RoleDeputyIsPermanent', '$Views_RoleDeputies_RoleDeputyIsPermanent');

  /**
   * ID:6
   * Alias: RoleDeputyIsEnabled
   * Caption: $Views_RoleDeputies_RoleDeputyAvailable.
   */
   readonly ColumnRoleDeputyIsEnabled: ViewObject = new ViewObject(6, 'RoleDeputyIsEnabled', '$Views_RoleDeputies_RoleDeputyAvailable');

  /**
   * ID:7
   * Alias: RoleDeputyIsActive
   * Caption: $Views_RoleDeputies_RoleDeputyIsActive.
   */
   readonly ColumnRoleDeputyIsActive: ViewObject = new ViewObject(7, 'RoleDeputyIsActive', '$Views_RoleDeputies_RoleDeputyIsActive');

  /**
   * ID:8
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(8, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Deputized
   * Caption: $Views_RoleDeputies_RoleDeputized.
   */
   readonly ParamDeputized: ViewObject = new ViewObject(0, 'Deputized', '$Views_RoleDeputies_RoleDeputized');

  /**
   * ID:1
   * Alias: Deputy
   * Caption: $Views_RoleDeputies_RoleDeputy.
   */
   readonly ParamDeputy: ViewObject = new ViewObject(1, 'Deputy', '$Views_RoleDeputies_RoleDeputy');

  /**
   * ID:2
   * Alias: AvailableOnDate
   * Caption: $Views_RoleDeputies_AvailableOnDate.
   */
   readonly ParamAvailableOnDate: ViewObject = new ViewObject(2, 'AvailableOnDate', '$Views_RoleDeputies_AvailableOnDate');

  /**
   * ID:3
   * Alias: IsEnabled
   * Caption: $Views_RoleDeputies_RoleDeputyAvailable.
   */
   readonly ParamIsEnabled: ViewObject = new ViewObject(3, 'IsEnabled', '$Views_RoleDeputies_RoleDeputyAvailable');

  /**
   * ID:4
   * Alias: IsActive
   * Caption: $Views_RoleDeputies_RoleDeputyIsActive.
   */
   readonly ParamIsActive: ViewObject = new ViewObject(4, 'IsActive', '$Views_RoleDeputies_RoleDeputyIsActive');

  /**
   * ID:5
   * Alias: CardType
   * Caption: $Views_RoleDeputies_CardType.
   */
   readonly ParamCardType: ViewObject = new ViewObject(5, 'CardType', '$Views_RoleDeputies_CardType');

  //#endregion
}

//#endregion

//#region RoleDeputiesNew

/**
 * ID: {87c8a806-31fc-4d45-b6a0-cb1028d9fc24}
 * Alias: RoleDeputiesNew
 * Caption: $Views_Names_RoleDeputiesNew
 * Group: System
 */
class RoleDeputiesNewViewInfo {
  //#region Common

  /**
   * View identifier for "RoleDeputiesNew": {87c8a806-31fc-4d45-b6a0-cb1028d9fc24}.
   */
   readonly ID: guid = '87c8a806-31fc-4d45-b6a0-cb1028d9fc24';

  /**
   * View name for "RoleDeputiesNew".
   */
   readonly Alias: string = 'RoleDeputiesNew';

  /**
   * View caption for "RoleDeputiesNew".
   */
   readonly Caption: string = '$Views_Names_RoleDeputiesNew';

  /**
   * View group for "RoleDeputiesNew".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RoleDeputyID.
   */
   readonly ColumnRoleDeputyID: ViewObject = new ViewObject(0, 'RoleDeputyID');

  /**
   * ID:1
   * Alias: RoleDeputyName
   * Caption: $Views_RoleDeputies_RoleDeputy.
   */
   readonly ColumnRoleDeputyName: ViewObject = new ViewObject(1, 'RoleDeputyName', '$Views_RoleDeputies_RoleDeputy');

  /**
   * ID:2
   * Alias: RoleDeputizedID.
   */
   readonly ColumnRoleDeputizedID: ViewObject = new ViewObject(2, 'RoleDeputizedID');

  /**
   * ID:3
   * Alias: RoleDeputizedName
   * Caption: $Views_RoleDeputies_RoleDeputized.
   */
   readonly ColumnRoleDeputizedName: ViewObject = new ViewObject(3, 'RoleDeputizedName', '$Views_RoleDeputies_RoleDeputized');

  /**
   * ID:4
   * Alias: RoleDeputyFrom
   * Caption: $Views_RoleDeputies_RoleDeputyFrom.
   */
   readonly ColumnRoleDeputyFrom: ViewObject = new ViewObject(4, 'RoleDeputyFrom', '$Views_RoleDeputies_RoleDeputyFrom');

  /**
   * ID:5
   * Alias: RoleDeputyTo
   * Caption: $Views_RoleDeputies_RoleDeputyTo.
   */
   readonly ColumnRoleDeputyTo: ViewObject = new ViewObject(5, 'RoleDeputyTo', '$Views_RoleDeputies_RoleDeputyTo');

  /**
   * ID:6
   * Alias: RoleDeputyIsPermanent
   * Caption: $Views_RoleDeputies_RoleDeputyIsPermanent.
   */
   readonly ColumnRoleDeputyIsPermanent: ViewObject = new ViewObject(6, 'RoleDeputyIsPermanent', '$Views_RoleDeputies_RoleDeputyIsPermanent');

  /**
   * ID:7
   * Alias: RoleDeputyIsEnabled
   * Caption: $Views_RoleDeputies_RoleDeputyAvailable.
   */
   readonly ColumnRoleDeputyIsEnabled: ViewObject = new ViewObject(7, 'RoleDeputyIsEnabled', '$Views_RoleDeputies_RoleDeputyAvailable');

  /**
   * ID:8
   * Alias: RoleDeputyIsActive
   * Caption: $Views_RoleDeputies_RoleDeputyIsActive.
   */
   readonly ColumnRoleDeputyIsActive: ViewObject = new ViewObject(8, 'RoleDeputyIsActive', '$Views_RoleDeputies_RoleDeputyIsActive');

  /**
   * ID:9
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(9, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Role
   * Caption: $Views_RoleDeputies_Role.
   */
   readonly ParamRole: ViewObject = new ViewObject(0, 'Role', '$Views_RoleDeputies_Role');

  /**
   * ID:1
   * Alias: Deputized
   * Caption: $Views_RoleDeputies_RoleDeputized.
   */
   readonly ParamDeputized: ViewObject = new ViewObject(1, 'Deputized', '$Views_RoleDeputies_RoleDeputized');

  /**
   * ID:2
   * Alias: Deputy
   * Caption: $Views_RoleDeputies_RoleDeputy.
   */
   readonly ParamDeputy: ViewObject = new ViewObject(2, 'Deputy', '$Views_RoleDeputies_RoleDeputy');

  /**
   * ID:3
   * Alias: AvailableOnDate
   * Caption: $Views_RoleDeputies_AvailableOnDate.
   */
   readonly ParamAvailableOnDate: ViewObject = new ViewObject(3, 'AvailableOnDate', '$Views_RoleDeputies_AvailableOnDate');

  /**
   * ID:4
   * Alias: IsEnabled
   * Caption: $Views_RoleDeputies_RoleDeputyAvailable.
   */
   readonly ParamIsEnabled: ViewObject = new ViewObject(4, 'IsEnabled', '$Views_RoleDeputies_RoleDeputyAvailable');

  /**
   * ID:5
   * Alias: IsActive
   * Caption: $Views_RoleDeputies_RoleDeputyIsActive.
   */
   readonly ParamIsActive: ViewObject = new ViewObject(5, 'IsActive', '$Views_RoleDeputies_RoleDeputyIsActive');

  //#endregion
}

//#endregion

//#region RoleGenerators

/**
 * ID: {e8163ca6-19e3-09ce-bb88-efcac757a8f4}
 * Alias: RoleGenerators
 * Caption: $Views_Names_RoleGenerators
 * Group: System
 */
class RoleGeneratorsViewInfo {
  //#region Common

  /**
   * View identifier for "RoleGenerators": {e8163ca6-19e3-09ce-bb88-efcac757a8f4}.
   */
   readonly ID: guid = 'e8163ca6-19e3-09ce-bb88-efcac757a8f4';

  /**
   * View name for "RoleGenerators".
   */
   readonly Alias: string = 'RoleGenerators';

  /**
   * View caption for "RoleGenerators".
   */
   readonly Caption: string = '$Views_Names_RoleGenerators';

  /**
   * View group for "RoleGenerators".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: GeneratorID.
   */
   readonly ColumnGeneratorID: ViewObject = new ViewObject(0, 'GeneratorID');

  /**
   * ID:1
   * Alias: GeneratorName
   * Caption: $Views_RoleGenerators_Name.
   */
   readonly ColumnGeneratorName: ViewObject = new ViewObject(1, 'GeneratorName', '$Views_RoleGenerators_Name');

  /**
   * ID:2
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(2, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_RoleGenerators_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_RoleGenerators_Name_Param');

  //#endregion
}

//#endregion

//#region Roles

/**
 * ID: {e168749e-d123-4833-a820-77c9dae80f05}
 * Alias: Roles
 * Caption: $Views_Names_Roles
 * Group: System
 */
class RolesViewInfo {
  //#region Common

  /**
   * View identifier for "Roles": {e168749e-d123-4833-a820-77c9dae80f05}.
   */
   readonly ID: guid = 'e168749e-d123-4833-a820-77c9dae80f05';

  /**
   * View name for "Roles".
   */
   readonly Alias: string = 'Roles';

  /**
   * View caption for "Roles".
   */
   readonly Caption: string = '$Views_Names_Roles';

  /**
   * View group for "Roles".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RoleID.
   */
   readonly ColumnRoleID: ViewObject = new ViewObject(0, 'RoleID');

  /**
   * ID:1
   * Alias: RoleName
   * Caption: $Views_Roles_Role.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(1, 'RoleName', '$Views_Roles_Role');

  /**
   * ID:2
   * Alias: TypeName
   * Caption: $Views_Roles_Type.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(2, 'TypeName', '$Views_Roles_Type');

  /**
   * ID:3
   * Alias: Info
   * Caption: $Views_Roles_Info.
   */
   readonly ColumnInfo: ViewObject = new ViewObject(3, 'Info', '$Views_Roles_Info');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Roles_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Roles_Name_Param');

  /**
   * ID:1
   * Alias: TypeID
   * Caption: $Views_Roles_Type_Param.
   */
   readonly ParamTypeID: ViewObject = new ViewObject(1, 'TypeID', '$Views_Roles_Type_Param');

  /**
   * ID:2
   * Alias: GeneratorID
   * Caption: $Views_Roles_Generator_Param.
   */
   readonly ParamGeneratorID: ViewObject = new ViewObject(2, 'GeneratorID', '$Views_Roles_Generator_Param');

  /**
   * ID:3
   * Alias: IsStaticRole
   * Caption: Is static role.
   */
   readonly ParamIsStaticRole: ViewObject = new ViewObject(3, 'IsStaticRole', 'Is static role');

  /**
   * ID:4
   * Alias: ShowHidden
   * Caption: $Views_Roles_ShowHidden_Param.
   */
   readonly ParamShowHidden: ViewObject = new ViewObject(4, 'ShowHidden', '$Views_Roles_ShowHidden_Param');

  //#endregion
}

//#endregion

//#region RoleTypes

/**
 * ID: {df92983b-2dd3-4092-8603-48d0245cd049}
 * Alias: RoleTypes
 * Caption: $Views_Names_RoleTypes
 * Group: System
 */
class RoleTypesViewInfo {
  //#region Common

  /**
   * View identifier for "RoleTypes": {df92983b-2dd3-4092-8603-48d0245cd049}.
   */
   readonly ID: guid = 'df92983b-2dd3-4092-8603-48d0245cd049';

  /**
   * View name for "RoleTypes".
   */
   readonly Alias: string = 'RoleTypes';

  /**
   * View caption for "RoleTypes".
   */
   readonly Caption: string = '$Views_Names_RoleTypes';

  /**
   * View group for "RoleTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RoleTypeID
   * Caption: Id типа роли.
   */
   readonly ColumnRoleTypeID: ViewObject = new ViewObject(0, 'RoleTypeID', 'Id типа роли');

  /**
   * ID:1
   * Alias: RoleTypeName
   * Caption: $Views_RoleTypes_Name.
   */
   readonly ColumnRoleTypeName: ViewObject = new ViewObject(1, 'RoleTypeName', '$Views_RoleTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: RoleTypeNameParam
   * Caption: $Views_RoleTypes_Name_Param.
   */
   readonly ParamRoleTypeNameParam: ViewObject = new ViewObject(0, 'RoleTypeNameParam', '$Views_RoleTypes_Name_Param');

  //#endregion
}

//#endregion

//#region Sequences

/**
 * ID: {b712a7be-2796-4986-b226-8bbcb26d1648}
 * Alias: Sequences
 * Caption: $Views_Names_Sequences
 * Group: System
 */
class SequencesViewInfo {
  //#region Common

  /**
   * View identifier for "Sequences": {b712a7be-2796-4986-b226-8bbcb26d1648}.
   */
   readonly ID: guid = 'b712a7be-2796-4986-b226-8bbcb26d1648';

  /**
   * View name for "Sequences".
   */
   readonly Alias: string = 'Sequences';

  /**
   * View caption for "Sequences".
   */
   readonly Caption: string = '$Views_Names_Sequences';

  /**
   * View group for "Sequences".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SequenceID.
   */
   readonly ColumnSequenceID: ViewObject = new ViewObject(0, 'SequenceID');

  /**
   * ID:1
   * Alias: SequenceName
   * Caption: $Views_Sequences_Name.
   */
   readonly ColumnSequenceName: ViewObject = new ViewObject(1, 'SequenceName', '$Views_Sequences_Name');

  /**
   * ID:2
   * Alias: Created
   * Caption: $Views_Sequences_Created.
   */
   readonly ColumnCreated: ViewObject = new ViewObject(2, 'Created', '$Views_Sequences_Created');

  /**
   * ID:3
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(3, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Sequences_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Sequences_Name_Param');

  /**
   * ID:1
   * Alias: Created
   * Caption: $Views_Sequences_Created_Param.
   */
   readonly ParamCreated: ViewObject = new ViewObject(1, 'Created', '$Views_Sequences_Created_Param');

  //#endregion
}

//#endregion

//#region Sessions

/**
 * ID: {567e7c41-536e-47af-8c2d-b6d8cb6b64fb}
 * Alias: Sessions
 * Caption: $Views_Names_Sessions
 * Group: System
 */
class SessionsViewInfo {
  //#region Common

  /**
   * View identifier for "Sessions": {567e7c41-536e-47af-8c2d-b6d8cb6b64fb}.
   */
   readonly ID: guid = '567e7c41-536e-47af-8c2d-b6d8cb6b64fb';

  /**
   * View name for "Sessions".
   */
   readonly Alias: string = 'Sessions';

  /**
   * View caption for "Sessions".
   */
   readonly Caption: string = '$Views_Names_Sessions';

  /**
   * View group for "Sessions".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SessionID.
   */
   readonly ColumnSessionID: ViewObject = new ViewObject(0, 'SessionID');

  /**
   * ID:1
   * Alias: SessionName.
   */
   readonly ColumnSessionName: ViewObject = new ViewObject(1, 'SessionName');

  /**
   * ID:2
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(2, 'UserID');

  /**
   * ID:3
   * Alias: UserName
   * Caption: $Views_Sessions_User.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(3, 'UserName', '$Views_Sessions_User');

  /**
   * ID:4
   * Alias: UserLogin
   * Caption: $Views_Sessions_Login.
   */
   readonly ColumnUserLogin: ViewObject = new ViewObject(4, 'UserLogin', '$Views_Sessions_Login');

  /**
   * ID:5
   * Alias: HostName
   * Caption: $Views_Sessions_Host.
   */
   readonly ColumnHostName: ViewObject = new ViewObject(5, 'HostName', '$Views_Sessions_Host');

  /**
   * ID:6
   * Alias: HostIP
   * Caption: $Views_Sessions_HostIP.
   */
   readonly ColumnHostIP: ViewObject = new ViewObject(6, 'HostIP', '$Views_Sessions_HostIP');

  /**
   * ID:7
   * Alias: Created
   * Caption: $Views_Sessions_Created.
   */
   readonly ColumnCreated: ViewObject = new ViewObject(7, 'Created', '$Views_Sessions_Created');

  /**
   * ID:8
   * Alias: LastActivity
   * Caption: $Views_Sessions_LastActivity.
   */
   readonly ColumnLastActivity: ViewObject = new ViewObject(8, 'LastActivity', '$Views_Sessions_LastActivity');

  /**
   * ID:9
   * Alias: IsActive
   * Caption: $Views_Sessions_IsActive.
   */
   readonly ColumnIsActive: ViewObject = new ViewObject(9, 'IsActive', '$Views_Sessions_IsActive');

  /**
   * ID:10
   * Alias: ApplicationID.
   */
   readonly ColumnApplicationID: ViewObject = new ViewObject(10, 'ApplicationID');

  /**
   * ID:11
   * Alias: ApplicationName
   * Caption: $Views_Sessions_Application.
   */
   readonly ColumnApplicationName: ViewObject = new ViewObject(11, 'ApplicationName', '$Views_Sessions_Application');

  /**
   * ID:12
   * Alias: AccessLevelID.
   */
   readonly ColumnAccessLevelID: ViewObject = new ViewObject(12, 'AccessLevelID');

  /**
   * ID:13
   * Alias: AccessLevelName
   * Caption: $Views_Sessions_AccessLevel.
   */
   readonly ColumnAccessLevelName: ViewObject = new ViewObject(13, 'AccessLevelName', '$Views_Sessions_AccessLevel');

  /**
   * ID:14
   * Alias: LoginTypeID.
   */
   readonly ColumnLoginTypeID: ViewObject = new ViewObject(14, 'LoginTypeID');

  /**
   * ID:15
   * Alias: LoginTypeName
   * Caption: $Views_Sessions_LoginType.
   */
   readonly ColumnLoginTypeName: ViewObject = new ViewObject(15, 'LoginTypeName', '$Views_Sessions_LoginType');

  /**
   * ID:16
   * Alias: LicenseTypeID.
   */
   readonly ColumnLicenseTypeID: ViewObject = new ViewObject(16, 'LicenseTypeID');

  /**
   * ID:17
   * Alias: LicenseTypeName
   * Caption: $Views_Sessions_LicenseType.
   */
   readonly ColumnLicenseTypeName: ViewObject = new ViewObject(17, 'LicenseTypeName', '$Views_Sessions_LicenseType');

  /**
   * ID:18
   * Alias: ServiceTypeID.
   */
   readonly ColumnServiceTypeID: ViewObject = new ViewObject(18, 'ServiceTypeID');

  /**
   * ID:19
   * Alias: ServiceTypeName
   * Caption: $Views_Sessions_ServiceType.
   */
   readonly ColumnServiceTypeName: ViewObject = new ViewObject(19, 'ServiceTypeName', '$Views_Sessions_ServiceType');

  /**
   * ID:20
   * Alias: DeviceTypeID.
   */
   readonly ColumnDeviceTypeID: ViewObject = new ViewObject(20, 'DeviceTypeID');

  /**
   * ID:21
   * Alias: DeviceTypeName
   * Caption: $Views_Sessions_DeviceType.
   */
   readonly ColumnDeviceTypeName: ViewObject = new ViewObject(21, 'DeviceTypeName', '$Views_Sessions_DeviceType');

  /**
   * ID:22
   * Alias: OSName
   * Caption: $Views_Sessions_OSName.
   */
   readonly ColumnOSName: ViewObject = new ViewObject(22, 'OSName', '$Views_Sessions_OSName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: UserID
   * Caption: $Views_Sessions_User_Param.
   */
   readonly ParamUserID: ViewObject = new ViewObject(0, 'UserID', '$Views_Sessions_User_Param');

  /**
   * ID:1
   * Alias: Login
   * Caption: $Views_Sessions_Login_Param.
   */
   readonly ParamLogin: ViewObject = new ViewObject(1, 'Login', '$Views_Sessions_Login_Param');

  /**
   * ID:2
   * Alias: HostName
   * Caption: $Views_Sessions_Host_Param.
   */
   readonly ParamHostName: ViewObject = new ViewObject(2, 'HostName', '$Views_Sessions_Host_Param');

  /**
   * ID:3
   * Alias: HostIP
   * Caption: $Views_Sessions_HostIP_Param.
   */
   readonly ParamHostIP: ViewObject = new ViewObject(3, 'HostIP', '$Views_Sessions_HostIP_Param');

  /**
   * ID:4
   * Alias: Created
   * Caption: $Views_Sessions_Created_Param.
   */
   readonly ParamCreated: ViewObject = new ViewObject(4, 'Created', '$Views_Sessions_Created_Param');

  /**
   * ID:5
   * Alias: LastActivity
   * Caption: $Views_Sessions_LastActivity_Param.
   */
   readonly ParamLastActivity: ViewObject = new ViewObject(5, 'LastActivity', '$Views_Sessions_LastActivity_Param');

  /**
   * ID:6
   * Alias: IsActive
   * Caption: $Views_Sessions_IsActive.
   */
   readonly ParamIsActive: ViewObject = new ViewObject(6, 'IsActive', '$Views_Sessions_IsActive');

  /**
   * ID:7
   * Alias: Application
   * Caption: $Views_Sessions_Application_Param.
   */
   readonly ParamApplication: ViewObject = new ViewObject(7, 'Application', '$Views_Sessions_Application_Param');

  /**
   * ID:8
   * Alias: AccessLevel
   * Caption: $Views_Sessions_AccessLevel_Param.
   */
   readonly ParamAccessLevel: ViewObject = new ViewObject(8, 'AccessLevel', '$Views_Sessions_AccessLevel_Param');

  /**
   * ID:9
   * Alias: LoginType
   * Caption: $Views_Sessions_LoginType_Param.
   */
   readonly ParamLoginType: ViewObject = new ViewObject(9, 'LoginType', '$Views_Sessions_LoginType_Param');

  /**
   * ID:10
   * Alias: LicenseType
   * Caption: $Views_Sessions_LicenseType_Param.
   */
   readonly ParamLicenseType: ViewObject = new ViewObject(10, 'LicenseType', '$Views_Sessions_LicenseType_Param');

  /**
   * ID:11
   * Alias: ServiceType
   * Caption: $Views_Sessions_ServiceType_Param.
   */
   readonly ParamServiceType: ViewObject = new ViewObject(11, 'ServiceType', '$Views_Sessions_ServiceType_Param');

  /**
   * ID:12
   * Alias: DeviceType
   * Caption: $Views_Sessions_DeviceType_Param.
   */
   readonly ParamDeviceType: ViewObject = new ViewObject(12, 'DeviceType', '$Views_Sessions_DeviceType_Param');

  /**
   * ID:13
   * Alias: OSName
   * Caption: $Views_Sessions_OSName_Param.
   */
   readonly ParamOSName: ViewObject = new ViewObject(13, 'OSName', '$Views_Sessions_OSName_Param');

  /**
   * ID:14
   * Alias: Department
   * Caption: $Views_Sessions_Department_Param.
   */
   readonly ParamDepartment: ViewObject = new ViewObject(14, 'Department', '$Views_Sessions_Department_Param');

  //#endregion
}

//#endregion

//#region SessionServiceTypes

/**
 * ID: {24f1eafb-5b69-4329-bf8b-10de805770df}
 * Alias: SessionServiceTypes
 * Caption: $Views_Names_SessionServiceTypes
 * Group: System
 */
class SessionServiceTypesViewInfo {
  //#region Common

  /**
   * View identifier for "SessionServiceTypes": {24f1eafb-5b69-4329-bf8b-10de805770df}.
   */
   readonly ID: guid = '24f1eafb-5b69-4329-bf8b-10de805770df';

  /**
   * View name for "SessionServiceTypes".
   */
   readonly Alias: string = 'SessionServiceTypes';

  /**
   * View caption for "SessionServiceTypes".
   */
   readonly Caption: string = '$Views_Names_SessionServiceTypes';

  /**
   * View group for "SessionServiceTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: $Views_SessionServiceTypes_Name.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', '$Views_SessionServiceTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_SessionServiceTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_SessionServiceTypes_Name_Param');

  //#endregion
}

//#endregion

//#region SignatureDigestAlgos

/**
 * ID: {c6f08c11-6dae-4e7f-a1d7-260134e9b3ff}
 * Alias: SignatureDigestAlgos
 * Caption: $Views_Names_SignatureDigestAlgos
 * Group: System
 */
class SignatureDigestAlgosViewInfo {
  //#region Common

  /**
   * View identifier for "SignatureDigestAlgos": {c6f08c11-6dae-4e7f-a1d7-260134e9b3ff}.
   */
   readonly ID: guid = 'c6f08c11-6dae-4e7f-a1d7-260134e9b3ff';

  /**
   * View name for "SignatureDigestAlgos".
   */
   readonly Alias: string = 'SignatureDigestAlgos';

  /**
   * View caption for "SignatureDigestAlgos".
   */
   readonly Caption: string = '$Views_Names_SignatureDigestAlgos';

  /**
   * View group for "SignatureDigestAlgos".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SignatureDigestAlgorithmID.
   */
   readonly ColumnSignatureDigestAlgorithmID: ViewObject = new ViewObject(0, 'SignatureDigestAlgorithmID');

  /**
   * ID:1
   * Alias: SignatureDigestAlgorithmName
   * Caption: $Views_SignatureDigestAlgorithms_Name.
   */
   readonly ColumnSignatureDigestAlgorithmName: ViewObject = new ViewObject(1, 'SignatureDigestAlgorithmName', '$Views_SignatureDigestAlgorithms_Name');

  /**
   * ID:2
   * Alias: SignatureDigestAlgorithmOID
   * Caption: $Views_SignatureDigestAlgorithms_OID.
   */
   readonly ColumnSignatureDigestAlgorithmOID: ViewObject = new ViewObject(2, 'SignatureDigestAlgorithmOID', '$Views_SignatureDigestAlgorithms_OID');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_SignatureDigestAlgorithms_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_SignatureDigestAlgorithms_Name_Param');

  /**
   * ID:1
   * Alias: EncAlgoOid
   * Caption: EncAlgoOid.
   */
   readonly ParamEncAlgoOid: ViewObject = new ViewObject(1, 'EncAlgoOid', 'EncAlgoOid');

  //#endregion
}

//#endregion

//#region SignatureEncryptionAlgos

/**
 * ID: {4cb7fef2-7b0d-4300-a9ac-7b2b082bcb75}
 * Alias: SignatureEncryptionAlgos
 * Caption: $Views_Names_SignatureEncryptionAlgos
 * Group: System
 */
class SignatureEncryptionAlgosViewInfo {
  //#region Common

  /**
   * View identifier for "SignatureEncryptionAlgos": {4cb7fef2-7b0d-4300-a9ac-7b2b082bcb75}.
   */
   readonly ID: guid = '4cb7fef2-7b0d-4300-a9ac-7b2b082bcb75';

  /**
   * View name for "SignatureEncryptionAlgos".
   */
   readonly Alias: string = 'SignatureEncryptionAlgos';

  /**
   * View caption for "SignatureEncryptionAlgos".
   */
   readonly Caption: string = '$Views_Names_SignatureEncryptionAlgos';

  /**
   * View group for "SignatureEncryptionAlgos".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SignatureEncryptionAlgorithmID.
   */
   readonly ColumnSignatureEncryptionAlgorithmID: ViewObject = new ViewObject(0, 'SignatureEncryptionAlgorithmID');

  /**
   * ID:1
   * Alias: SignatureEncryptionAlgorithmName
   * Caption: $Views_SignatureEncryptionAlgorithms_Name.
   */
   readonly ColumnSignatureEncryptionAlgorithmName: ViewObject = new ViewObject(1, 'SignatureEncryptionAlgorithmName', '$Views_SignatureEncryptionAlgorithms_Name');

  /**
   * ID:2
   * Alias: SignatureEncryptionAlgorithmOID
   * Caption: $Views_SignatureEncryptionAlgorithms_OID.
   */
   readonly ColumnSignatureEncryptionAlgorithmOID: ViewObject = new ViewObject(2, 'SignatureEncryptionAlgorithmOID', '$Views_SignatureEncryptionAlgorithms_OID');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_SignatureEncryptionAlgorithms_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_SignatureEncryptionAlgorithms_Name_Param');

  //#endregion
}

//#endregion

//#region SignaturePackagings

/**
 * ID: {b49ba9e8-4b5c-40af-8fa2-08daf310aca6}
 * Alias: SignaturePackagings
 * Caption: $Views_Names_SignaturePackagings
 * Group: System
 */
class SignaturePackagingsViewInfo {
  //#region Common

  /**
   * View identifier for "SignaturePackagings": {b49ba9e8-4b5c-40af-8fa2-08daf310aca6}.
   */
   readonly ID: guid = 'b49ba9e8-4b5c-40af-8fa2-08daf310aca6';

  /**
   * View name for "SignaturePackagings".
   */
   readonly Alias: string = 'SignaturePackagings';

  /**
   * View caption for "SignaturePackagings".
   */
   readonly Caption: string = '$Views_Names_SignaturePackagings';

  /**
   * View group for "SignaturePackagings".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SignaturePackagingID.
   */
   readonly ColumnSignaturePackagingID: ViewObject = new ViewObject(0, 'SignaturePackagingID');

  /**
   * ID:1
   * Alias: SignaturePackagingName
   * Caption: $Views_SignaturePackagings_Name.
   */
   readonly ColumnSignaturePackagingName: ViewObject = new ViewObject(1, 'SignaturePackagingName', '$Views_SignaturePackagings_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_SignaturePackagings_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_SignaturePackagings_Name_Param');

  //#endregion
}

//#endregion

//#region SignatureProfiles

/**
 * ID: {8cffc16e-6086-488b-99ce-bba53520e37c}
 * Alias: SignatureProfiles
 * Caption: $Views_Names_SignatureProfiles
 * Group: System
 */
class SignatureProfilesViewInfo {
  //#region Common

  /**
   * View identifier for "SignatureProfiles": {8cffc16e-6086-488b-99ce-bba53520e37c}.
   */
   readonly ID: guid = '8cffc16e-6086-488b-99ce-bba53520e37c';

  /**
   * View name for "SignatureProfiles".
   */
   readonly Alias: string = 'SignatureProfiles';

  /**
   * View caption for "SignatureProfiles".
   */
   readonly Caption: string = '$Views_Names_SignatureProfiles';

  /**
   * View group for "SignatureProfiles".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SignatureProfileID.
   */
   readonly ColumnSignatureProfileID: ViewObject = new ViewObject(0, 'SignatureProfileID');

  /**
   * ID:1
   * Alias: SignatureProfileName
   * Caption: $Views_SignatureProfiles_Name.
   */
   readonly ColumnSignatureProfileName: ViewObject = new ViewObject(1, 'SignatureProfileName', '$Views_SignatureProfiles_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_SignatureProfiles_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_SignatureProfiles_Name_Param');

  //#endregion
}

//#endregion

//#region SignatureTypes

/**
 * ID: {30a4931f-c0cf-41fd-b962-053488596ca1}
 * Alias: SignatureTypes
 * Caption: $Views_Names_SignatureTypes
 * Group: System
 */
class SignatureTypesViewInfo {
  //#region Common

  /**
   * View identifier for "SignatureTypes": {30a4931f-c0cf-41fd-b962-053488596ca1}.
   */
   readonly ID: guid = '30a4931f-c0cf-41fd-b962-053488596ca1';

  /**
   * View name for "SignatureTypes".
   */
   readonly Alias: string = 'SignatureTypes';

  /**
   * View caption for "SignatureTypes".
   */
   readonly Caption: string = '$Views_Names_SignatureTypes';

  /**
   * View group for "SignatureTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SignatureTypeID.
   */
   readonly ColumnSignatureTypeID: ViewObject = new ViewObject(0, 'SignatureTypeID');

  /**
   * ID:1
   * Alias: SignatureTypeName
   * Caption: $Views_SignatureTypes_Name.
   */
   readonly ColumnSignatureTypeName: ViewObject = new ViewObject(1, 'SignatureTypeName', '$Views_SignatureTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_SignatureTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_SignatureTypes_Name_Param');

  //#endregion
}

//#endregion

//#region SmartRoleGenerators

/**
 * ID: {26ce38e4-cf2d-48f0-a790-c4bd631e3eea}
 * Alias: SmartRoleGenerators
 * Caption: $Views_Names_SmartRoleGenerators
 * Group: Acl
 */
class SmartRoleGeneratorsViewInfo {
  //#region Common

  /**
   * View identifier for "SmartRoleGenerators": {26ce38e4-cf2d-48f0-a790-c4bd631e3eea}.
   */
   readonly ID: guid = '26ce38e4-cf2d-48f0-a790-c4bd631e3eea';

  /**
   * View name for "SmartRoleGenerators".
   */
   readonly Alias: string = 'SmartRoleGenerators';

  /**
   * View caption for "SmartRoleGenerators".
   */
   readonly Caption: string = '$Views_Names_SmartRoleGenerators';

  /**
   * View group for "SmartRoleGenerators".
   */
   readonly Group: string = 'Acl';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SmartRoleGeneratorID.
   */
   readonly ColumnSmartRoleGeneratorID: ViewObject = new ViewObject(0, 'SmartRoleGeneratorID');

  /**
   * ID:1
   * Alias: SmartRoleGeneratorName
   * Caption: $Views_SmartRoleGenerators_Name.
   */
   readonly ColumnSmartRoleGeneratorName: ViewObject = new ViewObject(1, 'SmartRoleGeneratorName', '$Views_SmartRoleGenerators_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_SmartRoleGenerators_Name.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_SmartRoleGenerators_Name');

  //#endregion
}

//#endregion

//#region TagCards

/**
 * ID: {c2b5ea9e-2bfa-43f6-a566-7e1df45f0f51}
 * Alias: TagCards
 * Caption: $Views_Names_TagCards
 * Group: Tags
 */
class TagCardsViewInfo {
  //#region Common

  /**
   * View identifier for "TagCards": {c2b5ea9e-2bfa-43f6-a566-7e1df45f0f51}.
   */
   readonly ID: guid = 'c2b5ea9e-2bfa-43f6-a566-7e1df45f0f51';

  /**
   * View name for "TagCards".
   */
   readonly Alias: string = 'TagCards';

  /**
   * View caption for "TagCards".
   */
   readonly Caption: string = '$Views_Names_TagCards';

  /**
   * View group for "TagCards".
   */
   readonly Group: string = 'Tags';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(0, 'CardID');

  /**
   * ID:1
   * Alias: CardType
   * Caption: $Views_TagCards_CardType.
   */
   readonly ColumnCardType: ViewObject = new ViewObject(1, 'CardType', '$Views_TagCards_CardType');

  /**
   * ID:2
   * Alias: CardNumber
   * Caption: $Views_TagCards_CardNumber.
   */
   readonly ColumnCardNumber: ViewObject = new ViewObject(2, 'CardNumber', '$Views_TagCards_CardNumber');

  /**
   * ID:3
   * Alias: CardSubject
   * Caption: $Views_TagCards_CardSubject.
   */
   readonly ColumnCardSubject: ViewObject = new ViewObject(3, 'CardSubject', '$Views_TagCards_CardSubject');

  /**
   * ID:4
   * Alias: CardDate
   * Caption: $Views_TagCards_CardDate.
   */
   readonly ColumnCardDate: ViewObject = new ViewObject(4, 'CardDate', '$Views_TagCards_CardDate');

  /**
   * ID:5
   * Alias: CardState
   * Caption: $Views_TagCards_CardState.
   */
   readonly ColumnCardState: ViewObject = new ViewObject(5, 'CardState', '$Views_TagCards_CardState');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Tag
   * Caption: $Views_TagCards_Tag_Param.
   */
   readonly ParamTag: ViewObject = new ViewObject(0, 'Tag', '$Views_TagCards_Tag_Param');

  //#endregion
}

//#endregion

//#region Tags

/**
 * ID: {99751083-96ea-435b-8331-4e0de99d69d6}
 * Alias: Tags
 * Caption: $Views_Names_Tags
 * Group: Tags
 */
class TagsViewInfo {
  //#region Common

  /**
   * View identifier for "Tags": {99751083-96ea-435b-8331-4e0de99d69d6}.
   */
   readonly ID: guid = '99751083-96ea-435b-8331-4e0de99d69d6';

  /**
   * View name for "Tags".
   */
   readonly Alias: string = 'Tags';

  /**
   * View caption for "Tags".
   */
   readonly Caption: string = '$Views_Names_Tags';

  /**
   * View group for "Tags".
   */
   readonly Group: string = 'Tags';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TagID.
   */
   readonly ColumnTagID: ViewObject = new ViewObject(0, 'TagID');

  /**
   * ID:1
   * Alias: TagName
   * Caption: $Views_Tags_Name.
   */
   readonly ColumnTagName: ViewObject = new ViewObject(1, 'TagName', '$Views_Tags_Name');

  /**
   * ID:2
   * Alias: TagOwnerID.
   */
   readonly ColumnTagOwnerID: ViewObject = new ViewObject(2, 'TagOwnerID');

  /**
   * ID:3
   * Alias: TagOwnerName
   * Caption: $Views_Tags_Owner.
   */
   readonly ColumnTagOwnerName: ViewObject = new ViewObject(3, 'TagOwnerName', '$Views_Tags_Owner');

  /**
   * ID:4
   * Alias: TagBackground
   * Caption: $CardTypes_Controls_Background.
   */
   readonly ColumnTagBackground: ViewObject = new ViewObject(4, 'TagBackground', '$CardTypes_Controls_Background');

  /**
   * ID:5
   * Alias: TagForeground
   * Caption: $CardTypes_Controls_Foreground.
   */
   readonly ColumnTagForeground: ViewObject = new ViewObject(5, 'TagForeground', '$CardTypes_Controls_Foreground');

  /**
   * ID:6
   * Alias: TagIsCommon
   * Caption: $Tags_IsCommon.
   */
   readonly ColumnTagIsCommon: ViewObject = new ViewObject(6, 'TagIsCommon', '$Tags_IsCommon');

  /**
   * ID:7
   * Alias: TagCanEdit.
   */
   readonly ColumnTagCanEdit: ViewObject = new ViewObject(7, 'TagCanEdit');

  /**
   * ID:8
   * Alias: TagCanUse.
   */
   readonly ColumnTagCanUse: ViewObject = new ViewObject(8, 'TagCanUse');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Tags_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Tags_Name_Param');

  /**
   * ID:1
   * Alias: Owner
   * Caption: $Views_Tags_Owner_Param.
   */
   readonly ParamOwner: ViewObject = new ViewObject(1, 'Owner', '$Views_Tags_Owner_Param');

  /**
   * ID:2
   * Alias: MyTagsOnly
   * Caption: $Views_Tags_MyTagsOnly_Param.
   */
   readonly ParamMyTagsOnly: ViewObject = new ViewObject(2, 'MyTagsOnly', '$Views_Tags_MyTagsOnly_Param');

  /**
   * ID:3
   * Alias: IgnoreCardID
   * Caption: $Tags_ExcludeCardTags.
   */
   readonly ParamIgnoreCardID: ViewObject = new ViewObject(3, 'IgnoreCardID', '$Tags_ExcludeCardTags');

  /**
   * ID:4
   * Alias: AddToCardDialogMode
   * Caption: $Tags_AddToCardMode.
   */
   readonly ParamAddToCardDialogMode: ViewObject = new ViewObject(4, 'AddToCardDialogMode', '$Tags_AddToCardMode');

  //#endregion
}

//#endregion

//#region TaskAssignedRoles

/**
 * ID: {b956e262-9b54-4aaf-a7b8-1649ea9462e7}
 * Alias: TaskAssignedRoles
 * Caption: $Views_Names_TaskAssignedRoles
 * Group: TaskAssignedRoles
 */
class TaskAssignedRolesViewInfo {
  //#region Common

  /**
   * View identifier for "TaskAssignedRoles": {b956e262-9b54-4aaf-a7b8-1649ea9462e7}.
   */
   readonly ID: guid = 'b956e262-9b54-4aaf-a7b8-1649ea9462e7';

  /**
   * View name for "TaskAssignedRoles".
   */
   readonly Alias: string = 'TaskAssignedRoles';

  /**
   * View caption for "TaskAssignedRoles".
   */
   readonly Caption: string = '$Views_Names_TaskAssignedRoles';

  /**
   * View group for "TaskAssignedRoles".
   */
   readonly Group: string = 'TaskAssignedRoles';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TaskRowID.
   */
   readonly ColumnTaskRowID: ViewObject = new ViewObject(0, 'TaskRowID');

  /**
   * ID:1
   * Alias: AssignedRoleRowID.
   */
   readonly ColumnAssignedRoleRowID: ViewObject = new ViewObject(1, 'AssignedRoleRowID');

  /**
   * ID:2
   * Alias: AssignedRoleTaskRoleID.
   */
   readonly ColumnAssignedRoleTaskRoleID: ViewObject = new ViewObject(2, 'AssignedRoleTaskRoleID');

  /**
   * ID:3
   * Alias: TaskRoleCaption
   * Caption: $Views_TaskAssignedRoles_TaskRoleName.
   */
   readonly ColumnTaskRoleCaption: ViewObject = new ViewObject(3, 'TaskRoleCaption', '$Views_TaskAssignedRoles_TaskRoleName');

  /**
   * ID:4
   * Alias: AssignedRoleRoleID.
   */
   readonly ColumnAssignedRoleRoleID: ViewObject = new ViewObject(4, 'AssignedRoleRoleID');

  /**
   * ID:5
   * Alias: RoleName
   * Caption: $Views_TaskAssignedRoles_RoleName.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(5, 'RoleName', '$Views_TaskAssignedRoles_RoleName');

  /**
   * ID:6
   * Alias: RoleTypeID.
   */
   readonly ColumnRoleTypeID: ViewObject = new ViewObject(6, 'RoleTypeID');

  /**
   * ID:7
   * Alias: RoleTypeCaption
   * Caption: $Views_TaskAssignedRoles_RoleTypeCaption.
   */
   readonly ColumnRoleTypeCaption: ViewObject = new ViewObject(7, 'RoleTypeCaption', '$Views_TaskAssignedRoles_RoleTypeCaption');

  /**
   * ID:8
   * Alias: Position
   * Caption: $Views_TaskAssignedRoles_Position.
   */
   readonly ColumnPosition: ViewObject = new ViewObject(8, 'Position', '$Views_TaskAssignedRoles_Position');

  /**
   * ID:9
   * Alias: ParentRowID.
   */
   readonly ColumnParentRowID: ViewObject = new ViewObject(9, 'ParentRowID');

  /**
   * ID:10
   * Alias: Master
   * Caption: $Views_TaskAssignedRoles_Master.
   */
   readonly ColumnMaster: ViewObject = new ViewObject(10, 'Master', '$Views_TaskAssignedRoles_Master');

  /**
   * ID:11
   * Alias: ShowInTaskDetails
   * Caption: $Views_TaskAssignedRoles_ShowInTaskDetails.
   */
   readonly ColumnShowInTaskDetails: ViewObject = new ViewObject(11, 'ShowInTaskDetails', '$Views_TaskAssignedRoles_ShowInTaskDetails');

  /**
   * ID:12
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(12, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: TaskRowID
   * Caption: $Views_TaskAssignedRoles_TaskRowID.
   */
   readonly ParamTaskRowID: ViewObject = new ViewObject(0, 'TaskRowID', '$Views_TaskAssignedRoles_TaskRowID');

  /**
   * ID:1
   * Alias: FunctionRole
   * Caption: $Views_TaskAssignedRoles_FunctionRole.
   */
   readonly ParamFunctionRole: ViewObject = new ViewObject(1, 'FunctionRole', '$Views_TaskAssignedRoles_FunctionRole');

  /**
   * ID:2
   * Alias: TaskRoleCaptionOrAlias
   * Caption: $Views_TaskAssignedRoles_TaskRoleCaptionAlias.
   */
   readonly ParamTaskRoleCaptionOrAlias: ViewObject = new ViewObject(2, 'TaskRoleCaptionOrAlias', '$Views_TaskAssignedRoles_TaskRoleCaptionAlias');

  //#endregion
}

//#endregion

//#region TaskAssignedRoleUsers

/**
 * ID: {966b7c87-1443-4ab9-b23a-6864234ed30e}
 * Alias: TaskAssignedRoleUsers
 * Caption: TaskAssignedRoleUsers
 * Group: TaskAssignedRoles
 */
class TaskAssignedRoleUsersViewInfo {
  //#region Common

  /**
   * View identifier for "TaskAssignedRoleUsers": {966b7c87-1443-4ab9-b23a-6864234ed30e}.
   */
   readonly ID: guid = '966b7c87-1443-4ab9-b23a-6864234ed30e';

  /**
   * View name for "TaskAssignedRoleUsers".
   */
   readonly Alias: string = 'TaskAssignedRoleUsers';

  /**
   * View caption for "TaskAssignedRoleUsers".
   */
   readonly Caption: string = 'TaskAssignedRoleUsers';

  /**
   * View group for "TaskAssignedRoleUsers".
   */
   readonly Group: string = 'TaskAssignedRoles';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(0, 'UserID');

  /**
   * ID:1
   * Alias: UserName
   * Caption: $Views_Users_Name.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(1, 'UserName', '$Views_Users_Name');

  /**
   * ID:2
   * Alias: Email
   * Caption: $Views_Users_Email.
   */
   readonly ColumnEmail: ViewObject = new ViewObject(2, 'Email', '$Views_Users_Email');

  /**
   * ID:3
   * Alias: Position
   * Caption: $Views_Users_Position.
   */
   readonly ColumnPosition: ViewObject = new ViewObject(3, 'Position', '$Views_Users_Position');

  /**
   * ID:4
   * Alias: Departments
   * Caption: $Views_Users_Departments.
   */
   readonly ColumnDepartments: ViewObject = new ViewObject(4, 'Departments', '$Views_Users_Departments');

  /**
   * ID:5
   * Alias: StaticRoles
   * Caption: $Views_Users_StaticRoles.
   */
   readonly ColumnStaticRoles: ViewObject = new ViewObject(5, 'StaticRoles', '$Views_Users_StaticRoles');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Users_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Users_Name_Param');

  /**
   * ID:1
   * Alias: TaskAssignedRoleRowID
   * Caption: $Views_Users_Role_Param.
   */
   readonly ParamTaskAssignedRoleRowID: ViewObject = new ViewObject(1, 'TaskAssignedRoleRowID', '$Views_Users_Role_Param');

  /**
   * ID:2
   * Alias: RoleExclusionID
   * Caption: $Views_Users_RoleExclusion_Param.
   */
   readonly ParamRoleExclusionID: ViewObject = new ViewObject(2, 'RoleExclusionID', '$Views_Users_RoleExclusion_Param');

  /**
   * ID:3
   * Alias: ShowHidden
   * Caption: $Views_Users_ShowHidden_Param.
   */
   readonly ParamShowHidden: ViewObject = new ViewObject(3, 'ShowHidden', '$Views_Users_ShowHidden_Param');

  /**
   * ID:4
   * Alias: RoleID
   * Caption: $Views_Users_Role_Param.
   */
   readonly ParamRoleID: ViewObject = new ViewObject(4, 'RoleID', '$Views_Users_Role_Param');

  //#endregion
}

//#endregion

//#region TaskFunctionRoles

/**
 * ID: {20a41d67-4807-496f-b5f8-1ae3f036eb2f}
 * Alias: TaskFunctionRoles
 * Caption: $Views_Names_TaskFunctionRoles
 * Group: TaskAssignedRoles
 */
class TaskFunctionRolesViewInfo {
  //#region Common

  /**
   * View identifier for "TaskFunctionRoles": {20a41d67-4807-496f-b5f8-1ae3f036eb2f}.
   */
   readonly ID: guid = '20a41d67-4807-496f-b5f8-1ae3f036eb2f';

  /**
   * View name for "TaskFunctionRoles".
   */
   readonly Alias: string = 'TaskFunctionRoles';

  /**
   * View caption for "TaskFunctionRoles".
   */
   readonly Caption: string = '$Views_Names_TaskFunctionRoles';

  /**
   * View group for "TaskFunctionRoles".
   */
   readonly Group: string = 'TaskAssignedRoles';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: FunctionRoleID.
   */
   readonly ColumnFunctionRoleID: ViewObject = new ViewObject(0, 'FunctionRoleID');

  /**
   * ID:1
   * Alias: RoleName
   * Caption: $Views_TaskAssignedRoles_TaskRoleName.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(1, 'RoleName', '$Views_TaskAssignedRoles_TaskRoleName');

  /**
   * ID:2
   * Alias: RoleCaption
   * Caption: $Views_TaskAssignedRoles_TaskRoleCaption.
   */
   readonly ColumnRoleCaption: ViewObject = new ViewObject(2, 'RoleCaption', '$Views_TaskAssignedRoles_TaskRoleCaption');

  /**
   * ID:3
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(3, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: TaskRowID
   * Caption: $Views_TaskAssignedRoles_TaskRowID.
   */
   readonly ParamTaskRowID: ViewObject = new ViewObject(0, 'TaskRowID', '$Views_TaskAssignedRoles_TaskRowID');

  /**
   * ID:1
   * Alias: AssignedRoleTaskRoleCaptionOrAlias
   * Caption: $Views_TaskAssignedRoles_TaskRoleCaptionAlias.
   */
   readonly ParamAssignedRoleTaskRoleCaptionOrAlias: ViewObject = new ViewObject(1, 'AssignedRoleTaskRoleCaptionOrAlias', '$Views_TaskAssignedRoles_TaskRoleCaptionAlias');

  //#endregion
}

//#endregion

//#region TaskHistory

/**
 * ID: {00da4f8f-bbf5-4b18-8b43-c528e5359d28}
 * Alias: TaskHistory
 * Caption: $Views_Names_TaskHistory
 * Group: System
 */
class TaskHistoryViewInfo {
  //#region Common

  /**
   * View identifier for "TaskHistory": {00da4f8f-bbf5-4b18-8b43-c528e5359d28}.
   */
   readonly ID: guid = '00da4f8f-bbf5-4b18-8b43-c528e5359d28';

  /**
   * View name for "TaskHistory".
   */
   readonly Alias: string = 'TaskHistory';

  /**
   * View caption for "TaskHistory".
   */
   readonly Caption: string = '$Views_Names_TaskHistory';

  /**
   * View group for "TaskHistory".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RowID.
   */
   readonly ColumnRowID: ViewObject = new ViewObject(0, 'RowID');

  /**
   * ID:1
   * Alias: ParentRowID.
   */
   readonly ColumnParentRowID: ViewObject = new ViewObject(1, 'ParentRowID');

  /**
   * ID:2
   * Alias: GroupRowID.
   */
   readonly ColumnGroupRowID: ViewObject = new ViewObject(2, 'GroupRowID');

  /**
   * ID:3
   * Alias: GroupParentRowID.
   */
   readonly ColumnGroupParentRowID: ViewObject = new ViewObject(3, 'GroupParentRowID');

  /**
   * ID:4
   * Alias: GroupCaption.
   */
   readonly ColumnGroupCaption: ViewObject = new ViewObject(4, 'GroupCaption');

  /**
   * ID:5
   * Alias: Group.
   */
   readonly ColumnGroup: ViewObject = new ViewObject(5, 'Group');

  /**
   * ID:6
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(6, 'TypeID');

  /**
   * ID:7
   * Alias: TypeCaption
   * Caption: $UI_Cards_TaskHistory_Task.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(7, 'TypeCaption', '$UI_Cards_TaskHistory_Task');

  /**
   * ID:8
   * Alias: StateCaption
   * Caption: $Views_TaskHistory_StateCaption.
   */
   readonly ColumnStateCaption: ViewObject = new ViewObject(8, 'StateCaption', '$Views_TaskHistory_StateCaption');

  /**
   * ID:9
   * Alias: Date
   * Caption: $Views_TaskHistory_Date.
   */
   readonly ColumnDate: ViewObject = new ViewObject(9, 'Date', '$Views_TaskHistory_Date');

  /**
   * ID:10
   * Alias: OptionCaption
   * Caption: $Views_TaskHistory_OptionCaption.
   */
   readonly ColumnOptionCaption: ViewObject = new ViewObject(10, 'OptionCaption', '$Views_TaskHistory_OptionCaption');

  /**
   * ID:11
   * Alias: CompletedByName
   * Caption: $Views_TaskHistory_CompletedByName.
   */
   readonly ColumnCompletedByName: ViewObject = new ViewObject(11, 'CompletedByName', '$Views_TaskHistory_CompletedByName');

  /**
   * ID:12
   * Alias: Result
   * Caption: $Views_TaskHistory_Result.
   */
   readonly ColumnResult: ViewObject = new ViewObject(12, 'Result', '$Views_TaskHistory_Result');

  /**
   * ID:13
   * Alias: AuthorName
   * Caption: $Views_TaskHistory_Author.
   */
   readonly ColumnAuthorName: ViewObject = new ViewObject(13, 'AuthorName', '$Views_TaskHistory_Author');

  /**
   * ID:14
   * Alias: RoleName
   * Caption: $Views_TaskHistory_RoleName.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(14, 'RoleName', '$Views_TaskHistory_RoleName');

  /**
   * ID:15
   * Alias: UserName
   * Caption: $Views_TaskHistory_UserName.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(15, 'UserName', '$Views_TaskHistory_UserName');

  /**
   * ID:16
   * Alias: Created
   * Caption: $Views_TaskHistory_Created.
   */
   readonly ColumnCreated: ViewObject = new ViewObject(16, 'Created', '$Views_TaskHistory_Created');

  /**
   * ID:17
   * Alias: Planned
   * Caption: $Views_TaskHistory_Planned.
   */
   readonly ColumnPlanned: ViewObject = new ViewObject(17, 'Planned', '$Views_TaskHistory_Planned');

  /**
   * ID:18
   * Alias: InProgress
   * Caption: $Views_TaskHistory_InProgress.
   */
   readonly ColumnInProgress: ViewObject = new ViewObject(18, 'InProgress', '$Views_TaskHistory_InProgress');

  /**
   * ID:19
   * Alias: Completed
   * Caption: $Views_TaskHistory_Completed.
   */
   readonly ColumnCompleted: ViewObject = new ViewObject(19, 'Completed', '$Views_TaskHistory_Completed');

  /**
   * ID:20
   * Alias: FilesCount
   * Caption: $Views_TaskHistory_FilesCount.
   */
   readonly ColumnFilesCount: ViewObject = new ViewObject(20, 'FilesCount', '$Views_TaskHistory_FilesCount');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: CardID
   * Caption: $Views_Templates_Digest.
   */
   readonly ParamCardID: ViewObject = new ViewObject(0, 'CardID', '$Views_Templates_Digest');

  /**
   * ID:1
   * Alias: Token
   * Caption: Token.
   */
   readonly ParamToken: ViewObject = new ViewObject(1, 'Token', 'Token');

  //#endregion
}

//#endregion

//#region TaskHistoryGroupTypes

/**
 * ID: {25d1c651-1008-496c-8252-778a4b5d9064}
 * Alias: TaskHistoryGroupTypes
 * Caption: $Views_Names_TaskHistoryGroupTypes
 * Group: System
 */
class TaskHistoryGroupTypesViewInfo {
  //#region Common

  /**
   * View identifier for "TaskHistoryGroupTypes": {25d1c651-1008-496c-8252-778a4b5d9064}.
   */
   readonly ID: guid = '25d1c651-1008-496c-8252-778a4b5d9064';

  /**
   * View name for "TaskHistoryGroupTypes".
   */
   readonly Alias: string = 'TaskHistoryGroupTypes';

  /**
   * View caption for "TaskHistoryGroupTypes".
   */
   readonly Caption: string = '$Views_Names_TaskHistoryGroupTypes';

  /**
   * View group for "TaskHistoryGroupTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: GroupTypeID.
   */
   readonly ColumnGroupTypeID: ViewObject = new ViewObject(0, 'GroupTypeID');

  /**
   * ID:1
   * Alias: GroupTypeCaption
   * Caption: $Views_TaskHistoryGroupTypes_GroupTypeCaption.
   */
   readonly ColumnGroupTypeCaption: ViewObject = new ViewObject(1, 'GroupTypeCaption', '$Views_TaskHistoryGroupTypes_GroupTypeCaption');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: GroupTypeCaptionParam
   * Caption: $Views_TaskHistoryGroupTypes_GroupTypeCaption.
   */
   readonly ParamGroupTypeCaptionParam: ViewObject = new ViewObject(0, 'GroupTypeCaptionParam', '$Views_TaskHistoryGroupTypes_GroupTypeCaption');

  //#endregion
}

//#endregion

//#region TaskKinds

/**
 * ID: {57dc8c0a-080f-486d-ba97-ffc52869754e}
 * Alias: TaskKinds
 * Caption: $Views_Names_TaskKinds
 * Group: System
 */
class TaskKindsViewInfo {
  //#region Common

  /**
   * View identifier for "TaskKinds": {57dc8c0a-080f-486d-ba97-ffc52869754e}.
   */
   readonly ID: guid = '57dc8c0a-080f-486d-ba97-ffc52869754e';

  /**
   * View name for "TaskKinds".
   */
   readonly Alias: string = 'TaskKinds';

  /**
   * View caption for "TaskKinds".
   */
   readonly Caption: string = '$Views_Names_TaskKinds';

  /**
   * View group for "TaskKinds".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: KindID.
   */
   readonly ColumnKindID: ViewObject = new ViewObject(0, 'KindID');

  /**
   * ID:1
   * Alias: KindCaption
   * Caption: $Views_TaskKinds_Caption.
   */
   readonly ColumnKindCaption: ViewObject = new ViewObject(1, 'KindCaption', '$Views_TaskKinds_Caption');

  /**
   * ID:2
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(2, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: ID
   * Caption: ID.
   */
   readonly ParamID: ViewObject = new ViewObject(0, 'ID', 'ID');

  /**
   * ID:1
   * Alias: Caption
   * Caption: $Views_TaskKinds_Caption_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(1, 'Caption', '$Views_TaskKinds_Caption_Param');

  //#endregion
}

//#endregion

//#region TaskStates

/**
 * ID: {b75a29da-0672-45ff-8f58-39abcb129506}
 * Alias: TaskStates
 * Caption: $Views_Names_TaskStates
 * Group: System
 */
class TaskStatesViewInfo {
  //#region Common

  /**
   * View identifier for "TaskStates": {b75a29da-0672-45ff-8f58-39abcb129506}.
   */
   readonly ID: guid = 'b75a29da-0672-45ff-8f58-39abcb129506';

  /**
   * View name for "TaskStates".
   */
   readonly Alias: string = 'TaskStates';

  /**
   * View caption for "TaskStates".
   */
   readonly Caption: string = '$Views_Names_TaskStates';

  /**
   * View group for "TaskStates".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TaskStateID.
   */
   readonly ColumnTaskStateID: ViewObject = new ViewObject(0, 'TaskStateID');

  /**
   * ID:1
   * Alias: TaskStateName
   * Caption: $Views_TaskStates_Name.
   */
   readonly ColumnTaskStateName: ViewObject = new ViewObject(1, 'TaskStateName', '$Views_TaskStates_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_TaskStates_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_TaskStates_Name_Param');

  //#endregion
}

//#endregion

//#region TaskTypes

/**
 * ID: {fcd3f5ad-545f-41d1-ad85-345157020e33}
 * Alias: TaskTypes
 * Caption: $Views_Names_TaskTypes
 * Group: System
 */
class TaskTypesViewInfo {
  //#region Common

  /**
   * View identifier for "TaskTypes": {fcd3f5ad-545f-41d1-ad85-345157020e33}.
   */
   readonly ID: guid = 'fcd3f5ad-545f-41d1-ad85-345157020e33';

  /**
   * View name for "TaskTypes".
   */
   readonly Alias: string = 'TaskTypes';

  /**
   * View caption for "TaskTypes".
   */
   readonly Caption: string = '$Views_Names_TaskTypes';

  /**
   * View group for "TaskTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeCaption
   * Caption: $Views_TaskTypes_Name.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(1, 'TypeCaption', '$Views_TaskTypes_Name');

  /**
   * ID:2
   * Alias: TypeName
   * Caption: $Views_TaskTypes_Alias.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(2, 'TypeName', '$Views_TaskTypes_Alias');

  /**
   * ID:3
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(3, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_TaskTypes_Name_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_TaskTypes_Name_Param');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_TaskTypes_Alias_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_TaskTypes_Alias_Param');

  /**
   * ID:2
   * Alias: NameOrCaption
   * Caption: NameOrCaption.
   */
   readonly ParamNameOrCaption: ViewObject = new ViewObject(2, 'NameOrCaption', 'NameOrCaption');

  /**
   * ID:3
   * Alias: OnlyEnabledForRoutes
   * Caption: OnlyEnabledForRoutes.
   */
   readonly ParamOnlyEnabledForRoutes: ViewObject = new ViewObject(3, 'OnlyEnabledForRoutes', 'OnlyEnabledForRoutes');

  //#endregion
}

//#endregion

//#region Templates

/**
 * ID: {edab4e60-c19e-49a2-979f-7634133c377e}
 * Alias: Templates
 * Caption: $Views_Names_Templates
 * Group: System
 */
class TemplatesViewInfo {
  //#region Common

  /**
   * View identifier for "Templates": {edab4e60-c19e-49a2-979f-7634133c377e}.
   */
   readonly ID: guid = 'edab4e60-c19e-49a2-979f-7634133c377e';

  /**
   * View name for "Templates".
   */
   readonly Alias: string = 'Templates';

  /**
   * View caption for "Templates".
   */
   readonly Caption: string = '$Views_Names_Templates';

  /**
   * View group for "Templates".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TemplateID.
   */
   readonly ColumnTemplateID: ViewObject = new ViewObject(0, 'TemplateID');

  /**
   * ID:1
   * Alias: TemplateCaption
   * Caption: $Views_Templates_Name.
   */
   readonly ColumnTemplateCaption: ViewObject = new ViewObject(1, 'TemplateCaption', '$Views_Templates_Name');

  /**
   * ID:2
   * Alias: TemplateDescription
   * Caption: $Views_Templates_Description.
   */
   readonly ColumnTemplateDescription: ViewObject = new ViewObject(2, 'TemplateDescription', '$Views_Templates_Description');

  /**
   * ID:3
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(3, 'TypeID');

  /**
   * ID:4
   * Alias: TypeCaption
   * Caption: $Views_Templates_Type.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(4, 'TypeCaption', '$Views_Templates_Type');

  /**
   * ID:5
   * Alias: TemplateDigest
   * Caption: $Views_Templates_Digest.
   */
   readonly ColumnTemplateDigest: ViewObject = new ViewObject(5, 'TemplateDigest', '$Views_Templates_Digest');

  /**
   * ID:6
   * Alias: TemplateVersion
   * Caption: $Views_Templates_Version.
   */
   readonly ColumnTemplateVersion: ViewObject = new ViewObject(6, 'TemplateVersion', '$Views_Templates_Version');

  /**
   * ID:7
   * Alias: TemplateDate
   * Caption: $Views_Templates_Date.
   */
   readonly ColumnTemplateDate: ViewObject = new ViewObject(7, 'TemplateDate', '$Views_Templates_Date');

  /**
   * ID:8
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(8, 'UserID');

  /**
   * ID:9
   * Alias: UserName
   * Caption: $Views_Templates_User.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(9, 'UserName', '$Views_Templates_User');

  /**
   * ID:10
   * Alias: CardID.
   */
   readonly ColumnCardID: ViewObject = new ViewObject(10, 'CardID');

  /**
   * ID:11
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(11, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: TemplateCaption
   * Caption: $Views_Templates_Name_Param.
   */
   readonly ParamTemplateCaption: ViewObject = new ViewObject(0, 'TemplateCaption', '$Views_Templates_Name_Param');

  /**
   * ID:1
   * Alias: TemplateDescription
   * Caption: $Views_Templates_Description_Param.
   */
   readonly ParamTemplateDescription: ViewObject = new ViewObject(1, 'TemplateDescription', '$Views_Templates_Description_Param');

  /**
   * ID:2
   * Alias: TypeID
   * Caption: $Views_Templates_Type_Param.
   */
   readonly ParamTypeID: ViewObject = new ViewObject(2, 'TypeID', '$Views_Templates_Type_Param');

  /**
   * ID:3
   * Alias: TemplateDigest
   * Caption: $Views_Templates_Digest_Param.
   */
   readonly ParamTemplateDigest: ViewObject = new ViewObject(3, 'TemplateDigest', '$Views_Templates_Digest_Param');

  /**
   * ID:4
   * Alias: TemplateVersion
   * Caption: $Views_Templates_Version_Param.
   */
   readonly ParamTemplateVersion: ViewObject = new ViewObject(4, 'TemplateVersion', '$Views_Templates_Version_Param');

  /**
   * ID:5
   * Alias: TemplateDate
   * Caption: $Views_Templates_Date_Param.
   */
   readonly ParamTemplateDate: ViewObject = new ViewObject(5, 'TemplateDate', '$Views_Templates_Date_Param');

  /**
   * ID:6
   * Alias: UserID
   * Caption: $Views_Templates_User_Param.
   */
   readonly ParamUserID: ViewObject = new ViewObject(6, 'UserID', '$Views_Templates_User_Param');

  /**
   * ID:7
   * Alias: CardID
   * Caption: $Views_Templates_SourceCard.
   */
   readonly ParamCardID: ViewObject = new ViewObject(7, 'CardID', '$Views_Templates_SourceCard');

  //#endregion
}

//#endregion

//#region TileSizes

/**
 * ID: {942b9908-f1c0-442d-b8b1-ba269431742d}
 * Alias: TileSizes
 * Caption: $Views_Names_TileSizes
 * Group: System
 */
class TileSizesViewInfo {
  //#region Common

  /**
   * View identifier for "TileSizes": {942b9908-f1c0-442d-b8b1-ba269431742d}.
   */
   readonly ID: guid = '942b9908-f1c0-442d-b8b1-ba269431742d';

  /**
   * View name for "TileSizes".
   */
   readonly Alias: string = 'TileSizes';

  /**
   * View caption for "TileSizes".
   */
   readonly Caption: string = '$Views_Names_TileSizes';

  /**
   * View group for "TileSizes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RefID.
   */
   readonly ColumnRefID: ViewObject = new ViewObject(0, 'RefID');

  /**
   * ID:1
   * Alias: RefName
   * Caption: $Views_TileSizes_Name.
   */
   readonly ColumnRefName: ViewObject = new ViewObject(1, 'RefName', '$Views_TileSizes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_TileSizes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_TileSizes_Name_Param');

  //#endregion
}

//#endregion

//#region TimeZones

/**
 * ID: {24ed3370-f3c0-4074-a3f2-4614a7baaebb}
 * Alias: TimeZones
 * Caption: $Views_Names_TimeZones
 * Group: System
 */
class TimeZonesViewInfo {
  //#region Common

  /**
   * View identifier for "TimeZones": {24ed3370-f3c0-4074-a3f2-4614a7baaebb}.
   */
   readonly ID: guid = '24ed3370-f3c0-4074-a3f2-4614a7baaebb';

  /**
   * View name for "TimeZones".
   */
   readonly Alias: string = 'TimeZones';

  /**
   * View caption for "TimeZones".
   */
   readonly Caption: string = '$Views_Names_TimeZones';

  /**
   * View group for "TimeZones".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ZoneID
   * Caption: $Views_TimeZones_ZoneID.
   */
   readonly ColumnZoneID: ViewObject = new ViewObject(0, 'ZoneID', '$Views_TimeZones_ZoneID');

  /**
   * ID:1
   * Alias: ZoneShortName
   * Caption: $Views_TimeZones_ShortName.
   */
   readonly ColumnZoneShortName: ViewObject = new ViewObject(1, 'ZoneShortName', '$Views_TimeZones_ShortName');

  /**
   * ID:2
   * Alias: ZoneCodeName
   * Caption: $Views_TimeZones_CodeName.
   */
   readonly ColumnZoneCodeName: ViewObject = new ViewObject(2, 'ZoneCodeName', '$Views_TimeZones_CodeName');

  /**
   * ID:3
   * Alias: ZoneUtcOffsetMinutes
   * Caption: $Views_TimeZones_UtcOffsetMinutes.
   */
   readonly ColumnZoneUtcOffsetMinutes: ViewObject = new ViewObject(3, 'ZoneUtcOffsetMinutes', '$Views_TimeZones_UtcOffsetMinutes');

  /**
   * ID:4
   * Alias: ZoneDisplayName
   * Caption: $Views_TimeZones_DisplayName.
   */
   readonly ColumnZoneDisplayName: ViewObject = new ViewObject(4, 'ZoneDisplayName', '$Views_TimeZones_DisplayName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_TimeZones_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_TimeZones_Name_Param');

  //#endregion
}

//#endregion

//#region TopicParticipants

/**
 * ID: {982d76b5-8997-4265-a8a3-c5e427746834}
 * Alias: TopicParticipants
 * Caption: $Views_Names_TopicParticipants
 * Group: Fm
 */
class TopicParticipantsViewInfo {
  //#region Common

  /**
   * View identifier for "TopicParticipants": {982d76b5-8997-4265-a8a3-c5e427746834}.
   */
   readonly ID: guid = '982d76b5-8997-4265-a8a3-c5e427746834';

  /**
   * View name for "TopicParticipants".
   */
   readonly Alias: string = 'TopicParticipants';

  /**
   * View caption for "TopicParticipants".
   */
   readonly Caption: string = '$Views_Names_TopicParticipants';

  /**
   * View group for "TopicParticipants".
   */
   readonly Group: string = 'Fm';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: RoleID.
   */
   readonly ColumnRoleID: ViewObject = new ViewObject(0, 'RoleID');

  /**
   * ID:1
   * Alias: RoleName
   * Caption: $Views_Roles_Role.
   */
   readonly ColumnRoleName: ViewObject = new ViewObject(1, 'RoleName', '$Views_Roles_Role');

  /**
   * ID:2
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(2, 'TypeID');

  /**
   * ID:3
   * Alias: TypeName
   * Caption: $Views_Roles_Type.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(3, 'TypeName', '$Views_Roles_Type');

  /**
   * ID:4
   * Alias: TopicID.
   */
   readonly ColumnTopicID: ViewObject = new ViewObject(4, 'TopicID');

  /**
   * ID:5
   * Alias: ReadOnly
   * Caption: $Views_TopicParticipants_ReadOnly.
   */
   readonly ColumnReadOnly: ViewObject = new ViewObject(5, 'ReadOnly', '$Views_TopicParticipants_ReadOnly');

  /**
   * ID:6
   * Alias: Subscribed
   * Caption: $Views_TopicParticipants_Subscribed.
   */
   readonly ColumnSubscribed: ViewObject = new ViewObject(6, 'Subscribed', '$Views_TopicParticipants_Subscribed');

  /**
   * ID:7
   * Alias: TypeParticipant
   * Caption: ParticipantTypeID.
   */
   readonly ColumnTypeParticipant: ViewObject = new ViewObject(7, 'TypeParticipant', 'ParticipantTypeID');

  /**
   * ID:8
   * Alias: TypeParticipantName
   * Caption: $Views_TopicParticipants_TypeParticipant.
   */
   readonly ColumnTypeParticipantName: ViewObject = new ViewObject(8, 'TypeParticipantName', '$Views_TopicParticipants_TypeParticipant');

  /**
   * ID:9
   * Alias: InvitingUserName
   * Caption: $Views_TopicParticipants_InvitingUserName.
   */
   readonly ColumnInvitingUserName: ViewObject = new ViewObject(9, 'InvitingUserName', '$Views_TopicParticipants_InvitingUserName');

  /**
   * ID:10
   * Alias: Info
   * Caption: $Views_Roles_Info.
   */
   readonly ColumnInfo: ViewObject = new ViewObject(10, 'Info', '$Views_Roles_Info');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Roles_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Roles_Name_Param');

  /**
   * ID:1
   * Alias: TypeID
   * Caption: $Views_Roles_Type_Param.
   */
   readonly ParamTypeID: ViewObject = new ViewObject(1, 'TypeID', '$Views_Roles_Type_Param');

  /**
   * ID:2
   * Alias: GeneratorID
   * Caption: $Views_Roles_Generator_Param.
   */
   readonly ParamGeneratorID: ViewObject = new ViewObject(2, 'GeneratorID', '$Views_Roles_Generator_Param');

  /**
   * ID:3
   * Alias: TopicID
   * Caption: $Views_TopicParticipants_Topic.
   */
   readonly ParamTopicID: ViewObject = new ViewObject(3, 'TopicID', '$Views_TopicParticipants_Topic');

  /**
   * ID:4
   * Alias: ParticipantTypeID
   * Caption: ParticipantTypeID.
   */
   readonly ParamParticipantTypeID: ViewObject = new ViewObject(4, 'ParticipantTypeID', 'ParticipantTypeID');

  /**
   * ID:5
   * Alias: CardID
   * Caption: CardID.
   */
   readonly ParamCardID: ViewObject = new ViewObject(5, 'CardID', 'CardID');

  /**
   * ID:6
   * Alias: IsStaticRole
   * Caption: $Views_TopicParticipants_IsStaticRole.
   */
   readonly ParamIsStaticRole: ViewObject = new ViewObject(6, 'IsStaticRole', '$Views_TopicParticipants_IsStaticRole');

  /**
   * ID:7
   * Alias: ShowHidden
   * Caption: $Views_Roles_ShowHidden_Param.
   */
   readonly ParamShowHidden: ViewObject = new ViewObject(7, 'ShowHidden', '$Views_Roles_ShowHidden_Param');

  //#endregion
}

//#endregion

//#region Types

/**
 * ID: {77b991d4-3f6d-4827-ae02-354e514f6c60}
 * Alias: Types
 * Caption: $Views_Names_Types
 * Group: System
 */
class TypesViewInfo {
  //#region Common

  /**
   * View identifier for "Types": {77b991d4-3f6d-4827-ae02-354e514f6c60}.
   */
   readonly ID: guid = '77b991d4-3f6d-4827-ae02-354e514f6c60';

  /**
   * View name for "Types".
   */
   readonly Alias: string = 'Types';

  /**
   * View caption for "Types".
   */
   readonly Caption: string = '$Views_Names_Types';

  /**
   * View group for "Types".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeCaption
   * Caption: $Views_Types_Name.
   */
   readonly ColumnTypeCaption: ViewObject = new ViewObject(1, 'TypeCaption', '$Views_Types_Name');

  /**
   * ID:2
   * Alias: TypeName
   * Caption: $Views_Types_Alias.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(2, 'TypeName', '$Views_Types_Alias');

  /**
   * ID:3
   * Alias: rn.
   */
   readonly Columnrn: ViewObject = new ViewObject(3, 'rn');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Caption
   * Caption: $Views_Types_Name_Param.
   */
   readonly ParamCaption: ViewObject = new ViewObject(0, 'Caption', '$Views_Types_Name_Param');

  /**
   * ID:1
   * Alias: Name
   * Caption: $Views_Types_Alias_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(1, 'Name', '$Views_Types_Alias_Param');

  /**
   * ID:2
   * Alias: IsTypeForSettings
   * Caption: $Views_Types_IsTypeForSettings_Param.
   */
   readonly ParamIsTypeForSettings: ViewObject = new ViewObject(2, 'IsTypeForSettings', '$Views_Types_IsTypeForSettings_Param');

  //#endregion
}

//#endregion

//#region Users

/**
 * ID: {8b68754e-19c8-0984-aac8-51d8908acecf}
 * Alias: Users
 * Caption: $Views_Names_Users
 * Group: System
 */
class UsersViewInfo {
  //#region Common

  /**
   * View identifier for "Users": {8b68754e-19c8-0984-aac8-51d8908acecf}.
   */
   readonly ID: guid = '8b68754e-19c8-0984-aac8-51d8908acecf';

  /**
   * View name for "Users".
   */
   readonly Alias: string = 'Users';

  /**
   * View caption for "Users".
   */
   readonly Caption: string = '$Views_Names_Users';

  /**
   * View group for "Users".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(0, 'UserID');

  /**
   * ID:1
   * Alias: UserName
   * Caption: $Views_Users_Name.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(1, 'UserName', '$Views_Users_Name');

  /**
   * ID:2
   * Alias: Email
   * Caption: $Views_Users_Email.
   */
   readonly ColumnEmail: ViewObject = new ViewObject(2, 'Email', '$Views_Users_Email');

  /**
   * ID:3
   * Alias: Position
   * Caption: $Views_Users_Position.
   */
   readonly ColumnPosition: ViewObject = new ViewObject(3, 'Position', '$Views_Users_Position');

  /**
   * ID:4
   * Alias: Departments
   * Caption: $Views_Users_Departments.
   */
   readonly ColumnDepartments: ViewObject = new ViewObject(4, 'Departments', '$Views_Users_Departments');

  /**
   * ID:5
   * Alias: StaticRoles
   * Caption: $Views_Users_StaticRoles.
   */
   readonly ColumnStaticRoles: ViewObject = new ViewObject(5, 'StaticRoles', '$Views_Users_StaticRoles');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Users_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Users_Name_Param');

  /**
   * ID:1
   * Alias: RoleID
   * Caption: $Views_Users_Role_Param.
   */
   readonly ParamRoleID: ViewObject = new ViewObject(1, 'RoleID', '$Views_Users_Role_Param');

  /**
   * ID:2
   * Alias: RoleExclusionID
   * Caption: $Views_Users_RoleExclusion_Param.
   */
   readonly ParamRoleExclusionID: ViewObject = new ViewObject(2, 'RoleExclusionID', '$Views_Users_RoleExclusion_Param');

  /**
   * ID:3
   * Alias: DepartmentRoleID
   * Caption: $Views_Users_Role_Param.
   */
   readonly ParamDepartmentRoleID: ViewObject = new ViewObject(3, 'DepartmentRoleID', '$Views_Users_Role_Param');

  /**
   * ID:4
   * Alias: StaticRoleID
   * Caption: $Views_Users_Role_Param.
   */
   readonly ParamStaticRoleID: ViewObject = new ViewObject(4, 'StaticRoleID', '$Views_Users_Role_Param');

  /**
   * ID:5
   * Alias: ParentRoleID
   * Caption: $Views_Users_ParentRole_Param.
   */
   readonly ParamParentRoleID: ViewObject = new ViewObject(5, 'ParentRoleID', '$Views_Users_ParentRole_Param');

  /**
   * ID:6
   * Alias: ShowHidden
   * Caption: $Views_Users_ShowHidden_Param.
   */
   readonly ParamShowHidden: ViewObject = new ViewObject(6, 'ShowHidden', '$Views_Users_ShowHidden_Param');

  //#endregion
}

//#endregion

//#region VatTypes

/**
 * ID: {c14070dd-219a-4340-91f3-7c2ffd891382}
 * Alias: VatTypes
 * Caption: $Views_Names_VatTypes
 * Group: System
 */
class VatTypesViewInfo {
  //#region Common

  /**
   * View identifier for "VatTypes": {c14070dd-219a-4340-91f3-7c2ffd891382}.
   */
   readonly ID: guid = 'c14070dd-219a-4340-91f3-7c2ffd891382';

  /**
   * View name for "VatTypes".
   */
   readonly Alias: string = 'VatTypes';

  /**
   * View caption for "VatTypes".
   */
   readonly Caption: string = '$Views_Names_VatTypes';

  /**
   * View group for "VatTypes".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: VatTypeID.
   */
   readonly ColumnVatTypeID: ViewObject = new ViewObject(0, 'VatTypeID');

  /**
   * ID:1
   * Alias: VatTypeName
   * Caption: $Views_VatTypes_Name.
   */
   readonly ColumnVatTypeName: ViewObject = new ViewObject(1, 'VatTypeName', '$Views_VatTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_VatTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_VatTypes_Name_Param');

  //#endregion
}

//#endregion

//#region ViewFiles

/**
 * ID: {af26a537-a1c9-4e09-9048-b892cc0c687e}
 * Alias: ViewFiles
 * Caption: $Views_Names_ViewFiles
 * Group: Testing
 */
class ViewFilesViewInfo {
  //#region Common

  /**
   * View identifier for "ViewFiles": {af26a537-a1c9-4e09-9048-b892cc0c687e}.
   */
   readonly ID: guid = 'af26a537-a1c9-4e09-9048-b892cc0c687e';

  /**
   * View name for "ViewFiles".
   */
   readonly Alias: string = 'ViewFiles';

  /**
   * View caption for "ViewFiles".
   */
   readonly Caption: string = '$Views_Names_ViewFiles';

  /**
   * View group for "ViewFiles".
   */
   readonly Group: string = 'Testing';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: FullFileName
   * Caption: FullFileName.
   */
   readonly ColumnFullFileName: ViewObject = new ViewObject(0, 'FullFileName', 'FullFileName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_ViewFiles_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_ViewFiles_Name_Param');

  /**
   * ID:1
   * Alias: Folder
   * Caption: $Views_ViewFiles_Folder_Param.
   */
   readonly ParamFolder: ViewObject = new ViewObject(1, 'Folder', '$Views_ViewFiles_Folder_Param');

  /**
   * ID:2
   * Alias: ParentFolder
   * Caption: $Views_ViewFiles_ParentFolder_Param.
   */
   readonly ParamParentFolder: ViewObject = new ViewObject(2, 'ParentFolder', '$Views_ViewFiles_ParentFolder_Param');

  //#endregion
}

//#endregion

//#region Views

/**
 * ID: {54614ef7-6a51-46c4-aa03-93814eb79126}
 * Alias: Views
 * Caption: $Views_Names_Views
 * Group: System
 */
class ViewsViewInfo {
  //#region Common

  /**
   * View identifier for "Views": {54614ef7-6a51-46c4-aa03-93814eb79126}.
   */
   readonly ID: guid = '54614ef7-6a51-46c4-aa03-93814eb79126';

  /**
   * View name for "Views".
   */
   readonly Alias: string = 'Views';

  /**
   * View caption for "Views".
   */
   readonly Caption: string = '$Views_Names_Views';

  /**
   * View group for "Views".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ViewID.
   */
   readonly ColumnViewID: ViewObject = new ViewObject(0, 'ViewID');

  /**
   * ID:1
   * Alias: ViewAlias
   * Caption: $Views_KrTypes_Alias.
   */
   readonly ColumnViewAlias: ViewObject = new ViewObject(1, 'ViewAlias', '$Views_KrTypes_Alias');

  /**
   * ID:2
   * Alias: ViewCaption
   * Caption: $Views_KrTypes_Caption.
   */
   readonly ColumnViewCaption: ViewObject = new ViewObject(2, 'ViewCaption', '$Views_KrTypes_Caption');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: AliasOrCaption
   * Caption: $Views_KrTypes_AliasOrCaption_Param.
   */
   readonly ParamAliasOrCaption: ViewObject = new ViewObject(0, 'AliasOrCaption', '$Views_KrTypes_AliasOrCaption_Param');

  //#endregion
}

//#endregion

//#region WebApplications

/**
 * ID: {7849787c-5422-4a08-9652-dd77d8557f4a}
 * Alias: WebApplications
 * Caption: $Views_Names_WebApplications
 * Group: System
 */
class WebApplicationsViewInfo {
  //#region Common

  /**
   * View identifier for "WebApplications": {7849787c-5422-4a08-9652-dd77d8557f4a}.
   */
   readonly ID: guid = '7849787c-5422-4a08-9652-dd77d8557f4a';

  /**
   * View name for "WebApplications".
   */
   readonly Alias: string = 'WebApplications';

  /**
   * View caption for "WebApplications".
   */
   readonly Caption: string = '$Views_Names_WebApplications';

  /**
   * View group for "WebApplications".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: AppID.
   */
   readonly ColumnAppID: ViewObject = new ViewObject(0, 'AppID');

  /**
   * ID:1
   * Alias: AppName
   * Caption: $Views_WebApplications_Name.
   */
   readonly ColumnAppName: ViewObject = new ViewObject(1, 'AppName', '$Views_WebApplications_Name');

  /**
   * ID:2
   * Alias: LanguageID.
   */
   readonly ColumnLanguageID: ViewObject = new ViewObject(2, 'LanguageID');

  /**
   * ID:3
   * Alias: LanguageCode.
   */
   readonly ColumnLanguageCode: ViewObject = new ViewObject(3, 'LanguageCode');

  /**
   * ID:4
   * Alias: LanguageCaption
   * Caption: $Views_WebApplications_LanguageCaption.
   */
   readonly ColumnLanguageCaption: ViewObject = new ViewObject(4, 'LanguageCaption', '$Views_WebApplications_LanguageCaption');

  /**
   * ID:5
   * Alias: OSName
   * Caption: $Views_WebApplications_OSName.
   */
   readonly ColumnOSName: ViewObject = new ViewObject(5, 'OSName', '$Views_WebApplications_OSName');

  /**
   * ID:6
   * Alias: Client64Bit
   * Caption: $Views_WebApplications_Client64Bit.
   */
   readonly ColumnClient64Bit: ViewObject = new ViewObject(6, 'Client64Bit', '$Views_WebApplications_Client64Bit');

  /**
   * ID:7
   * Alias: ExecutableFileName.
   */
   readonly ColumnExecutableFileName: ViewObject = new ViewObject(7, 'ExecutableFileName');

  /**
   * ID:8
   * Alias: AppVersion
   * Caption: $Views_WebApplications_AppVersion.
   */
   readonly ColumnAppVersion: ViewObject = new ViewObject(8, 'AppVersion', '$Views_WebApplications_AppVersion');

  /**
   * ID:9
   * Alias: PlatformVersion
   * Caption: $Views_WebApplications_PlatformVersion.
   */
   readonly ColumnPlatformVersion: ViewObject = new ViewObject(9, 'PlatformVersion', '$Views_WebApplications_PlatformVersion');

  /**
   * ID:10
   * Alias: Description
   * Caption: $Views_WebApplications_Description.
   */
   readonly ColumnDescription: ViewObject = new ViewObject(10, 'Description', '$Views_WebApplications_Description');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_WebApplications_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_WebApplications_Name_Param');

  /**
   * ID:1
   * Alias: CheckAvailable
   * Caption: $Views_WebApplications_CheckAvailable_Param.
   */
   readonly ParamCheckAvailable: ViewObject = new ViewObject(1, 'CheckAvailable', '$Views_WebApplications_CheckAvailable_Param');

  //#endregion
}

//#endregion

//#region WeTaskControlTypes

/**
 * ID: {81a77ff3-5f38-42a4-bdaf-61ec8c508c39}
 * Alias: WeTaskControlTypes
 * Caption: $Views_Names_WeTaskControlTypes
 * Group: WorkflowEngine
 */
class WeTaskControlTypesViewInfo {
  //#region Common

  /**
   * View identifier for "WeTaskControlTypes": {81a77ff3-5f38-42a4-bdaf-61ec8c508c39}.
   */
   readonly ID: guid = '81a77ff3-5f38-42a4-bdaf-61ec8c508c39';

  /**
   * View name for "WeTaskControlTypes".
   */
   readonly Alias: string = 'WeTaskControlTypes';

  /**
   * View caption for "WeTaskControlTypes".
   */
   readonly Caption: string = '$Views_Names_WeTaskControlTypes';

  /**
   * View group for "WeTaskControlTypes".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ControlTypeID.
   */
   readonly ColumnControlTypeID: ViewObject = new ViewObject(0, 'ControlTypeID');

  /**
   * ID:1
   * Alias: ControlTypeName
   * Caption: $Views_WeTaskControlTypes_Name.
   */
   readonly ColumnControlTypeName: ViewObject = new ViewObject(1, 'ControlTypeName', '$Views_WeTaskControlTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_WeTaskControlTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_WeTaskControlTypes_Name_Param');

  //#endregion
}

//#endregion

//#region WeTaskGroupActionOptionTypes

/**
 * ID: {6a66914d-790f-480a-9976-cb85cc67e028}
 * Alias: WeTaskGroupActionOptionTypes
 * Caption: $Views_Names_WeTaskGroupActionOptionTypes
 * Group: WorkflowEngine
 */
class WeTaskGroupActionOptionTypesViewInfo {
  //#region Common

  /**
   * View identifier for "WeTaskGroupActionOptionTypes": {6a66914d-790f-480a-9976-cb85cc67e028}.
   */
   readonly ID: guid = '6a66914d-790f-480a-9976-cb85cc67e028';

  /**
   * View name for "WeTaskGroupActionOptionTypes".
   */
   readonly Alias: string = 'WeTaskGroupActionOptionTypes';

  /**
   * View caption for "WeTaskGroupActionOptionTypes".
   */
   readonly Caption: string = '$Views_Names_WeTaskGroupActionOptionTypes';

  /**
   * View group for "WeTaskGroupActionOptionTypes".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: OptionTypeID.
   */
   readonly ColumnOptionTypeID: ViewObject = new ViewObject(0, 'OptionTypeID');

  /**
   * ID:1
   * Alias: OptionTypeName
   * Caption: $Views_WeTaskGroupActionOptionTypes_Name.
   */
   readonly ColumnOptionTypeName: ViewObject = new ViewObject(1, 'OptionTypeName', '$Views_WeTaskGroupActionOptionTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_WeTaskGroupActionOptionTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_WeTaskGroupActionOptionTypes_Name_Param');

  //#endregion
}

//#endregion

//#region WfResolutionAuthors

/**
 * ID: {f3cc3c6c-67c6-477e-a2ab-99fb0bd6e95f}
 * Alias: WfResolutionAuthors
 * Caption: $Views_Names_WfResolutionAuthors
 * Group: Kr Wf
 */
class WfResolutionAuthorsViewInfo {
  //#region Common

  /**
   * View identifier for "WfResolutionAuthors": {f3cc3c6c-67c6-477e-a2ab-99fb0bd6e95f}.
   */
   readonly ID: guid = 'f3cc3c6c-67c6-477e-a2ab-99fb0bd6e95f';

  /**
   * View name for "WfResolutionAuthors".
   */
   readonly Alias: string = 'WfResolutionAuthors';

  /**
   * View caption for "WfResolutionAuthors".
   */
   readonly Caption: string = '$Views_Names_WfResolutionAuthors';

  /**
   * View group for "WfResolutionAuthors".
   */
   readonly Group: string = 'Kr Wf';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: UserID.
   */
   readonly ColumnUserID: ViewObject = new ViewObject(0, 'UserID');

  /**
   * ID:1
   * Alias: UserName
   * Caption: $Views_WfResolutionAuthors_Name.
   */
   readonly ColumnUserName: ViewObject = new ViewObject(1, 'UserName', '$Views_WfResolutionAuthors_Name');

  /**
   * ID:2
   * Alias: Departments
   * Caption: $Views_WfResolutionAuthors_Departments.
   */
   readonly ColumnDepartments: ViewObject = new ViewObject(2, 'Departments', '$Views_WfResolutionAuthors_Departments');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_WfResolutionAuthors_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_WfResolutionAuthors_Name_Param');

  //#endregion
}

//#endregion

//#region WorkflowEngineCompiledBaseTypes

/**
 * ID: {d3ba4765-6a54-465e-8803-323050fd951e}
 * Alias: WorkflowEngineCompiledBaseTypes
 * Caption: $Views_Names_WorkflowEngineCompiledBaseTypes
 * Group: WorkflowEngine
 */
class WorkflowEngineCompiledBaseTypesViewInfo {
  //#region Common

  /**
   * View identifier for "WorkflowEngineCompiledBaseTypes": {d3ba4765-6a54-465e-8803-323050fd951e}.
   */
   readonly ID: guid = 'd3ba4765-6a54-465e-8803-323050fd951e';

  /**
   * View name for "WorkflowEngineCompiledBaseTypes".
   */
   readonly Alias: string = 'WorkflowEngineCompiledBaseTypes';

  /**
   * View caption for "WorkflowEngineCompiledBaseTypes".
   */
   readonly Caption: string = '$Views_Names_WorkflowEngineCompiledBaseTypes';

  /**
   * View group for "WorkflowEngineCompiledBaseTypes".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TypeID.
   */
   readonly ColumnTypeID: ViewObject = new ViewObject(0, 'TypeID');

  /**
   * ID:1
   * Alias: TypeName
   * Caption: $Views_WorkflowEngineCompiledBaseTypes_Name.
   */
   readonly ColumnTypeName: ViewObject = new ViewObject(1, 'TypeName', '$Views_WorkflowEngineCompiledBaseTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_WorkflowEngineCompiledBaseTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_WorkflowEngineCompiledBaseTypes_Name_Param');

  //#endregion
}

//#endregion

//#region WorkflowEngineErrors

/**
 * ID: {6138ccf4-b5a2-4789-a3c2-82382ae666a2}
 * Alias: WorkflowEngineErrors
 * Caption: $Views_Names_WorkflowEngineErrors
 * Group: WorkflowEngine
 */
class WorkflowEngineErrorsViewInfo {
  //#region Common

  /**
   * View identifier for "WorkflowEngineErrors": {6138ccf4-b5a2-4789-a3c2-82382ae666a2}.
   */
   readonly ID: guid = '6138ccf4-b5a2-4789-a3c2-82382ae666a2';

  /**
   * View name for "WorkflowEngineErrors".
   */
   readonly Alias: string = 'WorkflowEngineErrors';

  /**
   * View caption for "WorkflowEngineErrors".
   */
   readonly Caption: string = '$Views_Names_WorkflowEngineErrors';

  /**
   * View group for "WorkflowEngineErrors".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ProcessErrorID.
   */
   readonly ColumnProcessErrorID: ViewObject = new ViewObject(0, 'ProcessErrorID');

  /**
   * ID:1
   * Alias: ProcessErrorAdded
   * Caption: $Views_WorkflowEngineErrors_Added.
   */
   readonly ColumnProcessErrorAdded: ViewObject = new ViewObject(1, 'ProcessErrorAdded', '$Views_WorkflowEngineErrors_Added');

  /**
   * ID:2
   * Alias: ProcessErrorText
   * Caption: $Views_WorkflowEngineErrors_ErrorText.
   */
   readonly ColumnProcessErrorText: ViewObject = new ViewObject(2, 'ProcessErrorText', '$Views_WorkflowEngineErrors_ErrorText');

  /**
   * ID:3
   * Alias: ProcessNodeInstanceID.
   */
   readonly ColumnProcessNodeInstanceID: ViewObject = new ViewObject(3, 'ProcessNodeInstanceID');

  /**
   * ID:4
   * Alias: ProcessNodeID.
   */
   readonly ColumnProcessNodeID: ViewObject = new ViewObject(4, 'ProcessNodeID');

  /**
   * ID:5
   * Alias: ProcessIsAsync.
   */
   readonly ColumnProcessIsAsync: ViewObject = new ViewObject(5, 'ProcessIsAsync');

  /**
   * ID:6
   * Alias: ProcessResumable.
   */
   readonly ColumnProcessResumable: ViewObject = new ViewObject(6, 'ProcessResumable');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Added
   * Caption: $Views_WorkflowEngineErrors_Added_Param.
   */
   readonly ParamAdded: ViewObject = new ViewObject(0, 'Added', '$Views_WorkflowEngineErrors_Added_Param');

  /**
   * ID:1
   * Alias: ProcessInstance
   * Caption: $Views_WorkflowEngineErrors_Process_Param.
   */
   readonly ParamProcessInstance: ViewObject = new ViewObject(1, 'ProcessInstance', '$Views_WorkflowEngineErrors_Process_Param');

  /**
   * ID:2
   * Alias: Active
   * Caption: $Views_WorkflowEngineErrors_ShowActiveOnly_Param.
   */
   readonly ParamActive: ViewObject = new ViewObject(2, 'Active', '$Views_WorkflowEngineErrors_ShowActiveOnly_Param');

  //#endregion
}

//#endregion

//#region WorkflowEngineLogLevels

/**
 * ID: {e91f6c3a-c8a0-46d3-bc07-5277f0e7d3f7}
 * Alias: WorkflowEngineLogLevels
 * Caption: $Views_Names_WorkflowEngineLogLevels
 * Group: WorkflowEngine
 */
class WorkflowEngineLogLevelsViewInfo {
  //#region Common

  /**
   * View identifier for "WorkflowEngineLogLevels": {e91f6c3a-c8a0-46d3-bc07-5277f0e7d3f7}.
   */
   readonly ID: guid = 'e91f6c3a-c8a0-46d3-bc07-5277f0e7d3f7';

  /**
   * View name for "WorkflowEngineLogLevels".
   */
   readonly Alias: string = 'WorkflowEngineLogLevels';

  /**
   * View caption for "WorkflowEngineLogLevels".
   */
   readonly Caption: string = '$Views_Names_WorkflowEngineLogLevels';

  /**
   * View group for "WorkflowEngineLogLevels".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: LogLevelID.
   */
   readonly ColumnLogLevelID: ViewObject = new ViewObject(0, 'LogLevelID');

  /**
   * ID:1
   * Alias: LogLevelName
   * Caption: $Views_WorkflowEngineLogLevels_Name.
   */
   readonly ColumnLogLevelName: ViewObject = new ViewObject(1, 'LogLevelName', '$Views_WorkflowEngineLogLevels_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_WorkflowEngineLogLevels_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_WorkflowEngineLogLevels_Name_Param');

  //#endregion
}

//#endregion

//#region WorkflowEngineLogs

/**
 * ID: {db1faa0a-fdfd-4a97-80e4-c1573d47b6c3}
 * Alias: WorkflowEngineLogs
 * Caption: $Views_Names_WorkflowEngineLogs
 * Group: WorkflowEngine
 */
class WorkflowEngineLogsViewInfo {
  //#region Common

  /**
   * View identifier for "WorkflowEngineLogs": {db1faa0a-fdfd-4a97-80e4-c1573d47b6c3}.
   */
   readonly ID: guid = 'db1faa0a-fdfd-4a97-80e4-c1573d47b6c3';

  /**
   * View name for "WorkflowEngineLogs".
   */
   readonly Alias: string = 'WorkflowEngineLogs';

  /**
   * View caption for "WorkflowEngineLogs".
   */
   readonly Caption: string = '$Views_Names_WorkflowEngineLogs';

  /**
   * View group for "WorkflowEngineLogs".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ProcessLogID.
   */
   readonly ColumnProcessLogID: ViewObject = new ViewObject(0, 'ProcessLogID');

  /**
   * ID:1
   * Alias: ProcessLogAdded
   * Caption: $Views_WorkflowEngineLogs_Added.
   */
   readonly ColumnProcessLogAdded: ViewObject = new ViewObject(1, 'ProcessLogAdded', '$Views_WorkflowEngineLogs_Added');

  /**
   * ID:2
   * Alias: ProcessLogLevel
   * Caption: $Views_WorkflowEngineLogs_LogLevel.
   */
   readonly ColumnProcessLogLevel: ViewObject = new ViewObject(2, 'ProcessLogLevel', '$Views_WorkflowEngineLogs_LogLevel');

  /**
   * ID:3
   * Alias: ProcessLogObject
   * Caption: $Views_WorkflowEngineLogs_LogObject.
   */
   readonly ColumnProcessLogObject: ViewObject = new ViewObject(3, 'ProcessLogObject', '$Views_WorkflowEngineLogs_LogObject');

  /**
   * ID:4
   * Alias: ProcessLogText
   * Caption: $Views_WorkflowEngineLogs_Text.
   */
   readonly ColumnProcessLogText: ViewObject = new ViewObject(4, 'ProcessLogText', '$Views_WorkflowEngineLogs_Text');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Added
   * Caption: $Views_WorkflowEngineLogs_Added_Param.
   */
   readonly ParamAdded: ViewObject = new ViewObject(0, 'Added', '$Views_WorkflowEngineLogs_Added_Param');

  /**
   * ID:1
   * Alias: ProcessInstance
   * Caption: $Views_WorkflowEngineLogs_Process_Param.
   */
   readonly ParamProcessInstance: ViewObject = new ViewObject(1, 'ProcessInstance', '$Views_WorkflowEngineLogs_Process_Param');

  /**
   * ID:2
   * Alias: LogLevel
   * Caption: $Views_WorkflowEngineLogs_LogLevel_Param.
   */
   readonly ParamLogLevel: ViewObject = new ViewObject(2, 'LogLevel', '$Views_WorkflowEngineLogs_LogLevel_Param');

  //#endregion
}

//#endregion

//#region WorkflowEngineTaskActions

/**
 * ID: {c39c3de3-6448-4c35-8978-4b385ca6a647}
 * Alias: WorkflowEngineTaskActions
 * Caption: $Views_Names_WorkflowEngineTaskActions
 * Group: WorkflowEngine
 */
class WorkflowEngineTaskActionsViewInfo {
  //#region Common

  /**
   * View identifier for "WorkflowEngineTaskActions": {c39c3de3-6448-4c35-8978-4b385ca6a647}.
   */
   readonly ID: guid = 'c39c3de3-6448-4c35-8978-4b385ca6a647';

  /**
   * View name for "WorkflowEngineTaskActions".
   */
   readonly Alias: string = 'WorkflowEngineTaskActions';

  /**
   * View caption for "WorkflowEngineTaskActions".
   */
   readonly Caption: string = '$Views_Names_WorkflowEngineTaskActions';

  /**
   * View group for "WorkflowEngineTaskActions".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TaskActionID.
   */
   readonly ColumnTaskActionID: ViewObject = new ViewObject(0, 'TaskActionID');

  /**
   * ID:1
   * Alias: TaskActionName
   * Caption: $Views_WorkflowEngineTaskActions_Name.
   */
   readonly ColumnTaskActionName: ViewObject = new ViewObject(1, 'TaskActionName', '$Views_WorkflowEngineTaskActions_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_WorkflowEngineTaskActions_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_WorkflowEngineTaskActions_Name_Param');

  //#endregion
}

//#endregion

//#region WorkflowEngineTileManagerExtensions

/**
 * ID: {e102a6c1-807c-4976-b2e7-180404090e75}
 * Alias: WorkflowEngineTileManagerExtensions
 * Caption: $Views_Names_WorkflowEngineTileManagerExtensions
 * Group: WorkflowEngine
 */
class WorkflowEngineTileManagerExtensionsViewInfo {
  //#region Common

  /**
   * View identifier for "WorkflowEngineTileManagerExtensions": {e102a6c1-807c-4976-b2e7-180404090e75}.
   */
   readonly ID: guid = 'e102a6c1-807c-4976-b2e7-180404090e75';

  /**
   * View name for "WorkflowEngineTileManagerExtensions".
   */
   readonly Alias: string = 'WorkflowEngineTileManagerExtensions';

  /**
   * View caption for "WorkflowEngineTileManagerExtensions".
   */
   readonly Caption: string = '$Views_Names_WorkflowEngineTileManagerExtensions';

  /**
   * View group for "WorkflowEngineTileManagerExtensions".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: ExtensionID.
   */
   readonly ColumnExtensionID: ViewObject = new ViewObject(0, 'ExtensionID');

  /**
   * ID:1
   * Alias: ExtensionName
   * Caption: $Views_WorkflowEngineTileManagerExtensions_Name.
   */
   readonly ColumnExtensionName: ViewObject = new ViewObject(1, 'ExtensionName', '$Views_WorkflowEngineTileManagerExtensions_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_WorkflowEngineTileManagerExtensions_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_WorkflowEngineTileManagerExtensions_Name_Param');

  //#endregion
}

//#endregion

//#region WorkflowLinkModes

/**
 * ID: {cc6b5f26-00f7-4e57-9260-49ed427fd243}
 * Alias: WorkflowLinkModes
 * Caption: $Views_Names_WorkflowLinkModes
 * Group: WorkflowEngine
 */
class WorkflowLinkModesViewInfo {
  //#region Common

  /**
   * View identifier for "WorkflowLinkModes": {cc6b5f26-00f7-4e57-9260-49ed427fd243}.
   */
   readonly ID: guid = 'cc6b5f26-00f7-4e57-9260-49ed427fd243';

  /**
   * View name for "WorkflowLinkModes".
   */
   readonly Alias: string = 'WorkflowLinkModes';

  /**
   * View caption for "WorkflowLinkModes".
   */
   readonly Caption: string = '$Views_Names_WorkflowLinkModes';

  /**
   * View group for "WorkflowLinkModes".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: LinkModeID.
   */
   readonly ColumnLinkModeID: ViewObject = new ViewObject(0, 'LinkModeID');

  /**
   * ID:1
   * Alias: LinkModeName
   * Caption: $Views_WorkflowLinkModes_Name.
   */
   readonly ColumnLinkModeName: ViewObject = new ViewObject(1, 'LinkModeName', '$Views_WorkflowLinkModes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_WorkflowLinkModes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_WorkflowLinkModes_Name_Param');

  //#endregion
}

//#endregion

//#region WorkflowNodeInstanceSubprocesses

/**
 * ID: {a08fa0ff-ec43-4848-9130-b7b5728fc686}
 * Alias: WorkflowNodeInstanceSubprocesses
 * Caption: $Views_Names_WorkflowNodeInstanceSubprocesses
 * Group: WorkflowEngine
 */
class WorkflowNodeInstanceSubprocessesViewInfo {
  //#region Common

  /**
   * View identifier for "WorkflowNodeInstanceSubprocesses": {a08fa0ff-ec43-4848-9130-b7b5728fc686}.
   */
   readonly ID: guid = 'a08fa0ff-ec43-4848-9130-b7b5728fc686';

  /**
   * View name for "WorkflowNodeInstanceSubprocesses".
   */
   readonly Alias: string = 'WorkflowNodeInstanceSubprocesses';

  /**
   * View caption for "WorkflowNodeInstanceSubprocesses".
   */
   readonly Caption: string = '$Views_Names_WorkflowNodeInstanceSubprocesses';

  /**
   * View group for "WorkflowNodeInstanceSubprocesses".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SubprocessID.
   */
   readonly ColumnSubprocessID: ViewObject = new ViewObject(0, 'SubprocessID');

  /**
   * ID:1
   * Alias: SubprocessName.
   */
   readonly ColumnSubprocessName: ViewObject = new ViewObject(1, 'SubprocessName');

  /**
   * ID:2
   * Alias: SubprocessCreated.
   */
   readonly ColumnSubprocessCreated: ViewObject = new ViewObject(2, 'SubprocessCreated');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: NodeInstanceID
   * Caption: Node instance ID.
   */
   readonly ParamNodeInstanceID: ViewObject = new ViewObject(0, 'NodeInstanceID', 'Node instance ID');

  /**
   * ID:1
   * Alias: ProcessInstance
   * Caption: Process instance.
   */
   readonly ParamProcessInstance: ViewObject = new ViewObject(1, 'ProcessInstance', 'Process instance');

  //#endregion
}

//#endregion

//#region WorkflowNodeInstanceTasks

/**
 * ID: {31853f54-cb3d-4004-a79c-a020cc0014c3}
 * Alias: WorkflowNodeInstanceTasks
 * Caption: $Views_Names_WorkflowNodeInstanceTasks
 * Group: WorkflowEngine
 */
class WorkflowNodeInstanceTasksViewInfo {
  //#region Common

  /**
   * View identifier for "WorkflowNodeInstanceTasks": {31853f54-cb3d-4004-a79c-a020cc0014c3}.
   */
   readonly ID: guid = '31853f54-cb3d-4004-a79c-a020cc0014c3';

  /**
   * View name for "WorkflowNodeInstanceTasks".
   */
   readonly Alias: string = 'WorkflowNodeInstanceTasks';

  /**
   * View caption for "WorkflowNodeInstanceTasks".
   */
   readonly Caption: string = '$Views_Names_WorkflowNodeInstanceTasks';

  /**
   * View group for "WorkflowNodeInstanceTasks".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: TaskID.
   */
   readonly ColumnTaskID: ViewObject = new ViewObject(0, 'TaskID');

  /**
   * ID:1
   * Alias: TaskTypeID.
   */
   readonly ColumnTaskTypeID: ViewObject = new ViewObject(1, 'TaskTypeID');

  /**
   * ID:2
   * Alias: TaskTypeCaption.
   */
   readonly ColumnTaskTypeCaption: ViewObject = new ViewObject(2, 'TaskTypeCaption');

  /**
   * ID:3
   * Alias: TaskRoleID.
   */
   readonly ColumnTaskRoleID: ViewObject = new ViewObject(3, 'TaskRoleID');

  /**
   * ID:4
   * Alias: TaskRoleName.
   */
   readonly ColumnTaskRoleName: ViewObject = new ViewObject(4, 'TaskRoleName');

  /**
   * ID:5
   * Alias: TaskUserID.
   */
   readonly ColumnTaskUserID: ViewObject = new ViewObject(5, 'TaskUserID');

  /**
   * ID:6
   * Alias: TaskUserName.
   */
   readonly ColumnTaskUserName: ViewObject = new ViewObject(6, 'TaskUserName');

  /**
   * ID:7
   * Alias: TaskAuthorID.
   */
   readonly ColumnTaskAuthorID: ViewObject = new ViewObject(7, 'TaskAuthorID');

  /**
   * ID:8
   * Alias: TaskAuthorName.
   */
   readonly ColumnTaskAuthorName: ViewObject = new ViewObject(8, 'TaskAuthorName');

  /**
   * ID:9
   * Alias: TaskStateID.
   */
   readonly ColumnTaskStateID: ViewObject = new ViewObject(9, 'TaskStateID');

  /**
   * ID:10
   * Alias: TaskStateName.
   */
   readonly ColumnTaskStateName: ViewObject = new ViewObject(10, 'TaskStateName');

  /**
   * ID:11
   * Alias: TaskDigest.
   */
   readonly ColumnTaskDigest: ViewObject = new ViewObject(11, 'TaskDigest');

  /**
   * ID:12
   * Alias: TaskCreated.
   */
   readonly ColumnTaskCreated: ViewObject = new ViewObject(12, 'TaskCreated');

  /**
   * ID:13
   * Alias: TaskPlanned.
   */
   readonly ColumnTaskPlanned: ViewObject = new ViewObject(13, 'TaskPlanned');

  /**
   * ID:14
   * Alias: TaskInProgress.
   */
   readonly ColumnTaskInProgress: ViewObject = new ViewObject(14, 'TaskInProgress');

  /**
   * ID:15
   * Alias: TaskPostponed.
   */
   readonly ColumnTaskPostponed: ViewObject = new ViewObject(15, 'TaskPostponed');

  /**
   * ID:16
   * Alias: TaskPostponedTo.
   */
   readonly ColumnTaskPostponedTo: ViewObject = new ViewObject(16, 'TaskPostponedTo');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: NodeInstanceID
   * Caption: Node instance ID.
   */
   readonly ParamNodeInstanceID: ViewObject = new ViewObject(0, 'NodeInstanceID', 'Node instance ID');

  /**
   * ID:1
   * Alias: ProcessInstance
   * Caption: Process instance.
   */
   readonly ParamProcessInstance: ViewObject = new ViewObject(1, 'ProcessInstance', 'Process instance');

  /**
   * ID:2
   * Alias: FunctionRoleAuthorParam
   * Caption: $Views_MyTasks_FunctionRole_Author_Param.
   */
   readonly ParamFunctionRoleAuthorParam: ViewObject = new ViewObject(2, 'FunctionRoleAuthorParam', '$Views_MyTasks_FunctionRole_Author_Param');

  /**
   * ID:3
   * Alias: FunctionRolePerformerParam
   * Caption: $Views_MyTasks_FunctionRole_Performer_Param.
   */
   readonly ParamFunctionRolePerformerParam: ViewObject = new ViewObject(3, 'FunctionRolePerformerParam', '$Views_MyTasks_FunctionRole_Performer_Param');

  //#endregion
}

//#endregion

//#region WorkflowSignalProcessingModes

/**
 * ID: {718a1f3a-0a06-490d-8a55-654114c93d54}
 * Alias: WorkflowSignalProcessingModes
 * Caption: $Views_Names_WorkflowSignalProcessingModes
 * Group: WorkflowEngine
 */
class WorkflowSignalProcessingModesViewInfo {
  //#region Common

  /**
   * View identifier for "WorkflowSignalProcessingModes": {718a1f3a-0a06-490d-8a55-654114c93d54}.
   */
   readonly ID: guid = '718a1f3a-0a06-490d-8a55-654114c93d54';

  /**
   * View name for "WorkflowSignalProcessingModes".
   */
   readonly Alias: string = 'WorkflowSignalProcessingModes';

  /**
   * View caption for "WorkflowSignalProcessingModes".
   */
   readonly Caption: string = '$Views_Names_WorkflowSignalProcessingModes';

  /**
   * View group for "WorkflowSignalProcessingModes".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SignalProcessingModeID.
   */
   readonly ColumnSignalProcessingModeID: ViewObject = new ViewObject(0, 'SignalProcessingModeID');

  /**
   * ID:1
   * Alias: SignalProcessingModeName
   * Caption: $Views_WorkflowSignalProcessingModes_Name.
   */
   readonly ColumnSignalProcessingModeName: ViewObject = new ViewObject(1, 'SignalProcessingModeName', '$Views_WorkflowSignalProcessingModes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_WorkflowSignalProcessingModes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_WorkflowSignalProcessingModes_Name_Param');

  //#endregion
}

//#endregion

//#region WorkflowSignalTypes

/**
 * ID: {3bc28139-54ad-45fe-9aa7-83119dc47b62}
 * Alias: WorkflowSignalTypes
 * Caption: $Views_Names_WorkflowSignalTypes
 * Group: WorkflowEngine
 */
class WorkflowSignalTypesViewInfo {
  //#region Common

  /**
   * View identifier for "WorkflowSignalTypes": {3bc28139-54ad-45fe-9aa7-83119dc47b62}.
   */
   readonly ID: guid = '3bc28139-54ad-45fe-9aa7-83119dc47b62';

  /**
   * View name for "WorkflowSignalTypes".
   */
   readonly Alias: string = 'WorkflowSignalTypes';

  /**
   * View caption for "WorkflowSignalTypes".
   */
   readonly Caption: string = '$Views_Names_WorkflowSignalTypes';

  /**
   * View group for "WorkflowSignalTypes".
   */
   readonly Group: string = 'WorkflowEngine';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: SignalTypeID.
   */
   readonly ColumnSignalTypeID: ViewObject = new ViewObject(0, 'SignalTypeID');

  /**
   * ID:1
   * Alias: SignalTypeName
   * Caption: $Views_WorkflowSignalTypes_Name.
   */
   readonly ColumnSignalTypeName: ViewObject = new ViewObject(1, 'SignalTypeName', '$Views_WorkflowSignalTypes_Name');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_WorkflowSignalTypes_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_WorkflowSignalTypes_Name_Param');

  //#endregion
}

//#endregion

//#region Workplaces

/**
 * ID: {36b9cf55-a385-4b3d-84d8-7d251702cc88}
 * Alias: Workplaces
 * Caption: $Views_Names_Workplaces
 * Group: System
 */
class WorkplacesViewInfo {
  //#region Common

  /**
   * View identifier for "Workplaces": {36b9cf55-a385-4b3d-84d8-7d251702cc88}.
   */
   readonly ID: guid = '36b9cf55-a385-4b3d-84d8-7d251702cc88';

  /**
   * View name for "Workplaces".
   */
   readonly Alias: string = 'Workplaces';

  /**
   * View caption for "Workplaces".
   */
   readonly Caption: string = '$Views_Names_Workplaces';

  /**
   * View group for "Workplaces".
   */
   readonly Group: string = 'System';

  //#endregion

  //#region Columns

  /**
   * ID:0
   * Alias: WorkplaceID.
   */
   readonly ColumnWorkplaceID: ViewObject = new ViewObject(0, 'WorkplaceID');

  /**
   * ID:1
   * Alias: WorkplaceName.
   */
   readonly ColumnWorkplaceName: ViewObject = new ViewObject(1, 'WorkplaceName');

  /**
   * ID:2
   * Alias: WorkplaceLocalizedName
   * Caption: $Views_Workplaces_WorkplaceLocalizedName.
   */
   readonly ColumnWorkplaceLocalizedName: ViewObject = new ViewObject(2, 'WorkplaceLocalizedName', '$Views_Workplaces_WorkplaceLocalizedName');

  //#endregion

  //#region Parameters

  /**
   * ID:0
   * Alias: Name
   * Caption: $Views_Workplaces_Name_Param.
   */
   readonly ParamName: ViewObject = new ViewObject(0, 'Name', '$Views_Workplaces_Name_Param');

  //#endregion
}

//#endregion

//#region RefSections

class RefSectionsInfo {
  //#region RefSections

  /**
   * Views:
   * AccessLevels
   */
   readonly AccessLevels: string = 'AccessLevels';

  /**
   * Views:
   * AclGenerationRuleExtensions
   */
   readonly AclGenerationRuleExtensions: string = 'AclGenerationRuleExtensions';

  /**
   * Views:
   * AclForCard
   * AclGenerationRules
   */
   readonly AclGenerationRules: string = 'AclGenerationRules';

  /**
   * Views:
   * AcquaintanceStates
   */
   readonly AcquaintanceStates: string = 'AcquaintanceStates';

  /**
   * Views:
   * ActionHistoryTypes
   */
   readonly ActionHistoryTypes: string = 'ActionHistoryTypes';

  /**
   * Views:
   * ActionHistory
   * Errors
   */
   readonly ActionHistoryVirtual: string = 'ActionHistoryVirtual';

  /**
   * Views:
   * ActionTypes
   */
   readonly ActionTypes: string = 'ActionTypes';

  /**
   * Views:
   * ApplicationArchitectures
   */
   readonly ApplicationArchitectures: string = 'ApplicationArchitectures';

  /**
   * Views:
   * ApplicationNames
   */
   readonly ApplicationNames: string = 'ApplicationNames';

  /**
   * Views:
   * Applications
   */
   readonly Applications: string = 'Applications';

  /**
   * Views:
   * AvailableDeputyRoles
   */
   readonly AvailableDeputyRoles: string = 'AvailableDeputyRoles';

  /**
   * Views:
   * AvailableDeputyUsers
   */
   readonly AvailableDeputyUsers: string = 'AvailableDeputyUsers';

  /**
   * Views:
   * BarcodeTypes
   */
   readonly BarcodeTypes: string = 'BarcodeTypes';

  /**
   * Views:
   * ActiveWorkflows
   * BusinessProcessTemplates
   * ErrorWorkflows
   */
   readonly BusinessProcessInfo: string = 'BusinessProcessInfo';

  /**
   * Views:
   * CalendarCalcMethods
   */
   readonly CalendarCalcMethods: string = 'CalendarCalcMethods';

  /**
   * Views:
   * Calendars
   */
   readonly CalendarSettings: string = 'CalendarSettings';

  /**
   * Views:
   * Calendars
   * CalendarTypes
   */
   readonly CalendarTypes: string = 'CalendarTypes';

  /**
   * Views:
   * CompletionOptionCards
   * CompletionOptions
   */
   readonly CompletionOptions: string = 'CompletionOptions';

  /**
   * Views:
   * ConditionTypes
   */
   readonly ConditionTypes: string = 'ConditionTypes';

  /**
   * Views:
   * ConditionUsePlaces
   */
   readonly ConditionUsePlaces: string = 'ConditionUsePlaces';

  /**
   * Views:
   * Currencies
   */
   readonly Currencies: string = 'Currencies';

  /**
   * Views:
   * DateFormats
   */
   readonly DateFormats: string = 'DateFormats';

  /**
   * Views:
   * Deleted
   */
   readonly Deleted: string = 'Deleted';

  /**
   * Views:
   * Departments
   */
   readonly DepartmentRoles: string = 'DepartmentRoles';

  /**
   * Views:
   * DeviceTypes
   */
   readonly DeviceTypes: string = 'DeviceTypes';

  /**
   * Views:
   * DialogButtonTypes
   */
   readonly DialogButtonTypes: string = 'DialogButtonTypes';

  /**
   * Views:
   * DialogCardAutoOpenModes
   */
   readonly DialogCardAutoOpenModes: string = 'DialogCardAutoOpenModes';

  /**
   * Views:
   * DialogCardStoreModes
   */
   readonly DialogCardStoreModes: string = 'DialogCardStoreModes';

  /**
   * Views:
   * KrDocNumberRegistrationAutoAssigment
   */
   readonly DocNumberRegistrationAutoAssignment: string = 'DocNumberRegistrationAutoAssignment';

  /**
   * Views:
   * KrDocNumberRegularAutoAssigment
   */
   readonly DocNumberRegularAutoAssignment: string = 'DocNumberRegularAutoAssignment';

  /**
   * Views:
   * ContractsDocuments
   * Documents
   * IncomingDocuments
   * LinkedDocuments
   * MyDocuments
   * OutgoingDocuments
   * Protocols
   */
   readonly DocRefsSection: string = 'DocRefsSection';

  /**
   * Views:
   * DocumentCategories
   */
   readonly DocumentCategories: string = 'DocumentCategories';

  /**
   * Views:
   * ContractsDocuments
   * Documents
   * IncomingDocuments
   * LinkedDocuments
   * MyAcquaintanceHistory
   * MyDocuments
   * OutgoingDocuments
   * Protocols
   * RefDocumentsLookup
   */
   readonly DocumentCommonInfo: string = 'DocumentCommonInfo';

  /**
   * Views:
   * DocumentTypes
   */
   readonly DocumentTypes: string = 'DocumentTypes';

  /**
   * Views:
   * AcquaintanceHistory
   * ContractsDocuments
   * ContractsDocuments
   * Departments
   * Departments
   * DeputiesManagement
   * Documents
   * Documents
   * DurableRoles
   * IncomingDocuments
   * LinkedDocuments
   * LinkedDocuments
   * MyDocuments
   * MyDocuments
   * OutgoingDocuments
   * OutgoingDocuments
   * Protocols
   * Protocols
   * TaskAssignedRoleUsers
   * Users
   */
   readonly DurableRoles: string = 'DurableRoles';

  /**
   * Views:
   * FileCategoriesAll
   * FileCategoriesFiltered
   */
   readonly FileCategory: string = 'FileCategory';

  /**
   * Views:
   * FileConverterTypes
   */
   readonly FileConverterTypes: string = 'FileConverterTypes';

  /**
   * Views:
   * FileTemplates
   */
   readonly FileTemplates: string = 'FileTemplates';

  /**
   * Views:
   * FileTemplateTemplateTypes
   */
   readonly FileTemplateTemplateTypes: string = 'FileTemplateTemplateTypes';

  /**
   * Views:
   * FileTemplateTypes
   */
   readonly FileTemplateTypes: string = 'FileTemplateTypes';

  /**
   * Views:
   * FormatSettings
   */
   readonly FormatSettings: string = 'FormatSettings';

  /**
   * Views:
   * FunctionRoleCards
   */
   readonly FunctionRoles: string = 'FunctionRoles';

  /**
   * Views:
   * HelpSections
   */
   readonly HelpSections: string = 'HelpSections';

  /**
   * Views:
   * ActionHistory
   * CompletedTasks
   * Errors
   * LastTopics
   * MyCompletedTasks
   * MyTasks
   * MyTopics
   * NotificationSubscriptions
   * ProtocolCompletedTasks
   * TagCards
   */
   readonly Instances: string = 'Instances';

  /**
   * Views:
   * KrActionTypes
   */
   readonly KrActionTypes: string = 'KrActionTypes';

  /**
   * Views:
   * KrTypesEffective
   */
   readonly KrCardTypesVirtual: string = 'KrCardTypesVirtual';

  /**
   * Views:
   * KrCreateCardStageTypeModes
   */
   readonly KrCreateCardStageTypeModes: string = 'KrCreateCardStageTypeModes';

  /**
   * Views:
   * KrCycleGroupingModes
   */
   readonly KrCycleGroupingModes: string = 'KrCycleGroupingModes';

  /**
   * Views:
   * KrDocStateCards
   * KrDocStates
   */
   readonly KrDocState: string = 'KrDocState';

  /**
   * Views:
   * KrDocTypes
   * KrTypes
   */
   readonly KrDocType: string = 'KrDocType';

  /**
   * Views:
   * KrForkManagementStageTypeModes
   */
   readonly KrForkManagementStageTypeModes: string = 'KrForkManagementStageTypeModes';

  /**
   * Views:
   * KrPermissionAclGenerationRules
   */
   readonly KrPermissionAclGenerationRules: string = 'KrPermissionAclGenerationRules';

  /**
   * Views:
   * KrPermissionFlags
   */
   readonly KrPermissionFlags: string = 'KrPermissionFlags';

  /**
   * Views:
   * KrPermissionRoles
   */
   readonly KrPermissionRoles: string = 'KrPermissionRoles';

  /**
   * Views:
   * KrPermissionRuleAccessSettings
   */
   readonly KrPermissionRuleAccessSettings: string = 'KrPermissionRuleAccessSettings';

  /**
   * Views:
   * KrPermissions
   * KrPermissionsExtendedCards
   * KrPermissionsExtendedFiles
   * KrPermissionsExtendedMandatory
   * KrPermissionsExtendedTasks
   * KrPermissionsExtendedVisibility
   * KrPermissionsReport
   */
   readonly KrPermissions: string = 'KrPermissions';

  /**
   * Views:
   * KrPermissionsControlTypes
   */
   readonly KrPermissionsControlTypes: string = 'KrPermissionsControlTypes';

  /**
   * Views:
   * KrPermissionsFileCheckRules
   */
   readonly KrPermissionsFileCheckRules: string = 'KrPermissionsFileCheckRules';

  /**
   * Views:
   * KrPermissionsFileEditAccessSettings
   */
   readonly KrPermissionsFileEditAccessSettings: string = 'KrPermissionsFileEditAccessSettings';

  /**
   * Views:
   * KrPermissionsFileReadAccessSettings
   */
   readonly KrPermissionsFileReadAccessSettings: string = 'KrPermissionsFileReadAccessSettings';

  /**
   * Views:
   * KrPermissionsMandatoryValidationTypes
   */
   readonly KrPermissionsMandatoryValidationTypes: string = 'KrPermissionsMandatoryValidationTypes';

  /**
   * Views:
   * KrPermissionStates
   */
   readonly KrPermissionStates: string = 'KrPermissionStates';

  /**
   * Views:
   * KrPermissionTypes
   */
   readonly KrPermissionTypes: string = 'KrPermissionTypes';

  /**
   * Views:
   * KrProcessManagementStageTypeModes
   */
   readonly KrProcessManagementStageTypeModes: string = 'KrProcessManagementStageTypeModes';

  /**
   * Views:
   * KrStageTypes
   */
   readonly KrProcessStageTypes: string = 'KrProcessStageTypes';

  /**
   * Views:
   * KrRouteModes
   */
   readonly KrRouteModes: string = 'KrRouteModes';

  /**
   * Views:
   * KrSecondaryProcesses
   */
   readonly KrSecondaryProcess: string = 'KrSecondaryProcess';

  /**
   * Views:
   * KrSecondaryProcessModes
   */
   readonly KrSecondaryProcessModes: string = 'KrSecondaryProcessModes';

  /**
   * Views:
   * KrStageCommonMethods
   */
   readonly KrStageCommonMethods: string = 'KrStageCommonMethods';

  /**
   * Views:
   * KrStageGroups
   */
   readonly KrStageGroups: string = 'KrStageGroups';

  /**
   * Views:
   * KrStageRows
   */
   readonly KrStages: string = 'KrStages';

  /**
   * Views:
   * KrStageTemplateGroupPosition
   */
   readonly KrStageTemplateGroupPosition: string = 'KrStageTemplateGroupPosition';

  /**
   * Views:
   * KrStageTemplates
   */
   readonly KrStageTemplates: string = 'KrStageTemplates';

  /**
   * Views:
   * KrTypesForDialogs
   */
   readonly KrTypesForDialogs: string = 'KrTypesForDialogs';

  /**
   * Views:
   * KrTypesForPermissionsExtension
   */
   readonly KrTypesForPermissionsExtension: string = 'KrTypesForPermissionsExtension';

  /**
   * Views:
   * KrVirtualFiles
   */
   readonly KrVirtualFiles: string = 'KrVirtualFiles';

  /**
   * Views:
   * Languages
   * WebApplications
   */
   readonly Languages: string = 'Languages';

  /**
   * Views:
   * LawCategories
   */
   readonly LawCategories: string = 'LawCategories';

  /**
   * Views:
   * LawClassificationPlans
   */
   readonly LawClassificationPlans: string = 'LawClassificationPlans';

  /**
   * Views:
   * LawClients
   */
   readonly LawClients: string = 'LawClients';

  /**
   * Views:
   * LawDocKinds
   */
   readonly LawDocKinds: string = 'LawDocKinds';

  /**
   * Views:
   * LawDocTypes
   */
   readonly LawDocTypes: string = 'LawDocTypes';

  /**
   * Views:
   * LawEntityKinds
   */
   readonly LawEntityKinds: string = 'LawEntityKinds';

  /**
   * Views:
   * LawFileStorages
   */
   readonly LawFileStorages: string = 'LawFileStorages';

  /**
   * Views:
   * LawPartnerRepresentatives
   */
   readonly LawPartnerRepresentatives: string = 'LawPartnerRepresentatives';

  /**
   * Views:
   * LawPartners
   */
   readonly LawPartners: string = 'LawPartners';

  /**
   * Views:
   * LawStoreLocations
   */
   readonly LawStoreLocations: string = 'LawStoreLocations';

  /**
   * Views:
   * LawUsers
   */
   readonly LawUsers: string = 'LawUsers';

  /**
   * Views:
   * LicenseTypes
   */
   readonly LicenseTypes: string = 'LicenseTypes';

  /**
   * Views:
   * LoginTypes
   */
   readonly LoginTypes: string = 'LoginTypes';

  /**
   * Views:
   * Notifications
   */
   readonly Notifications: string = 'Notifications';

  /**
   * Views:
   * NotificationSubscriptions
   */
   readonly NotificationSubscriptions: string = 'NotificationSubscriptions';

  /**
   * Views:
   * NotificationTypes
   */
   readonly NotificationTypes: string = 'NotificationTypes';

  /**
   * Views:
   * OcrLanguages
   */
   readonly OcrLanguages: string = 'OcrLanguages';

  /**
   * Views:
   * OcrOperations
   */
   readonly OcrOperations: string = 'OcrOperations';

  /**
   * Views:
   * OcrPatternTypes
   */
   readonly OcrPatternTypes: string = 'OcrPatternTypes';

  /**
   * Views:
   * OcrRecognitionModes
   */
   readonly OcrRecognitionModes: string = 'OcrRecognitionModes';

  /**
   * Views:
   * OcrRequests
   */
   readonly OcrRequests: string = 'OcrRequests';

  /**
   * Views:
   * OcrSegmentationModes
   */
   readonly OcrSegmentationModes: string = 'OcrSegmentationModes';

  /**
   * Views:
   * Operations
   */
   readonly Operations: string = 'Operations';

  /**
   * Views:
   * PartnersContacts
   */
   readonly PaCo: string = 'PaCo';

  /**
   * Views:
   * CompletionOptionCards
   * FunctionRoleCards
   * KrDocStateCards
   * Partitions
   */
   readonly Partitions: string = 'Partitions';

  /**
   * Views:
   * ContractsDocuments
   * IncomingDocuments
   * LinkedDocuments
   * OutgoingDocuments
   * Partners
   */
   readonly Partners: string = 'Partners';

  /**
   * Views:
   * PartnersContacts
   */
   readonly PartnersContacts: string = 'PartnersContacts';

  /**
   * Views:
   * PartnersTypes
   */
   readonly PartnersTypes: string = 'PartnersTypes';

  /**
   * Views:
   * AcquaintanceHistory
   * Cars
   * ContractsDocuments
   * ContractsDocuments
   * Deleted
   * Departments
   * DeputiesManagement
   * Documents
   * Documents
   * IncomingDocuments
   * LinkedDocuments
   * LinkedDocuments
   * MyDocuments
   * MyDocuments
   * OutgoingDocuments
   * OutgoingDocuments
   * Protocols
   * Protocols
   * Sessions
   * TaskAssignedRoleUsers
   * Templates
   * Users
   */
   readonly PersonalRoles: string = 'PersonalRoles';

  /**
   * Views:
   * ReportCurrentTasksRules
   */
   readonly ReportRolesRules: string = 'ReportRolesRules';

  /**
   * Views:
   * DeputiesManagement
   */
   readonly RoleDeputiesManagementVirtual: string = 'RoleDeputiesManagementVirtual';

  /**
   * Views:
   * RoleGenerators
   */
   readonly RoleGenerators: string = 'RoleGenerators';

  /**
   * Views:
   * AclForCard
   * AcquaintanceHistory
   * Cars
   * ContractsDocuments
   * ContractsDocuments
   * Deleted
   * Departments
   * Departments
   * DeputiesManagement
   * Documents
   * Documents
   * IncomingDocuments
   * LinkedDocuments
   * LinkedDocuments
   * MyDocuments
   * MyDocuments
   * OutgoingDocuments
   * OutgoingDocuments
   * Protocols
   * Protocols
   * Roles
   * Sessions
   * TaskAssignedRoleUsers
   * Templates
   * TopicParticipants
   * Users
   */
   readonly Roles: string = 'Roles';

  /**
   * Views:
   * RoleTypes
   */
   readonly RoleTypes: string = 'RoleTypes';

  /**
   * Views:
   * Sequences
   */
   readonly SequencesInfo: string = 'SequencesInfo';

  /**
   * Views:
   * Sessions
   */
   readonly Sessions: string = 'Sessions';

  /**
   * Views:
   * SessionServiceTypes
   */
   readonly SessionServiceTypes: string = 'SessionServiceTypes';

  /**
   * Views:
   * SignatureDigestAlgos
   */
   readonly SignatureDigestAlgorithms: string = 'SignatureDigestAlgorithms';

  /**
   * Views:
   * SignatureEncryptionAlgos
   */
   readonly SignatureEncryptionAlgorithms: string = 'SignatureEncryptionAlgorithms';

  /**
   * Views:
   * EdsManagers
   */
   readonly SignatureManagerVirtual: string = 'SignatureManagerVirtual';

  /**
   * Views:
   * SignaturePackagings
   */
   readonly SignaturePackagings: string = 'SignaturePackagings';

  /**
   * Views:
   * SignatureProfiles
   */
   readonly SignatureProfiles: string = 'SignatureProfiles';

  /**
   * Views:
   * SignatureTypes
   */
   readonly SignatureTypes: string = 'SignatureTypes';

  /**
   * Views:
   * SmartRoleGenerators
   */
   readonly SmartRoleGenerators: string = 'SmartRoleGenerators';

  /**
   * Views:
   * MyTags
   * Tags
   */
   readonly Tags: string = 'Tags';

  /**
   * Views:
   * TaskAssignedRoles
   * TaskFunctionRoles
   */
   readonly TaskAssignedRoles: string = 'TaskAssignedRoles';

  /**
   * Views:
   * TaskHistoryGroupTypes
   */
   readonly TaskHistoryGroupTypes: string = 'TaskHistoryGroupTypes';

  /**
   * Views:
   * TaskKinds
   */
   readonly TaskKinds: string = 'TaskKinds';

  /**
   * Views:
   * CardTasks
   */
   readonly Tasks: string = 'Tasks';

  /**
   * Views:
   * TaskStates
   */
   readonly TaskStates: string = 'TaskStates';

  /**
   * Views:
   * TaskTypes
   */
   readonly TaskTypes: string = 'TaskTypes';

  /**
   * Views:
   * Templates
   */
   readonly Templates: string = 'Templates';

  /**
   * Views:
   * EmittedTasks
   */
   readonly tessa_Instances: string = 'tessa_Instances';

  /**
   * Views:
   * Cars
   */
   readonly Test_CarMainInfo: string = 'Test_CarMainInfo';

  /**
   * Views:
   * TileSizes
   */
   readonly TileSizes: string = 'TileSizes';

  /**
   * Views:
   * TimeZones
   */
   readonly TimeZones: string = 'TimeZones';

  /**
   * Views:
   * KrTypesEffective
   */
   readonly TypeForView: string = 'TypeForView';

  /**
   * Views:
   * Types
   */
   readonly Types: string = 'Types';

  /**
   * Views:
   * RoleDeputies
   * RoleDeputies
   * RoleDeputiesManagementDeputized
   * RoleDeputiesNew
   * RoleDeputiesNew
   */
   readonly Users: string = 'Users';

  /**
   * Views:
   * VatTypes
   */
   readonly VatTypes: string = 'VatTypes';

  /**
   * Views:
   * Views
   */
   readonly Views: string = 'Views';

  /**
   * Views:
   * WebApplications
   */
   readonly WebApplications: string = 'WebApplications';

  /**
   * Views:
   * WeTaskControlTypes
   */
   readonly WeTaskControlTypes: string = 'WeTaskControlTypes';

  /**
   * Views:
   * WeTaskGroupActionOptionTypes
   */
   readonly WeTaskGroupActionOptionTypes: string = 'WeTaskGroupActionOptionTypes';

  /**
   * Views:
   * WfResolutionAuthors
   */
   readonly WfResolutionAuthors: string = 'WfResolutionAuthors';

  /**
   * Views:
   * WorkflowEngineCompiledBaseTypes
   */
   readonly WorkflowEngineCompiledBaseTypes: string = 'WorkflowEngineCompiledBaseTypes';

  /**
   * Views:
   * WorkflowEngineLogLevels
   */
   readonly WorkflowEngineLogLevels: string = 'WorkflowEngineLogLevels';

  /**
   * Views:
   * WorkflowEngineTaskActions
   */
   readonly WorkflowEngineTaskActions: string = 'WorkflowEngineTaskActions';

  /**
   * Views:
   * WorkflowEngineTileManagerExtensions
   */
   readonly WorkflowEngineTileManagerExtensions: string = 'WorkflowEngineTileManagerExtensions';

  /**
   * Views:
   * WorkflowLinkModes
   */
   readonly WorkflowLinkModes: string = 'WorkflowLinkModes';

  /**
   * Views:
   * WorkflowSignalProcessingModes
   */
   readonly WorkflowSignalProcessingModes: string = 'WorkflowSignalProcessingModes';

  /**
   * Views:
   * WorkflowSignalTypes
   */
   readonly WorkflowSignalTypes: string = 'WorkflowSignalTypes';

  /**
   * Views:
   * Workplaces
   */
   readonly Workplaces: string = 'Workplaces';

  //#endregion
}

//#endregion

export class ViewInfo {
  //#region Views

  /**
   * View identifier for "$Views_Names_AccessLevels": {55fe2b54-d61b-4b02-9737-3619cfbfd962}.
   */
  static get AccessLevels(): AccessLevelsViewInfo {
    return ViewInfo.accessLevels = ViewInfo.accessLevels ?? new AccessLevelsViewInfo();
  }

  private static accessLevels: AccessLevelsViewInfo;

  /**
   * View identifier for "$Views_Names_AclForCard": {4e53c550-1954-457a-95a6-4b23c3452fb4}.
   */
  static get AclForCard(): AclForCardViewInfo {
    return ViewInfo.aclForCard = ViewInfo.aclForCard ?? new AclForCardViewInfo();
  }

  private static aclForCard: AclForCardViewInfo;

  /**
   * View identifier for "$Views_Names_AclGenerationRuleExtensions": {480df66f-bb9f-4a16-b26c-d1f358c80a4a}.
   */
  static get AclGenerationRuleExtensions(): AclGenerationRuleExtensionsViewInfo {
    return ViewInfo.aclGenerationRuleExtensions = ViewInfo.aclGenerationRuleExtensions ?? new AclGenerationRuleExtensionsViewInfo();
  }

  private static aclGenerationRuleExtensions: AclGenerationRuleExtensionsViewInfo;

  /**
   * View identifier for "$Views_Names_AclGenerationRules": {edf05d46-215d-4c33-826a-568e626f60c6}.
   */
  static get AclGenerationRules(): AclGenerationRulesViewInfo {
    return ViewInfo.aclGenerationRules = ViewInfo.aclGenerationRules ?? new AclGenerationRulesViewInfo();
  }

  private static aclGenerationRules: AclGenerationRulesViewInfo;

  /**
   * View identifier for "$Views_Names_AcquaintanceHistory": {6fe5e33f-4ba9-4378-bd7f-8a2a15f0d838}.
   */
  static get AcquaintanceHistory(): AcquaintanceHistoryViewInfo {
    return ViewInfo.acquaintanceHistory = ViewInfo.acquaintanceHistory ?? new AcquaintanceHistoryViewInfo();
  }

  private static acquaintanceHistory: AcquaintanceHistoryViewInfo;

  /**
   * View identifier for "$Views_Names_AcquaintanceStates": {02f5ab66-8e1f-4c0b-a257-5b53428273e2}.
   */
  static get AcquaintanceStates(): AcquaintanceStatesViewInfo {
    return ViewInfo.acquaintanceStates = ViewInfo.acquaintanceStates ?? new AcquaintanceStatesViewInfo();
  }

  private static acquaintanceStates: AcquaintanceStatesViewInfo;

  /**
   * View identifier for "$Views_Names_ActionHistory": {7b10287f-31bb-4d4c-a515-ae754e452ed3}.
   */
  static get ActionHistory(): ActionHistoryViewInfo {
    return ViewInfo.actionHistory = ViewInfo.actionHistory ?? new ActionHistoryViewInfo();
  }

  private static actionHistory: ActionHistoryViewInfo;

  /**
   * View identifier for "$Views_Names_ActionHistoryTypes": {07775e91-96d5-4b0c-b978-abc26c55d899}.
   */
  static get ActionHistoryTypes(): ActionHistoryTypesViewInfo {
    return ViewInfo.actionHistoryTypes = ViewInfo.actionHistoryTypes ?? new ActionHistoryTypesViewInfo();
  }

  private static actionHistoryTypes: ActionHistoryTypesViewInfo;

  /**
   * View identifier for "$Views_Names_ActionTypes": {12532568-f56f-4399-9a86-5e76871c33aa}.
   */
  static get ActionTypes(): ActionTypesViewInfo {
    return ViewInfo.actionTypes = ViewInfo.actionTypes ?? new ActionTypesViewInfo();
  }

  private static actionTypes: ActionTypesViewInfo;

  /**
   * View identifier for "$Views_Names_ActiveWorkflows": {68def22d-ade6-439f-bbc4-21ea18a3c409}.
   */
  static get ActiveWorkflows(): ActiveWorkflowsViewInfo {
    return ViewInfo.activeWorkflows = ViewInfo.activeWorkflows ?? new ActiveWorkflowsViewInfo();
  }

  private static activeWorkflows: ActiveWorkflowsViewInfo;

  /**
   * View identifier for "$Views_Names_ApplicationArchitectures": {1ff3c65c-4926-4eac-ab0a-0c28f213d482}.
   */
  static get ApplicationArchitectures(): ApplicationArchitecturesViewInfo {
    return ViewInfo.applicationArchitectures = ViewInfo.applicationArchitectures ?? new ApplicationArchitecturesViewInfo();
  }

  private static applicationArchitectures: ApplicationArchitecturesViewInfo;

  /**
   * View identifier for "$Views_Names_ApplicationNames": {1e314e13-904d-491d-93fb-d9f2f912498e}.
   */
  static get ApplicationNames(): ApplicationNamesViewInfo {
    return ViewInfo.applicationNames = ViewInfo.applicationNames ?? new ApplicationNamesViewInfo();
  }

  private static applicationNames: ApplicationNamesViewInfo;

  /**
   * View identifier for "$Views_Names_Applications": {87345860-1b95-4fdd-887e-bf632fb3752d}.
   */
  static get Applications(): ApplicationsViewInfo {
    return ViewInfo.applications = ViewInfo.applications ?? new ApplicationsViewInfo();
  }

  private static applications: ApplicationsViewInfo;

  /**
   * View identifier for "$Views_Names_AvailableApplications": {1a272344-a020-452a-b6e9-3720064ed760}.
   */
  static get AvailableApplications(): AvailableApplicationsViewInfo {
    return ViewInfo.availableApplications = ViewInfo.availableApplications ?? new AvailableApplicationsViewInfo();
  }

  private static availableApplications: AvailableApplicationsViewInfo;

  /**
   * View identifier for "$Views_Names_AvailableDeputyRoles": {530f0463-70bd-4d23-9acc-7e79e1de11af}.
   */
  static get AvailableDeputyRoles(): AvailableDeputyRolesViewInfo {
    return ViewInfo.availableDeputyRoles = ViewInfo.availableDeputyRoles ?? new AvailableDeputyRolesViewInfo();
  }

  private static availableDeputyRoles: AvailableDeputyRolesViewInfo;

  /**
   * View identifier for "$Views_Names_AvailableDeputyUsers": {c7c46016-75e6-46e5-9627-74a4e5e66e29}.
   */
  static get AvailableDeputyUsers(): AvailableDeputyUsersViewInfo {
    return ViewInfo.availableDeputyUsers = ViewInfo.availableDeputyUsers ?? new AvailableDeputyUsersViewInfo();
  }

  private static availableDeputyUsers: AvailableDeputyUsersViewInfo;

  /**
   * View identifier for "$Views_Names_BarcodeTypes": {f92af4c2-e862-4469-9e44-5c96e650e349}.
   */
  static get BarcodeTypes(): BarcodeTypesViewInfo {
    return ViewInfo.barcodeTypes = ViewInfo.barcodeTypes ?? new BarcodeTypesViewInfo();
  }

  private static barcodeTypes: BarcodeTypesViewInfo;

  /**
   * View identifier for "$Views_Names_BusinessProcessTemplates": {b9174abb-c460-4a68-b56e-187d4d8f4896}.
   */
  static get BusinessProcessTemplates(): BusinessProcessTemplatesViewInfo {
    return ViewInfo.businessProcessTemplates = ViewInfo.businessProcessTemplates ?? new BusinessProcessTemplatesViewInfo();
  }

  private static businessProcessTemplates: BusinessProcessTemplatesViewInfo;

  /**
   * View identifier for "$Views_Names_CalendarCalcMethods": {61a516b2-bb7d-41b7-b05c-57a5aeb564ac}.
   */
  static get CalendarCalcMethods(): CalendarCalcMethodsViewInfo {
    return ViewInfo.calendarCalcMethods = ViewInfo.calendarCalcMethods ?? new CalendarCalcMethodsViewInfo();
  }

  private static calendarCalcMethods: CalendarCalcMethodsViewInfo;

  /**
   * View identifier for "$Views_Names_Calendars": {d352f577-8724-4677-a61b-d3e66effd5e1}.
   */
  static get Calendars(): CalendarsViewInfo {
    return ViewInfo.calendars = ViewInfo.calendars ?? new CalendarsViewInfo();
  }

  private static calendars: CalendarsViewInfo;

  /**
   * View identifier for "$Views_Names_CalendarTypes": {422a7b6e-9d7f-4d76-aba1-d3487cae216d}.
   */
  static get CalendarTypes(): CalendarTypesViewInfo {
    return ViewInfo.calendarTypes = ViewInfo.calendarTypes ?? new CalendarTypesViewInfo();
  }

  private static calendarTypes: CalendarTypesViewInfo;

  /**
   * View identifier for "$Views_Names_CardTasks": {eff8e7b5-0874-4e7d-ab09-2537e821b43d}.
   */
  static get CardTasks(): CardTasksViewInfo {
    return ViewInfo.cardTasks = ViewInfo.cardTasks ?? new CardTasksViewInfo();
  }

  private static cardTasks: CardTasksViewInfo;

  /**
   * View identifier for "$Views_Names_CardTaskSessionRoles": {088b9367-ca87-46b4-a9e2-336b0a183a8d}.
   */
  static get CardTaskSessionRoles(): CardTaskSessionRolesViewInfo {
    return ViewInfo.cardTaskSessionRoles = ViewInfo.cardTaskSessionRoles ?? new CardTaskSessionRolesViewInfo();
  }

  private static cardTaskSessionRoles: CardTaskSessionRolesViewInfo;

  /**
   * View identifier for "$Views_Names_Cars": {257b72ba-9bba-457a-8456-d90d55d440e2}.
   */
  static get Cars(): CarsViewInfo {
    return ViewInfo.cars = ViewInfo.cars ?? new CarsViewInfo();
  }

  private static cars: CarsViewInfo;

  /**
   * View identifier for "$Views_Names_CompletedTasks": {c480683b-b3b4-4f8a-8786-3899b5bf7f00}.
   */
  static get CompletedTasks(): CompletedTasksViewInfo {
    return ViewInfo.completedTasks = ViewInfo.completedTasks ?? new CompletedTasksViewInfo();
  }

  private static completedTasks: CompletedTasksViewInfo;

  /**
   * View identifier for "$Views_Names_CompletionOptionCards": {f74f5397-74b2-4b55-8d4e-2cc3031f35af}.
   */
  static get CompletionOptionCards(): CompletionOptionCardsViewInfo {
    return ViewInfo.completionOptionCards = ViewInfo.completionOptionCards ?? new CompletionOptionCardsViewInfo();
  }

  private static completionOptionCards: CompletionOptionCardsViewInfo;

  /**
   * View identifier for "$Views_Names_CompletionOptions": {7aa4bb6b-2bd0-469b-aac4-90c46c2d3502}.
   */
  static get CompletionOptions(): CompletionOptionsViewInfo {
    return ViewInfo.completionOptions = ViewInfo.completionOptions ?? new CompletionOptionsViewInfo();
  }

  private static completionOptions: CompletionOptionsViewInfo;

  /**
   * View identifier for "$Views_Names_ConditionTypes": {ecb69da2-2b28-41dd-b56d-941dd12df77b}.
   */
  static get ConditionTypes(): ConditionTypesViewInfo {
    return ViewInfo.conditionTypes = ViewInfo.conditionTypes ?? new ConditionTypesViewInfo();
  }

  private static conditionTypes: ConditionTypesViewInfo;

  /**
   * View identifier for "$Views_Names_ConditionUsePlaces": {c0b966a6-aa3a-4ea6-b5ab-a6084099cc1f}.
   */
  static get ConditionUsePlaces(): ConditionUsePlacesViewInfo {
    return ViewInfo.conditionUsePlaces = ViewInfo.conditionUsePlaces ?? new ConditionUsePlacesViewInfo();
  }

  private static conditionUsePlaces: ConditionUsePlacesViewInfo;

  /**
   * View identifier for "$Views_Names_ContractsDocuments": {24f43f33-9b1b-476d-aa33-3deb11b9fe3b}.
   */
  static get ContractsDocuments(): ContractsDocumentsViewInfo {
    return ViewInfo.contractsDocuments = ViewInfo.contractsDocuments ?? new ContractsDocumentsViewInfo();
  }

  private static contractsDocuments: ContractsDocumentsViewInfo;

  /**
   * View identifier for "$Views_Names_CreateFileFromTemplate": {9334eab6-f2b7-4c35-b0ff-bf764cd0092c}.
   */
  static get CreateFileFromTemplate(): CreateFileFromTemplateViewInfo {
    return ViewInfo.createFileFromTemplate = ViewInfo.createFileFromTemplate ?? new CreateFileFromTemplateViewInfo();
  }

  private static createFileFromTemplate: CreateFileFromTemplateViewInfo;

  /**
   * View identifier for "$Views_Names_Currencies": {67e0e026-8dbd-462a-93fa-9ec03636564f}.
   */
  static get Currencies(): CurrenciesViewInfo {
    return ViewInfo.currencies = ViewInfo.currencies ?? new CurrenciesViewInfo();
  }

  private static currencies: CurrenciesViewInfo;

  /**
   * View identifier for "$Views_Names_DateFormats": {10ad5b14-16cd-4c8c-ad1f-63c24daeb00c}.
   */
  static get DateFormats(): DateFormatsViewInfo {
    return ViewInfo.dateFormats = ViewInfo.dateFormats ?? new DateFormatsViewInfo();
  }

  private static dateFormats: DateFormatsViewInfo;

  /**
   * View identifier for "$Views_Names_Deleted": {52c2fe9f-b0a8-455a-b426-ab5bc2285a05}.
   */
  static get Deleted(): DeletedViewInfo {
    return ViewInfo.deleted = ViewInfo.deleted ?? new DeletedViewInfo();
  }

  private static deleted: DeletedViewInfo;

  /**
   * View identifier for "$Views_Names_Departments": {ab58bf23-b9d7-4b51-97c1-c9517daa7993}.
   */
  static get Departments(): DepartmentsViewInfo {
    return ViewInfo.departments = ViewInfo.departments ?? new DepartmentsViewInfo();
  }

  private static departments: DepartmentsViewInfo;

  /**
   * View identifier for "$Views_Names_DeputiesManagement": {4c2769bc-89ca-4b08-bc53-d6ee93a45b95}.
   */
  static get DeputiesManagement(): DeputiesManagementViewInfo {
    return ViewInfo.deputiesManagement = ViewInfo.deputiesManagement ?? new DeputiesManagementViewInfo();
  }

  private static deputiesManagement: DeputiesManagementViewInfo;

  /**
   * View identifier for "$Views_Names_DeviceTypes": {4a9aaa12-6830-4dc5-bd0d-c31415f7a306}.
   */
  static get DeviceTypes(): DeviceTypesViewInfo {
    return ViewInfo.deviceTypes = ViewInfo.deviceTypes ?? new DeviceTypesViewInfo();
  }

  private static deviceTypes: DeviceTypesViewInfo;

  /**
   * View identifier for "$Views_Names_DialogButtonTypes": {bf4ac076-a8b3-4271-867e-f8f7ae9287a6}.
   */
  static get DialogButtonTypes(): DialogButtonTypesViewInfo {
    return ViewInfo.dialogButtonTypes = ViewInfo.dialogButtonTypes ?? new DialogButtonTypesViewInfo();
  }

  private static dialogButtonTypes: DialogButtonTypesViewInfo;

  /**
   * View identifier for "$Views_Names_DialogCardAutoOpenModes": {115854cc-71a8-45ae-ab4f-940962b332c6}.
   */
  static get DialogCardAutoOpenModes(): DialogCardAutoOpenModesViewInfo {
    return ViewInfo.dialogCardAutoOpenModes = ViewInfo.dialogCardAutoOpenModes ?? new DialogCardAutoOpenModesViewInfo();
  }

  private static dialogCardAutoOpenModes: DialogCardAutoOpenModesViewInfo;

  /**
   * View identifier for "$Views_Names_DialogCardStoreModes": {ad759faa-bfc1-4cd1-a322-f1eb1a42b3bc}.
   */
  static get DialogCardStoreModes(): DialogCardStoreModesViewInfo {
    return ViewInfo.dialogCardStoreModes = ViewInfo.dialogCardStoreModes ?? new DialogCardStoreModesViewInfo();
  }

  private static dialogCardStoreModes: DialogCardStoreModesViewInfo;

  /**
   * View identifier for "$Views_Names_DocumentCategories": {15fc8ec2-f206-4de1-b942-1c29c931213f}.
   */
  static get DocumentCategories(): DocumentCategoriesViewInfo {
    return ViewInfo.documentCategories = ViewInfo.documentCategories ?? new DocumentCategoriesViewInfo();
  }

  private static documentCategories: DocumentCategoriesViewInfo;

  /**
   * View identifier for "$Views_Names_Documents": {8354ee75-639a-4084-808d-cf97a2b86be9}.
   */
  static get Documents(): DocumentsViewInfo {
    return ViewInfo.documents = ViewInfo.documents ?? new DocumentsViewInfo();
  }

  private static documents: DocumentsViewInfo;

  /**
   * View identifier for "$Views_Names_DocumentTypes": {b05eebcc-eb4b-4f5c-b4b8-d8bd134e27c6}.
   */
  static get DocumentTypes(): DocumentTypesViewInfo {
    return ViewInfo.documentTypes = ViewInfo.documentTypes ?? new DocumentTypesViewInfo();
  }

  private static documentTypes: DocumentTypesViewInfo;

  /**
   * View identifier for "$Views_Names_DurableRoles": {8144d12b-ac9b-4da7-a21c-4ad1ca355dbe}.
   */
  static get DurableRoles(): DurableRolesViewInfo {
    return ViewInfo.durableRoles = ViewInfo.durableRoles ?? new DurableRolesViewInfo();
  }

  private static durableRoles: DurableRolesViewInfo;

  /**
   * View identifier for "$Views_Names_EdsManagers": {18d94578-ee8e-49f7-9fbb-50b3ad3bf76b}.
   */
  static get EdsManagers(): EdsManagersViewInfo {
    return ViewInfo.edsManagers = ViewInfo.edsManagers ?? new EdsManagersViewInfo();
  }

  private static edsManagers: EdsManagersViewInfo;

  /**
   * View identifier for "$Views_Names_EmittedTasks": {b6e14161-038f-4060-bd35-66ba13da2cb8}.
   */
  static get EmittedTasks(): EmittedTasksViewInfo {
    return ViewInfo.emittedTasks = ViewInfo.emittedTasks ?? new EmittedTasksViewInfo();
  }

  private static emittedTasks: EmittedTasksViewInfo;

  /**
   * View identifier for "$Views_Names_Errors": {e1307d4f-a74d-460b-bdd9-e5d8644f98da}.
   */
  static get Errors(): ErrorsViewInfo {
    return ViewInfo.errors = ViewInfo.errors ?? new ErrorsViewInfo();
  }

  private static errors: ErrorsViewInfo;

  /**
   * View identifier for "$Views_Names_ErrorWorkflows": {91bae5ac-e846-4b71-a87b-3cee38381c66}.
   */
  static get ErrorWorkflows(): ErrorWorkflowsViewInfo {
    return ViewInfo.errorWorkflows = ViewInfo.errorWorkflows ?? new ErrorWorkflowsViewInfo();
  }

  private static errorWorkflows: ErrorWorkflowsViewInfo;

  /**
   * View identifier for "$Views_Names_FileCategoriesAll": {f44a1e46-8b4b-43c7-bb9b-2f88507400db}.
   */
  static get FileCategoriesAll(): FileCategoriesAllViewInfo {
    return ViewInfo.fileCategoriesAll = ViewInfo.fileCategoriesAll ?? new FileCategoriesAllViewInfo();
  }

  private static fileCategoriesAll: FileCategoriesAllViewInfo;

  /**
   * View identifier for "$Views_Names_FileCategoriesFiltered": {c54a9c60-2010-4806-9c52-a117baef7643}.
   */
  static get FileCategoriesFiltered(): FileCategoriesFilteredViewInfo {
    return ViewInfo.fileCategoriesFiltered = ViewInfo.fileCategoriesFiltered ?? new FileCategoriesFilteredViewInfo();
  }

  private static fileCategoriesFiltered: FileCategoriesFilteredViewInfo;

  /**
   * View identifier for "$Views_Names_FileConverterTypes": {cab50857-4492-4521-8779-9ef0f5055b44}.
   */
  static get FileConverterTypes(): FileConverterTypesViewInfo {
    return ViewInfo.fileConverterTypes = ViewInfo.fileConverterTypes ?? new FileConverterTypesViewInfo();
  }

  private static fileConverterTypes: FileConverterTypesViewInfo;

  /**
   * View identifier for "$Views_Names_FileTemplates": {86b47fab-4522-4d84-9cf1-d0db3fd06c75}.
   */
  static get FileTemplates(): FileTemplatesViewInfo {
    return ViewInfo.fileTemplates = ViewInfo.fileTemplates ?? new FileTemplatesViewInfo();
  }

  private static fileTemplates: FileTemplatesViewInfo;

  /**
   * View identifier for "$Views_Names_FileTemplateTemplateTypes": {91a427d5-4dd9-4a7f-b35f-cb48de3254d0}.
   */
  static get FileTemplateTemplateTypes(): FileTemplateTemplateTypesViewInfo {
    return ViewInfo.fileTemplateTemplateTypes = ViewInfo.fileTemplateTemplateTypes ?? new FileTemplateTemplateTypesViewInfo();
  }

  private static fileTemplateTemplateTypes: FileTemplateTemplateTypesViewInfo;

  /**
   * View identifier for "$Views_Names_FileTemplateTypes": {eb59292c-378c-412f-b780-88469049a349}.
   */
  static get FileTemplateTypes(): FileTemplateTypesViewInfo {
    return ViewInfo.fileTemplateTypes = ViewInfo.fileTemplateTypes ?? new FileTemplateTypesViewInfo();
  }

  private static fileTemplateTypes: FileTemplateTypesViewInfo;

  /**
   * View identifier for "$Views_Names_FormatSettings": {038628a6-a2c0-4276-a986-2ab73428ca42}.
   */
  static get FormatSettings(): FormatSettingsViewInfo {
    return ViewInfo.formatSettings = ViewInfo.formatSettings ?? new FormatSettingsViewInfo();
  }

  private static formatSettings: FormatSettingsViewInfo;

  /**
   * View identifier for "$Views_Names_FunctionRoleCards": {6693a0e4-421c-484f-a4ce-21be436a4be2}.
   */
  static get FunctionRoleCards(): FunctionRoleCardsViewInfo {
    return ViewInfo.functionRoleCards = ViewInfo.functionRoleCards ?? new FunctionRoleCardsViewInfo();
  }

  private static functionRoleCards: FunctionRoleCardsViewInfo;

  /**
   * View identifier for "$Views_Names_GetCardIDView": {07100666-36ac-49e3-ae68-e53caafb45a2}.
   */
  static get GetCardIDView(): GetCardIDViewViewInfo {
    return ViewInfo.getCardIDView = ViewInfo.getCardIDView ?? new GetCardIDViewViewInfo();
  }

  private static getCardIDView: GetCardIDViewViewInfo;

  /**
   * View identifier for "$Views_Names_GetFileNameView": {1eb7431c-32f1-4ed6-bf71-57a842d61949}.
   */
  static get GetFileNameView(): GetFileNameViewViewInfo {
    return ViewInfo.getFileNameView = ViewInfo.getFileNameView ?? new GetFileNameViewViewInfo();
  }

  private static getFileNameView: GetFileNameViewViewInfo;

  /**
   * View identifier for "$Views_Names_Groups": {4f179970-84b9-4b6a-b921-72cc79ca2cb3}.
   */
  static get Groups(): GroupsViewInfo {
    return ViewInfo.groups = ViewInfo.groups ?? new GroupsViewInfo();
  }

  private static groups: GroupsViewInfo;

  /**
   * View identifier for "$Views_Names_GroupsWithHierarchy": {5de1d29e-41ce-4279-90d5-573d2a81f009}.
   */
  static get GroupsWithHierarchy(): GroupsWithHierarchyViewInfo {
    return ViewInfo.groupsWithHierarchy = ViewInfo.groupsWithHierarchy ?? new GroupsWithHierarchyViewInfo();
  }

  private static groupsWithHierarchy: GroupsWithHierarchyViewInfo;

  /**
   * View identifier for "$Views_Names_HelpSections": {c35e6ac1-9cec-482d-a20f-b3c330f2dc53}.
   */
  static get HelpSections(): HelpSectionsViewInfo {
    return ViewInfo.helpSections = ViewInfo.helpSections ?? new HelpSectionsViewInfo();
  }

  private static helpSections: HelpSectionsViewInfo;

  /**
   * View identifier for "$Views_Names_Hierarchy": {29929a97-79f8-4eda-a6ee-b9621aa9ae49}.
   */
  static get Hierarchy(): HierarchyViewInfo {
    return ViewInfo.hierarchy = ViewInfo.hierarchy ?? new HierarchyViewInfo();
  }

  private static hierarchy: HierarchyViewInfo;

  /**
   * View identifier for "$Views_Names_IncomingDocuments": {a1889a97-2c55-489a-a942-fa36b61dff04}.
   */
  static get IncomingDocuments(): IncomingDocumentsViewInfo {
    return ViewInfo.incomingDocuments = ViewInfo.incomingDocuments ?? new IncomingDocumentsViewInfo();
  }

  private static incomingDocuments: IncomingDocumentsViewInfo;

  /**
   * View identifier for "$Views_Names_KrActionTypes": {73ad84ae-84b6-4292-b496-5bf63cf9033e}.
   */
  static get KrActionTypes(): KrActionTypesViewInfo {
    return ViewInfo.krActionTypes = ViewInfo.krActionTypes ?? new KrActionTypesViewInfo();
  }

  private static krActionTypes: KrActionTypesViewInfo;

  /**
   * View identifier for "$Views_Names_KrCreateCardStageTypeModes": {1e8ca3c1-72a3-4c2c-8740-01c4dd96de39}.
   */
  static get KrCreateCardStageTypeModes(): KrCreateCardStageTypeModesViewInfo {
    return ViewInfo.krCreateCardStageTypeModes = ViewInfo.krCreateCardStageTypeModes ?? new KrCreateCardStageTypeModesViewInfo();
  }

  private static krCreateCardStageTypeModes: KrCreateCardStageTypeModesViewInfo;

  /**
   * View identifier for "$Views_Names_KrCycleGroupingModes": {ac33bea6-af04-4e73-b9ca-5fedb8fcf64f}.
   */
  static get KrCycleGroupingModes(): KrCycleGroupingModesViewInfo {
    return ViewInfo.krCycleGroupingModes = ViewInfo.krCycleGroupingModes ?? new KrCycleGroupingModesViewInfo();
  }

  private static krCycleGroupingModes: KrCycleGroupingModesViewInfo;

  /**
   * View identifier for "$Views_Names_KrDocNumberRegistrationAutoAssigment": {65867469-2aec-4c13-a807-a2784a023d6b}.
   */
  static get KrDocNumberRegistrationAutoAssigment(): KrDocNumberRegistrationAutoAssigmentViewInfo {
    return ViewInfo.krDocNumberRegistrationAutoAssigment = ViewInfo.krDocNumberRegistrationAutoAssigment ?? new KrDocNumberRegistrationAutoAssigmentViewInfo();
  }

  private static krDocNumberRegistrationAutoAssigment: KrDocNumberRegistrationAutoAssigmentViewInfo;

  /**
   * View identifier for "$Views_Names_KrDocNumberRegularAutoAssigment": {021327d4-1e7a-4834-bbc8-6cafd415f098}.
   */
  static get KrDocNumberRegularAutoAssigment(): KrDocNumberRegularAutoAssigmentViewInfo {
    return ViewInfo.krDocNumberRegularAutoAssigment = ViewInfo.krDocNumberRegularAutoAssigment ?? new KrDocNumberRegularAutoAssigmentViewInfo();
  }

  private static krDocNumberRegularAutoAssigment: KrDocNumberRegularAutoAssigmentViewInfo;

  /**
   * View identifier for "$Views_Names_KrDocStateCards": {d9534c6c-ec26-4de9-be78-5df833b70f43}.
   */
  static get KrDocStateCards(): KrDocStateCardsViewInfo {
    return ViewInfo.krDocStateCards = ViewInfo.krDocStateCards ?? new KrDocStateCardsViewInfo();
  }

  private static krDocStateCards: KrDocStateCardsViewInfo;

  /**
   * View identifier for "$Views_Names_KrDocStates": {51e63141-3564-4819-8bb8-c324cf772aae}.
   */
  static get KrDocStates(): KrDocStatesViewInfo {
    return ViewInfo.krDocStates = ViewInfo.krDocStates ?? new KrDocStatesViewInfo();
  }

  private static krDocStates: KrDocStatesViewInfo;

  /**
   * View identifier for "$Views_Names_KrDocTypes": {f85d195b-7e93-4c09-830c-c9564c450f23}.
   */
  static get KrDocTypes(): KrDocTypesViewInfo {
    return ViewInfo.krDocTypes = ViewInfo.krDocTypes ?? new KrDocTypesViewInfo();
  }

  private static krDocTypes: KrDocTypesViewInfo;

  /**
   * View identifier for "$Views_Names_KrFilteredStageGroups": {888b6543-2e85-45d3-8325-7a80f230560f}.
   */
  static get KrFilteredStageGroups(): KrFilteredStageGroupsViewInfo {
    return ViewInfo.krFilteredStageGroups = ViewInfo.krFilteredStageGroups ?? new KrFilteredStageGroupsViewInfo();
  }

  private static krFilteredStageGroups: KrFilteredStageGroupsViewInfo;

  /**
   * View identifier for "$Views_Names_KrFilteredStageTypes": {e046d0cf-be7c-4965-b2d4-47cb943a8a7d}.
   */
  static get KrFilteredStageTypes(): KrFilteredStageTypesViewInfo {
    return ViewInfo.krFilteredStageTypes = ViewInfo.krFilteredStageTypes ?? new KrFilteredStageTypesViewInfo();
  }

  private static krFilteredStageTypes: KrFilteredStageTypesViewInfo;

  /**
   * View identifier for "$Views_Names_KrForkManagementStageTypeModes": {d5a3ccbd-975c-42de-993c-e4bccfd2ac0d}.
   */
  static get KrForkManagementStageTypeModes(): KrForkManagementStageTypeModesViewInfo {
    return ViewInfo.krForkManagementStageTypeModes = ViewInfo.krForkManagementStageTypeModes ?? new KrForkManagementStageTypeModesViewInfo();
  }

  private static krForkManagementStageTypeModes: KrForkManagementStageTypeModesViewInfo;

  /**
   * View identifier for "$Views_Names_KrManagerTasks": {98e09dab-c265-46e0-96ae-0a81cef3fa20}.
   */
  static get KrManagerTasks(): KrManagerTasksViewInfo {
    return ViewInfo.krManagerTasks = ViewInfo.krManagerTasks ?? new KrManagerTasksViewInfo();
  }

  private static krManagerTasks: KrManagerTasksViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionAclGenerationRules": {8adc6a95-fd78-4efa-922d-43c4c4838e39}.
   */
  static get KrPermissionAclGenerationRules(): KrPermissionAclGenerationRulesViewInfo {
    return ViewInfo.krPermissionAclGenerationRules = ViewInfo.krPermissionAclGenerationRules ?? new KrPermissionAclGenerationRulesViewInfo();
  }

  private static krPermissionAclGenerationRules: KrPermissionAclGenerationRulesViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionFlags": {eb357452-657c-40cd-a2f4-f0214d0ac957}.
   */
  static get KrPermissionFlags(): KrPermissionFlagsViewInfo {
    return ViewInfo.krPermissionFlags = ViewInfo.krPermissionFlags ?? new KrPermissionFlagsViewInfo();
  }

  private static krPermissionFlags: KrPermissionFlagsViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionRoles": {34026b2a-6699-425c-8669-5ad5c75945f9}.
   */
  static get KrPermissionRoles(): KrPermissionRolesViewInfo {
    return ViewInfo.krPermissionRoles = ViewInfo.krPermissionRoles ?? new KrPermissionRolesViewInfo();
  }

  private static krPermissionRoles: KrPermissionRolesViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionRuleAccessSettings": {e9005e6d-e9d0-4643-86aa-8f0c72826e28}.
   */
  static get KrPermissionRuleAccessSettings(): KrPermissionRuleAccessSettingsViewInfo {
    return ViewInfo.krPermissionRuleAccessSettings = ViewInfo.krPermissionRuleAccessSettings ?? new KrPermissionRuleAccessSettingsViewInfo();
  }

  private static krPermissionRuleAccessSettings: KrPermissionRuleAccessSettingsViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissions": {42facec2-7986-4456-b089-972413cf8e89}.
   */
  static get KrPermissions(): KrPermissionsViewInfo {
    return ViewInfo.krPermissions = ViewInfo.krPermissions ?? new KrPermissionsViewInfo();
  }

  private static krPermissions: KrPermissionsViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionsControlTypes": {8053d28d-666a-4997-b0ef-aff1298c4aaf}.
   */
  static get KrPermissionsControlTypes(): KrPermissionsControlTypesViewInfo {
    return ViewInfo.krPermissionsControlTypes = ViewInfo.krPermissionsControlTypes ?? new KrPermissionsControlTypesViewInfo();
  }

  private static krPermissionsControlTypes: KrPermissionsControlTypesViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionsExtendedCards": {1175f833-a028-481a-be77-69bab81a01a2}.
   */
  static get KrPermissionsExtendedCards(): KrPermissionsExtendedCardsViewInfo {
    return ViewInfo.krPermissionsExtendedCards = ViewInfo.krPermissionsExtendedCards ?? new KrPermissionsExtendedCardsViewInfo();
  }

  private static krPermissionsExtendedCards: KrPermissionsExtendedCardsViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionsExtendedFiles": {76a033f1-404b-4dee-9305-9cc8bf8c22f0}.
   */
  static get KrPermissionsExtendedFiles(): KrPermissionsExtendedFilesViewInfo {
    return ViewInfo.krPermissionsExtendedFiles = ViewInfo.krPermissionsExtendedFiles ?? new KrPermissionsExtendedFilesViewInfo();
  }

  private static krPermissionsExtendedFiles: KrPermissionsExtendedFilesViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionsExtendedMandatory": {73970cb5-843d-49ec-821f-cb069463c1aa}.
   */
  static get KrPermissionsExtendedMandatory(): KrPermissionsExtendedMandatoryViewInfo {
    return ViewInfo.krPermissionsExtendedMandatory = ViewInfo.krPermissionsExtendedMandatory ?? new KrPermissionsExtendedMandatoryViewInfo();
  }

  private static krPermissionsExtendedMandatory: KrPermissionsExtendedMandatoryViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionsExtendedTasks": {3d026d41-fe09-4b2d-bd5f-bb482c6c7726}.
   */
  static get KrPermissionsExtendedTasks(): KrPermissionsExtendedTasksViewInfo {
    return ViewInfo.krPermissionsExtendedTasks = ViewInfo.krPermissionsExtendedTasks ?? new KrPermissionsExtendedTasksViewInfo();
  }

  private static krPermissionsExtendedTasks: KrPermissionsExtendedTasksViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionsExtendedVisibility": {acbfb44c-d180-4b5b-9719-5868631b998a}.
   */
  static get KrPermissionsExtendedVisibility(): KrPermissionsExtendedVisibilityViewInfo {
    return ViewInfo.krPermissionsExtendedVisibility = ViewInfo.krPermissionsExtendedVisibility ?? new KrPermissionsExtendedVisibilityViewInfo();
  }

  private static krPermissionsExtendedVisibility: KrPermissionsExtendedVisibilityViewInfo;

  /**
   * View identifier for "$Views_KrPermissionsFileCheckRules": {2215eeaa-790a-4389-b800-7790487318aa}.
   */
  static get KrPermissionsFileCheckRules(): KrPermissionsFileCheckRulesViewInfo {
    return ViewInfo.krPermissionsFileCheckRules = ViewInfo.krPermissionsFileCheckRules ?? new KrPermissionsFileCheckRulesViewInfo();
  }

  private static krPermissionsFileCheckRules: KrPermissionsFileCheckRulesViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionsFileEditAccessSettings": {e0588f6d-d7e0-4c3b-bf90-82b898bd512b}.
   */
  static get KrPermissionsFileEditAccessSettings(): KrPermissionsFileEditAccessSettingsViewInfo {
    return ViewInfo.krPermissionsFileEditAccessSettings = ViewInfo.krPermissionsFileEditAccessSettings ?? new KrPermissionsFileEditAccessSettingsViewInfo();
  }

  private static krPermissionsFileEditAccessSettings: KrPermissionsFileEditAccessSettingsViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionsFileReadAccessSettings": {e8b1f86f-b19e-426f-8703-d87359d75c32}.
   */
  static get KrPermissionsFileReadAccessSettings(): KrPermissionsFileReadAccessSettingsViewInfo {
    return ViewInfo.krPermissionsFileReadAccessSettings = ViewInfo.krPermissionsFileReadAccessSettings ?? new KrPermissionsFileReadAccessSettingsViewInfo();
  }

  private static krPermissionsFileReadAccessSettings: KrPermissionsFileReadAccessSettingsViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionsMandatoryValidationTypes": {ea16e82d-f10a-4897-90f6-a9caf61ce9cc}.
   */
  static get KrPermissionsMandatoryValidationTypes(): KrPermissionsMandatoryValidationTypesViewInfo {
    return ViewInfo.krPermissionsMandatoryValidationTypes = ViewInfo.krPermissionsMandatoryValidationTypes ?? new KrPermissionsMandatoryValidationTypesViewInfo();
  }

  private static krPermissionsMandatoryValidationTypes: KrPermissionsMandatoryValidationTypesViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionsReport": {cb1362ac-3a78-4afc-baec-c585e570955a}.
   */
  static get KrPermissionsReport(): KrPermissionsReportViewInfo {
    return ViewInfo.krPermissionsReport = ViewInfo.krPermissionsReport ?? new KrPermissionsReportViewInfo();
  }

  private static krPermissionsReport: KrPermissionsReportViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionStates": {44026b2a-6699-425c-8669-5ad5c75945f9}.
   */
  static get KrPermissionStates(): KrPermissionStatesViewInfo {
    return ViewInfo.krPermissionStates = ViewInfo.krPermissionStates ?? new KrPermissionStatesViewInfo();
  }

  private static krPermissionStates: KrPermissionStatesViewInfo;

  /**
   * View identifier for "$Views_Names_KrPermissionTypes": {54026b2a-6699-425c-8669-5ad5c75945f9}.
   */
  static get KrPermissionTypes(): KrPermissionTypesViewInfo {
    return ViewInfo.krPermissionTypes = ViewInfo.krPermissionTypes ?? new KrPermissionTypesViewInfo();
  }

  private static krPermissionTypes: KrPermissionTypesViewInfo;

  /**
   * View identifier for "$Views_Names_KrProcessManagementStageTypeModes": {20036792-579c-4228-b2f9-79495f326f06}.
   */
  static get KrProcessManagementStageTypeModes(): KrProcessManagementStageTypeModesViewInfo {
    return ViewInfo.krProcessManagementStageTypeModes = ViewInfo.krProcessManagementStageTypeModes ?? new KrProcessManagementStageTypeModesViewInfo();
  }

  private static krProcessManagementStageTypeModes: KrProcessManagementStageTypeModesViewInfo;

  /**
   * View identifier for "$Views_Names_KrRouteModes": {3179625f-bf5e-478a-ba44-07fd2babede7}.
   */
  static get KrRouteModes(): KrRouteModesViewInfo {
    return ViewInfo.krRouteModes = ViewInfo.krRouteModes ?? new KrRouteModesViewInfo();
  }

  private static krRouteModes: KrRouteModesViewInfo;

  /**
   * View identifier for "$Views_Names_KrSecondaryProcesses": {e824a33b-2194-4713-a42f-89ac225e0141}.
   */
  static get KrSecondaryProcesses(): KrSecondaryProcessesViewInfo {
    return ViewInfo.krSecondaryProcesses = ViewInfo.krSecondaryProcesses ?? new KrSecondaryProcessesViewInfo();
  }

  private static krSecondaryProcesses: KrSecondaryProcessesViewInfo;

  /**
   * View identifier for "$Views_Names_KrSecondaryProcessModes": {4f6f0744-5a4e-4285-b39e-064c56737715}.
   */
  static get KrSecondaryProcessModes(): KrSecondaryProcessModesViewInfo {
    return ViewInfo.krSecondaryProcessModes = ViewInfo.krSecondaryProcessModes ?? new KrSecondaryProcessModesViewInfo();
  }

  private static krSecondaryProcessModes: KrSecondaryProcessModesViewInfo;

  /**
   * View identifier for "$Views_Names_KrStageCommonMethods": {3b31f77a-1667-443c-b4f0-9bdb04798c72}.
   */
  static get KrStageCommonMethods(): KrStageCommonMethodsViewInfo {
    return ViewInfo.krStageCommonMethods = ViewInfo.krStageCommonMethods ?? new KrStageCommonMethodsViewInfo();
  }

  private static krStageCommonMethods: KrStageCommonMethodsViewInfo;

  /**
   * View identifier for "$Views_Names_KrStageGroups": {6492b1f7-0fa4-4910-9911-67b2eb1614d7}.
   */
  static get KrStageGroups(): KrStageGroupsViewInfo {
    return ViewInfo.krStageGroups = ViewInfo.krStageGroups ?? new KrStageGroupsViewInfo();
  }

  private static krStageGroups: KrStageGroupsViewInfo;

  /**
   * View identifier for "$Views_Names_KrStageRows": {74f0ec7a-2cb4-4bb9-9eca-942e28374ea9}.
   */
  static get KrStageRows(): KrStageRowsViewInfo {
    return ViewInfo.krStageRows = ViewInfo.krStageRows ?? new KrStageRowsViewInfo();
  }

  private static krStageRows: KrStageRowsViewInfo;

  /**
   * View identifier for "$Views_Names_KrStageTemplateGroupPosition": {c4092348-06c2-452d-984e-18638961365b}.
   */
  static get KrStageTemplateGroupPosition(): KrStageTemplateGroupPositionViewInfo {
    return ViewInfo.krStageTemplateGroupPosition = ViewInfo.krStageTemplateGroupPosition ?? new KrStageTemplateGroupPositionViewInfo();
  }

  private static krStageTemplateGroupPosition: KrStageTemplateGroupPositionViewInfo;

  /**
   * View identifier for "$Views_Names_KrStageTemplates": {2163b711-2672-443f-9af1-ede4af0e9e89}.
   */
  static get KrStageTemplates(): KrStageTemplatesViewInfo {
    return ViewInfo.krStageTemplates = ViewInfo.krStageTemplates ?? new KrStageTemplatesViewInfo();
  }

  private static krStageTemplates: KrStageTemplatesViewInfo;

  /**
   * View identifier for "$Views_Names_KrStageTypes": {a7ea0334-626e-41d8-9ae3-80cf3c710daa}.
   */
  static get KrStageTypes(): KrStageTypesViewInfo {
    return ViewInfo.krStageTypes = ViewInfo.krStageTypes ?? new KrStageTypesViewInfo();
  }

  private static krStageTypes: KrStageTypesViewInfo;

  /**
   * View identifier for "$Views_Names_KrTypes": {399837f5-1c6e-470e-ae4a-6d85362650c7}.
   */
  static get KrTypes(): KrTypesViewInfo {
    return ViewInfo.krTypes = ViewInfo.krTypes ?? new KrTypesViewInfo();
  }

  private static krTypes: KrTypesViewInfo;

  /**
   * View identifier for "$Views_Names_KrTypesEffective": {fd177ebc-050e-4f1a-8a28-deba816a727d}.
   */
  static get KrTypesEffective(): KrTypesEffectiveViewInfo {
    return ViewInfo.krTypesEffective = ViewInfo.krTypesEffective ?? new KrTypesEffectiveViewInfo();
  }

  private static krTypesEffective: KrTypesEffectiveViewInfo;

  /**
   * View identifier for "$Views_Names_KrTypesForDialogs": {2c0b6a4a-8759-43d1-b23c-0c64f365d343}.
   */
  static get KrTypesForDialogs(): KrTypesForDialogsViewInfo {
    return ViewInfo.krTypesForDialogs = ViewInfo.krTypesForDialogs ?? new KrTypesForDialogsViewInfo();
  }

  private static krTypesForDialogs: KrTypesForDialogsViewInfo;

  /**
   * View identifier for "$Views_Names_KrTypesForPermissionsExtension": {d2c9ecb8-0e7f-4f79-a76c-c2cc71b0d959}.
   */
  static get KrTypesForPermissionsExtension(): KrTypesForPermissionsExtensionViewInfo {
    return ViewInfo.krTypesForPermissionsExtension = ViewInfo.krTypesForPermissionsExtension ?? new KrTypesForPermissionsExtensionViewInfo();
  }

  private static krTypesForPermissionsExtension: KrTypesForPermissionsExtensionViewInfo;

  /**
   * View identifier for "$Views_Names_KrVirtualFiles": {e2ca613f-9ad1-4dba-bdaa-feb0d96b9700}.
   */
  static get KrVirtualFiles(): KrVirtualFilesViewInfo {
    return ViewInfo.krVirtualFiles = ViewInfo.krVirtualFiles ?? new KrVirtualFilesViewInfo();
  }

  private static krVirtualFiles: KrVirtualFilesViewInfo;

  /**
   * View identifier for "$Views_Names_Languages": {7ed54a59-1c9e-469b-83eb-ed1c6ec70753}.
   */
  static get Languages(): LanguagesViewInfo {
    return ViewInfo.languages = ViewInfo.languages ?? new LanguagesViewInfo();
  }

  private static languages: LanguagesViewInfo;

  /**
   * View identifier for "$Views_Names_LastTopics": {ba6ff2de-b4d3-47cc-9f98-29baabdd6bce}.
   */
  static get LastTopics(): LastTopicsViewInfo {
    return ViewInfo.lastTopics = ViewInfo.lastTopics ?? new LastTopicsViewInfo();
  }

  private static lastTopics: LastTopicsViewInfo;

  /**
   * View identifier for "$Views_Names_LawCases": {fc9df545-e9f7-4562-a37d-f8da4a10d248}.
   */
  static get LawCases(): LawCasesViewInfo {
    return ViewInfo.lawCases = ViewInfo.lawCases ?? new LawCasesViewInfo();
  }

  private static lawCases: LawCasesViewInfo;

  /**
   * View identifier for "$Views_Names_LawCategories": {abe65a63-77cd-4f40-a6ce-d5c51ac1d022}.
   */
  static get LawCategories(): LawCategoriesViewInfo {
    return ViewInfo.lawCategories = ViewInfo.lawCategories ?? new LawCategoriesViewInfo();
  }

  private static lawCategories: LawCategoriesViewInfo;

  /**
   * View identifier for "$Views_Names_LawClassificationPlans": {838245d0-f5b9-4ed6-9343-ee74d383f689}.
   */
  static get LawClassificationPlans(): LawClassificationPlansViewInfo {
    return ViewInfo.lawClassificationPlans = ViewInfo.lawClassificationPlans ?? new LawClassificationPlansViewInfo();
  }

  private static lawClassificationPlans: LawClassificationPlansViewInfo;

  /**
   * View identifier for "$Views_Names_LawClients": {d87978ea-f6d4-4cec-9a74-3354a910c5f1}.
   */
  static get LawClients(): LawClientsViewInfo {
    return ViewInfo.lawClients = ViewInfo.lawClients ?? new LawClientsViewInfo();
  }

  private static lawClients: LawClientsViewInfo;

  /**
   * View identifier for "$Views_Names_LawDocKinds": {121cee33-1f41-491b-8ac0-1d3d199be43a}.
   */
  static get LawDocKinds(): LawDocKindsViewInfo {
    return ViewInfo.lawDocKinds = ViewInfo.lawDocKinds ?? new LawDocKindsViewInfo();
  }

  private static lawDocKinds: LawDocKindsViewInfo;

  /**
   * View identifier for "$Views_Names_LawDocTypes": {79b46d45-d6ac-4b46-8051-7cfabc879bfe}.
   */
  static get LawDocTypes(): LawDocTypesViewInfo {
    return ViewInfo.lawDocTypes = ViewInfo.lawDocTypes ?? new LawDocTypesViewInfo();
  }

  private static lawDocTypes: LawDocTypesViewInfo;

  /**
   * View identifier for "$Views_Names_LawEntityKinds": {05d82dad-cf51-4917-bb9e-6d76ede89d0b}.
   */
  static get LawEntityKinds(): LawEntityKindsViewInfo {
    return ViewInfo.lawEntityKinds = ViewInfo.lawEntityKinds ?? new LawEntityKindsViewInfo();
  }

  private static lawEntityKinds: LawEntityKindsViewInfo;

  /**
   * View identifier for "$Views_Names_LawFileStorages": {0160728f-e47c-426e-a569-09491c421091}.
   */
  static get LawFileStorages(): LawFileStoragesViewInfo {
    return ViewInfo.lawFileStorages = ViewInfo.lawFileStorages ?? new LawFileStoragesViewInfo();
  }

  private static lawFileStorages: LawFileStoragesViewInfo;

  /**
   * View identifier for "$Views_Names_LawFolders": {79dba5d9-833e-49ee-a6be-9530abe314f1}.
   */
  static get LawFolders(): LawFoldersViewInfo {
    return ViewInfo.lawFolders = ViewInfo.lawFolders ?? new LawFoldersViewInfo();
  }

  private static lawFolders: LawFoldersViewInfo;

  /**
   * View identifier for "$Views_Names_LawPartnerRepresentatives": {c8f9dad9-f2e0-40f8-9e6f-9fabb2e8fe83}.
   */
  static get LawPartnerRepresentatives(): LawPartnerRepresentativesViewInfo {
    return ViewInfo.lawPartnerRepresentatives = ViewInfo.lawPartnerRepresentatives ?? new LawPartnerRepresentativesViewInfo();
  }

  private static lawPartnerRepresentatives: LawPartnerRepresentativesViewInfo;

  /**
   * View identifier for "$Views_Names_LawPartners": {0f2cecd3-2051-4c26-8c5a-312dbecfb2fc}.
   */
  static get LawPartners(): LawPartnersViewInfo {
    return ViewInfo.lawPartners = ViewInfo.lawPartners ?? new LawPartnersViewInfo();
  }

  private static lawPartners: LawPartnersViewInfo;

  /**
   * View identifier for "$Views_Names_LawStoreLocations": {0604e158-4700-40e5-9a1a-b3e766262f6a}.
   */
  static get LawStoreLocations(): LawStoreLocationsViewInfo {
    return ViewInfo.lawStoreLocations = ViewInfo.lawStoreLocations ?? new LawStoreLocationsViewInfo();
  }

  private static lawStoreLocations: LawStoreLocationsViewInfo;

  /**
   * View identifier for "$Views_Names_LawUsers": {04b79978-1ad2-4ecd-9326-34d259d6ea34}.
   */
  static get LawUsers(): LawUsersViewInfo {
    return ViewInfo.lawUsers = ViewInfo.lawUsers ?? new LawUsersViewInfo();
  }

  private static lawUsers: LawUsersViewInfo;

  /**
   * View identifier for "$Views_Names_LicenseTypes": {613dd133-9ac2-4cae-851f-c417fe657ec4}.
   */
  static get LicenseTypes(): LicenseTypesViewInfo {
    return ViewInfo.licenseTypes = ViewInfo.licenseTypes ?? new LicenseTypesViewInfo();
  }

  private static licenseTypes: LicenseTypesViewInfo;

  /**
   * View identifier for "$Views_Names_LinkedDocuments": {88069520-441a-4f38-a103-c8f2ac8a2101}.
   */
  static get LinkedDocuments(): LinkedDocumentsViewInfo {
    return ViewInfo.linkedDocuments = ViewInfo.linkedDocuments ?? new LinkedDocumentsViewInfo();
  }

  private static linkedDocuments: LinkedDocumentsViewInfo;

  /**
   * View identifier for "$Views_Names_LoginTypes": {b0afaa90-23f1-4c2d-a85d-54506e027745}.
   */
  static get LoginTypes(): LoginTypesViewInfo {
    return ViewInfo.loginTypes = ViewInfo.loginTypes ?? new LoginTypesViewInfo();
  }

  private static loginTypes: LoginTypesViewInfo;

  /**
   * View identifier for "$Views_Names_MyAcquaintanceHistory": {ef66bde2-1126-4c09-9f4e-56d710ccda40}.
   */
  static get MyAcquaintanceHistory(): MyAcquaintanceHistoryViewInfo {
    return ViewInfo.myAcquaintanceHistory = ViewInfo.myAcquaintanceHistory ?? new MyAcquaintanceHistoryViewInfo();
  }

  private static myAcquaintanceHistory: MyAcquaintanceHistoryViewInfo;

  /**
   * View identifier for "$Views_Names_MyCompletedTasks": {89cf35b0-69bc-406a-9b95-77be879a94fa}.
   */
  static get MyCompletedTasks(): MyCompletedTasksViewInfo {
    return ViewInfo.myCompletedTasks = ViewInfo.myCompletedTasks ?? new MyCompletedTasksViewInfo();
  }

  private static myCompletedTasks: MyCompletedTasksViewInfo;

  /**
   * View identifier for "$Views_Names_MyDocuments": {198282c6-a340-472f-97fb-e8895c3cc3ca}.
   */
  static get MyDocuments(): MyDocumentsViewInfo {
    return ViewInfo.myDocuments = ViewInfo.myDocuments ?? new MyDocumentsViewInfo();
  }

  private static myDocuments: MyDocumentsViewInfo;

  /**
   * View identifier for "$Views_Names_MyTags": {b206c96c-de91-4bc5-aa55-d00bf7eb9604}.
   */
  static get MyTags(): MyTagsViewInfo {
    return ViewInfo.myTags = ViewInfo.myTags ?? new MyTagsViewInfo();
  }

  private static myTags: MyTagsViewInfo;

  /**
   * View identifier for "$Views_Names_MyTasks": {d249d321-c5a3-4847-951a-f47ffcf5509d}.
   */
  static get MyTasks(): MyTasksViewInfo {
    return ViewInfo.myTasks = ViewInfo.myTasks ?? new MyTasksViewInfo();
  }

  private static myTasks: MyTasksViewInfo;

  /**
   * View identifier for "$Views_Names_MyTopics": {01629718-ee20-45d9-820f-188b350bcf88}.
   */
  static get MyTopics(): MyTopicsViewInfo {
    return ViewInfo.myTopics = ViewInfo.myTopics ?? new MyTopicsViewInfo();
  }

  private static myTopics: MyTopicsViewInfo;

  /**
   * View identifier for "$Views_Names_Notifications": {ecc76994-461e-428d-abd0-c2499f6711fe}.
   */
  static get Notifications(): NotificationsViewInfo {
    return ViewInfo.notifications = ViewInfo.notifications ?? new NotificationsViewInfo();
  }

  private static notifications: NotificationsViewInfo;

  /**
   * View identifier for "$Views_Names_NotificationSubscriptions": {41fef937-d98e-48f8-9d47-eed0c1d32adf}.
   */
  static get NotificationSubscriptions(): NotificationSubscriptionsViewInfo {
    return ViewInfo.notificationSubscriptions = ViewInfo.notificationSubscriptions ?? new NotificationSubscriptionsViewInfo();
  }

  private static notificationSubscriptions: NotificationSubscriptionsViewInfo;

  /**
   * View identifier for "$Views_Names_NotificationTypes": {72cd38b6-102a-4368-97a1-08b216865c96}.
   */
  static get NotificationTypes(): NotificationTypesViewInfo {
    return ViewInfo.notificationTypes = ViewInfo.notificationTypes ?? new NotificationTypesViewInfo();
  }

  private static notificationTypes: NotificationTypesViewInfo;

  /**
   * View identifier for "$Views_Names_OcrLanguages": {a7496820-b1b7-4443-b889-990996deeff1}.
   */
  static get OcrLanguages(): OcrLanguagesViewInfo {
    return ViewInfo.ocrLanguages = ViewInfo.ocrLanguages ?? new OcrLanguagesViewInfo();
  }

  private static ocrLanguages: OcrLanguagesViewInfo;

  /**
   * View identifier for "$Views_Names_OcrOperations": {c6047012-cb5c-4fd3-a3d1-3b2b39be7a1f}.
   */
  static get OcrOperations(): OcrOperationsViewInfo {
    return ViewInfo.ocrOperations = ViewInfo.ocrOperations ?? new OcrOperationsViewInfo();
  }

  private static ocrOperations: OcrOperationsViewInfo;

  /**
   * View identifier for "$Views_Names_OcrPatternTypes": {a1f31945-fbbb-4ae2-aa5a-db3354d75693}.
   */
  static get OcrPatternTypes(): OcrPatternTypesViewInfo {
    return ViewInfo.ocrPatternTypes = ViewInfo.ocrPatternTypes ?? new OcrPatternTypesViewInfo();
  }

  private static ocrPatternTypes: OcrPatternTypesViewInfo;

  /**
   * View identifier for "$Views_Names_OcrRecognitionModes": {ab45451f-974d-4231-a182-16c6d04bdfba}.
   */
  static get OcrRecognitionModes(): OcrRecognitionModesViewInfo {
    return ViewInfo.ocrRecognitionModes = ViewInfo.ocrRecognitionModes ?? new OcrRecognitionModesViewInfo();
  }

  private static ocrRecognitionModes: OcrRecognitionModesViewInfo;

  /**
   * View identifier for "$Views_Names_OcrRequests": {9eed84c8-53f0-4d9a-8619-bdf97c0fc615}.
   */
  static get OcrRequests(): OcrRequestsViewInfo {
    return ViewInfo.ocrRequests = ViewInfo.ocrRequests ?? new OcrRequestsViewInfo();
  }

  private static ocrRequests: OcrRequestsViewInfo;

  /**
   * View identifier for "$Views_Names_OcrSegmentationModes": {740088cd-bd83-4d81-89bb-7ff9e12da0d0}.
   */
  static get OcrSegmentationModes(): OcrSegmentationModesViewInfo {
    return ViewInfo.ocrSegmentationModes = ViewInfo.ocrSegmentationModes ?? new OcrSegmentationModesViewInfo();
  }

  private static ocrSegmentationModes: OcrSegmentationModesViewInfo;

  /**
   * View identifier for "$Views_Names_Operations": {c0bc56cb-0868-4108-996f-a87fe290d90d}.
   */
  static get Operations(): OperationsViewInfo {
    return ViewInfo.operations = ViewInfo.operations ?? new OperationsViewInfo();
  }

  private static operations: OperationsViewInfo;

  /**
   * View identifier for "$Views_Names_OutgoingDocuments": {a4d2d4e1-a59c-4265-a2ee-f58ca0cbc3fc}.
   */
  static get OutgoingDocuments(): OutgoingDocumentsViewInfo {
    return ViewInfo.outgoingDocuments = ViewInfo.outgoingDocuments ?? new OutgoingDocumentsViewInfo();
  }

  private static outgoingDocuments: OutgoingDocumentsViewInfo;

  /**
   * View identifier for "$Views_Names_Partitions": {9500e883-9c8e-427e-930b-e93adfd0f56a}.
   */
  static get Partitions(): PartitionsViewInfo {
    return ViewInfo.partitions = ViewInfo.partitions ?? new PartitionsViewInfo();
  }

  private static partitions: PartitionsViewInfo;

  /**
   * View identifier for "$Views_Names_Partners": {f9e6f291-2de8-459c-8d05-420fb8cce90f}.
   */
  static get Partners(): PartnersViewInfo {
    return ViewInfo.partners = ViewInfo.partners ?? new PartnersViewInfo();
  }

  private static partners: PartnersViewInfo;

  /**
   * View identifier for "$Views_Names_PartnersContacts": {d0971b0f-42a0-433c-b3f4-a1d1b279156c}.
   */
  static get PartnersContacts(): PartnersContactsViewInfo {
    return ViewInfo.partnersContacts = ViewInfo.partnersContacts ?? new PartnersContactsViewInfo();
  }

  private static partnersContacts: PartnersContactsViewInfo;

  /**
   * View identifier for "$Views_Names_PartnersTypes": {59d56dd0-700f-46de-afd5-9aa3a1f9f69e}.
   */
  static get PartnersTypes(): PartnersTypesViewInfo {
    return ViewInfo.partnersTypes = ViewInfo.partnersTypes ?? new PartnersTypesViewInfo();
  }

  private static partnersTypes: PartnersTypesViewInfo;

  /**
   * View identifier for "$Views_Names_ProtocolCompletedTasks": {65dde283-7e4c-4898-b91f-2921e8fc8ac4}.
   */
  static get ProtocolCompletedTasks(): ProtocolCompletedTasksViewInfo {
    return ViewInfo.protocolCompletedTasks = ViewInfo.protocolCompletedTasks ?? new ProtocolCompletedTasksViewInfo();
  }

  private static protocolCompletedTasks: ProtocolCompletedTasksViewInfo;

  /**
   * View identifier for "$Views_Names_ProtocolReportsWithPhoto": {a5bb501c-3169-4d72-8098-588f932815b1}.
   */
  static get ProtocolReportsWithPhoto(): ProtocolReportsWithPhotoViewInfo {
    return ViewInfo.protocolReportsWithPhoto = ViewInfo.protocolReportsWithPhoto ?? new ProtocolReportsWithPhotoViewInfo();
  }

  private static protocolReportsWithPhoto: ProtocolReportsWithPhotoViewInfo;

  /**
   * View identifier for "$Views_Names_Protocols": {775558fa-2ec3-4c38-819e-9395e94c28c7}.
   */
  static get Protocols(): ProtocolsViewInfo {
    return ViewInfo.protocols = ViewInfo.protocols ?? new ProtocolsViewInfo();
  }

  private static protocols: ProtocolsViewInfo;

  /**
   * View identifier for "$Views_Names_RefDocumentsLookup": {57fb8582-bfe3-4ae9-8ee3-1feb96b18803}.
   */
  static get RefDocumentsLookup(): RefDocumentsLookupViewInfo {
    return ViewInfo.refDocumentsLookup = ViewInfo.refDocumentsLookup ?? new RefDocumentsLookupViewInfo();
  }

  private static refDocumentsLookup: RefDocumentsLookupViewInfo;

  /**
   * View identifier for "$Views_Names_ReportCurrentTasksByDepartment": {48224a01-8731-4ac1-a4f0-749ee5f375d2}.
   */
  static get ReportCurrentTasksByDepartment(): ReportCurrentTasksByDepartmentViewInfo {
    return ViewInfo.reportCurrentTasksByDepartment = ViewInfo.reportCurrentTasksByDepartment ?? new ReportCurrentTasksByDepartmentViewInfo();
  }

  private static reportCurrentTasksByDepartment: ReportCurrentTasksByDepartmentViewInfo;

  /**
   * View identifier for "$Views_Names_ReportCurrentTasksByDepUnpivoted": {d36bf57a-20f1-4d77-8884-0d1465333380}.
   */
  static get ReportCurrentTasksByDepUnpivoted(): ReportCurrentTasksByDepUnpivotedViewInfo {
    return ViewInfo.reportCurrentTasksByDepUnpivoted = ViewInfo.reportCurrentTasksByDepUnpivoted ?? new ReportCurrentTasksByDepUnpivotedViewInfo();
  }

  private static reportCurrentTasksByDepUnpivoted: ReportCurrentTasksByDepUnpivotedViewInfo;

  /**
   * View identifier for "$Views_Names_ReportCurrentTasksByUser": {871ad32d-0846-448c-beb2-85c1d357177b}.
   */
  static get ReportCurrentTasksByUser(): ReportCurrentTasksByUserViewInfo {
    return ViewInfo.reportCurrentTasksByUser = ViewInfo.reportCurrentTasksByUser ?? new ReportCurrentTasksByUserViewInfo();
  }

  private static reportCurrentTasksByUser: ReportCurrentTasksByUserViewInfo;

  /**
   * View identifier for "$Views_Names_ReportCurrentTasksRules": {146b25b3-e61e-4259-bcc5-959040755de6}.
   */
  static get ReportCurrentTasksRules(): ReportCurrentTasksRulesViewInfo {
    return ViewInfo.reportCurrentTasksRules = ViewInfo.reportCurrentTasksRules ?? new ReportCurrentTasksRulesViewInfo();
  }

  private static reportCurrentTasksRules: ReportCurrentTasksRulesViewInfo;

  /**
   * View identifier for "$Views_Names_ReportDocumentsByType": {35ebb779-0abd-411b-bc6b-358d2e2cabca}.
   */
  static get ReportDocumentsByType(): ReportDocumentsByTypeViewInfo {
    return ViewInfo.reportDocumentsByType = ViewInfo.reportDocumentsByType ?? new ReportDocumentsByTypeViewInfo();
  }

  private static reportDocumentsByType: ReportDocumentsByTypeViewInfo;

  /**
   * View identifier for "$Views_Names_ReportPastTasksByDepartment": {49562bb2-b059-4f06-9771-c5e38892ff6f}.
   */
  static get ReportPastTasksByDepartment(): ReportPastTasksByDepartmentViewInfo {
    return ViewInfo.reportPastTasksByDepartment = ViewInfo.reportPastTasksByDepartment ?? new ReportPastTasksByDepartmentViewInfo();
  }

  private static reportPastTasksByDepartment: ReportPastTasksByDepartmentViewInfo;

  /**
   * View identifier for "$Views_Names_ReportPastTasksByUser": {44b3c3ba-6dc4-4b3b-bd1e-48f939c3a8a1}.
   */
  static get ReportPastTasksByUser(): ReportPastTasksByUserViewInfo {
    return ViewInfo.reportPastTasksByUser = ViewInfo.reportPastTasksByUser ?? new ReportPastTasksByUserViewInfo();
  }

  private static reportPastTasksByUser: ReportPastTasksByUserViewInfo;

  /**
   * View identifier for "$Views_Names_RoleDeputies": {181eafda-939b-4b38-9f71-21c699133396}.
   */
  static get RoleDeputies(): RoleDeputiesViewInfo {
    return ViewInfo.roleDeputies = ViewInfo.roleDeputies ?? new RoleDeputiesViewInfo();
  }

  private static roleDeputies: RoleDeputiesViewInfo;

  /**
   * View identifier for "$Views_Names_RoleDeputiesManagementDeputized": {2877fb32-b5fa-40ed-94d2-32dcd675afc1}.
   */
  static get RoleDeputiesManagementDeputized(): RoleDeputiesManagementDeputizedViewInfo {
    return ViewInfo.roleDeputiesManagementDeputized = ViewInfo.roleDeputiesManagementDeputized ?? new RoleDeputiesManagementDeputizedViewInfo();
  }

  private static roleDeputiesManagementDeputized: RoleDeputiesManagementDeputizedViewInfo;

  /**
   * View identifier for "$Views_Names_RoleDeputiesNew": {87c8a806-31fc-4d45-b6a0-cb1028d9fc24}.
   */
  static get RoleDeputiesNew(): RoleDeputiesNewViewInfo {
    return ViewInfo.roleDeputiesNew = ViewInfo.roleDeputiesNew ?? new RoleDeputiesNewViewInfo();
  }

  private static roleDeputiesNew: RoleDeputiesNewViewInfo;

  /**
   * View identifier for "$Views_Names_RoleGenerators": {e8163ca6-19e3-09ce-bb88-efcac757a8f4}.
   */
  static get RoleGenerators(): RoleGeneratorsViewInfo {
    return ViewInfo.roleGenerators = ViewInfo.roleGenerators ?? new RoleGeneratorsViewInfo();
  }

  private static roleGenerators: RoleGeneratorsViewInfo;

  /**
   * View identifier for "$Views_Names_Roles": {e168749e-d123-4833-a820-77c9dae80f05}.
   */
  static get Roles(): RolesViewInfo {
    return ViewInfo.roles = ViewInfo.roles ?? new RolesViewInfo();
  }

  private static roles: RolesViewInfo;

  /**
   * View identifier for "$Views_Names_RoleTypes": {df92983b-2dd3-4092-8603-48d0245cd049}.
   */
  static get RoleTypes(): RoleTypesViewInfo {
    return ViewInfo.roleTypes = ViewInfo.roleTypes ?? new RoleTypesViewInfo();
  }

  private static roleTypes: RoleTypesViewInfo;

  /**
   * View identifier for "$Views_Names_Sequences": {b712a7be-2796-4986-b226-8bbcb26d1648}.
   */
  static get Sequences(): SequencesViewInfo {
    return ViewInfo.sequences = ViewInfo.sequences ?? new SequencesViewInfo();
  }

  private static sequences: SequencesViewInfo;

  /**
   * View identifier for "$Views_Names_Sessions": {567e7c41-536e-47af-8c2d-b6d8cb6b64fb}.
   */
  static get Sessions(): SessionsViewInfo {
    return ViewInfo.sessions = ViewInfo.sessions ?? new SessionsViewInfo();
  }

  private static sessions: SessionsViewInfo;

  /**
   * View identifier for "$Views_Names_SessionServiceTypes": {24f1eafb-5b69-4329-bf8b-10de805770df}.
   */
  static get SessionServiceTypes(): SessionServiceTypesViewInfo {
    return ViewInfo.sessionServiceTypes = ViewInfo.sessionServiceTypes ?? new SessionServiceTypesViewInfo();
  }

  private static sessionServiceTypes: SessionServiceTypesViewInfo;

  /**
   * View identifier for "$Views_Names_SignatureDigestAlgos": {c6f08c11-6dae-4e7f-a1d7-260134e9b3ff}.
   */
  static get SignatureDigestAlgos(): SignatureDigestAlgosViewInfo {
    return ViewInfo.signatureDigestAlgos = ViewInfo.signatureDigestAlgos ?? new SignatureDigestAlgosViewInfo();
  }

  private static signatureDigestAlgos: SignatureDigestAlgosViewInfo;

  /**
   * View identifier for "$Views_Names_SignatureEncryptionAlgos": {4cb7fef2-7b0d-4300-a9ac-7b2b082bcb75}.
   */
  static get SignatureEncryptionAlgos(): SignatureEncryptionAlgosViewInfo {
    return ViewInfo.signatureEncryptionAlgos = ViewInfo.signatureEncryptionAlgos ?? new SignatureEncryptionAlgosViewInfo();
  }

  private static signatureEncryptionAlgos: SignatureEncryptionAlgosViewInfo;

  /**
   * View identifier for "$Views_Names_SignaturePackagings": {b49ba9e8-4b5c-40af-8fa2-08daf310aca6}.
   */
  static get SignaturePackagings(): SignaturePackagingsViewInfo {
    return ViewInfo.signaturePackagings = ViewInfo.signaturePackagings ?? new SignaturePackagingsViewInfo();
  }

  private static signaturePackagings: SignaturePackagingsViewInfo;

  /**
   * View identifier for "$Views_Names_SignatureProfiles": {8cffc16e-6086-488b-99ce-bba53520e37c}.
   */
  static get SignatureProfiles(): SignatureProfilesViewInfo {
    return ViewInfo.signatureProfiles = ViewInfo.signatureProfiles ?? new SignatureProfilesViewInfo();
  }

  private static signatureProfiles: SignatureProfilesViewInfo;

  /**
   * View identifier for "$Views_Names_SignatureTypes": {30a4931f-c0cf-41fd-b962-053488596ca1}.
   */
  static get SignatureTypes(): SignatureTypesViewInfo {
    return ViewInfo.signatureTypes = ViewInfo.signatureTypes ?? new SignatureTypesViewInfo();
  }

  private static signatureTypes: SignatureTypesViewInfo;

  /**
   * View identifier for "$Views_Names_SmartRoleGenerators": {26ce38e4-cf2d-48f0-a790-c4bd631e3eea}.
   */
  static get SmartRoleGenerators(): SmartRoleGeneratorsViewInfo {
    return ViewInfo.smartRoleGenerators = ViewInfo.smartRoleGenerators ?? new SmartRoleGeneratorsViewInfo();
  }

  private static smartRoleGenerators: SmartRoleGeneratorsViewInfo;

  /**
   * View identifier for "$Views_Names_TagCards": {c2b5ea9e-2bfa-43f6-a566-7e1df45f0f51}.
   */
  static get TagCards(): TagCardsViewInfo {
    return ViewInfo.tagCards = ViewInfo.tagCards ?? new TagCardsViewInfo();
  }

  private static tagCards: TagCardsViewInfo;

  /**
   * View identifier for "$Views_Names_Tags": {99751083-96ea-435b-8331-4e0de99d69d6}.
   */
  static get Tags(): TagsViewInfo {
    return ViewInfo.tags = ViewInfo.tags ?? new TagsViewInfo();
  }

  private static tags: TagsViewInfo;

  /**
   * View identifier for "$Views_Names_TaskAssignedRoles": {b956e262-9b54-4aaf-a7b8-1649ea9462e7}.
   */
  static get TaskAssignedRoles(): TaskAssignedRolesViewInfo {
    return ViewInfo.taskAssignedRoles = ViewInfo.taskAssignedRoles ?? new TaskAssignedRolesViewInfo();
  }

  private static taskAssignedRoles: TaskAssignedRolesViewInfo;

  /**
   * View identifier for "TaskAssignedRoleUsers": {966b7c87-1443-4ab9-b23a-6864234ed30e}.
   */
  static get TaskAssignedRoleUsers(): TaskAssignedRoleUsersViewInfo {
    return ViewInfo.taskAssignedRoleUsers = ViewInfo.taskAssignedRoleUsers ?? new TaskAssignedRoleUsersViewInfo();
  }

  private static taskAssignedRoleUsers: TaskAssignedRoleUsersViewInfo;

  /**
   * View identifier for "$Views_Names_TaskFunctionRoles": {20a41d67-4807-496f-b5f8-1ae3f036eb2f}.
   */
  static get TaskFunctionRoles(): TaskFunctionRolesViewInfo {
    return ViewInfo.taskFunctionRoles = ViewInfo.taskFunctionRoles ?? new TaskFunctionRolesViewInfo();
  }

  private static taskFunctionRoles: TaskFunctionRolesViewInfo;

  /**
   * View identifier for "$Views_Names_TaskHistory": {00da4f8f-bbf5-4b18-8b43-c528e5359d28}.
   */
  static get TaskHistory(): TaskHistoryViewInfo {
    return ViewInfo.taskHistory = ViewInfo.taskHistory ?? new TaskHistoryViewInfo();
  }

  private static taskHistory: TaskHistoryViewInfo;

  /**
   * View identifier for "$Views_Names_TaskHistoryGroupTypes": {25d1c651-1008-496c-8252-778a4b5d9064}.
   */
  static get TaskHistoryGroupTypes(): TaskHistoryGroupTypesViewInfo {
    return ViewInfo.taskHistoryGroupTypes = ViewInfo.taskHistoryGroupTypes ?? new TaskHistoryGroupTypesViewInfo();
  }

  private static taskHistoryGroupTypes: TaskHistoryGroupTypesViewInfo;

  /**
   * View identifier for "$Views_Names_TaskKinds": {57dc8c0a-080f-486d-ba97-ffc52869754e}.
   */
  static get TaskKinds(): TaskKindsViewInfo {
    return ViewInfo.taskKinds = ViewInfo.taskKinds ?? new TaskKindsViewInfo();
  }

  private static taskKinds: TaskKindsViewInfo;

  /**
   * View identifier for "$Views_Names_TaskStates": {b75a29da-0672-45ff-8f58-39abcb129506}.
   */
  static get TaskStates(): TaskStatesViewInfo {
    return ViewInfo.taskStates = ViewInfo.taskStates ?? new TaskStatesViewInfo();
  }

  private static taskStates: TaskStatesViewInfo;

  /**
   * View identifier for "$Views_Names_TaskTypes": {fcd3f5ad-545f-41d1-ad85-345157020e33}.
   */
  static get TaskTypes(): TaskTypesViewInfo {
    return ViewInfo.taskTypes = ViewInfo.taskTypes ?? new TaskTypesViewInfo();
  }

  private static taskTypes: TaskTypesViewInfo;

  /**
   * View identifier for "$Views_Names_Templates": {edab4e60-c19e-49a2-979f-7634133c377e}.
   */
  static get Templates(): TemplatesViewInfo {
    return ViewInfo.templates = ViewInfo.templates ?? new TemplatesViewInfo();
  }

  private static templates: TemplatesViewInfo;

  /**
   * View identifier for "$Views_Names_TileSizes": {942b9908-f1c0-442d-b8b1-ba269431742d}.
   */
  static get TileSizes(): TileSizesViewInfo {
    return ViewInfo.tileSizes = ViewInfo.tileSizes ?? new TileSizesViewInfo();
  }

  private static tileSizes: TileSizesViewInfo;

  /**
   * View identifier for "$Views_Names_TimeZones": {24ed3370-f3c0-4074-a3f2-4614a7baaebb}.
   */
  static get TimeZones(): TimeZonesViewInfo {
    return ViewInfo.timeZones = ViewInfo.timeZones ?? new TimeZonesViewInfo();
  }

  private static timeZones: TimeZonesViewInfo;

  /**
   * View identifier for "$Views_Names_TopicParticipants": {982d76b5-8997-4265-a8a3-c5e427746834}.
   */
  static get TopicParticipants(): TopicParticipantsViewInfo {
    return ViewInfo.topicParticipants = ViewInfo.topicParticipants ?? new TopicParticipantsViewInfo();
  }

  private static topicParticipants: TopicParticipantsViewInfo;

  /**
   * View identifier for "$Views_Names_Types": {77b991d4-3f6d-4827-ae02-354e514f6c60}.
   */
  static get Types(): TypesViewInfo {
    return ViewInfo.types = ViewInfo.types ?? new TypesViewInfo();
  }

  private static types: TypesViewInfo;

  /**
   * View identifier for "$Views_Names_Users": {8b68754e-19c8-0984-aac8-51d8908acecf}.
   */
  static get Users(): UsersViewInfo {
    return ViewInfo.users = ViewInfo.users ?? new UsersViewInfo();
  }

  private static users: UsersViewInfo;

  /**
   * View identifier for "$Views_Names_VatTypes": {c14070dd-219a-4340-91f3-7c2ffd891382}.
   */
  static get VatTypes(): VatTypesViewInfo {
    return ViewInfo.vatTypes = ViewInfo.vatTypes ?? new VatTypesViewInfo();
  }

  private static vatTypes: VatTypesViewInfo;

  /**
   * View identifier for "$Views_Names_ViewFiles": {af26a537-a1c9-4e09-9048-b892cc0c687e}.
   */
  static get ViewFiles(): ViewFilesViewInfo {
    return ViewInfo.viewFiles = ViewInfo.viewFiles ?? new ViewFilesViewInfo();
  }

  private static viewFiles: ViewFilesViewInfo;

  /**
   * View identifier for "$Views_Names_Views": {54614ef7-6a51-46c4-aa03-93814eb79126}.
   */
  static get Views(): ViewsViewInfo {
    return ViewInfo.views = ViewInfo.views ?? new ViewsViewInfo();
  }

  private static views: ViewsViewInfo;

  /**
   * View identifier for "$Views_Names_WebApplications": {7849787c-5422-4a08-9652-dd77d8557f4a}.
   */
  static get WebApplications(): WebApplicationsViewInfo {
    return ViewInfo.webApplications = ViewInfo.webApplications ?? new WebApplicationsViewInfo();
  }

  private static webApplications: WebApplicationsViewInfo;

  /**
   * View identifier for "$Views_Names_WeTaskControlTypes": {81a77ff3-5f38-42a4-bdaf-61ec8c508c39}.
   */
  static get WeTaskControlTypes(): WeTaskControlTypesViewInfo {
    return ViewInfo.weTaskControlTypes = ViewInfo.weTaskControlTypes ?? new WeTaskControlTypesViewInfo();
  }

  private static weTaskControlTypes: WeTaskControlTypesViewInfo;

  /**
   * View identifier for "$Views_Names_WeTaskGroupActionOptionTypes": {6a66914d-790f-480a-9976-cb85cc67e028}.
   */
  static get WeTaskGroupActionOptionTypes(): WeTaskGroupActionOptionTypesViewInfo {
    return ViewInfo.weTaskGroupActionOptionTypes = ViewInfo.weTaskGroupActionOptionTypes ?? new WeTaskGroupActionOptionTypesViewInfo();
  }

  private static weTaskGroupActionOptionTypes: WeTaskGroupActionOptionTypesViewInfo;

  /**
   * View identifier for "$Views_Names_WfResolutionAuthors": {f3cc3c6c-67c6-477e-a2ab-99fb0bd6e95f}.
   */
  static get WfResolutionAuthors(): WfResolutionAuthorsViewInfo {
    return ViewInfo.wfResolutionAuthors = ViewInfo.wfResolutionAuthors ?? new WfResolutionAuthorsViewInfo();
  }

  private static wfResolutionAuthors: WfResolutionAuthorsViewInfo;

  /**
   * View identifier for "$Views_Names_WorkflowEngineCompiledBaseTypes": {d3ba4765-6a54-465e-8803-323050fd951e}.
   */
  static get WorkflowEngineCompiledBaseTypes(): WorkflowEngineCompiledBaseTypesViewInfo {
    return ViewInfo.workflowEngineCompiledBaseTypes = ViewInfo.workflowEngineCompiledBaseTypes ?? new WorkflowEngineCompiledBaseTypesViewInfo();
  }

  private static workflowEngineCompiledBaseTypes: WorkflowEngineCompiledBaseTypesViewInfo;

  /**
   * View identifier for "$Views_Names_WorkflowEngineErrors": {6138ccf4-b5a2-4789-a3c2-82382ae666a2}.
   */
  static get WorkflowEngineErrors(): WorkflowEngineErrorsViewInfo {
    return ViewInfo.workflowEngineErrors = ViewInfo.workflowEngineErrors ?? new WorkflowEngineErrorsViewInfo();
  }

  private static workflowEngineErrors: WorkflowEngineErrorsViewInfo;

  /**
   * View identifier for "$Views_Names_WorkflowEngineLogLevels": {e91f6c3a-c8a0-46d3-bc07-5277f0e7d3f7}.
   */
  static get WorkflowEngineLogLevels(): WorkflowEngineLogLevelsViewInfo {
    return ViewInfo.workflowEngineLogLevels = ViewInfo.workflowEngineLogLevels ?? new WorkflowEngineLogLevelsViewInfo();
  }

  private static workflowEngineLogLevels: WorkflowEngineLogLevelsViewInfo;

  /**
   * View identifier for "$Views_Names_WorkflowEngineLogs": {db1faa0a-fdfd-4a97-80e4-c1573d47b6c3}.
   */
  static get WorkflowEngineLogs(): WorkflowEngineLogsViewInfo {
    return ViewInfo.workflowEngineLogs = ViewInfo.workflowEngineLogs ?? new WorkflowEngineLogsViewInfo();
  }

  private static workflowEngineLogs: WorkflowEngineLogsViewInfo;

  /**
   * View identifier for "$Views_Names_WorkflowEngineTaskActions": {c39c3de3-6448-4c35-8978-4b385ca6a647}.
   */
  static get WorkflowEngineTaskActions(): WorkflowEngineTaskActionsViewInfo {
    return ViewInfo.workflowEngineTaskActions = ViewInfo.workflowEngineTaskActions ?? new WorkflowEngineTaskActionsViewInfo();
  }

  private static workflowEngineTaskActions: WorkflowEngineTaskActionsViewInfo;

  /**
   * View identifier for "$Views_Names_WorkflowEngineTileManagerExtensions": {e102a6c1-807c-4976-b2e7-180404090e75}.
   */
  static get WorkflowEngineTileManagerExtensions(): WorkflowEngineTileManagerExtensionsViewInfo {
    return ViewInfo.workflowEngineTileManagerExtensions = ViewInfo.workflowEngineTileManagerExtensions ?? new WorkflowEngineTileManagerExtensionsViewInfo();
  }

  private static workflowEngineTileManagerExtensions: WorkflowEngineTileManagerExtensionsViewInfo;

  /**
   * View identifier for "$Views_Names_WorkflowLinkModes": {cc6b5f26-00f7-4e57-9260-49ed427fd243}.
   */
  static get WorkflowLinkModes(): WorkflowLinkModesViewInfo {
    return ViewInfo.workflowLinkModes = ViewInfo.workflowLinkModes ?? new WorkflowLinkModesViewInfo();
  }

  private static workflowLinkModes: WorkflowLinkModesViewInfo;

  /**
   * View identifier for "$Views_Names_WorkflowNodeInstanceSubprocesses": {a08fa0ff-ec43-4848-9130-b7b5728fc686}.
   */
  static get WorkflowNodeInstanceSubprocesses(): WorkflowNodeInstanceSubprocessesViewInfo {
    return ViewInfo.workflowNodeInstanceSubprocesses = ViewInfo.workflowNodeInstanceSubprocesses ?? new WorkflowNodeInstanceSubprocessesViewInfo();
  }

  private static workflowNodeInstanceSubprocesses: WorkflowNodeInstanceSubprocessesViewInfo;

  /**
   * View identifier for "$Views_Names_WorkflowNodeInstanceTasks": {31853f54-cb3d-4004-a79c-a020cc0014c3}.
   */
  static get WorkflowNodeInstanceTasks(): WorkflowNodeInstanceTasksViewInfo {
    return ViewInfo.workflowNodeInstanceTasks = ViewInfo.workflowNodeInstanceTasks ?? new WorkflowNodeInstanceTasksViewInfo();
  }

  private static workflowNodeInstanceTasks: WorkflowNodeInstanceTasksViewInfo;

  /**
   * View identifier for "$Views_Names_WorkflowSignalProcessingModes": {718a1f3a-0a06-490d-8a55-654114c93d54}.
   */
  static get WorkflowSignalProcessingModes(): WorkflowSignalProcessingModesViewInfo {
    return ViewInfo.workflowSignalProcessingModes = ViewInfo.workflowSignalProcessingModes ?? new WorkflowSignalProcessingModesViewInfo();
  }

  private static workflowSignalProcessingModes: WorkflowSignalProcessingModesViewInfo;

  /**
   * View identifier for "$Views_Names_WorkflowSignalTypes": {3bc28139-54ad-45fe-9aa7-83119dc47b62}.
   */
  static get WorkflowSignalTypes(): WorkflowSignalTypesViewInfo {
    return ViewInfo.workflowSignalTypes = ViewInfo.workflowSignalTypes ?? new WorkflowSignalTypesViewInfo();
  }

  private static workflowSignalTypes: WorkflowSignalTypesViewInfo;

  /**
   * View identifier for "$Views_Names_Workplaces": {36b9cf55-a385-4b3d-84d8-7d251702cc88}.
   */
  static get Workplaces(): WorkplacesViewInfo {
    return ViewInfo.workplaces = ViewInfo.workplaces ?? new WorkplacesViewInfo();
  }

  private static workplaces: WorkplacesViewInfo;

  //#endregion

  //#region RefSections

  static get RefSections(): RefSectionsInfo {
    return ViewInfo.refSections = ViewInfo.refSections ?? new RefSectionsInfo();
  }

  private static refSections: RefSectionsInfo;

  //#endregion
}