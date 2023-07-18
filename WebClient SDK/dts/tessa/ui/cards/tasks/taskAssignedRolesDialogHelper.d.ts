import { CardMetadataSealed } from 'tessa/cards/metadata';
import { ICardModel } from 'tessa/ui/cards';
import { CardTask } from 'tessa/cards';
export declare class TaskAssignedRolesDialogHelper {
    private static readonly taskAssignedRolesDialogTypeName;
    private static readonly taskAssignedRoleEditorDialogTypeName;
    static showTaskAssignedRolesEditorDialog(metadata: CardMetadataSealed | null, taskRowId: guid, currentUserId: guid, checkAccessFunction: (cardTask: CardTask, cardModel: ICardModel, innerFunc: () => Promise<void>) => Promise<[boolean, CardTask | null]>, getCardTaskFunction: () => Promise<CardTask | null>, trySaveChangesFunction: () => Promise<boolean>): Promise<void>;
    private static tryTranslateTaskAssignRolesInfo;
    private static showNewTaskAssignedRoleEditorWindow;
    private static fillVirtualRow;
}
