#nullable enable

namespace Tessa.Extensions.Default.Shared
{
    /// <summary>
    /// Кэши с результатами компиляции, доступные в типовом решении.
    /// </summary>
    public static class DefaultCompilationCacheNames
    {
        #region Constants

        /// <summary>
        /// Кэш, содержащий результаты компиляции вторичных процессов подсистемы маршрутов и этапов в них.
        /// 
        /// <c>Tessa.Extensions.Default.Server.Workflow.KrCompilers.IKrSecondaryProcessCompilationCache</c>
        /// </summary>
        public const string KrSecondaryProcess = nameof(KrSecondaryProcess);

        /// <summary>
        /// Кэш, содержащий результаты компиляции групп этапов процессов подсистемы маршрутов.
        /// 
        /// <c>Tessa.Extensions.Default.Server.Workflow.KrCompilers.IKrStageGroupCompilationCache</c>
        /// </summary>
        public const string KrStageGroup = nameof(KrStageGroup);

        /// <summary>
        /// Кэш, содержащий результаты компиляции шаблонов этапов процессов подсистемы маршрутов и этапов в них.
        /// 
        /// <c>Tessa.Extensions.Default.Server.Workflow.KrCompilers.IKrStageTemplateCompilationCache</c>
        /// </summary>
        public const string KrStageTemplate = nameof(KrStageTemplate);

        /// <summary>
        /// Кэш, содержащий результаты компиляции методов расширений подсистемы маршрутов.
        /// 
        /// <c>Tessa.Extensions.Default.Server.Workflow.KrCompilers.IKrCommonMethodCompilationCache</c>
        /// </summary>
        public const string KrCommonMethod = nameof(KrCommonMethod);

        /// <summary>
        /// Кэш, содержащий результаты компиляции скриптов виртуальных файлов.
        /// 
        /// <c>Tessa.Extensions.Default.Server.Files.VirtualFiles.Compilation.IKrVirtualFileCompilationCache</c>
        /// </summary>
        public const string KrVirtualFile = nameof(KrVirtualFile);

        #endregion
    }
}
