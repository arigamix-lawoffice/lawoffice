using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.AddFromTemplateDescriptor"/>.
    /// </summary>
    public sealed class AddFileFromTemplateStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override ValueTask FormatClientAsync(IStageTypeFormatterContext context)
        {
            context.DisplayParticipants = string.Empty;
            context.DisplayTimeLimit = string.Empty;

            FormatInternal(context, context.StageRow.Fields, true);

            return new ValueTask();
        }

        /// <inheritdoc/>
        public override ValueTask FormatServerAsync(IStageTypeFormatterContext context)
        {
            context.DisplayParticipants = string.Empty;
            context.DisplayTimeLimit = string.Empty;

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
            var builder = StringBuilderHelper.Acquire();

            AppendString(
                builder,
                settings.TryGet<string>(KrConstants.KrAddFromTemplateSettingsVirtual.FileTemplateName),
                null,
                true,
                limit: isClient ? DefaultSettingMax : -1);
            AppendString(
                builder,
                settings.TryGet<string>(KrConstants.KrAddFromTemplateSettingsVirtual.Name),
                null,
                true,
                limit: isClient ? DefaultSettingMax : -1);

            context.DisplaySettings = builder.ToStringAndRelease();
        }

        #endregion
    }
}