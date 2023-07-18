using System;
using System.Collections.Generic;
using Tessa.Properties.Resharper;

namespace Tessa.Extensions.Default.Client.Workplaces.WebChart.Palette
{
    /// <summary>
    /// Описание интерфейса реестра объектов
    /// </summary>
    /// <typeparam name="TItem">
    /// Тип элемента реестра
    /// </typeparam>
    public interface IRegistry<out TItem>
        where TItem : class, ITypeIdentifier
    {
        /// <summary>
        ///     Gets Список объектов реестра
        /// </summary>
        [NotNull]
        IEnumerable<TItem> Items { get; }

        /// <summary>
        /// Осуществляет поиск объекта по его идентификатору.
        ///     В случае если объект с указанным идентификатором не найден
        ///     в реестре возвращает <c>null</c>
        /// </summary>
        /// <param name="typeId">
        /// Идентификатор объекта
        /// </param>
        /// <returns>
        /// Найденный объект или <c>null</c>
        /// </returns>
        [CanBeNull]
        TItem TryGetItem(Guid typeId);
    }
}