import { DeskiManager } from 'tessa/deski';
import { APP_URL } from './deskiCommon';
import {
  CardGetFileContentExtension,
  ICardGetFileContentExtensionContext
} from 'tessa/cards/extensions';
import Platform from 'common/platform';
import { tryGetFromInfo } from 'tessa/ui';
import { CardGetFileContentResponse } from 'tessa/cards/service';
import { createTypedField, DotNetType } from 'tessa/platform';
import { IStorage } from 'tessa/platform/storage';
import { ValidationResultType } from 'tessa/platform/validation';
import { DeskiIgnoreGetContentExtensionKey } from 'tessa/files';

export class DeskiFileContentExtension extends CardGetFileContentExtension {
  public async beforeRequest(context: ICardGetFileContentExtensionContext) {
    if (!DeskiManager.instance.deskiAvailable || Platform.isMobile()) {
      return;
    }

    const request = context.request;

    // tslint:disable-next-line: triple-equals
    if (this.isVirtualFile(context) || request.versionRowId == undefined) {
      return;
    }

    const info = request.tryGetInfo();
    if (info && tryGetFromInfo<boolean>(info, DeskiIgnoreGetContentExtensionKey, false)) {
      delete info[DeskiIgnoreGetContentExtensionKey];
      return;
    }

    // если есть поле .deskiContentModified в info, значит запрос контента идет для редактируемого файла
    // .deskiContentModified содержит время редактирования текущего контента или null, если контент еще не редактировался
    const hasContentModified = !!info && '.deskiContentModified' in info;
    if (hasContentModified) {
      await this.requestForEditableFile(context, info!);
    } else {
      await this.requestForReadonlyFile(context);
    }
  }

  private isVirtualFile(context: ICardGetFileContentExtensionContext) {
    // tslint:disable-next-line: triple-equals
    return context.request.fileTypeId == undefined;
  }

  private async requestForEditableFile(
    context: ICardGetFileContentExtensionContext,
    info: IStorage
  ) {
    const request = context.request;
    const contentModified = tryGetFromInfo<string | null>(info, '.deskiContentModified', null);
    const deskiOriginalVersionId = tryGetFromInfo<guid | null>(
      info,
      '.deskiOriginalVersionId',
      null
    );
    const versionRowId = deskiOriginalVersionId ?? request.versionRowId!;
    delete info['.deskiContentModified'];
    delete info['.deskiOriginalVersionId'];

    const { info: fileInfo, result } = await DeskiManager.instance.getFileInfo(
      APP_URL,
      versionRowId,
      true
    );

    if (result) {
      context.validationResult.add(result);
      return;
    }

    // ошибку сохранили выше
    if (!fileInfo) {
      return;
    }

    // если контент не поменялся, то отдаем пустой респонс
    if (
      (contentModified &&
        new Date(contentModified).getTime() === new Date(fileInfo.Modified).getTime()) ||
      !fileInfo.IsModified
    ) {
      const fileContentResponse = new CardGetFileContentResponse();
      context.response = fileContentResponse;
      return;
    }

    const fileContentResult = await DeskiManager.instance.getFileData(APP_URL, versionRowId);
    if (!fileContentResult.data) {
      context.validationResult.add(
        ValidationResultType.Error,
        `Can not find file content in deski. ID: ${versionRowId}`
      );
      return;
    }

    const fileContentResponse = new CardGetFileContentResponse();
    const content = fileContentResult.data;
    const fileName = fileInfo.Name || request.fileName;
    fileContentResponse.setContent(content, fileName);
    fileContentResponse.info['.deskiContentModified'] = createTypedField(
      fileInfo.Modified,
      DotNetType.DateTime
    );
    context.response = fileContentResponse;
  }

  private async requestForReadonlyFile(context: ICardGetFileContentExtensionContext) {
    const request = context.request;
    const versionRowId = request.versionRowId!;
    try {
      const { info: fileInfo } = await DeskiManager.instance.getFileInfo(
        APP_URL,
        versionRowId,
        false
      );

      // не нашли файл в deski
      if (!fileInfo) {
        return;
      }

      const fileContentResult = await DeskiManager.instance.getContent(APP_URL, versionRowId);
      if (!fileContentResult.data) {
        return;
      }

      const fileContentResponse = new CardGetFileContentResponse();
      const content = fileContentResult.data;
      const fileName = fileInfo.Name || request.fileName;
      fileContentResponse.setContent(content, fileName);
      context.response = fileContentResponse;
    } catch (err) {}
  }
}
