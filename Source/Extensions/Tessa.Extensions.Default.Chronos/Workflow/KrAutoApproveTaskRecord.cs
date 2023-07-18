using System;

namespace Tessa.Extensions.Default.Chronos.Workflow
{
    public sealed class KrAutoApproveTaskRecord
    {
        #region Constructors

        public KrAutoApproveTaskRecord(Guid cardID, string cardTypeName, Guid taskId, string approvalComment)
        {
            this.cardID = cardID;
            this.cardTypeName = cardTypeName;
            this.taskID = taskId;
            this.approvalComment = approvalComment;
        }

        #endregion

        #region Properties

        private readonly Guid cardID;

        public Guid CardID
        {
            get { return this.cardID; }
        }


        private readonly string cardTypeName;

        public string CardTypeName
        {
            get { return this.cardTypeName; }
        }


        private readonly Guid taskID;

        public Guid TaskID
        {
            get { return this.taskID; }
        }


        private string approvalComment;

        public string ApprovalComment
        {
            get { return this.approvalComment; }
            set { this.approvalComment = value; }
        }

        #endregion
    }
}