import { IWorkplaceViewComponent } from 'tessa/ui/views';
import { WorkplaceViewComponentExtension } from 'tessa/ui/views/extensions';
/**
 * Расширение, выполняющее открытие карточки из представления по двойному клику в диалоге.
 */
export declare class OpenInModalDialogOnDoubleClickExtension extends WorkplaceViewComponentExtension {
    private _settings;
    getExtensionName(): string;
    initialize(model: IWorkplaceViewComponent): void;
}
