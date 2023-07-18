using System.Collections.Generic;
using System.Globalization;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Console.ExportCards
{
    public class OperationContext
    {
        public List<CardInfo> CardInfoList { get; set; }

        public string CardLibraryPath { get; set; }

        public string OutputFolder { get; set; }

        public bool BinaryFormat { get; set; }
        
        public CultureInfo LocalizationCulture { get; set; }
        
        public bool OverwriteModifiedValues { get; set; }
        
        public string StorageContentMappingFileName { get; set; }
    }
}
