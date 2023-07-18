import { ArrayStorage } from 'tessa/platform/storage';
import { CardAutoCompleteSearchMode } from 'tessa/cards/cardAutoCompleteSearchMode';
import { CardMetadataSectionSealed } from 'tessa/cards/metadata';
import { CardRow, CardRowState } from 'tessa/cards';
import { CardTypeEntryControl, TypeSettings } from 'tessa/cards/types';
import { clearStorageRows } from '../../misc/ocrUtilities';
import {
  containsCriteriaOperator,
  CriteriaOperator,
  endsWithCriteriaOperator,
  equalsCriteriaOperator,
  startsWithCriteriaOperator,
  ViewParameterMetadataSealed
} from 'tessa/views/metadata';
import { DotNetType, Guid, SchemeDbType } from 'tessa/platform';
import { ICardModel } from 'tessa/ui/cards';
import { ITessaView, RequestParameterBuilder, TessaViewRequest, ViewService } from 'tessa/views';
import { observable, runInAction } from 'mobx';
import { showViewsDialog } from 'tessa/ui/uiHost';
import { tryGetFromSettings } from 'tessa/ui/uiHelper';

/** Модель представления с данными для контрола выбора выбора даты из календаря. */
export class OcrReferenceViewModel {
  //#region constructors

  /**
   * Создает экземпляр класса {@link OcrReferenceViewModel}.
   * @param {CardTypeEntryControl} control
   * Объект, описывающий расположение и свойства элемента
   * управления для привязки к полям строковой секции карточки.
   * @param {ICardModel} model Модель карточки, доступная в UI.
   */
  constructor(
    control: CardTypeEntryControl,
    model: ICardModel,
    prefix: string,
    metadataSectionSealed: CardMetadataSectionSealed
  ) {
    this._mappingFieldName = prefix;

    // Получение метаданных секции, связанной с контролом
    const metadataColumnSealed = metadataSectionSealed.getColumnById(control.complexColumnId!);
    if (!metadataColumnSealed) {
      throw new Error(`Can not find metadata for column with id '${control.complexColumnId}'.`);
    }

    const mappingComplexFields = model.card.sections.tryGet('OcrMappingComplexFields')?.rows;
    if (!mappingComplexFields) {
      throw new Error("Can not find card section with name 'OcrMappingComplexFields'.");
    }
    this._mappingComplexFields = mappingComplexFields;

    const settings = control.controlSettings;
    const refSectionSetting = settings['RefSection'];
    let refSection: string[] | null;
    if (typeof refSectionSetting === 'string') {
      refSection = !!refSectionSetting ? [refSectionSetting] : null;
    } else {
      refSection = refSectionSetting as string[];
    }
    this._viewRefSection = refSection ?? [metadataColumnSealed.referencedSection!.name ?? ''];

    this._view = this.getViewFromSettings(settings);
    this._viewParameter = this.getViewParameterFromSettings(settings);
    this._viewReferencePrefix = this.getViewReferencePrefixFromSettings(settings);
    this._criteriaOperator = this.getCriteriaOperator(settings);
    this.hideClearButton = !tryGetFromSettings<boolean>(settings, 'IsClearFieldVisible', true);
    this.hideSelectorButton = tryGetFromSettings<boolean>(settings, 'HideSelectorButton', false);
    this._manualInput = tryGetFromSettings(settings, 'ManualInput', false);
  }

  //#endregion

  //#region fields

  private readonly _mappingFieldName: string;
  private readonly _mappingComplexFields: ArrayStorage<CardRow>;

  private readonly _view: ITessaView;
  private readonly _viewReferencePrefix: string;
  private readonly _viewRefSection: ReadonlyArray<string>;
  private readonly _viewParameter: ViewParameterMetadataSealed;
  private readonly _criteriaOperator: CriteriaOperator;
  private readonly _manualInput: boolean;

  /** Признак выполнения загрузки данных. */
  @observable
  private _isDataLoading: boolean;

  //#endregion

  //#region properties

  /** Скрывать кнопку очистки значения. */
  public readonly hideClearButton: boolean;

  /** Скрывать кнопку выбора. */
  public readonly hideSelectorButton: boolean;

