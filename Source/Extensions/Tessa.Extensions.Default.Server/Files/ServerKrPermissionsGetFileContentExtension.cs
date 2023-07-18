using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;

namespace Tessa.Extensions.Default.Server.Files
{
    public sealed class ServerKrPermissionsGetFileContentExtension :
        CardGetFileContentExtension
    {
        #region Fields

        private readonly IKrPermissionsManager permissionsManager;

        #endregion

        #region Constructors

        public ServerKrPermissionsGetFileContentExtension(IKrPermissionsManager permissionsManager)
        {
            this.permissionsManager = permissionsManager;
        }

        #endregion

        #region Base Overrides

        public override Task BeforeRequest(ICardGetFileContentExtensionContext context)
        {
            // если запрос уже пришёл с сервера, то не проверяем права
            if (context.Request.ServiceType == CardServiceType.Default)
            {
                return Task.CompletedTask;
            }

            Guid? cardID = context.Request.CardID;
            if (!cardID.HasValue)
            {
                return Task.CompletedTask;
            }

            return KrFileAccessHelper.CheckAccessAsync(
                context.Request,
                context,
                cardID.Value,
                permissionsManager,
                context.CancellationToken);
        }

        #endregion
    }
}
