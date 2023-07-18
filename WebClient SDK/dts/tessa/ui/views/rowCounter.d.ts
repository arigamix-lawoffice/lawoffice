export interface IRowCounter {
    readonly rowCounterVisible: boolean;
    readonly actualRowCount: number;
    readonly isCounterAvailable: boolean;
    calculatedRowCount: number;
}
export declare function isIRowCounter(obj: Object): obj is IRowCounter;
