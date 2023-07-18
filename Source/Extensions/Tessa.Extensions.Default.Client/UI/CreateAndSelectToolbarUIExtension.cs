using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Tessa.Extensions.Default.Client.Views;
using Tessa.Extensions.Default.Shared;
using Tessa.Extensions.Platform.Client.Tiles;
using Tessa.Platform;
using Tessa.UI;
using Tessa.UI.Cards;
using Tessa.UI.Tiles;

namespace Tessa.Extensions.Default.Client.UI
{
    public sealed class CreateAndSelectToolbarUIExtension : CardUIExtension
    {
        public override async Task Initialized(ICardUIExtensionContext context)
        {
            // Интересует только тот случай, когда родительским контекстом является представление в режиме выбора с необходимым флагом
            var uiContext = TryGetParentSelectionViewContext(UIContext.Current);
            if (uiContext?.Info?.Contains(new KeyValuePair<string, object>(CreateCardExtensionViewModel.CreateAndSelectID, null)) != true)
            {
                return;
            }
                
            var actions = context.ToolbarActions;
            if (!actions.Contains(DefaultTileNames.SaveAndSelect))
            {
                actions.Remove(TileNames.SaveAndCloseCard);
                actions.Remove(TileNames.SaveCloseAndCreateCard);
                
                var saveAndCloseItem = new CardToolbarAction(
                    DefaultTileNames.SaveAndSelect,
                    "$KrTiles_SaveAndSelect",
                    context.UIContext.CardEditor.Toolbar.CreateIcon("Int426"),
                    SaveCardAndSelectCommand(),
                    tooltip: TileHelper.GetToolTip("$KrTiles_SaveAndSelect_Tooltip", TileKeys.SaveAndCloseCard),
                    order: -1,
                    gestures: new [] { TileKeys.SaveAndCloseCard }
                );
                
                actions.Add(saveAndCloseItem);
            }
        }
        
        private static ICommand SaveCardAndSelectCommand()
        {
            return new DelegateCommand(async _ =>
            {
                var context = UIContext.Current;
                var editor = UIContext.Current.CardEditor;
                
                if (editor != null 
                    && !editor.OperationInProgress)
                {
                    bool success = await editor.SaveCardAsync(
                        context,
                        request: new CardSavingRequest(CardSavingMode.KeepPreviousCard));
                    
                    if (success)
                    {
                        var uiContext = TryGetParentSelectionViewContext(context);
                        uiContext.Info[CreateCardExtensionViewModel.CreateAndSelectID] = editor.CardModel.Card.ID;
                        await editor.CloseAsync();
                    }
                }
            });
        }

        private static IUIContext TryGetParentSelectionViewContext(
            IUIContext uiContext)
        {
            var curr = uiContext;
            while (curr != null 
                && curr.ViewContext is null)
            {
                curr = curr.Parent;
            }
            return curr;
        }
        
    }
}