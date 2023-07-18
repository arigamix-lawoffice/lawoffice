import { reaction, Lambda, runInAction } from 'mobx';
import type { FileControlCreationParams } from './fileControlCreationParams';
import { ShowContextMenuButtonViewModel } from './showContextMenuButtonViewModel';
import { TableFileRowViewModel } from './tableFileRowViewModel';
import { CardUIExtension, FormCreationContext, ICardModel, IControlViewModel } from 'tessa/ui/cards';
import { ViewService, TessaViewRequest, ITessaViewResult, RequestParameterBuilder } from 'tessa/views';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { CardInstanceType, CardFileType, ViewControlControlType } from 'tessa/cards';
import { hasNotFlag, Visibility } from 'tessa/platform';
import { CardTypeFlags, CardTypeEntryControl } from 'tessa/cards/types';
import { userSession } from 'common/utility';
import { LocalizationManager } from 'tessa/localization';
import {
  ViewControlViewModel,
  FileListViewModel,
  FileSortingDirection,
  IViewControlInitializationStrategy,
  addRequestParameters,
  FileGroupingFiltering,
  ViewControlRefreshButtonViewModel,
  FileViewModel,
  getSelectedFilesPreviewMessage,
  ViewControlButtonPanelViewModel
} from 'tessa/ui/cards/controls';
import { showError, showFileDialog, tryGetFromInfo, UIButton } from 'tessa/ui';
import { FileCategory, allowPreviewExtensions } from 'tessa/files';
import { ViewParameterMetadata, equalsCriteriaOperator } from 'tessa/views/metadata';
import { Keyboard } from 'tessa';
import { SchemeType } from 'tessa/scheme';
import { FileSelectContext } from 'tessa/ui/files';
import { FileHelper } from 'src/law/helpers/fileHelper';

export class FilesViewGeneratorBaseUIExtension extends CardUIExtension {
  readonly _dispose: Array<Function | Lambda | null> = [];

  finalized(): void {
    for (const dispose of this._dispose) {
      if (dispose) {
        dispose();
      }
    }
    this._dispose.length = 0;
  }

