using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI.Cards;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    /// <summary>
    /// При редактировании карточки в шаблоне скрываем все блоки, кроме <see cref="Ui.KrDisclaimerBlockAlias"/>,
    /// который наоборот отображаем.
    /// </summary>
    public sealed class KrTemplateUIExtension :
        CardUIExtension
    {
        #region Constructors

        public KrTemplateUIExtension(IKrTypesCache typesCache)
        {
            this.typesCache = typesCache;
        }

        #endregion

        #region Fields

        private readonly IKrTypesCache typesCache;

        #endregion

        #region Private Methods

        private async ValueTask<bool> CardIsAvailableForExtensionAsync(ICardModel model, CancellationToken cancellationToken = default)
        {
            if (KrProcessSharedHelper.DesignTimeCard(model.CardType.ID))
            {
                return true;
            }

            KrComponents usedComponents = await KrComponentsHelper.GetKrComponentsAsync(model.Card, this.typesCache, cancellationToken);

            return usedComponents.Has(KrComponents.Routes);
        }

        #endregion

        #region Base Overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            if (!context.Model.Flags.Has(CardModelFlags.EditTemplate)
                || !await this.CardIsAvailableForExtensionAsync(context.Model, context.CancellationToken)
                || !context.Model.Forms.TryGet(Ui.KrApprovalProcessFormAlias, out IFormWithBlocksViewModel routesForm))
            {
                return;
            }

            foreach (IBlockViewModel block in routesForm.Blocks)
            {
                block.BlockVisibility = block.Name == Ui.KrDisclaimerBlockAlias
                    ? Visibility.Visible
                    : Visibility.Collapsed;
            }

            routesForm.RearrangeSelf();
        }

        #endregion
    }
}
