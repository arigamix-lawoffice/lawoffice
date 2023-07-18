using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    [StorageObjectGenerator(GenerateDefaultConstructor = false)]
    public sealed partial class BranchAdditionInfo : StorageObject
    {
        #region constructors

        public BranchAdditionInfo(
            Guid secondaryProcessID,
            string secondaryProcessName,
            Dictionary<string, object> startingProcessInfo)
            : base(new Dictionary<string, object>())
        {
            this.Init(nameof(this.SecondaryProcessID), secondaryProcessID);
            this.Init(nameof(this.SecondaryProcessName), secondaryProcessName);
            this.Init(nameof(this.StartingProcessInfo), startingProcessInfo);
        }
        
        /// <inheritdoc />
        public BranchAdditionInfo(
            Dictionary<string, object> storage)
            : base(storage)
        {
        }

        #endregion

        #region properties

        public Guid SecondaryProcessID => this.Get<Guid>(nameof(this.SecondaryProcessID));

        public string SecondaryProcessName => this.Get<string>(nameof(this.SecondaryProcessName));
        
        public Dictionary<string, object> StartingProcessInfo =>
            this.Get<Dictionary<string, object>>(nameof(this.StartingProcessInfo));

        #endregion
    }
}
