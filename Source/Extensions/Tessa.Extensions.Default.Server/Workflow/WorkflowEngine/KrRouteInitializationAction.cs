using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Platform.Storage;
using Tessa.Scheme;
using Tessa.Workflow;
using Tessa.Workflow.Compilation;
using Tessa.Workflow.Signals;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;
using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    /// <summary>
    /// Обработчик действия <see cref="KrDescriptors.KrRouteInitializationDescriptor"/>.
    /// </summary>
    public sealed class KrRouteInitializationAction
        : KrWorkflowActionBase
    {
        #region Constants

        private const string mainSectionName = KrRouteInitializationActionVirtual.SectionName;

        #endregion

        #region Constructor

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrRouteInitializationAction"/>.
        /// </summary>
        public KrRouteInitializationAction(
            ICardRepository cardRepository,
            IWorkflowEngineCardRequestExtender requestExtender,
            IBusinessCalendarService calendarService,
            IKrDocumentStateManager krDocumentStateManager)
            : base(
                  KrDescriptors.KrRouteInitializationDescriptor,
                  cardRepository,
                  requestExtender,
                  calendarService,
                  krDocumentStateManager)
        {
        }

        #endregion

        #region Base overrides

        /// <inheritdoc/>
        protected override async Task ExecuteAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject)
        {
            await base.ExecuteAsync(context, scriptObject);

            if (context.Signal.Type != WorkflowSignalTypes.Default)
            {
                return;
            }

            var sCard = await context.GetKrSatelliteAsync();

            if (sCard is null)
            {
                return;
            }

            var aciFields = sCard.GetApprovalInfoSection().Fields;
            await TryInitializeInitiatorProcessAsync(context, aciFields);

            if (string.IsNullOrWhiteSpace(aciFields.TryGet<string>(KrApprovalCommonInfo.AuthorComment)))
            {
                aciFields[KrApprovalCommonInfo.AuthorComment] = await context.GetAsync<string>(
                    mainSectionName,
                    KrRouteInitializationActionVirtual.InitiatorComment);
            }
        }

        #endregion

        #region Private methods

        /// <summary>
        /// Инициализирует значение полей содержащих информацию о инициаторе процесса согласования.<para/>
        /// Если инициализируемое поле заполнено, то его значение не изменяется.<para/>
        /// Значения берутся из параметров действия, если в них оно отсутствует, то используется значение из текущей сессии.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="aciFields">Словарь содержащий инициализируемые поля секции <see cref="KrApprovalCommonInfo"/>.</param>
        /// <returns>Асинхронная задача.</returns>
        private static async ValueTask TryInitializeInitiatorProcessAsync(
            IWorkflowEngineContext context,
            IDictionary<string, object> aciFields)
        {
            if (aciFields.TryGet<object>(KrApprovalCommonInfo.AuthorID) is null)
            {
                var initiatorID = await context.GetAsync<Guid?>(
                    mainSectionName,
                    KrRouteInitializationActionVirtual.Initiator,
                    Names.Table_ID);
                string initiatorName;

                if (initiatorID.HasValue)
                {
                    initiatorName =
                        await context.GetAsync<string>(
                            mainSectionName,
                            KrRouteInitializationActionVirtual.Initiator,
                            Table_Field_Name);
                }
                else
                {
                    initiatorID = context.Session.User.ID;
                    initiatorName = context.Session.User.Name;
                }

                aciFields[KrApprovalCommonInfo.AuthorID] = initiatorID;
                aciFields[KrApprovalCommonInfo.AuthorName] = initiatorName;
            }
        }

        #endregion
    }
}
