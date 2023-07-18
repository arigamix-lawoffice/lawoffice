import {
  AdvancedCardDialogManager,
  CardToolbarAction,
  CardUIExtension,
  ICardModel,
  ICardUIExtensionContext,
  VirtualSchemePresenter
} from 'tessa/ui/cards';
import {
  AutoCompleteEntryViewModel,
  AutoCompleteTableViewModel,
  ButtonViewModel,
  DateTimeViewModel,
  FileListViewModel,
  FilePreviewViewModel,
  GridRowAction,
  GridViewModel,
  RowAutoCompleteItem,
  TextBoxViewModel
} from 'tessa/ui/cards/controls';
import {
  createCardEditorModel,
  createDialogForm,
  FormCreationOptions,
  LoadingOverlay,
  MenuAction,
  showError,
  showLoadingOverlay,
  showMessage,
  showNotEmpty,
  tryGetFromInfo,
  tryGetFromSettings,
  UIButton,
  UIContext
} from 'tessa/ui';
import { createTypedField, deepClone, DotNetType, Guid, unseal } from 'tessa/platform';
import { showDialog } from 'tessa/ui/uiHost';
import { MetadataStorage } from 'tessa';
import { CardTypeSection, systemKeyPrefix, TypeExtensionContext } from 'tessa/cards';
import { SchemeTableContentType } from 'tessa/scheme';
import { getTessaIcon } from 'common/utility';
import { CardRequest, CardResponse, CardService } from 'tessa/cards/service';
import { ValidationResultType } from 'tessa/platform/validation';
import { CardTableViewControlViewModel } from './tableViewExtension/cardTableViewControlViewModel';
import { CardTableViewRowData } from './tableViewExtension/cardTableViewRowData';
import { IPreviewerViewModel, PdfPreviewerViewModel } from 'tessa/ui/cards/controls/previewer';
import { reaction } from 'mobx';
import { DefaultFormTabWithTasksViewModel } from 'tessa/ui/cards/forms';
import { CustomStyle } from 'tessa/ui/cards/customElementStyle';
import { getFileViewExtensionInitializationStrategyHandlers } from './cardFiles/cardFilesExtensions';
import { CustomFilesViewCardControlInitializationStrategy } from './cardFiles/customFilesViewCardControlInitializationStrategy';
import { FilesViewControlContentItemsFactory } from './cardFiles/filesViewControlContentItemsFactory';

export class CarUIExtension extends CardUIExtension {
  private _disposes: Array<(() => void) | null> = [];
  private _syncPageEvents: Array<() => void> = [];

  public initializing(context: ICardUIExtensionContext): void {
    getFileViewExtensionInitializationStrategyHandlers(context.model)?.push(
      (ctx: TypeExtensionContext, _m: ICardModel) => {
        const settings = ctx.settings;
        const filesViewAlias = tryGetFromSettings<string>(settings, 'FilesViewAlias', '');
        return filesViewAlias === 'AllFilesControl'
          ? new CustomFilesViewCardControlInitializationStrategy(
              new FilesViewControlContentItemsFactory()
            )
          : null;
      }
    );
  }

