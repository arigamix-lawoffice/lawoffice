import { IExtension } from 'tessa/extensions';
import { IStorage } from 'tessa/platform/storage';
export interface IWorkplaceExtension<T> extends IExtension {
    settingsStorage: IStorage;
    getExtensionName(): string;
    initializeSettings(model: T): void;
    initialize(model: T): void;
    initialized(model: T): void;
    finalized(model: T): void;
}
