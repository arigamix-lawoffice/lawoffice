import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { AutoCompleteTableViewModel } from 'tessa/ui/cards/controls';
import { Visibility } from 'tessa/platform';

export class HideEmptyIncomingReferencesControl extends CardUIExtension {
  initialized(context: ICardUIExtensionContext): void {
    const model = context.model;

    const block = model.blocks.get('RefsBlock');
    if (!block) {
      return;
    }

    const control = block.controls.find(x => x.name === 'IncomingRefsControl');
    const autoComplete = control as AutoCompleteTableViewModel;

    if (
      autoComplete &&
      autoComplete instanceof AutoCompleteTableViewModel &&
      autoComplete.items.length === 0
    ) {
      autoComplete.controlVisibility = Visibility.Collapsed;
    }
  }
}
