import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { HideEmptyIncomingReferencesControl } from './hideEmptyIncomingReferencesControl';
ExtensionContainer.instance.registerExtension({
  extension: HideEmptyIncomingReferencesControl,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
