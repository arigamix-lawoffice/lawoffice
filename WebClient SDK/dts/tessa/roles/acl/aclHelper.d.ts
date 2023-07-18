/**
 * Вспомогательные свойства и методы для функционала Acl.
 */
export declare class AclHelper {
    /**
     * Основная секция для хранения записей Acl.
     */
    static readonly AclSection = "Acl";
    /**
     * Таблица с информацией о генерации ACL.
     */
    static readonly AclGenerationInfoTable = "AclGenerationInfo";
    /**
     * Имя операции отложенной обработки расчёта Acl.
     */
    static readonly AclManagerOperationName = "$Enum_OperationTypes_AclCalculation";
    /**
     * Метка, которая используется для обозначения, что требуется валидация правила.
     */
    static readonly ValidateMark: string;
    /**
     * Метка, которая используется для обозначения, что требуется валидация всех правил.
     */
    static readonly ValidateAllMark: string;
}
