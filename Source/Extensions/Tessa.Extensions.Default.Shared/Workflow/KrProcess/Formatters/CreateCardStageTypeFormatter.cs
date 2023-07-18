using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants.KrCreateCardStageSettingsVirtual;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.CreateCardDescriptor"/>.
    /// </summary>
    public sealed class CreateCardStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc />
        public override ValueTask FormatClientAsync(
            IStageTypeFormatterContext context)
        {
            FormatInternal(context, context.StageRow.Fields, true);

            return new ValueTask();
        }

        /// <inheritdoc />
        public override ValueTask FormatServerAsync(
            IStageTypeFormatterContext context)
        {
            FormatInternal(context, context.Settings, false);

            return new ValueTask();
        }

        #endregion

        #region Private Methods

        private static void FormatInternal(
            IStageTypeFormatterContext context,
            IDictionary<string, object> settings,
            bool isClient)
        {
            var sb = StringBuilderHelper.Acquire();

            AppendString(
                sb,
                settings.TryGet<string>(TypeCaption),
                null,
                true,
                limit: isClient ? DefaultSettingMax : -1);
            AppendString(
                sb,
                settings.TryGet<string>(TemplateCaption),
                null,
                true,
                limit: isClient ? DefaultSettingMax : -1);
            AppendString(
                sb,
                settings.TryGet<string>(ModeName),
                null,
                true,
                limit: isClient ? DefaultSettingMax : -1);

            context.DisplaySettings = sb.ToStringAndRelease();
        }

        #endregion
    }
}