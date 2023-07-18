import { CardFilesDataProvider } from './cardFilesDataProvider';
import { ViewControlInitializationStrategy, ViewControlInitializationContext, FileListViewModel } from 'tessa/ui/cards/controls';
import { IViewMetadata, ViewMetadataSealed } from 'tessa/views/metadata';
export declare class FilesViewCardControlInitializationStrategy extends ViewControlInitializationStrategy {
    initializeMetadata(context: ViewControlInitializationContext): void;
    initializeDataProvider(context: ViewControlInitializationContext): void;
    /**
     * Create view metadata for files view.
     * @param context initialization context
     * @returns view metadata
     */
    protected createViewMetadata(_context: ViewControlInitializationContext): IViewMetadata;
    /**
     * Create data provider for files view.
     * @param context initialization context
     * @param viewMetadata metadata of source view
     * @param fileControl target view model
     * @returns data provider
     */
    protected createDataProvider(_context: ViewControlInitializationContext, viewMetadata: ViewMetadataSealed, fileControl: FileListViewModel): CardFilesDataProvider;
}
