import { CardTask } from './cardTask';
import { CardTaskAssignedRole } from './cardTaskAssignedRole';
/**
 * Добавление роли в список ролей связанных с заданием.
 * @param task Задание, в которое добавляется роль.
 * @param roleID ID роли, которая будет добавлена
 * @param roleName Имя роли, которая будет добавлена
 * @param functionRoleID ID функциональной роли для добавляемой записи.
 * @param parentRowID RowId родительской записи.
 * @param showInTaskDetails Отображать ли запись в списке ролей балона задания
 * @param master Признак того, что эта запись является основной. На её основе будет определяться временная зона, календарь и т.д.
 * @returns Запись о функциональной роли в задании.
 */
export declare function addRole(task: CardTask, roleID: guid, roleName: string, functionRoleID: guid, parentRowID?: guid, showInTaskDetails?: boolean, master?: boolean): CardTaskAssignedRole;
