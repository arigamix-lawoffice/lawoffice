using System;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Идентификаторы и имена типов карточек, задействованных в типовом решении.
    /// </summary>
    public static class DefaultCardTypes
    {
        #region Dictionaries Fields and Names

        /// <summary>
        /// Card type identifier for "Currency": {4BCE97D2-6B76-468D-9B09-711BA189CB1E}.
        /// </summary>
        public static readonly Guid CurrencyTypeID = new Guid(0x4bce97d2, 0x6b76, 0x468d, 0x9b, 0x09, 0x71, 0x1b, 0xa1, 0x89, 0xcb, 0x1e);

        /// <summary>
        /// Card type name for "Currency".
        /// </summary>
        public const string CurrencyTypeName = "Currency";

        /// <summary>
        /// Card type identifier for "FileCategory": {97182AFC-43CE-4BD9-9C96-73A4D2C5D5EB}.
        /// </summary>
        public static readonly Guid FileCategoryTypeID = new Guid(0x97182afc, 0x43ce, 0x4bd9, 0x9c, 0x96, 0x73, 0xa4, 0xd2, 0xc5, 0xd5, 0xeb);

        /// <summary>
        /// Card type name for "FileCategory".
        /// </summary>
        public const string FileCategoryTypeName = "FileCategory";

        /// <summary>
        /// Card type identifier for "FileTemplate": {B7E1B93E-EEDA-49B7-9402-2471D4D14BDF}.
        /// </summary>
        public static readonly Guid FileTemplateTypeID = new Guid(0xb7e1b93e, 0xeeda, 0x49b7, 0x94, 0x02, 0x24, 0x71, 0xd4, 0xd1, 0x4b, 0xdf);

        /// <summary>
        /// Card type name for "FileTemplate".
        /// </summary>
        public const string FileTemplateTypeName = "FileTemplate";

        /// <summary>
        /// Card type identifier for "KrDocType": {B17F4F35-17E1-4509-994B-EBD576F2C95E}.
        /// </summary>
        public static readonly Guid KrDocTypeTypeID = new Guid(0xb17f4f35, 0x17e1, 0x4509, 0x99, 0x4b, 0xeb, 0xd5, 0x76, 0xf2, 0xc9, 0x5e);

        /// <summary>
        /// Card type name for "KrDocType".
        /// </summary>
        public const string KrDocTypeTypeName = "KrDocType";

        /// <summary>
        /// Card type identifier for "Partner": {B9A1F125-AB1D-4CFF-929F-5AD8351BDA4F}.
        /// </summary>
        public static readonly Guid PartnerTypeID = new Guid(0xb9a1f125, 0xab1d, 0x4cff, 0x92, 0x9f, 0x5a, 0xd8, 0x35, 0x1b, 0xda, 0x4f);

        /// <summary>
        /// Card type name for "Partner".
        /// </summary>
        public const string PartnerTypeName = "Partner";

        /// <summary>
        /// Card type identifier for "ProtocolType": {5DA46E89-F932-4A48-B5A3-EC6285BFE3EA}.
        /// </summary>
        public static readonly Guid ProtocolTypeTypeID = new Guid(0x5da46e89, 0xf932, 0x4a48, 0xb5, 0xa3, 0xec, 0x62, 0x85, 0xbf, 0xe3, 0xea);

        /// <summary>
        /// Card type name for "ProtocolType".
        /// </summary>
        public const string ProtocolTypeTypeName = "ProtocolType";

        /// <summary>
        /// Card type identifier for "TaskKind": {2F41698A-3823-48C9-9476-F756F090EB11}.
        /// </summary>
        public static readonly Guid TaskKindTypeID = new Guid(0x2f41698a, 0x3823, 0x48c9, 0x94, 0x76, 0xf7, 0x56, 0xf0, 0x90, 0xeb, 0x11);

        /// <summary>
        /// Card type name for "TaskKind".
        /// </summary>
        public const string TaskKindTypeName = "TaskKind";

        /// <summary>
        /// Card type identifier for "KrDocState": {E83A230A-F5FC-445E-9B44-7D0140EE69F6}.
        /// </summary>
        public static readonly Guid KrDocStateTypeID = new Guid(0xe83a230a, 0xf5fc, 0x445e, 0x9b, 0x44, 0x7d, 0x01, 0x40, 0xee, 0x69, 0xf6);

        /// <summary>
        /// Card type name for "KrDocState".
        /// </summary>
        public const string KrDocStateTypeName = "KrDocState";

        #endregion

        #region Documents Fields and Names

        /// <summary>
        /// Card type identifier for "Contract": {335F86A1-D009-012C-8B45-1F43C2382C2D}.
        /// </summary>
        public static readonly Guid ContractTypeID = new Guid(0x335f86a1, 0xd009, 0x012c, 0x8b, 0x45, 0x1f, 0x43, 0xc2, 0x38, 0x2c, 0x2d);

        /// <summary>
        /// Card type name for "Contract".
        /// </summary>
        public const string ContractTypeName = "Contract";

        /// <summary>
        /// Card type identifier for "Document": {6D06C5A0-9687-4F6B-9BED-D3A081D84D9A}.
        /// </summary>
        public static readonly Guid DocumentTypeID = new Guid(0x6d06c5a0, 0x9687, 0x4f6b, 0x9b, 0xed, 0xd3, 0xa0, 0x81, 0xd8, 0x4d, 0x9a);

        /// <summary>
        /// Card type name for "Document".
        /// </summary>
        public const string DocumentTypeName = "Document";

        /// <summary>
        /// Card type identifier for "Incoming": {001F99FD-5BF3-0679-9B6F-455767AF72B5}.
        /// </summary>
        public static readonly Guid IncomingTypeID = new Guid(0x001f99fd, 0x5bf3, 0x0679, 0x9b, 0x6f, 0x45, 0x57, 0x67, 0xaf, 0x72, 0xb5);

        /// <summary>
        /// Card type name for "Incoming".
        /// </summary>
        public const string IncomingTypeName = "Incoming";

        /// <summary>
        /// Card type identifier for "Outgoing": {C59B76D9-C0DB-01CD-A3FB-B339740F0620}.
        /// </summary>
        public static readonly Guid OutgoingTypeID = new Guid(0xc59b76d9, 0xc0db, 0x01cd, 0xa3, 0xfb, 0xb3, 0x39, 0x74, 0x0f, 0x06, 0x20);

        /// <summary>
        /// Card type name for "Outgoing".
        /// </summary>
        public const string OutgoingTypeName = "Outgoing";

        /// <summary>
        /// Card type identifier for "Protocol": {4D9F9590-0131-4D32-9710-5E07C282B5D3}.
        /// </summary>
        public static readonly Guid ProtocolTypeID = new Guid(0x4d9f9590, 0x0131, 0x4d32, 0x97, 0x10, 0x5e, 0x07, 0xc2, 0x82, 0xb5, 0xd3);

        /// <summary>
        /// Card type name for "Protocol".
        /// </summary>
        public const string ProtocolTypeName = "Protocol";

        /// <summary>
        /// Card type identifier for "KrExampleDialogSatellite": {7CFE67A4-0B8E-423B-8C15-8E2C584B429B}.
        /// </summary>
        public static readonly Guid KrExampleDialogSatelliteTypeID = new Guid(0x7cfe67a4, 0x0b8e, 0x423b, 0x8c, 0x15, 0x8e, 0x2c, 0x58, 0x4b, 0x42, 0x9b);

        /// <summary>
        /// Card type name for "KrExampleDialogSatellite".
        /// </summary>
        public const string KrExampleDialogSatelliteTypeName = "KrExampleDialogSatellite";

        #endregion

        #region KrProcess Fields and Names

        /// <summary>
        /// Card type identifier for "KrCard": {21BCA3FC-F75F-413B-B5C8-49538CBFC761}.
        /// </summary>
        public static readonly Guid KrCardTypeID = new Guid(0x21bca3fc, 0xf75f, 0x413b, 0xb5, 0xc8, 0x49, 0x53, 0x8c, 0xbf, 0xc7, 0x61);

        /// <summary>
        /// Card type name for "KrCard".
        /// </summary>
        public const string KrCardTypeName = "KrCard";

        /// <summary>
        /// Card type identifier for "KrSatellite": {4115F07E-0AAA-4563-A749-0450C1A850AF}.
        /// </summary>
        public static readonly Guid KrSatelliteTypeID = new Guid(0x4115f07e, 0x0aaa, 0x4563, 0xa7, 0x49, 0x04, 0x50, 0xc1, 0xa8, 0x50, 0xaf);

        /// <summary>
        /// Card type name for "KrSatellite".
        /// </summary>
        public const string KrSatelliteTypeName = "KrSatellite";

        /// <summary>
        /// Card type identifier for "KrSecondarySatellite": {7593C144-31F7-43C2-9C4B-E3B776562F8F}.
        /// </summary>
        public static readonly Guid KrSecondarySatelliteTypeID = new Guid(0x7593c144, 0x31f7, 0x43c2, 0x9c, 0x4b, 0xe3, 0xb7, 0x76, 0x56, 0x2f, 0x8f);

        /// <summary>
        /// Card type name for "KrSecondarySatellite".
        /// </summary>
        public const string KrSecondarySatelliteTypeName = "KrSecondarySatellite";

        /// <summary>
        /// Card type identifier for "KrPerformersSettings": {5AC67186-B62B-4DC4-B9B9-E74D18F53600}.
        /// </summary>
        public static readonly Guid KrPerformersSettingsTypeID = new Guid(0x5ac67186, 0xb62b, 0x4dc4, 0xb9, 0xb9, 0xe7, 0x4d, 0x18, 0xf5, 0x36, 0x00);

        /// <summary>
        /// Card type name for "KrPerformersSettings".
        /// </summary>
        public const string KrPerformersSettingsTypeName = "KrPerformersSettings";

        /// <summary>
        /// Card type identifier for "KrAuthorSettings": {02DBC094-7F2C-48B0-ACF3-1FC6DDDF015C}.
        /// </summary>
        public static readonly Guid KrAuthorSettingsTypeID = new Guid(0x02dbc094, 0x7f2c, 0x48b0, 0xac, 0xf3, 0x1f, 0xc6, 0xdd, 0xdf, 0x01, 0x5c);

        /// <summary>
        /// Card type name for "KrAuthorSettings".
        /// </summary>
        public const string KrAuthorSettingsTypeName = "KrAuthorSettings";

        /// <summary>
        /// Card type identifier for "KrHistoryManagementStageTypeSettings": {CFE5E2AF-1014-4DDB-AFA1-7450623B103A}.
        /// </summary>
        public static readonly Guid KrHistoryManagementStageTypeSettingsTypeID = new Guid(0xcfe5e2af, 0x1014, 0x4ddb, 0xaf, 0xa1, 0x74, 0x50, 0x62, 0x3b, 0x10, 0x3a);

        /// <summary>
        /// Card type name for "KrHistoryManagementStageTypeSettings".
        /// </summary>
        public const string KrHistoryManagementStageTypeSettingsTypeName = "KrHistoryManagementStageTypeSettings";

        /// <summary>
        /// Card type identifier for "KrTaskKindSettings": {F1E87CA5-E1F1-4E5A-ACBF-D6CFB20BCB11}.
        /// </summary>
        public static readonly Guid KrTaskKindSettingsTypeID = new Guid(0xf1e87ca5, 0xe1f1, 0x4e5a, 0xac, 0xbf, 0xd6, 0xcf, 0xb2, 0x0b, 0xcb, 0x11);

        /// <summary>
        /// Card type name for "KrTaskKindSettings".
        /// </summary>
        public const string KrTaskKindSettingsTypeName = "KrTaskKindSettings";

        /// <summary>
        /// Card type identifier for "KrApprovalStageTypeSettings": {4A377758-2366-47E9-98AC-C5F553974236}.
        /// </summary>
        public static readonly Guid KrApprovalStageTypeSettingsTypeID = new Guid(0x4a377758, 0x2366, 0x47e9, 0x98, 0xac, 0xc5, 0xf5, 0x53, 0x97, 0x42, 0x36);

        /// <summary>
        /// Card type name for "KrApprovalStageTypeSettings".
        /// </summary>
        public const string KrApprovalStageTypeSettingsTypeName = "KrApprovalStageTypeSettings";

        /// <summary>
        /// Card type identifier for "KrApprovalAction": {70762C81-BD23-4580-A3FB-C452604F6E78}.
        /// </summary>
        public static readonly Guid KrApprovalActionTypeID = new Guid(0x70762c81, 0xbd23, 0x4580, 0xa3, 0xfb, 0xc4, 0x52, 0x60, 0x4f, 0x6e, 0x78);

        /// <summary>
        /// Card type name for "KrApprovalAction".
        /// </summary>
        public const string KrApprovalActionTypeName = "KrApprovalAction";

        /// <summary>
        /// Card type identifier for "KrSigningAction": {01762690-A192-4E8E-9B5E-0110666FD977}.
        /// </summary>
        public static readonly Guid KrSigningActionTypeID = new Guid(0x01762690, 0xa192, 0x4e8e, 0x9b, 0x5e, 0x01, 0x10, 0x66, 0x6f, 0xd9, 0x77);

        /// <summary>
        /// Card type name for "KrSigningAction".
        /// </summary>
        public const string KrSigningActionTypeName = "KrSigningAction";

        /// <summary>
        /// Card type identifier for "KrAmendingAction": {9C530E93-EC3A-48BA-B09C-EE9ECEB2173E}.
        /// </summary>
        public static readonly Guid KrAmendingActionTypeID = new Guid(0x9c530e93, 0xec3a, 0x48ba, 0xb0, 0x9c, 0xee, 0x9e, 0xce, 0xb2, 0x17, 0x3e);

        /// <summary>
        /// Card type name for "KrAmendingAction".
        /// </summary>
        public const string KrAmendingActionTypeName = "KrAmendingAction";

        /// <summary>
        /// Card type identifier for "KrUniversalTaskAction": {231EEA47-DB41-4AD4-8846-164DA4EF4048}.
        /// </summary>
        public static readonly Guid KrUniversalTaskActionTypeID = new Guid(0x231eea47, 0xdb41, 0x4ad4, 0x88, 0x46, 0x16, 0x4d, 0xa4, 0xef, 0x40, 0x48);

        /// <summary>
        /// Card type name for "KrUniversalTaskAction".
        /// </summary>
        public const string KrUniversalTaskActionTypeName = "KrUniversalTaskAction";

        /// <summary>
        /// Card type identifier for "KrResolutionAction": {235E42EA-7AD8-4321-9A3A-91B752985EF0}.
        /// </summary>
        public static readonly Guid KrResolutionActionTypeID = new Guid(0x235e42ea, 0x7ad8, 0x4321, 0x9a, 0x3a, 0x91, 0xb7, 0x52, 0x98, 0x5e, 0xf0);

        /// <summary>
        /// Card type name for "KrResolutionAction".
        /// </summary>
        public const string KrResolutionActionTypeName = "KrResolutionAction";

        /// <summary>
        /// Card type identifier for "KrRouteInitializationAction": {25CA876A-50B2-4C27-B847-56D4FC597934}.
        /// </summary>
        public static readonly Guid KrRouteInitializationActionTypeID = new Guid(0x25ca876a, 0x50b2, 0x4c27, 0xb8, 0x47, 0x56, 0xd4, 0xfc, 0x59, 0x79, 0x34);

        /// <summary>
        /// Card type name for "KrRouteInitializationAction".
        /// </summary>
        public const string KrRouteInitializationActionTypeName = "KrRouteInitializationAction";

        /// <summary>
        /// Card type identifier for "KrDialogStageTypeSettings": {71464F65-E572-4FBA-B54F-3E9F9EF0125A}.
        /// </summary>
        public static readonly Guid KrDialogStageTypeSettingsTypeID = new Guid(0x71464f65, 0xe572, 0x4fba, 0xb5, 0x4f, 0x3e, 0x9f, 0x9e, 0xf0, 0x12, 0x5a);

        /// <summary>
        /// Card type name for "KrDialogStageTypeSettings".
        /// </summary>
        public const string KrDialogStageTypeSettingsTypeName = "KrDialogStageTypeSettings";

        /// <summary>
        /// Card type identifier for "KrResolutionStageTypeSettings": {C898080F-0FA7-45D9-BBC9-F28DFD2C8F1C}.
        /// </summary>
        public static readonly Guid KrResolutionStageTypeSettingsTypeID = new Guid(0xc898080f, 0x0fa7, 0x45d9, 0xbb, 0xc9, 0xf2, 0x8d, 0xfd, 0x2c, 0x8f, 0x1c);

        /// <summary>
        /// Card type name for "KrResolutionStageTypeSettings".
        /// </summary>
        public const string KrResolutionStageTypeSettingsTypeName = "KrResolutionStageTypeSettings";

        /// <summary>
        /// Card type identifier for "KrUniversalTaskStageTypeSettings": {EADA56ED-7D98-4E6E-9D9F-950D8AA42696}.
        /// </summary>
        public static readonly Guid KrUniversalTaskStageTypeSettingsTypeID = new Guid(0xeada56ed, 0x7d98, 0x4e6e, 0x9d, 0x9f, 0x95, 0x0d, 0x8a, 0xa4, 0x26, 0x96);

        /// <summary>
        /// Card type name for "KrUniversalTaskStageTypeSettings".
        /// </summary>
        public const string KrUniversalTaskStageTypeSettingsTypeName = "KrUniversalTaskStageTypeSettings";

        /// <summary>
        /// Card type identifier for "KrSigningStageTypeSettings": {5C473877-1E54-495C-8ECA-74885D292786}.
        /// </summary>
        public static readonly Guid KrSigningStageTypeSettingsTypeID = new Guid(0x5c473877, 0x1e54, 0x495c, 0x8e, 0xca, 0x74, 0x88, 0x5d, 0x29, 0x27, 0x86);

        /// <summary>
        /// Card type name for "KrSigningStageTypeSettings".
        /// </summary>
        public const string KrSigningStageTypeSettingsTypeName = "KrSigningStageTypeSettings";

        /// <summary>
        /// Card type identifier for "KrProcessManagementStageTypeSettings": {FF753641-0691-4CFC-A8CC-BAA89B25A83B}.
        /// </summary>
        public static readonly Guid KrProcessManagementStageTypeSettingsTypeID = new Guid(0xff753641, 0x0691, 0x4cfc, 0xa8, 0xcc, 0xba, 0xa8, 0x9b, 0x25, 0xa8, 0x3b);

        /// <summary>
        /// Card type name for "KrProcessManagementStageTypeSettings".
        /// </summary>
        public const string KrProcessManagementStageTypeSettingsTypeName = "KrProcessManagementStageTypeSettings";

        #endregion

        #region Settings Fields and Names

        /// <summary>
        /// Card type identifier for "KrPermissions": {FA9DBDAC-8708-41DF-BD72-900F69655DFA}.
        /// </summary>
        public static readonly Guid KrPermissionsTypeID = new Guid(0xfa9dbdac, 0x8708, 0x41df, 0xbd, 0x72, 0x90, 0x0f, 0x69, 0x65, 0x5d, 0xfa);

        /// <summary>
        /// Card type name for "KrPermissions".
        /// </summary>
        public const string KrPermissionsTypeName = "KrPermissions";

        /// <summary>
        /// Card type identifier for "KrSettings": {35A03878-57B6-4263-AE36-92EB59032132}.
        /// </summary>
        public static readonly Guid KrSettingsTypeID = new Guid(0x35a03878, 0x57b6, 0x4263, 0xae, 0x36, 0x92, 0xeb, 0x59, 0x03, 0x21, 0x32);

        /// <summary>
        /// Card type name for "KrSettings".
        /// </summary>
        public const string KrSettingsTypeName = "KrSettings";

        /// <summary>
        /// Card type identifier for "ReportPermissions": {65A88390-3B00-4B74-925D-B635027FEFF2}.
        /// </summary>
        public static readonly Guid ReportPermissionsTypeID = new Guid(0x65a88390, 0x3b00, 0x4b74, 0x92, 0x5d, 0xb6, 0x35, 0x02, 0x7f, 0xef, 0xf2);

        /// <summary>
        /// Card type name for "ReportPermissions".
        /// </summary>
        public const string ReportPermissionsTypeName = "ReportPermissions";

        /// <summary>
        /// Card type identifier for "Forum": {4D9425F8-A986-4F8A-A37A-D8A86598225C}.
        /// </summary>
        public static readonly Guid ForumTypeID = new Guid(0x4d9425f8, 0xa986, 0x4f8a, 0xa3, 0x7a, 0xd8, 0xa8, 0x65, 0x98, 0x22, 0x5c);

        /// <summary>
        /// Card type name for "Forum".
        /// </summary>
        public const string ForumTypeName = "Forum";

        #endregion

        #region UserSettings Fields and Names

        /// <summary>
        /// Card type identifier for "KrUserSettings": {793864E5-39E5-4D4F-AF59-C3D7A9FACCA9}.
        /// </summary>
        public static readonly Guid KrUserSettingsTypeID = new Guid(0x793864e5, 0x39e5, 0x4d4f, 0xaf, 0x59, 0xc3, 0xd7, 0xa9, 0xfa, 0xcc, 0xa9);

        /// <summary>
        /// Card type name for "KrUserSettings".
        /// </summary>
        public const string KrUserSettingsTypeName = "KrUserSettings";

        /// <summary>
        /// Card type identifier for "TagsUserSettings": {7101497d-9057-43d9-99c6-1d425eadf3bd}.
        /// </summary>
        public static readonly Guid TagsUserSettingsTypeID = new Guid(0x7101497d, 0x9057, 0x43d9, 0x99, 0xc6, 0x1d, 0x42, 0x5e, 0xad, 0xf3, 0xbd);

        /// <summary>
        /// Card type name for "TagsUserSettings".
        /// </summary>
        public const string TagsUserSettingsTypeName = "TagsUserSettings";

        #endregion

        #region Wf Fields and Names

        /// <summary>
        /// Card type identifier for "WfSatellite": {A382EC40-6321-42E5-A9F9-C7B103FEB38D}.
        /// </summary>
        public static readonly Guid WfSatelliteTypeID = new Guid(0xa382ec40, 0x6321, 0x42e5, 0xa9, 0xf9, 0xc7, 0xb1, 0x03, 0xfe, 0xb3, 0x8d);

        /// <summary>
        /// Card type name for "WfSatellite".
        /// </summary>
        public const string WfSatelliteTypeName = "WfSatellite";

        /// <summary>
        /// Card type identifier for "WfTaskCard": {DE75A343-8164-472D-A20E-4937819760AC}.
        /// </summary>
        public static readonly Guid WfTaskCardTypeID = new Guid(0xde75a343, 0x8164, 0x472d, 0xa2, 0x0e, 0x49, 0x37, 0x81, 0x97, 0x60, 0xac);

        /// <summary>
        /// Card type name for "WfTaskCard".
        /// </summary>
        public const string WfTaskCardTypeName = "WfTaskCard";

        #endregion

        #region KrCompile

        /// <summary>
        /// Card type identifier for "KrStageCommonMethod": {66CD517B-5423-43DB-8374-F50EC0D967EB}.
        /// </summary>
        public static readonly Guid KrStageCommonMethodTypeID = new Guid(0x66cd517b, 0x5423, 0x43db, 0x83, 0x74, 0xf5, 0x0e, 0xc0, 0xd9, 0x67, 0xeb);

        /// <summary>
        /// Card type name for "KrStageCommonMethod".
        /// </summary>
        public const string KrStageCommonMethodTypeName = "KrStageCommonMethod";

        /// <summary>
        /// Отображаемое имя типа карточки "KrStageCommonMethod".
        /// </summary>
        public const string KrStageCommonMethodTypeCaption = "$CardTypes_TypesNames_KrCommonMethod";

        /// <summary>
        /// Card type identifier for "KrStageTemplate": {2FA85BB3-BBA4-4AB6-BA97-652106DB96DE}.
        /// </summary>
        public static readonly Guid KrStageTemplateTypeID = new Guid(0x2fa85bb3, 0xbba4, 0x4ab6, 0xba, 0x97, 0x65, 0x21, 0x06, 0xdb, 0x96, 0xde);

        /// <summary>
        /// Card type name for "KrStageTemplate".
        /// </summary>
        public const string KrStageTemplateTypeName = "KrStageTemplate";

        /// <summary>
        /// Отображаемое имя типа карточки "KrStageTemplate".
        /// </summary>
        public static readonly string KrStageTemplateTypeCaption = "$CardTypes_TypesNames_KrStageTemplate";

        /// <summary>
        /// Card type identifier for "KrStageGroup": {9CE8E9F4-CBF0-4B5F-A569-B508B1FD4B3A}.
        /// </summary>
        public static readonly Guid KrStageGroupTypeID = new Guid(0x9ce8e9f4, 0xcbf0, 0x4b5f, 0xa5, 0x69, 0xb5, 0x08, 0xb1, 0xfd, 0x4b, 0x3a);

        /// <summary>
        /// Card type name for "KrStageGroup".
        /// </summary>
        public const string KrStageGroupTypeName = "KrStageGroup";

        /// <summary>
        /// Отображаемое имя типа карточки "KrStageGroup".
        /// </summary>
        public static readonly string KrStageGroupTypeCaption = "$CardTypes_TypesNames_KrStageGroup";

        /// <summary>
        /// Card type identifier for "KrSecondaryProcess": {61420FA1-CC1F-47CB-B0BB-4EA8EE77F51A}.
        /// </summary>
        public static readonly Guid KrSecondaryProcessTypeID = new Guid(0x61420fa1, 0xcc1f, 0x47cb, 0xb0, 0xbb, 0x4e, 0xa8, 0xee, 0x77, 0xf5, 0x1a);

        /// <summary>
        /// Card type name for "KrSecondaryProcess".
        /// </summary>
        public const string KrSecondaryProcessTypeName = "KrSecondaryProcess";

        /// <summary>
        /// Отображаемое имя типа карточки "KrSecondaryProcess".
        /// </summary>
        public static readonly string KrSecondaryProcessTypeCaption = "$CardTypes_TypesNames_KrSecondaryProcess";

        /// <summary>
        /// Card type identifier for "KrTemplateCard": {D3D3D2E1-A45E-40C5-8228-CD304FDF6F4D}.
        /// </summary>
        public static readonly Guid KrTemplateCardTypeID = new Guid(0xd3d3d2e1, 0xa45e, 0x40c5, 0x82, 0x28, 0xcd, 0x30, 0x4f, 0xdf, 0x6f, 0x4d);

        /// <summary>
        /// Card type name for "KrTemplateCard".
        /// </summary>
        public const string KrTemplateCardTypeName = "KrTemplateCard";

        #endregion

        #region WorkflowEngine

        /// <summary>
        /// Card type identifier for "KrChangeStateAction": {4F07209C-AB6B-44F6-9460-F594C3BDF8A3}.
        /// </summary>
        public static readonly Guid KrChangeStateActionTypeID = new Guid(0x4f07209c, 0xab6b, 0x44f6, 0x94, 0x60, 0xf5, 0x94, 0xc3, 0xbd, 0xf8, 0xa3);

        /// <summary>
        /// Card type name for "KrChangeStateAction".
        /// </summary>
        public const string KrChangeStateActionTypeName = "KrChangeStateAction";

        /// <summary>
        /// Card type identifier for "WorkflowCreateCardAction": {1C7A8067-CD49-45AB-A3C9-04A0CFE26C43}.
        /// </summary>
        public static readonly Guid WorkflowCreateCardActionTypeID = new Guid(0x1c7a8067, 0xcd49, 0x45ab, 0xa3, 0xc9, 0x04, 0xa0, 0xcf, 0xe2, 0x6c, 0x43);

        /// <summary>
        /// Card type name for "WorkflowCreateCardAction".
        /// </summary>
        public const string WorkflowCreateCardActionTypeName = "WorkflowCreateCardAction";

        /// <summary>
        /// Card type identifier for "KrAcquaintanceAction": {956A34EB-8318-4D35-92A2-C0DF118C01EA}.
        /// </summary>
        public static readonly Guid KrAcquaintanceActionTypeID = new Guid(0x956a34eb, 0x8318, 0x4d35, 0x92, 0xa2, 0xc0, 0xdf, 0x11, 0x8c, 0x01, 0xea);

        /// <summary>
        /// Card type name for "KrAcquaintanceAction".
        /// </summary>
        public const string KrAcquaintanceActionTypeName = "KrAcquaintanceAction";

        /// <summary>
        /// Card type identifier for "KrRegistrationAction": {BF4641AD-F4DC-4A75-83F4-534CBA8BF225}.
        /// </summary>
        public static readonly Guid KrRegistrationActionTypeID = new Guid(0xbf4641ad, 0xf4dc, 0x4a75, 0x83, 0xf4, 0x53, 0x4c, 0xba, 0x8b, 0xf2, 0x25);

        /// <summary>
        /// Card type name for "KrRegistrationAction".
        /// </summary>
        public const string KrRegistrationActionTypeName = "KrRegistrationAction";

        /// <summary>
        /// Card type identifier for "KrDeregistrationAction": {94E91C8C-1336-4C04-87C5-11CEB9839DE3}.
        /// </summary>
        public static readonly Guid KrDeregistrationActionTypeID = new Guid(0x94e91c8c, 0x1336, 0x4c04, 0x87, 0xc5, 0x11, 0xce, 0xb9, 0x83, 0x9d, 0xe3);

        /// <summary>
        /// Card type name for "KrDeregistrationAction".
        /// </summary>
        public const string KrDeregistrationActionTypeName = "KrDeregistrationAction";

        /// <summary>
        /// Card type identifier for "KrTaskRegistrationAction": {2D6CBF60-1C5A-40FD-A091-FA42BD4441BC}.
        /// </summary>
        public static readonly Guid KrTaskRegistrationActionTypeID = new Guid(0x2d6cbf60, 0x1c5a, 0x40fd, 0xa0, 0x91, 0xfa, 0x42, 0xbd, 0x44, 0x41, 0xbc);

        /// <summary>
        /// Card type name for "KrTaskRegistrationAction".
        /// </summary>
        public const string KrTaskRegistrationActionTypeName = "KrTaskRegistrationAction";

        #endregion

        #region VirtualFile

        /// <summary>
        /// Card type identifier for "KrVirtualFile": {81250A95-5C1E-488C-A423-106E7F982C6B}.
        /// </summary>
        public static readonly Guid KrVirtualFileTypeID = new Guid(0x81250a95, 0x5c1e, 0x488c, 0xa4, 0x23, 0x10, 0x6e, 0x7f, 0x98, 0x2c, 0x6b);

        /// <summary>
        /// Card type name for "KrVirtualFile".
        /// </summary>
        public const string KrVirtualFileTypeName = "KrVirtualFile";

        #endregion

        #region Without Group Fields and Names

        /// <summary>
        /// Card type identifier for "Car": {D0006E40-A342-4797-8D77-6501C4B7C4AC}.
        /// </summary>
        public static readonly Guid CarTypeID = new Guid(0xd0006e40, 0xa342, 0x4797, 0x8d, 0x77, 0x65, 0x01, 0xc4, 0xb7, 0xc4, 0xac);

        /// <summary>
        /// Card type name for "Car".
        /// </summary>
        public const string CarTypeName = "Car";

        #endregion
    }
}
