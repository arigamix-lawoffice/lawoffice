import { IApplicationExtensionMetadataContext } from 'tessa';
import { ApplicationExtension } from 'tessa/applicationExtension';
import { DeskiMobileOperation, DeskiMobileService } from './deskiMobileService';
import Platform from 'common/platform';
import { FileContainer, FileSignatureState, IFileVersion } from 'tessa/files';
import { showError, showNotEmpty, tryGetFromInfo, UIContext } from 'tessa/ui';
import { startOperation, validateTrackingDeskiMobile } from './helper';
import { PageLifecycleSingleton } from 'common';
import { ICardModel } from 'tessa/ui/cards';
import { getPOJO, ISignatureValidationInfo } from 'tessa/cards';
import { showFileSigns } from 'tessa/ui/cards/controls';
import { VerifySignaturesHelpers, VerifyProps } from 'common/utility/verifySignaturesHelpers';
import { OperationResponse } from 'tessa/platform/operations';

export class DeskiMobileVerifyExtension extends ApplicationExtension {
  //#region ApplicationExtension

  async afterMetadataReceived(_context: IApplicationExtensionMetadataContext): Promise<void> {
    if (!DeskiMobileService.instance.deskiMobileEnabled || !Platform.isMobile()) {
      return;
    }
    VerifySignaturesHelpers.instance.verifySignatures = this.verifyInDeskiMobile;
  }

  // #endregion

  //#region private methods

  private async verifyInDeskiMobile(props: VerifyProps): Promise<void> {
    const fileRequest = props.file.source.getLinkData(props.file);
    if (!fileRequest) {
      await showError('FileResult is null.');
      return;
    }
    const operationResult = await startOperation(fileRequest, 'verify');
    if (!operationResult) {
      return;
    }

    const showConfirmBeforeUnload = PageLifecycleSingleton.instance.showConfirmBeforeUnload;
    PageLifecycleSingleton.instance.showConfirmBeforeUnload = false;
    window.location.assign(operationResult.link);
    setTimeout(() => {
      PageLifecycleSingleton.instance.showConfirmBeforeUnload = showConfirmBeforeUnload;
    }, 100);

    const operationId = await validateTrackingDeskiMobile(operationResult.operationId);
    if (!operationId) {
      return;
    }

    const [response, validationResult] = await DeskiMobileService.instance.trackingOperation(
      DeskiMobileOperation.verify,
      operationId
    );

    if (!validationResult.isSuccessful) {
      await showNotEmpty(validationResult);
      return;
    }

    // операция отменена
    if (typeof response === 'boolean') {
      return;
    }

    const cardModel = UIContext.current.cardEditor?.cardModel;

    if (props.onClose) {
      props.onClose();
    }

    await DeskiMobileVerifyExtension.getActualVerifyOperationResult(
      cardModel,
      props.fileContainer,
      props.file.lastVersion,
      response
    );
  }

  private static async getActualVerifyOperationResult(
    cardModel: ICardModel | null | undefined,
    fileContainer: FileContainer,
    lastVersion: IFileVersion,
    response: OperationResponse
  ): Promise<void> {
    try {
      const info = response.tryGetInfo();
      if (!info) {
        await showError('Info parameter not found in operation response.');
        return;
      }
      const statusDataObject = info.StatusData;
      if (statusDataObject) {
        const canceled = tryGetFromInfo<boolean>(statusDataObject, 'Canceled', false);
        // Действие отменено пользователем в DeskiMobile
        if (canceled) {
          return;
        }
      }

      const signatureValidationObject = info.SignatureValidationInfo;
      if (!signatureValidationObject) {
        await showError('SignatureValidationInfo not found in operation response.');
        return;
      }

      const signatureValidation: { ID: guid; ValidationInfo: ISignatureValidationInfo[] }[] =
        getPOJO(signatureValidationObject);

      for (const signRow of lastVersion.signatures) {
        let state: FileSignatureState = FileSignatureState.Checked;
        let error = '';

        const item = signatureValidation.find(e => e.ID === signRow.id);
        if (!item) {
          await showError('SignatureInfo in server not contain SignatureInfo from web client.');
          return;
        }

        const validationInfos = item.ValidationInfo;
        if (validationInfos.some(x => x.state === FileSignatureState.Failed)) {
          state = FileSignatureState.Failed;
          const firstInfo = validationInfos.find(x => x.state === FileSignatureState.Failed)!;
          error = firstInfo.signingCertificateValidityDesc;
        } else if (validationInfos.some(x => x.state !== FileSignatureState.Checked)) {
          state = FileSignatureState.CheckedWithWarning;
        }

        signRow.updateState(state, validationInfos, error);
      }

      const signatureValidationDataObject = info.SignatureValidationData;
      if (!signatureValidationDataObject) {
        await showError('signatureValidationData not found in operation response.');
        return;
      }

      const showValidationDialogFlag = tryGetFromInfo<boolean>(
        signatureValidationDataObject,
        'ShowValidationDialog',
        true
      );

      if (showValidationDialogFlag && cardModel) {
        await showFileSigns(fileContainer, cardModel.edsProvider, lastVersion);
      }
    } catch (err) {
      await showError(err);
    }
  }

  // #endregion
}
