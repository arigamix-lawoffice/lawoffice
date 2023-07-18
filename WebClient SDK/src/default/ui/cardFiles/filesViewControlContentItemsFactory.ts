import { FilesViewControlTableGridViewModel } from './filesViewControlTableGridViewModel';
import {
  BaseViewControlItem,
  StandardViewControlContentItemsFactory,
  ViewControlInitializationContext
} from 'tessa/ui/cards/controls';

export class FilesViewControlContentItemsFactory extends StandardViewControlContentItemsFactory {
  createTableContent(context: ViewControlInitializationContext): BaseViewControlItem | null {
    return new FilesViewControlTableGridViewModel(context.controlViewModel);
  }
}
