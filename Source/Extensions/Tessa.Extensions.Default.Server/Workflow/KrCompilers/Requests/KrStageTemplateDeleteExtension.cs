#nullable enable

using Tessa.Extensions.Default.Shared;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Расширение, выполняющее сброс кэшей данных и результатов компиляции при удалении карточки типа <see cref="DefaultCardTypes.KrStageTemplateTypeID"/>.
    /// </summary>
    public sealed class KrStageTemplateDeleteExtension :
        KrCompileSourceDeleteExtensionBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="processCache"><inheritdoc cref="IKrProcessCache" path="/summary"/></param>
        /// <param name="compilationCache"><inheritdoc cref="IKrStageTemplateCompilationCache" path="/summary"/></param>
        public KrStageTemplateDeleteExtension(
            IKrProcessCache processCache,
            IKrStageTemplateCompilationCache compilationCache)
            : base(
                  processCache,
                  compilationCache)
        {

        }

        #endregion
    }
}
