namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет названия областей выполнения.
    /// </summary>
    public class TestScopeNames
    {
        /// <summary>
        /// Область выполнения по умолчанию для БД, содержащей данные, импортированные в <see cref="T:Tessa.Test.Default.Server.ServerTestBase"/> и SQL-скриптом <see cref="TestHelper.DbScriptDefaultMs"/> под управлением Sql Server.
        /// </summary>
        public const string ServerSqlServer = "Server-" + TestHelper.ShortSqlServerName;

        /// <summary>
        /// Область выполнения по умолчанию для БД, содержащей данные, импортированные в <see cref="T:Tessa.Test.Default.Server.ServerTestBase"/> и SQL-скриптом <see cref="TestHelper.DbScriptDefaultPg"/> под управлением PostgreSql.
        /// </summary>
        public const string ServerPostgreSql = "Server-" + TestHelper.ShortPostgreSqlName;

        /// <summary>
        /// Область выполнения по умолчанию для БД, содержащей данные, импортированные в <see cref="T:Tessa.Test.Default.Server.Kr.KrServerTestBase"/> и SQL-скриптом <see cref="TestHelper.DbScriptDefaultMs"/> под управлением Sql Server.
        /// </summary>
        public const string KrServerSqlServer = "KrServer-" + TestHelper.ShortSqlServerName;

        /// <summary>
        /// Область выполнения по умолчанию для БД, содержащей данные, импортированные в <see cref="T:Tessa.Test.Default.Server.Kr.KrServerTestBase"/>и SQL-скриптом <see cref="TestHelper.DbScriptDefaultPg"/> под управлением PostgreSql.
        /// </summary>
        public const string KrServerPostgreSql = "KrServer-" + TestHelper.ShortPostgreSqlName;

        /// <summary>
        /// Область выполнения по умолчанию для БД, содержащей данные, импортированные в <see cref="T:Tessa.Test.Default.Client.HybridClientTestBase"/> и SQL-скриптом <see cref="TestHelper.DbScriptDefaultMs"/> под управлением Sql Server.
        /// </summary>
        public const string HybridClientSqlServer = "HybridClient-" + TestHelper.ShortSqlServerName;

        /// <summary>
        /// Область выполнения по умолчанию для БД, содержащей данные, импортированные в <see cref="T:Tessa.Test.Default.Client.HybridClientTestBase"/> и SQL-скриптом <see cref="TestHelper.DbScriptDefaultPg"/> под управлением PostgreSql.
        /// </summary>
        public const string HybridClientPostgreSql = "HybridClient-" + TestHelper.ShortPostgreSqlName;

        /// <summary>
        /// Область выполнения по умолчанию для БД, содержащей данные, импортированные в <see cref="T:Tessa.Test.Default.Client.Kr.KrHybridClientTestBase"/> и SQL-скриптом <see cref="TestHelper.DbScriptDefaultMs"/> под управлением Sql Server.
        /// </summary>
        public const string KrHybridClientSqlServer = "KrHybridClient-" + TestHelper.ShortSqlServerName;

        /// <summary>
        /// Область выполнения по умолчанию для БД, содержащей данные, импортированные в <see cref="T:Tessa.Test.Default.Client.Kr.KrHybridClientTestBase"/> и SQL-скриптом <see cref="TestHelper.DbScriptDefaultPg"/> под управлением PostgreSql.
        /// </summary>
        public const string KrHybridClientPostgreSql = "KrHybridClient-" + TestHelper.ShortPostgreSqlName;

        /// <summary>
        /// Область выполнения по умолчанию для БД, содержащей данные, импортированные в <see cref="T:Tessa.Test.Default.Server.Workflow.WeScenarioTestBase"/> и SQL-скриптом <see cref="TestHelper.DbScriptDefaultMs"/> под управлением Sql Server.
        /// </summary>
        public const string WorkflowEngineSqlServer = "WorkflowEngine-" + TestHelper.ShortSqlServerName;

        /// <summary>
        /// Область выполнения по умолчанию для БД, содержащей данные, импортированные в <see cref="T:Tessa.Test.Default.Server.Workflow.WeScenarioTestBase"/> и SQL-скриптом <see cref="TestHelper.DbScriptDefaultPg"/> под управлением PostgreSql.
        /// </summary>
        public const string WorkflowEnginePostgreSql = "WorkflowEngine-" + TestHelper.ShortPostgreSqlName;

    }
}
