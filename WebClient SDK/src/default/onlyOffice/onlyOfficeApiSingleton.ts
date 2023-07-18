import { OnlyOfficeApi } from './onlyOfficeApi';

/**
 * Синглтон-объект, представляющий API редактора документов и его хранилища.
 */
export class OnlyOfficeApiSingleton {
  private static _instance: OnlyOfficeApi | null = null;

  // noinspection JSUnusedLocalSymbols
  private constructor() {}

  public static get instance(): OnlyOfficeApi {
    if (!this._instance) throw new Error('OnlyOfficeApi is null');
    return this._instance;
  }

  public static get isAvailable(): boolean {
    return !!this._instance;
  }

  public static init(api: OnlyOfficeApi): void {
    this._instance = api;
  }
}
