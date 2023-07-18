using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Placeholders;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using Tessa.Scheme;
using Tessa.Workflow;
using Tessa.Workflow.Helpful;

using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;
using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    /// <summary>
    /// Предоставляет методы расширения для <see cref="IWorkflowEngineContext"/>.
    /// </summary>
    public static class WorkflowEngineContextExtension
    {
        /// <summary>
        /// Возвращает карточку основного сателлита <see cref="DefaultCardTypes.KrSatelliteTypeID"/>.
        /// </summary>
        /// <param name="context">Контекст обработки процесса.</param>
        /// <returns>Карточка сателлита или значение <see langword="null"/>, если произошла ошибка.</returns>
        /// <remarks>Если карточка сателлита не существует, то она автоматически создаётся.</remarks>
        public static async ValueTask<Card> GetKrSatelliteAsync(
            this IWorkflowEngineContext context)
        {
            Check.ArgumentNotNull(context, nameof(context));

            return await context.GetMainCardSatelliteAsync(
                DefaultCardTypes.KrSatelliteTypeID);
        }

        /// <summary>
        /// Асинхронно отправляет задание доработки автором (<see cref="DefaultTaskTypes.KrEditInterjectTypeID"/>). Параметры задания берутся из секции <see cref="KrWeEditInterjectOptionsVirtual.SectionName"/>.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="placeholderManager">Объект, управляющий операциями с плейсхолдерами.</param>
        /// <param name="roleGetStrategy">Стратегия для получения информации о ролях.</param>
        /// <param name="contextRoleManager">Обработчик контекстных ролей.</param>
        /// <param name="roleIDDefaultValue">Значение по умолчанию для параметра "Роль".</param>
        /// <param name="periodInDaysDefaultValue">Значение по умолчанию для параметра "Длительность, рабочие дни". Данное значение используется только, если в схеме не указано значение по умолчанию для поля <see cref="KrWeEditInterjectOptionsVirtual.Period"/>.</param>
        /// <returns>Задание доработки автором или значение по умолчанию для типа, если при отправке задания произошла ошибка.</returns>
        public static async Task<CardTask> SendEditInterjectTaskAsync(
            this IWorkflowEngineContext context,
            IPlaceholderManager placeholderManager,
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            Guid roleIDDefaultValue,
            double periodInDaysDefaultValue)
        {
            Check.ArgumentNotNull(context, nameof(context));
            Check.ArgumentNotNull(placeholderManager, nameof(placeholderManager));
            Check.ArgumentNotNull(roleGetStrategy, nameof(roleGetStrategy));

            var performerID = await context.GetAsync<Guid?>(
                KrWeEditInterjectOptionsVirtual.SectionName,
                KrWeEditInterjectOptionsVirtual.Role,
                Names.Table_ID) ?? roleIDDefaultValue;
            var performerName = await context.GetAsync<string>(
                KrWeEditInterjectOptionsVirtual.SectionName,
                KrWeEditInterjectOptionsVirtual.Role,
                Table_Field_Name);

            var digest = await context.GetAsync<string>(
                KrWeEditInterjectOptionsVirtual.SectionName,
                KrWeEditInterjectOptionsVirtual.Digest);
            var periodInDays = (double?) (await context.GetAsync<object>(
                KrWeEditInterjectOptionsVirtual.SectionName,
                KrWeEditInterjectOptionsVirtual.Period)
                ?? (await context.CardMetadata.GetSectionsAsync(context.CancellationToken))[KrWeEditInterjectOptionsVirtual.SectionName]
                    .Columns[KrWeEditInterjectOptionsVirtual.Period]
                    .DefaultValue)
                ?? periodInDaysDefaultValue;
            var planned = await context.GetAsync<DateTime?>(
                KrWeEditInterjectOptionsVirtual.SectionName,
                KrWeEditInterjectOptionsVirtual.Planned);
            var authorID = await context.GetAsync<Guid?>(
                KrWeEditInterjectOptionsVirtual.SectionName,
                KrWeEditInterjectOptionsVirtual.Author,
                Names.Table_ID);

            var kindID = await context.GetAsync<Guid?>(
                KrWeEditInterjectOptionsVirtual.SectionName,
                KrWeEditInterjectOptionsVirtual.Kind,
                Names.Table_ID);
            var kindCaption = await context.GetAsync<string>(
                KrWeEditInterjectOptionsVirtual.SectionName,
                KrWeEditInterjectOptionsVirtual.Kind,
                Table_Field_Caption);

            digest = await placeholderManager.ReplaceTextAsync(
                digest,
                context.Session,
                context.Container,
                context.DbScope,
                default,
                cardID: context.ProcessInstance.CardID,
                task: context.Task,
                info: context.CreatePlaceholderInfo(),
                withScripts: true,
                cancellationToken: context.CancellationToken);

            var cardTask =
                await context.SendTaskAsync(
                    DefaultTaskTypes.KrEditInterjectTypeID,
                    digest,
                    planned,
                    null,
                    periodInDays,
                    performerID,
                    performerName,
                    cancellationToken: context.CancellationToken);

            if (cardTask is null
                || !context.ValidationResult.IsSuccessful())
            {
                return default;
            }

            if (kindID.HasValue)
            {
                cardTask.Info[CardHelper.TaskKindIDKey] = kindID;
                cardTask.Info[CardHelper.TaskKindCaptionKey] = kindCaption;
            }

            authorID = await context.GetAuthorIDAsync(roleGetStrategy, contextRoleManager, authorID);
            if (authorID.HasValue)
            {
                cardTask.AddRole(
                    authorID.Value,
                    null,
                    CardFunctionRoles.AuthorID);
                return cardTask;
            }

            return default;
        }

        /// <summary>
        /// Возвращает идентификатор роли автора задания.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="roleGetStrategy">Стратегия для получения информации о ролях.</param>
        /// <param name="contextRoleManager">Обработчик контекстных ролей.</param>
        /// <param name="roleID">Идентификатор роли заданный в параметрах действия.</param>
        /// <returns>Идентификатор роли автора задания или значение по умолчанию для типа, если при обработке возникли ошибки.</returns>
        public static async Task<Guid?> GetAuthorIDAsync(
            this IWorkflowEngineContext context,
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager,
            Guid? roleID)
        {
            Check.ArgumentNotNull(context, nameof(context));
            Check.ArgumentNotNull(roleGetStrategy, nameof(roleGetStrategy));

            if (roleID.HasValue)
            {
                (bool isSuccessful, Guid? resultRoleID) =
                    await WorkflowHelper.TryGetPersonalRoleIDAsync(
                        roleID.Value,
                        context.ProcessInstance.CardID,
                        roleGetStrategy,
                        contextRoleManager,
                        context.ValidationResult,
                        context.CancellationToken);

                if (isSuccessful)
                {
                    if (resultRoleID.HasValue)
                    {
                        return resultRoleID;
                    }

                    context.ValidationResult.AddError("$KrActions_OnlyPersonalOrContextRolesSetAsAuthor");
                }
            }
            else
            {
                var sCard = await context.GetKrSatelliteAsync();

                if (sCard is not null)
                {
                    return sCard.GetApprovalInfoSection().Fields.TryGet<Guid?>(KrApprovalCommonInfo.AuthorID) ?? context.Session.User.ID;
                }
            }

            return default;
        }

        /// <summary>
        /// Добавляет указанный идентификатор задания в список активных заданий.
        /// </summary>
        /// <param name="context">Контекст обработки процесса.</param>
        /// <param name="taskID">Идентификатор задания.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task AddActiveTaskAsync(
            this IWorkflowEngineContext context,
            Guid taskID)
        {
            ThrowIfNull(context);

            var sCard = await context.GetKrSatelliteAsync();

            if (sCard is null)
            {
                return;
            }

            var activeTasksSection = sCard.GetActiveTasksSection();

            if (activeTasksSection.Rows.Any(p => taskID.Equals(p.Fields[KrConstants.KrActiveTasks.TaskID])))
            {
                throw new InvalidOperationException($"Task with id \"{taskID:B}\" is already active.");
            }

            var row = activeTasksSection.Rows.Add();
            row.State = CardRowState.Inserted;
            row.RowID = Guid.NewGuid();
            row.Fields[KrConstants.KrActiveTasks.TaskID] = taskID;
        }

        /// <summary>
        /// Удаляет указанный идентификатор задания из списка активных заданий.
        /// </summary>
        /// <param name="context">Контекст обработки процесса.</param>
        /// <param name="taskID">Идентификатор задания.</param>
        /// <returns>Значение <see langword="true"/>, если идентификатор задания успешно удалён из списка активных заданий, иначе - <see langword="false"/>.</returns>
        public static async ValueTask<bool> TryRemoveActiveTaskAsync(
            this IWorkflowEngineContext context,
            Guid taskID)
        {
            ThrowIfNull(context);

            var sCard = await context.GetKrSatelliteAsync();

            if (sCard is null)
            {
                return false;
            }

            var activeTasksSection = sCard.GetActiveTasksSection();
            var activeTaskRow = activeTasksSection.Rows.FirstOrDefault(p => taskID.Equals(p.Fields[KrConstants.KrActiveTasks.TaskID]));

            switch (activeTaskRow?.State)
            {
                case null:
                case CardRowState.Deleted:
                    return false;
                case CardRowState.Inserted:
                    activeTasksSection.Rows.Remove(activeTaskRow);
                    break;
                case CardRowState.Modified:
                case CardRowState.None:
                    activeTaskRow.State = CardRowState.Deleted;
                    break;
            }

            return true;
        }

        /// <summary>
        /// Возвращает доступную только для чтения коллекцию идентификаторов активных заданий.
        /// </summary>
        /// <param name="context">Контекст обработки процесса.</param>
        /// <returns>Доступная только для чтения коллекция идентификаторов активных заданий.</returns>
        public static async ValueTask<ReadOnlyCollection<Guid>> GetActiveTasksAsync(
            this IWorkflowEngineContext context)
        {
            ThrowIfNull(context);

            var sCard = await context.GetKrSatelliteAsync();

            if (sCard is null)
            {
                return EmptyHolder<Guid>.Collection;
            }

            return sCard.GetActiveTasksSection()
                .Rows
                .Select(p => p.Get<Guid>(KrConstants.KrActiveTasks.TaskID))
                .ToList()
                .AsReadOnly();
        }

        /// <summary>
        /// Добавляет в историю процесса запись о задании.
        /// </summary>
        /// <param name="context">Контекст обработки процесса в WorkflowEngine.</param>
        /// <param name="taskRowID">Идентификатор задания.</param>
        /// <param name="cycle">Номер цикла согласования.</param>
        /// <param name="isAdvisory">Значение <see langword="true"/>, если задание является рекомендательным, иначе - <see langword="false"/>.</param>
        /// <returns>Асинхронная задача.</returns>
        public static async Task AddToHistoryAsync(
            this IWorkflowEngineContext context,
            Guid taskRowID,
            int cycle,
            bool isAdvisory = default)
            => (await NotNullOrThrow(context).GetKrSatelliteAsync())?.AddToHistory(taskRowID, cycle: cycle, advisory: isAdvisory);
    }
}
