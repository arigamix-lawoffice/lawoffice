using System;
using Tessa.Cards;
using Tessa.Files;
using Tessa.Localization;
using Tessa.Platform.Storage;
using Tessa.Scheme;
using Tessa.Workflow.Actions;
using Tessa.Workflow.Actions.Descriptors;
using Tessa.Workflow.Helpful;

using static Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine.WorkflowConstants;

namespace Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine
{
    /// <summary>
    /// Описания методов действий.
    /// </summary>
    public static class KrWorkflowActionMethods
    {
        #region Methods parameters

        public static readonly Tuple<string, string>[] TaskCompleteBaseParams = new Tuple<string, string>[]
        {
            new Tuple<string, string>(nameof(CardTask), "task"),
            new Tuple<string, string>("dynamic", "taskCard"),
            new Tuple<string, string>("dynamic", "taskCardTables"),
            new Tuple<string, string>(nameof(WorkflowTaskNotificationInfoBase), "notificationInfo"),
        };

        public static readonly Tuple<string, string>[] ActionCompleteParams = new Tuple<string, string>[]
        {
            new Tuple<string, string>(nameof(WorkflowTaskNotificationInfoBase), "notificationInfo"),
        };

        #endregion

        #region KrTaskRegistration methods

        /// <summary>
        /// Дескриптор метода инициализации задания в действии <see cref="KrDescriptors.KrTaskRegistrationDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrTaskRegistrationTaskInitMethod =
            CreateTaskInitMethodTemplate(
                new[] { KrTaskRegistrationActionVirtual.SectionName, KrTaskRegistrationActionVirtual.InitTaskScript });

        /// <summary>
        /// Дескриптор метода выполняющегося при завершении задания с определённым вариантом завершения в действии <see cref="KrDescriptors.KrTaskRegistrationDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrTaskRegistrationTaskOptionMethod =
            CreateTaskOptionMethodTemplate(
                new[] { KrTaskRegistrationActionOptionsVirtual.Script },
                new[] { KrTaskRegistrationActionOptionsVirtual.SectionName },
                new[] { KrTaskRegistrationActionOptionsVirtual.Option, Table_Field_Caption },
                new[] { KrTaskRegistrationActionOptionsVirtual.Option, Table_Field_Caption });

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при завершении задания с определённым вариантом завершения в действии <see cref="KrDescriptors.KrTaskRegistrationDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrTaskRegistrationTaskCompleteNotificationMethod =
            CreateTaskCompleteNotificationMethodTemplate(
                new[] { KrTaskRegistrationActionOptionsVirtual.NotificationScript },
                new[] { KrTaskRegistrationActionOptionsVirtual.SectionName },
                new[] { KrTaskRegistrationActionOptionsVirtual.Option, Table_Field_Caption },
                new[] { KrTaskRegistrationActionOptionsVirtual.Option, Table_Field_Caption });

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при отправлении задания в действии <see cref="KrDescriptors.KrTaskRegistrationDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrTaskRegistrationTaskStartNotificationMethod =
            CreateTaskStartNotificationMethodTemplate(
                new[] { KrTaskRegistrationActionVirtual.SectionName, KrTaskRegistrationActionVirtual.NotificationScript });

        #endregion

        #region KrApproval methods

        /// <summary>
        /// Дескриптор метода инициализации задания в действии <see cref="KrDescriptors.KrApprovalDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrApprovalInitMethod =
            CreateTaskInitMethodTemplate(
                new[] { KrApprovalActionVirtual.SectionName, KrApprovalActionVirtual.InitTaskScript });

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при отправлении задания в действии <see cref="KrDescriptors.KrApprovalDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrApprovalStartNotificationMethod =
            CreateTaskStartNotificationMethodTemplate(
                new[] { KrApprovalActionVirtual.SectionName, KrApprovalActionVirtual.NotificationScript });

