using System.IO;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Files
{
    /// <summary>
    /// Расширение можно вызвать не только с сервера, но и с клиента.
    /// </summary>
    public sealed class EmbeddedDataGetFileContentExtension :
        CardGetFileContentExtension
    {
        public override Task BeforeRequest(ICardGetFileContentExtensionContext context)
        {
            byte[] data = context.Request.Info.TryGet<byte[]>("Data");
            if (data == null || context.Response != null)
            {
                return Task.CompletedTask;
            }

            context.ContentFuncAsync = ct => new ValueTask<Stream>(new MemoryStream(data));
            context.Response = new CardGetFileContentResponse { Size = data.Length };

            return Task.CompletedTask;
        }
    }
}
