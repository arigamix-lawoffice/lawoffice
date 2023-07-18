using System;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Идентификаторы и имена типов заданий, задействованных в типовом решении.
    /// </summary>
    public static class DefaultTaskTypes
    {
        #region KrProcess Fields and Names

        /// <summary>
        /// Task type identifier for "KrApprove": {E4D7F6BF-FEA9-4A3B-8A5A-E1A0A40DE74C}.
        /// </summary>
        public static readonly Guid KrApproveTypeID = new Guid(0xe4d7f6bf, 0xfea9, 0x4a3b, 0x8a, 0x5a, 0xe1, 0xa0, 0xa4, 0x0d, 0xe7, 0x4c);

        /// <summary>
        /// Task type name for "KrApprove".
        /// </summary>
        public const string KrApproveTypeName = "KrApprove";


        /// <summary>
        /// Task type identifier for "KrCancel": {B0FF98BE-438B-41B8-B833-709B817E2EC1}.
        /// </summary>
        public static readonly Guid KrCancelTypeID = new Guid(0xb0ff98be, 0x438b, 0x41b8, 0xb8, 0x33, 0x70, 0x9b, 0x81, 0x7e, 0x2e, 0xc1);

        /// <summary>
        /// Task type name for "KrCancel".
        /// </summary>
        public const string KrCancelTypeName = "KrCancel";


        /// <summary>
        /// Task type identifier for "KrEdit": {E19CA9B5-48BE-4FDF-8DC5-78534B4767DE}.
        /// </summary>
        public static readonly Guid KrEditTypeID = new Guid(0xe19ca9b5, 0x48be, 0x4fdf, 0x8d, 0xc5, 0x78, 0x53, 0x4b, 0x47, 0x67, 0xde);

        /// <summary>
        /// Task type name for "KrEdit".
        /// </summary>
        public const string KrEditTypeName = "KrEdit";


        /// <summary>
        /// Task type identifier for "KrEditInterject": {C9B93AE3-9B7B-4431-A306-AACE4AEA8732}.
        /// </summary>
        public static readonly Guid KrEditInterjectTypeID = new Guid(0xc9b93ae3, 0x9b7b, 0x4431, 0xa3, 0x06, 0xaa, 0xce, 0x4a, 0xea, 0x87, 0x32);

        /// <summary>
        /// Task type name for "KrEditInterject".
        /// </summary>
        public const string KrEditInterjectTypeName = "KrEditInterject";


        /// <summary>
        /// Task type identifier for "KrInfoForInitiator": {C6F3828F-B001-46F6-B121-3F3ED9E65CDE}.
        /// </summary>
        public static readonly Guid KrInfoForInitiatorTypeID = new Guid(0xc6f3828f, 0xb001, 0x46f6, 0xb1, 0x21, 0x3f, 0x3e, 0xd9, 0xe6, 0x5c, 0xde);

        /// <summary>
        /// Task type name for "KrInfoForInitiator".
        /// </summary>
        public const string KrInfoForInitiatorTypeName = "KrInfoForInitiator";


        /// <summary>
        /// Task type identifier for "KrInfoRequestComment": {90B4500E-8674-467E-8120-ADCBE62BACA5}.
        /// </summary>
        public static readonly Guid KrInfoRequestCommentTypeID = new Guid(0x90b4500e, 0x8674, 0x467e, 0x81, 0x20, 0xad, 0xcb, 0xe6, 0x2b, 0xac, 0xa5);

        /// <summary>
        /// Task type name for "KrInfoRequestComment".
        /// </summary>
        public const string KrInfoRequestCommentTypeName = "KrInfoRequestComment";


        /// <summary>
        /// Task type identifier for "KrAdditionalApproval": {B3D8EAE3-C6BF-4B59-BCC7-461D526C326C}.
        /// </summary>
        public static readonly Guid KrAdditionalApprovalTypeID = new Guid(0xb3d8eae3, 0xc6bf, 0x4b59, 0xbc, 0xc7, 0x46, 0x1d, 0x52, 0x6c, 0x32, 0x6c);

        /// <summary>
        /// Task type name for "KrAdditionalApproval".
        /// </summary>
        public const string KrAdditionalApprovalTypeName = "KrAdditionalApproval";


        /// <summary>
        /// Task type identifier for "KrInfoAdditionalApproval": {21BB8BBD-5080-49D2-81F4-9FC985B5B369}.
        /// </summary>
        public static readonly Guid KrInfoAdditionalApprovalTypeID = new Guid(0x21bb8bbd, 0x5080, 0x49d2, 0x81, 0xf4, 0x9f, 0xc9, 0x85, 0xb5, 0xb3, 0x69);

        /// <summary>
        /// Task type name for KrInfoAdditionalApproval".
        /// </summary>
        public const string KrInfoAdditionalApprovalTypeName = "KrInfoAdditionalApproval";


        /// <summary>
        /// Task type identifier for "KrRebuild": {4191115B-6D81-4688-8A44-63BD3EBCDAB4}.
        /// </summary>
        public static readonly Guid KrRebuildTypeID = new Guid(0x4191115b, 0x6d81, 0x4688, 0x8a, 0x44, 0x63, 0xbd, 0x3e, 0xbc, 0xda, 0xb4);

        /// <summary>
        /// Task type name for "KrRebuild".
        /// </summary>
        public const string KrRebuildTypeName = "KrRebuild";


        /// <summary>
        /// Task type identifier for "KrReject": {38ECD20C-3D0A-434A-A31F-7ABB447832E5}.
        /// </summary>
        public static readonly Guid KrRejectTypeID = new Guid(0x38ecd20c, 0x3d0a, 0x434a, 0xa3, 0x1f, 0x7a, 0xbb, 0x44, 0x78, 0x32, 0xe5);

        /// <summary>
        /// Task type name for "KrReject".
        /// </summary>
        public const string KrRejectTypeName = "KrReject";


        /// <summary>
        /// Task type identifier for "KrRequestComment": {F0360D95-4F88-4809-B926-57B34A2F69F5}.
        /// </summary>
        public static readonly Guid KrRequestCommentTypeID = new Guid(0xf0360d95, 0x4f88, 0x4809, 0xb9, 0x26, 0x57, 0xb3, 0x4a, 0x2f, 0x69, 0xf5);

        /// <summary>
        /// Task type name for "KrRequestComment".
        /// </summary>
        public const string KrRequestCommentTypeName = "KrRequestComment";


        /// <summary>
        /// Task type identifier for "KrStartApprovalProcess": {2EDEC511-8645-4179-B364-CA9D47D16B0F}.
        /// </summary>
        public static readonly Guid KrStartApprovalProcessTypeID = new Guid(0x2edec511, 0x8645, 0x4179, 0xb3, 0x64, 0xca, 0x9d, 0x47, 0xd1, 0x6b, 0x0f);

        /// <summary>
        /// Task type name for "KrStartApprovalProcess".
        /// </summary>
        public const string KrStartApprovalProcessTypeName = "KrStartApprovalProcess";


        /// <summary>
        /// Task type identifier for "KrRegistration": {09FDD6A3-3946-4F30-9EF9-F533FAD3A4A2}.
        /// </summary>
        public static readonly Guid KrRegistrationTypeID = new Guid(0x09fdd6a3, 0x3946, 0x4f30, 0x9e, 0xf9, 0xf5, 0x33, 0xfa, 0xd3, 0xa4, 0xa2);

        /// <summary>
        /// Task type name for "KrRegistration".
        /// </summary>
        public const string KrRegistrationTypeName = "KrRegistration";

        /// <summary>
        /// Task type identifier for "Signing": {968D68B3-A7C5-4B5D-BFA4-BB0F346880B6}.
        /// </summary>
        public static readonly Guid KrSigningTypeID = new Guid(0x968d68b3, 0xa7c5, 0x4b5d, 0xbf, 0xa4, 0xbb, 0x0f, 0x34, 0x68, 0x80, 0xb6);

        /// <summary>
        /// Task type name for "KrSign".
        /// </summary>
        public const string KrSigningTypeName = "KrSigning";

        /// <summary>
        /// Task type identifier for "KrUniversalTask": {9C6D9824-41D7-41E6-99F1-E19EA9E576C5}.
        /// </summary>
        public static readonly Guid KrUniversalTaskTypeID = new Guid(0x9c6d9824, 0x41d7, 0x41e6, 0x99, 0xf1, 0xe1, 0x9e, 0xa9, 0xe5, 0x76, 0xc5);

        /// <summary>
        /// Task type name for "KrUniversalTask".
        /// </summary>
        public const string KrUniversalTaskTypeName = "KrUniversalTask";

        /// <summary>
        /// Task type identifier for "KrShowDialog": {5309CE42-C4D2-4E99-A733-697C589311E7}.
        /// </summary>
        public static readonly Guid KrShowDialogTypeID = new Guid(0x5309ce42, 0xc4d2, 0x4e99, 0xa7, 0x33, 0x69, 0x7c, 0x58, 0x93, 0x11, 0xe7);

        /// <summary>
        /// Task type name for "KrShowDialog".
        /// </summary>
        public const string KrShowDialogTypeName = "KrShowDialog";
        
        #endregion

        #region TestProcess Fields and Names

        /// <summary>
        /// Task type identifier for "TestTask1": {929E345C-ACDF-41EA-ACB6-6BB308DE73AE}.
        /// </summary>
        public static readonly Guid TestTask1TypeID = new Guid(0x929e345c, 0xacdf, 0x41ea, 0xac, 0xb6, 0x6b, 0xb3, 0x08, 0xde, 0x73, 0xae);

        /// <summary>
        /// Task type name for "TestTask1".
        /// </summary>
        public const string TestTask1TypeName = "TestTask1";


        /// <summary>
        /// Task type identifier for "TestTask2": {5239E1B6-1ED6-4A3F-A11E-7E4C6E187AF6}.
        /// </summary>
        public static readonly Guid TestTask2TypeID = new Guid(0x5239e1b6, 0x1ed6, 0x4a3f, 0xa1, 0x1e, 0x7e, 0x4c, 0x6e, 0x18, 0x7a, 0xf6);

        /// <summary>
        /// Task type name for "TestTask2".
        /// </summary>
        public const string TestTask2TypeName = "TestTask2";

        #endregion

        #region Wf Fields and Names

        /// <summary>
        /// Task type identifier for "WfResolution": {928132FE-202D-4F9F-8EC5-5093EA2122D1}.
        /// </summary>
        public static readonly Guid WfResolutionTypeID = new Guid(0x928132fe, 0x202d, 0x4f9f, 0x8e, 0xc5, 0x50, 0x93, 0xea, 0x21, 0x22, 0xd1);

        /// <summary>
        /// Task type name for "WfResolution".
        /// </summary>
        public const string WfResolutionTypeName = "WfResolution";


        /// <summary>
        /// Task type identifier for "WfResolutionChild": {539ECFE8-5FB6-4681-8AA8-1EE4D9EF1DDA}.
        /// </summary>
        public static readonly Guid WfResolutionChildTypeID = new Guid(0x539ecfe8, 0x5fb6, 0x4681, 0x8a, 0xa8, 0x1e, 0xe4, 0xd9, 0xef, 0x1d, 0xda);

        /// <summary>
        /// Task type name for "WfResolutionChild".
        /// </summary>
        public const string WfResolutionChildTypeName = "WfResolutionChild";


        /// <summary>
        /// Task type identifier for "WfResolutionControl": {85A5E8D7-A901-46DF-9173-4D9A043CE6D3}.
        /// </summary>
        public static readonly Guid WfResolutionControlTypeID = new Guid(0x85a5e8d7, 0xa901, 0x46df, 0x91, 0x73, 0x4d, 0x9a, 0x04, 0x3c, 0xe6, 0xd3);

        /// <summary>
        /// Task type name for "WfResolutionControl".
        /// </summary>
        public const string WfResolutionControlTypeName = "WfResolutionControl";


        /// <summary>
        /// Task type identifier for "WfResolutionProject": {C989D91F-7DDD-455C-AE16-3BB380132BA8}.
        /// </summary>
        public static readonly Guid WfResolutionProjectTypeID = new Guid(0xc989d91f, 0x7ddd, 0x455c, 0xae, 0x16, 0x3b, 0xb3, 0x80, 0x13, 0x2b, 0xa8);

        /// <summary>
        /// Task type name for "WfResolutionProject".
        /// </summary>
        public const string WfResolutionProjectTypeName = "WfResolutionProject";

        #endregion
    }
}
