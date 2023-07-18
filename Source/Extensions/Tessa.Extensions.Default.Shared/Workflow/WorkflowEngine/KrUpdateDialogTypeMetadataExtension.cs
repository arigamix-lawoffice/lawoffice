using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Workflow.Actions.Descriptors;

namespace Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine
{
    public sealed class KrUpdateDialogTypeMetadataExtension : CardTypeMetadataExtension
    {
        #region Nested Types

        private class ModifySettings
        {
            public Guid TypeID { get; set; }

            public string BlockAlias { get; set; }

            public string ControlAlias { get; set; }
        }

        private class UpdateTypeControlVisitor : CardTypeVisitor
        {
            #region Fields

            private readonly ModifySettings settings;

            #endregion

            #region Constructors

            public UpdateTypeControlVisitor(ModifySettings settings)
            {
                this.settings = settings;
            }

            #endregion

            #region Base Overrides

            public override ValueTask VisitControlAsync(
                CardTypeControl control,
                CardTypeBlock block, 
                CardTypeForm form, 
                CardType type, 
                CancellationToken cancellationToken = default)
            {
                if (this.settings.ControlAlias == control.Name
                    && this.settings.BlockAlias == block.Name
                    && control is CardTypeEntryControl entryControl
                    && entryControl.Type == CardControlTypes.AutoCompleteEntry)
                {
                    var controlSettings = entryControl.ControlSettings;
                    controlSettings[CardControlSettings.RefSectionSetting] = "KrTypesForDialogs";
                    controlSettings[CardControlSettings.ViewAliasSetting] = "KrTypesForDialogs";
                    controlSettings[CardControlSettings.ParameterAliasSetting] = "NameOrCaption";
                }

                return new ValueTask();
            }

            #endregion
        }

        #endregion

        #region Fields

        private readonly ModifySettings[] modifySettings = new ModifySettings[]
        {
            new ModifySettings
            {
                TypeID = WorkflowActionDescriptors.DialogDescriptor.ID,
                BlockAlias = "MainBlock",
                ControlAlias = "DialogType",
            },
            new ModifySettings
            {
                TypeID = WorkflowActionDescriptors.TaskDescriptor.ID,
                BlockAlias = "MainBlock",
                ControlAlias = "DialogType",
            },
            new ModifySettings
            {
                TypeID = WorkflowActionDescriptors.TaskGroupDescriptor.ID,
                BlockAlias = "MainBlock",
                ControlAlias = "DialogType",
            },
        };

        #endregion

        #region Base Overrides

        public override async Task ModifyTypes(ICardMetadataExtensionContext context)
        {
            foreach(ModifySettings settings in modifySettings)
            {
                var cardType = await TryGetCardTypeAsync(context, settings.TypeID, false);
                if (cardType != null)
                {
                    await cardType.VisitAsync(new UpdateTypeControlVisitor(settings));
                }
            }
        }

        #endregion
    }
}
