/**
 * Операции синхронизации AD
 */
export declare enum OperationAdTypes {
    None = 0,
    Employees = 1,
    Departments = 2,
    StaticRoles = 4,
    ManualEmployee = 8,
    ManualDepartment = 16,
    ManualStaticRole = 32
}
/**
 * Операция ручной синхронизации AD
 */
export declare enum OperationAdSync {
    StaticRoles = 0,
    Employees = 1,
    Departments = 2
}
