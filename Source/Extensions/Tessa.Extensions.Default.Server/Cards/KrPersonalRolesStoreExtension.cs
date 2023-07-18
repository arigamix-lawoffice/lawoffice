using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Platform.Shared.Initialization;
using Tessa.Roles;
using Tessa.Extensions.Platform.Server.Roles;
using Tessa.Cards;
using Tessa.Platform.Runtime;
using System.Linq;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Cards
{
    /// <summary>
    /// Расширение для предоставления доступа сотрудникам редактировать свои настройки
    /// и для установки прав на редактирование некоторых полей сотрудника, заполняемых в расширениях <see cref="PersonalRoleStoreExtension"/>
    /// и <see cref="FixPersonalRolesStoreExtension" />
    /// </summary>
    public sealed class KrPersonalRolesStoreExtension : CardStoreExtension
    {
        #region Fields

        private readonly IKrTypesCache typesCache;

        #endregion

        #region Constructors

        public KrPersonalRolesStoreExtension(
            IKrTypesCache typesCache)
        {
            this.typesCache = typesCache;
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful()
                || !await KrComponentsHelper.HasBaseAsync(context.Request.Card.TypeID, typesCache, context.CancellationToken))
            {
                return;
            }

            var card = context.Request.Card;
            var user = context.Session.User;
            if (card.ID == user.ID
                || user.IsAdministrator())
            {
                // Всегда разрешаем редактировать свои настройки и менять свой пароль через вкладку "Мои настройки"
                await context.SetCardAccessAsync(
                    InitializationExtensionHelper.PersonalRoleVirtualSection,
                    InitializationExtensionHelper.PersonalRoleSettingsField,
                    InitializationExtensionHelper.PersonalRoleNotificationSettingsField);
                
                if (context.Request.ServiceType == CardServiceType.Default)
                {
                    await context.SetCardAccessAsync(
                        InitializationExtensionHelper.PersonalRoleVirtualSection,
                        "Password",
                        "PasswordRepeat");

                    await context.SetCardAccessAsync(
                        RoleStrings.PersonalRoles,
                        "PasswordKey",
                        "PasswordHash",
                        "PasswordChanged");
                }
            }

            // Для новой карточки разрешаем изменение информации, заполненное системой
            if (card.StoreMode == Tessa.Cards.CardStoreMode.Insert)
            {
                await context.SetCardAccessAsync(
                    InitializationExtensionHelper.PersonalRoleVirtualSection,
                    InitializationExtensionHelper.PersonalRoleSettingsField,
                    InitializationExtensionHelper.PersonalRoleNotificationSettingsField);

                await context.SetCardAccessAsync(
                    RoleStrings.Roles,
                    "Name");

                await context.SetCardAccessAsync(
                    RoleStrings.PersonalRoles,
                    "Name",
                    "FullName",
                    "PasswordChanged");

                await context.SetCardAccessAsync(
                    RoleStrings.RoleUsers);
            }

            if (user.IsAdministrator()
                || user.ID == card.ID
                || (card.Sections.Any(x => RoleHelper.PersonalRoleDeputiesSections.Contains(x.Key))
                    && await RoleHelper.CanEditDeputies(
                        context.DbScope,
                        card.ID,
                        user.ID,
                        context.CancellationToken)))
            {
                // Даем права доступа на редактирование заместителей, если сотрудник меняет свою карточки, 
                // или сотрудник Администратор системы, или он есть в списке тех, кто может менять заместителей для текущего сотрудника
                foreach (var sectionName in RoleHelper.PersonalRoleDeputiesSections)
                {
                    await context.SetCardAccessAsync(sectionName);
                }
            }

            // Токен может и не прокидываться, поэтому укажем, что не нужно писать Warning
            context.Request.SetIgnorePermissionsWarning();
        }

        #endregion
    }
}
