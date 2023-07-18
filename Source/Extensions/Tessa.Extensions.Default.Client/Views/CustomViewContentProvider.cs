using System;
using System.Collections.Generic;
using Tessa.UI;
using Tessa.UI.Views;
using Tessa.UI.Views.Content;
using Tessa.UI.Views.Workplaces.Tree;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// The custom view content provider.
    /// </summary>
    internal sealed class CustomViewContentProvider : ViewModel<EmptyModel>, IContentProvider
    {
        #region Constructors and Destructors

        /// <summary>
        /// Initializes a new instance of the <see cref="CustomViewContentProvider"/> class.
        /// </summary>
        public CustomViewContentProvider(IFolderTreeItem folder) =>
            this.Content = new CustomFolderView(folder ?? throw new ArgumentNullException(nameof(folder)));

        #endregion

        #region Public Properties

        /// <summary>
        /// Список компонентов, предоставляемых поставщиком.
        /// </summary>
        public IDictionary<Guid, IWorkplaceViewComponent> Components { get; } = new Dictionary<Guid, IWorkplaceViewComponent>();

        /// <summary>
        /// Содержимое объекта.
        /// </summary>
        public object Content { get; }

        /// <summary>
        /// Контекст представления.
        /// </summary>
        public IViewContext ViewContext => null;

        #endregion

        #region Public Methods and Operators

        /// <summary>
        /// Обновляет представление.
        /// </summary>
        public void Refresh() => this.OnPropertyChanged(nameof(Content));

        #endregion
    }
}