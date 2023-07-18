import { KrProcessClientCommand } from './krProcessClientCommand';
import { IStorage } from 'tessa/platform/storage';
export interface IClientCommandHandlerContext {
    command: KrProcessClientCommand;
    outerContext: any | null;
    info: IStorage;
}
