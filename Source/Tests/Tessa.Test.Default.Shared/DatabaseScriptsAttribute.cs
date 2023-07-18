using System;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using Tessa.Platform;
using Tessa.Platform.Data;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Устанавливает имена скриптов, которые должны быть выполнены при инициализации базы данных.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
    public sealed class DatabaseScriptsAttribute :
        NUnitAttribute,
        IApplyToTest
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр класса <see cref="DatabaseScriptsAttribute"/>.
        /// </summary>
        /// <param name="dbms">Тип БД, для которого должны выполняться указанные скрипты.</param>
        /// <param name="scriptFileNames">
        /// Массив имён файлов скриптов, которые должны быть выполнены при инициализации БД.<para/>
        /// Файлы должны быть включены в текущую сборку.<para/>
        /// Данные файлы скриптов выполняются только при совпадении типа текущей БД с указанным типом <see cref="Dbms"/>.
        /// </param>
        public DatabaseScriptsAttribute(Dbms dbms, params string[] scriptFileNames)
        {
            this.Dbms = dbms;
            this.ScriptFileNames = scriptFileNames;
        }

        #endregion

        #region IApplyToTest Overrides

        /// <inheritdoc/>
        public void ApplyToTest(NUnit.Framework.Internal.Test test)
        {
            if (test is null)
            {
                throw new ArgumentNullException(nameof(test));
            }

            if (this.ScriptFileNames is null)
            {
                return;
            }

            var properties = test.Properties;
            if (this.CheckDbms(properties))
            {
                properties[DatabasePropertyNames.ScriptFileNamesProvider] = this.ScriptFileNames;
            }
        }

        #endregion

        #region Properties

        /// <summary>
        /// Возвращает тип используемой БД.
        /// </summary>
        public Dbms Dbms { get; }

        /// <summary>
        /// Возвращает массив имён файлов скриптов, которые должны быть выполнены при инициализации БД.<para/>
        /// Файлы должны быть включены в текущую сборку.<para/>
        /// Данные файлы скриптов выполняются только при совпадении типа текущей БД с указанным типом <see cref="Dbms"/>.
        /// </summary>
        public string[] ScriptFileNames { get; }

        #endregion

        #region Private methods

        private bool CheckDbms(IPropertyBag properties)
        {
            // К тесту не применён атрибут DatabaseTestAttribute?
            if (properties.Get(DatabasePropertyNames.ConnectionStringName) is null)
            {
                return false;
            }

            var providerName = (string) properties.Get(DatabasePropertyNames.ProviderName);

            try
            {
                var factory = ConfigurationManager
                    .GetConfigurationDataProviderFromType(providerName)
                    .GetDbProviderFactory();

                var dbms = factory.GetDbms();
                return dbms == this.Dbms;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}
