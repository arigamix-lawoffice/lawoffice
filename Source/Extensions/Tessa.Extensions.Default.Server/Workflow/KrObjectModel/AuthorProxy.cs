using System;
using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrObjectModel
{
    public sealed class AuthorProxy: Author
    {
        private readonly IDictionary<string, object> storage;
        
        /// <inheritdoc />
        public AuthorProxy(
            IDictionary<string, object> storage)
        {
            this.storage = storage;
        }
        
        /// <inheritdoc />
        public override Guid AuthorID => this.storage.TryGet<Guid>(KrConstants.KrAuthorSettingsVirtual.AuthorID);

        /// <inheritdoc />
        public override string AuthorName => this.storage.TryGet<string>(KrConstants.KrAuthorSettingsVirtual.AuthorName);
    }
}