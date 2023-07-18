import { AdvancedCardDialogManager, CardModelFlags, ICardModel } from 'tessa/ui/cards';
import { Card, CardSection } from 'tessa/cards';
import { UIButton, UIContext, createDialogForm } from 'tessa/ui';

import { DotNetType } from 'tessa/platform';
import { FilterViewDialogDescriptor } from './filterViewDialogDescriptor';
import { IViewParameters } from 'tessa/ui/views/parameters';
import { MapStorage } from 'tessa/platform/storage';
import { ParameterMapping } from './parameterMapping';
import { RequestParameter } from 'tessa/views/metadata';
import { RequestParameterBuilder } from 'tessa/views';
import { runInAction } from 'mobx';

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

export class AdvancedFilterViewDialogManager implements IAdvancedFilterViewDialogManager {
  //#region ctor

  private constructor() {}

  //#endregion

  //#region instance
  private static _instance: AdvancedFilterViewDialogManager;

  /**
   * Экземпляр этого объекта.
   */
  public static get instance(): AdvancedFilterViewDialogManager {
    if (!AdvancedFilterViewDialogManager._instance) {
      AdvancedFilterViewDialogManager._instance = new AdvancedFilterViewDialogManager();
    }

    return AdvancedFilterViewDialogManager._instance;
  }

  //#endregion

  //#region IAdvancedFilterViewDialogManager members

  async open(descriptor: FilterViewDialogDescriptor, parameters: IViewParameters): Promise<void> {
    const dialogCardModel = await this.createDialogCardModel(
      descriptor.dialogName,
      descriptor.formAlias
    );

    await this.showDialog(descriptor, parameters, dialogCardModel);
  }

  //#endregion

  //#region protected methods

  /**
   * Создаёт модель карточки диалога, содержащего параметры представления.
   * @param dialogName Имя типа диалога.
   * @param formAlias Алиас формы диалога или undefined, если требуется создать форму для первой вкладки типа диалога.
   * @returns Модель карточки диалога.
   */
  protected async createDialogCardModel(
    dialogName: string,
    formAlias: string | undefined
  ): Promise<ICardModel> {
    const result = await createDialogForm(
      'CarViewParameters',
      'MainTab',
      undefined,
      undefined,
      async (newCardModel: ICardModel) => {
        newCardModel.card.version = 1;
        newCardModel.flags |= CardModelFlags.IgnoreChanges;
      }
    );
    if (!result) {
      throw new Error(
        `Failed to create dialog. Dialog name: "${dialogName}". Form alias: "${
          formAlias ?? 'undefined'
        }".`
      );
    }

    const [form, cardModel] = result;
    cardModel.mainForm = form;

    return cardModel;
  }

  /**
   * Отображает диалог, содержащий параметры фильтрации представления.
   * @param descriptor {@link FilterViewDialogDescriptor}
   * @param parameters {@link IViewParameters}
   * @param dialogCardModel Модель карточки диалога.
   */
  protected async showDialog(
    descriptor: FilterViewDialogDescriptor,
    parameters: IViewParameters,
    dialogCardModel: ICardModel
  ): Promise<void> {
    await AdvancedCardDialogManager.instance.showCardModel({
      cardModel: dialogCardModel,
      displayValue: '$Views_FilterDialog_Caption',
      dialogOptions: {
        withDialogWallpaper: false,
        dialogAutoSize: true
      },
      prepareEditorAction: editor => {
        editor.statusBarIsVisible = false;

        AdvancedFilterViewDialogManager.fillFields(
          parameters,
          editor.cardModel!.card,
          descriptor.parametersMapping
        );

        editor.toolbar.clearItems();
        editor.bottomToolbar.clearItems();
        editor.bottomDialogButtons.length = 0;

        editor.bottomDialogButtons.push(
          UIButton.create({
            caption: '$UI_Common_OK',
            buttonAction: async () => {
              const editor = UIContext.current.cardEditor;

              if (!editor || editor.operationInProgress) {
                return;
              }

              const params = AdvancedFilterViewDialogManager.createParameters(
                parameters,
                editor.cardModel!.card,
                descriptor.parametersMapping
              );
              runInAction(() => {
                parameters.clear();
                parameters.addParameters(...params);
              });

              await editor.close();
            }
          })
        );

        editor.bottomDialogButtons.push(
          UIButton.create({
            caption: '$UI_Common_Cancel',
            buttonAction: async () => {
              const editor = UIContext.current.cardEditor;

              if (!editor || editor.operationInProgress) {
                return;
              }

              await editor.close();
            }
          })
        );

        return true;
      }
    });
  }

