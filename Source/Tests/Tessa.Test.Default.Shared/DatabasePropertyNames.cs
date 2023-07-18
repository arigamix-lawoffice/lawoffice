using NUnit.Framework.Interfaces;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет имена констант по которым в <see cref="ITest.Properties"/> содержится информация о подключении к базе данных используемой при тестировании.
    /// </summary>
    public static class DatabasePropertyNames
    {
        /// <summary>
        /// Ключ по которому содержится строка подключения.
        /// </summary>
        public const string ConnectionString = "_DB_CONSTR";

        /// <summary>
        /// Ключ по которому содержится имя строки подключения.
        /// </summary>
        public const string ConnectionStringName = "_DB_CONSTRNAME";

        /// <summary>
        /// Ключ по которому содержится имя провайдера обеспечивающего взаимодействие с базой данных. Значение может быть не задано в <see cref="ITest.Properties"/>. В таком случае используется провайдер по умолчанию.
        /// </summary>
        public const string ProviderName = "_DB_PROVIDERNAME";

        /// <summary>
        /// Ключ по которому содержится строковый массив имён скриптов которые должны быть выполнены при инициализации базы данных.
        /// </summary>
        public const string ScriptFileNamesProvider = "_DB_SCRIPTFILENAMES";
    }
}
