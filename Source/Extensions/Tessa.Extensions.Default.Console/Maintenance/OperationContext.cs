#nullable enable
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.Maintenance
{
    public sealed class OperationContext
    {
        public SupportedCommand Command { get; set; }
        
        public string? Address { get; set; }
        
        public int Timeout { get; set; }

        public List<string>? RawMessages { get; set; }
        
        public bool Isolated { get; set; }
    }
}
