import { CriteriaOperatorConst, getCriteria } from 'tessa/views/metadata';

import { FilterViewDialogDescriptor } from './filterViewDialogDescriptor';

/**
 * Предоставляет объекты типа {@link FilterViewDialogDescriptor}.
 */
export class FilterViewDialogDescriptors {
  /**
   * Дескриптор, описывающий специальный диалог с параметрами фильтрации представления РМ Администратор/Тестирование/Автомобили.
   */
  static readonly cars: FilterViewDialogDescriptor = {
    dialogName: 'CarViewParameters',
    parametersMapping: [
      { alias: 'CarName', valueSectionName: 'Parameters', valueFieldName: 'Name' },
      {
        alias: 'CarMaxSpeed',
        valueSectionName: 'Parameters',
        valueFieldName: 'MaxSpeed',
        criteriaOperator: getCriteria(CriteriaOperatorConst.Equality)
      },
      {
        alias: 'Driver',
        valueSectionName: 'Parameters',
        valueFieldName: 'DriverID',
        displayValueSectionName: 'Parameters',
        displayValueFieldName: 'DriverName'
      },
      {
        alias: 'CarReleaseDateFrom',
        valueSectionName: 'Parameters',
        valueFieldName: 'ReleaseDateFrom'
      },
      { alias: 'CarReleaseDateTo', valueSectionName: 'Parameters', valueFieldName: 'ReleaseDateTo' }
    ]
  };
}
