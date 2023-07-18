import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
/**
 * Расширение, переопределяющее диалог фильтрации представления.
 */
export declare class FilterViewDialogOverrideWorkplaceComponentExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
}
