using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess.Formatters
{
    /// <summary>
    /// Форматтер типа этапа <see cref="StageTypeDescriptors.ForkManagementDescriptor"/>.
    /// </summary>
    public sealed class ForkManagementStageTypeFormatter : StageTypeFormatterBase
    {
        #region Base Overrides

        /// <inheritdoc />
        public override ValueTask FormatClientAsync(IStageTypeFormatterContext context)
        {
            var rows = context.Card.Sections[KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic].Rows;
            FormatInternal(context, context.StageRow.Fields, rows, true);
            return ValueTask.CompletedTask;
        }

        /// <inheritdoc />
        public override ValueTask FormatServerAsync(IStageTypeFormatterContext context)
        {
            var rows = context.Settings.TryGet<IList>(KrConstants.KrForkSecondaryProcessesSettingsVirtual.Synthetic);
            FormatInternal(context, context.Settings, rows, false);
            return ValueTask.CompletedTask;
        }

        #endregion

        #region Private Methods

        private static void FormatInternal(
            IStageTypeFormatterContext context,
            IDictionary<string, object> settings,
            IList forkSecondaryProcessesSettings,
            bool isClient)
        {
            var sb = StringBuilderHelper.Acquire();

            AppendString(
                sb,
                settings.TryGet<string>(KrConstants.KrForkManagementSettingsVirtual.ModeName),
                null,
                true);

            if (forkSecondaryProcessesSettings?.Count > 0)
            {
                var secondaryProcessNames = ExtractRows(
                    context.StageRow.RowID,
                    forkSecondaryProcessesSettings.Cast<IDictionary<string, object>>());

                foreach (var row in secondaryProcessNames)
                {
                    AppendString(
                        sb,
                        row,
                        null,
                        true,
                        limit: isClient ? DefaultSettingMax : -1);
                }
            }

            context.DisplaySettings = sb.ToStringAndRelease();
        }

        private static IEnumerable<string> ExtractRows(
            Guid stageRowID,
            IEnumerable<IDictionary<string, object>> rows)
        {
            if (rows is null)
            {
                yield break;
            }

            foreach (var row in rows)
            {
                if (row.TryGet<Guid?>(KrConstants.StageRowIDReferenceToOwner) == stageRowID)
                {
                    var state = (CardRowState) row.TryGet<int>(CardRow.SystemStateKey);
                    if (state != CardRowState.Deleted)
                    {
                        yield return row.TryGet<string>(KrConstants.KrForkSecondaryProcessesSettingsVirtual.SecondaryProcessName);
                    }
                }
            }
        }

        #endregion
    }
}