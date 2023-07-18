using System.Threading.Tasks;
using Tessa.Extensions.Default.Client.Notifications;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess.ClientCommandInterpreter;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess.CommandInterpreter
{
    /// <summary>
    /// Обработчик клиентской команды <see cref="DefaultCommandTypes.RefreshAndNotify"/>.
    /// </summary>
    public sealed class RefreshAndNotifyClientCommandHandler : ClientCommandHandlerBase
    {
        #region Fields

        private readonly IKrNotificationManager notificationManager;

        #endregion

        #region Constructor

        public RefreshAndNotifyClientCommandHandler(IKrNotificationManager notificationManager)
        {
            // В клиентских командах можно получать любые IoC-зависимости
            this.notificationManager = notificationManager;
        }

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override async Task Handle(
            IClientCommandHandlerContext context)
        {
            if (await this.notificationManager.CanCheckTasksAsync(context.CancellationToken))
            {
                var _ = this.notificationManager.CheckTasksAsync(cancellationToken: context.CancellationToken);
            }
        }

        #endregion
    }
}