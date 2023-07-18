import { ArrayStorage } from 'tessa/platform/storage';
import { CardMetadataSectionSealed } from 'tessa/cards/metadata';
import { CardRow, CardRowState, FieldMapStorage } from 'tessa/cards';
import { CardToolbarAction, CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { checkRequestsStates, getContextEditorModel } from '../misc/ocrUtilities';
import { createTypedField, DotNetType, Guid } from 'tessa/platform';
import { FileListViewModel, FilePreviewViewModel } from 'tessa/ui/cards/controls';
import { getTessaIcon } from 'common';
import { localize } from 'tessa/localization';
import { MetadataStorage } from 'tessa/metadataStorage';
import { MultipageFileExtensions, OcrDeleteOperationIdKey, OcrKey } from '../misc/ocrConstants';
import {
  ocrCreateLanguagesRows,
  ocrCreateRequestDialog,
  ocrCreateRequestRow
} from '../misc/ocrCreateRequestDialog';
import { OcrOperationTypeId } from '../misc/ocrConstants';
import { OcrRequestStates } from '../misc/ocrTypes';
import { showNotEmpty, tryGetFromInfo } from 'tessa/ui';
import { ValidationResult } from 'tessa/platform/validation';

/** Расширение, добавляющее дополнительные кнопки тулбара в карточке операции OCR. */
export class OcrToolbarUIExtension extends CardUIExtension {
  //#region base overrides

  public shouldExecute(context: ICardUIExtensionContext): boolean {
    return (
      Guid.equals(context.card.typeId, OcrOperationTypeId) &&
      !checkRequestsStates(context.card, OcrRequestStates.Active, OcrRequestStates.Created)
    );
  }

  public async initialized(context: ICardUIExtensionContext): Promise<void> {
    const editor = context.uiContext.cardEditor;
    if (!editor) {
      return;
    }

    editor.toolbar.addItemIfNotExists(
      new CardToolbarAction({
        order: 0,
        name: 'OcrProcessRun',
        icon: getTessaIcon('Thin76'),
        caption: '$UI_ToolbarButtons_OcrProcessRun',
        toolTip: '$UI_Tiles_OcrProcessRun_Tooltip',
        command: this.ocrProcessRunCommand
      }),
      {
        name: 'Ctrl+Shift+R',
        key: 'KeyR',
        modifiers: { ctrl: true, shift: true }
      }
    );

    if (context.uiContext.parent?.cardEditor?.cardModel) {
      editor.toolbar.addItemIfNotExists(
        new CardToolbarAction({
          order: 1,
          name: 'OcrResultSave',
          icon: getTessaIcon('Thin418'),
          caption: '$UI_ToolbarButtons_OcrResultSave',
          toolTip: '$UI_Tiles_OcrResultSave_Tooltip',
          command: this.ocrResultSaveCommand
        }),
        {
          name: 'Ctrl+Shift+S',
          key: 'KeyS',
          modifiers: { ctrl: true, shift: true }
        }
      );
    } else {
      editor.toolbar.removeItemIfExists('OcrResultSave');
    }
  }

  public contextInitialized(context: ICardUIExtensionContext): void {
    if (
      tryGetFromInfo<boolean>(context.card.info, OcrKey, false) &&
      !checkRequestsStates(
        context.card,
        OcrRequestStates.Created,
        OcrRequestStates.Active,
        OcrRequestStates.Completed
      )
    ) {
      // Если в карточке нет ни одного успешного запроса, то отображаем диалог создания нового запроса
      this.ocrProcessRunCommand();
    }
  }

  //#endregion

  //#region commands

  private ocrProcessRunCommand = async () => {
    const { uiContext, editor, model } = getContextEditorModel();
    if (!editor || !model) {
      return;
    }
    // Отображаем диалог создания нового запроса на распознавание
    const fileName: string = model.card.sections.get('OcrOperations')!.fields.get('FileName');
    const dotIndex = fileName.lastIndexOf('.');
    const fileExtension = dotIndex !== -1 ? fileName.substring(dotIndex + 1) : '';
    const result = await ocrCreateRequestDialog(MultipageFileExtensions.includes(fileExtension));
    if (result) {
      // Создаем запрос на распознавание в карточке OCR
      const requests = model.card.sections.get('OcrRequests')!.rows;
      const languages = model.card.sections.get('OcrRequestsLanguages')!.rows;
      const requestRow = requests.add(ocrCreateRequestRow(result.request));
      languages.push(...ocrCreateLanguagesRows(result.languages, requestRow.rowId));
      // Сохраняем карточку OCR
      if (!(await editor.saveCard(uiContext))) {
        OcrToolbarUIExtension.removeInsertedRows(requests);
        OcrToolbarUIExtension.removeInsertedRows(languages);
      }
    }
  };

  private ocrResultSaveCommand = async () => {
    const { uiContext, editor, model } = getContextEditorModel();
    if (!editor || !model) {
      return;
    }

    // Получение модель исходной карточки
    const sourceModel = uiContext.parent?.cardEditor?.cardModel;
    if (!sourceModel) {
      return;
    }

    // Проверка на наличие изменений файла и верифицированных данных в карточке операции OCR
    if (model.card.hasChanges(true)) {
      const error = ValidationResult.fromError('$UI_Cards_CannotCreateFileTemplateOnNewCard');
      await showNotEmpty(error);
      return;
    }

    // Выбор текущего файла, установленного в предпросмотре (вкладка "Верификация")
    const preview = model.controls.get('Preview') as FilePreviewViewModel;
    const previewTool = preview.fileControlManager.previewToolViewModel;
    const fileId = previewTool?.fileVersion?.file.id;
    if (!fileId) {
      const error = ValidationResult.fromError('$UI_Controls_Preview_FileNotLoaded');
      await showNotEmpty(error);
      return;
    }

    const ocrSections = model.card.sections;
    // Поиск в исходной карточке файла, проассоциированный с карточкой операции OCR
    const sourceFileId = ocrSections.get('OcrOperations')!.fields.get('FileID');
    const sourceFile = sourceModel.fileContainer.files.find(f => Guid.equals(f.id, sourceFileId));
    const sourceCardFile = sourceModel.card.files.find(f => Guid.equals(f.rowId, sourceFileId));
    if (!sourceFile || !sourceCardFile) {
      const error = ValidationResult.fromError(localize('$UI_Common_FileNotFound', sourceFileId));
      await showNotEmpty(error);
      return;
    }

    // Поиск распознанного файла в файловом контейнере карточки операции OCR
    const file = model.fileContainer.files.find(file => Guid.equals(file.id, fileId));
    if (!file) {
      const error = ValidationResult.fromError(localize('$UI_Common_FileNotFound', fileId));
      await showNotEmpty(error);
      return;
    }

    // Загрузка контента распознанного файла
    const validationResult = await file.ensureContentDownloaded();
    if (await showNotEmpty(validationResult)) {
      return;
    }

    // Удаление признака и тэга OCR из исходного файла в исходной карточке
    delete sourceFile.options[OcrKey];
    sourceFile.source.notifyOptionsModified(sourceFile);
    for (const control of sourceModel.controlsBag) {
      if (control instanceof FileListViewModel) {
        const fileListViewModel = control as FileListViewModel;
        const fileViewModel = fileListViewModel.files.find(f => Guid.equals(f.id, sourceFileId));
        if (fileViewModel) {
          // сбрасываем тэг OCR с модели представления файла
          fileViewModel.tag = null;
        }
      }
    }

    // Выполнение замены исходного файла распознанным файлом
    sourceFile.replace(file.lastVersion.content!, true);
    // Добавление идентификатора карточки операции OCR (для ее удаления) в info файла исходной карточки
    sourceCardFile.info[OcrDeleteOperationIdKey] = createTypedField(model.card.id, DotNetType.Guid);

    // Попытка получения секций исходной карточки и переноса в них значений из карточки операции OCR
    const sections = sourceModel.card.tryGetSections();
    if (sections) {
      const fieldsCache = new Map<string, FieldMapStorage | null | undefined>();
      const sectionsMetadataCache = new Map<string, CardMetadataSectionSealed | undefined>();
      const ocrMappingComplexFields = ocrSections.get('OcrMappingComplexFields')!.rows;

      for (const mappingField of ocrSections.get('OcrMappingFields')!.rows) {
        const sectionName = mappingField.get('Section');

        // Получаем поля секции исходной карточки
        let fields: FieldMapStorage | null | undefined;
        if (!fieldsCache.has(sectionName)) {
          fields = sections.tryGet(sectionName)?.tryGetFields();
          fieldsCache.set(sectionName, fields);
        } else {
          fields = fieldsCache.get(sectionName);
        }

        if (!fields || fields.size <= 0) {
          continue;
        }

        // Поиск связанных полей (для ссылки) с текущим полем
        const mappingComplexFields = ocrMappingComplexFields.filter(r =>
          Guid.equals(r.parentRowId, mappingField.rowId)
        );

        const copyField = (mappingRow: CardRow) => {
          const fieldName = mappingRow.get('Field');
          if (!fields?.has(fieldName)) {
            return;
          }

          let fieldType = fields.tryGetField(fieldName)?.$type;

          if (!fieldType) {
            let sectionMetadata: CardMetadataSectionSealed | undefined;
            if (!sectionsMetadataCache.has(sectionName)) {
              sectionMetadata = MetadataStorage.instance.cardMetadata.getSectionByName(sectionName);
              sectionsMetadataCache.set(sectionName, sectionMetadata);
            } else {
              sectionMetadata = sectionsMetadataCache.get(sectionName);
            }

            const columnMetadata = sectionMetadata?.getColumnByName(fieldName);
            fieldType = columnMetadata?.metadataType?.dotNetType;
            if (!fieldType) {
              return;
            }
          }

          fields.set(fieldName, mappingRow.get('Value'), fieldType);
        };

        if (mappingComplexFields.length > 0) {
          for (const complexField of mappingComplexFields) {
            copyField(complexField);
          }
        } else {
          copyField(mappingField);
        }
      }
    }

    // Закрытие редактора карточки операции OCR
    await editor.close();
  };

  //#endregion

  //#region helpers

  private static removeInsertedRows(rows: ArrayStorage<CardRow>) {
    for (let i = rows.length - 1; i >= 0; --i) {
      const row = rows[i];
      if (row.state === CardRowState.Inserted) {
        rows.remove(row);
      }
    }
  }

  //#endregion
}
