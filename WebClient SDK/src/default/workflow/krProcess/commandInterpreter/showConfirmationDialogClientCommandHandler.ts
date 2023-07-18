import { ClientCommandHandlerBase } from 'tessa/workflow/krProcess/clientCommandInterpreter';
import { IClientCommandHandlerContext } from 'tessa/workflow/krProcess';
import { getTypedFieldValue } from 'tessa/platform';
import { showMessage } from 'tessa/ui';

export class ShowConfirmationDialogClientCommandHandler extends ClientCommandHandlerBase {

  public async handle(context: IClientCommandHandlerContext) {
    const text = getTypedFieldValue(context.command.parameters['text']);

    if (text) {
      await showMessage(text);
    }
  }

}