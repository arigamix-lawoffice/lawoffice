import { ShowContextMenuButtonViewModel } from './showContextMenuButtonViewModel';
import { ShowContextMenuButton } from './showContextMenuButton';
import { FilesViewControlTableGridViewModel } from './filesViewControlTableGridViewModel';
import { FilesViewControlTableView } from './filesViewControlTableView';
import { ApplicationExtension, IApplicationExtensionContext } from 'tessa';
import { ViewComponentRegistry } from 'tessa/ui/views';

export class FilesViewApplicationExtension extends ApplicationExtension {
  public initialize(_context: IApplicationExtensionContext): void {
    ViewComponentRegistry.instance.register(ShowContextMenuButtonViewModel, () => {
      return ShowContextMenuButton;
    });
    ViewComponentRegistry.instance.register(FilesViewControlTableGridViewModel, () => {
      return FilesViewControlTableView;
    });
  }
}
