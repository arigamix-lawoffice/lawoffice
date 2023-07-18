using System.Threading.Tasks;
using Tessa.Platform.Storage;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.AcquaintanceDescriptor"/>.
    /// </summary>
    public sealed class AcquaintanceStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override async ValueTask FormatClientAsync(IStageTypeFormatterContext context)
        {
            await base.FormatClientAsync(context);
            context.DisplayTimeLimit = string.Empty;
            context.DisplaySettings = context.StageRow.Fields.Get<bool>(KrAcquaintanceSettingsVirtual.ExcludeDeputies)
                ? "$UI_KrAcquaintance_ExcludeDeputies"
                : string.Empty;
        }

        /// <inheritdoc/>
        public override async ValueTask FormatServerAsync(IStageTypeFormatterContext context)
        {
            await base.FormatServerAsync(context);
            context.DisplayTimeLimit = string.Empty;
            context.DisplaySettings = context.Settings.TryGet<bool>(KrAcquaintanceSettingsVirtual.ExcludeDeputies)
                ? "$UI_KrAcquaintance_ExcludeDeputies"
                : string.Empty;
        }

        #endregion
    }
}