export interface IConditionType {
    ID: guid;
    Name: string;
    ConditionText: string;
    SettingsTypeID?: guid;
    UsePlaces: ReadonlyArray<guid>;
}
interface IConditionTypesProvider {
    getAll(): ReadonlyArray<IConditionType>;
    get(id: guid): IConditionType | null;
}
/**
 * Объект, который производит получение информации о типах условий на клиенте.
 * Получение информации возможно только после окончательного формирования метаданных со всеми расширениями.
 */
export declare class ClientConditionTypesProvider implements IConditionTypesProvider {
    private conditions;
    private isInitialized;
    private static _instance;
    static get instance(): ClientConditionTypesProvider;
    get(id: string): IConditionType | null;
    getAll(): IConditionType[];
    /**
     * Производит инициализацию типов условий на клиенте по переданным данным.
     * Инициализация может быть вызвана только раз.
     */
    initialize(conditionTypes: IConditionType[]): void;
}
export {};
