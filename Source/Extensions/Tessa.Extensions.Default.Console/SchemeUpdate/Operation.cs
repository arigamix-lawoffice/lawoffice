using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;
using Tessa.Scheme;

namespace Tessa.Extensions.Default.Console.SchemeUpdate
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            string source,
            IEnumerable<string> includedPartitions,
            IEnumerable<string> excludedPartitions)
        {
            string filePath = DefaultConsoleHelper.GetSourceFiles(source, "*.tsd").FirstOrDefault();
            if (filePath is null)
            {
                await logger.ErrorAsync("Can't find database file *.tsd in \"{0}\"", source);
                return -2;
            }

            string fileFullPath = Path.GetFullPath(filePath);
            await logger.InfoAsync("Updating the scheme to current version from file: \"{0}\"", fileFullPath);

            var fileSchemeService = new FileSchemeService(
                fileFullPath,
                DefaultConsoleHelper.GetSchemePartitions(fileFullPath, includedPartitions, excludedPartitions));

            foreach (string partitionFileName in fileSchemeService.PartitionFileNames)
            {
                await logger.InfoAsync("Partition: \"{0}\"", partitionFileName);
            }

            if (await fileSchemeService.IsStorageUpToDateAsync())
            {
                await logger.InfoAsync("Scheme is up-to-date, skipping update");
            }
            else
            {
                await logger.InfoAsync("Scheme isn't up-to-date in the database, updating it...");
                await fileSchemeService.UpdateStorageAsync();
                await logger.InfoAsync("Scheme has been successfully updated");
            }

            return 0;
        }
    }
}
