#nullable enable

using Tessa.Extensions.Default.Shared;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Расширение, выполняющее сброс кэшей данных и результатов компиляции при удалении карточки типа <see cref="DefaultCardTypes.KrStageGroupTypeID"/>.
    /// </summary>
    public sealed class KrStageGroupDeleteExtension :
        KrCompileSourceDeleteExtensionBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="processCache"><inheritdoc cref="IKrProcessCache" path="/summary"/></param>
        /// <param name="compilationCache"><inheritdoc cref="IKrStageGroupCompilationCache" path="/summary"/></param>
        public KrStageGroupDeleteExtension(
            IKrProcessCache processCache,
            IKrStageGroupCompilationCache compilationCache)
            : base(
                  processCache,
                  compilationCache)
        {
        }

        #endregion
    }
}
