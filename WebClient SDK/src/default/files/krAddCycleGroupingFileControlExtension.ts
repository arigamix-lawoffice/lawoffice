import { reaction } from 'mobx';
import { CycleGrouping } from './cycleGrouping';
import { CycleFilesMode } from './cycleFilesMode';
import { switchFilesVisibility, restoreFilesList } from './cycleGroupingUIHelper';
import { FileControlExtension, IFileControlExtensionContext } from 'tessa/ui/files';
import { ICardModel } from 'tessa/ui/cards';
import { getKrComponentsByCard, KrTypesCache, KrComponents } from 'tessa/workflow';
import { hasNotFlag, createTypedField, DotNetType } from 'tessa/platform';
import { tryGetFromInfo, UIContext } from 'tessa/ui';
import { IStorage } from 'tessa/platform/storage';
import { CardSingletonCache } from 'tessa/cards';

// tslint:disable: triple-equals

/**
 * Управляет появлением виртуальных файлов "версий" при включении и выключении группировки по циклам согласования
 */
export class KrAddCycleGroupingFileControlExtension extends FileControlExtension {
  private _dispose: Function[] = [];

  public initialized(context: IFileControlExtensionContext) {
    const model = context.info['.cardModel'] as ICardModel;
    if (!model || model.inSpecialMode) {
      return;
    }

    const card = model.card;
    if (
      !card ||
      hasNotFlag(getKrComponentsByCard(card, KrTypesCache.instance), KrComponents.Routes)
    ) {
      return;
    }

    let cycleGrouping = context.groupings.find(p => p.name === 'Cycle');

    // Добавляем группировку/фильтр
    if (!cycleGrouping) {
      cycleGrouping = new CycleGrouping('Cycle', '$UI_Controls_FilesControl_GroupingByCycle');
      context.groupings.push(cycleGrouping);
    }

    // Если по умолчанию не выбрана группировка по циклу согласования, то надо убрать виртуальные файлы версий.
    if (!(context.control.selectedGrouping instanceof CycleGrouping)) {
      context.control.removeFiles(file => {
        const cardFile = card.files.find(x => x.rowId == file.id);
        const cycle = tryGetFromInfo<number>(file.model.info, 'KrCycleID');
        return cardFile?.isVirtual !== false && cycle != undefined;
      });
    }

    const sections = card.tryGetSections();
    if (!sections) {
      return;
    }

    const commonInfo = sections.tryGet('KrApprovalCommonInfoVirtual');
    const state: number | null = commonInfo ? commonInfo.fields.tryGet('StateID') : null;

    let currentCycle: number | null = null;
    const approvalHistory = sections.tryGet('KrApprovalHistoryVirtual');
    if (approvalHistory) {
      const rows = approvalHistory.tryGetRows();
      if (rows && rows.length > 0) {
        currentCycle = Math.max(...rows.map(x => x.tryGet('Cycle', 0)));
      }
    }

    this._dispose.push(
      context.control.containerFileAdded.addWithDispose(e => {
        if (
          state != undefined &&
          state !== 0 && // KrState.Draft
          !!e.file.origin &&
          currentCycle != undefined &&
          currentCycle > 0
        ) {
          e.file.info['KrCycleID'] = createTypedField(currentCycle, DotNetType.Int32);
        }
      })!
    );

    this._dispose.push(
      reaction(
        () => context.control.selectedGrouping,
        grouping => {
          if (grouping instanceof CycleGrouping) {
            const currentMode = tryGetFromInfo<CycleFilesMode | null>(
              context.control.info,
              'CycleGroupingMode'
            );
            switchFilesVisibility(
              context.control,
              card,
              currentCycle,
              currentMode == undefined ? CycleFilesMode.ShowAllCycleFiles : currentMode
            );
          } else {
            restoreFilesList(context.control, card);
          }
        }
      )
    );

    let currentCycleMode: CycleFilesMode | null = null;
    let modeFromContext: CycleFilesMode = CycleFilesMode.ShowAllCycleFiles;
    const cardEditor = UIContext.current.cardEditor;
    const groupingModeFromInfo = tryGetFromInfo<IStorage | null>(
      UIContext.current.info,
      'CycleGroupingMode'
    );

    if (
      context.control.name &&
      cardEditor &&
      cardEditor.cardModel &&
      card.id === cardEditor.cardModel.card.id &&
      groupingModeFromInfo &&
      context.control.name in groupingModeFromInfo
    ) {
      modeFromContext = groupingModeFromInfo[context.control.name];
      currentCycleMode = modeFromContext;
    }

    if (currentCycleMode == undefined) {
      const cardModelInfo = model.info;
      const mode = tryGetFromInfo<CycleFilesMode | null>(cardModelInfo, 'CycleGroupingMode');
      if (mode == undefined) {
        const settings = CardSingletonCache.instance.cards.get('KrSettings')!;

        // Проверим, что тип карточки/документа включён в настройки
        // Читеем тип карточки/документа ровно один раз
        let docCardTypeId: guid | undefined = undefined;
        const dciSection = card.sections.tryGet('DocumentCommonInfo');
        if (dciSection) {
          docCardTypeId = dciSection.fields.tryGet('DocTypeID');
        }
        if (docCardTypeId == undefined) {
          docCardTypeId = card.typeId;
        }

        let settingsRowId: guid | undefined = undefined;
        if (
          state != undefined &&
          settings.sections.get('KrSettingsCycleGrouping')!.rows.some(x => {
            const typeId = x.get('TypeID');
            if (typeId === docCardTypeId) {
              settingsRowId = x.get('TypesRowID');
              // Проверим состояния
              if (
                settings.sections
                  .get('KrSettingsCycleGroupingStates')!
                  .rows.filter(y => y.get('TypesRowID') === settingsRowId)
                  .some(y => y.get('StateID') === state)
              ) {
                return true;
              }
            }

            return false;
          })
        ) {
          const row = settings.sections
            .get('KrSettingsCycleGroupingTypes')!
            .rows.find(x => x.rowId === settingsRowId);
          currentCycleMode = row ? row.get('DefaultModeID') : null;
          if (currentCycleMode != undefined && cardModelInfo) {
            cardModelInfo['CycleGroupingMode'] = currentCycleMode;
          }
        }
      }
    }

    if (currentCycleMode != undefined) {
      context.control.info['CycleGroupingMode'] = currentCycleMode;
      if (!context.control.selectedGrouping) {
        context.control.selectedGrouping = cycleGrouping;
      }
    }
  }

  public finalized() {
    for (let dispose of this._dispose) {
      if (dispose) {
        dispose();
      }
    }
    this._dispose.length = 0;
  }
}
