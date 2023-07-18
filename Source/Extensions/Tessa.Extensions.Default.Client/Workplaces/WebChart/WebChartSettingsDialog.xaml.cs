// --------------------------------------------------------------------------------------------------------------------
// <copyright file="WebChartSettingsDialog.xaml.cs" company="Syntellect">
//   Tessa Project
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Tessa.Extensions.Default.Client.Workplaces.WebChart
{
    #region

    using System.Windows;
    using Tessa.UI.Windows;

    #endregion

    /// <summary>
    ///     Interaction logic for ChartDesignerWindow.xaml
    /// </summary>
    public partial class WebChartSettingsDialog : TessaWindow
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="WebChartSettingsDialog" /> class.
        /// </summary>
        public WebChartSettingsDialog()
        {
            this.InitializeComponent();
        }

        private void OkButtonClick(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}