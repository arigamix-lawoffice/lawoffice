import { FileControlExtension, IFileControl, IFileControlExtensionContext } from 'tessa/ui/files';
import {
  MenuAction,
  SeparatorMenuAction,
  showError,
  showLoadingOverlay,
  showNotEmpty,
  tryGetFromInfo,
  tryGetFromSettings,
  UIButton
} from 'tessa/ui';
import { LocalizationManager } from 'tessa/localization';
import { OnlyOfficeApiSingleton } from './onlyOfficeApiSingleton';
import { ValidationResult } from 'tessa/platform/validation';
import { FileContainer } from 'tessa/files';
import { Visibility } from 'tessa/platform';
import { runInAction } from 'mobx';
import { CardUIExtension, ICardModel, ICardUIExtensionContext } from 'tessa/ui/cards';
import { CardTypeExtensionTypes, executeExtensions, TypeExtensionContext } from 'tessa/cards';
import { DefaultFormTabWithTasksViewModel } from 'tessa/ui/cards/forms';
import {
  FileListViewModel,
  FileTagViewModel,
  ViewControlButtonPanelViewModel,
  ViewControlViewModel
} from 'tessa/ui/cards/controls';

// TODO: tag should have feature to store description to show
const OONamesKey = '.coeditnames';
// const OODateKey = '.coeditdate';

const withUiHandler = async (
  action: () => Promise<{ validation: ValidationResult }>
): Promise<void> => {
  try {
    const { validation } = await showLoadingOverlay(action);
    await showNotEmpty(validation);
  } catch (e) {
    await showError(e.message);
  }
};

/**
 * Представляет собой расширение, которое добавляет возможность создания файлов по шаблону.
 */
export class OnlyOfficeFileControlExtension extends FileControlExtension {
  public initializing(context: IFileControlExtensionContext): void {
    const control = context.control;
    const container = control.fileContainer;
    const newFileNameCaption = LocalizationManager.instance.localize(
      '$OnlyOffice_NewDocumentCaption'
    );
    const isHidden = !control.fileContainer.permissions.canAdd;

    const createEmptyFileGroupButton = UIButton.create({
      icon: 'ta icon-thin-080',
      name: 'CreateEmptyFileGroup',
      className: `button-plain files-control-button`,
      tooltip: '$OnlyOffice_CreateEmptyFile',
      child: [
        UIButton.create({
          name: 'CreateEmptyWordFile',
          caption: 'Word',
          buttonAction: () =>
            OnlyOfficeFileControlExtension.createEmptyWordFileAction(
              control,
              container,
              newFileNameCaption
            )
        }),
        UIButton.create({
          name: 'CreateEmptyExcelFile',
          caption: 'Excel',
          buttonAction: () =>
            OnlyOfficeFileControlExtension.createEmptyExcelFileAction(
              control,
              container,
              newFileNameCaption
            )
        }),
        UIButton.create({
          name: 'CreateEmptyPowerPointFile',
          caption: 'PowerPoint',
          buttonAction: () =>
            OnlyOfficeFileControlExtension.createEmptyPowerPointFileAction(
              control,
              container,
              newFileNameCaption
            )
        })
      ],
      visibility: isHidden ? Visibility.Collapsed : Visibility.Visible
    });

    runInAction(() => {
      control.fileControlButtons.push(createEmptyFileGroupButton);
    });

    for (const file of control.files) {
      const cardFile = container.files.find(x => x.id === file.id);
      const coeditNames = tryGetFromInfo<string>(cardFile?.info, OONamesKey);
      if (coeditNames) {
        file.tag = new FileTagViewModel('ta icon-thin-196', 'rgba(0, 176, 0, 0.19)');
      }
    }
  }

  public openingMenu(context: IFileControlExtensionContext): void {
    const control = context.control;
    const container = control.fileContainer;
    const newFileNameCaption = LocalizationManager.instance.localize(
      '$OnlyOffice_NewDocumentCaption'
    );

    const createWordFileAction = new MenuAction(
      'CreateEmptyWordFile',
      'Word',
      'ta icon-thin-069',
      () =>
        OnlyOfficeFileControlExtension.createEmptyWordFileAction(
          control,
          container,
          newFileNameCaption
        )
    );

    const createExcelFileAction = new MenuAction(
      'CreateEmptyExcelFile',
      'Excel',
      'ta icon-thin-085',
      () =>
        OnlyOfficeFileControlExtension.createEmptyExcelFileAction(
          control,
          container,
          newFileNameCaption
        )
    );

    const createPowerPointFileAction = new MenuAction(
      'CreateEmptyPowerPointFile',
      'PowerPoint',
      'ta icon-thin-073',
      () =>
        OnlyOfficeFileControlExtension.createEmptyPowerPointFileAction(
          control,
          container,
          newFileNameCaption
        )
    );

    const createEmptyFileGroup = new MenuAction(
      'CreateEmptyFileGroup',
      '$OnlyOffice_CreateEmptyFile',
      'ta icon-thin-080',
      null,
      [createWordFileAction, createExcelFileAction, createPowerPointFileAction],
      !context.control.fileContainer.permissions.canAdd
    );

    // ищем пункт "Загрузить", после него ищем ближайший разделитель, и перед ним вставляем группу "Создать файл"
    let uploadActionIndex = context.actions.findIndex(a => a.name === 'Upload');
    if (uploadActionIndex !== -1) {
      const separatorIndex = context.actions
        .slice(uploadActionIndex)
        .findIndex(a => a instanceof SeparatorMenuAction);
      if (separatorIndex !== -1) {
        uploadActionIndex = uploadActionIndex + separatorIndex - 1;
      }
    }

    const insertToIndex = uploadActionIndex !== -1 ? uploadActionIndex + 1 : context.actions.length;
    context.actions.splice(insertToIndex, 0, createEmptyFileGroup);
  }

