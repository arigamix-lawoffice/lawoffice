namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Перечисление состояний <see cref="TestAction"/>.
    /// </summary>
    public enum TestActionState
    {
        /// <summary>
        /// Не выполнялось.
        /// </summary>
        NotExecuted,

        /// <summary>
        /// Выполняется.
        /// </summary>
        InProgress,

        /// <summary>
        /// Выполнено.
        /// </summary>
        Completed,

        /// <summary>
        /// При выполнении произошла ошибка.
        /// </summary>
        Error,
    }
}
