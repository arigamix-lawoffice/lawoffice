namespace Tessa.Extensions.Default.Server.Workflow.KrPermissions
{
    /// <summary>
    /// Контекст проверки прав доступа с дополнительной информацией для перерасчета токена.
    /// </summary>
    public interface IKrPermissionsRecalcContext : IKrPermissionsManagerContext
    {
        /// <summary>
        /// Определяет, требуется ли перерасчет токена.
        /// </summary>
        bool IsRecalcRequired { get; set; }
    }
}
