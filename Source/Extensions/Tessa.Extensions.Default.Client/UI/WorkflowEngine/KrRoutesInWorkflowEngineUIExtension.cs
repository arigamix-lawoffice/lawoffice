using System.Threading.Tasks;
using System.Windows;

using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI.Cards;

using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Client.UI.WorkflowEngine
{
    /// <summary>
    /// Настраивает отображение карточки в которой используются маршруты в Workflow Engine.
    /// </summary>
    public sealed class KrRoutesInWorkflowEngineUIExtension
        : CardUIExtension
    {
        #region Fields

        private readonly IKrTypesCache typesCache;

        #endregion

        #region Constructor

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrRoutesInWorkflowEngineUIExtension"/>.
        /// </summary>
        /// <param name="typesCache">Кэш по типам карточек и документов, содержащих информацию по типовому решению.</param>
        public KrRoutesInWorkflowEngineUIExtension(
            IKrTypesCache typesCache)
        {
            this.typesCache = typesCache;
        }

        #endregion

        #region Base overrides

        /// <inheritdoc/>
        public override async Task Initialized(ICardUIExtensionContext context)
        {
            var model = context.Model;
            var card = model.Card;
            var cardTypeID = card.TypeID;

            if (model.Flags.Has(CardModelFlags.EditTemplate)
                || KrProcessSharedHelper.DesignTimeCard(cardTypeID)
                || (await KrComponentsHelper.GetKrComponentsAsync(
                        card,
                        this.typesCache,
                        context.CancellationToken)).HasNot(KrComponents.Routes)
                || !(await KrProcessSharedHelper.TryGetKrTypeAsync(
                        this.typesCache,
                        card,
                        cardTypeID,
                        validationResult: context.ValidationResult,
                        validationObject: this,
                        cancellationToken: context.CancellationToken)).UseRoutesInWorkflowEngine
                || !model.Forms.TryGet(Ui.KrApprovalProcessFormAlias, out var routesForm)
                || !model.Blocks.TryGet(Ui.KrApprovalStagesBlockAlias, out var stageBlock))
            {
                return;
            }

            if (stageBlock.BlockVisibility != Visibility.Collapsed)
            {
                stageBlock.BlockVisibility = Visibility.Collapsed;
                routesForm.RearrangeSelf();
            }
        }

        #endregion
    }
}