  /** Признак выполнения загрузки данных. */
  public get isDataLoading(): boolean {
    return this._isDataLoading;
  }
  public set isDataLoading(value: boolean) {
    runInAction(() => (this._isDataLoading = value));
  }

  /** Разрешен ручной ввод. */
  public get manualInput(): boolean {
    return this._manualInput;
  }

  //#endregion

  //#region public methods

  public async selectValue(parentRowId: guid): Promise<string | null> {
    let displayed: string | null = null;
    let value: string | null = null;

    await showViewsDialog(this._viewRefSection, async selectedValue => {
      // если значение не было выбрано, то ничего не делаем
      if (!selectedValue) {
        return;
      }

      // сначала очищаем все значения, которые были в полях для маппинга
      clearStorageRows(this._mappingComplexFields, r => Guid.equals(r.parentRowId, parentRowId));

      // вычисляем префикс для ссылочного значения
      const columnPrefix = this._viewReferencePrefix || selectedValue.metadata!.colPrefix;
      const columnPrefixUpper = columnPrefix?.toUpperCase() || '';

      // Вычисляем отображаемую колонку:
      // 1) если отображаемая колонка, заданная в представлении, начинается с подходящего префикса, то используем ее
      // 2) в противном случае, отображаемая колонка будет вычислена, как первая колонка строкового типа
      const displayColumn = selectedValue.metadata?.displayValueColumn;
      const useDisplayColumn = displayColumn?.toUpperCase()?.startsWith(columnPrefixUpper);
      let hasValueForDisplayColumn = false;

      for (const [columnName, columnValue] of selectedValue.selectedRow!.entries()) {
        if (columnName.toUpperCase().startsWith(columnPrefixUpper)) {
          const stringValue = columnValue?.toString()?.trim();
          value = !stringValue ? null : stringValue;

          const columnMetadata = selectedValue.viewMetadata?.columns.get(columnName);
          if (
            !hasValueForDisplayColumn &&
            (useDisplayColumn
              ? columnName === displayColumn
              : columnMetadata?.schemeType?.dotNetType === DotNetType.String)
          ) {
            hasValueForDisplayColumn = true;
            displayed = value;
          }

          const row = new CardRow();
          row.rowId = Guid.newGuid();
          row.parentRowId = parentRowId;
          row.state = CardRowState.Inserted;
          const fieldName = columnName.substring(columnPrefixUpper.length);
          const mappingFieldName = `${this._mappingFieldName}${fieldName}`;
          row.set('Field', mappingFieldName, DotNetType.String);
          row.set('Value', value, DotNetType.String);
          this._mappingComplexFields.add(row);
        }
      }
    });

    return displayed;
  }

  public async initializeMappingComplexFields(
    text: string,
    parentRowId: guid
  ): Promise<[displayed: string | null, value: string | null]> {
    this.isDataLoading = true;

    try {
      // Создаем запрос к представлению для поиска записи
      const request = new TessaViewRequest(this._view!.metadata);
      const parameter = new RequestParameterBuilder()
        .withMetadata(this._viewParameter!)
        .addCriteria(this._criteriaOperator, text, text)
        .asRequestParameter();
      request.values.push(parameter);

      // Создаем структуру для хранения записи из представления
      let rowData: { name: string; value: unknown; type: SchemeDbType }[] | null = null;
      // Вычисляем префикс ссылки из представления
      const columnPrefix = this._viewReferencePrefix.toUpperCase();

      try {
        // Пытаемся получить данные из первой строки представления с учетом параметра фильтрации
        const result = await this._view!.getData(request);
        const firstRow = result.rows[0];
        if (firstRow) {
          rowData = result.columns.map((column, index) => ({
            name: column,
            value: firstRow[index],
            type: result.schemeTypes[index].dbType
          }));
        }
      } finally {
        // Удаляем все записи с информацией о полях ссылки
        clearStorageRows(this._mappingComplexFields!, r => Guid.equals(r.parentRowId, parentRowId));
      }

      // Если была найдена запись в представлении, то копируем из нее значения
      if (rowData && rowData.length > 0) {
        let isValueSet = false;
        let value: string | null = null;
        for (const item of rowData) {
          if (item.name.toUpperCase().startsWith(columnPrefix)) {
            this.addMappingComplexFieldRow(item.name, columnPrefix, item.value, parentRowId);
            if (!isValueSet && item.type === SchemeDbType.String) {
              isValueSet = true;
              value = `${item.value}`;
            }
          }
        }
        return [text, value];
      }

      // Если же контрол поддерживает ручной ввод, то пытаемся установить параметры ссылки
      if (this.manualInput) {
        let isGuidValueSet = false;
        let isStringValueSet = false;
        for (const [columnName, columnMetadata] of this._view!.metadata.columns) {
          if (columnName.toUpperCase().startsWith(columnPrefix)) {
            const columnType = columnMetadata.schemeType?.dotNetType;
            if (!isGuidValueSet && columnType === DotNetType.Guid) {
              this.addMappingComplexFieldRow(columnName, columnPrefix, Guid.empty, parentRowId);
              isGuidValueSet = true;
            } else if (!isStringValueSet && columnType === DotNetType.String) {
              this.addMappingComplexFieldRow(columnName, columnPrefix, text, parentRowId);
              isStringValueSet = true;
            }
          }
        }
      }

      return [text, null];
    } finally {
      this.isDataLoading = false;
    }
  }

