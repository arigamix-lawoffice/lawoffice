using System;
using System.Collections.Generic;
using Tessa.Notices;
using Tessa.Platform.IO;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    public sealed class MailSenderMessage :
        IDisposable
    {
        #region Constructors

        public MailSenderMessage(OutboxMessage message)
        {
            this.Message = message;
            this.Info = !string.IsNullOrEmpty(message.Info)
                ? MailInfo.TryDeserialize(message.Info)
                : new MailInfo();
        }

        #endregion

        #region Properties

        public OutboxMessage Message { get; }

        public MailInfo Info { get; }

        public List<ITempFile> MessageFiles { get; } = new List<ITempFile>();

        public List<string> MissedFiles { get; } = new List<string>();

        public List<string> OversizedFiles { get; } = new List<string>();

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            foreach (ITempFile file in this.MessageFiles)
            {
                file.Dispose();
            }
        }

        #endregion
    }
}