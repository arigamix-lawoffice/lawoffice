namespace Tessa.Extensions.Default.Shared.Workflow
{
    /// <summary>
    /// Перечисление режимов использования стандартного поля с исполнителями.
    /// </summary>
    public enum PerformerUsageMode
    {
        /// <summary>
        /// Стандартное поле отсутствует.
        /// </summary>
        None,

        /// <summary>
        /// Отображается поле для задания одного исполнителя.
        /// </summary>
        Single,

        /// <summary>
        /// Отображается поле для задания нескольких исполнителей.
        /// </summary>
        Multiple,
    }
}