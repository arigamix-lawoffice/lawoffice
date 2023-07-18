using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    [StorageObjectGenerator(GenerateDefaultConstructor = false)]
    public sealed partial class KrDocType : StorageObject, IKrType
    {
        #region Constructors

        public KrDocType(Dictionary<string, object> storage)
            : base(storage)
        {
        }

        #endregion

        #region Storage Properties

        private const string IDKey = "ID";

        public Guid ID
        {
            get { return this.Get<Guid>(IDKey); }
            set { this.Set(IDKey, value); }
        }

        private const string NameKey = "Name";

        public string Name
        {
            get { return this.Get<string>(NameKey); }
            set { this.Set(NameKey, value); }
        }

        private const string CaptionKey = "Caption";

        public string Caption
        {
            get { return this.Get<string>(CaptionKey); }
            set { this.Set(CaptionKey, value); }
        }

        private const string CardTypeIDKey = "CardTypeID";

        public Guid CardTypeID
        {
            get { return this.Get<Guid>(CardTypeIDKey); }
            set { this.Set(CardTypeIDKey, value); }
        }

        private const string CardTypeNameKey = "CardTypeName";

        public string CardTypeName
        {
            get { return this.Get<string>(CardTypeNameKey); }
            set { this.Set(CardTypeNameKey, value); }
        }

        private const string UseApprovingKey = "UseApproving";

        public bool UseApproving
        {
            get { return this.Get<bool>(UseApprovingKey); }
            set { this.Set(UseApprovingKey, value); }
        }

        private const string UseRegistrationKey = "UseRegistration";

        public bool UseRegistration
        {
            get { return this.Get<bool>(UseRegistrationKey); }
            set { this.Set(UseRegistrationKey, value); }
        }

        private const string UseResolutionsKey = "UseResolutions";

        public bool UseResolutions
        {
            get { return this.Get<bool>(UseResolutionsKey); }
            set { this.Set(UseResolutionsKey, value); }
        }

        private const string DisableChildResolutionDateCheckKey = "DisableChildResolutionDateCheck";

        public bool DisableChildResolutionDateCheck
        {
            get { return this.Get<bool>(DisableChildResolutionDateCheckKey); }
            set { this.Set(DisableChildResolutionDateCheckKey, value); }
        }

        private const string DocNumberRegularAutoAssignmentIDKey = "DocNumberRegularAutoAssignmentID";

        public KrDocNumberRegularAutoAssignmentID DocNumberRegularAutoAssignmentID
        {
            get { return this.Get<KrDocNumberRegularAutoAssignmentID>(DocNumberRegularAutoAssignmentIDKey); }
            set { this.Set(DocNumberRegularAutoAssignmentIDKey, value); }
        }

        private const string DocNumberRegularSequenceKey = "DocNumberRegularSequence";

        public string DocNumberRegularSequence
        {
            get { return this.Get<string>(DocNumberRegularSequenceKey); }
            set { this.Set(DocNumberRegularSequenceKey, value); }
        }

        private const string DocNumberRegularFormatKey = "DocNumberRegularFormat";

        public string DocNumberRegularFormat
        {
            get { return this.Get<string>(DocNumberRegularFormatKey); }
            set { this.Set(DocNumberRegularFormatKey, value); }
        }

        private const string AllowManualRegularDocNumberAssignmentKey = "AllowManualRegularDocNumberAssignment";

        public bool AllowManualRegularDocNumberAssignment
        {
            get { return this.Get<bool>(AllowManualRegularDocNumberAssignmentKey); }
            set { this.Set(AllowManualRegularDocNumberAssignmentKey, value); }
        }

        private const string DocNumberRegistrationAutoAssignmentIDKey = "DocNumberRegistrationAutoAssignmentID";

        public KrDocNumberRegistrationAutoAssignmentID DocNumberRegistrationAutoAssignmentID
        {
            get { return this.Get<KrDocNumberRegistrationAutoAssignmentID>(DocNumberRegistrationAutoAssignmentIDKey); }
            set { this.Set(DocNumberRegistrationAutoAssignmentIDKey, value); }
        }

        private const string DocNumberRegistrationSequenceKey = "DocNumberRegistrationSequence";

        public string DocNumberRegistrationSequence
        {
            get { return this.Get<string>(DocNumberRegistrationSequenceKey); }
            set { this.Set(DocNumberRegistrationSequenceKey, value); }
        }

        private const string DocNumberRegistrationFormatKey = "DocNumberRegistrationFormat";

        public string DocNumberRegistrationFormat
        {
            get { return this.Get<string>(DocNumberRegistrationFormatKey); }
            set { this.Set(DocNumberRegistrationFormatKey, value); }
        }

        private const string AllowManualRegistrationDocNumberAssignmentKey =
            "AllowManualRegistrationDocNumberAssignment";

        public bool AllowManualRegistrationDocNumberAssignment
        {
            get { return this.Get<bool>(AllowManualRegistrationDocNumberAssignmentKey); }
            set { this.Set(AllowManualRegistrationDocNumberAssignmentKey, value); }
        }

        private const string ReleaseRegistrationNumberOnFinalDeletionKey = "ReleaseRegistrationNumberOnFinalDeletion";

        public bool ReleaseRegistrationNumberOnFinalDeletion
        {
            get { return this.Get<bool>(ReleaseRegistrationNumberOnFinalDeletionKey); }
            set { this.Set(ReleaseRegistrationNumberOnFinalDeletionKey, value); }
        }

        private const string ReleaseRegularNumberOnFinalDeletionKey = "ReleaseRegularNumberOnFinalDeletion";

        public bool ReleaseRegularNumberOnFinalDeletion
        {
            get { return this.Get<bool>(ReleaseRegularNumberOnFinalDeletionKey); }
            set { this.Set(ReleaseRegularNumberOnFinalDeletionKey, value); }
        }

        private const string HideCreationButtonKey = "HideCreationButton";

        public bool HideCreationButton
        {
            get { return this.Get<bool>(HideCreationButtonKey); }
            set { this.Set(HideCreationButtonKey, value); }
        }

        private const string HideRouteTabKey = "HideRouteTab";

        public bool HideRouteTab
        {
            get { return this.Get<bool>(HideRouteTabKey); }
            set { this.Set(HideRouteTabKey, value); }
        }

        private const string UseForumKey = "UseForum";
        public bool UseForum
        {
            get { return this.Get<bool>(UseForumKey); }
            set { this.Set(UseForumKey, value); }
        }
        
        private const string UseDefaultDiscussionTabKey = "UseDefaultDiscussionTab";
        public bool UseDefaultDiscussionTab
        {
            get { return this.Get<bool>(UseDefaultDiscussionTabKey); }
            set { this.Set(UseDefaultDiscussionTabKey, value); }
        }

        private const string UseRoutesInWorkflowEngineKey = "UseRoutesInWorkflowEngine";

        /// <summary>
        /// Возвращает или задаёт значение, показывающее, используются ли маршруты в бизнес процессах или нет.
        /// </summary>
        public bool UseRoutesInWorkflowEngine
        {
            get { return this.Get<bool>(UseRoutesInWorkflowEngineKey); }
            set { this.Set(UseRoutesInWorkflowEngineKey, value); }
        }

        #endregion
    }
}
