import { ApplicationExtension, IApplicationExtensionMetadataContext } from 'tessa';
import { CardSingletonCache, CardSection } from 'tessa/cards';
import { MapStorage } from 'tessa/platform/storage';
import { DeskiMobileService } from './deskiMobileService';

export class DeskiMobileApplicationInitializationExtension extends ApplicationExtension {
  //#region ApplicationExtension

  async afterMetadataReceived(context: IApplicationExtensionMetadataContext): Promise<void> {
    if (!context.response) {
      return;
    }

    let deskiMobileEnabled = false;
    const serverInstance = CardSingletonCache.instance.cards.get('ServerInstance');
    if (serverInstance) {
      let sections: MapStorage<CardSection>;
      let mainSection: CardSection;
      if (
        (sections = serverInstance.tryGetSections()!) &&
        (mainSection = sections.tryGet('ServerInstances')!)
      ) {
        deskiMobileEnabled = mainSection.fields.tryGet('DeskiMobileEnabled', false);
      }
    }

    DeskiMobileService.instance.init(deskiMobileEnabled);
  }

  //#endregion
}
