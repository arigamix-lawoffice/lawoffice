import {
  CardEditorModelResizeArgs,
  CardUIExtension,
  CardUIInitializationType,
  ICardUIExtensionContext
} from 'tessa/ui/cards';

/**
 * Расширение для карточек, открывающихся в диалоге.
 *
 * Если карточка открывается в диалоговом окне
 * или основной предпросмотр карточки скрыт из-за ширины экрана,
 * предпросмотр файлов будет выводиться так же в диалоговом окне.
 */
export class CardDialogPreviewUIExtension extends CardUIExtension {
  private _disposes: Array<(() => void) | null> = [];

  contextInitialized(context: ICardUIExtensionContext): void {
    if (!context.model.previewManager.enabled) {
      return;
    }

    if (!!context.dialogName) {
      context.model.previewManager.previewInDialog = true;
    }

    const { cardEditor } = context.uiContext;
    if (!cardEditor) {
      return;
    }
    const resize = cardEditor.resize;
    if (!resize.events.has(this.updatePreviewInDialog)) {
      this._disposes.push(resize.addWithDispose(this.updatePreviewInDialog));

      // При рефреше или сохранении карточки, создается новый PreviewManager,
      // нужно вычислить previewInDialog, без события resize.
      const resizedElement = cardEditor.tryGetComponentRef();
      if (!resizedElement || context.initializationType !== CardUIInitializationType.Replacing) {
        return;
      }
      const rect = resizedElement.getBoundingClientRect();
      this.updatePreviewInDialog({ cardEditorModel: cardEditor, resizedElement, rect });
    }
  }

  finalized(): void {
    for (const func of this._disposes) {
      if (func) {
        func();
      }
    }
    this._disposes.length = 0;
  }

  private updatePreviewInDialog(e: CardEditorModelResizeArgs): void {
    const { cardEditorModel, rect } = e;
    const previewArea = cardEditorModel.cardModel?.tryGetPreviewArea();

    if (!rect || !previewArea) {
      return;
    }

    if (rect.width < 1201) {
      previewArea.isCollapsed = true;
    } else if (!previewArea.alwaysHidden) {
      previewArea.isCollapsed = false;
    }
  }
}
