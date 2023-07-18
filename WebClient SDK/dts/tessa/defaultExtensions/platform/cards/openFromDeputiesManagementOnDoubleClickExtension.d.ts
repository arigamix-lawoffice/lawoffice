import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
/**
 * Расширение, выполняющее открытие виртуальной карточки "Мои замещения"
 * по двойному клику из представления, предоставляющего идентификатор сотрудника
 * как референс для двойного клика.
 */
export declare class OpenFromDeputiesManagementOnDoubleClickExtension extends WorkplaceViewComponentExtension {
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
    private static getActualCardId;
    private static swapEndian;
}
