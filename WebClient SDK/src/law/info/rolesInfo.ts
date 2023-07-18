// noinspection JSUnusedGlobalSymbols,SpellCheckingInspection

//#region System Employees

//#region Admin

/**
 * ID: {3db19fa0-228a-497f-873a-0250bf0a4ccb}
 * Alias: Admin
 * Caption: Admin
 */
class AdminPersonalInfo {
  /**
   * Role identifier for "Admin": {3db19fa0-228a-497f-873a-0250bf0a4ccb}.
   */
  readonly ID: guid = '3db19fa0-228a-497f-873a-0250bf0a4ccb';

  /**
   * Role alias for "Admin".
   */
  readonly Alias: string = 'Admin';

  /**
   * Role caption for "Admin".
   */
  readonly Caption: string = 'Admin';
}

//#endregion

//#region System

/**
 * ID: {11111111-1111-1111-1111-111111111111}
 * Alias: System
 * Caption: System
 */
class SystemPersonalInfo {
  /**
   * Role identifier for "System": {11111111-1111-1111-1111-111111111111}.
   */
  readonly ID: guid = '11111111-1111-1111-1111-111111111111';

  /**
   * Role alias for "System".
   */
  readonly Alias: string = 'System';

  /**
   * Role caption for "System".
   */
  readonly Caption: string = 'System';
}

//#endregion

//#endregion

class PersonalRoleInfo {
  //#region Personal

  /**
   * ID: {3db19fa0-228a-497f-873a-0250bf0a4ccb}
   * Alias: Admin
   * Caption: Admin
   */
  readonly Admin = new AdminPersonalInfo();

  /**
   * ID: {11111111-1111-1111-1111-111111111111}
   * Alias: System
   * Caption: System
   */
  readonly System = new SystemPersonalInfo();

  //#endregion
}

export class RoleInfo {
  //#region Roles

  static get Personal(): PersonalRoleInfo {
    return RoleInfo.personal = RoleInfo.personal ?? new PersonalRoleInfo();
  }

  private static personal: PersonalRoleInfo;

  //#endregion
}