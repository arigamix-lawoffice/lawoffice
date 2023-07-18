import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { tryGetFromInfo, createCardFileSourceForCard } from 'tessa/ui';
import { IStorage, clear } from 'tessa/platform/storage';
import { Guid, TypedField, DotNetType, getTypedFieldValue, createTypedField } from 'tessa/platform';
import { VirtualFile, VirtualFileVersion } from 'tessa/files';
import { CardFileContentSource } from 'tessa/cards';

export class KrGetCycleFileInfoUIExtension extends CardUIExtension {
  public async initializing(context: ICardUIExtensionContext) {
    if (context.model.inSpecialMode) {
      return;
    }

    const cardInfo = context.card.tryGetInfo();
    if (!cardInfo) {
      return;
    }

    const filesByCycles = tryGetFromInfo<{ [key: string]: TypedField<DotNetType.Guid, guid> }>(
      cardInfo,
      'KrFilesByCycles'
    );
    const filesModifiedByCyclesStorage = tryGetFromInfo<IStorage[]>(
      cardInfo,
      'KrFilesModifiedByCycles'
    );
    const maxCycleNumber = tryGetFromInfo<number>(cardInfo, 'KrMaxCycleNumber');
    // tslint:disable-next-line: triple-equals
    if (!filesByCycles || !filesModifiedByCyclesStorage || maxCycleNumber == undefined) {
      return;
    }

    for (let fileId in filesByCycles) {
      const cycleId = getTypedFieldValue(filesByCycles[fileId]);
      const fileModel = context.fileContainer.files.find(x => Guid.equals(x.id, fileId));
      if (fileModel) {
        fileModel.info['KrCycleID'] = Number(cycleId);
        fileModel.info['KrCycleorder'] = Number(maxCycleNumber) - Number(cycleId);
        fileModel.info['KrMaxCycleNumber'] = Number(maxCycleNumber);
      }
    }

    if (filesModifiedByCyclesStorage.length > 0) {
      // clone - пустая карточка с той же информацией по типу и по версии, и др. системной информацией, но без фактических данных;
      // в неё будут добавляться виртуальные файлы, чтобы незахламлять структуру основной карточки

      const clone = context.card.clone();
      clone.sections.clear();
      clone.files.clear();
      clone.tasks.clear();
      clone.taskHistory.clear();
      clone.taskHistoryGroups.clear();
      clear(clone.info);

      const cloneFileSource = createCardFileSourceForCard(clone);

      for (let versionInfo of filesModifiedByCyclesStorage) {
        const fileId = tryGetFromInfo<guid>(versionInfo, 'FileID');

        const originalFile = context.fileContainer.files.find(x => Guid.equals(x.id, fileId));
        if (!originalFile) {
          continue;
        }

        const originalCardFile = context.card.files.find(x => Guid.equals(x.rowId, fileId));
        if (!originalCardFile) {
          continue;
        }

        const cycle = tryGetFromInfo<number>(versionInfo, 'Cycle');
        const versionId = tryGetFromInfo<guid>(versionInfo, 'VersionID');
        const versionNumber = tryGetFromInfo<number>(versionInfo, 'Number');
        const versionSize = tryGetFromInfo<number>(versionInfo, 'Size');
        const sourceId = tryGetFromInfo<number>(versionInfo, 'SourceID');
        const versionCreated = tryGetFromInfo<string>(versionInfo, 'Created');
        const versionCreatedById = tryGetFromInfo<guid>(versionInfo, 'CreatedByID');
        const versionCreatedByName = tryGetFromInfo<string>(versionInfo, 'CreatedByName');

        // теперь создаём файл и наполняем его
        const virtualFile = await context.fileContainer.addVirtalFile(
          cloneFileSource,
          new VirtualFile(
            originalCardFile.typeName,
            Guid.newGuid(),
            originalCardFile.name,
            token => {
              token.size = versionSize;
              token.lastVersionTags.push(...originalFile.lastVersion.tags);
            }
          ),
          new VirtualFileVersion(Guid.newGuid(), originalCardFile.name, token => {
            token.size = versionSize;
            token.number = versionNumber;
            token.created = versionCreated;
            token.createdById = versionCreatedById;
            token.createdByName = versionCreatedByName;
            token.tags.push(...originalFile.lastVersion.tags);
          })
        );

        const virtualCardFile = clone.files.find(x => x.card.id === virtualFile.id)!;
        virtualCardFile.card.createdById = versionCreatedById;
        virtualCardFile.card.createdByName = versionCreatedByName;
        virtualCardFile.card.created = versionCreated;
        virtualCardFile.lastVersion!.number = versionNumber;
        virtualCardFile.lastVersion!.created = versionCreated;
        virtualCardFile.lastVersion!.createdById = versionCreatedById;
        virtualCardFile.lastVersion!.createdByName = versionCreatedByName;

        virtualCardFile.externalSource = new CardFileContentSource();
        virtualCardFile.externalSource.cardId = context.card.id;
        virtualCardFile.externalSource.cardTypeId = context.model.cardType.id!;
        virtualCardFile.externalSource.fileId = fileId;
        virtualCardFile.externalSource.storeSource = sourceId;
        virtualCardFile.externalSource.versionRowId = versionId;

        virtualFile.info['KrCreated'] = createTypedField(versionCreated, DotNetType.DateTime);
        virtualFile.info['KrCreatedByName'] = createTypedField(
          versionCreatedByName,
          DotNetType.String
        );
        virtualFile.info['KrCycleID'] = createTypedField(cycle, DotNetType.Int32);
        virtualFile.info['KrCycleorder'] = createTypedField(
          maxCycleNumber - cycle,
          DotNetType.Int32
        );
        virtualFile.info['KrMaxCycleNumber'] = createTypedField(maxCycleNumber, DotNetType.Int32);
      }
    }
  }
}
