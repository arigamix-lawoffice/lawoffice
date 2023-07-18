namespace Tessa.Extensions.Default.Shared.Workflow.KrCompilers
{
    /// <summary>
    /// Описывает объект предоставляющий информацию о дополнительном методе.
    /// </summary>
    public interface IExtraSource
    {
        /// <summary>
        /// Отображаемое имя метода.
        /// </summary>
        string DisplayName { get; }

        /// <summary>
        /// Имя метода.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Тип возвращаемого значения.
        /// </summary>
        string ReturnType { get; }

        /// <summary>
        /// Тип параметра.
        /// </summary>
        string ParameterType { get; }

        /// <summary>
        /// Имя параметра.
        /// </summary>
        string ParameterName { get; }

        /// <summary>
        /// Исходный код.
        /// </summary>
        string Source { get; }

        /// <summary>
        /// Преобразует объект к изменяемому типу.
        /// </summary>
        /// <returns>Текущий объект преобразованный к изменяемому типу.</returns>
        IExtraSource ToMutable();

        /// <summary>
        /// Преобразует объект к неизменяемому типу.
        /// </summary>
        /// <returns>Текущий объект преобразованный к неизменяемому типу.</returns>
        IExtraSource ToReadonly();
    }
}