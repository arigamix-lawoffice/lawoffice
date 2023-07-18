using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope
{
    /// <summary>
    /// Расширение на сохранение карточки, включённой в типовое решение. Выполняет создание и обработку завершения уровня контекста <see cref="KrScopeContext"/>.
    /// </summary>
    public sealed class KrLifecycleScopeStoreExtension :
        CardStoreExtension
    {
        #region Fields

        private readonly IKrTypesCache krTypesCache;
        private readonly IKrScope scope;
        private KrScopeLevel level;

        #endregion

        #region Constructors

        public KrLifecycleScopeStoreExtension(
            IKrTypesCache krTypesCache,
            IKrScope scope)
        {
            this.krTypesCache = NotNullOrThrow(krTypesCache);
            this.scope = NotNullOrThrow(scope);
        }

        #endregion

        #region Base overrides

        /// <inheritdoc/>
        public override async Task AfterBeginTransaction(
            ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful()
                || !await KrComponentsHelper.HasBaseAsync(
                        context.Request.Card.TypeID,
                        this.krTypesCache,
                        context.CancellationToken))
            {
                return;
            }

            this.level = this.scope.EnterNewLevel();
        }

        /// <inheritdoc/>
        public override async Task AfterRequest(
            ICardStoreExtensionContext context)
        {
            if (this.level is not null)
            {
                await this.level.ExitAsync(context.ValidationResult);
                this.level = null;
            }
        }

        /// <inheritdoc/>
        public override async Task AfterRequestFinally(
            ICardStoreExtensionContext context)
        {
            if (this.level is not null)
            {
                await this.level.ExitAsync(context.ValidationResult);
                this.level = null;
            }
        }

        #endregion
    }
}
