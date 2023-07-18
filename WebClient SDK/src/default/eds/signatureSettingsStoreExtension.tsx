import { ICardStoreExtensionContext, CardStoreExtension } from 'tessa/cards/extensions';
import { CardRowState } from 'tessa/cards';
import {
  SignatureSettingsTypeName,
  SignatureSettingsCertificateSettingsSectionName,
  CertificateSettingsStartDateFieldName,
  CertificateSettingsEndDateFieldName,
  CertificateSettingsIsValidDateFieldName,
  CertificateSettingsCompanyFieldName,
  CertificateSettingsSubjectFieldName,
  CertificateSettingsIssuerFieldName
} from './helpers';

export class SignatureSettingsStoreExtension extends CardStoreExtension {
  public beforeRequest(context: ICardStoreExtensionContext) {
    if (context.cardType?.name === SignatureSettingsTypeName) {
      for (let row of context?.request.card?.sections?.get(
        SignatureSettingsCertificateSettingsSectionName
      )?.rows || []) {
        if (
          !row.get(CertificateSettingsStartDateFieldName) &&
          !row.get(CertificateSettingsEndDateFieldName) &&
          !row.get(CertificateSettingsIsValidDateFieldName) &&
          !row.get(CertificateSettingsCompanyFieldName) &&
          !row.get(CertificateSettingsSubjectFieldName) &&
          !row.get(CertificateSettingsIssuerFieldName)
        ) {
          row.state = CardRowState.Deleted;
        }
      }
    }
  }
}
