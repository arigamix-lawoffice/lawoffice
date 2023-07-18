// noinspection JSUnusedGlobalSymbols,SpellCheckingInspection

//#region Administrator

/**
 * ID: {ee693038-a5aa-4c3f-aa54-c230b4960d3f}
 * Alias: Administrator
 * Caption: $Workplaces_Administrator
 */
class AdministratorWorkplaceInfo {
  //#region Common

  /**
   * Workplace identifier for "$Workplaces_Administrator": {ee693038-a5aa-4c3f-aa54-c230b4960d3f}.
   */
   readonly ID: guid = 'ee693038-a5aa-4c3f-aa54-c230b4960d3f';

  /**
   * Workplace name for "$Workplaces_Administrator".
   */
   readonly Alias: string = 'Administrator';

  /**
   * Workplace Caption for "$Workplaces_Administrator".
   */
   readonly Caption: string = '$Workplaces_Administrator';

  //#endregion
}

//#endregion

//#region LawOffice

/**
 * ID: {c323a4ab-c3ca-4369-906e-18d8845971ac}
 * Alias: LawOffice
 * Caption: $Workplaces_LawOffice
 */
class LawOfficeWorkplaceInfo {
  //#region Common

  /**
   * Workplace identifier for "$Workplaces_LawOffice": {c323a4ab-c3ca-4369-906e-18d8845971ac}.
   */
   readonly ID: guid = 'c323a4ab-c3ca-4369-906e-18d8845971ac';

  /**
   * Workplace name for "$Workplaces_LawOffice".
   */
   readonly Alias: string = 'LawOffice';

  /**
   * Workplace Caption for "$Workplaces_LawOffice".
   */
   readonly Caption: string = '$Workplaces_LawOffice';

  //#endregion
}

//#endregion

//#region User

/**
 * ID: {c3d72683-f6c0-4766-a3d4-1fd9a7fe6827}
 * Alias: User
 * Caption: $Workplaces_User
 */
class UserWorkplaceInfo {
  //#region Common

  /**
   * Workplace identifier for "$Workplaces_User": {c3d72683-f6c0-4766-a3d4-1fd9a7fe6827}.
   */
   readonly ID: guid = 'c3d72683-f6c0-4766-a3d4-1fd9a7fe6827';

  /**
   * Workplace name for "$Workplaces_User".
   */
   readonly Alias: string = 'User';

  /**
   * Workplace Caption for "$Workplaces_User".
   */
   readonly Caption: string = '$Workplaces_User';

  //#endregion
}

//#endregion

export class WorkplaceInfo {
  //#region Workplaces

  /**
   * Workplace identifier for "$Workplaces_Administrator": {ee693038-a5aa-4c3f-aa54-c230b4960d3f}.
   */
  static get Administrator(): AdministratorWorkplaceInfo {
    return WorkplaceInfo.administrator = WorkplaceInfo.administrator ?? new AdministratorWorkplaceInfo();
  }

  private static administrator: AdministratorWorkplaceInfo;

  /**
   * Workplace identifier for "$Workplaces_LawOffice": {c323a4ab-c3ca-4369-906e-18d8845971ac}.
   */
  static get LawOffice(): LawOfficeWorkplaceInfo {
    return WorkplaceInfo.lawOffice = WorkplaceInfo.lawOffice ?? new LawOfficeWorkplaceInfo();
  }

  private static lawOffice: LawOfficeWorkplaceInfo;

  /**
   * Workplace identifier for "$Workplaces_User": {c3d72683-f6c0-4766-a3d4-1fd9a7fe6827}.
   */
  static get User(): UserWorkplaceInfo {
    return WorkplaceInfo.user = WorkplaceInfo.user ?? new UserWorkplaceInfo();
  }

  private static user: UserWorkplaceInfo;

  //#endregion
}