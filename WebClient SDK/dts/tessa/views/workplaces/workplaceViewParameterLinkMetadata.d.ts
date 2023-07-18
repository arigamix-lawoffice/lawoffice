import { WorkplaceCompositeMetadata, IWorkplaceCompositeMetadata, WorkplaceCompositeMetadataSealed } from './workplaceCompositeMetadata';
import { IParameterSource, ParameterSourceSealed } from './parameterSource';
import { IViewParameterMetadata, ViewParameterMetadataSealed } from '../metadata';
export interface IWorkplaceViewParameterLinkMetadata extends IWorkplaceCompositeMetadata {
    displayValueFormat: string;
    displayValues: string[];
    linkedSource: IParameterSource | null;
    parameter: IViewParameterMetadata | null;
    setName: string;
    seal<T = WorkplaceViewParameterLinkMetadataSealed>(): T;
}
export interface WorkplaceViewParameterLinkMetadataSealed extends WorkplaceCompositeMetadataSealed {
    readonly displayValueFormat: string;
    readonly displayValues: string[];
    readonly linkedSource: ParameterSourceSealed | null;
    readonly parameter: ViewParameterMetadataSealed | null;
    readonly setName: string;
    seal<T = WorkplaceViewParameterLinkMetadataSealed>(): T;
}
export declare class WorkplaceViewParameterLinkMetadata extends WorkplaceCompositeMetadata implements IWorkplaceViewParameterLinkMetadata {
    constructor();
    displayValueFormat: string;
    displayValues: string[];
    linkedSource: IParameterSource | null;
    parameter: IViewParameterMetadata | null;
    setName: string;
    seal<T = WorkplaceViewParameterLinkMetadataSealed>(): T;
}
