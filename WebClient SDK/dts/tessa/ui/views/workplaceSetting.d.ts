import { IWorkplaceViewModel } from './workplaceViewModel';
export interface WorkplaceViewModelSettings {
    treeWidth: number | null;
    treeVisible: boolean | null;
    emptyFoldersVisible: boolean | null;
}
export declare class WorkplaceSetting {
    private constructor();
    private static _instance;
    static get instance(): WorkplaceSetting;
    private getWorkpalaces;
    private getSettings;
    private tryGetSettings;
    saveTreeWidth(workplace: IWorkplaceViewModel): void;
    getTreeWidth(compositionId: guid): number | null;
    saveTreeVisible(workplace: IWorkplaceViewModel): void;
    getTreeVisible(compositionId: guid): boolean | null;
    saveEmptyFoldersVisible(workplace: IWorkplaceViewModel): void;
    getEmptyFoldersVisible(compositionId: guid): boolean | null;
    resetSettings(compositionId: guid): boolean;
    hasSettings(compositionId: guid): boolean;
}
