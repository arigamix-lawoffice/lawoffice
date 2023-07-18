import { TreeItemExtension } from 'tessa/ui/views/extensions';
import { ITreeItem } from 'tessa/ui/views/workplaces/tree';
export declare class AutomaticNodeRefreshExtension extends TreeItemExtension {
    private _settings;
    private _timer;
    private _refreshPending;
    private _treeItem;
    private _disposes;
    getExtensionName(): string;
    initialized(model: ITreeItem): void;
    private subscribeToEvents;
    private unsubscribeFromEvents;
    private startTimer;
    private stopTimer;
    private updateByTimer;
    private updateByTimerCore;
    private refreshTableContent;
    private refreshContent;
}
