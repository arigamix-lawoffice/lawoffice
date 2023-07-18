namespace Tessa.Extensions.Default.Shared.Workflow.KrCompilers
{
    /// <summary>
    /// Тип действия с этапом маршрута.
    /// </summary>
    public enum RouteDiffAction
    {
        /// <summary>
        /// Этап был добавлен.
        /// </summary>
        Insert,

        /// <summary>
        /// Этап был удалён.
        /// </summary>
        Delete,

        /// <summary>
        /// Этап был изменён.
        /// </summary>
        Modify,
    }
}
