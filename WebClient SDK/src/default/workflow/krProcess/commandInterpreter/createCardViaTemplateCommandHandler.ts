import { ClientCommandHandlerBase } from 'tessa/workflow/krProcess/clientCommandInterpreter';
import { IClientCommandHandlerContext } from 'tessa/workflow/krProcess';
import { getTypedFieldValue } from 'tessa/platform';
import { createFromTemplate } from 'tessa/ui/uiHost';

/**
 * Обработчик клиентской команды CreateCardViaTemplate.
 */
export class CreateCardViaTemplateCommandHandler extends ClientCommandHandlerBase {
  public async handle(context: IClientCommandHandlerContext): Promise<void> {
    const command = context.command;
    const templateId = getTypedFieldValue(command.parameters['TemplateID']);

    if (templateId) {
      // Запоминаем запрос на создание карточки по шаблону, т.к. данный обработчик выполняется только при создании карточки на клиенте без сохранения.
      await createFromTemplate(templateId, {
        info: {
          ['.NewBilletCard']: command.parameters['NewCard'],
          ['.NewBilletCardSignature']: command.parameters['NewCardSignature']
        },
        saveCreationRequest: true
      });
    }
  }
}
