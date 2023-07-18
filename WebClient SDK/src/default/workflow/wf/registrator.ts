import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { WfCardUIExtension } from './wfCardUIExtension';
import { WfTasksClientGetExtension } from './wfTasksClientGetExtension';
import { WfTaskSatelliteUIExtension } from './wfTaskSatelliteUIExtension';
import { WfTypeSettingsUIExtension } from './wfTypeSettingsUIExtension';
import { WfTileExtension } from './wfTileExtension';
import { WfTaskSatelliteClientGetFileContentExtension } from './wfTaskSatelliteClientGetFileContentExtension';
import { WfTaskHistoryViewUIExtension } from './wfTaskHistoryViewUIExtension';
ExtensionContainer.instance.registerExtension({
  extension: WfCardUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: WfTasksClientGetExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: WfTaskSatelliteUIExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: WfTypeSettingsUIExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: WfTileExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: WfTaskSatelliteClientGetFileContentExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: WfTaskHistoryViewUIExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
