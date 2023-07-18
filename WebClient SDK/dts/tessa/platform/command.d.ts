export declare type CommandFunc<P extends Array<any> = [], R = void | Promise<void>> = (...args: P) => R;
export declare class Command<P extends Array<any> = [], R = void | Promise<void>> {
    private _atom;
    private _executing;
    private _func;
    constructor(command?: CommandFunc<P, R> | null);
    get executing(): boolean;
    get func(): CommandFunc<P, R> | null;
    set func(value: CommandFunc<P, R> | null);
    execute(...args: P): Promise<R>;
}
