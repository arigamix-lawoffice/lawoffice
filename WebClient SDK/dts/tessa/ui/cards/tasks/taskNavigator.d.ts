import { TaskWorkspaceState } from './taskWorkspaceState';
import { TaskAction } from './taskAction';
import { ICardModel } from '../interfaces';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { EventHandler } from 'tessa/platform';
export declare class TaskNavigator {
    constructor(taskModel: ICardModel, cardMetadata: CardMetadataSealed);
    readonly taskModel: ICardModel;
    readonly cardMetadata: CardMetadataSealed;
    readonly parameters: any;
    readonly navigated: EventHandler<(args: TaskNavigationEventArgs) => void>;
    navigate(state: TaskWorkspaceState, actions: TaskAction[], additionalActions?: TaskAction[]): void;
    navigateToForm(state: TaskWorkspaceState, formName: string, actions: TaskAction[], additionalActions?: TaskAction[]): void;
}
export declare class TaskNavigationEventArgs {
    constructor(navigator: TaskNavigator, hasForm: boolean, formName: string, state: TaskWorkspaceState, actions: TaskAction[], additionalActions: TaskAction[]);
    readonly navigator: TaskNavigator;
    readonly hasForm: boolean;
    readonly formName: string;
    readonly state: TaskWorkspaceState;
    readonly actions: TaskAction[];
    readonly additionalActions: TaskAction[];
}
