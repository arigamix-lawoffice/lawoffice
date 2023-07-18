using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers;
using Tessa.Extensions.Default.Server.Workflow.KrCompilers.SourceBuilders;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Notices;
using Tessa.Notices.Parameters;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="StageTypeDescriptors.NotificationDescriptor"/>.
    /// </summary>
    public class NotificationStageTypeHandler : StageTypeHandlerBase
    {
        #region Constants And Static Fields

        /// <summary>
        /// Дескриптор метода "Сценарий изменения уведомления".
        /// </summary>
        public static readonly KrExtraSourceDescriptor ModifyEmailMethodDescriptor = new KrExtraSourceDescriptor("ModifyEmailAction")
        {
            DisplayName = "$CardTypes_Controls_EmailModifyScenario",
            ParameterName = "email",
            ParameterType = $"global::{typeof(NotificationEmail).FullName}",
            ScriptField = KrConstants.KrNotificationSettingVirtual.EmailModificationScript
        };

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="NotificationStageTypeHandler"/>.
        /// </summary>
        /// <param name="notificationManager"><inheritdoc cref="NotificationManager" path="/summary"/></param>
        /// <param name="session"><inheritdoc cref="Session" path="/summary"/></param>
        /// <param name="compilationCache"><inheritdoc cref="CompilationCache" path="/summary"/></param>
        /// <param name="unityContainer"><inheritdoc cref="UnityContainer" path="/summary"/></param>
        public NotificationStageTypeHandler(
            [Dependency(NotificationManagerNames.WithoutTransaction)] INotificationManager notificationManager,
            ISession session,
            IKrStageTemplateCompilationCache compilationCache,
            IUnityContainer unityContainer)
        {
            this.NotificationManager = NotNullOrThrow(notificationManager);
            this.Session = NotNullOrThrow(session);
            this.CompilationCache = NotNullOrThrow(compilationCache);
            this.UnityContainer = NotNullOrThrow(unityContainer);
        }

        #endregion

        #region Protected Properties

        /// <inheritdoc cref="INotificationManager"/>
        protected INotificationManager NotificationManager { get; }

        /// <inheritdoc cref="ISession"/>
        protected ISession Session { get; }

        /// <inheritdoc cref="IKrStageTemplateCompilationCache"/>
        protected IKrStageTemplateCompilationCache CompilationCache { get; }

        /// <summary>
        /// Unity-контейнер.
        /// </summary>
        protected IUnityContainer UnityContainer { get; }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext context)
        {
            var optionalRecipientsList = context.Stage.SettingsStorage.TryGet<IList>(KrConstants.KrNotificationOptionalRecipientsVirtual.Synthetic);

            if (context.Stage.Performers.Count == 0
                && optionalRecipientsList is null or { Count: 0 })
            {
                // Некому отправлять уведомления, считаем, что этап завершен
                return StageHandlerResult.CompleteResult;
            }

            var roles = context.Stage.Performers.Select(x => x.PerformerID).ToArray();
            var optionalRecipients = optionalRecipientsList?.Cast<Dictionary<string, object>>().Select(x => x.TryGet<Guid>(KrConstants.KrNotificationOptionalRecipientsVirtual.RoleID)).ToArray();
            var mainCardID = context.Stage.InfoStorage.TryGet<Guid?>("MainCardID") ?? context.MainCardID ?? this.Session.User.ID;
            var notificationID = context.Stage.SettingsStorage.TryGet<Guid>(KrConstants.KrNotificationSettingVirtual.NotificationID);
            var excludeDeputies = context.Stage.SettingsStorage.TryGet<bool?>(KrConstants.KrNotificationSettingVirtual.ExcludeDeputies) ?? false;
            var excludeSubscribers = context.Stage.SettingsStorage.TryGet<bool?>(KrConstants.KrNotificationSettingVirtual.ExcludeSubscribers) ?? false;

            Func<NotificationEmail, CancellationToken, Task> modifyEmailActionAsync = null;

            if (context.Stage.TemplateID.HasValue)
            {
                var compilationObject = await this.CompilationCache.GetAsync(
                    context.Stage.TemplateID.Value,
                    cancellationToken: context.CancellationToken);

                var inst = compilationObject.TryCreateKrScriptInstance(
                    KrCompilersHelper.FormatClassName(
                        SourceIdentifiers.KrRuntimeClass,
                        SourceIdentifiers.StageAlias,
                        context.Stage.ID),
                    context.ValidationResult,
                    true);

                if (!context.ValidationResult.IsSuccessful())
                {
                    return StageHandlerResult.EmptyResult;
                }

                if (inst is not null)
                {
                    await HandlerHelper.InitScriptContextAsync(
                        this.UnityContainer,
                        inst,
                        context);

                    modifyEmailActionAsync = async (e, _) => await inst.InvokeExtraAsync(
                        ModifyEmailMethodDescriptor.MethodName,
                        e,
                        false);
                }
            }

            INotificationRecipientsSourceParameter recipientsParameter = (roles.Length, optionalRecipients) switch
            {
                (_, null) => new IDNotificationRecipientsSourceParameter { IDs = roles },
                (0, not null) => new IDNotificationRecipientsSourceParameter { IDs = optionalRecipients, IsOptional = true },
                _ => new AggregateNotificationRecipientsSourceParameter
                {
                    Parameters = new[]
                    {
                        new IDNotificationRecipientsSourceParameter { IDs = roles },
                        new IDNotificationRecipientsSourceParameter { IDs = optionalRecipients, IsOptional = true },
                    }
                }
            };

            context.ValidationResult.Add(
                await this.NotificationManager.SendAsync(
                    new IDNotificationEmailSourceParameter
                    {
                        ID = notificationID,
                    },
                    recipientsParameter,
                    new NotificationSendContext
                    {
                        MainCardID = mainCardID,
                        ExcludeDeputies = excludeDeputies,
                        DisableSubscribers = excludeSubscribers,
                        ModifyEmailActionAsync = modifyEmailActionAsync
                    },
                    context.CancellationToken));

            return StageHandlerResult.CompleteResult;
        }

        #endregion
    }
}
