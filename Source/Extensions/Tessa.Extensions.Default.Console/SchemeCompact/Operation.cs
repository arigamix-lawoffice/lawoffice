using System.IO;
using System.Linq;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Tessa.Localization;
using Tessa.Platform.ConsoleApps;
using Tessa.Scheme;

namespace Tessa.Extensions.Default.Console.SchemeCompact
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            TextWriter stdOut,
            string source,
            string target,
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
            await logger.InfoAsync("Reading scheme from: \"{0}\"", fileFullPath);

            // генерация файлов всегда происходит для английской культуры
            LocalizationManager.SetEnglishLocalization();

            var fileSchemeService = new FileSchemeService(
                fileFullPath,
                DefaultConsoleHelper.GetSchemePartitions(fileFullPath, includedPartitions, excludedPartitions));

            foreach (string partitionFileName in fileSchemeService.PartitionFileNames)
            {
                await logger.InfoAsync("Partition: \"{0}\"", partitionFileName);
            }

            var database = new SchemeDatabase(DatabaseNames.Original);
            await database.RefreshAsync(fileSchemeService);

            await logger.InfoAsync($"Compacting the scheme into a single file: \"{target}\"");

            StreamWriter streamWriter = null;
            XmlWriter xmlWriter = null;
            SchemeSerializationScope scope = null;

            try
            {
                if (target is not null)
                {
                    streamWriter = new StreamWriter(target, false, Encoding.UTF8) { NewLine = "\n" };
                }

                xmlWriter = XmlWriter.Create(streamWriter ?? stdOut, FileSchemeService.XmlWriterSettings);
                scope = new SchemeSerializationScope();

                database.WriteXml(xmlWriter);
            }
            finally
            {
                scope?.Dispose();
                xmlWriter?.Close();

                if (streamWriter is not null)
                {
                    await streamWriter.DisposeAsync();
                }
            }

            await logger.InfoAsync("Scheme has been compacted");
            return 0;
        }
    }
}