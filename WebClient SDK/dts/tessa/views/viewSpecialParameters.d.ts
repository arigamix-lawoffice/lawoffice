import { IViewParameterMetadata } from './metadata';
export declare class ViewSpecialParameters {
    static getMSSQLParameterMetadata(hidden?: boolean): IViewParameterMetadata;
    static getPostgreSQLParameterMetadata(hidden?: boolean): IViewParameterMetadata;
    static tryGetSpecialParameterMetadata(alias: string, hidden?: boolean): IViewParameterMetadata | null;
}
