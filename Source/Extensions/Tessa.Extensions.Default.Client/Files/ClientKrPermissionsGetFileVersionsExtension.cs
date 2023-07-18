using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Files
{
    public sealed class ClientKrPermissionsGetFileVersionsExtension :
        CardGetFileVersionsExtension
    {
        #region Base Overrides

        public override Task BeforeRequest(ICardGetFileVersionsExtensionContext context)
        {
            ICardEditorModel editor = UIContext.Current.CardEditor;
            ICardModel model;
            KrToken token;

            if (editor != null
                && (model = editor.CardModel) != null
                && (token = KrToken.TryGet(model.Card.Info)) != null)
            {
                token.Set(context.Request.Info);
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}