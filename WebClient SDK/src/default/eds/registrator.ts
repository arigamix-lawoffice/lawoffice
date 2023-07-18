import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { EDSProviderInitializeExtension } from './edsProviderInitializeExtension';
import { SignatureSettingsStoreExtension } from './signatureSettingsStoreExtension';
import { SignatureSettingsUIExtension } from './signatureSettingsUIExtension';

ExtensionContainer.instance.registerExtension({
  extension: EDSProviderInitializeExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});

ExtensionContainer.instance.registerExtension({
  extension: SignatureSettingsStoreExtension,
  stage: ExtensionStage.BeforePlatform,
  singleton: true
});

ExtensionContainer.instance.registerExtension({
  extension: SignatureSettingsUIExtension,
  stage: ExtensionStage.AfterPlatform
});
