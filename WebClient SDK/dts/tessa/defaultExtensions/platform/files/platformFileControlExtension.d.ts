import { FileControlExtension, IFileControlExtensionContext } from 'tessa/ui/files';
/**
 * Расширение, инициализирующее меню файлового контрола значениями по умолчанию.
 */
export declare class PlatformFileControlExtension extends FileControlExtension {
    initializing(context: IFileControlExtensionContext): void;
    openingMenu(context: IFileControlExtensionContext): void;
    private static openCreateFileFromTemplateDialogAsync;
    private uploadAction;
    private multiselectAction;
    private getSortingActions;
    private getGroupingActions;
    private getFilteringActions;
}
