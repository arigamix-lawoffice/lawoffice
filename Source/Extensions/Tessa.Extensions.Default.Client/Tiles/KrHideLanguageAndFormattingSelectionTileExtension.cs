using System.Threading.Tasks;
using Tessa.Cards.Caching;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.KrProcess;
using Tessa.Platform;
using Tessa.Platform.Runtime;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    public sealed class KrHideLanguageAndFormattingSelectionTileExtension : TileExtension
    {
        #region Private Fields

        private readonly ISession session;
        private readonly ICardCache cardCache;

        #endregion

        #region Constructors

        public KrHideLanguageAndFormattingSelectionTileExtension(ISession session, ICardCache cardCache)
        {
            this.session = session;
            this.cardCache = cardCache;
        }

        #endregion

        #region Base Overrides

        public override async Task InitializingGlobal(ITileGlobalExtensionContext context)
        {
            if (this.session.User.IsAdministrator())
            {
                return;
            }

            var mySettingsTile =
                context.Workspace.RightPanel.Tiles.TryGet(TileNames.MySettings);
            if (mySettingsTile is null)
            {
                return;
            }

            var cardValue = await this.cardCache.Cards.GetAsync(DefaultCardTypes.KrSettingsTypeName, context.CancellationToken);
            if (!cardValue.IsSuccess)
            {
                return;
            }

            var card = cardValue.GetValue();
            var hideLanguageSelection = card.Entries.Get<bool>(KrConstants.KrSettings.Name,
                KrConstants.KrSettings.HideLanguageSelection);
            var hideFormatSelection = card.Entries.Get<bool>(KrConstants.KrSettings.Name,
                KrConstants.KrSettings.HideFormattingSelection);

            if (hideLanguageSelection)
            {
                var selectLanguageTile = mySettingsTile.Tiles.TryGet(TileNames.SelectLanguage);
                if (selectLanguageTile is not null)
                {
                    selectLanguageTile.ResetEvaluating();
                    selectLanguageTile.Evaluating += TileHelper.DisableTileWithCollapsingHandler;
                }
            }

            if (hideFormatSelection)
            {
                var selectFormatTile = mySettingsTile.Tiles.TryGet(TileNames.SelectFormat);
                if (selectFormatTile is not null)
                {
                    selectFormatTile.ResetEvaluating();
                    selectFormatTile.Evaluating += TileHelper.DisableTileWithCollapsingHandler;
                }
            }
        }

        #endregion
    }
}
