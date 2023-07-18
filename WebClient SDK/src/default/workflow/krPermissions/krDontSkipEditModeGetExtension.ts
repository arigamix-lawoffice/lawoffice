import { CardGetExtension, ICardGetExtensionContext } from 'tessa/cards/extensions';
import { UIContext, tryGetFromInfo } from 'tessa/ui';
import { ICardEditorModel, ICardModel, CardEditorOperationType } from 'tessa/ui/cards';
import { createTypedField, DotNetType } from 'tessa/platform';

export class KrDontSkipEditModeGetExtension extends CardGetExtension {

  public beforeRequest(context: ICardGetExtensionContext) {
    const uiContext = UIContext.current;
    let editor!: ICardEditorModel;
    let model!: ICardModel;
    if ((editor = uiContext.cardEditor!)
      && (model = editor.cardModel!)
      // Флаг сохраняем только при открытии при сохранении карточки, рефреш карточки скидывает режим редактирования
      && editor.currentOperationType === CardEditorOperationType.SaveAndRefresh
    ) {
      const card = model.card;
      if (tryGetFromInfo(card.info, 'kr_permissions_calculated', false)) {
        context.request.info['kr_calculate_permissions'] = createTypedField(true, DotNetType.Boolean);
        // Тащим и этот признак на сервер, чтобы не отображать информационное сообщение
        // о выдаче прав после сохранения карточки
        context.request.info['kr_permissions_calculated'] = createTypedField(true, DotNetType.Boolean);
      }
    }
  }

}