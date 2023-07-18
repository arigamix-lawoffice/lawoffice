import { ITileGlobalExtensionContext, TileExtension, disableWithCollapsing } from 'tessa/ui/tiles';
import { userSession } from 'common/utility';
import { CardSingletonCache } from 'tessa/cards';

export class KrHideLanguageAndFormattingSelectionTileExtension extends TileExtension {
  public initializingGlobal(context: ITileGlobalExtensionContext) {
    if (userSession.isAdmin) {
      return;
    }

    const mySettingsTile = context.workspace.rightPanel.tryGetTile('MySettings');
    if (!mySettingsTile) {
      return;
    }

    const settings = CardSingletonCache.instance.cards.get('KrSettings');
    if (!settings) {
      return;
    }

    const section = settings.sections.tryGet('KrSettings');
    if (!section) {
      return;
    }

    const hideLanguageSelection = section.fields.get('HideLanguageSelection');
    const hideFormatSelection = section.fields.get('HideFormattingSelection');

    if (hideLanguageSelection) {
      const selectLanguageTile = mySettingsTile.tryGetTile('SelectLanguage');
      if (selectLanguageTile) {
        disableWithCollapsing(selectLanguageTile);
      }
    }

    if (hideFormatSelection) {
      const selectFormatTile = mySettingsTile.tryGetTile('SelectFormat');
      if (selectFormatTile) {
        disableWithCollapsing(selectFormatTile);
      }
    }
  }
}
