import { runInAction } from 'mobx';
import { IFileControl } from 'tessa/ui/files';
import { CycleFilesMode } from './cycleFilesMode';
import { tryGetFromInfo } from 'tessa/ui';
import { Card } from 'tessa/cards';
import { IStorage } from 'tessa/platform/storage';
import { IFile } from 'tessa/files';

export function switchFilesVisibility(
  control: IFileControl,
  card: Card,
  currentCycle: number | null,
  mode: CycleFilesMode
): void {
  switch (mode) {
    case CycleFilesMode.ShowAllCycleFiles:
      returnAllFiles(control);
      break;

    case CycleFilesMode.ShowCurrentCycleFilesOnly:
      if (currentCycle != undefined) {
        // Добавить в контейнер все файлы относящиеся к последнему циклу
        const newFiles: IFile[] = [];
        const controlFiles = control.files;
        for (const containerFile of control.fileContainer.files) {
          const cycle = tryGetFromInfo<number>(containerFile.info, 'KrCycleID');
          if (
            cycle != undefined &&
            cycle === currentCycle &&
            controlFiles.every(x => x.id !== containerFile.id) &&
            newFiles.every(x => x.id !== containerFile.id)
          ) {
            newFiles.push(containerFile);
          }
        }
        // Удалить все файлы, что не относятся к последнему циклу
        // применить добавление в контрол
        runInAction(() => {
          control.removeFiles(file => {
            const cycle = tryGetFromInfo<number>(file.model.info, 'KrCycleID');
            return cycle != undefined && cycle !== currentCycle;
          });
          control.addFiles(newFiles);
        });
      }
      break;

    case CycleFilesMode.ShowCurrentAndLastCycleFilesOnly:
      if (currentCycle != undefined) {
        const newFiles: IFile[] = [];
        const controlFiles = control.files;
        // Добавить в контейнер все файлы, относящиеся к последнему и предпоследнему циклу
        for (const containerFile of control.fileContainer.files) {
          const cardFile = card.files.find(x => x.rowId == containerFile.id);
          const cycle = tryGetFromInfo<number>(containerFile.info, 'KrCycleID');
          if (
            cardFile?.isVirtual !== false &&
            cycle != undefined &&
            (cycle === currentCycle || (currentCycle > 1 && cycle === currentCycle - 1)) &&
            controlFiles.every(x => x.id !== containerFile.id) &&
            newFiles.every(x => x.id !== containerFile.id)
          ) {
            newFiles.push(containerFile);
          }
        }
        // Удалить все файлы, что не относятся к последнему и предпоследнему циклу
        // применить добавление в контрол
        runInAction(() => {
          control.removeFiles(file => {
            const cycle = tryGetFromInfo<number>(file.model.info, 'KrCycleID');
            return (
              cycle != undefined &&
              cycle !== currentCycle &&
              currentCycle > 1 &&
              cycle !== currentCycle - 1
            );
          });
          control.addFiles(newFiles);
        });
      }
      break;
  }
}

export function modifyFileList(
  control: IFileControl,
  card: Card,
  currentMode: CycleFilesMode,
  mode: CycleFilesMode
): void {
  // Какой сейчас последний цикл?
  const historySection = card.sections.get('KrApprovalHistoryVirtual');
  const currentCycle = historySection
    ? Math.max(...historySection.rows.map(x => x.get('Cycle')))
    : null;

  if (
    currentMode !== CycleFilesMode.ShowAllCycleFiles &&
    mode !== CycleFilesMode.ShowAllCycleFiles
  ) {
    // Добавить все файлы назад
    returnAllFiles(control);
  }

  switch (mode) {
    case CycleFilesMode.ShowAllCycleFiles:
      // Добавить все файлы назад
      returnAllFiles(control);
      break;

    case CycleFilesMode.ShowCurrentCycleFilesOnly:
      if (currentCycle != undefined) {
        // Удалить все файлы, что не относятся к последнему циклу
        control.removeFiles(file => {
          const cycle = tryGetFromInfo<number>(file.model.info, 'KrCycleID');
          return cycle != undefined && cycle !== currentCycle;
        });
      }
      break;

    case CycleFilesMode.ShowCurrentAndLastCycleFilesOnly:
      if (currentCycle != undefined) {
        // Удалить все файлы, что не относятся к последнему и предпоследнему циклу
        control.removeFiles(file => {
          const cycle = tryGetFromInfo<number>(file.model.info, 'KrCycleID');
          return (
            cycle != undefined &&
            cycle !== currentCycle &&
            currentCycle > 1 &&
            cycle !== currentCycle - 1
          );
        });
      }
      break;
  }

  control.model.executeInContext(context => {
    // Записываем в инфо UIContext`а, только если есть имя (алиас) контрола
    if (control.name) {
      const controlsModes = tryGetFromInfo<IStorage>(context.info, 'CycleGroupingMode') || {};

      if (!('CycleGroupingMode' in context.info)) {
        context.info['CycleGroupingMode'] = controlsModes;
      }

      controlsModes[control.name] = mode;
    }

    // так же запишем в инфо контрола, чтобы работало локальное сохранение, до обновления краточки
    // даже если у контролла нет алиаса
    control.info['CycleGroupingMode'] = mode;
  });
}

export function restoreFilesList(control: IFileControl, card: Card): void {
  // Удалим виртуальные файлы "Версий"
  control.removeFiles(file => {
    const cardFile = card.files.find(x => x.rowId == file.id);
    const cycle = tryGetFromInfo<number>(file.model.info, 'KrCycleID');
    return cardFile?.isVirtual !== false && cycle != undefined;
  });

  // Вернём все "копии"
  const newFiles: IFile[] = [];
  const controlFiles = control.files;
  for (const containerFile of control.fileContainer.files) {
    const cardFile = card.files.find(x => x.rowId == containerFile.id);
    const cycle = tryGetFromInfo<number>(containerFile.info, 'KrCycleID');
    if (
      cardFile?.isVirtual === false &&
      cycle != undefined &&
      controlFiles.every(x => x.id !== containerFile.id) &&
      newFiles.every(x => x.id !== containerFile.id)
    ) {
      newFiles.push(containerFile);
    }
  }
  control.addFiles(newFiles);
}

function returnAllFiles(control: IFileControl) {
  const newFiles: IFile[] = [];
  const controlFiles = control.files;
  for (const containerFile of control.fileContainer.files) {
    const cycle = tryGetFromInfo<number>(containerFile.info, 'KrCycleID');
    if (
      cycle != undefined &&
      controlFiles.every(x => x.id !== containerFile.id) &&
      newFiles.every(x => x.id !== containerFile.id)
    ) {
      newFiles.push(containerFile);
    }
  }
  control.addFiles(newFiles);
}
