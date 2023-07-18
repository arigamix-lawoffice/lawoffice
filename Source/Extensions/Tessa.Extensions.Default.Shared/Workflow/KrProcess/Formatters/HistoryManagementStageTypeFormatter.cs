using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.HistoryManagementDescriptor"/>.
    /// </summary>
    public class HistoryManagementStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override ValueTask FormatClientAsync(IStageTypeFormatterContext context)
        {
            FormatInternal(context, context.StageRow.Fields, true);
            return ValueTask.CompletedTask;
        }

        /// <inheritdoc/>
        public override ValueTask FormatServerAsync(
            IStageTypeFormatterContext context)
        {
            FormatInternal(context, context.Settings, false);
            return ValueTask.CompletedTask;
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
                settings.TryGet<string>(KrConstants.KrHistoryManagementStageSettingsVirtual.TaskHistoryGroupTypeCaption),
                null,
                true,
                limit: isClient ? DefaultSettingMax : -1);
            AppendString(
                sb,
                settings.TryGet<string>(KrConstants.KrHistoryManagementStageSettingsVirtual.ParentTaskHistoryGroupTypeCaption),
                null,
                true,
                limit: isClient ? DefaultSettingMax : -1);

            var newIteration = settings.TryGet<bool?>(KrConstants.KrHistoryManagementStageSettingsVirtual.NewIteration);

            if (newIteration == true)
            {
                if (sb.Length > 0)
                {
                    sb.AppendLine();
                }

                sb.Append("{$UI_KrHistoryManagement_NewIteration}");
            }

            context.DisplaySettings = sb.ToStringAndRelease();
        }

        #endregion
    }
}