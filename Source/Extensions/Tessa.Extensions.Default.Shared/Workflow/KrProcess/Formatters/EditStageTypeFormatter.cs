using System.Threading.Tasks;
using Tessa.Platform.Storage;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.EditDescriptor"/>.
    /// </summary>
    public sealed class EditStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc />
        public override async ValueTask FormatClientAsync(IStageTypeFormatterContext context)
        {
            await base.FormatClientAsync(context);
            context.DisplaySettings = context.StageRow.Fields.Get<bool>(KrEditSettingsVirtual.ChangeState)
                ? "$UI_KrEdit_ChangeState"
                : string.Empty;
        }

        /// <inheritdoc />
        public override async ValueTask FormatServerAsync(IStageTypeFormatterContext context)
        {
            await base.FormatServerAsync(context);
            context.DisplaySettings = context.Settings.TryGet<bool>(KrEditSettingsVirtual.ChangeState)
                ? "$UI_KrEdit_ChangeState"
                : string.Empty;
        }

        #endregion
    }
}