import { IWorkplaceExtension } from './workplaceExtension';
import { IWorkplaceViewComponent } from '../workplaceViewComponent';
import { IStorage } from 'tessa/platform/storage';
export interface IWorkplaceViewComponentExtension extends IWorkplaceExtension<IWorkplaceViewComponent> {
}
export declare abstract class WorkplaceViewComponentExtension implements IWorkplaceViewComponentExtension {
    static readonly type = "WorkplaceViewComponentExtension";
    settingsStorage: IStorage;
    shouldExecute(model: IWorkplaceViewComponent): boolean;
    abstract getExtensionName(): string;
    initializeSettings(model: IWorkplaceViewComponent): void;
    initialize(_model: IWorkplaceViewComponent): void;
    initialized(_model: IWorkplaceViewComponent): void;
    finalized(_model: IWorkplaceViewComponent): void;
}
