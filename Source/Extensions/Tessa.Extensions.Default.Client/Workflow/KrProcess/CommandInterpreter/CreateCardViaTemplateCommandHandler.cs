using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;
using Tessa.Platform.Storage;
using Tessa.UI;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess.CommandInterpreter
{
    /// <summary>
    /// Обработчик клиентской команды <see cref="DefaultCommandTypes.CreateCardViaTemplate"/>.
    /// </summary>
    public sealed class CreateCardViaTemplateCommandHandler : ClientCommandHandlerBase
    {
        #region Fields

        private readonly IUIHost uiHost;

        #endregion

        #region Constructor

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="CreateCardViaTemplateCommandHandler"/>.
        /// </summary>
        /// <param name="uiHost">Объект, предоставляющий упрощённый доступ к основным функциям платформы, которые связаны с отображением информации пользователю.</param>
        public CreateCardViaTemplateCommandHandler(
            IUIHost uiHost) =>
            this.uiHost = uiHost ?? throw new ArgumentNullException(nameof(uiHost));

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Handle(
            IClientCommandHandlerContext context)
        {
            var command = context.Command;
            if (command.Parameters.TryGetValue(KrConstants.Keys.TemplateID, out var templateIDObj)
                && templateIDObj is Guid templateID)
            {
                await DispatcherHelper.InvokeInUIAsync(async () =>
                {
                    using var splash = TessaSplash.Create(TessaSplashMessage.CreatingCard);

                    // Запоминаем запрос на создание карточки по шаблону, т.к. данный обработчик выполняется только при создании карточки на клиенте без сохранения.
                    await this.uiHost.CreateFromTemplateAsync(
                        templateID,
                        options: new CreateCardOptions
                        {
                            Splash = splash,
                            Info = new Dictionary<string, object>(StringComparer.Ordinal)
                            {
                                [CardHelper.NewCardBilletKey] = command.Parameters.TryGet<byte[]>(KrConstants.Keys.NewCard),
                                [CardHelper.NewCardBilletSignatureKey] = command.Parameters.TryGet<byte[]>(KrConstants.Keys.NewCardSignature),
                            },
                            SaveCreationRequest = true,
                        },
                        cancellationToken: context.CancellationToken);
                });
            }
        }

        #endregion
    }
}
