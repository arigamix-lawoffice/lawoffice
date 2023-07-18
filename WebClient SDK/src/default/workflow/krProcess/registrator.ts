import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { GlobalButtonsInitalizationExtension } from './initialization/globalButtonsInitalizationExtension';
ExtensionContainer.instance.registerExtension({
  extension: GlobalButtonsInitalizationExtension,
  stage: ExtensionStage.AfterPlatform
});

// Requests
import {
  KrCardStoreExtension,
  KrClientCommandCustomExtension,
  KrClientCommandStoreExtension
} from './requests';
ExtensionContainer.instance.registerExtension({
  extension: KrCardStoreExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: KrClientCommandCustomExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: KrClientCommandStoreExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});

// Commands
import './commandInterpreter/registrator';

// Formatters
import {
  KrEditStageTypeFormatter,
  KrApprovalStageTypeFormatter,
  KrChangeStateStageTypeFormatter,
  KrCreateCardStageTypeFormatter,
  KrResolutionStageTypeFormatter,
  KrSigningStageTypeFormatter,
  KrProcessManagementStageTypeFormatter,
  RegistrationStageTypeFormater,
  TypedTaskStageTypeFormatter,
  UniversalTaskStageTypeFormatter,
  NotificationStageTypeFormatter,
  AddFileFromTemplateStageTypeFormatter,
  DialogsStageTypeFormatter
} from './formatters';
ExtensionContainer.instance.registerExtension({
  extension: KrEditStageTypeFormatter,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrApprovalStageTypeFormatter,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrChangeStateStageTypeFormatter,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrCreateCardStageTypeFormatter,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrResolutionStageTypeFormatter,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrSigningStageTypeFormatter,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrProcessManagementStageTypeFormatter,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: RegistrationStageTypeFormater,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: TypedTaskStageTypeFormatter,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: UniversalTaskStageTypeFormatter,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: NotificationStageTypeFormatter,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: AddFileFromTemplateStageTypeFormatter,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: DialogsStageTypeFormatter,
  stage: ExtensionStage.AfterPlatform
});