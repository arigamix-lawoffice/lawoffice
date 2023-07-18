import { TypedField } from 'tessa/platform/typedField';
import { DotNetType } from 'tessa/platform/dotNetType';
export declare enum CardPermissionFlags {
    None = 0,
    AllowModify = 1,
    ProhibitModify = 2,
    AllowInsertRow = 4,
    ProhibitInsertRow = 8,
    AllowDeleteRow = 16,
    ProhibitDeleteRow = 32,
    AllowDeleteCard = 64,
    ProhibitDeleteCard = 128,
    AllowInsertFile = 256,
    ProhibitInsertFile = 512,
    AllowDeleteFile = 1024,
    ProhibitDeleteFile = 2048,
    AllowReplaceFile = 4096,
    ProhibitReplaceFile = 8192,
    AllowEditNumber = 16384,
    ProhibitEditNumber = 32768,
    AllowSignFile = 65536,
    ProhibitSignFile = 131072
}
export declare type CardPermissionFlagsTypedField = TypedField<DotNetType.Int32, CardPermissionFlags>;
export interface FlagPair {
    readonly allow: CardPermissionFlags;
    readonly prohibit: CardPermissionFlags;
}
export declare function setNormalized(flags: CardPermissionFlags, flag: CardPermissionFlags): CardPermissionFlags;
