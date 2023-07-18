using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Client.Tiles;
using Tessa.Extensions.Default.Shared.Initialization;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Unity;

namespace Tessa.Extensions.Default.Client.Initialization
{
    public sealed class KrClientInitializationExtension :
        KrClientAndConsoleInitializationExtension
    {
        #region Constructors

        public KrClientInitializationExtension([OptionalDependency] ICardCache cardCache)
            : base(cardCache)
        {
        }

        #endregion

        #region Base Overrides

        protected override Task AdditionalInitializationAsync(
            Guid[] unavailableTypes,
            ListStorage<KrDocType> docTypes,
            CancellationToken cancellationToken)
        {
            KrTileHelper.SetUnavailableTypes(new ReadOnlyCollection<Guid>(unavailableTypes));
            return Task.CompletedTask;
        }

        #endregion
    }
}
