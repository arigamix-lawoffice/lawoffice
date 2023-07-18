import { PropertyState } from './propertyState';
import { WorkplaceMetadataComponent, IWorkplaceComponentMetadata, WorkplaceMetadataComponentSealed } from '../workplaceMetadataComponent';
export interface IItemProperty extends IWorkplaceComponentMetadata {
    propertyName: string;
    propertyState: PropertyState;
    value: string;
    seal<T = ItemPropertySealed>(): T;
}
export interface ItemPropertySealed extends WorkplaceMetadataComponentSealed {
    readonly propertyName: string;
    readonly propertyState: PropertyState;
    readonly value: string;
    seal<T = ItemPropertySealed>(): T;
}
export declare class ItemProperty extends WorkplaceMetadataComponent implements IItemProperty {
    constructor();
    propertyName: string;
    propertyState: PropertyState;
    value: string;
    seal<T = ItemPropertySealed>(): T;
}
