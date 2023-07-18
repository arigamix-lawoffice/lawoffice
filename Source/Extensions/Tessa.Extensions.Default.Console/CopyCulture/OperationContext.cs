using System.Globalization;

namespace Tessa.Extensions.Default.Console.CopyCulture
{
    public class OperationContext
    {
        public string Source { get; set; }

        public string Target { get; set; }

        public CultureInfo FromCulture { get; set; }

        public CultureInfo ToCulture { get; set; }

        public bool ForceDetached { get; set; }

        public bool EmptyOnly { get; set; }
    }
}
