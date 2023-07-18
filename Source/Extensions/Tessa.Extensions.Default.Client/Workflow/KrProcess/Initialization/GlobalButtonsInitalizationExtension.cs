using System.Threading.Tasks;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Initialization;
using Unity;

namespace Tessa.Extensions.Default.Client.Workflow.KrProcess.Initialization
{
    /// <summary>
    /// Расширение на инициализацию приложения со стороны клиента. Сохраняет информацию о тайлах глобальных вторичных процессов в <see cref="IKrGlobalTileContainer"/>.
    /// </summary>
    public class GlobalButtonsInitalizationExtension :
        ClientInitializationExtension
    {
        #region Fields

        private readonly IKrGlobalTileContainer tileContainer;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="GlobalButtonsInitalizationExtension"/>.
        /// </summary>
        /// <param name="tileContainer">Контейнер, содержащий информацию о тайлах глобальных вторичных процессов.</param>
        public GlobalButtonsInitalizationExtension(
            [OptionalDependency] IKrGlobalTileContainer tileContainer) =>
            this.tileContainer = tileContainer;

        #endregion

        #region Base Overrides

        /// <inheritdoc/>
        public override Task AfterRequest(IClientInitializationExtensionContext context)
        {
            if (!context.RequestIsSuccessful
                || this.tileContainer is null
                || !context.Response.TryGetGlobalTiles(out var globalTiles))
            {
                return Task.CompletedTask;
            }

            this.tileContainer.Init(globalTiles);

            return Task.CompletedTask;
        }

        #endregion
    }
}
