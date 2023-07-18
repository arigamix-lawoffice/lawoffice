import { CardStoreExtension, ICardStoreExtensionContext } from 'tessa/cards/extensions';
import { KrToken } from 'tessa/workflow';
import { UIContext } from 'tessa/ui';
import { ICardEditorModel, ICardModel } from 'tessa/ui/cards';

export class AcquaintanceClientStoreExtension extends CardStoreExtension {

  public afterRequest(context: ICardStoreExtensionContext) {
    if (!context.requestIsSuccessful) {
      return;
    }

    const token = KrToken.tryGet(context.response!.info);
    const uiContext = UIContext.current;

    let editor: ICardEditorModel;
    let model: ICardModel;
    if (token
      && (editor = uiContext.cardEditor!)
      && (model = editor.cardModel!)
      && '.SaveWithPermissionsCalc' in context.request.info
      && context.response!.cardVersion !== model.card.version
    ) {
      token.setInfo(editor.info);
    }
  }

}