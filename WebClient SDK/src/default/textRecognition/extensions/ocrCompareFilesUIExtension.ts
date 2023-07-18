import { ArrayStorage } from 'tessa/platform/storage';
import { CardRow } from 'tessa/cards';
import { CardTableViewControlViewModel } from '../../ui/tableViewExtension/cardTableViewControlViewModel';
import { CardTableViewRowData } from '../../ui/tableViewExtension/cardTableViewRowData';
import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { checkRequestsStates } from '../misc/ocrUtilities';
import { DotNetType, Guid } from 'tessa/platform';
import { FilePreviewViewModel } from 'tessa/ui/cards/controls';
import { getTessaIcon } from 'common';
import { IFile } from 'tessa/files';
import { IPreviewerViewModel, PdfPreviewerViewModel } from 'tessa/ui/cards/controls/previewer';
import { localize } from 'tessa/localization';
import { MenuAction } from 'tessa/ui';
import { OcrOperationTypeId } from '../misc/ocrConstants';
import { OcrRecognizedLayout } from 'tessa/platform/textRecognition/entities/ocrRecognizedLayout';
import { OcrRequestStates } from '../misc/ocrTypes';
import { reaction } from 'mobx';

/** Расширение, реализующее функционал постраничного сравнения файлов. */
export class OcrCompareFilesUIExtension extends CardUIExtension {
  //#region fields

  private _commonDisposers: Array<(() => void) | null> = [];
  private _syncPageEventDisposers: Array<() => void> = [];

  //#endregion

  //#region base overrides

  public shouldExecute(context: ICardUIExtensionContext): boolean {
    return (
      Guid.equals(context.card.typeId, OcrOperationTypeId) &&
      !checkRequestsStates(context.card, OcrRequestStates.Active, OcrRequestStates.Created)
    );
  }

  public async initialized(context: ICardUIExtensionContext): Promise<void> {
    const controls = context.model.controls;
    const files1 = controls.get('FirstFiles') as CardTableViewControlViewModel;
    const files2 = controls.get('SecondFiles') as CardTableViewControlViewModel;
    const preview1 = controls.get('FirstPreview') as FilePreviewViewModel;
    const preview2 = controls.get('SecondPreview') as FilePreviewViewModel;
    if (!files1 || !files2 || !preview1 || !preview2) {
      return;
    }

    const sourceFileId = context.card.sections.get('OcrOperations')?.fields.get('FileID');
    if (!sourceFileId) {
      return;
    }

    const sourceFiles = context.uiContext.parent?.cardEditor?.cardModel?.fileContainer.files;
    const sourceFile = sourceFiles?.find(f => Guid.equals(f.id, sourceFileId));
    if (!sourceFile) {
      return;
    }

    // Добавление контекстного меню для сравнения распознанного файла с исходным
    Static.generateContextMenu(files1, files2, preview2, sourceFile);
    Static.generateContextMenu(files2, files1, preview1, sourceFile);

    // Установка настроек для контролов предпросмотра в режиме распознавания
    Static.modifyPreviewer(preview1, sourceFileId);
    Static.modifyPreviewer(preview2, sourceFileId);

    // Обработчики для проверки наличия записи с установленным флагом "Основной"
    const requests = context.card.sections.get('OcrRequests')!.rows;
    for (const request of requests) {
      this._commonDisposers.push(Static.onRowCheckHandler(request, requests));
    }

    // Вспомогательная функция для отображения файла в предпросмотре
    const showInPreview = async (
      preview: FilePreviewViewModel,
      selectedRow: ReadonlyMap<string, unknown> | null
    ) => {
      const cardRow = (selectedRow as CardTableViewRowData)?.cardRow;
      if (cardRow) {
        preview.fileControlManager.reset();
        const fileId = cardRow.get('ContentFileID');
        if (fileId) {
          const file = context.fileContainer.files.find(file => Guid.equals(file.id, fileId));
          if (file) {
            await preview.fileControlManager.showPreview(file);
          }
        }
      }
    };

    this._commonDisposers.push(
      // При выборе строки в таблице с результатами выполняется предпросмотр файла
      reaction(
        () => files1.selectedRow,
        async row => await showInPreview(preview1, row)
      ),
      reaction(
        () => files2.selectedRow,
        async row => await showInPreview(preview2, row)
      ),
      // Если в каком-либо превью меняется вью-модель средства, необходимо заново их связать.
      reaction(
        () => preview1.fileControlManager.previewToolViewModel,
        vm1 => this.setPageSyncEvents(vm1, preview2.fileControlManager.previewToolViewModel)
      ),
      reaction(
        () => preview2.fileControlManager.previewToolViewModel,
        vm2 => this.setPageSyncEvents(preview1.fileControlManager.previewToolViewModel, vm2)
      ),
      // Подсветка строк таблицы с результатами распознавания в зависимости от состояния запроса
      Static.colorizeRowsFunc(files1),
      Static.colorizeRowsFunc(files2)
    );

    this.setPageSyncEvents(
      preview1.fileControlManager.previewToolViewModel,
      preview2.fileControlManager.previewToolViewModel
    );
  }

  public finalized(): void {
    Static.dispose(this._commonDisposers);
    Static.dispose(this._syncPageEventDisposers);
  }

  //#endregion

  //#region private

