using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Console.InvalidateCache
{
    public sealed class Operation : ConsoleOperation<OperationContext>
    {
        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            ICardRepository cardRepository)
            : base(logger, sessionManager, extendedInitialization: true) =>
            this.cardRepository = cardRepository;

        #endregion

        #region Fields

        private readonly ICardRepository cardRepository;

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            if (context.CacheNames.Length == 0)
            {
                await this.Logger.InfoAsync("Invalidating all caches...");
            }
            else
            {
                await this.Logger.InfoAsync("Invalidating cache(s): {0}...", string.Join(", ", context.CacheNames.Select(x => "\"" + x + "\"")));
            }

            try
            {
                // если указана папка, то находим первый файл с подходящим расширением
                ValidationResult result = await this.cardRepository.InvalidateCacheAsync(context.CacheNames.Length == 0 ? null : context.CacheNames, cancellationToken);
                await this.Logger.LogResultAsync(result);

                if (!result.IsSuccessful)
                {
                    return -1;
                }
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error invalidating cache(s)", e);
                return -1;
            }

            await this.Logger.InfoAsync("Invalidation is successful");
            return 0;
        }

        #endregion
    }
}
