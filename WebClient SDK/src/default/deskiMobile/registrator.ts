import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { DeskiMobileApplicationInitializationExtension } from './deskiMobileApplicationInitializationExtension';
import { DeskiMobileFileExtension } from './deskiMobileFileExtension';
import { DeskiMobileVerifyExtension } from './deskiMobileVerifyExtension';

ExtensionContainer.instance.registerExtension({
  extension: DeskiMobileApplicationInitializationExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: DeskiMobileFileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: DeskiMobileVerifyExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
