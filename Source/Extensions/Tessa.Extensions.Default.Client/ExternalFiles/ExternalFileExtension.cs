using System.Linq;
using System.Threading.Tasks;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Files;

namespace Tessa.Extensions.Default.Client.ExternalFiles
{
    /// <summary>
    /// Расширение, инициализирующее контекстное меню для файлов значениями по умолчанию.
    /// </summary>
    public sealed class ExternalFileExtension :
        FileExtension
    {
        #region Base Overrides

        public override async Task OpeningMenu(IFileExtensionContext context)
        {
            // Проверяем тип карточки
            ICardEditorModel editor = UIContext.Current.CardEditor;
            ICardModel model;
            if (editor == null
                || (model = editor.CardModel) == null
                || model.CardType.Name != "Car")
            {
                return;
            }

            // Отключаем пункт "Копировать ссылку"
            var copyLinkAction = context.Actions.FirstOrDefault(p => p.Name == FileMenuActionNames.CopyLink);
            if (context.File is ExternalFile &&
                copyLinkAction != null)
            {
                copyLinkAction.IsEnabled = false;
                copyLinkAction.IsCollapsed = true;
            }
        }

        #endregion
    }
}
