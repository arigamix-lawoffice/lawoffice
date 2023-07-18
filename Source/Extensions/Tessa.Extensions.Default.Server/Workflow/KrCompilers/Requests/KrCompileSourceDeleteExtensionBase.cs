#nullable enable

using System.Threading.Tasks;
using Tessa.Cards.Extensions;

namespace Tessa.Extensions.Default.Server.Workflow.KrCompilers.Requests
{
    /// <summary>
    /// Базовое расширение, выполняющее сброс кэшей данных и результатов компиляции при удалении карточки.
    /// </summary>
    public abstract class KrCompileSourceDeleteExtensionBase :
        CardDeleteExtension
    {
        #region Fields

        private readonly IKrProcessCache processCache;

        private readonly IKrCompilationCacheBase compilationCache;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса.
        /// </summary>
        /// <param name="processCache"><inheritdoc cref="IKrProcessCache" path="/summary"/></param>
        /// <param name="compilationCache"><inheritdoc cref="IKrCompilationCacheBase" path="/summary"/></param>
        protected KrCompileSourceDeleteExtensionBase(
            IKrProcessCache processCache,
            IKrCompilationCacheBase compilationCache)
        {
            this.processCache = NotNullOrThrow(processCache);
            this.compilationCache = NotNullOrThrow(compilationCache);
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task AfterRequest(
            ICardDeleteExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                || context.Request.CardID is not { } cardID)
            {
                return;
            }

            await this.processCache.InvalidateAsync(
                context.CancellationToken);

            await this.compilationCache.InvalidateAsync(
                new[] { cardID },
                context.CancellationToken);
        }

        #endregion
    }
}
