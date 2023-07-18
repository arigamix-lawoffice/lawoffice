using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.ImportCards
{
    public class OperationContext
    {
        public bool IgnoreExistentCards { get; set; }

        public bool IgnoreRepairMessages { get; set; }

        public string MergeOptionsPath { get; set; }

        public IEnumerable<string> Sources { get; set; }

        public string IgnoredFilesPath { get; set; }
    }
}
