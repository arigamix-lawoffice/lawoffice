import { IViewControlDataProvider, FileListViewModel, ViewControlDataProviderResponse, ViewControlDataProviderRequest, FileViewModel } from 'tessa/ui/cards/controls';
import { RequestParameter, ViewMetadataSealed, RequestCriteria } from 'tessa/views/metadata';
import { IStorage } from 'tessa/platform/storage';
import { SortingColumn } from 'tessa/views';
/**
 * File data provider.
 */
export declare class CardFilesDataProvider implements IViewControlDataProvider {
    readonly viewMetadata: ViewMetadataSealed;
    readonly fileControl: FileListViewModel;
    /**
     * Creates data provider for files in view.
     * @param viewMetadata source view metadata
     * @param fileControl target file view model
     */
    constructor(viewMetadata: ViewMetadataSealed, fileControl: FileListViewModel);
    getDataAsync(request: ViewControlDataProviderRequest): Promise<ViewControlDataProviderResponse>;
    /**
     * Make base description of data response.
     * @param result data response
     */
    protected addFieldDescriptions(result: ViewControlDataProviderResponse): void;
    protected populateDataRowsAsync(request: ViewControlDataProviderRequest, result: ViewControlDataProviderResponse): void;
    protected getItems(): readonly FileViewModel[];
    protected getFilter(request: ViewControlDataProviderRequest): (item: FileViewModel) => boolean;
    protected getSorter(request: ViewControlDataProviderRequest): (lhv: IStorage, rhv: IStorage) => number;
    buildParametersCollectionFromRequest(request: ViewControlDataProviderRequest): RequestParameter[];
    protected mapFileToRow(file: FileViewModel): IStorage;
}
/**
 * Parameter filter criteria joined by logical OR (for single parameter).
 */
export declare class ParameterFilterItem {
    readonly criterialFilters: Array<(item: FileViewModel) => boolean>;
    constructor(...criterialFilters: Array<(item: FileViewModel) => boolean>);
}
export declare class FileFilter {
    readonly parameterFilters: Array<ParameterFilterItem>;
    readonly dataProvider: CardFilesDataProvider;
    readonly request: ViewControlDataProviderRequest;
    constructor(dataProvider: CardFilesDataProvider, request: ViewControlDataProviderRequest);
    initialize(): void;
    filter: (item: FileViewModel) => boolean;
    /**
     * Build up criteria for name filtration.
     * @param criteria source criteria
     * @param filterBlock target filtering block
     */
    protected appendCriteriaToCaptionFilterFunc(criteria: RequestCriteria, filterBlock: ParameterFilterItem): void;
}
export declare class FileSorter {
    readonly dataProvider: CardFilesDataProvider;
    readonly request: ViewControlDataProviderRequest;
    readonly sortingColumns: Array<SortingColumn>;
    constructor(dataProvider: CardFilesDataProvider, request: ViewControlDataProviderRequest);
    initialize(): void;
    sort: (lhv: IStorage, rhv: IStorage) => number;
}
