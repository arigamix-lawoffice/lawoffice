#nullable enable

using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using LinqToDB.Data;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Notices;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Data;
using Tessa.Platform.Formatting;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;
using NotificationHelper = Tessa.Extensions.Default.Shared.Notices.NotificationHelper;

namespace Tessa.Extensions.Default.Server.Workflow.Wf
{
    /// <summary>
    /// Класс, реализующий логику бизнес-процессов Workflow.
    /// </summary>
    public class WfWorkflowWorker :
        WorkflowTaskWorker<WfWorkflowManager>
    {
        #region Constructors

        /// <summary>
        /// Создаёт экземпляр класса с указанием значений его свойств.
        /// </summary>
        /// <param name="manager">Объект, предоставляющий возможности для управления бизнес-процессом.</param>
        /// <param name="cardRepositoryToCreateTasks">
        /// Репозиторий для создания заданий, отправляемых по ходу бизнес-процесса.
        /// </param>
        public WfWorkflowWorker(
            WfWorkflowManager manager,
            ICardRepository cardRepositoryToCreateTasks)
            : base(manager, cardRepositoryToCreateTasks)
        {
        }

        #endregion

        #region ResolutionParameters Protected Class

        /// <summary>
        /// Параметры резолюции, содержащие информацию по номерам переходов в подпроцессе.
        /// </summary>
        protected sealed class ResolutionParameters
        {
            #region Methods

            /// <summary>
            /// Создаёт параметры задания резолюции, сохраняемые в информации по заданию <see cref="IWorkflowTaskInfo"/>.
            /// </summary>
            /// <returns>Созданные параметры задания резолюции.</returns>
            public Dictionary<string, object?> CreateTaskParameters() => new(StringComparer.Ordinal);

            #endregion
        }

        #endregion

        #region Fields

        private bool? canCheckChildResolutionDate;

        #endregion

        #region Protected Constants

        /// <summary>
        /// Переход в любом подпроцессе, завершающий подпроцесс.
        /// </summary>
        protected const int StopProcessTransition = 0;

        /// <summary>
        /// Первый переход в любом подпроцессе.
        /// </summary>
        protected const int StartProcessTransition = 1;

        /// <summary>
        /// Переход в любом подпроцессе, удаляющий текущее задание из списка в подпроцессе
        /// и завершающий подпроцесс только в том случае, если текущее задание было последним.
        /// </summary>
        protected const int RemoveTaskAndStopIfLastTransition = 2;

        /// <summary>
        /// Переход в подпроцессе резолюций, завершающий резолюцию.
        /// </summary>
        protected const int WfResolutionCompleteTransition = 3;

        /// <summary>
        /// Переход в подпроцессе резолюций, отправляющий резолюцию исполнителю.
        /// </summary>
        protected const int WfResolutionSendToPerformerTransition = 4;

        /// <summary>
        /// Переход в подпроцессе резолюций, создающий дочернюю резолюцию.
        /// </summary>
        protected const int WfResolutionCreateChildTransition = 5;

        /// <summary>
        /// Переход в подпроцессе резолюций, отзывающий или отменяющий резолюцию.
        /// </summary>
        protected const int WfResolutionRevokeOrCancelTransition = 6;

        /// <summary>
        /// Переход в подпроцессе резолюций, изменяющий дату выполнения резолюции.
        /// </summary>
        protected const int WfModifyAsAuthorTransition = 7;

        #endregion

        #region Protected Methods

        /// <summary>
        /// Отправляет задание резолюции с заданными параметрами.
        /// </summary>
        /// <param name="typeID">Тип задания.</param>
        /// <param name="processInfo">Информация по подпроцессу, в рамках которого отправляется задание.</param>
        /// <param name="parameters">
        /// Параметры создаваемой резолюции или <c>null</c>, если используются параметры по умолчанию.
        /// </param>
        /// <param name="parentResolutionTask">
        /// Родительское задание с параметрами резолюции, содержащее исполнителей создаваемого задания.
        /// Если указано, то запись в истории заданий для создаваемой резолюции становится дочерней по отношению к этому заданию.
        /// </param>
        /// <param name="roleID">
        /// Идентификатор роли, на которую отправляется резолюция,
        /// или <c>null</c>, если роль определяется автоматически.
        /// </param>
        /// <param name="roleName">
        /// Имя роли, на которую отправляется резолюция. Значение используется только в том случае,
        /// если параметр <paramref name="roleID"/> отличен от <c>null</c>.
        /// </param>
        /// <param name="flags">
        /// Флаги, определяющие способ отправки резолюции.
        /// Родительским заданием для некоторых флагов считается <paramref name="parentResolutionTask"/>.
        /// </param>
        /// <param name="rowID">
        /// Идентификатор отправляемого задания или <c>null</c>, если для задания создаётся новый идентификатор.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Информация по отправленному заданию резолюции
        /// или <c>null</c>, если отправить задание не удалось.
        /// </returns>
        protected async Task<IWorkflowTaskInfo?> SendResolutionAsync(
            Guid typeID,
            IWorkflowProcessInfo processInfo,
            WfResolutionSendingFlags flags = WfResolutionSendingFlags.None,
            ResolutionParameters? parameters = null,
            CardTask? parentResolutionTask = null,
            Guid? roleID = null,
            string? roleName = null,
            Guid? rowID = null,
            CancellationToken cancellationToken = default)
        {
            bool negativeTimeLimitsIsNotAllowed =
                flags.HasNot(WfResolutionSendingFlags.IgnoreTimeLimitRestrictions);

            Card? parentCard = null;
            StringDictionaryStorage<CardSection>? parentSections = null;
            CardSection? parentSection = null;
            Dictionary<string, object?>? parentFields = null;

            // список строк с ролями исполнителей для массовой рассылки или null, если не выполняется массовая рассылка
            // по индексу 0 расположена роль, для которой создаётся первая из резолюций (остальные создаются её клонированием)
            CardRow[]? massCreationPerformers = null;

            // здесь может быть создана временная роль для исполнителей задания,
            // поэтому выполняем этот код только после всех остальных проверок

            List<(Guid performerRoleID, string? performerRoleName)>? taskRoles;

            if (roleID.HasValue)
            {
                taskRoles = new List<(Guid performerRoleID, string? performerRoleName)> { (roleID.Value, roleName) };
            }
            else
            {
                if (parentResolutionTask is not null)
                {
                    // должен быть установлен и флаг в flags, и флаговое поле MassCreation
                    if (flags.Has(WfResolutionSendingFlags.UseMassCreation)
                        && (parentCard = parentResolutionTask.TryGetCard()) is not null
                        && (parentSections = parentCard.TryGetSections()) is not null
                        && (parentSections.TryGetValue(WfHelper.ResolutionSection, out parentSection))
                        && (parentFields = parentSection.RawFields).Get<bool>(WfHelper.ResolutionMassCreationField))
                    {
                        massCreationPerformers = this.TryGetPerformersAndCheckNotEmpty(parentResolutionTask);
                        if (massCreationPerformers is null)
                        {
                            return null;
                        }

                        CardRow firstPerformer = massCreationPerformers[0];
                        taskRoles =
                            new List<(Guid performerRoleID, string? performerRoleName)>
                            {
                                (firstPerformer.Get<Guid>(WfHelper.ResolutionPerformerRoleIDField), firstPerformer.Get<string>(WfHelper.ResolutionPerformerRoleNameField))
                            };
                    }
                    else
                    {
                        taskRoles =
                            await this.TryGetPerformerRolesAsync(parentResolutionTask, cancellationToken);

                        if (taskRoles is null)
                        {
                            return null;
                        }
                    }
                }
                else
                {
                    IUser user = this.Manager.Session.User;
                    taskRoles = new List<(Guid performerRoleID, string? performerRoleName)> { (user.ID, user.Name) };
                }
            }

            DateTime? planned = null;
            double? duration = null;

            if (flags.Has(WfResolutionSendingFlags.UseParentPlanned) && parentResolutionTask is not null)
            {
                parentCard = parentResolutionTask.TryGetCard();
                if (parentCard is not null
                    && (parentSections = parentCard.TryGetSections()) is not null
                    && parentSections.TryGetValue(WfHelper.ResolutionSection, out parentSection))
                {
                    parentFields = parentSection.RawFields;

                    duration = parentFields.Get<double?>(WfHelper.ResolutionDurationInDaysField);
                    if (!duration.HasValue)
                    {
                        DateTime? plannedValue = parentFields.Get<DateTime?>(WfHelper.ResolutionPlannedField);
                        if (plannedValue.HasValue)
                        {
                            planned = plannedValue.Value.ToUniversalTime();
                        }
                        else
                        {
                            this.Manager.ValidationResult.AddError(this, "$WfResolution_Error_ResolutionHasNoPlannedDate");
                            return null;
                        }
                    }

                    if (negativeTimeLimitsIsNotAllowed)
                    {
                        if (!duration.HasValue && planned < this.Manager.StoreDateTime)
                        {
                            TimeSpan clientUtcOffset = this.Manager.Session.ClientUtcOffset;

                            this.Manager.ValidationResult.AddError(this,
                                "$WfResolution_Error_ResolutionCantBePlannedInThePast",
                                FormattingHelper.FormatDateTimeWithoutSeconds(
                                    planned.Value + clientUtcOffset,
                                    convertToLocal: false),
                                FormattingHelper.FormatDateTimeWithoutSeconds(
                                    this.Manager.StoreDateTime + clientUtcOffset,
                                    convertToLocal: false));
                            return null;
                        }

                        if (duration is <= 0)
                        {
                            this.Manager.ValidationResult.AddError(this,
                                "$WfResolution_Error_ResolutionCantUseNegativeDuaration");
                            return null;
                        }
                    }
                }
            }

            Dictionary<string, object?>? taskParameters = parameters?.CreateTaskParameters();
            IWorkflowTaskInfo? taskInfo =
                await this.SendTaskAsync(
                    typeID,
                    processInfo,
                    null, // digest
                    cardTask =>
                    {
                        for (var i = 0; i < taskRoles.Count; i++)
                        {
                            var taskRole = taskRoles[i];
                            cardTask.AddPerformer(taskRole.performerRoleID, taskRole.performerRoleName, i == 0);
                        }
                    },
                    taskParameters,
                    rowID,
                    cancellationToken: cancellationToken);

            if (taskInfo is not null)
            {
                CardTask task = taskInfo.Task;

                DateTime? parentPlanned;
                if (flags.Has(WfResolutionSendingFlags.CreatingChildResolution)
                    && parentResolutionTask is not null
                    && await this.CanCheckChildResolutionDateAsync(parentResolutionTask, cancellationToken)
                    && (parentPlanned = parentResolutionTask.Planned).HasValue)
                {
                    task.Info.Add(
                        WfHelper.CheckSafeLimitKey,
                        (await this.Manager.GetSettingsAsync(cancellationToken)).SafeChildResolutionTimeLimit);

                    task.Info.Add(WfHelper.ParentPlannedKey, parentPlanned);
                    task.Info.Add(WfHelper.StoreDateTimeKey, this.Manager.StoreDateTime);
                }

                var newTaskRowIDList = new List<Guid> { task.RowID };
                var taskList = new List<CardTask> { task };

                task.Flags |= CardTaskFlags.CreateHistoryItem;
                task.ProcessID = processInfo.ProcessID;
                task.ProcessName = processInfo.ProcessTypeName;
                task.ProcessKind = WfHelper.ResolutionProcessName;

                if (parentResolutionTask is not null)
                {
                    // при создании одной резолюции из другой создаваемая запись в истории
                    // всегда будет дочерней по отношению к родительской резолюции

                    // а вот само задание будет "наследовать" родительское задание при отправке исполнителю,
                    // и ставить parentResolutionTask в качестве родителя только при создании дочерней резолюции

                    Guid parentTaskID = parentResolutionTask.RowID;
                    task.ParentRowID = flags.Has(WfResolutionSendingFlags.CreatingChildResolution) ? parentTaskID : parentResolutionTask.ParentRowID;
                    task.HistoryItemParentRowID = parentTaskID;

                    if (planned.HasValue)
                    {
                        task.Planned = planned;
                        task.PlannedQuants = null;
                        task.PlannedWorkingDays = null;
                    }
                    else if (duration.HasValue)
                    {
                        task.PlannedWorkingDays = duration.Value;
                        task.Planned = null;
                        task.PlannedQuants = null;
                    }

                    bool hasAuthor = false;
                    if (flags.HasAny(
                            WfResolutionSendingFlags.UseParentComment
                            | WfResolutionSendingFlags.UseParentKind
                            | WfResolutionSendingFlags.UseParentAuthor
                            | WfResolutionSendingFlags.UseParentController
                            | WfResolutionSendingFlags.UseMassCreation
                            | WfResolutionSendingFlags.CreatingChildResolution
                            | WfResolutionSendingFlags.CanRevokeChildResolutions)
                        && (parentCard ??= parentResolutionTask.TryGetCard()) is not null
                        && (parentSections ??= parentCard!.TryGetSections()) is not null
                        && (parentSection is not null || parentSections!.TryGetValue(WfHelper.ResolutionSection, out parentSection)))
                    {
                        parentFields ??= parentSection.RawFields;
                        string? parentComment = null;

                        Guid? parentUserID = parentResolutionTask.UserID;
                        string? parentUserName;
                        if (parentUserID.HasValue)
                        {
                            parentUserName = parentResolutionTask.UserName;
                        }
                        else
                        {
                            IUser currentUser = this.Manager.Session.User;
                            parentUserID = currentUser.ID;
                            parentUserName = currentUser.Name;
                        }

                        if (flags.HasAny(
                                WfResolutionSendingFlags.UseParentComment
                                | WfResolutionSendingFlags.UseParentKind
                                | WfResolutionSendingFlags.CreatingChildResolution))
                        {
                            StringDictionaryStorage<CardSection> sections = task.Card.Sections;

                            if (flags.HasAny(
                                    WfResolutionSendingFlags.UseParentComment
                                    | WfResolutionSendingFlags.CreatingChildResolution))
                            {
                                Dictionary<string, object?> createdResolutionFields = sections[WfHelper.ResolutionSection].RawFields;
                                parentComment = parentFields.Get<string>(WfHelper.ResolutionCommentField).NormalizeCommentWithLineBreaks();

                                if (flags.Has(WfResolutionSendingFlags.UseParentComment))
                                {
                                    createdResolutionFields[WfHelper.ResolutionParentCommentField] = parentComment;

                                    if (flags.HasNot(WfResolutionSendingFlags.UseMajorPerformer))
                                    {
                                        task.Digest = string.IsNullOrEmpty(parentComment)
                                            ? null
                                            : parentComment;
                                    }
                                }

                                if (flags.Has(WfResolutionSendingFlags.UseMajorPerformer))
                                {
                                    task.Digest = string.IsNullOrEmpty(parentComment)
                                        ? string.Format(
                                            await LocalizeAndEscapeFormatAsync("$WfResolution_Digest_MajorPerformerWithoutComment", cancellationToken) ?? string.Empty,
                                            parentUserName)
                                        : string.Format(
                                            await LocalizeAndEscapeFormatAsync("$WfResolution_Digest_MajorPerformer", cancellationToken) ?? string.Empty,
                                            parentUserName,
                                            parentComment);
                                }

                                if (flags.HasAny(WfResolutionSendingFlags.CreatingChildResolution | WfResolutionSendingFlags.UseMajorPerformer))
                                {
                                    // комментарий автора сразу записывается в результат задания
                                    task.Result = string.IsNullOrEmpty(parentComment)
                                        ? null
                                        : string.Format(
                                            await LocalizeAndEscapeFormatAsync("$WfResolution_Result_ParentComment", cancellationToken) ?? string.Empty,
                                            parentComment.ReplaceLineEndings());
                                }
                            }

                            if (flags.Has(WfResolutionSendingFlags.UseParentKind))
                            {
                                Dictionary<string, object?> createdCommonInfoFields = sections[WfHelper.CommonInfoSection].RawFields;
                                var kindID = parentFields[WfHelper.ResolutionKindIDField];
                                var kindCaption = parentFields[WfHelper.ResolutionKindCaptionField];
                                task.Info[CardHelper.TaskKindIDKey] = kindID;
                                task.Info[CardHelper.TaskKindCaptionKey] = kindCaption;
                                createdCommonInfoFields[WfHelper.CommonInfoKindIDField] = kindID;
                                createdCommonInfoFields[WfHelper.CommonInfoKindCaptionField] = kindCaption;
                            }
                        }

                        if (flags.Has(WfResolutionSendingFlags.UseParentAuthor))
                        {
                            Guid? authorID = parentFields.Get<Guid?>(WfHelper.ResolutionAuthorIDField);
                            if (authorID.HasValue)
                            {
                                hasAuthor = true;
                                task.AddAuthor(authorID.Value);

                                Guid? senderID = parentFields.Get<Guid?>(WfHelper.ResolutionSenderIDField);
                                task.AddRole(
                                    senderID ?? this.Manager.Session.User.ID,
                                    senderID.HasValue 
                                        ? null 
                                        : this.Manager.Session.User.Name,
                                    CardFunctionRoles.SenderID);
                            }
                        }

                        if (flags.Has(WfResolutionSendingFlags.UseParentController))
                        {
                            bool withControl = parentFields.Get<bool>(WfHelper.ResolutionWithControlField);
                            if (withControl)
                            {
                                Guid? controllerID = parentFields.Get<Guid?>(WfHelper.ResolutionControllerIDField);
                                string? controllerName;
                                if (controllerID.HasValue)
                                {
                                    controllerName = parentFields.Get<string>(WfHelper.ResolutionControllerNameField);
                                }
                                else
                                {
                                    // если поле "контролёр" не заполнено, то контролёром является пользователь,
                                    // взявший в работу родительское задание

                                    // обычно это именно тот пользователь, который завершает задание,
                                    // кроме случаев, когда задание завершает личный заместитель в роли, на которую назначено задание
                                    controllerID = parentUserID;
                                    controllerName = parentUserName;
                                }

                                WfHelper.SetController(task, controllerID, controllerName);
                            }
                        }

                        if (flags.Has(WfResolutionSendingFlags.CanRevokeChildResolutions))
                        {
                            bool revokeChildren = parentFields.Get<bool>(WfHelper.ResolutionRevokeChildrenField);
                            if (revokeChildren)
                            {
                                await this.RevokeChildResolutionsAsync(parentResolutionTask, cancellationToken: cancellationToken);
                            }
                        }

                        // создаём дополнительные резолюции, если введено более одно исполителя и стоит соответствующая галка
                        if (massCreationPerformers is not null && massCreationPerformers.Length > 1)
                        {
                            CardType? coperformerTaskType;
                            string? coperformerDigest;
                            Guid majorTaskRowID;
                            if (flags.Has(WfResolutionSendingFlags.UseMajorPerformer))
                            {
                                // если первый исполнитель - ответственный, то все дополнительные задания создаются как дочерние резолюции,
                                // для которых родительским является первое созданное задание
                                coperformerTaskType = (await this.Manager.CardMetadata.GetCardTypesAsync(cancellationToken))
                                    [DefaultTaskTypes.WfResolutionChildTypeID];

                                coperformerDigest = string.IsNullOrEmpty(parentComment)
                                    ? string.Format(
                                        await LocalizeAndEscapeFormatAsync("$WfResolution_Digest_CoperformerWithoutComment", cancellationToken) ?? string.Empty,
                                        parentUserName,
                                        taskRoles[0].performerRoleName)
                                    : string.Format(
                                        await LocalizeAndEscapeFormatAsync("$WfResolution_Digest_Coperformer", cancellationToken) ?? string.Empty,
                                        parentUserName,
                                        taskRoles[0].performerRoleName,
                                        parentComment);

                                majorTaskRowID = task.RowID;
                            }
                            else
                            {
                                coperformerTaskType = null;
                                coperformerDigest = null;
                                majorTaskRowID = Guid.Empty;
                            }

                            await CardComponentHelper.FillTaskAssignedRolesAsync(task, this.Manager.DbScope, cancellationToken: cancellationToken);

                            Dictionary<string, object?> taskStorageToClone = task.GetStorage();

                            for (int i = 1; i < massCreationPerformers.Length; i++)
                            {
                                // готовим роль исполнителя, которому будет отправлена очередная резолюция
                                CardRow performer = massCreationPerformers[i];
                                Guid cloneRoleID = performer.Get<Guid>(WfHelper.ResolutionPerformerRoleIDField);
                                string? cloneRoleName = performer.Get<string>(WfHelper.ResolutionPerformerRoleNameField);

                                // новое задание - копия первого созданного, но с другим ID и ролью исполнителя
                                Guid cloneRowID = Guid.NewGuid();
                                CardTask cloneTask = this.Manager.AddTask();

                                Dictionary<string, object?> cloneTaskStorage = cloneTask.GetStorage();
                                cloneTaskStorage.Clear();
                                StorageHelper.Merge(taskStorageToClone, cloneTaskStorage);

                                cloneTask.RowID = cloneRowID;
                                cloneTask.Card.ID = cloneRowID;
                                cloneTask.TaskAssignedRoles.RemoveAll(x => x.TaskRoleID == CardFunctionRoles.PerformerID);
                                foreach (var taskAssignedRole in cloneTask.TaskAssignedRoles)
                                {
                                    taskAssignedRole.RowID = Guid.NewGuid();
                                }

                                cloneTask.AddPerformer(cloneRoleID, cloneRoleName);

                                if (coperformerTaskType is not null)
                                {
                                    cloneTask.TypeID = coperformerTaskType.ID;
                                    cloneTask.TypeName = coperformerTaskType.Name;
                                    cloneTask.TypeCaption = coperformerTaskType.Caption;

                                    cloneTask.Digest = coperformerDigest;

                                    cloneTask.ParentRowID = majorTaskRowID;
                                    cloneTask.HistoryItemParentRowID = majorTaskRowID;

                                    if (WfHelper.TaskTypeIsResolutionWithoutControl(coperformerTaskType.ID))
                                    {
                                        WfHelper.SetController(cloneTask, null, null);
                                    }
                                }

                                if (!hasAuthor)
                                {
                                    CardTaskAssignedRole? cloneTaskParentAuthor = null;
                                    if (flags.Has(WfResolutionSendingFlags.UseParentTaskAuthorForOtherPerformers))
                                    {
                                        await CardComponentHelper.FillTaskAssignedRolesAsync(parentResolutionTask, this.Manager.DbScope, cancellationToken: cancellationToken);

                                        cloneTaskParentAuthor =
                                            parentResolutionTask.TaskAssignedRoles.FirstOrDefault(p => p.TaskRoleID == CardFunctionRoles.AuthorID && p.ParentRowID is null);
                                    }

                                    cloneTask.AddAuthor(cloneTaskParentAuthor?.RoleID ?? this.Manager.Session.User.ID);
                                    cloneTask.AddRole(
                                        this.Manager.Session.User.ID,
                                        this.Manager.Session.User.Name,
                                        CardFunctionRoles.SenderID);
                                }

                                Dictionary<string, object?>? cloneParameters =
                                    taskParameters is not null
                                        ? StorageHelper.Clone(taskParameters)
                                        : null;

                                // регистрируем задание в Workflow
                                await this.Manager.AddTaskAsync(cloneTask, processInfo, cloneParameters, cancellationToken);
                                newTaskRowIDList.Add(cloneRowID);
                                taskList.Add(cloneTask);
                            }
                        }
                    }

                    if (!hasAuthor)
                    {
                        CardTaskAssignedRole? parentAuthor = null;
                        if (flags.Has(WfResolutionSendingFlags.UseParentTaskAuthor))
                        {
                            await CardComponentHelper.FillTaskAssignedRolesAsync(parentResolutionTask, this.Manager.DbScope, cancellationToken: cancellationToken);

                            parentAuthor =
                                parentResolutionTask.TaskAssignedRoles.FirstOrDefault(p => p.TaskRoleID == CardFunctionRoles.AuthorID && p.ParentRowID is null);
                        }

                        task.AddAuthor(parentAuthor?.RoleID ?? this.Manager.Session.User.ID);

                        CardTaskAssignedRole? sender = null;
                        if (typeID == DefaultTaskTypes.WfResolutionControlTypeID)
                        {
                            sender =
                                parentResolutionTask.TaskAssignedRoles.FirstOrDefault(p =>
                                    p.TaskRoleID == CardFunctionRoles.SenderID);
                        }

                        task.AddRole(
                            sender?.RoleID ?? this.Manager.Session.User.ID,
                            sender?.RoleName ?? this.Manager.Session.User.Name,
                            CardFunctionRoles.SenderID);
                    }
                }

                if (flags.Has(WfResolutionSendingFlags.ParentResolutionIsRemoving) && parentResolutionTask is not null)
                {
                    this.RemoveTaskFromProcessInfo(processInfo, parentResolutionTask.RowID);
                }

                this.AddTaskToProcessInfo(processInfo, newTaskRowIDList);

                if (flags.Has(WfResolutionSendingFlags.SendNotification))
                {
                    await this.AddNotificationsOnCreatedTasksAsync(taskList, cancellationToken);
                }
            }

            return taskInfo;
        }


        /// <summary>
        /// Оправляет задание проекта резолюции с заданными параметрами для текущего пользователя.
        /// </summary>
        /// <param name="processInfo">Информация по подпроцессу, в рамках которого отправляется задание.</param>
        /// <param name="parameters">
        /// Параметры создаваемой резолюции или <c>null</c>, если используются параметры по умолчанию.
        /// </param>
        /// <param name="duration">
        /// Планируемая длительность в днях по бизнес-календарю для создаваемого задания проекта резолюции.
        /// Если длительность меньше, либо равна нулю, то используется стандартная длительность из настроек.
        /// </param>
        /// <param name="rowID">
        /// Идентификатор отправляемого задания или <c>null</c>, если для задания создаётся новый идентификатор.
        /// </param>
        /// <param name="groupRowID">
        /// <para>
        /// Идентификатор экземпляра группы в истории заданий, в которую добавляется запись по создаваемой задаче,
        /// или <c>null</c>, если задача добавляется без группы.
        /// </para>
        /// <para>
        /// Идентификатор используется только при указании параметра <paramref name="useSpecifiedGroup"/>, равного <c>true</c>,
        /// в противном случае запись всегда добавляется в типовую группу <see cref="DefaultTaskHistoryGroupTypes.WfResolutions"/>.
        /// </para>
        /// </param>
        /// <param name="useSpecifiedGroup">
        /// Признак того, что запись в истории заданий будет добавлена в указанную группу <paramref name="groupRowID"/>, даже если она <c>null</c>.
        /// Если в параметре указано <c>false</c>, то добавление всегда производится в типовую группу
        /// <see cref="DefaultTaskHistoryGroupTypes.WfResolutions"/>.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Информация по отправленному заданию по проекту резолюции
        /// или <c>null</c>, если отправить задание не удалось.
        /// </returns>
        protected async Task<IWorkflowTaskInfo?> SendResolutionProjectAsync(
            IWorkflowProcessInfo processInfo,
            ResolutionParameters? parameters = null,
            double duration = 0.0,
            Guid? rowID = null,
            Guid? groupRowID = null,
            bool useSpecifiedGroup = false,
            CancellationToken cancellationToken = default)
        {
            double actualDuration = duration > 0.0
                ? duration
                : (await this.Manager.GetSettingsAsync(cancellationToken)).ResolutionProjectDuration;

            IWorkflowTaskInfo? taskInfo =
                await this.SendResolutionAsync(
                    DefaultTaskTypes.WfResolutionProjectTypeID,
                    processInfo,
                    WfHelper.ResolutionProjectFlags,
                    parameters,
                    rowID: rowID,
                    cancellationToken: cancellationToken);

            if (taskInfo is not null)
            {
                taskInfo.Task.PlannedWorkingDays = actualDuration;
                taskInfo.Task.GroupRowID = useSpecifiedGroup
                    ? groupRowID
                    : (await this.ResolveTaskHistoryGroupAsync(
                        DefaultTaskHistoryGroupTypes.WfResolutions, cancellationToken: cancellationToken))?.RowID;
                taskInfo.Task.AddAuthor(this.Manager.Session.User.ID);
            }

            return taskInfo;
        }


        /// <summary>
        /// Отправляет задание на контроль исполнения резолюции с заданными параметрами.
        /// </summary>
        /// <param name="completedTask">
        /// Последнее завершённое задание, в результате которого требуется отправить контроль исполнения.
        /// </param>
        /// <param name="processInfo">Информация по подпроцессу, в рамках которого отправляется задание.</param>
        /// <param name="parameters">
        /// Параметры создаваемой резолюции или <c>null</c>, если используются параметры по умолчанию.
        /// </param>
        /// <param name="duration">
        /// Планируемая длительность в днях по бизнес-календарю для создаваемого задания контроля исполнения.
        /// Если длительность меньше, либо равна нулю, то используется стандартная длительность из настроек.
        /// </param>
        /// <param name="rootTaskRowID">
        /// Идентификатор задания, относительно которого выполняется поиск ролей для возврата на контроль,
        /// или <c>null</c>, если используется идентификатор задания <paramref name="completedTask"/>.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Информация по отправленному заданию по контролю исполнения резолюции
        /// или <c>null</c>, если контроль исполнения не требуется или отправить задание не удалось.
        /// </returns>
        protected async Task<IWorkflowTaskInfo?> SendResolutionControlAsync(
            CardTask completedTask,
            IWorkflowProcessInfo processInfo,
            ResolutionParameters? parameters = null,
            double duration = 0.0,
            Guid? rootTaskRowID = null,
            CancellationToken cancellationToken = default)
        {
            if (WfHelper.TaskTypeIsResolutionWithoutControl(completedTask.TypeID))
            {
                // если бы мы начали искать контроль исполнения для дочерней резолюции,
                // которую отправили с контролем, то гарантированно нашли бы её же, что нельзя делать
                return null;
            }

            Guid completedTaskRowID = completedTask.RowID;

            IDbScope dbScope = this.Manager.DbScope;
            await using (dbScope.Create())
            {
                var db = dbScope.Db;
                var executor = dbScope.Executor;
                var builderFactory = dbScope.BuilderFactory;

                (bool hasControl, Guid? controllableTaskRowID, Guid? controllerID, string? controllerName) =
                    await TryGetTaskToControlAsync(rootTaskRowID ?? completedTaskRowID, db, builderFactory, cancellationToken);

                if (!hasControl || !controllableTaskRowID.HasValue || !controllerID.HasValue)
                {
                    return null;
                }

                IWorkflowTaskInfo? taskInfo =
                    await this.SendResolutionAsync(
                        DefaultTaskTypes.WfResolutionControlTypeID,
                        processInfo,
                        WfHelper.ResolutionControlFlags,
                        parameters,
                        completedTask,
                        roleID: controllerID.Value,
                        roleName: controllerName,
                        cancellationToken: cancellationToken);

                if (taskInfo is null)
                {
                    return null;
                }

                CardTask controlTask = taskInfo.Task;

                double actualDuration = duration > 0.0
                    ? duration
                    : (await this.Manager.GetSettingsAsync(cancellationToken)).ResolutionControlDuration;

                controlTask.PlannedWorkingDays = actualDuration;
                controlTask.ParentRowID = controllableTaskRowID.Value;
                controlTask.HistoryItemParentRowID = completedTaskRowID;

                Card? completedTaskCard;
                StringDictionaryStorage<CardSection>? completedTaskSections;
                if ((completedTaskCard = completedTask.TryGetCard()) is not null
                    && (completedTaskSections = completedTaskCard.TryGetSections()) is not null
                    && completedTaskSections.TryGetValue(WfHelper.ResolutionSection, out CardSection? completedTaskSection))
                {
                    string parentComment = completedTaskSection.RawFields.Get<string>(WfHelper.ResolutionCommentField).NormalizeComment();
                    string userName = this.Manager.Session.User.Name;

                    controlTask.Digest = string.IsNullOrEmpty(parentComment)
                        ? string.Format(await LocalizeAndEscapeFormatAsync("$WfResolution_Digest_ControlWithoutComment", cancellationToken) ?? string.Empty, userName)
                        : string.Format(await LocalizeAndEscapeFormatAsync("$WfResolution_Digest_ControlWithComment", cancellationToken) ?? string.Empty, userName, parentComment);
                }

                (bool hasParameters, DateTime? planned, Guid? performerID, string? performerName) =
                    await TryGetTaskToControlParametersAsync(controllableTaskRowID.Value, db, builderFactory, cancellationToken);

                if (hasParameters)
                {
                    Card? controlTaskCard = controlTask.TryGetCard();
                    StringDictionaryStorage<CardSection>? controlTaskSections;
                    if (controlTaskCard is not null
                        && (controlTaskSections = controlTaskCard.TryGetSections()) is not null)
                    {
                        if (controlTaskSections.TryGetValue(WfHelper.ResolutionSection, out CardSection? controlResolutionSection))
                        {
                            Dictionary<string, object?> resolutionFields = controlResolutionSection.RawFields;
                            resolutionFields[WfHelper.ResolutionWithControlField] = BooleanBoxes.True;

                            if (planned.HasValue)
                            {
                                resolutionFields[WfHelper.ResolutionPlannedField] = planned;
                                resolutionFields[WfHelper.ResolutionDurationInDaysField] = null;
                            }
                        }

                        if (performerID.HasValue
                            && controlTaskSections.TryGetValue(WfHelper.ResolutionPerformersSection, out CardSection? controlPerformersSection))
                        {
                            CardRow performer = controlPerformersSection.Rows.Add();
                            performer.RowID = Guid.NewGuid();
                            performer[WfHelper.ResolutionPerformerRoleIDField] = performerID.Value;
                            performer[WfHelper.ResolutionPerformerRoleNameField] = performerName;
                            performer[WfHelper.ResolutionPerformerOrderField] = Int32Boxes.Zero;
                            performer.State = CardRowState.Inserted;
                        }
                    }
                }

                await MarkTaskAsControlledAsync(controllableTaskRowID.Value, executor, builderFactory, cancellationToken);
                return taskInfo;
            }
        }


        /// <summary>
        /// Отправляет задание на контроль исполнения резолюции с заданными параметрами
        /// для родительского задания при завершении последней дочерней резолюции
        /// или задании в цепочке отправки или контроля исполнения, где родительским заданием для цепочки
        /// является дочерняя резолюция.
        /// </summary>
        /// <param name="completedTask">
        /// Завершённое задание, в результате которого требуется отправить контроль исполнения.
        /// Если тип задания не соответствует дочернему и отсутствует дочернее задание в цепочке исполнения выше,
        /// или родительское задание отсутствует,
        /// то метод не выполняет действий и возвращает <c>null</c>.
        /// </param>
        /// <param name="processInfo">Информация по подпроцессу, в рамках которого отправляется задание.</param>
        /// <param name="parameters">
        /// Параметры создаваемой резолюции или <c>null</c>, если используются параметры по умолчанию.
        /// </param>
        /// <param name="duration">
        /// Планируемая длительность в днях по бизнес-календарю для создаваемого задания контроля исполнения.
        /// Если длительность меньше, либо равна нулю, то используется стандартная длительность из настроек.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Информация по отправленному заданию по контролю исполнения резолюции
        /// или <c>null</c>, если контроль исполнения не требуется или отправить задание не удалось.
        /// </returns>
        protected async Task<IWorkflowTaskInfo?> SendResolutionControlIfNecessaryForLastChildAsync(
            CardTask completedTask,
            IWorkflowProcessInfo processInfo,
            ResolutionParameters? parameters = null,
            int duration = 0,
            CancellationToken cancellationToken = default)
        {
            Guid childRowID = completedTask.RowID;
            Guid typeID = completedTask.TypeID;

            IDbScope dbScope = this.Manager.DbScope;
            await using (dbScope.Create())
            {
                while (true)
                {
                    Guid? parentRowID;
                    Guid authorID;
                    string authorName;
                    if (typeID == DefaultTaskTypes.WfResolutionChildTypeID)
                    {
                        parentRowID = completedTask.ParentRowID;
                    }
                    else if (typeID == DefaultTaskTypes.WfResolutionProjectTypeID)
                    {
                        return null;
                    }
                    else
                    {
                        // ищем дочернюю резолюцию среди родительских для текущей завершённой задачи
                        DbManager db1 = dbScope.Db;
                        IQueryBuilderFactory builderFactory1 = dbScope.BuilderFactory;

                        // Возвращает идентификатор первого задания типа WfChildResolution (дочерняя резолюция)
                        // по цепочке заданий, начиная с задания с идентификатором @RowID.
                        // Если задание с идентификатором @RowID является дочерней резолюцией, то его идентификатор и возвращается.
                        // Если в цепочке заданий выше @RowID нет ни одной дочерней резолюции, то запрос не возвращает строк.
                        db1
                            .SetCommand(
                                builderFactory1
                                    .With("TaskHistoryCTE", b => b
                                            .Select().P("RowID")
                                            .UnionAll()
                                            .Select().C("th", "ParentRowID")
                                            .From("TaskHistoryCTE", "t")
                                            .InnerJoin("TaskHistory", "th").NoLock()
                                            .On().C("th", "RowID").Equals().C("t", "RowID"),
                                        columnNames: new[] { "RowID" },
                                        recursive: true)
                                    .Select().Top(1)
                                    .C("th", "RowID", "ParentRowID")
                                    .From("TaskHistoryCTE", "t")
                                    .InnerJoin("TaskHistory", "th").NoLock()
                                    .On().C("th", "RowID").Equals().C("t", "RowID")
                                    .Where().C("th", "TypeID").Equals().V(DefaultTaskTypes.WfResolutionChildTypeID)
                                    .Limit(1)
                                    .Build(),
                                db1.Parameter("RowID", childRowID))
                            .LogCommand();

                        await using DbDataReader reader = await db1.ExecuteReaderAsync(cancellationToken);
                        if (!await reader.ReadAsync(cancellationToken))
                        {
                            return null;
                        }

                        childRowID = reader.GetGuid(0);
                        parentRowID = reader.GetValue<Guid?>(1);
                    }

                    if (!parentRowID.HasValue)
                    {
                        return null;
                    }

                    var db = dbScope.Db;
                    var builderFactory = dbScope.BuilderFactory;

                    await db
                        .SetCommand(
                            builderFactory
                                .Update("WfSatelliteTaskHistory")
                                .C("AliveSubtasks").Assign().C("AliveSubtasks").Substract(1)
                                .Where().C("RowID").Equals().P("ParentRowID")
                                .Build(),
                            db.Parameter("ParentRowID", parentRowID.Value))
                        .LogCommand()
                        .ExecuteNonQueryAsync(cancellationToken);

                    // Получает информацию по автору родительского задания, если завершается последнее дочернее задание,
                    // причём для родительского задания требуется выслать контроль исполнения.
                    //
                    // Родительское задание должно быть завершено, единственным незавершённым подзаданием для него
                    // должно быть заданным дочерним, а среди завершённых не должно быть заданий с типом, отличным от WfResolutionChild.
                    //
                    // В параметре @ParentRowID получает идентификатор родительского задания.
                    db
                        .SetCommand(
                            builderFactory
                                .Select()
                                .C("th", "AuthorID", "AuthorName")
                                .From("TaskHistory", "th").NoLock()
                                .InnerJoin("WfSatelliteTaskHistory", "s").NoLock()
                                .On().C("s", "RowID").Equals().C("th", "RowID")
                                .Where()
                                .C("th", "RowID").Equals().P("ParentRowID")
                                .And().C("s", "AliveSubtasks").Equals().V(0)
                                .And().NotExists(b => b
                                    .Select().V(null)
                                    .From("TaskHistory", "th2").NoLock()
                                    .Where()
                                    .C("th2", "ParentRowID").Equals().P("ParentRowID")
                                    .And().C("th2", "TypeID").NotEquals().V(DefaultTaskTypes.WfResolutionChildTypeID))
                                .Build(),
                            db.Parameter("ParentRowID", parentRowID.Value),
                            db.Parameter("AuthorRoleID", CardFunctionRoles.AuthorID))
                        .LogCommand();

                    await using (DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken))
                    {
                        if (!await reader.ReadAsync(cancellationToken))
                        {
                            return null;
                        }

                        authorID = reader.GetGuid(0);
                        authorName = reader.GetString(1);
                    }

                    // исправляем задание, относительно которого считается контроль исполнения,
                    // как дочернее для того же родительского parentRowID, у которого есть контролёр

                    Guid? actualChildRowID = await db
                        .SetCommand(
                            builderFactory
                                .Select().Top(1)
                                .C("th", "RowID")
                                .From("TaskHistory", "th").NoLock()
                                .InnerJoin("WfSatelliteTaskHistory", "s").NoLock()
                                .On().C("s", "RowID").Equals().C("th", "RowID")
                                .Where()
                                .C("th", "ParentRowID").Equals().P("ParentRowID")
                                .And().C("s", "Controlled").Equals().V(false)
                                .Limit(1)
                                .Build(),
                            db.Parameter("ParentRowID", parentRowID.Value))
                        .LogCommand()
                        .ExecuteAsync<Guid?>(cancellationToken);

                    // если таких нет вообще, то контроль может быть в горизонтальной цепочке выше
                    if (actualChildRowID.HasValue)
                    {
                        childRowID = actualChildRowID.Value;
                    }

                    await CardComponentHelper.FillTaskAssignedRolesAsync(completedTask, dbScope, cancellationToken: cancellationToken);

                    // идентификатор задания, тип и информация по автору (кроме AuthorPosition) маскируется под родительское задание
                    // тип нужен лишь для проверки внутри SendResolutionControl
                    var pseudoParentTask =
                        new CardTask(StorageHelper.Clone(completedTask.GetStorage()))
                        {
                            RowID = parentRowID.Value,
                            TypeID = DefaultTaskTypes.WfResolutionTypeID,
                        };
                    pseudoParentTask.TaskAssignedRoles.RemoveAll(x => x.TaskRoleID == CardFunctionRoles.AuthorID);
                    foreach (var taskAssignedRole in pseudoParentTask.TaskAssignedRoles)
                    {
                        taskAssignedRole.RowID = Guid.NewGuid();
                    }

                    pseudoParentTask.AddAuthor(authorID, authorName);

                    IWorkflowTaskInfo? controlTaskInfo =
                        await this.SendResolutionControlAsync(
                            pseudoParentTask,
                            processInfo,
                            parameters,
                            duration,
                            rootTaskRowID: childRowID,
                            cancellationToken: cancellationToken);

                    if (controlTaskInfo is not null)
                    {
                        return controlTaskInfo;
                    }

                    // если задание возврата не отправлено в этой цепочке "родительская задача, завершённая отправкой на подзадачи",
                    // то может существовать другая такая же цепочка выше по дереву (если эта цепочка была вложенной);
                    // для такой вышестоящей цепочки тоже надо изменить счётчик завершения подзадач и отправить контроль, если требуется
                    typeID = DefaultTaskTypes.WfResolutionTypeID;
                    childRowID = parentRowID.Value;
                }
            }
        }