  public initialized(context: ICardUIExtensionContext): void {
    if (context.card.typeId !== 'd0006e40-a342-4797-8d77-6501c4b7c4ac') {
      // CardTypeID
      return;
    }

    const cardModel = context.model;

    const imageFileControl = tryGetFromInfo<FileListViewModel | null>(
      cardModel.info,
      'ImageFilesControl',
      null
    );

    // показываем файлы только с категорией Image

    if (imageFileControl) {
      imageFileControl.removeFiles(
        file => !(file.model.category && file.model.category.caption === 'Image')
      );

      this._disposes.push(
        imageFileControl.containerFileAdding.addWithDispose(e => {
          if (!(e.file.category && e.file.category.caption === 'Image')) {
            e.cancel = true;
          }
        })
      );
    }

    // показываем файлы без категории Image
    const allFilesControl = tryGetFromInfo<FileListViewModel | null>(
      cardModel.info,
      'AllFilesControl',
      null
    );
    if (allFilesControl) {
      allFilesControl.removeFiles(
        file => !!file.model.category && file.model.category.caption === 'Image'
      );

      this._disposes.push(
        allFilesControl.containerFileAdding.addWithDispose(e => {
          if (e.file.category && e.file.category.caption === 'Image') {
            e.cancel = true;
          }
        })
      );
    }

    // запрещаем добавлять файлы с расширением .exe
    this._disposes.push(
      context.fileContainer.containerFileChanging.addWithDispose(async e => {
        const file = e.added;
        if (file && file.getExtension() === 'exe') {
          await showMessage("Can't add file: " + file.name);
          e.cancel = true;
        }
      })
    );

    const driverControl = context.model.controls.get('DriverName2') as AutoCompleteEntryViewModel;
    if (driverControl) {
      driverControl.openCardCommand.func = async () => {
        if (
          !driverControl.item ||
          // tslint:disable-next-line:triple-equals
          driverControl.item.reference == undefined
        ) {
          return;
        }

        const info = {};
        info['.autocomplete'] = driverControl.getRefInfo();

        await cardModel.executeInContext(async uiContext => {
          await showLoadingOverlay(async splashResolve => {
            await AdvancedCardDialogManager.instance.openCard({
              cardId: driverControl.item!.reference,
              context: uiContext,
              info,
              splashResolve
            });
          });
        });
      };
    }

    const ownersControl = context.model.controls.get('Owners') as AutoCompleteTableViewModel;
    if (ownersControl) {
      this._disposes.push(
        ownersControl.valueSet.addWithDispose(e => {
          const items = e.autocomplete.items as RowAutoCompleteItem[];
          const minOrder = Math.min(...items.map(x => x.order));
          const firstItem = items.find(x => x.order === minOrder);
          if (!firstItem) {
            return;
          }
          const displayText: string = firstItem.row.get('UserName');
          if (!displayText.endsWith(' (*)')) {
            firstItem.row.rawSet(
              'UserName',
              createTypedField(`${displayText} (*)`, DotNetType.String)
            );
          }
        })
      );
    }

    const carNameControl = context.model.controls.get('CarName');
    if (carNameControl) {
      carNameControl.hasActiveValidation = true;
      carNameControl.validationFunc = c => {
        if ((c as TextBoxViewModel).text === '42') {
          return "Can't enter magic number here";
        }

        return null;
      };
    }

    const preview1 = context.model.controls.get('Preview1') as FilePreviewViewModel;
    const preview2 = context.model.controls.get('Preview2') as FilePreviewViewModel;

    if (preview1 && preview2) {
      // если в каком-либо превью меняется вью-модель средства, необходимо заново их связать.
      const preview1ReactionDispose = reaction(
        () => preview1.fileControlManager.previewToolViewModel,
        viewModel1 => {
          this.setPageSyncEvents(viewModel1, preview2.fileControlManager.previewToolViewModel);
        }
      );

      const preview2ReactionDispose = reaction(
        () => preview2.fileControlManager.previewToolViewModel,
        viewModel2 => {
          this.setPageSyncEvents(preview1.fileControlManager.previewToolViewModel, viewModel2);
        }
      );

      this._disposes.push(preview1ReactionDispose, preview2ReactionDispose);

      this.setPageSyncEvents(
        preview1.fileControlManager.previewToolViewModel,
        preview2.fileControlManager.previewToolViewModel
      );
    }

    // Добавляем тестовую кнопку в тулбар.
    // Кнопка отображается в карточке, открытой во вкладке и в диалоге.
    // Если необходимо отображать кнопку, например, только во вкладке, то надо добавить условие:
    // !!context.dialogName
    if (context.toolbar) {
      const title = 'TestButton';
      context.toolbar.removeItemIfExists(title);
      context.toolbar.addItem(
        new CardToolbarAction({
          name: title,
          caption: 'Test Button',
          icon: getTessaIcon('Thin285'),
          command: () => {
            showMessage('Test button was pressed.');
          },
          order: -1 // Всегда выводим первой.
        })
      );

      context.toolbar.removeItemIfExists('PreviewInDialog');
      context.toolbar.addItem(
        new CardToolbarAction({
          name: 'PreviewInDialog',
          caption: cardModel.previewManager.previewInDialog
            ? 'Default Preview'
            : 'Preview In Dialog',
          icon: getTessaIcon('Thin8'),
          command: item => {
            const previewInDialog = !cardModel.previewManager.previewInDialog;
            cardModel.previewManager.previewInDialog = previewInDialog;
            item.caption = previewInDialog ? 'Default Preview' : 'Preview In Dialog';
          }
        })
      );
    }

    const get1CButton = context.model.controls.get('Get1CButton') as ButtonViewModel;
    if (get1CButton) {
      get1CButton.onClick = this.get1CfileAsync;
      get1CButton.isReadOnly = !context.model.fileContainer.permissions.canAdd;
    }

    // Использование апи выставления поворотов
    // this._disposes.push(
    //   preview1.fileControlManager.rotateChanged.addWithDispose(
    //     (args: { fileVersionId: string; page: number; angle: number }) => {
    //       const version = preview2.fileControlManager.fileVersion;
    //       if (!version) {
    //         return;
    //       }
    //       preview2.fileControlManager.setRotateAngle(
    //         version.id,
    //         preview2.fileControlManager.pageNumber,
    //         args.angle
    //       );
    //     }
    //   )!
    // );

    // Использование апи масштабирования
    // this._disposes.push(
    //   preview1.fileControlManager.scaleChanged.addWithDispose(
    //     (args: { fileVersionId: string; scale: ScaleOption; customScaleValue: number }) => {
    //       preview2.fileControlManager.setScale(args.scale, args.customScaleValue);
    //     }
    //   )!
    // );

    const datePickerViewModel = context.model.controls.get('ReleaseDate') as DateTimeViewModel;
    if (datePickerViewModel) {
      datePickerViewModel.calendarModification = async ctx => {
        const { days, modifiedDays } = ctx;

        days
          .filter(dayOfMonth => dayOfMonth.day() === 2)
          .map(day => ({
            day: day.format(),
            className: 'calendar-day-danger',
            classNameSelected: 'calendar-day-danger-selected'
          }))
          .forEach(day => modifiedDays.push(day));
        return modifiedDays;
      };
    }

    const showDialogTypeFormButton = cardModel.controls.get(
      'ShowDialogTypeForm'
    ) as ButtonViewModel;
    if (showDialogTypeFormButton) {
      showDialogTypeFormButton.onClick = async () => {
        const virtualSchemeDialog = MetadataStorage.instance.cardMetadata.getCardTypeById(
          'b2b4d2c2-8f92-4262-9951-fe1a64bf9b30'
        ); // VirtualSchemeCardTypeID
        const virtualSchemeDialogClone = deepClone(virtualSchemeDialog)!;

        const generalSection = new CardTypeSection();
        generalSection.id = Guid.newGuid();
        generalSection.name = 'Instances';
        generalSection.description = 'Fake Instances';
        generalSection.tableType = SchemeTableContentType.Entries;
        const presenter = new VirtualSchemePresenter(
          unseal<CardTypeSection[]>(virtualSchemeDialogClone.cardTypeSections),
          [generalSection]
        );
        const form = await presenter.createForm();
        if (!form) {
          showError('Dialog type "VirtualScheme" not found.');
          return;
        }

        await showDialog(
          form,
          null,
          [
            UIButton.create({
              caption: '$UI_Common_Close',
              buttonAction: btn => btn.close()
            })
          ],
          {
            hideTopCloseIcon: true
          }
        );
      };
    }

    // в контроле-таблице "Список акций"
    const grid = context.model.controls.get('ShareList') as GridViewModel;
    if (grid) {
      // добавляем валидацию при сохранении редактируемой строки
      this._disposes.push(
        grid.rowValidating.addWithDispose(e => {
          if (!e.row.get('Name') || e.row.get('Name') === '') {
            e.validationResult.add(
              ValidationResultType.Error,
              "Share's name is empty (from RowValidating)."
            );
          }
        }),
        // нажатие Ctrl+Enter показывает окно для выбранной строки в фокусе
        grid.keyDown.addWithDispose(e => {
          const code = e.event.keyCode || e.event.charCode;
          if (code === 13 && e.event.ctrlKey) {
            e.event.stopPropagation();
            e.event.preventDefault();

            const gridViewModel = e.control as GridViewModel;

            if (gridViewModel.selectedRow) {
              showMessage(`Share name is ${gridViewModel.selectedRow.row.get('Name')}`);
            }
          }
        }),
        // при открытии окна добавления/редактирования строки добавляем горячую клавишу
        grid.keyDown.addWithDispose(e => {
          const code = e.event.keyCode || e.event.charCode;
          if (code === 116) {
            // f5
            e.event.stopPropagation();
            e.event.preventDefault();
            showMessage('F5 key is pressed');
          }
        }),
        // при клике по ячейке из первой колонки "Акция" в открытом окне будет поставлен фокус
        // на первый контрол типа "Строка", в котором также будет выделен весь текст
        grid.rowInitialized.addWithDispose(e => {
          if (e.action === GridRowAction.Opening && e.columnIndex === 0) {
            const textBox = e.rowModel?.controlsBag.find(
              x => x instanceof TextBoxViewModel
            ) as TextBoxViewModel;
            if (textBox) {
              textBox.focus();
              textBox.selectAll();
            }
          }
        })
      );

      // контекстное меню таблицы, зависит от кликнутой ячейки
      grid.contextMenuGenerators.push(ctx => {
        const text = `Name=${ctx.row.row.get('Name')}, Count=${
          ctx.control.selectedRows.length
        }, Cell="${ctx.cell?.value}"`;

        ctx.menuActions.push(
          MenuAction.create({
            name: 'Name',
            caption: text,
            action: () => {
              showMessage(`Share name is ${ctx.row.row.get('Name')}`);
            }
          }),
          MenuAction.create({
            name: 'EditRow',
            caption: 'Edit row',
            icon: getTessaIcon('Thin2'),
            action: async () => {
              await grid.editRow(ctx.row, ctx.columnIndex);
            }
          })
        );
      });
    }

    const shareListView = context.model.controls.get(
      'ShareListView'
    ) as CardTableViewControlViewModel;
    if (shareListView) {
      // добавляем валидацию при сохранении редактируемой строки
      this._disposes.push(
        shareListView.rowValidating.addWithDispose(e => {
          if (!e.row.get('Name')) {
            e.validationResult.add(
              ValidationResultType.Error,
              "Share's name is empty (from RowValidating)."
            );
          }
        }),
        // при клике по ячейке из первой колонки "Акция" в открытом окне будет поставлен фокус
        // на первый контрол типа "Строка", в котором также будет выделен весь текст
        shareListView.rowInitialized.addWithDispose(e => {
          // e.Cell всегда null, если ViewSelectionMode.Row
          if (e.action === GridRowAction.Opening && e.cell?.column.columnName === '0') {
            const textBox = e.rowModel?.controlsBag.find(
              x => x instanceof TextBoxViewModel
            ) as TextBoxViewModel;
            if (textBox) {
              textBox.focus();
              textBox.selectAll();
            }
          }
        })
      );

      // контекстное меню таблицы, зависит от кликнутой ячейки
      if (shareListView.table) {
        this._disposes.push(
          // при открытии окна добавления/редактирования строки добавляем горячую клавишу
          shareListView.table.keyDown.addWithDispose(e => {
            const code = e.event.keyCode || e.event.charCode;
            if (code === 116) {
              // f5
              e.event.stopPropagation();
              e.event.preventDefault();
              showMessage('F5 key is pressed');
            }
          }),
          // нажатие Ctrl+Enter показывает окно для выбранной строки в фокусе
          shareListView.table.keyDown.addWithDispose(e => {
            const code = e.event.keyCode || e.event.charCode;
            if (code === 13 && e.event.ctrlKey) {
              const selectedRow = e.control.viewComponent.selectedRow;

              e.event.stopPropagation();
              e.event.preventDefault();
              showMessage(`Share name is ${selectedRow?.get('Name')}`);
            }
          })
        );

        shareListView.table?.rowContextMenuGenerators?.push(ctx => {
          const data = ctx.row.data as CardTableViewRowData;
          // e.Cell всегда null, если ViewSelectionMode.Row
          const text = `Name=${data.get('0')}, Count=${
            ctx.tableGrid.viewComponent.selectedRows?.length
          }, Cell="${ctx.cell?.value ?? ctx.cell?.convertedValue}"`;

          ctx.menuActions.push(
            MenuAction.create({
              name: 'Name',
              caption: text,
              action: () => {
                showMessage(`Share name is ${data.get('0')}`);
              }
            }),
            MenuAction.create({
              name: 'EditRow',
              caption: 'Edit row',
              icon: getTessaIcon('Thin2'),
              action: async () => {
                await shareListView.editRow(ctx.row);
              }
            })
          );
        });
      }
    }

    // Изменяем цвет фона кнопок завершения заданий
    if (context.model.mainForm instanceof DefaultFormTabWithTasksViewModel) {
      for (const task of context.model.mainForm.tasks) {
        task.modifyWorkspace(e => {
          const workspace = e.task.taskWorkspace;
          // Окрашиваем кнопку "ещё" в красный цвет
          workspace.moreAction.background = 'red';
          for (const action of workspace.actions) {
            if (action.completionOption) {
              // Окрашиваем видимые кнопки завершения задания в тёмно синий цвет
              action.background = 'darkblue';
            }
          }
          for (const action of workspace.additionalActions) {
            // Окрашиваем кнопки дополнительных действий в контекстном меню в синий цвет
            action.background = 'skyblue';
          }
        });
      }
    }

    // Кастомизация стилей блока MainInfo
    const mainInfoBlock = context.model.blocks.get('MainInfo');
    if (mainInfoBlock) {
      mainInfoBlock.customStyle = CustomStyle.createCustomBlockStyle({
        captionFontSize: '1.2rem',
        captionIcon: getTessaIcon('Thin145'),
        captionFontColor: 'darkblue',
        captionHoverBackgroundColor: 'rgba(19, 174, 104, .30)',
        captionHoverBorderColor: 'rgba(19, 174, 104)',
        mainBorderColor: 'rgba(19, 174, 104)',
        mainBackgroundColor: 'rgba(216, 242, 227)',
        mainPadding: '10px',
        controlsPadding: '10px'
      });
    }
  }