  protected async initializeFileControl(model: ICardModel, viewControlName: string, creationParams: FileControlCreationParams) {
    const cardMetadata = model.generalMetadata;
    const cardTypes = cardMetadata.cardTypes.filter(
      p =>
        p.instanceType === CardInstanceType.File &&
        hasNotFlag(p.flags, CardTypeFlags.Hidden) &&
        (userSession.isAdmin || hasNotFlag(p.flags, CardTypeFlags.Administrative))
    );

    const fileTypes = cardTypes
      .map(fileType => new CardFileType(fileType))
      // tslint:disable-next-line:max-line-length
      .map(x => {
        return {
          item: x,
          localizedCaption: LocalizationManager.instance.localize(x.caption)
        };
      })
      .sort((a, b) => {
        if (a.localizedCaption < b.localizedCaption) return -1;
        else if (a.localizedCaption > b.localizedCaption) return 1;
        return 0;
      })
      .map(x => x.item);

    model.controlInitializers.push(control => {
      if (control instanceof ViewControlViewModel) {
        if (control.name !== viewControlName) {
          return;
        }

        if (FormCreationContext.current?.fileControls.some(x => x.name === viewControlName)) {
          showError(`Multiple FileViewControlViewModel with Name='${viewControlName}' was found on the form.`);
          return;
        }

        const categoriesView = ViewService.instance.getByName(creationParams.categoriesViewAlias);
        if (!categoriesView) {
          showError(`Categories View:'${creationParams.categoriesViewAlias}' isn't found.`);
          return;
        }

        const controlType = new CardTypeEntryControl();
        controlType.name = viewControlName;
        controlType.type = ViewControlControlType;
        const fileControl = new FileListViewModel(
          controlType,
          model,
          null,
          FileSortingDirection.Ascending,
          null,
          false,
          creationParams.isCategoriesEnabled,
          creationParams.isManualCategoriesCreationDisabled,
          creationParams.isNullCategoryCreationDisabled,
          false,
          creationParams.isIgnoreExistingCategories,
          categoriesView,
          null,
          creationParams.previewControlName,
          fileTypes
        );

        fileControl.categoryFilter = async context => {
          const categories = context.categories;
          if (!categoriesView) {
            return categories;
          }

          const request = new TessaViewRequest(categoriesView.metadata);

          // Добавляем параметры из маппинга
          const parameters = addRequestParameters(creationParams.categoriesViewMapping, model, categoriesView);
          if (parameters) {
            request.values = parameters;
          }

          let result!: ITessaViewResult;
          await model.executeInContext(async () => (result = await categoriesView.getData(request)));

          const rows = result.rows || [];

          // категории из представления в порядке, в котором их
          // вернуло представление (кроме строчек null)
          const viewCategories = rows.filter(x => x.length > 0 && !!x[0]).map(x => new FileCategory(x[0], x[1]));

          // tslint:disable-next-line: triple-equals
          const notNullCategories = categories.filter(x => x != null) as FileCategory[];
          // категории из представления плюс вручную добавленные или другие
          // присутствующие в карточке категории, кроме null
          const mainCategories = viewCategories
            .concat(...notNullCategories)
            // tslint:disable-next-line:triple-equals
            .filter(x => x != null);

          const finalCategories: (FileCategory | null)[] = [];
          // делаем distinct
          mainCategories.forEach(category => {
            if (finalCategories.every(x => !FileCategory.equals(x, category))) {
              finalCategories.push(category);
            }
          });

          // добавляем наверх "Без категории" и возвращаем результирующий список
          finalCategories.splice(0, 0, null);

          return finalCategories;
        };

        fileControl.initialize();
        model.info[viewControlName] = fileControl;
        FormCreationContext.current?.registerFileControl(fileControl);
        (model.controlsBag as Array<IControlViewModel>).push(fileControl);
        this._dispose.push(() => fileControl.unload(new ValidationResultBuilder()));
      }
    });
  }

  protected async attachViewToFileControl(
    cardModel: ICardModel,
    viewControlName: string,
    initializationStrategy?: IViewControlInitializationStrategy,
    viewModifierAction?: (viewControl: ViewControlViewModel) => void
  ): Promise<FileListViewModel | null> {
    const viewControlViewModel = cardModel.controls.get(viewControlName) as ViewControlViewModel;
    if (!viewControlViewModel) {
      return null;
    }

    // viewControlViewModel.createRowFunc = createRowFunc;
    const fileControl = tryGetFromInfo<FileListViewModel | null>(cardModel.info, viewControlName, null);
    if (!fileControl) {
      throw new Error(`File control not found.`);
    }

    if (initializationStrategy) {
      viewControlViewModel.initializeStrategy(initializationStrategy, true);
      if (viewModifierAction) {
        viewModifierAction(viewControlViewModel);
      }
      if (viewControlViewModel.table) {
        viewControlViewModel.table.createRowAction = opt => new TableFileRowViewModel(opt, fileControl);
      }
      viewControlViewModel.initialize();
      viewControlViewModel.initialRefresh();
    }

    this._dispose.push(
      reaction(
        () => fileControl.files,
        () => viewControlViewModel.refreshWithDelay(150),
        {
          equals: () => false
        }
      )
    );

    this.initializeSelection(viewControlViewModel, fileControl);
    this.initializeGrouping(viewControlViewModel, fileControl);
    this.initializeFiltering(viewControlViewModel, fileControl);
    this.initializeClickCommands(viewControlViewModel, fileControl);
    this.initializeMenuButton(viewControlViewModel, fileControl);
    // this.initializeKeyDownHandlers(viewControlViewModel, fileControl);
    this.initializeContextMenu(viewControlViewModel, fileControl);

    return fileControl;
  }