        /// <summary>
        /// Отправляет резолюцию исполнителям. Может выполнить как стандартную отправку на заданную роль,
        /// так и массовую отправку, в процессе которой на каждую из ролей исполнителей отправляется
        /// дочерняя резолюция, после чего отправляемая резолюция завершается без отзыва дочерних.
        /// </summary>
        /// <param name="taskToSend">Отправляемая резолюция.</param>
        /// <param name="processInfo">Информация по подпроцессу, в рамках которого отправляется задание.</param>
        /// <param name="parameters">
        /// Параметры создаваемой резолюции или <c>null</c>, если используются параметры по умолчанию.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task SendResolutionToPerformersAsync(
            CardTask taskToSend,
            IWorkflowProcessInfo processInfo,
            ResolutionParameters? parameters = null,
            CancellationToken cancellationToken = default)
        {
            // если стоит флаг на массовое создание и указано несколько дочерних резолюций,
            // то они создаются как подзадачи, а текущее задание завершается как Complete

            WfResolutionSendingFlags additionalFlags = WfResolutionSendingFlags.None;

            bool ignoreTimeLimitRestrictions = taskToSend.TryGetInfo()
                ?.TryGet<bool>(WfHelper.IgnoreTimeLimitRestrictionsKey) == true;

            if (ignoreTimeLimitRestrictions)
            {
                additionalFlags |= WfResolutionSendingFlags.IgnoreTimeLimitRestrictions;
            }

            StringDictionaryStorage<CardSection>? taskToSendSections;
            CardRow[] performers;
            Card? taskToSendCard = taskToSend.TryGetCard();
            if (taskToSendCard is not null
                && (taskToSendSections = taskToSendCard.TryGetSections()) is not null
                && taskToSendSections.TryGetValue(WfHelper.ResolutionSection, out CardSection? taskToSendSection)
                && taskToSendSection.RawFields.TryGet<bool>(WfHelper.ResolutionMassCreationField)
                && (performers = WfHelper.TryGetPerformers(taskToSend)) is not null
                && performers.Length > 1)
            {
                bool firstPerformerIsMajor = taskToSendSection.RawFields.TryGet<bool>(WfHelper.ResolutionMajorPerformerField);
                if (firstPerformerIsMajor)
                {
                    await this.SendResolutionAsync(
                        DefaultTaskTypes.WfResolutionTypeID,
                        processInfo,
                        WfHelper.SendResolutionToPerformerFlags
                        | additionalFlags
                        | WfResolutionSendingFlags.UseMassCreation
                        | WfResolutionSendingFlags.UseMajorPerformer
                        | WfResolutionSendingFlags.UseParentTaskAuthor
                        | WfResolutionSendingFlags.UseParentTaskAuthorForOtherPerformers,
                        parameters,
                        taskToSend,
                        cancellationToken: cancellationToken);
                }
                else
                {
                    // в дочерние резолюции прописывается контролёр из завершаемого задания и автор из поля "от имени"
                    await this.SendResolutionAsync(
                        DefaultTaskTypes.WfResolutionChildTypeID,
                        processInfo,
                        WfHelper.CreateChildResolutionFlags
                        | additionalFlags
                        | WfResolutionSendingFlags.UseParentAuthor
                        | WfResolutionSendingFlags.UseParentController
                        | WfResolutionSendingFlags.UseParentTaskAuthor
                        | WfResolutionSendingFlags.UseParentTaskAuthorForOtherPerformers,
                        parameters,
                        taskToSend,
                        cancellationToken: cancellationToken);

                    this.RemoveTaskFromProcessInfo(processInfo, taskToSend.RowID);

                    // для родительской задачи пишем текущее количество подзадач + количество создаваемых подзадач в AliveSubtasks
                    IDbScope dbScope = this.Manager.DbScope;
                    await using (dbScope.Create())
                    {
                        var executor = dbScope.Executor;
                        var builder = dbScope.BuilderFactory
                            .Update("WfSatelliteTaskHistory")
                            .C("AliveSubtasks").Assign().P("PerformerCount").Add(b => b
                                .Select().Count()
                                .From("TaskHistory", "th").NoLock()
                                .Where()
                                .C("th", "ParentRowID").Equals().P("RowID")
                                .And().C("th", "TypeID").Equals().V(DefaultTaskTypes.WfResolutionChildTypeID))
                            .Where().C("RowID").Equals().P("RowID");

                        await executor
                            .ExecuteNonQueryAsync(
                                builder.Build(),
                                cancellationToken,
                                executor.Parameter("RowID", taskToSend.RowID),
                                executor.Parameter("PerformerCount", performers.Length));
                    }
                }
            }
            else
            {
                await this.SendResolutionAsync(
                    DefaultTaskTypes.WfResolutionTypeID,
                    processInfo,
                    WfHelper.SendResolutionToPerformerFlags | additionalFlags,
                    parameters,
                    taskToSend,
                    cancellationToken: cancellationToken);
            }
        }


        /// <summary>
        /// Выполняет переход, номер которого содержится в параметрах задания <paramref name="taskInfo"/>
        /// по ключу <paramref name="transitionKey"/>. Если номер перехода не найден или задан отрицательным
        /// числом, то действий не выполняется и возвращается <c>false</c>.
        /// </summary>
        /// <param name="transitionKey">Ключ, по которому требуется найти номер перехода в параметрах задания.</param>
        /// <param name="taskInfo">
        /// Информация по заданию, для которого выполняется переход. Содержит параметры задания с номером перехода.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если переход был выполнен;
        /// <c>false</c>, если номер перехода не найден по ключу или задан отрицательным числом.
        /// </returns>
        protected async Task<bool> RenderStepAsync(
            string transitionKey,
            IWorkflowTaskInfo taskInfo,
            CancellationToken cancellationToken = default)
        {
            int? transitionNumber = taskInfo.TaskParameters.TryGet<int?>(transitionKey);
            if (transitionNumber is >= 0)
            {
                await this.RenderStepAsync(transitionNumber.Value, taskInfo, cancellationToken);
                return true;
            }

            return false;
        }


        /// <summary>
        /// Возвращает массив строк с ролями исполнителей, используемыми при отправке резолюции,
        /// или <c>null</c>, если получить список невозможно или список пуст.
        /// Возвращаемое значение не может быть пустым массивом.
        /// Если значение пустое, то добавляется сообщение об ошибке.
        /// </summary>
        /// <param name="resolutionPerformerTask">
        /// Задание резолюции, в котором содержится информация по исполнителям.
        /// </param>
        /// <returns>
        /// Массив строк с ролями исполнителей, используемыми при отправке резолюции,
        /// или <c>null</c>, если получить список невозможно или список пуст.
        /// </returns>
        protected CardRow[]? TryGetPerformersAndCheckNotEmpty(CardTask resolutionPerformerTask)
        {
            CardRow[] actualPerformers = WfHelper.TryGetPerformers(resolutionPerformerTask);
            if (actualPerformers is null)
            {
                this.Manager.ValidationResult.AddError(this, "$WfResolution_Error_CantObtainPerformers");
                return null;
            }

            return actualPerformers;
        }


        /// <summary>
        /// Возвращает список ролей исполнителей, на которые должно быть назначено задание.
        ///
        /// Возвращает <c>false</c>, если не удалось получить роли исполнитей;
        /// в этом случае информация по произошедшей ошибке содержится в контекте бизнес-процесса
        /// <see cref="IWorkflowContext.ValidationResult"/>.
        /// </summary>
        /// <param name="resolutionPerformerTask">
        /// Задание резолюции, в котором содержится информация по исполнителям.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если роль исполнителя задания успешно получена или создана;
        /// <c>false</c>, если в карточке <paramref name="resolutionPerformerTask"/> отсутствуют исполнители
        /// или ни одна из нескольких ролей исполнителей не вернула ни одного пользователя.
        /// </returns>
        protected async ValueTask<List<(Guid roleID, string? roleName)>?> TryGetPerformerRolesAsync(
            CardTask resolutionPerformerTask,
            CancellationToken cancellationToken = default)
        {
            CardRow[]? actualPerformers = this.TryGetPerformersAndCheckNotEmpty(resolutionPerformerTask);
            if (actualPerformers is null)
            {
                return null;
            }

            // если исполнитель один, то он и является ролью, на которую назначается резолюция
            if (actualPerformers.Length == 1)
            {
                CardRow performerRow = actualPerformers[0];

                Guid roleID = performerRow.Get<Guid>(WfHelper.ResolutionPerformerRoleIDField);
                string? roleName = performerRow.Get<string>(WfHelper.ResolutionPerformerRoleNameField);

                return new List<(Guid roleID, string? roleName)> { (roleID, roleName) };
            }

            var performerTuples = new List<Tuple<Guid, string?>>(actualPerformers.Length);
            foreach (CardRow performerRow in actualPerformers)
            {
                Guid performerID = performerRow.Get<Guid>(WfHelper.ResolutionPerformerRoleIDField);
                string? performerName = performerRow.Get<string>(WfHelper.ResolutionPerformerRoleNameField);
                performerTuples.Add(Tuple.Create(performerID, performerName));
            }

            Guid cardID = this.Manager.NextRequest.Card.ID;

            (bool success, List<(Guid ID, string Name)> taskPerformersRoles) =
                await WfHelper.TryGetResolutionPerformerRoleAsync(
                    cardID,
                    performerTuples,
                    this.Manager.RoleGetStrategy,
                    this.Manager.DbScope,
                    this.Manager.ValidationResult,
                    cancellationToken: cancellationToken);

            if (taskPerformersRoles is null || taskPerformersRoles.Count == 0 || !success)
            {
                if (success)
                {
                    this.Manager.ValidationResult.AddError(this, "$WfResolution_Error_PerformerIsEmpty");
                }

                return null;
            }

            return taskPerformersRoles!;
        }


        /// <summary>
        /// Возвращает признак того, что требуется выполнить проверку на дату запланированного завершения
        /// дочерней резолюции, чтобы она не превышала дату завершения родительской резолюции более, чем на один бизнес-день.
        /// </summary>
        /// <param name="parentTask">
        /// Родительская резолюция, которая инициировала создание дочерней резолюции.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Признак того, что требуется выполнить проверку на дату запланированного завершения
        /// дочерней резолюции, чтобы она не превышала дату завершения родительской резолюции более, чем на один бизнес-день.
        /// </returns>
        protected virtual async ValueTask<bool> CanCheckChildResolutionDateAsync(CardTask parentTask, CancellationToken cancellationToken = default)
        {
            if (this.canCheckChildResolutionDate.HasValue)
            {
                return this.canCheckChildResolutionDate.Value;
            }

            // не выполняем проверку на дату дочерней резолюции, если родительской резолюцией]
            // является задание проекта резолюции или контроля исполнения;
            // у этих заданий дата запланированного завершения устанавливается системой автоматически,
            // поэтому её не следует использовать для проверок
            Guid parentTaskTypeID = parentTask.TypeID;
            if (parentTaskTypeID == DefaultTaskTypes.WfResolutionProjectTypeID
                || parentTaskTypeID == DefaultTaskTypes.WfResolutionControlTypeID)
            {
                return false;
            }

            IKrType? krType = await TryGetKrTypeAsync(
                this.Manager.KrTypesCache,
                this.Manager.Request.Card,
                this.Manager.CardType.ID,
                this.Manager.DbScope,
                cancellationToken: cancellationToken);

            bool canCheckChildResolutionDate =
                krType is null || !krType.DisableChildResolutionDateCheck;

            this.canCheckChildResolutionDate = canCheckChildResolutionDate;
            return canCheckChildResolutionDate;
        }


        /// <summary>
        /// Добавляем уведомления в очередь о созданных заданиях.
        /// </summary>
        /// <param name="taskList">Список созданных заданий.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected virtual async Task AddNotificationsOnCreatedTasksAsync(
            List<CardTask> taskList,
            CancellationToken cancellationToken = default)
        {
            ThrowIfNull(taskList);

            Guid mainCardID = this.Manager.Request.Card.ID;
            foreach (CardTask task in taskList)
            {
                this.Manager.ValidationResult.Add(
                    await this.Manager.NotificationManager.SendAsync(
                        DefaultNotifications.TaskNotification,
                        task.TaskAssignedRoles.Where(x => x.TaskRoleID == CardFunctionRoles.PerformerID).Select(x => x.RoleID).ToArray(),
                        new NotificationSendContext
                        {
                            MainCardID = mainCardID,
                            Info = NotificationHelper.GetInfoWithTask(task),
                            ModifyEmailActionAsync = async (email, ct) =>
                            {
                                string mobileApprovalEmail = await NotificationHelper.GetMobileApprovalEmailAsync(this.Manager.CardCache, ct);
                                NotificationHelper.ModifyEmailForMobileApprovers(email, task, mobileApprovalEmail);
                                NotificationHelper.ModifyTaskCaption(email, task);
                            },
                            GetCardFuncAsync = (_, ct) =>
                            {
                                if (KrScopeContext.HasCurrent
                                    && KrScopeContext.Current.Cards.TryGetValue(mainCardID, out var card))
                                {
                                    return new ValueTask<Card>(card);
                                }

                                return new ValueTask<Card?>((Card?) null);
                            },
                        },
                        cancellationToken));
            }

            this.Manager.NotifyNextRequestPending();
        }

        #endregion

        #region RevokeChildResolutions Protected Methods

        /// <summary>
        /// В следующий запрос на сохранение <see cref="IWorkflowContext.NextRequest"/>
        /// добавляет отзыв всех дочерних резолюций для заданной резолюции <paramref name="parentResolution"/>.
        /// При этом рекурсивно будут отозваны все дочерние резолюции, включая дочерние для дочерних.
        ///
        /// Возвращает признак того, что все резолюции, нуждающиеся в отзыве, будут отозваны,
        /// причём при их загрузке не возникло ошибок.
        ///
        /// Метод выполняется только в том случае, если в коллекции ChildrenResolutions
        /// есть хотя бы одно дочернее задание.
        /// </summary>
        /// <param name="parentResolution">
        /// Родительская резолюция, для которой отзываются дочерние задания,
        /// если хотя бы одна запись о незавершённом дочернем задании присутствует.
        /// </param>
        /// <param name="checkRevokeChildrenField">
        /// Признак того, что в задании <paramref name="parentResolution"/> надо проверить флаг
        /// <see cref="WfHelper.ResolutionWithControlField"/>, разрешающий завершение дочерних резолюций.
        /// Если этот параметр и проверяемый флаг равны <c>true</c>, то метод возвращает <c>true</c>,
        /// т.к. эта ситуация не является ошибкой отзыва.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если все резолюции, нуждающиеся в отзыве, будут отозваны,
        /// причём при их загрузке не возникло ошибок;
        /// <c>false</c>, если при попытке отозвать хотя бы одну резолюцию возникла ошибка.
        /// </returns>
        protected async Task<bool> RevokeChildResolutionsAsync(
            CardTask parentResolution,
            bool checkRevokeChildrenField = false,
            CancellationToken cancellationToken = default)
        {
            if (parentResolution is null)
            {
                throw new ArgumentNullException(nameof(parentResolution));
            }

            // если в задании parentResolution есть хотя бы одна строка с незавершённым заданием,
            // то выполняем метод RevokeChildResolutions(Guid) и возвращаем его результат;
            // иначе отзыв дочерних заданий не требуется (т.е. он выполнен успешно) - возвращаем true

            Card? taskCard = parentResolution.TryGetCard();
            StringDictionaryStorage<CardSection>? taskSections;
            ListStorage<CardRow>? childrenRows;
            return taskCard is null
                || (taskSections = taskCard.TryGetSections()) is null
                || !taskSections.TryGetValue(WfHelper.ResolutionChildrenSection, out CardSection? childrenSection)
                || (childrenRows = childrenSection.TryGetRows()) is null
                || childrenRows.Count == 0
                || childrenRows.All(x => x.Get<object>(WfHelper.ResolutionChildrenCompletedField) is not null)
                || (checkRevokeChildrenField
                    && (!taskSections.TryGetValue(WfHelper.ResolutionSection, out CardSection? resolutionSection)
                        || !resolutionSection.RawFields.TryGet<bool>(WfHelper.ResolutionRevokeChildrenField)))
                || await this.RevokeChildResolutionsAsync(parentResolution.RowID, cancellationToken);
        }


        /// <summary>
        /// В следующий запрос на сохранение <see cref="IWorkflowContext.NextRequest"/>
        /// добавляет отзыв всех дочерних резолюций для резолюции с заданным идентификатором.
        /// При этом рекурсивно будут отозваны все дочерние резолюции, включая дочерние для дочерних.
        ///
        /// Возвращает признак того, что все резолюции, нуждающиеся в отзыве, будут отозваны,
        /// причём при их загрузке не возникло ошибок.
        /// </summary>
        /// <param name="parentResolutionID">Идентификатор родительской резолюции.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если все резолюции, нуждающиеся в отзыве, будут отозваны,
        /// причём при их загрузке не возникло ошибок;
        /// <c>false</c>, если при попытке отозвать хотя бы одну резолюцию возникла ошибка.
        /// </returns>
        protected async Task<bool> RevokeChildResolutionsAsync(Guid parentResolutionID, CancellationToken cancellationToken = default)
        {
            await using (this.Manager.DbScope.Create())
            {
                var db = this.Manager.DbScope.Db;
                var builderFactory = this.Manager.DbScope.BuilderFactory;

                List<Guid> taskToRevokeRowIDList = await GetChildResolutionsToRevokeAsync(parentResolutionID, db, builderFactory, cancellationToken);
                if (taskToRevokeRowIDList.Count == 0)
                {
                    return true;
                }

                Card card = this.Manager.NextRequest.Card;
                bool success = await this.LoadTasksToRevokeAsync(card.ID, card, taskToRevokeRowIDList, db, cancellationToken);
                if (!success)
                {
                    // основная ошибка уже должна была быть записана в ValidationResult
                    // здесь мы просто резюмируем, что ошибка произошла в связи с отзывом дочерних резолюций

                    this.Manager.ValidationResult.AddError(this, "$WfResolution_Error_RevokingChildResolutions");
                }

                return success;
            }
        }


        /// <summary>
        /// Очищает поля с параметрами вариантов завершения у задачи при завершении без удаления.
        /// Используется при создании дочерних задач, чтобы очистить комментарий и прочие поля.
        /// </summary>
        /// <param name="task">Родительская задача, для которой создаётся дочерняя задача.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task ClearWasteOnNextStoreAsync(CardTask task, CancellationToken cancellationToken = default)
        {
            Card? taskCard = task.TryGetCard();
            StringDictionaryStorage<CardSection>? taskSections;

            if (taskCard is null
                || (taskSections = taskCard.TryGetSections()) is null
                || taskSections.Count == 0)
            {
                return;
            }

            CardTask modifiedTask =
                NotNullOrThrow(await this.AddNewTaskAsync(
                    task.TypeID,
                    cardTask => cardTask.TaskAssignedRoles.Clear(),
                    task.Planned,
                    task.RowID,
                    cancellationToken));

            // Нам не требуется никаких изменений исполнителей, потому очищаем данный список.
            modifiedTask.Flags = task.Flags;
            modifiedTask.StoredState = task.StoredState;
            modifiedTask.State = CardRowState.Modified;

            StringDictionaryStorage<CardSection> modifiedSections = modifiedTask.Card.Sections;

            if (modifiedSections.TryGetValue(WfHelper.ResolutionSection, out CardSection? modifiedMainSection))
            {
                // указываем только те поля, которые сбрасываются на значения по умолчанию;
                // при этом поля с датой планируемого задания нельзя сбрасывать, т.к. они через настройки могут быть настроены по-разному

                modifiedMainSection
                    .SetChanged(WfHelper.ResolutionMassCreationField)
                    .SetChanged(WfHelper.ResolutionShowAdditionalField)
                    .SetChanged(WfHelper.ResolutionKindIDField)
                    .SetChanged(WfHelper.ResolutionKindCaptionField)
                    .SetChanged(WfHelper.ResolutionCommentField)
                    ;
            }

            if (taskSections.TryGetValue(WfHelper.ResolutionPerformersSection, out CardSection? performers)
                && performers.TryGetRows() is { Count: > 0 } performersRows 
                && modifiedSections.TryGetValue(WfHelper.ResolutionPerformersSection, out CardSection? modifiedPerformers))
            {
                ListStorage<CardRow> modifiedPerformersRows = modifiedPerformers.Rows;

                foreach (CardRow performer in performersRows)
                {
                    CardRow newRow = modifiedPerformersRows.Add(performer);
                    newRow.State = CardRowState.Deleted;
                }
            }

            modifiedTask.Info[WfHelper.ResettingFieldsAfterTaskIsCompletedAsModifiedKey] = BooleanBoxes.True;
        }


        /// <summary>
        /// Указывает на необходимость завершить задание постановки задачи с указанными параметрами и значениями в секциях.
        /// </summary>
        /// <param name="taskRowID">Идентификатор постановки задачи.</param>
        /// <param name="task">
        /// Задание, в котором используются секции <c>task.Card.Sections</c> для параметров завершения
        /// и <see cref="CardTask.OptionID"/> как идентификатор варианта завершения.
        /// По умолчанию вариант завершения <see cref="DefaultCompletionOptions.SendToPerformer"/>.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task CompleteResolutionProjectOnNextStoreAsync(
            Guid taskRowID,
            CardTask task,
            CancellationToken cancellationToken = default)
        {
            IUser currentUser = this.Manager.Session.User;
            CardTask modifiedTask =
                NotNullOrThrow(await this.AddNewTaskAsync(
                    DefaultTaskTypes.WfResolutionProjectTypeID,
                    cardTask => cardTask.AddPerformer(currentUser.ID, currentUser.Name),
                    DateTime.UtcNow,
                    taskRowID,
                    cancellationToken));

            modifiedTask.StoredState = CardTaskState.Created;
            modifiedTask.Flags = task.Flags
                .SetFlag(
                    CardTaskFlags.CanPerform
                    | CardTaskFlags.HistoryItemCreated,
                    true);

            CardRowState state = task.State;
            if (state != CardRowState.Modified)
            {
                state = CardRowState.Deleted;
            }

            modifiedTask.State = state;
            modifiedTask.Action = CardTaskAction.Complete;
            modifiedTask.OptionID = task.OptionID ?? DefaultCompletionOptions.SendToPerformer;
            modifiedTask.Info[WfHelper.IgnoreTimeLimitRestrictionsKey] = BooleanBoxes.True;

            StringDictionaryStorage<CardSection> modifiedTaskSections = modifiedTask.Card.Sections;

            StringDictionaryStorage<CardSection>? sections = task.TryGetCard()?.TryGetSections();
            if (sections is not null && sections.Count > 0)
            {
                foreach (KeyValuePair<string, CardSection> pair in sections)
                {
                    if (modifiedTaskSections.TryGetValue(pair.Key, out CardSection? modifiedSection)
                        && modifiedSection.Type == pair.Value.Type)
                    {
                        if (modifiedSection.Type == CardSectionType.Entry)
                        {
                            // копируем поля строковой секции таким образом, что отсутствующие в секции task-а поля
                            // из modifiedSection сохраняют свои значения по умолчанию
                            StorageHelper.Merge(pair.Value.RawFields, modifiedSection.RawFields);
                        }
                        else
                        {
                            modifiedSection.Set(pair.Value);
                        }
                    }
                }
            }
        }


        /// <summary>
        /// Изменяет дату выполнения и комментарий задания в соответствии с заполненными пользователем данными
        /// при следующем сохранении.
        /// </summary>
        /// <param name="task">Задание, дату выполнения которого требуется изменить.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task ModifyAsAuthorOnNextStoreAsync(CardTask task, CancellationToken cancellationToken = default)
        {
            Card? taskCard = task.TryGetCard();
            StringDictionaryStorage<CardSection>? taskSections;
            DateTime? planned;
            if (taskCard is null
                || (taskSections = taskCard.TryGetSections()) is null
                || !taskSections.TryGetValue(WfHelper.ResolutionVirtualSection, out CardSection? section)
                || !(planned = section.RawFields.Get<DateTime?>(WfHelper.ResolutionVirtualPlannedField)).HasValue)
            {
                return;
            }

            CardTaskFlags flags = CardTaskFlags.None;

            // проверяем необходимость UpdateDigest
            string prevDigest = await LocalizeFormatAsync(task.Digest, cancellationToken);
            string? digest = section.RawFields.Get<string>(WfHelper.ResolutionVirtualDigestField); // уже был локализован при загрузке
            if (!string.Equals(prevDigest, digest, StringComparison.Ordinal))
            {
                flags |= CardTaskFlags.UpdateDigest;
            }

            // проверяем необходимость UpdatePlanned
            DateTime? prevPlanned = task.Planned;
            if (prevPlanned.HasValue)
            {
                prevPlanned = prevPlanned.Value.ToUniversalTime();
            }

            planned = planned.Value.ToUniversalTime(); // planned is not null всегда

            if (planned != prevPlanned)
            {
                flags |= CardTaskFlags.UpdatePlanned;
            }

            // ничего не нужно - выходим
            if (flags == CardTaskFlags.None)
            {
                return;
            }

            await CardComponentHelper.FillTaskAssignedRolesAsync(task, this.Manager.DbScope, cancellationToken: cancellationToken);

            CardTask modifiedTask =
                NotNullOrThrow(await this.AddNewTaskAsync(
                    task.TypeID,
                    _ => { },
                    planned.Value,
                    task.RowID,
                    cancellationToken));

            modifiedTask.Digest = digest;
            modifiedTask.Flags = modifiedTask.Flags
                .SetFlag(
                    flags
                    | CardTaskFlags.CanPerform
                    | CardTaskFlags.HistoryItemCreated,
                    true);
            modifiedTask.StoredState = task.StoredState;
            modifiedTask.State = CardRowState.Modified;
            modifiedTask.Action = CardTaskAction.None;
            modifiedTask.TaskAssignedRoles.AddRange(task.TaskAssignedRoles);
        }


        /// <summary>
        /// Возвращает идентификатор родительской записи в истории заданий для заданного задания <paramref name="taskRowID"/>
        /// или <c>null</c>, если родительская запись отсутствует или по заданному заданию не была создана запись в истории.
        /// </summary>
        /// <param name="taskRowID">Идентификатор задания, по которому требуется получить идентификатор родительского задания.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Идентификатор родительской записи в истории заданий для заданного задания <paramref name="taskRowID"/>
        /// или <c>null</c>, если родительская запись отсутствует или по заданному заданию не была создана запись в истории.
        /// </returns>
        protected async Task<Guid?> TryGetHistoryItemParentRowIDAsync(Guid taskRowID, CancellationToken cancellationToken = default)
        {
            await using (this.Manager.DbScope.Create())
            {
                var db = this.Manager.DbScope.Db;
                var builderFactory = this.Manager.DbScope.BuilderFactory;

                return await db
                    .SetCommand(
                        builderFactory
                            .Select().C("ParentRowID")
                            .From("TaskHistory").NoLock()
                            .Where().C("RowID").Equals().P("RowID")
                            .Build(),
                        db.Parameter("RowID", taskRowID))
                    .LogCommand()
                    .ExecuteAsync<Guid?>(cancellationToken);
            }
        }


        /// <summary>
        /// Указывает для заданного результата завершения задания информацию о том,
        /// что автор изменил задание после отправки.
        /// </summary>
        /// <param name="result">Результат завершения задания, в котором требуется указать дату изменения.</param>
        /// <returns>Исходная или изменённая строка <paramref name="result"/>.</returns>
        protected string UpdateModifiedByAuthorDate(string result)
        {
            if (string.IsNullOrEmpty(result))
            {
                return result;
            }

            // отображаем дату и время в часовом поясе текущего сотрудника;
            // дата и время разделяются неделимыми пробелами
            string modified = FormattingHelper
                .FormatDateTimeWithoutSeconds(
                    this.Manager.StoreDateTime.ToUniversalTime() + this.Manager.Session.ClientUtcOffset,
                    convertToLocal: false)
                .ReplaceSpacesToNonBreakable();

            // строка изменяется первый раз, просто дописываем информацию в конец
            if (!result.Contains("{$WfResolution_Placeholder_ModifiedByAuthor}", StringComparison.Ordinal))
            {
                return result + " ({$WfResolution_Placeholder_ModifiedByAuthor} " + modified + ")";
            }

            // строка изменяется повторно, надо заменить старую дату на новую
            return Regex.Replace(
                result,
                @"^(.+?)\s+\(\{\$WfResolution_Placeholder_ModifiedByAuthor\}.+$",
                "$1 ({$WfResolution_Placeholder_ModifiedByAuthor} " + modified + ")");
        }


        /// <summary>
        /// Изменяем подстроку <paramref name="oldSubstring"/> на строку <paramref name="newSubstring"/>
        /// в результате записи в истории завершённых заданий для задания <paramref name="taskRowID"/>.
        /// Например, можно изменить одно название роли на другое.
        ///
        /// Возвращает признак того, что запись была изменена.
        /// </summary>
        /// <param name="taskRowID">Идентификатор задания.</param>
        /// <param name="oldSubstring">
        /// Заменяемое слово. Если задано <c>null</c> или строка из пробелов, то замены на подстроку <paramref name="newSubstring"/> не происходит.
        /// </param>
        /// <param name="newSubstring">
        /// Новое слово или строка, которая записывается на место подстроки <paramref name="oldSubstring"/>.
        /// Укажите пустую строку или <c>null</c>, чтобы удалить подстроку <paramref name="oldSubstring"/>.
        /// </param>
        /// <param name="modifyStringAction">
        /// Метод, получающий на вход строку, в которой были выполнены изменения, и выполняющий опциональную обработку
        /// этой строки, например, записывающий в неё дату изменения.
        ///
        /// Значение <c>null</c> означает, что дополнительной обработки выполнено не будет.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected async Task<bool> ReplaceTaskHistorySubstringAsync(
            Guid taskRowID,
            string? oldSubstring,
            string? newSubstring,
            Func<string, string?>? modifyStringAction = null,
            CancellationToken cancellationToken = default)
        {
            oldSubstring = oldSubstring?.Trim();

            await using (this.Manager.DbScope.Create())
            {
                var db = this.Manager.DbScope.Db;
                var builderFactory = this.Manager.DbScope.BuilderFactory;

                string? oldResult = await db
                    .SetCommand(
                        builderFactory
                            .Select().C("Result")
                            .From("TaskHistory").NoLock()
                            .Where().C("RowID").Equals().P("RowID")
                            .Build(),
                        db.Parameter("RowID", taskRowID))
                    .LogCommand()
                    .ExecuteAsync<string>(cancellationToken);

                if (string.IsNullOrEmpty(oldResult))
                {
                    return false;
                }

                string newResult = string.IsNullOrEmpty(oldSubstring)
                    ? oldResult
                    : oldResult.Replace(oldSubstring, newSubstring, StringComparison.Ordinal);

                if (modifyStringAction is not null)
                {
                    newResult = modifyStringAction(newResult) ?? string.Empty;
                }

                if (string.Equals(newResult, oldResult, StringComparison.Ordinal))
                {
                    return false;
                }

                var executor = this.Manager.DbScope.Executor;

                await executor
                    .ExecuteNonQueryAsync(
                        builderFactory
                            .Update("TaskHistory").C("Result").Assign().P("Result")
                            .Where().C("RowID").Equals().P("RowID")
                            .Build(),
                        cancellationToken,
                        executor.Parameter("Result", newResult),
                        executor.Parameter("RowID", taskRowID));

                return true;
            }
        }


        /// <summary>
        /// Ключ с признаком того, что задание в процессе отзыва, который был инициирован
        /// завершением родительского задания.
        /// </summary>
        private const string RevokedByParentKey = CardHelper.SystemKeyPrefix + "revokedByParent";


        private static void SetRevokedByParent(CardTask task, bool value)
        {
            task.Info[RevokedByParentKey] = BooleanBoxes.Box(value);
        }


        private static bool GetRevokedByParent(CardTask task)
        {
            Dictionary<string, object?>? info = task.TryGetInfo();
            return info is not null && info.TryGet<bool>(RevokedByParentKey);
        }


        /// <summary>
        /// К моменту выполнения запроса родительская резолюция уже удалена,
        /// поэтому на первом уровне рекурсии надо искать дочерние резолюции
        /// в таблице Tasks, указывая, что TypeID у них от дочерней резолюции
        /// и ParentID связан с родительской резолюцией.
        /// </summary>
        /// <param name="parentResolutionID">Идентификатор родительской резолюции.</param>
        /// <param name="db">Объект для взаимодействия с базой данных.</param>
        /// <param name="builderFactory">Объект для построения запросов к БД.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        private static async Task<List<Guid>> GetChildResolutionsToRevokeAsync(
            Guid parentResolutionID,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            DataParameter? dpTypeIDs = null;
            return await db
                .SetCommand(
                    builderFactory
                        .With("TasksToRevokeCTE", b => b
                                .Select().C("t", "RowID")
                                .From("Tasks", "t").NoLock()
                                .Where().C("t", "TypeID").InArray(
                                    WfHelper.ResolutionTaskTypeIDList
                                        .Where(x => x != DefaultTaskTypes.WfResolutionProjectTypeID), "TypeIDs", out dpTypeIDs)
                                .And().C("t", "ParentID").Equals().P("ParentResolutionID")
                                .UnionAll()
                                .Select().C("c", "RowID")
                                .From("WfResolutionChildren", "c").NoLock()
                                .InnerJoin("TasksToRevokeCTE", "cte")
                                .On().C("cte", "RowID").Equals().C("c", "ID")
                                .Where().C("c", "Completed").IsNull(),
                            columnNames: new[] { "RowID" },
                            recursive: true)
                        .Select().C("RowID")
                        .From("TasksToRevokeCTE")
                        .Build(),
                    DataParameters.Get(
                        db.Parameter("ParentResolutionID", parentResolutionID),
                        dpTypeIDs))
                .LogCommand()
                .ExecuteListAsync<Guid>(cancellationToken);
        }


        private async Task<bool> LoadTasksToRevokeAsync(
            Guid cardID,
            Card card,
            List<Guid> taskToRevokeRowIDList,
            DbManager db,
            CancellationToken cancellationToken = default)
        {
            IList<CardGetContext>? taskContexts = await NotNullOrThrow(this.Manager.CardGetStrategy)
                .TryLoadTaskInstancesAsync(
                    cardID,
                    card,
                    db,
                    this.Manager.CardMetadata,
                    this.Manager.ValidationResult,
                    this.Manager.Session,
                    CardNewMode.Default,
                    CardGetTaskMode.All,
                    loadCalendarInfo: false,
                    taskRowIDList: taskToRevokeRowIDList,
                    cancellationToken: cancellationToken);

            if (taskContexts is null)
            {
                return false;
            }

            foreach (CardGetContext taskContext in taskContexts)
            {
                if (!await this.Manager.CardGetStrategy.LoadSectionsAsync(taskContext, cancellationToken))
                {
                    return false;
                }
            }

            ListStorage<CardTask>? tasks = card.TryGetTasks();
            if (tasks is not null)
            {
                foreach (CardTask task in tasks)
                {
                    if (taskToRevokeRowIDList.Contains(task.RowID))
                    {
                        task.Action = CardTaskAction.Complete;
                        task.State = CardRowState.Deleted;
                        task.OptionID = DefaultCompletionOptions.Revoke;
                        task.Flags = task.Flags
                            & ~CardTaskFlags.Locked
                            | CardTaskFlags.UnlockedForPerformer
                            | CardTaskFlags.HistoryItemCreated;

                        SetRevokedByParent(task, true);
                    }
                }
            }

            return true;
        }


        /// <summary>
        /// Получает информацию по одному ближайшему заданию, для которого надо отправить контроль исполнения.
        ///
        /// Если в цепочке есть дочерняя резолюция или проект резолюции, то не возвращаем контроль исполнения,
        /// назначенный при завершении родительского задания.
        /// В случае проекта резолюции такая ситуация всё равно невозможно, т.к. проект отправляет система.
        ///
        /// Для дочерней резолюции это важно, т.к. если дочерняя резолюция создана в результате отправки с подзадачами,
        /// то в каждую из дочерних резолюций (подзадач) был записан контролёр, которому должно возвратиться задание
        /// при завершении последней дочерней резолюции. Этот контролёр не должен учитываться в цепочке контроля,
        /// запускаемой начиная от подзадачи (когда подзадача отправляется с контролем).
        ///
        /// Задание с идентификатором @RowID может быть типа "подзадача", поэтому проверка выполняется только для ParentRowID
        /// в CTE на родительские задания.
        ///
        /// В параметре @RowID получает идентификатор последнего завершённого задания.
        /// </summary>
        private static async Task<(bool hasValue, Guid? controllableTaskRowID, Guid? controllerID, string? controllerName)> TryGetTaskToControlAsync(
            Guid completedTaskRowID,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            db
                .SetCommand(
                    builderFactory
                        .With("TaskHistoryCTE", b => b
                                .Select().P("RowID")
                                .UnionAll()
                                .Select().C("th", "ParentRowID")
                                .From("TaskHistoryCTE", "t")
                                .InnerJoin("TaskHistory", "th").NoLock()
                                .On().C("th", "RowID").Equals().C("t", "RowID")
                                .InnerJoin("TaskHistory", "thp").NoLock()
                                .On().C("thp", "RowID").Equals().C("th", "ParentRowID")
                                .Where()
                                .C("th", "TypeID").NotEquals().V(DefaultTaskTypes.WfResolutionProjectTypeID)
                                .And().C("thp", "TypeID").NotEquals().V(DefaultTaskTypes.WfResolutionChildTypeID),
                            columnNames: new[] { "RowID" },
                            recursive: true)
                        .Select().Top(1)
                        .C("t", "RowID").C("sth", "ControllerID", "ControllerName")
                        .From("TaskHistoryCTE", "t")
                        .InnerJoin("WfSatelliteTaskHistory", "sth").NoLock()
                        .On().C("sth", "RowID").Equals().C("t", "RowID")
                        .Where()
                        .C("sth", "Controlled").Equals().V(false)
                        .Limit(1)
                        .Build(),
                    db.Parameter("RowID", completedTaskRowID))
                .LogCommand();

            await using (DbDataReader reader = await db.ExecuteReaderAsync(cancellationToken))
            {
                if (await reader.ReadAsync(cancellationToken))
                {
                    Guid controllableTaskRowID = reader.GetGuid(0);
                    Guid controllerID = reader.GetGuid(1);
                    string? controllerName = reader.GetValue<string>(2);
                    return (true, controllableTaskRowID, controllerID, controllerName);
                }
            }

            return (false, null, null, null);
        }


        /// <summary>
        /// Получает информацию по заданию, которое было отправлено исполнителю с контролем.
        /// Эта информация затем переносится в задание контроля исполнения.
        /// </summary>
        private static async Task<(bool hasValue, DateTime? planned, Guid? performerID, string? performerName)> TryGetTaskToControlParametersAsync(
            Guid controllableTaskRowID,
            DbManager db,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default)
        {
            db
                .SetCommand(
                    builderFactory
                        .Select().Top(1)
                        .C("th", "Planned", "UserID", "UserName", "Settings")
                        .From("TaskHistory", "th").NoLock()
                        .Where()
                        .C("RowID").Equals().P("ControllableTaskRowID")
                        .Limit(1)
                        .Build(),
                    db.Parameter("ControllableTaskRowID", controllableTaskRowID),
                    db.Parameter("PerformerRoleID", CardFunctionRoles.PerformerID))
                .LogCommand();

            await using (DbDataReader reader = await db.ExecuteReaderAsync(CommandBehavior.SequentialAccess, cancellationToken))
            {
                if (await reader.ReadAsync(cancellationToken))
                {
                    DateTime? planned = reader.GetNullableDateTimeUtc(0);
                    Guid userID = reader.GetGuid(1);
                    string? userName = reader.GetValue<string>(2);
                    string? settingsJson = reader.GetValue<string>(3);

                    var settings =
                        (string.IsNullOrEmpty(settingsJson)
                            ? null
                            : StorageHelper.DeserializeFromTypedJson(settingsJson))
                        ?? new Dictionary<string, object?>(StringComparer.Ordinal);

                    if (settings.TryGet<Guid?>(TaskHistorySettingsKeys.PerformerRoleTypeID) is not { } performerRoleTypeID ||
                        CardComponentHelper.TemporaryTaskRoleTypeIDList.Contains(performerRoleTypeID) ||
                        settings.TryGet<Guid?>(TaskHistorySettingsKeys.PerformerID) is not { } performerID ||
                        settings.TryGet<string>(TaskHistorySettingsKeys.PerformerName) is not { } performerName)
                    {
                        performerID = userID;
                        performerName = userName;
                    }

                    return (true, planned, performerID, performerName);
                }
            }

            return (false, null, null, null);
        }

        private static Task MarkTaskAsControlledAsync(
            Guid controllableTaskRowID,
            IQueryExecutor executor,
            IQueryBuilderFactory builderFactory,
            CancellationToken cancellationToken = default) =>
            executor
                .ExecuteNonQueryAsync(
                    builderFactory
                        .Update("WfSatelliteTaskHistory")
                        .C("Controlled").Assign().V(true)
                        .From("TaskHistory", "th").NoLock()
                        .InnerJoin("TaskHistory", "thc").NoLock()
                        .On().C("thc", "ParentRowID").Equals().C("th", "ParentRowID")
                        .Where()
                        .C("WfSatelliteTaskHistory", "RowID").Equals().C("thc", "RowID")
                        .And().C("th", "RowID").Equals().P("RowID")
                        .Build(),
                    cancellationToken,
                    executor.Parameter("RowID", controllableTaskRowID));


        /// <summary>
        /// Возвращает эффективные настройки для типа карточки или типа документа <see cref="IKrType"/>
        /// или <c>null</c>, если настройки нельзя получить.
        /// </summary>
        /// <param name="krTypesCache">Кэш типов карточек и типов документов.</param>
        /// <param name="card">Карточка.</param>
        /// <param name="cardTypeID">Идентификатор типа карточки.</param>
        /// <param name="dbScope">Объект, обеспечивающий взаимодействие с базой данных.</param>
        /// <param name="docTypeID">
        /// Идентификатор типа документа или <c>null</c>, если идентификатор типа
        /// ещё неизвестен или тип карточки не использует типы документов.
        /// </param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// Эффективные настройки для типа карточки или типа документа <see cref="IKrType"/>
        /// или <c>null</c>, если настройки нельзя получить.
        /// </returns>
        private static async ValueTask<IKrType?> TryGetKrTypeAsync(
            IKrTypesCache krTypesCache,
            Card card,
            Guid cardTypeID,
            IDbScope dbScope,
            Guid? docTypeID = null,
            CancellationToken cancellationToken = default)
        {
            KrCardType? cardType = (await krTypesCache.GetCardTypesAsync(cancellationToken))
                .FirstOrDefault(x => x.ID == cardTypeID);

            if (cardType is null || !cardType.UseDocTypes)
            {
                return cardType;
            }

            if (!docTypeID.HasValue)
            {
                docTypeID = await KrProcessSharedHelper.GetDocTypeIDAsync(card, dbScope, cancellationToken);
                if (!docTypeID.HasValue)
                {
                    return null;
                }
            }

            return (await krTypesCache.GetDocTypesAsync(cancellationToken))
                .FirstOrDefault(x => x.ID == docTypeID.Value);
        }

        #endregion

        #region Base Overrides

        /// <summary>
        /// Выполняет действия при запуске подпроцесса.
        /// </summary>
        /// <param name="processInfo">Информация по запускаемому подпроцессу.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>Асинхронная задача.</returns>
        protected override Task StartProcessCoreAsync(
            IWorkflowProcessInfo processInfo,
            CancellationToken cancellationToken = default)
        {
            switch (processInfo.ProcessTypeName)
            {
                case WfHelper.ResolutionSubProcessName:
                    return this.RenderStepAsync(StartProcessTransition, processInfo, cancellationToken);

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
        protected override Task StopProcessCoreAsync(
            IWorkflowProcessInfo processInfo,
            CancellationToken cancellationToken = default)
        {
            switch (processInfo.ProcessTypeName)
            {
                case WfHelper.ResolutionSubProcessName:
                    // резолюция завершена
                    return Task.CompletedTask;

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
        protected override async Task CompleteTaskCoreAsync(IWorkflowTaskInfo taskInfo, CancellationToken cancellationToken = default)
        {
            CardTask task = taskInfo.Task;
            Guid typeID = task.TypeID;
            Guid? optionID = task.OptionID;

            if (WfHelper.TaskTypeIsResolution(typeID))
            {
                // отзыв резолюции
                if (optionID == DefaultCompletionOptions.Revoke)
                {
                    await this.RenderStepAsync(
                        GetRevokedByParent(taskInfo.Task)
                            ? RemoveTaskAndStopIfLastTransition
                            : WfResolutionRevokeOrCancelTransition,
                        taskInfo,
                        cancellationToken);

                    await this.SendRevokedNotificationAsync(taskInfo.Task, cancellationToken);
                    return;
                }

                // отмена резолюции
                if (optionID == DefaultCompletionOptions.Cancel)
                {
                    await this.RenderStepAsync(WfResolutionRevokeOrCancelTransition, taskInfo, cancellationToken);
                    return;
                }

                // завершение резолюции
                if (optionID == DefaultCompletionOptions.Complete)
                {
                    await this.RenderStepAsync(WfResolutionCompleteTransition, taskInfo, cancellationToken);
                    return;
                }

                // отправка резолюции исполнителю
                if (optionID == DefaultCompletionOptions.SendToPerformer)
                {
                    await this.RenderStepAsync(WfResolutionSendToPerformerTransition, taskInfo, cancellationToken);
                    return;
                }

                // создание дочерней резолюции
                if (optionID == DefaultCompletionOptions.CreateChildResolution)
                {
                    await this.RenderStepAsync(WfResolutionCreateChildTransition, taskInfo, cancellationToken);
                    return;
                }

                // изменение даты выполнения
                if (optionID == DefaultCompletionOptions.ModifyAsAuthor)
                {
                    await this.RenderStepAsync(WfModifyAsAuthorTransition, taskInfo, cancellationToken);
                }
            }

            // ничего не делаем для других вариантов завершения
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
                case WfHelper.ResolutionSubProcessName:
                    switch (transitionNumber)
                    {
                        case StartProcessTransition:
                            Guid? taskRowID = this.Manager.Request.TryGetStartingProcessTaskRowID();
                            bool useSpecifiedGroup = this.Manager.Request.TryGetStartingProcessTaskGroupRowID(out Guid? taskGroupRowID);

                            IWorkflowTaskInfo? taskInfo = await this.SendResolutionProjectAsync(
                                processInfo,
                                rowID: taskRowID,
                                groupRowID: taskGroupRowID,
                                useSpecifiedGroup: useSpecifiedGroup,
                                cancellationToken: cancellationToken);

                            if (taskInfo is not null && this.Manager.Request.TryGetStartingProcessNextTask() is not null)
                            {
                                // отправляем на следующее сохранение сигнал, который получит данные из предыдущего сохранения
                                // и сохранит созданное задание "через сохранение" в своём NextRequest
                                this.Manager.NextRequest.Card.GetWorkflowQueue().AddSignal(
                                    WfHelper.ResolutionSubProcessName,
                                    WfHelper.ResolutionCompleteProjectSignalName,
                                    parameters: new Dictionary<string, object?>
                                    {
                                        { "TaskRowID", taskInfo.Task.RowID },
                                    });
                            }

                            break;

                        case StopProcessTransition:
                            await this.StopProcessAsync(processInfo, cancellationToken);
                            break;

                        case RemoveTaskAndStopIfLastTransition:
                            bool removed = this.RemoveTaskFromProcessInfo(processInfo, processInfo.ToTaskInfo().Task.RowID);
                            if (removed && !this.HasTasks(processInfo))
                            {
                                await this.RenderStepAsync(StopProcessTransition, processInfo, cancellationToken);
                            }

                            break;

                        case WfResolutionCompleteTransition:
                            CardTask taskToComplete = processInfo.ToTaskInfo().Task;
                            await this.RevokeChildResolutionsAsync(
                                taskToComplete, checkRevokeChildrenField: true, cancellationToken: cancellationToken);

                            if (await this.SendResolutionControlAsync(taskToComplete, processInfo, cancellationToken: cancellationToken) is null)
                            {
                                await this.SendResolutionControlIfNecessaryForLastChildAsync(taskToComplete, processInfo, cancellationToken: cancellationToken);
                            }

                            if (taskToComplete.TypeID == DefaultTaskTypes.WfResolutionChildTypeID)
                            {
                                await this.SendChildCompleteNotificationAsync(taskToComplete, cancellationToken);
                            }

                            await this.RenderStepAsync(RemoveTaskAndStopIfLastTransition, processInfo, cancellationToken);
                            break;

                        case WfResolutionSendToPerformerTransition:
                            CardTask taskToSend = processInfo.ToTaskInfo().Task;
                            await this.SendResolutionToPerformersAsync(taskToSend, processInfo, cancellationToken: cancellationToken);
                            break;

                        case WfResolutionRevokeOrCancelTransition:
                            CardTask taskToRevoke = processInfo.ToTaskInfo().Task;
                            await this.RevokeChildResolutionsAsync(taskToRevoke, checkRevokeChildrenField: true, cancellationToken: cancellationToken);
                            await this.SendResolutionControlIfNecessaryForLastChildAsync(taskToRevoke, processInfo, cancellationToken: cancellationToken);
                            await this.RenderStepAsync(RemoveTaskAndStopIfLastTransition, processInfo, cancellationToken);
                            break;

                        case WfResolutionCreateChildTransition:
                            CardTask parentTask = processInfo.ToTaskInfo().Task;
                            await this.SendResolutionAsync(
                                DefaultTaskTypes.WfResolutionChildTypeID,
                                processInfo,
                                WfHelper.CreateChildResolutionFlags,
                                parentResolutionTask: parentTask,
                                cancellationToken: cancellationToken);

                            await this.ClearWasteOnNextStoreAsync(parentTask, cancellationToken);
                            break;

                        case WfModifyAsAuthorTransition:
                            CardTask taskToModifyPlanned = processInfo.ToTaskInfo().Task;
                            await this.ModifyAsAuthorOnNextStoreAsync(taskToModifyPlanned, cancellationToken);
                            break;

                        default:
                            throw new ArgumentOutOfRangeException(nameof(transitionNumber), transitionNumber, null);
                    }

                    break;

                default:
                    throw new ArgumentOutOfRangeException(nameof(processInfo.ProcessTypeName), processInfo.ProcessTypeName, null);
            }
        }

        /// <summary>
        /// Выполняет действие по обработке сигнала. Возвращает признак того, что сигнал был ожидаем и обработан (необязательно успешно).
        ///
        /// Необработанный сигнал по умолчанию не приводит к ошибке сохранения карточки и не приводит к откату транзакции,
        /// но не помечается как обработанный в очереди. По умолчанию все сигналы считаются необработанными.
        /// Необработанное исключение, возникшее в обработчике, также отмечает сигнал как необработанный.
        ///
        /// Если для ожидаемого сигнала требуется прервать транзакцию, то добавьте ошибку в <c>Manager.ValidationResult</c>, но верните в методе <c>true</c>.
        /// </summary>
        /// <param name="signalInfo">Информация по поступившему сигналу и подпроцессу, для которого поступил сигнал.</param>
        /// <param name="cancellationToken">Объект, посредством которого можно отменить асинхронную задачу.</param>
        /// <returns>
        /// <c>true</c>, если сигнал был ожидаем и обработан;
        /// <c>false</c> в противном случае.
        /// </returns>
        protected override async Task<bool> ProcessSignalCoreAsync(
            IWorkflowSignalInfo signalInfo,
            CancellationToken cancellationToken = default)
        {
            switch (signalInfo.Signal.Name)
            {
                case WfHelper.ResolutionCompleteProjectSignalName:
                    Guid? taskRowID = signalInfo.Signal.Parameters.TryGet<Guid?>("TaskRowID");
                    if (taskRowID.HasValue)
                    {
                        CardTask? task = WorkflowScopeContext.Current.Parent?.WorkflowContext?.Request
                            .TryGetStartingProcessNextTask();

                        if (task is not null)
                        {
                            await this.CompleteResolutionProjectOnNextStoreAsync(taskRowID.Value, task, cancellationToken);
                        }
                    }

                    return true;
            }

            return await base.ProcessSignalCoreAsync(signalInfo, cancellationToken);
        }

        #endregion

        #region Private Methods

        private async Task SendChildCompleteNotificationAsync(CardTask task, CancellationToken cancellationToken = default)
        {
            var dbScope = this.Manager.DbScope;
            var parentRowID = task.ParentRowID;
            var mainCardID = this.Manager.Request.Card.ID;

            await CardComponentHelper.FillTaskAssignedRolesAsync(task, dbScope, cancellationToken: cancellationToken);

            this.Manager.ValidationResult.Add(
                await this.Manager.NotificationManager
                    .SendAsync(
                        DefaultNotifications.WfChildResolutionNotification,
                        task.TaskAssignedRoles.Where(x => x.TaskRoleID == CardFunctionRoles.AuthorID).Select(x => x.RoleID).ToArray(),
                        new NotificationSendContext
                        {
                            MainCardID = mainCardID,
                            Info = NotificationHelper.GetInfoWithTask(task),
                            ModifyEmailActionAsync = async (email, ct) =>
                            {
                                NotificationHelper.ModifyTaskCaption(email, task);
                                await using (dbScope.Create())
                                {
                                    var db = dbScope.Db;
                                    var builder = dbScope.BuilderFactory;

                                    bool hasChilds = await db
                                        .SetCommand(
                                            builder
                                                .Select().Top(1).V(true)
                                                .From("Tasks", "t").NoLock()
                                                .Where().C("t", "ParentID").Equals().P("ParentRowID")
                                                .And().C("t", "TypeID").Equals().V(DefaultTaskTypes.WfResolutionChildTypeID)
                                                .Limit(1).Build(),
                                            db.Parameter("ParentRowID", parentRowID))
                                        .LogCommand()
                                        .ExecuteAsync<bool>(ct);

                                    email.PlaceholderAliases.SetReplacement(
                                        "subjectLabel",
                                        hasChilds
                                            ? "$WfResolution_TaskNotification_ChildCompleted"
                                            : "$WfResolution_TaskNotification_LastChildCompleted");
                                }
                            },
                            GetCardFuncAsync = (_, ct) =>
                            {
                                if (KrScopeContext.HasCurrent
                                    && KrScopeContext.Current.Cards.TryGetValue(mainCardID, out var card))
                                {
                                    return new ValueTask<Card>(card);
                                }

                                return new ValueTask<Card?>((Card?) null);
                            },
                        },
                        cancellationToken));
        }

        private async ValueTask SendRevokedNotificationAsync(CardTask task, CancellationToken cancellationToken = default)
        {
            await CardComponentHelper.FillTaskAssignedRolesAsync(task, this.Manager.DbScope, cancellationToken: cancellationToken);

            if (task.UserID.HasValue
                && !task.TaskAssignedRoles.Any(x => x.TaskRoleID == CardFunctionRoles.AuthorID && x.RoleID == task.UserID.Value))
            {
                var dbScope = this.Manager.DbScope;
                var parentRowID = task.ParentRowID;
                var mainCardID = this.Manager.Request.Card.ID;

                this.Manager.ValidationResult.Add(
                    await this.Manager.NotificationManager
                        .SendAsync(
                            DefaultNotifications.WfRevokeNotification,
                            new[] { task.UserID.Value },
                            new NotificationSendContext
                            {
                                MainCardID = mainCardID,
                                Info = NotificationHelper.GetInfoWithTask(task),
                                ModifyEmailActionAsync = async (email, ct) =>
                                {
                                    NotificationHelper.ModifyTaskCaption(email, task);
                                    await using (dbScope.Create())
                                    {
                                        var db = dbScope.Db;
                                        var builder = dbScope.BuilderFactory;

                                        string? option = await db
                                            .SetCommand(
                                                builder
                                                    .Select().Top(1)
                                                    .C("th", "OptionCaption")
                                                    .From("TaskHistory", "th").NoLock()
                                                    .Where().C("th", "RowID").Equals().P("ParentRowID")
                                                    .Limit(1).Build(),
                                                db.Parameter("ParentRowID", parentRowID))
                                            .LogCommand()
                                            .ExecuteAsync<string>(ct);

                                        if (!string.IsNullOrEmpty(option))
                                        {
                                            email.BodyTemplate = email.BodyTemplate.Replace(
                                                "{parentOption}",
                                                await LocalizeFormatAsync(
                                                    "{$WfResolution_TaskNotification_Revoked_ParentOption}",
                                                    "{task:ParentRowID=>TaskHistory.UserName}",
                                                    option, ct), StringComparison.Ordinal);
                                        }
                                    }
                                },
                                GetCardFuncAsync = (_, ct) =>
                                {
                                    if (KrScopeContext.HasCurrent
                                        && KrScopeContext.Current.Cards.TryGetValue(mainCardID, out var card))
                                    {
                                        return new ValueTask<Card>(card);
                                    }

                                    return new ValueTask<Card?>((Card?) null);
                                },
                            },
                            cancellationToken));
            }
        }

        #endregion
    }
}
