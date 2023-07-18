using System.Windows.Input;

namespace Tessa.Extensions.Default.Client.Tiles
{
    public static class KrTileKeys
    {
        /// <summary>
        /// Режим редактирования карточки
        /// </summary>
        public static readonly KeyGesture Edit =
            new KeyGesture(Key.E, ModifierKeys.Alt, "Alt+E");

        /// <summary>
        /// Режим просмотра скрытых этапов
        /// </summary>
        public static readonly KeyGesture ShowHiddenStages =
            new KeyGesture(Key.H, ModifierKeys.Control | ModifierKeys.Alt, "Ctrl+Alt+H");

        /// <summary>
        /// Проверка новых заданий с сервера и вывод всплывающих уведомлений
        /// </summary>
        public static readonly KeyGesture CheckTasks =
            new KeyGesture(Key.F9, ModifierKeys.None, "F9");

        /// <summary>
        /// Выполнить проверку метода
        /// </summary>
        public static readonly KeyGesture StageSourceBuild =
            new KeyGesture(Key.B, ModifierKeys.Control | ModifierKeys.Shift, "Ctrl+Shift+B");

        /// <summary>
        /// Режим просмотра пропущенных этапов
        /// </summary>
        public static readonly KeyGesture ShowSkipStages =
            new KeyGesture(Key.H, ModifierKeys.Control | ModifierKeys.Alt, "Ctrl+Alt+H");
    }
}
