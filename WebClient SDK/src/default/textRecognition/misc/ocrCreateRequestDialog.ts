import { ArrayStorage } from 'tessa/platform/storage';
import { CardRow, CardRowState, FieldMapStorage } from 'tessa/cards';
import { createDialogForm, FormCreationOptions, showNotEmpty, UIButton } from 'tessa/ui';
import { DotNetType, Guid, Visibility } from 'tessa/platform';
import { OcrRequestDialogTypeName } from './ocrConstants';
import { OcrRequestStates } from './ocrTypes';
import { showDialog } from 'tessa/ui/uiHost';
import { userSession } from 'common/utility';
import { ValidationResultBuilder, ValidationResultType } from 'tessa/platform/validation';

/**
 * Выполняет создание и отображение диалога "Создание запроса на распознавание".
 * @param isMultipage Признак многостраничного файла.
 * @returns Информация по запросу, заполненная пользователем - если создание выполнено успешно, в противном случае - null.
 */
export async function ocrCreateRequestDialog(isMultipage: boolean): Promise<{
  request: FieldMapStorage;
  languages: ArrayStorage<CardRow>;
} | null> {
  // Создаем форму диалогового окна для создания запроса на распознавание файла
  const createDialogResult = await createDialogForm(
    OcrRequestDialogTypeName,
    OcrRequestDialogTypeName,
    FormCreationOptions.None
  );

  // Если по каким-то причинам создать форму не удалось, то выходим
  if (!createDialogResult) {
    return null;
  }

  // Получаем форму и модель диалогового окна и выполняем инициализацию параметров
  const [form, cardModel] = createDialogResult;
  const languagesCtrl = cardModel.controls.get('Languages');
  const request = cardModel.card.sections.get('OcrRequest')?.fields;
  const requestLanguages = cardModel.card.sections.get('OcrRequestLanguages')?.rows;
  if (!languagesCtrl || !request || !requestLanguages) {
    return null;
  }

  const createDetectableLanguage = () => {
    const language = requestLanguages.add();
    language.set('LanguageID', 0, DotNetType.Int32);
    language.set('LanguageISO', null, DotNetType.String);
    language.set('LanguageCaption', 'Auto', DotNetType.String);
    language.rowId = Guid.newGuid();
    language.state = CardRowState.Inserted;
  };

  // Выполняем необходимые настройки и модификации в диалоговом окне перед отображением
  languagesCtrl.isReadOnly = request.get('DetectLanguages');
  if (languagesCtrl.isReadOnly) {
    createDetectableLanguage();
  }

  const fieldChangedDisposer = request.fieldChanged.addWithDispose(e => {
    if (e.fieldName === 'DetectLanguages') {
      languagesCtrl.isReadOnly = !languagesCtrl.isReadOnly;
      requestLanguages.clear();
      if (e.fieldValue) {
        createDetectableLanguage();
      }
    }
  });

  // Скрытие метки и признака перезаписи многостраничного файла
  if (!isMultipage) {
    const overwriteCtrl = cardModel.controls.get('Overwrite');
    if (overwriteCtrl) {
      overwriteCtrl.controlVisibility = Visibility.Collapsed;
    }

    const textLayerHintCtrl = cardModel.controls.get('TextLayerHint');
    if (textLayerHintCtrl) {
      textLayerHintCtrl.controlVisibility = Visibility.Collapsed;
    }
  }

  // Показываем форму диалогового окна с вариантами "Подтвердить" и "Отмена"
  const dialogResult = await showDialog<boolean>(
    form,
    null,
    [
      new UIButton('$UI_Common_OK', async button => {
        const validationResult = new ValidationResultBuilder();

        // Вспомогательная функция для добавления ошибки валидации и подсветки контрола
        const addError = (controlName: string, message: string) => {
          validationResult.add(ValidationResultType.Error, message);
          const control = cardModel.controls.get(controlName);
          if (control) {
            control.hasActiveValidation = true;
          }
        };

        // Выполняем валидацию полей, заполненных пользователем
        if (request?.get('SegmentationModeID') == null) {
          addError('SegmentationMode', '$CardTypes_Validators_ImagePageSegmentationMode');
        }
        if (!requestLanguages?.some(l => l.state !== CardRowState.Deleted)) {
          addError('Languages', '$CardTypes_Validators_Languages');
        }

        // Если все данные введены корректно, то выходим из диалога
        if (!(await showNotEmpty(validationResult.build()))) {
          fieldChangedDisposer?.();
          button.close(true);
        }
      }),
      new UIButton('$UI_Common_Cancel', button => {
        fieldChangedDisposer?.();
        button.close(false);
      })
    ],
    { hideTopCloseIcon: true },
    undefined,
    { style: { width: '50%', padding: '0.5em' } }
  );

  return dialogResult
    ? {
        request: cardModel.card.sections.get('OcrRequest')!.fields,
        languages: cardModel.card.sections.get('OcrRequestLanguages')!.rows
      }
    : null;
}

/**
 * Создает строку {@link CardRow} и переносит в нее данные по запросу из {@link request}.
 * @param request Запрос на распознавание текса.
 * @returns Строку {@link CardRow} с информацией по распознаванию.
 */
export function ocrCreateRequestRow(request: FieldMapStorage): CardRow {
  const row = new CardRow();
  row.state = CardRowState.Inserted;
  row.rowId = Guid.newGuid();
  row.set('Created', new Date().toISOString(), DotNetType.DateTime);
  row.set('CreatedByID', userSession.UserID, DotNetType.Guid);
  row.set('CreatedByName', userSession.UserName, DotNetType.String);
  row.set('StateID', OcrRequestStates.Created, DotNetType.Int32);
  row.set('Confidence', request.getField('Confidence')!);
  row.set('Preprocess', request.getField('Preprocess')!);
  row.set('SegmentationModeID', request.getField('SegmentationModeID')!);
  row.set('SegmentationModeName', request.getField('SegmentationModeName')!);
  row.set('DetectLanguages', request.getField('DetectLanguages')!);
  row.set('Overwrite', request.getField('Overwrite')!);
  row.set('DetectRotation', request.getField('DetectRotation')!);
  row.set('DetectTables', request.getField('DetectTables')!);
  return row;
}

/**
 * Создает строку {@link CardRow} и переносит в нее данные по языкам запроса из {@link languages}.
 * @param languages Языки для запроса на распознавание текса.
 * @param requestId Идентификатор созданной строки с запросом на распознавание текста.
 * @returns Строку {@link CardRow} с информацией по распознаванию.
 */
export function ocrCreateLanguagesRows(
  languages: ArrayStorage<CardRow>,
  requestId: guid
): CardRow[] {
  const uniqueLanguages = new Map<number, CardRow>();
  for (const language of languages) {
    const languageId = language.getField('LanguageID')!;
    if (!uniqueLanguages.has(languageId.$value)) {
      const row = new CardRow();
      row.state = CardRowState.Inserted;
      row.rowId = Guid.newGuid();
      row.parentRowId = requestId;
      row.set('LanguageID', languageId);
      row.set('LanguageISO', language.getField('LanguageISO')!);
      row.set('LanguageCaption', language.getField('LanguageCaption')!);
      uniqueLanguages.set(languageId.$value, row);
    }
  }
  return [...uniqueLanguages.values()];
}
