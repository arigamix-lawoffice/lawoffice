import {
  CardUIExtension,
  ICardUIExtensionContext,
  CardDefaultDialog,
  CardToolbarAction,
  tileIsVisible
} from 'tessa/ui/cards';
import { showConfirmWithCancel } from 'tessa/ui';
import { openMarkedCard } from 'tessa/ui/tiles';

export class KrEditModeToolbarUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext) {
    if (context.dialogName !== CardDefaultDialog) {
      return;
    }

    if (tileIsVisible(context.card, 'KrEditMode')) {
      context.toolbar.addItemIfNotExists(
        new CardToolbarAction({
          name: 'KrEditMode',
          caption: '$KrTiles_EditMode',
          icon: 'ta icon-thin-002',
          order: 11,
          toolTip: '$KrTiles_EditModeTooltip',
          command: this.openForEditing
        }),
        {
          name: 'Alt+E',
          key: 'KeyE',
          modifiers: { alt: true }
        }
      );
    } else {
      context.toolbar.removeItemIfExists('KrEditMode');
    }
  }

  private openForEditing = async () => {
    await openMarkedCard(
      'kr_calculate_permissions',
      null, // Не требуем подтверждения действия, если не было изменений
      () => showConfirmWithCancel('$KrTiles_EditModeConfirmation')
    );
  };
}