  private static modifyPreviewer(previewer: FilePreviewViewModel, sourceFileId: guid): void {
    const manager = previewer.fileControlManager;
    // переопределим обратный вызов после загрузки файла в инструмент предпросмотра
    const handleFileContentLoading = manager.handleFileContentLoading;
    manager.handleFileContentLoading = async (version, callback, handleParams?) => {
      // вызываем исходный метод
      await handleFileContentLoading(version, callback, handleParams);
      // установим режим отображения распознанных элементов
      const pdfPreviewerViewModel = manager.previewToolViewModel as PdfPreviewerViewModel;
      pdfPreviewerViewModel.recognizedLayout = OcrRecognizedLayout.Text;
    };

    // изменяем способ создания средства предпросмотра
    const previewToolFactory = manager.previewToolFactory;
    manager.previewToolFactory = version => {
      // вызываем изначально установленную фабрику получения фабрики модели представления
      const viewModelFactory = previewToolFactory(version);
      if (viewModelFactory?.type === PdfPreviewerViewModel.type) {
        // если вдруг, модель представления задана (такое, например, возможно,
        // если ранее выполнялся предпросмотр файла того же формата)
        if (manager.previewToolViewModel) {
          // то модифицируем саму модель представления
          Static.modifyPreviewerForRecognitionMode(manager.previewToolViewModel, sourceFileId);
        } else {
          // если фабрика для создания модели представления инструмента была создана,
          const createViewModelFunc = viewModelFactory.createViewModelFunc;
          viewModelFactory.createViewModelFunc = () => {
            // то вызываем изначально установленную фабрику получения модели представления
            const previewerViewModel = createViewModelFunc();
            // и модифицируем саму модель представления
            Static.modifyPreviewerForRecognitionMode(previewerViewModel, sourceFileId);
            return previewerViewModel;
          };
        }
      }
      return viewModelFactory;
    };
  }

  private static modifyPreviewerForRecognitionMode(
    previewerViewModel: IPreviewerViewModel,
    sourceFileId: guid
  ): void {
    const pdfPreviewerViewModel = previewerViewModel as PdfPreviewerViewModel;
    // установим режим распознавания
    pdfPreviewerViewModel.recognitionMode = true;
    // сбросим функцию модификации заголовка
    previewerViewModel.modifyNameHeader = _ =>
      Guid.equals(previewerViewModel.fileVersion?.file.id, sourceFileId)
        ? localize('$UI_Controls_Preview_Headers_SourceFile')
        : null;
  }

  private static onRowCheckHandler(
    request: CardRow,
    requests: ArrayStorage<CardRow>
  ): VoidFunction | null {
    return request.fieldChanged.addWithDispose(args => {
      if (args.fieldName === 'IsMain' && args.fieldValue) {
        (request.get('StateID') === OcrRequestStates.Completed
          ? requests.find(r => r.get('IsMain') && !Guid.equals(r.rowId, request.rowId))
          : request
        )?.set('IsMain', false, DotNetType.Boolean);
      }
    });
  }

  private static colorizeRowsFunc(files: CardTableViewControlViewModel): VoidFunction | null {
    return files.table!.modifyRowActions.addWithDispose(row => {
      const stateId = (row.data as CardTableViewRowData)?.cardRow.get('StateID');
      if (stateId === OcrRequestStates.Completed) {
        row.style.backgroundColor = 'rgba(82,242,112,0.3)'; // green
      } else if (stateId === OcrRequestStates.Active) {
        row.style.backgroundColor = 'rgba(214,232,101,0.3)'; // yellow green
      } else if (stateId === OcrRequestStates.Interrupted) {
        row.style.backgroundColor = 'rgba(247,169,52,0.3)'; // orange
      }
    });
  }

  private setPageSyncEvents(
    previewArea1: IPreviewerViewModel | null,
    previewArea2: IPreviewerViewModel | null
  ): void {
    Static.dispose(this._syncPageEventDisposers);

    if (
      previewArea1 &&
      previewArea2 &&
      previewArea1.type === PdfPreviewerViewModel.type &&
      previewArea2.type === PdfPreviewerViewModel.type
    ) {
      const pdfControl1 = previewArea1 as PdfPreviewerViewModel;
      const pdfControl2 = previewArea2 as PdfPreviewerViewModel;

      // на изменение страницы в одном превью, меняем на такую же страницу в другом превью
      this._syncPageEventDisposers.push(
        reaction(
          () => pdfControl1.pageIndex,
          index => (pdfControl2.pageIndex = index)
        ),
        reaction(
          () => pdfControl2.pageIndex,
          index => (pdfControl1.pageIndex = index)
        )
      );
    }
  }

  private static generateContextMenu(
    filesView: CardTableViewControlViewModel,
    otherView: CardTableViewControlViewModel,
    preview: FilePreviewViewModel,
    sourceFile: IFile
  ): void {
    filesView.table?.rowContextMenuGenerators?.push(ctx => {
      const stateId = (ctx.row.data as CardTableViewRowData)?.cardRow.get('StateID');
      ctx.menuActions.push(
        MenuAction.create({
          name: 'CompareWithSourceFile',
          icon: getTessaIcon('Thin64'),
          caption: localize('$UI_Cards_ContextMenu_CompareWithSourceFile'),
          tooltip: localize('$UI_Cards_ContextMenu_CompareWithSourceFile_Tooltip'),
          isCollapsed: stateId !== OcrRequestStates.Completed,
          action: async () => {
            // сброс выделения для всех строк в таблицах
            filesView.table?.rows.forEach(r => r.selectRow(false));
            otherView.table?.rows.forEach(r => r.selectRow(false));
            // установка выделения текущей строки
            ctx.row.selectRow(true);
            // отображение файла в предпросмотре
            await preview.fileControlManager.showPreview(sourceFile);
          }
        })
      );
    });
  }

  private static dispose(disposers: Array<(() => void) | null>): void {
    for (const disposer of disposers) {
      disposer?.();
    }
    disposers.length = 0;
  }

  //#endregion
}

const Static: typeof OcrCompareFilesUIExtension = OcrCompareFilesUIExtension;
