using System;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Acquaintance;
using Tessa.Extensions.Default.Shared.Workflow.WorkflowEngine;
using Tessa.Platform.Validation;
using Tessa.Roles;
using Tessa.Roles.ContextRoles;
using Tessa.Workflow;
using Tessa.Workflow.Actions;
using Tessa.Workflow.Compilation;
using Tessa.Workflow.Helpful;
using Unity;

namespace Tessa.Extensions.Default.Server.Workflow.WorkflowEngine
{
    public sealed class KrAcquaintanceAction : WorkflowActionBase
    {
        #region Consts

        public const string MainActionSection = "KrAcquaintanceAction";
        public const string RolesSection = "KrAcquaintanceActionRoles";

        #endregion

        #region Fields

        private readonly IKrAcquaintanceManager acquaintanceManager;
        private readonly IRoleGetStrategy roleGetStrategy;
        private readonly IContextRoleManager contextRoleManager;

        #endregion

        #region Constructors

        public KrAcquaintanceAction(
            [Dependency(KrAcquaintanceManagerNames.WithoutTransaction)]IKrAcquaintanceManager acquaintanceManager,
            IRoleGetStrategy roleGetStrategy,
            IContextRoleManager contextRoleManager)
            :base(KrDescriptors.AcquaintanceDescriptor)
        {
            this.acquaintanceManager = acquaintanceManager;
            this.roleGetStrategy = roleGetStrategy;
            this.contextRoleManager = contextRoleManager;
        }

        #endregion

        #region Base Overrides

        protected override async Task ExecuteAsync(
            IWorkflowEngineContext context,
            IWorkflowEngineCompiled scriptObject)
        {
            var roles = await context.GetAllRowsAsync(RolesSection);
            var mainCardID = context.ProcessInstance.CardID;

            if (roles == null
                || roles.Count == 0
                || mainCardID == context.ProcessInstance.WorkflowCardID)
            {
                // Некому отправлять ознакомление или нет карточки для ознакомления
                return;
            }

            var roleIDs = roles.Select(x => WorkflowEngineHelper.Get<Guid>(x, WorkflowEngineHelper.WildCardHashMark, "ID")).ToList();
            var notificationID = await context.GetAsync<Guid?>(MainActionSection, "Notification", "ID");
            var excludeDeputies = await context.GetAsync<bool?>(MainActionSection, "ExcludeDeputies") ?? false;
            var comment = await context.GetAsync<string>(MainActionSection, "Comment");
            var placeholderAliases = await context.GetAsync<string>(MainActionSection, "AliasMetadata");
            var senderID = await context.GetAsync<Guid?>(MainActionSection, "Sender", "ID");

            if (senderID.HasValue)
            {
                var role = await this.roleGetStrategy.GetRoleParamsAsync(senderID.Value, cancellationToken: context.CancellationToken);
                if (role.Type == null)
                {
                    context.ValidationResult.AddError(this, "Sender role isn't found.");
                    return;
                }

                switch (role.Type)
                {
                    case RoleType.Personal:
                        // Do Nothing
                        break;

                    case RoleType.Context:
                        var contextRole = await this.contextRoleManager.GetContextRoleAsync(senderID.Value, cancellationToken: context.CancellationToken);
                        var users = await this.contextRoleManager.GetCardContextUsersAsync(
                            contextRole, 
                            mainCardID, 
                            cancellationToken: context.CancellationToken);
                        if (users.Count > 0)
                        {
                            senderID = users[0].UserID;
                        }
                        break;

                    default:
                        context.ValidationResult.AddError(this, "$KrProcess_Acquaintance_SenderShoudBePersonalOrContext");
                        return;
                }
            }

            var result = await this.acquaintanceManager.SendAsync(
                mainCardID,
                roleIDs,
                excludeDeputies,
                comment,
                placeholderAliases,
                null,
                notificationID,
                senderID,
                cancellationToken: context.CancellationToken);

            if (!result.IsSuccessful || result.HasWarnings)
            {
                context.ValidationResult.Add(result);
            }
        }

        #endregion

    }
}
