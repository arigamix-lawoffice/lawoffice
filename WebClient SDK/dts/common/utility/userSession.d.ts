import { TypedField } from 'tessa/platform/typedField';
import { DotNetType } from 'tessa/platform';
import { IStorage } from 'tessa/platform/storage';
export interface IUserSession {
    isAdmin: boolean;
    AccessLevel: UserAccessLevel;
    ApplicationID: string;
    Created: string | null;
    Culture: string;
    Expires: string | null;
    HostIP: string;
    HostName: string;
    InstanceName: string;
    LicenseType: SessionLicenseType;
    LoginType: UserLoginType;
    ServerCode: string;
    SessionID: string;
    Signature: string;
    UICulture: string;
    UserID: string;
    UserLogin: string;
    UserName: string;
    UtcOffsetMinutes: number;
    TimeZoneUtcOffset: number;
    init(storage: IStorage): boolean;
    serializeToXml(): string;
    isExpired(): boolean;
}
export interface IToken {
    AccessLevel: TypedField<DotNetType.Int32, UserAccessLevel>;
    ApplicationID: TypedField<DotNetType.Guid, guid>;
    Created: TypedField<DotNetType.DateTime, string>;
    Culture: TypedField<DotNetType.String, string>;
    Expires: TypedField<DotNetType.DateTime, string>;
    HostIP: TypedField<DotNetType.String, string>;
    HostName: TypedField<DotNetType.String, string>;
    InstanceName: TypedField<DotNetType.String, string>;
    LicenseType: TypedField<DotNetType.Int32, SessionLicenseType>;
    LoginType: TypedField<DotNetType.Int32, UserLoginType>;
    ServerCode: TypedField<DotNetType.String, string>;
    SessionID: TypedField<DotNetType.Guid, guid>;
    Signature: TypedField<DotNetType.String, string>;
    UICulture: TypedField<DotNetType.String, string>;
    UserID: TypedField<DotNetType.Guid, guid>;
    UserLogin: TypedField<DotNetType.String, string>;
    UserName: TypedField<DotNetType.String, string>;
    UtcOffsetMinutes: TypedField<DotNetType.Double, number>;
    TimeZoneUtcOffset: TypedField<DotNetType.Double, number>;
}
declare const userSession: IUserSession;
export default userSession;
export declare enum SessionLicenseType {
    /**
     * Лицензия не используется.
     */
    Unspecified = 0,
    /**
     * Конкурентная лицензия толстого клиента.
     */
    ConcurrentClient = 1,
    /**
     * Персональная лицензия толстого клиента.
     */
    PersonalClient = 2,
    /**
     * Конкурентная лицензия лёгкого клиента.
     */
    ConcurrentWeb = 3,
    /**
     * Персональная лицензия лёгкого клиента.
     */
    PersonalWeb = 4
}
export declare enum UserAccessLevel {
    /**
     * Обычный пользователь.
     */
    Regular = 0,
    /**
     * Администратор системы, которому доступны расширенные привилегии.
     */
    Administrator = 1
}
export declare enum UserLoginType {
    /**
     * Пользователю не разрешено выполнять аутентификацию в системе.
     * Это может быть как отключённый пользователь (например, сотрудник, который был уволен),
     * так и системный пользователь.
     */
    None = 0,
    /**
     * Аутентификация производится по паре логин/пароль, указанной в карточке сотрудника Tessa.
     * Такой пользователь может не иметь учётной записи Windows, но он сможет авторизоваться
     * и работать в системе.
     */
    Tessa = 1,
    /**
     * Аутентификация пользователя производится по учётной записи Windows,
     * в т.ч. используя доменную авторизацию.
     */
    Windows = 2
}
