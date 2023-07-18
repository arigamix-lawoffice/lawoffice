using System;
using System.IO;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Platform;
using Tessa.Platform.IO;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Server.Files
{
    /// <summary>
    /// Расширение устанавливает признак необходимости преобразования txt файла в UTF-8, если запрос пришел с web-клиента.
    /// </summary>
    public sealed class SetUpTranslateTxtToUtf8GetFileContentExtension :
        CardGetFileContentExtension
    {
        #region Base Overrides

        /// <inheritdoc/>
        public override Task BeforeRequest(ICardGetFileContentExtensionContext context)
        {
            if (context.Session.ApplicationID != ApplicationIdentifiers.WebClient
                || string.IsNullOrEmpty(context.Request.FileName)
                || !string.Equals(FileHelper.GetExtension(context.Request.FileName), ".txt", StringComparison.OrdinalIgnoreCase))
            {
                return Task.CompletedTask;
            }

            context.Request.Info[CardHelper.TranslateTxtToUtf8InfoKey] = BooleanBoxes.True;

            return Task.CompletedTask;
        }

        #endregion
    }
}
