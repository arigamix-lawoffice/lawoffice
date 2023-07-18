using System;
using Tessa.Cards;
using Tessa.Files;
using Tessa.Workflow.Actions.Descriptors;

namespace Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine
{
    public static class KrDescriptors
    {

        public static readonly WorkflowActionDescriptor KrChangeStateDescriptor =
            new WorkflowActionDescriptor(DefaultCardTypes.KrChangeStateActionTypeID)
            {
                Group = "$KrActions_StandardSolutionGroup",
                Icon = "Thin248",
                Order = 99,
                NotPersistentModeAllowed = true,
            };

        public static readonly WorkflowActionDescriptor CreateCardDescriptor =
            new WorkflowActionDescriptor(DefaultCardTypes.WorkflowCreateCardActionTypeID)
            {
                Icon = "Thin1",
                Methods = new[]
                {
                    KrWorkflowActionMethods.CreateCardInitMethod,
                },
                NotPersistentModeAllowed = true,
            };

        public static readonly WorkflowActionDescriptor AcquaintanceDescriptor =
            new WorkflowActionDescriptor(DefaultCardTypes.KrAcquaintanceActionTypeID)
            {
                Group = "$KrActions_StandardSolutionGroup",
                Icon = "Thin83",
                NotPersistentModeAllowed = true,
            };

        public static readonly WorkflowActionDescriptor RegistrationDescriptor =
            new WorkflowActionDescriptor(DefaultCardTypes.KrRegistrationActionTypeID)
            {
                Group = "$KrActions_StandardSolutionGroup",
                Icon = "Thin325",
                NotPersistentModeAllowed = true,
            };

        public static readonly WorkflowActionDescriptor DeregistrationDescriptor =
            new WorkflowActionDescriptor(DefaultCardTypes.KrDeregistrationActionTypeID)
            {
                Group = "$KrActions_StandardSolutionGroup",
                Icon = "Thin325",
                NotPersistentModeAllowed = true,
            };

        /// <summary>
        /// Задание регистрации.
        /// </summary>
        public static readonly WorkflowActionDescriptor KrTaskRegistrationDescriptor =
            new WorkflowActionDescriptor(DefaultCardTypes.KrTaskRegistrationActionTypeID)
            {
                Group = "$KrActions_RoutesGroup",
                Icon = "Thin325",
                Methods = new[]
                {
                    KrWorkflowActionMethods.KrTaskRegistrationTaskInitMethod,
                    KrWorkflowActionMethods.KrTaskRegistrationTaskOptionMethod,
                    WorkflowActionMethods.TaskEventMethod,
                    KrWorkflowActionMethods.KrTaskRegistrationTaskStartNotificationMethod,
                    KrWorkflowActionMethods.KrTaskRegistrationTaskCompleteNotificationMethod
                },
            };

        /// <summary>
        /// Согласование.
        /// </summary>
        public static readonly WorkflowActionDescriptor KrApprovalDescriptor =
            new WorkflowActionDescriptor(DefaultCardTypes.KrApprovalActionTypeID)
            {
                Group = "$KrActions_RoutesGroup",
                Icon = "Int968",
                Methods = new[]
                {
                    KrWorkflowActionMethods.KrApprovalInitMethod,
                    KrWorkflowActionMethods.KrApprovalOptionMethod,
                    WorkflowActionMethods.TaskEventMethod,
                    KrWorkflowActionMethods.KrApprovalStartNotificationMethod,
                    KrWorkflowActionMethods.KrApprovalCompleteNotificationMethod,
                    KrWorkflowActionMethods.KrApprovalActionOptionActionMethod,
                    KrWorkflowActionMethods.KrApprovalCompleteActionNotificationMethod,
                    KrWorkflowActionMethods.EditInterjectTaskInitMethod,
                    KrWorkflowActionMethods.EditInterjectTaskStartNotificationMethod,
                    KrWorkflowActionMethods.AdditionalApprovalTaskInitMethod,
                    KrWorkflowActionMethods.AdditionalApprovalTaskStartNotificationMethod,
                    KrWorkflowActionMethods.RequestCommentTaskInitMethod,
                    KrWorkflowActionMethods.RequestCommentTaskStartNotificationMethod,
                },
            };

