import { ApplicationExtension } from 'tessa';
import { TreeItemExtension } from 'tessa/ui/views/extensions';
import { ITreeItem } from 'tessa/ui/views/workplaces/tree';
export declare class CustomFolderViewExtension extends TreeItemExtension {
    getExtensionName(): string;
    initialize(model: ITreeItem): void;
}
export declare class CustomFolderInitializeExtension extends ApplicationExtension {
    initialize(): void;
}
