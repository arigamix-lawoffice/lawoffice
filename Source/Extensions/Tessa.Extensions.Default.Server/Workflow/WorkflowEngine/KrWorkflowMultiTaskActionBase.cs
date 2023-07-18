using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

using Tessa.BusinessCalendar;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Cards;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.Workflow;
using Tessa.Workflow.Actions.Descriptors;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    /// <summary>
    /// Базовый класс для обработчиков действий отправляющих несколько заданий.
    /// </summary>
    public abstract class KrWorkflowMultiTaskActionBase
        : KrWorkflowTaskActionBase
    {
        #region Constants

        /// <summary>
        /// Имя ключа, по которому в параметрах действия содержится список идентификаторов обрабатываемых заданий данным экземпляром действия. Тип значения: <see cref="List{T}"/>, где T - <see cref="Guid"/>.
        /// </summary>
        protected const string ProcessingTaskIDListParamKey = CardHelper.SystemKeyPrefix + "ProcessingTaskIDList";

        #endregion

        #region Constructor

        protected KrWorkflowMultiTaskActionBase(
            WorkflowActionDescriptor descriptor,
            INotificationManager notificationManager,
            IWorkflowEngineCardsScope cardsScope,
            ICardRepository cardRepository,
            ICardRepository cardRepositoryDef,
            ICardServerPermissionsProvider serverPermissionsProvider,
            ISignatureProvider signatureProvider,
            Func<ICardTaskCompletionOptionSettingsBuilder> ctcBuilderFactory,
            ICardFileManager cardFileManager,
            IWorkflowEngineCardRequestExtender requestExtender,
            IBusinessCalendarService calendarService,
            IKrDocumentStateManager krDocumentStateManager)
            : base(descriptor,
                  notificationManager,
                  cardsScope,
                  cardRepository,
                  cardRepositoryDef,
                  serverPermissionsProvider,
                  signatureProvider,
                  ctcBuilderFactory,
                  cardFileManager,
                  requestExtender,
                  calendarService,
                  krDocumentStateManager)
        {}

        #endregion

        #region Base overrides

        /// <summary>
        /// Метод для проверки факта, что действие активно и должно сохранить свое состояние вместе с состоянием своего узла.
        /// </summary>
        /// <param name="context">>Текущий контекст выполнения обработки процесса</param>
        /// <returns>Возвращает true, если действие активно и должно сохранить свое состояние, иначе false</returns>
        /// <remarks>Факт активности действия проверяется путём проверки наличия не завершённых заданий/</remarks>
        protected override bool CheckActive(IWorkflowEngineContext context)
        {
            return GetProcessingTaskIDList(context)?.Cast<object>().Any() == true;
        }

        #endregion

        #region Protected methods

        /// <summary>
        /// Возвращает список идентификаторов заданий обрабатываемых данным экземпляром действия.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <returns>Список идентификаторов заданий обрабатываемых данным экземпляром действия или значение <see langword="null"/>, если текущий экземпляр действий никогда не содержал информации об обрабатываемых заданиях. Тип значения: <see cref="Guid"/>.</returns>
        protected static IList GetProcessingTaskIDList(
            IWorkflowEngineContext context)
        {
            return context.ActionInstance.Hash.TryGet<IList>(ProcessingTaskIDListParamKey);
        }

        /// <summary>
        /// Добавляет указанный идентификатор задания в список обрабатываемых заданий данным экземпляром действия.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="taskRowID">Идентификатор задания.</param>
        protected static void AddNewProcessingTaskID(
            IWorkflowEngineContext context,
            Guid taskRowID)
        {
            var taskIDList = GetProcessingTaskIDList(context);
            if (taskIDList is null)
            {
                taskIDList = new List<object>();
                context.ActionInstance.Hash[ProcessingTaskIDListParamKey] = taskIDList;
            }
            taskIDList.Add(taskRowID);
        }

        #endregion
    }
}
