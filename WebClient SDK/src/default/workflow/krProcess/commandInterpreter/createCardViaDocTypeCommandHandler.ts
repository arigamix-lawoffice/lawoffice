import { ClientCommandHandlerBase } from 'tessa/workflow/krProcess/clientCommandInterpreter';
import { IClientCommandHandlerContext } from 'tessa/workflow/krProcess';
import { getTypedFieldValue, createTypedField, DotNetType } from 'tessa/platform';
import { createCard } from 'tessa/ui/uiHost';

export class CreateCardViaDocTypeCommandHandler extends ClientCommandHandlerBase {
  public async handle(context: IClientCommandHandlerContext) {
    const command = context.command;
    const typeId = getTypedFieldValue(command.parameters['TypeID']);

    if (typeId) {
      const info = {
        ['.NewBilletCard']: command.parameters['NewCard'],
        ['.NewBilletCardSignature']: command.parameters['NewCardSignature']
      };

      const docTypeId = getTypedFieldValue(command.parameters['docTypeID']);
      const docTypeTitle = getTypedFieldValue(command.parameters['docTypeTitle']);

      if (docTypeId && docTypeTitle) {
        info['docTypeID'] = createTypedField(docTypeId, DotNetType.Guid);
        info['docTypeTitle'] = createTypedField(docTypeTitle, DotNetType.String);
      }
      await createCard({
        cardTypeId: typeId,
        info
      });
    }
  }
}
