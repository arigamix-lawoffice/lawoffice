import { ApplicationExtension } from 'tessa';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
export declare class ChartoViewExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
}
export declare class ChartoInitializeExtension extends ApplicationExtension {
    initialize(): void;
}
