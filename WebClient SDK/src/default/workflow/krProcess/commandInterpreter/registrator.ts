import { CreateCardViaDocTypeCommandHandler } from './createCardViaDocTypeCommandHandler';
import { CreateCardViaTemplateCommandHandler } from './createCardViaTemplateCommandHandler';
import { OpenCardClientCommandHandler } from './openCardClientCommandHandler';
import { ShowConfirmationDialogClientCommandHandler } from './showConfirmationDialogClientCommandHandler';
import { ClientCommandInterpreter } from 'tessa/workflow/krProcess/clientCommandInterpreter';
import { KrAdvancedDialogCommandHandler } from './krAdvancedDialogCommandHandler';
import { WeAdvancedDialogCommandHandler } from '../../workflowEngine/weAdvancedDialogCommandHandler';

ClientCommandInterpreter.instance.registerHandler(
  'ShowConfirmationDialog',
  ShowConfirmationDialogClientCommandHandler
);
// ClientCommandInterpreter.instance.registerHandler('RefreshAndNotify', RefreshAndNotifyClientCommandHandler); // not used
ClientCommandInterpreter.instance.registerHandler(
  'CreateCardViaTemplate',
  CreateCardViaTemplateCommandHandler
);
ClientCommandInterpreter.instance.registerHandler(
  'CreateCardViaDocType',
  CreateCardViaDocTypeCommandHandler
);
ClientCommandInterpreter.instance.registerHandler('OpenCard', OpenCardClientCommandHandler);
ClientCommandInterpreter.instance.registerHandler(
  'ShowAdvancedDialog',
  KrAdvancedDialogCommandHandler
);
ClientCommandInterpreter.instance.registerHandler(
  'WeShowAdvancedDialog',
  WeAdvancedDialogCommandHandler
);
