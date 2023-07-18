using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.ImportScheme
{
    public class OperationContext
    {
        public string Source { get; set; }

        public IEnumerable<string> IncludedPartitions { get; set; }

        public IEnumerable<string> ExcludedPartitions { get; set; }
    }
}