using System;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.GlobalSignals
{
    public sealed class SignalFilterItem
    {
        public SignalFilterItem(
            string signalType,
            Type handlerType)
        {
            this.SignalType = signalType;
            this.HandlerType = handlerType;
        }

        public string SignalType { get; }
        public Type HandlerType { get; }
    }
}