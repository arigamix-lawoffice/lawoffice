using System.Threading;
using System.Threading.Tasks;
using Chronos.Plugins;
using NLog;
using Tessa.Platform;
using Tessa.Platform.Data;
using Unity;

namespace Tessa.Extensions.Chronos.Plugins
{
    /// <summary>
    /// Пример плагина, который может работать через серверное API.
    /// </summary>
    [Plugin(
        Name = "Example plugin",
        Description = "Plugin is used as an example of how Chronos plugins should be created"
            + " to communicate with database as server.",
        Version = 1,
        ConfigFile = ConfigFilePath)]
    public sealed class ExamplePlugin :
        Plugin
    {
        #region Constants

        /// <summary>
        /// Относительный путь к конфигурационному файлу плагина.
        /// </summary>
        private const string ConfigFilePath = "configuration/ExamplePlugin.xml";

        #endregion

        #region Fields

        private static readonly Logger logger = LogManager.GetCurrentClassLogger();

        #endregion

        #region Base Overrides

        public override async Task EntryPointAsync(CancellationToken cancellationToken = default)
        {
            logger.Trace("Starting plugin");

            // конфигурируем контейнер Unity для использования стандартных серверных API (в т.ч. API карточек)
            // а также для получения прямого доступа к базе данных через IDbScope по строке подключения из app.config;
            // предполагаем, что все действия, совершаемые плагином, будут выполняться от имени пользователя System
            logger.Trace("Configuring container");

            IUnityContainer container = await new UnityContainer()
                // дополнительные регистрации в контейнере Unity могут быть здесь:
                // .RegisterType<MyService>(new ContainerControlledLifetimeManager())

                .RegisterServerForPluginAsync(cancellationToken: cancellationToken)
                ;

            // любая полезная работа плагина может быть здесь
            logger.Trace("Doing useful stuff here");

            IDbScope dbScope = container.Resolve<IDbScope>();
            await using (dbScope.Create())
            {
                // работа в пределах одного SQL-соединения, транзакция при этом явно не создаётся

                if (this.StopRequested)
                {
                    // была запрошена асинхронная остановка, можно периодически проверять значение этого свойства,
                    // и консистентно завершать выполнение (закрыть транзакцию, если была открыта, и др.)
                    return;
                }

                var db = dbScope.Db;
                var builder = dbScope.BuilderFactory;

                int value = await db
                    .SetCommand(
                        builder
                            .Select().Top(1).V(1)
                            .From("Instances").NoLock()
                            .Limit(1).Build())
                    .LogCommand()
                    .ExecuteAsync<int>(cancellationToken);

                if (value == 1)
                {
                    logger.Trace("Acquired value from db");
                }

                // var cardRepository = container.Resolve<Tessa.Cards.ICardRepository>();
                // var response = await cardRepository.GetAsync(new Tessa.Cards.CardGetRequest { CardID = System.Guid.NewGuid() }, cancellationToken);
            }

            logger.Trace("Shutting down");
        }

        #endregion
    }
}
