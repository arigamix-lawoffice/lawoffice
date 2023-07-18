using System.Globalization;

namespace Tessa.Extensions.Default.Console.ImportDiffCulture
{
    public class OperationContext
    {
        public string Source { get; set; }

        public string Output { get; set; }

        public CultureInfo TargetCulture { get; set; }
    }
}
