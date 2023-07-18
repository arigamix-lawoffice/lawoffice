#nullable enable

using Tessa.Cards;
using Tessa.Extensions.Default.Shared;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Класс расширения, выполняющего заполнение виртуальных секций результата
    /// компиляции для карточек типа <see cref="DefaultCardTypes.KrStageGroupTypeID"/>.
    /// </summary>
    public sealed class KrSourceStageGroupGetExtension :
        KrSourceGetExtensionBase
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="krCommonMethodCompilationCache"><inheritdoc cref="IKrCommonMethodCompilationCache" path="/summary"/></param>
        /// <param name="krStageTemplateCompilationCache"><inheritdoc cref="IKrStageTemplateCompilationCache" path="/summary"/></param>
        /// <param name="krStageGroupCompilationCache"><inheritdoc cref="IKrStageGroupCompilationCache" path="/summary"/></param>
        /// <param name="krSecondaryProcessCompilationCache"><inheritdoc cref="IKrSecondaryProcessCompilationCache" path="/summary"/></param>
        /// <param name="krProcessCache"><inheritdoc cref="IKrProcessCache" path="/summary"/></param>
        /// <param name="cardMetadata"><inheritdoc cref="ICardMetadata" path="/summary"/></param>
        public KrSourceStageGroupGetExtension(
            IKrCommonMethodCompilationCache krCommonMethodCompilationCache,
            IKrStageTemplateCompilationCache krStageTemplateCompilationCache,
            IKrStageGroupCompilationCache krStageGroupCompilationCache,
            IKrSecondaryProcessCompilationCache krSecondaryProcessCompilationCache,
            IKrProcessCache krProcessCache,
            ICardMetadata cardMetadata)
            : base(
                  krStageGroupCompilationCache,
                  krCommonMethodCompilationCache,
                  krStageTemplateCompilationCache,
                  krStageGroupCompilationCache,
                  krSecondaryProcessCompilationCache,
                  krProcessCache,
                  cardMetadata)
        {
        }

        #endregion
    }
}
