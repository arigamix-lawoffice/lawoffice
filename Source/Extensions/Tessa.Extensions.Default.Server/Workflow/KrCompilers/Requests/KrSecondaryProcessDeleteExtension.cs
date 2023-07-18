#nullable enable

using Tessa.Extensions.Default.Shared;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Расширение, выполняющее сброс кэшей данных и результатов компиляции при удалении карточки типа <see cref="DefaultCardTypes.KrSecondaryProcessTypeID"/>.
    /// </summary>
    public sealed class KrSecondaryProcessDeleteExtension :
        KrCompileSourceDeleteExtensionBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="processCache"><inheritdoc cref="IKrProcessCache" path="/summary"/></param>
        /// <param name="compilationCache"><inheritdoc cref="IKrSecondaryProcessCompilationCache" path="/summary"/></param>
        public KrSecondaryProcessDeleteExtension(
            IKrProcessCache processCache,
            IKrSecondaryProcessCompilationCache compilationCache)
            : base(
                  processCache,
                  compilationCache)
        {
        }

        #endregion
    }
}
