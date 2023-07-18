using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Files;

namespace Tessa.Extensions.Default.Client.ExternalFiles
{
    public class ExternalFile : File
    {
        #region Constructors

        public ExternalFile(
            Guid id,
            string name,
            long size,
            IFileCategory category,
            IFileType type,
            IFileContent content,
            ExternalFileSource source,
            DateTime? modified = null,
            Guid? modifiedByID = null,
            string modifiedByName = null,
            DateTime? created = null,
            Guid? createdByID = null,
            string createdByName = null,
            IFilePermissions permissions = null,
            bool isLocal = true,
            IFile origin = null,
            string description = null)
            : base(
                id,
                name,
                size,
                category,
                type,
                content,
                source,
                modified,
                modifiedByID,
                modifiedByName,
                created,
                createdByID,
                createdByName,
                permissions,
                isLocal,
                origin)
        {
            this.Description = description;
        }

        #endregion

        #region Properties

        public string Description { get; protected set; }

        #endregion

        #region Methods

        public Task SetDescriptionAsync(
            string value,
            Func<Action, CancellationToken, Task> executePropertyChangedAsync = null,
            CancellationToken cancellationToken = default)
        {
            if (this.Description == value)
            {
                return Task.CompletedTask;
            }

            this.Description = value;

            if (executePropertyChangedAsync is null)
            {
                this.OnPropertyChanged(nameof(this.Description));
                return Task.CompletedTask;
            }

            return executePropertyChangedAsync(() => this.OnPropertyChanged(nameof(this.Description)), cancellationToken);
        }

        #endregion
    }
}
