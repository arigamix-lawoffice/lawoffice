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
export declare class FilterViewDialogDescriptorRegistry implements IFilterViewDialogDescriptorRegistry {
    private readonly _descriptors;
    private constructor();
    private static _instance;
    /**
     * Экземпляр этого объекта.
     */
    static get instance(): FilterViewDialogDescriptorRegistry;
    register(compositionId: string, descriptor: FilterViewDialogDescriptor): void;
    tryGet(compositionId: string): FilterViewDialogDescriptor | undefined;
}