        /// <summary>
        /// Подписание.
        /// </summary>
        public static readonly WorkflowActionDescriptor KrSigningDescriptor =
            new WorkflowActionDescriptor(DefaultCardTypes.KrSigningActionTypeID)
            {
                Group = "$KrActions_RoutesGroup",
                Icon = "Int1042",
                Methods = new[]
                {
                    KrWorkflowActionMethods.KrSigningInitMethod,
                    KrWorkflowActionMethods.KrSigningOptionMethod,
                    WorkflowActionMethods.TaskEventMethod,
                    KrWorkflowActionMethods.KrSigningStartNotificationMethod,
                    KrWorkflowActionMethods.KrSigningCompleteNotificationMethod,
                    KrWorkflowActionMethods.KrSigningActionOptionActionMethod,
                    KrWorkflowActionMethods.KrSigningCompleteActionNotificationMethod,
                    KrWorkflowActionMethods.EditInterjectTaskInitMethod,
                    KrWorkflowActionMethods.EditInterjectTaskStartNotificationMethod,
                    KrWorkflowActionMethods.AdditionalApprovalTaskInitMethod,
                    KrWorkflowActionMethods.AdditionalApprovalTaskStartNotificationMethod,
                    KrWorkflowActionMethods.RequestCommentTaskInitMethod,
                    KrWorkflowActionMethods.RequestCommentTaskStartNotificationMethod,
                },
            };

        /// <summary>
        /// Доработка.
        /// </summary>
        public static readonly WorkflowActionDescriptor KrAmendingDescriptor =
            new WorkflowActionDescriptor(DefaultCardTypes.KrAmendingActionTypeID)
            {
                Group = "$KrActions_RoutesGroup",
                Icon = "Thin3",
                Methods = new[]
                {
                    WorkflowActionMethods.TaskEventMethod,
                    KrWorkflowActionMethods.KrAmendingInitMethod,
                    KrWorkflowActionMethods.KrAmendingStartNotificationMethod,
                    KrWorkflowActionMethods.KrAmendingOptionMethod,
                    KrWorkflowActionMethods.KrAmendingCompleteNotificationMethod
                },
            };

        /// <summary>
        /// Настраиваемое задание.
        /// </summary>
        public static readonly WorkflowActionDescriptor KrUniversalTaskDescriptor =
            new WorkflowActionDescriptor(DefaultCardTypes.KrUniversalTaskActionTypeID)
            {
                Group = "$KrActions_RoutesGroup",
                Icon = "Int1053",
                Methods = new[]
                {
                    WorkflowActionMethods.TaskEventMethod,
                    KrWorkflowActionMethods.KrUniversalTaskInitMethod,
                    KrWorkflowActionMethods.KrUniversalTaskStartNotificationMethod,
                    KrWorkflowActionMethods.KrUniversalTaskOptionMethod,
                    KrWorkflowActionMethods.KrUniversalTaskCompleteNotificationMethod
                },
            };

        /// <summary>
        /// Выполнение задачи.
        /// </summary>
        public static readonly WorkflowActionDescriptor KrResolutionDescriptor =
            new WorkflowActionDescriptor(DefaultCardTypes.KrResolutionActionTypeID)
            {
                Group = "$KrActions_RoutesGroup",
                Icon = "Int1036",
                Methods = new[]
                {
                    WorkflowActionMethods.TaskEventMethod,
                },
            };

        /// <summary>
        /// Инициализация маршрута.
        /// </summary>
        public static readonly WorkflowActionDescriptor KrRouteInitializationDescriptor =
            new WorkflowActionDescriptor(DefaultCardTypes.KrRouteInitializationActionTypeID)
            {
                Group = "$KrActions_RoutesGroup",
                Icon = "Int146",
            };
    }
}
