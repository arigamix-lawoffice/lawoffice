using System;

namespace Tessa.Extensions.Default.Shared.Workflow.KrProcess
{
    [Flags]
    public enum KrComponents
    {
        /// <summary>
        /// Стандартное решение не используется (для типа)
        /// </summary>
        None = 0x0,
        /// <summary>
        /// Стандартное решение используется
        /// </summary>
        Base = 0x1,
        /// <summary>
        /// Используются типы документов
        /// </summary>
        DocTypes = 0x2,
        /// <summary>
        /// Используется согласование
        /// </summary>
        Routes = 0x4,
        /// <summary>
        /// Используется регистрация
        /// </summary>
        Registration = 0x8,
        /// <summary>
        /// Используются резолюции
        /// </summary>
        Resolutions = 0x10,
        /// <summary>
        /// Используется система форумов
        /// </summary>
        UseForum = 0x20,
    }
}
