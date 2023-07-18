import { IStorage } from 'tessa/platform/storage';
import { FileListViewModel, FileViewModel, ViewControlDataProviderResponse } from 'tessa/ui/cards/controls';
import { ViewMetadataSealed } from 'tessa/views/metadata';
import { CardFilesDataProvider } from './cardFilesDataProvider';
/**
 * Customized provider for files in view with addition of Creation date column and corresponding data.
 */
export declare class CustomCardFilesDataProvider extends CardFilesDataProvider {
    readonly viewMetadata: ViewMetadataSealed;
    readonly fileControl: FileListViewModel;
    constructor(viewMetadata: ViewMetadataSealed, fileControl: FileListViewModel);
    /**
     * Fill in data columns descriptions in data response.
     * @param result response with columns filled in
     */
    protected addFieldDescriptions(result: ViewControlDataProviderResponse): void;
    /**
     * Fill in data row based on given file view model.
     * @param file file view model
     * @returns one row in data response.
     */
    protected mapFileToRow(file: FileViewModel): IStorage;
}
