using System;
using System.Collections.Generic;
using System.Linq;
using Tessa.Properties.Resharper;

namespace Tessa.Extensions.Default.Client.Workplaces.WebChart.Palette
{
    /// <summary>
    /// Обобщенный класс реестра для объектов поддерживающих идентификацию с
    ///     помощью реализации интерфейса <see cref="ITypeIdentifier"/>.
    ///     Заполнение реестра осуществляется через передачу списка объектов
    ///     содержащийхся в реестре через конструктор
    /// </summary>
    /// <typeparam name="TItem">
    /// Тип элемента реестра
    /// </typeparam>
    public class Registry<TItem> : IRegistry<TItem>
        where TItem : class, ITypeIdentifier
    {
        #region Fields

        [NotNull]
        private readonly Dictionary<Guid, TItem> items;

        #endregion

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="Registry{TItem}"/> class.
        /// Инициализирует новый экземпляр класса <see cref="T:System.Object"/>.
        /// </summary>
        /// <param name="items">
        /// Список элементов реестра
        /// </param>
        public Registry([NotNull] TItem[] items)
        {
            if (items is null)
            {
                throw new ArgumentNullException(nameof(items));
            }

            this.items = items.ToDictionary(x => x.TypeId, x => x);
        }

        #endregion

        #region Properties

        /// <summary>
        ///     Gets Список объектов реестра
        /// </summary>
        public IEnumerable<TItem> Items => this.items.Values;

        #endregion

        #region Methods

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
        public TItem TryGetItem(Guid typeId) => this.items.TryGetValue(typeId, out var result) ? result : null;

        #endregion
    }
}