import { MetadataStorage } from 'tessa';
import { CardNewRequest, CardService } from 'tessa/cards/service';
import { CardTypeNamedFormSealed } from 'tessa/cards/types';
import { Guid } from 'tessa/platform';
import { showNotEmpty, createCardModel } from 'tessa/ui';
import { ICardModel } from 'tessa/ui/cards';

/**
 * Helper for working with dialogs
 */
export abstract class DialogHelper {
  /**
   * Get the model and shape of the dialog
   * @param dialogName Name of the dialog card
   * @returns [Dialog Model, Dialog Form]
   */
  public static async GetDialogModelAsync(dialogName: string): Promise<[ICardModel | null, CardTypeNamedFormSealed | null]> {
    const dialogsType = MetadataStorage.instance.cardMetadata.getCardTypeByName(dialogName);
    if (!dialogsType) {
      return [null, null];
    }

    const dialogForm = dialogsType.forms[0];
    if (!dialogForm) {
      return [null, null];
    }

    const request = new CardNewRequest();
    request.cardTypeId = dialogsType.id;
    const response = await CardService.instance.new(request);
    response.card.id = Guid.newGuid();
    await showNotEmpty(response.validationResult.build());

    if (!response.validationResult.isSuccessful) {
      return [null, null];
    }
    const windowCardModel = createCardModel(response.card, response.sectionRows);

    return [windowCardModel, dialogForm];
  }
}
