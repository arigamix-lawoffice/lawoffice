#nullable enable

using System;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles.Compilation
{
    /// <inheritdoc cref="IKrVirtualFileCompilationContext"/>
    public sealed class KrVirtualFileCompilationContext :
        IKrVirtualFileCompilationContext
    {
        #region Fields

        private string name = string.Empty;

        #endregion

        #region IKrVirtualFileCompilationContext Members

        /// <inheritdoc/>
        public Guid ID { get; set; }

        /// <inheritdoc/>
        public string Name
        {
            get => this.name;
            set => this.name = NotNullOrThrow(value);
        }

        /// <inheritdoc/>
        public string? InitializationScenario { get; set; }

        #endregion
    }
}
