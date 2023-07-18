using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;
using Tessa.Platform;
using Tessa.Platform.Storage;
using Tessa.UI;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess.CommandInterpreter
{
    /// <summary>
    /// Обработчик клиентской команды <see cref="DefaultCommandTypes.CreateCardViaDocType"/>.
    /// </summary>
    public sealed class CreateCardViaDocTypeCommandHandler : ClientCommandHandlerBase
    {
        #region Fields

        private readonly IUIHost uiHost;

        #endregion

        #region Constructor

        public CreateCardViaDocTypeCommandHandler(
            IUIHost uiHost)
        {
            this.uiHost = uiHost;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Handle(
            IClientCommandHandlerContext context)
        {
            var command = context.Command;
            if (command.Parameters.TryGetValue(KrConstants.Keys.TypeID, out var typeIDObj)
                && typeIDObj is Guid typeID)
            {
                var info = new Dictionary<string, object>
                {
                    [CardHelper.NewCardBilletKey] = command.Parameters.TryGet<byte[]>(KrConstants.Keys.NewCard),
                    [CardHelper.NewCardBilletSignatureKey] = command.Parameters.TryGet<byte[]>(KrConstants.Keys.NewCardSignature),
                };
                
                if (command.Parameters.TryGetValue(KrConstants.Keys.DocTypeID, out var doctypeIDObj)
                    && command.Parameters.TryGetValue(KrConstants.Keys.DocTypeTitle, out var doctypeTitleObj)
                    && doctypeIDObj is Guid doctypeID
                    && doctypeTitleObj is string doctypeTitle)
                {
                    info[KrConstants.Keys.DocTypeID] = doctypeID;
                    info[KrConstants.Keys.DocTypeTitle] = doctypeTitle;
                }
                
                await DispatcherHelper.InvokeInUIAsync(async () =>
                {
                    using ISplash splash = TessaSplash.Create(TessaSplashMessage.CreatingCard);
                    await this.uiHost.CreateCardAsync(
                        typeID,
                        options: new CreateCardOptions
                        {
                            Splash = splash,
                            Info = info,
                        },
                        cancellationToken: context.CancellationToken);
                });
            }
        }

        #endregion
    }
}