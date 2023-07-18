using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;
using Tessa.UI.Cards;

namespace Tessa.Extensions.Default.Client.UI.KrProcess
{
    public sealed class KrDocumentWorkspaceInfoUIExtension :
        CardUIExtension
    {
        #region Constructors

        public KrDocumentWorkspaceInfoUIExtension(IKrTypesCache typesCache)
        {
            this.typesCache = typesCache;
        }

        #endregion

        #region Fields

        private readonly IKrTypesCache typesCache;

        #endregion

        #region Private Methods

        private static void OnCardModelInitialized(object sender, CardModelInitializingEventArgs e)
        {
            StringDictionaryStorage<CardSection> sections = e.CardModel.Card.TryGetSections();
            if (sections == null
                || !sections.TryGetValue("DocumentCommonInfo", out var section))
            {
                return;
            }

            string docTypeTitle = section.RawFields.TryGet<string>("DocTypeTitle");

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

            if ((await KrComponentsHelper.GetKrComponentsAsync(context.Model.CardType.ID, this.typesCache, context.CancellationToken))
                .Has(KrComponents.DocTypes))
            {
                editor.CardModelInitialized += OnCardModelInitialized;
            }
        }

        #endregion
    }
}
