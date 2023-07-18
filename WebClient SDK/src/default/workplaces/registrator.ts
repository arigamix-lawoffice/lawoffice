import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import {
  ManagerWorkplaceExtension,
  ManagerWorkplaceInitializeExtension
} from './manager/managerWorkplaceExtension';
import { RefSectionExtension } from './refSectionExtension';
import { AutomaticNodeRefreshExtension } from './automaticNodeRefreshExtension';
import { ChartoViewExtension, ChartoInitializeExtension } from './chart-o/chartoExtension';
ExtensionContainer.instance.registerExtension({
  extension: ManagerWorkplaceExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: ManagerWorkplaceInitializeExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: RefSectionExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: AutomaticNodeRefreshExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: ChartoViewExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: ChartoInitializeExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
