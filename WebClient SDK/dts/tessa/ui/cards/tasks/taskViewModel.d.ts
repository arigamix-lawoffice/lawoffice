import { TaskNavigator } from './taskNavigator';
import { TaskWorkspaceViewModel } from './taskWorkspaceViewModel';
import { ICardModel } from '../interfaces';
import { SupportUnloadingViewModel } from '../supportUnloadingViewModel';
import { Card, CardRow, CardTask } from 'tessa/cards';
import { EventHandler } from 'tessa/platform';
import { CardMetadataSealed } from 'tessa/cards/metadata';
import { TaskInfoViewModel } from 'tessa/ui/cards/controls/taskInfoViewModel';
import { IValidationResultBuilder } from 'tessa/platform/validation';
import { MapStorage } from 'tessa/platform/storage';
export interface TaskViewModelEventArgs {
    task: TaskViewModel;
}
export interface TaskFormContentViewModelEventArgs extends TaskViewModelEventArgs {
    card: Card;
    sectionRows: MapStorage<CardRow>;
}
export interface WorkspaceChangedArgs {
    task: TaskViewModel;
}
export declare enum TaskViewModelState {
    None = 0,
    Postponed = 1,
    AuthorLocked = 2
}
/**
 * Модель представления для вывода области с заданием.
 */
export declare class TaskViewModel extends SupportUnloadingViewModel {
    constructor(parentModel: ICardModel, cardTask: CardTask);
    private _cardTask;
    private _postponeMetadata;
    private _postponeFormContent;
    private _taskInfo;
    private _taskWorkspace;
    private _sectionFieldsChangedHandlerDisposer;
    private _background;
    private _themeBackground;
    static postponeTypeId: string;
    readonly taskModel: ICardModel;
    readonly taskNavigator: TaskNavigator;
    get postponeMetadata(): CardMetadataSealed;
    get id(): guid;
    get taskWorkspace(): TaskWorkspaceViewModel;
    set taskWorkspace(value: TaskWorkspaceViewModel);
    get background(): string | null | undefined;
    set background(value: string | null | undefined);
    get themeBackground(): string | null | undefined;
    set themeBackground(value: string | null | undefined);
    get backgroundColor(): string | null | undefined;
    get state(): TaskViewModelState;
    private navigatedToEmptyStateHandler;
    private navigateToStateHandler;
    private setWorkspace;
    private sortActions;
    private getInitializedPostponeMetadata;
    private initializePostponeMetadata;
    private createFakeCardResponse;
    private createFakeForm;
    private getPostponeFormContent;
    getTaskInfoViewModel(): TaskInfoViewModel;
    private getBackgroundColor;
    modifyWorkspace(action: (e: WorkspaceChangedArgs) => void): (() => void) | null;
    readonly postponeMetadataInitializing: EventHandler<(e: TaskViewModelEventArgs) => void>;
    readonly postponeContentInitializing: EventHandler<(e: TaskFormContentViewModelEventArgs) => void>;
    readonly workspaceChanged: EventHandler<(e: WorkspaceChangedArgs) => void>;
    private sectionFieldsChangedHandler;
    onUnloading(validationResult: IValidationResultBuilder): void;
}
