import { ISignedData, SignatureProfile, SignatureType } from 'tessa/cards';
import { CertData, FileVersionState } from 'tessa/files';
import { FileExtension, FileExtensionContext } from 'tessa/ui/files';
import { MenuAction, tryGetFromInfo } from 'tessa/ui';
import { MessageBoxButtons, show, showError, showNotEmpty } from 'tessa/ui/tessaDialog';
import { PageLifecycleSingleton } from 'common';
import Platform from 'common/platform';
import { DeskiMobileOperation, DeskiMobileService } from './deskiMobileService';
import { startOperation, validateTrackingDeskiMobile } from './helper';
import { VerifySignaturesHelpers } from 'common/utility/verifySignaturesHelpers';

export class DeskiMobileFileExtension extends FileExtension {
  public openingMenu(context: FileExtensionContext): void {
    if (!DeskiMobileService.instance.deskiMobileEnabled || !Platform.isMobile()) {
      return;
    }

    const control = context.control;
    const fileVM = context.file;
    const file = fileVM.model;
    const singleMode = context.files.length === 1;
    const canUseSignatures = file.permissions.canUseSignatures;
    const hasSignatures = file.lastVersion.signatures.length > 0;
    const hasValidState =
      file.lastVersion.state === FileVersionState.Success ||
      file.lastVersion.state === FileVersionState.Created;

    const signIndex = context.actions.findIndex(x => x.name === 'SignEDS');
    if (signIndex >= 0) {
      const signDSCollapsed = !(
        singleMode &&
        canUseSignatures &&
        file.permissions.canSign &&
        hasValidState
      );
      context.actions.splice(
        signIndex,
        1,
        new MenuAction(
          'SignEDS',
          '$UI_Controls_FilesControl_SignDS',
          'icon-thin-005',
          async () => {
            if (file.lastVersion.state === FileVersionState.Created) {
              await show({
                text: '$UI_Cards_CannotPerformEDSOperation',
                buttons: MessageBoxButtons.OK
              });
              return;
            }

            const fileRequest = file.source.getLinkData(file);
            if (!fileRequest) {
              await showError('FileResult is null.');
              return;
            }

            const operationResult = await startOperation(fileRequest, 'sign');
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

            const [response, validationResult] =
              await DeskiMobileService.instance.trackingOperation(
                DeskiMobileOperation.sign,
                operationId
              );

            if (!validationResult.isSuccessful) {
              await showNotEmpty(validationResult);
              return;
            }
            // операция отменена
            if (response === true) {
              return;
            }

            if (response === false) {
              await showError('Operation response is null.');
              return;
            }

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

            const certDataObject = info.CertData;
            if (!certDataObject) {
              await showError('CertData not found in operation response.');
              return;
            }

            const certData: CertData = {
              subjectName: tryGetFromInfo<string>(certDataObject, 'SubjectName', ''),
              issuerName: tryGetFromInfo<string>(certDataObject, 'IssuerName', ''),
              validFrom: tryGetFromInfo<string>(certDataObject, 'ValidFrom', ''),
              validTo: tryGetFromInfo<string>(certDataObject, 'ValidTo', ''),
              company: tryGetFromInfo<string>(certDataObject, 'Company', ''),
              serialNumber: tryGetFromInfo<string>(certDataObject, 'SerialNumber', ''),
              thumbprint: tryGetFromInfo<string>(certDataObject, 'Thumbprint', ''),
              certificateStr: tryGetFromInfo<string>(certDataObject, 'Certificate', '')
            };

            const signedDataObject = info.SignedData;
            if (!signedDataObject) {
              await showError('SignedData not found in operation response.');
              return;
            }

            const strType = tryGetFromInfo<string>(signedDataObject, 'Type', '');
            const type: SignatureType = SignatureType[strType] ?? SignatureType.None;

            const strProfile = tryGetFromInfo<string>(signedDataObject, 'Profile', '');
            const profile: SignatureProfile = SignatureProfile[strProfile] ?? SignatureProfile.None;

            const signedData: ISignedData = {
              signature: tryGetFromInfo<string>(signedDataObject, 'Signature', ''),
              type,
              profile
            };

            await control.fileContainer.addSignature(file.lastVersion, certData, signedData, '');
          },
          null,
          signDSCollapsed
        )
      );
    }

    const checkIndex = context.actions.findIndex(x => x.name === 'CheckEDS');
    if (checkIndex >= 0) {
      const checkDSCollapsed = !(singleMode && canUseSignatures && hasSignatures && hasValidState);
      context.actions.splice(
        checkIndex,
        1,
        new MenuAction(
          'CheckEDS',
          '$UI_Controls_FilesControl_CheckDS',
          'icon-thin-091',
          async () => {
            if (
              file.lastVersion.state === FileVersionState.Created ||
              file.lastVersion.signaturesAdded.length > 0
            ) {
              // 1. файл добавлен и не сохранен
              // 2. после подписи файл не был сохранен
              await show({
                text: '$UI_Cards_CannotPerformEDSOperation',
                buttons: MessageBoxButtons.OK
              });
              return;
            }

            if (VerifySignaturesHelpers.instance.verifySignatures) {
              await VerifySignaturesHelpers.instance.verifySignatures({
                file,
                fileContainer: control.fileContainer
              });
            }
          },
          null,
          checkDSCollapsed
        )
      );
    }
  }
}
