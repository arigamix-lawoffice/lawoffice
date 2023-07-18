import { ExternalFile } from './externalFile';
import { FileGrouping, FileViewModel, GroupInfo } from 'tessa/ui/cards/controls';

export class FileSourceGrouping extends FileGrouping {

  public readonly cardSourceGroupName = 'CardSourceGroup';
  public readonly externalSourceGroupName = 'ExternalSourceGroup';

  public getGroupInfo(file: FileViewModel): GroupInfo {
    const isExternal = file.model instanceof ExternalFile;

    if (isExternal) {
      return {
        groupId: this.externalSourceGroupName,
        groupCaption: '$KrTest_ExternalFilesGroup',
        order: -1
      };
    } else {
      return {
        groupId: this.cardSourceGroupName,
        groupCaption: '$KrTest_MainFilesGroup'
      };
    }
  }

  public clone(): FileSourceGrouping {
    return new FileSourceGrouping(
      this.name,
      this.caption,
      this.isCollapsed
    );
  }

}