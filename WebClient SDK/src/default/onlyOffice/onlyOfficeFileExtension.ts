import { FileExtension, IFileExtensionContext } from 'tessa/ui/files';
import { MenuAction, showConfirm, showConfirmWithCancel, showNotEmpty, UIContext } from 'tessa/ui';
import { OnlyOfficeApiSingleton } from './onlyOfficeApiSingleton';
import { OnlyOfficeEditorWindowViewModel } from './onlyOfficeEditorWindowViewModel';
import { OnlyOfficeApi } from './onlyOfficeApi';
import { getNameAndExtForFile } from 'common/utility';
import { FileVersionState } from 'tessa/files';
import { ICardModel } from 'tessa/ui/cards';
import { LocalizationManager, localize } from 'tessa/localization';
import moment from 'moment';

/**
 * Представляет собой расширение, которое добавляет возможность открытия файла в редакторе OnlyOffice.
 */
export class OnlyOfficeFileExtension extends FileExtension {
  public openingMenu(context: IFileExtensionContext): void {
    const version = context.file.model.lastVersion;
    const api = OnlyOfficeApiSingleton.instance;
    const extSupported = OnlyOfficeApi.isSupportedFormat(version.getExtension());
    const cardId = context.control.model.card.id;
    const singleMode = context.files.length === 1;
    const file = context.file.model;

    const openWindowForRead = new MenuAction(
      'DocEditorOpenWindowForRead',
      '$OnlyOffice_OpenWindowForRead',
      'ta icon-thin-006',
      async () => {
        const vm = new OnlyOfficeEditorWindowViewModel(api, version, false, cardId);
        await vm.load();
      },
      [],
      !extSupported
    );

    const editCollapsed =
      !version.file.permissions.canEdit ||
      !version.file.permissions.canReplace ||
      !extSupported ||
      !!api.openFiles.find(
        of => of.forEdit && of.version.id === version.id && of.cardId === cardId
      );

    const openWindowForEdit = new MenuAction(
      'DocEditorOpenWindowForEdit',
      '$OnlyOffice_OpenWindowForEdit',
      'ta icon-thin-003',
      async () => {
        const vm = new OnlyOfficeEditorWindowViewModel(api, version, true, cardId);
        await vm.load(false);
      },
      [],
      editCollapsed
    );

    const openWindowForCoEdit = new MenuAction(
      'DocEditorOpenWindowForCoEdit',
      '$OnlyOffice_OpenWindowForCoEdit',
      'ta icon-thin-003',
      async () => {
        const currentCoedit = await OnlyOfficeApi.getCurrentCoedit(version.id);
        if (currentCoedit.validation.hasErrors) {
          await showNotEmpty(currentCoedit.validation);
          return;
        }

        if (currentCoedit.data?.lastVersionId && currentCoedit.data.lastVersionId !== version.id) {
          const result = await showConfirmWithCancel(
            '$OnlyOffice_BrotherYouAreOutdated',
            undefined,
            {
              YesButtonText: localize('$OnlyOffice_RefreshCard'),
              NoButtonText: localize('$OnlyOffice_UsePrevious')
            }
          );
          if (result === null) {
            return;
          } else if (result) {
            await UIContext.current.cardEditor?.refreshCard();
            return;
          }
        }

        if (currentCoedit.data?.names && currentCoedit.data?.date) {
          const dialogResult = await showConfirm(
            LocalizationManager.instance.format(
              '$OnlyOffice_JoinCoedit',
              currentCoedit.data.names,
              moment.utc(currentCoedit.data.date).local().format('DD-MM-YYYY HH:mm:ss')
            )
          );
          if (!dialogResult) {
            return;
          }
        } else {
          const dialogResult = await showConfirm('$OnlyOffice_ConfirmNewCoedit');
          if (!dialogResult) {
            return;
          }
        }

        const uiContext = UIContext.current;
        const editor = uiContext.cardEditor;
        let model: ICardModel;
        if (!editor || !(model = editor.cardModel!)) {
          return;
        }

        let saveBefore: boolean;
        if (await model.hasChanges()) {
          const dialogResult = await showConfirmWithCancel('$OnlyOffice_ConfirmBeforeEdit');
          if (dialogResult == null) {
            return;
          }

          saveBefore = dialogResult;
        } else {
          saveBefore = false;
        }

        if (saveBefore) {
          const saved = await editor.saveCard(uiContext);
          if (!saved) {
            return;
          }
        }

        // is there any content changes?
        let isChanged = false;

        const vm = new OnlyOfficeEditorWindowViewModel(api, version, true, cardId);
        await vm.load(true, async () => {
          isChanged = true;
        });

        if (isChanged) {
          await editor.refreshCard(editor.context);
        }
      },
      [],
      editCollapsed ||
        !singleMode ||
        file.lastVersion.state !== FileVersionState.Success ||
        file.lastVersion === file.versionAdded
    );

    const isSomeNotDocFile = files =>
      files.some(
        x => getNameAndExtForFile(x.model.lastVersion.name).ext?.toLocaleLowerCase() !== 'docx'
      );

    const otherVersion = context.files
      .map(x => x.model.lastVersion)
      .filter(x => x.id !== version.id)[0];
    const mergeConvertCollapsed =
      singleMode ||
      editCollapsed ||
      context.files.length > 2 ||
      isSomeNotDocFile(context.files) ||
      !otherVersion;

    const mergeFiles = new MenuAction(
      'OnlyMerge',
      '$OnlyOffice_OpenWindowForMerge',
      'ta icon-thin-359',
      async () => {
        const vm = new OnlyOfficeEditorWindowViewModel(api, version, true, cardId, otherVersion);
        await vm.mergeOrCompare();
      },
      null,
      mergeConvertCollapsed
    );

    const compareFiles = new MenuAction(
      'OnlyCompare',
      '$OnlyOffice_OpenWindowForCompare',
      'ta icon-thin-420',
      async () => {
        const vm = new OnlyOfficeEditorWindowViewModel(api, version, false, cardId, otherVersion);
        await vm.mergeOrCompare();
      },
      null,
      mergeConvertCollapsed
    );

    const menuItems = [
      openWindowForRead,
      openWindowForEdit,
      openWindowForCoEdit,
      compareFiles,
      mergeFiles
    ];

    const previewIndex = context.actions.findIndex(x => x.name === 'Preview');
    context.actions.splice(previewIndex + 1, 0, ...menuItems);
  }

  public shouldExecute(): boolean {
    return OnlyOfficeApiSingleton.isAvailable;
  }
}
