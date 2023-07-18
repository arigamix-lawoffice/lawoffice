using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;

namespace Tessa.Extensions.Default.Server.Cards
{
    public class KrPermissionRuleDeleteExtension : CardDeleteExtension
    {
        #region Fields

        private readonly IKrPermissionsCacheContainer permissionsCache;

        #endregion

        #region Constructors

        public KrPermissionRuleDeleteExtension(
            IKrPermissionsCacheContainer permissionsCache)
        {
            this.permissionsCache = permissionsCache;
        }

        #endregion

        #region Base Overrides

        public override async Task AfterRequest(ICardDeleteExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return;
            }

            // Сбрасываем кеш правил доступа при удалении карточки правил доступа
            await permissionsCache.UpdateVersionAsync(context.CancellationToken);
        }

        #endregion
    }
}
