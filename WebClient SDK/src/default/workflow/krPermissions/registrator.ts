import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { KrDontSkipEditModeGetExtension } from './krDontSkipEditModeGetExtension';
import { KrKeepReadCardPermissionGetExtension } from './krKeepReadCardPermissionGetExtension';
import { KrKeepReadCardPermissionStoreExtension } from './krKeepReadCardPermissionStoreExtension';
import { KrTokenToTaskHistoryViewUIExtension } from './krTokenToTaskHistoryViewUIExtension';
ExtensionContainer.instance.registerExtension({
  extension: KrDontSkipEditModeGetExtension,
  stage: ExtensionStage.BeforePlatform,
  order: 1,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: KrKeepReadCardPermissionGetExtension,
  stage: ExtensionStage.BeforePlatform,
  order: 2,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: KrKeepReadCardPermissionStoreExtension,
  stage: ExtensionStage.BeforePlatform,
  order: 3,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: KrTokenToTaskHistoryViewUIExtension,
  stage: ExtensionStage.BeforePlatform,
  order: 4,
  singleton: true
});
