import { IWorkplaceViewModel } from '../workplaceViewModel';
import { ICardEditorModel } from 'tessa/ui/cards';
export declare class OpenedCardObserver {
    private constructor();
    private static _instance;
    static get instance(): OpenedCardObserver;
    private _cardTrackers;
    private _reactionDis;
    subscribe(workplace: IWorkplaceViewModel, editorModel: ICardEditorModel): void;
    private handleWorkplaceClosed;
    private handleWorkplaceActivated;
    private handleWorkplaceChangedCurrentItem;
    private disposeWorkplace;
}
