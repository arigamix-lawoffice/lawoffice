using System.Threading.Tasks;
using Tessa.Platform.Storage;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.SigningDescriptor"/>.
    /// </summary>
    public sealed class SigningStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc />
        public override async ValueTask FormatClientAsync(IStageTypeFormatterContext context)
        {
            await base.FormatClientAsync(context);
            context.DisplaySettings = context.StageRow.TryGet<bool>(KrSigningStageSettingsVirtual.IsParallel)
                ? "$UI_KrApproval_Parallel"
                : "$UI_KrApproval_Sequential";
        }

        /// <inheritdoc />
        public override async ValueTask FormatServerAsync(IStageTypeFormatterContext context)
        {
            await base.FormatServerAsync(context);
            context.DisplaySettings = context.Settings.TryGet<bool>(KrSigningStageSettingsVirtual.IsParallel)
                ? "$UI_KrApproval_Parallel"
                : "$UI_KrApproval_Sequential";
        }

        #endregion
    }
}