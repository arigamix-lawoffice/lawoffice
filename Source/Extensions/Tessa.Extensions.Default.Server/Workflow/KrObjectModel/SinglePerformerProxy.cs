using System;
using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    public sealed class SinglePerformerProxy: Performer
    {
        private readonly IDictionary<string, object> storage;
        
        public SinglePerformerProxy(
            IDictionary<string, object> storage)
        {
            this.storage = storage;
        }
        
        /// <inheritdoc />
        public override Guid PerformerID =>
            this.storage.TryGet<Guid>(KrConstants.KrSinglePerformerVirtual.PerformerID);

        /// <inheritdoc />
        public override string PerformerName =>
            this.storage.TryGet<string>(KrConstants.KrSinglePerformerVirtual.PerformerName);

    }
}