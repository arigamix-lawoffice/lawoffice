import { TreeItemExtension } from 'tessa/ui/views/extensions';
import { ITreeItem } from 'tessa/ui/views/workplaces/tree';

export class TreeViewItemTestExtension extends TreeItemExtension {

  public getExtensionName(): string {
    return 'Tessa.Extensions.Default.Client.Views.TreeViewItemTestExtension';
  }

  public initialize(model: ITreeItem) {
    console.log(model);
  }

}