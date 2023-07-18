#nullable enable
using Tessa.Files;

namespace Tessa.Extensions.Default.Client.ExternalFiles
{
    public class ExternalFileCreationToken : FileCreationToken
    {
        #region Properties

        /// <summary>
        /// File description.
        /// </summary>
        public string? Description { get; set; }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        protected override void SetCore(IFileCreationToken token)
        {
            base.SetCore(token);

            this.Description = token is ExternalFileCreationToken typedToken ? typedToken.Description : null;
        }

        /// <inheritdoc/>
        protected override void SetCore(IFile file)
        {
            base.SetCore(file);

            this.Description = file is ExternalFile typedFile ? typedFile.Description : null;
        }

        #endregion
    }
}
