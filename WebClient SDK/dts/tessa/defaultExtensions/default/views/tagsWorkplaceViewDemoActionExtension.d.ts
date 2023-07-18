import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { TableGridViewModelBase } from 'tessa/ui/views/content';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
export declare class TagsWorkplaceViewDemoActionExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    private table;
    initialize(model: IWorkplaceViewComponent): void;
    private createAddTagMenuAction;
    addTag(viewComponent: IWorkplaceViewComponent, table: TableGridViewModelBase | null): Promise<void>;
}
