using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Caching;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.UserAPI;
using Tessa.Extensions.Default.Server.Workflow.KrObjectModel;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Serialization;
using Tessa.Extensions.Default.Shared.Workflow.KrCompilers;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    public static class HandlerHelper
    {
        private const string OverridenTaskHistoryGroup = CardHelper.SystemKeyPrefix + nameof(OverridenTaskHistoryGroup);

        /// <summary>
        /// Возвращает идентификатор текущей группы истории заданий из <see cref="Stage.InfoStorage"/> указанного этапа.
        /// </summary>
        /// <param name="stage">Этап из которого требуется получить идентификатор текущей группы истории заданий.</param>
        /// <param name="taskHistoryGroupID">Возвращаемое значение. Сохранённый в <see cref="Stage.InfoStorage"/> идентификатор текущей группы истории заданий или значение <see langword="null"/>, если он не найден.</param>
        /// <returns>Значение <see langword="true"/>, если идентификатор текущей группы истории заданий найден в <see cref="Stage.InfoStorage"/>, иначе - <see langword="false"/>.</returns>
        public static bool TryGetOverridenTaskHistoryGroup(Stage stage, out Guid? taskHistoryGroupID)
        {
            taskHistoryGroupID = null;
            if (stage is not null
                && stage.InfoStorage.TryGetValue(OverridenTaskHistoryGroup, out var idObj))
            {
                if (idObj is Guid id)
                {
                    taskHistoryGroupID = id;
                }
                return true;
            }
            return false;
        }

        /// <summary>
        /// Удаляет из <see cref="Stage.InfoStorage"/> информацию о ранее определённом идентификаторе текущей группы истории заданий.
        /// </summary>
        /// <param name="stage">Этап в котором требуется выполнить удаление информации о ранее определённом идентификаторе текущей группы истории заданий.</param>
        public static void RemoveTaskHistoryGroupOverride(
            Stage stage)
        {
            Check.ArgumentNotNull(stage, nameof(stage));
            stage.InfoStorage.Remove(OverridenTaskHistoryGroup);
        }

        /// <summary>
        /// Возвращает идентификатор текущей группы истории заданий.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="scope">Объект содержащий информацию по подсистеме маршрутов в текущей области видимости.</param>
        /// <returns>Идентификатор текущей группы истории заданий или значение <see langword="null"/>, если при её определении произошла ошибка. Информация об ошибках записывается в <see cref="IStageTypeHandlerContext.ValidationResult"/> указанного контекста обработчика этапа.</returns>
        public static async ValueTask<Guid?> GetTaskHistoryGroupAsync(
            IStageTypeHandlerContext context,
            IKrScope scope)
        {
            Check.ArgumentNotNull(context, nameof(context));
            Check.ArgumentNotNull(context.MainCardID, nameof(context.MainCardID));
            Check.ArgumentNotNull(scope, nameof(scope));

            var stage = context.Stage;
            if (TryGetOverridenTaskHistoryGroup(stage, out var overridenID))
            {
                return overridenID;
            }

            var taskHistoryGroupID =
                stage.SettingsStorage.TryGet<Guid?>(
                    KrConstants.KrHistoryManagementStageSettingsVirtual.TaskHistoryGroupTypeID);
            var parentTaskHistoryGroupID =
                stage.SettingsStorage.TryGet<Guid?>(
                    KrConstants.KrHistoryManagementStageSettingsVirtual.ParentTaskHistoryGroupTypeID);
            var newIteration =
                stage.SettingsStorage.TryGet<bool?>(
                    KrConstants.KrHistoryManagementStageSettingsVirtual.NewIteration);
            if (taskHistoryGroupID.HasValue)
            {
                var newGroup = await context.TaskHistoryResolver.ResolveTaskHistoryGroupAsync(
                    taskHistoryGroupID.Value,
                    parentTaskHistoryGroupID,
                    newIteration == true,
                    cancellationToken: context.CancellationToken);

                if (newGroup is null)
                {
                    return null;
                }

                var newRowID = newGroup.RowID;
                stage.InfoStorage[OverridenTaskHistoryGroup] = newRowID;
                return newRowID;
            }

            return await scope.GetCurrentHistoryGroupAsync(context.MainCardID.Value, context.ValidationResult, context.CancellationToken);
        }

        // TODO: Убрать этот костыльный метод. Данный метод в дальнейшем следует
        // заменить в местах использование на простое присовение значения
        // свойству Result для CardTask. Но на текущий момент сохранение задания
        // происходит до начала этапа и значение не попадает в базу данных.
        public static async Task SetTaskResultAsync(IStageTypeHandlerContext context, CardTask task, string value)
        {
            Check.ArgumentNotNull(context, nameof(context));
            Check.ArgumentNotNull(task, nameof(task));

            var storeContext = (ICardStoreExtensionContext) context.CardExtensionContext;
            var scope = storeContext.DbScope;
            await using (scope.Create())
            {
                await scope.Db
                    .SetCommand(
                        scope.BuilderFactory
                            .Update("TaskHistory").C("Result").Assign().P("Result")
                            .Where().C("RowID").Equals().P("RowID")
                            .Build(),
                        scope.Db.Parameter("RowID", task.RowID),
                        scope.Db.Parameter("Result", value))
                    .ExecuteNonQueryAsync(context.CancellationToken);
            }

            task.Result = value;
        }

        /// <summary>
        /// Возвращает автора текущего этапа.
        /// </summary>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <param name="roleGetStrategy">Стратегия для получения информации о ролях.</param>
        /// <param name="contextRoleManager">Обработчик контекстных ролей.</param>
        /// <param name="session">Сессия пользователя.</param>
        /// <returns>Автора текущего этапа.</returns>
        public static async Task<Author> GetStageAuthorAsync(
            IStageTypeHandlerContext context,
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            ISession session)
        {
            Check.ArgumentNotNull(context, nameof(context));
            Check.ArgumentNotNull(roleGetStrategy, nameof(roleGetStrategy));
            Check.ArgumentNotNull(contextRoleManager, nameof(contextRoleManager));
            Check.ArgumentNotNull(session, nameof(session));

            var initiator = context.WorkflowProcess.Author;
            var overridenAuthor = context.Stage.Author;

            if (overridenAuthor is not null)
            {
                var authorID = overridenAuthor.AuthorID;

                var role = await roleGetStrategy.GetRoleParamsAsync(authorID, context.CancellationToken);
                if (role.Type is null)
                {
                    context.ValidationResult.AddError("$KrProcess_ErrorMessage_AuthorRoleIsntFound");
                    return null;
                }

                switch (role.Type)
                {
                    case RoleType.Personal:
                        return overridenAuthor;

                    case RoleType.Context:
                        var mainCardID = context.MainCardID;
                        if (!mainCardID.HasValue)
                        {
                            context.ValidationResult.AddError("$KrProcess_ErrorMessage_ContextRoleRequiresCard");
                            return null;
                        }

                        var contextRole = await contextRoleManager.GetContextRoleAsync(authorID, context.CancellationToken);

                        var users = await contextRoleManager.GetCardContextUsersAsync(contextRole, mainCardID.Value, cancellationToken: context.CancellationToken);
                        if (users.Count > 0)
                        {
                            return new Author(users[0].UserID, users[0].UserName);
                        }
                        context.ValidationResult.AddError("$KrProcess_ErrorMessage_ContextRoleIsEmpty");
                        return null;

                    default:
                        context.ValidationResult.AddError("$KrProcess_ErrorMessage_OnlyPersonalAndContextRoles");
                        return null;
                }

            }
            if (initiator is not null)
            {
                return initiator;
            }
            return new Author(session.User.ID, session.User.Name);
        }

        public static (Guid?, string) GetTaskKind(IStageTypeHandlerContext context)
        {
            Check.ArgumentNotNull(context, nameof(context));

            var stage = context.Stage;
            var kindID = stage.SettingsStorage.TryGet<Guid?>(KrConstants.KrTaskKindSettingsVirtual.KindID);
            var kindCaption = stage.SettingsStorage.TryGet<string>(KrConstants.KrTaskKindSettingsVirtual.KindCaption);
            return (kindID, kindCaption);
        }

        public static void SetTaskKind(
            CardTask task,
            Guid? kindID,
            string kindCaption,
            IStageTypeHandlerContext context)
        {
            Check.ArgumentNotNull(task, nameof(task));
            Check.ArgumentNotNull(context, nameof(context));

            if (kindID.HasValue
                && kindCaption is not null)
            {
                task.Info[CardHelper.TaskKindIDKey] = kindID;
                task.Info[CardHelper.TaskKindCaptionKey] = kindCaption;

                if (task.Card.Sections.TryGetValue(KrConstants.TaskCommonInfo.Name, out var tci))
                {
                    tci.Fields[KrConstants.TaskCommonInfo.KindID] = kindID;
                    tci.Fields[KrConstants.TaskCommonInfo.KindCaption] = kindCaption;
                }
                else
                {
                    context.ValidationResult.AddError(
                        nameof(HandlerHelper),
                        "$KrProcess_ErrorMessage_MissingTaskCommonInfoKind",
                        context.Stage.Name);
                }
            }
        }

        /// <summary>
        /// Инициализирует указанный объект информацией содержащейся в контексте обработчика этапа.
        /// </summary>
        /// <param name="unityContainer">Unity-контейнер.</param>
        /// <param name="instance">Инициализируемый объект.</param>
        /// <param name="context">Контекст обработчика этапа.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task InitScriptContextAsync(
            IUnityContainer unityContainer,
            IKrScript instance,
            IStageTypeHandlerContext context)
        {
            Check.ArgumentNotNull(unityContainer, nameof(unityContainer));
            Check.ArgumentNotNull(instance, nameof(instance));
            Check.ArgumentNotNull(context, nameof(context));

            var currentStage = context.Stage;
            var processCache = unityContainer.Resolve<IKrProcessCache>();

            instance.MainCardAccessStrategy = context.MainCardAccessStrategy;
            instance.CardID = context.MainCardID ?? Guid.Empty;
            instance.CardType = context.MainCardType;
            instance.DocTypeID = context.MainCardDocTypeID ?? Guid.Empty;
            if (context.KrComponents.HasValue)
            {
                instance.KrComponents = context.KrComponents.Value;
            }

            instance.WorkflowProcessInfo = context.ProcessInfo;
            instance.ProcessID = context.ProcessInfo?.ProcessID;
            instance.ProcessTypeName = context.ProcessInfo?.ProcessTypeName;
            instance.InitiationCause = context.InitiationCause;
            instance.SetContextualSatellite(context.ContextualSatellite);
            instance.ProcessHolderSatellite = context.ProcessHolderSatellite;
            instance.SecondaryProcess = context.SecondaryProcess;
            instance.CardContext = context.CardExtensionContext;
            instance.ValidationResult = context.ValidationResult;
            instance.TaskHistoryResolver = context.TaskHistoryResolver;
            instance.Session = unityContainer.Resolve<ISession>();
            instance.DbScope = unityContainer.Resolve<IDbScope>();
            instance.UnityContainer = unityContainer;
            instance.CardMetadata = unityContainer.Resolve<ICardMetadata>();
            instance.KrScope = unityContainer.Resolve<IKrScope>();
            instance.CardCache = unityContainer.Resolve<ICardCache>();
            instance.KrTypesCache = unityContainer.Resolve<IKrTypesCache>();
            instance.StageSerializer = unityContainer.Resolve<IKrStageSerializer>();

            if (currentStage.TemplateID is null
                || !(await processCache.GetAllStageTemplatesAsync(context.CancellationToken)).TryGetValue(currentStage.TemplateID.Value, out var stageTemplate))
            {
                return;
            }

            instance.StageGroupID = currentStage.StageGroupID;
            instance.StageGroupName = currentStage.StageGroupName;
            instance.StageGroupOrder = currentStage.StageGroupOrder;
            instance.TemplateID = currentStage.TemplateID ?? Guid.Empty;
            instance.TemplateName = currentStage.TemplateName;
            instance.Order = stageTemplate?.Order ?? -1;
            instance.Position = stageTemplate?.Position ?? GroupPosition.Unspecified;
            instance.CanChangeOrder = stageTemplate?.CanChangeOrder ?? true;
            instance.IsStagesReadonly = stageTemplate?.IsStagesReadonly ?? true;

            // На данном этапе нет контейнера, способного пересчитывать положения этапов.
            instance.StagesContainer = null;
            instance.WorkflowProcess = context.WorkflowProcess;
            instance.Stage = currentStage;

            // Необходимо сбросить информацию о переключении контекста
            instance.DifferentContextCardID = null;
            instance.DifferentContextWholeCurrentGroup = false;
            instance.DifferentContextProcessInfo = null;
            instance.DifferentContextSetupScriptType = null;

            instance.CancellationToken = context.CancellationToken;
        }

        /// <summary>
        /// Удаляет список завершённых заданий этапа из указанного этапа.
        /// </summary>
        /// <param name="stage">Этап из которого необходимо удалить список завершенных заданий.</param>
        /// <seealso cref="KrConstants.Keys.Tasks"/>
        public static void ClearCompletedTasks(Stage stage)
        {
            Check.ArgumentNotNull(stage, nameof(stage));

            stage.InfoStorage.Remove(KrConstants.Keys.Tasks);
        }

        /// <summary>
        /// Добавляет информацию о завершённом задании в список завершённых заданий этапа предварительно выполнив подготовку в соответствии со значением свойства этапа <see cref="Stage.WriteTaskFullInformation"/>.
        /// </summary>
        /// <param name="stage">Этап, к которому относится завершенное задание.</param>
        /// <param name="task">Завершенное задание.</param>
        /// <param name="modifyTaskAction">Метод модифицирующий задание, перед его сохранением, после выполнения стандартных действий.</param>
        /// <remarks>
        /// Подготовка задания к сохранению состоит в удаление следующей информации: информации о состояниях по которым можно было бы понять, что задание изменено, <see cref="CardTask.SectionRows"/>, <see cref="CardTask.Card"/>, <see cref="CardInfoStorageObject.Info"/>.<para/>
        /// Оригинальное задание <paramref name="task"/> не изменяется, все операции выполняются над его копией.
        /// </remarks>
        /// <seealso cref="Stage.WriteTaskFullInformation"/>
        /// <seealso cref="KrConstants.Keys.Tasks"/>
        public static void AppendToCompletedTasksWithPreparing(
            Stage stage,
            CardTask task,
            Action<CardTask> modifyTaskAction = default)
        {
            Check.ArgumentNotNull(stage, nameof(stage));
            Check.ArgumentNotNull(task, nameof(task));

            var taskStorage = StorageHelper.Clone(task.GetStorage());
            var taskCopy = new CardTask(taskStorage);
            taskCopy.RemoveChanges();

            if (!stage.WriteTaskFullInformation)
            {
                taskStorage.Remove(nameof(CardTask.SectionRows));
                taskStorage.Remove(nameof(CardTask.Card));
                taskStorage.Remove(CardInfoStorageObject.InfoKey);
            }

            modifyTaskAction?.Invoke(taskCopy);

            AppendToCompletedTasks(stage, taskCopy);
        }

        /// <summary>
        /// Добавляет информацию о завершённом задании в список завершённых заданий этапа.
        /// </summary>
        /// <param name="stage">Этап, к которому относится завершенное задание.</param>
        /// <param name="task">Завершенное задание.</param>
        /// <seealso cref="KrConstants.Keys.Tasks"/>
        public static void AppendToCompletedTasks(
            Stage stage,
            CardTask task)
        {
            Check.ArgumentNotNull(stage, nameof(stage));
            Check.ArgumentNotNull(task, nameof(task));

            var stageInfoStorage = stage.InfoStorage;
            var list = stageInfoStorage.TryGet<IList>(KrConstants.Keys.Tasks);
            if (list is null)
            {
                list = new List<object>();
                stageInfoStorage[KrConstants.Keys.Tasks] = list;
            }

            list.Add(task.GetStorage());
        }
    }
}
