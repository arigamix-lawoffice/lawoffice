import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
export declare class ViewsContextMenuExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
    private static createRefreshMenuAction;
    private static createFilterMenuAction;
    private static createClearFilterMenuAction;
    private static createOpenCardMenuAction;
}
