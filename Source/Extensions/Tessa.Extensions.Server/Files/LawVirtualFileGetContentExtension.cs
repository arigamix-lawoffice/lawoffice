using System;
using System.IO;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Shared.Info;
using Tessa.Platform.IO;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Server.Files
{
    /// <summary>
    /// Расширение на процесс получения файла LawVirtualFile.
    /// </summary>
    public sealed class LawVirtualFileGetContentExtension : CardGetFileContentExtension
    {
        #region Base overrides

        /// <inheritdoc />
        public override Task BeforeRequest(ICardGetFileContentExtensionContext context)
        {
            string? path;
            if (string.IsNullOrEmpty(path = context.Request.Info.TryGet<string>(InfoMarks.Path))
                || !context.Request.FileID.HasValue
                || string.IsNullOrEmpty(context.Request.FileName)
                || context.Response != null)
            {
                return Task.CompletedTask;
            }

            var fileFullName = Path.Combine(path, context.Request.FileID.Value.ToString(), context.Request.FileName);
            var stream = FileHelper.OpenRead(fileFullName);
            var info = new FileInfo(fileFullName);
            long size = info.Length;

            context.ContentFuncAsync = ct =>
            {
                return new ValueTask<Stream?>(stream);
            };
            context.Response = new CardGetFileContentResponse { Size = size };

            return Task.CompletedTask;
        }

        #endregion
    }
}