import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { DefaultFormMainViewModel } from 'tessa/ui/cards/forms';
import { getKrComponentsByCard, KrTypesCache, KrComponents } from 'tessa/workflow';
import { tryGetKrType } from '../workflow/krProcess/krUIHelper';
import { hasNotFlag } from 'tessa/platform';

export class HideForumTabUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext) {
    const model = context.model;
    const mainForm = model.mainForm;
    if (!(mainForm instanceof DefaultFormMainViewModel)) {
      return;
    }

    const usedComponents = getKrComponentsByCard(model.card, KrTypesCache.instance);
    const krType = tryGetKrType(KrTypesCache.instance, model.card, model.card.typeId);

    if (
      hasNotFlag(usedComponents, KrComponents.UseForum) ||
      (!!krType && !krType.useForum) ||
      (!!krType && krType.useForum && !krType.useDefaultDiscussionTab)
    ) {
      const forumTab = model.forms.find(x => x.name === 'Forum');
      if (forumTab) {
        forumTab.isCollapsed = true;
        mainForm.restoreSelectedTab();
      }
    }
  }
}
