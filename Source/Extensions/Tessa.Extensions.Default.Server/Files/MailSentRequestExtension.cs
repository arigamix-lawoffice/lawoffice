using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Notices;
using Tessa.Platform.IO;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Files
{
    /// <summary>
    /// Расширение можно вызвать только с сервера (в т.ч. из серверных плагинов Chronos).
    /// </summary>
    public sealed class MailSentRequestExtension :
        CardRequestExtension
    {
        public override Task AfterRequest(ICardRequestExtensionContext context)
        {
            Dictionary<string, object> mainInfoStorage;

            Dictionary<string, object> info;
            if (!context.RequestIsSuccessful
                || context.Request.ServiceType != CardServiceType.Default
                || (info = context.Request.TryGetInfo()) == null
                || (mainInfoStorage = info.TryGet<Dictionary<string, object>>("MailInfo")) == null)
            {
                return Task.CompletedTask;
            }

            var mailInfo = new MailInfo(mainInfoStorage);

            ListStorage<MailFile> files = mailInfo.TryGetFiles();
            if (files != null && files.Count > 0)
            {
                foreach (MailFile file in files)
                {
                    string filePath = file.Info.TryGet<string>("ServerFilePath");

                    if (!string.IsNullOrEmpty(filePath)
                        && file.Info.TryGet<bool>("RemoveFile")
                        && File.Exists(filePath))
                    {
                        bool removeFolder = file.Info.TryGet<bool>("RemoveFolder");
                        FileHelper.ReleaseFilePath(filePath, keepFolder: !removeFolder);
                    }
                }
            }

            return Task.CompletedTask;
        }
    }
}
