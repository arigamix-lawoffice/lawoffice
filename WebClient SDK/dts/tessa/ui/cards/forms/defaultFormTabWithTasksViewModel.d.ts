import { TasksFormViewModel } from './tasksFormViewModel';
import { DefaultFormTabWithTaskHistoryViewModel } from './defaultFormTabWithTaskHistoryViewModel';
import { ICardModel } from '../interfaces';
import { CardTask } from 'tessa/cards/cardTask';
import { TaskViewModel } from 'tessa/ui/cards/tasks';
import { IValidationResultBuilder } from 'tessa/platform/validation';
/**
 * Форма карточки с заданиями. Хотя бы одно задание должно быть загружено,
 * но заданий может не отображаться в случае, если все существующие задания отложены.
 */
export declare class DefaultFormTabWithTasksViewModel extends DefaultFormTabWithTaskHistoryViewModel {
    /**
     * Создаёт экземпляр класса с указанием информации,
     * необходимой для создания формы по умолчанию основной части карточки.
     * @param model Модель карточки в UI.
     * @param tasks Список заданий, которые следует отобразить в UI.
     */
    constructor(model: ICardModel, tasks: CardTask[]);
    private _postponedTasksAreVisible;
    private _authorLockedTasksAreVisible;
    readonly tasks: TaskViewModel[];
    readonly tasksTab: TasksFormViewModel;
    get visibleTasks(): ReadonlyArray<TaskViewModel>;
    get hasPostponedTasks(): boolean;
    get hasAuthorLockedTasks(): boolean;
    get postponedTasksAreVisible(): boolean;
    set postponedTasksAreVisible(value: boolean);
    get authorLockedTasksAreVisible(): boolean;
    set authorLockedTasksAreVisible(value: boolean);
    createTaskViewModel(task: CardTask): TaskViewModel;
    protected initializeCore(): void;
    onUnloading(validationResult: IValidationResultBuilder): void;
}
