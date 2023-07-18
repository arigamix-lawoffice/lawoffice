import { ValidationStorageObject } from 'tessa/platform/validation';
import { IStorage, ArrayStorage } from 'tessa/platform/storage';
import { TypedField, DotNetType } from 'tessa/platform';
export declare class MessageBody extends ValidationStorageObject {
    constructor(storage?: IStorage);
    static readonly textKey: string;
    static readonly usersKey: string;
    static readonly rolesKey: string;
    get text(): string | null;
    set text(value: string | null);
    get users(): ArrayStorage<TypedField<DotNetType.String, string>>;
    set users(value: ArrayStorage<TypedField<DotNetType.String, string>>);
    get roles(): ArrayStorage<TypedField<DotNetType.String, string>>;
    set roles(value: ArrayStorage<TypedField<DotNetType.String, string>>);
    tryGetUsers(): ArrayStorage<TypedField<DotNetType.String, string>> | null | undefined;
    tryGetRoles(): ArrayStorage<TypedField<DotNetType.String, string>> | null | undefined;
}
