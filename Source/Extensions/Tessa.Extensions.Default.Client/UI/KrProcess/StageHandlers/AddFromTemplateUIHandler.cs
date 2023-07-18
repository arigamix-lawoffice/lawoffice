#nullable enable

using System;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <summary>
    /// UI обработчик типа этапа <see cref="StageTypeDescriptors.AddFromTemplateDescriptor"/>.
    /// </summary>
    public class AddFromTemplateUIHandler :
        StageTypeUIHandlerBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override Task Validate(IKrStageTypeUIHandlerContext context)
        {
            var template = context.Row.TryGet<Guid?>(KrConstants.KrAddFromTemplateSettingsVirtual.FileTemplateID);
            if (!template.HasValue)
            {
                context.ValidationResult.AddWarning(this, "$KrStages_AddFromTemplate_TemplateIsRequiredWarning");
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
