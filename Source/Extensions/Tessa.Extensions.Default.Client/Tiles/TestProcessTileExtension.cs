using System.Collections.Generic;
using System.Threading.Tasks;
using Tessa.Cards;
using Tessa.Cards.Workflow;
using Tessa.Extensions.Default.Shared.Workflow.TestProcess;
using Tessa.Platform.Collections;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Tiles;
using Tessa.UI.Tiles.Extensions;

namespace Tessa.Extensions.Default.Client.Tiles
{
    /// <summary>
    /// Расширения для бизнес-процесса TestProcess.
    /// </summary>
    public sealed class TestProcessTileExtension :
        TileExtension
    {
        #region Evaluating Handlers

        private static void EnableOnTestTypesAndNoProcesses(object sender, TileEvaluationEventArgs e)
        {
            ICardEditorModel editor = e.CurrentTile.Context.CardEditor;
            ICardModel model;

            e.SetIsEnabledWithCollapsing(
                e.CurrentTile,
                editor != null
                && (model = editor.CardModel) != null
                && model.CardType.Flags.Has(CardTypeFlags.AllowTasks)
                && model.CardType.Name == "Car"
                && model.Card.StoreMode == CardStoreMode.Update
                && model.Card.Sections["WorkflowProcesses"].Rows.Count == 0);
        }


        private static void EnableOnTestTypesAndHasProcesses(object sender, TileEvaluationEventArgs e)
        {
            ICardEditorModel editor = e.CurrentTile.Context.CardEditor;
            ICardModel model;

            e.SetIsEnabledWithCollapsing(
                e.CurrentTile,
                editor != null
                && (model = editor.CardModel) != null
                && model.CardType.Flags.Has(CardTypeFlags.AllowTasks)
                && model.CardType.Name == "Car"
                && model.Card.StoreMode == CardStoreMode.Update
                && model.Card.Sections["WorkflowProcesses"].Rows.Count > 0);
        }

        #endregion

        #region Command Actions

        private static async void StartTestProcessActionAsync(object parameters)
        {
            IUIContext context = UIContext.Current;
            ICardEditorModel editor = context.CardEditor;

            if (editor == null || editor.CardModel == null)
            {
                return;
            }

            await editor.SaveCardAsync(
                context,
                new Dictionary<string, object>()
                    .SetStartingProcessName(TestProcessHelper.ProcessName));
        }


        private static async void SendTestSignalActionAsync(object parameters)
        {
            IUIContext context = UIContext.Current;
            ICardEditorModel editor = context.CardEditor;

            if (editor == null || editor.CardModel == null)
            {
                return;
            }

            await editor.SaveCardAsync(
                context,
                request: new CardSavingRequest(cardModifierActionAsync:
                    async (card, ct) =>
                    {
                        WorkflowQueue queue = card.GetWorkflowQueue();

                        queue.AddSignal(
                            TestProcessHelper.MainSubProcess,
                            TestProcessHelper.TestSignal);
                    }));
        }

        #endregion

        #region Base Overrides

        public override Task InitializingGlobal(ITileGlobalExtensionContext context)
        {
            ITileContextSource contextSource = context.Workspace.LeftPanel;

            context.Workspace.LeftPanel.Tiles.AddRange(
                new Tile(
                    "StartTestProcess",
                    TileHelper.SplitCaption("$KrTest_TestApprovalTile"),
                    context.Icons.Get("Thin127"),
                    contextSource,
                    new DelegateCommand(StartTestProcessActionAsync),
                    TileGroups.Cards,
                    order: 6,
                    evaluating: EnableOnTestTypesAndNoProcesses),

                new Tile(
                    "SendTestSignal",
                    TileHelper.SplitCaption("$KrTest_TestSignalTile"),
                    context.Icons.Get("Thin229"),
                    contextSource,
                    new DelegateCommand(SendTestSignalActionAsync),
                    TileGroups.Cards,
                    order: 6,
                    evaluating: EnableOnTestTypesAndHasProcesses))
                ;

            return Task.CompletedTask;
        }

        #endregion
    }
}
