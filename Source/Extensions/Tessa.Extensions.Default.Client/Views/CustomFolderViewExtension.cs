using System;
using Tessa.UI.Views;
using Tessa.UI.Views.Workplaces.Tree;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <remarks>
    /// У расширения есть конфигуратор <see cref="CustomFolderViewExtensionConfigurator"/>
    /// </remarks>
    public sealed class CustomFolderViewExtension : ITreeItemExtension
    {
        /// <summary>
        /// Вызывается при клонировании модели <paramref name="source"/> в контексте <paramref name="context"/>
        /// </summary>
        /// <param name="source">
        /// Исходная модель
        /// </param>
        /// <param name="cloned">
        /// Клонированная модель
        /// </param>
        /// <param name="context">
        /// Контекст клонирования
        /// </param>
        public void Clone(ITreeItem source, ITreeItem cloned, ICloneableContext context)
        {
            var folder = (IFolderTreeItem) cloned;
            folder.ContentProviderFactory = (item, viewModel, strategy) => new CustomViewContentProvider(folder);
        }

        /// <summary>
        /// Вызывается после создания модели <paramref name="model"/>
        /// </summary>
        /// <param name="model">
        /// Инициализируемая модель
        /// </param>
        public void Initialize(ITreeItem model)
        {
            if (!(model is IFolderTreeItem folder))
            {
                throw new InvalidOperationException("Use extension for folders only.");
            }

            folder.SwitchExpandOnSingleClick = false;
            folder.ContentProviderFactory = (item, viewModel, strategy) => new CustomViewContentProvider(folder);
        }

        /// <summary>
        /// Вызывается после создания модели <paramref name="model"/> перед отображением в UI
        /// </summary>
        /// <param name="model">
        /// Модель
        /// </param>
        public void Initialized(ITreeItem model)
        {
        }
    }
}
