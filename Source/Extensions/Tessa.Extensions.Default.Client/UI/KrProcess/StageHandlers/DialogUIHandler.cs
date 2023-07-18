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
    /// UI обработчик типа этапа <see cref="StageTypeDescriptors.DialogDescriptor"/>.
    /// </summary>
    public class DialogUIHandler :
        StageTypeUIHandlerBase
    {
        #region Fields

        private BlockContentIndicator? indicator;

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override Task Validate(IKrStageTypeUIHandlerContext context)
        {
            if (!context.Row.TryGet<int?>(KrDialogStageTypeSettingsVirtual.CardStoreModeID).HasValue)
            {
                context.ValidationResult.AddError(this, "$KrStages_Dialog_CardStoreModeNotSpecified");
            }

            if (!context.Row.TryGet<int?>(KrDialogStageTypeSettingsVirtual.OpenModeID).HasValue)
            {
                context.ValidationResult.AddError(this, "$KrStages_Dialog_CardOpenModeNotSpecified");
            }

            if (!context.Row.TryGet<Guid?>(KrDialogStageTypeSettingsVirtual.DialogTypeID).HasValue
                && !context.Row.TryGet<Guid?>(KrDialogStageTypeSettingsVirtual.TemplateID).HasValue)
            {
                context.ValidationResult.AddError(this, "$KrStages_Dialog_TemplateAndTypeNotSpecified");
            }

            if (context.Row.TryGet<Guid?>(KrDialogStageTypeSettingsVirtual.DialogTypeID).HasValue
                && context.Row.TryGet<Guid?>(KrDialogStageTypeSettingsVirtual.TemplateID).HasValue)
            {
                context.ValidationResult.AddError(this, "$KrStages_Dialog_TemplateAndTypeSelected");
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc />
        public override async Task Initialize(IKrStageTypeUIHandlerContext context)
        {
            context.Row.FieldChanged += OnSettingsFieldChanged;

            if (context.SettingsForms.FirstOrDefault(static i => i.Name == DefaultCardTypes.KrDialogStageTypeSettingsTypeName) is not { } form)
            {
                return;
            }

            if (form.Blocks.FirstOrDefault(static i => i.Name == "MainInfo") is { } mainInfoBlock
                && mainInfoBlock.Controls.FirstOrDefault(static i => i.Name == "ButtonSettings") is GridViewModel grid)
            {
                grid.RowEditorClosing += this.ButtonSettings_RowClosing;
            }

            if (form.Blocks.FirstOrDefault(static i => i.Name == Ui.KrDialogScriptsBlock) is { } krDialogScriptsBlock)
            {
                var sectionMeta = (await context.CardModel.CardMetadata.GetSectionsAsync(context.CancellationToken))[KrStages.Virtual];

                var fieldIDs = sectionMeta.Columns.ToDictionary(
                    static k => k.ID,
                    static v => v.Name);

                this.indicator = new BlockContentIndicator(
                    krDialogScriptsBlock,
                    context.Row,
                    fieldIDs);
            }
        }

        /// <inheritdoc />
        public override Task Finalize(IKrStageTypeUIHandlerContext context)
        {
            context.Row.FieldChanged -= OnSettingsFieldChanged;

            if (context.SettingsForms.FirstOrDefault(static i => i.Name == DefaultCardTypes.KrDialogStageTypeSettingsTypeName) is { } form
                && form.Blocks.FirstOrDefault(static i => i.Name == "MainInfo") is { } mainInfoBlock
                && mainInfoBlock.Controls.FirstOrDefault(static i => i.Name == "ButtonSettings") is GridViewModel grid)
            {
                grid.RowEditorClosing -= this.ButtonSettings_RowClosing;
            }

            if (this.indicator is not null)
            {
                this.indicator.Dispose();
                this.indicator = null;
            }

            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Проверяет корректность настройки кнопок.
        /// </summary>
        /// <param name="sender">Источник события.</param>
        /// <param name="e">Информация о событии.</param>
        private void ButtonSettings_RowClosing(object? sender, GridRowEventArgs e)
        {
            var row = e.Row;
            IValidationResultBuilder? validationResult = null;

            if (!row.TryGet<int?>(KrDialogButtonSettingsVirtual.TypeID).HasValue)
            {
                validationResult ??= new ValidationResultBuilder();
                validationResult.AddError(this, "$KrStages_Dialog_ButtonTypeIDNotSpecified");
                e.Cancel = true;
            }

            if (string.IsNullOrEmpty(row.TryGet<string>(KrDialogButtonSettingsVirtual.Caption)))
            {
                validationResult ??= new ValidationResultBuilder();
                validationResult.AddError(this, "$KrStages_Dialog_ButtonCaptionNotSpecified");
                e.Cancel = true;
            }

            if (string.IsNullOrEmpty(row.TryGet<string>(KrDialogButtonSettingsVirtual.Name)))
            {
                validationResult ??= new ValidationResultBuilder();
                validationResult.AddError(this, "$KrStages_Dialog_ButtonAliasNotSpecified");
                e.Cancel = true;
            }

            if (validationResult is not null)
            {
                TessaDialog.ShowNotEmpty(validationResult);
            }
        }

        private static void OnSettingsFieldChanged(object? sender, CardFieldChangedEventArgs e)
        {
            var settings = (CardRow) sender!;

            if (e.FieldName == KrDialogStageTypeSettingsVirtual.DialogTypeID)
            {
                if (e.FieldValue is not null)
                {
                    settings.Fields[KrDialogStageTypeSettingsVirtual.TemplateID] = null;
                    settings.Fields[KrDialogStageTypeSettingsVirtual.TemplateCaption] = null;
                }
            }
            else if (e.FieldName == KrDialogStageTypeSettingsVirtual.TemplateID)
            {
                if (e.FieldValue is not null)
                {
                    settings.Fields[KrDialogStageTypeSettingsVirtual.DialogTypeID] = null;
                    settings.Fields[KrDialogStageTypeSettingsVirtual.DialogTypeName] = null;
                    settings.Fields[KrDialogStageTypeSettingsVirtual.DialogTypeCaption] = null;
                }
            }
        }

        #endregion
    }
}
