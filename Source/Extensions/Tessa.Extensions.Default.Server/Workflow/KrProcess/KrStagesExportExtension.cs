using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Extensions;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform.Storage;

namespace Tessa.Extensions.Default.Server.Workflow.KrProcess
{
    public sealed class KrStagesExportExtension :
        CardGetExtension
    {
        #region Base Overrides

        public override Task AfterRequest(ICardGetExtensionContext context)
        {
            Card card;
            StringDictionaryStorage<CardSection> sections;
            ListStorage<CardRow> rows;

            if (!context.ValidationResult.IsSuccessful()
                || (card = context.Response.TryGetCard()) is null
                || (sections = card.TryGetSections()) is null
                || !sections.TryGetValue(nameof(KrConstants.KrStages), out CardSection section)
                || (rows = section.TryGetRows()) is null
                || rows.Count == 0)
            {
                return Task.CompletedTask;
            }

            foreach (CardRow row in rows)
            {
                row[KrConstants.KrStages.DisplaySettings] = null;
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}