using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Расширение проверки прав доступа по заданию.
    /// </summary>
    public interface ITaskPermissionsExtension : IExtension
    {
        /// <summary>
        /// Метод, расширяющий права на карточку на основе содержащихся в ней заданий.
        /// </summary>
        /// <param name="context">
        /// Контекст расширения прав доступа по заданию.
        /// </param>
        Task ExtendPermissionsAsync(ITaskPermissionsExtensionContext context);
    }
}
