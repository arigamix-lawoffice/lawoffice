using System.Collections.Generic;
using System.Globalization;

namespace Tessa.Extensions.Default.Console.ExportDiffCulture
{
    public class OperationContext
    {
        public List<string> Sources { get; set; }

        public string Output { get; set; }

        public CultureInfo TargetCulture { get; set; }

        public CultureInfo BaseCulture { get; set; }
    }
}
