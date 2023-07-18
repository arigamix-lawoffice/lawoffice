using System;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    public interface IStageTypeFormatterContainer
    {
        IStageTypeFormatterContainer RegisterFormatter<T>(
            StageTypeDescriptor descriptor) where T : IStageTypeFormatter;

        IStageTypeFormatterContainer RegisterFormatter(
            StageTypeDescriptor descriptor,
            Type handlerType);

        IStageTypeFormatter ResolveFormatter(Guid descriptorID);
    }
}