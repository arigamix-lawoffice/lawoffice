import React from 'react';
import { Dialog, DialogContainer, DialogContent, DialogFooter, RaisedButton } from 'ui';
import { getTessaIcon } from 'common';
import { localize } from 'tessa/localization';
import { observer } from 'mobx-react';
import { OcrProgressDialogOptions } from './ocrProgressDialogOptions';
import { OcrProgressDialogViewModel } from './ocrProgressDialogViewModel';
import './ocrProgressDialogStyle.css';

/** Пропсы диалога прогресса OCR. */
interface OcrProgressDialogProps {
  /** Модель представления диалога прогресса OCR. */
  viewModel: OcrProgressDialogViewModel;
  /** Действие, выполняемое при закрытии диалога прогресса OCR.*/
  onClose: (option: OcrProgressDialogOptions) => void;
}

/** Диалог отслеживания прогресса операции. */
export const OcrProgressDialog: React.FC<OcrProgressDialogProps> = observer(
  ({ viewModel, onClose }) => {
    if (viewModel.progress === 100) {
      onClose(OcrProgressDialogOptions.Completed);
    }

    const handleCloseFormWithContinueInBackground = () => {
      onClose(OcrProgressDialogOptions.ContinueInBackground);
    };

    const handleCloseFormWithCancel = () => {
      onClose(OcrProgressDialogOptions.Cancel);
    };

    return (
      <Dialog isOpened={true} noPortal={true} isAutoSize={false} className="progress-dialog">
        <DialogContainer>
          <DialogContent className="progress-dialog-content">
            {localize('$UI_Common_Splash_TextRecognitionProgress', viewModel.progress)}
          </DialogContent>
          <DialogFooter className="default-footer">
            <RaisedButton
              key="continueInBackground"
              icon={getTessaIcon('Thin254')}
              onClick={handleCloseFormWithContinueInBackground}
              label={localize('$UI_Common_ContinueInBackground')}
            />
            <RaisedButton
              key="cancel"
              icon={getTessaIcon('Thin253')}
              disabled={!viewModel.canCancel}
              onClick={handleCloseFormWithCancel}
              label={localize('$UI_Common_Cancel')}
            />
          </DialogFooter>
        </DialogContainer>
      </Dialog>
    );

    //#endregion
  }
);
