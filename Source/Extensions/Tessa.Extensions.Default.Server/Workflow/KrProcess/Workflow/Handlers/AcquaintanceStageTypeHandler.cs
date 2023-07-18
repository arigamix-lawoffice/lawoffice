using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Acquaintance;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using Unity;
using static Tessa.Extensions.Default.Shared.Workflow.KrProcess.KrConstants;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    /// <summary>
    /// Обработчик этапа <see cref="Shared.Workflow.KrProcess.StageTypeDescriptors.AcquaintanceDescriptor"/>.
    /// </summary>
    public class AcquaintanceStageTypeHandler : StageTypeHandlerBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="AcquaintanceStageTypeHandler"/>.
        /// </summary>
        /// <param name="acquaintanceManager">Менеджер для отправки массового ознакомления.</param>
        /// <param name="roleGetStrategy">Стратегия для получения информации о ролях.</param>
        /// <param name="contextRoleManager">Обработчик контекстных ролей.</param>
        public AcquaintanceStageTypeHandler(
            [Dependency(KrAcquaintanceManagerNames.WithoutTransaction)] IKrAcquaintanceManager acquaintanceManager,
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager)
        {
            this.AcquaintanceManager = acquaintanceManager;
            this.RoleGetStrategy = roleGetStrategy;
            this.СontextRoleManager = contextRoleManager;
        }

        #endregion

        #region Protected Properties

        /// <summary>
        /// Возвращает или задаёт менеджер для отправки массового ознакомления.
        /// </summary>
        protected IKrAcquaintanceManager AcquaintanceManager { get; set; }

        /// <summary>
        /// Стратегия для получения информации о ролях.
        /// </summary>
        protected IRoleGetStrategy RoleGetStrategy { get; set; }

        /// <summary>
        /// Возвращает или задаёт обработчик контекстных ролей.
        /// </summary>
        protected IContextRoleManager СontextRoleManager { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task<StageHandlerResult> HandleStageStartAsync(IStageTypeHandlerContext context)
        {
            var roles = context.Stage.Performers.Select(x => x.PerformerID).ToList();
            var mainCardID = context.Stage.InfoStorage.TryGet<Guid?>("MainCardID") ?? context.MainCardID ?? Guid.Empty;
            if (roles.Count == 0
                || mainCardID == Guid.Empty)
            {
                // Некому отправлять ознакомление или нет карточки для ознакомления, считаем, что этап завершен
                return StageHandlerResult.CompleteResult;
            }

            var stageSettings = context.Stage.SettingsStorage;
            var notificationID = stageSettings.TryGet<Guid?>(KrAcquaintanceSettingsVirtual.NotificationID);
            var excludeDeputies = stageSettings.TryGet<bool?>(KrAcquaintanceSettingsVirtual.ExcludeDeputies) ?? false;
            var comment = stageSettings.TryGet<string>(KrAcquaintanceSettingsVirtual.Comment);
            var placeholderAliases = stageSettings.TryGet<string>(KrAcquaintanceSettingsVirtual.AliasMetadata);
            var senderID = stageSettings.TryGet<Guid?>(KrAcquaintanceSettingsVirtual.SenderID);

            if (senderID.HasValue)
            {
                var role = await this.RoleGetStrategy.GetRoleParamsAsync(senderID.Value, context.CancellationToken);
                if (role.Type is null)
                {
                    context.ValidationResult.AddError(this, "Sender role isn't found.");
                    return StageHandlerResult.EmptyResult;
                }

                switch (role.Type)
                {
                    case RoleType.Personal:
                        // Do Nothing
                        break;

                    case RoleType.Context:
                        var contextRole = await this.СontextRoleManager.GetContextRoleAsync(senderID.Value, context.CancellationToken);

                        var users = await this.СontextRoleManager.GetCardContextUsersAsync(contextRole, mainCardID, cancellationToken: context.CancellationToken);
                        if (users.Count > 0)
                        {
                            senderID = users[0].UserID;
                        }
                        break;

                    default:
                        context.ValidationResult.AddError(this, "$KrProcess_Acquaintance_SenderShoudBePersonalOrContext");
                        return StageHandlerResult.EmptyResult;
                }
            }

            var result = await this.AcquaintanceManager.SendAsync(
                mainCardID,
                roles,
                excludeDeputies,
                comment,
                placeholderAliases,
                null,
                notificationID,
                senderID,
                cancellationToken: context.CancellationToken);

            // при успешной отправке записывается текст вида "Ознакомление отправлено N сотрудникам",
            // его нет смысла отображать пользователю, который "продвинул" маршрут
            if (!result.IsSuccessful || result.HasWarnings)
            {
                context.ValidationResult.Add(result);
            }

            return StageHandlerResult.CompleteResult;
        }

        #endregion
    }
}
