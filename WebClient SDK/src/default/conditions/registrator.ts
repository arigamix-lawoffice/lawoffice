import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';
import { ConditionTypesClientInitializationExtension } from './conditionTypesClientInitializationExtension';

ExtensionContainer.instance.registerExtension({
  extension: ConditionTypesClientInitializationExtension,
  stage: ExtensionStage.Platform,
  singleton: true,
  order: 1
});
