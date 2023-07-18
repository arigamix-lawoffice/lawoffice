#nullable enable

using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.UI;
using Tessa.UI.Cards.Controls;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <summary>
    /// UI обработчик типа этапа <see cref="StageTypeDescriptors.UniversalTaskDescriptor"/>.
    /// </summary>
    public class UniversalTaskStageTypeUIHandler :
        StageTypeUIHandlerBase
    {
        #region Base Overrides

        /// <inheritdoc />
        public override Task Initialize(
            IKrStageTypeUIHandlerContext context)
        {
            if (context.SettingsForms.FirstOrDefault(static i => i.Name == DefaultCardTypes.KrUniversalTaskStageTypeSettingsTypeName)
                ?.Blocks.FirstOrDefault(static i => i.Name == "MainInfo")
                ?.Controls.FirstOrDefault(static i => i.Name == "CompletionOptions") is GridViewModel grid)
            {
                grid.RowInvoked += RowInvoked;
                grid.RowEditorClosing += RowClosing;
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public override Task Finalize(
            IKrStageTypeUIHandlerContext context)
        {
            if (context.SettingsForms.FirstOrDefault(static i => i.Name == DefaultCardTypes.KrUniversalTaskStageTypeSettingsTypeName)
                ?.Blocks.FirstOrDefault(static i => i.Name == "MainInfo")
                ?.Controls.FirstOrDefault(static i => i.Name == "CompletionOptions") is GridViewModel grid)
            {
                grid.RowInvoked -= RowInvoked;
                grid.RowEditorClosing -= RowClosing;
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        private static void RowInvoked(object? sender, GridRowEventArgs e)
        {
            if (e.Action == GridRowAction.Inserted)
            {
                e.Row.Fields[KrUniversalTaskOptionsSettingsVirtual.OptionID] = Guid.NewGuid();
            }
        }

        private static void RowClosing(
            object? sender,
            GridRowEventArgs e)
        {
            var row = e.Row;
            IValidationResultBuilder? validationResult = null;

            var optionID = row.TryGet<Guid?>(KrUniversalTaskOptionsSettingsVirtual.OptionID);

            if (optionID.HasValue)
            {
                var rows = e.CardModel.Card.Sections[KrUniversalTaskOptionsSettingsVirtual.Synthetic].Rows;

                if (CheckDuplicatesOptionID(rows, row.RowID, optionID.Value))
                {
                    validationResult ??= new ValidationResultBuilder();
                    validationResult.AddError(nameof(UniversalTaskStageTypeUIHandler), "$KrProcess_UniversalTask_CompletionOptionIDNotUnique");
                    e.Cancel = true;
                }
            }
            else
            {
                validationResult ??= new ValidationResultBuilder();
                validationResult.AddError(nameof(UniversalTaskStageTypeUIHandler), "$KrProcess_UniversalTask_CompletionOptionIDEmpty");
                e.Cancel = true;
            }

            if (string.IsNullOrEmpty(row.TryGet<string>(KrUniversalTaskOptionsSettingsVirtual.Caption)))
            {
                validationResult ??= new ValidationResultBuilder();
                validationResult.AddError(nameof(UniversalTaskStageTypeUIHandler), "$KrProcess_UniversalTask_CompletionOptionCaptionEmpty");
                e.Cancel = true;
            }

            if (validationResult is not null)
            {
                TessaDialog.ShowNotEmpty(validationResult);
            }
        }

        private static bool CheckDuplicatesOptionID(
            ListStorage<CardRow> rows,
            Guid rowID,
            Guid optionID)
        {
            foreach (var row in rows)
            {
                if (row.RowID == rowID
                    || row.State == CardRowState.Deleted)
                {
                    continue;
                }

                var iOptionID = row.TryGet<Guid?>(KrUniversalTaskOptionsSettingsVirtual.OptionID);

                if (!iOptionID.HasValue)
                {
                    continue;
                }

                if (optionID == iOptionID)
                {
                    return true;
                }
            }

            return false;
        }

        #endregion
    }
}
