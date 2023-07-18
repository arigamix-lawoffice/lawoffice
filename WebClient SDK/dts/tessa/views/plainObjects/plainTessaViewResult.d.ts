import { TessaViewResult } from '../tessaViewResult';
import { CardStorageObject } from 'tessa/cards';
import { ArrayStorage, IStorage } from 'tessa/platform/storage';
import { DotNetType, TypedField } from 'tessa/platform';
export declare class PlainTessaViewResult extends CardStorageObject {
    constructor(storage?: IStorage);
    static readonly rowCountKey: string;
    static readonly columnsKey: string;
    static readonly schemeTypesKey: string;
    static readonly rowsKey: string;
    static readonly infoKey: string;
    get rowCount(): number;
    set rowCount(value: number);
    get columns(): ArrayStorage<TypedField<DotNetType.String, string>> | null;
    set columns(value: ArrayStorage<TypedField<DotNetType.String, string>> | null);
    get schemeTypes(): ArrayStorage<TypedField<DotNetType.String, string>> | null;
    set schemeTypes(value: ArrayStorage<TypedField<DotNetType.String, string>> | null);
    get info(): IStorage;
    set info(value: IStorage);
    get rows(): ArrayStorage<ArrayStorage<TypedField>>;
    set rows(value: ArrayStorage<ArrayStorage<TypedField>>);
    private static readonly _objectFactory;
    tryGetColumns(): ArrayStorage<TypedField<DotNetType.String, string>> | null | undefined;
    tryGetSchemeTypes(): ArrayStorage<TypedField<DotNetType.String, string>> | null | undefined;
    tryGetRows(): ArrayStorage<ArrayStorage<TypedField>> | null | undefined;
    toTessaViewResult(): TessaViewResult;
}
