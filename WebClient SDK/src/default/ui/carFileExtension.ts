import { getNameAndExtForFile, userSession } from 'common';
import {
  AnnotationClassType,
  AnnotationEditorViewModel,
  EditorMode,
  AnnotationType
} from 'tessa/pdf-annotations';
import { MenuAction, showViewModelDialog, UIContext } from 'tessa/ui';
import {
  PdfAnnotationDialog,
  PdfAnnotationDialogViewModel
} from 'tessa/ui/cards/components/pdfAnnotations/pdfAnnotationsDialog';
import { FileExtension, IFileExtensionContext } from 'tessa/ui/files';

const CarCardTypeID = 'd0006e40-a342-4797-8d77-6501c4b7c4ac';

export class CarFileExtension extends FileExtension {
  public shouldExecute() {
    if (UIContext.current.cardEditor?.cardModel?.cardType.id !== CarCardTypeID) {
      return false;
    }

    return true;
  }

  public openingMenu(context: IFileExtensionContext) {
    if (
      'pdf' !== getNameAndExtForFile(context.file.model?.lastVersion.name).ext?.toLocaleLowerCase()
    ) {
      return;
    }

    const sepIndex = context.actions.findIndex(x => x.name === 'Separator');
    const file = context.file.model;

    const handler =
      (editorMode: EditorMode, onInit?: (vm: AnnotationEditorViewModel) => void) => async () => {
        await file.lastVersion.ensureContentDownloaded();

        const viewModel = new PdfAnnotationDialogViewModel(
          file.lastVersion.name,
          (_, bytes, annotations) => {
            file.replace(
              new File([new Blob([bytes], { type: 'application/pdf' })], file.name),
              false
            );
            console.log(annotations);
          },
          await file.lastVersion.content?.arrayBuffer()!,
          editorMode,
          onInit
        );

        await showViewModelDialog(viewModel, PdfAnnotationDialog);
      };

    context.actions.splice(
      sepIndex + 1,
      0,
      new MenuAction(
        'FacsimileAndAnnotationsMenuGroup',
        '$UI_FacsimileAndAnnotationsGroup',
        'ta icon-thin-439',
        null,
        [
          new MenuAction(
            'ViewAnnotations',
            '$UI_FacsimileAndAnnotationsGroup_ViewAnnotations',
            'ta icon-thin-086',
            handler(EditorMode.ViewAnnotations),
            null,
            false
          ),
          new MenuAction(
            'EditAnnotations',
            '$UI_FacsimileAndAnnotationsGroup_EditAnnotations',
            'ta icon-thin-120',
            handler(EditorMode.EditAnnotations),
            null,
            false
          ),
          new MenuAction(
            'Fascimile',
            '$UI_FacsimileAndAnnotationsGroup_Fascimile',
            'ta icon-thin-119',
            handler(EditorMode.ImageAdding),
            null,
            false
          ),
          new MenuAction(
            'EditFascimileAndAnnotations',
            '$UI_FacsimileAndAnnotationsGroup_EditFascimileAndAnnotations',
            'ta icon-thin-119',
            handler(EditorMode.EditAndImage),
            null,
            false
          ),
          new MenuAction(
            'AddFascimile',
            '$UI_FacsimileAndAnnotationsGroup_AddFascimile',
            'ta icon-thin-119',
            () => {
              const input = document.createElement('INPUT');
              input.setAttribute('type', 'file');
              input.setAttribute('accept', '.jpg,.jpeg,.png');
              input.style.display = 'none';
              document.body.appendChild(input);
              input.addEventListener('change', async e => {
                const target = e.target as HTMLInputElement;
                const file = target.files![0];
                target.remove();
                const bytes = await file.arrayBuffer();
                const reader = new FileReader();
                reader.onload = e => {
                  handler(EditorMode.ImageAdding, vm => {
                    vm.addAnnotation({
                      class: AnnotationClassType.Annotation,
                      type: AnnotationType.Image,
                      page: 1,
                      userId: userSession.UserID,
                      userName: userSession.UserName,
                      fileName: file.name,
                      x: 0,
                      y: 0,
                      width: 200,
                      height: 200,
                      bytes,
                      opacity: 0.5,
                      dataUrl: e.target!.result as string
                    });
                  })();
                };
                reader.readAsDataURL(file);
              });
              input.click();
            },
            null,
            false
          )
        ],
        false
      )
    );
  }
}
