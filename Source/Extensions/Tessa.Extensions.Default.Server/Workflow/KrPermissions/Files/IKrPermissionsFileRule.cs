#nullable enable

using System.Threading.Tasks;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions.Files
{
    /// <summary>
    /// Правило для проверки доступа к файлу.
    /// </summary>
    public interface IKrPermissionsFileRule
    {
        /// <summary>
        /// Приоритет правила доступа к файлу.
        /// </summary>
        int Priority { get; }

        /// <summary>
        /// Доступ на добавление файла или <c>null</c>, если правило не определяет данный доступ.
        /// </summary>
        int? AddAccessSetting { get; }

        /// <summary>
        /// Доступ на чтение файла или <c>null</c>, если правило не определяет данный доступ.
        /// </summary>
        int? ReadAccessSetting { get; }

        /// <summary>
        /// Доступ на редактирование файла или <c>null</c>, если правило не определяет данный доступ.
        /// </summary>
        int? EditAccessSetting { get; }

        /// <summary>
        /// Доступ на удаление файла или <c>null</c>, если правило не определяет данный доступ.
        /// </summary>
        int? DeleteAccessSetting { get; }

        /// <summary>
        /// Доступ на подписание файла или <c>null</c>, если правило не определяет данный доступ.
        /// </summary>
        int? SignAccessSetting { get; }

        /// <summary>
        /// Ограничение на размер файла в байтах.
        /// </summary>
        long? FileSizeLimit { get; }

        /// <summary>
        /// Метод для проверки выполнения правила для файла.
        /// </summary>
        /// <param name="userID">Контекст проверки доступа к файлу.</param>
        /// <returns>Значение <c>true</c>, если проверка выполнена успешно, иначе <c>false</c>.</returns>
        ValueTask<bool> CheckFileAsync(IKrPermissionsFilesManagerContext context);
    }
}
