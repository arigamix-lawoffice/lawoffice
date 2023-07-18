import { TaskActionType } from './taskActionType';
import { TaskGroupingType } from './taskGroupingType';
import { ICardModel } from 'tessa/ui/cards/interfaces';
import { CardMetadataCompletionOption } from 'tessa/cards/metadata';
export declare abstract class TaskAction {
    constructor(caption: string, action: Function, type?: TaskActionType, groupingType?: TaskGroupingType, completionOption?: CardMetadataCompletionOption | null);
    _caption: string;
    get caption(): string;
    set caption(val: string);
    readonly command: Function;
    readonly type: TaskActionType;
    readonly groupingType: TaskGroupingType;
    readonly completionOption: CardMetadataCompletionOption | null;
    _background: string | null;
    /**
     * Цвет фона. Если `null`, то будет использоваться цвет из темы.
     */
    get background(): string | null;
    set background(val: string | null);
}
export declare class TaskActionViewModel extends TaskAction {
    constructor(caption: string, action: Function, type?: TaskActionType, groupingType?: TaskGroupingType, completionOption?: CardMetadataCompletionOption | null, model?: ICardModel | null);
}
export declare class TaskAdditionalActionViewModel extends TaskAction {
    constructor(caption: string, action: Function, type?: TaskActionType, groupingType?: TaskGroupingType, completionOption?: CardMetadataCompletionOption | null, model?: ICardModel | null);
}
