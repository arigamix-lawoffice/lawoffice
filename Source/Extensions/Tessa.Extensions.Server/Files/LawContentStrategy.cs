using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.ComponentModel;
using Tessa.Extensions.Shared.Extensions;
using Tessa.Extensions.Shared.Info;
using Tessa.Platform;
using Tessa.Platform.Data;
using Tessa.Platform.IO;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Server.Files
{
    /// <summary>
    /// Стратегия хранения файлов. 
    /// Реализовано только добавление, так как карточка виртуальная и
    /// получение данных в стратегии не будет выполняться, будет ошибка что не удалось найти версию
    /// (данные получаются в LawVirtualFileGetContentExtension).
    /// </summary>
    public sealed class LawContentStrategy : ICardContentStrategy
    {
        private readonly IDbScope dbScope;
        private readonly string fileBasePath;

        public LawContentStrategy(IDbScope dbScope, ICardFileSource settings)
        {
            this.dbScope = dbScope;
            this.fileBasePath = settings.Path;
        }

        /// <inheritdoc />
        public async ValueTask<Stream> GetAsync(
            CardContentContext context,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Getting the file content is not implemented.");
        }

        /// <inheritdoc />
        public async ValueTask<long> GetSizeAsync(
            CardContentContext context,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Getting the file size is not implemented.");
        }

        /// <inheritdoc />
        public async Task StoreAsync(
            CardContentContext context,
            Stream contentStream,
            CancellationToken cancellationToken = default)
        {
            string fileFolderPath = this.GetFileFolderPath(context.FileID);
            FileHelper.CreateDirectoryIfNotExists(fileFolderPath, true);

            var fileName = await this.GetFileNameAsync(context.FileID, cancellationToken);
            if (string.IsNullOrEmpty(fileName))
            {
                ValidationSequence
                   .Begin(context.ValidationResult)
                   .SetObjectName(this)
                   .Error(
                       CardValidationKeys.FileContentNotFound,
                       context.CardID,
                       context.FileID,
                       context.VersionRowID)
                   .End();
                return;
            }

            string filePath = GetContentPath(fileName, fileFolderPath);
            await using FileStream fileStream = FileHelper.Create(filePath, bufferSize: FileHelper.DefaultCopyBufferSize);
            await contentStream.CopyToAsync(fileStream, cancellationToken).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public async Task DeleteAsync(
            CardContentContext context,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("File deletion is not implemented.");
        }

        /// <inheritdoc />
        public async Task<bool> CopyAsync(
            CardContentContext sourceContext,
            CardContentContext targetContext,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("File copying is not implemented.");
        }

        /// <inheritdoc />
        public async Task<bool> MoveAsync(
            CardContentContext sourceContext,
            CardContentContext targetContext,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Moving the file is not implemented.");
        }

        /// <inheritdoc />
        public async Task CleanCardAsync(
            Guid cardID,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Card deletion is not implemented.");
        }

        /// <inheritdoc />
        public async Task CleanFileAsync(
            Guid cardID,
            Guid fileID,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("File deletion is not implemented.");
        }

        /// <inheritdoc />
        public async Task MoveFilesAsync(
            Guid sourceCardID,
            Guid targetCardID,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Moving the file is not implemented.");
        }

        /// <inheritdoc />
        public async Task MoveFileAsync(
            Guid sourceCardID,
            Guid sourceFileID,
            Guid targetCardID,
            Guid targetFileID,
            CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException("Moving the file is not implemented.");
        }

        #region Private Methods

        private string GetFileFolderPath(Guid fileID)
        {
            // BaseFolder\fileID
            string fileFolder = fileID.ToString("D");
            return Path.Combine(this.fileBasePath, fileFolder);
        }

        private static string GetContentPath(string fileName, string fileFolderPath)
        {
            // BaseFolder\fileID => BaseFolder\fileID\fileName
            return Path.Combine(fileFolderPath, fileName);
        }

        private async Task<string?> GetFileNameAsync(
            Guid fileID, 
            CancellationToken cancellationToken = default)
        {
            await using (this.dbScope.CreateNew(ExtSchemeInfo.ConnectionString))
            {
                return await this.dbScope.GetFieldAsync<string>(fileID,
                    ExtSchemeInfo.Datoteka,
                    ExtSchemeInfo.Datoteka.Ime,
                    ExtSchemeInfo.Datoteka.Uid,
                    cancellationToken);
            }
        }

        #endregion
    }
}
