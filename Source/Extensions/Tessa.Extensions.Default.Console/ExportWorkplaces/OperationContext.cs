using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.ExportWorkplaces
{
    public class OperationContext
    {
        public List<string> WorkplaceNamesOrIdentifiers { get; set; }

        public string OutputFolder { get; set; }

        public bool ClearOutputFolder { get; set; }

        public bool IncludeViews { get; set; }

        public bool IncludeSearchQueries { get; set; }
    }
}