import { FilterViewDialogDescriptor } from './filterViewDialogDescriptor';

/**
 * Объект, предоставляющий {@link FilterViewDialogDescriptor}.
 */
export interface IFilterViewDialogDescriptorRegistry {
  /**
   * Регистрирует {@link descriptor} для указанного {@link compositionId}. Метод замещает предыдущую регистрацию при её наличии.
   * @param compositionId Уникальный идентификатор элемента рабочего места, для которого должно использоваться переопределение диалога с параметрами представления.
   *
   * Значение расположено в TessaAdmin на вкладке "Рабочие места" в окне "Свойства" в поле "Id". К данному элементу дерева должно быть применено расширение Tessa.Extensions.Default.Client.Views.FilterViewDialogOverrideWorkplaceComponentExtension.
   * @param descriptor {@link FilterViewDialogDescriptor}
   */
  register(compositionId: guid, descriptor: FilterViewDialogDescriptor): void;

  /**
   * Возвращает {@link FilterViewDialogDescriptor} для заданного {@link compositionId}.
   * @param compositionId Уникальный идентификатор элемента рабочего места, для которого должно использоваться переопределение диалога с параметрами представления.
   * @returns Объект {@link FilterViewDialogDescriptor} или значение undefined, если не найден объект соответствующий {@link compositionId}.
   */
  tryGet(compositionId: guid): FilterViewDialogDescriptor | undefined;
}

export class FilterViewDialogDescriptorRegistry implements IFilterViewDialogDescriptorRegistry {
  //#region fields

  private readonly _descriptors: Map<guid, FilterViewDialogDescriptor> = new Map<
    guid,
    FilterViewDialogDescriptor
  >();

  //#endregion

  //#region ctor

  private constructor() {}

  //#endregion

  //#region instance

  private static _instance: FilterViewDialogDescriptorRegistry;

  /**
   * Экземпляр этого объекта.
   */
  public static get instance(): FilterViewDialogDescriptorRegistry {
    if (!FilterViewDialogDescriptorRegistry._instance) {
      FilterViewDialogDescriptorRegistry._instance = new FilterViewDialogDescriptorRegistry();
    }

    return FilterViewDialogDescriptorRegistry._instance;
  }

  //#endregion

  //#region IFilterViewDialogDescriptorRegistry members

  register(compositionId: string, descriptor: FilterViewDialogDescriptor): void {
    this._descriptors.set(compositionId, descriptor);
  }

  tryGet(compositionId: string): FilterViewDialogDescriptor | undefined {
    return this._descriptors.get(compositionId);
  }

  //#endregion
}
