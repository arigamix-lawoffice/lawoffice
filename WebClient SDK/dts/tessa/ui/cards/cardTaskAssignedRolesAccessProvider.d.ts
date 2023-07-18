import { IUIContext } from '../uiContext';
/**
 * Объект для проверки на клиенте возможности редактирования связанных с заданием ролей
 */
export interface ICardTaskAssignedRolesAccessProvider {
    checkAccessAsync(taskRowID: guid, mainCardUIcontext: IUIContext): Promise<boolean>;
}
export declare class CardTaskAssignedRolesAccessProvider implements ICardTaskAssignedRolesAccessProvider {
    /**
     *   <doc path='info[@type="ICardTaskAssignedRolesAccessProvider" and @item="CheckAccessAsync"]'/>
     */
    checkAccessAsync(taskRowID: guid, mainCardUIcontext: IUIContext): Promise<boolean>;
}
export declare class CardTaskAssignedRolesAccessProviderFactory {
    private constructor();
    private static _instance;
    static get instance(): CardTaskAssignedRolesAccessProviderFactory;
    private instanceFunc;
    getProvider: () => CardTaskAssignedRolesAccessProvider;
    setProviderFunc(func: () => ICardTaskAssignedRolesAccessProvider): void;
}
