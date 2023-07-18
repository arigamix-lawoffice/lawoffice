import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
/**
 * Расширение, выполняющее открытие виртуальной карточки по строке в состояниях документа.
 */
export declare class OpenFromKrDocStatesOnDoubleClickExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
}
