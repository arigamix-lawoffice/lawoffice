using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.TestProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.TestProcess
{
    /// <summary>
    /// Класс, реализующий логику бизнес-процесса TestProcess.
    /// </summary>
    public sealed class TestWorkflowWorker :
        WorkflowTaskWorker<IWorkflowManager>
    {
        #region Constructors

        public TestWorkflowWorker(
            IWorkflowManager manager,
            ICardRepository cardRepositoryToCreateTasks)
            : base(manager, cardRepositoryToCreateTasks)
        {
        }

        #endregion

        #region Private Task Helpers

        /// <summary>
        /// Отправляет задание типа Тип1 с указанием переходов,
        /// выполняемых при завершении задания по каждому из вариантов завершения.
        /// </summary>
        /// <param name="completionTransitionA">Номер перехода, выполняемого при выборе варианта завершения А.</param>
        /// <param name="completionTransitionB">Номер перехода, выполняемого при выборе варианта завершения Б.</param>
        /// <param name="processInfo">Подпроцесс, в котором отправляется задание.</param>
        /// <param name="digest">Краткая информация по заданию, которую увидит пользователь.</param>
        /// <param name="roleID">Идентификатор роли, на которую отправляется задание.</param>
        /// <param name="roleName">Имя роли, на которую отправляется задание.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private Task SendTaskTypeOneAsync(
            int completionTransitionA,
            int completionTransitionB,
            IWorkflowProcessInfo processInfo,
            string digest,
            Guid roleID,
            string roleName,
            CancellationToken cancellationToken = default) =>
            this.SendTaskAsync(
                DefaultTaskTypes.TestTask1TypeID,
                processInfo,
                digest,
                cardTask => cardTask.AddPerformer(roleID, roleName),
                new Dictionary<string, object>(StringComparer.Ordinal)
                {
                    { "A", completionTransitionA },
                    { "B", completionTransitionB },
                },
                cancellationToken: cancellationToken);


        /// <summary>
        /// Отправляет задание типа Тип1 на роль текущего пользователя с указанием переходов,
        /// выполняемых при завершении задания по каждому из вариантов завершения.
        /// </summary>
        /// <param name="completionTransitionA">Номер перехода, выполняемого при выборе варианта завершения А.</param>
        /// <param name="completionTransitionB">Номер перехода, выполняемого при выборе варианта завершения Б.</param>
        /// <param name="processInfo">Подпроцесс, в котором отправляется задание.</param>
        /// <param name="digest">Краткая информация по заданию, которую увидит пользователь.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private Task SendTaskTypeOneAsync(
            int completionTransitionA,
            int completionTransitionB,
            IWorkflowProcessInfo processInfo,
            string digest,
            CancellationToken cancellationToken = default) =>
            this.SendTaskTypeOneAsync(
                completionTransitionA,
                completionTransitionB,
                processInfo,
                digest,
                this.Manager.Session.User.ID,
                this.Manager.Session.User.Name,
                cancellationToken);


        /// <summary>
        /// Отправляет задание типа Тип2 с указанием перехода, выполняемого при завершении задания.
        /// </summary>
        /// <param name="completionTransition">Номер перехода, выполняемого при завершении задания.</param>
        /// <param name="processInfo">Подпроцесс, в котором отправляется задание.</param>
        /// <param name="digest">Краткая информация по заданию, которую увидит пользователь.</param>
        /// <param name="roleID">Идентификатор роли, на которую отправляется задание.</param>
        /// <param name="roleName">Имя роли, на которую отправляется задание.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private Task SendTaskTypeTwoAsync(
            int completionTransition,
            IWorkflowProcessInfo processInfo,
            string digest,
            Guid roleID,
            string roleName,
            CancellationToken cancellationToken = default) =>
            this.SendTaskAsync(
                DefaultTaskTypes.TestTask2TypeID,
                processInfo,
                digest,
                cardTask => cardTask.AddPerformer(roleID, roleName),
                new Dictionary<string, object>(StringComparer.Ordinal)
                {
                    { "Completion", completionTransition },
                },
                cancellationToken: cancellationToken);


        /// <summary>
        /// Отправляет задание типа Тип2 на роль текущего пользователя с указанием перехода,
        /// выполняемого при завершении задания.
        /// </summary>
        /// <param name="completionTransition">Номер перехода, выполняемого при завершении задания.</param>
        /// <param name="processInfo">Подпроцесс, в котором отправляется задание.</param>
        /// <param name="digest">Краткая информация по заданию, которую увидит пользователь.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private Task SendTaskTypeTwoAsync(
            int completionTransition,
            IWorkflowProcessInfo processInfo,
            string digest,
            CancellationToken cancellationToken = default) =>
            this.SendTaskTypeTwoAsync(
                completionTransition,
                processInfo,
                digest,
                this.Manager.Session.User.ID,
                this.Manager.Session.User.Name,
                cancellationToken);

        #endregion

        #region Base Overrides

        /// <summary>
        /// Выполняет действия при запуске подпроцесса.
        /// </summary>
        /// <param name="processInfo">Информация по запускаемому подпроцессу.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected override async Task StartProcessCoreAsync(
            IWorkflowProcessInfo processInfo,
            CancellationToken cancellationToken = default)
        {
            switch (processInfo.ProcessTypeName)
            {
                case TestProcessHelper.MainSubProcess:
                    await this.Manager.InitCounterAsync(1, processInfo, initialValue: 2, cancellationToken: cancellationToken);
                    await this.RenderStepAsync(1, processInfo, cancellationToken);
                    await this.RenderStepAsync(2, processInfo, cancellationToken);
                    break;

                case TestProcessHelper.SubProcess1:
                    await this.RenderStepAsync(1, processInfo, cancellationToken);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(processInfo.ProcessTypeName), processInfo.ProcessTypeName, null);
            }
        }


        /// <summary>
        /// Выполняет действия при завершении подпроцесса.
        /// </summary>
        /// <param name="processInfo">Информация по завершаемому подпроцессу.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected override async Task StopProcessCoreAsync(
            IWorkflowProcessInfo processInfo,
            CancellationToken cancellationToken = default)
        {
            switch (processInfo.ProcessTypeName)
            {
                case TestProcessHelper.MainSubProcess:
                    // завершился бизнес-процесс, здесь можно перевести карточку в состоянию "Согласовано"
                    break;

                case TestProcessHelper.SubProcess1:
                    await this.StopSubProcessWithCompletionAsync(processInfo, cancellationToken);
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(processInfo.ProcessTypeName), processInfo.ProcessTypeName, null);
            }
        }


        /// <summary>
        /// Выполняет действия при завершении задания.
        /// </summary>
        /// <param name="taskInfo">Информация по завершаемому заданию.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected override async Task CompleteTaskCoreAsync(
            IWorkflowTaskInfo taskInfo,
            CancellationToken cancellationToken = default)
        {
            Guid typeID = taskInfo.Task.TypeID;
            if (typeID == DefaultTaskTypes.TestTask1TypeID)
            {
                Guid? optionID = taskInfo.Task.OptionID;
                if (optionID == DefaultCompletionOptions.OptionA)
                {
                    // Тип1, Вариант А
                    int transitionNumber = taskInfo.TaskParameters.Get<int>("A");
                    await this.RenderStepAsync(transitionNumber, taskInfo, cancellationToken);
                }
                else if (optionID == DefaultCompletionOptions.OptionB)
                {
                    // Тип1, Вариант Б
                    int transitionNumber = taskInfo.TaskParameters.Get<int>("B");
                    await this.RenderStepAsync(transitionNumber, taskInfo, cancellationToken);
                }
            }
            else if (typeID == DefaultTaskTypes.TestTask2TypeID)
            {
                // Тип2
                int transitionNumber = taskInfo.TaskParameters.Get<int>("Completion");
                await this.RenderStepAsync(transitionNumber, taskInfo, cancellationToken);
            }
        }


        /// <summary>
        /// Обрабатывает внешний сигнал. Возвращает признак того, что сигнал известен и был обработан.
        /// </summary>
        /// <param name="signalInfo">Информация по сигналу и подпроцессу, для которого выполняется сигнал.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Признак того, что сигнал известен и был обработан.</returns>
        protected override async Task<bool> ProcessSignalCoreAsync(
            IWorkflowSignalInfo signalInfo,
            CancellationToken cancellationToken = default)
        {
            if (signalInfo.Signal.Type != WorkflowSignalTypes.Default)
            {
                return await base.ProcessSignalCoreAsync(signalInfo, cancellationToken);
            }

            switch (signalInfo.ProcessTypeName)
            {
                case TestProcessHelper.MainSubProcess:
                    switch (signalInfo.Signal.Name)
                    {
                        case TestProcessHelper.TestSignal:
                            await this.RenderStepCoreAsync(7, signalInfo, cancellationToken);
                            return true;
                    }
                    break;
            }

            return false;
        }


        /// <summary>
        /// Выполняет переход.
        /// </summary>
        /// <param name="transitionNumber">Номер перехода.</param>
        /// <param name="processInfo">Информация по подпроцессу, в котором выполняется переход.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected override async Task RenderStepCoreAsync(
            int transitionNumber,
            IWorkflowProcessInfo processInfo,
            CancellationToken cancellationToken = default)
        {
            switch (processInfo.ProcessTypeName)
            {
                case TestProcessHelper.MainSubProcess:
                    switch (transitionNumber)
                    {
                        case 1:
                            await this.SendTaskTypeOneAsync(3, 4, processInfo, "Первое задание в основном процессе", cancellationToken);
                            break;

                        case 2:
                            await this.StartSubProcessWithCompletionAsync(
                                TestProcessHelper.SubProcess1, 4, processInfo, cancellationToken: cancellationToken);
                            break;

                        case 3:
                            await this.SendTaskTypeTwoAsync(4, processInfo, "Второе необязательное задание в основном процессе", cancellationToken);
                            break;

                        case 4:
                            if (await this.Manager.DecrementCounterAsync(
                                1, processInfo, cancellationToken: cancellationToken) == WorkflowCounterState.Finished)
                            {
                                await this.RenderStepAsync(5, processInfo, cancellationToken);
                            }
                            break;

                        case 5:
                            await this.StartSubProcessWithCompletionAsync(
                                TestProcessHelper.SubProcess1, 6, processInfo, cancellationToken: cancellationToken);
                            break;

                        case 6:
                            await this.StopProcessAsync(processInfo, cancellationToken);
                            break;

                        case 7:
                            this.Manager.ValidationResult.AddInfo(this, "Тестовый сигнал обработан");
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(transitionNumber), transitionNumber, null);
                    }
                    break;

                case TestProcessHelper.SubProcess1:
                    switch (transitionNumber)
                    {
                        case 1:
                            await this.Manager.InitCounterAsync(1, processInfo, 3, cancellationToken);
                            for (int i = 0; i < 3; i++)
                            {
                                await this.SendTaskTypeOneAsync(2, 3, processInfo, "Одно из трёх заданий в подпроцессе", cancellationToken);
                            }
                            break;

                        case 2:
                            await this.RenderStepAsync(4, processInfo, cancellationToken);
                            break;

                        case 3:
                            await this.RenderStepAsync(4, processInfo, cancellationToken);
                            break;

                        case 4:
                            if (await this.Manager.DecrementCounterAsync(
                                1, processInfo, cancellationToken: cancellationToken) == WorkflowCounterState.Finished)
                            {
                                await this.RenderStepAsync(5, processInfo, cancellationToken);
                            }
                            break;

                        case 5:
                            await this.StopProcessAsync(processInfo, cancellationToken);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(transitionNumber), transitionNumber, null);
                    }
                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(processInfo.ProcessTypeName), processInfo.ProcessTypeName, null);
            }
        }

        #endregion
    }
}
