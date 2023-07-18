import { DotNetType } from './dotNetType';
import { IStorage, IStorageArray } from './storage';
export declare type TypedField<T extends DotNetType = DotNetType, V = any> = {
    $type: T;
    $value: V;
};
export declare function isTypedField(object: any): object is TypedField;
export declare function createTypedField<T extends DotNetType, V>(value: V, type: T): TypedField<T, V>;
export declare function getTypedFieldValue(field: TypedField | null): any;
export declare function getTypedOrNormalValue(field: TypedField | any | null): any;
export declare function getGuidEmpty(): TypedField<DotNetType.Guid, string>;
export declare function getZero(): TypedField<DotNetType.Int32, number>;
export declare const SystemUserIDValue = "11111111-1111-1111-1111-111111111111";
export declare function getSystemUserId(): TypedField<DotNetType.Guid, string>;
export declare const SystemUserNameValue = "System";
export declare function getSystemUserName(): TypedField<DotNetType.String, string>;
export declare function isDecimalField(field: TypedField | null): boolean;
export declare function convertTypedFieldsToPlain(obj: IStorage | IStorageArray, recursive?: boolean): IStorage | IStorageArray;
export declare function convertPlainToTypedField(obj: IStorage | IStorageArray, recursive?: boolean): IStorage | IStorageArray;
