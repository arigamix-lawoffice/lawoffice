import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
import { IWorkplaceViewComponent } from 'tessa/ui/views/workplaceViewComponent';
export declare class OpenTopicOnDoubleClickExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initializeSettings(model: IWorkplaceViewComponent): void;
    private static cardModifierAction;
}
