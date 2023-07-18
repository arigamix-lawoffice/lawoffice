import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { ApplicationExtension } from 'tessa';
export declare class CreateCardExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
}
export declare class CreateCardInitializeExtension extends ApplicationExtension {
    initialize(): void;
}
