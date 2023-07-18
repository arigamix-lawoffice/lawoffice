import { TabPanelButton } from './tabPanelButton';
import { IExtension } from 'tessa/extensions';
export interface ITabPanelUIExtensionContext {
    buttons: TabPanelButton[];
}
export declare class TabPanelUIExtensionContext implements ITabPanelUIExtensionContext {
    buttons: TabPanelButton[];
}
export interface ITabPanelUIExtension extends IExtension {
    initialize(context: ITabPanelUIExtensionContext): any;
}
export declare abstract class TabPanelUIExtension implements ITabPanelUIExtension {
    static readonly type = "TabPanelUIExtension";
    shouldExecute(_context: ITabPanelUIExtensionContext): boolean;
    initialize(_context: ITabPanelUIExtensionContext): void;
}
