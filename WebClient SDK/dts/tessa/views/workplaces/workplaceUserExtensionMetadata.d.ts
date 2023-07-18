import { WorkplaceCompositeMetadata, IWorkplaceCompositeMetadata, WorkplaceCompositeMetadataSealed } from './workplaceCompositeMetadata';
import { IWorkplaceSettingsMetadata, WorkplaceSettingsMetadataSealed } from './workplaceSettingsMetadata';
import { IItemProperties, ItemPropertiesSealed } from './properties';
import { RoleLink } from '../roleLink';
export interface IWorkplaceUserExtensionMetadata extends IWorkplaceCompositeMetadata {
    readonly properties: ReadonlyArray<IItemProperties>;
    roles: Array<RoleLink>;
    readonly workplaceSettings: ReadonlyArray<IWorkplaceSettingsMetadata>;
    seal<T = WorkplaceUserExtensionMetadataSealed>(): T;
}
export interface WorkplaceUserExtensionMetadataSealed extends WorkplaceCompositeMetadataSealed {
    readonly properties: ReadonlyArray<ItemPropertiesSealed>;
    readonly roles: ReadonlyArray<RoleLink>;
    readonly workplaceSettings: ReadonlyArray<WorkplaceSettingsMetadataSealed>;
    seal<T = WorkplaceUserExtensionMetadataSealed>(): T;
}
export declare class WorkplaceUserExtensionMetadata extends WorkplaceCompositeMetadata implements IWorkplaceUserExtensionMetadata {
    constructor();
    get properties(): ReadonlyArray<IItemProperties>;
    roles: Array<RoleLink>;
    get workplaceSettings(): ReadonlyArray<IWorkplaceSettingsMetadata>;
    seal<T = WorkplaceUserExtensionMetadataSealed>(): T;
}
