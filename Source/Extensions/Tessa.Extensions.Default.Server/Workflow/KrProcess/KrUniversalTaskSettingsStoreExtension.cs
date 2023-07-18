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
    public sealed class KrUniversalTaskSettingsStoreExtension : CardStoreExtension
    {
        #region Fields

        private readonly IRoleGetStrategy roleGetStrategy;

        #endregion

        #region Constructors

        public KrUniversalTaskSettingsStoreExtension(
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
                    Guid? authorID;
                    if (row.State != CardRowState.Deleted
                        && (authorID = row.TryGet<Guid?>(KrConstants.KrUniversalTaskSettingsVirtual.AuthorID)).HasValue)
                    {
                        var role = await roleGetStrategy.GetRoleParamsAsync(authorID.Value);
                        if (role.Type != RoleType.Context && role.Type != RoleType.Personal)
                        {
                            context.ValidationResult.AddError(this, "$KrProcess_UniversalTask_AuthorShoudBePersonalOrContext");
                            return;
                        }
                    }
                }
            }

            if (card.Sections.TryGetValue(KrConstants.KrUniversalTaskOptionsSettingsVirtual.Synthetic, out var optionsSection))
            {
                foreach (var row in optionsSection.Rows)
                {
                    if (row.State != CardRowState.Deleted
                        && string.IsNullOrEmpty(row.TryGet(KrConstants.KrUniversalTaskOptionsSettingsVirtual.Caption, "CaptionAlreadyExist")))
                    {
                        context.ValidationResult.AddError(this, "$KrProcess_UniversalTask_CompletionOptionCaptionEmpty");
                        return;
                    }
                }
            }
        }

        #endregion
    }
}
