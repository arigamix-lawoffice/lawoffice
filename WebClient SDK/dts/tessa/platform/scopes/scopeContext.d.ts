export declare class ScopeContext<TContext> {
    private _stack;
    get context(): TContext | null;
    get hasCurrent(): boolean;
    get current(): ScopeContextInstance<TContext> | null;
    get parent(): ScopeContextInstance<TContext> | null;
    create(context: TContext | null, disposeAction?: ((context: TContext | null) => void) | null): ScopeContextInstance<TContext>;
    private remove;
}
export declare class ScopeContextInstance<TContext> {
    private _context;
    private _disposeAction;
    private _destroyer;
    get context(): TContext | null;
    initialize(context: TContext | null, disposeAction: ((context: TContext | null) => void) | null | undefined, destroyer: (instance: ScopeContextInstance<TContext>) => void): void;
    dispose(): void;
}
