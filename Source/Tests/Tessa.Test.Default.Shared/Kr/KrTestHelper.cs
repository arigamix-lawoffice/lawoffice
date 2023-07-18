using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Tessa.Cards;
using Tessa.Cards.Extensions.Templates;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;
using Tessa.Files;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет вспомогательные методы для тестирования процессов.
    /// </summary>
    public static class KrTestHelper
    {
        #region Public Methods

        /// <summary>
        /// Запускает глобальный вторичный процесс аналогично запуску через тайл.
        /// </summary>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="krProcessInstance">Информация о запускаемом процессе.</param>
        /// <param name="raiseErrorWhenExecutionIsForbidden">Значение <see langword="true"/>, если должна быть создана ошибка при невозможности запуска процесса из-за нарушения ограничений (Сообщение при невозможности выполнения процесса), иначе - <see langword="false"/>.</param>
        /// <param name="info">Дополнительная пользовательская информация которая должна быть передана в запросе на запуск процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат запуска процесса.</returns>
        public static async Task<KrProcessLaunchResult> LaunchGlobalKrProcessAsync(
            ICardRepository cardRepository,
            KrProcessInstance krProcessInstance,
            bool raiseErrorWhenExecutionIsForbidden = default,
            IDictionary<string, object> info = default,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardRepository, nameof(cardRepository));
            Check.ArgumentNotNull(krProcessInstance, nameof(krProcessInstance));

            var req = new CardRequest
            {
                RequestType = KrConstants.LaunchProcessRequestType,
            };

            req.SetKrProcessInstance(krProcessInstance);

            if (raiseErrorWhenExecutionIsForbidden)
            {
                req.Info[KrConstants.RaiseErrorWhenExecutionIsForbidden] = BooleanBoxes.True;
            }

            if (info is not null)
            {
                StorageHelper.Merge(info, req.Info);
            }

            var resp = await cardRepository.RequestAsync(req, cancellationToken);
            return resp.GetKrProcessLaunchFullResult();
        }

        /// <summary>
        /// Имитирует цикл: Завершение этапа/действия с отрицательным вариантом завершения, например, задание этапа/действия "Согласование" с вариантом завершения <see cref="DefaultCompletionOptions.Disapprove"/> -&gt; Доработка -&gt; Завершение этапа/действия с положительным вариантом завершения.
        /// </summary>
        /// <typeparam name="TClc">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, управляющий жизненным циклом карточки, в которой запущен процесс.</param>
        /// <param name="actionAsync">Асинхронное действие содержащее завершение этапа/действия, отрицательный вариант завершения которого приводит к переходу к "Доработке".</param>
        /// <param name="beforeNegativeAction">Действие выполняющееся перед завершением задания типа "Доработка".</param>
        /// <param name="amengingTaskTypeID">Идентификатор типа задания доработки. Если значение не задано, то используется <see cref="DefaultTaskTypes.KrEditTypeID"/>.</param>
        /// <param name="amendingTaskComplectionOptionID">Идентификатор варианта завершения задания доработки по которому процесс переходит к следующей итерации согласования. Если значение не задано, то используется <see cref="DefaultCompletionOptions.NewApprovalCycle"/>.</param>
        /// <param name="negativeCompleteOptionRepeatCount">Число повторений отрицательного варианта завершения. Значение по умолчанию: 1.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static ValueTask EditingCycleAsync<TClc>(
            TClc clc,
            Func<EditingCycleContext<TClc>, ValueTask> actionAsync,
            Action<TClc> beforeNegativeAction = default,
            Guid? amengingTaskTypeID = default,
            Guid? amendingTaskComplectionOptionID = default,
            int negativeCompleteOptionRepeatCount = 1,
            CancellationToken cancellationToken = default)
            where TClc : ICardLifecycleCompanion<TClc>
        {
            return EditingCycleAsync(
                clc,
                actionAsync,
                async (clc, cancellationToken) =>
                {
                    beforeNegativeAction?.Invoke(clc);

                    await clc
                        .GetTaskOrThrow(amengingTaskTypeID ?? DefaultTaskTypes.KrEditTypeID, out var task)
                        .CompleteTask(
                            task,
                            amendingTaskComplectionOptionID ?? DefaultCompletionOptions.NewApprovalCycle)
                        .GoAsync(cancellationToken: cancellationToken);
                },
                negativeCompleteOptionRepeatCount,
                cancellationToken);
        }

        /// <summary>
        /// Имитирует цикл: Завершение этапа/действия с отрицательным вариантом завершения, например, задание этапа "Согласование" с вариантом завершения <see cref="DefaultCompletionOptions.Disapprove"/> -&gt; Обработка отрицательного завершения, например, завершение этапа/действия "Доработка" -&gt; Завершение этапа/действия с положительным вариантом завершения.
        /// </summary>
        /// <typeparam name="TClc">Тип объекта управляющего жизненным циклом карточки в которой запущен процесс.</typeparam>
        /// <param name="clc">Объект, управляющий жизненным циклом карточки, в которой запущен процесс.</param>
        /// <param name="actionAsync">Асинхронное действие содержащее завершение этапа/действия, отрицательный вариант завершения которого приводит к переходу к "Доработке".</param>
        /// <param name="negativeAction">Асинхронное действие выполняющееся после завершения <paramref name="actionAsync"/> отрицательным вариантом завершения.</param>
        /// <param name="negativeCompleteOptionRepeatCount">Число повторений отрицательного варианта завершения. Значение по умолчанию: 1.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async ValueTask EditingCycleAsync<TClc>(
            TClc clc,
            Func<EditingCycleContext<TClc>, ValueTask> actionAsync,
            Func<TClc, CancellationToken, ValueTask> negativeAction,
            int negativeCompleteOptionRepeatCount = 1,
            CancellationToken cancellationToken = default)
            where TClc : ICardLifecycleCompanion<TClc>
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(actionAsync, nameof(actionAsync));
            Check.ArgumentNotNull(negativeAction, nameof(negativeAction));

            var currentNegativeCORepeatCount = 0;
            bool isNegativeCompletionOption;

            do
            {
                isNegativeCompletionOption = currentNegativeCORepeatCount < negativeCompleteOptionRepeatCount;

                if (!isNegativeCompletionOption)
                {
                    currentNegativeCORepeatCount = 0;
                }

                await actionAsync(
                    new EditingCycleContext<TClc>(
                        clc,
                        !isNegativeCompletionOption,
                        currentNegativeCORepeatCount,
                        cancellationToken));

                if (isNegativeCompletionOption)
                {
                    await negativeAction(clc, cancellationToken);

                    currentNegativeCORepeatCount++;
                }
            } while (isNegativeCompletionOption);
        }

        /// <summary>
        /// Инициализирует объект, управляющий жизненным циклом карточки, диалога.
        /// </summary>
        /// <param name="completionOptionSettings">Параметры этапа.</param>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        /// <returns>Объект, управляющий жизненным циклом карточки, диалога.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static CardLifecycleCompanion InitializeDialogCard(
            CardTaskCompletionOptionSettings completionOptionSettings,
            ICardLifecycleCompanionDependencies deps)
        {
            Check.ArgumentNotNull(completionOptionSettings, nameof(completionOptionSettings));
            Check.ArgumentNotNull(deps, nameof(deps));

            return completionOptionSettings.StoreMode switch
            {
                CardTaskDialogStoreMode.Info or CardTaskDialogStoreMode.Settings => completionOptionSettings.DialogCard is null
                    ? CreateDialogCard(completionOptionSettings, deps)
                    : new CardLifecycleCompanion(
                        completionOptionSettings.DialogCard,
                        deps),
                CardTaskDialogStoreMode.Card => completionOptionSettings.PersistentDialogCardID == Guid.Empty
                    ? CreateDialogCard(completionOptionSettings, deps)
                    : new CardLifecycleCompanion(
                        completionOptionSettings.PersistentDialogCardID,
                        null,
                        null,
                        deps)
                    .Load(),
                _ => throw new ArgumentOutOfRangeException(
                    $"{nameof(completionOptionSettings)}.{nameof(completionOptionSettings.StoreMode)}",
                    completionOptionSettings.StoreMode,
                    null),
            };
        }

        /// <summary>
        /// Планирует создание карточки диалога.
        /// </summary>
        /// <param name="completionOptionSettings">Параметры этапа.</param>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточкой.</param>
        /// <returns>Объект, управляющий жизненным циклом карточки, диалога.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Действия аналогичны выполняемым обработчиком клиентской команды <see cref="DefaultCommandTypes.ShowAdvancedDialog"/> с учётом выполнения на сервере.
        /// </remarks>
        public static CardLifecycleCompanion CreateDialogCard(
            CardTaskCompletionOptionSettings completionOptionSettings,
            ICardLifecycleCompanionDependencies deps)
        {
            Check.ArgumentNotNull(completionOptionSettings, nameof(completionOptionSettings));
            Check.ArgumentNotNull(deps, nameof(deps));

            var info = completionOptionSettings.Info;

            if (completionOptionSettings.PreparedNewCard != null
                && completionOptionSettings.PreparedNewCardSignature != null)
            {
                info[CardHelper.NewCardBilletKey] = completionOptionSettings.PreparedNewCard;
                info[CardHelper.NewCardBilletSignatureKey] = completionOptionSettings.PreparedNewCardSignature;
            }

            info[CardTaskDialogHelper.StoreMode] = Int32Boxes.Box((int) completionOptionSettings.StoreMode);

            CardLifecycleCompanion clc;
            var dialogTypeID = completionOptionSettings.DialogTypeID;

            switch (completionOptionSettings.CardNewMethod)
            {
                case CardTaskDialogNewMethod.Default:
                    clc = new CardLifecycleCompanion(
                            dialogTypeID,
                            default,
                            deps)
                        .Create()
                        .WithInfo(info);
                    break;
                case CardTaskDialogNewMethod.Template:
                    clc = new CardLifecycleCompanion(
                        Guid.Empty,
                        CardHelper.TemplateTypeID,
                        CardHelper.TemplateTypeName,
                        deps)
                    .Create()
                    .WithInfo(info);

                    clc.GetLastPendingAction().Info.SetTemplateCardID(dialogTypeID);
                    break;
                default:
                    throw new ArgumentException(
                        nameof(completionOptionSettings.CardNewMethod) + $" = ({completionOptionSettings.CardNewMethod}) is not supported.",
                        nameof(completionOptionSettings));
            }

            return clc;
        }

        /// <summary>
        /// Создаёт объект содержащий информацию о завершении диалога.
        /// </summary>
        /// <param name="dialogFileContainer">Контейнер содержащий информацию о карточке диалога и её файлах.</param>
        /// <param name="coSettings">Параметры диалога."</param>
        /// <param name="buttonAlias">Алиас кнопки - варианта завершения диалога.</param>
        /// <param name="mainCardFileContainer">Контейнер содержащий информацию об основной карточке и её файлах. Параметр имеет значение по умолчанию, если карточка не известна, например, при использовании диалога в глобальном процессе.</param>
        /// <param name="taskRowID">Идентификатор задания диалога. Параметр имеет значение по умолчанию, если диалог не связан с заданием, например, при использовании диалога в глобальном процессе.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Кортеж: &lt;Информация о завершении диалога; Результат выполнения операции&gt;.</returns>
        /// <remarks>Выполняемые действия соответствуют методу <see cref="M:Tessa.Extensions.Default.Client.Workflow.KrProcess.CommandInterpreter.AdvancedDialogCommandHandler.CompleteDialogAsync"/>.</remarks>
        public static async ValueTask<(CardTaskDialogActionResult, ValidationResult)> CreateRequestInfoForCompleteDialogAsync(
            ICardFileContainer dialogFileContainer,
            CardTaskCompletionOptionSettings coSettings,
            string buttonAlias,
            ISession session,
            ICardFileContainer mainCardFileContainer = default,
            Guid taskRowID = default,
            CancellationToken cancellationToken = default)
        {
            ThrowIfNull(dialogFileContainer);
            ThrowIfNull(coSettings);

            var dialogCard = dialogFileContainer.Card;
            var files = dialogFileContainer.FileContainer.Files;
            var validationResult = new ValidationResultBuilder
            {
                await files.EnsureAllContentModifiedAsync(cancellationToken)
            };

            if (!validationResult.IsSuccessful())
            {
                return (null, validationResult.Build());
            }

            switch (coSettings.StoreMode)
            {
                case CardTaskDialogStoreMode.Info:
                    await CardTaskDialogHelper.SetFileContentToInfoAsync(
                        dialogCard,
                        files,
                        session,
                        validationResult,
                        cancellationToken);
                    break;
                case CardTaskDialogStoreMode.Settings:
                    CheckTaskRowID(taskRowID);

                    ThrowIfNull(mainCardFileContainer);
                    SetFileContentToMainCard(
                        dialogFileContainer,
                        mainCardFileContainer,
                        taskRowID,
                        session.User);
                    break;
                case CardTaskDialogStoreMode.Card:
                    CheckTaskRowID(taskRowID);
                    ThrowIfNull(mainCardFileContainer);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(
                        nameof(coSettings) + "." + nameof(coSettings.StoreMode),
                        coSettings.StoreMode,
                        "Dialog store mode is unknown.");
            }

            var completeDialog = buttonAlias is not null && coSettings.Buttons.Single(i => i.Name == buttonAlias).CompleteDialog;

            var actionResult = new CardTaskDialogActionResult
            {
                TaskID = taskRowID,
                PressedButtonName = buttonAlias,
                StoreMode = coSettings.StoreMode,
                KeepFiles = coSettings.KeepFiles,
                CompleteDialog = completeDialog,
            };

            if (mainCardFileContainer is not null)
            {
                actionResult.MainCardID = mainCardFileContainer.Card.ID;
            }

            actionResult.SetDialogCard(dialogCard);

            return (actionResult, validationResult.Build());

            static void CheckTaskRowID(Guid taskRowID)
            {
                if (taskRowID == Guid.Empty)
                {
                    throw new ArgumentException("The dialog task RowID is empty.", nameof(taskRowID));
                }
            }
        }

        /// <summary>
        /// Завершает диалог указанным вариантом завершения.
        /// </summary>
        /// <typeparam name="T">Тип объекта управляющего жизненным циклом основной карточки.</typeparam>
        /// <param name="mainClc">Объект, управляющий жизненным циклом основной карточки.</param>
        /// <param name="dialogCardFileContainer">Контейнер, содержащий информацию по карточке диалога и её файлам.</param>
        /// <param name="buttonAlias">Алиас кнопки - варианта завершения диалога.</param>
        /// <param name="coSettings">Параметры диалога.</param>
        /// <param name="dialogTask">Завершаемое задание.</param>
        /// <param name="session">Сессия пользователя.</param>
        /// <param name="completionOptionID">Идентификатор варианта завершения задания диалога.<para/>
        /// Стандартные варианты завершения:
        /// <list type="table">
        ///     <listheader>
        ///         <description>Подсистема</description>
        ///         <description>Объект</description>
        ///         <description>Значение</description>
        ///     </listheader>
        ///     <item>
        ///         <description>Маршруты документов</description>
        ///         <description>Этап "Диалог"</description>
        ///         <description><see cref="DefaultCompletionOptions.ShowDialog"/></description>
        ///     </item>
        ///     <item>
        ///         <description>Редактор бизнес-процессов (Workflow Engine)</description>
        ///         <description>Действие "Диалог"</description>
        ///         <description><see cref="CardTaskDialogHelper.ShowDialogOption"/></description>
        ///     </item>
        ///     <item>
        ///         <description>Редактор бизнес-процессов (Workflow Engine)</description>
        ///         <description>Действие "Задание"</description>
        ///         <description>Идентификатор варианта завершения для которого настроено открытие диалога.</description>
        ///     </item>
        /// </list>
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Результат выполнения.</returns>
        public static async Task<ValidationResult> CompleteDialogAsync<T>(
            T mainClc,
            ICardFileContainer dialogCardFileContainer,
            string buttonAlias,
            CardTaskCompletionOptionSettings coSettings,
            CardTask dialogTask,
            ISession session,
            Guid completionOptionID,
            CancellationToken cancellationToken = default)
            where T : ICardLifecycleCompanion<T>
        {
            ThrowIfNull(mainClc);
            ThrowIfNull(dialogCardFileContainer);
            ThrowIfNull(coSettings);
            ThrowIfNull(dialogTask);
            ThrowIfNull(session);

            var dialogCard = dialogCardFileContainer.Card;
            var dialogTaskRowID = dialogTask.RowID;

            (var actionResult, var result) = await KrTestHelper.CreateRequestInfoForCompleteDialogAsync(
                dialogCardFileContainer,
                coSettings,
                buttonAlias,
                session,
                await mainClc.GetCardFileContainerAsync(cancellationToken: cancellationToken),
                dialogTaskRowID,
                cancellationToken);

            if (result.HasErrors)
            {
                return result;
            }

            var validationResults = new ValidationResultBuilder
            {
                result,
            };

            var hasFilesToSave = CardHelper.HasFilesContentsToSave(dialogCard);

            dialogCard.RemoveChanges(CardRemoveChangesDeletedHandling.Remove);
            actionResult.SetDialogCard(dialogCard);
            CardTaskDialogHelper.SetCardTaskDialogActionResult(dialogTask, actionResult);

            if (coSettings.StoreMode == CardTaskDialogStoreMode.Settings
                && hasFilesToSave)
            {
                dialogTask.OptionID = completionOptionID;

                await mainClc
                    .ActionTask(
                        dialogTask,
                        CardTaskAction.None)
                    .GoAsync((result) => validationResults.Add(result), cancellationToken);

                if (!validationResults.IsSuccessful())
                {
                    return validationResults.Build();
                }

                // При успешном сохранении подчищаем список файлов и секций для повторного сохранения.
                var mainCard = mainClc.Card;

                foreach (var section in mainCard.Sections)
                {
                    section.Value.RemoveChanges(CardRemoveChangesDeletedHandling.Remove);
                }

                foreach (var file in mainCard.Files.ToArray())
                {
                    if (file.State == CardFileState.Deleted)
                    {
                        mainCard.Files.Remove(file);
                    }
                    else
                    {
                        file.RemoveChanges(CardRemoveChangesDeletedHandling.Remove);
                    }
                }

                mainClc.GetTaskOrThrow(i => i.RowID == dialogTaskRowID, $"Not found task RowID = {dialogTaskRowID:B}.", out dialogTask);
                CardTaskDialogHelper.SetCardTaskDialogActionResult(dialogTask, actionResult);
            }

            await mainClc
                .CompleteTask(
                    dialogTask,
                    completionOptionID)
                .GoAsync((result) => validationResults.Add(result), cancellationToken);

            return validationResults.Build();
        }

        /// <summary>
        /// Запускает глобальный вторичный процесс при выполнении которого открывается диалог.
        /// </summary>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="processID">Идентификатор вторичного процесса.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Кортеж &lt;Информация об экземпляре процесса; Параметры диалога&gt;.</returns>
        public static Task<(KrProcessInstance, CardTaskCompletionOptionSettings)> LaunchGlobalKrProcessWithDialogAsync(
            ICardRepository cardRepository,
            Guid processID,
            CancellationToken cancellationToken = default)
        {
            var process = KrProcessBuilder
                .CreateProcess()
                .SetProcess(processID)
                .Build();

            return LaunchKrProcessWithDialogAsync(
                cardRepository,
                process,
                cancellationToken);
        }

        /// <summary>
        /// Запускает вторичный процесс при выполнении которого открывается диалог.
        /// </summary>
        /// <param name="cardRepository">Репозиторий для управления карточками.</param>
        /// <param name="krProcessInstance">Информация о запускаемом процессе.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Кортеж &lt;Информация об экземпляре процесса; Параметры диалога&gt;.</returns>
        public static async Task<(KrProcessInstance, CardTaskCompletionOptionSettings)> LaunchKrProcessWithDialogAsync(
            ICardRepository cardRepository,
            KrProcessInstance krProcessInstance,
            CancellationToken cancellationToken = default)
        {
            var result = await KrTestHelper.LaunchGlobalKrProcessAsync(
                cardRepository,
                krProcessInstance,
                cancellationToken: cancellationToken);
            ValidationAssert.HasEmpty(result.ValidationResult);

            // Получение из ответа на запрос процесса и параметров диалога.
            // Действия аналогичны выполняемым в Tessa.Extensions.Default.Client.Workflow.KrProcess.CommandInterpreter.KrAdvancedDialogCommandHandler с учётом выполнения на сервере.
            var clientCommands = result.CardResponse.GetKrProcessClientCommands();
            Assert.NotNull(clientCommands);

            var command = clientCommands.Single();
            Assert.That(command.CommandType, Is.EqualTo(DefaultCommandTypes.ShowAdvancedDialog));

            var processInstanceStorage = command.Parameters.Get<Dictionary<string, object>>(KrConstants.Keys.ProcessInstance);
            Assert.NotNull(processInstanceStorage);

            var coSettingsStorage = command.Parameters.Get<Dictionary<string, object>>(KrConstants.Keys.CompletionOptionSettings);
            Assert.NotNull(coSettingsStorage);

            var processInstance = new KrProcessInstance(processInstanceStorage);
            var coSettings = new CardTaskCompletionOptionSettings(coSettingsStorage);

            return (processInstance, coSettings);
        }

        /// <summary>
        /// Возвращает информацию о процессах запущенных по карточке с указанным идентификатором.
        /// </summary>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="mainCardID">Идентификатор карточки для которой необходимо получить информацию по запущенным процессам.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача содержащая коллекция объектов описывающих информацию по процессам.</returns>
        public static async Task<List<WorkflowProcessInfoForTest>> GetWorkflowProcessAsync(
            IDbScope dbScope,
            Guid mainCardID,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            await using (dbScope.Create())
            {
                var db = dbScope.Db;

                var queryBuilder = dbScope.BuilderFactory
                    .Select()
                        .C("wp", "ID")
                        .C("wp", "TypeName")
                        .C("wp", "Params")
                    .From("WorkflowProcesses", "wp").NoLock()
                        .InnerJoin(CardSatelliteHelper.SatellitesSectionName, "s").NoLock()
                        .On().C("s", "ID").Equals().C("wp", "ID")
                    .Where()
                        .C("s", CardSatelliteHelper.MainCardIDColumn).Equals().P(CardSatelliteHelper.MainCardIDColumn);

                db.SetCommand(
                        queryBuilder.Build(),
                        db.Parameter(CardSatelliteHelper.MainCardIDColumn, mainCardID, LinqToDB.DataType.Guid))
                    .LogCommand();

                var results = new List<WorkflowProcessInfoForTest>();
                await using (var reader = await db.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken))
                {
                    while (await reader.ReadAsync(cancellationToken))
                    {
                        var id = reader.GetGuid(0);
                        var typeName = reader.GetString(1);
                        var processParamsJson = await reader.GetSequentialNullableStringAsync(2, cancellationToken);

                        results.Add(new WorkflowProcessInfoForTest(id, typeName, processParamsJson));
                    }
                }

                return results;
            }
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Задаёт контент указанных файлов в основную карточку.
        /// </summary>
        /// <param name="dialogCardFileContainer">Контейнер содержащий информацию о карточке диалога и её файлах.</param>
        /// <param name="mainCardFileContainer">Контейнер содержащий информацию об основной карточке и её файлах.</param>
        /// <param name="taskRowID">Идентификатор задания, к которому относится диалог.</param>
        /// <param name="currentUser">Текущий пользователь.</param>
        private static void SetFileContentToMainCard(
            ICardFileContainer dialogCardFileContainer,
            ICardFileContainer mainCardFileContainer,
            Guid taskRowID,
            IUser currentUser)
        {
            var mainCard = mainCardFileContainer.Card;
            var fileContainer = mainCardFileContainer.FileContainer;

            var cardFiles = dialogCardFileContainer.Card.Files;
            foreach (var cardFile in cardFiles)
            {
                var fileID = cardFile.RowID;
                if (cardFile.State != CardFileState.Deleted)
                {
                    var file = dialogCardFileContainer.FileContainer.Files.FirstOrDefault(p => p.ID == fileID);
                    if (file is null)
                    {
                        continue;
                    }

                    // Файл был изменён.
                    if (cardFile.LastVersion is null)
                    {
                        var newNumber = file.Versions.Max(static x => x.Number) + 1;
                        var newCardFileVersion = cardFile.Versions.Add();

                        newCardFileVersion.RowID = cardFile.VersionRowID;
                        newCardFileVersion.Name = file.Name;
                        newCardFileVersion.Size = file.Size;
                        newCardFileVersion.State = CardFileVersionState.Success;
                        newCardFileVersion.Source = cardFile.StoreSource;
                        newCardFileVersion.Created = DateTime.UtcNow;
                        newCardFileVersion.Number = newNumber;

                        newCardFileVersion.CreatedByID = currentUser.ID;
                        newCardFileVersion.CreatedByName = currentUser.Name;

                        newCardFileVersion.Hash = file.Hash;

                        newCardFileVersion.ErrorDate = default;
                        newCardFileVersion.ErrorMessage = default;

                        cardFile.InvalidateLastVersion();
                    }

                    fileContainer.Files.Add(file);
                }

                var mainCardFile = mainCard.Files.Add(cardFile);
                mainCardFile.TaskID = taskRowID;
            }
        }

        #endregion
    }
}
