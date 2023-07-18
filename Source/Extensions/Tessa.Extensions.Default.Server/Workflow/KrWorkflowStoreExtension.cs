using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Extensions;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow
{
    /// <summary>
    /// Базовый класс для расширения <see cref="WorkflowStoreExtension"/>,
    /// который учитывает сохранение карточки с правами доступа.
    /// </summary>
    public abstract class KrWorkflowStoreExtension :
        WorkflowStoreExtension
    {
        #region Constructors

        protected KrWorkflowStoreExtension(
            IKrTokenProvider krTokenProvider,
            ICardRepository cardRepositoryToCreateNextRequest,
            ICardRepository cardRepositoryToStoreNextRequest,
            ICardRepository cardRepositoryToCreateTasks,
            ICardTaskHistoryManager taskHistoryManager,
            ICardGetStrategy cardGetStrategy,
            IWorkflowQueueProcessor workflowQueueProcessor = null)
            : base(
                cardRepositoryToCreateNextRequest,
                cardRepositoryToStoreNextRequest,
                cardRepositoryToCreateTasks,
                taskHistoryManager,
                cardGetStrategy,
                workflowQueueProcessor)
        {
            this.KrTokenProvider = krTokenProvider;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Объект, обеспечивающий создание и валидацию токена безопасности для типового решения.
        /// </summary>
        protected IKrTokenProvider KrTokenProvider { get; }

        #endregion

        #region Base Overrides

        protected override async ValueTask<IWorkflowContext> CreateContextAsync(
            ICardStoreExtensionContext context,
            CardStoreRequest nextRequest)
        {
            IWorkflowContext workflowContext = await base.CreateContextAsync(context, nextRequest);

            Card nextCard = nextRequest.Card;
            nextCard.Permissions.SetCardPermissions(CardPermissionFlags.AllowModify);

            // если карточка использует права доступа, рассчитываемые через KrToken,
            // то считаем, что следующее сохранение карточки в NextRequest обладает всеми правами
            // (т.к. выполняется на основании бизнес-процесса, а не по данным, пришедшим с клиента)
            KrToken krToken = this.KrTokenProvider.CreateFullToken(nextCard);
            krToken.Set(nextCard.Info);

            // на крайний случай запрещаем кидать предупреждения при нерассчитанных правах
            // на сохранение по Workflow; тогда права рассчитываются автоматически
            nextRequest.SetIgnorePermissionsWarning();
            return workflowContext;
        }

        #endregion
    }
}
