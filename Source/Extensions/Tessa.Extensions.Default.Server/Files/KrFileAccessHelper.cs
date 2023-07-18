using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Files
{
    public static class KrFileAccessHelper
    {
        #region Static Methods

        /// <summary>
        /// Метод проверяет права доступа к файлу и возвращает результат проверки прав, или null, если проверка прав доступа не требуется.
        /// </summary>
        public static async Task<KrPermissionsManagerCheckResult> CheckAccessAsync(
            CardFileRequestBase request,
            ICardExtensionContext context,
            Guid cardID,
            IKrPermissionsManager permissionsManager,
            CancellationToken cancellationToken = default)
        {
            var permContext = await permissionsManager.TryCreateContextAsync(
                new KrPermissionsCreateContextParams
                {
                    CardID = cardID,
                    FileID = request.FileID,
                    FileVersionID = request is CardGetFileContentRequest getFileContentRequest ? getFileContentRequest.VersionRowID : null,
                    ValidationResult = context.ValidationResult,
                    AdditionalInfo = context.Info,
                    ExtensionContext = context,
                    ServerToken = context.Info.TryGetServerToken(),
                    WithExtendedPermissions = true,
                    PrevToken = KrToken.TryGet(request.Info),
                },
                cancellationToken: context.CancellationToken);

            if (permContext != null)
            {
                return await permissionsManager.CheckRequiredPermissionsAsync(
                    permContext,
                    KrPermissionFlagDescriptors.ReadCard);
            }

            return null;
        }

        #endregion
    }
}