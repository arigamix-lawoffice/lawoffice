using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.PackageWebApp
{
    public sealed class WebApplication
    {
        #region Properties

        public string Name { get; set; }

        public string Version { get; set; }

        public string ExeFileName { get; set; }

        public string Description { get; set; }

        public int? LanguageID { get; set; }

        public string LanguageCode { get; set; }

        public string LanguageCaption { get; set; }

        public string OSName { get; set; }

        public bool Client64Bit { get; set; }

        public List<WebApplicationFile> Files { get; } = new List<WebApplicationFile>();

        #endregion
    }
}