using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;
using Tessa.Roles;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public sealed class KrAcquaintanceSettingsStoreExtension : CardStoreExtension
    {
        #region Fields

        private readonly IRoleGetStrategy roleGetStrategy;

        #endregion

        #region Constructors

        public KrAcquaintanceSettingsStoreExtension(
            IRoleGetStrategy roleGetStrategy)
        {
            this.roleGetStrategy = roleGetStrategy;
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardStoreExtensionContext context)
        {
            var card = context.Request.Card;

            if (card.Sections.TryGetValue(KrConstants.KrStages.Virtual, out var stageSection))
            {
                foreach (var row in stageSection.Rows)
                {
                    Guid? senderID;
                    if (row.State != CardRowState.Deleted
                        && (senderID = row.TryGet<Guid?>(KrConstants.KrAcquaintanceSettingsVirtual.SenderID)).HasValue)
                    {
                        var role = await roleGetStrategy.GetRoleParamsAsync(senderID.Value, context.CancellationToken);
                        if (role.Type != RoleType.Context
                            && role.Type != RoleType.Personal)
                        {
                            context.ValidationResult.AddError(this, "$KrProcess_Acquaintance_SenderShoudBePersonalOrContext");
                            return;
                        }
                    }
                }
            }
        }

        #endregion
    }
}
