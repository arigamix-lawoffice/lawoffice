import {
  IViewControlDataProvider,
  FileListViewModel,
  ViewControlDataProviderResponse,
  ViewControlDataProviderRequest,
  FileViewModel
} from 'tessa/ui/cards/controls';
import { RequestParameter, ViewMetadataSealed, RequestCriteria } from 'tessa/views/metadata';
import { ValidationResultBuilder } from 'tessa/platform/validation';
import { formatSize } from 'tessa/platform/formatting';
import { IStorage } from 'tessa/platform/storage';
import { LocalizationManager } from 'tessa/localization';
import { SortingColumn, SortDirection } from 'tessa/views';
import { SchemeType } from 'tessa/scheme';

/**
 * File data provider.
 */
export class CardFilesDataProvider implements IViewControlDataProvider {
  /**
   * Creates data provider for files in view.
   * @param viewMetadata source view metadata
   * @param fileControl target file view model
   */
  constructor(readonly viewMetadata: ViewMetadataSealed, readonly fileControl: FileListViewModel) {}

  async getDataAsync(
    request: ViewControlDataProviderRequest
  ): Promise<ViewControlDataProviderResponse> {
    const result = new ViewControlDataProviderResponse(new ValidationResultBuilder());
    this.addFieldDescriptions(result);
    this.populateDataRowsAsync(request, result);
    return result;
  }

  /**
   * Make base description of data response.
   * @param result data response
   */
  protected addFieldDescriptions(result: ViewControlDataProviderResponse): void {
    result.columns.push(['GroupCaption', SchemeType.String]);
    result.columns.push(['Caption', SchemeType.String]);
    result.columns.push(['CategoryCaption', SchemeType.String]);
    result.columns.push(['Size', SchemeType.String]);
    result.columns.push(['SizeAbsolute', SchemeType.Int64]);
  }

  protected populateDataRowsAsync(
    request: ViewControlDataProviderRequest,
    result: ViewControlDataProviderResponse
  ): void {
    const requestParameters: RequestParameter[] =
      this.buildParametersCollectionFromRequest(request);

    // если фильтры сбросили через кнопку в представлении, то нужно сбросить фильтрацию в файловом контроле.
    if (
      this.fileControl.selectedFiltering &&
      !requestParameters.some(x => x.name === 'FilterParameter')
    ) {
      this.fileControl.selectedFiltering = null;
    }

    const filter = this.getFilter(request);
    let filteredFiles = this.getItems();
    filteredFiles = filteredFiles.filter(filter);

    const rows: Array<IStorage> = [];
    for (const file of filteredFiles) {
      rows.push(this.mapFileToRow(file));
    }

    const sorter = this.getSorter(request);
    rows.sort(sorter);
    result.rows.push(...rows);
  }

  protected getItems(): readonly FileViewModel[] {
    return this.fileControl.filteredFiles;
  }

  protected getFilter(request: ViewControlDataProviderRequest): (item: FileViewModel) => boolean {
    const filter = new FileFilter(this, request);
    filter.initialize();
    return filter.filter;
  }

  protected getSorter(
    request: ViewControlDataProviderRequest
  ): (lhv: IStorage, rhv: IStorage) => number {
    const sorter = new FileSorter(this, request);
    sorter.initialize();
    return sorter.sort;
  }

  public buildParametersCollectionFromRequest(
    request: ViewControlDataProviderRequest
  ): RequestParameter[] {
    const parametersCollection: RequestParameter[] = [];
    for (const action of request.parametersActions) {
      action(parametersCollection);
    }
    return parametersCollection;
  }

  protected mapFileToRow(file: FileViewModel): IStorage {
    const size =
      formatSize(file.model.size, 1000) +
      LocalizationManager.instance.localize('$Format_Unit_Kilobytes');

    let groupCaption = '';
    if (this.fileControl.groups) {
      for (const group of this.fileControl.groups.values()) {
        if (group.files.find(x => x === file)) {
          groupCaption = group.caption;
          break;
        }
      }
    }

    return {
      FileViewModel: file,
      GroupCaption: groupCaption,
      CategoryCaption:
        file.model.category?.caption ??
        LocalizationManager.instance.localize('$UI_Cards_FileNoCategory'),
      Caption: file.caption,
      SizeAbsolute: file.model.size,
      Size: size
    };
  }
}

/**
 * Parameter filter criteria joined by logical OR (for single parameter).
 */
