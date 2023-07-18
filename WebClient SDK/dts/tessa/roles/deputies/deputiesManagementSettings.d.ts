import { IStorage } from 'tessa/platform/storage';
/**
 * Настройки для менеджера заместителей.
 */
export interface IDeputiesManagementSettings {
    /**
     * Уровень транзитивности для заместителей.
     * Проще говоря, является ли заместитель заместителя - действенным заместителем.
     * По умолчанию равен <c>0</c>, т.е. заместитель заместителя сотрудника не является валидным заместителем сотрудника.
     * При значении <c>1</c>, заместитель заместителя сотрудника уже является, а заместитель заместителя заместителя сотрудника - нет.
     * Не рекомендуется использовать значение больше, чем 1.
     */
    deputyTransitivityLevel: number;
    /**
     * Определяет, включено ли в системе разделение заместителей по ролям.
     * Если выключено, то заместитель сотрудника является его заместителем вне зависимости от роли.
     * Если включено, то заместитель сотрудника является его заместителем только в списке заданных ролей.
     */
    useDeputyRoleSeparation: boolean;
    /**
     * Определяет, используется ли таблица RoleDeputies для предрасчёта заместителей в ролях.
     * Наличие данной настройки положительно сказывается на скорости расчёта заместителей, и в тоже время скорость обработки сильно деградирует
     * при сильном увеличении числа ролей в системе и их заместителей.
     * Рекомендуется использовать с <see cref="DeputyTransitivityLevel"/> равным <c>0</c>.
     */
    useRoleDeputies: boolean;
}
export declare class DeputiesManagementSettings implements IDeputiesManagementSettings {
    private static readonly DeputiesManagementSettingsKey;
    private static readonly DeputyTransitivityLevelKey;
    private static readonly UseDeputyRoleSeparationKey;
    private static readonly UseRoleDeputiesKey;
    private _deputyTransitivityLevel;
    private _useDeputyRoleSeparation;
    private _useRoleDeputies;
    /** @inheritdoc  */
    get deputyTransitivityLevel(): number;
    /** @inheritdoc  */
    set deputyTransitivityLevel(value: number);
    /** @inheritdoc  */
    get useDeputyRoleSeparation(): boolean;
    /** @inheritdoc  */
    set useDeputyRoleSeparation(value: boolean);
    /** @inheritdoc  */
    get useRoleDeputies(): boolean;
    /** @inheritdoc  */
    set useRoleDeputies(value: boolean);
    toString(): string;
    static tryUnpack(info: IStorage): IDeputiesManagementSettings | null;
}
