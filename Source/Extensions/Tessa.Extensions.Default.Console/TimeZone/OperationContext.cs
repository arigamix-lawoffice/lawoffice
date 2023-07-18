using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.TimeZone
{
    public class OperationContext
    {
        public IEnumerable<OperationFunction> OperationFunction { get; set; }

        public int? ZoneID { get; set; }

        public int? ZoneOffset { get; set; }
    }
}