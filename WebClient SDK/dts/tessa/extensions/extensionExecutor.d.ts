import { IExtension } from './extension';
export interface IExtensionExecutor {
    readonly length: number;
    execute<TContext>(method: string, context: TContext): any;
    executeWithCheck<TContext>(method: string, context: TContext, showMessageBox?: boolean): boolean;
    executeAsync<TContext>(method: string, context: TContext): Promise<void>;
    executeAsyncWithCheck<TContext>(method: string, context: TContext, showMessageBox?: boolean): Promise<boolean>;
    getExtensions<TContext>(context: TContext | null): IExtension[];
}
export declare class ExtensionExecutor implements IExtensionExecutor {
    constructor(instances: IExtension[]);
    private _instances;
    get length(): number;
    execute<TContext>(method: string, context: TContext): void;
    executeWithCheck<TContext>(method: string, context: TContext, showMessageBox?: boolean): boolean;
    executeAsync<TContext>(method: string, context: TContext): Promise<void>;
    executeAsyncWithCheck<TContext>(method: string, context: TContext, showMessageBox?: boolean): Promise<boolean>;
    getExtensions<TContext>(context?: TContext | null): any;
}
