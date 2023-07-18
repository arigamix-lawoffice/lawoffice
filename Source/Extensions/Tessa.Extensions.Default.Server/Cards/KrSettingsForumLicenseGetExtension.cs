using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;
using Tessa.Forums;
using Tessa.Platform;
using Tessa.Platform.Licensing;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Cards
{
    public sealed class KrSettingsForumLicenseGetExtension : CardGetExtension
    {
        #region Fields

        private readonly ILicenseManager licenseManager;

        #endregion

        #region Constructors

        public KrSettingsForumLicenseGetExtension(ILicenseManager licenseManager)
        {
            this.licenseManager = licenseManager;
        }

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardGetExtensionContext context)
        {
            if (!context.RequestIsSuccessful ||
                (await this.licenseManager.GetLicenseAsync(context.CancellationToken))
                .Modules.HasEnterpriseOrContains(LicenseModules.ForumsID))
            {
                return;
            }

            // расширение зарегистрировано только для этих двух типов карточек
            Card card = context.Response.Card;
            if (context.CardTypeIs(DefaultCardTypes.KrSettingsTypeID))
            {
                var sectionPermissions = card.Permissions.Sections.GetOrAddTable("KrSettingsCardTypes");
                sectionPermissions.FieldPermissions["UseForum"] = CardPermissionFlags.ProhibitModify;

                ListStorage<CardRow> rows = card.TryGetSections()?.TryGet("KrSettingsCardTypes")?.TryGetRows();
                if (rows?.Count > 0)
                {
                    foreach (CardRow row in rows)
                    {
                        // если флаг уже выставлен, то позволяем его снять для этой строки
                        if (row.TryGet<bool>("UseForum"))
                        {
                            sectionPermissions.Rows.GetOrAdd(row.RowID).FieldPermissions["UseForum"] = CardPermissionFlags.AllowModify;
                        }
                    }
                }
            }
            else if (context.CardTypeIs(DefaultCardTypes.KrDocTypeTypeID))
            {
                bool useForum = card.TryGetSections()?.TryGet("KrDocType")?.TryGetRawFields()?.TryGet<bool>("UseForum") ?? false;

                // если флаг ещё не выставлен, то не позволяем его установить
                if (!useForum)
                {
                    card.Permissions.Sections.GetOrAddEntry("KrDocType").FieldPermissions["UseForum"] = CardPermissionFlags.ProhibitModify;
                }
            }

            card.Info[ForumHelper.LicenseWarningFlag] = BooleanBoxes.True;
        }

        #endregion
    }
}