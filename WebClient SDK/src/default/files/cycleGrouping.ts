import { FileGrouping, FileViewModel, GroupInfo } from 'tessa/ui/cards/controls';
import { tryGetFromInfo } from 'tessa/ui';
import { LocalizationManager } from 'tessa/localization';
import { formatToString, formatDate } from 'tessa/platform/formatting';
import { FileCategory } from 'tessa/files';

// tslint:disable: triple-equals

export class CycleGrouping extends FileGrouping {
  //#region ctor

  constructor(name: string, caption: string, isCollapsed: boolean = false) {
    super(name, caption, isCollapsed);
  }

  //#endregion

  //#region methods

  public getGroupInfo(file: FileViewModel): GroupInfo {
    const cycleNumber = tryGetFromInfo<number>(file.model.info, 'KrCycleID');
    const cycleOrder = tryGetFromInfo<number>(file.model.info, 'KrCycleorder');
    const maxCyclewNumber = tryGetFromInfo<number>(file.model.info, 'KrMaxCycleNumber');
    if (cycleNumber != undefined && cycleOrder != undefined && maxCyclewNumber != undefined) {
      return {
        groupId: `CycleGroup${cycleNumber}`,
        groupCaption: LocalizationManager.instance.format(
          '$UI_Controls_FilesControl_CycleGroup',
          cycleNumber
        ),
        order: cycleOrder + 1
      };
    }

    return {
      groupId: 'DocumentsOnApprovalGroup',
      groupCaption: LocalizationManager.instance.localize(
        '$UI_Controls_FilesControl_DocumentsOnApprovalGroup'
      ),
      order: 0
    };
  }

  public clone(): CycleGrouping {
    return new CycleGrouping(this.name, this.caption, this.isCollapsed);
  }

  public attach(file: FileViewModel) {
    super.attach(file);
    file.captionDelegate.set(CycleGrouping.getCaption);
  }

  public detach(file: FileViewModel) {
    super.detach(file);
    const restoredDelegate = file.captionDelegate.restore();
    if (restoredDelegate !== CycleGrouping.getCaption) {
      throw new Error('FileViewModel.captionDelegate stack is damaged.');
    }
  }

  private static getCaption(viewModel: FileViewModel): string {
    const file = viewModel.model;
    const origin = file.origin;
    const category = file.category;

    if (
      !origin &&
      category &&
      viewModel.collection.some(
        x => x.model.category && FileCategory.equals(x.model.category, category)
      )
    ) {
      return LocalizationManager.instance.format(
        '$UI_Controls_FilesControl_Category',
        file.name,
        category.caption
      );
    }

    const cycle = tryGetFromInfo<number>(file.info, 'KrCycleID');
    const createdByName = tryGetFromInfo<string>(file.info, 'KrCreatedByName');
    const created = tryGetFromInfo<string>(file.info, 'KrCreated');

    if (!origin && cycle != undefined && createdByName != undefined && created != undefined) {
      return LocalizationManager.instance.format(
        '$UI_Controls_FilesControl_Version',
        file.name,
        file.lastVersion.number,
        createdByName
      );
    }

    if (origin) {
      const format = LocalizationManager.instance.localize(
        '$UI_Controls_FilesControl_ByCopy_Format'
      );
      const copy = LocalizationManager.instance.localize('$UI_Controls_FilesControl_ByCopy_Copy');
      return formatToString(
        format,
        file.name,
        copy,
        file.lastVersion.createdByName,
        formatDate(file.lastVersion.created)
      );
    }

    return file.name;
  }

  //#endregion
}
