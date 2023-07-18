using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Files
{
    public sealed class ClientKrPermissionsGetFileContentExtension :
        CardGetFileContentExtension
    {
        #region Base Overrides

        public override Task BeforeRequest(ICardGetFileContentExtensionContext context)
        {
            ICardEditorModel editor = UIContext.Current.CardEditor;
            ICardModel model;
            KrToken token;

            if (editor != null
                && (model = editor.CardModel) != null
                && (token = KrToken.TryGet(model.Card.Info)) != null
                && context.Request.VersionRowID != CardHelper.ReplacePlaceholdersVersionRowID
                && model.Card.ID == context.Request.CardID)
            {
                token.Set(context.Request.Info);
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}