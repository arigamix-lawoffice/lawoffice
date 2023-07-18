using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Scheme;

namespace Tessa.Extensions.Default.Console.SchemeScript
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            TextWriter stdOut,
            string source,
            string target,
            Dbms dbms,
            Version dbmsv,
            IEnumerable<string> includedPartitions,
            IEnumerable<string> excludedPartitions,
            bool withoutTransactions)
        {
            dbmsv ??= dbms switch
            {
                // по умолчанию поддерживаем MS SQL Server 2012 и старше
                Dbms.SqlServer => new Version(11, 0, 0, 0),
                Dbms.PostgreSql => new Version(9, 6, 0, 0),
                _ => throw new ArgumentOutOfRangeException(nameof(dbms), dbms, null)
            };

            string filePath = DefaultConsoleHelper.GetSourceFiles(source, "*.tsd").FirstOrDefault();
            if (filePath is null)
            {
                await logger.ErrorAsync("Can't find database file *.tsd in \"{0}\"", source);
                return -2;
            }

            string fileFullPath = Path.GetFullPath(filePath);
            await logger.InfoAsync("Generating scheme script for: {0}", fileFullPath);

            // генерация файлов всегда происходит для английской культуры
            LocalizationManager.SetEnglishLocalization();

            StreamWriter streamWriter = null;

            try
            {
                if (target is not null)
                {
                    streamWriter = new StreamWriter(target, false, Encoding.UTF8) { NewLine = "\n" };
                }

                var fileSchemeService = new FileSchemeService(
                    fileFullPath,
                    DefaultConsoleHelper.GetSchemePartitions(fileFullPath, includedPartitions, excludedPartitions));

                foreach (string partitionFileName in fileSchemeService.PartitionFileNames)
                {
                    await logger.InfoAsync("Partition: \"{0}\"", partitionFileName);
                }

                var database = new SchemeDatabase(Guid.Empty, DatabaseNames.Applied, false, true);
                var fakeService = new FakeSchemeService(streamWriter ?? stdOut, !withoutTransactions, dbms, dbmsv);

                await database.RefreshAsync(fileSchemeService);
                await fakeService.CreateStorageAsync();
                await database.SubmitChangesAsync(fakeService);
            }
            finally
            {
                if (streamWriter is not null)
                {
                    await streamWriter.DisposeAsync();
                }
            }

            return 0;
        }
    }
}