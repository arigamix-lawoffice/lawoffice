using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Controls;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    public sealed class KrHideCardTypeSettingsUIExtension : CardUIExtension
    {
        public override async Task Initialized(ICardUIExtensionContext context)
        {
            ICardModel model = context.Model;

            // model.MainForm проверяется на null здесь, следовательно во всех вызываемых приватных методах, это проверять не нужно.
            if (model.MainForm is null)
            {
                return;
            }

            if (model.CardType.ID == DefaultCardTypes.KrSettingsTypeID)
            {
                IBlockViewModel cardTypesBlock = model.Blocks.TryGet(KrTypesUIHelper.TypesBlock);

                if (cardTypesBlock != null)
                {
                    if (cardTypesBlock.Controls.FirstOrDefault(x => x.Name == KrTypesUIHelper.TypesControl) is GridViewModel cardTypesControl)
                    {
                        cardTypesControl.RowInvoked += cardTypesControl_RowInvoked;
                    }
                }
            }
            else if (model.CardType.ID == DefaultCardTypes.KrDocTypeTypeID)
            {
                CollapseIfUnchecked(model, KrTypesUIHelper.UseRegistrationBlock,
                    new [] { KrTypesUIHelper.UseRegistrationControl }, KrTypesUIHelper.RegistrationSettingsBlock);

                CollapseIfUnchecked(model, KrTypesUIHelper.UseApprovingBlock,
                    new[] { KrTypesUIHelper.UseApprovingControl }, KrTypesUIHelper.AutoApprovalSettingsBlock1);

                CollapseIfUnchecked(model, KrTypesUIHelper.UseApprovingBlock,
                    new[] { KrTypesUIHelper.UseApprovingControl, KrTypesUIHelper.UseAutoApprovingControl }, KrTypesUIHelper.AutoApprovalSettingsBlock2);

                CollapseIfUnchecked(model, KrTypesUIHelper.AutoApprovalSettingsBlock1,
                    new[] { KrTypesUIHelper.UseApprovingControl, KrTypesUIHelper.UseAutoApprovingControl }, KrTypesUIHelper.AutoApprovalSettingsBlock2);

                model.MainForm.RearrangeSelf();
            }
        }

        private static void cardTypesControl_RowInvoked(object sender, GridRowEventArgs e)
        {
            if (e.Action == GridRowAction.Inserted || e.Action == GridRowAction.Opening)
            {
                if (e.RowModel.Blocks.All(x => x.Key != KrTypesUIHelper.TypeSettingBlock))
                {
                    return;
                }

                CollapseIfUnchecked(e, KrTypesUIHelper.UseRegistrationBlock, KrTypesUIHelper.UseRegistrationControl,
                    new[] { KrTypesUIHelper.UseRegistrationField }, KrTypesUIHelper.RegistrationSettingsBlock);

                CollapseIfUnchecked(e, KrTypesUIHelper.UseApprovingBlock, KrTypesUIHelper.UseApprovingControl,
                    new []{ KrTypesUIHelper.UseApprovingField }, KrTypesUIHelper.AutoApprovalSettingsBlock1);

                CollapseIfUnchecked(e, KrTypesUIHelper.UseApprovingBlock, KrTypesUIHelper.UseApprovingControl,
                    new [] { KrTypesUIHelper.UseApprovingField, KrTypesUIHelper.UseAutoApprovingField }, KrTypesUIHelper.AutoApprovalSettingsBlock2);

                CollapseIfUnchecked(e, KrTypesUIHelper.AutoApprovalSettingsBlock1, KrTypesUIHelper.UseAutoApprovingControl,
                    new[] { KrTypesUIHelper.UseApprovingField, KrTypesUIHelper.UseAutoApprovingField }, KrTypesUIHelper.AutoApprovalSettingsBlock2);


                CollapseIfDocTypesUnused(e);

            }
        }

        private static void CollapseIfDocTypesUnused(GridRowEventArgs e)
        {
            IControlViewModel useControl
                = e.RowModel.Blocks[KrTypesUIHelper.TypeSettingBlock].Controls.FirstOrDefault(x => x.Name == KrTypesUIHelper.UseDocTypesControl);

            if (useControl != null)
            {
                CollapseSettingsIfDocTypesInUse(e);
                e.RowModel.MainFormWithBlocks.RearrangeSelf();


                useControl.PropertyChanged += (sender2, e2) =>
                {
                    if (e2.PropertyName == "IsChecked")
                    {
                        CollapseSettingsIfDocTypesInUse(e);
                    }
                };
            }
        }

        private static void CollapseSettingsIfDocTypesInUse(GridRowEventArgs e)
        {
            foreach (var block in e.RowModel.MainFormWithBlocks.Blocks)
            {
                if (block.Name == KrTypesUIHelper.TypeSettingBlock)
                {
                    continue;
                }

                if (block.Name == KrTypesUIHelper.RegistrationSettingsBlock)
                {
                    bool inUse = e.Row.Fields.Get<bool>(KrTypesUIHelper.UseRegistrationField)
                        && !e.Row.Fields.Get<bool>(KrTypesUIHelper.UseDocTypesField);
                    block.BlockVisibility = inUse
                        ? System.Windows.Visibility.Visible
                        : System.Windows.Visibility.Collapsed;
                    continue;
                }

                if (block.Name == KrTypesUIHelper.AutoApprovalSettingsBlock1)
                {
                    bool inUse = e.Row.Fields.Get<bool>(KrTypesUIHelper.UseApprovingField)
                        && !e.Row.Fields.Get<bool>(KrTypesUIHelper.UseDocTypesField);
                    block.BlockVisibility = inUse
                        ? System.Windows.Visibility.Visible
                        : System.Windows.Visibility.Collapsed;
                    continue;
                }

                if (block.Name == KrTypesUIHelper.AutoApprovalSettingsBlock2)
                {
                    bool inUse = e.Row.Fields.Get<bool>(KrTypesUIHelper.UseApprovingField)
                        && e.Row.Fields.Get<bool>(KrTypesUIHelper.UseAutoApprovingField)
                        && !e.Row.Fields.Get<bool>(KrTypesUIHelper.UseDocTypesField);
                    block.BlockVisibility = inUse
                        ? System.Windows.Visibility.Visible
                        : System.Windows.Visibility.Collapsed;
                    continue;
                }
                if (block.Name == KrTypesUIHelper.UseForumBlock)
                {
                    bool inUse = !e.Row.Fields.Get<bool>(KrTypesUIHelper.UseDocTypesField);
                    block.BlockVisibility = inUse
                        ? System.Windows.Visibility.Visible
                        : System.Windows.Visibility.Collapsed;
                    continue;
                }

                block.BlockVisibility = !e.Row.Fields.Get<bool>(KrTypesUIHelper.UseDocTypesField)
                    ? System.Windows.Visibility.Visible
                    : System.Windows.Visibility.Collapsed;
            }

            e.RowModel.MainFormWithBlocks.RearrangeSelf();
        }

        private static void CollapseIfUnchecked(
            GridRowEventArgs e,
            string useBlockName,
            string useControlName,
            string[] useFieldNames,
            string settingsBlockName)
        {
            if (e.RowModel.Blocks.Any(x => x.Key == useBlockName)
                && e.RowModel.Blocks.Any(x => x.Key == settingsBlockName))
            {
                IControlViewModel useControl
                    = e.RowModel.Blocks[useBlockName].Controls.FirstOrDefault(x => x.Name == useControlName);

                if (useControl == null)
                {
                    return;
                }

                IBlockViewModel settingsBlock
                    = e.RowModel.Blocks[settingsBlockName];

                

                settingsBlock.BlockVisibility = useFieldNames.Select(p => e.Row.Fields.Get<bool>(p)).All(q => q)
                    ? System.Windows.Visibility.Visible
                    : System.Windows.Visibility.Collapsed;
                settingsBlock.RearrangeSelf();

                useControl.PropertyChanged += (sender2, e2) =>
                {
                    if (e2.PropertyName == "IsChecked")
                    {
                        bool inUse = useFieldNames.Select(p => e.Row.Fields.Get<bool>(p)).All(q => q)
                            && !e.Row.Fields.Get<bool>(KrTypesUIHelper.UseDocTypesField);
                        settingsBlock.BlockVisibility = inUse
                            ? System.Windows.Visibility.Visible
                            : System.Windows.Visibility.Collapsed;
                        settingsBlock.RearrangeSelf();
                        settingsBlock.Form.RearrangeSelf();
                    }
                };
            }
            e.RowModel.MainFormWithBlocks.RearrangeSelf();
        }

        private static void CollapseIfUnchecked(
            ICardModel model,
            string useBlockName,
            string[] useControlNames,
            string settingsBlockName)
        {
            IControlViewModel[] useControls =
                useControlNames.Select(p => model.Blocks[useBlockName].Controls.FirstOrDefault(q => q.Name == p)).ToArray();

            if (useControls.All(p => p == null))
            {
                return;
            }

            IBlockViewModel settingsBlock
                = model.Blocks[settingsBlockName];

            settingsBlock.BlockVisibility = useControlNames.Select(p => ((CheckBoxViewModel)model.Controls[p]).IsChecked).All(q => q)
                ? System.Windows.Visibility.Visible
                : System.Windows.Visibility.Collapsed;
            settingsBlock.RearrangeSelf();

            foreach (var useControl in useControls)
            {
                if (useControl == null)
                {
                    continue;
                }

                useControl.PropertyChanged += (sender2, e2) =>
                {
                    if (e2.PropertyName == "IsChecked")
                    {
                        bool inUse = useControlNames.Select(p => ((CheckBoxViewModel)model.Controls[p]).IsChecked).All(q => q);
                        settingsBlock.BlockVisibility = inUse
                            ? System.Windows.Visibility.Visible
                            : System.Windows.Visibility.Collapsed;
                        settingsBlock.RearrangeSelf();
                        settingsBlock.Form.RearrangeSelf();
                    }
                };
            }
            

            model.MainForm.RearrangeSelf();
        }
    }
}
