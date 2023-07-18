using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.ChangesStateDescriptor"/>.
    /// </summary>
    public sealed class ChangeStateStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override ValueTask FormatClientAsync(IStageTypeFormatterContext context)
        {
            FormatInternal(context, context.StageRow.Fields);

            return new ValueTask();
        }

        /// <inheritdoc/>
        public override ValueTask FormatServerAsync(
            IStageTypeFormatterContext context)
        {
            FormatInternal(context, context.Settings);

            return new ValueTask();
        }

        #endregion

        #region Private Methods

        private static void FormatInternal(
            IStageTypeFormatterContext context,
            IDictionary<string, object> settings)
        {
            var sb = StringBuilderHelper.Acquire();

            AppendString(
                sb,
                settings.TryGet<string>(KrConstants.KrChangeStateSettingsVirtual.StateName),
                "$UI_KrChangeState_State",
                true);

            context.DisplaySettings = sb.ToStringAndRelease();
        }

        #endregion
    }
}