        /// <summary>
        /// Дескриптор метода выполняющегося при завершении задания с определённым вариантом завершения в действии <see cref="KrDescriptors.KrApprovalDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrApprovalOptionMethod =
            CreateTaskOptionMethodTemplate(
                new[] { KrApprovalActionOptionsVirtual.Script },
                new[] { KrApprovalActionOptionsVirtual.SectionName },
                new[] { KrApprovalActionOptionsVirtual.Option, Table_Field_Caption },
                new[] { KrApprovalActionOptionsVirtual.Option, Table_Field_Caption });

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при завершении задания с определённым вариантом завершения в действии <see cref="KrDescriptors.KrApprovalDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrApprovalCompleteNotificationMethod =
            CreateTaskCompleteNotificationMethodTemplate(
                new[] { KrApprovalActionOptionsVirtual.NotificationScript },
                new[] { KrApprovalActionOptionsVirtual.SectionName },
                new[] { KrApprovalActionOptionsVirtual.Option, Table_Field_Caption },
                new[] { KrApprovalActionOptionsVirtual.Option, Table_Field_Caption });

        /// <summary>
        /// Дескриптор метода выполняющегося при завершении задания с определённым вариантом завершения в действии <see cref="KrDescriptors.KrApprovalDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrApprovalActionOptionActionMethod =
            CreateActionOptionMethodTemplate(
                new[] { KrApprovalActionOptionsActionVirtual.Script },
                new[] { KrApprovalActionOptionsActionVirtual.SectionName },
                new[] { KrApprovalActionOptionsActionVirtual.ActionOption, Table_Field_Caption },
                new[] { KrApprovalActionOptionsActionVirtual.ActionOption, Table_Field_Caption });

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при завершении действия с определённым вариантом завершения в действии <see cref="KrDescriptors.KrApprovalDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrApprovalCompleteActionNotificationMethod =
            CreateActionCompleteNotificationMethodTemplate(
                new[] { KrApprovalActionOptionsActionVirtual.NotificationScript },
                new[] { KrApprovalActionOptionsActionVirtual.SectionName },
                new[] { KrApprovalActionOptionsActionVirtual.ActionOption, Table_Field_Caption },
                new[] { KrApprovalActionOptionsActionVirtual.ActionOption, Table_Field_Caption });

        #endregion

        #region KrSigning methods

        /// <summary>
        /// Дескриптор метода инициализации задания в действии <see cref="KrDescriptors.KrSigningDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrSigningInitMethod =
            CreateTaskInitMethodTemplate(
                new[] { KrSigningActionVirtual.SectionName, KrSigningActionVirtual.InitTaskScript });

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при отправлении задания в действии <see cref="KrDescriptors.KrSigningDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrSigningStartNotificationMethod =
            CreateTaskStartNotificationMethodTemplate(
                new[] { KrSigningActionVirtual.SectionName, KrSigningActionVirtual.NotificationScript });

        /// <summary>
        /// Дескриптор метода выполняющегося при завершении задания с определённым вариантом завершения в действии <see cref="KrDescriptors.KrSigningDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrSigningOptionMethod =
            CreateTaskOptionMethodTemplate(
                new[] { KrSigningActionOptionsVirtual.Script },
                new[] { KrSigningActionOptionsVirtual.SectionName },
                new[] { KrSigningActionOptionsVirtual.Option, Table_Field_Caption },
                new[] { KrSigningActionOptionsVirtual.Option, Table_Field_Caption });

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при завершении задания с определённым вариантом завершения в действии <see cref="KrDescriptors.KrSigningDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrSigningCompleteNotificationMethod =
            CreateTaskCompleteNotificationMethodTemplate(
                new[] { KrSigningActionOptionsVirtual.NotificationScript },
                new[] { KrSigningActionOptionsVirtual.SectionName },
                new[] { KrSigningActionOptionsVirtual.Option, Table_Field_Caption },
                new[] { KrSigningActionOptionsVirtual.Option, Table_Field_Caption });

