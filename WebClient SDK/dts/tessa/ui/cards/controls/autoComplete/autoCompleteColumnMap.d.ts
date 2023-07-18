export declare class AutoCompleteColumnMapping {
    constructor(viewColumnName: string, schemeColumnName: string, viewResultIndex: number);
    readonly viewColumnName: string;
    readonly schemeColumnName: string;
    readonly viewResultIndex: number;
}
export declare enum MapType {
    Input = 0,
    Browse = 1
}
export declare class AutoCompleteColumnMap {
    constructor(mapType: MapType, mapping: AutoCompleteColumnMapping[], viewAlias: string, viewColPrefix: string, referenceColumnIndex: number);
    readonly mapType: MapType;
    readonly mapping: ReadonlyArray<AutoCompleteColumnMapping>;
    readonly viewAlias: string;
    readonly viewColPrefix: string;
    readonly referenceColumnIndex: number;
}
