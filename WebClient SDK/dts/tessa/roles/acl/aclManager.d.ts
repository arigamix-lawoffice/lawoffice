import { ValidationResult } from 'tessa/platform/validation';
import { AclManagerRequest } from './aclManagerRequest';
/**
 * Объект, который занимается расчётом ACL.
 */
export interface IAclManager {
    /**
     * Производит расчёт Acl по заданному запросу.
     * @param request Запрос на расчёт Acl.
     * @returns Результат обработки запроса.
     */
    updateAclAsync(request: AclManagerRequest): Promise<ValidationResult>;
}
/**
 * Клиентская реализация <see cref="IAclManager"/>.
 * Выполнение доступно только от администратора.
 */
export declare class AclManager implements IAclManager {
    private static _instance;
    static get instance(): AclManager;
    updateAclAsync(request: AclManagerRequest): Promise<ValidationResult>;
}
