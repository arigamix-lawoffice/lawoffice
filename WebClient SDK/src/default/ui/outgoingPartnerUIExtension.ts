import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';

export class OutgoingPartnerUIExtension extends CardUIExtension {

  public initialized(context: ICardUIExtensionContext) {
    if (context.card.typeId !== 'c59b76d9-c0db-01cd-a3fb-b339740f0620') { // OutgoingTypeID
      return;
    }

    const dciSection = context.card.sections.tryGet('DocumentCommonInfo');
    if (!dciSection) {
      return;
    }

    dciSection.fields.fieldChanged.add(e => {
      if (e.fieldName === 'PartnerID') {
        e.storage.set('ReceiverName', null);
        e.storage.set('ReceiverRowID', null);
      }
    });
  }

}