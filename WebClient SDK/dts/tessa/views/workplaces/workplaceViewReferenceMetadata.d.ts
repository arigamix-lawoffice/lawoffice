/**
 * Метаданные настроек ссылки представления в узле рабочего места.
 */
export interface IWorkplaceViewReferenceMetadata {
    /** Префикс колонки ссылки представления. */
    colPrefix: string;
    /** Имена секций, которые указываются для данной ссылки. */
    refSection: Array<string>;
    /** Защищает объект от изменений. */
    seal<T = WorkplaceViewReferenceMetadataSealed>(): T;
}
export interface WorkplaceViewReferenceMetadataSealed {
    readonly colPrefix: string;
    readonly refSection: ReadonlyArray<string>;
    seal<T = WorkplaceViewReferenceMetadataSealed>(): T;
}
export declare class WorkplaceViewReferenceMetadata implements IWorkplaceViewReferenceMetadata {
    constructor();
    colPrefix: string;
    refSection: Array<string>;
    seal<T = WorkplaceViewReferenceMetadataSealed>(): T;
}
