import { designTimeCard } from '../../workflow/krProcess/krUIHelper';
import {
  CardUIExtension,
  ICardUIExtensionContext,
  CardModelFlags,
  ICardModel,
  IFormWithBlocksViewModel
} from 'tessa/ui/cards';
import { hasFlag, Visibility } from 'tessa/platform';
import { getKrComponentsByCard, KrTypesCache, KrComponents } from 'tessa/workflow';

export class KrTemplateUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext) {
    let routesForm: IFormWithBlocksViewModel;
    if (
      !hasFlag(context.model.flags, CardModelFlags.EditTemplate) ||
      !this.cardIsAvailableForExtension(context.model) ||
      !(routesForm = context.model.forms.find(x => x.name === 'ApprovalProcess')!)
    ) {
      return;
    }

    for (let block of routesForm.blocks) {
      block.blockVisibility =
        block.name === 'DisclaimerBlock' ? Visibility.Visible : Visibility.Collapsed;
    }
  }

  private cardIsAvailableForExtension(model: ICardModel): boolean {
    if (designTimeCard(model.card.typeId)) {
      return true;
    }

    const usedComponents = getKrComponentsByCard(model.card, KrTypesCache.instance);
    return hasFlag(usedComponents, KrComponents.Routes);
  }
}
