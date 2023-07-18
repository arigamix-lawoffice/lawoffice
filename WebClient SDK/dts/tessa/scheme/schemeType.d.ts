import { DotNetType } from 'tessa/platform/dotNetType';
import { SchemeDbType } from 'tessa/platform/schemeDbType';
export declare enum SchemeTypeFlags {
    NotSet = 0,
    HasLength = 1,
    HasPrecision = 2,
    HasScale = 4,
    IsNullable = 8,
    IsReference = 16,
    IsAbstractReference = 48
}
export declare class SchemeType {
    private _name;
    private _dbType;
    private _length;
    private _precision;
    private _scale;
    private _flags;
    constructor(_name: string, _dbType: SchemeDbType, _length: number, _precision: number, _scale: number, _flags: SchemeTypeFlags);
    get name(): string;
    get hasLength(): boolean;
    get hasMaxLength(): boolean;
    get length(): number;
    get hasPrecision(): boolean;
    get precision(): number;
    get hasScale(): boolean;
    get scale(): number;
    get isNullable(): boolean;
    get isReference(): boolean;
    get isAbstractReference(): boolean;
    get isTypifiedReference(): boolean;
    get dbType(): SchemeDbType;
    get dotNetType(): DotNetType;
    get isStringType(): boolean;
    get isIntegerType(): boolean;
    equals(other: SchemeType): boolean;
    toString(): string;
    static parse(s: string, notNullByDefault?: boolean): SchemeType;
    static tryParse(s: string, notNullByDefault?: boolean): SchemeType | null;
    static fromType(type: DotNetType): SchemeType;
    static readonly AnsiString: SchemeType;
    static readonly AnsiStringFixedLength: SchemeType;
    static readonly NullableAnsiString: SchemeType;
    static readonly NullableAnsiStringFixedLength: SchemeType;
    static readonly String: SchemeType;
    static readonly StringFixedLength: SchemeType;
    static readonly NullableString: SchemeType;
    static readonly NullableStringFixedLength: SchemeType;
    static readonly SByte: SchemeType;
    static readonly Byte: SchemeType;
    static readonly NullableSByte: SchemeType;
    static readonly NullableByte: SchemeType;
    static readonly Int16: SchemeType;
    static readonly UInt16: SchemeType;
    static readonly NullableInt16: SchemeType;
    static readonly NullableUInt16: SchemeType;
    static readonly Int32: SchemeType;
    static readonly UInt32: SchemeType;
    static readonly NullableInt32: SchemeType;
    static readonly NullableUInt32: SchemeType;
    static readonly Int64: SchemeType;
    static readonly UInt64: SchemeType;
    static readonly NullableInt64: SchemeType;
    static readonly NullableUInt64: SchemeType;
    static readonly Single: SchemeType;
    static readonly Double: SchemeType;
    static readonly NullableSingle: SchemeType;
    static readonly NullableDouble: SchemeType;
    static readonly Currency: SchemeType;
    static readonly Decimal: SchemeType;
    static readonly NullableCurrency: SchemeType;
    static readonly NullableDecimal: SchemeType;
    static readonly Time: SchemeType;
    static readonly Date: SchemeType;
    static readonly DateTime: SchemeType;
    static readonly DateTime2: SchemeType;
    static readonly DateTimeOffset: SchemeType;
    static readonly NullableTime: SchemeType;
    static readonly NullableDate: SchemeType;
    static readonly NullableDateTime: SchemeType;
    static readonly NullableDateTime2: SchemeType;
    static readonly NullableDateTimeOffset: SchemeType;
    static readonly Boolean: SchemeType;
    static readonly Binary: SchemeType;
    static readonly Guid: SchemeType;
    static readonly Xml: SchemeType;
    static readonly Json: SchemeType;
    static readonly BinaryJson: SchemeType;
    static readonly NullableBoolean: SchemeType;
    static readonly NullableBinary: SchemeType;
    static readonly NullableGuid: SchemeType;
    static readonly NullableXml: SchemeType;
    static readonly NullableJson: SchemeType;
    static readonly NullableBinaryJson: SchemeType;
    static readonly ReferenceAbstract: SchemeType;
    static readonly ReferenceTypified: SchemeType;
    static readonly NullableReferenceAbstract: SchemeType;
    static readonly NullableReferenceTypified: SchemeType;
    static readonly KnownTypes: ReadonlyArray<SchemeType>;
}
