import { DeskiManager } from 'tessa/deski';
import { APP_URL } from './deskiCommon';
import { CardRequestExtension, ICardRequestExtensionContext } from 'tessa/cards/extensions';
import { CardResponse } from 'tessa/cards/service';

export class DeskiInvalidateFileContentExtension extends CardRequestExtension {
  public async beforeRequest(context: ICardRequestExtensionContext) {
    const request = context.request;
    if (
      context.requestType !== '0e7fae2d-5603-4438-911a-313a2d2c8760' || // WebInvalidateContentRequest
      !request.fileVersionId
    ) {
      return;
    }

    const response = await DeskiManager.instance.removeFile(APP_URL, request.fileVersionId, true);
    context.response = new CardResponse();
    if (!response.success) {
      context.response.validationResult.add(response.result);
    }
  }
}