  protected initializeSelection(viewControl: ViewControlViewModel, fileControl: FileListViewModel): void {
    this._dispose.push(
      reaction(
        () => viewControl.selectedRows,
        () => {
          const selectedFiles: Array<FileViewModel> = viewControl.selectedRows?.map(x => x.get('FileViewModel')) ?? [];
          const notSelectedFiles: Array<FileViewModel> = fileControl.files.filter(x => !selectedFiles.includes(x));

          for (const file of selectedFiles) {
            file.selected = true;
          }
          for (const file of notSelectedFiles) {
            file.selected = false;
          }

          const selectedRow = viewControl.selectedRow;
          const selectedFile: FileViewModel = selectedRow?.get('FileViewModel');
          const multiSelect =
            Keyboard.instance.ctrl ||
            Keyboard.instance.shift ||
            viewControl.multiSelectEnabled ||
            (viewControl.selectedRows && viewControl.selectedRows.length > 1);
          const preview = fileControl.manager;

          if (preview) {
            if (selectedFiles.length === 0) {
              preview.reset();
              return;
            }
            /*
              отключать предпросмотр,
              если файл был загружен в файловом контролле,
              но не сохранен в карточке.
             */
            if (selectedFile.isFileStateCreated() && !allowPreviewExtensions(selectedFile.model)) {
              preview.resetPreview();
              return;
            }

            if (!multiSelect && selectedFile) {
              if (preview.inProgress) {
                // TODO ViewControl
                // uncheck row ?
                return;
              }

              preview.showPreview(selectedFile.model);
              return;
            }

            if (multiSelect) {
              preview.resetPreview();
              preview.message = getSelectedFilesPreviewMessage(selectedFiles);
              return;
            }
          }
        },
        {
          equals: () => false
        }
      )
    );
  }

  protected initializeGrouping(viewControl: ViewControlViewModel, fileControl: FileListViewModel): void {
    const table = viewControl.table;
    if (!table) {
      return;
    }

    const groupCaptionColumn = table.columns.find(x => x.columnName === 'GroupCaption');
    if (groupCaptionColumn) {
      groupCaptionColumn.visibility = false;
    }

    this._dispose.push(
      reaction(
        () => fileControl.selectedGrouping,
        grouping => {
          if (!grouping) {
            table.groupingColumn = null;
            const categoryColumn = table.columns.find(x => x.columnName === 'CategoryCaption');
            if (categoryColumn) {
              categoryColumn.visibility = true;
            }
          } else {
            if (grouping.name === 'Category') {
              const categoryColumn = table.columns.find(x => x.columnName === 'CategoryCaption');
              if (categoryColumn) {
                categoryColumn.visibility = false;
              }
            }
            const groupCaptionColumn = table.columns.find(x => x.columnName === 'GroupCaption');
            if (groupCaptionColumn) {
              table.groupingColumn = groupCaptionColumn.metadata;
            }
          }

          const groupCaptionColumn = table.columns.find(x => x.columnName === 'GroupCaption');
          if (groupCaptionColumn) {
            groupCaptionColumn.visibility = false;
          }
        }
      )
    );
  }

  protected initializeFiltering(viewControl: ViewControlViewModel, fileControl: FileListViewModel): void {
    this._dispose.push(
      reaction(
        () => fileControl.selectedFiltering,
        filtering => {
          if (viewControl.isDataLoading) {
            return;
          }

          const previousFilteringParameter = viewControl.parameters.parameters.find(x => x.metadata?.alias === 'FilterParameter');
          if (previousFilteringParameter) {
            viewControl.parameters.removeParameters(previousFilteringParameter);
          }
          if (filtering && filtering instanceof FileGroupingFiltering) {
            const parameterMetadata = new ViewParameterMetadata();
            parameterMetadata.caption = filtering.grouping.caption;
            parameterMetadata.alias = 'FilterParameter';
            parameterMetadata.schemeType = SchemeType.String;
            const newFilteringParameter = new RequestParameterBuilder()
              .withMetadata(parameterMetadata)
              .addCriteria(equalsCriteriaOperator(), filtering.caption, filtering.caption)
              .asRequestParameter();
            viewControl.parameters.addParameters(newFilteringParameter);
          }
          viewControl.refresh();
        }
      )
    );
  }

