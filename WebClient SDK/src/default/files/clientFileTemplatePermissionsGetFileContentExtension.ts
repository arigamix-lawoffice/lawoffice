import {
  CardGetFileContentExtension,
  ICardGetFileContentExtensionContext
} from 'tessa/cards/extensions';
import { UIContext } from 'tessa/ui';
import { KrToken } from 'tessa/workflow';
import { ICardModel } from 'tessa/ui/cards';
import { CardTemplateHelper } from 'tessa/cards';

export class ClientFileTemplatePermissionsGetFileContentExtension extends CardGetFileContentExtension {
  public beforeRequest(context: ICardGetFileContentExtensionContext): void {
    const editor = UIContext.current.cardEditor;
    let model: ICardModel;
    let token: KrToken;

    if (
      editor &&
      (model = editor.cardModel!) &&
      (token = KrToken.tryGet(model.card.info)!) &&
      context.request.versionRowId === CardTemplateHelper.ReplacePlaceholdersVersionRowID &&
      model.card.id === context.request.info[CardTemplateHelper.PlaceholderCurrentCardIDInfo]
    ) {
      token.setInfo(context.request.info);
    }
  }
}
