using System;
using Tessa.Platform;
using Tessa.PreviewHandlers;
using Tessa.UI.Files;
using Tessa.UI.Views;
using Unity;

namespace Tessa.Extensions.Default.Client.Views
{
    /// <summary>
    /// Расширение позволяющее отображать содержимое файла по пути к нему
    /// </summary>
    /// <remarks>
    /// У расширения есть конфигуратор <see cref="PreviewExtensionConfigurator"/>
    /// </remarks>
    public sealed class PreviewExtension :
        IWorkplaceViewComponentExtension,
        IPreviewPageExtractorProvider
    {
        #region Constructors

        public PreviewExtension(
            IUnityContainer unityContainer,
            [OptionalDependency] IPreviewHandlersSelectorPool previewHandlersPool = null,
            [OptionalDependency] IFilePreviewInfoCache previewInfoCache = null)
        {
            this.unityContainer = unityContainer ?? throw new ArgumentNullException(nameof(unityContainer));
            this.previewHandlersPool = previewHandlersPool;
            this.previewInfoCache = previewInfoCache;
        }

        #endregion

        #region Fields

        private readonly IUnityContainer unityContainer;

        private readonly IPreviewHandlersSelectorPool previewHandlersPool;

        private readonly IFilePreviewInfoCache previewInfoCache;

        #endregion

        #region IWorkplaceViewComponentExtension Members

        /// <inheritdoc />
        public void Clone(IWorkplaceViewComponent source, IWorkplaceViewComponent cloned, ICloneableContext context)
        {
        }

        /// <inheritdoc />
        public void Initialize(IWorkplaceViewComponent model)
        {
            model.ContentFactories.Clear();
            model.ContentFactories["Preview"] = c => new PreviewView(c, this.previewHandlersPool, this.previewInfoCache, this);
        }

        /// <inheritdoc />
        public void Initialized(IWorkplaceViewComponent model)
        {
        }

        #endregion

        #region IPreviewPageExtractorProvider Members

        /// <inheritdoc />
        public IPreviewPageExtractor TryGetPageExtractor(string filePath) =>
            this.unityContainer.TryResolve<IPreviewPageExtractor>();

        /// <inheritdoc />
        public IPreviewPageOptions PageOptions { get; } = new PreviewPageOptions();

        #endregion
    }
}