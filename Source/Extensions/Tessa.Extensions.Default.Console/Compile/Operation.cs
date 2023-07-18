#nullable enable

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Compilation;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Validation;

namespace Tessa.Extensions.Default.Console.Compile
{
    public sealed class Operation :
        ConsoleOperation<OperationContext>
    {
        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            IClientCompilationCacheContainer сlientCompilationCacheContainer)
            : base(logger, sessionManager, extendedInitialization: true) =>
            this.сlientCompilationCacheContainer = NotNullOrThrow(сlientCompilationCacheContainer);

        #endregion

        #region Fields

        private readonly IClientCompilationCacheContainer сlientCompilationCacheContainer;

        #endregion

        #region Base Overrides

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(
            OperationContext context,
            CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            if (context.ShowCategories)
            {
                return await this.ShowRegisteredCategoriesAsync(
                    cancellationToken);
            }

            return await this.RebuildAsync(
                context,
                cancellationToken);
        }

        #endregion

        #region Private Methods

        private async Task<int> ShowRegisteredCategoriesAsync(
            CancellationToken cancellationToken = default)
        {
            var validationResult = new ValidationResultBuilder();

            var registeredCategories = await this.сlientCompilationCacheContainer.GetRegisteredCategoriesAsync(
                validationResult,
                cancellationToken);

            await this.Logger.LogResultAsync(validationResult.Build());

            if (!validationResult.IsSuccessful())
            {
                return -1;
            }

            if (registeredCategories.Count == 0)
            {
                await this.Logger.WriteAsync("Not found categories of compiled system objects.");
            }
            else
            {
                if (!this.Logger.Quiet)
                {
                    await this.Logger.WriteLineAsync();
                    await this.Logger.WriteLineAsync("Categories of compiled system objects:");
                }

                foreach (var registeredCategory in registeredCategories
                    .OrderBy(
                        static i => i,
                        StringComparer.Ordinal))
                {
                    await this.Logger.WriteLineAsync(registeredCategory);
                }

                if (!this.Logger.Quiet)
                {
                    await this.Logger.WriteLineAsync();
                }
            }

            return 0;
        }

        private async Task<int> RebuildAsync(
            OperationContext context,
            CancellationToken cancellationToken = default)
        {
            if (context.Categories is null)
            {
                await this.Logger.InfoAsync("Compile all...");
            }
            else
            {
                await this.Logger.InfoAsync(
                    "Compile category(s): {0}.",
                    string.Join(
                        ", ",
                        context.Categories.Select(x => $"\"{x}\"")));

                if (context.Identifiers is not null)
                {
                    await this.Logger.InfoAsync(
                        "Compile object(s) with ID: {0}.",
                        string.Join(
                            ", ",
                            context.Identifiers));
                }
            }

            try
            {
                var validationResult = new ValidationResultBuilder();
                await this.сlientCompilationCacheContainer.RebuildAsync(
                    new CompilationOptions()
                    {
                        Categories = context.Categories,
                        Identifiers = context.Identifiers,
                    },
                    validationResult,
                    cancellationToken);
                await this.Logger.LogResultAsync(validationResult.Build());

                if (!validationResult.IsSuccessful())
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
                await this.Logger.LogExceptionAsync("Error compile objects.", e);
                return -1;
            }

            await this.Logger.InfoAsync("Compile is successful.");
            return 0;
        }

        #endregion
    }
}
