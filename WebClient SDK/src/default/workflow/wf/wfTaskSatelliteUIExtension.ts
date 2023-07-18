import {
  CardUIExtension,
  ICardUIExtensionContext,
  CardModelFlags,
  CardModelInitializingEventArgs
} from 'tessa/ui/cards';
import { hasNotFlag } from 'tessa/platform';

export class WfTaskSatelliteUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext) {
    if (
      (context.model.inSpecialMode &&
        hasNotFlag(context.model.flags, CardModelFlags.EditTemplate) &&
        hasNotFlag(context.model.flags, CardModelFlags.ViewExported)) ||
      !context.uiContext.cardEditor
    ) {
      return;
    }

    const editor = context.uiContext.cardEditor;
    editor.cardModelInitialized.remove(WfTaskSatelliteUIExtension.onCardModelInitialized);
    if (context.model.cardType.id === 'de75a343-8164-472d-a20e-4937819760ac') {
      editor.cardModelInitialized.add(WfTaskSatelliteUIExtension.onCardModelInitialized);

      const taskHistory = context.model.tryGetTaskHistory();
      if (taskHistory) {
        taskHistory.hideOpenViewCommand = true;
      }
    }
  }

  public static async onCardModelInitialized(e: CardModelInitializingEventArgs) {
    const sections = e.cardModel.card.tryGetSections();
    if (!sections) {
      return;
    }

    const section = sections.tryGet('WfTaskCardsVirtual');
    const docTypeTitle = !!section ? section.fields.tryGet('DocTypeTitle') : null;

    if (!docTypeTitle) {
      e.workspaceInfo = docTypeTitle;
    }
  }
}
