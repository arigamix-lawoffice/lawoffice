using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Files
{
    public sealed class ServerKrPermissionsGetFileVersionsExtension :
        CardGetFileVersionsExtension
    {
        #region Fields

        private readonly IKrPermissionsManager permissionsManager;

        #endregion

        #region Constructors

        public ServerKrPermissionsGetFileVersionsExtension(IKrPermissionsManager permissionsManager)
        {
            this.permissionsManager = permissionsManager;
        }

        #endregion

        #region Base Overrides

        public override async Task BeforeRequest(ICardGetFileVersionsExtensionContext context)
        {
            // если запрос уже пришёл с сервера, то не проверяем права
            if (context.Request.ServiceType == CardServiceType.Default)
            {
                return;
            }

            Guid? cardID = context.Request.CardID;
            if (!cardID.HasValue)
            {
                return;
            }

            var result = await KrFileAccessHelper.CheckAccessAsync(
                context.Request,
                context,
                cardID.Value,
                permissionsManager);

            // Проверяем доступ
            if (result != null
                && result.Result)
            {
                context.Info[nameof(ServerKrPermissionsGetFileVersionsExtension)] = result.Info;
            }
        }

        public override Task AfterRequest(ICardGetFileVersionsExtensionContext context)
        {
            ListStorage<CardFileVersion> fileVersions;
            if (context.RequestIsSuccessful
                && (fileVersions = context.Response.TryGetFileVersions()) != null
                && context.Info.TryGetValue(nameof(ServerKrPermissionsGetFileVersionsExtension), out var obj)
                && obj is Dictionary<string, object> info
                && info.TryGetValue(KrPermissionsHelper.FileReadAccessSettings.InfoKey, out var accessSettingObj)
                && accessSettingObj is int accessSetting)
            {
                var lastVersionNum = fileVersions.Max(x => x.Number);
                if (accessSetting == KrPermissionsHelper.FileReadAccessSettings.OnlyLastVersion)
                {
                    fileVersions.RemoveAll(x => x.Number != lastVersionNum);
                }
                else if (accessSetting == KrPermissionsHelper.FileReadAccessSettings.OnlyLastAndOwnVersions)
                {
                    fileVersions.RemoveAll(x => x.Number != lastVersionNum && x.CreatedByID != context.Session.User.ID);
                }
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}