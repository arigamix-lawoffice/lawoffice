import { ContentPlaceArea, FilterButtonViewModel } from 'tessa/ui/views/content';
import { IWorkplaceViewComponent } from 'tessa/ui/views';
/**
 * Модель представления, позволяющая переопределить действие, выполняемое при нажатии на кнопку открытия диалога с параметрами фильтрации представления.
 */
export declare class CustomFilterButtonViewModel extends FilterButtonViewModel {
    constructor(openFilterCommand: (viewComponent: IWorkplaceViewComponent) => Promise<void>, viewComponent: IWorkplaceViewComponent, area?: ContentPlaceArea, order?: number);
    private _openFilterCommand;
    openFilter(): Promise<void>;
}
