/**
 * Менеджер для запроса обновления умных ролей.
 */
import { SmartRoleUpdateRequest } from '../smartRoleUpdateRequest';
import { ValidationResult } from 'tessa/platform/validation';
export interface ISmartRoleUpdateManager {
    /**
     * Производит обновление умных ролей по заданному запросу.
     * @param request Запрос на обновление умных ролей.
     */
    updateSmartRolesAsync(request: SmartRoleUpdateRequest): Promise<ValidationResult>;
}
/**
 * Клиентская реализация <see cref="ISmartRoleUpdateManager"/>.
 * Выполнение доступно только от администратора.
 */
export declare class SmartRoleUpdateManager implements ISmartRoleUpdateManager {
    private constructor();
    private static _instance;
    static get instance(): SmartRoleUpdateManager;
    updateSmartRolesAsync(request: SmartRoleUpdateRequest): Promise<ValidationResult>;
}
