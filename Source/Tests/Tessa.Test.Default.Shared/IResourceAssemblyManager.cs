using System.Reflection;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Предоставляет информацию о встроенных ресурсах.
    /// </summary>
    public interface IResourceAssemblyManager
    {
        #region Properties

        /// <summary>
        /// Возвращает сборку содержащую встроенные ресурсы.
        /// </summary>
        Assembly ResourceAssembly { get; }

        #endregion
    }
}
