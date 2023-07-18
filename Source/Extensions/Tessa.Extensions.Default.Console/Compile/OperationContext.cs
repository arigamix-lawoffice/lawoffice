#nullable enable

using System;
using System.Collections.Generic;

namespace Tessa.Extensions.Default.Console.Compile
{
    /// <summary>
    /// Контекст команды.
    /// </summary>
    public class OperationContext
    {
        #region Properties

        /// <summary>
        /// Категории компилируемых объектов или значение <see langword="null"/>, если выполняется компиляция объектов всех известных категорий.
        /// </summary>
        public IReadOnlySet<string>? Categories { get; set; }

        /// <summary>
        /// Идентификаторы компилируемых объектов или значение <see langword="null"/>, если выполняется компиляция всех объектов в заданных категориях.
        /// </summary>
        public IReadOnlySet<Guid>? Identifiers { get; set; }

        /// <summary>
        /// Вывести категории компилируемых объектов системы.
        /// </summary>
        public bool ShowCategories { get; set; }

        #endregion
    }
}
