using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Cards.Numbers;
using Tessa.Extensions.Default.Server.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrProcess;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Workflow;
using Tessa.Workflow.Compilation;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    public sealed class KrDeregistrationAction : KrWorkflowActionBase
    {
        #region Fields

        private readonly INumberDirectorContainer numberDirectorContainer;
        private readonly IKrStageSerializer krStageSerializer;

        #endregion

        #region Constructors

        public KrDeregistrationAction(
            ICardRepository cardRepository,
            IWorkflowEngineCardRequestExtender requestExtender,
            INumberDirectorContainer numberDirectorContainer,
            IBusinessCalendarService calendarService,
            IKrStageSerializer krStageSerializer,
            IKrDocumentStateManager krDocumentStateManager)
            : base(
                  KrDescriptors.DeregistrationDescriptor,
                  cardRepository,
                  requestExtender,
                  calendarService,
                  krDocumentStateManager)
        {
            this.numberDirectorContainer = numberDirectorContainer;
            this.krStageSerializer = krStageSerializer;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(IWorkflowEngineContext context, IWorkflowEngineCompiled scriptObject)
        {
            await base.ExecuteAsync(context, scriptObject);

            var mainCard = await context.GetMainCardAsync(context.CancellationToken);
            if (mainCard == null)
            {
                return;
            }

            var cardType = (await context.CardMetadata.GetCardTypesAsync(context.CancellationToken))[mainCard.TypeID];

            // выделение номера при регистрации
            var numberProvider = this.numberDirectorContainer.GetProvider(cardType.ID);
            var numberDirector = numberProvider.GetDirector();
            var numberComposer = numberProvider.GetComposer();
            var numberContext = await numberDirector.CreateContextAsync(
                numberComposer,
                mainCard,
                cardType,
                transactionMode: NumberTransactionMode.SeparateTransaction,
                cancellationToken: context.CancellationToken);

            await numberDirector.NotifyOnDeregisteringCardAsync(numberContext, context.CancellationToken);
            context.ValidationResult.Add(numberContext.ValidationResult);

            if (context.ValidationResult.IsSuccessful())
            {
                await this.AddTaskHistoryAsync(
                    context,
                    KrConstants.KrDeregistrationTypeID,
                    KrConstants.KrDeregistrationTypeName,
                    "$CardTypes_TypesNames_KrDeregistration",
                    DefaultCompletionOptions.DeregisterDocument,
                    "$ApprovalHistory_DocumentDeregistered");

                var sCard = await context.GetKrSatelliteAsync();

                if (sCard is null)
                {
                    return;
                }

                var krProcessInfo = ProcessInfoCacheHelper.Get(this.krStageSerializer, sCard);
                var prevStateID = krProcessInfo.TryGetValue(KrConstants.Keys.StateBeforeRegistration, out var stateIDObj)
                    ? (int?) stateIDObj ?? KrState.Draft.ID
                    : this.TryGetPreviousState(context);

                await this.SetStateIDAsync(
                    context,
                    (KrState) prevStateID,
                    cancellationToken: context.CancellationToken);
            }
        }

        #endregion
    }
}