  public static async createEmptyWordFileAction(
    control: IFileControl,
    container: FileContainer,
    newFileNameCaption: string
  ): Promise<void> {
    await withUiHandler(() =>
      OnlyOfficeApiSingleton.instance.createTemplateFile(
        control,
        container,
        'empty.docx',
        newFileNameCaption + '.docx'
      )
    );
  }

  public static async createEmptyExcelFileAction(
    control: IFileControl,
    container: FileContainer,
    newFileNameCaption: string
  ): Promise<void> {
    await withUiHandler(() =>
      OnlyOfficeApiSingleton.instance.createTemplateFile(
        control,
        container,
        'empty.xlsx',
        newFileNameCaption + '.xlsx'
      )
    );
  }

  public static async createEmptyPowerPointFileAction(
    control: IFileControl,
    container: FileContainer,
    newFileNameCaption: string
  ): Promise<void> {
    await withUiHandler(() =>
      OnlyOfficeApiSingleton.instance.createTemplateFile(
        control,
        container,
        'empty.pptx',
        newFileNameCaption + '.pptx'
      )
    );
  }

  public shouldExecute(): boolean {
    return OnlyOfficeApiSingleton.isAvailable;
  }
}

// В данном расширении добавляются кнопки onlyOffice в контролы представления с расширением на файлы
export class OnlyOfficeViewFileControlExtension extends CardUIExtension {
  public async initialized(context: ICardUIExtensionContext): Promise<void> {
    const result = await executeExtensions(
      CardTypeExtensionTypes.initializeFilesView,
      context.card,
      context.model.generalMetadata,
      this.executeInitializedAction,
      context
    );

    context.validationResult.add(result);
  }

  private executeInitializedAction = async (context: TypeExtensionContext) => {
    const uiContext = context.externalContext as ICardUIExtensionContext;
    const settings = context.settings;
    const filesViewAlias = tryGetFromSettings<string>(settings, 'FilesViewAlias', '');
    if (!filesViewAlias) {
      return;
    }
    if (!context.cardTask) {
      this.initializeViewControl(uiContext.model, filesViewAlias);
    } else {
      const model = uiContext.model;
      const tasks = (model.mainForm as DefaultFormTabWithTasksViewModel).tasks;
      if (!tasks) {
        return;
      }
      const task = tasks.find(x => x.taskModel.cardTask === context.cardTask);
      if (task) {
        task.modifyWorkspace(async () => {
          this.initializeViewControl(task.taskModel, filesViewAlias);
        });
      }
    }
  };

  private initializeViewControl(cardModel: ICardModel, viewControlAlias: string) {
    const viewControlViewModel = cardModel.controls.get(viewControlAlias) as ViewControlViewModel;
    if (!viewControlViewModel) {
      return;
    }

    const fileControl = tryGetFromInfo<FileListViewModel | null>(
      cardModel.info,
      viewControlAlias,
      null
    );
    if (!fileControl) {
      return;
    }

    const container = fileControl.fileContainer;
    const permissions = fileControl.fileContainer.permissions;
    const newFileNameCaption = LocalizationManager.instance.localize(
      '$OnlyOffice_NewDocumentCaption'
    );

    const createEmptyFileGroupButton = UIButton.create({
      icon: 'ta icon-thin-080',
      name: 'CreateEmptyFileGroup',
      className: `button-plain files-control-button`,
      tooltip: '$OnlyOffice_CreateEmptyFile',
      child: [
        UIButton.create({
          name: 'CreateEmptyWordFile',
          caption: 'Word',
          buttonAction: () =>
            OnlyOfficeFileControlExtension.createEmptyWordFileAction(
              fileControl,
              container,
              newFileNameCaption
            )
        }),
        UIButton.create({
          name: 'CreateEmptyExcelFile',
          caption: 'Excel',
          buttonAction: () =>
            OnlyOfficeFileControlExtension.createEmptyExcelFileAction(
              fileControl,
              container,
              newFileNameCaption
            )
        }),
        UIButton.create({
          name: 'CreateEmptyPowerPointFile',
          caption: 'PowerPoint',
          buttonAction: () =>
            OnlyOfficeFileControlExtension.createEmptyPowerPointFileAction(
              fileControl,
              container,
              newFileNameCaption
            )
        })
      ],
      visibility:
        permissions.canAdd && OnlyOfficeApiSingleton.isAvailable
          ? Visibility.Visible
          : Visibility.Collapsed
    });

    const bottomPanelButtons = viewControlViewModel.bottomItems.find(
      x => x instanceof ViewControlButtonPanelViewModel
    ) as ViewControlButtonPanelViewModel;

    if (!bottomPanelButtons) {
      return;
    }

    runInAction(() => {
      bottomPanelButtons.buttons.push(createEmptyFileGroupButton);
    });
  }
}
