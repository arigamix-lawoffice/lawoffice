import { CardTableViewPanel, CardTableViewPanelViewModel } from './cardTableViewPanel';
import {
  CardTableViewFormContainerViewModel,
  CardTableViewFormContainer
} from './cardTableViewFormContainer';
import { ApplicationExtension, IApplicationExtensionContext } from 'tessa';
import { ViewComponentRegistry } from 'tessa/ui/views';

export class CardTableViewControlApplicationExtension extends ApplicationExtension {
  public initialize(_context: IApplicationExtensionContext) {
    ViewComponentRegistry.instance.register(CardTableViewPanelViewModel, () => {
      return CardTableViewPanel;
    });
    ViewComponentRegistry.instance.register(CardTableViewFormContainerViewModel, () => {
      return CardTableViewFormContainer;
    });
  }
}
