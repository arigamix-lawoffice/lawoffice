import { getTessaIcon } from 'common/utility';
import {
  CardEditorModelResizeArgs,
  CardToolbarAction,
  CardUIExtension,
  ICardModel,
  ICardUIExtensionContext,
  IFormViewModelBase
} from 'tessa/ui/cards';
import Platform from 'common/platform';
import { DefaultFormTabWithTasksViewModel, TasksFormViewModel } from 'tessa/ui/cards/forms';
import { TaskViewModelState } from 'tessa/ui/cards/tasks';

export class CardToolbarTaskButtonUIExtension extends CardUIExtension {
  private _disposes: Array<(() => void) | null> = [];

  initialized(context: ICardUIExtensionContext): void {
    const { cardEditor } = context.uiContext;
    if (!cardEditor || !Platform.isMobile() || !cardEditor.isTaskBottomCardModeEnabled) {
      return;
    }

    const resize = cardEditor.resize;
    if (!resize.events.has(this._updateStateTaskButton)) {
      this._disposes.push(resize.addWithDispose(this._updateStateTaskButton));
    }
  }

  contextInitialized(context: ICardUIExtensionContext): void {
    const { cardEditor } = context.uiContext;
    if (!cardEditor || !Platform.isMobile() || !cardEditor.isTaskBottomCardModeEnabled) {
      return;
    }
    const componentRef = cardEditor.tryGetComponentRef();
    if (componentRef) {
      this._updateStateTaskButton({
        cardEditorModel: cardEditor,
        resizedElement: componentRef,
        rect: componentRef.getBoundingClientRect()
      });
    }
  }

  finalized(): void {
    for (const func of this._disposes) {
      if (func) {
        func();
      }
    }
    this._disposes.length = 0;
  }

  private _updateStateTaskButton = (e: CardEditorModelResizeArgs): void => {
    const { cardEditorModel, resizedElement, rect } = e;
    const width = rect?.width;
    const taskBottomCardState = !!(width && width < 768);

    const button = cardEditorModel.toolbar.items.find(x => x.name === 'ScrollToTasks');
    if (!button && taskBottomCardState && this._isNeedAddTaskButton(cardEditorModel.cardModel)) {
      const item = new CardToolbarAction({
        name: 'ScrollToTasks',
        caption: '$UI_Cards_Toolbar_ButtonScrollTasksCaption',
        icon: getTessaIcon('Int1391'),
        order: 1,
        command: () => this._onScrollToBottomTaskButton(resizedElement)
      });
      cardEditorModel.toolbar.addItem(item);
    }

    if (button && (!taskBottomCardState || !this._isNeedAddTaskButton(cardEditorModel.cardModel))) {
      cardEditorModel.toolbar.removeItem(button);
    }

    const mainForm = cardEditorModel.cardModel?.mainForm;

    if (taskBottomCardState) {
      this._setTabTasksFormVisible(mainForm, false);
      cardEditorModel.className.add('tasks-bottom-card-container');
    } else {
      this._setTabTasksFormVisible(mainForm, true);
      cardEditorModel.className.remove('tasks-bottom-card-container');
    }
  };

  // если задания скрыты, отложены или отображается один таск для KrInfoForInitiator,
  // то удаляем кнопку скроллинга к таскам
  private _isNeedAddTaskButton = (cardModel: ICardModel | null) => {
    if (!cardModel) {
      return false;
    }

    let formTabWithTasks;
    if (cardModel.mainForm instanceof DefaultFormTabWithTasksViewModel) {
      formTabWithTasks = cardModel.mainForm;
    }

    if (!formTabWithTasks) {
      return false;
    }

    const visibleAssingnedTasks = formTabWithTasks.visibleTasks.filter(
      x => x.state === TaskViewModelState.None
    );

    return (
      (visibleAssingnedTasks.length === 1 &&
        visibleAssingnedTasks[0].taskModel.cardTask?.typeId !==
          'c6f3828f-b001-46f6-b121-3f3ed9e65cde') || // KrInfoForInitiator
      visibleAssingnedTasks.length > 1
    );
  };

  private _setTabTasksFormVisible = (
    mainForm: IFormViewModelBase | null | undefined,
    visible: boolean
  ): void => {
    const tabTasksForm =
      mainForm instanceof DefaultFormTabWithTasksViewModel
        ? mainForm.visibleTabs.find(tab => tab instanceof TasksFormViewModel)
        : null;

    if (tabTasksForm) {
      tabTasksForm.isCollapsed = !visible;
    }
  };

  private _onScrollToBottomTaskButton = (element: HTMLDivElement) => {
    const el = element.querySelector('.tasks');
    if (!el) return;
    el.scrollIntoView({ block: 'start', behavior: 'smooth' });
  };
}
