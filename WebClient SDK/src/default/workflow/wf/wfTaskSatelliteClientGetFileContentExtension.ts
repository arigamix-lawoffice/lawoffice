import { CardGetFileContentExtension, ICardGetFileContentExtensionContext } from 'tessa/cards/extensions';
import { UIContext, tryGetFromInfo } from 'tessa/ui';
import { ICardModel } from 'tessa/ui/cards';
import { getOrUpdateCardDigest } from 'tessa/cards';
import { createTypedField, DotNetType } from 'tessa/platform';

export class WfTaskSatelliteClientGetFileContentExtension extends CardGetFileContentExtension {

  public async beforeRequest(context: ICardGetFileContentExtensionContext) {
    const editor = UIContext.current.cardEditor;
    const request = context.request;
    let model: ICardModel;
    if (editor
      && (model = editor.cardModel!)
      && model.cardType.id === 'de75a343-8164-472d-a20e-4937819760ac' // WfTaskCard
      && !tryGetFromInfo(request.tryGetInfo()!, '.digest', null)
    ) {
      const digest = await getOrUpdateCardDigest(model);
      if (digest) {
        request.info['.digest'] = createTypedField(digest, DotNetType.String);
      }
      return;
    }
  }

}