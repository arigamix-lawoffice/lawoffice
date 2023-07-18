#nullable enable

using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tessa.FileConverters;
using Tessa.Platform.Data;
using Tessa.Platform.IO;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.OnlyOffice
{
    /// <inheritdoc />
    public sealed class OnlyOfficeFileCache :
        IOnlyOfficeFileCache
    {
        #region Constructors

        public OnlyOfficeFileCache(
            IDbScope dbScope,
            ISession session,
            IOnlyOfficeFileCacheInfoStrategy cacheInfoStrategy,
            IFileConverterCache fileConverterCache)
        {
            this.dbScope = dbScope ?? throw new ArgumentNullException(nameof(dbScope));
            this.session = session ?? throw new ArgumentNullException(nameof(session));
            this.cacheInfoStrategy = cacheInfoStrategy ?? throw new ArgumentNullException(nameof(cacheInfoStrategy));
            this.fileConverterCache = fileConverterCache ?? throw new ArgumentNullException(nameof(fileConverterCache));
        }

        #endregion

        #region Fields

        private readonly IDbScope dbScope;

        private readonly ISession session;

        private readonly IOnlyOfficeFileCacheInfoStrategy cacheInfoStrategy;

        private readonly IFileConverterCache fileConverterCache;

        #endregion

        #region IOnlyOfficeFileCache Members

        /// <inheritdoc />
        public async ValueTask<ValidationResult> CreateAsync(
            Guid id,
            Guid sourceFileVersionID,
            string sourceFileName,
            Stream stream,
            CancellationToken cancellationToken = default)
        {
            ValidationResult result;

            await using var _ = this.dbScope.Create();
            using (ITempFile file = TempFile.Acquire(sourceFileName))
            {
                await using (FileStream fileStream = FileHelper.Create(file.Path))
                {
                    await stream.CopyToAsync(fileStream, FileHelper.DefaultFileBufferSize, cancellationToken);
                }

                // пользователь без прав админа не может изменять карточку "Кэш файлов", поскольку это синглтон;
                // временно добавляем ему права администратора
                await using var __ = this.session.User.IsAdministrator() ? null : SessionContext.Create(
                    this.session.CreateNestedSessionToken(this.session.User.ID, this.session.User.Name));

                result = await this.fileConverterCache.StoreFileAsync(
                    sourceFileVersionID,
                    null,
                    id,
                    sourceFileName,
                    file.Path,
                    cancellationToken: cancellationToken);
            }

            if (!result.IsSuccessful)
            {
                return result;
            }

            try
            {
                // для совместного редактирования уже будет строка
                var info = await this.cacheInfoStrategy.TryGetInfoAsync(id, cancellationToken);

                if (info is null)
                {
                    await this.cacheInfoStrategy.InsertAsync(
                        new OnlyOfficeFileCacheInfo
                        {
                            ID = id,
                            SourceFileVersionID = sourceFileVersionID,
                            SourceFileName = sourceFileName,
                            CreatedByID = this.session.User.ID,
                            LastAccessTime = DateTime.UtcNow,
                        },
                        cancellationToken);
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception ex)
            {
                result = ValidationResult.Aggregate(ValidationResult.FromException(this, ex), result);
            }

            return result;
        }


        /// <inheritdoc />
        public async ValueTask<(ValidationResult Result, Func<CancellationToken, ValueTask<Stream>>? GetContentStreamFunc, long Size)> GetContentAsync(
            Guid id,
            CancellationToken cancellationToken = default) =>
            await this.fileConverterCache.GetFileAsync(id, cancellationToken);


        /// <inheritdoc />
        public async ValueTask<ValidationResult> DeleteAsync(
            Guid id,
            Guid? sourceFileVersionID = null,
            CancellationToken cancellationToken = default)
        {
            ValidationResult result = ValidationResult.Empty;

            await using var _ = this.dbScope.Create();

            if (!sourceFileVersionID.HasValue)
            {
                IOnlyOfficeFileCacheInfo? info = await this.cacheInfoStrategy.TryGetInfoAsync(id, cancellationToken);
                sourceFileVersionID = info?.SourceFileVersionID;
            }

            if (sourceFileVersionID.HasValue)
            {
                // пользователь без прав админа не может изменять карточку "Кэш файлов", поскольку это синглтон;
                // временно добавляем ему права администратора
                await using var __ = this.session.User.IsAdministrator() ? null : SessionContext.Create(
                    this.session.CreateNestedSessionToken(this.session.User.ID, this.session.User.Name));

                (ValidationResult convertResult, bool _) = await this.fileConverterCache
                    .DeleteFileAsync(sourceFileVersionID.Value, null, cancellationToken);

                if (!convertResult.IsSuccessful)
                {
                    return convertResult;
                }

                result = convertResult;
            }

            try
            {
                await this.cacheInfoStrategy.DeleteAsync(id, CancellationToken.None);
            }
            catch (Exception ex)
            {
                result = ValidationResult.Aggregate(ValidationResult.FromException(this, ex), result);
            }

            return result;
        }

        #endregion
    }
}
