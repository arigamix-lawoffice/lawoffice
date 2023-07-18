import { DefaultCardTypes, plainColumnName } from 'tessa/workflow';
import {
  IKrStageTypeUIHandlerContext,
  KrStageTypeUIHandler,
  StageTypeHandlerDescriptor,
  signingDescriptor
} from 'tessa/workflow/krProcess';

import { CardFieldChangedEventArgs } from 'tessa/cards';
import { IControlViewModel } from 'tessa/ui/cards';
import { TabControlViewModel } from 'tessa/ui/cards/controls';
import { Visibility } from 'tessa/platform';

/**
 * UI обработчик типа этапа {@link signingDescriptor}.
 */
export class SigningUIHandler extends KrStageTypeUIHandler {
  //#region fields

  private static readonly notReturnEdit = plainColumnName(
    'KrSigningStageSettingsVirtual',
    'NotReturnEdit'
  );

  private _returnIfNotSignedFlagControl?: IControlViewModel;
  private _returnAfterSigningFlagControl?: IControlViewModel;

  //#endregion

  //#region base overrides

  public descriptors(): StageTypeHandlerDescriptor[] {
    return [signingDescriptor];
  }

  public async initialize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    let flagsTabs: IControlViewModel | undefined;

    if (
      (flagsTabs = context.settingsForms
        .find(i => i.name === DefaultCardTypes.KrSigningStageTypeSettingsTypeName)
        ?.blocks.find(i => i.name === 'SigningStageFlags')
        ?.controls.find(x => x.name === 'FlagsTabs')) &&
      flagsTabs instanceof TabControlViewModel
    ) {
      this._returnIfNotSignedFlagControl = flagsTabs.tabs
        .find(x => x.name === 'CommonSettings')
        ?.blocks.find(x => x.name === 'StageFlags')
        ?.controls.find(x => x.name === 'ReturnIfNotSigned');

      this._returnAfterSigningFlagControl = flagsTabs.tabs
        .find(x => x.name === 'AdditionalSettings')
        ?.blocks.find(x => x.name === 'StageFlags')
        ?.controls.find(x => x.name === 'ReturnAfterSigning');
    }

    context.row.fieldChanged.add(this.onSettingsFieldChanged);

    this.notReturnEditConfigureFields(context.row.tryGet(SigningUIHandler.notReturnEdit, false));
  }

  public async finalize(context: IKrStageTypeUIHandlerContext): Promise<void> {
    context.row.fieldChanged.remove(this.onSettingsFieldChanged);
  }

  //#endregion

  //#region private methods

  private onSettingsFieldChanged = (e: CardFieldChangedEventArgs) => {
    if (e.fieldName === SigningUIHandler.notReturnEdit) {
      this.notReturnEditConfigureFields(e.fieldValue);
    }
  };

  private notReturnEditConfigureFields(isNotReturnEdit: boolean) {
    if (isNotReturnEdit) {
      if (this._returnIfNotSignedFlagControl) {
        this._returnIfNotSignedFlagControl.controlVisibility = Visibility.Collapsed;
      }

      if (this._returnAfterSigningFlagControl) {
        this._returnAfterSigningFlagControl.controlVisibility = Visibility.Collapsed;
      }
    } else {
      if (this._returnIfNotSignedFlagControl) {
        this._returnIfNotSignedFlagControl.controlVisibility = Visibility.Visible;
      }

      if (this._returnAfterSigningFlagControl) {
        this._returnAfterSigningFlagControl.controlVisibility = Visibility.Visible;
      }
    }
  }

  //#endregion
}
