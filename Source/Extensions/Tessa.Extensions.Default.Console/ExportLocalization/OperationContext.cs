using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.ExportLocalization
{
    public class OperationContext
    {
        public List<string> LibraryNames { get; set; }

        public string OutputFolder { get; set; }

        public bool ClearOutputFolder { get; set; }
    }
}
