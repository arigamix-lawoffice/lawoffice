using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.DeleteCards
{
    public class OperationContext
    {
        public List<CardInfo> CardInfoList { get; set; }
        
        public bool IgnoreAlreadyDeleted { get; set; }
    }
}