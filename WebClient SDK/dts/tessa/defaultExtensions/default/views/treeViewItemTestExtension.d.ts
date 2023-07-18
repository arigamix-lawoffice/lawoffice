import { TreeItemExtension } from 'tessa/ui/views/extensions';
import { ITreeItem } from 'tessa/ui/views/workplaces/tree';
export declare class TreeViewItemTestExtension extends TreeItemExtension {
    getExtensionName(): string;
    initialize(model: ITreeItem): void;
}
