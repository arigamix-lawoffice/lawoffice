using System.IO;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.IO;
using Tessa.Platform.Storage;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Files
{
    /// <summary>
    /// Расширение можно вызвать только с сервера (в т.ч. из серверных плагинов Chronos).
    /// </summary>
    public sealed class ServerPathGetFileContentExtension :
        CardGetFileContentExtension
    {
        public override Task BeforeRequest(ICardGetFileContentExtensionContext context)
        {
            string filePath;
            if (context.Request.ServiceType != CardServiceType.Default
                || string.IsNullOrEmpty(filePath = context.Request.Info.TryGet<string>("ServerFilePath"))
                || context.Response != null)
            {
                return Task.CompletedTask;
            }

            if (!File.Exists(filePath))
            {
                context.ValidationResult.AddError(this, "Can't find file: {0}", filePath);
                return Task.CompletedTask;
            }

            var info = new FileInfo(filePath);
            long size = info.Length;

            context.ContentFuncAsync = ct => new ValueTask<Stream>(FileHelper.OpenRead(filePath));
            context.Response = new CardGetFileContentResponse { Size = size };

            return Task.CompletedTask;
        }
    }
}