export class ParameterFilterItem {
  readonly criterialFilters: Array<(item: FileViewModel) => boolean> = [];
  constructor(...criterialFilters: Array<(item: FileViewModel) => boolean>) {
    this.criterialFilters = criterialFilters;
  }
}

export class FileFilter {
  readonly parameterFilters: Array<ParameterFilterItem> = [];
  readonly dataProvider: CardFilesDataProvider;
  readonly request: ViewControlDataProviderRequest;

  constructor(dataProvider: CardFilesDataProvider, request: ViewControlDataProviderRequest) {
    this.dataProvider = dataProvider;
    this.request = request;
  }

  public initialize(): void {
    this.parameterFilters.length = 0;
    this.parameterFilters.push(new ParameterFilterItem(() => true));
    const requestParameters = this.dataProvider.buildParametersCollectionFromRequest(this.request);
    for (const parameter of requestParameters) {
      if (parameter.name === 'Caption') {
        const filterBlock = new ParameterFilterItem();
        for (const criteria of parameter.criteriaValues) {
          this.appendCriteriaToCaptionFilterFunc(criteria, filterBlock);
        }
        this.parameterFilters.push(filterBlock);
      }
    }
  }

  filter = (item: FileViewModel): boolean => {
    return this.parameterFilters
      .map(block => block.criterialFilters.reduce((current, func) => current || func(item), false))
      .reduce((filterResult, blockResult) => filterResult && blockResult, true);
  };

  /**
   * Build up criteria for name filtration.
   * @param criteria source criteria
   * @param filterBlock target filtering block
   */
  protected appendCriteriaToCaptionFilterFunc(
    criteria: RequestCriteria,
    filterBlock: ParameterFilterItem
  ): void {
    const valueAsString = (criteria.values[0].value as string)?.toLocaleUpperCase() ?? '';
    switch (criteria.criteriaName) {
      case 'Contains':
        filterBlock.criterialFilters.push(x =>
          x.caption.toLocaleUpperCase().includes(valueAsString)
        );
        break;
      case 'Equality':
        filterBlock.criterialFilters.push(x => x.caption.toLocaleUpperCase() === valueAsString);
        break;
      case 'StartWith':
        filterBlock.criterialFilters.push(x =>
          x.caption.toLocaleUpperCase().startsWith(valueAsString)
        );
        break;
      case 'EndWith':
        filterBlock.criterialFilters.push(x =>
          x.caption.toLocaleUpperCase().endsWith(valueAsString)
        );
        break;
    }
  }
}

export class FileSorter {
  readonly dataProvider: CardFilesDataProvider;
  readonly request: ViewControlDataProviderRequest;
  readonly sortingColumns: Array<SortingColumn> = [];

  constructor(dataProvider: CardFilesDataProvider, request: ViewControlDataProviderRequest) {
    this.dataProvider = dataProvider;
    this.request = request;
  }

  public initialize(): void {
    for (const column of this.request.sortingColumns) {
      const sortingColumn = new SortingColumn(
        this.dataProvider.viewMetadata.columns.get(column.alias!)?.sortBy ?? '',
        column.sortDirection
      );
      this.sortingColumns.push(sortingColumn);
    }
  }

  sort = (lhv: IStorage, rhv: IStorage): number => {
    if (!lhv && !rhv) {
      return 0;
    }
    if (!lhv) {
      return -1;
    }
    if (!rhv) {
      return 1;
    }
    const lvm: FileViewModel = lhv['FileViewModel'];
    const rvm: FileViewModel = rhv['FileViewModel'];
    // оргининал всегда "выше" чем копия файла независимо от направления сортировки
    if (lvm.model.origin === rvm.model) {
      return 1;
    }

    if (rvm.model.origin === lvm.model) {
      return -1;
    }

    for (const sortingColumn of this.sortingColumns) {
      let comparsion = -0;
      const lvalue = lhv[sortingColumn.alias] ?? '';
      const rvalue = rhv[sortingColumn.alias] ?? '';
      if (typeof lvalue === 'string') {
        comparsion = lvalue.localeCompare(rvalue);
      } else {
        comparsion = lvalue - rvalue;
      }
      if (comparsion === 0) {
        continue;
      }
      return sortingColumn.sortDirection === SortDirection.Ascending ? comparsion : -comparsion;
    }
    return 0;
  };
}
