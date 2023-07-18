using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Workflow.KrPermissions
{
    public sealed class KrDontSkipEditModeGetExtension : CardGetExtension
    {
        public override Task BeforeRequest(ICardGetExtensionContext context)
        {
            IUIContext uicontext = UIContext.Current;

            ICardEditorModel editor;
            ICardModel model;
            if ((editor = uicontext.CardEditor) != null
                && (model = editor.CardModel) != null
                //Флаг сохраняем только при открытии при сохранении карточки, рефреш карточки скидывает режим редактирования
                && (editor.CurrentOperationType == CardEditorOperationType.SaveAndRefresh))
            {
                Card card = model.Card;
                if (card.Info.TryGet<bool>(KrPermissionsHelper.PermissionsCalculatedMark))
                {
                    context.Request.Info.Add(KrPermissionsHelper.CalculatePermissionsMark, true);
                    //Тащим и этот признак на сервер, чтобы не отображать информационное сообщение
                    //о выдаче прав после сохранения карточки
                    context.Request.Info.Add(KrPermissionsHelper.PermissionsCalculatedMark, true);
                }
            }

            return Task.CompletedTask;
        }
    }
}