  public clearComplexFieldsValues(parentRowId: guid): void {
    for (const mappingComplexField of this._mappingComplexFields!) {
      if (Guid.equals(mappingComplexField.parentRowId, parentRowId)) {
        mappingComplexField.set('Value', null, DotNetType.String);
      }
    }
  }

  //#endregion

  //#region private methods

  private getViewFromSettings(settings: TypeSettings): ITessaView {
    const viewAlias = tryGetFromSettings<string>(settings, 'ViewAlias', '');
    if (!viewAlias) {
      throw new Error('Can not find view alias at control settings.');
    }
    const view = ViewService.instance.getByName(viewAlias);
    if (!view) {
      throw new Error(`Can not find view with alias '${viewAlias}' at metadata.`);
    }

    return view;
  }

  private getViewParameterFromSettings(settings: TypeSettings): ViewParameterMetadataSealed {
    const parameterName = tryGetFromSettings<string>(settings, 'ParameterAlias', '');
    if (!parameterName) {
      throw new Error('Can not find view parameter at control settings.');
    }
    const viewParameter = this._view?.metadata.parameters.get(parameterName);
    if (!viewParameter) {
      throw new Error(`Can not find view parameter '${parameterName}' at metadata.`);
    }

    return viewParameter;
  }

  private getViewReferencePrefixFromSettings(settings: TypeSettings): string {
    const viewRefPrefix = tryGetFromSettings<string>(settings, 'ViewReferencePrefix', undefined);
    if (viewRefPrefix == undefined) {
      throw new Error('Can not find view reference prefix at control settings.');
    }

    return !!viewRefPrefix ? viewRefPrefix : this._mappingFieldName;
  }

  private getCriteriaOperator(settings: TypeSettings): CriteriaOperator {
    const searchMode = tryGetFromSettings<CardAutoCompleteSearchMode>(
      settings,
      'SearchMode',
      CardAutoCompleteSearchMode.Contains
    );

    switch (searchMode) {
      case CardAutoCompleteSearchMode.Contains:
        return containsCriteriaOperator();
      case CardAutoCompleteSearchMode.StartsWith:
        return startsWithCriteriaOperator();
      case CardAutoCompleteSearchMode.EndsWith:
        return endsWithCriteriaOperator();
      case CardAutoCompleteSearchMode.Equals:
        return equalsCriteriaOperator();
    }
  }

  private addMappingComplexFieldRow(
    columnName: string,
    columnPrefix: string,
    columnValue: unknown,
    parentRowId: guid
  ): void {
    const row = new CardRow();
    row.rowId = Guid.newGuid();
    row.parentRowId = parentRowId;
    row.state = CardRowState.Inserted;
    const fieldName = columnName.substring(columnPrefix.length);
    const mappingFieldName = `${this._mappingFieldName}${fieldName}`;
    row.set('Field', mappingFieldName, DotNetType.String);
    const compiledValue = columnValue ? `${columnValue}` : null;
    row.set('Value', compiledValue, DotNetType.String);
    this._mappingComplexFields!.add(row);
  }

  //#endregion
}
