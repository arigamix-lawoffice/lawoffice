using System;
using System.Collections.Generic;
using Tessa.Extensions.Default.Shared.Workplaces;
using Tessa.UI.Views;
using Tessa.UI.Views.Extensions;
using Tessa.Views;
using Unity;

namespace Tessa.Extensions.Default.Client.Workplaces.WebChart
{
    /// <summary>
    ///     Расширение рабочего места руководителя
    /// </summary>
    /// <remarks>
    /// У расширения есть конфигуратор <see cref="WebChartWorkplaceExtensionConfigurator"/>.
    /// </remarks>
    public sealed class WebChartWorkplaceExtension :
        IWorkplaceViewComponentExtension,
        IWorkplaceExtensionSettingsRestore
    {
        #region Fields

        private readonly IUnityContainer container;

        private WebChartWorkplaceSettings settings;

        #endregion

        #region Constructors

        public WebChartWorkplaceExtension(IUnityContainer container) =>
            this.container = container ?? throw new ArgumentNullException(nameof(container));

        #endregion

        #region IWorkplaceViewComponentExtension Members

        /// <inheritdoc />
        public void Clone(IWorkplaceViewComponent source, IWorkplaceViewComponent cloned, ICloneableContext context)
        {
        }

        /// <inheritdoc />
        public void Initialize(IWorkplaceViewComponent model)
        {
        }

        /// <inheritdoc />
        public void Initialized(IWorkplaceViewComponent model)
        {
        }

        #endregion

        #region IWorkplaceExtensionSettingsRestore Members

        /// <summary>
        /// Осуществляет восстановление настроек расширения
        /// </summary>
        /// <param name="metadata">
        /// Сериализованные метаданные настроек
        /// </param>
        public void Restore(Dictionary<string, object> metadata) =>
            this.settings = ExtensionSettingsSerializationHelper.DeserializeDictionary<WebChartWorkplaceSettings>(metadata) as WebChartWorkplaceSettings;

        #endregion
    }
}