        /// <summary>
        /// Дескриптор метода выполняющегося при завершении задания с определённым вариантом завершения в действии <see cref="KrDescriptors.KrSigningDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrSigningActionOptionActionMethod =
            CreateActionOptionMethodTemplate(
                new[] { KrSigningActionOptionsActionVirtual.Script },
                new[] { KrSigningActionOptionsActionVirtual.SectionName },
                new[] { KrSigningActionOptionsActionVirtual.ActionOption, Table_Field_Caption },
                new[] { KrSigningActionOptionsActionVirtual.ActionOption, Table_Field_Caption });

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при завершении действия с определённым вариантом завершения в действии <see cref="KrDescriptors.KrSigningDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrSigningCompleteActionNotificationMethod =
            CreateActionCompleteNotificationMethodTemplate(
                new[] { KrSigningActionOptionsActionVirtual.NotificationScript },
                new[] { KrSigningActionOptionsActionVirtual.SectionName },
                new[] { KrSigningActionOptionsActionVirtual.ActionOption, Table_Field_Caption },
                new[] { KrSigningActionOptionsActionVirtual.ActionOption, Table_Field_Caption });

        #endregion

        #region KrAmending methods

        /// <summary>
        /// Дескриптор метода инициализации задания в действии <see cref="KrDescriptors.KrAmendingDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrAmendingInitMethod =
            CreateTaskInitMethodTemplate(
                new[] { KrAmendingActionVirtual.SectionName, KrAmendingActionVirtual.InitTaskScript });

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при отправлении задания в действии <see cref="KrDescriptors.KrAmendingDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrAmendingStartNotificationMethod =
            CreateTaskStartNotificationMethodTemplate(
                new[] { KrAmendingActionVirtual.SectionName, KrAmendingActionVirtual.NotificationScript });

        /// <summary>
        /// Дескриптор метода выполняющегося при завершении задания с определённым вариантом завершения в действии <see cref="KrDescriptors.KrAmendingDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrAmendingOptionMethod =
            new WorkflowActionMethodDescriptor()
            {
                ErrorDescription = "$KrActions_Task_SingleOptionScriptError",
                MethodName = "CompleteOptionTaskScript",
                Parameters = TaskCompleteBaseParams,
                StorePath = new[] { KrAmendingActionVirtual.SectionName, KrAmendingActionVirtual.CompleteOptionTaskScript },
            };

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при завершении задания с определённым вариантом завершения в действии <see cref="KrDescriptors.KrAmendingDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrAmendingCompleteNotificationMethod =
            CreateCompletionTaskNotificationMethodTemplate(
                new[] { KrAmendingActionVirtual.SectionName, KrAmendingActionVirtual.CompleteOptionNotificationScript });

        #endregion

        #region KrUniversalTask methods

        /// <summary>
        /// Дескриптор метода инициализации задания в действии <see cref="KrDescriptors.KrUniversalTaskDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrUniversalTaskInitMethod =
            CreateTaskInitMethodTemplate(
                new[] { KrUniversalTaskActionVirtual.SectionName, KrUniversalTaskActionVirtual.InitTaskScript });

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при отправлении задания в действии <see cref="KrDescriptors.KrUniversalTaskDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrUniversalTaskStartNotificationMethod =
            CreateTaskStartNotificationMethodTemplate(
                new[] { KrUniversalTaskActionVirtual.SectionName, KrUniversalTaskActionVirtual.NotificationScript });

        /// <summary>
        /// Дескриптор метода выполняющегося при завершении задания с определённым вариантом завершения в действии <see cref="KrDescriptors.KrUniversalTaskDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrUniversalTaskOptionMethod =
            CreateTaskOptionMethodTemplate(
                new[] { KrUniversalTaskActionButtonsVirtual.Script },
                new[] { KrUniversalTaskActionButtonsVirtual.SectionName },
                new[] { Table_Field_Caption },
                new[] { Table_Field_Caption });

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при завершении задания с определённым вариантом завершения в действии <see cref="KrDescriptors.KrUniversalTaskDescriptor"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor KrUniversalTaskCompleteNotificationMethod =
            CreateTaskCompleteNotificationMethodTemplate(
                new[] { KrUniversalTaskActionButtonsVirtual.NotificationScript },
                new[] { KrUniversalTaskActionButtonsVirtual.SectionName },
                new[] { Table_Field_Caption },
                new[] { Table_Field_Caption });

