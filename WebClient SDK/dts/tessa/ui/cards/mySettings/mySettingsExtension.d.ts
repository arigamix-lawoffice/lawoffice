import { IMySettingsExtensionContext } from './mySettingsExtensionContext';
import { IExtension } from 'tessa/extensions';
export interface IMySettingsExtension extends IExtension {
    initialized(context: IMySettingsExtensionContext): any;
    validating(context: IMySettingsExtensionContext): any;
    userCancelled(context: IMySettingsExtensionContext): any;
    saving(context: IMySettingsExtensionContext): any;
    saved(context: IMySettingsExtensionContext): any;
    finalized(context: IMySettingsExtensionContext): any;
}
export declare class MySettingsExtension implements IMySettingsExtension {
    static readonly type = "MySettingsExtension";
    shouldExecute(_context: any): boolean;
    initialized(_context: IMySettingsExtensionContext): void;
    validating(_context: IMySettingsExtensionContext): void;
    userCancelled(_context: IMySettingsExtensionContext): void;
    saving(_context: IMySettingsExtensionContext): void;
    saved(_context: IMySettingsExtensionContext): void;
    finalized(_context: IMySettingsExtensionContext): void;
}
