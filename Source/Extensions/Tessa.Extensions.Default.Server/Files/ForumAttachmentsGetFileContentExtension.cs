using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Forums;
using Tessa.Platform.Data;

namespace Tessa.Extensions.Default.Server.Files
{
    /// <summary>
    /// Расширение получает имя файла для вложения форума, ориентируясь на флаг <c>ForumHelper.ForumAttachmentKey</c>.
    /// </summary>
    public class ForumAttachmentsGetFileContentExtension : CardGetFileContentExtension
    {
        #region Base Overrides

        public override async Task AfterRequest(ICardGetFileContentExtensionContext context)
        {
            if (!context.Request.Info.TryGetValue(ForumHelper.ForumAttachmentKey, out object? isForumAttachment) || 
                isForumAttachment as bool? != true)
            {
                return;
            }

            await using (context.DbScope.Create())
            {
                var db = context.DbScope.Db;
                var fileName = await db.SetCommand(context.DbScope.BuilderFactory.
                        Select().
                        C("f", "Name").
                        From("Files", "f").NoLock().
                        Where().
                        C("f", "RowID").Equals().P("ID").Build(), db.Parameter("ID", context.Request.FileID))
                    .LogCommand()
                    .ExecuteAsync<string>(context.CancellationToken);
                context.Response.SetSuggestedFileName(fileName);
            }
        }

        #endregion
    }
}
