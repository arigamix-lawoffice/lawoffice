import { CardGetExtension, ICardGetExtensionContext } from 'tessa/cards/extensions';
import { UIContext } from 'tessa/ui';
import { ICardEditorModel, ICardModel, CardEditorOperationType } from 'tessa/ui/cards';
import { KrToken } from 'tessa/workflow';
import { Guid } from "tessa/platform";

export class KrKeepReadCardPermissionGetExtension extends CardGetExtension {

  public beforeRequest(context: ICardGetExtensionContext) {
    const uiContext = UIContext.current;
    let editor!: ICardEditorModel;
    let model!: ICardModel;
    if ((editor = uiContext.cardEditor!)
      && (model = editor.cardModel!)
      // Флаг сохраняем только при открытии при сохранении карточки, рефреш карточки скидывает режим редактирования
      && (editor.currentOperationType === CardEditorOperationType.SaveAndRefresh
        || editor.currentOperationType === CardEditorOperationType.Open)
    ) {
      if (KrToken.contains(context.request.info)) {
        return;
      }

      const token = KrToken.tryGet(model.card.info);
      if (token && (Guid.equals(token.cardId, context.request.cardId) || Guid.equals(token.cardId, Guid.empty))) {
        token.setInfo(context.request.info);
      }
    }
  }

}