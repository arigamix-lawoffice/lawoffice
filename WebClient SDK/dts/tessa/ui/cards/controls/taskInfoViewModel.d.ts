import { ControlViewModelBase } from './controlViewModelBase';
import { CardTypeControl } from 'tessa/cards/types';
import { Command, Visibility } from 'tessa/platform';
import { IUIContext } from 'tessa/ui/uiContext';
import { TaskAssignedRoleViewModel } from './taskAssignedRoleViewModel';
import { TaskInfoModel } from '.';
export declare class TaskInfoViewModel extends ControlViewModelBase {
    private _executeInContext;
    private taskRowID;
    private currentUserId;
    private taskAssignedRolesAccessProvider;
    constructor(control: CardTypeControl, taskModel: TaskInfoModel, executeInContext: (action: (context: IUIContext) => Promise<void>) => Promise<void>, includedInControl?: boolean, functionRolesNamesWithDeputyInfo?: {
        caption: string;
        isDeputy: boolean;
    }[] | null);
    roleNavigationActionAsync(): Promise<void>;
    private static fillVirtualRows;
    private checkAccess;
    private getTaskAssignedRolesList;
    private getInfoToolTip;
    private static getLimitationPrefix;
    private static getLimitation;
    private static getLimitationExtra;
    private static getLimitationFontWeight;
    private static getLimitationTimeZone;
    /**
     * Возвращает информацию об отношении открывающего к заданию
     */
    private static getPerformerType;
    private static getPerformerTypeVisibility;
    /**
     * Возвращает список кэпшнов функциональных ролей с признаком замещения, относящихся к варианту завершения в виде строки.
     */
    private static getFunctionRolesInfo;
    /**
     * Возвращает информацию о доступности информации об отношении открывающего к заданию
     */
    private static getFunctionRolesInfoVisibility;
    private static getInProgressUserVisibility;
    private static getTypeCaption;
    private getSecondSeparatorVisibility;
    taskAssignedRoles: TaskAssignedRoleViewModel[];
    typeCaption: string;
    inProgressUserName: string | null;
    inProgressUserVisibility: Visibility;
    roleNamePrefix: string;
    roleName: string | null;
    displayRoleName: string;
    roleNavigationCommand: Command;
    limitationPrefix: string;
    limitation: string;
    limitationExtra: string;
    limitationFontWeight: any;
    limitationTimeZone: string | null;
    performerType: string;
    performerTypeVisibility: Visibility;
    functionRolesInfo: string | null;
    functionRolesInfoVisibility: Visibility;
    digest: string | null;
    digestVisibility: Visibility;
    firstSeparatorVisibility: Visibility;
    secondSeparatorVisibility: Visibility;
    plannedToolTip: string;
    infoToolTip: string;
    includedInControl: boolean;
}
