import { ICardModel } from 'tessa/ui/cards';
import { Card, CardSection } from 'tessa/cards';
import { FilterViewDialogDescriptor } from './filterViewDialogDescriptor';
import { IViewParameters } from 'tessa/ui/views/parameters';
import { MapStorage } from 'tessa/platform/storage';
import { ParameterMapping } from './parameterMapping';
import { RequestParameter } from 'tessa/views/metadata';
/**
 * Объект, предоставляющий методы для открытия модального диалога с параметрами фильтрации представления.
 */
export interface IAdvancedFilterViewDialogManager {
    /**
     * Открывает диалог с параметрами фильтрации представления.
     * @param descriptor {@link FilterViewDialogDescriptor}
     * @param parameters {@link IViewParameters}
     */
    open(descriptor: FilterViewDialogDescriptor, parameters: IViewParameters): Promise<void>;
}
export declare class AdvancedFilterViewDialogManager implements IAdvancedFilterViewDialogManager {
    private constructor();
    private static _instance;
    /**
     * Экземпляр этого объекта.
     */
    static get instance(): AdvancedFilterViewDialogManager;
    open(descriptor: FilterViewDialogDescriptor, parameters: IViewParameters): Promise<void>;
    /**
     * Создаёт модель карточки диалога, содержащего параметры представления.
     * @param dialogName Имя типа диалога.
     * @param formAlias Алиас формы диалога или undefined, если требуется создать форму для первой вкладки типа диалога.
     * @returns Модель карточки диалога.
     */
    protected createDialogCardModel(dialogName: string, formAlias: string | undefined): Promise<ICardModel>;
    /**
     * Отображает диалог, содержащий параметры фильтрации представления.
     * @param descriptor {@link FilterViewDialogDescriptor}
     * @param parameters {@link IViewParameters}
     * @param dialogCardModel Модель карточки диалога.
     */
    protected showDialog(descriptor: FilterViewDialogDescriptor, parameters: IViewParameters, dialogCardModel: ICardModel): Promise<void>;
    /**
     * Заполняет поля карточки, данными параметров запроса к представлению.
     * @param parameters Список параметров представления.
     * @param card Карточка, содержащая параметры.
     * @param parameterMappings Коллекция, содержащая информацию о связи параметров представления и полей карточки.
     */
    protected static fillFields(parameters: IViewParameters, card: Card, parameterMappings: ReadonlyArray<ParameterMapping>): void;
    /**
     * Заполняет поле {@link ParameterMapping.valueFieldName} в секции {@link ParameterMapping.valueSectionName}, содержащее параметр фильтрации представления {@link ParameterMapping.alias}.
     * @param parameters Список параметров представления.
     * @param sections Секции, содержащиеся в карточке диалога с параметрами представления.
     * @param parameterMapping {@link ParameterMapping}
     */
    protected static fillField(parameters: IViewParameters, sections: MapStorage<CardSection>, parameterMapping: ParameterMapping): void;
    /**
     * Создаёт параметры запроса к представлению.
     * @param parameters Список параметров представления.
     * @param card Карточка, содержащая параметры.
     * @param parameterMappings >Коллекция, содержащая информацию о связи параметров представления и полей карточки.
     * @returns Список параметров, передаваемых в запросе к представлению.
     */
    protected static createParameters(parameters: IViewParameters, card: Card, parameterMappings: ReadonlyArray<ParameterMapping>): RequestParameter[];
    /**
     * Добавляет параметр в запрос к представлению, если поле {@link ParameterMapping.valueFieldName} в секции{@link ParameterMapping.valueSectionName} содержит данные.
     * @param paramsStorage Список параметров, передаваемых в запросе к представлению.
     * @param parameters Список параметров представления.
     * @param sections Секции, содержащиеся в карточке диалога с параметрами представления.
     * @param parameterMapping {@link ParameterMapping}
     */
    protected static addParameterIfValueNotEmpty(paramsStorage: RequestParameter[], parameters: IViewParameters, sections: MapStorage<CardSection>, parameterMapping: ParameterMapping): void;
}