        #endregion

        #region EditInterjectTask

        /// <summary>
        /// Дескриптор метода инициализации задания доработки автором <see cref="DefaultTaskTypes.KrEditInterjectTypeID"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor EditInterjectTaskInitMethod =
            CreateTaskInitMethodTemplate(
                new[] { KrWeEditInterjectOptionsVirtual.SectionName, KrWeEditInterjectOptionsVirtual.InitTaskScript },
                "InitEditInterjectTaskScript");

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при отправлении задания доработки автором <see cref="DefaultTaskTypes.KrEditInterjectTypeID"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor EditInterjectTaskStartNotificationMethod =
            CreateTaskStartNotificationMethodTemplate(
                new[] { KrWeEditInterjectOptionsVirtual.SectionName, KrWeEditInterjectOptionsVirtual.NotificationScript },
                "EditInterjectTaskNotificationScript");

        #endregion

        #region AdditionalApproval

        /// <summary>
        /// Дескриптор метода инициализации задания дополнительного согласования <see cref="DefaultTaskTypes.KrAdditionalApprovalTypeID"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor AdditionalApprovalTaskInitMethod =
            CreateTaskInitMethodTemplate(
                new[] { KrWeAdditionalApprovalOptionsVirtual.SectionName, KrWeAdditionalApprovalOptionsVirtual.InitTaskScript },
                "InitAdditionalApprovalTaskScript");

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при отправлении задания дополнительного согласования <see cref="DefaultTaskTypes.KrAdditionalApprovalTypeID"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor AdditionalApprovalTaskStartNotificationMethod =
            CreateTaskStartNotificationMethodTemplate(
                new[] { KrWeAdditionalApprovalOptionsVirtual.SectionName, KrWeAdditionalApprovalOptionsVirtual.NotificationScript },
                "AdditionalApprovalTaskNotificationScript");

        #endregion

        #region RequestComment

        /// <summary>
        /// Дескриптор метода инициализации задания запроса комментария <see cref="DefaultTaskTypes.KrRequestCommentTypeID"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor RequestCommentTaskInitMethod =
            CreateTaskInitMethodTemplate(
                new[] { KrWeRequestCommentOptionsVirtual.SectionName, KrWeRequestCommentOptionsVirtual.InitTaskScript },
                "InitRequestCommentTaskScript");

        /// <summary>
        /// Дескриптор метода изменения уведомления отправляющегося при отправлении задания запроса комментария <see cref="DefaultTaskTypes.KrRequestCommentTypeID"/>.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor RequestCommentTaskStartNotificationMethod =
            CreateTaskStartNotificationMethodTemplate(
                new[] { KrWeRequestCommentOptionsVirtual.SectionName, KrWeRequestCommentOptionsVirtual.NotificationScript },
                "RequestCommentTaskNotificationScript");

        #endregion

        #region WorkflowCreate Methods

        /// <summary>
        /// Дескриптор метода инициализации карточки в действии создания карточки.
        /// </summary>
        public static readonly WorkflowActionMethodDescriptor CreateCardInitMethod = new()
        {
            ErrorDescription = "$WorkflowEngine_Actions_CreateCard_CompilationError",
            MethodName = "InitCard",
            Parameters = new[]
            {
                new Tuple<string, string>("dynamic", "newCard"),
                new Tuple<string, string>("dynamic", "newCardTables"),
                new Tuple<string, string>(nameof(Card), "newCardObject"),
                new Tuple<string, string>(nameof(IFileContainer), "newCardFileContainer"),
            },
            StorePath = new[] { "KrCreateCardAction", "Script" },
        };

        #endregion

        #region Public methods

