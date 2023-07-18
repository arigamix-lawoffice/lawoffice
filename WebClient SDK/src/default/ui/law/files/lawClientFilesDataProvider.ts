import { IStorage } from 'tessa/platform/storage';
import { SchemeType } from 'tessa/scheme/schemeType';
import {
  FileListViewModel,
  FileViewModel,
  ViewControlDataProviderRequest,
  ViewControlDataProviderResponse,
  ViewControlViewModel
} from 'tessa/ui/cards/controls';
import { RequestCriteria, RequestParameter, ViewMetadataSealed } from 'tessa/views/metadata';
import { LawColumnsConst } from './lawColumnsConst';
import { tryGetFromInfo } from 'tessa/ui';
import { CardFilesDataProvider, ParameterFilterItem } from '../../cardFiles/cardFilesDataProvider';
import { InfoMarks } from 'src/law/infoMarks';

export class LawClientFilesDataProvider extends CardFilesDataProvider {
  constructor(readonly viewMetadata: ViewMetadataSealed, readonly fileControl: FileListViewModel, readonly filesViewControl: ViewControlViewModel) {
    super(viewMetadata, fileControl);
  }

  protected addFieldDescriptions(result: ViewControlDataProviderResponse): void {
    result.columns.push([LawColumnsConst.Name, SchemeType.String]);
    result.columns.push([LawColumnsConst.Extension, SchemeType.String]);
    result.columns.push([LawColumnsConst.Classification, SchemeType.String]);
    result.columns.push([LawColumnsConst.ReservedBy, SchemeType.NullableString]);
    result.columns.push([LawColumnsConst.Added, SchemeType.DateTime]);
    result.columns.push([LawColumnsConst.Created, SchemeType.NullableDateTime]);
  }

  protected mapFileToRow(file: FileViewModel): IStorage {
    let raw = {
      FileViewModel: file
    };
    raw[LawColumnsConst.Name] = file.caption;
    raw[LawColumnsConst.Extension] = tryGetFromInfo(file.model.options, InfoMarks.Extension);
    raw[LawColumnsConst.Classification] = tryGetFromInfo(file.model.options, InfoMarks.Classification);
    raw[LawColumnsConst.ReservedBy] = tryGetFromInfo(file.model.options, InfoMarks.ReservedBy);
    raw[LawColumnsConst.Added] = tryGetFromInfo(file.model.options, InfoMarks.Added);
    raw[LawColumnsConst.Created] = tryGetFromInfo(file.model.options, InfoMarks.Created);
    return raw;
  }

  protected populateDataRowsAsync(request: ViewControlDataProviderRequest, result: ViewControlDataProviderResponse): void {
    const requestParameters: RequestParameter[] = this.buildParametersCollectionFromRequest(request);

    if (this.fileControl.selectedFiltering && !requestParameters.some(x => x.name === 'FilterParameter')) {
      this.fileControl.selectedFiltering = null;
    }

    const filter = this.getFilter(request);
    let filteredFiles = this.getItems();
    filteredFiles = filteredFiles.filter(filter);

    this.filesViewControl.pageCount = filteredFiles.length / this.viewMetadata.pageLimit + 1;

    const rows: Array<IStorage> = [];
    for (const file of filteredFiles) {
      rows.push(this.mapFileToRow(file));
    }

    const sorter = this.getSorter(request);
    rows.sort(sorter);

    //paging
    const currPage =
      this.filesViewControl.currentPage > this.filesViewControl.pageCount ? this.filesViewControl.pageCount : this.filesViewControl.currentPage;
    const stInd = (currPage - 1) * this.viewMetadata.pageLimit;
    const endInd = stInd + this.viewMetadata.pageLimit > rows.length ? undefined : stInd + this.viewMetadata.pageLimit + 1;
    result.rows.push(...rows.slice(stInd, endInd));
  }

  protected getFilter(request: ViewControlDataProviderRequest): (item: FileViewModel) => boolean {
    const filter = new FileFilter(this, request);
    filter.initialize();
    return filter.filter;
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
      if (parameter.name === LawColumnsConst.Name) {
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
  protected appendCriteriaToCaptionFilterFunc(criteria: RequestCriteria, filterBlock: ParameterFilterItem): void {
    const valueAsString = (criteria.values[0].value as string)?.toLocaleUpperCase() ?? '';
    switch (criteria.criteriaName) {
      case 'Contains':
        filterBlock.criterialFilters.push(x => x.caption.toLocaleUpperCase().includes(valueAsString));
        break;
      case 'Equality':
        filterBlock.criterialFilters.push(x => x.caption.toLocaleUpperCase() === valueAsString);
        break;
      case 'StartWith':
        filterBlock.criterialFilters.push(x => x.caption.toLocaleUpperCase().startsWith(valueAsString));
        break;
      case 'EndWith':
        filterBlock.criterialFilters.push(x => x.caption.toLocaleUpperCase().endsWith(valueAsString));
        break;
    }
  }
}

