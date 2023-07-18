using System.Windows;

namespace Tessa.Extensions.Default.Client.Views.StageSelector
{
    /// <summary>
    /// Interaction logic for StageSelectorView.xaml
    /// </summary>
    public partial class StageSelectorView
    {
        public StageSelectorView()
        {
            this.InitializeComponent();
        }

        private void ButtonOk_OnClick(object sender, RoutedEventArgs e)
        {
            var window = Window.GetWindow(this);
            if (window != null)
            {
                window.DialogResult = true;
                window.Close();
            }
        }
    }
}
