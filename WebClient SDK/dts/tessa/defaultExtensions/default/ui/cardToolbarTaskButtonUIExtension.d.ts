import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
export declare class CardToolbarTaskButtonUIExtension extends CardUIExtension {
    private _disposes;
    initialized(context: ICardUIExtensionContext): void;
    contextInitialized(context: ICardUIExtensionContext): void;
    finalized(): void;
    private _updateStateTaskButton;
    private _isNeedAddTaskButton;
    private _setTabTasksFormVisible;
    private _onScrollToBottomTaskButton;
}
