using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using Tessa.Platform;
using Tessa.Platform.IO;
using Tessa.Platform.Storage;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Настройки для тестов.
    /// </summary>
    public static class TestSettings
    {
        #region Constants And Static Fields

        private static readonly TimeSpan defaultGCKeepAliveInterval = new TimeSpan(6, 0, 0);

        #endregion

        #region Properties

        /// <summary>
        /// Путь к файловому хранилищу или значение <see langword="null"/>, если параметр не задан в конфигурации.
        /// </summary>
        public static string FileStoragePath { get; } = ConfigurationManager.Settings.TryGet<string>("FileStoragePath");

        /// <summary>
        /// Дата и время, используемые при создании имён временных ресурсов, используемых в тестах или значение <see langword="null"/>, если параметр не задан в конфигурации.
        /// </summary>
        public static DateTime? FixtureDate { get; } = ConfigurationManager.Settings.TryGet<DateTime?>("FixtureDate");

        /// <summary>
        /// Начальное значение для вычисления последовательности псевдослучайных чисел, используемых при создании имён временных ресурсов в тестах, или значение <see langword="null"/>, если параметр не задан в конфигурации.
        /// </summary>
        public static long? FixtureSeed { get; } = ConfigurationManager.Settings.TryGet<long?>("FixtureSeed");

        /// <summary>
        /// Строка подключения к локальной базе данных, содержащей информацию о внешних временных ресурсах.
        /// </summary>
        /// <remarks>
        /// Если строка подключения не задана в конфигурации, то используется строка подключения по умолчанию к локальной базе данных, расположенной в текущей рабочей папке.<para/>
        /// Для полноценной работы база данных с информацией о внешних объектах должна располагаться в месте защищённом от случайного удаления. Иначе информация об отслеживаемых объектах будет потеряна.
        /// </remarks>
        public static LiteDB.ConnectionString GCDbConnectionString { get; } = TryGetConnectionString();

        /// <summary>
        /// Время жизни внешних объектов, оставшихся после выполнения тестов. Значение должно быть не отрицательным.
        /// </summary>
        /// <remarks>Значение по умолчанию: "06:00:00".</remarks>
        public static TimeSpan GCKeepAliveInterval { get; } = GetGCKeepAliveInterval();

        /// <summary>
        /// Разрешено использовать области выполнения.
        /// </summary>
        /// <remarks>Значение по умолчанию: <see langword="true"/>.</remarks>
        public static bool UseTestScope { get; } = ConfigurationManager.Settings.TryGet(nameof(UseTestScope), true);

        #endregion

        #region Private Methods

        private static TimeSpan GetTimeSpan(
            IDictionary<string, object> settings,
            string key,
            TimeSpan defaultValue)
        {
            var text = settings.TryGet<string>(key);

            return string.IsNullOrEmpty(text)
                ? defaultValue
                : TimeSpan.ParseExact(text, "g", CultureInfo.InvariantCulture);
        }

        private static LiteDB.ConnectionString TryGetConnectionString()
        {
            const string key = "gc";

            if (!ConfigurationManager.Connections.TryGetValue(key, out var configurationConnection))
            {
                return new LiteDB.ConnectionString()
                {
                    Filename = "gclocal.db",
                    Connection = LiteDB.ConnectionType.Shared,
                };
            }

            var connectionString = configurationConnection.ConnectionString;

            if (string.IsNullOrEmpty(connectionString))
            {
                throw new InvalidOperationException(
                    $"Configuration string \"{key}\" is null or empty.");
            }

            var connection = new LiteDB.ConnectionString(connectionString);

            // Создание папки с БД.
            var dbDir = Path.GetDirectoryName(connection.Filename);
            FileHelper.CreateDirectoryIfNotExists(dbDir, true);

            return connection;
        }

        private static TimeSpan GetGCKeepAliveInterval()
        {
            var value = GetTimeSpan(ConfigurationManager.Settings, nameof(GCKeepAliveInterval), defaultGCKeepAliveInterval);

            if (value < TimeSpan.Zero)
            {
                throw new ArgumentOutOfRangeException(
                    nameof(GCKeepAliveInterval),
                    value,
                    "The value must be non-negative.");
            }

            return value;
        }

        #endregion
    }
}
