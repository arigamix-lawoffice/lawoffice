import { addToMediaStyle } from 'ui';
import { ArrayStorage } from 'tessa/platform/storage';
import { CardRow, FieldMapStorage } from 'tessa/cards';
import { CardUIExtension, ICardUIExtensionContext, IControlViewModel } from 'tessa/ui/cards';
import { checkRequestsStates } from '../misc/ocrUtilities';
import { DotNetType, Guid, Visibility } from 'tessa/platform';
import { FilePreviewViewModel } from 'tessa/ui/cards/controls';
import { IOcrRecognizedBox } from 'tessa/platform/textRecognition/entities/ocrRecognizedBox';
import { IPreviewerViewModel, PdfPreviewerViewModel } from 'tessa/ui/cards/controls/previewer';
import { OcrOperationTypeId } from '../misc/ocrConstants';
import { OcrRecognizedLayout } from 'tessa/platform/textRecognition/entities/ocrRecognizedLayout';
import { OcrRequestStates } from '../misc/ocrTypes';

/** Расширение для реализации общей UI логики взаимодействия с карточкой OCR. */
export class OcrVerificationUIExtension extends CardUIExtension {
  //#region fields

  private _recognizedBoxSelectedDisposer: VoidFunction | null | undefined = null;
  private readonly _disposers: (VoidFunction | null)[] = [];

  //#endregion

  //#region base overrides

  public shouldExecute(context: ICardUIExtensionContext): boolean {
    return (
      Guid.equals(context.card.typeId, OcrOperationTypeId) &&
      !checkRequestsStates(context.card, OcrRequestStates.Active, OcrRequestStates.Created)
    );
  }

  public async initialized(context: ICardUIExtensionContext): Promise<void> {
    const sections = context.card.sections;
    const requests = sections.get('OcrRequests')!.rows;
    const operations = sections.get('OcrOperations')!.fields;
    const resultsVirtual = sections.get('OcrResultsVirtual')!.fields;

    const controls = context.model.controls;
    const recognizedTextCtrl = controls.get('Text')!;
    const previewer = controls.get('Preview') as FilePreviewViewModel;

    // Скрытие подсказки о наличии текстового слоя в файле
    if (operations.get('FileHasText')) {
      const textLayerHintCtrl = controls.get('TextLayerHint')!;
      textLayerHintCtrl.controlVisibility = Visibility.Visible;
    }

    // Поиск файла, для отображения в предпросмотре на вкладке "Верификация" по умолчанию.
    // Если подходящий файл не был найден, то нет смысла в дальнейшей обработке.
    const fileId = Static.getRecognizedFileId(requests);
    if (!fileId) {
      return;
    }

    this.modifyPreviewer(previewer, resultsVirtual, recognizedTextCtrl);

    // вспомогательная функция для установки предпросмотра для файла
    const setFilePreview = async (fileId: guid | null | undefined) => {
      //TODO: убедиться, что не нужно this._recognizedBoxSelectedDisposer?.();

      const file = context.fileContainer.files.find(file => Guid.equals(file.id, fileId));
      if (file) {
        previewer.fileControlManager.resetPreview();
        await previewer.fileControlManager.showPreview(file);
      }
    };

    // Отображаем файл в предпросмотре
    await setFilePreview(fileId);

    // Теперь для каждой строки запроса выполним подписку на изменение поля
    for (const request of requests) {
      this._disposers.push(
        request.fieldChanged.addWithDispose(async args => {
          // Если было установлено поле "Основной", то отображаем этот файл в предпросмотре
          if (args.fieldName === 'IsMain' && args.fieldValue) {
            await setFilePreview(request.get('ContentFileID'));
          }
        })
      );
    }
  }

  public finalized(): void {
    this._recognizedBoxSelectedDisposer?.();
    this._recognizedBoxSelectedDisposer = null;
    for (const disposer of this._disposers) {
      disposer?.();
    }
    this._disposers.length = 0;
  }

  //#endregion

  //#region private

