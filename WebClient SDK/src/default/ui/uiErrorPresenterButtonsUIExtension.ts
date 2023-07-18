import { showConfirm, UIButton } from 'tessa/ui';
import {
  CardEditorModelUIErrorOccurredArgs,
  CardUIExtension,
  ICardUIExtensionContext
} from 'tessa/ui/cards';

export class UIErrorPresenterButtonsUIExtension extends CardUIExtension {
  private _disposer: (() => void) | null = null;

  initializing(context: ICardUIExtensionContext): void {
    const cardEditor = context.uiContext.cardEditor;
    if (!cardEditor) {
      return;
    }
    this._disposer = cardEditor.uiErrorOccurred.addWithDispose(this.addRefreshButton);
  }

  public finalized(): void {
    if (this._disposer) {
      this._disposer();
      this._disposer = null;
    }
  }

  private addRefreshButton = (e: CardEditorModelUIErrorOccurredArgs): void => {
    const errorViewModel = e.uiErrorPresenterViewModel;
    const cardEditor = e.cardEditorModel;
    errorViewModel.buttons.push(
      new UIButton('$UI_Tiles_Refresh', async () => {
        try {
          if (!cardEditor.cardModel || cardEditor.operationInProgress) {
            return;
          }
          if (
            (await cardEditor.cardModel.hasChanges()) &&
            !(await showConfirm('$UI_Cards_ConfirmRefresh'))
          ) {
            return;
          }
          await cardEditor.refreshCard(cardEditor.context);
          errorViewModel.close();
        } catch (error) {
          console.error(error);
        }
      })
    );
  };
}
