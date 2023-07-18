import { IWorkplaceExtension } from './workplaceExtension';
import { ITreeItem } from '../workplaces/tree';
import { IStorage } from 'tessa/platform/storage';
export interface ITreeItemExtension extends IWorkplaceExtension<ITreeItem> {
}
export declare abstract class TreeItemExtension implements ITreeItemExtension {
    static readonly type = "TreeItemExtension";
    settingsStorage: IStorage;
    shouldExecute(model: ITreeItem): boolean;
    abstract getExtensionName(): string;
    initializeSettings(model: ITreeItem): void;
    initialize(_model: ITreeItem): void;
    initialized(_model: ITreeItem): void;
    finalized(_model: ITreeItem): void;
}
