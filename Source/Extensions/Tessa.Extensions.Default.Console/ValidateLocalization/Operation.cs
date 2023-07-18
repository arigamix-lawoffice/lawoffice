using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.ConsoleApps;

namespace Tessa.Extensions.Default.Console.ValidateLocalization
{
    public sealed class Operation :
        ConsoleOperation<OperationContext>
    {
        #region Constructors

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger)
            : base(logger, sessionManager)
        {
        }

        #endregion

        #region Base Overrides

        public override async Task<int> ExecuteAsync(
           OperationContext context,
           CancellationToken cancellationToken = default)
        {
            try
            {
                await this.Logger.InfoAsync(
                    "Validating localization in \"{0}\"",
                    context.Source);

                foreach (string outputFilePath in DefaultConsoleHelper
                                 .GetSourceFiles(
                                     context.Source,
                                     JsonFileLocalizationService.FileSearchPattern,
                                     throwIfNotFound: false))
                {
                    var localizationService = new JsonFileLocalizationService(new[] { outputFilePath });
                    LocalizationLibrary library =
                        (await localizationService.GetLibrariesAsync(returnComments: true, cancellationToken: cancellationToken))
                        .First();
                    
                    await this.Logger.InfoAsync("Saving library \"{0}\"", library.Name);
                    await localizationService.SaveLibraryAsync(library, cancellationToken);
                }

                return 0;
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error validating localization", e);
                return -1;
            }
        }

        #endregion
    }
}
