using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.Platform.Validation;

namespace Tessa.Test.Default.Shared.Kr
{
    /// <summary>
    /// Предоставляет методы, выполняющие настройку параметров сервера.
    /// </summary>
    public class ServerConfigurator :
        CardLifecycleCompanion<ServerConfigurator>,
        IConfiguratorScopeManager<TestConfigurationBuilder>
    {
        #region Fields

        private readonly ICardFileSourceSettings cardFileSourceSettings;
        private readonly ConfiguratorScopeManager<TestConfigurationBuilder> configuratorScopeManager;

        #endregion

        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ServerConfigurator"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        /// <param name="cardFileSourceSettings">Потокобезопасный кэш настроек по всем местоположениям файлов. Если значение не задано, то выполняется инициализация значениями по умолчанию.</param>
        public ServerConfigurator(
            ICardLifecycleCompanionDependencies deps,
            ICardFileSourceSettings cardFileSourceSettings)
            : this(deps, cardFileSourceSettings, default)
        {
        }

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="ServerConfigurator"/>.
        /// </summary>
        /// <param name="deps">Зависимости, используемые при взаимодействии с карточками.</param>
        /// <param name="cardFileSourceSettings">Потокобезопасный кэш настроек по всем местоположениям файлов. Если значение не задано, то выполняется инициализация значениями по умолчанию.</param>
        /// <param name="scope">Конфигуратор верхнего уровня. Может быть не задан.</param>
        public ServerConfigurator(
            ICardLifecycleCompanionDependencies deps,
            ICardFileSourceSettings cardFileSourceSettings,
            TestConfigurationBuilder scope)
            : base(CardHelper.ServerInstanceTypeID, CardHelper.ServerInstanceTypeName, deps)
        {
            this.cardFileSourceSettings = cardFileSourceSettings;
            this.configuratorScopeManager = new ConfiguratorScopeManager<TestConfigurationBuilder>(scope);
        }

        #endregion

        #region IConfiguratorScopeManager<T> Members

        /// <inheritdoc/>
        public TestConfigurationBuilder Complete() =>
            this.configuratorScopeManager.Complete();

        #endregion

        #region Public Methods

        /// <summary>
        /// Изменяет путь к файловому хранилищу на файловое хранилище используемое для тестов. Настройка выполняется только для хранилищ у которых не стоит флаг "База данных".
        /// </summary>
        /// <param name="sourceID">Идентификатор настраиваемого файлового хранилища.</param>
        /// <param name="getPathFunc">Метод, возвращающий путь к файловому хранилищу. Параметр: строка, содержащая информацию о файловом хранилище.</param>
        /// <returns>Объект <see cref="ServerConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public ServerConfigurator ChangeFileSourcePathWithTestSource(
            int sourceID,
            Func<CardRow, string> getPathFunc) =>
            this.ChangeFileSourcePathWithTestSource(i => i.Get<int>("SourceID") == sourceID, getPathFunc);

