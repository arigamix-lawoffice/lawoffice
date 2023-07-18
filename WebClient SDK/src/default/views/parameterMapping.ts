import { CriteriaOperator } from 'tessa/views/metadata';

/**
 * Предоставляет информацию о связи параметра представления и поля карточки.
 */

export class ParameterMapping {
  /**
   * Алиас параметра представления.
   */
  readonly alias: string;

  /**
   * Имя секции, содержащей поле {@link valueFieldName}.
   */
  readonly valueSectionName: string;

  /**
   * Поле, содержащее значение параметра.
   */
  readonly valueFieldName: string;

  /**
   * Имя секции, содержащей поле {@link displayValueFieldName}. Если не задано, то используется строковое представление значения параметра.
   */
  readonly displayValueSectionName?: string;

  /**
   * Имя поля, содержащего отображаемое значение параметра. Если не задано, то используется строковое представление значения параметра.
   *
   * Для корректной работы должно быть задано значение {@link displayValueSectionName}.
   */
  readonly displayValueFieldName?: string;

  /**
   * Условный оператор. Если не задан, то используется оператор по умолчанию.
   */
  criteriaOperator?: CriteriaOperator;
}
