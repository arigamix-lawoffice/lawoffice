import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
/**
 * Расширение, выполняющее создание карточки по шаблону вместо открытия шаблона по двойному клику.
 */
export declare class CreateFromTemplateOnDoubleClickExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
}
