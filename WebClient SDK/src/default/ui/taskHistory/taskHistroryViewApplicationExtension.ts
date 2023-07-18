import { TaskHistoryViewSearchViewModel } from './taskHistoryViewSearchViewModel';
import { TaskHistoryViewSearch } from './taskHistoryViewSearch';
import { ApplicationExtension, IApplicationExtensionContext } from 'tessa';
import { ViewComponentRegistry } from 'tessa/ui/views';

export class TaskHistroryViewApplicationExtension extends ApplicationExtension {
  public initialize(_context: IApplicationExtensionContext) {
    ViewComponentRegistry.instance.register(TaskHistoryViewSearchViewModel, () => {
      return TaskHistoryViewSearch;
    });
  }
}