  private modifyPreviewer(
    previewer: FilePreviewViewModel,
    storage: FieldMapStorage,
    ...controls: IControlViewModel[]
  ): void {
    const manager = previewer.fileControlManager;
    // переопределим обратный вызов после загрузки файла в инструмент предпросмотра
    const handleFileContentLoading = manager.handleFileContentLoading;
    manager.handleFileContentLoading = async (version, callback, handleParams?) => {
      // вызываем исходный метод
      await handleFileContentLoading(version, callback, handleParams);
      const pdfPreviewerViewModel = manager.previewToolViewModel as PdfPreviewerViewModel;
      // подписываемся на выбор распознанного элемента в предпросмотре и добавляем обработчик
      // выполняем это здесь, так как только после загрузки файла будет задана recognizedCollection
      const boxSelectedHandler = pdfPreviewerViewModel?.recognizedCollection?.recognizedBoxSelected;
      this._recognizedBoxSelectedDisposer = boxSelectedHandler?.addWithDispose(box =>
        Static.onRecognizedBoxSelect(box, storage, controls)
      );
      // установим режим отображения распознанных элементов
      pdfPreviewerViewModel.recognizedLayout = boxSelectedHandler
        ? OcrRecognizedLayout.Word
        : OcrRecognizedLayout.Default;
    };

    // изменяем способ создания средства предпросмотра
    const previewToolFactory = manager.previewToolFactory;
    manager.previewToolFactory = version => {
      // вызовем предыдущие методы обратного вызова для освобождения ресурсов, если они были
      this._recognizedBoxSelectedDisposer?.();
      // вызываем изначально установленную фабрику получения фабрики модели представления
      const viewModelFactory = previewToolFactory(version);
      if (viewModelFactory?.type === PdfPreviewerViewModel.type) {
        // если вдруг, модель представления задана (такое, например, возможно,
        // если ранее выполнялся предпросмотр файла того же формата)
        if (manager.previewToolViewModel) {
          // то модифицируем саму модель представления
          Static.modifyPreviewerForRecognitionMode(manager.previewToolViewModel);
        } else {
          // если фабрика для создания модели представления инструмента была создана,
          const createViewModelFunc = viewModelFactory.createViewModelFunc;
          viewModelFactory.createViewModelFunc = () => {
            // то вызываем изначально установленную фабрику получения модели представления
            const previewerViewModel = createViewModelFunc();
            // и модифицируем саму модель представления
            Static.modifyPreviewerForRecognitionMode(previewerViewModel);
            return previewerViewModel;
          };
        }
      }
      return viewModelFactory;
    };
  }

  private static modifyPreviewerForRecognitionMode(previewerViewModel: IPreviewerViewModel): void {
    const pdfPreviewerViewModel = previewerViewModel as PdfPreviewerViewModel;
    // установим режим распознавания
    pdfPreviewerViewModel.recognitionMode = true;
    // сбросим функцию модификации заголовка
    previewerViewModel.modifyNameHeader = _ => null;
  }

  private static onRecognizedBoxSelect(
    box: IOcrRecognizedBox | null,
    storage: FieldMapStorage,
    controls: IControlViewModel[]
  ): void {
    // Настраиваем цвет фона контрола
    const color = box?.color[0] ? `${box.color[0]} !important` : undefined;
    for (const control of controls) {
      if (color) {
        const mediaStyle = addToMediaStyle(control.controlStyle, 'default', { background: color });
        control.controlStyle = Object.assign({}, mediaStyle);
      } else {
        delete control.controlStyle?.default?.background;
      }
      // Устанавливаем значение для полей
      storage.systemSet(control.name!, box?.text, DotNetType.String);
    }
  }

  private static getRecognizedFileId(storage: ArrayStorage<CardRow>): guid | null | undefined {
    return (
      // поиск файла, отмеченного, как основной
      storage.find(r => r.get('IsMain'))?.get('ContentFileID') ??
      // поиск последнего успешно распознанного файла
      storage
        .sort((a, b) => a.get('Created') - b.get('Created'))
        .find(r => r.get('StateID') === OcrRequestStates.Completed)
        ?.get('ContentFileID')
    );
  }

  //#endregion
}

const Static: typeof OcrVerificationUIExtension = OcrVerificationUIExtension;
