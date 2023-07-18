import { CardUIExtension, ICardUIExtensionContext, CardEditorOperationType } from 'tessa/ui/cards';
import { tryGetFromInfo } from 'tessa/ui';
import { DotNetType, createTypedField } from 'tessa/platform';

/**
 * Расширение на обновление виртуальной карточки состояния документа со стороны клиента.
 */
export class KrDocStateUIExtension extends CardUIExtension {

  public shouldExecute(context: ICardUIExtensionContext) {
    return context.card.typeId === 'e83a230a-f5fc-445e-9b44-7d0140ee69f6'; // KrDocStateTypeID
  }

  public reopening(context: ICardUIExtensionContext) {
    const editor = context.uiContext.cardEditor;
    if (editor == null
      || context.model == null
      || context.getRequest == null
    ) {
        return;
    }

    let stateId: number | null = null;
    if (editor.currentOperationType === CardEditorOperationType.SaveAndRefresh) {
      // идентификатор этой карточки можно менять, поэтому актуальный идентификатор при рефреше после сохранения будет в той карточке,
      // которая сохранялась (и была успешно сохранена, раз запущен рефреш)
      stateId = context.model.card.sections.get('KrDocStateVirtual')!.fields.get('StateID');
    }

    if (stateId == null) {
      // либо это обновление без сохранения, либо по какой-то причине поле было пустым
      stateId = tryGetFromInfo<number | null>(context.model.card.tryGetInfo() || {}, 'StateID');
    }

    if (stateId == null) {
      return;
    }

    context.getRequest.info['StateID'] = createTypedField(stateId, DotNetType.Int32);
  }

}