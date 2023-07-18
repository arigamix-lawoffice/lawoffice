using System;
using System.Globalization;
using System.Threading;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Operations;
using Tessa.Platform.Validation;
using Tessa.Test.Default.Shared.Kr;
using Tessa.Workflow;
using Tessa.Workflow.Signals;

namespace Tessa.Test.Default.Shared.Workflow
{
    /// <summary>
    /// Предоставляет методы расширения для <see cref="ICardLifecycleCompanion"/> используемые в тестах WorkflowEngine.
    /// </summary>
    public static class CardLifecycleCompanionExtensions
    {
        #region ICardLifecycleCompanion Extensions

        /// <summary>
        /// Заполняет основные данные в шаблоне процесса.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку шаблона бизнес-процесса.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        public static T FillMainData<T>(this T clc) where T : ICardLifecycleCompanion
        {
            var templateCard = clc.GetCardOrThrow();
            var mainSection = templateCard.Sections["BusinessProcessInfo"];

            mainSection.Fields["Name"] = "TestProcess_" + DateTime.Now.Ticks.ToString(CultureInfo.InvariantCulture);
            mainSection.Fields["Group"] = "TestGroup";

            return clc;
        }

        /// <summary>
        /// Добавляет новую версию шаблона.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку шаблона бизнес-процесса.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        public static T AddNewVersion<T>(this T clc) where T : ICardLifecycleCompanion
        {
            var templateCard = clc.GetCardOrThrow();
            var versionsSection = templateCard.Sections.GetOrAddTable("BusinessProcessVersions", CardTableType.Hierarchy);

            var newVersionRow = versionsSection.Rows.Add();
            newVersionRow.RowID = Guid.NewGuid();
            newVersionRow.ParentRowID = null;
            newVersionRow.State = CardRowState.Inserted;

            return clc;
        }

        #endregion

        #region CardLifecycleCompanion Extensions

        /// <summary>
        /// Создаёт и запускает бизнес процесс в карточке которой управляет объект <paramref name="clc"/>.
        /// </summary>
        /// <param name="clc">Объект, управляющий жизненным циклов карточки в которой должен быть запущен бизнес-процесс.</param>
        /// <param name="cardTransactionStrategy">Стратегия обеспечения блокировок reader/writer при выполнении операций с карточкой в которой запущен бизнес-процесс.</param>
        /// <param name="workflowEngineProcessor">Объект-обработчик процессов WorkflowEngine на сервере.</param>
        /// <param name="workflowService">Сервис для управления шаблонами, экземплярами и подписками бизнес-процесса.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="operationRepository">Репозиторий, управляющий операциями.</param>
        /// <param name="workflowEngineCache">Объект для получения шаблонов процессов с кешированием их.</param>
        /// <param name="processTemplateID">Идентификатор шаблона процесса.</param>
        /// <param name="signal">Сигнал, запускающий процесс.</param>
        /// <param name="processID">Идентификатор бизнес-процесса или значение <see langword="null"/>, если он должен быть создан случайным.</param>
        /// <returns>Объект, управляющий жизненным циклом карточки, в которой запущен экземпляр бизнес-процесса.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="WeProcessInstanceLifecycleCompanion.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Последний запрос на обработку сигнала в <see cref="IWorkflowEngineProcessor"/> и ответ на него можно получить в <see cref="WeProcessInstanceLifecycleCompanion.LastData"/> в свойствах <see cref="ICardLifecycleCompanionData.OtherRequests"/> и <see cref="ICardLifecycleCompanionData.OtherResponses"/> по ключам <see cref="WorkflowTestHelper.WorkflowEngineProcessRequestKey"/> и <see cref="WorkflowTestHelper.WorkflowEngineProcessResultKey"/>, соответственно.
        /// </remarks>
        public static WeProcessInstanceLifecycleCompanion CreateWorkflowEngineProcess(
            this CardLifecycleCompanion clc,
            ICardTransactionStrategy cardTransactionStrategy,
            IWorkflowEngineProcessor workflowEngineProcessor,
            IWorkflowService workflowService,
            IDbScope dbScope,
            IOperationRepository operationRepository,
            IWorkflowEngineCache workflowEngineCache,
            Guid processTemplateID,
            WorkflowEngineSignal signal,
            Guid? processID = default)
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(cardTransactionStrategy, nameof(cardTransactionStrategy));
            Check.ArgumentNotNull(workflowEngineProcessor, nameof(workflowEngineProcessor));
            Check.ArgumentNotNull(workflowService, nameof(workflowService));
            Check.ArgumentNotNull(dbScope, nameof(dbScope));
            Check.ArgumentNotNull(operationRepository, nameof(operationRepository));
            Check.ArgumentNotNull(workflowEngineCache, nameof(workflowEngineCache));
            Check.ArgumentNotNull(signal, nameof(signal));

            processID ??= Guid.NewGuid();
            WeProcessInstanceLifecycleCompanion processInstanceCompanion = default;

            clc.AddPendingAction(
                new PendingAction(
                    nameof(CardLifecycleCompanionExtensions) + "." + nameof(CreateWorkflowEngineProcess),
                    async (action, ct) =>
                    {
                        var validationResult = new ValidationResultBuilder();

                        await cardTransactionStrategy.ExecuteInTransactionAsync(
                            validationResult,
                            async (p) =>
                            {
                                var card = clc.GetCardOrThrow();
                                var processRequest = new WorkflowEngineProcessRequest
                                {
                                    ProcessFlag = WorkflowEngineProcessFlags.DefaultNew,
                                    StoreCard = card,
                                    Signal = signal,
                                    ProcessTemplateID = processTemplateID,
                                    ProcessInstanceID = processID,
                                };

                                clc.LastData.OtherRequests[WorkflowTestHelper.WorkflowEngineProcessRequestKey] = processRequest;
                                clc.LastData.OtherResponses[WorkflowTestHelper.WorkflowEngineProcessResultKey] = default;

                                var processResult = await workflowEngineProcessor.ProcessSignalAsync(processRequest, p.CancellationToken);
                                clc.LastData.OtherResponses[WorkflowTestHelper.WorkflowEngineProcessResultKey] = processResult;
                                p.ValidationResult.Add(processResult.ValidationResult);

                                if (!p.ValidationResult.IsSuccessful())
                                {
                                    p.ReportError = true;
                                }
                            },
                            cancellationToken: ct);

                        // Результат запуска процесса будет проверен в тестах.
                        return validationResult.Build();
                    }));

            processInstanceCompanion = new WeProcessInstanceLifecycleCompanion(
                clc,
                processID.Value,
                workflowService,
                workflowEngineProcessor,
                cardTransactionStrategy,
                dbScope,
                operationRepository,
                workflowEngineCache)
                .Load();

            return processInstanceCompanion;
        }

        #endregion
    }
}
