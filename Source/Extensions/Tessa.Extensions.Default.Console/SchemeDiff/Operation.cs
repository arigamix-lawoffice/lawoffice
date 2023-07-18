using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Localization;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.IO;
using Tessa.Scheme;
using Tessa.Scheme.Differences;

namespace Tessa.Extensions.Default.Console.SchemeDiff
{
    public static class Operation
    {
        #region Private Methods

        private static async ValueTask<SchemeDatabase> OpenDatabaseAsync(
            IConsoleLogger logger,
            string source,
            IEnumerable<string> includedPartitions,
            IEnumerable<string> excludedPartitions)
        {
            string fileFullPath = Path.GetFullPath(source);
            await logger.InfoAsync("Reading scheme from: \"{0}\"", fileFullPath);

            FileSchemeService fileSchemeService = new FileSchemeService(
                fileFullPath,
                DefaultConsoleHelper.GetSchemePartitions(fileFullPath, includedPartitions, excludedPartitions));

            foreach (string partitionFileName in fileSchemeService.PartitionFileNames)
            {
                await logger.InfoAsync("Partition: \"{0}\"", partitionFileName);
            }

            SchemeDatabase tessaDatabase = new SchemeDatabase(DatabaseNames.Original);
            await tessaDatabase.RefreshAsync(fileSchemeService);

            return tessaDatabase;
        }

        private static void RestoreForegroundColor(ConsoleColor color)
        {
            System.Console.ForegroundColor = color;
        }

        private static void SetForegroundColor(ConsoleColor color, out ConsoleColor previousColor)
        {
            previousColor = System.Console.ForegroundColor;
            System.Console.ForegroundColor = color;
        }

        private static void WriteObject(SchemeObject obj, TextWriter writer)
        {
            writer.Write(obj.GetDisplayName());

            if (obj is SchemeNamedObject named)
            {
                writer.Write(", ID: ");
                writer.Write(named.ID);
            }
        }

        private static void WritePropertyDifference(SchemePropertyDifference propDifference, IndentedTextWriter writer)
        {
            writer.WriteLine();
            writer.Write(propDifference.Property);
            writer.WriteLine(':');
            writer.Indent();

            ConsoleColor previousColor;

            switch (propDifference)
            {
                case SchemeScalarPropertyDifference scalarDifference:
                    SetForegroundColor(ConsoleColor.Yellow, out previousColor);

                    writer.Write("X: ");
                    writer.Write(scalarDifference.Left);
                    writer.WriteLine();
                    writer.Write("Y: ");
                    writer.Write(scalarDifference.Right);

                    RestoreForegroundColor(previousColor);
                    break;

                case SchemeCollectionDifference objCollectionDifference:
                    writer.Write("Added:");
                    writer.Indent();

                    SetForegroundColor(ConsoleColor.Green, out previousColor);

                    foreach (var obj in objCollectionDifference.Added)
                    {
                        writer.WriteLine();
                        WriteObject(obj, writer);
                    }

                    RestoreForegroundColor(previousColor);

                    writer.Unindent();

                    writer.WriteLine();
                    writer.Write("Removed:");
                    writer.Indent();

                    SetForegroundColor(ConsoleColor.Red, out previousColor);

                    foreach (var obj in objCollectionDifference.Removed)
                    {
                        writer.WriteLine();
                        WriteObject(obj, writer);
                    }

                    RestoreForegroundColor(previousColor);

                    writer.Unindent();

                    writer.WriteLine();
                    writer.Write("Modified:");
                    writer.Indent();

                    foreach (var objDifference in objCollectionDifference.Modified)
                    {
                        writer.WriteLine();

                        SetForegroundColor(ConsoleColor.Yellow, out previousColor);
                        WriteObject(objDifference.Right, writer);
                        RestoreForegroundColor(previousColor);

                        writer.Indent();

                        foreach (var nestedDifference in objDifference.Modified)
                        {
                            WritePropertyDifference(nestedDifference, writer);
                        }

                        writer.Unindent();
                    }

                    writer.Unindent();
                    break;
            }

            writer.Unindent();
        }

        #endregion

        #region Static Methods

        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            TextWriter stdOut,
            string sourceA,
            string sourceB,
            IEnumerable<string> includedPartitionsA,
            IEnumerable<string> excludedPartitionsA,
            IEnumerable<string> includedPartitionsB,
            IEnumerable<string> excludedPartitionsB)
        {
            string sourcePathA = DefaultConsoleHelper.GetSourceFiles(sourceA, "*.tsd").FirstOrDefault();
            if (sourcePathA is null)
            {
                await logger.ErrorAsync("Can't find first database file *.tsd in \"{0}\"", sourceA);
                return -2;
            }

            string sourcePathB = DefaultConsoleHelper.GetSourceFiles(sourceB, "*.tsd").FirstOrDefault();
            if (sourcePathB is null)
            {
                await logger.ErrorAsync("Can't find second database file *.tsd in \"{0}\"", sourceB);
                return -2;
            }

            await logger.InfoAsync("Evaluating difference between \"{0}\" and \"{1}\"", sourceA, sourceB);

            SchemeDatabase databaseA = await OpenDatabaseAsync(logger, sourcePathA, includedPartitionsA, excludedPartitionsA);
            SchemeDatabase databaseB = await OpenDatabaseAsync(logger, sourcePathB, includedPartitionsB, excludedPartitionsB);

            SchemeObjectDifference difference = SchemeObjectDifference.GetDifferenceIfExists(databaseA, databaseB, false);
            await logger.InfoAsync("Difference is evaluated, printing it to output", sourceA, sourceB);

            if (difference is null)
            {
                await stdOut.WriteLineAsync(await LocalizationManager.GetStringAsync("Common_CLI_Scheme_DatabasesAreEqual"));
            }
            else
            {
                await using IndentedTextWriter writer = new IndentedTextWriter(stdOut);
                foreach (SchemePropertyDifference propDifference in difference.Modified)
                {
                    WritePropertyDifference(propDifference, writer);
                }
            }

            return 0;
        }

        #endregion
    }
}