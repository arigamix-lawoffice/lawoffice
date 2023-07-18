import { ClientCommandHandlerBase } from 'tessa/workflow/krProcess/clientCommandInterpreter';
import { IClientCommandHandlerContext } from 'tessa/workflow/krProcess';
import { getTypedFieldValue } from 'tessa/platform';
import { openCard } from 'tessa/ui/uiHost';

export class OpenCardClientCommandHandler extends ClientCommandHandlerBase {

  public async handle(context: IClientCommandHandlerContext) {
    const cardId = getTypedFieldValue(context.command.parameters['cardID']);

    if (cardId) {
      await openCard({cardId});
    }
  }

}