import { addToMediaStyle } from 'ui';
import { CardUIExtension, ICardUIExtensionContext, IControlViewModel } from 'tessa/ui/cards';
import { DotNetType, Guid } from 'tessa/platform';
import { FieldMapStorage } from 'tessa/cards';
import { IFileControlManager } from 'tessa/ui/files';
import { IFileVersion } from 'tessa/files';
import { IOcrRecognizedBox } from 'tessa/platform/textRecognition/entities/ocrRecognizedBox';
import { IPreviewerViewModel, PdfPreviewerViewModel } from 'tessa/ui/cards/controls/previewer';
import { OcrRecognizedLayout } from 'tessa/platform/textRecognition/entities/ocrRecognizedLayout';
import { TestCardTypeID } from './common';

/**
 * Расширение для модификации инструмента предпросмотра PDF в режиме распознавания.
 * - Выполняется переопределение фабрики для создания инструмента предпросмотра.
 * - Устанавливается режим распознавания в модели представления инструмента.
 * - Устанавливается заголовок в средстве предпросмотра.
 * - Добавляется обработчик события выбора распознанного элемента.
 */
export class OcrPreviewerUIExtension extends CardUIExtension {
  //#region fields

  private _recognizedBoxSelectedDisposer: VoidFunction | null | undefined = null;

  //#endregion

  //#region base overrides

  public shouldExecute(context: ICardUIExtensionContext): boolean {
    // если карточка не для тестов, то ничего не делаем
    return Guid.equals(context.card.typeId, TestCardTypeID);
  }

  public initialized(context: ICardUIExtensionContext): void {
    this.modifyPreviewerViewModel(
      'Color',
      context.card.sections.get('TEST_CarAdditionalInfo')!.fields,
      context.model.controls.get('Color')!,
      context.model.previewManager
    );
  }

  public finalized(_context: ICardUIExtensionContext): void {
    this._recognizedBoxSelectedDisposer?.();
    this._recognizedBoxSelectedDisposer = null;
  }

  //#endregion

  //#region private methods

  private modifyPreviewerViewModel(
    fieldName: string,
    storage: FieldMapStorage,
    control: IControlViewModel,
    manager: IFileControlManager
  ): void {
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
        this.onRecognizedBoxSelect(box, fieldName, storage, control)
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
          this.modifyPreviewerForRecognitionMode(manager.previewToolViewModel, version);
        } else {
          // если фабрика для создания модели представления инструмента была создана,
          const createViewModelFunc = viewModelFactory.createViewModelFunc;
          viewModelFactory.createViewModelFunc = () => {
            // то вызываем изначально установленную фабрику получения модели представления
            const previewerViewModel = createViewModelFunc();
            // и модифицируем саму модель представления
            this.modifyPreviewerForRecognitionMode(previewerViewModel, version);
            return previewerViewModel;
          };
        }
      }
      return viewModelFactory;
    };
  }

  private modifyPreviewerForRecognitionMode(
    previewerViewModel: IPreviewerViewModel,
    version: IFileVersion | null
  ): void {
    const pdfPreviewerViewModel = previewerViewModel as PdfPreviewerViewModel;
    // ищем для файла связанный с ним оригинал файла в формате JSON
    const metadataFileVersion = version?.file?.origin?.lastVersion;
    if (metadataFileVersion && metadataFileVersion.getExtension() === 'json') {
      // переопределим метод (если он был задан) для модификации заголовка
      previewerViewModel.modifyNameHeader = (name: string) => {
        return version!.number > 1 ? `File name is "${name}"` : null;
      };
      // установим режим распознавания
      pdfPreviewerViewModel.recognitionMode = true;
    } else {
      previewerViewModel.modifyNameHeader = undefined;
      pdfPreviewerViewModel.recognitionMode = false;
    }
  }

  private onRecognizedBoxSelect(
    box: IOcrRecognizedBox | null,
    fieldName: string,
    storage: FieldMapStorage,
    control: IControlViewModel
  ): void {
    // Настраиваем цвет фона контрола
    const color = box?.color[0] ? `${box.color[0]} !important` : undefined;
    if (color) {
      const mediaStyle = addToMediaStyle(control.controlStyle, 'default', { background: color });
      control.controlStyle = Object.assign({}, mediaStyle);
    } else {
      delete control.controlStyle?.default?.background;
    }
    // Устанавливаем значение поля
    storage.systemSet(fieldName, box?.text, DotNetType.String);
  }

  //#endregion
}
