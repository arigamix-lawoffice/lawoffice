import { ValidationResult } from 'tessa/platform/validation';
import { OperationResponse, OperationState } from 'tessa/platform/operations';
import { processDefaultThen, retry } from 'common/utility';
import { showLoadingOverlay } from 'tessa/ui';
import { IStorage } from 'tessa/platform/storage';
import { LocalizationManager } from 'tessa/localization';
import { InitOperationRequest } from './deskiMobileInitOperationRequest';
import { serializeFromPlainToTyped } from 'tessa/platform/serialization';

export enum DeskiMobileOperation {
  sign = 'sign',
  verify = 'verify',
  preview = 'preview'
}

export class DeskiMobileService {
  //#region ctor

  private constructor() {
    this._servicePath = `${window.__INSTANCE_PATH__}`;
    this._deskiMobileEnabled = false;
  }

  //#endregion

  //#region instance

  private static _instance: DeskiMobileService;

  public static get instance(): DeskiMobileService {
    if (!DeskiMobileService._instance) {
      DeskiMobileService._instance = new DeskiMobileService();
    }
    return DeskiMobileService._instance;
  }

  //#endregion

  //#region fields

  private _servicePath: string;

  private _deskiMobileEnabled: boolean;

  //#endregion

  //#region props

  get deskiMobileEnabled(): boolean {
    return this._deskiMobileEnabled;
  }

  //#endregion

  //#region methods

  public init(enabled: boolean): void {
    this._deskiMobileEnabled = enabled;
  }

  /**
   * Создание операции.
   * Требуется для понимания наличия deskiMobile на устройстве пользователя
   *
   * @param request информация о файле
   * @param operation тип генерируемой операции
   * @returns { OperationID: string, Link: string }
   *    - ID операции и link для подтверждения наличия приложения (выполняется из DeskiMobile)
   */
  public async initOperation(
    request: InitOperationRequest,
    operation: 'sign' | 'verify'
  ): Promise<[IStorage | null, ValidationResult]> {
    let responseResult: ValidationResult = ValidationResult.empty;
    if (!this.deskiMobileEnabled) {
      responseResult = ValidationResult.fromError('DeskiMobile disabled.');
      return [null, responseResult];
    }

    let response: IStorage | null = null;
    try {
      response = await this.initOperationRequest(request, operation);
    } catch (err) {
      responseResult = ValidationResult.fromError(err);
    }

    return [response, responseResult];
  }

  /**
   * Отслеживание запуска deskiMobile.
   * Отображение пользователю loader'а и периодический опрос сервера на наличие изменения состояния операции
   *
   * @param operationId ID операции
   * @param timeout максимальное время ожидания изменения состояния операции
   * @returns ошибку, если она возникла
   */
  public async trackingDeskiMobile(
    operationId: string,
    timeout = 10000
  ): Promise<ValidationResult> {
    let responseResult = ValidationResult.empty;
    if (!this.deskiMobileEnabled) {
      responseResult = ValidationResult.fromError('DeskiMobile disabled.');
      return responseResult;
    }
    try {
      const message = '$UI_Controls_FilesControl_ApplicationAvailabilityProgress';

      await showLoadingOverlay(
        async () =>
          retry(async () => await this.initOperationStateListener(operationId, timeout), {
            retries: 3
          }),
        LocalizationManager.instance.localize(message)
      );
    } catch (err) {
      responseResult = ValidationResult.fromError(err);
    }

    return responseResult;
  }