        /// <summary>
        /// Создаёт дескриптор метода инициализации задания.
        /// </summary>
        /// <param name="storePath">Путь к месту в параметрах действия, где хранится текст скрипта. Для методов формируемых автоматически по строке - данное поле определяет путь к месту не в параметрах действия, а в строке.</param>
        /// <param name="methodName">Имя метода. Значение по умолчанию: InitTaskScript.</param>
        /// <returns>Дескриптор метода действия.</returns>
        public static WorkflowActionMethodDescriptor CreateTaskInitMethodTemplate(
            string[] storePath,
            string methodName = "InitTaskScript")
        {
            return new WorkflowActionMethodDescriptor()
            {
                ErrorDescription = "$WorkflowEngine_Actions_Task_InitScriptError",
                MethodName = methodName,
                Parameters = WorkflowTaskActionBase.TaskParams,
                StorePath = storePath,
            };
        }

        /// <summary>
        /// Создаёт дескриптор метода изменения уведомления отправляющегося при отправлении задания.
        /// </summary>
        /// <param name="storePath">Путь к месту в параметрах действия, где хранится текст скрипта. Для методов формируемых автоматически по строке - данное поле определяет путь к месту не в параметрах действия, а в строке.</param>
        /// <param name="methodName">Имя метода. Значение по умолчанию: NotificationScript.</param>
        /// <returns>Дескриптор метода действия.</returns>
        public static WorkflowActionMethodDescriptor CreateTaskStartNotificationMethodTemplate(
            string[] storePath,
            string methodName = "NotificationScript")
        {
            return new WorkflowActionMethodDescriptor()
            {
                ErrorDescription = "$WorkflowEngine_Actions_Task_NotificationScriptError",
                MethodName = methodName,
                Parameters = new Tuple<string, string>[]
                {
                    new Tuple<string, string>("Tessa.Notices.NotificationEmail", "email"),
                    new Tuple<string, string>(nameof(CardTask), "task"),
                },
                StorePath = storePath,
            };
        }

        /// <summary>
        /// Создаёт дескриптор метода выполняющегося при завершении задания с определённым вариантом завершения.
        /// </summary>
        /// <param name="storePath">Путь к месту в параметрах действия, где хранится текст скрипта. Для методов формируемых автоматически по строке - данное поле определяет путь к месту не в параметрах действия, а в строке.</param>
        /// <param name="listPath">Путь к месту в параметрах действия, где хранится таблица со скриптами. Путь к скрипту внутри строки определяется по <paramref name="storePath"/>.</param>
        /// <param name="errorDescriptionPath">Путь в строке к месту, где хранится строка используемая при формировании описания места возникновения ошибки при компиляции скрипта. Путь к строке определяется по <paramref name="listPath"/>.</param>
        /// <param name="methodDescriptionPath">Путь в строке к месту, где хранится строка используемая при формировании описания метода. Путь к строке определяется по <paramref name="listPath"/>.</param>
        /// <returns>Дескриптор метода действия.</returns>
        public static WorkflowActionMethodDescriptor CreateTaskOptionMethodTemplate(
            string[] storePath,
            string[] listPath,
            string[] errorDescriptionPath,
            string[] methodDescriptionPath)
        {
            return new WorkflowActionMethodDescriptor()
            {
                ErrorDescription = "$WorkflowEngine_Actions_Task_OptionScriptError",
                MethodName = "Option",
                Parameters = WorkflowTaskActionBase.TaskCompleteParams,
                StorePath = storePath,
                ComplexDescriptor = new WorkflowActionMethodsComplexDescriptor()
                {
                    ListPath = listPath,
                    GetMethodNameSuffix = (hash) => hash.TryGet<Guid>(Names.Table_RowID).ToString("N"),
                    GetErrorDescription = (text, hash) => LocalizationManager.Format(
                        text,
                        WorkflowEngineHelper.Get<string>(hash, errorDescriptionPath)),
                    GetMethodDescription = (text, index, hash) =>
                        LocalizationManager.Format(
                            "$WorkflowEngine_Actions_Task_OptionsDescription",
                            WorkflowEngineHelper.Get<string>(hash, methodDescriptionPath),
                            (index + 1).ToString()),
                },
            };
        }

