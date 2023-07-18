import { KrProcessClientCommand } from '../krProcessClientCommand';
export interface IClientCommandInterpreter {
    registerHandler(type: string, handlerType: any): IClientCommandInterpreter;
    interpret(commands: KrProcessClientCommand[], outerContext: any): Promise<void>;
}
export declare class ClientCommandInterpreter implements IClientCommandInterpreter {
    private constructor();
    private static _instance;
    static get instance(): ClientCommandInterpreter;
    registerHandler(type: string, handlerType: any): IClientCommandInterpreter;
    interpret(commands: KrProcessClientCommand[], outerContext: any): Promise<void>;
}