  /**
   * Отслеживание выполнения операции в deskiMobile.
   * Отображение пользователю loader'а и периодический опрос сервера на наличие изменения состояния операции подписи (проверки подписи)
   *
   * @param operation тип операции
   * @param operationId ID операции
   * @returns OperationResponse - если операция завершена, true - если операция отменена, иначе - false
   */
  public async trackingOperation(
    operation: DeskiMobileOperation,
    operationId: string
  ): Promise<[OperationResponse | boolean, ValidationResult]> {
    let responseResult = ValidationResult.empty;
    const controller = new AbortController();
    const label = '$UI_Common_CancelOperationLabel';
    let response: OperationResponse | boolean = false;

    if (!this.deskiMobileEnabled) {
      responseResult = ValidationResult.fromError('DeskiMobile disabled.');
      return [false, responseResult];
    }

    try {
      let message = '';
      switch (operation) {
        case DeskiMobileOperation.sign:
          message = '$UI_Controls_FilesControl_DocumentSigningProgress';
          break;
        case DeskiMobileOperation.verify:
          message = '$UI_Controls_FilesControl_DocumentVerifyProgress';
          break;
        default:
          throw new Error('Operation must be "sign" or "verify"');
      }

      response = await showLoadingOverlay(
        async () =>
          retry(async () => await this.operationListener(operationId, controller.signal), {
            controller
          }),
        LocalizationManager.instance.localize(message),
        {
          text: LocalizationManager.instance.localize(label),
          onClick: () => {
            controller.abort();
          }
        }
      );
    } catch (err) {
      if (JSON.stringify(err).indexOf('The operation was aborted.') === -1) {
        responseResult = ValidationResult.fromError(err);
      } else {
        // В случае отмены операции
        response = true;
      }
    }

    return [response, responseResult];
  }

  //#endregion

  //#region private methods

  private getURL(url?: string): string {
    return `${this._servicePath}${url ? `/${url}` : ''}`;
  }

  private getDefaultOptions(): RequestInit {
    return {
      mode: 'cors',
      credentials: 'same-origin'
    };
  }

  private getRequestInit(method = 'POST', body?: BodyInit): RequestInit {
    return {
      method,
      headers: {
        'Content-Type': 'application/json'
      },
      body
    };
  }

  private async send<T>({
    url,
    init,
    ignoreSession,
    token,
    transformResponse
  }: {
    url: string;
    init?: RequestInit;
    token?: string;
    ignoreSession?: boolean;
    transformResponse?: (response: Response) => Promise<T>;
  }): Promise<T> {
    init = Object.assign({}, this.getDefaultOptions(), init);
    if (!ignoreSession && !!token) {
      const headers = init.headers ?? (init.headers = {});
      headers['Tessa-Session'] = token;
    }
    return await processDefaultThen(fetch(this.getURL(url), init), transformResponse);
  }

  /**
   * Отслеживание изменения состояния операции на Completed
   *
   * @param operationId ID операции
   * @returns OperationResponse - если операция завершена, true - если операция отменена, иначе - false
   */
  private async operationListener(
    operationId: string,
    signal: AbortSignal
  ): Promise<OperationResponse | boolean> {
    return new Promise(async (resolve, reject) => {
      try {
        while (true) {
          if (signal.aborted) {
            const operation = await this.getOperationOrCancelWithDeleteRequest(operationId);
            const response = operation.Response ? new OperationResponse(operation.Response) : true;
            resolve(response);
            return;
          }

          const operation = await this.getOperationAndDeleteRequest(operationId);
          if (operation.Response || operation.Deleted) {
            const response = operation.Response
              ? new OperationResponse(operation.Response)
              : operation.Deleted;
            resolve(response);
            return;
          }

          await this.sleep(1000);
        }
      } catch (err) {
        reject(err);
      }
    });
  }

