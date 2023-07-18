using System;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Events
{
    public sealed class KrEventPolicy: IKrEventPolicy
    {
        private readonly string[] eventTypes;

        public KrEventPolicy(
            string[] eventTypes)
        {
            this.eventTypes = eventTypes ?? throw new ArgumentNullException(nameof(eventTypes));

            for (int i = 0; i < this.eventTypes.Length; i++)
            {
                if (string.IsNullOrWhiteSpace(this.eventTypes[i]))
                {
                    throw new ArgumentNullException($"EventType[{i}] is null or whitespace.");
                }
            }
        }

        /// <inheritdoc />
        public bool IsAllowed(
            string eventType)
        {
            if (this.eventTypes.Length == 0)
            {
                return true;
            }

            for (int i = 0; i < this.eventTypes.Length; i++)
            {
                if (string.Equals(this.eventTypes[i], eventType, StringComparison.Ordinal))
                {
                    return true;
                }
            }

            return false;
        }
    }
}