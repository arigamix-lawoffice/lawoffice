using System;

namespace Tessa.Extensions.Default.Chronos.Notices
{
    public class OutboxMessage
    {
        public Guid ID { get; set; }

        public string Email { get; set; }

        public string Subject { get; set; }

        public string Body { get; set; }

        public int Attempts { get; set; }

        public string Info { get; set; }
    }
}