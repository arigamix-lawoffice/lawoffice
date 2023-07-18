#nullable enable

using System;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <summary>
    /// UI обработчик типа этапа <see cref="StageTypeDescriptors.TypedTaskDescriptor"/>.
    /// </summary>
    public sealed class TypedTaskUIHandler :
        StageTypeUIHandlerBase
    {
        #region Base Overrides

        /// <inheritdoc />
        public override Task Validate(IKrStageTypeUIHandlerContext context)
        {
            if (!context.Row.TryGet<Guid?>(KrTypedTaskSettingsVirtual.TaskTypeID).HasValue)
            {
                context.ValidationResult.AddError(this, "$KrStages_TypedTask_TaskType");
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
