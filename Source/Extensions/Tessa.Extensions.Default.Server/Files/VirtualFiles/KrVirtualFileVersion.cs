using System;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles
{
    public sealed class KrVirtualFileVersion : IKrVirtualFileVersion
    {
        public Guid ID { get; set; }
        public string Name { get; set; }
        public Guid FileTemplateID { get; set; }
    }
}
