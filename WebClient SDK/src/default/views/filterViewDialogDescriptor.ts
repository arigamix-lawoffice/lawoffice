import { ParameterMapping } from './parameterMapping';

/**
 * Дескриптор, содержащий параметры настраиваемого диалога с параметрами фильтрации представления.
 */
export class FilterViewDialogDescriptor {
  /**
   * Имя типа диалога.
   */
  readonly dialogName: string;

  /**
   * Алиас формы диалога или undefined, если требуется создать форму для первой вкладки типа диалога.
   */
  readonly formAlias?: string;

  /**
   * Коллекция, содержащая информацию о связи параметров представления и полей карточки.
   */
  readonly parametersMapping: ReadonlyArray<ParameterMapping>;
}
