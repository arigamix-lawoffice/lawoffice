import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { AddTopics, KrToken, SuperModeratorMode } from 'tessa/workflow';
import { ForumViewModel } from 'tessa/ui/cards/controls';

export class ForumControlUIExtension extends CardUIExtension {
  public initializing(_context: ICardUIExtensionContext): void {
    let token: KrToken | null;
    _context.model.controlInitializers.push(control => {
      if (!(control instanceof ForumViewModel)) {
        return;
      }

      token = KrToken.tryGet(_context.card.info);
      if (!token) {
        return;
      }

      control.isAddTopicEnabled = token.hasPermission(AddTopics);
      control.isSuperModeratorModeEnabled = token.hasPermission(SuperModeratorMode);
    });
  }
}
