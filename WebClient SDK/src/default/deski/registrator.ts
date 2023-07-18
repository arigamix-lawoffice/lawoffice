import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import {
  DeskiExtension,
  DeskiFileExtension,
  DeskiUIExtension,
  DeskiFileControlExtension,
  DeskiFileVersionExtension,
  DeskiViewFileControlExtension
} from './deskiExtension';
import { DeskiFileContentExtension } from './deskiFileContentExtension';
import { DeskiInvalidateFileContentExtension } from './deskiInvalidateFileContentExtension';

ExtensionContainer.instance.registerExtension({
  extension: DeskiExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: DeskiFileExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: DeskiUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: DeskiFileContentExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: DeskiInvalidateFileContentExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: DeskiFileControlExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: DeskiViewFileControlExtension,
  stage: ExtensionStage.AfterPlatform,
  order: 100
});
ExtensionContainer.instance.registerExtension({
  extension: DeskiFileVersionExtension,
  stage: ExtensionStage.AfterPlatform
});
