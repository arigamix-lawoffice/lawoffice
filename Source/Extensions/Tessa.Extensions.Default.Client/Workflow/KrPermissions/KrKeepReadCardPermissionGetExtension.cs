using System;
using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Workflow.KrPermissions
{
    public sealed class KrKeepReadCardPermissionGetExtension : CardGetExtension
    {
        public override Task BeforeRequest(ICardGetExtensionContext context)
        {
            ICardEditorModel editor;
            ICardModel model;
            if ((editor = UIContext.Current.CardEditor) != null
                && (model = editor.CardModel) != null
                && (editor.CurrentOperationType == CardEditorOperationType.SaveAndRefresh
                || editor.CurrentOperationType == CardEditorOperationType.Open))
            {
                if (KrToken.Contains(context.Request.Info))
                {
                    return Task.CompletedTask;
                }

                KrToken token = KrToken.TryGet(model.Card.Info);
                if (token != null && (token.CardID == context.Request.CardID || token.CardID == Guid.Empty))
                {
                    token.Set(context.Request.Info);
                }
            }

            return Task.CompletedTask;
        }
    }
}
