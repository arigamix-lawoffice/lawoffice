import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { KrUIExtension } from './krUIExtension';
import { KrStageUIExtension } from './krStageUIExtension';
import { KrStageTemplateUIExtension } from './krStageTemplateUIExtension';
import { KrStageSourceUIExtension } from './krStageSourceUIExtension';
import { KrRecalcStagesUIExtension } from './krRecalcStagesUIExtension';
import { KrHideCardTypeSettingsUIExtension } from './krHideCardTypeSettingsUIExtension';
import { KrHideApprovalTabOrDocStateBlockUIExtension } from './krHideApprovalTabOrDocStateBlockUIExtension';
import { KrHideApprovalStagePermissionsDisclaimer } from './krHideApprovalStagePermissionsDisclaimer';
import { KrDocumentWorkspaceInfoUIExtension } from './krDocumentWorkspaceInfoUIExtension';
import { KrCommentRequestUIExtension } from './krCommentRequestUIExtension';
import { KrTilesUIExtension } from './krTilesUIExtension';
import { KrAdditionalApprovalCardUIExtension } from './krAdditionalApprovalCardUIExtension';
import { KrTemplateUIExtension } from './krTemplateUIExtension';
import { KrSecondaryProcessUIExtension } from './krSecondaryProcessUIExtension';
import { KrEditModeToolbarUIExtension } from './krEditModeToolbarUIExtension';

ExtensionContainer.instance.registerExtension({
  extension: KrUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrStageUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrStageTemplateUIExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: KrStageSourceUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrRecalcStagesUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrHideCardTypeSettingsUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrHideApprovalTabOrDocStateBlockUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrHideApprovalStagePermissionsDisclaimer,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrDocumentWorkspaceInfoUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrCommentRequestUIExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: KrTilesUIExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: KrAdditionalApprovalCardUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrTemplateUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrSecondaryProcessUIExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrEditModeToolbarUIExtension,
  stage: ExtensionStage.AfterPlatform
});

// StageHandlers
import {
  ResolutionStageUIHandler,
  CreateCardUIHandler,
  ProcessManagementUIHandler,
  UniversalTaskStageTypeUIHandler,
  AddFromTemplateUIHandler,
  DialogUIHandler,
  TabCaptionUIHandler,
  ApprovalUIHandler,
  TypedTaskUIHandler,
  SigningUIHandler
} from './stageHandlers';
ExtensionContainer.instance.registerExtension({
  extension: AddFromTemplateUIHandler,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ApprovalUIHandler,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: CreateCardUIHandler,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: DialogUIHandler,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: UniversalTaskStageTypeUIHandler,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ProcessManagementUIHandler,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: ResolutionStageUIHandler,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: TabCaptionUIHandler,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: TypedTaskUIHandler,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: SigningUIHandler,
  stage: ExtensionStage.AfterPlatform
});
