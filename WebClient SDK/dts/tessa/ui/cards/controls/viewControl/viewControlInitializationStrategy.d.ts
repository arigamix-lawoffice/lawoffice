import type { ViewControlInitializationContext } from './viewControlInitializationContext';
import { ViewMappingSettings } from './viewMappingSettings';
import { IViewControlContentItemsFactory } from './contents';
import { IStorage } from 'tessa/platform/storage';
import { ITessaView } from 'tessa/views';
import { ViewMetadataSealed } from 'tessa/views/metadata';
import { IParametersSetNameProvider, IViewParameters } from 'tessa/ui/views/parameters';
export interface IViewControlInitializationStrategy {
    viewExists(context: ViewControlInitializationContext): boolean;
    initializeMetadata(context: ViewControlInitializationContext): any;
    initializeDataProvider(context: ViewControlInitializationContext): any;
    initializeTable(context: ViewControlInitializationContext): any;
    initializeSorting(context: ViewControlInitializationContext): any;
    initializeContextMenu(context: ViewControlInitializationContext): any;
    initializeParameters(context: ViewControlInitializationContext): any;
    initializePaging(context: ViewControlInitializationContext): any;
    initializeContent(context: ViewControlInitializationContext): any;
}
export declare class ViewControlInitializationStrategy implements IViewControlInitializationStrategy {
    private readonly _itemsFactory;
    constructor(_itemsFactory?: IViewControlContentItemsFactory);
    viewExists(context: ViewControlInitializationContext): boolean;
    initializeMetadata(context: ViewControlInitializationContext): void;
    initializeDataProvider(context: ViewControlInitializationContext): void;
    initializeTable(context: ViewControlInitializationContext): void;
    initializeSorting(context: ViewControlInitializationContext): void;
    initializeContextMenu(_context: ViewControlInitializationContext): void;
    initializeParameters(context: ViewControlInitializationContext): void;
    initializePaging(context: ViewControlInitializationContext): void;
    initializeContent(context: ViewControlInitializationContext): void;
    protected getView(settings: IStorage): ITessaView | null;
    protected getViewMapping(settings: IStorage): ViewMappingSettings[];
    protected createParameters(viewMetadata: ViewMetadataSealed | null, viewMappings: ViewMappingSettings[], parametersSetNameProvider: IParametersSetNameProvider): IViewParameters;
    protected getMappingsBySets(viewMetadata: ViewMetadataSealed | null, viewMappings: ViewMappingSettings[]): Array<[string, IViewParameters]>;
}
