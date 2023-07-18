#nullable enable

using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Client.UI.KrProcess.StageHandlers
{
    /// <summary>
    /// UI обработчик типа этапа <see cref="StageTypeDescriptors.CreateCardDescriptor"/>.
    /// </summary>
    public sealed class CreateCardUIHandler :
        StageTypeUIHandlerBase
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override Task Validate(IKrStageTypeUIHandlerContext context)
        {
            var templateID = context.Row.TryGet<Guid?>(KrConstants.KrCreateCardStageSettingsVirtual.TemplateID);
            var typeID = context.Row.TryGet<Guid?>(KrConstants.KrCreateCardStageSettingsVirtual.TypeID);

            if (!templateID.HasValue && !typeID.HasValue)
            {
                context.ValidationResult.AddError(this, "$KrStages_CreateCard_TemplateAndTypeNotSpecified");
            }
            else if (templateID.HasValue && typeID.HasValue)
            {
                context.ValidationResult.AddError(this, "$KrStages_CreateCard_TemplateAndTypeSelected");
            }

            var modeID = context.Row.TryGet<int?>(KrConstants.KrCreateCardStageSettingsVirtual.ModeID);

            if (!modeID.HasValue)
            {
                context.ValidationResult.AddError(this, "$KrStages_CreateCard_ModeRequired");
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override Task Initialize(IKrStageTypeUIHandlerContext context)
        {
            context.Row.FieldChanged += OnSettingsFieldChanged;

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override Task Finalize(IKrStageTypeUIHandlerContext context)
        {
            context.Row.FieldChanged -= OnSettingsFieldChanged;

            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        private static void OnSettingsFieldChanged(object? sender, CardFieldChangedEventArgs e)
        {
            var row = (CardRow) sender!;

            if (e.FieldName == KrConstants.KrCreateCardStageSettingsVirtual.TypeID)
            {
                if (e.FieldValue is not null)
                {
                    row.Fields[KrConstants.KrCreateCardStageSettingsVirtual.TemplateID] = null;
                    row.Fields[KrConstants.KrCreateCardStageSettingsVirtual.TemplateCaption] = null;
                }
            }
            else if (e.FieldName == KrConstants.KrCreateCardStageSettingsVirtual.TemplateID)
            {
                if (e.FieldValue is not null)
                {
                    row.Fields[KrConstants.KrCreateCardStageSettingsVirtual.TypeID] = null;
                    row.Fields[KrConstants.KrCreateCardStageSettingsVirtual.TypeCaption] = null;
                }
            }
        }

        #endregion
    }
}
