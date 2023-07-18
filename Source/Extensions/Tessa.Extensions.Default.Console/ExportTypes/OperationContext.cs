using System.Collections.Generic;
using Tessa.Cards;

namespace Tessa.Extensions.Default.Console.ExportTypes
{
    public class OperationContext
    {
        public List<string> TypeNamesOrIdentifiers { get; set; }

        public string OutputFolder { get; set; }

        public bool ClearOutputFolder { get; set; }

        public bool CreateTypesSubfolders { get; set; }

        public CardInstanceType? CardInstanceType { get; set; }
    }
}
