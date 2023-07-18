using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Client.Workflow.Wf;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Default.Shared.Workflow.Wf;
using Tessa.Localization;
using Tessa.Platform;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    /// <summary>
    /// Плитки для бизнес-процессов Workflow.
    /// </summary>
    public sealed class WfTileExtension :
        TileExtension
    {
        #region Private Methods

        private static async void CreateWfResolutionActionAsync(object parameters)
        {
            IUIContext context = UIContext.Current;
            ICardEditorModel editor = context.CardEditor;

            ICardModel model;
            if (editor != null && (model = editor.CardModel) != null)
            {
                bool cardIsNew = model.Card.StoreMode == CardStoreMode.Insert;

                if (cardIsNew)
                {
                    bool saved = await editor.SaveCardAsync(context);
                    if (!saved)
                    {
                        return;
                    }
                }

                await editor.SaveCardAsync(
                    context,
                    new Dictionary<string, object>()
                        .SetStartingProcessName(WfHelper.ResolutionProcessName));
            }
        }


        private static void EnableOnCardIsNotTaskCard(object sender, TileEvaluationEventArgs e)
        {
            ICardEditorModel editor = e.CurrentTile.Context.CardEditor;

            e.SetIsEnabledWithCollapsing(
                e.CurrentTile,
                editor?.CardModel?.CardType.ID != DefaultCardTypes.WfTaskCardTypeID);
        }


        private static void EnableOnCardIsNotVirtualTaskCard(object sender, TileEvaluationEventArgs e)
        {
            ICardModel model = e.CurrentTile.Context.CardEditor?.CardModel;

            e.SetIsEnabledWithCollapsing(
                e.CurrentTile,
                model == null
                || model.CardType.ID != DefaultCardTypes.WfTaskCardTypeID
                || (!model.Card.TryGetInfo()?.ContainsKey(WfHelper.VirtualMainCardIDKey) ?? true));
        }

        #endregion

        #region Base Overrides

        public override Task InitializingGlobal(ITileGlobalExtensionContext context)
        {
            ITilePanel panel = context.Workspace.LeftPanel;
            TileCollection leftPanelTiles = context.Workspace.LeftPanel.Tiles;

            leftPanelTiles.Add(
                new Tile(
                    DefaultTileNames.WfCreateResolution,
                    LocalizationManager.GetString("WfTiles_CreateResolution"),
                    context.Icons.Get("Thin91"),
                    panel,
                    new DelegateCommand(CreateWfResolutionActionAsync),
                    TileGroups.Cards,
                    order: 50,
                    toolTip: TileHelper.GetToolTip("$WfTiles_CreateResolution_ToolTip", WfTileKeys.CreateResolution),
                    evaluating: TileHelper.EnableWhenVisibleInCardHandler));

            return Task.CompletedTask;
        }


        public override Task InitializingLocal(ITileLocalExtensionContext context)
        {
            TileCollection leftPanelTiles = context.Workspace.LeftPanel.Tiles;

            ITile createResolution = leftPanelTiles.TryGet(DefaultTileNames.WfCreateResolution);
            if (createResolution != null)
            {
                createResolution.Context.AddInputBinding(createResolution, WfTileKeys.CreateResolution);
            }

            ITile notificationSubscriptions = leftPanelTiles.TryGet(TileNames.NotificationSubscriptions);
            if (notificationSubscriptions != null)
            {
                notificationSubscriptions.Evaluating += EnableOnCardIsNotTaskCard;
            }

            ITile createFileTemplate = leftPanelTiles.TryGet(TileNames.CreateFileFromTemplate);
            if (createFileTemplate != null)
            {
                createFileTemplate.Evaluating += EnableOnCardIsNotTaskCard;
            }

            ITile copyLink = leftPanelTiles.TryGet(TileNames.CardOthers)?.Tiles.TryGet(TileNames.CopyCardLink);
            if (copyLink != null)
            {
                copyLink.Evaluating += EnableOnCardIsNotVirtualTaskCard;
            }

            return Task.CompletedTask;
        }

        #endregion
    }
}
