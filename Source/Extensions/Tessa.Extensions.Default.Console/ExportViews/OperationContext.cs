using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.ExportViews
{
    public class OperationContext
    {
        public List<string> ViewAliasesOrIdentifiers { get; set; }

        public string OutputFolder { get; set; }

        public bool ClearOutputFolder { get; set; }
    }
}