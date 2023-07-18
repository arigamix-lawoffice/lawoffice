namespace Tessa.Extensions.Default.Shared.Settings
{
    /// <summary>
    /// Режим по умолчанию для флажков, которые устанавливаются в задаче при выборе нескольких исполнителей.
    /// </summary>
    public enum WfMultiplePerformersDefaults
    {
        /// <summary>
        /// Объединить список персональных ролей в единственную задачу.
        /// </summary>
        MergeIntoSingleTask,

        /// <summary>
        /// Создать по одной задаче на каждую роль. Ответственный исполнитель не назначается.
        /// </summary>
        CreateMultipleTasks,

        /// <summary>
        /// Создать по одной задаче на каждую роль, причём первая роль назначается ответственным исполнителем.
        /// </summary>
        CreateMultipleTasksWithMajorPerformer,

        /// <summary>
        /// Значение по умолчанию. В текущей версии платформы это <see cref="CreateMultipleTasks"/>.
        /// </summary>
        Default = CreateMultipleTasks,
    }
}
