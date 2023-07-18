using System.Threading.Tasks;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrPermissions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.UI;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.Cards
{
    public class AcquaintanceClientStoreExtension : CardStoreExtension
    {
        #region Base Overrides

        public override Task AfterRequest(ICardStoreExtensionContext context)
        {
            if (!context.RequestIsSuccessful)
            {
                return Task.CompletedTask;
            }

            var token = KrToken.TryGet(context.Response.Info);
            IUIContext uicontext = UIContext.Current;

            ICardEditorModel editor;
            ICardModel model;
            if (token != null &&
                (editor = uicontext.CardEditor) != null
                && (model = editor.CardModel) != null
                && context.Request.Info.ContainsKey(KrPermissionsHelper.SaveWithPermissionsCalcFlag)
                && context.Response.CardVersion != model.Card.Version)
            {
                token.Set(editor.Info.GetStorage());
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}