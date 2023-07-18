import { IClientCommandHandlerContext } from '../clientCommandHandlerContext';
import { IExtension } from 'tessa/extensions';
export interface IClientCommandHandler extends IExtension {
    handle(context: IClientCommandHandlerContext): Promise<void>;
}
export declare abstract class ClientCommandHandlerBase implements IClientCommandHandler {
    shouldExecute(): boolean;
    handle(_context: IClientCommandHandlerContext): Promise<void>;
}
