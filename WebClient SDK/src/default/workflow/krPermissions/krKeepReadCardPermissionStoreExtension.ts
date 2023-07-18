import { ICardStoreExtensionContext, CardStoreExtension } from 'tessa/cards/extensions';
import { UIContext } from 'tessa/ui';
import { ICardEditorModel, ICardModel, CardEditorOperationType } from 'tessa/ui/cards';
import { KrToken } from 'tessa/workflow';
import { Guid } from 'tessa/platform';

export class KrKeepReadCardPermissionStoreExtension extends CardStoreExtension {
  public afterRequest(context: ICardStoreExtensionContext): void {
    const uiContext = UIContext.current;
    let editor!: ICardEditorModel;
    let model!: ICardModel;
    if (
      (editor = uiContext.cardEditor!) &&
      (model = editor.cardModel!) &&
      // Флаг сохраняем только при открытии при сохранении карточки, рефреш карточки скидывает режим редактирования
      (editor.currentOperationType === CardEditorOperationType.SaveAndRefresh ||
        editor.currentOperationType === CardEditorOperationType.Open)
    ) {
      let token: KrToken | null = null;
      if (context.response) {
        token = KrToken.tryGet(context.response.info);
      }

      if (
        token &&
        (Guid.equals(token.cardId, model.card.id) || Guid.equals(token.cardId, Guid.empty))
      ) {
        token.setInfo(context.request.info);
      }
    }
  }
}
