import { PropertyState } from './propertyState';
import { IItemProperty, ItemPropertySealed } from './itemProperty';
import { WorkplaceCompositeMetadata, IWorkplaceCompositeMetadata, WorkplaceCompositeMetadataSealed } from '../workplaceCompositeMetadata';
export interface IItemProperties extends IWorkplaceCompositeMetadata {
    propertyClass: string;
    propertyState: PropertyState;
    scope: string;
    readonly properties: ReadonlyArray<IItemProperty>;
    seal<T = ItemPropertiesSealed>(): T;
}
export interface ItemPropertiesSealed extends WorkplaceCompositeMetadataSealed {
    readonly propertyClass: string;
    readonly propertyState: PropertyState;
    readonly scope: string;
    readonly properties: ReadonlyArray<ItemPropertySealed>;
    seal<T = ItemPropertiesSealed>(): T;
}
export declare class ItemProperties extends WorkplaceCompositeMetadata implements IItemProperties {
    constructor();
    propertyClass: string;
    propertyState: PropertyState;
    get scope(): string;
    set scope(value: string);
    get properties(): ReadonlyArray<IItemProperty>;
    seal<T = ItemPropertiesSealed>(): T;
}
