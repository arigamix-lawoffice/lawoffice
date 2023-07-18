using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;
using Tessa.UI.Cards.Tasks;

namespace Tessa.Extensions.Default.Client.Workflow.Wf
{
    public class WfTaskSatelliteUIExtension :
        CardUIExtension
    {
        #region Private Methods

        protected void OnCardModelInitialized(object sender, CardModelInitializingEventArgs e)
        {
            StringDictionaryStorage<CardSection> sections = e.CardModel.Card.TryGetSections();
            if (sections == null)
            {
                return;
            }

            string docTypeTitle = sections.TryGetValue("WfTaskCardsVirtual", out var section)
                ? section.RawFields.TryGet<string>("DocTypeTitle")
                : null;

            if (!string.IsNullOrWhiteSpace(docTypeTitle))
            {
                e.WorkspaceInfo = docTypeTitle;
            }
        }

        #endregion

        #region Base Overrides

        public override async Task Initialized(ICardUIExtensionContext context)
        {
            ICardEditorModel editor;

            if (context.Model.InSpecialMode()
                && context.Model.Flags.HasNot(CardModelFlags.EditTemplate)
                && context.Model.Flags.HasNot(CardModelFlags.ViewExported)
                || (editor = context.UIContext.CardEditor) == null)
            {
                return;
            }

            editor.CardModelInitialized -= OnCardModelInitialized;

            if (context.Model.CardType.ID == DefaultCardTypes.WfTaskCardTypeID)
            {
                editor.CardModelInitialized += OnCardModelInitialized;

                TaskHistoryViewModel taskHistory = context.Model.TryGetTaskHistory();
                if (taskHistory != null)
                {
                    taskHistory.HideOpenViewCommand = true;
                }
            }
        }

        #endregion
    }
}
