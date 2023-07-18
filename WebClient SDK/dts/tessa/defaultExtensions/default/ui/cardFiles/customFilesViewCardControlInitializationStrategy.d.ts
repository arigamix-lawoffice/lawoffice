import { ViewControlInitializationContext, FileListViewModel } from 'tessa/ui/cards/controls';
import { IViewMetadata, ViewMetadataSealed } from 'tessa/views/metadata';
import { CardFilesDataProvider } from './cardFilesDataProvider';
import { FilesViewCardControlInitializationStrategy } from './filesViewCardControlInitializationStrategy';
/**
 * Custom strategy that provide customized data for files in view.
 */
export declare class CustomFilesViewCardControlInitializationStrategy extends FilesViewCardControlInitializationStrategy {
    /**
     * Customize view metadata (base + new column)
     * @param context initialization context
     * @returns view metadata
     */
    protected createViewMetadata(context: ViewControlInitializationContext): IViewMetadata;
    /**
     * Customize data provider.
     * @param context initialization context
     * @param viewMetadata prepared view metadata
     * @param fileControl file view model
     * @returns customized card data provider
     */
    protected createDataProvider(context: ViewControlInitializationContext, viewMetadata: ViewMetadataSealed, fileControl: FileListViewModel): CardFilesDataProvider;
}
