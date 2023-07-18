import { ArgumentNullError } from 'tessa/platform/errors';

/** Типы шаблонов для проверки значения. */
export enum OcrPatternTypes {
  /** Булево значение. */
  Boolean,
  /** Целое число. */
  Integer,
  /** Вещественное число. */
  Double,
  /** Дата и время. */
  DateTime,
  /** Только дата. */
  Date,
  /** Только время. */
  Time,
  /** Временной интервал. */
  Interval
}

/** Состояния запросов на распознавание текста в файле. */
export enum OcrRequestStates {
  /** Создан. */
  Created,
  /** Активен. */
  Active,
  /** Выполнен. */
  Completed,
  /** Прерван. */
  Interrupted
}

/** Контракт, предоставляющий метод для создания экземпляра заданного типа. */
export interface ILazyInitializer<T> {
  (): T;
}

/** Обеспечивает поддержку отложенной инициализации. */
export class Lazy<T> {
  private instance: T | null = null;
  private readonly initializer: ILazyInitializer<T>;

  /**
   * Создает экземпляр класса {@link Lazy<T>}.
   * @param {ILazyInitializer<T>} initializer
   * Функция для создания значения с отложенной инициализацией при необходимости.
   */
  constructor(initializer: ILazyInitializer<T>) {
    if (!initializer) {
      throw new ArgumentNullError('initializer');
    }

    this.initializer = initializer;
  }

  //#region properties

  /** Получает значение, которое показывает, создано ли значение для этого экземпляра {@link Lazy<T>}. */
  public get isValueCreated(): boolean {
    return this.instance != null;
  }

  /** Получает значение с отложенной инициализацией текущего экземпляра {@link Lazy<T>}. */
  public get value(): T {
    if (this.instance == null) {
      this.instance = this.initializer();
    }

    return this.instance;
  }

  //#endregion
}
