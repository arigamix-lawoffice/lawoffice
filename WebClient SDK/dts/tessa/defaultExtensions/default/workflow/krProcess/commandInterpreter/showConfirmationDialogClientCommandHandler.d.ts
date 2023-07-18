import { ClientCommandHandlerBase } from 'tessa/workflow/krProcess/clientCommandInterpreter';
import { IClientCommandHandlerContext } from 'tessa/workflow/krProcess';
export declare class ShowConfirmationDialogClientCommandHandler extends ClientCommandHandlerBase {
    handle(context: IClientCommandHandlerContext): Promise<void>;
}
