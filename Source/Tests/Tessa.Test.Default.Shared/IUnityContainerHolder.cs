using Unity;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Контейнер для объекта <see cref="IUnityContainer"/>, содержащего регистрации
    /// для различных API, например, для клиентской сессии, обеспечивающей доступ к веб-сервисам.
    /// </summary>
    public interface IUnityContainerHolder
    {
        /// <summary>
        /// Контейнер Unity. Не должен быть равен <c>null</c>.
        /// </summary>
        IUnityContainer UnityContainer { get; set; }
    }
}
