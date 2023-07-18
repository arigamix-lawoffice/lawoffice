using System.Reflection;

namespace Tessa.Test.Default.Shared
{
    /// <inheritdoc cref="IResourceAssemblyManager"/>
    /// <remarks>Класс-наследник, экземпляр которого будет использоваться в тестах, должен находиться в сборке, содержащей необходимые встроенные ресурсы.</remarks>
    public abstract class ResourceAssemblyManager :
        IResourceAssemblyManager
    {
        #region Fields

        private Assembly resourceAssembly;

        #endregion

        #region Properties

        /// <inheritdoc/>
        public Assembly ResourceAssembly => this.resourceAssembly ??= this.GetType().Assembly;

        #endregion
    }
}
