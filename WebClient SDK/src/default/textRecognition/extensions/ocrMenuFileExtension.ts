import { AdvancedCardDialogManager, CardEditorModel } from 'tessa/ui/cards';
import { createTypedField, DotNetType, Guid } from 'tessa/platform';
import { FileExtension, IFileExtensionContext } from 'tessa/ui/files';
import { FileVersionState } from 'tessa/files';
import { getContextEditorModel } from '../misc/ocrUtilities';
import { getTessaIcon } from 'common';
import { IStorage } from 'tessa/platform/storage';
import { MenuAction, showLoadingOverlay, showNotEmpty, tryGetFromInfo, UIContext } from 'tessa/ui';
import { MultipageFileExtensions, OcrKey, SupportedFileExtensions } from '../misc/ocrConstants';
import { ocrCardModelWithMetadataFactory } from '../misc/ocrCardModelWithMetadataFactory';
import { ocrCreateRequestDialog } from '../misc/ocrCreateRequestDialog';
import { OcrOperationTypeId, OcrOperationTypeName } from '../misc/ocrConstants';
import { OcrSettings } from '../misc/ocrSettings';

/** Расширение, добавляющее пункт "Распознавание текста" в контекстное меню файла. */
export class OcrMenuFileExtension extends FileExtension {
  //#region base overrides

  public shouldExecute(_context: IFileExtensionContext): boolean {
    return OcrSettings.instance.isEnabled;
  }

  public openingMenu(context: IFileExtensionContext): void {
    const { editor, model } = getContextEditorModel();
    if (!editor || !model) {
      return;
    }

    const file = context.file.model;
    const fileVersion = file.lastVersion;

    // Проверка на возможность выполнения распознавания файла
    const canRecognize =
      !file.isDirty &&
      context.files.length === 1 &&
      file.permissions.canReplace &&
      fileVersion !== file.versionAdded &&
      fileVersion.state === FileVersionState.Success &&
      SupportedFileExtensions.includes(file.getExtension());
    if (!canRecognize) {
      return;
    }

    // Находим пункт "Предпросмотр" контекстного меню файла
    const previewIndex = context.actions.findIndex(x => x.name === 'Preview');
    if (previewIndex === -1) {
      return;
    }

    // Добавляем в контекстное меню файла пункт "Распознавание текста" после пункта "Предпросмотр"
    context.actions.splice(
      previewIndex + 1,
      0,
      new MenuAction(
        'TextRecognition',
        '$UI_Controls_FilesControl_TextRecognition',
        getTessaIcon('Thin76'),
        async () => {
          // Пытаемся получить параметры OCR из опций исходного файла
          const ocrOptions = tryGetFromInfo<IStorage<unknown> | undefined>(file.options, OcrKey);
          let ocrCardId = tryGetFromInfo<guid | undefined>(ocrOptions, 'CardID');

          if (!ocrCardId) {
            const isMultipage = MultipageFileExtensions.includes(file.getExtension());
            // Отображаем диалог инициации нового запроса на распознавание
            const result = await ocrCreateRequestDialog(isMultipage);
            if (!result) {
              return;
            }

            // Достаем карточку файла, запоминаем состояние и опции файла
            const cardFile = model.card.files.find(f => Guid.equals(f.rowId, file.id))!;
            const cardFileFlags = cardFile.flags;

            // В карточке уже может быть несколько распознанных файлов,
            // или же может быть несколько измененных файлов, поэтому
            // добавляем флаг, чтобы однозначно отличить текущий распознаваемый файл.
            cardFile.info[OcrKey] = createTypedField(true, DotNetType.Boolean);
            ocrCardId = Guid.newGuid();

            // В опции исходного файла добавляется идентификатор создаваемой карточки OCR
            file.options[OcrKey] = { ['CardID']: createTypedField(ocrCardId, DotNetType.Guid) };
            file.source.notifyOptionsModified(file);

            // Параметры для OCR запроса добавляем в качестве дополнительной информации при сохранении карточки
            const info: IStorage<unknown> = {
              [OcrKey]: {
                OcrRequests: result.request.getStorage(),
                OcrRequestsLanguages: result.languages.getStorage()
              }
            };

            // Пытаемся сохранить карточку, если неуспешно, то удаляем признак OCR из файла
            let saved = false;
            try {
              saved = await editor.saveCard(editor.context, info);
            } finally {
              if (!saved) {
                delete cardFile.info[OcrKey];
                delete file.options[OcrKey];
                file.source.notifyOptionsModified(file);
                cardFile.flags = cardFileFlags;
                ocrCardId = undefined;
                return;
              }
            }
          }

          // К этому моменту, на сервере уже была создана карточка OCR и известным нам идентификатором,
          // а также создан запрос на распознавание файла, поэтому можем открыть карточку OCR
          await showLoadingOverlay(async splashResolve => {
            await AdvancedCardDialogManager.instance.openCard({
              cardId: ocrCardId,
              cardTypeId: OcrOperationTypeId,
              cardTypeName: OcrOperationTypeName,
              context: UIContext.current,
              withUIExtensions: true,
              splashResolve: splashResolve,
              dialogOptions: { openInFullscreen: true },
              cardEditorFactory: () => new CardEditorModel(ocrCardModelWithMetadataFactory(model)),
              cardModifierAction: async editorContext => {
                const cardInfo = editorContext.card.info;
                cardInfo[OcrKey] = createTypedField(true, DotNetType.Boolean);
              },
              cardEditorModifierAction: async editorContext => {
                const editor = editorContext.editor;
                await showNotEmpty(editor.notifyContextInitialized());
              }
            });
          });
        }
      )
    );
  }

  //#endregion
}
