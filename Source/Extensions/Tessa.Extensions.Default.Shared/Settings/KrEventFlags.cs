using System;

namespace Tessa.Extensions.Default.Shared.Settings
{
    /// <summary>
    /// Флаги, указывающие события, которые происходят с карточкой в типовом процессе.
    /// </summary>
    [Flags]
    public enum KrEventFlags
    {
        /// <summary>
        /// События не указаны.
        /// </summary>
        None = 0,

        /// <summary>
        /// Регистрация документа.
        /// </summary>
        Register = 1,

        /// <summary>
        /// Дерегистрация документа.
        /// </summary>
        Deregister = 2,

        /// <summary>
        /// Запуск согласования.
        /// </summary>
        StartApproval = 4,

        /// <summary>
        /// Возврат документа на доработку.
        /// </summary>
        RebuildApproval = 8,

        /// <summary>
        /// Отзыв согласования.
        /// </summary>
        RevokeApproval = 16,

        /// <summary>
        /// Отмена согласования.
        /// </summary>
        CancelApproval = 32,

        /// <summary>
        /// Все поддерживаемые события, которые происходят с карточкой в типовом процессе.
        /// </summary>
        All = Register
            | Deregister
            | StartApproval
            | RebuildApproval
            | RevokeApproval
            | CancelApproval,
    }
}
