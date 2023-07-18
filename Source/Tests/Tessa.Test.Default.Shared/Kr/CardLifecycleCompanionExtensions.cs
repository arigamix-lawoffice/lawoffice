using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Platform;
using Tessa.Platform.Formatting;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Test.Default.Shared.Cards;
using Tessa.Test.Default.Shared.Kr.Routes;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет статические методы расширения.
    /// </summary>
    public static class CardLifecycleCompanionExtensions
    {
        #region CardLifecycleCompanion Extensions

        /// <summary>
        /// Запускает процесс, подсистемы маршрутов, имеющий указанный идентификатор и инициализирует объект типа <see cref="KrRouteProcessInstanceLifecycleCompanion"/>.
        /// </summary>
        /// <param name="clc">Объект, управляющий жизненным циклом карточки, в которой должен быть запущен процесс.</param>
        /// <param name="processID">Идентификатор запускаемого процесса.</param>
        /// <param name="info">Дополнительная информация передаваемая в процесс при запуске.</param>
        /// <returns>Объект <see cref="KrRouteProcessInstanceLifecycleCompanion"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Обратите внимание: запуск производится при сохранении карточки. После сохранения выполняется автоматическая загрузка карточки.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в запросе на сохранение карточки.<para/>
        /// По умолчанию всегда генерируется ошибка, если запуск процесса запрещён ограничениями. Если это не требуется, то необходимо задать дополнительному параметру <see cref="KrConstants.RaiseErrorWhenExecutionIsForbidden"/>, расположенному в дополнительной информации отложенного действия, значение <see cref="BooleanBoxes.False"/>.
        /// </remarks>
        /// <returns>Объект типа <see cref="KrRouteProcessInstanceLifecycleCompanion"/> для создания цепочки.</returns>
        public static KrRouteProcessInstanceLifecycleCompanion CreateKrProcess(
            this CardLifecycleCompanion clc,
            Guid processID,
            Dictionary<string, object> info = default)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            var krProcess = KrProcessBuilder
                .CreateProcess()
                .SetProcess(processID)
                .SetCard(clc.CardID)
                .SetProcessInfo(info)
                .Build();

            return clc.CreateKrProcess(krProcess);
        }

        /// <summary>
        /// Запускает процесс подсистемы маршрутов и инициализирует объект типа <see cref="KrRouteProcessInstanceLifecycleCompanion"/>.
        /// </summary>
        /// <param name="clc">Объект, управляющий жизненным циклом карточки, в которой должен быть запущен процесс.</param>
        /// <param name="krProcess">Информация об экземпляре процесса.</param>
        /// <returns>Объект <see cref="KrRouteProcessInstanceLifecycleCompanion"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Обратите внимание: запуск производится при сохранении карточки. После сохранения выполняется автоматическая загрузка карточки.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в запросе на сохранение карточки.<para/>
        /// По умолчанию всегда генерируется ошибка, если запуск процесса запрещён ограничениями. Если это не требуется, то необходимо задать дополнительному параметру <see cref="KrConstants.RaiseErrorWhenExecutionIsForbidden"/>, расположенному в дополнительной информации отложенного действия, значение <see cref="BooleanBoxes.False"/>.
        /// </remarks>
        /// <returns>Объект типа <see cref="KrRouteProcessInstanceLifecycleCompanion"/> для создания цепочки.</returns>
        public static KrRouteProcessInstanceLifecycleCompanion CreateKrProcess(
            this CardLifecycleCompanion clc,
            KrProcessInstance krProcess)
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(krProcess, nameof(krProcess));

            var routeProcessInstanceLifecycleCompanion = new KrRouteProcessInstanceLifecycleCompanion(clc, default);

            var saveAction = clc.Save().GetLastPendingAction();
            saveAction.Info.SetKrProcessInstance(krProcess);
            saveAction.Info.Add(KrConstants.RaiseErrorWhenExecutionIsForbidden, BooleanBoxes.True);

            saveAction.AddAfterAction(
                new PendingAction(
                    $"{nameof(CardLifecycleCompanionExtensions)}.{nameof(CreateKrProcess)}: ProcessID = {krProcess.ProcessID:B}",
                    (pendingAction, ct) =>
                    {
                        var storeResponse = clc.LastData.StoreResponse;
                        var result = storeResponse.GetKrProcessLaunchResult();

                        routeProcessInstanceLifecycleCompanion.SetProcessInstanceID(result?.ProcessID);

                        // Результат выполнения был добавлен при выполнении ICardLifecycleCompanion<T>.Save().
                        return new ValueTask<ValidationResult>(ValidationResult.Empty);
                    }));

            return routeProcessInstanceLifecycleCompanion;
        }

        /// <summary>
        /// Инициализирует объект типа <see cref="KrRouteProcessInstanceLifecycleCompanion"/>.
        /// </summary>
        /// <param name="clc">Объект, управляющий жизненным циклом карточки, в которой должен быть запущен процесс.</param>
        /// <returns>Объект <see cref="KrRouteProcessInstanceLifecycleCompanion"/> для создания цепочки.</returns>
        /// <returns>Объект типа <see cref="KrRouteProcessInstanceLifecycleCompanion"/> для создания цепочки.</returns>
        public static KrRouteProcessInstanceLifecycleCompanion CreateKrProcess(
            this CardLifecycleCompanion clc) =>
            new KrRouteProcessInstanceLifecycleCompanion(clc, default);

        #endregion

        #region ICardLifecycleCompanion<T> Extensions

        /// <summary>
        /// Устанавливает тип документа имеющий указанный идентификатор и имя.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="docTypeID">Идентификатор типа документа.</param>
        /// <param name="docTypeName">Имя типа документа.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static T WithDocType<T>(
            this T clc,
            Guid docTypeID,
            string docTypeName)
            where T : ICardLifecycleCompanion<T>
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            return clc
                .WithInfoPair(KrConstants.Keys.DocTypeID, docTypeID)
                .WithInfoPair(KrConstants.Keys.DocTypeTitle, docTypeName);
        }

        /// <summary>
        /// Берёт в работу указанное задание.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="task">Завершаемое задание.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <exception cref="InvalidOperationException">Задание с указанным идентификатором не найдено.</exception>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static T TakeInProgressTask<T>(
            this T clc,
            CardTask task)
            where T : ICardLifecycleCompanion<T>
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(task, nameof(task));

            clc
                .Save()
                .GetLastPendingAction()
                .AddPreparationAction(
                    new PendingAction(
                        nameof(CardLifecycleCompanionExtensions) + "." + nameof(TakeInProgressTask),
                        (action, ct) =>
                        {
                            if (!clc.GetCardOrThrow().Tasks.Contains(task))
                            {
                                throw new ArgumentException(
                                    $"The specified task object is not contained in the {nameof(clc)}.{nameof(clc.Card)}.{nameof(clc.Card.Tasks)}.",
                                    nameof(task));
                            }

                            task.State = CardRowState.Modified;
                            task.Action = CardTaskAction.Progress;

                            return ValueTask.FromResult(ValidationResult.Empty);
                        }));

            return clc;
        }

        /// <summary>
        /// Завершает указанное задание.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="task">Завершаемое задание.</param>
        /// <param name="optionID">Идентификатор варианта завершения.</param>
        /// <param name="beforeCompleteTaskActionAsync">Действие выполняемое перед завершением задания.</param>
        /// <param name="deleteTask">Значение <see langword="true"/>, если задание должно быть удалено при завершении, иначе - <see langword="false"/>. Если значение не задано и в метаданных найден тип задания и вариант завершения, то состояние строки с заданием определяется автоматически в соответствии с флагом <see cref="CardTypeCompletionOptionFlags.DoNotDeleteTask"/>. Если тип задания или вариант завершения не найдены, то задание удаляется.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <exception cref="InvalidOperationException">Задание с указанным идентификатором не найдено.</exception>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// После завершения задания, выполняется сохранение карточки.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в запросе на сохранение карточки.
        /// </remarks>
        public static T CompleteTask<T>(
            this T clc,
            CardTask task,
            Guid optionID,
            Func<CardTask, CancellationToken, ValueTask> beforeCompleteTaskActionAsync = default,
            bool? deleteTask = null)
            where T : ICardLifecycleCompanion<T>
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(task, nameof(task));

            clc
                .Save()
                .GetLastPendingAction()
                .AddPreparationAction(
                    new PendingAction(
                        nameof(CardLifecycleCompanionExtensions) + "." + nameof(CompleteTask),
                        async (action, ct) =>
                        {
                            if (!clc.GetCardOrThrow().Tasks.Contains(task))
                            {
                                throw new ArgumentException(
                                    $"The specified task object is not contained in the {nameof(clc)}.{nameof(clc.Card)}.{nameof(clc.Card.Tasks)}.",
                                    nameof(task));
                            }

                            await CompleteTaskInternalAsync(
                                clc.Dependencies.CardMetadata,
                                task,
                                optionID,
                                deleteTask,
                                ct);

                            if (beforeCompleteTaskActionAsync is not null)
                            {
                                await beforeCompleteTaskActionAsync(task, ct);
                            }

                            return ValidationResult.Empty;
                        }));

            return clc;
        }

        /// <summary>
        /// Устанавливает заданное действие выполняемое с заданием.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="task">Задание в котором устанавливается действие.</param>
        /// <param name="taskAction">Устанавливаемое действие.</param>
        /// <param name="beforeCompleteActionAsync">Действие выполняемое перед заданием действия и состояния строки задания.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Состояние строки с заданием устанавливается равным <see cref="CardRowState.Modified"/>.<para/>
        /// После выполнения действия задания, выполняется сохранение карточки.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в запросе на сохранение карточки.
        /// </remarks>
        public static T ActionTask<T>(
            this T clc,
            CardTask task,
            CardTaskAction taskAction,
            Func<Card, CardTask, ValueTask> beforeCompleteActionAsync = default)
            where T : ICardLifecycleCompanion<T>
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(task, nameof(task));

            clc
                .Save()
                .GetLastPendingAction()
                .AddPreparationAction(
                    new PendingAction(
                        nameof(CardLifecycleCompanionExtensions) + "." + nameof(ActionTask),
                        async (action, ct) =>
                        {
                            var card = clc.GetCardOrThrow();

                            if (beforeCompleteActionAsync is not null)
                            {
                                await beforeCompleteActionAsync(card, task);
                            }
                            task.Action = taskAction;
                            task.State = CardRowState.Modified;

                            return ValidationResult.Empty;
                        }));

            return clc;
        }

        /// <summary>
        /// Выполняет пересчёт маршрута в указанной карточке.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="iac">Режим вывода информации об изменениях в маршруте после пересчёта.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// После пересчёта маршрута выполняется сохранение карточки.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в запросе на сохранение карточки.
        /// </remarks>
        public static T Recalc<T>(
            this T clc,
            InfoAboutChanges iac = InfoAboutChanges.ToInfo)
            where T : ICardLifecycleCompanion<T>
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            clc.Save();

            var lastActionInfo = clc.GetLastPendingAction().Info;
            lastActionInfo.SetRecalcFlag();
            lastActionInfo.SetInfoAboutChanges(iac);

            return clc;
        }

        /// <summary>
        /// Загружает карточку с скрытыми этапами.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// Можно указать дополнительную информацию, которая будет передана в запросе на загрузку карточки.
        /// </remarks>
        public static T LoadWithHiddenStages<T>(
            this T clc)
            where T : ICardLifecycleCompanion<T>
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            clc
                .Load()
                .GetLastPendingAction().Info.DontHideStages();
            return clc;
        }

        #endregion

        #region ICardLifecycleCompanion Extensions

        /// <summary>
        /// Изменяет значение указанного поля строковой секции карточки добавляя к значению <paramref name="value"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="section">Имя секции.</param>
        /// <param name="field">Имя поля.</param>
        /// <param name="value">Добавляемое значение. Если задано значение <see langword="null"/> или <see cref="string.Empty"/>, то добавляется значение по умолчанию.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static T ModifyDocument<T>(
            this T clc,
            string section = KrConstants.DocumentCommonInfo.Name,
            string field = KrConstants.DocumentCommonInfo.Subject,
            string value = ".")
            where T : ICardLifecycleCompanion, IPendingActionsProvider<IPendingAction, T>
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            clc.AddPendingAction(
                new PendingAction(
                    nameof(CardLifecycleCompanionExtensions) + "." + nameof(ModifyDocument),
                    (action, ct) =>
                    {
                        var fields = clc.GetCardOrThrow().Sections[section].Fields;

                        if (string.IsNullOrEmpty(value))
                        {
                            value = ".";
                        }

                        fields[field] = fields.Get<object>(field) + value;
                        return new ValueTask<ValidationResult>(ValidationResult.Empty);
                    }));

            return clc;
        }

        /// <summary>
        /// Устанавливает значение указанного поля строковой секции карточки.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="section">Имя секции.</param>
        /// <param name="field">Имя поля.</param>
        /// <param name="newValue">Задаваемое значение.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static T SetValue<T>(
            this T clc,
            string section,
            string field,
            object newValue)
            where T : ICardLifecycleCompanion, IPendingActionsProvider<IPendingAction, T>
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            clc.AddPendingAction(
                new PendingAction(
                    $"{nameof(CardLifecycleCompanionExtensions)}.{nameof(SetValue)}: Section name: \"{section}\", Field name: \"{field}\".",
                    (action, ct) =>
                    {
                        clc.GetCardOrThrow().Sections[section].Fields[field] = newValue;
                        return new ValueTask<ValidationResult>(ValidationResult.Empty);
                    }));

            return clc;
        }

        /// <summary>
        /// Устанавливает значение в указанном поле заданной строки коллекционной или древовидной секции.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="section">Имя секции.</param>
        /// <param name="rowNumber">Порядковый номер строки.</param>
        /// <param name="field">Имя поля.</param>
        /// <param name="newValue">Задаваемое значение.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static T SetValueRow<T>(
            this T clc,
            string section,
            int rowNumber,
            string field,
            object newValue)
            where T : ICardLifecycleCompanion, IPendingActionsProvider<IPendingAction, T>
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            clc.AddPendingAction(
                new PendingAction(
                    $"{nameof(CardLifecycleCompanionExtensions)}.{nameof(SetValueRow)}: Section name: \"{section}\", Row number: {rowNumber}, Field name: \"{field}\".",
                    (action, ct) =>
                    {
                        clc.GetCardOrThrow().Sections[section].Rows[rowNumber].Fields[field] = newValue;
                        return new ValueTask<ValidationResult>(ValidationResult.Empty);
                    }));
            return clc;
        }

        /// <summary>
        /// Возвращает значение поля <paramref name="fieldName"/> содержащегося в строковой секции <paramref name="sectionName"/>.
        /// </summary>
        /// <typeparam name="T">Тип значения.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Значение поля или значение по умолчанию для типа <typeparamref name="T"/>, если секция не содержит указанного поля.</returns>
        public static T GetValue<T>(
            this ICardLifecycleCompanion clc,
            string sectionName,
            string fieldName)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            return clc
                .GetCardOrThrow()
                .Sections[sectionName]
                .RawFields
                .Get<T>(fieldName);
        }

        /// <summary>
        /// Возвращает значение поля <paramref name="fieldName"/> содержащегося в строковой секции <paramref name="sectionName"/>.
        /// </summary>
        /// <typeparam name="T">Тип значения.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Значение поля или значение по умолчанию для типа <typeparamref name="T"/>, если секция не содержит указанного поля.</returns>
        public static T TryGetValue<T>(
            this ICardLifecycleCompanion clc,
            string sectionName,
            string fieldName)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            return clc
                .GetCardOrThrow()
                .Sections[sectionName]
                .RawFields
                .TryGet<T>(fieldName);
        }

        /// <summary>
        /// Возвращает значение поля <paramref name="fieldName"/>, содержащегося в строке <paramref name="rowIndex"/> коллекционной или древовидной секции <paramref name="sectionName"/>.
        /// </summary>
        /// <typeparam name="T">Тип значения.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="rowIndex">Порядковый индекс строки.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Значение поля.</returns>
        public static T GetValue<T>(
            this ICardLifecycleCompanion clc,
            string sectionName,
            int rowIndex,
            string fieldName)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            return clc
                .GetCardOrThrow()
                .Sections[sectionName]
                .Rows[rowIndex]
                .Get<T>(fieldName);
        }

        /// <summary>
        /// Возвращает значение поля <paramref name="fieldName"/>, содержащегося в строке <paramref name="rowIndex"/> коллекционной или древовидной секции <paramref name="sectionName"/>.
        /// </summary>
        /// <typeparam name="T">Тип значения.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="rowIndex">Порядковый индекс строки.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Значение поля или значение по умолчанию для типа <typeparamref name="T"/>, если секция не содержит строки или строка не содержит указанного поля.</returns>
        public static T TryGetValue<T>(
            this ICardLifecycleCompanion clc,
            string sectionName,
            int rowIndex,
            string fieldName)
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(sectionName, nameof(sectionName));

            if (rowIndex < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(rowIndex), rowIndex, "The value is less than zero.");
            }

            Check.ArgumentNotNull(fieldName, nameof(fieldName));

            var row = clc
                .GetCardOrThrow()
                .Sections[sectionName]
                .TryGetRows()
                ?.ElementAtOrDefault(rowIndex);

            return row is null ? default : row.TryGet<T>(fieldName);
        }

        /// <summary>
        /// Возвращает значение поля <paramref name="fieldName"/>, содержащегося в первой строке коллекционной или древовидной секции <paramref name="sectionName"/>, удовлетворяющей условию <paramref name="rowPredicate"/>.
        /// </summary>
        /// <typeparam name="T">Тип значения.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="rowPredicate">Поисковое условие.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <returns>Значение поля или значение по умолчанию для типа <typeparamref name="T"/>, если не найдена строка удовлетворяющая поисковому условию или строка не содержит указанного поля.</returns>
        public static T GetValue<T>(
            this ICardLifecycleCompanion clc,
            string sectionName,
            Func<CardRow, bool> rowPredicate,
            string fieldName)
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(sectionName, nameof(sectionName));
            Check.ArgumentNotNull(rowPredicate, nameof(rowPredicate));
            Check.ArgumentNotNull(fieldName, nameof(fieldName));

            var row = clc
                .GetCardOrThrow()
                .Sections[sectionName]
                .TryGetRows()
                ?.FirstOrDefault(rowPredicate);

            return row is null ? default : row.TryGet<T>(fieldName);
        }

        /// <summary>
        /// Возвращает значение поля <paramref name="fieldName"/> содержащегося в строке удовлетворяющей условию <paramref name="rowPredicateAsync"/> коллекционной или древовидной секции <paramref name="sectionName"/>.
        /// </summary>
        /// <typeparam name="T">Тип значения.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="sectionName">Имя секции.</param>
        /// <param name="rowPredicateAsync">Поисковое условие.</param>
        /// <param name="fieldName">Имя поля.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение поля или значение по умолчанию для типа <typeparamref name="T"/>, если не найдена строка удовлетворяющая поисковому условию или строка не содержит указанного поля.</returns>
        public static async ValueTask<T> GetValueAsync<T>(
            this ICardLifecycleCompanion clc,
            string sectionName,
            Func<CardRow, CancellationToken, ValueTask<bool>> rowPredicateAsync,
            string fieldName,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(rowPredicateAsync, nameof(rowPredicateAsync));

            var rows = clc
                .GetCardOrThrow()
                .Sections[sectionName]
                .TryGetRows();

            if (rows is null)
            {
                return default;
            }

            var row = await rows.FirstOrDefaultAsync(rowPredicateAsync, cancellationToken);
            return row is not null ? row.TryGet<T>(fieldName) : default;
        }

        /// <summary>
        /// Добавляет новую строку в коллекционную или древовидную секцию.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку, в секцию которой должна быть добавлена новая строка.</param>
        /// <param name="section">Имя секции в которую должна быть добавлена новая строка.</param>
        /// <param name="orderField">Имя поля содержащего порядок сортировки (order).</param>
        /// <param name="rowValues">Перечисление пар &lt;ключ; значение&gt; которыми выполняется инициализация полей новой строки. Может быть не задано.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.<para/>
        /// RowID строки задаётся автоматически, если не был указан явно в <paramref name="rowValues"/>.
        /// </remarks>
        public static T AddRow<T>(
            this T clc,
            string section,
            string orderField = default,
            IEnumerable<KeyValuePair<string, object>> rowValues = default)
            where T : ICardLifecycleCompanion, IPendingActionsProvider<IPendingAction, T>
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(section, nameof(section));

            clc.AddPendingAction(
                new PendingAction(
                    nameof(CardLifecycleCompanionExtensions) + "." + nameof(AddRow),
                    (action, ct) =>
                    {
                        var rows = clc.GetCardOrThrow().Sections[section].Rows;
                        TestCardHelper.AddRow(rows, clc.CardID, orderField, rowValues);
                        return new ValueTask<ValidationResult>(ValidationResult.Empty);
                    }));

            return clc;
        }

        /// <summary>
        /// Возвращает строку содержащую информацию о первом этапе имеющем указанное имя.
        /// </summary>
        /// <param name="clc">Объект, содержащий карточку с маршрутом из которого требуется получить этап с указанным именем.</param>
        /// <param name="name">Имя этапа.</param>
        /// <returns>Строка содержащая информацию о первом этапе имеющем указанное имя или значение по умолчанию для типа, если такую строку не удалось найти.</returns>
        public static CardRow GetStage(
            this ICardLifecycleCompanion clc,
            string name)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            return clc
                .GetCardOrThrow()
                .GetStagesSection()
                .TryGetRows()
                ?.FirstOrDefault(p => string.Equals(p.Fields.Get<string>(KrConstants.KrStages.NameField), name, StringComparison.Ordinal));
        }

        /// <summary>
        /// Проверяет содержит ли объект, управляющий жизненным циклом карточки, карточку указанного типа.
        /// </summary>
        /// <param name="clc">Объект, управляющий жизненным циклом карточки.</param>
        /// <param name="expectedTypeID">Идентификатор ожидаемого типа.</param>
        /// <param name="expectedTypeName">Имя ожидаемого типа.</param>
        /// <returns>Карточка содержащаяся в <paramref name="clc"/>.</returns>
        /// <exception cref="ArgumentException">The <see cref="ICardLifecycleCompanion"/> object must contain a card of type <paramref name="expectedTypeName"/> (ID = "<paramref name="expectedTypeID"/>"). Actual type <see cref="Card.TypeCaption"/> (ID = "<see cref="Card.TypeID"/>").</exception>
        public static Card GetAndCheckCardTypeThrow(
            this ICardLifecycleCompanion clc,
            Guid expectedTypeID,
            string expectedTypeName)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            var card = clc.GetCardOrThrow();

            KrAssert.CheckCardType(
                card,
                expectedTypeID,
                expectedTypeName);

            return card;
        }

        /// <summary>
        /// Возвращает первое задание удовлетворяющее указанному предикату. Если задание не удалось найти, то генерируется исключение <see cref="AssertionException"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="predicate">Поисковое условие.</param>
        /// <param name="notFoundMessage">Строка добавляемая к стандартному тексту сообщения об отсутствии удовлетворяющего условия задания.</param>
        /// <param name="task">Возвращаемое значение. Первое задание удовлетворяющее условие или значение по умолчанию для типа, если таковое не удалось найти.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <exception cref="AssertionException">Task not found. <paramref name="notFoundMessage"/></exception>
        public static T GetTaskOrThrow<T>(
            this T clc,
            Func<CardTask, bool> predicate,
            string notFoundMessage,
            out CardTask task) where T : ICardLifecycleCompanion
        {
            // Значения clc и predicateAsync будут проверены в GetTask.

            task = clc.GetTask(predicate);

            if (task is null)
            {
                AssertFailTaskNotFound(clc, notFoundMessage);
            }

            return clc;
        }

        /// <summary>
        /// Возвращает первое задание удовлетворяющее указанному предикату. Если задание не удалось найти, то генерируется исключение <see cref="AssertionException"/>.
        /// </summary>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="predicateAsync">Поисковое условие.</param>
        /// <param name="notFoundMessage">Строка добавляемая к стандартному тексту сообщения об отсутствии удовлетворяющего условия задания.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Первое задание удовлетворяющее условие или значение по умолчанию для типа, если таковое не удалось найти.</returns>
        /// <exception cref="AssertionException">Task not found. <paramref name="notFoundMessage"/></exception>
        public static async ValueTask<CardTask> GetTaskOrThrowAsync(
            this ICardLifecycleCompanion clc,
            Func<CardTask, CancellationToken, ValueTask<bool>> predicateAsync,
            string notFoundMessage,
            CancellationToken cancellationToken = default)
        {
            // Значения clc и predicateAsync будут проверены в GetTaskAsync.

            var task = await clc.GetTaskAsync(predicateAsync, cancellationToken);

            if (task is null)
            {
                AssertFailTaskNotFound(clc, notFoundMessage);
            }

            return task;
        }

        /// <summary>
        /// Возвращает первое задание указанного типа. Если задание не удалось найти, то генерируется исключение <see cref="AssertionException"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="taskTypeID">Идентификатор типа задания.</param>
        /// <param name="task">Искомое задание или <see langword="null"/>, если задание не удалось найти.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <exception cref="AssertionException">Task not found. Task type ID = <paramref name="taskTypeID"/>.</exception>
        public static T GetTaskOrThrow<T>(
            this T clc,
            Guid taskTypeID,
            out CardTask task) where T : ICardLifecycleCompanion
        {
            // Значение clc будет проверено в GetTaskOrThrow.

            return clc
                .GetTaskOrThrow(
                    task => task.TypeID == taskTypeID,
                    $"Task type ID = {taskTypeID:B}.",
                    out task);
        }

        /// <summary>
        /// Возвращает первое задание указанного типа, назначенное на исполнителя задания с указанной ролью. Если задание не удалось найти, то генерируется исключение <see cref="AssertionException"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="taskTypeID">Идентификатор типа задания.</param>
        /// <param name="roleID">Идентификатор роли, на которую назначено задание.</param>
        /// <param name="task">Искомое задание или <see langword="null"/>, если задание не удалось найти.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <exception cref="AssertionException">Task not found. Task type ID = <paramref name="taskTypeID"/>, Task role ID = <see cref="CardFunctionRoles.PerformerID"/>, Role ID = <paramref name="roleID"/>.</exception>
        public static T GetTaskWithPerformerOrThrow<T>(
            this T clc,
            Guid taskTypeID,
            Guid roleID,
            out CardTask task) where T : ICardLifecycleCompanion
        {
            // Значение clc будет проверено в GetTaskWithRoleOrThrow.

            return clc
                .GetTaskWithRoleOrThrow(
                    taskTypeID,
                    CardFunctionRoles.PerformerID,
                    roleID,
                    out task);
        }

        /// <summary>
        /// Возвращает первое задание указанного типа, назначенное на указанную роль с функциональной ролью заданного типа. Если задание не удалось найти, то генерируется исключение <see cref="AssertionException"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="taskTypeID">Идентификатор типа задания.</param>
        /// <param name="taskRoleID">Идентификатор функциональной роли.</param>
        /// <param name="roleID">Идентификатор роли, на которую назначено задание.</param>
        /// <param name="task">Искомое задание или <see langword="null"/>, если задание не удалось найти.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <exception cref="AssertionException">Task not found. Task type ID = <paramref name="taskTypeID"/>, Task role ID = <paramref name="taskRoleID"/>, Role ID = <paramref name="roleID"/>.</exception>
        public static T GetTaskWithRoleOrThrow<T>(
            this T clc,
            Guid taskTypeID,
            Guid taskRoleID,
            Guid roleID,
            out CardTask task) where T : ICardLifecycleCompanion
        {
            // Значение clc будет проверено в GetTaskOrThrow.

            return clc
                .GetTaskOrThrow(
                    task =>
                        task.TypeID == taskTypeID
                        && task
                            .TryGetTaskAssignedRoles()
                            ?.Any(i =>
                                i.TaskRoleID == taskRoleID && i.RoleID == roleID) == true,
                    $"Task type ID = {taskTypeID:B}, Task role ID = {taskRoleID:B}, Role ID = {roleID:B}.",
                    out task);
        }

        /// <summary>
        /// Возвращает первое задание произвольного типа, кроме виртуального задания типа <see cref="DefaultTaskTypes.KrInfoForInitiatorTypeID"/>. Если задание не удалось найти, то генерируется исключение <see cref="AssertionException"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="task">Возвращаемое значение. Первое задание, тип которого не равен <see cref="DefaultTaskTypes.KrInfoForInitiatorTypeID"/>, или значение по умолчанию для типа, если таковое не удалось найти.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <exception cref="AssertionException">Task not found. Except task type ID = <see cref="DefaultTaskTypes.KrInfoForInitiatorTypeID"/>.</exception>
        public static T GetAnyTaskOrThrow<T>(
            this T clc,
            out CardTask task) where T : ICardLifecycleCompanion
        {
            // Значение clc будет проверено в GetTaskOrThrow.

            return clc.GetTaskOrThrow(
                task => task.TypeID != DefaultTaskTypes.KrInfoForInitiatorTypeID,
                $"Except task type ID = {DefaultTaskTypes.KrInfoForInitiatorTypeID:B}.",
                out task);
        }

        /// <summary>
        /// Возвращает любое первое задание типового процесса согласования, кроме виртуального задания типа <see cref="DefaultTaskTypes.KrInfoForInitiatorTypeID"/>. Если задание не удалось найти, то генерируется исключение <see cref="AssertionException"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="task">Возвращаемое значение. Первое задание типового процесса согласования, кроме виртуального задания типа <see cref="DefaultTaskTypes.KrInfoForInitiatorTypeID"/> или значение по умолчанию для типа, если таковое не удалось найти.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <exception cref="AssertionException">Task not found. Card does not contain Kr tasks.</exception>
        /// <seealso cref="KrConstants.KrTaskTypeIDList"/>
        public static T GetAnyKrTaskOrThrow<T>(
            this T clc,
            out CardTask task) where T : ICardLifecycleCompanion
        {
            // Значение clc будет проверено в GetTaskOrThrow.

            return clc.GetTaskOrThrow(
                task => KrConstants.KrTaskTypeIDList.Contains(task.TypeID),
                "Card does not contain Kr tasks.",
                out task);
        }

        /// <summary>
        /// Возвращает любое первое задание процесса резолюции. Если задание не удалось найти, то генерируется исключение <see cref="AssertionException"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="task">Возвращаемое значение. Первое задание процесса резолюции или значение по умолчанию для типа, если таковое не удалось найти.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <exception cref="AssertionException">Task not found. Card does not contain WfResolution tasks.</exception>
        /// <seealso cref="WfHelper.ResolutionTaskTypeIDList"/>
        public static T GetAnyWfTaskOrThrow<T>(
            this T clc,
            out CardTask task) where T : ICardLifecycleCompanion
        {
            // Значение clc будет проверено в GetTaskOrThrow.

            return clc.GetTaskOrThrow(
                task => WfHelper.TaskTypeIsResolution(task.TypeID),
                "Card does not contain WfResolution tasks.",
                out task);
        }

        /// <summary>
        /// Возвращает первое задание удовлетворяющее заданному условию или значение по умолчанию для типа, если такое задание не было найдено.
        /// </summary>
        /// <param name="clc">Объект, содержащий карточку, в которой выполняется поиск задания.</param>
        /// <param name="predicate">Предикат, определяющий искомое задание.</param>
        /// <returns>Задание удовлетворяющее заданному условию или значение по умолчанию для типа, если такое задание не было найдено.</returns>
        public static CardTask GetTask(
            this ICardLifecycleCompanion clc,
            Func<CardTask, bool> predicate)
        {
            Check.ArgumentNotNull(clc, nameof(clc));

            return clc.GetCardOrThrow().TryGetTasks()?.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Возвращает первое задание удовлетворяющее заданному условию или значение по умолчанию для типа, если такое задание не было найдено.
        /// </summary>
        /// <param name="clc">Объект, содержащий карточку, в которой выполняется поиск задания.</param>
        /// <param name="predicateAsync">Предикат, определяющий искомое задание.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Задание удовлетворяющее заданному условию или значение по умолчанию для типа, если такое задание не было найдено.</returns>
        public static ValueTask<CardTask> GetTaskAsync(
            this ICardLifecycleCompanion clc,
            Func<CardTask, CancellationToken, ValueTask<bool>> predicateAsync,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(predicateAsync, nameof(predicateAsync));

            var tasks = clc.GetCardOrThrow().TryGetTasks();

            return tasks is null
                ? ValueTask.FromResult<CardTask>(null)
                : tasks.FirstOrDefaultAsync(predicateAsync, cancellationToken);
        }

        /// <summary>
        /// Возвращает первую запись из истории заданий, удовлетворяющую условию.
        /// </summary>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="predicate">Условие поиска.</param>
        /// <returns>Первая запись из истории заданий, удовлетворяющая условию, или значение <see langword="null"/>, если её не удалось найти.</returns>
        public static CardTaskHistoryItem GetCardTaskHistoryItem(
            this ICardLifecycleCompanion clc,
            Func<CardTaskHistoryItem, bool> predicate)
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(predicate, nameof(predicate));

            return clc
                .GetCardOrThrow()
                .TryGetTaskHistory()
                ?.FirstOrDefault(predicate);
        }

        /// <summary>
        /// Возвращает первую запись из истории заданий, удовлетворяющую условию.
        /// </summary>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="predicate">Условие поиска.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Первая запись из истории заданий, удовлетворяющая условию, или значение <see langword="null"/>, если её не удалось найти.</returns>
        public static ValueTask<CardTaskHistoryItem> GetCardTaskHistoryItemAsync(
            this ICardLifecycleCompanion clc,
            Func<CardTaskHistoryItem, CancellationToken, ValueTask<bool>> predicate,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(clc, nameof(clc));
            Check.ArgumentNotNull(predicate, nameof(predicate));

            var items = clc
                .GetCardOrThrow()
                .TryGetTaskHistory();

            return items is null
                ? ValueTask.FromResult<CardTaskHistoryItem>(null)
                : items.FirstOrDefaultAsync(predicate, cancellationToken);
        }

        /// <summary>
        /// Возвращает первую запись из истории заданий, удовлетворяющую условию. Если запись не удалось найти, то генерируется исключение <see cref="AssertionException"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="predicate">Условие поиска.</param>
        /// <param name="notFoundMessage">Строка добавляемая к стандартному тексту сообщения об отсутствии удовлетворяющей условие записи.</param>
        /// <param name="item">Искомая запись или <see langword="null"/>, если её не удалось найти.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <exception cref="AssertionException">Task history item not found. <paramref name="notFoundMessage"/></exception>
        public static T GetCardTaskHistoryItemOrThrow<T>(
            this T clc,
            Func<CardTaskHistoryItem, bool> predicate,
            string notFoundMessage,
            out CardTaskHistoryItem item)
            where T : ICardLifecycleCompanion
        {
            // Значения clc и predicate будут проверены в GetCardTaskHistoryItem.

            item = clc.GetCardTaskHistoryItem(predicate);

            if (item is null)
            {
                AssertFailTaskHistoryItemNotFound(clc, notFoundMessage);
            }

            return clc;
        }

        /// <summary>
        /// Возвращает первую запись из истории заданий, удовлетворяющую условию. Если запись не удалось найти, то генерируется исключение <see cref="AssertionException"/>.
        /// </summary>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="predicateAsync">Условие поиска.</param>
        /// <param name="notFoundMessage">Строка добавляемая к стандартному тексту сообщения об отсутствии удовлетворяющей условие записи.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Искомая запись или <see langword="null"/>, если её не удалось найти.</returns>
        /// <exception cref="AssertionException">Task history item not found. <paramref name="notFoundMessage"/></exception>
        public static async ValueTask<CardTaskHistoryItem> GetCardTaskHistoryItemOrThrowAsync(
            this ICardLifecycleCompanion clc,
            Func<CardTaskHistoryItem, CancellationToken, ValueTask<bool>> predicateAsync,
            string notFoundMessage,
            CancellationToken cancellationToken = default)
        {
            // Значения clc и predicateAsync будут проверены в GetCardTaskHistoryItemAsync.

            var item = await clc.GetCardTaskHistoryItemAsync(
                predicateAsync,
                cancellationToken);

            if (item is null)
            {
                AssertFailTaskHistoryItemNotFound(clc, notFoundMessage);
            }

            return item;
        }

        /// <summary>
        /// Возвращает первую запись из истории заданий, имеющую заданный идентификатор. Если запись не удалось найти, то генерируется исключение <see cref="AssertionException"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="rowID">Идентификатор записи.</param>
        /// <param name="item">Искомая запись или <see langword="null"/>, если её не удалось найти.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <exception cref="AssertionException">Task history item not found. RowID = <paramref name="rowID"/></exception>
        public static T GetCardTaskHistoryItemOrThrow<T>(
            this T clc,
            Guid rowID,
            out CardTaskHistoryItem item)
            where T : ICardLifecycleCompanion
        {
            // Значение clc будет проверено в GetCardTaskHistoryItemOrThrow.

            return clc
                .GetCardTaskHistoryItemOrThrow(
                    i => i.RowID == rowID,
                    $"RowID = {rowID:B}",
                    out item);
        }

        /// <summary>
        /// Возвращает родительскую запись из истории заданий для записи с указанным идентификатором. Если запись не удалось найти, то генерируется исключение <see cref="AssertionException"/>.
        /// </summary>
        /// <typeparam name="T">Тип объекта, управляющего жизненным циклом карточки.</typeparam>
        /// <param name="clc">Объект, содержащий карточку.</param>
        /// <param name="childItemRowID">Идентификатор дочерней записи.</param>
        /// <param name="item">Искомая запись или <see langword="null"/>, если её не удалось найти.</param>
        /// <returns>Объект <typeparamref name="T"/> для создания цепочки.</returns>
        /// <exception cref="AssertionException">Task history item not found.</exception>
        /// <exception cref="InvalidOperationException"><see cref="CardTaskHistoryItem.ParentRowID"/> is required.</exception>
        public static T GetParentCardTaskHistoryItemOrThrow<T>(
            this T clc,
            Guid childItemRowID,
            out CardTaskHistoryItem item)
            where T : ICardLifecycleCompanion
        {
            // Значение clc будет проверено в GetCardTaskHistoryItemOrThrow.

            clc
                .GetCardTaskHistoryItemOrThrow(
                    childItemRowID,
                    out var childCardTaskHistoryItem);

            if (!childCardTaskHistoryItem.ParentRowID.HasValue)
            {
                throw new InvalidOperationException($"{nameof(CardTaskHistoryItem.ParentRowID)} is required. Row: {TestHelper.GetCardTaskHistoryItemInfo(childCardTaskHistoryItem)}.");
            }

            clc
                .GetCardTaskHistoryItemOrThrow(
                    childCardTaskHistoryItem.ParentRowID.Value,
                    out item);

            return clc;
        }

        #endregion

        #region Private methods

        private static async ValueTask<CardTask> CompleteTaskInternalAsync(
            ICardMetadata cardMetadata,
            CardTask task,
            Guid optionID,
            bool? deleteTask,
            CancellationToken cancellationToken = default)
        {
            CardRowState taskState;

            if (deleteTask.HasValue)
            {
                taskState = deleteTask.Value
                    ? CardRowState.Deleted
                    : CardRowState.Modified;
            }
            else
            {
                var cardTypeColection = await cardMetadata.GetCardTypesAsync(cancellationToken);
                cardTypeColection.TryGetValue(task.TypeID, out var cardType);
                var optionInfo = cardType
                    ?.CompletionOptions
                    .FirstOrDefault(x => x.TypeID == optionID);

                taskState = optionInfo is not null
                        && optionInfo.Flags.Has(CardTypeCompletionOptionFlags.DoNotDeleteTask)
                    ? CardRowState.Modified
                    : CardRowState.Deleted;
            }

            task.State = taskState;
            task.OptionID = optionID;
            task.Action = CardTaskAction.Complete;

            return task;
        }

        private static async ValueTask<T> FirstOrDefaultAsync<T>(
            this IEnumerable<T> source,
            Func<T, CancellationToken, ValueTask<bool>> predicateAsync,
            CancellationToken cancellationToken = default)
        {
            foreach (var item in source)
            {
                if (await predicateAsync(item, cancellationToken))
                {
                    return item;
                }
            }

            return default;
        }

        private static void AssertFailTaskNotFound(
            ICardLifecycleCompanion clc,
            string notFoundMessage)
        {
            var tasks = clc.GetCardOrThrow().TryGetTasks();
            var tasksStr = tasks is null || tasks.Count == 0
                ? FormattingHelper.EmptyText
                : Environment.NewLine + string.Join(Environment.NewLine, tasks.Select(TestHelper.GetTaskInfo));

            var sb = StringBuilderHelper.Acquire()
                .Append("Task not found.");

            if (!string.IsNullOrEmpty(notFoundMessage))
            {
                sb
                    .Append(' ')
                    .Append(notFoundMessage);
            }

            sb
                .AppendLine()
                .Append("Actual tasks: ")
                .Append(tasksStr);

            Assert.Fail(sb.ToString());
        }

        private static void AssertFailTaskHistoryItemNotFound(
            ICardLifecycleCompanion clc,
            string notFoundMessage)
        {
            var items = clc.GetCardOrThrow().TryGetTaskHistory();
            var itemsStr = items is null
                ? FormattingHelper.EmptyText
                : Environment.NewLine + string.Join(Environment.NewLine, items.Select(TestHelper.GetCardTaskHistoryItemInfo));

            var sb = StringBuilderHelper.Acquire()
                .Append("Task history item not found.");

            if (!string.IsNullOrEmpty(notFoundMessage))
            {
                sb
                    .Append(' ')
                    .Append(notFoundMessage);
            }

            sb
                .AppendLine()
                .Append("Actual task history items: ")
                .Append(itemsStr);

            Assert.Fail(sb.ToString());
        }

        #endregion
    }
}
