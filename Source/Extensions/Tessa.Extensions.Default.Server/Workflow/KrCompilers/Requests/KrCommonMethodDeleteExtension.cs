using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Расширение, выполняющее сброс кэшей данных и результатов компиляции при удалении карточки типа <see cref="DefaultCardTypes.KrStageCommonMethodTypeID"/>.
    /// </summary>
    public sealed class KrCommonMethodDeleteExtension :
        KrCompileSourceDeleteExtensionBase
    {
        #region Fields

        private readonly IKrStageTemplateCompilationCache stageTemplateCompilationCache;

        private readonly IKrStageGroupCompilationCache stageGroupCompilationCache;

        private readonly IKrSecondaryProcessCompilationCache secondaryProcessCompilationCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="processCache"><inheritdoc cref="IKrProcessCache" path="/summary"/></param>
        /// <param name="commonMethodCompilationCache"><inheritdoc cref="IKrCommonMethodCompilationCache" path="/summary"/></param>
        /// <param name="stageTemplateCompilationCache"><inheritdoc cref="IKrStageTemplateCompilationCache" path="/summary"/></param>
        /// <param name="stageGroupCompilationCache"><inheritdoc cref="IKrCompilationCacheBase" path="/summary"/></param>
        /// <param name="secondaryProcessCompilationCache"><inheritdoc cref="IKrSecondaryProcessCompilationCache" path="/summary"/></param>
        public KrCommonMethodDeleteExtension(
            IKrProcessCache processCache,
            IKrCommonMethodCompilationCache commonMethodCompilationCache,
            IKrStageTemplateCompilationCache stageTemplateCompilationCache,
            IKrStageGroupCompilationCache stageGroupCompilationCache,
            IKrSecondaryProcessCompilationCache secondaryProcessCompilationCache)
            : base(
                  processCache,
                  commonMethodCompilationCache)
        {
            this.stageTemplateCompilationCache = NotNullOrThrow(stageTemplateCompilationCache);
            this.stageGroupCompilationCache = NotNullOrThrow(stageGroupCompilationCache);
            this.secondaryProcessCompilationCache = NotNullOrThrow(secondaryProcessCompilationCache);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(
            ICardDeleteExtensionContext context)
        {
            await base.AfterRequest(context);

            if (!context.RequestIsSuccessful)
            {
                return;
            }

            // Выполняем инвалидацию всех кэшей компиляции, содержащих объекты подсистемы маршрутов, т.к. в любом из них может использоваться метод расширения.

            if (context.Request.CardID is not { } cardID)
            {
                return;
            }

            var cardIDArr = new[] { cardID };

            await this.stageTemplateCompilationCache.InvalidateAsync(
                cardIDArr,
                context.CancellationToken);

            await this.stageGroupCompilationCache.InvalidateAsync(
                cardIDArr,
                context.CancellationToken);

            await this.secondaryProcessCompilationCache.InvalidateAsync(
                cardIDArr,
                context.CancellationToken);
        }

        #endregion
    }
}
