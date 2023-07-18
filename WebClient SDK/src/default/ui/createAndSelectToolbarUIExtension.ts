import { CardUIExtension, ICardUIExtensionContext, CardToolbarAction,
  CardSavingRequest, CardSavingMode } from 'tessa/ui/cards';
import { IUIContext, UIContext } from 'tessa/ui';

export class CreateAndSelectToolbarUIExtension extends CardUIExtension {

  public initialized(context: ICardUIExtensionContext) {
    const toolbar = context.toolbar;
    // Интересует только тот случай, когда родительским контекстом является представление в режиме выбора с необходимым флагом
    const uiContext = CreateAndSelectToolbarUIExtension.tryGetParentSelectionViewContext(UIContext.current);
    if (!uiContext
      || !uiContext.info
    ) {
      return;
    }

    if (!('CreateAndSelectID' in uiContext.info)) {
      return;
    }

    if (!toolbar.items.find(x => x.name === 'SaveAndSelect')) {
      toolbar.removeItemIfExists('SaveAndCloseCard');
      toolbar.removeItemIfExists('SaveCloseAndCreateCard');

      const saveAndCloseItem = new CardToolbarAction({
        name: 'SaveAndSelect',
        caption: '$KrTiles_SaveAndSelect',
        icon: 'ta icon-Int426',
        order: -1,
        toolTip: '$KrTiles_SaveAndSelect_Tooltip',
        command: this.saveCardAndSelectCommand
      });

      toolbar.addItemIfNotExists(
        saveAndCloseItem,
        {
          name: 'Ctrl+Shift+S', key: 'KeyS', modifiers: {ctrl: true, shift: true}
        }
      );
    }
  }

  private static tryGetParentSelectionViewContext(uiContext: IUIContext) {
    let curr: IUIContext | null = uiContext;
    while (curr && !curr.viewContext) {
      curr = curr.parent;
    }
    return curr;
  }

  private saveCardAndSelectCommand = async () => {
    const context = UIContext.current;
    const editor = context.cardEditor;

    if (!editor || editor.operationInProgress) {
      return;
    }

    const success = await editor.saveCard(context, undefined, new CardSavingRequest(CardSavingMode.KeepPreviousCard));
    if (success) {
      const uiContext = CreateAndSelectToolbarUIExtension.tryGetParentSelectionViewContext(context);
      if (uiContext) {
        uiContext.info['CreateAndSelectID'] = editor.cardModel!.card.id;
        await editor.close(true);
      }
    }
  }

}