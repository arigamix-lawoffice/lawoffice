import { ApplicationExtension } from 'tessa';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
export declare class ManagerWorkplaceExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
}
export declare class ManagerWorkplaceInitializeExtension extends ApplicationExtension {
    initialize(): void;
}
