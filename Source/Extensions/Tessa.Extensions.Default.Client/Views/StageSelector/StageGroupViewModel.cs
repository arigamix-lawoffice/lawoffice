using System;
using Tessa.UI;

namespace Tessa.Extensions.Default.Client.Views.StageSelector
{
    /// <summary>
    /// Модель представления группы этапов.
    /// </summary>
    public class StageGroupViewModel : ViewModel<EmptyModel>
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпрял класса <see cref="StageGroupViewModel"/>.
        /// </summary>
        /// <param name="id">Идентификатор группы этапов.</param>
        /// <param name="name">Название группы этапов.</param>
        /// <param name="order">Порядковый номер группы этапов.</param>
        public StageGroupViewModel(Guid id, string name, int order)
        {
            this.ID = id;
            this.Name = name;
            this.Order = order;
        }

        #endregion

        #region Fields

        /// <summary>
        /// Возвращает идентификатор группы этапов.
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// Возвращает название группы этапов.
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// Возвращает порядковый номер группы этапов.
        /// </summary>
        public int Order { get; }

        #endregion
    }
}
