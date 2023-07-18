using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform.Data;
using Tessa.Platform.Licensing;
using Tessa.Platform.Storage;
using Tessa.Roles.Acl;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Расширение прав доступа, которое выдает доступ на чтение карточки, если в карточке настроек типового решения установлен флаг "Доступ ACL на чтение карточки"
    /// и сотрудник входит в ACL карточки.
    /// </summary>
    public sealed class KrAclReadAccessCardPermissionExtension : ICardPermissionsExtension
    {
        #region Fields

        private readonly ILicenseManager licenseManager;
        private readonly ICardCache cardCache;

        #endregion

        #region Constructors

        public KrAclReadAccessCardPermissionExtension(
            ILicenseManager licenseManager,
            ICardCache cardCache)
        {
            this.licenseManager = NotNullOrThrow(licenseManager);
            this.cardCache = NotNullOrThrow(cardCache);
        }

        #endregion

        #region ICardPermissionsExtension Implementation

        /// <inheritdoc/>
        public async Task ExtendPermissionsAsync(IKrPermissionsManagerContext context)
        {
            if (context.Mode == KrPermissionsCheckMode.WithoutCard
                || context.CardID is null
                || !context.Descriptor.StillRequired.Contains(KrPermissionFlagDescriptors.ReadCard))
            {
                return;
            }

            var license = await this.licenseManager.GetLicenseAsync(context.CancellationToken);
            if (!license.Modules.HasEnterpriseOrContains(LicenseModules.AclID))
            {
                return;
            }

            var settingsCardCacheValue = await this.cardCache.Cards.GetAsync(DefaultCardTypes.KrSettingsTypeName, context.CancellationToken);
            if (!settingsCardCacheValue.IsSuccess)
            {
                context.ValidationResult.Add(settingsCardCacheValue.ValidationResult);
                return;
            }

            var card = settingsCardCacheValue.GetValue();
            if (card.Sections["KrSettings"].RawFields.TryGet<bool>("AclReadCardAccess")
                && await HasAclAccessAsync(context.CardID.Value, context.Session.User.ID, context.DbScope, context.CancellationToken))
            {
                context.Descriptor.Set(KrPermissionFlagDescriptors.ReadCard, true);
            }
        }

        /// <inheritdoc/>
        public Task IsPermissionsRecalcRequired(IKrPermissionsRecalcContext context)
        {
            return Task.CompletedTask;
        }

        #endregion

        #region Private Methods

        private static async Task<bool> HasAclAccessAsync(
            Guid cardID,
            Guid userID,
            IDbScope dbScope,
            CancellationToken cancellationToken)
        {
            await using var _ = dbScope.Create();

            var db = dbScope.Db;

            return await db.SetCommand(
                dbScope.BuilderFactory
                    .Select().Top(1).V(true)
                    .From(AclHelper.AclSection, "a").NoLock()
                    .InnerJoin("RoleUsers", "ru").NoLock()
                        .On().C("a", "RoleID").Equals().C("ru", "ID")
                    .Where().C("a", "ID").Equals().P("CardID")
                        .And().C("ru", "UserID").Equals().P("UserID")
                    .Limit(1)
                    .Build(),
                db.Parameter("CardID", cardID, LinqToDB.DataType.Guid),
                db.Parameter("UserID", userID, LinqToDB.DataType.Guid))
                .LogCommand()
                .ExecuteAsync<bool>(cancellationToken);
        }

        #endregion
    }
}
