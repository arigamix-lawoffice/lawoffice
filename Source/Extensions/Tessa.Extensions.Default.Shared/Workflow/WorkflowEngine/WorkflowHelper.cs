using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using Tessa.Scheme;
using Tessa.Views;
using Tessa.Views.Metadata.Criteria;
using Tessa.Workflow.Bindings;
using Tessa.Workflow.Helpful;
using Tessa.Workflow.Storage;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;
using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine
{
    /// <summary>
    /// Предоставляет вспомогательные методы для работы с WorkflowEngine.
    /// </summary>
    public static class WorkflowHelper
    {
        #region Static methods

        #region ProcessCycle

        /// <summary>
        /// Возвращает номер текущего цикла процесса согласования.
        /// </summary>
        /// <param name="processHash">Параметры процесса.</param>
        /// <param name="defaultValue">Номер цикла по умолчанию. Значение по умолчанию: 1.</param>
        /// <returns>Номер текущего цикла процесса согласования. Если не найден в параметрах процесса, то считается равным <paramref name="defaultValue"/>.</returns>
        /// <seealso cref="NamesKeys.ProcessCycle"/>
        public static int GetProcessCycle(IDictionary<string, object> processHash, int defaultValue = 1) =>
            WorkflowEngineHelper.Get<int?>(processHash, WorkflowBindingTypes.Process.Name, NamesKeys.ProcessCycle) ?? defaultValue;

        /// <summary>
        /// Устанавливает номер цикла процесса согласования в параметры процесса.
        /// </summary>
        /// <param name="processHash">Параметры процесса.</param>
        /// <param name="cycle">Номер цикла согласования.</param>
        /// <seealso cref="NamesKeys.ProcessCycle"/>
        public static void SetProcessCycle(IDictionary<string, object> processHash, int cycle) =>
            WorkflowEngineHelper.Set(processHash, Int32Boxes.Box(cycle), WorkflowBindingTypes.Process.Name, NamesKeys.ProcessCycle);

        /// <summary>
        /// Увеличивает номер цикла процесса согласования, содержащийся в параметрах процесса, на указанное значение.
        /// </summary>
        /// <param name="processHash">Параметры процесса.</param>
        /// <param name="value">Значение на которое увеличивается номер цикла согласования.</param>
        /// <returns>Новое значение номера цикла согласования.</returns>
        /// <remarks>Задавая в параметре <paramref name="value"/> отрицательные значения, можно реализовать декремент.</remarks>
        /// <seealso cref="NamesKeys.ProcessCycle"/>
        public static int ProcessCycleIncrement(IDictionary<string, object> processHash, int value = 1)
        {
            var newValue = GetProcessCycle(processHash) + value;
            SetProcessCycle(processHash, newValue);
            return newValue;
        }

        #endregion

        #region CurrentPerformerIndex

        /// <summary>
        /// Возвращает порядковый номер текущего исполнителя из параметров действия.
        /// </summary>
        /// <param name="actionHash">Параметры действия.</param>
        /// <param name="defaultValue">Порядковый номер текущего исполнителя по умолчанию. Значение по умолчанию: 0.</param>
        /// <returns>Порядковый номер текущего исполнителя. Если не найден в параметрах действия, то считается равным <paramref name="defaultValue"/>.</returns>
        /// <seealso cref="NamesKeys.CurrentPerformerIndex"/>
        public static int GetCurrentPerformerIndex(IDictionary<string, object> actionHash, int defaultValue = default) =>
            WorkflowEngineHelper.Get<int?>(actionHash, NamesKeys.CurrentPerformerIndex) ?? defaultValue;

        /// <summary>
        /// Устанавливает порядковый номер текущего исполнителя в параметрах действия.
        /// </summary>
        /// <param name="actionHash">Параметры действия.</param>
        /// <param name="index">Порядковый номер текущего исполнителя.</param>
        /// <seealso cref="NamesKeys.CurrentPerformerIndex"/>
        public static void SetCurrentPerformerIndex(
            IDictionary<string, object> actionHash,
            int index) =>
            WorkflowEngineHelper.Set(actionHash, Int32Boxes.Box(index), NamesKeys.CurrentPerformerIndex);

        /// <summary>
        /// Увеличивает на указанное число порядковый номер текущего исполнителя.
        /// </summary>
        /// <param name="actionHash">Параметры действия.</param>
        /// <param name="value">Значение на которое выполняется увеличение.</param>
        /// <returns>Новое значение.</returns>
        /// <seealso cref="NamesKeys.CurrentPerformerIndex"/>.
        public static int CurrentPerformerIndexIncrement(
            IDictionary<string, object> actionHash,
            int value = 1)
        {
            var newValue = GetCurrentPerformerIndex(actionHash) + value;
            SetCurrentPerformerIndex(actionHash, newValue);
            return newValue;
        }

        #endregion

        /// <summary>
        /// Возвращает персональную роль (пользователя) для роли имеющую указанный идентификатор.
        /// </summary>
        /// <param name="roleID">Идентификатор роли.</param>
        /// <param name="cardID">Идентификатор карточки, для которой требуется получить состав контекстной роли. Используется, если указанная роль является контекстной.</param>
        /// <param name="roleGetStrategy">Стратегия для получения информации о ролях.</param>
        /// <param name="contextRoleManager">Обработчик контекстных ролей.</param>
        /// <param name="validationResult">Результат валидации. Если указан, то в него будут записываться сообщения об ошибках обработки.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Кортеж содержащий: значение, показывающее, успешно ли завершена обработка и идентификатор персональной роли или значение по умолчанию для типа, если указанная роль является контекстной и не содержит участников или указанная роль имеет тип отличный от <see cref="RoleType.Personal"/> или <see cref="RoleType.Context"/>. Если роль являющаяся контекстной возвращает более одного участника, то берётся первый участник.</returns>
        public static async Task<(bool, Guid?)> TryGetPersonalRoleIDAsync(
            Guid roleID,
            Guid cardID,
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            IValidationResultBuilder validationResult = default,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(roleGetStrategy, nameof(roleGetStrategy));
            Check.ArgumentNotNull(contextRoleManager, nameof(contextRoleManager));

            var role = await roleGetStrategy.GetRoleParamsAsync(roleID, cancellationToken).ConfigureAwait(false);

            switch (role.Type)
            {
                case null:
                    validationResult?.AddError(default, "$KrActions_RoleNotFound", roleID.ToString());
                    break;

                case RoleType.Personal:
                    return (true, roleID);

                case RoleType.Context:
                    var contextRole = await contextRoleManager.GetContextRoleAsync(roleID, cancellationToken).ConfigureAwait(false);
                    var users = await contextRoleManager.GetCardContextUsersAsync(
                        contextRole,
                        cardID,
                        cancellationToken: cancellationToken).ConfigureAwait(false);

                    if (users.Any())
                    {
                        return (true, users[0].UserID);
                    }
                    validationResult?.AddError(default, "$KrActions_ContextRoleIsEmpty", role.Name);
                    break;
                default:
                    return (true, default);
            }

            return default;
        }

        /// <summary>
        /// Формирует единый список исполнителей, составленный из исполнителей указанных в настройках действия и вычисляемых исполнителей.
        /// </summary>
        /// <param name="performers">Коллекция исполнителей указанных в настройках действия. Список должен быть отсортирован в соответствии с порядком следования элементов.</param>
        /// <param name="sqlPerformers">Коллекция вычисляемых исполнителей.</param>
        /// <param name="sqlPerformerRoleID">Идентификатор роли на место  которой будет подставлен список с вычисляемыми исполнителями.</param>
        /// <returns>Единый список исполнителей.</returns>
        public static List<RoleEntryStorage> CombinePerformers(
            IEnumerable<RoleEntryStorage> performers,
            IReadOnlyCollection<RoleEntryStorage> sqlPerformers,
            Guid sqlPerformerRoleID)
        {
            Check.ArgumentNotNull(performers, nameof(performers));
            Check.ArgumentNotNull(sqlPerformers, nameof(sqlPerformers));

            const int defaultCapacity = 5;

            var result = new List<RoleEntryStorage>(defaultCapacity + sqlPerformers.Count);
            var isSqlPerformersProcessed = default(bool);

            foreach (var performer in performers)
            {
                if (performer.ID == sqlPerformerRoleID)
                {
                    foreach (var sqlPerformer in sqlPerformers)
                    {
                        AddPerformer(sqlPerformer);
                    }

                    isSqlPerformersProcessed = true;
                }
                else
                {
                    AddPerformer(performer);
                }
            }

            if (!isSqlPerformersProcessed)
            {
                foreach (var sqlPerformer in sqlPerformers)
                {
                    AddPerformer(sqlPerformer);
                }
            }

            return result;

            void AddPerformer(RoleEntryStorage performer)
            {
                result.Add(performer);
            }
        }

        /// <summary>
        /// Возвращает информацию по указанному идентификатору вида задания.
        /// </summary>
        /// <param name="viewService">Сервис представлений.</param>
        /// <param name="id">Идентификатор вида задания.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить выполнения асинхронной операции.</param>
        /// <returns>Кортеж содержащий: флаг, показывающий, что вид задания имеющий указанный идентификатор найден и название вида задания.</returns>
        public static async Task<(bool, string)> GetKindAsync(
            IViewService viewService,
            Guid id,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(viewService, nameof(viewService));

            var taskKindsView = await viewService.GetByNameAsync(KrConstants.Views.TaskKinds, cancellationToken).ConfigureAwait(false);
            var taskKindsMetadata = await taskKindsView.GetMetadataAsync(cancellationToken);

            var request = new TessaViewRequest(taskKindsMetadata);

            var idParam = new RequestParameterBuilder()
                .WithMetadata(taskKindsMetadata.Parameters.FindByName("ID"))
                .AddCriteria(new EqualsCriteriaOperator(), string.Empty, id)
                .AsRequestParameter();

            request.Values.Add(idParam);

            var result = await taskKindsView.GetDataAsync(request, cancellationToken).ConfigureAwait(false);

            if (result.Rows?.Any() != true)
            {
                return default;
            }

            var row = (IList<object>) result.Rows[0];
            return (true, (string) row[1]);
        }

        /// <summary>
        /// Возвращает значение, показывающее, что указанное задание <paramref name="task"/> из карточки <paramref name="card"/> было отправлено из Workflow Engine.
        /// </summary>
        /// <param name="task">Проверяемое задание.</param>
        /// <param name="card">Карточка содержащая проверяемое задание или значение <see langword="null"/>, если она не доступна.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Значение <see langword="true"/>, если задание было отправлено из Workflow Engine, иначе - <see langword="false"/>.</returns>
        /// <remarks>
        /// Проверка основана на том, что при отправке задания, стандартным способом, посредством метода
        /// <see cref="Tessa.Workflow.WorkflowEngineContext.SendTaskAsync(Guid, string, DateTime?, int?, Guid, string, Guid?, Guid?, Action{CardTask}, CancellationToken)"/>
        /// задаётся значение <see cref="CardTask.ProcessKind"/> равное <see cref="WorkflowEngineHelper.WorkflowEngineProcessName"/>,
        /// если задание не содержит информации об <see cref="CardTask.ProcessKind"/> и <paramref name="card"/> задан,
        /// то выполняется её поиск в истории заданий в <see cref="CardTaskHistoryItem.ProcessKind"/>,
        /// если и там отсутствует информация или не задано значение <paramref name="card"/>,
        /// то проверяется наличие подписки на задание в таблице <b>WorkflowEngineTaskSubscriptions</b>.
        /// </remarks>
        public static async Task<bool> IsWorkflowEngineTaskAsync(
            CardTask task,
            Card card,
            IDbScope dbScope,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(task, nameof(task));
            Check.ArgumentNotNull(dbScope, nameof(dbScope));

            string processKind = default;
            if (task.ProcessKind is null)
            {
                var isCheckInDb = true;
                if (card is not null)
                {
                    var historyItem = card.TryGetTaskHistory()?.FirstOrDefault(i => i.RowID == task.RowID);
                    if (historyItem is not null)
                    {
                        processKind = historyItem.Settings.TryGet<string>(TaskHistorySettingsKeys.ProcessKind);
                        isCheckInDb = false;
                    }
                }

                if (isCheckInDb)
                {
                    await using (dbScope.Create())
                    {
                        var db = dbScope.Db;
                        return await db
                            .SetCommand(
                                dbScope.BuilderFactory
                                    .Select()
                                        .V(true)
                                    .From("WorkflowEngineTaskSubscriptions").NoLock()
                                    .Where()
                                        .C("TaskID").Equals().P("TaskID")
                                    .Build(),
                                db.Parameter("TaskID", task.RowID))
                            .LogCommand()
                            .ExecuteAsync<bool>(cancellationToken);
                    }
                }
            }
            else
            {
                processKind = task.ProcessKind;
            }

            return processKind == WorkflowEngineHelper.WorkflowEngineProcessName;
        }

        /// <summary>
        /// Добавляет информацию о согласовавшем/не согласовавшем пользователе в строковую секцию <see cref="KrApprovalCommonInfo.Name"/>.
        /// </summary>
        /// <param name="sections">Словарь содержащий информацию о секциях карточки.</param>
        /// <param name="dbScope">Объект для взаимодействия с базой данных.</param>
        /// <param name="user">Пользователь завершивший задание процесса согласования.</param>
        /// <param name="task">Завершенное задание.</param>
        /// <param name="isNegativeResult">Значение <see langword="true"/>, если результат завершения задания отрицательный, иначе - <see langword="false"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async ValueTask AppendApprovalInfoUserCompleteTaskAsync(
            IDictionary<string, CardSection> sections,
            IDbScope dbScope,
            IUser user,
            CardTask task,
            bool isNegativeResult,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(sections, nameof(sections));
            Check.ArgumentNotNull(user, nameof(user));
            Check.ArgumentNotNull(task, nameof(task));

            if (sections.TryGetValue(KrApprovalCommonInfo.Name, out var section))
            {
                var fields = section.Fields;
                var targetFieldName = isNegativeResult ? KrApprovalCommonInfo.DisapprovedBy : KrApprovalCommonInfo.ApprovedBy;
                var value = fields.TryGet<string>(targetFieldName);
                var result = StringBuilderHelper.Acquire();
                if (!string.IsNullOrEmpty(value))
                {
                    result.Append(value).Append("; ");
                }

                result.Append(user.Name);

                await CardComponentHelper.FillTaskAssignedRolesAsync(
                    task,
                    dbScope,
                    cancellationToken: cancellationToken);
                await CardComponentHelper.FillTaskSessionRolesAsync(
                    task,
                    user.ID,
                    dbScope.Db,
                    dbScope.BuilderFactory,
                    cancellationToken: cancellationToken);

                // Ищем роль сотрудника. Если таких ролей несколько, берём первую по алфавиту. Если среди ролей есть его персональная роль - используем её.
                CardTaskAssignedRole role = null;
                if (task.TaskSessionRoles.Count > 0)
                {
                    foreach (var taskSessionRole in task.TaskSessionRoles)
                    {
                        if (task.TaskAssignedRoles.TryFirst(x => x.RowID == taskSessionRole.TaskRoleRowID, out var taskAssignedRole))
                        {
                            if (taskAssignedRole.RoleID == user.ID)
                            {
                                role = null;
                                break;
                            }
                            else if (role is null
                                || taskAssignedRole.RoleName?.CompareTo(role.RoleName) < 0)
                            {
                                role = taskAssignedRole;
                            }
                        }
                    }
                }

                if (role is not null)
                {
                    result.Append(" (").Append(role.RoleName).Append(')');
                }

                fields[targetFieldName] = result.ToStringAndRelease();
            }
        }

        /// <summary>
        /// Инициализирует указанный список строк, содержащий параметры обработки вариантов завершения действий, заданными вариантами завершения.
        /// </summary>
        /// <param name="completionOptions">Коллекция содержащая информацию о вариантах завершения.</param>
        /// <param name="rows">Инициализируемый список строк.</param>
        /// <param name="templateRow">Строка, используемая в качестве шаблона. Значение не изменяется.</param>
        /// <param name="optionIDs">Перечисление идентификаторов вариантов завершения.</param>
        /// <remarks>Секция не изменяется, если содержит значения.</remarks>
        public static void InializeActionCompletionOptions(
            IReadOnlyDictionary<Guid, ActionCompletionOption> completionOptions,
            ListStorage<CardRow> rows,
            CardRow templateRow,
            IList<Guid> optionIDs)
        {
            Check.ArgumentNotNull(completionOptions, nameof(completionOptions));
            Check.ArgumentNotNull(rows, nameof(rows));
            Check.ArgumentNotNull(templateRow, nameof(templateRow));
            Check.ArgumentNotNull(optionIDs, nameof(optionIDs));

            if (rows.Any())
            {
                return;
            }

            int index = default;
            foreach (var optionID in optionIDs)
            {
                var row = rows.Add();
                var newRow = templateRow.Clone();
                newRow.RowID = Guid.NewGuid();

                var fields = newRow.Fields;
                var optionCaption = completionOptions[optionID].Caption;
                fields[ActionOptionsActionBase.ActionOption + Names.Table_ID] = optionID;
                fields[ActionOptionsActionBase.ActionOption + Table_Field_Caption] = optionCaption;
                fields[ActionOptionsActionBase.Order] = Int32Boxes.Box(index++);

                row.Set(newRow);
                row.State = CardRowState.Inserted;
            }
        }

        /// <summary>
        /// Инициализирует указанный список строк, содержащий параметры обработки вариантов завершения заданий указанных типов.
        /// </summary>
        /// <param name="cardMetadata">Метаинформация, необходимая для использования типов карточек совместно с пакетом карточек.</param>
        /// <param name="rows">Инициализируемый список строк.</param>
        /// <param name="templateRow">Строка, используемая в качестве шаблона. Значение не изменяется.</param>
        /// <param name="taskTypeIDs">Перечисление идентификаторов типов заданий.</param>
        /// <param name="validationResult">Результат выполнения.</param>
        /// <param name="validationResultObject">Ссылка на объект, определяющая фактический тип объекта, имя которого используется в сообщении валидации.</param>
        /// <param name="isSetTaskTypeInfo">Значение <see langword="true"/>, если необходимо задать информацию о типе задания, иначе - <see langword="false"/>.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        /// <remarks>Секция не изменяется, если содержит значения.</remarks>
        public static async ValueTask InializeTaskCompletionOptionsAsync(
            ICardMetadata cardMetadata,
            ListStorage<CardRow> rows,
            CardRow templateRow,
            IEnumerable<Guid> taskTypeIDs,
            IValidationResultBuilder validationResult,
            object validationResultObject = default,
            bool isSetTaskTypeInfo = default,
            CancellationToken cancellationToken = default)
        {
            Check.ArgumentNotNull(cardMetadata, nameof(cardMetadata));
            Check.ArgumentNotNull(rows, nameof(rows));
            Check.ArgumentNotNull(templateRow, nameof(templateRow));
            Check.ArgumentNotNull(taskTypeIDs, nameof(taskTypeIDs));
            Check.ArgumentNotNull(validationResult, nameof(validationResult));

            if (rows.Any()
                || !taskTypeIDs.Any())
            {
                return;
            }

            var metaCompletionOptions = (await cardMetadata.GetEnumerationsAsync(cancellationToken).ConfigureAwait(false)).CompletionOptions;

            int index = default;
            foreach (var taskTypeID in taskTypeIDs)
            {
                var taskType = await CardComponentHelper.TryGetCardTypeAsync(taskTypeID, cardMetadata, cancellationToken).ConfigureAwait(false);
                if (taskType is null)
                {
                    ValidationSequence
                        .Begin(validationResult)
                        .SetObjectName(validationResultObject)
                        .Error(CardValidationKeys.UnknownCardType, taskTypeID)
                        .End();
                    return;
                }

                foreach (var completionOption in taskType.CompletionOptions)
                {
                    var optionID = completionOption.TypeID;
                    if (!metaCompletionOptions.TryGetValue(optionID, out var metaCompletionOption))
                    {
                        ValidationSequence
                            .Begin(validationResult)
                            .SetObjectName(validationResultObject)
                            .Error(CardValidationKeys.UnknownCompletionOption, optionID)
                            .End();
                        return;
                    }

                    var row = rows.Add();
                    var newRow = templateRow.Clone();
                    newRow.RowID = Guid.NewGuid();

                    var fields = newRow.Fields;
                    fields[ActionOptionsBase.Option + Names.Table_ID] = optionID;
                    fields[ActionOptionsBase.Option + Table_Field_Caption] = metaCompletionOption.Caption;

                    if (isSetTaskTypeInfo)
                    {
                        fields[ActionSeveralTaskTypesOptionsBase.TaskType + Names.Table_ID] = taskTypeID;
                        fields[ActionSeveralTaskTypesOptionsBase.TaskType + Table_Field_Name] = taskType.Name;
                        fields[ActionSeveralTaskTypesOptionsBase.TaskType + Table_Field_Caption] = taskType.Caption;
                    }
                    fields[ActionOptionsBase.Order] = Int32Boxes.Box(index++);

                    row.Set(newRow);
                    row.State = CardRowState.Inserted;
                }
            }
        }

        /// <summary>
        /// Устанавливает вид задания.
        /// </summary>
        /// <param name="cardTask">Задание в котором надо установить вид.</param>
        /// <param name="kindID">Идентификатор вида задания.</param>
        /// <param name="kindCaption">Отображаемое имя вида задания.</param>
        /// <param name="objectName">Текущий объект <c>this</c> для задания имени объекта вызвавшего ошибку. Можно также передать тип объекта, строку с именем объекта или <see langword="null"/>, если имя остаётся неизвестным.</param>
        /// <returns>Результат выполнения.</returns>
        /// <remarks>Для задания значения необходимо, что бы тип карточки задания содержал комплексную колонку TaskCommonInfo.Kind.</remarks>
        public static ValidationResult SetTaskKind(
            CardTask cardTask,
            Guid? kindID,
            string kindCaption,
            object objectName = default)
        {
            Check.ArgumentNotNull(cardTask, nameof(cardTask));

            if (kindID.HasValue)
            {
                cardTask.Info[CardHelper.TaskKindIDKey] = kindID;
                cardTask.Info[CardHelper.TaskKindCaptionKey] = kindCaption;

                if (cardTask.Card.Sections.TryGetValue(TaskCommonInfo.Name, out var taskCommonInfoSection))
                {
                    var tciFields = taskCommonInfoSection.Fields;
                    tciFields[TaskCommonInfo.KindID] = kindID;
                    tciFields[TaskCommonInfo.KindCaption] = kindCaption;
                }
                else
                {
                    return ValidationResult.FromText(objectName, "$KrActions_MissingTaskCommonInfoKind", type: ValidationResultType.Error);
                }
            }

            return ValidationResult.Empty;
        }

        /// <summary>
        /// Формирует сообщение валидации о том, что нет ни одного исполнителя.
        /// </summary>
        /// <param name="action">Действие.</param>
        /// <param name="node">Узел.</param>
        /// <param name="template">Шаблон.</param>
        /// <returns>Сформированное сообщение.</returns>
        public static string GetValidatePerformerNotSpecifiedMessage(
            WorkflowActionStorage action,
            WorkflowNodeStorage node,
            string template = "$KrActions_PerformerNotSpecified")
        {
            Check.ArgumentNotNull(action, nameof(action));
            Check.ArgumentNotNull(node, nameof(node));

            return
                LocalizationManager.Format(
                    template,
                    action.Name,
                    node.GetObjectName());
        }

        #endregion
    }
}
