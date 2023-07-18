using Tessa.Cards;

namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Контекст расширения проверки прав доступа по заданию.
    /// </summary>
    public interface ITaskPermissionsExtensionContext : IKrPermissionsManagerContext
    {
        /// <summary>
        /// Объект задания.
        /// </summary>
        CardTask Task { get; set; }

        /// <summary>
        /// Тип задания.
        /// </summary>
        CardType TaskType { get; set; }
    }
}
