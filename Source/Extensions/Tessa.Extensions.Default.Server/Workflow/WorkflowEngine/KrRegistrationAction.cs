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
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Workflow;
using Tessa.Workflow.Compilation;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    public sealed class KrRegistrationAction : KrWorkflowActionBase
    {
        #region Fields

        private readonly INumberDirectorContainer numberDirectorContainer;
        private readonly IKrStageSerializer krStageSerializer;

        #endregion

        #region Constructors

        public KrRegistrationAction(
            ICardRepository cardRepository,
            IWorkflowEngineCardRequestExtender requestExtender,
            INumberDirectorContainer numberDirectorContainer,
            IBusinessCalendarService calendarService,
            IKrStageSerializer krStageSerializer,
            IKrDocumentStateManager krDocumentStateManager)
            : base(
                  KrDescriptors.RegistrationDescriptor,
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

            await numberDirector.NotifyOnRegisteringCardAsync(numberContext, context.CancellationToken);
            context.ValidationResult.Add(numberContext.ValidationResult);

            if (!context.ValidationResult.IsSuccessful())
            {
                return;
            }

            await this.AddTaskHistoryByTaskAsync(
                context,
                DefaultTaskTypes.KrRegistrationTypeID,
                DefaultCompletionOptions.RegisterDocument,
                "$ApprovalHistory_DocumentRegistered");

            var sCard = await context.GetKrSatelliteAsync();

            if (sCard is null)
            {
                return;
            }

            var approvalCommonInfoFields = sCard.GetApprovalInfoSection().Fields;
            var currentStateID = approvalCommonInfoFields.TryGet<int>(KrConstants.KrApprovalCommonInfo.StateID);

            var info = ProcessInfoCacheHelper.Get(this.krStageSerializer, sCard);
            info[KrConstants.Keys.StateBeforeRegistration] = Int32Boxes.Box(currentStateID);
            ProcessInfoCacheHelper.Update(this.krStageSerializer, sCard);

            await this.SetStateIDAsync(
                context,
                KrState.Registered,
                cancellationToken: context.CancellationToken);
        }

        #endregion
    }
}
