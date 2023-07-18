using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.ExportSearchQueries
{
    public class OperationContext
    {
        public List<string> SearchQueryNamesOrIdentifiers { get; set; }

        public string OutputFolder { get; set; }

        public bool ClearOutputFolder { get; set; }

        public bool PublicQueriesOnly { get; set; }
    }
}