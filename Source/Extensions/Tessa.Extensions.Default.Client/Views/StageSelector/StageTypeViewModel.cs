using System;
using Tessa.UI;

namespace Tessa.Extensions.Default.Client.Views.StageSelector
{
    /// <summary>
    /// Модель представления типа этапа.
    /// </summary>
    public class StageTypeViewModel : ViewModel<EmptyModel>
    {
        #region Constructors

        /// <summary>
        /// Инициализирует новый экземпляр <see cref="StageTypeViewModel"/>.
        /// </summary>
        /// <param name="id">Идентификатор типа этапа.</param>
        /// <param name="caption">Название типа этапа.</param>
        /// <param name="defaultStageName">Стандартное название для нового этапа.</param>
        public StageTypeViewModel(Guid id, string caption, string defaultStageName)
        {
            this.ID = id;
            this.Caption = caption;
            this.DefaultStageName = defaultStageName;
        }

        #endregion

        #region Fields
        
        /// <summary>
        /// Возвращает идентификатор типа этапа.
        /// </summary>
        public Guid ID { get; }

        /// <summary>
        /// Возвращает название типа этапа.
        /// </summary>
        public string Caption { get; }

        /// <summary>
        /// Возвращает стандартное название для нового этапа.
        /// </summary>
        public string DefaultStageName { get; }

        #endregion
    }
}
