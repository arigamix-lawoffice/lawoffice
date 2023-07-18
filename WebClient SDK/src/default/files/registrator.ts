import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { ClientKrPermissionsGetFileContentExtension } from './clientKrPermissionsGetFileContentExtension';
import { ClientKrPermissionsGetFileVersionsExtension } from './clientKrPermissionsGetFileVersionsExtension';
import { WfCardFileExtension } from './wfCardFileExtension';
import { KrAddCycleGroupingFileControlExtension } from './krAddCycleGroupingFileControlExtension';
import { KrCurrentCycleFileControlExtension } from './krCurrentCycleFileControlExtension';
import { ClientFileTemplatePermissionsGetFileContentExtension } from './clientFileTemplatePermissionsGetFileContentExtension';

ExtensionContainer.instance.registerExtension({
  extension: KrAddCycleGroupingFileControlExtension,
  stage: ExtensionStage.BeforePlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ClientFileTemplatePermissionsGetFileContentExtension,
  stage: ExtensionStage.BeforePlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: ClientKrPermissionsGetFileContentExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: ClientKrPermissionsGetFileVersionsExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: WfCardFileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: KrCurrentCycleFileControlExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
