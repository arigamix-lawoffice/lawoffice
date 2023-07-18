/**
 * Режим работы этапа "Управление процессом".
 */
export enum ProcessManagementStageTypeMode {
  /**
   * Переход на этап.
   */
  StageMode = 0,

  /**
   * Переход на группу.
   */
  GroupMode = 1,

  /**
   * Переход на следующую группу.
   */
  NextGroupMode = 2,

  /**
   * Переход на предыдущую группу.
   */
  PrevGroupMode = 3,

  /**
   * Переход на начало текущей группы.
   */
  CurrentGroupMode = 4,

  /**
   * Отправить процесс.
   */
  SendSignalMode = 5,

  /**
   * Отменить процесс.
   */
  CancelProcessMode = 6,

  /**
   * Пропустить процесс.
   */
  SkipProcessMode = 7
}
