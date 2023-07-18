using System.Collections.ObjectModel;

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Кэши для сброса, доступные в типовом решении.
    /// </summary>
    public static class DefaultCacheNames
    {
        #region Constants

        /// <summary>
        /// Кэш для данных из карточек шаблонов этапов
        /// <c>Tessa.Extensions.Default.Server.Workflow.KrCompilers.IKrProcessCache</c>.
        /// </summary>
        public const string KrProcess = nameof(KrProcess);

        /// <summary>
        /// Кэш настроек типов карточек и документов в типовом решении
        /// <see cref="Tessa.Extensions.Default.Shared.Workflow.KrProcess.IKrTypesCache"/>.
        /// </summary>
        public const string KrTypes = nameof(KrTypes);

        /// <summary>
        /// Кэш настроек виртуальных файлов
        /// <c>Tessa.Extensions.Default.Server.Files.VirtualFiles.Compilation.IKrVirtualFileCache</c>.
        /// </summary>
        public const string KrVirtualFiles = nameof(KrVirtualFiles);

        #endregion

        #region Public Fields

        /// <summary>
        /// Список с именами всех кэшей в платформе.
        /// </summary>
        public static readonly ReadOnlyCollection<string> All =
            new ReadOnlyCollection<string>(
                new[]
                {
                    KrProcess,
                    KrTypes,
                    KrVirtualFiles,
                });

        #endregion
    }
}