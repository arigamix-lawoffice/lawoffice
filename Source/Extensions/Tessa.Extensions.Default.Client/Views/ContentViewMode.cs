// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ContentViewMode.cs" company="Syntellect">
//   Tessa Project
// </copyright>
// <summary>
//   Режим отображения данных модели представления <see cref="IWorkplaceViewComponent" />
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tessa.Extensions.Default.Client.Views
{
    using Tessa.UI.Views;

    /// <summary>
    /// Режим отображения данных модели представления <see cref="IWorkplaceViewComponent"/>
    /// </summary>
    public enum ContentViewMode
    {
        /// <summary>
        /// Отображение в виде таблицы
        /// </summary>
        TableView, 

        /// <summary>
        /// Отображение в виде строк
        /// </summary>
        RecordView
    }
}