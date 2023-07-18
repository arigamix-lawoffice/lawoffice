import { ClientCommandHandlerBase } from 'tessa/workflow/krProcess/clientCommandInterpreter';
import { IClientCommandHandlerContext } from 'tessa/workflow/krProcess';
/**
 * Обработчик клиентской команды CreateCardViaTemplate.
 */
export declare class CreateCardViaTemplateCommandHandler extends ClientCommandHandlerBase {
    handle(context: IClientCommandHandlerContext): Promise<void>;
}
