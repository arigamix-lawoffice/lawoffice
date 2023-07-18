namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Оболочка базового класса для тестов.
    /// </summary>
    /// <typeparam name="T">Тип объекта для которого создаётся оболочка.</typeparam>
    /// <remarks>
    /// Предназначена для создания классов с тестами, выполняемыми в различном окружении. Например, на клиенте и сервере.<para/>
    /// 
    /// Класс, содержащий общие тесты.
    /// <code language="cs">
    /// <![CDATA[
    /// public abstract class ExampleTestBase<T> :
    ///     TestBaseWrapper<T>
    ///     where T : ITestBase
    /// {
    ///     #region Constructors
    ///
    ///     protected ExampleTestBase(T internalTestBase)
    ///         : base(internalTestBase)
    ///     {
    ///     }
    ///
    ///     #endregion
    /// 
    ///     [Test]
    ///     public void TestMethod()
    ///     {
    ///     }
    /// }
    /// ]]>
    /// </code>
    /// 
    /// Класс, содержащий серверные тесты.
    /// <code language="cs">
    /// <![CDATA[
    /// public class ExampleServerTest :
    ///     ExampleTestBase<ExampleServerTest.ServerTestCore>
    /// {
    ///     #region Nested Types
    ///
    ///     public sealed class ServerTestCore :
    ///         ServerTestBase
    ///     {
    ///     }
    ///
    ///     #endregion
    ///
    ///     #region Constructors
    ///
    ///     public ExampleServerTest()
    ///         : base(new ServerTestCore())
    ///     {
    ///     }
    ///
    ///     #endregion
    /// }
    /// ]]>
    /// </code>
    /// 
    /// Класс, содержащий клиентские тесты, выполняемые на специально подготовленном сервере приложений.
    /// <code language="cs">
    /// <![CDATA[
    /// public class ExampleHybridClientTest :
    ///     ExampleTestBase<ExampleHybridClientTest.HybridClientTestCore>
    /// {
    ///     #region Nested Types
    ///
    ///     public sealed class HybridClientTestCore :
    ///         HybridClientTestBase
    ///     {
    ///     }
    ///
    ///     #endregion
    ///
    ///     #region Constructors
    ///
    ///     public ExampleHybridClientTest()
    ///         : base(new HybridClientTestCore())
    ///     {
    ///     }
    ///
    ///     #endregion
    /// }
    /// ]]>
    /// </code>
    /// </remarks>
    public interface ITestBaseWrapper<T> :
        ITestBase
        where T : class, ITestBase
    {
        #region Properties

        /// <summary>
        /// Возвращает объект для которого создана обёртка.
        /// </summary>
        public T InternalTestBase { get; }

        #endregion
    }
}
