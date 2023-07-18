using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;
using Tessa.Scheme;

namespace Tessa.Extensions.Default.Console.ImportScheme
{
    public sealed class Operation : ConsoleOperation<OperationContext>
    {
        private readonly ISchemeService schemeService;

        public Operation(
            ConsoleSessionManager sessionManager,
            IConsoleLogger logger,
            ISchemeService schemeService)
            : base(logger, sessionManager)
        {
            this.schemeService = schemeService;
        }

        /// <inheritdoc />
        public override async Task<int> ExecuteAsync(OperationContext context, CancellationToken cancellationToken = default)
        {
            if (!this.SessionManager.IsOpened)
            {
                return -1;
            }

            string filePath = DefaultConsoleHelper.GetSourceFiles(context.Source, "*.tsd").FirstOrDefault();
            if (filePath is null)
            {
                await this.Logger.ErrorAsync("Can't find database file *.tsd in \"{0}\"", context.Source);
                return -2;
            }

            try
            {
                string fileFullPath = Path.GetFullPath(filePath);
                await this.Logger.InfoAsync("Reading scheme from: \"{0}\"", fileFullPath);

                var fileSchemeService = new FileSchemeService(
                    fileFullPath,
                    DefaultConsoleHelper.GetSchemePartitions(fileFullPath, context.IncludedPartitions, context.ExcludedPartitions));

                foreach (string partitionFileName in fileSchemeService.PartitionFileNames)
                {
                    await this.Logger.InfoAsync("Partition: \"{0}\"", partitionFileName);
                }

                if (!await fileSchemeService.IsStorageUpToDateAsync(cancellationToken))
                {
                    await this.Logger.InfoAsync("Scheme isn't up-to-date in the file folder, upgrading it...");
                    await fileSchemeService.UpdateStorageAsync(cancellationToken);
                }

                SchemeDatabase tessaDatabase = new SchemeDatabase(DatabaseNames.Original);
                await tessaDatabase.RefreshAsync(fileSchemeService, cancellationToken);

                await this.Logger.InfoAsync("Importing the scheme using web service");
                await tessaDatabase.SubmitChangesAsync(this.schemeService, cancellationToken);
            }
            catch (OperationCanceledException)
            {
                throw;
            }
            catch (Exception e)
            {
                await this.Logger.LogExceptionAsync("Error importing scheme", e);
                return -1;
            }

            await this.Logger.InfoAsync("Scheme has been imported successfully");
            return 0;
        }
    }
}