        /// <summary>
        /// Создаёт дескриптор метода изменения уведомления отправляющегося при завершении задания с определённым вариантом завершения.
        /// </summary>
        /// <param name="storePath">Путь к месту в параметрах действия, где хранится текст скрипта. Для методов формируемых автоматически по строке - данное поле определяет путь к месту не в параметрах действия, а в строке.</param>
        /// <param name="listPath">Путь к месту в параметрах действия, где хранится таблица со скриптами. Путь к скрипту внутри строки определяется по <paramref name="storePath"/>.</param>
        /// <param name="errorDescriptionPath">Путь в строке к месту, где хранится строка используемая при формировании описания места возникновения ошибки при компиляции скрипта. Путь к строке определяется по <paramref name="listPath"/>.</param>
        /// <param name="methodDescriptionPath">Путь в строке к месту, где хранится строка используемая при формировании описания метода. Путь к строке определяется по <paramref name="listPath"/>.</param>
        /// <returns>Дескриптор метода действия.</returns>
        public static WorkflowActionMethodDescriptor CreateTaskCompleteNotificationMethodTemplate(
            string[] storePath,
            string[] listPath,
            string[] errorDescriptionPath,
            string[] methodDescriptionPath)
        {
            return new WorkflowActionMethodDescriptor()
            {
                ErrorDescription = "$WorkflowEngine_Actions_Task_OptionNotificationScriptError",
                MethodName = "Notification",
                Parameters = new Tuple<string, string>[]
                {
                    new Tuple<string, string>("Tessa.Notices.NotificationEmail", "email"),
                    new Tuple<string, string>(nameof(CardTask), "task"),
                },
                StorePath = storePath,
                ComplexDescriptor = new WorkflowActionMethodsComplexDescriptor()
                {
                    ListPath = listPath,
                    GetMethodNameSuffix = (hash) => hash.TryGet<Guid>(Names.Table_RowID).ToString("N"),
                    GetErrorDescription = (text, hash) => LocalizationManager.Format(
                        text,
                        WorkflowEngineHelper.Get<string>(hash, errorDescriptionPath)),
                    GetMethodDescription = (text, index, hash) =>
                        LocalizationManager.Format(
                            "$WorkflowEngine_Actions_Task_OptionsDescription",
                            WorkflowEngineHelper.Get<string>(hash, methodDescriptionPath),
                            (index + 1).ToString()),
                },
            };
        }

        /// <summary>
        /// Создаёт дескриптор метода изменения уведомления отправляющегося при завершении задания.
        /// </summary>
        /// <param name="storePath">Путь к месту в параметрах действия, где хранится текст скрипта. Для методов формируемых автоматически по строке - данное поле определяет путь к месту не в параметрах действия, а в строке.</param>
        /// <param name="methodName">Имя метода. Значение по умолчанию: CompletionNotificationScript.</param>
        /// <returns>Дескриптор метода действия.</returns>
        public static WorkflowActionMethodDescriptor CreateCompletionTaskNotificationMethodTemplate(
            string[] storePath,
            string methodName = "CompletionNotificationScript")
        {
            return new WorkflowActionMethodDescriptor()
            {
                ErrorDescription = "$KrActions_TaskCompletion_NotificationScriptError",
                MethodName = methodName,
                Parameters = new Tuple<string, string>[]
                {
                    new Tuple<string, string>("Tessa.Notices.NotificationEmail", "email"),
                    new Tuple<string, string>(nameof(CardTask), "task"),
                },
                StorePath = storePath,
            };
        }

