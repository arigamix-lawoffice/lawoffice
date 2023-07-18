import { CardTaskAssignedRolesAccessProvider } from 'tessa/ui/cards';
import { IUIContext } from 'tessa/ui';
import { IStorage } from 'tessa/platform/storage';
export declare class KrCardTaskAssignedRolesAccessProvider extends CardTaskAssignedRolesAccessProvider {
    private onceReLoaded;
    static reopenMainCardWithMarkAsync(mainCardContext: IUIContext, mark: string, proceedConfirmation?: () => Promise<boolean>, proceedAndSaveCardConfirmation?: () => Promise<boolean | null>, continuationOnSuccessFunc?: () => Promise<boolean>, getInfo?: IStorage): Promise<boolean>;
    checkAccessAsync(taskRowID: guid, mainCardUIcontext: IUIContext): Promise<boolean>;
}
