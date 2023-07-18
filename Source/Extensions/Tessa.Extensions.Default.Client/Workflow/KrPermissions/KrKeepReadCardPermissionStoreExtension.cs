using System;
using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Workflow.KrPermissions
{
    public sealed class KrKeepReadCardPermissionStoreExtension : CardStoreExtension
    {
        public override Task AfterRequest(ICardStoreExtensionContext context)
        {
            IUIContext uicontext = UIContext.Current;

            ICardEditorModel editor;
            ICardModel model;
            if ((editor = uicontext.CardEditor) != null
                && (model = editor.CardModel) != null
                && (editor.CurrentOperationType == CardEditorOperationType.SaveAndRefresh
                || editor.CurrentOperationType == CardEditorOperationType.Open))
            {
                KrToken token = KrToken.TryGet(context.Response.Info);
                if (token != null && (token.CardID == model.Card.ID || token.CardID == Guid.Empty))
                {
                    token.Set(model.Card.Info);
                }
            }

            return Task.CompletedTask;
        }
    }
}
