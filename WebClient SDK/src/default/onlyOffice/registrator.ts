import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';
import { OnlyOfficeFileExtension } from './onlyOfficeFileExtension';
import { OnlyOfficeInitializationApplicationExtension } from './onlyOfficeInitializationApplicationExtension';
import {
  OnlyOfficeFileControlExtension,
  OnlyOfficeViewFileControlExtension
} from './onlyOfficeFileControlExtension';
import { OnlyOfficeCardUIExtension } from './onlyOfficeCardUIExtension';

ExtensionContainer.instance.registerExtension({
  extension: OnlyOfficeFileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});

ExtensionContainer.instance.registerExtension({
  extension: OnlyOfficeInitializationApplicationExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});

ExtensionContainer.instance.registerExtension({
  extension: OnlyOfficeFileControlExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});

ExtensionContainer.instance.registerExtension({
  extension: OnlyOfficeViewFileControlExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true,
  order: 100
});

ExtensionContainer.instance.registerExtension({
  extension: OnlyOfficeCardUIExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