        /// <summary>
        /// Создаёт дескриптор метода выполняющегося при завершении действия с определённым вариантом завершения.
        /// </summary>
        /// <param name="storePath">Путь к месту в параметрах действия, где хранится текст скрипта. Для методов формируемых автоматически по строке - данное поле определяет путь к месту не в параметрах действия, а в строке.</param>
        /// <param name="listPath">Путь к месту в параметрах действия, где хранится таблица со скриптами. Путь к скрипту внутри строки определяется по <paramref name="storePath"/>.</param>
        /// <param name="errorDescriptionPath">Путь в строке к месту, где хранится строка используемая при формировании описания места возникновения ошибки при компиляции скрипта. Путь к строке определяется по <paramref name="listPath"/>.</param>
        /// <param name="methodDescriptionPath">Путь в строке к месту, где хранится строка используемая при формировании описания метода. Путь к строке определяется по <paramref name="listPath"/>.</param>
        /// <returns>Дескриптор метода действия.</returns>
        public static WorkflowActionMethodDescriptor CreateActionOptionMethodTemplate(
            string[] storePath,
            string[] listPath,
            string[] errorDescriptionPath,
            string[] methodDescriptionPath)
        {
            return new WorkflowActionMethodDescriptor()
            {
                ErrorDescription = "$KrActions_Actions_Action_OptionScriptError",
                MethodName = "ActionOption",
                Parameters = ActionCompleteParams,
                StorePath = storePath,
                ComplexDescriptor = new WorkflowActionMethodsComplexDescriptor()
                {
                    ListPath = listPath,
                    GetMethodNameSuffix = (hash) => hash.TryGet<Guid>(Names.Table_RowID).ToString("N"),
                    GetErrorDescription = (text, hash) => LocalizationManager.Format(
                        text,
                        WorkflowEngineHelper.Get<string>(hash, errorDescriptionPath)),
                    GetMethodDescription = (text, index, hash) =>
                        LocalizationManager.Format(
                            "$KrActions_Actions_Action_OptionsDescription",
                            WorkflowEngineHelper.Get<string>(hash, methodDescriptionPath),
                            (index + 1).ToString()),
                },
            };
        }

        /// <summary>
        /// Создаёт дескриптор метода изменения уведомления отправляющегося при завершении задания с определённым вариантом завершения.
        /// </summary>
        /// <param name="storePath">Путь к месту в параметрах действия, где хранится текст скрипта. Для методов формируемых автоматически по строке - данное поле определяет путь к месту не в параметрах действия, а в строке.</param>
        /// <param name="listPath">Путь к месту в параметрах действия, где хранится таблица со скриптами. Путь к скрипту внутри строки определяется по <paramref name="storePath"/>.</param>
        /// <param name="errorDescriptionPath">Путь в строке к месту, где хранится строка используемая при формировании описания места возникновения ошибки при компиляции скрипта. Путь к строке определяется по <paramref name="listPath"/>.</param>
        /// <param name="methodDescriptionPath">Путь в строке к месту, где хранится строка используемая при формировании описания метода. Путь к строке определяется по <paramref name="listPath"/>.</param>
        /// <returns>Дескриптор метода действия.</returns>
        public static WorkflowActionMethodDescriptor CreateActionCompleteNotificationMethodTemplate(
            string[] storePath,
            string[] listPath,
            string[] errorDescriptionPath,
            string[] methodDescriptionPath)
        {
            return new WorkflowActionMethodDescriptor()
            {
                ErrorDescription = "$KrActions_Actions_Action_OptionNotificationScriptError",
                MethodName = "ActionNotification",
                Parameters = new Tuple<string, string>[] { new Tuple<string, string>("Tessa.Notices.NotificationEmail", "email") },
                StorePath = storePath,
                ComplexDescriptor = new WorkflowActionMethodsComplexDescriptor()
                {
                    ListPath = listPath,
                    GetMethodNameSuffix = (hash) => hash.TryGet<Guid>(Names.Table_RowID).ToString("N"),
                    GetErrorDescription = (text, hash) => LocalizationManager.Format(
                        text,
                        WorkflowEngineHelper.Get<string>(hash, errorDescriptionPath)),
                    GetMethodDescription = (text, index, hash) =>
                        LocalizationManager.Format(
                            "$KrActions_Actions_Action_OptionsDescription",
                            WorkflowEngineHelper.Get<string>(hash, methodDescriptionPath),
                            (index + 1).ToString()),
                },
            };
        }

        #endregion
    }
}
