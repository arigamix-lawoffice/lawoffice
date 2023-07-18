import { CardRequestExtension, ICardRequestExtensionContext } from 'tessa/cards/extensions';
import { UIContext, tryGetFromInfo } from 'tessa/ui';
import { CardInstanceType } from 'tessa/cards';
import { TypedField, DotNetType, createTypedField } from 'tessa/platform';
import { CardResponse, GetTypeIDList } from 'tessa/cards/service';

export class CompletionOptionGetTypeIdListRequestExtension extends CardRequestExtension {

  public shouldExecute(context: ICardRequestExtensionContext) {
    return context.requestType === GetTypeIDList;
  }

  public beforeRequest(context: ICardRequestExtensionContext) {
    // расширение обычно вызывается при удалении или при экспорте из представления; экспорт запрещён, так что это удаление
    const viewContext = UIContext.current.viewContext;
    if (!viewContext
      || !viewContext.view
      || viewContext.view.metadata.alias !== 'CompletionOptionCards'
    ) {
      return;
    }

    let instanceIdListCount: number = 0;
    let instanceType: CardInstanceType | null = null;
    const requestInfo = context.request.tryGetInfo();
    if (requestInfo) {
      instanceType = tryGetFromInfo(requestInfo, '.instanceType', null);
      const idList = tryGetFromInfo<TypedField[] | null>(requestInfo, '.instanceIDList', null);
      if (idList) {
        instanceIdListCount = idList.length;
      }
    }

    if (instanceType !== CardInstanceType.Card) {
      return;
    }

    const types: TypedField<DotNetType.Guid>[] = [];
    for (let i = 0; i < instanceIdListCount; i++) {
      types.push(createTypedField('f6b95639-234e-4800-a2f1-3cb20e0bcda4', DotNetType.Guid)); // CompletionOptionTypeID
    }

    const response = new CardResponse();
    response.info['.typeIDList'] = types;

    context.response = response;
  }

}