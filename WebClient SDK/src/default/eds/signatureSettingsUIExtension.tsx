import { CardUIExtension, ICardUIExtensionContext } from 'tessa/ui/cards';
import { GridViewModel } from 'tessa/ui/cards/controls';
import {
  SignatureSettingsTypeName,
  SignatureSettingsEncryptionDigestControlName,
  SignatureSettingsDigestAlgorithmsIDName,
  SignatureSettingsEncryptionAlgorithmIDName,
  SignatureSettingsDigestAlgorithmsOIDName,
  SignatureSettingsDigestAlgorithmsNameName
} from './helpers';

export class SignatureSettingsUIExtension extends CardUIExtension {
  public initialized(context: ICardUIExtensionContext) {
    if (context.card.typeName !== SignatureSettingsTypeName) {
      return;
    }

    const tableControl = context.model.controls.get(
      SignatureSettingsEncryptionDigestControlName
    ) as GridViewModel;
    if (!tableControl) {
      return;
    }

    tableControl.rowInitializing.add(e => {
      const control = e.rowModel?.controls.get(DigestControl);
      if (control) {
        control.isReadOnly = e.row.getField(SignatureSettingsDigestAlgorithmsIDName) == null;
      }
      e.row.fieldChanged.add(event => {
        if (event.fieldName === SignatureSettingsEncryptionAlgorithmIDName) {
          e.row.set(SignatureSettingsDigestAlgorithmsIDName, null);
          e.row.set(SignatureSettingsDigestAlgorithmsNameName, null);
          e.row.set(SignatureSettingsDigestAlgorithmsOIDName, null);
          if (control) {
            control.isReadOnly = event.fieldValue == null;
          }
        }
      });
    });
  }
}

const DigestControl = 'DigestAlgorithm';
