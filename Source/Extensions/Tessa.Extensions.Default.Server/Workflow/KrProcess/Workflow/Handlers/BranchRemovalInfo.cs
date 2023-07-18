using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess.Workflow.Handlers
{
    [StorageObjectGenerator(GenerateDefaultConstructor = false)]
    public sealed partial class BranchRemovalInfo: StorageObject
    {
        #region constructors

        public BranchRemovalInfo(
            IEnumerable<Guid> secondaryProcesses,
            IEnumerable<Guid> nestedProcesses,
            DirectionAfterInterrupt directionAfterInterrupt)
            : base(new Dictionary<string, object>())
        {
            this.SetStorageValue(nameof(this.SecondaryProcesses), new ListStorage<Guid>(secondaryProcesses.Cast<object>().ToList()));
            this.SetStorageValue(nameof(this.NestedProcesses), new ListStorage<Guid>(nestedProcesses.Cast<object>().ToList()));
            this.Init(nameof(this.DirectionAfterInterrupt), (int)directionAfterInterrupt);
        }
        
        /// <inheritdoc />
        public BranchRemovalInfo(
            Dictionary<string, object> storage)
            : base(storage)
        {
        }

        #endregion

        #region properties

        public IList<Guid> SecondaryProcesses => this.GetList(nameof(this.SecondaryProcesses), x => new ListStorage<Guid>(x));
        
        public IList<Guid> NestedProcesses => this.GetList(nameof(this.NestedProcesses), x => new ListStorage<Guid>(x));

        public DirectionAfterInterrupt DirectionAfterInterrupt =>
            (DirectionAfterInterrupt) this.Get<int>(nameof(this.DirectionAfterInterrupt));

        #endregion

    }
}
