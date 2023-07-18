import { ValidationResult } from 'tessa/platform/validation';
import { OperationResponse } from 'tessa/platform/operations';
import { IStorage } from 'tessa/platform/storage';
import { InitOperationRequest } from './deskiMobileInitOperationRequest';
export declare enum DeskiMobileOperation {
    sign = "sign",
    verify = "verify",
    preview = "preview"
}
export declare class DeskiMobileService {
    private constructor();
    private static _instance;
    static get instance(): DeskiMobileService;
    private _servicePath;
    private _deskiMobileEnabled;
    get deskiMobileEnabled(): boolean;
    init(enabled: boolean): void;
    /**
     * Создание операции.
     * Требуется для понимания наличия deskiMobile на устройстве пользователя
     *
     * @param request информация о файле
     * @param operation тип генерируемой операции
     * @returns { OperationID: string, Link: string }
     *    - ID операции и link для подтверждения наличия приложения (выполняется из DeskiMobile)
     */
    initOperation(request: InitOperationRequest, operation: 'sign' | 'verify'): Promise<[IStorage | null, ValidationResult]>;
    /**
     * Отслеживание запуска deskiMobile.
     * Отображение пользователю loader'а и периодический опрос сервера на наличие изменения состояния операции
     *
     * @param operationId ID операции
     * @param timeout максимальное время ожидания изменения состояния операции
     * @returns ошибку, если она возникла
     */
    trackingDeskiMobile(operationId: string, timeout?: number): Promise<ValidationResult>;
    /**
     * Отслеживание выполнения операции в deskiMobile.
     * Отображение пользователю loader'а и периодический опрос сервера на наличие изменения состояния операции подписи (проверки подписи)
     *
     * @param operation тип операции
     * @param operationId ID операции
     * @returns OperationResponse - если операция завершена, true - если операция отменена, иначе - false
     */
    trackingOperation(operation: DeskiMobileOperation, operationId: string): Promise<[OperationResponse | boolean, ValidationResult]>;
    private getURL;
    private getDefaultOptions;
    private getRequestInit;
    private send;
    /**
     * Отслеживание изменения состояния операции на Completed
     *
     * @param operationId ID операции
     * @returns OperationResponse - если операция завершена, true - если операция отменена, иначе - false
     */
    private operationListener;
    /**
     * Отслеживание изменения состояния операции на InProgress
     *
     * @param operationId ID операции
     * @param timeout максимальное время ожидания изменения состояния операции на InProgress
     * @returns ошибку, если она возникла
     */
    private initOperationStateListener;
    /**
     * Метод для ожидания истечения ${timeout} в мс.
     * @param timeout время ожидания в мс
     */
    private sleep;
    /**
     * Запрос на сервер для получения статуса операции
     * @param operationId ID операции
     * @returns статус операции
     */
    private getDeskiMobileOperationStateRequest;
    /**
     * Запрос на сервер для удаления операции.
     * @param operationId ID операции.
     * @param forceDelete требуется удалить принудительно.
     * @returns true - удалена операция, false - нет.
     */
    private deleteDeskiMobileOperationRequest;
    /**
     * Запрос на сервер для получения полезной нагрузки операции и удаления операция
     * @param operationId ID операции
     * @returns
     *    Deleted - была ли удалена операция
     *    Response - полезная нагрузка операции
     */
    private getOperationAndDeleteRequest;
    /**
     * Запрос на сервер для получения полезной нагрузки операции или отмены операции
     * @param operationId ID операции
     * @returns
     *    Cancelled - была ли удалена операция
     *    Response - полезная нагрузка операции
     */
    private getOperationOrCancelWithDeleteRequest;
    /**
     * Запрос на сервер для создания операции
     * @param request информация о файле
     * @param operation тип генерируемой операции
     * @returns { OperationID: string, Link: string }
     *    - ID операции и link для подтверждения наличия приложения (выполняется из DeskiMobile)
     */
    private initOperationRequest;
}
