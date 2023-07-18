interface EventSetting {
    once?: boolean;
}
export declare class EventHandler<T extends Function> {
    constructor();
    protected _events: Set<T>;
    protected _settings: Map<T, EventSetting>;
    get events(): ReadonlySet<T>;
    get isEmpty(): boolean;
    add(event: T): boolean;
    addWithDispose(event: T): (() => void) | null;
    addOnce(event: T): (() => void) | null;
    remove(event: T): boolean;
    clear(): void;
    invoke(...args: any[]): void;
    invokeAsync(...args: any[]): Promise<void>;
}
export {};