  /**
   * Отслеживание изменения состояния операции на InProgress
   *
   * @param operationId ID операции
   * @param timeout максимальное время ожидания изменения состояния операции на InProgress
   * @returns ошибку, если она возникла
   */
  private async initOperationStateListener(operationId: string, timeout: number): Promise<void> {
    const promises: Promise<void>[] = [];

    let timeoutID = 0;
    const controller = new AbortController();
    promises.push(
      new Promise(async (resolve, reject) => {
        try {
          while (true) {
            if (controller.signal.aborted) {
              return;
            }

            const state = await this.getDeskiMobileOperationStateRequest(operationId);
            if (state === OperationState.InProgress) {
              resolve();
              return;
            }

            await this.sleep(1000);
          }
        } catch (e) {
          reject(e);
        }
      })
    );

    promises.push(
      new Promise((resolve, reject) => {
        timeoutID = window.setTimeout(async () => {
          try {
            controller.abort();

            if (await this.deleteDeskiMobileOperationRequest(operationId)) {
              reject(
                'No response from deskiMobile. Check that you have installed the deskiMobile app and try again.'
              );
              return;
            }

            const state = await this.getDeskiMobileOperationStateRequest(operationId);
            if (state !== OperationState.Created) {
              resolve();
              return;
            }

            await this.deleteDeskiMobileOperationRequest(operationId, true);
            reject(`Operation ID=${operationId} isn't active.`);
          } catch (err) {
            reject(err);
          }
        }, timeout);
      })
    );

    try {
      return await Promise.race(promises);
    } finally {
      window.clearTimeout(timeoutID);
    }
  }

  /**
   * Метод для ожидания истечения ${timeout} в мс.
   * @param timeout время ожидания в мс
   */
  private async sleep(timeout: number): Promise<void> {
    return new Promise(resolve => setTimeout(resolve, timeout));
  }

  /**
   * Запрос на сервер для получения статуса операции
   * @param operationId ID операции
   * @returns статус операции
   */
  private async getDeskiMobileOperationStateRequest(
    operationId: string
  ): Promise<OperationState | undefined> {
    const url = `mobile/operation-status/${operationId}`;
    const state: string = await this.send({ url, init: this.getRequestInit('GET') });
    return OperationState[state];
  }

  /**
   * Запрос на сервер для удаления операции.
   * @param operationId ID операции.
   * @param forceDelete требуется удалить принудительно.
   * @returns true - удалена операция, false - нет.
   */
  private async deleteDeskiMobileOperationRequest(
    operationId: string,
    forceDelete = false
  ): Promise<boolean> {
    const url = `mobile/created-operation/${operationId}?force=${forceDelete}`;
    return await this.send({ url, init: this.getRequestInit('DELETE') });
  }

  /**
   * Запрос на сервер для получения полезной нагрузки операции и удаления операция
   * @param operationId ID операции
   * @returns
   *    Deleted - была ли удалена операция
   *    Response - полезная нагрузка операции
   */
  private async getOperationAndDeleteRequest(
    operationId: string
  ): Promise<{ Deleted: boolean; Response: IStorage | null }> {
    const url = `mobile/try-get-response-and-delete?operationId=${operationId}`;
    return await this.send({ url, init: this.getRequestInit() });
  }

  /**
   * Запрос на сервер для получения полезной нагрузки операции или отмены операции
   * @param operationId ID операции
   * @returns
   *    Cancelled - была ли удалена операция
   *    Response - полезная нагрузка операции
   */
  private async getOperationOrCancelWithDeleteRequest(
    operationId: string
  ): Promise<{ Cancelled: boolean; Response: IStorage | null }> {
    const url = `mobile/get-response-or-cancel?operationId=${operationId}`;
    return await this.send({ url, init: this.getRequestInit() });
  }

  /**
   * Запрос на сервер для создания операции
   * @param request информация о файле
   * @param operation тип генерируемой операции
   * @returns { OperationID: string, Link: string }
   *    - ID операции и link для подтверждения наличия приложения (выполняется из DeskiMobile)
   */
  private async initOperationRequest(
    request: InitOperationRequest,
    operation: 'sign' | 'verify'
  ): Promise<IStorage | null> {
    const url = `mobile/init-operation?operation=${operation}`;
    return await this.send({
      url,
      init: this.getRequestInit('POST', serializeFromPlainToTyped(request.getStorage()))
    });
  }

  //#endregion
}
