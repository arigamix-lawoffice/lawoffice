export declare class Delegate<T> {
    private _stack;
    private _currentDelegate;
    get currentDelegate(): T | null;
    get canInvoke(): boolean;
    set(func: T): void;
    restore(): T | null;
}
