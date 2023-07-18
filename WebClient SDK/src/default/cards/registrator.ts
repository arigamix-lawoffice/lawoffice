import { ExtensionContainer, ExtensionStage } from 'tessa/extensions';

import { AcquaintanceClientStoreExtension } from './acquaintanceClientStoreExtension';
import { OpenFromKrDocStatesOnDoubleClickExtension } from './openFromKrDocStatesOnDoubleClickExtension';
import { KrDocStateClientDeleteExtension } from './krDocStateClientDeleteExtension';
import { CompletionOptionGetTypeIdListRequestExtension } from './completionOptionGetTypeIdListRequestExtension';
import { FunctionRoleGetTypeIdListRequestExtension } from './functionRoleGetTypeIdListRequestExtension';
import { KrPermissionsMandatoryStoreExtension } from './krPermissionsMandatoryStoreExtension';
import { KrCardTaskAssignedRolesAccessProvider } from './krCardTaskAssignedRolesAccessProvider';
import { CardTaskAssignedRolesAccessProviderFactory } from 'tessa/ui/cards';
import { DefaultCardTypeExtensionTypes } from './defaultCardTypeExtensionTypes';

ExtensionContainer.instance.registerExtension({
  extension: AcquaintanceClientStoreExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: OpenFromKrDocStatesOnDoubleClickExtension,
  stage: ExtensionStage.AfterPlatform,
  singleton: true
});
ExtensionContainer.instance.registerExtension({
  extension: KrDocStateClientDeleteExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: CompletionOptionGetTypeIdListRequestExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: FunctionRoleGetTypeIdListRequestExtension,
  stage: ExtensionStage.AfterPlatform
});
ExtensionContainer.instance.registerExtension({
  extension: KrPermissionsMandatoryStoreExtension,
  stage: ExtensionStage.AfterPlatform
});

CardTaskAssignedRolesAccessProviderFactory.instance.setProviderFunc(
  () => new KrCardTaskAssignedRolesAccessProvider()
);

DefaultCardTypeExtensionTypes.register();