  public finalized(): void {
    for (const func of this._disposes) {
      if (func) {
        func();
      }
    }
    this._disposes.length = 0;

    this.clearSyncPagesEvents();
  }

  public saving(context: ICardUIExtensionContext): void {
    // Добавим ошибку валидации, если найден файл с определённым именем.
    if (context.fileContainer.files.find(x => x.name === 'show error.txt')) {
      context.validationResult.add(
        ValidationResultType.Error,
        'File "show error.txt" was added, can\'t save the card.'
      );

      return;
    }

    // Удаляем файл с определённым именем.
    const fileToRemove = context.fileContainer.files.find(x => x.name === 'remove me.txt');
    if (fileToRemove) {
      fileToRemove.changeName('file was removed.txt');
    }
  }

  private async get1CfileAsync(): Promise<void> {
    const mainEditor = UIContext.current?.cardEditor;
    const mainModel = mainEditor?.cardModel;
    if (!mainModel) {
      return;
    }
    const dialogResult = await createDialogForm(
      'Car1CDialog',
      undefined,
      FormCreationOptions.AlwaysCreateTabbedForm,
      undefined
    );
    if (!dialogResult) {
      return;
    }

    const [form, model] = dialogResult;
    model.mainForm = form;
    const cardEditor = createCardEditorModel();
    cardEditor.cardModel = model;
    await AdvancedCardDialogManager.instance.showCard({
      editor: cardEditor,
      prepareEditorAction: editor => {
        editor.statusBarIsVisible = false;

        editor.toolbar.addItem(
          new CardToolbarAction({
            name: 'Ok',
            caption: '$UI_Common_OK',
            icon: 'ta icon-Int426',
            command: async _ => {
              const request = new CardRequest();
              const requestInfo = request.info;
              const testCardSection = model.card.sections.get('TEST_CarMainInfoDialog');

              request.requestType = '86333B21-A1C5-4698-B023-B427C8BCCF94';
              requestInfo['Name'] = createTypedField(
                testCardSection?.fields.get('Name'),
                DotNetType.String
              );
              requestInfo['Driver'] = createTypedField(
                testCardSection?.fields.get('DriverName'),
                DotNetType.String
              );

              // показываем сплэш и задаём блокирующую операцию для карточки:
              // пользователь не сможет закрыть вкладку или отрефрешить карточку, пока она не закончится
              let response: CardResponse = new CardResponse();
              await LoadingOverlay.instance.show(async () => {
                await cardEditor.setOperationInProgress(async () => {
                  setTimeout(() => '42', 2000);
                  response = await CardService.instance.request(request);
                });
              });

              const result = response.validationResult.build();

              await showNotEmpty(result);

              if (!result.isSuccessful) {
                return;
              }

              const content = tryGetFromInfo<string>(response.info, 'Xml');
              const fileName = '1C.xml';
              const newFile = new File([content], fileName);

              const file = mainModel.fileContainer.files.find(f => f.name === fileName);
              if (file) {
                await mainModel.previewManager.resetIfInPreview(file);
                file.replace(new File([content], fileName), true);
              } else {
                await mainModel.fileContainer.addFile(
                  mainModel.fileContainer.createFile(newFile),
                  false,
                  true
                );
              }
              await editor.close();
            },
            order: -1
          })
        );

        editor.toolbar.addItem(
          new CardToolbarAction({
            name: 'Cancel',
            caption: '$UI_Common_Cancel',
            icon: 'ta icon-Int626',
            order: 40,
            toolTip: '$UI_Common_Cancel',
            command: async _ => {
              await editor.close();
            }
          })
        );

        editor.context.info[systemKeyPrefix + 'DialogClosingAction'] = () => Promise.resolve(false);

        return true;
      }
    });
  }

  private setPageSyncEvents(
    previewArea1: IPreviewerViewModel | null,
    previewArea2: IPreviewerViewModel | null
  ): void {
    this.clearSyncPagesEvents();

    if (
      previewArea1 &&
      previewArea2 &&
      previewArea1.type === PdfPreviewerViewModel.type &&
      previewArea2.type === PdfPreviewerViewModel.type
    ) {
      const pdfControl1 = previewArea1 as PdfPreviewerViewModel;
      const pdfControl2 = previewArea2 as PdfPreviewerViewModel;

      // на изменение страницы в 1 превью, меняем на такую же страницу во 2 превью.
      const control1IndexReaction = reaction(
        () => pdfControl1.pageIndex,
        index => {
          pdfControl2.pageIndex = index;
        }
      );

      this._syncPageEvents.push(control1IndexReaction);
    }
  }

  private clearSyncPagesEvents(): void {
    for (const f of this._syncPageEvents) {
      f();
    }
    this._syncPageEvents.length = 0;
  }
}