        /// <summary>
        /// Изменяет путь к файловому хранилищу на файловое хранилище используемое для тестов. Настройка выполняется только для хранилищ у которых не стоит флаг "База данных".
        /// </summary>
        /// <param name="predicate">Условие отбора настраиваемых файловых хранилищ.</param>
        /// <param name="getPathFunc">Метод, возвращающий путь к файловому хранилищу. Параметр: строка, содержащая информацию о файловом хранилище.</param>
        /// <returns>Объект <see cref="ServerConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public ServerConfigurator ChangeFileSourcePathWithTestSource(
            Func<CardRow, bool> predicate,
            Func<CardRow, string> getPathFunc)
        {
            Check.ArgumentNotNull(predicate, nameof(predicate));
            Check.ArgumentNotNull(getPathFunc, nameof(getPathFunc));

            this.ApplyAction((clc, action) =>
            {
                var serverInstance = clc.Card;
                if (serverInstance.StoreMode == CardStoreMode.Update)
                {
                    var fileSourceRows = serverInstance.Sections["FileSourcesVirtual"].Rows
                    .Where(i => !i.Get<bool>("IsDatabase"))
                    .Where(predicate);

                    foreach (var fileSourceRow in fileSourceRows)
                    {
                        fileSourceRow.Fields["Path"] = getPathFunc(fileSourceRow);
                    }
                }
            });

            return this;
        }

        /// <summary>
        /// Инициализирует карточку параметров сервера. Если карточка имеет состояние отличное от <see cref="CardStoreMode.Insert"/>, то не выполняет действий.
        /// </summary>
        /// <param name="fileStoragePath">Путь к файловому хранилищу (<see cref="CardFileSourceType.FileSystem"/>).</param>
        /// <returns>Объект <see cref="ServerConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public virtual ServerConfigurator InitializeServerInstance(string fileStoragePath)
        {
            Check.ArgumentNotNullOrWhiteSpace(fileStoragePath, nameof(fileStoragePath));

            return this.ApplyAction((clc, _, ct) =>
                this.InitializeServerInstanceAsync(clc, fileStoragePath, ct));
        }

        #endregion

        #region Private methods

        private async ValueTask<ValidationResult> InitializeServerInstanceAsync(
            ServerConfigurator clc,
            string fileStoragePath,
            CancellationToken cancellationToken = default)
        {
            var card = clc.GetCardOrThrow();

            if (card.StoreMode == CardStoreMode.Update)
            {
                return ValidationResult.Empty;
            }

            var serverInstancesFields = card.Sections["ServerInstances"].Fields;
            serverInstancesFields["Name"] = RuntimeHelper.DefaultInstanceName;

            var newSourceRow = clc.LastData.NewResponse.SectionRows["FileSourcesVirtual"];
            var fileSourcesVirtualRows = card.Sections["FileSourcesVirtual"].Rows;
            ICardFileSource defaultCardFileSource;
            int defaultFileSourceID;

            if (this.cardFileSourceSettings is null
                || (defaultCardFileSource = await this.cardFileSourceSettings.GetDefaultAsync(cancellationToken)) is null)
            {
                defaultFileSourceID = CardFileSourceType.FileSystem.ID;

                var source = fileSourcesVirtualRows.Add();
                source.Set(newSourceRow);

                FillCardFileSourceRow(
                    row: source,
                    type: CardFileSourceType.FileSystem,
                    name: nameof(CardFileSourceType.FileSystem),
                    isDefault: true,
                    path: fileStoragePath,
                    isDatabase: false,
                    size: 0,
                    maxSize: 0,
                    fileExtensions: default);

                source = fileSourcesVirtualRows.Add();
                source.Set(newSourceRow);

                FillCardFileSourceRow(
                    row: source,
                    type: CardFileSourceType.Database,
                    name: nameof(CardFileSourceType.Database),
                    isDefault: false,
                    path: CardFileSourceType.DefaultDatabasePath,
                    isDatabase: true,
                    size: 0,
                    maxSize: 0,
                    fileExtensions: default);
            }
            else
            {
                var cardFileSources = await this.cardFileSourceSettings.GetAllAsync(cancellationToken);
                var defaultCardFileSourceTypeID = defaultCardFileSource.Type.ID;
                defaultFileSourceID = defaultCardFileSourceTypeID;

                foreach (var cardFileSource in cardFileSources)
                {
                    var row = fileSourcesVirtualRows.Add();
                    row.Set(newSourceRow);

                    FillCardFileSourceRow(
                        row: row,
                        type: cardFileSource.Type,
                        name: cardFileSource.Name,
                        isDefault: defaultCardFileSourceTypeID == cardFileSource.Type.ID,
                        path: cardFileSource.Path,
                        isDatabase: cardFileSource.IsDatabase,
                        size: cardFileSource.Size,
                        maxSize: cardFileSource.MaxSize,
                        fileExtensions: default);
                }
            }

            serverInstancesFields["DefaultFileSourceID"] = Int32Boxes.Box(defaultFileSourceID);
            serverInstancesFields["DefaultCalendarID"] = CalendarTestsHelper.DefaultCalendarID;
            serverInstancesFields["DefaultCalendarName"] = CalendarTestsHelper.DefaultCalendarName;

            return ValidationResult.Empty;
        }

        private static void FillCardFileSourceRow(
            CardRow row,
            CardFileSourceType type,
            string name,
            bool isDefault,
            string path,
            bool isDatabase,
            int size,
            int maxSize,
            string fileExtensions)
        {
            row.RowID = Guid.NewGuid();
            var fields = row.Fields;
            fields["IsDatabase"] = BooleanBoxes.Box(isDatabase);
            fields["IsDefault"] = BooleanBoxes.Box(isDefault);
            fields["MaxSize"] = Int32Boxes.Box(maxSize);
            fields["Name"] = name;
            fields["Path"] = path;
            fields["Size"] = Int32Boxes.Box(size);
            fields["SourceID"] = Int32Boxes.Box(type.ID);
            fields["SourceIDText"] = default;
            fields["FileExtensions"] = fileExtensions;
            row.State = CardRowState.Inserted;
        }

        #endregion
    }
}
