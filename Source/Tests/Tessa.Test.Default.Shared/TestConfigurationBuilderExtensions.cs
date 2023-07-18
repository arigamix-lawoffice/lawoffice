using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.SmartMerge;
using Tessa.Platform.Data;
using Tessa.Platform.Validation;
using Tessa.Test.Default.Shared.Kr;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет статические методы расширения для объекта <see cref="TestConfigurationBuilder"/>
    /// </summary>
    public static class TestConfigurationBuilderExtensions
    {
        #region Import Cards Methods

        /// <summary>
        /// Импортирует карточки типового процесса согласования, расположенные в папке <see cref="ResourcesPaths.Cards.KrProcess"/>.
        /// </summary>
        /// <param name="builder">Объект <see cref="TestConfigurationBuilder"/> выполняющий конфигурирование тестовой базы данных.</param>
        /// <param name="cardPredicateAsync">Функция определяющая возможность импорта карточки или значение по умолчанию для типа, если фильтрация не выполняется.</param>
        /// <param name="getMergeOptionsFuncAsync">Функция возвращающая параметры объединения для файла с заданным именем или значение по умолчанию для типа, если используются параметры по умолчанию. Параметры: имя файла для которого запрашиваются параметры объединения.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static TestConfigurationBuilder ImportKrProcessCards(
            this TestConfigurationBuilder builder,
            Func<Card, CancellationToken, ValueTask<bool>> cardPredicateAsync = default,
            Func<string, CancellationToken, ValueTask<ICardMergeOptions>> getMergeOptionsFuncAsync = default) =>
            builder.ImportCardsFromDirectory(
                static (_, _) => new ValueTask<string>(ResourcesPaths.Cards.KrProcess),
                cardPredicateAsync,
                getMergeOptionsFuncAsync);

        /// <summary>
        /// Импортирует карточки типов условий, расположенные в папке <see cref="ResourcesPaths.Cards.ConditionTypes"/>.
        /// </summary>
        /// <param name="builder">Объект <see cref="TestConfigurationBuilder"/> выполняющий конфигурирование тестовой базы данных.</param>
        /// <param name="cardPredicateAsync">Функция определяющая возможность импорта карточки или значение по умолчанию для типа, если фильтрация не выполняется.</param>
        /// <param name="getMergeOptionsFuncAsync">Функция возвращающая параметры объединения для файла с заданным именем или значение по умолчанию для типа, если используются параметры по умолчанию. Параметры: имя файла для которого запрашиваются параметры объединения.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static TestConfigurationBuilder ImportConditionTypesCards(
            this TestConfigurationBuilder builder,
            Func<Card, CancellationToken, ValueTask<bool>> cardPredicateAsync = default,
            Func<string, CancellationToken, ValueTask<ICardMergeOptions>> getMergeOptionsFuncAsync = default) =>
            builder
                .ImportCardsFromDirectory(
                    static (_, _) => new ValueTask<string>(Path.Combine(ResourcesPaths.Cards.Configuration, ResourcesPaths.Cards.ConditionTypes)),
                    cardPredicateAsync,
                    getMergeOptionsFuncAsync);

        /// <summary>
        /// Импортирует карточки ролей, расположенные в папке <see cref="ResourcesPaths.Cards.Roles.SqlServer"/> или <see cref="ResourcesPaths.Cards.Roles.PostgreSql"/>.
        /// </summary>
        /// <param name="builder">Объект <see cref="TestConfigurationBuilder"/> выполняющий конфигурирование тестовой базы данных.</param>
        /// <param name="cardPredicateAsync">Функция определяющая возможность импорта карточки или значение по умолчанию для типа, если фильтрация не выполняется.</param>
        /// <param name="getMergeOptionsFuncAsync">Функция возвращающая параметры объединения для файла с заданным именем или значение по умолчанию для типа, если используются параметры по умолчанию. Параметры: имя файла для которого запрашиваются параметры объединения.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static TestConfigurationBuilder ImportRoleCards(
            this TestConfigurationBuilder builder,
            Func<Card, CancellationToken, ValueTask<bool>> cardPredicateAsync = default,
            Func<string, CancellationToken, ValueTask<ICardMergeOptions>> getMergeOptionsFuncAsync = default)
        {
            return builder.ImportCardsFromDirectory(
                static async (builder, cancellationToken) =>
                {
                    var dbms = await builder.DbScope.GetDbmsAsync(cancellationToken);

                    return dbms switch
                    {
                        Dbms.SqlServer => Path.Combine(ResourcesPaths.Cards.Roles.Name, ResourcesPaths.Cards.Roles.SqlServer),
                        Dbms.PostgreSql => Path.Combine(ResourcesPaths.Cards.Roles.Name, ResourcesPaths.Cards.Roles.PostgreSql),
                        _ => throw new NotSupportedException($"Dbms {dbms:G} is not supported."),
                    };
                },
                cardPredicateAsync,
                getMergeOptionsFuncAsync);
        }

        /// <summary>
        /// Импортирует карточки уведомлений (из папки <see cref="ResourcesPaths.Cards.Notifications"/>) и типов уведомлений (из папки <see cref="ResourcesPaths.Cards.NotificationTypes"/>).
        /// </summary>
        /// <param name="builder">Объект <see cref="TestConfigurationBuilder"/> выполняющий конфигурирование тестовой базы данных.</param>
        /// <param name="cardPredicateAsync">Функция определяющая возможность импорта карточки или значение по умолчанию для типа, если фильтрация не выполняется.</param>
        /// <param name="getMergeOptionsFuncAsync">Функция возвращающая параметры объединения для файла с заданным именем или значение по умолчанию для типа, если используются параметры по умолчанию. Параметры: имя файла для которого запрашиваются параметры объединения.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static TestConfigurationBuilder ImportNotificationCards(
            this TestConfigurationBuilder builder,
            Func<Card, CancellationToken, ValueTask<bool>> cardPredicateAsync = default,
            Func<string, CancellationToken, ValueTask<ICardMergeOptions>> getMergeOptionsFuncAsync = default) =>
            builder
                .ImportCardsFromDirectory(
                    static (_, _) => new ValueTask<string>(ResourcesPaths.Cards.NotificationTypes),
                    cardPredicateAsync,
                    getMergeOptionsFuncAsync)
                .ImportCardsFromDirectory(
                    static (_, _) => new ValueTask<string>(ResourcesPaths.Cards.Notifications),
                    cardPredicateAsync,
                    getMergeOptionsFuncAsync);

        /// <summary>
        /// Импортирует карточки типов документов (из папки <see cref="ResourcesPaths.Cards.DocumentTypes"/>).
        /// </summary>
        /// <param name="builder">Объект <see cref="TestConfigurationBuilder"/> выполняющий конфигурирование тестовой базы данных.</param>
        /// <param name="cardPredicateAsync">Функция определяющая возможность импорта карточки или значение по умолчанию для типа, если фильтрация не выполняется.</param>
        /// <param name="getMergeOptionsFuncAsync">Функция возвращающая параметры объединения для файла с заданным именем или значение по умолчанию для типа, если используются параметры по умолчанию. Параметры: имя файла для которого запрашиваются параметры объединения.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static TestConfigurationBuilder ImportDocumentTypesCards(
            this TestConfigurationBuilder builder,
            Func<Card, CancellationToken, ValueTask<bool>> cardPredicateAsync = default,
            Func<string, CancellationToken, ValueTask<ICardMergeOptions>> getMergeOptionsFuncAsync = default) =>
            builder.ImportCardsFromDirectory(
                static (_, _) => new ValueTask<string>(ResourcesPaths.Cards.DocumentTypes),
                cardPredicateAsync,
                getMergeOptionsFuncAsync);

        /// <summary>
        /// Импортирует карточки в соответствии с используемой СУБД: "<see cref="ResourcesPaths.Cards.Name"/>\<see cref="ResourcesPaths.Cards.Configuration"/>\Platform.jcardlib".
        /// </summary>
        /// <param name="builder">Объект <see cref="TestConfigurationBuilder"/> выполняющий конфигурирование тестовой базы данных.</param>
        /// <param name="cardPredicateAsync">Функция определяющая возможность импорта карточки или значение по умолчанию для типа, если фильтрация не выполняется.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static TestConfigurationBuilder ImportCardsWithTessaCardLib(
            this TestConfigurationBuilder builder,
            Func<Card, CancellationToken, ValueTask<bool>> cardPredicateAsync = default) =>
            builder.ImportCardsWithCardLib(static (_, _) => new ValueTask<string>(Path.Combine(ResourcesPaths.Cards.Configuration, "Platform.jcardlib")), cardPredicateAsync);

        /// <summary>
        /// Импортирует карточки в соответствии с "<see cref="ResourcesPaths.Cards.Name"/>\<see cref="ResourcesPaths.Cards.Configuration"/>\File templates.jcardlib".
        /// </summary>
        /// <param name="builder">Объект <see cref="TestConfigurationBuilder"/> выполняющий конфигурирование тестовой базы данных.</param>
        /// <param name="cardPredicateAsync">Функция определяющая возможность импорта карточки или значение по умолчанию для типа, если фильтрация не выполняется.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static TestConfigurationBuilder ImportCardsWithFileTemplatesCardLib(
            this TestConfigurationBuilder builder,
            Func<Card, CancellationToken, ValueTask<bool>> cardPredicateAsync = default) =>
            builder.ImportCardsWithCardLib(static (_, _) => new ValueTask<string>(Path.Combine(ResourcesPaths.Cards.Configuration, "File templates.jcardlib")), cardPredicateAsync);

        #endregion

        #region Import Card Types Methods

        /// <summary>
        /// Импортирует все типы карточек, расположенные в директориях: <see cref="ResourcesPaths.Types.Cards"/>, <see cref="ResourcesPaths.Types.Dialogs"/>, <see cref="ResourcesPaths.Types.Files"/>, <see cref="ResourcesPaths.Types.Tasks"/>.
        /// </summary>
        /// <param name="callbackAsync">Метод, вызываемый перед импортом каждого типа.</param>
        /// <returns>Объект <see cref="TestConfigurationBuilder"/> для создания цепочки.</returns>
        /// <exception cref="InvalidOperationException">Метод доступен только в серверных тестах.</exception>
        /// <remarks>
        /// Этот метод реализуется с помощью отложенного выполнения. Для выполнения запрошенного действия необходимо вызвать метод <see cref="IPendingActionsExecutor{T}.GoAsync(Action{ValidationResult}, CancellationToken)"/>.
        /// </remarks>
        public static TestConfigurationBuilder ImportAllTypes(
            this TestConfigurationBuilder builder,
            Func<CardType, CancellationToken, ValueTask> callbackAsync = default) =>
            builder
                .ImportTypesFromDirectory(ResourcesPaths.Types.Cards, callbackAsync)
                .ImportTypesFromDirectory(ResourcesPaths.Types.Dialogs, callbackAsync)
                .ImportTypesFromDirectory(ResourcesPaths.Types.Files, callbackAsync)
                .ImportTypesFromDirectory(ResourcesPaths.Types.Tasks, callbackAsync);

        #endregion

        #region Get Configurator Methods

        /// <summary>
        /// Возвращает конфигуратор, предоставляющий методы, выполняющие настройку параметров сервера.
        /// </summary>
        /// <param name="builder">Объект <see cref="TestConfigurationBuilder"/> выполняющий конфигурирование тестовой базы данных.</param>
        /// <returns>Объект <see cref="ServerConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Для возврата к текущему конфигуратору используйте метод <see cref="IConfiguratorScopeManager{T}.Complete"/>.<para/>
        /// Все запланированные действия будут выполнены при выполнении запланированных действий текущего конфигуратора.
        /// </remarks>
        public static ServerConfigurator GetServerConfigurator(
            this TestConfigurationBuilder builder)
        {
            return builder.RegisterPendingActionsProducer(
                new ServerConfigurator(
                    builder.Dependencies,
                    builder.CardFileSourceSettings,
                    builder));
        }

        /// <summary>
        /// Возвращает конфигуратор, предоставляющий методы, выполняющие настройку правил доступа.
        /// </summary>
        /// <param name="builder">Объект <see cref="TestConfigurationBuilder"/> выполняющий конфигурирование тестовой базы данных.</param>
        /// <returns>Объект <see cref="PermissionsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Для возврата к текущему конфигуратору используйте метод <see cref="IConfiguratorScopeManager{T}.Complete"/>.<para/>
        /// Все запланированные действия будут выполнены при выполнении запланированных действий текущего конфигуратора.
        /// </remarks>
        public static PermissionsConfigurator GetPermissionsConfigurator(
            this TestConfigurationBuilder builder)
        {
            return builder.RegisterPendingActionsProducer(
                new PermissionsConfigurator(
                    builder.Dependencies,
                    builder));
        }

        /// <summary>
        /// Возвращает конфигуратор, предоставляющий методы, выполняющие настройку лицензий.
        /// </summary>
        /// <param name="builder">Объект <see cref="TestConfigurationBuilder"/> выполняющий конфигурирование тестовой базы данных.</param>
        /// <returns>Объект <see cref="LicenseConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Для возврата к текущему конфигуратору используйте метод <see cref="IConfiguratorScopeManager{T}.Complete"/>.<para/>
        /// Все запланированные действия будут выполнены при выполнении запланированных действий текущего конфигуратора.
        /// </remarks>
        public static LicenseConfigurator GetLicenseConfigurator(
            this TestConfigurationBuilder builder)
        {
            return builder.RegisterPendingActionsProducer(
                new LicenseConfigurator(
                    builder.Dependencies,
                    builder));
        }

        /// <summary>
        /// Возвращает конфигуратор, предоставляющий методы, выполняющие настройку типового решения.
        /// </summary>
        /// <param name="builder">Объект <see cref="TestConfigurationBuilder"/> выполняющий конфигурирование тестовой базы данных.</param>
        /// <returns>Объект <see cref="KrSettingsConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Для возврата к текущему конфигуратору используйте метод <see cref="IConfiguratorScopeManager{T}.Complete"/>.<para/>
        /// Все запланированные действия будут выполнены при выполнении запланированных действий текущего конфигуратора.
        /// </remarks>
        public static KrSettingsConfigurator GetKrSettingsConfigurator(
            this TestConfigurationBuilder builder)
        {
            return builder.RegisterPendingActionsProducer(
                new KrSettingsConfigurator(
                    builder.Dependencies,
                    builder));
        }

        /// <summary>
        /// Возвращает конфигуратор, предоставляющий методы, выполняющие настройку типов документов.
        /// </summary>
        /// <param name="builder">Объект <see cref="TestConfigurationBuilder"/> выполняющий конфигурирование тестовой базы данных.</param>
        /// <returns>Объект <see cref="KrDocTypesConfigurator"/> для создания цепочки.</returns>
        /// <remarks>
        /// Для возврата к текущему конфигуратору используйте метод <see cref="IConfiguratorScopeManager{T}.Complete"/>.<para/>
        /// Все запланированные действия будут выполнены при выполнении запланированных действий текущего конфигуратора.
        /// </remarks>
        public static KrDocTypesConfigurator GetKrDocTypesConfigurator(
            this TestConfigurationBuilder builder)
        {
            return builder.RegisterPendingActionsProducer(
                new KrDocTypesConfigurator(
                    builder.Dependencies,
                    builder));
        }

        #endregion
    }
}
