using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Platform;
using Tessa.Platform.Storage;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants.KrProcessManagementStageSettingsVirtual;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.ProcessManagementDescriptor"/>.
    /// </summary>
    public sealed class ProcessManagementStageTypeFormatter : StageTypeFormatterBase
    {
        #region Constants

        private const int StageMode = 0;
        private const int GroupMode = 1;
        private const int SignalMode = 5;

        #endregion

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
            var sb = StringBuilderHelper.Acquire(256);

            AppendString(
                sb,
                settings.TryGet<string>(ModeName),
                null,
                true);

            var modeID = settings.TryGet<int?>(ModeID);
            switch (modeID)
            {
                case StageMode:
                    var stageName = settings.TryGet<string>(StageName)?.Trim();

                    if (!string.IsNullOrEmpty(stageName))
                    {
                        AppendString(
                            sb,
                            stageName,
                            null,
                            true,
                            limit: isClient ? DefaultSettingMax : -1);

                        var groupRowName = settings.TryGet<string>(StageRowGroupName)?.Trim();

                        if (!string.IsNullOrEmpty(groupRowName))
                        {
                            sb.Append(" (");
                            AppendString(
                                sb,
                                groupRowName,
                                null,
                                true,
                                limit: isClient ? DefaultSettingMax : -1,
                                appendNewLine: false);
                            sb.Append(')');
                        }
                    }

                    break;

                case GroupMode:
                    AppendString(
                        sb,
                        settings.TryGet<string>(StageGroupName),
                        null,
                        true,
                        limit: isClient ? DefaultSettingMax : -1);

                    break;

                case SignalMode:
                    AppendString(
                        sb,
                        settings.TryGet<string>(Signal),
                        null,
                        false,
                        limit: isClient ? DefaultSettingMax : -1);

                    break;
            }

            if (settings.TryGet<bool?>(ManagePrimaryProcess) == true)
            {
                if (sb.Length > 0)
                {
                    sb.AppendLine();
                }

                sb.Append("{$CardTypes_Controls_ManagePrimaryProcess}");
            }

            context.DisplaySettings = sb.ToStringAndRelease();
        }

        #endregion
    }
}