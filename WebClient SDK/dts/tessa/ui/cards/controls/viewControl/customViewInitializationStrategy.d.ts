import { ViewControlInitializationStrategy } from './viewControlInitializationStrategy';
import { ViewControlInitializationContext } from './viewControlInitializationContext';
import { CustomViewInitializationOptions } from './customViewInitializationOptions';
import { IViewService } from 'tessa/views';
export declare class CustomViewInitializationStrategy extends ViewControlInitializationStrategy {
    private _options;
    private _viewService;
    constructor(_options: CustomViewInitializationOptions, _viewService: IViewService);
    initializeMetadata(context: ViewControlInitializationContext): void;
    initializeDataProvider(context: ViewControlInitializationContext): void;
    initializeParameters(context: ViewControlInitializationContext): void;
}
