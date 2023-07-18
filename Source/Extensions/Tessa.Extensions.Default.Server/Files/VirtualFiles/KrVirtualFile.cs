using System;
using System.Collections.Generic;
using Tessa.Files;
using Tessa.Platform.Conditions;

namespace Tessa.Extensions.Default.Server.Files.VirtualFiles
{
    public sealed class KrVirtualFile : IKrVirtualFile
    {
        #region Constructors

        public KrVirtualFile()
        {
            this.Versions = new List<IKrVirtualFileVersion>();
        }

        #endregion

        #region IKrVirtualFile Implementation

        public Guid ID { get; set; }

        public string Name { get; set; }

        public FileCategory FileCategory { get; set; }

        public List<IKrVirtualFileVersion> Versions { get; }

        public IEnumerable<ConditionSettings> Conditions { get; set; }

        public string InitializationScenario { get; set; }

        #endregion
    }
}