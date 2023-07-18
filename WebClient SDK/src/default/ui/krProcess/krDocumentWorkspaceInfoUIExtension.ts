import { CardUIExtension, ICardUIExtensionContext, CardModelFlags, CardModelInitializingEventArgs } from 'tessa/ui/cards';
import { hasNotFlag, hasFlag } from 'tessa/platform';
import { getKrComponents, KrTypesCache, KrComponents } from 'tessa/workflow';

export class KrDocumentWorkspaceInfoUIExtension extends CardUIExtension {

  public initialized(context: ICardUIExtensionContext) {
    const editor = context.uiContext.cardEditor;

    if (!editor
      || context.model.inSpecialMode
      && hasNotFlag(context.model.flags, CardModelFlags.EditTemplate)
      && hasNotFlag(context.model.flags, CardModelFlags.ViewExported)
    ) {
      return;
    }

    editor.cardModelInitialized.remove(KrDocumentWorkspaceInfoUIExtension.onCardModelInitialized);

    const usedComponents = getKrComponents(context.model.cardType.id!, null, KrTypesCache.instance);
    if (hasFlag(usedComponents, KrComponents.DocTypes)) {
      editor.cardModelInitialized.add(KrDocumentWorkspaceInfoUIExtension.onCardModelInitialized);
    }
  }

  private static async onCardModelInitialized(e: CardModelInitializingEventArgs) {
    const sections = e.cardModel.card.tryGetSections();
    if (!sections) {
      return;
    }

    const section = sections.tryGet('DocumentCommonInfo');
    if (!section) {
      return;
    }

    const docTypeTitle = section.fields.tryGet('DocTypeTitle');
    if (docTypeTitle) {
      e.workspaceInfo = docTypeTitle;
    }
  }

}