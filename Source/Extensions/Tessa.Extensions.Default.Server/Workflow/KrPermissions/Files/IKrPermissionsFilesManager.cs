#nullable enable

using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files
{
    /// <summary>
    /// Объект для проверки доступа к файлу по правилам доступа.
    /// </summary>
    public interface IKrPermissionsFilesManager
    {
        /// <summary>
        /// Расчитывает доступ к файлу по расширенным правилам доступа.
        /// </summary>
        /// <param name="context">Контекст расчёта доступа к файлу.</param>
        /// <returns>Настройки доступа к файлу.</returns>
        ValueTask<IKrPermissionsFilesManagerResult> CheckPermissionsAsync(IKrPermissionsFilesManagerContext context);
    }
}
