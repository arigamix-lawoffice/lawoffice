using System.Linq;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Localization;
using Tessa.Platform.Validation;
using Tessa.Workflow;
using Tessa.Workflow.Compilation;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    /// <summary>
    /// Обработчик действия <see cref="KrDescriptors.KrChangeStateDescriptor"/>.
    /// </summary>
    public sealed class KrChangeStateAction : KrWorkflowActionBase
    {
        #region Consts

        public const string MainActionSection = "KrChangeStateAction";

        #endregion

        #region Fields

        private readonly IKrTypesCache typesCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrChangeStateAction"/>.
        /// </summary>
        public KrChangeStateAction(
            ICardRepository cardRepository,
            IKrTypesCache typesCache,
            IWorkflowEngineCardRequestExtender requestExtender,
            IBusinessCalendarService calendarService,
            IKrDocumentStateManager krDocumentStateManager)
            : base(
                  KrDescriptors.KrChangeStateDescriptor,
                  cardRepository,
                  requestExtender,
                  calendarService,
                  krDocumentStateManager)
        {
            this.typesCache = typesCache;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject)
        {
            await base.ExecuteAsync(context, scriptObject);

            var typeID =
                context.StoreCard?.ID == context.ProcessInstance.CardID
                ? context.StoreCard.TypeID
                : (await context.GetMainCardAsync(context.CancellationToken))?.TypeID;

            if (!typeID.HasValue)
            {
                return;
            }

            var cardType = (await this.typesCache.GetCardTypesAsync(context.CancellationToken))
                .FirstOrDefault(x => x.ID == typeID);

            if (cardType is null)
            {
                var typeCaption = (await context.CardMetadata.GetCardTypesAsync(context.CancellationToken))[typeID.Value].Caption;
                context.ValidationResult.AddError(
                    this,
                    "$KrActions_ChangeState_TypeNotAllowed",
                    LocalizationManager.Localize(typeCaption));
                return;
            }

            var stateID = await context.GetAsync<int?>(MainActionSection, "State", "ID");
            var stateName = await context.GetAsync<string>(MainActionSection, "State", "Name");
            if (stateID.HasValue)
            {
                await this.SetStateIDAsync(
                    context,
                    (KrState) stateID.Value,
                    cancellationToken: context.CancellationToken);
            }
        }

        #endregion
    }
}
