using System;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Runtime;

namespace Tessa.Extensions.Default.Console.IncrementVersion
{
    public sealed class Operation : ConsoleOperation
    {
        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            IConfigurationVersionProvider configurationVersionProvider)
            : base(logger, sessionManager)
        {
            this.configurationVersionProvider = configurationVersionProvider;
        }

        #endregion

        #region Fields

        private readonly IConfigurationVersionProvider configurationVersionProvider;

        #endregion

        #region Base Overrides

        public override async Task<int> ExecuteAsync(ConsoleOperationContext context, CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            try
            {
                await this.configurationVersionProvider.IncrementVersionAsync(cancellationToken: cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error incrementing configuration version", e);
                return -1;
            }

            await this.Logger.InfoAsync("Configuration version is successfully incremented");
            return 0;
        }

        #endregion
    }
}
