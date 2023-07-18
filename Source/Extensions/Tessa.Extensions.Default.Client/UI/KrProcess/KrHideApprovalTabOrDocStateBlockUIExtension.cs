using System.Threading.Tasks;
using System.Windows;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Forms;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    public sealed class KrHideApprovalTabOrDocStateBlockUIExtension : CardUIExtension
    {
        private readonly IKrTypesCache typesCache;

        public KrHideApprovalTabOrDocStateBlockUIExtension(IKrTypesCache typesCache)
        {
            this.typesCache = typesCache;
        }

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            // В карточке шаблона этапов и вторичного процесса ничего не трогать
            if (KrProcessSharedHelper.DesignTimeCard(context.Card.TypeID))
            {
                return;
            }

            ICardModel model = context.Model;

            if (model.MainFormWithTabs is not { } mainForm)
            {
                return;
            }

            KrComponents usedComponents = await KrComponentsHelper.GetKrComponentsAsync(model.Card, this.typesCache, context.CancellationToken);
            var krType = await KrProcessSharedHelper.TryGetKrTypeAsync(
                this.typesCache, model.Card, model.Card.TypeID, cancellationToken: context.CancellationToken);

            // удаляем вкладку согласования
            if (usedComponents.HasNot(KrComponents.Routes) || krType != null && krType.HideRouteTab)
            {
                if (model.Forms.TryGet(KrConstants.Ui.KrApprovalProcessFormAlias, out var approvalProcessTab))
                {
                    // если вкладка была выбрана при том, что мы её удаляем, то мы, наверное, переходим из карточки-сателлита в основную карточку
                    // с восстановлением состояния основной карточки; это означает, что нам надо перевыбрать правильную вкладку по индексу
                    int selectedIndexToRestore = ReferenceEquals(mainForm.SelectedTab, approvalProcessTab)
                        ? model.Forms.IndexOf(approvalProcessTab)
                        : -1;

                    model.Forms.Remove(approvalProcessTab);

                    if (selectedIndexToRestore >= 0 && selectedIndexToRestore < model.Forms.Count)
                    {
                        mainForm.SelectedTab = model.Forms[selectedIndexToRestore];
                    }
                }
            }

            // скрываем специальный блок с состоянием документа, если нет согласования и регистрации
            if (usedComponents.HasNot(KrComponents.Routes)
                && usedComponents.HasNot(KrComponents.Registration))
            {
                CollapseSpecialBlock(model);
            }
        }

        private static void CollapseSpecialBlock(ICardModel model)
        {
            foreach (IFormWithBlocksViewModel tab in model.Forms)
            {
                foreach (IBlockViewModel block in tab.Blocks)
                {
                    if (block.Name == KrConstants.Ui.KrBlockForDocStatusAlias)
                    {
                        block.BlockVisibility = Visibility.Collapsed;
                        block.RearrangeSelf();
                        break;
                    }
                }
            }
        }
    }
}
