import { FileControlExtension, IFileControlExtensionContext } from 'tessa/ui/files';
import { UIContext, tryGetFromInfo, MenuAction } from 'tessa/ui';
import { ICardModel } from 'tessa/ui/cards';
import { Card } from 'tessa/cards';
import { CycleFilesMode } from './cycleFilesMode';
import { IStorage } from 'tessa/platform/storage';
import { CycleGrouping } from './cycleGrouping';
import { modifyFileList } from './cycleGroupingUIHelper';

// tslint:disable: triple-equals

export class KrCurrentCycleFileControlExtension extends FileControlExtension {
  public openingMenu(context: IFileControlExtensionContext) {
    // Проверяем тип карточки
    const editor = UIContext.current.cardEditor;
    let model: ICardModel | null = null;
    let card: Card | null = null;
    if (
      !editor ||
      !(model = editor.cardModel) ||
      !(card = model.card) ||
      !card.sections.has('KrApprovalHistoryVirtual')
    ) {
      return;
    }

    const historyRows = card.sections.get('KrApprovalHistoryVirtual')!.rows;

    let currentMode: CycleFilesMode | null = null;
    let modeFromContext: CycleFilesMode = CycleFilesMode.ShowAllCycleFiles;

    const groupingModeFromInfo = tryGetFromInfo<IStorage | null>(
      UIContext.current.info,
      'CycleGroupingMode'
    );

    if (
      context.control.name &&
      editor &&
      editor.cardModel &&
      card.id === editor.cardModel.card.id &&
      groupingModeFromInfo &&
      context.control.name in groupingModeFromInfo
    ) {
      modeFromContext = groupingModeFromInfo[context.control.name];
      currentMode = modeFromContext;
    }

    if (currentMode == undefined) {
      const mode = tryGetFromInfo<CycleFilesMode>(context.control.info, 'CycleGroupingMode');
      currentMode = mode != undefined ? mode : CycleFilesMode.ShowAllCycleFiles;
    }

    const groupingsItem = context.actions.find(x => x.name === 'Groupings');

    const action = new MenuAction(
      'CycleFilesMode',
      '$UI_Controls_FilesControl_CycleFilesMode',
      'icon-Int787',
      null,
      [
        new MenuAction(
          'ShowAllCycleFiles',
          '$UI_Controls_FilesControl_ShowAllCycleFiles',
          'icon-Int787',
          () => {
            modifyFileList(context.control, card!, currentMode!, CycleFilesMode.ShowAllCycleFiles);
          },
          null,
          false,
          currentMode === CycleFilesMode.ShowAllCycleFiles
        ),
        new MenuAction(
          'ShowCurrentCycleFilesOnly',
          '$UI_Controls_FilesControl_ShowCurrentCycleFilesOnly',
          'icon-Int768',
          () => {
            modifyFileList(
              context.control,
              card!,
              currentMode!,
              CycleFilesMode.ShowCurrentCycleFilesOnly
            );
          },
          null,
          false,
          currentMode === CycleFilesMode.ShowCurrentCycleFilesOnly
        ),
        new MenuAction(
          'ShowCurrentAndLastCycleFilesOnly',
          '$UI_Controls_FilesControl_ShowCurrentAndLastCycleFilesOnly',
          'icon-Int770',
          () => {
            modifyFileList(
              context.control,
              card!,
              currentMode!,
              CycleFilesMode.ShowCurrentAndLastCycleFilesOnly
            );
          },
          null,
          false,
          currentMode === CycleFilesMode.ShowCurrentAndLastCycleFilesOnly
        )
      ],
      !(context.control.selectedGrouping instanceof CycleGrouping) ||
        !historyRows ||
        historyRows.length === 0
    );

    if (groupingsItem) {
      context.actions.splice(context.actions.indexOf(groupingsItem) + 1, 0, action);
    } else {
      context.actions.push(action);
    }
  }
}
