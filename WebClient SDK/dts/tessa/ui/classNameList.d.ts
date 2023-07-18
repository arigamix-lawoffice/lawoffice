export declare class ClassNameList {
    constructor();
    private _result;
    private _classNames;
    private _computedClassNames;
    private get _computedResult();
    get empty(): boolean;
    get result(): string;
    add(className: string, predicate?: () => boolean): void;
    remove(className: string): void;
    has(className: string): boolean;
    clear(): void;
}
