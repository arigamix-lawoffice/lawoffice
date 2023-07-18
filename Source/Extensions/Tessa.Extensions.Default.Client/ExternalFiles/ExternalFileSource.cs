using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Files;
using Tessa.Platform.Runtime;
using Unity;

namespace Tessa.Extensions.Default.Client.ExternalFiles
{
    public class ExternalFileSource : FileSource
    {
        #region Constructors

        public ExternalFileSource(
            IFileCache cache,
            ISession session,
            [OptionalDependency] IFileManager manager = null)
            : base(cache, session, manager)
        {
        }

        #endregion

        #region IFileSource Items

        protected override async ValueTask<IFile> CreateFileCoreAsync(
            IFileCreationToken token,
            IFileContent content = null,
            CancellationToken cancellationToken = default)
        {
            // здесь возможно исключение, связанное с параметрами метода
            var typedToken = (ExternalFileCreationToken)token;

            string name = token.Name;
            IFileContent allocatedContent = null;
            IFileContent actualContent = content
                ?? (allocatedContent = await this.Cache.AllocateAsync(name, cancellationToken: cancellationToken).ConfigureAwait(false));

            try
            {
                // все свойства токена проверяются в конструкторе
                return new ExternalFile(
                    token.ID ?? Guid.NewGuid(),
                    name,
                    token.Size,
                    token.Category,
                    token.Type,
                    actualContent,
                    this,
                    token.Modified,
                    token.ModifiedByID,
                    token.ModifiedByName,
                    token.Created,
                    token.CreatedByID,
                    token.CreatedByName,
                    token.Permissions.Clone(),
                    token.IsLocal,
                    description: typedToken.Description);
            }
            catch (Exception)
            {
                if (allocatedContent != null)
                {
                    await allocatedContent.DisposeAsync().ConfigureAwait(false);
                }
                
                throw;
            }
        }

        protected override ValueTask<IFileCreationToken> GetFileCreationTokenCoreAsync(
            CancellationToken cancellationToken = default)
        {
            var user = this.Session.User;
            var now = DateTime.UtcNow;
            var token = new ExternalFileCreationToken
            {
                Modified = now,
                ModifiedByID = user.ID,
                ModifiedByName = user.Name,
                Created = now,
                CreatedByID = user.ID,
                CreatedByName = user.Name,
            };
            return new(token);
        }

        protected override async ValueTask<IFileContentResponse> GetContentCoreAsync(
            IFileContentRequest request,
            CancellationToken cancellationToken = default)
        {
            string content = ((ExternalFile) request.Version.File).Description ?? string.Empty;

            await using (var stream = new MemoryStream(Encoding.UTF8.GetBytes(content)))
            {
                Func<Stream, CancellationToken, ValueTask> processContentActionAsync =
                    request.ProcessContentActionAsync ?? ((s, ct2) => new ValueTask());

                await processContentActionAsync(stream, cancellationToken).ConfigureAwait(false);
            }

            return new FileContentResponse(request.Version);
        }

        #endregion
    }
}
