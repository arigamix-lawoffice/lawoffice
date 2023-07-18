import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class KrCardTasksEditorUIExtension extends CardUIExtension {
    private static readonly viewControlName;
    private static readonly taskIDPrefixReference;
    private static readonly cardIDColumnName;
    private static readonly tokenParamName;
    private attachDoubleClickHandlerAsync;
    private doubleClickHandler;
    private static tryMapView;
    private static tryGetColumnAlias;
    shouldExecute(context: ICardUIExtensionContext): boolean;
    initialized(context: ICardUIExtensionContext): Promise<void>;
}
