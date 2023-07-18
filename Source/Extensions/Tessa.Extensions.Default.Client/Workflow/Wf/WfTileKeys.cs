using System.Windows.Input;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    /// <summary>
    /// Горячие клавиши для плиток, относящихся к бизнес-процессам Workflow.
    /// </summary>
    public static class WfTileKeys
    {
        #region Static Fields

        /// <summary>
        /// Создание резолюции.
        /// </summary>
        public static readonly KeyGesture CreateResolution =
            new KeyGesture(Key.R, ModifierKeys.Alt, "Alt+R");

        #endregion
    }
}
