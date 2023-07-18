import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { ExternalFileExtension } from './externalFileExtension';
import { ExternalFilesFileControlExtension } from './externalFilesFileControlExtension';

ExtensionContainer.instance.registerExtension({
  extension: ExternalFileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: ExternalFilesFileControlExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
