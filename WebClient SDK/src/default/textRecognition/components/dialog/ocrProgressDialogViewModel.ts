import { ArgumentOutOfRangeError } from 'tessa/platform/errors';
import { CardGetMode, ErrorTypeID, ErrorTypeName } from 'tessa/cards';
import { CardGetRequest, CardService } from 'tessa/cards/service';
import { Guid } from 'tessa/platform';
import { observable, runInAction } from 'mobx';
import {
  OperationService,
  OperationState,
  OperationStateAndProgress
} from 'tessa/platform/operations';
import { ValidationResult, ValidationResultType } from 'tessa/platform/validation';

/** Модель представления диалога прогресса OCR. */
export class OcrProgressDialogViewModel {
  //#region fields

  /** Прогресс в процентном соотношении (от 0 до 100). */
  @observable
  private _progress: number;
  /** Результат валидации. */
  private _validationResult: ValidationResult | null;
  /** Таймер для отслеживания операции. */
  private _progressTimer: number;
  /** Идентификатор отслеживаемой операции. */
  private readonly _operationId: guid;
  /** Промежуток времени, через который выполняется запрос состояния операции. */
  private readonly _timeout: number;
  /** Признак возможности отмены операции. */
  private readonly _canCancel: boolean;
  /** Ключ валидации с сообщением, что карточка с заданным идентификатором не найдена в таблице Instances. */
  private static readonly _instanceNotFoundKey: guid = 'a9dcb9c1-3c74-408f-bd75-8b834850148b';

  //#endregion

  //#region properties

  /** Прогресс в процентном соотношении (от 0 до 100). */
  public get progress(): number {
    return this._progress;
  }
  private set progress(value: number) {
    if (value < 0 || value > 100) {
      throw new ArgumentOutOfRangeError('progress', value);
    }
    runInAction(() => (this._progress = value));
  }

  /** Результат валидации. */
  public get validationResult(): ValidationResult | null {
    return this._validationResult;
  }

  /** Признак возможности отмены операции. */
  public get canCancel(): boolean {
    return this._canCancel;
  }

  //#endregion

  //#region constructors

  /**
   * Создает экземпляр класса {@link OcrProgressDialogViewModel}.
   * @param {guid} operationId Идентификатор отслеживаемой операции.
   * @param {number} timeout Промежуток времени (мс), через который выполняется запрос состояния операции (от 500 мс).
   * @param {number} progress Начальное значение прогресса в процентном соотношении (от 0 до 100).
   * @param {boolean} canCancel Признак возможности отмены операции.
   */
  constructor(operationId: guid, timeout: number, progress = 0, canCancel = true) {
    if (!Guid.isValid(operationId)) {
      throw new Error('Operation identifier is not GUID.');
    }
    if (timeout < 500) {
      throw new Error('Interval must be greater than 500ms.');
    }

    this.progress = progress;
    this._canCancel = canCancel;
    this._operationId = operationId;
    this._timeout = timeout;
  }

  //#endregion

  //#region public methods

  /**
   * Выполняет запуск процесса отслеживания прогресса операции.
   * @returns Функция, выполняющая остановку процесса отслеживания прогресса операции.
   */
  public monitorStart(): VoidFunction {
    const handler = async () => {
      let operation: OperationStateAndProgress | null = null;
      try {
        operation = await OperationService.instance.getStateAndProgress(this._operationId);
      } catch (e) {
        // ignore 404 Not Found
        this.monitorExit();
        return;
      }

      if (!operation || operation.progress == null || this.isOperationCompleted(operation)) {
        try {
          // Попытаемся получить карточку с ошибкой для операции.
          // Для завершенной операции также мо`жет существовать карточка с ошибкой.
          const request = new CardGetRequest();
          request.cardId = this._operationId;
          request.cardTypeId = ErrorTypeID;
          request.cardTypeName = ErrorTypeName;
          request.getMode = CardGetMode.ReadOnly;
          const response = await CardService.instance.get(request);
          const result = response.validationResult.build();
          if (result.isSuccessful) {
            const text = response
              .tryGetCard()
              ?.tryGetSections()
              ?.tryGet('Errors')
              ?.tryGetFields()
              ?.tryGet('Text', '');

            if (text) {
              this._validationResult = ValidationResult.fromText(text, ValidationResultType.Error);
            }
          } else if (
            !result.items?.some(i =>
              Guid.equals(i.key, OcrProgressDialogViewModel._instanceNotFoundKey)
            )
          ) {
            this._validationResult = result;
          }
        } catch (e) {
          this._validationResult = ValidationResult.fromError(e);
        } finally {
          this.monitorExit();
          return;
        }
      } else if (this.progress !== 100) {
        this.progress = operation.progress;
        this._progressTimer = window.setTimeout(handler, this._timeout);
      }
    };

    this._progressTimer = window.setTimeout(handler, this._timeout);

    return this.monitorExit.bind(this);
  }

  /** Выполняет остановку процесса отслеживания прогресса операции. */
  public monitorExit(): void {
    window.clearTimeout(this._progressTimer);
    this.progress = 100;
  }

  //#endregion

  //#region private methods

  private isOperationCompleted(info: OperationStateAndProgress): boolean {
    return typeof info.state === 'string'
      ? info.state === OperationState[OperationState.Completed]
      : info.state === OperationState.Completed;
  }

  //#endregion
}
