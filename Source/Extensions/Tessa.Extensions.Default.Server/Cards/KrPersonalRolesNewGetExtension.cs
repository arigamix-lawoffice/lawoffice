using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Platform.Shared.Initialization;
using Tessa.Platform;
using Tessa.Platform.Collections;
using Tessa.Platform.Runtime;
using Tessa.Platform.Storage;
using Tessa.Roles;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Расширение для предоставления доступа сотрудникам загружать карточку сотрудника для настроек.
    /// </summary>
    public sealed class KrPersonalRolesNewGetExtension : CardNewGetExtension
    {
        #region Fields

        private readonly IKrTypesCache typesCache;

        #endregion

        #region Constructors

        public KrPersonalRolesNewGetExtension(
            IKrTypesCache typesCache)
        {
            Check.ArgumentNotNull(typesCache, nameof(typesCache));

            this.typesCache = typesCache;
        }

        #endregion

        #region New Overrides

        public override async Task AfterRequest(ICardNewExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                || context.Session.User.IsAdministrator()
                || !await KrComponentsHelper.HasBaseAsync(context.Response.Card.TypeID, this.typesCache, context.CancellationToken))
            {
                return;
            }

            await ProhibitSectionsAfterRequestAsync(context.Response.Card, context.CancellationToken);
        }

        #endregion

        #region Get Overrides

        public override async Task BeforeRequest(ICardGetExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful()
                || context.Request.CardTypeID is null
                || !context.Request.Info.TryGet<bool>(CardHelper.UserSettingsOnlyKey)
                || !(context.Request.CardID == context.Session.User.ID 
                    || context.Session.User.IsAdministrator())
                || !await KrComponentsHelper.HasBaseAsync(context.Request.CardTypeID.Value, this.typesCache, context.CancellationToken))
            {
                return;
            }

            var serverToken = context.Info.GetOrCreateServerToken();
            serverToken.AddPermission(KrPermissionFlagDescriptors.ReadCard);

            // Не загружаем никаких секций карточки, кроме секций с настройками и PersonalRoleVirtualSection.
            var personalRoleMetadata = await context.CardMetadata.GetMetadataForTypeAsync(context.Request.CardTypeID.Value, context.CancellationToken);
            var personalRoleSections = await personalRoleMetadata.GetSectionsAsync(context.CancellationToken);
            var settingsMetadata = await context.CardMetadata.GetMetadataForTypeAsync(CardHelper.UserSettingsSystemTypeTypeID, context.CancellationToken);
            var settingsSectionsHash = (await personalRoleMetadata.GetSectionsAsync(context.CancellationToken)).Select(x => x.Name).ToHashSet();
            context.Request.SectionsToExclude.AddRange(
                personalRoleSections
                    .Where(x => x.Name != InitializationExtensionHelper.PersonalRoleVirtualSection
                        && !settingsSectionsHash.Contains(x.Name))
                    .Select(x => x.Name));
        }

        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                || context.Session.User.IsAdministrator()
                || !await KrComponentsHelper.HasBaseAsync(context.Response.Card.TypeID, this.typesCache, context.CancellationToken))
            {
                return;
            }

            await ProhibitSectionsAfterRequestAsync(context.Response.Card, context.CancellationToken);
        }

        #endregion

        #region Private Methods

        private async Task ProhibitSectionsAfterRequestAsync(Card card, CancellationToken cancellationToken)
        {
            if (!card.Permissions.Sections.TryGetValue("PersonalRoleStaticRolesVirtual", out var staticRolesPermissions)
                || staticRolesPermissions.SectionPermissions.HasNot(CardPermissionFlags.AllowModify)
                || !await KrComponentsHelper.HasBaseAsync(RoleHelper.StaticRoleTypeID, this.typesCache, cancellationToken))
            {
                staticRolesPermissions ??= card.Permissions.Sections.GetOrAddTable("PersonalRoleStaticRolesVirtual");
                staticRolesPermissions.SetSectionPermissions(CardPermissionFlags.ProhibitModify);
            }

            if (!card.Permissions.Sections.TryGetValue("PersonalRoleDepartmentsVirtual", out var departmentsPermissions)
                || departmentsPermissions.SectionPermissions.HasNot(CardPermissionFlags.AllowModify)
                || !await KrComponentsHelper.HasBaseAsync(RoleHelper.DepartmentRoleTypeID, this.typesCache, cancellationToken))
            {
                departmentsPermissions ??= card.Permissions.Sections.GetOrAddTable("PersonalRoleDepartmentsVirtual");
                departmentsPermissions.SetSectionPermissions(CardPermissionFlags.ProhibitModify);
            }
        }

        #endregion
    }
}
