using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Tessa.Platform.ConsoleApps;
using Tessa.Platform.Data;
using Tessa.Platform.Runtime;
using Tessa.Scheme;

namespace Tessa.Extensions.Default.Console.SchemeRename
{
    public static class Operation
    {
        public static async Task<int> ExecuteAsync(
            IConsoleLogger logger,
            TextWriter stdOut,
            string source,
            string tableName,
            string columnName,
            string target,
            Dbms dbms,
            IEnumerable<string> includedPartitions,
            IEnumerable<string> excludedPartitions)
        {
            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentException("Table name isn't specified", nameof(tableName));
            }

            if (string.IsNullOrEmpty(columnName))
            {
                throw new ArgumentException("Column name isn't specified", nameof(columnName));
            }

            string filePath = DefaultConsoleHelper.GetSourceFiles(source, "*.tsd").FirstOrDefault();
            if (filePath is null)
            {
                await logger.ErrorAsync("Can't find database file *.tsd in \"{0}\"", source);
                return -2;
            }

            string fileFullPath = Path.GetFullPath(filePath);
            await logger.InfoAsync("Generating rename script for the scheme: \"{0}\"", fileFullPath);

            var fileSchemeService = new FileSchemeService(
                fileFullPath,
                DefaultConsoleHelper.GetSchemePartitions(fileFullPath, includedPartitions, excludedPartitions),
                SchemeServiceOptions.ReadOnly);

            foreach (string partitionFileName in fileSchemeService.PartitionFileNames)
            {
                await logger.InfoAsync("Partition: \"{0}\"", partitionFileName);
            }

            SchemeTable table = await fileSchemeService.GetTableAsync(tableName);
            if (table is null)
            {
                throw new InvalidOperationException($"Can't find table \"{tableName}\".");
            }

            if (!table.Columns.TryGetItem(columnName, out SchemeColumn _))
            {
                throw new InvalidOperationException($"Can't find column \"{columnName}\" in table \"{tableName}\".");
            }

            string identifierColumnName = table.ContentType <= SchemeTableContentType.Entries
                ? Names.Table_ID_ID
                : Names.Table_RowID;

            IQueryBuilder builder = new OperationQueryBuilder(dbms);
            string tempTable = "RenamingValues";

            builder
                .CreateTempTable(ref tempTable, b => b
                    .C(identifierColumnName).Type(SchemeDbType.Guid).Q(" NOT NULL PRIMARY KEY,").N()
                    .C(columnName).Type(SchemeDbType.String).Q(" NOT NULL")).Z()
                .InsertInto(tempTable, identifierColumnName, columnName)
                .Values(b => b.V(Session.SystemID).V(Session.SystemName)).Z()
                .InsertInto(tempTable, identifierColumnName, columnName)
                .Values(b => b.V(new Guid(0x3DB19FA0, 0x228A, 0x497F, 0x87, 0x3A, 0x02, 0x50, 0xBF, 0x0A, 0x4C, 0xCB)).V("Admin")).Z()
                .Update(tableName)
                .C(columnName).Assign().C("r", columnName)
                .From(tempTable, "r").NoLock()
                .Where().C(tableName, identifierColumnName).Equals().C("r", identifierColumnName)
                .And().C(tableName, columnName).NotEquals().C("r", columnName).Z();

            // в каждой невиртуальной таблице, в т.ч. в таблице table (для рекурсивных связей)
            foreach (SchemeTable referencingTable in table.GetReferencingTables())
            {
                if (referencingTable != table && !referencingTable.IsVirtual)
                {
                    // для каждой комплексной колонки, которая ссылается на таблицу table, кроме системных колонок ID
                    // и в которой есть колонка вида Reference+ColumnName
                    foreach (var candidateColumn in referencingTable.Columns)
                    {
                        if (candidateColumn.IsSystem)
                        {
                            continue;
                        }

                        var complexColumn = candidateColumn as SchemeComplexColumn; // User
                        string targetTableName = referencingTable.Name;
                        string targetColumnName; // UserName

                        if (complexColumn is not null &&
                            complexColumn.ReferencedTable == table &&
                            complexColumn.Columns.Contains(complexColumn.Name + identifierColumnName) &&
                            complexColumn.Columns.Contains(targetColumnName = complexColumn.Name + columnName))
                        {
                            // пишем скрипт, обновляющий колонку targetColumnName в таблице t
                            builder
                                .Update(targetTableName)
                                .C(targetColumnName).Assign().C("r", columnName)
                                .From(tempTable, "r").NoLock()
                                .Where().C(targetTableName, identifierColumnName).Equals().C("r", identifierColumnName)
                                .And().C(targetTableName, columnName).NotEquals().C("r", columnName).Z();
                        }
                    }
                }
            }

            builder.DropTable(tempTable).Z();

            if (target is null)
            {
                await stdOut.WriteAsync(builder.GetStringBuilder());
            }
            else
            {
                await using var fileOutput = new StreamWriter(target) { NewLine = "\n" };
                await fileOutput.WriteAsync(builder.GetStringBuilder());
            }

            return 0;
        }
    }
}
