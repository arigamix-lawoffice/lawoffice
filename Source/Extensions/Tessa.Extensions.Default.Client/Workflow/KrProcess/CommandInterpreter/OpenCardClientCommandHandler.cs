using System;
using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;
using Tessa.Platform.Storage;
using Tessa.UI;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess.CommandInterpreter
{
    /// <summary>
    /// Обработчик клиентской команды <see cref="DefaultCommandTypes.OpenCard"/>.
    /// </summary>
    public sealed class OpenCardClientCommandHandler : ClientCommandHandlerBase
    {
        #region Fields

        private readonly IUIHost uiHost;

        #endregion

        #region Constructor

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="OpenCardClientCommandHandler"/>.
        /// </summary>
        /// <param name="uiHost">Объект, предоставляющий упрощённый доступ к основным функциям платформы,
        /// которые связаны с отображением информации пользователю.</param>
        public OpenCardClientCommandHandler(
            IUIHost uiHost)
        {
            this.uiHost = uiHost ?? throw new ArgumentNullException(nameof(uiHost));
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Handle(
            IClientCommandHandlerContext context)
        {
            var command = context.Command;

            var cardID = command.Parameters.TryGet<object>(KrConstants.Keys.NewCardID) as Guid?;
            var cardTypeID = command.Parameters.TryGet<object>(KrConstants.Keys.TypeID) as Guid?;
            var cardTypeName = command.Parameters.TryGet<object>(KrConstants.Keys.TypeName) as string;

            if (cardID.HasValue
                || cardTypeID.HasValue
                || cardTypeName is not null)
            {
                await DispatcherHelper.InvokeInUIAsync(async () =>
                {
                    using var splash = TessaSplash.Create(TessaSplashMessage.OpeningCard);
                    await this.uiHost.OpenCardAsync(
                        cardID: cardID,
                        cardTypeID: cardTypeID,
                        cardTypeName: cardTypeName,
                        options: new OpenCardOptions
                        {
                            Splash = splash,
                        },
                        cancellationToken: context.CancellationToken);
                });
            }
        }

        #endregion
    }
}