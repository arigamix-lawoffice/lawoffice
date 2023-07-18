using System;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Files
{
    public sealed class ClientFileTemplatePermissionsGetFileContentExtension :
        CardGetFileContentExtension
    {
        #region Base Overrides

        public override Task BeforeRequest(ICardGetFileContentExtensionContext context)
        {
            ICardEditorModel editor = UIContext.Current.CardEditor;
            ICardModel model;
            KrToken token;

            if (editor is not null
                && (model = editor.CardModel) is not null
                && (token = KrToken.TryGet(model.Card.Info)) is not null
                && context.Request.VersionRowID == CardHelper.ReplacePlaceholdersVersionRowID
                && model.Card.ID == context.Request?.Info.TryGet<Guid?>(CardHelper.PlaceholderCurrentCardIDInfo))
            {
                token.Set(context.Request.Info);
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
