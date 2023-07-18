import { ApplicationExtension } from 'tessa/applicationExtension';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions/workplaceViewComponentExtension';
import { IWorkplaceViewComponent } from 'tessa/ui/views/workplaceViewComponent';
/**
 * Расширение для вывода надписи "Выберите тег в дереве слева", вместо таблицы, при открытии представления "TagCards".
 */
export declare class TagCardsViewExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
}
export declare class TagCardsViewInitializeExtension extends ApplicationExtension {
    initialize(): void;
}
