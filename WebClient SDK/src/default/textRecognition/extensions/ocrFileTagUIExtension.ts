import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { FileListViewModel, FileTagViewModel } from 'tessa/ui/cards/controls';
import { getTessaIcon } from 'common';
import { Guid } from 'tessa/platform';
import { OcrKey } from '../misc/ocrConstants';

/** Расширение, выполняющее установку тэга OCR на файле в каждом файловом контроле карточки. */
export class OcrFileTagUIExtension extends CardUIExtension {
  //#region fields

  private static readonly fileTag = new FileTagViewModel(
    getTessaIcon('Int74'),
    'rgba(0, 176, 0, 0.19)'
  );

  //#endregion

  //#region base overrides

  public shouldExecute(context: ICardUIExtensionContext): boolean {
    return context.fileContainer.files.length > 0;
  }

  public contextInitialized(context: ICardUIExtensionContext): void {
    const controls = context.uiContext.cardEditor?.cardModel?.controlsBag?.filter(
      control => control instanceof FileListViewModel
    );
    if (controls) {
      for (const file of context.fileContainer.files) {
        if (file.options?.[OcrKey]) {
          for (const control of controls) {
            const fileListViewModel = control as FileListViewModel;
            const fileControl = fileListViewModel.files.find(f => Guid.equals(f.id, file.id));
            if (fileControl) {
              fileControl.tag = OcrFileTagUIExtension.fileTag;
            }
          }
        }
      }
    }
  }

  //#endregion
}