  protected initializeClickCommands(viewControl: ViewControlViewModel, _fileControl: FileListViewModel): void {
    viewControl.doubleClickAction = async info => {
      const table = viewControl.table;
      if (!table) {
        return;
      }

      const row = table.rows.find(x => x.data === info.selectedObject);
      if (!row) {
        return;
      }

      const actions = row.getContextMenu();
      const openForRead = actions.find(x => x.name === 'OpenForRead');
      if (openForRead) {
        openForRead.executeAction();
      }

      if (Keyboard.instance.alt) {
        const openForEdit = actions.find(x => x.name === 'OpenForEdit');
        if (openForEdit) {
          openForEdit.executeAction();
        }
      }
    };
  }

  protected initializeMenuButton(viewControl: ViewControlViewModel, fileControl: FileListViewModel): void {
    const refreshButtonIndex = viewControl.bottomItems.findIndex(x => x instanceof ViewControlRefreshButtonViewModel);

    if (refreshButtonIndex !== -1) {
      viewControl.bottomItems.splice(refreshButtonIndex, 1);
    }

    const bottomPanelButtons = new ViewControlButtonPanelViewModel(viewControl);
    viewControl.bottomItems.splice(0, 0, bottomPanelButtons);

    const uploadButton = fileControl.getControlActions().find(button => button.name === 'Upload');
    const permissions = fileControl.fileContainer.permissions;

    if (uploadButton && !uploadButton.isCollapsed) {
      const uploadFileButton = UIButton.create({
        icon: 'icon-grid-plus',
        name: uploadButton.name,
        className: 'button-plain files-control-button',
        buttonAction: () => this.uploadAction(fileControl),
        tooltip: '$UI_Controls_FilesControl_UploadFiles',
        visibility: permissions.canAdd ? Visibility.Visible : Visibility.Collapsed
      });

      runInAction(() => {
        bottomPanelButtons.buttons.push(uploadFileButton);
      });
    }

    const addMenuButton = new ShowContextMenuButtonViewModel(viewControl, fileControl);
    viewControl.bottomItems.push(addMenuButton);
  }

  protected initializeContextMenu(viewControl: ViewControlViewModel, fileControl: FileListViewModel): void {
    if (viewControl.table) {
      viewControl.table.rowContextMenuGenerators.push(ctx => {
        const row = ctx.row as TableFileRowViewModel;

        // если строка, по которой кликнули выделена, то мы учитываем все выделенные строки
        const withAnotherSelectedFiles = row.isSelected;

        const actions = fileControl.getFileActions(row.fileViewModel, withAnotherSelectedFiles);
        const replaceAction = actions.find(a => a.name === 'Replace');
        if (replaceAction) {
          replaceAction.isCollapsed = true;
        }

        const convertAction = actions.find(a => a.name === 'ConvertToPDFWithReplace');
        if (convertAction) {
          convertAction.isCollapsed = true;
        }

        ctx.menuActions.push(...actions);
      });
    }
  }

  private async uploadAction(fileControl: FileListViewModel) {
    const modifyFileSelect = fileControl.modifyFileSelect;
    let accept: string | undefined;
    if (modifyFileSelect) {
      const context = new FileSelectContext(null);
      modifyFileSelect(context);
      accept = context.selectFileDialogAccept;
    }

    const contents = await showFileDialog(true, accept);
    if (contents.length === 0) {
      return;
    }

    await FileHelper.addFilesAsync(fileControl, contents);
  }
}
