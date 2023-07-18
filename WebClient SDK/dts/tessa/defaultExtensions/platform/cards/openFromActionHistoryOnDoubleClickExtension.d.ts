import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
/**
 * Расширение, выполняющее открытие виртуальной карточки по записи в истории действий.
 */
export declare class OpenFromActionHistoryOnDoubleClickExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
}
