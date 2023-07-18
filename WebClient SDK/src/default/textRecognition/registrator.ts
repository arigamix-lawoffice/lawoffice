import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { OcrCompareFilesUIExtension } from './extensions/ocrCompareFilesUIExtension';
import { OcrFileTagUIExtension } from './extensions/ocrFileTagUIExtension';
import { OcrMenuFileExtension } from './extensions/ocrMenuFileExtension';
import { OcrMonitoringUIExtension } from './extensions/ocrMonitoringUIExtension';
import { OcrRequestMetadataExtension } from './extensions/ocrRequestMetadataExtension';
import { OcrSettingsUIExtension } from './extensions/ocrSettingsUIExtension';
import { OcrRegistratorApplicationExtension } from './extensions/ocrRegistratorApplicationExtension';
import { OcrToolbarUIExtension } from './extensions/ocrToolbarUIExtension';
import { OcrVerificationUIExtension } from './extensions/ocrVerificationUIExtension';

ExtensionContainer.instance.registerExtension({
  extension: OcrRegistratorApplicationExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: OcrRequestMetadataExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: OcrFileTagUIExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: OcrMenuFileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: OcrToolbarUIExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: OcrVerificationUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: OcrMonitoringUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: OcrCompareFilesUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: OcrSettingsUIExtension,
  stage: ExtensionStage.AfterPlatform
});
