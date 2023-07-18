using System;

namespace Tessa.Test.Default.Shared
{
    /// <summary>
    /// Перечисление параметров действия <see cref="TestAction"/>.
    /// </summary>
    [Flags]
    public enum TestActionOptions
    {
        /// <summary>
        /// Параметры не заданы.
        /// </summary>
        None,

        /// <summary>
        /// Действие должно быть выполнено только один раз.
        /// </summary>
        RunOnce,

        /// <summary>
        /// Параметры по умолчанию.
        /// </summary>
        Default = RunOnce,
    }
}
