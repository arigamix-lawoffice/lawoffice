// --------------------------------------------------------------------------------------------------------------------
// <copyright file="CardCreationKind.cs" company="Syntellect">
//   Tessa Project
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Tessa.Extensions.Default.Shared.Views
{
    /// <summary>
    /// Режим создания карточки.
    /// </summary>
    public enum CardCreationKind
    {
        /// <summary>
        ///     По типу полученному из текущей строки
        /// </summary>
        ByTypeFromSelection,

        /// <summary>
        ///     По псевдониму типа
        /// </summary>
        ByTypeAlias,

        /// <summary>
        ///     По идентификатору типа
        /// </summary>
        ByDocTypeIdentifier
    }
}