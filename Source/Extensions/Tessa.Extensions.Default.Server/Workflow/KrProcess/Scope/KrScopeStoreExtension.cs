using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Scope
{
    /// <summary>
    /// Расширение на сохранение карточки, обрабатывающее результаты выполнения, записанные в <see cref="KrScopeContext"/>.
    /// </summary>
    public sealed class KrScopeStoreExtension :
        CardStoreExtension
    {
        #region Fields

        private readonly IKrTypesCache krTypesCache;

        private List<KrProcessClientCommand> clientCommands;

        private IValidationResultBuilder scopeValidationResult;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="KrScopeStoreExtension"/>.
        /// </summary>
        /// <param name="krTypesCache"><inheritdoc cref="IKrTypesCache" path="/summary"/></param>
        public KrScopeStoreExtension(
            IKrTypesCache krTypesCache) =>
            this.krTypesCache = NotNullOrThrow(krTypesCache);

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task BeforeCommitTransaction(
            ICardStoreExtensionContext context)
        {
            if (!context.ValidationResult.IsSuccessful()
                || !await KrComponentsHelper.HasBaseAsync(context.Request.Card.TypeID, this.krTypesCache, context.CancellationToken))
            {
                return;
            }

            var scopeContext = KrScopeContext.Current;
            if (scopeContext is not null)
            {
                var card = context.Request.Card;

                await scopeContext.LevelStack.Peek().ApplyChangesAsync(
                    card.ID,
                    card.StoreMode == CardStoreMode.Update,
                    context.ValidationResult,
                    cancellationToken: context.CancellationToken);
                this.clientCommands = scopeContext.GetKrProcessClientCommands();
                this.scopeValidationResult = scopeContext.ValidationResult;

                context.ValidationResult.Add(this.scopeValidationResult);
                this.scopeValidationResult.Clear();
            }
        }

        /// <inheritdoc/>
        public override Task AfterRequest(
            ICardStoreExtensionContext context)
        {
            if (KrScopeContext.HasCurrent)
            {
                return Task.CompletedTask;
            }

            if (this.clientCommands is not null)
            {
                context.Response.AddKrProcessClientCommands(this.clientCommands);
            }

            if (this.scopeValidationResult is not null)
            {
                context.ValidationResult.Add(this.scopeValidationResult);
                this.scopeValidationResult = null;
            }

            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override Task AfterRequestFinally(ICardStoreExtensionContext context)
        {
            if (KrScopeContext.HasCurrent)
            {
                return Task.CompletedTask;
            }

            // Сохранение оставшихся результатов валидации, из-за невыполнения цепочки AfterRequest.
            if (this.scopeValidationResult is not null)
            {
                context.ValidationResult.Add(this.scopeValidationResult);
                this.scopeValidationResult = null;
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