  /**
   * Заполняет поля карточки, данными параметров запроса к представлению.
   * @param parameters Список параметров представления.
   * @param card Карточка, содержащая параметры.
   * @param parameterMappings Коллекция, содержащая информацию о связи параметров представления и полей карточки.
   */
  protected static fillFields(
    parameters: IViewParameters,
    card: Card,
    parameterMappings: ReadonlyArray<ParameterMapping>
  ): void {
    const sections = card.sections;

    for (const parameterMapping of parameterMappings) {
      AdvancedFilterViewDialogManager.fillField(parameters, sections, parameterMapping);
    }
  }

  /**
   * Заполняет поле {@link ParameterMapping.valueFieldName} в секции {@link ParameterMapping.valueSectionName}, содержащее параметр фильтрации представления {@link ParameterMapping.alias}.
   * @param parameters Список параметров представления.
   * @param sections Секции, содержащиеся в карточке диалога с параметрами представления.
   * @param parameterMapping {@link ParameterMapping}
   */
  protected static fillField(
    parameters: IViewParameters,
    sections: MapStorage<CardSection>,
    parameterMapping: ParameterMapping
  ): void {
    const parameter = parameters.parameters.find(x => x.metadata?.alias === parameterMapping.alias);

    if (!parameter) {
      return;
    }

    const value = parameter.criteriaValues[0]?.values[0];
    if (value == undefined) {
      return;
    }

    const valueSection = sections.get(parameterMapping.valueSectionName);

    if (!valueSection) {
      return;
    }

    valueSection.fields.set(
      parameterMapping.valueFieldName,
      value.value,
      parameter.metadata?.schemeType?.dotNetType ?? DotNetType.String
    );

    if (!parameterMapping.displayValueSectionName || !parameterMapping.displayValueFieldName) {
      return;
    }

    const displayValueSection = sections.get(parameterMapping.displayValueSectionName);

    if (!displayValueSection) {
      return;
    }

    displayValueSection.fields.set(
      parameterMapping.displayValueFieldName,
      value.text,
      DotNetType.String
    );
  }

  /**
   * Создаёт параметры запроса к представлению.
   * @param parameters Список параметров представления.
   * @param card Карточка, содержащая параметры.
   * @param parameterMappings >Коллекция, содержащая информацию о связи параметров представления и полей карточки.
   * @returns Список параметров, передаваемых в запросе к представлению.
   */
  protected static createParameters(
    parameters: IViewParameters,
    card: Card,
    parameterMappings: ReadonlyArray<ParameterMapping>
  ): RequestParameter[] {
    const params: RequestParameter[] = [];
    const sections = card.sections;

    for (const parameterMapping of parameterMappings) {
      AdvancedFilterViewDialogManager.addParameterIfValueNotEmpty(
        params,
        parameters,
        sections,
        parameterMapping
      );
    }

    return params;
  }

  /**
   * Добавляет параметр в запрос к представлению, если поле {@link ParameterMapping.valueFieldName} в секции{@link ParameterMapping.valueSectionName} содержит данные.
   * @param paramsStorage Список параметров, передаваемых в запросе к представлению.
   * @param parameters Список параметров представления.
   * @param sections Секции, содержащиеся в карточке диалога с параметрами представления.
   * @param parameterMapping {@link ParameterMapping}
   */
  protected static addParameterIfValueNotEmpty(
    paramsStorage: RequestParameter[],
    parameters: IViewParameters,
    sections: MapStorage<CardSection>,
    parameterMapping: ParameterMapping
  ): void {
    const valueSection = sections.get(parameterMapping.valueSectionName);

    if (!valueSection) {
      return;
    }

    const fieldValue = valueSection.fields.tryGet(parameterMapping.valueFieldName);

    if (fieldValue == undefined) {
      return;
    }

    const parameterMetadata = parameters.metadata.find(x => x.alias === parameterMapping.alias);

    if (!parameterMetadata) {
      return;
    }

    let displayValue = fieldValue.toString();
    if (parameterMapping.displayValueSectionName && parameterMapping.displayValueFieldName) {
      const displayValueSection = sections.get(parameterMapping.displayValueSectionName);

      if (displayValueSection) {
        const displayValueField = displayValueSection.fields.tryGet(
          parameterMapping.displayValueFieldName
        );

        if (displayValueField != undefined) {
          displayValue = displayValueField.toString();
        }
      }
    }

    const requestParameter = new RequestParameterBuilder()
      .withMetadata(parameterMetadata)
      .addCriteria(
        parameterMapping.criteriaOperator ?? parameterMetadata.getDefaultCriteria(),
        displayValue,
        fieldValue
      )
      .asRequestParameter();

    paramsStorage.push(requestParameter);
  }

  //#endregion
}
