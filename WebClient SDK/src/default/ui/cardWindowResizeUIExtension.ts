import {
  CardEditorModelResizeArgs,
  CardUIExtension,
  ICardUIExtensionContext
} from 'tessa/ui/cards';
import { DefaultFormMainViewModel, TasksFormViewModel } from 'tessa/ui/cards/forms';
import { MediaSizes } from 'ui';

export class CardWindowResizeUIExtension extends CardUIExtension {
  private _disposes: Array<(() => void) | null> = [];

  public initialized(context: ICardUIExtensionContext): void {
    const { cardEditor } = context.uiContext;
    if (!cardEditor) {
      return;
    }

    const resize = cardEditor.resize;
    if (!resize.events.has(this.updateWindowSize)) {
      this._disposes.push(resize.addWithDispose(this.updateWindowSize));
    }
  }

  private updateWindowSize(e: CardEditorModelResizeArgs) {
    const { cardModel } = e.cardEditorModel;

    if (!cardModel) {
      return;
    }

    const mainForm = cardModel.mainForm as DefaultFormMainViewModel;
    const MD_SIZE = MediaSizes.md;

    if (window.innerWidth >= MD_SIZE && mainForm.selectedTab instanceof TasksFormViewModel) {
      const firstAvailableTab = mainForm.findAvailableSelectedTab();

      if (!firstAvailableTab) {
        return;
      }

      mainForm.selectedTab = firstAvailableTab;
    }
  }

  public finalized(): void {
    for (const func of this._disposes) {
      if (func) {
        func();
      }
    }
    this._disposes.length = 0;
  }
}
